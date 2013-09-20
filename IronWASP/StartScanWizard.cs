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
using System.Xml;
using System.IO;

namespace IronWASP
{
    internal partial class StartScanWizard : Form
    {
        bool CanClose = false;

        internal Request BaseRequest;
        Thread T;
        
        int CurrentStep = 0;
        
        public StartScanWizard()
        {
            InitializeComponent();
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

        private void StartScanWizard_Load(object sender, EventArgs e)
        {
            foreach (string Name in ActivePlugin.List())
            {
                ScanPluginsGrid.Rows.Add(new object[]{true, Name});
            }
            if (InjectQueryCB.Checked) QueryParametersFilterCB.Enabled = true;
            if (InjectBodyCB.Checked) BodyParametersFilterCB.Enabled = true;
            if (InjectCookieCB.Checked) CookieParametersFilterCB.Enabled = true;
            if (InjectHeadersCB.Checked) HeadersParametersFilterCB.Enabled = true;
            if(BaseRequest != null)
            {
                StartingUrlTB.Text = BaseRequest.Url;
                BaseUrlTB.Text = "/";
                if (BaseRequest.SSL)
                {
                    CrossProtoQuestionLbl.Text = string.Format(CrossProtoQuestionLbl.Text, "HTTP");
                }
                else
                {
                    CrossProtoQuestionLbl.Text = string.Format(CrossProtoQuestionLbl.Text, "HTTPS");
                }
            }
            Step1StatusLbl.Text = "Checking if the provided Url is reachable, please wait...";
            Step1Progress.Visible = true;
            T = new Thread(CheckTargetConnectivity);
            T.Start();
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
                case("One"):
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
                case ("Five"):
                    if (this.CurrentStep != 5)
                    {
                        this.BaseTabs.SelectTab(this.CurrentStep);
                        MessageBox.Show("Use the 'Next Step ->' and 'Previous Step ->' buttons on the bottom left and right corners of this window for navigation.");
                    }
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
            this.CurrentStep = 2;
            this.BaseTabs.SelectTab(2);
        }

        private void StepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab(1);
        }

        private void StepTwoNextBtn_Click(object sender, EventArgs e)
        {
            string Msg = CheckStep1Input();
            if (Msg.Length == 0)
            {
                if (CrawlOnlyRB.Checked)
                {
                    this.CurrentStep = 5;
                    this.BaseTabs.SelectTab(5);
                }
                else
                {
                    this.CurrentStep = 3;
                    this.BaseTabs.SelectTab(3);
                }
            }
            else
            {
                ShowStep2Error(Msg);
            }
        }

        private void StepThreePreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 2;
            this.BaseTabs.SelectTab(2);
        }

        private void StepThreeNextBtn_Click(object sender, EventArgs e)
        {
            string Msg = CheckStep2Input();
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

        private void StepFourNextBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 5;
            this.BaseTabs.SelectTab(5);
        }

        private void StepFivePreviousBtn_Click(object sender, EventArgs e)
        {
            if (CrawlOnlyRB.Checked)
            {
                this.CurrentStep = 2;
                this.BaseTabs.SelectTab(2);
            }
            else
            {
                this.CurrentStep = 4;
                this.BaseTabs.SelectTab(4);
            }
        }

        string CheckStep1Input()
        {
            if (!BaseUrlTB.Text.StartsWith("/"))
            {
                BaseUrlTB.BackColor = Color.Red;
                return "value must start with /";
            }
            if (!StartingUrlTB.Text.StartsWith("/"))
            {
                StartingUrlTB.BackColor = Color.Red;
                return "value must start with /";
            }
            return "";
        }

        string CheckStep2Input()
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

