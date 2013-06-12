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
    partial class LogTraceViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogTraceViewer));
            this.BaseSplit = new System.Windows.Forms.SplitContainer();
            this.ScanTraceTabs = new System.Windows.Forms.TabControl();
            this.ScanTraceOverviewTab = new System.Windows.Forms.TabPage();
            this.DoDiffBtn = new System.Windows.Forms.Button();
            this.ClickActionDisplayLogRB = new System.Windows.Forms.RadioButton();
            this.ClickActionSelectLogRB = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.ScanTraceOverviewGrid = new System.Windows.Forms.DataGridView();
            this.SelectClmn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanTraceDetailedInfoTab = new System.Windows.Forms.TabPage();
            this.ScanTraceMsgRTB = new System.Windows.Forms.RichTextBox();
            this.LoadLogProgressBar = new System.Windows.Forms.ProgressBar();
            this.LogDisplayTabs = new System.Windows.Forms.TabControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.RequestView = new IronWASP.RequestView();
            this.tabPage29 = new System.Windows.Forms.TabPage();
            this.ResponseView = new IronWASP.ResponseView();
            this.BaseSplit.Panel1.SuspendLayout();
            this.BaseSplit.Panel2.SuspendLayout();
            this.BaseSplit.SuspendLayout();
            this.ScanTraceTabs.SuspendLayout();
            this.ScanTraceOverviewTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanTraceOverviewGrid)).BeginInit();
            this.ScanTraceDetailedInfoTab.SuspendLayout();
            this.LogDisplayTabs.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage29.SuspendLayout();
            this.SuspendLayout();
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
            this.BaseSplit.Panel1.Controls.Add(this.ScanTraceTabs);
            // 
            // BaseSplit.Panel2
            // 
            this.BaseSplit.Panel2.Controls.Add(this.LoadLogProgressBar);
            this.BaseSplit.Panel2.Controls.Add(this.LogDisplayTabs);
            this.BaseSplit.Size = new System.Drawing.Size(884, 561);
            this.BaseSplit.SplitterDistance = 329;
            this.BaseSplit.SplitterWidth = 2;
            this.BaseSplit.TabIndex = 11;
            // 
            // ScanTraceTabs
            // 
            this.ScanTraceTabs.Controls.Add(this.ScanTraceOverviewTab);
            this.ScanTraceTabs.Controls.Add(this.ScanTraceDetailedInfoTab);
            this.ScanTraceTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanTraceTabs.Location = new System.Drawing.Point(0, 0);
            this.ScanTraceTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ScanTraceTabs.Name = "ScanTraceTabs";
            this.ScanTraceTabs.Padding = new System.Drawing.Point(0, 0);
            this.ScanTraceTabs.SelectedIndex = 0;
            this.ScanTraceTabs.Size = new System.Drawing.Size(884, 329);
            this.ScanTraceTabs.TabIndex = 2;
            // 
            // ScanTraceOverviewTab
            // 
            this.ScanTraceOverviewTab.Controls.Add(this.DoDiffBtn);
            this.ScanTraceOverviewTab.Controls.Add(this.ClickActionDisplayLogRB);
            this.ScanTraceOverviewTab.Controls.Add(this.ClickActionSelectLogRB);
            this.ScanTraceOverviewTab.Controls.Add(this.label7);
            this.ScanTraceOverviewTab.Controls.Add(this.ScanTraceOverviewGrid);
            this.ScanTraceOverviewTab.Location = new System.Drawing.Point(4, 22);
            this.ScanTraceOverviewTab.Margin = new System.Windows.Forms.Padding(0);
            this.ScanTraceOverviewTab.Name = "ScanTraceOverviewTab";
            this.ScanTraceOverviewTab.Size = new System.Drawing.Size(876, 303);
            this.ScanTraceOverviewTab.TabIndex = 0;
            this.ScanTraceOverviewTab.Text = "    Overview    ";
            this.ScanTraceOverviewTab.UseVisualStyleBackColor = true;
            // 
            // DoDiffBtn
            // 
            this.DoDiffBtn.Enabled = false;
            this.DoDiffBtn.Location = new System.Drawing.Point(272, 5);
            this.DoDiffBtn.Name = "DoDiffBtn";
            this.DoDiffBtn.Size = new System.Drawing.Size(140, 23);
            this.DoDiffBtn.TabIndex = 42;
            this.DoDiffBtn.Text = "Diff Selected Sessions";
            this.DoDiffBtn.UseVisualStyleBackColor = true;
            this.DoDiffBtn.Click += new System.EventHandler(this.DoDiffBtn_Click);
            // 
            // ClickActionDisplayLogRB
            // 
            this.ClickActionDisplayLogRB.AutoSize = true;
            this.ClickActionDisplayLogRB.Checked = true;
            this.ClickActionDisplayLogRB.Location = new System.Drawing.Point(83, 7);
            this.ClickActionDisplayLogRB.Name = "ClickActionDisplayLogRB";
            this.ClickActionDisplayLogRB.Size = new System.Drawing.Size(80, 17);
            this.ClickActionDisplayLogRB.TabIndex = 41;
            this.ClickActionDisplayLogRB.TabStop = true;
            this.ClickActionDisplayLogRB.Text = "Display Log";
            this.ClickActionDisplayLogRB.UseVisualStyleBackColor = true;
            // 
            // ClickActionSelectLogRB
            // 
            this.ClickActionSelectLogRB.AutoSize = true;
            this.ClickActionSelectLogRB.Location = new System.Drawing.Point(179, 8);
            this.ClickActionSelectLogRB.Name = "ClickActionSelectLogRB";
            this.ClickActionSelectLogRB.Size = new System.Drawing.Size(76, 17);
            this.ClickActionSelectLogRB.TabIndex = 40;
            this.ClickActionSelectLogRB.Text = "Select Log";
            this.ClickActionSelectLogRB.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Click Action: ";
            // 
            // ScanTraceOverviewGrid
            // 
            this.ScanTraceOverviewGrid.AllowUserToAddRows = false;
            this.ScanTraceOverviewGrid.AllowUserToDeleteRows = false;
            this.ScanTraceOverviewGrid.AllowUserToOrderColumns = true;
            this.ScanTraceOverviewGrid.AllowUserToResizeRows = false;
            this.ScanTraceOverviewGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanTraceOverviewGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ScanTraceOverviewGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanTraceOverviewGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScanTraceOverviewGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ScanTraceOverviewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ScanTraceOverviewGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectClmn,
            this.Column4,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.Column5,
            this.dataGridViewTextBoxColumn22});
            this.ScanTraceOverviewGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanTraceOverviewGrid.GridColor = System.Drawing.Color.White;
            this.ScanTraceOverviewGrid.Location = new System.Drawing.Point(0, 33);
            this.ScanTraceOverviewGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanTraceOverviewGrid.MultiSelect = false;
            this.ScanTraceOverviewGrid.Name = "ScanTraceOverviewGrid";
            this.ScanTraceOverviewGrid.ReadOnly = true;
            this.ScanTraceOverviewGrid.RowHeadersVisible = false;
            this.ScanTraceOverviewGrid.RowHeadersWidth = 10;
            this.ScanTraceOverviewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanTraceOverviewGrid.Size = new System.Drawing.Size(876, 270);
            this.ScanTraceOverviewGrid.TabIndex = 9;
            this.ScanTraceOverviewGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanTraceOverviewGrid_CellClick);
            // 
            // SelectClmn
            // 
            this.SelectClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SelectClmn.HeaderText = "Select";
            this.SelectClmn.Name = "SelectClmn";
            this.SelectClmn.ReadOnly = true;
            this.SelectClmn.Width = 60;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.HeaderText = "ID";
            this.Column4.MinimumWidth = 30;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 30;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn17.HeaderText = "Log ID";
            this.dataGridViewTextBoxColumn17.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.Width = 64;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn18.HeaderText = "Injected Payload";
            this.dataGridViewTextBoxColumn18.MinimumWidth = 200;
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Width = 200;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn19.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn19.HeaderText = "Code";
            this.dataGridViewTextBoxColumn19.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 50;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn20.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn20.HeaderText = "Body Length";
            this.dataGridViewTextBoxColumn20.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Width = 92;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn21.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn21.HeaderText = "MIME Type";
            this.dataGridViewTextBoxColumn21.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Width = 87;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column5.HeaderText = "Time(ms)";
            this.Column5.MinimumWidth = 50;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn22.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn22.HeaderText = "Response Signature";
            this.dataGridViewTextBoxColumn22.MinimumWidth = 200;
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Width = 200;
            // 
            // ScanTraceDetailedInfoTab
            // 
            this.ScanTraceDetailedInfoTab.Controls.Add(this.ScanTraceMsgRTB);
            this.ScanTraceDetailedInfoTab.Location = new System.Drawing.Point(4, 22);
            this.ScanTraceDetailedInfoTab.Margin = new System.Windows.Forms.Padding(0);
            this.ScanTraceDetailedInfoTab.Name = "ScanTraceDetailedInfoTab";
            this.ScanTraceDetailedInfoTab.Size = new System.Drawing.Size(876, 303);
            this.ScanTraceDetailedInfoTab.TabIndex = 1;
            this.ScanTraceDetailedInfoTab.Text = "    Detailed Info    ";
            this.ScanTraceDetailedInfoTab.UseVisualStyleBackColor = true;
            // 
            // ScanTraceMsgRTB
            // 
            this.ScanTraceMsgRTB.BackColor = System.Drawing.Color.White;
            this.ScanTraceMsgRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanTraceMsgRTB.DetectUrls = false;
            this.ScanTraceMsgRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanTraceMsgRTB.Location = new System.Drawing.Point(0, 0);
            this.ScanTraceMsgRTB.Name = "ScanTraceMsgRTB";
            this.ScanTraceMsgRTB.ReadOnly = true;
            this.ScanTraceMsgRTB.Size = new System.Drawing.Size(876, 303);
            this.ScanTraceMsgRTB.TabIndex = 0;
            this.ScanTraceMsgRTB.Text = "";
            // 
            // LoadLogProgressBar
            // 
            this.LoadLogProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadLogProgressBar.Location = new System.Drawing.Point(303, 45);
            this.LoadLogProgressBar.MarqueeAnimationSpeed = 10;
            this.LoadLogProgressBar.Name = "LoadLogProgressBar";
            this.LoadLogProgressBar.Size = new System.Drawing.Size(236, 23);
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
            this.LogDisplayTabs.Size = new System.Drawing.Size(884, 230);
            this.LogDisplayTabs.TabIndex = 4;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.RequestView);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Size = new System.Drawing.Size(876, 204);
            this.tabPage12.TabIndex = 0;
            this.tabPage12.Text = "  Request  ";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // RequestView
            // 
            this.RequestView.BackColor = System.Drawing.Color.White;
            this.RequestView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequestView.Location = new System.Drawing.Point(0, 0);
            this.RequestView.Margin = new System.Windows.Forms.Padding(0);
            this.RequestView.Name = "RequestView";
            this.RequestView.ReadOnly = true;
            this.RequestView.Size = new System.Drawing.Size(876, 204);
            this.RequestView.TabIndex = 0;
            // 
            // tabPage29
            // 
            this.tabPage29.Controls.Add(this.ResponseView);
            this.tabPage29.Location = new System.Drawing.Point(4, 22);
            this.tabPage29.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage29.Name = "tabPage29";
            this.tabPage29.Size = new System.Drawing.Size(876, 204);
            this.tabPage29.TabIndex = 1;
            this.tabPage29.Text = "  Response  ";
            this.tabPage29.UseVisualStyleBackColor = true;
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
            this.ResponseView.Size = new System.Drawing.Size(876, 204);
            this.ResponseView.TabIndex = 0;
            // 
            // LogTraceViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.BaseSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogTraceViewer";
            this.Text = "Click on a Trace Row to view Request/Response";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogTraceViewer_FormClosing);
            this.BaseSplit.Panel1.ResumeLayout(false);
            this.BaseSplit.Panel2.ResumeLayout(false);
            this.BaseSplit.ResumeLayout(false);
            this.ScanTraceTabs.ResumeLayout(false);
            this.ScanTraceOverviewTab.ResumeLayout(false);
            this.ScanTraceOverviewTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanTraceOverviewGrid)).EndInit();
            this.ScanTraceDetailedInfoTab.ResumeLayout(false);
            this.LogDisplayTabs.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage29.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer BaseSplit;
        private System.Windows.Forms.ProgressBar LoadLogProgressBar;
        internal System.Windows.Forms.TabControl LogDisplayTabs;
        private System.Windows.Forms.TabPage tabPage12;
        internal RequestView RequestView;
        private System.Windows.Forms.TabPage tabPage29;
        internal ResponseView ResponseView;
        private System.Windows.Forms.TabControl ScanTraceTabs;
        private System.Windows.Forms.TabPage ScanTraceOverviewTab;
        internal System.Windows.Forms.DataGridView ScanTraceOverviewGrid;
        private System.Windows.Forms.TabPage ScanTraceDetailedInfoTab;
        internal System.Windows.Forms.RichTextBox ScanTraceMsgRTB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.RadioButton ClickActionDisplayLogRB;
        private System.Windows.Forms.RadioButton ClickActionSelectLogRB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button DoDiffBtn;
    }
}