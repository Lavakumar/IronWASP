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
using System.Threading;

namespace IronWASP
{
    public partial class LogTraceViewer : Form
    {
        Thread LogLoadThread;

        int SelectedRowsCount = 0;

        public LogTraceViewer()
        {
            InitializeComponent();
        }

        private void ScanTraceOverviewGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanTraceOverviewGrid.SelectedRows == null) return;
            if (ScanTraceOverviewGrid.SelectedRows.Count == 0) return;

                
            if (ClickActionSelectLogRB.Checked)
            {
                ScanTraceOverviewGrid.SelectedRows[0].Cells[0].Value = !((bool)ScanTraceOverviewGrid.SelectedRows[0].Cells[0].Value);
                if ((bool)ScanTraceOverviewGrid.SelectedRows[0].Cells[0].Value)
                {
                    SelectedRowsCount++;
                }
                else
                {
                    SelectedRowsCount--;
                }
                if (SelectedRowsCount > 0)
                {
                    DoDiffBtn.Enabled = true;
                }
                else
                {
                    DoDiffBtn.Enabled = false;
                }
            }
            else
            {
                try
                {
                    int LogId = Int32.Parse(ScanTraceOverviewGrid.SelectedCells[2].Value.ToString());
                    ShowSelectedLog(LogId);
                }
                catch { }
            }
        }
        void ShowSelectedLog(int LogId)
        {
            LogDisplayTabs.Visible = false;
            LoadLogProgressBar.Visible = true;

            RequestView.ClearRequest();
            ResponseView.ClearResponse();

            if (LogLoadThread != null)
            {
                try
                {
                    LogLoadThread.Abort();
                }
                catch { }
            }
            LogLoadThread = new Thread(ShowSelectedLog);
            LogLoadThread.Start(LogId);
        }

        void ShowSelectedLog(object SelectedLogInfoObject)
        {
            int LogId = (int)SelectedLogInfoObject;

            try
            {
                Session Session = Session.FromScanLog(LogId);

                if (Session.Request != null)
                {
                    RequestView.SetRequest(Session.Request);

                    if (Session.Response != null) ResponseView.SetResponse(Session.Response, Session.Request);
                }

            }
            catch (ThreadAbortException) { }
            catch (Exception Exp) { IronException.Report("Error loading Selected Log info in Scan Trace Viewer", Exp); }
            finally
            {
                EndLogLoad();
            }
        }

        delegate void EndLogLoad_d();
        void EndLogLoad()
        {
            if (ScanTraceOverviewGrid.InvokeRequired)
            {
                EndLogLoad_d CALL_d = new EndLogLoad_d(EndLogLoad);
                ScanTraceOverviewGrid.Invoke(CALL_d, new object[] { });
            }
            else
            {
                LoadLogProgressBar.Visible = false;
                LogDisplayTabs.Visible = true;
            }
        }

        private void LogTraceViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                LogLoadThread.Abort();
            }
            catch { }
        }

        private void DoDiffBtn_Click(object sender, EventArgs e)
        {
            if (SelectedRowsCount == 2)
            {
                int ALogId = -1;
                int BLogId = -1;
                foreach (DataGridViewRow Row in ScanTraceOverviewGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        if (ALogId == -1)
                        {
                            try
                            {
                                ALogId = Int32.Parse(Row.Cells[2].Value.ToString());
                            }
                            catch { }
                        }
                        else if (BLogId == -1)
                        {
                            try
                            {
                                BLogId = Int32.Parse(Row.Cells[2].Value.ToString());
                            }
                            catch { }
                            break;
                        }
                    }
                }
                SessionsDiffer Sdiff = new SessionsDiffer();
                Sdiff.SetSessions("Scan", ALogId, BLogId);
                Sdiff.Show();
            }
            else
            {
                MessageBox.Show(string.Format("Diff can be done only when two sessions are selected. You have selected {0} sessions", SelectedRowsCount), "Selection Error");
            }
        }
    }
}
