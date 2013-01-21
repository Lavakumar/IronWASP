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
    partial class ModuleStartPromptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleStartPromptForm));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.RunModuleBtn = new System.Windows.Forms.Button();
            this.DisplayRTB = new System.Windows.Forms.RichTextBox();
            this.ProgressBarPanel = new System.Windows.Forms.Panel();
            this.ProgressPB = new System.Windows.Forms.ProgressBar();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.ProgressBarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelBtn.Location = new System.Drawing.Point(311, 381);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(196, 23);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // RunModuleBtn
            // 
            this.RunModuleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RunModuleBtn.Location = new System.Drawing.Point(25, 381);
            this.RunModuleBtn.Name = "RunModuleBtn";
            this.RunModuleBtn.Size = new System.Drawing.Size(198, 23);
            this.RunModuleBtn.TabIndex = 1;
            this.RunModuleBtn.Text = "Run this Module";
            this.RunModuleBtn.UseVisualStyleBackColor = true;
            this.RunModuleBtn.Click += new System.EventHandler(this.RunModuleBtn_Click);
            // 
            // DisplayRTB
            // 
            this.DisplayRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayRTB.BackColor = System.Drawing.Color.White;
            this.DisplayRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DisplayRTB.Location = new System.Drawing.Point(0, 0);
            this.DisplayRTB.Margin = new System.Windows.Forms.Padding(0);
            this.DisplayRTB.Name = "DisplayRTB";
            this.DisplayRTB.ReadOnly = true;
            this.DisplayRTB.Size = new System.Drawing.Size(534, 348);
            this.DisplayRTB.TabIndex = 2;
            this.DisplayRTB.Text = "";
            // 
            // ProgressBarPanel
            // 
            this.ProgressBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarPanel.Controls.Add(this.ProgressPB);
            this.ProgressBarPanel.Location = new System.Drawing.Point(5, 377);
            this.ProgressBarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ProgressBarPanel.Name = "ProgressBarPanel";
            this.ProgressBarPanel.Size = new System.Drawing.Size(523, 32);
            this.ProgressBarPanel.TabIndex = 3;
            this.ProgressBarPanel.Visible = false;
            // 
            // ProgressPB
            // 
            this.ProgressPB.Location = new System.Drawing.Point(10, 4);
            this.ProgressPB.MarqueeAnimationSpeed = 10;
            this.ProgressPB.Name = "ProgressPB";
            this.ProgressPB.Size = new System.Drawing.Size(504, 23);
            this.ProgressPB.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressPB.TabIndex = 0;
            // 
            // StatusLbl
            // 
            this.StatusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLbl.AutoSize = true;
            this.StatusLbl.Location = new System.Drawing.Point(12, 354);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(43, 13);
            this.StatusLbl.TabIndex = 4;
            this.StatusLbl.Text = "            ";
            // 
            // ModuleStartPromptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 412);
            this.ControlBox = false;
            this.Controls.Add(this.StatusLbl);
            this.Controls.Add(this.ProgressBarPanel);
            this.Controls.Add(this.DisplayRTB);
            this.Controls.Add(this.RunModuleBtn);
            this.Controls.Add(this.CancelBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModuleStartPromptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorize Module";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleStartPromptForm_FormClosing);
            this.ProgressBarPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button RunModuleBtn;
        private System.Windows.Forms.RichTextBox DisplayRTB;
        private System.Windows.Forms.Panel ProgressBarPanel;
        private System.Windows.Forms.ProgressBar ProgressPB;
        private System.Windows.Forms.Label StatusLbl;
    }
}