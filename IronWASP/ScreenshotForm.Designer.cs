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
    partial class ScreenshotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenshotForm));
            this.TakeScreenshotBtn = new System.Windows.Forms.Button();
            this.SaveScreenshotDialog = new System.Windows.Forms.SaveFileDialog();
            this.ReqResSplit = new System.Windows.Forms.SplitContainer();
            this.RequestRTB = new System.Windows.Forms.RichTextBox();
            this.ResponseRTB = new System.Windows.Forms.RichTextBox();
            this.TextSizeDownBtn = new System.Windows.Forms.Button();
            this.TextSizeUpBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AlignTopBottomRB = new System.Windows.Forms.RadioButton();
            this.AlignLeftRightRB = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.FontColorBtn = new System.Windows.Forms.Button();
            this.FontHighlightBtn = new System.Windows.Forms.Button();
            this.FontColorDialog = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectedTextSizeUpBtn = new System.Windows.Forms.Button();
            this.SelectedTextSizeDownBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.RootSplit = new System.Windows.Forms.SplitContainer();
            this.SnipSelectedTextBtn = new System.Windows.Forms.Button();
            this.ReqResSplit.Panel1.SuspendLayout();
            this.ReqResSplit.Panel2.SuspendLayout();
            this.ReqResSplit.SuspendLayout();
            this.RootSplit.Panel1.SuspendLayout();
            this.RootSplit.Panel2.SuspendLayout();
            this.RootSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // TakeScreenshotBtn
            // 
            this.TakeScreenshotBtn.Location = new System.Drawing.Point(3, 8);
            this.TakeScreenshotBtn.Name = "TakeScreenshotBtn";
            this.TakeScreenshotBtn.Size = new System.Drawing.Size(148, 23);
            this.TakeScreenshotBtn.TabIndex = 0;
            this.TakeScreenshotBtn.Text = "Take Screenshot";
            this.TakeScreenshotBtn.UseVisualStyleBackColor = true;
            this.TakeScreenshotBtn.Click += new System.EventHandler(this.TakeScreenshotBtn_Click);
            // 
            // SaveScreenshotDialog
            // 
            this.SaveScreenshotDialog.DefaultExt = "jpg";
            // 
            // ReqResSplit
            // 
            this.ReqResSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReqResSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReqResSplit.Location = new System.Drawing.Point(0, 0);
            this.ReqResSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ReqResSplit.Name = "ReqResSplit";
            this.ReqResSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ReqResSplit.Panel1
            // 
            this.ReqResSplit.Panel1.Controls.Add(this.RequestRTB);
            // 
            // ReqResSplit.Panel2
            // 
            this.ReqResSplit.Panel2.Controls.Add(this.ResponseRTB);
            this.ReqResSplit.Size = new System.Drawing.Size(784, 492);
            this.ReqResSplit.SplitterDistance = 245;
            this.ReqResSplit.TabIndex = 1;
            // 
            // RequestRTB
            // 
            this.RequestRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RequestRTB.DetectUrls = false;
            this.RequestRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequestRTB.Location = new System.Drawing.Point(0, 0);
            this.RequestRTB.Name = "RequestRTB";
            this.RequestRTB.Size = new System.Drawing.Size(782, 243);
            this.RequestRTB.TabIndex = 0;
            this.RequestRTB.Text = "";
            this.RequestRTB.SelectionChanged += new System.EventHandler(this.RequestRTB_SelectionChanged);
            // 
            // ResponseRTB
            // 
            this.ResponseRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResponseRTB.DetectUrls = false;
            this.ResponseRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResponseRTB.Location = new System.Drawing.Point(0, 0);
            this.ResponseRTB.Name = "ResponseRTB";
            this.ResponseRTB.Size = new System.Drawing.Size(782, 241);
            this.ResponseRTB.TabIndex = 1;
            this.ResponseRTB.Text = "";
            this.ResponseRTB.SelectionChanged += new System.EventHandler(this.ResponseRTB_SelectionChanged);
            // 
            // TextSizeDownBtn
            // 
            this.TextSizeDownBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextSizeDownBtn.Location = new System.Drawing.Point(160, 8);
            this.TextSizeDownBtn.Name = "TextSizeDownBtn";
            this.TextSizeDownBtn.Size = new System.Drawing.Size(34, 23);
            this.TextSizeDownBtn.TabIndex = 2;
            this.TextSizeDownBtn.Text = "-";
            this.TextSizeDownBtn.UseVisualStyleBackColor = true;
            this.TextSizeDownBtn.Click += new System.EventHandler(this.TextSizeDownBtn_Click);
            // 
            // TextSizeUpBtn
            // 
            this.TextSizeUpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextSizeUpBtn.Location = new System.Drawing.Point(258, 8);
            this.TextSizeUpBtn.Name = "TextSizeUpBtn";
            this.TextSizeUpBtn.Size = new System.Drawing.Size(34, 23);
            this.TextSizeUpBtn.TabIndex = 3;
            this.TextSizeUpBtn.Text = "+";
            this.TextSizeUpBtn.UseVisualStyleBackColor = true;
            this.TextSizeUpBtn.Click += new System.EventHandler(this.TextSizeUpBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Text Size";
            // 
            // AlignTopBottomRB
            // 
            this.AlignTopBottomRB.AutoSize = true;
            this.AlignTopBottomRB.Checked = true;
            this.AlignTopBottomRB.Location = new System.Drawing.Point(467, 12);
            this.AlignTopBottomRB.Name = "AlignTopBottomRB";
            this.AlignTopBottomRB.Size = new System.Drawing.Size(82, 17);
            this.AlignTopBottomRB.TabIndex = 5;
            this.AlignTopBottomRB.TabStop = true;
            this.AlignTopBottomRB.Text = "Top/Bottom";
            this.AlignTopBottomRB.UseVisualStyleBackColor = true;
            // 
            // AlignLeftRightRB
            // 
            this.AlignLeftRightRB.AutoSize = true;
            this.AlignLeftRightRB.Location = new System.Drawing.Point(555, 12);
            this.AlignLeftRightRB.Name = "AlignLeftRightRB";
            this.AlignLeftRightRB.Size = new System.Drawing.Size(73, 17);
            this.AlignLeftRightRB.TabIndex = 6;
            this.AlignLeftRightRB.Text = "Left/Right";
            this.AlignLeftRightRB.UseVisualStyleBackColor = true;
            this.AlignLeftRightRB.CheckedChanged += new System.EventHandler(this.AlignLeftRightRB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Request/Response Alignmment:";
            // 
            // FontColorBtn
            // 
            this.FontColorBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontColorBtn.ForeColor = System.Drawing.Color.Red;
            this.FontColorBtn.Location = new System.Drawing.Point(248, 39);
            this.FontColorBtn.Name = "FontColorBtn";
            this.FontColorBtn.Size = new System.Drawing.Size(34, 23);
            this.FontColorBtn.TabIndex = 8;
            this.FontColorBtn.Text = "A";
            this.FontColorBtn.UseVisualStyleBackColor = true;
            this.FontColorBtn.Click += new System.EventHandler(this.FontColorBtn_Click);
            // 
            // FontHighlightBtn
            // 
            this.FontHighlightBtn.BackColor = System.Drawing.Color.Red;
            this.FontHighlightBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FontHighlightBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontHighlightBtn.Location = new System.Drawing.Point(288, 39);
            this.FontHighlightBtn.Name = "FontHighlightBtn";
            this.FontHighlightBtn.Size = new System.Drawing.Size(34, 23);
            this.FontHighlightBtn.TabIndex = 9;
            this.FontHighlightBtn.Text = "A";
            this.FontHighlightBtn.UseVisualStyleBackColor = false;
            this.FontHighlightBtn.Click += new System.EventHandler(this.FontHighlightBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Selected Text Size";
            // 
            // SelectedTextSizeUpBtn
            // 
            this.SelectedTextSizeUpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedTextSizeUpBtn.Location = new System.Drawing.Point(516, 39);
            this.SelectedTextSizeUpBtn.Name = "SelectedTextSizeUpBtn";
            this.SelectedTextSizeUpBtn.Size = new System.Drawing.Size(34, 23);
            this.SelectedTextSizeUpBtn.TabIndex = 11;
            this.SelectedTextSizeUpBtn.Text = "+";
            this.SelectedTextSizeUpBtn.UseVisualStyleBackColor = true;
            this.SelectedTextSizeUpBtn.Click += new System.EventHandler(this.SelectedTextSizeUpBtn_Click);
            // 
            // SelectedTextSizeDownBtn
            // 
            this.SelectedTextSizeDownBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedTextSizeDownBtn.Location = new System.Drawing.Point(374, 39);
            this.SelectedTextSizeDownBtn.Name = "SelectedTextSizeDownBtn";
            this.SelectedTextSizeDownBtn.Size = new System.Drawing.Size(34, 23);
            this.SelectedTextSizeDownBtn.TabIndex = 10;
            this.SelectedTextSizeDownBtn.Text = "-";
            this.SelectedTextSizeDownBtn.UseVisualStyleBackColor = true;
            this.SelectedTextSizeDownBtn.Click += new System.EventHandler(this.SelectedTextSizeDownBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Adjust Color and Highlighting of  Selected Text:";
            // 
            // RootSplit
            // 
            this.RootSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RootSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.RootSplit.IsSplitterFixed = true;
            this.RootSplit.Location = new System.Drawing.Point(0, 0);
            this.RootSplit.Margin = new System.Windows.Forms.Padding(0);
            this.RootSplit.Name = "RootSplit";
            this.RootSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // RootSplit.Panel1
            // 
            this.RootSplit.Panel1.Controls.Add(this.SnipSelectedTextBtn);
            this.RootSplit.Panel1.Controls.Add(this.TakeScreenshotBtn);
            this.RootSplit.Panel1.Controls.Add(this.SelectedTextSizeDownBtn);
            this.RootSplit.Panel1.Controls.Add(this.label4);
            this.RootSplit.Panel1.Controls.Add(this.AlignTopBottomRB);
            this.RootSplit.Panel1.Controls.Add(this.TextSizeDownBtn);
            this.RootSplit.Panel1.Controls.Add(this.label1);
            this.RootSplit.Panel1.Controls.Add(this.label2);
            this.RootSplit.Panel1.Controls.Add(this.FontHighlightBtn);
            this.RootSplit.Panel1.Controls.Add(this.label3);
            this.RootSplit.Panel1.Controls.Add(this.SelectedTextSizeUpBtn);
            this.RootSplit.Panel1.Controls.Add(this.FontColorBtn);
            this.RootSplit.Panel1.Controls.Add(this.AlignLeftRightRB);
            this.RootSplit.Panel1.Controls.Add(this.TextSizeUpBtn);
            // 
            // RootSplit.Panel2
            // 
            this.RootSplit.Panel2.Controls.Add(this.ReqResSplit);
            this.RootSplit.Size = new System.Drawing.Size(784, 561);
            this.RootSplit.SplitterDistance = 67;
            this.RootSplit.SplitterWidth = 2;
            this.RootSplit.TabIndex = 14;
            // 
            // SnipSelectedTextBtn
            // 
            this.SnipSelectedTextBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SnipSelectedTextBtn.Location = new System.Drawing.Point(596, 39);
            this.SnipSelectedTextBtn.Name = "SnipSelectedTextBtn";
            this.SnipSelectedTextBtn.Size = new System.Drawing.Size(165, 23);
            this.SnipSelectedTextBtn.TabIndex = 14;
            this.SnipSelectedTextBtn.Text = "Snip Selected Text";
            this.SnipSelectedTextBtn.UseVisualStyleBackColor = true;
            this.SnipSelectedTextBtn.Click += new System.EventHandler(this.SnipSelectedTextBtn_Click);
            // 
            // ScreenshotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.RootSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScreenshotForm";
            this.Text = "IronWASP Request/Response Screenshot Window";
            this.Load += new System.EventHandler(this.LogScreenshotForm_Load);
            this.ReqResSplit.Panel1.ResumeLayout(false);
            this.ReqResSplit.Panel2.ResumeLayout(false);
            this.ReqResSplit.ResumeLayout(false);
            this.RootSplit.Panel1.ResumeLayout(false);
            this.RootSplit.Panel1.PerformLayout();
            this.RootSplit.Panel2.ResumeLayout(false);
            this.RootSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TakeScreenshotBtn;
        private System.Windows.Forms.SaveFileDialog SaveScreenshotDialog;
        private System.Windows.Forms.SplitContainer ReqResSplit;
        private System.Windows.Forms.RichTextBox RequestRTB;
        private System.Windows.Forms.RichTextBox ResponseRTB;
        private System.Windows.Forms.Button TextSizeDownBtn;
        private System.Windows.Forms.Button TextSizeUpBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton AlignTopBottomRB;
        private System.Windows.Forms.RadioButton AlignLeftRightRB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FontColorBtn;
        private System.Windows.Forms.Button FontHighlightBtn;
        private System.Windows.Forms.ColorDialog FontColorDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SelectedTextSizeUpBtn;
        private System.Windows.Forms.Button SelectedTextSizeDownBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer RootSplit;
        private System.Windows.Forms.Button SnipSelectedTextBtn;
    }
}