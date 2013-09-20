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
using System.IO;
using System.Xml;
using System.Threading;

namespace IronWASP
{
    public partial class ScanSelectedWizard : Form
    {
        bool CanClose = false;

        int CurrentStep = 0;
        
        Thread ScanCreationThread;

        int TotalScans = 0;
        int ScanDone = 0;

        List<int> LogIdsToScan = new List<int>();
        string LogSource = "";

        internal bool ScanUrl = true;
        internal bool ScanQuery = true;
        internal bool ScanBody = true;
        internal bool ScanCookie = true;
        internal bool ScanHeaders = true;

        internal string SelectedSessionPlugin = "";
        internal List<string> FormatPlugins = new List<string>();

        internal List<string> ActivePlugins = new List<string>();

        internal List<string> QueryWhiteList = new List<string>();
        internal List<string> QueryBlackList = new List<string>();

        internal List<string> BodyWhiteList = new List<string>();
        internal List<string> BodyBlackList = new List<string>();

        internal List<string> CookieWhiteList = new List<string>();
        internal List<string> CookieBlackList = new List<string>();

        internal List<string> HeaderWhiteList = new List<string>();
        internal List<string> HeaderBlackList = new List<string>();


        public ScanSelectedWizard()
        {
            InitializeComponent();
        }

        private void ScanSelectedWizard_Load(object sender, EventArgs e)
        {
            foreach (string Name in ActivePlugin.List())
            {
                ScanPluginsGrid.Rows.Add(new object[] { true, Name });
            }
            foreach (string Name in FormatPlugin.List())
            {
                FormatGrid.Rows.Add(new object[] { true, Name });
            }
            ScanBranchSessionPluginsCombo.Items.AddRange(SessionPlugin.List().ToArray());

            if (InjectQueryCB.Checked) QueryParametersFilterCB.Enabled = true;
            if (InjectBodyCB.Checked) BodyParametersFilterCB.Enabled = true;
            if (InjectCookieCB.Checked) CookieParametersFilterCB.Enabled = true;
            if (InjectHeadersCB.Checked) HeadersParametersFilterCB.Enabled = true;
        }

        internal void SetSourceAndLogs(string Source, List<int> LogIds)
        {
            this.LogSource = Source;
            this.LogIdsToScan.AddRange(LogIds);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.CanClose = true;
            this.Close();
        }

        private void StepZeroNextBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab(1);
        }

