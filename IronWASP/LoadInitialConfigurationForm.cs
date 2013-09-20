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
    public partial class LoadInitialConfigurationForm : Form
    {
        public LoadInitialConfigurationForm()
        {
            InitializeComponent();
        }

        private void LoadInitialConfigurationForm_Load(object sender, EventArgs e)
        {
            ProxyListenPortTB.Text = IronProxy.Port.ToString();
            
            if (IronProxy.LoopBackOnly)
            {
                AcceptRemoteYesRB.Checked = false;
                AcceptRemoteNoRB.Checked = true;
            }
            else
            {
                AcceptRemoteYesRB.Checked = true;
                AcceptRemoteNoRB.Checked = false;
            }

            if (IronProxy.UseSystemProxyAsUpStreamProxy)
            {
                UseBrowserUpstreamRB.Checked = true;
                DontUseUpstreamProxyRB.Checked = false;
                UseCustomUpstreamProxyRB.Checked = false;
            }
            else if (IronProxy.UseUpstreamProxy)
            {
                UseBrowserUpstreamRB.Checked = false;
                DontUseUpstreamProxyRB.Checked = false;
                UseCustomUpstreamProxyRB.Checked = true;
            }
            else
            {
                UseBrowserUpstreamRB.Checked = false;
                DontUseUpstreamProxyRB.Checked = true;
                UseCustomUpstreamProxyRB.Checked = false;
            }
            UpstreamProxyIPTB.Text = IronProxy.UpstreamProxyIP;
            UpstreamProxyPortTB.Text = IronProxy.UpstreamProxyPort.ToString();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            IronUI.UI.CanShutdown = true;
            this.Close();
        }

        private void ProxyListenPortTB_TextChanged(object sender, EventArgs e)
        {
            int PortNumber = 0;
            if (IronProxy.ValidProxyPort(ProxyListenPortTB.Text))
            {
                PortNumber = Int32.Parse(ProxyListenPortTB.Text);
                ProxyListenPortTB.BackColor = Color.White;
            }
            else
            {
                ProxyListenPortTB.BackColor = Color.Red;
            }
        }

        private void UpstreamProxyPortTB_TextChanged(object sender, EventArgs e)
        {
            //IronProxy.UseUpstreamProxy = false;
            CheckUpstreamProxyPort();
        }

        void CheckUpstreamProxyPort()
        {
            int PortNumber = 0;
            if (Int32.TryParse(UpstreamProxyPortTB.Text, out PortNumber))
            {
                if (PortNumber > 0 && PortNumber < 65536)
                {
                    UpstreamProxyPortTB.BackColor = Color.White;
                }
                else
                {
                    UpstreamProxyPortTB.BackColor = Color.Red;
                }
            }
            else
            {
                UpstreamProxyPortTB.BackColor = Color.Red;
            }
        }

        private void UpstreamProxyIPTB_TextChanged(object sender, EventArgs e)
        {
            //IronProxy.UseUpstreamProxy = false;
            CheckUpstreamProxyIp();
        }

        void CheckUpstreamProxyIp()
        {
            if (UpstreamProxyIPTB.Text.Length > 0)
            {
                UpstreamProxyIPTB.BackColor = Color.White;
            }
            else
            {
                UpstreamProxyIPTB.BackColor = Color.Red;
            }
        }

        private void UseCustomUpstreamProxyRB_CheckedChanged(object sender, EventArgs e)
        {
            if (UseCustomUpstreamProxyRB.Checked)
            {
                UpstreamProxyIPTB.Enabled = true;
                UpstreamProxyPortTB.Enabled = true;
                CheckUpstreamProxyIp();
                CheckUpstreamProxyPort();
            }
            else
            {
                UpstreamProxyIPTB.Enabled = false;
                UpstreamProxyIPTB.BackColor = Color.White;
                UpstreamProxyPortTB.Enabled = false;
                UpstreamProxyPortTB.BackColor = Color.White;
            }
        }

        void ShowMessage(string Msg, bool Error)
        {
            MessageTB.Text = Msg;
            if (Error)
            {
                MessageTB.ForeColor = Color.Red;
            }
            else
            {
                MessageTB.ForeColor = Color.Black;
            }
            if (Msg.Length > 0)
            {
                MessageTB.Visible = true;
            }
            else
            {
                MessageTB.Visible = false;
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            int ProxyPortNumber = 0;
            if (Int32.TryParse(ProxyListenPortTB.Text.Trim(), out ProxyPortNumber))
            {
                if (!(ProxyPortNumber > 0 && ProxyPortNumber < 65536))
                {
                    ShowMessage("Invalid Proxy port number. Enter a value between 1 and 65535", true);
                    return;
                }
            }
            else
            {
                ShowMessage("Invalid Proxy port number. Enter a value between 1 and 65535", true);
                return;
            }
            IronProxy.Port = ProxyPortNumber;


            string UpstreamProxyIP = "";
            int UpstreamProxyPort = 0;
            

            if (AcceptRemoteYesRB.Checked)
            {
                IronProxy.LoopBackOnly = false;
            }
            else
            {
                IronProxy.LoopBackOnly = true;
            }

            if (SetAsSystemProxyYesRB.Checked)
            {
                IronProxy.SystemProxy = true;
            }
            else
            {
                IronProxy.SystemProxy = false;
            }

            if (UseBrowserUpstreamRB.Checked)
            {
                IronProxy.UseSystemProxyAsUpStreamProxy = true;
                IronProxy.UseUpstreamProxy = false;
            }
            else if(UseCustomUpstreamProxyRB.Checked)
            {
                IronProxy.UseUpstreamProxy = true;
                IronProxy.UseSystemProxyAsUpStreamProxy = false;

                if (UpstreamProxyIPTB.Text.Trim().Length > 0)
                {
                    UpstreamProxyIP = UpstreamProxyIPTB.Text.Trim();
                }
                else
                {
                    ShowMessage("The IP address/hostname of the Upstream proxy server is empty. Please enter an IP address or unselect the option to use an custom upstream proxy.", true);
                    return;
                }
                if (Int32.TryParse(UpstreamProxyPortTB.Text, out UpstreamProxyPort))
                {
                    if (!(UpstreamProxyPort > 0 && UpstreamProxyPort < 65536))
                    {
                        ShowMessage("Invalid Upstream Proxy port number. Enter a value between 1 and 65535", true);
                        return;
                    }
                }
                else
                {
                    ShowMessage("Invalid Upstream Proxy port number. Enter a value between 1 and 65535", true);
                    return;
                }
                IronProxy.UpstreamProxyIP = UpstreamProxyIP;
                IronProxy.UpstreamProxyPort = UpstreamProxyPort;
            }
            else
            {
                IronProxy.UseUpstreamProxy = false;
                IronProxy.UseSystemProxyAsUpStreamProxy = false;
            }
            if (IronProxy.Start())
            {
                IronDB.StoreProxyConfig();
                IronDB.StoreUpstreamProxyConfig();
                IronUI.UpdateProxyStatusInConfigPanel(true);
                IronUI.SetProxyLoopbackLabel();
                IronUI.SetProxyPortLabel();
                this.Close();
            }
            else
            {
                if (ProxyPortNumber > 1024)
                {
                    ShowMessage(string.Format("Unable to start the IronWASP proxy on port {0}. \r\nThis could be happening because there is another service listening on the same port. \r\nPlease choose some other port and try again.", ProxyPortNumber), true);
                }
                else
                {
                    ShowMessage(string.Format("Unable to start the IronWASP proxy on port {0}. \r\nThis could be happening because IronWASP does not have administrative privileges. \r\nTo listen on ports from 1 to 1024 IronWASP must be started with Administrative privileges. \r\nYou can either do that or pick a port above 1024 for the proxy. \r\nIf you still have this problem then it could be because there is another service listening on the same port. Please choose some other port and try again.", ProxyPortNumber), true);
                }
            }
        }

        private void ShowImportCertFormLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImportCertForm ICF = new ImportCertForm();
            ICF.Show();
        }
    }
}
