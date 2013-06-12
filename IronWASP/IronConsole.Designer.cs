namespace IronWASP
{
    partial class IronConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IronConsole));
            this.BaseSplit = new System.Windows.Forms.SplitContainer();
            this.OutputTB = new System.Windows.Forms.TextBox();
            this.InputLbl = new System.Windows.Forms.Label();
            this.InputTB = new System.Windows.Forms.TextBox();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.BaseSplit.Panel1.SuspendLayout();
            this.BaseSplit.Panel2.SuspendLayout();
            this.BaseSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseSplit
            // 
            this.BaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseSplit.Location = new System.Drawing.Point(0, 0);
            this.BaseSplit.Name = "BaseSplit";
            this.BaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // BaseSplit.Panel1
            // 
            this.BaseSplit.Panel1.Controls.Add(this.OutputTB);
            // 
            // BaseSplit.Panel2
            // 
            this.BaseSplit.Panel2.BackColor = System.Drawing.Color.White;
            this.BaseSplit.Panel2.Controls.Add(this.SubmitBtn);
            this.BaseSplit.Panel2.Controls.Add(this.InputTB);
            this.BaseSplit.Panel2.Controls.Add(this.InputLbl);
            this.BaseSplit.Size = new System.Drawing.Size(784, 561);
            this.BaseSplit.SplitterDistance = 422;
            this.BaseSplit.SplitterWidth = 2;
            this.BaseSplit.TabIndex = 0;
            // 
            // OutputTB
            // 
            this.OutputTB.BackColor = System.Drawing.Color.Black;
            this.OutputTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputTB.Cursor = System.Windows.Forms.Cursors.Default;
            this.OutputTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputTB.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputTB.ForeColor = System.Drawing.Color.Lime;
            this.OutputTB.Location = new System.Drawing.Point(0, 0);
            this.OutputTB.Margin = new System.Windows.Forms.Padding(0);
            this.OutputTB.Multiline = true;
            this.OutputTB.Name = "OutputTB";
            this.OutputTB.ReadOnly = true;
            this.OutputTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTB.Size = new System.Drawing.Size(784, 422);
            this.OutputTB.TabIndex = 0;
            this.OutputTB.TabStop = false;
            // 
            // InputLbl
            // 
            this.InputLbl.AutoSize = true;
            this.InputLbl.Location = new System.Drawing.Point(7, 13);
            this.InputLbl.Name = "InputLbl";
            this.InputLbl.Size = new System.Drawing.Size(115, 13);
            this.InputLbl.TabIndex = 0;
            this.InputLbl.Text = "Enter your input below:";
            // 
            // InputTB
            // 
            this.InputTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTB.Location = new System.Drawing.Point(12, 43);
            this.InputTB.Name = "InputTB";
            this.InputTB.Size = new System.Drawing.Size(678, 20);
            this.InputTB.TabIndex = 1;
            this.InputTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputTB_KeyPress);
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SubmitBtn.Location = new System.Drawing.Point(697, 41);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(81, 23);
            this.SubmitBtn.TabIndex = 2;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // IronConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.BaseSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IronConsole";
            this.Text = "Script\'s Console Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IronConsole_FormClosing);
            this.Load += new System.EventHandler(this.IronConsole_Load);
            this.BaseSplit.Panel1.ResumeLayout(false);
            this.BaseSplit.Panel1.PerformLayout();
            this.BaseSplit.Panel2.ResumeLayout(false);
            this.BaseSplit.Panel2.PerformLayout();
            this.BaseSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer BaseSplit;
        private System.Windows.Forms.TextBox OutputTB;
        private System.Windows.Forms.Label InputLbl;
        private System.Windows.Forms.Button SubmitBtn;
        private System.Windows.Forms.TextBox InputTB;
    }
}