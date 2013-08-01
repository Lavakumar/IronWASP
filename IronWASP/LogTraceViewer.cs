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
    public partial class LogTraceViewer : Form
    {
        Thread LogLoadThread;
        Thread AnalysisThread;

        int SelectedRowsCount = 0;

        int TraceId = 0;
        IronTrace Trace;
        ScanTraceBehaviourAnalysisResultsUiInformation UiResults;

        int BaselineLogId = 0;
        Session BaselineSession;

        internal LogTraceViewer(int TraceId)
        {
            this.TraceId = TraceId;
            InitializeComponent();
        }

        internal LogTraceViewer(IronTrace Trace)
        {
            this.Trace = Trace;
            InitializeComponent();
        }

        internal LogTraceViewer(int TraceId, ScanTraceBehaviourAnalysisResultsUiInformation UiResults)
        {
            this.TraceId = TraceId;
            this.UiResults = UiResults;
            InitializeComponent();
        }

        private void ScanTraceOverviewGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanTraceOverviewGrid.SelectedRows == null) return;
            if (ScanTraceOverviewGrid.SelectedRows.Count == 0) return;
            if (ScanTraceOverviewGrid.SelectedRows[0].Cells[2].Value == null) return;
                
            if (e.ColumnIndex == 0)// ClickActionSelectLogRB.Checked)
            {
                ScanTraceOverviewGrid.SelectedRows[0].Cells[0].Value = !((bool)ScanTraceOverviewGrid.SelectedRows[0].Cells[0].Value);
                if ((bool)ScanTraceOverviewGrid.SelectedRows[0].Cells[0].Value)
                {
                    SelectedRowsCount++;
                }
                else
                {
                    SelectedRowsCount--;
                }
                if (SelectedRowsCount > 0)
                {
                    DoDiffBtn.Enabled = true;
                }
                else
                {
                    DoDiffBtn.Enabled = false;
                }
            }
            else
            {
                try
                {
                    int LogId = Int32.Parse(ScanTraceOverviewGrid.SelectedCells[2].Value.ToString());
                    if (BaselineLogId == 0)
                    {
                        if (ScanTraceOverviewGrid.Rows.Count > 0 && ScanTraceOverviewGrid.Rows[0].Cells[2].Value != null)
                        {
                            BaselineLogId = Int32.Parse(ScanTraceOverviewGrid.Rows[0].Cells[2].Value.ToString());
                        }
                    }
                    ShowSelectedLog(LogId);
                }
                catch { }
            }
        }
        void ShowSelectedLog(int LogId)
        {
            LogDisplayTabs.Visible = false;
            LoadLogProgressBar.Visible = true;

            RequestView.ClearRequest();
            ResponseView.ClearResponse();
            RequestDRV.ClearDiffResults();
            ResponseDRV.ClearDiffResults();

            if (LogLoadThread != null)
            {
                try
                {
                    LogLoadThread.Abort();
                }
                catch { }
            }
            LogLoadThread = new Thread(ShowSelectedLog);
            LogLoadThread.Start(LogId);
        }

        void ShowSelectedLog(object SelectedLogInfoObject)
        {
            int LogId = (int)SelectedLogInfoObject;
            bool EnableBtn = false;
            string ScriptCode = "";

            try
            {
                Session Session = Session.FromScanLog(LogId);
                if (BaselineSession == null && BaselineLogId > 0)
                {
                    BaselineSession = Session.FromScanLog(BaselineLogId);
                }

                StringBuilder SB = new StringBuilder("<i<br>>To access the selected Request in the IronWASP Scripting shell for fuzzing or testing use the following code:<i<br>>");
                SB.Append(string.Format("<i<br>><i<hh>>Python:<i</hh>><i<br>>req = <i<cg>>Request<i</cg>>.<i<cb>>FromScanLog<i</cb>>({0})", LogId));
                SB.Append(string.Format("<i<br>><i<br>><i<hh>>Ruby:<i</hh>><i<br>>req = <i<cg>>Request<i</cg>>.<i<cb>>from_scan_log<i</cb>>({0})", LogId));
                ScriptCode = SB.ToString();

                SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;\red255\green255\blue255;}");
                SB.Append(Tools.RtfSafe(ScriptCode));
                ScriptCode = SB.ToString();
                ScriptCode = ScriptCode.Replace(" . ", ".").Replace(" (", "(").Replace("Request ", "Request").Replace(" From", "From").Replace(" from", "from").Replace("Log ", "Log").Replace("log ", "log");

                string RequestStr = "";
                string ResponseStr = "";
                string BaselineRequestStr = "";
                string BaselineResponseStr = "";

                if (Session.Request != null)
                {
                    RequestStr = Session.Request.ToString();
                    RequestView.SetRequest(Session.Request);
                    EnableBtn = true;

                    if (Session.Response != null)
                    {
                        ResponseStr = Session.Response.ToString();
                        ResponseView.SetResponse(Session.Response, Session.Request);
                    }
                }
                if (BaselineSession != null)
                {
                    if (BaselineSession.Request != null)
                    {
                        BaselineRequestStr = BaselineSession.Request.ToString();
                        if (BaselineSession.Response != null)
                        {
                            BaselineResponseStr = BaselineSession.Response.ToString();
                        }
                    }
                }


                string[] RequestSidebySideResults = DiffWindow.DoSideBySideDiff(BaselineRequestStr, RequestStr);
                string[] ResponseSidebySideResults = DiffWindow.DoSideBySideDiff(BaselineResponseStr, ResponseStr);

                string RequestSinglePageResults = DiffWindow.DoSinglePageDiff(BaselineRequestStr, RequestStr);
                string ResponseSinglePageResults = DiffWindow.DoSinglePageDiff(BaselineResponseStr, ResponseStr);

                RequestDRV.ShowDiffResults(RequestSinglePageResults, RequestSidebySideResults[0], RequestSidebySideResults[1]);
                ResponseDRV.ShowDiffResults(ResponseSinglePageResults, ResponseSidebySideResults[0], ResponseSidebySideResults[1]);

            }
            catch (ThreadAbortException) { }
            catch (Exception Exp) { IronException.Report("Error loading Selected Log info in Scan Trace Viewer", Exp); }
            finally
            {
                EndLogLoad(EnableBtn, ScriptCode);
            }
        }

        delegate void EndLogLoad_d(bool EnableBtn, string ScriptRtf);
        void EndLogLoad(bool EnableBtn, string ScriptRtf)
        {
            if (ScanTraceOverviewGrid.InvokeRequired)
            {
                EndLogLoad_d CALL_d = new EndLogLoad_d(EndLogLoad);
                ScanTraceOverviewGrid.Invoke(CALL_d, new object[] { EnableBtn, ScriptRtf });
            }
            else
            {
                LoadLogProgressBar.Visible = false;
                LogDisplayTabs.Visible = true;
                ManualTestRequestBtn.Enabled = EnableBtn;
                ScriptedTestRequestBtn.Rtf = ScriptRtf;
            }
        }

        private void LogTraceViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                LogLoadThread.Abort();
            }
            catch { }
            try
            {
                AnalysisThread.Abort();
            }
            catch { }
        }

        private void DoDiffBtn_Click(object sender, EventArgs e)
        {
            if (SelectedRowsCount == 2)
            {
                int ALogId = -1;
                int BLogId = -1;
                foreach (DataGridViewRow Row in ScanTraceOverviewGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        if (ALogId == -1)
                        {
                            try
                            {
                                ALogId = Int32.Parse(Row.Cells[2].Value.ToString());
                            }
                            catch { }
                        }
                        else if (BLogId == -1)
                        {
                            try
                            {
                                BLogId = Int32.Parse(Row.Cells[2].Value.ToString());
                            }
                            catch { }
                            break;
                        }
                    }
                }
                SessionsDiffer Sdiff = new SessionsDiffer();
                Sdiff.SetSessions("Scan", ALogId, BLogId);
                Sdiff.Show();
            }
            else
            {
                MessageBox.Show(string.Format("Diff can be done only when two sessions are selected. You have selected {0} sessions", SelectedRowsCount), "Selection Error");
            }
        }

        private void CodeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CodeGrid.SelectedRows == null) return;
            if (CodeGrid.SelectedRows.Count == 0) return;
            int LogId = Int32.Parse(CodeGrid.SelectedCells[0].Value.ToString());
            ShowSelectedLog(LogId);
        }

        private void KeywordsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (KeywordsGrid.SelectedRows == null) return;
            if (KeywordsGrid.SelectedRows.Count == 0) return;
            int LogId = Int32.Parse(KeywordsGrid.SelectedCells[0].Value.ToString());
            ShowSelectedLog(LogId);
        }

        private void BodyGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BodyGrid.SelectedRows == null) return;
            if (BodyGrid.SelectedRows.Count == 0) return;
            int LogId = Int32.Parse(BodyGrid.SelectedCells[0].Value.ToString());
            ShowSelectedLog(LogId);
        }

        private void SetCookieGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SetCookieGrid.SelectedRows == null) return;
            if (SetCookieGrid.SelectedRows.Count == 0) return;
            int LogId = Int32.Parse(SetCookieGrid.SelectedCells[0].Value.ToString());
            ShowSelectedLog(LogId);
        }

        private void HeadersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (HeadersGrid.SelectedRows == null) return;
            if (HeadersGrid.SelectedRows.Count == 0) return;
            int LogId = Int32.Parse(HeadersGrid.SelectedCells[0].Value.ToString());
            ShowSelectedLog(LogId);
        }

        private void RoundtripGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RoundtripGrid.SelectedRows == null) return;
            if (RoundtripGrid.SelectedRows.Count == 0) return;
            int LogId = Int32.Parse(RoundtripGrid.SelectedCells[0].Value.ToString());
            ShowSelectedLog(LogId);
        }

        delegate void SetAnalysisUiResults_d(ScanTraceBehaviourAnalysisResultsUiInformation UiResults);
        void SetAnalysisUiResults(ScanTraceBehaviourAnalysisResultsUiInformation UiResults)
        {
            if (this.PayloadEffectTabs.InvokeRequired)
            {
                SetAnalysisUiResults_d CALL_d = new SetAnalysisUiResults_d(SetAnalysisUiResults);
                this.PayloadEffectTabs.Invoke(CALL_d, new object[] { UiResults });
            }
            else
            {
                StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;\red255\green255\blue255;}");
                SB.Append(Tools.RtfSafe(UiResults.SummaryText));

                this.SummaryRTB.Rtf = SB.ToString();

                if (UiResults.CodeGridRows.Count > 0)
                {
                    this.PayloadEffectTabs.TabPages["CodeTab"].Text = "  Code Variation  ";
                    foreach (object[] Row in UiResults.CodeGridRows)
                    {
                        this.CodeGrid.Rows.Add(Row);
                    }
                }
                else
                {
                    this.PayloadEffectTabs.TabPages["CodeTab"].Text = "  -  ";
                }
                if (UiResults.TimeGridRows.Count > 0)
                {
                    this.PayloadEffectTabs.TabPages["TimeTab"].Text = "  Time Variation  ";
                    foreach (object[] Row in UiResults.TimeGridRows)
                    {
                        this.RoundtripGrid.Rows.Add(Row);
                    }
                }
                else
                {
                    this.PayloadEffectTabs.TabPages["TimeTab"].Text = "  -  ";
                }
                if (UiResults.KeywordGridRows.Count > 0)
                {
                    this.PayloadEffectTabs.TabPages["KeywordsTab"].Text = "  Keywords Inserted  ";
                    foreach (object[] Row in UiResults.KeywordGridRows)
                    {
                        this.KeywordsGrid.Rows.Add(Row);
                    }
                }
                else
                {
                    this.PayloadEffectTabs.TabPages["KeywordsTab"].Text = "  -  ";
                }
                if (UiResults.BodyGridRows.Count > 0)
                {
                    this.PayloadEffectTabs.TabPages["BodyTab"].Text = "  Body Variation  ";
                    foreach (object[] Row in UiResults.BodyGridRows)
                    {
                        this.BodyGrid.Rows.Add(Row);
                    }
                }
                else
                {
                    this.PayloadEffectTabs.TabPages["BodyTab"].Text = "  -  ";
                }
                if (UiResults.SetCookieGridRows.Count > 0)
                {
                    this.PayloadEffectTabs.TabPages["SetCookieTab"].Text = "  Set-Cookie Variations  ";
                    foreach (object[] Row in UiResults.SetCookieGridRows)
                    {
                        this.SetCookieGrid.Rows.Add(Row);
                    }
                }
                else
                {
                    this.PayloadEffectTabs.TabPages["SetCookieTab"].Text = "  -  ";
                }
                if (UiResults.HeadersGridRows.Count > 0)
                {
                    this.PayloadEffectTabs.TabPages["HeadersTab"].Text = "  Headers Variation  ";
                    foreach (object[] Row in UiResults.HeadersGridRows)
                    {
                        this.HeadersGrid.Rows.Add(Row);
                    }
                }
                else
                {
                    this.PayloadEffectTabs.TabPages["HeadersTab"].Text = "  -  ";
                }
                this.AnalysisProgressBar.Visible = false;
                this.PayloadEffectTabs.Visible = true;
            }
        }

        delegate void SetTraceData_d(List<object[]> Rows, string Rtf);
        void SetTraceData(List<object[]> Rows, string Rtf)
        {
            if (this.ScanTraceOverviewGrid.InvokeRequired)
            {
                SetTraceData_d CALL_d = new SetTraceData_d(SetTraceData);
                this.ScanTraceOverviewGrid.Invoke(CALL_d, new object[] { Rows, Rtf });
            }
            else
            {
                foreach (object[] Row in Rows)
                {
                    string Message = Row[9].ToString();
                    string TagColor = "";
                    Dictionary<string, Color> TagColors = new Dictionary<string, Color>(){
                {"cr", Color.IndianRed},
                {"co", Color.Orange},
                {"h", Color.Orange},
                {"hh", Color.Orange},
                {"cb", Color.LightBlue},
                {"cg", Color.LightGreen},
                {"b", Color.LightGray}
                };

                    foreach (string TG in new List<string> { "cr", "co", "h", "hh", "cb", "cg", "b" })//This is used insted of TagColors.Keys to enforce color priority.
                    {
                        string OT = string.Format("<i<{0}>>", TG);
                        string CT = string.Format("<i</{0}>>", TG);
                        if (Message.Contains(OT) && Message.Contains(CT))
                        {
                            TagColor = TG;
                            break;
                        }
                    }
                    Row[9] = Tools.StripRtfTags(Message);

                    int RowId = this.ScanTraceOverviewGrid.Rows.Add(Row);
                    if (RowId == 0)
                    {
                        if (ScanTraceOverviewGrid.Rows[0].Cells[2].Value != null)
                        {
                            try
                            {
                                BaselineLogId = Int32.Parse(ScanTraceOverviewGrid.Rows[0].Cells[2].Value.ToString());
                            }
                            catch { }
                        }
                    }
                    if (TagColor.Length > 0)
                    {
                        this.ScanTraceOverviewGrid.Rows[RowId].DefaultCellStyle.BackColor = TagColors[TagColor];
                    }
                }
                this.ScanTraceMsgRTB.Rtf = Rtf;
                this.MainLoadProgressBar.Visible = false;
                this.BaseSplit.Visible = true;
            }
        }

        delegate void CloseTraceWindow_d();
        void CloseTraceWindow()
        {
            if (this.ScanTraceOverviewGrid.InvokeRequired)
            {
                CloseTraceWindow_d CALL_d = new CloseTraceWindow_d(CloseTraceWindow);
                this.ScanTraceOverviewGrid.Invoke(CALL_d, new object[] { });
            }
            else
            {
                this.Close();
            }
        }

        private void LogTraceViewer_Load(object sender, EventArgs e)
        {
            try
            {
                MainLoadProgressBar.Visible = true;
                AnalysisProgressBar.Visible = true;

                AnalysisThread = new Thread(LoadAndFill);
                AnalysisThread.Start();
            }
            catch(Exception Exp) 
            {
                IronException.Report("Error loading Scan Trace", Exp);
            }
        }

        void LoadAndFill()
        {
            if (this.Trace == null)
            {
                this.Trace = IronDB.GetScanTrace(this.TraceId);
            }

            try
            {
                List<object[]> Rows = IronTrace.GetGridRowsFromTraceAndOverviewXml(Trace.OverviewXml, Trace.MessageXml);

                StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;\red255\green255\blue255;}");
                SB.Append(Tools.RtfSafe(Trace.GetScanTracePrettyMessage()));

                this.SetTraceData(Rows, SB.ToString());
            }
            catch(Exception Exp)
            {
                IronException.Report("Error loading Scan Trace", Exp);
                this.CloseTraceWindow();
            }

            if (this.UiResults == null)
            {
                DoAnalysis();
            }
            else
            {
                SetAnalysisUiResults(this.UiResults);
            }
        }

        void DoAnalysis()
        {
            try
            {
                BehaviourAnalysis BA = new BehaviourAnalysis(ScanTraceBehaviourAnalysis.DefaultErrorKeywords, ScanTraceBehaviourAnalysis.DefaultResponseTimeChange, ScanTraceBehaviourAnalysis.DefaultResponseTimeChangeFactor, ScanTraceBehaviourAnalysis.DefaultCharsCount);
                BA.Analyze(this.Trace.OverviewXml, this.Trace.Section);
                ScanTraceBehaviourAnalysisResultsUiInformation UiReslts = ScanTraceBehaviourAnalysis.GetUiDisplayResults(BA.ResultsXml, BA.BaseLineSession.Response.Code.ToString(), BA.BaseLineRoundtripTime.ToString());
                SetAnalysisUiResults(UiReslts);
            }
            catch (Exception Exp)
            {
                IronException.Report("Error calculating Payload Effect Analysis", Exp);
                SetAnalysisUiResults(new ScanTraceBehaviourAnalysisResultsUiInformation());
            }
        }

        private void DisplayFilterLogsOnlyRB_CheckedChanged(object sender, EventArgs e)
        {
            if (DisplayFilterLogsOnlyRB.Checked)
            {
                foreach (DataGridViewRow Row in ScanTraceOverviewGrid.Rows)
                {
                    if (Row.Cells[2].Value == null) Row.Visible = false;
                }
            }
            else
            {
                foreach (DataGridViewRow Row in ScanTraceOverviewGrid.Rows)
                {
                    Row.Visible = true;
                }
            }
        }

        private void ManualTestRequestBtn_Click(object sender, EventArgs e)
        {
            NameTestGroupWizard NTGW = new NameTestGroupWizard();
            try
            {
                Request Req = RequestView.GetRequest();
                if(Req != null)
                {
                    NTGW.RequestToTest = Req;
                    NTGW.Show();
                }
            }
            catch{}
        }
    }
}