        delegate void ConnectivityCheckEnd_d(bool Success);
        internal void ConnectivityCheckEnd(bool Success)
        {
            if (Step1StatusLbl.InvokeRequired)
            {
                ConnectivityCheckEnd_d CALL_d = new ConnectivityCheckEnd_d(ConnectivityCheckEnd);
                Step1StatusLbl.Invoke(CALL_d, new object[] { Success });
            }
            else
            {
                if (Success)
                {
                    Step1Progress.Visible = false;
                    Step1StatusLbl.Text = "Congrats! Url is reachable, proceed to the next step.";
                    StepZeroNextBtn.Enabled = true;
                }
                else
                {
                    IronUI.UI.ConsoleScanUrlTB.ReadOnly = false;
                    this.CloseWindow();
                }
            }
        }

        delegate void ShowStep0Message_d(string Text);
        internal void ShowStep0Message(string Text)
        {
            if (Step1StatusLbl.InvokeRequired)
            {
                ShowStep0Message_d CALL_d = new ShowStep0Message_d(ShowStep0Message);
                Step1StatusLbl.Invoke(CALL_d, new object[] { Text });
            }
            else
            {
                Step1StatusLbl.Text = Text;
            }
        }

        delegate void ShowStep0Error_d(string Text);
        internal void ShowStep0Error(string Text)
        {
            if (Step1StatusLbl.InvokeRequired)
            {
                ShowStep0Error_d CALL_d = new ShowStep0Error_d(ShowStep0Error);
                Step1StatusLbl.Invoke(CALL_d, new object[] { Text });
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
            this.Step3StatusTB.Text = Text;
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
            this.Step4StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step4StatusTB.Visible = false;
            }
            else
            {
                this.Step4StatusTB.ForeColor = Color.Red;
                this.Step4StatusTB.Visible = true;
            }
        }

        private void ConfigureScanBaseUrlTB_TextChanged(object sender, EventArgs e)
        {
            if (BaseUrlTB.BackColor == Color.Red)
            {
                BaseUrlTB.BackColor = Color.White;
            }
        }

        private void ConfigureScanStartingUrlTB_TextChanged(object sender, EventArgs e)
        {
            if (StartingUrlTB.BackColor == Color.Red)
            {
                StartingUrlTB.BackColor = Color.White;
            }
        }

        void CheckTargetConnectivity()
        {
            try
            {
                BaseRequest.Source = RequestSource.Probe;
                Response Res = BaseRequest.Send();
                this.ConnectivityCheckEnd(true);
            }
            catch (Exception Exp)
            {
                IronUI.ShowConsoleStatus(string.Format("Url is not reachable. Error: {0}", Exp.Message), true);
                this.ConnectivityCheckEnd(false);
            }
        }

        private void ConfigCrawlerThreadMaxCountTB_Scroll(object sender, EventArgs e)
        {
            ConfigCrawlerThreadMaxCountLbl.Text = ConfigCrawlerThreadMaxCountTB.Value.ToString();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            IronUI.UI.ConsoleScanUrlTB.ReadOnly = false;
            this.CloseWindow();
        }

