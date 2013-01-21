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
    partial class LogAnalyzer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogAnalyzer));
            this.LogGrid = new System.Windows.Forms.DataGridView();
            this.SelectClmn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IDClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HostNameSelectClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MethodClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URLClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSLClmn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ParametersClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodeClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LengthClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmnMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetCookieClmn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NotesClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTestBtn = new System.Windows.Forms.Button();
            this.RunModulesBtn = new System.Windows.Forms.Button();
            this.StartScanBtn = new System.Windows.Forms.Button();
            this.SelectAllCB = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.MatchKeywordInResponseBodySectionCB = new System.Windows.Forms.CheckBox();
            this.MatchKeywordInResponseHeadersSectionCB = new System.Windows.Forms.CheckBox();
            this.MatchKeywordInRequestBodySectionCB = new System.Windows.Forms.CheckBox();
            this.MatchKeywordInRequestHeadersSectionCB = new System.Windows.Forms.CheckBox();
            this.MatchKeywordInAllSectionsCB = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MatchKeywordTB = new System.Windows.Forms.TextBox();
            this.MatchKeywordCB = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.MatchRequestUrlTypeCombo = new System.Windows.Forms.ComboBox();
            this.MatchRequestUrlKeywordTB = new System.Windows.Forms.TextBox();
            this.MatchRequestUrlCB = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchTypeNewRB = new System.Windows.Forms.RadioButton();
            this.SearchTypeChainedRB = new System.Windows.Forms.RadioButton();
            this.LogSourceAndIdsPanel = new System.Windows.Forms.Panel();
            this.LogIdsRangeBelowTB = new System.Windows.Forms.TextBox();
            this.SelectLogSourceCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LogIdsRangeAnyRB = new System.Windows.Forms.RadioButton();
            this.LogIdsRangeAboveCB = new System.Windows.Forms.CheckBox();
            this.LogIdsRangeBelowCB = new System.Windows.Forms.CheckBox();
            this.LogIdsRangeCustomRB = new System.Windows.Forms.RadioButton();
            this.LogIdsRangeAboveTB = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.MatchFileExtensionsMinusRB = new System.Windows.Forms.RadioButton();
            this.MatchFileExtensionsPlusRB = new System.Windows.Forms.RadioButton();
            this.MatchFileExtensionsTB = new System.Windows.Forms.TextBox();
            this.MatchFileExtensionsCB = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MatchHostNamesMinusRB = new System.Windows.Forms.RadioButton();
            this.MatchHostNamesPlusRB = new System.Windows.Forms.RadioButton();
            this.MatchHostNamesTB = new System.Windows.Forms.TextBox();
            this.MatchHostNamesCB = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MatchResponseCodesMinusRB = new System.Windows.Forms.RadioButton();
            this.MatchResponseCodesPlusRB = new System.Windows.Forms.RadioButton();
            this.MatchResponseCodesTB = new System.Windows.Forms.TextBox();
            this.MatchResponseCodesCB = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MatchRequestMethodsMinusRB = new System.Windows.Forms.RadioButton();
            this.MatchRequestMethodsPlusRB = new System.Windows.Forms.RadioButton();
            this.MatchRequestMethodsTB = new System.Windows.Forms.TextBox();
            this.MatchRequestMethodsCB = new System.Windows.Forms.CheckBox();
            this.BaseSplit = new System.Windows.Forms.SplitContainer();
            this.DoDiffBtn = new System.Windows.Forms.Button();
            this.ClickActionDisplayLogRB = new System.Windows.Forms.RadioButton();
            this.ClickActionSelectLogRB = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.SearchResultsCountLbl = new System.Windows.Forms.Label();
            this.SearchProgressBar = new System.Windows.Forms.ProgressBar();
            this.LoadLogProgressBar = new System.Windows.Forms.ProgressBar();
            this.LogDisplayTabs = new System.Windows.Forms.TabControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.tabPage29 = new System.Windows.Forms.TabPage();
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.SearchFilterTab = new System.Windows.Forms.TabPage();
            this.SearchFilterPanel = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Step0StatusTB = new System.Windows.Forms.TextBox();
            this.SearchResultsTab = new System.Windows.Forms.TabPage();
            this.RequestView = new IronWASP.RequestView();
            this.ResponseView = new IronWASP.ResponseView();
            ((System.ComponentModel.ISupportInitialize)(this.LogGrid)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.LogSourceAndIdsPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.BaseSplit.Panel1.SuspendLayout();
            this.BaseSplit.Panel2.SuspendLayout();
            this.BaseSplit.SuspendLayout();
            this.LogDisplayTabs.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage29.SuspendLayout();
            this.BaseTabs.SuspendLayout();
            this.SearchFilterTab.SuspendLayout();
            this.SearchFilterPanel.SuspendLayout();
            this.SearchResultsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogGrid
            // 
            this.LogGrid.AllowUserToAddRows = false;
            this.LogGrid.AllowUserToDeleteRows = false;
            this.LogGrid.AllowUserToOrderColumns = true;
            this.LogGrid.AllowUserToResizeRows = false;
            this.LogGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.LogGrid.BackgroundColor = System.Drawing.Color.White;
            this.LogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.LogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.LogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectClmn,
            this.IDClmn,
            this.HostNameSelectClmn,
            this.MethodClmn,
            this.URLClmn,
            this.FileClmn,
            this.SSLClmn,
            this.ParametersClmn,
            this.CodeClmn,
            this.LengthClmn,
            this.ClmnMIME,
            this.SetCookieClmn,
            this.NotesClmn});
            this.LogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.LogGrid.GridColor = System.Drawing.Color.White;
            this.LogGrid.Location = new System.Drawing.Point(0, 66);
            this.LogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.LogGrid.MultiSelect = false;
            this.LogGrid.Name = "LogGrid";
            this.LogGrid.ReadOnly = true;
            this.LogGrid.RowHeadersVisible = false;
            this.LogGrid.RowHeadersWidth = 10;
            this.LogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LogGrid.Size = new System.Drawing.Size(876, 249);
            this.LogGrid.TabIndex = 3;
            this.LogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LogGrid_CellClick);
            this.LogGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.LogGrid_RowsRemoved);
            // 
            // SelectClmn
            // 
            this.SelectClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SelectClmn.HeaderText = "SELECT";
            this.SelectClmn.Name = "SelectClmn";
            this.SelectClmn.ReadOnly = true;
            this.SelectClmn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectClmn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SelectClmn.Width = 60;
            // 
            // IDClmn
            // 
            this.IDClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IDClmn.HeaderText = "ID";
            this.IDClmn.MinimumWidth = 50;
            this.IDClmn.Name = "IDClmn";
            this.IDClmn.ReadOnly = true;
            this.IDClmn.Width = 50;
            // 
            // HostNameSelectClmn
            // 
            this.HostNameSelectClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.HostNameSelectClmn.HeaderText = "HOSTNAME";
            this.HostNameSelectClmn.Name = "HostNameSelectClmn";
            this.HostNameSelectClmn.ReadOnly = true;
            this.HostNameSelectClmn.Width = 120;
            // 
            // MethodClmn
            // 
            this.MethodClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MethodClmn.HeaderText = "METHOD";
            this.MethodClmn.Name = "MethodClmn";
            this.MethodClmn.ReadOnly = true;
            this.MethodClmn.Width = 60;
            // 
            // URLClmn
            // 
            this.URLClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.URLClmn.HeaderText = "URL";
            this.URLClmn.MinimumWidth = 150;
            this.URLClmn.Name = "URLClmn";
            this.URLClmn.ReadOnly = true;
            // 
            // FileClmn
            // 
            this.FileClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FileClmn.HeaderText = "FILE";
            this.FileClmn.Name = "FileClmn";
            this.FileClmn.ReadOnly = true;
            this.FileClmn.Width = 40;
            // 
            // SSLClmn
            // 
            this.SSLClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SSLClmn.HeaderText = "SSL";
            this.SSLClmn.Name = "SSLClmn";
            this.SSLClmn.ReadOnly = true;
            this.SSLClmn.Width = 30;
            // 
            // ParametersClmn
            // 
            this.ParametersClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ParametersClmn.HeaderText = "PARAMETERS";
            this.ParametersClmn.Name = "ParametersClmn";
            this.ParametersClmn.ReadOnly = true;
            this.ParametersClmn.Width = 85;
            // 
            // CodeClmn
            // 
            this.CodeClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CodeClmn.HeaderText = "CODE";
            this.CodeClmn.Name = "CodeClmn";
            this.CodeClmn.ReadOnly = true;
            this.CodeClmn.Width = 45;
            // 
            // LengthClmn
            // 
            this.LengthClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LengthClmn.HeaderText = "LENGTH";
            this.LengthClmn.Name = "LengthClmn";
            this.LengthClmn.ReadOnly = true;
            this.LengthClmn.Width = 55;
            // 
            // ClmnMIME
            // 
            this.ClmnMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ClmnMIME.HeaderText = "MIME";
            this.ClmnMIME.Name = "ClmnMIME";
            this.ClmnMIME.ReadOnly = true;
            this.ClmnMIME.Width = 60;
            // 
            // SetCookieClmn
            // 
            this.SetCookieClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SetCookieClmn.HeaderText = "SET-COOKIE";
            this.SetCookieClmn.Name = "SetCookieClmn";
            this.SetCookieClmn.ReadOnly = true;
            this.SetCookieClmn.Width = 80;
            // 
            // NotesClmn
            // 
            this.NotesClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NotesClmn.HeaderText = "NOTES";
            this.NotesClmn.Name = "NotesClmn";
            this.NotesClmn.ReadOnly = true;
            this.NotesClmn.Visible = false;
            this.NotesClmn.Width = 80;
            // 
            // StartTestBtn
            // 
            this.StartTestBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartTestBtn.Enabled = false;
            this.StartTestBtn.Location = new System.Drawing.Point(625, 18);
            this.StartTestBtn.Name = "StartTestBtn";
            this.StartTestBtn.Size = new System.Drawing.Size(121, 44);
            this.StartTestBtn.TabIndex = 4;
            this.StartTestBtn.Text = "Test Selected Sessions";
            this.StartTestBtn.UseVisualStyleBackColor = true;
            this.StartTestBtn.Click += new System.EventHandler(this.StartTestBtn_Click);
            // 
            // RunModulesBtn
            // 
            this.RunModulesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RunModulesBtn.Enabled = false;
            this.RunModulesBtn.Location = new System.Drawing.Point(752, 18);
            this.RunModulesBtn.Name = "RunModulesBtn";
            this.RunModulesBtn.Size = new System.Drawing.Size(119, 44);
            this.RunModulesBtn.TabIndex = 6;
            this.RunModulesBtn.Text = "Run Modules on Selected Sessions";
            this.RunModulesBtn.UseVisualStyleBackColor = true;
            this.RunModulesBtn.Click += new System.EventHandler(this.RunModulesBtn_Click);
            // 
            // StartScanBtn
            // 
            this.StartScanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartScanBtn.Enabled = false;
            this.StartScanBtn.Location = new System.Drawing.Point(511, 18);
            this.StartScanBtn.Name = "StartScanBtn";
            this.StartScanBtn.Size = new System.Drawing.Size(108, 43);
            this.StartScanBtn.TabIndex = 7;
            this.StartScanBtn.Text = "Scan Selected Requests";
            this.StartScanBtn.UseVisualStyleBackColor = true;
            this.StartScanBtn.Click += new System.EventHandler(this.StartScanBtn_Click);
            // 
            // SelectAllCB
            // 
            this.SelectAllCB.AutoSize = true;
            this.SelectAllCB.Location = new System.Drawing.Point(8, 42);
            this.SelectAllCB.Name = "SelectAllCB";
            this.SelectAllCB.Size = new System.Drawing.Size(94, 17);
            this.SelectAllCB.TabIndex = 8;
            this.SelectAllCB.Text = "Select all rows";
            this.SelectAllCB.UseVisualStyleBackColor = true;
            this.SelectAllCB.Click += new System.EventHandler(this.SelectAllCB_Click);
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.MatchKeywordInResponseBodySectionCB);
            this.panel8.Controls.Add(this.MatchKeywordInResponseHeadersSectionCB);
            this.panel8.Controls.Add(this.MatchKeywordInRequestBodySectionCB);
            this.panel8.Controls.Add(this.MatchKeywordInRequestHeadersSectionCB);
            this.panel8.Controls.Add(this.MatchKeywordInAllSectionsCB);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.MatchKeywordTB);
            this.panel8.Controls.Add(this.MatchKeywordCB);
            this.panel8.Location = new System.Drawing.Point(14, 219);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(852, 52);
            this.panel8.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(595, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(247, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "(Binary Request and Response bodies are ignored)";
            // 
            // MatchKeywordInResponseBodySectionCB
            // 
            this.MatchKeywordInResponseBodySectionCB.AutoSize = true;
            this.MatchKeywordInResponseBodySectionCB.Checked = true;
            this.MatchKeywordInResponseBodySectionCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MatchKeywordInResponseBodySectionCB.Enabled = false;
            this.MatchKeywordInResponseBodySectionCB.Location = new System.Drawing.Point(488, 32);
            this.MatchKeywordInResponseBodySectionCB.Name = "MatchKeywordInResponseBodySectionCB";
            this.MatchKeywordInResponseBodySectionCB.Size = new System.Drawing.Size(101, 17);
            this.MatchKeywordInResponseBodySectionCB.TabIndex = 18;
            this.MatchKeywordInResponseBodySectionCB.Text = "Response Body";
            this.MatchKeywordInResponseBodySectionCB.UseVisualStyleBackColor = true;
            this.MatchKeywordInResponseBodySectionCB.Click += new System.EventHandler(this.MatchKeywordInResponseBodySectionCB_Click);
            // 
            // MatchKeywordInResponseHeadersSectionCB
            // 
            this.MatchKeywordInResponseHeadersSectionCB.AutoSize = true;
            this.MatchKeywordInResponseHeadersSectionCB.Checked = true;
            this.MatchKeywordInResponseHeadersSectionCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MatchKeywordInResponseHeadersSectionCB.Enabled = false;
            this.MatchKeywordInResponseHeadersSectionCB.Location = new System.Drawing.Point(365, 32);
            this.MatchKeywordInResponseHeadersSectionCB.Name = "MatchKeywordInResponseHeadersSectionCB";
            this.MatchKeywordInResponseHeadersSectionCB.Size = new System.Drawing.Size(117, 17);
            this.MatchKeywordInResponseHeadersSectionCB.TabIndex = 17;
            this.MatchKeywordInResponseHeadersSectionCB.Text = "Response Headers";
            this.MatchKeywordInResponseHeadersSectionCB.UseVisualStyleBackColor = true;
            this.MatchKeywordInResponseHeadersSectionCB.Click += new System.EventHandler(this.MatchKeywordInResponseHeadersSectionCB_Click);
            // 
            // MatchKeywordInRequestBodySectionCB
            // 
            this.MatchKeywordInRequestBodySectionCB.AutoSize = true;
            this.MatchKeywordInRequestBodySectionCB.Checked = true;
            this.MatchKeywordInRequestBodySectionCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MatchKeywordInRequestBodySectionCB.Enabled = false;
            this.MatchKeywordInRequestBodySectionCB.Location = new System.Drawing.Point(266, 32);
            this.MatchKeywordInRequestBodySectionCB.Name = "MatchKeywordInRequestBodySectionCB";
            this.MatchKeywordInRequestBodySectionCB.Size = new System.Drawing.Size(93, 17);
            this.MatchKeywordInRequestBodySectionCB.TabIndex = 16;
            this.MatchKeywordInRequestBodySectionCB.Text = "Request Body";
            this.MatchKeywordInRequestBodySectionCB.UseVisualStyleBackColor = true;
            this.MatchKeywordInRequestBodySectionCB.Click += new System.EventHandler(this.MatchKeywordInRequestBodySectionCB_Click);
            // 
            // MatchKeywordInRequestHeadersSectionCB
            // 
            this.MatchKeywordInRequestHeadersSectionCB.AutoSize = true;
            this.MatchKeywordInRequestHeadersSectionCB.Checked = true;
            this.MatchKeywordInRequestHeadersSectionCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MatchKeywordInRequestHeadersSectionCB.Enabled = false;
            this.MatchKeywordInRequestHeadersSectionCB.Location = new System.Drawing.Point(151, 32);
            this.MatchKeywordInRequestHeadersSectionCB.Name = "MatchKeywordInRequestHeadersSectionCB";
            this.MatchKeywordInRequestHeadersSectionCB.Size = new System.Drawing.Size(109, 17);
            this.MatchKeywordInRequestHeadersSectionCB.TabIndex = 15;
            this.MatchKeywordInRequestHeadersSectionCB.Text = "Request Headers";
            this.MatchKeywordInRequestHeadersSectionCB.UseVisualStyleBackColor = true;
            this.MatchKeywordInRequestHeadersSectionCB.Click += new System.EventHandler(this.MatchKeywordInRequestHeadersSectionCB_Click);
            // 
            // MatchKeywordInAllSectionsCB
            // 
            this.MatchKeywordInAllSectionsCB.AutoSize = true;
            this.MatchKeywordInAllSectionsCB.Checked = true;
            this.MatchKeywordInAllSectionsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MatchKeywordInAllSectionsCB.Enabled = false;
            this.MatchKeywordInAllSectionsCB.Location = new System.Drawing.Point(108, 32);
            this.MatchKeywordInAllSectionsCB.Name = "MatchKeywordInAllSectionsCB";
            this.MatchKeywordInAllSectionsCB.Size = new System.Drawing.Size(37, 17);
            this.MatchKeywordInAllSectionsCB.TabIndex = 14;
            this.MatchKeywordInAllSectionsCB.Text = "All";
            this.MatchKeywordInAllSectionsCB.UseVisualStyleBackColor = true;
            this.MatchKeywordInAllSectionsCB.Click += new System.EventHandler(this.MatchKeywordInAllSectionsCB_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Match in Sections:";
            // 
            // MatchKeywordTB
            // 
            this.MatchKeywordTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchKeywordTB.Enabled = false;
            this.MatchKeywordTB.Location = new System.Drawing.Point(114, 3);
            this.MatchKeywordTB.Name = "MatchKeywordTB";
            this.MatchKeywordTB.Size = new System.Drawing.Size(733, 20);
            this.MatchKeywordTB.TabIndex = 11;
            this.MatchKeywordTB.TextChanged += new System.EventHandler(this.MatchKeywordTB_TextChanged);
            // 
            // MatchKeywordCB
            // 
            this.MatchKeywordCB.AutoSize = true;
            this.MatchKeywordCB.Location = new System.Drawing.Point(8, 6);
            this.MatchKeywordCB.Name = "MatchKeywordCB";
            this.MatchKeywordCB.Size = new System.Drawing.Size(103, 17);
            this.MatchKeywordCB.TabIndex = 12;
            this.MatchKeywordCB.Text = "Match Keyword:";
            this.MatchKeywordCB.UseVisualStyleBackColor = true;
            this.MatchKeywordCB.CheckedChanged += new System.EventHandler(this.MatchKeywordCB_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.MatchRequestUrlTypeCombo);
            this.panel7.Controls.Add(this.MatchRequestUrlKeywordTB);
            this.panel7.Controls.Add(this.MatchRequestUrlCB);
            this.panel7.Location = new System.Drawing.Point(14, 277);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(852, 27);
            this.panel7.TabIndex = 19;
            // 
            // MatchRequestUrlTypeCombo
            // 
            this.MatchRequestUrlTypeCombo.Enabled = false;
            this.MatchRequestUrlTypeCombo.FormattingEnabled = true;
            this.MatchRequestUrlTypeCombo.Items.AddRange(new object[] {
            "contains",
            "does not contain",
            "starts with",
            "ends with",
            "equals",
            "does not equal"});
            this.MatchRequestUrlTypeCombo.Location = new System.Drawing.Point(116, 2);
            this.MatchRequestUrlTypeCombo.Name = "MatchRequestUrlTypeCombo";
            this.MatchRequestUrlTypeCombo.Size = new System.Drawing.Size(176, 21);
            this.MatchRequestUrlTypeCombo.TabIndex = 16;
            this.MatchRequestUrlTypeCombo.SelectedIndexChanged += new System.EventHandler(this.MatchRequestUrlTypeCombo_SelectedIndexChanged);
            this.MatchRequestUrlTypeCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MatchRequestUrlTypeCombo_KeyPress);
            // 
            // MatchRequestUrlKeywordTB
            // 
            this.MatchRequestUrlKeywordTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchRequestUrlKeywordTB.Enabled = false;
            this.MatchRequestUrlKeywordTB.Location = new System.Drawing.Point(319, 3);
            this.MatchRequestUrlKeywordTB.Name = "MatchRequestUrlKeywordTB";
            this.MatchRequestUrlKeywordTB.Size = new System.Drawing.Size(528, 20);
            this.MatchRequestUrlKeywordTB.TabIndex = 11;
            this.MatchRequestUrlKeywordTB.TextChanged += new System.EventHandler(this.MatchRequestUrlKeywordTB_TextChanged);
            // 
            // MatchRequestUrlCB
            // 
            this.MatchRequestUrlCB.AutoSize = true;
            this.MatchRequestUrlCB.Location = new System.Drawing.Point(8, 6);
            this.MatchRequestUrlCB.Name = "MatchRequestUrlCB";
            this.MatchRequestUrlCB.Size = new System.Drawing.Size(91, 17);
            this.MatchRequestUrlCB.TabIndex = 12;
            this.MatchRequestUrlCB.Text = "Request URL";
            this.MatchRequestUrlCB.UseVisualStyleBackColor = true;
            this.MatchRequestUrlCB.CheckedChanged += new System.EventHandler(this.MatchRequestUrlCB_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.SearchBtn);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.SearchTypeNewRB);
            this.panel6.Controls.Add(this.SearchTypeChainedRB);
            this.panel6.Location = new System.Drawing.Point(14, 152);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(852, 27);
            this.panel6.TabIndex = 18;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(718, 2);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(118, 23);
            this.SearchBtn.TabIndex = 9;
            this.SearchBtn.Text = "Search with this Filter";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Search Type:";
            // 
            // SearchTypeNewRB
            // 
            this.SearchTypeNewRB.AutoSize = true;
            this.SearchTypeNewRB.Checked = true;
            this.SearchTypeNewRB.Location = new System.Drawing.Point(83, 5);
            this.SearchTypeNewRB.Name = "SearchTypeNewRB";
            this.SearchTypeNewRB.Size = new System.Drawing.Size(212, 17);
            this.SearchTypeNewRB.TabIndex = 3;
            this.SearchTypeNewRB.TabStop = true;
            this.SearchTypeNewRB.Text = "New Search (searches entire database)";
            this.SearchTypeNewRB.UseVisualStyleBackColor = true;
            this.SearchTypeNewRB.CheckedChanged += new System.EventHandler(this.SearchTypeNewRB_CheckedChanged);
            // 
            // SearchTypeChainedRB
            // 
            this.SearchTypeChainedRB.AutoSize = true;
            this.SearchTypeChainedRB.Location = new System.Drawing.Point(305, 5);
            this.SearchTypeChainedRB.Name = "SearchTypeChainedRB";
            this.SearchTypeChainedRB.Size = new System.Drawing.Size(364, 17);
            this.SearchTypeChainedRB.TabIndex = 8;
            this.SearchTypeChainedRB.Text = "Chained Search (searches only within the results of the previous search)";
            this.SearchTypeChainedRB.UseVisualStyleBackColor = true;
            this.SearchTypeChainedRB.CheckedChanged += new System.EventHandler(this.SearchTypeChainedRB_CheckedChanged);
            // 
            // LogSourceAndIdsPanel
            // 
            this.LogSourceAndIdsPanel.Controls.Add(this.LogIdsRangeBelowTB);
            this.LogSourceAndIdsPanel.Controls.Add(this.SelectLogSourceCombo);
            this.LogSourceAndIdsPanel.Controls.Add(this.label1);
            this.LogSourceAndIdsPanel.Controls.Add(this.label2);
            this.LogSourceAndIdsPanel.Controls.Add(this.LogIdsRangeAnyRB);
            this.LogSourceAndIdsPanel.Controls.Add(this.LogIdsRangeAboveCB);
            this.LogSourceAndIdsPanel.Controls.Add(this.LogIdsRangeBelowCB);
            this.LogSourceAndIdsPanel.Controls.Add(this.LogIdsRangeCustomRB);
            this.LogSourceAndIdsPanel.Controls.Add(this.LogIdsRangeAboveTB);
            this.LogSourceAndIdsPanel.Location = new System.Drawing.Point(14, 186);
            this.LogSourceAndIdsPanel.Name = "LogSourceAndIdsPanel";
            this.LogSourceAndIdsPanel.Size = new System.Drawing.Size(852, 27);
            this.LogSourceAndIdsPanel.TabIndex = 17;
            // 
            // LogIdsRangeBelowTB
            // 
            this.LogIdsRangeBelowTB.Enabled = false;
            this.LogIdsRangeBelowTB.Location = new System.Drawing.Point(746, 3);
            this.LogIdsRangeBelowTB.Name = "LogIdsRangeBelowTB";
            this.LogIdsRangeBelowTB.Size = new System.Drawing.Size(90, 20);
            this.LogIdsRangeBelowTB.TabIndex = 7;
            this.LogIdsRangeBelowTB.Text = "200";
            this.LogIdsRangeBelowTB.TextChanged += new System.EventHandler(this.LogIdsRangeBelowTB_TextChanged);
            // 
            // SelectLogSourceCombo
            // 
            this.SelectLogSourceCombo.FormattingEnabled = true;
            this.SelectLogSourceCombo.Location = new System.Drawing.Point(100, 3);
            this.SelectLogSourceCombo.Name = "SelectLogSourceCombo";
            this.SelectLogSourceCombo.Size = new System.Drawing.Size(176, 21);
            this.SelectLogSourceCombo.TabIndex = 0;
            this.SelectLogSourceCombo.SelectedIndexChanged += new System.EventHandler(this.SelectLogSourceCombo_SelectedIndexChanged);
            this.SelectLogSourceCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectLogSourceCombo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Log Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Log IDs:";
            // 
            // LogIdsRangeAnyRB
            // 
            this.LogIdsRangeAnyRB.AutoSize = true;
            this.LogIdsRangeAnyRB.Checked = true;
            this.LogIdsRangeAnyRB.Location = new System.Drawing.Point(355, 5);
            this.LogIdsRangeAnyRB.Name = "LogIdsRangeAnyRB";
            this.LogIdsRangeAnyRB.Size = new System.Drawing.Size(43, 17);
            this.LogIdsRangeAnyRB.TabIndex = 3;
            this.LogIdsRangeAnyRB.TabStop = true;
            this.LogIdsRangeAnyRB.Text = "Any";
            this.LogIdsRangeAnyRB.UseVisualStyleBackColor = true;
            this.LogIdsRangeAnyRB.CheckedChanged += new System.EventHandler(this.LogIdsRangeAnyRB_CheckedChanged);
            // 
            // LogIdsRangeAboveCB
            // 
            this.LogIdsRangeAboveCB.AutoSize = true;
            this.LogIdsRangeAboveCB.Enabled = false;
            this.LogIdsRangeAboveCB.Location = new System.Drawing.Point(503, 5);
            this.LogIdsRangeAboveCB.Name = "LogIdsRangeAboveCB";
            this.LogIdsRangeAboveCB.Size = new System.Drawing.Size(57, 17);
            this.LogIdsRangeAboveCB.TabIndex = 9;
            this.LogIdsRangeAboveCB.Text = "Above";
            this.LogIdsRangeAboveCB.UseVisualStyleBackColor = true;
            this.LogIdsRangeAboveCB.CheckedChanged += new System.EventHandler(this.LogIdsRangeAboveCB_CheckedChanged);
            // 
            // LogIdsRangeBelowCB
            // 
            this.LogIdsRangeBelowCB.AutoSize = true;
            this.LogIdsRangeBelowCB.Enabled = false;
            this.LogIdsRangeBelowCB.Location = new System.Drawing.Point(685, 5);
            this.LogIdsRangeBelowCB.Name = "LogIdsRangeBelowCB";
            this.LogIdsRangeBelowCB.Size = new System.Drawing.Size(55, 17);
            this.LogIdsRangeBelowCB.TabIndex = 10;
            this.LogIdsRangeBelowCB.Text = "Below";
            this.LogIdsRangeBelowCB.UseVisualStyleBackColor = true;
            this.LogIdsRangeBelowCB.CheckedChanged += new System.EventHandler(this.LogIdsRangeBelowCB_CheckedChanged);
            // 
            // LogIdsRangeCustomRB
            // 
            this.LogIdsRangeCustomRB.AutoSize = true;
            this.LogIdsRangeCustomRB.Location = new System.Drawing.Point(404, 5);
            this.LogIdsRangeCustomRB.Name = "LogIdsRangeCustomRB";
            this.LogIdsRangeCustomRB.Size = new System.Drawing.Size(93, 17);
            this.LogIdsRangeCustomRB.TabIndex = 8;
            this.LogIdsRangeCustomRB.Text = "Only in range -";
            this.LogIdsRangeCustomRB.UseVisualStyleBackColor = true;
            this.LogIdsRangeCustomRB.CheckedChanged += new System.EventHandler(this.LogIdsRangeCustomRB_CheckedChanged);
            // 
            // LogIdsRangeAboveTB
            // 
            this.LogIdsRangeAboveTB.Enabled = false;
            this.LogIdsRangeAboveTB.Location = new System.Drawing.Point(566, 3);
            this.LogIdsRangeAboveTB.Name = "LogIdsRangeAboveTB";
            this.LogIdsRangeAboveTB.Size = new System.Drawing.Size(90, 20);
            this.LogIdsRangeAboveTB.TabIndex = 5;
            this.LogIdsRangeAboveTB.Text = "100";
            this.LogIdsRangeAboveTB.TextChanged += new System.EventHandler(this.LogIdsRangeAboveTB_TextChanged);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.MatchFileExtensionsMinusRB);
            this.panel4.Controls.Add(this.MatchFileExtensionsPlusRB);
            this.panel4.Controls.Add(this.MatchFileExtensionsTB);
            this.panel4.Controls.Add(this.MatchFileExtensionsCB);
            this.panel4.Location = new System.Drawing.Point(14, 408);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(852, 27);
            this.panel4.TabIndex = 16;
            // 
            // MatchFileExtensionsMinusRB
            // 
            this.MatchFileExtensionsMinusRB.AutoSize = true;
            this.MatchFileExtensionsMinusRB.Checked = true;
            this.MatchFileExtensionsMinusRB.Enabled = false;
            this.MatchFileExtensionsMinusRB.Location = new System.Drawing.Point(287, 5);
            this.MatchFileExtensionsMinusRB.Name = "MatchFileExtensionsMinusRB";
            this.MatchFileExtensionsMinusRB.Size = new System.Drawing.Size(28, 17);
            this.MatchFileExtensionsMinusRB.TabIndex = 15;
            this.MatchFileExtensionsMinusRB.TabStop = true;
            this.MatchFileExtensionsMinusRB.Text = "-";
            this.MatchFileExtensionsMinusRB.UseVisualStyleBackColor = true;
            this.MatchFileExtensionsMinusRB.CheckedChanged += new System.EventHandler(this.MatchFileExtensionsMinusRB_CheckedChanged);
            // 
            // MatchFileExtensionsPlusRB
            // 
            this.MatchFileExtensionsPlusRB.AutoSize = true;
            this.MatchFileExtensionsPlusRB.Enabled = false;
            this.MatchFileExtensionsPlusRB.Location = new System.Drawing.Point(250, 5);
            this.MatchFileExtensionsPlusRB.Name = "MatchFileExtensionsPlusRB";
            this.MatchFileExtensionsPlusRB.Size = new System.Drawing.Size(31, 17);
            this.MatchFileExtensionsPlusRB.TabIndex = 14;
            this.MatchFileExtensionsPlusRB.Text = "+";
            this.MatchFileExtensionsPlusRB.UseVisualStyleBackColor = true;
            this.MatchFileExtensionsPlusRB.CheckedChanged += new System.EventHandler(this.MatchFileExtensionsPlusRB_CheckedChanged);
            // 
            // MatchFileExtensionsTB
            // 
            this.MatchFileExtensionsTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchFileExtensionsTB.Enabled = false;
            this.MatchFileExtensionsTB.Location = new System.Drawing.Point(319, 3);
            this.MatchFileExtensionsTB.Name = "MatchFileExtensionsTB";
            this.MatchFileExtensionsTB.Size = new System.Drawing.Size(528, 20);
            this.MatchFileExtensionsTB.TabIndex = 11;
            this.MatchFileExtensionsTB.Text = "jpg, jpeg, png, gif, ico, pdf, doc, docx, ppt, pptx, xls, xlsx, zip, tar, 7z, exe" +
                ", swf, jar, css, js, html";
            this.MatchFileExtensionsTB.TextChanged += new System.EventHandler(this.MatchFileExtensionsTB_TextChanged);
            // 
            // MatchFileExtensionsCB
            // 
            this.MatchFileExtensionsCB.AutoSize = true;
            this.MatchFileExtensionsCB.Location = new System.Drawing.Point(8, 6);
            this.MatchFileExtensionsCB.Name = "MatchFileExtensionsCB";
            this.MatchFileExtensionsCB.Size = new System.Drawing.Size(207, 17);
            this.MatchFileExtensionsCB.TabIndex = 12;
            this.MatchFileExtensionsCB.Text = "Except these Request File Extensions:";
            this.MatchFileExtensionsCB.UseVisualStyleBackColor = true;
            this.MatchFileExtensionsCB.CheckedChanged += new System.EventHandler(this.MatchFileExtensionsCB_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.MatchHostNamesMinusRB);
            this.panel3.Controls.Add(this.MatchHostNamesPlusRB);
            this.panel3.Controls.Add(this.MatchHostNamesTB);
            this.panel3.Controls.Add(this.MatchHostNamesCB);
            this.panel3.Location = new System.Drawing.Point(14, 375);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(852, 27);
            this.panel3.TabIndex = 15;
            // 
            // MatchHostNamesMinusRB
            // 
            this.MatchHostNamesMinusRB.AutoSize = true;
            this.MatchHostNamesMinusRB.Checked = true;
            this.MatchHostNamesMinusRB.Enabled = false;
            this.MatchHostNamesMinusRB.Location = new System.Drawing.Point(231, 5);
            this.MatchHostNamesMinusRB.Name = "MatchHostNamesMinusRB";
            this.MatchHostNamesMinusRB.Size = new System.Drawing.Size(28, 17);
            this.MatchHostNamesMinusRB.TabIndex = 15;
            this.MatchHostNamesMinusRB.TabStop = true;
            this.MatchHostNamesMinusRB.Text = "-";
            this.MatchHostNamesMinusRB.UseVisualStyleBackColor = true;
            this.MatchHostNamesMinusRB.CheckedChanged += new System.EventHandler(this.MatchHostNamesMinusRB_CheckedChanged);
            // 
            // MatchHostNamesPlusRB
            // 
            this.MatchHostNamesPlusRB.AutoSize = true;
            this.MatchHostNamesPlusRB.Enabled = false;
            this.MatchHostNamesPlusRB.Location = new System.Drawing.Point(194, 5);
            this.MatchHostNamesPlusRB.Name = "MatchHostNamesPlusRB";
            this.MatchHostNamesPlusRB.Size = new System.Drawing.Size(31, 17);
            this.MatchHostNamesPlusRB.TabIndex = 14;
            this.MatchHostNamesPlusRB.Text = "+";
            this.MatchHostNamesPlusRB.UseVisualStyleBackColor = true;
            this.MatchHostNamesPlusRB.CheckedChanged += new System.EventHandler(this.MatchHostNamesPlusRB_CheckedChanged);
            // 
            // MatchHostNamesTB
            // 
            this.MatchHostNamesTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchHostNamesTB.Enabled = false;
            this.MatchHostNamesTB.Location = new System.Drawing.Point(265, 3);
            this.MatchHostNamesTB.Name = "MatchHostNamesTB";
            this.MatchHostNamesTB.Size = new System.Drawing.Size(582, 20);
            this.MatchHostNamesTB.TabIndex = 11;
            this.MatchHostNamesTB.Text = "www.google.com, www.facebook.com, www.twitter.com";
            this.MatchHostNamesTB.TextChanged += new System.EventHandler(this.MatchHostNamesTB_TextChanged);
            // 
            // MatchHostNamesCB
            // 
            this.MatchHostNamesCB.AutoSize = true;
            this.MatchHostNamesCB.Location = new System.Drawing.Point(8, 6);
            this.MatchHostNamesCB.Name = "MatchHostNamesCB";
            this.MatchHostNamesCB.Size = new System.Drawing.Size(147, 17);
            this.MatchHostNamesCB.TabIndex = 12;
            this.MatchHostNamesCB.Text = "Except these Hostnames:";
            this.MatchHostNamesCB.UseVisualStyleBackColor = true;
            this.MatchHostNamesCB.CheckedChanged += new System.EventHandler(this.MatchHostNamesCB_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.MatchResponseCodesMinusRB);
            this.panel2.Controls.Add(this.MatchResponseCodesPlusRB);
            this.panel2.Controls.Add(this.MatchResponseCodesTB);
            this.panel2.Controls.Add(this.MatchResponseCodesCB);
            this.panel2.Location = new System.Drawing.Point(14, 341);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 27);
            this.panel2.TabIndex = 14;
            // 
            // MatchResponseCodesMinusRB
            // 
            this.MatchResponseCodesMinusRB.AutoSize = true;
            this.MatchResponseCodesMinusRB.Enabled = false;
            this.MatchResponseCodesMinusRB.Location = new System.Drawing.Point(231, 5);
            this.MatchResponseCodesMinusRB.Name = "MatchResponseCodesMinusRB";
            this.MatchResponseCodesMinusRB.Size = new System.Drawing.Size(28, 17);
            this.MatchResponseCodesMinusRB.TabIndex = 15;
            this.MatchResponseCodesMinusRB.Text = "-";
            this.MatchResponseCodesMinusRB.UseVisualStyleBackColor = true;
            this.MatchResponseCodesMinusRB.CheckedChanged += new System.EventHandler(this.MatchResponseCodesMinusRB_CheckedChanged);
            // 
            // MatchResponseCodesPlusRB
            // 
            this.MatchResponseCodesPlusRB.AutoSize = true;
            this.MatchResponseCodesPlusRB.Checked = true;
            this.MatchResponseCodesPlusRB.Enabled = false;
            this.MatchResponseCodesPlusRB.Location = new System.Drawing.Point(194, 5);
            this.MatchResponseCodesPlusRB.Name = "MatchResponseCodesPlusRB";
            this.MatchResponseCodesPlusRB.Size = new System.Drawing.Size(31, 17);
            this.MatchResponseCodesPlusRB.TabIndex = 14;
            this.MatchResponseCodesPlusRB.TabStop = true;
            this.MatchResponseCodesPlusRB.Text = "+";
            this.MatchResponseCodesPlusRB.UseVisualStyleBackColor = true;
            this.MatchResponseCodesPlusRB.CheckedChanged += new System.EventHandler(this.MatchResponseCodesPlusRB_CheckedChanged);
            // 
            // MatchResponseCodesTB
            // 
            this.MatchResponseCodesTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchResponseCodesTB.Enabled = false;
            this.MatchResponseCodesTB.Location = new System.Drawing.Point(265, 3);
            this.MatchResponseCodesTB.Name = "MatchResponseCodesTB";
            this.MatchResponseCodesTB.Size = new System.Drawing.Size(582, 20);
            this.MatchResponseCodesTB.TabIndex = 11;
            this.MatchResponseCodesTB.Text = "200, 301, 302";
            this.MatchResponseCodesTB.TextChanged += new System.EventHandler(this.MatchResponseCodesTB_TextChanged);
            // 
            // MatchResponseCodesCB
            // 
            this.MatchResponseCodesCB.AutoSize = true;
            this.MatchResponseCodesCB.Location = new System.Drawing.Point(8, 6);
            this.MatchResponseCodesCB.Name = "MatchResponseCodesCB";
            this.MatchResponseCodesCB.Size = new System.Drawing.Size(163, 17);
            this.MatchResponseCodesCB.TabIndex = 12;
            this.MatchResponseCodesCB.Text = "Only these Response Codes:";
            this.MatchResponseCodesCB.UseVisualStyleBackColor = true;
            this.MatchResponseCodesCB.CheckedChanged += new System.EventHandler(this.MatchResponseCodesCB_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.MatchRequestMethodsMinusRB);
            this.panel1.Controls.Add(this.MatchRequestMethodsPlusRB);
            this.panel1.Controls.Add(this.MatchRequestMethodsTB);
            this.panel1.Controls.Add(this.MatchRequestMethodsCB);
            this.panel1.Location = new System.Drawing.Point(14, 308);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 27);
            this.panel1.TabIndex = 13;
            // 
            // MatchRequestMethodsMinusRB
            // 
            this.MatchRequestMethodsMinusRB.AutoSize = true;
            this.MatchRequestMethodsMinusRB.Enabled = false;
            this.MatchRequestMethodsMinusRB.Location = new System.Drawing.Point(231, 5);
            this.MatchRequestMethodsMinusRB.Name = "MatchRequestMethodsMinusRB";
            this.MatchRequestMethodsMinusRB.Size = new System.Drawing.Size(28, 17);
            this.MatchRequestMethodsMinusRB.TabIndex = 15;
            this.MatchRequestMethodsMinusRB.Text = "-";
            this.MatchRequestMethodsMinusRB.UseVisualStyleBackColor = true;
            this.MatchRequestMethodsMinusRB.CheckedChanged += new System.EventHandler(this.MatchRequestMethodsMinusRB_CheckedChanged);
            // 
            // MatchRequestMethodsPlusRB
            // 
            this.MatchRequestMethodsPlusRB.AutoSize = true;
            this.MatchRequestMethodsPlusRB.Checked = true;
            this.MatchRequestMethodsPlusRB.Enabled = false;
            this.MatchRequestMethodsPlusRB.Location = new System.Drawing.Point(194, 5);
            this.MatchRequestMethodsPlusRB.Name = "MatchRequestMethodsPlusRB";
            this.MatchRequestMethodsPlusRB.Size = new System.Drawing.Size(31, 17);
            this.MatchRequestMethodsPlusRB.TabIndex = 14;
            this.MatchRequestMethodsPlusRB.TabStop = true;
            this.MatchRequestMethodsPlusRB.Text = "+";
            this.MatchRequestMethodsPlusRB.UseVisualStyleBackColor = true;
            this.MatchRequestMethodsPlusRB.CheckedChanged += new System.EventHandler(this.MatchRequestMethodsPlusRB_CheckedChanged);
            // 
            // MatchRequestMethodsTB
            // 
            this.MatchRequestMethodsTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchRequestMethodsTB.Enabled = false;
            this.MatchRequestMethodsTB.Location = new System.Drawing.Point(265, 3);
            this.MatchRequestMethodsTB.Name = "MatchRequestMethodsTB";
            this.MatchRequestMethodsTB.Size = new System.Drawing.Size(582, 20);
            this.MatchRequestMethodsTB.TabIndex = 11;
            this.MatchRequestMethodsTB.Text = "GET, POST";
            this.MatchRequestMethodsTB.TextChanged += new System.EventHandler(this.MatchRequestMethodsTB_TextChanged);
            // 
            // MatchRequestMethodsCB
            // 
            this.MatchRequestMethodsCB.AutoSize = true;
            this.MatchRequestMethodsCB.Location = new System.Drawing.Point(8, 6);
            this.MatchRequestMethodsCB.Name = "MatchRequestMethodsCB";
            this.MatchRequestMethodsCB.Size = new System.Drawing.Size(166, 17);
            this.MatchRequestMethodsCB.TabIndex = 12;
            this.MatchRequestMethodsCB.Text = "Only these Request Methods:";
            this.MatchRequestMethodsCB.UseVisualStyleBackColor = true;
            this.MatchRequestMethodsCB.CheckedChanged += new System.EventHandler(this.MatchRequestMethodsCB_CheckedChanged);
            // 
            // BaseSplit
            // 
            this.BaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseSplit.Location = new System.Drawing.Point(0, 0);
            this.BaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.BaseSplit.Name = "BaseSplit";
            this.BaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // BaseSplit.Panel1
            // 
            this.BaseSplit.Panel1.Controls.Add(this.DoDiffBtn);
            this.BaseSplit.Panel1.Controls.Add(this.ClickActionDisplayLogRB);
            this.BaseSplit.Panel1.Controls.Add(this.ClickActionSelectLogRB);
            this.BaseSplit.Panel1.Controls.Add(this.label7);
            this.BaseSplit.Panel1.Controls.Add(this.SearchResultsCountLbl);
            this.BaseSplit.Panel1.Controls.Add(this.SearchProgressBar);
            this.BaseSplit.Panel1.Controls.Add(this.LogGrid);
            this.BaseSplit.Panel1.Controls.Add(this.StartScanBtn);
            this.BaseSplit.Panel1.Controls.Add(this.StartTestBtn);
            this.BaseSplit.Panel1.Controls.Add(this.SelectAllCB);
            this.BaseSplit.Panel1.Controls.Add(this.RunModulesBtn);
            // 
            // BaseSplit.Panel2
            // 
            this.BaseSplit.Panel2.Controls.Add(this.LoadLogProgressBar);
            this.BaseSplit.Panel2.Controls.Add(this.LogDisplayTabs);
            this.BaseSplit.Size = new System.Drawing.Size(876, 536);
            this.BaseSplit.SplitterDistance = 315;
            this.BaseSplit.SplitterWidth = 2;
            this.BaseSplit.TabIndex = 10;
            // 
            // DoDiffBtn
            // 
            this.DoDiffBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DoDiffBtn.Enabled = false;
            this.DoDiffBtn.Location = new System.Drawing.Point(408, 20);
            this.DoDiffBtn.Name = "DoDiffBtn";
            this.DoDiffBtn.Size = new System.Drawing.Size(97, 40);
            this.DoDiffBtn.TabIndex = 39;
            this.DoDiffBtn.Text = "Diff Selected Sessions";
            this.DoDiffBtn.UseVisualStyleBackColor = true;
            this.DoDiffBtn.Click += new System.EventHandler(this.DoDiffBtn_Click);
            // 
            // ClickActionDisplayLogRB
            // 
            this.ClickActionDisplayLogRB.AutoSize = true;
            this.ClickActionDisplayLogRB.Checked = true;
            this.ClickActionDisplayLogRB.Location = new System.Drawing.Point(183, 41);
            this.ClickActionDisplayLogRB.Name = "ClickActionDisplayLogRB";
            this.ClickActionDisplayLogRB.Size = new System.Drawing.Size(80, 17);
            this.ClickActionDisplayLogRB.TabIndex = 38;
            this.ClickActionDisplayLogRB.TabStop = true;
            this.ClickActionDisplayLogRB.Text = "Display Log";
            this.ClickActionDisplayLogRB.UseVisualStyleBackColor = true;
            this.ClickActionDisplayLogRB.CheckedChanged += new System.EventHandler(this.ClickActionDisplayLogRB_CheckedChanged);
            // 
            // ClickActionSelectLogRB
            // 
            this.ClickActionSelectLogRB.AutoSize = true;
            this.ClickActionSelectLogRB.Location = new System.Drawing.Point(269, 41);
            this.ClickActionSelectLogRB.Name = "ClickActionSelectLogRB";
            this.ClickActionSelectLogRB.Size = new System.Drawing.Size(76, 17);
            this.ClickActionSelectLogRB.TabIndex = 37;
            this.ClickActionSelectLogRB.Text = "Select Log";
            this.ClickActionSelectLogRB.UseVisualStyleBackColor = true;
            this.ClickActionSelectLogRB.CheckedChanged += new System.EventHandler(this.ClickActionSelectLogRB_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(108, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Click Action: ";
            // 
            // SearchResultsCountLbl
            // 
            this.SearchResultsCountLbl.AutoSize = true;
            this.SearchResultsCountLbl.Location = new System.Drawing.Point(8, 10);
            this.SearchResultsCountLbl.Name = "SearchResultsCountLbl";
            this.SearchResultsCountLbl.Size = new System.Drawing.Size(122, 13);
            this.SearchResultsCountLbl.TabIndex = 35;
            this.SearchResultsCountLbl.Text = "Search Results Count: 0";
            // 
            // SearchProgressBar
            // 
            this.SearchProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchProgressBar.Location = new System.Drawing.Point(189, 8);
            this.SearchProgressBar.MarqueeAnimationSpeed = 10;
            this.SearchProgressBar.Name = "SearchProgressBar";
            this.SearchProgressBar.Size = new System.Drawing.Size(174, 20);
            this.SearchProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.SearchProgressBar.TabIndex = 34;
            this.SearchProgressBar.Visible = false;
            // 
            // LoadLogProgressBar
            // 
            this.LoadLogProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadLogProgressBar.Location = new System.Drawing.Point(303, 45);
            this.LoadLogProgressBar.MarqueeAnimationSpeed = 10;
            this.LoadLogProgressBar.Name = "LoadLogProgressBar";
            this.LoadLogProgressBar.Size = new System.Drawing.Size(228, 23);
            this.LoadLogProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.LoadLogProgressBar.TabIndex = 33;
            this.LoadLogProgressBar.Visible = false;
            // 
            // LogDisplayTabs
            // 
            this.LogDisplayTabs.Controls.Add(this.tabPage12);
            this.LogDisplayTabs.Controls.Add(this.tabPage29);
            this.LogDisplayTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogDisplayTabs.Location = new System.Drawing.Point(0, 0);
            this.LogDisplayTabs.Margin = new System.Windows.Forms.Padding(0);
            this.LogDisplayTabs.Name = "LogDisplayTabs";
            this.LogDisplayTabs.Padding = new System.Drawing.Point(0, 0);
            this.LogDisplayTabs.SelectedIndex = 0;
            this.LogDisplayTabs.Size = new System.Drawing.Size(876, 219);
            this.LogDisplayTabs.TabIndex = 4;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.RequestView);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Size = new System.Drawing.Size(868, 193);
            this.tabPage12.TabIndex = 0;
            this.tabPage12.Text = "  Request  ";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // tabPage29
            // 
            this.tabPage29.Controls.Add(this.ResponseView);
            this.tabPage29.Location = new System.Drawing.Point(4, 22);
            this.tabPage29.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage29.Name = "tabPage29";
            this.tabPage29.Size = new System.Drawing.Size(868, 193);
            this.tabPage29.TabIndex = 1;
            this.tabPage29.Text = "  Response  ";
            this.tabPage29.UseVisualStyleBackColor = true;
            // 
            // BaseTabs
            // 
            this.BaseTabs.Controls.Add(this.SearchFilterTab);
            this.BaseTabs.Controls.Add(this.SearchResultsTab);
            this.BaseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseTabs.Location = new System.Drawing.Point(0, 0);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(884, 562);
            this.BaseTabs.TabIndex = 10;
            // 
            // SearchFilterTab
            // 
            this.SearchFilterTab.Controls.Add(this.SearchFilterPanel);
            this.SearchFilterTab.Location = new System.Drawing.Point(4, 22);
            this.SearchFilterTab.Margin = new System.Windows.Forms.Padding(0);
            this.SearchFilterTab.Name = "SearchFilterTab";
            this.SearchFilterTab.Size = new System.Drawing.Size(876, 536);
            this.SearchFilterTab.TabIndex = 0;
            this.SearchFilterTab.Text = "   Create Filter to Search Logs   ";
            this.SearchFilterTab.UseVisualStyleBackColor = true;
            // 
            // SearchFilterPanel
            // 
            this.SearchFilterPanel.BackColor = System.Drawing.Color.White;
            this.SearchFilterPanel.Controls.Add(this.textBox2);
            this.SearchFilterPanel.Controls.Add(this.Step0StatusTB);
            this.SearchFilterPanel.Controls.Add(this.panel6);
            this.SearchFilterPanel.Controls.Add(this.panel4);
            this.SearchFilterPanel.Controls.Add(this.LogSourceAndIdsPanel);
            this.SearchFilterPanel.Controls.Add(this.panel8);
            this.SearchFilterPanel.Controls.Add(this.panel1);
            this.SearchFilterPanel.Controls.Add(this.panel3);
            this.SearchFilterPanel.Controls.Add(this.panel7);
            this.SearchFilterPanel.Controls.Add(this.panel2);
            this.SearchFilterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchFilterPanel.Location = new System.Drawing.Point(0, 0);
            this.SearchFilterPanel.Name = "SearchFilterPanel";
            this.SearchFilterPanel.Size = new System.Drawing.Size(876, 536);
            this.SearchFilterPanel.TabIndex = 21;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(14, 9);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(852, 85);
            this.textBox2.TabIndex = 153;
            this.textBox2.TabStop = false;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // Step0StatusTB
            // 
            this.Step0StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Step0StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step0StatusTB.Location = new System.Drawing.Point(23, 115);
            this.Step0StatusTB.Multiline = true;
            this.Step0StatusTB.Name = "Step0StatusTB";
            this.Step0StatusTB.Size = new System.Drawing.Size(827, 30);
            this.Step0StatusTB.TabIndex = 21;
            this.Step0StatusTB.TabStop = false;
            this.Step0StatusTB.Visible = false;
            // 
            // SearchResultsTab
            // 
            this.SearchResultsTab.Controls.Add(this.BaseSplit);
            this.SearchResultsTab.Location = new System.Drawing.Point(4, 22);
            this.SearchResultsTab.Margin = new System.Windows.Forms.Padding(0);
            this.SearchResultsTab.Name = "SearchResultsTab";
            this.SearchResultsTab.Size = new System.Drawing.Size(876, 536);
            this.SearchResultsTab.TabIndex = 1;
            this.SearchResultsTab.Text = "   Search Results   ";
            this.SearchResultsTab.UseVisualStyleBackColor = true;
            // 
            // RequestView
            // 
            this.RequestView.BackColor = System.Drawing.Color.White;
            this.RequestView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequestView.Location = new System.Drawing.Point(0, 0);
            this.RequestView.Margin = new System.Windows.Forms.Padding(0);
            this.RequestView.Name = "RequestView";
            this.RequestView.ReadOnly = true;
            this.RequestView.Size = new System.Drawing.Size(868, 193);
            this.RequestView.TabIndex = 0;
            // 
            // ResponseView
            // 
            this.ResponseView.BackColor = System.Drawing.Color.White;
            this.ResponseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResponseView.IncludeReflectionTab = true;
            this.ResponseView.Location = new System.Drawing.Point(0, 0);
            this.ResponseView.Margin = new System.Windows.Forms.Padding(0);
            this.ResponseView.Name = "ResponseView";
            this.ResponseView.ReadOnly = true;
            this.ResponseView.Size = new System.Drawing.Size(868, 193);
            this.ResponseView.TabIndex = 0;
            // 
            // LogAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.BaseTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogAnalyzer";
            this.Text = "Log Analysis and Testing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogAnalyzer_FormClosing);
            this.Load += new System.EventHandler(this.LogAnalyzer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogGrid)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.LogSourceAndIdsPanel.ResumeLayout(false);
            this.LogSourceAndIdsPanel.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.BaseSplit.Panel1.ResumeLayout(false);
            this.BaseSplit.Panel1.PerformLayout();
            this.BaseSplit.Panel2.ResumeLayout(false);
            this.BaseSplit.ResumeLayout(false);
            this.LogDisplayTabs.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage29.ResumeLayout(false);
            this.BaseTabs.ResumeLayout(false);
            this.SearchFilterTab.ResumeLayout(false);
            this.SearchFilterPanel.ResumeLayout(false);
            this.SearchFilterPanel.PerformLayout();
            this.SearchResultsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView LogGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HostNameSelectClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MethodClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn URLClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileClmn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SSLClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParametersClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LengthClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmnMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SetCookieClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotesClmn;
        private System.Windows.Forms.Button StartTestBtn;
        private System.Windows.Forms.Button RunModulesBtn;
        private System.Windows.Forms.Button StartScanBtn;
        private System.Windows.Forms.CheckBox SelectAllCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SelectLogSourceCombo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton MatchRequestMethodsMinusRB;
        private System.Windows.Forms.RadioButton MatchRequestMethodsPlusRB;
        private System.Windows.Forms.TextBox MatchRequestMethodsTB;
        private System.Windows.Forms.CheckBox MatchRequestMethodsCB;
        private System.Windows.Forms.TextBox LogIdsRangeBelowTB;
        private System.Windows.Forms.CheckBox LogIdsRangeBelowCB;
        private System.Windows.Forms.TextBox LogIdsRangeAboveTB;
        private System.Windows.Forms.RadioButton LogIdsRangeCustomRB;
        private System.Windows.Forms.CheckBox LogIdsRangeAboveCB;
        private System.Windows.Forms.RadioButton LogIdsRangeAnyRB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton SearchTypeNewRB;
        private System.Windows.Forms.RadioButton SearchTypeChainedRB;
        private System.Windows.Forms.Panel LogSourceAndIdsPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton MatchFileExtensionsMinusRB;
        private System.Windows.Forms.RadioButton MatchFileExtensionsPlusRB;
        private System.Windows.Forms.TextBox MatchFileExtensionsTB;
        private System.Windows.Forms.CheckBox MatchFileExtensionsCB;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton MatchHostNamesMinusRB;
        private System.Windows.Forms.RadioButton MatchHostNamesPlusRB;
        private System.Windows.Forms.TextBox MatchHostNamesTB;
        private System.Windows.Forms.CheckBox MatchHostNamesCB;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton MatchResponseCodesMinusRB;
        private System.Windows.Forms.RadioButton MatchResponseCodesPlusRB;
        private System.Windows.Forms.TextBox MatchResponseCodesTB;
        private System.Windows.Forms.CheckBox MatchResponseCodesCB;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox MatchRequestUrlTypeCombo;
        private System.Windows.Forms.TextBox MatchRequestUrlKeywordTB;
        private System.Windows.Forms.CheckBox MatchRequestUrlCB;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox MatchKeywordInResponseBodySectionCB;
        private System.Windows.Forms.CheckBox MatchKeywordInResponseHeadersSectionCB;
        private System.Windows.Forms.CheckBox MatchKeywordInRequestBodySectionCB;
        private System.Windows.Forms.CheckBox MatchKeywordInRequestHeadersSectionCB;
        private System.Windows.Forms.CheckBox MatchKeywordInAllSectionsCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MatchKeywordTB;
        private System.Windows.Forms.CheckBox MatchKeywordCB;
        private System.Windows.Forms.SplitContainer BaseSplit;
        internal System.Windows.Forms.TabControl LogDisplayTabs;
        private System.Windows.Forms.TabPage tabPage12;
        internal RequestView RequestView;
        private System.Windows.Forms.TabPage tabPage29;
        internal ResponseView ResponseView;
        private System.Windows.Forms.ProgressBar LoadLogProgressBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl BaseTabs;
        private System.Windows.Forms.TabPage SearchFilterTab;
        private System.Windows.Forms.TabPage SearchResultsTab;
        private System.Windows.Forms.ProgressBar SearchProgressBar;
        private System.Windows.Forms.Panel SearchFilterPanel;
        internal System.Windows.Forms.TextBox Step0StatusTB;
        private System.Windows.Forms.RadioButton ClickActionDisplayLogRB;
        private System.Windows.Forms.RadioButton ClickActionSelectLogRB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label SearchResultsCountLbl;
        private System.Windows.Forms.Button DoDiffBtn;
        private System.Windows.Forms.TextBox textBox2;
    }
}