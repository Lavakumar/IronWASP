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
    public partial class SessionsDiffer : Form
    {
        Session A;
        Session B;
        Thread DiffThread;

        string LogSource = "";
        int ALogId = -1;
        int BLogId = -1;

        bool FetchFromLogs = false;

        public SessionsDiffer()
        {
            InitializeComponent();
        }

        public void SetSessions(string Source, int ALogId, int BLogId)
        {
            this.LogSource = Source;
            this.ALogId = ALogId;
            this.BLogId = BLogId;
            this.FetchFromLogs = true;
        }

        public void SetSessions(Session A, Session B)
        {
            this.A = A;
            this.B = B;
            this.FetchFromLogs = false;
        }

        private void SessionsDiffer_Load(object sender, EventArgs e)
        {
            BaseTabs.Visible = false;
            DiffProgressBar.Visible = true;
            DiffThread = new Thread(DoDiff);
            DiffThread.Start();
        }

        void DoDiff()
        {
            try
            {
                if (FetchFromLogs)
                {
                    this.A = Session.FromLog(this.ALogId, this.LogSource);
                    this.B = Session.FromLog(this.BLogId, this.LogSource);
                }
                
                string RequestA = "";
                string RequestB = "";
                string ResponseA = "";
                string ResponseB = "";
                if (A != null)
                {
                    if (A.Request != null)
                        RequestA = A.Request.ToString();
                    if (A.Response != null)
                        ResponseA = A.Response.ToString();
                }
                if (B != null)
                {
                    if (B.Request != null)
                        RequestB = B.Request.ToString();
                    if (B.Response != null)
                        ResponseB = B.Response.ToString();
                }

                string[] RequestSidebySideResults = DiffWindow.DoSideBySideDiff(RequestA, RequestB);
                string[] ResponseSidebySideResults = DiffWindow.DoSideBySideDiff(ResponseA, ResponseB);
                string RequestSinglePageResults = DiffWindow.DoSinglePageDiff(RequestA, RequestB);
                string ResponseSinglePageResults = DiffWindow.DoSinglePageDiff(ResponseA, ResponseB);
                RequestDRV.ShowDiffResults(RequestSinglePageResults, RequestSidebySideResults[0], RequestSidebySideResults[1]);
                ResponseDRV.ShowDiffResults(ResponseSinglePageResults, ResponseSidebySideResults[0], ResponseSidebySideResults[1]);
            }
            catch(Exception Exp)
            { 
                IronException.Report("Error doing diff on Sessions", Exp);
                ShowError();
            }
            EndDiff();
        }

        delegate void ShowError_d();
        void ShowError()
        {
            if (BaseTabs.InvokeRequired)
            {
                ShowError_d CALL_d = new ShowError_d(ShowError);
                BaseTabs.Invoke(CALL_d, new object[] { });
            }
            else
            {
                this.Text = "Error doing diff. Make sure you selected proper input.";   
            }
        }

        delegate void EndDiff_d();
        void EndDiff()
        {
            if (BaseTabs.InvokeRequired)
            {
                EndDiff_d CALL_d = new EndDiff_d(EndDiff);
                BaseTabs.Invoke(CALL_d, new object[] { });
            }
            else
            {
                DiffProgressBar.Visible = false;

                BaseTabs.Visible = true;
            }
        }

        private void SessionsDiffer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DiffThread != null)
            {
                try
                {
                    DiffThread.Abort();
                }
                catch { }
            }
        }
    }
}
