//
// Copyright 2011-2013 Lavakumar Kuppan
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
using System.Text.RegularExpressions;
using System.Threading;

namespace IronWASP
{
    public class Analyzer
    {
        //internal static Dictionary<string, Request> ProbeStrings = new Dictionary<string, Request>();
        internal static Dictionary<string, int> ProbeStrings = new Dictionary<string, int>();

        static int ProbeStringCounter = 0;

        public static List<string> GetProbeStrings()
        {
            List<string> AllProbeStrings = new List<string>(ProbeStrings.Keys);
            return AllProbeStrings;
        }

        public static Request GetProbeStringRequest(string ProbeString)
        {
            return Request.FromScanLog(ProbeStrings[ProbeString]);
        }

        public static int GetProbeCounter()
        {
            return Interlocked.Increment(ref ProbeStringCounter);
        }

        public static string GetProbeString()
        {
            int ID = GetProbeCounter();
            string[] Alphabets = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            Random R = new Random();
            string AlphaChar = Alphabets[R.Next(26)];
            int FirstPartSize = R.Next(1, 4);
            int LastPartSize = R.Next(4, 8);
            StringBuilder PS = new StringBuilder();
            for (int i = 0; i <= FirstPartSize; i++)
            {
                PS.Append(AlphaChar);
            }
            PS.Append(ID.ToString());
            for (int i = 0; i <= LastPartSize; i++)
            {
                PS.Append(AlphaChar);
            }
            return PS.ToString();
        }

        public static void AddProbeString(string ProbeString, int LogId)
        {
            lock (ProbeStrings)
            {
                ProbeStrings.Add(ProbeString, LogId);
            }
        }

        internal static Reflections GetAllReflections(Session IrSe)
        {
            Reflections AllReflections = new Reflections();

            if (IrSe == null) return AllReflections;
            if (IrSe.Request == null) return AllReflections;
            if (IrSe.Response == null) return AllReflections;
            int TotalReflections = 0;

            List<Reflection> UrlReflections = new List<Reflection>();
            List<Reflection> UrlPathPartReflections = new List<Reflection>();
            List<Reflection> QueryReflections = new List<Reflection>();
            List<Reflection> BodyReflections = new List<Reflection>();
            List<Reflection> CookieReflections = new List<Reflection>();
            List<Reflection> HeaderReflections = new List<Reflection>();

            string ResString = IrSe.Response.ToString();

            //Check if the URL is being reflected back
            if (IrSe.Request.Url.Length > 1)
            {
                Reflection RefResult = GetReflections(IrSe.Request.Url, ResString);
                if (RefResult.Count > 0)
                {
                    RefResult.Name = "URL";
                    RefResult.Value = IrSe.Request.Url;
                    RefResult.Section = "URL";
                    UrlReflections.Add(RefResult);
                }
            }

            //check if any URL path parts are being reflected. To be checked only when Querystring and File extension are absent (to handle URL rewriting)

            if ((IrSe.Request.Query.Count == 0) && (IrSe.Request.File.Length == 0) && IrSe.Request.UrlPathParts.Count > 0)
            {
                int PathCount = 0;
                foreach (string UrlPathPart in IrSe.Request.UrlPathParts)
                {
                    Reflection RefResult = GetReflections(UrlPathPart, ResString);
                    if (RefResult.Count > 0)
                    {
                        RefResult.Name = "UrlPathPart : " + PathCount.ToString();
                        RefResult.Value = UrlPathPart;
                        RefResult.Section = "UrlPathPart";
                        UrlPathPartReflections.Add(RefResult);
                    }
                    PathCount++;
                }
            }

            //check if any Query parameters are being reflected
            foreach (string Name in IrSe.Request.Query.GetNames())
            {
                List<string> SubParametervalues = IrSe.Request.Query.GetAll(Name);
                List<string> ParameterResults = new List<string>();
                foreach (string Value in SubParametervalues)
                {
                    Reflection RefResult = GetReflections(Value, ResString);
                    if (RefResult.Count > 0)
                    {
                        RefResult.Name = Name;
                        RefResult.Value = Value;
                        RefResult.Section = "Query";
                        QueryReflections.Add(RefResult);
                    }
                }
            }

            //check if any Body parameters are being reflected
            foreach (string Name in IrSe.Request.Body.GetNames())
            {
                List<string> SubParametervalues = IrSe.Request.Body.GetAll(Name);
                List<string> ParameterResults = new List<string>();
                foreach (string Value in SubParametervalues)
                {
                    Reflection RefResult = GetReflections(Value, ResString);
                    if (RefResult.Count > 0)
                    {
                        RefResult.Name = Name;
                        RefResult.Value = Value;
                        RefResult.Section = "Body";
                        BodyReflections.Add(RefResult);
                    }
                }
            }

            //check if any Cookie parameters are being reflected
            foreach (string Name in IrSe.Request.Cookie.GetNames())
            {
                List<string> SubParametervalues = IrSe.Request.Cookie.GetAll(Name);
                List<string> ParameterResults = new List<string>();
                foreach (string Value in SubParametervalues)
                {
                    Reflection RefResult = GetReflections(Value, ResString);
                    if (RefResult.Count > 0)
                    {
                        RefResult.Name = Name;
                        RefResult.Value = Value;
                        RefResult.Section = "Cookie";
                        CookieReflections.Add(RefResult);
                    }
                }
            }

            //check if any Header parameters are being reflected
            foreach (string Name in IrSe.Request.Headers.GetNames())
            {
                List<string> SubParametervalues = IrSe.Request.Headers.GetAll(Name);
                List<string> ParameterResults = new List<string>();
                foreach (string Value in SubParametervalues)
                {
                    Reflection RefResult = GetReflections(Value, ResString);
                    if (RefResult.Count > 0)
                    {
                        RefResult.Name = Name;
                        RefResult.Value = Value;
                        RefResult.Section = "Header";
                        HeaderReflections.Add(RefResult);
                    }
                }
            }

            TotalReflections = UrlReflections.Count + UrlPathPartReflections.Count + QueryReflections.Count + BodyReflections.Count + CookieReflections.Count + HeaderReflections.Count;

            AllReflections.Url = UrlReflections;
            AllReflections.UrlPathPart = UrlPathPartReflections;
            AllReflections.Query = QueryReflections;
            AllReflections.Body = BodyReflections;
            AllReflections.Cookie = CookieReflections;
            AllReflections.Header = HeaderReflections;

            return AllReflections;
        }

