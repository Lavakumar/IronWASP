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

            IronUI.LF.ShowLoadMessage("Loading.....");
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


            IronUI.LF.ShowLoadMessage("Creating Project Database....");
            try
            {
                Config.SetRootDir();
                IronDB.InitialiseLogDB();
            }
            catch(Exception Exp)
            {
                IronUI.LF.ShowLoadMessage("Error Creating Project Database - " + Exp.Message);
            }

            IronUI.LF.ShowLoadMessage("Reading Stored Configuration Information....");
            try
            {
                IronDB.UpdateConfigFromDB();
                Config.ReadUserAgentsList();
                CreateImageList();
            }
            catch (Exception Exp)
            {
                IronUI.LF.ShowLoadMessage("Error Reading Stored Configuration Information - " + Exp.Message);
            }

            IronUI.LF.ShowLoadMessage("Applying Previous Configuration....");
            try
            {
                IronUI.UpdateUIFromConfig();
                //IronJint.ShowDefaultTaintConfig();
            }
            catch (Exception Exp)
            {
                IronUI.LF.ShowLoadMessage("Error Applying Previous Configuration - " + Exp.Message);
            }

            IronUI.LF.ShowLoadMessage("Creating API Documentation Trees....");
            try
            {
                APIDoc.Initialise();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error creating API Docs", Exp.Message, Exp.StackTrace);
            }
            //Initialise the Scripting Engines and compile the plug-ins
            IronUI.LF.ShowLoadMessage("Loading All Plugins....");
            try
            {
                PluginEngine.StartUp = true;
                PluginEngine.InitialiseAllPlugins();
                PluginEngine.StartUp = false;
                
            }
            catch (Exception Exp)
            {
                PluginEngine.StartUp = false;
                IronException.Report("Error initialising Plugins", Exp.Message, Exp.StackTrace);
            }

            IronUI.LF.ShowLoadMessage("Reading available Modules....");
            try
            {
                try
                {
                    string ModulesDir = string.Format("{0}\\modules", Config.RootDir);
                    if (!Directory.Exists(ModulesDir))
                        Directory.CreateDirectory(ModulesDir);
                }
                catch { }
                Module.ReadModulesXml();
                PopulateModuleMenus();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error reading available Modules", Exp.Message, Exp.StackTrace);
            }
            try
            {
                IronUI.BuildPluginTree();
            }
            catch (Exception Exp) { IronException.Report("Error Building PluginTree", Exp); }
            IronUI.LF.ShowLoadMessage("Starting Internal Analyzers....");
            try
            {
                PassiveChecker.Start();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Internal Analyzers", Exp.Message, Exp.StackTrace);
            }


            IronUI.LF.ShowLoadMessage("Preparing the Scripting Shell....");
            try
            {
                IronScripting.InitialiseScriptingEnvironment();
                IronUI.InitialiseAllScriptEditors();
                try
                {
                    ShowSampleScriptedInterceptionScript();
                    ShowSampleScriptedSendScript();
                }
                catch { }
            }
            catch (Exception Exp)
            {
                IronUI.LF.ShowLoadMessage("Error Preparing the Scripting Shell - " + Exp.Message);
                IronException.Report("Error Preparing the Scripting Shell", Exp.Message, Exp.StackTrace);
            }


            //IronUI.LF.ShowLoadMessage("Starting the Proxy");
            //IronProxy.Start();

            IronUI.LF.ShowLoadMessage("Done!");
            IronUI.LF.ShowLoadMessage("0");

            IronUI.LICF = new LoadInitialConfigurationForm();
            IronUI.LICF.ShowDialog();

            if (CanShutdown)
            {
                this.ShutDown();
                Application.Exit();
            }

            try
            {
                CheckUpdate.CheckForUpdates();
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Starting New Version Check", Exp.Message, Exp.StackTrace);
            }

            SetUiComponentsToInitialState();
            this.Activate();
            PluginEditorInTE.Document.ReadOnly = true;
            CheckDotNetVersion();
        }

        static void Splash()
        {
            IronUI.LF.LoadLogoPB.Select();
            IronUI.LF.ShowDialog();
        }

        void CheckDotNetVersion()
        {
            bool OldVersion = false;
            try
            {
                string[] VersionParts = Environment.Version.ToString().Split(new char[] { '.' });
                if (Int32.Parse(VersionParts[2]) < 50000)
                {
                    OldVersion = true;
                }
                if (Int32.Parse(VersionParts[3]) < 3000)
                {
                    OldVersion = true;
                }
            }
            catch
            {}
            if (OldVersion)
            {
                MessageBox.Show("You are running an older version of .NET 2.0 that does not support all features of IronWASP. Please install .NET 2.0 SP2, it can be downloaded from - https://www.microsoft.com/en-us/download/details.aspx?id=1639", "Dependency Alert!!!");
            }
        }

        void SetUiComponentsToInitialState()
        {
            ScanTopPanel.Visible = true;
            ScanDisplayPanel.Visible = false;
            ScanJobsBaseSplit.SplitterDistance = 62;

            //TestResponseSplit.SplitterDistance = 30;
            ScanJobsTopSplit.SplitterDistance = 470;
            ScanJobsBottomSplit.SplitterDistance = ScanJobsBottomSplit.Height - 52;

            ASInjectHeaderLbl.Location = new Point(13, ASInjectHeaderLbl.Location.Y);
            ASRequestScanAllCB.Location = new Point(13, ASRequestScanAllCB.Location.Y);
            ASRequestScanURLCB.Location = new Point(13, ASRequestScanURLCB.Location.Y);
            ASRequestScanQueryCB.Location = new Point(13, ASRequestScanQueryCB.Location.Y);
            ASRequestScanBodyCB.Location = new Point(13, ASRequestScanBodyCB.Location.Y);
            ASRequestScanCookieCB.Location = new Point(13, ASRequestScanCookieCB.Location.Y);
            ASRequestScanHeadersCB.Location = new Point(13, ASRequestScanHeadersCB.Location.Y);
            ASRequestScanParameterNamesCB.Location = new Point(13, ASRequestScanParameterNamesCB.Location.Y);
        }

        void PopulateModuleMenus()
        {
            Dictionary<string, List<Module>> MenuItems = new Dictionary<string, List<Module>>();
            foreach(Module M in Module.ModuleListFromXml)
            {
                if (!MenuItems.ContainsKey(M.Category))
                    MenuItems[M.Category] = new List<Module>();
                MenuItems[M.Category].Add(M);
            }
            foreach (string Category in MenuItems.Keys)
            {
                ToolStripMenuItem Item = (ToolStripMenuItem)modulesToolStripMenuItem.DropDownItems.Add(Category);
                bool WorksOnFinding = false;
                bool WorksOnUrl = false;
                bool WorksOnSession = false;
                foreach (Module M in MenuItems[Category])
                {
                    Item.DropDownItems.Add(M.DisplayName).Click += RunModuleMenuItem_Click;
                    if (M.WorksOnFinding) WorksOnFinding = true;
                    if (M.WorksOnUrl) WorksOnUrl = true;
                    if (M.WorksOnSession) WorksOnSession = true;
                }
                if (WorksOnFinding)
                {
                    Item = (ToolStripMenuItem)RunModulesOnFindingToolStripMenuItem.DropDownItems.Add(Category);
                    foreach (Module M in MenuItems[Category])
                    {
                        if (M.WorksOnFinding) Item.DropDownItems.Add(M.DisplayName).Click += RunModuleOnFindingMenuItem_Click;
                    }
                }
                if (WorksOnUrl)
                {
                    Item = (ToolStripMenuItem)RunModulesOnUrlToolStripMenuItem.DropDownItems.Add(Category);
                    foreach (Module M in MenuItems[Category])
                    {
                        if (M.WorksOnUrl) Item.DropDownItems.Add(M.DisplayName).Click += RunModuleOnUrlMenuItem_Click;
                    }
                }
                if (WorksOnSession)
                {
                    Item = (ToolStripMenuItem)RunModulesOnRequestResponseToolStripMenuItem.DropDownItems.Add(Category);
                    foreach (Module M in MenuItems[Category])
                    {
                        if (M.WorksOnSession) Item.DropDownItems.Add(M.DisplayName).Click += RunModuleOnSessionMenuItem_Click;
                    }
                }
            }
        }

        internal void PopulateRecentOnSessionModuleMenus(string[] RecentModules)
        {
            while (LogMenu.Items.Count > 6)
            {
                LogMenu.Items.RemoveAt(LogMenu.Items.Count -1);
            }
            foreach (string DisplayName in RecentModules)
            {
                LogMenu.Items.Add(DisplayName).Click += RunModuleOnSessionMenuItem_Click;
            }
        }

        private void RunModuleMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString() + e.ToString());
            Module.StartModule((sender as ToolStripMenuItem).Text);
        }

        private void RunModuleOnUrlMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RunModuleOnFindingMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString() + e.ToString());
            int FindingId = IronUI.GetFindingIdFromIronTree();
            try
            {
                Module.StartModuleOnFinding((sender as ToolStripMenuItem).Text, FindingId);
            }
            catch (Exception Exp) { IronException.Report("Unable to run Module on Finding", Exp); }
            //IronUI.OpenEncodeDecodeWindow();
        }
        private void RunModuleOnSessionMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString() + e.ToString());
            try
            {
                string SelectedDisplayName = (sender as ToolStripMenuItem).Text;
                bool AlreadyExists = false;
                foreach (string DisplayName in Module.RecentOnSessionModules)
                {
                    if (DisplayName.Equals(SelectedDisplayName))
                    {
                        AlreadyExists = true;
                        break;
                    }
                }
                if (!AlreadyExists)
                {
                    if (Module.RecentOnSessionModules.Count >= 3)
                    {
                        Module.RecentOnSessionModules.Dequeue();
                    }
                    Module.RecentOnSessionModules.Enqueue(SelectedDisplayName);
                }
                PopulateRecentOnSessionModuleMenus(Module.RecentOnSessionModules.ToArray());
                Module.StartModuleOnSession(SelectedDisplayName, GetSource(), Int32.Parse(GetID()));
            }
            catch (Exception Exp) { IronException.Report("Unable to run Module on Session", Exp); }
            //IronUI.OpenEncodeDecodeWindow();
        }


        private void ProxySendBtn_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            if (IronProxy.CurrentSession == null) return;
            if (IronProxy.CurrentSession.FiddlerSession == null) return;
            if (IronProxy.CurrentSession.FiddlerSession.state == Fiddler.SessionStates.HandTamperRequest)
            {
                try
                {
                    IronUI.ResetProxyException();
                    Request UpdatedRequest = ProxyRequestView.GetRequest();
                    if (UpdatedRequest == null) throw new Exception("Cannot forward invalid request");
                    if(IronProxy.RequestChanged)
                        IronProxy.UpdateCurrentSessionWithNewRequest(UpdatedRequest);
                    //IronUI.HandleAnyChangesInRequest();
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
                    Response UpdatedResponse = ProxyResponseView.GetResponse();
                    if (UpdatedResponse == null) throw new Exception("Cannot forward invalid response");
                    if (IronProxy.ResponseChanged)
                        IronProxy.UpdateCurrentSessionWithNewResponse(UpdatedResponse);
                    //IronUI.HandleAnyChangesInResponse();
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
            ProxyBaseSplit.Panel1.BackColor = Color.White;
            ProxySendBtn.Enabled = false;
            ProxyDropBtn.Enabled = false;
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

        private void InteractiveShellIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void InteractiveShellIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.InteractiveShellIn.ReadOnly) return;
            if (Config.BlinkPrompt)
            {
                EndOfShellPromptBlink();
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
                    //this.InteractiveShellIn.Text = this.InteractiveShellIn.Text.Replace("\r\n", "");
                    this.InteractiveShellIn.Text = this.InteractiveShellIn.Text;
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
                    //this.InteractiveShellIn.Text = this.InteractiveShellIn.Text.Replace("\r\n", "");
                    this.InteractiveShellIn.Text = this.InteractiveShellIn.Text;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                IronUI.FreezeInteractiveShellUI();
                //string Command = this.InteractiveShellIn.Text.Replace("\r\n", "");
                string Command = this.InteractiveShellIn.Text;
                if(InteractiveShellPythonRB.Checked)
                {
                    if (InteractiveShellPromptBox.Text.Contains(">>"))
                    {
                        Command = Command.TrimStart();//Remove whitespace before Python commands from the interactive shell when indentation is not required. This is a common newbie mistake.
                    }
                }
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
                        if (TestGroupLogGrid.SelectedCells[6].Value == null) ResponseAvailable = false;
                    }
                    break;
                case ("OtherLogGrid"):
                    if (OtherLogGrid.SelectedCells.Count < 1 || OtherLogGrid.SelectedCells[0].Value == null) RowsSelected = false;
                    if (RowsSelected)
                    {
                        if (OtherLogGrid.SelectedCells[6].Value == null) ResponseAvailable = false;
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
            this.RunModulesOnRequestResponseToolStripMenuItem.Enabled = RowsSelected;
            this.SelectResponseForJavaScriptTestingToolStripMenuItem.Enabled = RowsSelected && ResponseAvailable;// && JSTaintTraceControlBtn.Text.Equals("Start Taint Trace");
        }

        private void MTSendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                IronUI.ResetMTExceptionFields();
                ManualTesting.CurrentRequest = TestRequestView.GetRequest();
                if (ManualTesting.CurrentRequest == null) throw new Exception("Cannot sent invalid request");
                //IronUI.HandleAnyChangesInMTRequest();
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
            if (!IronProxy.ProxyRunning)
            {
                IronUI.ShowMTException("Unable to send Request because IronWASP Proxy is not running. Start the proxy to fix this problem.");
                return;
            }
            try
            {
                IronUI.ResetMTExceptionFields();
                ManualTesting.ResetChangedStatus();
                ManualTesting.SendRequest();
                IronUI.StartMTSend(ManualTesting.CurrentRequestID);
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to Send 'Manual Testing' Request", Exp.Message, Exp.StackTrace);
                IronUI.ShowMTException("Error sending Request");
                IronUI.EndMTSend(true);
            }
        }


        private void MTLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TestLogGrid.SelectedCells.Count < 1 || TestLogGrid.SelectedCells[0].Value == null || TestLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Test, TestLogGrid.SelectedCells[0].Value.ToString(), TestLogGrid.SelectedRows[0].Index, false);
        }

        private void LogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProxyLogGrid.SelectedCells.Count < 1 || ProxyLogGrid.SelectedCells[0].Value == null || ProxyLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Proxy, ProxyLogGrid.SelectedCells[0].Value.ToString(), ProxyLogGrid.SelectedRows[0].Index, false);
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
                //ScanManager.Stop();
                ScanManager.DoStop();
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
            try
            {
                foreach (int ScanID in Scanner.ScanThreads.Keys)
                {
                    try
                    {
                        Scanner.ScanThreads[ScanID].Abort();
                    }
                    catch { }
                }
            }
            catch { }
            try
            {
                CheckUpdate.StopUpdateCheck();
            }catch{}
            try
            {
                IronDB.CommandsLogFile.Close();
            }
            catch { }
        }

        //private void MTIsSSLCB_CheckedChanged(object sender, EventArgs e)
        //{
        //    ManualTesting.CurrentRequestIsSSL = this.MTIsSSLCB.Checked;
        //}
        
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
            //if (e.Node == null) return;
            //if ((e.Node.Level > 5) || (e.Node.Level == 5 && (e.Node.Parent.Parent.Parent.Parent.Index == 4)) || (e.Node.Level == 4 && (e.Node.Parent.Parent.Parent.Index == 4)) || (e.Node.Level == 3 && (e.Node.Parent.Parent.Index == 4)) || (e.Node.Level == 2 && (e.Node.Parent.Index == 4)))
            //{
            //    List<string> UrlPaths = new List<string>();
            //    string Query = "";
            //    TreeNode SiteMapNode = e.Node;
            //    if (SiteMapNode.Text.StartsWith("?"))
            //    {
            //        Query = SiteMapNode.Text;
            //        SiteMapNode = SiteMapNode.Parent;
            //    }
            //    while(SiteMapNode.Level > 2)
            //    {
            //        UrlPaths.Add(SiteMapNode.Text);
            //        SiteMapNode = SiteMapNode.Parent;
            //    }
            //    UrlPaths.Reverse();
            //    StringBuilder UrlPathBuilder = new StringBuilder();
            //    foreach (string Path in UrlPaths)
            //    {
            //        UrlPathBuilder.Append("/"); UrlPathBuilder.Append(Path);
            //    }
            //    string Host = SiteMapNode.Text;
            //    string Url = UrlPathBuilder.ToString() + Query;
            //    if (Url == "//") Url = "/";
            //    IronUI.UpdateResultsTab(Host, Url);
            //    return;
            //}
            Request SelectedUrl = IronUI.GetSelectedUrlFromSiteMap();
            if (SelectedUrl != null)
            {
                IronUI.UpdateResultsTab(SelectedUrl);
                return;
            }

            if (IronUI.IsFindingsNodeSelected())
            {
                //PluginResult.CurrentPluginResult = IronDB.GetPluginResultFromDB(Int32.Parse(e.Node.Name));
                Finding.CurrentPluginResult = IronDB.GetPluginResultFromDB(IronUI.GetFindingIdFromIronTree());
                IronUI.UpdateResultsTab(Finding.CurrentPluginResult);
            }
            //if (e.Node.Level == 4 && (e.Node.Parent.Parent.Parent.Index == 1 || e.Node.Parent.Parent.Parent.Index == 2))
            //{
            //    PluginResult.CurrentPluginResult = IronDB.GetPluginResultFromDB(Int32.Parse(e.Node.Name));
            //    IronUI.UpdateResultsTab(PluginResult.CurrentPluginResult);
            //}
            //else if (e.Node.Level == 5 && e.Node.Parent.Parent.Parent.Parent.Index == 0)
            //{
            //    PluginResult.CurrentPluginResult = IronDB.GetPluginResultFromDB(Int32.Parse(e.Node.Name));
            //    IronUI.UpdateResultsTab(PluginResult.CurrentPluginResult);
            //}
            //else if (e.Node.Level == 2 && e.Node.Parent.Index == 3)
            else if (IronUI.IsExceptionsNodeSelected())
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
            if (ShellLogGrid.SelectedCells.Count < 1 || ShellLogGrid.SelectedCells[0].Value == null || ShellLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Shell, ShellLogGrid.SelectedCells[0].Value.ToString(), ShellLogGrid.SelectedRows[0].Index, false);
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
            if (CustomSendPythonRB.Checked)
            {
                this.ShowScriptedSendTemplateLL.Text = "Show sample Python script";
                this.CustomSendTopRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue255;\red25\green25\blue112;} \cf1 def \cf0 \cf2 \b1 ScriptedSend \b0 \cf0 (req):";
                this.CustomSendBottomRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue128;} \cf1     return \cf0 res";
                this.CustomSendActivateCB.Checked = false;
                this.MTScriptedSendBtn.Enabled = false;
                Directory.SetCurrentDirectory(Config.RootDir);
                CustomSendTE.SetHighlighting("Python");
            }
        }

        private void CustomSendRubyRB_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomSendRubyRB.Checked)
            {
                this.ShowScriptedSendTemplateLL.Text = "Show sample Ruby script";
                this.CustomSendTopRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue255;\red25\green25\blue112;} \cf1 def \cf0 \cf2 \b1 scripted_send \b0 \cf0 (req)";
                this.CustomSendBottomRtb.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue128;\red0\green0\blue255;} \cf1     return \cf0 res \par \cf2 end \cf0";
                this.CustomSendActivateCB.Checked = false;
                this.MTScriptedSendBtn.Enabled = false;
                Directory.SetCurrentDirectory(Config.RootDir);
                CustomSendTE.SetHighlighting("Ruby");
            }
        }

        private void MTScriptedSendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                IronUI.ResetMTExceptionFields();
                ManualTesting.CurrentRequest = TestRequestView.GetRequest();
                if (ManualTesting.CurrentRequest == null) throw new Exception("Cannot sent invalid request");
                //IronUI.HandleAnyChangesInMTRequest();
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
                //ManualTesting.CurrentRequest.SSL = MTIsSSLCB.Checked;
                ManualTesting.ScriptedSend();
                IronUI.StartMTSend(ManualTesting.CurrentRequestID);
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to Send 'Manual Testing' Request", Exp.Message, Exp.StackTrace);
                IronUI.ShowMTException("Error sending Request");
                IronUI.EndMTSend(true);
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

        private string GetSource()
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
                case("OtherLogGrid"):
                        return IronLog.SelectedOtherSource;
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
                    string TrimmedTriggerVal = ResultsTriggersGrid.SelectedCells[0].Value.ToString();
                    if (TrimmedTriggerVal.Equals("Normal"))
                    {
                        return "0";
                    }
                    else
                    {
                        return TrimmedTriggerVal.Replace("Trigger", "").Trim();
                    }
                case ("TestGroupLogGrid"):
                    return TestGroupLogGrid.SelectedCells[1].Value.ToString();
                case ("OtherLogGrid"):
                    return OtherLogGrid.SelectedCells[0].Value.ToString();
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
            try
            {
                this.ASStartScanBtn.Enabled = false;
                Scanner.ResetChangedStatus();
                IronUI.ResetConfigureScanFields();
                Thread T = new Thread(Scanner.LoadScannerFromDDAndFillAutomatedScanningTab);
                T.Start(Int32.Parse(ASQueueGrid.SelectedCells[0].Value.ToString()));
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to load Request from Scan Queue DB", Exp.Message, Exp.StackTrace);
                IronUI.ShowConfigureScanException("Unable to load Request");
                return;
            }

            //try
            //{
            //    Scanner.CurrentScanID = Int32.Parse(ASQueueGrid.SelectedCells[0].Value.ToString());
            //    Scanner.CurrentScanner = IronDB.GetScannerFromDB(Scanner.CurrentScanID);
            //}
            //catch(Exception Exp)
            //{
            //    IronException.Report("Unable to load Request from Scan Queue DB", Exp.Message, Exp.StackTrace);
            //    IronUI.ShowConfigureScanException("Unable to load Request");
            //    return;
            //}
            //Scanner.CurrentScanner.OriginalRequest.Source = RequestSource.Scan;
            //try
            //{
            //    IronUI.FillConfigureScanFullFields(Scanner.CurrentScanner.OriginalRequest);
            //    this.ASRequestTabs.SelectTab(0);
            //    IronUI.UpdateScanTabsWithRequestData();
            //    ScanIDLbl.Text = "Scan ID: " + Scanner.CurrentScanID.ToString();
            //    ScanStatusLbl.Text = "Scan Status: " + ScanStatus;
            //    Scanner.ResetChangedStatus();
            //}
            //catch(Exception Exp)
            //{
            //    IronException.Report("Unable to display Request in 'Automated Scanning' section", Exp.Message, Exp.StackTrace);
            //    IronUI.ShowConfigureScanException("Unable to display request");
            //    return;
            //}

            //if (ASScanPluginsGrid.Rows.Count > 0)
            //{
            //    ASScanPluginsGrid.Rows[0].Cells[0].Value = false;
            //    foreach (DataGridViewRow Row in this.ASScanPluginsGrid.Rows)
            //    {
            //        if (Row.Index > 0)
            //        {
            //            Row.Cells[0].Value = Scanner.CurrentScanner.ShowChecks().Contains(Row.Cells[1].Value.ToString());
            //        }
            //    }
            //    if (ASScanPluginsGrid.Rows.Count > 1)
            //    {
            //        bool AllSelected = true;
            //        for (int i = 1; i < ASScanPluginsGrid.Rows.Count; i++)
            //        {
            //            if (!(bool)ASScanPluginsGrid.Rows[i].Cells[0].Value)
            //            {
            //                AllSelected = false;
            //                break;
            //            }
            //        }
            //        if (AllSelected) ASScanPluginsGrid.Rows[0].Cells[0].Value = true;
            //    }
            //}

            //this.ASSessionPluginsCombo.Items.Add("");
            //int SelectedSessionPluginID = -1;
            //bool SelectedSessionPluginFound = false;
            //foreach (string Name in SessionPlugin.List())
            //{
            //    int ItemID = this.ASSessionPluginsCombo.Items.Add(Name);
            //    if (!SelectedSessionPluginFound)
            //    {
            //        if (Scanner.CurrentScanner.SessionHandler.Name.Equals(Name))
            //        {
            //            SelectedSessionPluginID = ItemID;
            //            SelectedSessionPluginFound = true;
            //        }
            //    }
            //}
            
            //if(SelectedSessionPluginID >= 0 ) this.ASSessionPluginsCombo.SelectedIndex = SelectedSessionPluginID;
            //try
            //{
            //    FillInjectionsPointsinUI(Scanner.CurrentScanner);
            //}
            //catch (Exception Exp)
            //{
            //    IronException.Report("Error restoring 'Automated Scan' configuration information from DB", Exp.Message, Exp.StackTrace);
            //    IronUI.ShowConfigureScanException("Error retriving scan information");
            //}

            //if (ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Completed") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Running"))
            //{
            //    this.ASStartScanBtn.Text = "Scan Again";
            //}
            //else if (ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Not Started") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Incomplete") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Aborted") || ASQueueGrid.SelectedCells[1].Value.ToString().Equals("Stopped"))
            //{
            //    this.ASStartScanBtn.Text = "Start Scan";
            //}
            //this.ASStartScanBtn.Enabled = true;
        }

        //void FillInjectionsPointsinUI(Scanner Scanner)
        //{
        //    bool AllUlr = ASRequestScanURLGrid.Rows.Count > 0;
        //    foreach (DataGridViewRow Row in this.ASRequestScanURLGrid.Rows)
        //    {
        //        bool Result = Scanner.URLInjections.Contains(Row.Index);
        //        if (AllUlr)
        //        {
        //            AllUlr = Result;
        //        }
        //        Row.Cells[0].Value = Result;
        //    }

        //    int SubParameterIndex = 0;
        //    string LastParameterName = "";

        //    bool AllQuery = ASRequestScanQueryGrid.Rows.Count > 0;
        //    foreach (DataGridViewRow Row in this.ASRequestScanQueryGrid.Rows)
        //    {
        //        string Name = Row.Cells[1].Value.ToString();
        //        if (Name.Equals(LastParameterName))
        //        {
        //            SubParameterIndex++;
        //        }
        //        else
        //        {
        //            SubParameterIndex = 0;
        //        }
        //        bool Result = Scanner.QueryInjections.Has(Name) && Scanner.QueryInjections.GetAll(Name).Contains(SubParameterIndex);
        //        if (AllQuery)
        //        {
        //            AllQuery = Result;
        //        }
        //        Row.Cells[0].Value = Result;
        //        LastParameterName = Name;
        //    }

        //    SubParameterIndex = 0;
        //    LastParameterName = "";

        //    bool AllBody = ConfigureScanRequestBodyGrid.Rows.Count > 0;
        //    if (Scanner.BodyFormat.Name.Length == 0)
        //    {
        //        foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyGrid.Rows)
        //        {
        //            string Name = Row.Cells[1].Value.ToString();
        //            if (Name.Equals(LastParameterName))
        //            {
        //                SubParameterIndex++;
        //            }
        //            else
        //            {
        //                SubParameterIndex = 0;
        //            }
        //            bool Result = Scanner.BodyInjections.Has(Name) && Scanner.BodyInjections.GetAll(Name).Contains(SubParameterIndex);
        //            if (AllBody)
        //            {
        //                AllBody = Result;
        //            }
        //            Row.Cells[0].Value = Result;
        //            LastParameterName = Name;
        //        }
        //    }
        //    else
        //    {
        //        foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyGrid.Rows)
        //        {
        //            bool Result = Scanner.BodyXmlInjections.Contains(Row.Index);
        //            if (AllBody)
        //            {
        //                AllBody = Result;
        //            }
        //            Row.Cells[0].Value = Result;
        //        }
        //    }

        //    SubParameterIndex = 0;
        //    LastParameterName = "";

        //    bool AllCookie = ASRequestScanCookieGrid.Rows.Count > 0;
        //    foreach (DataGridViewRow Row in this.ASRequestScanCookieGrid.Rows)
        //    {
        //        string Name = Row.Cells[1].Value.ToString();
        //        if (Name.Equals(LastParameterName))
        //        {
        //            SubParameterIndex++;
        //        }
        //        else
        //        {
        //            SubParameterIndex = 0;
        //        }
        //        bool Result = Scanner.CookieInjections.Has(Name) && Scanner.CookieInjections.GetAll(Name).Contains(SubParameterIndex);
        //        if (AllCookie)
        //        {
        //            AllCookie = Result;
        //        }
        //        Row.Cells[0].Value = Result;
        //        LastParameterName = Name;
        //    }

        //    SubParameterIndex = 0;
        //    LastParameterName = "";

        //    bool AllHeaders = ASRequestScanHeadersGrid.Rows.Count > 0;
        //    foreach (DataGridViewRow Row in this.ASRequestScanHeadersGrid.Rows)
        //    {
        //        string Name = Row.Cells[1].Value.ToString();
        //        if (Name.Equals(LastParameterName))
        //        {
        //            SubParameterIndex++;
        //        }
        //        else
        //        {
        //            SubParameterIndex = 0;
        //        }
        //        bool Result = Scanner.HeadersInjections.Has(Name) && Scanner.HeadersInjections.GetAll(Name).Contains(SubParameterIndex);
        //        if (AllHeaders)
        //        {
        //            AllHeaders = Result;
        //        }
        //        Row.Cells[0].Value = Result;
        //        LastParameterName = Name;
        //    }

        //    ASRequestScanAllCB.Checked = AllUlr && AllQuery && AllBody && AllCookie && AllHeaders;
        //    ASRequestScanURLCB.Checked = AllUlr;
        //    ASRequestScanQueryCB.Checked = AllQuery ;
        //    ASRequestScanBodyCB.Checked = AllBody;
        //    ASRequestScanCookieCB.Checked = AllCookie;
        //    ASRequestScanHeadersCB.Checked = AllHeaders;
        //}

        private void ScanLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanLogGrid.SelectedCells.Count < 1 || ScanLogGrid.SelectedCells[0].Value == null || ScanLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Scan, ScanLogGrid.SelectedCells[0].Value.ToString(), ScanLogGrid.SelectedRows[0].Index, false);
            return;
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
                    else if (this.ASStartScanBtn.Text.Equals("Stop Scan"))
                    {
                        try
                        {
                            string Status = ASQueueGrid.SelectedCells[1].Value.ToString();
                            if (Status.Equals("Running"))
                            {
                                int ScanID = Int32.Parse(ASQueueGrid.SelectedCells[0].Value.ToString());
                                Scanner.ScanThreads[ScanID].Abort();
                            }
                            this.ASStartScanBtn.Text = "Start Scan";
                        }
                        catch(Exception Exp)
                        {
                            IronUI.ShowConfigureScanException("Unable to stop this scan");
                            IronException.Report("Unable to Stop a Active Scan Job", Exp.Message, Exp.StackTrace);
                        }
                        return;
                    }
                }
                catch { }
                //try
                //{
                //    IronUI.HandleAnyChangesInConfigureScanRequest();
                //}
                //catch (Exception Exp)
                //{
                //    IronUI.ShowConfigureScanException(Exp.Message);
                //    return;
                //}
                if (Scanner.CurrentScanner == null)
                {
                    IronUI.ShowConfigureScanException("No Scan Job selected");
                    return;
                }
                if (Scanner.CurrentScanner.OriginalRequest == null)
                {
                    IronUI.ShowConfigureScanException("No Scan Job selected");
                    return;
                }
                
                //string SelectedFormatPlugin = "None";
                //if (Scanner.CurrentScanner.BodyFormat.Name.Length > 0) SelectedFormatPlugin = Scanner.CurrentScanner.BodyFormat.Name;


                //Scanner.CurrentScanner.URLInjections = new List<int>();
                //if (this.ASRequestScanURLCB.Checked)
                //{
                //    Scanner.CurrentScanner.InjectURL();
                //}
                //else
                //{
                //    for (int i = 0; i < this.ASRequestScanURLGrid.Rows.Count; i++)
                //    {
                //        if ((bool)this.ASRequestScanURLGrid.Rows[i].Cells[0].Value)
                //        {
                //            Scanner.CurrentScanner.InjectUrl(i);
                //        }
                //    }
                //}
                //Scanner.CurrentScanner.QueryInjections = new InjectionParameters();
                //if (this.ASRequestScanQueryCB.Checked)
                //{
                //    Scanner.CurrentScanner.InjectQuery();
                //}
                //else
                //{
                //    int SubParameterPosition = 0;
                //    string ParameterName = "";
                //    foreach (DataGridViewRow Row in this.ASRequestScanQueryGrid.Rows)
                //    {
                //        string CurrentParameterName = Row.Cells[1].Value.ToString();
                //        if (ParameterName.Equals(CurrentParameterName))
                //        {
                //            SubParameterPosition++;
                //        }
                //        else
                //        {
                //            ParameterName = CurrentParameterName;
                //            SubParameterPosition = 0;
                //        }
                //        if ((bool)Row.Cells[0].Value)
                //        {
                //            Scanner.CurrentScanner.InjectQuery(ParameterName, SubParameterPosition);
                //        }
                //    }
                //}

                //if (ASBodyTypeNormalRB.Checked)
                //{
                //    if (this.ASRequestScanBodyCB.Checked)
                //    {
                //        Scanner.CurrentScanner.InjectBody();
                //    }
                //    else
                //    {
                //        int SubParameterPosition = 0;
                //        string ParameterName = "";

                //        foreach (DataGridViewRow Row in this.ASRequestScanBodyTypeNormalGrid.Rows)
                //        {
                //            string CurrentParameterName = Row.Cells[1].Value.ToString();
                //            if (ParameterName.Equals(CurrentParameterName))
                //            {
                //                SubParameterPosition++;
                //            }
                //            else
                //            {
                //                ParameterName = CurrentParameterName;
                //                SubParameterPosition = 0;
                //            }
                //            if ((bool)Row.Cells[0].Value)
                //            {
                //                Scanner.CurrentScanner.InjectBody(ParameterName, SubParameterPosition);
                //            }
                //        }
                //    }
                //}
                //else if (ASBodyTypeFormatPluginRB.Checked)
                //{
                //    foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyTypeFormatPluginGrid.Rows)
                //    {                            
                //        if ((bool)Row.Cells[0].Value)
                //        {
                //            Scanner.CurrentScanner.InjectBody(Row.Index);
                //        }
                //    }
                //}
                //else if (ASBodyTypeCustomRB.Checked)
                //{
                //    if (ASRequestScanBodyCB.Checked)
                //    {
                //        Scanner.CurrentScanner.InjectBody(Scanner.CurrentScanner.CustomInjectionPointStartMarker, Scanner.CurrentScanner.CustomInjectionPointEndMarker);
                //    }
                //    else
                //    {
                //        Scanner.CurrentScanner.CustomInjectionPointStartMarker = "";
                //        Scanner.CurrentScanner.CustomInjectionPointEndMarker = "";
                //    }
                //}
                //else
                //{
                //    Scanner.CurrentScanner.BodyInjections = new InjectionParameters();
                //    Scanner.CurrentScanner.CustomInjectionPointStartMarker = "";
                //    Scanner.CurrentScanner.CustomInjectionPointEndMarker = "";
                //    Scanner.CurrentScanner.BodyFormat = new FormatPlugin();
                //}
                //Scanner.CurrentScanner.CookieInjections = new InjectionParameters();
                //if (this.ASRequestScanCookieCB.Checked)
                //{
                //    Scanner.CurrentScanner.InjectCookie();
                //}
                //else
                //{
                //    int SubParameterPosition = 0;
                //    string ParameterName = "";
                //    foreach (DataGridViewRow Row in this.ASRequestScanCookieGrid.Rows)
                //    {
                //        string CurrentParameterName = Row.Cells[1].Value.ToString();
                //        if (ParameterName.Equals(CurrentParameterName))
                //        {
                //            SubParameterPosition++;
                //        }
                //        else
                //        {
                //            ParameterName = CurrentParameterName;
                //            SubParameterPosition = 0;
                //        }
                //        if ((bool)Row.Cells[0].Value)
                //        {
                //            Scanner.CurrentScanner.InjectCookie(ParameterName, SubParameterPosition);
                //        }
                //    }
                //}
                //Scanner.CurrentScanner.HeadersInjections = new InjectionParameters();
                //if (this.ASRequestScanHeadersCB.Checked)
                //{
                //    Scanner.CurrentScanner.InjectHeaders();
                //}
                //else
                //{
                //    int SubParameterPosition = 0;
                //    string ParameterName = "";
                //    foreach (DataGridViewRow Row in this.ASRequestScanHeadersGrid.Rows)
                //    {
                //        string CurrentParameterName = Row.Cells[1].Value.ToString();
                //        if (ParameterName.Equals(CurrentParameterName))
                //        {
                //            SubParameterPosition++;
                //        }
                //        else
                //        {
                //            ParameterName = CurrentParameterName;
                //            SubParameterPosition = 0;
                //        }
                //        if ((bool)Row.Cells[0].Value)
                //        {
                //            Scanner.CurrentScanner.InjectHeaders(ParameterName, SubParameterPosition);
                //        }
                //    }
                //}

                //if ((Scanner.CurrentScanner.URLInjections.Count + Scanner.CurrentScanner.QueryInjections.Count + Scanner.CurrentScanner.BodyInjections.Count + Scanner.CurrentScanner.BodyXmlInjections.Count + Scanner.CurrentScanner.GetCustomInjectionPointsCount() + Scanner.CurrentScanner.CookieInjections.Count + Scanner.CurrentScanner.HeadersInjections.Count) == 0)
                //{
                //    IronUI.ShowConfigureScanException("No Injection Points Selected or Available!");
                //    return;
                //}

                //StringBuilder ScanPluginsBuilder = new StringBuilder();
                //foreach (DataGridViewRow Row in ASScanPluginsGrid.Rows)
                //{
                //    if (Row.Index > 0)
                //    {
                //        if ((bool)Row.Cells[0].Value)
                //        {
                //            string PluginName = Row.Cells[1].Value.ToString();
                //            Scanner.CurrentScanner.AddCheck(PluginName);
                //            ScanPluginsBuilder.Append(PluginName);
                //            ScanPluginsBuilder.Append(",");
                //        }
                //    }
                //}
                //string SelectedScanPlugins = ScanPluginsBuilder.ToString().TrimEnd(new char[] { ',' });
                //if (Scanner.CurrentScanner.ShowChecks().Count == 0)
                //{
                //    IronUI.ShowConfigureScanException("No Plugin Selected!");
                //    return;
                //}
                ////string SelectedSessionPlugin = "None";
                ////if (ASSessionPluginsCombo.SelectedItem != null)
                ////{
                ////    string SessionPluginName = this.ASSessionPluginsCombo.SelectedItem.ToString();
                ////    if (SessionPluginName.Length > 0)
                ////    {
                ////        Scanner.CurrentScanner.SessionHandler = SessionPlugin.Get(SessionPluginName);
                ////        SelectedSessionPlugin = SessionPluginName;
                ////    }
                ////}

                //if (Scanner.CurrentScanner.ShowChecks().Count == 0)
                //{
                //    IronUI.ShowConfigureScanException("No Plugin Selected!");
                //    return;
                //}

                if (ASStartScanBtn.Text.Equals("Start Scan"))
                    Scanner.CurrentScanner.StartScan();
                else
                    Scanner.CurrentScanner.LaunchScan();

                Scanner.CurrentScanner = null;
                Scanner.CurrentScanID = 0;
                this.ASStartScanBtn.Text = "Scan";
                Scanner.ResetChangedStatus();
                IronUI.ResetConfigureScanFields();
                ScanDisplayPanel.Visible = false;
                ScanTopPanel.Visible = true;
                ScanJobsBaseSplit.SplitterDistance = 62;
            }
            catch(Exception Exp)
            {
                IronException.Report("Error starting a configured scan", Exp.Message, Exp.StackTrace);
            }
        }

        //private void ASScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (ASScanPluginsGrid.SelectedCells.Count < 1 || ASScanPluginsGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    if (this.ASScanPluginsGrid.SelectedRows[0].Index == 0)
        //    {
        //        bool AllValue = !(bool)this.ASScanPluginsGrid.SelectedCells[0].Value;
        //        this.ASScanPluginsGrid.SelectedCells[0].Value = AllValue;
        //        foreach (DataGridViewRow Row in this.ASScanPluginsGrid.Rows)
        //        {
        //            if (Row.Index > 0)
        //            {
        //                Row.Cells[0].Value = AllValue;
        //            }
        //        }
        //        return;
        //    }
        //    if ((bool)this.ASScanPluginsGrid.SelectedCells[0].Value)
        //    {
        //        this.ASScanPluginsGrid.SelectedCells[0].Value = false;
        //        this.ASScanPluginsGrid.Rows[0].SetValues(new object[]{false, "All"});
        //    }
        //    else
        //    {
        //        this.ASScanPluginsGrid.SelectedCells[0].Value = true;
        //    }
        //}

        //private void ASRequestScanAllCB_Click(object sender, EventArgs e)
        //{
        //    CheckAllASRequestInjections();
        //}

        //private void CheckAllASRequestInjections()
        //{
        //    this.ASRequestScanURLCB.Checked = this.ASRequestScanAllCB.Checked;
        //    this.CheckAllASRequestScanURLGridRows();
        //    this.ASRequestScanQueryCB.Checked = this.ASRequestScanAllCB.Checked;
        //    this.CheckAllASRequestScanQueryGridRows();
        //    this.ASRequestScanBodyCB.Checked = this.ASRequestScanAllCB.Checked;
        //    this.CheckAllASRequestScanBodyGridRows();
        //    this.ASRequestScanCookieCB.Checked = this.ASRequestScanAllCB.Checked;
        //    this.CheckAllASRequestScanCookieGridRows();
        //    this.ASRequestScanHeadersCB.Checked = this.ASRequestScanAllCB.Checked;
        //    this.CheckAllASRequestScanHeadersGridRows();
        //}

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
        //private void ASRequestScanBodyCB_Click(object sender, EventArgs e)
        //{
        //    this.CheckAllASRequestScanBodyGridRows();
        //    if (!this.ASRequestScanBodyCB.Checked)
        //    {
        //        if (this.ASRequestScanAllCB.Checked)
        //        {
        //            this.ASRequestScanAllCB.Checked = false;
        //        }
        //    }
        //}
        //private void CheckAllASRequestScanBodyGridRows()
        //{
        //    if (ASBodyTypeNormalRB.Checked)
        //    {
        //        foreach (DataGridViewRow Row in this.ASRequestScanBodyTypeNormalGrid.Rows)
        //        {
        //            Row.Cells[0].Value = this.ASRequestScanBodyCB.Checked;
        //        }
        //    }
        //    if (ASBodyTypeFormatPluginRB.Checked)
        //    {
        //        foreach (DataGridViewRow Row in this.ConfigureScanRequestBodyTypeFormatPluginGrid.Rows)
        //        {
        //            Row.Cells[0].Value = this.ASRequestScanBodyCB.Checked;
        //        }
        //    }
        //    if (ASBodyTypeCustomRB.Checked)
        //    {
        //        ASRequestScanBodyCB.Checked = false;
        //        IronUI.ShowConfigureScanException("For custom injection markers press 'Apply' next to the injection marker textbox.");
        //    }
        //}
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

        //private void ASRequestScanURLGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (ASRequestScanURLGrid.SelectedCells.Count < 1 || ASRequestScanURLGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    if ((bool)this.ASRequestScanURLGrid.SelectedCells[0].Value)
        //    {
        //        this.ASRequestScanURLGrid.SelectedCells[0].Value = false;
        //        this.ASRequestScanAllCB.Checked = false;
        //        this.ASRequestScanURLCB.Checked = false;
        //    }
        //    else
        //    {
        //        this.ASRequestScanURLGrid.SelectedCells[0].Value = true;
        //    }
        //}

        //private void ASRequestScanQueryGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (ASRequestScanQueryGrid.SelectedCells.Count < 1 || ASRequestScanQueryGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    if ((bool)this.ASRequestScanQueryGrid.SelectedCells[0].Value)
        //    {
        //        this.ASRequestScanQueryGrid.SelectedCells[0].Value = false;
        //        this.ASRequestScanAllCB.Checked = false;
        //        this.ASRequestScanQueryCB.Checked = false;
        //    }
        //    else
        //    {
        //        this.ASRequestScanQueryGrid.SelectedCells[0].Value = true;
        //    }
        //}

        //private void ASRequestScanBodyGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (ConfigureScanRequestBodyTypeFormatPluginGrid.SelectedCells.Count < 1 || ConfigureScanRequestBodyTypeFormatPluginGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    if ((bool)this.ConfigureScanRequestBodyTypeFormatPluginGrid.SelectedCells[0].Value)
        //    {
        //        this.ConfigureScanRequestBodyTypeFormatPluginGrid.SelectedCells[0].Value = false;
        //        this.ASRequestScanAllCB.Checked = false;
        //        this.ASRequestScanBodyCB.Checked = false;
        //    }
        //    else
        //    {
        //        this.ConfigureScanRequestBodyTypeFormatPluginGrid.SelectedCells[0].Value = true;
        //    }
        //}

        //private void ASRequestScanCookieGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (ASRequestScanCookieGrid.SelectedCells.Count < 1 || ASRequestScanCookieGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    if ((bool)this.ASRequestScanCookieGrid.SelectedCells[0].Value)
        //    {
        //        this.ASRequestScanCookieGrid.SelectedCells[0].Value = false;
        //        this.ASRequestScanAllCB.Checked = false;
        //        this.ASRequestScanCookieCB.Checked = false;
        //    }
        //    else
        //    {
        //        this.ASRequestScanCookieGrid.SelectedCells[0].Value = true;
        //    }
        //}

        //private void ASRequestScanHeadersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (ASRequestScanHeadersGrid.SelectedCells.Count < 1 || ASRequestScanHeadersGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    if ((bool)this.ASRequestScanHeadersGrid.SelectedCells[0].Value)
        //    {
        //        this.ASRequestScanHeadersGrid.SelectedCells[0].Value = false;
        //        this.ASRequestScanAllCB.Checked = false;
        //        this.ASRequestScanHeadersCB.Checked = false;
        //    }
        //    else
        //    {
        //        this.ASRequestScanHeadersGrid.SelectedCells[0].Value = true;
        //    }
        //}

        //private void ASSessionPluginsCombo_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    this.ASScanPluginsGrid.Focus();
        //}

        private void UpdateBodyGridForFormat(FormatPlugin Plugin, bool CheckStatus)
        {
            ConfigureScanRequestBodyTypeFormatPluginGrid.Rows.Clear();
            string XmlString = Plugin.ToXml(Scanner.CurrentScanner.OriginalRequest.BodyArray);
            ConfigureScanRequestFormatXMLTB.Text = XmlString;
            string[,] InjectionPoints = FormatPlugin.XmlToArray(XmlString);
            for (int i = 0; i < InjectionPoints.GetLength(0); i++)
            {
                ConfigureScanRequestBodyTypeFormatPluginGrid.Rows.Add(new object[] { CheckStatus, InjectionPoints[i, 0], InjectionPoints[i, 1] });
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
                CloseCurrentProjectAndPrepareForReload();
                IronDB.UpdateLogFilePaths(LogFilesDirectory);
                LoadSelectedTraceBtn.Enabled = false;
                IronUI.StartUpdatingFullUIFromDB();
            }
        }

        void CloseCurrentProjectAndPrepareForReload()
        {
            try { IronUI.RGW.Close(); }
            catch { }
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

        //private void ProxyRequestParametersTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (IronProxy.ManualTamperingFree) return;
        //    try
        //    {
        //        IronUI.ResetProxyException();
        //        IronUI.HandleAnyChangesInRequest();
        //    }
        //    catch(Exception Exp)
        //    {
        //        IronUI.ShowProxyException(Exp.Message);
        //    }
        //}

        //private void ProxyInterceptRequestTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (IronProxy.ManualTamperingFree) return;
        //    try
        //    {
        //        IronUI.ResetProxyException();
        //        IronUI.HandleAnyChangesInRequest();
        //    }
        //    catch(Exception Exp)
        //    {
        //        IronUI.ShowProxyException(Exp.Message);
        //    }
        //}

        //private void ProxyInterceptResponseTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (IronProxy.ManualTamperingFree) return;
        //    try
        //    {
        //        IronUI.ResetProxyException();
        //        IronUI.HandleAnyChangesInResponse();
        //    }
        //    catch(Exception Exp)
        //    {
        //        IronUI.ShowProxyException(Exp.Message);
        //    }
        //}

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

        //private void ProxyShowOriginalRequestCB_Click(object sender, EventArgs e)
        //{
        //    if (IronProxy.CurrentSession != null)
        //    {
        //        if (ProxyShowOriginalRequestCB.Checked)
        //        {
        //            if (IronProxy.CurrentSession.OriginalRequest != null)
        //            {
        //                IronUI.FillProxyFields(IronProxy.CurrentSession.OriginalRequest);
        //                IronUI.MakeProxyFieldsReadOnly();
        //            }
        //        }
        //        else
        //        {
        //            if (IronProxy.CurrentSession.Request != null)
        //            {
        //                IronUI.FillProxyFields(IronProxy.CurrentSession.Request);
        //                IronUI.MakeProxyFieldsReadOnly();
        //            }
        //        }
        //    }
        //}

        //private void ProxyShowOriginalResponseCB_Click(object sender, EventArgs e)
        //{
        //    if (IronProxy.CurrentSession != null)
        //    {
        //        if (ProxyShowOriginalResponseCB.Checked)
        //        {
        //            if (IronProxy.CurrentSession.OriginalResponse != null)
        //            {
        //                IronUI.FillProxyFields(IronProxy.CurrentSession.OriginalResponse);
        //            }
        //        }
        //        else
        //        {
        //            if (IronProxy.CurrentSession.Response != null)
        //            {
        //                IronUI.FillProxyFields(IronProxy.CurrentSession.Response);
        //            }
        //        }
        //    }
        //    ProxyInterceptTabs.SelectedIndex = 1;
        //}

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

        //private void MTRequestTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    try
        //    {
        //        IronUI.ResetMTExceptionFields();
        //        IronUI.HandleAnyChangesInMTRequest();
        //    }
        //    catch(Exception Exp)
        //    {
        //        IronUI.ShowMTException(Exp.Message);
        //    }
        //}

        //private void MTRequestParametersTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    try
        //    {
        //        IronUI.ResetMTExceptionFields();
        //        IronUI.HandleAnyChangesInMTRequest();
        //    }
        //    catch(Exception Exp)
        //    {
        //        IronUI.ShowMTException(Exp.Message);
        //    }
        //}

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
                if (e.Node.Level == 3)
                {
                    //Plugins
                    if (e.Node.Parent.Parent.Index == 0)
                    {
                        string PluginName = e.Node.Name;
                        string[] PluginDetails = new string[] { "", "", "", "", "" };
                        PluginDetails[0] = e.Node.Name;
                        Plugin P = new Plugin();
                        switch (e.Node.Parent.Index)
                        {
                            case 0:
                                P = ActivePlugin.Get(PluginDetails[0]);
                                PluginDetails[2] = string.Format("{0}\\plugins\\active\\{1}", Config.RootDir, P.FileName);
                                break;
                            case 1:
                                P = PassivePlugin.Get(PluginDetails[0]);
                                PluginDetails[2] = string.Format("{0}\\plugins\\passive\\{1}", Config.RootDir, P.FileName);
                                break;
                            case 2:
                                P = FormatPlugin.Get(PluginDetails[0]);
                                PluginDetails[2] = string.Format("{0}\\plugins\\format\\{1}", Config.RootDir, P.FileName);
                                break;
                            case 3:
                                P = SessionPlugin.Get(PluginDetails[0]);
                                PluginDetails[2] = string.Format("{0}\\plugins\\session\\{1}", Config.RootDir, P.FileName);
                                break;
                        }
                        PluginDetails[1] = P.Description;
                        if (P.FileName == "Internal")
                        {
                            PluginDetails[3] = "This plugin is implemented inside the core of IronWASP.\r\nTo view the source code of this plugin visit - https://github.com/lavakumar/ironwasp ";
                        }
                        else if (P.FileName.Length > 0)
                        {
                            if (P.FileName.EndsWith(".py"))
                                PluginDetails[4] = "Python";
                            else if (P.FileName.EndsWith(".rb"))
                                PluginDetails[4] = "Ruby";
                            else
                                PluginDetails[4] = "-";
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
                        IronUI.DisplayPluginDetails(PluginDetails);
                    }
                    //Modules
                    else if(e.Node.Parent.Parent.Index == 1)
                    {
                        string ModuleName = e.Node.Name;
                        string[] ModuleDetails = new string[] { "", "", "", "", "", "" };
                        ModuleDetails[0] = e.Node.Name;
                        Module M = Module.GetModuleReadFromXml(ModuleDetails[0]);
                        ModuleDetails[5] = M.DisplayName;
                        ModuleDetails[1] = M.Description;
                        ModuleDetails[2] = string.Format("{0}\\modules\\{1}\\{2}", Config.RootDir, M.Name, M.FileName);
                        if (M.FileName.Length > 0)
                        {
                            if (M.FileName.EndsWith(".py"))
                                ModuleDetails[4] = "Python";
                            else if (M.FileName.EndsWith(".rb"))
                                ModuleDetails[4] = "Ruby";
                            else
                                ModuleDetails[4] = "-";
                            try
                            {
                                StreamReader SR = File.OpenText(ModuleDetails[2]);
                                ModuleDetails[3] = SR.ReadToEnd();
                                SR.Close();
                            }
                            catch (Exception exp)
                            {
                                ModuleDetails[3] = "Error reading file: " + exp.Message;
                            }
                        }
                        else
                        {
                            ModuleDetails[3] = "FileName information is missing in the Module";
                        }
                        IronUI.DisplayModuleDetails(ModuleDetails);
                    }
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Error showing Plugin details", Exp.Message, Exp.StackTrace);
            }
        }

        private void SelectedPluginReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PluginAndModuleTree.SelectedNode == null) return;
            TreeNode SelectedNode = PluginAndModuleTree.SelectedNode;
            if (SelectedNode.Level == 3 && SelectedNode.Parent.Parent.Index == 0)
            {
                PluginType Type = PluginType.None;
                string FileName = "";

                switch (SelectedNode.Parent.Index)
                {
                    case 0:
                        Type = PluginType.Active;
                        FileName = ActivePlugin.Get(SelectedNode.Name).FileName;
                        break;
                    case 1:
                        Type = PluginType.Passive;
                        FileName = PassivePlugin.Get(SelectedNode.Name).FileName;
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
                    PluginEngine.ReloadPlugin(Type, SelectedNode.Name, FileName);
                }
            }
        }

        private void PluginTreeMenu_Opening(object sender, CancelEventArgs e)
        {
            SelectedPluginReloadToolStripMenuItem.Enabled = false;
            SelectedModuleReloadToolStripMenuItem.Enabled = false;
            SelectedPluginDeactivateToolStripMenuItem.Visible = false;

            if (PluginAndModuleTree.SelectedNode == null) return;
            
            TreeNode SelectedNode = PluginAndModuleTree.SelectedNode;
            if (SelectedNode.Level == 3)
            {
                //Plugins
                if (SelectedNode.Parent.Parent.Index == 0)
                {
                    SelectedPluginReloadToolStripMenuItem.Enabled = true;
                    //Passive Plugins
                    if (SelectedNode.Parent.Index == 1)
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
                }
                //Modules
                else if (SelectedNode.Parent.Parent.Index == 1)
                {
                    //Loaded Modules
                    if (SelectedNode.Parent.Index == 0)
                    {
                        SelectedModuleReloadToolStripMenuItem.Enabled = true;
                    }

                }
            }
        }

        private void AllPluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadAllPlugins);
            T.Start();
        }

        private void PassivePluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadAllPassivePlugins);
            T.Start();
        }

        private void ActivePluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadAllActivePlugins);
            T.Start();
        }

        private void FormatPluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadAllFormatPlugins);
            T.Start();
        }

        private void SessionPluginsRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadAllSessionPlugins);
            T.Start();
        }

        private void SessionPluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadNewSessionPlugins);
            T.Start();
        }

        private void FormatPluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadNewFormatPlugins);
            T.Start();
        }

        private void ActivePluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadNewActivePlugins);
            T.Start();
        }

        private void PassivePluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadNewPassivePlugins);
            T.Start();
        }

        private void AllPluginsANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(PluginEngine.LoadAllNewPlugins);
            T.Start();
        }

        private void SelectedPluginDeactivateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PluginAndModuleTree.SelectedNode == null) return;
            if (SelectedPluginDeactivateToolStripMenuItem.Text.Equals("Deactivate Selected Plugin"))
            {
                PassivePlugin.Deactivate(PluginAndModuleTree.SelectedNode.Name);
                PluginAndModuleTree.SelectedNode.ForeColor = Color.Gray;
                PluginAndModuleTree.SelectedNode.Text = PluginAndModuleTree.SelectedNode.Name + " (Deactivated)";
            }
            else
            {
                PassivePlugin.Activate(PluginAndModuleTree.SelectedNode.Name);
                PluginAndModuleTree.SelectedNode.ForeColor = Color.Green;
                PluginAndModuleTree.SelectedNode.Text = PluginAndModuleTree.SelectedNode.Name;
            }
        }

        private void ProxyDropBtn_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            IronProxy.DropInterceptedMessage();
            ProxyBaseSplit.Panel1.BackColor = Color.White;
            ProxySendBtn.Enabled = false;
            ProxyDropBtn.Enabled = false;
        }

        //private void MTRequestFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        //{
        //    this.MTRequestDeSerObjectToXmlMenuItem.Enabled = false;
        //    this.MTRequestSerXmlToObjectMenuItem.Enabled = false;
        //    if (this.MTRequestFormatPluginsGrid.SelectedRows.Count == 0)
        //    {
        //        return;
        //    }
        //    if (this.MTRequestFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
        //    this.MTRequestDeSerObjectToXmlMenuItem.Enabled = true;
        //    if (this.MTRequestFormatXMLTB.Text.Length > 0)
        //    {
        //        this.MTRequestSerXmlToObjectMenuItem.Enabled = true;
        //    }
        //}

        //private void MTRequestDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        //{
        //    MTRequestFormatXMLTB.Text = "";
        //    string PluginName = MTRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowMTException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (ManualTesting.CurrentRequest == null)
        //    {
        //        IronUI.ShowMTException("Invalid Request");
        //        return;
        //    }
        //    ManualTesting.StartDeSerializingRequestBody(ManualTesting.CurrentRequest, Plugin);
        //}

        //private void MTRequestSerXmlToObjectMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (MTRequestFormatPluginsGrid.SelectedCells == null) return;
        //    if (MTRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
        //    string PluginName = MTRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowMTException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (ManualTesting.CurrentRequest == null)
        //    {
        //        IronUI.ShowMTException("Invalid Request");
        //        return;
        //    }
        //    string XML = MTRequestFormatXMLTB.Text;
        //    ManualTesting.StartSerializingRequestBody(ManualTesting.CurrentRequest, Plugin, XML);
        //}

        //private void ProxyRequestFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        //{
        //    this.ProxyRequestDeSerObjectToXmlMenuItem.Enabled = false;
        //    this.ProxyRequestSerXmlToObjectMenuItem.Enabled = false;
        //    if (this.ProxyRequestFormatPluginsGrid.SelectedRows.Count == 0)
        //    {
        //        return;
        //    }

        //    if (this.ProxyRequestFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
        //    this.ProxyRequestDeSerObjectToXmlMenuItem.Enabled = true;
        //    if (this.ProxyRequestFormatXMLTB.Text.Length > 0)
        //    {
        //        this.ProxyRequestSerXmlToObjectMenuItem.Enabled = true;
        //    }
        //}

        //private void ProxyResponseFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        //{
        //    this.ProxyResponseDeSerObjectToXmlMenuItem.Enabled = false;
        //    this.ProxyResponseSerXmlToObjectMenuItem.Enabled = false;
        //    if (this.ProxyResponseFormatPluginsGrid.SelectedRows.Count == 0) return;
        //    if (this.ProxyResponseFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
        //    this.ProxyResponseDeSerObjectToXmlMenuItem.Enabled = true;                
        //    if (this.ProxyResponseFormatXMLTB.Text.Length > 0)
        //    {
        //        this.ProxyResponseSerXmlToObjectMenuItem.Enabled = true;
        //    }
        //}

        //private void ProxyRequestDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        //{
        //    ProxyRequestFormatXMLTB.Text = "";
        //    if (ProxyRequestFormatPluginsGrid.SelectedCells == null) return;
        //    if (ProxyRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
        //    string PluginName = ProxyRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowProxyException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (IronProxy.CurrentSession == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Request");
        //        return;
        //    }
        //    if (IronProxy.CurrentSession.Request == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Request");
        //        return;
        //    }
        //    IronProxy.StartDeSerializingRequestBody(IronProxy.CurrentSession.Request, Plugin);
        //}

        //private void ProxyRequestSerXmlToObjectMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (ProxyRequestFormatPluginsGrid.SelectedCells == null) return;
        //    if (ProxyRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
        //    string PluginName = ProxyRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowProxyException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (IronProxy.CurrentSession == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Request");
        //        return;
        //    }
        //    if (IronProxy.CurrentSession.Request == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Request");
        //        return;
        //    }
        //    string XML = ProxyRequestFormatXMLTB.Text;
        //    IronProxy.StartSerializingRequestBody(IronProxy.CurrentSession.Request, Plugin, XML);
        //}

        //private void ProxyResponseDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        //{
        //    ProxyResponseFormatXMLTB.Text = "";
        //    if (ProxyResponseFormatPluginsGrid.SelectedCells == null) return;
        //    if (ProxyResponseFormatPluginsGrid.SelectedCells.Count == 0) return;
        //    string PluginName = ProxyResponseFormatPluginsGrid.SelectedCells[0].Value.ToString();
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowProxyException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (IronProxy.CurrentSession == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Response");
        //        return;
        //    }
        //    if (IronProxy.CurrentSession.Response == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Response");
        //        return;
        //    }
        //    IronProxy.StartDeSerializingResponseBody(IronProxy.CurrentSession.Response, Plugin);
        //}

        //private void ProxyResponseSerXmlToObjectMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (ProxyResponseFormatPluginsGrid.SelectedCells == null) return;
        //    if (ProxyResponseFormatPluginsGrid.SelectedCells.Count == 0) return;
        //    string PluginName = ProxyResponseFormatPluginsGrid.SelectedCells[0].Value.ToString();
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowProxyException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (IronProxy.CurrentSession == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Response");
        //        return;
        //    }
        //    if (IronProxy.CurrentSession.Response == null)
        //    {
        //        IronUI.ShowProxyException("Invalid Response");
        //        return;
        //    }
        //    string XML = ProxyResponseFormatXMLTB.Text;
        //    IronProxy.StartSerializingResponseBody(IronProxy.CurrentSession.Response, Plugin, XML);
        //}

        //private void ConfigureScanRequestFormatPluginsMenu_Opening(object sender, CancelEventArgs e)
        //{
        //    this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Enabled = false;
        //    if (this.ConfigureScanRequestFormatPluginsGrid.SelectedRows.Count == 0)
        //    {
        //        return;
        //    }
        //    if (Scanner.CurrentScanner == null) return;
        //    if (Scanner.CurrentScanner.OriginalRequest == null) return;
        //    if (this.ConfigureScanRequestFormatPluginsGrid.SelectedCells[0].Value.ToString().Equals("None")) return;
        //    if (Scanner.CurrentScanner.OriginalRequest.HasBody)
        //    {
        //        this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Enabled = true;
        //    }
        //}

        //private void ConfigureScanRequestDeSerObjectToXmlMenuItem_Click(object sender, EventArgs e)
        //{
        //    ConfigureScanRequestFormatXMLTB.Text = "";
        //    if (ConfigureScanRequestFormatPluginsGrid.SelectedCells == null) return;
        //    if (ConfigureScanRequestFormatPluginsGrid.SelectedCells.Count == 0) return;
        //    string PluginName = ConfigureScanRequestFormatPluginsGrid.SelectedCells[0].Value.ToString();
        //    if (PluginName.Equals("None"))
        //    {
        //        IronUI.UpdateScanBodyTabWithDataInDefaultFormat();
        //        ConfigureScanRequestFormatXMLTB.Text = "";
        //        Scanner.CurrentScanner.BodyFormat = new FormatPlugin();
        //        return;
        //    }
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowConfigureScanException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (Scanner.CurrentScanner == null)
        //    {
        //        IronUI.ShowConfigureScanException("Invalid Request");
        //        return;
        //    }
        //    if (Scanner.CurrentScanner.OriginalRequest == null)
        //    {
        //        IronUI.ShowConfigureScanException("Invalid Request");
        //        return;
        //    }
        //    Scanner.CurrentScanner.BodyFormat = Plugin;
        //    Scanner.StartDeSerializingRequestBody(Scanner.CurrentScanner.OriginalRequest, Plugin, new List<bool>(), false);
        //}

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

        private void SiteMapLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SiteMapLogGrid.SelectedCells.Count < 1 || SiteMapLogGrid.SelectedCells[0].Value == null || SiteMapLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Proxy"))
            {
                IronLog.ShowLog(RequestSource.Proxy, SiteMapLogGrid.SelectedCells[0].Value.ToString(), SiteMapLogGrid.SelectedRows[0].Index, true); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Test"))
            {
                IronLog.ShowLog(RequestSource.Test, SiteMapLogGrid.SelectedCells[0].Value.ToString(), SiteMapLogGrid.SelectedRows[0].Index, true); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Shell"))
            {
                IronLog.ShowLog(RequestSource.Shell, SiteMapLogGrid.SelectedCells[0].Value.ToString(), SiteMapLogGrid.SelectedRows[0].Index, true); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Probe"))
            {
                IronLog.ShowLog(RequestSource.Probe, SiteMapLogGrid.SelectedCells[0].Value.ToString(), SiteMapLogGrid.SelectedRows[0].Index, true); 
            }
            else if (SiteMapLogGrid.SelectedCells[1].Value.ToString().Equals("Scan"))
            {
                IronLog.ShowLog(RequestSource.Scan, SiteMapLogGrid.SelectedCells[0].Value.ToString(), SiteMapLogGrid.SelectedRows[0].Index, true); 
            }
            else
            {
                return;
            }
        }

        private void IronTreeMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //Disable all menu items
            RunModulesOnUrlToolStripMenuItem.Enabled = false;
            RunModulesOnFindingToolStripMenuItem.Enabled = false;
            ScanBranchToolStripMenuItem.Enabled = false;
            
            if (IronTree.SelectedNode == null) return;
            TreeNode Node = IronTree.SelectedNode;
            //if ((Node.Level > 5) || (Node.Level == 5 && (Node.Parent.Parent.Parent.Parent.Index == 4)) || (Node.Level == 4 && (Node.Parent.Parent.Parent.Index == 4)) || (Node.Level == 3 && (Node.Parent.Parent.Index == 4)) || (Node.Level == 2 && (Node.Parent.Index == 4)))
            if(IronUI.IsSiteMapNodeSelected())
            {
                RunModulesOnUrlToolStripMenuItem.Enabled = true;
                if (IronUI.IsScanBranchFormOpen())
                {
                    IronUI.SBF.Activate();
                }
                else
                {
                    ScanBranchToolStripMenuItem.Enabled = true;
                }
            }
            else if(IronUI.IsFindingsNodeSelected())
            {
                RunModulesOnFindingToolStripMenuItem.Enabled = true;
            }

        }

        private void ScanBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (IronTree.SelectedNode == null) return;
            //TreeNode Node = IronTree.SelectedNode;
            //if ((Node.Level > 4) || (Node.Level == 4 && (Node.Parent.Parent.Parent.Index == 4)) || (Node.Level == 3 && (Node.Parent.Parent.Index == 4)) || (Node.Level == 2 && (Node.Parent.Index == 4)))
            //{
            //    List<string> UrlPaths = new List<string>();
            //    string Query = "";
            //    TreeNode SiteMapNode = Node;
            //    if (SiteMapNode.Text.StartsWith("?"))
            //    {
            //        Query = SiteMapNode.Text;
            //        SiteMapNode = SiteMapNode.Parent;
            //    }
            //    while (SiteMapNode.Level > 2)
            //    {
            //        UrlPaths.Add(SiteMapNode.Text);
            //        SiteMapNode = SiteMapNode.Parent;
            //    }
            //    UrlPaths.Reverse();
            //    StringBuilder UrlPathBuilder = new StringBuilder();
            //    foreach (string Path in UrlPaths)
            //    {
            //        UrlPathBuilder.Append("/"); UrlPathBuilder.Append(Path);
            //    }
            //    string Host = SiteMapNode.Text;
            //    string Url = UrlPathBuilder.ToString();// +Query;
                //if (Url.Length == 0)
                //{
                //    Url = "/*";
                //}
                //else
                //{
                //    Url = Url + "*";
                //}
            Request SelectedUrl = IronUI.GetSelectedUrlFromSiteMap();
            if (SelectedUrl == null) return;
            SelectedUrl.Url = string.Format("{0}*", SelectedUrl.UrlPath);
            IronUI.ShowScanBranchForm(SelectedUrl);
            //}
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
            try
            {
                Clipboard.SetText("Could not copy");
            }
            catch { }
            IronLog.CopyRequest(GetSource(), GetID());
        }

        private void CopyResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText("Could not copy");
            }
            catch { }
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
            StopAllScansAction();
        }

        void StopAllScansAction()
        {
            Thread T = new Thread(Scanner.StopAll);
            T.Start();
        }

        private void StartAllStoppedAndAbortedScansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartAllStoppedAndAbortedScansAction();
        }

        void StartAllStoppedAndAbortedScansAction()
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
                IronUI.MainViewSelectedScanTraceId = ID;
                IronTrace Trace = IronDB.GetScanTrace(ID);
                string OverviewXml = Trace.OverviewXml;
                string Message = Trace.GetScanTracePrettyMessage();
                
                
                //string Message = IronDB.GetScanTraceMessage(ID);
                StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
                SB.Append(Tools.RtfSafe(Message));
                ScanTraceMsgRTB.Rtf = SB.ToString();

                try
                {
                    List<Dictionary<string, string>> OverviewEntries = IronTrace.GetOverviewEntriesFromXml(OverviewXml);
                    ScanTraceOverviewGrid.Rows.Clear();
                    foreach (Dictionary<string, string> Entry in OverviewEntries)
                    {
                        ScanTraceOverviewGrid.Rows.Add(new object[] { Entry["id"], Entry["log_id"], Entry["payload"], Entry["code"], Entry["length"], Entry["mime"], Entry["time"], Entry["signature"] });
                    }
                }
                catch 
                {
                    //Probaly an entry from the log of an older version
                }
                LoadSelectedTraceBtn.Enabled = true;
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

        private void NextLogBtn_Click(object sender, EventArgs e)
        {
            if (IronLog.CurrentID == 0) return;
            DataGridView CurrentGrid = GetCurrentGrid();
            int NewRowId = -1;
            int CurrentRowId = GetCurrentRowIndex(CurrentGrid);
            if (CurrentRowId == -1)
            {
                CurrentRowId = GetFirstRowIndex(CurrentGrid);
                NewRowId = CurrentRowId;
            }
            if (CurrentRowId == -1)
            {
                //show error to user
                IronUI.ShowLogStatus("Unable to load Request/Response from Log", true);
                return;
            }
            else if(NewRowId == -1)
            {
                NewRowId = CurrentRowId + 1;
            }
            if (NewRowId >= IronLog.MaxRowCount)
            {
                //ask user to load next set of log from db
                IronUI.ShowLogStatus("Reached end of visible log entries. Load next set using the '>' button below.", false);
            }
            else if (NewRowId >= CurrentGrid.Rows.Count)
            {
                //say end of log reached
                IronUI.ShowLogStatus("Reached end of Log", false);
            }
            else
            {
                if (CurrentGrid.Rows[NewRowId].Cells.Count == 0 || CurrentGrid.Rows[NewRowId].Cells[0].Value == null)
                {
                    //say unable to load next log
                    IronUI.ShowLogStatus("Unable to load next entry from Log", true);
                }
                else
                {
                    int NewId = Int32.Parse(CurrentGrid.Rows[NewRowId].Cells[0].Value.ToString());
                    IronLog.ShowLog(IronLog.CurrentSource, NewId);
                }
            }
            //IronLog.ShowNextLog();
        }

        private void PreviousLogBtn_Click(object sender, EventArgs e)
        {
            if (IronLog.CurrentID == 0) return;
            DataGridView CurrentGrid = GetCurrentGrid();
            int NewRowId = -1;
            int CurrentRowId = GetCurrentRowIndex(CurrentGrid);
            if (CurrentRowId == -1)
            {
                CurrentRowId = GetLastRowIndex(CurrentGrid);
                NewRowId = CurrentRowId;
            }
            if (CurrentRowId == -1)
            {
                //show error to user
                IronUI.ShowLogStatus("Unable to load Request/Response from Log", true);
                return;
            }
            else if (NewRowId == -1)
            {
                NewRowId = CurrentRowId - 1;
            }
            if (NewRowId == -1)
            {
                if (CurrentGrid.Rows.Count == IronLog.MaxRowCount)
                {
                    //ask user to load previous set of log from db
                    IronUI.ShowLogStatus("Reached start of visible log entries. Load previous set using the '<' button below.", false);
                }
                else
                {
                    //say start of log reached
                    IronUI.ShowLogStatus("Reached start of Log", false);
                }
            }
            else
            {
                if (CurrentGrid.Rows[NewRowId].Cells.Count == 0 || CurrentGrid.Rows[NewRowId].Cells[0].Value == null)
                {
                    //say unable to load next log
                    IronUI.ShowLogStatus("Unable to load previous entry from Log", true);
                }
                else
                {
                    int NewId = Int32.Parse(CurrentGrid.Rows[NewRowId].Cells[0].Value.ToString());
                    IronLog.ShowLog(IronLog.CurrentSource, NewId);
                }
            }
            //IronLog.ShowPreviousLog();
        }

        DataGridView GetCurrentGrid()
        {
            DataGridView CurrentGrid = null;
            if (IronLog.IsSiteMap)
            {
                CurrentGrid = SiteMapLogGrid;
            }
            switch (IronLog.CurrentSource)
            {
                case (RequestSource.Proxy):
                    CurrentGrid = ProxyLogGrid;
                    break;
                case (RequestSource.Probe):
                    CurrentGrid = ProbeLogGrid;
                    break;
                case (RequestSource.Shell):
                    CurrentGrid = ShellLogGrid;
                    break;
                case (RequestSource.Scan):
                    CurrentGrid = ScanLogGrid;
                    break;
                case (RequestSource.Test):
                    CurrentGrid = TestLogGrid;
                    break;
                default:
                    CurrentGrid = OtherLogGrid;
                    break;
            }
            return CurrentGrid;
        }

        int GetCurrentRowIndex(DataGridView CurrentGrid)
        {
            int CurrentRowId = -1;
            if (CurrentGrid == null) return CurrentRowId;
            int NewRowId = -1;
            if (IronLog.CurrentRowID < CurrentGrid.Rows.Count)
            {
                if (IronLog.CurrentID.ToString().Equals(CurrentGrid.Rows[IronLog.CurrentRowID].Cells[0].Value.ToString()))
                {
                    CurrentRowId = IronLog.CurrentRowID;
                }
            }
            if (NewRowId == -1)
            {
                foreach (DataGridViewRow Row in CurrentGrid.Rows)
                {
                    if (Row.Cells.Count == 0) continue;
                    if (Row.Cells[0].Value == null) continue;
                    if (Row.Cells[0].Value.ToString() == IronLog.CurrentID.ToString()) CurrentRowId = Row.Index;
                }
            }
            return CurrentRowId;
        }

        int GetFirstRowIndex(DataGridView CurrentGrid)
        {
            int FirstRowId = -1;
            if (CurrentGrid == null) return FirstRowId;
            if (CurrentGrid.Rows.Count > 0)
            {
                try
                {
                    Int32.Parse(CurrentGrid.Rows[0].Cells[0].Value.ToString());
                    FirstRowId = 0;
                }
                catch
                {}
            }
            return FirstRowId;
        }

        int GetLastRowIndex(DataGridView CurrentGrid)
        {
            int LastRowId = -1;
            if (CurrentGrid == null) return LastRowId;
            if (CurrentGrid.Rows.Count > 0)
            {
                try
                {
                    Int32.Parse(CurrentGrid.Rows[CurrentGrid.Rows.Count - 1].Cells[0].Value.ToString());
                    LastRowId = CurrentGrid.Rows.Count - 1;
                }
                catch 
                {
                    try
                    {
                        Int32.Parse(CurrentGrid.Rows[CurrentGrid.Rows.Count - 2].Cells[0].Value.ToString());
                        LastRowId = CurrentGrid.Rows.Count - 2;
                    }
                    catch { }
                }
            }
            return LastRowId;
        }

        

        private void TestGroupLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TestGroupLogGrid.SelectedCells.Count < 1 || TestGroupLogGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            
            try
            {
                if (e.ColumnIndex == 0)// TestGroupHistoryClickActionSelectLogRB.Checked)
                {
                    TestGroupLogGrid.SelectedRows[0].Cells[0].Value = !((bool)TestGroupLogGrid.SelectedRows[0].Cells[0].Value);
                }
                else
                {
                    IronUI.ResetMTDisplayFields();
                    int ID = Int32.Parse(TestGroupLogGrid.SelectedCells[1].Value.ToString());
                    Session IrSe = null;
                    foreach (string Group in ManualTesting.GroupSessions.Keys)
                    {
                        if (ManualTesting.GroupSessions[Group].ContainsKey(ID))
                        {
                            IrSe = ManualTesting.GroupSessions[Group][ID];
                            IrSe.Flags["Group"] = Group;
                        }
                    }
                    if (IrSe != null)
                    {
                        IronUI.FillMTFields(IrSe);
                    }
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

        //private void RedGroupToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IronLog.MarkForTesting(GetSource(), GetID(),"Red");
        //}

        //private void GreenGroupToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IronLog.MarkForTesting(GetSource(), GetID(), "Green");
        //}

        //private void BlueGroupToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IronLog.MarkForTesting(GetSource(), GetID(), "Blue");
        //}

        //private void GrayGroupToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IronLog.MarkForTesting(GetSource(), GetID(), "Gray");
        //}

        //private void BrownGroupToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IronLog.MarkForTesting(GetSource(), GetID(), "Brown");
        //}

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
                if (IronUI.IsStartScanWizardOpen())
                {
                    IronUI.SSW.CloseWindow();
                }
            }
            catch { }
            if (ConsoleStartScanBtn.Text.Equals("Start Scan"))
            {
                try
                {
                    Request Req = new Request(ConsoleScanUrlTB.Text);
                    IronUI.ShowStartScanWizard(Req);
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

        //private void JSTaintTraceControlBtn_Click(object sender, EventArgs e)
        //{
        //    if (JSTaintTraceControlBtn.Text.Equals("Start Taint Trace"))
        //    {
        //        JSTaintResultGrid.Rows.Clear();
        //        IronJint.PauseAtTaint = PauseAtTaintCB.Checked;
        //        JSTaintTraceControlBtn.Text = "Stop Trace";
        //        PauseAtTaintCB.Visible = PauseAtTaintCB.Checked;
                
        //        List<string> SourceObjects = new List<string>();
        //        List<string> SinkObjects = new List<string>();
        //        List<string> SourceReturningMethods = new List<string>();
        //        List<string> SinkReturningMethods = new List<string>();
        //        List<string> ArgumentReturningMethods = new List<string>();
        //        List<string> ArgumentAssignedASourceMethods = new List<string>();
        //        List<string> ArgumentAssignedToSinkMethods = new List<string>();

        //        foreach (DataGridViewRow Row in JSTaintConfigGrid.Rows)
        //        {
        //            if (Row == null) continue;
        //            if (Row.Cells == null) continue;
        //            if (Row.Cells.Count < 7) continue;
        //            if (Row.Cells["JSTaintDefaultSourceObjectsColumn"].Value != null)
        //            {
        //                string SourceObject = Row.Cells["JSTaintDefaultSourceObjectsColumn"].Value.ToString().Trim();
        //                if (SourceObject.Length > 0) SourceObjects.Add(SourceObject);
        //            }
        //            if (Row.Cells["JSTaintDefaultSinkObjectsColumn"].Value != null)
        //            {
        //                string SinkObject = Row.Cells["JSTaintDefaultSinkObjectsColumn"].Value.ToString().Trim();
        //                if (SinkObject.Length > 0) SinkObjects.Add(SinkObject);
        //            }
        //            if (Row.Cells["JSTaintDefaultArgumentAssignedASourceMethodsColumn"].Value != null)
        //            {
        //                string ArgumentAssignedASourceMethod = Row.Cells["JSTaintDefaultArgumentAssignedASourceMethodsColumn"].Value.ToString().Trim();
        //                if (ArgumentAssignedASourceMethod.Length > 0) ArgumentAssignedASourceMethods.Add(ArgumentAssignedASourceMethod);
        //            }
        //            if (Row.Cells["JSTaintDefaultArgumentAssignedToSinkMethodsColumn"].Value != null)
        //            {
        //                string ArgumentAssignedToSinkMethod = Row.Cells["JSTaintDefaultArgumentAssignedToSinkMethodsColumn"].Value.ToString().Trim();
        //                if (ArgumentAssignedToSinkMethod.Length > 0) ArgumentAssignedToSinkMethods.Add(ArgumentAssignedToSinkMethod);
        //            }
        //            if (Row.Cells["JSTaintDefaultSourceReturningMethodsColumn"].Value != null)
        //            {
        //                string SourceReturningMethod = Row.Cells["JSTaintDefaultSourceReturningMethodsColumn"].Value.ToString().Trim();
        //                if (SourceReturningMethod.Length > 0) SourceReturningMethods.Add(SourceReturningMethod);
        //            }
        //            if (Row.Cells["JSTaintDefaultSinkReturningMethodsColumn"].Value != null)
        //            {
        //                string SinkReturningMethod = Row.Cells["JSTaintDefaultSinkReturningMethodsColumn"].Value.ToString().Trim();
        //                if (SinkReturningMethod.Length > 0) SinkReturningMethods.Add(SinkReturningMethod);
        //            }
        //            if (Row.Cells["JSTaintDefaultArgumentReturningMethodsColumn"].Value != null)
        //            {
        //                string ArgumentReturningMethod = Row.Cells["JSTaintDefaultArgumentReturningMethodsColumn"].Value.ToString().Trim();
        //                if (ArgumentReturningMethod.Length > 0) ArgumentReturningMethods.Add(ArgumentReturningMethod);
        //            }
        //        }

        //        IronJint.StartTraceFromUI(JSTaintTraceInRTB.Text, SourceObjects, SinkObjects, SourceReturningMethods, SinkReturningMethods, ArgumentReturningMethods, ArgumentAssignedASourceMethods, ArgumentAssignedToSinkMethods);
        //    }
        //    else
        //    {
        //        IronJint.StopUITrace();
        //        IronUI.ResetTraceStatus();
        //        IronUI.ShowTraceStatus("Trace Stopped", false);
        //    }
        //}

        //private void JSTaintTabs_Selected(object sender, TabControlEventArgs e)
        //{
        //    if (e.TabPage == null) return;
        //    if (e.TabPage.Name.Equals("JSTaintResultTab"))
        //    {
        //        TaintTraceResultSinkLegendTB.Visible = true;
        //        TaintTraceResultSourceLegendTB.Visible = true;
        //        TaintTraceResultSourcePlusSinkLegendTB.Visible = true;
        //        TaintTraceResultSourceToSinkLegendTB.Visible = true;
        //        JSTaintShowLinesLbl.Visible = true;
        //        JSTaintShowCleanCB.Visible = true;
        //        JSTaintShowSourceCB.Visible = true;
        //        JSTaintShowSinkCB.Visible = true;
        //        JSTaintShowSourceToSinkCB.Visible = true;
        //    }
        //    else
        //    {
        //        TaintTraceResultSinkLegendTB.Visible = false;
        //        TaintTraceResultSourceLegendTB.Visible = false;
        //        TaintTraceResultSourcePlusSinkLegendTB.Visible = false;
        //        TaintTraceResultSourceToSinkLegendTB.Visible = false;
        //        JSTaintShowLinesLbl.Visible = false;
        //        JSTaintShowCleanCB.Visible = false;
        //        JSTaintShowSourceCB.Visible = false;
        //        JSTaintShowSinkCB.Visible = false;
        //        JSTaintShowSourceToSinkCB.Visible = false;
        //    }
        //}

        //private void JSTainTraceEditMenu_Opening(object sender, CancelEventArgs e)
        //{
        //    AddSourceTaintToolStripMenuItem.Visible = false;
        //    AddSinkTaintToolStripMenuItem.Visible = false;
        //    RemoveSourceTaintToolStripMenuItem.Visible = false;
        //    RemoveSinkTaintToolStripMenuItem.Visible = false;

        //    if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null) return;

        //    if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.Orange)
        //    {
        //        RemoveSourceTaintToolStripMenuItem.Visible = true;
        //        AddSinkTaintToolStripMenuItem.Visible = true;
        //    }
        //    else if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.HotPink)
        //    {
        //        RemoveSinkTaintToolStripMenuItem.Visible = true;
        //        AddSourceTaintToolStripMenuItem.Visible = true;
        //    }
        //    else if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.IndianRed)
        //    {
        //        RemoveSinkTaintToolStripMenuItem.Visible = true;
        //        RemoveSourceTaintToolStripMenuItem.Visible = true;
        //    }
        //    else if (JSTaintResultGrid.SelectedRows[0].Cells[1].Style.BackColor == Color.Red)
        //    {
        //        RemoveSinkTaintToolStripMenuItem.Visible = true;
        //        RemoveSourceTaintToolStripMenuItem.Visible = true;
        //    }
        //    else
        //    {
        //        AddSinkTaintToolStripMenuItem.Visible = true;
        //        AddSourceTaintToolStripMenuItem.Visible = true;
        //    }
        //}

        //private void AddSourceTaintToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    int LineNo = GetLineFromTaintGrid();
        //    if (LineNo == 0) return;
        //    if (!IronJint.SourceLinesToInclude.Contains(LineNo))IronJint.SourceLinesToInclude.Add(LineNo);
        //    if (IronJint.SourceLinesToIgnore.Contains(LineNo)) IronJint.SourceLinesToIgnore.Remove(LineNo);
        //    IronJint.ReDoTraceFromUI();
        //}

        //private void AddSinkTaintToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    int LineNo = GetLineFromTaintGrid();
        //    if (LineNo == 0) return;
        //    if (!IronJint.SinkLinesToInclude.Contains(LineNo)) IronJint.SinkLinesToInclude.Add(LineNo);
        //    if (IronJint.SinkLinesToIgnore.Contains(LineNo)) IronJint.SinkLinesToIgnore.Remove(LineNo);
        //    IronJint.ReDoTraceFromUI();
        //}

        //private void RemoveSourceTaintToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    int LineNo = GetLineFromTaintGrid();
        //    if (LineNo == 0) return;
        //    if(!IronJint.SourceLinesToIgnore.Contains(LineNo)) IronJint.SourceLinesToIgnore.Add(LineNo);
        //    if (IronJint.SourceLinesToInclude.Contains(LineNo)) IronJint.SourceLinesToInclude.Remove(LineNo);
        //    IronJint.ReDoTraceFromUI();
        //}

        //private void RemoveSinkTaintToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    int LineNo = GetLineFromTaintGrid();
        //    if (LineNo == 0) return;
        //    if (!IronJint.SinkLinesToIgnore.Contains(LineNo)) IronJint.SinkLinesToIgnore.Add(LineNo);
        //    if (IronJint.SinkLinesToInclude.Contains(LineNo)) IronJint.SinkLinesToInclude.Remove(LineNo);
        //    IronJint.ReDoTraceFromUI();
        //}

        //private int GetLineFromTaintGrid()
        //{
        //    if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null) return 0;

        //    try
        //    {
        //        int LineNo = Int32.Parse(JSTaintResultGrid.SelectedCells[0].Value.ToString());
        //        return LineNo;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        //private void PauseAtTaintCB_Click(object sender, EventArgs e)
        //{
        //    IronJint.PauseAtTaint = PauseAtTaintCB.Checked;
        //}

        //private void JSTaintContinueBtn_Click(object sender, EventArgs e)
        //{
        //    IronJint.UIIJ.MSR.Set();
        //    JSTaintContinueBtn.Visible = false;
        //    JSTaintResultGrid.Focus();
        //}

        //private void JSTaintResultGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    try
        //    {
        //        int LineNo = Int32.Parse(JSTaintResultGrid.SelectedCells[0].Value.ToString());
        //        IronUI.ShowTaintReasons(LineNo, IronJint.UIIJ.GetSourceReasons(LineNo), IronJint.UIIJ.GetSinkReasons(LineNo));
        //    }catch{}
        //}

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

        //private void JSTaintConfigShowHideBtn_Click(object sender, EventArgs e)
        //{
        //    JSTaintShowLinesLbl.Visible = JSTaintShowCleanCB.Visible = JSTaintShowSourceCB.Visible = JSTaintShowSinkCB.Visible = JSTaintShowSourceToSinkCB.Visible = JSTaintConfigPanel.Visible;
        //    if (JSTaintConfigPanel.Visible == true)
        //    {
        //        JSTaintConfigPanel.Visible = false;
        //        JSTaintConfigShowHideBtn.Text = "Show Taint Config";
        //    }
        //    else
        //    {
        //        JSTaintConfigPanel.Height = 450;
        //        JSTaintConfigGrid.Height = 400;
        //        JSTaintConfigPanel.Visible = true;
        //        JSTaintConfigShowHideBtn.Text = "Hide Taint Config";
        //    }
        //}

        //private void TaintTraceResetTaintConfigBtn_Click(object sender, EventArgs e)
        //{
        //    IronJint.ShowDefaultTaintConfig();
        //}

        private void ConsoleStatusTB_Enter(object sender, EventArgs e)
        {
            ConsoleScanUrlTB.Focus();
        }

        private void ProbeLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProbeLogGrid.SelectedCells.Count < 1 || ProbeLogGrid.SelectedCells[0].Value == null || ProbeLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            IronLog.ShowLog(RequestSource.Probe, ProbeLogGrid.SelectedCells[0].Value.ToString(), ProbeLogGrid.SelectedRows[0].Index, false);
            return;
        }

        //private void ConfigJSTaintConfigApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    Config.UpdateJSTaintConfigFromUI();
        //    IronDB.StoreJSTaintConfig();
        //}

        //private void ConfigJSTaintConfigCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    IronUI.UpdateJSTaintConfigInUIFromConfig();
        //}

        //private void TaintTraceClearTaintConfigBtn_Click(object sender, EventArgs e)
        //{
        //    JSTaintConfigGrid.Rows.Clear();
        //}

        private void ConfigScannerThreadMaxCountTB_Scroll(object sender, EventArgs e)
        {
            ConfigScannerThreadMaxCountLbl.Text = ConfigScannerThreadMaxCountTB.Value.ToString();
            ConfigScannerThreadMaxCountLbl.ForeColor = Color.FloralWhite;
        }

        private void SelectResponseForJavaScriptTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature has been temporarily disabled. It will be reintroduced in a future version.");
            //IronLog.MarkForJavaScriptTesting(GetSource(), GetID());
        }

        private void ConfigScannerSettingsApplyChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Config.UpdateScannerSettingsFromUI();
            IronDB.StoreScannerSettings();
            ConfigScannerThreadMaxCountLbl.ForeColor = Color.Black;
        }

        private void ConfigScannerSettingsCancelChangesLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IronUI.UpdateScannerSettingsInUIFromConfig();
            ConfigScannerThreadMaxCountLbl.ForeColor = Color.Black;
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
                Tools.Run(Config.RootDir + "/RenderHtml.exe");
            }
            catch (Exception Exp) { IronException.Report("Unable to Open RenderHtml", Exp); }
        }

        //private void JSTaintShowCleanCB_CheckedChanged(object sender, EventArgs e)
        //{
        //    IronUI.SetJSTaintTraceResult();
        //}

        //private void JSTaintShowSourceCB_CheckedChanged(object sender, EventArgs e)
        //{
        //    IronUI.SetJSTaintTraceResult();
        //}

        //private void JSTaintShowSinkCB_CheckedChanged(object sender, EventArgs e)
        //{
        //    IronUI.SetJSTaintTraceResult();
        //}

        //private void JSTaintShowSourceToSinkCB_CheckedChanged(object sender, EventArgs e)
        //{
        //    IronUI.SetJSTaintTraceResult();
        //}

        //private void CopyLineTaintToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (JSTaintResultGrid.SelectedCells.Count < 1 || JSTaintResultGrid.SelectedCells[0].Value == null) return;
        //    try
        //    {
        //        try
        //        {
        //            Clipboard.SetText("Copy Failed!");
        //        }
        //        catch { }
        //        string Line = JSTaintResultGrid.SelectedCells[1].Value.ToString();
        //        try
        //        {
        //            Clipboard.SetText(Line);
        //        }
        //        catch { }
        //    }
        //    catch
        //    {}
        //}

        private void LogOptionsBtn_Click(object sender, EventArgs e)
        {
            LogOptionsBtn.ContextMenuStrip.Show(LogOptionsBtn, new Point(0, LogOptionsBtn.Height));
        }

        private void ProxyOptionsBtn_Click(object sender, EventArgs e)
        {
            ProxyOptionsBtn.ContextMenuStrip.Show(ProxyOptionsBtn, new Point(0, LogOptionsBtn.Height));
        }

        //private void PauseAtTaintCB_CheckedChanged(object sender, EventArgs e)
        //{
        //    JSTaintShowCleanCB.Enabled = !PauseAtTaintCB.Checked;
        //    JSTaintShowSourceCB.Enabled = !PauseAtTaintCB.Checked;
        //    JSTaintShowSinkCB.Enabled = !PauseAtTaintCB.Checked;
        //    JSTaintShowSourceToSinkCB.Enabled = !PauseAtTaintCB.Checked;
        //}

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

        private void PromptBlinkTimer_Tick(object sender, EventArgs e)
        {
            if (InteractiveShellPromptBox.ForeColor == Color.Lime)
                InteractiveShellPromptBox.ForeColor = Color.Black;
            else
                InteractiveShellPromptBox.ForeColor = Color.Lime;
        }

        private void EndOfShellPromptBlink()
        {
            Config.BlinkPrompt = false;
            PromptBlinkTimer.Stop();
            InteractiveShellPromptBox.ForeColor = Color.Lime;
        }

        private void main_tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (main_tab.SelectedTab.Name.Equals("mt_scripting"))
            {
                if (Config.BlinkPrompt) PromptBlinkTimer.Start();
            }
            else
            {
                if (Config.BlinkPrompt) PromptBlinkTimer.Stop();
            }
        }

        private void LogTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            IronUI.ShowCurrentLogStat();
            IronUI.ShowLogBottomStatus("", false);
        }

        private void MainLogFrontOneBtn_Click(object sender, EventArgs e)
        {
            MoveLog(true, 1);
        }

        private void MainLogFrontTwoBtn_Click(object sender, EventArgs e)
        {
            MoveLog(true, 2);
        }

        private void MainLogFrontThreeBtn_Click(object sender, EventArgs e)
        {
            MoveLog(true, 3);
        }

        private void MainLogFrontFourBtn_Click(object sender, EventArgs e)
        {
            MoveLog(true, 4);
        }

        private void MainLogBackOneBtn_Click(object sender, EventArgs e)
        {
            MoveLog(false, 1);
        }

        private void MainLogBackTwoBtn_Click(object sender, EventArgs e)
        {
            MoveLog(false, 2);
        }

        private void MainLogBackThreeBtn_Click(object sender, EventArgs e)
        {
            MoveLog(false, 3);
        }

        private void MainLogBackFourBtn_Click(object sender, EventArgs e)
        {
            MoveLog(false, 4);
        }

        void MoveLog(bool Forward, int Level)
        {
            IronUI.ShowLogBottomStatus("Loading...", false);
            switch (LogTabs.SelectedTab.Name)
            {
                case ("ProxyLogTab"):
                    if (Forward)
                        IronLog.MoveProxyLogRecordForward(Level);
                    else
                        IronLog.MoveProxyLogRecordBack(Level);
                    break;
                case ("ScanLogTab"):
                    if (Forward)
                        IronLog.MoveScanLogRecordForward(Level);
                    else
                        IronLog.MoveScanLogRecordBack(Level);
                    break;
                case ("TestLogTab"):
                    if (Forward)
                        IronLog.MoveTestLogRecordForward(Level);
                    else
                        IronLog.MoveTestLogRecordBack(Level);
                    break;
                case ("ShellLogTab"):
                    if (Forward)
                        IronLog.MoveShellLogRecordForward(Level);
                    else
                        IronLog.MoveShellLogRecordBack(Level);
                    break;
                case ("ProbeLogTab"):
                    if (Forward)
                        IronLog.MoveProbeLogRecordForward(Level);
                    else
                        IronLog.MoveProbeLogRecordBack(Level);
                    break;
                case ("OtherLogTab"):
                    if (Forward)
                        IronLog.MoveOtherLogRecordForward(Level);
                    else
                        IronLog.MoveOtherLogRecordBack(Level);
                    break;
                case ("SiteMapLogTab"):
                    IronUI.ShowLogBottomStatus("Cannot move SiteMap logs. Move Proxy/Probe logs and click on the SiteMap tree to display new logs", true);
                    break;
            }
        }

        private void ScanTraceFrontOneBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordForward(1);
        }

        private void ScanTraceFrontTwoBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordForward(2);
        }

        private void ScanTraceFrontThreeBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordForward(3);
        }

        private void ScanTraceFrontFourBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordForward(4);
        }

        private void ScanTraceBackOneBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordBack(1);
        }

        private void ScanTraceBackTwoBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordBack(2);
        }

        private void ScanTraceBackThreeBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordBack(3);
        }

        private void ScanTraceBackFourBtn_Click(object sender, EventArgs e)
        {
            IronTrace.MoveScanTraceRecordBack(4);
        }

        private void ProxyShowOriginalRequestCB_CheckedChanged(object sender, EventArgs e)
        {
            if (ProxyShowOriginalRequestCB.Checked)
            {
                if (IronLog.CurrentSession.OriginalRequest != null)
                    IronUI.FillLogFields(IronLog.CurrentSession.OriginalRequest);
            }
            else
            {
                IronUI.FillLogFields(IronLog.CurrentSession.Request);
            }
        }

        private void ProxyShowOriginalResponseCB_CheckedChanged(object sender, EventArgs e)
        {
            if (ProxyShowOriginalResponseCB.Checked)
            {
                if (IronLog.CurrentSession.OriginalResponse != null)
                    IronUI.FillLogFields(IronLog.CurrentSession.OriginalResponse, null);
            }
            else
            {
                IronUI.FillLogFields(IronLog.CurrentSession.Response, null);
            }
        }

        private void UIDesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IronUI.IsUIDesignerOpen())
            {
                IronUI.UD.Activate();
            }
            else
            {
                IronUI.UD = new ModUiDesigner();
                IronUI.UD.Show();
            }
        }

        private void CustomSendActivateCB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CustomSendActivateCB.Checked)
            {
                CustomSendErrorTB.Visible = false;
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

            if (this.CustomSendActivateCB.Checked)
            {
                ScriptedSendTP.BackColor = Color.DarkGreen;
            }
            else
            {
                ScriptedSendTP.BackColor = Color.White;
            }
        }

        private void SelectedModuleReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PluginAndModuleTree.SelectedNode == null) return;
            TreeNode SelectedNode = PluginAndModuleTree.SelectedNode;
            if (SelectedNode.Level == 3 && SelectedNode.Parent.Parent.Index == 1)
            {
                IronThread.Run(Module.ReloadModule, SelectedNode.Name);
            }
        }

        private void OtherLogSourceGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OtherLogSourceGrid.SelectedRows == null) return;
            if (OtherLogSourceGrid.SelectedRows.Count == 0) return;
            IronLog.SelectedOtherSource = OtherLogSourceGrid.SelectedRows[0].Cells[0].Value.ToString();
            IronLog.CurrentSource = IronLog.SelectedOtherSource;
            Thread T = new Thread(IronLog.ShowOtherSourceRecords);
            T.Start();
        }

        private void OtherLogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OtherLogGrid.SelectedCells.Count < 1 || OtherLogGrid.SelectedCells[0].Value == null || OtherLogGrid.SelectedRows.Count == 0)
            {
                return;
            }
            IronLog.ShowLog(IronLog.SelectedOtherSource, OtherLogGrid.SelectedCells[0].Value.ToString(), OtherLogGrid.SelectedRows[0].Index, false);
            return;

        }

        //private void ConfigureScanRequestFormatPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (Scanner.CurrentScanner == null) return;
        //    if (Scanner.CurrentScanner.OriginalRequest == null) return;
        //    string PluginName = "";
        //    ASExceptionTB.Text = "";
        //    foreach (DataGridViewRow Row in ConfigureScanRequestFormatPluginsGrid.Rows)
        //    {
        //        if (e.RowIndex == Row.Index)
        //        {
        //            Row.Cells[0].Value = true;
        //            PluginName = Row.Cells[1].Value.ToString();
        //        }
        //        else
        //        {
        //            Row.Cells[0].Value = false;
        //        }
        //    }

        //    ConfigureScanRequestFormatXMLTB.Text = "";
            
        //    //if (PluginName.Equals("None"))
        //    //{
        //    //    IronUI.UpdateScanBodyTabWithDataInDefaultFormat();
        //    //    ConfigureScanRequestFormatXMLTB.Text = "";
        //    //    Scanner.CurrentScanner.BodyFormat = new FormatPlugin();
        //    //    return;
        //    //}
        //    if (!FormatPlugin.List().Contains(PluginName))
        //    {
        //        IronUI.ShowConfigureScanException("Format Plugin not found");
        //        return;
        //    }
        //    FormatPlugin Plugin = FormatPlugin.Get(PluginName);
        //    if (Scanner.CurrentScanner == null)
        //    {
        //        IronUI.ShowConfigureScanException("Invalid Request");
        //        return;
        //    }
        //    if (Scanner.CurrentScanner.OriginalRequest == null)
        //    {
        //        IronUI.ShowConfigureScanException("Invalid Request");
        //        return;
        //    }
        //    //Scanner.CurrentScanner.BodyFormat = Plugin;
        //    Scanner.StartDeSerializingRequestBody(Scanner.CurrentScanner.OriginalRequest, Plugin, new List<bool>(), false);
        //}

       // private void ScanJobCustomizeBtn_Click(object sender, EventArgs e)
       // {
       //     ScanCustomizationAssistant SCA = new ScanCustomizationAssistant();
       //     SCA.Show();
       //}

        //private void ASBaseTab_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (e.TabPageIndex == 0)
        //    {
        //        try
        //        {
        //            IronUI.HandleAnyChangesInConfigureScanRequest();
        //        }
        //        catch (Exception Exp)
        //        {
        //            IronUI.ShowConfigureScanException(Exp.Message);
        //        }
        //    }
        //}

        //private void ASBodyInjectTypeTabs_Selecting(object sender, TabControlCancelEventArgs e)
        //{
        //    //If none of the options are selected then select normal
        //    if (!(ASBodyTypeNormalRB.Checked || ASBodyTypeFormatPluginRB.Checked || ASBodyTypeCustomRB.Checked))
        //        ASBodyTypeNormalRB.Checked = true;

        //    switch (e.TabPage.Name)
        //    {
        //        case ("ASBodyTypeNormalTab"):
        //            if (!ASBodyTypeNormalRB.Checked)
        //            {
        //                if (ASBodyTypeFormatPluginRB.Checked)
        //                    ASBodyInjectTypeTabs.SelectTab("ASBodyTypeFormatPluginTab");
        //                else if(ASBodyTypeCustomRB.Checked)
        //                    ASBodyInjectTypeTabs.SelectTab("ASBodyTypeCustomTab");
        //            }
        //            break;
        //        case ("ASBodyTypeFormatPluginTab"):
        //            if (!ASBodyTypeFormatPluginRB.Checked)
        //            {
        //                if (ASBodyTypeNormalRB.Checked)
        //                    ASBodyInjectTypeTabs.SelectTab("ASBodyTypeNormalTab");
        //                else if (ASBodyTypeCustomRB.Checked)
        //                    ASBodyInjectTypeTabs.SelectTab("ASBodyTypeCustomTab");
        //            }
        //            break;
        //        case ("ASBodyTypeCustomTab"):
        //            if (!ASBodyTypeCustomRB.Checked)
        //            {
        //                if (ASBodyTypeFormatPluginRB.Checked)
        //                    ASBodyInjectTypeTabs.SelectTab("ASBodyTypeFormatPluginTab");
        //                else if (ASBodyTypeNormalRB.Checked)
        //                    ASBodyInjectTypeTabs.SelectTab("ASBodyTypeNormalTab");
        //            }
        //            if (!ASRequestCustomInjectionMarkerTabs.SelectedTab.Name.Equals("ASRequestCustomMarkerSelectionTab"))
        //                ASRequestCustomInjectionMarkerTabs.SelectTab("ASRequestCustomMarkerSelectionTab");
        //            break;
        //    }
        //}

        //private void ASBodyTypeNormalRB_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ASBodyTypeNormalRB.Checked)
        //    {
        //        IronUI.UpdateScanBodyTabWithDataInDefaultFormat();
        //        ASBodyInjectTypeTabs.SelectTab("ASBodyTypeNormalTab");
        //    }
        //}

        //private void ASBodyTypeFormatPluginRB_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ASBodyTypeFormatPluginRB.Checked)
        //    {
        //        ASRequestScanAllCB.Checked = false;
        //        ASRequestScanBodyCB.Checked = false;
        //        foreach (DataGridViewRow Row in ConfigureScanRequestFormatPluginsGrid.Rows)
        //        {
        //            Row.Cells[0].Value = false;
        //        }
        //        ConfigureScanRequestBodyTypeFormatPluginGrid.Rows.Clear();
        //        ConfigureScanRequestFormatXMLTB.Text = "";
        //        ASBodyInjectTypeTabs.SelectTab("ASBodyTypeFormatPluginTab");
        //    }
        //}

        //private void ASBodyTypeCustomRB_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ASBodyTypeCustomRB.Checked)
        //    {
        //        ASRequestScanAllCB.Checked = false;
        //        ASRequestScanBodyCB.Checked = false;
        //        ASBodyInjectTypeTabs.SelectTab("ASBodyTypeCustomTab");
        //        if (Scanner.CurrentScanner != null && Scanner.CurrentScanner.OriginalRequest != null)
        //        {
        //            ASRequestCustomInjectionPointsHighlightTB.Text = Scanner.CurrentScanner.OriginalRequest.BodyString;
        //        }
        //        ASRequestCustomInjectionPointsHighlightLbl.Text = "Number of Injection Points Detected: 0";
        //        ASRequestCustomInjectionMarkerTabs.SelectTab("ASRequestCustomMarkerSelectionTab");
        //    }
        //}

        //private void ASRequestScanBodyTypeNormalGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (ASRequestScanBodyTypeNormalGrid.SelectedCells.Count < 1 || ASRequestScanBodyTypeNormalGrid.SelectedCells[0].Value == null)
        //    {
        //        return;
        //    }
        //    if ((bool)this.ASRequestScanBodyTypeNormalGrid.SelectedCells[0].Value)
        //    {
        //        this.ASRequestScanBodyTypeNormalGrid.SelectedCells[0].Value = false;
        //        this.ASRequestScanAllCB.Checked = false;
        //        this.ASRequestScanBodyCB.Checked = false;
        //    }
        //    else
        //    {
        //        this.ASRequestScanBodyTypeNormalGrid.SelectedCells[0].Value = true;
        //    }
        //}

        //private void ASApplyCustomMarkersLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    string StartMarker = ASCustomStartMarkerTB.Text.Trim();
        //    string EndMarker = ASCustomEndMarkerTB.Text.Trim();
        //    if (StartMarker.Length == 0 || EndMarker.Length == 0)
        //    {
        //        IronUI.ShowConfigureScanException("Start and End markers cannot be empty.");
        //        return;
        //    }
        //    if (StartMarker.Equals(EndMarker))
        //    {
        //        IronUI.ShowConfigureScanException("Start and End markers cannot be the same.");
        //        return;
        //    }
        //    Scanner.CurrentScanner.CustomInjectionPointStartMarker = StartMarker;
        //    Scanner.CurrentScanner.CustomInjectionPointEndMarker = EndMarker;
        //    IronUI.DetectAndHighLightCustomInjectionPoints();
        //}

        //private void ASCustomStartMarkerTB_TextChanged(object sender, EventArgs e)
        //{
        //    ASRequestScanBodyCB.Checked = false;
        //    ASRequestCustomInjectionPointsHighlightTB.Text = Scanner.CurrentScanner.OriginalRequest.BodyString;
        //    ASRequestCustomInjectionPointsHighlightLbl.Text = "Number of Injection Points Detected: 0";
        //}

        //private void ASCustomEndMarkerTB_TextChanged(object sender, EventArgs e)
        //{
        //    ASRequestScanBodyCB.Checked = false;
        //    ASRequestCustomInjectionPointsHighlightTB.Text = Scanner.CurrentScanner.OriginalRequest.BodyString;
        //    ASRequestCustomInjectionPointsHighlightLbl.Text = "Number of Injection Points Detected: 0";
        //}

        private void ASClearScanBtn_Click(object sender, EventArgs e)
        {
            Scanner.ResetChangedStatus();
            IronUI.ResetConfigureScanFields();
            Scanner.CurrentScanner = null;
            ScanDisplayPanel.Visible = false;
            ScanTopPanel.Visible = true;
            ScanJobsBaseSplit.SplitterDistance = 62;
        }

        private void ASStopAllScansLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StopAllScansAction();
        }

        private void ASStartAllStoppedAndAbortedScansLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartAllStoppedAndAbortedScansAction();
        }

        private void CreateNewTestRequestBtn_Click(object sender, EventArgs e)
        {
            CreateNewRequestWizard CNRW = new CreateNewRequestWizard();
            CNRW.Show();
        }

        void CreateImageList()
        {
            ImageList IL = new ImageList();
            IL.ImageSize = new System.Drawing.Size(10, 10);
            Bitmap BM = new Bitmap(10, 10);
            Graphics G = Graphics.FromImage(BM);
            SolidBrush B = new SolidBrush(Color.LightSkyBlue);
            G.FillRectangle(B, 0, 0, 10, 10);
            IL.Images.Add("Square", BM);

            TestGroupsLV.SmallImageList = IL;
            TestGroupsLV.LargeImageList = IL;
        }

        private void TestGroupsLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TestGroupsLV.SelectedItems.Count > 0)
            {
                if (!MTBaseSplit.Visible) MTBaseSplit.Visible = true;
                string Group = TestGroupsLV.SelectedItems[0].Name;
                if (!Group.Equals(ManualTesting.CurrentGroup))
                    ManualTesting.ShowGroup(Group);
            }
        }

        private void MTDeleteGroupLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                TestGroupsLV.Items.RemoveByKey(ManualTesting.CurrentGroup);
                IronUI.ResetMTDisplayFields();
                TestIDLbl.Text = "ID: 0";
                MTCurrentGroupNameTB.Text = "";
                TestGroupLogGrid.Rows.Clear();
                ManualTesting.ClearGroup();
                //MTReqResTabs.SelectTab("MTRequestTab");
                MTBaseSplit.Visible = false;
                if (TestGroupsLV.Items.Count == 0)
                {
                    TestGroupsTitleTB.Visible = false;
                    TestGroupsLV.Visible = false;
                }
            }
            catch { }
        }

        private void MTRenameGroupLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            string NewName = MTCurrentGroupNameTB.Text.Trim();
            if (NewName.Length == 0)
            {
                MTCurrentGroupNameTB.Text = ManualTesting.CurrentGroup;
                IronUI.ShowMTException("A group with name already exists");
                return;
            }
            if (ManualTesting.GroupSessions.ContainsKey(NewName))
            {
                MTCurrentGroupNameTB.Text = ManualTesting.CurrentGroup;
                IronUI.ShowMTException("A group with name already exists");
                return;
            }
            Dictionary<int, Session> SessionsList = ManualTesting.GroupSessions[ManualTesting.CurrentGroup];
            int CurrentId = ManualTesting.CurrentGroupLogId[ManualTesting.CurrentGroup];
            ManualTesting.GroupSessions.Remove(ManualTesting.CurrentGroup);
            ManualTesting.CurrentGroupLogId.Remove(ManualTesting.CurrentGroup);
            string OldName = ManualTesting.CurrentGroup;
            TestGroupsLV.Items[OldName].Name = NewName;
            TestGroupsLV.Items[NewName].Text = NewName;
            ManualTesting.CurrentGroup = NewName;
            ManualTesting.GroupSessions[ManualTesting.CurrentGroup] = SessionsList;
            ManualTesting.CurrentGroupLogId[ManualTesting.CurrentGroup] = CurrentId;
            IronDB.RenameGroup(OldName, NewName);
            IronUI.ShowMTException("");
        }

        private void SelectForManualTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IronLog.MarkForTesting(GetSource(), GetID(), "");
        }

        private void MTFollowRedirectBtn_Click(object sender, EventArgs e)
        {
            ManualTesting.FollowRedirect();
        }

        private void MTGetRedirectBtn_Click(object sender, EventArgs e)
        {
            ManualTesting.GetRedirect();
        }

        private void ProxyRequestView_RequestChanged()
        {
            IronProxy.RequestChanged = true;
        }

        private void ProxyResponseView_ResponseChanged()
        {
            IronProxy.ResponseChanged = true;
        }

        private void StartLogAnalyzerBtn_Click(object sender, EventArgs e)
        {
            LogAnalyzer LoAn = new LogAnalyzer();
            LoAn.Show();
        }

        private void LoadSelectedTraceBtn_Click(object sender, EventArgs e)
        {
            //IronTrace Trace = IronDB.GetScanTrace(IronUI.MainViewSelectedScanTraceId);
            LogTraceViewer TraceViewer = new LogTraceViewer(IronUI.MainViewSelectedScanTraceId);
            //TraceViewer.ScanTraceMsgRTB.Rtf = ScanTraceMsgRTB.Rtf;
            //foreach (DataGridViewRow Row in ScanTraceOverviewGrid.Rows)
            //{
            //    object[] RowValues = new object[Row.Cells.Count + 1];
            //    RowValues[0] = false;
            //    foreach (DataGridViewCell Cell in Row.Cells)
            //    {
            //        RowValues[Cell.ColumnIndex + 1] = Cell.Value;
            //    }
            //    TraceViewer.ScanTraceOverviewGrid.Rows.Add(RowValues);
            //}
            TraceViewer.Show();
        }

        private void ASBodyInjectTypeTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (ASBodyInjectTypeTabs.SelectedIndex != Scanner.CurrentScannerBodyFormatTabIndex)
                ASBodyInjectTypeTabs.SelectTab(Scanner.CurrentScannerBodyFormatTabIndex);
        }

        private void TestGroupHistoryDoDiffBtn_Click(object sender, EventArgs e)
        {
            int ALogId = -1;
            int BLogId = -1;
            int SelectedRowsCount = 0;
            foreach (DataGridViewRow Row in TestGroupLogGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    SelectedRowsCount++;
                    if (ALogId == -1)
                    {
                        try
                        {
                            ALogId = Int32.Parse(Row.Cells[1].Value.ToString());
                        }
                        catch { }
                    }
                    else if (BLogId == -1)
                    {
                        try
                        {
                            BLogId = Int32.Parse(Row.Cells[1].Value.ToString());
                        }
                        catch { }
                    }
                }
            }

            if (SelectedRowsCount == 2)
            {             
                SessionsDiffer Sdiff = new SessionsDiffer();
                Sdiff.SetSessions("Test", ALogId, BLogId);
                Sdiff.Show();
            }
            else
            {
                MessageBox.Show(string.Format("Diff can be done only when two sessions are selected. You have selected {0} sessions", SelectedRowsCount), "Selection Error");
            }
        }

        private void SessionPluginTraceLoadLogBtn_Click(object sender, EventArgs e)
        {
            IronTrace.LoadSessionPluginTraceLog();
            if (!SessionPluginTraceBottomTabs.SelectedTab.Name.Equals("SessionPluginTraceLogViewTab"))
            {
                SessionPluginTraceBottomTabs.SelectTab("SessionPluginTraceLogViewTab");
            }
        }

        private void sessionPluginCreationAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionPluginCreationAssistant SPCA = new SessionPluginCreationAssistant();
            SPCA.Show();
        }

        private void SessionPluginTraceGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (SessionPluginTraceGrid.SelectedCells == null || SessionPluginTraceGrid.SelectedCells.Count == 0) return;
            try
            {
                IronTrace.SelectedSessionPluginTraceLogId = Int32.Parse(SessionPluginTraceGrid.SelectedRows[0].Cells["SessionPluginTraceLogIdClmn"].Value.ToString());
                IronTrace.SelectedSessionPluginTraceSource = SessionPluginTraceGrid.SelectedRows[0].Cells["SessionPluginTraceLogSourceClmn"].Value.ToString();
                SessionPluginTraceLoadLogBtn.Enabled = (IronTrace.SelectedSessionPluginTraceLogId > 0);
                SessionPluginTraceMsgRTB.Text = SessionPluginTraceGrid.SelectedRows[0].Cells["SessionPluginTraceMessageClmn"].Value.ToString();
                IronUI.ShowSessionPluginTraceLog(null, null);
                if (!SessionPluginTraceBottomTabs.SelectedTab.Name.Equals("SessionPluginTraceMessageTab"))
                {
                    SessionPluginTraceBottomTabs.SelectTab("SessionPluginTraceMessageTab");
                }
            }
            catch { }
        }

        private void activePluginCreationAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivePluginCreationAssistant APCA = new ActivePluginCreationAssistant();
            APCA.Show();
        }

        private void MTMaximizeRequestViewBtn_Click(object sender, EventArgs e)
        {
            if (MTMaximizeRequestViewBtn.Text.Equals("\\/"))
            {
                MTBaseSplit.SplitterDistance = MTBaseSplit.Height - 10;
                MTMaximizeRequestViewBtn.Text = "--";
            }
            else
            {
                MTBaseSplit.SplitterDistance = MTBaseSplit.Height/2;
                MTMaximizeRequestViewBtn.Text = "\\/";
            }
        }

        private void MTMaximizeResponseViewBtn_Click(object sender, EventArgs e)
        {
            if (MTMaximizeResponseViewBtn.Text.Equals("/\\"))
            {
                MTBaseSplit.SplitterDistance = 10;
                MTMaximizeResponseViewBtn.Text = "--";
            }
            else
            {
                MTBaseSplit.SplitterDistance = MTBaseSplit.Height / 2;
                MTMaximizeResponseViewBtn.Text = "/\\";
            }
        }

        private void passivePluginCreationAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PassivePluginCreationAssistant PPCA = new PassivePluginCreationAssistant();
            PPCA.Show();
        }

        private void scriptCreationAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptCreationAssistant SCA = new ScriptCreationAssistant();
            SCA.Show();
        }

        private void ShowScriptCreationAssistantBtn_Click(object sender, EventArgs e)
        {
            ScriptCreationAssistant SCA = new ScriptCreationAssistant();
            SCA.Show();
        }

        private void ScriptedInterceptionRunScriptCreationAssistantBtn_Click(object sender, EventArgs e)
        {
            ScriptCreationAssistant SCA = new ScriptCreationAssistant();
            SCA.Show();
        }

        private void ScriptedInterceptionRubyRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ScriptedInterceptionRubyRB.Checked)
            {
                this.ShowScriptedInterceptionTemplateLL.Text = "Show sample Ruby script";
                
                ScriptedInterceptionCTB.LangCode = 2;

                this.ScriptedInterceptionScriptTopRTB.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue255;\red25\green25\blue112;} \cf1 def \cf0 \cf2 \b1 should_intercept \b0 \cf0 (sess)";
                this.ScriptedInterceptionScriptBottomRTB.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue128;\red0\green0\blue255;} \cf1     return \cf0 false \par \cf2 end \cf0";
                this.ScriptedInterceptionActivateScriptCB.Checked = false;
                
                Directory.SetCurrentDirectory(Config.RootDir);
            }
        }

        private void ScriptedInterceptionPythonRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ScriptedInterceptionPythonRB.Checked)
            {
                this.ShowScriptedInterceptionTemplateLL.Text = "Show sample Python script";
                
                ScriptedInterceptionCTB.LangCode = 2;

                this.ScriptedInterceptionScriptTopRTB.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue255;\red25\green25\blue112;} \cf1 def \cf0 \cf2 \b1 ShouldIntercept \b0 \cf0 (sess):";
                this.ScriptedInterceptionScriptBottomRTB.Rtf = @"{\rtf1{\colortbl ;\red0\green0\blue128;} \cf1     return \cf0 False";
                this.ScriptedInterceptionActivateScriptCB.Checked = false;

                Directory.SetCurrentDirectory(Config.RootDir);
            }
        }

        private void ScriptedInterceptionActivateScriptCB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ScriptedInterceptionActivateScriptCB.Checked)
            {
                ScriptedInterceptionErrorTB.Visible = false;
                string Result = "";
                if (this.ScriptedInterceptionPythonRB.Checked)
                {
                    Result = IronProxy.SetPyScriptedInterception(this.ScriptedInterceptionCTB.Text);
                }
                else
                {
                    Result = IronProxy.SetRbScriptedInterception(this.ScriptedInterceptionCTB.Text);
                }
                if (Result.Length > 0)
                {
                    IronUI.ShowScriptedInterceptionScriptException(Result);
                }
            }
            
            IronProxy.ScriptedInterceptionEnabled = this.ScriptedInterceptionActivateScriptCB.Checked;

            if (IronProxy.ScriptedInterceptionEnabled)
            {
                this.InterceptRequestCB.Enabled = false;
                this.InterceptResponseCB.Enabled = false;
            }
            else
            {
                this.InterceptRequestCB.Enabled = true;
                this.InterceptResponseCB.Enabled = true;
            }

            if (this.ScriptedInterceptionActivateScriptCB.Checked)
            {
                ScriptedInterceptionBaseSplit.Panel1.BackColor = Color.DarkGreen;
            }
            else
            {
                ScriptedInterceptionBaseSplit.Panel1.BackColor = Color.White;
            }
        }

        private void ScriptedInterceptionCTB_ValueChanged()
        {
            if (this.ScriptedInterceptionActivateScriptCB.Checked)
            {
                this.ScriptedInterceptionActivateScriptCB.Checked = false;
            }
            IronUI.ResetScriptedInterceptionScriptExceptionFields();
            
            if (IronProxy.ScriptedInterceptionEnabled)
            {
                IronProxy.ScriptedInterceptionEnabled = false;
            }
        }

        private void ShowScriptedInterceptionTemplateLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowSampleScriptedInterceptionScript();
        }

        void ShowSampleScriptedInterceptionScript()
        {
            try
            {
                if (ScriptedInterceptionPythonRB.Checked)
                {
                    ScriptedInterceptionCTB.Text = ScriptedInterceptor.GetSamplePythonScript();
                }
                else
                {
                    ScriptedInterceptionCTB.Text = ScriptedInterceptor.GetSampleRubyScript();
                }
            }
            catch(Exception Exp) 
            {
                IronException.Report("Unable to show the sample script", Exp);
            }
        }

        private void ShowScriptedSendTemplateLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowSampleScriptedSendScript();
        }

        void ShowSampleScriptedSendScript()
        {
            try
            {
                if (CustomSendPythonRB.Checked)
                {
                    CustomSendTE.Text = ScriptedSender.GetSamplePythonScript();
                }
                else
                {
                    CustomSendTE.Text = ScriptedSender.GetSampleRubyScript();
                }
            }
            catch(Exception Exp) 
            {
                IronException.Report("Unable to show the sample script", Exp);
            }
        }

        private void TestAdvancedOptionsHelpLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            main_tab.SelectTab("mt_scripting");
            ScriptingShellTabs.SelectTab("ScriptedSendTP");
        }

        private void ProxyInterceptTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (ScriptedInterceptionBaseSplit.SplitterDistance != 130)
            {
                try
                {
                    ScriptedInterceptionBaseSplit.SplitterDistance = 130;
                }
                catch { }
                try
                {
                    ScriptedInterceptionBottomSplit.SplitterDistance = ScriptedInterceptionBottomSplit.Height - 118;
                }
                catch { }
            }
        }

        private void ProxyInterceptTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProxyInterceptTabs.SelectedIndex == 2 && !ScriptedInterceptionBaseSplit.Visible)
            {
                ScriptedInterceptionBaseSplit.Visible = true;
            }
        }

        private void moduleCreationAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModuleCreationAssistant MCA = new ModuleCreationAssistant();
            MCA.Show();
        }

        private void LaunchPayloadEffectAnalyzerBtn_Click(object sender, EventArgs e)
        {
            ScanTraceBehaviourAnalysis STBA = new ScanTraceBehaviourAnalysis();
            STBA.Show();
        }

        private void TestUpdateCookieStoreLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManualTesting.UpdateCookieStoreFromResponse();
            TestUpdateFromCookieStoreLL.Visible = true;
            TestUpdateCookieStoreLL.Visible = false;
        }

        private void TestUpdateFromCookieStoreLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManualTesting.UpdateRequestFromCookieStore();
        }

        private void ViewProxyInterceptionConfigLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigPanel.Height = 350;
            ConfigPanel.Visible = true;
            ConfigViewHideLL.Text = "Hide Config";
            ConfigPanelTabs.SelectTab("ConfigInterceptRulesTab");
        }

        private void ViewProxyDisplayFilterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigPanel.Height = 350;
            ConfigPanel.Visible = true;
            ConfigViewHideLL.Text = "Hide Config";
            ConfigPanelTabs.SelectTab("ConfigDisplayRulesTab");
        }

        private void ResultsShowTriggersMenuLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogMenu.Show(ResultsTriggersGrid, 10, 10);
        }

        private void DoDiffBtn_Click(object sender, EventArgs e)
        {
            List<int> TriggerIds = new List<int>();
            TriggerIds = new List<int>(Finding.TriggersSelectedForDiff);

            if (TriggerIds.Count == 2)
            {
                SessionsDiffer SD = new SessionsDiffer();
                Trigger A;
                Trigger B;
                if (TriggerIds[0] == 0)
                {
                    A = new Trigger("", Finding.CurrentPluginResult.BaseRequest, "", Finding.CurrentPluginResult.BaseResponse);
                }
                else
                {
                    A = Finding.CurrentPluginResult.Triggers.GetTrigger(TriggerIds[0] - 1);
                }
                if (TriggerIds[1] == 0)
                {
                    B = new Trigger("", Finding.CurrentPluginResult.BaseRequest, "", Finding.CurrentPluginResult.BaseResponse);
                }
                else
                {
                    B = Finding.CurrentPluginResult.Triggers.GetTrigger(TriggerIds[1] - 1);
                }
                
                Session First = null;
                Session Second = null;
                if (A.Response == null)
                {
                    First = new Session(A.Request);
                }
                else
                {
                    First = new Session(A.Request, A.Response);
                }
                if (B.Response == null)
                {
                    Second = new Session(B.Request);
                }
                else
                {
                    Second = new Session(B.Request, B.Response);
                }
                SD.SetSessions(First, Second);
                SD.Show();
            }
            else
            {
                if (TriggerIds.Count == 0)
                {
                    MessageBox.Show("Select two items before doing a Diff. You have NOT selected any items currently");
                }
                else if (TriggerIds.Count == 1)
                {
                    MessageBox.Show("Select two items before doing a Diff. You have selected only one item currently");
                }
                else if (TriggerIds.Count > 2)
                {
                    MessageBox.Show("Select two items before doing a Diff. You have selected more than two items currently");
                }
            }
        }

        private void SelectForDiffTriggersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SelectForDiffTriggersGrid.SelectedRows == null) return;
            if (SelectForDiffTriggersGrid.SelectedRows.Count == 0) return;


            SelectForDiffTriggersGrid.SelectedRows[0].Cells[0].Value = !((bool)SelectForDiffTriggersGrid.SelectedRows[0].Cells[0].Value);

            if ((bool)SelectForDiffTriggersGrid.SelectedRows[0].Cells[0].Value)
            {
                if (SelectForDiffTriggersGrid.SelectedRows[0].Cells[1].Value.ToString().Equals("Normal"))
                {
                    Finding.TriggersSelectedForDiff.Add(0);
                }
                else
                {
                    try
                    {
                        Finding.TriggersSelectedForDiff.Add(Int32.Parse(SelectForDiffTriggersGrid.SelectedRows[0].Cells[1].Value.ToString().Replace("Trigger", "").Trim()));
                    }
                    catch { }
                }
                if (Finding.TriggersSelectedForDiff.Count > 2)
                {
                    int i = Finding.TriggersSelectedForDiff[0];
                    Finding.TriggersSelectedForDiff.RemoveAt(0);

                    foreach (DataGridViewRow Row in SelectForDiffTriggersGrid.Rows)
                    {
                        if (i == 0 && Row.Cells[1].Value.ToString().Trim().Equals("Normal"))
                        {
                            Row.Cells[0].Value = false;
                        }
                        else
                        {
                            try
                            {
                                if (i == Int32.Parse(Row.Cells[1].Value.ToString().Replace("Trigger", "").Trim()))
                                {
                                    Row.Cells[0].Value = false;
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            else
            {
                if (SelectForDiffTriggersGrid.SelectedRows[0].Cells[1].Value.ToString().Equals("Normal"))
                {
                    Finding.TriggersSelectedForDiff.Remove(0);
                }
                else
                {
                    try
                    {
                        Finding.TriggersSelectedForDiff.Remove(Int32.Parse(SelectForDiffTriggersGrid.SelectedRows[0].Cells[1].Value.ToString().Replace("Trigger", "").Trim()));
                    }
                    catch { }
                }
            }           
        }

        private void ResultsTriggersGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (ResultsTriggersGrid.SelectedCells.Count < 1 || ResultsTriggersGrid.SelectedCells[0].Value == null)
            {
                return;
            }
            if (Finding.CurrentPluginResult != null)
            {
                if (ResultsTriggersGrid.SelectedCells[0].Value.ToString().Trim().Equals("Normal"))
                {
                    IronUI.DisplayPluginResultsTrigger(-1);
                }
                else
                {
                    int TriggerNumber = Int32.Parse(ResultsTriggersGrid.SelectedCells[0].Value.ToString().Replace("Trigger", "").Trim()) - 1;
                    IronUI.DisplayPluginResultsTrigger(TriggerNumber);
                } 
            }
        }

        private void ResultsShowScanTraceBtn_Click(object sender, EventArgs e)
        {
            try
            {
                List<IronTrace> Traces = IronDB.GetScanTraces(Finding.CurrentPluginResult);
                List<LogTraceViewer> Viewers = new List<LogTraceViewer>();

                foreach (IronTrace Trace in Traces)
                {
                    LogTraceViewer TraceViewer = new LogTraceViewer(Trace);
                    Viewers.Add(TraceViewer);
                    //StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
                    //SB.Append(Tools.RtfSafe(Trace.Message));
                    //TraceViewer.ScanTraceMsgRTB.Rtf = SB.ToString();

                    //try
                    //{
                    //    List<Dictionary<string, string>> OverviewEntries = IronTrace.GetOverviewEntriesFromXml(Trace.OverviewXml);
                    //    //ScanTraceOverviewGrid.Rows.Clear();
                    //    foreach (Dictionary<string, string> Entry in OverviewEntries)
                    //    {
                    //        TraceViewer.ScanTraceOverviewGrid.Rows.Add(new object[] { false, Entry["id"], Entry["log_id"], Entry["payload"], Entry["code"], Entry["length"], Entry["mime"], Entry["time"], Entry["signature"] });
                    //    }
                    //    Viewers.Add(TraceViewer);
                    //}
                    //catch
                    //{
                    //    //Probaly an entry from the log of an older version
                    //}
                }
                foreach (LogTraceViewer Viewer in Viewers)
                {
                    Viewer.Show();
                }
                if (Viewers.Count > 1)
                {
                    MessageBox.Show(string.Format("{0} traces entries matched this finding, so {0} windows have been opened", Viewers.Count));
                }
                else if (Viewers.Count == 0)
                {
                    MessageBox.Show("No trace entries matching this finding could be find. Please look for the associated trace manually in the 'Scan Trace' section of the 'Automated Scanning' section.");
                }
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to load logs associated with Finding", Exp);
            }
        }

        private void TraceMsgRTB_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            MessageBox.Show(e.LinkText);
        }

        private void generateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IronUI.IsReportGenerationWizardOpen())
            {
                IronUI.RGW.Activate();
            }
            else
            {
                IronUI.RGW = new ReportGenerationWizard();
                TreeNode TopNode = IronUI.RGW.FindingsTree.Nodes.Add("All", "All");
                TopNode.Checked = true;
                CopyTree(IronTree.TopNode, TopNode);
                IronUI.RGW.Show();
            }            
        }

        void CopyTree(TreeNode FromNode, TreeNode ToNode)
        {
            if (FromNode.Level == 1 && FromNode.Index > 2) return;
            foreach (TreeNode Node in FromNode.Nodes)
            {
                if (Node.Level == 1 && Node.Index > 2) continue;
                TreeNode NewNode = ToNode.Nodes.Add(Node.Name, Node.Name);
                NewNode.Checked = true;
                if (Node.Nodes.Count > 0)
                {
                    CopyTree(Node, NewNode);
                }
            }
        }
    }
}

