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
    partial class ConfiguredScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguredScan));
            this.ConfigureScanHTTPSCB = new System.Windows.Forms.CheckBox();
            this.ConfigureScanCancelBtn = new System.Windows.Forms.Button();
            this.ConfigureScanStartScanBtn = new System.Windows.Forms.Button();
            this.ConfigureScanSessionPluginsCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfigureScanBaseUrlTB = new System.Windows.Forms.TextBox();
            this.ConfigureScanHTTPCB = new System.Windows.Forms.CheckBox();
            this.ConfigureScanHostNameTB = new System.Windows.Forms.TextBox();
            this.ConfigureScanCrawlAndScanRB = new System.Windows.Forms.RadioButton();
            this.ConfigureScanCrawlOnlyRB = new System.Windows.Forms.RadioButton();
            this.ConfigureScanIncludeSubDomainsCB = new System.Windows.Forms.CheckBox();
            this.ConfigureScanDirAndFileGuessingCB = new System.Windows.Forms.CheckBox();
            this.ConfigureScanUrlToAvoidTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfigureScanStartingUrlTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ConfigureScanHostsToIncludeTB = new System.Windows.Forms.TextBox();
            this.ConfigureScanErrorTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConfigureScanHTTPSCB
            // 
            this.ConfigureScanHTTPSCB.AutoSize = true;
            this.ConfigureScanHTTPSCB.Checked = true;
            this.ConfigureScanHTTPSCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigureScanHTTPSCB.Location = new System.Drawing.Point(422, 39);
            this.ConfigureScanHTTPSCB.Name = "ConfigureScanHTTPSCB";
            this.ConfigureScanHTTPSCB.Size = new System.Drawing.Size(62, 17);
            this.ConfigureScanHTTPSCB.TabIndex = 111;
            this.ConfigureScanHTTPSCB.Text = "HTTPS";
            this.ConfigureScanHTTPSCB.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanCancelBtn
            // 
            this.ConfigureScanCancelBtn.Location = new System.Drawing.Point(546, 41);
            this.ConfigureScanCancelBtn.Name = "ConfigureScanCancelBtn";
            this.ConfigureScanCancelBtn.Size = new System.Drawing.Size(88, 23);
            this.ConfigureScanCancelBtn.TabIndex = 103;
            this.ConfigureScanCancelBtn.Text = "Cancel";
            this.ConfigureScanCancelBtn.UseVisualStyleBackColor = true;
            this.ConfigureScanCancelBtn.Click += new System.EventHandler(this.ConfigureScanCancelBtn_Click);
            // 
            // ConfigureScanStartScanBtn
            // 
            this.ConfigureScanStartScanBtn.Location = new System.Drawing.Point(546, 12);
            this.ConfigureScanStartScanBtn.Name = "ConfigureScanStartScanBtn";
            this.ConfigureScanStartScanBtn.Size = new System.Drawing.Size(88, 23);
            this.ConfigureScanStartScanBtn.TabIndex = 102;
            this.ConfigureScanStartScanBtn.Text = "Start Scan";
            this.ConfigureScanStartScanBtn.UseVisualStyleBackColor = true;
            this.ConfigureScanStartScanBtn.Click += new System.EventHandler(this.ConfigureScanStartScanBtn_Click);
            // 
            // ConfigureScanSessionPluginsCombo
            // 
            this.ConfigureScanSessionPluginsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigureScanSessionPluginsCombo.FormattingEnabled = true;
            this.ConfigureScanSessionPluginsCombo.Location = new System.Drawing.Point(95, 93);
            this.ConfigureScanSessionPluginsCombo.Name = "ConfigureScanSessionPluginsCombo";
            this.ConfigureScanSessionPluginsCombo.Size = new System.Drawing.Size(251, 21);
            this.ConfigureScanSessionPluginsCombo.TabIndex = 99;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 98;
            this.label3.Text = "Session Plugin:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Base Url:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 96;
            this.label1.Text = "HostName:";
            // 
            // ConfigureScanBaseUrlTB
            // 
            this.ConfigureScanBaseUrlTB.Location = new System.Drawing.Point(76, 64);
            this.ConfigureScanBaseUrlTB.Name = "ConfigureScanBaseUrlTB";
            this.ConfigureScanBaseUrlTB.Size = new System.Drawing.Size(270, 20);
            this.ConfigureScanBaseUrlTB.TabIndex = 95;
            // 
            // ConfigureScanHTTPCB
            // 
            this.ConfigureScanHTTPCB.AutoSize = true;
            this.ConfigureScanHTTPCB.Checked = true;
            this.ConfigureScanHTTPCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigureScanHTTPCB.Location = new System.Drawing.Point(353, 39);
            this.ConfigureScanHTTPCB.Name = "ConfigureScanHTTPCB";
            this.ConfigureScanHTTPCB.Size = new System.Drawing.Size(55, 17);
            this.ConfigureScanHTTPCB.TabIndex = 94;
            this.ConfigureScanHTTPCB.Text = "HTTP";
            this.ConfigureScanHTTPCB.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanHostNameTB
            // 
            this.ConfigureScanHostNameTB.Location = new System.Drawing.Point(76, 9);
            this.ConfigureScanHostNameTB.Name = "ConfigureScanHostNameTB";
            this.ConfigureScanHostNameTB.Size = new System.Drawing.Size(270, 20);
            this.ConfigureScanHostNameTB.TabIndex = 93;
            // 
            // ConfigureScanCrawlAndScanRB
            // 
            this.ConfigureScanCrawlAndScanRB.AutoSize = true;
            this.ConfigureScanCrawlAndScanRB.Checked = true;
            this.ConfigureScanCrawlAndScanRB.Location = new System.Drawing.Point(503, 98);
            this.ConfigureScanCrawlAndScanRB.Name = "ConfigureScanCrawlAndScanRB";
            this.ConfigureScanCrawlAndScanRB.Size = new System.Drawing.Size(100, 17);
            this.ConfigureScanCrawlAndScanRB.TabIndex = 112;
            this.ConfigureScanCrawlAndScanRB.TabStop = true;
            this.ConfigureScanCrawlAndScanRB.Text = "Crawl and Scan";
            this.ConfigureScanCrawlAndScanRB.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanCrawlOnlyRB
            // 
            this.ConfigureScanCrawlOnlyRB.AutoSize = true;
            this.ConfigureScanCrawlOnlyRB.Location = new System.Drawing.Point(401, 98);
            this.ConfigureScanCrawlOnlyRB.Name = "ConfigureScanCrawlOnlyRB";
            this.ConfigureScanCrawlOnlyRB.Size = new System.Drawing.Size(75, 17);
            this.ConfigureScanCrawlOnlyRB.TabIndex = 113;
            this.ConfigureScanCrawlOnlyRB.Text = "Crawl Only";
            this.ConfigureScanCrawlOnlyRB.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanIncludeSubDomainsCB
            // 
            this.ConfigureScanIncludeSubDomainsCB.AutoSize = true;
            this.ConfigureScanIncludeSubDomainsCB.Location = new System.Drawing.Point(353, 12);
            this.ConfigureScanIncludeSubDomainsCB.Name = "ConfigureScanIncludeSubDomainsCB";
            this.ConfigureScanIncludeSubDomainsCB.Size = new System.Drawing.Size(167, 17);
            this.ConfigureScanIncludeSubDomainsCB.TabIndex = 114;
            this.ConfigureScanIncludeSubDomainsCB.Text = "Include Subdomains in Scope";
            this.ConfigureScanIncludeSubDomainsCB.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanDirAndFileGuessingCB
            // 
            this.ConfigureScanDirAndFileGuessingCB.AutoSize = true;
            this.ConfigureScanDirAndFileGuessingCB.Checked = true;
            this.ConfigureScanDirAndFileGuessingCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigureScanDirAndFileGuessingCB.Location = new System.Drawing.Point(352, 67);
            this.ConfigureScanDirAndFileGuessingCB.Name = "ConfigureScanDirAndFileGuessingCB";
            this.ConfigureScanDirAndFileGuessingCB.Size = new System.Drawing.Size(194, 17);
            this.ConfigureScanDirAndFileGuessingCB.TabIndex = 115;
            this.ConfigureScanDirAndFileGuessingCB.Text = "Perform Directory and File Guessing";
            this.ConfigureScanDirAndFileGuessingCB.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanUrlToAvoidTB
            // 
            this.ConfigureScanUrlToAvoidTB.Location = new System.Drawing.Point(6, 169);
            this.ConfigureScanUrlToAvoidTB.Multiline = true;
            this.ConfigureScanUrlToAvoidTB.Name = "ConfigureScanUrlToAvoidTB";
            this.ConfigureScanUrlToAvoidTB.Size = new System.Drawing.Size(333, 113);
            this.ConfigureScanUrlToAvoidTB.TabIndex = 116;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 118;
            this.label4.Text = "Starting Url:";
            // 
            // ConfigureScanStartingUrlTB
            // 
            this.ConfigureScanStartingUrlTB.Location = new System.Drawing.Point(76, 37);
            this.ConfigureScanStartingUrlTB.Name = "ConfigureScanStartingUrlTB";
            this.ConfigureScanStartingUrlTB.Size = new System.Drawing.Size(270, 20);
            this.ConfigureScanStartingUrlTB.TabIndex = 117;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(118, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 119;
            this.label5.Text = "Urls to Avoid:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(442, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 121;
            this.label6.Text = "Hostnames to Include:";
            // 
            // ConfigureScanHostsToIncludeTB
            // 
            this.ConfigureScanHostsToIncludeTB.Location = new System.Drawing.Point(361, 169);
            this.ConfigureScanHostsToIncludeTB.Multiline = true;
            this.ConfigureScanHostsToIncludeTB.Name = "ConfigureScanHostsToIncludeTB";
            this.ConfigureScanHostsToIncludeTB.Size = new System.Drawing.Size(271, 113);
            this.ConfigureScanHostsToIncludeTB.TabIndex = 120;
            // 
            // ConfigureScanErrorTB
            // 
            this.ConfigureScanErrorTB.BackColor = System.Drawing.SystemColors.Control;
            this.ConfigureScanErrorTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfigureScanErrorTB.Location = new System.Drawing.Point(6, 123);
            this.ConfigureScanErrorTB.Name = "ConfigureScanErrorTB";
            this.ConfigureScanErrorTB.ReadOnly = true;
            this.ConfigureScanErrorTB.Size = new System.Drawing.Size(628, 13);
            this.ConfigureScanErrorTB.TabIndex = 122;
            // 
            // ConfiguredScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 287);
            this.ControlBox = false;
            this.Controls.Add(this.ConfigureScanErrorTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ConfigureScanHostsToIncludeTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ConfigureScanStartingUrlTB);
            this.Controls.Add(this.ConfigureScanUrlToAvoidTB);
            this.Controls.Add(this.ConfigureScanDirAndFileGuessingCB);
            this.Controls.Add(this.ConfigureScanIncludeSubDomainsCB);
            this.Controls.Add(this.ConfigureScanCrawlOnlyRB);
            this.Controls.Add(this.ConfigureScanCrawlAndScanRB);
            this.Controls.Add(this.ConfigureScanHTTPSCB);
            this.Controls.Add(this.ConfigureScanCancelBtn);
            this.Controls.Add(this.ConfigureScanStartScanBtn);
            this.Controls.Add(this.ConfigureScanSessionPluginsCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConfigureScanBaseUrlTB);
            this.Controls.Add(this.ConfigureScanHTTPCB);
            this.Controls.Add(this.ConfigureScanHostNameTB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(655, 325);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(655, 325);
            this.Name = "ConfiguredScan";
            this.Text = "Enter Scan Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox ConfigureScanHTTPSCB;
        internal System.Windows.Forms.Button ConfigureScanCancelBtn;
        private System.Windows.Forms.Button ConfigureScanStartScanBtn;
        internal System.Windows.Forms.ComboBox ConfigureScanSessionPluginsCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox ConfigureScanBaseUrlTB;
        internal System.Windows.Forms.CheckBox ConfigureScanHTTPCB;
        internal System.Windows.Forms.TextBox ConfigureScanHostNameTB;
        private System.Windows.Forms.RadioButton ConfigureScanCrawlAndScanRB;
        private System.Windows.Forms.RadioButton ConfigureScanCrawlOnlyRB;
        private System.Windows.Forms.CheckBox ConfigureScanIncludeSubDomainsCB;
        private System.Windows.Forms.CheckBox ConfigureScanDirAndFileGuessingCB;
        private System.Windows.Forms.TextBox ConfigureScanUrlToAvoidTB;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox ConfigureScanStartingUrlTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ConfigureScanHostsToIncludeTB;
        internal System.Windows.Forms.TextBox ConfigureScanErrorTB;

    }
}