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
using System.Threading;

namespace IronWASP
{
    internal class IronLog
    {
        internal static string CurrentSource = RequestSource.Scan;
        internal static Session CurrentSession;
        internal static int CurrentID = 0;
        internal static int CurrentRowID = 0;
        internal static bool IsSiteMap = false;
        internal const int MaxRowCount = 2000;
        static Thread CurrentThread;

        internal static string SourceControl = "";

        internal static int ProxyMin = 0;
        internal static int ProxyMax = 0;
        internal static int ShellMin = 0;
        internal static int ShellMax = 0;
        internal static int ProbeMin = 0;
        internal static int ProbeMax = 0;
        internal static int ScanMin = 0;
        internal static int ScanMax = 0;
        internal static int TestMin = 0;
        internal static int TestMax = 0;

        internal static int SitemapProxyMin = 0;
        internal static int SitemapProxyMax = 0;
        internal static int SitemapProbeMin = 0;
        internal static int SitemapProbeMax = 0;

        internal static int OtherSourceMin = 0;
        internal static int OtherSourceMax = 0;
        internal static string SelectedOtherSource = "";

        internal static string MainLogDefaultMsg = "Right-click on any log below to get a menu that will let you perform scans and other actions on the selected log.";

        internal static string CurrentSourceName
        {
            get
            {
                return SourceName(CurrentSource);
            }
        }

