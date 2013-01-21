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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
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

namespace IronWASP
{
    public partial class ScanBranchWizard : Form
    {
        internal Request BaseRequest;
        
        int CurrentStep = 0;

        public ScanBranchWizard()
        {
            InitializeComponent();
        }

        private void ScanBranchPickProxyLogCB_CheckedChanged(object sender, EventArgs e)
        {
            ScanBranch.PickFromProxyLog = ScanBranchPickProxyLogCB.Checked;
        }

        private void ScanBranchPickProbeLogCB_CheckedChanged(object sender, EventArgs e)
        {
            ScanBranch.PickFromProbeLog = ScanBranchPickProbeLogCB.Checked;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (ScanBranch.ScanThread != null)
            {
                try
                {
                    ScanBranch.ScanThread.Abort();
                }
                catch
                {
                    //
                }
            }
            ScanBranchWizard.ActiveForm.Close();
        }

        private void FinalBtn_Click(object sender, EventArgs e)
        {
            FinalBtn.Enabled = false;
            StepFourPreviousBtn.Enabled = false;

            ScanBranchErrorTB.Text = "";
            if (ScanBranchHostNameTB.Text.Trim().Length == 0)
            {
                ScanBranchErrorTB.Text = "No HostName Specified";
                FinalBtn.Enabled = true;
                StepFourPreviousBtn.Enabled = true;
                return;
            }
            if (ScanBranchUrlPatternTB.Text.Trim().Length == 0)
            {
                ScanBranchErrorTB.Text = "No Url Pattern Specified";
                FinalBtn.Enabled = true;
                StepFourPreviousBtn.Enabled = true;
                return;
            }
            if (!(ScanBranchPickProxyLogCB.Checked || ScanBranchPickProbeLogCB.Checked))
            {
                ScanBranchErrorTB.Text = "Log source not selected. Select from Proxy Log/Probe Log";
                FinalBtn.Enabled = true;
                StepFourPreviousBtn.Enabled = true;
                return;
            }

            ScanBranch.ProxyLogIDs.Clear();
            ScanBranch.ProbeLogIDs.Clear();
            UpdateScanBranchConfigFromUI();
            ScanBranchStatsPanel.Visible = true;
            ScanBranchProgressLbl.Text = "Selecting requests based on filter";
            if (ScanBranch.PickFromProxyLog)
            {
                if (IronUI.UI.ProxyLogGrid.Rows.Count == 0 && !ScanBranch.PickFromProbeLog)
                {
                    ScanBranchErrorTB.Text = "Proxy Log is Empty. Capture Some Traffic with the Proxy First";
                    ScanBranchStatsPanel.Visible = false;
                    FinalBtn.Enabled = true;
                    StepFourPreviousBtn.Enabled = true;
                    return;
                }
                foreach (DataGridViewRow Row in IronUI.UI.ProxyLogGrid.Rows)
                {
                    try
                    {
                        if (ScanBranch.CanScan(Row, "Proxy"))
                        {
                            ScanBranch.ProxyLogIDs.Add((int)Row.Cells[0].Value);
                        }
                    }
                    catch (Exception Exp)
                    {
                        IronException.Report("ScanBranch Error reading ProxyLogGrid Message", Exp.Message, Exp.StackTrace);
                    }
                }
                if (ScanBranch.ProxyLogIDs.Count == 0 && !ScanBranch.PickFromProbeLog)
                {
                    ScanBranchErrorTB.Text = "No Requests were Selected. Try Changing the Filter or Capture More Traffic With the Proxy";
                    ScanBranchStatsPanel.Visible = false;
                    FinalBtn.Enabled = true;
                    StepFourPreviousBtn.Enabled = true;
                    return;
                }
            }
            if (ScanBranch.PickFromProbeLog)
            {
                if (IronUI.UI.ProbeLogGrid.Rows.Count == 0 && !ScanBranch.PickFromProxyLog)
                {
                    ScanBranchErrorTB.Text = "Probe Log is Empty. Crawl a website to populate the Probe Log";
                    ScanBranchStatsPanel.Visible = false;
                    FinalBtn.Enabled = true;
                    StepFourPreviousBtn.Enabled = true;
                    return;
                }
                foreach (DataGridViewRow Row in IronUI.UI.ProbeLogGrid.Rows)
                {
                    try
                    {
                        if (ScanBranch.CanScan(Row, "Probe"))
                        {
                            ScanBranch.ProbeLogIDs.Add((int)Row.Cells[0].Value);
                        }
                    }
                    catch (Exception Exp)
                    {
                        IronException.Report("ScanBranch Error reading ProbeLogGrid Message", Exp.Message, Exp.StackTrace);
                    }
                }
                if (ScanBranch.ProbeLogIDs.Count == 0 && !ScanBranch.PickFromProxyLog)
                {
                    ScanBranchErrorTB.Text = "No Requests were Selected. Try Changing the Filter or Crawl more of the site.";
                    ScanBranchStatsPanel.Visible = false;
                    FinalBtn.Enabled = true;
                    StepFourPreviousBtn.Enabled = true;
                    return;
                }
            }
            if (ScanBranch.ProxyLogIDs.Count == 0 && ScanBranch.ProbeLogIDs.Count == 0)
            {
                ScanBranchErrorTB.Text = "No Requests were Selected. Try Changing the Filter or make sure there are Requests in the Proxy/Probe Logs";
                ScanBranchStatsPanel.Visible = false;
                FinalBtn.Enabled = true;
                StepFourPreviousBtn.Enabled = true;
                return;
            }
            ScanBranchProgressBar.Minimum = 0;
            ScanBranchProgressBar.Maximum = ScanBranch.ProxyLogIDs.Count + ScanBranch.ProbeLogIDs.Count;
            ScanBranchProgressBar.Step = 1;
            ScanBranchProgressBar.Value = 0;
            ScanBranchProgressLbl.Text = ScanBranch.ProxyLogIDs.Count.ToString() + " Requests Selected";
            IronUI.UI.ASMainTabs.SelectTab(0);
            if (!IronUI.UI.main_tab.SelectedTab.Name.Equals("mt_auto")) IronUI.UI.main_tab.SelectTab("mt_auto");
            ScanBranch.Start();
        }

