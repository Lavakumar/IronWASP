namespace IronWASP
{
    partial class DiffResultView
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
            this.ResultsTabs = new System.Windows.Forms.TabControl();
            this.SinglePageTab = new System.Windows.Forms.TabPage();
            this.DiffResultRTB = new System.Windows.Forms.RichTextBox();
            this.SideBySideTab = new System.Windows.Forms.TabPage();
            this.SideBySideSplit = new System.Windows.Forms.SplitContainer();
            this.SourceResultRTB = new System.Windows.Forms.RichTextBox();
            this.DestinationResultRTB = new System.Windows.Forms.RichTextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ResultsTabs.SuspendLayout();
            this.SinglePageTab.SuspendLayout();
            this.SideBySideTab.SuspendLayout();
            this.SideBySideSplit.Panel1.SuspendLayout();
            this.SideBySideSplit.Panel2.SuspendLayout();
            this.SideBySideSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResultsTabs
            // 
            this.ResultsTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsTabs.Controls.Add(this.SinglePageTab);
            this.ResultsTabs.Controls.Add(this.SideBySideTab);
            this.ResultsTabs.Location = new System.Drawing.Point(0, 20);
            this.ResultsTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTabs.Name = "ResultsTabs";
            this.ResultsTabs.Padding = new System.Drawing.Point(0, 0);
            this.ResultsTabs.SelectedIndex = 0;
            this.ResultsTabs.Size = new System.Drawing.Size(400, 160);
            this.ResultsTabs.TabIndex = 2;
            // 
            // SinglePageTab
            // 
            this.SinglePageTab.Controls.Add(this.DiffResultRTB);
            this.SinglePageTab.Location = new System.Drawing.Point(4, 22);
            this.SinglePageTab.Margin = new System.Windows.Forms.Padding(0);
            this.SinglePageTab.Name = "SinglePageTab";
            this.SinglePageTab.Size = new System.Drawing.Size(392, 134);
            this.SinglePageTab.TabIndex = 1;
            this.SinglePageTab.Text = "  Single Page  ";
            this.SinglePageTab.UseVisualStyleBackColor = true;
            // 
            // DiffResultRTB
            // 
            this.DiffResultRTB.BackColor = System.Drawing.Color.White;
            this.DiffResultRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DiffResultRTB.DetectUrls = false;
            this.DiffResultRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiffResultRTB.Location = new System.Drawing.Point(0, 0);
            this.DiffResultRTB.Name = "DiffResultRTB";
            this.DiffResultRTB.ReadOnly = true;
            this.DiffResultRTB.Size = new System.Drawing.Size(392, 134);
            this.DiffResultRTB.TabIndex = 0;
            this.DiffResultRTB.Text = "";
            // 
            // SideBySideTab
            // 
            this.SideBySideTab.Controls.Add(this.SideBySideSplit);
            this.SideBySideTab.Location = new System.Drawing.Point(4, 22);
            this.SideBySideTab.Margin = new System.Windows.Forms.Padding(0);
            this.SideBySideTab.Name = "SideBySideTab";
            this.SideBySideTab.Size = new System.Drawing.Size(392, 134);
            this.SideBySideTab.TabIndex = 0;
            this.SideBySideTab.Text = "  Side By Side  ";
            this.SideBySideTab.UseVisualStyleBackColor = true;
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
            this.SideBySideSplit.Size = new System.Drawing.Size(392, 134);
            this.SideBySideSplit.SplitterDistance = 187;
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
            this.SourceResultRTB.Size = new System.Drawing.Size(187, 134);
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
            this.DestinationResultRTB.Size = new System.Drawing.Size(203, 134);
            this.DestinationResultRTB.TabIndex = 1;
            this.DestinationResultRTB.Text = "";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.Color.Gray;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(330, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(30, 13);
            this.textBox3.TabIndex = 17;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(213, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(30, 13);
            this.textBox4.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Filler";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Unchanged";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Orange;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(118, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(30, 13);
            this.textBox1.TabIndex = 13;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.LimeGreen;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(19, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(30, 13);
            this.textBox2.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Deleted";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Inserted";
            // 
            // DiffResultView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ResultsTabs);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DiffResultView";
            this.Size = new System.Drawing.Size(400, 180);
            this.ResultsTabs.ResumeLayout(false);
            this.SinglePageTab.ResumeLayout(false);
            this.SideBySideTab.ResumeLayout(false);
            this.SideBySideSplit.Panel1.ResumeLayout(false);
            this.SideBySideSplit.Panel2.ResumeLayout(false);
            this.SideBySideSplit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl ResultsTabs;
        private System.Windows.Forms.TabPage SideBySideTab;
        private System.Windows.Forms.SplitContainer SideBySideSplit;
        internal System.Windows.Forms.RichTextBox SourceResultRTB;
        internal System.Windows.Forms.RichTextBox DestinationResultRTB;
        private System.Windows.Forms.TabPage SinglePageTab;
        internal System.Windows.Forms.RichTextBox DiffResultRTB;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
