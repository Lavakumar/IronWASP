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
    public partial class ScanTraceBehaviourAnalysis : Form
    {
        public ScanTraceBehaviourAnalysis()
        {
            InitializeComponent();
        }

        Thread AnalysisThread = null;

        int SelectedTraceId = 0;

        List<string> SelectedChecks = new List<string>();
        bool CheckAllScanTrace = false;
        int StartScanTraceId = 0;
        int EndScanTraceId = 0;

        List<string> Keywords = new List<string>();
        int RoundtripIncrease = 0;
        int RoundtripIncreaseFactor = 0;
        int InsertedCharsCount = 0;

        private void ScanTraceBehaviourAnalysis_Load(object sender, EventArgs e)
        {
            ScanPluginsGrid.Rows.Add(new object[]{true, "All Checks"});
            foreach (string Name in ActivePlugin.List())
            {
                ScanPluginsGrid.Rows.Add(new object[] { true, Name });
            }

            ConfigKeywordsTB.Text = "error, exception, not allowed, unauthorized, blocked, filtered, attack, unexpected, sql, database, failed";
            ConfigResponseTimeChangeMSTB.Text = "1000";
            ConfigResponseTimeChangeFactorTB.Text = "10";
            ConfigCharsCountTB.Text = "20";
        }

        private void ScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanPluginsGrid.SelectedRows == null) return;
            if (ScanPluginsGrid.SelectedRows.Count == 0) return;

            if ((bool)ScanPluginsGrid.SelectedRows[0].Cells[0].Value)
            {
                ScanPluginsGrid.SelectedRows[0].Cells[0].Value = false;
                ScanPluginsGrid.Rows[0].Cells[0].Value = false;
            }
            else
            {
                ScanPluginsGrid.SelectedRows[0].Cells[0].Value = true;
            }
            if (ScanPluginsGrid.SelectedRows[0].Index == 0)
            {
                foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
                {
                    Row.Cells[0].Value = ScanPluginsGrid.Rows[0].Cells[0].Value;
                }
            }
        }

        private void AnalyzeInRangeScanTraceRB_CheckedChanged(object sender, EventArgs e)
        {
            if (AnalyzeInRangeScanTraceRB.Checked)
            {
                ScanTraceRangeStartTB.Enabled = true;
                ScanTraceRangeEndTB.Enabled = true;
            }
            else
            {
                ScanTraceRangeStartTB.Enabled = false;
                ScanTraceRangeEndTB.Enabled = false;
            }
        }

        private void ShowHideConfigPanelLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ShowHideConfigPanelLL.Text.Equals("Show Config Panel"))
            {
                ShowConfigPanel();
            }
            else
            {
                HideConfigPanel();
            }
        }

        private void StartAnalysisBtn_Click(object sender, EventArgs e)
        {
            if (StartAnalysisBtn.Text.Equals("Start Analysis"))
            {
                StartAnalysisBtn.Text = "Stop Analysis";
                try
                {
                    AnalysisThread.Abort();
                }
                catch { }
                if (!ReadFilter()) return;
                if (!ReadConfig())
                {
                    ShowConfigPanel();
                    return;
                }
                ClearResultsGrid();
                AnalysisProgressBar.Visible = true;
                AnalysisThread = new Thread(DoAnalysis);
                AnalysisThread.Start();
                BaseTabs.SelectTab(1);
            }
            else
            {
                StopAnalysis(false);
            }
        }

        void ClearResultsGrid()
        {
            TraceGrid.Rows.Clear();
            SelectedTraceId = 0;
            LoadTraceViewerBtn.Enabled = false;
            ClearResultsTabs();
        }

        void ClearResultsTabs()
        {
            SummaryRTB.Text = "";
            CodeGrid.Rows.Clear();
            KeywordsGrid.Rows.Clear();
            SetCookieGrid.Rows.Clear();
            HeadersGrid.Rows.Clear();
            RoundtripGrid.Rows.Clear();
            BodyGrid.Rows.Clear();
        }

        bool ReadFilter()
        {
            this.SelectedChecks.Clear();
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                if (Row.Index > 0)
                {
                    if ((bool)Row.Cells[0].Value) this.SelectedChecks.Add(Row.Cells[1].Value.ToString());
                }
            }
            if (this.SelectedChecks.Count == 0)
            {
                ShowError("Select atleast one vulnerability check whose Scan Trace must be analyzed");
                return false;
            }
            this.CheckAllScanTrace = AnalyzeAllScanTraceRB.Checked;
            try
            {
                this.StartScanTraceId = Int32.Parse(ScanTraceRangeStartTB.Text.Trim());
            }
            catch
            {
                ShowError("Scan Trace range start value must be a number");
                return false;
            }
            try
            {
                this.EndScanTraceId = Int32.Parse(ScanTraceRangeEndTB.Text.Trim());
            }
            catch
            {
                ShowError("Scan Trace range end value must be a number");
                return false;
            }
            if (this.StartScanTraceId < 0)
            {
                ShowError("Scan Trace range start value must be 0 or a positive number");
                return false;
            }
            if (this.EndScanTraceId < 0)
            {
                ShowError("Scan Trace range end value must be a positive number");
                return false;
            }
            if(this.EndScanTraceId <= this.StartScanTraceId)
            {
                ShowError("Scan Trace range end value must be greater than the Scan Trace start value");
                return false;
            }
            return true;
        }

        void ShowError(string Msg)
        {
            ErrorTB.Text = Msg;
            if (Msg.Length == 0)
            {
                ErrorTB.Visible = false;
            }
            else
            {
                ErrorTB.Visible = true;
            }
        }

        bool ReadConfig()
        {
            this.Keywords.Clear();

            string[] ExtractedKeywords = ConfigKeywordsTB.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string EK in ExtractedKeywords)
            {
                string TEK = EK.Trim();
                if (TEK.Length > 0) this.Keywords.Add(TEK);
            }

            //if (this.Keywords.Count == 0)
            //{
            //    ShowError("No keywords specified");
            //    return false;
            //}
            
            try
            {
                this.RoundtripIncrease = Int32.Parse(ConfigResponseTimeChangeMSTB.Text.Trim());
            }
            catch
            {
                ShowError("Response time variation value must be a number");
                return false;
            }

            try
            {
                this.RoundtripIncreaseFactor = Int32.Parse(ConfigResponseTimeChangeFactorTB.Text.Trim());
            }
            catch
            {
                ShowError("Response time variation multiple value must be a number");
                return false;
            }


            try
            {
                this.InsertedCharsCount = Int32.Parse(ConfigCharsCountTB.Text.Trim());
            }
            catch
            {
                ShowError("Inserted characters count value must be a number");
                return false;
            }

            if (this.RoundtripIncrease < 1)
            {
                ShowError("Response time variation value must be a value greater than 0");
                return false;
            }
            if (this.RoundtripIncreaseFactor < 1)
            {
                ShowError("Response time variation multiple value must be a value greater than 0");
                return false;
            }
            if (this.InsertedCharsCount < 1)
            {
                ShowError("Inserted characters count value must be a value greater than 0");
                return false;
            }

            return true;
        }

        void HideConfigPanel()
        {
            ConfigPanel.Visible = false;
            ShowHideConfigPanelLL.Text = "Show Config Panel";
        }
        void ShowConfigPanel()
        {
            ConfigPanel.Visible = true;
            ShowHideConfigPanelLL.Text = "Hide Config Panel";
        }

        private void ScanTraceRangeStartTB_TextChanged(object sender, EventArgs e)
        {
            string Value = ScanTraceRangeStartTB.Text.Trim();
            try
            {
                Int32.Parse(Value);
                ScanTraceRangeStartTB.BackColor = Color.White;
            }
            catch
            {
                ScanTraceRangeStartTB.BackColor = Color.Red;
            }
        }

        delegate void AddAnalysisResultEntry_d(List<object> Vals); 
        void AddAnalysisResultEntry(List<object> Vals)
        {
            if (TraceGrid.InvokeRequired)
            {
                AddAnalysisResultEntry_d ARE_d = new AddAnalysisResultEntry_d(AddAnalysisResultEntry);
                TraceGrid.Invoke(ARE_d, new object[] { Vals });
            }
            else
            {
                TraceGrid.Rows.Add(Vals.ToArray());
            }
        }

        private void ScanTraceRangeEndTB_TextChanged(object sender, EventArgs e)
        {
            string Value = ScanTraceRangeEndTB.Text.Trim();
            try
            {
                Int32.Parse(Value);
                ScanTraceRangeEndTB.BackColor = Color.White;
            }
            catch
            {
                ScanTraceRangeEndTB.BackColor = Color.Red;
            }
        }

        private void ConfigResponseTimeChangeMSTB_TextChanged(object sender, EventArgs e)
        {
            string Value = ConfigResponseTimeChangeMSTB.Text.Trim();
            try
            {
                Int32.Parse(Value);
                ConfigResponseTimeChangeMSTB.BackColor = Color.White;
            }
            catch
            {
                ConfigResponseTimeChangeMSTB.BackColor = Color.Red;
            }
        }

        private void ConfigResponseTimeChangeFactorTB_TextChanged(object sender, EventArgs e)
        {
            string Value = ConfigResponseTimeChangeFactorTB.Text.Trim();
            try
            {
                Int32.Parse(Value);
                ConfigResponseTimeChangeFactorTB.BackColor = Color.White;
            }
            catch
            {
                ConfigResponseTimeChangeFactorTB.BackColor = Color.Red;
            }
        }

        private void ConfigCharsCountTB_TextChanged(object sender, EventArgs e)
        {
            string Value = ConfigCharsCountTB.Text.Trim();
            try
            {
                Int32.Parse(Value);
                ConfigCharsCountTB.BackColor = Color.White;
            }
            catch
            {
                ConfigCharsCountTB.BackColor = Color.Red;
            }
        }

        delegate void StopAnalysis_d(bool Completed);
        void StopAnalysis(bool Completed)
        {
            if (TraceGrid.InvokeRequired)
            {
                StopAnalysis_d SA_d = new StopAnalysis_d(StopAnalysis);
                TraceGrid.Invoke(SA_d, new object[] { Completed });
            }
            else
            {
                try
                {
                    AnalysisThread.Abort();
                }
                catch { }
                StartAnalysisBtn.Text = "Start Analysis";
                AnalysisProgressBar.Visible = false;
                if (Completed)
                {
                    ShowAnalysisStatus("Analysis Completed");
                }
                else
                {
                    ShowAnalysisStatus("Analysis Stopped");
                }
            }
        }

        delegate void ShowAnalysisStatus_d(string Status);
        void ShowAnalysisStatus(string Status)
        {
            if (AnalysisStatusLbl.InvokeRequired)
            {
                ShowAnalysisStatus_d SAS_d = new ShowAnalysisStatus_d(ShowAnalysisStatus);
                TraceGrid.Invoke(SAS_d, new object[] { Status });
            }
            else
            {
                AnalysisStatusLbl.Text = Status;
            }
        }

        void DoAnalysis()
        {
            bool CanDo = true;
            int StartId = 1;
            int LastId = 0;
            
            if (!this.CheckAllScanTrace)
            {
                StartId = this.StartScanTraceId;
                LastId = this.EndScanTraceId;
            }
            int CurrentId = StartId;
            try
            {
                while(CanDo && CurrentId <= Config.LastScanTraceId)
                {
                    try
                    {
                        CanDo = false;
                        string TraceOverviewMessage = IronDB.GetScanTraceOverviewAndMessage(CurrentId)[0];
                        IronTrace ScanTraceRecord = IronDB.GetScanTraceRecords(CurrentId, 1)[0];

                        if (this.SelectedChecks.Contains(ScanTraceRecord.PluginName))
                        {
                            AnalyzeTraceId(CurrentId, TraceOverviewMessage, ScanTraceRecord);
                        }
                    }
                    catch(ThreadAbortException)
                    {}
                    catch(Exception Exp)
                    {
                        IronException.Report(string.Format("Error performing Payload Effect Analysis on Scan Trace ID - {0}", CurrentId), Exp);
                    }
                    if (CurrentId < Config.LastScanTraceId && (CurrentId < LastId || LastId == 0))
                    {
                        CanDo = true;
                        CurrentId++;
                    }
                }
                StopAnalysis(true);
            }
            catch(ThreadAbortException) {}
        }

        void AnalyzeTraceId(int CurrentId, string TraceOverviewMessage, IronTrace ScanTraceRecord)
        {
            ShowAnalysisStatus(string.Format("Analyzing Trace ID - {0}", CurrentId));
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
            BehaviourAnalysis BA = new BehaviourAnalysis(this.Keywords, this.RoundtripIncrease, this.RoundtripIncreaseFactor, this.InsertedCharsCount);
            BA.Analyze(Payloads, LogIds, RoundTrips, ScanTraceRecord.Section);

            bool CodeResultFound = false;
            bool KeywordsResultFound = false;
            string BodyResultFound = "";
            bool SetCookieFound = false;
            bool HeadersFound = false;
            string RoundtripFound = "";

            BehaviourAnalysisResults Results = new BehaviourAnalysisResults(BA.Results);

            if (Results.SetCookies.Count > 0) SetCookieFound = true;
            if (Results.Codes.Count > 0) CodeResultFound = true;
            if (Results.Keywords.Count > 0) KeywordsResultFound = true;
            if (Results.Headers.Count > 0) HeadersFound = true;

            if (Results.InsertedChars.Count > 0) BodyResultFound = string.Format("{0} chars", Results.InsertedChars[Results.InsertedChars.Count - 1]);
            if (Results.PlusRoundtripDiffs.Count > 0 || Results.MinusRoundtripDiffs.Count > 0)
            {
                int PlusMax = 0;
                int MinusMax = 0;
                if (Results.PlusRoundtripDiffs.Count > 0) PlusMax = Results.PlusRoundtripDiffs[Results.PlusRoundtripDiffs.Count - 1];
                if (Results.MinusRoundtripDiffs.Count > 0) MinusMax = Results.MinusRoundtripDiffs[Results.MinusRoundtripDiffs.Count - 1];
                if (PlusMax > MinusMax)
                    RoundtripFound = string.Format("+{0} ms", PlusMax);
                else
                    RoundtripFound = string.Format("-{0} ms", MinusMax);
            }

            if (CodeResultFound || KeywordsResultFound || BodyResultFound.Length > 0 || SetCookieFound || HeadersFound || RoundtripFound.Length > 0)
            {
                List<object> Vals = new List<object>() { CurrentId, ScanTraceRecord.Section, ScanTraceRecord.PluginName, CodeResultFound, KeywordsResultFound, BodyResultFound, SetCookieFound, HeadersFound, RoundtripFound, BA.BaseLineLogId, BA.BaseLinePayload, BA.BaseLineRoundtripTime, BA.BaseLineSession.Response.Code, BA.ResultsXml };
                AddAnalysisResultEntry(Vals);
            }
        }

        private void TraceGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (TraceGrid.SelectedCells.Count < 1 || TraceGrid.SelectedCells[0].Value == null)
            {
                return;
            }

            SelectedTraceId = Int32.Parse(TraceGrid.SelectedRows[0].Cells["IDClmn"].Value.ToString());
            LoadTraceViewerBtn.Enabled = true;

            ClearResultsTabs();

            string BaselineLogId = TraceGrid.SelectedRows[0].Cells["BaselineLogIdClmn"].Value.ToString();
            string BaselinePayload = TraceGrid.SelectedRows[0].Cells["BaselinePayloadClmn"].Value.ToString();
            string ResultsXml = TraceGrid.SelectedRows[0].Cells["XmlClmn"].Value.ToString();
            string BaselineCode = TraceGrid.SelectedRows[0].Cells["BaselineCodeClmn"].Value.ToString();
            string BaselineRoundtrip = TraceGrid.SelectedRows[0].Cells["BaselineRoundtripClmn"].Value.ToString();

            List<BehaviourAnalysisResult> Results = BehaviourAnalysisResult.ToObjectList(ResultsXml);
            List<int> Codes = new List<int>();
            List<string> Keywords = new List<string>();
            List<string> Roundtrips = new List<string>();
            List<int> InsertedChars = new List<int>();
            List<string> SetCookies = new List<string>();
            List<string> Headers = new List<string>();

            List<object[]> CodeGridRows = new List<object[]>();
            List<object[]> KeywordGridRows = new List<object[]>();
            List<object[]> SetCookieGridRows = new List<object[]>();
            List<object[]> HeadersGridRows = new List<object[]>();
            List<object[]> BodyGridRows = new List<object[]>();
            List<object[]> TimeGridRows = new List<object[]>();

            foreach (BehaviourAnalysisResult Result in Results)
            {
                if (Result.ResponseCodeResult > 0)
                {
                    CodeGridRows.Add(new object[]{Result.LogId, Result.ResponseCodeResult, Result.Payload});
                    if(!Codes.Contains(Result.ResponseCodeResult)) Codes.Add(Result.ResponseCodeResult);
                }
                if (Result.ResponseContentResult > 0)
                {
                    BodyGridRows.Add(new object[]{Result.LogId, Result.ResponseContentResult, Result.Payload});
                    if(!InsertedChars.Contains(Result.ResponseContentResult)) InsertedChars.Add(Result.ResponseContentResult);
                }
                if (Result.RoundtripTimeResult.Length > 0)
                {
                    TimeGridRows.Add(new object[]{Result.LogId, Result.RoundtripTimeResult, Result.Payload});
                    if(Int32.Parse(Result.RoundtripTimeResult.Trim(new char[]{'+', '-', 'm', 's'})) > 0) Roundtrips.Add(Result.RoundtripTimeResult);
                }

                if (Result.ResponseKeywordsResult.Count > 0) KeywordGridRows.Add(new object[]{Result.LogId, string.Join(", ", Result.ResponseKeywordsResult.ToArray()), Result.Payload});
                foreach (string Keyword in Result.ResponseKeywordsResult)
                {
                    if (!Keywords.Contains(Keyword)) Keywords.Add(Keyword);
                }

                if (Result.SetCookieHeaderResult.Count > 0) SetCookieGridRows.Add(new object[] { Result.LogId, string.Join(", ", Result.SetCookieHeaderResult.ToArray()), Result.Payload });
                foreach (string SC in Result.SetCookieHeaderResult)
                {
                    if (!SetCookies.Contains(SC)) SetCookies.Add(SC);
                }

                if (Result.ResponseHeadersResult.Count > 0) HeadersGridRows.Add(new object[] { Result.LogId, string.Join(", ", Result.ResponseHeadersResult.ToArray()), Result.Payload });
                foreach (string H in Result.ResponseHeadersResult)
                {
                    if (! Headers.Contains(H)) Headers.Add(H);
                }
            }

            if (CodeGridRows.Count > 0)
            {
                //CodeGridRows.Insert(0, new object[] { BaselineLogId, BaselineCode, string.Format("(baseline) {0}", BaselinePayload) });
            }
            if (TimeGridRows.Count > 0)
            {
                //TimeGridRows.Insert(0, new object[] { BaselineLogId, string.Format("{0} ms", BaselineRoundtrip), string.Format("(baseline) {0}", BaselinePayload) });
            }

            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
            StringBuilder Summary = new StringBuilder();
            Summary.Append("<i<h1>>Payload Effect Analysis Result:<i</h1>><i<br>><i<br>>");
            if (Codes.Count > 0)
            {
                Summary.Append(string.Format("Some payloads caused the response codes to change from the baseline value of {0} to ", BaselineCode));
                for (int i = 0; i < Codes.Count; i++)
                {
                    Summary.Append(Codes[i]);
                    if (i < Codes.Count - 1) Summary.Append(", ");
                }
                Summary.Append("<i<br>><i<br>>");
            }
            if (Keywords.Count > 0)
            {
                Summary.Append("Some payloads caused the introduction of the following keywords in the response:<i<br>>");
                for (int i = 0; i < Keywords.Count; i++)
                {
                    Summary.Append("<i<cr>>"); Summary.Append(Keywords[i]); Summary.Append("<i</cr>>");
                    if (i < Keywords.Count - 1) Summary.Append(", ");
                }
                Summary.Append("<i<br>><i<br>>");
            }
            if (InsertedChars.Count > 0)
            {
                InsertedChars.Sort();
                Summary.Append("Some payloads caused the response to change. where the number of new characters found in the responses were ");
                for (int i = InsertedChars.Count - 1; i >= 0; i--)
                {
                    Summary.Append(InsertedChars[i]);
                    if (i > 0) Summary.Append(", ");
                }
                Summary.Append("<i<br>><i<br>>");
            }
            if (Roundtrips.Count > 0)
            {
                int BaselineRoundtripInt = Int32.Parse(BaselineRoundtrip);
                
                List<int> PlusRoundtripIntList = new List<int>();
                List<int> MinusRoundtripIntList = new List<int>();

                for (int i = 0; i < Roundtrips.Count; i++)
                {
                    int RoundtripDiff = Int32.Parse(Roundtrips[i].Trim(new char[]{'+', '-', 'm', 's', ' '}));
                    if (Roundtrips[i][0] == '+')
                    {
                        PlusRoundtripIntList.Add(BaselineRoundtripInt + RoundtripDiff);
                    }
                    else
                    {
                        MinusRoundtripIntList.Add(BaselineRoundtripInt - RoundtripDiff);
                    }
                }

                PlusRoundtripIntList.Sort();
                MinusRoundtripIntList.Sort();

                if (PlusRoundtripIntList.Count > 0)
                {
                    Summary.Append(string.Format("Some payloads caused the response time to INCREASE from the baseline value of {0} ms to ", BaselineRoundtrip));
                    for (int i = PlusRoundtripIntList.Count - 1; i >= 0; i--)
                    {
                        Summary.Append(PlusRoundtripIntList[i]); Summary.Append(" ms");
                        if (i > 0) Summary.Append(", ");
                    }
                    Summary.Append("<i<br>><i<br>>");
                }

                if (MinusRoundtripIntList.Count > 0)
                {
                    Summary.Append(string.Format("Some payloads caused the response time to DECREASE from the baseline value of {0} ms to ", BaselineRoundtrip));
                    for (int i = MinusRoundtripIntList.Count - 1; i >= 0; i--)
                    {
                        Summary.Append(MinusRoundtripIntList[i]); Summary.Append(" ms");
                        if (i > 0) Summary.Append(", ");
                    }
                    Summary.Append("<i<br>><i<br>>");
                }
            }
            if (SetCookies.Count > 0)
            {
                Summary.Append("Some payloads caused the Set-Cookie values to vary from the baseline response as follows:<i<br>>");
                for (int i = 0; i < SetCookies.Count; i++)
                {
                    Summary.Append(SetCookies[i]);
                    if (i < SetCookies.Count - 1) Summary.Append(", ");
                }
                Summary.Append("<i<br>><i<br>>");
            }
            if (Headers.Count > 0)
            {
                Summary.Append("Some payloads caused the header parameters to vary from the baseline response as follows:<i<br>>");
                for (int i = 0; i < Headers.Count; i++)
                {
                    Summary.Append(Headers[i]);
                    if (i < Headers.Count - 1) Summary.Append(", ");
                }
                Summary.Append("<i<br>><i<br>>");
            }

            SB.Append(Tools.RtfSafe(Summary.ToString()));
            SummaryRTB.Rtf = SB.ToString();
            
            if (CodeGridRows.Count > 0)
            {
                BottomTabs.TabPages["CodeTab"].Text = "  Code Variation  ";
                foreach (object[] Row in CodeGridRows)
                {
                    CodeGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["CodeTab"].Text = "  -  ";
            }
            if (TimeGridRows.Count > 0)
            {
                BottomTabs.TabPages["TimeTab"].Text = "  Time Variation  ";
                foreach (object[] Row in TimeGridRows)
                {
                    RoundtripGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["TimeTab"].Text = "  -  ";
            }
            if (KeywordGridRows.Count > 0)
            {
                BottomTabs.TabPages["KeywordsTab"].Text = "  Keywords Inserted  ";
                foreach (object[] Row in KeywordGridRows)
                {
                    KeywordsGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["KeywordsTab"].Text = "  -  ";
            }
            if (BodyGridRows.Count > 0)
            {
                BottomTabs.TabPages["BodyTab"].Text = "  Body Variation  ";
                foreach (object[] Row in BodyGridRows)
                {
                    BodyGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["BodyTab"].Text = "  -  ";
            }
            if (SetCookieGridRows.Count > 0)
            {
                BottomTabs.TabPages["SetCookieTab"].Text = "  Set-Cookie Variations  ";
                foreach (object[] Row in SetCookieGridRows)
                {
                    SetCookieGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["SetCookieTab"].Text = "  -  ";
            }
            if (HeadersGridRows.Count > 0)
            {
                BottomTabs.TabPages["HeadersTab"].Text = "  Headers Variation  ";
                foreach (object[] Row in HeadersGridRows)
                {
                    HeadersGrid.Rows.Add(Row);
                }
                HeadersGrid.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            else
            {
                BottomTabs.TabPages["HeadersTab"].Text = "  -  ";
            }
        }

        private void LoadTraceViewerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = SelectedTraceId;
                string[] OverviewAndMessage = IronDB.GetScanTraceOverviewAndMessage(ID);
                string OverviewXml = OverviewAndMessage[0];
                string Message = OverviewAndMessage[1];


                //string Message = IronDB.GetScanTraceMessage(ID);
                StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
                SB.Append(Tools.RtfSafe(Message));
                LogTraceViewer TraceViewer = new LogTraceViewer();
                TraceViewer.ScanTraceMsgRTB.Rtf = SB.ToString();

                try
                {
                    List<Dictionary<string, string>> OverviewEntries = IronTrace.GetOverviewEntriesFromXml(OverviewXml);

                    foreach (Dictionary<string, string> Entry in OverviewEntries)
                    {
                        TraceViewer.ScanTraceOverviewGrid.Rows.Add(new object[] { false, Entry["id"], Entry["log_id"], Entry["payload"], Entry["code"], Entry["length"], Entry["mime"], Entry["time"], Entry["signature"] });
                    }
                }
                catch
                {
                    //Probaly an entry from the log of an older version
                }

                TraceViewer.Show();

            }
            catch (Exception Exp)
            {
                IronException.Report("Error reading ScanTrace Message from DB", Exp.Message, Exp.StackTrace);
            }
        }
    }
}
