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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IronWASP
{
    public partial class ModuleStartPromptForm : Form
    {
        internal Module DisplayedModule = new Module();
        internal bool ModuleAuthroized = false;
        internal bool IsClosed = false;

        internal void SetModule(Module M)
        {
            this.DisplayedModule = M;
            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
            if (M.Category.Equals("My Downloads"))
            {
                SB.Append(@" \par \qc \fs20 \cf3  This Module was downloaded by you and was not included along with IronWASP. Therefore the details displayed below are unverified, only run this Module if you trust the source you recieved this Module from.  \cf0 \par \par");
            }
            else
            {
                SB.Append(@" \par \qc \fs15 \cf3  Modules are not internal components of IronWASP. These are seperate tools written by other developers and therefore require user permission before running.  \cf0 \par \par");
            }
            SB.Append(string.Format(@" \qc \fs30 {0} \par \fs20 \ql", M.DisplayName));
            StringBuilder InfoBuilder = new StringBuilder();
            InfoBuilder.Append(string.Format("<i<br>><i<b>><i<cb>>Name:<i</cb>><i</b>>{0}<i<br>>", M.Name));
            InfoBuilder.Append(string.Format("<i<b>><i<cb>>Author:<i</cb>><i</b>>{0}<i<br>>", M.Author));
            InfoBuilder.Append(string.Format("<i<b>><i<cb>>Project Home:<i</cb>><i</b>>{0}<i<br>>", M.ProjectHome));
            InfoBuilder.Append(string.Format("<i<b>><i<cb>>Description:<i</cb>><i</b>><i<br>><i<br>>{0}", M.Description));
            SB.Append(Tools.RtfSafe(InfoBuilder.ToString()));
            this.DisplayRTB.Rtf = SB.ToString();
        }
        
        public ModuleStartPromptForm()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ModuleAuthroized = true;
            this.Close();
        }

        delegate void CloseForm_d();
        internal void CloseForm()
        {
            if (this.InvokeRequired)
            {
                CloseForm_d CF_d = new CloseForm_d(CloseForm);
                this.Invoke(CF_d, new object[] { });
            }
            else
            {
                this.Close();
            }
        }

        delegate void ShowError_d(string Error);
        internal void ShowError(string Error)
        {
            if (this.InvokeRequired)
            {
                ShowError_d SE_d = new ShowError_d(ShowError);
                this.Invoke(SE_d, new object[] { Error });
            }
            else
            {
                this.StatusLbl.Text = Error;
                this.StatusLbl.ForeColor = Color.Red;
                this.RunModuleBtn.Enabled = false;
                this.ProgressBarPanel.Visible = false;
            }
        }

        private void RunModuleBtn_Click(object sender, EventArgs e)
        {
            this.ProgressBarPanel.Visible = true;
            this.StatusLbl.Text = "Loading Module.....";
            //this.ProgressPB.PerformStep();
            this.ModuleAuthroized = true;
            IronThread.Run(Module.LoadModule, this.DisplayedModule);
        }

        private void ModuleStartPromptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosed = true;
            Module.RemovePromptWindowFromList(this.DisplayedModule);
        }
    }
}
