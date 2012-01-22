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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IronWASP
{
    public partial class CloseForm : Form
    {
        public CloseForm()
        {
            InitializeComponent();
        }

        private void CloseForm_Load(object sender, EventArgs e)
        {
            StringBuilder Msg = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red255\green0\blue0;\red0\green200\blue50;} \qc \fs22 \cf1 \b System Status \b0  \cf0 \par \pard \fs18 ");
            Msg.Append(@" \tab Active Scan Jobs - ");
            string ScansActive = @" \cf3 \b No \b0 \cf0 \par ";
            foreach (int ScanID in Scanner.ScanThreads.Keys)
            {
                if (Scanner.ScanThreads[ScanID].ThreadState == ThreadState.Running || Scanner.ScanThreads[ScanID].ThreadState == ThreadState.WaitSleepJoin)
                {
                    ScansActive = @" \cf2 \b Yes \b0 \cf0 \par ";
                    break;
                }
            }
            Msg.AppendLine(ScansActive);
            Msg.Append(@" \tab Scripts Running in Scripting Shell - ");
            string ShellActive = @" \cf3 \b No \b0 \cf0 \par ";
            if (IronUI.UI.InteractiveShellCtrlCBtn.Enabled) ShellActive = @" \cf2 \b Yes \b0 \cf0 \par ";
            Msg.AppendLine(ShellActive);
            Msg.Append(@" \tab Requests pending for Passive Plug-in Analysis - ");
            int ReqQLen = PassiveChecker.RequestQueueLength;
            if (ReqQLen == 0)
                Msg.Append(@" \cf3 \b 0 \b0 \cf0 \par ");
            else
            { Msg.Append(@" \cf2 \b "); Msg.Append(ReqQLen.ToString()); Msg.Append(@" \b0 \cf0 \par "); }

            Msg.Append(@" \tab Responses pending for Passive Plug-in Analysis - ");
            int ResQLen = PassiveChecker.ResponseQueueLength;
            if (ResQLen == 0)
                Msg.Append(@" \cf3 \b 0 \b0 \cf0 \par ");
            else
            { Msg.Append(@" \cf2 \b "); Msg.Append(ResQLen.ToString()); Msg.Append(@" \b0 \cf0 \par "); }
            Msg.AppendLine("}");
            StatusMsgRTB.Rtf = Msg.ToString();
            ProjectNameTB.Text = IronDB.LogPath;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                CloseBtn.Enabled = false;
                RenameCloseBtn.Enabled = false;
                CancelBtn.Enabled = false;
                ErrorTB.Text = "Shutting down Iron...";
                IronUI.UI.ShutDown();
            }
            catch { }
            Application.Exit();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RenameCloseBtn_Click(object sender, EventArgs e)
        {
            CloseBtn.Enabled = false;
            if (RenameCloseBtn.Text.Equals("Rename and Close"))
            {
                RenameCloseBtn.Enabled = false;
                CloseBtn.Enabled = false;
                CancelBtn.Enabled = false;
                ErrorTB.Text = "Shutting down Iron...";
                IronUI.UI.ShutDown();
                RenameCloseBtn.Text = "Rename";
                RenameCloseBtn.Enabled = true;
                ProjectNameTB.ReadOnly = false;
                ProjectNameTB.BackColor = Color.White;
                ErrorTB.Text = "Rename Project Folder";
            }
            else
            {
                try
                {
                    Directory.Move(IronDB.LogPath, ProjectNameTB.Text);
                    ErrorTB.Text = "Rename Complete!";
                }
                catch(Exception Exp)
                {
                    ErrorTB.Text = Exp.Message;
                    CloseBtn.Enabled = true;
                    return;
                }
                Application.Exit();
            }
        }

        private void StatusMsgRTB_Enter(object sender, EventArgs e)
        {
            CancelBtn.Focus();
        }
    }
}
