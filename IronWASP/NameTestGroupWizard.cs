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
    public partial class NameTestGroupWizard : Form
    {
        static bool MoveToMTSection = true;

        internal Request RequestToTest = null;

        public NameTestGroupWizard()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DoneBtn_Click(object sender, EventArgs e)
        {
            ShowStep0Error("");
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
                    ShowStep0Error("A Request group with this name already exists, pick another one.");
                    return;
                }
            }
            MoveToMTSection = SwithToMTSectionCB.Checked;
            ManualTesting.CreateNewGroupWithRequest(RequestToTest, Name, MoveToMTSection);
            this.Close();
        }

        private void NameTestGroupWizard_Load(object sender, EventArgs e)
        {
            SwithToMTSectionCB.Checked = MoveToMTSection;
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
    }
}