        public static string CheckReflections(Session IrSe)
        {
            Reflections AllReflections = GetAllReflections(IrSe);
            return CheckReflections(AllReflections);
        }

        public static string CheckReflections(Reflections AllReflections)
        {
            StringBuilder Result = new StringBuilder();
            if (AllReflections.Count == 0) return Result.ToString();

            int TotalReflections = 0;

            List<Reflection> UrlReflections = AllReflections.Url;
            List<Reflection> UrlPathPartReflections = AllReflections.UrlPathPart;
            List<Reflection> QueryReflections = AllReflections.Query;
            List<Reflection> BodyReflections = AllReflections.Body;
            List<Reflection> CookieReflections = AllReflections.Cookie;
            List<Reflection> HeaderReflections = AllReflections.Header;


            TotalReflections = UrlReflections.Count + UrlPathPartReflections.Count + QueryReflections.Count + BodyReflections.Count + CookieReflections.Count + HeaderReflections.Count;

            Result.Append("<i<hh>>Total Reflections: "); Result.Append(TotalReflections.ToString()); Result.Append("<i</hh>> | ");

            if (UrlReflections.Count > 0)
            {
                Result.Append("<i<hh>> URL : "); Result.Append(UrlReflections.Count.ToString()); Result.Append("<i</hh>> | ");
            }
            if (UrlPathPartReflections.Count > 0)
            {
                Result.Append("<i<hh>> URL Path : "); Result.Append(UrlPathPartReflections.Count.ToString()); Result.Append("<i</hh>> | ");
            }
            if (QueryReflections.Count > 0)
            {
                Result.Append("<i<hh>> Query : "); Result.Append(QueryReflections.Count.ToString()); Result.Append("<i</hh>> | ");
            }
            if (BodyReflections.Count > 0)
            {
                Result.Append("<i<hh>> Body : "); Result.Append(BodyReflections.Count.ToString()); Result.Append("<i</hh>> | ");
            }
            if (CookieReflections.Count > 0)
            {
                Result.Append("<i<hh>> Cookie : "); Result.Append(CookieReflections.Count.ToString()); Result.Append("<i</hh>> | ");
            }
            if (HeaderReflections.Count > 0)
            {
                Result.Append("<i<hh>> Headers : "); Result.Append(HeaderReflections.Count.ToString()); Result.Append("<i</hh>> | ");
            }

            Result.Append("<i<br>>");

            Dictionary<int, List<Reflection>> OrderedReflections = new Dictionary<int, List<Reflection>>();

            foreach (List<Reflection> ReflectionList in AllReflections.GetList())
            {
                foreach (Reflection Refl in ReflectionList)
                {
                    if (OrderedReflections.ContainsKey(Refl.Length))
                        OrderedReflections[Refl.Length].Add(Refl);
                    else
                        OrderedReflections.Add(Refl.Length, new List<Reflection>() { Refl });
                }
            }

            List<int> LengthOrder = new List<int>(OrderedReflections.Keys);
            LengthOrder.Sort();
            for (int i = LengthOrder.Count - 1; i >= 0; i--)
            {
                int Length = LengthOrder[i];
                foreach (Reflection Refl in OrderedReflections[Length])
                {
                    Result.Append("<i<br>><i<br>>");
                    Result.Append("<i<h>>Section:<i</h>> "); Result.Append(Refl.Section); Result.Append(" | <i<h>>Parameter:<i</h>> "); Result.Append(Refl.Name); Result.Append(" | <i<h>>Count:<i</h>> "); Result.Append(Refl.Count.ToString()); Result.Append(" | <i<h>>Value:<i</h>><i<hlo>> "); Result.Append(Refl.Value); Result.Append("<i</hlo>>");
                    foreach (string R in Refl.GetReflections())
                    {
                        Result.Append("<i<br>>    "); Result.Append(R);
                    }
                }
            }
            return Result.ToString();
        }

