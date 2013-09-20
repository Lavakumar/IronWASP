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
    partial class ResponseView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResponseView));
            this.StatusAndErrorTB = new System.Windows.Forms.TextBox();
            this.WaitProgressBar = new System.Windows.Forms.ProgressBar();
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.HeadersTab = new System.Windows.Forms.TabPage();
            this.HeadersTBP = new IronWASP.TextBoxPlus();
            this.BodyTab = new System.Windows.Forms.TabPage();
            this.BodyTBP = new IronWASP.TextBoxPlus();
            this.BodyParametersTab = new System.Windows.Forms.TabPage();
            this.BodyParametersTabSplit = new System.Windows.Forms.SplitContainer();
            this.FormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.FormatPluginSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FormatPluginNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanBodyFormatPluginTypeTabs = new System.Windows.Forms.TabControl();
            this.BodyTypeFormatPluginInjectionArrayGridTab = new System.Windows.Forms.TabPage();
            this.BodyFormatPluginsParametersGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.BodyTypeFormatPluginXMLTab = new System.Windows.Forms.TabPage();
            this.FormatXmlBaseSplit = new System.Windows.Forms.SplitContainer();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ConvertXmlToObjectBtn = new System.Windows.Forms.Button();
            this.FormatXmlTBP = new IronWASP.TextBoxPlus();
            this.ReflectionsTab = new System.Windows.Forms.TabPage();
            this.ReflectionRTB = new System.Windows.Forms.RichTextBox();
            this.HelpTab = new System.Windows.Forms.TabPage();
            this.HelpTB = new System.Windows.Forms.TextBox();
            this.EditingTab = new System.Windows.Forms.TabPage();
            this.SaveEditsLbl = new System.Windows.Forms.LinkLabel();
            this.EditTBP = new IronWASP.TextBoxPlus();
            this.RenderLbl = new System.Windows.Forms.LinkLabel();
            this.RoundTripLbl = new System.Windows.Forms.Label();
            this.ScreenshotBtn = new System.Windows.Forms.Button();
            this.BaseTabs.SuspendLayout();
            this.HeadersTab.SuspendLayout();
            this.BodyTab.SuspendLayout();
            this.BodyParametersTab.SuspendLayout();
            this.BodyParametersTabSplit.Panel1.SuspendLayout();
            this.BodyParametersTabSplit.Panel2.SuspendLayout();
            this.BodyParametersTabSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormatPluginsGrid)).BeginInit();
            this.ScanBodyFormatPluginTypeTabs.SuspendLayout();
            this.BodyTypeFormatPluginInjectionArrayGridTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BodyFormatPluginsParametersGrid)).BeginInit();
            this.BodyTypeFormatPluginXMLTab.SuspendLayout();
            this.FormatXmlBaseSplit.Panel1.SuspendLayout();
            this.FormatXmlBaseSplit.Panel2.SuspendLayout();
            this.FormatXmlBaseSplit.SuspendLayout();
            this.ReflectionsTab.SuspendLayout();
            this.HelpTab.SuspendLayout();
            this.EditingTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusAndErrorTB
            // 
            this.StatusAndErrorTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusAndErrorTB.BackColor = System.Drawing.Color.White;
            this.StatusAndErrorTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusAndErrorTB.Location = new System.Drawing.Point(59, 3);
            this.StatusAndErrorTB.Name = "StatusAndErrorTB";
            this.StatusAndErrorTB.ReadOnly = true;
            this.StatusAndErrorTB.Size = new System.Drawing.Size(421, 13);
            this.StatusAndErrorTB.TabIndex = 7;
            this.StatusAndErrorTB.Visible = false;
            // 
            // WaitProgressBar
            // 
            this.WaitProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WaitProgressBar.Location = new System.Drawing.Point(490, 3);
            this.WaitProgressBar.MarqueeAnimationSpeed = 10;
            this.WaitProgressBar.Name = "WaitProgressBar";
            this.WaitProgressBar.Size = new System.Drawing.Size(114, 13);
            this.WaitProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.WaitProgressBar.TabIndex = 6;
            this.WaitProgressBar.Visible = false;
            // 
            // BaseTabs
            // 
            this.BaseTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseTabs.Controls.Add(this.HeadersTab);
            this.BaseTabs.Controls.Add(this.BodyTab);
            this.BaseTabs.Controls.Add(this.BodyParametersTab);
            this.BaseTabs.Controls.Add(this.ReflectionsTab);
            this.BaseTabs.Controls.Add(this.HelpTab);
            this.BaseTabs.Controls.Add(this.EditingTab);
            this.BaseTabs.Location = new System.Drawing.Point(0, 20);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(682, 161);
            this.BaseTabs.TabIndex = 5;
            this.BaseTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.BaseTabs_Selecting);
            this.BaseTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.BaseTabs_Deselecting);
            // 
            // HeadersTab
            // 
            this.HeadersTab.Controls.Add(this.HeadersTBP);
            this.HeadersTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeadersTab.Location = new System.Drawing.Point(4, 22);
            this.HeadersTab.Margin = new System.Windows.Forms.Padding(0);
            this.HeadersTab.Name = "HeadersTab";
            this.HeadersTab.Size = new System.Drawing.Size(674, 135);
            this.HeadersTab.TabIndex = 0;
            this.HeadersTab.Text = "  Raw Headers  ";
            this.HeadersTab.UseVisualStyleBackColor = true;
            // 
            // HeadersTBP
            // 
            this.HeadersTBP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeadersTBP.Location = new System.Drawing.Point(0, 0);
            this.HeadersTBP.Margin = new System.Windows.Forms.Padding(0);
            this.HeadersTBP.Name = "HeadersTBP";
            this.HeadersTBP.ReadOnly = false;
            this.HeadersTBP.Size = new System.Drawing.Size(674, 135);
            this.HeadersTBP.TabIndex = 0;
            this.HeadersTBP.ValueChanged += new IronWASP.TextBoxPlus.ValueChangedEvent(this.HeadersTBP_ValueChanged);
            // 
            // BodyTab
            // 
            this.BodyTab.Controls.Add(this.BodyTBP);
            this.BodyTab.Location = new System.Drawing.Point(4, 22);
            this.BodyTab.Name = "BodyTab";
            this.BodyTab.Size = new System.Drawing.Size(674, 135);
            this.BodyTab.TabIndex = 3;
            this.BodyTab.Text = "  Raw Body  ";
            this.BodyTab.UseVisualStyleBackColor = true;
            // 
            // BodyTBP
            // 
            this.BodyTBP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BodyTBP.Location = new System.Drawing.Point(0, 0);
            this.BodyTBP.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTBP.Name = "BodyTBP";
            this.BodyTBP.ReadOnly = false;
            this.BodyTBP.Size = new System.Drawing.Size(674, 135);
            this.BodyTBP.TabIndex = 0;
            this.BodyTBP.ValueChanged += new IronWASP.TextBoxPlus.ValueChangedEvent(this.BodyTBP_ValueChanged);
            // 
            // BodyParametersTab
            // 
            this.BodyParametersTab.Controls.Add(this.BodyParametersTabSplit);
            this.BodyParametersTab.Location = new System.Drawing.Point(4, 22);
            this.BodyParametersTab.Name = "BodyParametersTab";
            this.BodyParametersTab.Size = new System.Drawing.Size(674, 135);
            this.BodyParametersTab.TabIndex = 4;
            this.BodyParametersTab.Text = "  Body Parameters  ";
            this.BodyParametersTab.UseVisualStyleBackColor = true;
            // 
            // BodyParametersTabSplit
            // 
            this.BodyParametersTabSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BodyParametersTabSplit.Location = new System.Drawing.Point(0, 0);
            this.BodyParametersTabSplit.Margin = new System.Windows.Forms.Padding(0);
            this.BodyParametersTabSplit.Name = "BodyParametersTabSplit";
            // 
            // BodyParametersTabSplit.Panel1
            // 
            this.BodyParametersTabSplit.Panel1.Controls.Add(this.FormatPluginsGrid);
            // 
            // BodyParametersTabSplit.Panel2
            // 
            this.BodyParametersTabSplit.Panel2.Controls.Add(this.ScanBodyFormatPluginTypeTabs);
            this.BodyParametersTabSplit.Size = new System.Drawing.Size(674, 135);
            this.BodyParametersTabSplit.SplitterDistance = 98;
            this.BodyParametersTabSplit.SplitterWidth = 2;
            this.BodyParametersTabSplit.TabIndex = 0;
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
            this.FormatPluginSelectColumn,
            this.FormatPluginNameColumn});
            this.FormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.FormatPluginsGrid.GridColor = System.Drawing.Color.White;
            this.FormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.FormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.FormatPluginsGrid.MultiSelect = false;
            this.FormatPluginsGrid.Name = "FormatPluginsGrid";
            this.FormatPluginsGrid.RowHeadersVisible = false;
            this.FormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FormatPluginsGrid.Size = new System.Drawing.Size(98, 135);
            this.FormatPluginsGrid.TabIndex = 0;
            this.FormatPluginsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FormatPluginsGrid_CellClick);
            // 
            // FormatPluginSelectColumn
            // 
            this.FormatPluginSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FormatPluginSelectColumn.HeaderText = "";
            this.FormatPluginSelectColumn.MinimumWidth = 20;
            this.FormatPluginSelectColumn.Name = "FormatPluginSelectColumn";
            this.FormatPluginSelectColumn.Width = 20;
            // 
            // FormatPluginNameColumn
            // 
            this.FormatPluginNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FormatPluginNameColumn.HeaderText = "Body Format";
            this.FormatPluginNameColumn.Name = "FormatPluginNameColumn";
            this.FormatPluginNameColumn.ReadOnly = true;
            this.FormatPluginNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.ScanBodyFormatPluginTypeTabs.Size = new System.Drawing.Size(574, 135);
            this.ScanBodyFormatPluginTypeTabs.TabIndex = 0;
            // 
            // BodyTypeFormatPluginInjectionArrayGridTab
            // 
            this.BodyTypeFormatPluginInjectionArrayGridTab.Controls.Add(this.BodyFormatPluginsParametersGrid);
            this.BodyTypeFormatPluginInjectionArrayGridTab.Location = new System.Drawing.Point(4, 22);
            this.BodyTypeFormatPluginInjectionArrayGridTab.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTypeFormatPluginInjectionArrayGridTab.Name = "BodyTypeFormatPluginInjectionArrayGridTab";
            this.BodyTypeFormatPluginInjectionArrayGridTab.Size = new System.Drawing.Size(566, 109);
            this.BodyTypeFormatPluginInjectionArrayGridTab.TabIndex = 0;
            this.BodyTypeFormatPluginInjectionArrayGridTab.Text = "  Name/Value ";
            this.BodyTypeFormatPluginInjectionArrayGridTab.UseVisualStyleBackColor = true;
            // 
            // BodyFormatPluginsParametersGrid
            // 
            this.BodyFormatPluginsParametersGrid.AllowUserToAddRows = false;
            this.BodyFormatPluginsParametersGrid.AllowUserToDeleteRows = false;
            this.BodyFormatPluginsParametersGrid.AllowUserToResizeRows = false;
            this.BodyFormatPluginsParametersGrid.BackgroundColor = System.Drawing.Color.White;
            this.BodyFormatPluginsParametersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BodyFormatPluginsParametersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BodyFormatPluginsParametersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Column1});
            this.BodyFormatPluginsParametersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BodyFormatPluginsParametersGrid.GridColor = System.Drawing.Color.White;
            this.BodyFormatPluginsParametersGrid.Location = new System.Drawing.Point(0, 0);
            this.BodyFormatPluginsParametersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.BodyFormatPluginsParametersGrid.Name = "BodyFormatPluginsParametersGrid";
            this.BodyFormatPluginsParametersGrid.RowHeadersVisible = false;
            this.BodyFormatPluginsParametersGrid.Size = new System.Drawing.Size(566, 109);
            this.BodyFormatPluginsParametersGrid.TabIndex = 2;
            this.BodyFormatPluginsParametersGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BodyFormatPluginsParametersGrid_CellClick);
            this.BodyFormatPluginsParametersGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.BodyFormatPluginsParametersGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "NAME";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "VALUE";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "";
            this.Column1.MinimumWidth = 20;
            this.Column1.Name = "Column1";
            this.Column1.Width = 20;
            // 
            // BodyTypeFormatPluginXMLTab
            // 
            this.BodyTypeFormatPluginXMLTab.Controls.Add(this.FormatXmlBaseSplit);
            this.BodyTypeFormatPluginXMLTab.Location = new System.Drawing.Point(4, 40);
            this.BodyTypeFormatPluginXMLTab.Margin = new System.Windows.Forms.Padding(0);
            this.BodyTypeFormatPluginXMLTab.Name = "BodyTypeFormatPluginXMLTab";
            this.BodyTypeFormatPluginXMLTab.Size = new System.Drawing.Size(154, 30);
            this.BodyTypeFormatPluginXMLTab.TabIndex = 1;
            this.BodyTypeFormatPluginXMLTab.Text = "  Format Plugin XML (For Format Plugin Developers)  ";
            this.BodyTypeFormatPluginXMLTab.UseVisualStyleBackColor = true;
            // 
            // FormatXmlBaseSplit
            // 
            this.FormatXmlBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormatXmlBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.FormatXmlBaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.FormatXmlBaseSplit.Name = "FormatXmlBaseSplit";
            // 
            // FormatXmlBaseSplit.Panel1
            // 
            this.FormatXmlBaseSplit.Panel1.Controls.Add(this.textBox2);
            this.FormatXmlBaseSplit.Panel1.Controls.Add(this.ConvertXmlToObjectBtn);
            // 
            // FormatXmlBaseSplit.Panel2
            // 
            this.FormatXmlBaseSplit.Panel2.Controls.Add(this.FormatXmlTBP);
            this.FormatXmlBaseSplit.Size = new System.Drawing.Size(154, 30);
            this.FormatXmlBaseSplit.SplitterDistance = 28;
            this.FormatXmlBaseSplit.SplitterWidth = 2;
            this.FormatXmlBaseSplit.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox2.Location = new System.Drawing.Point(0, -38);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(28, 68);
            this.textBox2.TabIndex = 5;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "You can edit this XML & update the changes to the Response body by clicking the a" +
    "bove button";
            // 
            // ConvertXmlToObjectBtn
            // 
            this.ConvertXmlToObjectBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConvertXmlToObjectBtn.Location = new System.Drawing.Point(0, 0);
            this.ConvertXmlToObjectBtn.Name = "ConvertXmlToObjectBtn";
            this.ConvertXmlToObjectBtn.Size = new System.Drawing.Size(28, 38);
            this.ConvertXmlToObjectBtn.TabIndex = 3;
            this.ConvertXmlToObjectBtn.Text = "Convert this XML to object";
            this.ConvertXmlToObjectBtn.UseVisualStyleBackColor = true;
            this.ConvertXmlToObjectBtn.Click += new System.EventHandler(this.ConvertXmlToObjectBtn_Click);
            // 
            // FormatXmlTBP
            // 
            this.FormatXmlTBP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormatXmlTBP.Location = new System.Drawing.Point(0, 0);
            this.FormatXmlTBP.Name = "FormatXmlTBP";
            this.FormatXmlTBP.ReadOnly = false;
            this.FormatXmlTBP.Size = new System.Drawing.Size(124, 30);
            this.FormatXmlTBP.TabIndex = 4;
            this.FormatXmlTBP.ValueChanged += new IronWASP.TextBoxPlus.ValueChangedEvent(this.FormatXmlTBP_ValueChanged);
            // 
            // ReflectionsTab
            // 
            this.ReflectionsTab.Controls.Add(this.ReflectionRTB);
            this.ReflectionsTab.Location = new System.Drawing.Point(4, 22);
            this.ReflectionsTab.Name = "ReflectionsTab";
            this.ReflectionsTab.Size = new System.Drawing.Size(674, 135);
            this.ReflectionsTab.TabIndex = 5;
            this.ReflectionsTab.Text = "  Reflections  ";
            this.ReflectionsTab.UseVisualStyleBackColor = true;
            // 
            // ReflectionRTB
            // 
            this.ReflectionRTB.BackColor = System.Drawing.Color.White;
            this.ReflectionRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReflectionRTB.DetectUrls = false;
            this.ReflectionRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReflectionRTB.Font = new System.Drawing.Font("Lucida Bright", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReflectionRTB.Location = new System.Drawing.Point(0, 0);
            this.ReflectionRTB.Name = "ReflectionRTB";
            this.ReflectionRTB.ReadOnly = true;
            this.ReflectionRTB.Size = new System.Drawing.Size(674, 135);
            this.ReflectionRTB.TabIndex = 1;
            this.ReflectionRTB.Text = "";
            // 
            // HelpTab
            // 
            this.HelpTab.Controls.Add(this.HelpTB);
            this.HelpTab.Location = new System.Drawing.Point(4, 22);
            this.HelpTab.Name = "HelpTab";
            this.HelpTab.Size = new System.Drawing.Size(674, 135);
            this.HelpTab.TabIndex = 6;
            this.HelpTab.Text = "  Help  ";
            this.HelpTab.UseVisualStyleBackColor = true;
            // 
            // HelpTB
            // 
            this.HelpTB.BackColor = System.Drawing.Color.White;
            this.HelpTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HelpTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HelpTB.Location = new System.Drawing.Point(0, 0);
            this.HelpTB.Multiline = true;
            this.HelpTB.Name = "HelpTB";
            this.HelpTB.ReadOnly = true;
            this.HelpTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HelpTB.Size = new System.Drawing.Size(674, 135);
            this.HelpTB.TabIndex = 1;
            this.HelpTB.TabStop = false;
            this.HelpTB.Text = resources.GetString("HelpTB.Text");
            // 
            // EditingTab
            // 
            this.EditingTab.Controls.Add(this.SaveEditsLbl);
            this.EditingTab.Controls.Add(this.EditTBP);
            this.EditingTab.Location = new System.Drawing.Point(4, 22);
            this.EditingTab.Name = "EditingTab";
            this.EditingTab.Size = new System.Drawing.Size(674, 135);
            this.EditingTab.TabIndex = 7;
            this.EditingTab.Text = "  ";
            this.EditingTab.UseVisualStyleBackColor = true;
            // 
            // SaveEditsLbl
            // 
            this.SaveEditsLbl.AutoSize = true;
            this.SaveEditsLbl.Location = new System.Drawing.Point(3, 2);
            this.SaveEditsLbl.Name = "SaveEditsLbl";
            this.SaveEditsLbl.Size = new System.Drawing.Size(77, 13);
            this.SaveEditsLbl.TabIndex = 12;
            this.SaveEditsLbl.TabStop = true;
            this.SaveEditsLbl.Text = "Save Changes";
            this.SaveEditsLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SaveEditsLbl_LinkClicked);
            // 
            // EditTBP
            // 
            this.EditTBP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditTBP.Location = new System.Drawing.Point(0, 16);
            this.EditTBP.Margin = new System.Windows.Forms.Padding(0);
            this.EditTBP.Name = "EditTBP";
            this.EditTBP.ReadOnly = false;
            this.EditTBP.Size = new System.Drawing.Size(674, 119);
            this.EditTBP.TabIndex = 11;
            // 
            // RenderLbl
            // 
            this.RenderLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderLbl.AutoSize = true;
            this.RenderLbl.Location = new System.Drawing.Point(607, 2);
            this.RenderLbl.Name = "RenderLbl";
            this.RenderLbl.Size = new System.Drawing.Size(42, 13);
            this.RenderLbl.TabIndex = 8;
            this.RenderLbl.TabStop = true;
            this.RenderLbl.Text = "Render";
            this.RenderLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RenderLbl_LinkClicked);
            // 
            // RoundTripLbl
            // 
            this.RoundTripLbl.AutoSize = true;
            this.RoundTripLbl.ForeColor = System.Drawing.Color.Blue;
            this.RoundTripLbl.Location = new System.Drawing.Point(3, 3);
            this.RoundTripLbl.Name = "RoundTripLbl";
            this.RoundTripLbl.Size = new System.Drawing.Size(0, 13);
            this.RoundTripLbl.TabIndex = 9;
            // 
            // ScreenshotBtn
            // 
            this.ScreenshotBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenshotBtn.BackgroundImage = global::IronWASP.Properties.Resources.camera;
            this.ScreenshotBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ScreenshotBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScreenshotBtn.Location = new System.Drawing.Point(652, 1);
            this.ScreenshotBtn.Name = "ScreenshotBtn";
            this.ScreenshotBtn.Size = new System.Drawing.Size(29, 17);
            this.ScreenshotBtn.TabIndex = 10;
            this.ScreenshotBtn.UseVisualStyleBackColor = true;
            this.ScreenshotBtn.Click += new System.EventHandler(this.ScreenshotBtn_Click);
            // 
            // ResponseView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ScreenshotBtn);
            this.Controls.Add(this.RoundTripLbl);
            this.Controls.Add(this.RenderLbl);
            this.Controls.Add(this.StatusAndErrorTB);
            this.Controls.Add(this.WaitProgressBar);
            this.Controls.Add(this.BaseTabs);
            this.Name = "ResponseView";
            this.Size = new System.Drawing.Size(682, 181);
            this.Load += new System.EventHandler(this.ResponseView_Load);
            this.BaseTabs.ResumeLayout(false);
            this.HeadersTab.ResumeLayout(false);
            this.BodyTab.ResumeLayout(false);
            this.BodyParametersTab.ResumeLayout(false);
            this.BodyParametersTabSplit.Panel1.ResumeLayout(false);
            this.BodyParametersTabSplit.Panel2.ResumeLayout(false);
            this.BodyParametersTabSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FormatPluginsGrid)).EndInit();
            this.ScanBodyFormatPluginTypeTabs.ResumeLayout(false);
            this.BodyTypeFormatPluginInjectionArrayGridTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BodyFormatPluginsParametersGrid)).EndInit();
            this.BodyTypeFormatPluginXMLTab.ResumeLayout(false);
            this.FormatXmlBaseSplit.Panel1.ResumeLayout(false);
            this.FormatXmlBaseSplit.Panel1.PerformLayout();
            this.FormatXmlBaseSplit.Panel2.ResumeLayout(false);
            this.FormatXmlBaseSplit.ResumeLayout(false);
            this.ReflectionsTab.ResumeLayout(false);
            this.HelpTab.ResumeLayout(false);
            this.HelpTab.PerformLayout();
            this.EditingTab.ResumeLayout(false);
            this.EditingTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StatusAndErrorTB;
        private System.Windows.Forms.ProgressBar WaitProgressBar;
        private System.Windows.Forms.TabControl BaseTabs;
        private System.Windows.Forms.TabPage HeadersTab;
        private TextBoxPlus HeadersTBP;
        private System.Windows.Forms.TabPage BodyTab;
        private TextBoxPlus BodyTBP;
        private System.Windows.Forms.TabPage BodyParametersTab;
        private System.Windows.Forms.SplitContainer BodyParametersTabSplit;
        internal System.Windows.Forms.DataGridView FormatPluginsGrid;
        internal System.Windows.Forms.TabControl ScanBodyFormatPluginTypeTabs;
        private System.Windows.Forms.TabPage BodyTypeFormatPluginInjectionArrayGridTab;
        internal System.Windows.Forms.DataGridView BodyFormatPluginsParametersGrid;
        private System.Windows.Forms.TabPage BodyTypeFormatPluginXMLTab;
        private System.Windows.Forms.SplitContainer FormatXmlBaseSplit;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button ConvertXmlToObjectBtn;
        private TextBoxPlus FormatXmlTBP;
        private System.Windows.Forms.TabPage ReflectionsTab;
        internal System.Windows.Forms.RichTextBox ReflectionRTB;
        private System.Windows.Forms.LinkLabel RenderLbl;
        private System.Windows.Forms.TabPage HelpTab;
        private System.Windows.Forms.TextBox HelpTB;
        private System.Windows.Forms.Label RoundTripLbl;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FormatPluginSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormatPluginNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.TabPage EditingTab;
        private System.Windows.Forms.LinkLabel SaveEditsLbl;
        private TextBoxPlus EditTBP;
        private System.Windows.Forms.Button ScreenshotBtn;
    }
}
