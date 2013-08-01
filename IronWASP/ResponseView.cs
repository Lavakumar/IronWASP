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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace IronWASP
{
    public partial class ResponseView : UserControl
    {
        public ResponseView()
        {
            InitializeComponent();
        }

        int ExpandedParameterIndex = 0;

        Request RequestOfDisplayedResponse;
        Response DisplayedResponse;

        bool readOnly = false;

        bool HeadersChanged = false;
        bool BodyChanged = false;
        bool BodyTypeFormatPluginsParametersChanged = false;
        bool BodyFormatXmlChanged = false;

        Thread FormatPluginCallingThread;
        Thread ReflectionCheckingThread;

        string CurrentFormatXml = "";
        string[,] CurrentXmlNameValueArray = new string[,] { };

        bool HasReflectionTab = true;

        public delegate void ResponseChangedEvent();

        public event ResponseChangedEvent ResponseChanged;

        public bool IncludeReflectionTab
        {
            get
            {
                return HasReflectionTab;
            }
            set
            {
                if(HasReflectionTab)
                {
                    if (!value)
                        HasReflectionTab = value;
                }
            }
        }

        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                SetReadOnly(value);
            }
        }

        delegate void SetReadOnly_d(bool ReadOnlyVal);
        public void SetReadOnly(bool ReadOnlyVal)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetReadOnly_d InvokeDelegate_d = new SetReadOnly_d(SetReadOnly);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { ReadOnlyVal });
            }
            else
            {
                this.readOnly = ReadOnlyVal;
                HeadersTBP.ReadOnly = ReadOnlyVal;
                BodyTBP.ReadOnly = ReadOnlyVal;
                FormatXmlTBP.ReadOnly = ReadOnlyVal;
                EditTBP.ReadOnly = ReadOnly;
                SaveEditsLbl.Visible = !ReadOnly;
                //Disable format plugins
                //Make all parameters grid value fields read-only
                foreach (DataGridViewRow Row in BodyFormatPluginsParametersGrid.Rows)
                {
                    Row.Cells[1].ReadOnly = this.ReadOnly;
                }
            }
        }

        public void ClearResponse()
        {
            this.DisplayedResponse = null;
            this.ClearData();
        }

        delegate void ClearData_d();
        void ClearData()
        {
            if (this.BaseTabs.InvokeRequired)
            {
                ClearData_d InvokeDelegate_d = new ClearData_d(ClearData);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { });
            }
            else
            {
                RoundTripLbl.Text = "";
                HeadersTBP.ClearData();
                BodyTBP.ClearData();
                FormatXmlTBP.ClearData();
                ConvertXmlToObjectBtn.Text = "Convert this XML to Object";
                BodyFormatPluginsParametersGrid.Rows.Clear();
                ReflectionRTB.Text = "";
                ClearEditTab();
                foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
                {
                    Row.Cells[0].Value = false;
                }
                if(FormatPluginCallingThread != null)
                {
                    try
                    {
                        FormatPluginCallingThread.Abort();
                    }
                    catch { }
                }
                
                if(ReflectionCheckingThread != null)
                {
                    try
                    {
                        ReflectionCheckingThread.Abort();
                    }
                    catch { }
                }
                ShowStatusMsg("");
                ShowProgressBar(false);
            }
        }

        delegate void ShowStatusMsg_d(string Msg);
        public void ShowStatusMsg(string Msg)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                ShowStatusMsg_d InvokeDelegate_d = new ShowStatusMsg_d(ShowStatusMsg);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Msg });
            }
            else
            {
                StatusAndErrorTB.Text = Msg;
                if (Msg.Length == 0)
                {
                    StatusAndErrorTB.Visible = false;
                }
                else
                {
                    StatusAndErrorTB.ForeColor = Color.Black;
                    StatusAndErrorTB.Visible = true;
                }
            }
        }

        delegate void ShowErrorMsg_d(string Msg);
        public void ShowErrorMsg(string Msg)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                ShowErrorMsg_d InvokeDelegate_d = new ShowErrorMsg_d(ShowErrorMsg);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Msg });
            }
            else
            {
                StatusAndErrorTB.Text = Msg;
                if (Msg.Length == 0)
                {
                    StatusAndErrorTB.Visible = false;
                }
                else
                {
                    StatusAndErrorTB.ForeColor = Color.Red;
                    StatusAndErrorTB.Visible = true;
                }
            }
        }

        delegate void ShowProgressBar_d(bool Show);
        public void ShowProgressBar(bool Show)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                ShowProgressBar_d InvokeDelegate_d = new ShowProgressBar_d(ShowProgressBar);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Show });
            }
            else
            {
                this.WaitProgressBar.Visible = Show;
            }
        }

        public void SetResponse(Response Res)
        {
            this.SetResponse(Res, null);
        }
        
        delegate void SetResponse_d(Response Res, Request Req);
        public void SetResponse(Response Res, Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetResponse_d InvokeDelegate_d = new SetResponse_d(SetResponse);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Res, Req });
            }
            else
            {
                this.ClearData();

                this.SetHeader(Res);
                this.SetBody(Res);
                this.SetRoundTrip(Res.RoundTrip);
                FormatPluginsGrid.Rows.Clear();
                foreach (string Name in FormatPlugin.List())
                {
                    FormatPluginsGrid.Rows.Add(new object[] { false, Name });
                }
                this.ResetAllChangedValueStatus();
                this.DisplayedResponse = Res;
                this.RequestOfDisplayedResponse = Req;
                this.AutoDetectFormatAndSetBodyParameters(Res);
                CheckAndShowReflection();
            }
        }

        delegate void SetHeader_d(Response Res);
        void SetHeader(Response Res)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetHeader_d InvokeDelegate_d = new SetHeader_d(SetHeader);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Res });
            }
            else
            {
                this.HeadersTBP.SetText(Res.GetHeadersAsString());
                this.ResetHeadersChangedStatus();
            }
        }

        delegate void SetRoundTrip_d(int RoundTrip);
        void SetRoundTrip(int RoundTrip)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetRoundTrip_d InvokeDelegate_d = new SetRoundTrip_d(SetRoundTrip);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { RoundTrip });
            }
            else
            {
                this.RoundTripLbl.Text = string.Format("{0} ms", RoundTrip);
            }
        }

        delegate void SetBody_d(Response Res);
        void SetBody(Response Res)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetBody_d InvokeDelegate_d = new SetBody_d(SetBody);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Res });
            }
            else
            {
                if (Res.HasBody)
                {
                    if (Res.IsBinary)
                        this.BodyTBP.SetBytes(Res.BodyArray);
                    else
                        this.BodyTBP.SetText(Res.BodyString);
                }
                this.ResetBodyChangedStatus();
            }
        }

        void AutoDetectFormatAndSetBodyParameters(Response Res)
        {
            if (FormatPluginCallingThread != null)
            {
                try
                {
                    FormatPluginCallingThread.Abort();
                }
                catch { }
            }
            ShowStatusMsg("Detecting Request body format..");
            ShowProgressBar(true);
            FormatPluginCallingThread = new Thread(AutoDetectFormatAndSetBodyParameters);
            FormatPluginCallingThread.Start(Res);
        }
        void AutoDetectFormatAndSetBodyParameters(object ResObj)
        {
            try
            {
                Response Res = ((Response)ResObj).GetClone();
                string FPName = FormatPlugin.Get(Res);
                if (FPName.Length > 0 && FPName != "Normal")
                {
                    try
                    {
                        FormatPlugin FP = FormatPlugin.Get(FPName);
                        CurrentFormatXml = FP.ToXmlFromResponse(Res);
                        CurrentXmlNameValueArray = FormatPlugin.XmlToArray(CurrentFormatXml);
                        SetDeserializedDataInUi(FP.Name, CurrentFormatXml, CurrentXmlNameValueArray);
                    }
                    catch
                    { }
                }
                this.ResetBodyTypeFormatPluginsParametersChangedStatus();
                ShowStatusMsg("");
                ShowProgressBar(false);
            }
            catch{}
        }

        public Response GetResponse()
        {
            try
            {
                return this.GetResponseOrException();
            }
            catch
            {
                return null;
            }
        }
        public Response GetResponseOrException()
        {
            this.HandleAllDataChanges();
            if (FormatPluginCallingThread != null)
            {
                try
                {
                    while (FormatPluginCallingThread.ThreadState == ThreadState.Running)
                    {
                        Thread.Sleep(100);
                    }
                }
                catch { }
            }
            return DisplayedResponse;
        }

        delegate void UpdateResponse_d();
        public void UpdateResponse()
        {
            if (this.BaseTabs.InvokeRequired)
            {
                UpdateResponse_d InvokeDelegate_d = new UpdateResponse_d(UpdateResponse);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { });
            }
            else
            {
                try
                {
                    this.HandleAllDataChanges();
                }
                catch (Exception Exp)
                {
                    ShowErrorMsg(Exp.Message);
                }
            }
        }

        void HandleAllDataChanges()
        {
            if (this.ReadOnly) return;
            if (HeadersChanged || BodyChanged )
            {
                ShowStatusMsg("");
            }
            if (HeadersChanged)
            {
                Response NewResponse;
                if (DisplayedResponse.HasBody)
                {
                    if (DisplayedResponse.IsBinary)
                        NewResponse = new Response(HeadersTBP.GetText(), BodyTBP.GetBytes());
                    else
                        NewResponse = new Response(HeadersTBP.GetText(), BodyTBP.GetText());
                }
                else
                {
                    NewResponse = new Response(HeadersTBP.GetText(), new byte[]{});
                }
                this.DisplayedResponse = NewResponse;
                ResetHeadersChangedStatus();
            }
            if (BodyChanged && this.DisplayedResponse != null)
            {
                if (BodyTBP.IsBinary)
                    this.DisplayedResponse.BodyArray = BodyTBP.GetBytes();
                else
                    this.DisplayedResponse.BodyString = BodyTBP.GetText();
                AutoDetectFormatAndSetBodyParameters(this.DisplayedResponse);
                ResetBodyChangedStatus();
                CheckAndShowReflection();
            }
            if (BodyTypeFormatPluginsParametersChanged && this.DisplayedResponse != null)
            {
                string[,] EditedNameValuePairs = new string[BodyFormatPluginsParametersGrid.Rows.Count, 2];
                foreach (DataGridViewRow Row in BodyFormatPluginsParametersGrid.Rows)
                {
                    EditedNameValuePairs[Row.Index, 0] = Row.Cells[0].Value.ToString();
                    if (Row.Cells[1].Value == null)
                        EditedNameValuePairs[Row.Index, 1] = "";
                    else
                        EditedNameValuePairs[Row.Index, 1] = Row.Cells[1].Value.ToString();
                }
                string PluginName = GetSelectedFormatPluginName();
                if (PluginName.Length > 0)
                    SerializeNewParametersWithFormatPlugin(EditedNameValuePairs, PluginName);
                ResetBodyTypeFormatPluginsParametersChangedStatus();
            }
        }

        void ClearBodyTypeFormatPluginsUi()
        {
            BodyFormatPluginsParametersGrid.Rows.Clear();
            ConvertXmlToObjectBtn.Text = "Convert this XML to Object";
            FormatXmlTBP.ClearData();
            foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
            {
                Row.Cells[0].Value = false;
            }
            ResetBodyFormatXmlChangedStatus();
            ResetBodyTypeFormatPluginsParametersChangedStatus();
        }

        void ResetAllChangedValueStatus()
        {
            ResetHeadersChangedStatus();
            ResetBodyChangedStatus();
            ResetBodyTypeFormatPluginsParametersChangedStatus();
            ResetBodyFormatXmlChangedStatus();
        }
        
        void ResetHeadersChangedStatus()
        {
            HeadersChanged = false;
        }
        void ResetBodyChangedStatus()
        {
            BodyChanged = false;
        }
        void ResetBodyTypeFormatPluginsParametersChangedStatus()
        {
            BodyTypeFormatPluginsParametersChanged = false;
        }
        void ResetBodyFormatXmlChangedStatus()
        {
            BodyFormatXmlChanged = false;
        }

        private void BaseTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                this.HandleAllDataChanges();
            }
            catch (Exception Exp) { ShowErrorMsg(Exp.Message); }
        }

        private void FormatPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DisplayedResponse == null) return;
            string PluginName = "";
            CurrentFormatXml = "";
            CurrentXmlNameValueArray = new string[,] { };
            foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
            {
                if (e.RowIndex == Row.Index)
                {
                    PluginName = Row.Cells[1].Value.ToString();
                }
                Row.Cells[0].Value = false;
            }
            BodyFormatPluginsParametersGrid.Rows.Clear();
            FormatXmlTBP.ClearData();
            ConvertXmlToObjectBtn.Text = "Convert this XML to Object";
            if (FormatPluginCallingThread != null)
            {
                try
                {
                    FormatPluginCallingThread.Abort();
                }
                catch { }
            }
            if (PluginName.Length == 0) return;
            ShowStatusMsg(string.Format("Parsing Response body as {0}", PluginName));
            ShowProgressBar(true);
            FormatPluginCallingThread = new Thread(DeserializeWithFormatPlugin);
            FormatPluginCallingThread.Start(PluginName);
        }

        private void ConvertXmlToObjectBtn_Click(object sender, EventArgs e)
        {
            if (this.ReadOnly) return;
            if (BodyFormatXmlChanged)
            {
                string XML = FormatXmlTBP.GetText();
                string PluginName = this.GetSelectedFormatPluginName();
                if (PluginName.Length > 0 && XML.Length > 0)
                    this.SerializeNewXmlWithFormatPlugin(XML, PluginName);
            }
            ResetBodyFormatXmlChangedStatus();
        }

        private void FormatXmlTBP_ValueChanged()
        {
            BodyFormatXmlChanged = true;
        }

        private void BodyFormatPluginsParametersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            BodyTypeFormatPluginsParametersChanged = true;
            if (DisplayedResponse != null && ResponseChanged != null)
                ResponseChanged();
        }

        private void BodyTBP_ValueChanged()
        {
            BodyChanged = true;
            if (DisplayedResponse != null && ResponseChanged != null)
                ResponseChanged();
        }

        private void HeadersTBP_ValueChanged()
        {
            HeadersChanged = true;
            if (DisplayedResponse != null && ResponseChanged != null)
                ResponseChanged();
        }

        void DeserializeWithFormatPlugin(object PluginNameObject)
        {
            string PluginName = PluginNameObject.ToString();
            try
            {
                Response Res = DisplayedResponse.GetClone(true);
                FormatPlugin FP = FormatPlugin.Get(PluginName);
                CurrentFormatXml = FP.ToXmlFromResponse(Res);
                CurrentXmlNameValueArray = FormatPlugin.XmlToArray(CurrentFormatXml);
                ShowStatusMsg("");
                SetDeserializedDataInUi(PluginName, CurrentFormatXml, CurrentXmlNameValueArray);
                ShowProgressBar(false);
            }
            catch (ThreadAbortException)
            {
                ShowStatusMsg("");
            }
            catch (Exception Exp)
            {
                IronException.Report(string.Format("Error converting Response to {0}", PluginName), Exp);
                ShowErrorMsg(string.Format("Unable to parse Response body as {0}", PluginName));
                ShowProgressBar(false);
            }
        }

        delegate void SetDeserializedDataInUi_d(string PluginName, string XML, string[,] XmlNameValueArray);
        void SetDeserializedDataInUi(string PluginName, string XML, string[,] XmlNameValueArray)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetDeserializedDataInUi_d InvokeDelegate_d = new SetDeserializedDataInUi_d(SetDeserializedDataInUi);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { PluginName, XML, XmlNameValueArray });
            }
            else
            {
                foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
                {
                    if (Row.Cells[1].Value.ToString().Equals(PluginName))
                        Row.Cells[0].Value = true;
                    else
                        Row.Cells[0].Value = false;
                }
                FormatXmlTBP.SetText(XML);
                ConvertXmlToObjectBtn.Text = string.Format("Convert this XML to {0}", PluginName);
                BodyFormatPluginsParametersGrid.Rows.Clear();
                for (int i = 0; i < XmlNameValueArray.GetLength(0); i++)
                {
                    int RowId = BodyFormatPluginsParametersGrid.Rows.Add(new object[] { XmlNameValueArray[i, 0], XmlNameValueArray[i, 1], Properties.Resources.Glass });
                    BodyFormatPluginsParametersGrid.Rows[RowId].Cells[1].ReadOnly = this.ReadOnly;
                }
            }
        }

        void SerializeNewParametersWithFormatPlugin(string[,] EditedXmlNameValueArray, string PluginName)
        {
            for (int i = 0; i < this.CurrentXmlNameValueArray.GetLength(0); i++)
            {
                if (this.CurrentXmlNameValueArray[i, 0].Equals(EditedXmlNameValueArray[i, 0]))
                {
                    if (!this.CurrentXmlNameValueArray[i, 1].Equals(EditedXmlNameValueArray[i, 1]))
                    {
                        this.CurrentFormatXml = FormatPlugin.InjectInXml(this.CurrentFormatXml, i, EditedXmlNameValueArray[i, 1]);
                    }
                }
            }
            this.CurrentXmlNameValueArray = EditedXmlNameValueArray;
            this.SerializeNewXmlWithFormatPlugin(this.CurrentFormatXml, PluginName);
        }
        void SerializeNewXmlWithFormatPlugin(string XML, string PluginName)
        {
            this.CurrentFormatXml = XML;
            this.CurrentXmlNameValueArray = FormatPlugin.XmlToArray(this.CurrentFormatXml);
            if (FormatPluginCallingThread != null)
            {
                try
                {
                    FormatPluginCallingThread.Abort();
                }
                catch { }
            }
            ShowProgressBar(true);
            ShowStatusMsg(string.Format("Updating edited values in {0}", PluginName));
            FormatPluginCallingThread = new Thread(SerializeNewXmlWithFormatPlugin);
            FormatPluginCallingThread.Start(PluginName);
        }

        void SerializeNewXmlWithFormatPlugin(object PluginNameObject)
        {
            string PluginName = PluginNameObject.ToString();
            try
            {
                Response Res = DisplayedResponse.GetClone(true);
                FormatPlugin FP = FormatPlugin.Get(PluginName);
                Response NewResponse = FP.ToResponseFromXml(Res, CurrentFormatXml);
                this.DisplayedResponse = NewResponse;
                ShowStatusMsg("");
                this.SetNonFormatPluginResponseFields(NewResponse);
                ShowProgressBar(false);
                CheckAndShowReflection();
            }
            catch (ThreadAbortException)
            {
                ShowStatusMsg("");
            }
            catch (Exception Exp)
            {
                IronException.Report(string.Format("Error converting {0} to Response", PluginName), Exp);
                ShowErrorMsg(string.Format("Unable to update edited values in {0}", PluginName));
                ShowProgressBar(false);
            }
        }
        delegate void SetNonFormatPluginResponseFields_d(Response Res);
        void SetNonFormatPluginResponseFields(Response Res)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetNonFormatPluginResponseFields_d InvokeDelegate_d = new SetNonFormatPluginResponseFields_d(SetNonFormatPluginResponseFields);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Res });
            }
            else
            {
                this.SetHeader(Res);
                this.SetBody(Res);
            }
        }

        string GetSelectedFormatPluginName()
        {
            foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                    return Row.Cells[1].Value.ToString();
            }
            return "";
        }

        private void ResponseView_Load(object sender, EventArgs e)
        {
            FormatPluginsGrid.Rows.Clear();
            foreach (string Name in FormatPlugin.List())
            {
                FormatPluginsGrid.Rows.Add(new object[] { false, Name });
            }
            if (!HasReflectionTab)
            {
                BaseTabs.TabPages.RemoveByKey("ReflectionsTab");
            }
        }

        void CheckAndShowReflection()
        {
            if (DisplayedResponse != null && RequestOfDisplayedResponse != null && HasReflectionTab)
            {
                Session Sess = new Session(RequestOfDisplayedResponse, DisplayedResponse);
                if (ReflectionCheckingThread != null)
                {
                    try
                    {
                        ReflectionCheckingThread.Abort();
                    }
                    catch { }
                }
                ShowReflection("Checking for reflection...");
                ReflectionCheckingThread = new Thread(CheckReflection);
                ReflectionCheckingThread.Start(Sess);
            }
        }

        void CheckReflection(object SessObject)
        {
            if (!HasReflectionTab) return;
            try
            {
                Session Sess = (Session)SessObject;
                string Reflection = Analyzer.CheckReflections(Sess);
                ShowReflection(Reflection);
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp)
            {
                ShowReflection("Error when checking for reflections");
                IronException.Report("Error checking reflections", Exp);
            }
        }

        delegate void ShowReflection_d(string ReflectionString);
        void ShowReflection(string ReflectionString)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                ShowReflection_d InvokeDelegate_d = new ShowReflection_d(ShowReflection);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { ReflectionString });
            }
            else
            {
                if (!HasReflectionTab) return;
                StringBuilder ReflectionBuilder = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
                ReflectionBuilder.Append(Tools.RtfSafe(ReflectionString));
                ReflectionRTB.Rtf = ReflectionBuilder.ToString();
            }
        }

        private void RenderLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.DisplayedResponse != null)
            {
                //Tools.Run(Config.RootDir + "/RenderHtml.exe", Tools.Base64Encode(this.DisplayedResponse.BodyString));
                this.DisplayedResponse.Render();
            }
        }

        private void BodyFormatPluginsParametersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                ExpandedParameterIndex = e.RowIndex;
                SetEditTab(BodyFormatPluginsParametersGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }

        void SetEditTab(string Value)
        {
            EditTBP.SetText(Value);
            BaseTabs.TabPages["EditingTab"].Text = "  Selected Parameter Value  ";
            BaseTabs.SelectTab("EditingTab");
        }

        void ClearEditTab()
        {
            EditTBP.SetText("");
            BaseTabs.TabPages["EditingTab"].Text = "  ";
            if (BaseTabs.SelectedTab.Name == "EditingTab")
            {
                BaseTabs.SelectTab("HeadersTab");
            }
        }

        private void SaveEditsLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BodyFormatPluginsParametersGrid.Rows[ExpandedParameterIndex].Cells[1].Value = EditTBP.GetText();
            BodyTypeFormatPluginsParametersChanged = true;
            BaseTabs.SelectTab("BodyParametersTab");
            ClearEditTab();
        }

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
