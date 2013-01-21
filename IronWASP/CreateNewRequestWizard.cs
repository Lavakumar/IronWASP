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
using System.Threading;
using System.Windows.Forms;

namespace IronWASP
{
    public partial class CreateNewRequestWizard : Form
    {
        int CurrentStep = 0;

        Request RequestToTest = null;

        public CreateNewRequestWizard()
        {
            InitializeComponent();
        }

        private void CreateNewRequestWizard_Load(object sender, EventArgs e)
        {
            BuildUserAgentTree();
        }

        void BuildUserAgentTree()
        {
            UserAgentTree.BeginUpdate();
            TreeNode RootNode = UserAgentTree.Nodes.Add("User-Agents", "User-Agents Categories   [ List source: http://techpatterns.com/forums/about304.html ]");
            foreach (string Category in Config.UserAgentsList.Keys)
            {
                try
                {
                    TreeNode T = RootNode.Nodes.Add(Category, Category);
                    foreach (string Name in Config.UserAgentsList[Category].Keys)
                    {
                        try
                        {
                            T.Nodes.Add(Config.UserAgentsList[Category][Name], Name);
                        }
                        catch { }
                    }
                }
                catch { }
            }
            UserAgentTree.EndUpdate();
            UserAgentTree.Nodes[0].Expand();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserAgentTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            if (e.Node.Level == 2)
            {
                SelectedUserAgentLbl.Text = e.Node.Name;
                UseUserAgentCB.Checked = true;
            }
            else
            {
                SelectedUserAgentLbl.Text = "";
                UseUserAgentCB.Checked = false;
            }
        }

        private void CreateRequestBtn_Click(object sender, EventArgs e)
        {
            ShowStep1Error("");
            string Name = RequestNameTB.Text.Trim();
            if (Name.Length == 0)
            {
                bool Named = false;
                while (!Named)
                {
                    Name = string.Format("untitled-{0}", Interlocked.Increment(ref ManualTesting.UntitledCount));
                    if (!ManualTesting.GroupSessions.ContainsKey(Name))
                    {
                        Named = true;
                    }
                }
            }
            else
            {
                if (ManualTesting.GroupSessions.ContainsKey(Name))
                {
                    ShowStep1Error("A Request group with this name already exists, pick another one.");
                    return;
                }
            }
            ManualTesting.CreateNewGroupWithRequest(RequestToTest, Name, true);
            this.Close();
        }

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (BaseTabs.SelectedIndex != CurrentStep) BaseTabs.SelectTab(CurrentStep);
        }

        private void Step0NextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStep0Status("");
                RequestToTest = new Request(RequestUrlTB.Text.Trim());
                if (UseUserAgentCB.Checked)
                {
                    if (SelectedUserAgentLbl.Text.Trim().Length > 0)
                    {
                        RequestToTest.Headers.Set("User-Agent", SelectedUserAgentLbl.Text);
                    }
                    else
                    {
                        ShowStep0Error("No user-agent strings were selected");
                        return;
                    }
                }
                if (UseAdditionalHeadersCB.Checked)
                {
                    RequestToTest.Headers.Set("Accept", "*/*");
                    RequestToTest.Headers.Set("Accept-Encoding", "gzip,deflate");
                    RequestToTest.Headers.Set("Accept-Charset", "ISO-8859-1,utf-8");
                }
                if (UsePostBodyCB.Checked)
                {
                    if (PostBodyTB.Text.Trim().Length == 0)
                    {
                        ShowStep0Error("Post body is empty");
                        return;
                    }
                    else
                    {
                        RequestToTest.Method = "POST";
                        RequestToTest.BodyString = PostBodyTB.Text.Trim();
                    }
                }
                CurrentStep = 1;
                BaseTabs.SelectTab(1);
            }
            catch (Exception Exp) { ShowStep0Error(string.Format("Invalid input - {0}", Exp.Message)); }
        }

        void ShowStep0Status(string Text)
        {
            Step0StatusTB.Text = Text;
            Step0StatusTB.BackColor = Color.White;
            if (Text.Length == 0)
                Step0StatusTB.Visible = false;
            else
                Step0StatusTB.Visible = true;
        }
        void ShowStep0Error(string Text)
        {
            Step0StatusTB.Text = Text;
            Step0StatusTB.BackColor = Color.Red;
            if (Text.Length == 0)
                Step0StatusTB.Visible = false;
            else
                Step0StatusTB.Visible = true;
        }

        void ShowStep1Error(string Text)
        {
            Step1StatusTB.Text = Text;
            Step1StatusTB.BackColor = Color.Red;
            if (Text.Length == 0)
                Step1StatusTB.Visible = false;
            else
                Step1StatusTB.Visible = true;
        }

        private void Step1PreviousStepBtn_Click(object sender, EventArgs e)
        {
            CurrentStep = 0;
            BaseTabs.SelectTab(0);
        }

        private void UseUserAgentCB_CheckedChanged(object sender, EventArgs e)
        {
            UserAgentTree.Enabled = UseUserAgentCB.Checked;
        }

        private void UsePostBodyCB_CheckedChanged(object sender, EventArgs e)
        {
            PostBodyTB.Enabled = UsePostBodyCB.Checked;
        }
    }
}
