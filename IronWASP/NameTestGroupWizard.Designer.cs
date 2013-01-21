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
    partial class NameTestGroupWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameTestGroupWizard));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DoneBtn = new System.Windows.Forms.Button();
            this.SwithToMTSectionCB = new System.Windows.Forms.CheckBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.Step0StatusTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RequestNameTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(10, 227);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // DoneBtn
            // 
            this.DoneBtn.Location = new System.Drawing.Point(391, 97);
            this.DoneBtn.Name = "DoneBtn";
            this.DoneBtn.Size = new System.Drawing.Size(89, 23);
            this.DoneBtn.TabIndex = 1;
            this.DoneBtn.Text = "Done";
            this.DoneBtn.UseVisualStyleBackColor = true;
            this.DoneBtn.Click += new System.EventHandler(this.DoneBtn_Click);
            // 
            // SwithToMTSectionCB
            // 
            this.SwithToMTSectionCB.AutoSize = true;
            this.SwithToMTSectionCB.Checked = true;
            this.SwithToMTSectionCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SwithToMTSectionCB.Location = new System.Drawing.Point(229, 236);
            this.SwithToMTSectionCB.Name = "SwithToMTSectionCB";
            this.SwithToMTSectionCB.Size = new System.Drawing.Size(259, 17);
            this.SwithToMTSectionCB.TabIndex = 2;
            this.SwithToMTSectionCB.Text = "Take me to Manual Testing Section automatically";
            this.SwithToMTSectionCB.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox4.Location = new System.Drawing.Point(5, 5);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(479, 77);
            this.textBox4.TabIndex = 6;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "The Manual Testing section can contain multiple requests and so naming requests m" +
                "akes it easy to identify them.\r\n\r\nEnter a name and click \'Done\'";
            // 
            // Step0StatusTB
            // 
            this.Step0StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Step0StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step0StatusTB.Location = new System.Drawing.Point(17, 161);
            this.Step0StatusTB.Multiline = true;
            this.Step0StatusTB.Name = "Step0StatusTB";
            this.Step0StatusTB.Size = new System.Drawing.Size(394, 46);
            this.Step0StatusTB.TabIndex = 19;
            this.Step0StatusTB.TabStop = false;
            this.Step0StatusTB.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "( if left blank a name is automatically assigned )";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Name this Request:";
            // 
            // RequestNameTB
            // 
            this.RequestNameTB.Location = new System.Drawing.Point(117, 99);
            this.RequestNameTB.Name = "RequestNameTB";
            this.RequestNameTB.Size = new System.Drawing.Size(269, 20);
            this.RequestNameTB.TabIndex = 16;
            // 
            // NameTestGroupWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 256);
            this.ControlBox = false;
            this.Controls.Add(this.Step0StatusTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RequestNameTB);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.SwithToMTSectionCB);
            this.Controls.Add(this.DoneBtn);
            this.Controls.Add(this.CancelBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NameTestGroupWizard";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Name this Request";
            this.Load += new System.EventHandler(this.NameTestGroupWizard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button DoneBtn;
        private System.Windows.Forms.CheckBox SwithToMTSectionCB;
        private System.Windows.Forms.TextBox textBox4;
        internal System.Windows.Forms.TextBox Step0StatusTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RequestNameTB;
    }
}