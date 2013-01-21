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
    partial class TextBoxPlus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainText = new System.Windows.Forms.TextBox();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.MatchCountLabel = new System.Windows.Forms.Label();
            this.FunctionsPanel = new System.Windows.Forms.Panel();
            this.BinaryWarningLbl = new System.Windows.Forms.Label();
            this.LessOptionsLL = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.CaseSensitiveSearchCB = new System.Windows.Forms.CheckBox();
            this.ReplaceAllLL = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.ReplaceBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MoreOptionsLL = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.TextTab = new System.Windows.Forms.TabPage();
            this.TextPanel = new System.Windows.Forms.Panel();
            this.MainRichText = new System.Windows.Forms.RichTextBox();
            this.HexTab = new System.Windows.Forms.TabPage();
            this.MainHex = new Be.Windows.Forms.HexBox();
            this.FunctionsPanel.SuspendLayout();
            this.MainTabs.SuspendLayout();
            this.TextTab.SuspendLayout();
            this.TextPanel.SuspendLayout();
            this.HexTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainText
            // 
            this.MainText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainText.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainText.HideSelection = false;
            this.MainText.Location = new System.Drawing.Point(6, 5);
            this.MainText.Margin = new System.Windows.Forms.Padding(0);
            this.MainText.MaxLength = 2147483647;
            this.MainText.Multiline = true;
            this.MainText.Name = "MainText";
            this.MainText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MainText.Size = new System.Drawing.Size(199, 17);
            this.MainText.TabIndex = 2;
            this.MainText.TextChanged += new System.EventHandler(this.MainText_TextChanged);
            this.MainText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainText_KeyDown);
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBox.Location = new System.Drawing.Point(66, 7);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(332, 20);
            this.SearchBox.TabIndex = 3;
            this.SearchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchBox_KeyPress);
            this.SearchBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchBox_KeyUp);
            // 
            // MatchCountLabel
            // 
            this.MatchCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchCountLabel.AutoSize = true;
            this.MatchCountLabel.Location = new System.Drawing.Point(403, 11);
            this.MatchCountLabel.Name = "MatchCountLabel";
            this.MatchCountLabel.Size = new System.Drawing.Size(0, 13);
            this.MatchCountLabel.TabIndex = 4;
            // 
            // FunctionsPanel
            // 
            this.FunctionsPanel.Controls.Add(this.BinaryWarningLbl);
            this.FunctionsPanel.Controls.Add(this.LessOptionsLL);
            this.FunctionsPanel.Controls.Add(this.label4);
            this.FunctionsPanel.Controls.Add(this.CaseSensitiveSearchCB);
            this.FunctionsPanel.Controls.Add(this.ReplaceAllLL);
            this.FunctionsPanel.Controls.Add(this.label3);
            this.FunctionsPanel.Controls.Add(this.ReplaceBox);
            this.FunctionsPanel.Controls.Add(this.label2);
            this.FunctionsPanel.Controls.Add(this.MoreOptionsLL);
            this.FunctionsPanel.Controls.Add(this.label1);
            this.FunctionsPanel.Controls.Add(this.SearchBox);
            this.FunctionsPanel.Controls.Add(this.MatchCountLabel);
            this.FunctionsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FunctionsPanel.Location = new System.Drawing.Point(0, 29);
            this.FunctionsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FunctionsPanel.Name = "FunctionsPanel";
            this.FunctionsPanel.Size = new System.Drawing.Size(552, 80);
            this.FunctionsPanel.TabIndex = 5;
            // 
            // BinaryWarningLbl
            // 
            this.BinaryWarningLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BinaryWarningLbl.AutoSize = true;
            this.BinaryWarningLbl.BackColor = System.Drawing.Color.Red;
            this.BinaryWarningLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BinaryWarningLbl.Location = new System.Drawing.Point(448, 10);
            this.BinaryWarningLbl.Name = "BinaryWarningLbl";
            this.BinaryWarningLbl.Size = new System.Drawing.Size(22, 13);
            this.BinaryWarningLbl.TabIndex = 15;
            this.BinaryWarningLbl.Text = " b ";
            // 
            // LessOptionsLL
            // 
            this.LessOptionsLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LessOptionsLL.AutoSize = true;
            this.LessOptionsLL.Location = new System.Drawing.Point(477, 58);
            this.LessOptionsLL.Name = "LessOptionsLL";
            this.LessOptionsLL.Size = new System.Drawing.Size(68, 13);
            this.LessOptionsLL.TabIndex = 14;
            this.LessOptionsLL.TabStop = true;
            this.LessOptionsLL.Text = "Hide Options";
            this.LessOptionsLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LessOptionsLL_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(330, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Use \'Page Up\' / \'Page Down\' keys to move to next / previous match";
            // 
            // CaseSensitiveSearchCB
            // 
            this.CaseSensitiveSearchCB.AutoSize = true;
            this.CaseSensitiveSearchCB.Location = new System.Drawing.Point(92, 32);
            this.CaseSensitiveSearchCB.Name = "CaseSensitiveSearchCB";
            this.CaseSensitiveSearchCB.Size = new System.Drawing.Size(96, 17);
            this.CaseSensitiveSearchCB.TabIndex = 12;
            this.CaseSensitiveSearchCB.Text = "Case Sensitive";
            this.CaseSensitiveSearchCB.UseVisualStyleBackColor = true;
            this.CaseSensitiveSearchCB.Click += new System.EventHandler(this.CaseSensitiveSearchCB_Click);
            // 
            // ReplaceAllLL
            // 
            this.ReplaceAllLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceAllLL.AutoSize = true;
            this.ReplaceAllLL.Location = new System.Drawing.Point(406, 58);
            this.ReplaceAllLL.Name = "ReplaceAllLL";
            this.ReplaceAllLL.Size = new System.Drawing.Size(61, 13);
            this.ReplaceAllLL.TabIndex = 11;
            this.ReplaceAllLL.TabStop = true;
            this.ReplaceAllLL.Text = "Replace All";
            this.ReplaceAllLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ReplaceAllLL_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Replace With (case-sensitive):";
            // 
            // ReplaceBox
            // 
            this.ReplaceBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceBox.Location = new System.Drawing.Point(168, 55);
            this.ReplaceBox.Name = "ReplaceBox";
            this.ReplaceBox.Size = new System.Drawing.Size(230, 20);
            this.ReplaceBox.TabIndex = 8;
            this.ReplaceBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReplaceBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Search Options:";
            // 
            // MoreOptionsLL
            // 
            this.MoreOptionsLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoreOptionsLL.AutoSize = true;
            this.MoreOptionsLL.Location = new System.Drawing.Point(477, 10);
            this.MoreOptionsLL.Name = "MoreOptionsLL";
            this.MoreOptionsLL.Size = new System.Drawing.Size(73, 13);
            this.MoreOptionsLL.TabIndex = 6;
            this.MoreOptionsLL.TabStop = true;
            this.MoreOptionsLL.Text = "Show Options";
            this.MoreOptionsLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MoreOptionsLL_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search for:";
            // 
            // MainTabs
            // 
            this.MainTabs.Controls.Add(this.TextTab);
            this.MainTabs.Controls.Add(this.HexTab);
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 0);
            this.MainTabs.Margin = new System.Windows.Forms.Padding(0);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.Padding = new System.Drawing.Point(0, 0);
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(560, 135);
            this.MainTabs.TabIndex = 6;
            this.MainTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MainTabs_Deselecting);
            // 
            // TextTab
            // 
            this.TextTab.Controls.Add(this.TextPanel);
            this.TextTab.Controls.Add(this.FunctionsPanel);
            this.TextTab.Location = new System.Drawing.Point(4, 22);
            this.TextTab.Margin = new System.Windows.Forms.Padding(0);
            this.TextTab.Name = "TextTab";
            this.TextTab.Size = new System.Drawing.Size(552, 109);
            this.TextTab.TabIndex = 0;
            this.TextTab.Text = " Text ";
            this.TextTab.UseVisualStyleBackColor = true;
            // 
            // TextPanel
            // 
            this.TextPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPanel.Controls.Add(this.MainRichText);
            this.TextPanel.Controls.Add(this.MainText);
            this.TextPanel.Location = new System.Drawing.Point(0, 0);
            this.TextPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(552, 29);
            this.TextPanel.TabIndex = 6;
            // 
            // MainRichText
            // 
            this.MainRichText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainRichText.DetectUrls = false;
            this.MainRichText.Location = new System.Drawing.Point(307, 5);
            this.MainRichText.Name = "MainRichText";
            this.MainRichText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.MainRichText.Size = new System.Drawing.Size(206, 17);
            this.MainRichText.TabIndex = 3;
            this.MainRichText.Text = "";
            this.MainRichText.TextChanged += new System.EventHandler(this.MainRichText_TextChanged);
            // 
            // HexTab
            // 
            this.HexTab.Controls.Add(this.MainHex);
            this.HexTab.Location = new System.Drawing.Point(4, 22);
            this.HexTab.Margin = new System.Windows.Forms.Padding(0);
            this.HexTab.Name = "HexTab";
            this.HexTab.Size = new System.Drawing.Size(552, 109);
            this.HexTab.TabIndex = 1;
            this.HexTab.Text = " Hex ";
            this.HexTab.UseVisualStyleBackColor = true;
            // 
            // MainHex
            // 
            this.MainHex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainHex.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainHex.InfoForeColor = System.Drawing.Color.Empty;
            this.MainHex.LineInfoVisible = true;
            this.MainHex.Location = new System.Drawing.Point(0, 0);
            this.MainHex.Name = "MainHex";
            this.MainHex.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.MainHex.Size = new System.Drawing.Size(552, 109);
            this.MainHex.StringViewVisible = true;
            this.MainHex.TabIndex = 0;
            this.MainHex.VScrollBarVisible = true;
            // 
            // TextBoxPlus
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.MainTabs);
            this.Name = "TextBoxPlus";
            this.Size = new System.Drawing.Size(560, 135);
            this.Load += new System.EventHandler(this.ModTextBoxPlus_Load);
            this.FunctionsPanel.ResumeLayout(false);
            this.FunctionsPanel.PerformLayout();
            this.MainTabs.ResumeLayout(false);
            this.TextTab.ResumeLayout(false);
            this.TextPanel.ResumeLayout(false);
            this.TextPanel.PerformLayout();
            this.HexTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox MainText;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Label MatchCountLabel;
        private System.Windows.Forms.Panel FunctionsPanel;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage TextTab;
        private System.Windows.Forms.TabPage HexTab;
        private System.Windows.Forms.LinkLabel MoreOptionsLL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox MainRichText;
        private Be.Windows.Forms.HexBox MainHex;
        private System.Windows.Forms.Panel TextPanel;
        private System.Windows.Forms.LinkLabel ReplaceAllLL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ReplaceBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CaseSensitiveSearchCB;
        private System.Windows.Forms.LinkLabel LessOptionsLL;
        private System.Windows.Forms.Label BinaryWarningLbl;
    }
}
