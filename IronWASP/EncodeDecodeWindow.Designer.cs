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
    partial class EncodeDecodeWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncodeDecodeWindow));
            this.EncDecBaseSplit = new System.Windows.Forms.SplitContainer();
            this.EncDecRightSplit = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EncodeOutToEncodeInBtn = new System.Windows.Forms.Button();
            this.SHA512Btn = new System.Windows.Forms.Button();
            this.SHA384Btn = new System.Windows.Forms.Button();
            this.SHA256Btn = new System.Windows.Forms.Button();
            this.SHA1Btn = new System.Windows.Forms.Button();
            this.MD5Btn = new System.Windows.Forms.Button();
            this.Base64DecodeBtn = new System.Windows.Forms.Button();
            this.HexDecodeBtn = new System.Windows.Forms.Button();
            this.HtmlDecodeBtn = new System.Windows.Forms.Button();
            this.UrlDecodeBtn = new System.Windows.Forms.Button();
            this.ToHexBtn = new System.Windows.Forms.Button();
            this.Base64EncodeBtn = new System.Windows.Forms.Button();
            this.HexEncodeBtn = new System.Windows.Forms.Button();
            this.HtmlEncodeBtn = new System.Windows.Forms.Button();
            this.UrlEncodeBtn = new System.Windows.Forms.Button();
            this.StatusTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OutputTB = new System.Windows.Forms.TextBox();
            this.InputTB = new System.Windows.Forms.TextBox();
            this.EncDecBaseSplit.Panel1.SuspendLayout();
            this.EncDecBaseSplit.Panel2.SuspendLayout();
            this.EncDecBaseSplit.SuspendLayout();
            this.EncDecRightSplit.Panel1.SuspendLayout();
            this.EncDecRightSplit.Panel2.SuspendLayout();
            this.EncDecRightSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // EncDecBaseSplit
            // 
            this.EncDecBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EncDecBaseSplit.IsSplitterFixed = true;
            this.EncDecBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.EncDecBaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.EncDecBaseSplit.Name = "EncDecBaseSplit";
            // 
            // EncDecBaseSplit.Panel1
            // 
            this.EncDecBaseSplit.Panel1.Controls.Add(this.EncDecRightSplit);
            // 
            // EncDecBaseSplit.Panel2
            // 
            this.EncDecBaseSplit.Panel2.Controls.Add(this.EncodeOutToEncodeInBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.SHA512Btn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.SHA384Btn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.SHA256Btn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.SHA1Btn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.MD5Btn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.Base64DecodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.HexDecodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.HtmlDecodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.UrlDecodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.ToHexBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.Base64EncodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.HexEncodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.HtmlEncodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.UrlEncodeBtn);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.StatusTB);
            this.EncDecBaseSplit.Panel2.Controls.Add(this.label3);
            this.EncDecBaseSplit.Size = new System.Drawing.Size(684, 562);
            this.EncDecBaseSplit.SplitterDistance = 503;
            this.EncDecBaseSplit.SplitterWidth = 2;
            this.EncDecBaseSplit.TabIndex = 0;
            // 
            // EncDecRightSplit
            // 
            this.EncDecRightSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EncDecRightSplit.Location = new System.Drawing.Point(0, 0);
            this.EncDecRightSplit.Margin = new System.Windows.Forms.Padding(0);
            this.EncDecRightSplit.Name = "EncDecRightSplit";
            this.EncDecRightSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // EncDecRightSplit.Panel1
            // 
            this.EncDecRightSplit.Panel1.Controls.Add(this.label1);
            this.EncDecRightSplit.Panel1.Controls.Add(this.InputTB);
            // 
            // EncDecRightSplit.Panel2
            // 
            this.EncDecRightSplit.Panel2.Controls.Add(this.label2);
            this.EncDecRightSplit.Panel2.Controls.Add(this.OutputTB);
            this.EncDecRightSplit.Size = new System.Drawing.Size(503, 562);
            this.EncDecRightSplit.SplitterDistance = 270;
            this.EncDecRightSplit.SplitterWidth = 2;
            this.EncDecRightSplit.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output:";
            // 
            // EncodeOutToEncodeInBtn
            // 
            this.EncodeOutToEncodeInBtn.BackColor = System.Drawing.Color.Transparent;
            this.EncodeOutToEncodeInBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EncodeOutToEncodeInBtn.BackgroundImage")));
            this.EncodeOutToEncodeInBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EncodeOutToEncodeInBtn.ForeColor = System.Drawing.Color.Transparent;
            this.EncodeOutToEncodeInBtn.Location = new System.Drawing.Point(4, 226);
            this.EncodeOutToEncodeInBtn.Name = "EncodeOutToEncodeInBtn";
            this.EncodeOutToEncodeInBtn.Size = new System.Drawing.Size(35, 96);
            this.EncodeOutToEncodeInBtn.TabIndex = 32;
            this.EncodeOutToEncodeInBtn.UseVisualStyleBackColor = false;
            this.EncodeOutToEncodeInBtn.Click += new System.EventHandler(this.EncodeOutToEncodeInBtn_Click);
            // 
            // SHA512Btn
            // 
            this.SHA512Btn.Location = new System.Drawing.Point(45, 458);
            this.SHA512Btn.Name = "SHA512Btn";
            this.SHA512Btn.Size = new System.Drawing.Size(126, 23);
            this.SHA512Btn.TabIndex = 31;
            this.SHA512Btn.Text = "SHA512(Input)";
            this.SHA512Btn.UseVisualStyleBackColor = true;
            this.SHA512Btn.Click += new System.EventHandler(this.SHA512Btn_Click);
            // 
            // SHA384Btn
            // 
            this.SHA384Btn.Location = new System.Drawing.Point(45, 429);
            this.SHA384Btn.Name = "SHA384Btn";
            this.SHA384Btn.Size = new System.Drawing.Size(126, 23);
            this.SHA384Btn.TabIndex = 30;
            this.SHA384Btn.Text = "SHA384(Input)";
            this.SHA384Btn.UseVisualStyleBackColor = true;
            this.SHA384Btn.Click += new System.EventHandler(this.SHA384Btn_Click);
            // 
            // SHA256Btn
            // 
            this.SHA256Btn.Location = new System.Drawing.Point(45, 402);
            this.SHA256Btn.Name = "SHA256Btn";
            this.SHA256Btn.Size = new System.Drawing.Size(126, 23);
            this.SHA256Btn.TabIndex = 29;
            this.SHA256Btn.Text = "SHA256(Input)";
            this.SHA256Btn.UseVisualStyleBackColor = true;
            this.SHA256Btn.Click += new System.EventHandler(this.SHA256Btn_Click);
            // 
            // SHA1Btn
            // 
            this.SHA1Btn.Location = new System.Drawing.Point(45, 373);
            this.SHA1Btn.Name = "SHA1Btn";
            this.SHA1Btn.Size = new System.Drawing.Size(126, 23);
            this.SHA1Btn.TabIndex = 28;
            this.SHA1Btn.Text = "SHA1(Input)";
            this.SHA1Btn.UseVisualStyleBackColor = true;
            this.SHA1Btn.Click += new System.EventHandler(this.SHA1Btn_Click);
            // 
            // MD5Btn
            // 
            this.MD5Btn.Location = new System.Drawing.Point(45, 344);
            this.MD5Btn.Name = "MD5Btn";
            this.MD5Btn.Size = new System.Drawing.Size(126, 23);
            this.MD5Btn.TabIndex = 27;
            this.MD5Btn.Text = "MD5(Input)";
            this.MD5Btn.UseVisualStyleBackColor = true;
            this.MD5Btn.Click += new System.EventHandler(this.MD5Btn_Click);
            // 
            // Base64DecodeBtn
            // 
            this.Base64DecodeBtn.Location = new System.Drawing.Point(45, 289);
            this.Base64DecodeBtn.Name = "Base64DecodeBtn";
            this.Base64DecodeBtn.Size = new System.Drawing.Size(126, 23);
            this.Base64DecodeBtn.TabIndex = 26;
            this.Base64DecodeBtn.Text = "Base64Decode(Input)";
            this.Base64DecodeBtn.UseVisualStyleBackColor = true;
            this.Base64DecodeBtn.Click += new System.EventHandler(this.Base64DecodeBtn_Click);
            // 
            // HexDecodeBtn
            // 
            this.HexDecodeBtn.Location = new System.Drawing.Point(45, 260);
            this.HexDecodeBtn.Name = "HexDecodeBtn";
            this.HexDecodeBtn.Size = new System.Drawing.Size(126, 23);
            this.HexDecodeBtn.TabIndex = 25;
            this.HexDecodeBtn.Text = "HexDecode(Input)";
            this.HexDecodeBtn.UseVisualStyleBackColor = true;
            this.HexDecodeBtn.Click += new System.EventHandler(this.HexDecodeBtn_Click);
            // 
            // HtmlDecodeBtn
            // 
            this.HtmlDecodeBtn.Location = new System.Drawing.Point(45, 229);
            this.HtmlDecodeBtn.Name = "HtmlDecodeBtn";
            this.HtmlDecodeBtn.Size = new System.Drawing.Size(126, 23);
            this.HtmlDecodeBtn.TabIndex = 24;
            this.HtmlDecodeBtn.Text = "HtmlDecode(Input)";
            this.HtmlDecodeBtn.UseVisualStyleBackColor = true;
            this.HtmlDecodeBtn.Click += new System.EventHandler(this.HtmlDecodeBtn_Click);
            // 
            // UrlDecodeBtn
            // 
            this.UrlDecodeBtn.Location = new System.Drawing.Point(45, 200);
            this.UrlDecodeBtn.Name = "UrlDecodeBtn";
            this.UrlDecodeBtn.Size = new System.Drawing.Size(126, 23);
            this.UrlDecodeBtn.TabIndex = 23;
            this.UrlDecodeBtn.Text = "UrlDecode(Input)";
            this.UrlDecodeBtn.UseVisualStyleBackColor = true;
            this.UrlDecodeBtn.Click += new System.EventHandler(this.UrlDecodeBtn_Click);
            // 
            // ToHexBtn
            // 
            this.ToHexBtn.Location = new System.Drawing.Point(45, 149);
            this.ToHexBtn.Name = "ToHexBtn";
            this.ToHexBtn.Size = new System.Drawing.Size(126, 23);
            this.ToHexBtn.TabIndex = 22;
            this.ToHexBtn.Text = "ToHex(Input)";
            this.ToHexBtn.UseVisualStyleBackColor = true;
            this.ToHexBtn.Click += new System.EventHandler(this.ToHexBtn_Click);
            // 
            // Base64EncodeBtn
            // 
            this.Base64EncodeBtn.Location = new System.Drawing.Point(45, 120);
            this.Base64EncodeBtn.Name = "Base64EncodeBtn";
            this.Base64EncodeBtn.Size = new System.Drawing.Size(126, 23);
            this.Base64EncodeBtn.TabIndex = 21;
            this.Base64EncodeBtn.Text = "Base64Encode(Input)";
            this.Base64EncodeBtn.UseVisualStyleBackColor = true;
            this.Base64EncodeBtn.Click += new System.EventHandler(this.Base64EncodeBtn_Click);
            // 
            // HexEncodeBtn
            // 
            this.HexEncodeBtn.Location = new System.Drawing.Point(45, 91);
            this.HexEncodeBtn.Name = "HexEncodeBtn";
            this.HexEncodeBtn.Size = new System.Drawing.Size(126, 23);
            this.HexEncodeBtn.TabIndex = 20;
            this.HexEncodeBtn.Text = "HexEncode(Input)";
            this.HexEncodeBtn.UseVisualStyleBackColor = true;
            this.HexEncodeBtn.Click += new System.EventHandler(this.HexEncodeBtn_Click);
            // 
            // HtmlEncodeBtn
            // 
            this.HtmlEncodeBtn.Location = new System.Drawing.Point(45, 62);
            this.HtmlEncodeBtn.Name = "HtmlEncodeBtn";
            this.HtmlEncodeBtn.Size = new System.Drawing.Size(126, 23);
            this.HtmlEncodeBtn.TabIndex = 19;
            this.HtmlEncodeBtn.Text = "HtmlEncode(Input)";
            this.HtmlEncodeBtn.UseVisualStyleBackColor = true;
            this.HtmlEncodeBtn.Click += new System.EventHandler(this.HtmlEncodeBtn_Click);
            // 
            // UrlEncodeBtn
            // 
            this.UrlEncodeBtn.Location = new System.Drawing.Point(45, 33);
            this.UrlEncodeBtn.Name = "UrlEncodeBtn";
            this.UrlEncodeBtn.Size = new System.Drawing.Size(126, 23);
            this.UrlEncodeBtn.TabIndex = 18;
            this.UrlEncodeBtn.Text = "UrlEncode(Input)";
            this.UrlEncodeBtn.UseVisualStyleBackColor = true;
            this.UrlEncodeBtn.Click += new System.EventHandler(this.UrlEncodeBtn_Click);
            // 
            // StatusTB
            // 
            this.StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusTB.Location = new System.Drawing.Point(13, 491);
            this.StatusTB.Multiline = true;
            this.StatusTB.Name = "StatusTB";
            this.StatusTB.ReadOnly = true;
            this.StatusTB.Size = new System.Drawing.Size(154, 59);
            this.StatusTB.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tools API";
            // 
            // OutputTB
            // 
            this.OutputTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputTB.Location = new System.Drawing.Point(0, 20);
            this.OutputTB.Multiline = true;
            this.OutputTB.Name = "OutputTB";
            this.OutputTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTB.Size = new System.Drawing.Size(503, 270);
            this.OutputTB.TabIndex = 1;
            // 
            // InputTB
            // 
            this.InputTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InputTB.Location = new System.Drawing.Point(0, 20);
            this.InputTB.Multiline = true;
            this.InputTB.Name = "InputTB";
            this.InputTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InputTB.Size = new System.Drawing.Size(503, 250);
            this.InputTB.TabIndex = 0;
            // 
            // EncodeDecodeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 562);
            this.Controls.Add(this.EncDecBaseSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 600);
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "EncodeDecodeWindow";
            this.Text = "EncodeDecodeWindow";
            this.EncDecBaseSplit.Panel1.ResumeLayout(false);
            this.EncDecBaseSplit.Panel2.ResumeLayout(false);
            this.EncDecBaseSplit.Panel2.PerformLayout();
            this.EncDecBaseSplit.ResumeLayout(false);
            this.EncDecRightSplit.Panel1.ResumeLayout(false);
            this.EncDecRightSplit.Panel1.PerformLayout();
            this.EncDecRightSplit.Panel2.ResumeLayout(false);
            this.EncDecRightSplit.Panel2.PerformLayout();
            this.EncDecRightSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer EncDecBaseSplit;
        private System.Windows.Forms.SplitContainer EncDecRightSplit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox StatusTB;
        internal System.Windows.Forms.Button HtmlEncodeBtn;
        internal System.Windows.Forms.Button UrlEncodeBtn;
        internal System.Windows.Forms.Button HexEncodeBtn;
        internal System.Windows.Forms.Button Base64EncodeBtn;
        internal System.Windows.Forms.Button ToHexBtn;
        internal System.Windows.Forms.Button UrlDecodeBtn;
        internal System.Windows.Forms.Button HtmlDecodeBtn;
        internal System.Windows.Forms.Button Base64DecodeBtn;
        internal System.Windows.Forms.Button HexDecodeBtn;
        internal System.Windows.Forms.Button SHA384Btn;
        internal System.Windows.Forms.Button SHA256Btn;
        internal System.Windows.Forms.Button SHA1Btn;
        internal System.Windows.Forms.Button MD5Btn;
        internal System.Windows.Forms.Button SHA512Btn;
        private System.Windows.Forms.Button EncodeOutToEncodeInBtn;
        internal System.Windows.Forms.TextBox OutputTB;
        internal System.Windows.Forms.TextBox InputTB;
    }
}