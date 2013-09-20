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
    partial class LoadInitialConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadInitialConfigurationForm));
            this.StartBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ProxyListenPortTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.AcceptRemoteNoRB = new System.Windows.Forms.RadioButton();
            this.AcceptRemoteYesRB = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.UpstreamProxyPortTB = new System.Windows.Forms.TextBox();
            this.UpstreamProxyIPTB = new System.Windows.Forms.TextBox();
            this.UseCustomUpstreamProxyRB = new System.Windows.Forms.RadioButton();
            this.DontUseUpstreamProxyRB = new System.Windows.Forms.RadioButton();
            this.UseBrowserUpstreamRB = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.MessageTB = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SetAsSystemProxyNoRB = new System.Windows.Forms.RadioButton();
            this.SetAsSystemProxyYesRB = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ShowImportCertFormLL = new System.Windows.Forms.LinkLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.SystemColors.Control;
            this.StartBtn.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBtn.Location = new System.Drawing.Point(3, 3);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(160, 40);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start IronWASP";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.Location = new System.Drawing.Point(2, 3);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(161, 40);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Green;
            this.panel1.Controls.Add(this.StartBtn);
            this.panel1.Location = new System.Drawing.Point(516, 463);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 46);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Controls.Add(this.CancelBtn);
            this.panel2.Location = new System.Drawing.Point(3, 463);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(166, 46);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Interception Proxy Settings:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Upstream Proxy Settings:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Port number on which this proxy server must listen:";
            // 
            // ProxyListenPortTB
            // 
            this.ProxyListenPortTB.Location = new System.Drawing.Point(256, 60);
            this.ProxyListenPortTB.Name = "ProxyListenPortTB";
            this.ProxyListenPortTB.Size = new System.Drawing.Size(100, 20);
            this.ProxyListenPortTB.TabIndex = 7;
            this.ProxyListenPortTB.TextChanged += new System.EventHandler(this.ProxyListenPortTB_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(409, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "IronWASP needs to start a local HTTP proxy server to enable browser traffic analy" +
    "sis.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(308, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Should this proxy server accept connections from remote hosts?";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.AcceptRemoteNoRB);
            this.panel3.Controls.Add(this.AcceptRemoteYesRB);
            this.panel3.Location = new System.Drawing.Point(320, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(118, 27);
            this.panel3.TabIndex = 10;
            // 
            // AcceptRemoteNoRB
            // 
            this.AcceptRemoteNoRB.AutoSize = true;
            this.AcceptRemoteNoRB.Checked = true;
            this.AcceptRemoteNoRB.Location = new System.Drawing.Point(68, 6);
            this.AcceptRemoteNoRB.Name = "AcceptRemoteNoRB";
            this.AcceptRemoteNoRB.Size = new System.Drawing.Size(39, 17);
            this.AcceptRemoteNoRB.TabIndex = 1;
            this.AcceptRemoteNoRB.TabStop = true;
            this.AcceptRemoteNoRB.Text = "No";
            this.AcceptRemoteNoRB.UseVisualStyleBackColor = true;
            // 
            // AcceptRemoteYesRB
            // 
            this.AcceptRemoteYesRB.AutoSize = true;
            this.AcceptRemoteYesRB.Location = new System.Drawing.Point(8, 5);
            this.AcceptRemoteYesRB.Name = "AcceptRemoteYesRB";
            this.AcceptRemoteYesRB.Size = new System.Drawing.Size(43, 17);
            this.AcceptRemoteYesRB.TabIndex = 0;
            this.AcceptRemoteYesRB.Text = "Yes";
            this.AcceptRemoteYesRB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(469, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Sometimes your company/network provider requires you to use their proxy server fo" +
    "r web browsing";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.UpstreamProxyPortTB);
            this.panel4.Controls.Add(this.UpstreamProxyIPTB);
            this.panel4.Controls.Add(this.UseCustomUpstreamProxyRB);
            this.panel4.Controls.Add(this.DontUseUpstreamProxyRB);
            this.panel4.Controls.Add(this.UseBrowserUpstreamRB);
            this.panel4.Location = new System.Drawing.Point(16, 81);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(652, 71);
            this.panel4.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(258, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "IP/Hostname:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(463, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Port:";
            // 
            // UpstreamProxyPortTB
            // 
            this.UpstreamProxyPortTB.Enabled = false;
            this.UpstreamProxyPortTB.Location = new System.Drawing.Point(494, 47);
            this.UpstreamProxyPortTB.Name = "UpstreamProxyPortTB";
            this.UpstreamProxyPortTB.Size = new System.Drawing.Size(69, 20);
            this.UpstreamProxyPortTB.TabIndex = 9;
            this.UpstreamProxyPortTB.TextChanged += new System.EventHandler(this.UpstreamProxyPortTB_TextChanged);
            // 
            // UpstreamProxyIPTB
            // 
            this.UpstreamProxyIPTB.Enabled = false;
            this.UpstreamProxyIPTB.Location = new System.Drawing.Point(335, 47);
            this.UpstreamProxyIPTB.Name = "UpstreamProxyIPTB";
            this.UpstreamProxyIPTB.Size = new System.Drawing.Size(122, 20);
            this.UpstreamProxyIPTB.TabIndex = 8;
            this.UpstreamProxyIPTB.TextChanged += new System.EventHandler(this.UpstreamProxyIPTB_TextChanged);
            // 
            // UseCustomUpstreamProxyRB
            // 
            this.UseCustomUpstreamProxyRB.AutoSize = true;
            this.UseCustomUpstreamProxyRB.Location = new System.Drawing.Point(8, 48);
            this.UseCustomUpstreamProxyRB.Name = "UseCustomUpstreamProxyRB";
            this.UseCustomUpstreamProxyRB.Size = new System.Drawing.Size(248, 17);
            this.UseCustomUpstreamProxyRB.TabIndex = 2;
            this.UseCustomUpstreamProxyRB.Text = "Use this custom upstream proxy setting instead:";
            this.UseCustomUpstreamProxyRB.UseVisualStyleBackColor = true;
            this.UseCustomUpstreamProxyRB.CheckedChanged += new System.EventHandler(this.UseCustomUpstreamProxyRB_CheckedChanged);
            // 
            // DontUseUpstreamProxyRB
            // 
            this.DontUseUpstreamProxyRB.AutoSize = true;
            this.DontUseUpstreamProxyRB.Location = new System.Drawing.Point(8, 25);
            this.DontUseUpstreamProxyRB.Name = "DontUseUpstreamProxyRB";
            this.DontUseUpstreamProxyRB.Size = new System.Drawing.Size(39, 17);
            this.DontUseUpstreamProxyRB.TabIndex = 1;
            this.DontUseUpstreamProxyRB.Text = "No";
            this.DontUseUpstreamProxyRB.UseVisualStyleBackColor = true;
            // 
            // UseBrowserUpstreamRB
            // 
            this.UseBrowserUpstreamRB.AutoSize = true;
            this.UseBrowserUpstreamRB.Checked = true;
            this.UseBrowserUpstreamRB.Location = new System.Drawing.Point(8, 4);
            this.UseBrowserUpstreamRB.Name = "UseBrowserUpstreamRB";
            this.UseBrowserUpstreamRB.Size = new System.Drawing.Size(551, 17);
            this.UseBrowserUpstreamRB.TabIndex = 0;
            this.UseBrowserUpstreamRB.TabStop = true;
            this.UseBrowserUpstreamRB.Text = "Yes (recommended. If the upstream proxy requires NTLM or other auth then this set" +
    "ting handles it automatically.)";
            this.UseBrowserUpstreamRB.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(437, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Should IronWASP use the same proxy settings as your Internet Explorer or Google C" +
    "hrome?";
            // 
            // MessageTB
            // 
            this.MessageTB.BackColor = System.Drawing.Color.White;
            this.MessageTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessageTB.Location = new System.Drawing.Point(5, 397);
            this.MessageTB.Multiline = true;
            this.MessageTB.Name = "MessageTB";
            this.MessageTB.ReadOnly = true;
            this.MessageTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MessageTB.Size = new System.Drawing.Size(674, 60);
            this.MessageTB.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.SetAsSystemProxyNoRB);
            this.panel5.Controls.Add(this.SetAsSystemProxyYesRB);
            this.panel5.Location = new System.Drawing.Point(465, 29);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(118, 27);
            this.panel5.TabIndex = 16;
            // 
            // SetAsSystemProxyNoRB
            // 
            this.SetAsSystemProxyNoRB.AutoSize = true;
            this.SetAsSystemProxyNoRB.Checked = true;
            this.SetAsSystemProxyNoRB.Location = new System.Drawing.Point(68, 6);
            this.SetAsSystemProxyNoRB.Name = "SetAsSystemProxyNoRB";
            this.SetAsSystemProxyNoRB.Size = new System.Drawing.Size(39, 17);
            this.SetAsSystemProxyNoRB.TabIndex = 1;
            this.SetAsSystemProxyNoRB.TabStop = true;
            this.SetAsSystemProxyNoRB.Text = "No";
            this.SetAsSystemProxyNoRB.UseVisualStyleBackColor = true;
            // 
            // SetAsSystemProxyYesRB
            // 
            this.SetAsSystemProxyYesRB.AutoSize = true;
            this.SetAsSystemProxyYesRB.Location = new System.Drawing.Point(8, 5);
            this.SetAsSystemProxyYesRB.Name = "SetAsSystemProxyYesRB";
            this.SetAsSystemProxyYesRB.Size = new System.Drawing.Size(43, 17);
            this.SetAsSystemProxyYesRB.TabIndex = 0;
            this.SetAsSystemProxyYesRB.Text = "Yes";
            this.SetAsSystemProxyYesRB.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(443, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Do you want to start capturing traffic from your Internet Explorer, Google Chrome" +
    " and Safari ?";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(410, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "(you can configure this later by selecting \'Set as System Proxy\' option in the Pr" +
    "oxy tab)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(180, 18);
            this.label12.TabIndex = 18;
            this.label12.Text = "Activate Traffic Interception:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(542, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "For better HTTPS traffic interception you can import IronWASP as a trusted CA on " +
    "your machine. To find out how ";
            // 
            // ShowImportCertFormLL
            // 
            this.ShowImportCertFormLL.AutoSize = true;
            this.ShowImportCertFormLL.Location = new System.Drawing.Point(547, 71);
            this.ShowImportCertFormLL.Name = "ShowImportCertFormLL";
            this.ShowImportCertFormLL.Size = new System.Drawing.Size(53, 13);
            this.ShowImportCertFormLL.TabIndex = 20;
            this.ShowImportCertFormLL.TabStop = true;
            this.ShowImportCertFormLL.Text = "click here";
            this.ShowImportCertFormLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShowImportCertFormLL_LinkClicked);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.ProxyListenPortTB);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Location = new System.Drawing.Point(6, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(673, 120);
            this.panel6.TabIndex = 21;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.ShowImportCertFormLL);
            this.panel7.Controls.Add(this.panel5);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Location = new System.Drawing.Point(6, 131);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(673, 92);
            this.panel7.TabIndex = 22;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.panel4);
            this.panel8.Location = new System.Drawing.Point(5, 229);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(674, 160);
            this.panel8.TabIndex = 23;
            // 
            // LoadInitialConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(684, 511);
            this.ControlBox = false;
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.MessageTB);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoadInitialConfigurationForm";
            this.Text = "IronWASP Start-up Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoadInitialConfigurationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ProxyListenPortTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton AcceptRemoteNoRB;
        private System.Windows.Forms.RadioButton AcceptRemoteYesRB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton DontUseUpstreamProxyRB;
        private System.Windows.Forms.RadioButton UseBrowserUpstreamRB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton UseCustomUpstreamProxyRB;
        private System.Windows.Forms.TextBox UpstreamProxyIPTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox UpstreamProxyPortTB;
        private System.Windows.Forms.TextBox MessageTB;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton SetAsSystemProxyNoRB;
        private System.Windows.Forms.RadioButton SetAsSystemProxyYesRB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.LinkLabel ShowImportCertFormLL;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
    }
}