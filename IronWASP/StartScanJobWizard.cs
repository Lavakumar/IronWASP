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
    public partial class StartScanJobWizard : Form
    {
        bool CanClose = false;

        Fuzzer Fuzz = null;

        bool ScanJobMode = true;
        
        internal SessionPluginCreationAssistant SCA = null;
        int CurrentStep = 0;

        Request RequestToScan = null;
        bool RequestHeadersChanged = false;
        bool RequestBodyChanged = false;
        bool RequestSSLCheckChanged = false;

        bool WasParametersBlackListChanged = false;
        bool ShouldSetInjectionPoints = true;

        bool UrlPathPartRequiresExplicitSelection = false;

        Thread BodyDeserializeThread;

        internal static List<string> ParametersBlackList = new List<string>();
        static List<string[]> SelectedEncodingRules = new List<string[]>();
        string CurrentStartMarker = "";
        string CurrentEndMarker = "";

        public StartScanJobWizard()
        {
            InitializeComponent();

            if (!ScanJobMode)
            {
                BaseTabs.TabPages.RemoveAt(1);
            }
        }

        internal Fuzzer GetFuzzer()
        {
            return this.Fuzz;
        }

        private void ScanJobCustomizeBtn_Click(object sender, EventArgs e)
        {
            if (IsScanCustomizationAssistanctOpen())
            {
                SCA.Activate();
            }
            else
            {
                SCA = new SessionPluginCreationAssistant();
                SCA.Show();
            }
        }

        bool IsScanCustomizationAssistanctOpen()
        {
            if (SCA == null)
            {
                return false;
            }
            else
            {
                if (SCA.IsDisposed)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void StartScanJobWizard_Load(object sender, EventArgs e)
        {
            //IronUI.AddStartScanJobWizard(this);
            if (ScanJobMode)
            {
                SetScanPluginsGrid();
            }
            SetParametersBlackList();
            SetCharacterEscapingRules();
            SetSessionPluginsCombo();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.CanClose = true;
            this.Close();
        }

        private void StepOneNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep0Status("");
            if (UpdateCurrentRequestFromUi())
            {               
                CurrentStep = 1;
                BaseTabs.SelectTab(1);
            }
        }

        private void StepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            CurrentStep = 0;
            BaseTabs.SelectTab(0);
        }

        private void StepTwoNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep1Status("");
            if (IsCheckSelectionOk())
            {
                CurrentStep = 2;
                BaseTabs.SelectTab(2);
            }
        }

        private void StepThreePreviousBtn_Click(object sender, EventArgs e)
        {
            if (ScanJobMode)
            {
                CurrentStep = 1;
                BaseTabs.SelectTab(1);
            }
            else
            {
                CurrentStep = 0;
                BaseTabs.SelectTab(0);
            }
        }

        private void StepThreeNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep2Status("");
            if (IsInjectionPointSelectionOk())
            {
                if (ScanJobMode)
                {
                    CurrentStep = 3;
                    BaseTabs.SelectTab(3);
                }
                else
                {
                    CurrentStep = 2;
                    BaseTabs.SelectTab(2);
                }
            }
        }

        private void StepFourPreviousBtn_Click(object sender, EventArgs e)
        {
            if (ScanJobMode)
            {
                CurrentStep = 2;
                BaseTabs.SelectTab(2);
            }
            else
            {
                CurrentStep = 1;
                BaseTabs.SelectTab(1);
            }
        }

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (BaseTabs.SelectedIndex != CurrentStep)
            {
                BaseTabs.SelectTab(CurrentStep);
                MessageBox.Show("Use the 'Next Step ->' and 'Previous Step ->' buttons on the bottom left and right corners of this window for navigation.");
            }
            else if ((ScanJobMode && CurrentStep == 2) || (!ScanJobMode && CurrentStep == 1))
            {
                CheckAndUpdateInjectionPointsInUi();
            }
        }

        

        #region Step0Actions
        internal void SetRequest(Request Req)
        {
            this.RequestToScan = Req;
            UpdateRequestInUi();

        }
        internal void SetFuzzer(Fuzzer Fuzz)
        {
            this.Fuzz = Fuzz;
            this.SetRequest(Fuzz.OriginalRequest);
            this.ScanJobMode = false;
            this.ScanThreadLimitCB.Visible = false;
            BaseTabs.TabPages.RemoveAt(1);
            FinalBtn.Text = "Done";
            this.Text = "Configure Scan/Fuzz Settings";
            this.Step2TopMsgTB.Text = @"Select which parameters and sections of the Request must be scanned.

You can either select all parameters or entire sections for scanning. Or go through the different tabs below and select the exact parameters you want be to scanned to perform a high precision scan.";
        }
        void UpdateRequestInUi()
        {
            this.RequestRawHeadersIDV.Text = this.RequestToScan.GetHeadersAsString();
            this.RequestRawBodyIDV.Text = this.RequestToScan.BodyString;
            this.RequestRawBodyIDV.ReadOnly = this.RequestToScan.IsBinary;
            this.RequestSSLCB.Checked = this.RequestToScan.SSL;
        }
        bool UpdateCurrentRequestFromUi()
        {
            if (this.RequestHeadersChanged)
            {
                try
                {
                    byte[] Body = new byte[]{};
                    if (this.RequestToScan != null)
                        Body = this.RequestToScan.BodyArray;
                    this.RequestToScan = new Request(this.RequestRawHeadersIDV.Text.TrimEnd(), this.RequestSSLCB.Checked);
                    this.RequestToScan.BodyArray = Body;
                }
                catch (Exception Exp) 
                {
                    ShowStep0Error(Exp.Message);
                    return false; 
                }
            }
            if(this.RequestBodyChanged)
            {
                if (this.RequestToScan == null)
                {
                    ShowStep0Error("Invalid Request");
                    return false;
                }
                else
                {
                    try
                    {
                        this.RequestToScan.BodyString = this.RequestRawBodyIDV.Text;
                    }
                    catch (Exception Exp) 
                    {
                        ShowStep0Error(Exp.Message); 
                        return false;
                    }
                }
            }
            if (this.RequestSSLCheckChanged)
            {
                if (this.RequestToScan == null)
                {
                    ShowStep0Error("Invalid Request");
                    return false;
                }
                else
                {
                    this.RequestToScan.SSL = this.RequestSSLCB.Checked;
                }
            }
            return true;
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
        #endregion
        #region Step1Actions
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
        bool IsCheckSelectionOk()
        {
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value) return true;
            }
            ShowStep1Error("You did not select any checks. Atleast one check must be selected");
            return false;
        }

        #endregion
        #region Step2Actions
        bool IsInjectionPointSelectionOk()
        {
            CheckAndUpdateParametersBlackList();
            UpdateInjectionPointLabels();
            if (AllPointsSelectedLbl.Text.Equals("0"))
            {
                ShowStep2Error("No injection points selected. Select atleast one injection point.");
                return false;
            }
            return true;
        }
        void SetParametersBlackList()
        {
            ParametersBlacklistTB.Text = "";
            StringBuilder SB = new StringBuilder();
            foreach (string ParameterName in ParametersBlackList)
            {
                SB.AppendLine(ParameterName);
            }
            ParametersBlacklistTB.Text = SB.ToString();
        }
        void SetCharacterEscapingRules()
        {
            if (Scanner.UserSpecifiedEncodingRuleList.Count == 0)
            {
                Scanner.UserSpecifiedEncodingRuleList.AddRange(Scanner.DefaultEncodingRuleList);
            }
            CharacterEscapingGrid.Rows.Clear();
            foreach (string[] Rule in Scanner.UserSpecifiedEncodingRuleList)
            {
                bool Selected = false;
                foreach (string[] R in SelectedEncodingRules)
                {
                    if (Rule[0].Equals(R[0]) && Rule[1].Equals(R[1]))
                    {
                        Selected = true;
                        break;
                    }
                }
                CharacterEscapingGrid.Rows.Add(new object[]{Selected, Rule[0], "->", Rule[1]});
            }
        }
        delegate void ShowStep2Status_d(string Text);
        void ShowStep2Status(string Text)
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                ShowStep2Status_d SS_d = new ShowStep2Status_d(ShowStep2Status);
                InjectionPointBaseTabs.Invoke(SS_d, new object[] { Text });
            }
            else
            {
                this.Step2StatusTB.Text = Text;
                if (Text.Length == 0)
                {
                    this.Step2StatusTB.Visible = false;
                }
                else
                {
                    this.Step2StatusTB.ForeColor = Color.Black;
                    this.Step2StatusTB.Visible = true;
                }
            }
        }
        delegate void ShowStep2Error_d(string Text);
        void ShowStep2Error(string Text)
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                ShowStep2Error_d SE_d = new ShowStep2Error_d(ShowStep2Error);
                InjectionPointBaseTabs.Invoke(SE_d, new object[] { Text });
            }
            else
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
        }
        void CheckAndUpdateInjectionPointsInUi()
        {
            if (ShouldSetInjectionPoints) UpdateInjectionPointsInUi();
        }
        void UpdateInjectionPointsInUi()
        {
            InjectionPointBaseTabs.Visible = false;
            StepThreePreviousBtn.Enabled = false;
            StepThreeNextBtn.Enabled = false;
            Step2ProgressBar.Visible = true;
            Step2StatusTB.Visible = false;
            ShouldSetInjectionPoints = false;
            Thread T = new Thread(SetInjectionPointsInUi);
            T.Start();
        }

        delegate void UpdateInjectionPointLabels_d();
        void UpdateInjectionPointLabels()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                UpdateInjectionPointLabels_d UIPL_d = new UpdateInjectionPointLabels_d(UpdateInjectionPointLabels);
                InjectionPointBaseTabs.Invoke(UIPL_d, new object[] { });
            }
            else
            {
                #region Names
                int NamesPointsAvlCount = 4;
                AllNamesPointsAvlLbl.Text = NamesPointsAvlCount.ToString();
                int NamesPointsSelectedCount = 0;
                if (ScanQueryParameterNameCB.Checked) NamesPointsSelectedCount++;
                if (ScanBodyParameterNameCB.Checked) NamesPointsSelectedCount++;
                if (ScanCookieParameterNameCB.Checked) NamesPointsSelectedCount++;
                if (ScanHeadersParameterNameCB.Checked) NamesPointsSelectedCount++;
                AllNamesPointsSelectedLbl.Text = NamesPointsSelectedCount.ToString();
                #endregion
                #region Headers
                int HeaderPointsAvlCount = ScanHeadersGrid.Rows.Count;
                int HeaderPointsSelectedCount = 0;
                foreach (DataGridViewRow Row in ScanHeadersGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value) HeaderPointsSelectedCount++;
                }
                AllHeaderPointsAvlLbl.Text = HeaderPointsAvlCount.ToString();
                AllHeaderPointsSelectedLbl.Text = HeaderPointsSelectedCount.ToString();
                #endregion
                #region Cookie
                int CookiePointsAvlCount = ScanCookieGrid.Rows.Count;
                int CookiePointsSelectedCount = 0;
                foreach (DataGridViewRow Row in ScanCookieGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value) CookiePointsSelectedCount++;
                }
                AllCookiePointsAvlLbl.Text = CookiePointsAvlCount.ToString();
                AllCookiePointsSelectedLbl.Text = CookiePointsSelectedCount.ToString();
                #endregion
                #region Body
                int BodyPointsAvlCount = 0;
                int BodyPointsSelectedCount = 0;
                if (BodyTypeNormalRB.Checked)
                {
                    BodyPointsAvlCount = ScanBodyTypeNormalGrid.Rows.Count;
                    foreach (DataGridViewRow Row in ScanBodyTypeNormalGrid.Rows)
                    {
                        if ((bool)Row.Cells[0].Value) BodyPointsSelectedCount++;
                    }
                }
                else if (BodyTypeFormatPluginRB.Checked)
                {
                    BodyPointsAvlCount = BodyTypeFormatPluginGrid.Rows.Count;
                    foreach (DataGridViewRow Row in BodyTypeFormatPluginGrid.Rows)
                    {
                        if ((bool)Row.Cells[0].Value) BodyPointsSelectedCount++;
                    }
                }
                else if (BodyTypeCustomRB.Checked)
                {
                    try
                    {
                        int CustomInjectionPointsCount = Int32.Parse(CustomInjectionPointsHighlightLbl.Text);
                        BodyPointsAvlCount = CustomInjectionPointsCount;
                        BodyPointsSelectedCount = CustomInjectionPointsCount;
                    }
                    catch 
                    { 
                        BodyPointsAvlCount = 0;
                        BodyPointsSelectedCount = 0;
                    }
                }
                AllBodyPointsAvlLbl.Text = BodyPointsAvlCount.ToString();
                AllBodyPointsSelectedLbl.Text = BodyPointsSelectedCount.ToString();
                #endregion
                #region Query
                int QueryPointsAvlCount = ScanQueryGrid.Rows.Count;
                int QueryPointsSelectedCount = 0;
                foreach (DataGridViewRow Row in ScanQueryGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value) QueryPointsSelectedCount++;
                }
                AllQueryPointsAvlLbl.Text = QueryPointsAvlCount.ToString();
                AllQueryPointsSelectedLbl.Text = QueryPointsSelectedCount.ToString();
                #endregion
                #region Url
                int UrlPointsAvlCount = ScanURLGrid.Rows.Count;
                int UrlPointsSelectedCount = 0;
                foreach (DataGridViewRow Row in ScanURLGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value) UrlPointsSelectedCount++;
                }
                AllUrlPointsAvlLbl.Text = UrlPointsAvlCount.ToString();
                AllUrlPointsSelectedLbl.Text = UrlPointsSelectedCount.ToString();
                #endregion
                #region All
                int AllPointsAvlCount = NamesPointsAvlCount + HeaderPointsAvlCount + CookiePointsAvlCount + BodyPointsAvlCount + QueryPointsAvlCount + UrlPointsAvlCount;
                int AllPointsSelectedCount = NamesPointsSelectedCount + HeaderPointsSelectedCount + CookiePointsSelectedCount + BodyPointsSelectedCount + QueryPointsSelectedCount + UrlPointsSelectedCount;
                AllPointsAvlLbl.Text = AllPointsAvlCount.ToString();
                AllPointsSelectedLbl.Text = AllPointsSelectedCount.ToString();
                #endregion
                #region ParameterBlaacklist
                BlacklistItemsCountLbl.Text = ParametersBlacklistTB.Text.Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length.ToString();
                #endregion
            }
        }

        void SetInjectionPointsInUi()
        {
            SetUrlInjectionPointsInUi();
            SetQueryInjectionPointsInUi();
            SetBodyInjectionPointsInUi();
            SetCookieInjectionPointsInUi();
            SetHeaderInjectionPointsInUi();
            UpdateInjectionPointLabels();
            FinishUpdatingInjectionPointsInUi();
        }
        delegate void SetUrlInjectionPointsInUi_d();
        void SetUrlInjectionPointsInUi()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetUrlInjectionPointsInUi_d SUIPIU_d = new SetUrlInjectionPointsInUi_d(SetUrlInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(SUIPIU_d, new object[] { });
            }
            else
            {
                ScanURLGrid.Rows.Clear();
                List<string> UPP = RequestToScan.UrlPathParts;
                for (int i = 0; i < UPP.Count; i++)
                {
                    ScanURLGrid.Rows.Add(new object[] { false, i, UPP[i] });
                }
                if (RequestToScan.Query.Count > 0)
                {
                    SetUrlPathPartMessage("Url has Querystring so path parts would require explicit selection.");
                    UrlPathPartRequiresExplicitSelection = true;
                }
                else if (RequestToScan.File.Length > 0)
                {
                    SetUrlPathPartMessage(string.Format("Url ends with file extention - {0},  so path parts would require explicit selection.", RequestToScan.File));
                    UrlPathPartRequiresExplicitSelection = true;
                }
                else
                {
                    SetUrlPathPartMessage("Url does not have file extension or querystring, indicates possible use of URL rewriting.");
                    UrlPathPartRequiresExplicitSelection = false;
                }
            }
        }
        delegate void SetQueryInjectionPointsInUi_d();
        void SetQueryInjectionPointsInUi()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetQueryInjectionPointsInUi_d SQIPIU_d = new SetQueryInjectionPointsInUi_d(SetQueryInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(SQIPIU_d, new object[] { });
            }
            else
            {
                ScanQueryGrid.Rows.Clear();
                foreach (string ParameterName in RequestToScan.Query.GetNames())
                {
                    foreach (string ParameterValue in RequestToScan.Query.GetAll(ParameterName))
                    {
                        ScanQueryGrid.Rows.Add(new object[] { false, ParameterName, ParameterValue });
                    }
                }
            }
        }
        void SetBodyInjectionPointsInUi()
        {
            SetDefaultBodyTabValues();
            SetBodyTypeNormalInjectionPointsInUi();
            if (RequestToScan.BodyLength == 0)
            {
                SetBodyTypeMessage("Request does not have a body");
                return;
            }
            if (FormatPlugin.IsNormal(RequestToScan))
            {
                SetBodyTypeMessage("Request body looks to be of normal format");
            }
            else
            {
                string FPName = FormatPlugin.Get(RequestToScan);
                if (FPName.Length > 0 && FPName != "Normal")
                {
                    string FormatPluginName = FPName;
                    FormatPlugin FP = FormatPlugin.Get(FPName);
                    string XML = FP.ToXmlFromRequest(RequestToScan);
                    string[,] XmlInjectionPoints = FormatPlugin.XmlToArray(XML);
                    SetBodyTypeFormatPluginInjectionPointsInUi(FormatPluginName, XmlInjectionPoints, XML);
                    SetBodyTypeMessage(string.Format("Request body format has been auto-detected as '{0}'", FormatPluginName));
                }
                else
                {
                    SetBodyTypeMessage("Request body format is not normal. Use the options under Body tab to handle the body injection.");
                }
            }
        }
        delegate void SetDefaultBodyTabValues_d();
        void SetDefaultBodyTabValues()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetDefaultBodyTabValues_d SDBTV_d = new SetDefaultBodyTabValues_d(SetDefaultBodyTabValues);
                InjectionPointBaseTabs.Invoke(SDBTV_d, new object[] { });
            }
            else
            {
                if (CurrentStartMarker.Length == 0 || CurrentEndMarker.Length == 0 || CurrentStartMarker == CurrentEndMarker)
                {
                    CurrentStartMarker = Scanner.DefaultStartMarker;
                    CurrentEndMarker = Scanner.DefaultEndMarker;
                }
                CustomStartMarkerTB.Text = CurrentStartMarker;
                CustomEndMarkerTB.Text = CurrentEndMarker;
                SetCustomInjectionPointsSTB.Text = RequestToScan.BodyString;
                HighlightCustomInjectionPointsRTB.Text = RequestToScan.BodyString;

                BodyTypeFormatPluginGrid.Rows.Clear();
                FormatXMLTB.Text = "";
                FormatPluginsGrid.Rows.Clear();
                foreach (string Name in FormatPlugin.List())
                {
                    FormatPluginsGrid.Rows.Add(new object[]{false, Name});
                }

                ScanBodyTypeNormalGrid.Rows.Clear();
                BodyTypeNormalRB.Checked = true;
            }
        }
        delegate void SetBodyTypeNormalInjectionPointsInUi_d();
        void SetBodyTypeNormalInjectionPointsInUi()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetBodyTypeNormalInjectionPointsInUi_d SBIPIU_d = new SetBodyTypeNormalInjectionPointsInUi_d(SetBodyTypeNormalInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(SBIPIU_d, new object[] { });
            }
            else
            {
                ScanBodyTypeNormalGrid.Rows.Clear();
                foreach (string ParameterName in RequestToScan.Body.GetNames())
                {
                    if (ParameterName.Equals("Content-Length", StringComparison.OrdinalIgnoreCase)) continue;
                    foreach (string ParameterValue in RequestToScan.Body.GetAll(ParameterName))
                    {
                        ScanBodyTypeNormalGrid.Rows.Add(new object[] { false, ParameterName, ParameterValue });
                    }
                }
                BodyTypeNormalRB.Checked = true;
            }
        }
        delegate void SetBodyTypeFormatPluginInjectionPointsInUi_d(string FormatPluginName, string[,] InjectionArray, string XML);
        void SetBodyTypeFormatPluginInjectionPointsInUi(string FormatPluginName, string[,] InjectionArray, string XML)
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetBodyTypeFormatPluginInjectionPointsInUi_d SBIPIU_d = new SetBodyTypeFormatPluginInjectionPointsInUi_d(SetBodyTypeFormatPluginInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(SBIPIU_d, new object[] { FormatPluginName, InjectionArray, XML });
            }
            else
            {
                BodyTypeFormatPluginGrid.Rows.Clear();
                for (int i = 0; i < InjectionArray.GetLength(0); i++)
                {
                    BodyTypeFormatPluginGrid.Rows.Add(new object[] { false, InjectionArray[i, 0], InjectionArray[i,1] });
                }
                FormatXMLTB.Text = XML;
                foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
                {
                    if (Row.Cells[1].Value.ToString().Equals(FormatPluginName))
                        Row.Cells[0].Value = true;
                    else
                        Row.Cells[0].Value = false;
                }
                BodyTypeFormatPluginRB.Checked = true;
                ScanBodyFormatPluginTypeTabs.SelectTab("BodyTypeFormatPluginInjectionArrayGridTab");
            }
        }
        delegate void SetBodyTypeCustomInjectionPointsInUi_d();
        void SetBodyTypeCustomInjectionPointsInUi()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetBodyTypeCustomInjectionPointsInUi_d SBIPIU_d = new SetBodyTypeCustomInjectionPointsInUi_d(SetBodyTypeCustomInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(SBIPIU_d, new object[] { });
            }
            else
            {

                string StartMarker = CustomStartMarkerTB.Text.Trim();
                string EndMarker = CustomEndMarkerTB.Text.Trim();
                if (StartMarker.Length == 0 || EndMarker.Length == 0)
                {
                    ShowStep2Error("Start and End markers cannot be empty.");
                    return;
                }
                if (StartMarker.Equals(EndMarker))
                {
                    ShowStep2Error("Start and End markers cannot be the same.");
                    return;
                }
                int SSI = HighlightCustomInjectionPointsRTB.SelectionStart;
                int SL = HighlightCustomInjectionPointsRTB.SelectionLength;
                HighlightCustomInjectionPointsRTB.Text = SetCustomInjectionPointsSTB.Text;
                bool CheckFurther = true;
                int Pointer = 0;
                //string Content = SetCustomInjectionPointsSTB.Text;
                string Content = HighlightCustomInjectionPointsRTB.Text;//Using the text from RichTextBox as content instead of the TextBox since \r\n from TB is converted to \r in RTB and so the highlighting of markers is visually offset. This is done only for highlighting.
                if (Content.Length == 0)
                {
                    ShowStep2Error("No injection points detected.");
                    return;
                }
                int MatchCount = 0;
                while (CheckFurther && Content.Length > Pointer)
                {
                    int Start = Content.IndexOf(StartMarker, Pointer);
                    int Stop = -1;
                    if (Content.Length >= (Start + StartMarker.Length))
                        Stop = Content.IndexOf(EndMarker, Start + StartMarker.Length);
                    if (Start == -1 || Stop == -1) CheckFurther = false;
                    if (CheckFurther)
                    {
                        HighlightCustomInjectionPointsRTB.SelectionStart = Start;
                        HighlightCustomInjectionPointsRTB.SelectionLength = (Stop + EndMarker.Length) - Start;
                        HighlightCustomInjectionPointsRTB.SelectionBackColor = Color.Orange;
                        MatchCount++;
                    }
                    Pointer = Stop + EndMarker.Length;
                }
                HighlightCustomInjectionPointsRTB.SelectionStart = SSI;
                HighlightCustomInjectionPointsRTB.SelectionLength = SL;

                CustomInjectionPointsHighlightLbl.Text = MatchCount.ToString();
                if (MatchCount > 0)
                    ScanBodyCB.Checked = true;
                else
                    ScanBodyCB.Checked = false;
                CustomInjectionMarkerTabs.SelectTab("CustomMarkerDisplayTab");
                BodyTypeCustomRB.Checked = true;
            }
        }
        delegate void SetCookieInjectionPointsInUi_d();
        void SetCookieInjectionPointsInUi()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetCookieInjectionPointsInUi_d SCIPIU_d = new SetCookieInjectionPointsInUi_d(SetCookieInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(SCIPIU_d, new object[] { });
            }
            else
            {
                ScanCookieGrid.Rows.Clear();
                foreach (string ParameterName in RequestToScan.Cookie.GetNames())
                {
                    foreach (string ParameterValue in RequestToScan.Cookie.GetAll(ParameterName))
                    {
                        ScanCookieGrid.Rows.Add(new object[] { false, ParameterName, ParameterValue });
                    }
                }
            }
        }
        delegate void SetHeaderInjectionPointsInUi_d();
        void SetHeaderInjectionPointsInUi()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetHeaderInjectionPointsInUi_d SHIPIU_d = new SetHeaderInjectionPointsInUi_d(SetHeaderInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(SHIPIU_d, new object[] { });
            }
            else
            {
                ScanHeadersGrid.Rows.Clear();
                foreach (string ParameterName in RequestToScan.Headers.GetNames())
                {
                    if (ParameterName.Equals("Host", StringComparison.OrdinalIgnoreCase) || ParameterName.Equals("Cookie", StringComparison.OrdinalIgnoreCase)) continue;
                    foreach (string ParameterValue in RequestToScan.Headers.GetAll(ParameterName))
                    {
                        ScanHeadersGrid.Rows.Add(new object[] { false, ParameterName, ParameterValue });
                    }
                }
            }
        }
        delegate void SetUrlPathPartMessage_d(string Message);
        void SetUrlPathPartMessage(string Message)
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetUrlPathPartMessage_d SUPPM_d = new SetUrlPathPartMessage_d(SetUrlPathPartMessage);
                InjectionPointBaseTabs.Invoke(SUPPM_d, new object[] { Message });
            }
            else
            {
                UrlPathPartInjectionMessageLbl.Text = Message;
            }
        }
        delegate void SetBodyTypeMessage_d(string Message);
        void SetBodyTypeMessage(string Message)
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                SetBodyTypeMessage_d SBTM_d = new SetBodyTypeMessage_d(SetBodyTypeMessage);
                InjectionPointBaseTabs.Invoke(SBTM_d, new object[] { Message });
            }
            else
            {
                BodyInjectionMessageLbl.Text = Message;
            }
        }
        delegate void FinishUpdatingInjectionPointsInUi_d();
        void FinishUpdatingInjectionPointsInUi()
        {
            if (InjectionPointBaseTabs.InvokeRequired)
            {
                FinishUpdatingInjectionPointsInUi_d FUIPIU_d = new FinishUpdatingInjectionPointsInUi_d(FinishUpdatingInjectionPointsInUi);
                InjectionPointBaseTabs.Invoke(FUIPIU_d, new object[] { });
            }
            else
            {
                InjectionPointBaseTabs.Visible = true;
                StepThreePreviousBtn.Enabled = true;
                StepThreeNextBtn.Enabled = true;
                Step2ProgressBar.Visible = false;
                Step2StatusTB.Visible = false;
            }
        }
        void InjectAll(bool Inject)
        {
            if (Inject)
            {
                if (!UrlPathPartRequiresExplicitSelection) InjectUrl(Inject);
            }
            else
            {
                InjectUrl(Inject);
            }
            InjectQuery(Inject);
            InjectBody(Inject);
            InjectCookie(Inject);
            InjectHeaders(Inject);
            InjectNames(Inject);
        }
        void InjectUrl(bool Inject)
        {
            foreach (DataGridViewRow Row in ScanURLGrid.Rows)
            {
                Row.Cells[0].Value = Inject;
            }
            UpdateInjectionPointLabels();
        }
        void InjectQuery(bool Inject)
        {
            foreach (DataGridViewRow Row in ScanQueryGrid.Rows)
            {
                if (Inject && UseBlackListCB.Checked && ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                    continue;
                else
                    Row.Cells[0].Value = Inject;
            }
            UpdateInjectionPointLabels();
        }
        void InjectBody(bool Inject)
        {
            if (BodyTypeNormalRB.Checked)
            {
                foreach (DataGridViewRow Row in ScanBodyTypeNormalGrid.Rows)
                {
                    if (Inject && UseBlackListCB.Checked && ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                        continue;
                    else
                        Row.Cells[0].Value = Inject;
                }
            }
            else if (BodyTypeFormatPluginRB.Checked)
            {
                foreach (DataGridViewRow Row in BodyTypeFormatPluginGrid.Rows)
                {
                    Row.Cells[0].Value = Inject;
                }
            }
            else if (BodyTypeCustomRB.Checked)
            {
                HighlightCustomInjectionPointsRTB.Text = "";
                CustomInjectionPointsHighlightLbl.Text = "0";
                CustomInjectionMarkerTabs.SelectTab("CustomMarkerSelectionTab");
            }
            UpdateInjectionPointLabels();
        }
        void InjectCookie(bool Inject)
        {
            foreach (DataGridViewRow Row in ScanCookieGrid.Rows)
            {
                if (Inject && UseBlackListCB.Checked && ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                    continue;
                else
                    Row.Cells[0].Value = Inject;
            }
            UpdateInjectionPointLabels();
        }
        void InjectHeaders(bool Inject)
        {
            foreach (DataGridViewRow Row in ScanHeadersGrid.Rows)
            {
                if (Inject && UseBlackListCB.Checked && ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                    continue;
                else
                    Row.Cells[0].Value = Inject;
            }
            UpdateInjectionPointLabels();
        }
        void InjectNames(bool Inject)
        {
            ScanQueryParameterNameCB.Checked = Inject;
            ScanBodyParameterNameCB.Checked = Inject;
            ScanCookieParameterNameCB.Checked = Inject;
            ScanHeadersParameterNameCB.Checked = Inject;
            UpdateInjectionPointLabels();
        }
        #endregion
        #region Step3Actions
        void SetSessionPluginsCombo()
        {
            this.SessionPluginsCombo.Items.Clear();
            this.SessionPluginsCombo.Text = "";
            this.SessionPluginsCombo.Items.Add("");
            foreach (string Name in SessionPlugin.List())
            {
                this.SessionPluginsCombo.Items.Add(Name);
            }
        }
        #endregion
        private void RequestSSLCB_CheckedChanged(object sender, EventArgs e)
        {
            this.RequestSSLCheckChanged = true;
        }

        private void RequestRawHeadersIDV_IDVTextChanged()
        {
            this.RequestHeadersChanged = true;
            this.ShouldSetInjectionPoints = true;
        }

        private void RequestRawBodyIDV_IDVTextChanged()
        {
            this.RequestBodyChanged = true;
            this.ShouldSetInjectionPoints = true;
        }

        void SetScanPluginsGrid()
        {
            this.ScanPluginsGrid.Rows.Clear();
            foreach (string Name in ActivePlugin.List())
            {
                this.ScanPluginsGrid.Rows.Add(new object[]{false, Name});
            }
        }

        private void SelectAllChecksCB_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                Row.Cells[0].Value = SelectAllChecksCB.Checked;
            }
        }

        private void ScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanPluginsGrid.SelectedRows == null) return;
            if (ScanPluginsGrid.SelectedRows.Count == 0) return;
            ScanPluginsGrid.SelectedRows[0].Cells[0].Value = !((bool)ScanPluginsGrid.SelectedRows[0].Cells[0].Value);
            if (!(bool)ScanPluginsGrid.SelectedRows[0].Cells[0].Value) SelectAllChecksCB.Checked = false;
        }

        private void ASApplyCustomMarkersLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetBodyTypeCustomInjectionPointsInUi();
        }

        private void BodyTypeNormalRB_CheckedChanged(object sender, EventArgs e)
        {
            ShowStep2Status("");
            if (BodyTypeNormalRB.Checked)
            {
                foreach (DataGridViewRow Row in ScanBodyTypeNormalGrid.Rows)
                {
                    Row.Cells[0].Value = false;
                }
                ScanAllCB.Checked = false;
                ScanBodyCB.Checked = false;
                BodyInjectTypeTabs.SelectTab("BodyTypeNormalTab");
            }
        }

        private void BodyTypeFormatPluginRB_CheckedChanged(object sender, EventArgs e)
        {
            ShowStep2Status("");
            if (BodyTypeFormatPluginRB.Checked)
            {
                foreach (DataGridViewRow Row in BodyTypeFormatPluginGrid.Rows)
                {
                    Row.Cells[0].Value = false;
                }
                ScanBodyFormatPluginTypeTabs.SelectTab("BodyTypeFormatPluginInjectionArrayGridTab");
                ScanAllCB.Checked = false;
                ScanBodyCB.Checked = false;
                BodyInjectTypeTabs.SelectTab("BodyTypeFormatPluginTab");
            }
        }

        private void BodyTypeCustomRB_CheckedChanged(object sender, EventArgs e)
        {
            ShowStep2Status("");
            if (BodyTypeCustomRB.Checked)
            {
                HighlightCustomInjectionPointsRTB.Text = "";
                CustomInjectionPointsHighlightLbl.Text = "0";
                CustomInjectionMarkerTabs.SelectTab("CustomMarkerSelectionTab");
                ScanAllCB.Checked = false;
                ScanBodyCB.Checked = false;
                BodyInjectTypeTabs.SelectTab("BodyTypeCustomTab");
            }
        }

        private void ScanAllCB_Click(object sender, EventArgs e)
        {
            InjectAll(ScanAllCB.Checked);
            if (ScanAllCB.Checked)
            {
                if (!UrlPathPartRequiresExplicitSelection)
                {
                    ScanURLCB.Checked = ScanAllCB.Checked;
                }
            }
            else
            {
                ScanURLCB.Checked = ScanAllCB.Checked;
            }
            ScanQueryCB.Checked = ScanAllCB.Checked;
            ScanBodyCB.Checked = ScanAllCB.Checked;
            ScanCookieCB.Checked = ScanAllCB.Checked;
            ScanHeadersCB.Checked = ScanAllCB.Checked;
            ScanParameterNamesCB.Checked = ScanAllCB.Checked;
        }

        private void ScanURLCB_Click(object sender, EventArgs e)
        {
            InjectUrl(ScanURLCB.Checked);
            if (!ScanURLCB.Checked) ScanAllCB.Checked = false;
        }

        private void ScanQueryCB_Click(object sender, EventArgs e)
        {
            InjectQuery(ScanQueryCB.Checked);
            if (!ScanQueryCB.Checked) ScanAllCB.Checked = false;
        }

        private void ScanBodyCB_Click(object sender, EventArgs e)
        {
            InjectBody(ScanBodyCB.Checked);
            if (!ScanBodyCB.Checked) ScanAllCB.Checked = false;
        }

        private void ScanCookieCB_Click(object sender, EventArgs e)
        {
            InjectCookie(ScanCookieCB.Checked);
            if (!ScanCookieCB.Checked) ScanAllCB.Checked = false;
        }

        private void ScanHeadersCB_Click(object sender, EventArgs e)
        {
            InjectHeaders(ScanHeadersCB.Checked);
            if (!ScanHeadersCB.Checked) ScanAllCB.Checked = false;
        }

        private void ScanParameterNamesCB_Click(object sender, EventArgs e)
        {
            InjectNames(ScanParameterNamesCB.Checked);
            if (!ScanParameterNamesCB.Checked) ScanAllCB.Checked = false;
        }

        private void InjectionPointBaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (InjectionPointBaseTabs.SelectedTab.Name.Equals("AllTab"))
                UpdateInjectionPointLabels();
        }

        private void ScanURLGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanURLGrid.SelectedCells.Count < 1 || ScanURLGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ScanURLGrid.SelectedCells[0].Value)
            {
                this.ScanURLGrid.SelectedCells[0].Value = false;
                this.ScanAllCB.Checked = false;
                this.ScanURLCB.Checked = false;
            }
            else
            {
                this.ScanURLGrid.SelectedCells[0].Value = true;
            }
        }

        private void ScanQueryGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanQueryGrid.SelectedCells.Count < 1 || ScanQueryGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ScanQueryGrid.SelectedCells[0].Value)
            {
                this.ScanQueryGrid.SelectedCells[0].Value = false;
                this.ScanAllCB.Checked = false;
                this.ScanQueryCB.Checked = false;
            }
            else
            {
                if (UseBlackListCB.Checked && ParametersBlackList.Contains(this.ScanQueryGrid.SelectedCells[1].Value.ToString()))
                {
                    ShowStep2Error("Cannot select parameter, it is part of the parameters black-list and use of parameter black-list is turned on.");
                }
                else
                {
                    this.ScanQueryGrid.SelectedCells[0].Value = true;
                }
            }
        }

        private void ScanBodyTypeNormalGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanBodyTypeNormalGrid.SelectedCells.Count < 1 || ScanBodyTypeNormalGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ScanBodyTypeNormalGrid.SelectedCells[0].Value)
            {
                this.ScanBodyTypeNormalGrid.SelectedCells[0].Value = false;
                this.ScanAllCB.Checked = false;
                this.ScanBodyCB.Checked = false;
            }
            else
            {
                if (UseBlackListCB.Checked && ParametersBlackList.Contains(this.ScanBodyTypeNormalGrid.SelectedCells[1].Value.ToString()))
                {
                    ShowStep2Error("Cannot select parameter, it is part of the parameters black-list and use of parameter black-list is turned on.");
                }
                else
                {
                    this.ScanBodyTypeNormalGrid.SelectedCells[0].Value = true;
                }
            }
        }

        private void BodyTypeFormatPluginGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BodyTypeFormatPluginGrid.SelectedCells.Count < 1 || BodyTypeFormatPluginGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.BodyTypeFormatPluginGrid.SelectedCells[0].Value)
            {
                this.BodyTypeFormatPluginGrid.SelectedCells[0].Value = false;
                this.ScanAllCB.Checked = false;
                this.ScanBodyCB.Checked = false;
            }
            else
            {
                this.BodyTypeFormatPluginGrid.SelectedCells[0].Value = true;
            }
        }

        private void ScanCookieGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanCookieGrid.SelectedCells.Count < 1 || ScanCookieGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ScanCookieGrid.SelectedCells[0].Value)
            {
                this.ScanCookieGrid.SelectedCells[0].Value = false;
                this.ScanAllCB.Checked = false;
                this.ScanCookieCB.Checked = false;
            }
            else
            {
                if (UseBlackListCB.Checked && ParametersBlackList.Contains(this.ScanCookieGrid.SelectedCells[1].Value.ToString()))
                {
                    ShowStep2Error("Cannot select parameter, it is part of the parameters black-list and use of parameter black-list is turned on.");
                }
                else
                {
                    this.ScanCookieGrid.SelectedCells[0].Value = true;
                }
            }
        }

        private void ScanHeadersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanHeadersGrid.SelectedCells.Count < 1 || ScanHeadersGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ScanHeadersGrid.SelectedCells[0].Value)
            {
                this.ScanHeadersGrid.SelectedCells[0].Value = false;
                this.ScanAllCB.Checked = false;
                this.ScanHeadersCB.Checked = false;
            }
            else
            {
                if (UseBlackListCB.Checked && ParametersBlackList.Contains(this.ScanHeadersGrid.SelectedCells[1].Value.ToString()))
                {
                    ShowStep2Error("Cannot select parameter, it is part of the parameters black-list and use of parameter black-list is turned on.");
                }
                else
                {
                    this.ScanHeadersGrid.SelectedCells[0].Value = true;
                }
            }
        }

        private void ScanQueryParameterNameCB_Click(object sender, EventArgs e)
        {
            this.ScanAllCB.Checked = false;
            this.ScanParameterNamesCB.Checked = false;
        }

        private void ScanBodyParameterNameCB_Click(object sender, EventArgs e)
        {
            this.ScanAllCB.Checked = false;
            this.ScanParameterNamesCB.Checked = false;
        }

        private void ScanCookieParameterNameCB_Click(object sender, EventArgs e)
        {
            this.ScanAllCB.Checked = false;
            this.ScanParameterNamesCB.Checked = false;
        }

        private void ScanHeadersParameterNameCB_Click(object sender, EventArgs e)
        {
            this.ScanAllCB.Checked = false;
            this.ScanParameterNamesCB.Checked = false;
        }

        private void FormatPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowStep2Status("Deserialzing Request body using selected format");
            string PluginName = "";
            foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
            {
                Row.Cells[0].Value = false;
                if (e.RowIndex == Row.Index) PluginName = Row.Cells[1].Value.ToString();
            }
            BodyTypeFormatPluginGrid.Rows.Clear();
            FormatXMLTB.Text = "";

            if (!FormatPlugin.List().Contains(PluginName))
            {
                ShowStep2Error("Selected Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (BodyDeserializeThread != null)
            {
                try { BodyDeserializeThread.Abort(); }
                catch { }
            }
            BodyDeserializeThread = new Thread(DeserializeRequestBody);
            BodyDeserializeThread.Start(Plugin);
        }

        void DeserializeRequestBody(object FormatPluginObject)
        {
            try
            {
                FormatPlugin FP = (FormatPlugin)FormatPluginObject;
                string XML = FP.ToXmlFromRequest(RequestToScan);
                string[,] XmlInjectionPoints = FormatPlugin.XmlToArray(XML);
                SetBodyTypeFormatPluginInjectionPointsInUi(FP.Name, XmlInjectionPoints, XML);
                SetBodyTypeMessage(string.Format("Request body format has been set by you as '{0}'", FP.Name));
                ShowStep2Status("");
            }
            catch (ThreadAbortException) { }
            catch(Exception Exp)
            {
                IronException.Report("Error deserializing Request body", Exp);
                ShowStep2Error("Error parsing Request body in selected format");
            }
        }

        private void FinalBtn_Click(object sender, EventArgs e)
        {
            if (FinalBtn.Text.Equals("Close"))
            {
                this.CanClose = true;
                this.Close();
            }
            else
            {
                try
                {
                    FinalBtn.Enabled = false;
                    if (this.ScanJobMode)
                    {
                        Step3StatusTB.Text = "Creating scan job, please wait...";
                    }
                    else
                    {
                        Step3StatusTB.Text = "Reading your inputs, please wait...";
                    }

                    Scanner NewScanner = new Scanner(RequestToScan);
                    
                    string SessionPluginName = SessionPluginsCombo.Text;

                    
                    if (SessionPluginName.Length > 0)
                    {
                        if (SessionPlugin.List().Contains(SessionPluginName))
                        {
                            if (ScanThreadLimitCB.Checked)
                            {
                                Scanner.MaxParallelScanCount = 1;
                                IronUI.UpdateScannerSettingsInUIFromConfig();
                                IronDB.StoreScannerSettings();
                            }
                        }
                        else
                        {
                            Step3StatusTB.Text = "Invalid Session Plugin name selected.";
                            FinalBtn.Enabled = true;
                            return;
                        }
                    }

                    //
                    //No updates to the NewScanner object must be done before calling this.UpdateScannerFromUi method.
                    //There is a chance that this method might create a new scanner object and return it (when custom body injection points is selected).
                    //Any updates to NewScanner made before this method are lost if a new scanner object is returned
                    //
                    if (ScanJobMode)
                    {
                        NewScanner = this.UpdateScannerFromUi(NewScanner, SessionPluginName);
                    }
                    else
                    {
                        this.Fuzz = (Fuzzer) this.UpdateScannerFromUi(this.Fuzz, SessionPluginName);
                    }

                    if (ScanJobMode)
                    {
                        int ScanID = NewScanner.LaunchScan();
                        Step3StatusTB.Text = string.Format("Scan has been started. The ID for this scan job is {0}.\r\n\r\nThe status of this scan job can be checked in the 'Automated Scanning' tab, this window can be closed.", ScanID);
                        FinalBtn.Text = "Close";
                        StepFourPreviousBtn.Enabled = false;
                        FinalBtn.Enabled = true;
                    }
                    else
                    {
                        this.CanClose = true;
                        this.Close();
                    }
                }
                catch (Exception Exp)
                {
                    if (this.ScanJobMode)
                    {
                        IronException.Report("Error starting a configured scan", Exp.Message, Exp.StackTrace);
                        Step3StatusTB.Text = "Error Starting Scan!";
                    }
                    else
                    {
                        IronException.Report("Error getting injection points from UI", Exp.Message, Exp.StackTrace);
                        Step3StatusTB.Text = "Error reading Injecton Points";
                    }
                    FinalBtn.Enabled = true;
                }
            }
        }

        Scanner UpdateScannerFromUi(Scanner NewScanner, string SessionPluginName)
        {
            //Body must come above everything else because for a custom injection marker selection a new scanner object is created.
            int SubParameterPosition = 0;
            string ParameterName = "";
            #region BodyInjectionPoints
            if (BodyTypeNormalRB.Checked)
            {
                SubParameterPosition = 0;
                ParameterName = "";

                foreach (DataGridViewRow Row in this.ScanBodyTypeNormalGrid.Rows)
                {
                    string CurrentParameterName = Row.Cells[1].Value.ToString();
                    if (ParameterName.Equals(CurrentParameterName))
                    {
                        SubParameterPosition++;
                    }
                    else
                    {
                        ParameterName = CurrentParameterName;
                        SubParameterPosition = 0;
                    }
                    if ((bool)Row.Cells[0].Value)
                    {
                        NewScanner.InjectBody(ParameterName, SubParameterPosition);
                    }
                }
            }
            else if (BodyTypeFormatPluginRB.Checked)
            {
                bool FormatPluginSelected = false;
                bool FormatPluginInjectionPointSelected = false;
                foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        NewScanner.BodyFormat = FormatPlugin.Get(Row.Cells[1].Value.ToString());
                        FormatPluginSelected = true;
                        break;
                    }
                }
                foreach (DataGridViewRow Row in this.BodyTypeFormatPluginGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        FormatPluginInjectionPointSelected = true;
                        break;
                    }
                }
                if (FormatPluginSelected && FormatPluginInjectionPointSelected)
                {
                    NewScanner.InjectionArrayXML = FormatXMLTB.Text;
                    NewScanner.XmlInjectionArray = new string[BodyTypeFormatPluginGrid.Rows.Count, 2];
                    NewScanner.BodyXmlInjectionParameters = new Parameters();
                    for (int i = 0; i < BodyTypeFormatPluginGrid.Rows.Count; i++)
                    {
                        NewScanner.XmlInjectionArray[i, 0] = BodyTypeFormatPluginGrid.Rows[i].Cells[1].Value.ToString();
                        NewScanner.XmlInjectionArray[i, 1] = BodyTypeFormatPluginGrid.Rows[i].Cells[2].Value.ToString();
                        NewScanner.BodyXmlInjectionParameters.Add(NewScanner.XmlInjectionArray[i, 0], NewScanner.XmlInjectionArray[i, 1]);
                    }

                    foreach (DataGridViewRow Row in this.BodyTypeFormatPluginGrid.Rows)
                    {
                        if ((bool)Row.Cells[0].Value)
                        {
                            NewScanner.InjectBody(Row.Index);
                        }
                    }
                }
            }
            else if (BodyTypeCustomRB.Checked)
            {
                if (ScanBodyCB.Checked)
                {
                    Request RequestToScanClone = RequestToScan.GetClone();
                    RequestToScanClone.BodyString = SetCustomInjectionPointsSTB.Text;
                    if (ScanJobMode)
                    {
                        NewScanner = new Scanner(RequestToScanClone);
                    }
                    else
                    {
                        NewScanner = new Fuzzer(RequestToScanClone);
                        this.Fuzz = (Fuzzer) NewScanner;
                    }
                    NewScanner.InjectBody(CurrentStartMarker, CurrentEndMarker);
                    lock (Scanner.UserSpecifiedEncodingRuleList)
                    {
                        Scanner.UserSpecifiedEncodingRuleList.Clear();
                        foreach (DataGridViewRow Row in CharacterEscapingGrid.Rows)
                        {
                            Scanner.UserSpecifiedEncodingRuleList.Add(new string[] { Row.Cells[1].Value.ToString(), Row.Cells[3].Value.ToString() });
                            if ((bool)Row.Cells[0].Value)
                                NewScanner.AddEscapeRule(Row.Cells[1].Value.ToString(), Row.Cells[3].Value.ToString());
                        }
                    }
                    IronDB.StoreCharacterEscapingRules();
                }
                else
                {
                    NewScanner.CustomInjectionPointStartMarker = "";
                    NewScanner.CustomInjectionPointEndMarker = "";
                }
            }
            #endregion
            #region UrlPathPartsInjectionPoints
            for (int i = 0; i < this.ScanURLGrid.Rows.Count; i++)
            {
                if ((bool)this.ScanURLGrid.Rows[i].Cells[0].Value)
                {
                    NewScanner.InjectUrl(i);
                }
            }
            #endregion
            #region QueryInjectionPoints
            SubParameterPosition = 0;
            ParameterName = "";
            foreach (DataGridViewRow Row in this.ScanQueryGrid.Rows)
            {
                string CurrentParameterName = Row.Cells[1].Value.ToString();
                if (ParameterName.Equals(CurrentParameterName))
                {
                    SubParameterPosition++;
                }
                else
                {
                    ParameterName = CurrentParameterName;
                    SubParameterPosition = 0;
                }
                if ((bool)Row.Cells[0].Value)
                {
                    NewScanner.InjectQuery(ParameterName, SubParameterPosition);
                }
            }
            #endregion
            #region CookieInjectionPoints
            SubParameterPosition = 0;
            ParameterName = "";
            foreach (DataGridViewRow Row in this.ScanCookieGrid.Rows)
            {
                string CurrentParameterName = Row.Cells[1].Value.ToString();
                if (ParameterName.Equals(CurrentParameterName))
                {
                    SubParameterPosition++;
                }
                else
                {
                    ParameterName = CurrentParameterName;
                    SubParameterPosition = 0;
                }
                if ((bool)Row.Cells[0].Value)
                {
                    NewScanner.InjectCookie(ParameterName, SubParameterPosition);
                }
            }
            #endregion
            #region HeaderInjectionPoints
            SubParameterPosition = 0;
            ParameterName = "";
            foreach (DataGridViewRow Row in this.ScanHeadersGrid.Rows)
            {
                string CurrentParameterName = Row.Cells[1].Value.ToString();
                if (ParameterName.Equals(CurrentParameterName))
                {
                    SubParameterPosition++;
                }
                else
                {
                    ParameterName = CurrentParameterName;
                    SubParameterPosition = 0;
                }
                if ((bool)Row.Cells[0].Value)
                {
                    NewScanner.InjectHeaders(ParameterName, SubParameterPosition);
                }
            }
            #endregion
            #region ParameterNameInjectionPoints
            if (ScanQueryParameterNameCB.Checked)
            {
                NewScanner.InjectParameterName("Query");
            }
            if (ScanBodyParameterNameCB.Checked)
            {
                NewScanner.InjectParameterName("Body");
            }
            if (ScanCookieParameterNameCB.Checked)
            {
                NewScanner.InjectParameterName("Cookie");
            }
            if (ScanHeadersParameterNameCB.Checked)
            {
                NewScanner.InjectParameterName("Headers");
            }
            #endregion

            #region SetSessionPlugin
            SessionPluginName = SessionPluginsCombo.Text;
            if (SessionPluginName.Length > 0)
            {
                NewScanner.SessionHandler = SessionPlugin.Get(SessionPluginName);
            }
            #endregion

            
            #region SetChecks
            if (this.ScanJobMode)
            {
                foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value) NewScanner.AddCheck(Row.Cells[1].Value.ToString());
                }
            }
            #endregion
            return NewScanner;
        }

        private void AddToEscapeRuleBtn_Click(object sender, EventArgs e)
        {
            ShowCharacterEscapeError("");
            string RawCharacter = RawCharacterTB.Text;
            string EncodedCharacter = EncodedCharacterTB.Text;
            foreach (DataGridViewRow Row in CharacterEscapingGrid.Rows)
            {
                if (Row.Cells[1].Value.ToString().Equals(RawCharacter) && Row.Cells[3].Value.ToString().Equals(EncodedCharacter))
                {
                    ShowCharacterEscapeError("This rule already exists.");
                    return;
                }
            }
            int RowId = CharacterEscapingGrid.Rows.Add(new object[]{false, RawCharacter, "->", EncodedCharacter});
            try
            {
                CharacterEscapingGrid.FirstDisplayedScrollingRowIndex = RowId;
            }
            catch { }
            RawCharacterTB.Text = "";
            EncodedCharacterTB.Text = "";
        }

        void ShowCharacterEscapeError(string Error)
        {
            CharacterEscapingStatusTB.Text = Error;
            if (Error.Length == 0)
                CharacterEscapingStatusTB.Visible = false;
            else
                CharacterEscapingStatusTB.Visible = true;
        }

        private void EditRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RawCharacterTB.Text = CharacterEscapingGrid.SelectedRows[0].Cells[1].Value.ToString();
                EncodedCharacterTB.Text = CharacterEscapingGrid.SelectedRows[0].Cells[3].Value.ToString();
                CharacterEscapingGrid.Rows.RemoveAt(CharacterEscapingGrid.SelectedRows[0].Index);
                ShowCharacterEscapeError("");
            }
            catch { }
        }

        private void DeleteRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CharacterEscapingGrid.Rows.RemoveAt(CharacterEscapingGrid.SelectedRows[0].Index);
            }
            catch { }
        }

        private void CharacterEscapingMenu_Opening(object sender, CancelEventArgs e)
        {
            if (CharacterEscapingGrid.SelectedCells.Count < 1 || CharacterEscapingGrid.SelectedCells[0].Value == null)
            {
                EditRuleToolStripMenuItem.Enabled = false;
                DeleteRuleToolStripMenuItem.Enabled = false;
            }
            else
            {
                EditRuleToolStripMenuItem.Enabled = true;
                DeleteRuleToolStripMenuItem.Enabled = true;
            }
        }

        private void BodyInjectTypeTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (BodyInjectTypeTabs.SelectedTab.Name)
            {
                case ("BodyTypeNormalTab"):
                    if (BodyTypeNormalRB.Checked)
                        return;
                    break;
                case ("BodyTypeFormatPluginTab"):
                    if (BodyTypeFormatPluginRB.Checked)
                        return;
                    break;
                case ("BodyTypeCustomTab"):
                    if (BodyTypeCustomRB.Checked)
                        return;
                    break;
            }
            if (BodyTypeNormalRB.Checked)
            {
                BodyInjectTypeTabs.SelectTab("BodyTypeNormalTab");
            }
            else if (BodyTypeFormatPluginRB.Checked)
            {
                BodyInjectTypeTabs.SelectTab("BodyTypeFormatPluginTab");
            }
            else if (BodyTypeCustomRB.Checked)
            {
                BodyInjectTypeTabs.SelectTab("BodyTypeCustomTab");
            }
            else
            {
                BodyTypeNormalRB.Checked = true;
                BodyInjectTypeTabs.SelectTab("BodyTypeNormalTab");
            }
        }

        private void CharacterEscapingGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ShowCharacterEscapeError("");
                DataGridViewRow SelectedRow = CharacterEscapingGrid.Rows[e.RowIndex];
                if ((bool)SelectedRow.Cells[0].Value)
                {
                    SelectedRow.Cells[0].Value = false;
                }
                else
                {
                    string RawCharacter = SelectedRow.Cells[1].Value.ToString();
                    string EncodedCharacter = SelectedRow.Cells[3].Value.ToString();
                    foreach (DataGridViewRow R in CharacterEscapingGrid.Rows)
                    {
                        if ((bool)R.Cells[0].Value && R.Cells[1].Value.ToString().Equals(RawCharacter))
                        {
                            ShowCharacterEscapeError(string.Format("A rule for {0} has already been selected", RawCharacter));
                            return;
                        }
                    }
                    SelectedRow.Cells[0].Value = true;
                }
            }
            catch { }
        }

        private void UseBlackListCB_Click(object sender, EventArgs e)
        {
            if (UseBlackListCB.Checked)
            {
                EnforceParametersBlackList();
            }
        }

        void EnforceParametersBlackList()
        {
            foreach (DataGridViewRow Row in ScanQueryGrid.Rows)
            {
                if (ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                    Row.Cells[0].Value = false;
            }
            foreach (DataGridViewRow Row in ScanBodyTypeNormalGrid.Rows)
            {
                if (ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                    Row.Cells[0].Value = false;
            }
            foreach (DataGridViewRow Row in ScanCookieGrid.Rows)
            {
                if (ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                    Row.Cells[0].Value = false;
            }
            foreach (DataGridViewRow Row in ScanHeadersGrid.Rows)
            {
                if (ParametersBlackList.Contains(Row.Cells[1].Value.ToString()))
                    Row.Cells[0].Value = false;
            }
            UpdateInjectionPointLabels();
        }

        private void ParametersBlacklistTB_TextChanged(object sender, EventArgs e)
        {
            WasParametersBlackListChanged = true;
        }

        private void InjectionPointBaseTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (InjectionPointBaseTabs.SelectedTab.Name.Equals("BlackListTab"))
            {
                if (WasParametersBlackListChanged) CheckAndUpdateParametersBlackList();
            }
        }



        void CheckAndUpdateParametersBlackList()
        {
            if (WasParametersBlackListChanged)
            {
                string[] NewList = ParametersBlacklistTB.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                lock (ParametersBlackList)
                {
                    ParametersBlackList.Clear();
                    foreach (string NL in NewList)
                    {
                        string Item = NL.Trim();
                        if (!ParametersBlackList.Contains(Item)) ParametersBlackList.Add(Item);
                    }
                }
                IronDB.StoreParametersBlackList();
                WasParametersBlackListChanged = false;
            }
        }

        private void PlaceInjectionMarkerLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!CustomInjectionMarkerTabs.SelectedTab.Name.Equals("CustomMarkerSelectionTab"))
            {
                CustomInjectionMarkerTabs.SelectTab("CustomMarkerSelectionTab");
                return;
            }
            string StartMarker = CustomStartMarkerTB.Text.Trim();
            string EndMarker = CustomEndMarkerTB.Text.Trim();
            if (StartMarker.Length == 0 || EndMarker.Length == 0)
            {
                ShowStep2Error("Start and End markers cannot be empty.");
                return;
            }
            if (StartMarker.Equals(EndMarker))
            {
                ShowStep2Error("Start and End markers cannot be the same.");
                return;
            }
            
            string ErrMsg = SetCustomInjectionPointsSTB.PlaceMarkersAroundSelectedText(StartMarker, EndMarker);
            ShowStep2Error(ErrMsg);
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

        private void StartScanJobWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IronUI.UI.CanShutdown) return;
            if (!CanClose)
            {
                if (this.CurrentStep == 0)
                {
                    this.CanClose = true;
                }
                else if ((this.ScanJobMode && this.CurrentStep == 3) || (!this.ScanJobMode && this.CurrentStep == 2))
                {
                    if (this.ScanJobMode)
                    {
                        e.Cancel = true;
                        MessageBox.Show("If you have started the scan then you can close this window by clicking on the 'Close' button on the button right corner of the window.\r\n Otherwise use the '<- Previous Step' button on the bottom left corner to go to the first step and then press the 'Cancel' button on the bottom left corner.");
                    }
                    else
                    {
                        e.Cancel = true;
                        MessageBox.Show("You can close this window by clicking on the 'Done' button on the button right corner of the window.");
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