        private void StepOnePreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 0;
            this.BaseTabs.SelectTab(0);
        }

        private void StepOneNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep1Error("");
            string Msg = CheckStep1Input();
            if (Msg.Length == 0)
            {
                this.CurrentStep = 2;
                this.BaseTabs.SelectTab(2);
            }
            else
            {
                ShowStep1Error(Msg);
            }
        }

        private void StepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab(1);
        }

        private void StepTwoNextBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 3;
            this.BaseTabs.SelectTab(3);
        }

        private void StepThreePreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 2;
            this.BaseTabs.SelectTab(2);
        }

        private void StepThreeNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep3Error("");
            string Msg = CheckStep3Input();
            if (Msg.Length == 0)
            {
                this.CurrentStep = 4;
                this.BaseTabs.SelectTab(4);
            }
            else
            {
                ShowStep3Error(Msg);
            }
        }

        private void StepFourPreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 3;
            this.BaseTabs.SelectTab(3);
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
        void ShowStep2Error(string Text)
        {
            this.Step2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step2StatusTB.Visible = false;
            }
            else
            {
                this.Step2StatusTB.ForeColor = Color.Red;
                this.Step2StatusTB.Visible = true;
            }
        }
        void ShowStep3Error(string Text)
        {
            this.Step1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step3StatusTB.Visible = false;
            }
            else
            {
                this.Step3StatusTB.ForeColor = Color.Red;
                this.Step3StatusTB.Visible = true;
            }
        }
        void ShowStep4Error(string Text)
        {
            this.Step2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step2StatusTB.Visible = false;
            }
            else
            {
                this.Step2StatusTB.ForeColor = Color.Red;
                this.Step2StatusTB.Visible = true;
            }
        }

        string CheckStep1Input()
        {
            if (!(InjectQueryCB.Checked || InjectBodyCB.Checked || InjectCookieCB.Checked || InjectHeadersCB.Checked || InjectUrlPathPartsCB.Checked || InjectNamesCB.Checked))
            {
                return "Atleat one section of the request must be selected for scanning.";
            }
            if (InjectQueryCB.Checked) QueryParametersFilterCB.Checked = true;
            if (InjectBodyCB.Checked) BodyParametersFilterCB.Checked = true;
            if (InjectCookieCB.Checked) CookieParametersFilterCB.Checked = true;
            if (InjectHeadersCB.Checked) HeadersParametersFilterCB.Checked = true;

            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    return "";
                }
            }
            return "Atleat one security check must be selected for scanning.";
        }

        string CheckStep3Input()
        {
            if (ScanBranchSessionPluginsCombo.Text.Trim().Length > 0 && !SessionPlugin.List().Contains(ScanBranchSessionPluginsCombo.Text))
            {
                return "Session Plugin value is invalid, either enter a valid Plugin name or leave this value blank";
            }
            return "";
        }

        private void FinalBtn_Click(object sender, EventArgs e)
        {
            if (FinalBtn.Text.Equals("Close"))
            {
                this.CanClose = true;
                this.Close();
                return;
            }

            if (ScanCreationThread != null)
            {
                try
                {
                    ScanCreationThread.Abort();
                }
                catch { }
            }

            UpdateScanBranchConfigFromUI();

            ScanBranchProgressBar.Minimum = 0;
            ScanBranchProgressBar.Maximum = LogIdsToScan.Count;
            ScanBranchProgressBar.Step = 1;
            ScanBranchProgressBar.Value = 0;
            ScanBranchProgressLbl.Text = LogIdsToScan.Count.ToString() + " Requests Selected";
            ScanBranchStatsPanel.Visible = true;

            TotalScans = LogIdsToScan.Count;

            ScanCreationThread = new Thread(CreateScans);
            ScanCreationThread.Start();

            StepFourPreviousBtn.Enabled = false;
            FinalBtn.Enabled = false;
        }

        void CreateScans()
        {
            foreach (int LogId in LogIdsToScan)
            {
                try
                {
                    Request Req = Request.FromLog(LogId, this.LogSource);
                    CreateScan(Req);
                }
                catch { }
            }
            UpdateScanBranchStats(0, 0, "All Scan Jobs Created && Queued. Close this Window.", false, true);
        }

        void CreateScan(Request Req)
        {
            Scanner Scan = new Scanner(Req);
            Scan = SetSessionPlugin(Scan);
            Scan = SetFormatPlugin(Scan);
            Scan = AddActivePlugins(Scan);
            Scan = SetInjectionPoints(Scan);
            if (Scan.InjectionPointsCount == 0)
            {
                TotalScans--;
                UpdateScanBranchStats(ScanDone, TotalScans, "Skipping Request as no Injection Points were Identified...." + ScanDone.ToString() + "/" + TotalScans.ToString() + " done", true, false);
                return;
            }
            Scan.LaunchScan();
            ScanDone++;
            UpdateScanBranchStats(ScanDone, TotalScans, "Creating and Queueing Scans...." + ScanDone.ToString() + "/" + TotalScans.ToString() + " done", true, false);

        }

        Scanner SetFormatPlugin(Scanner S)
        {
            Request RequestToScan = S.OriginalRequest;

            if (!FormatPlugin.IsNormal(RequestToScan))
            {
                string FPName = FormatPlugin.Get(RequestToScan, FormatPlugins);
                if (FPName.Length > 0 && FPName != "Normal")
                {
                    S.BodyFormat = FormatPlugin.Get(FPName);
                }
            }
            return S;
        }

        Scanner SetInjectionPoints(Scanner S)
        {
            if (ScanQuery)
            {
                if (QueryWhiteList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Query.GetNames())
                    {
                        if (QueryWhiteList.Contains(Name)) S.InjectQuery(Name);
                    }
                }
                else if (QueryBlackList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Query.GetNames())
                    {
                        if (!QueryBlackList.Contains(Name)) S.InjectQuery(Name);
                    }
                }
                else
                {
                    S.InjectQuery();
                }
            }

            if (ScanBody)
            {
                if (BodyWhiteList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Body.GetNames())
                    {
                        if (BodyWhiteList.Contains(Name)) S.InjectBody(Name);
                    }
                }
                else if (BodyBlackList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Body.GetNames())
                    {
                        if (!BodyBlackList.Contains(Name)) S.InjectBody(Name);
                    }
                }
                else
                {
                    S.InjectBody();
                }
            }

            if (ScanCookie)
            {
                if (CookieWhiteList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Cookie.GetNames())
                    {
                        if (CookieWhiteList.Contains(Name)) S.InjectCookie(Name);
                    }
                }
                else if (CookieBlackList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Cookie.GetNames())
                    {
                        if (!CookieBlackList.Contains(Name)) S.InjectCookie(Name);
                    }
                }
                else
                {
                    S.InjectCookie();
                }
            }

            if (ScanHeaders)
            {
                if (HeaderWhiteList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Headers.GetNames())
                    {
                        if (HeaderWhiteList.Contains(Name)) S.InjectHeaders(Name);
                    }
                }
                else if (HeaderBlackList.Count > 0)
                {
                    foreach (string Name in S.OriginalRequest.Headers.GetNames())
                    {
                        if (!HeaderBlackList.Contains(Name)) S.InjectHeaders(Name);
                    }
                }
                else
                {
                    S.InjectHeaders();
                }
            }

            if (ScanUrl)
            {
                if (S.OriginalRequest.Query.Count == 0 && S.OriginalRequest.File.Length == 0)
                    S.InjectUrl();
            }

            return S;
        }

        Scanner AddActivePlugins(Scanner Scan)
        {
            foreach (string Name in ActivePlugins)
            {
                Scan.AddCheck(Name);
            }
            return Scan;
        }

        Scanner SetSessionPlugin(Scanner Scan)
        {
            if (SelectedSessionPlugin.Length > 0)
            {
                Scan.SessionHandler = SessionPlugin.Get(SelectedSessionPlugin);
            }
            else
            {
                Scan.SessionHandler = new SessionPlugin();
            }
            return Scan;
        }

        internal void UpdateScanBranchConfigFromUI()
        {

            this.ScanUrl = this.InjectUrlPathPartsCB.Checked;
            this.ScanQuery = this.InjectQueryCB.Checked;
            this.ScanBody = this.InjectBodyCB.Checked;
            this.ScanCookie = this.InjectCookieCB.Checked;
            this.ScanHeaders = this.InjectHeadersCB.Checked;

             this.SelectedSessionPlugin = "";
            if (this.ScanBranchSessionPluginsCombo.SelectedItem != null)
            {
                string PluginName = this.ScanBranchSessionPluginsCombo.SelectedItem.ToString();
                if (PluginName.Length > 0)
                {
                    if (SessionPlugin.List().Contains(PluginName))
                    {
                        this.SelectedSessionPlugin = PluginName;
                        if (ScanThreadLimitCB.Checked)
                        {
                            Scanner.MaxParallelScanCount = 1;
                            IronUI.UpdateScannerSettingsInUIFromConfig();
                            IronDB.StoreScannerSettings();
                        }
                    }
                }
            }

            this.FormatPlugins.Clear();

            foreach (DataGridViewRow Row in this.FormatGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    this.FormatPlugins.Add(Row.Cells[1].Value.ToString());
                }
            }

            this.ActivePlugins.Clear();

            foreach (DataGridViewRow Row in this.ScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    this.ActivePlugins.Add(Row.Cells[1].Value.ToString());
                }
            }


            this.QueryWhiteList.Clear();
            this.QueryBlackList.Clear();

            this.BodyWhiteList.Clear();
            this.BodyBlackList.Clear();

            this.CookieWhiteList.Clear();
            this.CookieBlackList.Clear();

            this.HeaderWhiteList.Clear();
            this.HeaderBlackList.Clear();

            if (QueryParametersFilterCB.Checked)
            {
                if (QueryParametersPlusRB.Checked)
                {
                    foreach (string Name in QueryParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.QueryWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in QueryParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.QueryBlackList.Add(Name.Trim());
                    }
                }
            }

            if (BodyParametersFilterCB.Checked)
            {
                if (BodyParametersPlusRB.Checked)
                {
                    foreach (string Name in BodyParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.BodyWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in BodyParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.BodyBlackList.Add(Name.Trim());
                    }
                }
            }

            if (CookieParametersFilterCB.Checked)
            {
                if (CookieParametersPlusRB.Checked)
                {
                    foreach (string Name in CookieParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.CookieWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in CookieParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.CookieBlackList.Add(Name.Trim());
                    }
                }
            }

            if (HeadersParametersFilterCB.Checked)
            {
                if (HeadersParametersPlusRB.Checked)
                {
                    foreach (string Name in HeadersParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.HeaderWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in HeadersParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.HeaderBlackList.Add(Name.Trim());
                    }
                }
            }
        }

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (BaseTabs.SelectedTab.Name)
            {
                case ("Zero"):
                    if (this.CurrentStep != 0)
                    {
                        this.BaseTabs.SelectTab(this.CurrentStep);
                        MessageBox.Show("Use the 'Next Step ->' and 'Previous Step ->' buttons on the bottom left and right corners of this window for navigation.");
                    }
                    break;
                case ("One"):
                    if (this.CurrentStep != 1)
                    {
                        this.BaseTabs.SelectTab(this.CurrentStep);
                        MessageBox.Show("Use the 'Next Step ->' and 'Previous Step ->' buttons on the bottom left and right corners of this window for navigation.");
                    }
                    break;
                case ("Two"):
                    if (this.CurrentStep != 2)
                    {
                        this.BaseTabs.SelectTab(this.CurrentStep);
                        MessageBox.Show("Use the 'Next Step ->' and 'Previous Step ->' buttons on the bottom left and right corners of this window for navigation.");
                    }
                    break;
                case ("Three"):
                    if (this.CurrentStep != 3)
                    {
                        this.BaseTabs.SelectTab(this.CurrentStep);
                        MessageBox.Show("Use the 'Next Step ->' and 'Previous Step ->' buttons on the bottom left and right corners of this window for navigation.");
                    }
                    break;
                case ("Four"):
                    if (this.CurrentStep != 4)
                    {
                        this.BaseTabs.SelectTab(this.CurrentStep);
                        MessageBox.Show("Use the 'Next Step ->' and 'Previous Step ->' buttons on the bottom left and right corners of this window for navigation.");
                    }
                    break;
            }
        }

        private void FormatGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FormatGrid.SelectedRows == null) return;
            if (FormatGrid.SelectedRows.Count == 0) return;

            if ((bool)FormatGrid.SelectedRows[0].Cells[0].Value)
            {
                FormatGrid.SelectedRows[0].Cells[0].Value = false;
            }
            else
            {
                FormatGrid.SelectedRows[0].Cells[0].Value = true;
            }
        }

        private void ScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanPluginsGrid.SelectedRows == null) return;
            if (ScanPluginsGrid.SelectedRows.Count == 0) return;

            if ((bool)ScanPluginsGrid.SelectedRows[0].Cells[0].Value)
            {
                ScanPluginsGrid.SelectedRows[0].Cells[0].Value = false;
                ScanAllPluginsCB.Checked = false;
            }
            else
            {
                ScanPluginsGrid.SelectedRows[0].Cells[0].Value = true;
            }
        }

        void RefreshInjectNamesText()
        {
            StringBuilder SB = new StringBuilder();

            if (InjectQueryCB.Checked)
            {
                if (SB.Length > 0) SB.Append(", ");
                SB.Append("Query");
            }
            if (InjectBodyCB.Checked)
            {
                if (SB.Length > 0) SB.Append(", ");
                SB.Append("Body");
            }
            if (InjectCookieCB.Checked)
            {
                if (SB.Length > 0) SB.Append(", ");
                SB.Append("Cookie");
            }
            if (InjectHeadersCB.Checked)
            {
                if (SB.Length > 0) SB.Append(", ");
                SB.Append("Header");
            }

            InjectNamesCB.Text = string.Format("Parameter Names of {0}", SB.ToString());
        }

        private void InjectQueryCB_CheckedChanged(object sender, EventArgs e)
        {
            RefreshInjectNamesText();
            if (InjectQueryCB.Checked)
            {
                QueryParametersFilterCB.Enabled = true;
            }
            else
            {
                QueryParametersFilterCB.Checked = false;
                QueryParametersFilterCB.Enabled = false;
            }
        }

        private void InjectBodyCB_CheckedChanged(object sender, EventArgs e)
        {
            RefreshInjectNamesText();
            if (InjectBodyCB.Checked)
            {
                BodyParametersFilterCB.Enabled = true;
            }
            else
            {
                BodyParametersFilterCB.Checked = false;
                BodyParametersFilterCB.Enabled = false;
            }
        }

        private void InjectCookieCB_CheckedChanged(object sender, EventArgs e)
        {
            RefreshInjectNamesText();
            if (InjectCookieCB.Checked)
            {
                CookieParametersFilterCB.Enabled = true;
            }
            else
            {
                CookieParametersFilterCB.Checked = false;
                CookieParametersFilterCB.Enabled = false;
            }
        }

        private void InjectHeadersCB_CheckedChanged(object sender, EventArgs e)
        {
            RefreshInjectNamesText();
            if (InjectHeadersCB.Checked)
            {
                HeadersParametersFilterCB.Enabled = true;
            }
            else
            {
                HeadersParametersFilterCB.Checked = false;
                HeadersParametersFilterCB.Enabled = false;
            }
        }

        private void QueryParametersFilterCB_CheckedChanged(object sender, EventArgs e)
        {
            if (QueryParametersFilterCB.Checked)
            {
                QueryParametersPlusRB.Enabled = true;
                QueryParametersMinusRB.Enabled = true;
                QueryParametersPlusTB.Enabled = true;
                QueryParametersMinusTB.Enabled = true;
            }
            else
            {
                QueryParametersPlusRB.Enabled = false;
                QueryParametersMinusRB.Enabled = false;
                QueryParametersPlusTB.Enabled = false;
                QueryParametersMinusTB.Enabled = false;
            }
        }

        private void BodyParametersFilterCB_CheckedChanged(object sender, EventArgs e)
        {
            if (BodyParametersFilterCB.Checked)
            {
                BodyParametersPlusRB.Enabled = true;
                BodyParametersMinusRB.Enabled = true;
                BodyParametersPlusTB.Enabled = true;
                BodyParametersMinusTB.Enabled = true;
            }
            else
            {
                BodyParametersPlusRB.Enabled = false;
                BodyParametersMinusRB.Enabled = false;
                BodyParametersPlusTB.Enabled = false;
                BodyParametersMinusTB.Enabled = false;
            }
        }

        private void CookieParametersFilterCB_CheckedChanged(object sender, EventArgs e)
        {
            if (CookieParametersFilterCB.Checked)
            {
                CookieParametersPlusRB.Enabled = true;
                CookieParametersMinusRB.Enabled = true;
                CookieParametersPlusTB.Enabled = true;
                CookieParametersMinusTB.Enabled = true;
            }
            else
            {
                CookieParametersPlusRB.Enabled = false;
                CookieParametersMinusRB.Enabled = false;
                CookieParametersPlusTB.Enabled = false;
                CookieParametersMinusTB.Enabled = false;
            }
        }

        private void HeadersParametersFilterCB_CheckedChanged(object sender, EventArgs e)
        {
            if (HeadersParametersFilterCB.Checked)
            {
                HeadersParametersPlusRB.Enabled = true;
                HeadersParametersMinusRB.Enabled = true;
                HeadersParametersPlusTB.Enabled = true;
                HeadersParametersMinusTB.Enabled = true;
            }
            else
            {
                HeadersParametersPlusRB.Enabled = false;
                HeadersParametersMinusRB.Enabled = false;
                HeadersParametersPlusTB.Enabled = false;
                HeadersParametersMinusTB.Enabled = false;
            }
        }

        private void QueryParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (QueryParametersPlusRB.Checked)
            {
                QueryParametersFilterCB.Text = "Only scan these Query Parameters:";
            }
        }

        private void BodyParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (BodyParametersPlusRB.Checked)
            {
                BodyParametersFilterCB.Text = "Only scan these Body Parameters:";
            }
        }

        private void CookieParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (CookieParametersPlusRB.Checked)
            {
                CookieParametersFilterCB.Text = "Only scan these Cookie Parameters:";
            }
        }

        private void HeadersParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (HeadersParametersPlusRB.Checked)
            {
                HeadersParametersFilterCB.Text = "Only scan these Header Parameters:";
            }
        }

        private void QueryParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (QueryParametersMinusRB.Checked)
            {
                QueryParametersFilterCB.Text = "Don't scan these Query Parameters:";
            }
        }

        private void BodyParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (BodyParametersMinusRB.Checked)
            {
                BodyParametersFilterCB.Text = "Don't scan these Body Parameters:";
            }
        }

        private void CookieParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (CookieParametersMinusRB.Checked)
            {
                CookieParametersFilterCB.Text = "Don't scan these Cookie Parameters:";
            }
        }

        private void HeadersParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (HeadersParametersMinusRB.Checked)
            {
                HeadersParametersFilterCB.Text = "Don't scan these Header Parameters:";
            }
        }

        private void LoadTemplateLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenTemplateDialog.Title = "Open an existing Scan Template";

            while (OpenTemplateDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo OpenedFile = new FileInfo(OpenTemplateDialog.FileName);
                    StreamReader Reader = new StreamReader(OpenedFile.FullName);
                    string TemplateXml = Reader.ReadToEnd();
                    Reader.Close();
                    LoadSettingsFromTemplate(TemplateXml);
                    ScanTemplateNameLbl.Text = string.Format("Loaded template file - {0}", OpenedFile.Name);
                    break;
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(string.Format("Unable to open file: {0}", new object[] { Exp.Message }));
                }
            }
        }

        void LoadSettingsFromTemplate(string TemplateXml)
        {
            XmlDocument XDoc = new XmlDocument();
            XDoc.LoadXml(TemplateXml);
            foreach (XmlNode Node in XDoc.ChildNodes[1].ChildNodes)
            {
                switch (Node.Name)
                {
                    case ("scan_type"):
                        break;
                    case ("inject_upp"):
                        InjectUrlPathPartsCB.Checked = (Node.InnerText == "true");
                        break;
                    case ("inject_query"):
                        InjectQueryCB.Checked = (Node.InnerText == "true");
                        break;
                    case ("inject_body"):
                        InjectBodyCB.Checked = (Node.InnerText == "true");
                        break;
                    case ("inject_cookie"):
                        InjectCookieCB.Checked = (Node.InnerText == "true");
                        break;
                    case ("inject_header"):
                        InjectHeadersCB.Checked = (Node.InnerText == "true");
                        break;
                    case ("inject_names"):
                        InjectNamesCB.Checked = (Node.InnerText == "true");
                        break;
                    case ("checks"):
                        int CheckedCount = 0;
                        foreach (string PluginName in Node.InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
                            {
                                if (Row.Cells[1].Value.ToString().Equals(PluginName.Trim()))
                                {
                                    Row.Cells[0].Value = true;
                                    CheckedCount++;
                                }
                            }
                        }
                        if (CheckedCount == ScanPluginsGrid.Rows.Count)
                            ScanAllPluginsCB.Checked = true;
                        else
                            ScanAllPluginsCB.Checked = false;
                        break;
                    case ("formats"):
                        foreach (string PluginName in Node.InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            foreach (DataGridViewRow Row in FormatGrid.Rows)
                            {
                                if (Row.Cells[1].Value.ToString().Equals(PluginName.Trim()))
                                {
                                    Row.Cells[0].Value = true;
                                }
                            }
                        }
                        break;
                    case ("session_plugin"):
                        try
                        {
                            ScanBranchSessionPluginsCombo.SelectedIndex = ScanBranchSessionPluginsCombo.Items.IndexOf(Node.InnerText);
                        }
                        catch { }
                        break;
                    case ("query_white_list"):
                        QueryParametersPlusTB.Text = Node.InnerText;
                        QueryParametersPlusRB.Checked = true;
                        break;
                    case ("query_black_list"):
                        QueryParametersMinusTB.Text = Node.InnerText;
                        QueryParametersMinusRB.Checked = true;
                        break;
                    case ("body_white_list"):
                        BodyParametersPlusTB.Text = Node.InnerText;
                        BodyParametersPlusRB.Checked = true;
                        break;
                    case ("body_black_list"):
                        BodyParametersMinusTB.Text = Node.InnerText;
                        BodyParametersMinusRB.Checked = true;
                        break;
                    case ("cookie_white_list"):
                        CookieParametersPlusTB.Text = Node.InnerText;
                        CookieParametersPlusRB.Checked = true;
                        break;
                    case ("cookie_black_list"):
                        CookieParametersMinusTB.Text = Node.InnerText;
                        CookieParametersMinusRB.Checked = true;
                        break;
                    case ("headers_white_list"):
                        HeadersParametersPlusTB.Text = Node.InnerText;
                        HeadersParametersPlusRB.Checked = true;
                        break;
                    case ("headers_black_list"):
                        HeadersParametersMinusTB.Text = Node.InnerText;
                        HeadersParametersMinusRB.Checked = true;
                        break;
                    case ("query_filter_type"):
                        if (Node.InnerText.Equals("+"))
                            QueryParametersPlusRB.Checked = true;
                        else
                            QueryParametersMinusRB.Checked = true;
                        break;
                    case ("body_filter_type"):
                        if (Node.InnerText.Equals("+"))
                            BodyParametersPlusRB.Checked = true;
                        else
                            BodyParametersMinusRB.Checked = true;
                        break;
                    case ("cookie_filter_type"):
                        if (Node.InnerText.Equals("+"))
                            CookieParametersPlusRB.Checked = true;
                        else
                            CookieParametersMinusRB.Checked = true;
                        break;
                    case ("headers_filter_type"):
                        if (Node.InnerText.Equals("+"))
                            HeadersParametersPlusRB.Checked = true;
                        else
                            HeadersParametersMinusRB.Checked = true;
                        break;
                }
            }
        }

        private void SaveTemplateLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            while (SaveTemplateDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo Info = new FileInfo(SaveTemplateDialog.FileName);
                string Content = GetTemplateXml();
                if (Info.Name.Length == 0)
                {
                    MessageBox.Show("Please enter a name");
                }
                else if (!Info.Name.EndsWith(".isst"))
                {
                    MessageBox.Show("The file extension must be .isst");
                }
                else
                {
                    try
                    {
                        StreamWriter Writer = new StreamWriter(Info.FullName);
                        Writer.Write(Content);
                        Writer.Close();
                    }
                    catch (Exception Exp)
                    {
                        MessageBox.Show(string.Format("Unable to save file: {0}", new object[] { Exp.Message }));
                    }
                    break;
                }
            }
        }

        string GetTemplateXml()
        {
            StringBuilder SB = new StringBuilder();
            XmlWriterSettings XWS = new XmlWriterSettings();
            XWS.Indent = true;
            XmlWriter XW = XmlWriter.Create(SB, XWS);
            XW.WriteStartDocument();
            XW.WriteStartElement("scan_template");
            XW.WriteStartElement("version"); XW.WriteValue("1.0"); XW.WriteEndElement();
            XW.WriteStartElement("scan_type"); XW.WriteValue("3"); XW.WriteEndElement();
            //Crawler settings

            //Selected Injection Points
            XW.WriteStartElement("inject_upp"); XW.WriteValue(InjectUrlPathPartsCB.Checked); XW.WriteEndElement();
            XW.WriteStartElement("inject_query"); XW.WriteValue(InjectQueryCB.Checked); XW.WriteEndElement();
            XW.WriteStartElement("inject_body"); XW.WriteValue(InjectBodyCB.Checked); XW.WriteEndElement();
            XW.WriteStartElement("inject_cookie"); XW.WriteValue(InjectCookieCB.Checked); XW.WriteEndElement();
            XW.WriteStartElement("inject_header"); XW.WriteValue(InjectHeadersCB.Checked); XW.WriteEndElement();
            XW.WriteStartElement("inject_names"); XW.WriteValue(InjectNamesCB.Checked); XW.WriteEndElement();
            //Selected checks
            XW.WriteStartElement("checks");
            StringBuilder CB = new StringBuilder();
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    if (CB.Length > 0) CB.Append(", ");
                    CB.Append(Row.Cells[1].Value.ToString());
                }
            }
            XW.WriteValue(CB.ToString());
            XW.WriteEndElement();

            //Selected formats
            XW.WriteStartElement("checks");
            StringBuilder FB = new StringBuilder();
            foreach (DataGridViewRow Row in FormatGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    if (FB.Length > 0) FB.Append(", ");
                    FB.Append(Row.Cells[1].Value.ToString());
                }
            }
            XW.WriteValue(FB.ToString());
            XW.WriteEndElement();
            if (ScanBranchSessionPluginsCombo.SelectedIndex > -1)
            {
                XW.WriteStartElement("session_plugin"); XW.WriteValue(ScanBranchSessionPluginsCombo.Text); XW.WriteEndElement();
            }

            XW.WriteStartElement("query_white_list"); XW.WriteValue(QueryParametersPlusTB.Text); XW.WriteEndElement();
            XW.WriteStartElement("query_black_list"); XW.WriteValue(QueryParametersMinusTB.Text); XW.WriteEndElement();

            XW.WriteStartElement("body_white_list"); XW.WriteValue(BodyParametersPlusTB.Text); XW.WriteEndElement();
            XW.WriteStartElement("body_black_list"); XW.WriteValue(BodyParametersMinusTB.Text); XW.WriteEndElement();

            XW.WriteStartElement("cookie_white_list"); XW.WriteValue(CookieParametersPlusTB.Text); XW.WriteEndElement();
            XW.WriteStartElement("cookie_black_list"); XW.WriteValue(CookieParametersMinusTB.Text); XW.WriteEndElement();

            XW.WriteStartElement("headers_white_list"); XW.WriteValue(HeadersParametersPlusTB.Text); XW.WriteEndElement();
            XW.WriteStartElement("headers_black_list"); XW.WriteValue(HeadersParametersMinusTB.Text); XW.WriteEndElement();

            if (QueryParametersPlusRB.Checked)
            {
                XW.WriteStartElement("query_filter_type"); XW.WriteValue("+"); XW.WriteEndElement();
            }
            else
            {
                XW.WriteStartElement("query_filter_type"); XW.WriteValue("-"); XW.WriteEndElement();
            }

            if (BodyParametersPlusRB.Checked)
            {
                XW.WriteStartElement("body_filter_type"); XW.WriteValue("+"); XW.WriteEndElement();
            }
            else
            {
                XW.WriteStartElement("body_filter_type"); XW.WriteValue("-"); XW.WriteEndElement();
            }

            if (CookieParametersPlusRB.Checked)
            {
                XW.WriteStartElement("cookie_filter_type"); XW.WriteValue("+"); XW.WriteEndElement();
            }
            else
            {
                XW.WriteStartElement("cookie_filter_type"); XW.WriteValue("-"); XW.WriteEndElement();
            }

            if (HeadersParametersPlusRB.Checked)
            {
                XW.WriteStartElement("headers_filter_type"); XW.WriteValue("+"); XW.WriteEndElement();
            }
            else
            {
                XW.WriteStartElement("headers_filter_type"); XW.WriteValue("-"); XW.WriteEndElement();
            }

            XW.WriteEndElement();
            XW.WriteEndDocument();
            XW.Close();
            return SB.ToString();
        }

        private void LaunchSessionPluginCreationAssistantLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SessionPluginCreationAssistant SPCA = new SessionPluginCreationAssistant();
            SPCA.Show();
        }

        private void RefreshSessListLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ScanBranchSessionPluginsCombo.Items.Clear();
            ScanBranchSessionPluginsCombo.Items.AddRange(SessionPlugin.List().ToArray());
        }

        delegate void UpdateScanBranchStats_d(int ScanDone, int TotalScans, string Message, bool Progress, bool CloseWindow);
        internal void UpdateScanBranchStats(int ScanDone, int TotalScans, string Message, bool Progress, bool CloseWindow)
        {
            if (ScanBranchProgressLbl.InvokeRequired)
            {
                UpdateScanBranchStats_d USBS_d = new UpdateScanBranchStats_d(UpdateScanBranchStats);
                ScanBranchProgressLbl.Invoke(USBS_d, new object[] { ScanDone, TotalScans, Message, Progress, CloseWindow });
            }
            else
            {
                if (Progress) ScanBranchProgressBar.PerformStep();
                ScanBranchProgressLbl.Text = Message;
                if (ScanDone == TotalScans)
                {
                    FinalBtn.Text = "Close";
                    FinalBtn.Enabled = true;
                }
                if (CloseWindow)
                {
                    this.CanClose = true;
                    this.Close();
                }
            }
        }

        private void ScanAllPluginsCB_Click(object sender, EventArgs e)
        {         
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                Row.Cells[0].Value = ScanAllPluginsCB.Checked;
            }
        }

        private void ScanSelectedWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IronUI.UI.CanShutdown) return;
            if (!CanClose)
            {
                if (this.CurrentStep == 0)
                {
                    this.CanClose = true;
                }
                else if (this.CurrentStep == 4)
                {
                    e.Cancel = true;
                    if (StepFourPreviousBtn.Enabled)
                    {
                        MessageBox.Show("This window can only be closed from the first step.\r\nUse the '<- Previous Step' button on the bottom left corner to go to the first step and then press the 'Cancel' button on the bottom left corner.");
                    }
                    else
                    {
                        MessageBox.Show("This window cannot be closed by the user now.\r\nAfter all scan jobs are created the window will automatically close.");
                    }
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("This window can only be closed from the first step.\r\nUse the '<- Previous Step' button on the bottom left corner to go to the first step and then press the 'Cancel' button on the bottom left corner.");
                }
            }
        }
    }
}
