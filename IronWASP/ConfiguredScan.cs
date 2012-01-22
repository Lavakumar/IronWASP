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
    public partial class ConfiguredScan : Form
    {
        public ConfiguredScan()
        {
            InitializeComponent();
        }

        private void ConfigureScanStartScanBtn_Click(object sender, EventArgs e)
        {
            ScanManager.Stop(true);
            ScanManager.PrimaryHost = ConfigureScanHostNameTB.Text;
            ScanManager.BaseUrl = ConfigureScanBaseUrlTB.Text;
            ScanManager.StartingUrl = ConfigureScanStartingUrlTB.Text;
            ScanManager.Mode = ScanMode.UserConfigured;
            ScanManager.PerformDirAndFileGuessing = ConfigureScanDirAndFileGuessingCB.Checked;
            ScanManager.HTTP = ConfigureScanHTTPCB.Checked;
            ScanManager.HTTPS = ConfigureScanHTTPSCB.Checked;
            ScanManager.HostsToInclude = new List<string>(ConfigureScanHostsToIncludeTB.Text.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries));
            ScanManager.UrlsToAvoid = new List<string>(ConfigureScanUrlToAvoidTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            ScanManager.IncludeSubDomains = ConfigureScanIncludeSubDomainsCB.Checked;
            ScanManager.CrawlAndScan = ConfigureScanCrawlAndScanRB.Checked;
            if(IronUI.CSF.ConfigureScanSessionPluginsCombo.Text.Length > 0)
            {
                if (SessionPlugin.List().Contains(IronUI.CSF.ConfigureScanSessionPluginsCombo.Text))
                {
                    ScanManager.SessionHandler = SessionPlugin.Get(IronUI.CSF.ConfigureScanSessionPluginsCombo.Text);
                }
                else
                {
                    IronUI.ShowConfiguredScanMessage("Non-existent Session Plugin Selected", true);
                    return;
                }
            }
            string Message = CheckInput();
            if (Message.Length > 0)
            {
                IronUI.ShowConfiguredScanMessage(Message, true);
                return;
            }
            ScanManager.StartScan();
            IronUI.CSF.Close();
            IronUI.UpdateConsoleControlsStatus(true);
        }

        private string CheckInput()
        {
            if (ScanManager.PrimaryHost.Length == 0) return "Hostname cannot be empty";
            if (!ScanManager.BaseUrl.StartsWith("/")) return "Invalid base URL";
            if (!ScanManager.StartingUrl.StartsWith("/")) return "Invalid starting URL";
            if (!(ScanManager.HTTP || ScanManager.HTTPS)) return "Neither HTTP/HTTPS were selected";
            return "";
        }

        private void ConfigureScanCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
