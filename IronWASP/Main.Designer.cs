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
// along with IronWASP.  If not, see http://www.gnu.org/licenses/.
//

namespace IronWASP
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.split_main = new System.Windows.Forms.SplitContainer();
            this.IronTree = new System.Windows.Forms.TreeView();
            this.IronTreeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ScanBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.main_tab = new System.Windows.Forms.TabControl();
            this.mt_console = new System.Windows.Forms.TabPage();
            this.ScanJobsCompletedLbl = new System.Windows.Forms.Label();
            this.ScanJobsCreatedLbl = new System.Windows.Forms.Label();
            this.CrawlerRequestsLbl = new System.Windows.Forms.Label();
            this.ConsoleStatusTB = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.ConsoleStartScanBtn = new System.Windows.Forms.Button();
            this.InteractiveScanModeRB = new System.Windows.Forms.RadioButton();
            this.ConfiguredScanModeRB = new System.Windows.Forms.RadioButton();
            this.OptimalScanModeRB = new System.Windows.Forms.RadioButton();
            this.label28 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.ConsoleScanUrlTB = new System.Windows.Forms.TextBox();
            this.ConsoleRTB = new System.Windows.Forms.RichTextBox();
            this.mt_auto = new System.Windows.Forms.TabPage();
            this.ASMainTabs = new System.Windows.Forms.TabControl();
            this.ASConfigureTab = new System.Windows.Forms.TabPage();
            this.ASConfigureSplit = new System.Windows.Forms.SplitContainer();
            this.ASScanPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASSessionPluginsCombo = new System.Windows.Forms.ComboBox();
            this.ASStartScanBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfigureScanRequestSSLCB = new System.Windows.Forms.CheckBox();
            this.ASExceptionTB = new System.Windows.Forms.TextBox();
            this.ASRequestTabs = new System.Windows.Forms.TabControl();
            this.ASRequestFullTab = new System.Windows.Forms.TabPage();
            this.ASRequestScanFullTabs = new System.Windows.Forms.TabControl();
            this.tabPage20 = new System.Windows.Forms.TabPage();
            this.ASRequestRawHeadersIDV = new IronDataView.IronDataView();
            this.tabPage21 = new System.Windows.Forms.TabPage();
            this.ASRequestRawBodyIDV = new IronDataView.IronDataView();
            this.ASRequestScanHeadersCB = new System.Windows.Forms.CheckBox();
            this.ASRequestScanCookieCB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ASRequestScanBodyCB = new System.Windows.Forms.CheckBox();
            this.ASRequestScanAllCB = new System.Windows.Forms.CheckBox();
            this.ASRequestScanQueryCB = new System.Windows.Forms.CheckBox();
            this.ASRequestScanURLCB = new System.Windows.Forms.CheckBox();
            this.ASRequestURLTab = new System.Windows.Forms.TabPage();
            this.ASRequestScanURLGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestURLSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestURLPositionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestURLValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestQueryTab = new System.Windows.Forms.TabPage();
            this.ASRequestScanQueryGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestQuerySelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestQueryNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestQueryValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestBodyTab = new System.Windows.Forms.TabPage();
            this.ASRequestBodyTabSplit = new System.Windows.Forms.SplitContainer();
            this.ConfigureScanRequestFormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestBodyDataFormatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigureScanRequestFormatPluginsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConfigureScanRequestDeSerObjectToXmlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ASRequestScanBodyTabs = new System.Windows.Forms.TabControl();
            this.ASRequestScanBodyGridTab = new System.Windows.Forms.TabPage();
            this.ConfigureScanRequestBodyGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestBodySelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestBodyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestBodyValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestScanBodyXMLTab = new System.Windows.Forms.TabPage();
            this.ConfigureScanRequestFormatXMLTB = new System.Windows.Forms.TextBox();
            this.ASRequestCookieTab = new System.Windows.Forms.TabPage();
            this.ASRequestScanCookieGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestCookieSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestCookieNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestCookieValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestHeadersTab = new System.Windows.Forms.TabPage();
            this.ASRequestScanHeadersGrid = new System.Windows.Forms.DataGridView();
            this.ASRequestHeadersSelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ASRequestHeadersNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASRequestHeadersValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanStatusLbl = new System.Windows.Forms.Label();
            this.ScanIDLbl = new System.Windows.Forms.Label();
            this.ASQueueGrid = new System.Windows.Forms.DataGridView();
            this.ASQueueGridScanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASQueueGridStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASQueueGridMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASQueueGridURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanQueueMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StartAllStoppedAndAbortedScansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopAllScansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopThisScanJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ASTraceTab = new System.Windows.Forms.TabPage();
            this.ScanTraceBaseSplit = new System.Windows.Forms.SplitContainer();
            this.ScanTraceGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanTraceMsgRTB = new System.Windows.Forms.RichTextBox();
            this.mt_manual = new System.Windows.Forms.TabPage();
            this.MTTabs = new System.Windows.Forms.TabControl();
            this.MTTestTP = new System.Windows.Forms.TabPage();
            this.MTBaseSplit = new System.Windows.Forms.SplitContainer();
            this.NextTestLog = new System.Windows.Forms.Button();
            this.PreviousTestLog = new System.Windows.Forms.Button();
            this.MTReqResTabs = new System.Windows.Forms.TabControl();
            this.MTRequestTab = new System.Windows.Forms.TabPage();
            this.MTRequestTabs = new System.Windows.Forms.TabControl();
            this.MTRequestHeadersTP = new System.Windows.Forms.TabPage();
            this.MTRequestHeadersIDV = new IronDataView.IronDataView();
            this.MTRequestBodyTP = new System.Windows.Forms.TabPage();
            this.MTRequestBodyIDV = new IronDataView.IronDataView();
            this.MTRequestParametersTP = new System.Windows.Forms.TabPage();
            this.MTRequestParametersTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MTRequestParametersQueryGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MTRequestParametersBodyGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.MTRequestParametersCookieGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.MTRequestParametersHeadersGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTRequestFormatPluginsTP = new System.Windows.Forms.TabPage();
            this.MTRequestFormatSplit = new System.Windows.Forms.SplitContainer();
            this.MTRequestFormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTRequestFormatPluginsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MTRequestDeSerObjectToXmlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MTRequestSerXmlToObjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MTRequestFormatXMLTB = new System.Windows.Forms.TextBox();
            this.MTResponseTab = new System.Windows.Forms.TabPage();
            this.MTResponseTabs = new System.Windows.Forms.TabControl();
            this.MTResponseHeadersTP = new System.Windows.Forms.TabPage();
            this.MTResponseHeadersIDV = new IronDataView.IronDataView();
            this.MTResponseBodyTP = new System.Windows.Forms.TabPage();
            this.MTResponseBodyIDV = new IronDataView.IronDataView();
            this.MTResponseReflectionTP = new System.Windows.Forms.TabPage();
            this.MTReflectionsRTB = new System.Windows.Forms.RichTextBox();
            this.MTClearFieldsBtn = new System.Windows.Forms.Button();
            this.MTExceptionTB = new System.Windows.Forms.TextBox();
            this.MTStoredRequestBtn = new System.Windows.Forms.Button();
            this.MTScriptedSendBtn = new System.Windows.Forms.Button();
            this.MTIsSSLCB = new System.Windows.Forms.CheckBox();
            this.MTSendBtn = new System.Windows.Forms.Button();
            this.TestIDLbl = new System.Windows.Forms.Label();
            this.TestGroupLogGrid = new System.Windows.Forms.DataGridView();
            this.TestGroupLogGridForID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestGroupLogGridForHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestGroupLogGridForMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestGroupLogGridForURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestGroupLogGridForSSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TestGroupLogGridForCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestGroupLogGridForLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestGroupLogGridForMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestGroupLogGridForSetCookie = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectForManualTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreenGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BlueGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrayGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrownGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectForAutomatedScanningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectResponseForJavaScriptTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestBrownGroupBtn = new System.Windows.Forms.Button();
            this.TestGrayGroupBtn = new System.Windows.Forms.Button();
            this.TestBlueGroupBtn = new System.Windows.Forms.Button();
            this.TestGreenGroupBtn = new System.Windows.Forms.Button();
            this.TestRedGroupBtn = new System.Windows.Forms.Button();
            this.MTScriptingTP = new System.Windows.Forms.TabPage();
            this.ScriptingShellSplit = new System.Windows.Forms.SplitContainer();
            this.ClearShellDisplayBtn = new System.Windows.Forms.Button();
            this.MultiLineShellExecuteBtn = new System.Windows.Forms.Button();
            this.InteractiveShellCtrlCBtn = new System.Windows.Forms.Button();
            this.ScriptingShellTabs = new System.Windows.Forms.TabControl();
            this.InteractiveShellTP = new System.Windows.Forms.TabPage();
            this.InteractiveShellPromptBox = new System.Windows.Forms.TextBox();
            this.InteractiveShellOut = new System.Windows.Forms.TextBox();
            this.InteractiveShellIn = new System.Windows.Forms.TextBox();
            this.MultiLineShellTP = new System.Windows.Forms.TabPage();
            this.MultiLineShellInTE = new ICSharpCode.TextEditor.TextEditorControl();
            this.ScriptedSendTP = new System.Windows.Forms.TabPage();
            this.CustomSendErrorTB = new System.Windows.Forms.TextBox();
            this.CustomSendTE = new ICSharpCode.TextEditor.TextEditorControl();
            this.CustomSendBottomRtb = new System.Windows.Forms.RichTextBox();
            this.CustomSendTopRtb = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomSendActivateCB = new System.Windows.Forms.CheckBox();
            this.CustomSendRubyRB = new System.Windows.Forms.RadioButton();
            this.CustomSendPythonRB = new System.Windows.Forms.RadioButton();
            this.InteractiveShellRubyRB = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.InteractiveShellPythonRB = new System.Windows.Forms.RadioButton();
            this.ScriptingShellAPISplit = new System.Windows.Forms.SplitContainer();
            this.ScriptingShellAPITreeTabs = new System.Windows.Forms.TabControl();
            this.ScriptingShellAPITreePythonTab = new System.Windows.Forms.TabPage();
            this.ScriptingShellPythonAPITree = new System.Windows.Forms.TreeView();
            this.ScriptingShellAPITreeRubyTab = new System.Windows.Forms.TabPage();
            this.ScriptingShellRubyAPITree = new System.Windows.Forms.TreeView();
            this.ShellAPIDetailsRTB = new System.Windows.Forms.RichTextBox();
            this.MTJavaScriptTaintTP = new System.Windows.Forms.TabPage();
            this.JSTaintShowSourceToSinkCB = new System.Windows.Forms.CheckBox();
            this.JSTaintShowLinesLbl = new System.Windows.Forms.Label();
            this.JSTaintShowSinkCB = new System.Windows.Forms.CheckBox();
            this.JSTaintShowSourceCB = new System.Windows.Forms.CheckBox();
            this.JSTaintShowCleanCB = new System.Windows.Forms.CheckBox();
            this.JSTaintConfigPanel = new System.Windows.Forms.Panel();
            this.TaintTraceClearTaintConfigBtn = new System.Windows.Forms.Button();
            this.JSTaintConfigGrid = new System.Windows.Forms.DataGridView();
            this.JSTaintDefaultSourceObjectsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTaintDefaultSinkObjectsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTaintDefaultArgumentAssignedASourceMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTaintDefaultArgumentAssignedToSinkMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTaintDefaultSourceReturningMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTaintDefaultSinkReturningMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTaintDefaultArgumentReturningMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaintTraceResetTaintConfigBtn = new System.Windows.Forms.Button();
            this.JSTaintContinueBtn = new System.Windows.Forms.Button();
            this.JSTaintConfigShowHideBtn = new System.Windows.Forms.Button();
            this.PauseAtTaintCB = new System.Windows.Forms.CheckBox();
            this.JSTaintStatusTB = new System.Windows.Forms.TextBox();
            this.TaintTraceResultSinkLegendTB = new System.Windows.Forms.TextBox();
            this.JSTaintTabs = new System.Windows.Forms.TabControl();
            this.JSTaintInputTab = new System.Windows.Forms.TabPage();
            this.JSTaintTraceInRTB = new System.Windows.Forms.RichTextBox();
            this.TaintTraceMsgTB = new System.Windows.Forms.TextBox();
            this.JSTaintResultTab = new System.Windows.Forms.TabPage();
            this.JSTaintResultSplit = new System.Windows.Forms.SplitContainer();
            this.JSTaintResultGrid = new System.Windows.Forms.DataGridView();
            this.JSTaintResultLineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTaintResultCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JSTainTraceEditMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddSourceTaintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSinkTaintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveSourceTaintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveSinkTaintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyLineTaintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JSTaintReasonsRTB = new System.Windows.Forms.RichTextBox();
            this.JSTaintTraceControlBtn = new System.Windows.Forms.Button();
            this.TaintTraceResultSourceToSinkLegendTB = new System.Windows.Forms.TextBox();
            this.TaintTraceResultSourceLegendTB = new System.Windows.Forms.TextBox();
            this.TaintTraceResultSourcePlusSinkLegendTB = new System.Windows.Forms.TextBox();
            this.mt_proxy = new System.Windows.Forms.TabPage();
            this.ProxyOptionsBtn = new System.Windows.Forms.Button();
            this.ConfigLoopBackOnlyCB = new System.Windows.Forms.CheckBox();
            this.ViewProxyLogLink = new System.Windows.Forms.LinkLabel();
            this.ConfigProxyListenPortTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ConfigSetAsSystemProxyCB = new System.Windows.Forms.CheckBox();
            this.ConfigProxyRunBtn = new System.Windows.Forms.Button();
            this.ProxyDropBtn = new System.Windows.Forms.Button();
            this.ProxySendBtn = new System.Windows.Forms.Button();
            this.ProxyExceptionTB = new System.Windows.Forms.TextBox();
            this.InterceptResponseCB = new System.Windows.Forms.CheckBox();
            this.ProxyInterceptTabs = new System.Windows.Forms.TabControl();
            this.ProxyInterceptRequestTab = new System.Windows.Forms.TabPage();
            this.ProxyInterceptRequestTabs = new System.Windows.Forms.TabControl();
            this.ProxyInterceptRequestHeadersTab = new System.Windows.Forms.TabPage();
            this.ProxyRequestHeadersIDV = new IronDataView.IronDataView();
            this.ProxyInterceptRequestBodyTab = new System.Windows.Forms.TabPage();
            this.ProxyRequestBodyIDV = new IronDataView.IronDataView();
            this.ProxyInterceptRequestParametersTab = new System.Windows.Forms.TabPage();
            this.ProxyRequestParametersTabs = new System.Windows.Forms.TabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.ProxyRequestParametersQueryGrid = new System.Windows.Forms.DataGridView();
            this.ProxyRequestQueryParametersNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyRequestQueryParametersValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.ProxyRequestParametersBodyGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.ProxyRequestParametersCookieGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.ProxyRequestParametersHeadersGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyInterceptRequestFormatPluginsTab = new System.Windows.Forms.TabPage();
            this.ProxyRequestFormatSplit = new System.Windows.Forms.SplitContainer();
            this.ProxyRequestFormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyRequestFormatPluginsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ProxyRequestDeSerObjectToXmlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProxyRequestSerXmlToObjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProxyRequestFormatXMLTB = new System.Windows.Forms.TextBox();
            this.ProxyInterceptResponseTab = new System.Windows.Forms.TabPage();
            this.ProxyInterceptResponseTabs = new System.Windows.Forms.TabControl();
            this.ProxyInterceptResponseHeadersTab = new System.Windows.Forms.TabPage();
            this.ProxyResponseHeadersIDV = new IronDataView.IronDataView();
            this.ProxyInterceptResponseBodyTab = new System.Windows.Forms.TabPage();
            this.ProxyResponseBodyIDV = new IronDataView.IronDataView();
            this.ProxyInterceptResponseFormatPluginsTab = new System.Windows.Forms.TabPage();
            this.ProxyResponseFormatSplit = new System.Windows.Forms.SplitContainer();
            this.ProxyResponseFormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyResponseFormatPluginsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ProxyResponseDeSerObjectToXmlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProxyResponseSerXmlToObjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProxyResponseFormatXMLTB = new System.Windows.Forms.TextBox();
            this.InterceptRequestCB = new System.Windows.Forms.CheckBox();
            this.mt_logs = new System.Windows.Forms.TabPage();
            this.LogBaseSplit = new System.Windows.Forms.SplitContainer();
            this.LogOptionsBtn = new System.Windows.Forms.Button();
            this.LogIDLbl = new System.Windows.Forms.Label();
            this.LogSourceLbl = new System.Windows.Forms.Label();
            this.LogStatusTB = new System.Windows.Forms.TextBox();
            this.ShowLogGridBtn = new System.Windows.Forms.Button();
            this.NextLogBtn = new System.Windows.Forms.Button();
            this.ProxyShowOriginalResponseCB = new System.Windows.Forms.CheckBox();
            this.ProxyShowOriginalRequestCB = new System.Windows.Forms.CheckBox();
            this.PreviousLogBtn = new System.Windows.Forms.Button();
            this.LogDisplayTabs = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.LogRequestTabs = new System.Windows.Forms.TabControl();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.LogRequestHeadersIDV = new IronDataView.IronDataView();
            this.tabPage22 = new System.Windows.Forms.TabPage();
            this.LogRequestBodyIDV = new IronDataView.IronDataView();
            this.tabPage23 = new System.Windows.Forms.TabPage();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage24 = new System.Windows.Forms.TabPage();
            this.LogRequestParametersQueryGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage25 = new System.Windows.Forms.TabPage();
            this.LogRequestParametersBodyGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage26 = new System.Windows.Forms.TabPage();
            this.LogRequestParametersCookieGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage27 = new System.Windows.Forms.TabPage();
            this.LogRequestParametersHeadersGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage28 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LogRequestFormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogRequestFormatXMLTB = new System.Windows.Forms.TextBox();
            this.tabPage29 = new System.Windows.Forms.TabPage();
            this.LogResponseTabs = new System.Windows.Forms.TabControl();
            this.tabPage30 = new System.Windows.Forms.TabPage();
            this.LogResponseHeadersIDV = new IronDataView.IronDataView();
            this.tabPage31 = new System.Windows.Forms.TabPage();
            this.LogResponseBodyIDV = new IronDataView.IronDataView();
            this.tabPage39 = new System.Windows.Forms.TabPage();
            this.LogReflectionRTB = new System.Windows.Forms.RichTextBox();
            this.tabPage32 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.LogResponseFormatPluginsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogResponseFormatXMLTB = new System.Windows.Forms.TextBox();
            this.LogTabs = new System.Windows.Forms.TabControl();
            this.ProxyLogTab = new System.Windows.Forms.TabPage();
            this.ProxyLogGrid = new System.Windows.Forms.DataGridView();
            this.ProxyLogGridColumnForID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForHostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForSSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProxyLogGridColumnForParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProxyLogGridColumnForSetCookie = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProxyLogGridColumnForEdited = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProxyLogGridColumnForNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogTab = new System.Windows.Forms.TabPage();
            this.ScanLogGrid = new System.Windows.Forms.DataGridView();
            this.ScanLogGridColumnForID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForScanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForSSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ScanLogGridColumnForParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanLogGridColumnForSetCookie = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TestLogTab = new System.Windows.Forms.TabPage();
            this.TestLogGrid = new System.Windows.Forms.DataGridView();
            this.MTLogGridColumnForID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForHostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForSSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MTLogGridColumnForParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTLogGridColumnForSetCookie = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ShellLogTab = new System.Windows.Forms.TabPage();
            this.ShellLogGrid = new System.Windows.Forms.DataGridView();
            this.ScriptingLogGridColumnForID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForHostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForSSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ScriptingLogGridColumnForParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScriptingLogGridColumnForSetCookie = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProbeLogTab = new System.Windows.Forms.TabPage();
            this.ProbeLogGrid = new System.Windows.Forms.DataGridView();
            this.ProbeLogGridColumnForID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForHostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForSSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProbeLogGridColumnForParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProbeLogGridColumnForSetCookie = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SiteMapLogTab = new System.Windows.Forms.TabPage();
            this.SiteMapLogGrid = new System.Windows.Forms.DataGridView();
            this.SiteMapLogGridColumnForID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForSSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SiteMapLogGridColumnForParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForMIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteMapLogGridColumnForSetCookie = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mt_results = new System.Windows.Forms.TabPage();
            this.ResultsTabMainSplit = new System.Windows.Forms.SplitContainer();
            this.ResultsTopSplit = new System.Windows.Forms.SplitContainer();
            this.ResultsDisplayRTB = new System.Windows.Forms.RichTextBox();
            this.ResultsTriggersMainSplit = new System.Windows.Forms.SplitContainer();
            this.ResultsTriggersGrid = new System.Windows.Forms.DataGridView();
            this.ResultsTriggerGridNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultsTriggersSplit = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.ResultsRequestTriggerTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ResultsResponseTriggerTB = new System.Windows.Forms.TextBox();
            this.ResultsDisplayTabs = new System.Windows.Forms.TabControl();
            this.ResultsRequestTab = new System.Windows.Forms.TabPage();
            this.ResultsRequestIDV = new IronDataView.IronDataView();
            this.ResultsResponseTab = new System.Windows.Forms.TabPage();
            this.ResultsResponseIDV = new IronDataView.IronDataView();
            this.mt_plugins = new System.Windows.Forms.TabPage();
            this.PluginsMainSplit = new System.Windows.Forms.SplitContainer();
            this.PluginTree = new System.Windows.Forms.TreeView();
            this.PluginTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectedPluginDeactivateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectedPluginReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllPluginsRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllPluginsANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passivePluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PassivePluginsRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PassivePluginsANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activePluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ActivePluginsRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ActivePluginsANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FormatPluginsRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FormatPluginsANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionPluginsRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionPluginsANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PluginEditorSplit = new System.Windows.Forms.SplitContainer();
            this.PluginsCentreSplit = new System.Windows.Forms.SplitContainer();
            this.PluginDetailsRTB = new System.Windows.Forms.RichTextBox();
            this.PluginEditorInTE = new ICSharpCode.TextEditor.TextEditorControl();
            this.PluginEditorAPISplit = new System.Windows.Forms.SplitContainer();
            this.PluginEditorAPITreeTabs = new System.Windows.Forms.TabControl();
            this.PluginEditorPythonAPITreeTab = new System.Windows.Forms.TabPage();
            this.PluginEditorPythonAPITree = new System.Windows.Forms.TreeView();
            this.PluginEditorRubyAPITreeTab = new System.Windows.Forms.TabPage();
            this.PluginEditorRubyAPITree = new System.Windows.Forms.TreeView();
            this.PluginEditorAPIDetailsRTB = new System.Windows.Forms.RichTextBox();
            this.mt_trace = new System.Windows.Forms.TabPage();
            this.TraceBaseSplit = new System.Windows.Forms.SplitContainer();
            this.TraceGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TraceMsgRTB = new System.Windows.Forms.RichTextBox();
            this.ConfigPanel = new System.Windows.Forms.Panel();
            this.ConfigPanelTabs = new System.Windows.Forms.TabControl();
            this.ConfigIronProxyTab = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ConfigUpstreamProxyPortTB = new System.Windows.Forms.TextBox();
            this.ConfigUseUpstreamProxyCB = new System.Windows.Forms.CheckBox();
            this.ConfigUpstreamProxyIPTB = new System.Windows.Forms.TextBox();
            this.ConfigInterceptRulesTab = new System.Windows.Forms.TabPage();
            this.ConfigRuleRequestOnResponseRulesCB = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.ConfigRuleKeywordInResponseGB = new System.Windows.Forms.GroupBox();
            this.ConfigRuleKeywordInResponseCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleKeywordInResponsePlusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleKeywordInResponseMinusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleKeywordInResponsePlusRB = new System.Windows.Forms.RadioButton();
            this.ConfigRuleKeywordInResponseMinusRB = new System.Windows.Forms.RadioButton();
            this.ConfigRuleContentJSONCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleKeywordInRequestGB = new System.Windows.Forms.GroupBox();
            this.ConfigRuleKeywordInRequestCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleKeywordInRequestPlusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleKeywordInRequestMinusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleKeywordInRequestPlusRB = new System.Windows.Forms.RadioButton();
            this.ConfigRuleKeywordInRequestMinusRB = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ConfigRuleHostNamesCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleHostNamesPlusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleHostNamesMinusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleHostNamesPlusRB = new System.Windows.Forms.RadioButton();
            this.ConfigRuleHostNamesMinusRB = new System.Windows.Forms.RadioButton();
            this.ConfigRuleApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConfigRuleFileExtensionsCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleFileExtensionsPlusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleFileExtensionsMinusTB = new System.Windows.Forms.TextBox();
            this.ConfigRuleFileExtensionsPlusRB = new System.Windows.Forms.RadioButton();
            this.ConfigRuleFileExtensionsMinusRB = new System.Windows.Forms.RadioButton();
            this.ConfigRuleCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.ConfigRuleContentCSSCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleCode5xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleContentJSCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleCode500CB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleContentImgCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleCode4xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleContentOtherBinaryCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleCode403CB = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ConfigRuleCode3xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleContentHTMLCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleCode301_2CB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleGETMethodCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleCode2xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigRulePOSTMethodCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleCode200CB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleOtherMethodsCB = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ConfigRuleContentOtherTextCB = new System.Windows.Forms.CheckBox();
            this.ConfigRuleContentXMLCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRulesTab = new System.Windows.Forms.TabPage();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.ConfigDisplayRuleApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigDisplayRuleCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigDisplayRuleContentJSONCB = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ConfigDisplayRuleHostNamesCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleHostNamesPlusTB = new System.Windows.Forms.TextBox();
            this.ConfigDisplayRuleHostNamesMinusTB = new System.Windows.Forms.TextBox();
            this.ConfigDisplayRuleHostNamesPlusRB = new System.Windows.Forms.RadioButton();
            this.ConfigDisplayRuleHostNamesMinusRB = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ConfigDisplayRuleFileExtensionsCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleFileExtensionsPlusTB = new System.Windows.Forms.TextBox();
            this.ConfigDisplayRuleFileExtensionsMinusTB = new System.Windows.Forms.TextBox();
            this.ConfigDisplayRuleFileExtensionsPlusRB = new System.Windows.Forms.RadioButton();
            this.ConfigDisplayRuleFileExtensionsMinusRB = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.ConfigDisplayRuleContentCSSCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleCode5xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleContentJSCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleCode500CB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleContentImgCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleCode4xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleContentOtherBinaryCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleCode403CB = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.ConfigDisplayRuleCode3xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleContentHTMLCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleCode301_2CB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleGETMethodCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleCode2xxCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRulePOSTMethodCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleCode200CB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleOtherMethodsCB = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.ConfigDisplayRuleContentOtherTextCB = new System.Windows.Forms.CheckBox();
            this.ConfigDisplayRuleContentXMLCB = new System.Windows.Forms.CheckBox();
            this.ConfigScriptingTab = new System.Windows.Forms.TabPage();
            this.ConfigScriptBaseSplit = new System.Windows.Forms.SplitContainer();
            this.label14 = new System.Windows.Forms.Label();
            this.ConfigScriptPathApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigScriptPathCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigScriptPathSplit = new System.Windows.Forms.SplitContainer();
            this.label15 = new System.Windows.Forms.Label();
            this.ConfigScriptPyPathsTB = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ConfigScriptRbPathsTB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ConfigScriptCommandApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigScriptCommandCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigScriptCommandSplit = new System.Windows.Forms.SplitContainer();
            this.label17 = new System.Windows.Forms.Label();
            this.ConfigScriptPyCommandsTB = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ConfigScriptRbCommandsTB = new System.Windows.Forms.TextBox();
            this.ConfigHTTPAPITab = new System.Windows.Forms.TabPage();
            this.ConfigHTTPAPIBaseSplit = new System.Windows.Forms.SplitContainer();
            this.ConfigRequestTypesTB = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.ConfigRequestTypesCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigRequestTypesApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigResponseTypesTB = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.ConfigResponseTypesApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigResponseTypesCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigTaintConfigTab = new System.Windows.Forms.TabPage();
            this.ConfigJSTaintConfigCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigJSTaintConfigApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigDefaultJSTaintConfigGrid = new System.Windows.Forms.DataGridView();
            this.ConfigDefaultSourceObjectsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigDefaultSinkObjectsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigDefaultArgumentAssignedASourceMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigDefaultArgumentAssignedToSinkMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigDefaultSourceReturningMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigDefaultSinkReturningMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigDefaultArgumentReturningMethodsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigScannerTab = new System.Windows.Forms.TabPage();
            this.ConfigCrawlerUserAgentTB = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.ConfigScannerSettingsCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigScannerSettingsApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigCrawlerThreadMaxCountLbl = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.ConfigCrawlerThreadMaxCountTB = new System.Windows.Forms.TrackBar();
            this.ConfigScannerThreadMaxCountLbl = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.ConfigScannerThreadMaxCountTB = new System.Windows.Forms.TrackBar();
            this.ConfigPassiveAnalysisTab = new System.Windows.Forms.TabPage();
            this.ConfigPassiveAnalysisOnProbeTrafficCB = new System.Windows.Forms.CheckBox();
            this.ConfigPassiveAnalysisOnScanTrafficCB = new System.Windows.Forms.CheckBox();
            this.ConfigPassiveAnalysisOnTestTrafficCB = new System.Windows.Forms.CheckBox();
            this.ConfigPassiveAnalysisOnShellTrafficCB = new System.Windows.Forms.CheckBox();
            this.ConfigPassiveAnalysisOnProxyTrafficCB = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.ConfigPassiveAnalysisSettingsCancelChangesLL = new System.Windows.Forms.LinkLabel();
            this.ConfigPassiveAnalysisSettingsApplyChangesLL = new System.Windows.Forms.LinkLabel();
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportBurpLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EncodeDecodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DiffTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PluginEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.BurpLogOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.ConfigViewHideLL = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.split_main.Panel1.SuspendLayout();
            this.split_main.Panel2.SuspendLayout();
            this.split_main.SuspendLayout();
            this.IronTreeMenuStrip.SuspendLayout();
            this.main_tab.SuspendLayout();
            this.mt_console.SuspendLayout();
            this.mt_auto.SuspendLayout();
            this.ASMainTabs.SuspendLayout();
            this.ASConfigureTab.SuspendLayout();
            this.ASConfigureSplit.Panel1.SuspendLayout();
            this.ASConfigureSplit.Panel2.SuspendLayout();
            this.ASConfigureSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ASScanPluginsGrid)).BeginInit();
            this.ASRequestTabs.SuspendLayout();
            this.ASRequestFullTab.SuspendLayout();
            this.ASRequestScanFullTabs.SuspendLayout();
            this.tabPage20.SuspendLayout();
            this.tabPage21.SuspendLayout();
            this.ASRequestURLTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanURLGrid)).BeginInit();
            this.ASRequestQueryTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanQueryGrid)).BeginInit();
            this.ASRequestBodyTab.SuspendLayout();
            this.ASRequestBodyTabSplit.Panel1.SuspendLayout();
            this.ASRequestBodyTabSplit.Panel2.SuspendLayout();
            this.ASRequestBodyTabSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigureScanRequestFormatPluginsGrid)).BeginInit();
            this.ConfigureScanRequestFormatPluginsMenu.SuspendLayout();
            this.ASRequestScanBodyTabs.SuspendLayout();
            this.ASRequestScanBodyGridTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigureScanRequestBodyGrid)).BeginInit();
            this.ASRequestScanBodyXMLTab.SuspendLayout();
            this.ASRequestCookieTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanCookieGrid)).BeginInit();
            this.ASRequestHeadersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanHeadersGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASQueueGrid)).BeginInit();
            this.ScanQueueMenu.SuspendLayout();
            this.ASTraceTab.SuspendLayout();
            this.ScanTraceBaseSplit.Panel1.SuspendLayout();
            this.ScanTraceBaseSplit.Panel2.SuspendLayout();
            this.ScanTraceBaseSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanTraceGrid)).BeginInit();
            this.mt_manual.SuspendLayout();
            this.MTTabs.SuspendLayout();
            this.MTTestTP.SuspendLayout();
            this.MTBaseSplit.Panel1.SuspendLayout();
            this.MTBaseSplit.Panel2.SuspendLayout();
            this.MTBaseSplit.SuspendLayout();
            this.MTReqResTabs.SuspendLayout();
            this.MTRequestTab.SuspendLayout();
            this.MTRequestTabs.SuspendLayout();
            this.MTRequestHeadersTP.SuspendLayout();
            this.MTRequestBodyTP.SuspendLayout();
            this.MTRequestParametersTP.SuspendLayout();
            this.MTRequestParametersTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersQueryGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersBodyGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersCookieGrid)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersHeadersGrid)).BeginInit();
            this.MTRequestFormatPluginsTP.SuspendLayout();
            this.MTRequestFormatSplit.Panel1.SuspendLayout();
            this.MTRequestFormatSplit.Panel2.SuspendLayout();
            this.MTRequestFormatSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestFormatPluginsGrid)).BeginInit();
            this.MTRequestFormatPluginsMenu.SuspendLayout();
            this.MTResponseTab.SuspendLayout();
            this.MTResponseTabs.SuspendLayout();
            this.MTResponseHeadersTP.SuspendLayout();
            this.MTResponseBodyTP.SuspendLayout();
            this.MTResponseReflectionTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestGroupLogGrid)).BeginInit();
            this.LogMenu.SuspendLayout();
            this.MTScriptingTP.SuspendLayout();
            this.ScriptingShellSplit.Panel1.SuspendLayout();
            this.ScriptingShellSplit.Panel2.SuspendLayout();
            this.ScriptingShellSplit.SuspendLayout();
            this.ScriptingShellTabs.SuspendLayout();
            this.InteractiveShellTP.SuspendLayout();
            this.MultiLineShellTP.SuspendLayout();
            this.ScriptedSendTP.SuspendLayout();
            this.ScriptingShellAPISplit.Panel1.SuspendLayout();
            this.ScriptingShellAPISplit.Panel2.SuspendLayout();
            this.ScriptingShellAPISplit.SuspendLayout();
            this.ScriptingShellAPITreeTabs.SuspendLayout();
            this.ScriptingShellAPITreePythonTab.SuspendLayout();
            this.ScriptingShellAPITreeRubyTab.SuspendLayout();
            this.MTJavaScriptTaintTP.SuspendLayout();
            this.JSTaintConfigPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JSTaintConfigGrid)).BeginInit();
            this.JSTaintTabs.SuspendLayout();
            this.JSTaintInputTab.SuspendLayout();
            this.JSTaintResultTab.SuspendLayout();
            this.JSTaintResultSplit.Panel1.SuspendLayout();
            this.JSTaintResultSplit.Panel2.SuspendLayout();
            this.JSTaintResultSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JSTaintResultGrid)).BeginInit();
            this.JSTainTraceEditMenu.SuspendLayout();
            this.mt_proxy.SuspendLayout();
            this.ProxyInterceptTabs.SuspendLayout();
            this.ProxyInterceptRequestTab.SuspendLayout();
            this.ProxyInterceptRequestTabs.SuspendLayout();
            this.ProxyInterceptRequestHeadersTab.SuspendLayout();
            this.ProxyInterceptRequestBodyTab.SuspendLayout();
            this.ProxyInterceptRequestParametersTab.SuspendLayout();
            this.ProxyRequestParametersTabs.SuspendLayout();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersQueryGrid)).BeginInit();
            this.tabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersBodyGrid)).BeginInit();
            this.tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersCookieGrid)).BeginInit();
            this.tabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersHeadersGrid)).BeginInit();
            this.ProxyInterceptRequestFormatPluginsTab.SuspendLayout();
            this.ProxyRequestFormatSplit.Panel1.SuspendLayout();
            this.ProxyRequestFormatSplit.Panel2.SuspendLayout();
            this.ProxyRequestFormatSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestFormatPluginsGrid)).BeginInit();
            this.ProxyRequestFormatPluginsMenu.SuspendLayout();
            this.ProxyInterceptResponseTab.SuspendLayout();
            this.ProxyInterceptResponseTabs.SuspendLayout();
            this.ProxyInterceptResponseHeadersTab.SuspendLayout();
            this.ProxyInterceptResponseBodyTab.SuspendLayout();
            this.ProxyInterceptResponseFormatPluginsTab.SuspendLayout();
            this.ProxyResponseFormatSplit.Panel1.SuspendLayout();
            this.ProxyResponseFormatSplit.Panel2.SuspendLayout();
            this.ProxyResponseFormatSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyResponseFormatPluginsGrid)).BeginInit();
            this.ProxyResponseFormatPluginsMenu.SuspendLayout();
            this.mt_logs.SuspendLayout();
            this.LogBaseSplit.Panel1.SuspendLayout();
            this.LogBaseSplit.Panel2.SuspendLayout();
            this.LogBaseSplit.SuspendLayout();
            this.LogDisplayTabs.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.LogRequestTabs.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage22.SuspendLayout();
            this.tabPage23.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersQueryGrid)).BeginInit();
            this.tabPage25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersBodyGrid)).BeginInit();
            this.tabPage26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersCookieGrid)).BeginInit();
            this.tabPage27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersHeadersGrid)).BeginInit();
            this.tabPage28.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestFormatPluginsGrid)).BeginInit();
            this.tabPage29.SuspendLayout();
            this.LogResponseTabs.SuspendLayout();
            this.tabPage30.SuspendLayout();
            this.tabPage31.SuspendLayout();
            this.tabPage39.SuspendLayout();
            this.tabPage32.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogResponseFormatPluginsGrid)).BeginInit();
            this.LogTabs.SuspendLayout();
            this.ProxyLogTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyLogGrid)).BeginInit();
            this.ScanLogTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanLogGrid)).BeginInit();
            this.TestLogTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestLogGrid)).BeginInit();
            this.ShellLogTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShellLogGrid)).BeginInit();
            this.ProbeLogTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProbeLogGrid)).BeginInit();
            this.SiteMapLogTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SiteMapLogGrid)).BeginInit();
            this.mt_results.SuspendLayout();
            this.ResultsTabMainSplit.Panel1.SuspendLayout();
            this.ResultsTabMainSplit.Panel2.SuspendLayout();
            this.ResultsTabMainSplit.SuspendLayout();
            this.ResultsTopSplit.Panel1.SuspendLayout();
            this.ResultsTopSplit.Panel2.SuspendLayout();
            this.ResultsTopSplit.SuspendLayout();
            this.ResultsTriggersMainSplit.Panel1.SuspendLayout();
            this.ResultsTriggersMainSplit.Panel2.SuspendLayout();
            this.ResultsTriggersMainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTriggersGrid)).BeginInit();
            this.ResultsTriggersSplit.Panel1.SuspendLayout();
            this.ResultsTriggersSplit.Panel2.SuspendLayout();
            this.ResultsTriggersSplit.SuspendLayout();
            this.ResultsDisplayTabs.SuspendLayout();
            this.ResultsRequestTab.SuspendLayout();
            this.ResultsResponseTab.SuspendLayout();
            this.mt_plugins.SuspendLayout();
            this.PluginsMainSplit.Panel1.SuspendLayout();
            this.PluginsMainSplit.Panel2.SuspendLayout();
            this.PluginsMainSplit.SuspendLayout();
            this.PluginTreeMenu.SuspendLayout();
            this.PluginEditorSplit.Panel1.SuspendLayout();
            this.PluginEditorSplit.Panel2.SuspendLayout();
            this.PluginEditorSplit.SuspendLayout();
            this.PluginsCentreSplit.Panel1.SuspendLayout();
            this.PluginsCentreSplit.Panel2.SuspendLayout();
            this.PluginsCentreSplit.SuspendLayout();
            this.PluginEditorAPISplit.Panel1.SuspendLayout();
            this.PluginEditorAPISplit.Panel2.SuspendLayout();
            this.PluginEditorAPISplit.SuspendLayout();
            this.PluginEditorAPITreeTabs.SuspendLayout();
            this.PluginEditorPythonAPITreeTab.SuspendLayout();
            this.PluginEditorRubyAPITreeTab.SuspendLayout();
            this.mt_trace.SuspendLayout();
            this.TraceBaseSplit.Panel1.SuspendLayout();
            this.TraceBaseSplit.Panel2.SuspendLayout();
            this.TraceBaseSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TraceGrid)).BeginInit();
            this.ConfigPanel.SuspendLayout();
            this.ConfigPanelTabs.SuspendLayout();
            this.ConfigIronProxyTab.SuspendLayout();
            this.ConfigInterceptRulesTab.SuspendLayout();
            this.ConfigRuleKeywordInResponseGB.SuspendLayout();
            this.ConfigRuleKeywordInRequestGB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ConfigDisplayRulesTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.ConfigScriptingTab.SuspendLayout();
            this.ConfigScriptBaseSplit.Panel1.SuspendLayout();
            this.ConfigScriptBaseSplit.Panel2.SuspendLayout();
            this.ConfigScriptBaseSplit.SuspendLayout();
            this.ConfigScriptPathSplit.Panel1.SuspendLayout();
            this.ConfigScriptPathSplit.Panel2.SuspendLayout();
            this.ConfigScriptPathSplit.SuspendLayout();
            this.ConfigScriptCommandSplit.Panel1.SuspendLayout();
            this.ConfigScriptCommandSplit.Panel2.SuspendLayout();
            this.ConfigScriptCommandSplit.SuspendLayout();
            this.ConfigHTTPAPITab.SuspendLayout();
            this.ConfigHTTPAPIBaseSplit.Panel1.SuspendLayout();
            this.ConfigHTTPAPIBaseSplit.Panel2.SuspendLayout();
            this.ConfigHTTPAPIBaseSplit.SuspendLayout();
            this.ConfigTaintConfigTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDefaultJSTaintConfigGrid)).BeginInit();
            this.ConfigScannerTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigCrawlerThreadMaxCountTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigScannerThreadMaxCountTB)).BeginInit();
            this.ConfigPassiveAnalysisTab.SuspendLayout();
            this.TopMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // split_main
            // 
            this.split_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.split_main.Location = new System.Drawing.Point(0, 23);
            this.split_main.Margin = new System.Windows.Forms.Padding(0);
            this.split_main.Name = "split_main";
            // 
            // split_main.Panel1
            // 
            this.split_main.Panel1.Controls.Add(this.IronTree);
            // 
            // split_main.Panel2
            // 
            this.split_main.Panel2.Controls.Add(this.main_tab);
            this.split_main.Size = new System.Drawing.Size(883, 538);
            this.split_main.SplitterDistance = 166;
            this.split_main.TabIndex = 0;
            // 
            // IronTree
            // 
            this.IronTree.BackColor = System.Drawing.Color.White;
            this.IronTree.ContextMenuStrip = this.IronTreeMenuStrip;
            this.IronTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IronTree.Location = new System.Drawing.Point(0, 0);
            this.IronTree.Margin = new System.Windows.Forms.Padding(0);
            this.IronTree.Name = "IronTree";
            this.IronTree.ShowRootLines = false;
            this.IronTree.Size = new System.Drawing.Size(166, 538);
            this.IronTree.TabIndex = 1;
            this.IronTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.IronTree_AfterSelect);
            // 
            // IronTreeMenuStrip
            // 
            this.IronTreeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScanBranchToolStripMenuItem});
            this.IronTreeMenuStrip.Name = "IronTreeMenuStrip";
            this.IronTreeMenuStrip.Size = new System.Drawing.Size(140, 26);
            this.IronTreeMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.IronTreeMenuStrip_Opening);
            // 
            // ScanBranchToolStripMenuItem
            // 
            this.ScanBranchToolStripMenuItem.Name = "ScanBranchToolStripMenuItem";
            this.ScanBranchToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.ScanBranchToolStripMenuItem.Text = "Scan Branch";
            this.ScanBranchToolStripMenuItem.Click += new System.EventHandler(this.ScanBranchToolStripMenuItem_Click);
            // 
            // main_tab
            // 
            this.main_tab.Controls.Add(this.mt_console);
            this.main_tab.Controls.Add(this.mt_auto);
            this.main_tab.Controls.Add(this.mt_manual);
            this.main_tab.Controls.Add(this.mt_proxy);
            this.main_tab.Controls.Add(this.mt_logs);
            this.main_tab.Controls.Add(this.mt_results);
            this.main_tab.Controls.Add(this.mt_plugins);
            this.main_tab.Controls.Add(this.mt_trace);
            this.main_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_tab.Location = new System.Drawing.Point(0, 0);
            this.main_tab.Margin = new System.Windows.Forms.Padding(0);
            this.main_tab.Name = "main_tab";
            this.main_tab.Padding = new System.Drawing.Point(0, 0);
            this.main_tab.SelectedIndex = 0;
            this.main_tab.Size = new System.Drawing.Size(713, 538);
            this.main_tab.TabIndex = 0;
            // 
            // mt_console
            // 
            this.mt_console.BackColor = System.Drawing.Color.White;
            this.mt_console.Controls.Add(this.ScanJobsCompletedLbl);
            this.mt_console.Controls.Add(this.ScanJobsCreatedLbl);
            this.mt_console.Controls.Add(this.CrawlerRequestsLbl);
            this.mt_console.Controls.Add(this.ConsoleStatusTB);
            this.mt_console.Controls.Add(this.label30);
            this.mt_console.Controls.Add(this.ConsoleStartScanBtn);
            this.mt_console.Controls.Add(this.InteractiveScanModeRB);
            this.mt_console.Controls.Add(this.ConfiguredScanModeRB);
            this.mt_console.Controls.Add(this.OptimalScanModeRB);
            this.mt_console.Controls.Add(this.label28);
            this.mt_console.Controls.Add(this.richTextBox2);
            this.mt_console.Controls.Add(this.label29);
            this.mt_console.Controls.Add(this.ConsoleScanUrlTB);
            this.mt_console.Controls.Add(this.ConsoleRTB);
            this.mt_console.Location = new System.Drawing.Point(4, 22);
            this.mt_console.Name = "mt_console";
            this.mt_console.Size = new System.Drawing.Size(705, 512);
            this.mt_console.TabIndex = 8;
            this.mt_console.Text = "Console";
            // 
            // ScanJobsCompletedLbl
            // 
            this.ScanJobsCompletedLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanJobsCompletedLbl.AutoSize = true;
            this.ScanJobsCompletedLbl.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanJobsCompletedLbl.Location = new System.Drawing.Point(252, 259);
            this.ScanJobsCompletedLbl.Name = "ScanJobsCompletedLbl";
            this.ScanJobsCompletedLbl.Size = new System.Drawing.Size(148, 18);
            this.ScanJobsCompletedLbl.TabIndex = 16;
            this.ScanJobsCompletedLbl.Text = "ScanJobs Completed: 0";
            this.ScanJobsCompletedLbl.Visible = false;
            // 
            // ScanJobsCreatedLbl
            // 
            this.ScanJobsCreatedLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanJobsCreatedLbl.AutoSize = true;
            this.ScanJobsCreatedLbl.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanJobsCreatedLbl.Location = new System.Drawing.Point(272, 229);
            this.ScanJobsCreatedLbl.Name = "ScanJobsCreatedLbl";
            this.ScanJobsCreatedLbl.Size = new System.Drawing.Size(128, 18);
            this.ScanJobsCreatedLbl.TabIndex = 15;
            this.ScanJobsCreatedLbl.Text = "ScanJobs Created: 0";
            this.ScanJobsCreatedLbl.Visible = false;
            // 
            // CrawlerRequestsLbl
            // 
            this.CrawlerRequestsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CrawlerRequestsLbl.AutoSize = true;
            this.CrawlerRequestsLbl.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrawlerRequestsLbl.Location = new System.Drawing.Point(236, 198);
            this.CrawlerRequestsLbl.Name = "CrawlerRequestsLbl";
            this.CrawlerRequestsLbl.Size = new System.Drawing.Size(164, 18);
            this.CrawlerRequestsLbl.TabIndex = 14;
            this.CrawlerRequestsLbl.Text = "Requests From Crawler: 0";
            this.CrawlerRequestsLbl.Visible = false;
            // 
            // ConsoleStatusTB
            // 
            this.ConsoleStatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleStatusTB.BackColor = System.Drawing.Color.White;
            this.ConsoleStatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsoleStatusTB.Location = new System.Drawing.Point(141, 150);
            this.ConsoleStatusTB.Multiline = true;
            this.ConsoleStatusTB.Name = "ConsoleStatusTB";
            this.ConsoleStatusTB.ReadOnly = true;
            this.ConsoleStatusTB.Size = new System.Drawing.Size(466, 45);
            this.ConsoleStatusTB.TabIndex = 13;
            this.ConsoleStatusTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ConsoleStatusTB.Enter += new System.EventHandler(this.ConsoleStatusTB_Enter);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Blue;
            this.label30.Location = new System.Drawing.Point(8, 128);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(114, 13);
            this.label30.TabIndex = 12;
            this.label30.Text = "Eg: http://example.org";
            // 
            // ConsoleStartScanBtn
            // 
            this.ConsoleStartScanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleStartScanBtn.BackColor = System.Drawing.Color.Transparent;
            this.ConsoleStartScanBtn.Location = new System.Drawing.Point(614, 104);
            this.ConsoleStartScanBtn.Name = "ConsoleStartScanBtn";
            this.ConsoleStartScanBtn.Size = new System.Drawing.Size(83, 23);
            this.ConsoleStartScanBtn.TabIndex = 11;
            this.ConsoleStartScanBtn.Text = "Start Scan";
            this.ConsoleStartScanBtn.UseVisualStyleBackColor = false;
            this.ConsoleStartScanBtn.Click += new System.EventHandler(this.ConsoleStartScanBtn_Click);
            // 
            // InteractiveScanModeRB
            // 
            this.InteractiveScanModeRB.AutoSize = true;
            this.InteractiveScanModeRB.Location = new System.Drawing.Point(465, 131);
            this.InteractiveScanModeRB.Name = "InteractiveScanModeRB";
            this.InteractiveScanModeRB.Size = new System.Drawing.Size(75, 17);
            this.InteractiveScanModeRB.TabIndex = 10;
            this.InteractiveScanModeRB.Text = "Interactive";
            this.InteractiveScanModeRB.UseVisualStyleBackColor = true;
            this.InteractiveScanModeRB.Visible = false;
            // 
            // ConfiguredScanModeRB
            // 
            this.ConfiguredScanModeRB.AutoSize = true;
            this.ConfiguredScanModeRB.Location = new System.Drawing.Point(322, 131);
            this.ConfiguredScanModeRB.Name = "ConfiguredScanModeRB";
            this.ConfiguredScanModeRB.Size = new System.Drawing.Size(142, 17);
            this.ConfiguredScanModeRB.TabIndex = 9;
            this.ConfiguredScanModeRB.Text = "User Configured Settings";
            this.ConfiguredScanModeRB.UseVisualStyleBackColor = true;
            // 
            // OptimalScanModeRB
            // 
            this.OptimalScanModeRB.AutoSize = true;
            this.OptimalScanModeRB.Checked = true;
            this.OptimalScanModeRB.Location = new System.Drawing.Point(215, 131);
            this.OptimalScanModeRB.Name = "OptimalScanModeRB";
            this.OptimalScanModeRB.Size = new System.Drawing.Size(100, 17);
            this.OptimalScanModeRB.TabIndex = 8;
            this.OptimalScanModeRB.TabStop = true;
            this.OptimalScanModeRB.Text = "Default Settings";
            this.OptimalScanModeRB.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(140, 134);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 13);
            this.label28.TabIndex = 7;
            this.label28.Text = "Scan Modes:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.BackColor = System.Drawing.Color.White;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(0, 307);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(705, 205);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.Text = "";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(4, 106);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(131, 18);
            this.label29.TabIndex = 5;
            this.label29.Text = "Enter a URL to Scan:";
            // 
            // ConsoleScanUrlTB
            // 
            this.ConsoleScanUrlTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleScanUrlTB.Location = new System.Drawing.Point(141, 106);
            this.ConsoleScanUrlTB.Name = "ConsoleScanUrlTB";
            this.ConsoleScanUrlTB.Size = new System.Drawing.Size(466, 20);
            this.ConsoleScanUrlTB.TabIndex = 4;
            // 
            // ConsoleRTB
            // 
            this.ConsoleRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleRTB.BackColor = System.Drawing.Color.White;
            this.ConsoleRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsoleRTB.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleRTB.ForeColor = System.Drawing.Color.Gray;
            this.ConsoleRTB.Location = new System.Drawing.Point(0, 0);
            this.ConsoleRTB.Margin = new System.Windows.Forms.Padding(0);
            this.ConsoleRTB.Name = "ConsoleRTB";
            this.ConsoleRTB.ReadOnly = true;
            this.ConsoleRTB.Size = new System.Drawing.Size(705, 101);
            this.ConsoleRTB.TabIndex = 3;
            this.ConsoleRTB.Text = resources.GetString("ConsoleRTB.Text");
            // 
            // mt_auto
            // 
            this.mt_auto.Controls.Add(this.ASMainTabs);
            this.mt_auto.Location = new System.Drawing.Point(4, 22);
            this.mt_auto.Margin = new System.Windows.Forms.Padding(0);
            this.mt_auto.Name = "mt_auto";
            this.mt_auto.Size = new System.Drawing.Size(705, 512);
            this.mt_auto.TabIndex = 3;
            this.mt_auto.Text = "Automated Scanning";
            this.mt_auto.UseVisualStyleBackColor = true;
            // 
            // ASMainTabs
            // 
            this.ASMainTabs.Controls.Add(this.ASConfigureTab);
            this.ASMainTabs.Controls.Add(this.ASTraceTab);
            this.ASMainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASMainTabs.Location = new System.Drawing.Point(0, 0);
            this.ASMainTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ASMainTabs.Name = "ASMainTabs";
            this.ASMainTabs.Padding = new System.Drawing.Point(0, 0);
            this.ASMainTabs.SelectedIndex = 0;
            this.ASMainTabs.Size = new System.Drawing.Size(705, 512);
            this.ASMainTabs.TabIndex = 0;
            // 
            // ASConfigureTab
            // 
            this.ASConfigureTab.Controls.Add(this.ASConfigureSplit);
            this.ASConfigureTab.Location = new System.Drawing.Point(4, 22);
            this.ASConfigureTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASConfigureTab.Name = "ASConfigureTab";
            this.ASConfigureTab.Size = new System.Drawing.Size(697, 486);
            this.ASConfigureTab.TabIndex = 0;
            this.ASConfigureTab.Text = "Scan Jobs";
            this.ASConfigureTab.UseVisualStyleBackColor = true;
            // 
            // ASConfigureSplit
            // 
            this.ASConfigureSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASConfigureSplit.Location = new System.Drawing.Point(0, 0);
            this.ASConfigureSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ASConfigureSplit.Name = "ASConfigureSplit";
            this.ASConfigureSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ASConfigureSplit.Panel1
            // 
            this.ASConfigureSplit.Panel1.Controls.Add(this.ASScanPluginsGrid);
            this.ASConfigureSplit.Panel1.Controls.Add(this.ASSessionPluginsCombo);
            this.ASConfigureSplit.Panel1.Controls.Add(this.ASStartScanBtn);
            this.ASConfigureSplit.Panel1.Controls.Add(this.label4);
            this.ASConfigureSplit.Panel1.Controls.Add(this.ConfigureScanRequestSSLCB);
            this.ASConfigureSplit.Panel1.Controls.Add(this.ASExceptionTB);
            this.ASConfigureSplit.Panel1.Controls.Add(this.ASRequestTabs);
            this.ASConfigureSplit.Panel1.Controls.Add(this.ScanStatusLbl);
            this.ASConfigureSplit.Panel1.Controls.Add(this.ScanIDLbl);
            // 
            // ASConfigureSplit.Panel2
            // 
            this.ASConfigureSplit.Panel2.Controls.Add(this.ASQueueGrid);
            this.ASConfigureSplit.Size = new System.Drawing.Size(697, 486);
            this.ASConfigureSplit.SplitterDistance = 287;
            this.ASConfigureSplit.SplitterWidth = 2;
            this.ASConfigureSplit.TabIndex = 0;
            // 
            // ASScanPluginsGrid
            // 
            this.ASScanPluginsGrid.AllowUserToAddRows = false;
            this.ASScanPluginsGrid.AllowUserToDeleteRows = false;
            this.ASScanPluginsGrid.AllowUserToResizeRows = false;
            this.ASScanPluginsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ASScanPluginsGrid.BackgroundColor = System.Drawing.Color.White;
            this.ASScanPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ASScanPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ASScanPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn9,
            this.dataGridViewTextBoxColumn27});
            this.ASScanPluginsGrid.GridColor = System.Drawing.Color.White;
            this.ASScanPluginsGrid.Location = new System.Drawing.Point(543, 45);
            this.ASScanPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ASScanPluginsGrid.Name = "ASScanPluginsGrid";
            this.ASScanPluginsGrid.ReadOnly = true;
            this.ASScanPluginsGrid.RowHeadersVisible = false;
            this.ASScanPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ASScanPluginsGrid.Size = new System.Drawing.Size(154, 242);
            this.ASScanPluginsGrid.TabIndex = 5;
            this.ASScanPluginsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ASScanPluginsGrid_CellClick);
            // 
            // dataGridViewCheckBoxColumn9
            // 
            this.dataGridViewCheckBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewCheckBoxColumn9.HeaderText = "";
            this.dataGridViewCheckBoxColumn9.Name = "dataGridViewCheckBoxColumn9";
            this.dataGridViewCheckBoxColumn9.ReadOnly = true;
            this.dataGridViewCheckBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn9.Width = 20;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn27.HeaderText = "SCAN PLUGINS";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASSessionPluginsCombo
            // 
            this.ASSessionPluginsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASSessionPluginsCombo.FormattingEnabled = true;
            this.ASSessionPluginsCombo.Location = new System.Drawing.Point(349, 6);
            this.ASSessionPluginsCombo.Name = "ASSessionPluginsCombo";
            this.ASSessionPluginsCombo.Size = new System.Drawing.Size(191, 21);
            this.ASSessionPluginsCombo.TabIndex = 3;
            this.ASSessionPluginsCombo.SelectionChangeCommitted += new System.EventHandler(this.ASSessionPluginsCombo_SelectionChangeCommitted);
            // 
            // ASStartScanBtn
            // 
            this.ASStartScanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASStartScanBtn.Location = new System.Drawing.Point(546, 5);
            this.ASStartScanBtn.Name = "ASStartScanBtn";
            this.ASStartScanBtn.Size = new System.Drawing.Size(145, 23);
            this.ASStartScanBtn.TabIndex = 2;
            this.ASStartScanBtn.Text = "Scan";
            this.ASStartScanBtn.UseVisualStyleBackColor = true;
            this.ASStartScanBtn.Click += new System.EventHandler(this.ASStartScanBtn_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(238, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Session Plugin:";
            // 
            // ConfigureScanRequestSSLCB
            // 
            this.ConfigureScanRequestSSLCB.AutoSize = true;
            this.ConfigureScanRequestSSLCB.Location = new System.Drawing.Point(6, 27);
            this.ConfigureScanRequestSSLCB.Name = "ConfigureScanRequestSSLCB";
            this.ConfigureScanRequestSSLCB.Size = new System.Drawing.Size(46, 17);
            this.ConfigureScanRequestSSLCB.TabIndex = 11;
            this.ConfigureScanRequestSSLCB.Text = "SSL";
            this.ConfigureScanRequestSSLCB.UseVisualStyleBackColor = true;
            this.ConfigureScanRequestSSLCB.Click += new System.EventHandler(this.ConfigureScanRequestSSLCB_Click);
            // 
            // ASExceptionTB
            // 
            this.ASExceptionTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ASExceptionTB.BackColor = System.Drawing.SystemColors.Window;
            this.ASExceptionTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ASExceptionTB.ForeColor = System.Drawing.Color.Red;
            this.ASExceptionTB.Location = new System.Drawing.Point(61, 29);
            this.ASExceptionTB.Name = "ASExceptionTB";
            this.ASExceptionTB.ReadOnly = true;
            this.ASExceptionTB.Size = new System.Drawing.Size(482, 13);
            this.ASExceptionTB.TabIndex = 8;
            this.ASExceptionTB.Visible = false;
            // 
            // ASRequestTabs
            // 
            this.ASRequestTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestTabs.Controls.Add(this.ASRequestFullTab);
            this.ASRequestTabs.Controls.Add(this.ASRequestURLTab);
            this.ASRequestTabs.Controls.Add(this.ASRequestQueryTab);
            this.ASRequestTabs.Controls.Add(this.ASRequestBodyTab);
            this.ASRequestTabs.Controls.Add(this.ASRequestCookieTab);
            this.ASRequestTabs.Controls.Add(this.ASRequestHeadersTab);
            this.ASRequestTabs.Location = new System.Drawing.Point(0, 46);
            this.ASRequestTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestTabs.Name = "ASRequestTabs";
            this.ASRequestTabs.Padding = new System.Drawing.Point(0, 0);
            this.ASRequestTabs.SelectedIndex = 0;
            this.ASRequestTabs.Size = new System.Drawing.Size(543, 241);
            this.ASRequestTabs.TabIndex = 0;
            this.ASRequestTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.ASRequestTabs_Deselecting);
            // 
            // ASRequestFullTab
            // 
            this.ASRequestFullTab.Controls.Add(this.ASRequestScanFullTabs);
            this.ASRequestFullTab.Controls.Add(this.ASRequestScanHeadersCB);
            this.ASRequestFullTab.Controls.Add(this.ASRequestScanCookieCB);
            this.ASRequestFullTab.Controls.Add(this.label3);
            this.ASRequestFullTab.Controls.Add(this.ASRequestScanBodyCB);
            this.ASRequestFullTab.Controls.Add(this.ASRequestScanAllCB);
            this.ASRequestFullTab.Controls.Add(this.ASRequestScanQueryCB);
            this.ASRequestFullTab.Controls.Add(this.ASRequestScanURLCB);
            this.ASRequestFullTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestFullTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestFullTab.Name = "ASRequestFullTab";
            this.ASRequestFullTab.Size = new System.Drawing.Size(535, 215);
            this.ASRequestFullTab.TabIndex = 5;
            this.ASRequestFullTab.Text = "Full";
            this.ASRequestFullTab.UseVisualStyleBackColor = true;
            // 
            // ASRequestScanFullTabs
            // 
            this.ASRequestScanFullTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestScanFullTabs.Controls.Add(this.tabPage20);
            this.ASRequestScanFullTabs.Controls.Add(this.tabPage21);
            this.ASRequestScanFullTabs.Location = new System.Drawing.Point(0, 0);
            this.ASRequestScanFullTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanFullTabs.Multiline = true;
            this.ASRequestScanFullTabs.Name = "ASRequestScanFullTabs";
            this.ASRequestScanFullTabs.Padding = new System.Drawing.Point(0, 0);
            this.ASRequestScanFullTabs.SelectedIndex = 0;
            this.ASRequestScanFullTabs.Size = new System.Drawing.Size(450, 215);
            this.ASRequestScanFullTabs.TabIndex = 0;
            // 
            // tabPage20
            // 
            this.tabPage20.Controls.Add(this.ASRequestRawHeadersIDV);
            this.tabPage20.Location = new System.Drawing.Point(4, 22);
            this.tabPage20.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage20.Name = "tabPage20";
            this.tabPage20.Size = new System.Drawing.Size(442, 189);
            this.tabPage20.TabIndex = 0;
            this.tabPage20.Text = "Raw Headers";
            this.tabPage20.UseVisualStyleBackColor = true;
            // 
            // ASRequestRawHeadersIDV
            // 
            this.ASRequestRawHeadersIDV.AutoSize = true;
            this.ASRequestRawHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestRawHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.ASRequestRawHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestRawHeadersIDV.Name = "ASRequestRawHeadersIDV";
            this.ASRequestRawHeadersIDV.ReadOnly = false;
            this.ASRequestRawHeadersIDV.Size = new System.Drawing.Size(442, 189);
            this.ASRequestRawHeadersIDV.TabIndex = 0;
            this.ASRequestRawHeadersIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.ASRequestRawHeadersIDV_IDVTextChanged);
            // 
            // tabPage21
            // 
            this.tabPage21.Controls.Add(this.ASRequestRawBodyIDV);
            this.tabPage21.Location = new System.Drawing.Point(4, 22);
            this.tabPage21.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage21.Name = "tabPage21";
            this.tabPage21.Size = new System.Drawing.Size(442, 189);
            this.tabPage21.TabIndex = 1;
            this.tabPage21.Text = "Raw Body";
            this.tabPage21.UseVisualStyleBackColor = true;
            // 
            // ASRequestRawBodyIDV
            // 
            this.ASRequestRawBodyIDV.AutoSize = true;
            this.ASRequestRawBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestRawBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.ASRequestRawBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestRawBodyIDV.Name = "ASRequestRawBodyIDV";
            this.ASRequestRawBodyIDV.ReadOnly = false;
            this.ASRequestRawBodyIDV.Size = new System.Drawing.Size(442, 189);
            this.ASRequestRawBodyIDV.TabIndex = 1;
            this.ASRequestRawBodyIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.ASRequestRawBodyIDV_IDVTextChanged);
            // 
            // ASRequestScanHeadersCB
            // 
            this.ASRequestScanHeadersCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestScanHeadersCB.AutoSize = true;
            this.ASRequestScanHeadersCB.Location = new System.Drawing.Point(464, 138);
            this.ASRequestScanHeadersCB.Name = "ASRequestScanHeadersCB";
            this.ASRequestScanHeadersCB.Size = new System.Drawing.Size(66, 17);
            this.ASRequestScanHeadersCB.TabIndex = 5;
            this.ASRequestScanHeadersCB.Text = "Headers";
            this.ASRequestScanHeadersCB.UseVisualStyleBackColor = true;
            this.ASRequestScanHeadersCB.Click += new System.EventHandler(this.ASRequestScanHeadersCB_Click);
            // 
            // ASRequestScanCookieCB
            // 
            this.ASRequestScanCookieCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestScanCookieCB.AutoSize = true;
            this.ASRequestScanCookieCB.Location = new System.Drawing.Point(464, 118);
            this.ASRequestScanCookieCB.Name = "ASRequestScanCookieCB";
            this.ASRequestScanCookieCB.Size = new System.Drawing.Size(59, 17);
            this.ASRequestScanCookieCB.TabIndex = 4;
            this.ASRequestScanCookieCB.Text = "Cookie";
            this.ASRequestScanCookieCB.UseVisualStyleBackColor = true;
            this.ASRequestScanCookieCB.Click += new System.EventHandler(this.ASRequestScanCookieCB_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(461, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Inject:";
            // 
            // ASRequestScanBodyCB
            // 
            this.ASRequestScanBodyCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestScanBodyCB.AutoSize = true;
            this.ASRequestScanBodyCB.Location = new System.Drawing.Point(464, 98);
            this.ASRequestScanBodyCB.Name = "ASRequestScanBodyCB";
            this.ASRequestScanBodyCB.Size = new System.Drawing.Size(50, 17);
            this.ASRequestScanBodyCB.TabIndex = 3;
            this.ASRequestScanBodyCB.Text = "Body";
            this.ASRequestScanBodyCB.UseVisualStyleBackColor = true;
            this.ASRequestScanBodyCB.Click += new System.EventHandler(this.ASRequestScanBodyCB_Click);
            // 
            // ASRequestScanAllCB
            // 
            this.ASRequestScanAllCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestScanAllCB.AutoSize = true;
            this.ASRequestScanAllCB.Location = new System.Drawing.Point(464, 38);
            this.ASRequestScanAllCB.Name = "ASRequestScanAllCB";
            this.ASRequestScanAllCB.Size = new System.Drawing.Size(37, 17);
            this.ASRequestScanAllCB.TabIndex = 0;
            this.ASRequestScanAllCB.Text = "All";
            this.ASRequestScanAllCB.UseVisualStyleBackColor = true;
            this.ASRequestScanAllCB.Click += new System.EventHandler(this.ASRequestScanAllCB_Click);
            // 
            // ASRequestScanQueryCB
            // 
            this.ASRequestScanQueryCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestScanQueryCB.AutoSize = true;
            this.ASRequestScanQueryCB.Location = new System.Drawing.Point(464, 78);
            this.ASRequestScanQueryCB.Name = "ASRequestScanQueryCB";
            this.ASRequestScanQueryCB.Size = new System.Drawing.Size(54, 17);
            this.ASRequestScanQueryCB.TabIndex = 2;
            this.ASRequestScanQueryCB.Text = "Query";
            this.ASRequestScanQueryCB.UseVisualStyleBackColor = true;
            this.ASRequestScanQueryCB.Click += new System.EventHandler(this.ASRequestScanQueryCB_Click);
            // 
            // ASRequestScanURLCB
            // 
            this.ASRequestScanURLCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ASRequestScanURLCB.AutoSize = true;
            this.ASRequestScanURLCB.Location = new System.Drawing.Point(464, 58);
            this.ASRequestScanURLCB.Name = "ASRequestScanURLCB";
            this.ASRequestScanURLCB.Size = new System.Drawing.Size(48, 17);
            this.ASRequestScanURLCB.TabIndex = 1;
            this.ASRequestScanURLCB.Text = "URL";
            this.ASRequestScanURLCB.UseVisualStyleBackColor = true;
            this.ASRequestScanURLCB.Click += new System.EventHandler(this.ASRequestScanURLCB_Click);
            // 
            // ASRequestURLTab
            // 
            this.ASRequestURLTab.Controls.Add(this.ASRequestScanURLGrid);
            this.ASRequestURLTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestURLTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestURLTab.Name = "ASRequestURLTab";
            this.ASRequestURLTab.Size = new System.Drawing.Size(535, 215);
            this.ASRequestURLTab.TabIndex = 0;
            this.ASRequestURLTab.Text = "URL";
            this.ASRequestURLTab.UseVisualStyleBackColor = true;
            // 
            // ASRequestScanURLGrid
            // 
            this.ASRequestScanURLGrid.AllowUserToAddRows = false;
            this.ASRequestScanURLGrid.AllowUserToDeleteRows = false;
            this.ASRequestScanURLGrid.AllowUserToResizeRows = false;
            this.ASRequestScanURLGrid.BackgroundColor = System.Drawing.Color.White;
            this.ASRequestScanURLGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ASRequestScanURLGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ASRequestScanURLGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestURLSelectColumn,
            this.ASRequestURLPositionColumn,
            this.ASRequestURLValueColumn});
            this.ASRequestScanURLGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestScanURLGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ASRequestScanURLGrid.GridColor = System.Drawing.Color.White;
            this.ASRequestScanURLGrid.Location = new System.Drawing.Point(0, 0);
            this.ASRequestScanURLGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanURLGrid.MultiSelect = false;
            this.ASRequestScanURLGrid.Name = "ASRequestScanURLGrid";
            this.ASRequestScanURLGrid.RowHeadersVisible = false;
            this.ASRequestScanURLGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ASRequestScanURLGrid.Size = new System.Drawing.Size(535, 215);
            this.ASRequestScanURLGrid.TabIndex = 0;
            this.ASRequestScanURLGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ASRequestScanURLGrid_CellClick);
            // 
            // ASRequestURLSelectColumn
            // 
            this.ASRequestURLSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestURLSelectColumn.HeaderText = "INJECT";
            this.ASRequestURLSelectColumn.Name = "ASRequestURLSelectColumn";
            this.ASRequestURLSelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestURLSelectColumn.Width = 55;
            // 
            // ASRequestURLPositionColumn
            // 
            this.ASRequestURLPositionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestURLPositionColumn.HeaderText = "PARAMETER POSITION";
            this.ASRequestURLPositionColumn.Name = "ASRequestURLPositionColumn";
            this.ASRequestURLPositionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestURLValueColumn
            // 
            this.ASRequestURLValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestURLValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestURLValueColumn.Name = "ASRequestURLValueColumn";
            this.ASRequestURLValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestQueryTab
            // 
            this.ASRequestQueryTab.Controls.Add(this.ASRequestScanQueryGrid);
            this.ASRequestQueryTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestQueryTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestQueryTab.Name = "ASRequestQueryTab";
            this.ASRequestQueryTab.Size = new System.Drawing.Size(535, 215);
            this.ASRequestQueryTab.TabIndex = 1;
            this.ASRequestQueryTab.Text = "Query";
            this.ASRequestQueryTab.UseVisualStyleBackColor = true;
            // 
            // ASRequestScanQueryGrid
            // 
            this.ASRequestScanQueryGrid.AllowUserToAddRows = false;
            this.ASRequestScanQueryGrid.AllowUserToDeleteRows = false;
            this.ASRequestScanQueryGrid.AllowUserToResizeRows = false;
            this.ASRequestScanQueryGrid.BackgroundColor = System.Drawing.Color.White;
            this.ASRequestScanQueryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ASRequestScanQueryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ASRequestScanQueryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestQuerySelectColumn,
            this.ASRequestQueryNameColumn,
            this.ASRequestQueryValueColumn});
            this.ASRequestScanQueryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestScanQueryGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ASRequestScanQueryGrid.GridColor = System.Drawing.Color.White;
            this.ASRequestScanQueryGrid.Location = new System.Drawing.Point(0, 0);
            this.ASRequestScanQueryGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanQueryGrid.Name = "ASRequestScanQueryGrid";
            this.ASRequestScanQueryGrid.RowHeadersVisible = false;
            this.ASRequestScanQueryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ASRequestScanQueryGrid.Size = new System.Drawing.Size(535, 215);
            this.ASRequestScanQueryGrid.TabIndex = 1;
            this.ASRequestScanQueryGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ASRequestScanQueryGrid_CellClick);
            // 
            // ASRequestQuerySelectColumn
            // 
            this.ASRequestQuerySelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestQuerySelectColumn.HeaderText = "INJECT";
            this.ASRequestQuerySelectColumn.Name = "ASRequestQuerySelectColumn";
            this.ASRequestQuerySelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestQuerySelectColumn.Width = 55;
            // 
            // ASRequestQueryNameColumn
            // 
            this.ASRequestQueryNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestQueryNameColumn.HeaderText = "PARAMETER NAME";
            this.ASRequestQueryNameColumn.Name = "ASRequestQueryNameColumn";
            this.ASRequestQueryNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestQueryValueColumn
            // 
            this.ASRequestQueryValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestQueryValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestQueryValueColumn.Name = "ASRequestQueryValueColumn";
            this.ASRequestQueryValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestBodyTab
            // 
            this.ASRequestBodyTab.Controls.Add(this.ASRequestBodyTabSplit);
            this.ASRequestBodyTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestBodyTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestBodyTab.Name = "ASRequestBodyTab";
            this.ASRequestBodyTab.Size = new System.Drawing.Size(535, 215);
            this.ASRequestBodyTab.TabIndex = 2;
            this.ASRequestBodyTab.Text = "Body";
            this.ASRequestBodyTab.UseVisualStyleBackColor = true;
            // 
            // ASRequestBodyTabSplit
            // 
            this.ASRequestBodyTabSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestBodyTabSplit.Location = new System.Drawing.Point(0, 0);
            this.ASRequestBodyTabSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestBodyTabSplit.Name = "ASRequestBodyTabSplit";
            // 
            // ASRequestBodyTabSplit.Panel1
            // 
            this.ASRequestBodyTabSplit.Panel1.Controls.Add(this.ConfigureScanRequestFormatPluginsGrid);
            // 
            // ASRequestBodyTabSplit.Panel2
            // 
            this.ASRequestBodyTabSplit.Panel2.Controls.Add(this.ASRequestScanBodyTabs);
            this.ASRequestBodyTabSplit.Size = new System.Drawing.Size(535, 215);
            this.ASRequestBodyTabSplit.SplitterDistance = 105;
            this.ASRequestBodyTabSplit.SplitterWidth = 2;
            this.ASRequestBodyTabSplit.TabIndex = 0;
            // 
            // ConfigureScanRequestFormatPluginsGrid
            // 
            this.ConfigureScanRequestFormatPluginsGrid.AllowUserToAddRows = false;
            this.ConfigureScanRequestFormatPluginsGrid.AllowUserToDeleteRows = false;
            this.ConfigureScanRequestFormatPluginsGrid.AllowUserToResizeRows = false;
            this.ConfigureScanRequestFormatPluginsGrid.BackgroundColor = System.Drawing.Color.White;
            this.ConfigureScanRequestFormatPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfigureScanRequestFormatPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigureScanRequestFormatPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestBodyDataFormatColumn});
            this.ConfigureScanRequestFormatPluginsGrid.ContextMenuStrip = this.ConfigureScanRequestFormatPluginsMenu;
            this.ConfigureScanRequestFormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigureScanRequestFormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ConfigureScanRequestFormatPluginsGrid.GridColor = System.Drawing.Color.White;
            this.ConfigureScanRequestFormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.ConfigureScanRequestFormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigureScanRequestFormatPluginsGrid.MultiSelect = false;
            this.ConfigureScanRequestFormatPluginsGrid.Name = "ConfigureScanRequestFormatPluginsGrid";
            this.ConfigureScanRequestFormatPluginsGrid.RowHeadersVisible = false;
            this.ConfigureScanRequestFormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ConfigureScanRequestFormatPluginsGrid.Size = new System.Drawing.Size(105, 215);
            this.ConfigureScanRequestFormatPluginsGrid.TabIndex = 0;
            // 
            // ASRequestBodyDataFormatColumn
            // 
            this.ASRequestBodyDataFormatColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestBodyDataFormatColumn.HeaderText = "FORMAT";
            this.ASRequestBodyDataFormatColumn.Name = "ASRequestBodyDataFormatColumn";
            this.ASRequestBodyDataFormatColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ConfigureScanRequestFormatPluginsMenu
            // 
            this.ConfigureScanRequestFormatPluginsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigureScanRequestDeSerObjectToXmlMenuItem});
            this.ConfigureScanRequestFormatPluginsMenu.Name = "ProxyLogMenu";
            this.ConfigureScanRequestFormatPluginsMenu.Size = new System.Drawing.Size(230, 26);
            this.ConfigureScanRequestFormatPluginsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ConfigureScanRequestFormatPluginsMenu_Opening);
            // 
            // ConfigureScanRequestDeSerObjectToXmlMenuItem
            // 
            this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Name = "ConfigureScanRequestDeSerObjectToXmlMenuItem";
            this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Size = new System.Drawing.Size(229, 22);
            this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Text = "DeSerialize to Injection Points";
            this.ConfigureScanRequestDeSerObjectToXmlMenuItem.Click += new System.EventHandler(this.ConfigureScanRequestDeSerObjectToXmlMenuItem_Click);
            // 
            // ASRequestScanBodyTabs
            // 
            this.ASRequestScanBodyTabs.Controls.Add(this.ASRequestScanBodyGridTab);
            this.ASRequestScanBodyTabs.Controls.Add(this.ASRequestScanBodyXMLTab);
            this.ASRequestScanBodyTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestScanBodyTabs.Location = new System.Drawing.Point(0, 0);
            this.ASRequestScanBodyTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanBodyTabs.Multiline = true;
            this.ASRequestScanBodyTabs.Name = "ASRequestScanBodyTabs";
            this.ASRequestScanBodyTabs.Padding = new System.Drawing.Point(0, 0);
            this.ASRequestScanBodyTabs.SelectedIndex = 0;
            this.ASRequestScanBodyTabs.Size = new System.Drawing.Size(428, 215);
            this.ASRequestScanBodyTabs.TabIndex = 0;
            // 
            // ASRequestScanBodyGridTab
            // 
            this.ASRequestScanBodyGridTab.Controls.Add(this.ConfigureScanRequestBodyGrid);
            this.ASRequestScanBodyGridTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestScanBodyGridTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanBodyGridTab.Name = "ASRequestScanBodyGridTab";
            this.ASRequestScanBodyGridTab.Size = new System.Drawing.Size(420, 189);
            this.ASRequestScanBodyGridTab.TabIndex = 0;
            this.ASRequestScanBodyGridTab.Text = "Injection Points";
            this.ASRequestScanBodyGridTab.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanRequestBodyGrid
            // 
            this.ConfigureScanRequestBodyGrid.AllowUserToAddRows = false;
            this.ConfigureScanRequestBodyGrid.AllowUserToDeleteRows = false;
            this.ConfigureScanRequestBodyGrid.AllowUserToResizeRows = false;
            this.ConfigureScanRequestBodyGrid.BackgroundColor = System.Drawing.Color.White;
            this.ConfigureScanRequestBodyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfigureScanRequestBodyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigureScanRequestBodyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestBodySelectColumn,
            this.ASRequestBodyNameColumn,
            this.ASRequestBodyValueColumn});
            this.ConfigureScanRequestBodyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigureScanRequestBodyGrid.GridColor = System.Drawing.Color.White;
            this.ConfigureScanRequestBodyGrid.Location = new System.Drawing.Point(0, 0);
            this.ConfigureScanRequestBodyGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigureScanRequestBodyGrid.Name = "ConfigureScanRequestBodyGrid";
            this.ConfigureScanRequestBodyGrid.ReadOnly = true;
            this.ConfigureScanRequestBodyGrid.RowHeadersVisible = false;
            this.ConfigureScanRequestBodyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ConfigureScanRequestBodyGrid.Size = new System.Drawing.Size(420, 189);
            this.ConfigureScanRequestBodyGrid.TabIndex = 2;
            this.ConfigureScanRequestBodyGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ASRequestScanBodyGrid_CellClick);
            // 
            // ASRequestBodySelectColumn
            // 
            this.ASRequestBodySelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestBodySelectColumn.HeaderText = "INJECT";
            this.ASRequestBodySelectColumn.Name = "ASRequestBodySelectColumn";
            this.ASRequestBodySelectColumn.ReadOnly = true;
            this.ASRequestBodySelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestBodySelectColumn.Width = 55;
            // 
            // ASRequestBodyNameColumn
            // 
            this.ASRequestBodyNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestBodyNameColumn.HeaderText = "PARAMETER NAME";
            this.ASRequestBodyNameColumn.Name = "ASRequestBodyNameColumn";
            this.ASRequestBodyNameColumn.ReadOnly = true;
            this.ASRequestBodyNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestBodyValueColumn
            // 
            this.ASRequestBodyValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestBodyValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestBodyValueColumn.Name = "ASRequestBodyValueColumn";
            this.ASRequestBodyValueColumn.ReadOnly = true;
            this.ASRequestBodyValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestScanBodyXMLTab
            // 
            this.ASRequestScanBodyXMLTab.Controls.Add(this.ConfigureScanRequestFormatXMLTB);
            this.ASRequestScanBodyXMLTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestScanBodyXMLTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanBodyXMLTab.Name = "ASRequestScanBodyXMLTab";
            this.ASRequestScanBodyXMLTab.Size = new System.Drawing.Size(420, 189);
            this.ASRequestScanBodyXMLTab.TabIndex = 1;
            this.ASRequestScanBodyXMLTab.Text = "XML";
            this.ASRequestScanBodyXMLTab.UseVisualStyleBackColor = true;
            // 
            // ConfigureScanRequestFormatXMLTB
            // 
            this.ConfigureScanRequestFormatXMLTB.BackColor = System.Drawing.SystemColors.Window;
            this.ConfigureScanRequestFormatXMLTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfigureScanRequestFormatXMLTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigureScanRequestFormatXMLTB.Location = new System.Drawing.Point(0, 0);
            this.ConfigureScanRequestFormatXMLTB.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigureScanRequestFormatXMLTB.Multiline = true;
            this.ConfigureScanRequestFormatXMLTB.Name = "ConfigureScanRequestFormatXMLTB";
            this.ConfigureScanRequestFormatXMLTB.ReadOnly = true;
            this.ConfigureScanRequestFormatXMLTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigureScanRequestFormatXMLTB.Size = new System.Drawing.Size(420, 189);
            this.ConfigureScanRequestFormatXMLTB.TabIndex = 0;
            // 
            // ASRequestCookieTab
            // 
            this.ASRequestCookieTab.Controls.Add(this.ASRequestScanCookieGrid);
            this.ASRequestCookieTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestCookieTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestCookieTab.Name = "ASRequestCookieTab";
            this.ASRequestCookieTab.Size = new System.Drawing.Size(535, 215);
            this.ASRequestCookieTab.TabIndex = 3;
            this.ASRequestCookieTab.Text = "Cookie";
            this.ASRequestCookieTab.UseVisualStyleBackColor = true;
            // 
            // ASRequestScanCookieGrid
            // 
            this.ASRequestScanCookieGrid.AllowUserToAddRows = false;
            this.ASRequestScanCookieGrid.AllowUserToDeleteRows = false;
            this.ASRequestScanCookieGrid.AllowUserToResizeRows = false;
            this.ASRequestScanCookieGrid.BackgroundColor = System.Drawing.Color.White;
            this.ASRequestScanCookieGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ASRequestScanCookieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ASRequestScanCookieGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestCookieSelectColumn,
            this.ASRequestCookieNameColumn,
            this.ASRequestCookieValueColumn});
            this.ASRequestScanCookieGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestScanCookieGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ASRequestScanCookieGrid.GridColor = System.Drawing.Color.White;
            this.ASRequestScanCookieGrid.Location = new System.Drawing.Point(0, 0);
            this.ASRequestScanCookieGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanCookieGrid.Name = "ASRequestScanCookieGrid";
            this.ASRequestScanCookieGrid.RowHeadersVisible = false;
            this.ASRequestScanCookieGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ASRequestScanCookieGrid.Size = new System.Drawing.Size(535, 215);
            this.ASRequestScanCookieGrid.TabIndex = 2;
            this.ASRequestScanCookieGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ASRequestScanCookieGrid_CellClick);
            // 
            // ASRequestCookieSelectColumn
            // 
            this.ASRequestCookieSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestCookieSelectColumn.HeaderText = "INJECT";
            this.ASRequestCookieSelectColumn.Name = "ASRequestCookieSelectColumn";
            this.ASRequestCookieSelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestCookieSelectColumn.Width = 55;
            // 
            // ASRequestCookieNameColumn
            // 
            this.ASRequestCookieNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestCookieNameColumn.HeaderText = "PARAMETER NAME";
            this.ASRequestCookieNameColumn.Name = "ASRequestCookieNameColumn";
            this.ASRequestCookieNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestCookieValueColumn
            // 
            this.ASRequestCookieValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestCookieValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestCookieValueColumn.Name = "ASRequestCookieValueColumn";
            this.ASRequestCookieValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestHeadersTab
            // 
            this.ASRequestHeadersTab.Controls.Add(this.ASRequestScanHeadersGrid);
            this.ASRequestHeadersTab.Location = new System.Drawing.Point(4, 22);
            this.ASRequestHeadersTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestHeadersTab.Name = "ASRequestHeadersTab";
            this.ASRequestHeadersTab.Size = new System.Drawing.Size(535, 215);
            this.ASRequestHeadersTab.TabIndex = 4;
            this.ASRequestHeadersTab.Text = "Headers";
            this.ASRequestHeadersTab.UseVisualStyleBackColor = true;
            // 
            // ASRequestScanHeadersGrid
            // 
            this.ASRequestScanHeadersGrid.AllowUserToAddRows = false;
            this.ASRequestScanHeadersGrid.AllowUserToDeleteRows = false;
            this.ASRequestScanHeadersGrid.AllowUserToResizeRows = false;
            this.ASRequestScanHeadersGrid.BackgroundColor = System.Drawing.Color.White;
            this.ASRequestScanHeadersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ASRequestScanHeadersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ASRequestScanHeadersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASRequestHeadersSelectColumn,
            this.ASRequestHeadersNameColumn,
            this.ASRequestHeadersValueColumn});
            this.ASRequestScanHeadersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASRequestScanHeadersGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ASRequestScanHeadersGrid.GridColor = System.Drawing.Color.White;
            this.ASRequestScanHeadersGrid.Location = new System.Drawing.Point(0, 0);
            this.ASRequestScanHeadersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ASRequestScanHeadersGrid.Name = "ASRequestScanHeadersGrid";
            this.ASRequestScanHeadersGrid.RowHeadersVisible = false;
            this.ASRequestScanHeadersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ASRequestScanHeadersGrid.Size = new System.Drawing.Size(535, 215);
            this.ASRequestScanHeadersGrid.TabIndex = 3;
            this.ASRequestScanHeadersGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ASRequestScanHeadersGrid_CellClick);
            // 
            // ASRequestHeadersSelectColumn
            // 
            this.ASRequestHeadersSelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASRequestHeadersSelectColumn.HeaderText = "INJECT";
            this.ASRequestHeadersSelectColumn.Name = "ASRequestHeadersSelectColumn";
            this.ASRequestHeadersSelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASRequestHeadersSelectColumn.Width = 55;
            // 
            // ASRequestHeadersNameColumn
            // 
            this.ASRequestHeadersNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestHeadersNameColumn.HeaderText = "PARAMETER NAME";
            this.ASRequestHeadersNameColumn.Name = "ASRequestHeadersNameColumn";
            this.ASRequestHeadersNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ASRequestHeadersValueColumn
            // 
            this.ASRequestHeadersValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASRequestHeadersValueColumn.HeaderText = "PARAMETER VALUE";
            this.ASRequestHeadersValueColumn.Name = "ASRequestHeadersValueColumn";
            this.ASRequestHeadersValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ScanStatusLbl
            // 
            this.ScanStatusLbl.AutoSize = true;
            this.ScanStatusLbl.Location = new System.Drawing.Point(92, 6);
            this.ScanStatusLbl.Name = "ScanStatusLbl";
            this.ScanStatusLbl.Size = new System.Drawing.Size(68, 13);
            this.ScanStatusLbl.TabIndex = 10;
            this.ScanStatusLbl.Text = "Scan Status:";
            // 
            // ScanIDLbl
            // 
            this.ScanIDLbl.AutoSize = true;
            this.ScanIDLbl.Location = new System.Drawing.Point(6, 6);
            this.ScanIDLbl.Name = "ScanIDLbl";
            this.ScanIDLbl.Size = new System.Drawing.Size(49, 13);
            this.ScanIDLbl.TabIndex = 9;
            this.ScanIDLbl.Text = "Scan ID:";
            // 
            // ASQueueGrid
            // 
            this.ASQueueGrid.AllowUserToAddRows = false;
            this.ASQueueGrid.AllowUserToDeleteRows = false;
            this.ASQueueGrid.AllowUserToResizeRows = false;
            this.ASQueueGrid.BackgroundColor = System.Drawing.Color.White;
            this.ASQueueGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ASQueueGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ASQueueGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ASQueueGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASQueueGridScanID,
            this.ASQueueGridStatus,
            this.ASQueueGridMethod,
            this.ASQueueGridURL});
            this.ASQueueGrid.ContextMenuStrip = this.ScanQueueMenu;
            this.ASQueueGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASQueueGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ASQueueGrid.GridColor = System.Drawing.Color.White;
            this.ASQueueGrid.Location = new System.Drawing.Point(0, 0);
            this.ASQueueGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ASQueueGrid.MultiSelect = false;
            this.ASQueueGrid.Name = "ASQueueGrid";
            this.ASQueueGrid.RowHeadersVisible = false;
            this.ASQueueGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ASQueueGrid.ShowEditingIcon = false;
            this.ASQueueGrid.Size = new System.Drawing.Size(697, 197);
            this.ASQueueGrid.TabIndex = 0;
            this.ASQueueGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ASQueueGrid_CellClick);
            // 
            // ASQueueGridScanID
            // 
            this.ASQueueGridScanID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASQueueGridScanID.HeaderText = "SCAN ID";
            this.ASQueueGridScanID.Name = "ASQueueGridScanID";
            this.ASQueueGridScanID.Width = 60;
            // 
            // ASQueueGridStatus
            // 
            this.ASQueueGridStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ASQueueGridStatus.HeaderText = "STATUS";
            this.ASQueueGridStatus.Name = "ASQueueGridStatus";
            this.ASQueueGridStatus.Width = 70;
            // 
            // ASQueueGridMethod
            // 
            this.ASQueueGridMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ASQueueGridMethod.HeaderText = "METHOD";
            this.ASQueueGridMethod.Name = "ASQueueGridMethod";
            this.ASQueueGridMethod.Width = 79;
            // 
            // ASQueueGridURL
            // 
            this.ASQueueGridURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ASQueueGridURL.HeaderText = "URL";
            this.ASQueueGridURL.Name = "ASQueueGridURL";
            // 
            // ScanQueueMenu
            // 
            this.ScanQueueMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartAllStoppedAndAbortedScansToolStripMenuItem,
            this.StopAllScansToolStripMenuItem,
            this.StopThisScanJobToolStripMenuItem});
            this.ScanQueueMenu.Name = "ScanQueueMenu";
            this.ScanQueueMenu.Size = new System.Drawing.Size(286, 70);
            this.ScanQueueMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ScanQueueMenu_Opening);
            // 
            // StartAllStoppedAndAbortedScansToolStripMenuItem
            // 
            this.StartAllStoppedAndAbortedScansToolStripMenuItem.Name = "StartAllStoppedAndAbortedScansToolStripMenuItem";
            this.StartAllStoppedAndAbortedScansToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.StartAllStoppedAndAbortedScansToolStripMenuItem.Text = "Start All Stopped and Aborted Scan Jobs";
            this.StartAllStoppedAndAbortedScansToolStripMenuItem.Click += new System.EventHandler(this.StartAllStoppedAndAbortedScansToolStripMenuItem_Click);
            // 
            // StopAllScansToolStripMenuItem
            // 
            this.StopAllScansToolStripMenuItem.Name = "StopAllScansToolStripMenuItem";
            this.StopAllScansToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.StopAllScansToolStripMenuItem.Text = "Stop All Scan Jobs";
            this.StopAllScansToolStripMenuItem.Click += new System.EventHandler(this.StopAllScansToolStripMenuItem_Click);
            // 
            // StopThisScanJobToolStripMenuItem
            // 
            this.StopThisScanJobToolStripMenuItem.Name = "StopThisScanJobToolStripMenuItem";
            this.StopThisScanJobToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.StopThisScanJobToolStripMenuItem.Text = "Stop This Scan Job";
            this.StopThisScanJobToolStripMenuItem.Click += new System.EventHandler(this.StopThisScanJobToolStripMenuItem_Click);
            // 
            // ASTraceTab
            // 
            this.ASTraceTab.Controls.Add(this.ScanTraceBaseSplit);
            this.ASTraceTab.Location = new System.Drawing.Point(4, 22);
            this.ASTraceTab.Margin = new System.Windows.Forms.Padding(0);
            this.ASTraceTab.Name = "ASTraceTab";
            this.ASTraceTab.Size = new System.Drawing.Size(697, 486);
            this.ASTraceTab.TabIndex = 2;
            this.ASTraceTab.Text = "Scan Trace";
            this.ASTraceTab.UseVisualStyleBackColor = true;
            // 
            // ScanTraceBaseSplit
            // 
            this.ScanTraceBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanTraceBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.ScanTraceBaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ScanTraceBaseSplit.Name = "ScanTraceBaseSplit";
            this.ScanTraceBaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ScanTraceBaseSplit.Panel1
            // 
            this.ScanTraceBaseSplit.Panel1.Controls.Add(this.ScanTraceGrid);
            // 
            // ScanTraceBaseSplit.Panel2
            // 
            this.ScanTraceBaseSplit.Panel2.Controls.Add(this.ScanTraceMsgRTB);
            this.ScanTraceBaseSplit.Size = new System.Drawing.Size(697, 486);
            this.ScanTraceBaseSplit.SplitterDistance = 233;
            this.ScanTraceBaseSplit.TabIndex = 1;
            // 
            // ScanTraceGrid
            // 
            this.ScanTraceGrid.AllowUserToAddRows = false;
            this.ScanTraceGrid.AllowUserToDeleteRows = false;
            this.ScanTraceGrid.AllowUserToOrderColumns = true;
            this.ScanTraceGrid.AllowUserToResizeRows = false;
            this.ScanTraceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ScanTraceGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanTraceGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScanTraceGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ScanTraceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ScanTraceGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.Column2,
            this.dataGridViewTextBoxColumn37,
            this.Column3});
            this.ScanTraceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanTraceGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanTraceGrid.GridColor = System.Drawing.Color.White;
            this.ScanTraceGrid.Location = new System.Drawing.Point(0, 0);
            this.ScanTraceGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanTraceGrid.MultiSelect = false;
            this.ScanTraceGrid.Name = "ScanTraceGrid";
            this.ScanTraceGrid.ReadOnly = true;
            this.ScanTraceGrid.RowHeadersVisible = false;
            this.ScanTraceGrid.RowHeadersWidth = 10;
            this.ScanTraceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanTraceGrid.Size = new System.Drawing.Size(697, 233);
            this.ScanTraceGrid.TabIndex = 8;
            this.ScanTraceGrid.SelectionChanged += new System.EventHandler(this.ScanTraceGrid_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn32.HeaderText = "ID";
            this.dataGridViewTextBoxColumn32.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.Width = 50;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn35.HeaderText = "SCAN ID";
            this.dataGridViewTextBoxColumn35.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.Width = 70;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn36.HeaderText = "PLUGIN";
            this.dataGridViewTextBoxColumn36.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.Width = 90;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "SECTION";
            this.Column2.MinimumWidth = 50;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 90;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn37.HeaderText = "PARAMETER";
            this.dataGridViewTextBoxColumn37.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "TITLE";
            this.Column3.MinimumWidth = 150;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // ScanTraceMsgRTB
            // 
            this.ScanTraceMsgRTB.BackColor = System.Drawing.Color.White;
            this.ScanTraceMsgRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScanTraceMsgRTB.DetectUrls = false;
            this.ScanTraceMsgRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanTraceMsgRTB.Location = new System.Drawing.Point(0, 0);
            this.ScanTraceMsgRTB.Name = "ScanTraceMsgRTB";
            this.ScanTraceMsgRTB.ReadOnly = true;
            this.ScanTraceMsgRTB.Size = new System.Drawing.Size(697, 249);
            this.ScanTraceMsgRTB.TabIndex = 0;
            this.ScanTraceMsgRTB.Text = "";
            // 
            // mt_manual
            // 
            this.mt_manual.Controls.Add(this.MTTabs);
            this.mt_manual.Location = new System.Drawing.Point(4, 22);
            this.mt_manual.Margin = new System.Windows.Forms.Padding(0);
            this.mt_manual.Name = "mt_manual";
            this.mt_manual.Size = new System.Drawing.Size(705, 512);
            this.mt_manual.TabIndex = 2;
            this.mt_manual.Text = "Manual Testing";
            this.mt_manual.UseVisualStyleBackColor = true;
            // 
            // MTTabs
            // 
            this.MTTabs.Controls.Add(this.MTTestTP);
            this.MTTabs.Controls.Add(this.MTScriptingTP);
            this.MTTabs.Controls.Add(this.MTJavaScriptTaintTP);
            this.MTTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTTabs.Location = new System.Drawing.Point(0, 0);
            this.MTTabs.Margin = new System.Windows.Forms.Padding(0);
            this.MTTabs.Name = "MTTabs";
            this.MTTabs.Padding = new System.Drawing.Point(0, 0);
            this.MTTabs.SelectedIndex = 0;
            this.MTTabs.Size = new System.Drawing.Size(705, 512);
            this.MTTabs.TabIndex = 0;
            // 
            // MTTestTP
            // 
            this.MTTestTP.Controls.Add(this.MTBaseSplit);
            this.MTTestTP.Location = new System.Drawing.Point(4, 22);
            this.MTTestTP.Name = "MTTestTP";
            this.MTTestTP.Size = new System.Drawing.Size(697, 486);
            this.MTTestTP.TabIndex = 5;
            this.MTTestTP.Text = "Test Server";
            this.MTTestTP.UseVisualStyleBackColor = true;
            // 
            // MTBaseSplit
            // 
            this.MTBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.MTBaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.MTBaseSplit.Name = "MTBaseSplit";
            this.MTBaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MTBaseSplit.Panel1
            // 
            this.MTBaseSplit.Panel1.Controls.Add(this.NextTestLog);
            this.MTBaseSplit.Panel1.Controls.Add(this.PreviousTestLog);
            this.MTBaseSplit.Panel1.Controls.Add(this.MTReqResTabs);
            this.MTBaseSplit.Panel1.Controls.Add(this.MTClearFieldsBtn);
            this.MTBaseSplit.Panel1.Controls.Add(this.MTExceptionTB);
            this.MTBaseSplit.Panel1.Controls.Add(this.MTStoredRequestBtn);
            this.MTBaseSplit.Panel1.Controls.Add(this.MTScriptedSendBtn);
            this.MTBaseSplit.Panel1.Controls.Add(this.MTIsSSLCB);
            this.MTBaseSplit.Panel1.Controls.Add(this.MTSendBtn);
            this.MTBaseSplit.Panel1.Controls.Add(this.TestIDLbl);
            // 
            // MTBaseSplit.Panel2
            // 
            this.MTBaseSplit.Panel2.Controls.Add(this.TestGroupLogGrid);
            this.MTBaseSplit.Panel2.Controls.Add(this.TestBrownGroupBtn);
            this.MTBaseSplit.Panel2.Controls.Add(this.TestGrayGroupBtn);
            this.MTBaseSplit.Panel2.Controls.Add(this.TestBlueGroupBtn);
            this.MTBaseSplit.Panel2.Controls.Add(this.TestGreenGroupBtn);
            this.MTBaseSplit.Panel2.Controls.Add(this.TestRedGroupBtn);
            this.MTBaseSplit.Size = new System.Drawing.Size(697, 486);
            this.MTBaseSplit.SplitterDistance = 345;
            this.MTBaseSplit.SplitterWidth = 2;
            this.MTBaseSplit.TabIndex = 0;
            // 
            // NextTestLog
            // 
            this.NextTestLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextTestLog.Location = new System.Drawing.Point(59, 4);
            this.NextTestLog.Name = "NextTestLog";
            this.NextTestLog.Size = new System.Drawing.Size(49, 23);
            this.NextTestLog.TabIndex = 12;
            this.NextTestLog.Text = ">";
            this.NextTestLog.UseVisualStyleBackColor = true;
            this.NextTestLog.Click += new System.EventHandler(this.NextTestLog_Click);
            // 
            // PreviousTestLog
            // 
            this.PreviousTestLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviousTestLog.Location = new System.Drawing.Point(4, 4);
            this.PreviousTestLog.Name = "PreviousTestLog";
            this.PreviousTestLog.Size = new System.Drawing.Size(49, 23);
            this.PreviousTestLog.TabIndex = 11;
            this.PreviousTestLog.Text = "<";
            this.PreviousTestLog.UseVisualStyleBackColor = true;
            this.PreviousTestLog.Click += new System.EventHandler(this.PreviousTestLog_Click);
            // 
            // MTReqResTabs
            // 
            this.MTReqResTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MTReqResTabs.Controls.Add(this.MTRequestTab);
            this.MTReqResTabs.Controls.Add(this.MTResponseTab);
            this.MTReqResTabs.Location = new System.Drawing.Point(0, 50);
            this.MTReqResTabs.Margin = new System.Windows.Forms.Padding(0);
            this.MTReqResTabs.Name = "MTReqResTabs";
            this.MTReqResTabs.Padding = new System.Drawing.Point(0, 0);
            this.MTReqResTabs.SelectedIndex = 0;
            this.MTReqResTabs.Size = new System.Drawing.Size(697, 295);
            this.MTReqResTabs.TabIndex = 10;
            // 
            // MTRequestTab
            // 
            this.MTRequestTab.Controls.Add(this.MTRequestTabs);
            this.MTRequestTab.Location = new System.Drawing.Point(4, 22);
            this.MTRequestTab.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestTab.Name = "MTRequestTab";
            this.MTRequestTab.Size = new System.Drawing.Size(689, 269);
            this.MTRequestTab.TabIndex = 0;
            this.MTRequestTab.Text = "Request";
            this.MTRequestTab.UseVisualStyleBackColor = true;
            // 
            // MTRequestTabs
            // 
            this.MTRequestTabs.Controls.Add(this.MTRequestHeadersTP);
            this.MTRequestTabs.Controls.Add(this.MTRequestBodyTP);
            this.MTRequestTabs.Controls.Add(this.MTRequestParametersTP);
            this.MTRequestTabs.Controls.Add(this.MTRequestFormatPluginsTP);
            this.MTRequestTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestTabs.Location = new System.Drawing.Point(0, 0);
            this.MTRequestTabs.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestTabs.Name = "MTRequestTabs";
            this.MTRequestTabs.Padding = new System.Drawing.Point(0, 0);
            this.MTRequestTabs.SelectedIndex = 0;
            this.MTRequestTabs.Size = new System.Drawing.Size(689, 269);
            this.MTRequestTabs.TabIndex = 0;
            this.MTRequestTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MTRequestTabs_Deselecting);
            // 
            // MTRequestHeadersTP
            // 
            this.MTRequestHeadersTP.Controls.Add(this.MTRequestHeadersIDV);
            this.MTRequestHeadersTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MTRequestHeadersTP.Location = new System.Drawing.Point(4, 22);
            this.MTRequestHeadersTP.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestHeadersTP.Name = "MTRequestHeadersTP";
            this.MTRequestHeadersTP.Size = new System.Drawing.Size(681, 243);
            this.MTRequestHeadersTP.TabIndex = 0;
            this.MTRequestHeadersTP.Text = "Headers";
            this.MTRequestHeadersTP.UseVisualStyleBackColor = true;
            // 
            // MTRequestHeadersIDV
            // 
            this.MTRequestHeadersIDV.AutoSize = true;
            this.MTRequestHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.MTRequestHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestHeadersIDV.Name = "MTRequestHeadersIDV";
            this.MTRequestHeadersIDV.ReadOnly = false;
            this.MTRequestHeadersIDV.Size = new System.Drawing.Size(681, 243);
            this.MTRequestHeadersIDV.TabIndex = 1;
            this.MTRequestHeadersIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.MTRequestHeadersIDV_IDVTextChanged);
            // 
            // MTRequestBodyTP
            // 
            this.MTRequestBodyTP.Controls.Add(this.MTRequestBodyIDV);
            this.MTRequestBodyTP.Location = new System.Drawing.Point(4, 22);
            this.MTRequestBodyTP.Name = "MTRequestBodyTP";
            this.MTRequestBodyTP.Size = new System.Drawing.Size(681, 243);
            this.MTRequestBodyTP.TabIndex = 3;
            this.MTRequestBodyTP.Text = "Body";
            this.MTRequestBodyTP.UseVisualStyleBackColor = true;
            // 
            // MTRequestBodyIDV
            // 
            this.MTRequestBodyIDV.AutoSize = true;
            this.MTRequestBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.MTRequestBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestBodyIDV.Name = "MTRequestBodyIDV";
            this.MTRequestBodyIDV.ReadOnly = false;
            this.MTRequestBodyIDV.Size = new System.Drawing.Size(681, 243);
            this.MTRequestBodyIDV.TabIndex = 2;
            this.MTRequestBodyIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.MTRequestBodyIDV_IDVTextChanged);
            // 
            // MTRequestParametersTP
            // 
            this.MTRequestParametersTP.Controls.Add(this.MTRequestParametersTabs);
            this.MTRequestParametersTP.Location = new System.Drawing.Point(4, 22);
            this.MTRequestParametersTP.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestParametersTP.Name = "MTRequestParametersTP";
            this.MTRequestParametersTP.Size = new System.Drawing.Size(681, 243);
            this.MTRequestParametersTP.TabIndex = 1;
            this.MTRequestParametersTP.Text = "Parameters";
            this.MTRequestParametersTP.UseVisualStyleBackColor = true;
            // 
            // MTRequestParametersTabs
            // 
            this.MTRequestParametersTabs.Controls.Add(this.tabPage1);
            this.MTRequestParametersTabs.Controls.Add(this.tabPage2);
            this.MTRequestParametersTabs.Controls.Add(this.tabPage3);
            this.MTRequestParametersTabs.Controls.Add(this.tabPage4);
            this.MTRequestParametersTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestParametersTabs.Location = new System.Drawing.Point(0, 0);
            this.MTRequestParametersTabs.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestParametersTabs.Name = "MTRequestParametersTabs";
            this.MTRequestParametersTabs.Padding = new System.Drawing.Point(0, 0);
            this.MTRequestParametersTabs.SelectedIndex = 0;
            this.MTRequestParametersTabs.Size = new System.Drawing.Size(681, 243);
            this.MTRequestParametersTabs.TabIndex = 5;
            this.MTRequestParametersTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MTRequestParametersTabs_Deselecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MTRequestParametersQueryGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(673, 217);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Query";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MTRequestParametersQueryGrid
            // 
            this.MTRequestParametersQueryGrid.AllowUserToAddRows = false;
            this.MTRequestParametersQueryGrid.AllowUserToDeleteRows = false;
            this.MTRequestParametersQueryGrid.BackgroundColor = System.Drawing.Color.White;
            this.MTRequestParametersQueryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTRequestParametersQueryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MTRequestParametersQueryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.MTRequestParametersQueryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestParametersQueryGrid.GridColor = System.Drawing.Color.White;
            this.MTRequestParametersQueryGrid.Location = new System.Drawing.Point(0, 0);
            this.MTRequestParametersQueryGrid.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestParametersQueryGrid.Name = "MTRequestParametersQueryGrid";
            this.MTRequestParametersQueryGrid.RowHeadersVisible = false;
            this.MTRequestParametersQueryGrid.Size = new System.Drawing.Size(673, 217);
            this.MTRequestParametersQueryGrid.TabIndex = 0;
            this.MTRequestParametersQueryGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.MTRequestParametersQueryGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.MTRequestParametersBodyGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(673, 217);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Body";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MTRequestParametersBodyGrid
            // 
            this.MTRequestParametersBodyGrid.AllowUserToAddRows = false;
            this.MTRequestParametersBodyGrid.AllowUserToDeleteRows = false;
            this.MTRequestParametersBodyGrid.BackgroundColor = System.Drawing.Color.White;
            this.MTRequestParametersBodyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTRequestParametersBodyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MTRequestParametersBodyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.MTRequestParametersBodyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestParametersBodyGrid.GridColor = System.Drawing.Color.White;
            this.MTRequestParametersBodyGrid.Location = new System.Drawing.Point(0, 0);
            this.MTRequestParametersBodyGrid.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestParametersBodyGrid.Name = "MTRequestParametersBodyGrid";
            this.MTRequestParametersBodyGrid.RowHeadersVisible = false;
            this.MTRequestParametersBodyGrid.Size = new System.Drawing.Size(673, 217);
            this.MTRequestParametersBodyGrid.TabIndex = 1;
            this.MTRequestParametersBodyGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.MTRequestParametersBodyGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn9.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.MTRequestParametersCookieGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(673, 217);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cookie";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // MTRequestParametersCookieGrid
            // 
            this.MTRequestParametersCookieGrid.AllowUserToAddRows = false;
            this.MTRequestParametersCookieGrid.AllowUserToDeleteRows = false;
            this.MTRequestParametersCookieGrid.BackgroundColor = System.Drawing.Color.White;
            this.MTRequestParametersCookieGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTRequestParametersCookieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MTRequestParametersCookieGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.MTRequestParametersCookieGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestParametersCookieGrid.GridColor = System.Drawing.Color.White;
            this.MTRequestParametersCookieGrid.Location = new System.Drawing.Point(0, 0);
            this.MTRequestParametersCookieGrid.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestParametersCookieGrid.Name = "MTRequestParametersCookieGrid";
            this.MTRequestParametersCookieGrid.RowHeadersVisible = false;
            this.MTRequestParametersCookieGrid.Size = new System.Drawing.Size(673, 217);
            this.MTRequestParametersCookieGrid.TabIndex = 1;
            this.MTRequestParametersCookieGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.MTRequestParametersCookieGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn11.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn12.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.MTRequestParametersHeadersGrid);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(673, 217);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Headers";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // MTRequestParametersHeadersGrid
            // 
            this.MTRequestParametersHeadersGrid.AllowUserToAddRows = false;
            this.MTRequestParametersHeadersGrid.AllowUserToDeleteRows = false;
            this.MTRequestParametersHeadersGrid.BackgroundColor = System.Drawing.Color.White;
            this.MTRequestParametersHeadersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTRequestParametersHeadersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MTRequestParametersHeadersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            this.MTRequestParametersHeadersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestParametersHeadersGrid.GridColor = System.Drawing.Color.White;
            this.MTRequestParametersHeadersGrid.Location = new System.Drawing.Point(0, 0);
            this.MTRequestParametersHeadersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestParametersHeadersGrid.Name = "MTRequestParametersHeadersGrid";
            this.MTRequestParametersHeadersGrid.RowHeadersVisible = false;
            this.MTRequestParametersHeadersGrid.Size = new System.Drawing.Size(673, 217);
            this.MTRequestParametersHeadersGrid.TabIndex = 1;
            this.MTRequestParametersHeadersGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.MTRequestParametersHeadersGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn13.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn14.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // MTRequestFormatPluginsTP
            // 
            this.MTRequestFormatPluginsTP.Controls.Add(this.MTRequestFormatSplit);
            this.MTRequestFormatPluginsTP.Location = new System.Drawing.Point(4, 22);
            this.MTRequestFormatPluginsTP.Name = "MTRequestFormatPluginsTP";
            this.MTRequestFormatPluginsTP.Size = new System.Drawing.Size(681, 243);
            this.MTRequestFormatPluginsTP.TabIndex = 2;
            this.MTRequestFormatPluginsTP.Text = "Format Plugins";
            this.MTRequestFormatPluginsTP.UseVisualStyleBackColor = true;
            // 
            // MTRequestFormatSplit
            // 
            this.MTRequestFormatSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestFormatSplit.Location = new System.Drawing.Point(0, 0);
            this.MTRequestFormatSplit.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestFormatSplit.Name = "MTRequestFormatSplit";
            // 
            // MTRequestFormatSplit.Panel1
            // 
            this.MTRequestFormatSplit.Panel1.Controls.Add(this.MTRequestFormatPluginsGrid);
            // 
            // MTRequestFormatSplit.Panel2
            // 
            this.MTRequestFormatSplit.Panel2.Controls.Add(this.MTRequestFormatXMLTB);
            this.MTRequestFormatSplit.Size = new System.Drawing.Size(681, 243);
            this.MTRequestFormatSplit.SplitterDistance = 108;
            this.MTRequestFormatSplit.SplitterWidth = 2;
            this.MTRequestFormatSplit.TabIndex = 0;
            // 
            // MTRequestFormatPluginsGrid
            // 
            this.MTRequestFormatPluginsGrid.AllowUserToAddRows = false;
            this.MTRequestFormatPluginsGrid.AllowUserToDeleteRows = false;
            this.MTRequestFormatPluginsGrid.AllowUserToResizeRows = false;
            this.MTRequestFormatPluginsGrid.BackgroundColor = System.Drawing.Color.White;
            this.MTRequestFormatPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTRequestFormatPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MTRequestFormatPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn23});
            this.MTRequestFormatPluginsGrid.ContextMenuStrip = this.MTRequestFormatPluginsMenu;
            this.MTRequestFormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestFormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.MTRequestFormatPluginsGrid.GridColor = System.Drawing.Color.White;
            this.MTRequestFormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.MTRequestFormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestFormatPluginsGrid.MultiSelect = false;
            this.MTRequestFormatPluginsGrid.Name = "MTRequestFormatPluginsGrid";
            this.MTRequestFormatPluginsGrid.RowHeadersVisible = false;
            this.MTRequestFormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MTRequestFormatPluginsGrid.Size = new System.Drawing.Size(108, 243);
            this.MTRequestFormatPluginsGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn23.HeaderText = "FORMAT";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MTRequestFormatPluginsMenu
            // 
            this.MTRequestFormatPluginsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MTRequestDeSerObjectToXmlMenuItem,
            this.MTRequestSerXmlToObjectMenuItem});
            this.MTRequestFormatPluginsMenu.Name = "ProxyLogMenu";
            this.MTRequestFormatPluginsMenu.Size = new System.Drawing.Size(210, 48);
            this.MTRequestFormatPluginsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MTRequestFormatPluginsMenu_Opening);
            // 
            // MTRequestDeSerObjectToXmlMenuItem
            // 
            this.MTRequestDeSerObjectToXmlMenuItem.Name = "MTRequestDeSerObjectToXmlMenuItem";
            this.MTRequestDeSerObjectToXmlMenuItem.Size = new System.Drawing.Size(209, 22);
            this.MTRequestDeSerObjectToXmlMenuItem.Text = "DeSerialize Object to XML";
            this.MTRequestDeSerObjectToXmlMenuItem.Click += new System.EventHandler(this.MTRequestDeSerObjectToXmlMenuItem_Click);
            // 
            // MTRequestSerXmlToObjectMenuItem
            // 
            this.MTRequestSerXmlToObjectMenuItem.Name = "MTRequestSerXmlToObjectMenuItem";
            this.MTRequestSerXmlToObjectMenuItem.Size = new System.Drawing.Size(209, 22);
            this.MTRequestSerXmlToObjectMenuItem.Text = "Serialize XML to Object";
            this.MTRequestSerXmlToObjectMenuItem.Click += new System.EventHandler(this.MTRequestSerXmlToObjectMenuItem_Click);
            // 
            // MTRequestFormatXMLTB
            // 
            this.MTRequestFormatXMLTB.BackColor = System.Drawing.SystemColors.Window;
            this.MTRequestFormatXMLTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTRequestFormatXMLTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTRequestFormatXMLTB.Location = new System.Drawing.Point(0, 0);
            this.MTRequestFormatXMLTB.Margin = new System.Windows.Forms.Padding(0);
            this.MTRequestFormatXMLTB.Multiline = true;
            this.MTRequestFormatXMLTB.Name = "MTRequestFormatXMLTB";
            this.MTRequestFormatXMLTB.ReadOnly = true;
            this.MTRequestFormatXMLTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MTRequestFormatXMLTB.Size = new System.Drawing.Size(571, 243);
            this.MTRequestFormatXMLTB.TabIndex = 1;
            // 
            // MTResponseTab
            // 
            this.MTResponseTab.Controls.Add(this.MTResponseTabs);
            this.MTResponseTab.Location = new System.Drawing.Point(4, 22);
            this.MTResponseTab.Margin = new System.Windows.Forms.Padding(0);
            this.MTResponseTab.Name = "MTResponseTab";
            this.MTResponseTab.Size = new System.Drawing.Size(689, 269);
            this.MTResponseTab.TabIndex = 1;
            this.MTResponseTab.Text = "Response";
            this.MTResponseTab.UseVisualStyleBackColor = true;
            // 
            // MTResponseTabs
            // 
            this.MTResponseTabs.Controls.Add(this.MTResponseHeadersTP);
            this.MTResponseTabs.Controls.Add(this.MTResponseBodyTP);
            this.MTResponseTabs.Controls.Add(this.MTResponseReflectionTP);
            this.MTResponseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTResponseTabs.Location = new System.Drawing.Point(0, 0);
            this.MTResponseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.MTResponseTabs.Name = "MTResponseTabs";
            this.MTResponseTabs.Padding = new System.Drawing.Point(0, 0);
            this.MTResponseTabs.SelectedIndex = 0;
            this.MTResponseTabs.Size = new System.Drawing.Size(689, 269);
            this.MTResponseTabs.TabIndex = 0;
            // 
            // MTResponseHeadersTP
            // 
            this.MTResponseHeadersTP.Controls.Add(this.MTResponseHeadersIDV);
            this.MTResponseHeadersTP.Location = new System.Drawing.Point(4, 22);
            this.MTResponseHeadersTP.Margin = new System.Windows.Forms.Padding(0);
            this.MTResponseHeadersTP.Name = "MTResponseHeadersTP";
            this.MTResponseHeadersTP.Size = new System.Drawing.Size(681, 243);
            this.MTResponseHeadersTP.TabIndex = 0;
            this.MTResponseHeadersTP.Text = "Headers";
            this.MTResponseHeadersTP.UseVisualStyleBackColor = true;
            // 
            // MTResponseHeadersIDV
            // 
            this.MTResponseHeadersIDV.AutoSize = true;
            this.MTResponseHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTResponseHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.MTResponseHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.MTResponseHeadersIDV.Name = "MTResponseHeadersIDV";
            this.MTResponseHeadersIDV.ReadOnly = true;
            this.MTResponseHeadersIDV.Size = new System.Drawing.Size(681, 243);
            this.MTResponseHeadersIDV.TabIndex = 2;
            // 
            // MTResponseBodyTP
            // 
            this.MTResponseBodyTP.Controls.Add(this.MTResponseBodyIDV);
            this.MTResponseBodyTP.Location = new System.Drawing.Point(4, 22);
            this.MTResponseBodyTP.Name = "MTResponseBodyTP";
            this.MTResponseBodyTP.Size = new System.Drawing.Size(681, 243);
            this.MTResponseBodyTP.TabIndex = 2;
            this.MTResponseBodyTP.Text = "Body";
            this.MTResponseBodyTP.UseVisualStyleBackColor = true;
            // 
            // MTResponseBodyIDV
            // 
            this.MTResponseBodyIDV.AutoSize = true;
            this.MTResponseBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTResponseBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.MTResponseBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.MTResponseBodyIDV.Name = "MTResponseBodyIDV";
            this.MTResponseBodyIDV.ReadOnly = true;
            this.MTResponseBodyIDV.Size = new System.Drawing.Size(681, 243);
            this.MTResponseBodyIDV.TabIndex = 3;
            // 
            // MTResponseReflectionTP
            // 
            this.MTResponseReflectionTP.Controls.Add(this.MTReflectionsRTB);
            this.MTResponseReflectionTP.Location = new System.Drawing.Point(4, 22);
            this.MTResponseReflectionTP.Margin = new System.Windows.Forms.Padding(0);
            this.MTResponseReflectionTP.Name = "MTResponseReflectionTP";
            this.MTResponseReflectionTP.Size = new System.Drawing.Size(681, 243);
            this.MTResponseReflectionTP.TabIndex = 3;
            this.MTResponseReflectionTP.Text = "Reflections";
            this.MTResponseReflectionTP.UseVisualStyleBackColor = true;
            // 
            // MTReflectionsRTB
            // 
            this.MTReflectionsRTB.BackColor = System.Drawing.Color.White;
            this.MTReflectionsRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTReflectionsRTB.DetectUrls = false;
            this.MTReflectionsRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTReflectionsRTB.Font = new System.Drawing.Font("Lucida Bright", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MTReflectionsRTB.Location = new System.Drawing.Point(0, 0);
            this.MTReflectionsRTB.Name = "MTReflectionsRTB";
            this.MTReflectionsRTB.ReadOnly = true;
            this.MTReflectionsRTB.Size = new System.Drawing.Size(681, 243);
            this.MTReflectionsRTB.TabIndex = 0;
            this.MTReflectionsRTB.Text = "";
            // 
            // MTClearFieldsBtn
            // 
            this.MTClearFieldsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MTClearFieldsBtn.Location = new System.Drawing.Point(490, 2);
            this.MTClearFieldsBtn.Name = "MTClearFieldsBtn";
            this.MTClearFieldsBtn.Size = new System.Drawing.Size(100, 23);
            this.MTClearFieldsBtn.TabIndex = 8;
            this.MTClearFieldsBtn.Text = "Clear";
            this.MTClearFieldsBtn.UseVisualStyleBackColor = true;
            this.MTClearFieldsBtn.Click += new System.EventHandler(this.MTClearFieldsBtn_Click);
            // 
            // MTExceptionTB
            // 
            this.MTExceptionTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MTExceptionTB.BackColor = System.Drawing.SystemColors.Window;
            this.MTExceptionTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MTExceptionTB.ForeColor = System.Drawing.Color.Red;
            this.MTExceptionTB.Location = new System.Drawing.Point(114, 6);
            this.MTExceptionTB.Multiline = true;
            this.MTExceptionTB.Name = "MTExceptionTB";
            this.MTExceptionTB.ReadOnly = true;
            this.MTExceptionTB.Size = new System.Drawing.Size(251, 32);
            this.MTExceptionTB.TabIndex = 7;
            this.MTExceptionTB.Visible = false;
            // 
            // MTStoredRequestBtn
            // 
            this.MTStoredRequestBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MTStoredRequestBtn.Enabled = false;
            this.MTStoredRequestBtn.Location = new System.Drawing.Point(490, 27);
            this.MTStoredRequestBtn.Name = "MTStoredRequestBtn";
            this.MTStoredRequestBtn.Size = new System.Drawing.Size(100, 23);
            this.MTStoredRequestBtn.TabIndex = 6;
            this.MTStoredRequestBtn.Text = "Stored Request";
            this.MTStoredRequestBtn.UseVisualStyleBackColor = true;
            this.MTStoredRequestBtn.Click += new System.EventHandler(this.MTStoredRequestBtn_Click);
            // 
            // MTScriptedSendBtn
            // 
            this.MTScriptedSendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MTScriptedSendBtn.Enabled = false;
            this.MTScriptedSendBtn.Location = new System.Drawing.Point(596, 27);
            this.MTScriptedSendBtn.Name = "MTScriptedSendBtn";
            this.MTScriptedSendBtn.Size = new System.Drawing.Size(100, 23);
            this.MTScriptedSendBtn.TabIndex = 5;
            this.MTScriptedSendBtn.Text = "Scripted Send";
            this.MTScriptedSendBtn.UseVisualStyleBackColor = true;
            this.MTScriptedSendBtn.Click += new System.EventHandler(this.MTScriptedSendBtn_Click);
            // 
            // MTIsSSLCB
            // 
            this.MTIsSSLCB.AutoSize = true;
            this.MTIsSSLCB.Location = new System.Drawing.Point(63, 32);
            this.MTIsSSLCB.Name = "MTIsSSLCB";
            this.MTIsSSLCB.Size = new System.Drawing.Size(46, 17);
            this.MTIsSSLCB.TabIndex = 4;
            this.MTIsSSLCB.Text = "SSL";
            this.MTIsSSLCB.UseVisualStyleBackColor = true;
            this.MTIsSSLCB.CheckedChanged += new System.EventHandler(this.MTIsSSLCB_CheckedChanged);
            // 
            // MTSendBtn
            // 
            this.MTSendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MTSendBtn.Location = new System.Drawing.Point(596, 2);
            this.MTSendBtn.Name = "MTSendBtn";
            this.MTSendBtn.Size = new System.Drawing.Size(100, 23);
            this.MTSendBtn.TabIndex = 1;
            this.MTSendBtn.Text = "Send";
            this.MTSendBtn.UseVisualStyleBackColor = true;
            this.MTSendBtn.Click += new System.EventHandler(this.MTSendBtn_Click);
            // 
            // TestIDLbl
            // 
            this.TestIDLbl.AutoSize = true;
            this.TestIDLbl.BackColor = System.Drawing.Color.Red;
            this.TestIDLbl.Location = new System.Drawing.Point(5, 33);
            this.TestIDLbl.Name = "TestIDLbl";
            this.TestIDLbl.Size = new System.Drawing.Size(24, 13);
            this.TestIDLbl.TabIndex = 2;
            this.TestIDLbl.Text = "ID: ";
            // 
            // TestGroupLogGrid
            // 
            this.TestGroupLogGrid.AllowUserToAddRows = false;
            this.TestGroupLogGrid.AllowUserToDeleteRows = false;
            this.TestGroupLogGrid.AllowUserToOrderColumns = true;
            this.TestGroupLogGrid.AllowUserToResizeRows = false;
            this.TestGroupLogGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TestGroupLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TestGroupLogGrid.BackgroundColor = System.Drawing.Color.White;
            this.TestGroupLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestGroupLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TestGroupLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TestGroupLogGridForID,
            this.TestGroupLogGridForHost,
            this.TestGroupLogGridForMethod,
            this.TestGroupLogGridForURL,
            this.TestGroupLogGridForSSL,
            this.TestGroupLogGridForCode,
            this.TestGroupLogGridForLength,
            this.TestGroupLogGridForMIME,
            this.TestGroupLogGridForSetCookie});
            this.TestGroupLogGrid.ContextMenuStrip = this.LogMenu;
            this.TestGroupLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TestGroupLogGrid.GridColor = System.Drawing.Color.White;
            this.TestGroupLogGrid.Location = new System.Drawing.Point(87, 0);
            this.TestGroupLogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.TestGroupLogGrid.MultiSelect = false;
            this.TestGroupLogGrid.Name = "TestGroupLogGrid";
            this.TestGroupLogGrid.ReadOnly = true;
            this.TestGroupLogGrid.RowHeadersVisible = false;
            this.TestGroupLogGrid.RowHeadersWidth = 10;
            this.TestGroupLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TestGroupLogGrid.Size = new System.Drawing.Size(610, 139);
            this.TestGroupLogGrid.TabIndex = 23;
            this.TestGroupLogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TestGroupLogGrid_CellClick);
            // 
            // TestGroupLogGridForID
            // 
            this.TestGroupLogGridForID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForID.HeaderText = "ID";
            this.TestGroupLogGridForID.MinimumWidth = 50;
            this.TestGroupLogGridForID.Name = "TestGroupLogGridForID";
            this.TestGroupLogGridForID.ReadOnly = true;
            this.TestGroupLogGridForID.Width = 50;
            // 
            // TestGroupLogGridForHost
            // 
            this.TestGroupLogGridForHost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForHost.HeaderText = "HOSTNAME";
            this.TestGroupLogGridForHost.Name = "TestGroupLogGridForHost";
            this.TestGroupLogGridForHost.ReadOnly = true;
            this.TestGroupLogGridForHost.Width = 120;
            // 
            // TestGroupLogGridForMethod
            // 
            this.TestGroupLogGridForMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForMethod.HeaderText = "METHOD";
            this.TestGroupLogGridForMethod.Name = "TestGroupLogGridForMethod";
            this.TestGroupLogGridForMethod.ReadOnly = true;
            this.TestGroupLogGridForMethod.Width = 60;
            // 
            // TestGroupLogGridForURL
            // 
            this.TestGroupLogGridForURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TestGroupLogGridForURL.HeaderText = "URL";
            this.TestGroupLogGridForURL.MinimumWidth = 150;
            this.TestGroupLogGridForURL.Name = "TestGroupLogGridForURL";
            this.TestGroupLogGridForURL.ReadOnly = true;
            // 
            // TestGroupLogGridForSSL
            // 
            this.TestGroupLogGridForSSL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForSSL.HeaderText = "SSL";
            this.TestGroupLogGridForSSL.Name = "TestGroupLogGridForSSL";
            this.TestGroupLogGridForSSL.ReadOnly = true;
            this.TestGroupLogGridForSSL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TestGroupLogGridForSSL.Width = 30;
            // 
            // TestGroupLogGridForCode
            // 
            this.TestGroupLogGridForCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForCode.HeaderText = "CODE";
            this.TestGroupLogGridForCode.Name = "TestGroupLogGridForCode";
            this.TestGroupLogGridForCode.ReadOnly = true;
            this.TestGroupLogGridForCode.Width = 60;
            // 
            // TestGroupLogGridForLength
            // 
            this.TestGroupLogGridForLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForLength.HeaderText = "LENGTH";
            this.TestGroupLogGridForLength.Name = "TestGroupLogGridForLength";
            this.TestGroupLogGridForLength.ReadOnly = true;
            this.TestGroupLogGridForLength.Width = 60;
            // 
            // TestGroupLogGridForMIME
            // 
            this.TestGroupLogGridForMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForMIME.HeaderText = "MIME";
            this.TestGroupLogGridForMIME.Name = "TestGroupLogGridForMIME";
            this.TestGroupLogGridForMIME.ReadOnly = true;
            this.TestGroupLogGridForMIME.Width = 70;
            // 
            // TestGroupLogGridForSetCookie
            // 
            this.TestGroupLogGridForSetCookie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TestGroupLogGridForSetCookie.HeaderText = "SET-COOKIE";
            this.TestGroupLogGridForSetCookie.Name = "TestGroupLogGridForSetCookie";
            this.TestGroupLogGridForSetCookie.ReadOnly = true;
            this.TestGroupLogGridForSetCookie.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TestGroupLogGridForSetCookie.Width = 80;
            // 
            // LogMenu
            // 
            this.LogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectForManualTestingToolStripMenuItem,
            this.SelectForAutomatedScanningToolStripMenuItem,
            this.SelectResponseForJavaScriptTestingToolStripMenuItem,
            this.CopyRequestToolStripMenuItem,
            this.CopyResponseToolStripMenuItem});
            this.LogMenu.Name = "ProxyLogMenu";
            this.LogMenu.Size = new System.Drawing.Size(239, 114);
            this.LogMenu.Opening += new System.ComponentModel.CancelEventHandler(this.LogMenu_Opening);
            // 
            // SelectForManualTestingToolStripMenuItem
            // 
            this.SelectForManualTestingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RedGroupToolStripMenuItem,
            this.GreenGroupToolStripMenuItem,
            this.BlueGroupToolStripMenuItem,
            this.GrayGroupToolStripMenuItem,
            this.BrownGroupToolStripMenuItem});
            this.SelectForManualTestingToolStripMenuItem.Name = "SelectForManualTestingToolStripMenuItem";
            this.SelectForManualTestingToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.SelectForManualTestingToolStripMenuItem.Text = "Select for Manual Testing";
            // 
            // RedGroupToolStripMenuItem
            // 
            this.RedGroupToolStripMenuItem.Name = "RedGroupToolStripMenuItem";
            this.RedGroupToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.RedGroupToolStripMenuItem.Text = "Red Group";
            this.RedGroupToolStripMenuItem.Click += new System.EventHandler(this.RedGroupToolStripMenuItem_Click);
            // 
            // GreenGroupToolStripMenuItem
            // 
            this.GreenGroupToolStripMenuItem.Name = "GreenGroupToolStripMenuItem";
            this.GreenGroupToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.GreenGroupToolStripMenuItem.Text = "Green Group";
            this.GreenGroupToolStripMenuItem.Click += new System.EventHandler(this.GreenGroupToolStripMenuItem_Click);
            // 
            // BlueGroupToolStripMenuItem
            // 
            this.BlueGroupToolStripMenuItem.Name = "BlueGroupToolStripMenuItem";
            this.BlueGroupToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.BlueGroupToolStripMenuItem.Text = "Blue Group";
            this.BlueGroupToolStripMenuItem.Click += new System.EventHandler(this.BlueGroupToolStripMenuItem_Click);
            // 
            // GrayGroupToolStripMenuItem
            // 
            this.GrayGroupToolStripMenuItem.Name = "GrayGroupToolStripMenuItem";
            this.GrayGroupToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.GrayGroupToolStripMenuItem.Text = "Gray Group";
            this.GrayGroupToolStripMenuItem.Click += new System.EventHandler(this.GrayGroupToolStripMenuItem_Click);
            // 
            // BrownGroupToolStripMenuItem
            // 
            this.BrownGroupToolStripMenuItem.Name = "BrownGroupToolStripMenuItem";
            this.BrownGroupToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.BrownGroupToolStripMenuItem.Text = "Brown Group";
            this.BrownGroupToolStripMenuItem.Click += new System.EventHandler(this.BrownGroupToolStripMenuItem_Click);
            // 
            // SelectForAutomatedScanningToolStripMenuItem
            // 
            this.SelectForAutomatedScanningToolStripMenuItem.Name = "SelectForAutomatedScanningToolStripMenuItem";
            this.SelectForAutomatedScanningToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.SelectForAutomatedScanningToolStripMenuItem.Text = "Select for Automated Scanning";
            this.SelectForAutomatedScanningToolStripMenuItem.Click += new System.EventHandler(this.SelectForAutomatedScanningToolStripMenuItem_Click);
            // 
            // SelectResponseForJavaScriptTestingToolStripMenuItem
            // 
            this.SelectResponseForJavaScriptTestingToolStripMenuItem.Name = "SelectResponseForJavaScriptTestingToolStripMenuItem";
            this.SelectResponseForJavaScriptTestingToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.SelectResponseForJavaScriptTestingToolStripMenuItem.Text = "Select for JavaScript Testing";
            this.SelectResponseForJavaScriptTestingToolStripMenuItem.Click += new System.EventHandler(this.SelectResponseForJavaScriptTestingToolStripMenuItem_Click);
            // 
            // CopyRequestToolStripMenuItem
            // 
            this.CopyRequestToolStripMenuItem.Name = "CopyRequestToolStripMenuItem";
            this.CopyRequestToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.CopyRequestToolStripMenuItem.Text = "Copy Request";
            this.CopyRequestToolStripMenuItem.Click += new System.EventHandler(this.CopyRequestToolStripMenuItem_Click);
            // 
            // CopyResponseToolStripMenuItem
            // 
            this.CopyResponseToolStripMenuItem.Name = "CopyResponseToolStripMenuItem";
            this.CopyResponseToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.CopyResponseToolStripMenuItem.Text = "Copy Response";
            this.CopyResponseToolStripMenuItem.Click += new System.EventHandler(this.CopyResponseToolStripMenuItem_Click);
            // 
            // TestBrownGroupBtn
            // 
            this.TestBrownGroupBtn.BackColor = System.Drawing.Color.Chocolate;
            this.TestBrownGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TestBrownGroupBtn.Location = new System.Drawing.Point(3, 110);
            this.TestBrownGroupBtn.Name = "TestBrownGroupBtn";
            this.TestBrownGroupBtn.Size = new System.Drawing.Size(80, 23);
            this.TestBrownGroupBtn.TabIndex = 22;
            this.TestBrownGroupBtn.Text = "Brown Group";
            this.TestBrownGroupBtn.UseVisualStyleBackColor = false;
            this.TestBrownGroupBtn.Click += new System.EventHandler(this.MTBrownGroupBtn_Click);
            // 
            // TestGrayGroupBtn
            // 
            this.TestGrayGroupBtn.BackColor = System.Drawing.Color.Silver;
            this.TestGrayGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TestGrayGroupBtn.Location = new System.Drawing.Point(3, 83);
            this.TestGrayGroupBtn.Name = "TestGrayGroupBtn";
            this.TestGrayGroupBtn.Size = new System.Drawing.Size(80, 23);
            this.TestGrayGroupBtn.TabIndex = 21;
            this.TestGrayGroupBtn.Text = "Gray Group";
            this.TestGrayGroupBtn.UseVisualStyleBackColor = false;
            this.TestGrayGroupBtn.Click += new System.EventHandler(this.MTGrayGroupBtn_Click);
            // 
            // TestBlueGroupBtn
            // 
            this.TestBlueGroupBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.TestBlueGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TestBlueGroupBtn.Location = new System.Drawing.Point(3, 56);
            this.TestBlueGroupBtn.Name = "TestBlueGroupBtn";
            this.TestBlueGroupBtn.Size = new System.Drawing.Size(80, 23);
            this.TestBlueGroupBtn.TabIndex = 20;
            this.TestBlueGroupBtn.Text = "Blue Group";
            this.TestBlueGroupBtn.UseVisualStyleBackColor = false;
            this.TestBlueGroupBtn.Click += new System.EventHandler(this.MTBlueGroupBtn_Click);
            // 
            // TestGreenGroupBtn
            // 
            this.TestGreenGroupBtn.BackColor = System.Drawing.Color.Green;
            this.TestGreenGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TestGreenGroupBtn.Location = new System.Drawing.Point(3, 30);
            this.TestGreenGroupBtn.Name = "TestGreenGroupBtn";
            this.TestGreenGroupBtn.Size = new System.Drawing.Size(80, 23);
            this.TestGreenGroupBtn.TabIndex = 19;
            this.TestGreenGroupBtn.Text = "Green Group";
            this.TestGreenGroupBtn.UseVisualStyleBackColor = false;
            this.TestGreenGroupBtn.Click += new System.EventHandler(this.MTGreenGroupBtn_Click);
            // 
            // TestRedGroupBtn
            // 
            this.TestRedGroupBtn.BackColor = System.Drawing.Color.Red;
            this.TestRedGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TestRedGroupBtn.Location = new System.Drawing.Point(3, 3);
            this.TestRedGroupBtn.Name = "TestRedGroupBtn";
            this.TestRedGroupBtn.Size = new System.Drawing.Size(80, 23);
            this.TestRedGroupBtn.TabIndex = 18;
            this.TestRedGroupBtn.Text = "Red Group";
            this.TestRedGroupBtn.UseVisualStyleBackColor = false;
            this.TestRedGroupBtn.Click += new System.EventHandler(this.MTRedGroupBtn_Click);
            // 
            // MTScriptingTP
            // 
            this.MTScriptingTP.Controls.Add(this.ScriptingShellSplit);
            this.MTScriptingTP.Location = new System.Drawing.Point(4, 22);
            this.MTScriptingTP.Margin = new System.Windows.Forms.Padding(0);
            this.MTScriptingTP.Name = "MTScriptingTP";
            this.MTScriptingTP.Size = new System.Drawing.Size(697, 486);
            this.MTScriptingTP.TabIndex = 1;
            this.MTScriptingTP.Text = "Scripting";
            this.MTScriptingTP.UseVisualStyleBackColor = true;
            // 
            // ScriptingShellSplit
            // 
            this.ScriptingShellSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptingShellSplit.Location = new System.Drawing.Point(0, 0);
            this.ScriptingShellSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellSplit.Name = "ScriptingShellSplit";
            // 
            // ScriptingShellSplit.Panel1
            // 
            this.ScriptingShellSplit.Panel1.Controls.Add(this.ClearShellDisplayBtn);
            this.ScriptingShellSplit.Panel1.Controls.Add(this.MultiLineShellExecuteBtn);
            this.ScriptingShellSplit.Panel1.Controls.Add(this.InteractiveShellCtrlCBtn);
            this.ScriptingShellSplit.Panel1.Controls.Add(this.ScriptingShellTabs);
            this.ScriptingShellSplit.Panel1.Controls.Add(this.InteractiveShellRubyRB);
            this.ScriptingShellSplit.Panel1.Controls.Add(this.label2);
            this.ScriptingShellSplit.Panel1.Controls.Add(this.InteractiveShellPythonRB);
            // 
            // ScriptingShellSplit.Panel2
            // 
            this.ScriptingShellSplit.Panel2.Controls.Add(this.ScriptingShellAPISplit);
            this.ScriptingShellSplit.Size = new System.Drawing.Size(704, 487);
            this.ScriptingShellSplit.SplitterDistance = 533;
            this.ScriptingShellSplit.SplitterWidth = 2;
            this.ScriptingShellSplit.TabIndex = 6;
            // 
            // ClearShellDisplayBtn
            // 
            this.ClearShellDisplayBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearShellDisplayBtn.BackColor = System.Drawing.Color.Transparent;
            this.ClearShellDisplayBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearShellDisplayBtn.ForeColor = System.Drawing.Color.Black;
            this.ClearShellDisplayBtn.Location = new System.Drawing.Point(358, 3);
            this.ClearShellDisplayBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ClearShellDisplayBtn.Name = "ClearShellDisplayBtn";
            this.ClearShellDisplayBtn.Size = new System.Drawing.Size(100, 20);
            this.ClearShellDisplayBtn.TabIndex = 7;
            this.ClearShellDisplayBtn.Text = "Clear Shell Output";
            this.ClearShellDisplayBtn.UseVisualStyleBackColor = false;
            this.ClearShellDisplayBtn.Click += new System.EventHandler(this.ClearShellDisplayBtn_Click);
            // 
            // MultiLineShellExecuteBtn
            // 
            this.MultiLineShellExecuteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MultiLineShellExecuteBtn.BackColor = System.Drawing.Color.Transparent;
            this.MultiLineShellExecuteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MultiLineShellExecuteBtn.ForeColor = System.Drawing.Color.Black;
            this.MultiLineShellExecuteBtn.Location = new System.Drawing.Point(221, 3);
            this.MultiLineShellExecuteBtn.Margin = new System.Windows.Forms.Padding(0);
            this.MultiLineShellExecuteBtn.Name = "MultiLineShellExecuteBtn";
            this.MultiLineShellExecuteBtn.Size = new System.Drawing.Size(129, 20);
            this.MultiLineShellExecuteBtn.TabIndex = 6;
            this.MultiLineShellExecuteBtn.Text = "Execute MutliLine Script";
            this.MultiLineShellExecuteBtn.UseVisualStyleBackColor = false;
            this.MultiLineShellExecuteBtn.Visible = false;
            this.MultiLineShellExecuteBtn.Click += new System.EventHandler(this.MultiLineShellExecuteBtn_Click);
            // 
            // InteractiveShellCtrlCBtn
            // 
            this.InteractiveShellCtrlCBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InteractiveShellCtrlCBtn.BackColor = System.Drawing.Color.Transparent;
            this.InteractiveShellCtrlCBtn.Enabled = false;
            this.InteractiveShellCtrlCBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InteractiveShellCtrlCBtn.ForeColor = System.Drawing.Color.Black;
            this.InteractiveShellCtrlCBtn.Location = new System.Drawing.Point(467, 3);
            this.InteractiveShellCtrlCBtn.Margin = new System.Windows.Forms.Padding(0);
            this.InteractiveShellCtrlCBtn.Name = "InteractiveShellCtrlCBtn";
            this.InteractiveShellCtrlCBtn.Size = new System.Drawing.Size(62, 20);
            this.InteractiveShellCtrlCBtn.TabIndex = 3;
            this.InteractiveShellCtrlCBtn.Text = "Ctrl + C";
            this.InteractiveShellCtrlCBtn.UseVisualStyleBackColor = false;
            this.InteractiveShellCtrlCBtn.Click += new System.EventHandler(this.InteractiveShellCtrlCBtn_Click);
            // 
            // ScriptingShellTabs
            // 
            this.ScriptingShellTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptingShellTabs.Controls.Add(this.InteractiveShellTP);
            this.ScriptingShellTabs.Controls.Add(this.MultiLineShellTP);
            this.ScriptingShellTabs.Controls.Add(this.ScriptedSendTP);
            this.ScriptingShellTabs.Location = new System.Drawing.Point(0, 25);
            this.ScriptingShellTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellTabs.Name = "ScriptingShellTabs";
            this.ScriptingShellTabs.Padding = new System.Drawing.Point(0, 0);
            this.ScriptingShellTabs.SelectedIndex = 0;
            this.ScriptingShellTabs.Size = new System.Drawing.Size(533, 461);
            this.ScriptingShellTabs.TabIndex = 3;
            this.ScriptingShellTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.ScriptingShellTabs_Selecting);
            // 
            // InteractiveShellTP
            // 
            this.InteractiveShellTP.BackColor = System.Drawing.Color.Black;
            this.InteractiveShellTP.Controls.Add(this.InteractiveShellPromptBox);
            this.InteractiveShellTP.Controls.Add(this.InteractiveShellOut);
            this.InteractiveShellTP.Controls.Add(this.InteractiveShellIn);
            this.InteractiveShellTP.Location = new System.Drawing.Point(4, 22);
            this.InteractiveShellTP.Margin = new System.Windows.Forms.Padding(0);
            this.InteractiveShellTP.Name = "InteractiveShellTP";
            this.InteractiveShellTP.Size = new System.Drawing.Size(525, 435);
            this.InteractiveShellTP.TabIndex = 0;
            this.InteractiveShellTP.Text = "Interactive Shell";
            // 
            // InteractiveShellPromptBox
            // 
            this.InteractiveShellPromptBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InteractiveShellPromptBox.BackColor = System.Drawing.Color.Black;
            this.InteractiveShellPromptBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InteractiveShellPromptBox.Font = new System.Drawing.Font("Lucida Console", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InteractiveShellPromptBox.ForeColor = System.Drawing.Color.Lime;
            this.InteractiveShellPromptBox.Location = new System.Drawing.Point(0, 419);
            this.InteractiveShellPromptBox.Margin = new System.Windows.Forms.Padding(0);
            this.InteractiveShellPromptBox.Name = "InteractiveShellPromptBox";
            this.InteractiveShellPromptBox.ReadOnly = true;
            this.InteractiveShellPromptBox.Size = new System.Drawing.Size(27, 13);
            this.InteractiveShellPromptBox.TabIndex = 2;
            this.InteractiveShellPromptBox.Text = ">>>>";
            this.InteractiveShellPromptBox.Enter += new System.EventHandler(this.InteractiveShellPromptBox_Enter);
            // 
            // InteractiveShellOut
            // 
            this.InteractiveShellOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.InteractiveShellOut.BackColor = System.Drawing.Color.Black;
            this.InteractiveShellOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InteractiveShellOut.Font = new System.Drawing.Font("Lucida Console", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InteractiveShellOut.ForeColor = System.Drawing.Color.Lime;
            this.InteractiveShellOut.Location = new System.Drawing.Point(0, 0);
            this.InteractiveShellOut.Margin = new System.Windows.Forms.Padding(0);
            this.InteractiveShellOut.MaxLength = 2147483647;
            this.InteractiveShellOut.Multiline = true;
            this.InteractiveShellOut.Name = "InteractiveShellOut";
            this.InteractiveShellOut.ReadOnly = true;
            this.InteractiveShellOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InteractiveShellOut.Size = new System.Drawing.Size(525, 412);
            this.InteractiveShellOut.TabIndex = 1;
            // 
            // InteractiveShellIn
            // 
            this.InteractiveShellIn.AcceptsReturn = true;
            this.InteractiveShellIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.InteractiveShellIn.BackColor = System.Drawing.Color.Black;
            this.InteractiveShellIn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InteractiveShellIn.Font = new System.Drawing.Font("Lucida Console", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InteractiveShellIn.ForeColor = System.Drawing.Color.Lime;
            this.InteractiveShellIn.Location = new System.Drawing.Point(27, 419);
            this.InteractiveShellIn.Margin = new System.Windows.Forms.Padding(0);
            this.InteractiveShellIn.Multiline = true;
            this.InteractiveShellIn.Name = "InteractiveShellIn";
            this.InteractiveShellIn.Size = new System.Drawing.Size(481, 13);
            this.InteractiveShellIn.TabIndex = 0;
            this.InteractiveShellIn.WordWrap = false;
            this.InteractiveShellIn.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InteractiveShellIn_KeyUp);
            // 
            // MultiLineShellTP
            // 
            this.MultiLineShellTP.Controls.Add(this.MultiLineShellInTE);
            this.MultiLineShellTP.Location = new System.Drawing.Point(4, 22);
            this.MultiLineShellTP.Margin = new System.Windows.Forms.Padding(0);
            this.MultiLineShellTP.Name = "MultiLineShellTP";
            this.MultiLineShellTP.Size = new System.Drawing.Size(525, 435);
            this.MultiLineShellTP.TabIndex = 1;
            this.MultiLineShellTP.Text = "Multi-Line Shell";
            this.MultiLineShellTP.UseVisualStyleBackColor = true;
            // 
            // MultiLineShellInTE
            // 
            this.MultiLineShellInTE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MultiLineShellInTE.IsIconBarVisible = false;
            this.MultiLineShellInTE.Location = new System.Drawing.Point(0, 0);
            this.MultiLineShellInTE.Margin = new System.Windows.Forms.Padding(0);
            this.MultiLineShellInTE.Name = "MultiLineShellInTE";
            this.MultiLineShellInTE.ShowEOLMarkers = true;
            this.MultiLineShellInTE.ShowSpaces = true;
            this.MultiLineShellInTE.ShowTabs = true;
            this.MultiLineShellInTE.ShowVRuler = true;
            this.MultiLineShellInTE.Size = new System.Drawing.Size(525, 435);
            this.MultiLineShellInTE.TabIndex = 4;
            // 
            // ScriptedSendTP
            // 
            this.ScriptedSendTP.Controls.Add(this.CustomSendErrorTB);
            this.ScriptedSendTP.Controls.Add(this.CustomSendTE);
            this.ScriptedSendTP.Controls.Add(this.CustomSendBottomRtb);
            this.ScriptedSendTP.Controls.Add(this.CustomSendTopRtb);
            this.ScriptedSendTP.Controls.Add(this.label1);
            this.ScriptedSendTP.Controls.Add(this.CustomSendActivateCB);
            this.ScriptedSendTP.Controls.Add(this.CustomSendRubyRB);
            this.ScriptedSendTP.Controls.Add(this.CustomSendPythonRB);
            this.ScriptedSendTP.Location = new System.Drawing.Point(4, 22);
            this.ScriptedSendTP.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptedSendTP.Name = "ScriptedSendTP";
            this.ScriptedSendTP.Size = new System.Drawing.Size(525, 435);
            this.ScriptedSendTP.TabIndex = 2;
            this.ScriptedSendTP.Text = "Scripted Send";
            this.ScriptedSendTP.UseVisualStyleBackColor = true;
            // 
            // CustomSendErrorTB
            // 
            this.CustomSendErrorTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CustomSendErrorTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CustomSendErrorTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CustomSendErrorTB.Location = new System.Drawing.Point(0, 371);
            this.CustomSendErrorTB.Multiline = true;
            this.CustomSendErrorTB.Name = "CustomSendErrorTB";
            this.CustomSendErrorTB.ReadOnly = true;
            this.CustomSendErrorTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CustomSendErrorTB.Size = new System.Drawing.Size(525, 64);
            this.CustomSendErrorTB.TabIndex = 15;
            this.CustomSendErrorTB.Visible = false;
            // 
            // CustomSendTE
            // 
            this.CustomSendTE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomSendTE.IsIconBarVisible = false;
            this.CustomSendTE.Location = new System.Drawing.Point(0, 74);
            this.CustomSendTE.Margin = new System.Windows.Forms.Padding(0);
            this.CustomSendTE.Name = "CustomSendTE";
            this.CustomSendTE.ShowEOLMarkers = true;
            this.CustomSendTE.ShowSpaces = true;
            this.CustomSendTE.ShowTabs = true;
            this.CustomSendTE.ShowVRuler = true;
            this.CustomSendTE.Size = new System.Drawing.Size(525, 244);
            this.CustomSendTE.TabIndex = 14;
            // 
            // CustomSendBottomRtb
            // 
            this.CustomSendBottomRtb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomSendBottomRtb.BackColor = System.Drawing.SystemColors.Window;
            this.CustomSendBottomRtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CustomSendBottomRtb.Location = new System.Drawing.Point(0, 318);
            this.CustomSendBottomRtb.Margin = new System.Windows.Forms.Padding(0);
            this.CustomSendBottomRtb.Name = "CustomSendBottomRtb";
            this.CustomSendBottomRtb.ReadOnly = true;
            this.CustomSendBottomRtb.Size = new System.Drawing.Size(525, 44);
            this.CustomSendBottomRtb.TabIndex = 10;
            this.CustomSendBottomRtb.Text = "     return res";
            // 
            // CustomSendTopRtb
            // 
            this.CustomSendTopRtb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomSendTopRtb.BackColor = System.Drawing.SystemColors.Window;
            this.CustomSendTopRtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CustomSendTopRtb.Location = new System.Drawing.Point(0, 54);
            this.CustomSendTopRtb.Margin = new System.Windows.Forms.Padding(0);
            this.CustomSendTopRtb.Name = "CustomSendTopRtb";
            this.CustomSendTopRtb.ReadOnly = true;
            this.CustomSendTopRtb.Size = new System.Drawing.Size(525, 20);
            this.CustomSendTopRtb.TabIndex = 9;
            this.CustomSendTopRtb.Text = "def ScriptedSend(req):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Language:";
            // 
            // CustomSendActivateCB
            // 
            this.CustomSendActivateCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomSendActivateCB.AutoSize = true;
            this.CustomSendActivateCB.Location = new System.Drawing.Point(421, 9);
            this.CustomSendActivateCB.Name = "CustomSendActivateCB";
            this.CustomSendActivateCB.Size = new System.Drawing.Size(95, 17);
            this.CustomSendActivateCB.TabIndex = 13;
            this.CustomSendActivateCB.Text = "Activate Script";
            this.CustomSendActivateCB.UseVisualStyleBackColor = true;
            this.CustomSendActivateCB.Click += new System.EventHandler(this.CustomSendActivateCB_Click);
            // 
            // CustomSendRubyRB
            // 
            this.CustomSendRubyRB.AutoSize = true;
            this.CustomSendRubyRB.Location = new System.Drawing.Point(132, 7);
            this.CustomSendRubyRB.Name = "CustomSendRubyRB";
            this.CustomSendRubyRB.Size = new System.Drawing.Size(50, 17);
            this.CustomSendRubyRB.TabIndex = 8;
            this.CustomSendRubyRB.Text = "Ruby";
            this.CustomSendRubyRB.UseVisualStyleBackColor = true;
            this.CustomSendRubyRB.CheckedChanged += new System.EventHandler(this.CustomSendRubyRB_CheckedChanged);
            // 
            // CustomSendPythonRB
            // 
            this.CustomSendPythonRB.AutoSize = true;
            this.CustomSendPythonRB.Checked = true;
            this.CustomSendPythonRB.Location = new System.Drawing.Point(71, 7);
            this.CustomSendPythonRB.Name = "CustomSendPythonRB";
            this.CustomSendPythonRB.Size = new System.Drawing.Size(58, 17);
            this.CustomSendPythonRB.TabIndex = 7;
            this.CustomSendPythonRB.TabStop = true;
            this.CustomSendPythonRB.Text = "Python";
            this.CustomSendPythonRB.UseVisualStyleBackColor = true;
            this.CustomSendPythonRB.CheckedChanged += new System.EventHandler(this.CustomSendPythonRB_CheckedChanged);
            // 
            // InteractiveShellRubyRB
            // 
            this.InteractiveShellRubyRB.AutoSize = true;
            this.InteractiveShellRubyRB.Location = new System.Drawing.Point(131, 1);
            this.InteractiveShellRubyRB.Name = "InteractiveShellRubyRB";
            this.InteractiveShellRubyRB.Size = new System.Drawing.Size(50, 17);
            this.InteractiveShellRubyRB.TabIndex = 5;
            this.InteractiveShellRubyRB.Text = "Ruby";
            this.InteractiveShellRubyRB.UseVisualStyleBackColor = true;
            this.InteractiveShellRubyRB.CheckedChanged += new System.EventHandler(this.InteractiveShellRubyRB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Language:";
            // 
            // InteractiveShellPythonRB
            // 
            this.InteractiveShellPythonRB.AutoSize = true;
            this.InteractiveShellPythonRB.Checked = true;
            this.InteractiveShellPythonRB.Location = new System.Drawing.Point(67, 1);
            this.InteractiveShellPythonRB.Name = "InteractiveShellPythonRB";
            this.InteractiveShellPythonRB.Size = new System.Drawing.Size(58, 17);
            this.InteractiveShellPythonRB.TabIndex = 4;
            this.InteractiveShellPythonRB.TabStop = true;
            this.InteractiveShellPythonRB.Text = "Python";
            this.InteractiveShellPythonRB.UseVisualStyleBackColor = true;
            this.InteractiveShellPythonRB.CheckedChanged += new System.EventHandler(this.InteractiveShellPythonRB_CheckedChanged);
            // 
            // ScriptingShellAPISplit
            // 
            this.ScriptingShellAPISplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptingShellAPISplit.Location = new System.Drawing.Point(0, 0);
            this.ScriptingShellAPISplit.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellAPISplit.Name = "ScriptingShellAPISplit";
            this.ScriptingShellAPISplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ScriptingShellAPISplit.Panel1
            // 
            this.ScriptingShellAPISplit.Panel1.Controls.Add(this.ScriptingShellAPITreeTabs);
            // 
            // ScriptingShellAPISplit.Panel2
            // 
            this.ScriptingShellAPISplit.Panel2.Controls.Add(this.ShellAPIDetailsRTB);
            this.ScriptingShellAPISplit.Size = new System.Drawing.Size(169, 487);
            this.ScriptingShellAPISplit.SplitterDistance = 233;
            this.ScriptingShellAPISplit.SplitterWidth = 2;
            this.ScriptingShellAPISplit.TabIndex = 0;
            // 
            // ScriptingShellAPITreeTabs
            // 
            this.ScriptingShellAPITreeTabs.Controls.Add(this.ScriptingShellAPITreePythonTab);
            this.ScriptingShellAPITreeTabs.Controls.Add(this.ScriptingShellAPITreeRubyTab);
            this.ScriptingShellAPITreeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptingShellAPITreeTabs.Location = new System.Drawing.Point(0, 0);
            this.ScriptingShellAPITreeTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellAPITreeTabs.Name = "ScriptingShellAPITreeTabs";
            this.ScriptingShellAPITreeTabs.Padding = new System.Drawing.Point(0, 0);
            this.ScriptingShellAPITreeTabs.SelectedIndex = 0;
            this.ScriptingShellAPITreeTabs.Size = new System.Drawing.Size(169, 233);
            this.ScriptingShellAPITreeTabs.TabIndex = 0;
            // 
            // ScriptingShellAPITreePythonTab
            // 
            this.ScriptingShellAPITreePythonTab.Controls.Add(this.ScriptingShellPythonAPITree);
            this.ScriptingShellAPITreePythonTab.Location = new System.Drawing.Point(4, 22);
            this.ScriptingShellAPITreePythonTab.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellAPITreePythonTab.Name = "ScriptingShellAPITreePythonTab";
            this.ScriptingShellAPITreePythonTab.Size = new System.Drawing.Size(161, 207);
            this.ScriptingShellAPITreePythonTab.TabIndex = 0;
            this.ScriptingShellAPITreePythonTab.Text = "Python";
            this.ScriptingShellAPITreePythonTab.UseVisualStyleBackColor = true;
            // 
            // ScriptingShellPythonAPITree
            // 
            this.ScriptingShellPythonAPITree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScriptingShellPythonAPITree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptingShellPythonAPITree.Location = new System.Drawing.Point(0, 0);
            this.ScriptingShellPythonAPITree.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellPythonAPITree.Name = "ScriptingShellPythonAPITree";
            this.ScriptingShellPythonAPITree.Size = new System.Drawing.Size(161, 207);
            this.ScriptingShellPythonAPITree.TabIndex = 0;
            this.ScriptingShellPythonAPITree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ScriptingShellPythonAPITree_AfterSelect);
            // 
            // ScriptingShellAPITreeRubyTab
            // 
            this.ScriptingShellAPITreeRubyTab.Controls.Add(this.ScriptingShellRubyAPITree);
            this.ScriptingShellAPITreeRubyTab.Location = new System.Drawing.Point(4, 22);
            this.ScriptingShellAPITreeRubyTab.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellAPITreeRubyTab.Name = "ScriptingShellAPITreeRubyTab";
            this.ScriptingShellAPITreeRubyTab.Size = new System.Drawing.Size(161, 207);
            this.ScriptingShellAPITreeRubyTab.TabIndex = 1;
            this.ScriptingShellAPITreeRubyTab.Text = "Ruby";
            this.ScriptingShellAPITreeRubyTab.UseVisualStyleBackColor = true;
            // 
            // ScriptingShellRubyAPITree
            // 
            this.ScriptingShellRubyAPITree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScriptingShellRubyAPITree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptingShellRubyAPITree.Location = new System.Drawing.Point(0, 0);
            this.ScriptingShellRubyAPITree.Margin = new System.Windows.Forms.Padding(0);
            this.ScriptingShellRubyAPITree.Name = "ScriptingShellRubyAPITree";
            this.ScriptingShellRubyAPITree.Size = new System.Drawing.Size(161, 207);
            this.ScriptingShellRubyAPITree.TabIndex = 1;
            this.ScriptingShellRubyAPITree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ScriptingShellRubyAPITree_AfterSelect);
            // 
            // ShellAPIDetailsRTB
            // 
            this.ShellAPIDetailsRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShellAPIDetailsRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShellAPIDetailsRTB.Location = new System.Drawing.Point(0, 0);
            this.ShellAPIDetailsRTB.Margin = new System.Windows.Forms.Padding(0);
            this.ShellAPIDetailsRTB.Name = "ShellAPIDetailsRTB";
            this.ShellAPIDetailsRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ShellAPIDetailsRTB.Size = new System.Drawing.Size(169, 252);
            this.ShellAPIDetailsRTB.TabIndex = 0;
            this.ShellAPIDetailsRTB.Text = "";
            // 
            // MTJavaScriptTaintTP
            // 
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintShowSourceToSinkCB);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintShowLinesLbl);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintShowSinkCB);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintShowSourceCB);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintShowCleanCB);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintConfigPanel);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintContinueBtn);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintConfigShowHideBtn);
            this.MTJavaScriptTaintTP.Controls.Add(this.PauseAtTaintCB);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintStatusTB);
            this.MTJavaScriptTaintTP.Controls.Add(this.TaintTraceResultSinkLegendTB);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintTabs);
            this.MTJavaScriptTaintTP.Controls.Add(this.JSTaintTraceControlBtn);
            this.MTJavaScriptTaintTP.Controls.Add(this.TaintTraceResultSourceToSinkLegendTB);
            this.MTJavaScriptTaintTP.Controls.Add(this.TaintTraceResultSourceLegendTB);
            this.MTJavaScriptTaintTP.Controls.Add(this.TaintTraceResultSourcePlusSinkLegendTB);
            this.MTJavaScriptTaintTP.Location = new System.Drawing.Point(4, 22);
            this.MTJavaScriptTaintTP.Name = "MTJavaScriptTaintTP";
            this.MTJavaScriptTaintTP.Size = new System.Drawing.Size(697, 486);
            this.MTJavaScriptTaintTP.TabIndex = 6;
            this.MTJavaScriptTaintTP.Text = "Test JavaScript";
            this.MTJavaScriptTaintTP.UseVisualStyleBackColor = true;
            // 
            // JSTaintShowSourceToSinkCB
            // 
            this.JSTaintShowSourceToSinkCB.AutoSize = true;
            this.JSTaintShowSourceToSinkCB.Checked = true;
            this.JSTaintShowSourceToSinkCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.JSTaintShowSourceToSinkCB.Location = new System.Drawing.Point(241, 50);
            this.JSTaintShowSourceToSinkCB.Name = "JSTaintShowSourceToSinkCB";
            this.JSTaintShowSourceToSinkCB.Size = new System.Drawing.Size(100, 17);
            this.JSTaintShowSourceToSinkCB.TabIndex = 16;
            this.JSTaintShowSourceToSinkCB.Text = "Source To Sink";
            this.JSTaintShowSourceToSinkCB.UseVisualStyleBackColor = true;
            this.JSTaintShowSourceToSinkCB.Visible = false;
            this.JSTaintShowSourceToSinkCB.CheckedChanged += new System.EventHandler(this.JSTaintShowSourceToSinkCB_CheckedChanged);
            // 
            // JSTaintShowLinesLbl
            // 
            this.JSTaintShowLinesLbl.AutoSize = true;
            this.JSTaintShowLinesLbl.Location = new System.Drawing.Point(4, 49);
            this.JSTaintShowLinesLbl.Name = "JSTaintShowLinesLbl";
            this.JSTaintShowLinesLbl.Size = new System.Drawing.Size(65, 13);
            this.JSTaintShowLinesLbl.TabIndex = 15;
            this.JSTaintShowLinesLbl.Text = "Show Lines:";
            this.JSTaintShowLinesLbl.Visible = false;
            // 
            // JSTaintShowSinkCB
            // 
            this.JSTaintShowSinkCB.AutoSize = true;
            this.JSTaintShowSinkCB.Checked = true;
            this.JSTaintShowSinkCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.JSTaintShowSinkCB.Location = new System.Drawing.Point(190, 50);
            this.JSTaintShowSinkCB.Name = "JSTaintShowSinkCB";
            this.JSTaintShowSinkCB.Size = new System.Drawing.Size(47, 17);
            this.JSTaintShowSinkCB.TabIndex = 14;
            this.JSTaintShowSinkCB.Text = "Sink";
            this.JSTaintShowSinkCB.UseVisualStyleBackColor = true;
            this.JSTaintShowSinkCB.Visible = false;
            this.JSTaintShowSinkCB.CheckedChanged += new System.EventHandler(this.JSTaintShowSinkCB_CheckedChanged);
            // 
            // JSTaintShowSourceCB
            // 
            this.JSTaintShowSourceCB.AutoSize = true;
            this.JSTaintShowSourceCB.Checked = true;
            this.JSTaintShowSourceCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.JSTaintShowSourceCB.Location = new System.Drawing.Point(126, 50);
            this.JSTaintShowSourceCB.Name = "JSTaintShowSourceCB";
            this.JSTaintShowSourceCB.Size = new System.Drawing.Size(60, 17);
            this.JSTaintShowSourceCB.TabIndex = 13;
            this.JSTaintShowSourceCB.Text = "Source";
            this.JSTaintShowSourceCB.UseVisualStyleBackColor = true;
            this.JSTaintShowSourceCB.Visible = false;
            this.JSTaintShowSourceCB.CheckedChanged += new System.EventHandler(this.JSTaintShowSourceCB_CheckedChanged);
            // 
            // JSTaintShowCleanCB
            // 
            this.JSTaintShowCleanCB.AutoSize = true;
            this.JSTaintShowCleanCB.Checked = true;
            this.JSTaintShowCleanCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.JSTaintShowCleanCB.Location = new System.Drawing.Point(69, 50);
            this.JSTaintShowCleanCB.Name = "JSTaintShowCleanCB";
            this.JSTaintShowCleanCB.Size = new System.Drawing.Size(53, 17);
            this.JSTaintShowCleanCB.TabIndex = 12;
            this.JSTaintShowCleanCB.Text = "Clean";
            this.JSTaintShowCleanCB.UseVisualStyleBackColor = true;
            this.JSTaintShowCleanCB.Visible = false;
            this.JSTaintShowCleanCB.CheckedChanged += new System.EventHandler(this.JSTaintShowCleanCB_CheckedChanged);
            // 
            // JSTaintConfigPanel
            // 
            this.JSTaintConfigPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.JSTaintConfigPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JSTaintConfigPanel.Controls.Add(this.TaintTraceClearTaintConfigBtn);
            this.JSTaintConfigPanel.Controls.Add(this.JSTaintConfigGrid);
            this.JSTaintConfigPanel.Controls.Add(this.TaintTraceResetTaintConfigBtn);
            this.JSTaintConfigPanel.Location = new System.Drawing.Point(0, 32);
            this.JSTaintConfigPanel.Name = "JSTaintConfigPanel";
            this.JSTaintConfigPanel.Size = new System.Drawing.Size(697, 10);
            this.JSTaintConfigPanel.TabIndex = 11;
            this.JSTaintConfigPanel.Visible = false;
            // 
            // TaintTraceClearTaintConfigBtn
            // 
            this.TaintTraceClearTaintConfigBtn.Location = new System.Drawing.Point(114, 8);
            this.TaintTraceClearTaintConfigBtn.Name = "TaintTraceClearTaintConfigBtn";
            this.TaintTraceClearTaintConfigBtn.Size = new System.Drawing.Size(55, 23);
            this.TaintTraceClearTaintConfigBtn.TabIndex = 10;
            this.TaintTraceClearTaintConfigBtn.Text = "Clear All";
            this.TaintTraceClearTaintConfigBtn.UseVisualStyleBackColor = true;
            this.TaintTraceClearTaintConfigBtn.Click += new System.EventHandler(this.TaintTraceClearTaintConfigBtn_Click);
            // 
            // JSTaintConfigGrid
            // 
            this.JSTaintConfigGrid.AllowUserToResizeRows = false;
            this.JSTaintConfigGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.JSTaintConfigGrid.BackgroundColor = System.Drawing.Color.White;
            this.JSTaintConfigGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JSTaintConfigGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JSTaintConfigGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JSTaintDefaultSourceObjectsColumn,
            this.JSTaintDefaultSinkObjectsColumn,
            this.JSTaintDefaultArgumentAssignedASourceMethodsColumn,
            this.JSTaintDefaultArgumentAssignedToSinkMethodsColumn,
            this.JSTaintDefaultSourceReturningMethodsColumn,
            this.JSTaintDefaultSinkReturningMethodsColumn,
            this.JSTaintDefaultArgumentReturningMethodsColumn});
            this.JSTaintConfigGrid.GridColor = System.Drawing.Color.White;
            this.JSTaintConfigGrid.Location = new System.Drawing.Point(0, 45);
            this.JSTaintConfigGrid.Name = "JSTaintConfigGrid";
            this.JSTaintConfigGrid.Size = new System.Drawing.Size(695, 0);
            this.JSTaintConfigGrid.TabIndex = 9;
            // 
            // JSTaintDefaultSourceObjectsColumn
            // 
            this.JSTaintDefaultSourceObjectsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JSTaintDefaultSourceObjectsColumn.HeaderText = "Source Objects";
            this.JSTaintDefaultSourceObjectsColumn.Name = "JSTaintDefaultSourceObjectsColumn";
            // 
            // JSTaintDefaultSinkObjectsColumn
            // 
            this.JSTaintDefaultSinkObjectsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JSTaintDefaultSinkObjectsColumn.HeaderText = "Sink Objects";
            this.JSTaintDefaultSinkObjectsColumn.Name = "JSTaintDefaultSinkObjectsColumn";
            // 
            // JSTaintDefaultArgumentAssignedASourceMethodsColumn
            // 
            this.JSTaintDefaultArgumentAssignedASourceMethodsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JSTaintDefaultArgumentAssignedASourceMethodsColumn.HeaderText = "Argument Assigned A Source Methods";
            this.JSTaintDefaultArgumentAssignedASourceMethodsColumn.Name = "JSTaintDefaultArgumentAssignedASourceMethodsColumn";
            // 
            // JSTaintDefaultArgumentAssignedToSinkMethodsColumn
            // 
            this.JSTaintDefaultArgumentAssignedToSinkMethodsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JSTaintDefaultArgumentAssignedToSinkMethodsColumn.HeaderText = "Argument Assigned To Sink Methods";
            this.JSTaintDefaultArgumentAssignedToSinkMethodsColumn.Name = "JSTaintDefaultArgumentAssignedToSinkMethodsColumn";
            // 
            // JSTaintDefaultSourceReturningMethodsColumn
            // 
            this.JSTaintDefaultSourceReturningMethodsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JSTaintDefaultSourceReturningMethodsColumn.HeaderText = "Source Returning Methods";
            this.JSTaintDefaultSourceReturningMethodsColumn.Name = "JSTaintDefaultSourceReturningMethodsColumn";
            // 
            // JSTaintDefaultSinkReturningMethodsColumn
            // 
            this.JSTaintDefaultSinkReturningMethodsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JSTaintDefaultSinkReturningMethodsColumn.HeaderText = "Sink Returning Methods";
            this.JSTaintDefaultSinkReturningMethodsColumn.Name = "JSTaintDefaultSinkReturningMethodsColumn";
            // 
            // JSTaintDefaultArgumentReturningMethodsColumn
            // 
            this.JSTaintDefaultArgumentReturningMethodsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JSTaintDefaultArgumentReturningMethodsColumn.HeaderText = "Argument Returning Methods";
            this.JSTaintDefaultArgumentReturningMethodsColumn.Name = "JSTaintDefaultArgumentReturningMethodsColumn";
            // 
            // TaintTraceResetTaintConfigBtn
            // 
            this.TaintTraceResetTaintConfigBtn.Location = new System.Drawing.Point(3, 8);
            this.TaintTraceResetTaintConfigBtn.Name = "TaintTraceResetTaintConfigBtn";
            this.TaintTraceResetTaintConfigBtn.Size = new System.Drawing.Size(105, 23);
            this.TaintTraceResetTaintConfigBtn.TabIndex = 3;
            this.TaintTraceResetTaintConfigBtn.Text = "Reset To Default";
            this.TaintTraceResetTaintConfigBtn.UseVisualStyleBackColor = true;
            this.TaintTraceResetTaintConfigBtn.Click += new System.EventHandler(this.TaintTraceResetTaintConfigBtn_Click);
            // 
            // JSTaintContinueBtn
            // 
            this.JSTaintContinueBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("JSTaintContinueBtn.BackgroundImage")));
            this.JSTaintContinueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.JSTaintContinueBtn.Location = new System.Drawing.Point(242, 5);
            this.JSTaintContinueBtn.Name = "JSTaintContinueBtn";
            this.JSTaintContinueBtn.Size = new System.Drawing.Size(22, 23);
            this.JSTaintContinueBtn.TabIndex = 10;
            this.JSTaintContinueBtn.UseVisualStyleBackColor = true;
            this.JSTaintContinueBtn.Visible = false;
            this.JSTaintContinueBtn.Click += new System.EventHandler(this.JSTaintContinueBtn_Click);
            // 
            // JSTaintConfigShowHideBtn
            // 
            this.JSTaintConfigShowHideBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JSTaintConfigShowHideBtn.Location = new System.Drawing.Point(586, 5);
            this.JSTaintConfigShowHideBtn.Name = "JSTaintConfigShowHideBtn";
            this.JSTaintConfigShowHideBtn.Size = new System.Drawing.Size(105, 23);
            this.JSTaintConfigShowHideBtn.TabIndex = 4;
            this.JSTaintConfigShowHideBtn.Text = "Show Taint Config";
            this.JSTaintConfigShowHideBtn.UseVisualStyleBackColor = true;
            this.JSTaintConfigShowHideBtn.Click += new System.EventHandler(this.JSTaintConfigShowHideBtn_Click);
            // 
            // PauseAtTaintCB
            // 
            this.PauseAtTaintCB.AutoSize = true;
            this.PauseAtTaintCB.Location = new System.Drawing.Point(142, 9);
            this.PauseAtTaintCB.Name = "PauseAtTaintCB";
            this.PauseAtTaintCB.Size = new System.Drawing.Size(95, 17);
            this.PauseAtTaintCB.TabIndex = 9;
            this.PauseAtTaintCB.Text = "Pause at Taint";
            this.PauseAtTaintCB.UseVisualStyleBackColor = true;
            this.PauseAtTaintCB.CheckedChanged += new System.EventHandler(this.PauseAtTaintCB_CheckedChanged);
            this.PauseAtTaintCB.Click += new System.EventHandler(this.PauseAtTaintCB_Click);
            // 
            // JSTaintStatusTB
            // 
            this.JSTaintStatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.JSTaintStatusTB.BackColor = System.Drawing.SystemColors.Window;
            this.JSTaintStatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JSTaintStatusTB.Location = new System.Drawing.Point(356, 31);
            this.JSTaintStatusTB.Margin = new System.Windows.Forms.Padding(0);
            this.JSTaintStatusTB.Multiline = true;
            this.JSTaintStatusTB.Name = "JSTaintStatusTB";
            this.JSTaintStatusTB.ReadOnly = true;
            this.JSTaintStatusTB.Size = new System.Drawing.Size(335, 36);
            this.JSTaintStatusTB.TabIndex = 4;
            // 
            // TaintTraceResultSinkLegendTB
            // 
            this.TaintTraceResultSinkLegendTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TaintTraceResultSinkLegendTB.BackColor = System.Drawing.Color.HotPink;
            this.TaintTraceResultSinkLegendTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaintTraceResultSinkLegendTB.Location = new System.Drawing.Point(388, 11);
            this.TaintTraceResultSinkLegendTB.Name = "TaintTraceResultSinkLegendTB";
            this.TaintTraceResultSinkLegendTB.ReadOnly = true;
            this.TaintTraceResultSinkLegendTB.Size = new System.Drawing.Size(27, 13);
            this.TaintTraceResultSinkLegendTB.TabIndex = 5;
            this.TaintTraceResultSinkLegendTB.Text = "Sink";
            this.TaintTraceResultSinkLegendTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaintTraceResultSinkLegendTB.Visible = false;
            // 
            // JSTaintTabs
            // 
            this.JSTaintTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.JSTaintTabs.Controls.Add(this.JSTaintInputTab);
            this.JSTaintTabs.Controls.Add(this.JSTaintResultTab);
            this.JSTaintTabs.Location = new System.Drawing.Point(0, 70);
            this.JSTaintTabs.Margin = new System.Windows.Forms.Padding(0);
            this.JSTaintTabs.Multiline = true;
            this.JSTaintTabs.Name = "JSTaintTabs";
            this.JSTaintTabs.Padding = new System.Drawing.Point(0, 0);
            this.JSTaintTabs.SelectedIndex = 0;
            this.JSTaintTabs.Size = new System.Drawing.Size(697, 416);
            this.JSTaintTabs.TabIndex = 2;
            this.JSTaintTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.JSTaintTabs_Selected);
            // 
            // JSTaintInputTab
            // 
            this.JSTaintInputTab.Controls.Add(this.JSTaintTraceInRTB);
            this.JSTaintInputTab.Controls.Add(this.TaintTraceMsgTB);
            this.JSTaintInputTab.Location = new System.Drawing.Point(4, 22);
            this.JSTaintInputTab.Margin = new System.Windows.Forms.Padding(0);
            this.JSTaintInputTab.Name = "JSTaintInputTab";
            this.JSTaintInputTab.Size = new System.Drawing.Size(689, 390);
            this.JSTaintInputTab.TabIndex = 0;
            this.JSTaintInputTab.Text = "JavaScript Input";
            this.JSTaintInputTab.UseVisualStyleBackColor = true;
            // 
            // JSTaintTraceInRTB
            // 
            this.JSTaintTraceInRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.JSTaintTraceInRTB.BackColor = System.Drawing.Color.White;
            this.JSTaintTraceInRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JSTaintTraceInRTB.DetectUrls = false;
            this.JSTaintTraceInRTB.Location = new System.Drawing.Point(0, 14);
            this.JSTaintTraceInRTB.Margin = new System.Windows.Forms.Padding(0);
            this.JSTaintTraceInRTB.Name = "JSTaintTraceInRTB";
            this.JSTaintTraceInRTB.Size = new System.Drawing.Size(689, 376);
            this.JSTaintTraceInRTB.TabIndex = 0;
            this.JSTaintTraceInRTB.Text = "";
            // 
            // TaintTraceMsgTB
            // 
            this.TaintTraceMsgTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TaintTraceMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaintTraceMsgTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.TaintTraceMsgTB.Location = new System.Drawing.Point(0, 0);
            this.TaintTraceMsgTB.Margin = new System.Windows.Forms.Padding(0);
            this.TaintTraceMsgTB.Name = "TaintTraceMsgTB";
            this.TaintTraceMsgTB.ReadOnly = true;
            this.TaintTraceMsgTB.Size = new System.Drawing.Size(689, 13);
            this.TaintTraceMsgTB.TabIndex = 1;
            this.TaintTraceMsgTB.Text = "Enter JavaScript/HTML with inline JS below and hit \'Start Taint Trace\'";
            // 
            // JSTaintResultTab
            // 
            this.JSTaintResultTab.Controls.Add(this.JSTaintResultSplit);
            this.JSTaintResultTab.Location = new System.Drawing.Point(4, 22);
            this.JSTaintResultTab.Margin = new System.Windows.Forms.Padding(0);
            this.JSTaintResultTab.Name = "JSTaintResultTab";
            this.JSTaintResultTab.Size = new System.Drawing.Size(689, 390);
            this.JSTaintResultTab.TabIndex = 1;
            this.JSTaintResultTab.Text = "Taint Trace Result";
            this.JSTaintResultTab.UseVisualStyleBackColor = true;
            // 
            // JSTaintResultSplit
            // 
            this.JSTaintResultSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JSTaintResultSplit.Location = new System.Drawing.Point(0, 0);
            this.JSTaintResultSplit.Margin = new System.Windows.Forms.Padding(0);
            this.JSTaintResultSplit.Name = "JSTaintResultSplit";
            this.JSTaintResultSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // JSTaintResultSplit.Panel1
            // 
            this.JSTaintResultSplit.Panel1.Controls.Add(this.JSTaintResultGrid);
            // 
            // JSTaintResultSplit.Panel2
            // 
            this.JSTaintResultSplit.Panel2.Controls.Add(this.JSTaintReasonsRTB);
            this.JSTaintResultSplit.Size = new System.Drawing.Size(689, 390);
            this.JSTaintResultSplit.SplitterDistance = 266;
            this.JSTaintResultSplit.SplitterWidth = 2;
            this.JSTaintResultSplit.TabIndex = 0;
            // 
            // JSTaintResultGrid
            // 
            this.JSTaintResultGrid.AllowUserToAddRows = false;
            this.JSTaintResultGrid.AllowUserToDeleteRows = false;
            this.JSTaintResultGrid.AllowUserToOrderColumns = true;
            this.JSTaintResultGrid.AllowUserToResizeRows = false;
            this.JSTaintResultGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.JSTaintResultGrid.BackgroundColor = System.Drawing.Color.White;
            this.JSTaintResultGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JSTaintResultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.JSTaintResultGrid.ColumnHeadersVisible = false;
            this.JSTaintResultGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JSTaintResultLineColumn,
            this.JSTaintResultCodeColumn});
            this.JSTaintResultGrid.ContextMenuStrip = this.JSTainTraceEditMenu;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.JSTaintResultGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.JSTaintResultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JSTaintResultGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.JSTaintResultGrid.GridColor = System.Drawing.Color.White;
            this.JSTaintResultGrid.Location = new System.Drawing.Point(0, 0);
            this.JSTaintResultGrid.Margin = new System.Windows.Forms.Padding(0);
            this.JSTaintResultGrid.MultiSelect = false;
            this.JSTaintResultGrid.Name = "JSTaintResultGrid";
            this.JSTaintResultGrid.ReadOnly = true;
            this.JSTaintResultGrid.RowHeadersVisible = false;
            this.JSTaintResultGrid.RowHeadersWidth = 10;
            this.JSTaintResultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.JSTaintResultGrid.Size = new System.Drawing.Size(689, 266);
            this.JSTaintResultGrid.TabIndex = 3;
            this.JSTaintResultGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.JSTaintResultGrid_CellClick);
            // 
            // JSTaintResultLineColumn
            // 
            this.JSTaintResultLineColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.JSTaintResultLineColumn.HeaderText = "LINE";
            this.JSTaintResultLineColumn.MinimumWidth = 50;
            this.JSTaintResultLineColumn.Name = "JSTaintResultLineColumn";
            this.JSTaintResultLineColumn.ReadOnly = true;
            this.JSTaintResultLineColumn.Width = 50;
            // 
            // JSTaintResultCodeColumn
            // 
            this.JSTaintResultCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.JSTaintResultCodeColumn.HeaderText = "CODE";
            this.JSTaintResultCodeColumn.Name = "JSTaintResultCodeColumn";
            this.JSTaintResultCodeColumn.ReadOnly = true;
            this.JSTaintResultCodeColumn.Width = 5;
            // 
            // JSTainTraceEditMenu
            // 
            this.JSTainTraceEditMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSourceTaintToolStripMenuItem,
            this.AddSinkTaintToolStripMenuItem,
            this.RemoveSourceTaintToolStripMenuItem,
            this.RemoveSinkTaintToolStripMenuItem,
            this.CopyLineTaintToolStripMenuItem});
            this.JSTainTraceEditMenu.Name = "JSTainTraceEditMenu";
            this.JSTainTraceEditMenu.Size = new System.Drawing.Size(187, 114);
            this.JSTainTraceEditMenu.Opening += new System.ComponentModel.CancelEventHandler(this.JSTainTraceEditMenu_Opening);
            // 
            // AddSourceTaintToolStripMenuItem
            // 
            this.AddSourceTaintToolStripMenuItem.Name = "AddSourceTaintToolStripMenuItem";
            this.AddSourceTaintToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.AddSourceTaintToolStripMenuItem.Text = "Set Source Taint";
            this.AddSourceTaintToolStripMenuItem.Click += new System.EventHandler(this.AddSourceTaintToolStripMenuItem_Click);
            // 
            // AddSinkTaintToolStripMenuItem
            // 
            this.AddSinkTaintToolStripMenuItem.Name = "AddSinkTaintToolStripMenuItem";
            this.AddSinkTaintToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.AddSinkTaintToolStripMenuItem.Text = "Set Sink Taint";
            this.AddSinkTaintToolStripMenuItem.Click += new System.EventHandler(this.AddSinkTaintToolStripMenuItem_Click);
            // 
            // RemoveSourceTaintToolStripMenuItem
            // 
            this.RemoveSourceTaintToolStripMenuItem.Name = "RemoveSourceTaintToolStripMenuItem";
            this.RemoveSourceTaintToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.RemoveSourceTaintToolStripMenuItem.Text = "Remove Source Taint";
            this.RemoveSourceTaintToolStripMenuItem.Click += new System.EventHandler(this.RemoveSourceTaintToolStripMenuItem_Click);
            // 
            // RemoveSinkTaintToolStripMenuItem
            // 
            this.RemoveSinkTaintToolStripMenuItem.Name = "RemoveSinkTaintToolStripMenuItem";
            this.RemoveSinkTaintToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.RemoveSinkTaintToolStripMenuItem.Text = "Remove Sink Taint";
            this.RemoveSinkTaintToolStripMenuItem.Click += new System.EventHandler(this.RemoveSinkTaintToolStripMenuItem_Click);
            // 
            // CopyLineTaintToolStripMenuItem
            // 
            this.CopyLineTaintToolStripMenuItem.Name = "CopyLineTaintToolStripMenuItem";
            this.CopyLineTaintToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.CopyLineTaintToolStripMenuItem.Text = "Copy Line";
            this.CopyLineTaintToolStripMenuItem.Click += new System.EventHandler(this.CopyLineTaintToolStripMenuItem_Click);
            // 
            // JSTaintReasonsRTB
            // 
            this.JSTaintReasonsRTB.BackColor = System.Drawing.Color.White;
            this.JSTaintReasonsRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JSTaintReasonsRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JSTaintReasonsRTB.Location = new System.Drawing.Point(0, 0);
            this.JSTaintReasonsRTB.Name = "JSTaintReasonsRTB";
            this.JSTaintReasonsRTB.ReadOnly = true;
            this.JSTaintReasonsRTB.Size = new System.Drawing.Size(689, 122);
            this.JSTaintReasonsRTB.TabIndex = 0;
            this.JSTaintReasonsRTB.Text = "";
            // 
            // JSTaintTraceControlBtn
            // 
            this.JSTaintTraceControlBtn.Location = new System.Drawing.Point(7, 5);
            this.JSTaintTraceControlBtn.Name = "JSTaintTraceControlBtn";
            this.JSTaintTraceControlBtn.Size = new System.Drawing.Size(105, 23);
            this.JSTaintTraceControlBtn.TabIndex = 0;
            this.JSTaintTraceControlBtn.Text = "Start Taint Trace";
            this.JSTaintTraceControlBtn.UseVisualStyleBackColor = true;
            this.JSTaintTraceControlBtn.Click += new System.EventHandler(this.JSTaintTraceControlBtn_Click);
            // 
            // TaintTraceResultSourceToSinkLegendTB
            // 
            this.TaintTraceResultSourceToSinkLegendTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TaintTraceResultSourceToSinkLegendTB.BackColor = System.Drawing.Color.Red;
            this.TaintTraceResultSourceToSinkLegendTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaintTraceResultSourceToSinkLegendTB.Location = new System.Drawing.Point(496, 11);
            this.TaintTraceResultSourceToSinkLegendTB.Name = "TaintTraceResultSourceToSinkLegendTB";
            this.TaintTraceResultSourceToSinkLegendTB.ReadOnly = true;
            this.TaintTraceResultSourceToSinkLegendTB.Size = new System.Drawing.Size(79, 13);
            this.TaintTraceResultSourceToSinkLegendTB.TabIndex = 8;
            this.TaintTraceResultSourceToSinkLegendTB.Text = "Source To Sink";
            this.TaintTraceResultSourceToSinkLegendTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaintTraceResultSourceToSinkLegendTB.Visible = false;
            // 
            // TaintTraceResultSourceLegendTB
            // 
            this.TaintTraceResultSourceLegendTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TaintTraceResultSourceLegendTB.BackColor = System.Drawing.Color.Orange;
            this.TaintTraceResultSourceLegendTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaintTraceResultSourceLegendTB.Location = new System.Drawing.Point(345, 11);
            this.TaintTraceResultSourceLegendTB.Name = "TaintTraceResultSourceLegendTB";
            this.TaintTraceResultSourceLegendTB.ReadOnly = true;
            this.TaintTraceResultSourceLegendTB.Size = new System.Drawing.Size(39, 13);
            this.TaintTraceResultSourceLegendTB.TabIndex = 6;
            this.TaintTraceResultSourceLegendTB.Text = "Source";
            this.TaintTraceResultSourceLegendTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaintTraceResultSourceLegendTB.Visible = false;
            // 
            // TaintTraceResultSourcePlusSinkLegendTB
            // 
            this.TaintTraceResultSourcePlusSinkLegendTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TaintTraceResultSourcePlusSinkLegendTB.BackColor = System.Drawing.Color.IndianRed;
            this.TaintTraceResultSourcePlusSinkLegendTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaintTraceResultSourcePlusSinkLegendTB.Location = new System.Drawing.Point(419, 11);
            this.TaintTraceResultSourcePlusSinkLegendTB.Name = "TaintTraceResultSourcePlusSinkLegendTB";
            this.TaintTraceResultSourcePlusSinkLegendTB.ReadOnly = true;
            this.TaintTraceResultSourcePlusSinkLegendTB.Size = new System.Drawing.Size(74, 13);
            this.TaintTraceResultSourcePlusSinkLegendTB.TabIndex = 7;
            this.TaintTraceResultSourcePlusSinkLegendTB.Text = "Source & Sink";
            this.TaintTraceResultSourcePlusSinkLegendTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaintTraceResultSourcePlusSinkLegendTB.Visible = false;
            // 
            // mt_proxy
            // 
            this.mt_proxy.Controls.Add(this.ProxyOptionsBtn);
            this.mt_proxy.Controls.Add(this.ConfigLoopBackOnlyCB);
            this.mt_proxy.Controls.Add(this.ViewProxyLogLink);
            this.mt_proxy.Controls.Add(this.ConfigProxyListenPortTB);
            this.mt_proxy.Controls.Add(this.label7);
            this.mt_proxy.Controls.Add(this.ConfigSetAsSystemProxyCB);
            this.mt_proxy.Controls.Add(this.ConfigProxyRunBtn);
            this.mt_proxy.Controls.Add(this.ProxyDropBtn);
            this.mt_proxy.Controls.Add(this.ProxySendBtn);
            this.mt_proxy.Controls.Add(this.ProxyExceptionTB);
            this.mt_proxy.Controls.Add(this.InterceptResponseCB);
            this.mt_proxy.Controls.Add(this.ProxyInterceptTabs);
            this.mt_proxy.Controls.Add(this.InterceptRequestCB);
            this.mt_proxy.Location = new System.Drawing.Point(4, 22);
            this.mt_proxy.Margin = new System.Windows.Forms.Padding(0);
            this.mt_proxy.Name = "mt_proxy";
            this.mt_proxy.Size = new System.Drawing.Size(705, 512);
            this.mt_proxy.TabIndex = 1;
            this.mt_proxy.Text = "Proxy";
            this.mt_proxy.UseVisualStyleBackColor = true;
            // 
            // ProxyOptionsBtn
            // 
            this.ProxyOptionsBtn.ContextMenuStrip = this.LogMenu;
            this.ProxyOptionsBtn.Location = new System.Drawing.Point(321, 19);
            this.ProxyOptionsBtn.Name = "ProxyOptionsBtn";
            this.ProxyOptionsBtn.Size = new System.Drawing.Size(47, 19);
            this.ProxyOptionsBtn.TabIndex = 16;
            this.ProxyOptionsBtn.Text = "\\/";
            this.ProxyOptionsBtn.UseVisualStyleBackColor = true;
            this.ProxyOptionsBtn.Click += new System.EventHandler(this.ProxyOptionsBtn_Click);
            // 
            // ConfigLoopBackOnlyCB
            // 
            this.ConfigLoopBackOnlyCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigLoopBackOnlyCB.AutoSize = true;
            this.ConfigLoopBackOnlyCB.Checked = true;
            this.ConfigLoopBackOnlyCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigLoopBackOnlyCB.Location = new System.Drawing.Point(535, 33);
            this.ConfigLoopBackOnlyCB.Name = "ConfigLoopBackOnlyCB";
            this.ConfigLoopBackOnlyCB.Size = new System.Drawing.Size(169, 17);
            this.ConfigLoopBackOnlyCB.TabIndex = 1;
            this.ConfigLoopBackOnlyCB.Text = "Accept LoopBack Traffic Only";
            this.ConfigLoopBackOnlyCB.UseVisualStyleBackColor = true;
            // 
            // ViewProxyLogLink
            // 
            this.ViewProxyLogLink.AutoSize = true;
            this.ViewProxyLogLink.Location = new System.Drawing.Point(318, 3);
            this.ViewProxyLogLink.Name = "ViewProxyLogLink";
            this.ViewProxyLogLink.Size = new System.Drawing.Size(56, 13);
            this.ViewProxyLogLink.TabIndex = 13;
            this.ViewProxyLogLink.TabStop = true;
            this.ViewProxyLogLink.Text = "View Logs";
            this.ViewProxyLogLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ViewProxyLogLink_LinkClicked);
            // 
            // ConfigProxyListenPortTB
            // 
            this.ConfigProxyListenPortTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigProxyListenPortTB.Location = new System.Drawing.Point(538, 5);
            this.ConfigProxyListenPortTB.Name = "ConfigProxyListenPortTB";
            this.ConfigProxyListenPortTB.Size = new System.Drawing.Size(68, 20);
            this.ConfigProxyListenPortTB.TabIndex = 2;
            this.ConfigProxyListenPortTB.TextChanged += new System.EventHandler(this.ConfigProxyListenPortTB_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(407, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Listen on Port ( > 1024 ) :";
            // 
            // ConfigSetAsSystemProxyCB
            // 
            this.ConfigSetAsSystemProxyCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigSetAsSystemProxyCB.AutoSize = true;
            this.ConfigSetAsSystemProxyCB.Enabled = false;
            this.ConfigSetAsSystemProxyCB.Location = new System.Drawing.Point(410, 32);
            this.ConfigSetAsSystemProxyCB.Name = "ConfigSetAsSystemProxyCB";
            this.ConfigSetAsSystemProxyCB.Size = new System.Drawing.Size(122, 17);
            this.ConfigSetAsSystemProxyCB.TabIndex = 5;
            this.ConfigSetAsSystemProxyCB.Text = "Set as System Proxy";
            this.ConfigSetAsSystemProxyCB.UseVisualStyleBackColor = true;
            this.ConfigSetAsSystemProxyCB.Click += new System.EventHandler(this.ConfigSetAsSystemProxyCB_Click);
            // 
            // ConfigProxyRunBtn
            // 
            this.ConfigProxyRunBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigProxyRunBtn.Location = new System.Drawing.Point(610, 3);
            this.ConfigProxyRunBtn.Name = "ConfigProxyRunBtn";
            this.ConfigProxyRunBtn.Size = new System.Drawing.Size(93, 23);
            this.ConfigProxyRunBtn.TabIndex = 0;
            this.ConfigProxyRunBtn.Text = "Start Proxy";
            this.ConfigProxyRunBtn.UseVisualStyleBackColor = true;
            this.ConfigProxyRunBtn.Click += new System.EventHandler(this.ConfigProxyRunBtn_Click);
            // 
            // ProxyDropBtn
            // 
            this.ProxyDropBtn.Location = new System.Drawing.Point(98, 10);
            this.ProxyDropBtn.MaximumSize = new System.Drawing.Size(80, 22);
            this.ProxyDropBtn.Name = "ProxyDropBtn";
            this.ProxyDropBtn.Size = new System.Drawing.Size(80, 22);
            this.ProxyDropBtn.TabIndex = 3;
            this.ProxyDropBtn.Text = "Drop";
            this.ProxyDropBtn.UseVisualStyleBackColor = true;
            this.ProxyDropBtn.Click += new System.EventHandler(this.ProxyDropBtn_Click);
            // 
            // ProxySendBtn
            // 
            this.ProxySendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ProxySendBtn.AutoSize = true;
            this.ProxySendBtn.Location = new System.Drawing.Point(5, 10);
            this.ProxySendBtn.MaximumSize = new System.Drawing.Size(80, 22);
            this.ProxySendBtn.MinimumSize = new System.Drawing.Size(68, 22);
            this.ProxySendBtn.Name = "ProxySendBtn";
            this.ProxySendBtn.Size = new System.Drawing.Size(80, 22);
            this.ProxySendBtn.TabIndex = 2;
            this.ProxySendBtn.Text = "Send";
            this.ProxySendBtn.UseVisualStyleBackColor = true;
            this.ProxySendBtn.Click += new System.EventHandler(this.ProxySendBtn_Click);
            // 
            // ProxyExceptionTB
            // 
            this.ProxyExceptionTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProxyExceptionTB.BackColor = System.Drawing.SystemColors.Window;
            this.ProxyExceptionTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyExceptionTB.ForeColor = System.Drawing.Color.Red;
            this.ProxyExceptionTB.Location = new System.Drawing.Point(5, 43);
            this.ProxyExceptionTB.Name = "ProxyExceptionTB";
            this.ProxyExceptionTB.ReadOnly = true;
            this.ProxyExceptionTB.Size = new System.Drawing.Size(387, 13);
            this.ProxyExceptionTB.TabIndex = 12;
            this.ProxyExceptionTB.Visible = false;
            // 
            // InterceptResponseCB
            // 
            this.InterceptResponseCB.AutoSize = true;
            this.InterceptResponseCB.Location = new System.Drawing.Point(191, 22);
            this.InterceptResponseCB.MaximumSize = new System.Drawing.Size(125, 17);
            this.InterceptResponseCB.Name = "InterceptResponseCB";
            this.InterceptResponseCB.Size = new System.Drawing.Size(124, 17);
            this.InterceptResponseCB.TabIndex = 7;
            this.InterceptResponseCB.Text = "Intercept Responses";
            this.InterceptResponseCB.UseVisualStyleBackColor = true;
            this.InterceptResponseCB.CheckedChanged += new System.EventHandler(this.InterceptResponseCB_CheckedChanged);
            // 
            // ProxyInterceptTabs
            // 
            this.ProxyInterceptTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProxyInterceptTabs.Controls.Add(this.ProxyInterceptRequestTab);
            this.ProxyInterceptTabs.Controls.Add(this.ProxyInterceptResponseTab);
            this.ProxyInterceptTabs.Location = new System.Drawing.Point(0, 60);
            this.ProxyInterceptTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptTabs.Name = "ProxyInterceptTabs";
            this.ProxyInterceptTabs.Padding = new System.Drawing.Point(0, 0);
            this.ProxyInterceptTabs.SelectedIndex = 0;
            this.ProxyInterceptTabs.Size = new System.Drawing.Size(705, 452);
            this.ProxyInterceptTabs.TabIndex = 1;
            // 
            // ProxyInterceptRequestTab
            // 
            this.ProxyInterceptRequestTab.Controls.Add(this.ProxyInterceptRequestTabs);
            this.ProxyInterceptRequestTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptRequestTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptRequestTab.Name = "ProxyInterceptRequestTab";
            this.ProxyInterceptRequestTab.Size = new System.Drawing.Size(697, 426);
            this.ProxyInterceptRequestTab.TabIndex = 0;
            this.ProxyInterceptRequestTab.Text = "Request";
            this.ProxyInterceptRequestTab.UseVisualStyleBackColor = true;
            // 
            // ProxyInterceptRequestTabs
            // 
            this.ProxyInterceptRequestTabs.Controls.Add(this.ProxyInterceptRequestHeadersTab);
            this.ProxyInterceptRequestTabs.Controls.Add(this.ProxyInterceptRequestBodyTab);
            this.ProxyInterceptRequestTabs.Controls.Add(this.ProxyInterceptRequestParametersTab);
            this.ProxyInterceptRequestTabs.Controls.Add(this.ProxyInterceptRequestFormatPluginsTab);
            this.ProxyInterceptRequestTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyInterceptRequestTabs.Location = new System.Drawing.Point(0, 0);
            this.ProxyInterceptRequestTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptRequestTabs.Name = "ProxyInterceptRequestTabs";
            this.ProxyInterceptRequestTabs.Padding = new System.Drawing.Point(0, 0);
            this.ProxyInterceptRequestTabs.SelectedIndex = 0;
            this.ProxyInterceptRequestTabs.Size = new System.Drawing.Size(697, 426);
            this.ProxyInterceptRequestTabs.TabIndex = 1;
            this.ProxyInterceptRequestTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.ProxyInterceptRequestTabs_Deselecting);
            // 
            // ProxyInterceptRequestHeadersTab
            // 
            this.ProxyInterceptRequestHeadersTab.Controls.Add(this.ProxyRequestHeadersIDV);
            this.ProxyInterceptRequestHeadersTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptRequestHeadersTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptRequestHeadersTab.Name = "ProxyInterceptRequestHeadersTab";
            this.ProxyInterceptRequestHeadersTab.Size = new System.Drawing.Size(689, 400);
            this.ProxyInterceptRequestHeadersTab.TabIndex = 0;
            this.ProxyInterceptRequestHeadersTab.Text = "Headers";
            this.ProxyInterceptRequestHeadersTab.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestHeadersIDV
            // 
            this.ProxyRequestHeadersIDV.AutoSize = true;
            this.ProxyRequestHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestHeadersIDV.Name = "ProxyRequestHeadersIDV";
            this.ProxyRequestHeadersIDV.ReadOnly = true;
            this.ProxyRequestHeadersIDV.Size = new System.Drawing.Size(689, 400);
            this.ProxyRequestHeadersIDV.TabIndex = 0;
            this.ProxyRequestHeadersIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.ProxyRequestHeadersIDV_IDVTextChanged);
            // 
            // ProxyInterceptRequestBodyTab
            // 
            this.ProxyInterceptRequestBodyTab.Controls.Add(this.ProxyRequestBodyIDV);
            this.ProxyInterceptRequestBodyTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptRequestBodyTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptRequestBodyTab.Name = "ProxyInterceptRequestBodyTab";
            this.ProxyInterceptRequestBodyTab.Size = new System.Drawing.Size(689, 400);
            this.ProxyInterceptRequestBodyTab.TabIndex = 1;
            this.ProxyInterceptRequestBodyTab.Text = "Body";
            this.ProxyInterceptRequestBodyTab.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestBodyIDV
            // 
            this.ProxyRequestBodyIDV.AutoSize = true;
            this.ProxyRequestBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestBodyIDV.Name = "ProxyRequestBodyIDV";
            this.ProxyRequestBodyIDV.ReadOnly = true;
            this.ProxyRequestBodyIDV.Size = new System.Drawing.Size(689, 400);
            this.ProxyRequestBodyIDV.TabIndex = 1;
            this.ProxyRequestBodyIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.ProxyRequestBodyIDV_IDVTextChanged);
            // 
            // ProxyInterceptRequestParametersTab
            // 
            this.ProxyInterceptRequestParametersTab.Controls.Add(this.ProxyRequestParametersTabs);
            this.ProxyInterceptRequestParametersTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptRequestParametersTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptRequestParametersTab.Name = "ProxyInterceptRequestParametersTab";
            this.ProxyInterceptRequestParametersTab.Size = new System.Drawing.Size(689, 400);
            this.ProxyInterceptRequestParametersTab.TabIndex = 2;
            this.ProxyInterceptRequestParametersTab.Text = "Parameters";
            this.ProxyInterceptRequestParametersTab.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestParametersTabs
            // 
            this.ProxyRequestParametersTabs.Controls.Add(this.tabPage8);
            this.ProxyRequestParametersTabs.Controls.Add(this.tabPage9);
            this.ProxyRequestParametersTabs.Controls.Add(this.tabPage10);
            this.ProxyRequestParametersTabs.Controls.Add(this.tabPage11);
            this.ProxyRequestParametersTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestParametersTabs.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestParametersTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestParametersTabs.Name = "ProxyRequestParametersTabs";
            this.ProxyRequestParametersTabs.Padding = new System.Drawing.Point(0, 0);
            this.ProxyRequestParametersTabs.SelectedIndex = 0;
            this.ProxyRequestParametersTabs.Size = new System.Drawing.Size(689, 400);
            this.ProxyRequestParametersTabs.TabIndex = 4;
            this.ProxyRequestParametersTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.ProxyRequestParametersTabs_Deselecting);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.ProxyRequestParametersQueryGrid);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(681, 374);
            this.tabPage8.TabIndex = 0;
            this.tabPage8.Text = "Query";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestParametersQueryGrid
            // 
            this.ProxyRequestParametersQueryGrid.AllowUserToAddRows = false;
            this.ProxyRequestParametersQueryGrid.AllowUserToDeleteRows = false;
            this.ProxyRequestParametersQueryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyRequestParametersQueryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyRequestParametersQueryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProxyRequestQueryParametersNameColumn,
            this.ProxyRequestQueryParametersValueColumn});
            this.ProxyRequestParametersQueryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestParametersQueryGrid.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestParametersQueryGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestParametersQueryGrid.Name = "ProxyRequestParametersQueryGrid";
            this.ProxyRequestParametersQueryGrid.RowHeadersVisible = false;
            this.ProxyRequestParametersQueryGrid.Size = new System.Drawing.Size(681, 374);
            this.ProxyRequestParametersQueryGrid.TabIndex = 0;
            this.ProxyRequestParametersQueryGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProxyRequestParametersQueryGrid_CellValueChanged);
            // 
            // ProxyRequestQueryParametersNameColumn
            // 
            this.ProxyRequestQueryParametersNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProxyRequestQueryParametersNameColumn.HeaderText = "PARAMETERS NAME";
            this.ProxyRequestQueryParametersNameColumn.Name = "ProxyRequestQueryParametersNameColumn";
            this.ProxyRequestQueryParametersNameColumn.ReadOnly = true;
            // 
            // ProxyRequestQueryParametersValueColumn
            // 
            this.ProxyRequestQueryParametersValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProxyRequestQueryParametersValueColumn.HeaderText = "PARAMETERS VALUE";
            this.ProxyRequestQueryParametersValueColumn.Name = "ProxyRequestQueryParametersValueColumn";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.ProxyRequestParametersBodyGrid);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(681, 374);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "Body";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestParametersBodyGrid
            // 
            this.ProxyRequestParametersBodyGrid.AllowUserToAddRows = false;
            this.ProxyRequestParametersBodyGrid.AllowUserToDeleteRows = false;
            this.ProxyRequestParametersBodyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyRequestParametersBodyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyRequestParametersBodyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.ProxyRequestParametersBodyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestParametersBodyGrid.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestParametersBodyGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestParametersBodyGrid.Name = "ProxyRequestParametersBodyGrid";
            this.ProxyRequestParametersBodyGrid.RowHeadersVisible = false;
            this.ProxyRequestParametersBodyGrid.Size = new System.Drawing.Size(681, 374);
            this.ProxyRequestParametersBodyGrid.TabIndex = 1;
            this.ProxyRequestParametersBodyGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProxyRequestParametersBodyGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.ProxyRequestParametersCookieGrid);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(681, 374);
            this.tabPage10.TabIndex = 2;
            this.tabPage10.Text = "Cookie";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestParametersCookieGrid
            // 
            this.ProxyRequestParametersCookieGrid.AllowUserToAddRows = false;
            this.ProxyRequestParametersCookieGrid.AllowUserToDeleteRows = false;
            this.ProxyRequestParametersCookieGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyRequestParametersCookieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyRequestParametersCookieGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.ProxyRequestParametersCookieGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestParametersCookieGrid.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestParametersCookieGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestParametersCookieGrid.Name = "ProxyRequestParametersCookieGrid";
            this.ProxyRequestParametersCookieGrid.RowHeadersVisible = false;
            this.ProxyRequestParametersCookieGrid.Size = new System.Drawing.Size(681, 374);
            this.ProxyRequestParametersCookieGrid.TabIndex = 1;
            this.ProxyRequestParametersCookieGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProxyRequestParametersCookieGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.ProxyRequestParametersHeadersGrid);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(681, 374);
            this.tabPage11.TabIndex = 3;
            this.tabPage11.Text = "Headers";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestParametersHeadersGrid
            // 
            this.ProxyRequestParametersHeadersGrid.AllowUserToAddRows = false;
            this.ProxyRequestParametersHeadersGrid.AllowUserToDeleteRows = false;
            this.ProxyRequestParametersHeadersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyRequestParametersHeadersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyRequestParametersHeadersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.ProxyRequestParametersHeadersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestParametersHeadersGrid.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestParametersHeadersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestParametersHeadersGrid.Name = "ProxyRequestParametersHeadersGrid";
            this.ProxyRequestParametersHeadersGrid.RowHeadersVisible = false;
            this.ProxyRequestParametersHeadersGrid.Size = new System.Drawing.Size(681, 374);
            this.ProxyRequestParametersHeadersGrid.TabIndex = 1;
            this.ProxyRequestParametersHeadersGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProxyRequestParametersHeadersGrid_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // ProxyInterceptRequestFormatPluginsTab
            // 
            this.ProxyInterceptRequestFormatPluginsTab.Controls.Add(this.ProxyRequestFormatSplit);
            this.ProxyInterceptRequestFormatPluginsTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptRequestFormatPluginsTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptRequestFormatPluginsTab.Name = "ProxyInterceptRequestFormatPluginsTab";
            this.ProxyInterceptRequestFormatPluginsTab.Size = new System.Drawing.Size(689, 400);
            this.ProxyInterceptRequestFormatPluginsTab.TabIndex = 4;
            this.ProxyInterceptRequestFormatPluginsTab.Text = "Format Plugins";
            this.ProxyInterceptRequestFormatPluginsTab.UseVisualStyleBackColor = true;
            // 
            // ProxyRequestFormatSplit
            // 
            this.ProxyRequestFormatSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestFormatSplit.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestFormatSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestFormatSplit.Name = "ProxyRequestFormatSplit";
            // 
            // ProxyRequestFormatSplit.Panel1
            // 
            this.ProxyRequestFormatSplit.Panel1.Controls.Add(this.ProxyRequestFormatPluginsGrid);
            // 
            // ProxyRequestFormatSplit.Panel2
            // 
            this.ProxyRequestFormatSplit.Panel2.Controls.Add(this.ProxyRequestFormatXMLTB);
            this.ProxyRequestFormatSplit.Size = new System.Drawing.Size(689, 400);
            this.ProxyRequestFormatSplit.SplitterDistance = 229;
            this.ProxyRequestFormatSplit.SplitterWidth = 2;
            this.ProxyRequestFormatSplit.TabIndex = 1;
            // 
            // ProxyRequestFormatPluginsGrid
            // 
            this.ProxyRequestFormatPluginsGrid.AllowUserToAddRows = false;
            this.ProxyRequestFormatPluginsGrid.AllowUserToDeleteRows = false;
            this.ProxyRequestFormatPluginsGrid.AllowUserToResizeRows = false;
            this.ProxyRequestFormatPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyRequestFormatPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyRequestFormatPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn24});
            this.ProxyRequestFormatPluginsGrid.ContextMenuStrip = this.ProxyRequestFormatPluginsMenu;
            this.ProxyRequestFormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestFormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ProxyRequestFormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestFormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestFormatPluginsGrid.MultiSelect = false;
            this.ProxyRequestFormatPluginsGrid.Name = "ProxyRequestFormatPluginsGrid";
            this.ProxyRequestFormatPluginsGrid.RowHeadersVisible = false;
            this.ProxyRequestFormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProxyRequestFormatPluginsGrid.Size = new System.Drawing.Size(229, 400);
            this.ProxyRequestFormatPluginsGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn24.HeaderText = "FORMAT";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProxyRequestFormatPluginsMenu
            // 
            this.ProxyRequestFormatPluginsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProxyRequestDeSerObjectToXmlMenuItem,
            this.ProxyRequestSerXmlToObjectMenuItem});
            this.ProxyRequestFormatPluginsMenu.Name = "ProxyLogMenu";
            this.ProxyRequestFormatPluginsMenu.Size = new System.Drawing.Size(210, 48);
            this.ProxyRequestFormatPluginsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ProxyRequestFormatPluginsMenu_Opening);
            // 
            // ProxyRequestDeSerObjectToXmlMenuItem
            // 
            this.ProxyRequestDeSerObjectToXmlMenuItem.Name = "ProxyRequestDeSerObjectToXmlMenuItem";
            this.ProxyRequestDeSerObjectToXmlMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ProxyRequestDeSerObjectToXmlMenuItem.Text = "DeSerialize Object to XML";
            this.ProxyRequestDeSerObjectToXmlMenuItem.Click += new System.EventHandler(this.ProxyRequestDeSerObjectToXmlMenuItem_Click);
            // 
            // ProxyRequestSerXmlToObjectMenuItem
            // 
            this.ProxyRequestSerXmlToObjectMenuItem.Name = "ProxyRequestSerXmlToObjectMenuItem";
            this.ProxyRequestSerXmlToObjectMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ProxyRequestSerXmlToObjectMenuItem.Text = "Serialize XML to Object";
            this.ProxyRequestSerXmlToObjectMenuItem.Click += new System.EventHandler(this.ProxyRequestSerXmlToObjectMenuItem_Click);
            // 
            // ProxyRequestFormatXMLTB
            // 
            this.ProxyRequestFormatXMLTB.BackColor = System.Drawing.SystemColors.Window;
            this.ProxyRequestFormatXMLTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyRequestFormatXMLTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyRequestFormatXMLTB.Location = new System.Drawing.Point(0, 0);
            this.ProxyRequestFormatXMLTB.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyRequestFormatXMLTB.Multiline = true;
            this.ProxyRequestFormatXMLTB.Name = "ProxyRequestFormatXMLTB";
            this.ProxyRequestFormatXMLTB.ReadOnly = true;
            this.ProxyRequestFormatXMLTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ProxyRequestFormatXMLTB.Size = new System.Drawing.Size(458, 400);
            this.ProxyRequestFormatXMLTB.TabIndex = 1;
            // 
            // ProxyInterceptResponseTab
            // 
            this.ProxyInterceptResponseTab.Controls.Add(this.ProxyInterceptResponseTabs);
            this.ProxyInterceptResponseTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptResponseTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptResponseTab.Name = "ProxyInterceptResponseTab";
            this.ProxyInterceptResponseTab.Size = new System.Drawing.Size(697, 426);
            this.ProxyInterceptResponseTab.TabIndex = 1;
            this.ProxyInterceptResponseTab.Text = "Response";
            this.ProxyInterceptResponseTab.UseVisualStyleBackColor = true;
            // 
            // ProxyInterceptResponseTabs
            // 
            this.ProxyInterceptResponseTabs.Controls.Add(this.ProxyInterceptResponseHeadersTab);
            this.ProxyInterceptResponseTabs.Controls.Add(this.ProxyInterceptResponseBodyTab);
            this.ProxyInterceptResponseTabs.Controls.Add(this.ProxyInterceptResponseFormatPluginsTab);
            this.ProxyInterceptResponseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyInterceptResponseTabs.Location = new System.Drawing.Point(0, 0);
            this.ProxyInterceptResponseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptResponseTabs.Name = "ProxyInterceptResponseTabs";
            this.ProxyInterceptResponseTabs.Padding = new System.Drawing.Point(0, 0);
            this.ProxyInterceptResponseTabs.SelectedIndex = 0;
            this.ProxyInterceptResponseTabs.Size = new System.Drawing.Size(697, 426);
            this.ProxyInterceptResponseTabs.TabIndex = 2;
            this.ProxyInterceptResponseTabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.ProxyInterceptResponseTabs_Deselecting);
            // 
            // ProxyInterceptResponseHeadersTab
            // 
            this.ProxyInterceptResponseHeadersTab.Controls.Add(this.ProxyResponseHeadersIDV);
            this.ProxyInterceptResponseHeadersTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptResponseHeadersTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptResponseHeadersTab.Name = "ProxyInterceptResponseHeadersTab";
            this.ProxyInterceptResponseHeadersTab.Size = new System.Drawing.Size(689, 400);
            this.ProxyInterceptResponseHeadersTab.TabIndex = 0;
            this.ProxyInterceptResponseHeadersTab.Text = "Headers";
            this.ProxyInterceptResponseHeadersTab.UseVisualStyleBackColor = true;
            // 
            // ProxyResponseHeadersIDV
            // 
            this.ProxyResponseHeadersIDV.AutoSize = true;
            this.ProxyResponseHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyResponseHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.ProxyResponseHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyResponseHeadersIDV.Name = "ProxyResponseHeadersIDV";
            this.ProxyResponseHeadersIDV.ReadOnly = true;
            this.ProxyResponseHeadersIDV.Size = new System.Drawing.Size(689, 400);
            this.ProxyResponseHeadersIDV.TabIndex = 1;
            this.ProxyResponseHeadersIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.ProxyResponseHeadersIDV_IDVTextChanged);
            // 
            // ProxyInterceptResponseBodyTab
            // 
            this.ProxyInterceptResponseBodyTab.Controls.Add(this.ProxyResponseBodyIDV);
            this.ProxyInterceptResponseBodyTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptResponseBodyTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptResponseBodyTab.Name = "ProxyInterceptResponseBodyTab";
            this.ProxyInterceptResponseBodyTab.Size = new System.Drawing.Size(689, 400);
            this.ProxyInterceptResponseBodyTab.TabIndex = 1;
            this.ProxyInterceptResponseBodyTab.Text = "Body";
            this.ProxyInterceptResponseBodyTab.UseVisualStyleBackColor = true;
            // 
            // ProxyResponseBodyIDV
            // 
            this.ProxyResponseBodyIDV.AutoSize = true;
            this.ProxyResponseBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyResponseBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.ProxyResponseBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyResponseBodyIDV.Name = "ProxyResponseBodyIDV";
            this.ProxyResponseBodyIDV.ReadOnly = true;
            this.ProxyResponseBodyIDV.Size = new System.Drawing.Size(689, 400);
            this.ProxyResponseBodyIDV.TabIndex = 1;
            this.ProxyResponseBodyIDV.IDVTextChanged += new IronDataView.IronDataView.TextChanged(this.ProxyResponseBodyIDV_IDVTextChanged);
            // 
            // ProxyInterceptResponseFormatPluginsTab
            // 
            this.ProxyInterceptResponseFormatPluginsTab.Controls.Add(this.ProxyResponseFormatSplit);
            this.ProxyInterceptResponseFormatPluginsTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyInterceptResponseFormatPluginsTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyInterceptResponseFormatPluginsTab.Name = "ProxyInterceptResponseFormatPluginsTab";
            this.ProxyInterceptResponseFormatPluginsTab.Size = new System.Drawing.Size(689, 400);
            this.ProxyInterceptResponseFormatPluginsTab.TabIndex = 4;
            this.ProxyInterceptResponseFormatPluginsTab.Text = "Format Plugins";
            this.ProxyInterceptResponseFormatPluginsTab.UseVisualStyleBackColor = true;
            // 
            // ProxyResponseFormatSplit
            // 
            this.ProxyResponseFormatSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyResponseFormatSplit.Location = new System.Drawing.Point(0, 0);
            this.ProxyResponseFormatSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyResponseFormatSplit.Name = "ProxyResponseFormatSplit";
            // 
            // ProxyResponseFormatSplit.Panel1
            // 
            this.ProxyResponseFormatSplit.Panel1.Controls.Add(this.ProxyResponseFormatPluginsGrid);
            // 
            // ProxyResponseFormatSplit.Panel2
            // 
            this.ProxyResponseFormatSplit.Panel2.Controls.Add(this.ProxyResponseFormatXMLTB);
            this.ProxyResponseFormatSplit.Size = new System.Drawing.Size(689, 400);
            this.ProxyResponseFormatSplit.SplitterDistance = 229;
            this.ProxyResponseFormatSplit.SplitterWidth = 2;
            this.ProxyResponseFormatSplit.TabIndex = 1;
            // 
            // ProxyResponseFormatPluginsGrid
            // 
            this.ProxyResponseFormatPluginsGrid.AllowUserToAddRows = false;
            this.ProxyResponseFormatPluginsGrid.AllowUserToDeleteRows = false;
            this.ProxyResponseFormatPluginsGrid.AllowUserToResizeRows = false;
            this.ProxyResponseFormatPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyResponseFormatPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyResponseFormatPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn25});
            this.ProxyResponseFormatPluginsGrid.ContextMenuStrip = this.ProxyResponseFormatPluginsMenu;
            this.ProxyResponseFormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyResponseFormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ProxyResponseFormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.ProxyResponseFormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyResponseFormatPluginsGrid.MultiSelect = false;
            this.ProxyResponseFormatPluginsGrid.Name = "ProxyResponseFormatPluginsGrid";
            this.ProxyResponseFormatPluginsGrid.RowHeadersVisible = false;
            this.ProxyResponseFormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProxyResponseFormatPluginsGrid.Size = new System.Drawing.Size(229, 400);
            this.ProxyResponseFormatPluginsGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn25.HeaderText = "FORMAT";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProxyResponseFormatPluginsMenu
            // 
            this.ProxyResponseFormatPluginsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProxyResponseDeSerObjectToXmlMenuItem,
            this.ProxyResponseSerXmlToObjectMenuItem});
            this.ProxyResponseFormatPluginsMenu.Name = "ProxyLogMenu";
            this.ProxyResponseFormatPluginsMenu.Size = new System.Drawing.Size(210, 48);
            this.ProxyResponseFormatPluginsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ProxyResponseFormatPluginsMenu_Opening);
            // 
            // ProxyResponseDeSerObjectToXmlMenuItem
            // 
            this.ProxyResponseDeSerObjectToXmlMenuItem.Name = "ProxyResponseDeSerObjectToXmlMenuItem";
            this.ProxyResponseDeSerObjectToXmlMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ProxyResponseDeSerObjectToXmlMenuItem.Text = "DeSerialize Object to XML";
            this.ProxyResponseDeSerObjectToXmlMenuItem.Click += new System.EventHandler(this.ProxyResponseDeSerObjectToXmlMenuItem_Click);
            // 
            // ProxyResponseSerXmlToObjectMenuItem
            // 
            this.ProxyResponseSerXmlToObjectMenuItem.Name = "ProxyResponseSerXmlToObjectMenuItem";
            this.ProxyResponseSerXmlToObjectMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ProxyResponseSerXmlToObjectMenuItem.Text = "Serialize XML to Object";
            this.ProxyResponseSerXmlToObjectMenuItem.Click += new System.EventHandler(this.ProxyResponseSerXmlToObjectMenuItem_Click);
            // 
            // ProxyResponseFormatXMLTB
            // 
            this.ProxyResponseFormatXMLTB.BackColor = System.Drawing.SystemColors.Window;
            this.ProxyResponseFormatXMLTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProxyResponseFormatXMLTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyResponseFormatXMLTB.Location = new System.Drawing.Point(0, 0);
            this.ProxyResponseFormatXMLTB.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyResponseFormatXMLTB.Multiline = true;
            this.ProxyResponseFormatXMLTB.Name = "ProxyResponseFormatXMLTB";
            this.ProxyResponseFormatXMLTB.ReadOnly = true;
            this.ProxyResponseFormatXMLTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ProxyResponseFormatXMLTB.Size = new System.Drawing.Size(458, 400);
            this.ProxyResponseFormatXMLTB.TabIndex = 1;
            // 
            // InterceptRequestCB
            // 
            this.InterceptRequestCB.AutoSize = true;
            this.InterceptRequestCB.Location = new System.Drawing.Point(191, 4);
            this.InterceptRequestCB.MaximumSize = new System.Drawing.Size(125, 17);
            this.InterceptRequestCB.Name = "InterceptRequestCB";
            this.InterceptRequestCB.Size = new System.Drawing.Size(116, 17);
            this.InterceptRequestCB.TabIndex = 6;
            this.InterceptRequestCB.Text = "Intercept Requests";
            this.InterceptRequestCB.UseVisualStyleBackColor = true;
            this.InterceptRequestCB.CheckedChanged += new System.EventHandler(this.InterceptRequestCB_CheckedChanged);
            // 
            // mt_logs
            // 
            this.mt_logs.Controls.Add(this.LogBaseSplit);
            this.mt_logs.Location = new System.Drawing.Point(4, 22);
            this.mt_logs.Margin = new System.Windows.Forms.Padding(0);
            this.mt_logs.Name = "mt_logs";
            this.mt_logs.Size = new System.Drawing.Size(705, 512);
            this.mt_logs.TabIndex = 9;
            this.mt_logs.Text = "Logs";
            this.mt_logs.UseVisualStyleBackColor = true;
            // 
            // LogBaseSplit
            // 
            this.LogBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.LogBaseSplit.Name = "LogBaseSplit";
            this.LogBaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // LogBaseSplit.Panel1
            // 
            this.LogBaseSplit.Panel1.Controls.Add(this.LogOptionsBtn);
            this.LogBaseSplit.Panel1.Controls.Add(this.LogIDLbl);
            this.LogBaseSplit.Panel1.Controls.Add(this.LogSourceLbl);
            this.LogBaseSplit.Panel1.Controls.Add(this.LogStatusTB);
            this.LogBaseSplit.Panel1.Controls.Add(this.ShowLogGridBtn);
            this.LogBaseSplit.Panel1.Controls.Add(this.NextLogBtn);
            this.LogBaseSplit.Panel1.Controls.Add(this.ProxyShowOriginalResponseCB);
            this.LogBaseSplit.Panel1.Controls.Add(this.ProxyShowOriginalRequestCB);
            this.LogBaseSplit.Panel1.Controls.Add(this.PreviousLogBtn);
            this.LogBaseSplit.Panel1.Controls.Add(this.LogDisplayTabs);
            // 
            // LogBaseSplit.Panel2
            // 
            this.LogBaseSplit.Panel2.Controls.Add(this.LogTabs);
            this.LogBaseSplit.Size = new System.Drawing.Size(705, 512);
            this.LogBaseSplit.SplitterDistance = 279;
            this.LogBaseSplit.TabIndex = 3;
            // 
            // LogOptionsBtn
            // 
            this.LogOptionsBtn.ContextMenuStrip = this.LogMenu;
            this.LogOptionsBtn.Location = new System.Drawing.Point(115, 4);
            this.LogOptionsBtn.Name = "LogOptionsBtn";
            this.LogOptionsBtn.Size = new System.Drawing.Size(47, 19);
            this.LogOptionsBtn.TabIndex = 15;
            this.LogOptionsBtn.Text = "\\/";
            this.LogOptionsBtn.UseVisualStyleBackColor = true;
            this.LogOptionsBtn.Click += new System.EventHandler(this.LogOptionsBtn_Click);
            // 
            // LogIDLbl
            // 
            this.LogIDLbl.AutoSize = true;
            this.LogIDLbl.Location = new System.Drawing.Point(90, 28);
            this.LogIDLbl.Name = "LogIDLbl";
            this.LogIDLbl.Size = new System.Drawing.Size(21, 13);
            this.LogIDLbl.TabIndex = 14;
            this.LogIDLbl.Text = "ID:";
            // 
            // LogSourceLbl
            // 
            this.LogSourceLbl.AutoSize = true;
            this.LogSourceLbl.Location = new System.Drawing.Point(5, 28);
            this.LogSourceLbl.Name = "LogSourceLbl";
            this.LogSourceLbl.Size = new System.Drawing.Size(44, 13);
            this.LogSourceLbl.TabIndex = 2;
            this.LogSourceLbl.Text = "Source:";
            // 
            // LogStatusTB
            // 
            this.LogStatusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LogStatusTB.BackColor = System.Drawing.SystemColors.Window;
            this.LogStatusTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogStatusTB.ForeColor = System.Drawing.Color.Red;
            this.LogStatusTB.Location = new System.Drawing.Point(140, 23);
            this.LogStatusTB.Multiline = true;
            this.LogStatusTB.Name = "LogStatusTB";
            this.LogStatusTB.ReadOnly = true;
            this.LogStatusTB.Size = new System.Drawing.Size(448, 17);
            this.LogStatusTB.TabIndex = 13;
            this.LogStatusTB.Visible = false;
            // 
            // ShowLogGridBtn
            // 
            this.ShowLogGridBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowLogGridBtn.Location = new System.Drawing.Point(595, 7);
            this.ShowLogGridBtn.Name = "ShowLogGridBtn";
            this.ShowLogGridBtn.Size = new System.Drawing.Size(104, 23);
            this.ShowLogGridBtn.TabIndex = 12;
            this.ShowLogGridBtn.Text = "Hide Log Grids";
            this.ShowLogGridBtn.UseVisualStyleBackColor = true;
            this.ShowLogGridBtn.Click += new System.EventHandler(this.ShowLogGridBtn_Click);
            // 
            // NextLogBtn
            // 
            this.NextLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextLogBtn.Location = new System.Drawing.Point(59, 2);
            this.NextLogBtn.Name = "NextLogBtn";
            this.NextLogBtn.Size = new System.Drawing.Size(49, 23);
            this.NextLogBtn.TabIndex = 4;
            this.NextLogBtn.Text = ">";
            this.NextLogBtn.UseVisualStyleBackColor = true;
            this.NextLogBtn.Click += new System.EventHandler(this.NextLogBtn_Click);
            // 
            // ProxyShowOriginalResponseCB
            // 
            this.ProxyShowOriginalResponseCB.AutoSize = true;
            this.ProxyShowOriginalResponseCB.Location = new System.Drawing.Point(341, 3);
            this.ProxyShowOriginalResponseCB.MaximumSize = new System.Drawing.Size(155, 17);
            this.ProxyShowOriginalResponseCB.Name = "ProxyShowOriginalResponseCB";
            this.ProxyShowOriginalResponseCB.Size = new System.Drawing.Size(142, 17);
            this.ProxyShowOriginalResponseCB.TabIndex = 11;
            this.ProxyShowOriginalResponseCB.Text = "Show Original Response";
            this.ProxyShowOriginalResponseCB.UseVisualStyleBackColor = true;
            this.ProxyShowOriginalResponseCB.Visible = false;
            this.ProxyShowOriginalResponseCB.Click += new System.EventHandler(this.ProxyShowOriginalResponseCB_Click);
            // 
            // ProxyShowOriginalRequestCB
            // 
            this.ProxyShowOriginalRequestCB.AutoSize = true;
            this.ProxyShowOriginalRequestCB.Location = new System.Drawing.Point(201, 3);
            this.ProxyShowOriginalRequestCB.MaximumSize = new System.Drawing.Size(155, 17);
            this.ProxyShowOriginalRequestCB.Name = "ProxyShowOriginalRequestCB";
            this.ProxyShowOriginalRequestCB.Size = new System.Drawing.Size(134, 17);
            this.ProxyShowOriginalRequestCB.TabIndex = 10;
            this.ProxyShowOriginalRequestCB.Text = "Show Original Request";
            this.ProxyShowOriginalRequestCB.UseVisualStyleBackColor = true;
            this.ProxyShowOriginalRequestCB.Visible = false;
            this.ProxyShowOriginalRequestCB.Click += new System.EventHandler(this.ProxyShowOriginalRequestCB_Click);
            // 
            // PreviousLogBtn
            // 
            this.PreviousLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviousLogBtn.Location = new System.Drawing.Point(3, 2);
            this.PreviousLogBtn.Name = "PreviousLogBtn";
            this.PreviousLogBtn.Size = new System.Drawing.Size(49, 23);
            this.PreviousLogBtn.TabIndex = 3;
            this.PreviousLogBtn.Text = "<";
            this.PreviousLogBtn.UseVisualStyleBackColor = true;
            this.PreviousLogBtn.Click += new System.EventHandler(this.PreviousLogBtn_Click);
            // 
            // LogDisplayTabs
            // 
            this.LogDisplayTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LogDisplayTabs.Controls.Add(this.tabPage7);
            this.LogDisplayTabs.Controls.Add(this.tabPage29);
            this.LogDisplayTabs.Location = new System.Drawing.Point(0, 45);
            this.LogDisplayTabs.Margin = new System.Windows.Forms.Padding(0);
            this.LogDisplayTabs.Name = "LogDisplayTabs";
            this.LogDisplayTabs.Padding = new System.Drawing.Point(0, 0);
            this.LogDisplayTabs.SelectedIndex = 0;
            this.LogDisplayTabs.Size = new System.Drawing.Size(705, 234);
            this.LogDisplayTabs.TabIndex = 2;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.LogRequestTabs);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(697, 208);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "Request";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // LogRequestTabs
            // 
            this.LogRequestTabs.Controls.Add(this.tabPage16);
            this.LogRequestTabs.Controls.Add(this.tabPage22);
            this.LogRequestTabs.Controls.Add(this.tabPage23);
            this.LogRequestTabs.Controls.Add(this.tabPage28);
            this.LogRequestTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestTabs.Location = new System.Drawing.Point(0, 0);
            this.LogRequestTabs.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestTabs.Name = "LogRequestTabs";
            this.LogRequestTabs.Padding = new System.Drawing.Point(0, 0);
            this.LogRequestTabs.SelectedIndex = 0;
            this.LogRequestTabs.Size = new System.Drawing.Size(697, 208);
            this.LogRequestTabs.TabIndex = 1;
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.LogRequestHeadersIDV);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Size = new System.Drawing.Size(689, 182);
            this.tabPage16.TabIndex = 0;
            this.tabPage16.Text = "Headers";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // LogRequestHeadersIDV
            // 
            this.LogRequestHeadersIDV.AutoSize = true;
            this.LogRequestHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.LogRequestHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestHeadersIDV.Name = "LogRequestHeadersIDV";
            this.LogRequestHeadersIDV.ReadOnly = true;
            this.LogRequestHeadersIDV.Size = new System.Drawing.Size(689, 182);
            this.LogRequestHeadersIDV.TabIndex = 0;
            // 
            // tabPage22
            // 
            this.tabPage22.Controls.Add(this.LogRequestBodyIDV);
            this.tabPage22.Location = new System.Drawing.Point(4, 22);
            this.tabPage22.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage22.Name = "tabPage22";
            this.tabPage22.Size = new System.Drawing.Size(689, 182);
            this.tabPage22.TabIndex = 1;
            this.tabPage22.Text = "Body";
            this.tabPage22.UseVisualStyleBackColor = true;
            // 
            // LogRequestBodyIDV
            // 
            this.LogRequestBodyIDV.AutoSize = true;
            this.LogRequestBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.LogRequestBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestBodyIDV.Name = "LogRequestBodyIDV";
            this.LogRequestBodyIDV.ReadOnly = true;
            this.LogRequestBodyIDV.Size = new System.Drawing.Size(689, 182);
            this.LogRequestBodyIDV.TabIndex = 1;
            // 
            // tabPage23
            // 
            this.tabPage23.Controls.Add(this.tabControl5);
            this.tabPage23.Location = new System.Drawing.Point(4, 22);
            this.tabPage23.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage23.Name = "tabPage23";
            this.tabPage23.Size = new System.Drawing.Size(689, 182);
            this.tabPage23.TabIndex = 2;
            this.tabPage23.Text = "Parameters";
            this.tabPage23.UseVisualStyleBackColor = true;
            // 
            // tabControl5
            // 
            this.tabControl5.Controls.Add(this.tabPage24);
            this.tabControl5.Controls.Add(this.tabPage25);
            this.tabControl5.Controls.Add(this.tabPage26);
            this.tabControl5.Controls.Add(this.tabPage27);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(0, 0);
            this.tabControl5.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.Padding = new System.Drawing.Point(0, 0);
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(689, 182);
            this.tabControl5.TabIndex = 4;
            // 
            // tabPage24
            // 
            this.tabPage24.Controls.Add(this.LogRequestParametersQueryGrid);
            this.tabPage24.Location = new System.Drawing.Point(4, 22);
            this.tabPage24.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage24.Name = "tabPage24";
            this.tabPage24.Size = new System.Drawing.Size(681, 156);
            this.tabPage24.TabIndex = 0;
            this.tabPage24.Text = "Query";
            this.tabPage24.UseVisualStyleBackColor = true;
            // 
            // LogRequestParametersQueryGrid
            // 
            this.LogRequestParametersQueryGrid.AllowUserToAddRows = false;
            this.LogRequestParametersQueryGrid.AllowUserToDeleteRows = false;
            this.LogRequestParametersQueryGrid.BackgroundColor = System.Drawing.Color.White;
            this.LogRequestParametersQueryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogRequestParametersQueryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogRequestParametersQueryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34});
            this.LogRequestParametersQueryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestParametersQueryGrid.GridColor = System.Drawing.Color.White;
            this.LogRequestParametersQueryGrid.Location = new System.Drawing.Point(0, 0);
            this.LogRequestParametersQueryGrid.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestParametersQueryGrid.Name = "LogRequestParametersQueryGrid";
            this.LogRequestParametersQueryGrid.ReadOnly = true;
            this.LogRequestParametersQueryGrid.RowHeadersVisible = false;
            this.LogRequestParametersQueryGrid.Size = new System.Drawing.Size(681, 156);
            this.LogRequestParametersQueryGrid.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn33.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn34.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            // 
            // tabPage25
            // 
            this.tabPage25.Controls.Add(this.LogRequestParametersBodyGrid);
            this.tabPage25.Location = new System.Drawing.Point(4, 22);
            this.tabPage25.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage25.Name = "tabPage25";
            this.tabPage25.Size = new System.Drawing.Size(681, 156);
            this.tabPage25.TabIndex = 1;
            this.tabPage25.Text = "Body";
            this.tabPage25.UseVisualStyleBackColor = true;
            // 
            // LogRequestParametersBodyGrid
            // 
            this.LogRequestParametersBodyGrid.AllowUserToAddRows = false;
            this.LogRequestParametersBodyGrid.AllowUserToDeleteRows = false;
            this.LogRequestParametersBodyGrid.BackgroundColor = System.Drawing.Color.White;
            this.LogRequestParametersBodyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogRequestParametersBodyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogRequestParametersBodyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39});
            this.LogRequestParametersBodyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestParametersBodyGrid.GridColor = System.Drawing.Color.White;
            this.LogRequestParametersBodyGrid.Location = new System.Drawing.Point(0, 0);
            this.LogRequestParametersBodyGrid.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestParametersBodyGrid.Name = "LogRequestParametersBodyGrid";
            this.LogRequestParametersBodyGrid.ReadOnly = true;
            this.LogRequestParametersBodyGrid.RowHeadersVisible = false;
            this.LogRequestParametersBodyGrid.Size = new System.Drawing.Size(681, 156);
            this.LogRequestParametersBodyGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn38.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn39.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.ReadOnly = true;
            // 
            // tabPage26
            // 
            this.tabPage26.Controls.Add(this.LogRequestParametersCookieGrid);
            this.tabPage26.Location = new System.Drawing.Point(4, 22);
            this.tabPage26.Name = "tabPage26";
            this.tabPage26.Size = new System.Drawing.Size(681, 156);
            this.tabPage26.TabIndex = 2;
            this.tabPage26.Text = "Cookie";
            this.tabPage26.UseVisualStyleBackColor = true;
            // 
            // LogRequestParametersCookieGrid
            // 
            this.LogRequestParametersCookieGrid.AllowUserToAddRows = false;
            this.LogRequestParametersCookieGrid.AllowUserToDeleteRows = false;
            this.LogRequestParametersCookieGrid.BackgroundColor = System.Drawing.Color.White;
            this.LogRequestParametersCookieGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogRequestParametersCookieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogRequestParametersCookieGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn41});
            this.LogRequestParametersCookieGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestParametersCookieGrid.GridColor = System.Drawing.Color.White;
            this.LogRequestParametersCookieGrid.Location = new System.Drawing.Point(0, 0);
            this.LogRequestParametersCookieGrid.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestParametersCookieGrid.Name = "LogRequestParametersCookieGrid";
            this.LogRequestParametersCookieGrid.ReadOnly = true;
            this.LogRequestParametersCookieGrid.RowHeadersVisible = false;
            this.LogRequestParametersCookieGrid.Size = new System.Drawing.Size(681, 156);
            this.LogRequestParametersCookieGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn40.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn41.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.ReadOnly = true;
            // 
            // tabPage27
            // 
            this.tabPage27.Controls.Add(this.LogRequestParametersHeadersGrid);
            this.tabPage27.Location = new System.Drawing.Point(4, 22);
            this.tabPage27.Name = "tabPage27";
            this.tabPage27.Size = new System.Drawing.Size(681, 156);
            this.tabPage27.TabIndex = 3;
            this.tabPage27.Text = "Headers";
            this.tabPage27.UseVisualStyleBackColor = true;
            // 
            // LogRequestParametersHeadersGrid
            // 
            this.LogRequestParametersHeadersGrid.AllowUserToAddRows = false;
            this.LogRequestParametersHeadersGrid.AllowUserToDeleteRows = false;
            this.LogRequestParametersHeadersGrid.BackgroundColor = System.Drawing.Color.White;
            this.LogRequestParametersHeadersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogRequestParametersHeadersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogRequestParametersHeadersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43});
            this.LogRequestParametersHeadersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestParametersHeadersGrid.GridColor = System.Drawing.Color.White;
            this.LogRequestParametersHeadersGrid.Location = new System.Drawing.Point(0, 0);
            this.LogRequestParametersHeadersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestParametersHeadersGrid.Name = "LogRequestParametersHeadersGrid";
            this.LogRequestParametersHeadersGrid.ReadOnly = true;
            this.LogRequestParametersHeadersGrid.RowHeadersVisible = false;
            this.LogRequestParametersHeadersGrid.Size = new System.Drawing.Size(681, 156);
            this.LogRequestParametersHeadersGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn42.HeaderText = "PARAMETERS NAME";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn43.HeaderText = "PARAMETERS VALUE";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.ReadOnly = true;
            // 
            // tabPage28
            // 
            this.tabPage28.Controls.Add(this.splitContainer1);
            this.tabPage28.Location = new System.Drawing.Point(4, 22);
            this.tabPage28.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage28.Name = "tabPage28";
            this.tabPage28.Size = new System.Drawing.Size(689, 182);
            this.tabPage28.TabIndex = 4;
            this.tabPage28.Text = "Format Plugins";
            this.tabPage28.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LogRequestFormatPluginsGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LogRequestFormatXMLTB);
            this.splitContainer1.Size = new System.Drawing.Size(689, 182);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // LogRequestFormatPluginsGrid
            // 
            this.LogRequestFormatPluginsGrid.AllowUserToAddRows = false;
            this.LogRequestFormatPluginsGrid.AllowUserToDeleteRows = false;
            this.LogRequestFormatPluginsGrid.AllowUserToResizeRows = false;
            this.LogRequestFormatPluginsGrid.BackgroundColor = System.Drawing.Color.White;
            this.LogRequestFormatPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogRequestFormatPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogRequestFormatPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn44});
            this.LogRequestFormatPluginsGrid.ContextMenuStrip = this.ProxyRequestFormatPluginsMenu;
            this.LogRequestFormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestFormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.LogRequestFormatPluginsGrid.GridColor = System.Drawing.Color.White;
            this.LogRequestFormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.LogRequestFormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestFormatPluginsGrid.MultiSelect = false;
            this.LogRequestFormatPluginsGrid.Name = "LogRequestFormatPluginsGrid";
            this.LogRequestFormatPluginsGrid.RowHeadersVisible = false;
            this.LogRequestFormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LogRequestFormatPluginsGrid.Size = new System.Drawing.Size(109, 182);
            this.LogRequestFormatPluginsGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn44.HeaderText = "FORMAT";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LogRequestFormatXMLTB
            // 
            this.LogRequestFormatXMLTB.BackColor = System.Drawing.SystemColors.Window;
            this.LogRequestFormatXMLTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogRequestFormatXMLTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRequestFormatXMLTB.Location = new System.Drawing.Point(0, 0);
            this.LogRequestFormatXMLTB.Margin = new System.Windows.Forms.Padding(0);
            this.LogRequestFormatXMLTB.Multiline = true;
            this.LogRequestFormatXMLTB.Name = "LogRequestFormatXMLTB";
            this.LogRequestFormatXMLTB.ReadOnly = true;
            this.LogRequestFormatXMLTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogRequestFormatXMLTB.Size = new System.Drawing.Size(578, 182);
            this.LogRequestFormatXMLTB.TabIndex = 1;
            // 
            // tabPage29
            // 
            this.tabPage29.Controls.Add(this.LogResponseTabs);
            this.tabPage29.Location = new System.Drawing.Point(4, 22);
            this.tabPage29.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage29.Name = "tabPage29";
            this.tabPage29.Size = new System.Drawing.Size(697, 208);
            this.tabPage29.TabIndex = 1;
            this.tabPage29.Text = "Response";
            this.tabPage29.UseVisualStyleBackColor = true;
            // 
            // LogResponseTabs
            // 
            this.LogResponseTabs.Controls.Add(this.tabPage30);
            this.LogResponseTabs.Controls.Add(this.tabPage31);
            this.LogResponseTabs.Controls.Add(this.tabPage39);
            this.LogResponseTabs.Controls.Add(this.tabPage32);
            this.LogResponseTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogResponseTabs.Location = new System.Drawing.Point(0, 0);
            this.LogResponseTabs.Margin = new System.Windows.Forms.Padding(0);
            this.LogResponseTabs.Name = "LogResponseTabs";
            this.LogResponseTabs.Padding = new System.Drawing.Point(0, 0);
            this.LogResponseTabs.SelectedIndex = 0;
            this.LogResponseTabs.Size = new System.Drawing.Size(697, 208);
            this.LogResponseTabs.TabIndex = 2;
            // 
            // tabPage30
            // 
            this.tabPage30.Controls.Add(this.LogResponseHeadersIDV);
            this.tabPage30.Location = new System.Drawing.Point(4, 22);
            this.tabPage30.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage30.Name = "tabPage30";
            this.tabPage30.Size = new System.Drawing.Size(689, 182);
            this.tabPage30.TabIndex = 0;
            this.tabPage30.Text = "Headers";
            this.tabPage30.UseVisualStyleBackColor = true;
            // 
            // LogResponseHeadersIDV
            // 
            this.LogResponseHeadersIDV.AutoSize = true;
            this.LogResponseHeadersIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogResponseHeadersIDV.Location = new System.Drawing.Point(0, 0);
            this.LogResponseHeadersIDV.Margin = new System.Windows.Forms.Padding(0);
            this.LogResponseHeadersIDV.Name = "LogResponseHeadersIDV";
            this.LogResponseHeadersIDV.ReadOnly = true;
            this.LogResponseHeadersIDV.Size = new System.Drawing.Size(689, 182);
            this.LogResponseHeadersIDV.TabIndex = 1;
            // 
            // tabPage31
            // 
            this.tabPage31.Controls.Add(this.LogResponseBodyIDV);
            this.tabPage31.Location = new System.Drawing.Point(4, 22);
            this.tabPage31.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage31.Name = "tabPage31";
            this.tabPage31.Size = new System.Drawing.Size(689, 182);
            this.tabPage31.TabIndex = 1;
            this.tabPage31.Text = "Body";
            this.tabPage31.UseVisualStyleBackColor = true;
            // 
            // LogResponseBodyIDV
            // 
            this.LogResponseBodyIDV.AutoSize = true;
            this.LogResponseBodyIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogResponseBodyIDV.Location = new System.Drawing.Point(0, 0);
            this.LogResponseBodyIDV.Margin = new System.Windows.Forms.Padding(0);
            this.LogResponseBodyIDV.Name = "LogResponseBodyIDV";
            this.LogResponseBodyIDV.ReadOnly = true;
            this.LogResponseBodyIDV.Size = new System.Drawing.Size(689, 182);
            this.LogResponseBodyIDV.TabIndex = 1;
            // 
            // tabPage39
            // 
            this.tabPage39.Controls.Add(this.LogReflectionRTB);
            this.tabPage39.Location = new System.Drawing.Point(4, 22);
            this.tabPage39.Name = "tabPage39";
            this.tabPage39.Size = new System.Drawing.Size(689, 182);
            this.tabPage39.TabIndex = 5;
            this.tabPage39.Text = "Reflections";
            this.tabPage39.UseVisualStyleBackColor = true;
            // 
            // LogReflectionRTB
            // 
            this.LogReflectionRTB.BackColor = System.Drawing.Color.White;
            this.LogReflectionRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogReflectionRTB.DetectUrls = false;
            this.LogReflectionRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogReflectionRTB.Font = new System.Drawing.Font("Lucida Bright", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogReflectionRTB.Location = new System.Drawing.Point(0, 0);
            this.LogReflectionRTB.Name = "LogReflectionRTB";
            this.LogReflectionRTB.ReadOnly = true;
            this.LogReflectionRTB.Size = new System.Drawing.Size(689, 182);
            this.LogReflectionRTB.TabIndex = 0;
            this.LogReflectionRTB.Text = "";
            // 
            // tabPage32
            // 
            this.tabPage32.Controls.Add(this.splitContainer2);
            this.tabPage32.Location = new System.Drawing.Point(4, 22);
            this.tabPage32.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage32.Name = "tabPage32";
            this.tabPage32.Size = new System.Drawing.Size(689, 182);
            this.tabPage32.TabIndex = 4;
            this.tabPage32.Text = "Format Plugins";
            this.tabPage32.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.LogResponseFormatPluginsGrid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.LogResponseFormatXMLTB);
            this.splitContainer2.Size = new System.Drawing.Size(689, 182);
            this.splitContainer2.SplitterDistance = 229;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 1;
            // 
            // LogResponseFormatPluginsGrid
            // 
            this.LogResponseFormatPluginsGrid.AllowUserToAddRows = false;
            this.LogResponseFormatPluginsGrid.AllowUserToDeleteRows = false;
            this.LogResponseFormatPluginsGrid.AllowUserToResizeRows = false;
            this.LogResponseFormatPluginsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogResponseFormatPluginsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogResponseFormatPluginsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn45});
            this.LogResponseFormatPluginsGrid.ContextMenuStrip = this.ProxyResponseFormatPluginsMenu;
            this.LogResponseFormatPluginsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogResponseFormatPluginsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.LogResponseFormatPluginsGrid.Location = new System.Drawing.Point(0, 0);
            this.LogResponseFormatPluginsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.LogResponseFormatPluginsGrid.MultiSelect = false;
            this.LogResponseFormatPluginsGrid.Name = "LogResponseFormatPluginsGrid";
            this.LogResponseFormatPluginsGrid.RowHeadersVisible = false;
            this.LogResponseFormatPluginsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LogResponseFormatPluginsGrid.Size = new System.Drawing.Size(229, 182);
            this.LogResponseFormatPluginsGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn45.HeaderText = "FORMAT";
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            this.dataGridViewTextBoxColumn45.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LogResponseFormatXMLTB
            // 
            this.LogResponseFormatXMLTB.BackColor = System.Drawing.SystemColors.Window;
            this.LogResponseFormatXMLTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogResponseFormatXMLTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogResponseFormatXMLTB.Location = new System.Drawing.Point(0, 0);
            this.LogResponseFormatXMLTB.Margin = new System.Windows.Forms.Padding(0);
            this.LogResponseFormatXMLTB.Multiline = true;
            this.LogResponseFormatXMLTB.Name = "LogResponseFormatXMLTB";
            this.LogResponseFormatXMLTB.ReadOnly = true;
            this.LogResponseFormatXMLTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogResponseFormatXMLTB.Size = new System.Drawing.Size(458, 182);
            this.LogResponseFormatXMLTB.TabIndex = 1;
            // 
            // LogTabs
            // 
            this.LogTabs.Controls.Add(this.ProxyLogTab);
            this.LogTabs.Controls.Add(this.ScanLogTab);
            this.LogTabs.Controls.Add(this.TestLogTab);
            this.LogTabs.Controls.Add(this.ShellLogTab);
            this.LogTabs.Controls.Add(this.ProbeLogTab);
            this.LogTabs.Controls.Add(this.SiteMapLogTab);
            this.LogTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTabs.Location = new System.Drawing.Point(0, 0);
            this.LogTabs.Margin = new System.Windows.Forms.Padding(0);
            this.LogTabs.Name = "LogTabs";
            this.LogTabs.Padding = new System.Drawing.Point(0, 0);
            this.LogTabs.SelectedIndex = 0;
            this.LogTabs.Size = new System.Drawing.Size(705, 229);
            this.LogTabs.TabIndex = 0;
            // 
            // ProxyLogTab
            // 
            this.ProxyLogTab.Controls.Add(this.ProxyLogGrid);
            this.ProxyLogTab.Location = new System.Drawing.Point(4, 22);
            this.ProxyLogTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyLogTab.Name = "ProxyLogTab";
            this.ProxyLogTab.Size = new System.Drawing.Size(697, 203);
            this.ProxyLogTab.TabIndex = 3;
            this.ProxyLogTab.Text = "Proxy Logs";
            this.ProxyLogTab.UseVisualStyleBackColor = true;
            // 
            // ProxyLogGrid
            // 
            this.ProxyLogGrid.AllowUserToAddRows = false;
            this.ProxyLogGrid.AllowUserToDeleteRows = false;
            this.ProxyLogGrid.AllowUserToOrderColumns = true;
            this.ProxyLogGrid.AllowUserToResizeRows = false;
            this.ProxyLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProxyLogGrid.BackgroundColor = System.Drawing.Color.White;
            this.ProxyLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProxyLogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ProxyLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ProxyLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProxyLogGridColumnForID,
            this.ProxyLogGridColumnForHostName,
            this.ProxyLogGridColumnForMethod,
            this.ProxyLogGridColumnForURL,
            this.ProxyLogGridColumnForFile,
            this.ProxyLogGridColumnForSSL,
            this.ProxyLogGridColumnForParameters,
            this.ProxyLogGridColumnForCode,
            this.ProxyLogGridColumnForLength,
            this.ProxyLogGridColumnForMIME,
            this.ProxyLogGridColumnForSetCookie,
            this.ProxyLogGridColumnForEdited,
            this.ProxyLogGridColumnForNotes});
            this.ProxyLogGrid.ContextMenuStrip = this.LogMenu;
            this.ProxyLogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProxyLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ProxyLogGrid.GridColor = System.Drawing.Color.White;
            this.ProxyLogGrid.Location = new System.Drawing.Point(0, 0);
            this.ProxyLogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProxyLogGrid.MultiSelect = false;
            this.ProxyLogGrid.Name = "ProxyLogGrid";
            this.ProxyLogGrid.ReadOnly = true;
            this.ProxyLogGrid.RowHeadersVisible = false;
            this.ProxyLogGrid.RowHeadersWidth = 10;
            this.ProxyLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProxyLogGrid.Size = new System.Drawing.Size(697, 203);
            this.ProxyLogGrid.TabIndex = 2;
            this.ProxyLogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LogGrid_CellClick);
            // 
            // ProxyLogGridColumnForID
            // 
            this.ProxyLogGridColumnForID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForID.HeaderText = "ID";
            this.ProxyLogGridColumnForID.MinimumWidth = 50;
            this.ProxyLogGridColumnForID.Name = "ProxyLogGridColumnForID";
            this.ProxyLogGridColumnForID.ReadOnly = true;
            this.ProxyLogGridColumnForID.Width = 50;
            // 
            // ProxyLogGridColumnForHostName
            // 
            this.ProxyLogGridColumnForHostName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForHostName.HeaderText = "HOSTNAME";
            this.ProxyLogGridColumnForHostName.Name = "ProxyLogGridColumnForHostName";
            this.ProxyLogGridColumnForHostName.ReadOnly = true;
            this.ProxyLogGridColumnForHostName.Width = 120;
            // 
            // ProxyLogGridColumnForMethod
            // 
            this.ProxyLogGridColumnForMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForMethod.HeaderText = "METHOD";
            this.ProxyLogGridColumnForMethod.Name = "ProxyLogGridColumnForMethod";
            this.ProxyLogGridColumnForMethod.ReadOnly = true;
            this.ProxyLogGridColumnForMethod.Width = 60;
            // 
            // ProxyLogGridColumnForURL
            // 
            this.ProxyLogGridColumnForURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProxyLogGridColumnForURL.HeaderText = "URL";
            this.ProxyLogGridColumnForURL.MinimumWidth = 150;
            this.ProxyLogGridColumnForURL.Name = "ProxyLogGridColumnForURL";
            this.ProxyLogGridColumnForURL.ReadOnly = true;
            // 
            // ProxyLogGridColumnForFile
            // 
            this.ProxyLogGridColumnForFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForFile.HeaderText = "FILE";
            this.ProxyLogGridColumnForFile.Name = "ProxyLogGridColumnForFile";
            this.ProxyLogGridColumnForFile.ReadOnly = true;
            this.ProxyLogGridColumnForFile.Width = 40;
            // 
            // ProxyLogGridColumnForSSL
            // 
            this.ProxyLogGridColumnForSSL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForSSL.HeaderText = "SSL";
            this.ProxyLogGridColumnForSSL.Name = "ProxyLogGridColumnForSSL";
            this.ProxyLogGridColumnForSSL.ReadOnly = true;
            this.ProxyLogGridColumnForSSL.Width = 30;
            // 
            // ProxyLogGridColumnForParameters
            // 
            this.ProxyLogGridColumnForParameters.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForParameters.HeaderText = "PARAMETERS";
            this.ProxyLogGridColumnForParameters.Name = "ProxyLogGridColumnForParameters";
            this.ProxyLogGridColumnForParameters.ReadOnly = true;
            this.ProxyLogGridColumnForParameters.Width = 85;
            // 
            // ProxyLogGridColumnForCode
            // 
            this.ProxyLogGridColumnForCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForCode.HeaderText = "CODE";
            this.ProxyLogGridColumnForCode.Name = "ProxyLogGridColumnForCode";
            this.ProxyLogGridColumnForCode.ReadOnly = true;
            this.ProxyLogGridColumnForCode.Width = 45;
            // 
            // ProxyLogGridColumnForLength
            // 
            this.ProxyLogGridColumnForLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForLength.HeaderText = "LENGTH";
            this.ProxyLogGridColumnForLength.Name = "ProxyLogGridColumnForLength";
            this.ProxyLogGridColumnForLength.ReadOnly = true;
            this.ProxyLogGridColumnForLength.Width = 55;
            // 
            // ProxyLogGridColumnForMIME
            // 
            this.ProxyLogGridColumnForMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForMIME.HeaderText = "MIME";
            this.ProxyLogGridColumnForMIME.Name = "ProxyLogGridColumnForMIME";
            this.ProxyLogGridColumnForMIME.ReadOnly = true;
            this.ProxyLogGridColumnForMIME.Width = 60;
            // 
            // ProxyLogGridColumnForSetCookie
            // 
            this.ProxyLogGridColumnForSetCookie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForSetCookie.HeaderText = "SET-COOKIE";
            this.ProxyLogGridColumnForSetCookie.Name = "ProxyLogGridColumnForSetCookie";
            this.ProxyLogGridColumnForSetCookie.ReadOnly = true;
            this.ProxyLogGridColumnForSetCookie.Width = 80;
            // 
            // ProxyLogGridColumnForEdited
            // 
            this.ProxyLogGridColumnForEdited.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForEdited.HeaderText = "EDITIED";
            this.ProxyLogGridColumnForEdited.Name = "ProxyLogGridColumnForEdited";
            this.ProxyLogGridColumnForEdited.ReadOnly = true;
            this.ProxyLogGridColumnForEdited.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProxyLogGridColumnForEdited.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ProxyLogGridColumnForEdited.Width = 60;
            // 
            // ProxyLogGridColumnForNotes
            // 
            this.ProxyLogGridColumnForNotes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProxyLogGridColumnForNotes.HeaderText = "NOTES";
            this.ProxyLogGridColumnForNotes.Name = "ProxyLogGridColumnForNotes";
            this.ProxyLogGridColumnForNotes.ReadOnly = true;
            this.ProxyLogGridColumnForNotes.Visible = false;
            this.ProxyLogGridColumnForNotes.Width = 80;
            // 
            // ScanLogTab
            // 
            this.ScanLogTab.Controls.Add(this.ScanLogGrid);
            this.ScanLogTab.Location = new System.Drawing.Point(4, 22);
            this.ScanLogTab.Margin = new System.Windows.Forms.Padding(0);
            this.ScanLogTab.Name = "ScanLogTab";
            this.ScanLogTab.Size = new System.Drawing.Size(697, 203);
            this.ScanLogTab.TabIndex = 0;
            this.ScanLogTab.Text = "Scan Logs";
            this.ScanLogTab.UseVisualStyleBackColor = true;
            // 
            // ScanLogGrid
            // 
            this.ScanLogGrid.AllowUserToAddRows = false;
            this.ScanLogGrid.AllowUserToDeleteRows = false;
            this.ScanLogGrid.AllowUserToOrderColumns = true;
            this.ScanLogGrid.AllowUserToResizeRows = false;
            this.ScanLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ScanLogGrid.BackgroundColor = System.Drawing.Color.White;
            this.ScanLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScanLogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.ScanLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ScanLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ScanLogGridColumnForID,
            this.ScanLogGridColumnForScanID,
            this.ScanLogGridColumnForHost,
            this.ScanLogGridColumnForMethod,
            this.ScanLogGridColumnForURL,
            this.ScanLogGridColumnForFile,
            this.ScanLogGridColumnForSSL,
            this.ScanLogGridColumnForParameters,
            this.ScanLogGridColumnForCode,
            this.ScanLogGridColumnForLength,
            this.ScanLogGridColumnForMIME,
            this.ScanLogGridColumnForSetCookie});
            this.ScanLogGrid.ContextMenuStrip = this.LogMenu;
            this.ScanLogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ScanLogGrid.GridColor = System.Drawing.Color.White;
            this.ScanLogGrid.Location = new System.Drawing.Point(0, 0);
            this.ScanLogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ScanLogGrid.MultiSelect = false;
            this.ScanLogGrid.Name = "ScanLogGrid";
            this.ScanLogGrid.ReadOnly = true;
            this.ScanLogGrid.RowHeadersVisible = false;
            this.ScanLogGrid.RowHeadersWidth = 10;
            this.ScanLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ScanLogGrid.Size = new System.Drawing.Size(697, 203);
            this.ScanLogGrid.TabIndex = 6;
            this.ScanLogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScanLogGrid_CellClick);
            // 
            // ScanLogGridColumnForID
            // 
            this.ScanLogGridColumnForID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScanLogGridColumnForID.HeaderText = "ID";
            this.ScanLogGridColumnForID.MinimumWidth = 50;
            this.ScanLogGridColumnForID.Name = "ScanLogGridColumnForID";
            this.ScanLogGridColumnForID.ReadOnly = true;
            this.ScanLogGridColumnForID.Width = 50;
            // 
            // ScanLogGridColumnForScanID
            // 
            this.ScanLogGridColumnForScanID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScanLogGridColumnForScanID.HeaderText = "SCAN ID";
            this.ScanLogGridColumnForScanID.MinimumWidth = 60;
            this.ScanLogGridColumnForScanID.Name = "ScanLogGridColumnForScanID";
            this.ScanLogGridColumnForScanID.ReadOnly = true;
            this.ScanLogGridColumnForScanID.Width = 60;
            // 
            // ScanLogGridColumnForHost
            // 
            this.ScanLogGridColumnForHost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScanLogGridColumnForHost.HeaderText = "HOSTNAME";
            this.ScanLogGridColumnForHost.Name = "ScanLogGridColumnForHost";
            this.ScanLogGridColumnForHost.ReadOnly = true;
            this.ScanLogGridColumnForHost.Width = 120;
            // 
            // ScanLogGridColumnForMethod
            // 
            this.ScanLogGridColumnForMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScanLogGridColumnForMethod.HeaderText = "METHOD";
            this.ScanLogGridColumnForMethod.Name = "ScanLogGridColumnForMethod";
            this.ScanLogGridColumnForMethod.ReadOnly = true;
            this.ScanLogGridColumnForMethod.Width = 60;
            // 
            // ScanLogGridColumnForURL
            // 
            this.ScanLogGridColumnForURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ScanLogGridColumnForURL.HeaderText = "URL";
            this.ScanLogGridColumnForURL.MinimumWidth = 150;
            this.ScanLogGridColumnForURL.Name = "ScanLogGridColumnForURL";
            this.ScanLogGridColumnForURL.ReadOnly = true;
            // 
            // ScanLogGridColumnForFile
            // 
            this.ScanLogGridColumnForFile.HeaderText = "FILE";
            this.ScanLogGridColumnForFile.Name = "ScanLogGridColumnForFile";
            this.ScanLogGridColumnForFile.ReadOnly = true;
            // 
            // ScanLogGridColumnForSSL
            // 
            this.ScanLogGridColumnForSSL.HeaderText = "SSL";
            this.ScanLogGridColumnForSSL.Name = "ScanLogGridColumnForSSL";
            this.ScanLogGridColumnForSSL.ReadOnly = true;
            // 
            // ScanLogGridColumnForParameters
            // 
            this.ScanLogGridColumnForParameters.HeaderText = "PARAMETERS";
            this.ScanLogGridColumnForParameters.Name = "ScanLogGridColumnForParameters";
            this.ScanLogGridColumnForParameters.ReadOnly = true;
            // 
            // ScanLogGridColumnForCode
            // 
            this.ScanLogGridColumnForCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScanLogGridColumnForCode.HeaderText = "CODE";
            this.ScanLogGridColumnForCode.Name = "ScanLogGridColumnForCode";
            this.ScanLogGridColumnForCode.ReadOnly = true;
            this.ScanLogGridColumnForCode.Width = 60;
            // 
            // ScanLogGridColumnForLength
            // 
            this.ScanLogGridColumnForLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScanLogGridColumnForLength.HeaderText = "LENGTH";
            this.ScanLogGridColumnForLength.Name = "ScanLogGridColumnForLength";
            this.ScanLogGridColumnForLength.ReadOnly = true;
            this.ScanLogGridColumnForLength.Width = 60;
            // 
            // ScanLogGridColumnForMIME
            // 
            this.ScanLogGridColumnForMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScanLogGridColumnForMIME.HeaderText = "MIME";
            this.ScanLogGridColumnForMIME.Name = "ScanLogGridColumnForMIME";
            this.ScanLogGridColumnForMIME.ReadOnly = true;
            this.ScanLogGridColumnForMIME.Width = 70;
            // 
            // ScanLogGridColumnForSetCookie
            // 
            this.ScanLogGridColumnForSetCookie.HeaderText = "SET-COOKIE";
            this.ScanLogGridColumnForSetCookie.Name = "ScanLogGridColumnForSetCookie";
            this.ScanLogGridColumnForSetCookie.ReadOnly = true;
            // 
            // TestLogTab
            // 
            this.TestLogTab.Controls.Add(this.TestLogGrid);
            this.TestLogTab.Location = new System.Drawing.Point(4, 22);
            this.TestLogTab.Margin = new System.Windows.Forms.Padding(0);
            this.TestLogTab.Name = "TestLogTab";
            this.TestLogTab.Size = new System.Drawing.Size(697, 203);
            this.TestLogTab.TabIndex = 1;
            this.TestLogTab.Text = "Test Logs";
            this.TestLogTab.UseVisualStyleBackColor = true;
            // 
            // TestLogGrid
            // 
            this.TestLogGrid.AllowUserToAddRows = false;
            this.TestLogGrid.AllowUserToDeleteRows = false;
            this.TestLogGrid.AllowUserToOrderColumns = true;
            this.TestLogGrid.AllowUserToResizeRows = false;
            this.TestLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TestLogGrid.BackgroundColor = System.Drawing.Color.White;
            this.TestLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TestLogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.TestLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TestLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MTLogGridColumnForID,
            this.MTLogGridColumnForHostName,
            this.MTLogGridColumnForMethod,
            this.MTLogGridColumnForURL,
            this.MTLogGridColumnForFile,
            this.MTLogGridColumnForSSL,
            this.MTLogGridColumnForParameters,
            this.MTLogGridColumnForCode,
            this.MTLogGridColumnForLength,
            this.MTLogGridColumnForMIME,
            this.MTLogGridColumnForSetCookie});
            this.TestLogGrid.ContextMenuStrip = this.LogMenu;
            this.TestLogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TestLogGrid.GridColor = System.Drawing.Color.White;
            this.TestLogGrid.Location = new System.Drawing.Point(0, 0);
            this.TestLogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.TestLogGrid.MultiSelect = false;
            this.TestLogGrid.Name = "TestLogGrid";
            this.TestLogGrid.ReadOnly = true;
            this.TestLogGrid.RowHeadersVisible = false;
            this.TestLogGrid.RowHeadersWidth = 10;
            this.TestLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TestLogGrid.Size = new System.Drawing.Size(697, 203);
            this.TestLogGrid.TabIndex = 4;
            this.TestLogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MTLogGrid_CellClick);
            // 
            // MTLogGridColumnForID
            // 
            this.MTLogGridColumnForID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForID.HeaderText = "ID";
            this.MTLogGridColumnForID.MinimumWidth = 50;
            this.MTLogGridColumnForID.Name = "MTLogGridColumnForID";
            this.MTLogGridColumnForID.ReadOnly = true;
            this.MTLogGridColumnForID.Width = 50;
            // 
            // MTLogGridColumnForHostName
            // 
            this.MTLogGridColumnForHostName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForHostName.HeaderText = "HOSTNAME";
            this.MTLogGridColumnForHostName.Name = "MTLogGridColumnForHostName";
            this.MTLogGridColumnForHostName.ReadOnly = true;
            this.MTLogGridColumnForHostName.Width = 120;
            // 
            // MTLogGridColumnForMethod
            // 
            this.MTLogGridColumnForMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForMethod.HeaderText = "METHOD";
            this.MTLogGridColumnForMethod.Name = "MTLogGridColumnForMethod";
            this.MTLogGridColumnForMethod.ReadOnly = true;
            this.MTLogGridColumnForMethod.Width = 60;
            // 
            // MTLogGridColumnForURL
            // 
            this.MTLogGridColumnForURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MTLogGridColumnForURL.HeaderText = "URL";
            this.MTLogGridColumnForURL.MinimumWidth = 150;
            this.MTLogGridColumnForURL.Name = "MTLogGridColumnForURL";
            this.MTLogGridColumnForURL.ReadOnly = true;
            // 
            // MTLogGridColumnForFile
            // 
            this.MTLogGridColumnForFile.HeaderText = "FILE";
            this.MTLogGridColumnForFile.Name = "MTLogGridColumnForFile";
            this.MTLogGridColumnForFile.ReadOnly = true;
            // 
            // MTLogGridColumnForSSL
            // 
            this.MTLogGridColumnForSSL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForSSL.HeaderText = "SSL";
            this.MTLogGridColumnForSSL.Name = "MTLogGridColumnForSSL";
            this.MTLogGridColumnForSSL.ReadOnly = true;
            this.MTLogGridColumnForSSL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MTLogGridColumnForSSL.Width = 30;
            // 
            // MTLogGridColumnForParameters
            // 
            this.MTLogGridColumnForParameters.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForParameters.HeaderText = "PARAMETERS";
            this.MTLogGridColumnForParameters.Name = "MTLogGridColumnForParameters";
            this.MTLogGridColumnForParameters.ReadOnly = true;
            this.MTLogGridColumnForParameters.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MTLogGridColumnForParameters.Width = 90;
            // 
            // MTLogGridColumnForCode
            // 
            this.MTLogGridColumnForCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForCode.HeaderText = "CODE";
            this.MTLogGridColumnForCode.Name = "MTLogGridColumnForCode";
            this.MTLogGridColumnForCode.ReadOnly = true;
            this.MTLogGridColumnForCode.Width = 60;
            // 
            // MTLogGridColumnForLength
            // 
            this.MTLogGridColumnForLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForLength.HeaderText = "LENGTH";
            this.MTLogGridColumnForLength.Name = "MTLogGridColumnForLength";
            this.MTLogGridColumnForLength.ReadOnly = true;
            this.MTLogGridColumnForLength.Width = 60;
            // 
            // MTLogGridColumnForMIME
            // 
            this.MTLogGridColumnForMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForMIME.HeaderText = "MIME";
            this.MTLogGridColumnForMIME.Name = "MTLogGridColumnForMIME";
            this.MTLogGridColumnForMIME.ReadOnly = true;
            this.MTLogGridColumnForMIME.Width = 70;
            // 
            // MTLogGridColumnForSetCookie
            // 
            this.MTLogGridColumnForSetCookie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MTLogGridColumnForSetCookie.HeaderText = "SET-COOKIE";
            this.MTLogGridColumnForSetCookie.Name = "MTLogGridColumnForSetCookie";
            this.MTLogGridColumnForSetCookie.ReadOnly = true;
            this.MTLogGridColumnForSetCookie.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MTLogGridColumnForSetCookie.Width = 80;
            // 
            // ShellLogTab
            // 
            this.ShellLogTab.Controls.Add(this.ShellLogGrid);
            this.ShellLogTab.Location = new System.Drawing.Point(4, 22);
            this.ShellLogTab.Margin = new System.Windows.Forms.Padding(0);
            this.ShellLogTab.Name = "ShellLogTab";
            this.ShellLogTab.Size = new System.Drawing.Size(697, 203);
            this.ShellLogTab.TabIndex = 2;
            this.ShellLogTab.Text = "Shell Logs";
            this.ShellLogTab.UseVisualStyleBackColor = true;
            // 
            // ShellLogGrid
            // 
            this.ShellLogGrid.AllowUserToAddRows = false;
            this.ShellLogGrid.AllowUserToDeleteRows = false;
            this.ShellLogGrid.AllowUserToOrderColumns = true;
            this.ShellLogGrid.AllowUserToResizeRows = false;
            this.ShellLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ShellLogGrid.BackgroundColor = System.Drawing.Color.White;
            this.ShellLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ShellLogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.ShellLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ShellLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ScriptingLogGridColumnForID,
            this.ScriptingLogGridColumnForHostName,
            this.ScriptingLogGridColumnForMethod,
            this.ScriptingLogGridColumnForURL,
            this.ScriptingLogGridColumnForFile,
            this.ScriptingLogGridColumnForSSL,
            this.ScriptingLogGridColumnForParameters,
            this.ScriptingLogGridColumnForCode,
            this.ScriptingLogGridColumnForLength,
            this.ScriptingLogGridColumnForMIME,
            this.ScriptingLogGridColumnForSetCookie});
            this.ShellLogGrid.ContextMenuStrip = this.LogMenu;
            this.ShellLogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShellLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ShellLogGrid.GridColor = System.Drawing.Color.White;
            this.ShellLogGrid.Location = new System.Drawing.Point(0, 0);
            this.ShellLogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ShellLogGrid.MultiSelect = false;
            this.ShellLogGrid.Name = "ShellLogGrid";
            this.ShellLogGrid.ReadOnly = true;
            this.ShellLogGrid.RowHeadersVisible = false;
            this.ShellLogGrid.RowHeadersWidth = 10;
            this.ShellLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ShellLogGrid.Size = new System.Drawing.Size(697, 203);
            this.ShellLogGrid.TabIndex = 5;
            this.ShellLogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShellLogGrid_CellClick);
            // 
            // ScriptingLogGridColumnForID
            // 
            this.ScriptingLogGridColumnForID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScriptingLogGridColumnForID.HeaderText = "ID";
            this.ScriptingLogGridColumnForID.MinimumWidth = 50;
            this.ScriptingLogGridColumnForID.Name = "ScriptingLogGridColumnForID";
            this.ScriptingLogGridColumnForID.ReadOnly = true;
            this.ScriptingLogGridColumnForID.Width = 50;
            // 
            // ScriptingLogGridColumnForHostName
            // 
            this.ScriptingLogGridColumnForHostName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScriptingLogGridColumnForHostName.HeaderText = "HOSTNAME";
            this.ScriptingLogGridColumnForHostName.Name = "ScriptingLogGridColumnForHostName";
            this.ScriptingLogGridColumnForHostName.ReadOnly = true;
            this.ScriptingLogGridColumnForHostName.Width = 120;
            // 
            // ScriptingLogGridColumnForMethod
            // 
            this.ScriptingLogGridColumnForMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScriptingLogGridColumnForMethod.HeaderText = "METHOD";
            this.ScriptingLogGridColumnForMethod.Name = "ScriptingLogGridColumnForMethod";
            this.ScriptingLogGridColumnForMethod.ReadOnly = true;
            this.ScriptingLogGridColumnForMethod.Width = 60;
            // 
            // ScriptingLogGridColumnForURL
            // 
            this.ScriptingLogGridColumnForURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ScriptingLogGridColumnForURL.HeaderText = "URL";
            this.ScriptingLogGridColumnForURL.MinimumWidth = 150;
            this.ScriptingLogGridColumnForURL.Name = "ScriptingLogGridColumnForURL";
            this.ScriptingLogGridColumnForURL.ReadOnly = true;
            // 
            // ScriptingLogGridColumnForFile
            // 
            this.ScriptingLogGridColumnForFile.HeaderText = "FILE";
            this.ScriptingLogGridColumnForFile.Name = "ScriptingLogGridColumnForFile";
            this.ScriptingLogGridColumnForFile.ReadOnly = true;
            // 
            // ScriptingLogGridColumnForSSL
            // 
            this.ScriptingLogGridColumnForSSL.HeaderText = "SSL";
            this.ScriptingLogGridColumnForSSL.Name = "ScriptingLogGridColumnForSSL";
            this.ScriptingLogGridColumnForSSL.ReadOnly = true;
            // 
            // ScriptingLogGridColumnForParameters
            // 
            this.ScriptingLogGridColumnForParameters.HeaderText = "PARAMETERS";
            this.ScriptingLogGridColumnForParameters.Name = "ScriptingLogGridColumnForParameters";
            this.ScriptingLogGridColumnForParameters.ReadOnly = true;
            // 
            // ScriptingLogGridColumnForCode
            // 
            this.ScriptingLogGridColumnForCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScriptingLogGridColumnForCode.HeaderText = "CODE";
            this.ScriptingLogGridColumnForCode.Name = "ScriptingLogGridColumnForCode";
            this.ScriptingLogGridColumnForCode.ReadOnly = true;
            this.ScriptingLogGridColumnForCode.Width = 60;
            // 
            // ScriptingLogGridColumnForLength
            // 
            this.ScriptingLogGridColumnForLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScriptingLogGridColumnForLength.HeaderText = "LENGTH";
            this.ScriptingLogGridColumnForLength.Name = "ScriptingLogGridColumnForLength";
            this.ScriptingLogGridColumnForLength.ReadOnly = true;
            this.ScriptingLogGridColumnForLength.Width = 60;
            // 
            // ScriptingLogGridColumnForMIME
            // 
            this.ScriptingLogGridColumnForMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScriptingLogGridColumnForMIME.HeaderText = "MIME";
            this.ScriptingLogGridColumnForMIME.Name = "ScriptingLogGridColumnForMIME";
            this.ScriptingLogGridColumnForMIME.ReadOnly = true;
            this.ScriptingLogGridColumnForMIME.Width = 70;
            // 
            // ScriptingLogGridColumnForSetCookie
            // 
            this.ScriptingLogGridColumnForSetCookie.HeaderText = "SET-COOKIE";
            this.ScriptingLogGridColumnForSetCookie.Name = "ScriptingLogGridColumnForSetCookie";
            this.ScriptingLogGridColumnForSetCookie.ReadOnly = true;
            // 
            // ProbeLogTab
            // 
            this.ProbeLogTab.Controls.Add(this.ProbeLogGrid);
            this.ProbeLogTab.Location = new System.Drawing.Point(4, 22);
            this.ProbeLogTab.Margin = new System.Windows.Forms.Padding(0);
            this.ProbeLogTab.Name = "ProbeLogTab";
            this.ProbeLogTab.Size = new System.Drawing.Size(697, 203);
            this.ProbeLogTab.TabIndex = 4;
            this.ProbeLogTab.Text = "Probe Logs";
            this.ProbeLogTab.UseVisualStyleBackColor = true;
            // 
            // ProbeLogGrid
            // 
            this.ProbeLogGrid.AllowUserToAddRows = false;
            this.ProbeLogGrid.AllowUserToDeleteRows = false;
            this.ProbeLogGrid.AllowUserToOrderColumns = true;
            this.ProbeLogGrid.AllowUserToResizeRows = false;
            this.ProbeLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProbeLogGrid.BackgroundColor = System.Drawing.Color.White;
            this.ProbeLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProbeLogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.ProbeLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ProbeLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProbeLogGridColumnForID,
            this.ProbeLogGridColumnForHostName,
            this.ProbeLogGridColumnForMethod,
            this.ProbeLogGridColumnForURL,
            this.ProbeLogGridColumnForFile,
            this.ProbeLogGridColumnForSSL,
            this.ProbeLogGridColumnForParameters,
            this.ProbeLogGridColumnForCode,
            this.ProbeLogGridColumnForLength,
            this.ProbeLogGridColumnForMIME,
            this.ProbeLogGridColumnForSetCookie});
            this.ProbeLogGrid.ContextMenuStrip = this.LogMenu;
            this.ProbeLogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProbeLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ProbeLogGrid.GridColor = System.Drawing.Color.White;
            this.ProbeLogGrid.Location = new System.Drawing.Point(0, 0);
            this.ProbeLogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProbeLogGrid.MultiSelect = false;
            this.ProbeLogGrid.Name = "ProbeLogGrid";
            this.ProbeLogGrid.ReadOnly = true;
            this.ProbeLogGrid.RowHeadersVisible = false;
            this.ProbeLogGrid.RowHeadersWidth = 10;
            this.ProbeLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProbeLogGrid.Size = new System.Drawing.Size(697, 203);
            this.ProbeLogGrid.TabIndex = 7;
            this.ProbeLogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProbeLogGrid_CellClick);
            // 
            // ProbeLogGridColumnForID
            // 
            this.ProbeLogGridColumnForID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProbeLogGridColumnForID.HeaderText = "ID";
            this.ProbeLogGridColumnForID.MinimumWidth = 50;
            this.ProbeLogGridColumnForID.Name = "ProbeLogGridColumnForID";
            this.ProbeLogGridColumnForID.ReadOnly = true;
            this.ProbeLogGridColumnForID.Width = 50;
            // 
            // ProbeLogGridColumnForHostName
            // 
            this.ProbeLogGridColumnForHostName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProbeLogGridColumnForHostName.HeaderText = "HOSTNAME";
            this.ProbeLogGridColumnForHostName.Name = "ProbeLogGridColumnForHostName";
            this.ProbeLogGridColumnForHostName.ReadOnly = true;
            this.ProbeLogGridColumnForHostName.Width = 120;
            // 
            // ProbeLogGridColumnForMethod
            // 
            this.ProbeLogGridColumnForMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProbeLogGridColumnForMethod.HeaderText = "METHOD";
            this.ProbeLogGridColumnForMethod.Name = "ProbeLogGridColumnForMethod";
            this.ProbeLogGridColumnForMethod.ReadOnly = true;
            this.ProbeLogGridColumnForMethod.Width = 60;
            // 
            // ProbeLogGridColumnForURL
            // 
            this.ProbeLogGridColumnForURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProbeLogGridColumnForURL.HeaderText = "URL";
            this.ProbeLogGridColumnForURL.MinimumWidth = 150;
            this.ProbeLogGridColumnForURL.Name = "ProbeLogGridColumnForURL";
            this.ProbeLogGridColumnForURL.ReadOnly = true;
            // 
            // ProbeLogGridColumnForFile
            // 
            this.ProbeLogGridColumnForFile.HeaderText = "FILE";
            this.ProbeLogGridColumnForFile.Name = "ProbeLogGridColumnForFile";
            this.ProbeLogGridColumnForFile.ReadOnly = true;
            // 
            // ProbeLogGridColumnForSSL
            // 
            this.ProbeLogGridColumnForSSL.HeaderText = "SSL";
            this.ProbeLogGridColumnForSSL.Name = "ProbeLogGridColumnForSSL";
            this.ProbeLogGridColumnForSSL.ReadOnly = true;
            // 
            // ProbeLogGridColumnForParameters
            // 
            this.ProbeLogGridColumnForParameters.HeaderText = "PARAMETERS";
            this.ProbeLogGridColumnForParameters.Name = "ProbeLogGridColumnForParameters";
            this.ProbeLogGridColumnForParameters.ReadOnly = true;
            // 
            // ProbeLogGridColumnForCode
            // 
            this.ProbeLogGridColumnForCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProbeLogGridColumnForCode.HeaderText = "CODE";
            this.ProbeLogGridColumnForCode.Name = "ProbeLogGridColumnForCode";
            this.ProbeLogGridColumnForCode.ReadOnly = true;
            this.ProbeLogGridColumnForCode.Width = 60;
            // 
            // ProbeLogGridColumnForLength
            // 
            this.ProbeLogGridColumnForLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProbeLogGridColumnForLength.HeaderText = "LENGTH";
            this.ProbeLogGridColumnForLength.Name = "ProbeLogGridColumnForLength";
            this.ProbeLogGridColumnForLength.ReadOnly = true;
            this.ProbeLogGridColumnForLength.Width = 60;
            // 
            // ProbeLogGridColumnForMIME
            // 
            this.ProbeLogGridColumnForMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProbeLogGridColumnForMIME.HeaderText = "MIME";
            this.ProbeLogGridColumnForMIME.Name = "ProbeLogGridColumnForMIME";
            this.ProbeLogGridColumnForMIME.ReadOnly = true;
            this.ProbeLogGridColumnForMIME.Width = 70;
            // 
            // ProbeLogGridColumnForSetCookie
            // 
            this.ProbeLogGridColumnForSetCookie.HeaderText = "SET-COOKIE";
            this.ProbeLogGridColumnForSetCookie.Name = "ProbeLogGridColumnForSetCookie";
            this.ProbeLogGridColumnForSetCookie.ReadOnly = true;
            // 
            // SiteMapLogTab
            // 
            this.SiteMapLogTab.Controls.Add(this.SiteMapLogGrid);
            this.SiteMapLogTab.Location = new System.Drawing.Point(4, 22);
            this.SiteMapLogTab.Margin = new System.Windows.Forms.Padding(0);
            this.SiteMapLogTab.Name = "SiteMapLogTab";
            this.SiteMapLogTab.Size = new System.Drawing.Size(697, 203);
            this.SiteMapLogTab.TabIndex = 5;
            this.SiteMapLogTab.Text = "Sitemap";
            this.SiteMapLogTab.UseVisualStyleBackColor = true;
            // 
            // SiteMapLogGrid
            // 
            this.SiteMapLogGrid.AllowUserToAddRows = false;
            this.SiteMapLogGrid.AllowUserToDeleteRows = false;
            this.SiteMapLogGrid.AllowUserToOrderColumns = true;
            this.SiteMapLogGrid.AllowUserToResizeRows = false;
            this.SiteMapLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SiteMapLogGrid.BackgroundColor = System.Drawing.Color.White;
            this.SiteMapLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SiteMapLogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.SiteMapLogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.SiteMapLogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SiteMapLogGridColumnForID,
            this.SiteMapLogGridColumnForSource,
            this.SiteMapLogGridColumnForHost,
            this.SiteMapLogGridColumnForMethod,
            this.SiteMapLogGridColumnForURL,
            this.SiteMapLogGridColumnForFile,
            this.SiteMapLogGridColumnForSSL,
            this.SiteMapLogGridColumnForParameters,
            this.SiteMapLogGridColumnForCode,
            this.SiteMapLogGridColumnForLength,
            this.SiteMapLogGridColumnForMIME,
            this.SiteMapLogGridColumnForSetCookie});
            this.SiteMapLogGrid.ContextMenuStrip = this.LogMenu;
            this.SiteMapLogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SiteMapLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.SiteMapLogGrid.GridColor = System.Drawing.Color.White;
            this.SiteMapLogGrid.Location = new System.Drawing.Point(0, 0);
            this.SiteMapLogGrid.Margin = new System.Windows.Forms.Padding(0);
            this.SiteMapLogGrid.MultiSelect = false;
            this.SiteMapLogGrid.Name = "SiteMapLogGrid";
            this.SiteMapLogGrid.ReadOnly = true;
            this.SiteMapLogGrid.RowHeadersVisible = false;
            this.SiteMapLogGrid.RowHeadersWidth = 10;
            this.SiteMapLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SiteMapLogGrid.Size = new System.Drawing.Size(697, 203);
            this.SiteMapLogGrid.TabIndex = 7;
            this.SiteMapLogGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SiteMapLogGrid_CellClick);
            // 
            // SiteMapLogGridColumnForID
            // 
            this.SiteMapLogGridColumnForID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteMapLogGridColumnForID.HeaderText = "ID";
            this.SiteMapLogGridColumnForID.MinimumWidth = 50;
            this.SiteMapLogGridColumnForID.Name = "SiteMapLogGridColumnForID";
            this.SiteMapLogGridColumnForID.ReadOnly = true;
            this.SiteMapLogGridColumnForID.Width = 50;
            // 
            // SiteMapLogGridColumnForSource
            // 
            this.SiteMapLogGridColumnForSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteMapLogGridColumnForSource.HeaderText = "SOURCE";
            this.SiteMapLogGridColumnForSource.MinimumWidth = 60;
            this.SiteMapLogGridColumnForSource.Name = "SiteMapLogGridColumnForSource";
            this.SiteMapLogGridColumnForSource.ReadOnly = true;
            this.SiteMapLogGridColumnForSource.Width = 60;
            // 
            // SiteMapLogGridColumnForHost
            // 
            this.SiteMapLogGridColumnForHost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteMapLogGridColumnForHost.HeaderText = "HOSTNAME";
            this.SiteMapLogGridColumnForHost.Name = "SiteMapLogGridColumnForHost";
            this.SiteMapLogGridColumnForHost.ReadOnly = true;
            this.SiteMapLogGridColumnForHost.Width = 120;
            // 
            // SiteMapLogGridColumnForMethod
            // 
            this.SiteMapLogGridColumnForMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteMapLogGridColumnForMethod.HeaderText = "METHOD";
            this.SiteMapLogGridColumnForMethod.Name = "SiteMapLogGridColumnForMethod";
            this.SiteMapLogGridColumnForMethod.ReadOnly = true;
            this.SiteMapLogGridColumnForMethod.Width = 60;
            // 
            // SiteMapLogGridColumnForURL
            // 
            this.SiteMapLogGridColumnForURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SiteMapLogGridColumnForURL.HeaderText = "URL";
            this.SiteMapLogGridColumnForURL.MinimumWidth = 150;
            this.SiteMapLogGridColumnForURL.Name = "SiteMapLogGridColumnForURL";
            this.SiteMapLogGridColumnForURL.ReadOnly = true;
            // 
            // SiteMapLogGridColumnForFile
            // 
            this.SiteMapLogGridColumnForFile.HeaderText = "FILE";
            this.SiteMapLogGridColumnForFile.Name = "SiteMapLogGridColumnForFile";
            this.SiteMapLogGridColumnForFile.ReadOnly = true;
            // 
            // SiteMapLogGridColumnForSSL
            // 
            this.SiteMapLogGridColumnForSSL.HeaderText = "SSL";
            this.SiteMapLogGridColumnForSSL.Name = "SiteMapLogGridColumnForSSL";
            this.SiteMapLogGridColumnForSSL.ReadOnly = true;
            // 
            // SiteMapLogGridColumnForParameters
            // 
            this.SiteMapLogGridColumnForParameters.HeaderText = "PARAMETERS";
            this.SiteMapLogGridColumnForParameters.Name = "SiteMapLogGridColumnForParameters";
            this.SiteMapLogGridColumnForParameters.ReadOnly = true;
            // 
            // SiteMapLogGridColumnForCode
            // 
            this.SiteMapLogGridColumnForCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteMapLogGridColumnForCode.HeaderText = "CODE";
            this.SiteMapLogGridColumnForCode.Name = "SiteMapLogGridColumnForCode";
            this.SiteMapLogGridColumnForCode.ReadOnly = true;
            this.SiteMapLogGridColumnForCode.Width = 60;
            // 
            // SiteMapLogGridColumnForLength
            // 
            this.SiteMapLogGridColumnForLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteMapLogGridColumnForLength.HeaderText = "LENGTH";
            this.SiteMapLogGridColumnForLength.Name = "SiteMapLogGridColumnForLength";
            this.SiteMapLogGridColumnForLength.ReadOnly = true;
            this.SiteMapLogGridColumnForLength.Width = 60;
            // 
            // SiteMapLogGridColumnForMIME
            // 
            this.SiteMapLogGridColumnForMIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteMapLogGridColumnForMIME.HeaderText = "MIME";
            this.SiteMapLogGridColumnForMIME.Name = "SiteMapLogGridColumnForMIME";
            this.SiteMapLogGridColumnForMIME.ReadOnly = true;
            this.SiteMapLogGridColumnForMIME.Width = 70;
            // 
            // SiteMapLogGridColumnForSetCookie
            // 
            this.SiteMapLogGridColumnForSetCookie.HeaderText = "SET-COOKIE";
            this.SiteMapLogGridColumnForSetCookie.Name = "SiteMapLogGridColumnForSetCookie";
            this.SiteMapLogGridColumnForSetCookie.ReadOnly = true;
            // 
            // mt_results
            // 
            this.mt_results.Controls.Add(this.ResultsTabMainSplit);
            this.mt_results.Location = new System.Drawing.Point(4, 22);
            this.mt_results.Margin = new System.Windows.Forms.Padding(0);
            this.mt_results.Name = "mt_results";
            this.mt_results.Size = new System.Drawing.Size(705, 512);
            this.mt_results.TabIndex = 6;
            this.mt_results.Text = "Results";
            this.mt_results.UseVisualStyleBackColor = true;
            // 
            // ResultsTabMainSplit
            // 
            this.ResultsTabMainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTabMainSplit.Location = new System.Drawing.Point(0, 0);
            this.ResultsTabMainSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTabMainSplit.Name = "ResultsTabMainSplit";
            this.ResultsTabMainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ResultsTabMainSplit.Panel1
            // 
            this.ResultsTabMainSplit.Panel1.Controls.Add(this.ResultsTopSplit);
            // 
            // ResultsTabMainSplit.Panel2
            // 
            this.ResultsTabMainSplit.Panel2.Controls.Add(this.ResultsDisplayTabs);
            this.ResultsTabMainSplit.Size = new System.Drawing.Size(705, 512);
            this.ResultsTabMainSplit.SplitterDistance = 226;
            this.ResultsTabMainSplit.SplitterWidth = 2;
            this.ResultsTabMainSplit.TabIndex = 0;
            // 
            // ResultsTopSplit
            // 
            this.ResultsTopSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTopSplit.Location = new System.Drawing.Point(0, 0);
            this.ResultsTopSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTopSplit.Name = "ResultsTopSplit";
            // 
            // ResultsTopSplit.Panel1
            // 
            this.ResultsTopSplit.Panel1.Controls.Add(this.ResultsDisplayRTB);
            // 
            // ResultsTopSplit.Panel2
            // 
            this.ResultsTopSplit.Panel2.Controls.Add(this.ResultsTriggersMainSplit);
            this.ResultsTopSplit.Size = new System.Drawing.Size(705, 226);
            this.ResultsTopSplit.SplitterDistance = 416;
            this.ResultsTopSplit.SplitterWidth = 2;
            this.ResultsTopSplit.TabIndex = 0;
            // 
            // ResultsDisplayRTB
            // 
            this.ResultsDisplayRTB.BackColor = System.Drawing.SystemColors.Window;
            this.ResultsDisplayRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResultsDisplayRTB.DetectUrls = false;
            this.ResultsDisplayRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsDisplayRTB.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsDisplayRTB.Location = new System.Drawing.Point(0, 0);
            this.ResultsDisplayRTB.Name = "ResultsDisplayRTB";
            this.ResultsDisplayRTB.ReadOnly = true;
            this.ResultsDisplayRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ResultsDisplayRTB.Size = new System.Drawing.Size(416, 226);
            this.ResultsDisplayRTB.TabIndex = 0;
            this.ResultsDisplayRTB.Text = "";
            // 
            // ResultsTriggersMainSplit
            // 
            this.ResultsTriggersMainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTriggersMainSplit.Location = new System.Drawing.Point(0, 0);
            this.ResultsTriggersMainSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTriggersMainSplit.Name = "ResultsTriggersMainSplit";
            // 
            // ResultsTriggersMainSplit.Panel1
            // 
            this.ResultsTriggersMainSplit.Panel1.Controls.Add(this.ResultsTriggersGrid);
            // 
            // ResultsTriggersMainSplit.Panel2
            // 
            this.ResultsTriggersMainSplit.Panel2.Controls.Add(this.ResultsTriggersSplit);
            this.ResultsTriggersMainSplit.Size = new System.Drawing.Size(287, 226);
            this.ResultsTriggersMainSplit.SplitterDistance = 54;
            this.ResultsTriggersMainSplit.SplitterWidth = 2;
            this.ResultsTriggersMainSplit.TabIndex = 0;
            // 
            // ResultsTriggersGrid
            // 
            this.ResultsTriggersGrid.AllowUserToAddRows = false;
            this.ResultsTriggersGrid.AllowUserToDeleteRows = false;
            this.ResultsTriggersGrid.AllowUserToResizeRows = false;
            this.ResultsTriggersGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ResultsTriggersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResultsTriggersGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ResultsTriggersGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.ResultsTriggersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsTriggersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ResultsTriggerGridNumberColumn});
            this.ResultsTriggersGrid.ContextMenuStrip = this.LogMenu;
            this.ResultsTriggersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTriggersGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ResultsTriggersGrid.EnableHeadersVisualStyles = false;
            this.ResultsTriggersGrid.GridColor = System.Drawing.Color.White;
            this.ResultsTriggersGrid.Location = new System.Drawing.Point(0, 0);
            this.ResultsTriggersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTriggersGrid.MultiSelect = false;
            this.ResultsTriggersGrid.Name = "ResultsTriggersGrid";
            this.ResultsTriggersGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ResultsTriggersGrid.RowHeadersVisible = false;
            this.ResultsTriggersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ResultsTriggersGrid.Size = new System.Drawing.Size(54, 226);
            this.ResultsTriggersGrid.TabIndex = 1;
            this.ResultsTriggersGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultsTriggersGrid_CellClick);
            // 
            // ResultsTriggerGridNumberColumn
            // 
            this.ResultsTriggerGridNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ResultsTriggerGridNumberColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.ResultsTriggerGridNumberColumn.HeaderText = "Triggers:";
            this.ResultsTriggerGridNumberColumn.Name = "ResultsTriggerGridNumberColumn";
            this.ResultsTriggerGridNumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ResultsTriggersSplit
            // 
            this.ResultsTriggersSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTriggersSplit.Location = new System.Drawing.Point(0, 0);
            this.ResultsTriggersSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsTriggersSplit.Name = "ResultsTriggersSplit";
            this.ResultsTriggersSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ResultsTriggersSplit.Panel1
            // 
            this.ResultsTriggersSplit.Panel1.Controls.Add(this.label5);
            this.ResultsTriggersSplit.Panel1.Controls.Add(this.ResultsRequestTriggerTB);
            // 
            // ResultsTriggersSplit.Panel2
            // 
            this.ResultsTriggersSplit.Panel2.Controls.Add(this.label6);
            this.ResultsTriggersSplit.Panel2.Controls.Add(this.ResultsResponseTriggerTB);
            this.ResultsTriggersSplit.Size = new System.Drawing.Size(231, 226);
            this.ResultsTriggersSplit.SplitterDistance = 116;
            this.ResultsTriggersSplit.SplitterWidth = 2;
            this.ResultsTriggersSplit.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Request Trigger:";
            // 
            // ResultsRequestTriggerTB
            // 
            this.ResultsRequestTriggerTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsRequestTriggerTB.BackColor = System.Drawing.SystemColors.Window;
            this.ResultsRequestTriggerTB.Location = new System.Drawing.Point(0, 16);
            this.ResultsRequestTriggerTB.Multiline = true;
            this.ResultsRequestTriggerTB.Name = "ResultsRequestTriggerTB";
            this.ResultsRequestTriggerTB.ReadOnly = true;
            this.ResultsRequestTriggerTB.Size = new System.Drawing.Size(231, 100);
            this.ResultsRequestTriggerTB.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Response Trigger:";
            // 
            // ResultsResponseTriggerTB
            // 
            this.ResultsResponseTriggerTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsResponseTriggerTB.BackColor = System.Drawing.SystemColors.Window;
            this.ResultsResponseTriggerTB.Location = new System.Drawing.Point(0, 16);
            this.ResultsResponseTriggerTB.Multiline = true;
            this.ResultsResponseTriggerTB.Name = "ResultsResponseTriggerTB";
            this.ResultsResponseTriggerTB.ReadOnly = true;
            this.ResultsResponseTriggerTB.Size = new System.Drawing.Size(231, 92);
            this.ResultsResponseTriggerTB.TabIndex = 1;
            // 
            // ResultsDisplayTabs
            // 
            this.ResultsDisplayTabs.Controls.Add(this.ResultsRequestTab);
            this.ResultsDisplayTabs.Controls.Add(this.ResultsResponseTab);
            this.ResultsDisplayTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsDisplayTabs.Location = new System.Drawing.Point(0, 0);
            this.ResultsDisplayTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsDisplayTabs.Name = "ResultsDisplayTabs";
            this.ResultsDisplayTabs.Padding = new System.Drawing.Point(0, 0);
            this.ResultsDisplayTabs.SelectedIndex = 0;
            this.ResultsDisplayTabs.Size = new System.Drawing.Size(705, 284);
            this.ResultsDisplayTabs.TabIndex = 3;
            // 
            // ResultsRequestTab
            // 
            this.ResultsRequestTab.Controls.Add(this.ResultsRequestIDV);
            this.ResultsRequestTab.Location = new System.Drawing.Point(4, 22);
            this.ResultsRequestTab.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsRequestTab.Name = "ResultsRequestTab";
            this.ResultsRequestTab.Size = new System.Drawing.Size(697, 258);
            this.ResultsRequestTab.TabIndex = 0;
            this.ResultsRequestTab.Text = "Request";
            this.ResultsRequestTab.UseVisualStyleBackColor = true;
            // 
            // ResultsRequestIDV
            // 
            this.ResultsRequestIDV.AutoSize = true;
            this.ResultsRequestIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsRequestIDV.Location = new System.Drawing.Point(0, 0);
            this.ResultsRequestIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsRequestIDV.Name = "ResultsRequestIDV";
            this.ResultsRequestIDV.ReadOnly = true;
            this.ResultsRequestIDV.Size = new System.Drawing.Size(697, 258);
            this.ResultsRequestIDV.TabIndex = 0;
            // 
            // ResultsResponseTab
            // 
            this.ResultsResponseTab.Controls.Add(this.ResultsResponseIDV);
            this.ResultsResponseTab.Location = new System.Drawing.Point(4, 22);
            this.ResultsResponseTab.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsResponseTab.Name = "ResultsResponseTab";
            this.ResultsResponseTab.Size = new System.Drawing.Size(697, 258);
            this.ResultsResponseTab.TabIndex = 1;
            this.ResultsResponseTab.Text = "Response";
            this.ResultsResponseTab.UseVisualStyleBackColor = true;
            // 
            // ResultsResponseIDV
            // 
            this.ResultsResponseIDV.AutoSize = true;
            this.ResultsResponseIDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsResponseIDV.Location = new System.Drawing.Point(0, 0);
            this.ResultsResponseIDV.Margin = new System.Windows.Forms.Padding(0);
            this.ResultsResponseIDV.Name = "ResultsResponseIDV";
            this.ResultsResponseIDV.ReadOnly = true;
            this.ResultsResponseIDV.Size = new System.Drawing.Size(697, 258);
            this.ResultsResponseIDV.TabIndex = 1;
            // 
            // mt_plugins
            // 
            this.mt_plugins.Controls.Add(this.PluginsMainSplit);
            this.mt_plugins.Location = new System.Drawing.Point(4, 22);
            this.mt_plugins.Margin = new System.Windows.Forms.Padding(0);
            this.mt_plugins.Name = "mt_plugins";
            this.mt_plugins.Size = new System.Drawing.Size(705, 512);
            this.mt_plugins.TabIndex = 5;
            this.mt_plugins.Text = "Plugins";
            this.mt_plugins.UseVisualStyleBackColor = true;
            // 
            // PluginsMainSplit
            // 
            this.PluginsMainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginsMainSplit.Location = new System.Drawing.Point(0, 0);
            this.PluginsMainSplit.Margin = new System.Windows.Forms.Padding(0);
            this.PluginsMainSplit.Name = "PluginsMainSplit";
            // 
            // PluginsMainSplit.Panel1
            // 
            this.PluginsMainSplit.Panel1.Controls.Add(this.PluginTree);
            // 
            // PluginsMainSplit.Panel2
            // 
            this.PluginsMainSplit.Panel2.Controls.Add(this.PluginEditorSplit);
            this.PluginsMainSplit.Size = new System.Drawing.Size(705, 512);
            this.PluginsMainSplit.SplitterDistance = 139;
            this.PluginsMainSplit.SplitterWidth = 2;
            this.PluginsMainSplit.TabIndex = 1;
            // 
            // PluginTree
            // 
            this.PluginTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PluginTree.ContextMenuStrip = this.PluginTreeMenu;
            this.PluginTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginTree.Location = new System.Drawing.Point(0, 0);
            this.PluginTree.Margin = new System.Windows.Forms.Padding(0);
            this.PluginTree.Name = "PluginTree";
            this.PluginTree.ShowRootLines = false;
            this.PluginTree.Size = new System.Drawing.Size(139, 512);
            this.PluginTree.TabIndex = 0;
            this.PluginTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PluginTree_AfterSelect);
            // 
            // PluginTreeMenu
            // 
            this.PluginTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectedPluginDeactivateToolStripMenuItem,
            this.SelectedPluginReloadToolStripMenuItem,
            this.allPluginsToolStripMenuItem,
            this.passivePluginsToolStripMenuItem,
            this.activePluginsToolStripMenuItem,
            this.formatPluginsToolStripMenuItem,
            this.sessionPluginsToolStripMenuItem});
            this.PluginTreeMenu.Name = "ProxyLogMenu";
            this.PluginTreeMenu.Size = new System.Drawing.Size(214, 158);
            this.PluginTreeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.PluginTreeMenu_Opening);
            // 
            // SelectedPluginDeactivateToolStripMenuItem
            // 
            this.SelectedPluginDeactivateToolStripMenuItem.Name = "SelectedPluginDeactivateToolStripMenuItem";
            this.SelectedPluginDeactivateToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.SelectedPluginDeactivateToolStripMenuItem.Text = "Deactivate Selected Plugin";
            this.SelectedPluginDeactivateToolStripMenuItem.Click += new System.EventHandler(this.SelectedPluginDeactivateToolStripMenuItem_Click);
            // 
            // SelectedPluginReloadToolStripMenuItem
            // 
            this.SelectedPluginReloadToolStripMenuItem.Name = "SelectedPluginReloadToolStripMenuItem";
            this.SelectedPluginReloadToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.SelectedPluginReloadToolStripMenuItem.Text = "Reload Selected Plugin";
            this.SelectedPluginReloadToolStripMenuItem.Click += new System.EventHandler(this.SelectedPluginReloadToolStripMenuItem_Click);
            // 
            // allPluginsToolStripMenuItem
            // 
            this.allPluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AllPluginsRAToolStripMenuItem,
            this.AllPluginsANToolStripMenuItem});
            this.allPluginsToolStripMenuItem.Name = "allPluginsToolStripMenuItem";
            this.allPluginsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.allPluginsToolStripMenuItem.Text = "All Plugins";
            // 
            // AllPluginsRAToolStripMenuItem
            // 
            this.AllPluginsRAToolStripMenuItem.Name = "AllPluginsRAToolStripMenuItem";
            this.AllPluginsRAToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.AllPluginsRAToolStripMenuItem.Text = "Reload All";
            this.AllPluginsRAToolStripMenuItem.Click += new System.EventHandler(this.AllPluginsRAToolStripMenuItem_Click);
            // 
            // AllPluginsANToolStripMenuItem
            // 
            this.AllPluginsANToolStripMenuItem.Name = "AllPluginsANToolStripMenuItem";
            this.AllPluginsANToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.AllPluginsANToolStripMenuItem.Text = "Add New";
            this.AllPluginsANToolStripMenuItem.Click += new System.EventHandler(this.AllPluginsANToolStripMenuItem_Click);
            // 
            // passivePluginsToolStripMenuItem
            // 
            this.passivePluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PassivePluginsRAToolStripMenuItem,
            this.PassivePluginsANToolStripMenuItem});
            this.passivePluginsToolStripMenuItem.Name = "passivePluginsToolStripMenuItem";
            this.passivePluginsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.passivePluginsToolStripMenuItem.Text = "Passive Plugins";
            // 
            // PassivePluginsRAToolStripMenuItem
            // 
            this.PassivePluginsRAToolStripMenuItem.Name = "PassivePluginsRAToolStripMenuItem";
            this.PassivePluginsRAToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.PassivePluginsRAToolStripMenuItem.Text = "Reload All";
            this.PassivePluginsRAToolStripMenuItem.Click += new System.EventHandler(this.PassivePluginsRAToolStripMenuItem_Click);
            // 
            // PassivePluginsANToolStripMenuItem
            // 
            this.PassivePluginsANToolStripMenuItem.Name = "PassivePluginsANToolStripMenuItem";
            this.PassivePluginsANToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.PassivePluginsANToolStripMenuItem.Text = "Add New";
            this.PassivePluginsANToolStripMenuItem.Click += new System.EventHandler(this.PassivePluginsANToolStripMenuItem_Click);
            // 
            // activePluginsToolStripMenuItem
            // 
            this.activePluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActivePluginsRAToolStripMenuItem,
            this.ActivePluginsANToolStripMenuItem});
            this.activePluginsToolStripMenuItem.Name = "activePluginsToolStripMenuItem";
            this.activePluginsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.activePluginsToolStripMenuItem.Text = "Active Plugins";
            // 
            // ActivePluginsRAToolStripMenuItem
            // 
            this.ActivePluginsRAToolStripMenuItem.Name = "ActivePluginsRAToolStripMenuItem";
            this.ActivePluginsRAToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.ActivePluginsRAToolStripMenuItem.Text = "Reload All";
            this.ActivePluginsRAToolStripMenuItem.Click += new System.EventHandler(this.ActivePluginsRAToolStripMenuItem_Click);
            // 
            // ActivePluginsANToolStripMenuItem
            // 
            this.ActivePluginsANToolStripMenuItem.Name = "ActivePluginsANToolStripMenuItem";
            this.ActivePluginsANToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.ActivePluginsANToolStripMenuItem.Text = "Add New";
            this.ActivePluginsANToolStripMenuItem.Click += new System.EventHandler(this.ActivePluginsANToolStripMenuItem_Click);
            // 
            // formatPluginsToolStripMenuItem
            // 
            this.formatPluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FormatPluginsRAToolStripMenuItem,
            this.FormatPluginsANToolStripMenuItem});
            this.formatPluginsToolStripMenuItem.Name = "formatPluginsToolStripMenuItem";
            this.formatPluginsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.formatPluginsToolStripMenuItem.Text = "Format Plugins";
            // 
            // FormatPluginsRAToolStripMenuItem
            // 
            this.FormatPluginsRAToolStripMenuItem.Name = "FormatPluginsRAToolStripMenuItem";
            this.FormatPluginsRAToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.FormatPluginsRAToolStripMenuItem.Text = "Reload All";
            this.FormatPluginsRAToolStripMenuItem.Click += new System.EventHandler(this.FormatPluginsRAToolStripMenuItem_Click);
            // 
            // FormatPluginsANToolStripMenuItem
            // 
            this.FormatPluginsANToolStripMenuItem.Name = "FormatPluginsANToolStripMenuItem";
            this.FormatPluginsANToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.FormatPluginsANToolStripMenuItem.Text = "Add New";
            this.FormatPluginsANToolStripMenuItem.Click += new System.EventHandler(this.FormatPluginsANToolStripMenuItem_Click);
            // 
            // sessionPluginsToolStripMenuItem
            // 
            this.sessionPluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionPluginsRAToolStripMenuItem,
            this.SessionPluginsANToolStripMenuItem});
            this.sessionPluginsToolStripMenuItem.Name = "sessionPluginsToolStripMenuItem";
            this.sessionPluginsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.sessionPluginsToolStripMenuItem.Text = "Session Plugins";
            // 
            // SessionPluginsRAToolStripMenuItem
            // 
            this.SessionPluginsRAToolStripMenuItem.Name = "SessionPluginsRAToolStripMenuItem";
            this.SessionPluginsRAToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.SessionPluginsRAToolStripMenuItem.Text = "Reload All";
            this.SessionPluginsRAToolStripMenuItem.Click += new System.EventHandler(this.SessionPluginsRAToolStripMenuItem_Click);
            // 
            // SessionPluginsANToolStripMenuItem
            // 
            this.SessionPluginsANToolStripMenuItem.Name = "SessionPluginsANToolStripMenuItem";
            this.SessionPluginsANToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.SessionPluginsANToolStripMenuItem.Text = "Add New";
            this.SessionPluginsANToolStripMenuItem.Click += new System.EventHandler(this.SessionPluginsANToolStripMenuItem_Click);
            // 
            // PluginEditorSplit
            // 
            this.PluginEditorSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginEditorSplit.Location = new System.Drawing.Point(0, 0);
            this.PluginEditorSplit.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorSplit.Name = "PluginEditorSplit";
            // 
            // PluginEditorSplit.Panel1
            // 
            this.PluginEditorSplit.Panel1.Controls.Add(this.PluginsCentreSplit);
            // 
            // PluginEditorSplit.Panel2
            // 
            this.PluginEditorSplit.Panel2.Controls.Add(this.PluginEditorAPISplit);
            this.PluginEditorSplit.Size = new System.Drawing.Size(564, 512);
            this.PluginEditorSplit.SplitterDistance = 394;
            this.PluginEditorSplit.SplitterWidth = 2;
            this.PluginEditorSplit.TabIndex = 1;
            // 
            // PluginsCentreSplit
            // 
            this.PluginsCentreSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginsCentreSplit.Location = new System.Drawing.Point(0, 0);
            this.PluginsCentreSplit.Margin = new System.Windows.Forms.Padding(0);
            this.PluginsCentreSplit.Name = "PluginsCentreSplit";
            this.PluginsCentreSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PluginsCentreSplit.Panel1
            // 
            this.PluginsCentreSplit.Panel1.Controls.Add(this.PluginDetailsRTB);
            // 
            // PluginsCentreSplit.Panel2
            // 
            this.PluginsCentreSplit.Panel2.Controls.Add(this.PluginEditorInTE);
            this.PluginsCentreSplit.Size = new System.Drawing.Size(394, 512);
            this.PluginsCentreSplit.SplitterDistance = 203;
            this.PluginsCentreSplit.SplitterWidth = 2;
            this.PluginsCentreSplit.TabIndex = 4;
            // 
            // PluginDetailsRTB
            // 
            this.PluginDetailsRTB.BackColor = System.Drawing.Color.AliceBlue;
            this.PluginDetailsRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PluginDetailsRTB.DetectUrls = false;
            this.PluginDetailsRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginDetailsRTB.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PluginDetailsRTB.Location = new System.Drawing.Point(0, 0);
            this.PluginDetailsRTB.Name = "PluginDetailsRTB";
            this.PluginDetailsRTB.ReadOnly = true;
            this.PluginDetailsRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.PluginDetailsRTB.Size = new System.Drawing.Size(394, 203);
            this.PluginDetailsRTB.TabIndex = 1;
            this.PluginDetailsRTB.Text = "";
            // 
            // PluginEditorInTE
            // 
            this.PluginEditorInTE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginEditorInTE.IsIconBarVisible = false;
            this.PluginEditorInTE.Location = new System.Drawing.Point(0, 0);
            this.PluginEditorInTE.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorInTE.Name = "PluginEditorInTE";
            this.PluginEditorInTE.ShowEOLMarkers = true;
            this.PluginEditorInTE.ShowSpaces = true;
            this.PluginEditorInTE.ShowTabs = true;
            this.PluginEditorInTE.ShowVRuler = true;
            this.PluginEditorInTE.Size = new System.Drawing.Size(394, 307);
            this.PluginEditorInTE.TabIndex = 5;
            // 
            // PluginEditorAPISplit
            // 
            this.PluginEditorAPISplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginEditorAPISplit.Location = new System.Drawing.Point(0, 0);
            this.PluginEditorAPISplit.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorAPISplit.Name = "PluginEditorAPISplit";
            this.PluginEditorAPISplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PluginEditorAPISplit.Panel1
            // 
            this.PluginEditorAPISplit.Panel1.Controls.Add(this.PluginEditorAPITreeTabs);
            // 
            // PluginEditorAPISplit.Panel2
            // 
            this.PluginEditorAPISplit.Panel2.Controls.Add(this.PluginEditorAPIDetailsRTB);
            this.PluginEditorAPISplit.Size = new System.Drawing.Size(168, 512);
            this.PluginEditorAPISplit.SplitterDistance = 280;
            this.PluginEditorAPISplit.SplitterWidth = 2;
            this.PluginEditorAPISplit.TabIndex = 1;
            // 
            // PluginEditorAPITreeTabs
            // 
            this.PluginEditorAPITreeTabs.Controls.Add(this.PluginEditorPythonAPITreeTab);
            this.PluginEditorAPITreeTabs.Controls.Add(this.PluginEditorRubyAPITreeTab);
            this.PluginEditorAPITreeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginEditorAPITreeTabs.Location = new System.Drawing.Point(0, 0);
            this.PluginEditorAPITreeTabs.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorAPITreeTabs.Name = "PluginEditorAPITreeTabs";
            this.PluginEditorAPITreeTabs.Padding = new System.Drawing.Point(0, 0);
            this.PluginEditorAPITreeTabs.SelectedIndex = 0;
            this.PluginEditorAPITreeTabs.Size = new System.Drawing.Size(168, 280);
            this.PluginEditorAPITreeTabs.TabIndex = 0;
            // 
            // PluginEditorPythonAPITreeTab
            // 
            this.PluginEditorPythonAPITreeTab.Controls.Add(this.PluginEditorPythonAPITree);
            this.PluginEditorPythonAPITreeTab.Location = new System.Drawing.Point(4, 22);
            this.PluginEditorPythonAPITreeTab.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorPythonAPITreeTab.Name = "PluginEditorPythonAPITreeTab";
            this.PluginEditorPythonAPITreeTab.Size = new System.Drawing.Size(160, 254);
            this.PluginEditorPythonAPITreeTab.TabIndex = 0;
            this.PluginEditorPythonAPITreeTab.Text = "Python";
            this.PluginEditorPythonAPITreeTab.UseVisualStyleBackColor = true;
            // 
            // PluginEditorPythonAPITree
            // 
            this.PluginEditorPythonAPITree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PluginEditorPythonAPITree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginEditorPythonAPITree.Location = new System.Drawing.Point(0, 0);
            this.PluginEditorPythonAPITree.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorPythonAPITree.Name = "PluginEditorPythonAPITree";
            this.PluginEditorPythonAPITree.Size = new System.Drawing.Size(160, 254);
            this.PluginEditorPythonAPITree.TabIndex = 0;
            this.PluginEditorPythonAPITree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PluginEditorPythonAPITree_AfterSelect);
            // 
            // PluginEditorRubyAPITreeTab
            // 
            this.PluginEditorRubyAPITreeTab.Controls.Add(this.PluginEditorRubyAPITree);
            this.PluginEditorRubyAPITreeTab.Location = new System.Drawing.Point(4, 22);
            this.PluginEditorRubyAPITreeTab.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorRubyAPITreeTab.Name = "PluginEditorRubyAPITreeTab";
            this.PluginEditorRubyAPITreeTab.Size = new System.Drawing.Size(160, 254);
            this.PluginEditorRubyAPITreeTab.TabIndex = 1;
            this.PluginEditorRubyAPITreeTab.Text = "Ruby";
            this.PluginEditorRubyAPITreeTab.UseVisualStyleBackColor = true;
            // 
            // PluginEditorRubyAPITree
            // 
            this.PluginEditorRubyAPITree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PluginEditorRubyAPITree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginEditorRubyAPITree.Location = new System.Drawing.Point(0, 0);
            this.PluginEditorRubyAPITree.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorRubyAPITree.Name = "PluginEditorRubyAPITree";
            this.PluginEditorRubyAPITree.Size = new System.Drawing.Size(160, 254);
            this.PluginEditorRubyAPITree.TabIndex = 1;
            this.PluginEditorRubyAPITree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PluginEditorRubyAPITree_AfterSelect);
            // 
            // PluginEditorAPIDetailsRTB
            // 
            this.PluginEditorAPIDetailsRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginEditorAPIDetailsRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PluginEditorAPIDetailsRTB.Location = new System.Drawing.Point(0, 0);
            this.PluginEditorAPIDetailsRTB.Margin = new System.Windows.Forms.Padding(0);
            this.PluginEditorAPIDetailsRTB.Name = "PluginEditorAPIDetailsRTB";
            this.PluginEditorAPIDetailsRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.PluginEditorAPIDetailsRTB.Size = new System.Drawing.Size(168, 230);
            this.PluginEditorAPIDetailsRTB.TabIndex = 0;
            this.PluginEditorAPIDetailsRTB.Text = "";
            // 
            // mt_trace
            // 
            this.mt_trace.Controls.Add(this.TraceBaseSplit);
            this.mt_trace.Location = new System.Drawing.Point(4, 22);
            this.mt_trace.Name = "mt_trace";
            this.mt_trace.Size = new System.Drawing.Size(705, 512);
            this.mt_trace.TabIndex = 7;
            this.mt_trace.Text = "Trace";
            this.mt_trace.UseVisualStyleBackColor = true;
            // 
            // TraceBaseSplit
            // 
            this.TraceBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.TraceBaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.TraceBaseSplit.Name = "TraceBaseSplit";
            this.TraceBaseSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // TraceBaseSplit.Panel1
            // 
            this.TraceBaseSplit.Panel1.Controls.Add(this.TraceGrid);
            // 
            // TraceBaseSplit.Panel2
            // 
            this.TraceBaseSplit.Panel2.Controls.Add(this.TraceMsgRTB);
            this.TraceBaseSplit.Size = new System.Drawing.Size(705, 512);
            this.TraceBaseSplit.SplitterDistance = 364;
            this.TraceBaseSplit.TabIndex = 0;
            // 
            // TraceGrid
            // 
            this.TraceGrid.AllowUserToAddRows = false;
            this.TraceGrid.AllowUserToDeleteRows = false;
            this.TraceGrid.AllowUserToOrderColumns = true;
            this.TraceGrid.AllowUserToResizeRows = false;
            this.TraceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TraceGrid.BackgroundColor = System.Drawing.Color.White;
            this.TraceGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TraceGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.TraceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TraceGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn28,
            this.Column1,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn31});
            this.TraceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TraceGrid.GridColor = System.Drawing.Color.White;
            this.TraceGrid.Location = new System.Drawing.Point(0, 0);
            this.TraceGrid.Margin = new System.Windows.Forms.Padding(0);
            this.TraceGrid.MultiSelect = false;
            this.TraceGrid.Name = "TraceGrid";
            this.TraceGrid.ReadOnly = true;
            this.TraceGrid.RowHeadersVisible = false;
            this.TraceGrid.RowHeadersWidth = 10;
            this.TraceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TraceGrid.Size = new System.Drawing.Size(705, 364);
            this.TraceGrid.TabIndex = 8;
            this.TraceGrid.SelectionChanged += new System.EventHandler(this.TraceGrid_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn26.HeaderText = "ID";
            this.dataGridViewTextBoxColumn26.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.Width = 50;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn28.HeaderText = "TIME";
            this.dataGridViewTextBoxColumn28.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.Width = 58;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "DATE";
            this.Column1.MinimumWidth = 20;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn29.HeaderText = "THREAD ID";
            this.dataGridViewTextBoxColumn29.MinimumWidth = 30;
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            this.dataGridViewTextBoxColumn29.Width = 91;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn30.HeaderText = "SOURCE";
            this.dataGridViewTextBoxColumn30.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.Width = 77;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn31.HeaderText = "MESSAGE";
            this.dataGridViewTextBoxColumn31.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            // 
            // TraceMsgRTB
            // 
            this.TraceMsgRTB.BackColor = System.Drawing.Color.White;
            this.TraceMsgRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TraceMsgRTB.DetectUrls = false;
            this.TraceMsgRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceMsgRTB.Location = new System.Drawing.Point(0, 0);
            this.TraceMsgRTB.Name = "TraceMsgRTB";
            this.TraceMsgRTB.ReadOnly = true;
            this.TraceMsgRTB.Size = new System.Drawing.Size(705, 144);
            this.TraceMsgRTB.TabIndex = 0;
            this.TraceMsgRTB.Text = "";
            // 
            // ConfigPanel
            // 
            this.ConfigPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigPanel.AutoScroll = true;
            this.ConfigPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigPanel.Controls.Add(this.ConfigPanelTabs);
            this.ConfigPanel.Location = new System.Drawing.Point(1, 25);
            this.ConfigPanel.Name = "ConfigPanel";
            this.ConfigPanel.Size = new System.Drawing.Size(883, 10);
            this.ConfigPanel.TabIndex = 1;
            this.ConfigPanel.Visible = false;
            // 
            // ConfigPanelTabs
            // 
            this.ConfigPanelTabs.Controls.Add(this.ConfigIronProxyTab);
            this.ConfigPanelTabs.Controls.Add(this.ConfigInterceptRulesTab);
            this.ConfigPanelTabs.Controls.Add(this.ConfigDisplayRulesTab);
            this.ConfigPanelTabs.Controls.Add(this.ConfigScriptingTab);
            this.ConfigPanelTabs.Controls.Add(this.ConfigHTTPAPITab);
            this.ConfigPanelTabs.Controls.Add(this.ConfigTaintConfigTab);
            this.ConfigPanelTabs.Controls.Add(this.ConfigScannerTab);
            this.ConfigPanelTabs.Controls.Add(this.ConfigPassiveAnalysisTab);
            this.ConfigPanelTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigPanelTabs.Location = new System.Drawing.Point(0, 0);
            this.ConfigPanelTabs.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigPanelTabs.Name = "ConfigPanelTabs";
            this.ConfigPanelTabs.Padding = new System.Drawing.Point(0, 0);
            this.ConfigPanelTabs.SelectedIndex = 0;
            this.ConfigPanelTabs.Size = new System.Drawing.Size(881, 8);
            this.ConfigPanelTabs.TabIndex = 0;
            this.ConfigPanelTabs.SelectedIndexChanged += new System.EventHandler(this.ConfigPanelTabs_SelectedIndexChanged);
            // 
            // ConfigIronProxyTab
            // 
            this.ConfigIronProxyTab.AutoScroll = true;
            this.ConfigIronProxyTab.Controls.Add(this.label11);
            this.ConfigIronProxyTab.Controls.Add(this.label8);
            this.ConfigIronProxyTab.Controls.Add(this.ConfigUpstreamProxyPortTB);
            this.ConfigIronProxyTab.Controls.Add(this.ConfigUseUpstreamProxyCB);
            this.ConfigIronProxyTab.Controls.Add(this.ConfigUpstreamProxyIPTB);
            this.ConfigIronProxyTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigIronProxyTab.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigIronProxyTab.Name = "ConfigIronProxyTab";
            this.ConfigIronProxyTab.Size = new System.Drawing.Size(873, 0);
            this.ConfigIronProxyTab.TabIndex = 0;
            this.ConfigIronProxyTab.Text = "Proxy Settings";
            this.ConfigIronProxyTab.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(196, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 60;
            this.label11.Text = "Proxy Port:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "Proxy IP:";
            // 
            // ConfigUpstreamProxyPortTB
            // 
            this.ConfigUpstreamProxyPortTB.Location = new System.Drawing.Point(264, 37);
            this.ConfigUpstreamProxyPortTB.Name = "ConfigUpstreamProxyPortTB";
            this.ConfigUpstreamProxyPortTB.Size = new System.Drawing.Size(78, 20);
            this.ConfigUpstreamProxyPortTB.TabIndex = 61;
            this.ConfigUpstreamProxyPortTB.TextChanged += new System.EventHandler(this.ConfigUpstreamProxyPortTB_TextChanged);
            // 
            // ConfigUseUpstreamProxyCB
            // 
            this.ConfigUseUpstreamProxyCB.AutoSize = true;
            this.ConfigUseUpstreamProxyCB.Enabled = false;
            this.ConfigUseUpstreamProxyCB.Location = new System.Drawing.Point(6, 11);
            this.ConfigUseUpstreamProxyCB.Name = "ConfigUseUpstreamProxyCB";
            this.ConfigUseUpstreamProxyCB.Size = new System.Drawing.Size(122, 17);
            this.ConfigUseUpstreamProxyCB.TabIndex = 62;
            this.ConfigUseUpstreamProxyCB.Text = "Use Upstream Proxy";
            this.ConfigUseUpstreamProxyCB.UseVisualStyleBackColor = true;
            this.ConfigUseUpstreamProxyCB.Click += new System.EventHandler(this.ConfigUseUpstreamProxyCB_Click);
            // 
            // ConfigUpstreamProxyIPTB
            // 
            this.ConfigUpstreamProxyIPTB.Location = new System.Drawing.Point(62, 37);
            this.ConfigUpstreamProxyIPTB.Name = "ConfigUpstreamProxyIPTB";
            this.ConfigUpstreamProxyIPTB.Size = new System.Drawing.Size(121, 20);
            this.ConfigUpstreamProxyIPTB.TabIndex = 59;
            this.ConfigUpstreamProxyIPTB.TextChanged += new System.EventHandler(this.ConfigUpstreamProxyIPTB_TextChanged);
            // 
            // ConfigInterceptRulesTab
            // 
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleRequestOnResponseRulesCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.label25);
            this.ConfigInterceptRulesTab.Controls.Add(this.label24);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleKeywordInResponseGB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentJSONCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleKeywordInRequestGB);
            this.ConfigInterceptRulesTab.Controls.Add(this.groupBox2);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleApplyChangesLL);
            this.ConfigInterceptRulesTab.Controls.Add(this.groupBox1);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCancelChangesLL);
            this.ConfigInterceptRulesTab.Controls.Add(this.label10);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentCSSCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode5xxCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentJSCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode500CB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentImgCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode4xxCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentOtherBinaryCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode403CB);
            this.ConfigInterceptRulesTab.Controls.Add(this.label9);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode3xxCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentHTMLCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode301_2CB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleGETMethodCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode2xxCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRulePOSTMethodCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleCode200CB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleOtherMethodsCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.label13);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentOtherTextCB);
            this.ConfigInterceptRulesTab.Controls.Add(this.ConfigRuleContentXMLCB);
            this.ConfigInterceptRulesTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigInterceptRulesTab.Name = "ConfigInterceptRulesTab";
            this.ConfigInterceptRulesTab.Size = new System.Drawing.Size(873, 0);
            this.ConfigInterceptRulesTab.TabIndex = 4;
            this.ConfigInterceptRulesTab.Text = "Proxy Traffic Interception Rules";
            this.ConfigInterceptRulesTab.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleRequestOnResponseRulesCB
            // 
            this.ConfigRuleRequestOnResponseRulesCB.AutoSize = true;
            this.ConfigRuleRequestOnResponseRulesCB.Checked = true;
            this.ConfigRuleRequestOnResponseRulesCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleRequestOnResponseRulesCB.Location = new System.Drawing.Point(682, 205);
            this.ConfigRuleRequestOnResponseRulesCB.Name = "ConfigRuleRequestOnResponseRulesCB";
            this.ConfigRuleRequestOnResponseRulesCB.Size = new System.Drawing.Size(186, 17);
            this.ConfigRuleRequestOnResponseRulesCB.TabIndex = 70;
            this.ConfigRuleRequestOnResponseRulesCB.Text = "Response Rules + Request Rules";
            this.ConfigRuleRequestOnResponseRulesCB.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(5, 204);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(88, 13);
            this.label25.TabIndex = 69;
            this.label25.Text = "Response Rules:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(5, 5);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 13);
            this.label24.TabIndex = 68;
            this.label24.Text = "Request Rules:";
            // 
            // ConfigRuleKeywordInResponseGB
            // 
            this.ConfigRuleKeywordInResponseGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleKeywordInResponseGB.Controls.Add(this.ConfigRuleKeywordInResponseCB);
            this.ConfigRuleKeywordInResponseGB.Controls.Add(this.ConfigRuleKeywordInResponsePlusTB);
            this.ConfigRuleKeywordInResponseGB.Controls.Add(this.ConfigRuleKeywordInResponseMinusTB);
            this.ConfigRuleKeywordInResponseGB.Controls.Add(this.ConfigRuleKeywordInResponsePlusRB);
            this.ConfigRuleKeywordInResponseGB.Controls.Add(this.ConfigRuleKeywordInResponseMinusRB);
            this.ConfigRuleKeywordInResponseGB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConfigRuleKeywordInResponseGB.Location = new System.Drawing.Point(2, 260);
            this.ConfigRuleKeywordInResponseGB.Name = "ConfigRuleKeywordInResponseGB";
            this.ConfigRuleKeywordInResponseGB.Size = new System.Drawing.Size(868, 52);
            this.ConfigRuleKeywordInResponseGB.TabIndex = 66;
            this.ConfigRuleKeywordInResponseGB.TabStop = false;
            // 
            // ConfigRuleKeywordInResponseCB
            // 
            this.ConfigRuleKeywordInResponseCB.AutoSize = true;
            this.ConfigRuleKeywordInResponseCB.Location = new System.Drawing.Point(8, 20);
            this.ConfigRuleKeywordInResponseCB.Name = "ConfigRuleKeywordInResponseCB";
            this.ConfigRuleKeywordInResponseCB.Size = new System.Drawing.Size(132, 17);
            this.ConfigRuleKeywordInResponseCB.TabIndex = 59;
            this.ConfigRuleKeywordInResponseCB.Text = "Keyword in Response:";
            this.ConfigRuleKeywordInResponseCB.UseVisualStyleBackColor = true;
            this.ConfigRuleKeywordInResponseCB.CheckedChanged += new System.EventHandler(this.ConfigRuleKeywordInResponseCB_CheckedChanged);
            // 
            // ConfigRuleKeywordInResponsePlusTB
            // 
            this.ConfigRuleKeywordInResponsePlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleKeywordInResponsePlusTB.Enabled = false;
            this.ConfigRuleKeywordInResponsePlusTB.Location = new System.Drawing.Point(180, 8);
            this.ConfigRuleKeywordInResponsePlusTB.Name = "ConfigRuleKeywordInResponsePlusTB";
            this.ConfigRuleKeywordInResponsePlusTB.Size = new System.Drawing.Size(685, 20);
            this.ConfigRuleKeywordInResponsePlusTB.TabIndex = 51;
            // 
            // ConfigRuleKeywordInResponseMinusTB
            // 
            this.ConfigRuleKeywordInResponseMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleKeywordInResponseMinusTB.Enabled = false;
            this.ConfigRuleKeywordInResponseMinusTB.Location = new System.Drawing.Point(180, 29);
            this.ConfigRuleKeywordInResponseMinusTB.Name = "ConfigRuleKeywordInResponseMinusTB";
            this.ConfigRuleKeywordInResponseMinusTB.Size = new System.Drawing.Size(685, 20);
            this.ConfigRuleKeywordInResponseMinusTB.TabIndex = 52;
            // 
            // ConfigRuleKeywordInResponsePlusRB
            // 
            this.ConfigRuleKeywordInResponsePlusRB.AutoSize = true;
            this.ConfigRuleKeywordInResponsePlusRB.Enabled = false;
            this.ConfigRuleKeywordInResponsePlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleKeywordInResponsePlusRB.Location = new System.Drawing.Point(142, 9);
            this.ConfigRuleKeywordInResponsePlusRB.Name = "ConfigRuleKeywordInResponsePlusRB";
            this.ConfigRuleKeywordInResponsePlusRB.Size = new System.Drawing.Size(34, 20);
            this.ConfigRuleKeywordInResponsePlusRB.TabIndex = 57;
            this.ConfigRuleKeywordInResponsePlusRB.TabStop = true;
            this.ConfigRuleKeywordInResponsePlusRB.Text = "+";
            this.ConfigRuleKeywordInResponsePlusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleKeywordInResponseMinusRB
            // 
            this.ConfigRuleKeywordInResponseMinusRB.AutoSize = true;
            this.ConfigRuleKeywordInResponseMinusRB.Enabled = false;
            this.ConfigRuleKeywordInResponseMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleKeywordInResponseMinusRB.Location = new System.Drawing.Point(142, 28);
            this.ConfigRuleKeywordInResponseMinusRB.Name = "ConfigRuleKeywordInResponseMinusRB";
            this.ConfigRuleKeywordInResponseMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ConfigRuleKeywordInResponseMinusRB.TabIndex = 58;
            this.ConfigRuleKeywordInResponseMinusRB.TabStop = true;
            this.ConfigRuleKeywordInResponseMinusRB.Text = "-";
            this.ConfigRuleKeywordInResponseMinusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleContentJSONCB
            // 
            this.ConfigRuleContentJSONCB.AutoSize = true;
            this.ConfigRuleContentJSONCB.Checked = true;
            this.ConfigRuleContentJSONCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleContentJSONCB.Location = new System.Drawing.Point(331, 244);
            this.ConfigRuleContentJSONCB.Name = "ConfigRuleContentJSONCB";
            this.ConfigRuleContentJSONCB.Size = new System.Drawing.Size(54, 17);
            this.ConfigRuleContentJSONCB.TabIndex = 67;
            this.ConfigRuleContentJSONCB.Text = "JSON";
            this.ConfigRuleContentJSONCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleKeywordInRequestGB
            // 
            this.ConfigRuleKeywordInRequestGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleKeywordInRequestGB.Controls.Add(this.ConfigRuleKeywordInRequestCB);
            this.ConfigRuleKeywordInRequestGB.Controls.Add(this.ConfigRuleKeywordInRequestPlusTB);
            this.ConfigRuleKeywordInRequestGB.Controls.Add(this.ConfigRuleKeywordInRequestMinusTB);
            this.ConfigRuleKeywordInRequestGB.Controls.Add(this.ConfigRuleKeywordInRequestPlusRB);
            this.ConfigRuleKeywordInRequestGB.Controls.Add(this.ConfigRuleKeywordInRequestMinusRB);
            this.ConfigRuleKeywordInRequestGB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConfigRuleKeywordInRequestGB.Location = new System.Drawing.Point(2, 145);
            this.ConfigRuleKeywordInRequestGB.Name = "ConfigRuleKeywordInRequestGB";
            this.ConfigRuleKeywordInRequestGB.Size = new System.Drawing.Size(868, 52);
            this.ConfigRuleKeywordInRequestGB.TabIndex = 65;
            this.ConfigRuleKeywordInRequestGB.TabStop = false;
            // 
            // ConfigRuleKeywordInRequestCB
            // 
            this.ConfigRuleKeywordInRequestCB.AutoSize = true;
            this.ConfigRuleKeywordInRequestCB.Location = new System.Drawing.Point(8, 20);
            this.ConfigRuleKeywordInRequestCB.Name = "ConfigRuleKeywordInRequestCB";
            this.ConfigRuleKeywordInRequestCB.Size = new System.Drawing.Size(124, 17);
            this.ConfigRuleKeywordInRequestCB.TabIndex = 59;
            this.ConfigRuleKeywordInRequestCB.Text = "Keyword in Request:";
            this.ConfigRuleKeywordInRequestCB.UseVisualStyleBackColor = true;
            this.ConfigRuleKeywordInRequestCB.CheckedChanged += new System.EventHandler(this.ConfigRuleKeywordInRequestCB_CheckedChanged);
            // 
            // ConfigRuleKeywordInRequestPlusTB
            // 
            this.ConfigRuleKeywordInRequestPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleKeywordInRequestPlusTB.Enabled = false;
            this.ConfigRuleKeywordInRequestPlusTB.Location = new System.Drawing.Point(180, 8);
            this.ConfigRuleKeywordInRequestPlusTB.Name = "ConfigRuleKeywordInRequestPlusTB";
            this.ConfigRuleKeywordInRequestPlusTB.Size = new System.Drawing.Size(685, 20);
            this.ConfigRuleKeywordInRequestPlusTB.TabIndex = 51;
            // 
            // ConfigRuleKeywordInRequestMinusTB
            // 
            this.ConfigRuleKeywordInRequestMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleKeywordInRequestMinusTB.Enabled = false;
            this.ConfigRuleKeywordInRequestMinusTB.Location = new System.Drawing.Point(180, 29);
            this.ConfigRuleKeywordInRequestMinusTB.Name = "ConfigRuleKeywordInRequestMinusTB";
            this.ConfigRuleKeywordInRequestMinusTB.Size = new System.Drawing.Size(685, 20);
            this.ConfigRuleKeywordInRequestMinusTB.TabIndex = 52;
            // 
            // ConfigRuleKeywordInRequestPlusRB
            // 
            this.ConfigRuleKeywordInRequestPlusRB.AutoSize = true;
            this.ConfigRuleKeywordInRequestPlusRB.Enabled = false;
            this.ConfigRuleKeywordInRequestPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleKeywordInRequestPlusRB.Location = new System.Drawing.Point(142, 9);
            this.ConfigRuleKeywordInRequestPlusRB.Name = "ConfigRuleKeywordInRequestPlusRB";
            this.ConfigRuleKeywordInRequestPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ConfigRuleKeywordInRequestPlusRB.TabIndex = 57;
            this.ConfigRuleKeywordInRequestPlusRB.TabStop = true;
            this.ConfigRuleKeywordInRequestPlusRB.Text = "+";
            this.ConfigRuleKeywordInRequestPlusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleKeywordInRequestMinusRB
            // 
            this.ConfigRuleKeywordInRequestMinusRB.AutoSize = true;
            this.ConfigRuleKeywordInRequestMinusRB.Enabled = false;
            this.ConfigRuleKeywordInRequestMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleKeywordInRequestMinusRB.Location = new System.Drawing.Point(142, 28);
            this.ConfigRuleKeywordInRequestMinusRB.Name = "ConfigRuleKeywordInRequestMinusRB";
            this.ConfigRuleKeywordInRequestMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ConfigRuleKeywordInRequestMinusRB.TabIndex = 58;
            this.ConfigRuleKeywordInRequestMinusRB.TabStop = true;
            this.ConfigRuleKeywordInRequestMinusRB.Text = "-";
            this.ConfigRuleKeywordInRequestMinusRB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ConfigRuleHostNamesCB);
            this.groupBox2.Controls.Add(this.ConfigRuleHostNamesPlusTB);
            this.groupBox2.Controls.Add(this.ConfigRuleHostNamesMinusTB);
            this.groupBox2.Controls.Add(this.ConfigRuleHostNamesPlusRB);
            this.groupBox2.Controls.Add(this.ConfigRuleHostNamesMinusRB);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(2, 91);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(868, 52);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            // 
            // ConfigRuleHostNamesCB
            // 
            this.ConfigRuleHostNamesCB.AutoSize = true;
            this.ConfigRuleHostNamesCB.Location = new System.Drawing.Point(8, 20);
            this.ConfigRuleHostNamesCB.Name = "ConfigRuleHostNamesCB";
            this.ConfigRuleHostNamesCB.Size = new System.Drawing.Size(84, 17);
            this.ConfigRuleHostNamesCB.TabIndex = 59;
            this.ConfigRuleHostNamesCB.Text = "HostNames:";
            this.ConfigRuleHostNamesCB.UseVisualStyleBackColor = true;
            this.ConfigRuleHostNamesCB.CheckedChanged += new System.EventHandler(this.ConfigRuleHostNamesCB_CheckedChanged);
            // 
            // ConfigRuleHostNamesPlusTB
            // 
            this.ConfigRuleHostNamesPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleHostNamesPlusTB.Enabled = false;
            this.ConfigRuleHostNamesPlusTB.Location = new System.Drawing.Point(160, 8);
            this.ConfigRuleHostNamesPlusTB.Name = "ConfigRuleHostNamesPlusTB";
            this.ConfigRuleHostNamesPlusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigRuleHostNamesPlusTB.TabIndex = 51;
            // 
            // ConfigRuleHostNamesMinusTB
            // 
            this.ConfigRuleHostNamesMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleHostNamesMinusTB.Enabled = false;
            this.ConfigRuleHostNamesMinusTB.Location = new System.Drawing.Point(160, 29);
            this.ConfigRuleHostNamesMinusTB.Name = "ConfigRuleHostNamesMinusTB";
            this.ConfigRuleHostNamesMinusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigRuleHostNamesMinusTB.TabIndex = 52;
            // 
            // ConfigRuleHostNamesPlusRB
            // 
            this.ConfigRuleHostNamesPlusRB.AutoSize = true;
            this.ConfigRuleHostNamesPlusRB.Enabled = false;
            this.ConfigRuleHostNamesPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleHostNamesPlusRB.Location = new System.Drawing.Point(118, 9);
            this.ConfigRuleHostNamesPlusRB.Name = "ConfigRuleHostNamesPlusRB";
            this.ConfigRuleHostNamesPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ConfigRuleHostNamesPlusRB.TabIndex = 57;
            this.ConfigRuleHostNamesPlusRB.TabStop = true;
            this.ConfigRuleHostNamesPlusRB.Text = "+";
            this.ConfigRuleHostNamesPlusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleHostNamesMinusRB
            // 
            this.ConfigRuleHostNamesMinusRB.AutoSize = true;
            this.ConfigRuleHostNamesMinusRB.Enabled = false;
            this.ConfigRuleHostNamesMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleHostNamesMinusRB.Location = new System.Drawing.Point(118, 28);
            this.ConfigRuleHostNamesMinusRB.Name = "ConfigRuleHostNamesMinusRB";
            this.ConfigRuleHostNamesMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ConfigRuleHostNamesMinusRB.TabIndex = 58;
            this.ConfigRuleHostNamesMinusRB.TabStop = true;
            this.ConfigRuleHostNamesMinusRB.Text = "-";
            this.ConfigRuleHostNamesMinusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleApplyChangesLL
            // 
            this.ConfigRuleApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigRuleApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleApplyChangesLL.AutoSize = true;
            this.ConfigRuleApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigRuleApplyChangesLL.Location = new System.Drawing.Point(670, 6);
            this.ConfigRuleApplyChangesLL.Name = "ConfigRuleApplyChangesLL";
            this.ConfigRuleApplyChangesLL.Size = new System.Drawing.Size(91, 13);
            this.ConfigRuleApplyChangesLL.TabIndex = 56;
            this.ConfigRuleApplyChangesLL.TabStop = true;
            this.ConfigRuleApplyChangesLL.Text = "Apply Changes";
            this.ConfigRuleApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigRuleApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigRuleApplyChangesLL_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ConfigRuleFileExtensionsCB);
            this.groupBox1.Controls.Add(this.ConfigRuleFileExtensionsPlusTB);
            this.groupBox1.Controls.Add(this.ConfigRuleFileExtensionsMinusTB);
            this.groupBox1.Controls.Add(this.ConfigRuleFileExtensionsPlusRB);
            this.groupBox1.Controls.Add(this.ConfigRuleFileExtensionsMinusRB);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(2, 39);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 52);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            // 
            // ConfigRuleFileExtensionsCB
            // 
            this.ConfigRuleFileExtensionsCB.AutoSize = true;
            this.ConfigRuleFileExtensionsCB.Checked = true;
            this.ConfigRuleFileExtensionsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleFileExtensionsCB.Location = new System.Drawing.Point(8, 20);
            this.ConfigRuleFileExtensionsCB.Name = "ConfigRuleFileExtensionsCB";
            this.ConfigRuleFileExtensionsCB.Size = new System.Drawing.Size(99, 17);
            this.ConfigRuleFileExtensionsCB.TabIndex = 59;
            this.ConfigRuleFileExtensionsCB.Text = "File Extensions:";
            this.ConfigRuleFileExtensionsCB.UseVisualStyleBackColor = true;
            this.ConfigRuleFileExtensionsCB.CheckedChanged += new System.EventHandler(this.ConfigRuleFileExtensionsCB_CheckedChanged);
            // 
            // ConfigRuleFileExtensionsPlusTB
            // 
            this.ConfigRuleFileExtensionsPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleFileExtensionsPlusTB.Location = new System.Drawing.Point(160, 8);
            this.ConfigRuleFileExtensionsPlusTB.Name = "ConfigRuleFileExtensionsPlusTB";
            this.ConfigRuleFileExtensionsPlusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigRuleFileExtensionsPlusTB.TabIndex = 51;
            // 
            // ConfigRuleFileExtensionsMinusTB
            // 
            this.ConfigRuleFileExtensionsMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleFileExtensionsMinusTB.Location = new System.Drawing.Point(160, 29);
            this.ConfigRuleFileExtensionsMinusTB.Name = "ConfigRuleFileExtensionsMinusTB";
            this.ConfigRuleFileExtensionsMinusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigRuleFileExtensionsMinusTB.TabIndex = 52;
            this.ConfigRuleFileExtensionsMinusTB.Text = "css,js,jpg,jpeg,png,gif,ico,swf,doc,docx,pdf,xls,xlsx,ppt,pptx";
            // 
            // ConfigRuleFileExtensionsPlusRB
            // 
            this.ConfigRuleFileExtensionsPlusRB.AutoSize = true;
            this.ConfigRuleFileExtensionsPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleFileExtensionsPlusRB.Location = new System.Drawing.Point(118, 9);
            this.ConfigRuleFileExtensionsPlusRB.Name = "ConfigRuleFileExtensionsPlusRB";
            this.ConfigRuleFileExtensionsPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ConfigRuleFileExtensionsPlusRB.TabIndex = 57;
            this.ConfigRuleFileExtensionsPlusRB.Text = "+";
            this.ConfigRuleFileExtensionsPlusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleFileExtensionsMinusRB
            // 
            this.ConfigRuleFileExtensionsMinusRB.AutoSize = true;
            this.ConfigRuleFileExtensionsMinusRB.Checked = true;
            this.ConfigRuleFileExtensionsMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleFileExtensionsMinusRB.Location = new System.Drawing.Point(118, 28);
            this.ConfigRuleFileExtensionsMinusRB.Name = "ConfigRuleFileExtensionsMinusRB";
            this.ConfigRuleFileExtensionsMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ConfigRuleFileExtensionsMinusRB.TabIndex = 58;
            this.ConfigRuleFileExtensionsMinusRB.TabStop = true;
            this.ConfigRuleFileExtensionsMinusRB.Text = "-";
            this.ConfigRuleFileExtensionsMinusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCancelChangesLL
            // 
            this.ConfigRuleCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRuleCancelChangesLL.AutoSize = true;
            this.ConfigRuleCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRuleCancelChangesLL.Location = new System.Drawing.Point(767, 5);
            this.ConfigRuleCancelChangesLL.Name = "ConfigRuleCancelChangesLL";
            this.ConfigRuleCancelChangesLL.Size = new System.Drawing.Size(99, 13);
            this.ConfigRuleCancelChangesLL.TabIndex = 55;
            this.ConfigRuleCancelChangesLL.TabStop = true;
            this.ConfigRuleCancelChangesLL.Text = "Cancel Changes";
            this.ConfigRuleCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigRuleCancelChangesLL_LinkClicked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Request Method:";
            // 
            // ConfigRuleContentCSSCB
            // 
            this.ConfigRuleContentCSSCB.AutoSize = true;
            this.ConfigRuleContentCSSCB.Location = new System.Drawing.Point(229, 244);
            this.ConfigRuleContentCSSCB.Name = "ConfigRuleContentCSSCB";
            this.ConfigRuleContentCSSCB.Size = new System.Drawing.Size(47, 17);
            this.ConfigRuleContentCSSCB.TabIndex = 0;
            this.ConfigRuleContentCSSCB.Text = "CSS";
            this.ConfigRuleContentCSSCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCode5xxCB
            // 
            this.ConfigRuleCode5xxCB.AutoSize = true;
            this.ConfigRuleCode5xxCB.Location = new System.Drawing.Point(481, 224);
            this.ConfigRuleCode5xxCB.Name = "ConfigRuleCode5xxCB";
            this.ConfigRuleCode5xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigRuleCode5xxCB.TabIndex = 27;
            this.ConfigRuleCode5xxCB.Text = "5xx";
            this.ConfigRuleCode5xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleContentJSCB
            // 
            this.ConfigRuleContentJSCB.AutoSize = true;
            this.ConfigRuleContentJSCB.Location = new System.Drawing.Point(185, 244);
            this.ConfigRuleContentJSCB.Name = "ConfigRuleContentJSCB";
            this.ConfigRuleContentJSCB.Size = new System.Drawing.Size(38, 17);
            this.ConfigRuleContentJSCB.TabIndex = 1;
            this.ConfigRuleContentJSCB.Text = "JS";
            this.ConfigRuleContentJSCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCode500CB
            // 
            this.ConfigRuleCode500CB.AutoSize = true;
            this.ConfigRuleCode500CB.Checked = true;
            this.ConfigRuleCode500CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleCode500CB.Location = new System.Drawing.Point(431, 224);
            this.ConfigRuleCode500CB.Name = "ConfigRuleCode500CB";
            this.ConfigRuleCode500CB.Size = new System.Drawing.Size(44, 17);
            this.ConfigRuleCode500CB.TabIndex = 22;
            this.ConfigRuleCode500CB.Text = "500";
            this.ConfigRuleCode500CB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleContentImgCB
            // 
            this.ConfigRuleContentImgCB.AutoSize = true;
            this.ConfigRuleContentImgCB.Location = new System.Drawing.Point(471, 244);
            this.ConfigRuleContentImgCB.Name = "ConfigRuleContentImgCB";
            this.ConfigRuleContentImgCB.Size = new System.Drawing.Size(60, 17);
            this.ConfigRuleContentImgCB.TabIndex = 2;
            this.ConfigRuleContentImgCB.Text = "Images";
            this.ConfigRuleContentImgCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCode4xxCB
            // 
            this.ConfigRuleCode4xxCB.AutoSize = true;
            this.ConfigRuleCode4xxCB.Location = new System.Drawing.Point(383, 224);
            this.ConfigRuleCode4xxCB.Name = "ConfigRuleCode4xxCB";
            this.ConfigRuleCode4xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigRuleCode4xxCB.TabIndex = 21;
            this.ConfigRuleCode4xxCB.Text = "4xx";
            this.ConfigRuleCode4xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleContentOtherBinaryCB
            // 
            this.ConfigRuleContentOtherBinaryCB.AutoSize = true;
            this.ConfigRuleContentOtherBinaryCB.Location = new System.Drawing.Point(537, 244);
            this.ConfigRuleContentOtherBinaryCB.Name = "ConfigRuleContentOtherBinaryCB";
            this.ConfigRuleContentOtherBinaryCB.Size = new System.Drawing.Size(84, 17);
            this.ConfigRuleContentOtherBinaryCB.TabIndex = 3;
            this.ConfigRuleContentOtherBinaryCB.Text = "Other Binary";
            this.ConfigRuleContentOtherBinaryCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCode403CB
            // 
            this.ConfigRuleCode403CB.AutoSize = true;
            this.ConfigRuleCode403CB.Checked = true;
            this.ConfigRuleCode403CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleCode403CB.Location = new System.Drawing.Point(333, 224);
            this.ConfigRuleCode403CB.Name = "ConfigRuleCode403CB";
            this.ConfigRuleCode403CB.Size = new System.Drawing.Size(44, 17);
            this.ConfigRuleCode403CB.TabIndex = 20;
            this.ConfigRuleCode403CB.Text = "403";
            this.ConfigRuleCode403CB.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Response Content Type:";
            // 
            // ConfigRuleCode3xxCB
            // 
            this.ConfigRuleCode3xxCB.AutoSize = true;
            this.ConfigRuleCode3xxCB.Location = new System.Drawing.Point(285, 224);
            this.ConfigRuleCode3xxCB.Name = "ConfigRuleCode3xxCB";
            this.ConfigRuleCode3xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigRuleCode3xxCB.TabIndex = 19;
            this.ConfigRuleCode3xxCB.Text = "3xx";
            this.ConfigRuleCode3xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleContentHTMLCB
            // 
            this.ConfigRuleContentHTMLCB.AutoSize = true;
            this.ConfigRuleContentHTMLCB.Checked = true;
            this.ConfigRuleContentHTMLCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleContentHTMLCB.Location = new System.Drawing.Point(129, 244);
            this.ConfigRuleContentHTMLCB.Name = "ConfigRuleContentHTMLCB";
            this.ConfigRuleContentHTMLCB.Size = new System.Drawing.Size(56, 17);
            this.ConfigRuleContentHTMLCB.TabIndex = 8;
            this.ConfigRuleContentHTMLCB.Text = "HTML";
            this.ConfigRuleContentHTMLCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCode301_2CB
            // 
            this.ConfigRuleCode301_2CB.AutoSize = true;
            this.ConfigRuleCode301_2CB.Checked = true;
            this.ConfigRuleCode301_2CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleCode301_2CB.Location = new System.Drawing.Point(226, 224);
            this.ConfigRuleCode301_2CB.Name = "ConfigRuleCode301_2CB";
            this.ConfigRuleCode301_2CB.Size = new System.Drawing.Size(53, 17);
            this.ConfigRuleCode301_2CB.TabIndex = 18;
            this.ConfigRuleCode301_2CB.Text = "301-2";
            this.ConfigRuleCode301_2CB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleGETMethodCB
            // 
            this.ConfigRuleGETMethodCB.AutoSize = true;
            this.ConfigRuleGETMethodCB.Checked = true;
            this.ConfigRuleGETMethodCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleGETMethodCB.Location = new System.Drawing.Point(99, 24);
            this.ConfigRuleGETMethodCB.Name = "ConfigRuleGETMethodCB";
            this.ConfigRuleGETMethodCB.Size = new System.Drawing.Size(48, 17);
            this.ConfigRuleGETMethodCB.TabIndex = 10;
            this.ConfigRuleGETMethodCB.Text = "GET";
            this.ConfigRuleGETMethodCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCode2xxCB
            // 
            this.ConfigRuleCode2xxCB.AutoSize = true;
            this.ConfigRuleCode2xxCB.Location = new System.Drawing.Point(178, 224);
            this.ConfigRuleCode2xxCB.Name = "ConfigRuleCode2xxCB";
            this.ConfigRuleCode2xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigRuleCode2xxCB.TabIndex = 17;
            this.ConfigRuleCode2xxCB.Text = "2xx";
            this.ConfigRuleCode2xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRulePOSTMethodCB
            // 
            this.ConfigRulePOSTMethodCB.AutoSize = true;
            this.ConfigRulePOSTMethodCB.Checked = true;
            this.ConfigRulePOSTMethodCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRulePOSTMethodCB.Location = new System.Drawing.Point(149, 24);
            this.ConfigRulePOSTMethodCB.Name = "ConfigRulePOSTMethodCB";
            this.ConfigRulePOSTMethodCB.Size = new System.Drawing.Size(55, 17);
            this.ConfigRulePOSTMethodCB.TabIndex = 11;
            this.ConfigRulePOSTMethodCB.Text = "POST";
            this.ConfigRulePOSTMethodCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleCode200CB
            // 
            this.ConfigRuleCode200CB.AutoSize = true;
            this.ConfigRuleCode200CB.Checked = true;
            this.ConfigRuleCode200CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleCode200CB.Location = new System.Drawing.Point(129, 224);
            this.ConfigRuleCode200CB.Name = "ConfigRuleCode200CB";
            this.ConfigRuleCode200CB.Size = new System.Drawing.Size(44, 17);
            this.ConfigRuleCode200CB.TabIndex = 16;
            this.ConfigRuleCode200CB.Text = "200";
            this.ConfigRuleCode200CB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleOtherMethodsCB
            // 
            this.ConfigRuleOtherMethodsCB.AutoSize = true;
            this.ConfigRuleOtherMethodsCB.Checked = true;
            this.ConfigRuleOtherMethodsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleOtherMethodsCB.Location = new System.Drawing.Point(209, 24);
            this.ConfigRuleOtherMethodsCB.Name = "ConfigRuleOtherMethodsCB";
            this.ConfigRuleOtherMethodsCB.Size = new System.Drawing.Size(52, 17);
            this.ConfigRuleOtherMethodsCB.TabIndex = 12;
            this.ConfigRuleOtherMethodsCB.Text = "Other";
            this.ConfigRuleOtherMethodsCB.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 224);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Response Status Code:";
            // 
            // ConfigRuleContentOtherTextCB
            // 
            this.ConfigRuleContentOtherTextCB.AutoSize = true;
            this.ConfigRuleContentOtherTextCB.Checked = true;
            this.ConfigRuleContentOtherTextCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleContentOtherTextCB.Location = new System.Drawing.Point(389, 244);
            this.ConfigRuleContentOtherTextCB.Name = "ConfigRuleContentOtherTextCB";
            this.ConfigRuleContentOtherTextCB.Size = new System.Drawing.Size(76, 17);
            this.ConfigRuleContentOtherTextCB.TabIndex = 13;
            this.ConfigRuleContentOtherTextCB.Text = "Other Text";
            this.ConfigRuleContentOtherTextCB.UseVisualStyleBackColor = true;
            // 
            // ConfigRuleContentXMLCB
            // 
            this.ConfigRuleContentXMLCB.AutoSize = true;
            this.ConfigRuleContentXMLCB.Checked = true;
            this.ConfigRuleContentXMLCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigRuleContentXMLCB.Location = new System.Drawing.Point(282, 244);
            this.ConfigRuleContentXMLCB.Name = "ConfigRuleContentXMLCB";
            this.ConfigRuleContentXMLCB.Size = new System.Drawing.Size(48, 17);
            this.ConfigRuleContentXMLCB.TabIndex = 14;
            this.ConfigRuleContentXMLCB.Text = "XML";
            this.ConfigRuleContentXMLCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRulesTab
            // 
            this.ConfigDisplayRulesTab.Controls.Add(this.label27);
            this.ConfigDisplayRulesTab.Controls.Add(this.label26);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleApplyChangesLL);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCancelChangesLL);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentJSONCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.groupBox3);
            this.ConfigDisplayRulesTab.Controls.Add(this.groupBox4);
            this.ConfigDisplayRulesTab.Controls.Add(this.label20);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentCSSCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode5xxCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentJSCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode500CB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentImgCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode4xxCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentOtherBinaryCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode403CB);
            this.ConfigDisplayRulesTab.Controls.Add(this.label21);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode3xxCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentHTMLCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode301_2CB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleGETMethodCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode2xxCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRulePOSTMethodCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleCode200CB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleOtherMethodsCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.label23);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentOtherTextCB);
            this.ConfigDisplayRulesTab.Controls.Add(this.ConfigDisplayRuleContentXMLCB);
            this.ConfigDisplayRulesTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigDisplayRulesTab.Name = "ConfigDisplayRulesTab";
            this.ConfigDisplayRulesTab.Size = new System.Drawing.Size(873, 0);
            this.ConfigDisplayRulesTab.TabIndex = 5;
            this.ConfigDisplayRulesTab.Text = "Proxy Traffic Display Rules";
            this.ConfigDisplayRulesTab.UseVisualStyleBackColor = true;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(7, 165);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(88, 13);
            this.label27.TabIndex = 95;
            this.label27.Text = "Response Rules:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(7, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(80, 13);
            this.label26.TabIndex = 94;
            this.label26.Text = "Request Rules:";
            // 
            // ConfigDisplayRuleApplyChangesLL
            // 
            this.ConfigDisplayRuleApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigDisplayRuleApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDisplayRuleApplyChangesLL.AutoSize = true;
            this.ConfigDisplayRuleApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigDisplayRuleApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigDisplayRuleApplyChangesLL.Location = new System.Drawing.Point(664, 5);
            this.ConfigDisplayRuleApplyChangesLL.Name = "ConfigDisplayRuleApplyChangesLL";
            this.ConfigDisplayRuleApplyChangesLL.Size = new System.Drawing.Size(91, 13);
            this.ConfigDisplayRuleApplyChangesLL.TabIndex = 93;
            this.ConfigDisplayRuleApplyChangesLL.TabStop = true;
            this.ConfigDisplayRuleApplyChangesLL.Text = "Apply Changes";
            this.ConfigDisplayRuleApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigDisplayRuleApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigDisplayRuleApplyChangesLL_LinkClicked);
            // 
            // ConfigDisplayRuleCancelChangesLL
            // 
            this.ConfigDisplayRuleCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDisplayRuleCancelChangesLL.AutoSize = true;
            this.ConfigDisplayRuleCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigDisplayRuleCancelChangesLL.Location = new System.Drawing.Point(771, 5);
            this.ConfigDisplayRuleCancelChangesLL.Name = "ConfigDisplayRuleCancelChangesLL";
            this.ConfigDisplayRuleCancelChangesLL.Size = new System.Drawing.Size(99, 13);
            this.ConfigDisplayRuleCancelChangesLL.TabIndex = 92;
            this.ConfigDisplayRuleCancelChangesLL.TabStop = true;
            this.ConfigDisplayRuleCancelChangesLL.Text = "Cancel Changes";
            this.ConfigDisplayRuleCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigDisplayRuleCancelChangesLL_LinkClicked);
            // 
            // ConfigDisplayRuleContentJSONCB
            // 
            this.ConfigDisplayRuleContentJSONCB.AutoSize = true;
            this.ConfigDisplayRuleContentJSONCB.Checked = true;
            this.ConfigDisplayRuleContentJSONCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleContentJSONCB.Location = new System.Drawing.Point(332, 207);
            this.ConfigDisplayRuleContentJSONCB.Name = "ConfigDisplayRuleContentJSONCB";
            this.ConfigDisplayRuleContentJSONCB.Size = new System.Drawing.Size(54, 17);
            this.ConfigDisplayRuleContentJSONCB.TabIndex = 91;
            this.ConfigDisplayRuleContentJSONCB.Text = "JSON";
            this.ConfigDisplayRuleContentJSONCB.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ConfigDisplayRuleHostNamesCB);
            this.groupBox3.Controls.Add(this.ConfigDisplayRuleHostNamesPlusTB);
            this.groupBox3.Controls.Add(this.ConfigDisplayRuleHostNamesMinusTB);
            this.groupBox3.Controls.Add(this.ConfigDisplayRuleHostNamesPlusRB);
            this.groupBox3.Controls.Add(this.ConfigDisplayRuleHostNamesMinusRB);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Location = new System.Drawing.Point(2, 97);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(868, 52);
            this.groupBox3.TabIndex = 90;
            this.groupBox3.TabStop = false;
            // 
            // ConfigDisplayRuleHostNamesCB
            // 
            this.ConfigDisplayRuleHostNamesCB.AutoSize = true;
            this.ConfigDisplayRuleHostNamesCB.Location = new System.Drawing.Point(8, 20);
            this.ConfigDisplayRuleHostNamesCB.Name = "ConfigDisplayRuleHostNamesCB";
            this.ConfigDisplayRuleHostNamesCB.Size = new System.Drawing.Size(84, 17);
            this.ConfigDisplayRuleHostNamesCB.TabIndex = 59;
            this.ConfigDisplayRuleHostNamesCB.Text = "HostNames:";
            this.ConfigDisplayRuleHostNamesCB.UseVisualStyleBackColor = true;
            this.ConfigDisplayRuleHostNamesCB.CheckedChanged += new System.EventHandler(this.ConfigDisplayRuleHostNamesCB_CheckedChanged);
            // 
            // ConfigDisplayRuleHostNamesPlusTB
            // 
            this.ConfigDisplayRuleHostNamesPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDisplayRuleHostNamesPlusTB.Enabled = false;
            this.ConfigDisplayRuleHostNamesPlusTB.Location = new System.Drawing.Point(160, 8);
            this.ConfigDisplayRuleHostNamesPlusTB.Name = "ConfigDisplayRuleHostNamesPlusTB";
            this.ConfigDisplayRuleHostNamesPlusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigDisplayRuleHostNamesPlusTB.TabIndex = 51;
            // 
            // ConfigDisplayRuleHostNamesMinusTB
            // 
            this.ConfigDisplayRuleHostNamesMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDisplayRuleHostNamesMinusTB.Enabled = false;
            this.ConfigDisplayRuleHostNamesMinusTB.Location = new System.Drawing.Point(160, 29);
            this.ConfigDisplayRuleHostNamesMinusTB.Name = "ConfigDisplayRuleHostNamesMinusTB";
            this.ConfigDisplayRuleHostNamesMinusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigDisplayRuleHostNamesMinusTB.TabIndex = 52;
            // 
            // ConfigDisplayRuleHostNamesPlusRB
            // 
            this.ConfigDisplayRuleHostNamesPlusRB.AutoSize = true;
            this.ConfigDisplayRuleHostNamesPlusRB.Enabled = false;
            this.ConfigDisplayRuleHostNamesPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigDisplayRuleHostNamesPlusRB.Location = new System.Drawing.Point(118, 9);
            this.ConfigDisplayRuleHostNamesPlusRB.Name = "ConfigDisplayRuleHostNamesPlusRB";
            this.ConfigDisplayRuleHostNamesPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ConfigDisplayRuleHostNamesPlusRB.TabIndex = 57;
            this.ConfigDisplayRuleHostNamesPlusRB.TabStop = true;
            this.ConfigDisplayRuleHostNamesPlusRB.Text = "+";
            this.ConfigDisplayRuleHostNamesPlusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleHostNamesMinusRB
            // 
            this.ConfigDisplayRuleHostNamesMinusRB.AutoSize = true;
            this.ConfigDisplayRuleHostNamesMinusRB.Enabled = false;
            this.ConfigDisplayRuleHostNamesMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigDisplayRuleHostNamesMinusRB.Location = new System.Drawing.Point(118, 28);
            this.ConfigDisplayRuleHostNamesMinusRB.Name = "ConfigDisplayRuleHostNamesMinusRB";
            this.ConfigDisplayRuleHostNamesMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ConfigDisplayRuleHostNamesMinusRB.TabIndex = 58;
            this.ConfigDisplayRuleHostNamesMinusRB.TabStop = true;
            this.ConfigDisplayRuleHostNamesMinusRB.Text = "-";
            this.ConfigDisplayRuleHostNamesMinusRB.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.ConfigDisplayRuleFileExtensionsCB);
            this.groupBox4.Controls.Add(this.ConfigDisplayRuleFileExtensionsPlusTB);
            this.groupBox4.Controls.Add(this.ConfigDisplayRuleFileExtensionsMinusTB);
            this.groupBox4.Controls.Add(this.ConfigDisplayRuleFileExtensionsPlusRB);
            this.groupBox4.Controls.Add(this.ConfigDisplayRuleFileExtensionsMinusRB);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(2, 46);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(868, 52);
            this.groupBox4.TabIndex = 89;
            this.groupBox4.TabStop = false;
            // 
            // ConfigDisplayRuleFileExtensionsCB
            // 
            this.ConfigDisplayRuleFileExtensionsCB.AutoSize = true;
            this.ConfigDisplayRuleFileExtensionsCB.Checked = true;
            this.ConfigDisplayRuleFileExtensionsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleFileExtensionsCB.Location = new System.Drawing.Point(8, 20);
            this.ConfigDisplayRuleFileExtensionsCB.Name = "ConfigDisplayRuleFileExtensionsCB";
            this.ConfigDisplayRuleFileExtensionsCB.Size = new System.Drawing.Size(99, 17);
            this.ConfigDisplayRuleFileExtensionsCB.TabIndex = 59;
            this.ConfigDisplayRuleFileExtensionsCB.Text = "File Extensions:";
            this.ConfigDisplayRuleFileExtensionsCB.UseVisualStyleBackColor = true;
            this.ConfigDisplayRuleFileExtensionsCB.CheckedChanged += new System.EventHandler(this.ConfigDisplayRuleFileExtensionsCB_CheckedChanged);
            // 
            // ConfigDisplayRuleFileExtensionsPlusTB
            // 
            this.ConfigDisplayRuleFileExtensionsPlusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDisplayRuleFileExtensionsPlusTB.Location = new System.Drawing.Point(160, 8);
            this.ConfigDisplayRuleFileExtensionsPlusTB.Name = "ConfigDisplayRuleFileExtensionsPlusTB";
            this.ConfigDisplayRuleFileExtensionsPlusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigDisplayRuleFileExtensionsPlusTB.TabIndex = 51;
            // 
            // ConfigDisplayRuleFileExtensionsMinusTB
            // 
            this.ConfigDisplayRuleFileExtensionsMinusTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDisplayRuleFileExtensionsMinusTB.Location = new System.Drawing.Point(160, 29);
            this.ConfigDisplayRuleFileExtensionsMinusTB.Name = "ConfigDisplayRuleFileExtensionsMinusTB";
            this.ConfigDisplayRuleFileExtensionsMinusTB.Size = new System.Drawing.Size(705, 20);
            this.ConfigDisplayRuleFileExtensionsMinusTB.TabIndex = 52;
            this.ConfigDisplayRuleFileExtensionsMinusTB.Text = "css,js,jpg,jpeg,png,gif,ico,swf,doc,docx,pdf,xls,xlsx,ppt,pptx";
            // 
            // ConfigDisplayRuleFileExtensionsPlusRB
            // 
            this.ConfigDisplayRuleFileExtensionsPlusRB.AutoSize = true;
            this.ConfigDisplayRuleFileExtensionsPlusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigDisplayRuleFileExtensionsPlusRB.Location = new System.Drawing.Point(118, 9);
            this.ConfigDisplayRuleFileExtensionsPlusRB.Name = "ConfigDisplayRuleFileExtensionsPlusRB";
            this.ConfigDisplayRuleFileExtensionsPlusRB.Size = new System.Drawing.Size(34, 20);
            this.ConfigDisplayRuleFileExtensionsPlusRB.TabIndex = 57;
            this.ConfigDisplayRuleFileExtensionsPlusRB.Text = "+";
            this.ConfigDisplayRuleFileExtensionsPlusRB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleFileExtensionsMinusRB
            // 
            this.ConfigDisplayRuleFileExtensionsMinusRB.AutoSize = true;
            this.ConfigDisplayRuleFileExtensionsMinusRB.Checked = true;
            this.ConfigDisplayRuleFileExtensionsMinusRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigDisplayRuleFileExtensionsMinusRB.Location = new System.Drawing.Point(118, 28);
            this.ConfigDisplayRuleFileExtensionsMinusRB.Name = "ConfigDisplayRuleFileExtensionsMinusRB";
            this.ConfigDisplayRuleFileExtensionsMinusRB.Size = new System.Drawing.Size(31, 20);
            this.ConfigDisplayRuleFileExtensionsMinusRB.TabIndex = 58;
            this.ConfigDisplayRuleFileExtensionsMinusRB.TabStop = true;
            this.ConfigDisplayRuleFileExtensionsMinusRB.Text = "-";
            this.ConfigDisplayRuleFileExtensionsMinusRB.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 13);
            this.label20.TabIndex = 74;
            this.label20.Text = "Request Method:";
            // 
            // ConfigDisplayRuleContentCSSCB
            // 
            this.ConfigDisplayRuleContentCSSCB.AutoSize = true;
            this.ConfigDisplayRuleContentCSSCB.Location = new System.Drawing.Point(230, 207);
            this.ConfigDisplayRuleContentCSSCB.Name = "ConfigDisplayRuleContentCSSCB";
            this.ConfigDisplayRuleContentCSSCB.Size = new System.Drawing.Size(47, 17);
            this.ConfigDisplayRuleContentCSSCB.TabIndex = 68;
            this.ConfigDisplayRuleContentCSSCB.Text = "CSS";
            this.ConfigDisplayRuleContentCSSCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleCode5xxCB
            // 
            this.ConfigDisplayRuleCode5xxCB.AutoSize = true;
            this.ConfigDisplayRuleCode5xxCB.Location = new System.Drawing.Point(482, 187);
            this.ConfigDisplayRuleCode5xxCB.Name = "ConfigDisplayRuleCode5xxCB";
            this.ConfigDisplayRuleCode5xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigDisplayRuleCode5xxCB.TabIndex = 88;
            this.ConfigDisplayRuleCode5xxCB.Text = "5xx";
            this.ConfigDisplayRuleCode5xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleContentJSCB
            // 
            this.ConfigDisplayRuleContentJSCB.AutoSize = true;
            this.ConfigDisplayRuleContentJSCB.Location = new System.Drawing.Point(186, 207);
            this.ConfigDisplayRuleContentJSCB.Name = "ConfigDisplayRuleContentJSCB";
            this.ConfigDisplayRuleContentJSCB.Size = new System.Drawing.Size(38, 17);
            this.ConfigDisplayRuleContentJSCB.TabIndex = 69;
            this.ConfigDisplayRuleContentJSCB.Text = "JS";
            this.ConfigDisplayRuleContentJSCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleCode500CB
            // 
            this.ConfigDisplayRuleCode500CB.AutoSize = true;
            this.ConfigDisplayRuleCode500CB.Checked = true;
            this.ConfigDisplayRuleCode500CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleCode500CB.Location = new System.Drawing.Point(432, 187);
            this.ConfigDisplayRuleCode500CB.Name = "ConfigDisplayRuleCode500CB";
            this.ConfigDisplayRuleCode500CB.Size = new System.Drawing.Size(44, 17);
            this.ConfigDisplayRuleCode500CB.TabIndex = 87;
            this.ConfigDisplayRuleCode500CB.Text = "500";
            this.ConfigDisplayRuleCode500CB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleContentImgCB
            // 
            this.ConfigDisplayRuleContentImgCB.AutoSize = true;
            this.ConfigDisplayRuleContentImgCB.Location = new System.Drawing.Point(472, 207);
            this.ConfigDisplayRuleContentImgCB.Name = "ConfigDisplayRuleContentImgCB";
            this.ConfigDisplayRuleContentImgCB.Size = new System.Drawing.Size(60, 17);
            this.ConfigDisplayRuleContentImgCB.TabIndex = 70;
            this.ConfigDisplayRuleContentImgCB.Text = "Images";
            this.ConfigDisplayRuleContentImgCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleCode4xxCB
            // 
            this.ConfigDisplayRuleCode4xxCB.AutoSize = true;
            this.ConfigDisplayRuleCode4xxCB.Location = new System.Drawing.Point(384, 187);
            this.ConfigDisplayRuleCode4xxCB.Name = "ConfigDisplayRuleCode4xxCB";
            this.ConfigDisplayRuleCode4xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigDisplayRuleCode4xxCB.TabIndex = 86;
            this.ConfigDisplayRuleCode4xxCB.Text = "4xx";
            this.ConfigDisplayRuleCode4xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleContentOtherBinaryCB
            // 
            this.ConfigDisplayRuleContentOtherBinaryCB.AutoSize = true;
            this.ConfigDisplayRuleContentOtherBinaryCB.Location = new System.Drawing.Point(538, 207);
            this.ConfigDisplayRuleContentOtherBinaryCB.Name = "ConfigDisplayRuleContentOtherBinaryCB";
            this.ConfigDisplayRuleContentOtherBinaryCB.Size = new System.Drawing.Size(84, 17);
            this.ConfigDisplayRuleContentOtherBinaryCB.TabIndex = 71;
            this.ConfigDisplayRuleContentOtherBinaryCB.Text = "Other Binary";
            this.ConfigDisplayRuleContentOtherBinaryCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleCode403CB
            // 
            this.ConfigDisplayRuleCode403CB.AutoSize = true;
            this.ConfigDisplayRuleCode403CB.Checked = true;
            this.ConfigDisplayRuleCode403CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleCode403CB.Location = new System.Drawing.Point(334, 187);
            this.ConfigDisplayRuleCode403CB.Name = "ConfigDisplayRuleCode403CB";
            this.ConfigDisplayRuleCode403CB.Size = new System.Drawing.Size(44, 17);
            this.ConfigDisplayRuleCode403CB.TabIndex = 85;
            this.ConfigDisplayRuleCode403CB.Text = "403";
            this.ConfigDisplayRuleCode403CB.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(5, 207);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(125, 13);
            this.label21.TabIndex = 72;
            this.label21.Text = "Response Content Type:";
            // 
            // ConfigDisplayRuleCode3xxCB
            // 
            this.ConfigDisplayRuleCode3xxCB.AutoSize = true;
            this.ConfigDisplayRuleCode3xxCB.Location = new System.Drawing.Point(286, 187);
            this.ConfigDisplayRuleCode3xxCB.Name = "ConfigDisplayRuleCode3xxCB";
            this.ConfigDisplayRuleCode3xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigDisplayRuleCode3xxCB.TabIndex = 84;
            this.ConfigDisplayRuleCode3xxCB.Text = "3xx";
            this.ConfigDisplayRuleCode3xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleContentHTMLCB
            // 
            this.ConfigDisplayRuleContentHTMLCB.AutoSize = true;
            this.ConfigDisplayRuleContentHTMLCB.Checked = true;
            this.ConfigDisplayRuleContentHTMLCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleContentHTMLCB.Location = new System.Drawing.Point(130, 207);
            this.ConfigDisplayRuleContentHTMLCB.Name = "ConfigDisplayRuleContentHTMLCB";
            this.ConfigDisplayRuleContentHTMLCB.Size = new System.Drawing.Size(56, 17);
            this.ConfigDisplayRuleContentHTMLCB.TabIndex = 73;
            this.ConfigDisplayRuleContentHTMLCB.Text = "HTML";
            this.ConfigDisplayRuleContentHTMLCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleCode301_2CB
            // 
            this.ConfigDisplayRuleCode301_2CB.AutoSize = true;
            this.ConfigDisplayRuleCode301_2CB.Checked = true;
            this.ConfigDisplayRuleCode301_2CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleCode301_2CB.Location = new System.Drawing.Point(227, 187);
            this.ConfigDisplayRuleCode301_2CB.Name = "ConfigDisplayRuleCode301_2CB";
            this.ConfigDisplayRuleCode301_2CB.Size = new System.Drawing.Size(53, 17);
            this.ConfigDisplayRuleCode301_2CB.TabIndex = 83;
            this.ConfigDisplayRuleCode301_2CB.Text = "301-2";
            this.ConfigDisplayRuleCode301_2CB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleGETMethodCB
            // 
            this.ConfigDisplayRuleGETMethodCB.AutoSize = true;
            this.ConfigDisplayRuleGETMethodCB.Checked = true;
            this.ConfigDisplayRuleGETMethodCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleGETMethodCB.Location = new System.Drawing.Point(132, 32);
            this.ConfigDisplayRuleGETMethodCB.Name = "ConfigDisplayRuleGETMethodCB";
            this.ConfigDisplayRuleGETMethodCB.Size = new System.Drawing.Size(48, 17);
            this.ConfigDisplayRuleGETMethodCB.TabIndex = 75;
            this.ConfigDisplayRuleGETMethodCB.Text = "GET";
            this.ConfigDisplayRuleGETMethodCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleCode2xxCB
            // 
            this.ConfigDisplayRuleCode2xxCB.AutoSize = true;
            this.ConfigDisplayRuleCode2xxCB.Location = new System.Drawing.Point(179, 187);
            this.ConfigDisplayRuleCode2xxCB.Name = "ConfigDisplayRuleCode2xxCB";
            this.ConfigDisplayRuleCode2xxCB.Size = new System.Drawing.Size(42, 17);
            this.ConfigDisplayRuleCode2xxCB.TabIndex = 82;
            this.ConfigDisplayRuleCode2xxCB.Text = "2xx";
            this.ConfigDisplayRuleCode2xxCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRulePOSTMethodCB
            // 
            this.ConfigDisplayRulePOSTMethodCB.AutoSize = true;
            this.ConfigDisplayRulePOSTMethodCB.Checked = true;
            this.ConfigDisplayRulePOSTMethodCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRulePOSTMethodCB.Location = new System.Drawing.Point(182, 32);
            this.ConfigDisplayRulePOSTMethodCB.Name = "ConfigDisplayRulePOSTMethodCB";
            this.ConfigDisplayRulePOSTMethodCB.Size = new System.Drawing.Size(55, 17);
            this.ConfigDisplayRulePOSTMethodCB.TabIndex = 76;
            this.ConfigDisplayRulePOSTMethodCB.Text = "POST";
            this.ConfigDisplayRulePOSTMethodCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleCode200CB
            // 
            this.ConfigDisplayRuleCode200CB.AutoSize = true;
            this.ConfigDisplayRuleCode200CB.Checked = true;
            this.ConfigDisplayRuleCode200CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleCode200CB.Location = new System.Drawing.Point(130, 187);
            this.ConfigDisplayRuleCode200CB.Name = "ConfigDisplayRuleCode200CB";
            this.ConfigDisplayRuleCode200CB.Size = new System.Drawing.Size(44, 17);
            this.ConfigDisplayRuleCode200CB.TabIndex = 81;
            this.ConfigDisplayRuleCode200CB.Text = "200";
            this.ConfigDisplayRuleCode200CB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleOtherMethodsCB
            // 
            this.ConfigDisplayRuleOtherMethodsCB.AutoSize = true;
            this.ConfigDisplayRuleOtherMethodsCB.Checked = true;
            this.ConfigDisplayRuleOtherMethodsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleOtherMethodsCB.Location = new System.Drawing.Point(242, 32);
            this.ConfigDisplayRuleOtherMethodsCB.Name = "ConfigDisplayRuleOtherMethodsCB";
            this.ConfigDisplayRuleOtherMethodsCB.Size = new System.Drawing.Size(52, 17);
            this.ConfigDisplayRuleOtherMethodsCB.TabIndex = 77;
            this.ConfigDisplayRuleOtherMethodsCB.Text = "Other";
            this.ConfigDisplayRuleOtherMethodsCB.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 187);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(119, 13);
            this.label23.TabIndex = 80;
            this.label23.Text = "Response Status Code:";
            // 
            // ConfigDisplayRuleContentOtherTextCB
            // 
            this.ConfigDisplayRuleContentOtherTextCB.AutoSize = true;
            this.ConfigDisplayRuleContentOtherTextCB.Checked = true;
            this.ConfigDisplayRuleContentOtherTextCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleContentOtherTextCB.Location = new System.Drawing.Point(390, 207);
            this.ConfigDisplayRuleContentOtherTextCB.Name = "ConfigDisplayRuleContentOtherTextCB";
            this.ConfigDisplayRuleContentOtherTextCB.Size = new System.Drawing.Size(76, 17);
            this.ConfigDisplayRuleContentOtherTextCB.TabIndex = 78;
            this.ConfigDisplayRuleContentOtherTextCB.Text = "Other Text";
            this.ConfigDisplayRuleContentOtherTextCB.UseVisualStyleBackColor = true;
            // 
            // ConfigDisplayRuleContentXMLCB
            // 
            this.ConfigDisplayRuleContentXMLCB.AutoSize = true;
            this.ConfigDisplayRuleContentXMLCB.Checked = true;
            this.ConfigDisplayRuleContentXMLCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigDisplayRuleContentXMLCB.Location = new System.Drawing.Point(283, 207);
            this.ConfigDisplayRuleContentXMLCB.Name = "ConfigDisplayRuleContentXMLCB";
            this.ConfigDisplayRuleContentXMLCB.Size = new System.Drawing.Size(48, 17);
            this.ConfigDisplayRuleContentXMLCB.TabIndex = 79;
            this.ConfigDisplayRuleContentXMLCB.Text = "XML";
            this.ConfigDisplayRuleContentXMLCB.UseVisualStyleBackColor = true;
            // 
            // ConfigScriptingTab
            // 
            this.ConfigScriptingTab.Controls.Add(this.ConfigScriptBaseSplit);
            this.ConfigScriptingTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigScriptingTab.Name = "ConfigScriptingTab";
            this.ConfigScriptingTab.Size = new System.Drawing.Size(873, 0);
            this.ConfigScriptingTab.TabIndex = 2;
            this.ConfigScriptingTab.Text = "Scripting Engines";
            this.ConfigScriptingTab.UseVisualStyleBackColor = true;
            // 
            // ConfigScriptBaseSplit
            // 
            this.ConfigScriptBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigScriptBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.ConfigScriptBaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigScriptBaseSplit.Name = "ConfigScriptBaseSplit";
            // 
            // ConfigScriptBaseSplit.Panel1
            // 
            this.ConfigScriptBaseSplit.Panel1.Controls.Add(this.label14);
            this.ConfigScriptBaseSplit.Panel1.Controls.Add(this.ConfigScriptPathApplyChangesLL);
            this.ConfigScriptBaseSplit.Panel1.Controls.Add(this.ConfigScriptPathCancelChangesLL);
            this.ConfigScriptBaseSplit.Panel1.Controls.Add(this.ConfigScriptPathSplit);
            // 
            // ConfigScriptBaseSplit.Panel2
            // 
            this.ConfigScriptBaseSplit.Panel2.Controls.Add(this.label12);
            this.ConfigScriptBaseSplit.Panel2.Controls.Add(this.ConfigScriptCommandApplyChangesLL);
            this.ConfigScriptBaseSplit.Panel2.Controls.Add(this.ConfigScriptCommandCancelChangesLL);
            this.ConfigScriptBaseSplit.Panel2.Controls.Add(this.ConfigScriptCommandSplit);
            this.ConfigScriptBaseSplit.Size = new System.Drawing.Size(873, 0);
            this.ConfigScriptBaseSplit.SplitterDistance = 425;
            this.ConfigScriptBaseSplit.SplitterWidth = 2;
            this.ConfigScriptBaseSplit.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(177, 13);
            this.label14.TabIndex = 62;
            this.label14.Text = "Library Paths to include on Start-Up:";
            // 
            // ConfigScriptPathApplyChangesLL
            // 
            this.ConfigScriptPathApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigScriptPathApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptPathApplyChangesLL.AutoSize = true;
            this.ConfigScriptPathApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigScriptPathApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigScriptPathApplyChangesLL.Location = new System.Drawing.Point(329, 6);
            this.ConfigScriptPathApplyChangesLL.Name = "ConfigScriptPathApplyChangesLL";
            this.ConfigScriptPathApplyChangesLL.Size = new System.Drawing.Size(36, 13);
            this.ConfigScriptPathApplyChangesLL.TabIndex = 58;
            this.ConfigScriptPathApplyChangesLL.TabStop = true;
            this.ConfigScriptPathApplyChangesLL.Text = "Save";
            this.ConfigScriptPathApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigScriptPathApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigScriptPathApplyChangesLL_LinkClicked);
            // 
            // ConfigScriptPathCancelChangesLL
            // 
            this.ConfigScriptPathCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptPathCancelChangesLL.AutoSize = true;
            this.ConfigScriptPathCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigScriptPathCancelChangesLL.Location = new System.Drawing.Point(374, 6);
            this.ConfigScriptPathCancelChangesLL.Name = "ConfigScriptPathCancelChangesLL";
            this.ConfigScriptPathCancelChangesLL.Size = new System.Drawing.Size(46, 13);
            this.ConfigScriptPathCancelChangesLL.TabIndex = 57;
            this.ConfigScriptPathCancelChangesLL.TabStop = true;
            this.ConfigScriptPathCancelChangesLL.Text = "Cancel";
            this.ConfigScriptPathCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigScriptPathCancelChangesLL_LinkClicked);
            // 
            // ConfigScriptPathSplit
            // 
            this.ConfigScriptPathSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptPathSplit.Location = new System.Drawing.Point(0, 20);
            this.ConfigScriptPathSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigScriptPathSplit.Name = "ConfigScriptPathSplit";
            this.ConfigScriptPathSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ConfigScriptPathSplit.Panel1
            // 
            this.ConfigScriptPathSplit.Panel1.Controls.Add(this.label15);
            this.ConfigScriptPathSplit.Panel1.Controls.Add(this.ConfigScriptPyPathsTB);
            // 
            // ConfigScriptPathSplit.Panel2
            // 
            this.ConfigScriptPathSplit.Panel2.Controls.Add(this.label16);
            this.ConfigScriptPathSplit.Panel2.Controls.Add(this.ConfigScriptRbPathsTB);
            this.ConfigScriptPathSplit.Size = new System.Drawing.Size(425, 54);
            this.ConfigScriptPathSplit.SplitterDistance = 25;
            this.ConfigScriptPathSplit.SplitterWidth = 2;
            this.ConfigScriptPathSplit.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 63;
            this.label15.Text = "Python:";
            // 
            // ConfigScriptPyPathsTB
            // 
            this.ConfigScriptPyPathsTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptPyPathsTB.Location = new System.Drawing.Point(0, 20);
            this.ConfigScriptPyPathsTB.Multiline = true;
            this.ConfigScriptPyPathsTB.Name = "ConfigScriptPyPathsTB";
            this.ConfigScriptPyPathsTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigScriptPyPathsTB.Size = new System.Drawing.Size(425, 8);
            this.ConfigScriptPyPathsTB.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 64;
            this.label16.Text = "Ruby:";
            // 
            // ConfigScriptRbPathsTB
            // 
            this.ConfigScriptRbPathsTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptRbPathsTB.Location = new System.Drawing.Point(0, 20);
            this.ConfigScriptRbPathsTB.Multiline = true;
            this.ConfigScriptRbPathsTB.Name = "ConfigScriptRbPathsTB";
            this.ConfigScriptRbPathsTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigScriptRbPathsTB.Size = new System.Drawing.Size(425, 0);
            this.ConfigScriptRbPathsTB.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 13);
            this.label12.TabIndex = 61;
            this.label12.Text = "Commands to run on Start-Up:";
            // 
            // ConfigScriptCommandApplyChangesLL
            // 
            this.ConfigScriptCommandApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigScriptCommandApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptCommandApplyChangesLL.AutoSize = true;
            this.ConfigScriptCommandApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigScriptCommandApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigScriptCommandApplyChangesLL.Location = new System.Drawing.Point(352, 6);
            this.ConfigScriptCommandApplyChangesLL.Name = "ConfigScriptCommandApplyChangesLL";
            this.ConfigScriptCommandApplyChangesLL.Size = new System.Drawing.Size(36, 13);
            this.ConfigScriptCommandApplyChangesLL.TabIndex = 60;
            this.ConfigScriptCommandApplyChangesLL.TabStop = true;
            this.ConfigScriptCommandApplyChangesLL.Text = "Save";
            this.ConfigScriptCommandApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigScriptCommandApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigScriptCommandApplyChangesLL_LinkClicked);
            // 
            // ConfigScriptCommandCancelChangesLL
            // 
            this.ConfigScriptCommandCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptCommandCancelChangesLL.AutoSize = true;
            this.ConfigScriptCommandCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigScriptCommandCancelChangesLL.Location = new System.Drawing.Point(397, 6);
            this.ConfigScriptCommandCancelChangesLL.Name = "ConfigScriptCommandCancelChangesLL";
            this.ConfigScriptCommandCancelChangesLL.Size = new System.Drawing.Size(46, 13);
            this.ConfigScriptCommandCancelChangesLL.TabIndex = 59;
            this.ConfigScriptCommandCancelChangesLL.TabStop = true;
            this.ConfigScriptCommandCancelChangesLL.Text = "Cancel";
            this.ConfigScriptCommandCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigScriptCommandCancelChangesLL_LinkClicked);
            // 
            // ConfigScriptCommandSplit
            // 
            this.ConfigScriptCommandSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptCommandSplit.Location = new System.Drawing.Point(0, 20);
            this.ConfigScriptCommandSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigScriptCommandSplit.Name = "ConfigScriptCommandSplit";
            this.ConfigScriptCommandSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ConfigScriptCommandSplit.Panel1
            // 
            this.ConfigScriptCommandSplit.Panel1.Controls.Add(this.label17);
            this.ConfigScriptCommandSplit.Panel1.Controls.Add(this.ConfigScriptPyCommandsTB);
            // 
            // ConfigScriptCommandSplit.Panel2
            // 
            this.ConfigScriptCommandSplit.Panel2.Controls.Add(this.label18);
            this.ConfigScriptCommandSplit.Panel2.Controls.Add(this.ConfigScriptRbCommandsTB);
            this.ConfigScriptCommandSplit.Size = new System.Drawing.Size(446, 54);
            this.ConfigScriptCommandSplit.SplitterDistance = 25;
            this.ConfigScriptCommandSplit.SplitterWidth = 2;
            this.ConfigScriptCommandSplit.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 13);
            this.label17.TabIndex = 64;
            this.label17.Text = "Python:";
            // 
            // ConfigScriptPyCommandsTB
            // 
            this.ConfigScriptPyCommandsTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptPyCommandsTB.Location = new System.Drawing.Point(0, 20);
            this.ConfigScriptPyCommandsTB.Multiline = true;
            this.ConfigScriptPyCommandsTB.Name = "ConfigScriptPyCommandsTB";
            this.ConfigScriptPyCommandsTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigScriptPyCommandsTB.Size = new System.Drawing.Size(446, 7);
            this.ConfigScriptPyCommandsTB.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 65;
            this.label18.Text = "Ruby:";
            // 
            // ConfigScriptRbCommandsTB
            // 
            this.ConfigScriptRbCommandsTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScriptRbCommandsTB.Location = new System.Drawing.Point(0, 20);
            this.ConfigScriptRbCommandsTB.Multiline = true;
            this.ConfigScriptRbCommandsTB.Name = "ConfigScriptRbCommandsTB";
            this.ConfigScriptRbCommandsTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigScriptRbCommandsTB.Size = new System.Drawing.Size(446, 0);
            this.ConfigScriptRbCommandsTB.TabIndex = 1;
            // 
            // ConfigHTTPAPITab
            // 
            this.ConfigHTTPAPITab.Controls.Add(this.ConfigHTTPAPIBaseSplit);
            this.ConfigHTTPAPITab.Location = new System.Drawing.Point(4, 22);
            this.ConfigHTTPAPITab.Name = "ConfigHTTPAPITab";
            this.ConfigHTTPAPITab.Size = new System.Drawing.Size(873, 0);
            this.ConfigHTTPAPITab.TabIndex = 3;
            this.ConfigHTTPAPITab.Text = "HTTP API";
            this.ConfigHTTPAPITab.UseVisualStyleBackColor = true;
            // 
            // ConfigHTTPAPIBaseSplit
            // 
            this.ConfigHTTPAPIBaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigHTTPAPIBaseSplit.Location = new System.Drawing.Point(0, 0);
            this.ConfigHTTPAPIBaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigHTTPAPIBaseSplit.Name = "ConfigHTTPAPIBaseSplit";
            // 
            // ConfigHTTPAPIBaseSplit.Panel1
            // 
            this.ConfigHTTPAPIBaseSplit.Panel1.Controls.Add(this.ConfigRequestTypesTB);
            this.ConfigHTTPAPIBaseSplit.Panel1.Controls.Add(this.label19);
            this.ConfigHTTPAPIBaseSplit.Panel1.Controls.Add(this.ConfigRequestTypesCancelChangesLL);
            this.ConfigHTTPAPIBaseSplit.Panel1.Controls.Add(this.ConfigRequestTypesApplyChangesLL);
            // 
            // ConfigHTTPAPIBaseSplit.Panel2
            // 
            this.ConfigHTTPAPIBaseSplit.Panel2.Controls.Add(this.ConfigResponseTypesTB);
            this.ConfigHTTPAPIBaseSplit.Panel2.Controls.Add(this.label22);
            this.ConfigHTTPAPIBaseSplit.Panel2.Controls.Add(this.ConfigResponseTypesApplyChangesLL);
            this.ConfigHTTPAPIBaseSplit.Panel2.Controls.Add(this.ConfigResponseTypesCancelChangesLL);
            this.ConfigHTTPAPIBaseSplit.Size = new System.Drawing.Size(873, 0);
            this.ConfigHTTPAPIBaseSplit.SplitterDistance = 425;
            this.ConfigHTTPAPIBaseSplit.SplitterWidth = 2;
            this.ConfigHTTPAPIBaseSplit.TabIndex = 1;
            // 
            // ConfigRequestTypesTB
            // 
            this.ConfigRequestTypesTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRequestTypesTB.Location = new System.Drawing.Point(0, 20);
            this.ConfigRequestTypesTB.Multiline = true;
            this.ConfigRequestTypesTB.Name = "ConfigRequestTypesTB";
            this.ConfigRequestTypesTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigRequestTypesTB.Size = new System.Drawing.Size(425, 0);
            this.ConfigRequestTypesTB.TabIndex = 69;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(239, 13);
            this.label19.TabIndex = 68;
            this.label19.Text = "Request Content-Type headers treated as TEXT:";
            // 
            // ConfigRequestTypesCancelChangesLL
            // 
            this.ConfigRequestTypesCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRequestTypesCancelChangesLL.AutoSize = true;
            this.ConfigRequestTypesCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRequestTypesCancelChangesLL.Location = new System.Drawing.Point(377, 4);
            this.ConfigRequestTypesCancelChangesLL.Name = "ConfigRequestTypesCancelChangesLL";
            this.ConfigRequestTypesCancelChangesLL.Size = new System.Drawing.Size(46, 13);
            this.ConfigRequestTypesCancelChangesLL.TabIndex = 66;
            this.ConfigRequestTypesCancelChangesLL.TabStop = true;
            this.ConfigRequestTypesCancelChangesLL.Text = "Cancel";
            this.ConfigRequestTypesCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigRequestTypesCancelChangesLL_LinkClicked);
            // 
            // ConfigRequestTypesApplyChangesLL
            // 
            this.ConfigRequestTypesApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigRequestTypesApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigRequestTypesApplyChangesLL.AutoSize = true;
            this.ConfigRequestTypesApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigRequestTypesApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigRequestTypesApplyChangesLL.Location = new System.Drawing.Point(329, 4);
            this.ConfigRequestTypesApplyChangesLL.Name = "ConfigRequestTypesApplyChangesLL";
            this.ConfigRequestTypesApplyChangesLL.Size = new System.Drawing.Size(38, 13);
            this.ConfigRequestTypesApplyChangesLL.TabIndex = 67;
            this.ConfigRequestTypesApplyChangesLL.TabStop = true;
            this.ConfigRequestTypesApplyChangesLL.Text = "Apply";
            this.ConfigRequestTypesApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigRequestTypesApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigRequestTypesApplyChangesLL_LinkClicked);
            // 
            // ConfigResponseTypesTB
            // 
            this.ConfigResponseTypesTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigResponseTypesTB.Location = new System.Drawing.Point(0, 20);
            this.ConfigResponseTypesTB.Multiline = true;
            this.ConfigResponseTypesTB.Name = "ConfigResponseTypesTB";
            this.ConfigResponseTypesTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigResponseTypesTB.Size = new System.Drawing.Size(446, 0);
            this.ConfigResponseTypesTB.TabIndex = 65;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(247, 13);
            this.label22.TabIndex = 61;
            this.label22.Text = "Response Content-Type headers treated as TEXT:";
            // 
            // ConfigResponseTypesApplyChangesLL
            // 
            this.ConfigResponseTypesApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigResponseTypesApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigResponseTypesApplyChangesLL.AutoSize = true;
            this.ConfigResponseTypesApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigResponseTypesApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigResponseTypesApplyChangesLL.Location = new System.Drawing.Point(352, 6);
            this.ConfigResponseTypesApplyChangesLL.Name = "ConfigResponseTypesApplyChangesLL";
            this.ConfigResponseTypesApplyChangesLL.Size = new System.Drawing.Size(38, 13);
            this.ConfigResponseTypesApplyChangesLL.TabIndex = 60;
            this.ConfigResponseTypesApplyChangesLL.TabStop = true;
            this.ConfigResponseTypesApplyChangesLL.Text = "Apply";
            this.ConfigResponseTypesApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigResponseTypesApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigResponseTypesApplyChangesLL_LinkClicked);
            // 
            // ConfigResponseTypesCancelChangesLL
            // 
            this.ConfigResponseTypesCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigResponseTypesCancelChangesLL.AutoSize = true;
            this.ConfigResponseTypesCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigResponseTypesCancelChangesLL.Location = new System.Drawing.Point(397, 6);
            this.ConfigResponseTypesCancelChangesLL.Name = "ConfigResponseTypesCancelChangesLL";
            this.ConfigResponseTypesCancelChangesLL.Size = new System.Drawing.Size(46, 13);
            this.ConfigResponseTypesCancelChangesLL.TabIndex = 59;
            this.ConfigResponseTypesCancelChangesLL.TabStop = true;
            this.ConfigResponseTypesCancelChangesLL.Text = "Cancel";
            this.ConfigResponseTypesCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigResponseTypesCancelChangesLL_LinkClicked);
            // 
            // ConfigTaintConfigTab
            // 
            this.ConfigTaintConfigTab.Controls.Add(this.ConfigJSTaintConfigCancelChangesLL);
            this.ConfigTaintConfigTab.Controls.Add(this.ConfigJSTaintConfigApplyChangesLL);
            this.ConfigTaintConfigTab.Controls.Add(this.ConfigDefaultJSTaintConfigGrid);
            this.ConfigTaintConfigTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigTaintConfigTab.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigTaintConfigTab.Name = "ConfigTaintConfigTab";
            this.ConfigTaintConfigTab.Size = new System.Drawing.Size(873, 0);
            this.ConfigTaintConfigTab.TabIndex = 6;
            this.ConfigTaintConfigTab.Text = "Default JS Taint Config";
            this.ConfigTaintConfigTab.UseVisualStyleBackColor = true;
            // 
            // ConfigJSTaintConfigCancelChangesLL
            // 
            this.ConfigJSTaintConfigCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigJSTaintConfigCancelChangesLL.AutoSize = true;
            this.ConfigJSTaintConfigCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigJSTaintConfigCancelChangesLL.Location = new System.Drawing.Point(818, 10);
            this.ConfigJSTaintConfigCancelChangesLL.Name = "ConfigJSTaintConfigCancelChangesLL";
            this.ConfigJSTaintConfigCancelChangesLL.Size = new System.Drawing.Size(46, 13);
            this.ConfigJSTaintConfigCancelChangesLL.TabIndex = 68;
            this.ConfigJSTaintConfigCancelChangesLL.TabStop = true;
            this.ConfigJSTaintConfigCancelChangesLL.Text = "Cancel";
            this.ConfigJSTaintConfigCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigJSTaintConfigCancelChangesLL_LinkClicked);
            // 
            // ConfigJSTaintConfigApplyChangesLL
            // 
            this.ConfigJSTaintConfigApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigJSTaintConfigApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigJSTaintConfigApplyChangesLL.AutoSize = true;
            this.ConfigJSTaintConfigApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigJSTaintConfigApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigJSTaintConfigApplyChangesLL.Location = new System.Drawing.Point(770, 10);
            this.ConfigJSTaintConfigApplyChangesLL.Name = "ConfigJSTaintConfigApplyChangesLL";
            this.ConfigJSTaintConfigApplyChangesLL.Size = new System.Drawing.Size(36, 13);
            this.ConfigJSTaintConfigApplyChangesLL.TabIndex = 69;
            this.ConfigJSTaintConfigApplyChangesLL.TabStop = true;
            this.ConfigJSTaintConfigApplyChangesLL.Text = "Save";
            this.ConfigJSTaintConfigApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigJSTaintConfigApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigJSTaintConfigApplyChangesLL_LinkClicked);
            // 
            // ConfigDefaultJSTaintConfigGrid
            // 
            this.ConfigDefaultJSTaintConfigGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDefaultJSTaintConfigGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ConfigDefaultJSTaintConfigGrid.BackgroundColor = System.Drawing.Color.White;
            this.ConfigDefaultJSTaintConfigGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigDefaultJSTaintConfigGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConfigDefaultSourceObjectsColumn,
            this.ConfigDefaultSinkObjectsColumn,
            this.ConfigDefaultArgumentAssignedASourceMethodsColumn,
            this.ConfigDefaultArgumentAssignedToSinkMethodsColumn,
            this.ConfigDefaultSourceReturningMethodsColumn,
            this.ConfigDefaultSinkReturningMethodsColumn,
            this.ConfigDefaultArgumentReturningMethodsColumn});
            this.ConfigDefaultJSTaintConfigGrid.Location = new System.Drawing.Point(0, 30);
            this.ConfigDefaultJSTaintConfigGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigDefaultJSTaintConfigGrid.Name = "ConfigDefaultJSTaintConfigGrid";
            this.ConfigDefaultJSTaintConfigGrid.Size = new System.Drawing.Size(873, 612);
            this.ConfigDefaultJSTaintConfigGrid.TabIndex = 0;
            // 
            // ConfigDefaultSourceObjectsColumn
            // 
            this.ConfigDefaultSourceObjectsColumn.HeaderText = "Source Objects";
            this.ConfigDefaultSourceObjectsColumn.Name = "ConfigDefaultSourceObjectsColumn";
            // 
            // ConfigDefaultSinkObjectsColumn
            // 
            this.ConfigDefaultSinkObjectsColumn.HeaderText = "Sink Objects";
            this.ConfigDefaultSinkObjectsColumn.Name = "ConfigDefaultSinkObjectsColumn";
            // 
            // ConfigDefaultArgumentAssignedASourceMethodsColumn
            // 
            this.ConfigDefaultArgumentAssignedASourceMethodsColumn.HeaderText = "Argument Assigned A Source Methods";
            this.ConfigDefaultArgumentAssignedASourceMethodsColumn.Name = "ConfigDefaultArgumentAssignedASourceMethodsColumn";
            // 
            // ConfigDefaultArgumentAssignedToSinkMethodsColumn
            // 
            this.ConfigDefaultArgumentAssignedToSinkMethodsColumn.HeaderText = "Argument Assigned To Sink Methods";
            this.ConfigDefaultArgumentAssignedToSinkMethodsColumn.Name = "ConfigDefaultArgumentAssignedToSinkMethodsColumn";
            // 
            // ConfigDefaultSourceReturningMethodsColumn
            // 
            this.ConfigDefaultSourceReturningMethodsColumn.HeaderText = "Source Returning Methods";
            this.ConfigDefaultSourceReturningMethodsColumn.Name = "ConfigDefaultSourceReturningMethodsColumn";
            // 
            // ConfigDefaultSinkReturningMethodsColumn
            // 
            this.ConfigDefaultSinkReturningMethodsColumn.HeaderText = "Sink Returning Methods";
            this.ConfigDefaultSinkReturningMethodsColumn.Name = "ConfigDefaultSinkReturningMethodsColumn";
            // 
            // ConfigDefaultArgumentReturningMethodsColumn
            // 
            this.ConfigDefaultArgumentReturningMethodsColumn.HeaderText = "Argument Returning Methods";
            this.ConfigDefaultArgumentReturningMethodsColumn.Name = "ConfigDefaultArgumentReturningMethodsColumn";
            // 
            // ConfigScannerTab
            // 
            this.ConfigScannerTab.Controls.Add(this.ConfigCrawlerUserAgentTB);
            this.ConfigScannerTab.Controls.Add(this.label32);
            this.ConfigScannerTab.Controls.Add(this.ConfigScannerSettingsCancelChangesLL);
            this.ConfigScannerTab.Controls.Add(this.ConfigScannerSettingsApplyChangesLL);
            this.ConfigScannerTab.Controls.Add(this.ConfigCrawlerThreadMaxCountLbl);
            this.ConfigScannerTab.Controls.Add(this.label33);
            this.ConfigScannerTab.Controls.Add(this.ConfigCrawlerThreadMaxCountTB);
            this.ConfigScannerTab.Controls.Add(this.ConfigScannerThreadMaxCountLbl);
            this.ConfigScannerTab.Controls.Add(this.label31);
            this.ConfigScannerTab.Controls.Add(this.ConfigScannerThreadMaxCountTB);
            this.ConfigScannerTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigScannerTab.Name = "ConfigScannerTab";
            this.ConfigScannerTab.Size = new System.Drawing.Size(873, 0);
            this.ConfigScannerTab.TabIndex = 7;
            this.ConfigScannerTab.Text = "Scanner Settings";
            this.ConfigScannerTab.UseVisualStyleBackColor = true;
            // 
            // ConfigCrawlerUserAgentTB
            // 
            this.ConfigCrawlerUserAgentTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigCrawlerUserAgentTB.Location = new System.Drawing.Point(3, 185);
            this.ConfigCrawlerUserAgentTB.Multiline = true;
            this.ConfigCrawlerUserAgentTB.Name = "ConfigCrawlerUserAgentTB";
            this.ConfigCrawlerUserAgentTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigCrawlerUserAgentTB.Size = new System.Drawing.Size(867, 89);
            this.ConfigCrawlerUserAgentTB.TabIndex = 73;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 166);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(435, 13);
            this.label32.TabIndex = 72;
            this.label32.Text = "User-Agent Header value used by the Crawler (User-Agent header ignored for blank " +
                "value):";
            // 
            // ConfigScannerSettingsCancelChangesLL
            // 
            this.ConfigScannerSettingsCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScannerSettingsCancelChangesLL.AutoSize = true;
            this.ConfigScannerSettingsCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigScannerSettingsCancelChangesLL.Location = new System.Drawing.Point(814, 13);
            this.ConfigScannerSettingsCancelChangesLL.Name = "ConfigScannerSettingsCancelChangesLL";
            this.ConfigScannerSettingsCancelChangesLL.Size = new System.Drawing.Size(46, 13);
            this.ConfigScannerSettingsCancelChangesLL.TabIndex = 70;
            this.ConfigScannerSettingsCancelChangesLL.TabStop = true;
            this.ConfigScannerSettingsCancelChangesLL.Text = "Cancel";
            this.ConfigScannerSettingsCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigScannerSettingsCancelChangesLL_LinkClicked);
            // 
            // ConfigScannerSettingsApplyChangesLL
            // 
            this.ConfigScannerSettingsApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigScannerSettingsApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigScannerSettingsApplyChangesLL.AutoSize = true;
            this.ConfigScannerSettingsApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigScannerSettingsApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigScannerSettingsApplyChangesLL.Location = new System.Drawing.Point(766, 13);
            this.ConfigScannerSettingsApplyChangesLL.Name = "ConfigScannerSettingsApplyChangesLL";
            this.ConfigScannerSettingsApplyChangesLL.Size = new System.Drawing.Size(38, 13);
            this.ConfigScannerSettingsApplyChangesLL.TabIndex = 71;
            this.ConfigScannerSettingsApplyChangesLL.TabStop = true;
            this.ConfigScannerSettingsApplyChangesLL.Text = "Apply";
            this.ConfigScannerSettingsApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigScannerSettingsApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigScannerSettingsApplyChangesLL_LinkClicked);
            // 
            // ConfigCrawlerThreadMaxCountLbl
            // 
            this.ConfigCrawlerThreadMaxCountLbl.AutoSize = true;
            this.ConfigCrawlerThreadMaxCountLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigCrawlerThreadMaxCountLbl.Location = new System.Drawing.Point(231, 91);
            this.ConfigCrawlerThreadMaxCountLbl.Name = "ConfigCrawlerThreadMaxCountLbl";
            this.ConfigCrawlerThreadMaxCountLbl.Size = new System.Drawing.Size(14, 13);
            this.ConfigCrawlerThreadMaxCountLbl.TabIndex = 5;
            this.ConfigCrawlerThreadMaxCountLbl.Text = "5";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 90);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(216, 13);
            this.label33.TabIndex = 4;
            this.label33.Text = "Number of Parallel Crawler Threads Allowed:";
            // 
            // ConfigCrawlerThreadMaxCountTB
            // 
            this.ConfigCrawlerThreadMaxCountTB.LargeChange = 10;
            this.ConfigCrawlerThreadMaxCountTB.Location = new System.Drawing.Point(267, 78);
            this.ConfigCrawlerThreadMaxCountTB.Minimum = 1;
            this.ConfigCrawlerThreadMaxCountTB.Name = "ConfigCrawlerThreadMaxCountTB";
            this.ConfigCrawlerThreadMaxCountTB.Size = new System.Drawing.Size(104, 45);
            this.ConfigCrawlerThreadMaxCountTB.TabIndex = 3;
            this.ConfigCrawlerThreadMaxCountTB.Value = 5;
            this.ConfigCrawlerThreadMaxCountTB.Scroll += new System.EventHandler(this.ConfigCrawlerThreadMaxCountTB_Scroll);
            // 
            // ConfigScannerThreadMaxCountLbl
            // 
            this.ConfigScannerThreadMaxCountLbl.AutoSize = true;
            this.ConfigScannerThreadMaxCountLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigScannerThreadMaxCountLbl.Location = new System.Drawing.Point(231, 30);
            this.ConfigScannerThreadMaxCountLbl.Name = "ConfigScannerThreadMaxCountLbl";
            this.ConfigScannerThreadMaxCountLbl.Size = new System.Drawing.Size(14, 13);
            this.ConfigScannerThreadMaxCountLbl.TabIndex = 2;
            this.ConfigScannerThreadMaxCountLbl.Text = "3";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 29);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(221, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "Number of Parallel Scanner Threads Allowed:";
            // 
            // ConfigScannerThreadMaxCountTB
            // 
            this.ConfigScannerThreadMaxCountTB.LargeChange = 10;
            this.ConfigScannerThreadMaxCountTB.Location = new System.Drawing.Point(267, 17);
            this.ConfigScannerThreadMaxCountTB.Minimum = 1;
            this.ConfigScannerThreadMaxCountTB.Name = "ConfigScannerThreadMaxCountTB";
            this.ConfigScannerThreadMaxCountTB.Size = new System.Drawing.Size(104, 45);
            this.ConfigScannerThreadMaxCountTB.TabIndex = 0;
            this.ConfigScannerThreadMaxCountTB.Value = 3;
            this.ConfigScannerThreadMaxCountTB.Scroll += new System.EventHandler(this.ConfigScannerThreadMaxCountTB_Scroll);
            // 
            // ConfigPassiveAnalysisTab
            // 
            this.ConfigPassiveAnalysisTab.Controls.Add(this.ConfigPassiveAnalysisOnProbeTrafficCB);
            this.ConfigPassiveAnalysisTab.Controls.Add(this.ConfigPassiveAnalysisOnScanTrafficCB);
            this.ConfigPassiveAnalysisTab.Controls.Add(this.ConfigPassiveAnalysisOnTestTrafficCB);
            this.ConfigPassiveAnalysisTab.Controls.Add(this.ConfigPassiveAnalysisOnShellTrafficCB);
            this.ConfigPassiveAnalysisTab.Controls.Add(this.ConfigPassiveAnalysisOnProxyTrafficCB);
            this.ConfigPassiveAnalysisTab.Controls.Add(this.label34);
            this.ConfigPassiveAnalysisTab.Controls.Add(this.ConfigPassiveAnalysisSettingsCancelChangesLL);
            this.ConfigPassiveAnalysisTab.Controls.Add(this.ConfigPassiveAnalysisSettingsApplyChangesLL);
            this.ConfigPassiveAnalysisTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigPassiveAnalysisTab.Name = "ConfigPassiveAnalysisTab";
            this.ConfigPassiveAnalysisTab.Size = new System.Drawing.Size(873, 0);
            this.ConfigPassiveAnalysisTab.TabIndex = 8;
            this.ConfigPassiveAnalysisTab.Text = "Passive Analysis";
            this.ConfigPassiveAnalysisTab.UseVisualStyleBackColor = true;
            // 
            // ConfigPassiveAnalysisOnProbeTrafficCB
            // 
            this.ConfigPassiveAnalysisOnProbeTrafficCB.AutoSize = true;
            this.ConfigPassiveAnalysisOnProbeTrafficCB.Checked = true;
            this.ConfigPassiveAnalysisOnProbeTrafficCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigPassiveAnalysisOnProbeTrafficCB.Location = new System.Drawing.Point(54, 159);
            this.ConfigPassiveAnalysisOnProbeTrafficCB.Name = "ConfigPassiveAnalysisOnProbeTrafficCB";
            this.ConfigPassiveAnalysisOnProbeTrafficCB.Size = new System.Drawing.Size(61, 17);
            this.ConfigPassiveAnalysisOnProbeTrafficCB.TabIndex = 79;
            this.ConfigPassiveAnalysisOnProbeTrafficCB.Text = "Crawler";
            this.ConfigPassiveAnalysisOnProbeTrafficCB.UseVisualStyleBackColor = true;
            // 
            // ConfigPassiveAnalysisOnScanTrafficCB
            // 
            this.ConfigPassiveAnalysisOnScanTrafficCB.AutoSize = true;
            this.ConfigPassiveAnalysisOnScanTrafficCB.Location = new System.Drawing.Point(54, 136);
            this.ConfigPassiveAnalysisOnScanTrafficCB.Name = "ConfigPassiveAnalysisOnScanTrafficCB";
            this.ConfigPassiveAnalysisOnScanTrafficCB.Size = new System.Drawing.Size(125, 17);
            this.ConfigPassiveAnalysisOnScanTrafficCB.TabIndex = 78;
            this.ConfigPassiveAnalysisOnScanTrafficCB.Text = "Automated Scanning";
            this.ConfigPassiveAnalysisOnScanTrafficCB.UseVisualStyleBackColor = true;
            // 
            // ConfigPassiveAnalysisOnTestTrafficCB
            // 
            this.ConfigPassiveAnalysisOnTestTrafficCB.AutoSize = true;
            this.ConfigPassiveAnalysisOnTestTrafficCB.Checked = true;
            this.ConfigPassiveAnalysisOnTestTrafficCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigPassiveAnalysisOnTestTrafficCB.Location = new System.Drawing.Point(54, 113);
            this.ConfigPassiveAnalysisOnTestTrafficCB.Name = "ConfigPassiveAnalysisOnTestTrafficCB";
            this.ConfigPassiveAnalysisOnTestTrafficCB.Size = new System.Drawing.Size(99, 17);
            this.ConfigPassiveAnalysisOnTestTrafficCB.TabIndex = 77;
            this.ConfigPassiveAnalysisOnTestTrafficCB.Text = "Manual Testing";
            this.ConfigPassiveAnalysisOnTestTrafficCB.UseVisualStyleBackColor = true;
            // 
            // ConfigPassiveAnalysisOnShellTrafficCB
            // 
            this.ConfigPassiveAnalysisOnShellTrafficCB.AutoSize = true;
            this.ConfigPassiveAnalysisOnShellTrafficCB.Checked = true;
            this.ConfigPassiveAnalysisOnShellTrafficCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigPassiveAnalysisOnShellTrafficCB.Location = new System.Drawing.Point(54, 90);
            this.ConfigPassiveAnalysisOnShellTrafficCB.Name = "ConfigPassiveAnalysisOnShellTrafficCB";
            this.ConfigPassiveAnalysisOnShellTrafficCB.Size = new System.Drawing.Size(93, 17);
            this.ConfigPassiveAnalysisOnShellTrafficCB.TabIndex = 76;
            this.ConfigPassiveAnalysisOnShellTrafficCB.Text = "Scripting Shell";
            this.ConfigPassiveAnalysisOnShellTrafficCB.UseVisualStyleBackColor = true;
            // 
            // ConfigPassiveAnalysisOnProxyTrafficCB
            // 
            this.ConfigPassiveAnalysisOnProxyTrafficCB.AutoSize = true;
            this.ConfigPassiveAnalysisOnProxyTrafficCB.Checked = true;
            this.ConfigPassiveAnalysisOnProxyTrafficCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigPassiveAnalysisOnProxyTrafficCB.Location = new System.Drawing.Point(54, 67);
            this.ConfigPassiveAnalysisOnProxyTrafficCB.Name = "ConfigPassiveAnalysisOnProxyTrafficCB";
            this.ConfigPassiveAnalysisOnProxyTrafficCB.Size = new System.Drawing.Size(52, 17);
            this.ConfigPassiveAnalysisOnProxyTrafficCB.TabIndex = 75;
            this.ConfigPassiveAnalysisOnProxyTrafficCB.Text = "Proxy";
            this.ConfigPassiveAnalysisOnProxyTrafficCB.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(19, 35);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(307, 13);
            this.label34.TabIndex = 74;
            this.label34.Text = "Run Passive Plugins on HTTP traffic from the selected sources:";
            // 
            // ConfigPassiveAnalysisSettingsCancelChangesLL
            // 
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.AutoSize = true;
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.Location = new System.Drawing.Point(815, 15);
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.Name = "ConfigPassiveAnalysisSettingsCancelChangesLL";
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.Size = new System.Drawing.Size(46, 13);
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.TabIndex = 72;
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.TabStop = true;
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.Text = "Cancel";
            this.ConfigPassiveAnalysisSettingsCancelChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigPassiveAnalysisSettingsCancelChangesLL_LinkClicked);
            // 
            // ConfigPassiveAnalysisSettingsApplyChangesLL
            // 
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.ActiveLinkColor = System.Drawing.Color.Red;
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.AutoSize = true;
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.Location = new System.Drawing.Point(767, 15);
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.Name = "ConfigPassiveAnalysisSettingsApplyChangesLL";
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.Size = new System.Drawing.Size(38, 13);
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.TabIndex = 73;
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.TabStop = true;
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.Text = "Apply";
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ConfigPassiveAnalysisSettingsApplyChangesLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigPassiveAnalysisSettingsApplyChangesLL_LinkClicked);
            // 
            // TopMenu
            // 
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(884, 24);
            this.TopMenu.TabIndex = 1;
            this.TopMenu.Text = "TopMenu";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProjectToolStripMenuItem,
            this.importToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // OpenProjectToolStripMenuItem
            // 
            this.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem";
            this.OpenProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.OpenProjectToolStripMenuItem.Text = "Open Project";
            this.OpenProjectToolStripMenuItem.Click += new System.EventHandler(this.OpenProjectToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportBurpLogToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // ImportBurpLogToolStripMenuItem
            // 
            this.ImportBurpLogToolStripMenuItem.Name = "ImportBurpLogToolStripMenuItem";
            this.ImportBurpLogToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.ImportBurpLogToolStripMenuItem.Text = "Burp Log";
            this.ImportBurpLogToolStripMenuItem.Click += new System.EventHandler(this.ImportBurpLogToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EncodeDecodeToolStripMenuItem,
            this.DiffTextToolStripMenuItem,
            this.PluginEditorToolStripMenuItem,
            this.RenderHTMLToolStripMenuItem});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.reportToolStripMenuItem.Text = "Tools";
            // 
            // EncodeDecodeToolStripMenuItem
            // 
            this.EncodeDecodeToolStripMenuItem.Name = "EncodeDecodeToolStripMenuItem";
            this.EncodeDecodeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.EncodeDecodeToolStripMenuItem.Text = "Encode/Decode";
            this.EncodeDecodeToolStripMenuItem.Click += new System.EventHandler(this.EncodeDecodeToolStripMenuItem_Click);
            // 
            // DiffTextToolStripMenuItem
            // 
            this.DiffTextToolStripMenuItem.Name = "DiffTextToolStripMenuItem";
            this.DiffTextToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.DiffTextToolStripMenuItem.Text = "Diff Text";
            this.DiffTextToolStripMenuItem.Click += new System.EventHandler(this.DiffTextToolStripMenuItem_Click);
            // 
            // PluginEditorToolStripMenuItem
            // 
            this.PluginEditorToolStripMenuItem.Name = "PluginEditorToolStripMenuItem";
            this.PluginEditorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.PluginEditorToolStripMenuItem.Text = "Script/Plugin Editor";
            this.PluginEditorToolStripMenuItem.Click += new System.EventHandler(this.PluginEditorToolStripMenuItem_Click);
            // 
            // RenderHTMLToolStripMenuItem
            // 
            this.RenderHTMLToolStripMenuItem.Name = "RenderHTMLToolStripMenuItem";
            this.RenderHTMLToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.RenderHTMLToolStripMenuItem.Text = "Render HTML";
            this.RenderHTMLToolStripMenuItem.Click += new System.EventHandler(this.RenderHTMLToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // ProjectFileOpenDialog
            // 
            this.ProjectFileOpenDialog.Filter = "Iron Project Files|*.iron";
            this.ProjectFileOpenDialog.Title = "Open Iron Project File";
            // 
            // BurpLogOpenDialog
            // 
            this.BurpLogOpenDialog.Title = "Select the Burp Log file to Import";
            // 
            // ConfigViewHideLL
            // 
            this.ConfigViewHideLL.AutoSize = true;
            this.ConfigViewHideLL.Location = new System.Drawing.Point(3, 5);
            this.ConfigViewHideLL.Name = "ConfigViewHideLL";
            this.ConfigViewHideLL.Size = new System.Drawing.Size(67, 13);
            this.ConfigViewHideLL.TabIndex = 13;
            this.ConfigViewHideLL.TabStop = true;
            this.ConfigViewHideLL.Text = "Show Config";
            this.ConfigViewHideLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ConfigViewHideLL_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ConfigViewHideLL);
            this.panel1.Location = new System.Drawing.Point(812, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(72, 23);
            this.panel1.TabIndex = 14;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ConfigPanel);
            this.Controls.Add(this.split_main);
            this.Controls.Add(this.TopMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.TopMenu;
            this.Name = "Main";
            this.Text = "IronWASP Beta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.split_main.Panel1.ResumeLayout(false);
            this.split_main.Panel2.ResumeLayout(false);
            this.split_main.ResumeLayout(false);
            this.IronTreeMenuStrip.ResumeLayout(false);
            this.main_tab.ResumeLayout(false);
            this.mt_console.ResumeLayout(false);
            this.mt_console.PerformLayout();
            this.mt_auto.ResumeLayout(false);
            this.ASMainTabs.ResumeLayout(false);
            this.ASConfigureTab.ResumeLayout(false);
            this.ASConfigureSplit.Panel1.ResumeLayout(false);
            this.ASConfigureSplit.Panel1.PerformLayout();
            this.ASConfigureSplit.Panel2.ResumeLayout(false);
            this.ASConfigureSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ASScanPluginsGrid)).EndInit();
            this.ASRequestTabs.ResumeLayout(false);
            this.ASRequestFullTab.ResumeLayout(false);
            this.ASRequestFullTab.PerformLayout();
            this.ASRequestScanFullTabs.ResumeLayout(false);
            this.tabPage20.ResumeLayout(false);
            this.tabPage20.PerformLayout();
            this.tabPage21.ResumeLayout(false);
            this.tabPage21.PerformLayout();
            this.ASRequestURLTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanURLGrid)).EndInit();
            this.ASRequestQueryTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanQueryGrid)).EndInit();
            this.ASRequestBodyTab.ResumeLayout(false);
            this.ASRequestBodyTabSplit.Panel1.ResumeLayout(false);
            this.ASRequestBodyTabSplit.Panel2.ResumeLayout(false);
            this.ASRequestBodyTabSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConfigureScanRequestFormatPluginsGrid)).EndInit();
            this.ConfigureScanRequestFormatPluginsMenu.ResumeLayout(false);
            this.ASRequestScanBodyTabs.ResumeLayout(false);
            this.ASRequestScanBodyGridTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConfigureScanRequestBodyGrid)).EndInit();
            this.ASRequestScanBodyXMLTab.ResumeLayout(false);
            this.ASRequestScanBodyXMLTab.PerformLayout();
            this.ASRequestCookieTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanCookieGrid)).EndInit();
            this.ASRequestHeadersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ASRequestScanHeadersGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASQueueGrid)).EndInit();
            this.ScanQueueMenu.ResumeLayout(false);
            this.ASTraceTab.ResumeLayout(false);
            this.ScanTraceBaseSplit.Panel1.ResumeLayout(false);
            this.ScanTraceBaseSplit.Panel2.ResumeLayout(false);
            this.ScanTraceBaseSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanTraceGrid)).EndInit();
            this.mt_manual.ResumeLayout(false);
            this.MTTabs.ResumeLayout(false);
            this.MTTestTP.ResumeLayout(false);
            this.MTBaseSplit.Panel1.ResumeLayout(false);
            this.MTBaseSplit.Panel1.PerformLayout();
            this.MTBaseSplit.Panel2.ResumeLayout(false);
            this.MTBaseSplit.ResumeLayout(false);
            this.MTReqResTabs.ResumeLayout(false);
            this.MTRequestTab.ResumeLayout(false);
            this.MTRequestTabs.ResumeLayout(false);
            this.MTRequestHeadersTP.ResumeLayout(false);
            this.MTRequestHeadersTP.PerformLayout();
            this.MTRequestBodyTP.ResumeLayout(false);
            this.MTRequestBodyTP.PerformLayout();
            this.MTRequestParametersTP.ResumeLayout(false);
            this.MTRequestParametersTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersQueryGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersBodyGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersCookieGrid)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestParametersHeadersGrid)).EndInit();
            this.MTRequestFormatPluginsTP.ResumeLayout(false);
            this.MTRequestFormatSplit.Panel1.ResumeLayout(false);
            this.MTRequestFormatSplit.Panel2.ResumeLayout(false);
            this.MTRequestFormatSplit.Panel2.PerformLayout();
            this.MTRequestFormatSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MTRequestFormatPluginsGrid)).EndInit();
            this.MTRequestFormatPluginsMenu.ResumeLayout(false);
            this.MTResponseTab.ResumeLayout(false);
            this.MTResponseTabs.ResumeLayout(false);
            this.MTResponseHeadersTP.ResumeLayout(false);
            this.MTResponseHeadersTP.PerformLayout();
            this.MTResponseBodyTP.ResumeLayout(false);
            this.MTResponseBodyTP.PerformLayout();
            this.MTResponseReflectionTP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TestGroupLogGrid)).EndInit();
            this.LogMenu.ResumeLayout(false);
            this.MTScriptingTP.ResumeLayout(false);
            this.ScriptingShellSplit.Panel1.ResumeLayout(false);
            this.ScriptingShellSplit.Panel1.PerformLayout();
            this.ScriptingShellSplit.Panel2.ResumeLayout(false);
            this.ScriptingShellSplit.ResumeLayout(false);
            this.ScriptingShellTabs.ResumeLayout(false);
            this.InteractiveShellTP.ResumeLayout(false);
            this.InteractiveShellTP.PerformLayout();
            this.MultiLineShellTP.ResumeLayout(false);
            this.ScriptedSendTP.ResumeLayout(false);
            this.ScriptedSendTP.PerformLayout();
            this.ScriptingShellAPISplit.Panel1.ResumeLayout(false);
            this.ScriptingShellAPISplit.Panel2.ResumeLayout(false);
            this.ScriptingShellAPISplit.ResumeLayout(false);
            this.ScriptingShellAPITreeTabs.ResumeLayout(false);
            this.ScriptingShellAPITreePythonTab.ResumeLayout(false);
            this.ScriptingShellAPITreeRubyTab.ResumeLayout(false);
            this.MTJavaScriptTaintTP.ResumeLayout(false);
            this.MTJavaScriptTaintTP.PerformLayout();
            this.JSTaintConfigPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.JSTaintConfigGrid)).EndInit();
            this.JSTaintTabs.ResumeLayout(false);
            this.JSTaintInputTab.ResumeLayout(false);
            this.JSTaintInputTab.PerformLayout();
            this.JSTaintResultTab.ResumeLayout(false);
            this.JSTaintResultSplit.Panel1.ResumeLayout(false);
            this.JSTaintResultSplit.Panel2.ResumeLayout(false);
            this.JSTaintResultSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.JSTaintResultGrid)).EndInit();
            this.JSTainTraceEditMenu.ResumeLayout(false);
            this.mt_proxy.ResumeLayout(false);
            this.mt_proxy.PerformLayout();
            this.ProxyInterceptTabs.ResumeLayout(false);
            this.ProxyInterceptRequestTab.ResumeLayout(false);
            this.ProxyInterceptRequestTabs.ResumeLayout(false);
            this.ProxyInterceptRequestHeadersTab.ResumeLayout(false);
            this.ProxyInterceptRequestHeadersTab.PerformLayout();
            this.ProxyInterceptRequestBodyTab.ResumeLayout(false);
            this.ProxyInterceptRequestBodyTab.PerformLayout();
            this.ProxyInterceptRequestParametersTab.ResumeLayout(false);
            this.ProxyRequestParametersTabs.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersQueryGrid)).EndInit();
            this.tabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersBodyGrid)).EndInit();
            this.tabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersCookieGrid)).EndInit();
            this.tabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestParametersHeadersGrid)).EndInit();
            this.ProxyInterceptRequestFormatPluginsTab.ResumeLayout(false);
            this.ProxyRequestFormatSplit.Panel1.ResumeLayout(false);
            this.ProxyRequestFormatSplit.Panel2.ResumeLayout(false);
            this.ProxyRequestFormatSplit.Panel2.PerformLayout();
            this.ProxyRequestFormatSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProxyRequestFormatPluginsGrid)).EndInit();
            this.ProxyRequestFormatPluginsMenu.ResumeLayout(false);
            this.ProxyInterceptResponseTab.ResumeLayout(false);
            this.ProxyInterceptResponseTabs.ResumeLayout(false);
            this.ProxyInterceptResponseHeadersTab.ResumeLayout(false);
            this.ProxyInterceptResponseHeadersTab.PerformLayout();
            this.ProxyInterceptResponseBodyTab.ResumeLayout(false);
            this.ProxyInterceptResponseBodyTab.PerformLayout();
            this.ProxyInterceptResponseFormatPluginsTab.ResumeLayout(false);
            this.ProxyResponseFormatSplit.Panel1.ResumeLayout(false);
            this.ProxyResponseFormatSplit.Panel2.ResumeLayout(false);
            this.ProxyResponseFormatSplit.Panel2.PerformLayout();
            this.ProxyResponseFormatSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProxyResponseFormatPluginsGrid)).EndInit();
            this.ProxyResponseFormatPluginsMenu.ResumeLayout(false);
            this.mt_logs.ResumeLayout(false);
            this.LogBaseSplit.Panel1.ResumeLayout(false);
            this.LogBaseSplit.Panel1.PerformLayout();
            this.LogBaseSplit.Panel2.ResumeLayout(false);
            this.LogBaseSplit.ResumeLayout(false);
            this.LogDisplayTabs.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.LogRequestTabs.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.tabPage16.PerformLayout();
            this.tabPage22.ResumeLayout(false);
            this.tabPage22.PerformLayout();
            this.tabPage23.ResumeLayout(false);
            this.tabControl5.ResumeLayout(false);
            this.tabPage24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersQueryGrid)).EndInit();
            this.tabPage25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersBodyGrid)).EndInit();
            this.tabPage26.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersCookieGrid)).EndInit();
            this.tabPage27.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestParametersHeadersGrid)).EndInit();
            this.tabPage28.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogRequestFormatPluginsGrid)).EndInit();
            this.tabPage29.ResumeLayout(false);
            this.LogResponseTabs.ResumeLayout(false);
            this.tabPage30.ResumeLayout(false);
            this.tabPage30.PerformLayout();
            this.tabPage31.ResumeLayout(false);
            this.tabPage31.PerformLayout();
            this.tabPage39.ResumeLayout(false);
            this.tabPage32.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogResponseFormatPluginsGrid)).EndInit();
            this.LogTabs.ResumeLayout(false);
            this.ProxyLogTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProxyLogGrid)).EndInit();
            this.ScanLogTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanLogGrid)).EndInit();
            this.TestLogTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TestLogGrid)).EndInit();
            this.ShellLogTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShellLogGrid)).EndInit();
            this.ProbeLogTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProbeLogGrid)).EndInit();
            this.SiteMapLogTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SiteMapLogGrid)).EndInit();
            this.mt_results.ResumeLayout(false);
            this.ResultsTabMainSplit.Panel1.ResumeLayout(false);
            this.ResultsTabMainSplit.Panel2.ResumeLayout(false);
            this.ResultsTabMainSplit.ResumeLayout(false);
            this.ResultsTopSplit.Panel1.ResumeLayout(false);
            this.ResultsTopSplit.Panel2.ResumeLayout(false);
            this.ResultsTopSplit.ResumeLayout(false);
            this.ResultsTriggersMainSplit.Panel1.ResumeLayout(false);
            this.ResultsTriggersMainSplit.Panel2.ResumeLayout(false);
            this.ResultsTriggersMainSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTriggersGrid)).EndInit();
            this.ResultsTriggersSplit.Panel1.ResumeLayout(false);
            this.ResultsTriggersSplit.Panel1.PerformLayout();
            this.ResultsTriggersSplit.Panel2.ResumeLayout(false);
            this.ResultsTriggersSplit.Panel2.PerformLayout();
            this.ResultsTriggersSplit.ResumeLayout(false);
            this.ResultsDisplayTabs.ResumeLayout(false);
            this.ResultsRequestTab.ResumeLayout(false);
            this.ResultsRequestTab.PerformLayout();
            this.ResultsResponseTab.ResumeLayout(false);
            this.ResultsResponseTab.PerformLayout();
            this.mt_plugins.ResumeLayout(false);
            this.PluginsMainSplit.Panel1.ResumeLayout(false);
            this.PluginsMainSplit.Panel2.ResumeLayout(false);
            this.PluginsMainSplit.ResumeLayout(false);
            this.PluginTreeMenu.ResumeLayout(false);
            this.PluginEditorSplit.Panel1.ResumeLayout(false);
            this.PluginEditorSplit.Panel2.ResumeLayout(false);
            this.PluginEditorSplit.ResumeLayout(false);
            this.PluginsCentreSplit.Panel1.ResumeLayout(false);
            this.PluginsCentreSplit.Panel2.ResumeLayout(false);
            this.PluginsCentreSplit.ResumeLayout(false);
            this.PluginEditorAPISplit.Panel1.ResumeLayout(false);
            this.PluginEditorAPISplit.Panel2.ResumeLayout(false);
            this.PluginEditorAPISplit.ResumeLayout(false);
            this.PluginEditorAPITreeTabs.ResumeLayout(false);
            this.PluginEditorPythonAPITreeTab.ResumeLayout(false);
            this.PluginEditorRubyAPITreeTab.ResumeLayout(false);
            this.mt_trace.ResumeLayout(false);
            this.TraceBaseSplit.Panel1.ResumeLayout(false);
            this.TraceBaseSplit.Panel2.ResumeLayout(false);
            this.TraceBaseSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TraceGrid)).EndInit();
            this.ConfigPanel.ResumeLayout(false);
            this.ConfigPanelTabs.ResumeLayout(false);
            this.ConfigIronProxyTab.ResumeLayout(false);
            this.ConfigIronProxyTab.PerformLayout();
            this.ConfigInterceptRulesTab.ResumeLayout(false);
            this.ConfigInterceptRulesTab.PerformLayout();
            this.ConfigRuleKeywordInResponseGB.ResumeLayout(false);
            this.ConfigRuleKeywordInResponseGB.PerformLayout();
            this.ConfigRuleKeywordInRequestGB.ResumeLayout(false);
            this.ConfigRuleKeywordInRequestGB.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ConfigDisplayRulesTab.ResumeLayout(false);
            this.ConfigDisplayRulesTab.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ConfigScriptingTab.ResumeLayout(false);
            this.ConfigScriptBaseSplit.Panel1.ResumeLayout(false);
            this.ConfigScriptBaseSplit.Panel1.PerformLayout();
            this.ConfigScriptBaseSplit.Panel2.ResumeLayout(false);
            this.ConfigScriptBaseSplit.Panel2.PerformLayout();
            this.ConfigScriptBaseSplit.ResumeLayout(false);
            this.ConfigScriptPathSplit.Panel1.ResumeLayout(false);
            this.ConfigScriptPathSplit.Panel1.PerformLayout();
            this.ConfigScriptPathSplit.Panel2.ResumeLayout(false);
            this.ConfigScriptPathSplit.Panel2.PerformLayout();
            this.ConfigScriptPathSplit.ResumeLayout(false);
            this.ConfigScriptCommandSplit.Panel1.ResumeLayout(false);
            this.ConfigScriptCommandSplit.Panel1.PerformLayout();
            this.ConfigScriptCommandSplit.Panel2.ResumeLayout(false);
            this.ConfigScriptCommandSplit.Panel2.PerformLayout();
            this.ConfigScriptCommandSplit.ResumeLayout(false);
            this.ConfigHTTPAPITab.ResumeLayout(false);
            this.ConfigHTTPAPIBaseSplit.Panel1.ResumeLayout(false);
            this.ConfigHTTPAPIBaseSplit.Panel1.PerformLayout();
            this.ConfigHTTPAPIBaseSplit.Panel2.ResumeLayout(false);
            this.ConfigHTTPAPIBaseSplit.Panel2.PerformLayout();
            this.ConfigHTTPAPIBaseSplit.ResumeLayout(false);
            this.ConfigTaintConfigTab.ResumeLayout(false);
            this.ConfigTaintConfigTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDefaultJSTaintConfigGrid)).EndInit();
            this.ConfigScannerTab.ResumeLayout(false);
            this.ConfigScannerTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigCrawlerThreadMaxCountTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigScannerThreadMaxCountTB)).EndInit();
            this.ConfigPassiveAnalysisTab.ResumeLayout(false);
            this.ConfigPassiveAnalysisTab.PerformLayout();
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer split_main;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.TabPage mt_proxy;
        private System.Windows.Forms.TabPage mt_manual;
        private System.Windows.Forms.TabPage mt_auto;
        private System.Windows.Forms.TabPage ProxyInterceptRequestTab;
        private System.Windows.Forms.TabPage ProxyInterceptResponseTab;
        private System.Windows.Forms.CheckBox InterceptResponseCB;
        private System.Windows.Forms.CheckBox InterceptRequestCB;
        private System.Windows.Forms.SplitContainer MTBaseSplit;
        private System.Windows.Forms.TabControl MTRequestTabs;
        private System.Windows.Forms.TabPage MTRequestHeadersTP;
        private System.Windows.Forms.TabPage MTRequestParametersTP;
        private System.Windows.Forms.TabControl MTResponseTabs;
        private System.Windows.Forms.TabPage MTResponseHeadersTP;
        private System.Windows.Forms.TabPage MTScriptingTP;
        private System.Windows.Forms.TabControl ScriptingShellTabs;
        private System.Windows.Forms.TabPage InteractiveShellTP;
        private System.Windows.Forms.TabPage MultiLineShellTP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer ScriptingShellSplit;
        private System.Windows.Forms.ContextMenuStrip LogMenu;
        private System.Windows.Forms.ToolStripMenuItem SelectForManualTestingToolStripMenuItem;
        internal IronDataView.IronDataView MTResponseHeadersIDV;
        internal System.Windows.Forms.DataGridView ProxyLogGrid;
        private System.Windows.Forms.Button ProxyDropBtn;
        private System.Windows.Forms.Button ProxySendBtn;
        internal System.Windows.Forms.DataGridView TestLogGrid;
        internal System.Windows.Forms.Button MTSendBtn;
        internal System.Windows.Forms.Label TestIDLbl;
        private System.Windows.Forms.TabPage mt_plugins;
        private System.Windows.Forms.SplitContainer ScriptingShellAPISplit;
        private System.Windows.Forms.RichTextBox ShellAPIDetailsRTB;
        public System.Windows.Forms.TreeView IronTree;
        internal System.Windows.Forms.DataGridView ShellLogGrid;
        internal System.Windows.Forms.CheckBox MTIsSSLCB;
        internal IronDataView.IronDataView ProxyResponseHeadersIDV;
        internal System.Windows.Forms.TabControl ProxyInterceptTabs;
        internal System.Windows.Forms.TreeView ScriptingShellPythonAPITree;
        internal System.Windows.Forms.TextBox InteractiveShellOut;
        internal System.Windows.Forms.TextBox InteractiveShellPromptBox;
        internal System.Windows.Forms.TextBox InteractiveShellIn;
        internal System.Windows.Forms.Button InteractiveShellCtrlCBtn;
        private System.Windows.Forms.SplitContainer PluginsMainSplit;
        internal System.Windows.Forms.TreeView PluginTree;
        private System.Windows.Forms.SplitContainer PluginEditorSplit;
        private System.Windows.Forms.SplitContainer PluginEditorAPISplit;
        internal System.Windows.Forms.TreeView PluginEditorPythonAPITree;
        private System.Windows.Forms.RichTextBox PluginEditorAPIDetailsRTB;
        private System.Windows.Forms.TabPage ASConfigureTab;
        internal System.Windows.Forms.DataGridView ScanLogGrid;
        private System.Windows.Forms.SplitContainer ASConfigureSplit;
        private System.Windows.Forms.TabControl ScriptingShellAPITreeTabs;
        private System.Windows.Forms.TabPage ScriptingShellAPITreePythonTab;
        private System.Windows.Forms.TabPage ScriptingShellAPITreeRubyTab;
        internal System.Windows.Forms.TreeView ScriptingShellRubyAPITree;
        private System.Windows.Forms.TabControl PluginEditorAPITreeTabs;
        private System.Windows.Forms.TabPage PluginEditorPythonAPITreeTab;
        private System.Windows.Forms.TabPage PluginEditorRubyAPITreeTab;
        internal System.Windows.Forms.TreeView PluginEditorRubyAPITree;
        internal System.Windows.Forms.Button MTScriptedSendBtn;
        internal System.Windows.Forms.Button MTStoredRequestBtn;
        internal IronDataView.IronDataView MTRequestHeadersIDV;
        private System.Windows.Forms.TabPage ASRequestURLTab;
        private System.Windows.Forms.TabPage ASRequestQueryTab;
        private System.Windows.Forms.TabPage ASRequestBodyTab;
        private System.Windows.Forms.TabPage ASRequestCookieTab;
        private System.Windows.Forms.TabPage ASRequestHeadersTab;
        private System.Windows.Forms.TabPage ASRequestFullTab;
        private System.Windows.Forms.SplitContainer ASRequestBodyTabSplit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer PluginsCentreSplit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem SelectForAutomatedScanningToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASQueueGridScanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASQueueGridStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASQueueGridMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASQueueGridURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestBodyDataFormatColumn;
        internal System.Windows.Forms.DataGridView ASQueueGrid;
        private System.Windows.Forms.TabPage ASRequestScanBodyGridTab;
        private System.Windows.Forms.TabPage ASRequestScanBodyXMLTab;
        private System.Windows.Forms.TabPage mt_results;
        internal System.Windows.Forms.TabControl ResultsDisplayTabs;
        private System.Windows.Forms.TabPage ResultsRequestTab;
        internal IronDataView.IronDataView ResultsRequestIDV;
        private System.Windows.Forms.TabPage ResultsResponseTab;
        internal IronDataView.IronDataView ResultsResponseIDV;
        private System.Windows.Forms.SplitContainer ResultsTriggersMainSplit;
        private System.Windows.Forms.SplitContainer ResultsTriggersSplit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.RichTextBox ResultsDisplayRTB;
        internal System.Windows.Forms.DataGridView ResultsTriggersGrid;
        internal System.Windows.Forms.TextBox ResultsRequestTriggerTB;
        internal System.Windows.Forms.TextBox ResultsResponseTriggerTB;
        internal System.Windows.Forms.TabControl main_tab;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultsTriggerGridNumberColumn;
        private System.Windows.Forms.Panel ConfigPanel;
        private System.Windows.Forms.TabControl ConfigPanelTabs;
        private System.Windows.Forms.TabPage ConfigIronProxyTab;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox ConfigProxyListenPortTB;
        internal System.Windows.Forms.CheckBox ConfigLoopBackOnlyCB;
        internal System.Windows.Forms.Button ConfigProxyRunBtn;
        internal System.Windows.Forms.CheckBox ConfigSetAsSystemProxyCB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.LinkLabel ConfigRuleApplyChangesLL;
        private System.Windows.Forms.LinkLabel ConfigRuleCancelChangesLL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.CheckBox ConfigRuleOtherMethodsCB;
        internal System.Windows.Forms.CheckBox ConfigRulePOSTMethodCB;
        internal System.Windows.Forms.CheckBox ConfigRuleGETMethodCB;
        internal System.Windows.Forms.CheckBox ConfigRuleContentHTMLCB;
        internal System.Windows.Forms.CheckBox ConfigRuleContentOtherBinaryCB;
        internal System.Windows.Forms.CheckBox ConfigRuleContentImgCB;
        internal System.Windows.Forms.CheckBox ConfigRuleContentJSCB;
        internal System.Windows.Forms.CheckBox ConfigRuleContentCSSCB;
        internal System.Windows.Forms.CheckBox ConfigRuleContentXMLCB;
        internal System.Windows.Forms.CheckBox ConfigRuleContentOtherTextCB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode500CB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode4xxCB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode403CB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode3xxCB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode301_2CB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode2xxCB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode200CB;
        internal System.Windows.Forms.CheckBox ConfigRuleCode5xxCB;
        internal System.Windows.Forms.TextBox ConfigRuleFileExtensionsMinusTB;
        internal System.Windows.Forms.TextBox ConfigRuleFileExtensionsPlusTB;
        internal System.Windows.Forms.RadioButton ConfigRuleFileExtensionsMinusRB;
        internal System.Windows.Forms.RadioButton ConfigRuleFileExtensionsPlusRB;
        internal System.Windows.Forms.CheckBox ConfigRuleFileExtensionsCB;
        internal System.Windows.Forms.CheckBox ConfigRuleKeywordInRequestCB;
        internal System.Windows.Forms.TextBox ConfigRuleKeywordInRequestPlusTB;
        internal System.Windows.Forms.TextBox ConfigRuleKeywordInRequestMinusTB;
        internal System.Windows.Forms.RadioButton ConfigRuleKeywordInRequestPlusRB;
        internal System.Windows.Forms.RadioButton ConfigRuleKeywordInRequestMinusRB;
        internal System.Windows.Forms.CheckBox ConfigRuleHostNamesCB;
        internal System.Windows.Forms.TextBox ConfigRuleHostNamesPlusTB;
        internal System.Windows.Forms.TextBox ConfigRuleHostNamesMinusTB;
        internal System.Windows.Forms.RadioButton ConfigRuleHostNamesPlusRB;
        internal System.Windows.Forms.RadioButton ConfigRuleHostNamesMinusRB;
        internal System.Windows.Forms.CheckBox ConfigRuleKeywordInResponseCB;
        internal System.Windows.Forms.TextBox ConfigRuleKeywordInResponsePlusTB;
        internal System.Windows.Forms.TextBox ConfigRuleKeywordInResponseMinusTB;
        internal System.Windows.Forms.RadioButton ConfigRuleKeywordInResponsePlusRB;
        internal System.Windows.Forms.RadioButton ConfigRuleKeywordInResponseMinusRB;
        internal System.Windows.Forms.CheckBox ConfigUseUpstreamProxyCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem EncodeDecodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DiffTextToolStripMenuItem;
        internal System.Windows.Forms.OpenFileDialog ProjectFileOpenDialog;
        internal System.Windows.Forms.GroupBox ConfigRuleKeywordInRequestGB;
        internal System.Windows.Forms.GroupBox ConfigRuleKeywordInResponseGB;
        private System.Windows.Forms.TabControl ProxyInterceptRequestTabs;
        private System.Windows.Forms.TabPage ProxyInterceptRequestHeadersTab;
        private System.Windows.Forms.TabPage ProxyInterceptRequestBodyTab;
        private System.Windows.Forms.TabPage ProxyInterceptRequestParametersTab;
        private System.Windows.Forms.TabControl ProxyRequestParametersTabs;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage ProxyInterceptRequestFormatPluginsTab;
        internal IronDataView.IronDataView ProxyRequestBodyIDV;
        internal IronDataView.IronDataView ProxyRequestHeadersIDV;
        private System.Windows.Forms.TabControl ProxyInterceptResponseTabs;
        private System.Windows.Forms.TabPage ProxyInterceptResponseHeadersTab;
        private System.Windows.Forms.TabPage ProxyInterceptResponseBodyTab;
        internal IronDataView.IronDataView ProxyResponseBodyIDV;
        private System.Windows.Forms.TabPage ProxyInterceptResponseFormatPluginsTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForHostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProxyLogGridColumnForSSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProxyLogGridColumnForSetCookie;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProxyLogGridColumnForEdited;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyLogGridColumnForNotes;
        internal System.Windows.Forms.DataGridView ProxyRequestParametersQueryGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyRequestQueryParametersNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProxyRequestQueryParametersValueColumn;
        internal System.Windows.Forms.DataGridView ProxyRequestParametersBodyGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        internal System.Windows.Forms.DataGridView ProxyRequestParametersCookieGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.DataGridView ProxyRequestParametersHeadersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForScanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForHost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ScanLogGridColumnForSSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanLogGridColumnForMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ScanLogGridColumnForSetCookie;
        internal System.Windows.Forms.CheckBox ProxyShowOriginalResponseCB;
        internal System.Windows.Forms.CheckBox ProxyShowOriginalRequestCB;
        private System.Windows.Forms.TabPage MTRequestFormatPluginsTP;
        private System.Windows.Forms.TabPage MTRequestBodyTP;
        private System.Windows.Forms.TabPage MTResponseBodyTP;
        internal IronDataView.IronDataView MTRequestBodyIDV;
        internal IronDataView.IronDataView MTResponseBodyIDV;
        private System.Windows.Forms.TabControl MTRequestParametersTabs;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.DataGridView MTRequestParametersQueryGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.DataGridView MTRequestParametersBodyGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.TabPage tabPage3;
        internal System.Windows.Forms.DataGridView MTRequestParametersCookieGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.TabPage tabPage4;
        internal System.Windows.Forms.DataGridView MTRequestParametersHeadersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        internal System.Windows.Forms.DataGridView ASRequestScanURLGrid;
        internal System.Windows.Forms.TabControl ASRequestTabs;
        internal System.Windows.Forms.DataGridView ASRequestScanQueryGrid;
        internal System.Windows.Forms.DataGridView ConfigureScanRequestFormatPluginsGrid;
        internal System.Windows.Forms.DataGridView ConfigureScanRequestBodyGrid;
        internal System.Windows.Forms.TabControl ASRequestScanBodyTabs;
        internal System.Windows.Forms.TextBox ConfigureScanRequestFormatXMLTB;
        internal System.Windows.Forms.DataGridView ASRequestScanCookieGrid;
        internal System.Windows.Forms.DataGridView ASRequestScanHeadersGrid;
        internal System.Windows.Forms.DataGridView ASScanPluginsGrid;
        internal System.Windows.Forms.ComboBox ASSessionPluginsCombo;
        internal System.Windows.Forms.CheckBox ASRequestScanAllCB;
        internal System.Windows.Forms.CheckBox ASRequestScanURLCB;
        internal System.Windows.Forms.CheckBox ASRequestScanQueryCB;
        internal System.Windows.Forms.CheckBox ASRequestScanBodyCB;
        internal System.Windows.Forms.CheckBox ASRequestScanCookieCB;
        internal System.Windows.Forms.CheckBox ASRequestScanHeadersCB;
        internal IronDataView.IronDataView ASRequestRawHeadersIDV;
        internal IronDataView.IronDataView ASRequestRawBodyIDV;
        private System.Windows.Forms.TabPage tabPage20;
        private System.Windows.Forms.TabPage tabPage21;
        internal System.Windows.Forms.TabControl ASRequestScanFullTabs;
        internal System.Windows.Forms.CheckBox ConfigRuleContentJSONCB;
        internal System.Windows.Forms.RichTextBox PluginDetailsRTB;
        private System.Windows.Forms.ContextMenuStrip PluginTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem allPluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AllPluginsRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AllPluginsANToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passivePluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PassivePluginsRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PassivePluginsANToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activePluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ActivePluginsRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ActivePluginsANToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatPluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FormatPluginsRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FormatPluginsANToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sessionPluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SessionPluginsRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SessionPluginsANToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelectedPluginReloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelectedPluginDeactivateToolStripMenuItem;
        internal System.Windows.Forms.TextBox MTExceptionTB;
        internal System.Windows.Forms.TextBox ASExceptionTB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        internal System.Windows.Forms.TextBox ProxyExceptionTB;
        private System.Windows.Forms.SplitContainer MTRequestFormatSplit;
        internal System.Windows.Forms.DataGridView MTRequestFormatPluginsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        internal System.Windows.Forms.TextBox MTRequestFormatXMLTB;
        private System.Windows.Forms.ContextMenuStrip MTRequestFormatPluginsMenu;
        private System.Windows.Forms.ToolStripMenuItem MTRequestSerXmlToObjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MTRequestDeSerObjectToXmlMenuItem;
        private System.Windows.Forms.ContextMenuStrip ProxyRequestFormatPluginsMenu;
        private System.Windows.Forms.ToolStripMenuItem ProxyRequestSerXmlToObjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProxyRequestDeSerObjectToXmlMenuItem;
        private System.Windows.Forms.ContextMenuStrip ProxyResponseFormatPluginsMenu;
        private System.Windows.Forms.ToolStripMenuItem ProxyResponseSerXmlToObjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProxyResponseDeSerObjectToXmlMenuItem;
        private System.Windows.Forms.SplitContainer ProxyRequestFormatSplit;
        internal System.Windows.Forms.DataGridView ProxyRequestFormatPluginsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        internal System.Windows.Forms.TextBox ProxyRequestFormatXMLTB;
        private System.Windows.Forms.SplitContainer ProxyResponseFormatSplit;
        internal System.Windows.Forms.DataGridView ProxyResponseFormatPluginsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        internal System.Windows.Forms.TextBox ProxyResponseFormatXMLTB;
        private System.Windows.Forms.ContextMenuStrip ConfigureScanRequestFormatPluginsMenu;
        private System.Windows.Forms.ToolStripMenuItem ConfigureScanRequestDeSerObjectToXmlMenuItem;
        internal System.Windows.Forms.Button MultiLineShellExecuteBtn;
        private System.Windows.Forms.TabPage ConfigScriptingTab;
        private System.Windows.Forms.SplitContainer ConfigScriptBaseSplit;
        private System.Windows.Forms.SplitContainer ConfigScriptPathSplit;
        private System.Windows.Forms.SplitContainer ConfigScriptCommandSplit;
        private System.Windows.Forms.LinkLabel ConfigScriptPathApplyChangesLL;
        private System.Windows.Forms.LinkLabel ConfigScriptPathCancelChangesLL;
        private System.Windows.Forms.LinkLabel ConfigScriptCommandApplyChangesLL;
        private System.Windows.Forms.LinkLabel ConfigScriptCommandCancelChangesLL;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.TextBox ConfigUpstreamProxyIPTB;
        internal System.Windows.Forms.TextBox ConfigUpstreamProxyPortTB;
        internal System.Windows.Forms.TextBox ConfigScriptPyPathsTB;
        internal System.Windows.Forms.TextBox ConfigScriptRbPathsTB;
        internal System.Windows.Forms.TextBox ConfigScriptPyCommandsTB;
        internal System.Windows.Forms.TextBox ConfigScriptRbCommandsTB;
        private System.Windows.Forms.TabPage ConfigHTTPAPITab;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.SplitContainer ConfigHTTPAPIBaseSplit;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.LinkLabel ConfigResponseTypesApplyChangesLL;
        private System.Windows.Forms.LinkLabel ConfigResponseTypesCancelChangesLL;
        internal System.Windows.Forms.TextBox ConfigResponseTypesTB;
        internal System.Windows.Forms.TextBox ConfigRequestTypesTB;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.LinkLabel ConfigRequestTypesCancelChangesLL;
        private System.Windows.Forms.LinkLabel ConfigRequestTypesApplyChangesLL;
        private System.Windows.Forms.TabPage ConfigInterceptRulesTab;
        private System.Windows.Forms.TabPage ConfigDisplayRulesTab;
        private System.Windows.Forms.LinkLabel ConfigDisplayRuleApplyChangesLL;
        private System.Windows.Forms.LinkLabel ConfigDisplayRuleCancelChangesLL;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentJSONCB;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleHostNamesCB;
        internal System.Windows.Forms.TextBox ConfigDisplayRuleHostNamesPlusTB;
        internal System.Windows.Forms.TextBox ConfigDisplayRuleHostNamesMinusTB;
        internal System.Windows.Forms.RadioButton ConfigDisplayRuleHostNamesPlusRB;
        internal System.Windows.Forms.RadioButton ConfigDisplayRuleHostNamesMinusRB;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleFileExtensionsCB;
        internal System.Windows.Forms.TextBox ConfigDisplayRuleFileExtensionsPlusTB;
        internal System.Windows.Forms.TextBox ConfigDisplayRuleFileExtensionsMinusTB;
        internal System.Windows.Forms.RadioButton ConfigDisplayRuleFileExtensionsPlusRB;
        internal System.Windows.Forms.RadioButton ConfigDisplayRuleFileExtensionsMinusRB;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentCSSCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode5xxCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentJSCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode500CB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentImgCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode4xxCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentOtherBinaryCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode403CB;
        private System.Windows.Forms.Label label21;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode3xxCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentHTMLCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode301_2CB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleGETMethodCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode2xxCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRulePOSTMethodCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleCode200CB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleOtherMethodsCB;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentOtherTextCB;
        internal System.Windows.Forms.CheckBox ConfigDisplayRuleContentXMLCB;
        internal System.Windows.Forms.Button MTClearFieldsBtn;
        internal System.Windows.Forms.DataGridView SiteMapLogGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForHost;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SiteMapLogGridColumnForSSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteMapLogGridColumnForMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SiteMapLogGridColumnForSetCookie;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForHostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ScriptingLogGridColumnForSSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScriptingLogGridColumnForMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ScriptingLogGridColumnForSetCookie;
        internal System.Windows.Forms.SplitContainer ResultsTopSplit;
        internal System.Windows.Forms.SplitContainer ResultsTabMainSplit;
        private System.Windows.Forms.ContextMenuStrip IronTreeMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ScanBranchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PluginEditorToolStripMenuItem;
        internal ICSharpCode.TextEditor.TextEditorControl MultiLineShellInTE;
        internal ICSharpCode.TextEditor.TextEditorControl PluginEditorInTE;
        private System.Windows.Forms.ToolStripMenuItem CopyRequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyResponseToolStripMenuItem;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        internal System.Windows.Forms.CheckBox ConfigRuleRequestOnResponseRulesCB;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportBurpLogToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog BurpLogOpenDialog;
        internal System.Windows.Forms.RadioButton InteractiveShellPythonRB;
        internal System.Windows.Forms.RadioButton InteractiveShellRubyRB;
        private System.Windows.Forms.ContextMenuStrip ScanQueueMenu;
        private System.Windows.Forms.ToolStripMenuItem StopAllScansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartAllStoppedAndAbortedScansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopThisScanJobToolStripMenuItem;
        private System.Windows.Forms.TabPage mt_trace;
        private System.Windows.Forms.SplitContainer TraceBaseSplit;
        internal System.Windows.Forms.DataGridView TraceGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        internal System.Windows.Forms.RichTextBox TraceMsgRTB;
        private System.Windows.Forms.TabPage ASTraceTab;
        private System.Windows.Forms.SplitContainer ScanTraceBaseSplit;
        internal System.Windows.Forms.DataGridView ScanTraceGrid;
        internal System.Windows.Forms.RichTextBox ScanTraceMsgRTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        internal System.Windows.Forms.TabControl ASMainTabs;
        private System.Windows.Forms.TabPage mt_console;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.RichTextBox ConsoleRTB;
        private System.Windows.Forms.RichTextBox richTextBox2;
        internal System.Windows.Forms.DataGridView ProbeLogGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForHostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProbeLogGridColumnForSSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProbeLogGridColumnForMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProbeLogGridColumnForSetCookie;
        private System.Windows.Forms.TabPage mt_logs;
        internal System.Windows.Forms.TabControl LogDisplayTabs;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabControl LogRequestTabs;
        private System.Windows.Forms.TabPage tabPage16;
        internal IronDataView.IronDataView LogRequestHeadersIDV;
        private System.Windows.Forms.TabPage tabPage22;
        internal IronDataView.IronDataView LogRequestBodyIDV;
        private System.Windows.Forms.TabPage tabPage23;
        private System.Windows.Forms.TabControl tabControl5;
        private System.Windows.Forms.TabPage tabPage24;
        internal System.Windows.Forms.DataGridView LogRequestParametersQueryGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.TabPage tabPage25;
        internal System.Windows.Forms.DataGridView LogRequestParametersBodyGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.TabPage tabPage26;
        internal System.Windows.Forms.DataGridView LogRequestParametersCookieGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.TabPage tabPage27;
        internal System.Windows.Forms.DataGridView LogRequestParametersHeadersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.TabPage tabPage28;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.DataGridView LogRequestFormatPluginsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        internal System.Windows.Forms.TextBox LogRequestFormatXMLTB;
        private System.Windows.Forms.TabPage tabPage29;
        private System.Windows.Forms.TabControl LogResponseTabs;
        private System.Windows.Forms.TabPage tabPage30;
        internal IronDataView.IronDataView LogResponseHeadersIDV;
        private System.Windows.Forms.TabPage tabPage31;
        internal IronDataView.IronDataView LogResponseBodyIDV;
        private System.Windows.Forms.TabPage tabPage32;
        private System.Windows.Forms.SplitContainer splitContainer2;
        internal System.Windows.Forms.DataGridView LogResponseFormatPluginsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        internal System.Windows.Forms.TextBox LogResponseFormatXMLTB;
        private System.Windows.Forms.TabPage ScanLogTab;
        private System.Windows.Forms.TabPage TestLogTab;
        private System.Windows.Forms.TabPage ShellLogTab;
        private System.Windows.Forms.TabPage ProxyLogTab;
        private System.Windows.Forms.TabPage ProbeLogTab;
        private System.Windows.Forms.TabPage SiteMapLogTab;
        private System.Windows.Forms.Button NextLogBtn;
        private System.Windows.Forms.Button PreviousLogBtn;
        internal System.Windows.Forms.TextBox LogStatusTB;
        private System.Windows.Forms.TabPage tabPage39;
        internal System.Windows.Forms.RichTextBox LogReflectionRTB;
        private System.Windows.Forms.TabPage MTTestTP;
        private System.Windows.Forms.TabPage ScriptedSendTP;
        internal ICSharpCode.TextEditor.TextEditorControl CustomSendTE;
        internal System.Windows.Forms.RichTextBox CustomSendBottomRtb;
        internal System.Windows.Forms.RichTextBox CustomSendTopRtb;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.CheckBox CustomSendActivateCB;
        private System.Windows.Forms.RadioButton CustomSendRubyRB;
        private System.Windows.Forms.RadioButton CustomSendPythonRB;
        internal System.Windows.Forms.TextBox CustomSendErrorTB;
        private System.Windows.Forms.TabPage MTRequestTab;
        private System.Windows.Forms.TabPage MTResponseTab;
        private System.Windows.Forms.Button NextTestLog;
        private System.Windows.Forms.Button PreviousTestLog;
        private System.Windows.Forms.Button TestBrownGroupBtn;
        private System.Windows.Forms.Button TestGrayGroupBtn;
        private System.Windows.Forms.Button TestBlueGroupBtn;
        private System.Windows.Forms.Button TestGreenGroupBtn;
        private System.Windows.Forms.Button TestRedGroupBtn;
        internal System.Windows.Forms.DataGridView TestGroupLogGrid;
        internal System.Windows.Forms.SplitContainer LogBaseSplit;
        internal System.Windows.Forms.Button ShowLogGridBtn;
        internal System.Windows.Forms.Label LogIDLbl;
        internal System.Windows.Forms.Label LogSourceLbl;
        internal System.Windows.Forms.TabControl LogTabs;
        private System.Windows.Forms.ToolStripMenuItem RedGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreenGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BlueGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GrayGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BrownGroupToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForHostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MTLogGridColumnForSSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn MTLogGridColumnForMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MTLogGridColumnForSetCookie;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestGroupLogGridForID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestGroupLogGridForHost;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestGroupLogGridForMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestGroupLogGridForURL;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TestGroupLogGridForSSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestGroupLogGridForCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestGroupLogGridForLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestGroupLogGridForMIME;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TestGroupLogGridForSetCookie;
        internal System.Windows.Forms.TabControl MTReqResTabs;
        internal System.Windows.Forms.TabControl MTTabs;
        private System.Windows.Forms.RadioButton ConfiguredScanModeRB;
        private System.Windows.Forms.RadioButton OptimalScanModeRB;
        internal System.Windows.Forms.Label ScanIDLbl;
        internal System.Windows.Forms.Label ScanStatusLbl;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestURLSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestURLPositionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestURLValueColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestQuerySelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestQueryNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestQueryValueColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestBodySelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestBodyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestBodyValueColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestCookieSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestCookieNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestCookieValueColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ASRequestHeadersSelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestHeadersNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASRequestHeadersValueColumn;
        internal System.Windows.Forms.CheckBox ConfigureScanRequestSSLCB;
        internal System.Windows.Forms.Button ASStartScanBtn;
        private System.Windows.Forms.LinkLabel ViewProxyLogLink;
        private System.Windows.Forms.TabPage MTResponseReflectionTP;
        internal System.Windows.Forms.RichTextBox MTReflectionsRTB;
        private System.Windows.Forms.TabPage MTJavaScriptTaintTP;
        internal System.Windows.Forms.RichTextBox JSTaintTraceInRTB;
        private System.Windows.Forms.Button TaintTraceResetTaintConfigBtn;
        private System.Windows.Forms.TabPage JSTaintInputTab;
        private System.Windows.Forms.TabPage JSTaintResultTab;
        internal System.Windows.Forms.TextBox TaintTraceMsgTB;
        internal System.Windows.Forms.TabControl JSTaintTabs;
        private System.Windows.Forms.SplitContainer JSTaintResultSplit;
        internal System.Windows.Forms.DataGridView JSTaintResultGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintResultLineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintResultCodeColumn;
        private System.Windows.Forms.TextBox TaintTraceResultSinkLegendTB;
        private System.Windows.Forms.TextBox TaintTraceResultSourceLegendTB;
        private System.Windows.Forms.TextBox TaintTraceResultSourcePlusSinkLegendTB;
        internal System.Windows.Forms.TextBox JSTaintStatusTB;
        private System.Windows.Forms.TextBox TaintTraceResultSourceToSinkLegendTB;
        private System.Windows.Forms.ContextMenuStrip JSTainTraceEditMenu;
        private System.Windows.Forms.ToolStripMenuItem AddSourceTaintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddSinkTaintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveSourceTaintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveSinkTaintToolStripMenuItem;
        internal System.Windows.Forms.Button JSTaintContinueBtn;
        internal System.Windows.Forms.Button JSTaintTraceControlBtn;
        internal System.Windows.Forms.CheckBox PauseAtTaintCB;
        internal System.Windows.Forms.RichTextBox JSTaintReasonsRTB;
        private System.Windows.Forms.LinkLabel ConfigViewHideLL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button JSTaintConfigShowHideBtn;
        private System.Windows.Forms.Panel JSTaintConfigPanel;
        internal System.Windows.Forms.DataGridView JSTaintConfigGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintDefaultSourceObjectsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintDefaultSinkObjectsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintDefaultArgumentAssignedASourceMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintDefaultArgumentAssignedToSinkMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintDefaultSourceReturningMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintDefaultSinkReturningMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn JSTaintDefaultArgumentReturningMethodsColumn;
        private System.Windows.Forms.Label label30;
        internal System.Windows.Forms.TextBox ConsoleStatusTB;
        internal System.Windows.Forms.TextBox ConsoleScanUrlTB;
        internal System.Windows.Forms.Button ConsoleStartScanBtn;
        internal System.Windows.Forms.Label ScanJobsCompletedLbl;
        internal System.Windows.Forms.Label ScanJobsCreatedLbl;
        internal System.Windows.Forms.Label CrawlerRequestsLbl;
        private System.Windows.Forms.TabPage ConfigTaintConfigTab;
        private System.Windows.Forms.LinkLabel ConfigJSTaintConfigCancelChangesLL;
        private System.Windows.Forms.LinkLabel ConfigJSTaintConfigApplyChangesLL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefaultSourceObjectsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefaultSinkObjectsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefaultArgumentAssignedASourceMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefaultArgumentAssignedToSinkMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefaultSourceReturningMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefaultSinkReturningMethodsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefaultArgumentReturningMethodsColumn;
        internal System.Windows.Forms.DataGridView ConfigDefaultJSTaintConfigGrid;
        private System.Windows.Forms.Button TaintTraceClearTaintConfigBtn;
        private System.Windows.Forms.RadioButton InteractiveScanModeRB;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TabPage ConfigScannerTab;
        private System.Windows.Forms.Label label31;
        internal System.Windows.Forms.Label ConfigScannerThreadMaxCountLbl;
        private System.Windows.Forms.ToolStripMenuItem SelectResponseForJavaScriptTestingToolStripMenuItem;
        internal System.Windows.Forms.Label ConfigCrawlerThreadMaxCountLbl;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.LinkLabel ConfigScannerSettingsCancelChangesLL;
        private System.Windows.Forms.LinkLabel ConfigScannerSettingsApplyChangesLL;
        private System.Windows.Forms.Label label32;
        internal System.Windows.Forms.TextBox ConfigCrawlerUserAgentTB;
        internal System.Windows.Forms.TrackBar ConfigScannerThreadMaxCountTB;
        internal System.Windows.Forms.TrackBar ConfigCrawlerThreadMaxCountTB;
        private System.Windows.Forms.ToolStripMenuItem RenderHTMLToolStripMenuItem;
        internal System.Windows.Forms.CheckBox JSTaintShowSourceToSinkCB;
        private System.Windows.Forms.Label JSTaintShowLinesLbl;
        internal System.Windows.Forms.CheckBox JSTaintShowSinkCB;
        internal System.Windows.Forms.CheckBox JSTaintShowSourceCB;
        internal System.Windows.Forms.CheckBox JSTaintShowCleanCB;
        private System.Windows.Forms.ToolStripMenuItem CopyLineTaintToolStripMenuItem;
        private System.Windows.Forms.Button LogOptionsBtn;
        private System.Windows.Forms.Button ProxyOptionsBtn;
        private System.Windows.Forms.TabPage ConfigPassiveAnalysisTab;
        private System.Windows.Forms.LinkLabel ConfigPassiveAnalysisSettingsCancelChangesLL;
        private System.Windows.Forms.LinkLabel ConfigPassiveAnalysisSettingsApplyChangesLL;
        private System.Windows.Forms.Label label34;
        internal System.Windows.Forms.CheckBox ConfigPassiveAnalysisOnProxyTrafficCB;
        internal System.Windows.Forms.CheckBox ConfigPassiveAnalysisOnProbeTrafficCB;
        internal System.Windows.Forms.CheckBox ConfigPassiveAnalysisOnScanTrafficCB;
        internal System.Windows.Forms.CheckBox ConfigPassiveAnalysisOnTestTrafficCB;
        internal System.Windows.Forms.CheckBox ConfigPassiveAnalysisOnShellTrafficCB;
        internal System.Windows.Forms.Button ClearShellDisplayBtn;
    }
}

