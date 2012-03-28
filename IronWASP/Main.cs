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
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Hosting;
using IronPython;
using IronPython.Hosting;
using IronPython.Modules;
using IronPython.Runtime;
using IronPython.Runtime.Exceptions;
using IronRuby;
using IronRuby.Hosting;
using IronRuby.Runtime;
using IronRuby.StandardLibrary;

namespace IronWASP
{
    public partial class Main : Form
    {
        public Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Iron_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(IronUI_ThreadException);
            InitializeComponent();
        }


        private static void IronUI_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            DialogResult Result = DialogResult.No;
            try
            {
                try { File.Delete(Config.RootDir + "\\ironwasp_error.txt"); }
                catch { }
                try { File.WriteAllText(Config.RootDir + "\\ironwasp_error.txt", e.Exception.Message + "\r\nStackTrace:\r\n" + e.Exception.StackTrace); }
                catch { }

                Result = MessageBox.Show("Oops, Iron encountered an unexpected error and must close.\r\n\r\nYour project details are stored in the folder - " + IronDB.LogPath + "\r\n\r\nYou can choose to continue or restart the application(recommended).\r\n\r\nIMPORTANT:\r\nPlease help avoid this from occuring again by sending the following details(screenshot) to lava@ironwasp.org.\r\nAlternatively you can mail the file - " + Config.RootDir + "\\ironwasp_error.txt\r\n\r\nMessage: " + e.Exception.Message + "\r\nStackTrace:\r\n" + e.Exception.StackTrace, "Unexpected Error - Do you wish to continue running?(not recommended)", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            finally
            {
                if (Result == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void Iron_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception Exp = (Exception)e.ExceptionObject;
                try { File.Delete(Config.RootDir + "\\ironwasp_error.txt"); }
                catch { }
                try { File.WriteAllText(Config.RootDir + "\\ironwasp_error.txt", Exp.Message + "\r\nStackTrace:\r\n" + Exp.StackTrace); }
                catch { }
                IronException.Report("Unhandled Exception", Exp.Message, Exp.StackTrace);
                MessageBox.Show("Oops, Iron encountered an unexpected error and must close.\r\n\r\nYour project details are stored in the folder - " + IronDB.LogPath + "\r\n\r\nIMPORTANT:\r\nPlease help avoid this from occuring again by sending the following details(screenshot) to lava@ironwasp.org.\r\nAlternatively you can mail the file - " + Config.RootDir + "\\ironwasp_error.txt\r\n\r\nMessage: " + Exp.Message + "\r\nStackTrace:\r\n" + Exp.StackTrace, "Unexpected Error Occurred");
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        internal bool CanShutdown = false;

        private void Main_Load(object sender, EventArgs e)
        {          
            IronUI.LF = new LoadForm();
            Thread T = new Thread(new ThreadStart(Splash));
            T.Start();
            IronUI.SetUI(this);

            IronUI.ShowLoadMessage("Loading.....");
            try
            {
                IronUpdater.Start();

                //Set the ID column as int for proper sorting
                ProxyLogGrid.Columns[0].ValueType = typeof(System.Int32);

                //Initialise default nodes in the tree
                IronUI.BuildIronTree();
            }
            catch
            {

            }


            IronUI.ShowLoadMessage("Creating Project Database....");
            try
            {
                Config.SetRootDir();
                IronDB.InitialiseLogDB();
            }
            catch(Exception Exp)
            {
                IronUI.ShowLoadMessage("Error Creating Project Database - " + Exp.Message);
            }

            IronUI.ShowLoadMessage("Reading Stored Configuration Information....");
            try
            {
                IronDB.UpdateConfigFromDB();
            }
            catch (Exception Exp)
            {
                IronUI.ShowLoadMessage("Error Reading Stored Configuration Information - " + Exp.Message);
            }

            IronUI.ShowLoadMessage("Applying Previous Configuration....");
            try
            {
                IronUI.UpdateUIFromConfig();
                IronJint.ShowDefaultTaintConfig();
            }
            catch (Exception Exp)
            {
                IronUI.ShowLoadMessage("Error Applying Previous Configuration - " + Exp.Message);
            }

            IronUI.ShowLoadMessage("Creating API Documentation Trees....");
            try
            {
                APIDoc.Initialise();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error creating API Docs", Exp.Message, Exp.StackTrace);
            }
            //Initialise the Scripting Engines and compile the plug-ins
            IronUI.ShowLoadMessage("Loading All Plugins....");
            try
            {
                PluginStore.StartUp = true;
                PluginStore.InitialiseAllPlugins();
                PluginStore.StartUp = false;
                
            }
            catch (Exception Exp)
            {
                PluginStore.StartUp = false;
                IronException.Report("Error initialising Plugins", Exp.Message, Exp.StackTrace);
            }
            
            IronUI.ShowLoadMessage("Starting Internal Analyzers....");
            try
            {
                PassiveChecker.Start();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Internal Analyzers", Exp.Message, Exp.StackTrace);
            }


            IronUI.ShowLoadMessage("Preparing the Scripting Shell....");
            try
            {
                IronScripting.InitialiseScriptingEnvironment();
                IronUI.InitialiseAllScriptEditors();
            }
            catch (Exception Exp)
            {
                IronUI.ShowLoadMessage("Error Preparing the Scripting Shell - " + Exp.Message);
                IronException.Report("Error Preparing the Scripting Shell", Exp.Message, Exp.StackTrace);
            }


            IronUI.ShowLoadMessage("Starting the Proxy");
            IronProxy.Start();
            try
            {
                CheckUpdate.CheckForUpdates();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Starting New Version Check", Exp.Message, Exp.StackTrace);
            }

            IronUI.ShowLoadMessage("Done!");
            IronUI.ShowLoadMessage("0");
            this.Activate();
            PluginEditorInTE.Document.ReadOnly = true;
        }

        static void Splash()
        {
            IronUI.LF.LoadLogoPB.Select();
            IronUI.LF.ShowDialog();
        }

        private void ProxySendBtn_Click(object sender, EventArgs e)
        {
            if (IronProxy.CurrentSession == null) return;
            if (IronProxy.CurrentSession.FiddlerSession == null) return;
            if (IronProxy.CurrentSession.FiddlerSession.state == Fiddler.SessionStates.HandTamperRequest)
            {
                try
                {
                    IronUI.ResetProxyException();
                    IronUI.HandleAnyChangesInRequest();
                }
                catch(Exception Exp)
                {
                    IronUI.ShowProxyException(Exp.Message);
                    return;
                }
            }
            else if (IronProxy.CurrentSession.FiddlerSession.state == Fiddler.SessionStates.HandTamperResponse)
            {
                try
                {
                    IronUI.ResetProxyException();
                    IronUI.HandleAnyChangesInResponse();
                }
                catch(Exception Exp)
                {
                    IronUI.ShowProxyException(Exp.Message);
                    return;
                }
            }
            else
            {
                return;
            }
            IronProxy.ForwardInterceptedMessage();
            string SessionID = "";
            try
            {
                lock (IronProxy.SessionsQ)
                {

                    SessionID = IronProxy.SessionsQ.Dequeue();
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
            catch(Exception Exp)
            {
                IronException.Report("Error Dequeing from Proxy Interception Queue", Exp);
            }
            
            if (SessionID.Length > 0)
            {
                Session IntSession;
                lock(IronProxy.InterceptedSessions)
                {
                    IntSession = IronProxy.InterceptedSessions[SessionID];
                }
                if (IntSession != null) IronUI.FillInterceptorTab(IntSession);
            }
        }

        private void InterceptRequestCB_CheckedChanged(object sender, EventArgs e)
        {
            if(IronProxy.InterceptRequest)
            {
                IronProxy.InterceptRequest = false;
                if (IronProxy.CurrentSession != null)
                {
                    if (IronProxy.CurrentSession.FiddlerSession.state == Fiddler.SessionStates.HandTamperRequest)
                    {
                        try
                        {
                            IronProxy.ForwardInterceptedMessage();
                        }
                        catch (Exception Exp)
                        {
                            IronException.Report("Error forwarding Request from Proxy", Exp.Message, Exp.StackTrace);
                        }
                    }
                }
                IronProxy.ClearRequestQueue();
            }
            else
            {
                IronProxy.InterceptRequest = true;
            }
        }

        private void InterceptResponseCB_CheckedChanged(object sender, EventArgs e)
        {
            if (IronProxy.InterceptResponse)
            {
                IronProxy.InterceptResponse = false;
                if (IronProxy.CurrentSession != null)
                {
                    if (IronProxy.CurrentSession.FiddlerSession.state == Fiddler.SessionStates.HandTamperResponse)
                    {
                        try
                        {
                            IronProxy.ForwardInterceptedMessage();
                        }
                        catch (Exception Exp)
                        {
                            IronException.Report("Error forwarding Response from Proxy", Exp.Message, Exp.StackTrace);
                        }
                    }
                }
                IronProxy.ClearResponseQueue();
            }
            else
            {
                IronProxy.InterceptResponse = true;
            }
        }

        private void InteractiveShellIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.InteractiveShellIn.ReadOnly)
            {
                return;
            }
            if (e.KeyCode == Keys.Up)
            {
                string CommandFromHistory = IronScripting.GetPreviousCommandFromHistory();
                if (CommandFromHistory.Length > 0)
                {
                    this.InteractiveShellIn.Text = CommandFromHistory;
                }
                else
                {
                    this.InteractiveShellIn.Text = this.InteractiveShellIn.Text.Replace("\r\n", "");
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                string CommandFromHistory = IronScripting.GetNextCommandFromHistory();
                if (CommandFromHistory.Length > 0)
                {
                    this.InteractiveShellIn.Text = CommandFromHistory;
                }
                else
                {
                    this.InteractiveShellIn.Text = this.InteractiveShellIn.Text.Replace("\r\n", "");
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                IronUI.FreezeInteractiveShellUI();
                string Command = this.InteractiveShellIn.Text.Replace("\r\n", "");
                this.InteractiveShellIn.Text = "";
                if (Command.Length > 0)
                {
                    IronScripting.AddCommandToHistory(Command);
                }
                if (IronScripting.MoreExpected)
                {
                    IronUI.UpdateInteractiveShellOut(Command + Environment.NewLine);
                }
                else
                {
                    IronUI.UpdateInteractiveShellOut(IronScripting.ShellPrompt + Command + Environment.NewLine);
                    if (Command.Length == 0)
                    {
                        IronUI.ActivateinteractiveShellUI();
                        return;
                    }
                }
                try
                {
                    IronScripting.QueueInteractiveShellInputForExecution(Command);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error executing command in Scripting Shell", Exp.Message, Exp.StackTrace);
                    IronUI.ActivateinteractiveShellUI();
                }                
            }
        }

        private void InteractiveShellPythonRB_CheckedChanged(object sender, EventArgs e)
        {
            IronUI.UpdateShellInPrompt(">>>>");
            if (InteractiveShellPythonRB.Checked)
            {
                IronScripting.ChangeLanguageToPython();
            }
            Directory.SetCurrentDirectory(Config.RootDir);
            MultiLineShellInTE.SetHighlighting("Python");
        }

        private void InteractiveShellRubyRB_CheckedChanged(object sender, EventArgs e)
        {
            IronUI.UpdateShellInPrompt("irb>");
            if (InteractiveShellRubyRB.Checked)
            {
                IronScripting.ChangeLanguageToRuby();
            }
            Directory.SetCurrentDirectory(Config.RootDir);
            MultiLineShellInTE.SetHighlighting("Ruby");
        }

        private void LogMenu_Opening(object sender, CancelEventArgs e)
        {
            IronLog.SourceControl = LogMenu.SourceControl.Name;
            bool RowsSelected = true;
            bool ResponseAvailable = true;
            switch (IronLog.SourceControl)
            {
                case("ProxyLogGrid"):
                    if (ProxyLogGrid.SelectedCells.Count < 1 || ProxyLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (ProxyLogGrid.SelectedCells[7].Value == null) ResponseAvailable = false;
                    }
                    break;
                case ("ShellLogGrid"):
                    if (ShellLogGrid.SelectedCells.Count < 1 || ShellLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (ShellLogGrid.SelectedCells[7].Value == null) ResponseAvailable = false;
                    }
                    break;
                case ("TestLogGrid"):
                    if (TestLogGrid.SelectedCells.Count < 1 || TestLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (TestLogGrid.SelectedCells[7].Value == null) ResponseAvailable = false;
                    }
                    break;
                case ("ProbeLogGrid"):
                    if (ProbeLogGrid.SelectedCells.Count < 1 || ProbeLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (ProbeLogGrid.SelectedCells[7].Value == null) ResponseAvailable = false;
                    }
                    break;
                case ("ScanLogGrid"):
                    if (ScanLogGrid.SelectedCells.Count < 1 || ScanLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (ScanLogGrid.SelectedCells[8].Value == null) ResponseAvailable = false;
                    }
                    break;
                case ("SiteMapLogGrid"):
                    if (SiteMapLogGrid.SelectedCells.Count < 1 || SiteMapLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (SiteMapLogGrid.SelectedCells[8].Value == null) ResponseAvailable = false;
                    }
                    break;
                case ("ResultsTriggersGrid"):
                    if (ResultsTriggersGrid.SelectedCells.Count < 1 || ResultsTriggersGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    break;
                case ("TestGroupLogGrid"):
                    if (TestGroupLogGrid.SelectedCells.Count < 1 || TestGroupLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (TestGroupLogGrid.SelectedCells[5].Value == null) ResponseAvailable = false;
                    }
                    break;
                case("LogOptionsBtn"):
                    if (IronLog.CurrentSession == null)
                        RowsSelected = false;
                    else if (IronLog.CurrentSession.Response == null)
                        ResponseAvailable = false;
                    break;
                case("ProxyOptionsBtn"):
                    if (IronProxy.CurrentSession == null)
                        RowsSelected = false;
                    else if (IronProxy.CurrentSession.Response == null)
                        ResponseAvailable = false;
                    break;
            }

            this.SelectForManualTestingToolStripMenuItem.Enabled = RowsSelected;
            this.SelectForAutomatedScanningToolStripMenuItem.Enabled = RowsSelected;
            this.CopyRequestToolStripMenuItem.Enabled = RowsSelected;
            this.CopyResponseToolStripMenuItem.Enabled = RowsSelected;
            this.SelectResponseForJavaScriptTestingToolStripMenuItem.Enabled = RowsSelected && ResponseAvailable && JSTaintTraceControlBtn.Text.Equals("Start Taint Trace");
        }

        private void MTSendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                IronUI.ResetMTExceptionFields();
                IronUI.HandleAnyChangesInMTRequest();
            }
            catch(Exception Exp)
            {
                IronUI.ShowMTException(Exp.Message);
                return;
            }
            if (ManualTesting.CurrentRequest == null)
            {
                IronUI.ShowMTException("Invalid Request");
                return;
            }
            try
            {
                IronUI.ResetMTExceptionFields();
                ManualTesting.ResetChangedStatus();
                ManualTesting.CurrentRequest.SSL = MTIsSSLCB.Checked;
                ManualTesting.SendRequest();
                IronUI.StartMTSend(ManualTesting.CurrentRequestID);
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to Send 'Manual Testing' Request", Exp.Message, Exp.StackTrace);
                IronUI.ShowMTException("Error sending Request");
                IronUI.EndMTSend();
            }
        }


        private void MTLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TestLogGrid.SelectedCells.Count < 1 || TestLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Test, TestLogGrid.SelectedCells[0].Value.ToString());
        }

        private void LogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProxyLogGrid.SelectedCells.Count < 1 || ProxyLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Proxy, Int32.Parse(ProxyLogGrid.SelectedCells[0].Value.ToString()));
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CanShutdown)
            {
                if (IronUI.IsCloseFormOpen())
                {
                    IronUI.CF.Activate();
                }
                else
                {
                    IronUI.CF = new CloseForm();
                    IronUI.CF.Show();   
                }
                e.Cancel = true;
            }
        }

