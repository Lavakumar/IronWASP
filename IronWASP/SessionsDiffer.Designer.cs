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
    partial class SessionsDiffer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionsDiffer));
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DiffProgressBar = new System.Windows.Forms.ProgressBar();
            this.RequestDRV = new IronWASP.DiffResultView();
            this.ResponseDRV = new IronWASP.DiffResultView();
            this.BaseTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseTabs
            // 
            this.BaseTabs.Controls.Add(this.tabPage1);
            this.BaseTabs.Controls.Add(this.tabPage2);
            this.BaseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseTabs.Location = new System.Drawing.Point(0, 0);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(784, 462);
            this.BaseTabs.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.RequestDRV);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(776, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "   Request Diff   ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ResponseDRV);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(776, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "   Response Diff   ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DiffProgressBar
            // 
            this.DiffProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiffProgressBar.Location = new System.Drawing.Point(264, 24);
            this.DiffProgressBar.MarqueeAnimationSpeed = 10;
            this.DiffProgressBar.Name = "DiffProgressBar";
            this.DiffProgressBar.Size = new System.Drawing.Size(242, 23);
            this.DiffProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.DiffProgressBar.TabIndex = 33;
            this.DiffProgressBar.Visible = false;
            // 
            // RequestDRV
            // 
            this.RequestDRV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequestDRV.Location = new System.Drawing.Point(0, 0);
            this.RequestDRV.Margin = new System.Windows.Forms.Padding(0);
            this.RequestDRV.Name = "RequestDRV";
            this.RequestDRV.Size = new System.Drawing.Size(776, 436);
            this.RequestDRV.TabIndex = 0;
            // 
            // ResponseDRV
            // 
            this.ResponseDRV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResponseDRV.Location = new System.Drawing.Point(0, 0);
            this.ResponseDRV.Margin = new System.Windows.Forms.Padding(0);
            this.ResponseDRV.Name = "ResponseDRV";
            this.ResponseDRV.Size = new System.Drawing.Size(776, 436);
            this.ResponseDRV.TabIndex = 1;
            // 
            // SessionsDiffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.DiffProgressBar);
            this.Controls.Add(this.BaseTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SessionsDiffer";
            this.Text = "Request/Response Differ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SessionsDiffer_FormClosing);
            this.Load += new System.EventHandler(this.SessionsDiffer_Load);
            this.BaseTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl BaseTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private DiffResultView RequestDRV;
        private System.Windows.Forms.TabPage tabPage2;
        private DiffResultView ResponseDRV;
        private System.Windows.Forms.ProgressBar DiffProgressBar;
    }
}