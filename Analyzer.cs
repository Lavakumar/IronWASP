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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
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
        internal static Dictionary<string, Request> ProbeStrings = new Dictionary<string, Request>();
        
        static int ProbeStringCounter = 0;

        public static List<string> GetProbeStrings()
        {
            List<string> AllProbeStrings = new List<string>(ProbeStrings.Keys);
            return AllProbeStrings;
        }

        public static Request GetProbeStringRequest(string ProbeString)
        {
            return ProbeStrings[ProbeString];
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
            int FirstPartSize = R.Next(1,4);
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

        public static void AddProbeString(string ProbeString, Request InjectedRequest)
        {
            Request ClonedReq = InjectedRequest.GetClone();
            lock (ProbeStrings)
            {
                ProbeStrings.Add(ProbeString, ClonedReq);
            }
        }

        internal static string CheckReflections(Session IrSe)
        {
            StringBuilder Result = new StringBuilder();
            if (IrSe == null) return Result.ToString();
            if (IrSe.Request == null) return Result.ToString();
            if (IrSe.Response == null) return Result.ToString();
            int TotalReflections = 0;

            List<Reflection> UrlReflections = new List<Reflection>();
            List<Reflection> UrlPathPartReflections = new List<Reflection>();
            List<Reflection> QueryReflections = new List<Reflection>();
            List<Reflection> BodyReflections = new List<Reflection>();
            List<Reflection> CookieReflections = new List<Reflection>();
            List<Reflection> HeaderReflections = new List<Reflection>();

            //Check if the URL is being reflected back
            if (IrSe.Request.Url.Length > 1)
            {
                Reflection RefResult = GetReflections(IrSe.Request.Url, IrSe.Response);
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
                    Reflection RefResult = GetReflections(UrlPathPart, IrSe.Response);
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
                    Reflection RefResult = GetReflections(Value, IrSe.Response);
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
                    Reflection RefResult = GetReflections(Value, IrSe.Response);
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
                    Reflection RefResult = GetReflections(Value, IrSe.Response);
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
                    Reflection RefResult = GetReflections(Value, IrSe.Response);
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
            List<List<Reflection>> AllReflections = new List<List<Reflection>>() { UrlReflections, UrlPathPartReflections, QueryReflections, BodyReflections, CookieReflections, HeaderReflections };

            foreach (List<Reflection> ReflectionList in AllReflections)
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
                        Result.Append("<i<br>>    "); Result.Append(R.Replace(Refl.Value, "<i<hlo>>" + Refl.Value + "<i</hlo>>"));
                    }
                }
            }
            return Result.ToString();
        }

        public static Reflection GetReflections(string Input, Response Res)
        {
            List<string> Results = new List<string>();
            string ResString = Res.ToString();
            return GetReflections(Input, ResString);
        }

        public static Reflection GetReflections(string Input, string ResString)
        {
            Reflection Results = new Reflection("", Input, "");
            if (Input.Length == 0 || !ResString.Contains(Input)) return Results;

            string Pattern = String.Format(@"\W{0}\W", Input.Replace("\\", "\\\\").Replace(".", "\\.").Replace("$", "\\$").Replace("^", "\\^").Replace("*", "\\*").Replace("|", "\\|").Replace("+", "\\+").Replace("?", "\\?").Replace("{", "\\{").Replace("}", "\\}").Replace("[", "\\[").Replace("]", "\\]").Replace("(", "\\(").Replace(")", "\\)"));

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
