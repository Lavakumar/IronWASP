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
using System.Windows.Forms;

namespace IronWASP
{
    public partial class ScanBranchForm : Form
    {
        public ScanBranchForm()
        {
            InitializeComponent();
        }

        private void ScanBranchCancelBtn_Click(object sender, EventArgs e)
        {
            if (ScanBranch.ScanThread != null)
            {
                try
                {
                    ScanBranch.ScanThread.Abort();
                }
                catch
                {
                    //
                }
            }
            ScanBranchForm.ActiveForm.Close();
        }

        private void ScanBranchStartScanBtn_Click(object sender, EventArgs e)
        {
            ScanBranchStartScanBtn.Enabled = false;
            ScanBranchErrorTB.Text = "";
            if (ScanBranchHostNameTB.Text.Trim().Length == 0)
            {
                ScanBranchErrorTB.Text = "No HostName Specified";
                ScanBranchStartScanBtn.Enabled = true;
                return;
            }
            if (ScanBranchUrlPatternTB.Text.Trim().Length == 0)
            {
                ScanBranchErrorTB.Text = "No Url Pattern Specified";
                ScanBranchStartScanBtn.Enabled = true;
                return;
            }
            if (!(ScanBranchHTTPCB.Checked || ScanBranchHTTPSCB.Checked))
            {
                ScanBranchErrorTB.Text = "Both HTTP & HTTPS are Unchecked. Select Atleast One";
                ScanBranchStartScanBtn.Enabled = true;
                return;
            }
            if(!(ScanBranchInjectAllCB.Checked || ScanBranchInjectURLCB.Checked || ScanBranchInjectQueryCB.Checked || ScanBranchInjectBodyCB.Checked || ScanBranchInjectCookieCB.Checked || ScanBranchInjectHeadersCB.Checked))
            {
                ScanBranchErrorTB.Text = "No Injection Points Selected";
                ScanBranchStartScanBtn.Enabled = true;
                return;
            }
            if (!(ScanBranchPickProxyLogCB.Checked || ScanBranchPickProbeLogCB.Checked))
            {
                ScanBranchErrorTB.Text = "Log source not selected. Select from Proxy Log/Probe Log";
                ScanBranchStartScanBtn.Enabled = true;
                return;
            }
            bool Checked = false;
            foreach (DataGridViewRow Row in ScanBranchScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Checked = true;
                    break;
                }
            }
            if(!Checked)
            {
                ScanBranchErrorTB.Text = "No Scan Plugins Selected";
                ScanBranchStartScanBtn.Enabled = true;
                return;
            }
            if (ScanBranchSessionPluginsCombo.Text.Length > 0)
            {
                if (ScanBranchSessionPluginsCombo.SelectedItem == null)
                {
                    ScanBranchErrorTB.Text = "Selected Session Plugin is Not Valid";
                    ScanBranchStartScanBtn.Enabled = true;
                    return;
                }
            }
            if (ScanBranchFormatPluginsCombo.Text.Length > 0)
            {
                if (ScanBranchFormatPluginsCombo.SelectedItem == null)
                {
                    ScanBranchErrorTB.Text = "Selected Format Plugin is Not Valid";
                    ScanBranchStartScanBtn.Enabled = true;
                    return;
                }
            }
            ScanBranch.ProxyLogIDs.Clear();
            ScanBranch.ProbeLogIDs.Clear();
            IronUI.UpdateScanBranchConfigFromUI();
            ScanBranchStatsPanel.Visible = true;
            ScanBranchProgressLbl.Text = "Selecting requests based on filter";
            if (ScanBranch.PickFromProxyLog)
            {
                if (IronUI.UI.ProxyLogGrid.Rows.Count == 0 && !ScanBranch.PickFromProbeLog)
                {
                    ScanBranchErrorTB.Text = "Proxy Log is Empty. Capture Some Traffic with the Proxy First";
                    ScanBranchStatsPanel.Visible = false;
                    ScanBranchStartScanBtn.Enabled = true;
                    return;
                }
                foreach (DataGridViewRow Row in IronUI.UI.ProxyLogGrid.Rows)
                {
                    try
                    {
                        if (ScanBranch.CanScan(Row, "Proxy"))
                        {
                            ScanBranch.ProxyLogIDs.Add((int)Row.Cells[0].Value);
                        }
                    }
                    catch (Exception Exp)
                    {
                        IronException.Report("ScanBranch Error reading ProxyLogGrid Message", Exp.Message, Exp.StackTrace);
                    }
                }
                if (ScanBranch.ProxyLogIDs.Count == 0 && !ScanBranch.PickFromProbeLog)
                {
                    ScanBranchErrorTB.Text = "No Requests were Selected. Try Changing the Filter or Capture More Traffic With the Proxy";
                    ScanBranchStatsPanel.Visible = false;
                    ScanBranchStartScanBtn.Enabled = true;
                    return;
                }
            }
            if (ScanBranch.PickFromProbeLog)
            {
                if (IronUI.UI.ProbeLogGrid.Rows.Count == 0 && !ScanBranch.PickFromProxyLog)
                {
                    ScanBranchErrorTB.Text = "Probe Log is Empty. Crawl a website to populate the Probe Log";
                    ScanBranchStatsPanel.Visible = false;
                    ScanBranchStartScanBtn.Enabled = true;
                    return;
                }
                foreach (DataGridViewRow Row in IronUI.UI.ProbeLogGrid.Rows)
                {
                    try
                    {
                        if (ScanBranch.CanScan(Row, "Probe"))
                        {
                            ScanBranch.ProbeLogIDs.Add((int)Row.Cells[0].Value);
                        }
                    }
                    catch (Exception Exp)
                    {
                        IronException.Report("ScanBranch Error reading ProbeLogGrid Message", Exp.Message, Exp.StackTrace);
                    }
                }
                if (ScanBranch.ProbeLogIDs.Count == 0 && !ScanBranch.PickFromProxyLog)
                {
                    ScanBranchErrorTB.Text = "No Requests were Selected. Try Changing the Filter or Crawl more of the site.";
                    ScanBranchStatsPanel.Visible = false;
                    ScanBranchStartScanBtn.Enabled = true;
                    return;
                }
            }
            if (ScanBranch.ProxyLogIDs.Count == 0 && ScanBranch.ProbeLogIDs.Count == 0)
            {
                ScanBranchErrorTB.Text = "No Requests were Selected. Try Changing the Filter or make sure there are Requests in the Proxy/Probe Logs";
                ScanBranchStatsPanel.Visible = false;
                ScanBranchStartScanBtn.Enabled = true;
                return;
            }
            ScanBranchProgressBar.Minimum = 0;
            ScanBranchProgressBar.Maximum = ScanBranch.ProxyLogIDs.Count + ScanBranch.ProbeLogIDs.Count;
            ScanBranchProgressBar.Step = 1;
            ScanBranchProgressBar.Value = 0;
            ScanBranchProgressLbl.Text = ScanBranch.ProxyLogIDs.Count.ToString() +  " Requests Selected";
            IronUI.UI.ASMainTabs.SelectTab(0);
            if (!IronUI.UI.main_tab.SelectedTab.Name.Equals("mt_auto")) IronUI.UI.main_tab.SelectTab("mt_auto");
            ScanBranch.Start();
        }

