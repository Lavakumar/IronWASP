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
    public partial class RequestView : UserControl
    {
        public RequestView()
        {
            InitializeComponent();
        }

        Request DisplayedRequest;

        bool readOnly = false;

        bool UseSslChanged = false;
        bool HeadersChanged = false;
        bool BodyChanged = false;
        bool UrlPathPartsChanged = false;
        bool QueryParametersChanged = false;
        bool BodyTypeNormalParametersChanged = false;
        bool BodyTypeFormatPluginsParametersChanged = false;
        bool BodyFormatXmlChanged = false;
        bool CookieParametersChanged = false;
        bool HeadersParametersChanged = false;

        Thread FormatPluginCallingThread;

        string CurrentFormatXml = "";
        string[,] CurrentXmlNameValueArray = new string[,] { };

        public delegate void RequestChangedEvent();

        public event RequestChangedEvent RequestChanged;

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
                readOnly = ReadOnlyVal;
                UseSSLCB.Enabled = !ReadOnlyVal;
                HeadersTBP.ReadOnly = ReadOnlyVal;
                BodyTBP.ReadOnly = ReadOnlyVal;
                FormatXmlTBP.ReadOnly = ReadOnlyVal;
                //Disable format plugins
                //Make all parameters grid value fields read-only
                foreach (DataGridViewRow Row in UrlPathPartsParametersGrid.Rows)
                {
                    Row.Cells[1].ReadOnly = this.ReadOnly;
                }
                foreach (DataGridViewRow Row in QueryParametersGrid.Rows)
                {
                    Row.Cells[1].ReadOnly = this.ReadOnly;
                }
                foreach (DataGridViewRow Row in BodyNormalTypeParametersGrid.Rows)
                {
                    Row.Cells[1].ReadOnly = this.ReadOnly;
                }
                foreach (DataGridViewRow Row in BodyFormatPluginsParametersGrid.Rows)
                {
                    Row.Cells[1].ReadOnly = this.ReadOnly;
                }
                foreach (DataGridViewRow Row in CookieParametersGrid.Rows)
                {
                    Row.Cells[1].ReadOnly = this.ReadOnly;
                }
                foreach (DataGridViewRow Row in HeadersParametersGrid.Rows)
                {
                    Row.Cells[1].ReadOnly = this.ReadOnly;
                }
            }
        }

        public void ClearRequest()
        {
            this.DisplayedRequest = null;
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
                UseSSLCB.Checked = false;
                HeadersTBP.ClearData();
                BodyTBP.ClearData();
                FormatXmlTBP.ClearData();
                ConvertXmlToObjectBtn.Text = "Convert this XML to Object";
                UrlPathPartsParametersGrid.Rows.Clear();
                QueryParametersGrid.Rows.Clear();
                CookieParametersGrid.Rows.Clear();
                HeadersParametersGrid.Rows.Clear();
                BodyNormalTypeParametersGrid.Rows.Clear();
                BodyFormatPluginsParametersGrid.Rows.Clear();
                foreach (DataGridViewRow Row in FormatPluginsGrid.Rows)
                {
                    Row.Cells[0].Value = false;
                }
                if (FormatPluginCallingThread != null)
                {
                    try
                    {
                        FormatPluginCallingThread.Abort();
                    }
                    catch { }
                }
                ShowStatusMsg("");
                ShowProgressBar(false);
            }
        }

        public void ClearStatusAndError()
        {
            ShowStatusMsg("");
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

        delegate void SetRequest_d(Request Req);
        public void SetRequest(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetRequest_d InvokeDelegate_d = new SetRequest_d(SetRequest);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                this.ClearData();

                this.SetHeader(Req);
                this.SetBody(Req);
                this.SetUrlPathPartsParameters(Req);
                this.SetQueryParameters(Req);
                this.SetBodyParameters(Req);
                this.SetCookieParameters(Req);
                this.SetHeadersParameters(Req);

                FormatPluginsGrid.Rows.Clear();
                foreach (string Name in FormatPlugin.List())
                {
                    FormatPluginsGrid.Rows.Add(new object[]{false, Name});
                }
                this.ResetAllChangedValueStatus();
                this.DisplayedRequest = Req;
            }
        }

        delegate void SetHeader_d(Request Req);
        void SetHeader(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetHeader_d InvokeDelegate_d = new SetHeader_d(SetHeader);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                this.HeadersTBP.SetText(Req.GetHeadersAsStringWithoutFullURL());
                this.UseSSLCB.Checked = Req.SSL;
                this.ResetHeadersChangedStatus();
                this.ResetSslChangedStatus();
            }
        }
        
        delegate void SetBody_d(Request Req);
        void SetBody(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetBody_d InvokeDelegate_d = new SetBody_d(SetBody);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                if (Req.HasBody)
                {
                    if (Req.IsBinary)
                        this.BodyTBP.SetBytes(Req.BodyArray);
                    else
                        this.BodyTBP.SetText(Req.BodyString);
                }
                this.ResetBodyChangedStatus();
            }
        }
        
        delegate void SetUrlPathPartsParameters_d(Request Req);
        void SetUrlPathPartsParameters(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetUrlPathPartsParameters_d InvokeDelegate_d = new SetUrlPathPartsParameters_d(SetUrlPathPartsParameters);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                List<string> UrlPathParts = Req.UrlPathParts;
                UrlPathPartsParametersGrid.Rows.Clear();
                for (int i = 0; i < UrlPathParts.Count; i++)
                {
                    int RowId = UrlPathPartsParametersGrid.Rows.Add(new object[] { i, UrlPathParts[i] });
                    UrlPathPartsParametersGrid.Rows[RowId].Cells[1].ReadOnly = this.ReadOnly;
                }
                this.ResetUrlPathPartsChangedStatus();
            }
        }

        delegate void SetQueryParameters_d(Request Req);
        void SetQueryParameters(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetQueryParameters_d InvokeDelegate_d = new SetQueryParameters_d(SetQueryParameters);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                QueryParametersGrid.Rows.Clear();
                foreach (string Name in Req.Query.GetNames())
                {
                    foreach (string Value in Req.Query.GetAll(Name))
                    {
                        int RowId = QueryParametersGrid.Rows.Add(new object[] { Name, Value });
                        QueryParametersGrid.Rows[RowId].Cells[1].ReadOnly = this.ReadOnly;
                    }
                }
                this.ResetQueryParametersChangedStatus();
            }
        }

        delegate void SetBodyParameters_d(Request Req);
        void SetBodyParameters(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetBodyParameters_d InvokeDelegate_d = new SetBodyParameters_d(SetBodyParameters);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                BodyNormalTypeParametersGrid.Rows.Clear();
                foreach (string Name in Req.Body.GetNames())
                {
                    foreach (string Value in Req.Body.GetAll(Name))
                    {
                        int RowId = BodyNormalTypeParametersGrid.Rows.Add(new object[] { Name, Value });
                        BodyNormalTypeParametersGrid.Rows[RowId].Cells[1].ReadOnly = this.ReadOnly;
                    }
                }
                this.ResetBodyTypeNomarlParametersChangedStatus();
            }
        }

        delegate void SetCookieParameters_d(Request Req);
        void SetCookieParameters(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetCookieParameters_d InvokeDelegate_d = new SetCookieParameters_d(SetCookieParameters);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                CookieParametersGrid.Rows.Clear();
                foreach (string Name in Req.Cookie.GetNames())
                {
                    foreach (string Value in Req.Cookie.GetAll(Name))
                    {
                        int RowId = CookieParametersGrid.Rows.Add(new object[] { Name, Value });
                        CookieParametersGrid.Rows[RowId].Cells[1].ReadOnly = this.ReadOnly;
                    }
                }
                this.ResetCookieParametersChangedStatus();
            }
        }

        delegate void SetHeadersParameters_d(Request Req);
        void SetHeadersParameters(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetHeadersParameters_d InvokeDelegate_d = new SetHeadersParameters_d(SetHeadersParameters);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                HeadersParametersGrid.Rows.Clear();
                foreach (string Name in Req.Headers.GetNames())
                {
                    if (!Name.Equals("Host", StringComparison.OrdinalIgnoreCase) && !Name.Equals("Cookie", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (string Value in Req.Headers.GetAll(Name))
                        {
                            int RowId = HeadersParametersGrid.Rows.Add(new object[] { Name, Value });
                            HeadersParametersGrid.Rows[RowId].Cells[1].ReadOnly = this.ReadOnly;
                        }
                    }
                }
                this.ResetHeadersParametersChangedStatus();
            }
        }

        public Request GetRequest()
        {
            try
            {
                return this.GetRequestOrException();
            }
            catch
            {
                return null;
            }
        }
        public Request GetRequestOrException()
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
            return DisplayedRequest;
        }

        delegate void UpdateRequest_d();
        public void UpdateRequest()
        {
            if (this.BaseTabs.InvokeRequired)
            {
                UpdateRequest_d InvokeDelegate_d = new UpdateRequest_d(UpdateRequest);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { });
            }
            else
            {
                try
                {
                    this.HandleAllDataChanges();
                }
                catch(Exception Exp)
                {
                    ShowErrorMsg(Exp.Message);
                }
            }
        }

        void HandleAllDataChanges()
        {
            if (this.ReadOnly) return;
            if (HeadersChanged || BodyChanged || UrlPathPartsChanged || QueryParametersChanged || BodyTypeNormalParametersChanged || CookieParametersChanged || HeadersParametersChanged)
            {
                ShowStatusMsg("");
            }
            if (HeadersChanged)
            {
                Request NewRequest = new Request(HeadersTBP.GetText(), UseSSLCB.Checked);
                if(DisplayedRequest.HasBody)
                {
                    if (DisplayedRequest.IsBinary)
                        NewRequest.BodyArray = BodyTBP.GetBytes();
                    else
                        NewRequest.BodyString = BodyTBP.GetText();
                }
                this.DisplayedRequest = NewRequest;

                SetUrlPathPartsParameters(this.DisplayedRequest);
                SetQueryParameters(this.DisplayedRequest);
                SetCookieParameters(this.DisplayedRequest);
                SetHeadersParameters(this.DisplayedRequest);
                
                ResetHeadersChangedStatus();
                ResetUrlPathPartsChangedStatus();
                ResetQueryParametersChangedStatus();
                ResetCookieParametersChangedStatus();
                ResetHeadersParametersChangedStatus();
            }
            if (BodyChanged && this.DisplayedRequest != null)
            {
                if (BodyTBP.IsBinary)
                    this.DisplayedRequest.BodyArray = BodyTBP.GetBytes();
                else
                    this.DisplayedRequest.BodyString = BodyTBP.GetText();
                SetBodyParameters(this.DisplayedRequest);
                ClearBodyTypeFormatPluginsUi();
                ResetBodyChangedStatus();
                ResetBodyTypeNomarlParametersChangedStatus();
            }
            if (UrlPathPartsChanged && this.DisplayedRequest != null)
            {
                List<string> UrlPathParts = new List<string>();
                foreach (DataGridViewRow Row in UrlPathPartsParametersGrid.Rows)
                {
                    if (Row.Cells[1].Value == null)
                        UrlPathParts.Add("");
                    else
                        UrlPathParts.Add(Row.Cells[1].Value.ToString());
                }
                this.DisplayedRequest.UrlPathParts = UrlPathParts;
                HeadersTBP.SetText(this.DisplayedRequest.GetHeadersAsStringWithoutFullURL());
                ResetHeadersChangedStatus();
                ResetUrlPathPartsChangedStatus();
            }
            if (QueryParametersChanged && this.DisplayedRequest != null)
            {
                this.DisplayedRequest.Query.RemoveAll();
                foreach (DataGridViewRow Row in QueryParametersGrid.Rows)
                {
                    if (Row.Cells[1].Value == null)
                        this.DisplayedRequest.Query.Add(Row.Cells[0].Value.ToString(), "");
                    else
                        this.DisplayedRequest.Query.Add(Row.Cells[0].Value.ToString(), Row.Cells[1].Value.ToString());
                }
                this.HeadersTBP.SetText(this.DisplayedRequest.GetHeadersAsStringWithoutFullURL());
                ResetHeadersChangedStatus();
                ResetQueryParametersChangedStatus();
            }
            if (CookieParametersChanged && this.DisplayedRequest != null)
            {
                this.DisplayedRequest.Cookie.RemoveAll();
                foreach (DataGridViewRow Row in CookieParametersGrid.Rows)
                {
                    if (Row.Cells[1].Value == null)
                        this.DisplayedRequest.Cookie.Add(Row.Cells[0].Value.ToString(), "");
                    else
                        this.DisplayedRequest.Cookie.Add(Row.Cells[0].Value.ToString(), Row.Cells[1].Value.ToString());
                }
                this.HeadersTBP.SetText(this.DisplayedRequest.GetHeadersAsStringWithoutFullURL());
                ResetHeadersChangedStatus();
                ResetCookieParametersChangedStatus();
            }
            if (HeadersParametersChanged && this.DisplayedRequest != null)
            {
                this.DisplayedRequest.Headers.RemoveAll();
                foreach (DataGridViewRow Row in HeadersParametersGrid.Rows)
                {
                    if (Row.Cells[1].Value == null)
                        this.DisplayedRequest.Headers.Add(Row.Cells[0].Value.ToString(), "");
                    else
                        this.DisplayedRequest.Headers.Add(Row.Cells[0].Value.ToString(), Row.Cells[1].Value.ToString());
                }
                this.HeadersTBP.SetText(this.DisplayedRequest.GetHeadersAsStringWithoutFullURL());
                ResetHeadersChangedStatus();
                ResetHeadersParametersChangedStatus();
            }
            if (BodyTypeNormalParametersChanged && this.DisplayedRequest != null)
            {
                this.DisplayedRequest.Body.RemoveAll();
                foreach (DataGridViewRow Row in BodyNormalTypeParametersGrid.Rows)
                {
                    if (Row.Cells[1].Value == null)
                        this.DisplayedRequest.Body.Add(Row.Cells[0].Value.ToString(), "");
                    else
                        this.DisplayedRequest.Body.Add(Row.Cells[0].Value.ToString(), Row.Cells[1].Value.ToString());
                }
                this.BodyTBP.SetText(this.DisplayedRequest.BodyString);
                ResetBodyChangedStatus();
                ResetBodyTypeNomarlParametersChangedStatus();
                ClearBodyTypeFormatPluginsUi();                
            }
            if (BodyTypeFormatPluginsParametersChanged && this.DisplayedRequest != null)
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
                if(PluginName.Length > 0)
                    SerializeNewParametersWithFormatPlugin(EditedNameValuePairs, PluginName);
                ResetBodyTypeFormatPluginsParametersChangedStatus();
            }
            if (UseSslChanged && this.DisplayedRequest != null)
            {
                this.DisplayedRequest.SSL = UseSSLCB.Checked;
                ResetSslChangedStatus();
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
            ResetSslChangedStatus();
            ResetHeadersChangedStatus();
            ResetBodyChangedStatus();
            ResetUrlPathPartsChangedStatus();
            ResetQueryParametersChangedStatus();
            ResetBodyTypeNomarlParametersChangedStatus();
            ResetBodyTypeFormatPluginsParametersChangedStatus();
            ResetBodyFormatXmlChangedStatus();
            ResetCookieParametersChangedStatus();
            ResetHeadersParametersChangedStatus();
        }
        void ResetSslChangedStatus()
        {
            UseSslChanged = false;
        }
        void ResetHeadersChangedStatus()
        {
            HeadersChanged = false;
        }
        void ResetBodyChangedStatus()
        {
            BodyChanged = false;
        }
        void ResetUrlPathPartsChangedStatus()
        {
            UrlPathPartsChanged = false;
        }
        void ResetQueryParametersChangedStatus()
        {
            QueryParametersChanged = false;
        }
        void ResetBodyTypeNomarlParametersChangedStatus()
        {
            BodyTypeNormalParametersChanged = false;
        }
        void ResetBodyTypeFormatPluginsParametersChangedStatus()
        {
            BodyTypeFormatPluginsParametersChanged = false;
        }
        void ResetBodyFormatXmlChangedStatus()
        {
            BodyFormatXmlChanged = false;
        }
        void ResetCookieParametersChangedStatus()
        {
            CookieParametersChanged = false;
        }
        void ResetHeadersParametersChangedStatus()
        {
            HeadersParametersChanged = false;
        }

        private void UseSSLCB_CheckedChanged(object sender, EventArgs e)
        {
            UseSslChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void HeadersTBP_ValueChanged()
        {
            HeadersChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void BodyTBP_ValueChanged()
        {
            BodyChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void UrlPathPartsParametersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UrlPathPartsChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void QueryParametersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            QueryParametersChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void CookieParametersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CookieParametersChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void HeadersParametersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            HeadersParametersChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void BodyNormalTypeParametersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            BodyTypeNormalParametersChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void BodyFormatPluginsParametersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            BodyTypeFormatPluginsParametersChanged = true;
            if (DisplayedRequest != null && RequestChanged != null)
                RequestChanged();
        }

        private void FormatXmlTBP_ValueChanged()
        {
            BodyFormatXmlChanged = true;
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

        private void FormatPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DisplayedRequest == null) return;
            string PluginName = "";
            CurrentFormatXml = "";
            CurrentXmlNameValueArray = new string[,]{};
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
            ShowStatusMsg(string.Format("Parsing Request body as {0}", PluginName));
            ShowProgressBar(true);
            FormatPluginCallingThread = new Thread(DeserializeWithFormatPlugin);
            FormatPluginCallingThread.Start(PluginName);
        }

        void DeserializeWithFormatPlugin(object PluginNameObject)
        {
            string PluginName = PluginNameObject.ToString();
            try
            {
                Request Req = DisplayedRequest.GetClone(true);
                FormatPlugin FP = FormatPlugin.Get(PluginName);
                CurrentFormatXml = FP.ToXmlFromRequest(Req);
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
                IronException.Report(string.Format("Error converting Request to {0}", PluginName), Exp);
                ShowErrorMsg(string.Format("Unable to parse Request body as {0}", PluginName));
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
                    int RowId = BodyFormatPluginsParametersGrid.Rows.Add(new object[] { XmlNameValueArray[i, 0], XmlNameValueArray[i, 1] });
                    BodyFormatPluginsParametersGrid.Rows[RowId].Cells[1].ReadOnly = this.ReadOnly;
                }
            }
        }

        void SerializeNewParametersWithFormatPlugin(string[,] EditedXmlNameValueArray, string PluginName)
        {
            for (int i = 0; i < this.CurrentXmlNameValueArray.GetLength(0); i++)
            {
                if(this.CurrentXmlNameValueArray[i,0].Equals(EditedXmlNameValueArray[i,0]))
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
                Request Req = DisplayedRequest.GetClone(true);
                FormatPlugin FP = FormatPlugin.Get(PluginName);
                Request NewRequest = FP.ToRequestFromXml(Req, CurrentFormatXml);
                this.DisplayedRequest = NewRequest;
                ShowStatusMsg("");
                this.SetNonFormatPluginRequestFields(NewRequest);
                ShowProgressBar(false);
            }
            catch (ThreadAbortException)
            {
                ShowStatusMsg("");
            }
            catch (Exception Exp)
            {
                IronException.Report(string.Format("Error converting {0} to Request", PluginName), Exp);
                ShowErrorMsg(string.Format("Unable to update edited values in {0}", PluginName));
                ShowProgressBar(false);
            }
        }
        delegate void SetNonFormatPluginRequestFields_d(Request Req);
        void SetNonFormatPluginRequestFields(Request Req)
        {
            if (this.BaseTabs.InvokeRequired)
            {
                SetNonFormatPluginRequestFields_d InvokeDelegate_d = new SetNonFormatPluginRequestFields_d(SetNonFormatPluginRequestFields);
                this.BaseTabs.Invoke(InvokeDelegate_d, new object[] { Req });
            }
            else
            {
                this.SetHeader(Req);
                this.SetBody(Req);
                this.SetUrlPathPartsParameters(Req);
                this.SetQueryParameters(Req);
                this.SetBodyParameters(Req);
                this.SetCookieParameters(Req);
                this.SetHeadersParameters(Req);
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

        private void BaseTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                this.HandleAllDataChanges();
            }
            catch (Exception Exp) { ShowErrorMsg(Exp.Message); }
        }

        private void BodyParametersFormatTypeTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                this.HandleAllDataChanges();
            }
            catch (Exception Exp) { ShowErrorMsg(Exp.Message); }
        }

        private void RequestView_Load(object sender, EventArgs e)
        {
            FormatPluginsGrid.Rows.Clear();
            foreach (string Name in FormatPlugin.List())
            {
                FormatPluginsGrid.Rows.Add(new object[]{false, Name});
            }
        }
        
    }
}
