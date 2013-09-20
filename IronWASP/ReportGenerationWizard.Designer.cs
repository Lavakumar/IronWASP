namespace IronWASP
{
    partial class ReportGenerationWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportGenerationWizard));
            this.HostsGrid = new System.Windows.Forms.DataGridView();
            this.AskUserGridCheckColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AskUserGridValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FindingTypesGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FindingTitlesGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FindingsTree = new System.Windows.Forms.TreeView();
            this.GenerateReportBtn = new System.Windows.Forms.Button();
            this.ReportingPB = new System.Windows.Forms.ProgressBar();
            this.LinksPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveAsRtfLL = new System.Windows.Forms.LinkLabel();
            this.SaveAsHtmlLL = new System.Windows.Forms.LinkLabel();
            this.SaveReportDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.HostsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingTypesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingTitlesGrid)).BeginInit();
            this.LinksPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // HostsGrid
            // 
            this.HostsGrid.AllowUserToAddRows = false;
            this.HostsGrid.AllowUserToDeleteRows = false;
            this.HostsGrid.AllowUserToResizeColumns = false;
            this.HostsGrid.AllowUserToResizeRows = false;
            this.HostsGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HostsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.HostsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HostsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AskUserGridCheckColumn,
            this.AskUserGridValueColumn});
            this.HostsGrid.Location = new System.Drawing.Point(5, 45);
            this.HostsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.HostsGrid.MultiSelect = false;
            this.HostsGrid.Name = "HostsGrid";
            this.HostsGrid.ReadOnly = true;
            this.HostsGrid.RowHeadersVisible = false;
            this.HostsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HostsGrid.Size = new System.Drawing.Size(389, 148);
            this.HostsGrid.TabIndex = 5;
            this.HostsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HostsGrid_CellClick);
            // 
            // AskUserGridCheckColumn
            // 
            this.AskUserGridCheckColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.AskUserGridCheckColumn.HeaderText = "";
            this.AskUserGridCheckColumn.Name = "AskUserGridCheckColumn";
            this.AskUserGridCheckColumn.ReadOnly = true;
            this.AskUserGridCheckColumn.Width = 5;
            // 
            // AskUserGridValueColumn
            // 
            this.AskUserGridValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AskUserGridValueColumn.HeaderText = "Select Hosts to include in Report";
            this.AskUserGridValueColumn.Name = "AskUserGridValueColumn";
            this.AskUserGridValueColumn.ReadOnly = true;
            // 
            // FindingTypesGrid
            // 
            this.FindingTypesGrid.AllowUserToAddRows = false;
            this.FindingTypesGrid.AllowUserToDeleteRows = false;
            this.FindingTypesGrid.AllowUserToResizeColumns = false;
            this.FindingTypesGrid.AllowUserToResizeRows = false;
            this.FindingTypesGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FindingTypesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.FindingTypesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FindingTypesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1});
            this.FindingTypesGrid.Location = new System.Drawing.Point(5, 199);
            this.FindingTypesGrid.Margin = new System.Windows.Forms.Padding(0);
            this.FindingTypesGrid.MultiSelect = false;
            this.FindingTypesGrid.Name = "FindingTypesGrid";
            this.FindingTypesGrid.ReadOnly = true;
            this.FindingTypesGrid.RowHeadersVisible = false;
            this.FindingTypesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FindingTypesGrid.Size = new System.Drawing.Size(389, 90);
            this.FindingTypesGrid.TabIndex = 6;
            this.FindingTypesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FindingTypesGrid_CellClick);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Width = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Select Finding Types to include in Report";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // FindingTitlesGrid
            // 
            this.FindingTitlesGrid.AllowUserToAddRows = false;
            this.FindingTitlesGrid.AllowUserToDeleteRows = false;
            this.FindingTitlesGrid.AllowUserToResizeColumns = false;
            this.FindingTitlesGrid.AllowUserToResizeRows = false;
            this.FindingTitlesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FindingTitlesGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FindingTitlesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.FindingTitlesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FindingTitlesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewTextBoxColumn2});
            this.FindingTitlesGrid.Location = new System.Drawing.Point(5, 295);
            this.FindingTitlesGrid.Margin = new System.Windows.Forms.Padding(0);
            this.FindingTitlesGrid.MultiSelect = false;
            this.FindingTitlesGrid.Name = "FindingTitlesGrid";
            this.FindingTitlesGrid.ReadOnly = true;
            this.FindingTitlesGrid.RowHeadersVisible = false;
            this.FindingTitlesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FindingTitlesGrid.Size = new System.Drawing.Size(389, 260);
            this.FindingTitlesGrid.TabIndex = 7;
            this.FindingTitlesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FindingTitlesGrid_CellClick);
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dataGridViewCheckBoxColumn2.HeaderText = "";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Width = 5;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Select Finding Titles to include in Report";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // FindingsTree
            // 
            this.FindingsTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FindingsTree.BackColor = System.Drawing.Color.White;
            this.FindingsTree.CheckBoxes = true;
            this.FindingsTree.Location = new System.Drawing.Point(405, 45);
            this.FindingsTree.Margin = new System.Windows.Forms.Padding(0);
            this.FindingsTree.Name = "FindingsTree";
            this.FindingsTree.ShowRootLines = false;
            this.FindingsTree.Size = new System.Drawing.Size(475, 512);
            this.FindingsTree.TabIndex = 8;
            this.FindingsTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.FindingsTree_AfterCheck);
            // 
            // GenerateReportBtn
            // 
            this.GenerateReportBtn.Location = new System.Drawing.Point(5, 12);
            this.GenerateReportBtn.Name = "GenerateReportBtn";
            this.GenerateReportBtn.Size = new System.Drawing.Size(238, 23);
            this.GenerateReportBtn.TabIndex = 9;
            this.GenerateReportBtn.Text = "Generate Report for the Items selected below";
            this.GenerateReportBtn.UseVisualStyleBackColor = true;
            this.GenerateReportBtn.Click += new System.EventHandler(this.GenerateReportBtn_Click);
            // 
            // ReportingPB
            // 
            this.ReportingPB.Location = new System.Drawing.Point(578, 12);
            this.ReportingPB.Name = "ReportingPB";
            this.ReportingPB.Size = new System.Drawing.Size(302, 23);
            this.ReportingPB.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ReportingPB.TabIndex = 10;
            this.ReportingPB.Visible = false;
            // 
            // LinksPanel
            // 
            this.LinksPanel.Controls.Add(this.label1);
            this.LinksPanel.Controls.Add(this.SaveAsRtfLL);
            this.LinksPanel.Controls.Add(this.SaveAsHtmlLL);
            this.LinksPanel.Location = new System.Drawing.Point(249, 12);
            this.LinksPanel.Name = "LinksPanel";
            this.LinksPanel.Size = new System.Drawing.Size(323, 23);
            this.LinksPanel.TabIndex = 11;
            this.LinksPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Save generated report as - ";
            // 
            // SaveAsRtfLL
            // 
            this.SaveAsRtfLL.AutoSize = true;
            this.SaveAsRtfLL.Location = new System.Drawing.Point(210, 5);
            this.SaveAsRtfLL.Name = "SaveAsRtfLL";
            this.SaveAsRtfLL.Size = new System.Drawing.Size(44, 13);
            this.SaveAsRtfLL.TabIndex = 1;
            this.SaveAsRtfLL.TabStop = true;
            this.SaveAsRtfLL.Text = "RTF file";
            this.SaveAsRtfLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SaveAsRtfLL_LinkClicked);
            // 
            // SaveAsHtmlLL
            // 
            this.SaveAsHtmlLL.AutoSize = true;
            this.SaveAsHtmlLL.Location = new System.Drawing.Point(138, 5);
            this.SaveAsHtmlLL.Name = "SaveAsHtmlLL";
            this.SaveAsHtmlLL.Size = new System.Drawing.Size(53, 13);
            this.SaveAsHtmlLL.TabIndex = 0;
            this.SaveAsHtmlLL.TabStop = true;
            this.SaveAsHtmlLL.Text = "HTML file";
            this.SaveAsHtmlLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SaveAsHtmlLL_LinkClicked);
            // 
            // ReportGenerationWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.LinksPanel);
            this.Controls.Add(this.ReportingPB);
            this.Controls.Add(this.GenerateReportBtn);
            this.Controls.Add(this.FindingsTree);
            this.Controls.Add(this.FindingTitlesGrid);
            this.Controls.Add(this.FindingTypesGrid);
            this.Controls.Add(this.HostsGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportGenerationWizard";
            this.Text = "Generate Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportGenerationWizard_FormClosing);
            this.Load += new System.EventHandler(this.ReportGenerationWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HostsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingTypesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingTitlesGrid)).EndInit();
            this.LinksPanel.ResumeLayout(false);
            this.LinksPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView HostsGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AskUserGridCheckColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AskUserGridValueColumn;
        internal System.Windows.Forms.DataGridView FindingTypesGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        internal System.Windows.Forms.DataGridView FindingTitlesGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        public System.Windows.Forms.TreeView FindingsTree;
        private System.Windows.Forms.Button GenerateReportBtn;
        private System.Windows.Forms.ProgressBar ReportingPB;
        private System.Windows.Forms.Panel LinksPanel;
        private System.Windows.Forms.LinkLabel SaveAsRtfLL;
        private System.Windows.Forms.LinkLabel SaveAsHtmlLL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog SaveReportDialog;
    }
}