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
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace IronWASP
{
    internal class IronTrace
    {
        internal int ID=0;
        internal int ThreadID = 0;
        internal string Time = "";
        internal string Date = "";
        internal string Source = "";
        internal string Message = "";

        internal int ScanID = 0;
        internal string PluginName = "";
        internal string Section = "";
        internal string Parameter = "";
        internal string Title = "";
        internal string OverviewXml = "";
        string messageXml = "";

        internal int LogId = 0;
        internal string LogSource = "";
        internal string SessionPluginName = "";
        internal string Action = "";

        internal static int ScanTraceMin = 0;
        internal static int ScanTraceMax = 0;

        internal static int SelectedSessionPluginTraceLogId = 0;
        internal static string SelectedSessionPluginTraceSource = "";

        string Type = "Normal";

        static Thread SessionLogLoadThread;

        internal string MessageXml
        {
            set
            {
                try
                {
                    XmlDocument Xdoc = new XmlDocument();
                    Xdoc.XmlResolver = null;
                    Xdoc.LoadXml(value);
                }
                catch
                {
                    throw new Exception("Invalid Message Xml");
                }
                this.messageXml = value;
            }
            get
            {
                return this.messageXml;
            }
        }
        
        internal IronTrace()
        {

        }
        
        internal IronTrace(string Source, string Message)
        {
            this.ID = Interlocked.Increment(ref Config.TraceCount);
            this.ThreadID = Thread.CurrentThread.ManagedThreadId;
            this.Time = DateTime.Now.ToShortTimeString();
            this.Date = DateTime.Now.ToShortDateString();
            this.Source = Source;
            this.Message = Message;
            this.Type = "Normal";
        }

        internal IronTrace(int ScanID, string PluginName, string Section, string Parameter, string Title, string MessageXml, List<string[]> OverviewEntries)
        {
            this.ID = Interlocked.Increment(ref Config.ScanTraceCount);
            this.ScanID = ScanID;
            this.PluginName = PluginName;
            this.Section = Section;
            this.Parameter = Parameter;
            this.Title = Title;
            try
            {
                this.MessageXml = MessageXml;
            }
            catch 
            { 
                this.Message = MessageXml; 
            }
            this.OverviewXml = GetXmlFromOverviewEntries(OverviewEntries);
            this.Type = "Scan";
        }

        internal IronTrace(Request Req, string PluginName, string Action, string Message)
        {
            this.ID = Interlocked.Increment(ref Config.SessionPluginTraceCount);
            if (Req == null)
            {
                this.LogId = 0;
                this.LogSource = "";
            }
            else
            {
                this.LogId = Req.LogId;
                this.LogSource = Req.Source;
            }
            this.SessionPluginName = PluginName;
            this.Action = Action;
            this.Message = Message;
            this.Type = "SessionPlugin";
        }

        internal void Report()
        {
            if (this.Type.Equals("Normal"))
            {
                IronUpdater.AddTrace(this);
            }
            else if (this.Type.Equals("SessionPlugin"))
            {
                IronUpdater.AddSessionPluginTrace(this);
            }
            else
            {
                IronUpdater.AddScanTrace(this);
            }
        }

        internal string GetScanTracePrettyMessage()
        {
            if (this.MessageXml.Length == 0)
            {
                return this.Message;
            }

            StringBuilder SB = new StringBuilder();

            XmlDocument XDoc = new XmlDocument();
            XDoc.XmlResolver = null;
            XDoc.LoadXml(this.MessageXml);
            foreach (XmlElement Node in XDoc.DocumentElement.ChildNodes)
            {
                if (Node.Name.Equals("type_a"))
                {
                    SB.Append(string.Format("<i<br>>{0}", Node.InnerText));
                }
                else if (Node.Name.Equals("type_b"))
                {
                    int LogId = 0;
                    string RequestTrace = "";
                    string ResponseTrace = "";
                    foreach (XmlElement InnerNode in Node.ChildNodes)
                    {
                        switch (InnerNode.Name)
                        {
                            case ("log_id"):
                                LogId = Int32.Parse(InnerNode.InnerText);
                                break;
                            case ("req"):
                                RequestTrace = InnerNode.InnerText;
                                break;
                            case ("res"):
                                ResponseTrace = InnerNode.InnerText;
                                break;
                        }
                    }
                    SB.Append(string.Format("<i<br>>{0} | {1} {2}", LogId, RequestTrace, ResponseTrace));
                }
            }
            SB.Append("<i<br>>");
            return SB.ToString();
        }

        internal static void MoveScanTraceRecordForward(int JumpLevel)
        {
            IronUI.ShowScanTraceStatus("Loading please wait....", false);
            Thread T = new Thread(MoveScanTraceRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveScanTraceRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<IronTrace> Records = GetNextScanTraceRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowScanTraceStatus("Reached end of Scan Traces", true);
                return;
            }
            IronUI.SetScanTraceGrid(Records);
        }
        internal static List<IronTrace> GetNextScanTraceRecords(int JumpLevel)
        {
            int JumpCount = IronLog.GetJumpCount(JumpLevel);
            int StartIndex = IronTrace.ScanTraceMax + JumpCount;
            List<IronTrace> Records = IronDB.GetScanTraces(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastScanTraceId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetScanTraces(NewStartIndex, IronLog.MaxRowCount);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronTrace.ScanTraceMax) Records.Clear();
                    }
                }
            }
            return Records;
        }


        internal static void MoveScanTraceRecordBack(int JumpLevel)
        {
            IronUI.ShowScanTraceStatus("Loading please wait....", false);
            Thread T = new Thread(MoveScanTraceRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveScanTraceRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<IronTrace> Records = GetPreviousScanTraceRecords(JumpLevel);
            if (Records.Count == 0) return;
            IronUI.SetScanTraceGrid(Records);
        }

        internal static List<IronTrace> GetPreviousScanTraceRecords(int JumpLevel)
        {
            List<IronTrace> Records = new List<IronTrace>();
            int CurrentMin = IronTrace.ScanTraceMin;
            int JumpCount = IronLog.GetJumpCount(JumpLevel);
            if (CurrentMin <= 1)
            {
                IronUI.ShowScanTraceStatus("Reached beginning of the log. Cannot go back further.", true);
                return Records;
            }
            int StartIndex = CurrentMin - IronLog.MaxRowCount - JumpCount - 1;
            Records = IronDB.GetScanTraces(StartIndex, IronLog.MaxRowCount);
            return Records;
        }

        static int[] GetScanTraceMinMaxIds(List<IronTrace> Records)
        {
            int[] MinMax = new int[] { 0, 0 };
            if (Records.Count > 0)
            {
                MinMax[0] = Records[0].ID;
                MinMax[1] = Records[Records.Count - 1].ID;
            }
            return MinMax;
        }

        internal static string GetXmlFromOverviewEntries(List<string[]> OverviewEntries)
        {
            //For reference
            //if (this.TestResponse.ID == ResponseFromInjection.ID)
            //    this.TraceOverviewEntries.Add(new string[] { Payload, ResponseFromInjection.ID.ToString(), ResponseFromInjection.Code.ToString(), ResponseFromInjection.BodyLength.ToString(), ResponseFromInjection.ContentType, ResponseFromInjection.RoundTrip.ToString(), Tools.MD5(ResponseFromInjection.ToString()) });
            //else
            //    this.TraceOverviewEntries.Add(new string[] { Payload, this.TestResponse.ID.ToString(), this.TestResponse.Code.ToString(), this.TestResponse.BodyLength.ToString(), this.TestResponse.ContentType, this.TestResponse.RoundTrip.ToString(), Tools.MD5(this.TestResponse.ToString()), ResponseFromInjection.ID.ToString(), ResponseFromInjection.Code.ToString(), ResponseFromInjection.BodyLength.ToString(), ResponseFromInjection.ContentType, ResponseFromInjection.RoundTrip.ToString(), Tools.MD5(ResponseFromInjection.ToString()) });

            StringBuilder SB = new StringBuilder();
            XmlWriter XW = XmlWriter.Create(SB);
            XW.WriteStartDocument();
            XW.WriteStartElement("overview");
            for (int i = 0; i < OverviewEntries.Count; i++)
            {
                XW.WriteStartElement("entry");

                XW.WriteStartElement("id"); XW.WriteValue(i + 1); XW.WriteEndElement();
                XW.WriteStartElement("payload"); XW.WriteValue(Tools.Base64Encode(OverviewEntries[i][0])); XW.WriteEndElement();

                XW.WriteStartElement("log_id"); XW.WriteValue(OverviewEntries[i][1]); XW.WriteEndElement();
                XW.WriteStartElement("code"); XW.WriteValue(OverviewEntries[i][2]); XW.WriteEndElement();
                XW.WriteStartElement("length"); XW.WriteValue(OverviewEntries[i][3]); XW.WriteEndElement();
                XW.WriteStartElement("mime"); XW.WriteValue(OverviewEntries[i][4]); XW.WriteEndElement();
                XW.WriteStartElement("time"); XW.WriteValue(OverviewEntries[i][5]); XW.WriteEndElement();
                XW.WriteStartElement("signature"); XW.WriteValue(OverviewEntries[i][6]); XW.WriteEndElement();

                XW.WriteEndElement();

                if (OverviewEntries[i].Length == 13)
                {
                    XW.WriteStartElement("entry");

                    XW.WriteStartElement("id"); XW.WriteValue(i + 1); XW.WriteEndElement();
                    XW.WriteStartElement("payload"); XW.WriteValue(Tools.Base64Encode(OverviewEntries[i][0])); XW.WriteEndElement();

                    XW.WriteStartElement("log_id"); XW.WriteValue(OverviewEntries[i][7]); XW.WriteEndElement();
                    XW.WriteStartElement("code"); XW.WriteValue(OverviewEntries[i][8]); XW.WriteEndElement();
                    XW.WriteStartElement("length"); XW.WriteValue(OverviewEntries[i][9]); XW.WriteEndElement();
                    XW.WriteStartElement("mime"); XW.WriteValue(OverviewEntries[i][10]); XW.WriteEndElement();
                    XW.WriteStartElement("time"); XW.WriteValue(OverviewEntries[i][11]); XW.WriteEndElement();
                    XW.WriteStartElement("signature"); XW.WriteValue(OverviewEntries[i][12]); XW.WriteEndElement();

                    XW.WriteEndElement();
                }
            }
            XW.WriteEndElement();
            XW.WriteEndDocument();
            XW.Close();
            return SB.ToString();
        }

        internal static List<Dictionary<string, string>> GetOverviewEntriesFromXml(string OverviewXml)
        {
            List<Dictionary<string, string>> OverviewEntries = new List<Dictionary<string, string>>();
            XmlDocument XDoc = new XmlDocument();
            XDoc.XmlResolver = null;
            try
            {
                XDoc.LoadXml(OverviewXml);
                foreach (XmlNode EntryNode in XDoc.SelectNodes("//entry"))
                {
                    try
                    {
                        Dictionary<string, string> Entry = new Dictionary<string, string>()
                        {
                            {"id", EntryNode.SelectNodes("id")[0].InnerText},
                            {"payload", Tools.EncodeForTrace(Tools.Base64Decode(EntryNode.SelectNodes("payload")[0].InnerText))},
                            {"log_id", EntryNode.SelectNodes("log_id")[0].InnerText},
                            {"code", EntryNode.SelectNodes("code")[0].InnerText},
                            {"length", EntryNode.SelectNodes("length")[0].InnerText},
                            {"mime", EntryNode.SelectNodes("mime")[0].InnerText},
                            {"time", EntryNode.SelectNodes("time")[0].InnerText},
                            {"signature", EntryNode.SelectNodes("signature")[0].InnerText}
                        };
                        OverviewEntries.Add(Entry);
                    }
                    catch { }
                }
            }
            catch { }
            return OverviewEntries;
        }

        internal static void LoadSessionPluginTraceLog()
        {
            if (SelectedSessionPluginTraceLogId > 0 && SelectedSessionPluginTraceSource.Length > 0)
            {
                if (SessionLogLoadThread != null)
                {
                    try
                    {
                        SessionLogLoadThread.Abort();
                    }
                    catch { }
                }
                IronUI.ShowHideSessionPluginTraceProgressBar(true);
                SessionLogLoadThread = new System.Threading.Thread(DoLoadSessionPluginTraceLog);
                SessionLogLoadThread.Start();
            }
        }

        static void DoLoadSessionPluginTraceLog()
        {
            try
            {
                Session Sess = Session.FromLog(SelectedSessionPluginTraceLogId, SelectedSessionPluginTraceSource);
                IronUI.ShowSessionPluginTraceLog(Sess.Request, Sess.Response);
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp)
            {
                IronException.Report(string.Format("Error loading log-{0} from {1} log", SelectedSessionPluginTraceLogId, SelectedSessionPluginTraceSource), Exp);
            }
            finally
            {
                IronUI.ShowHideSessionPluginTraceProgressBar(false);
            }
        }

        internal static List<object[]> GetGridRowsFromTraceAndOverviewXml(string OverviewXml, string TraceXml)
        {
            List<object[]> Rows = new List<object[]>();
            Dictionary<string, string> Signatures = new Dictionary<string, string>();

            List<Dictionary<string, string>> ODict = GetOverviewEntriesFromXml(OverviewXml);
            List<string[]> TArr = GetTraceXmlEntries(TraceXml);

            int TArrPointer = -1;
            int TArrLogId = 0;

            for (int odi = 0; odi < ODict.Count ; odi++)
            {
                Dictionary<string, string> ODR = ODict[odi];
                int ODictLogId = Int32.Parse(ODR["log_id"]);

                string Sign = "";
                if (Signatures.ContainsKey(ODR["signature"]))
                {
                    Sign = Signatures[ODR["signature"]];
                }
                else
                {
                    Sign = GetShortResponseSignature(Signatures.Count);
                    Signatures.Add(ODR["signature"], Sign);
                }

                if (odi == 0)
                {
                    Rows.Add(new object[] { false, Rows.Count + 1, ODR["log_id"], ODR["payload"], ODR["code"], ODR["length"], ODR["mime"], ODR["time"], Sign, "<i<cg>>This is the baseline response<i</cg>>" });
                    continue;
                }
                
                
                while (ODictLogId > TArrLogId && TArrPointer+1 < TArr.Count)
                {
                    TArrPointer++;
                    try
                    {
                        TArrLogId = Int32.Parse(TArr[TArrPointer][0]);
                    }
                    catch
                    {
                        if (TArr[TArrPointer][1].Length > 0)
                            Rows.Add(new object[] { false, Rows.Count + 1, null, null, null, null, null, null, null, TArr[TArrPointer][1] });
                    }
                }

                


                if (ODictLogId == TArrLogId)
                {
                    Rows.Add(new object[] { false, Rows.Count + 1, ODR["log_id"], ODR["payload"], ODR["code"], ODR["length"], ODR["mime"], ODR["time"], Sign, TArr[TArrPointer][1] });
                }
                else
                {
                    Rows.Add(new object[] { false, Rows.Count + 1, ODR["log_id"], ODR["payload"], ODR["code"], ODR["length"], ODR["mime"], ODR["time"], Sign, "" });
                }
            }

            return Rows;
        }

        static List<string[]> GetTraceXmlEntries(string TraceXml)
        {
            List<string[]> Result = new List<string[]>();
            XmlDocument XDoc = new XmlDocument();
            XDoc.XmlResolver = null;
            try
            {
                XDoc.LoadXml(TraceXml);
                foreach (XmlElement Node in XDoc.DocumentElement.ChildNodes)
                {
                    if (Node.Name.Equals("type_a"))
                    {
                        foreach (string Line in SplitMessage(Node.InnerText))
                        {
                            Result.Add(new string[]{"", Line});
                            //Rows.Add(new object[] { false, null, null, null, null, null, null, null, null, Line });
                        }
                    }
                    else if (Node.Name.Equals("type_b"))
                    {
                        int LogId = 0;
                        string RequestTrace = "";
                        string ResponseTrace = "";
                        foreach (XmlElement InnerNode in Node.ChildNodes)
                        {
                            switch (InnerNode.Name)
                            {
                                case ("log_id"):
                                    try
                                    {
                                        LogId = Int32.Parse(InnerNode.InnerText);
                                    }
                                    catch { }
                                    break;
                                case ("req"):
                                    RequestTrace = InnerNode.InnerText;
                                    break;
                                case ("res"):
                                    ResponseTrace = InnerNode.InnerText;
                                    break;
                            }
                        }

                        List<string> MessageLines = SplitMessage(string.Format("{0} {1}", RequestTrace, ResponseTrace));

                        for (int i = 0; i < MessageLines.Count; i++)
                        {
                            if (i == 0 && LogId > 0)
                            {
                                Result.Add(new string[] { LogId.ToString(), MessageLines[i] });
                            }
                            else
                            {
                                Result.Add(new string[] { "", MessageLines[i] });
                            }
                        }
                    }
                }
            }
            catch{}
            return Result;
        }

        static List<string> SplitMessage(string Message)
        {
            List<string> Lines = new List<string>();
            string OpenColorTag = "";
            foreach (string Line in Message.Split(new string[] { "<i<br>>" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string LineToAdd = "";
                if (OpenColorTag.Length > 0)
                {
                    string OT = string.Format("<i<{0}>>", OpenColorTag);
                    string CT = string.Format("<i</{0}>>", OpenColorTag);
                    if (Line.Contains(CT))
                    {
                        LineToAdd = string.Format("{0}{1}", OT, Line);
                        OpenColorTag = "";
                    }
                    else
                    {
                        LineToAdd = string.Format("{0}{1}{2}", OT, Line, CT);
                    }
                }
                else
                {
                    foreach (string Color in new List<string> { "cr", "co", "h", "hh", "cb", "cg", "b" })
                    {
                        string OT = string.Format("<i<{0}>>", Color);
                        string CT = string.Format("<i</{0}>>", Color);

                        if (Line.Contains(OT) && !Line.Contains(CT))
                        {
                            LineToAdd = string.Format("{0} {1}", Line, CT);
                            OpenColorTag = Color;
                        }
                    }
                }
                if (LineToAdd.Length == 0)
                {
                    LineToAdd = Line;
                }
                Lines.Add(LineToAdd);
            }
            if (Lines.Count == 0)
            {
                Lines.Add("");
            }
            return Lines;
        }

        public static string GetShortResponseSignature(int Count)
        {
            if (Count < 26)
            {
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Count].ToString();
            }
            else
            {
                return GetShortResponseSignature(Count / 26) + GetShortResponseSignature(Count % 26);
            }
        }
    }
}
