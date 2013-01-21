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
    partial class CloseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseForm));
            this.CloseBtn = new System.Windows.Forms.Button();
            this.RenameCloseBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.ProjectNameTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ErrorTB = new System.Windows.Forms.TextBox();
            this.StatusMsgRTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(12, 172);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(169, 23);
            this.CloseBtn.TabIndex = 2;
            this.CloseBtn.Text = "Close without Renaming";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // RenameCloseBtn
            // 
            this.RenameCloseBtn.Location = new System.Drawing.Point(231, 172);
            this.RenameCloseBtn.Name = "RenameCloseBtn";
            this.RenameCloseBtn.Size = new System.Drawing.Size(164, 23);
            this.RenameCloseBtn.TabIndex = 1;
            this.RenameCloseBtn.Text = "Rename and Close";
            this.RenameCloseBtn.UseVisualStyleBackColor = true;
            this.RenameCloseBtn.Click += new System.EventHandler(this.RenameCloseBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(441, 172);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(135, 23);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // ProjectNameTB
            // 
            this.ProjectNameTB.BackColor = System.Drawing.Color.LightGray;
            this.ProjectNameTB.Location = new System.Drawing.Point(113, 122);
            this.ProjectNameTB.Name = "ProjectNameTB";
            this.ProjectNameTB.ReadOnly = true;
            this.ProjectNameTB.Size = new System.Drawing.Size(472, 20);
            this.ProjectNameTB.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Project Folder Name:";
            // 
            // ErrorTB
            // 
            this.ErrorTB.BackColor = System.Drawing.SystemColors.Control;
            this.ErrorTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorTB.Location = new System.Drawing.Point(9, 148);
            this.ErrorTB.Name = "ErrorTB";
            this.ErrorTB.ReadOnly = true;
            this.ErrorTB.Size = new System.Drawing.Size(573, 13);
            this.ErrorTB.TabIndex = 7;
            this.ErrorTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StatusMsgRTB
            // 
            this.StatusMsgRTB.BackColor = System.Drawing.Color.White;
            this.StatusMsgRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusMsgRTB.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusMsgRTB.Location = new System.Drawing.Point(0, 0);
            this.StatusMsgRTB.Name = "StatusMsgRTB";
            this.StatusMsgRTB.ReadOnly = true;
            this.StatusMsgRTB.Size = new System.Drawing.Size(584, 100);
            this.StatusMsgRTB.TabIndex = 8;
            this.StatusMsgRTB.Text = "";
            this.StatusMsgRTB.Enter += new System.EventHandler(this.StatusMsgRTB_Enter);
            // 
            // CloseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 202);
            this.ControlBox = false;
            this.Controls.Add(this.StatusMsgRTB);
            this.Controls.Add(this.ErrorTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProjectNameTB);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.RenameCloseBtn);
            this.Controls.Add(this.CloseBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 240);
            this.MinimumSize = new System.Drawing.Size(600, 240);
            this.Name = "CloseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Want to Shutdown IronWASP?";
            this.Load += new System.EventHandler(this.CloseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button RenameCloseBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox ProjectNameTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ErrorTB;
        private System.Windows.Forms.RichTextBox StatusMsgRTB;
    }
}