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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
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
    public partial class AskUserWindow : Form
    {
        public AskUserWindow()
        {
            InitializeComponent();
        }

        private void AskUserYesBtn_Click(object sender, EventArgs e)
        {
            AskUser.CurrentlyAsked.BoolAnswer = true;
            AskUser.CurrentlyAsked.MSR.Set();
            IronUI.AskUserAnswered();
        }

        private void AskUserNoBtn_Click(object sender, EventArgs e)
        {
            AskUser.CurrentlyAsked.BoolAnswer = false;
            AskUser.CurrentlyAsked.MSR.Set();
            IronUI.AskUserAnswered();
        }

        private void AskUserSubmitBtn_Click(object sender, EventArgs e)
        {
            if (AskUser.CurrentlyAsked.ReturnType.Equals("String"))
            {
                AskUser.CurrentlyAsked.StringAnswer = AskUserAnswerTB.Text;
            }
            else
            {
                List<int> Answer = new List<int>();
                if (AskUserAnswerRBOne.Visible)
                {
                    if(AskUserAnswerRBOne.Checked)
                        Answer.Add(1);
                    else
                        Answer.Add(0);
                    if (AskUserAnswerRBTwo.Checked)
                        Answer.Add(1);
                    else
                        Answer.Add(0);
                }
                foreach (DataGridViewRow Row in IronUI.AUW.AskUserAnswerGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value) Answer.Add(Row.Index);
                }
                if (AskUserAnswerRBOne.Visible && Answer.Count > 2)
                {
                    Answer[0] = 1;
                    Answer[1] = 0;
                }
                AskUser.CurrentlyAsked.ListAnswer = new List<int>(Answer);
            }
            AskUser.CurrentlyAsked.MSR.Set();
            IronUI.AskUserAnswered();
        }

        private void AskUserAnswerGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && AskUserAnswerRBOne.Visible == true)
            {
                if (!(bool)AskUserAnswerGrid.Rows[e.RowIndex].Cells[0].Value)
                {
                    AskUserAnswerRBOne.Checked = true;
                    AskUserSubmitBtn.Focus();
                    return;
                }
                foreach (DataGridViewRow Row in AskUserAnswerGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value && Row.Index != e.RowIndex)
                    {
                        AskUserAnswerRBOne.Checked = true;
                        AskUserSubmitBtn.Focus();
                        return;
                    }
                }
                AskUserAnswerRBTwo.Checked = true;
                AskUserSubmitBtn.Focus();
            }
        }

        private void AskUserAnswerRBTwo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in AskUserAnswerGrid.Rows)
            {
                Row.Cells[0].Value = false;
            }
        }

        private void AskUserWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
