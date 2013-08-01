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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace IronWASP
{
    internal class BehaviourAnalysis
    {

        internal Dictionary<string, Regex> Keywords = new Dictionary<string, Regex> {};// { "error", null }, { "exception", null }, { "not allowed", null }, { "unauthorized", null }, { "blocked", null }, { "filtered", null }, { "attack", null } };

        internal int RoundtripMinInterestingChange = 1000;
        internal int RoundtripMinInterestingChangeFactor = 10;

        internal int BodyDiffMinInterestingInsertedChars = 20;

        List<string> Payloads = new List<string>();

        List<Session> Logs = new List<Session>();

        List<int> RoundtripTimes = new List<int>();

        internal string BaseLinePayload = "";
        internal int BaseLineLogId = 0;
        internal int BaseLineRoundtripTime = 0;
        internal Session BaseLineSession = null;

        internal List<BehaviourAnalysisResult> Results = new List<BehaviourAnalysisResult>();
        internal string ResultsXml = "";

        internal BehaviourAnalysis(List<string> Keywords, int RoundtripChange, int RoundtripChangeFactor, int InsertedCharsCount)
        {
            foreach (string Keyword in Keywords)
            {
                this.Keywords[Keyword] = null;
            }
            this.RoundtripMinInterestingChange = RoundtripChange;
            this.RoundtripMinInterestingChangeFactor = RoundtripChangeFactor;
            this.BodyDiffMinInterestingInsertedChars = InsertedCharsCount;
        }

        internal void Analyze(string TraceOverviewMessage, string ScannedSection)
        {
            List<Dictionary<string, string>> OverviewEntries = IronTrace.GetOverviewEntriesFromXml(TraceOverviewMessage);
            List<string> Payloads = new List<string>();
            List<int> RoundTrips = new List<int>();
            List<int> LogIds = new List<int>();
            foreach (Dictionary<string, string> Entry in OverviewEntries)
            {
                try
                {
                    int LogId = Int32.Parse(Entry["log_id"]);
                    int Time = Int32.Parse(Entry["time"]);
                    Payloads.Add(Entry["payload"]);
                    LogIds.Add(LogId);
                    RoundTrips.Add(Time);
                }
                catch { }
            }
            Analyze(Payloads, LogIds, RoundTrips, ScannedSection);
        }

        internal void Analyze(List<string> Payloads, List<int> LogIds, List<int> RoundtripTimes, string ScannedSection)
        {
            if (Payloads.Count == 0 || LogIds.Count == 0 || Payloads.Count != LogIds.Count) return;

            CompileKeywordsRegex();

            this.BaseLinePayload = Payloads[0];
            this.BaseLineRoundtripTime = RoundtripTimes[0];
            try
            {
                this.BaseLineSession = Session.FromScanLog(LogIds[0]);
                this.BaseLineLogId = this.BaseLineSession.LogId;
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to Load from Scan Log", Exp);
            }

            this.Payloads.Clear();
            this.Logs.Clear();
            
            for (int i = 1; i < Payloads.Count; i++)
            {
                this.Payloads.Add(Payloads[i]);
                this.RoundtripTimes.Add(RoundtripTimes[i]);
                try
                {
                    this.Logs.Add(Session.FromScanLog(LogIds[i]));
                }
                catch(Exception Exp)
                {
                    IronException.Report("Unable to Load from Scan Log", Exp);
                }

                AnalyzePayloadBehaviour(i - 1);
            }

            this.ResultsXml = BehaviourAnalysisResult.ToXml(this.Results);

            DoOverallComparitiveAnalysis();
        }

        void CompileKeywordsRegex()
        {
            List<string> KeywordKeys = new List<string>(Keywords.Keys);
            foreach (string Keyword in KeywordKeys)
            {
                this.Keywords[Keyword] = new Regex(string.Format(@"\b{0}\b", Keyword), RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
        }

        void DoOverallComparitiveAnalysis()
        {
            //nothing to do here for now
        }

        void AnalyzePayloadBehaviour(int Index)
        {
            BehaviourAnalysisResult Result = new BehaviourAnalysisResult();
            Result.LogId = this.Logs[Index].LogId;
            Result.Payload = this.Payloads[Index];
            this.Results.Add(Result);

            AnalyzeResponseCode(Index, Result);
            AnalyzeRoundtripTime(Index, Result);
            AnalyzeSetCookieHeaders(Index, Result);
            AnalyzeResponseHeaders(Index, Result);
            AnalyzeResponseContent(Index, Result);
        }

        void AnalyzeResponseCode(int Index, BehaviourAnalysisResult Result)
        {
            if (this.Logs[Index].Response.Code != this.BaseLineSession.Response.Code)
            {
                Result.ResponseCodeResult = this.Logs[Index].Response.Code;
            }
        }

        void AnalyzeRoundtripTime(int Index, BehaviourAnalysisResult Result)
        {
            int CurrentRoundTrip = this.RoundtripTimes[Index];
            if (CurrentRoundTrip == 0) CurrentRoundTrip = 1;//this number to be used when multiplying so that the result is not 0
            if ((this.RoundtripTimes[Index] > (this.BaseLineRoundtripTime + RoundtripMinInterestingChange)) || (this.RoundtripTimes[Index] > (this.BaseLineRoundtripTime * RoundtripMinInterestingChangeFactor)))
            {
                Result.RoundtripTimeResult = string.Format("+{0} ms", this.RoundtripTimes[Index] - this.BaseLineRoundtripTime);
            }
            else if ((this.BaseLineRoundtripTime > (this.RoundtripTimes[Index] + RoundtripMinInterestingChange)) || (this.BaseLineRoundtripTime > (CurrentRoundTrip * RoundtripMinInterestingChangeFactor)))
            {
                Result.RoundtripTimeResult = string.Format("{0} ms", this.RoundtripTimes[Index] - this.BaseLineRoundtripTime);
            }
        }

        void AnalyzeResponseContent(int Index, BehaviourAnalysisResult Result)
        {
            DiffPlex.DiffBuilder.Model.SideBySideDiffModel DiffResult = IronDiffer.GetDiff(this.BaseLineSession.Response.BodyString, this.Logs[Index].Response.BodyString);
            int DiffLevel = IronDiffer.GetLevel(DiffResult, this.BaseLineSession.Response.BodyString, this.Logs[Index].Response.BodyString);

            List<string> InsertedStrings = IronDiffer.GetInsertedStrings(DiffResult);
            List<string> DeletedStrings = IronDiffer.GetDeletedStrings(DiffResult);
            

            List<string> KeywordsFound = new List<string>();
            foreach (string Keyword in this.Keywords.Keys)
            {
                if (this.Logs[Index].Response.BodyString.IndexOf(Keyword, 0, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    KeywordsFound.Add(Keyword);
                }
            }

            int NoOfInsertedChars = 0;
            foreach (string InsertedString in InsertedStrings)
            {
                NoOfInsertedChars += InsertedString.Length;
                if (KeywordsFound.Count > 0)
                {
                    foreach (string Keyword in KeywordsFound)
                    {
                        if (this.Keywords[Keyword].IsMatch(InsertedString))
                        {
                            if (!Result.ResponseKeywordsResult.Contains(Keyword))
                            {
                                Result.ResponseKeywordsResult.Add(Keyword);
                            }
                        }
                    }
                }
            }
            if (NoOfInsertedChars > BodyDiffMinInterestingInsertedChars)
            {
                Result.ResponseContentResult = NoOfInsertedChars;
            }
        }

        void AnalyzeSetCookieHeaders(int Index, BehaviourAnalysisResult Result)
        {
            foreach (SetCookie BSC in this.BaseLineSession.Response.SetCookies)
            {
                bool Found = false;
                foreach (SetCookie CSC in this.Logs[Index].Response.SetCookies)
                {
                    if (BSC.Name.Equals(CSC.Name))
                    {
                        Found = true;
                        if (BSC.Value.Length > 0 && CSC.Value.Length == 0)
                        {
                            Result.SetCookieHeaderResult.Add(string.Format("<{0}", BSC.Name));//value deleted
                        }
                    }
                }
                if (!Found)
                {
                    Result.SetCookieHeaderResult.Add(string.Format("-{0}", BSC.Name));//cookie deleted
                }
            }


            foreach (SetCookie CSC in this.Logs[Index].Response.SetCookies)
            {
                bool Found = false;
                foreach (SetCookie BSC in this.BaseLineSession.Response.SetCookies)
                {
                    if (BSC.Name.Equals(CSC.Name))
                    {
                        Found = true;
                        if (CSC.Value.Length > 0 && BSC.Value.Length == 0)
                        {
                            Result.SetCookieHeaderResult.Add(string.Format(">{0}", BSC.Name));//value added
                        }
                    }
                }
                if (!Found)
                {
                    Result.SetCookieHeaderResult.Add(string.Format("+{0}", CSC.Name));//cookie added
                }
            }
        }

        void AnalyzeResponseHeaders(int Index, BehaviourAnalysisResult Result)
        {
            foreach (string BaseName in this.BaseLineSession.Response.Headers.GetNames())
            {
                if (!this.Logs[Index].Response.Headers.GetNames().Contains(BaseName))
                {
                    Result.ResponseHeadersResult.Add(string.Format("-{0}", BaseName));//header deleted
                }
            }
            foreach (string CurrentName in this.Logs[Index].Response.Headers.GetNames())
            {
                if (!this.BaseLineSession.Response.Headers.GetNames().Contains(CurrentName))
                {
                    Result.ResponseHeadersResult.Add(string.Format("+{0}", CurrentName));//header added
                }
            }
        }
    }

    public class BehaviourAnalysisResults
    {
        List<BehaviourAnalysisResult> List = new List<BehaviourAnalysisResult>();
        
        public List<string> Keywords = new List<string>();
        public List<string> SetCookies = new List<string>();
        public List<int> Codes = new List<int>();
        public List<int> InsertedChars = new List<int>();
        public List<string> Headers = new List<string>();
        public List<int> PlusRoundtripDiffs = new List<int>();
        public List<int> MinusRoundtripDiffs = new List<int>();

        public BehaviourAnalysisResults(List<BehaviourAnalysisResult> ResultsList)
        {
            this.List = ResultsList;
            List<string> RoundtripsStr = new List<string>();

            foreach (BehaviourAnalysisResult Result in List)
            {
                if (Result.ResponseCodeResult > 0 && !Codes.Contains(Result.ResponseCodeResult)) Codes.Add(Result.ResponseCodeResult);
                if (Result.ResponseContentResult > 0 && !InsertedChars.Contains(Result.ResponseContentResult)) InsertedChars.Add(Result.ResponseContentResult);
                if (Result.RoundtripTimeResult.Length > 0 && Int32.Parse(Result.RoundtripTimeResult.Trim(new char[] { '+', '-', 'm', 's' })) > 0) RoundtripsStr.Add(Result.RoundtripTimeResult);

                foreach (string Keyword in Result.ResponseKeywordsResult)
                {
                    if (!Keywords.Contains(Keyword)) Keywords.Add(Keyword);
                }
                foreach (string SC in Result.SetCookieHeaderResult)
                {
                    if (!SetCookies.Contains(SC)) SetCookies.Add(SC);
                }
                foreach (string H in Result.ResponseHeadersResult)
                {
                    if (!Headers.Contains(H)) Headers.Add(H);
                }
            }

            InsertedChars.Sort();

            if (RoundtripsStr.Count > 0)
            {
                for (int i = 0; i < RoundtripsStr.Count; i++)
                {
                    int RoundtripDiff = Int32.Parse(RoundtripsStr[i].Trim(new char[] { '+', '-', 'm', 's' }));
                    if (RoundtripsStr[i][0] == '+')
                    {
                        PlusRoundtripDiffs.Add(RoundtripDiff);
                    }
                    else
                    {
                        MinusRoundtripDiffs.Add(RoundtripDiff);
                    }
                }


                PlusRoundtripDiffs.Sort();
                MinusRoundtripDiffs.Sort();
            }
        }

    }

    public class BehaviourAnalysisResult
    {
        internal int Weigth = 0;
        internal string Payload = "";
        internal int LogId = 0;
        internal int  ResponseCodeResult = 0; //404
        internal List<string> SetCookieHeaderResult = new List<string>();//{"amSessionId set", "IsAdmin missing", "LoggedIn deleted"}
        internal List<string> ResponseHeadersResult = new List<string>();//{"X-Logged-In added", "X-Is-Admin missing"}
        internal int ResponseContentResult = 0;//40 //treated as %
        internal string RoundtripTimeResult = "";//+1000ms
        internal List<string> ResponseKeywordsResult = new List<string>();//{"error", "exception"}

        internal static string ToXml(BehaviourAnalysisResult Result)
        {
            
            StringBuilder SB = new StringBuilder();
            XmlWriter XW = XmlWriter.Create(SB);

            XW.WriteStartDocument();
            XW.WriteStartElement("BAR");
            
            XW.WriteStartElement("Payload"); XW.WriteValue(Result.Payload); XW.WriteEndElement();
            XW.WriteStartElement("LogId"); XW.WriteValue(Result.LogId); XW.WriteEndElement();
            XW.WriteStartElement("Code"); XW.WriteValue(Result.ResponseCodeResult); XW.WriteEndElement();
            XW.WriteStartElement("Content"); XW.WriteValue(Result.ResponseContentResult); XW.WriteEndElement();
            XW.WriteStartElement("Time"); XW.WriteValue(Result.RoundtripTimeResult); XW.WriteEndElement();
            
            XW.WriteStartElement("SetCookies");
            foreach (string SC in Result.SetCookieHeaderResult)
            {
                XW.WriteStartElement("SetCookie"); XW.WriteValue(SC); XW.WriteEndElement();
            }
            XW.WriteEndElement();
            XW.WriteStartElement("Headers");
            foreach (string H in Result.ResponseHeadersResult)
            {
                XW.WriteStartElement("Header"); XW.WriteValue(H); XW.WriteEndElement();
            }
            XW.WriteEndElement();
            XW.WriteStartElement("Keywords");
            foreach (string K in Result.ResponseKeywordsResult)
            {
                XW.WriteStartElement("Keyword"); XW.WriteValue(K); XW.WriteEndElement();
            }
            XW.WriteEndElement();


            XW.WriteEndElement();
            XW.WriteEndDocument();

            XW.Close();
            return SB.ToString().Split(new string[]{"?>"}, StringSplitOptions.None)[1];
        }

        internal static BehaviourAnalysisResult ToObject(string XML)
        {
            StringReader SR = new StringReader(XML);
            XmlReader XR = XmlReader.Create(SR);

            BehaviourAnalysisResult Result = new BehaviourAnalysisResult();
            
            while (XR.Read())
            {
                if (XR.NodeType == XmlNodeType.Element)
                {
                    switch (XR.Name)
                    {
                        case ("Payload"):
                            XR.Read();
                            Result.Payload = XR.Value;
                            break;
                        case ("LogId"):
                            XR.Read();
                            try
                            {
                                Result.LogId = Int32.Parse(XR.Value);
                            }
                            catch { }
                            break;
                        case ("Code"):
                            XR.Read();
                            try
                            {
                                Result.ResponseCodeResult = Int32.Parse(XR.Value);
                            }
                            catch { }
                            break;
                        case ("Content"):
                            XR.Read();
                            try
                            {
                                Result.ResponseContentResult = Int32.Parse(XR.Value);
                            }
                            catch { }
                            break;
                        case ("Time"):
                            XR.Read();
                            try
                            {
                                Result.RoundtripTimeResult = XR.Value;
                            }
                            catch { }
                            break;
                        case ("SetCookie"):
                            XR.Read();
                            Result.SetCookieHeaderResult.Add(XR.Value);
                            break;
                        case ("Header"):
                            XR.Read();
                            Result.ResponseHeadersResult.Add(XR.Value);
                            break;
                        case ("Keyword"):
                            XR.Read();
                            Result.ResponseKeywordsResult.Add(XR.Value);
                            break;
                    }
                }
            }
            return Result;
        }

        internal static string ToXml(List<BehaviourAnalysisResult> Results)
        {
            StringBuilder SB = new StringBuilder();
            XmlWriter XW = XmlWriter.Create(SB);

            XW.WriteStartDocument();
            XW.WriteStartElement("BARS");

            foreach (BehaviourAnalysisResult Result in Results)
            {
                XW.WriteStartElement("BARNode"); XW.WriteValue(ToXml(Result)); XW.WriteEndElement();
            }

            XW.WriteEndElement();
            XW.WriteEndDocument();

            XW.Close();
            return SB.ToString().Split(new string[] { "?>" }, StringSplitOptions.None)[1];
        }

        internal static List<BehaviourAnalysisResult> ToObjectList(string XML)
        {
            List<BehaviourAnalysisResult> Results = new List<BehaviourAnalysisResult>();
            
            StringReader SR = new StringReader(XML);
            XmlReader XR = XmlReader.Create(SR);

            while (XR.Read())
            {
                if (XR.NodeType == XmlNodeType.Element)
                {
                    if (XR.Name == "BARNode")
                    {
                        XR.Read();
                        Results.Add(ToObject(XR.Value));
                    }
                }
            }
            return Results;
        }
    }
}

