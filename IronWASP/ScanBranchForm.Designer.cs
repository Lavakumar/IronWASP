//
// Copyright 2011-2012 Lavakumar Kuppan
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
    partial class ScanBranchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanBranchForm));
            this.ScanBranchHostNameTB = new System.Windows.Forms.TextBox();
            this.ScanBranchHTTPCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchUrlPatternTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ScanBranchSessionPluginsCombo = new System.Windows.Forms.ComboBox();
            this.ScanBranchFormatPluginsCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ScanBranchStartScanBtn = new System.Windows.Forms.Button();
            this.ScanBranchCancelBtn = new System.Windows.Forms.Button();
            this.ScanBranchInjectHeadersCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchInjectCookieCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchInjectBodyCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchInjectQueryCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchInjectURLCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchInjectAllCB = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ScanBranchGETMethodCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchPOSTMethodCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchOtherMethodsCB = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ScanBranchContentJSONCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchContentCSSCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCode5xxCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchContentJSCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCode500CB = new System.Windows.Forms.CheckBox();
            this.ScanBranchContentImgCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCode4xxCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchContentOtherBinaryCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCode403CB = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ScanBranchCode3xxCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchContentHTMLCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCode301_2CB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCode2xxCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCode200CB = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ScanBranchContentOtherTextCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchContentXMLCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchHTTPSCB = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ScanBranchQueryParametersCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchQueryParametersPlusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchQueryParametersMinusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchQueryParametersPlusRB = new System.Windows.Forms.RadioButton();
            this.ScanBranchQueryParametersMinusRB = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ScanBranchFileExtensionsCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchFileExtensionsPlusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchFileExtensionsMinusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchFileExtensionsPlusRB = new System.Windows.Forms.RadioButton();
            this.ScanBranchFileExtensionsMinusRB = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ScanBranchBodyParametersCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchBodyParametersPlusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchBodyParametersMinusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchBodyParametersPlusRB = new System.Windows.Forms.RadioButton();
            this.ScanBranchBodyParametersMinusRB = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ScanBranchCookieParametersCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchCookieParametersPlusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchCookieParametersMinusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchCookieParametersPlusRB = new System.Windows.Forms.RadioButton();
            this.ScanBranchCookieParametersMinusRB = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ScanBranchHeadersParametersCB = new System.Windows.Forms.CheckBox();
            this.ScanBranchHeadersParametersPlusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchHeadersParametersMinusTB = new System.Windows.Forms.TextBox();
            this.ScanBranchHeadersParametersPlusRB = new System.Windows.Forms.RadioButton();
            this.ScanBranchHeadersParametersMinusRB = new System.Windows.Forms.RadioButton();
            this.ScanBranchScanPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanBranchCode304CB = new System.Windows.Forms.CheckBox();
            this.ScanBranchFilterBtn = new System.Windows.Forms.Button();
            this.ScanBranchStatsPanel = new System.Windows.Forms.Panel();
            this.ScanBranchProgressLbl = new System.Windows.Forms.Label();
            this.ScanBranchProgressBar = new System.Windows.Forms.ProgressBar();
            this.ScanBranchErrorTB = new System.Windows.Forms.TextBox();
            this.ScanBranchConfigTabs = new System.Windows.Forms.TabControl();
            this.ScanBranchRequestFilterTab = new System.Windows.Forms.TabPage();
            this.ScanBranchParameterFilterTab = new System.Windows.Forms.TabPage();
            this.ScanBranchPickProxyLogCB = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ScanBranchPickProbeLogCB = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanBranchScanPluginsGrid)).BeginInit();
            this.ScanBranchStatsPanel.SuspendLayout();
            this.ScanBranchConfigTabs.SuspendLayout();
            this.ScanBranchRequestFilterTab.SuspendLayout();
            this.ScanBranchParameterFilterTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanBranchHostNameTB
            // 
            this.ScanBranchHostNameTB.Location = new System.Drawing.Point(74, 6);
            this.ScanBranchHostNameTB.Name = "ScanBranchHostNameTB";
            this.ScanBranchHostNameTB.Size = new System.Drawing.Size(270, 20);
            this.ScanBranchHostNameTB.TabIndex = 0;
            // 
            // ScanBranchHTTPCB
            // 
            this.ScanBranchHTTPCB.AutoSize = true;
            this.ScanBranchHTTPCB.Checked = true;
            this.ScanBranchHTTPCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchHTTPCB.Location = new System.Drawing.Point(350, 8);
            this.ScanBranchHTTPCB.Name = "ScanBranchHTTPCB";
            this.ScanBranchHTTPCB.Size = new System.Drawing.Size(55, 17);
            this.ScanBranchHTTPCB.TabIndex = 1;
            this.ScanBranchHTTPCB.Text = "HTTP";
            this.ScanBranchHTTPCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchUrlPatternTB
            // 
            this.ScanBranchUrlPatternTB.Location = new System.Drawing.Point(74, 32);
            this.ScanBranchUrlPatternTB.Name = "ScanBranchUrlPatternTB";
            this.ScanBranchUrlPatternTB.Size = new System.Drawing.Size(576, 20);
            this.ScanBranchUrlPatternTB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "HostName:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Url Pattern:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Session Plugin:";
            // 
            // ScanBranchSessionPluginsCombo
            // 
            this.ScanBranchSessionPluginsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchSessionPluginsCombo.FormattingEnabled = true;
            this.ScanBranchSessionPluginsCombo.Location = new System.Drawing.Point(96, 88);
            this.ScanBranchSessionPluginsCombo.Name = "ScanBranchSessionPluginsCombo";
            this.ScanBranchSessionPluginsCombo.Size = new System.Drawing.Size(165, 21);
            this.ScanBranchSessionPluginsCombo.TabIndex = 6;
            // 
            // ScanBranchFormatPluginsCombo
            // 
            this.ScanBranchFormatPluginsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchFormatPluginsCombo.FormattingEnabled = true;
            this.ScanBranchFormatPluginsCombo.Location = new System.Drawing.Point(96, 114);
            this.ScanBranchFormatPluginsCombo.Name = "ScanBranchFormatPluginsCombo";
            this.ScanBranchFormatPluginsCombo.Size = new System.Drawing.Size(165, 21);
            this.ScanBranchFormatPluginsCombo.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Format Plugin:";
            // 
            // ScanBranchStartScanBtn
            // 
            this.ScanBranchStartScanBtn.Location = new System.Drawing.Point(488, 6);
            this.ScanBranchStartScanBtn.Name = "ScanBranchStartScanBtn";
            this.ScanBranchStartScanBtn.Size = new System.Drawing.Size(88, 23);
            this.ScanBranchStartScanBtn.TabIndex = 9;
            this.ScanBranchStartScanBtn.Text = "Start Scan";
            this.ScanBranchStartScanBtn.UseVisualStyleBackColor = true;
            this.ScanBranchStartScanBtn.Click += new System.EventHandler(this.ScanBranchStartScanBtn_Click);
            // 
            // ScanBranchCancelBtn
            // 
            this.ScanBranchCancelBtn.Location = new System.Drawing.Point(584, 6);
            this.ScanBranchCancelBtn.Name = "ScanBranchCancelBtn";
            this.ScanBranchCancelBtn.Size = new System.Drawing.Size(67, 23);
            this.ScanBranchCancelBtn.TabIndex = 10;
            this.ScanBranchCancelBtn.Text = "Cancel";
            this.ScanBranchCancelBtn.UseVisualStyleBackColor = true;
            this.ScanBranchCancelBtn.Click += new System.EventHandler(this.ScanBranchCancelBtn_Click);
            // 
            // ScanBranchInjectHeadersCB
            // 
            this.ScanBranchInjectHeadersCB.AutoSize = true;
            this.ScanBranchInjectHeadersCB.Location = new System.Drawing.Point(369, 63);
            this.ScanBranchInjectHeadersCB.Name = "ScanBranchInjectHeadersCB";
            this.ScanBranchInjectHeadersCB.Size = new System.Drawing.Size(66, 17);
            this.ScanBranchInjectHeadersCB.TabIndex = 16;
            this.ScanBranchInjectHeadersCB.Text = "Headers";
            this.ScanBranchInjectHeadersCB.UseVisualStyleBackColor = true;
            this.ScanBranchInjectHeadersCB.Click += new System.EventHandler(this.ScanBranchInjectHeadersCB_Click);
            // 
            // ScanBranchInjectCookieCB
            // 
            this.ScanBranchInjectCookieCB.AutoSize = true;
            this.ScanBranchInjectCookieCB.Checked = true;
            this.ScanBranchInjectCookieCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchInjectCookieCB.Location = new System.Drawing.Point(302, 63);
            this.ScanBranchInjectCookieCB.Name = "ScanBranchInjectCookieCB";
            this.ScanBranchInjectCookieCB.Size = new System.Drawing.Size(59, 17);
            this.ScanBranchInjectCookieCB.TabIndex = 15;
            this.ScanBranchInjectCookieCB.Text = "Cookie";
            this.ScanBranchInjectCookieCB.UseVisualStyleBackColor = true;
            this.ScanBranchInjectCookieCB.Click += new System.EventHandler(this.ScanBranchInjectCookieCB_Click);
            // 
            // ScanBranchInjectBodyCB
            // 
            this.ScanBranchInjectBodyCB.AutoSize = true;
            this.ScanBranchInjectBodyCB.Checked = true;
            this.ScanBranchInjectBodyCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchInjectBodyCB.Location = new System.Drawing.Point(246, 63);
            this.ScanBranchInjectBodyCB.Name = "ScanBranchInjectBodyCB";
            this.ScanBranchInjectBodyCB.Size = new System.Drawing.Size(50, 17);
            this.ScanBranchInjectBodyCB.TabIndex = 14;
            this.ScanBranchInjectBodyCB.Text = "Body";
            this.ScanBranchInjectBodyCB.UseVisualStyleBackColor = true;
            this.ScanBranchInjectBodyCB.Click += new System.EventHandler(this.ScanBranchInjectBodyCB_Click);
            // 
            // ScanBranchInjectQueryCB
            // 
            this.ScanBranchInjectQueryCB.AutoSize = true;
            this.ScanBranchInjectQueryCB.Checked = true;
            this.ScanBranchInjectQueryCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchInjectQueryCB.Location = new System.Drawing.Point(188, 62);
            this.ScanBranchInjectQueryCB.Name = "ScanBranchInjectQueryCB";
            this.ScanBranchInjectQueryCB.Size = new System.Drawing.Size(54, 17);
            this.ScanBranchInjectQueryCB.TabIndex = 13;
            this.ScanBranchInjectQueryCB.Text = "Query";
            this.ScanBranchInjectQueryCB.UseVisualStyleBackColor = true;
            this.ScanBranchInjectQueryCB.Click += new System.EventHandler(this.ScanBranchInjectQueryCB_Click);
            // 
            // ScanBranchInjectURLCB
            // 
            this.ScanBranchInjectURLCB.AutoSize = true;
            this.ScanBranchInjectURLCB.Checked = true;
            this.ScanBranchInjectURLCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchInjectURLCB.Location = new System.Drawing.Point(134, 62);
            this.ScanBranchInjectURLCB.Name = "ScanBranchInjectURLCB";
            this.ScanBranchInjectURLCB.Size = new System.Drawing.Size(48, 17);
            this.ScanBranchInjectURLCB.TabIndex = 12;
            this.ScanBranchInjectURLCB.Text = "URL";
            this.ScanBranchInjectURLCB.UseVisualStyleBackColor = true;
            this.ScanBranchInjectURLCB.Click += new System.EventHandler(this.ScanBranchInjectURLCB_Click);
            // 
            // ScanBranchInjectAllCB
            // 
            this.ScanBranchInjectAllCB.AutoSize = true;
            this.ScanBranchInjectAllCB.Location = new System.Drawing.Point(95, 62);
            this.ScanBranchInjectAllCB.Name = "ScanBranchInjectAllCB";
            this.ScanBranchInjectAllCB.Size = new System.Drawing.Size(37, 17);
            this.ScanBranchInjectAllCB.TabIndex = 11;
            this.ScanBranchInjectAllCB.Text = "All";
            this.ScanBranchInjectAllCB.UseVisualStyleBackColor = true;
            this.ScanBranchInjectAllCB.Click += new System.EventHandler(this.ScanBranchInjectAllCB_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Injection Points:";
            // 
            // ScanBranchGETMethodCB
            // 
            this.ScanBranchGETMethodCB.AutoSize = true;
            this.ScanBranchGETMethodCB.Checked = true;
            this.ScanBranchGETMethodCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchGETMethodCB.Location = new System.Drawing.Point(92, 15);
            this.ScanBranchGETMethodCB.Name = "ScanBranchGETMethodCB";
            this.ScanBranchGETMethodCB.Size = new System.Drawing.Size(48, 17);
            this.ScanBranchGETMethodCB.TabIndex = 21;
            this.ScanBranchGETMethodCB.Text = "GET";
            this.ScanBranchGETMethodCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchPOSTMethodCB
            // 
            this.ScanBranchPOSTMethodCB.AutoSize = true;
            this.ScanBranchPOSTMethodCB.Checked = true;
            this.ScanBranchPOSTMethodCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchPOSTMethodCB.Location = new System.Drawing.Point(142, 15);
            this.ScanBranchPOSTMethodCB.Name = "ScanBranchPOSTMethodCB";
            this.ScanBranchPOSTMethodCB.Size = new System.Drawing.Size(55, 17);
            this.ScanBranchPOSTMethodCB.TabIndex = 22;
            this.ScanBranchPOSTMethodCB.Text = "POST";
            this.ScanBranchPOSTMethodCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchOtherMethodsCB
            // 
            this.ScanBranchOtherMethodsCB.AutoSize = true;
            this.ScanBranchOtherMethodsCB.Checked = true;
            this.ScanBranchOtherMethodsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchOtherMethodsCB.Location = new System.Drawing.Point(202, 15);
            this.ScanBranchOtherMethodsCB.Name = "ScanBranchOtherMethodsCB";
            this.ScanBranchOtherMethodsCB.Size = new System.Drawing.Size(52, 17);
            this.ScanBranchOtherMethodsCB.TabIndex = 23;
            this.ScanBranchOtherMethodsCB.Text = "Other";
            this.ScanBranchOtherMethodsCB.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "HTTP Method:";
            // 
            // ScanBranchContentJSONCB
            // 
            this.ScanBranchContentJSONCB.AutoSize = true;
            this.ScanBranchContentJSONCB.Checked = true;
            this.ScanBranchContentJSONCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentJSONCB.Location = new System.Drawing.Point(331, 65);
            this.ScanBranchContentJSONCB.Name = "ScanBranchContentJSONCB";
            this.ScanBranchContentJSONCB.Size = new System.Drawing.Size(54, 17);
            this.ScanBranchContentJSONCB.TabIndex = 85;
            this.ScanBranchContentJSONCB.Text = "JSON";
            this.ScanBranchContentJSONCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchContentCSSCB
            // 
            this.ScanBranchContentCSSCB.AutoSize = true;
            this.ScanBranchContentCSSCB.Checked = true;
            this.ScanBranchContentCSSCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentCSSCB.Location = new System.Drawing.Point(229, 65);
            this.ScanBranchContentCSSCB.Name = "ScanBranchContentCSSCB";
            this.ScanBranchContentCSSCB.Size = new System.Drawing.Size(47, 17);
            this.ScanBranchContentCSSCB.TabIndex = 68;
            this.ScanBranchContentCSSCB.Text = "CSS";
            this.ScanBranchContentCSSCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCode5xxCB
            // 
            this.ScanBranchCode5xxCB.AutoSize = true;
            this.ScanBranchCode5xxCB.Checked = true;
            this.ScanBranchCode5xxCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode5xxCB.Location = new System.Drawing.Point(531, 40);
            this.ScanBranchCode5xxCB.Name = "ScanBranchCode5xxCB";
            this.ScanBranchCode5xxCB.Size = new System.Drawing.Size(42, 17);
            this.ScanBranchCode5xxCB.TabIndex = 84;
            this.ScanBranchCode5xxCB.Text = "5xx";
            this.ScanBranchCode5xxCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchContentJSCB
            // 
            this.ScanBranchContentJSCB.AutoSize = true;
            this.ScanBranchContentJSCB.Checked = true;
            this.ScanBranchContentJSCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentJSCB.Location = new System.Drawing.Point(185, 65);
            this.ScanBranchContentJSCB.Name = "ScanBranchContentJSCB";
            this.ScanBranchContentJSCB.Size = new System.Drawing.Size(38, 17);
            this.ScanBranchContentJSCB.TabIndex = 69;
            this.ScanBranchContentJSCB.Text = "JS";
            this.ScanBranchContentJSCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCode500CB
            // 
            this.ScanBranchCode500CB.AutoSize = true;
            this.ScanBranchCode500CB.Checked = true;
            this.ScanBranchCode500CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode500CB.Location = new System.Drawing.Point(481, 40);
            this.ScanBranchCode500CB.Name = "ScanBranchCode500CB";
            this.ScanBranchCode500CB.Size = new System.Drawing.Size(44, 17);
            this.ScanBranchCode500CB.TabIndex = 83;
            this.ScanBranchCode500CB.Text = "500";
            this.ScanBranchCode500CB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchContentImgCB
            // 
            this.ScanBranchContentImgCB.AutoSize = true;
            this.ScanBranchContentImgCB.Checked = true;
            this.ScanBranchContentImgCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentImgCB.Location = new System.Drawing.Point(471, 65);
            this.ScanBranchContentImgCB.Name = "ScanBranchContentImgCB";
            this.ScanBranchContentImgCB.Size = new System.Drawing.Size(60, 17);
            this.ScanBranchContentImgCB.TabIndex = 70;
            this.ScanBranchContentImgCB.Text = "Images";
            this.ScanBranchContentImgCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCode4xxCB
            // 
            this.ScanBranchCode4xxCB.AutoSize = true;
            this.ScanBranchCode4xxCB.Checked = true;
            this.ScanBranchCode4xxCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode4xxCB.Location = new System.Drawing.Point(433, 40);
            this.ScanBranchCode4xxCB.Name = "ScanBranchCode4xxCB";
            this.ScanBranchCode4xxCB.Size = new System.Drawing.Size(42, 17);
            this.ScanBranchCode4xxCB.TabIndex = 82;
            this.ScanBranchCode4xxCB.Text = "4xx";
            this.ScanBranchCode4xxCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchContentOtherBinaryCB
            // 
            this.ScanBranchContentOtherBinaryCB.AutoSize = true;
            this.ScanBranchContentOtherBinaryCB.Checked = true;
            this.ScanBranchContentOtherBinaryCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentOtherBinaryCB.Location = new System.Drawing.Point(537, 65);
            this.ScanBranchContentOtherBinaryCB.Name = "ScanBranchContentOtherBinaryCB";
            this.ScanBranchContentOtherBinaryCB.Size = new System.Drawing.Size(84, 17);
            this.ScanBranchContentOtherBinaryCB.TabIndex = 71;
            this.ScanBranchContentOtherBinaryCB.Text = "Other Binary";
            this.ScanBranchContentOtherBinaryCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCode403CB
            // 
            this.ScanBranchCode403CB.AutoSize = true;
            this.ScanBranchCode403CB.Checked = true;
            this.ScanBranchCode403CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode403CB.Location = new System.Drawing.Point(383, 40);
            this.ScanBranchCode403CB.Name = "ScanBranchCode403CB";
            this.ScanBranchCode403CB.Size = new System.Drawing.Size(44, 17);
            this.ScanBranchCode403CB.TabIndex = 81;
            this.ScanBranchCode403CB.Text = "403";
            this.ScanBranchCode403CB.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 13);
            this.label9.TabIndex = 72;
            this.label9.Text = "Response Content Type:";
            // 
            // ScanBranchCode3xxCB
            // 
            this.ScanBranchCode3xxCB.AutoSize = true;
            this.ScanBranchCode3xxCB.Checked = true;
            this.ScanBranchCode3xxCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode3xxCB.Location = new System.Drawing.Point(333, 40);
            this.ScanBranchCode3xxCB.Name = "ScanBranchCode3xxCB";
            this.ScanBranchCode3xxCB.Size = new System.Drawing.Size(42, 17);
            this.ScanBranchCode3xxCB.TabIndex = 80;
            this.ScanBranchCode3xxCB.Text = "3xx";
            this.ScanBranchCode3xxCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchContentHTMLCB
            // 
            this.ScanBranchContentHTMLCB.AutoSize = true;
            this.ScanBranchContentHTMLCB.Checked = true;
            this.ScanBranchContentHTMLCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentHTMLCB.Location = new System.Drawing.Point(129, 65);
            this.ScanBranchContentHTMLCB.Name = "ScanBranchContentHTMLCB";
            this.ScanBranchContentHTMLCB.Size = new System.Drawing.Size(56, 17);
            this.ScanBranchContentHTMLCB.TabIndex = 73;
            this.ScanBranchContentHTMLCB.Text = "HTML";
            this.ScanBranchContentHTMLCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCode301_2CB
            // 
            this.ScanBranchCode301_2CB.AutoSize = true;
            this.ScanBranchCode301_2CB.Checked = true;
            this.ScanBranchCode301_2CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode301_2CB.Location = new System.Drawing.Point(226, 40);
            this.ScanBranchCode301_2CB.Name = "ScanBranchCode301_2CB";
            this.ScanBranchCode301_2CB.Size = new System.Drawing.Size(53, 17);
            this.ScanBranchCode301_2CB.TabIndex = 79;
            this.ScanBranchCode301_2CB.Text = "301-2";
            this.ScanBranchCode301_2CB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCode2xxCB
            // 
            this.ScanBranchCode2xxCB.AutoSize = true;
            this.ScanBranchCode2xxCB.Checked = true;
            this.ScanBranchCode2xxCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode2xxCB.Location = new System.Drawing.Point(178, 40);
            this.ScanBranchCode2xxCB.Name = "ScanBranchCode2xxCB";
            this.ScanBranchCode2xxCB.Size = new System.Drawing.Size(42, 17);
            this.ScanBranchCode2xxCB.TabIndex = 78;
            this.ScanBranchCode2xxCB.Text = "2xx";
            this.ScanBranchCode2xxCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCode200CB
            // 
            this.ScanBranchCode200CB.AutoSize = true;
            this.ScanBranchCode200CB.Checked = true;
            this.ScanBranchCode200CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchCode200CB.Location = new System.Drawing.Point(129, 40);
            this.ScanBranchCode200CB.Name = "ScanBranchCode200CB";
            this.ScanBranchCode200CB.Size = new System.Drawing.Size(44, 17);
            this.ScanBranchCode200CB.TabIndex = 77;
            this.ScanBranchCode200CB.Text = "200";
            this.ScanBranchCode200CB.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 13);
            this.label13.TabIndex = 76;
            this.label13.Text = "Response Status Code:";
            // 
            // ScanBranchContentOtherTextCB
            // 
            this.ScanBranchContentOtherTextCB.AutoSize = true;
            this.ScanBranchContentOtherTextCB.Checked = true;
            this.ScanBranchContentOtherTextCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentOtherTextCB.Location = new System.Drawing.Point(389, 65);
            this.ScanBranchContentOtherTextCB.Name = "ScanBranchContentOtherTextCB";
            this.ScanBranchContentOtherTextCB.Size = new System.Drawing.Size(76, 17);
            this.ScanBranchContentOtherTextCB.TabIndex = 74;
            this.ScanBranchContentOtherTextCB.Text = "Other Text";
            this.ScanBranchContentOtherTextCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchContentXMLCB
            // 
            this.ScanBranchContentXMLCB.AutoSize = true;
            this.ScanBranchContentXMLCB.Checked = true;
            this.ScanBranchContentXMLCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchContentXMLCB.Location = new System.Drawing.Point(282, 65);
            this.ScanBranchContentXMLCB.Name = "ScanBranchContentXMLCB";
            this.ScanBranchContentXMLCB.Size = new System.Drawing.Size(48, 17);
            this.ScanBranchContentXMLCB.TabIndex = 75;
            this.ScanBranchContentXMLCB.Text = "XML";
            this.ScanBranchContentXMLCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchHTTPSCB
            // 
            this.ScanBranchHTTPSCB.AutoSize = true;
            this.ScanBranchHTTPSCB.Checked = true;
            this.ScanBranchHTTPSCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchHTTPSCB.Location = new System.Drawing.Point(419, 8);
            this.ScanBranchHTTPSCB.Name = "ScanBranchHTTPSCB";
            this.ScanBranchHTTPSCB.Size = new System.Drawing.Size(62, 17);
            this.ScanBranchHTTPSCB.TabIndex = 86;
            this.ScanBranchHTTPSCB.Text = "HTTPS";
            this.ScanBranchHTTPSCB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ScanBranchQueryParametersCB);
            this.groupBox1.Controls.Add(this.ScanBranchQueryParametersPlusTB);
            this.groupBox1.Controls.Add(this.ScanBranchQueryParametersMinusTB);
            this.groupBox1.Controls.Add(this.ScanBranchQueryParametersPlusRB);
            this.groupBox1.Controls.Add(this.ScanBranchQueryParametersMinusRB);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(1, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 52);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            // 
            // ScanBranchQueryParametersCB
            // 
            this.ScanBranchQueryParametersCB.AutoSize = true;
            this.ScanBranchQueryParametersCB.Location = new System.Drawing.Point(8, 20);
            this.ScanBranchQueryParametersCB.Name = "ScanBranchQueryParametersCB";
            this.ScanBranchQueryParametersCB.Size = new System.Drawing.Size(113, 17);
            this.ScanBranchQueryParametersCB.TabIndex = 59;
            this.ScanBranchQueryParametersCB.Text = "Query Parameters:";
            this.ScanBranchQueryParametersCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchQueryParametersPlusTB
            // 
            this.ScanBranchQueryParametersPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchQueryParametersPlusTB.Location = new System.Drawing.Point(159, 8);
            this.ScanBranchQueryParametersPlusTB.Name = "ScanBranchQueryParametersPlusTB";
            this.ScanBranchQueryParametersPlusTB.Size = new System.Drawing.Size(486, 20);
            this.ScanBranchQueryParametersPlusTB.TabIndex = 51;
            // 
            // ScanBranchQueryParametersMinusTB
            // 
            this.ScanBranchQueryParametersMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchQueryParametersMinusTB.Location = new System.Drawing.Point(159, 29);
            this.ScanBranchQueryParametersMinusTB.Name = "ScanBranchQueryParametersMinusTB";
            this.ScanBranchQueryParametersMinusTB.Size = new System.Drawing.Size(486, 20);
            this.ScanBranchQueryParametersMinusTB.TabIndex = 52;
            // 
            // ScanBranchQueryParametersPlusRB
            // 
            this.ScanBranchQueryParametersPlusRB.AutoSize = true;
            this.ScanBranchQueryParametersPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchQueryParametersPlusRB.Location = new System.Drawing.Point(126, 9);
            this.ScanBranchQueryParametersPlusRB.Name = "ScanBranchQueryParametersPlusRB";
            this.ScanBranchQueryParametersPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ScanBranchQueryParametersPlusRB.TabIndex = 57;
            this.ScanBranchQueryParametersPlusRB.Text = "+";
            this.ScanBranchQueryParametersPlusRB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchQueryParametersMinusRB
            // 
            this.ScanBranchQueryParametersMinusRB.AutoSize = true;
            this.ScanBranchQueryParametersMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchQueryParametersMinusRB.Location = new System.Drawing.Point(126, 28);
            this.ScanBranchQueryParametersMinusRB.Name = "ScanBranchQueryParametersMinusRB";
            this.ScanBranchQueryParametersMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ScanBranchQueryParametersMinusRB.TabIndex = 58;
            this.ScanBranchQueryParametersMinusRB.Text = "-";
            this.ScanBranchQueryParametersMinusRB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ScanBranchFileExtensionsCB);
            this.groupBox2.Controls.Add(this.ScanBranchFileExtensionsPlusTB);
            this.groupBox2.Controls.Add(this.ScanBranchFileExtensionsMinusTB);
            this.groupBox2.Controls.Add(this.ScanBranchFileExtensionsPlusRB);
            this.groupBox2.Controls.Add(this.ScanBranchFileExtensionsMinusRB);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(1, 101);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(651, 52);
            this.groupBox2.TabIndex = 88;
            this.groupBox2.TabStop = false;
            // 
            // ScanBranchFileExtensionsCB
            // 
            this.ScanBranchFileExtensionsCB.AutoSize = true;
            this.ScanBranchFileExtensionsCB.Checked = true;
            this.ScanBranchFileExtensionsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchFileExtensionsCB.Location = new System.Drawing.Point(8, 20);
            this.ScanBranchFileExtensionsCB.Name = "ScanBranchFileExtensionsCB";
            this.ScanBranchFileExtensionsCB.Size = new System.Drawing.Size(99, 17);
            this.ScanBranchFileExtensionsCB.TabIndex = 59;
            this.ScanBranchFileExtensionsCB.Text = "File Extensions:";
            this.ScanBranchFileExtensionsCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchFileExtensionsPlusTB
            // 
            this.ScanBranchFileExtensionsPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchFileExtensionsPlusTB.Location = new System.Drawing.Point(160, 8);
            this.ScanBranchFileExtensionsPlusTB.Name = "ScanBranchFileExtensionsPlusTB";
            this.ScanBranchFileExtensionsPlusTB.Size = new System.Drawing.Size(485, 20);
            this.ScanBranchFileExtensionsPlusTB.TabIndex = 51;
            // 
            // ScanBranchFileExtensionsMinusTB
            // 
            this.ScanBranchFileExtensionsMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchFileExtensionsMinusTB.Location = new System.Drawing.Point(160, 29);
            this.ScanBranchFileExtensionsMinusTB.Name = "ScanBranchFileExtensionsMinusTB";
            this.ScanBranchFileExtensionsMinusTB.Size = new System.Drawing.Size(485, 20);
            this.ScanBranchFileExtensionsMinusTB.TabIndex = 52;
            this.ScanBranchFileExtensionsMinusTB.Text = "css,js,jpg,jpeg,png,gif,ico,swf,doc,docx,pdf,xls,xlsx,ppt,pptx";
            // 
            // ScanBranchFileExtensionsPlusRB
            // 
            this.ScanBranchFileExtensionsPlusRB.AutoSize = true;
            this.ScanBranchFileExtensionsPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchFileExtensionsPlusRB.Location = new System.Drawing.Point(127, 9);
            this.ScanBranchFileExtensionsPlusRB.Name = "ScanBranchFileExtensionsPlusRB";
            this.ScanBranchFileExtensionsPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ScanBranchFileExtensionsPlusRB.TabIndex = 57;
            this.ScanBranchFileExtensionsPlusRB.Text = "+";
            this.ScanBranchFileExtensionsPlusRB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchFileExtensionsMinusRB
            // 
            this.ScanBranchFileExtensionsMinusRB.AutoSize = true;
            this.ScanBranchFileExtensionsMinusRB.Checked = true;
            this.ScanBranchFileExtensionsMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchFileExtensionsMinusRB.Location = new System.Drawing.Point(127, 28);
            this.ScanBranchFileExtensionsMinusRB.Name = "ScanBranchFileExtensionsMinusRB";
            this.ScanBranchFileExtensionsMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ScanBranchFileExtensionsMinusRB.TabIndex = 58;
            this.ScanBranchFileExtensionsMinusRB.TabStop = true;
            this.ScanBranchFileExtensionsMinusRB.Text = "-";
            this.ScanBranchFileExtensionsMinusRB.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ScanBranchBodyParametersCB);
            this.groupBox3.Controls.Add(this.ScanBranchBodyParametersPlusTB);
            this.groupBox3.Controls.Add(this.ScanBranchBodyParametersMinusTB);
            this.groupBox3.Controls.Add(this.ScanBranchBodyParametersPlusRB);
            this.groupBox3.Controls.Add(this.ScanBranchBodyParametersMinusRB);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Location = new System.Drawing.Point(1, 54);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(651, 52);
            this.groupBox3.TabIndex = 89;
            this.groupBox3.TabStop = false;
            // 
            // ScanBranchBodyParametersCB
            // 
            this.ScanBranchBodyParametersCB.AutoSize = true;
            this.ScanBranchBodyParametersCB.Location = new System.Drawing.Point(8, 20);
            this.ScanBranchBodyParametersCB.Name = "ScanBranchBodyParametersCB";
            this.ScanBranchBodyParametersCB.Size = new System.Drawing.Size(109, 17);
            this.ScanBranchBodyParametersCB.TabIndex = 59;
            this.ScanBranchBodyParametersCB.Text = "Body Parameters:";
            this.ScanBranchBodyParametersCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchBodyParametersPlusTB
            // 
            this.ScanBranchBodyParametersPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchBodyParametersPlusTB.Location = new System.Drawing.Point(160, 8);
            this.ScanBranchBodyParametersPlusTB.Name = "ScanBranchBodyParametersPlusTB";
            this.ScanBranchBodyParametersPlusTB.Size = new System.Drawing.Size(485, 20);
            this.ScanBranchBodyParametersPlusTB.TabIndex = 51;
            // 
            // ScanBranchBodyParametersMinusTB
            // 
            this.ScanBranchBodyParametersMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchBodyParametersMinusTB.Location = new System.Drawing.Point(160, 29);
            this.ScanBranchBodyParametersMinusTB.Name = "ScanBranchBodyParametersMinusTB";
            this.ScanBranchBodyParametersMinusTB.Size = new System.Drawing.Size(485, 20);
            this.ScanBranchBodyParametersMinusTB.TabIndex = 52;
            // 
            // ScanBranchBodyParametersPlusRB
            // 
            this.ScanBranchBodyParametersPlusRB.AutoSize = true;
            this.ScanBranchBodyParametersPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchBodyParametersPlusRB.Location = new System.Drawing.Point(127, 9);
            this.ScanBranchBodyParametersPlusRB.Name = "ScanBranchBodyParametersPlusRB";
            this.ScanBranchBodyParametersPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ScanBranchBodyParametersPlusRB.TabIndex = 57;
            this.ScanBranchBodyParametersPlusRB.Text = "+";
            this.ScanBranchBodyParametersPlusRB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchBodyParametersMinusRB
            // 
            this.ScanBranchBodyParametersMinusRB.AutoSize = true;
            this.ScanBranchBodyParametersMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchBodyParametersMinusRB.Location = new System.Drawing.Point(127, 28);
            this.ScanBranchBodyParametersMinusRB.Name = "ScanBranchBodyParametersMinusRB";
            this.ScanBranchBodyParametersMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ScanBranchBodyParametersMinusRB.TabIndex = 58;
            this.ScanBranchBodyParametersMinusRB.Text = "-";
            this.ScanBranchBodyParametersMinusRB.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.ScanBranchCookieParametersCB);
            this.groupBox4.Controls.Add(this.ScanBranchCookieParametersPlusTB);
            this.groupBox4.Controls.Add(this.ScanBranchCookieParametersMinusTB);
            this.groupBox4.Controls.Add(this.ScanBranchCookieParametersPlusRB);
            this.groupBox4.Controls.Add(this.ScanBranchCookieParametersMinusRB);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(1, 108);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(648, 52);
            this.groupBox4.TabIndex = 90;
            this.groupBox4.TabStop = false;
            // 
            // ScanBranchCookieParametersCB
            // 
            this.ScanBranchCookieParametersCB.AutoSize = true;
            this.ScanBranchCookieParametersCB.Location = new System.Drawing.Point(8, 20);
            this.ScanBranchCookieParametersCB.Name = "ScanBranchCookieParametersCB";
            this.ScanBranchCookieParametersCB.Size = new System.Drawing.Size(118, 17);
            this.ScanBranchCookieParametersCB.TabIndex = 59;
            this.ScanBranchCookieParametersCB.Text = "Cookie Parameters:";
            this.ScanBranchCookieParametersCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCookieParametersPlusTB
            // 
            this.ScanBranchCookieParametersPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchCookieParametersPlusTB.Location = new System.Drawing.Point(161, 8);
            this.ScanBranchCookieParametersPlusTB.Name = "ScanBranchCookieParametersPlusTB";
            this.ScanBranchCookieParametersPlusTB.Size = new System.Drawing.Size(484, 20);
            this.ScanBranchCookieParametersPlusTB.TabIndex = 51;
            // 
            // ScanBranchCookieParametersMinusTB
            // 
            this.ScanBranchCookieParametersMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchCookieParametersMinusTB.Location = new System.Drawing.Point(161, 29);
            this.ScanBranchCookieParametersMinusTB.Name = "ScanBranchCookieParametersMinusTB";
            this.ScanBranchCookieParametersMinusTB.Size = new System.Drawing.Size(484, 20);
            this.ScanBranchCookieParametersMinusTB.TabIndex = 52;
            // 
            // ScanBranchCookieParametersPlusRB
            // 
            this.ScanBranchCookieParametersPlusRB.AutoSize = true;
            this.ScanBranchCookieParametersPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchCookieParametersPlusRB.Location = new System.Drawing.Point(129, 9);
            this.ScanBranchCookieParametersPlusRB.Name = "ScanBranchCookieParametersPlusRB";
            this.ScanBranchCookieParametersPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ScanBranchCookieParametersPlusRB.TabIndex = 57;
            this.ScanBranchCookieParametersPlusRB.Text = "+";
            this.ScanBranchCookieParametersPlusRB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchCookieParametersMinusRB
            // 
            this.ScanBranchCookieParametersMinusRB.AutoSize = true;
            this.ScanBranchCookieParametersMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchCookieParametersMinusRB.Location = new System.Drawing.Point(129, 28);
            this.ScanBranchCookieParametersMinusRB.Name = "ScanBranchCookieParametersMinusRB";
            this.ScanBranchCookieParametersMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ScanBranchCookieParametersMinusRB.TabIndex = 58;
            this.ScanBranchCookieParametersMinusRB.Text = "-";
            this.ScanBranchCookieParametersMinusRB.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.ScanBranchHeadersParametersCB);
            this.groupBox5.Controls.Add(this.ScanBranchHeadersParametersPlusTB);
            this.groupBox5.Controls.Add(this.ScanBranchHeadersParametersMinusTB);
            this.groupBox5.Controls.Add(this.ScanBranchHeadersParametersPlusRB);
            this.groupBox5.Controls.Add(this.ScanBranchHeadersParametersMinusRB);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(1, 162);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(651, 52);
            this.groupBox5.TabIndex = 91;
            this.groupBox5.TabStop = false;
            // 
            // ScanBranchHeadersParametersCB
            // 
            this.ScanBranchHeadersParametersCB.AutoSize = true;
            this.ScanBranchHeadersParametersCB.Location = new System.Drawing.Point(8, 20);
            this.ScanBranchHeadersParametersCB.Name = "ScanBranchHeadersParametersCB";
            this.ScanBranchHeadersParametersCB.Size = new System.Drawing.Size(120, 17);
            this.ScanBranchHeadersParametersCB.TabIndex = 59;
            this.ScanBranchHeadersParametersCB.Text = "Header Parameters:";
            this.ScanBranchHeadersParametersCB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchHeadersParametersPlusTB
            // 
            this.ScanBranchHeadersParametersPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchHeadersParametersPlusTB.Location = new System.Drawing.Point(161, 8);
            this.ScanBranchHeadersParametersPlusTB.Name = "ScanBranchHeadersParametersPlusTB";
            this.ScanBranchHeadersParametersPlusTB.Size = new System.Drawing.Size(484, 20);
            this.ScanBranchHeadersParametersPlusTB.TabIndex = 51;
            // 
            // ScanBranchHeadersParametersMinusTB
            // 
            this.ScanBranchHeadersParametersMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBranchHeadersParametersMinusTB.Location = new System.Drawing.Point(161, 29);
            this.ScanBranchHeadersParametersMinusTB.Name = "ScanBranchHeadersParametersMinusTB";
            this.ScanBranchHeadersParametersMinusTB.Size = new System.Drawing.Size(484, 20);
            this.ScanBranchHeadersParametersMinusTB.TabIndex = 52;
            // 
            // ScanBranchHeadersParametersPlusRB
            // 
            this.ScanBranchHeadersParametersPlusRB.AutoSize = true;
            this.ScanBranchHeadersParametersPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchHeadersParametersPlusRB.Location = new System.Drawing.Point(129, 9);
            this.ScanBranchHeadersParametersPlusRB.Name = "ScanBranchHeadersParametersPlusRB";
            this.ScanBranchHeadersParametersPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ScanBranchHeadersParametersPlusRB.TabIndex = 57;
            this.ScanBranchHeadersParametersPlusRB.Text = "+";
            this.ScanBranchHeadersParametersPlusRB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchHeadersParametersMinusRB
            // 
            this.ScanBranchHeadersParametersMinusRB.AutoSize = true;
            this.ScanBranchHeadersParametersMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBranchHeadersParametersMinusRB.Location = new System.Drawing.Point(129, 28);
            this.ScanBranchHeadersParametersMinusRB.Name = "ScanBranchHeadersParametersMinusRB";
            this.ScanBranchHeadersParametersMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ScanBranchHeadersParametersMinusRB.TabIndex = 58;
            this.ScanBranchHeadersParametersMinusRB.Text = "-";
            this.ScanBranchHeadersParametersMinusRB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchScanPluginsGrid
            // 
            this.ScanBranchScanPluginsGrid.AllowUserToAddRows = false;
            this.ScanBranchScanPluginsGrid.AllowUserToDeleteRows = false;
            this.ScanBranchScanPluginsGrid.AllowUserToResizeRows = false;
            this.ScanBranchScanPluginsGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanBranchScanPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanBranchScanPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanBranchScanPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn9,
            this.dataGridViewTextBoxColumn27});
            this.ScanBranchScanPluginsGrid.GridColor = System.Drawing.Color.White;
            this.ScanBranchScanPluginsGrid.Location = new System.Drawing.Point(455, 55);
            this.ScanBranchScanPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanBranchScanPluginsGrid.Name = "ScanBranchScanPluginsGrid";
            this.ScanBranchScanPluginsGrid.ReadOnly = true;
            this.ScanBranchScanPluginsGrid.RowHeadersVisible = false;
            this.ScanBranchScanPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanBranchScanPluginsGrid.Size = new System.Drawing.Size(194, 133);
            this.ScanBranchScanPluginsGrid.TabIndex = 92;
            this.ScanBranchScanPluginsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanBranchScanPluginsGrid_CellClick);
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
            this.dataGridViewTextBoxColumn27.HeaderText = "SCAN PLUGINS";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ScanBranchCode304CB
            // 
            this.ScanBranchCode304CB.AutoSize = true;
            this.ScanBranchCode304CB.Location = new System.Drawing.Point(279, 40);
            this.ScanBranchCode304CB.Name = "ScanBranchCode304CB";
            this.ScanBranchCode304CB.Size = new System.Drawing.Size(44, 17);
            this.ScanBranchCode304CB.TabIndex = 95;
            this.ScanBranchCode304CB.Text = "304";
            this.ScanBranchCode304CB.UseVisualStyleBackColor = true;
            // 
            // ScanBranchFilterBtn
            // 
            this.ScanBranchFilterBtn.Location = new System.Drawing.Point(338, 162);
            this.ScanBranchFilterBtn.Name = "ScanBranchFilterBtn";
            this.ScanBranchFilterBtn.Size = new System.Drawing.Size(100, 23);
            this.ScanBranchFilterBtn.TabIndex = 96;
            this.ScanBranchFilterBtn.Text = "Show Filter";
            this.ScanBranchFilterBtn.UseVisualStyleBackColor = true;
            this.ScanBranchFilterBtn.Click += new System.EventHandler(this.ScanBranchFilterBtn_Click);
            // 
            // ScanBranchStatsPanel
            // 
            this.ScanBranchStatsPanel.Controls.Add(this.ScanBranchProgressLbl);
            this.ScanBranchStatsPanel.Controls.Add(this.ScanBranchProgressBar);
            this.ScanBranchStatsPanel.Location = new System.Drawing.Point(3, 162);
            this.ScanBranchStatsPanel.Name = "ScanBranchStatsPanel";
            this.ScanBranchStatsPanel.Size = new System.Drawing.Size(449, 27);
            this.ScanBranchStatsPanel.TabIndex = 97;
            this.ScanBranchStatsPanel.Visible = false;
            // 
            // ScanBranchProgressLbl
            // 
            this.ScanBranchProgressLbl.AutoSize = true;
            this.ScanBranchProgressLbl.Location = new System.Drawing.Point(3, 9);
            this.ScanBranchProgressLbl.Name = "ScanBranchProgressLbl";
            this.ScanBranchProgressLbl.Size = new System.Drawing.Size(31, 13);
            this.ScanBranchProgressLbl.TabIndex = 98;
            this.ScanBranchProgressLbl.Text = "Stats";
            // 
            // ScanBranchProgressBar
            // 
            this.ScanBranchProgressBar.Location = new System.Drawing.Point(265, 2);
            this.ScanBranchProgressBar.Name = "ScanBranchProgressBar";
            this.ScanBranchProgressBar.Size = new System.Drawing.Size(181, 23);
            this.ScanBranchProgressBar.TabIndex = 0;
            // 
            // ScanBranchErrorTB
            // 
            this.ScanBranchErrorTB.BackColor = System.Drawing.SystemColors.Control;
            this.ScanBranchErrorTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanBranchErrorTB.ForeColor = System.Drawing.Color.Red;
            this.ScanBranchErrorTB.Location = new System.Drawing.Point(268, 91);
            this.ScanBranchErrorTB.Multiline = true;
            this.ScanBranchErrorTB.Name = "ScanBranchErrorTB";
            this.ScanBranchErrorTB.ReadOnly = true;
            this.ScanBranchErrorTB.Size = new System.Drawing.Size(181, 46);
            this.ScanBranchErrorTB.TabIndex = 98;
            // 
            // ScanBranchConfigTabs
            // 
            this.ScanBranchConfigTabs.Controls.Add(this.ScanBranchRequestFilterTab);
            this.ScanBranchConfigTabs.Controls.Add(this.ScanBranchParameterFilterTab);
            this.ScanBranchConfigTabs.Location = new System.Drawing.Point(0, 200);
            this.ScanBranchConfigTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ScanBranchConfigTabs.Name = "ScanBranchConfigTabs";
            this.ScanBranchConfigTabs.Padding = new System.Drawing.Point(0, 0);
            this.ScanBranchConfigTabs.SelectedIndex = 0;
            this.ScanBranchConfigTabs.Size = new System.Drawing.Size(657, 246);
            this.ScanBranchConfigTabs.TabIndex = 100;
            // 
            // ScanBranchRequestFilterTab
            // 
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode304CB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.label7);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode4xxCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentImgCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentOtherBinaryCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchOtherMethodsCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode500CB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode403CB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentJSCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.label9);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchPOSTMethodCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode5xxCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode3xxCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchGETMethodCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentCSSCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentXMLCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentHTMLCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.groupBox2);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentJSONCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchContentOtherTextCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode301_2CB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.label13);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode2xxCB);
            this.ScanBranchRequestFilterTab.Controls.Add(this.ScanBranchCode200CB);
            this.ScanBranchRequestFilterTab.Location = new System.Drawing.Point(4, 22);
            this.ScanBranchRequestFilterTab.Margin = new System.Windows.Forms.Padding(0);
            this.ScanBranchRequestFilterTab.Name = "ScanBranchRequestFilterTab";
            this.ScanBranchRequestFilterTab.Size = new System.Drawing.Size(649, 220);
            this.ScanBranchRequestFilterTab.TabIndex = 0;
            this.ScanBranchRequestFilterTab.Text = "Request Selection Filter";
            this.ScanBranchRequestFilterTab.UseVisualStyleBackColor = true;
            // 
            // ScanBranchParameterFilterTab
            // 
            this.ScanBranchParameterFilterTab.Controls.Add(this.groupBox5);
            this.ScanBranchParameterFilterTab.Controls.Add(this.groupBox1);
            this.ScanBranchParameterFilterTab.Controls.Add(this.groupBox3);
            this.ScanBranchParameterFilterTab.Controls.Add(this.groupBox4);
            this.ScanBranchParameterFilterTab.Location = new System.Drawing.Point(4, 22);
            this.ScanBranchParameterFilterTab.Margin = new System.Windows.Forms.Padding(0);
            this.ScanBranchParameterFilterTab.Name = "ScanBranchParameterFilterTab";
            this.ScanBranchParameterFilterTab.Size = new System.Drawing.Size(649, 220);
            this.ScanBranchParameterFilterTab.TabIndex = 1;
            this.ScanBranchParameterFilterTab.Text = "ParameterSelectionTab";
            this.ScanBranchParameterFilterTab.UseVisualStyleBackColor = true;
            // 
            // ScanBranchPickProxyLogCB
            // 
            this.ScanBranchPickProxyLogCB.AutoSize = true;
            this.ScanBranchPickProxyLogCB.Checked = true;
            this.ScanBranchPickProxyLogCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScanBranchPickProxyLogCB.Location = new System.Drawing.Point(124, 140);
            this.ScanBranchPickProxyLogCB.Name = "ScanBranchPickProxyLogCB";
            this.ScanBranchPickProxyLogCB.Size = new System.Drawing.Size(73, 17);
            this.ScanBranchPickProxyLogCB.TabIndex = 101;
            this.ScanBranchPickProxyLogCB.Text = "Proxy Log";
            this.ScanBranchPickProxyLogCB.UseVisualStyleBackColor = true;
            this.ScanBranchPickProxyLogCB.CheckedChanged += new System.EventHandler(this.ScanBranchPickProxyLogCB_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 102;
            this.label8.Text = "Select Requests from:";
            // 
            // ScanBranchPickProbeLogCB
            // 
            this.ScanBranchPickProbeLogCB.AutoSize = true;
            this.ScanBranchPickProbeLogCB.Location = new System.Drawing.Point(203, 140);
            this.ScanBranchPickProbeLogCB.Name = "ScanBranchPickProbeLogCB";
            this.ScanBranchPickProbeLogCB.Size = new System.Drawing.Size(75, 17);
            this.ScanBranchPickProbeLogCB.TabIndex = 103;
            this.ScanBranchPickProbeLogCB.Text = "Probe Log";
            this.ScanBranchPickProbeLogCB.UseVisualStyleBackColor = true;
            this.ScanBranchPickProbeLogCB.CheckedChanged += new System.EventHandler(this.ScanBranchPickProbeLogCB_CheckedChanged);
            // 
            // ScanBranchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(658, 447);
            this.ControlBox = false;
            this.Controls.Add(this.ScanBranchPickProbeLogCB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ScanBranchPickProxyLogCB);
            this.Controls.Add(this.ScanBranchConfigTabs);
            this.Controls.Add(this.ScanBranchErrorTB);
            this.Controls.Add(this.ScanBranchStatsPanel);
            this.Controls.Add(this.ScanBranchFilterBtn);
            this.Controls.Add(this.ScanBranchScanPluginsGrid);
            this.Controls.Add(this.ScanBranchHTTPSCB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ScanBranchInjectHeadersCB);
            this.Controls.Add(this.ScanBranchInjectCookieCB);
            this.Controls.Add(this.ScanBranchInjectBodyCB);
            this.Controls.Add(this.ScanBranchInjectQueryCB);
            this.Controls.Add(this.ScanBranchInjectURLCB);
            this.Controls.Add(this.ScanBranchInjectAllCB);
            this.Controls.Add(this.ScanBranchCancelBtn);
            this.Controls.Add(this.ScanBranchStartScanBtn);
            this.Controls.Add(this.ScanBranchFormatPluginsCombo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ScanBranchSessionPluginsCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScanBranchUrlPatternTB);
            this.Controls.Add(this.ScanBranchHTTPCB);
            this.Controls.Add(this.ScanBranchHostNameTB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(674, 485);
            this.MinimumSize = new System.Drawing.Size(674, 230);
            this.Name = "ScanBranchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure Scan";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanBranchScanPluginsGrid)).EndInit();
            this.ScanBranchStatsPanel.ResumeLayout(false);
            this.ScanBranchStatsPanel.PerformLayout();
            this.ScanBranchConfigTabs.ResumeLayout(false);
            this.ScanBranchRequestFilterTab.ResumeLayout(false);
            this.ScanBranchRequestFilterTab.PerformLayout();
            this.ScanBranchParameterFilterTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox ScanBranchSessionPluginsCombo;
        internal System.Windows.Forms.ComboBox ScanBranchFormatPluginsCombo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ScanBranchStartScanBtn;
        internal System.Windows.Forms.CheckBox ScanBranchInjectHeadersCB;
        internal System.Windows.Forms.CheckBox ScanBranchInjectCookieCB;
        internal System.Windows.Forms.CheckBox ScanBranchInjectBodyCB;
        internal System.Windows.Forms.CheckBox ScanBranchInjectQueryCB;
        internal System.Windows.Forms.CheckBox ScanBranchInjectURLCB;
        internal System.Windows.Forms.CheckBox ScanBranchInjectAllCB;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox ScanBranchGETMethodCB;
        internal System.Windows.Forms.CheckBox ScanBranchPOSTMethodCB;
        internal System.Windows.Forms.CheckBox ScanBranchOtherMethodsCB;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.CheckBox ScanBranchContentJSONCB;
        internal System.Windows.Forms.CheckBox ScanBranchContentCSSCB;
        internal System.Windows.Forms.CheckBox ScanBranchCode5xxCB;
        internal System.Windows.Forms.CheckBox ScanBranchContentJSCB;
        internal System.Windows.Forms.CheckBox ScanBranchCode500CB;
        internal System.Windows.Forms.CheckBox ScanBranchContentImgCB;
        internal System.Windows.Forms.CheckBox ScanBranchCode4xxCB;
        internal System.Windows.Forms.CheckBox ScanBranchContentOtherBinaryCB;
        internal System.Windows.Forms.CheckBox ScanBranchCode403CB;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.CheckBox ScanBranchCode3xxCB;
        internal System.Windows.Forms.CheckBox ScanBranchContentHTMLCB;
        internal System.Windows.Forms.CheckBox ScanBranchCode301_2CB;
        internal System.Windows.Forms.CheckBox ScanBranchCode2xxCB;
        internal System.Windows.Forms.CheckBox ScanBranchCode200CB;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.CheckBox ScanBranchContentOtherTextCB;
        internal System.Windows.Forms.CheckBox ScanBranchContentXMLCB;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.CheckBox ScanBranchQueryParametersCB;
        internal System.Windows.Forms.TextBox ScanBranchQueryParametersPlusTB;
        internal System.Windows.Forms.TextBox ScanBranchQueryParametersMinusTB;
        internal System.Windows.Forms.RadioButton ScanBranchQueryParametersPlusRB;
        internal System.Windows.Forms.RadioButton ScanBranchQueryParametersMinusRB;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.CheckBox ScanBranchFileExtensionsCB;
        internal System.Windows.Forms.TextBox ScanBranchFileExtensionsPlusTB;
        internal System.Windows.Forms.TextBox ScanBranchFileExtensionsMinusTB;
        internal System.Windows.Forms.RadioButton ScanBranchFileExtensionsPlusRB;
        internal System.Windows.Forms.RadioButton ScanBranchFileExtensionsMinusRB;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.CheckBox ScanBranchBodyParametersCB;
        internal System.Windows.Forms.TextBox ScanBranchBodyParametersPlusTB;
        internal System.Windows.Forms.TextBox ScanBranchBodyParametersMinusTB;
        internal System.Windows.Forms.RadioButton ScanBranchBodyParametersPlusRB;
        internal System.Windows.Forms.RadioButton ScanBranchBodyParametersMinusRB;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.CheckBox ScanBranchCookieParametersCB;
        internal System.Windows.Forms.TextBox ScanBranchCookieParametersPlusTB;
        internal System.Windows.Forms.TextBox ScanBranchCookieParametersMinusTB;
        internal System.Windows.Forms.RadioButton ScanBranchCookieParametersPlusRB;
        internal System.Windows.Forms.RadioButton ScanBranchCookieParametersMinusRB;
        private System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.CheckBox ScanBranchHeadersParametersCB;
        internal System.Windows.Forms.TextBox ScanBranchHeadersParametersPlusTB;
        internal System.Windows.Forms.TextBox ScanBranchHeadersParametersMinusTB;
        internal System.Windows.Forms.RadioButton ScanBranchHeadersParametersPlusRB;
        internal System.Windows.Forms.RadioButton ScanBranchHeadersParametersMinusRB;
        internal System.Windows.Forms.DataGridView ScanBranchScanPluginsGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        internal System.Windows.Forms.TextBox ScanBranchHostNameTB;
        internal System.Windows.Forms.CheckBox ScanBranchHTTPCB;
        internal System.Windows.Forms.TextBox ScanBranchUrlPatternTB;
        internal System.Windows.Forms.CheckBox ScanBranchHTTPSCB;
        internal System.Windows.Forms.Button ScanBranchFilterBtn;
        internal System.Windows.Forms.Panel ScanBranchStatsPanel;
        internal System.Windows.Forms.ProgressBar ScanBranchProgressBar;
        internal System.Windows.Forms.Label ScanBranchProgressLbl;
        internal System.Windows.Forms.CheckBox ScanBranchCode304CB;
        internal System.Windows.Forms.Button ScanBranchCancelBtn;
        internal System.Windows.Forms.TextBox ScanBranchErrorTB;
        private System.Windows.Forms.TabPage ScanBranchRequestFilterTab;
        private System.Windows.Forms.TabPage ScanBranchParameterFilterTab;
        internal System.Windows.Forms.TabControl ScanBranchConfigTabs;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.CheckBox ScanBranchPickProxyLogCB;
        internal System.Windows.Forms.CheckBox ScanBranchPickProbeLogCB;
    }
}