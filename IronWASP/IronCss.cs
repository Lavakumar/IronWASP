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
    public class IronCss
    {

        public static CssFx.CssStyleSheet Parse(string Css)
        {
            try
            {
                CssFx.CssParser Parser = new CssFx.CssParser("", Css);
                Parser.ParseStyleSheet();
                CssFx.CssStyleSheet ParsedCss = Parser.StyleSheet;
                return ParsedCss;
            }
            catch
            {
                return null;
            }
        }


        public static List<string> GetContext(string Css, string Keyword)
        {
            List<string> Contexts = new List<string>();
            CssFx.CssParser Parser = new CssFx.CssParser("", Css);
            Parser.ParseStyleSheet();
            CssFx.CssStyleSheet ParsedCss = Parser.StyleSheet;
            foreach(CssFx.CssStatement Stmt in ParsedCss.Statements)
            {
                switch (Stmt.GetType().Name)
                {
                    case("CssRuleSet"):
                        Contexts.AddRange(GetContext((CssFx.CssRuleSet)Stmt, Keyword));
                        break;
                    case("CssAtRule"):
                        Contexts.AddRange(GetContext((CssFx.CssAtRule)Stmt, Keyword));
                        break;
                }
            }
            foreach (string Comment in ParsedCss.Comments)
            {
                if (Comment.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    Contexts.Add("Comment");
                }
            }
            return Contexts;
        }

        static List<string> GetContext(CssFx.CssRuleSet Stmt, string Keyword)
        {
            List<string> Contexts = new List<string>();
            foreach (CssFx.CssSelector Selector in Stmt.Selectors)
            {
                if (Selector.Value.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    string SelectorValue = Selector.Value.Trim();
                    Contexts.AddRange(GetSelectorContext(SelectorValue, Keyword));
                }
            }
            foreach (CssFx.CssDeclaration Declaration in Stmt.Declarations)
            {
                Contexts.AddRange(GetDeclarationContext(Declaration, Keyword));
            }
            
            //Contexts.Add("RuleSet");
            return Contexts;
        }

        static List<string> GetContext(CssFx.CssAtRule Stmt, string Keyword)
        {
            List<string> Contexts = new List<string>();
            bool Import = false;
            if(Stmt.Ident.Equals("import", StringComparison.OrdinalIgnoreCase))
            {
                Import = true;
            }
            if (Stmt.Ident.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
            {  
                Contexts.Add(string.Format("Ident-Ident-{0}", GetPosition(Stmt.Ident, Keyword)));
            }
            if (Stmt.Value != null)
            {
                if (Stmt.Value.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    if (Import)
                    {
                        Contexts.AddRange(GetImportIdentContext(Stmt.Value, Keyword));
                    }
                    else if (Stmt.Ident.Equals("media", StringComparison.OrdinalIgnoreCase))
                    {
                        Contexts.Add(string.Format("Ident-MediaValue-{0}", GetPosition(Stmt.Ident, Keyword)));
                    }
                }
            }
            foreach (CssFx.ICssValue Value in Stmt.Block.Values)
            {
                try
                {
                    CssFx.CssDeclaration Declaration = (CssFx.CssDeclaration)Value;
                    Contexts.AddRange(GetDeclarationContext(Declaration, Keyword));
                }catch{}
            }
            return Contexts;
        }

        static List<string> GetDeclarationContext(CssFx.CssDeclaration Declaration, string Keyword)
        {
            List<string> Contexts = new List<string>();
            if (Declaration.Property.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
            {
                Contexts.Add(string.Format("Property-{0}", GetPosition(Declaration.Property, Keyword)));
            }
            foreach (CssFx.CssString Value in Declaration.Value.Values)
            {
                if (Value.Value.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    Contexts.AddRange(GetValueContext(Value.Value, Keyword));
                    //Contexts.Add("Value");
                }
            }
            return Contexts;
        }

        static List<string> GetImportIdentContext(string IdentValue, string Keyword)
        {
            List<string> Contexts = new List<string>();
            IdentValue = IdentValue.ToLower().Trim();
            Keyword = Keyword.ToLower().Trim();

            if (IdentValue.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
            {
                if (IdentValue.StartsWith("url(", StringComparison.OrdinalIgnoreCase) && IdentValue.EndsWith(")"))
                {
                    string UrlValue = IdentValue.Substring(4, IdentValue.Length - 5).Trim();
                    string QuoteType = GetQuoteType(UrlValue);
                    UrlValue = TrimQuote(UrlValue, QuoteType);
                    Contexts.Add(string.Format("Import-Url-{0}-{1}", GetPosition(UrlValue, Keyword), QuoteType));

                    if (UrlValue.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                    {
                        string JSValue = UrlValue.Substring(11);
                        if (JSValue.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            Contexts.Add(string.Format("Import-UrlJS-{0}-{1}", GetPosition(JSValue, Keyword), QuoteType));
                        }
                    }
                }
                else
                {
                    string QuoteType = GetQuoteType(IdentValue);
                    string UrlValue = TrimQuote(IdentValue, QuoteType);
                    Contexts.Add(string.Format("Import-Raw-{0}-{1}", GetPosition(UrlValue, Keyword), QuoteType));
                    
                    if (UrlValue.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                    {
                        string JSValue = UrlValue.Substring(11);
                        if (JSValue.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            Contexts.Add(string.Format("Import-UrlJS-{0}-{1}", GetPosition(JSValue, Keyword), QuoteType));
                        }
                    }
                }
            }
            return Contexts;
        }

        static List<string> GetSelectorContext(string Value, string Keyword)
        {
            List<string> Contexts = new List<string>();
            string Token = "";

            Value = Value.Trim();

            int SquareStart = Value.IndexOf('[');
            //if (SquareStart > 0 && Value[SquareStart - 1] == '\\') SquareStart = -1;
            int RoundStart = Value.IndexOf('(');
            //if (RoundStart > 0 && Value[RoundStart - 1] == '\\') RoundStart = -1;
            //int SingleQuoteStart = Value.IndexOf('\'');
            //if (SingleQuoteStart > 0 && Value[SingleQuoteStart - 1] == '\\') SingleQuoteStart = -1;
            //int DoubleQuoteStart = Value.IndexOf('"');
            //if (DoubleQuoteStart > 0 && Value[DoubleQuoteStart - 1] == '\\') DoubleQuoteStart = -1;

            string Prefix = "";

            if (SquareStart == -1 && RoundStart == -1)
            {
                Prefix = Value;
            }
            else if (SquareStart > -1 && ((RoundStart > SquareStart) || (RoundStart == -1)))
            {
                Prefix = Value.Substring(0, SquareStart);                
                Token =  Value.Substring(SquareStart).TrimStart('[').TrimEnd(']');
                if (Token.Length > 0)
                {
                    if (Token.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        Contexts.AddRange(GetSquareBracketSelectorContext(Token, Keyword));
                    }
                }
            }
            else if (RoundStart > -1 && ((SquareStart > RoundStart) || (SquareStart == -1)))
            {
                Prefix = Value.Substring(0, RoundStart);
                Token = Value.Substring(RoundStart).TrimStart('(').TrimEnd(')');
                if (Token.Length > 0)
                {
                    if(Token.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        Contexts.Add(string.Format("Selector-Round-{0}", GetPosition(Token, Keyword)));
                    }
                }
            }

            if (Prefix.Length > 0)
            {
                if (Prefix.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    Contexts.Add(string.Format("Selector-Normal-{0}", GetPosition(Prefix, Keyword)));
                }
            }

            return Contexts;
        }

        static List<string> GetSquareBracketSelectorContext(string Value, string Keyword)
        {
            List<string> Contexts = new List<string>();
            string[] KV = Value.Split(new char[] { '=' }, 2);
            if (KV.Length > 0)
            {
                if (KV[0].IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    Contexts.Add(string.Format("Selector-SquareKey-{0}", GetPosition(KV[0], Keyword)));
                }
            }
            if(KV.Length == 2)
            {
                if (KV[1].IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    string QuoteType = GetQuoteType(KV[1]);
                    Contexts.Add(string.Format("Selector-SquareValue-{0}-{1}", GetPosition(TrimQuote(KV[1], QuoteType), Keyword), QuoteType));
                }
            }
            return Contexts;
        }

        static List<string> GetValueContext(string Value, string Keyword)
        {
            List<string> Contexts = new List<string>();
            List<string> Values = new List<string>();
            Value = Value.Trim();
            string Quote = "";
            bool InQuote = false;
            int LastEscacpe = 0;
            List<char> TokenChars = new List<char>();
            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i] == '\\')
                {
                    TokenChars.Add(Value[i]);
                    LastEscacpe = i;
                }
                else if (Value[i] == '\'' || Value[i] == '"')
                {
                    string CurrentChar = Value[i].ToString();
                    //CheckQuoteStatus(string CurrentChar, bool InQuote, string CurrentQuote, bool IsEscaped);
                    string QuoteStatus = CheckQuoteStatus(CurrentChar, InQuote, Quote, (LastEscacpe == i - 1));
                    switch (QuoteStatus)
                    {
                        case ("Start"):
                            InQuote = true;
                            //string NormalString = new string(TokenChars.ToArray());
                            //NormalString = NormalString.Trim().Trim(',').Trim();
                            //Values.Add(NormalString);
                            //TokenChars.Clear();
                            Quote = CurrentChar;
                            TokenChars.Add(Value[i]);
                            break;
                        case ("End"):
                            InQuote = false;
                            TokenChars.Add(Value[i]);
                            //string QuotedString = new string(TokenChars.ToArray());
                            //QuotedString = QuotedString.Trim().Trim(',').Trim();
                            //Values.Add(QuotedString);
                            //TokenChars.Clear();
                            Quote = "";
                            break;
                        case ("None"):
                            TokenChars.Add(Value[i]);
                            break;
                    }
                }
                else if (Value[i] == ' ' || Value[i] == ',')
                {
                    if (InQuote)
                    {
                        TokenChars.Add(Value[i]);
                    }
                    else
                    {
                        string NormalString = new string(TokenChars.ToArray());
                        NormalString = NormalString.Trim().Trim(',').Trim();
                        Values.Add(NormalString);
                        TokenChars.Clear();
                    }
                }
                else
                {
                    TokenChars.Add(Value[i]);
                }
            }
            string LastNormalString = new string(TokenChars.ToArray());
            Values.Add(LastNormalString);
            for (int i = 0; i < Values.Count; i++)
            {
                string QuoteType = "";
                if (Values[i].StartsWith("url(", StringComparison.OrdinalIgnoreCase) && Values[i].EndsWith(")"))
                {
                    string UrlValue =  Values[i].Substring(4).TrimEnd(')');
                    if (UrlValue.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        QuoteType = GetQuoteType(UrlValue);
                        UrlValue = TrimQuote(UrlValue, QuoteType);
                        Contexts.Add(string.Format("Value-Url-{0}-{1}", GetPosition(UrlValue, Keyword), QuoteType));
                    }

                    if (UrlValue.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                    {
                        string JSValue = UrlValue.Substring(11);
                        if (JSValue.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            Contexts.Add(string.Format("Value-JS-{0}-{1}", GetPosition(JSValue, Keyword), QuoteType));
                        }
                    }
                }
                else if (Values[i].StartsWith("expression(", StringComparison.OrdinalIgnoreCase) && Values[i].EndsWith(")"))
                {
                    string JSValue = Values[i].Substring(11).TrimEnd(')');
                    if (JSValue.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        QuoteType = GetQuoteType(JSValue);
                        Contexts.Add(string.Format("Value-JS-{0}-{1}", GetPosition(TrimQuote(JSValue, QuoteType), Keyword), QuoteType));
                    }
                }
                else
                {
                    if (Values[i].IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        QuoteType = GetQuoteType(Values[i]);
                        Contexts.Add(string.Format("Value-Normal-{0}-{1}", GetPosition(TrimQuote(Values[i], QuoteType), Keyword), QuoteType));
                        if (Values.Count == 1)
                        {
                            Contexts.Add(string.Format("Value-OnlyNormal-{0}-{1}", GetPosition(TrimQuote(Values[i], QuoteType), Keyword), QuoteType));
                        }
                    }
                }
            }
            return Contexts;
        }

        static string CheckQuoteStatus(string CurrentChar, bool InQuote, string CurrentQuote, bool IsEscaped)
        {
            if (IsEscaped) return "None";
            if (InQuote)
            {
                if (CurrentChar == CurrentQuote)
                    return "End";
                else
                    return "None";
            }
            else
            {
                return "Start";
            }
        }

        static string GetQuoteType(string Value)
        {
            Value = Value.Trim();
            string Quote = "";
            if (Value.StartsWith("\""))
            {
                Quote = "Double";
                
            }
            else if (Value.StartsWith("'"))
            {
                Quote = "Single";
            }
            else
            {
                Quote = "None";
            }
            return Quote;
        }

        static string TrimQuote(string Value, string Quote)
        {
            if (Quote.Equals("Double"))
            {
                Value = Value.Trim().Trim('"').Trim();
            }
            else if (Quote.Equals("Single"))
            {
                Value = Value.Trim().Trim('\'').Trim();
            }
            return Value.Trim();
        }

        static string GetPosition(string Value, string Keyword)
        {
            string Position = "";
            if (Value.Equals(Keyword, StringComparison.OrdinalIgnoreCase))
            {
                Position = "Full";
            }
            else if (Value.StartsWith(Keyword, StringComparison.OrdinalIgnoreCase))
            {
                Position = "Start";
            }
            else if (Value.EndsWith(Keyword, StringComparison.OrdinalIgnoreCase))
            {
                Position = "End";
            }
            else if (Value.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) > -1)
            {
                Position = "In";
            }
            return Position;
        }
    }
}
