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
using System.Threading;

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

        internal IronTrace(int ScanID, string PluginName, string Section, string Parameter, string Title, string Message, List<string[]> OverviewEntries)
        {
            this.ID = Interlocked.Increment(ref Config.ScanTraceCount);
            this.ScanID = ScanID;
            this.PluginName = PluginName;
            this.Section = Section;
            this.Parameter = Parameter;
            this.Title = Title;
            this.Message = Message;
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
            List<IronTrace> Records = IronDB.GetScanTraceRecords(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastScanTraceId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetScanTraceRecords(NewStartIndex, IronLog.MaxRowCount);
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
            Records = IronDB.GetScanTraceRecords(StartIndex, IronLog.MaxRowCount);
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

                if (OverviewEntries.Count == 13)
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
    }
}
