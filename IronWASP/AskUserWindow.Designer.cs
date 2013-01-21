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
    partial class AskUserWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AskUserWindow));
            this.AskUserBaseSplit = new System.Windows.Forms.SplitContainer();
            this.AskUserMessageRTB = new System.Windows.Forms.RichTextBox();
            this.AskUserRightSplit = new System.Windows.Forms.SplitContainer();
            this.AskUserPB = new System.Windows.Forms.PictureBox();
            this.AskUserAnswerLbl = new System.Windows.Forms.Label();
            this.AskUserAnswerRBTwo = new System.Windows.Forms.RadioButton();
            this.AskUserAnswerRBOne = new System.Windows.Forms.RadioButton();
            this.AskUserAnswerGrid = new System.Windows.Forms.DataGridView();
            this.AskUserGridCheckColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AskUserGridValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskUserSubmitBtn = new System.Windows.Forms.Button();
            this.AskUserNoBtn = new System.Windows.Forms.Button();
            this.AskUserYesBtn = new System.Windows.Forms.Button();
            this.AskUserAnswerTB = new System.Windows.Forms.TextBox();
            this.AskUserBaseSplit.Panel1.SuspendLayout();
            this.AskUserBaseSplit.Panel2.SuspendLayout();
            this.AskUserBaseSplit.SuspendLayout();
            this.AskUserRightSplit.Panel1.SuspendLayout();
            this.AskUserRightSplit.Panel2.SuspendLayout();
            this.AskUserRightSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AskUserPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AskUserAnswerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // AskUserBaseSplit
            // 
            this.AskUserBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AskUserBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.AskUserBaseSplit.Name = "AskUserBaseSplit";
            this.AskUserBaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // AskUserBaseSplit.Panel1
            // 
            this.AskUserBaseSplit.Panel1.Controls.Add(this.AskUserMessageRTB);
            // 
            // AskUserBaseSplit.Panel2
            // 
            this.AskUserBaseSplit.Panel2.Controls.Add(this.AskUserRightSplit);
            this.AskUserBaseSplit.Size = new System.Drawing.Size(784, 312);
            this.AskUserBaseSplit.SplitterDistance = 151;
            this.AskUserBaseSplit.SplitterWidth = 2;
            this.AskUserBaseSplit.TabIndex = 0;
            // 
            // AskUserMessageRTB
            // 
            this.AskUserMessageRTB.BackColor = System.Drawing.Color.White;
            this.AskUserMessageRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AskUserMessageRTB.DetectUrls = false;
            this.AskUserMessageRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AskUserMessageRTB.Location = new System.Drawing.Point(0, 0);
            this.AskUserMessageRTB.Name = "AskUserMessageRTB";
            this.AskUserMessageRTB.ReadOnly = true;
            this.AskUserMessageRTB.Size = new System.Drawing.Size(784, 151);
            this.AskUserMessageRTB.TabIndex = 0;
            this.AskUserMessageRTB.Text = "";
            // 
            // AskUserRightSplit
            // 
            this.AskUserRightSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AskUserRightSplit.Location = new System.Drawing.Point(0, 0);
            this.AskUserRightSplit.Name = "AskUserRightSplit";
            // 
            // AskUserRightSplit.Panel1
            // 
            this.AskUserRightSplit.Panel1.Controls.Add(this.AskUserPB);
            // 
            // AskUserRightSplit.Panel2
            // 
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserAnswerLbl);
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserAnswerRBTwo);
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserAnswerRBOne);
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserAnswerGrid);
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserSubmitBtn);
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserNoBtn);
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserYesBtn);
            this.AskUserRightSplit.Panel2.Controls.Add(this.AskUserAnswerTB);
            this.AskUserRightSplit.Size = new System.Drawing.Size(784, 159);
            this.AskUserRightSplit.SplitterDistance = 385;
            this.AskUserRightSplit.SplitterWidth = 2;
            this.AskUserRightSplit.TabIndex = 0;
            // 
            // AskUserPB
            // 
            this.AskUserPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AskUserPB.Location = new System.Drawing.Point(0, 0);
            this.AskUserPB.Name = "AskUserPB";
            this.AskUserPB.Size = new System.Drawing.Size(385, 159);
            this.AskUserPB.TabIndex = 0;
            this.AskUserPB.TabStop = false;
            // 
            // AskUserAnswerLbl
            // 
            this.AskUserAnswerLbl.AutoSize = true;
            this.AskUserAnswerLbl.Location = new System.Drawing.Point(0, 44);
            this.AskUserAnswerLbl.Margin = new System.Windows.Forms.Padding(0);
            this.AskUserAnswerLbl.Name = "AskUserAnswerLbl";
            this.AskUserAnswerLbl.Size = new System.Drawing.Size(96, 13);
            this.AskUserAnswerLbl.TabIndex = 7;
            this.AskUserAnswerLbl.Text = "AskUserAnswerLbl";
            // 
            // AskUserAnswerRBTwo
            // 
            this.AskUserAnswerRBTwo.AutoSize = true;
            this.AskUserAnswerRBTwo.Location = new System.Drawing.Point(0, 22);
            this.AskUserAnswerRBTwo.Name = "AskUserAnswerRBTwo";
            this.AskUserAnswerRBTwo.Size = new System.Drawing.Size(46, 17);
            this.AskUserAnswerRBTwo.TabIndex = 6;
            this.AskUserAnswerRBTwo.TabStop = true;
            this.AskUserAnswerRBTwo.Text = "Two";
            this.AskUserAnswerRBTwo.UseVisualStyleBackColor = true;
            this.AskUserAnswerRBTwo.Click += new System.EventHandler(this.AskUserAnswerRBTwo_Click);
            // 
            // AskUserAnswerRBOne
            // 
            this.AskUserAnswerRBOne.AutoSize = true;
            this.AskUserAnswerRBOne.Location = new System.Drawing.Point(0, 2);
            this.AskUserAnswerRBOne.Margin = new System.Windows.Forms.Padding(0);
            this.AskUserAnswerRBOne.Name = "AskUserAnswerRBOne";
            this.AskUserAnswerRBOne.Size = new System.Drawing.Size(45, 17);
            this.AskUserAnswerRBOne.TabIndex = 5;
            this.AskUserAnswerRBOne.TabStop = true;
            this.AskUserAnswerRBOne.Text = "One";
            this.AskUserAnswerRBOne.UseVisualStyleBackColor = true;
            // 
            // AskUserAnswerGrid
            // 
            this.AskUserAnswerGrid.AllowUserToAddRows = false;
            this.AskUserAnswerGrid.AllowUserToDeleteRows = false;
            this.AskUserAnswerGrid.AllowUserToResizeColumns = false;
            this.AskUserAnswerGrid.AllowUserToResizeRows = false;
            this.AskUserAnswerGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AskUserAnswerGrid.BackgroundColor = System.Drawing.Color.White;
            this.AskUserAnswerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AskUserAnswerGrid.ColumnHeadersVisible = false;
            this.AskUserAnswerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AskUserGridCheckColumn,
            this.AskUserGridValueColumn});
            this.AskUserAnswerGrid.Location = new System.Drawing.Point(0, 57);
            this.AskUserAnswerGrid.Margin = new System.Windows.Forms.Padding(0);
            this.AskUserAnswerGrid.MultiSelect = false;
            this.AskUserAnswerGrid.Name = "AskUserAnswerGrid";
            this.AskUserAnswerGrid.RowHeadersVisible = false;
            this.AskUserAnswerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AskUserAnswerGrid.Size = new System.Drawing.Size(397, 67);
            this.AskUserAnswerGrid.TabIndex = 4;
            this.AskUserAnswerGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AskUserAnswerGrid_CellContentClick);
            // 
            // AskUserGridCheckColumn
            // 
            this.AskUserGridCheckColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.AskUserGridCheckColumn.HeaderText = "";
            this.AskUserGridCheckColumn.Name = "AskUserGridCheckColumn";
            this.AskUserGridCheckColumn.Width = 5;
            // 
            // AskUserGridValueColumn
            // 
            this.AskUserGridValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AskUserGridValueColumn.HeaderText = "";
            this.AskUserGridValueColumn.Name = "AskUserGridValueColumn";
            this.AskUserGridValueColumn.ReadOnly = true;
            // 
            // AskUserSubmitBtn
            // 
            this.AskUserSubmitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AskUserSubmitBtn.Location = new System.Drawing.Point(316, 133);
            this.AskUserSubmitBtn.Name = "AskUserSubmitBtn";
            this.AskUserSubmitBtn.Size = new System.Drawing.Size(75, 23);
            this.AskUserSubmitBtn.TabIndex = 3;
            this.AskUserSubmitBtn.Text = "Submit";
            this.AskUserSubmitBtn.UseVisualStyleBackColor = true;
            this.AskUserSubmitBtn.Click += new System.EventHandler(this.AskUserSubmitBtn_Click);
            // 
            // AskUserNoBtn
            // 
            this.AskUserNoBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AskUserNoBtn.Location = new System.Drawing.Point(232, 133);
            this.AskUserNoBtn.Name = "AskUserNoBtn";
            this.AskUserNoBtn.Size = new System.Drawing.Size(75, 23);
            this.AskUserNoBtn.TabIndex = 2;
            this.AskUserNoBtn.Text = "No";
            this.AskUserNoBtn.UseVisualStyleBackColor = true;
            this.AskUserNoBtn.Click += new System.EventHandler(this.AskUserNoBtn_Click);
            // 
            // AskUserYesBtn
            // 
            this.AskUserYesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AskUserYesBtn.Location = new System.Drawing.Point(149, 133);
            this.AskUserYesBtn.Name = "AskUserYesBtn";
            this.AskUserYesBtn.Size = new System.Drawing.Size(75, 23);
            this.AskUserYesBtn.TabIndex = 1;
            this.AskUserYesBtn.Text = "Yes";
            this.AskUserYesBtn.UseVisualStyleBackColor = true;
            this.AskUserYesBtn.Click += new System.EventHandler(this.AskUserYesBtn_Click);
            // 
            // AskUserAnswerTB
            // 
            this.AskUserAnswerTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AskUserAnswerTB.BackColor = System.Drawing.Color.White;
            this.AskUserAnswerTB.Location = new System.Drawing.Point(0, 0);
            this.AskUserAnswerTB.Margin = new System.Windows.Forms.Padding(0);
            this.AskUserAnswerTB.MaxLength = 2147483647;
            this.AskUserAnswerTB.Multiline = true;
            this.AskUserAnswerTB.Name = "AskUserAnswerTB";
            this.AskUserAnswerTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AskUserAnswerTB.Size = new System.Drawing.Size(397, 129);
            this.AskUserAnswerTB.TabIndex = 0;
            // 
            // AskUserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 312);
            this.Controls.Add(this.AskUserBaseSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(50, 50);
            this.Name = "AskUserWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "0/0";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AskUserWindow_FormClosing);
            this.AskUserBaseSplit.Panel1.ResumeLayout(false);
            this.AskUserBaseSplit.Panel2.ResumeLayout(false);
            this.AskUserBaseSplit.ResumeLayout(false);
            this.AskUserRightSplit.Panel1.ResumeLayout(false);
            this.AskUserRightSplit.Panel2.ResumeLayout(false);
            this.AskUserRightSplit.Panel2.PerformLayout();
            this.AskUserRightSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AskUserPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AskUserAnswerGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer AskUserBaseSplit;
        private System.Windows.Forms.SplitContainer AskUserRightSplit;
        internal System.Windows.Forms.TextBox AskUserAnswerTB;
        internal System.Windows.Forms.PictureBox AskUserPB;
        internal System.Windows.Forms.Button AskUserYesBtn;
        internal System.Windows.Forms.Button AskUserNoBtn;
        internal System.Windows.Forms.Button AskUserSubmitBtn;
        internal System.Windows.Forms.RichTextBox AskUserMessageRTB;
        internal System.Windows.Forms.DataGridView AskUserAnswerGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AskUserGridCheckColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AskUserGridValueColumn;
        internal System.Windows.Forms.RadioButton AskUserAnswerRBOne;
        internal System.Windows.Forms.RadioButton AskUserAnswerRBTwo;
        internal System.Windows.Forms.Label AskUserAnswerLbl;
    }
}