        internal static void ShowLog(string Source, string ID, int RowID, bool IsSiteMapSelected)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                CurrentRowID = RowID;
                IsSiteMap = IsSiteMapSelected;
                ShowLog(Source, IntID);
            }
            catch { return; }
        }

        internal static void ShowLog(string Source, int ID)
        {
            CurrentSource = Source;
            CurrentID = ID;
            PrepareToShowLog();
            CurrentThread = new Thread(ShowLog);
            CurrentThread.Start();
        }

        internal static void ShowNextLog()
        {
            PrepareToShowLog();
            CurrentID++;
            CurrentThread = new Thread(ShowLog);
            CurrentThread.Start();
        }

        internal static void ShowPreviousLog()
        {
            if (CurrentID > 1)
            {
                PrepareToShowLog();
                CurrentID--;
                CurrentThread = new Thread(ShowLog);
                CurrentThread.Start();
            }
            else
            {
                IronUI.ShowLogStatus("", false);
            }
        }



        static void PrepareToShowLog()
        {
            
            IronUI.ResetLogDisplayFields();
            IronUI.ShowLogStatus("Loading...", false);
            try
            {
                CurrentThread.Abort();
            }
            catch { }
        }

        static void ShowLog()
        {
            try
            {
                Session IrSe = GetLog(CurrentSource, CurrentID);
                CurrentSession = IrSe;
                IronUI.FillLogDisplayFields(IrSe);
            }
            catch(Exception Exp)
            {
                if (Exp.Message.Equals("ID not found in DB"))
                {
                    IronUI.ShowLogStatus(string.Format("Record ID - {0} not found in DB",CurrentID.ToString()), true);
                }
                else
                {
                    IronUI.ShowLogStatus("Unable to load Request/Response from Log", true);
                    IronException.Report("Error reading from " + SourceName(CurrentSource) + " log", Exp.Message, Exp.StackTrace);
                }
            }
        }

        internal static void MarkForTesting(string Source, string ID, string Group)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                MarkForTesting(Source, IntID, Group);
            }
            catch { return; }
        }

        internal static void MarkForTesting(string Source, int ID, string Group)
        {
            object[] Details = new object[] { Source, ID, Group };

            Thread Worker = new Thread(MarkForTesting);
            Worker.Start(Details);
        }

        static void MarkForTesting(object Details)
        {
            try
            {
                object[] DetailsArray = (object[])Details;
                string Source = DetailsArray[0].ToString();
                int ID = (int)DetailsArray[1];
                string Group = DetailsArray[2].ToString();

                Session IrSe = GetLog(Source, ID);
                if (IrSe == null)
                {
                    IronUI.ShowLogStatus("Unable to read Request from log", true);
                    return;
                }
                if (IrSe.Request == null)
                {
                    IronUI.ShowLogStatus("Unable to read Request from log", true);
                    return;
                }
                NameTestGroupWizard NTGW = new NameTestGroupWizard();
                NTGW.RequestToTest = IrSe.Request;
                NTGW.ShowDialog();
                //ManualTesting.CreateNewGroupWithRequest(IrSe.Request, Group);
                //int TestID = Interlocked.Increment(ref Config.TestRequestsCount);
                //IrSe.Request.ID = TestID;
                //IronDB.LogMTRequest(IrSe.Request);
                ////IronDB.ClearGroup(Group);
                //ManualTesting.CurrentRequestID = TestID;
                //ManualTesting.CurrentGroup = Group;
                //ManualTesting.ClearGroup(Group, TestID);
                //ManualTesting.StoreInGroupList(IrSe.Request);
                //IronUI.SetNewTestRequest(IrSe.Request, Group);
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Request from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void MarkForScanning(string Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                MarkForScanning(Source, IntID);
            }
            catch { return; }
        }

        internal static void MarkForScanning(string Source, int ID)
        {
            object[] Details = new object[] { Source, ID};

            Thread Worker = new Thread(MarkForScanning);
            Worker.Start(Details);
        }

        static void MarkForScanning(object Details)
        {
            try
            {
                object[] DetailsArray = (object[])Details;
                string Source = DetailsArray[0].ToString();
                int ID = (int)DetailsArray[1];

                Session IrSe = GetLog(Source, ID);
                if (IrSe == null)
                {
                    IronUI.ShowLogStatus("Unable to read Request from log", true);
                    return;
                }
                if (IrSe.Request == null)
                {
                    IronUI.ShowLogStatus("Unable to read Request from log", true);
                    return;
                }
                //int ScanID = Interlocked.Increment(ref Config.ScanCount);
                //IronDB.CreateScan(ScanID, IrSe.Request);
                //IronUI.CreateScan(ScanID, "Not Started", IrSe.Request.Method, IrSe.Request.FullUrl);
                //Scanner S = new Scanner(IrSe.Request);
                //S.ScanID = ScanID;
                //Scanner.ResetChangedStatus();
                //IronUI.ResetConfigureScanFields();
                //Scanner.SetScannerFromDBToUiAfterProcessing(S);
                //IronUI.ShowScanJobsQueue();
                StartScanJobWizard SSJW = new StartScanJobWizard();
                SSJW.SetRequest(IrSe.Request);
                SSJW.ShowDialog();
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Request from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void MarkForJavaScriptTesting(string Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                MarkForJavaScriptTesting(Source, IntID);
            }
            catch { return; }
        }
        
        internal static void MarkForJavaScriptTesting(string Source, int ID)
        {
            object[] Details = new object[] { Source, ID };

            Thread Worker = new Thread(MarkForJavaScriptTesting);
            Worker.Start(Details);
        }

        static void MarkForJavaScriptTesting(object Details)
        {
            try
            {
                object[] DetailsArray = (object[])Details;
                string Source = DetailsArray[0].ToString();
                int ID = (int)DetailsArray[1];

                Session IrSe = GetLog(Source, ID);
                if (IrSe == null)
                {
                    IronUI.ShowLogStatus("Unable to read Response from log", true);
                    return;
                }
                if (IrSe.Response == null)
                {
                    IronUI.ShowLogStatus("Unable to read Response from 4log", true);
                    return;
                }
                IronUI.FillAndShowJavaScriptTester(IrSe.Response.BodyString);
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Response from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void CopyRequest(string Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                CopyRequest(Source, IntID);
            }
            catch { return; }
        }

        internal static void CopyRequest(string Source, int ID)
        {
            object[] Details = new object[] { Source, ID };

            Thread Worker = new Thread(CopyRequest);
            Worker.Start(Details);
        }

        static void CopyRequest(object Details)
        {
            try
            {
                object[] DetailsArray = (object[])Details;
                string Source = DetailsArray[0].ToString();
                int ID = (int)DetailsArray[1];

                Session IrSe = GetLog(Source, ID);
                if (IrSe == null)
                {
                    IronUI.ShowLogStatus("Unable to read Request from log", true);
                    return;
                }
                if (IrSe.Request == null)
                {
                    IronUI.ShowLogStatus("Unable to read Request from log", true);
                    return;
                }
                IronUI.SetClipBoard(IrSe.Request.ToShortString());
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Request from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void CopyResponse(string Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                CopyResponse(Source, IntID);
            }
            catch { return; }
        }

        internal static void CopyResponse(string Source, int ID)
        {
            object[] Details = new object[] { Source, ID };

            Thread Worker = new Thread(CopyResponse);
            Worker.Start(Details);
        }

        static void CopyResponse(object Details)
        {
            try
            {
                object[] DetailsArray = (object[])Details;
                string Source = DetailsArray[0].ToString();
                int ID = (int)DetailsArray[1];

                Session IrSe = GetLog(Source, ID);
                if (IrSe == null)
                {
                    IronUI.ShowLogStatus("Unable to read Response from log", true);
                    return;
                }
                if (IrSe.Response == null)
                {
                    IronUI.ShowLogStatus("Unable to read Response from log", true);
                    return;
                }
                IronUI.SetClipBoard(IrSe.Response.ToString());
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Request from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        static Session GetLog(string Source, int ID)
        {
            Session IrSe = null;
            switch (Source)
            {
                case RequestSource.Proxy:
                    IrSe = Session.FromProxyLog(ID);
                    break;
                case RequestSource.Scan:
                    IrSe = Session.FromScanLog(ID);
                    break;
                case RequestSource.Shell:
                    IrSe = Session.FromShellLog(ID);
                    break;
                case RequestSource.Test:
                    IrSe = Session.FromTestLog(ID);
                    break;
                case RequestSource.Probe:
                    IrSe = Session.FromProbeLog(ID);
                    break;
                case RequestSource.Trigger:
                    Trigger SelectedTrigger = Finding.CurrentPluginResult.Triggers.GetTrigger(ID -1);
                    if (SelectedTrigger.Request != null)
                    {
                        if (SelectedTrigger.Response == null)
                            IrSe = new Session(SelectedTrigger.Request);
                        else
                            IrSe = new Session(SelectedTrigger.Request, SelectedTrigger.Response);
                    }
                    break;
                case RequestSource.TestGroup:
                    //if (ManualTesting.RedGroupSessions.ContainsKey(ID)) return ManualTesting.RedGroupSessions[ID].GetClone();
                    //if (ManualTesting.BlueGroupSessions.ContainsKey(ID)) return ManualTesting.BlueGroupSessions[ID].GetClone();
                    //if (ManualTesting.GreenGroupSessions.ContainsKey(ID)) return ManualTesting.GreenGroupSessions[ID].GetClone();
                    //if (ManualTesting.GrayGroupSessions.ContainsKey(ID)) return ManualTesting.GrayGroupSessions[ID].GetClone();
                    //if (ManualTesting.BrownGroupSessions.ContainsKey(ID)) return ManualTesting.BrownGroupSessions[ID].GetClone();
                    foreach (string Group in ManualTesting.GroupSessions.Keys)
                    {
                        if(ManualTesting.GroupSessions[Group].ContainsKey(ID))
                            return ManualTesting.GroupSessions[Group][ID].GetClone();
                    }
                    break;
                case RequestSource.SelectedLogEntry:
                    return IronLog.CurrentSession.GetClone();
                case RequestSource.CurrentProxyInterception:
                    return IronProxy.CurrentSession.GetClone();
                default:
                    IrSe = Session.FromLog(ID, Source);
                    break;
            }
            return IrSe;
        }

        internal static int GetJumpCount(int Level)
        {
            switch (Level)
            {
                case(4):
                    return IronLog.MaxRowCount * 125;
                case (3):
                    return IronLog.MaxRowCount * 25;
                case (2):
                    return IronLog.MaxRowCount * 5;
                case (1):
                default:
                    return 0;
            }
        }

        internal static void MoveProxyLogRecordForward(int JumpLevel)
        {
            Thread T = new Thread(MoveProxyLogRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveProxyLogRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<LogRow> Records = GetNextProxyLogRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowLogBottomStatus("Reached end of logs", true);
                return;
            }
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToProxyGridRowObjectArray());
            }
            IronUI.SetProxyGridRows(Rows);
        }
        internal static void MoveProbeLogRecordForward(int JumpLevel)
        {
            Thread T = new Thread(MoveProbeLogRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveProbeLogRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<LogRow> Records = GetNextProbeLogRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowLogBottomStatus("Reached end of logs", true);
                return;
            }
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToProbeGridRowObjectArray());
            }
            IronUI.SetProbeGridRows(Rows);
        }
        internal static void MoveScanLogRecordForward(int JumpLevel)
        {
            Thread T = new Thread(MoveScanLogRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveScanLogRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<LogRow> Records = GetNextScanLogRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowLogBottomStatus("Reached end of logs", true);
                return;
            }
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToScanGridRowObjectArray());
            }
            IronUI.SetScanGridRows(Rows);
        }
        internal static void MoveShellLogRecordForward(int JumpLevel)
        {
            Thread T = new Thread(MoveShellLogRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveShellLogRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int) JumpLevelObj;
            List<LogRow> Records = GetNextShellLogRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowLogBottomStatus("Reached end of logs", true);
                return;
            }
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToShellGridRowObjectArray());
            }
            IronUI.SetShellGridRows(Rows);
        }
        internal static void MoveOtherLogRecordForward(int JumpLevel)
        {
            Thread T = new Thread(MoveOtherLogRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveOtherLogRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<LogRow> Records = GetNextOtherLogRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowLogBottomStatus("Reached end of logs", true);
                return;
            }
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToShellGridRowObjectArray());
            }
            IronUI.SetOtherSourceGridRows(Rows, IronLog.SelectedOtherSource);
        }
        internal static void MoveTestLogRecordForward(int JumpLevel)
        {
            Thread T = new Thread(MoveTestLogRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveTestLogRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<LogRow> Records = GetNextTestLogRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowLogBottomStatus("Reached end of logs", true);
                return;
            }
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToTestGridRowObjectArray());
            }
            IronUI.SetTestGridRows(Rows);
        }

        internal static void MoveProxyLogRecordBack(int JumpLevel)
        {
            Thread T = new Thread(MoveProxyLogRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveProxyLogRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<LogRow> Records = GetPreviousProxyLogRecords(JumpLevel);
            if (Records.Count == 0) return;
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToProxyGridRowObjectArray());
            }
            IronUI.SetProxyGridRows(Rows);
        }
        internal static void MoveProbeLogRecordBack(int JumpLevel)
        {
            Thread T = new Thread(MoveProbeLogRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveProbeLogRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int) JumpLevelObj;
            List<LogRow> Records = GetPreviousProbeLogRecords(JumpLevel);
            if (Records.Count == 0) return;
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToProbeGridRowObjectArray());
            }
            IronUI.SetProbeGridRows(Rows);
        }
        internal static void MoveScanLogRecordBack(int JumpLevel)
        {
            Thread T = new Thread(MoveScanLogRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveScanLogRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int) JumpLevelObj;
            List<LogRow> Records = GetPreviousScanLogRecords(JumpLevel);
            if (Records.Count == 0) return;
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToScanGridRowObjectArray());
            }
            IronUI.SetScanGridRows(Rows);
        }
        internal static void MoveShellLogRecordBack(int JumpLevel)
        {
            Thread T = new Thread(MoveShellLogRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveShellLogRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int) JumpLevelObj;
            List<LogRow> Records = GetPreviousShellLogRecords(JumpLevel);
            if (Records.Count == 0) return;
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToShellGridRowObjectArray());
            }
            IronUI.SetShellGridRows(Rows);
        }
        internal static void MoveOtherLogRecordBack(int JumpLevel)
        {
            Thread T = new Thread(MoveOtherLogRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveOtherLogRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<LogRow> Records = GetPreviousOtherLogRecords(JumpLevel);
            if (Records.Count == 0) return;
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToShellGridRowObjectArray());
            }
            IronUI.SetOtherSourceGridRows(Rows, IronLog.SelectedOtherSource);
        }
        internal static void MoveTestLogRecordBack(int JumpLevel)
        {
            Thread T = new Thread(MoveTestLogRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveTestLogRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int) JumpLevelObj;
            List<LogRow> Records = GetPreviousTestLogRecords(JumpLevel);
            if (Records.Count == 0) return;
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToTestGridRowObjectArray());
            }
            IronUI.SetTestGridRows(Rows);
        }

        internal static List<LogRow> GetNextProxyLogRecords(int JumpLevel)
        {
            int JumpCount = GetJumpCount(JumpLevel);
            int StartIndex = IronLog.ProxyMax + JumpCount;
            List<LogRow> Records = IronDB.GetRecordsFromProxyLog(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastProxyLogId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetRecordsFromProxyLog(NewStartIndex, IronLog.MaxRowCount);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronLog.ProxyMax) Records.Clear();
                    }
                }
            }
            return Records;
        }
        internal static List<LogRow> GetNextProbeLogRecords(int JumpLevel)
        {
            int JumpCount = GetJumpCount(JumpLevel);
            int StartIndex = IronLog.ProbeMax + JumpCount;
            List<LogRow> Records = IronDB.GetRecordsFromProbeLog(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastProbeLogId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetRecordsFromProbeLog(NewStartIndex, IronLog.MaxRowCount);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronLog.ProbeMax) Records.Clear();
                    }
                }
            }
            return Records;
        }
        internal static List<LogRow> GetNextScanLogRecords(int JumpLevel)
        {
            int JumpCount = GetJumpCount(JumpLevel);
            int StartIndex = IronLog.ScanMax + JumpCount;
            List<LogRow> Records = IronDB.GetRecordsFromScanLog(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastScanLogId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetRecordsFromScanLog(NewStartIndex, IronLog.MaxRowCount);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronLog.ScanMax) Records.Clear();
                    }
                }
            }
            return Records;
        }
        internal static List<LogRow> GetNextShellLogRecords(int JumpLevel)
        {
            int JumpCount = GetJumpCount(JumpLevel);
            int StartIndex = IronLog.ShellMax + JumpCount;
            List<LogRow> Records = IronDB.GetRecordsFromShellLog(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastShellLogId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetRecordsFromShellLog(NewStartIndex, IronLog.MaxRowCount);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronLog.ShellMax) Records.Clear();
                    }
                }
            }
            return Records;
        }
        internal static List<LogRow> GetNextOtherLogRecords(int JumpLevel)
        {
            int JumpCount = GetJumpCount(JumpLevel);
            int StartIndex = IronLog.OtherSourceMax + JumpCount;
            List<LogRow> Records = IronDB.GetRecordsFromOtherSourceLog(StartIndex, IronLog.MaxRowCount, IronLog.SelectedOtherSource);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.GetLastLogId(IronLog.SelectedOtherSource) - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetRecordsFromOtherSourceLog(NewStartIndex, IronLog.MaxRowCount, IronLog.SelectedOtherSource);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronLog.OtherSourceMax) Records.Clear();
                    }
                }
            }
            return Records;
        }
        internal static List<LogRow> GetNextTestLogRecords(int JumpLevel)
        {
            int JumpCount = GetJumpCount(JumpLevel);
            int StartIndex = IronLog.TestMax + JumpCount;
            List<LogRow> Records = IronDB.GetRecordsFromTestLog(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastTestLogId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetRecordsFromTestLog(NewStartIndex, IronLog.MaxRowCount);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronLog.TestMax) Records.Clear();
                    }
                }
            }
            return Records;
        }

        internal static List<LogRow> GetPreviousProxyLogRecords(int JumpLevel)
        {
            List<LogRow> Records = new List<LogRow>();
            Records = GetPreviousLogRecords(IronDB.GetRecordsFromProxyLog, IronLog.ProxyMin, JumpLevel);
            return Records;
        }
        internal static List<LogRow> GetPreviousProbeLogRecords(int JumpLevel)
        {
            List<LogRow> Records = new List<LogRow>();
            Records = GetPreviousLogRecords(IronDB.GetRecordsFromProbeLog, IronLog.ProbeMin, JumpLevel);
            return Records;
        }
        internal static List<LogRow> GetPreviousScanLogRecords(int JumpLevel)
        {
            List<LogRow> Records = new List<LogRow>();
            Records = GetPreviousLogRecords(IronDB.GetRecordsFromScanLog, IronLog.ScanMin, JumpLevel);
            return Records;
        }
        internal static List<LogRow> GetPreviousShellLogRecords(int JumpLevel)
        {
            List<LogRow> Records = new List<LogRow>();
            Records = GetPreviousLogRecords(IronDB.GetRecordsFromShellLog, IronLog.ShellMin, JumpLevel);
            return Records;
        }
        internal static List<LogRow> GetPreviousOtherLogRecords(int JumpLevel)
        {
            List<LogRow> Records = new List<LogRow>();
            Records = GetPreviousLogRecords(IronDB.GetRecordsFromSelectedOtherSourceLog, IronLog.OtherSourceMin, JumpLevel);
            return Records;
        }
        internal static List<LogRow> GetPreviousTestLogRecords(int JumpLevel)
        {
            List<LogRow> Records = new List<LogRow>();
            Records = GetPreviousLogRecords(IronDB.GetRecordsFromTestLog, IronLog.TestMin, JumpLevel);
            return Records;
        }

        internal static List<LogRow> GetPreviousLogRecords(GetRecordsDelegate GetRecordMethod, int CurrentMin, int JumpLevel)
        {
            List<LogRow> Records = new List<LogRow>();
            int JumpCount = GetJumpCount(JumpLevel);
            if (CurrentMin <= 1)
            {
                IronUI.ShowLogBottomStatus("Reached beginning of the log. Cannot go back further.", true);
                return Records;
            }
            int StartIndex = CurrentMin - IronLog.MaxRowCount - JumpCount - 1;
            Records = GetRecordMethod(StartIndex, IronLog.MaxRowCount);
            return Records;
        }

        internal delegate List<LogRow> GetRecordsDelegate(int StartIndex, int Count);

        internal static void ShowOtherSourceRecords()
        {
            IronUI.ShowLogBottomStatus("Loading...", false);
            List<LogRow> Records = IronDB.GetRecordsFromOtherSourceLog(0, IronLog.MaxRowCount, IronLog.SelectedOtherSource);
            if (Records.Count == 0) return;
            List<object[]> Rows = new List<object[]>();
            foreach (LogRow Record in Records)
            {
                Rows.Add(Record.ToShellGridRowObjectArray());
            }
            IronUI.SetOtherSourceGridRows(Rows, IronLog.SelectedOtherSource);
        }

        static int[] GetMinMaxIds(List<LogRow> Records)
        {
            int[] MinMax = new int[] {0, 0};
            if (Records.Count > 0)
            {
                MinMax[0] = Records[0].ID;
                MinMax[1] = Records[Records.Count - 1].ID;
            }
            return MinMax;
        }

        internal static string SourceName(string Source)
        {
            string StringSource = "";
            switch (Source)
            {
                case RequestSource.Test:
                    StringSource = "Test";
                    break;
                case RequestSource.Scan:
                    StringSource = "Scan";
                    break;
                case RequestSource.Shell:
                    StringSource = "Shell";
                    break;
                case RequestSource.Probe:
                    StringSource = "Probe";
                    break;
                case RequestSource.Proxy:
                    StringSource = "Proxy";
                    break;
            }
            return StringSource;
        }

        static bool IsActionInProgress()
        {
            if (CurrentThread == null) return false;
            if (CurrentThread.ThreadState == ThreadState.Running || CurrentThread.ThreadState == ThreadState.WaitSleepJoin) return true;
            return false;
        }
    }
}