        //public static Reflection GetReflections(string Input, Response Res)
        //{
        //    List<string> Results = new List<string>();
        //    string ResString = Res.ToString();
        //    return GetReflections(Input, ResString);
        //}

        public static Reflection GetReflections(string Input, string ResString)
        {
            List<string> Variations = new List<string>();
            Variations.Add(Input);
            Variations.Add(Input.ToLower());
            Variations.Add(Input.ToUpper());
            Variations.Add(Tools.UrlEncode(Input));
            Variations.Add(Tools.UrlPathEncode(Input));
            Variations.Add(Tools.HtmlEncode(Input));
            Variations.Add(Tools.XmlEncode(Input));
            Variations.Add(Tools.JsonEncode(Input));
            Variations.Add(Tools.RelaxedUrlEncode(Input));
            Variations.Add(Tools.UrlUnicodeEncode(Input));
            Variations.Add(Input.Replace("\"", "\\\""));
            Variations.Add(Input.Replace("'", "\\\'"));

            Dictionary<string, int> TempDict = new Dictionary<string, int>();
            foreach (string V in Variations)
            {
                TempDict[V] = 0;
            }
            Variations = new List<string>(TempDict.Keys);


            Reflection Result = new Reflection("", Input, "");
            foreach (string V in Variations)
            {
                Reflection Ref = GetReflectionsFor(V, ResString);
                if (Ref.Count > 0)
                {
                    foreach (string RefStr in Ref.GetReflections())
                    {
                        Result.Add(RefStr.Replace(V, string.Format("<i<hlo>>{0}<i</hlo>>", V)));
                    }
                }
            }
            return Result;
        }

        public static Reflection GetReflectionsFor(string Input, string ResString)
        {
            Reflection Results = new Reflection("", Input, "");
            if (Input.Length == 0 || !ResString.Contains(Input)) return Results;

            //string Pattern = String.Format(@"\W{0}\W", Input.Replace("\\", "\\\\").Replace(".", "\\.").Replace("$", "\\$").Replace("^", "\\^").Replace("*", "\\*").Replace("|", "\\|").Replace("+", "\\+").Replace("?", "\\?").Replace("{", "\\{").Replace("}", "\\}").Replace("[", "\\[").Replace("]", "\\]").Replace("(", "\\(").Replace(")", "\\)"));
            string Pattern = String.Format(@"\W{0}\W", Regex.Escape(Input));

            MatchCollection MatchResults = Regex.Matches(ResString, Pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            foreach (Match M in MatchResults)
            {
                if (M.Success)
                {
                    int SubStringStart = M.Index - 20;
                    int SubStringLength = 0;
                    if (SubStringStart < 0)
                    {
                        SubStringStart = 0;
                        SubStringLength = SubStringStart + M.Length + 20;
                    }
                    else
                    {
                        SubStringLength = M.Length + 40;
                    }

                    if (SubStringStart + SubStringLength >= ResString.Length)
                        Results.Add(ResString.Substring(SubStringStart));
                    else
                        Results.Add(ResString.Substring(SubStringStart, SubStringLength));
                }
            }
            return Results;
        }
    }
}
