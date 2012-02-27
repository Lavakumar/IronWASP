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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
//

namespace IronWASP
{
    partial class DiffWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiffWindow));
            this.SideBySideSplit = new System.Windows.Forms.SplitContainer();
            this.SourceResultRTB = new System.Windows.Forms.RichTextBox();
            this.DestinationResultRTB = new System.Windows.Forms.RichTextBox();
            this.DiffWindowLeftSplit = new System.Windows.Forms.SplitContainer();
            this.DiffSourceTB = new System.Windows.Forms.TextBox();
            this.PasteSourceBtn = new System.Windows.Forms.Button();
            this.ClearSourceBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DiffDestinationTB = new System.Windows.Forms.TextBox();
            this.PasteDestinationBtn = new System.Windows.Forms.Button();
            this.ClearDestinationBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.DiffStatusTB = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DiffWindowShowDiffBtn = new System.Windows.Forms.Button();
            this.DiffResultRTB = new System.Windows.Forms.RichTextBox();
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.InputTab = new System.Windows.Forms.TabPage();
            this.ResultsTab = new System.Windows.Forms.TabPage();
            this.ResultsTabs = new System.Windows.Forms.TabControl();
            this.SideBySideTab = new System.Windows.Forms.TabPage();
            this.SinglePageTab = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SideBySideSplit.Panel1.SuspendLayout();
            this.SideBySideSplit.Panel2.SuspendLayout();
            this.SideBySideSplit.SuspendLayout();
            this.DiffWindowLeftSplit.Panel1.SuspendLayout();
            this.DiffWindowLeftSplit.Panel2.SuspendLayout();
            this.DiffWindowLeftSplit.SuspendLayout();
            this.BaseTabs.SuspendLayout();
            this.InputTab.SuspendLayout();
            this.ResultsTab.SuspendLayout();
            this.ResultsTabs.SuspendLayout();
            this.SideBySideTab.SuspendLayout();
            this.SinglePageTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // SideBySideSplit
            // 
            this.SideBySideSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SideBySideSplit.Location = new System.Drawing.Point(0, 0);
            this.SideBySideSplit.Margin = new System.Windows.Forms.Padding(0);
            this.SideBySideSplit.Name = "SideBySideSplit";
            // 
            // SideBySideSplit.Panel1
            // 
            this.SideBySideSplit.Panel1.Controls.Add(this.SourceResultRTB);
            // 
            // SideBySideSplit.Panel2
            // 
            this.SideBySideSplit.Panel2.Controls.Add(this.DestinationResultRTB);
            this.SideBySideSplit.Size = new System.Drawing.Size(668, 480);
            this.SideBySideSplit.SplitterDistance = 320;
            this.SideBySideSplit.SplitterWidth = 2;
            this.SideBySideSplit.TabIndex = 0;
            // 
            // SourceResultRTB
            // 
            this.SourceResultRTB.BackColor = System.Drawing.Color.White;
            this.SourceResultRTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SourceResultRTB.DetectUrls = false;
            this.SourceResultRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceResultRTB.Location = new System.Drawing.Point(0, 0);
            this.SourceResultRTB.Name = "SourceResultRTB";
            this.SourceResultRTB.ReadOnly = true;
            this.SourceResultRTB.Size = new System.Drawing.Size(320, 480);
            this.SourceResultRTB.TabIndex = 1;
            this.SourceResultRTB.Text = "";
            // 
            // DestinationResultRTB
            // 
            this.DestinationResultRTB.BackColor = System.Drawing.Color.White;
            this.DestinationResultRTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DestinationResultRTB.DetectUrls = false;
            this.DestinationResultRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DestinationResultRTB.Location = new System.Drawing.Point(0, 0);
            this.DestinationResultRTB.Name = "DestinationResultRTB";
            this.DestinationResultRTB.ReadOnly = true;
            this.DestinationResultRTB.Size = new System.Drawing.Size(346, 480);
            this.DestinationResultRTB.TabIndex = 1;
            this.DestinationResultRTB.Text = "";
            // 
            // DiffWindowLeftSplit
            // 
            this.DiffWindowLeftSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiffWindowLeftSplit.Location = new System.Drawing.Point(0, 0);
            this.DiffWindowLeftSplit.Margin = new System.Windows.Forms.Padding(0);
            this.DiffWindowLeftSplit.Name = "DiffWindowLeftSplit";
            this.DiffWindowLeftSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // DiffWindowLeftSplit.Panel1
            // 
            this.DiffWindowLeftSplit.Panel1.Controls.Add(this.DiffSourceTB);
            this.DiffWindowLeftSplit.Panel1.Controls.Add(this.PasteSourceBtn);
            this.DiffWindowLeftSplit.Panel1.Controls.Add(this.ClearSourceBtn);
            this.DiffWindowLeftSplit.Panel1.Controls.Add(this.label3);
            // 
            // DiffWindowLeftSplit.Panel2
            // 
            this.DiffWindowLeftSplit.Panel2.Controls.Add(this.DiffDestinationTB);
            this.DiffWindowLeftSplit.Panel2.Controls.Add(this.PasteDestinationBtn);
            this.DiffWindowLeftSplit.Panel2.Controls.Add(this.ClearDestinationBtn);
            this.DiffWindowLeftSplit.Panel2.Controls.Add(this.label4);
            this.DiffWindowLeftSplit.Size = new System.Drawing.Size(676, 506);
            this.DiffWindowLeftSplit.SplitterDistance = 259;
            this.DiffWindowLeftSplit.SplitterWidth = 2;
            this.DiffWindowLeftSplit.TabIndex = 0;
            // 
            // DiffSourceTB
            // 
            this.DiffSourceTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DiffSourceTB.Location = new System.Drawing.Point(0, 25);
            this.DiffSourceTB.MaxLength = 2147483647;
            this.DiffSourceTB.Multiline = true;
            this.DiffSourceTB.Name = "DiffSourceTB";
            this.DiffSourceTB.Size = new System.Drawing.Size(676, 234);
            this.DiffSourceTB.TabIndex = 3;
            // 
            // PasteSourceBtn
            // 
            this.PasteSourceBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasteSourceBtn.Location = new System.Drawing.Point(518, 2);
            this.PasteSourceBtn.Margin = new System.Windows.Forms.Padding(0);
            this.PasteSourceBtn.Name = "PasteSourceBtn";
            this.PasteSourceBtn.Size = new System.Drawing.Size(75, 22);
            this.PasteSourceBtn.TabIndex = 2;
            this.PasteSourceBtn.Text = "Paste";
            this.PasteSourceBtn.UseVisualStyleBackColor = true;
            this.PasteSourceBtn.Click += new System.EventHandler(this.PasteSourceBtn_Click);
            // 
            // ClearSourceBtn
            // 
            this.ClearSourceBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearSourceBtn.Location = new System.Drawing.Point(598, 2);
            this.ClearSourceBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ClearSourceBtn.Name = "ClearSourceBtn";
            this.ClearSourceBtn.Size = new System.Drawing.Size(75, 22);
            this.ClearSourceBtn.TabIndex = 1;
            this.ClearSourceBtn.Text = "Clear";
            this.ClearSourceBtn.UseVisualStyleBackColor = true;
            this.ClearSourceBtn.Click += new System.EventHandler(this.ClearSourceBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Enter Source Text:";
            // 
            // DiffDestinationTB
            // 
            this.DiffDestinationTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DiffDestinationTB.BackColor = System.Drawing.SystemColors.Window;
            this.DiffDestinationTB.Location = new System.Drawing.Point(0, 25);
            this.DiffDestinationTB.MaxLength = 2147483647;
            this.DiffDestinationTB.Multiline = true;
            this.DiffDestinationTB.Name = "DiffDestinationTB";
            this.DiffDestinationTB.ReadOnly = true;
            this.DiffDestinationTB.Size = new System.Drawing.Size(676, 220);
            this.DiffDestinationTB.TabIndex = 4;
            // 
            // PasteDestinationBtn
            // 
            this.PasteDestinationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasteDestinationBtn.Location = new System.Drawing.Point(518, 2);
            this.PasteDestinationBtn.Margin = new System.Windows.Forms.Padding(0);
            this.PasteDestinationBtn.Name = "PasteDestinationBtn";
            this.PasteDestinationBtn.Size = new System.Drawing.Size(75, 22);
            this.PasteDestinationBtn.TabIndex = 3;
            this.PasteDestinationBtn.Text = "Paste";
            this.PasteDestinationBtn.UseVisualStyleBackColor = true;
            this.PasteDestinationBtn.Click += new System.EventHandler(this.PasteDestinationBtn_Click);
            // 
            // ClearDestinationBtn
            // 
            this.ClearDestinationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearDestinationBtn.Location = new System.Drawing.Point(598, 2);
            this.ClearDestinationBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ClearDestinationBtn.Name = "ClearDestinationBtn";
            this.ClearDestinationBtn.Size = new System.Drawing.Size(75, 22);
            this.ClearDestinationBtn.TabIndex = 2;
            this.ClearDestinationBtn.Text = "Clear";
            this.ClearDestinationBtn.UseVisualStyleBackColor = true;
            this.ClearDestinationBtn.Click += new System.EventHandler(this.ClearDestinationBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Enter Destination Text:";
            // 
            // DiffStatusTB
            // 
            this.DiffStatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DiffStatusTB.BackColor = System.Drawing.SystemColors.Control;
            this.DiffStatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DiffStatusTB.Location = new System.Drawing.Point(92, 4);
            this.DiffStatusTB.Multiline = true;
            this.DiffStatusTB.Name = "DiffStatusTB";
            this.DiffStatusTB.ReadOnly = true;
            this.DiffStatusTB.Size = new System.Drawing.Size(400, 26);
            this.DiffStatusTB.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.LimeGreen;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(498, 1);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(30, 13);
            this.textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Orange;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(602, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(30, 13);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(534, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Inserted";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Deleted";
            // 
            // DiffWindowShowDiffBtn
            // 
            this.DiffWindowShowDiffBtn.Location = new System.Drawing.Point(3, 3);
            this.DiffWindowShowDiffBtn.Name = "DiffWindowShowDiffBtn";
            this.DiffWindowShowDiffBtn.Size = new System.Drawing.Size(83, 23);
            this.DiffWindowShowDiffBtn.TabIndex = 1;
            this.DiffWindowShowDiffBtn.Text = "Show Diff";
            this.DiffWindowShowDiffBtn.UseVisualStyleBackColor = true;
            this.DiffWindowShowDiffBtn.Click += new System.EventHandler(this.DiffWindowShowDiffBtn_Click);
            // 
            // DiffResultRTB
            // 
            this.DiffResultRTB.BackColor = System.Drawing.Color.White;
            this.DiffResultRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DiffResultRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiffResultRTB.Location = new System.Drawing.Point(0, 0);
            this.DiffResultRTB.Name = "DiffResultRTB";
            this.DiffResultRTB.ReadOnly = true;
            this.DiffResultRTB.Size = new System.Drawing.Size(668, 480);
            this.DiffResultRTB.TabIndex = 0;
            this.DiffResultRTB.Text = "";
            // 
            // BaseTabs
            // 
            this.BaseTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseTabs.Controls.Add(this.InputTab);
            this.BaseTabs.Controls.Add(this.ResultsTab);
            this.BaseTabs.Location = new System.Drawing.Point(0, 30);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(684, 532);
            this.BaseTabs.TabIndex = 1;
            // 
            // InputTab
            // 
            this.InputTab.Controls.Add(this.DiffWindowLeftSplit);
            this.InputTab.Location = new System.Drawing.Point(4, 22);
            this.InputTab.Margin = new System.Windows.Forms.Padding(0);
            this.InputTab.Name = "InputTab";
            this.InputTab.Size = new System.Drawing.Size(676, 506);
            this.InputTab.TabIndex = 0;
            this.InputTab.Text = "Input";
            this.InputTab.UseVisualStyleBackColor = true;
            // 
            // ResultsTab
            // 
            this.ResultsTab.Controls.Add(this.ResultsTabs);
            this.ResultsTab.Location = new System.Drawing.Point(4, 22);
            this.ResultsTab.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTab.Name = "ResultsTab";
            this.ResultsTab.Size = new System.Drawing.Size(676, 506);
            this.ResultsTab.TabIndex = 1;
            this.ResultsTab.Text = "Results";
            this.ResultsTab.UseVisualStyleBackColor = true;
            // 
            // ResultsTabs
            // 
            this.ResultsTabs.Controls.Add(this.SideBySideTab);
            this.ResultsTabs.Controls.Add(this.SinglePageTab);
            this.ResultsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTabs.Location = new System.Drawing.Point(0, 0);
            this.ResultsTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTabs.Name = "ResultsTabs";
            this.ResultsTabs.Padding = new System.Drawing.Point(0, 0);
            this.ResultsTabs.SelectedIndex = 0;
            this.ResultsTabs.Size = new System.Drawing.Size(676, 506);
            this.ResultsTabs.TabIndex = 0;
            // 
            // SideBySideTab
            // 
            this.SideBySideTab.Controls.Add(this.SideBySideSplit);
            this.SideBySideTab.Location = new System.Drawing.Point(4, 22);
            this.SideBySideTab.Margin = new System.Windows.Forms.Padding(0);
            this.SideBySideTab.Name = "SideBySideTab";
            this.SideBySideTab.Size = new System.Drawing.Size(668, 480);
            this.SideBySideTab.TabIndex = 0;
            this.SideBySideTab.Text = "Side By Side";
            this.SideBySideTab.UseVisualStyleBackColor = true;
            // 
            // SinglePageTab
            // 
            this.SinglePageTab.Controls.Add(this.DiffResultRTB);
            this.SinglePageTab.Location = new System.Drawing.Point(4, 22);
            this.SinglePageTab.Margin = new System.Windows.Forms.Padding(0);
            this.SinglePageTab.Name = "SinglePageTab";
            this.SinglePageTab.Size = new System.Drawing.Size(668, 480);
            this.SinglePageTab.TabIndex = 1;
            this.SinglePageTab.Text = "Single Page";
            this.SinglePageTab.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.Color.Gray;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(602, 17);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(30, 13);
            this.textBox3.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(498, 17);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(30, 13);
            this.textBox4.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(638, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Filler";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(534, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Unchanged";
            // 
            // DiffWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 562);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.DiffStatusTB);
            this.Controls.Add(this.BaseTabs);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.DiffWindowShowDiffBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiffWindow";
            this.Text = "View Diff in Text between a Source and Destination.";
            this.SideBySideSplit.Panel1.ResumeLayout(false);
            this.SideBySideSplit.Panel2.ResumeLayout(false);
            this.SideBySideSplit.ResumeLayout(false);
            this.DiffWindowLeftSplit.Panel1.ResumeLayout(false);
            this.DiffWindowLeftSplit.Panel1.PerformLayout();
            this.DiffWindowLeftSplit.Panel2.ResumeLayout(false);
            this.DiffWindowLeftSplit.Panel2.PerformLayout();
            this.DiffWindowLeftSplit.ResumeLayout(false);
            this.BaseTabs.ResumeLayout(false);
            this.InputTab.ResumeLayout(false);
            this.ResultsTab.ResumeLayout(false);
            this.ResultsTabs.ResumeLayout(false);
            this.SideBySideTab.ResumeLayout(false);
            this.SinglePageTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer SideBySideSplit;
        private System.Windows.Forms.SplitContainer DiffWindowLeftSplit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.RichTextBox DiffResultRTB;
        private System.Windows.Forms.TabPage InputTab;
        private System.Windows.Forms.TabPage ResultsTab;
        private System.Windows.Forms.TabControl ResultsTabs;
        private System.Windows.Forms.TabPage SideBySideTab;
        private System.Windows.Forms.TabPage SinglePageTab;
        internal System.Windows.Forms.RichTextBox SourceResultRTB;
        internal System.Windows.Forms.RichTextBox DestinationResultRTB;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TabControl BaseTabs;
        internal System.Windows.Forms.TextBox DiffStatusTB;
        internal System.Windows.Forms.Button DiffWindowShowDiffBtn;
        private System.Windows.Forms.Button ClearSourceBtn;
        private System.Windows.Forms.Button ClearDestinationBtn;
        private System.Windows.Forms.Button PasteSourceBtn;
        private System.Windows.Forms.Button PasteDestinationBtn;
        internal System.Windows.Forms.TextBox DiffSourceTB;
        internal System.Windows.Forms.TextBox DiffDestinationTB;
    }
}