        private void ScanBranchWizard_Load(object sender, EventArgs e)
        {
            foreach (string Name in ActivePlugin.List())
            {
                ScanPluginsGrid.Rows.Add(new object[]{true, Name});
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
                    case ("prompt_user"):
                        PromptUserCB.Checked = (Node.InnerText == "true");
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
                else if (!Info.Name.EndsWith(".ibst"))
                {
                    MessageBox.Show("The file extension must be .ibst");
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
            XW.WriteStartElement("scan_type"); XW.WriteValue("2"); XW.WriteEndElement();
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

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (BaseTabs.SelectedTab.Name)
            {
                case ("Zero"):
                    if (this.CurrentStep != 0) this.BaseTabs.SelectTab(this.CurrentStep);
                    break;
                case ("One"):
                    if (this.CurrentStep != 1) this.BaseTabs.SelectTab(this.CurrentStep);
                    break;
                case ("Two"):
                    if (this.CurrentStep != 2) this.BaseTabs.SelectTab(this.CurrentStep);
                    break;
                case ("Three"):
                    if (this.CurrentStep != 3) this.BaseTabs.SelectTab(this.CurrentStep);
                    break;
                case ("Four"):
                    if (this.CurrentStep != 4) this.BaseTabs.SelectTab(this.CurrentStep);
                    break;
            }
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

        delegate void ShowStep0Error_d(string Text);
        internal void ShowStep0Error(string Text)
        {
            if (Step0StatusTB.InvokeRequired)
            {
                ShowStep0Error_d CALL_d = new ShowStep0Error_d(ShowStep0Error);
                Step0StatusTB.Invoke(CALL_d, new object[] { Text });
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

        internal void UpdateScanBranchConfigFromUI()
        {
            ScanBranch.HostName = this.BaseRequest.Host;
            ScanBranch.UrlPattern = this.ScanBranchUrlPatternTB.Text;
            if (this.BaseRequest != null)
            {
                ScanBranch.HTTP = !this.BaseRequest.SSL;
                ScanBranch.HTTPS = this.BaseRequest.SSL;
            }

            ScanBranch.PromptUser = PromptUserCB.Checked;

            ScanBranch.ScanUrl = this.InjectUrlPathPartsCB.Checked;
            ScanBranch.ScanQuery = this.InjectQueryCB.Checked;
            ScanBranch.ScanBody = this.InjectBodyCB.Checked;
            ScanBranch.ScanCookie = this.InjectCookieCB.Checked;
            ScanBranch.ScanHeaders = this.InjectHeadersCB.Checked;

            ScanBranch.PickFromProxyLog = this.ScanBranchPickProxyLogCB.Checked;
            ScanBranch.PickFromProbeLog = this.ScanBranchPickProbeLogCB.Checked;
            ScanBranch.ProxyLogIDs.Clear();
            ScanBranch.ProbeLogIDs.Clear();

            ScanBranch.SessionPlugin = "";
            if (this.ScanBranchSessionPluginsCombo.SelectedItem != null)
            {
                string PluginName = this.ScanBranchSessionPluginsCombo.SelectedItem.ToString();
                if (PluginName.Length > 0)
                {
                    if (SessionPlugin.List().Contains(PluginName))
                    {
                        ScanBranch.SessionPlugin = PluginName;
                        if (ScanThreadLimitCB.Checked)
                        {
                            Scanner.MaxParallelScanCount = 1;
                            IronUI.UpdateScannerSettingsInUIFromConfig();
                            IronDB.StoreScannerSettings();
                        }
                    }
                }
            }

            ScanBranch.FormatPlugins.Clear();

            foreach (DataGridViewRow Row in this.FormatGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    ScanBranch.FormatPlugins.Add(Row.Cells[1].Value.ToString());
                }
            }

            ScanBranch.ActivePlugins.Clear();

            foreach (DataGridViewRow Row in this.ScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    ScanBranch.ActivePlugins.Add(Row.Cells[1].Value.ToString());
                }
            }


            ScanBranch.QueryWhiteList.Clear();
            ScanBranch.QueryBlackList.Clear();

            ScanBranch.BodyWhiteList.Clear();
            ScanBranch.BodyBlackList.Clear();

            ScanBranch.CookieWhiteList.Clear();
            ScanBranch.CookieBlackList.Clear();

            ScanBranch.HeaderWhiteList.Clear();
            ScanBranch.HeaderBlackList.Clear();

            if (QueryParametersFilterCB.Checked)
            {
                if (QueryParametersPlusRB.Checked)
                {
                    foreach (string Name in QueryParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.QueryWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in QueryParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.QueryBlackList.Add(Name.Trim());
                    }
                }
            }

            if (BodyParametersFilterCB.Checked)
            {
                if (BodyParametersPlusRB.Checked)
                {
                    foreach (string Name in BodyParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.BodyWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in BodyParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.BodyBlackList.Add(Name.Trim());
                    }
                }
            }

            if (CookieParametersFilterCB.Checked)
            {
                if (CookieParametersPlusRB.Checked)
                {
                    foreach (string Name in CookieParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.CookieWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in CookieParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.CookieBlackList.Add(Name.Trim());
                    }
                }
            }

            if (HeadersParametersFilterCB.Checked)
            {
                if (HeadersParametersPlusRB.Checked)
                {
                    foreach (string Name in HeadersParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.HeaderWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in HeadersParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanBranch.HeaderBlackList.Add(Name.Trim());
                    }
                }
            }
        }

        private void LaunchSessionPluginCreationAssistantLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SessionPluginCreationAssistant SPCA = new SessionPluginCreationAssistant();
            SPCA.Show();
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

        private void RefreshSessListLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ScanBranchSessionPluginsCombo.Items.Clear();
            ScanBranchSessionPluginsCombo.Items.AddRange(SessionPlugin.List().ToArray());
        }

        private void ScanAllPluginsCB_Click(object sender, EventArgs e)
        {         
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                Row.Cells[0].Value = ScanAllPluginsCB.Checked;
            }
        }
    }
}
