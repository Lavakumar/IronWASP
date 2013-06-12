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
    partial class CodeTextBox
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
            this.Editor = new ICSharpCode.TextEditor.TextEditorControl();
            this.SuspendLayout();
            // 
            // Editor
            // 
            this.Editor.ConvertTabsToSpaces = true;
            this.Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Editor.IsIconBarVisible = false;
            this.Editor.Location = new System.Drawing.Point(0, 0);
            this.Editor.Margin = new System.Windows.Forms.Padding(0);
            this.Editor.Name = "Editor";
            this.Editor.ShowEOLMarkers = true;
            this.Editor.ShowSpaces = true;
            this.Editor.ShowTabs = true;
            this.Editor.ShowVRuler = true;
            this.Editor.Size = new System.Drawing.Size(200, 100);
            this.Editor.TabIndent = 2;
            this.Editor.TabIndex = 5;
            // 
            // CodeTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Editor);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CodeTextBox";
            this.Size = new System.Drawing.Size(200, 100);
            this.Load += new System.EventHandler(this.CodeTextBox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal ICSharpCode.TextEditor.TextEditorControl Editor;
    }
}
