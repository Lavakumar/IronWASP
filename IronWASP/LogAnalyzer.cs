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
    public partial class LogAnalyzer : Form
    {
        ScanSelectedWizard SSW;
        
        string CurrentSearchSource = "";
        List<int> CurrentSearchResultsIds = new List<int>();
        List<int> LastSearchResultsIds = new List<int>();

        Thread LogSearchThread;
        Thread LogLoadThread;

        int SelectedRowsCount = 0;

        public LogAnalyzer()
        {
            InitializeComponent();
        }

        delegate void ShowRows_d(List<object[]> Rows);
        void ShowRows(List<object[]> Rows)
        {
            if (LogGrid.InvokeRequired)
            {
                ShowRows_d CALL_d = new ShowRows_d(ShowRows);
                LogGrid.Invoke(CALL_d, new object[] { Rows });
            }
            else
            {
                foreach (object[] Row in Rows)
                {
                    LogGrid.Rows.Add(Row);
                }
            }
            SearchResultsCountLbl.Text = string.Format("Search Results Count: {0}", LogGrid.Rows.Count);
        }

        delegate void ShowSearchProgressBar_d(bool Show);
        void ShowSearchProgressBar(bool Show)
        {
            if (LogGrid.InvokeRequired)
            {
                ShowSearchProgressBar_d CALL_d = new ShowSearchProgressBar_d(ShowSearchProgressBar);
                LogGrid.Invoke(CALL_d, new object[] { Show });
            }
            else
            {
                SearchProgressBar.Visible = Show;
            }
        }

        private void LogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LogGrid.SelectedRows == null) return;
            if (LogGrid.SelectedRows.Count == 0) return;
            if (ClickActionSelectLogRB.Checked)
            {
                LogGrid.SelectedRows[0].Cells[0].Value = !((bool)LogGrid.SelectedRows[0].Cells[0].Value);
                if ((bool)LogGrid.SelectedRows[0].Cells[0].Value)
                {
                    SelectedRowsCount++;   
                }
                else
                {
                    SelectAllCB.Checked = false;
                    SelectedRowsCount--;
                }
                if (SelectedRowsCount > 0)
                {
                    DoDiffBtn.Enabled = true;
                    StartScanBtn.Enabled = true;
                    StartTestBtn.Enabled = true;
                    RunModulesBtn.Enabled = true;
                }
                else
                {
                    DoDiffBtn.Enabled = false;
                    StartScanBtn.Enabled = false;
                    StartTestBtn.Enabled = false;
                    RunModulesBtn.Enabled = false;
                }
            }
            else
            {
                int LogId = (int)LogGrid.SelectedCells[1].Value;
                ShowSelectedLog(this.CurrentSearchSource, LogId);
            }
        }
        void ShowSelectedLog(string Source, int LogId)
        {
            LogDisplayTabs.Visible = false;
            LoadLogProgressBar.Visible = true;

            RequestView.ClearRequest();
            ResponseView.ClearResponse();
            
            if (LogLoadThread != null)
            {
                try
                {
                    LogLoadThread.Abort();
                }
                catch { }
            }
            object[] SelectedLogInfo = new object[] { Source, LogId };
            LogLoadThread = new Thread(ShowSelectedLog);
            LogLoadThread.Start(SelectedLogInfo);
        }

        void ShowSelectedLog(object SelectedLogInfoObject)
        {
            object[] SelectedLogInfo = (object[])SelectedLogInfoObject;
            string Source = SelectedLogInfo[0].ToString();
            int LogId = (int)SelectedLogInfo[1];

            try
            {
                Session Session = Session.FromLog(LogId, Source);

                if (Session.Request != null)
                {
                    RequestView.SetRequest(Session.Request);

                    if (Session.Response != null) ResponseView.SetResponse(Session.Response, Session.Request);
                }

            }
            catch (ThreadAbortException) { }
            catch (Exception Exp) { IronException.Report("Error loading Selected Log info in Log Analyzer", Exp); }
            finally
            {
                EndLogLoad();
            }
        }

        delegate void EndLogLoad_d();
        void EndLogLoad()
        {
            if (LogGrid.InvokeRequired)
            {
                EndLogLoad_d CALL_d = new EndLogLoad_d(EndLogLoad);
                LogGrid.Invoke(CALL_d, new object[] { });
            }
            else
            {
                LoadLogProgressBar.Visible = false;
                LogDisplayTabs.Visible = true;
            }
        }

        private void SelectAllCB_Click(object sender, EventArgs e)
        {
            bool Checked = SelectAllCB.Checked;
            foreach (DataGridViewRow Row in LogGrid.Rows)
            {
                Row.Cells[0].Value = Checked;
            }
            if (Checked)
                SelectedRowsCount = LogGrid.Rows.Count;
            else
                SelectedRowsCount = 0;
            if (SelectedRowsCount > 0)
            {
                DoDiffBtn.Enabled = true;
                StartScanBtn.Enabled = true;
                StartTestBtn.Enabled = true;
                RunModulesBtn.Enabled = true;
            }
            else
            {
                DoDiffBtn.Enabled = false;
                StartScanBtn.Enabled = false;
                StartTestBtn.Enabled = false;
                RunModulesBtn.Enabled = false;
            }
        }

        private void StartTestBtn_Click(object sender, EventArgs e)
        {
            List<int> IDs = new List<int>();
            foreach(DataGridViewRow Row in LogGrid.Rows)
            {
                if((bool)Row.Cells[0].Value)
                    IDs.Add((int)Row.Cells[1].Value);
            }
            LogsTester Tester = new LogsTester();
            Tester.SetSourceAndLogs(this.CurrentSearchSource, IDs);
            Tester.Show();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (SearchBtn.Text.Equals("Search with this Filter"))
            {
                LogSearchQuery Query = GetQueryFromInput();
                if (Query != null)
                {
                    LastSearchResultsIds = new List<int>(CurrentSearchResultsIds);
                    CurrentSearchResultsIds.Clear();
                    LogGrid.Rows.Clear();
                    BaseTabs.SelectTab("SearchResultsTab");
                    if (LogSearchThread != null)
                    {
                        try { LogSearchThread.Abort(); }
                        catch { }
                    }
                    LogSearchThread = new Thread(DoSearch);
                    LogSearchThread.Start(Query);
                    SearchBtn.Text = "Stop Current Search";
                }
            }
            else
            {
                StopSearch();
            }
        }

        void StopSearch()
        {
            try
            {
                LogSearchThread.Abort();
            }
            catch { }
            EndLogSearch();
        }

        delegate void EndLogSearch_d();
        void EndLogSearch()
        {
            if (LogGrid.InvokeRequired)
            {
                EndLogSearch_d CALL_d = new EndLogSearch_d(EndLogSearch);
                LogGrid.Invoke(CALL_d, new object[] { });
            }
            else
            {
                SearchProgressBar.Visible = false;
                SearchBtn.Text = "Search with this Filter";
            }
        }

        void DoSearch(object QueryObject)
        {
            LogSearchQuery Query = (LogSearchQuery)QueryObject;
            List<LogRow> LogRows = new List<LogRow>();
            ShowSearchProgressBar(true);
            try
            {
                if (Query.LogIds.Count > 0)
                {
                    int CurrentStartIndex = 0;
                    while (CurrentStartIndex < Query.LogIds.Count)
                    {
                        LogRows = IronDB.SearchLogs(Query, Query.LogIds, CurrentStartIndex, 100);
                        ConvertAndShowRows(LogRows);
                        CurrentStartIndex = CurrentStartIndex + 100;
                    }
                }
                else if (Query.ScanLogId > 0)
                {
                    LogRows = IronDB.SearchLogs(Query, Query.ScanLogId);
                }
                else if (Query.MinLogId > -1 | Query.MaxLogId > -1)
                {
                    int CurrentStartIndex = Query.MinLogId -1;
                    while (CurrentStartIndex < Query.MaxLogId + 1)
                    {
                        if(CurrentStartIndex + 1000 > Query.MaxLogId)
                            LogRows = IronDB.SearchLogs(Query, CurrentStartIndex, Query.MaxLogId + 1);
                        else
                            LogRows = IronDB.SearchLogs(Query, CurrentStartIndex, CurrentStartIndex + 1000);
                        ConvertAndShowRows(LogRows);
                        CurrentStartIndex = CurrentStartIndex + 1000;
                    }
                }
                else
                {
                    int CurrentStartIndex = 0;
                    while (CurrentStartIndex <= Config.GetLastLogId(Query.LogSource))
                    {
                        LogRows = IronDB.SearchLogs(Query, CurrentStartIndex, CurrentStartIndex + 1000);
                        ConvertAndShowRows(LogRows);
                        CurrentStartIndex = CurrentStartIndex + 1000;
                    }
                }
                if (this.CurrentSearchResultsIds.Count == 0)
                {
                    this.CurrentSearchResultsIds = new List<int>(this.LastSearchResultsIds);
                }
                
            }
            catch(ThreadAbortException) { }
            catch(Exception Exp)
            {
                IronException.Report("Error searching Logs", Exp);
                ShowStep0Error("Error searching logs");
            }
            finally
            {
                EndLogSearch();
            }
        }

        void ConvertAndShowRows(List<LogRow> LogRows)
        {
            List<object[]> LogRowsForGrid = new List<object[]>();
            foreach (LogRow Row in LogRows)
            {
                CurrentSearchResultsIds.Add(Row.ID);
                LogRowsForGrid.Add(Row.ToLogAnalyzerGridRowObjectArray());
            }
            ShowRows(LogRowsForGrid);
        }

        LogSearchQuery GetQueryFromInput()
        {
            ShowStep0Status("");
            LogSearchQuery Query = new LogSearchQuery();
            if (SearchTypeNewRB.Checked)
            {
                foreach (object Item in SelectLogSourceCombo.Items)
                {
                    if (Item.ToString().Equals(SelectLogSourceCombo.Text))
                    {
                        Query.LogSource = SelectLogSourceCombo.Text;
                        this.CurrentSearchSource = Query.LogSource;
                        break;
                    }
                }
                if (Query.LogSource.Length == 0)
                {
                    ShowStep0Error("Invaid Log source selected");
                    SelectLogSourceCombo.BackColor = Color.Red;
                    return null;
                }
                if (LogIdsRangeAnyRB.Checked)
                {
                    Query.MinLogId = -1;
                    Query.MaxLogId = -1;
                }
                else
                {
                    if (LogIdsRangeAboveCB.Visible)
                    {
                        if (LogIdsRangeAboveCB.Checked)
                        {
                            try
                            {
                                Query.MinLogId = Int32.Parse(LogIdsRangeAboveTB.Text.Trim());
                            }
                            catch
                            {
                                ShowStep0Error("Log ID range lower limit is not a valid number");
                                LogIdsRangeAboveTB.BackColor = Color.Red;
                                return null;
                            }
                        }
                        if (LogIdsRangeBelowCB.Checked)
                        {
                            try
                            {
                                Query.MaxLogId = Int32.Parse(LogIdsRangeBelowTB.Text.Trim());
                            }
                            catch
                            {
                                ShowStep0Error("Log ID range upper limit is not a valid number");
                                LogIdsRangeBelowTB.BackColor = Color.Red;
                                return null;
                            }
                        }
                        else if (LogIdsRangeAboveCB.Checked)
                        {
                            Query.MaxLogId = Config.GetLastLogId(Query.LogSource);
                        }
                    }
                    else
                    {
                        try
                        {
                            Query.ScanLogId = Int32.Parse(LogIdsRangeAboveTB.Text.Trim());
                        }
                        catch
                        {
                            ShowStep0Error("Scan ID is not a valid number");
                            LogIdsRangeAboveTB.BackColor = Color.Red;
                            return null;
                        }
                    }

                }
            }
            else
            {
                Query.LogSource = this.CurrentSearchSource;
                Query.LogIds = new List<int>(this.CurrentSearchResultsIds);
            }
            if (MatchKeywordCB.Checked)
            {
                if (MatchKeywordInRequestHeadersSectionCB.Checked || MatchKeywordInRequestBodySectionCB.Checked || MatchKeywordInResponseHeadersSectionCB.Checked || MatchKeywordInResponseBodySectionCB.Checked)
                {
                    if (MatchKeywordTB.Text.Length > 0)
                    {
                        Query.Keyword = MatchKeywordTB.Text;
                        Query.SearchRequestHeaders = MatchKeywordInRequestHeadersSectionCB.Checked;
                        Query.SearchRequestBody = MatchKeywordInRequestBodySectionCB.Checked;
                        Query.SearchResponseHeaders = MatchKeywordInResponseHeadersSectionCB.Checked;
                        Query.SearchResponseBody = MatchKeywordInResponseBodySectionCB.Checked;
                    }
                    else
                    {
                        ShowStep0Error("Keyword search is enabled but the keyword is empty");
                        MatchKeywordTB.BackColor = Color.Red;
                        return null;
                    }
                }
                else
                {
                    ShowStep0Error("Keyword search is enabled but no sections are selected");
                    return null;
                }
            }
            if (MatchRequestUrlCB.Checked)
            {
                if (MatchRequestUrlTypeCombo.SelectedIndex > -1)
                {
                    if (MatchRequestUrlKeywordTB.Text.Length > 0)
                    {
                        Query.UrlMatchMode = MatchRequestUrlTypeCombo.SelectedIndex;
                        Query.UrlMatchString = MatchRequestUrlKeywordTB.Text;
                    }
                    else
                    {
                        ShowStep0Error("Url match is enabled but the match keyword is empty");
                        MatchRequestUrlKeywordTB.BackColor = Color.Red;
                        return null;
                    }
                }
                else
                {
                    ShowStep0Error("Invalid Url match type is selected");
                    MatchRequestUrlTypeCombo.BackColor = Color.Red;
                    return null;
                }
            }
            if (MatchRequestMethodsCB.Checked)
            {
                string[] Methods = MatchRequestMethodsTB.Text.Split(new char[]{','});
                for (int i = 0; i < Methods.Length; i++)
                {
                    Methods[i] = Methods[i].Trim();
                }
                if (Methods.Length > 0)
                {
                    if (MatchRequestMethodsPlusRB.Checked)
                    {
                        Query.MethodsToCheck = new List<string>(Methods);
                    }
                    else
                    {
                        Query.MethodsToIgnore = new List<string>(Methods);
                    }
                }
                else
                {
                    ShowStep0Error("Request Methods match is selected but no methods specified Invalid Url match type is selected");
                    MatchRequestMethodsTB.BackColor = Color.Red;
                    return null;
                }
            }
            if (MatchResponseCodesCB.Checked)
            {
                string[] ResponseCodesStrings = MatchResponseCodesTB.Text.Split(new char[] { ',' });
                List<int> ResponseCodes = new List<int>();
                foreach (string CodeString in ResponseCodesStrings)
                {
                    try
                    {
                        ResponseCodes.Add(Int32.Parse(CodeString));
                    }
                    catch { }
                }
                if (ResponseCodes.Count > 0)
                {
                    if (MatchResponseCodesPlusRB.Checked)
                    {
                        Query.CodesToCheck = new List<int>(ResponseCodes);
                    }
                    else
                    {
                        Query.CodesToIgnore = new List<int>(ResponseCodes);
                    }
                }
                else
                {
                    ShowStep0Error("Response Codes match is selected but no methods specified Invalid Url match type is selected");
                    MatchResponseCodesTB.BackColor = Color.Red;
                    return null;
                }
            }
            if (MatchHostNamesCB.Checked)
            {
                string[] HostNames = MatchHostNamesTB.Text.Split(new char[] { ',' });
                for (int i = 0; i < HostNames.Length; i++)
                {
                    HostNames[i] = HostNames[i].Trim();
                }
                if (HostNames.Length > 0)
                {
                    if (MatchHostNamesPlusRB.Checked)
                    {
                        Query.HostNamesToCheck = new List<string>(HostNames);
                    }
                    else
                    {
                        Query.HostNamesToIgnore = new List<string>(HostNames);
                    }
                }
                else
                {
                    ShowStep0Error("Hostnames match is selected but no methods specified Invalid Url match type is selected");
                    MatchHostNamesTB.BackColor = Color.Red;
                    return null;
                }
            }
            if (MatchFileExtensionsCB.Checked)
            {
                string[] FileExtensions = MatchFileExtensionsTB.Text.Split(new char[] { ',' });
                for (int i = 0; i < FileExtensions.Length; i++)
                {
                    FileExtensions[i] = FileExtensions[i].Trim();
                }
                if (FileExtensions.Length > 0)
                {
                    if (MatchFileExtensionsPlusRB.Checked)
                    {
                        Query.FileExtensionsToCheck = new List<string>(FileExtensions);
                    }
                    else
                    {
                        Query.FileExtensionsToIgnore = new List<string>(FileExtensions);
                    }
                }
                else
                {
                    ShowStep0Error("Request File Extensions match is selected but no methods specified Invalid Url match type is selected");
                    MatchFileExtensionsTB.BackColor = Color.Red;
                    return null;
                }
            }
            if (Query.Keyword.Length > 0 || Query.UrlMatchString.Length > 0
                || Query.FileExtensionsToCheck.Count > 0 || Query.FileExtensionsToIgnore.Count > 0
                || Query.MethodsToCheck.Count > 0 || Query.MethodsToIgnore.Count > 0
                || Query.CodesToCheck.Count > 0 || Query.CodesToIgnore.Count > 0
                || Query.HostNamesToCheck.Count > 0 || Query.HostNamesToIgnore.Count > 0)
            {
                return Query;
            }
            else
            {
                ShowStep0Error("No options selected in the filter settings");
                return null;
            }
        }

        private void LogIdsRangeAboveTB_TextChanged(object sender, EventArgs e)
        {
            if (LogIdsRangeAboveTB.BackColor != Color.White)
                LogIdsRangeAboveTB.BackColor = Color.White;
        }

        private void LogIdsRangeBelowTB_TextChanged(object sender, EventArgs e)
        {
            if (LogIdsRangeBelowTB.BackColor != Color.White)
                LogIdsRangeBelowTB.BackColor = Color.White;
        }

        private void MatchKeywordTB_TextChanged(object sender, EventArgs e)
        {
            if (MatchKeywordTB.BackColor != Color.White)
                MatchKeywordTB.BackColor = Color.White;
        }

        private void MatchRequestUrlKeywordTB_TextChanged(object sender, EventArgs e)
        {
            if (MatchRequestUrlKeywordTB.BackColor != Color.White)
                MatchRequestUrlKeywordTB.BackColor = Color.White;
        }

        private void MatchRequestUrlTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MatchRequestUrlTypeCombo.BackColor != Color.White)
                MatchRequestUrlTypeCombo.BackColor = Color.White;
        }

        private void MatchRequestMethodsTB_TextChanged(object sender, EventArgs e)
        {
            if (MatchRequestMethodsTB.BackColor != Color.White)
                MatchRequestMethodsTB.BackColor = Color.White;
        }

        private void MatchResponseCodesTB_TextChanged(object sender, EventArgs e)
        {
            if (MatchResponseCodesTB.BackColor != Color.White)
                MatchResponseCodesTB.BackColor = Color.White;
        }

        private void MatchHostNamesTB_TextChanged(object sender, EventArgs e)
        {
            if (MatchHostNamesTB.BackColor != Color.White)
                MatchHostNamesTB.BackColor = Color.White;
        }

        private void MatchFileExtensionsTB_TextChanged(object sender, EventArgs e)
        {
            if (MatchFileExtensionsTB.BackColor != Color.White)
                MatchFileExtensionsTB.BackColor = Color.White;
        }

        private void SelectLogSourceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectLogSourceCombo.BackColor != Color.White)
                SelectLogSourceCombo.BackColor = Color.White;

            if (SelectLogSourceCombo.SelectedIndex == 3)
            {
                LogIdsRangeCustomRB.Text = "Only for Scan ID -";
                LogIdsRangeAboveTB.Enabled = true;
                LogIdsRangeAboveCB.Visible = false;
                LogIdsRangeBelowCB.Visible = false;
                LogIdsRangeBelowTB.Visible = false;
            }
            else
            {
                LogIdsRangeCustomRB.Text = "Only in range -";
                LogIdsRangeAboveTB.Enabled = LogIdsRangeAboveCB.Checked;
                LogIdsRangeAboveCB.Visible = true;
                LogIdsRangeBelowCB.Visible = true;
                LogIdsRangeBelowTB.Visible = true;
            }
        }

        delegate void ShowStep0Status_d(string Text);
        void ShowStep0Status(string Text)
        {
            if (LogGrid.InvokeRequired)
            {
                ShowStep0Status_d CALL_d = new ShowStep0Status_d(ShowStep0Status);
                LogGrid.Invoke(CALL_d, new object[] { Text });
            }
            else
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
        }
        delegate void ShowStep0Error_d(string Text);
        void ShowStep0Error(string Text)
        {
            if (LogGrid.InvokeRequired)
            {
                ShowStep0Error_d CALL_d = new ShowStep0Error_d(ShowStep0Error);
                LogGrid.Invoke(CALL_d, new object[] { Text });
            }
            else
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
        }

        private void LogAnalyzer_Load(object sender, EventArgs e)
        {
            SelectLogSourceCombo.Items.Add("Proxy");
            SelectLogSourceCombo.Items.Add("Test");
            SelectLogSourceCombo.Items.Add("Shell");
            SelectLogSourceCombo.Items.Add("Scan");
            SelectLogSourceCombo.Items.Add("Probe");
            SelectLogSourceCombo.Items.AddRange(Config.GetOtherSourceList().ToArray());
            SelectLogSourceCombo.SelectedIndex = 0;

            MatchRequestUrlTypeCombo.SelectedIndex = 0;
        }

        private void MatchRequestMethodsPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchRequestMethodsPlusRB.Checked)
            {
                MatchRequestMethodsCB.Text = "Only these Request Methods:";
            }
        }

        private void MatchRequestMethodsMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchRequestMethodsMinusRB.Checked)
            {
                MatchRequestMethodsCB.Text = "Except these Request Methods:";
            }
        }

        private void MatchResponseCodesPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchResponseCodesPlusRB.Checked)
            {
                MatchResponseCodesCB.Text = "Only these Response Codes:";
            }
        }

        private void MatchResponseCodesMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchResponseCodesMinusRB.Checked)
            {
                MatchResponseCodesCB.Text = "Except these Response Codes:";
            }
        }

        private void MatchHostNamesPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchHostNamesPlusRB.Checked)
            {
                MatchHostNamesCB.Text = "Only these Hostnames:";
            }
        }

        private void MatchHostNamesMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchHostNamesMinusRB.Checked)
            {
                MatchHostNamesCB.Text = "Except these Hostnames:";
            }
        }

        private void MatchFileExtensionsPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchFileExtensionsPlusRB.Checked)
            {
                MatchFileExtensionsCB.Text = "Only these Request File Extensions:";
            }
        }

        private void MatchFileExtensionsMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (MatchFileExtensionsMinusRB.Checked)
            {
                MatchFileExtensionsCB.Text = "Except these Request File Extensions:";
            }
        }

        private void LogIdsRangeAnyRB_CheckedChanged(object sender, EventArgs e)
        {
            LogIdsRangeAboveCB.Enabled = false;
            LogIdsRangeAboveTB.Enabled = false;

            LogIdsRangeBelowCB.Enabled = false;
            LogIdsRangeBelowTB.Enabled = false;
        }

        private void LogIdsRangeCustomRB_CheckedChanged(object sender, EventArgs e)
        {
            LogIdsRangeAboveCB.Enabled = true;
            if (SelectLogSourceCombo.SelectedIndex == 3)//LogSource-Scan
                LogIdsRangeAboveTB.Enabled = true;
            else
                LogIdsRangeAboveTB.Enabled = LogIdsRangeAboveCB.Checked;

            LogIdsRangeBelowCB.Enabled = true;
            LogIdsRangeBelowTB.Enabled = LogIdsRangeBelowCB.Checked;
        }

        private void SearchTypeChainedRB_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchTypeChainedRB.Checked)
            {
                LogSourceAndIdsPanel.Visible = false;
            }
        }

        private void SearchTypeNewRB_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchTypeNewRB.Checked)
            {
                LogSourceAndIdsPanel.Visible = true;
            }
        }

        private void LogIdsRangeAboveCB_CheckedChanged(object sender, EventArgs e)
        {
            LogIdsRangeAboveTB.Enabled = LogIdsRangeAboveCB.Checked;
        }

        private void LogIdsRangeBelowCB_CheckedChanged(object sender, EventArgs e)
        {
            LogIdsRangeBelowTB.Enabled = LogIdsRangeBelowCB.Checked;
        }

        private void SelectLogSourceCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void MatchRequestUrlTypeCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void MatchKeywordInAllSectionsCB_Click(object sender, EventArgs e)
        {
            MatchKeywordInRequestHeadersSectionCB.Checked = MatchKeywordInAllSectionsCB.Checked;
            MatchKeywordInRequestBodySectionCB.Checked = MatchKeywordInAllSectionsCB.Checked;
            MatchKeywordInResponseHeadersSectionCB.Checked = MatchKeywordInAllSectionsCB.Checked;
            MatchKeywordInResponseBodySectionCB.Checked = MatchKeywordInAllSectionsCB.Checked;
        }

        private void MatchKeywordInRequestHeadersSectionCB_Click(object sender, EventArgs e)
        {
            if (!MatchKeywordInRequestHeadersSectionCB.Checked)
                MatchKeywordInAllSectionsCB.Checked = false;
        }

        private void MatchKeywordInRequestBodySectionCB_Click(object sender, EventArgs e)
        {
            if (!MatchKeywordInRequestBodySectionCB.Checked)
                MatchKeywordInAllSectionsCB.Checked = false;
        }

        private void MatchKeywordInResponseHeadersSectionCB_Click(object sender, EventArgs e)
        {
            if (!MatchKeywordInResponseHeadersSectionCB.Checked)
                MatchKeywordInAllSectionsCB.Checked = false;
        }

        private void MatchKeywordInResponseBodySectionCB_Click(object sender, EventArgs e)
        {
            if (!MatchKeywordInResponseBodySectionCB.Checked)
                MatchKeywordInAllSectionsCB.Checked = false;
        }

        private void ClickActionSelectLogRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ClickActionSelectLogRB.Checked)
                SelectAllCB.Enabled = true;
        }

        private void ClickActionDisplayLogRB_CheckedChanged(object sender, EventArgs e)
        {
            //if(ClickActionDisplayLogRB.Checked)
            //    SelectAllCB.Enabled = false;
        }

        private void LogGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (LogGrid.Rows.Count == 0)
            {
                SelectedRowsCount = 0;
                DoDiffBtn.Enabled = false;
                StartScanBtn.Enabled = false;
                StartTestBtn.Enabled = false;
                RunModulesBtn.Enabled = false;
            }
        }

        private void LogAnalyzer_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopSearch();
        }

        private void DoDiffBtn_Click(object sender, EventArgs e)
        {
            if (SelectedRowsCount == 2)
            {
                int ALogId = -1;
                int BLogId = -1;
                foreach (DataGridViewRow Row in LogGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        if (ALogId == -1)
                        {
                            ALogId = (int)Row.Cells[1].Value;
                        }
                        else if (BLogId == -1)
                        {
                            BLogId = (int)Row.Cells[1].Value;
                            break;
                        }
                    }
                }
                SessionsDiffer Sdiff = new SessionsDiffer();
                Sdiff.SetSessions(this.CurrentSearchSource, ALogId, BLogId);
                Sdiff.Show();
            }
            else
            {
                MessageBox.Show(string.Format("Diff can be done only when two sessions are selected. You have selected {0} sessions", SelectedRowsCount), "Selection Error");
            }
        }

        private void StartScanBtn_Click(object sender, EventArgs e)
        {
            if (IsScanWindowOpen())
            {
                SSW.Activate();
            }
            else
            {
                
                List<int> IDs = new List<int>();
                foreach (DataGridViewRow Row in LogGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                        IDs.Add((int)Row.Cells[1].Value);
                }
                SSW = new ScanSelectedWizard();
                SSW.SetSourceAndLogs(this.CurrentSearchSource, IDs);
                SSW.Show();
            }
        }

        bool IsScanWindowOpen()
        {
            if (SSW == null)
            {
                return false;
            }
            else if (SSW.IsDisposed)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void MatchKeywordCB_CheckedChanged(object sender, EventArgs e)
        {
            MatchKeywordInAllSectionsCB.Enabled = MatchKeywordCB.Checked;
            MatchKeywordInRequestHeadersSectionCB.Enabled = MatchKeywordCB.Checked;
            MatchKeywordInRequestBodySectionCB.Enabled = MatchKeywordCB.Checked;
            MatchKeywordInResponseHeadersSectionCB.Enabled = MatchKeywordCB.Checked;
            MatchKeywordInResponseBodySectionCB.Enabled = MatchKeywordCB.Checked;
            MatchKeywordTB.Enabled = MatchKeywordCB.Checked;
        }

        private void MatchRequestUrlCB_CheckedChanged(object sender, EventArgs e)
        {
            MatchRequestUrlTypeCombo.Enabled = MatchRequestUrlCB.Checked;
            MatchRequestUrlKeywordTB.Enabled = MatchRequestUrlCB.Checked;
        }

        private void MatchRequestMethodsCB_CheckedChanged(object sender, EventArgs e)
        {
            MatchRequestMethodsPlusRB.Enabled = MatchRequestMethodsCB.Checked;
            MatchRequestMethodsMinusRB.Enabled = MatchRequestMethodsCB.Checked;
            MatchRequestMethodsTB.Enabled = MatchRequestMethodsCB.Checked;
        }

        private void MatchResponseCodesCB_CheckedChanged(object sender, EventArgs e)
        {
            MatchResponseCodesPlusRB.Enabled = MatchResponseCodesCB.Checked;
            MatchResponseCodesMinusRB.Enabled = MatchResponseCodesCB.Checked;
            MatchResponseCodesTB.Enabled = MatchResponseCodesCB.Checked;
        }

        private void MatchHostNamesCB_CheckedChanged(object sender, EventArgs e)
        {
            MatchHostNamesPlusRB.Enabled = MatchHostNamesCB.Checked;
            MatchHostNamesMinusRB.Enabled = MatchHostNamesCB.Checked;
            MatchHostNamesTB.Enabled = MatchHostNamesCB.Checked;
        }

        private void MatchFileExtensionsCB_CheckedChanged(object sender, EventArgs e)
        {
            MatchFileExtensionsPlusRB.Enabled = MatchFileExtensionsCB.Checked;
            MatchFileExtensionsMinusRB.Enabled = MatchFileExtensionsCB.Checked;
            MatchFileExtensionsTB.Enabled = MatchFileExtensionsCB.Checked;
        }

        private void RunModulesBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modules that support this feature are currently not available.");
        }
    }
}