        private void ScanBranchFilterBtn_Click(object sender, EventArgs e)
        {
            if (ScanBranchFilterBtn.Text.Equals("Show Filter"))
            {
                ScanBranchForm.ActiveForm.Height = 485;
                ScanBranchConfigTabs.Visible = true;
                ScanBranchFilterBtn.Text = "Hide Filter";
            }
            else
            {
                ScanBranchForm.ActiveForm.Height = 230;
                ScanBranchConfigTabs.Visible = false;
                ScanBranchFilterBtn.Text = "Show Filter";
            }
        }

        private void ScanBranchInjectAllCB_Click(object sender, EventArgs e)
        {
            ScanBranchInjectURLCB.Checked = ScanBranchInjectAllCB.Checked;
            ScanBranchInjectURLCB.Checked = ScanBranchInjectAllCB.Checked;
            ScanBranchInjectQueryCB.Checked = ScanBranchInjectAllCB.Checked;
            ScanBranchInjectBodyCB.Checked = ScanBranchInjectAllCB.Checked;
            ScanBranchInjectCookieCB.Checked = ScanBranchInjectAllCB.Checked;
            ScanBranchInjectHeadersCB.Checked = ScanBranchInjectAllCB.Checked;
        }

        private void ScanBranchInjectURLCB_Click(object sender, EventArgs e)
        {
            if (!ScanBranchInjectURLCB.Checked)
            {
                ScanBranchInjectAllCB.Checked = false;
            }
        }

        private void ScanBranchInjectQueryCB_Click(object sender, EventArgs e)
        {
            if (!ScanBranchInjectQueryCB.Checked)
            {
                ScanBranchInjectAllCB.Checked = false;
            }
        }

        private void ScanBranchInjectBodyCB_Click(object sender, EventArgs e)
        {
            if (!ScanBranchInjectBodyCB.Checked)
            {
                ScanBranchInjectAllCB.Checked = false;
            }
        }

        private void ScanBranchInjectCookieCB_Click(object sender, EventArgs e)
        {
            if (!ScanBranchInjectCookieCB.Checked)
            {
                ScanBranchInjectAllCB.Checked = false;
            }
        }

        private void ScanBranchInjectHeadersCB_Click(object sender, EventArgs e)
        {
            if (!ScanBranchInjectHeadersCB.Checked)
            {
                ScanBranchInjectAllCB.Checked = false;
            }
        }

        private void ScanBranchScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanBranchScanPluginsGrid.SelectedCells.Count < 1 || ScanBranchScanPluginsGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if (ScanBranchScanPluginsGrid.SelectedRows[0].Index == 0)
            {
                bool AllValue = !(bool)ScanBranchScanPluginsGrid.SelectedCells[0].Value;
                ScanBranchScanPluginsGrid.SelectedCells[0].Value = AllValue;
                foreach (DataGridViewRow Row in ScanBranchScanPluginsGrid.Rows)
                {
                    if (Row.Index > 0)
                    {
                        Row.Cells[0].Value = AllValue;
                    }
                }
                return;
            }
            if ((bool)ScanBranchScanPluginsGrid.SelectedCells[0].Value)
            {
                ScanBranchScanPluginsGrid.SelectedCells[0].Value = false;
                ScanBranchScanPluginsGrid.Rows[0].SetValues(new object[] { false, "All" });
            }
            else
            {
                ScanBranchScanPluginsGrid.SelectedCells[0].Value = true;
            }
        }

        private void ScanBranchPickProxyLogCB_CheckedChanged(object sender, EventArgs e)
        {
            ScanBranch.PickFromProxyLog = ScanBranchPickProxyLogCB.Checked;
        }

        private void ScanBranchPickProbeLogCB_CheckedChanged(object sender, EventArgs e)
        {
            ScanBranch.PickFromProbeLog = ScanBranchPickProbeLogCB.Checked;
        }
    }
}
