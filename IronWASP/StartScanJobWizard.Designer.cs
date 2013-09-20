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

namespace IronWASP
{
    partial class StartScanJobWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScanJobWizard));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.RequestTab = new System.Windows.Forms.TabPage();
            this.Step0StatusTB = new System.Windows.Forms.TextBox();
            this.RequestSSLCB = new System.Windows.Forms.CheckBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.Step0TopMsgTB = new System.Windows.Forms.TextBox();
            this.StepOneNextBtn = new System.Windows.Forms.Button();
            this.RequestTabs = new System.Windows.Forms.TabControl();
            this.tabPage20 = new System.Windows.Forms.TabPage();
            this.RequestRawHeadersIDV = new IronDataView.IronDataView();
            this.tabPage21 = new System.Windows.Forms.TabPage();
            this.RequestRawBodyIDV = new IronDataView.IronDataView();
            this.ChecksTab = new System.Windows.Forms.TabPage();
            this.Step1StatusTB = new System.Windows.Forms.TextBox();
            this.SelectAllChecksCB = new System.Windows.Forms.CheckBox();
            this.Step1TopMsgTB = new System.Windows.Forms.TextBox();
            this.StepTwoPreviousBtn = new System.Windows.Forms.Button();
            this.StepTwoNextBtn = new System.Windows.Forms.Button();
            this.ScanPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InjectionTab = new System.Windows.Forms.TabPage();
            this.Step2StatusTB = new System.Windows.Forms.TextBox();
            this.Step2ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Step2TopMsgTB = new System.Windows.Forms.TextBox();
            this.StepThreePreviousBtn = new System.Windows.Forms.Button();
            this.StepThreeNextBtn = new System.Windows.Forms.Button();
            this.InjectionPointBaseTabs = new System.Windows.Forms.TabControl();
            this.AllTab = new System.Windows.Forms.TabPage();
            this.ScanHeadersCB = new System.Windows.Forms.CheckBox();
            this.AllHeaderPointsAvlLbl = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ScanQueryCB = new System.Windows.Forms.CheckBox();
            this.AllQueryPointsAvlLbl = new System.Windows.Forms.Label();
            this.AllQueryPointsSelectedLbl = new System.Windows.Forms.Label();
            this.AllHeaderPointsSelectedLbl = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ScanAllCB = new System.Windows.Forms.CheckBox();
            this.AllPointsAvlLbl = new System.Windows.Forms.Label();
            this.AllPointsSelectedLbl = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ScanCookieCB = new System.Windows.Forms.CheckBox();
            this.AllCookiePointsAvlLbl = new System.Windows.Forms.Label();
            this.AllCookiePointsSelectedLbl = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.AllNamesPointsAvlLbl = new System.Windows.Forms.Label();
            this.ScanParameterNamesCB = new System.Windows.Forms.CheckBox();
            this.AllNamesPointsSelectedLbl = new System.Windows.Forms.Label();
            this.AllBodyPointsSelectedLbl = new System.Windows.Forms.Label();
            this.AllBodyPointsAvlLbl = new System.Windows.Forms.Label();
            this.AllUrlPointsSelectedLbl = new System.Windows.Forms.Label();
            this.AllUrlPointsAvlLbl = new System.Windows.Forms.Label();
            this.BodyInjectionMessageLbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BlacklistItemsCountLbl = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.UseBlackListCB = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UrlPathPartInjectionMessageLbl = new System.Windows.Forms.Label();
            this.ScanURLCB = new System.Windows.Forms.CheckBox();
            this.ScanBodyCB = new System.Windows.Forms.CheckBox();
            this.URLTab = new System.Windows.Forms.TabPage();
            this.ScanURLGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestURLSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestURLPositionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestURLValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QueryTab = new System.Windows.Forms.TabPage();
            this.ScanQueryGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestQuerySelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestQueryNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestQueryValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BodyTab = new System.Windows.Forms.TabPage();
            this.BodyTypeCustomRB = new System.Windows.Forms.RadioButton();
            this.BodyTypeFormatPluginRB = new System.Windows.Forms.RadioButton();
            this.BodyTypeNormalRB = new System.Windows.Forms.RadioButton();
            this.label35 = new System.Windows.Forms.Label();
            this.BodyInjectTypeTabs = new System.Windows.Forms.TabControl();
            this.BodyTypeNormalTab = new System.Windows.Forms.TabPage();
            this.ScanBodyTypeNormalGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BodyTypeFormatPluginTab = new System.Windows.Forms.TabPage();
            this.ASRequestBodyTabSplit = new System.Windows.Forms.SplitContainer();
            this.FormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestBodyDataFormatSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestBodyDataFormatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanBodyFormatPluginTypeTabs = new System.Windows.Forms.TabControl();
            this.BodyTypeFormatPluginInjectionArrayGridTab = new System.Windows.Forms.TabPage();
            this.BodyTypeFormatPluginGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestBodySelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestBodyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestBodyValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BodyTypeFormatPluginXMLTab = new System.Windows.Forms.TabPage();
            this.FormatXMLTB = new System.Windows.Forms.TextBox();
            this.BodyTypeCustomTab = new System.Windows.Forms.TabPage();
            this.PlaceInjectionMarkerLL = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.CustomInjectionPointsHighlightLbl = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.CustomEndMarkerTB = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ASApplyCustomMarkersLL = new System.Windows.Forms.LinkLabel();
            this.CustomInjectionMarkerTabs = new System.Windows.Forms.TabControl();
            this.CustomMarkerSelectionTab = new System.Windows.Forms.TabPage();
            this.SetCustomInjectionPointsSTB = new IronWASP.SearchableTextBox();
            this.CustomMarkerDisplayTab = new System.Windows.Forms.TabPage();
            this.HighlightCustomInjectionPointsRTB = new System.Windows.Forms.RichTextBox();
            this.CustomMarkerEscapingTab = new System.Windows.Forms.TabPage();
            this.CharacterEscapingStatusTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.EncodedCharacterTB = new System.Windows.Forms.TextBox();
            this.RawCharacterTB = new System.Windows.Forms.TextBox();
            this.AddToEscapeRuleBtn = new System.Windows.Forms.Button();
            this.CharacterEscapingGrid = new System.Windows.Forms.DataGridView();
            this.EscapingSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RawCharacterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArrowColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EncodedCharacterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CharacterEscapingMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomStartMarkerTB = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.CookieTab = new System.Windows.Forms.TabPage();
            this.ScanCookieGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestCookieSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestCookieNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestCookieValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeadersTab = new System.Windows.Forms.TabPage();
            this.ScanHeadersGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestHeadersSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestHeadersNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestHeadersValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterNamesTab = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ScanHeadersParameterNameCB = new System.Windows.Forms.CheckBox();
            this.ScanCookieParameterNameCB = new System.Windows.Forms.CheckBox();
            this.ScanBodyParameterNameCB = new System.Windows.Forms.CheckBox();
            this.ScanQueryParameterNameCB = new System.Windows.Forms.CheckBox();
            this.BlackListTab = new System.Windows.Forms.TabPage();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.ParametersBlacklistTB = new System.Windows.Forms.TextBox();
            this.CustomizeTab = new System.Windows.Forms.TabPage();
            this.SessionPluginsCombo = new System.Windows.Forms.ComboBox();
            this.ScanThreadLimitCB = new System.Windows.Forms.CheckBox();
            this.LaunchSessionPluginCreationAssistantLL = new System.Windows.Forms.LinkLabel();
            this.RefreshSessListLL = new System.Windows.Forms.LinkLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Step3StatusTB = new System.Windows.Forms.TextBox();
            this.StepFourPreviousBtn = new System.Windows.Forms.Button();
            this.FinalBtn = new System.Windows.Forms.Button();
            this.BaseTabs.SuspendLayout();
            this.RequestTab.SuspendLayout();
            this.RequestTabs.SuspendLayout();
            this.tabPage20.SuspendLayout();
            this.tabPage21.SuspendLayout();
            this.ChecksTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanPluginsGrid)).BeginInit();
            this.InjectionTab.SuspendLayout();
            this.InjectionPointBaseTabs.SuspendLayout();
            this.AllTab.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.URLTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanURLGrid)).BeginInit();
            this.QueryTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanQueryGrid)).BeginInit();
            this.BodyTab.SuspendLayout();
            this.BodyInjectTypeTabs.SuspendLayout();
            this.BodyTypeNormalTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanBodyTypeNormalGrid)).BeginInit();
            this.BodyTypeFormatPluginTab.SuspendLayout();
            this.ASRequestBodyTabSplit.Panel1.SuspendLayout();
            this.ASRequestBodyTabSplit.Panel2.SuspendLayout();
            this.ASRequestBodyTabSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormatPluginsGrid)).BeginInit();
            this.ScanBodyFormatPluginTypeTabs.SuspendLayout();
            this.BodyTypeFormatPluginInjectionArrayGridTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BodyTypeFormatPluginGrid)).BeginInit();
            this.BodyTypeFormatPluginXMLTab.SuspendLayout();
            this.BodyTypeCustomTab.SuspendLayout();
            this.CustomInjectionMarkerTabs.SuspendLayout();
            this.CustomMarkerSelectionTab.SuspendLayout();
            this.CustomMarkerDisplayTab.SuspendLayout();
            this.CustomMarkerEscapingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CharacterEscapingGrid)).BeginInit();
            this.CharacterEscapingMenu.SuspendLayout();
            this.CookieTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanCookieGrid)).BeginInit();
            this.HeadersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanHeadersGrid)).BeginInit();
            this.ParameterNamesTab.SuspendLayout();
            this.BlackListTab.SuspendLayout();
            this.CustomizeTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseTabs
            // 
            this.BaseTabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.BaseTabs.Controls.Add(this.RequestTab);
            this.BaseTabs.Controls.Add(this.ChecksTab);
            this.BaseTabs.Controls.Add(this.InjectionTab);
            this.BaseTabs.Controls.Add(this.CustomizeTab);
            this.BaseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseTabs.Location = new System.Drawing.Point(0, 0);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(884, 561);
            this.BaseTabs.TabIndex = 15;
            this.BaseTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.BaseTabs_Selecting);
            // 
            // RequestTab
            // 
            this.RequestTab.Controls.Add(this.Step0StatusTB);
            this.RequestTab.Controls.Add(this.RequestSSLCB);
            this.RequestTab.Controls.Add(this.CancelBtn);
            this.RequestTab.Controls.Add(this.Step0TopMsgTB);
            this.RequestTab.Controls.Add(this.StepOneNextBtn);
            this.RequestTab.Controls.Add(this.RequestTabs);
            this.RequestTab.Location = new System.Drawing.Point(4, 25);
            this.RequestTab.Margin = new System.Windows.Forms.Padding(0);
            this.RequestTab.Name = "RequestTab";
            this.RequestTab.Padding = new System.Windows.Forms.Padding(5);
            this.RequestTab.Size = new System.Drawing.Size(876, 532);
            this.RequestTab.TabIndex = 0;
            this.RequestTab.Text = "               Edit Request               ";
            this.RequestTab.UseVisualStyleBackColor = true;
            // 
            // Step0StatusTB
            // 
            this.Step0StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step0StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step0StatusTB.Location = new System.Drawing.Point(131, 476);
            this.Step0StatusTB.Multiline = true;
            this.Step0StatusTB.Name = "Step0StatusTB";
            this.Step0StatusTB.Size = new System.Drawing.Size(607, 48);
            this.Step0StatusTB.TabIndex = 6;
            this.Step0StatusTB.TabStop = false;
            this.Step0StatusTB.Visible = false;
            // 
            // RequestSSLCB
            // 
            this.RequestSSLCB.AutoSize = true;
            this.RequestSSLCB.Location = new System.Drawing.Point(13, 466);
            this.RequestSSLCB.Name = "RequestSSLCB";
            this.RequestSSLCB.Size = new System.Drawing.Size(114, 17);
            this.RequestSSLCB.TabIndex = 5;
            this.RequestSSLCB.Text = "Request uses SSL";
            this.RequestSSLCB.UseVisualStyleBackColor = true;
            this.RequestSSLCB.CheckedChanged += new System.EventHandler(this.RequestSSLCB_CheckedChanged);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(3, 496);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(105, 23);
            this.CancelBtn.TabIndex = 4;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // Step0TopMsgTB
            // 
            this.Step0TopMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step0TopMsgTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.Step0TopMsgTB.Location = new System.Drawing.Point(5, 5);
            this.Step0TopMsgTB.Multiline = true;
            this.Step0TopMsgTB.Name = "Step0TopMsgTB";
            this.Step0TopMsgTB.ReadOnly = true;
            this.Step0TopMsgTB.Size = new System.Drawing.Size(866, 70);
            this.Step0TopMsgTB.TabIndex = 3;
            this.Step0TopMsgTB.TabStop = false;
            this.Step0TopMsgTB.Text = resources.GetString("Step0TopMsgTB.Text");
            // 
            // StepOneNextBtn
            // 
            this.StepOneNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepOneNextBtn.Location = new System.Drawing.Point(755, 495);
            this.StepOneNextBtn.Name = "StepOneNextBtn";
            this.StepOneNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepOneNextBtn.TabIndex = 2;
            this.StepOneNextBtn.Text = "Next Step ->";
            this.StepOneNextBtn.UseVisualStyleBackColor = true;
            this.StepOneNextBtn.Click += new System.EventHandler(this.StepOneNextBtn_Click);
            // 
            // RequestTabs
            // 
            this.RequestTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RequestTabs.Controls.Add(this.tabPage20);
            this.RequestTabs.Controls.Add(this.tabPage21);
            this.RequestTabs.Location = new System.Drawing.Point(5, 82);
            this.RequestTabs.Margin = new System.Windows.Forms.Padding(0);
            this.RequestTabs.Multiline = true;
            this.RequestTabs.Name = "RequestTabs";
            this.RequestTabs.Padding = new System.Drawing.Point(0, 0);
            this.RequestTabs.SelectedIndex = 0;
            this.RequestTabs.Size = new System.Drawing.Size(866, 368);
            this.RequestTabs.TabIndex = 1;
            // 
            // tabPage20
            // 
            this.tabPage20.Controls.Add(this.RequestRawHeadersIDV);
            this.tabPage20.Location = new System.Drawing.Point(4, 22);
            this.tabPage20.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage20.Name = "tabPage20";
            this.tabPage20.Size = new System.Drawing.Size(858, 342);
            this.tabPage20.TabIndex = 0;
            this.tabPage20.Text = "Raw Headers";
            this.tabPage20.UseVisualStyleBackColor = true;
            // 
            // RequestRawHeadersIDV
            // 
            this.RequestRawHeadersIDV.AutoSize = true;
            this.RequestRawHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequestRawHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.RequestRawHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.RequestRawHeadersIDV.Name = "RequestRawHeadersIDV";
            this.RequestRawHeadersIDV.ReadOnly = false;
            this.RequestRawHeadersIDV.Size = new System.Drawing.Size(858, 342);
            this.RequestRawHeadersIDV.TabIndex = 0;
            this.RequestRawHeadersIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.RequestRawHeadersIDV_IDVTextChanged);
            // 
            // tabPage21
            // 
            this.tabPage21.Controls.Add(this.RequestRawBodyIDV);
            this.tabPage21.Location = new System.Drawing.Point(4, 22);
            this.tabPage21.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage21.Name = "tabPage21";
            this.tabPage21.Size = new System.Drawing.Size(858, 342);
            this.tabPage21.TabIndex = 1;
            this.tabPage21.Text = "Raw Body";
            this.tabPage21.UseVisualStyleBackColor = true;
            // 
            // RequestRawBodyIDV
            // 
            this.RequestRawBodyIDV.AutoSize = true;
            this.RequestRawBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequestRawBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.RequestRawBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.RequestRawBodyIDV.Name = "RequestRawBodyIDV";
            this.RequestRawBodyIDV.ReadOnly = false;
            this.RequestRawBodyIDV.Size = new System.Drawing.Size(858, 342);
            this.RequestRawBodyIDV.TabIndex = 1;
            this.RequestRawBodyIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.RequestRawBodyIDV_IDVTextChanged);
            // 
            // ChecksTab
            // 
            this.ChecksTab.Controls.Add(this.Step1StatusTB);
            this.ChecksTab.Controls.Add(this.SelectAllChecksCB);
            this.ChecksTab.Controls.Add(this.Step1TopMsgTB);
            this.ChecksTab.Controls.Add(this.StepTwoPreviousBtn);
            this.ChecksTab.Controls.Add(this.StepTwoNextBtn);
            this.ChecksTab.Controls.Add(this.ScanPluginsGrid);
            this.ChecksTab.Location = new System.Drawing.Point(4, 25);
            this.ChecksTab.Margin = new System.Windows.Forms.Padding(0);
            this.ChecksTab.Name = "ChecksTab";
            this.ChecksTab.Padding = new System.Windows.Forms.Padding(5);
            this.ChecksTab.Size = new System.Drawing.Size(876, 532);
            this.ChecksTab.TabIndex = 1;
            this.ChecksTab.Text = "               Select Checks               ";
            this.ChecksTab.UseVisualStyleBackColor = true;
            // 
            // Step1StatusTB
            // 
            this.Step1StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step1StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step1StatusTB.Location = new System.Drawing.Point(259, 436);
            this.Step1StatusTB.Multiline = true;
            this.Step1StatusTB.Name = "Step1StatusTB";
            this.Step1StatusTB.Size = new System.Drawing.Size(607, 50);
            this.Step1StatusTB.TabIndex = 11;
            this.Step1StatusTB.TabStop = false;
            this.Step1StatusTB.Visible = false;
            // 
            // SelectAllChecksCB
            // 
            this.SelectAllChecksCB.AutoSize = true;
            this.SelectAllChecksCB.Location = new System.Drawing.Point(13, 81);
            this.SelectAllChecksCB.Name = "SelectAllChecksCB";
            this.SelectAllChecksCB.Size = new System.Drawing.Size(109, 17);
            this.SelectAllChecksCB.TabIndex = 10;
            this.SelectAllChecksCB.Text = "Select All Checks";
            this.SelectAllChecksCB.UseVisualStyleBackColor = true;
            this.SelectAllChecksCB.Click += new System.EventHandler(this.SelectAllChecksCB_Click);
            // 
            // Step1TopMsgTB
            // 
            this.Step1TopMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step1TopMsgTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.Step1TopMsgTB.Location = new System.Drawing.Point(5, 5);
            this.Step1TopMsgTB.Multiline = true;
            this.Step1TopMsgTB.Name = "Step1TopMsgTB";
            this.Step1TopMsgTB.ReadOnly = true;
            this.Step1TopMsgTB.Size = new System.Drawing.Size(866, 70);
            this.Step1TopMsgTB.TabIndex = 9;
            this.Step1TopMsgTB.TabStop = false;
            this.Step1TopMsgTB.Text = "\r\nThe list below shows the various web application security vulnerabilities that " +
    "IronWAP can scan for.\r\n\r\nSelect the checks that you want to be performed on this" +
    " request.\r\n";
            // 
            // StepTwoPreviousBtn
            // 
            this.StepTwoPreviousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepTwoPreviousBtn.Location = new System.Drawing.Point(10, 499);
            this.StepTwoPreviousBtn.Name = "StepTwoPreviousBtn";
            this.StepTwoPreviousBtn.Size = new System.Drawing.Size(105, 23);
            this.StepTwoPreviousBtn.TabIndex = 8;
            this.StepTwoPreviousBtn.Text = "<-Previous Step";
            this.StepTwoPreviousBtn.UseVisualStyleBackColor = true;
            this.StepTwoPreviousBtn.Click += new System.EventHandler(this.StepTwoPreviousBtn_Click);
            // 
            // StepTwoNextBtn
            // 
            this.StepTwoNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepTwoNextBtn.Location = new System.Drawing.Point(760, 500);
            this.StepTwoNextBtn.Name = "StepTwoNextBtn";
            this.StepTwoNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepTwoNextBtn.TabIndex = 7;
            this.StepTwoNextBtn.Text = "Next Step ->";
            this.StepTwoNextBtn.UseVisualStyleBackColor = true;
            this.StepTwoNextBtn.Click += new System.EventHandler(this.StepTwoNextBtn_Click);
            // 
            // ScanPluginsGrid
            // 
            this.ScanPluginsGrid.AllowUserToAddRows = false;
            this.ScanPluginsGrid.AllowUserToDeleteRows = false;
            this.ScanPluginsGrid.AllowUserToResizeRows = false;
            this.ScanPluginsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanPluginsGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn9,
            this.dataGridViewTextBoxColumn27});
            this.ScanPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanPluginsGrid.GridColor = System.Drawing.Color.White;
            this.ScanPluginsGrid.Location = new System.Drawing.Point(1, 109);
            this.ScanPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanPluginsGrid.MultiSelect = false;
            this.ScanPluginsGrid.Name = "ScanPluginsGrid";
            this.ScanPluginsGrid.ReadOnly = true;
            this.ScanPluginsGrid.RowHeadersVisible = false;
            this.ScanPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanPluginsGrid.Size = new System.Drawing.Size(240, 298);
            this.ScanPluginsGrid.TabIndex = 6;
            this.ScanPluginsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanPluginsGrid_CellClick);
            // 
            // dataGridViewCheckBoxColumn9
            // 
            this.dataGridViewCheckBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewCheckBoxColumn9.HeaderText = "";
            this.dataGridViewCheckBoxColumn9.Name = "dataGridViewCheckBoxColumn9";
            this.dataGridViewCheckBoxColumn9.ReadOnly = true;
            this.dataGridViewCheckBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn9.Width = 20;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn27.HeaderText = "SELECT CHECKS";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // InjectionTab
            // 
            this.InjectionTab.Controls.Add(this.Step2StatusTB);
            this.InjectionTab.Controls.Add(this.Step2ProgressBar);
            this.InjectionTab.Controls.Add(this.Step2TopMsgTB);
            this.InjectionTab.Controls.Add(this.StepThreePreviousBtn);
            this.InjectionTab.Controls.Add(this.StepThreeNextBtn);
            this.InjectionTab.Controls.Add(this.InjectionPointBaseTabs);
            this.InjectionTab.Location = new System.Drawing.Point(4, 25);
            this.InjectionTab.Name = "InjectionTab";
            this.InjectionTab.Padding = new System.Windows.Forms.Padding(5);
            this.InjectionTab.Size = new System.Drawing.Size(876, 532);
            this.InjectionTab.TabIndex = 2;
            this.InjectionTab.Text = "               Set Injection Points               ";
            this.InjectionTab.UseVisualStyleBackColor = true;
            // 
            // Step2StatusTB
            // 
            this.Step2StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step2StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step2StatusTB.Location = new System.Drawing.Point(124, 509);
            this.Step2StatusTB.Multiline = true;
            this.Step2StatusTB.Name = "Step2StatusTB";
            this.Step2StatusTB.Size = new System.Drawing.Size(628, 18);
            this.Step2StatusTB.TabIndex = 13;
            this.Step2StatusTB.TabStop = false;
            this.Step2StatusTB.Visible = false;
            // 
            // Step2ProgressBar
            // 
            this.Step2ProgressBar.Location = new System.Drawing.Point(269, 505);
            this.Step2ProgressBar.MarqueeAnimationSpeed = 10;
            this.Step2ProgressBar.Name = "Step2ProgressBar";
            this.Step2ProgressBar.Size = new System.Drawing.Size(301, 22);
            this.Step2ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Step2ProgressBar.TabIndex = 12;
            // 
            // Step2TopMsgTB
            // 
            this.Step2TopMsgTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step2TopMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step2TopMsgTB.Location = new System.Drawing.Point(5, 5);
            this.Step2TopMsgTB.Multiline = true;
            this.Step2TopMsgTB.Name = "Step2TopMsgTB";
            this.Step2TopMsgTB.ReadOnly = true;
            this.Step2TopMsgTB.Size = new System.Drawing.Size(866, 65);
            this.Step2TopMsgTB.TabIndex = 11;
            this.Step2TopMsgTB.TabStop = false;
            this.Step2TopMsgTB.Text = resources.GetString("Step2TopMsgTB.Text");
            // 
            // StepThreePreviousBtn
            // 
            this.StepThreePreviousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepThreePreviousBtn.Location = new System.Drawing.Point(3, 498);
            this.StepThreePreviousBtn.Name = "StepThreePreviousBtn";
            this.StepThreePreviousBtn.Size = new System.Drawing.Size(105, 23);
            this.StepThreePreviousBtn.TabIndex = 10;
            this.StepThreePreviousBtn.Text = "<-Previous Step";
            this.StepThreePreviousBtn.UseVisualStyleBackColor = true;
            this.StepThreePreviousBtn.Click += new System.EventHandler(this.StepThreePreviousBtn_Click);
            // 
            // StepThreeNextBtn
            // 
            this.StepThreeNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepThreeNextBtn.Location = new System.Drawing.Point(758, 499);
            this.StepThreeNextBtn.Name = "StepThreeNextBtn";
            this.StepThreeNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepThreeNextBtn.TabIndex = 9;
            this.StepThreeNextBtn.Text = "Next Step ->";
            this.StepThreeNextBtn.UseVisualStyleBackColor = true;
            this.StepThreeNextBtn.Click += new System.EventHandler(this.StepThreeNextBtn_Click);
            // 
            // InjectionPointBaseTabs
            // 
            this.InjectionPointBaseTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InjectionPointBaseTabs.Controls.Add(this.AllTab);
            this.InjectionPointBaseTabs.Controls.Add(this.URLTab);
            this.InjectionPointBaseTabs.Controls.Add(this.QueryTab);
            this.InjectionPointBaseTabs.Controls.Add(this.BodyTab);
            this.InjectionPointBaseTabs.Controls.Add(this.CookieTab);
            this.InjectionPointBaseTabs.Controls.Add(this.HeadersTab);
            this.InjectionPointBaseTabs.Controls.Add(this.ParameterNamesTab);
            this.InjectionPointBaseTabs.Controls.Add(this.BlackListTab);
            this.InjectionPointBaseTabs.Location = new System.Drawing.Point(0, 75);
            this.InjectionPointBaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.InjectionPointBaseTabs.Name = "InjectionPointBaseTabs";
            this.InjectionPointBaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.InjectionPointBaseTabs.SelectedIndex = 0;
            this.InjectionPointBaseTabs.Size = new System.Drawing.Size(871, 420);
            this.InjectionPointBaseTabs.TabIndex = 1;
            this.InjectionPointBaseTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.InjectionPointBaseTabs_Selecting);
            this.InjectionPointBaseTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.InjectionPointBaseTabs_Deselecting);
            // 
            // AllTab
            // 
            this.AllTab.BackColor = System.Drawing.Color.White;
            this.AllTab.Controls.Add(this.ScanHeadersCB);
            this.AllTab.Controls.Add(this.AllHeaderPointsAvlLbl);
            this.AllTab.Controls.Add(this.panel7);
            this.AllTab.Controls.Add(this.AllHeaderPointsSelectedLbl);
            this.AllTab.Controls.Add(this.panel6);
            this.AllTab.Controls.Add(this.panel4);
            this.AllTab.Controls.Add(this.panel3);
            this.AllTab.Controls.Add(this.AllBodyPointsSelectedLbl);
            this.AllTab.Controls.Add(this.AllBodyPointsAvlLbl);
            this.AllTab.Controls.Add(this.AllUrlPointsSelectedLbl);
            this.AllTab.Controls.Add(this.AllUrlPointsAvlLbl);
            this.AllTab.Controls.Add(this.BodyInjectionMessageLbl);
            this.AllTab.Controls.Add(this.panel1);
            this.AllTab.Controls.Add(this.label5);
            this.AllTab.Controls.Add(this.label3);
            this.AllTab.Controls.Add(this.label2);
            this.AllTab.Controls.Add(this.UrlPathPartInjectionMessageLbl);
            this.AllTab.Controls.Add(this.ScanURLCB);
            this.AllTab.Controls.Add(this.ScanBodyCB);
            this.AllTab.Location = new System.Drawing.Point(4, 22);
            this.AllTab.Name = "AllTab";
            this.AllTab.Size = new System.Drawing.Size(863, 394);
            this.AllTab.TabIndex = 6;
            this.AllTab.Text = "All";
            // 
            // ScanHeadersCB
            // 
            this.ScanHeadersCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanHeadersCB.AutoSize = true;
            this.ScanHeadersCB.Location = new System.Drawing.Point(22, 257);
            this.ScanHeadersCB.Name = "ScanHeadersCB";
            this.ScanHeadersCB.Size = new System.Drawing.Size(131, 17);
            this.ScanHeadersCB.TabIndex = 18;
            this.ScanHeadersCB.Text = "All Header Parameters";
            this.ScanHeadersCB.UseVisualStyleBackColor = true;
            this.ScanHeadersCB.Click += new System.EventHandler(this.ScanHeadersCB_Click);
            // 
            // AllHeaderPointsAvlLbl
            // 
            this.AllHeaderPointsAvlLbl.AutoSize = true;
            this.AllHeaderPointsAvlLbl.Location = new System.Drawing.Point(632, 258);
            this.AllHeaderPointsAvlLbl.Name = "AllHeaderPointsAvlLbl";
            this.AllHeaderPointsAvlLbl.Size = new System.Drawing.Size(13, 13);
            this.AllHeaderPointsAvlLbl.TabIndex = 36;
            this.AllHeaderPointsAvlLbl.Text = "0";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel7.Controls.Add(this.ScanQueryCB);
            this.panel7.Controls.Add(this.AllQueryPointsAvlLbl);
            this.panel7.Controls.Add(this.AllQueryPointsSelectedLbl);
            this.panel7.Location = new System.Drawing.Point(5, 122);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(858, 30);
            this.panel7.TabIndex = 42;
            // 
            // ScanQueryCB
            // 
            this.ScanQueryCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanQueryCB.AutoSize = true;
            this.ScanQueryCB.Location = new System.Drawing.Point(17, 7);
            this.ScanQueryCB.Name = "ScanQueryCB";
            this.ScanQueryCB.Size = new System.Drawing.Size(124, 17);
            this.ScanQueryCB.TabIndex = 15;
            this.ScanQueryCB.Text = "All Query Parameters";
            this.ScanQueryCB.UseVisualStyleBackColor = true;
            this.ScanQueryCB.Click += new System.EventHandler(this.ScanQueryCB_Click);
            // 
            // AllQueryPointsAvlLbl
            // 
            this.AllQueryPointsAvlLbl.AutoSize = true;
            this.AllQueryPointsAvlLbl.Location = new System.Drawing.Point(627, 8);
            this.AllQueryPointsAvlLbl.Name = "AllQueryPointsAvlLbl";
            this.AllQueryPointsAvlLbl.Size = new System.Drawing.Size(13, 13);
            this.AllQueryPointsAvlLbl.TabIndex = 30;
            this.AllQueryPointsAvlLbl.Text = "0";
            // 
            // AllQueryPointsSelectedLbl
            // 
            this.AllQueryPointsSelectedLbl.AutoSize = true;
            this.AllQueryPointsSelectedLbl.Location = new System.Drawing.Point(775, 8);
            this.AllQueryPointsSelectedLbl.Name = "AllQueryPointsSelectedLbl";
            this.AllQueryPointsSelectedLbl.Size = new System.Drawing.Size(13, 13);
            this.AllQueryPointsSelectedLbl.TabIndex = 31;
            this.AllQueryPointsSelectedLbl.Text = "0";
            // 
            // AllHeaderPointsSelectedLbl
            // 
            this.AllHeaderPointsSelectedLbl.AutoSize = true;
            this.AllHeaderPointsSelectedLbl.Location = new System.Drawing.Point(780, 258);
            this.AllHeaderPointsSelectedLbl.Name = "AllHeaderPointsSelectedLbl";
            this.AllHeaderPointsSelectedLbl.Size = new System.Drawing.Size(13, 13);
            this.AllHeaderPointsSelectedLbl.TabIndex = 37;
            this.AllHeaderPointsSelectedLbl.Text = "0";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel6.Controls.Add(this.ScanAllCB);
            this.panel6.Controls.Add(this.AllPointsAvlLbl);
            this.panel6.Controls.Add(this.AllPointsSelectedLbl);
            this.panel6.Location = new System.Drawing.Point(5, 35);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(858, 30);
            this.panel6.TabIndex = 42;
            // 
            // ScanAllCB
            // 
            this.ScanAllCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanAllCB.AutoSize = true;
            this.ScanAllCB.Location = new System.Drawing.Point(16, 7);
            this.ScanAllCB.Name = "ScanAllCB";
            this.ScanAllCB.Size = new System.Drawing.Size(232, 17);
            this.ScanAllCB.TabIndex = 13;
            this.ScanAllCB.Text = "All Parameters in all sections of the Request";
            this.ScanAllCB.UseVisualStyleBackColor = true;
            this.ScanAllCB.Click += new System.EventHandler(this.ScanAllCB_Click);
            // 
            // AllPointsAvlLbl
            // 
            this.AllPointsAvlLbl.AutoSize = true;
            this.AllPointsAvlLbl.Location = new System.Drawing.Point(627, 8);
            this.AllPointsAvlLbl.Name = "AllPointsAvlLbl";
            this.AllPointsAvlLbl.Size = new System.Drawing.Size(13, 13);
            this.AllPointsAvlLbl.TabIndex = 26;
            this.AllPointsAvlLbl.Text = "0";
            // 
            // AllPointsSelectedLbl
            // 
            this.AllPointsSelectedLbl.AutoSize = true;
            this.AllPointsSelectedLbl.Location = new System.Drawing.Point(775, 8);
            this.AllPointsSelectedLbl.Name = "AllPointsSelectedLbl";
            this.AllPointsSelectedLbl.Size = new System.Drawing.Size(13, 13);
            this.AllPointsSelectedLbl.TabIndex = 27;
            this.AllPointsSelectedLbl.Text = "0";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.ScanCookieCB);
            this.panel4.Controls.Add(this.AllCookiePointsAvlLbl);
            this.panel4.Controls.Add(this.AllCookiePointsSelectedLbl);
            this.panel4.Location = new System.Drawing.Point(5, 211);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(858, 30);
            this.panel4.TabIndex = 40;
            // 
            // ScanCookieCB
            // 
            this.ScanCookieCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanCookieCB.AutoSize = true;
            this.ScanCookieCB.Location = new System.Drawing.Point(17, 7);
            this.ScanCookieCB.Name = "ScanCookieCB";
            this.ScanCookieCB.Size = new System.Drawing.Size(129, 17);
            this.ScanCookieCB.TabIndex = 17;
            this.ScanCookieCB.Text = "All Cookie Parameters";
            this.ScanCookieCB.UseVisualStyleBackColor = true;
            this.ScanCookieCB.Click += new System.EventHandler(this.ScanCookieCB_Click);
            // 
            // AllCookiePointsAvlLbl
            // 
            this.AllCookiePointsAvlLbl.AutoSize = true;
            this.AllCookiePointsAvlLbl.Location = new System.Drawing.Point(627, 8);
            this.AllCookiePointsAvlLbl.Name = "AllCookiePointsAvlLbl";
            this.AllCookiePointsAvlLbl.Size = new System.Drawing.Size(13, 13);
            this.AllCookiePointsAvlLbl.TabIndex = 34;
            this.AllCookiePointsAvlLbl.Text = "0";
            // 
            // AllCookiePointsSelectedLbl
            // 
            this.AllCookiePointsSelectedLbl.AutoSize = true;
            this.AllCookiePointsSelectedLbl.Location = new System.Drawing.Point(775, 8);
            this.AllCookiePointsSelectedLbl.Name = "AllCookiePointsSelectedLbl";
            this.AllCookiePointsSelectedLbl.Size = new System.Drawing.Size(13, 13);
            this.AllCookiePointsSelectedLbl.TabIndex = 35;
            this.AllCookiePointsSelectedLbl.Text = "0";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.AllNamesPointsAvlLbl);
            this.panel3.Controls.Add(this.ScanParameterNamesCB);
            this.panel3.Controls.Add(this.AllNamesPointsSelectedLbl);
            this.panel3.Location = new System.Drawing.Point(5, 291);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(858, 30);
            this.panel3.TabIndex = 39;
            // 
            // AllNamesPointsAvlLbl
            // 
            this.AllNamesPointsAvlLbl.AutoSize = true;
            this.AllNamesPointsAvlLbl.Location = new System.Drawing.Point(627, 8);
            this.AllNamesPointsAvlLbl.Name = "AllNamesPointsAvlLbl";
            this.AllNamesPointsAvlLbl.Size = new System.Drawing.Size(13, 13);
            this.AllNamesPointsAvlLbl.TabIndex = 38;
            this.AllNamesPointsAvlLbl.Text = "4";
            // 
            // ScanParameterNamesCB
            // 
            this.ScanParameterNamesCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanParameterNamesCB.AutoSize = true;
            this.ScanParameterNamesCB.Location = new System.Drawing.Point(17, 7);
            this.ScanParameterNamesCB.Name = "ScanParameterNamesCB";
            this.ScanParameterNamesCB.Size = new System.Drawing.Size(366, 17);
            this.ScanParameterNamesCB.TabIndex = 19;
            this.ScanParameterNamesCB.Text = "Parameter Name property of Query, Body, Cookie and Headers Sections";
            this.ScanParameterNamesCB.UseVisualStyleBackColor = true;
            this.ScanParameterNamesCB.Click += new System.EventHandler(this.ScanParameterNamesCB_Click);
            // 
            // AllNamesPointsSelectedLbl
            // 
            this.AllNamesPointsSelectedLbl.AutoSize = true;
            this.AllNamesPointsSelectedLbl.Location = new System.Drawing.Point(775, 8);
            this.AllNamesPointsSelectedLbl.Name = "AllNamesPointsSelectedLbl";
            this.AllNamesPointsSelectedLbl.Size = new System.Drawing.Size(13, 13);
            this.AllNamesPointsSelectedLbl.TabIndex = 39;
            this.AllNamesPointsSelectedLbl.Text = "0";
            // 
            // AllBodyPointsSelectedLbl
            // 
            this.AllBodyPointsSelectedLbl.AutoSize = true;
            this.AllBodyPointsSelectedLbl.Location = new System.Drawing.Point(780, 168);
            this.AllBodyPointsSelectedLbl.Name = "AllBodyPointsSelectedLbl";
            this.AllBodyPointsSelectedLbl.Size = new System.Drawing.Size(13, 13);
            this.AllBodyPointsSelectedLbl.TabIndex = 33;
            this.AllBodyPointsSelectedLbl.Text = "0";
            // 
            // AllBodyPointsAvlLbl
            // 
            this.AllBodyPointsAvlLbl.AutoSize = true;
            this.AllBodyPointsAvlLbl.Location = new System.Drawing.Point(632, 168);
            this.AllBodyPointsAvlLbl.Name = "AllBodyPointsAvlLbl";
            this.AllBodyPointsAvlLbl.Size = new System.Drawing.Size(13, 13);
            this.AllBodyPointsAvlLbl.TabIndex = 32;
            this.AllBodyPointsAvlLbl.Text = "0";
            // 
            // AllUrlPointsSelectedLbl
            // 
            this.AllUrlPointsSelectedLbl.AutoSize = true;
            this.AllUrlPointsSelectedLbl.Location = new System.Drawing.Point(780, 79);
            this.AllUrlPointsSelectedLbl.Name = "AllUrlPointsSelectedLbl";
            this.AllUrlPointsSelectedLbl.Size = new System.Drawing.Size(13, 13);
            this.AllUrlPointsSelectedLbl.TabIndex = 29;
            this.AllUrlPointsSelectedLbl.Text = "0";
            // 
            // AllUrlPointsAvlLbl
            // 
            this.AllUrlPointsAvlLbl.AutoSize = true;
            this.AllUrlPointsAvlLbl.Location = new System.Drawing.Point(632, 79);
            this.AllUrlPointsAvlLbl.Name = "AllUrlPointsAvlLbl";
            this.AllUrlPointsAvlLbl.Size = new System.Drawing.Size(13, 13);
            this.AllUrlPointsAvlLbl.TabIndex = 28;
            this.AllUrlPointsAvlLbl.Text = "0";
            // 
            // BodyInjectionMessageLbl
            // 
            this.BodyInjectionMessageLbl.AutoSize = true;
            this.BodyInjectionMessageLbl.ForeColor = System.Drawing.Color.DodgerBlue;
            this.BodyInjectionMessageLbl.Location = new System.Drawing.Point(37, 186);
            this.BodyInjectionMessageLbl.Name = "BodyInjectionMessageLbl";
            this.BodyInjectionMessageLbl.Size = new System.Drawing.Size(417, 13);
            this.BodyInjectionMessageLbl.TabIndex = 25;
            this.BodyInjectionMessageLbl.Text = "Body type has been discovered as XML and injection points were identified accordi" +
    "ngly";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.MistyRose;
            this.panel1.Controls.Add(this.BlacklistItemsCountLbl);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.UseBlackListCB);
            this.panel1.Location = new System.Drawing.Point(3, 349);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 42);
            this.panel1.TabIndex = 24;
            // 
            // BlacklistItemsCountLbl
            // 
            this.BlacklistItemsCountLbl.AutoSize = true;
            this.BlacklistItemsCountLbl.Location = new System.Drawing.Point(819, 13);
            this.BlacklistItemsCountLbl.Name = "BlacklistItemsCountLbl";
            this.BlacklistItemsCountLbl.Size = new System.Drawing.Size(13, 13);
            this.BlacklistItemsCountLbl.TabIndex = 38;
            this.BlacklistItemsCountLbl.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(677, 14);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(140, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "No. of items in the Blacklist: ";
            // 
            // UseBlackListCB
            // 
            this.UseBlackListCB.AutoSize = true;
            this.UseBlackListCB.Location = new System.Drawing.Point(20, 12);
            this.UseBlackListCB.Name = "UseBlackListCB";
            this.UseBlackListCB.Size = new System.Drawing.Size(379, 17);
            this.UseBlackListCB.TabIndex = 12;
            this.UseBlackListCB.Text = "Don\'t scan the parameters matching the names in the \'Parameters Blacklist\'";
            this.UseBlackListCB.UseVisualStyleBackColor = true;
            this.UseBlackListCB.Click += new System.EventHandler(this.UseBlackListCB_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(725, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "No. of Points selected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(578, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "No. of Points available";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(92, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Select Injection Points";
            // 
            // UrlPathPartInjectionMessageLbl
            // 
            this.UrlPathPartInjectionMessageLbl.AutoSize = true;
            this.UrlPathPartInjectionMessageLbl.ForeColor = System.Drawing.Color.DodgerBlue;
            this.UrlPathPartInjectionMessageLbl.Location = new System.Drawing.Point(37, 97);
            this.UrlPathPartInjectionMessageLbl.Name = "UrlPathPartInjectionMessageLbl";
            this.UrlPathPartInjectionMessageLbl.Size = new System.Drawing.Size(309, 13);
            this.UrlPathPartInjectionMessageLbl.TabIndex = 20;
            this.UrlPathPartInjectionMessageLbl.Text = "Url has Querystring so path parts would require explicit selection.";
            // 
            // ScanURLCB
            // 
            this.ScanURLCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanURLCB.AutoSize = true;
            this.ScanURLCB.Location = new System.Drawing.Point(22, 78);
            this.ScanURLCB.Name = "ScanURLCB";
            this.ScanURLCB.Size = new System.Drawing.Size(288, 17);
            this.ScanURLCB.TabIndex = 14;
            this.ScanURLCB.Text = "All URL path parts. Useful when site uses URL rewriting";
            this.ScanURLCB.UseVisualStyleBackColor = true;
            this.ScanURLCB.Click += new System.EventHandler(this.ScanURLCB_Click);
            // 
            // ScanBodyCB
            // 
            this.ScanBodyCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBodyCB.AutoSize = true;
            this.ScanBodyCB.Location = new System.Drawing.Point(22, 167);
            this.ScanBodyCB.Name = "ScanBodyCB";
            this.ScanBodyCB.Size = new System.Drawing.Size(120, 17);
            this.ScanBodyCB.TabIndex = 16;
            this.ScanBodyCB.Text = "All Body Parameters";
            this.ScanBodyCB.UseVisualStyleBackColor = true;
            this.ScanBodyCB.Click += new System.EventHandler(this.ScanBodyCB_Click);
            // 
            // URLTab
            // 
            this.URLTab.Controls.Add(this.ScanURLGrid);
            this.URLTab.Location = new System.Drawing.Point(4, 22);
            this.URLTab.Margin = new System.Windows.Forms.Padding(0);
            this.URLTab.Name = "URLTab";
            this.URLTab.Size = new System.Drawing.Size(863, 394);
            this.URLTab.TabIndex = 0;
            this.URLTab.Text = "URL Path Parts";
            this.URLTab.UseVisualStyleBackColor = true;
            // 
            // ScanURLGrid
            // 
            this.ScanURLGrid.AllowUserToAddRows = false;
            this.ScanURLGrid.AllowUserToDeleteRows = false;
            this.ScanURLGrid.AllowUserToResizeRows = false;
            this.ScanURLGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanURLGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanURLGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanURLGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestURLSelectColumn,
            this.ASRequestURLPositionColumn,
            this.ASRequestURLValueColumn});
            this.ScanURLGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanURLGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanURLGrid.GridColor = System.Drawing.Color.White;
            this.ScanURLGrid.Location = new System.Drawing.Point(0, 0);
            this.ScanURLGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanURLGrid.MultiSelect = false;
            this.ScanURLGrid.Name = "ScanURLGrid";
            this.ScanURLGrid.RowHeadersVisible = false;
            this.ScanURLGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanURLGrid.Size = new System.Drawing.Size(863, 394);
            this.ScanURLGrid.TabIndex = 0;
            this.ScanURLGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanURLGrid_CellClick);
            // 
            // ASRequestURLSelectColumn
            // 
            this.ASRequestURLSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestURLSelectColumn.HeaderText = "INJECT";
            this.ASRequestURLSelectColumn.Name = "ASRequestURLSelectColumn";
            this.ASRequestURLSelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestURLSelectColumn.Width = 55;
            // 
            // ASRequestURLPositionColumn
            // 
            this.ASRequestURLPositionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestURLPositionColumn.HeaderText = "PARAMETER POSITION";
            this.ASRequestURLPositionColumn.Name = "ASRequestURLPositionColumn";
            this.ASRequestURLPositionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestURLValueColumn
            // 
            this.ASRequestURLValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestURLValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestURLValueColumn.Name = "ASRequestURLValueColumn";
            this.ASRequestURLValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // QueryTab
            // 
            this.QueryTab.Controls.Add(this.ScanQueryGrid);
            this.QueryTab.Location = new System.Drawing.Point(4, 22);
            this.QueryTab.Margin = new System.Windows.Forms.Padding(0);
            this.QueryTab.Name = "QueryTab";
            this.QueryTab.Size = new System.Drawing.Size(863, 394);
            this.QueryTab.TabIndex = 1;
            this.QueryTab.Text = "Query";
            this.QueryTab.UseVisualStyleBackColor = true;
            // 
            // ScanQueryGrid
            // 
            this.ScanQueryGrid.AllowUserToAddRows = false;
            this.ScanQueryGrid.AllowUserToDeleteRows = false;
            this.ScanQueryGrid.AllowUserToResizeRows = false;
            this.ScanQueryGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanQueryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanQueryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanQueryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestQuerySelectColumn,
            this.ASRequestQueryNameColumn,
            this.ASRequestQueryValueColumn});
            this.ScanQueryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanQueryGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanQueryGrid.GridColor = System.Drawing.Color.White;
            this.ScanQueryGrid.Location = new System.Drawing.Point(0, 0);
            this.ScanQueryGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanQueryGrid.Name = "ScanQueryGrid";
            this.ScanQueryGrid.RowHeadersVisible = false;
            this.ScanQueryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanQueryGrid.Size = new System.Drawing.Size(863, 394);
            this.ScanQueryGrid.TabIndex = 1;
            this.ScanQueryGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanQueryGrid_CellClick);
            // 
            // ASRequestQuerySelectColumn
            // 
            this.ASRequestQuerySelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestQuerySelectColumn.HeaderText = "INJECT";
            this.ASRequestQuerySelectColumn.Name = "ASRequestQuerySelectColumn";
            this.ASRequestQuerySelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestQuerySelectColumn.Width = 55;
            // 
            // ASRequestQueryNameColumn
            // 
            this.ASRequestQueryNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestQueryNameColumn.HeaderText = "PARAMETER NAME";
            this.ASRequestQueryNameColumn.Name = "ASRequestQueryNameColumn";
            this.ASRequestQueryNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestQueryValueColumn
            // 
            this.ASRequestQueryValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestQueryValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestQueryValueColumn.Name = "ASRequestQueryValueColumn";
            this.ASRequestQueryValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BodyTab
            // 
            this.BodyTab.Controls.Add(this.BodyTypeCustomRB);
            this.BodyTab.Controls.Add(this.BodyTypeFormatPluginRB);
            this.BodyTab.Controls.Add(this.BodyTypeNormalRB);
            this.BodyTab.Controls.Add(this.label35);
            this.BodyTab.Controls.Add(this.BodyInjectTypeTabs);
            this.BodyTab.Location = new System.Drawing.Point(4, 22);
            this.BodyTab.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTab.Name = "BodyTab";
            this.BodyTab.Size = new System.Drawing.Size(863, 394);
            this.BodyTab.TabIndex = 2;
            this.BodyTab.Text = "Body";
            this.BodyTab.UseVisualStyleBackColor = true;
            // 
            // BodyTypeCustomRB
            // 
            this.BodyTypeCustomRB.AutoSize = true;
            this.BodyTypeCustomRB.Location = new System.Drawing.Point(400, 5);
            this.BodyTypeCustomRB.Name = "BodyTypeCustomRB";
            this.BodyTypeCustomRB.Size = new System.Drawing.Size(146, 17);
            this.BodyTypeCustomRB.TabIndex = 5;
            this.BodyTypeCustomRB.Text = "Custom/Unknown Format";
            this.BodyTypeCustomRB.UseVisualStyleBackColor = true;
            this.BodyTypeCustomRB.CheckedChanged += new System.EventHandler(this.BodyTypeCustomRB_CheckedChanged);
            // 
            // BodyTypeFormatPluginRB
            // 
            this.BodyTypeFormatPluginRB.AutoSize = true;
            this.BodyTypeFormatPluginRB.Location = new System.Drawing.Point(168, 5);
            this.BodyTypeFormatPluginRB.Name = "BodyTypeFormatPluginRB";
            this.BodyTypeFormatPluginRB.Size = new System.Drawing.Size(221, 17);
            this.BodyTypeFormatPluginRB.TabIndex = 4;
            this.BodyTypeFormatPluginRB.Text = "Other Know Format (Eg: XML,  JSON etc)";
            this.BodyTypeFormatPluginRB.UseVisualStyleBackColor = true;
            this.BodyTypeFormatPluginRB.CheckedChanged += new System.EventHandler(this.BodyTypeFormatPluginRB_CheckedChanged);
            // 
            // BodyTypeNormalRB
            // 
            this.BodyTypeNormalRB.AutoSize = true;
            this.BodyTypeNormalRB.Checked = true;
            this.BodyTypeNormalRB.Location = new System.Drawing.Point(101, 5);
            this.BodyTypeNormalRB.Name = "BodyTypeNormalRB";
            this.BodyTypeNormalRB.Size = new System.Drawing.Size(58, 17);
            this.BodyTypeNormalRB.TabIndex = 3;
            this.BodyTypeNormalRB.TabStop = true;
            this.BodyTypeNormalRB.Text = "Normal";
            this.BodyTypeNormalRB.UseVisualStyleBackColor = true;
            this.BodyTypeNormalRB.CheckedChanged += new System.EventHandler(this.BodyTypeNormalRB_CheckedChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(5, 7);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(94, 13);
            this.label35.TabIndex = 2;
            this.label35.Text = "Select Body Type:";
            // 
            // BodyInjectTypeTabs
            // 
            this.BodyInjectTypeTabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.BodyInjectTypeTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BodyInjectTypeTabs.Controls.Add(this.BodyTypeNormalTab);
            this.BodyInjectTypeTabs.Controls.Add(this.BodyTypeFormatPluginTab);
            this.BodyInjectTypeTabs.Controls.Add(this.BodyTypeCustomTab);
            this.BodyInjectTypeTabs.Location = new System.Drawing.Point(0, 24);
            this.BodyInjectTypeTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BodyInjectTypeTabs.Multiline = true;
            this.BodyInjectTypeTabs.Name = "BodyInjectTypeTabs";
            this.BodyInjectTypeTabs.Padding = new System.Drawing.Point(0, 0);
            this.BodyInjectTypeTabs.SelectedIndex = 0;
            this.BodyInjectTypeTabs.Size = new System.Drawing.Size(863, 370);
            this.BodyInjectTypeTabs.TabIndex = 1;
            this.BodyInjectTypeTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.BodyInjectTypeTabs_Selecting);
            // 
            // BodyTypeNormalTab
            // 
            this.BodyTypeNormalTab.Controls.Add(this.ScanBodyTypeNormalGrid);
            this.BodyTypeNormalTab.Location = new System.Drawing.Point(4, 4);
            this.BodyTypeNormalTab.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTypeNormalTab.Name = "BodyTypeNormalTab";
            this.BodyTypeNormalTab.Size = new System.Drawing.Size(855, 344);
            this.BodyTypeNormalTab.TabIndex = 0;
            this.BodyTypeNormalTab.Text = "Normal";
            this.BodyTypeNormalTab.UseVisualStyleBackColor = true;
            // 
            // ScanBodyTypeNormalGrid
            // 
            this.ScanBodyTypeNormalGrid.AllowUserToAddRows = false;
            this.ScanBodyTypeNormalGrid.AllowUserToDeleteRows = false;
            this.ScanBodyTypeNormalGrid.AllowUserToResizeRows = false;
            this.ScanBodyTypeNormalGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanBodyTypeNormalGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanBodyTypeNormalGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanBodyTypeNormalGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            this.ScanBodyTypeNormalGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanBodyTypeNormalGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanBodyTypeNormalGrid.GridColor = System.Drawing.Color.White;
            this.ScanBodyTypeNormalGrid.Location = new System.Drawing.Point(0, 0);
            this.ScanBodyTypeNormalGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanBodyTypeNormalGrid.Name = "ScanBodyTypeNormalGrid";
            this.ScanBodyTypeNormalGrid.RowHeadersVisible = false;
            this.ScanBodyTypeNormalGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanBodyTypeNormalGrid.Size = new System.Drawing.Size(855, 344);
            this.ScanBodyTypeNormalGrid.TabIndex = 2;
            this.ScanBodyTypeNormalGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanBodyTypeNormalGrid_CellClick);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewCheckBoxColumn1.HeaderText = "INJECT";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.Width = 55;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn15.HeaderText = "PARAMETER NAME";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn16.HeaderText = "PARAMETER VALUE";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BodyTypeFormatPluginTab
            // 
            this.BodyTypeFormatPluginTab.Controls.Add(this.ASRequestBodyTabSplit);
            this.BodyTypeFormatPluginTab.Location = new System.Drawing.Point(4, 4);
            this.BodyTypeFormatPluginTab.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTypeFormatPluginTab.Name = "BodyTypeFormatPluginTab";
            this.BodyTypeFormatPluginTab.Size = new System.Drawing.Size(855, 344);
            this.BodyTypeFormatPluginTab.TabIndex = 1;
            this.BodyTypeFormatPluginTab.Text = "Known Formats";
            this.BodyTypeFormatPluginTab.UseVisualStyleBackColor = true;
            // 
            // ASRequestBodyTabSplit
            // 
            this.ASRequestBodyTabSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestBodyTabSplit.Location = new System.Drawing.Point(0, 0);
            this.ASRequestBodyTabSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestBodyTabSplit.Name = "ASRequestBodyTabSplit";
            // 
            // ASRequestBodyTabSplit.Panel1
            // 
            this.ASRequestBodyTabSplit.Panel1.Controls.Add(this.FormatPluginsGrid);
            // 
            // ASRequestBodyTabSplit.Panel2
            // 
            this.ASRequestBodyTabSplit.Panel2.Controls.Add(this.ScanBodyFormatPluginTypeTabs);
            this.ASRequestBodyTabSplit.Size = new System.Drawing.Size(855, 344);
            this.ASRequestBodyTabSplit.SplitterDistance = 157;
            this.ASRequestBodyTabSplit.SplitterWidth = 2;
            this.ASRequestBodyTabSplit.TabIndex = 0;
            // 
            // FormatPluginsGrid
            // 
            this.FormatPluginsGrid.AllowUserToAddRows = false;
            this.FormatPluginsGrid.AllowUserToDeleteRows = false;
            this.FormatPluginsGrid.AllowUserToResizeRows = false;
            this.FormatPluginsGrid.BackgroundColor = System.Drawing.Color.White;
            this.FormatPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FormatPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormatPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestBodyDataFormatSelectColumn,
            this.ASRequestBodyDataFormatColumn});
            this.FormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.FormatPluginsGrid.GridColor = System.Drawing.Color.White;
            this.FormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.FormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.FormatPluginsGrid.MultiSelect = false;
            this.FormatPluginsGrid.Name = "FormatPluginsGrid";
            this.FormatPluginsGrid.RowHeadersVisible = false;
            this.FormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FormatPluginsGrid.Size = new System.Drawing.Size(157, 344);
            this.FormatPluginsGrid.TabIndex = 0;
            this.FormatPluginsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FormatPluginsGrid_CellClick);
            // 
            // ASRequestBodyDataFormatSelectColumn
            // 
            this.ASRequestBodyDataFormatSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestBodyDataFormatSelectColumn.HeaderText = "";
            this.ASRequestBodyDataFormatSelectColumn.MinimumWidth = 20;
            this.ASRequestBodyDataFormatSelectColumn.Name = "ASRequestBodyDataFormatSelectColumn";
            this.ASRequestBodyDataFormatSelectColumn.Width = 20;
            // 
            // ASRequestBodyDataFormatColumn
            // 
            this.ASRequestBodyDataFormatColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestBodyDataFormatColumn.HeaderText = "Select Format";
            this.ASRequestBodyDataFormatColumn.Name = "ASRequestBodyDataFormatColumn";
            this.ASRequestBodyDataFormatColumn.ReadOnly = true;
            this.ASRequestBodyDataFormatColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ScanBodyFormatPluginTypeTabs
            // 
            this.ScanBodyFormatPluginTypeTabs.Controls.Add(this.BodyTypeFormatPluginInjectionArrayGridTab);
            this.ScanBodyFormatPluginTypeTabs.Controls.Add(this.BodyTypeFormatPluginXMLTab);
            this.ScanBodyFormatPluginTypeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanBodyFormatPluginTypeTabs.Location = new System.Drawing.Point(0, 0);
            this.ScanBodyFormatPluginTypeTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ScanBodyFormatPluginTypeTabs.Multiline = true;
            this.ScanBodyFormatPluginTypeTabs.Name = "ScanBodyFormatPluginTypeTabs";
            this.ScanBodyFormatPluginTypeTabs.Padding = new System.Drawing.Point(0, 0);
            this.ScanBodyFormatPluginTypeTabs.SelectedIndex = 0;
            this.ScanBodyFormatPluginTypeTabs.Size = new System.Drawing.Size(696, 344);
            this.ScanBodyFormatPluginTypeTabs.TabIndex = 0;
            // 
            // BodyTypeFormatPluginInjectionArrayGridTab
            // 
            this.BodyTypeFormatPluginInjectionArrayGridTab.Controls.Add(this.BodyTypeFormatPluginGrid);
            this.BodyTypeFormatPluginInjectionArrayGridTab.Location = new System.Drawing.Point(4, 22);
            this.BodyTypeFormatPluginInjectionArrayGridTab.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTypeFormatPluginInjectionArrayGridTab.Name = "BodyTypeFormatPluginInjectionArrayGridTab";
            this.BodyTypeFormatPluginInjectionArrayGridTab.Size = new System.Drawing.Size(688, 318);
            this.BodyTypeFormatPluginInjectionArrayGridTab.TabIndex = 0;
            this.BodyTypeFormatPluginInjectionArrayGridTab.Text = "Format Specific Injection Points";
            this.BodyTypeFormatPluginInjectionArrayGridTab.UseVisualStyleBackColor = true;
            // 
            // BodyTypeFormatPluginGrid
            // 
            this.BodyTypeFormatPluginGrid.AllowUserToAddRows = false;
            this.BodyTypeFormatPluginGrid.AllowUserToDeleteRows = false;
            this.BodyTypeFormatPluginGrid.AllowUserToResizeRows = false;
            this.BodyTypeFormatPluginGrid.BackgroundColor = System.Drawing.Color.White;
            this.BodyTypeFormatPluginGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BodyTypeFormatPluginGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BodyTypeFormatPluginGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestBodySelectColumn,
            this.ASRequestBodyNameColumn,
            this.ASRequestBodyValueColumn});
            this.BodyTypeFormatPluginGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BodyTypeFormatPluginGrid.GridColor = System.Drawing.Color.White;
            this.BodyTypeFormatPluginGrid.Location = new System.Drawing.Point(0, 0);
            this.BodyTypeFormatPluginGrid.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTypeFormatPluginGrid.Name = "BodyTypeFormatPluginGrid";
            this.BodyTypeFormatPluginGrid.ReadOnly = true;
            this.BodyTypeFormatPluginGrid.RowHeadersVisible = false;
            this.BodyTypeFormatPluginGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BodyTypeFormatPluginGrid.Size = new System.Drawing.Size(688, 318);
            this.BodyTypeFormatPluginGrid.TabIndex = 2;
            this.BodyTypeFormatPluginGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BodyTypeFormatPluginGrid_CellClick);
            // 
            // ASRequestBodySelectColumn
            // 
            this.ASRequestBodySelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestBodySelectColumn.HeaderText = "INJECT";
            this.ASRequestBodySelectColumn.Name = "ASRequestBodySelectColumn";
            this.ASRequestBodySelectColumn.ReadOnly = true;
            this.ASRequestBodySelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestBodySelectColumn.Width = 55;
            // 
            // ASRequestBodyNameColumn
            // 
            this.ASRequestBodyNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestBodyNameColumn.HeaderText = "INJECTION POINT NAME";
            this.ASRequestBodyNameColumn.Name = "ASRequestBodyNameColumn";
            this.ASRequestBodyNameColumn.ReadOnly = true;
            this.ASRequestBodyNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestBodyValueColumn
            // 
            this.ASRequestBodyValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestBodyValueColumn.HeaderText = "VALUE TO INJECT";
            this.ASRequestBodyValueColumn.Name = "ASRequestBodyValueColumn";
            this.ASRequestBodyValueColumn.ReadOnly = true;
            this.ASRequestBodyValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BodyTypeFormatPluginXMLTab
            // 
            this.BodyTypeFormatPluginXMLTab.Controls.Add(this.FormatXMLTB);
            this.BodyTypeFormatPluginXMLTab.Location = new System.Drawing.Point(4, 22);
            this.BodyTypeFormatPluginXMLTab.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTypeFormatPluginXMLTab.Name = "BodyTypeFormatPluginXMLTab";
            this.BodyTypeFormatPluginXMLTab.Size = new System.Drawing.Size(688, 318);
            this.BodyTypeFormatPluginXMLTab.TabIndex = 1;
            this.BodyTypeFormatPluginXMLTab.Text = "Normalized XML from Body Data (for reference)";
            this.BodyTypeFormatPluginXMLTab.UseVisualStyleBackColor = true;
            // 
            // FormatXMLTB
            // 
            this.FormatXMLTB.BackColor = System.Drawing.SystemColors.Window;
            this.FormatXMLTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FormatXMLTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormatXMLTB.Location = new System.Drawing.Point(0, 0);
            this.FormatXMLTB.Margin = new System.Windows.Forms.Padding(0);
            this.FormatXMLTB.MaxLength = 2147483647;
            this.FormatXMLTB.Multiline = true;
            this.FormatXMLTB.Name = "FormatXMLTB";
            this.FormatXMLTB.ReadOnly = true;
            this.FormatXMLTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FormatXMLTB.Size = new System.Drawing.Size(688, 318);
            this.FormatXMLTB.TabIndex = 0;
            // 
            // BodyTypeCustomTab
            // 
            this.BodyTypeCustomTab.Controls.Add(this.PlaceInjectionMarkerLL);
            this.BodyTypeCustomTab.Controls.Add(this.label10);
            this.BodyTypeCustomTab.Controls.Add(this.label7);
            this.BodyTypeCustomTab.Controls.Add(this.label39);
            this.BodyTypeCustomTab.Controls.Add(this.CustomInjectionPointsHighlightLbl);
            this.BodyTypeCustomTab.Controls.Add(this.label37);
            this.BodyTypeCustomTab.Controls.Add(this.CustomEndMarkerTB);
            this.BodyTypeCustomTab.Controls.Add(this.textBox3);
            this.BodyTypeCustomTab.Controls.Add(this.ASApplyCustomMarkersLL);
            this.BodyTypeCustomTab.Controls.Add(this.CustomInjectionMarkerTabs);
            this.BodyTypeCustomTab.Controls.Add(this.CustomStartMarkerTB);
            this.BodyTypeCustomTab.Controls.Add(this.label38);
            this.BodyTypeCustomTab.Location = new System.Drawing.Point(4, 4);
            this.BodyTypeCustomTab.Name = "BodyTypeCustomTab";
            this.BodyTypeCustomTab.Size = new System.Drawing.Size(855, 344);
            this.BodyTypeCustomTab.TabIndex = 2;
            this.BodyTypeCustomTab.Text = "Custom";
            this.BodyTypeCustomTab.UseVisualStyleBackColor = true;
            // 
            // PlaceInjectionMarkerLL
            // 
            this.PlaceInjectionMarkerLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PlaceInjectionMarkerLL.AutoSize = true;
            this.PlaceInjectionMarkerLL.Location = new System.Drawing.Point(322, 90);
            this.PlaceInjectionMarkerLL.Name = "PlaceInjectionMarkerLL";
            this.PlaceInjectionMarkerLL.Size = new System.Drawing.Size(194, 13);
            this.PlaceInjectionMarkerLL.TabIndex = 10;
            this.PlaceInjectionMarkerLL.TabStop = true;
            this.PlaceInjectionMarkerLL.Text = "Place injection markers in selected area";
            this.PlaceInjectionMarkerLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PlaceInjectionMarkerLL_LinkClicked);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(313, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "To place markers select any section of the text below and click - ";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(651, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Number of Injection Points Detected:";
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(332, 67);
            this.label39.Margin = new System.Windows.Forms.Padding(0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(65, 13);
            this.label39.TabIndex = 6;
            this.label39.Text = "End Marker:";
            // 
            // CustomInjectionPointsHighlightLbl
            // 
            this.CustomInjectionPointsHighlightLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CustomInjectionPointsHighlightLbl.AutoSize = true;
            this.CustomInjectionPointsHighlightLbl.ForeColor = System.Drawing.Color.Blue;
            this.CustomInjectionPointsHighlightLbl.Location = new System.Drawing.Point(837, 67);
            this.CustomInjectionPointsHighlightLbl.Name = "CustomInjectionPointsHighlightLbl";
            this.CustomInjectionPointsHighlightLbl.Size = new System.Drawing.Size(13, 13);
            this.CustomInjectionPointsHighlightLbl.TabIndex = 1;
            this.CustomInjectionPointsHighlightLbl.Text = "0";
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(5, 67);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(137, 13);
            this.label37.TabIndex = 3;
            this.label37.Text = "Set Injection Point Markers:";
            // 
            // CustomEndMarkerTB
            // 
            this.CustomEndMarkerTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CustomEndMarkerTB.Location = new System.Drawing.Point(401, 65);
            this.CustomEndMarkerTB.Name = "CustomEndMarkerTB";
            this.CustomEndMarkerTB.Size = new System.Drawing.Size(100, 20);
            this.CustomEndMarkerTB.TabIndex = 7;
            this.CustomEndMarkerTB.Text = "<<--->>";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Margin = new System.Windows.Forms.Padding(0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(855, 58);
            this.textBox3.TabIndex = 8;
            this.textBox3.TabStop = false;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // ASApplyCustomMarkersLL
            // 
            this.ASApplyCustomMarkersLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ASApplyCustomMarkersLL.AutoSize = true;
            this.ASApplyCustomMarkersLL.Location = new System.Drawing.Point(507, 68);
            this.ASApplyCustomMarkersLL.Name = "ASApplyCustomMarkersLL";
            this.ASApplyCustomMarkersLL.Size = new System.Drawing.Size(33, 13);
            this.ASApplyCustomMarkersLL.TabIndex = 2;
            this.ASApplyCustomMarkersLL.TabStop = true;
            this.ASApplyCustomMarkersLL.Text = "Apply";
            this.ASApplyCustomMarkersLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ASApplyCustomMarkersLL_LinkClicked);
            // 
            // CustomInjectionMarkerTabs
            // 
            this.CustomInjectionMarkerTabs.Controls.Add(this.CustomMarkerSelectionTab);
            this.CustomInjectionMarkerTabs.Controls.Add(this.CustomMarkerDisplayTab);
            this.CustomInjectionMarkerTabs.Controls.Add(this.CustomMarkerEscapingTab);
            this.CustomInjectionMarkerTabs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CustomInjectionMarkerTabs.Location = new System.Drawing.Point(0, 108);
            this.CustomInjectionMarkerTabs.Margin = new System.Windows.Forms.Padding(0);
            this.CustomInjectionMarkerTabs.Name = "CustomInjectionMarkerTabs";
            this.CustomInjectionMarkerTabs.Padding = new System.Drawing.Point(0, 0);
            this.CustomInjectionMarkerTabs.SelectedIndex = 0;
            this.CustomInjectionMarkerTabs.Size = new System.Drawing.Size(855, 236);
            this.CustomInjectionMarkerTabs.TabIndex = 1;
            // 
            // CustomMarkerSelectionTab
            // 
            this.CustomMarkerSelectionTab.Controls.Add(this.SetCustomInjectionPointsSTB);
            this.CustomMarkerSelectionTab.Location = new System.Drawing.Point(4, 22);
            this.CustomMarkerSelectionTab.Margin = new System.Windows.Forms.Padding(0);
            this.CustomMarkerSelectionTab.Name = "CustomMarkerSelectionTab";
            this.CustomMarkerSelectionTab.Size = new System.Drawing.Size(847, 210);
            this.CustomMarkerSelectionTab.TabIndex = 0;
            this.CustomMarkerSelectionTab.Text = "Set Custom Injection Point Markers:";
            this.CustomMarkerSelectionTab.UseVisualStyleBackColor = true;
            // 
            // SetCustomInjectionPointsSTB
            // 
            this.SetCustomInjectionPointsSTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetCustomInjectionPointsSTB.Location = new System.Drawing.Point(0, 0);
            this.SetCustomInjectionPointsSTB.Margin = new System.Windows.Forms.Padding(0);
            this.SetCustomInjectionPointsSTB.Name = "SetCustomInjectionPointsSTB";
            this.SetCustomInjectionPointsSTB.ReadOnly = false;
            this.SetCustomInjectionPointsSTB.Size = new System.Drawing.Size(847, 210);
            this.SetCustomInjectionPointsSTB.TabIndex = 0;
            // 
            // CustomMarkerDisplayTab
            // 
            this.CustomMarkerDisplayTab.Controls.Add(this.HighlightCustomInjectionPointsRTB);
            this.CustomMarkerDisplayTab.Location = new System.Drawing.Point(4, 22);
            this.CustomMarkerDisplayTab.Margin = new System.Windows.Forms.Padding(0);
            this.CustomMarkerDisplayTab.Name = "CustomMarkerDisplayTab";
            this.CustomMarkerDisplayTab.Size = new System.Drawing.Size(847, 210);
            this.CustomMarkerDisplayTab.TabIndex = 1;
            this.CustomMarkerDisplayTab.Text = "View Sections of Body Selected for Injection:";
            this.CustomMarkerDisplayTab.UseVisualStyleBackColor = true;
            // 
            // HighlightCustomInjectionPointsRTB
            // 
            this.HighlightCustomInjectionPointsRTB.BackColor = System.Drawing.Color.White;
            this.HighlightCustomInjectionPointsRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HighlightCustomInjectionPointsRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HighlightCustomInjectionPointsRTB.Location = new System.Drawing.Point(0, 0);
            this.HighlightCustomInjectionPointsRTB.Margin = new System.Windows.Forms.Padding(0);
            this.HighlightCustomInjectionPointsRTB.Name = "HighlightCustomInjectionPointsRTB";
            this.HighlightCustomInjectionPointsRTB.ReadOnly = true;
            this.HighlightCustomInjectionPointsRTB.Size = new System.Drawing.Size(847, 210);
            this.HighlightCustomInjectionPointsRTB.TabIndex = 0;
            this.HighlightCustomInjectionPointsRTB.Text = "";
            // 
            // CustomMarkerEscapingTab
            // 
            this.CustomMarkerEscapingTab.Controls.Add(this.CharacterEscapingStatusTB);
            this.CustomMarkerEscapingTab.Controls.Add(this.label9);
            this.CustomMarkerEscapingTab.Controls.Add(this.label8);
            this.CustomMarkerEscapingTab.Controls.Add(this.label6);
            this.CustomMarkerEscapingTab.Controls.Add(this.label1);
            this.CustomMarkerEscapingTab.Controls.Add(this.textBox12);
            this.CustomMarkerEscapingTab.Controls.Add(this.EncodedCharacterTB);
            this.CustomMarkerEscapingTab.Controls.Add(this.RawCharacterTB);
            this.CustomMarkerEscapingTab.Controls.Add(this.AddToEscapeRuleBtn);
            this.CustomMarkerEscapingTab.Controls.Add(this.CharacterEscapingGrid);
            this.CustomMarkerEscapingTab.Location = new System.Drawing.Point(4, 22);
            this.CustomMarkerEscapingTab.Margin = new System.Windows.Forms.Padding(0);
            this.CustomMarkerEscapingTab.Name = "CustomMarkerEscapingTab";
            this.CustomMarkerEscapingTab.Size = new System.Drawing.Size(847, 210);
            this.CustomMarkerEscapingTab.TabIndex = 2;
            this.CustomMarkerEscapingTab.Text = "Set Character Escaping";
            this.CustomMarkerEscapingTab.UseVisualStyleBackColor = true;
            // 
            // CharacterEscapingStatusTB
            // 
            this.CharacterEscapingStatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CharacterEscapingStatusTB.BackColor = System.Drawing.Color.Red;
            this.CharacterEscapingStatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CharacterEscapingStatusTB.ForeColor = System.Drawing.Color.Black;
            this.CharacterEscapingStatusTB.Location = new System.Drawing.Point(11, 181);
            this.CharacterEscapingStatusTB.Multiline = true;
            this.CharacterEscapingStatusTB.Name = "CharacterEscapingStatusTB";
            this.CharacterEscapingStatusTB.Size = new System.Drawing.Size(538, 17);
            this.CharacterEscapingStatusTB.TabIndex = 15;
            this.CharacterEscapingStatusTB.TabStop = false;
            this.CharacterEscapingStatusTB.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(523, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "You can delete or edit a rule by doing a right-click on the rule and selecting th" +
    "e required option from the menu.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Add a new character escaping rule:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(191, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Encoded Character:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Raw Character:";
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.Color.LightSkyBlue;
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox12.Location = new System.Drawing.Point(0, 0);
            this.textBox12.Margin = new System.Windows.Forms.Padding(0);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(577, 80);
            this.textBox12.TabIndex = 9;
            this.textBox12.TabStop = false;
            this.textBox12.Text = resources.GetString("textBox12.Text");
            // 
            // EncodedCharacterTB
            // 
            this.EncodedCharacterTB.Location = new System.Drawing.Point(299, 117);
            this.EncodedCharacterTB.Name = "EncodedCharacterTB";
            this.EncodedCharacterTB.Size = new System.Drawing.Size(100, 20);
            this.EncodedCharacterTB.TabIndex = 4;
            // 
            // RawCharacterTB
            // 
            this.RawCharacterTB.Location = new System.Drawing.Point(99, 117);
            this.RawCharacterTB.Name = "RawCharacterTB";
            this.RawCharacterTB.Size = new System.Drawing.Size(76, 20);
            this.RawCharacterTB.TabIndex = 3;
            // 
            // AddToEscapeRuleBtn
            // 
            this.AddToEscapeRuleBtn.Location = new System.Drawing.Point(418, 114);
            this.AddToEscapeRuleBtn.Name = "AddToEscapeRuleBtn";
            this.AddToEscapeRuleBtn.Size = new System.Drawing.Size(74, 23);
            this.AddToEscapeRuleBtn.TabIndex = 2;
            this.AddToEscapeRuleBtn.Text = "Add to list";
            this.AddToEscapeRuleBtn.UseVisualStyleBackColor = true;
            this.AddToEscapeRuleBtn.Click += new System.EventHandler(this.AddToEscapeRuleBtn_Click);
            // 
            // CharacterEscapingGrid
            // 
            this.CharacterEscapingGrid.AllowUserToAddRows = false;
            this.CharacterEscapingGrid.AllowUserToDeleteRows = false;
            this.CharacterEscapingGrid.AllowUserToResizeRows = false;
            this.CharacterEscapingGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CharacterEscapingGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CharacterEscapingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CharacterEscapingGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EscapingSelectColumn,
            this.RawCharacterColumn,
            this.ArrowColumn,
            this.EncodedCharacterColumn});
            this.CharacterEscapingGrid.ContextMenuStrip = this.CharacterEscapingMenu;
            this.CharacterEscapingGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.CharacterEscapingGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.CharacterEscapingGrid.GridColor = System.Drawing.Color.White;
            this.CharacterEscapingGrid.Location = new System.Drawing.Point(577, 0);
            this.CharacterEscapingGrid.Margin = new System.Windows.Forms.Padding(0);
            this.CharacterEscapingGrid.MultiSelect = false;
            this.CharacterEscapingGrid.Name = "CharacterEscapingGrid";
            this.CharacterEscapingGrid.RowHeadersVisible = false;
            this.CharacterEscapingGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CharacterEscapingGrid.Size = new System.Drawing.Size(270, 210);
            this.CharacterEscapingGrid.TabIndex = 1;
            this.CharacterEscapingGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CharacterEscapingGrid_CellClick);
            // 
            // EscapingSelectColumn
            // 
            this.EscapingSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EscapingSelectColumn.HeaderText = "Use Rule";
            this.EscapingSelectColumn.MinimumWidth = 40;
            this.EscapingSelectColumn.Name = "EscapingSelectColumn";
            this.EscapingSelectColumn.Width = 40;
            // 
            // RawCharacterColumn
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RawCharacterColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.RawCharacterColumn.FillWeight = 70F;
            this.RawCharacterColumn.HeaderText = "Raw Character";
            this.RawCharacterColumn.Name = "RawCharacterColumn";
            this.RawCharacterColumn.ReadOnly = true;
            this.RawCharacterColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RawCharacterColumn.Width = 70;
            // 
            // ArrowColumn
            // 
            this.ArrowColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ArrowColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.ArrowColumn.HeaderText = "";
            this.ArrowColumn.MinimumWidth = 30;
            this.ArrowColumn.Name = "ArrowColumn";
            this.ArrowColumn.Width = 30;
            // 
            // EncodedCharacterColumn
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EncodedCharacterColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.EncodedCharacterColumn.FillWeight = 130F;
            this.EncodedCharacterColumn.HeaderText = "Encoded Character";
            this.EncodedCharacterColumn.Name = "EncodedCharacterColumn";
            this.EncodedCharacterColumn.Width = 130;
            // 
            // CharacterEscapingMenu
            // 
            this.CharacterEscapingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditRuleToolStripMenuItem,
            this.DeleteRuleToolStripMenuItem});
            this.CharacterEscapingMenu.Name = "CharacterEscapingMenu";
            this.CharacterEscapingMenu.Size = new System.Drawing.Size(134, 48);
            this.CharacterEscapingMenu.Opening += new System.ComponentModel.CancelEventHandler(this.CharacterEscapingMenu_Opening);
            // 
            // EditRuleToolStripMenuItem
            // 
            this.EditRuleToolStripMenuItem.Name = "EditRuleToolStripMenuItem";
            this.EditRuleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.EditRuleToolStripMenuItem.Text = "Edit Rule";
            this.EditRuleToolStripMenuItem.Click += new System.EventHandler(this.EditRuleToolStripMenuItem_Click);
            // 
            // DeleteRuleToolStripMenuItem
            // 
            this.DeleteRuleToolStripMenuItem.Name = "DeleteRuleToolStripMenuItem";
            this.DeleteRuleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.DeleteRuleToolStripMenuItem.Text = "Delete Rule";
            this.DeleteRuleToolStripMenuItem.Click += new System.EventHandler(this.DeleteRuleToolStripMenuItem_Click);
            // 
            // CustomStartMarkerTB
            // 
            this.CustomStartMarkerTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CustomStartMarkerTB.Location = new System.Drawing.Point(217, 65);
            this.CustomStartMarkerTB.Name = "CustomStartMarkerTB";
            this.CustomStartMarkerTB.Size = new System.Drawing.Size(100, 20);
            this.CustomStartMarkerTB.TabIndex = 5;
            this.CustomStartMarkerTB.Text = "<<+++>>";
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(146, 68);
            this.label38.Margin = new System.Windows.Forms.Padding(0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(68, 13);
            this.label38.TabIndex = 4;
            this.label38.Text = "Start Marker:";
            // 
            // CookieTab
            // 
            this.CookieTab.Controls.Add(this.ScanCookieGrid);
            this.CookieTab.Location = new System.Drawing.Point(4, 22);
            this.CookieTab.Margin = new System.Windows.Forms.Padding(0);
            this.CookieTab.Name = "CookieTab";
            this.CookieTab.Size = new System.Drawing.Size(863, 394);
            this.CookieTab.TabIndex = 3;
            this.CookieTab.Text = "Cookie";
            this.CookieTab.UseVisualStyleBackColor = true;
            // 
            // ScanCookieGrid
            // 
            this.ScanCookieGrid.AllowUserToAddRows = false;
            this.ScanCookieGrid.AllowUserToDeleteRows = false;
            this.ScanCookieGrid.AllowUserToResizeRows = false;
            this.ScanCookieGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanCookieGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanCookieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanCookieGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestCookieSelectColumn,
            this.ASRequestCookieNameColumn,
            this.ASRequestCookieValueColumn});
            this.ScanCookieGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanCookieGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanCookieGrid.GridColor = System.Drawing.Color.White;
            this.ScanCookieGrid.Location = new System.Drawing.Point(0, 0);
            this.ScanCookieGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanCookieGrid.Name = "ScanCookieGrid";
            this.ScanCookieGrid.RowHeadersVisible = false;
            this.ScanCookieGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanCookieGrid.Size = new System.Drawing.Size(863, 394);
            this.ScanCookieGrid.TabIndex = 2;
            this.ScanCookieGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanCookieGrid_CellClick);
            // 
            // ASRequestCookieSelectColumn
            // 
            this.ASRequestCookieSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestCookieSelectColumn.HeaderText = "INJECT";
            this.ASRequestCookieSelectColumn.Name = "ASRequestCookieSelectColumn";
            this.ASRequestCookieSelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestCookieSelectColumn.Width = 55;
            // 
            // ASRequestCookieNameColumn
            // 
            this.ASRequestCookieNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestCookieNameColumn.HeaderText = "PARAMETER NAME";
            this.ASRequestCookieNameColumn.Name = "ASRequestCookieNameColumn";
            this.ASRequestCookieNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestCookieValueColumn
            // 
            this.ASRequestCookieValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestCookieValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestCookieValueColumn.Name = "ASRequestCookieValueColumn";
            this.ASRequestCookieValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // HeadersTab
            // 
            this.HeadersTab.Controls.Add(this.ScanHeadersGrid);
            this.HeadersTab.Location = new System.Drawing.Point(4, 22);
            this.HeadersTab.Margin = new System.Windows.Forms.Padding(0);
            this.HeadersTab.Name = "HeadersTab";
            this.HeadersTab.Size = new System.Drawing.Size(863, 394);
            this.HeadersTab.TabIndex = 4;
            this.HeadersTab.Text = "Headers";
            this.HeadersTab.UseVisualStyleBackColor = true;
            // 
            // ScanHeadersGrid
            // 
            this.ScanHeadersGrid.AllowUserToAddRows = false;
            this.ScanHeadersGrid.AllowUserToDeleteRows = false;
            this.ScanHeadersGrid.AllowUserToResizeRows = false;
            this.ScanHeadersGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanHeadersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanHeadersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanHeadersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestHeadersSelectColumn,
            this.ASRequestHeadersNameColumn,
            this.ASRequestHeadersValueColumn});
            this.ScanHeadersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanHeadersGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanHeadersGrid.GridColor = System.Drawing.Color.White;
            this.ScanHeadersGrid.Location = new System.Drawing.Point(0, 0);
            this.ScanHeadersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanHeadersGrid.Name = "ScanHeadersGrid";
            this.ScanHeadersGrid.RowHeadersVisible = false;
            this.ScanHeadersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanHeadersGrid.Size = new System.Drawing.Size(863, 394);
            this.ScanHeadersGrid.TabIndex = 3;
            this.ScanHeadersGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanHeadersGrid_CellClick);
            // 
            // ASRequestHeadersSelectColumn
            // 
            this.ASRequestHeadersSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestHeadersSelectColumn.HeaderText = "INJECT";
            this.ASRequestHeadersSelectColumn.Name = "ASRequestHeadersSelectColumn";
            this.ASRequestHeadersSelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestHeadersSelectColumn.Width = 55;
            // 
            // ASRequestHeadersNameColumn
            // 
            this.ASRequestHeadersNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestHeadersNameColumn.HeaderText = "PARAMETER NAME";
            this.ASRequestHeadersNameColumn.Name = "ASRequestHeadersNameColumn";
            this.ASRequestHeadersNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestHeadersValueColumn
            // 
            this.ASRequestHeadersValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestHeadersValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestHeadersValueColumn.Name = "ASRequestHeadersValueColumn";
            this.ASRequestHeadersValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ParameterNamesTab
            // 
            this.ParameterNamesTab.Controls.Add(this.textBox1);
            this.ParameterNamesTab.Controls.Add(this.ScanHeadersParameterNameCB);
            this.ParameterNamesTab.Controls.Add(this.ScanCookieParameterNameCB);
            this.ParameterNamesTab.Controls.Add(this.ScanBodyParameterNameCB);
            this.ParameterNamesTab.Controls.Add(this.ScanQueryParameterNameCB);
            this.ParameterNamesTab.Location = new System.Drawing.Point(4, 22);
            this.ParameterNamesTab.Margin = new System.Windows.Forms.Padding(0);
            this.ParameterNamesTab.Name = "ParameterNamesTab";
            this.ParameterNamesTab.Size = new System.Drawing.Size(863, 394);
            this.ParameterNamesTab.TabIndex = 5;
            this.ParameterNamesTab.Text = "Names";
            this.ParameterNamesTab.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(7, 7);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(849, 68);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // ScanHeadersParameterNameCB
            // 
            this.ScanHeadersParameterNameCB.AutoSize = true;
            this.ScanHeadersParameterNameCB.Location = new System.Drawing.Point(20, 156);
            this.ScanHeadersParameterNameCB.Name = "ScanHeadersParameterNameCB";
            this.ScanHeadersParameterNameCB.Size = new System.Drawing.Size(148, 17);
            this.ScanHeadersParameterNameCB.TabIndex = 4;
            this.ScanHeadersParameterNameCB.Text = "Headers Parameter Name";
            this.ScanHeadersParameterNameCB.UseVisualStyleBackColor = true;
            this.ScanHeadersParameterNameCB.Click += new System.EventHandler(this.ScanHeadersParameterNameCB_Click);
            // 
            // ScanCookieParameterNameCB
            // 
            this.ScanCookieParameterNameCB.AutoSize = true;
            this.ScanCookieParameterNameCB.Location = new System.Drawing.Point(20, 133);
            this.ScanCookieParameterNameCB.Name = "ScanCookieParameterNameCB";
            this.ScanCookieParameterNameCB.Size = new System.Drawing.Size(141, 17);
            this.ScanCookieParameterNameCB.TabIndex = 3;
            this.ScanCookieParameterNameCB.Text = "Cookie Parameter Name";
            this.ScanCookieParameterNameCB.UseVisualStyleBackColor = true;
            this.ScanCookieParameterNameCB.Click += new System.EventHandler(this.ScanCookieParameterNameCB_Click);
            // 
            // ScanBodyParameterNameCB
            // 
            this.ScanBodyParameterNameCB.AutoSize = true;
            this.ScanBodyParameterNameCB.Location = new System.Drawing.Point(20, 110);
            this.ScanBodyParameterNameCB.Name = "ScanBodyParameterNameCB";
            this.ScanBodyParameterNameCB.Size = new System.Drawing.Size(132, 17);
            this.ScanBodyParameterNameCB.TabIndex = 2;
            this.ScanBodyParameterNameCB.Text = "Body Parameter Name";
            this.ScanBodyParameterNameCB.UseVisualStyleBackColor = true;
            this.ScanBodyParameterNameCB.Click += new System.EventHandler(this.ScanBodyParameterNameCB_Click);
            // 
            // ScanQueryParameterNameCB
            // 
            this.ScanQueryParameterNameCB.AutoSize = true;
            this.ScanQueryParameterNameCB.Location = new System.Drawing.Point(20, 87);
            this.ScanQueryParameterNameCB.Name = "ScanQueryParameterNameCB";
            this.ScanQueryParameterNameCB.Size = new System.Drawing.Size(136, 17);
            this.ScanQueryParameterNameCB.TabIndex = 1;
            this.ScanQueryParameterNameCB.Text = "Query Parameter Name";
            this.ScanQueryParameterNameCB.UseVisualStyleBackColor = true;
            this.ScanQueryParameterNameCB.Click += new System.EventHandler(this.ScanQueryParameterNameCB_Click);
            // 
            // BlackListTab
            // 
            this.BlackListTab.Controls.Add(this.textBox8);
            this.BlackListTab.Controls.Add(this.textBox7);
            this.BlackListTab.Controls.Add(this.ParametersBlacklistTB);
            this.BlackListTab.Location = new System.Drawing.Point(4, 22);
            this.BlackListTab.Name = "BlackListTab";
            this.BlackListTab.Size = new System.Drawing.Size(863, 394);
            this.BlackListTab.TabIndex = 7;
            this.BlackListTab.Text = "Parameters Black-list";
            this.BlackListTab.UseVisualStyleBackColor = true;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.White;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.ForeColor = System.Drawing.Color.Gray;
            this.textBox8.Location = new System.Drawing.Point(290, 122);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(271, 57);
            this.textBox8.TabIndex = 13;
            this.textBox8.Text = "Add one parameter name per line. Blank space at the start and end of parameter na" +
    "mes are stripped away.\r\n";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.White;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(863, 116);
            this.textBox7.TabIndex = 12;
            this.textBox7.Text = resources.GetString("textBox7.Text");
            // 
            // ParametersBlacklistTB
            // 
            this.ParametersBlacklistTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ParametersBlacklistTB.Location = new System.Drawing.Point(9, 122);
            this.ParametersBlacklistTB.Multiline = true;
            this.ParametersBlacklistTB.Name = "ParametersBlacklistTB";
            this.ParametersBlacklistTB.Size = new System.Drawing.Size(275, 265);
            this.ParametersBlacklistTB.TabIndex = 0;
            this.ParametersBlacklistTB.TextChanged += new System.EventHandler(this.ParametersBlacklistTB_TextChanged);
            // 
            // CustomizeTab
            // 
            this.CustomizeTab.Controls.Add(this.SessionPluginsCombo);
            this.CustomizeTab.Controls.Add(this.ScanThreadLimitCB);
            this.CustomizeTab.Controls.Add(this.LaunchSessionPluginCreationAssistantLL);
            this.CustomizeTab.Controls.Add(this.RefreshSessListLL);
            this.CustomizeTab.Controls.Add(this.textBox2);
            this.CustomizeTab.Controls.Add(this.label11);
            this.CustomizeTab.Controls.Add(this.Step3StatusTB);
            this.CustomizeTab.Controls.Add(this.StepFourPreviousBtn);
            this.CustomizeTab.Controls.Add(this.FinalBtn);
            this.CustomizeTab.Location = new System.Drawing.Point(4, 25);
            this.CustomizeTab.Name = "CustomizeTab";
            this.CustomizeTab.Padding = new System.Windows.Forms.Padding(5);
            this.CustomizeTab.Size = new System.Drawing.Size(876, 532);
            this.CustomizeTab.TabIndex = 3;
            this.CustomizeTab.Text = "               Customize Scan               ";
            this.CustomizeTab.UseVisualStyleBackColor = true;
            // 
            // SessionPluginsCombo
            // 
            this.SessionPluginsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionPluginsCombo.FormattingEnabled = true;
            this.SessionPluginsCombo.Location = new System.Drawing.Point(386, 120);
            this.SessionPluginsCombo.Name = "SessionPluginsCombo";
            this.SessionPluginsCombo.Size = new System.Drawing.Size(336, 21);
            this.SessionPluginsCombo.TabIndex = 166;
            // 
            // ScanThreadLimitCB
            // 
            this.ScanThreadLimitCB.AutoSize = true;
            this.ScanThreadLimitCB.Checked = true;
            this.ScanThreadLimitCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanThreadLimitCB.Location = new System.Drawing.Point(17, 149);
            this.ScanThreadLimitCB.Name = "ScanThreadLimitCB";
            this.ScanThreadLimitCB.Size = new System.Drawing.Size(489, 17);
            this.ScanThreadLimitCB.TabIndex = 165;
            this.ScanThreadLimitCB.Text = "When a Session Plugin is selected, automatically set the Scan thread limit to one" +
    ". (Recommended)";
            this.ScanThreadLimitCB.UseVisualStyleBackColor = true;
            // 
            // LaunchSessionPluginCreationAssistantLL
            // 
            this.LaunchSessionPluginCreationAssistantLL.AutoSize = true;
            this.LaunchSessionPluginCreationAssistantLL.Location = new System.Drawing.Point(16, 59);
            this.LaunchSessionPluginCreationAssistantLL.Name = "LaunchSessionPluginCreationAssistantLL";
            this.LaunchSessionPluginCreationAssistantLL.Size = new System.Drawing.Size(202, 13);
            this.LaunchSessionPluginCreationAssistantLL.TabIndex = 164;
            this.LaunchSessionPluginCreationAssistantLL.TabStop = true;
            this.LaunchSessionPluginCreationAssistantLL.Text = "Launch Session Plugin Creation Assistant";
            this.LaunchSessionPluginCreationAssistantLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LaunchSessionPluginCreationAssistantLL_LinkClicked);
            // 
            // RefreshSessListLL
            // 
            this.RefreshSessListLL.AutoSize = true;
            this.RefreshSessListLL.Location = new System.Drawing.Point(728, 123);
            this.RefreshSessListLL.Name = "RefreshSessListLL";
            this.RefreshSessListLL.Size = new System.Drawing.Size(140, 13);
            this.RefreshSessListLL.TabIndex = 163;
            this.RefreshSessListLL.TabStop = true;
            this.RefreshSessListLL.Text = "Refresh Session Plugins List";
            this.RefreshSessListLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RefreshSessListLL_LinkClicked);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(17, 12);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(798, 48);
            this.textBox2.TabIndex = 162;
            this.textBox2.TabStop = false;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(367, 13);
            this.label11.TabIndex = 161;
            this.label11.Text = "If you want to use a Session Plugin for the scan then select one from this list:";
            // 
            // Step3StatusTB
            // 
            this.Step3StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step3StatusTB.BackColor = System.Drawing.SystemColors.Control;
            this.Step3StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step3StatusTB.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Step3StatusTB.ForeColor = System.Drawing.Color.Blue;
            this.Step3StatusTB.Location = new System.Drawing.Point(235, 413);
            this.Step3StatusTB.Multiline = true;
            this.Step3StatusTB.Name = "Step3StatusTB";
            this.Step3StatusTB.Size = new System.Drawing.Size(628, 50);
            this.Step3StatusTB.TabIndex = 20;
            this.Step3StatusTB.TabStop = false;
            // 
            // StepFourPreviousBtn
            // 
            this.StepFourPreviousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepFourPreviousBtn.Location = new System.Drawing.Point(11, 497);
            this.StepFourPreviousBtn.Name = "StepFourPreviousBtn";
            this.StepFourPreviousBtn.Size = new System.Drawing.Size(105, 23);
            this.StepFourPreviousBtn.TabIndex = 19;
            this.StepFourPreviousBtn.Text = "<-Previous Step";
            this.StepFourPreviousBtn.UseVisualStyleBackColor = true;
            this.StepFourPreviousBtn.Click += new System.EventHandler(this.StepFourPreviousBtn_Click);
            // 
            // FinalBtn
            // 
            this.FinalBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FinalBtn.Location = new System.Drawing.Point(737, 484);
            this.FinalBtn.Name = "FinalBtn";
            this.FinalBtn.Size = new System.Drawing.Size(126, 35);
            this.FinalBtn.TabIndex = 16;
            this.FinalBtn.Text = "Start Scan";
            this.FinalBtn.UseVisualStyleBackColor = true;
            this.FinalBtn.Click += new System.EventHandler(this.FinalBtn_Click);
            // 
            // StartScanJobWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.BaseTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "StartScanJobWizard";
            this.Text = "Scan Job Creation Wizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartScanJobWizard_FormClosing);
            this.Load += new System.EventHandler(this.StartScanJobWizard_Load);
            this.BaseTabs.ResumeLayout(false);
            this.RequestTab.ResumeLayout(false);
            this.RequestTab.PerformLayout();
            this.RequestTabs.ResumeLayout(false);
            this.tabPage20.ResumeLayout(false);
            this.tabPage20.PerformLayout();
            this.tabPage21.ResumeLayout(false);
            this.tabPage21.PerformLayout();
            this.ChecksTab.ResumeLayout(false);
            this.ChecksTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanPluginsGrid)).EndInit();
            this.InjectionTab.ResumeLayout(false);
            this.InjectionTab.PerformLayout();
            this.InjectionPointBaseTabs.ResumeLayout(false);
            this.AllTab.ResumeLayout(false);
            this.AllTab.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.URLTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanURLGrid)).EndInit();
            this.QueryTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanQueryGrid)).EndInit();
            this.BodyTab.ResumeLayout(false);
            this.BodyTab.PerformLayout();
            this.BodyInjectTypeTabs.ResumeLayout(false);
            this.BodyTypeNormalTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanBodyTypeNormalGrid)).EndInit();
            this.BodyTypeFormatPluginTab.ResumeLayout(false);
            this.ASRequestBodyTabSplit.Panel1.ResumeLayout(false);
            this.ASRequestBodyTabSplit.Panel2.ResumeLayout(false);
            this.ASRequestBodyTabSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FormatPluginsGrid)).EndInit();
            this.ScanBodyFormatPluginTypeTabs.ResumeLayout(false);
            this.BodyTypeFormatPluginInjectionArrayGridTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BodyTypeFormatPluginGrid)).EndInit();
            this.BodyTypeFormatPluginXMLTab.ResumeLayout(false);
            this.BodyTypeFormatPluginXMLTab.PerformLayout();
            this.BodyTypeCustomTab.ResumeLayout(false);
            this.BodyTypeCustomTab.PerformLayout();
            this.CustomInjectionMarkerTabs.ResumeLayout(false);
            this.CustomMarkerSelectionTab.ResumeLayout(false);
            this.CustomMarkerDisplayTab.ResumeLayout(false);
            this.CustomMarkerEscapingTab.ResumeLayout(false);
            this.CustomMarkerEscapingTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CharacterEscapingGrid)).EndInit();
            this.CharacterEscapingMenu.ResumeLayout(false);
            this.CookieTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanCookieGrid)).EndInit();
            this.HeadersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanHeadersGrid)).EndInit();
            this.ParameterNamesTab.ResumeLayout(false);
            this.ParameterNamesTab.PerformLayout();
            this.BlackListTab.ResumeLayout(false);
            this.BlackListTab.PerformLayout();
            this.CustomizeTab.ResumeLayout(false);
            this.CustomizeTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl BaseTabs;
        private System.Windows.Forms.TabPage RequestTab;
        private System.Windows.Forms.TabPage ChecksTab;
        private System.Windows.Forms.TabPage InjectionTab;
        private System.Windows.Forms.TabPage CustomizeTab;
        internal System.Windows.Forms.TabControl RequestTabs;
        private System.Windows.Forms.TabPage tabPage20;
        internal IronDataView.IronDataView RequestRawHeadersIDV;
        private System.Windows.Forms.TabPage tabPage21;
        internal IronDataView.IronDataView RequestRawBodyIDV;
        internal System.Windows.Forms.DataGridView ScanPluginsGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        internal System.Windows.Forms.TabControl InjectionPointBaseTabs;
        private System.Windows.Forms.TabPage URLTab;
        internal System.Windows.Forms.DataGridView ScanURLGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestURLSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestURLPositionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestURLValueColumn;
        private System.Windows.Forms.TabPage QueryTab;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestQuerySelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestQueryNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestQueryValueColumn;
        private System.Windows.Forms.TabPage BodyTab;
        internal System.Windows.Forms.RadioButton BodyTypeCustomRB;
        internal System.Windows.Forms.RadioButton BodyTypeFormatPluginRB;
        internal System.Windows.Forms.RadioButton BodyTypeNormalRB;
        private System.Windows.Forms.Label label35;
        internal System.Windows.Forms.TabControl BodyInjectTypeTabs;
        private System.Windows.Forms.TabPage BodyTypeNormalTab;
        internal System.Windows.Forms.DataGridView ScanBodyTypeNormalGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.TabPage BodyTypeFormatPluginTab;
        private System.Windows.Forms.SplitContainer ASRequestBodyTabSplit;
        internal System.Windows.Forms.DataGridView FormatPluginsGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestBodyDataFormatSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestBodyDataFormatColumn;
        internal System.Windows.Forms.TabControl ScanBodyFormatPluginTypeTabs;
        private System.Windows.Forms.TabPage BodyTypeFormatPluginInjectionArrayGridTab;
        internal System.Windows.Forms.DataGridView BodyTypeFormatPluginGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestBodySelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestBodyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestBodyValueColumn;
        private System.Windows.Forms.TabPage BodyTypeFormatPluginXMLTab;
        internal System.Windows.Forms.TextBox FormatXMLTB;
        private System.Windows.Forms.TabPage BodyTypeCustomTab;
        internal System.Windows.Forms.TabControl CustomInjectionMarkerTabs;
        private System.Windows.Forms.TabPage CustomMarkerSelectionTab;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label37;
        internal System.Windows.Forms.TextBox CustomEndMarkerTB;
        private System.Windows.Forms.LinkLabel ASApplyCustomMarkersLL;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        internal System.Windows.Forms.TextBox CustomStartMarkerTB;
        private System.Windows.Forms.TabPage CustomMarkerDisplayTab;
        internal System.Windows.Forms.RichTextBox HighlightCustomInjectionPointsRTB;
        internal System.Windows.Forms.Label CustomInjectionPointsHighlightLbl;
        private System.Windows.Forms.TabPage CookieTab;
        internal System.Windows.Forms.DataGridView ScanCookieGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestCookieSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestCookieNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestCookieValueColumn;
        private System.Windows.Forms.TabPage HeadersTab;
        internal System.Windows.Forms.DataGridView ScanHeadersGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestHeadersSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestHeadersNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestHeadersValueColumn;
        private System.Windows.Forms.TabPage ParameterNamesTab;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox ScanHeadersParameterNameCB;
        private System.Windows.Forms.CheckBox ScanCookieParameterNameCB;
        private System.Windows.Forms.CheckBox ScanBodyParameterNameCB;
        private System.Windows.Forms.CheckBox ScanQueryParameterNameCB;
        private System.Windows.Forms.Button StepOneNextBtn;
        private System.Windows.Forms.Button StepTwoPreviousBtn;
        private System.Windows.Forms.Button StepTwoNextBtn;
        private System.Windows.Forms.Button StepThreePreviousBtn;
        private System.Windows.Forms.Button StepThreeNextBtn;
        private System.Windows.Forms.Button FinalBtn;
        private System.Windows.Forms.TextBox Step0TopMsgTB;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox Step1TopMsgTB;
        private System.Windows.Forms.TextBox Step2TopMsgTB;
        private System.Windows.Forms.TabPage AllTab;
        private System.Windows.Forms.TabPage BlackListTab;
        private System.Windows.Forms.CheckBox UseBlackListCB;
        internal System.Windows.Forms.CheckBox ScanParameterNamesCB;
        internal System.Windows.Forms.CheckBox ScanHeadersCB;
        internal System.Windows.Forms.CheckBox ScanCookieCB;
        internal System.Windows.Forms.CheckBox ScanURLCB;
        internal System.Windows.Forms.CheckBox ScanBodyCB;
        internal System.Windows.Forms.CheckBox ScanQueryCB;
        internal System.Windows.Forms.CheckBox ScanAllCB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label UrlPathPartInjectionMessageLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label BodyInjectionMessageLbl;
        private System.Windows.Forms.Label AllHeaderPointsSelectedLbl;
        private System.Windows.Forms.Label AllHeaderPointsAvlLbl;
        private System.Windows.Forms.Label AllCookiePointsSelectedLbl;
        private System.Windows.Forms.Label AllCookiePointsAvlLbl;
        private System.Windows.Forms.Label AllBodyPointsSelectedLbl;
        private System.Windows.Forms.Label AllBodyPointsAvlLbl;
        private System.Windows.Forms.Label AllQueryPointsSelectedLbl;
        private System.Windows.Forms.Label AllQueryPointsAvlLbl;
        private System.Windows.Forms.Label AllUrlPointsSelectedLbl;
        private System.Windows.Forms.Label AllUrlPointsAvlLbl;
        private System.Windows.Forms.Label AllPointsSelectedLbl;
        private System.Windows.Forms.Label AllPointsAvlLbl;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label BlacklistItemsCountLbl;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox ParametersBlacklistTB;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Button StepFourPreviousBtn;
        private System.Windows.Forms.CheckBox RequestSSLCB;
        internal System.Windows.Forms.TextBox Step0StatusTB;
        private System.Windows.Forms.CheckBox SelectAllChecksCB;
        internal System.Windows.Forms.TextBox Step1StatusTB;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label AllNamesPointsAvlLbl;
        private System.Windows.Forms.Label AllNamesPointsSelectedLbl;
        internal System.Windows.Forms.TextBox Step2StatusTB;
        private System.Windows.Forms.ProgressBar Step2ProgressBar;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.DataGridView ScanQueryGrid;
        internal System.Windows.Forms.TextBox Step3StatusTB;
        private System.Windows.Forms.TabPage CustomMarkerEscapingTab;
        internal System.Windows.Forms.DataGridView CharacterEscapingGrid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox EncodedCharacterTB;
        private System.Windows.Forms.TextBox RawCharacterTB;
        private System.Windows.Forms.Button AddToEscapeRuleBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox CharacterEscapingStatusTB;
        private System.Windows.Forms.ContextMenuStrip CharacterEscapingMenu;
        private System.Windows.Forms.ToolStripMenuItem EditRuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteRuleToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EscapingSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RawCharacterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArrowColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EncodedCharacterColumn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel PlaceInjectionMarkerLL;
        private SearchableTextBox SetCustomInjectionPointsSTB;
        private System.Windows.Forms.CheckBox ScanThreadLimitCB;
        private System.Windows.Forms.LinkLabel LaunchSessionPluginCreationAssistantLL;
        private System.Windows.Forms.LinkLabel RefreshSessListLL;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.ComboBox SessionPluginsCombo;

    }
}