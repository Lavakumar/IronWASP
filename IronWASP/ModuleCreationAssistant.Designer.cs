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
    partial class ModuleCreationAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleCreationAssistant));
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.NameTab = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ModuleDisplayNameTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ModuleDescTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ModuleNameTB = new System.Windows.Forms.TextBox();
            this.Step0StatusTB = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.StepOneNextBtn = new System.Windows.Forms.Button();
            this.DetailsTab = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ModuleProjectHomeTB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ModuleAuthorTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ModuleTypeLogRB = new System.Windows.Forms.RadioButton();
            this.ModuleTypeMainRB = new System.Windows.Forms.RadioButton();
            this.Step1StatusTB = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.StepTwoPreviousBtn = new System.Windows.Forms.Button();
            this.StepTwoNextBtn = new System.Windows.Forms.Button();
            this.LanguageTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ModuleLangRubyRB = new System.Windows.Forms.RadioButton();
            this.ModuleLangPythonRB = new System.Windows.Forms.RadioButton();
            this.Step2StatusTB = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.StepThreePreviousBtn = new System.Windows.Forms.Button();
            this.StepThreeNextBtn = new System.Windows.Forms.Button();
            this.FinalTab = new System.Windows.Forms.TabPage();
            this.ModuleFileTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FinalBtn = new System.Windows.Forms.Button();
            this.BaseTabs.SuspendLayout();
            this.NameTab.SuspendLayout();
            this.DetailsTab.SuspendLayout();
            this.LanguageTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FinalTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseTabs
            // 
            this.BaseTabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.BaseTabs.Controls.Add(this.NameTab);
            this.BaseTabs.Controls.Add(this.DetailsTab);
            this.BaseTabs.Controls.Add(this.LanguageTab);
            this.BaseTabs.Controls.Add(this.FinalTab);
            this.BaseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseTabs.Location = new System.Drawing.Point(0, 0);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(684, 462);
            this.BaseTabs.TabIndex = 17;
            this.BaseTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.BaseTabs_Selecting);
            // 
            // NameTab
            // 
            this.NameTab.Controls.Add(this.label9);
            this.NameTab.Controls.Add(this.label8);
            this.NameTab.Controls.Add(this.label7);
            this.NameTab.Controls.Add(this.ModuleDisplayNameTB);
            this.NameTab.Controls.Add(this.label2);
            this.NameTab.Controls.Add(this.ModuleDescTB);
            this.NameTab.Controls.Add(this.label1);
            this.NameTab.Controls.Add(this.ModuleNameTB);
            this.NameTab.Controls.Add(this.Step0StatusTB);
            this.NameTab.Controls.Add(this.CancelBtn);
            this.NameTab.Controls.Add(this.textBox2);
            this.NameTab.Controls.Add(this.StepOneNextBtn);
            this.NameTab.Location = new System.Drawing.Point(4, 25);
            this.NameTab.Margin = new System.Windows.Forms.Padding(0);
            this.NameTab.Name = "NameTab";
            this.NameTab.Padding = new System.Windows.Forms.Padding(5);
            this.NameTab.Size = new System.Drawing.Size(676, 433);
            this.NameTab.TabIndex = 0;
            this.NameTab.Text = "             Module Name             ";
            this.NameTab.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(443, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "(name that appears in the menu. Eg: HAWAS - Hybrid Analyzer for Web Application S" +
    "ecurity)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "(must be alphanumeric only. Eg: HAWAS)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Module Display Name:";
            // 
            // ModuleDisplayNameTB
            // 
            this.ModuleDisplayNameTB.Location = new System.Drawing.Point(130, 163);
            this.ModuleDisplayNameTB.Name = "ModuleDisplayNameTB";
            this.ModuleDisplayNameTB.Size = new System.Drawing.Size(536, 20);
            this.ModuleDisplayNameTB.TabIndex = 2;
            this.ModuleDisplayNameTB.TextChanged += new System.EventHandler(this.ModuleDisplayNameTB_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(457, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Module Description:   (A brief description of what the modules does and how it be" +
    "nefits the user)";
            // 
            // ModuleDescTB
            // 
            this.ModuleDescTB.Location = new System.Drawing.Point(14, 252);
            this.ModuleDescTB.Multiline = true;
            this.ModuleDescTB.Name = "ModuleDescTB";
            this.ModuleDescTB.Size = new System.Drawing.Size(652, 99);
            this.ModuleDescTB.TabIndex = 3;
            this.ModuleDescTB.TextChanged += new System.EventHandler(this.ModuleDescTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Module Name:";
            // 
            // ModuleNameTB
            // 
            this.ModuleNameTB.Location = new System.Drawing.Point(130, 95);
            this.ModuleNameTB.Name = "ModuleNameTB";
            this.ModuleNameTB.Size = new System.Drawing.Size(536, 20);
            this.ModuleNameTB.TabIndex = 1;
            this.ModuleNameTB.TextChanged += new System.EventHandler(this.ModuleNameTB_TextChanged);
            // 
            // Step0StatusTB
            // 
            this.Step0StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step0StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step0StatusTB.Location = new System.Drawing.Point(130, 374);
            this.Step0StatusTB.Multiline = true;
            this.Step0StatusTB.Name = "Step0StatusTB";
            this.Step0StatusTB.Size = new System.Drawing.Size(409, 51);
            this.Step0StatusTB.TabIndex = 6;
            this.Step0StatusTB.TabStop = false;
            this.Step0StatusTB.Visible = false;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(8, 402);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(105, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox2.Location = new System.Drawing.Point(5, 5);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(666, 61);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // StepOneNextBtn
            // 
            this.StepOneNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepOneNextBtn.Location = new System.Drawing.Point(561, 402);
            this.StepOneNextBtn.Name = "StepOneNextBtn";
            this.StepOneNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepOneNextBtn.TabIndex = 4;
            this.StepOneNextBtn.Text = "Next Step ->";
            this.StepOneNextBtn.UseVisualStyleBackColor = true;
            this.StepOneNextBtn.Click += new System.EventHandler(this.StepOneNextBtn_Click);
            // 
            // DetailsTab
            // 
            this.DetailsTab.Controls.Add(this.label3);
            this.DetailsTab.Controls.Add(this.label10);
            this.DetailsTab.Controls.Add(this.label11);
            this.DetailsTab.Controls.Add(this.ModuleProjectHomeTB);
            this.DetailsTab.Controls.Add(this.label12);
            this.DetailsTab.Controls.Add(this.ModuleAuthorTB);
            this.DetailsTab.Controls.Add(this.label5);
            this.DetailsTab.Controls.Add(this.ModuleTypeLogRB);
            this.DetailsTab.Controls.Add(this.ModuleTypeMainRB);
            this.DetailsTab.Controls.Add(this.Step1StatusTB);
            this.DetailsTab.Controls.Add(this.textBox4);
            this.DetailsTab.Controls.Add(this.StepTwoPreviousBtn);
            this.DetailsTab.Controls.Add(this.StepTwoNextBtn);
            this.DetailsTab.Location = new System.Drawing.Point(4, 25);
            this.DetailsTab.Margin = new System.Windows.Forms.Padding(0);
            this.DetailsTab.Name = "DetailsTab";
            this.DetailsTab.Padding = new System.Windows.Forms.Padding(5);
            this.DetailsTab.Size = new System.Drawing.Size(676, 433);
            this.DetailsTab.TabIndex = 1;
            this.DetailsTab.Text = "             Author & Module Details             ";
            this.DetailsTab.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "(Url of the Module\'s Github repository or home website)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "(Name of the Module\'s Author)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Project Home:";
            // 
            // ModuleProjectHomeTB
            // 
            this.ModuleProjectHomeTB.Location = new System.Drawing.Point(128, 140);
            this.ModuleProjectHomeTB.Name = "ModuleProjectHomeTB";
            this.ModuleProjectHomeTB.Size = new System.Drawing.Size(536, 20);
            this.ModuleProjectHomeTB.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(37, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Author Name:";
            // 
            // ModuleAuthorTB
            // 
            this.ModuleAuthorTB.Location = new System.Drawing.Point(128, 72);
            this.ModuleAuthorTB.Name = "ModuleAuthorTB";
            this.ModuleAuthorTB.Size = new System.Drawing.Size(536, 20);
            this.ModuleAuthorTB.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(463, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Modules can be launched from different places. Please specifiy how this module wi" +
    "l be launched.";
            // 
            // ModuleTypeLogRB
            // 
            this.ModuleTypeLogRB.AutoSize = true;
            this.ModuleTypeLogRB.Location = new System.Drawing.Point(76, 310);
            this.ModuleTypeLogRB.Name = "ModuleTypeLogRB";
            this.ModuleTypeLogRB.Size = new System.Drawing.Size(224, 17);
            this.ModuleTypeLogRB.TabIndex = 4;
            this.ModuleTypeLogRB.Text = "Module launched by right-clicking on a log";
            this.ModuleTypeLogRB.UseVisualStyleBackColor = true;
            // 
            // ModuleTypeMainRB
            // 
            this.ModuleTypeMainRB.AutoSize = true;
            this.ModuleTypeMainRB.Checked = true;
            this.ModuleTypeMainRB.Location = new System.Drawing.Point(76, 277);
            this.ModuleTypeMainRB.Name = "ModuleTypeMainRB";
            this.ModuleTypeMainRB.Size = new System.Drawing.Size(185, 17);
            this.ModuleTypeMainRB.TabIndex = 3;
            this.ModuleTypeMainRB.TabStop = true;
            this.ModuleTypeMainRB.Text = "Module launched from Main menu";
            this.ModuleTypeMainRB.UseVisualStyleBackColor = true;
            // 
            // Step1StatusTB
            // 
            this.Step1StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step1StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step1StatusTB.Location = new System.Drawing.Point(119, 402);
            this.Step1StatusTB.Multiline = true;
            this.Step1StatusTB.Name = "Step1StatusTB";
            this.Step1StatusTB.Size = new System.Drawing.Size(438, 23);
            this.Step1StatusTB.TabIndex = 11;
            this.Step1StatusTB.TabStop = false;
            this.Step1StatusTB.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox4.Location = new System.Drawing.Point(5, 5);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(666, 45);
            this.textBox4.TabIndex = 9;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "\r\nPlease specify the payloads you want to be sent by this Active Plugin. You can " +
    "either type the payloads one per line or provide a file with the list of payload" +
    "s.\r\n\r\n";
            // 
            // StepTwoPreviousBtn
            // 
            this.StepTwoPreviousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepTwoPreviousBtn.Location = new System.Drawing.Point(8, 402);
            this.StepTwoPreviousBtn.Name = "StepTwoPreviousBtn";
            this.StepTwoPreviousBtn.Size = new System.Drawing.Size(105, 23);
            this.StepTwoPreviousBtn.TabIndex = 6;
            this.StepTwoPreviousBtn.Text = "<-Previous Step";
            this.StepTwoPreviousBtn.UseVisualStyleBackColor = true;
            this.StepTwoPreviousBtn.Click += new System.EventHandler(this.StepTwoPreviousBtn_Click);
            // 
            // StepTwoNextBtn
            // 
            this.StepTwoNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepTwoNextBtn.Location = new System.Drawing.Point(563, 402);
            this.StepTwoNextBtn.Name = "StepTwoNextBtn";
            this.StepTwoNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepTwoNextBtn.TabIndex = 5;
            this.StepTwoNextBtn.Text = "Next Step ->";
            this.StepTwoNextBtn.UseVisualStyleBackColor = true;
            this.StepTwoNextBtn.Click += new System.EventHandler(this.StepTwoNextBtn_Click);
            // 
            // LanguageTab
            // 
            this.LanguageTab.Controls.Add(this.groupBox2);
            this.LanguageTab.Controls.Add(this.Step2StatusTB);
            this.LanguageTab.Controls.Add(this.textBox5);
            this.LanguageTab.Controls.Add(this.StepThreePreviousBtn);
            this.LanguageTab.Controls.Add(this.StepThreeNextBtn);
            this.LanguageTab.Location = new System.Drawing.Point(4, 25);
            this.LanguageTab.Name = "LanguageTab";
            this.LanguageTab.Padding = new System.Windows.Forms.Padding(5);
            this.LanguageTab.Size = new System.Drawing.Size(676, 433);
            this.LanguageTab.TabIndex = 2;
            this.LanguageTab.Text = "             Select Language             ";
            this.LanguageTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ModuleLangRubyRB);
            this.groupBox2.Controls.Add(this.ModuleLangPythonRB);
            this.groupBox2.Location = new System.Drawing.Point(7, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 110);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Which language should the Module be created in?";
            // 
            // ModuleLangRubyRB
            // 
            this.ModuleLangRubyRB.AutoSize = true;
            this.ModuleLangRubyRB.Location = new System.Drawing.Point(12, 79);
            this.ModuleLangRubyRB.Name = "ModuleLangRubyRB";
            this.ModuleLangRubyRB.Size = new System.Drawing.Size(50, 17);
            this.ModuleLangRubyRB.TabIndex = 2;
            this.ModuleLangRubyRB.Text = "Ruby";
            this.ModuleLangRubyRB.UseVisualStyleBackColor = true;
            // 
            // ModuleLangPythonRB
            // 
            this.ModuleLangPythonRB.AutoSize = true;
            this.ModuleLangPythonRB.Checked = true;
            this.ModuleLangPythonRB.Location = new System.Drawing.Point(12, 29);
            this.ModuleLangPythonRB.Name = "ModuleLangPythonRB";
            this.ModuleLangPythonRB.Size = new System.Drawing.Size(58, 17);
            this.ModuleLangPythonRB.TabIndex = 1;
            this.ModuleLangPythonRB.TabStop = true;
            this.ModuleLangPythonRB.Text = "Python";
            this.ModuleLangPythonRB.UseVisualStyleBackColor = true;
            // 
            // Step2StatusTB
            // 
            this.Step2StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Step2StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Step2StatusTB.Location = new System.Drawing.Point(132, 385);
            this.Step2StatusTB.Multiline = true;
            this.Step2StatusTB.Name = "Step2StatusTB";
            this.Step2StatusTB.Size = new System.Drawing.Size(410, 40);
            this.Step2StatusTB.TabIndex = 13;
            this.Step2StatusTB.TabStop = false;
            this.Step2StatusTB.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Location = new System.Drawing.Point(5, 5);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(666, 77);
            this.textBox5.TabIndex = 11;
            this.textBox5.TabStop = false;
            this.textBox5.Text = resources.GetString("textBox5.Text");
            // 
            // StepThreePreviousBtn
            // 
            this.StepThreePreviousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepThreePreviousBtn.Location = new System.Drawing.Point(8, 402);
            this.StepThreePreviousBtn.Name = "StepThreePreviousBtn";
            this.StepThreePreviousBtn.Size = new System.Drawing.Size(105, 23);
            this.StepThreePreviousBtn.TabIndex = 4;
            this.StepThreePreviousBtn.Text = "<-Previous Step";
            this.StepThreePreviousBtn.UseVisualStyleBackColor = true;
            this.StepThreePreviousBtn.Click += new System.EventHandler(this.StepThreePreviousBtn_Click);
            // 
            // StepThreeNextBtn
            // 
            this.StepThreeNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepThreeNextBtn.Location = new System.Drawing.Point(563, 402);
            this.StepThreeNextBtn.Name = "StepThreeNextBtn";
            this.StepThreeNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepThreeNextBtn.TabIndex = 3;
            this.StepThreeNextBtn.Text = "Next Step ->";
            this.StepThreeNextBtn.UseVisualStyleBackColor = true;
            this.StepThreeNextBtn.Click += new System.EventHandler(this.StepThreeNextBtn_Click);
            // 
            // FinalTab
            // 
            this.FinalTab.Controls.Add(this.ModuleFileTB);
            this.FinalTab.Controls.Add(this.label6);
            this.FinalTab.Controls.Add(this.textBox1);
            this.FinalTab.Controls.Add(this.FinalBtn);
            this.FinalTab.Location = new System.Drawing.Point(4, 25);
            this.FinalTab.Name = "FinalTab";
            this.FinalTab.Padding = new System.Windows.Forms.Padding(5);
            this.FinalTab.Size = new System.Drawing.Size(676, 433);
            this.FinalTab.TabIndex = 3;
            this.FinalTab.Text = "             Done             ";
            this.FinalTab.UseVisualStyleBackColor = true;
            // 
            // ModuleFileTB
            // 
            this.ModuleFileTB.Location = new System.Drawing.Point(130, 280);
            this.ModuleFileTB.Name = "ModuleFileTB";
            this.ModuleFileTB.Size = new System.Drawing.Size(536, 20);
            this.ModuleFileTB.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 283);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Module Files Location:";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(16, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(637, 209);
            this.textBox1.TabIndex = 21;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // FinalBtn
            // 
            this.FinalBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FinalBtn.Location = new System.Drawing.Point(491, 390);
            this.FinalBtn.Name = "FinalBtn";
            this.FinalBtn.Size = new System.Drawing.Size(177, 35);
            this.FinalBtn.TabIndex = 1;
            this.FinalBtn.Text = "Close this Assistant";
            this.FinalBtn.UseVisualStyleBackColor = true;
            this.FinalBtn.Click += new System.EventHandler(this.FinalBtn_Click);
            // 
            // ModuleCreationAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.BaseTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModuleCreationAssistant";
            this.Text = "Module Creation Assistant";
            this.BaseTabs.ResumeLayout(false);
            this.NameTab.ResumeLayout(false);
            this.NameTab.PerformLayout();
            this.DetailsTab.ResumeLayout(false);
            this.DetailsTab.PerformLayout();
            this.LanguageTab.ResumeLayout(false);
            this.LanguageTab.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.FinalTab.ResumeLayout(false);
            this.FinalTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl BaseTabs;
        private System.Windows.Forms.TabPage NameTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ModuleDescTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ModuleNameTB;
        internal System.Windows.Forms.TextBox Step0StatusTB;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button StepOneNextBtn;
        private System.Windows.Forms.TabPage DetailsTab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton ModuleTypeLogRB;
        private System.Windows.Forms.RadioButton ModuleTypeMainRB;
        internal System.Windows.Forms.TextBox Step1StatusTB;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button StepTwoPreviousBtn;
        private System.Windows.Forms.Button StepTwoNextBtn;
        private System.Windows.Forms.TabPage LanguageTab;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton ModuleLangRubyRB;
        private System.Windows.Forms.RadioButton ModuleLangPythonRB;
        internal System.Windows.Forms.TextBox Step2StatusTB;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button StepThreePreviousBtn;
        private System.Windows.Forms.Button StepThreeNextBtn;
        private System.Windows.Forms.TabPage FinalTab;
        private System.Windows.Forms.TextBox ModuleFileTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button FinalBtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ModuleDisplayNameTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ModuleProjectHomeTB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ModuleAuthorTB;
    }
}