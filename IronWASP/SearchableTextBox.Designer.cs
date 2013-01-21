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
    partial class SearchableTextBox
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
            this.matches = new System.Windows.Forms.Label();
            this.MainText = new System.Windows.Forms.TextBox();
            this.searchbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // matches
            // 
            this.matches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.matches.AutoSize = true;
            this.matches.Location = new System.Drawing.Point(522, 259);
            this.matches.Name = "matches";
            this.matches.Size = new System.Drawing.Size(0, 13);
            this.matches.TabIndex = 6;
            // 
            // MainText
            // 
            this.MainText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainText.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainText.HideSelection = false;
            this.MainText.Location = new System.Drawing.Point(0, 1);
            this.MainText.Margin = new System.Windows.Forms.Padding(0);
            this.MainText.MaxLength = 2147483647;
            this.MainText.Multiline = true;
            this.MainText.Name = "MainText";
            this.MainText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MainText.Size = new System.Drawing.Size(560, 252);
            this.MainText.TabIndex = 4;
            // 
            // searchbox
            // 
            this.searchbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchbox.Location = new System.Drawing.Point(45, 256);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(471, 20);
            this.searchbox.TabIndex = 5;
            // 
            // SearchableTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.matches);
            this.Controls.Add(this.MainText);
            this.Controls.Add(this.searchbox);
            this.Name = "SearchableTextBox";
            this.Size = new System.Drawing.Size(560, 277);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label matches;
        private System.Windows.Forms.TextBox MainText;
        private System.Windows.Forms.TextBox searchbox;

    }
}
