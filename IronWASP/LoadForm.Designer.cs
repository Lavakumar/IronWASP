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
    partial class LoadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadForm));
            this.LoadLogoPB = new System.Windows.Forms.PictureBox();
            this.LoadFormCopyrightTB = new System.Windows.Forms.TextBox();
            this.StatusTB = new System.Windows.Forms.TextBox();
            this.LoadMessageRTB = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.LoadLogoPB)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadLogoPB
            // 
            this.LoadLogoPB.Image = ((System.Drawing.Image)(resources.GetObject("LoadLogoPB.Image")));
            this.LoadLogoPB.InitialImage = ((System.Drawing.Image)(resources.GetObject("LoadLogoPB.InitialImage")));
            this.LoadLogoPB.Location = new System.Drawing.Point(143, 3);
            this.LoadLogoPB.Name = "LoadLogoPB";
            this.LoadLogoPB.Size = new System.Drawing.Size(218, 41);
            this.LoadLogoPB.TabIndex = 1;
            this.LoadLogoPB.TabStop = false;
            // 
            // LoadFormCopyrightTB
            // 
            this.LoadFormCopyrightTB.BackColor = System.Drawing.Color.White;
            this.LoadFormCopyrightTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoadFormCopyrightTB.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadFormCopyrightTB.Location = new System.Drawing.Point(140, 127);
            this.LoadFormCopyrightTB.Name = "LoadFormCopyrightTB";
            this.LoadFormCopyrightTB.ReadOnly = true;
            this.LoadFormCopyrightTB.Size = new System.Drawing.Size(247, 18);
            this.LoadFormCopyrightTB.TabIndex = 3;
            this.LoadFormCopyrightTB.Text = "Copyright © 2011 Lavakumar Kuppan";
            // 
            // StatusTB
            // 
            this.StatusTB.BackColor = System.Drawing.Color.White;
            this.StatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusTB.Location = new System.Drawing.Point(2, 84);
            this.StatusTB.Multiline = true;
            this.StatusTB.Name = "StatusTB";
            this.StatusTB.ReadOnly = true;
            this.StatusTB.Size = new System.Drawing.Size(526, 40);
            this.StatusTB.TabIndex = 5;
            this.StatusTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoadMessageRTB
            // 
            this.LoadMessageRTB.BackColor = System.Drawing.Color.White;
            this.LoadMessageRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoadMessageRTB.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadMessageRTB.ForeColor = System.Drawing.Color.SteelBlue;
            this.LoadMessageRTB.Location = new System.Drawing.Point(98, 48);
            this.LoadMessageRTB.Name = "LoadMessageRTB";
            this.LoadMessageRTB.Size = new System.Drawing.Size(309, 33);
            this.LoadMessageRTB.TabIndex = 6;
            this.LoadMessageRTB.Text = "                 Advanced Web Security Testing Platform\nPowered by FiddlerCore, I" +
                "ronPython, IronRuby, Jint & others";
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(530, 150);
            this.ControlBox = false;
            this.Controls.Add(this.LoadMessageRTB);
            this.Controls.Add(this.StatusTB);
            this.Controls.Add(this.LoadFormCopyrightTB);
            this.Controls.Add(this.LoadLogoPB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(530, 150);
            this.MinimumSize = new System.Drawing.Size(530, 150);
            this.Name = "LoadForm";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.LoadLogoPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox LoadLogoPB;
        internal System.Windows.Forms.TextBox LoadFormCopyrightTB;
        internal System.Windows.Forms.TextBox StatusTB;
        private System.Windows.Forms.RichTextBox LoadMessageRTB;
    }
}