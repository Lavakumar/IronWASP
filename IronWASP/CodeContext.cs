//
// Copyright 2011-2012 Lavakumar Kuppan
//
// This file is part of IronWASP
//
// IronWASP is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3 of the License.
//
// IronWASP is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with IronWASP.  If not, see http://www.gnu.org/licenses/.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    public class CodeContext
    {
        char[] CodeChars;
        int CharPointer = 0;
        string Keyword = "";
        List<char> CurrentTokenChars = new List<char>();
        List<string> Contexts = new List<string>();

        public CodeContext(string Code, string Keyword)
        {
            this.CodeChars = Code.ToCharArray();
            this.Keyword = Keyword;
            this.CharPointer = 0;
        }

        public static List<string> GetJavaScriptContext(string Code, string Keyword)
        {
            CodeContext cc = new CodeContext(Code, Keyword);
            return cc.GetJavaScriptContext();
        }

        public static List<string> GetVisualBasicContext(string Code, string Keyword)
        {
            CodeContext cc = new CodeContext(Code, Keyword);
            return cc.GetVisualBasicContext();
        }

        public List<string> GetJavaScriptContext()
        {
            string NormalString = "";
            while(CharPointer < CodeChars.Length)
            {
                char CurrentChar = CodeChars[CharPointer];
                switch(CurrentChar)
                {
                    case ('"'):
                        NormalString = new string(CurrentTokenChars.ToArray());
                        CheckAndAddContext(NormalString, Keyword, "NormalString");
                        CharPointer++;
                        ReadTill("\"", false, true);
                        string DoubleQuotedString = new string(CurrentTokenChars.ToArray());
                        if (DoubleQuotedString.EndsWith("\""))
                        {
                            CheckAndAddContext(DoubleQuotedString.TrimEnd('"'), Keyword, "DoubleQuotedString");
                        }
                        else
                        {
                            CheckAndAddContext(DoubleQuotedString, Keyword, "NormalString");
                        }
                        break;
                    case ('\''):
                        NormalString = new string(CurrentTokenChars.ToArray());
                        CheckAndAddContext(NormalString, Keyword, "NormalString");
                        CharPointer++;
                        ReadTill("'", false, true);
                        string SingleQuotedString = new string(CurrentTokenChars.ToArray());
                        if(SingleQuotedString.EndsWith("'"))
                        {
                            CheckAndAddContext(SingleQuotedString.TrimEnd('\''), Keyword, "SingleQuotedString");
                        }
                        else
                        {
                            CheckAndAddContext(SingleQuotedString, Keyword, "NormalString");
                        }
                        break;
                    case ('/'):
                        if (CharPointer < CodeChars.Length - 1)
                        {
                            if (CodeChars[CharPointer + 1] == '/')
                            {
                                NormalString = new string(CurrentTokenChars.ToArray());
                                CheckAndAddContext(NormalString, Keyword, "NormalString");
                                CharPointer++;
                                ReadTill(new List<char> { '\r', '\n' }, false, false);
                                string SingleLineComment = new string(CurrentTokenChars.ToArray());
                                CheckAndAddContext(SingleLineComment, Keyword, "SingleLineComment");
                            }
                            else if (CodeChars[CharPointer + 1] == '*')
                            {
                                NormalString = new string(CurrentTokenChars.ToArray());
                                CheckAndAddContext(NormalString, Keyword, "NormalString");
                                CharPointer++;
                                ReadTill("*/", true, false);
                                string MutliLineComment = (new string(CurrentTokenChars.ToArray())).Trim('/').Trim('*');
                                CheckAndAddContext(MutliLineComment, Keyword, "MutliLineComment");
                            }
                            else
                            {
                                CurrentTokenChars.Add(CurrentChar);
                            }
                        }
                        else
                        {
                            CurrentTokenChars.Add(CurrentChar);
                        }
                        break;
                    case ('\r'):
                    case ('\n'):
                        NormalString = (new string(CurrentTokenChars.ToArray())).Trim();
                        CurrentTokenChars.Clear();
                        CheckAndAddContext(NormalString, Keyword, "NormalString");
                        break;
                    default:
                        CurrentTokenChars.Add(CurrentChar);
                        break;
                }
                CharPointer++;
            }
            NormalString = new string(CurrentTokenChars.ToArray());
            CheckAndAddContext(NormalString, Keyword, "NormalString");
            return Contexts;
        }

        void CheckAndAddContext(string Value, string Keyword, string Context)
        {
            if (Value.Length == 0) return;
            CurrentTokenChars.Clear();
            if (Value.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
            {
                Contexts.Add(Context);
            }
        }

        public List<string> GetVisualBasicContext()
        {
            string NormalString = "";
            while (CharPointer < CodeChars.Length)
            {
                char CurrentChar = CodeChars[CharPointer];
                switch (CurrentChar)
                {
                    case ('"'):
                        NormalString = new string(CurrentTokenChars.ToArray());
                        CheckAndAddContext(NormalString, Keyword, "NormalString");
                        CharPointer++;
                        ReadTill("\"", false, true);
                        string DoubleQuotedString = new string(CurrentTokenChars.ToArray());
                        if (DoubleQuotedString.EndsWith("\""))
                        {
                            CheckAndAddContext(DoubleQuotedString.TrimEnd('"'), Keyword, "DoubleQuotedString");
                        }
                        else
                        {
                            CheckAndAddContext(DoubleQuotedString, Keyword, "NormalString");
                        }
                        break;
                    case ('\''):
                        NormalString = new string(CurrentTokenChars.ToArray());
                        CheckAndAddContext(NormalString, Keyword, "NormalString");
                        CharPointer++;
                        ReadTill(new List<char> { '\r', '\n' }, false, false);
                        string SingleLineComment = new string(CurrentTokenChars.ToArray());
                        CheckAndAddContext(SingleLineComment, Keyword, "SingleLineComment");
                        break;
                    case ('\r'):
                    case ('\n'):
                        NormalString = (new string(CurrentTokenChars.ToArray())).Trim();
                        CurrentTokenChars.Clear();
                        CheckAndAddContext(NormalString, Keyword, "NormalString");
                        break;
                    default:
                        CurrentTokenChars.Add(CurrentChar);
                        break;
                }
                CharPointer++;
            }
            NormalString = new string(CurrentTokenChars.ToArray());
            CheckAndAddContext(NormalString, Keyword, "NormalString");
            return Contexts;
        }

        void ReadTill(List<char> EndChars, bool IgnoreNewLines, bool CanEscapeNewLines)
        {
            if (CharPointer == CodeChars.Length) return;
            char[] EscapableChars;
            if (CanEscapeNewLines)
            {
                EscapableChars = new char[] { '\\', '\'', '"', '\r', '\n' };
            }
            else
            {
                EscapableChars = new char[] { '\\', '\'', '"' };
            }
            bool Read = true;
            while (Read)
            {
                char CurrentChar = CodeChars[CharPointer];
                CurrentTokenChars.Add(CurrentChar);
                if ((IsCharMatch(CurrentChar, '\r', EscapableChars) || IsCharMatch(CurrentChar, '\n', EscapableChars)) && !IgnoreNewLines)
                {
                    Read = false;
                }
                else
                {
                    foreach (char ec in EndChars)
                    {
                        if (IsCharMatch(ec, CurrentChar, EscapableChars))
                            Read = false;
                        else
                            Read = true;
                    }
                }
                CharPointer++;
                if (CharPointer >= CodeChars.Length) Read = false;
                if (!Read) CharPointer--;
            }
        }

        void ReadTill(string EndStr, bool IgnoreNewLines, bool CanEscapeNewLines)
        {
            if (CharPointer == CodeChars.Length) return;
            char[] EndChars = EndStr.ToCharArray();
            char[] EscapableChars;
            if (CanEscapeNewLines)
            {
                EscapableChars = new char[] { '\\', '\'', '"', '\r', '\n' };
            }
            else
            {
                EscapableChars = new char[] { '\\', '\'', '"' };
            }
            bool Read = true;
            while (Read)
            {
                char CurrentChar = CodeChars[CharPointer];
                CurrentTokenChars.Add(CurrentChar);
                if ((IsCharMatch(CurrentChar, '\r', EscapableChars) || IsCharMatch(CurrentChar, '\n', EscapableChars)) && !IgnoreNewLines)
                {
                    Read = false;
                }
                else
                {
                    if (IsCharMatch(EndChars[0], CurrentChar, EscapableChars))
                    {
                        if (CharPointer + EndStr.Length <= CodeChars.Length)
                        {
                            bool FullMatch = true;
                            for (int i = 0; i < EndChars.Length; i++)
                            {
                                if (EndChars[i] != CodeChars[CharPointer + i])
                                {
                                    FullMatch = false;
                                }
                            }
                            if (FullMatch) Read = false;
                        }
                    }
                    else
                    {
                        Read = true;
                    }
                }
                CharPointer++;
                if (CharPointer >= CodeChars.Length) Read = false;
                if (!Read) CharPointer--;
            }
        }

        bool IsCharMatch(char A, char B, char[] EscapableChars)
        {
            if (A == B)
            {
                if (CanBeEscaped(A, EscapableChars) && CharPointer > 0 && CodeChars[CharPointer - 1] == '\\')
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        bool CanBeEscaped(char C, char[] EscapableChars)
        {
            foreach (char EC in EscapableChars)
            {
                if (C == EC) return true;
            }
            return false;
        }

        public static List<string> ToChars()
        {
            string Code = "a/*a";
            char[] Chars = Code.ToCharArray();
            List<string> CharStr = new List<string>();
            foreach (char c in Chars)
            {
                CharStr.Add(c.ToString());
            }
            return CharStr;
        }
    }
}
