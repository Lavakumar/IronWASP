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
    partial class WaitForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitForm));
            this.WaitFormProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProjectLoadGrid = new System.Windows.Forms.DataGridView();
            this.Section = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectLoadGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // WaitFormProgressBar
            // 
            this.WaitFormProgressBar.Location = new System.Drawing.Point(12, 14);
            this.WaitFormProgressBar.Maximum = 6;
            this.WaitFormProgressBar.Minimum = 1;
            this.WaitFormProgressBar.Name = "WaitFormProgressBar";
            this.WaitFormProgressBar.Size = new System.Drawing.Size(399, 23);
            this.WaitFormProgressBar.Step = 1;
            this.WaitFormProgressBar.TabIndex = 0;
            this.WaitFormProgressBar.Value = 1;
            // 
            // ProjectLoadGrid
            // 
            this.ProjectLoadGrid.AllowUserToAddRows = false;
            this.ProjectLoadGrid.AllowUserToDeleteRows = false;
            this.ProjectLoadGrid.AllowUserToResizeRows = false;
            this.ProjectLoadGrid.BackgroundColor = System.Drawing.Color.White;
            this.ProjectLoadGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProjectLoadGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.ProjectLoadGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ProjectLoadGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProjectLoadGrid.ColumnHeadersVisible = false;
            this.ProjectLoadGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Section,
            this.Status});
            this.ProjectLoadGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ProjectLoadGrid.EnableHeadersVisualStyles = false;
            this.ProjectLoadGrid.GridColor = System.Drawing.Color.White;
            this.ProjectLoadGrid.Location = new System.Drawing.Point(12, 51);
            this.ProjectLoadGrid.MultiSelect = false;
            this.ProjectLoadGrid.Name = "ProjectLoadGrid";
            this.ProjectLoadGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProjectLoadGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProjectLoadGrid.RowHeadersVisible = false;
            this.ProjectLoadGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ProjectLoadGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.ProjectLoadGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.ProjectLoadGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProjectLoadGrid.ShowCellErrors = false;
            this.ProjectLoadGrid.ShowCellToolTips = false;
            this.ProjectLoadGrid.ShowEditingIcon = false;
            this.ProjectLoadGrid.ShowRowErrors = false;
            this.ProjectLoadGrid.Size = new System.Drawing.Size(399, 280);
            this.ProjectLoadGrid.TabIndex = 1;
            // 
            // Section
            // 
            this.Section.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Section.HeaderText = "Activity";
            this.Section.MinimumWidth = 100;
            this.Section.Name = "Section";
            this.Section.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.Status.DefaultCellStyle = dataGridViewCellStyle1;
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 100;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(302, 345);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(109, 23);
            this.OK.TabIndex = 2;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Visible = false;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 372);
            this.ControlBox = false;
            this.Controls.Add(this.OK);
            this.Controls.Add(this.ProjectLoadGrid);
            this.Controls.Add(this.WaitFormProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please wait....";
            ((System.ComponentModel.ISupportInitialize)(this.ProjectLoadGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ProgressBar WaitFormProgressBar;
        internal System.Windows.Forms.DataGridView ProjectLoadGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Section;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        internal System.Windows.Forms.Button OK;
    }
}