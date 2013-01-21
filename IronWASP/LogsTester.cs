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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace IronWASP
{
    public partial class LogsTester : Form
    {
        int CurrentStep = 0;
        
        int CurrentIndex = 0;
        int Counter = 0;
        Dictionary<int, int> LogIdsToTest = new Dictionary<int, int>();
        string LogSource = "";

        string TestType = AddParameterTest;
        SessionPlugin SessionHandler = new SessionPlugin();

        public const string AddParameterTest = "AddParameterTest";
        public const string EditParameterTest = "EditParameterTest";
        public const string DeleteParameterTest = "DeleteParameterTest";
        public const string PassivePluginTest = "PassivePluginTest";

        public const string UrlPartPartSection = "UrlPathPart";
        public const string QuerySection = "Query";
        public const string NormalBodySection = "Body";
        public const string OtherTypeBodySection = "OtherTypeBodySection";
        public const string CookieSection = "Cookie";
        public const string HeaderSection = "Header";

        const string TesterLogSourceAttributeValue = "LogTester";

        string ParameterSection = "";
        string ParameterName = "";
        int UrlPathPartPosition = 0;
        string ParameterValue = "";

        List<string> PassivePluginsToRun = new List<string>();
        int FindingsIdCounter = 0;

        Dictionary<int, Findings> AllFindings = new Dictionary<int, Findings>();

        bool DoTest = true;

        bool ShouldResendOriginalRequest = true;

        internal static Thread TestThread;
        internal static Thread LogLoadThread;

        public LogsTester()
        {
            InitializeComponent();
        }

        private void ParameterManipulationResultsLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ParameterManipulationResultsLogGrid.SelectedCells.Count < 1 || ParameterManipulationResultsLogGrid.SelectedCells[0].Value == null || ParameterManipulationResultsLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            int OriginalLogId = (int)ParameterManipulationResultsLogGrid.SelectedCells[0].Value;
            int ResentLogId = 0;
            if (ParameterManipulationResultsLogGrid.SelectedCells[3].Value != null)
                ResentLogId = (int)ParameterManipulationResultsLogGrid.SelectedCells[3].Value;
            int TestLogId = 0;
            if (ParameterManipulationResultsLogGrid.SelectedCells[6].Value != null)
                TestLogId = (int)ParameterManipulationResultsLogGrid.SelectedCells[6].Value;
            ShowSelectedTestResult(this.LogSource, OriginalLogId, ResentLogId, TestLogId);
        }

        void ShowSelectedTestResult(string Source, int OriginalLogId, int ResentLogId, int TestLogId)
        {
            ParameterManipulationResultsBottomTabs.Visible = false;
            ParameterManipulationLoadProgressBar.Visible = true;

            OriginalRequestView.ClearRequest();
            OriginalResponseView.ClearResponse();
            ResentRequestView.ClearRequest();
            ResentResponseView.ClearResponse();
            TestRequestView.ClearRequest();
            TestResponseView.ClearResponse();

            OriginalVsResentRequestDRV.ClearDiffResults();
            OriginalVsResentResponseDRV.ClearDiffResults();
            ResentVsTestRequestDRV.ClearDiffResults();
            ResentVsTestResponseDRV.ClearDiffResults();
            OriginalVsTestRequestDRV.ClearDiffResults();
            OriginalVsTestResponseDRV.ClearDiffResults();

            if (LogLoadThread != null)
            {
                try
                {
                    LogLoadThread.Abort();
                }
                catch { }
            }
            object[] SelectedLogInfo = new object[] { Source, OriginalLogId, ResentLogId, TestLogId};
            LogLoadThread = new Thread(ShowSelectedTestResult);
            LogLoadThread.Start(SelectedLogInfo);
        }

        void ShowSelectedTestResult(object SelectedLogInfoObject)
        {
            object[] SelectedLogInfo = (object[])SelectedLogInfoObject;
            string Source = SelectedLogInfo[0].ToString();
            int OriginalLogId = (int) SelectedLogInfo[1];
            int ResentLogId = (int) SelectedLogInfo[2];
            int TestLogId = (int)SelectedLogInfo[3];
            try
            {
                Session OriginalSession = Session.FromLog(OriginalLogId, Source);
                Session ResentSession = null;
                if (ResentLogId > 0)
                    ResentSession = Session.FromLog(ResentLogId, TesterLogSourceAttributeValue);
                Session TestSession = null;
                if (TestLogId > 0)
                    TestSession = Session.FromLog(TestLogId, TesterLogSourceAttributeValue);
                string OriginalRequestString = "";
                string OriginalResponseString = "";
                string ResentRequestString = "";
                string ResentResponseString = "";
                string TestRequestString = "";
                string TestResponseString = "";
                if (OriginalSession.Request != null)
                {
                    OriginalRequestView.SetRequest(OriginalSession.Request);
                    OriginalRequestString = OriginalSession.Request.ToString();
                }
                if (OriginalSession.Response != null)
                {
                    OriginalResponseView.SetResponse(OriginalSession.Response);
                    OriginalResponseString = OriginalSession.Response.ToString();
                }
                if (ResentSession != null && ResentSession.Request != null)
                {
                    ResentRequestView.SetRequest(ResentSession.Request);
                    ResentRequestString = ResentSession.Request.ToString();
                }
                if (ResentSession != null && ResentSession.Response != null)
                {
                    ResentResponseView.SetResponse(ResentSession.Response);
                    ResentResponseString = ResentSession.Response.ToString();
                }
                if (TestSession != null && TestSession.Request != null)
                {
                    TestRequestView.SetRequest(TestSession.Request);
                    TestRequestString = TestSession.Request.ToString();
                }
                if (TestSession != null && TestSession.Response != null)
                {
                    TestResponseView.SetResponse(TestSession.Response);
                    TestResponseString = TestSession.Response.ToString();
                }
                string[] OriginalVsResentRequestSidebySideResults = DiffWindow.DoSideBySideDiff(OriginalRequestString, ResentRequestString);
                string[] OriginalVsResentResponseSidebySideResults = DiffWindow.DoSideBySideDiff(OriginalResponseString, ResentResponseString);
                string[] OriginalVsTestRequestSidebySideResults = DiffWindow.DoSideBySideDiff(OriginalRequestString, TestRequestString);
                string[] OriginalVsTestResponseSidebySideResults = DiffWindow.DoSideBySideDiff(OriginalResponseString, TestResponseString);
                string[] ResentVsTestRequestSidebySideResults = DiffWindow.DoSideBySideDiff(ResentRequestString, TestRequestString);
                string[] ResentVsTestResponseSidebySideResults = DiffWindow.DoSideBySideDiff(ResentResponseString, TestResponseString);

                string OriginalVsResentRequestSinglePageResults = DiffWindow.DoSinglePageDiff(OriginalRequestString, ResentRequestString);
                string OriginalVsResentResponseSinglePageResults = DiffWindow.DoSinglePageDiff(OriginalResponseString, ResentResponseString);
                string OriginalVsTestRequestSinglePageResults = DiffWindow.DoSinglePageDiff(OriginalRequestString, TestRequestString);
                string OriginalVsTestResponseSinglePageResults = DiffWindow.DoSinglePageDiff(OriginalResponseString, TestResponseString);
                string ResentVsTestRequestSinglePageResults = DiffWindow.DoSinglePageDiff(ResentRequestString, TestRequestString);
                string ResentVsTestResponseSinglePageResults = DiffWindow.DoSinglePageDiff(ResentResponseString, TestResponseString);

                OriginalVsResentRequestDRV.ShowDiffResults(OriginalVsResentRequestSinglePageResults, OriginalVsResentRequestSidebySideResults[0], OriginalVsResentRequestSidebySideResults[1]);
                OriginalVsResentResponseDRV.ShowDiffResults(OriginalVsResentResponseSinglePageResults, OriginalVsResentResponseSidebySideResults[0], OriginalVsResentResponseSidebySideResults[0]);
                ResentVsTestRequestDRV.ShowDiffResults(ResentVsTestRequestSinglePageResults, ResentVsTestRequestSidebySideResults[0], ResentVsTestRequestSidebySideResults[1]);
                ResentVsTestResponseDRV.ShowDiffResults(ResentVsTestResponseSinglePageResults, ResentVsTestResponseSidebySideResults[0], ResentVsTestResponseSidebySideResults[1]);
                OriginalVsTestRequestDRV.ShowDiffResults(OriginalVsTestRequestSinglePageResults, OriginalVsTestRequestSidebySideResults[0], OriginalVsTestRequestSidebySideResults[1]);
                OriginalVsTestResponseDRV.ShowDiffResults(OriginalVsTestResponseSinglePageResults, OriginalVsTestResponseSidebySideResults[0], OriginalVsTestResponseSidebySideResults[1]);
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp) { IronException.Report("Error loading Log Tester info", Exp); }
            finally
            {
                EndLogLoad();
            }
        }
        internal void SetSourceAndLogs(string Source, List<int> LogIds)
        {
            this.LogSource = Source;
            this.CurrentIndex = 0;
            this.Counter = 0;
            this.LogIdsToTest.Clear();
            foreach (int ID in LogIds)
            {
                this.Counter++;
                this.LogIdsToTest[Counter] = ID;
            }
        }

        void StartTest()
        {
            try
            {
                this.DoTest = true;
                if (this.CurrentIndex == 0)
                {
                    this.AllFindings.Clear();
                    FindingsIdCounter = 0;
                }
                for (int i = 1; i <= this.LogIdsToTest.Count; i++)
                {
                    if (!this.DoTest) return;
                    if (this.CurrentIndex < i)
                    {
                        this.CurrentIndex = i;
                        this.DoTest = this.TestCurrentIndex();
                    }
                }
                this.CurrentIndex = 0;
                ShowStatus("Testing complete");
                SetButtonStatusOnTestCompletion();
            }
            catch (ThreadAbortException) { }
            catch(Exception Exp)
            {
                IronException.Report("Error performing log testing", Exp);
            }
        }

        delegate void SetButtonStatusOnTestCompletion_d();
        void SetButtonStatusOnTestCompletion()
        {
            if (ParameterManipulationResultsStatusLbl.InvokeRequired)
            {
                SetButtonStatusOnTestCompletion_d CALL_d = new SetButtonStatusOnTestCompletion_d(SetButtonStatusOnTestCompletion);
                ParameterManipulationResultsStatusLbl.Invoke(CALL_d, new object[] { });
            }
            else
            {
                PlayBtn.Enabled = true;
                PauseBtn.Enabled = false;
                StopBtn.Enabled = false;
            }
        }

        bool TestCurrentIndex()
        {
            int LogId = this.LogIdsToTest[this.CurrentIndex];
            ShowStatus(string.Format("Testing Log ID {0}", LogId));
            try
            {
                Session Sess = Session.FromLog(LogId, this.LogSource);
                if (this.TestType.Equals(PassivePluginTest))
                    return this.DoPassivePluginTestOnCurrentIndex(LogId, Sess);
                else
                    return this.DoParameterTestOnCurrentIndex(LogId, Sess);
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp)
            {
                ShowStatus("Test stopped, encountered error");
                IronException.Report(string.Format("Error Reading {0} Log - {1}", LogSource, LogId), Exp);
                return false;
            }
            return false;
        }

        bool DoPassivePluginTestOnCurrentIndex(int LogId, Session Sess)
        {
            foreach (string PluginName in this.PassivePluginsToRun)
            {
                PassivePlugin P = PassivePlugin.Get(PluginName);
                if (P.WorksOn == PluginWorksOn.Response && Sess.Response == null) continue;
                try
                {
                    ShowStatus(string.Format("Testing Log ID {0} with plugin - {1}", LogId, PluginName));
                    Findings CheckResults = new Findings();
                    P.Check(Sess, CheckResults, true);
                    foreach (Finding F in CheckResults.GetAll())
                    {
                        foreach (Trigger T in F.Triggers.GetTriggers())
                        {
                            //To save memory
                            T.Request = null;
                            T.Response = null;
                        }
                    }
                    if (CheckResults.GetAll().Count > 0)
                    {
                        lock (AllFindings)
                        {
                            if (!AllFindings.ContainsKey(LogId)) AllFindings[LogId] = new Findings();
                            foreach (Finding F in CheckResults.GetAll())
                            {
                                FindingsIdCounter++;
                                F.Id = FindingsIdCounter;
                                F.Plugin = PluginName;
                                AllFindings[LogId].Add(F);
                            }
                        }
                    }
                }
                catch(Exception Exp)
                {
                    IronException.Report(string.Format("Error running Passive Plugin '{0}' on {1} log with id {2}", PluginName, this.LogSource, LogId), Exp);
                }
            }
            if (AllFindings.ContainsKey(LogId))
                AddPassivePluginRowInfo(LogId);
            return true;
        }

        bool DoParameterTestOnCurrentIndex(int LogId, Session Sess)
        {
            try
            {
                Request TestRequest = GetTestRequest(Sess.Request);
                if (TestRequest == null)
                    return true;
                string OriginalResponseString = "";
                if (Sess.Response != null)
                {
                    SetOriginalSessionInfo(LogId, Sess.Response.Code, Sess.Response.BodyLength);
                    OriginalResponseString = Sess.Response.ToString();
                }
                else
                {
                    SetOriginalSessionInfo(LogId, 0, 0);
                }
                try
                {
                    string ResentResponseString = "";
                    if (ShouldResendOriginalRequest)
                    {
                        Response ResentResponse = SendRequest(Sess.Request);
                        ResentResponseString = ResentResponse.ToString();
                        int OriginalVsResentDiffLevel = Tools.DiffLevel(OriginalResponseString, ResentResponseString);
                        SetResentSessionInfo(LogId, ResentResponse.ID, ResentResponse.Code, ResentResponse.BodyLength, string.Format("{0}%", OriginalVsResentDiffLevel));
                    }

                    Response TestResponse = SendRequest(TestRequest);
                    string TestResponseString = TestResponse.ToString();
                    int OriginalVsTestDiffLevel = Tools.DiffLevel(OriginalResponseString, TestResponseString);
                    int ResentVsTestDiffLevel = Tools.DiffLevel(ResentResponseString, TestResponseString);
                    SetTestSessionInfo(LogId, TestResponse.ID, TestResponse.Code, TestResponse.BodyLength, string.Format("{0}%", OriginalVsTestDiffLevel), string.Format("{0}%", ResentVsTestDiffLevel));
                }
                catch (ThreadAbortException) { }
                catch (Exception Exp)
                {
                    ShowStatus("Test stopped, encountered error");
                    IronException.Report(string.Format("Error Testing {0} Log - {1}", LogSource, LogId), Exp);
                    return false;
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp)
            {
                ShowStatus("Test stopped, encountered error");
                IronException.Report(string.Format("Error Reading {0} Log - {1}", LogSource, LogId), Exp);
                return false;
            }
            return true;
        }

        Response SendRequest(Request Req)
        {
            if(SessionHandler != null && SessionHandler.Name.Length > 0)
                Req = SessionHandler.DoBeforeSending(Req, null);
            Req.SetSource(TesterLogSourceAttributeValue);
            return Req.Send();
        }

        Request GetTestRequest(Request Req)
        {
            Request TestRequest = Req.GetClone();
            switch (TestType)
            {
                case(AddParameterTest):
                    return AddParameter(TestRequest);
                case(EditParameterTest):
                    return EditParameter(TestRequest);
                case(DeleteParameterTest):
                    return DeleteParameter(TestRequest);
            }
            return TestRequest;
        }

        Request AddParameter(Request Req)
        {
            try
            {
                switch (ParameterSection)
                {
                    case (UrlPartPartSection):
                        List<string> UrlPathParts = Req.UrlPathParts;
                        UrlPathParts.Insert(UrlPathPartPosition, ParameterValue);
                        Req.UrlPathParts = UrlPathParts;
                        break;
                    case (QuerySection):
                        Req.Query.Add(ParameterName, ParameterValue);
                        break;
                    case (NormalBodySection):
                        Req.Body.Add(ParameterName, ParameterValue);
                        break;
                    case (OtherTypeBodySection):
                        break;
                    case (CookieSection):
                        Req.Cookie.Add(ParameterName, ParameterValue);
                        break;
                    case (HeaderSection):
                        Req.Headers.Add(ParameterName, ParameterValue);
                        break;
                }
            }
            catch { }
            return Req;
        }

        Request EditParameter(Request Req)
        {
            try
            {
                switch (ParameterSection)
                {
                    case (UrlPartPartSection):
                        List<string> UrlPathParts = Req.UrlPathParts;
                        UrlPathParts[UrlPathPartPosition] = ParameterValue;
                        Req.UrlPathParts = UrlPathParts;
                        break;
                    case (QuerySection):
                        Req.Query.Set(ParameterName, ParameterValue);
                        break;
                    case (NormalBodySection):
                        Req.Body.Set(ParameterName, ParameterValue);
                        break;
                    case (OtherTypeBodySection):
                        break;
                    case (CookieSection):
                        Req.Cookie.Set(ParameterName, ParameterValue);
                        break;
                    case (HeaderSection):
                        Req.Headers.Set(ParameterName, ParameterValue);
                        break;
                }
            }
            catch { }
            return Req;
        }

        Request DeleteParameter(Request Req)
        {
            try
            {
                switch (ParameterSection)
                {
                    case (UrlPartPartSection):
                        List<string> UrlPathParts = Req.UrlPathParts;
                        if (UrlPathPartPosition < UrlPathParts.Count)
                        {
                            UrlPathParts.RemoveAt(UrlPathPartPosition);
                            Req.UrlPathParts = UrlPathParts;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    case (QuerySection):
                        if (Req.Query.Has(ParameterName))
                            Req.Query.Remove(ParameterName);
                        else
                            return null;
                        break;
                    case (NormalBodySection):
                        if (Req.Body.Has(ParameterName))
                            Req.Body.Remove(ParameterName);
                        else
                            return null;
                        break;
                    case (OtherTypeBodySection):
                        break;
                    case (CookieSection):
                        if (Req.Cookie.Has(ParameterName))
                            Req.Cookie.Remove(ParameterName);
                        else
                            return null;
                        break;
                    case (HeaderSection):
                        if (Req.Headers.Has(ParameterName))
                            Req.Headers.Remove(ParameterName);
                        else
                            return null;
                        break;
                }
            }
            catch { return null; }
            return Req;
        }

        
        delegate void SetOriginalSessionInfo_d(int LogId, int Code, int Length);
        void SetOriginalSessionInfo(int LogId, int Code, int Length)
        {
            if (ParameterManipulationResultsLogGrid.InvokeRequired)
            {
                SetOriginalSessionInfo_d CALL_d = new SetOriginalSessionInfo_d(SetOriginalSessionInfo);
                ParameterManipulationResultsLogGrid.Invoke(CALL_d, new object[] { LogId, Code, Length });
            }
            else
            {
                bool AlreadyExists = false;
                foreach (DataGridViewRow Row in ParameterManipulationResultsLogGrid.Rows)
                {
                    if (LogId == (int)Row.Cells[0].Value) 
                    {
                        AlreadyExists = true;
                        break;
                    }
                }
                if(!AlreadyExists)
                    ParameterManipulationResultsLogGrid.Rows.Add(new object[]{LogId, Code, Length});
            }
        }
        delegate void SetResentSessionInfo_d(int LogId, int ResentLogId, int Code, int Length, string OriginalVsResent);
        void SetResentSessionInfo(int LogId, int ResentLogId, int Code, int Length, string OriginalVsResent)
        {
            if (ParameterManipulationResultsLogGrid.InvokeRequired)
            {
                SetResentSessionInfo_d CALL_d = new SetResentSessionInfo_d(SetResentSessionInfo);
                ParameterManipulationResultsLogGrid.Invoke(CALL_d, new object[] { LogId, ResentLogId, Code, Length, OriginalVsResent });
            }
            else
            {
                foreach (DataGridViewRow Row in ParameterManipulationResultsLogGrid.Rows)
                {
                    if (LogId == (int)Row.Cells[0].Value)
                    {
                        Row.Cells["ResentIdClmn"].Value = ResentLogId;
                        Row.Cells["ResentCodeClmn"].Value = Code;
                        Row.Cells["ResentLengthClmn"].Value = Length;
                        Row.Cells["OriginalVsResentClmn"].Value = OriginalVsResent;
                        return;
                    }
                }
            }
        }
        delegate void SetTestSessionInfo_d(int LogId, int TestLogId, int Code, int Length, string OriginalVsTest, string ResentVsTest);
        void SetTestSessionInfo(int LogId, int TestLogId, int Code, int Length, string OriginalVsTest, string ResentVsTest)
        {
            if (ParameterManipulationResultsLogGrid.InvokeRequired)
            {
                SetTestSessionInfo_d CALL_d = new SetTestSessionInfo_d(SetTestSessionInfo);
                ParameterManipulationResultsLogGrid.Invoke(CALL_d, new object[] { LogId, TestLogId, Code, Length, OriginalVsTest, ResentVsTest });
            }
            else
            {
                foreach (DataGridViewRow Row in ParameterManipulationResultsLogGrid.Rows)
                {
                    if (LogId == (int)Row.Cells[0].Value)
                    {
                        Row.Cells["TestIdClmn"].Value = TestLogId;
                        Row.Cells["TestCodeClmn"].Value = Code;
                        Row.Cells["TestLengthClmn"].Value = Length;
                        Row.Cells["OriginalVsTestClmn"].Value = OriginalVsTest;
                        Row.Cells["ResentVsTestClmn"].Value = ResentVsTest;
                        return;
                    }
                }
            }
        }

        delegate void EndLogLoad_d();
        void EndLogLoad()
        {
            if (ParameterManipulationResultsLogGrid.InvokeRequired)
            {
                EndLogLoad_d CALL_d = new EndLogLoad_d(EndLogLoad);
                ParameterManipulationResultsLogGrid.Invoke(CALL_d, new object[] { });
            }
            else
            {
                ParameterManipulationLoadProgressBar.Visible = false;

                ParameterManipulationResultsBottomTabs.Visible = true;
            }
        }

       
        delegate void ShowStatus_d(string Status);
        void ShowStatus(string Status)
        {
            if (ParameterManipulationResultsStatusLbl.InvokeRequired)
            {
                ShowStatus_d CALL_d = new ShowStatus_d(ShowStatus);
                ParameterManipulationResultsStatusLbl.Invoke(CALL_d, new object[] { Status });
            }
            else
            {
                ParameterManipulationResultsStatusLbl.Text = Status;
            }
        }

        delegate void AddPassivePluginRowInfo_d(int LogId);
        void AddPassivePluginRowInfo(int LogId)
        {
            if (PassivePluginsScanResultsGrid.InvokeRequired)
            {
                AddPassivePluginRowInfo_d CALL_d = new AddPassivePluginRowInfo_d(AddPassivePluginRowInfo);
                PassivePluginsScanResultsGrid.Invoke(CALL_d, new object[] { LogId });
            }
            else
            {
                Findings LogFindings = this.AllFindings[LogId];

                List<string> HighVulnTitles = new List<string>();
                List<string> MediumVulnTitles = new List<string>();
                List<string> LowVulnTitles = new List<string>();
                List<string> InfoTitles = new List<string>();
                List<string> TestLeadsTitles = new List<string>();

                foreach (Finding F in LogFindings.GetAll())
                {
                    switch(F.Type)
                    {
                        case(FindingType.Vulnerability):
                            switch(F.Severity)
                            {
                                case(FindingSeverity.High):
                                    HighVulnTitles.Add(F.Title);
                                    break;
                                case(FindingSeverity.Medium):
                                    MediumVulnTitles.Add(F.Title);
                                    break;
                                case(FindingSeverity.Low):
                                    LowVulnTitles.Add(F.Title);
                                    break;
                            }
                            break;
                        case(FindingType.Information):
                            InfoTitles.Add(F.Title);
                            break;
                        case (FindingType.TestLead):
                            TestLeadsTitles.Add(F.Title);
                            break;
                    }
                }

                StringBuilder Titles = new StringBuilder();
                if (HighVulnTitles.Count > 0)
                {
                    Titles.Append(string.Join(",", HighVulnTitles.ToArray()));
                }
                if (MediumVulnTitles.Count > 0)
                {
                    if (Titles.Length > 0) Titles.Append(",");
                    Titles.Append(string.Join(",", MediumVulnTitles.ToArray()));
                }
                if (LowVulnTitles.Count > 0)
                {
                    if (Titles.Length > 0) Titles.Append(",");
                    Titles.Append(string.Join(",", LowVulnTitles.ToArray()));
                }
                if (TestLeadsTitles.Count > 0)
                {
                    if (Titles.Length > 0) Titles.Append(",");
                    Titles.Append(string.Join(",", TestLeadsTitles.ToArray()));
                }
                if (InfoTitles.Count > 0)
                {
                    if (Titles.Length > 0) Titles.Append(",");
                    Titles.Append(string.Join(",", InfoTitles.ToArray()));
                }

                PassivePluginsScanResultsGrid.Rows.Add(new object[]{ LogId, HighVulnTitles.Count + MediumVulnTitles.Count + LowVulnTitles.Count, HighVulnTitles.Count, MediumVulnTitles.Count, LowVulnTitles.Count, InfoTitles.Count, TestLeadsTitles.Count, Titles.ToString() });
            }
        }

        public void PauseTest()
        {
            this.DoTest = false;
            try
            {
                TestThread.Abort();
            }
            catch { }
            this.CurrentIndex = this.CurrentIndex - 1;
            ShowStatus("Test paused");
            
        }

        public void StopTest()
        {
            this.DoTest = false;
            try
            {
                TestThread.Abort();
            }
            catch { }
            this.CurrentIndex = 0;
            ShowStatus("Test stopped");
        }

        private void StartTestBtn_Click(object sender, EventArgs e)
        {
            if (ReadTestSettings())
            {
                ShowStep0Status("");
                if (TestThread != null)
                {
                    try
                    {
                        TestThread.Abort();
                    }
                    catch { }
                }
                TestThread = new Thread(StartTest);
                TestThread.Start();
                CurrentStep = 2;
                if(this.TestType.Equals(PassivePluginTest))
                    ResultsBaseTabs.SelectTab("PassiveChecksResultsTab");
                else
                    ResultsBaseTabs.SelectTab("ParameterManipulationResultsTab");
                BaseTabs.SelectTab("ResultsTab");
                PlayBtn.Enabled = false;
                PauseBtn.Enabled = true;
                StopBtn.Enabled = true;
            }
        }

        bool ReadTestSettings()
        {
            if (this.TestType.Equals(PassivePluginTest))
            {
                this.PassivePluginsToRun.Clear();
                foreach (DataGridViewRow Row in PassiveScanPluginsGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value) PassivePluginsToRun.Add(Row.Cells[1].Value.ToString());
                }
                if (this.PassivePluginsToRun.Count == 0)
                {
                    ShowStep1Error("No plugins selected, select atleast one plugin");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                ParameterSection = ParameterTypeCombo.Text;
                ParameterName = ParameterNameTB.Text.Trim();
                ParameterValue = ParameterValueTB.Text.Trim();
                if (ParameterTypeCombo.SelectedIndex < 0)
                {
                    ShowStep1Error("Parameter Section is not selected");
                    return false;
                }
                if (ParameterName.Length == 0)
                {
                    ShowStep1Error("Parameter name cannot be blank");
                    return false;
                }
                if (ParameterTypeCombo.SelectedIndex == 0)
                {
                    try
                    {
                        Int32.Parse(ParameterName);
                    }
                    catch
                    {
                        ShowStep1Error("Parameter Index must be a number");
                        return false;
                    }
                }
                if (SessionPluginsCombo.Text.Trim().Length > 0)
                {
                    if (SessionPlugin.List().Contains(SessionPluginsCombo.Text))
                    {
                        SessionHandler = SessionPlugin.Get(SessionPluginsCombo.Text);
                    }
                    else
                    {
                        ShowStep1Error("Session Plugin name is invalid. Either select an existing plugin from drop-down or leave this field blank.");
                        return false;
                    }
                }
            }
            return true;
        }

        private void StepOneNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep0Status("");
            if (ParameterAddTestRB.Checked)
            {
                this.TestType = AddParameterTest;
                ParameterActionLbl.Text = "Parameter to Add:";
                ParameterValueTBLbl.Visible = true;
                ParameterValueTB.Visible = true;
                ConfigureTestTabs.SelectTab("ConfigureParameterTab");
            }
            else if (ParameterEditTestRB.Checked)
            {
                this.TestType = EditParameterTest;
                ParameterActionLbl.Text = "Parameter to Edit:";
                ParameterValueTBLbl.Visible = true;
                ParameterValueTB.Visible = true;
                ConfigureTestTabs.SelectTab("ConfigureParameterTab");
            }
            else if (ParameterDeleteTestRB.Checked)
            {
                this.TestType = DeleteParameterTest;
                ParameterActionLbl.Text = "Parameter to Delete:";
                ParameterValueTBLbl.Visible = false;
                ParameterValueTB.Visible = false;
                ConfigureTestTabs.SelectTab("ConfigureParameterTab");
            }
            else if (RunPassivePluginsRB.Checked)
            {
                this.TestType = PassivePluginTest;
                ConfigureTestTabs.SelectTab("SelectPluginsTab");
                PassiveScanPluginsGrid.Rows.Clear();
                SelectAllPassivePluginsCB.Checked = false;
                foreach (string PluginName in PassivePlugin.List())
                {
                    if(PassivePlugin.Get(PluginName).CallingState == PluginCallingState.Offline)
                        PassiveScanPluginsGrid.Rows.Add(new object[]{false, PluginName});
                }
                ConfigureTestTabs.SelectTab("SelectPluginsTab");
            }
            else
            {
                ShowStep0Error("Atleast one option must be selected");
                return;
            }
            CurrentStep = 1;
            BaseTabs.SelectTab("ConfigureTab");
        }

        void ShowStep0Status(string Text)
        {
            this.Step0StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step0StatusTB.Visible = false;
            }
            else
            {
                this.Step0StatusTB.ForeColor = Color.Black;
                this.Step0StatusTB.Visible = true;
            }
        }
        void ShowStep0Error(string Text)
        {
            this.Step0StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step0StatusTB.Visible = false;
            }
            else
            {
                this.Step0StatusTB.ForeColor = Color.Red;
                this.Step0StatusTB.Visible = true;
            }
        }

        void ShowStep1Status(string Text)
        {
            this.Step1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step1StatusTB.Visible = false;
            }
            else
            {
                this.Step1StatusTB.ForeColor = Color.Black;
                this.Step1StatusTB.Visible = true;
            }
        }
        void ShowStep1Error(string Text)
        {
            this.Step1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step1StatusTB.Visible = false;
            }
            else
            {
                this.Step1StatusTB.ForeColor = Color.Red;
                this.Step1StatusTB.Visible = true;
            }
        }

        private void ParameterTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParameterTypeCombo.SelectedIndex == 0)
                ParameterNameTBLbl.Text = "Parameter Index:";
            else
                ParameterNameTBLbl.Text = "Parameter Name:";
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            CurrentStep = 0;
            BaseTabs.SelectTab("SelectTestTab");
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            if (TestThread != null)
            {
                try
                {
                    TestThread.Abort();
                }
                catch { }
            }
            if (this.CurrentIndex == 0)
            {
                ParameterManipulationResultsLogGrid.Rows.Clear();
                PassivePluginsScanResultsGrid.Rows.Clear();
            }
            TestThread = new Thread(StartTest);
            TestThread.Start();
            PlayBtn.Enabled = false;
            PauseBtn.Enabled = true;
            StopBtn.Enabled = true;
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            PauseTest();
            PlayBtn.Enabled = true;
            PauseBtn.Enabled = false;
            StopBtn.Enabled = true;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopTest();
            PlayBtn.Enabled = true;
            PauseBtn.Enabled = false;
            StopBtn.Enabled = false;
        }

        private void LogsTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopTest();
        }

        private void ResultsBaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.TestType.Equals(PassivePluginTest))
            {
                if (!ResultsBaseTabs.SelectedTab.Name.Equals("PassiveChecksResultsTab"))
                    ResultsBaseTabs.SelectTab("PassiveChecksResultsTab");
            }
            else
            {
                if (!ResultsBaseTabs.SelectedTab.Name.Equals("ParameterManipulationResultsTab"))
                    ResultsBaseTabs.SelectTab("ParameterManipulationResultsTab");
            }
        }

        private void ConfigureTestTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.TestType.Equals(PassivePluginTest))
            {
                if(!ConfigureTestTabs.SelectedTab.Name.Equals("SelectPluginsTab"))
                    ConfigureTestTabs.SelectTab("SelectPluginsTab");
            }
            else
            {
                if (!ConfigureTestTabs.SelectedTab.Name.Equals("ConfigureParameterTab"))
                    ConfigureTestTabs.SelectTab("ConfigureParameterTab");
            }
        }

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (BaseTabs.SelectedIndex != CurrentStep)
                BaseTabs.SelectTab(CurrentStep);
        }

        private void ParameterTypeCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void ShouldResentOriginalRequestCB_CheckedChanged(object sender, EventArgs e)
        {
            this.ShouldResendOriginalRequest = ShouldResentOriginalRequestCB.Checked;
        }

        private void PassiveScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PassiveScanPluginsGrid.SelectedRows == null) return;
            if (PassiveScanPluginsGrid.SelectedRows.Count == 0) return;
            PassiveScanPluginsGrid.SelectedRows[0].Cells[0].Value = !((bool)PassiveScanPluginsGrid.SelectedRows[0].Cells[0].Value);
            if (!(bool)PassiveScanPluginsGrid.SelectedRows[0].Cells[0].Value) SelectAllPassivePluginsCB.Checked = false;
        }

        private void SelectAllPassivePluginsCB_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in PassiveScanPluginsGrid.Rows)
            {
                Row.Cells[0].Value = SelectAllPassivePluginsCB.Checked;
            }
        }

        private void PassivePluginsScanResultsGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (PassivePluginsScanResultsGrid.SelectedCells == null || PassivePluginsScanResultsGrid.SelectedCells.Count == 0) return;
            try
            {
                int LogId = (int)PassivePluginsScanResultsGrid.SelectedRows[0].Cells[0].Value;
                Findings Fs = AllFindings[LogId];
                FindingsTree.Nodes.Clear();
                PassivePluginLogRequestView.ClearRequest();
                PassivePluginLogResponseView.ClearResponse();
                PassivePluginScanResultsBottomTabs.SelectTab(0);
                
                TreeNode RootNode = FindingsTree.Nodes.Add(string.Format("LogId-{0}", LogId), "Click finding to view details");
                TreeNode VulnNode = RootNode.Nodes.Add("Vulnerabilities", "Vulnerabilities");
                TreeNode HighVulnNode = VulnNode.Nodes.Add("High", "High");
                HighVulnNode.ForeColor = Color.Red;
                TreeNode MediumVulnNode = VulnNode.Nodes.Add("Medium", "Medium");
                MediumVulnNode.ForeColor = Color.Orange;
                TreeNode LowVulnNode = VulnNode.Nodes.Add("Low", "Low");
                LowVulnNode.ForeColor = Color.SteelBlue;
                TreeNode InfoNode = RootNode.Nodes.Add("Information", "Information");
                TreeNode TestLeadsNode = RootNode.Nodes.Add("TestLeads", "TestLeads");
                
                int FindingCounter = 0;
                foreach (Finding F in Fs.GetAll())
                {
                    FindingCounter++;
                    switch (F.Type)
                    {
                        case(FindingType.Vulnerability):
                            switch (F.Severity)
                            {
                                case(FindingSeverity.High):
                                    HighVulnNode.Nodes.Add(F.Id.ToString(), F.Title);
                                    break;
                                case (FindingSeverity.Medium):
                                    MediumVulnNode.Nodes.Add(F.Id.ToString(), F.Title);
                                    break;
                                case (FindingSeverity.Low):
                                    LowVulnNode.Nodes.Add(F.Id.ToString(), F.Title);
                                    break;
                            }
                            break;
                        case (FindingType.Information):
                            InfoNode.Nodes.Add(F.Id.ToString(), F.Title);
                            break;
                        case (FindingType.TestLead):
                            TestLeadsNode.Nodes.Add(F.Id.ToString(), F.Title);
                            break;
                    }
                }
                RootNode.ExpandAll();
                ResultsDisplayRTB.Text = "";
            }
            catch { }
        }

        private void FindingsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode Node = FindingsTree.SelectedNode;
                if (Node == null) return;
                if (Node.Level == 3 || (Node.Level == 2 && (Node.Parent.Index == 1 || Node.Parent.Index == 2)))
                {
                    ResultsDisplayRTB.Text = "";
                    int LogId = Int32.Parse(FindingsTree.Nodes[0].Name.Replace("LogId-", ""));
                    int FindingId = Int32.Parse(Node.Name);
                    Findings LogFindings = AllFindings[LogId];
                    foreach (Finding PR in LogFindings.GetAll())
                    {
                        if (PR.Id == FindingId)
                        {
                            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
                            SB.Append(@" \b \fs30"); SB.Append(Tools.RtfSafe(PR.Title)); SB.Append(@"\b0  \fs20  \par  \par");
                            SB.Append(@" \cf1 \b Plugin: \b0 \cf0 "); SB.AppendLine(Tools.RtfSafe(PR.Plugin)); SB.Append(@" \par");
                            if (PR.Type == FindingType.Vulnerability)
                            {
                                SB.Append(@" \cf1 \b Severity: \b0 \cf0 "); SB.AppendLine(Tools.RtfSafe(PR.Severity.ToString())); SB.Append(@" \par");
                                SB.Append(@" \cf1 \b Confidence: \b0 \cf0 "); SB.AppendLine(Tools.RtfSafe(PR.Confidence.ToString())); SB.Append(@" \par");
                            }
                            SB.Append(@" \par");
                            SB.Append(@" \cf1 \b Summary: \b0 \cf0  \par ");
                            SB.AppendLine(Tools.RtfSafe(PR.Summary));
                            SB.Append(@" \par \par");
                            ResultsDisplayRTB.Rtf = SB.ToString();
                        }
                    }
                }
            }
            catch { }
        }

        private void PassivePluginScanResultsBottomTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (PassivePluginScanResultsBottomTabs.SelectedIndex == 1)
            {
                if (FindingsTree.Nodes.Count > 0)
                {
                    if (PassivePluginLogRequestView.GetRequest() == null)
                    {
                        try
                        {
                            int LogId = Int32.Parse(FindingsTree.Nodes[0].Name.Replace("LogId-", ""));
                            if (LogLoadThread != null)
                            {
                                try
                                {
                                    LogLoadThread.Abort();
                                }
                                catch { }
                            }
                            LogLoadThread = new Thread(LoadPassivePluginResultLog);
                            LogLoadThread.Start(LogId);
                            PassivePluginLogTabs.Visible = false;
                            PassivePluginLogLoadProgressBar.Visible = true;
                        }
                        catch{}
                    }
                }
            }
        }

        void LoadPassivePluginResultLog(object LogIdObj)
        {
            int LogId = (int)LogIdObj;
            try
            {
                Session Sess = Session.FromLog(LogId, this.LogSource);
                PassivePluginLogRequestView.SetRequest(Sess.Request);
                if(Sess.Response != null)
                    PassivePluginLogResponseView.SetResponse(Sess.Response, Sess.Request);
            }
            catch (Exception Exp)
            { 
                IronException.Report(string.Format("Unable to load {0} log id - {1}", this.LogSource, LogId), Exp); 
            }
            EndPassivePluginLogLoad();
        }

        delegate void EndPassivePluginLogLoad_d();
        void EndPassivePluginLogLoad()
        {
            if (PassivePluginLogTabs.InvokeRequired)
            {
                EndPassivePluginLogLoad_d CALL_d = new EndPassivePluginLogLoad_d(EndPassivePluginLogLoad);
                PassivePluginLogTabs.Invoke(CALL_d, new object[] { });
            }
            else
            {
                PassivePluginLogTabs.Visible = true;
                PassivePluginLogLoadProgressBar.Visible = false;
            }
        }

        private void RefreshSessListLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SessionPluginsCombo.Items.Clear();
            SessionPluginsCombo.Items.AddRange(SessionPlugin.List().ToArray());
        }

        private void LaunchSessionPluginCreationAssistantLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SessionPluginCreationAssistant SPCA = new SessionPluginCreationAssistant();
            SPCA.Show();
        }

        private void LogsTester_Load(object sender, EventArgs e)
        {
            SessionPluginsCombo.Items.Clear();
            SessionPluginsCombo.Items.AddRange(SessionPlugin.List().ToArray());
        }
    }
}