        internal void ShutDown()
        {
            CanShutdown = true;
            try
            {
                ScanManager.Stop();
            }
            catch { }
            try
            {
                PassiveChecker.Stop();
            }
            catch { }
            //try
            //{
            //    Analyzer.Stop();
            //}
            //catch { }
            try
            {
                IronScripting.StopExecutor();
            }
            catch { }
            try
            {
                Fiddler.FiddlerApplication.Shutdown();
            }
            catch { }
            try
            {
                IronUpdater.Stop();
            }
            catch { }
            foreach (int ScanID in Scanner.ScanThreads.Keys)
            {
                try
                {
                    Scanner.ScanThreads[ScanID].Abort();
                }
                catch { }
            }
            try
            {
                CheckUpdate.StopUpdateCheck();
            }catch{}
        }

        private void MTIsSSLCB_CheckedChanged(object sender, EventArgs e)
        {
            ManualTesting.CurrentRequestIsSSL = this.MTIsSSLCB.Checked;
        }
        
        private void ScriptingShellPythonAPITree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ShellAPIDetailsRTB.Rtf = APIDoc.GetPyDecription(e.Node);
        }

        private void PluginEditorPythonAPITree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.PluginEditorAPIDetailsRTB.Rtf = APIDoc.GetPyDecription(e.Node);
        }

        private void IronTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            if ((e.Node.Level > 5) || (e.Node.Level == 5 && (e.Node.Parent.Parent.Parent.Parent.Index == 4)) || (e.Node.Level == 4 && (e.Node.Parent.Parent.Parent.Index == 4)) || (e.Node.Level == 3 && (e.Node.Parent.Parent.Index == 4)) || (e.Node.Level == 2 && (e.Node.Parent.Index == 4)))
            {
                List<string> UrlPaths = new List<string>();
                string Query = "";
                TreeNode SiteMapNode = e.Node;
                if (SiteMapNode.Text.StartsWith("?"))
                {
                    Query = SiteMapNode.Text;
                    SiteMapNode = SiteMapNode.Parent;
                }
                while(SiteMapNode.Level > 2)
                {
                    UrlPaths.Add(SiteMapNode.Text);
                    SiteMapNode = SiteMapNode.Parent;
                }
                UrlPaths.Reverse();
                StringBuilder UrlPathBuilder = new StringBuilder();
                foreach (string Path in UrlPaths)
                {
                    UrlPathBuilder.Append("/"); UrlPathBuilder.Append(Path);
                }
                string Host = SiteMapNode.Text;
                string Url = UrlPathBuilder.ToString() + Query;
                if (Url == "//") Url = "/";
                IronUI.UpdateResultsTab(Host, Url);
                return;
            }
            if (e.Node.Level == 4 && (e.Node.Parent.Parent.Parent.Index == 1 || e.Node.Parent.Parent.Parent.Index == 2))
            {
                PluginResult.CurrentPluginResult = IronDB.GetPluginResultFromDB(Int32.Parse(e.Node.Name));
                IronUI.UpdateResultsTab(PluginResult.CurrentPluginResult);
            }
            else if (e.Node.Level == 5 && e.Node.Parent.Parent.Parent.Parent.Index == 0)
            {
                PluginResult.CurrentPluginResult = IronDB.GetPluginResultFromDB(Int32.Parse(e.Node.Name));
                IronUI.UpdateResultsTab(PluginResult.CurrentPluginResult);
            }
            else if (e.Node.Level == 2 && e.Node.Parent.Index == 3)
            {
                IronException IrEx = IronDB.GetException(Int32.Parse(e.Node.Name));
                IronUI.UpdateResultsTab(IrEx);
            }
            this.SiteMapLogGrid.Rows.Clear();
        }

        private void InteractiveShellCtrlCBtn_Click(object sender, EventArgs e)
        {
            IronScripting.StopExecutor();
            IronUI.UpdateShellInPrompt(IronScripting.ShellPrompt);
            IronUI.ActivateinteractiveShellUI();
            if (ScriptingShellTabs.SelectedIndex == 1)
            {
                MultiLineShellExecuteBtn.Visible = true;
            }
            else
            {
                MultiLineShellExecuteBtn.Visible = false;
            }
        }

        private void ShellLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ShellLogGrid.SelectedCells.Count < 1 || ShellLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Shell, ShellLogGrid.SelectedCells[0].Value.ToString());
            return;
        }

        private void ScriptingShellRubyAPITree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ShellAPIDetailsRTB.Rtf = APIDoc.GetRbDecription(e.Node);
        }

        private void PluginEditorRubyAPITree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.PluginEditorAPIDetailsRTB.Rtf = APIDoc.GetRbDecription(e.Node);
        }

        private void CustomSendPythonRB_CheckedChanged(object sender, EventArgs e)
        {
            this.CustomSendTopRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue255;\red25\green25\blue112;} \cf1 def \cf0 \cf2 \b1 ScriptedSend \b0 \cf0 (req):";
            this.CustomSendBottomRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue128;} \cf1     return \cf0 res";
            this.CustomSendActivateCB.Checked = false;
            this.MTScriptedSendBtn.Enabled = false;
            Directory.SetCurrentDirectory(Config.RootDir);
            CustomSendTE.SetHighlighting("Python");
        }

        private void CustomSendRubyRB_CheckedChanged(object sender, EventArgs e)
        {
            this.CustomSendTopRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue255;\red25\green25\blue112;} \cf1 def \cf0 \cf2 \b1 scripted_send \b0 \cf0 (req)";
            this.CustomSendBottomRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue128;\red0\green0\blue255;} \cf1     return \cf0 res \par \cf2 end \cf0";
            this.CustomSendActivateCB.Checked = false;
            this.MTScriptedSendBtn.Enabled = false;
            Directory.SetCurrentDirectory(Config.RootDir);
            CustomSendTE.SetHighlighting("Ruby");
        }

        private void MTScriptedSendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                IronUI.ResetMTExceptionFields();
                IronUI.HandleAnyChangesInMTRequest();
            }
            catch (Exception Exp)
            {
                IronUI.ShowMTException(Exp.Message);
                return;
            }
            if (ManualTesting.CurrentRequest == null)
            {
                IronUI.ShowMTException("Invalid Request");
                return;
            }
            try
            {
                IronUI.ResetMTExceptionFields();
                ManualTesting.ResetChangedStatus();
                ManualTesting.CurrentRequest.SSL = MTIsSSLCB.Checked;
                ManualTesting.ScriptedSend();
                IronUI.StartMTSend(ManualTesting.CurrentRequestID);
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to Send 'Manual Testing' Request", Exp.Message, Exp.StackTrace);
                IronUI.ShowMTException("Error sending Request");
                IronUI.EndMTSend();
            }
        }

        internal void CustomSendTE_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Left || e.KeyData == Keys.Right || e.KeyData == Keys.PageUp || e.KeyData == Keys.PageDown || e.KeyData == Keys.Home || e.KeyData == Keys.End || e.KeyData == Keys.CapsLock || e.KeyData == Keys.LWin || e.KeyData == Keys.RWin)
            {
                return;
            }
            if (this.CustomSendActivateCB.Checked)
            {
                this.CustomSendActivateCB.Checked = false;
            }
            IronUI.ResetScriptedSendScriptExceptionFields();
            if(this.MTScriptedSendBtn.Enabled)
            {
                this.MTScriptedSendBtn.Enabled = false;
            }
            if (ManualTesting.ScriptedSendEnabled)
            {
                ManualTesting.ScriptedSendEnabled = false;
            }
        }

        private void MTStoredRequestBtn_Click(object sender, EventArgs e)
        {
            if (ManualTesting.HasStoredRequest())
            {
                try
                {
                    IronUI.UpdateManualTestingRequest(ManualTesting.GetStoredRequest());
                }
                catch(Exception Exp)
                {
                    IronException.Report("Error displaying 'Stored Request'", Exp.Message, Exp.StackTrace);
                    IronUI.ShowMTException("Unable to display 'Stored Request'");
                }
            }
            this.MTStoredRequestBtn.Enabled = ManualTesting.HasStoredRequest();
        }

        private void SelectForAutomatedScanningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForScanning(GetSource(), GetID());
        }

        private RequestSource GetSource()
        {
            switch (IronLog.SourceControl)
            {
                case ("ProxyLogGrid"):
                    return RequestSource.Proxy;
                case ("ShellLogGrid"):
                    return RequestSource.Shell;
                case ("TestLogGrid"):
                    return RequestSource.Test;
                case ("ProbeLogGrid"):
                    return RequestSource.Probe;
                case ("ScanLogGrid"):
                    return RequestSource.Scan;
                case ("SiteMapLogGrid"):
                    string Source = SiteMapLogGrid.SelectedCells[1].Value.ToString();
                    switch (Source)
                    {
                        case "Proxy":
                            return RequestSource.Proxy;
                        case "Test":
                            return RequestSource.Test;
                        case "Shell":
                            return RequestSource.Shell;
                        case "Probe":
                            return RequestSource.Probe;
                        case "Scan":
                            return RequestSource.Scan;
                    }
                    break;
                case ("ResultsTriggersGrid"):
                        return RequestSource.Trigger;
                case ("TestGroupLogGrid"):
                        return RequestSource.TestGroup;
                case("LogOptionsBtn"):
                        return RequestSource.SelectedLogEntry;
                case("ProxyOptionsBtn"):
                        return RequestSource.CurrentProxyInterception;
            }
            return RequestSource.Proxy;
        }

        private string GetID()
        {
            switch (IronLog.SourceControl)
            {
                case ("ProxyLogGrid"):
                    return ProxyLogGrid.SelectedCells[0].Value.ToString();
                case ("ShellLogGrid"):
                    return ShellLogGrid.SelectedCells[0].Value.ToString();
                case ("TestLogGrid"):
                    return TestLogGrid.SelectedCells[0].Value.ToString();
                case ("ProbeLogGrid"):
                    return ProbeLogGrid.SelectedCells[0].Value.ToString();
                case ("ScanLogGrid"):
                    return ScanLogGrid.SelectedCells[0].Value.ToString();
                case ("SiteMapLogGrid"):
                    return SiteMapLogGrid.SelectedCells[0].Value.ToString();
                case ("ResultsTriggersGrid"):
                    return ResultsTriggersGrid.SelectedCells[0].Value.ToString();
                case ("TestGroupLogGrid"):
                    return TestGroupLogGrid.SelectedCells[0].Value.ToString();
                case("LogOptionsBtn"):
                case ("ProxyOptionsBtn"):
                    return "0";
            }
            return "";
        }

        private void ASQueueGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ASQueueGrid.SelectedCells.Count < 1 || ASQueueGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            this.ASStartScanBtn.Enabled = false;
            Scanner.ResetChangedStatus();
            IronUI.ResetConfigureScanFields();
            string ScanStatus = ASQueueGrid.SelectedCells[1].Value.ToString();
            try
            {
                Scanner.CurrentScanID = Int32.Parse(ASQueueGrid.SelectedCells[0].Value.ToString());
                Scanner.CurrentScanner = IronDB.GetScannerFromDB(Scanner.CurrentScanID);
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to load Request from Scan Queue DB", Exp.Message, Exp.StackTrace);
                IronUI.ShowConfigureScanException("Unable to load Request");
                return;
            }
            Scanner.CurrentScanner.OriginalRequest.Source = RequestSource.Scan;
            try
            {
                IronUI.FillConfigureScanFullFields(Scanner.CurrentScanner.OriginalRequest);
                this.ASRequestTabs.SelectTab(0);
                IronUI.UpdateScanTabsWithRequestData();
                ScanIDLbl.Text = "Scan ID: " + Scanner.CurrentScanID.ToString();
                ScanStatusLbl.Text = "Scan Status: " + ScanStatus;
                Scanner.ResetChangedStatus();
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to display Request in 'Automated Scanning' section", Exp.Message, Exp.StackTrace);
                IronUI.ShowConfigureScanException("Unable to display request");
                return;
            }

            if (ASScanPluginsGrid.Rows.Count > 0)
            {
                ASScanPluginsGrid.Rows[0].Cells[0].Value = false;
                foreach (DataGridViewRow Row in this.ASScanPluginsGrid.Rows)
                {
                    if (Row.Index > 0)
                    {
                        Row.Cells[0].Value = Scanner.CurrentScanner.ShowChecks().Contains(Row.Cells[1].Value.ToString());
                    }
                }
                if (ASScanPluginsGrid.Rows.Count > 1)
                {
                    bool AllSelected = true;
                    for (int i = 1; i < ASScanPluginsGrid.Rows.Count; i++)
                    {
                        if (!(bool)ASScanPluginsGrid.Rows[i].Cells[0].Value)
                        {
                            AllSelected = false;
                            break;
                        }
                    }
                    if (AllSelected) ASScanPluginsGrid.Rows[0].Cells[0].Value = true;
                }
            }

            this.ASSessionPluginsCombo.Items.Add("");
            int SelectedSessionPluginID = -1;
            bool SelectedSessionPluginFound = false;
            foreach (string Name in SessionPlugin.List())
            {
                int ItemID = this.ASSessionPluginsCombo.Items.Add(Name);
                if (!SelectedSessionPluginFound)
                {
                    if (Scanner.CurrentScanner.SessionHandler.Name.Equals(Name))
                    {
                        SelectedSessionPluginID = ItemID;
                        SelectedSessionPluginFound = true;
                    }
                }
            }
            
            if(SelectedSessionPluginID >= 0 ) this.ASSessionPluginsCombo.SelectedIndex = SelectedSessionPluginID;
            try
            {
                FillInjectionsPointsinUI(Scanner.CurrentScanner);
            }
            catch (Exception Exp)
            {
                IronException.Report("Error restoring 'Automated Scan' configuration information from DB", Exp.Message, Exp.StackTrace);
                IronUI.ShowConfigureScanException("Error retriving scan information");
            }

            if (ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Completed") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Running"))
            {
                this.ASStartScanBtn.Text = "Scan Again";
            }
            else if (ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Not Started") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Incomplete") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Aborted") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Stopped"))
            {
                this.ASStartScanBtn.Text = "Start Scan";
            }
            this.ASStartScanBtn.Enabled = true;
        }

        void FillInjectionsPointsinUI(Scanner Scanner)
        {
            bool AllUlr = ASRequestScanURLGrid.Rows.Count > 0;
            foreach (DataGridViewRow Row in this.ASRequestScanURLGrid.Rows)
            {
                bool Result = Scanner.URLInjections.Contains(Row.Index);
                if (AllUlr)
                {
                    AllUlr = Result;
                }
                Row.Cells[0].Value = Result;
            }

            int SubParameterIndex = 0;
            string LastParameterName = "";

            bool AllQuery = ASRequestScanQueryGrid.Rows.Count > 0;
            foreach (DataGridViewRow Row in this.ASRequestScanQueryGrid.Rows)
            {
                string Name = Row.Cells[1].Value.ToString();
                if (Name.Equals(LastParameterName))
                {
                    SubParameterIndex++;
                }
                else
                {
                    SubParameterIndex = 0;
                }
                bool Result = Scanner.QueryInjections.Has(Name) && Scanner.QueryInjections.GetAll(Name).Contains(SubParameterIndex);
                if (AllQuery)
                {
                    AllQuery = Result;
                }
                Row.Cells[0].Value = Result;
                LastParameterName = Name;
            }

            SubParameterIndex = 0;
            LastParameterName = "";

            bool AllBody = ConfigureScanRequestBodyGrid.Rows.Count > 0;
            if (Scanner.BodyFormat.Name.Length == 0)
            {
                foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyGrid.Rows)
                {
                    string Name = Row.Cells[1].Value.ToString();
                    if (Name.Equals(LastParameterName))
                    {
                        SubParameterIndex++;
                    }
                    else
                    {
                        SubParameterIndex = 0;
                    }
                    bool Result = Scanner.BodyInjections.Has(Name) && Scanner.BodyInjections.GetAll(Name).Contains(SubParameterIndex);
                    if (AllBody)
                    {
                        AllBody = Result;
                    }
                    Row.Cells[0].Value = Result;
                    LastParameterName = Name;
                }
            }
            else
            {
                foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyGrid.Rows)
                {
                    bool Result = Scanner.BodyXmlInjections.Contains(Row.Index);
                    if (AllBody)
                    {
                        AllBody = Result;
                    }
                    Row.Cells[0].Value = Result;
                }
            }

            SubParameterIndex = 0;
            LastParameterName = "";

            bool AllCookie = ASRequestScanCookieGrid.Rows.Count > 0;
            foreach (DataGridViewRow Row in this.ASRequestScanCookieGrid.Rows)
            {
                string Name = Row.Cells[1].Value.ToString();
                if (Name.Equals(LastParameterName))
                {
                    SubParameterIndex++;
                }
                else
                {
                    SubParameterIndex = 0;
                }
                bool Result = Scanner.CookieInjections.Has(Name) && Scanner.CookieInjections.GetAll(Name).Contains(SubParameterIndex);
                if (AllCookie)
                {
                    AllCookie = Result;
                }
                Row.Cells[0].Value = Result;
                LastParameterName = Name;
            }

            SubParameterIndex = 0;
            LastParameterName = "";

            bool AllHeaders = ASRequestScanHeadersGrid.Rows.Count > 0;
            foreach (DataGridViewRow Row in this.ASRequestScanHeadersGrid.Rows)
            {
                string Name = Row.Cells[1].Value.ToString();
                if (Name.Equals(LastParameterName))
                {
                    SubParameterIndex++;
                }
                else
                {
                    SubParameterIndex = 0;
                }
                bool Result = Scanner.HeadersInjections.Has(Name) && Scanner.HeadersInjections.GetAll(Name).Contains(SubParameterIndex);
                if (AllHeaders)
                {
                    AllHeaders = Result;
                }
                Row.Cells[0].Value = Result;
                LastParameterName = Name;
            }

            ASRequestScanAllCB.Checked = AllUlr && AllQuery && AllBody && AllCookie && AllHeaders;
            ASRequestScanURLCB.Checked = AllUlr;
            ASRequestScanQueryCB.Checked = AllQuery ;
            ASRequestScanBodyCB.Checked = AllBody;
            ASRequestScanCookieCB.Checked = AllCookie;
            ASRequestScanHeadersCB.Checked = AllHeaders;
        }

        private void ScanLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanLogGrid.SelectedCells.Count < 1 || ScanLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Scan, ScanLogGrid.SelectedCells[0].Value.ToString());
            return;
        }

        private void ASRequestTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 0)
            {
                try
                {
                    IronUI.HandleAnyChangesInConfigureScanRequest();
                }
                catch (Exception Exp)
                {
                    IronUI.ShowConfigureScanException(Exp.Message);
                }
            }
        }

        private void ASStartScanBtn_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (this.ASStartScanBtn.Text.Equals("Start Scan"))
                    {
                        string ID = Scanner.CurrentScanID.ToString();
                        foreach (DataGridViewRow Row in ASQueueGrid.Rows)
                        {
                            if (Row.Cells[0].Value.ToString().Equals(ID))
                            {
                                if (Row.Cells[1].Value.ToString().Equals("Running"))
                                {
                                    IronUI.ShowConfigureScanException("This ScanJob is already running");
                                    return;
                                }
                            }
                        }
                    }
                }
                catch { }
                try
                {
                    IronUI.HandleAnyChangesInConfigureScanRequest();
                }
                catch (Exception Exp)
                {
                    IronUI.ShowConfigureScanException(Exp.Message);
                    return;
                }
                if (Scanner.CurrentScanner == null)
                {
                    IronUI.ShowConfigureScanException("Invalid Request");
                    return;
                }
                if (Scanner.CurrentScanner.OriginalRequest == null)
                {
                    IronUI.ShowConfigureScanException("Invalid Request");
                    return;
                }
                
                string SelectedFormatPlugin = "None";
                if (Scanner.CurrentScanner.BodyFormat.Name.Length > 0) SelectedFormatPlugin = Scanner.CurrentScanner.BodyFormat.Name;

                if (this.ASRequestScanAllCB.Checked)
                {
                    Scanner.CurrentScanner.InjectAll();
                }
                else
                {
                    if (this.ASRequestScanURLCB.Checked)
                    {
                        Scanner.CurrentScanner.InjectURL();
                    }
                    else
                    {
                        for (int i = 0; i < this.ASRequestScanURLGrid.Rows.Count; i++)
                        {
                            if ((bool)this.ASRequestScanURLGrid.Rows[i].Cells[0].Value)
                            {
                                Scanner.CurrentScanner.InjectUrl(i);
                            }
                        }
                    }
                    if (this.ASRequestScanQueryCB.Checked)
                    {
                        Scanner.CurrentScanner.InjectQuery();
                    }
                    else
                    {
                        int SubParameterPosition = 0;
                        string ParameterName = "";
                        foreach (DataGridViewRow Row in this.ASRequestScanQueryGrid.Rows)
                        {
                            string CurrentParameterName = Row.Cells[1].Value.ToString();
                            if (ParameterName.Equals(CurrentParameterName))
                            {
                                SubParameterPosition++;
                            }
                            else
                            {
                                ParameterName = CurrentParameterName;
                                SubParameterPosition = 0;
                            }
                            if ((bool)Row.Cells[0].Value)
                            {
                                Scanner.CurrentScanner.InjectQuery(ParameterName, SubParameterPosition);
                            }
                        }
                    }
                    if (this.ASRequestScanBodyCB.Checked)
                    {
                        Scanner.CurrentScanner.InjectBody();
                    }
                    else
                    {
                        int SubParameterPosition = 0;
                        string ParameterName = "";
                        foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyGrid.Rows)
                        {
                            if (Scanner.CurrentScanner.BodyFormat.Name.Length == 0)
                            {
                                string CurrentParameterName = Row.Cells[1].Value.ToString();
                                if (ParameterName.Equals(CurrentParameterName))
                                {
                                    SubParameterPosition++;
                                }
                                else
                                {
                                    ParameterName = CurrentParameterName;
                                    SubParameterPosition = 0;
                                }
                                if ((bool)Row.Cells[0].Value)
                                {
                                    Scanner.CurrentScanner.InjectBody(ParameterName, SubParameterPosition);
                                }
                            }
                            else
                            {
                                if ((bool)Row.Cells[0].Value)
                                {
                                    Scanner.CurrentScanner.InjectBody(Row.Index);
                                }
                            }
                        }
                    }
                    if (this.ASRequestScanCookieCB.Checked)
                    {
                        Scanner.CurrentScanner.InjectCookie();
                    }
                    else
                    {
                        int SubParameterPosition = 0;
                        string ParameterName = "";
                        foreach (DataGridViewRow Row in this.ASRequestScanCookieGrid.Rows)
                        {
                            string CurrentParameterName = Row.Cells[1].Value.ToString();
                            if (ParameterName.Equals(CurrentParameterName))
                            {
                                SubParameterPosition++;
                            }
                            else
                            {
                                ParameterName = CurrentParameterName;
                                SubParameterPosition = 0;
                            }
                            if ((bool)Row.Cells[0].Value)
                            {
                                Scanner.CurrentScanner.InjectCookie(ParameterName, SubParameterPosition);
                            }
                        }
                    }
                    if (this.ASRequestScanHeadersCB.Checked)
                    {
                        Scanner.CurrentScanner.InjectHeaders();
                    }
                    else
                    {
                        int SubParameterPosition = 0;
                        string ParameterName = "";
                        foreach (DataGridViewRow Row in this.ASRequestScanHeadersGrid.Rows)
                        {
                            string CurrentParameterName = Row.Cells[1].Value.ToString();
                            if (ParameterName.Equals(CurrentParameterName))
                            {
                                SubParameterPosition++;
                            }
                            else
                            {
                                ParameterName = CurrentParameterName;
                                SubParameterPosition = 0;
                            }
                            if ((bool)Row.Cells[0].Value)
                            {
                                Scanner.CurrentScanner.InjectHeaders(ParameterName, SubParameterPosition);
                            }
                        }
                    }
                }
                if ((Scanner.CurrentScanner.URLInjections.Count + Scanner.CurrentScanner.QueryInjections.Count + Scanner.CurrentScanner.BodyInjections.Count + Scanner.CurrentScanner.BodyXmlInjections.Count + Scanner.CurrentScanner.CookieInjections.Count + Scanner.CurrentScanner.HeadersInjections.Count) == 0)
                {
                    IronUI.ShowConfigureScanException("No Injection Points Selected or Available!");
                    return;
                }

                StringBuilder ScanPluginsBuilder = new StringBuilder();
                foreach (DataGridViewRow Row in ASScanPluginsGrid.Rows)
                {
                    if (Row.Index > 0)
                    {
                        if ((bool)Row.Cells[0].Value)
                        {
                            string PluginName = Row.Cells[1].Value.ToString();
                            Scanner.CurrentScanner.AddCheck(PluginName);
                            ScanPluginsBuilder.Append(PluginName);
                            ScanPluginsBuilder.Append(",");
                        }
                    }
                }
                string SelectedScanPlugins = ScanPluginsBuilder.ToString().TrimEnd(new char[] { ',' });
                if (Scanner.CurrentScanner.ShowChecks().Count == 0)
                {
                    IronUI.ShowConfigureScanException("No Plugin Selected!");
                    return;
                }
                string SelectedSessionPlugin = "None";
                if (ASSessionPluginsCombo.SelectedItem != null)
                {
                    string SessionPluginName = this.ASSessionPluginsCombo.SelectedItem.ToString();
                    if (SessionPluginName.Length > 0)
                    {
                        Scanner.CurrentScanner.SessionHandler = SessionPlugin.Get(SessionPluginName);
                        SelectedSessionPlugin = SessionPluginName;
                    }
                }

                if (Scanner.CurrentScanner.ShowChecks().Count == 0)
                {
                    IronUI.ShowConfigureScanException("No Plugin Selected!");
                    return;
                }

                if (ASStartScanBtn.Text.Equals("Start Scan"))
                    Scanner.CurrentScanner.StartScan();
                else
                    Scanner.CurrentScanner.LaunchScan();

                Scanner.CurrentScanner = null;
                Scanner.CurrentScanID = 0;
                this.ASStartScanBtn.Text = "Scan";
                Scanner.ResetChangedStatus();
                IronUI.ResetConfigureScanFields();
            }
            catch(Exception Exp)
            {
                IronException.Report("Error starting a configured scan", Exp.Message, Exp.StackTrace);
            }
        }

        private void ASScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ASScanPluginsGrid.SelectedCells.Count < 1 || ASScanPluginsGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if (this.ASScanPluginsGrid.SelectedRows[0].Index == 0)
            {
                bool AllValue = !(bool)this.ASScanPluginsGrid.SelectedCells[0].Value;
                this.ASScanPluginsGrid.SelectedCells[0].Value = AllValue;
                foreach (DataGridViewRow Row in this.ASScanPluginsGrid.Rows)
                {
                    if (Row.Index > 0)
                    {
                        Row.Cells[0].Value = AllValue;
                    }
                }
                return;
            }
            if ((bool)this.ASScanPluginsGrid.SelectedCells[0].Value)
            {
                this.ASScanPluginsGrid.SelectedCells[0].Value = false;
                this.ASScanPluginsGrid.Rows[0].SetValues(new object[]{false, "All"});
            }
            else
            {
                this.ASScanPluginsGrid.SelectedCells[0].Value = true;
            }
        }

        private void ASRequestScanAllCB_Click(object sender, EventArgs e)
        {
            CheckAllASRequestInjections();
        }

        private void CheckAllASRequestInjections()
        {
            this.ASRequestScanURLCB.Checked = this.ASRequestScanAllCB.Checked;
            this.CheckAllASRequestScanURLGridRows();
            this.ASRequestScanQueryCB.Checked = this.ASRequestScanAllCB.Checked;
            this.CheckAllASRequestScanQueryGridRows();
            this.ASRequestScanBodyCB.Checked = this.ASRequestScanAllCB.Checked;
            this.CheckAllASRequestScanBodyGridRows();
            this.ASRequestScanCookieCB.Checked = this.ASRequestScanAllCB.Checked;
            this.CheckAllASRequestScanCookieGridRows();
            this.ASRequestScanHeadersCB.Checked = this.ASRequestScanAllCB.Checked;
            this.CheckAllASRequestScanHeadersGridRows();
        }

        private void ASRequestScanURLCB_Click(object sender, EventArgs e)
        {
            this.CheckAllASRequestScanURLGridRows();
            if (!this.ASRequestScanURLCB.Checked)
            {
                if (this.ASRequestScanAllCB.Checked)
                {
                    this.ASRequestScanAllCB.Checked = false;
                }
            }
        }
        private void CheckAllASRequestScanURLGridRows()
        {
            foreach (DataGridViewRow Row in this.ASRequestScanURLGrid.Rows)
            {
                Row.Cells[0].Value = this.ASRequestScanURLCB.Checked;
            }
        }
        private void ASRequestScanQueryCB_Click(object sender, EventArgs e)
        {
            this.CheckAllASRequestScanQueryGridRows();
            if (!this.ASRequestScanQueryCB.Checked)
            {
                if (this.ASRequestScanAllCB.Checked)
                {
                    this.ASRequestScanAllCB.Checked = false;
                }
            }
        }
        private void CheckAllASRequestScanQueryGridRows()
        {
            foreach (DataGridViewRow Row in this.ASRequestScanQueryGrid.Rows)
            {
                Row.Cells[0].Value = this.ASRequestScanQueryCB.Checked;
            }
        }
        private void ASRequestScanBodyCB_Click(object sender, EventArgs e)
        {
            this.CheckAllASRequestScanBodyGridRows();
            if (!this.ASRequestScanBodyCB.Checked)
            {
                if (this.ASRequestScanAllCB.Checked)
                {
                    this.ASRequestScanAllCB.Checked = false;
                }
            }
        }
        private void CheckAllASRequestScanBodyGridRows()
        {
            foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyGrid.Rows)
            {
                Row.Cells[0].Value = this.ASRequestScanBodyCB.Checked;
            }
        }
        private void ASRequestScanCookieCB_Click(object sender, EventArgs e)
        {
            this.CheckAllASRequestScanCookieGridRows();
            if (!this.ASRequestScanCookieCB.Checked)
            {
                if (this.ASRequestScanAllCB.Checked)
                {
                    this.ASRequestScanAllCB.Checked = false;
                }
            }
        }
        private void CheckAllASRequestScanCookieGridRows()
        {
            foreach (DataGridViewRow Row in this.ASRequestScanCookieGrid.Rows)
            {
                Row.Cells[0].Value = this.ASRequestScanCookieCB.Checked;
            }
        }
        private void ASRequestScanHeadersCB_Click(object sender, EventArgs e)
        {
            this.CheckAllASRequestScanHeadersGridRows();
            if (!this.ASRequestScanHeadersCB.Checked)
            {
                if (this.ASRequestScanAllCB.Checked)
                {
                    this.ASRequestScanAllCB.Checked = false;
                }
            }
        }
        private void CheckAllASRequestScanHeadersGridRows()
        {
            foreach (DataGridViewRow Row in this.ASRequestScanHeadersGrid.Rows)
            {
                Row.Cells[0].Value = this.ASRequestScanHeadersCB.Checked;
            }
        }

        private void ASRequestScanURLGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ASRequestScanURLGrid.SelectedCells.Count < 1 || ASRequestScanURLGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ASRequestScanURLGrid.SelectedCells[0].Value)
            {
                this.ASRequestScanURLGrid.SelectedCells[0].Value = false;
                this.ASRequestScanAllCB.Checked = false;
                this.ASRequestScanURLCB.Checked = false;
            }
            else
            {
                this.ASRequestScanURLGrid.SelectedCells[0].Value = true;
            }
        }

        private void ASRequestScanQueryGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ASRequestScanQueryGrid.SelectedCells.Count < 1 || ASRequestScanQueryGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ASRequestScanQueryGrid.SelectedCells[0].Value)
            {
                this.ASRequestScanQueryGrid.SelectedCells[0].Value = false;
                this.ASRequestScanAllCB.Checked = false;
                this.ASRequestScanQueryCB.Checked = false;
            }
            else
            {
                this.ASRequestScanQueryGrid.SelectedCells[0].Value = true;
            }
        }

        private void ASRequestScanBodyGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ConfigureScanRequestBodyGrid.SelectedCells.Count < 1 || ConfigureScanRequestBodyGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ConfigureScanRequestBodyGrid.SelectedCells[0].Value)
            {
                this.ConfigureScanRequestBodyGrid.SelectedCells[0].Value = false;
                this.ASRequestScanAllCB.Checked = false;
                this.ASRequestScanBodyCB.Checked = false;
            }
            else
            {
                this.ConfigureScanRequestBodyGrid.SelectedCells[0].Value = true;
            }
        }

        private void ASRequestScanCookieGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ASRequestScanCookieGrid.SelectedCells.Count < 1 || ASRequestScanCookieGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ASRequestScanCookieGrid.SelectedCells[0].Value)
            {
                this.ASRequestScanCookieGrid.SelectedCells[0].Value = false;
                this.ASRequestScanAllCB.Checked = false;
                this.ASRequestScanCookieCB.Checked = false;
            }
            else
            {
                this.ASRequestScanCookieGrid.SelectedCells[0].Value = true;
            }
        }

        private void ASRequestScanHeadersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ASRequestScanHeadersGrid.SelectedCells.Count < 1 || ASRequestScanHeadersGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if ((bool)this.ASRequestScanHeadersGrid.SelectedCells[0].Value)
            {
                this.ASRequestScanHeadersGrid.SelectedCells[0].Value = false;
                this.ASRequestScanAllCB.Checked = false;
                this.ASRequestScanHeadersCB.Checked = false;
            }
            else
            {
                this.ASRequestScanHeadersGrid.SelectedCells[0].Value = true;
            }
        }

        private void ASSessionPluginsCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.ASScanPluginsGrid.Focus();
        }

        private void UpdateBodyGridForFormat(FormatPlugin Plugin, bool CheckStatus)
        {
            ConfigureScanRequestBodyGrid.Rows.Clear();
            string XmlString = Plugin.ToXml(Scanner.CurrentScanner.OriginalRequest.BodyArray);
            ConfigureScanRequestFormatXMLTB.Text = XmlString;
            string[,] InjectionPoints = FormatPlugin.XmlToArray(XmlString);
            for (int i = 0; i < InjectionPoints.GetLength(0); i++)
            {
                ConfigureScanRequestBodyGrid.Rows.Add(new object[] { CheckStatus, InjectionPoints[i, 0], InjectionPoints[i, 1] });
            }
        }

        private void ResultsTriggersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ResultsTriggersGrid.SelectedCells.Count < 1 || ResultsTriggersGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if (PluginResult.CurrentPluginResult != null)
            {
                int TriggerNumber = Int32.Parse(ResultsTriggersGrid.SelectedCells[0].Value.ToString()) - 1;
                IronUI.DisplayPluginResultsTrigger(TriggerNumber);
            }
        }

        private void ConfigProxyRunBtn_Click(object sender, EventArgs e)
        {
            if (ConfigProxyRunBtn.Text.Equals("Start Proxy"))
            {
                IronUI.ResetProxyException();
                IronProxy.Port = Int32.Parse(ConfigProxyListenPortTB.Text);
                IronProxy.LoopBackOnly = ConfigLoopBackOnlyCB.Checked;
                IronProxy.Start();
                IronDB.StoreProxyConfig();
            }
            else
            {
                IronProxy.Stop();
            }
        }

        private void ConfigProxyListenPortTB_TextChanged(object sender, EventArgs e)
        {
            int PortNumber=0;
            if (IronProxy.ValidProxyPort(ConfigProxyListenPortTB.Text))
            {
                PortNumber = Int32.Parse(ConfigProxyListenPortTB.Text);
                ConfigProxyListenPortTB.BackColor = Color.White;
                ConfigProxyRunBtn.Enabled = true;
            }
            else
            {
                ConfigProxyListenPortTB.BackColor = Color.Red;
                ConfigProxyRunBtn.Enabled = false;
            }
        }

        private void ConfigRuleFileExtensionsCB_CheckedChanged(object sender, EventArgs e)
        {
            bool Status = ConfigRuleFileExtensionsCB.Checked;
            ConfigRuleFileExtensionsPlusRB.Enabled = Status;
            ConfigRuleFileExtensionsPlusTB.Enabled = Status;
            ConfigRuleFileExtensionsMinusRB.Enabled = Status;
            ConfigRuleFileExtensionsMinusTB.Enabled = Status;
        }

        private void ConfigRuleHostNamesCB_CheckedChanged(object sender, EventArgs e)
        {
            bool Status = ConfigRuleHostNamesCB.Checked;
            ConfigRuleHostNamesPlusRB.Enabled = Status;
            ConfigRuleHostNamesPlusTB.Enabled = Status;
            ConfigRuleHostNamesMinusRB.Enabled = Status;
            ConfigRuleHostNamesMinusTB.Enabled = Status;
        }

        private void ConfigRuleKeywordInRequestCB_CheckedChanged(object sender, EventArgs e)
        {
            bool Status = ConfigRuleKeywordInRequestCB.Checked;
            ConfigRuleKeywordInRequestPlusRB.Enabled = Status;
            ConfigRuleKeywordInRequestPlusTB.Enabled = Status;
            ConfigRuleKeywordInRequestMinusRB.Enabled = Status;
            ConfigRuleKeywordInRequestMinusTB.Enabled = Status;
        }

        private void ConfigRuleKeywordInResponseCB_CheckedChanged(object sender, EventArgs e)
        {
            bool Status = ConfigRuleKeywordInResponseCB.Checked;
            ConfigRuleKeywordInResponsePlusRB.Enabled = Status;
            ConfigRuleKeywordInResponsePlusTB.Enabled = Status;
            ConfigRuleKeywordInResponseMinusRB.Enabled = Status;
            ConfigRuleKeywordInResponseMinusTB.Enabled = Status;
        }

        private void ConfigRuleApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Config.UpdateInterceptionRulesFromUI();
            IronDB.StoreInterceptRules();
        }

        private void ConfigRuleCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdateInterceptionRulesInUIFromConfig();
        }

        private void ConfigUpstreamProxyPortTB_TextChanged(object sender, EventArgs e)
        {
            ConfigUseUpstreamProxyCB.Checked = false;
            IronProxy.UseUpstreamProxy = false;
            int PortNumber = 0;
            if (Int32.TryParse(ConfigUpstreamProxyPortTB.Text, out PortNumber))
            {
                if (PortNumber > 0 && PortNumber < 65536)
                {
                    ConfigUpstreamProxyPortTB.BackColor = Color.White;
                    ConfigUseUpstreamProxyCB.Enabled = true;
                }
                else
                {
                    ConfigUpstreamProxyPortTB.BackColor = Color.Red;
                    if (!ConfigUseUpstreamProxyCB.Checked) ConfigUseUpstreamProxyCB.Enabled = false;
                }
            }
            else
            {
                ConfigUpstreamProxyPortTB.BackColor = Color.Red;
                if(!ConfigUseUpstreamProxyCB.Checked) ConfigUseUpstreamProxyCB.Enabled = false;
            }
        }

        private void ConfigUpstreamProxyIPTB_TextChanged(object sender, EventArgs e)
        {
            ConfigUseUpstreamProxyCB.Checked = false;
            IronProxy.UseUpstreamProxy = false;
            if (ConfigUpstreamProxyIPTB.Text.Length > 0)
            {
                ConfigUpstreamProxyIPTB.BackColor = Color.White;
                if (ConfigUpstreamProxyPortTB.Text.Length > 0 && ConfigUpstreamProxyPortTB.BackColor != Color.Red)
                {
                    ConfigUseUpstreamProxyCB.Enabled = true;
                }
                else
                {
                    if (!ConfigUseUpstreamProxyCB.Checked) ConfigUseUpstreamProxyCB.Enabled = false;
                }
            }
            else
            {
                ConfigUpstreamProxyIPTB.BackColor = Color.Red;
                if (!ConfigUseUpstreamProxyCB.Checked) ConfigUseUpstreamProxyCB.Enabled = false;
            }
        }

        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string LogFilesDirectory = "";
            ProjectFileOpenDialog.InitialDirectory = Config.RootDir + "\\log\\";
            while (ProjectFileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (!ProjectFileOpenDialog.SafeFileName.Equals("Project.iron", StringComparison.InvariantCultureIgnoreCase))
                {
                    MessageBox.Show("Only files named 'Project.iron' are allowed");
                }
                else
                {
                    FileInfo Info = new FileInfo(ProjectFileOpenDialog.FileName);
                    LogFilesDirectory = Info.Directory.FullName;
                    break;
                }
            }
            if (LogFilesDirectory.Length > 0)
            {
                IronDB.UpdateLogFilePaths(LogFilesDirectory);
                IronUI.StartUpdatingFullUIFromDB();
            }
        }

        private void ProxyRequestHeadersIDV_IDVTextChanged()
        {
            IronProxy.RequestHeaderChanged = true;
            IronProxy.RequestChanged = true;
        }

        private void ProxyRequestBodyIDV_IDVTextChanged()
        {
            IronProxy.RequestBodyChanged = true;
            IronProxy.RequestChanged = true;
        }

        private void ProxyResponseHeadersIDV_IDVTextChanged()
        {
            IronProxy.ResponseHeaderChanged = true;
            IronProxy.ResponseChanged = true;
        }

        private void ProxyResponseBodyIDV_IDVTextChanged()
        {
            IronProxy.ResponseBodyChanged = true;
            IronProxy.ResponseChanged = true;
        }

        private void ProxyRequestParametersQueryGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            IronProxy.RequestQueryParametersChanged = true;
            IronProxy.RequestChanged = true;
        }

        private void ProxyRequestParametersBodyGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            IronProxy.RequestBodyParametersChanged = true;
            IronProxy.RequestChanged = true;
        }

        private void ProxyRequestParametersCookieGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            IronProxy.RequestCookieParametersChanged = true;
            IronProxy.RequestChanged = true;
        }

        private void ProxyRequestParametersHeadersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            IronProxy.RequestHeaderParametersChanged = true;
            IronProxy.RequestChanged = true;
        }

        private void ProxyRequestParametersTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (IronProxy.ManualTamperingFree) return;
            try
            {
                IronUI.ResetProxyException();
                IronUI.HandleAnyChangesInRequest();
            }
            catch(Exception Exp)
            {
                IronUI.ShowProxyException(Exp.Message);
            }
        }

        private void ProxyInterceptRequestTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (IronProxy.ManualTamperingFree) return;
            try
            {
                IronUI.ResetProxyException();
                IronUI.HandleAnyChangesInRequest();
            }
            catch(Exception Exp)
            {
                IronUI.ShowProxyException(Exp.Message);
            }
        }

        private void ProxyInterceptResponseTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (IronProxy.ManualTamperingFree) return;
            try
            {
                IronUI.ResetProxyException();
                IronUI.HandleAnyChangesInResponse();
            }
            catch(Exception Exp)
            {
                IronUI.ShowProxyException(Exp.Message);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IronUI.AF == null)
            {
                IronUI.AF = new AboutForm();
            }
            else if (IronUI.AF.IsDisposed)
            {
                IronUI.AF = new AboutForm();
            }
            IronUI.AF.Show();
            IronUI.AF.AboutLogoPB.Focus();
        }

        private void ProxyShowOriginalRequestCB_Click(object sender, EventArgs e)
        {
            if (IronProxy.CurrentSession != null)
            {
                if (ProxyShowOriginalRequestCB.Checked)
                {
                    if (IronProxy.CurrentSession.OriginalRequest != null)
                    {
                        IronUI.FillProxyFields(IronProxy.CurrentSession.OriginalRequest);
                        IronUI.MakeProxyFieldsReadOnly();
                    }
                }
                else
                {
                    if (IronProxy.CurrentSession.Request != null)
                    {
                        IronUI.FillProxyFields(IronProxy.CurrentSession.Request);
                        IronUI.MakeProxyFieldsReadOnly();
                    }
                }
            }
        }

        private void ProxyShowOriginalResponseCB_Click(object sender, EventArgs e)
        {
            if (IronProxy.CurrentSession != null)
            {
                if (ProxyShowOriginalResponseCB.Checked)
                {
                    if (IronProxy.CurrentSession.OriginalResponse != null)
                    {
                        IronUI.FillProxyFields(IronProxy.CurrentSession.OriginalResponse);
                    }
                }
                else
                {
                    if (IronProxy.CurrentSession.Response != null)
                    {
                        IronUI.FillProxyFields(IronProxy.CurrentSession.Response);
                    }
                }
            }
            ProxyInterceptTabs.SelectedIndex = 1;
        }

        private void MTRequestHeadersIDV_IDVTextChanged()
        {
            ManualTesting.RequestHeaderChanged = true;
            ManualTesting.RequestChanged = true;
        }

        private void MTRequestBodyIDV_IDVTextChanged()
        {
            ManualTesting.RequestBodyChanged = true;
            ManualTesting.RequestChanged = true;
        }

        private void MTRequestParametersHeadersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ManualTesting.RequestHeaderParametersChanged = true;
            ManualTesting.RequestChanged = true;
        }

        private void MTRequestParametersCookieGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ManualTesting.RequestCookieParametersChanged = true;
            ManualTesting.RequestChanged = true;
        }

        private void MTRequestParametersBodyGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ManualTesting.RequestBodyParametersChanged = true;
            ManualTesting.RequestChanged = true;
        }

        private void MTRequestParametersQueryGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ManualTesting.RequestQueryParametersChanged = true;
            ManualTesting.RequestChanged = true;
        }

        private void MTRequestTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                IronUI.ResetMTExceptionFields();
                IronUI.HandleAnyChangesInMTRequest();
            }
            catch(Exception Exp)
            {
                IronUI.ShowMTException(Exp.Message);
            }
        }

        private void MTRequestParametersTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                IronUI.ResetMTExceptionFields();
                IronUI.HandleAnyChangesInMTRequest();
            }
            catch(Exception Exp)
            {
                IronUI.ShowMTException(Exp.Message);
            }
        }

        private void ASRequestRawHeadersIDV_IDVTextChanged()
        {
            Scanner.RequestHeadersChanged = true;
        }

        private void ASRequestRawBodyIDV_IDVTextChanged()
        {
            Scanner.RequestBodyChanged = true;
        }

        private void PluginTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            try
            {
                if (e.Node.Level == 2)
                {
                    string PluginName = e.Node.Name;
                    string[] PluginDetails = new string[] { "", "", "", "", "" };
                    PluginDetails[0] = e.Node.Name;
                    switch (e.Node.Parent.Index)
                    {
                        case 0:
                            PassivePlugin PP = PassivePlugin.Get(PluginDetails[0]);
                            PluginDetails[1] = PP.Description;
                            PluginDetails[2] = Config.RootDir + "\\plugins\\passive\\" + PP.FileName;
                            if (PP.FileName.Length > 0)
                            {
                                if (PP.FileName.EndsWith(".py"))
                                {
                                    PluginDetails[4] = "Python";
                                }
                                else if (PP.FileName.EndsWith(".rb"))
                                {
                                    PluginDetails[4] = "Ruby";
                                }
                                else
                                {
                                    PluginDetails[4] = "-";
                                }
                                try
                                {
                                    StreamReader SR = File.OpenText(PluginDetails[2]);
                                    PluginDetails[3] = SR.ReadToEnd();
                                    SR.Close();
                                }
                                catch (Exception exp)
                                {
                                    PluginDetails[3] = "Error reading file: " + exp.Message;
                                }
                            }
                            else
                            {
                                PluginDetails[3] = "FileName information is missing in the Plugin";
                            }
                            break;
                        case 1:
                            ActivePlugin AP = ActivePlugin.Get(PluginDetails[0]);
                            PluginDetails[1] = AP.Description;
                            PluginDetails[2] = Config.RootDir + "\\plugins\\active\\" + AP.FileName;
                            if (AP.FileName.Length > 0)
                            {
                                if (AP.FileName.EndsWith(".py"))
                                {
                                    PluginDetails[4] = "Python";
                                }
                                else if (AP.FileName.EndsWith(".rb"))
                                {
                                    PluginDetails[4] = "Ruby";
                                }
                                else
                                {
                                    PluginDetails[4] = "-";
                                }
                                try
                                {
                                    StreamReader SR = File.OpenText(PluginDetails[2]);
                                    PluginDetails[3] = SR.ReadToEnd();
                                    SR.Close();
                                }
                                catch (Exception exp)
                                {
                                    PluginDetails[3] = "Error reading file: " + exp.Message;
                                }
                            }
                            else
                            {
                                PluginDetails[3] = "FileName information is missing in the Plugin";
                            }
                            break;
                        case 2:
                            FormatPlugin FP = FormatPlugin.Get(PluginDetails[0]);
                            PluginDetails[1] = FP.Description;
                            PluginDetails[2] = Config.RootDir + "\\plugins\\format\\" + FP.FileName;
                            if (FP.FileName.Length > 0)
                            {
                                if (FP.FileName.EndsWith(".py"))
                                {
                                    PluginDetails[4] = "Python";
                                }
                                else if (FP.FileName.EndsWith(".rb"))
                                {
                                    PluginDetails[4] = "Ruby";
                                }
                                else
                                {
                                    PluginDetails[4] = "-";
                                }
                                try
                                {
                                    StreamReader SR = File.OpenText(PluginDetails[2]);
                                    PluginDetails[3] = SR.ReadToEnd();
                                    SR.Close();
                                }
                                catch (Exception exp)
                                {
                                    PluginDetails[3] = "Error reading file: " + exp.Message;
                                }
                            }
                            else
                            {
                                PluginDetails[3] = "FileName information is missing in the Plugin";
                            }
                            break;
                        case 3:
                            SessionPlugin SP = SessionPlugin.Get(PluginDetails[0]);
                            PluginDetails[1] = SP.Description;
                            PluginDetails[2] = Config.RootDir + "\\plugins\\session\\" + SP.FileName;
                            if (SP.FileName.Length > 0)
                            {
                                if (SP.FileName.EndsWith(".py"))
                                {
                                    PluginDetails[4] = "Python";
                                }
                                else if (SP.FileName.EndsWith(".rb"))
                                {
                                    PluginDetails[4] = "Ruby";
                                }
                                else
                                {
                                    PluginDetails[4] = "-";
                                }
                                try
                                {
                                    StreamReader SR = File.OpenText(PluginDetails[2]);
                                    PluginDetails[3] = SR.ReadToEnd();
                                    SR.Close();
                                }
                                catch (Exception exp)
                                {
                                    PluginDetails[3] = "Error reading file: " + exp.Message;
                                }
                            }
                            else
                            {
                                PluginDetails[3] = "FileName information is missing in the Plugin";
                            }
                            break;
                    }
                    IronUI.DisplayPluginDetails(PluginDetails);
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Error showing Plugin details", Exp.Message, Exp.StackTrace);
            }
        }

        private void SelectedPluginReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PluginTree.SelectedNode == null) return;
            TreeNode SelectedNode = PluginTree.SelectedNode;
            if (SelectedNode.Level == 2)
            {
                PluginType Type = PluginType.None;
                string FileName = "";

                switch (SelectedNode.Parent.Index)
                {
                    case 0:
                        Type = PluginType.Passive;
                        FileName = PassivePlugin.Get(SelectedNode.Name).FileName;
                        break;
                    case 1:
                        Type = PluginType.Active;
                        FileName = ActivePlugin.Get(SelectedNode.Name).FileName;
                        break;
                    case 2:
                        Type = PluginType.Format;
                        FileName = FormatPlugin.Get(SelectedNode.Name).FileName;
                        break;
                    case 3:
                        Type = PluginType.Session;
                        FileName = SessionPlugin.Get(SelectedNode.Name).FileName;
                        break;
                }
                if (Type != PluginType.None && SelectedNode.Name.Length > 0)
                {
                    PluginStore.ReloadPlugin(Type, SelectedNode.Name, FileName);
                    IronUI.BuildPluginTree();
                }
            }
        }

        private void PluginTreeMenu_Opening(object sender, CancelEventArgs e)
        {
            if (PluginTree.SelectedNode == null) return;
            TreeNode SelectedNode = PluginTree.SelectedNode;
            if (SelectedNode.Level == 2)
            {
                SelectedPluginReloadToolStripMenuItem.Enabled = true;
                if (SelectedNode.Parent.Index == 0)
                {
                    SelectedPluginDeactivateToolStripMenuItem.Visible = true;
                    if (SelectedNode.ForeColor == Color.Gray)
                    {
                        SelectedPluginDeactivateToolStripMenuItem.Text = "Activate Selected Plugin";
                    }
                    else
                    {
                        SelectedPluginDeactivateToolStripMenuItem.Text = "Deactivate Selected Plugin";
                    }
                }
                else
                {
                    SelectedPluginDeactivateToolStripMenuItem.Visible = false;
                }
            }
            else
            {
                SelectedPluginReloadToolStripMenuItem.Enabled = false;
                SelectedPluginDeactivateToolStripMenuItem.Visible = false;
            }

        }

        private void AllPluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadAllPlugins);
            T.Start();
        }

        private void PassivePluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadAllPassivePlugins);
            T.Start();
        }

        private void ActivePluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadAllActivePlugins);
            T.Start();
        }

        private void FormatPluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadAllFormatPlugins);
            T.Start();
        }

        private void SessionPluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadAllSessionPlugins);
            T.Start();
        }

        private void SessionPluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadNewSessionPlugins);
            T.Start();
        }

        private void FormatPluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadNewFormatPlugins);
            T.Start();
        }

        private void ActivePluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadNewActivePlugins);
            T.Start();
        }

        private void PassivePluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadNewPassivePlugins);
            T.Start();
        }

        private void AllPluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginStore.LoadAllNewPlugins);
            T.Start();
        }

        private void SelectedPluginDeactivateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PluginTree.SelectedNode == null) return;
            if (SelectedPluginDeactivateToolStripMenuItem.Text.Equals("Deactivate Selected Plugin"))
            {
                PassivePlugin.Deactivate(PluginTree.SelectedNode.Name);
                PluginTree.SelectedNode.ForeColor = Color.Gray;
                PluginTree.SelectedNode.Text = PluginTree.SelectedNode.Name + " (Deactivated)";
            }
            else
            {
                PassivePlugin.Activate(PluginTree.SelectedNode.Name);
                PluginTree.SelectedNode.ForeColor = Color.Green;
                PluginTree.SelectedNode.Text = PluginTree.SelectedNode.Name;
            }
        }

        private void ProxyDropBtn_Click(object sender, EventArgs e)
        {
            IronProxy.DropInterceptedMessage();
        }

        private void CustomSendActivateCB_Click(object sender, EventArgs e)
        {
            if (this.CustomSendActivateCB.Checked)
            {
                string Result = "";
                if (this.CustomSendPythonRB.Checked)
                {
                    Result = ManualTesting.SetPyScriptedSend(this.CustomSendTE.Text);
                }
                else
                {
                    Result = ManualTesting.SetRbScriptedSend(this.CustomSendTE.Text);
                }
                if (Result.Length > 0)
                {
                    IronUI.ShowScriptedSendScriptException(Result);
                }
            }
            ManualTesting.ScriptedSendEnabled = this.CustomSendActivateCB.Checked;
            this.MTScriptedSendBtn.Enabled = ManualTesting.ScriptedSendEnabled;
        }

        private void MTRequestFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        {
            this.MTRequestDeSerObjectToXmlMenuItem.Enabled = false;
            this.MTRequestSerXmlToObjectMenuItem.Enabled = false;
            if (this.MTRequestFormatPluginsGrid.SelectedRows.Count == 0)
            {
                return;
            }
            if (this.MTRequestFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
            this.MTRequestDeSerObjectToXmlMenuItem.Enabled = true;
            if (this.MTRequestFormatXMLTB.Text.Length > 0)
            {
                this.MTRequestSerXmlToObjectMenuItem.Enabled = true;
            }
        }

        private void MTRequestDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        {
            MTRequestFormatXMLTB.Text = "";
            string PluginName = MTRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
            if (!FormatPlugin.List().Contains(PluginName))
            {
                IronUI.ShowMTException("Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (ManualTesting.CurrentRequest == null)
            {
                IronUI.ShowMTException("Invalid Request");
                return;
            }
            ManualTesting.StartDeSerializingRequestBody(ManualTesting.CurrentRequest, Plugin);
        }

        private void MTRequestSerXmlToObjectMenuItem_Click(object sender, EventArgs e)
        {
            if (MTRequestFormatPluginsGrid.SelectedCells == null) return;
            if (MTRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
            string PluginName = MTRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
            if (!FormatPlugin.List().Contains(PluginName))
            {
                IronUI.ShowMTException("Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (ManualTesting.CurrentRequest == null)
            {
                IronUI.ShowMTException("Invalid Request");
                return;
            }
            string XML = MTRequestFormatXMLTB.Text;
            ManualTesting.StartSerializingRequestBody(ManualTesting.CurrentRequest, Plugin, XML);
        }

        private void ProxyRequestFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        {
            this.ProxyRequestDeSerObjectToXmlMenuItem.Enabled = false;
            this.ProxyRequestSerXmlToObjectMenuItem.Enabled = false;
            if (this.ProxyRequestFormatPluginsGrid.SelectedRows.Count == 0)
            {
                return;
            }

            if (this.ProxyRequestFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
            this.ProxyRequestDeSerObjectToXmlMenuItem.Enabled = true;
            if (this.ProxyRequestFormatXMLTB.Text.Length > 0)
            {
                this.ProxyRequestSerXmlToObjectMenuItem.Enabled = true;
            }
        }

        private void ProxyResponseFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        {
            this.ProxyResponseDeSerObjectToXmlMenuItem.Enabled = false;
            this.ProxyResponseSerXmlToObjectMenuItem.Enabled = false;
            if (this.ProxyResponseFormatPluginsGrid.SelectedRows.Count == 0) return;
            if (this.ProxyResponseFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
            this.ProxyResponseDeSerObjectToXmlMenuItem.Enabled = true;                
            if (this.ProxyResponseFormatXMLTB.Text.Length > 0)
            {
                this.ProxyResponseSerXmlToObjectMenuItem.Enabled = true;
            }
        }

        private void ProxyRequestDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        {
            ProxyRequestFormatXMLTB.Text = "";
            if (ProxyRequestFormatPluginsGrid.SelectedCells == null) return;
            if (ProxyRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
            string PluginName = ProxyRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
            if (!FormatPlugin.List().Contains(PluginName))
            {
                IronUI.ShowProxyException("Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (IronProxy.CurrentSession == null)
            {
                IronUI.ShowProxyException("Invalid Request");
                return;
            }
            if (IronProxy.CurrentSession.Request == null)
            {
                IronUI.ShowProxyException("Invalid Request");
                return;
            }
            IronProxy.StartDeSerializingRequestBody(IronProxy.CurrentSession.Request, Plugin);
        }

        private void ProxyRequestSerXmlToObjectMenuItem_Click(object sender, EventArgs e)
        {
            if (ProxyRequestFormatPluginsGrid.SelectedCells == null) return;
            if (ProxyRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
            string PluginName = ProxyRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
            if (!FormatPlugin.List().Contains(PluginName))
            {
                IronUI.ShowProxyException("Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (IronProxy.CurrentSession == null)
            {
                IronUI.ShowProxyException("Invalid Request");
                return;
            }
            if (IronProxy.CurrentSession.Request == null)
            {
                IronUI.ShowProxyException("Invalid Request");
                return;
            }
            string XML = ProxyRequestFormatXMLTB.Text;
            IronProxy.StartSerializingRequestBody(IronProxy.CurrentSession.Request, Plugin, XML);
        }

        private void ProxyResponseDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        {
            ProxyResponseFormatXMLTB.Text = "";
            if (ProxyResponseFormatPluginsGrid.SelectedCells == null) return;
            if (ProxyResponseFormatPluginsGrid.SelectedCells.Count == 0) return;
            string PluginName = ProxyResponseFormatPluginsGrid.SelectedCells[0].Value.ToString();
            if (!FormatPlugin.List().Contains(PluginName))
            {
                IronUI.ShowProxyException("Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (IronProxy.CurrentSession == null)
            {
                IronUI.ShowProxyException("Invalid Response");
                return;
            }
            if (IronProxy.CurrentSession.Response == null)
            {
                IronUI.ShowProxyException("Invalid Response");
                return;
            }
            IronProxy.StartDeSerializingResponseBody(IronProxy.CurrentSession.Response, Plugin);
        }

        private void ProxyResponseSerXmlToObjectMenuItem_Click(object sender, EventArgs e)
        {
            if (ProxyResponseFormatPluginsGrid.SelectedCells == null) return;
            if (ProxyResponseFormatPluginsGrid.SelectedCells.Count == 0) return;
            string PluginName = ProxyResponseFormatPluginsGrid.SelectedCells[0].Value.ToString();
            if (!FormatPlugin.List().Contains(PluginName))
            {
                IronUI.ShowProxyException("Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (IronProxy.CurrentSession == null)
            {
                IronUI.ShowProxyException("Invalid Response");
                return;
            }
            if (IronProxy.CurrentSession.Response == null)
            {
                IronUI.ShowProxyException("Invalid Response");
                return;
            }
            string XML = ProxyResponseFormatXMLTB.Text;
            IronProxy.StartSerializingResponseBody(IronProxy.CurrentSession.Response, Plugin, XML);
        }

        private void ConfigureScanRequestFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        {
            this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Enabled = false;
            if (this.ConfigureScanRequestFormatPluginsGrid.SelectedRows.Count == 0)
            {
                return;
            }
            if (Scanner.CurrentScanner == null) return;
            if (Scanner.CurrentScanner.OriginalRequest == null) return;
            if (this.ConfigureScanRequestFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
            if (Scanner.CurrentScanner.OriginalRequest.HasBody)
            {
                this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Enabled = true;
            }
        }

        private void ConfigureScanRequestDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        {
            ConfigureScanRequestFormatXMLTB.Text = "";
            if (ConfigureScanRequestFormatPluginsGrid.SelectedCells == null) return;
            if (ConfigureScanRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
            string PluginName = ConfigureScanRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
            if (PluginName.Equals("None"))
            {
                IronUI.UpdateScanBodyTabWithDataInDefaultFormat();
                ConfigureScanRequestFormatXMLTB.Text = "";
                Scanner.CurrentScanner.BodyFormat = new FormatPlugin();
                return;
            }
            if (!FormatPlugin.List().Contains(PluginName))
            {
                IronUI.ShowConfigureScanException("Format Plugin not found");
                return;
            }
            FormatPlugin Plugin = FormatPlugin.Get(PluginName);
            if (Scanner.CurrentScanner == null)
            {
                IronUI.ShowConfigureScanException("Invalid Request");
                return;
            }
            if (Scanner.CurrentScanner.OriginalRequest == null)
            {
                IronUI.ShowConfigureScanException("Invalid Request");
                return;
            }
            Scanner.CurrentScanner.BodyFormat = Plugin;
            Scanner.StartDeSerializingRequestBody(Scanner.CurrentScanner.OriginalRequest, Plugin, new List<bool>(), false);
        }

        private void ScriptingShellTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1)
            {
                MultiLineShellExecuteBtn.Visible = true;
                ClearShellDisplayBtn.Visible = false;
            }
            else
            {
                MultiLineShellExecuteBtn.Visible = false;
                ClearShellDisplayBtn.Visible = true;
            }
        }

        private void MultiLineShellExecuteBtn_Click(object sender, EventArgs e)
        {
            IronUI.FreezeInteractiveShellUI();
            IronScripting.QueueMultiLineShellInputForExecution(MultiLineShellInTE.Text);
            ScriptingShellTabs.SelectedIndex = 0;
        }

        private void ConfigScriptPathApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] PyPaths = ConfigScriptPyPathsTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            lock (IronScripting.PyPaths)
            {
                IronScripting.PyPaths.Clear();
                IronScripting.PyPaths.AddRange(PyPaths);
            }
            string[] RbPaths = ConfigScriptRbPathsTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            lock (IronScripting.RbPaths)
            {
                IronScripting.RbPaths.Clear();
                IronScripting.RbPaths.AddRange(RbPaths);
            }
            IronDB.StoreScriptPathsConfig();
        }

        private void ConfigScriptPathCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdatePyPathsFromConfig();
            IronUI.UpdateRbPathsFromConfig();
        }

        private void ConfigScriptCommandApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] PyCommands = ConfigScriptPyCommandsTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            lock (IronScripting.PyCommands)
            {
                IronScripting.PyCommands.Clear();
                IronScripting.PyCommands.AddRange(PyCommands);
            }
            string[] RbCommands = ConfigScriptRbCommandsTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            lock (IronScripting.RbCommands)
            {
                IronScripting.RbCommands.Clear();
                IronScripting.RbCommands.AddRange(RbCommands);
            }
            IronDB.StoreScriptCommandsConfig();
        }

        private void ConfigScriptCommandCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdatePyCommandsFromConfig();
            IronUI.UpdateRbCommandsFromConfig();
        }

        private void ConfigRequestTypesApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] Types = ConfigRequestTypesTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            lock (Request.TextContentTypes)
            {
                Request.TextContentTypes.Clear();
                Request.TextContentTypes.AddRange(Types);
            }
            IronDB.StoreRequestTextContentTypesConfig();
        }

        private void ConfigRequestTypesCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdateRequestTextTypesFromConfig();
        }

        private void ConfigResponseTypesApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] Types = ConfigResponseTypesTB.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            lock (Response.TextContentTypes)
            {
                Response.TextContentTypes.Clear();
                Response.TextContentTypes.AddRange(Types);
            }
            IronDB.StoreResponseTextContentTypesConfig();
        }

        private void ConfigResponseTypesCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdateResponseTextTypesFromConfig();
        }

        private void ConfigDisplayRuleApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Config.UpdateProxyLogDisplayFilterFromUI();
            IronDB.StoreDisplayRules();
        }

        private void ConfigDisplayRuleCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdateLogDisplayFilterInUIFromConfig();
        }

        private void ConfigDisplayRuleFileExtensionsCB_CheckedChanged(object sender, EventArgs e)
        {
            bool Status = ConfigDisplayRuleFileExtensionsCB.Checked;
            ConfigDisplayRuleFileExtensionsPlusRB.Enabled = Status;
            ConfigDisplayRuleFileExtensionsPlusTB.Enabled = Status;
            ConfigDisplayRuleFileExtensionsMinusRB.Enabled = Status;
            ConfigDisplayRuleFileExtensionsMinusTB.Enabled = Status;
        }

        private void ConfigDisplayRuleHostNamesCB_CheckedChanged(object sender, EventArgs e)
        {
            bool Status = ConfigDisplayRuleHostNamesCB.Checked;
            ConfigDisplayRuleHostNamesPlusRB.Enabled = Status;
            ConfigDisplayRuleHostNamesPlusTB.Enabled = Status;
            ConfigDisplayRuleHostNamesMinusRB.Enabled = Status;
            ConfigDisplayRuleHostNamesMinusTB.Enabled = Status;
        }

        private void ConfigUseUpstreamProxyCB_Click(object sender, EventArgs e)
        {
            IronProxy.UseUpstreamProxy = ConfigUseUpstreamProxyCB.Checked;
            if (IronProxy.UseUpstreamProxy)
            {
                IronProxy.UpstreamProxyIP = ConfigUpstreamProxyIPTB.Text;
                IronProxy.UpstreamProxyPort = Int32.Parse(ConfigUpstreamProxyPortTB.Text);
            }
            IronDB.StoreUpstreamProxyConfig();
        }

        private void MTClearFieldsBtn_Click(object sender, EventArgs e)
        {
            IronUI.ResetMTDisplayFields();
            TestIDLbl.Text = "ID: 0";
            TestGroupLogGrid.Rows.Clear();
            ManualTesting.ClearGroup();
            MTReqResTabs.SelectTab("MTRequestTab");
        }

        private void SiteMapLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SiteMapLogGrid.SelectedCells.Count < 1 || SiteMapLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Proxy"))
            {
                IronLog.ShowLog(RequestSource.Proxy, SiteMapLogGrid.SelectedCells[0].Value.ToString()); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Test"))
            {
                IronLog.ShowLog(RequestSource.Test, SiteMapLogGrid.SelectedCells[0].Value.ToString()); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Shell"))
            {
                IronLog.ShowLog(RequestSource.Shell, SiteMapLogGrid.SelectedCells[0].Value.ToString()); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Probe"))
            {
                IronLog.ShowLog(RequestSource.Probe, SiteMapLogGrid.SelectedCells[0].Value.ToString()); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Scan"))
            {
                IronLog.ShowLog(RequestSource.Scan, SiteMapLogGrid.SelectedCells[0].Value.ToString()); 
            }
            else
            {
                return;
            }
        }

        private void IronTreeMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (IronTree.SelectedNode == null) return;
            TreeNode Node = IronTree.SelectedNode;
            if ((Node.Level > 5) || (Node.Level == 5 && (Node.Parent.Parent.Parent.Parent.Index == 4)) || (Node.Level == 4 && (Node.Parent.Parent.Parent.Index == 4)) || (Node.Level == 3 && (Node.Parent.Parent.Index == 4)) || (Node.Level == 2 && (Node.Parent.Index == 4)))
            {
                if (IronUI.IsScanBranchFormOpen())
                {
                    ScanBranchToolStripMenuItem.Enabled = false;
                    IronUI.SBF.Activate();
                }
                else
                {
                    ScanBranchToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                ScanBranchToolStripMenuItem.Enabled = false;
            }

        }

        private void ScanBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IronTree.SelectedNode == null) return;
            TreeNode Node = IronTree.SelectedNode;
            if ((Node.Level > 4) || (Node.Level == 4 && (Node.Parent.Parent.Parent.Index == 4)) || (Node.Level == 3 && (Node.Parent.Parent.Index == 4)) || (Node.Level == 2 && (Node.Parent.Index == 4)))
            {
                List<string> UrlPaths = new List<string>();
                string Query = "";
                TreeNode SiteMapNode = Node;
                if (SiteMapNode.Text.StartsWith("?"))
                {
                    Query = SiteMapNode.Text;
                    SiteMapNode = SiteMapNode.Parent;
                }
                while (SiteMapNode.Level > 2)
                {
                    UrlPaths.Add(SiteMapNode.Text);
                    SiteMapNode = SiteMapNode.Parent;
                }
                UrlPaths.Reverse();
                StringBuilder UrlPathBuilder = new StringBuilder();
                foreach (string Path in UrlPaths)
                {
                    UrlPathBuilder.Append("/"); UrlPathBuilder.Append(Path);
                }
                string Host = SiteMapNode.Text;
                string Url = UrlPathBuilder.ToString();// +Query;
                if (Url.Length == 0)
                {
                    Url = "/*";
                }
                else
                {
                    Url = Url + "*";
                }
                IronUI.ShowScanBranchForm(Host, Url);
            }
        }

        private void PluginEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IronUI.IsPluginEditorOpen())
            {
                IronUI.PE.Activate();
            }
            else
            {
                IronUI.PE = new PluginEditor();
                IronUI.PE.Show();
            }
        }

        private void DiffTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronUI.OpenDiffWindow();
        }

        private void EncodeDecodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronUI.OpenEncodeDecodeWindow();
        }

        private void InteractiveShellPromptBox_Enter(object sender, EventArgs e)
        {
            InteractiveShellIn.Focus();
        }

        private void CopyRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Could not copy");
            IronLog.CopyRequest(GetSource(), GetID());
        }

        private void CopyResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Could not copy");
            IronLog.CopyResponse(GetSource(), GetID());
        }

        Request GetSiteMapLogGridRequest()
        {
            if (SiteMapLogGrid.SelectedCells.Count < 1 || SiteMapLogGrid.SelectedCells[0].Value == null)
            {
                return null;
            }
            try
            {
                int id = Int32.Parse(SiteMapLogGrid.SelectedCells[0].Value.ToString());
                Request Req = null;
                string Source = SiteMapLogGrid.SelectedCells[1].Value.ToString();
                switch (Source)
                {
                    case "Proxy":
                        Req = Request.FromProxyLog(id);
                        break;
                    case "Test":
                        Req = Request.FromTestLog(id);
                        break;
                    case "Shell":
                        Req = Request.FromShellLog(id);
                        break;
                    case "Probe":
                        Req = Request.FromProbeLog(id);
                        break;
                    case "Scan":
                        Req = Request.FromScanLog(id);
                        break;
                }
                return Req;
            }
            catch (Exception Exp)
            {
                IronException.Report("Error loading Request from SiteMapLogGrid", Exp.Message, Exp.StackTrace);
                return null;
            }
        }

        private void ConfigSetAsSystemProxyCB_Click(object sender, EventArgs e)
        {
            IronProxy.SystemProxy = ConfigSetAsSystemProxyCB.Checked;
            if (ConfigSetAsSystemProxyCB.Checked)
            {
                Fiddler.FiddlerApplication.oProxy.Attach();
            }
            else
            {
                Fiddler.FiddlerApplication.oProxy.Detach();
            }
        }

        private void ImportBurpLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IronUI.IsImportFormOpen())
            {
                return;
            }
            while (BurpLogOpenDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo Info = new FileInfo(BurpLogOpenDialog.FileName);
                Import.ImportBurpLog(Info.FullName);
                break;
            }
        }

        private void StopAllScansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(Scanner.StopAll);
            T.Start();
        }

        private void StartAllStoppedAndAbortedScansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> ToStart = new List<int>();
            foreach (DataGridViewRow Row in ASQueueGrid.Rows)
            {
                string Status = Row.Cells[1].Value.ToString();
                if (Status.Equals("Stopped") || Status.Equals("Aborted"))
                {
                    try
                    {
                        int ScanID = Int32.Parse(Row.Cells[0].Value.ToString());
                        ToStart.Add(ScanID);
                    }
                    catch
                    { }
                }
            }

            Thread T = new Thread(Scanner.StartList);
            T.Start(ToStart);
        }

        private void StopThisScanJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ASQueueGrid.SelectedCells.Count < 1 || ASQueueGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            try
            {
                string Status = ASQueueGrid.SelectedCells[1].Value.ToString();
                if (Status.Equals("Running"))
                {
                    int ScanID = Int32.Parse(ASQueueGrid.SelectedCells[0].Value.ToString());
                    Scanner.ScanThreads[ScanID].Abort();
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to Stop a Active Scan Job", Exp.Message, Exp.StackTrace);
            }
        }

        private void ScanQueueMenu_Opening(object sender, CancelEventArgs e)
        {
            StopThisScanJobToolStripMenuItem.Enabled = false;
            if (ASQueueGrid.SelectedCells.Count < 1 || ASQueueGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            else
            {
                try
                {
                    string Status = ASQueueGrid.SelectedCells[1].Value.ToString();
                    if (Status.Equals("Running"))
                    {
                        StopThisScanJobToolStripMenuItem.Enabled = true;
                    }
                }
                catch { }
            }
        }

        private void TraceGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (TraceGrid.SelectedCells.Count < 1 || TraceGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            try
            {
                int ID = Int32.Parse(TraceGrid.SelectedCells[0].Value.ToString());
                string Message = IronDB.GetTraceMessage(ID);
                StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
                SB.Append(Tools.RtfSafe(Message));
                TraceMsgRTB.Rtf = SB.ToString();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error reading Trace Message from DB", Exp.Message, Exp.StackTrace);
            }
        }

        private void ScanTraceGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (ScanTraceGrid.SelectedCells.Count < 1 || ScanTraceGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            try
            {
                int ID = Int32.Parse(ScanTraceGrid.SelectedCells[0].Value.ToString());
                string Message = IronDB.GetScanTraceMessage(ID);
                StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
                SB.Append(Tools.RtfSafe(Message));
                ScanTraceMsgRTB.Rtf = SB.ToString();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error reading ScanTrace Message from DB", Exp.Message, Exp.StackTrace);
            }
        }

        private void ShowLogGridBtn_Click(object sender, EventArgs e)
        {
            if (ShowLogGridBtn.Text.Equals("Hide Log Grids"))
            {
                IronUI.LogGridStatus(false);
            }
            else
            {
                IronUI.LogGridStatus(true);
            }
        }

        private void MTRedGroupBtn_Click(object sender, EventArgs e)
        {
            TestIDLbl.BackColor = Color.Red;
            TestIDLbl.Text = "ID: 0";
            ManualTesting.CurrentGroup = "Red";
            IronUI.ResetMTDisplayFields();
            IronUI.UpdateTestGroupLogGrid(ManualTesting.RedGroupSessions);
            ManualTesting.ShowSession(ManualTesting.RedGroupID);
            if (ManualTesting.RedGroupID == 0) MTReqResTabs.SelectTab("MTRequestTab");
        }

        private void MTGreenGroupBtn_Click(object sender, EventArgs e)
        {
            TestIDLbl.BackColor = Color.Green;
            TestIDLbl.Text = "ID: 0";
            ManualTesting.CurrentGroup = "Green"; 
            IronUI.ResetMTDisplayFields();
            IronUI.UpdateTestGroupLogGrid(ManualTesting.GreenGroupSessions);
            ManualTesting.ShowSession(ManualTesting.GreenGroupID);
            if (ManualTesting.GreenGroupID == 0) MTReqResTabs.SelectTab("MTRequestTab");
        }

        private void MTBlueGroupBtn_Click(object sender, EventArgs e)
        {
            TestIDLbl.BackColor = Color.RoyalBlue;
            TestIDLbl.Text = "ID: 0";
            ManualTesting.CurrentGroup = "Blue";
            IronUI.ResetMTDisplayFields();
            IronUI.UpdateTestGroupLogGrid(ManualTesting.BlueGroupSessions);
            ManualTesting.ShowSession(ManualTesting.BlueGroupID);
            if (ManualTesting.BlueGroupID == 0) MTReqResTabs.SelectTab("MTRequestTab");
        }

        private void MTGrayGroupBtn_Click(object sender, EventArgs e)
        {
            TestIDLbl.BackColor = Color.Silver;
            TestIDLbl.Text = "ID: 0";
            ManualTesting.CurrentGroup = "Gray";
            IronUI.ResetMTDisplayFields();
            IronUI.UpdateTestGroupLogGrid(ManualTesting.GrayGroupSessions);
            ManualTesting.ShowSession(ManualTesting.GrayGroupID);
            if (ManualTesting.GrayGroupID == 0) MTReqResTabs.SelectTab("MTRequestTab");
        }

        private void MTBrownGroupBtn_Click(object sender, EventArgs e)
        {
            TestIDLbl.BackColor = Color.Chocolate;
            TestIDLbl.Text = "ID: 0";
            ManualTesting.CurrentGroup = "Brown";
            IronUI.ResetMTDisplayFields();
            IronUI.UpdateTestGroupLogGrid(ManualTesting.BrownGroupSessions);
            ManualTesting.ShowSession(ManualTesting.BrownGroupID);
            if (ManualTesting.BrownGroupID == 0) MTReqResTabs.SelectTab("MTRequestTab");
        }

        private void NextLogBtn_Click(object sender, EventArgs e)
        {
            IronLog.ShowNextLog();
        }

        private void PreviousLogBtn_Click(object sender, EventArgs e)
        {
            IronLog.ShowPreviousLog();
        }

        private void TestGroupLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TestGroupLogGrid.SelectedCells.Count < 1 || TestGroupLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            IronUI.ResetMTDisplayFields();
            try
            {
                int ID = Int32.Parse(TestGroupLogGrid.SelectedCells[0].Value.ToString());
                Session IrSe = null;
                if (ManualTesting.RedGroupSessions.ContainsKey(ID))
                {
                    IrSe = ManualTesting.RedGroupSessions[ID];
                    IrSe.Flags["Group"] = "Red";
                }
                else if (ManualTesting.BlueGroupSessions.ContainsKey(ID))
                {
                    IrSe = ManualTesting.BlueGroupSessions[ID];
                    IrSe.Flags["Group"] = "Blue";
                }
                else if (ManualTesting.GreenGroupSessions.ContainsKey(ID))
                {
                    IrSe = ManualTesting.GreenGroupSessions[ID];
                    IrSe.Flags["Group"] = "Green";
                }
                else if (ManualTesting.GrayGroupSessions.ContainsKey(ID))
                {
                    IrSe = ManualTesting.GrayGroupSessions[ID];
                    IrSe.Flags["Group"] = "Gray";
                }
                else if (ManualTesting.BrownGroupSessions.ContainsKey(ID))
                {
                    IrSe = ManualTesting.BrownGroupSessions[ID];
                    IrSe.Flags["Group"] = "Brown";
                }
                if (IrSe == null)
                    MTReqResTabs.SelectTab("MTRequestTab");
                else
                {
                    IronUI.FillMTFields(IrSe);
                    if (IrSe.Flags.ContainsKey("Reflecton"))
                        IronUI.FillTestReflection(IrSe.Flags["Reflecton"].ToString());
                    else
                        IronUI.FillTestReflection("");
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Error displaying Request from Test log", Exp.Message, Exp.StackTrace);
                IronUI.ShowMTException("Unable to display Request");
            }
        }

        private void NextTestLog_Click(object sender, EventArgs e)
        {
            ManualTesting.ShowNextSession();
        }

        private void PreviousTestLog_Click(object sender, EventArgs e)
        {
            ManualTesting.ShowPreviousSession();
        }

        private void RedGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForTesting(GetSource(), GetID(),"Red");
        }

        private void GreenGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForTesting(GetSource(), GetID(), "Green");
        }

        private void BlueGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForTesting(GetSource(), GetID(), "Blue");
        }

        private void GrayGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForTesting(GetSource(), GetID(), "Gray");
        }

        private void BrownGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForTesting(GetSource(), GetID(), "Brown");
        }

        private void ConfigureScanRequestSSLCB_Click(object sender, EventArgs e)
        {
            if (Scanner.CurrentScanner != null)
            {
                if (Scanner.CurrentScanner.OriginalRequest != null)
                {
                    Scanner.CurrentScanner.OriginalRequest.SSL = ConfigureScanRequestSSLCB.Checked;
                }
            }
        }

        private void ConsoleStartScanBtn_Click(object sender, EventArgs e)
        {
            IronUI.ShowConsoleStatus("",false);
            try
            {
                if (IronUI.IsConfiguredScanFormOpen())
                {
                    IronUI.CSF.Close();
                }
            }
            catch { }
            if (ConsoleStartScanBtn.Text.Equals("Start Scan"))
            {
                try
                {
                    Request Req = new Request(ConsoleScanUrlTB.Text);
                    if (OptimalScanModeRB.Checked)
                    {
                        ScanManager.PrimaryHost = Req.Host;
                        if (Req.SSL)
                        {
                            ScanManager.HTTPS = true;
                            ScanManager.HTTP = false;
                        }
                        else
                        {
                            ScanManager.HTTP = true;
                            ScanManager.HTTPS = false;
                        }
                        ScanManager.StartingUrl = Req.Url;
                        ScanManager.StartScan();
                        IronUI.UpdateConsoleControlsStatus(true);
                    }
                    else
                    {
                        IronUI.ShowConfiguredScanForm(Req);
                    }
                }
                catch (Exception Exp)
                {
                    IronUI.ShowConsoleStatus(Exp.Message, true);
                    return;
                }
            }
            else
            {
                ConsoleStartScanBtn.Enabled = false;
                IronUI.ShowConsoleStatus("Stopping Scan...", false);
                ScanManager.Stop();
            }
        }

        private void ViewProxyLogLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            main_tab.SelectTab("mt_logs");
            LogTabs.SelectTab("ProxyLogTab");
            IronUI.LogGridStatus(true);
        }

        private void JSTaintTraceControlBtn_Click(object sender, EventArgs e)
        {
            if (JSTaintTraceControlBtn.Text.Equals("Start Taint Trace"))
            {
                JSTaintResultGrid.Rows.Clear();
                IronJint.PauseAtTaint = PauseAtTaintCB.Checked;
                JSTaintTraceControlBtn.Text = "Stop Trace";
                PauseAtTaintCB.Visible = PauseAtTaintCB.Checked;
                
                List<string> SourceObjects = new List<string>();
                List<string> SinkObjects = new List<string>();
                List<string> SourceReturningMethods = new List<string>();
                List<string> SinkReturningMethods = new List<string>();
                List<string> ArgumentReturningMethods = new List<string>();
                List<string> ArgumentAssignedASourceMethods = new List<string>();
                List<string> ArgumentAssignedToSinkMethods = new List<string>();

                foreach (DataGridViewRow Row in JSTaintConfigGrid.Rows)
                {
                    if (Row == null) continue;
                    if (Row.Cells == null) continue;
                    if (Row.Cells.Count < 7) continue;
                    if (Row.Cells["JSTaintDefaultSourceObjectsColumn"].Value != null)
                    {
                        string SourceObject = Row.Cells["JSTaintDefaultSourceObjectsColumn"].Value.ToString().Trim();
                        if (SourceObject.Length > 0) SourceObjects.Add(SourceObject);
                    }
                    if (Row.Cells["JSTaintDefaultSinkObjectsColumn"].Value != null)
                    {
                        string SinkObject = Row.Cells["JSTaintDefaultSinkObjectsColumn"].Value.ToString().Trim();
                        if (SinkObject.Length > 0) SinkObjects.Add(SinkObject);
                    }
                    if (Row.Cells["JSTaintDefaultArgumentAssignedASourceMethodsColumn"].Value != null)
                    {
                        string ArgumentAssignedASourceMethod = Row.Cells["JSTaintDefaultArgumentAssignedASourceMethodsColumn"].Value.ToString().Trim();
                        if (ArgumentAssignedASourceMethod.Length > 0) ArgumentAssignedASourceMethods.Add(ArgumentAssignedASourceMethod);
                    }
                    if (Row.Cells["JSTaintDefaultArgumentAssignedToSinkMethodsColumn"].Value != null)
                    {
                        string ArgumentAssignedToSinkMethod = Row.Cells["JSTaintDefaultArgumentAssignedToSinkMethodsColumn"].Value.ToString().Trim();
                        if (ArgumentAssignedToSinkMethod.Length > 0) ArgumentAssignedToSinkMethods.Add(ArgumentAssignedToSinkMethod);
                    }
                    if (Row.Cells["JSTaintDefaultSourceReturningMethodsColumn"].Value != null)
                    {
                        string SourceReturningMethod = Row.Cells["JSTaintDefaultSourceReturningMethodsColumn"].Value.ToString().Trim();
                        if (SourceReturningMethod.Length > 0) SourceReturningMethods.Add(SourceReturningMethod);
                    }
                    if (Row.Cells["JSTaintDefaultSinkReturningMethodsColumn"].Value != null)
                    {
                        string SinkReturningMethod = Row.Cells["JSTaintDefaultSinkReturningMethodsColumn"].Value.ToString().Trim();
                        if (SinkReturningMethod.Length > 0) SinkReturningMethods.Add(SinkReturningMethod);
                    }
                    if (Row.Cells["JSTaintDefaultArgumentReturningMethodsColumn"].Value != null)
                    {
                        string ArgumentReturningMethod = Row.Cells["JSTaintDefaultArgumentReturningMethodsColumn"].Value.ToString().Trim();
                        if (ArgumentReturningMethod.Length > 0) ArgumentReturningMethods.Add(ArgumentReturningMethod);
                    }
                }

                IronJint.StartTraceFromUI(JSTaintTraceInRTB.Text, SourceObjects, SinkObjects, SourceReturningMethods, SinkReturningMethods, ArgumentReturningMethods, ArgumentAssignedASourceMethods, ArgumentAssignedToSinkMethods);
            }
            else
            {
                IronJint.StopUITrace();
                IronUI.ResetTraceStatus();
                IronUI.ShowTraceStatus("Trace Stopped", false);
            }
        }

        private void JSTaintTabs_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == null) return;
            if (e.TabPage.Name.Equals("JSTaintResultTab"))
            {
                TaintTraceResultSinkLegendTB.Visible = true;
                TaintTraceResultSourceLegendTB.Visible = true;
                TaintTraceResultSourcePlusSinkLegendTB.Visible = true;
                TaintTraceResultSourceToSinkLegendTB.Visible = true;
                JSTaintShowLinesLbl.Visible = true;
                JSTaintShowCleanCB.Visible = true;
                JSTaintShowSourceCB.Visible = true;
                JSTaintShowSinkCB.Visible = true;
                JSTaintShowSourceToSinkCB.Visible = true;
            }
            else
            {
                TaintTraceResultSinkLegendTB.Visible = false;
                TaintTraceResultSourceLegendTB.Visible = false;
                TaintTraceResultSourcePlusSinkLegendTB.Visible = false;
                TaintTraceResultSourceToSinkLegendTB.Visible = false;
                JSTaintShowLinesLbl.Visible = false;
                JSTaintShowCleanCB.Visible = false;
                JSTaintShowSourceCB.Visible = false;
                JSTaintShowSinkCB.Visible = false;
                JSTaintShowSourceToSinkCB.Visible = false;
            }
        }

        private void JSTainTraceEditMenu_Opening(object sender, CancelEventArgs e)
        {
            AddSourceTaintToolStripMenuItem.Visible = false;
            AddSinkTaintToolStripMenuItem.Visible = false;
            RemoveSourceTaintToolStripMenuItem.Visible = false;
            RemoveSinkTaintToolStripMenuItem.Visible = false;

            if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null) return;

            if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.Orange)
            {
                RemoveSourceTaintToolStripMenuItem.Visible = true;
                AddSinkTaintToolStripMenuItem.Visible = true;
            }
            else if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.HotPink)
            {
                RemoveSinkTaintToolStripMenuItem.Visible = true;
                AddSourceTaintToolStripMenuItem.Visible = true;
            }
            else if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.IndianRed)
            {
                RemoveSinkTaintToolStripMenuItem.Visible = true;
                RemoveSourceTaintToolStripMenuItem.Visible = true;
            }
            else if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.Red)
            {
                RemoveSinkTaintToolStripMenuItem.Visible = true;
                RemoveSourceTaintToolStripMenuItem.Visible = true;
            }
            else
            {
                AddSinkTaintToolStripMenuItem.Visible = true;
                AddSourceTaintToolStripMenuItem.Visible = true;
            }
        }

        private void AddSourceTaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LineNo = GetLineFromTaintGrid();
            if (LineNo == 0) return;
            if (!IronJint.SourceLinesToInclude.Contains(LineNo))IronJint.SourceLinesToInclude.Add(LineNo);
            if (IronJint.SourceLinesToIgnore.Contains(LineNo)) IronJint.SourceLinesToIgnore.Remove(LineNo);
            IronJint.ReDoTraceFromUI();
        }

        private void AddSinkTaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LineNo = GetLineFromTaintGrid();
            if (LineNo == 0) return;
            if (!IronJint.SinkLinesToInclude.Contains(LineNo)) IronJint.SinkLinesToInclude.Add(LineNo);
            if (IronJint.SinkLinesToIgnore.Contains(LineNo)) IronJint.SinkLinesToIgnore.Remove(LineNo);
            IronJint.ReDoTraceFromUI();
        }

        private void RemoveSourceTaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LineNo = GetLineFromTaintGrid();
            if (LineNo == 0) return;
            if(!IronJint.SourceLinesToIgnore.Contains(LineNo)) IronJint.SourceLinesToIgnore.Add(LineNo);
            if (IronJint.SourceLinesToInclude.Contains(LineNo)) IronJint.SourceLinesToInclude.Remove(LineNo);
            IronJint.ReDoTraceFromUI();
        }

        private void RemoveSinkTaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LineNo = GetLineFromTaintGrid();
            if (LineNo == 0) return;
            if (!IronJint.SinkLinesToIgnore.Contains(LineNo)) IronJint.SinkLinesToIgnore.Add(LineNo);
            if (IronJint.SinkLinesToInclude.Contains(LineNo)) IronJint.SinkLinesToInclude.Remove(LineNo);
            IronJint.ReDoTraceFromUI();
        }

        private int GetLineFromTaintGrid()
        {
            if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null) return 0;

            try
            {
                int LineNo = Int32.Parse(JSTaintResultGrid.SelectedCells[0].Value.ToString());
                return LineNo;
            }
            catch
            {
                return 0;
            }
        }

        private void PauseAtTaintCB_Click(object sender, EventArgs e)
        {
            IronJint.PauseAtTaint = PauseAtTaintCB.Checked;
        }

        private void JSTaintContinueBtn_Click(object sender, EventArgs e)
        {
            IronJint.UIIJ.MSR.Set();
            JSTaintContinueBtn.Visible = false;
            JSTaintResultGrid.Focus();
        }

        private void JSTaintResultGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            try
            {
                int LineNo = Int32.Parse(JSTaintResultGrid.SelectedCells[0].Value.ToString());
                IronUI.ShowTaintReasons(LineNo, IronJint.UIIJ.GetSourceReasons(LineNo), IronJint.UIIJ.GetSinkReasons(LineNo));
            }catch{}
        }

        private void ConfigViewHideLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ConfigPanel.Visible == true)
            {
                ConfigPanel.Visible = false;
                ConfigViewHideLL.Text = "Show Config";
            }
            else
            {
                ConfigPanel.Height = 350;
                ConfigPanel.Visible = true;
                ConfigViewHideLL.Text = "Hide Config";
            }
        }

        private void JSTaintConfigShowHideBtn_Click(object sender, EventArgs e)
        {
            JSTaintShowLinesLbl.Visible = JSTaintShowCleanCB.Visible = JSTaintShowSourceCB.Visible = JSTaintShowSinkCB.Visible = JSTaintShowSourceToSinkCB.Visible = JSTaintConfigPanel.Visible;
            if (JSTaintConfigPanel.Visible == true)
            {
                JSTaintConfigPanel.Visible = false;
                JSTaintConfigShowHideBtn.Text = "Show Taint Config";
            }
            else
            {
                JSTaintConfigPanel.Height = 450;
                JSTaintConfigGrid.Height = 400;
                JSTaintConfigPanel.Visible = true;
                JSTaintConfigShowHideBtn.Text = "Hide Taint Config";
            }
        }

        private void TaintTraceResetTaintConfigBtn_Click(object sender, EventArgs e)
        {
            IronJint.ShowDefaultTaintConfig();
        }

        private void ConsoleStatusTB_Enter(object sender, EventArgs e)
        {
            ConsoleScanUrlTB.Focus();
        }

        private void ProbeLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProbeLogGrid.SelectedCells.Count < 1 || ProbeLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Probe, ProbeLogGrid.SelectedCells[0].Value.ToString());
            return;
        }

        private void ConfigJSTaintConfigApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Config.UpdateJSTaintConfigFromUI();
            IronDB.StoreJSTaintConfig();
        }

        private void ConfigJSTaintConfigCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdateJSTaintConfigInUIFromConfig();
        }

        private void TaintTraceClearTaintConfigBtn_Click(object sender, EventArgs e)
        {
            JSTaintConfigGrid.Rows.Clear();
        }

        private void ConfigScannerThreadMaxCountTB_Scroll(object sender, EventArgs e)
        {
            ConfigScannerThreadMaxCountLbl.Text = ConfigScannerThreadMaxCountTB.Value.ToString();
        }

        private void SelectResponseForJavaScriptTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForJavaScriptTesting(GetSource(), GetID());
        }

        private void ConfigCrawlerThreadMaxCountTB_Scroll(object sender, EventArgs e)
        {
            ConfigCrawlerThreadMaxCountLbl.Text = ConfigCrawlerThreadMaxCountTB.Value.ToString();
        }

        private void ConfigScannerSettingsApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Config.UpdateScannerSettingsFromUI();
            IronDB.StoreScannerSettings();
        }

        private void ConfigScannerSettingsCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdateScannerSettingsInUIFromConfig();
        }

        private void RenderHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread RH = new Thread(RenderHtmlOpener);
            RH.Start();
        }

        void RenderHtmlOpener()
        {
            try
            {
                Tools.Run(Config.RootDir + "/" + "RenderHtml.exe");
            }
            catch (Exception Exp) { IronException.Report("Unable to Open RenderHtml", Exp); }
        }

        private void JSTaintShowCleanCB_CheckedChanged(object sender, EventArgs e)
        {
            IronUI.SetJSTaintTraceResult();
        }

        private void JSTaintShowSourceCB_CheckedChanged(object sender, EventArgs e)
        {
            IronUI.SetJSTaintTraceResult();
        }

        private void JSTaintShowSinkCB_CheckedChanged(object sender, EventArgs e)
        {
            IronUI.SetJSTaintTraceResult();
        }

        private void JSTaintShowSourceToSinkCB_CheckedChanged(object sender, EventArgs e)
        {
            IronUI.SetJSTaintTraceResult();
        }

        private void CopyLineTaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null) return;
            try
            {
                Clipboard.SetText("Copy Failed!");
                string Line = JSTaintResultGrid.SelectedCells[1].Value.ToString();
                Clipboard.SetText(Line);
            }
            catch
            {}
        }

        private void LogOptionsBtn_Click(object sender, EventArgs e)
        {
            LogOptionsBtn.ContextMenuStrip.Show(LogOptionsBtn, new Point(0, LogOptionsBtn.Height));
        }

        private void ProxyOptionsBtn_Click(object sender, EventArgs e)
        {
            ProxyOptionsBtn.ContextMenuStrip.Show(ProxyOptionsBtn, new Point(0, LogOptionsBtn.Height));
        }

        private void PauseAtTaintCB_CheckedChanged(object sender, EventArgs e)
        {
            JSTaintShowCleanCB.Enabled = !PauseAtTaintCB.Checked;
            JSTaintShowSourceCB.Enabled = !PauseAtTaintCB.Checked;
            JSTaintShowSinkCB.Enabled = !PauseAtTaintCB.Checked;
            JSTaintShowSourceToSinkCB.Enabled = !PauseAtTaintCB.Checked;
        }

        private void ConfigPanelTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConfigPanelTabs.SelectedTab.Name == "ConfigTaintConfigTab")
            {
                if (ConfigDefaultJSTaintConfigGrid.Height != 290)
                {
                    ConfigDefaultJSTaintConfigGrid.Height = 290;
                }
            }
        }

        private void ConfigPassiveAnalysisSettingsApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Config.UpdatePassiveAnalysisSettingsFromUI();
            IronDB.StorePassiveAnalysisSettings();
        }

        private void ConfigPassiveAnalysisSettingsCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdatePassiveAnalysisSettingsInUIFromConfig();
        }

        private void ClearShellDisplayBtn_Click(object sender, EventArgs e)
        {
            InteractiveShellOut.Text = "";
        }
    }
}