        private void QueryParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (QueryParametersMinusRB.Checked)
            {
                QueryParametersFilterCB.Text = "Don't scan these Query Parameters:";
            }
        }

        private void QueryParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (QueryParametersPlusRB.Checked)
            {
                QueryParametersFilterCB.Text = "Only scan these Query Parameters:";
            }
        }

        private void BodyParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (BodyParametersMinusRB.Checked)
            {
                BodyParametersFilterCB.Text = "Don't scan these Body Parameters:";
            }
        }

        private void BodyParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (BodyParametersPlusRB.Checked)
            {
                BodyParametersFilterCB.Text = "Only scan these Body Parameters:";
            }
        }

        private void CookieParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (CookieParametersMinusRB.Checked)
            {
                CookieParametersFilterCB.Text = "Don't scan these Cookie Parameters:";
            }
        }

        private void CookieParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (CookieParametersPlusRB.Checked)
            {
                CookieParametersFilterCB.Text = "Only scan these Cookie Parameters:";
            }
        }

        private void HeadersParametersMinusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (HeadersParametersMinusRB.Checked)
            {
                HeadersParametersFilterCB.Text = "Don't scan these Header Parameters:";
            }
        }

        private void HeadersParametersPlusRB_CheckedChanged(object sender, EventArgs e)
        {
            if (HeadersParametersPlusRB.Checked)
            {
                HeadersParametersFilterCB.Text = "Only scan these Header Parameters:";
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
                else if (!Info.Name.EndsWith(".ifst"))
                {
                    MessageBox.Show("The file extension must be .ifst");
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
            XW.WriteStartElement("scan_type"); XW.WriteValue("1"); XW.WriteEndElement();
            //Crawler settings
            XW.WriteStartElement("crawler_threads"); XW.WriteValue(ConfigCrawlerThreadMaxCountTB.Value); XW.WriteEndElement();
            XW.WriteStartElement("user_agent"); XW.WriteValue(ConfigCrawlerUserAgentTB.Text); XW.WriteEndElement();
            XW.WriteStartElement("prompt_user"); XW.WriteValue(ConfigCrawlerUserAgentTB.Text); XW.WriteEndElement();
            
            if (UseSpecialHeaderCB.Checked)
            {
                XW.WriteStartElement("spl_header_name"); XW.WriteValue(SpecialHeaderNameTB.Text); XW.WriteEndElement();
                XW.WriteStartElement("spl_header_value"); XW.WriteValue(SpecialHeaderValueTB.Text); XW.WriteEndElement();
            }
            XW.WriteStartElement("do_dir_guessing"); XW.WriteValue(ConfigureScanDirAndFileGuessingCB.Checked); XW.WriteEndElement();
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
                    case ("crawler_threads"):
                        try
                        {
                            ConfigCrawlerThreadMaxCountTB.Value = Int32.Parse(Node.InnerText);
                        }
                        catch { }
                        break;
                    case ("user_agent"):
                        ConfigCrawlerUserAgentTB.Text = Node.InnerText;
                        break;
                    case ("spl_header_name"):
                        UseSpecialHeaderCB.Checked = true;
                        SpecialHeaderNameTB.Text = Node.InnerText;
                        break;
                    case ("spl_header_value"):
                        SpecialHeaderValueTB.Text = Node.InnerText;
                        break;
                    case ("do_dir_guessing"):
                        ConfigureScanDirAndFileGuessingCB.Checked = (Node.InnerText == "true");
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
                        foreach(string PluginName in Node.InnerText.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
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
                            QueryParametersPlusRB.Checked =  true;
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

        private void FinalBtn_Click(object sender, EventArgs e)
        {
            StartScan();
        }

        void StartScan()
        {
            ScanManager.Stop(true);
            ScanManager.PrimaryHost = BaseRequest.Host;
            ScanManager.BaseUrl = BaseUrlTB.Text;
            ScanManager.StartingUrl = StartingUrlTB.Text;
            ScanManager.Mode = ScanMode.UserConfigured;
            ScanManager.PerformDirAndFileGuessing = ConfigureScanDirAndFileGuessingCB.Checked;
            ScanManager.CanPromptUser = PromptUserCB.Checked;

            Crawler.MaxCrawlThreads = ConfigCrawlerThreadMaxCountTB.Value;
            Crawler.UserAgent = ConfigCrawlerUserAgentTB.Text;

            if (UseSpecialHeaderCB.Checked)
            {
                ScanManager.SpecialHeader = new string[]{SpecialHeaderNameTB.Text, SpecialHeaderValueTB.Text};
            }

            if (BaseRequest.SSL)
            {
                ScanManager.HTTPS = true;
                if (CrossProtoYesRB.Checked) ScanManager.HTTP = true;
            }
            else
            {
                ScanManager.HTTP = true;
                if (CrossProtoYesRB.Checked) ScanManager.HTTPS = true;
            }
            ScanManager.Checks.Clear();
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value) ScanManager.Checks.Add(Row.Cells[1].Value.ToString());
            }

            ScanManager.InjectUrlPathParts = InjectUrlPathPartsCB.Checked;
            ScanManager.InjectQuery = InjectQueryCB.Checked;
            ScanManager.InjectBody = InjectBodyCB.Checked;
            ScanManager.InjectCookie = InjectCookieCB.Checked;
            ScanManager.InjectHeaders = InjectHeadersCB.Checked;

            if (InjectNamesCB.Checked)
            {
                ScanManager.InjectQueryName = InjectUrlPathPartsCB.Checked;
                ScanManager.InjectBodyName = InjectBodyCB.Checked;
                ScanManager.InjectCookieName = InjectCookieCB.Checked;
                ScanManager.InjectHeaderName = InjectHeadersCB.Checked;
            }
            else
            {
                ScanManager.InjectQueryName = false;
                ScanManager.InjectBodyName = false;
                ScanManager.InjectCookieName = false;
                ScanManager.InjectHeaderName = false;
            }

            ScanManager.QueryWhiteList.Clear();
            ScanManager.QueryBlackList.Clear();

            ScanManager.BodyWhiteList.Clear();
            ScanManager.BodyBlackList.Clear();
            
            ScanManager.CookieWhiteList.Clear();
            ScanManager.CookieBlackList.Clear();
            
            ScanManager.HeaderWhiteList.Clear();
            ScanManager.HeaderBlackList.Clear();

            if (QueryParametersFilterCB.Checked)
            {
                if (QueryParametersPlusRB.Checked)
                {
                    foreach (string Name in QueryParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.QueryWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in QueryParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.QueryBlackList.Add(Name.Trim());
                    }
                }
            }

            if (BodyParametersFilterCB.Checked)
            {
                if (BodyParametersPlusRB.Checked)
                {
                    foreach (string Name in BodyParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.BodyWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in BodyParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.BodyBlackList.Add(Name.Trim());
                    }
                }
            }

            if (CookieParametersFilterCB.Checked)
            {
                if (CookieParametersPlusRB.Checked)
                {
                    foreach (string Name in CookieParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.CookieWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in CookieParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.CookieBlackList.Add(Name.Trim());
                    }
                }
            }

            if (HeadersParametersFilterCB.Checked)
            {
                if (HeadersParametersPlusRB.Checked)
                {
                    foreach (string Name in HeadersParametersPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.HeaderWhiteList.Add(Name.Trim());
                    }
                }
                else
                {
                    foreach (string Name in HeadersParametersMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ScanManager.HeaderBlackList.Add(Name.Trim());
                    }
                }
            }

            //ScanManager.HostsToInclude = new List<string>(HostsToIncludeTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            ScanManager.UrlsToAvoid = new List<string>(ConfigureScanUrlToAvoidTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            ScanManager.IncludeSubDomains = SubdomainYesRB.Checked;
            ScanManager.CrawlAndScan = CrawlAndScanRB.Checked;

            ScanManager.StartScan();
            IronUI.UpdateConsoleControlsStatus(true);
            
            IronUI.SSW.CloseWindow();
        }

        private void ScanAllPluginsCB_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                Row.Cells[0].Value = ScanAllPluginsCB.Checked;
            }
        }

        private void StartScanWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IronUI.UI.CanShutdown) return;
            if (!CanClose)
            {
                if (this.CurrentStep == 0)
                {
                    this.CanClose = true;
                }
                else if (this.CurrentStep == 5)
                {
                    e.Cancel = true;
                    MessageBox.Show("Once you click on the 'Start Scan' button this window will automatically close.\r\nIf you want to close this window without starting a scan then it can only be done from the first step.\r\nUse the '<- Previous Step' button on the bottom left corner to go to the first step and then press the 'Cancel' button on the bottom left corner.");
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("This window can only be closed from the first step.\r\nUse the '<- Previous Step' button on the bottom left corner to go to the first step and then press the 'Cancel' button on the bottom left corner.");
                }
            }
        }

        internal void CloseWindow()
        {
            this.CanClose = true;
            this.Close();
        }
    }
}
