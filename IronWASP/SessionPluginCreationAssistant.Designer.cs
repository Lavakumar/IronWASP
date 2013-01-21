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
    partial class SessionPluginCreationAssistant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionPluginCreationAssistant));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AnswerTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BigAnswerTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SubmitAnswerBtn = new System.Windows.Forms.Button();
            this.QuestionRTB = new System.Windows.Forms.RichTextBox();
            this.StatusTB = new System.Windows.Forms.TextBox();
            this.BaseSplit = new System.Windows.Forms.SplitContainer();
            this.AnswerTabs = new System.Windows.Forms.TabControl();
            this.TextAnswerTab = new System.Windows.Forms.TabPage();
            this.RequestSourceAnswerTab = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.RequestSourceNameTB = new System.Windows.Forms.TextBox();
            this.RequestSourceAnswerMsgTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RequestSourceIdTB = new System.Windows.Forms.TextBox();
            this.RequestSourceAnswerSubmitBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.RequestSourceCombo = new System.Windows.Forms.ComboBox();
            this.ParameterAnswerTab = new System.Windows.Forms.TabPage();
            this.DeleteParameterAnswerEntryLL = new System.Windows.Forms.LinkLabel();
            this.EditParameterAnswerEntryLL = new System.Windows.Forms.LinkLabel();
            this.AddParameterAnswerEntryLL = new System.Windows.Forms.LinkLabel();
            this.UserHintPanel = new System.Windows.Forms.Panel();
            this.ParameterAskUserHintTB = new System.Windows.Forms.TextBox();
            this.ParameterFourLbl = new System.Windows.Forms.Label();
            this.HowToParseResponsePanel = new System.Windows.Forms.Panel();
            this.ParameterThreeLbl = new System.Windows.Forms.Label();
            this.ParseParameterFromHtmlRB = new System.Windows.Forms.RadioButton();
            this.ParseParameterFromRegexRB = new System.Windows.Forms.RadioButton();
            this.ParameterParseRegexTB = new System.Windows.Forms.TextBox();
            this.HowToUpdateParameterPanel = new System.Windows.Forms.Panel();
            this.ParameterSourceFromResponseRB = new System.Windows.Forms.RadioButton();
            this.ParameterSourceFromUserRB = new System.Windows.Forms.RadioButton();
            this.ParameterTwoLbl = new System.Windows.Forms.Label();
            this.ParametersDescLL = new System.Windows.Forms.LinkLabel();
            this.ParametersAnswerMsgTB = new System.Windows.Forms.TextBox();
            this.ParameterOneLbl = new System.Windows.Forms.Label();
            this.ParameterNameTB = new System.Windows.Forms.TextBox();
            this.ParameterNameTBLbl = new System.Windows.Forms.Label();
            this.ParameterTypeCombo = new System.Windows.Forms.ComboBox();
            this.ParameterTypeComboLbl = new System.Windows.Forms.Label();
            this.SubmitParameterAnswerBtn = new System.Windows.Forms.Button();
            this.ParametersAnswerGrid = new System.Windows.Forms.DataGridView();
            this.ParameterSectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateFromColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParseResponseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HintColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResponseSignatureTab = new System.Windows.Forms.TabPage();
            this.SignatureSubmitBtn = new System.Windows.Forms.Button();
            this.LocationSignatureKeywordTB = new System.Windows.Forms.TextBox();
            this.LocationSignatureTypeSelectCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.FullResponseSignatureSecondPanel = new System.Windows.Forms.Panel();
            this.BodySignatureKeywordTB = new System.Windows.Forms.TextBox();
            this.BodySignatureTypeSelectCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TitleSignatureKeywordTB = new System.Windows.Forms.TextBox();
            this.TitleSignatureTypeSelectCombo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.UseTitleSignatureCB = new System.Windows.Forms.CheckBox();
            this.UseBodySignatureCB = new System.Windows.Forms.CheckBox();
            this.UseLocationSignatureCB = new System.Windows.Forms.CheckBox();
            this.FullResponseSignatureFirstPanel = new System.Windows.Forms.Panel();
            this.LoggedOutResponseSignatureRB = new System.Windows.Forms.RadioButton();
            this.LoggedInResponseSignatureRB = new System.Windows.Forms.RadioButton();
            this.SignatureAnswerMsgTB = new System.Windows.Forms.TextBox();
            this.SignatureResponseCodeTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SelectOptionTab = new System.Windows.Forms.TabPage();
            this.SelectOptionAnswerMsgTB = new System.Windows.Forms.TextBox();
            this.SpecialOptionBtn = new System.Windows.Forms.Button();
            this.SelectedOptionSubmitBtn = new System.Windows.Forms.Button();
            this.OptionsGrid = new System.Windows.Forms.DataGridView();
            this.SelectClmn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OptionNameClmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowPseudoCodeTab = new System.Windows.Forms.TabPage();
            this.ShowPseudoCodeGoToMainMenuBtn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.ShowPseudoCodeTB = new System.Windows.Forms.TextBox();
            this.BaseSplit.Panel1.SuspendLayout();
            this.BaseSplit.Panel2.SuspendLayout();
            this.BaseSplit.SuspendLayout();
            this.AnswerTabs.SuspendLayout();
            this.TextAnswerTab.SuspendLayout();
            this.RequestSourceAnswerTab.SuspendLayout();
            this.ParameterAnswerTab.SuspendLayout();
            this.UserHintPanel.SuspendLayout();
            this.HowToParseResponsePanel.SuspendLayout();
            this.HowToUpdateParameterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParametersAnswerGrid)).BeginInit();
            this.ResponseSignatureTab.SuspendLayout();
            this.FullResponseSignatureSecondPanel.SuspendLayout();
            this.FullResponseSignatureFirstPanel.SuspendLayout();
            this.SelectOptionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsGrid)).BeginInit();
            this.ShowPseudoCodeTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnswerTB
            // 
            this.AnswerTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerTB.Location = new System.Drawing.Point(94, 106);
            this.AnswerTB.Name = "AnswerTB";
            this.AnswerTB.Size = new System.Drawing.Size(602, 20);
            this.AnswerTB.TabIndex = 0;
            this.AnswerTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AnswerTB_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "One Line Input:";
            // 
            // BigAnswerTB
            // 
            this.BigAnswerTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BigAnswerTB.Location = new System.Drawing.Point(94, 141);
            this.BigAnswerTB.Multiline = true;
            this.BigAnswerTB.Name = "BigAnswerTB";
            this.BigAnswerTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.BigAnswerTB.Size = new System.Drawing.Size(602, 106);
            this.BigAnswerTB.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Multi-line Input:";
            // 
            // SubmitAnswerBtn
            // 
            this.SubmitAnswerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SubmitAnswerBtn.Location = new System.Drawing.Point(702, 106);
            this.SubmitAnswerBtn.Name = "SubmitAnswerBtn";
            this.SubmitAnswerBtn.Size = new System.Drawing.Size(71, 141);
            this.SubmitAnswerBtn.TabIndex = 4;
            this.SubmitAnswerBtn.Text = "Submit";
            this.SubmitAnswerBtn.UseVisualStyleBackColor = true;
            this.SubmitAnswerBtn.Click += new System.EventHandler(this.SubmitAnswerBtn_Click);
            // 
            // QuestionRTB
            // 
            this.QuestionRTB.BackColor = System.Drawing.Color.White;
            this.QuestionRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QuestionRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuestionRTB.Location = new System.Drawing.Point(0, 0);
            this.QuestionRTB.Margin = new System.Windows.Forms.Padding(0);
            this.QuestionRTB.Name = "QuestionRTB";
            this.QuestionRTB.ReadOnly = true;
            this.QuestionRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.QuestionRTB.Size = new System.Drawing.Size(784, 281);
            this.QuestionRTB.TabIndex = 5;
            this.QuestionRTB.Text = "";
            // 
            // StatusTB
            // 
            this.StatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusTB.BackColor = System.Drawing.SystemColors.Control;
            this.StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusTB.ForeColor = System.Drawing.Color.Red;
            this.StatusTB.Location = new System.Drawing.Point(0, 0);
            this.StatusTB.Margin = new System.Windows.Forms.Padding(0);
            this.StatusTB.Multiline = true;
            this.StatusTB.Name = "StatusTB";
            this.StatusTB.ReadOnly = true;
            this.StatusTB.Size = new System.Drawing.Size(776, 45);
            this.StatusTB.TabIndex = 6;
            // 
            // BaseSplit
            // 
            this.BaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseSplit.Location = new System.Drawing.Point(0, 0);
            this.BaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.BaseSplit.Name = "BaseSplit";
            this.BaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // BaseSplit.Panel1
            // 
            this.BaseSplit.Panel1.Controls.Add(this.QuestionRTB);
            // 
            // BaseSplit.Panel2
            // 
            this.BaseSplit.Panel2.Controls.Add(this.AnswerTabs);
            this.BaseSplit.Size = new System.Drawing.Size(784, 562);
            this.BaseSplit.SplitterDistance = 281;
            this.BaseSplit.SplitterWidth = 2;
            this.BaseSplit.TabIndex = 7;
            // 
            // AnswerTabs
            // 
            this.AnswerTabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.AnswerTabs.Controls.Add(this.TextAnswerTab);
            this.AnswerTabs.Controls.Add(this.RequestSourceAnswerTab);
            this.AnswerTabs.Controls.Add(this.ParameterAnswerTab);
            this.AnswerTabs.Controls.Add(this.ResponseSignatureTab);
            this.AnswerTabs.Controls.Add(this.SelectOptionTab);
            this.AnswerTabs.Controls.Add(this.ShowPseudoCodeTab);
            this.AnswerTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnswerTabs.Location = new System.Drawing.Point(0, 0);
            this.AnswerTabs.Margin = new System.Windows.Forms.Padding(0);
            this.AnswerTabs.Multiline = true;
            this.AnswerTabs.Name = "AnswerTabs";
            this.AnswerTabs.Padding = new System.Drawing.Point(0, 0);
            this.AnswerTabs.SelectedIndex = 0;
            this.AnswerTabs.Size = new System.Drawing.Size(784, 279);
            this.AnswerTabs.TabIndex = 0;
            this.AnswerTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.AnswerTabs_Selecting);
            // 
            // TextAnswerTab
            // 
            this.TextAnswerTab.Controls.Add(this.StatusTB);
            this.TextAnswerTab.Controls.Add(this.SubmitAnswerBtn);
            this.TextAnswerTab.Controls.Add(this.AnswerTB);
            this.TextAnswerTab.Controls.Add(this.label2);
            this.TextAnswerTab.Controls.Add(this.label1);
            this.TextAnswerTab.Controls.Add(this.BigAnswerTB);
            this.TextAnswerTab.Location = new System.Drawing.Point(4, 25);
            this.TextAnswerTab.Margin = new System.Windows.Forms.Padding(0);
            this.TextAnswerTab.Name = "TextAnswerTab";
            this.TextAnswerTab.Size = new System.Drawing.Size(776, 250);
            this.TextAnswerTab.TabIndex = 0;
            this.TextAnswerTab.Text = "Enter Text";
            this.TextAnswerTab.UseVisualStyleBackColor = true;
            // 
            // RequestSourceAnswerTab
            // 
            this.RequestSourceAnswerTab.Controls.Add(this.label11);
            this.RequestSourceAnswerTab.Controls.Add(this.label10);
            this.RequestSourceAnswerTab.Controls.Add(this.label9);
            this.RequestSourceAnswerTab.Controls.Add(this.RequestSourceNameTB);
            this.RequestSourceAnswerTab.Controls.Add(this.RequestSourceAnswerMsgTB);
            this.RequestSourceAnswerTab.Controls.Add(this.label4);
            this.RequestSourceAnswerTab.Controls.Add(this.RequestSourceIdTB);
            this.RequestSourceAnswerTab.Controls.Add(this.RequestSourceAnswerSubmitBtn);
            this.RequestSourceAnswerTab.Controls.Add(this.label3);
            this.RequestSourceAnswerTab.Controls.Add(this.RequestSourceCombo);
            this.RequestSourceAnswerTab.Location = new System.Drawing.Point(4, 25);
            this.RequestSourceAnswerTab.Name = "RequestSourceAnswerTab";
            this.RequestSourceAnswerTab.Size = new System.Drawing.Size(776, 250);
            this.RequestSourceAnswerTab.TabIndex = 2;
            this.RequestSourceAnswerTab.Text = "Specify Request Source";
            this.RequestSourceAnswerTab.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(289, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "request";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(537, 26);
            this.label10.TabIndex = 23;
            this.label10.Text = resources.GetString("label10.Text");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Request Name:";
            // 
            // RequestSourceNameTB
            // 
            this.RequestSourceNameTB.BackColor = System.Drawing.Color.White;
            this.RequestSourceNameTB.Location = new System.Drawing.Point(118, 98);
            this.RequestSourceNameTB.Name = "RequestSourceNameTB";
            this.RequestSourceNameTB.Size = new System.Drawing.Size(170, 20);
            this.RequestSourceNameTB.TabIndex = 21;
            this.RequestSourceNameTB.TextChanged += new System.EventHandler(this.RequestSourceNameTB_TextChanged);
            // 
            // RequestSourceAnswerMsgTB
            // 
            this.RequestSourceAnswerMsgTB.BackColor = System.Drawing.SystemColors.Control;
            this.RequestSourceAnswerMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RequestSourceAnswerMsgTB.ForeColor = System.Drawing.Color.Red;
            this.RequestSourceAnswerMsgTB.Location = new System.Drawing.Point(3, 4);
            this.RequestSourceAnswerMsgTB.Name = "RequestSourceAnswerMsgTB";
            this.RequestSourceAnswerMsgTB.ReadOnly = true;
            this.RequestSourceAnswerMsgTB.Size = new System.Drawing.Size(770, 13);
            this.RequestSourceAnswerMsgTB.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Log ID:";
            // 
            // RequestSourceIdTB
            // 
            this.RequestSourceIdTB.BackColor = System.Drawing.Color.White;
            this.RequestSourceIdTB.Location = new System.Drawing.Point(119, 72);
            this.RequestSourceIdTB.Name = "RequestSourceIdTB";
            this.RequestSourceIdTB.Size = new System.Drawing.Size(80, 20);
            this.RequestSourceIdTB.TabIndex = 3;
            this.RequestSourceIdTB.TextChanged += new System.EventHandler(this.RequestSourceIdTB_TextChanged);
            // 
            // RequestSourceAnswerSubmitBtn
            // 
            this.RequestSourceAnswerSubmitBtn.Location = new System.Drawing.Point(376, 75);
            this.RequestSourceAnswerSubmitBtn.Name = "RequestSourceAnswerSubmitBtn";
            this.RequestSourceAnswerSubmitBtn.Size = new System.Drawing.Size(200, 46);
            this.RequestSourceAnswerSubmitBtn.TabIndex = 2;
            this.RequestSourceAnswerSubmitBtn.Text = "Done";
            this.RequestSourceAnswerSubmitBtn.UseVisualStyleBackColor = true;
            this.RequestSourceAnswerSubmitBtn.Click += new System.EventHandler(this.RequestSourceAnswerSubmitBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Request Log Source:";
            // 
            // RequestSourceCombo
            // 
            this.RequestSourceCombo.FormattingEnabled = true;
            this.RequestSourceCombo.Items.AddRange(new object[] {
            "Proxy",
            "Test",
            "Shell",
            "Scan",
            "Probe"});
            this.RequestSourceCombo.Location = new System.Drawing.Point(118, 45);
            this.RequestSourceCombo.Name = "RequestSourceCombo";
            this.RequestSourceCombo.Size = new System.Drawing.Size(170, 21);
            this.RequestSourceCombo.TabIndex = 0;
            this.RequestSourceCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RequestSourceCombo_KeyPress);
            // 
            // ParameterAnswerTab
            // 
            this.ParameterAnswerTab.Controls.Add(this.DeleteParameterAnswerEntryLL);
            this.ParameterAnswerTab.Controls.Add(this.EditParameterAnswerEntryLL);
            this.ParameterAnswerTab.Controls.Add(this.AddParameterAnswerEntryLL);
            this.ParameterAnswerTab.Controls.Add(this.UserHintPanel);
            this.ParameterAnswerTab.Controls.Add(this.HowToParseResponsePanel);
            this.ParameterAnswerTab.Controls.Add(this.HowToUpdateParameterPanel);
            this.ParameterAnswerTab.Controls.Add(this.ParametersDescLL);
            this.ParameterAnswerTab.Controls.Add(this.ParametersAnswerMsgTB);
            this.ParameterAnswerTab.Controls.Add(this.ParameterOneLbl);
            this.ParameterAnswerTab.Controls.Add(this.ParameterNameTB);
            this.ParameterAnswerTab.Controls.Add(this.ParameterNameTBLbl);
            this.ParameterAnswerTab.Controls.Add(this.ParameterTypeCombo);
            this.ParameterAnswerTab.Controls.Add(this.ParameterTypeComboLbl);
            this.ParameterAnswerTab.Controls.Add(this.SubmitParameterAnswerBtn);
            this.ParameterAnswerTab.Controls.Add(this.ParametersAnswerGrid);
            this.ParameterAnswerTab.Location = new System.Drawing.Point(4, 25);
            this.ParameterAnswerTab.Name = "ParameterAnswerTab";
            this.ParameterAnswerTab.Size = new System.Drawing.Size(776, 250);
            this.ParameterAnswerTab.TabIndex = 3;
            this.ParameterAnswerTab.Text = "Specify Parameters";
            this.ParameterAnswerTab.UseVisualStyleBackColor = true;
            // 
            // DeleteParameterAnswerEntryLL
            // 
            this.DeleteParameterAnswerEntryLL.AutoSize = true;
            this.DeleteParameterAnswerEntryLL.Location = new System.Drawing.Point(691, 193);
            this.DeleteParameterAnswerEntryLL.Name = "DeleteParameterAnswerEntryLL";
            this.DeleteParameterAnswerEntryLL.Size = new System.Drawing.Size(65, 13);
            this.DeleteParameterAnswerEntryLL.TabIndex = 26;
            this.DeleteParameterAnswerEntryLL.TabStop = true;
            this.DeleteParameterAnswerEntryLL.Text = "Delete Entry";
            this.DeleteParameterAnswerEntryLL.Visible = false;
            this.DeleteParameterAnswerEntryLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteParameterAnswerEntryLL_LinkClicked);
            // 
            // EditParameterAnswerEntryLL
            // 
            this.EditParameterAnswerEntryLL.AutoSize = true;
            this.EditParameterAnswerEntryLL.Location = new System.Drawing.Point(691, 174);
            this.EditParameterAnswerEntryLL.Name = "EditParameterAnswerEntryLL";
            this.EditParameterAnswerEntryLL.Size = new System.Drawing.Size(52, 13);
            this.EditParameterAnswerEntryLL.TabIndex = 25;
            this.EditParameterAnswerEntryLL.TabStop = true;
            this.EditParameterAnswerEntryLL.Text = "Edit Entry";
            this.EditParameterAnswerEntryLL.Visible = false;
            this.EditParameterAnswerEntryLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EditParameterAnswerEntryLL_LinkClicked);
            // 
            // AddParameterAnswerEntryLL
            // 
            this.AddParameterAnswerEntryLL.AutoSize = true;
            this.AddParameterAnswerEntryLL.Location = new System.Drawing.Point(691, 120);
            this.AddParameterAnswerEntryLL.Name = "AddParameterAnswerEntryLL";
            this.AddParameterAnswerEntryLL.Size = new System.Drawing.Size(53, 13);
            this.AddParameterAnswerEntryLL.TabIndex = 24;
            this.AddParameterAnswerEntryLL.TabStop = true;
            this.AddParameterAnswerEntryLL.Text = "Add Entry";
            this.AddParameterAnswerEntryLL.Visible = false;
            this.AddParameterAnswerEntryLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddParameterAnswerEntryLL_LinkClicked);
            // 
            // UserHintPanel
            // 
            this.UserHintPanel.Controls.Add(this.ParameterAskUserHintTB);
            this.UserHintPanel.Controls.Add(this.ParameterFourLbl);
            this.UserHintPanel.Location = new System.Drawing.Point(8, 132);
            this.UserHintPanel.Name = "UserHintPanel";
            this.UserHintPanel.Size = new System.Drawing.Size(672, 30);
            this.UserHintPanel.TabIndex = 23;
            // 
            // ParameterAskUserHintTB
            // 
            this.ParameterAskUserHintTB.Location = new System.Drawing.Point(137, 4);
            this.ParameterAskUserHintTB.Name = "ParameterAskUserHintTB";
            this.ParameterAskUserHintTB.Size = new System.Drawing.Size(532, 20);
            this.ParameterAskUserHintTB.TabIndex = 17;
            // 
            // ParameterFourLbl
            // 
            this.ParameterFourLbl.AutoSize = true;
            this.ParameterFourLbl.Location = new System.Drawing.Point(3, 8);
            this.ParameterFourLbl.Name = "ParameterFourLbl";
            this.ParameterFourLbl.Size = new System.Drawing.Size(128, 13);
            this.ParameterFourLbl.TabIndex = 16;
            this.ParameterFourLbl.Text = "Hint to user in the prompt:";
            // 
            // HowToParseResponsePanel
            // 
            this.HowToParseResponsePanel.Controls.Add(this.ParameterThreeLbl);
            this.HowToParseResponsePanel.Controls.Add(this.ParseParameterFromHtmlRB);
            this.HowToParseResponsePanel.Controls.Add(this.ParseParameterFromRegexRB);
            this.HowToParseResponsePanel.Controls.Add(this.ParameterParseRegexTB);
            this.HowToParseResponsePanel.Location = new System.Drawing.Point(8, 94);
            this.HowToParseResponsePanel.Name = "HowToParseResponsePanel";
            this.HowToParseResponsePanel.Size = new System.Drawing.Size(672, 32);
            this.HowToParseResponsePanel.TabIndex = 22;
            // 
            // ParameterThreeLbl
            // 
            this.ParameterThreeLbl.AutoSize = true;
            this.ParameterThreeLbl.Location = new System.Drawing.Point(3, 9);
            this.ParameterThreeLbl.Name = "ParameterThreeLbl";
            this.ParameterThreeLbl.Size = new System.Drawing.Size(200, 13);
            this.ParameterThreeLbl.TabIndex = 12;
            this.ParameterThreeLbl.Text = "How to Parse new value from Response:";
            // 
            // ParseParameterFromHtmlRB
            // 
            this.ParseParameterFromHtmlRB.AutoSize = true;
            this.ParseParameterFromHtmlRB.Checked = true;
            this.ParseParameterFromHtmlRB.Location = new System.Drawing.Point(210, 9);
            this.ParseParameterFromHtmlRB.Name = "ParseParameterFromHtmlRB";
            this.ParseParameterFromHtmlRB.Size = new System.Drawing.Size(137, 17);
            this.ParseParameterFromHtmlRB.TabIndex = 13;
            this.ParseParameterFromHtmlRB.TabStop = true;
            this.ParseParameterFromHtmlRB.Text = "From HTML Form Fields";
            this.ParseParameterFromHtmlRB.UseVisualStyleBackColor = true;
            // 
            // ParseParameterFromRegexRB
            // 
            this.ParseParameterFromRegexRB.AutoSize = true;
            this.ParseParameterFromRegexRB.Location = new System.Drawing.Point(351, 9);
            this.ParseParameterFromRegexRB.Name = "ParseParameterFromRegexRB";
            this.ParseParameterFromRegexRB.Size = new System.Drawing.Size(127, 17);
            this.ParseParameterFromRegexRB.TabIndex = 14;
            this.ParseParameterFromRegexRB.Text = "Parse with this Regex";
            this.ParseParameterFromRegexRB.UseVisualStyleBackColor = true;
            this.ParseParameterFromRegexRB.CheckedChanged += new System.EventHandler(this.ParseParameterFromRegexRB_CheckedChanged);
            // 
            // ParameterParseRegexTB
            // 
            this.ParameterParseRegexTB.BackColor = System.Drawing.Color.White;
            this.ParameterParseRegexTB.Enabled = false;
            this.ParameterParseRegexTB.Location = new System.Drawing.Point(481, 8);
            this.ParameterParseRegexTB.Name = "ParameterParseRegexTB";
            this.ParameterParseRegexTB.Size = new System.Drawing.Size(188, 20);
            this.ParameterParseRegexTB.TabIndex = 15;
            this.ParameterParseRegexTB.TextChanged += new System.EventHandler(this.ParameterParseRegexTB_TextChanged);
            // 
            // HowToUpdateParameterPanel
            // 
            this.HowToUpdateParameterPanel.Controls.Add(this.ParameterSourceFromResponseRB);
            this.HowToUpdateParameterPanel.Controls.Add(this.ParameterSourceFromUserRB);
            this.HowToUpdateParameterPanel.Controls.Add(this.ParameterTwoLbl);
            this.HowToUpdateParameterPanel.Location = new System.Drawing.Point(8, 64);
            this.HowToUpdateParameterPanel.Name = "HowToUpdateParameterPanel";
            this.HowToUpdateParameterPanel.Size = new System.Drawing.Size(361, 24);
            this.HowToUpdateParameterPanel.TabIndex = 21;
            // 
            // ParameterSourceFromResponseRB
            // 
            this.ParameterSourceFromResponseRB.AutoSize = true;
            this.ParameterSourceFromResponseRB.Location = new System.Drawing.Point(174, 4);
            this.ParameterSourceFromResponseRB.Name = "ParameterSourceFromResponseRB";
            this.ParameterSourceFromResponseRB.Size = new System.Drawing.Size(99, 17);
            this.ParameterSourceFromResponseRB.TabIndex = 10;
            this.ParameterSourceFromResponseRB.TabStop = true;
            this.ParameterSourceFromResponseRB.Text = "From Response";
            this.ParameterSourceFromResponseRB.UseVisualStyleBackColor = true;
            this.ParameterSourceFromResponseRB.CheckedChanged += new System.EventHandler(this.ParameterSourceFromResponseRB_CheckedChanged);
            // 
            // ParameterSourceFromUserRB
            // 
            this.ParameterSourceFromUserRB.AutoSize = true;
            this.ParameterSourceFromUserRB.Location = new System.Drawing.Point(281, 4);
            this.ParameterSourceFromUserRB.Name = "ParameterSourceFromUserRB";
            this.ParameterSourceFromUserRB.Size = new System.Drawing.Size(68, 17);
            this.ParameterSourceFromUserRB.TabIndex = 11;
            this.ParameterSourceFromUserRB.TabStop = true;
            this.ParameterSourceFromUserRB.Text = "Ask User";
            this.ParameterSourceFromUserRB.UseVisualStyleBackColor = true;
            this.ParameterSourceFromUserRB.CheckedChanged += new System.EventHandler(this.ParameterSourceFromUserRB_CheckedChanged);
            // 
            // ParameterTwoLbl
            // 
            this.ParameterTwoLbl.AutoSize = true;
            this.ParameterTwoLbl.Location = new System.Drawing.Point(7, 6);
            this.ParameterTwoLbl.Name = "ParameterTwoLbl";
            this.ParameterTwoLbl.Size = new System.Drawing.Size(152, 13);
            this.ParameterTwoLbl.TabIndex = 9;
            this.ParameterTwoLbl.Text = "How to Update this Parameter:";
            // 
            // ParametersDescLL
            // 
            this.ParametersDescLL.AutoSize = true;
            this.ParametersDescLL.Location = new System.Drawing.Point(643, 41);
            this.ParametersDescLL.Name = "ParametersDescLL";
            this.ParametersDescLL.Size = new System.Drawing.Size(33, 13);
            this.ParametersDescLL.TabIndex = 20;
            this.ParametersDescLL.TabStop = true;
            this.ParametersDescLL.Text = "Done";
            this.ParametersDescLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ParametersDescLL_LinkClicked);
            // 
            // ParametersAnswerMsgTB
            // 
            this.ParametersAnswerMsgTB.BackColor = System.Drawing.SystemColors.Control;
            this.ParametersAnswerMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ParametersAnswerMsgTB.ForeColor = System.Drawing.Color.Red;
            this.ParametersAnswerMsgTB.Location = new System.Drawing.Point(3, 4);
            this.ParametersAnswerMsgTB.Name = "ParametersAnswerMsgTB";
            this.ParametersAnswerMsgTB.ReadOnly = true;
            this.ParametersAnswerMsgTB.Size = new System.Drawing.Size(770, 13);
            this.ParametersAnswerMsgTB.TabIndex = 19;
            // 
            // ParameterOneLbl
            // 
            this.ParameterOneLbl.AutoSize = true;
            this.ParameterOneLbl.Location = new System.Drawing.Point(8, 40);
            this.ParameterOneLbl.Name = "ParameterOneLbl";
            this.ParameterOneLbl.Size = new System.Drawing.Size(108, 13);
            this.ParameterOneLbl.TabIndex = 8;
            this.ParameterOneLbl.Text = "Parameter to Update:";
            // 
            // ParameterNameTB
            // 
            this.ParameterNameTB.BackColor = System.Drawing.Color.White;
            this.ParameterNameTB.Location = new System.Drawing.Point(444, 38);
            this.ParameterNameTB.Name = "ParameterNameTB";
            this.ParameterNameTB.Size = new System.Drawing.Size(193, 20);
            this.ParameterNameTB.TabIndex = 7;
            this.ParameterNameTB.TextChanged += new System.EventHandler(this.ParameterNameTB_TextChanged);
            // 
            // ParameterNameTBLbl
            // 
            this.ParameterNameTBLbl.AutoSize = true;
            this.ParameterNameTBLbl.Location = new System.Drawing.Point(349, 41);
            this.ParameterNameTBLbl.Name = "ParameterNameTBLbl";
            this.ParameterNameTBLbl.Size = new System.Drawing.Size(89, 13);
            this.ParameterNameTBLbl.TabIndex = 6;
            this.ParameterNameTBLbl.Text = "Parameter Name:";
            // 
            // ParameterTypeCombo
            // 
            this.ParameterTypeCombo.FormattingEnabled = true;
            this.ParameterTypeCombo.Items.AddRange(new object[] {
            "UrlPathPart",
            "Query",
            "Body",
            "Cookie",
            "Header"});
            this.ParameterTypeCombo.Location = new System.Drawing.Point(213, 37);
            this.ParameterTypeCombo.Name = "ParameterTypeCombo";
            this.ParameterTypeCombo.Size = new System.Drawing.Size(121, 21);
            this.ParameterTypeCombo.TabIndex = 5;
            this.ParameterTypeCombo.SelectedIndexChanged += new System.EventHandler(this.ParameterTypeCombo_SelectedIndexChanged);
            this.ParameterTypeCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ParameterTypeCombo_KeyPress);
            // 
            // ParameterTypeComboLbl
            // 
            this.ParameterTypeComboLbl.AutoSize = true;
            this.ParameterTypeComboLbl.Location = new System.Drawing.Point(122, 40);
            this.ParameterTypeComboLbl.Name = "ParameterTypeComboLbl";
            this.ParameterTypeComboLbl.Size = new System.Drawing.Size(85, 13);
            this.ParameterTypeComboLbl.TabIndex = 4;
            this.ParameterTypeComboLbl.Text = "Parameter Type:";
            // 
            // SubmitParameterAnswerBtn
            // 
            this.SubmitParameterAnswerBtn.Location = new System.Drawing.Point(682, 216);
            this.SubmitParameterAnswerBtn.Name = "SubmitParameterAnswerBtn";
            this.SubmitParameterAnswerBtn.Size = new System.Drawing.Size(91, 33);
            this.SubmitParameterAnswerBtn.TabIndex = 3;
            this.SubmitParameterAnswerBtn.Text = "Submit Answer";
            this.SubmitParameterAnswerBtn.UseVisualStyleBackColor = true;
            this.SubmitParameterAnswerBtn.Click += new System.EventHandler(this.SubmitParameterAnswerBtn_Click);
            // 
            // ParametersAnswerGrid
            // 
            this.ParametersAnswerGrid.AllowUserToAddRows = false;
            this.ParametersAnswerGrid.AllowUserToDeleteRows = false;
            this.ParametersAnswerGrid.AllowUserToResizeRows = false;
            this.ParametersAnswerGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ParametersAnswerGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ParametersAnswerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ParametersAnswerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterSectionColumn,
            this.ParameterNameColumn,
            this.UpdateFromColumn,
            this.ParseResponseColumn,
            this.RegexColumn,
            this.HintColumn});
            this.ParametersAnswerGrid.GridColor = System.Drawing.Color.White;
            this.ParametersAnswerGrid.Location = new System.Drawing.Point(5, 165);
            this.ParametersAnswerGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ParametersAnswerGrid.MultiSelect = false;
            this.ParametersAnswerGrid.Name = "ParametersAnswerGrid";
            this.ParametersAnswerGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ParametersAnswerGrid.RowHeadersVisible = false;
            this.ParametersAnswerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ParametersAnswerGrid.Size = new System.Drawing.Size(674, 84);
            this.ParametersAnswerGrid.TabIndex = 0;
            this.ParametersAnswerGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ParametersAnswerGrid_CellClick);
            this.ParametersAnswerGrid.SelectionChanged += new System.EventHandler(this.ParametersAnswerGrid_SelectionChanged);
            // 
            // ParameterSectionColumn
            // 
            this.ParameterSectionColumn.HeaderText = "Parameter Section";
            this.ParameterSectionColumn.Name = "ParameterSectionColumn";
            this.ParameterSectionColumn.ReadOnly = true;
            this.ParameterSectionColumn.Width = 120;
            // 
            // ParameterNameColumn
            // 
            this.ParameterNameColumn.HeaderText = "Parameter Name";
            this.ParameterNameColumn.Name = "ParameterNameColumn";
            this.ParameterNameColumn.ReadOnly = true;
            this.ParameterNameColumn.Width = 120;
            // 
            // UpdateFromColumn
            // 
            this.UpdateFromColumn.HeaderText = "Update From";
            this.UpdateFromColumn.Name = "UpdateFromColumn";
            this.UpdateFromColumn.ReadOnly = true;
            // 
            // ParseResponseColumn
            // 
            this.ParseResponseColumn.HeaderText = "Response Read Mode";
            this.ParseResponseColumn.Name = "ParseResponseColumn";
            this.ParseResponseColumn.ReadOnly = true;
            this.ParseResponseColumn.Width = 140;
            // 
            // RegexColumn
            // 
            this.RegexColumn.HeaderText = "Regex";
            this.RegexColumn.Name = "RegexColumn";
            this.RegexColumn.ReadOnly = true;
            // 
            // HintColumn
            // 
            this.HintColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HintColumn.HeaderText = "Hint";
            this.HintColumn.Name = "HintColumn";
            this.HintColumn.ReadOnly = true;
            // 
            // ResponseSignatureTab
            // 
            this.ResponseSignatureTab.Controls.Add(this.SignatureSubmitBtn);
            this.ResponseSignatureTab.Controls.Add(this.LocationSignatureKeywordTB);
            this.ResponseSignatureTab.Controls.Add(this.LocationSignatureTypeSelectCombo);
            this.ResponseSignatureTab.Controls.Add(this.label6);
            this.ResponseSignatureTab.Controls.Add(this.FullResponseSignatureSecondPanel);
            this.ResponseSignatureTab.Controls.Add(this.UseLocationSignatureCB);
            this.ResponseSignatureTab.Controls.Add(this.FullResponseSignatureFirstPanel);
            this.ResponseSignatureTab.Controls.Add(this.SignatureAnswerMsgTB);
            this.ResponseSignatureTab.Controls.Add(this.SignatureResponseCodeTB);
            this.ResponseSignatureTab.Controls.Add(this.label5);
            this.ResponseSignatureTab.Location = new System.Drawing.Point(4, 25);
            this.ResponseSignatureTab.Name = "ResponseSignatureTab";
            this.ResponseSignatureTab.Padding = new System.Windows.Forms.Padding(3);
            this.ResponseSignatureTab.Size = new System.Drawing.Size(776, 250);
            this.ResponseSignatureTab.TabIndex = 1;
            this.ResponseSignatureTab.Text = "Define Response Signature";
            this.ResponseSignatureTab.UseVisualStyleBackColor = true;
            // 
            // SignatureSubmitBtn
            // 
            this.SignatureSubmitBtn.Location = new System.Drawing.Point(653, 37);
            this.SignatureSubmitBtn.Name = "SignatureSubmitBtn";
            this.SignatureSubmitBtn.Size = new System.Drawing.Size(115, 23);
            this.SignatureSubmitBtn.TabIndex = 11;
            this.SignatureSubmitBtn.Text = "Submit";
            this.SignatureSubmitBtn.UseVisualStyleBackColor = true;
            this.SignatureSubmitBtn.Click += new System.EventHandler(this.SignatureSubmitBtn_Click);
            // 
            // LocationSignatureKeywordTB
            // 
            this.LocationSignatureKeywordTB.BackColor = System.Drawing.Color.White;
            this.LocationSignatureKeywordTB.Location = new System.Drawing.Point(495, 91);
            this.LocationSignatureKeywordTB.Name = "LocationSignatureKeywordTB";
            this.LocationSignatureKeywordTB.Size = new System.Drawing.Size(263, 20);
            this.LocationSignatureKeywordTB.TabIndex = 10;
            this.LocationSignatureKeywordTB.TextChanged += new System.EventHandler(this.LocationSignatureKeywordTB_TextChanged);
            // 
            // LocationSignatureTypeSelectCombo
            // 
            this.LocationSignatureTypeSelectCombo.FormattingEnabled = true;
            this.LocationSignatureTypeSelectCombo.Items.AddRange(new object[] {
            "starts with",
            "ends with",
            "contains",
            "does not contain",
            "matches regex"});
            this.LocationSignatureTypeSelectCombo.Location = new System.Drawing.Point(359, 91);
            this.LocationSignatureTypeSelectCombo.Name = "LocationSignatureTypeSelectCombo";
            this.LocationSignatureTypeSelectCombo.Size = new System.Drawing.Size(121, 21);
            this.LocationSignatureTypeSelectCombo.TabIndex = 9;
            this.LocationSignatureTypeSelectCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LocationSignatureTypeSelectCombo_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(267, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Location Header";
            // 
            // FullResponseSignatureSecondPanel
            // 
            this.FullResponseSignatureSecondPanel.Controls.Add(this.BodySignatureKeywordTB);
            this.FullResponseSignatureSecondPanel.Controls.Add(this.BodySignatureTypeSelectCombo);
            this.FullResponseSignatureSecondPanel.Controls.Add(this.label8);
            this.FullResponseSignatureSecondPanel.Controls.Add(this.TitleSignatureKeywordTB);
            this.FullResponseSignatureSecondPanel.Controls.Add(this.TitleSignatureTypeSelectCombo);
            this.FullResponseSignatureSecondPanel.Controls.Add(this.label7);
            this.FullResponseSignatureSecondPanel.Controls.Add(this.UseTitleSignatureCB);
            this.FullResponseSignatureSecondPanel.Controls.Add(this.UseBodySignatureCB);
            this.FullResponseSignatureSecondPanel.Location = new System.Drawing.Point(8, 113);
            this.FullResponseSignatureSecondPanel.Name = "FullResponseSignatureSecondPanel";
            this.FullResponseSignatureSecondPanel.Size = new System.Drawing.Size(760, 79);
            this.FullResponseSignatureSecondPanel.TabIndex = 7;
            // 
            // BodySignatureKeywordTB
            // 
            this.BodySignatureKeywordTB.BackColor = System.Drawing.Color.White;
            this.BodySignatureKeywordTB.Location = new System.Drawing.Point(437, 43);
            this.BodySignatureKeywordTB.Name = "BodySignatureKeywordTB";
            this.BodySignatureKeywordTB.Size = new System.Drawing.Size(313, 20);
            this.BodySignatureKeywordTB.TabIndex = 16;
            this.BodySignatureKeywordTB.TextChanged += new System.EventHandler(this.BodySignatureKeywordTB_TextChanged);
            // 
            // BodySignatureTypeSelectCombo
            // 
            this.BodySignatureTypeSelectCombo.FormattingEnabled = true;
            this.BodySignatureTypeSelectCombo.Items.AddRange(new object[] {
            "starts with",
            "ends with",
            "contains",
            "does not contain",
            "matches regex"});
            this.BodySignatureTypeSelectCombo.Location = new System.Drawing.Point(301, 43);
            this.BodySignatureTypeSelectCombo.Name = "BodySignatureTypeSelectCombo";
            this.BodySignatureTypeSelectCombo.Size = new System.Drawing.Size(121, 21);
            this.BodySignatureTypeSelectCombo.TabIndex = 15;
            this.BodySignatureTypeSelectCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BodySignatureTypeSelectCombo_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Response Body";
            // 
            // TitleSignatureKeywordTB
            // 
            this.TitleSignatureKeywordTB.BackColor = System.Drawing.Color.White;
            this.TitleSignatureKeywordTB.Location = new System.Drawing.Point(438, 10);
            this.TitleSignatureKeywordTB.Name = "TitleSignatureKeywordTB";
            this.TitleSignatureKeywordTB.Size = new System.Drawing.Size(313, 20);
            this.TitleSignatureKeywordTB.TabIndex = 13;
            this.TitleSignatureKeywordTB.TextChanged += new System.EventHandler(this.TitleSignatureKeywordTB_TextChanged);
            // 
            // TitleSignatureTypeSelectCombo
            // 
            this.TitleSignatureTypeSelectCombo.FormattingEnabled = true;
            this.TitleSignatureTypeSelectCombo.Items.AddRange(new object[] {
            "starts with",
            "ends with",
            "contains",
            "does not contain",
            "matches regex"});
            this.TitleSignatureTypeSelectCombo.Location = new System.Drawing.Point(298, 10);
            this.TitleSignatureTypeSelectCombo.Name = "TitleSignatureTypeSelectCombo";
            this.TitleSignatureTypeSelectCombo.Size = new System.Drawing.Size(121, 21);
            this.TitleSignatureTypeSelectCombo.TabIndex = 12;
            this.TitleSignatureTypeSelectCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TitleSignatureTypeSelectCombo_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(206, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Response Title";
            // 
            // UseTitleSignatureCB
            // 
            this.UseTitleSignatureCB.AutoSize = true;
            this.UseTitleSignatureCB.Location = new System.Drawing.Point(13, 14);
            this.UseTitleSignatureCB.Name = "UseTitleSignatureCB";
            this.UseTitleSignatureCB.Size = new System.Drawing.Size(170, 17);
            this.UseTitleSignatureCB.TabIndex = 5;
            this.UseTitleSignatureCB.Text = "Use Response Title Signature:";
            this.UseTitleSignatureCB.UseVisualStyleBackColor = true;
            this.UseTitleSignatureCB.CheckedChanged += new System.EventHandler(this.UseTitleSignatureCB_CheckedChanged);
            // 
            // UseBodySignatureCB
            // 
            this.UseBodySignatureCB.AutoSize = true;
            this.UseBodySignatureCB.Location = new System.Drawing.Point(13, 47);
            this.UseBodySignatureCB.Name = "UseBodySignatureCB";
            this.UseBodySignatureCB.Size = new System.Drawing.Size(174, 17);
            this.UseBodySignatureCB.TabIndex = 6;
            this.UseBodySignatureCB.Text = "Use Response Body Signature:";
            this.UseBodySignatureCB.UseVisualStyleBackColor = true;
            this.UseBodySignatureCB.CheckedChanged += new System.EventHandler(this.UseBodySignatureCB_CheckedChanged);
            // 
            // UseLocationSignatureCB
            // 
            this.UseLocationSignatureCB.AutoSize = true;
            this.UseLocationSignatureCB.Location = new System.Drawing.Point(22, 95);
            this.UseLocationSignatureCB.Name = "UseLocationSignatureCB";
            this.UseLocationSignatureCB.Size = new System.Drawing.Size(221, 17);
            this.UseLocationSignatureCB.TabIndex = 4;
            this.UseLocationSignatureCB.Text = "Use Redirect Location Header Signature:";
            this.UseLocationSignatureCB.UseVisualStyleBackColor = true;
            this.UseLocationSignatureCB.CheckedChanged += new System.EventHandler(this.UseLocationSignatureCB_CheckedChanged);
            // 
            // FullResponseSignatureFirstPanel
            // 
            this.FullResponseSignatureFirstPanel.Controls.Add(this.LoggedOutResponseSignatureRB);
            this.FullResponseSignatureFirstPanel.Controls.Add(this.LoggedInResponseSignatureRB);
            this.FullResponseSignatureFirstPanel.Location = new System.Drawing.Point(11, 32);
            this.FullResponseSignatureFirstPanel.Name = "FullResponseSignatureFirstPanel";
            this.FullResponseSignatureFirstPanel.Size = new System.Drawing.Size(407, 26);
            this.FullResponseSignatureFirstPanel.TabIndex = 3;
            // 
            // LoggedOutResponseSignatureRB
            // 
            this.LoggedOutResponseSignatureRB.AutoSize = true;
            this.LoggedOutResponseSignatureRB.Location = new System.Drawing.Point(202, 6);
            this.LoggedOutResponseSignatureRB.Name = "LoggedOutResponseSignatureRB";
            this.LoggedOutResponseSignatureRB.Size = new System.Drawing.Size(195, 17);
            this.LoggedOutResponseSignatureRB.TabIndex = 5;
            this.LoggedOutResponseSignatureRB.TabStop = true;
            this.LoggedOutResponseSignatureRB.Text = "Signature for Logged Out Response";
            this.LoggedOutResponseSignatureRB.UseVisualStyleBackColor = true;
            // 
            // LoggedInResponseSignatureRB
            // 
            this.LoggedInResponseSignatureRB.AutoSize = true;
            this.LoggedInResponseSignatureRB.Location = new System.Drawing.Point(9, 5);
            this.LoggedInResponseSignatureRB.Name = "LoggedInResponseSignatureRB";
            this.LoggedInResponseSignatureRB.Size = new System.Drawing.Size(187, 17);
            this.LoggedInResponseSignatureRB.TabIndex = 4;
            this.LoggedInResponseSignatureRB.TabStop = true;
            this.LoggedInResponseSignatureRB.Text = "Signature for Logged In Response";
            this.LoggedInResponseSignatureRB.UseVisualStyleBackColor = true;
            // 
            // SignatureAnswerMsgTB
            // 
            this.SignatureAnswerMsgTB.BackColor = System.Drawing.SystemColors.Control;
            this.SignatureAnswerMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SignatureAnswerMsgTB.ForeColor = System.Drawing.Color.Red;
            this.SignatureAnswerMsgTB.Location = new System.Drawing.Point(3, 3);
            this.SignatureAnswerMsgTB.Name = "SignatureAnswerMsgTB";
            this.SignatureAnswerMsgTB.ReadOnly = true;
            this.SignatureAnswerMsgTB.Size = new System.Drawing.Size(770, 13);
            this.SignatureAnswerMsgTB.TabIndex = 2;
            // 
            // SignatureResponseCodeTB
            // 
            this.SignatureResponseCodeTB.BackColor = System.Drawing.Color.White;
            this.SignatureResponseCodeTB.Location = new System.Drawing.Point(145, 64);
            this.SignatureResponseCodeTB.Name = "SignatureResponseCodeTB";
            this.SignatureResponseCodeTB.Size = new System.Drawing.Size(78, 20);
            this.SignatureResponseCodeTB.TabIndex = 1;
            this.SignatureResponseCodeTB.TextChanged += new System.EventHandler(this.SignatureResponseCodeTB_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Response Status Code:";
            // 
            // SelectOptionTab
            // 
            this.SelectOptionTab.Controls.Add(this.SelectOptionAnswerMsgTB);
            this.SelectOptionTab.Controls.Add(this.SpecialOptionBtn);
            this.SelectOptionTab.Controls.Add(this.SelectedOptionSubmitBtn);
            this.SelectOptionTab.Controls.Add(this.OptionsGrid);
            this.SelectOptionTab.Location = new System.Drawing.Point(4, 25);
            this.SelectOptionTab.Name = "SelectOptionTab";
            this.SelectOptionTab.Size = new System.Drawing.Size(776, 250);
            this.SelectOptionTab.TabIndex = 4;
            this.SelectOptionTab.Text = "Select an Option";
            this.SelectOptionTab.UseVisualStyleBackColor = true;
            // 
            // SelectOptionAnswerMsgTB
            // 
            this.SelectOptionAnswerMsgTB.BackColor = System.Drawing.SystemColors.Control;
            this.SelectOptionAnswerMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelectOptionAnswerMsgTB.ForeColor = System.Drawing.Color.Red;
            this.SelectOptionAnswerMsgTB.Location = new System.Drawing.Point(3, 3);
            this.SelectOptionAnswerMsgTB.Name = "SelectOptionAnswerMsgTB";
            this.SelectOptionAnswerMsgTB.ReadOnly = true;
            this.SelectOptionAnswerMsgTB.Size = new System.Drawing.Size(770, 13);
            this.SelectOptionAnswerMsgTB.TabIndex = 3;
            // 
            // SpecialOptionBtn
            // 
            this.SpecialOptionBtn.Location = new System.Drawing.Point(438, 30);
            this.SpecialOptionBtn.Name = "SpecialOptionBtn";
            this.SpecialOptionBtn.Size = new System.Drawing.Size(330, 71);
            this.SpecialOptionBtn.TabIndex = 2;
            this.SpecialOptionBtn.Text = "Special Option";
            this.SpecialOptionBtn.UseVisualStyleBackColor = true;
            this.SpecialOptionBtn.Click += new System.EventHandler(this.SpecialOptionBtn_Click);
            // 
            // SelectedOptionSubmitBtn
            // 
            this.SelectedOptionSubmitBtn.Location = new System.Drawing.Point(438, 215);
            this.SelectedOptionSubmitBtn.Name = "SelectedOptionSubmitBtn";
            this.SelectedOptionSubmitBtn.Size = new System.Drawing.Size(327, 27);
            this.SelectedOptionSubmitBtn.TabIndex = 1;
            this.SelectedOptionSubmitBtn.Text = "Submit Selected Option";
            this.SelectedOptionSubmitBtn.UseVisualStyleBackColor = true;
            this.SelectedOptionSubmitBtn.Click += new System.EventHandler(this.SelectedOptionSubmitBtn_Click);
            // 
            // OptionsGrid
            // 
            this.OptionsGrid.AllowUserToAddRows = false;
            this.OptionsGrid.AllowUserToDeleteRows = false;
            this.OptionsGrid.AllowUserToResizeRows = false;
            this.OptionsGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OptionsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.OptionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OptionsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectClmn,
            this.OptionNameClmn});
            this.OptionsGrid.GridColor = System.Drawing.Color.White;
            this.OptionsGrid.Location = new System.Drawing.Point(22, 30);
            this.OptionsGrid.MultiSelect = false;
            this.OptionsGrid.Name = "OptionsGrid";
            this.OptionsGrid.ReadOnly = true;
            this.OptionsGrid.RowHeadersVisible = false;
            this.OptionsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OptionsGrid.Size = new System.Drawing.Size(410, 212);
            this.OptionsGrid.TabIndex = 0;
            this.OptionsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OptionsGrid_CellClick);
            // 
            // SelectClmn
            // 
            this.SelectClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SelectClmn.HeaderText = "SELECT";
            this.SelectClmn.Name = "SelectClmn";
            this.SelectClmn.ReadOnly = true;
            this.SelectClmn.Width = 60;
            // 
            // OptionNameClmn
            // 
            this.OptionNameClmn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OptionNameClmn.HeaderText = "OPTION";
            this.OptionNameClmn.Name = "OptionNameClmn";
            this.OptionNameClmn.ReadOnly = true;
            this.OptionNameClmn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OptionNameClmn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ShowPseudoCodeTab
            // 
            this.ShowPseudoCodeTab.Controls.Add(this.ShowPseudoCodeGoToMainMenuBtn);
            this.ShowPseudoCodeTab.Controls.Add(this.label12);
            this.ShowPseudoCodeTab.Controls.Add(this.ShowPseudoCodeTB);
            this.ShowPseudoCodeTab.Location = new System.Drawing.Point(4, 25);
            this.ShowPseudoCodeTab.Name = "ShowPseudoCodeTab";
            this.ShowPseudoCodeTab.Size = new System.Drawing.Size(776, 250);
            this.ShowPseudoCodeTab.TabIndex = 5;
            this.ShowPseudoCodeTab.Text = "Show Pseudo Code";
            this.ShowPseudoCodeTab.UseVisualStyleBackColor = true;
            // 
            // ShowPseudoCodeGoToMainMenuBtn
            // 
            this.ShowPseudoCodeGoToMainMenuBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowPseudoCodeGoToMainMenuBtn.Location = new System.Drawing.Point(619, 47);
            this.ShowPseudoCodeGoToMainMenuBtn.Name = "ShowPseudoCodeGoToMainMenuBtn";
            this.ShowPseudoCodeGoToMainMenuBtn.Size = new System.Drawing.Size(151, 59);
            this.ShowPseudoCodeGoToMainMenuBtn.TabIndex = 7;
            this.ShowPseudoCodeGoToMainMenuBtn.Text = "Go back to Main Menu";
            this.ShowPseudoCodeGoToMainMenuBtn.UseVisualStyleBackColor = true;
            this.ShowPseudoCodeGoToMainMenuBtn.Click += new System.EventHandler(this.ShowPseudoCodeGoToMainMenuBtn_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Pseudo Code:";
            // 
            // ShowPseudoCodeTB
            // 
            this.ShowPseudoCodeTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowPseudoCodeTB.Location = new System.Drawing.Point(11, 47);
            this.ShowPseudoCodeTB.Multiline = true;
            this.ShowPseudoCodeTB.Name = "ShowPseudoCodeTB";
            this.ShowPseudoCodeTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ShowPseudoCodeTB.Size = new System.Drawing.Size(602, 195);
            this.ShowPseudoCodeTB.TabIndex = 5;
            // 
            // SessionPluginCreationAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.BaseSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SessionPluginCreationAssistant";
            this.Text = "Session Plugin Creation Assistant";
            this.Load += new System.EventHandler(this.ScanCustomizationAssistant_Load);
            this.BaseSplit.Panel1.ResumeLayout(false);
            this.BaseSplit.Panel2.ResumeLayout(false);
            this.BaseSplit.ResumeLayout(false);
            this.AnswerTabs.ResumeLayout(false);
            this.TextAnswerTab.ResumeLayout(false);
            this.TextAnswerTab.PerformLayout();
            this.RequestSourceAnswerTab.ResumeLayout(false);
            this.RequestSourceAnswerTab.PerformLayout();
            this.ParameterAnswerTab.ResumeLayout(false);
            this.ParameterAnswerTab.PerformLayout();
            this.UserHintPanel.ResumeLayout(false);
            this.UserHintPanel.PerformLayout();
            this.HowToParseResponsePanel.ResumeLayout(false);
            this.HowToParseResponsePanel.PerformLayout();
            this.HowToUpdateParameterPanel.ResumeLayout(false);
            this.HowToUpdateParameterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParametersAnswerGrid)).EndInit();
            this.ResponseSignatureTab.ResumeLayout(false);
            this.ResponseSignatureTab.PerformLayout();
            this.FullResponseSignatureSecondPanel.ResumeLayout(false);
            this.FullResponseSignatureSecondPanel.PerformLayout();
            this.FullResponseSignatureFirstPanel.ResumeLayout(false);
            this.FullResponseSignatureFirstPanel.PerformLayout();
            this.SelectOptionTab.ResumeLayout(false);
            this.SelectOptionTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsGrid)).EndInit();
            this.ShowPseudoCodeTab.ResumeLayout(false);
            this.ShowPseudoCodeTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox AnswerTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BigAnswerTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SubmitAnswerBtn;
        private System.Windows.Forms.RichTextBox QuestionRTB;
        private System.Windows.Forms.TextBox StatusTB;
        private System.Windows.Forms.SplitContainer BaseSplit;
        private System.Windows.Forms.TabControl AnswerTabs;
        private System.Windows.Forms.TabPage TextAnswerTab;
        private System.Windows.Forms.TabPage ResponseSignatureTab;
        private System.Windows.Forms.TabPage RequestSourceAnswerTab;
        private System.Windows.Forms.TabPage ParameterAnswerTab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RequestSourceIdTB;
        private System.Windows.Forms.Button RequestSourceAnswerSubmitBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox RequestSourceCombo;
        private System.Windows.Forms.DataGridView ParametersAnswerGrid;
        private System.Windows.Forms.Button SubmitParameterAnswerBtn;
        private System.Windows.Forms.TextBox ParameterAskUserHintTB;
        private System.Windows.Forms.Label ParameterFourLbl;
        private System.Windows.Forms.TextBox ParameterParseRegexTB;
        private System.Windows.Forms.RadioButton ParseParameterFromRegexRB;
        private System.Windows.Forms.RadioButton ParseParameterFromHtmlRB;
        private System.Windows.Forms.Label ParameterThreeLbl;
        private System.Windows.Forms.RadioButton ParameterSourceFromUserRB;
        private System.Windows.Forms.RadioButton ParameterSourceFromResponseRB;
        private System.Windows.Forms.Label ParameterTwoLbl;
        private System.Windows.Forms.Label ParameterOneLbl;
        private System.Windows.Forms.TextBox ParameterNameTB;
        private System.Windows.Forms.Label ParameterNameTBLbl;
        private System.Windows.Forms.ComboBox ParameterTypeCombo;
        private System.Windows.Forms.Label ParameterTypeComboLbl;
        private System.Windows.Forms.TextBox ParametersAnswerMsgTB;
        private System.Windows.Forms.LinkLabel ParametersDescLL;
        private System.Windows.Forms.Panel HowToUpdateParameterPanel;
        private System.Windows.Forms.TextBox SignatureAnswerMsgTB;
        private System.Windows.Forms.TextBox SignatureResponseCodeTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel FullResponseSignatureFirstPanel;
        private System.Windows.Forms.RadioButton LoggedInResponseSignatureRB;
        private System.Windows.Forms.RadioButton LoggedOutResponseSignatureRB;
        private System.Windows.Forms.CheckBox UseBodySignatureCB;
        private System.Windows.Forms.CheckBox UseTitleSignatureCB;
        private System.Windows.Forms.CheckBox UseLocationSignatureCB;
        private System.Windows.Forms.Panel FullResponseSignatureSecondPanel;
        private System.Windows.Forms.ComboBox LocationSignatureTypeSelectCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox LocationSignatureKeywordTB;
        private System.Windows.Forms.TextBox BodySignatureKeywordTB;
        private System.Windows.Forms.ComboBox BodySignatureTypeSelectCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TitleSignatureKeywordTB;
        private System.Windows.Forms.ComboBox TitleSignatureTypeSelectCombo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button SignatureSubmitBtn;
        private System.Windows.Forms.TextBox RequestSourceAnswerMsgTB;
        private System.Windows.Forms.Panel UserHintPanel;
        private System.Windows.Forms.Panel HowToParseResponsePanel;
        private System.Windows.Forms.LinkLabel AddParameterAnswerEntryLL;
        private System.Windows.Forms.LinkLabel EditParameterAnswerEntryLL;
        private System.Windows.Forms.LinkLabel DeleteParameterAnswerEntryLL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterSectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateFromColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParseResponseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HintColumn;
        private System.Windows.Forms.TabPage SelectOptionTab;
        private System.Windows.Forms.Button SpecialOptionBtn;
        private System.Windows.Forms.Button SelectedOptionSubmitBtn;
        private System.Windows.Forms.DataGridView OptionsGrid;
        private System.Windows.Forms.TextBox SelectOptionAnswerMsgTB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectClmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OptionNameClmn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox RequestSourceNameTB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage ShowPseudoCodeTab;
        private System.Windows.Forms.Button ShowPseudoCodeGoToMainMenuBtn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ShowPseudoCodeTB;
    }
}