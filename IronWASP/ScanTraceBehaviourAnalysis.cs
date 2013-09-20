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

        ScanTraceBehaviourAnalysisResultsUiInformation CurrentUiResults = new ScanTraceBehaviourAnalysisResultsUiInformation();

        internal static List<string> DefaultErrorKeywords = new List<string>() { "error", "exception", "not allowed", "unauthorized", "blocked", "filtered", "attack", "unexpected", "sql", "database", "failed" };
        internal static int DefaultResponseTimeChange = 1000;
        internal static int DefaultResponseTimeChangeFactor = 10;
        internal static int DefaultCharsCount = 20;

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

            ConfigKeywordsTB.Text = string.Join(", ", DefaultErrorKeywords.ToArray());
            ConfigResponseTimeChangeMSTB.Text = DefaultResponseTimeChange.ToString();
            ConfigResponseTimeChangeFactorTB.Text = DefaultResponseTimeChangeFactor.ToString();
            ConfigCharsCountTB.Text = DefaultCharsCount.ToString();
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
                        IronTrace ScanTraceRecord = IronDB.GetScanTraces(CurrentId, 1)[0];

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
            
            BehaviourAnalysis BA = new BehaviourAnalysis(this.Keywords, this.RoundtripIncrease, this.RoundtripIncreaseFactor, this.InsertedCharsCount);
            BA.Analyze(TraceOverviewMessage, ScanTraceRecord.Section);

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

            ScanTraceBehaviourAnalysisResultsUiInformation UiResults = GetUiDisplayResults(ResultsXml, BaselineCode, BaselineRoundtrip);
            this.CurrentUiResults = UiResults;

            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;\red255\green255\blue255;}");
            SB.Append(Tools.RtfSafe(UiResults.SummaryText));

            SummaryRTB.Rtf = SB.ToString();

            if (UiResults.CodeGridRows.Count > 0)
            {
                BottomTabs.TabPages["CodeTab"].Text = "  Code Variation  ";
                foreach (object[] Row in UiResults.CodeGridRows)
                {
                    CodeGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["CodeTab"].Text = "  -  ";
            }
            if (UiResults.TimeGridRows.Count > 0)
            {
                BottomTabs.TabPages["TimeTab"].Text = "  Time Variation  ";
                foreach (object[] Row in UiResults.TimeGridRows)
                {
                    RoundtripGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["TimeTab"].Text = "  -  ";
            }
            if (UiResults.KeywordGridRows.Count > 0)
            {
                BottomTabs.TabPages["KeywordsTab"].Text = "  Keywords Inserted  ";
                foreach (object[] Row in UiResults.KeywordGridRows)
                {
                    KeywordsGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["KeywordsTab"].Text = "  -  ";
            }
            if (UiResults.BodyGridRows.Count > 0)
            {
                BottomTabs.TabPages["BodyTab"].Text = "  Body Variation  ";
                foreach (object[] Row in UiResults.BodyGridRows)
                {
                    BodyGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["BodyTab"].Text = "  -  ";
            }
            if (UiResults.SetCookieGridRows.Count > 0)
            {
                BottomTabs.TabPages["SetCookieTab"].Text = "  Set-Cookie Variations  ";
                foreach (object[] Row in UiResults.SetCookieGridRows)
                {
                    SetCookieGrid.Rows.Add(Row);
                }
            }
            else
            {
                BottomTabs.TabPages["SetCookieTab"].Text = "  -  ";
            }
            if (UiResults.HeadersGridRows.Count > 0)
            {
                BottomTabs.TabPages["HeadersTab"].Text = "  Headers Variation  ";
                foreach (object[] Row in UiResults.HeadersGridRows)
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

        internal static ScanTraceBehaviourAnalysisResultsUiInformation GetUiDisplayResults(string ResultsXml, string BaselineCode, string BaselineRoundtrip)
        {
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

            Dictionary<string, string> HeaderVariationMessage = new Dictionary<string, string>() {
            {"+", "Header added, this header was missing in baseline response"}, 
            {"-", "Missing header, this header was present in baseline response"}, 
            {">", "Value added, this header had an empty value in baseline"},
            {"<", "Empty value, this header had a non-empty value in baseline"}
            };

            Dictionary<string, string> SetCookieVariationMessage = new Dictionary<string, string>() {
            {"+", "Cookie added, this cookie was missing in baseline response"}, 
            {"-", "Missing cookie, this cookie was present in baseline response"}, 
            {">", "Value added, this cookie had an empty value in baseline"},
            {"<", "Empty value, this cookie had a non-empty value in baseline"}
            };

            Dictionary<string, string> HeaderVariationMessageForSummary = new Dictionary<string, string>() {
            {"+", "header added, this header was missing in baseline response"}, 
            {"-", "header is missing, this header was present in baseline response"}, 
            {">", "header's value added, this header had an empty value in baseline"},
            {"<", "header's value is empty, this header had a non-empty value in baseline"}
            };

            Dictionary<string, string> SetCookieVariationMessageForSummary = new Dictionary<string, string>() {
            {"+", "cookie added, this cookie was missing in baseline response"}, 
            {"-", "cookie is missing, this cookie was present in baseline response"}, 
            {">", "cookie's value added, this cookie had an empty value in baseline"},
            {"<", "cookie's value is empty, this cookie had a non-empty value in baseline"}
            };

            foreach (BehaviourAnalysisResult Result in Results)
            {
                if (Result.ResponseCodeResult > 0)
                {
                    CodeGridRows.Add(new object[] { Result.LogId, Result.ResponseCodeResult, Result.Payload });
                    if (!Codes.Contains(Result.ResponseCodeResult)) Codes.Add(Result.ResponseCodeResult);
                }
                if (Result.ResponseContentResult > 0)
                {
                    BodyGridRows.Add(new object[] { Result.LogId, Result.ResponseContentResult, Result.Payload });
                    if (!InsertedChars.Contains(Result.ResponseContentResult)) InsertedChars.Add(Result.ResponseContentResult);
                }
                if (Result.RoundtripTimeResult.Length > 0)
                {
                    TimeGridRows.Add(new object[] { Result.LogId, Result.RoundtripTimeResult, Result.Payload });
                    if (Int32.Parse(Result.RoundtripTimeResult.Trim(new char[] { '+', '-', 'm', 's' })) > 0)
                    {
                        if (!Roundtrips.Contains(Result.RoundtripTimeResult)) Roundtrips.Add(Result.RoundtripTimeResult);
                    }
                }

                if (Result.ResponseKeywordsResult.Count > 0) KeywordGridRows.Add(new object[] { Result.LogId, string.Join(", ", Result.ResponseKeywordsResult.ToArray()), Result.Payload });
                foreach (string Keyword in Result.ResponseKeywordsResult)
                {
                    if (!Keywords.Contains(Keyword)) Keywords.Add(Keyword);
                }

                if (Result.SetCookieHeaderResult.Count > 0)
                {
                    foreach (string SetCook in Result.SetCookieHeaderResult)
                    {
                        SetCookieGridRows.Add(new object[] { Result.LogId, SetCook.Substring(1), SetCookieVariationMessage[SetCook[0].ToString()], Result.Payload });
                    }
                }
                foreach (string SC in Result.SetCookieHeaderResult)
                {
                    if (!SetCookies.Contains(SC)) SetCookies.Add(SC);
                }

                if (Result.ResponseHeadersResult.Count > 0)
                {
                    foreach (string HeaderRes in Result.ResponseHeadersResult)
                    {
                        HeadersGridRows.Add(new object[] { Result.LogId, HeaderRes.Substring(1), HeaderVariationMessage[HeaderRes[0].ToString()], Result.Payload });
                    }
                }
                foreach (string H in Result.ResponseHeadersResult)
                {
                    if (!Headers.Contains(H)) Headers.Add(H);
                }
            }

            StringBuilder Summary = new StringBuilder();
            
            if (Codes.Count > 0)
            {
                Summary.Append(string.Format("Response codes changed from the baseline value of <i<cg>><i<b>>{0}<i</b>><i</cg>> to ", BaselineCode));
                for (int i = 0; i < Codes.Count; i++)
                {
                    Summary.Append(string.Format("<i<cb>><i<b>>{0}<i</b>><i</cb>>", Codes[i]));
                    if (i < Codes.Count - 1) Summary.Append(", ");
                }
                Summary.Append("<i<br>><i<br>>");
            }
            if (Keywords.Count > 0)
            {
                Summary.Append("Occurance of the following keywords in the response: ");
                for (int i = 0; i < Keywords.Count; i++)
                {
                    Summary.Append("<i<cr>><i<b>>"); Summary.Append(Keywords[i]); Summary.Append("<i</b>><i</cr>>");
                    if (i < Keywords.Count - 1) Summary.Append(", ");
                }
                Summary.Append("<i<br>><i<br>>");
            }
            if (InsertedChars.Count > 0)
            {
                InsertedChars.Sort();
                Summary.Append(string.Format("Up to <i<cb>><i<b>>{0}<i</b>><i</cb>> characters of new content found in some responses.", InsertedChars[0]));
                Summary.Append("<i<br>><i<br>>");
            }

            if (SetCookies.Count > 0)
            {
                Summary.Append("Changes in Set-Cookie values:<i<br>>");
                foreach (string SetCookie in SetCookies)
                {
                    Summary.Append("    ");
                    Summary.Append("<i<co>><i<b>>"); Summary.Append(SetCookie.Substring(1)); Summary.Append("<i</b>><i</co>> ");
                    Summary.Append(SetCookieVariationMessageForSummary[SetCookie[0].ToString()]);
                    Summary.Append("<i<br>>");
                }
                Summary.Append("<i<br>>");
            }
            if (Headers.Count > 0)
            {
                Summary.Append("Changes in Response Headers:<i<br>>");
                foreach (string Header in Headers)
                {
                    Summary.Append("    ");
                    Summary.Append("<i<co>><i<b>>"); Summary.Append(Header.Substring(1)); Summary.Append("<i</b>><i</co>> ");
                    Summary.Append(HeaderVariationMessageForSummary[Header[0].ToString()]);
                    Summary.Append("<i<br>>");
                }
                Summary.Append("<i<br>>");
            }

            if (Roundtrips.Count > 0)
            {
                int BaselineRoundtripInt = Int32.Parse(BaselineRoundtrip);

                List<int> PlusRoundtripIntList = new List<int>();
                List<int> MinusRoundtripIntList = new List<int>();

                for (int i = 0; i < Roundtrips.Count; i++)
                {
                    int RoundtripDiff = Int32.Parse(Roundtrips[i].Trim(new char[] { '+', '-', 'm', 's', ' ' }));
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

                Summary.Append(string.Format("Variation in the response roundtrip time from baseline value {0} ms:<i<br>><i<br>>", BaselineRoundtripInt));

                string BaselineTimeStr = string.Format("{0} ms (Normal)", BaselineRoundtripInt);

                string HighestTimeStr = "";
                string LowestTimeStr = "";

                double Factor = (double)BaselineRoundtripInt / 100.0;

                if (Factor == 0) Factor = 1.0;//To avoid divide by 0 exception or multiply by 0 and get 0

                if (PlusRoundtripIntList.Count > 0)
                {
                    HighestTimeStr = string.Format("{0} ms (Highest variation)", PlusRoundtripIntList[0]);
                    if ((double)PlusRoundtripIntList[0] / Factor > 250.0)
                    {
                        Factor = (double)PlusRoundtripIntList[0] / 250.0;
                        if (Factor == 0) Factor = 1.0;
                    }
                    else if ((double)PlusRoundtripIntList[0] / Factor < 100.0)
                    {
                        Factor = (double)PlusRoundtripIntList[0] / 100.0;
                        if (Factor == 0) Factor = 1.0;
                    }
                }
                if (MinusRoundtripIntList.Count > 0)
                {
                    LowestTimeStr = string.Format("{0} ms (Lowest variation)", MinusRoundtripIntList[0]);
                }

                Summary.Append("<i<hlg>>");
                Summary.Append(new String(' ', (int)Math.Round(((double)BaselineRoundtripInt / Factor))));
                Summary.Append("<i</hlg>>");
                Summary.Append("    "); Summary.Append(BaselineTimeStr); Summary.Append("<i<br>>");

                if (HighestTimeStr.Length > 0)
                {
                    Summary.Append("<i<hlb>>");
                    Summary.Append(new String(' ', (int)Math.Round(((double)PlusRoundtripIntList[0] / Factor))));
                    Summary.Append("<i</hlb>>");
                    Summary.Append("    "); Summary.Append(HighestTimeStr); Summary.Append("<i<br>>");
                }
                if (LowestTimeStr.Length > 0)
                {
                    Summary.Append("<i<hlo>>");
                    Summary.Append(new String(' ', (int)Math.Round(((double)MinusRoundtripIntList[0] / Factor))));
                    Summary.Append("<i</hlo>>");
                    Summary.Append("    "); Summary.Append(LowestTimeStr); Summary.Append("<i<br>>");
                }
                Summary.Append("<i<br>>");
            }
           
            ScanTraceBehaviourAnalysisResultsUiInformation UiResult = new ScanTraceBehaviourAnalysisResultsUiInformation();
            string SummaryText = Summary.ToString();
            if (Summary.Length == 0)
            {
                UiResult.SummaryText = "<i<h1>>No significant variations could be observed<i</h1>><i<br>><i<br>>";
            }
            else
            {
                UiResult.SummaryText = string.Format("<i<h1>>Some payloads caused the following effects:<i</h1>><i<br>><i<br>>{0}", SummaryText);
            }
            UiResult.SummaryText = Summary.ToString();
            UiResult.CodeGridRows = new List<object[]>(CodeGridRows);
            UiResult.KeywordGridRows = new List<object[]>(KeywordGridRows);
            UiResult.SetCookieGridRows = new List<object[]>(SetCookieGridRows);
            UiResult.HeadersGridRows = new List<object[]>(HeadersGridRows);
            UiResult.BodyGridRows = new List<object[]>(BodyGridRows);
            UiResult.TimeGridRows = new List<object[]>(TimeGridRows);
            return UiResult;
        }

        private void LoadTraceViewerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = SelectedTraceId;
                LogTraceViewer TraceViewer = new LogTraceViewer(ID, CurrentUiResults);
                
                //string[] OverviewAndMessage = IronDB.GetScanTraceOverviewAndMessage(ID);
                //string OverviewXml = OverviewAndMessage[0];
                //string Message = OverviewAndMessage[1];
                //IronTrace ScanTraceRecord = IronDB.GetScanTraces(ID, 1)[0];


                ////string Message = IronDB.GetScanTraceMessage(ID);
                //StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;\red255\green255\blue255;}");
                //SB.Append(Tools.RtfSafe(Message));
                //LogTraceViewer TraceViewer = new LogTraceViewer(OverviewXml, ScanTraceRecord.Section);
                //TraceViewer.ScanTraceMsgRTB.Rtf = SB.ToString();

                //try
                //{
                //    List<Dictionary<string, string>> OverviewEntries = IronTrace.GetOverviewEntriesFromXml(OverviewXml);

                //    foreach (Dictionary<string, string> Entry in OverviewEntries)
                //    {
                //        TraceViewer.ScanTraceOverviewGrid.Rows.Add(new object[] { false, Entry["id"], Entry["log_id"], Entry["payload"], Entry["code"], Entry["length"], Entry["mime"], Entry["time"], Entry["signature"] });
                //    }

                //    TraceViewer.SetAnalysisUiResults(CurrentUiResults);
                //}
                //catch
                //{
                //    //Probaly an entry from the log of an older version
                //}
                //TraceViewer.ShouldDoAnalysis = false;//We already updaing these values, they don't have to be calculated again
                TraceViewer.Show();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error reading ScanTrace Message from DB", Exp.Message, Exp.StackTrace);
            }
        }

        private void ScanTraceBehaviourAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAnalysis(true);
        }
    }

    internal class ScanTraceBehaviourAnalysisResultsUiInformation
    {
        internal string SummaryText = "";
        internal List<object[]> CodeGridRows = new List<object[]>();
        internal List<object[]> KeywordGridRows = new List<object[]>();
        internal List<object[]> SetCookieGridRows = new List<object[]>();
        internal List<object[]> HeadersGridRows = new List<object[]>();
        internal List<object[]> BodyGridRows = new List<object[]>();
        internal List<object[]> TimeGridRows = new List<object[]>();
    }
}
