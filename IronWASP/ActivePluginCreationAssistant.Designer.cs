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
    partial class ActivePluginCreationAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivePluginCreationAssistant));
            this.BaseTabs = new System.Windows.Forms.TabControl();
            this.NameTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.PluginDescTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PluginNameTB = new System.Windows.Forms.TextBox();
            this.Step0StatusTB = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.StepOneNextBtn = new System.Windows.Forms.Button();
            this.PayloadsTab = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NoOriginalParameterRB = new System.Windows.Forms.RadioButton();
            this.OriginalParameterAfterPayloadRB = new System.Windows.Forms.RadioButton();
            this.OriginalParameterBeforePayloadRB = new System.Windows.Forms.RadioButton();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.PayloadsFileLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PayloadsListTB = new System.Windows.Forms.TextBox();
            this.LoadPayloadsFileLL = new System.Windows.Forms.LinkLabel();
            this.Step1StatusTB = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.StepTwoPreviousBtn = new System.Windows.Forms.Button();
            this.StepTwoNextBtn = new System.Windows.Forms.Button();
            this.LanguageTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PluginLangRubyRB = new System.Windows.Forms.RadioButton();
            this.PluginLangPythonRB = new System.Windows.Forms.RadioButton();
            this.Step2StatusTB = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.StepThreePreviousBtn = new System.Windows.Forms.Button();
            this.StepThreeNextBtn = new System.Windows.Forms.Button();
            this.FinalTab = new System.Windows.Forms.TabPage();
            this.PluginFileTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FinalBtn = new System.Windows.Forms.Button();
            this.PayloadFileOpener = new System.Windows.Forms.OpenFileDialog();
            this.BaseTabs.SuspendLayout();
            this.NameTab.SuspendLayout();
            this.PayloadsTab.SuspendLayout();
            this.LanguageTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FinalTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseTabs
            // 
            this.BaseTabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.BaseTabs.Controls.Add(this.NameTab);
            this.BaseTabs.Controls.Add(this.PayloadsTab);
            this.BaseTabs.Controls.Add(this.LanguageTab);
            this.BaseTabs.Controls.Add(this.FinalTab);
            this.BaseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseTabs.Location = new System.Drawing.Point(0, 0);
            this.BaseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.BaseTabs.Name = "BaseTabs";
            this.BaseTabs.Padding = new System.Drawing.Point(0, 0);
            this.BaseTabs.SelectedIndex = 0;
            this.BaseTabs.Size = new System.Drawing.Size(684, 461);
            this.BaseTabs.TabIndex = 16;
            this.BaseTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.BaseTabs_Selecting);
            // 
            // NameTab
            // 
            this.NameTab.Controls.Add(this.label2);
            this.NameTab.Controls.Add(this.PluginDescTB);
            this.NameTab.Controls.Add(this.label1);
            this.NameTab.Controls.Add(this.PluginNameTB);
            this.NameTab.Controls.Add(this.Step0StatusTB);
            this.NameTab.Controls.Add(this.CancelBtn);
            this.NameTab.Controls.Add(this.textBox2);
            this.NameTab.Controls.Add(this.StepOneNextBtn);
            this.NameTab.Location = new System.Drawing.Point(4, 25);
            this.NameTab.Margin = new System.Windows.Forms.Padding(0);
            this.NameTab.Name = "NameTab";
            this.NameTab.Padding = new System.Windows.Forms.Padding(5);
            this.NameTab.Size = new System.Drawing.Size(676, 432);
            this.NameTab.TabIndex = 0;
            this.NameTab.Text = "               Plugin Name               ";
            this.NameTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Plugin Description:";
            // 
            // PluginDescTB
            // 
            this.PluginDescTB.Location = new System.Drawing.Point(110, 142);
            this.PluginDescTB.Name = "PluginDescTB";
            this.PluginDescTB.Size = new System.Drawing.Size(556, 20);
            this.PluginDescTB.TabIndex = 9;
            this.PluginDescTB.TextChanged += new System.EventHandler(this.PluginDescTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Plugin Name:";
            // 
            // PluginNameTB
            // 
            this.PluginNameTB.Location = new System.Drawing.Point(110, 95);
            this.PluginNameTB.Name = "PluginNameTB";
            this.PluginNameTB.Size = new System.Drawing.Size(556, 20);
            this.PluginNameTB.TabIndex = 7;
            this.PluginNameTB.TextChanged += new System.EventHandler(this.PluginNameTB_TextChanged);
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
            this.Step0StatusTB.Size = new System.Drawing.Size(409, 50);
            this.Step0StatusTB.TabIndex = 6;
            this.Step0StatusTB.TabStop = false;
            this.Step0StatusTB.Visible = false;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(8, 401);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(105, 23);
            this.CancelBtn.TabIndex = 4;
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
            this.StepOneNextBtn.Location = new System.Drawing.Point(561, 401);
            this.StepOneNextBtn.Name = "StepOneNextBtn";
            this.StepOneNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepOneNextBtn.TabIndex = 2;
            this.StepOneNextBtn.Text = "Next Step ->";
            this.StepOneNextBtn.UseVisualStyleBackColor = true;
            this.StepOneNextBtn.Click += new System.EventHandler(this.StepOneNextBtn_Click);
            // 
            // PayloadsTab
            // 
            this.PayloadsTab.Controls.Add(this.label5);
            this.PayloadsTab.Controls.Add(this.label4);
            this.PayloadsTab.Controls.Add(this.NoOriginalParameterRB);
            this.PayloadsTab.Controls.Add(this.OriginalParameterAfterPayloadRB);
            this.PayloadsTab.Controls.Add(this.OriginalParameterBeforePayloadRB);
            this.PayloadsTab.Controls.Add(this.textBox7);
            this.PayloadsTab.Controls.Add(this.PayloadsFileLbl);
            this.PayloadsTab.Controls.Add(this.label3);
            this.PayloadsTab.Controls.Add(this.PayloadsListTB);
            this.PayloadsTab.Controls.Add(this.LoadPayloadsFileLL);
            this.PayloadsTab.Controls.Add(this.Step1StatusTB);
            this.PayloadsTab.Controls.Add(this.textBox4);
            this.PayloadsTab.Controls.Add(this.StepTwoPreviousBtn);
            this.PayloadsTab.Controls.Add(this.StepTwoNextBtn);
            this.PayloadsTab.Location = new System.Drawing.Point(4, 25);
            this.PayloadsTab.Margin = new System.Windows.Forms.Padding(0);
            this.PayloadsTab.Name = "PayloadsTab";
            this.PayloadsTab.Padding = new System.Windows.Forms.Padding(5);
            this.PayloadsTab.Size = new System.Drawing.Size(676, 432);
            this.PayloadsTab.TabIndex = 1;
            this.PayloadsTab.Text = "               Select Payloads               ";
            this.PayloadsTab.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 356);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(468, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "During testing sometimes the parameter\'s original value needs to be added along w" +
    "ith the payload.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 374);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(311, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Where do you want to add the tested parameter\'s original value?";
            // 
            // NoOriginalParameterRB
            // 
            this.NoOriginalParameterRB.AutoSize = true;
            this.NoOriginalParameterRB.Location = new System.Drawing.Point(600, 372);
            this.NoOriginalParameterRB.Name = "NoOriginalParameterRB";
            this.NoOriginalParameterRB.Size = new System.Drawing.Size(69, 17);
            this.NoOriginalParameterRB.TabIndex = 24;
            this.NoOriginalParameterRB.Text = "don\'t add";
            this.NoOriginalParameterRB.UseVisualStyleBackColor = true;
            // 
            // OriginalParameterAfterPayloadRB
            // 
            this.OriginalParameterAfterPayloadRB.AutoSize = true;
            this.OriginalParameterAfterPayloadRB.Location = new System.Drawing.Point(480, 372);
            this.OriginalParameterAfterPayloadRB.Name = "OriginalParameterAfterPayloadRB";
            this.OriginalParameterAfterPayloadRB.Size = new System.Drawing.Size(104, 17);
            this.OriginalParameterAfterPayloadRB.TabIndex = 23;
            this.OriginalParameterAfterPayloadRB.Text = "after the payload";
            this.OriginalParameterAfterPayloadRB.UseVisualStyleBackColor = true;
            // 
            // OriginalParameterBeforePayloadRB
            // 
            this.OriginalParameterBeforePayloadRB.AutoSize = true;
            this.OriginalParameterBeforePayloadRB.Checked = true;
            this.OriginalParameterBeforePayloadRB.Location = new System.Drawing.Point(352, 372);
            this.OriginalParameterBeforePayloadRB.Name = "OriginalParameterBeforePayloadRB";
            this.OriginalParameterBeforePayloadRB.Size = new System.Drawing.Size(113, 17);
            this.OriginalParameterBeforePayloadRB.TabIndex = 22;
            this.OriginalParameterBeforePayloadRB.TabStop = true;
            this.OriginalParameterBeforePayloadRB.Text = "before the payload";
            this.OriginalParameterBeforePayloadRB.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Control;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.ForeColor = System.Drawing.Color.Blue;
            this.textBox7.Location = new System.Drawing.Point(15, 287);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(637, 49);
            this.textBox7.TabIndex = 16;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "NOTE: \r\nThe payloads must be in Url Encoded form. For example null character must" +
    " be represented as %00, new line as %0a, space character as +, + character as %2" +
    "b and so on.";
            // 
            // PayloadsFileLbl
            // 
            this.PayloadsFileLbl.AutoSize = true;
            this.PayloadsFileLbl.Location = new System.Drawing.Point(129, 247);
            this.PayloadsFileLbl.Name = "PayloadsFileLbl";
            this.PayloadsFileLbl.Size = new System.Drawing.Size(376, 13);
            this.PayloadsFileLbl.TabIndex = 21;
            this.PayloadsFileLbl.Text = "                                                                                 " +
    "                                          ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(501, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "(If there are more than 5 payloads then they will be saved in a seperate file in " +
    "the Active Plugins directory)";
            // 
            // PayloadsListTB
            // 
            this.PayloadsListTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PayloadsListTB.Location = new System.Drawing.Point(8, 53);
            this.PayloadsListTB.Multiline = true;
            this.PayloadsListTB.Name = "PayloadsListTB";
            this.PayloadsListTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PayloadsListTB.Size = new System.Drawing.Size(660, 188);
            this.PayloadsListTB.TabIndex = 17;
            this.PayloadsListTB.WordWrap = false;
            // 
            // LoadPayloadsFileLL
            // 
            this.LoadPayloadsFileLL.AutoSize = true;
            this.LoadPayloadsFileLL.Location = new System.Drawing.Point(8, 247);
            this.LoadPayloadsFileLL.Name = "LoadPayloadsFileLL";
            this.LoadPayloadsFileLL.Size = new System.Drawing.Size(115, 13);
            this.LoadPayloadsFileLL.TabIndex = 16;
            this.LoadPayloadsFileLL.TabStop = true;
            this.LoadPayloadsFileLL.Text = "Load payloads from file";
            this.LoadPayloadsFileLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadPayloadsFileLL_LinkClicked);
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
            this.Step1StatusTB.Size = new System.Drawing.Size(438, 22);
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
            this.StepTwoPreviousBtn.Location = new System.Drawing.Point(8, 401);
            this.StepTwoPreviousBtn.Name = "StepTwoPreviousBtn";
            this.StepTwoPreviousBtn.Size = new System.Drawing.Size(105, 23);
            this.StepTwoPreviousBtn.TabIndex = 8;
            this.StepTwoPreviousBtn.Text = "<-Previous Step";
            this.StepTwoPreviousBtn.UseVisualStyleBackColor = true;
            this.StepTwoPreviousBtn.Click += new System.EventHandler(this.StepTwoPreviousBtn_Click);
            // 
            // StepTwoNextBtn
            // 
            this.StepTwoNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepTwoNextBtn.Location = new System.Drawing.Point(563, 401);
            this.StepTwoNextBtn.Name = "StepTwoNextBtn";
            this.StepTwoNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepTwoNextBtn.TabIndex = 7;
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
            this.LanguageTab.Size = new System.Drawing.Size(676, 432);
            this.LanguageTab.TabIndex = 2;
            this.LanguageTab.Text = "               Select Language               ";
            this.LanguageTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PluginLangRubyRB);
            this.groupBox2.Controls.Add(this.PluginLangPythonRB);
            this.groupBox2.Location = new System.Drawing.Point(7, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 110);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Which language should the plugin be created in?";
            // 
            // PluginLangRubyRB
            // 
            this.PluginLangRubyRB.AutoSize = true;
            this.PluginLangRubyRB.Location = new System.Drawing.Point(12, 79);
            this.PluginLangRubyRB.Name = "PluginLangRubyRB";
            this.PluginLangRubyRB.Size = new System.Drawing.Size(50, 17);
            this.PluginLangRubyRB.TabIndex = 15;
            this.PluginLangRubyRB.Text = "Ruby";
            this.PluginLangRubyRB.UseVisualStyleBackColor = true;
            // 
            // PluginLangPythonRB
            // 
            this.PluginLangPythonRB.AutoSize = true;
            this.PluginLangPythonRB.Checked = true;
            this.PluginLangPythonRB.Location = new System.Drawing.Point(12, 29);
            this.PluginLangPythonRB.Name = "PluginLangPythonRB";
            this.PluginLangPythonRB.Size = new System.Drawing.Size(58, 17);
            this.PluginLangPythonRB.TabIndex = 14;
            this.PluginLangPythonRB.TabStop = true;
            this.PluginLangPythonRB.Text = "Python";
            this.PluginLangPythonRB.UseVisualStyleBackColor = true;
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
            this.Step2StatusTB.Size = new System.Drawing.Size(410, 39);
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
            this.textBox5.Text = "\r\nActive Plugins can be written in Python or in Ruby. Choose you language of choi" +
    "ce and this Active Plugin will be created in that language.";
            // 
            // StepThreePreviousBtn
            // 
            this.StepThreePreviousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepThreePreviousBtn.Location = new System.Drawing.Point(8, 401);
            this.StepThreePreviousBtn.Name = "StepThreePreviousBtn";
            this.StepThreePreviousBtn.Size = new System.Drawing.Size(105, 23);
            this.StepThreePreviousBtn.TabIndex = 10;
            this.StepThreePreviousBtn.Text = "<-Previous Step";
            this.StepThreePreviousBtn.UseVisualStyleBackColor = true;
            this.StepThreePreviousBtn.Click += new System.EventHandler(this.StepThreePreviousBtn_Click);
            // 
            // StepThreeNextBtn
            // 
            this.StepThreeNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StepThreeNextBtn.Location = new System.Drawing.Point(563, 401);
            this.StepThreeNextBtn.Name = "StepThreeNextBtn";
            this.StepThreeNextBtn.Size = new System.Drawing.Size(105, 23);
            this.StepThreeNextBtn.TabIndex = 9;
            this.StepThreeNextBtn.Text = "Next Step ->";
            this.StepThreeNextBtn.UseVisualStyleBackColor = true;
            this.StepThreeNextBtn.Click += new System.EventHandler(this.StepThreeNextBtn_Click);
            // 
            // FinalTab
            // 
            this.FinalTab.Controls.Add(this.PluginFileTB);
            this.FinalTab.Controls.Add(this.label6);
            this.FinalTab.Controls.Add(this.textBox1);
            this.FinalTab.Controls.Add(this.FinalBtn);
            this.FinalTab.Location = new System.Drawing.Point(4, 25);
            this.FinalTab.Name = "FinalTab";
            this.FinalTab.Padding = new System.Windows.Forms.Padding(5);
            this.FinalTab.Size = new System.Drawing.Size(676, 432);
            this.FinalTab.TabIndex = 3;
            this.FinalTab.Text = "               Done               ";
            this.FinalTab.UseVisualStyleBackColor = true;
            // 
            // PluginFileTB
            // 
            this.PluginFileTB.Location = new System.Drawing.Point(75, 280);
            this.PluginFileTB.Name = "PluginFileTB";
            this.PluginFileTB.Size = new System.Drawing.Size(591, 20);
            this.PluginFileTB.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 283);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Plugin File:";
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
            this.FinalBtn.Location = new System.Drawing.Point(491, 389);
            this.FinalBtn.Name = "FinalBtn";
            this.FinalBtn.Size = new System.Drawing.Size(177, 35);
            this.FinalBtn.TabIndex = 16;
            this.FinalBtn.Text = "Close this Assistant";
            this.FinalBtn.UseVisualStyleBackColor = true;
            this.FinalBtn.Click += new System.EventHandler(this.FinalBtn_Click);
            // 
            // ActivePluginCreationAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.BaseTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(700, 500);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "ActivePluginCreationAssistant";
            this.Text = "ActivePlugin Creation Assistant";
            this.BaseTabs.ResumeLayout(false);
            this.NameTab.ResumeLayout(false);
            this.NameTab.PerformLayout();
            this.PayloadsTab.ResumeLayout(false);
            this.PayloadsTab.PerformLayout();
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
        internal System.Windows.Forms.TextBox Step0StatusTB;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button StepOneNextBtn;
        private System.Windows.Forms.TabPage PayloadsTab;
        internal System.Windows.Forms.TextBox Step1StatusTB;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button StepTwoPreviousBtn;
        private System.Windows.Forms.Button StepTwoNextBtn;
        private System.Windows.Forms.TabPage LanguageTab;
        internal System.Windows.Forms.TextBox Step2StatusTB;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button StepThreePreviousBtn;
        private System.Windows.Forms.Button StepThreeNextBtn;
        private System.Windows.Forms.TabPage FinalTab;
        private System.Windows.Forms.Button FinalBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PluginDescTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PluginNameTB;
        private System.Windows.Forms.OpenFileDialog PayloadFileOpener;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox PayloadsListTB;
        private System.Windows.Forms.LinkLabel LoadPayloadsFileLL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PayloadsFileLbl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton PluginLangRubyRB;
        private System.Windows.Forms.RadioButton PluginLangPythonRB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton NoOriginalParameterRB;
        private System.Windows.Forms.RadioButton OriginalParameterAfterPayloadRB;
        private System.Windows.Forms.RadioButton OriginalParameterBeforePayloadRB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PluginFileTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
    }
}