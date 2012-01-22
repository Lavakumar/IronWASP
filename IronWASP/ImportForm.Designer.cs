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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
//

namespace IronWASP
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.WaitFormProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // WaitFormProgressBar
            // 
            this.WaitFormProgressBar.Location = new System.Drawing.Point(11, 30);
            this.WaitFormProgressBar.MarqueeAnimationSpeed = 10;
            this.WaitFormProgressBar.Maximum = 6;
            this.WaitFormProgressBar.Minimum = 1;
            this.WaitFormProgressBar.Name = "WaitFormProgressBar";
            this.WaitFormProgressBar.Size = new System.Drawing.Size(338, 23);
            this.WaitFormProgressBar.Step = 1;
            this.WaitFormProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.WaitFormProgressBar.TabIndex = 1;
            this.WaitFormProgressBar.Value = 1;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 92);
            this.ControlBox = false;
            this.Controls.Add(this.WaitFormProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(375, 130);
            this.MinimumSize = new System.Drawing.Size(375, 130);
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import in Progress....Please Wait";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ProgressBar WaitFormProgressBar;
    }
}