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
    partial class CreateNewRequestWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewRequestWizard));
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.CreateRequestTab = new System.Windows.Forms.TabPage();
            this.FromClipboardLL = new System.Windows.Forms.LinkLabel();
            this.FromClipBoardLbl = new System.Windows.Forms.Label();
            this.Step0StatusTB = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PostBodyTypeCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SelectedUserAgentLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UserAgentTree = new System.Windows.Forms.TreeView();
            this.UseUserAgentCB = new System.Windows.Forms.CheckBox();
            this.UseAdditionalHeadersCB = new System.Windows.Forms.CheckBox();
            this.PostBodyTB = new System.Windows.Forms.TextBox();
            this.UsePostBodyCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.RequestUrlTB = new System.Windows.Forms.TextBox();
            this.Step0NextBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.NameRequestTab = new System.Windows.Forms.TabPage();
            this.Step1StatusTB = new System.Windows.Forms.TextBox();
            this.Step1PreviousStepBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RequestNameTB = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.CreateRequestBtn = new System.Windows.Forms.Button();
            this.BaseTabs.SuspendLayout();
            this.CreateRequestTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.NameRequestTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseTabs
            // 
            this.BaseTabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.BaseTabs.Controls.Add(this.CreateRequestTab);
            this.BaseTabs.Controls.Add(this.NameRequestTab);
            this.BaseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseTabs.Location = new System.Drawing.Point(0, 0);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(784, 512);
            this.BaseTabs.TabIndex = 1;
            this.BaseTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.BaseTabs_Selecting);
            // 
            // CreateRequestTab
            // 
            this.CreateRequestTab.Controls.Add(this.FromClipboardLL);
            this.CreateRequestTab.Controls.Add(this.FromClipBoardLbl);
            this.CreateRequestTab.Controls.Add(this.Step0StatusTB);
            this.CreateRequestTab.Controls.Add(this.label30);
            this.CreateRequestTab.Controls.Add(this.label3);
            this.CreateRequestTab.Controls.Add(this.panel1);
            this.CreateRequestTab.Controls.Add(this.label1);
            this.CreateRequestTab.Controls.Add(this.textBox2);
            this.CreateRequestTab.Controls.Add(this.RequestUrlTB);
            this.CreateRequestTab.Controls.Add(this.Step0NextBtn);
            this.CreateRequestTab.Controls.Add(this.CancelBtn);
            this.CreateRequestTab.Location = new System.Drawing.Point(4, 25);
            this.CreateRequestTab.Margin = new System.Windows.Forms.Padding(0);
            this.CreateRequestTab.Name = "CreateRequestTab";
            this.CreateRequestTab.Padding = new System.Windows.Forms.Padding(5);
            this.CreateRequestTab.Size = new System.Drawing.Size(776, 483);
            this.CreateRequestTab.TabIndex = 0;
            this.CreateRequestTab.Text = "               Create Request               ";
            this.CreateRequestTab.UseVisualStyleBackColor = true;
            // 
            // FromClipboardLL
            // 
            this.FromClipboardLL.AutoSize = true;
            this.FromClipboardLL.Location = new System.Drawing.Point(734, 101);
            this.FromClipboardLL.Name = "FromClipboardLL";
            this.FromClipboardLL.Size = new System.Drawing.Size(25, 13);
            this.FromClipboardLL.TabIndex = 17;
            this.FromClipboardLL.TabStop = true;
            this.FromClipboardLL.Text = "Yes";
            this.FromClipboardLL.Visible = false;
            this.FromClipboardLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FromClipboardLL_LinkClicked);
            // 
            // FromClipBoardLbl
            // 
            this.FromClipBoardLbl.AutoSize = true;
            this.FromClipBoardLbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.FromClipBoardLbl.Location = new System.Drawing.Point(199, 101);
            this.FromClipBoardLbl.Name = "FromClipBoardLbl";
            this.FromClipBoardLbl.Size = new System.Drawing.Size(530, 13);
            this.FromClipBoardLbl.TabIndex = 16;
            this.FromClipBoardLbl.Text = "IMP: IronWASP has detected that you have copied an HTTP Request. Do you want to u" +
    "se this Request here?";
            this.FromClipBoardLbl.Visible = false;
            // 
            // Step0StatusTB
            // 
            this.Step0StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step0StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step0StatusTB.Location = new System.Drawing.Point(113, 446);
            this.Step0StatusTB.Multiline = true;
            this.Step0StatusTB.Name = "Step0StatusTB";
            this.Step0StatusTB.Size = new System.Drawing.Size(550, 29);
            this.Step0StatusTB.TabIndex = 14;
            this.Step0StatusTB.TabStop = false;
            this.Step0StatusTB.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Blue;
            this.label30.Location = new System.Drawing.Point(15, 79);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(211, 13);
            this.label30.TabIndex = 13;
            this.label30.Text = "NOTE: Url must start with http:// or https://";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Optional Settings:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.Controls.Add(this.PostBodyTypeCombo);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.SelectedUserAgentLbl);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.UserAgentTree);
            this.panel1.Controls.Add(this.UseUserAgentCB);
            this.panel1.Controls.Add(this.UseAdditionalHeadersCB);
            this.panel1.Controls.Add(this.PostBodyTB);
            this.panel1.Controls.Add(this.UsePostBodyCB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 313);
            this.panel1.TabIndex = 10;
            // 
            // PostBodyTypeCombo
            // 
            this.PostBodyTypeCombo.Enabled = false;
            this.PostBodyTypeCombo.FormattingEnabled = true;
            this.PostBodyTypeCombo.Items.AddRange(new object[] {
            "application/x-www-form-urlencoded",
            "application/json",
            "application/xml"});
            this.PostBodyTypeCombo.Location = new System.Drawing.Point(386, 286);
            this.PostBodyTypeCombo.Name = "PostBodyTypeCombo";
            this.PostBodyTypeCombo.Size = new System.Drawing.Size(374, 21);
            this.PostBodyTypeCombo.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Select Content-Type Header value:";
            // 
            // SelectedUserAgentLbl
            // 
            this.SelectedUserAgentLbl.AutoSize = true;
            this.SelectedUserAgentLbl.Location = new System.Drawing.Point(159, 178);
            this.SelectedUserAgentLbl.Name = "SelectedUserAgentLbl";
            this.SelectedUserAgentLbl.Size = new System.Drawing.Size(181, 13);
            this.SelectedUserAgentLbl.TabIndex = 17;
            this.SelectedUserAgentLbl.Text = "                                                          ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Selected User-Agent Value:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Select User-Agent from list:";
            // 
            // UserAgentTree
            // 
            this.UserAgentTree.Enabled = false;
            this.UserAgentTree.Location = new System.Drawing.Point(167, 32);
            this.UserAgentTree.Name = "UserAgentTree";
            this.UserAgentTree.ShowRootLines = false;
            this.UserAgentTree.Size = new System.Drawing.Size(594, 138);
            this.UserAgentTree.TabIndex = 14;
            this.UserAgentTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.UserAgentTree_AfterSelect);
            // 
            // UseUserAgentCB
            // 
            this.UseUserAgentCB.AutoSize = true;
            this.UseUserAgentCB.Location = new System.Drawing.Point(7, 9);
            this.UseUserAgentCB.Name = "UseUserAgentCB";
            this.UseUserAgentCB.Size = new System.Drawing.Size(192, 17);
            this.UseUserAgentCB.TabIndex = 13;
            this.UseUserAgentCB.Text = "Use an User-Agent for the Request";
            this.UseUserAgentCB.UseVisualStyleBackColor = true;
            this.UseUserAgentCB.CheckedChanged += new System.EventHandler(this.UseUserAgentCB_CheckedChanged);
            // 
            // UseAdditionalHeadersCB
            // 
            this.UseAdditionalHeadersCB.AutoSize = true;
            this.UseAdditionalHeadersCB.Location = new System.Drawing.Point(9, 210);
            this.UseAdditionalHeadersCB.Name = "UseAdditionalHeadersCB";
            this.UseAdditionalHeadersCB.Size = new System.Drawing.Size(368, 17);
            this.UseAdditionalHeadersCB.TabIndex = 12;
            this.UseAdditionalHeadersCB.Text = "Add headers like \'Accept-Encoding\', \'Accept-Charset\' etc to the Request";
            this.UseAdditionalHeadersCB.UseVisualStyleBackColor = true;
            // 
            // PostBodyTB
            // 
            this.PostBodyTB.Enabled = false;
            this.PostBodyTB.Location = new System.Drawing.Point(216, 235);
            this.PostBodyTB.Multiline = true;
            this.PostBodyTB.Name = "PostBodyTB";
            this.PostBodyTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PostBodyTB.Size = new System.Drawing.Size(545, 44);
            this.PostBodyTB.TabIndex = 7;
            // 
            // UsePostBodyCB
            // 
            this.UsePostBodyCB.AutoSize = true;
            this.UsePostBodyCB.Location = new System.Drawing.Point(9, 235);
            this.UsePostBodyCB.Name = "UsePostBodyCB";
            this.UsePostBodyCB.Size = new System.Drawing.Size(206, 17);
            this.UsePostBodyCB.TabIndex = 6;
            this.UsePostBodyCB.Text = "Include a POST body with the request";
            this.UsePostBodyCB.UseVisualStyleBackColor = true;
            this.UsePostBodyCB.CheckedChanged += new System.EventHandler(this.UsePostBodyCB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Enter POST body:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter URL:";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox2.Location = new System.Drawing.Point(5, 5);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(766, 40);
            this.textBox2.TabIndex = 4;
            this.textBox2.TabStop = false;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // RequestUrlTB
            // 
            this.RequestUrlTB.Location = new System.Drawing.Point(76, 51);
            this.RequestUrlTB.Name = "RequestUrlTB";
            this.RequestUrlTB.Size = new System.Drawing.Size(692, 20);
            this.RequestUrlTB.TabIndex = 2;
            // 
            // Step0NextBtn
            // 
            this.Step0NextBtn.Location = new System.Drawing.Point(674, 453);
            this.Step0NextBtn.Name = "Step0NextBtn";
            this.Step0NextBtn.Size = new System.Drawing.Size(94, 23);
            this.Step0NextBtn.TabIndex = 1;
            this.Step0NextBtn.Text = "Next Step ->";
            this.Step0NextBtn.UseVisualStyleBackColor = true;
            this.Step0NextBtn.Click += new System.EventHandler(this.Step0NextBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(8, 453);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(94, 23);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // NameRequestTab
            // 
            this.NameRequestTab.Controls.Add(this.Step1StatusTB);
            this.NameRequestTab.Controls.Add(this.Step1PreviousStepBtn);
            this.NameRequestTab.Controls.Add(this.label7);
            this.NameRequestTab.Controls.Add(this.label6);
            this.NameRequestTab.Controls.Add(this.RequestNameTB);
            this.NameRequestTab.Controls.Add(this.textBox4);
            this.NameRequestTab.Controls.Add(this.CreateRequestBtn);
            this.NameRequestTab.Location = new System.Drawing.Point(4, 25);
            this.NameRequestTab.Margin = new System.Windows.Forms.Padding(0);
            this.NameRequestTab.Name = "NameRequestTab";
            this.NameRequestTab.Padding = new System.Windows.Forms.Padding(5);
            this.NameRequestTab.Size = new System.Drawing.Size(776, 483);
            this.NameRequestTab.TabIndex = 1;
            this.NameRequestTab.Text = "               Name Request               ";
            this.NameRequestTab.UseVisualStyleBackColor = true;
            // 
            // Step1StatusTB
            // 
            this.Step1StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step1StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step1StatusTB.Location = new System.Drawing.Point(27, 158);
            this.Step1StatusTB.Multiline = true;
            this.Step1StatusTB.Name = "Step1StatusTB";
            this.Step1StatusTB.Size = new System.Drawing.Size(550, 29);
            this.Step1StatusTB.TabIndex = 15;
            this.Step1StatusTB.TabStop = false;
            this.Step1StatusTB.Visible = false;
            // 
            // Step1PreviousStepBtn
            // 
            this.Step1PreviousStepBtn.Location = new System.Drawing.Point(8, 452);
            this.Step1PreviousStepBtn.Name = "Step1PreviousStepBtn";
            this.Step1PreviousStepBtn.Size = new System.Drawing.Size(110, 23);
            this.Step1PreviousStepBtn.TabIndex = 9;
            this.Step1PreviousStepBtn.Text = "<- Previous Step";
            this.Step1PreviousStepBtn.UseVisualStyleBackColor = true;
            this.Step1PreviousStepBtn.Click += new System.EventHandler(this.Step1PreviousStepBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "( if left blank a name is automatically assigned )";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Name the Request:";
            // 
            // RequestNameTB
            // 
            this.RequestNameTB.Location = new System.Drawing.Point(128, 113);
            this.RequestNameTB.Name = "RequestNameTB";
            this.RequestNameTB.Size = new System.Drawing.Size(197, 20);
            this.RequestNameTB.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox4.Location = new System.Drawing.Point(5, 5);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(766, 77);
            this.textBox4.TabIndex = 5;
            this.textBox4.TabStop = false;
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // CreateRequestBtn
            // 
            this.CreateRequestBtn.Location = new System.Drawing.Point(588, 104);
            this.CreateRequestBtn.Name = "CreateRequestBtn";
            this.CreateRequestBtn.Size = new System.Drawing.Size(166, 36);
            this.CreateRequestBtn.TabIndex = 0;
            this.CreateRequestBtn.Text = "Create Request";
            this.CreateRequestBtn.UseVisualStyleBackColor = true;
            this.CreateRequestBtn.Click += new System.EventHandler(this.CreateRequestBtn_Click);
            // 
            // CreateNewRequestWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 512);
            this.Controls.Add(this.BaseTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateNewRequestWizard";
            this.Text = "New Request Creation Wizard";
            this.Load += new System.EventHandler(this.CreateNewRequestWizard_Load);
            this.BaseTabs.ResumeLayout(false);
            this.CreateRequestTab.ResumeLayout(false);
            this.CreateRequestTab.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.NameRequestTab.ResumeLayout(false);
            this.NameRequestTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl BaseTabs;
        private System.Windows.Forms.TabPage CreateRequestTab;
        private System.Windows.Forms.TabPage NameRequestTab;
        private System.Windows.Forms.Button Step0NextBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox RequestUrlTB;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PostBodyTB;
        private System.Windows.Forms.CheckBox UsePostBodyCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox UseAdditionalHeadersCB;
        private System.Windows.Forms.CheckBox UseUserAgentCB;
        private System.Windows.Forms.TreeView UserAgentTree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label SelectedUserAgentLbl;
        private System.Windows.Forms.Button CreateRequestBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RequestNameTB;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label30;
        internal System.Windows.Forms.TextBox Step0StatusTB;
        private System.Windows.Forms.Button Step1PreviousStepBtn;
        internal System.Windows.Forms.TextBox Step1StatusTB;
        private System.Windows.Forms.Label FromClipBoardLbl;
        private System.Windows.Forms.LinkLabel FromClipboardLL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox PostBodyTypeCombo;

    }
}