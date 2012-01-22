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
using System.Threading;

namespace IronWASP
{
    internal class IronLog
    {
        internal static RequestSource CurrentSource = RequestSource.Scan;
        internal static Session CurrentSession;
        internal static int CurrentID = 0;
        static Thread CurrentThread;

        internal static string SourceControl = "";

        internal static string CurrentSourceName
        {
            get
            {
                return SourceName(CurrentSource);
            }
        }

        internal static void ShowLog(RequestSource Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                ShowLog(Source, IntID);
            }
            catch { return; }
        }

        internal static void ShowLog(RequestSource Source, int ID)
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
                IronUI.FillLogDisplayFields(IrSe, Analyzer.CheckReflections(IrSe));
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

        internal static void MarkForTesting(RequestSource Source, string ID, string Group)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                MarkForTesting(Source, IntID, Group);
            }
            catch { return; }
        }

        internal static void MarkForTesting(RequestSource Source, int ID, string Group)
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
                RequestSource Source = (RequestSource)DetailsArray[0];
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
                int TestID = Interlocked.Increment(ref Config.ManualRequestsCount);
                IrSe.Request.ID = TestID;
                IronDB.LogMTRequest(IrSe.Request);
                IronDB.ClearGroup(Group);
                ManualTesting.CurrentRequestID = TestID;
                ManualTesting.CurrentGroup = Group;
                ManualTesting.ClearGroup(Group, TestID);
                ManualTesting.StoreInGroupList(IrSe.Request);
                IronUI.SetNewTestRequest(IrSe.Request, Group);
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Request from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void MarkForScanning(RequestSource Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                MarkForScanning(Source, IntID);
            }
            catch { return; }
        }

        internal static void MarkForScanning(RequestSource Source, int ID)
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
                RequestSource Source = (RequestSource)DetailsArray[0];
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
                int ScanID = Interlocked.Increment(ref Config.ScanCount);
                IronDB.CreateScan(ScanID, IrSe.Request);
                IronUI.CreateScan(ScanID, "Not Started", IrSe.Request.Method, IrSe.Request.FullUrl);
                IronUI.ShowScanJobsQueue();
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Request from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void MarkForJavaScriptTesting(RequestSource Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                MarkForJavaScriptTesting(Source, IntID);
            }
            catch { return; }
        }
        
        internal static void MarkForJavaScriptTesting(RequestSource Source, int ID)
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
                RequestSource Source = (RequestSource)DetailsArray[0];
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
                IronUI.FillAndShowJavaScriptTester(IrSe.Response.BodyString);
            }
            catch (Exception Exp)
            {
                IronUI.ShowLogStatus("Unable to read Response from Log", true);
                IronException.Report("Error reading from log", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void CopyRequest(RequestSource Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                CopyRequest(Source, IntID);
            }
            catch { return; }
        }

        internal static void CopyRequest(RequestSource Source, int ID)
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
                RequestSource Source = (RequestSource)DetailsArray[0];
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

        internal static void CopyResponse(RequestSource Source, string ID)
        {
            try
            {
                int IntID = Int32.Parse(ID);
                CopyResponse(Source, IntID);
            }
            catch { return; }
        }

        internal static void CopyResponse(RequestSource Source, int ID)
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
                RequestSource Source = (RequestSource)DetailsArray[0];
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

        static Session GetLog(RequestSource Source, int ID)
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
                    Trigger SelectedTrigger = PluginResult.CurrentPluginResult.Triggers.GetTrigger(ID -1);
                    if (SelectedTrigger.Request != null)
                    {
                        if (SelectedTrigger.Response == null)
                            IrSe = new Session(SelectedTrigger.Request);
                        else
                            IrSe = new Session(SelectedTrigger.Request, SelectedTrigger.Response);
                    }
                    break;
                case RequestSource.TestGroup:
                    if (ManualTesting.RedGroupSessions.ContainsKey(ID)) return ManualTesting.RedGroupSessions[ID].GetClone();
                    if (ManualTesting.BlueGroupSessions.ContainsKey(ID)) return ManualTesting.BlueGroupSessions[ID].GetClone();
                    if (ManualTesting.GreenGroupSessions.ContainsKey(ID)) return ManualTesting.GreenGroupSessions[ID].GetClone();
                    if (ManualTesting.GrayGroupSessions.ContainsKey(ID)) return ManualTesting.GrayGroupSessions[ID].GetClone();
                    if (ManualTesting.BrownGroupSessions.ContainsKey(ID)) return ManualTesting.BrownGroupSessions[ID].GetClone();
                    break;
                case RequestSource.SelectedLogEntry:
                    return IronLog.CurrentSession.GetClone();
                case RequestSource.CurrentProxyInterception:
                    return IronProxy.CurrentSession.GetClone();
            }
            return IrSe;
        }

        internal static string SourceName(RequestSource Source)
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
