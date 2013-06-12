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
using System.IO;

namespace IronWASP
{
    public partial class ScriptCreationAssistant : Form
    {
        string FullPyCode = "";
        string FullRbCode = "";

        public ScriptCreationAssistant()
        {
            InitializeComponent();
        }

        private void CRLogSourceGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CRLogSourceGrid.SelectedRows == null) return;
            if (CRLogSourceGrid.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow Row in CRLogSourceGrid.Rows)
            {
                if (Row.Index == CRLogSourceGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void CRFromUrlRB_CheckedChanged(object sender, EventArgs e)
        {
            if (CRFromUrlRB.Checked)
            {
                CRFromUrlPanel.Visible = true;
                CRFromLogPanel.Visible = false;
            }
        }

        private void CRFromLogRB_CheckedChanged(object sender, EventArgs e)
        {
            if (CRFromLogRB.Checked)
            {
                CRFromLogPanel.Visible = true;
                CRFromUrlPanel.Visible = false;
            }
        }

        private void CRIncludeRequestBodyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (CRIncludeRequestBodyCB.Checked)
            {
                CRRequestBodyTB.Enabled = true;
            }
            else
            {
                CRRequestBodyTB.Enabled = false;
            }
        }

        private void CRCreateCodeBtn_Click(object sender, EventArgs e)
        {
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#Request is created based on your requirement and stored in a variable named 'req'");
            Rb.AppendLine("#Request is created based on your requirement and stored in a variable named 'req'");

            ShowCRError("");

            if (!(CRFromUrlRB.Checked || CRFromLogRB.Checked))
            {
                ShowCRError("Select atleast one option");
                return;
            }

            if (CRFromUrlRB.Checked)
            {
                try
                {
                    new Request(CRRequestUrlTB.Text);
                    if (CRIncludeRequestBodyCB.Checked)
                    {
                        Py.AppendLine(string.Format(@"req = Request(""POST"", ""{0}"", ""{1}"")", CRRequestUrlTB.Text.Replace("\"", "\\\""), CRRequestBodyTB.Text.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req = Request.new(""POST"", ""{0}"", ""{1}"")", CRRequestUrlTB.Text.Replace("\"", "\\\""), CRRequestBodyTB.Text.Replace("\"", "\\\"")));
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req = Request(""{0}"")", CRRequestUrlTB.Text.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req = Request.new(""{0}"")", CRRequestUrlTB.Text.Replace("\"", "\\\"")));
                    }
                }
                catch
                {
                    ShowCRError("Invalid Request Url. Url must start with http:// or https://. Eg: http://ironwasp.org/index.html");
                    return;
                }
            }

            string LogSource = "";
            int LogId = 0;

            if (CRFromLogRB.Checked)
            {
                foreach (DataGridViewRow Row in CRLogSourceGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        LogSource = Row.Cells[1].Value.ToString();
                        break;
                    }
                }
                if (LogSource.Length == 0)
                {
                    ShowCRError("Log Source has not been selected.");
                    return;
                }
                try
                {
                    LogId = Int32.Parse(CRLogIdTB.Text.Trim());
                }
                catch
                {
                    ShowCRError("Log Id must be a valid number");
                    return;
                }
                switch (LogSource)
                {
                    case("Proxy"):
                        Py.AppendLine(string.Format(@"req = Request.FromProxyLog({0})", LogId));
                        Rb.AppendLine(string.Format(@"req = Request.from_proxy_log({0})", LogId));
                        break;
                    case ("Probe"):
                        Py.AppendLine(string.Format(@"req = Request.FromProbeLog({0})", LogId));
                        Rb.AppendLine(string.Format(@"req = Request.from_probe_log({0})", LogId));
                        break;
                    case ("Shell"):
                        Py.AppendLine(string.Format(@"req = Request.FromShellLog({0})", LogId));
                        Rb.AppendLine(string.Format(@"req = Request.from_shell_log({0})", LogId));
                        break;
                    case ("Scan"):
                        Py.AppendLine(string.Format(@"req = Request.FromScanLog({0})", LogId));
                        Rb.AppendLine(string.Format(@"req = Request.from_scan_log({0})", LogId));
                        break;
                    case ("Test"):
                        Py.AppendLine(string.Format(@"req = Request.FromTestLog({0})", LogId));
                        Rb.AppendLine(string.Format(@"req = Request.from_test_log({0})", LogId));
                        break;
                    default:
                        Py.AppendLine(string.Format(@"req = Request.FromLog({0}, ""{1}"")", LogId, LogSource));
                        Rb.AppendLine(string.Format(@"req = Request.from_log({0}, ""{1}"")", LogId, LogSource));
                        break;
                }
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }

        void ShowCRError(string Error)
        {
            CRErrorTB.Text = Error;
            if(Error.Length > 0)
                CRErrorTB.Visible = true;
            else
                CRErrorTB.Visible = false;
        }
        void ShowSRError(string Error)
        {
            SRErrorTB.Text = Error;
            if (Error.Length > 0)
                SRErrorTB.Visible = true;
            else
                SRErrorTB.Visible = false;
        }
        void ShowRPPError(string Error)
        {
            RPPErrorTB.Text = Error;
            if (Error.Length > 0)
                RPPErrorTB.Visible = true;
            else
                RPPErrorTB.Visible = false;
        }
        void ShowROPError(string Error)
        {
            ROPErrorTB.Text = Error;
            if (Error.Length > 0)
                ROPErrorTB.Visible = true;
            else
                ROPErrorTB.Visible = false;
        }
        void ShowResError(string Error)
        {
            ResErrorTB.Text = Error;
            if (Error.Length > 0)
                ResErrorTB.Visible = true;
            else
                ResErrorTB.Visible = false;
        }
        void ShowHtmlError(string Error)
        {
            HtmlErrorTB.Text = Error;
            if (Error.Length > 0)
                HtmlErrorTB.Visible = true;
            else
                HtmlErrorTB.Visible = false;
        }
        void ShowLogError(string Error)
        {
            LogErrorTB.Text = Error;
            if (Error.Length > 0)
                LogErrorTB.Visible = true;
            else
                LogErrorTB.Visible = false;
        }

        void ShowCode(string PyCode, string RbCode)
        {
            FullPyCode = PyCode;
            FullRbCode = RbCode;

            if (ShowHideCommentsLL.Text.Equals("Show Comments"))
            {
                string[] StrippedCode = StripComments(new string[] { PyCode, RbCode });
                PythonCTB.Text = StrippedCode[0];
                RubyCTB.Text = StrippedCode[1];
            }
            else
            {
                PythonCTB.Text = PyCode;
                RubyCTB.Text = RbCode;
            }
        }

        private void ScriptCreationAssistant_Load(object sender, EventArgs e)
        {
            List<string> LogSourcesList = new List<string>() { "Proxy", "Probe", "Shell", "Scan", "Test" };
            LogSourcesList.AddRange(Config.GetOtherSourceList());

            CRLogSourceGrid.Rows.Clear();
            LogSourceGrid.Rows.Clear();
            
            foreach (string LogSource in LogSourcesList)
            {
                CRLogSourceGrid.Rows.Add(new object[]{ false, LogSource });
                LogSourceGrid.Rows.Add(new object[] { false, LogSource });
            }

            RPPParameterTypeGrid.Rows.Clear();
            RPPParameterTypeGrid.Rows.Add(new object[]{false, "UrlPathParts", "These are the path section of the Request's Url. If the server uses URL-Rewriting then these could hold parameter values instead of file path."});
            RPPParameterTypeGrid.Rows.Add(new object[]{false, "Query", "Request Query Parameters"});
            RPPParameterTypeGrid.Rows.Add(new object[]{false, "Body", "Request Body Parameters"});
            RPPParameterTypeGrid.Rows.Add(new object[]{false, "Cookie", "Cookie values sent to the server along with the Request"});
            RPPParameterTypeGrid.Rows.Add(new object[]{false, "Headers", "The various Headers sent by the browser along with the Request"});

            ROPParameterTypeGrid.Rows.Clear();
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "Method", "full", "<method>", "HTTP method used by the request" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "SSL", "url", "<ssl>", "Boolean value indicating if Request uses SSL" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "Host", "full", "<host>", "Target hostname or IP the request will be sent to." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "File", "full", "<file>", "File extension in the Url" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "FullUrl", "url", "<full_url>", "Full url along with hostname and protocol." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "Url", "url", "<url>", "Url without hostname." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "BaseUrl", "url", "<base_url>", "Protocol and hostname." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "UrlPath", "full", "<url_path>", "Url without the querystring" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "UrlDir", "full", "<url_dir>", "UrlPath without the filename" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "BodyString", "full", "<body_string>", "The full request body as a single string." });
            //ROPParameterTypeGrid.Rows.Add(new object[] { false, "BodyArray", "full", "<body_string>", "The request body as a .NET byte array. Use this to handle binary body data." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "CookieString", "full", "<cookie_string>", "The value of the Cookie header in the request as a single string." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "BodyLength", "full", "<content_length>", "Length of the request body" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "ContentType", "full", "<content_type>", "Value of the Content-Type header" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "HasBody", "full", "", "Boolean value indicating if the request contains a body" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "IsBinary", "full", "", "Boolean value indicating if the request body is a binary value" });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "UrlPathParts", "full", "<upp>", "Check the 'Read or Modify Parameters' section for more details on this." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "Query Parameters", "full", "<q>", "Check the 'Read or Modify Parameters' section for more details on this." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "Body Parameters", "full", "<b>", "Check the 'Read or Modify Parameters' section for more details on this." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "Cookie Parameters", "full", "<c>", "Check the 'Read or Modify Parameters' section for more details on this." });
            ROPParameterTypeGrid.Rows.Add(new object[] { false, "Header Parameters", "full", "<h>", "Check the 'Read or Modify Parameters' section for more details on this." });

            ResParameterTypeGrid.Rows.Clear();
            ResParameterTypeGrid.Rows.Add(new object[] { false, "Code", "<code>", "The HTTP response code number." });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "Status", "<status>", "The message in the HTTP response code." });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "HttpVersion", "<version>", "HTTP version used." });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "BodyLength", "<len>", "Length of the Response body" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "ContentType", "<type>", "Value of the Content-Type header" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "BodyEncoding", "<char>", "The encoding scheme for the page. If the response header does not contain one then its is taken from the HTML. If no value is given it defaults to UTF-8." });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "SetCookies", "<sc>", "The collection of the SetCookie headers sent by the server." });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "Html", "<b>", "The parsed HTML of the response. Check the 'Analyze Html' for more details on this." });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "Headers", "<h>", "The headers in the Response. This property is similar to the Request Headers property. Check that section for usage information." });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "BodyString", "<b>", "The entire response body as a single string." });
            //ResParameterTypeGrid.Rows.Add(new object[] { false, "BodyArray", "<b>", "" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "IsHtml", "", "Boolean value indicating if the response body is a valid HTML" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "IsJson", "", "Boolean value indicating if the response body is a valid JSON" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "IsXml", "", "Boolean value indicating if the response body is a valid XML" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "IsJavaScript", "", "Boolean value indicating if the response body is a valid JavaScript" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "IsCss", "", "Boolean value indicating if the response body is a valid CSS" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "RoundTrip", "", "Time taken to get the response form the server in milliseconds" });
            ResParameterTypeGrid.Rows.Add(new object[] { false, "IsRedirect", "", "Boolean value indicating if the response is a redirect." });

            HtmlMainActionsGrid.Rows.Clear();
            HtmlMainActionsGrid.Rows.Add(new object[]{ false, "Get Title of Html", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get Links from Html", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get Comments from Html", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get JavaScript from Html", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get VisualBasic from Html", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get CSS from Html", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get Values of Html attributes", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get Elements from Html as strings", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get Elements from Html as objects", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get Forms from Html", "" });
            //HtmlMainActionsGrid.Rows.Add(new object[] { false, "Get Content of Meta tags", "" });
            HtmlMainActionsGrid.Rows.Add(new object[] { false, "Find the context a keyword", "" });

            ToolsItemGrid.Rows.Clear();
            ToolsItemGrid.Rows.Add(new object[] { false, "Find the location of IronWASP executable", "path"});
            ToolsItemGrid.Rows.Add(new object[] { false, "Perform Base64 encode and decode", "base64" });
            ToolsItemGrid.Rows.Add(new object[] { false, "Perform Hex encode and decode", "hex" });
            ToolsItemGrid.Rows.Add(new object[] { false, "Perform HTML encode and decode", "html" });
            ToolsItemGrid.Rows.Add(new object[] { false, "Perform URL encode and decode", "url" });
            ToolsItemGrid.Rows.Add(new object[] { false, "Create MD5, SHA1, SHA256, SHA384 or SHA512 hash", "hash" });
            ToolsItemGrid.Rows.Add(new object[] { false, "Compare two strings", "diff" });
            ToolsItemGrid.Rows.Add(new object[] { false, "Run external executable files", "exec" });
            ToolsItemGrid.Rows.Add(new object[] { false, "Debug Plugins, Modules etc", "debug" });

            ReqOActionsGrid.Rows.Clear();
            ReqOActionsGrid.Rows.Add(new object[] { false, "Create a copy or clone of a Request", "GetClone", "Creates a new identical copy of the request object so that you can modify the clone without affecting the original." });
            ReqOActionsGrid.Rows.Add(new object[] { false, "Convert this Request as a string", "ToBinaryString", "Convert the entire Request to a string value. Can also get the request object back from this string. Useful to embed requests in scripts." });
            ReqOActionsGrid.Rows.Add(new object[] { false, "Send Request to Manual Testing", "ToTestUi", "Send a Request from the Scripting shell to the ManualTesting section UI. A new group is created and the request is displayed there." });

            ResOActionsGrid.Rows.Clear();
            ResOActionsGrid.Rows.Add(new object[] { false, "Render the Response Body", "Render", "Renders the Response body content in a seperate UI window using IE's rendering engine." });
            ResOActionsGrid.Rows.Add(new object[] { false, "Save the Response Body to a file", "Save", "Save the entire body of the response to a file on the system." });

            FuzzParameterTypeGrid.Rows.Clear();
            FuzzParameterTypeGrid.Rows.Add(new object[] { false, "UrlPathParts" });
            FuzzParameterTypeGrid.Rows.Add(new object[] { false, "Query" });
            FuzzParameterTypeGrid.Rows.Add(new object[] { false, "Body" });
            FuzzParameterTypeGrid.Rows.Add(new object[] { false, "Cookie" });
            FuzzParameterTypeGrid.Rows.Add(new object[] { false, "Headers" });

            FuzzSessionPluginGrid.Rows.Clear();
            FuzzSessionPluginGrid.Rows.Add(new object[] { true, "---" });
            foreach (string Name in SessionPlugin.List())
            {
                FuzzSessionPluginGrid.Rows.Add(new object[] { false, Name });
            }

            ScanParameterTypeGrid.Rows.Clear();
            ScanParameterTypeGrid.Rows.Add(new object[] { false, "UrlPathParts" });
            ScanParameterTypeGrid.Rows.Add(new object[] { false, "Query" });
            ScanParameterTypeGrid.Rows.Add(new object[] { false, "Body" });
            ScanParameterTypeGrid.Rows.Add(new object[] { false, "Cookie" });
            ScanParameterTypeGrid.Rows.Add(new object[] { false, "Headers" });

            ScanSessionPluginGrid.Rows.Clear();
            ScanSessionPluginGrid.Rows.Add(new object[] { true, "---" });
            foreach (string Name in SessionPlugin.List())
            {
                ScanSessionPluginGrid.Rows.Add(new object[] { false, Name });
            }

            ScanPluginsGrid.Rows.Clear();
            foreach (string Name in ActivePlugin.List())
            {
                ScanPluginsGrid.Rows.Add(new object[] { false, Name });
            }

            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
            string IntroMessage = @"<i<h1>><i<h>>Welcome to the Script Creation Assistant<i</h>><i</h1>>
<i<br>><i<br>>
You don't have to be an expert to do scripting with IronWASP. If you just know the very basics of programming like what are variables, methods, for loops etc then you are good to go. This assistant will help you learn how to create scripts using the IronWASP API and turn your ideas in to working code.
<i<br>><i<br>>
You can use the Scripting Shell built in to IronWASP to run these scripts.
<i<br>><i<br>>
There are different sections here, in each section you define what you are trying to do and the script to perform that action will be created for you when you click on the 'Generate Code' button.
<i<br>><i<br>>
You can put the code from various sections together to create the scripts to fit your unique needs.
<i<br>><i<br>>
Let us look at some scenarios:
<i<br>><i<br>>
You want to analyze the HTTP logs to identify certain issues:<i<br>>
  1: Use the script from the 'Log Analysis' section to read through the logs one at a time<i<br>>
  2: Use the Request and Response sections to find out how to read the different properties and values from the Request and Response of the log and analyze them.<i<br>>
<i<br>>
You want to create a Password cracker for a site you are testing:<i<br>>
  1: Use the Request section to learn how to create and send the login request<i<br>>
  2: The Request section will also show you how to change the values of the username and password parameters<i<br>>
  3: The Response section will show you how to read the response properties like status code, headers and how to extract data from the various HTML elements<i<br>>
<i<br>>
You want to create a custom fuzzer or scanner to check for an issue:<i<br>>
  1: Use the Request section to create a request object to fuzz or test<i<br>>
  2: Use the Fuzzer section to create sample script that shows how you can inject your payloads in different sections of this request and get the response<i<br>>
  3: Use the Response section to learn how you can analyze this response and identify if your payload had any effect<i<br>>
";
            SB.AppendLine(Tools.RtfSafe(IntroMessage));
            IntroRTB.Rtf = SB.ToString();
        }

        private void SRCreateCodeBtn_Click(object sender, EventArgs e)
        {
            ShowSRError("");
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Rb.AppendLine("#'req' is a variable that is assumed to contain a Request object");

            if (SRSendWithLogSourceRB.Checked)
            {
                try
                {
                    Request Req = new Request("http://google.com");
                    Req.SetSource(SRLogSourceTB.Text);
                    string LogSource = SRLogSourceTB.Text;
                    Py.AppendLine("#The LogSource is set");
                    Rb.AppendLine("#The LogSource is set");

                    Py.AppendLine(string.Format(@"req.SetSource(""{0}"")", LogSource));
                    Rb.AppendLine(string.Format(@"req.set_source(""{0}"")", LogSource));
                }
                catch(Exception Exp) 
                {
                    ShowSRError(Exp.Message);
                }
            }
            Py.AppendLine("#Request is sent and the response stored in a variable named 'res'");
            Rb.AppendLine("#Request is sent and the response stored in a variable named 'res'");

            Py.AppendLine("res = req.Send()");
            Rb.AppendLine("res = req.send_req");

            if (SRFollowRedirectRB.Checked)
            {
                Py.AppendLine("#Check if the response is a redirect");
                Py.AppendLine("if res.IsRedirect:");
                Py.Append("  "); Py.AppendLine("#Get the redirect Request and store it in a variable named 'rd_req'. The redirect is followed by sending 'rd_req'");
                Py.Append("  "); Py.AppendLine("rd_req = req.GetRedirect(res)");
                Py.Append("  "); Py.AppendLine("final_res = rd_req.Send()");

                Rb.AppendLine("#Check if the response is a redirect");
                Rb.AppendLine("if res.is_redirect");
                Rb.Append("  "); Rb.AppendLine("#Get the redirect Request and store it in a variable named 'rd_req'. The redirect is followed by sending 'rd_req'");
                Rb.Append("  "); Rb.AppendLine("rd_req = req.get_redirect(res)");
                Rb.Append("  "); Rb.AppendLine("final_res = rd_req.send_req");
                Rb.AppendLine("end");
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void ShowHideCommentsLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ShowHideCommentsLL.Text.Equals("Show Comments"))
            {
                PythonCTB.Text = FullPyCode;
                RubyCTB.Text = FullRbCode;
                ShowHideCommentsLL.Text = "Hide Comments";
            }
            else
            {
                string[] StrippedCode = StripComments(new string[] { FullPyCode, FullRbCode });
                PythonCTB.Text = StrippedCode[0];
                RubyCTB.Text = StrippedCode[1];
                ShowHideCommentsLL.Text = "Show Comments";
            }
        }

        string[] StripComments(string[] FullCode)
        {
            string[] StrippedCode = new string[2];
            for(int i =0; i < 2; i++)
            {
                StringBuilder Code = new StringBuilder();
                foreach (string Line in FullCode[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (!Line.TrimStart().StartsWith("#")) Code.AppendLine(Line);
                }
                StrippedCode[i] = Code.ToString();
            }
            return StrippedCode;
        }

        private void RPPParameterTypeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RPPParameterTypeGrid.SelectedRows == null) return;
            if (RPPParameterTypeGrid.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow Row in RPPParameterTypeGrid.Rows)
            {
                if (Row.Index == RPPParameterTypeGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    RPPParameterDescriptionTB.Text = Row.Cells[2].Value.ToString();
                    if (Row.Cells[1].Value.ToString().Equals("UrlPathParts"))
                    {
                        RPPActionGrid.Rows.Clear();
                        RPPActionGrid.Rows.Add(new object[] { false, "Read all url path part values" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Edit an url path part value" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Add a new url path part value" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Get the number of url path part values" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Remove an url path part value" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Remove all url path part values" });
                    }
                    else
                    {
                        RPPActionGrid.Rows.Clear();
                        RPPActionGrid.Rows.Add(new object[] { false, "Read all parameter names" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Read a parameter's value" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Edit a parameter's value" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Add a new parameter" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Contains a parameter" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Get the number of parameters" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Remove a parameter" });
                        RPPActionGrid.Rows.Add(new object[] { false, "Remove all parameters" });

                    }

                    RPPActionGrid.Visible = true;
                    RPPQuestionGB.Visible = false;
                    RPPParameterNameLbl.Visible = false;
                    RPPParameterNameTB.Visible = false;
                    RPPParameterValueLbl.Visible = false;
                    RPPParameterValueTB.Visible = false;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void RPPActionGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RPPActionGrid.SelectedRows == null) return;
            if (RPPActionGrid.SelectedRows.Count == 0) return;
            string ParameterType = "";
            foreach (DataGridViewRow Row in RPPParameterTypeGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    ParameterType = Row.Cells[1].Value.ToString();
                    break;
                }
            }
            foreach (DataGridViewRow Row in RPPActionGrid.Rows)
            {
                if (Row.Index == RPPActionGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    if (Row.Cells[1].Value.ToString().StartsWith("Read all"))
                    {
                        RPPQuestionGB.Visible = false;
                        RPPParameterNameLbl.Visible = false;
                        RPPParameterNameTB.Visible = false;
                        RPPParameterValueLbl.Visible = false;
                        RPPParameterValueTB.Visible = false;
                    }
                    else if (Row.Cells[1].Value.ToString().StartsWith("Read a "))
                    {                      
                        RPPQuestionGB.Visible = true;
                        RPPQuestionGB.Text = "If there are more than one parameter values with this name then what do you wish to do?";
                        RPPAnswerOneRB.Text = "Get only the first value";
                        RPPAnswerTwoRB.Text = "Get all values in a list";
                        RPPAnswerOneRB.Checked = true;

                        RPPParameterValueLbl.Visible = false;
                        RPPParameterValueTB.Visible = false;

                        RPPParameterNameLbl.Text = "Enter Parameter Name:";
                        RPPParameterNameLbl.Visible = true;
                        RPPParameterNameTB.Visible = true;
                    }
                    else if (Row.Cells[1].Value.ToString().StartsWith("Edit a"))
                    {
                        if (ParameterType.Equals("UrlPathParts"))
                        {
                            RPPQuestionGB.Visible = false;

                            RPPParameterValueLbl.Visible = true;
                            RPPParameterValueTB.Visible = true;

                            RPPParameterNameLbl.Text = "UrlPathPart Position";
                                                        
                            RPPParameterNameLbl.Visible = true;
                            RPPParameterNameTB.Visible = true;
                        }
                        else
                        {
                            RPPQuestionGB.Visible = false;
                            RPPParameterNameLbl.Text = "Enter Parameter Name:";
                            RPPParameterNameLbl.Visible = true;
                            RPPParameterNameTB.Visible = true;
                            RPPParameterValueLbl.Visible = true;
                            RPPParameterValueTB.Visible = true;
                        }
                    }
                    else if (Row.Cells[1].Value.ToString().StartsWith("Contains a "))
                    {
                        RPPQuestionGB.Visible = false;
                        RPPParameterValueLbl.Visible = false;
                        RPPParameterValueTB.Visible = false;
                        
                        RPPParameterNameLbl.Text = "Enter Parameter Name:";
                        RPPParameterNameLbl.Visible = true;
                        RPPParameterNameTB.Visible = true;
                    }
                    else if (Row.Cells[1].Value.ToString().StartsWith("Add a "))
                    {
                        if (ParameterType.Equals("UrlPathParts"))
                        {
                            RPPQuestionGB.Visible = false;

                            RPPParameterValueLbl.Visible = true;
                            RPPParameterValueTB.Visible = true;

                            RPPParameterNameLbl.Visible = false;
                            RPPParameterNameTB.Visible = false;
                        }
                        else
                        {
                            RPPQuestionGB.Visible = true;
                            RPPQuestionGB.Text = "If a parameter with this name already exists then what do you wish to do?";
                            RPPAnswerOneRB.Text = "Overwrite it";
                            RPPAnswerTwoRB.Text = "Add another parameter with same name";
                            RPPAnswerOneRB.Checked = true;

                            RPPParameterNameLbl.Text = "Enter Parameter Name:";
                            RPPParameterNameLbl.Visible = true;
                            RPPParameterNameTB.Visible = true;
                            RPPParameterValueLbl.Visible = true;
                            RPPParameterValueTB.Visible = true;
                        }
                    }
                    else if (Row.Cells[1].Value.ToString().StartsWith("Get the"))
                    {
                        RPPQuestionGB.Visible = false;
                        RPPParameterNameLbl.Visible = false;
                        RPPParameterNameTB.Visible = false;
                        RPPParameterValueLbl.Visible = false;
                        RPPParameterValueTB.Visible = false;
                    }
                    else if (Row.Cells[1].Value.ToString().StartsWith("Remove a "))
                    {
                        if (ParameterType.Equals("UrlPathParts"))
                        {
                            RPPQuestionGB.Visible = false;

                            RPPParameterValueLbl.Visible = false;
                            RPPParameterValueTB.Visible = false;

                            RPPParameterNameLbl.Text = "UrlPathPart Position";

                            RPPParameterNameLbl.Visible = true;
                            RPPParameterNameTB.Visible = true;
                        }
                        else
                        {
                            RPPQuestionGB.Visible = false;
                            RPPParameterValueLbl.Visible = false;
                            RPPParameterValueTB.Visible = false;

                            RPPParameterNameLbl.Text = "Enter Parameter Name:";
                            RPPParameterNameLbl.Visible = true;
                            RPPParameterNameTB.Visible = true;
                        }
                    }
                    else if (Row.Cells[1].Value.ToString().StartsWith("Remove all"))
                    {
                        RPPQuestionGB.Visible = false;
                        RPPParameterNameLbl.Visible = false;
                        RPPParameterNameTB.Visible = false;
                        RPPParameterValueLbl.Visible = false;
                        RPPParameterValueTB.Visible = false;
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void RPPCreateCodeBtn_Click(object sender, EventArgs e)
        {
            ShowRPPError("");
            if (!RPPActionGrid.Visible)
            {
                ShowRPPError("No parameter sections were selected.");
                return;
            }
            string ParameterType = "";
            foreach (DataGridViewRow Row in RPPParameterTypeGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    ParameterType = Row.Cells[1].Value.ToString();
                    break;
                }
            }
            if (!RPPActionGrid.Visible)
            {
                ShowRPPError("No parameter sections were selected.");
                return;
            }
            string Action = "";
            foreach (DataGridViewRow Row in RPPActionGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Action = Row.Cells[1].Value.ToString();
                    break;
                }
            }
            if (RPPParameterNameTB.Visible)
            {
                if(RPPParameterNameTB.Text.Trim().Length == 0)
                {
                    ShowRPPError("Parameter name cannot be empty");
                    return;
                }
                if (ParameterType.Equals("UrlPathParts"))
                {
                    try
                    {
                        Int32.Parse(RPPParameterNameTB.Text.Trim());
                    }
                    catch 
                    {
                        ShowRPPError("UrlPathPart position must be a number");
                        return;
                    }
                }
            }
             
            string ParameterName = RPPParameterNameTB.Text;
            string ParameterValue = RPPParameterValueTB.Text;
            
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Rb.AppendLine("#'req' is a variable that is assumed to contain a Request object");

            if (Action.StartsWith("Remove all"))
            {
                if (ParameterType.Equals("UrlPathParts"))
                {
                    Py.AppendLine("#UrlPathParts is a .NET List type so to remove all url path parts we get the list value, clear it and assign the empty list to the UrlPathParts property.");
                    Py.AppendLine("upp = req.UrlPathParts");
                    Py.AppendLine("upp.Clear()");
                    Py.AppendLine("req.UrlPathParts = upp");

                    Py.AppendLine("#url_path_parts is a .NET List type so to remove all url path parts we get the list value, clear it and assign the empty list to the url_path_parts property.");
                    Rb.AppendLine("upp = req.url_path_parts");
                    Rb.AppendLine("upp.clear");
                    Rb.AppendLine("req.url_path_parts = upp");
                }
                else
                {
                    Py.AppendLine(string.Format("req.{0}.RemoveAll()", ParameterType));
                    Rb.AppendLine(string.Format("req.{0}.remove_all", ParameterType.ToLower()));
                }
            }
            else if (Action.StartsWith("Get the "))
            {
                if (ParameterType.Equals("UrlPathParts"))
                {
                    Py.AppendLine("print 'UrlPathParts count - ' + str(req.UrlPathParts.Count)");
                    Rb.AppendLine("puts 'UrlPathParts count - ' + req.url_path_parts.count.to_s");
                }
                else
                {
                    Py.AppendLine(string.Format("print '{0} parameters count - ' +  str(req.{0}.Count)", ParameterType));
                    Rb.AppendLine(string.Format("puts '{0} parameters count - ' +  req.{0}.count.to_s", ParameterType.ToLower()));
                }
            }
            else if (Action.StartsWith("Remove a"))
            {
                if (ParameterType.Equals("UrlPathParts"))
                {
                    Py.AppendLine("#UrlPathParts is a .NET List type so we use the RemoveAt method of the .NET list class to remove the item");
                    Py.AppendLine("upp = req.UrlPathParts");
                    Py.AppendLine(string.Format("upp.RemoveAt({1})", ParameterName));
                    Py.AppendLine("req.UrlPathParts = upp");

                    Py.AppendLine("#UrlPathParts is a .NET List type but we can still use the delete_at method to remove the item");
                    Rb.AppendLine("upp = req.url_path_parts");
                    Rb.AppendLine(string.Format("upp.delete_at({0})", ParameterName));
                    Rb.AppendLine("req.url_path_parts = upp");
                }
                else
                {
                    Py.AppendLine(string.Format(@"req.{0}.Remove(""{1}"")", ParameterType, ParameterName.Replace("\"", "\\\"")));
                    Rb.AppendLine(string.Format(@"req.{0}.remove(""{1}"")", ParameterType.ToLower(), ParameterName.Replace("\"", "\\\"")));
                }
            }
            else if (Action.StartsWith("Contains a"))
            {
                Py.AppendLine(string.Format(@"if req.{0}.Has(""{1}""):", ParameterType, ParameterName.Replace("\"", "\\\"")));
                Py.Append("  "); Py.AppendLine(string.Format(@"print ""{0} contains a parameter named '{1}'""", ParameterType, ParameterName.Replace("'", "\\'")));
                Py.AppendLine("else:");
                Py.Append("  "); Py.AppendLine(string.Format(@"print ""{0} does not contain a parameter named '{1}'""", ParameterType, ParameterName.Replace("'", "\\'")));
                
                Rb.AppendLine(string.Format(@"if req.{0}.has(""{1}"")", ParameterType.ToLower(), ParameterName));
                Rb.Append("  "); Rb.AppendLine(string.Format(@"puts ""{0} contains a parameter named '{1}'""", ParameterType.ToLower(), ParameterName.Replace("'", "\\'")));
                Rb.AppendLine("else");
                Rb.Append("  "); Rb.AppendLine(string.Format(@"puts ""{0} does not contain a parameter named '{1}'""", ParameterType.ToLower(), ParameterName.Replace("'", "\\'")));
                Rb.AppendLine("end");
            }
            else if (Action.StartsWith("Read all"))
            {
                if (ParameterType.Equals("UrlPathParts"))
                {
                    Py.AppendLine("upp = req.UrlPathParts");
                    Py.AppendLine("#UrlPathParts is a .NET List type but you can iterate over it like Python list.");
                    Py.AppendLine("for v in upp:");
                    Py.Append("  "); Py.AppendLine("print v");

                    Rb.AppendLine("upp = req.url_path_parts");
                    Py.AppendLine("#UrlPathParts is a .NET List type but you can iterate over it like Ruby list.");
                    Rb.AppendLine("for v in upp");
                    Rb.Append("  "); Rb.AppendLine("puts v");
                    Rb.AppendLine("end");
                }
                else
                {
                    Py.AppendLine(string.Format(@"names = req.{0}.GetNames()", ParameterType));
                    Py.AppendLine("#The names are returned as a .NET List type but you can iterate over it like Python list.");
                    Py.AppendLine("for n in names:");
                    Py.Append("  "); Py.AppendLine("print n");

                    Rb.AppendLine(string.Format(@"names = req.{0}.get_names", ParameterType.ToLower()));
                    Rb.AppendLine("#The names are returned as a .NET List type but you can iterate over it like Ruby list.");
                    Rb.AppendLine("for n in names");
                    Rb.Append("  "); Rb.AppendLine("puts n");
                    Rb.AppendLine("end");
                }
            }
            else if (Action.StartsWith("Read a"))
            {
                if (RPPAnswerOneRB.Checked)
                {
                    Py.AppendLine(string.Format(@"value = req.{0}.Get(""{1}"")", ParameterType, ParameterName.Replace("\"", "\\\"")));

                    Rb.AppendLine(string.Format(@"value = req.{0}.get(""{1}"")", ParameterType.ToLower(), ParameterName.Replace("\"", "\\\"")));
                }
                else
                {
                    Py.AppendLine(string.Format(@"values = req.{0}.GetAll(""{1}"")", ParameterType, ParameterName.Replace("\"", "\\\"")));
                    Py.AppendLine("#The values are returned as a .NET List type but you can iterate over it like Python list.");
                    Py.AppendLine("for v in values:");
                    Py.Append("  "); Py.AppendLine("print v");

                    Rb.AppendLine(string.Format(@"values = req.{0}.get_all(""{1}"")", ParameterType.ToLower(), ParameterName.Replace("\"", "\\\"")));
                    Py.AppendLine("#The values are returned as a .NET List type but you can iterate over it like Ruby list.");
                    Rb.AppendLine("for v in values");
                    Rb.Append("  "); Rb.AppendLine("puts v");
                    Rb.AppendLine("end");
                }
            }
            else if (Action.StartsWith("Edit a"))
            {
                if (ParameterType.Equals("UrlPathParts"))
                {
                    Py.AppendLine("upp = req.UrlPathParts");
                    Py.AppendLine(string.Format(@"upp[{0}] = ""{1}""", ParameterName, ParameterValue.Replace("\"", "\\\"")));
                    Py.AppendLine("req.UrlPathParts = upp");

                    Rb.AppendLine("upp = req.url_path_parts");
                    Rb.AppendLine(string.Format(@"upp[{0}] = ""{1}""", ParameterName, ParameterValue.Replace("\"", "\\\"")));
                    Rb.AppendLine("req.url_path_parts = upp");
                }
                else
                {
                    Py.AppendLine(string.Format(@"req.{0}.Set(""{1}"", ""{2}"")", ParameterType, ParameterName.Replace("\"", "\\\""), ParameterValue.Replace("\"", "\\\"")));
                    Py.AppendLine("#If there were more than one parameter with the same name then use SetAt(name, position, value) method.");
                    Py.AppendLine(string.Format(@"#Eg: req.{0}.SetAt(""{1}"", 0, ""{2}"")", ParameterType, ParameterName.Replace("\"", "\\\""), ParameterValue.Replace("\"", "\\\"")));

                    Rb.AppendLine(string.Format(@"req.{0}.set(""{1}"", ""{2}"")", ParameterType.ToLower(), ParameterName, ParameterValue));
                    Rb.AppendLine("#If there were more than one parameter with the same name then use set_at(name, position, value) method.");
                    Rb.AppendLine(string.Format(@"# Eg: req.{0}.set_at(""{1}"", 0, ""{2}"")", ParameterType.ToLower(), ParameterName.Replace("\"", "\\\""), ParameterValue.Replace("\"", "\\\"")));
                    
                }
            }
            else if (Action.StartsWith("Add a"))
            {
                if (ParameterType.Equals("UrlPathParts"))
                {
                    Py.AppendLine("upp = req.UrlPathParts");
                    Py.AppendLine("#The values are returned as a .NET List type so we use the Add method of the .NET list to add a new parameter");
                    Py.AppendLine(string.Format(@"upp.Add(""{0}"")", ParameterValue.Replace("\"", "\\\"")));
                    Py.AppendLine("req.UrlPathParts = upp");

                    Rb.AppendLine("upp = req.url_path_parts");
                    Rb.AppendLine("#The values are returned as a .NET List type so we use the Add method of the .NET list to add a new parameter");
                    Rb.AppendLine(string.Format(@"upp.add(""{1}"")", ParameterValue.Replace("\"", "\\\"")));
                    Rb.AppendLine("req.url_path_parts = upp");
                }
                else
                {
                    if (RPPAnswerOneRB.Checked)
                    {
                        Py.AppendLine(string.Format(@"req.{0}.Set(""{1}"", ""{2}"")", ParameterType, ParameterName.Replace("\"", "\\\""), ParameterValue.Replace("\"", "\\\"")));

                        Rb.AppendLine(string.Format(@"req.{0}.set(""{1}"", ""{2}"")", ParameterType.ToLower(), ParameterName.Replace("\"", "\\\""), ParameterValue.Replace("\"", "\\\"")));
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.{0}.Add(""{1}"", ""{2}"")", ParameterType, ParameterName.Replace("\"", "\\\""), ParameterValue.Replace("\"", "\\\"")));

                        Rb.AppendLine(string.Format(@"req.{0}.add(""{1}"", ""{2}"")", ParameterType.ToLower(), ParameterName.Replace("\"", "\\\""), ParameterValue.Replace("\"", "\\\"")));
                    }
                }
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }

        void HighLightRequestProperty(string StartPropertyTag, bool Full)
        {
            string[] Tags = new string[] { "<method>", "<full_url>", "<ssl>", "<base_url>", "<url>", "<url_path>", "<url_dir>", "<body_string>", "<content_length>", "<content_type>", "<cookie_string>", "<host>", "<file>", "<upp>", "<q>", "<c>", "<b>", "<h>",
            "</method>", "</full_url>", "</ssl>", "</base_url>", "</url>", "</url_path>", "</url_dir>", "</body_string>", "</content_length>", "</content_type>", "</cookie_string>", "</host>", "</file>", "</upp>", "</q>", "</c>", "</b>", "</h>"};

            string FullTemplate = @"<method>POST</method> <url_path><url_dir>/<upp>aaa</upp>/<upp>bbb</upp>/<upp></url_dir>ccc.<file>aspx</file></upp></url_path>?<q>ddd</q>=<q>111</q>&<q>eee</q>=<q>222</q><i<br>>
<h>Host</h>: <h><host>ironwasp.org</host></h><i<br>>
<h>Cookie</h>: <h><cookie_string><c>fff</c>=<c>333</c>; <c>ggg</c>=<c>444</c></cookie_string></h><i<br>>
<h>Content-Type</h>: <h><content_type>application/x-www-form-urlencoded</content_type></h><i<br>>
<h>Content-Length</h>: <h><content_length>15</content_length></h><i<br>>
<i<br>>
<body_string><b>hhh</b>=<b>555</b>&<b>iii</b>=<b>666</b></body_string>
";

            string UrlTemplate = "<i<br>><full_url><base_url><ssl>https</ssl>://ironwasp.org<url>/</base_url>aaa/bbb/ccc.aspx?ddd=111&eee=222</url></full_url>";
            string ToShow = "";
            
            if(Full)
                ToShow = FullTemplate;
            else
                ToShow = UrlTemplate;

            string EndPropertyTag = StartPropertyTag.Replace("<", "</");

            foreach (string Tag in Tags)
            {
                if(Tag.Equals(StartPropertyTag))
                    ToShow = ToShow.Replace(Tag, "<i<hlg>>");
                else if (Tag.Equals(EndPropertyTag))
                    ToShow = ToShow.Replace(Tag, "<i</hlg>>");
                else
                    ToShow = ToShow.Replace(Tag, "");
            }

            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
            SB.Append(Tools.RtfSafe(ToShow));
            SB.Append(@" \par");
            ROPDisplayRTB.Rtf = SB.ToString();
        }

        private void ROPParameterTypeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ROPParameterTypeGrid.SelectedRows == null) return;
            if (ROPParameterTypeGrid.SelectedRows.Count == 0) return;

            ROPQuestionGB.Text = "Do you want to read or modify this value?";
            ROPQuestionGB.Visible = false;
            ROPAnswerEditRB.Enabled = true;
            ROPCreateCodeBtn.Enabled = true;

            if (ROPAnswerEditRB.Checked)
            {
                ROPParameterValueLbl.Visible = true;
                ROPParameterValueTB.Visible = true;
            }

            foreach (DataGridViewRow Row in ROPParameterTypeGrid.Rows)
            {
                if (Row.Index == ROPParameterTypeGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    ROPParameterDescriptionTB.Text = Row.Cells[4].Value.ToString();
                    HighLightRequestProperty(Row.Cells[3].Value.ToString(), Row.Cells[2].Value.ToString() == "full");

                    string Property = Row.Cells[1].Value.ToString();

                    switch (Property)
                    {
                        case("File"):
                        case ("BaseUrl"):
                        case ("UrlDir"):
                        case ("BodyLength"):
                        case ("HasBody"):
                        case ("IsBinary"):
                            ROPQuestionGB.Text = "This is a read-only value, cannot be modified.";
                            ROPQuestionGB.Visible = true;
                            ROPAnswerReadRB.Checked = true;
                            ROPAnswerEditRB.Enabled = false;
                            break;
                        case ("UrlPathParts"):
                        case ("Query Parameters"):
                        case ("Body Parameters"):
                        case ("Cookie Parameters"):
                        case ("Header Parameters"):
                            ROPCreateCodeBtn.Enabled = false;
                            ROPParameterValueLbl.Visible = false;
                            ROPParameterValueTB.Visible = false;
                            break;
                        case("SSL"):
                            ROPParameterValueLbl.Visible = false;
                            ROPParameterValueTB.Visible = false;
                            ROPQuestionGB.Visible = true;
                            break;
                        default:
                            ROPQuestionGB.Visible = true;
                            break;
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ROPCreateCodeBtn_Click(object sender, EventArgs e)
        {
            ShowROPError("");

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Rb.AppendLine("#'req' is a variable that is assumed to contain a Request object");

            string Property = "";
            string Value = ROPParameterValueTB.Text;
            foreach (DataGridViewRow Row in ROPParameterTypeGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Property = Row.Cells[1].Value.ToString();
                }
            }
            if (Property.Length == 0)
            {
                ShowROPError("No property selected.");
                return;
            }
            switch(Property)
            {
                case ("Method"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request method is ' + req.Method");
                        Rb.AppendLine("puts 'Request method is ' + req.http_method");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.Method = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.http_method = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("SSL"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if req.SSL:");
                        Py.Append("  "); Py.AppendLine("print 'Request uses SSL'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Request does not use SSL'");

                        Rb.AppendLine("if req.ssl");
                        Rb.Append("  "); Rb.AppendLine("puts 'Request uses SSL'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Request does not use SSL'");
                        Rb.AppendLine("end");
                    }
                    else
                    {
                        Py.AppendLine("req.SSL = True");
                        Rb.AppendLine("req.ssl = true");
                    }
                    break;
                case ("HasBody"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if req.HasBody:");
                        Py.Append("  "); Py.AppendLine("print 'Request has a body'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Request does not have a body'");

                        Rb.AppendLine("if req.has_body");
                        Rb.Append("  "); Rb.AppendLine("puts 'Request has a body'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Request does not have a body'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("IsBinary"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if req.IsBinary:");
                        Py.Append("  "); Py.AppendLine("print 'Request body is a binary value'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Request body is not a binary value'");

                        Rb.AppendLine("if req.is_binary");
                        Rb.Append("  "); Rb.AppendLine("puts 'Request body is a binary value'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Request body is not a binary value'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("Host"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request host is ' + req.Host");
                        Rb.AppendLine("puts 'Request host is ' + req.host");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.Host = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.host = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("File"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request file extension is ' + req.File");
                        Rb.AppendLine("puts 'Request file extension is ' + req.file");
                    }
                    break;
                case ("FullUrl"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request full url is ' + req.FullUrl");
                        Rb.AppendLine("puts 'Request full url is ' + req.full_url");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.FullUrl = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.full_url = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("Url"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request url is ' + req.Url");
                        Rb.AppendLine("puts 'Request url is ' + req.url");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.Url = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.url = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("BaseUrl"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request base url is ' + req.BaseUrl");
                        Rb.AppendLine("puts 'Request base url is ' + req.base_url");
                    }
                    break;
                case ("UrlPath"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request url path is ' + req.UrlPath");
                        Rb.AppendLine("puts 'Request url path is ' + req.url_path");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.UrlPath = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.url_path = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("UrlDir"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request url dir is ' + req.UrlDir");
                        Rb.AppendLine("puts 'Request url dir is ' + req.url_dir");
                    }
                    break;
                case ("BodyString"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request body content is ' + req.BodyString");
                        Rb.AppendLine("puts 'Request body content is ' + req.body_string");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.BodyString = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.body_string = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("CookieString"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Request cookie content is ' + req.CookieString");
                        Rb.AppendLine("puts 'Request cookie content is ' + req.cookie_string");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.CookieString = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.cookie_string = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("BodyLength"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Size of the Request body is ' + str(req.BodyLength)");
                        Rb.AppendLine("puts 'Size of the Request body is ' + req.body_length.to_s");
                    }
                    break;
                case ("ContentType"):
                    if (ROPAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'The type of the Request body content is ' + req.ContentType");
                        Rb.AppendLine("puts 'The type of the Request body content is ' + req.content_type");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"req.ContentType = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"req.content_type = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
            }

            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void ROPAnswerReadRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ROPAnswerReadRB.Checked)
            {
                ROPParameterValueLbl.Visible = false;
                ROPParameterValueTB.Visible = false;
            }
            else
            {
                ROPParameterValueLbl.Visible = true;
                ROPParameterValueTB.Visible = true;
            } 
        }

        void HighLightResponseProperty(string StartPropertyTag)
        {
            string[] Tags = new string[] { "<version>", "<code>", "<status>", "<h>", "<type>", "<char>", "<sc>", "<t>", "<len>", "<b>",
            "</version>", "</code>", "</status>", "</h>", "</type>", "</char>", "</sc>", "</t>", "</len>","</b>" };

            string FullTemplate = @"<version>HTTP/1.1</version> <code>200</code> <status>OK</status><i<br>>
<h>Server</h>: <h>Microsoft-IIS/7.0</h><i<br>>
<h>Content-Type</h>: <h><type>text/html; charset=<char>utf-8</char></type></h><i<br>>
<h>Content-Length</h>: <h><len>69</len></h><i<br>>
<h>Set-Cookie</h>: <h><sc>aaa=111; expires=Thu, 01-Jan-2015 19:57:42 GMT; path=/; domain=.ironwasp.org</sc></h><i<br>>
<h>Set-Cookie</h>: <h><sc>bbb=222; expires=Wed, 03-Jul-2013 19:57:42 GMT; path=/; domain=www.ironwasp.org; HttpOnly</sc></h><i<br>>
<i<br>>
<b><html>Welcome</html></b>

";

            string ToShow = FullTemplate;

            string EndPropertyTag = StartPropertyTag.Replace("<", "</");

            foreach (string Tag in Tags)
            {
                if (Tag.Equals(StartPropertyTag))
                    ToShow = ToShow.Replace(Tag, "<i<hlg>>");
                else if (Tag.Equals(EndPropertyTag))
                    ToShow = ToShow.Replace(Tag, "<i</hlg>>");
                else
                    ToShow = ToShow.Replace(Tag, "");
            }

            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
            SB.Append(Tools.RtfSafe(ToShow));
            SB.Append(@" \par");
            ResDisplayRTB.Rtf = SB.ToString();
        }

        private void ResParameterTypeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ResParameterTypeGrid.SelectedRows == null) return;
            if (ResParameterTypeGrid.SelectedRows.Count == 0) return;

            ResQuestionGB.Text = "Do you want to read or modify this value?";
            ResQuestionGB.Visible = false;
            ResAnswerEditRB.Enabled = true;
            ResCreateCodeBtn.Enabled = true;
            ResParameterValueTB.Enabled = true;

            if (ResAnswerEditRB.Checked)
            {
                ResParameterValueLbl.Visible = true;
                ResParameterValueTB.Visible = true;
            }

            foreach (DataGridViewRow Row in ResParameterTypeGrid.Rows)
            {
                if (Row.Index == ResParameterTypeGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    ResParameterDescriptionTB.Text = Row.Cells[3].Value.ToString();
                    HighLightResponseProperty(Row.Cells[2].Value.ToString());

                    string Property = Row.Cells[1].Value.ToString();

                    switch (Property)
                    {
                        case ("ContentType"):
                        case ("BodyString"):
                            ResQuestionGB.Visible = true;
                            ResParameterValueLbl.Visible = true;
                            ResParameterValueTB.Visible = true;
                            ResParameterValueTB.Enabled = true;
                            break;
                        case ("Headers"):
                        case ("Html"):
                            ResCreateCodeBtn.Enabled = false;
                            ResParameterValueLbl.Visible = false;
                            ResParameterValueTB.Visible = false;
                            break;
                        case ("SetCookies"):
                            ResParameterValueLbl.Visible = false;
                            ResParameterValueTB.Visible = false;
                            ResQuestionGB.Visible = true;
                            ResParameterValueTB.Enabled = false;
                            break;
                        default:
                            ResQuestionGB.Text = "This is a read-only value, cannot be modified.";
                            ResQuestionGB.Visible = true;
                            ResAnswerReadRB.Checked = true;
                            ResAnswerEditRB.Enabled = false;                           
                            break;
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ResCreateCodeBtn_Click(object sender, EventArgs e)
        {

            ShowResError("");

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'res' is a variable that is assumed to contain a Response object");
            Rb.AppendLine("#'res' is a variable that is assumed to contain a Response object");

            string Property = "";
            foreach (DataGridViewRow Row in ResParameterTypeGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                     Property = Row.Cells[1].Value.ToString();
                     break;
                }
            }
            string Value = ResParameterValueTB.Text;
            switch (Property)
            {
                case ("Code"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Response Code is ' + str(res.Code)");
                        Rb.AppendLine("puts 'Response Code  is ' + res.code.to_s");
                    }
                    break;
                case ("Status"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Response status is ' + res.Status");
                        Rb.AppendLine("puts 'Response status is ' + res.status");
                    }
                    break;
                case ("HttpVersion"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Response HTTP version is ' + res.HttpVersion");
                        Rb.AppendLine("puts 'Response HTTP version is ' + res.http_version");
                    }
                    break;
                case ("BodyLength"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Size of the Response body is ' + str(res.BodyLength)");
                        Rb.AppendLine("puts 'Size of the Response body is ' + res.body_length.to_s");
                    }
                    break;
                case ("ContentType"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'The type of the Response body content is ' + res.ContentType");
                        Rb.AppendLine("puts 'The type of the Response body content is ' + res.content_type");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"res.ContentType = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"res.content_type = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("BodyEncoding"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'The encoding type of the Response body is ' + res.BodyEncoding");
                        Rb.AppendLine("puts 'The encoding type of the Response body is ' + res.body_encoding");
                    }
                    break;
                case ("SetCookies"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("#SetCookies is list of SetCookie objects. It is a .NET List type but you can check its length and iterate over it like Python list.");
                        Py.AppendLine("if len(res.SetCookies) > 0:");
                        Py.Append("  "); Py.AppendLine("for sc in res.SetCookies:");
                        Py.Append("    "); Py.AppendLine(@"print ""SetCookie Header Value is : "" + sc.FullString");
                        Py.Append("    "); Py.AppendLine(@"print ""  Name= "" + sc.Name");
                        Py.Append("    "); Py.AppendLine(@"print ""  Value= "" + sc.Value");
                        Py.Append("    "); Py.AppendLine(@"print ""  Path= "" + sc.Path");
                        Py.Append("    "); Py.AppendLine(@"print ""  Domain= "" + sc.Domain");
                        Py.Append("    "); Py.AppendLine(@"print ""  Expires= "" + sc.Expires");
                        Py.Append("    "); Py.AppendLine(@"print ""  MaxAge= "" + sc.MaxAge");
                        Py.Append("    "); Py.AppendLine(@"print ""  Comment= "" + sc.Comment");
                        Py.Append("    "); Py.AppendLine(@"print ""  Version= "" + sc.Version");
                        Py.Append("    "); Py.AppendLine(@"if sc.HttpOnly:");
                        Py.Append("      "); Py.AppendLine(@"print ""  HttpOnly flag is set""");
                        Py.Append("    "); Py.AppendLine(@"else:");
                        Py.Append("      "); Py.AppendLine(@"print ""  HttpOnly flag is not set""");
                        Py.Append("    "); Py.AppendLine(@"if sc.Secure:");
                        Py.Append("      "); Py.AppendLine(@"print ""  Secure flag is set""");
                        Py.Append("    "); Py.AppendLine(@"else:");
                        Py.Append("      "); Py.AppendLine(@"print ""  Secure flag is not set""");

                        Rb.AppendLine("#SetCookies is list of SetCookie objects. It is a .NET List type but you can check its length and iterate over it like Ruby list.");
                        Rb.AppendLine("if res.set_cookies.count > 0");
                        Rb.Append("  "); Rb.AppendLine("for sc in res.set_cookies");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""SetCookie Header Value is : "" + sc.full_string");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  Name= "" + sc.name");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  Value= "" + sc.value");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  Path= "" + sc.path");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  Domain= "" + sc.domain");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  Expires= "" + sc.expires");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  MaxAge= "" + sc.max_age");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  Comment= "" + sc.comment");
                        Rb.Append("    "); Rb.AppendLine(@"puts ""  Version= "" + sc.version");
                        Rb.Append("    "); Rb.AppendLine(@"if sc.http_only");
                        Rb.Append("      "); Rb.AppendLine(@"puts ""  HttpOnly flag is set""");
                        Rb.Append("    "); Rb.AppendLine(@"else");
                        Rb.Append("      "); Rb.AppendLine(@"puts ""  HttpOnly flag is not set""");
                        Rb.Append("    "); Rb.AppendLine(@"end");
                        Rb.Append("    "); Rb.AppendLine(@"if sc.secure");
                        Rb.Append("      "); Rb.AppendLine(@"puts ""  Secure flag is set""");
                        Rb.Append("    "); Rb.AppendLine(@"else");
                        Rb.Append("      "); Rb.AppendLine(@"puts ""  Secure flag is not set""");
                        Rb.Append("    "); Rb.AppendLine(@"end");
                        Rb.Append("  "); Rb.AppendLine(@"end");
                        Rb.AppendLine(@"end");
                    }
                    else
                    {
                        Py.AppendLine(@"#Create a new SetCookie object from string. The format of the string must be similar to the value of Set-Cookie response headers from server");
                        Py.AppendLine(@"sc = SetCookie(""aaa=111; Path=/"")");
                        Py.AppendLine("#SetCookies is a .NET List so we use the Add method of the .NET list to add a new SetCookie object");
                        Py.AppendLine(@"res.SetCookies.Add(sc)");

                        Rb.AppendLine(@"#Create a new SetCookie object from string. The format of the string must be similar to the value of Set-Cookie response headers from server");
                        Rb.AppendLine(@"sc = SetCookie.new(""aaa=111; Path=/"")");
                        Rb.AppendLine("#SetCookies is a .NET List so we use the Add method of the .NET list to add a new SetCookie object");
                        Rb.AppendLine(@"res.set_cookies.push(sc)");
                    }
                    break;
                case ("BodyString"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'Response body content is ' + res.BodyString");
                        Rb.AppendLine("puts 'Response body content is ' + res.body_string");
                    }
                    else
                    {
                        Py.AppendLine(string.Format(@"res.BodyString = ""{0}""", Value.Replace("\"", "\\\"")));
                        Rb.AppendLine(string.Format(@"res.body_string = ""{0}""", Value.Replace("\"", "\\\"")));
                    }
                    break;
                case ("RoundTrip"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("print 'The time taken to recieve this response is ' + str(res.RoundTrip) + ' ms'");
                        Rb.AppendLine("puts 'The time taken to recieve this response is ' + res.round_trip.to_s + ' ms'");
                    }
                    break;
                case ("HasBody"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if res.HasBody:");
                        Py.Append("  "); Py.AppendLine("print 'Response has a body'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Response does not have a body'");

                        Rb.AppendLine("if res.has_body");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response has a body'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response does not have a body'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("IsHtml"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if res.IsHtml:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is HTML'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is not HTML'");

                        Rb.AppendLine("if res.is_html");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is HTML'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is not HTML'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("IsJson"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if res.IsJson:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is JSON'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is not JSON'");

                        Rb.AppendLine("if res.is_json");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is JSON'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is not JSON'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("IsXml"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if res.IsXml:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is XML'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is not XML'");

                        Rb.AppendLine("if res.is_xml");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is XML'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is not XML'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("IsJavaScript"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if res.IsJavaScript:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is JavaScript'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is not JavaScript'");

                        Rb.AppendLine("if res.is_java_script");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is JavaScript'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is not JavaScript'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("IsCss"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if res.IsCss:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is CSS'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Response body is not CSS'");

                        Rb.AppendLine("if res.is_css");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is CSS'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response body is not CSS'");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("IsRedirect"):
                    if (ResAnswerReadRB.Checked)
                    {
                        Py.AppendLine("if res.IsRedirect:");
                        Py.Append("  "); Py.AppendLine("print 'Response is a redirect'");
                        Py.AppendLine("else:");
                        Py.Append("  "); Py.AppendLine("print 'Response is not a redirect'");

                        Rb.AppendLine("if res.is_redirect");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response is a Redirect'");
                        Rb.AppendLine("else");
                        Rb.Append("  "); Rb.AppendLine("puts 'Response is not a Redirect'");
                        Rb.AppendLine("end");
                    }
                    break;
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void ResAnswerReadRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ResAnswerReadRB.Checked)
            {
                ResParameterValueLbl.Visible = false;
                ResParameterValueTB.Visible = false;
            }
            else
            {
                ResParameterValueLbl.Visible = true;
                ResParameterValueTB.Visible = true;
            } 
        }

        private void SRSendWithLogSourceRB_CheckedChanged(object sender, EventArgs e)
        {
            if (SRSendWithLogSourceRB.Checked)
            {
                SRLogSourceLbl.Visible = true;
                SRLogSourceTB.Visible = true;
            }
            else
            {
                SRLogSourceLbl.Visible = false;
                SRLogSourceTB.Visible = false;
            }
        }

        private void HtmlMainActionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (HtmlMainActionsGrid.SelectedRows == null) return;
            if (HtmlMainActionsGrid.SelectedRows.Count == 0) return;

            HtmlDescriptionTB.Text = "";
            HtmlAnswerDescriptionTB.Visible = false;
            HtmlOptionsGrid.Visible = false;
            HtmlOptionsGrid.Rows.Clear();
            HtmlCreateCodeBtn.Enabled = false;
            
            HtmlGVPanel.Visible = false;
            HtmlGEPanel.Visible = false;
            HtmlAnswerPanel.Visible = false;

            HtmlGVPanel.Width = 10;
            HtmlGEPanel.Width = 10;
            HtmlAnswerPanel.Width = 10;

            foreach (DataGridViewRow Row in HtmlMainActionsGrid.Rows)
            {
                if (Row.Index == HtmlMainActionsGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    HtmlDescriptionTB.Text = Row.Cells[2].Value.ToString();

                    string Property = Row.Cells[1].Value.ToString();

                    switch (Property)
                    {
                        case ("Get Title of Html"):
                        case ("Get Links from Html"):
                        case ("Get Comments from Html"):
                            HtmlCreateCodeBtn.Enabled = true;
                            break;
                        case ("Get JavaScript from Html"):
                            HtmlOptionsGrid.Rows.Clear();
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Get All JavaScript", "JS" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Only JavaScript having a keyword", "JS" });
                            HtmlOptionsGrid.Visible = true;
                            break;
                        case ("Get VisualBasic from Html"):
                            HtmlOptionsGrid.Rows.Clear();
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Get All Visual Basic", "VB" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Only Visual Basic having a keyword", "VB" });
                            HtmlOptionsGrid.Visible = true;
                            break;
                        case ("Get CSS from Html"):
                            HtmlOptionsGrid.Rows.Clear();
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Get All CSS", "CSS" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Only CSS having a keyword", "CSS" });
                            HtmlOptionsGrid.Visible = true;
                            break;
                        case ("Get Values of Html attributes"):
                            HtmlOptionsGrid.Rows.Clear();
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 1", "GV" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 2", "GV" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 3", "GV" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 4", "GV" });
                            HtmlOptionsGrid.Visible = true;
                            HtmlGVPanel.Width = 398;
                            HtmlGVPanel.Visible = true;
                            HtmlAnswerDescriptionTB.Text = @"
Html tags look like: <input    name=""search""    type=""hidden"">

Here 'input' is the tag name. 'name' and 'type' are attribute names, 'search' and 'hidden' are corresponding attribute values.

The input fields at the bottom denote the values required to be entered by you.

The input box before 'value to get' should be filled by the name of attribute whose value you want to get.

The first input box will be the name of the html tags where this attribute value must be taken from. Enter * if you want to search all tags.

The 3rd and 4th input boxes are optional. If you provide an attribute name and value in these boxes then only tags containing them will be searched.

You can use any of the templates on the left for reference.

Template 1:
Gets the urls of all scripts loaded in this HTML

Template 2:
Gets the value of the element with id set as 'c_token'

Template 3:
Gets the names of all hidden input tags in the HTML

Template 4:
Gets the value of all 'onclick' event handlers in the HTML
";
                            HtmlAnswerDescriptionTB.Visible = true;
                            HtmlCreateCodeBtn.Enabled = true;
                            break;
                        case ("Get Elements from Html as strings"):
                        case ("Get Elements from Html as objects"):
                            HtmlOptionsGrid.Rows.Clear();
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 1", "GE" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 2", "GE" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 3", "GE" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 4", "GE" });
                            HtmlOptionsGrid.Rows.Add(new object[] { false, "Template 5", "GE" });
                            HtmlOptionsGrid.Visible = true;
                            HtmlGEPanel.Width = 398;
                            HtmlGEPanel.Visible = true;
                            HtmlAnswerDescriptionTB.Text = @"
Html tags look like: <input    name=""search""    type=""hidden"">

Here 'input' is the tag name. 'name' and 'type' are attribute names, 'search' and 'hidden' are corresponding attribute values.

The input fields at the bottom denote the values required to be entered by you.

The first input box will be the name of the html tags that must be returned. Enter * if you want to match all tags.

The 2nd and 3rd input boxes are optional. If you provide an attribute name and value in these boxes then only elements containing them will be returned.

You can use any of the templates on the left for reference.

Template 1:
Gets all HTML form elements in the page

Template 2:
Gets all script elements that load script from other files

Template 3:
Gets the element with id set as 'c_token'

Template 4:
Gets all hidden input tags in the HTML

Template 5:
Gets all elements that have the 'onclick' event handlers in the HTML
";
                            HtmlAnswerDescriptionTB.Visible = true;
                            HtmlCreateCodeBtn.Enabled = true;
                            break;
                        case ("Find the context a keyword"):
                            HtmlAnswerPanel.Width = 373;
                            HtmlAnswerPanel.Visible = true;
                            HtmlCreateCodeBtn.Enabled = true;
                            break;
                        case ("Get Content of Meta tags"):
                            HtmlAnswerPanel.Visible = true;
                            break;
                        case ("Get Forms from Html"):
                            HtmlCreateCodeBtn.Enabled = true;
                            break;
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void HtmlOptionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (HtmlOptionsGrid.SelectedRows == null) return;
            if (HtmlOptionsGrid.SelectedRows.Count == 0) return;

            string Option = "";
            string Type = "";
            foreach (DataGridViewRow Row in HtmlOptionsGrid.Rows)
            {
                if (Row.Index == HtmlOptionsGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    Option = Row.Cells[1].Value.ToString();
                    Type = Row.Cells[2].Value.ToString();
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
            if (Option.Length == 0 || Type.Length == 0)
            {
                return;
            }
            switch (Type)
            {
                case ("JS"):
                case ("VB"):
                case ("CSS"):
                    if (Option.StartsWith("Only"))
                    {
                        HtmlAnswerPanel.Width = 373;
                        HtmlAnswerPanel.Visible = true;
                    }
                    else
                    {
                        HtmlAnswerPanel.Width = 10;
                        HtmlAnswerPanel.Visible = false;
                    }
                    HtmlCreateCodeBtn.Enabled = true;
                    break;
                case ("GV"):
                    switch (Option)
                    {
                        case("Template 1"):
                            HtmlGVTagTB.Text = "script";
                            HtmlGVMainAttributeNameTB.Text = "src";
                            HtmlGVHelperAttributeValueTB.Text = "";
                            HtmlGVHelperAttributeNameTB.Text = "";
                            break;
                        case ("Template 2"):
                            HtmlGVTagTB.Text = "*";
                            HtmlGVMainAttributeNameTB.Text = "value";
                            HtmlGVHelperAttributeNameTB.Text = "id";
                            HtmlGVHelperAttributeValueTB.Text = "c_token";
                            break;
                        case ("Template 3"):
                            HtmlGVTagTB.Text = "input";
                            HtmlGVMainAttributeNameTB.Text = "name";
                            HtmlGVHelperAttributeNameTB.Text = "type";
                            HtmlGVHelperAttributeValueTB.Text = "hidden";
                            break;
                        case ("Template 4"):
                            HtmlGVTagTB.Text = "*";
                            HtmlGVMainAttributeNameTB.Text = "onclick";
                            HtmlGVHelperAttributeNameTB.Text = "";
                            HtmlGVHelperAttributeValueTB.Text = "";
                            break;
                    }
                    HtmlCreateCodeBtn.Enabled = true;
                    break;
                case ("GE"):
                    switch (Option)
                    {
                        case ("Template 1"):
                            HtmlGETagTB.Text = "form";
                            HtmlGEAttributeNameTB.Text = "";
                            HtmlGEAttributeValueTB.Text = "";
                            break;
                        case ("Template 2"):
                            HtmlGETagTB.Text = "script";
                            HtmlGEAttributeNameTB.Text = "src";
                            HtmlGEAttributeValueTB.Text = "";
                            break;
                        case ("Template 3"):
                            HtmlGETagTB.Text = "*";
                            HtmlGEAttributeNameTB.Text = "id";
                            HtmlGEAttributeValueTB.Text = "c_token";
                            break;
                        case ("Template 4"):
                            HtmlGETagTB.Text = "input";
                            HtmlGEAttributeNameTB.Text = "type";
                            HtmlGEAttributeValueTB.Text = "hidden";
                            break;
                        case ("Template 5"):
                            HtmlGETagTB.Text = "*";
                            HtmlGEAttributeNameTB.Text = "onclick";
                            HtmlGEAttributeValueTB.Text = "";
                            break;
                    }
                    HtmlCreateCodeBtn.Enabled = true;
                    break;
            }
        }

        private void HtmlCreateCodeBtn_Click(object sender, EventArgs e)
        {
            ShowHtmlError("");

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'res' is a variable that is assumed to contain a Response object");
            Rb.AppendLine("#'res' is a variable that is assumed to contain a Response object");

            string Property = "";
            string Type = "";

            string Keyword = HtmlAnswerTB.Text.Replace("\"", "\\\"");

            string TagName = "";
            string MainAttributeName = "";
            string HelperAttributeName = "";
            string HelperAttributeValue = "";

            foreach (DataGridViewRow Row in HtmlMainActionsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Property = Row.Cells[1].Value.ToString();
                    break;
                }
            }
            foreach (DataGridViewRow Row in HtmlOptionsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Type = Row.Cells[1].Value.ToString();
                    break;
                }
            }

            switch (Property)
            {
                case ("Get Title of Html"):
                    Py.AppendLine("print 'The title of the page is - ' + res.Html.Title");
                    Rb.AppendLine("puts 'The title of the page is - ' + res.html.title");
                    break;
                case ("Get Links from Html"):
                    Py.AppendLine("links = res.Html.Links");
                    Py.AppendLine("#Links are returned as a .NET List type but you can iterate over it like Python list.");
                    Py.AppendLine("for link in links:");
                    Py.Append("  "); Py.AppendLine("print link");

                    Rb.AppendLine("links = res.html.links");
                    Rb.AppendLine("#Links are returned as a .NET List type but you can iterate over it like Ruby list.");
                    Rb.AppendLine("for link in links");
                    Rb.Append("  "); Rb.AppendLine("puts link");
                    Rb.AppendLine("end");
                    break;
                case ("Get Comments from Html"):
                    Py.AppendLine("comments = res.Html.Comments");
                    Py.AppendLine("#Comments are returned as a .NET List type but you can iterate over it like Python list.");
                    Py.AppendLine("for comment in comments:");
                    Py.Append("  "); Py.AppendLine("print comment");

                    Rb.AppendLine("comments = res.html.comments");
                    Rb.AppendLine("#Comments are returned as a .NET List type but you can iterate over it like Ruby list.");
                    Rb.AppendLine("for comment in comments");
                    Rb.Append("  "); Rb.AppendLine("puts comment");
                    Rb.AppendLine("end");
                    break;
                case ("Get JavaScript from Html"):
                    if (Type.StartsWith("Get All"))
                    {
                        Py.AppendLine("#Get all JavaScript from script tags and event-handlers in the Html");
                        Py.AppendLine("scripts = res.Html.GetJavaScript()");
                        Py.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for script in scripts:");
                        Py.Append("  "); Py.AppendLine("print script");

                        Rb.AppendLine("#Get all JavaScript from script tags and event-handlers in the Html");
                        Rb.AppendLine("scripts = res.html.get_java_script");
                        Rb.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for script in scripts");
                        Rb.Append("  "); Rb.AppendLine("puts script");
                        Rb.AppendLine("end");
                    }
                    else
                    {
                        if (Keyword.Length == 0)
                        {
                            ShowHtmlError("Keyword cannot be empty");
                            return;
                        }
                        
                        Py.AppendLine(string.Format(@"#Get all JavaScript from script tags and event-handlers that contain the string ""{0}""", Keyword));
                        Py.AppendLine(string.Format(@"scripts = res.Html.GetJavaScript(""{0}"")", Keyword));
                        Py.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for script in scripts:");
                        Py.Append("  "); Py.AppendLine("print script");

                        Rb.AppendLine(string.Format(@"#Get all JavaScript from script tags and event-handlers that contain the string ""{0}""", Keyword));
                        Rb.AppendLine(string.Format(@"scripts = res.html.get_java_script(""{0}"")", Keyword));
                        Rb.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for script in scripts");
                        Rb.Append("  "); Rb.AppendLine("puts script");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("Get VisualBasic from Html"):
                    if (Type.StartsWith("Get All"))
                    {
                        Py.AppendLine("#Get all VB Script from script tags in the Html");
                        Py.AppendLine("scripts = res.Html.GetVisualBasic()");
                        Py.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for script in scripts:");
                        Py.Append("  "); Py.AppendLine("print script");

                        Rb.AppendLine("#Get all VB Script from script tags in the Html");
                        Rb.AppendLine("scripts = res.html.get_visual_basic");
                        Rb.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for script in scripts");
                        Rb.Append("  "); Rb.AppendLine("puts script");
                        Rb.AppendLine("end");
                    }
                    else
                    {
                        if (Keyword.Length == 0)
                        {
                            ShowHtmlError("Keyword cannot be empty");
                            return;
                        }

                        Py.AppendLine(string.Format(@"#Get all VB Script from script tags that contain the string ""{0}""", Keyword));
                        Py.AppendLine(string.Format(@"scripts = res.Html.GetVisualBasic(""{0}"")", Keyword));
                        Py.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for script in scripts:");
                        Py.Append("  "); Py.AppendLine("print script");

                        Rb.AppendLine(string.Format(@"#Get all VB Script from script tags that contain the string ""{0}""", Keyword));
                        Rb.AppendLine(string.Format(@"scripts = res.html.get_visual_basic(""{0}"")", Keyword));
                        Rb.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for script in scripts");
                        Rb.Append("  "); Rb.AppendLine("puts script");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("Get CSS from Html"):
                    if (Type.StartsWith("Get All"))
                    {
                        Py.AppendLine("#Get all CSS from style tags and style attribute of elements in the Html");
                        Py.AppendLine("all_css = res.Html.GetCss()");
                        Py.AppendLine("#CSSs are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for css in all_css:");
                        Py.Append("  "); Py.AppendLine("print css");

                        Rb.AppendLine("#Get all CSS from style tags and style attribute of elements in the Html");
                        Rb.AppendLine("all_css = res.html.get_css");
                        Rb.AppendLine("#CSSs are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for css in all_css");
                        Rb.Append("  "); Rb.AppendLine("puts css");
                        Rb.AppendLine("end");
                    }
                    else
                    {
                        if (Keyword.Length == 0)
                        {
                            ShowHtmlError("Keyword cannot be empty");
                            return;
                        }

                        Py.AppendLine(string.Format(@"#Get all CSS from style tags and style attribute that contain the string ""{0}""", Keyword));
                        Py.AppendLine(string.Format(@"all_css = res.Html.GetCss(""{0}"")", Keyword));
                        Py.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for css in all_css:");
                        Py.Append("  "); Py.AppendLine("print css");

                        Rb.AppendLine(string.Format(@"#Get all CSS from style tags and style attribute that contain the string ""{0}""", Keyword));
                        Rb.AppendLine(string.Format(@"all_css = res.html.get_css(""{0}"")", Keyword));
                        Rb.AppendLine("#Scripts are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for css in all_css");
                        Rb.Append("  "); Rb.AppendLine("puts css");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("Get Values of Html attributes"):
                    TagName = HtmlGVTagTB.Text.Replace("\"", "\\\"");
                    MainAttributeName = HtmlGVMainAttributeNameTB.Text.Replace("\"", "\\\"");
                    HelperAttributeName = HtmlGVHelperAttributeNameTB.Text.Replace("\"", "\\\"");
                    HelperAttributeValue = HtmlGVHelperAttributeValueTB.Text.Replace("\"", "\\\"");

                    if (TagName.Length == 0)
                    {
                        ShowHtmlError("Tagname cannot be empty");
                        return;
                    }
                    if (MainAttributeName.Length == 0)
                    {
                        ShowHtmlError("Cannot get value of attribute without its name. Provide the attribute name.");
                        return;
                    }

                    if (HelperAttributeName.Length == 0)
                    {
                        if (TagName.Equals("*"))
                        {
                            Py.AppendLine(string.Format(@"#Get the values of the '{0}"" attrubute from all tags", MainAttributeName));
                            Rb.AppendLine(string.Format(@"#Get the values of the '{0}"" attrubute from all tags", MainAttributeName));
                        }
                        else
                        {
                            Py.AppendLine(string.Format(@"#Get the values of the ""{0}"" attribute from '{1}' tags", MainAttributeName, TagName));
                            Rb.AppendLine(string.Format(@"#Get the values of the ""{0}"" attribute from '{1}' tags", MainAttributeName, TagName));
                        }

                        Py.AppendLine(string.Format(@"values = res.Html.GetValues(""{0}"", ""{1}"")", TagName, MainAttributeName));
                        Py.AppendLine("#Values are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for value in values:");
                        Py.Append("  "); Py.AppendLine("print value");


                        Rb.AppendLine(string.Format(@"values = res.html.get_values(""{0}"", ""{1}"")", TagName, MainAttributeName));
                        Rb.AppendLine("#Values are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for value in values");
                        Rb.Append("  "); Rb.AppendLine("puts value");
                        Rb.AppendLine("end");
                    }
                    else
                    {
                        if (TagName.Equals("*"))
                        {
                            Py.AppendLine(string.Format(@"#Get the values of the ""{0}"" attrubute from all tags that have the ""{1}"" attribute with value ""{2}""", MainAttributeName, HelperAttributeName, HelperAttributeValue));
                            Rb.AppendLine(string.Format(@"#Get the values of the ""{0}"" attrubute from all tags that have the ""{1}"" attribute with value ""{2}""", MainAttributeName, HelperAttributeName, HelperAttributeValue));
                        }
                        else
                        {
                            Py.AppendLine(string.Format(@"#Get the values of the '{0}' attribute from all '{1}' tags that have the '{2}' attribute with value '{3}'", MainAttributeName, TagName, HelperAttributeName, HelperAttributeValue));
                            Rb.AppendLine(string.Format(@"#Get the values of the '{0}' attribute from all '{1}' tags that have the '{2}' attribute with value '{3}'", MainAttributeName, TagName, HelperAttributeName, HelperAttributeValue));
                        }

                        Py.AppendLine(string.Format(@"values = res.Html.GetValues(""{0}"", ""{1}"", ""{2}"", ""{3}"")", TagName, HelperAttributeName, HelperAttributeValue, MainAttributeName));
                        Py.AppendLine(string.Format(@"#The previous command does a case sensitive match for the attribute value ""{0}"". If you want to do a case-insenstive match then use:", HelperAttributeValue));
                        Py.AppendLine(string.Format(@"#res.Html.GetValuesIgnoreValueCase(""{0}"", ""{1}"", ""{2}"", ""{3}"")", TagName, HelperAttributeName, HelperAttributeValue, MainAttributeName));
                        Py.AppendLine("#Values are returned as a .NET List type but you can iterate over it like Python list.");
                        Py.AppendLine("for value in values:");
                        Py.Append("  "); Py.AppendLine("print value");


                        Rb.AppendLine(string.Format(@"values = res.html.get_values(""{0}"", ""{1}"", ""{2}"", ""{3}"")", TagName, HelperAttributeName, HelperAttributeValue, MainAttributeName));
                        Py.AppendLine(string.Format(@"#The previous command does a case sensitive match for the attribute value ""{0}"". If you want to do a case-insenstive match then use:", HelperAttributeValue));
                        Py.AppendLine(string.Format(@"#res.html.get_values_ignore_value_case(""{0}"", ""{1}"", ""{2}"", ""{3}"")", TagName, HelperAttributeName, HelperAttributeValue, MainAttributeName));
                        Rb.AppendLine("#Values are returned as a .NET List type but you can iterate over it like Ruby list.");
                        Rb.AppendLine("for value in values");
                        Rb.Append("  "); Rb.AppendLine("puts value");
                        Rb.AppendLine("end");
                    }
                    break;
                case ("Get Elements from Html as strings"):
                case ("Get Elements from Html as objects"):
                    TagName = HtmlGETagTB.Text.Replace("\"", "\\\"");;
                    HelperAttributeName = HtmlGEAttributeNameTB.Text.Replace("\"", "\\\"");
                    HelperAttributeValue = HtmlGEAttributeValueTB.Text.Replace("\"", "\\\"");

                    string PyCommand = "";
                    string RbCommand = "";
                    
                    string PyElementAction = "";
                    string RbElementAction = "";

                    if (TagName.Length == 0)
                    {
                        ShowHtmlError("Tagname cannot be empty");
                        return;
                    }

                    if (Property.Equals("Get Elements from Html as strings"))
                    {
                        PyCommand = "Get";
                        RbCommand = "get";
                    }
                    else
                    {
                        PyCommand = "GetNodes";
                        RbCommand = "get_nodes";
                    }

                    StringBuilder PyEA = new StringBuilder();
                    StringBuilder RbEA = new StringBuilder();

                    if (Property.Equals("Get Elements from Html as strings"))
                    {
                        PyEA.AppendLine("#Elements are returned as a .NET List type but you can iterate over it like Python list.");
                        PyEA.AppendLine("for element in elements:");
                        PyEA.Append("  "); PyEA.AppendLine("print element");

                        RbEA.AppendLine("#Elements are returned as a .NET List type but you can iterate over it like Ruby list.");
                        RbEA.AppendLine("for element in elements");
                        RbEA.Append("  "); RbEA.AppendLine("puts element");
                        RbEA.AppendLine("end");
                    }
                    else
                    {
                        PyEA.AppendLine("#The elements are returned as HtmlNodeCollection object");
                        PyEA.AppendLine("#If there are no matches then None is returned, so first check if there are results");
                        PyEA.AppendLine("if elements:");
                        PyEA.Append("  "); PyEA.AppendLine("#We can look through the collection to access each individual element.");
                        PyEA.Append("  "); PyEA.AppendLine("for element in elements:");
                        PyEA.Append("    "); PyEA.AppendLine("#Each individual element is represented as HtmlNode object. We can get the required details from the object.");
                        PyEA.Append("    "); PyEA.AppendLine("print 'Element name: ' + element.Name");
                        PyEA.Append("    "); PyEA.AppendLine("print 'Element id: ' + element.Id");
                        PyEA.Append("    "); PyEA.AppendLine("print 'Element attributes:'");
                        PyEA.Append("    "); PyEA.AppendLine("for attr in element.Attributes:");
                        PyEA.Append("      "); PyEA.AppendLine("print attr.Name + '=' + attr.Value");
                        PyEA.Append("    "); PyEA.AppendLine("print 'Element innertext: ' + element.InnerText");
                        PyEA.Append("    "); PyEA.AppendLine("print 'Element innerhtml: ' + element.InnerHtml");
                        PyEA.Append("    "); PyEA.AppendLine("print 'Element outerhtml: ' + element.OuterHtml");
                        PyEA.Append("    "); PyEA.AppendLine("if element.HasChildNodes:");
                        PyEA.Append("      "); PyEA.AppendLine("print 'this element has child nodes.'");
                        PyEA.Append("      "); PyEA.AppendLine("#Child nodes can be accessed by element.ChildNodes property. This property returns a HtmlNodeCollection object.");
                        PyEA.Append("    "); PyEA.AppendLine("else:");
                        PyEA.Append("      "); PyEA.AppendLine("print 'this element does not have child nodes'");
                        

                        RbEA.AppendLine("#The elements are returned as HtmlNodeCollection object");
                        RbEA.AppendLine("#If there are no matches then nil is returned, so first check if there are results");
                        RbEA.AppendLine("if elements");
                        RbEA.Append("  "); RbEA.AppendLine("#We can look through the collection to access each individual element.");
                        RbEA.Append("  "); RbEA.AppendLine("for element in elements");
                        RbEA.Append("    "); RbEA.AppendLine("#Each individual element is represented as HtmlNode object. We can get the required details from the object.");
                        RbEA.Append("    "); RbEA.AppendLine("puts 'Element name: ' + element.name");
                        RbEA.Append("    "); RbEA.AppendLine("puts 'Element id: ' + element.id");
                        RbEA.Append("    "); RbEA.AppendLine("puts 'Element attributes:'");
                        RbEA.Append("    "); RbEA.AppendLine("for attr in element.attributes");
                        RbEA.Append("      "); RbEA.AppendLine("print attr.name + '=' + attr.value");
                        RbEA.Append("    "); RbEA.AppendLine("end");
                        RbEA.Append("    "); RbEA.AppendLine("puts 'Element innertext: ' + element.inner_text");
                        RbEA.Append("    "); RbEA.AppendLine("puts 'Element innerhtml: ' + element.inner_html");
                        RbEA.Append("    "); RbEA.AppendLine("puts 'Element outerhtml: ' + element.outer_html");
                        RbEA.Append("    "); RbEA.AppendLine("if element.has_child_nodes");
                        RbEA.Append("      "); RbEA.AppendLine("puts 'this element has child nodes.'");
                        RbEA.Append("      "); RbEA.AppendLine("#Child nodes can be accessed by element.ChildNodes property. This property returns a HtmlNodeCollection object.");
                        RbEA.Append("    "); RbEA.AppendLine("else");
                        RbEA.Append("      "); RbEA.AppendLine("puts 'this element does not have child nodes'");
                        RbEA.Append("    "); RbEA.AppendLine("end");
                        RbEA.Append("  "); RbEA.AppendLine("end");
                        RbEA.AppendLine("end");
                    }

                    PyElementAction = PyEA.ToString();
                    RbElementAction = RbEA.ToString();

                    if (HelperAttributeName.Length == 0)
                    {
                        if (TagName.Equals("*"))
                        {
                            Py.AppendLine(@"#Get all the elements in the Html");
                            Rb.AppendLine(@"#Get all the elements in the Html");
                        }
                        else
                        {
                            Py.AppendLine(string.Format(@"#Get all ""{0}"" elements from the Html", TagName));
                            Rb.AppendLine(string.Format(@"#Get all ""{0}"" elements from the Html", TagName));
                        }
                        
                        Py.AppendLine(string.Format(@"elements = res.Html.{0}(""{1}"")", PyCommand, TagName));
                        Py.AppendLine(PyElementAction);


                        Rb.AppendLine(string.Format(@"elements = res.html.{0}(""{1}"")", RbCommand, TagName));
                        Rb.AppendLine(RbElementAction);
                    }
                    else
                    {
                        if (HelperAttributeValue.Length == 0)
                        {
                            if (TagName.Equals("*"))
                            {
                                Py.AppendLine(string.Format(@"#Get all elements that have the ""{0}"" attribute", HelperAttributeName));
                                Rb.AppendLine(string.Format(@"#Get all elements that have the ""{0}"" attribute", HelperAttributeName));

                            }
                            else
                            {
                                Py.AppendLine(string.Format(@"#Get all ""{0}"" elements that have the ""{1}"" attribute", TagName, HelperAttributeName));
                                Rb.AppendLine(string.Format(@"#Get all ""{0}"" elements that have the ""{1}"" attribute", TagName, HelperAttributeName));
                            }

                            Py.AppendLine(string.Format(@"elements = res.Html.{0}(""{1}"", ""{2}"")", PyCommand, TagName, HelperAttributeName));
                            Py.AppendLine(PyElementAction);

                            Rb.AppendLine(string.Format(@"elements = res.html.{0}(""{1}"", ""{2}"")", RbCommand, TagName, HelperAttributeName));
                            Rb.AppendLine(RbElementAction);
                        }
                        else
                        {
                            if (TagName.Equals("*"))
                            {
                                Py.AppendLine(string.Format(@"#Get all elements that have the ""{0}"" attribute with value ""{1}""", HelperAttributeName, HelperAttributeValue));
                                Rb.AppendLine(string.Format(@"#Get all elements that have the ""{0}"" attribute with value ""{1}""", HelperAttributeName, HelperAttributeValue));
                            }
                            else
                            {
                                Py.AppendLine(string.Format(@"#Get all ""{0}"" elements that have the ""{1}"" attribute with value ""{2}""", TagName, HelperAttributeName, HelperAttributeValue));
                                Rb.AppendLine(string.Format(@"#Get all ""{0}"" elements that have the ""{1}"" attribute with value ""{2}""", TagName, HelperAttributeName, HelperAttributeValue));
                            }

                            Py.AppendLine(string.Format(@"elements = res.Html.{0}(""{1}"", ""{2}"", ""{3}"")", PyCommand, TagName, HelperAttributeName, HelperAttributeValue));
                            Py.AppendLine(PyElementAction);

                            Rb.AppendLine(string.Format(@"elements = res.html.{0}(""{1}"", ""{2}"", ""{3}"")", RbCommand, TagName, HelperAttributeName, HelperAttributeValue));
                            Rb.AppendLine(RbElementAction);
                        }
                    }
                    break;
                case ("Find the context a keyword"):
                    if (Keyword.Length == 0)
                    {
                        ShowHtmlError("Keyword cannot be empty");
                        return;
                    }

                    Py.AppendLine(string.Format(@"contexts = res.Html.GetContext(""{0}"")", Keyword));
                    Py.AppendLine("#Contexts are returned as a .NET List type but you can iterate over it like Python list.");
                    Py.AppendLine(string.Format(@"print ""The string \""{0}\"" appears in the following sections of the HTML:"" ", Keyword));
                    Py.AppendLine("for context in contexts:");
                    Py.Append("  "); Py.AppendLine("print context");


                    Rb.AppendLine(string.Format(@"contexts = res.html.get_context(""{0}"")", Keyword));
                    Rb.AppendLine("#Contexts are returned as a .NET List type but you can iterate over it like Ruby list.");
                    Rb.AppendLine(string.Format(@"puts ""The string \""{0}\"" appears in the following sections of the HTML:"" ", Keyword));
                    Rb.AppendLine("for context in contexts");
                    Rb.Append("  "); Rb.AppendLine("puts context");
                    Rb.AppendLine("end");
                    break;
                case ("Get Forms from Html"):
                    Py.AppendLine("forms = res.Html.GetForms()");
                    Py.AppendLine("#The elements are returned as list of HtmlNode objects");
                    Py.AppendLine("#We can look through the collection to access each individual element.");
                    Py.AppendLine("for element in forms:");
                    Py.Append("  "); Py.AppendLine("#Each individual element is represented as HtmlNode object. We can get the required details from the object.");
                    Py.Append("  "); Py.AppendLine("print 'Element name: ' + element.Name");
                    Py.Append("  "); Py.AppendLine("print 'Element id: ' + element.Id");
                    Py.Append("  "); Py.AppendLine("print 'Element attributes:'");
                    Py.Append("  "); Py.AppendLine("for attr in element.Attributes:");
                    Py.Append("    "); Py.AppendLine("print attr.Name + '=' + attr.Value");
                    Py.Append("  "); Py.AppendLine("print 'Element innertext: ' + element.InnerText");
                    Py.Append("  "); Py.AppendLine("print 'Element innerhtml: ' + element.InnerHtml");
                    Py.Append("  "); Py.AppendLine("print 'Element outerhtml: ' + element.OuterHtml");
                    Py.Append("  "); Py.AppendLine("if element.HasChildNodes:");
                    Py.Append("    "); Py.AppendLine("print 'this element has child nodes.'");
                    Py.Append("    "); Py.AppendLine("#Child nodes can be accessed by element.ChildNodes property. This property returns a HtmlNodeCollection object.");
                    Py.Append("  "); Py.AppendLine("else:");
                    Py.Append("    "); Py.AppendLine("print 'this element does not have child nodes'");

                    Rb.AppendLine("forms = res.html.get_forms");
                    Rb.AppendLine("#The elements are returned as list of HtmlNode objects");
                    Rb.AppendLine("#We can look through the collection to access each individual element.");
                    Rb.AppendLine("for element in forms");
                    Rb.Append("  "); Rb.AppendLine("#Each individual element is represented as HtmlNode object. We can get the required details from the object.");
                    Rb.Append("  "); Rb.AppendLine("puts 'Element name: ' + element.name");
                    Rb.Append("  "); Rb.AppendLine("puts 'Element id: ' + element.id");
                    Rb.Append("  "); Rb.AppendLine("puts 'Element attributes:'");
                    Rb.Append("  "); Rb.AppendLine("for attr in element.attributes");
                    Rb.Append("    "); Rb.AppendLine("print attr.name + '=' + attr.value");
                    Rb.Append("  "); Rb.AppendLine("end");
                    Rb.Append("  "); Rb.AppendLine("puts 'Element innertext: ' + element.inner_text");
                    Rb.Append("  "); Rb.AppendLine("puts 'Element innerhtml: ' + element.inner_html");
                    Rb.Append("  "); Rb.AppendLine("puts 'Element outerhtml: ' + element.outer_html");
                    Rb.Append("  "); Rb.AppendLine("if element.has_child_nodes");
                    Rb.Append("    "); Rb.AppendLine("puts 'this element has child nodes.'");
                    Rb.Append("    "); Rb.AppendLine("#Child nodes can be accessed by element.ChildNodes property. This property returns a HtmlNodeCollection object.");
                    Rb.Append("  "); Rb.AppendLine("else");
                    Rb.Append("    "); Rb.AppendLine("puts 'this element does not have child nodes'");
                    Rb.Append("  "); Rb.AppendLine("end");
                    Rb.AppendLine("end");
                    break;
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void LogCreateCodeBtn_Click(object sender, EventArgs e)
        {
            ShowLogError("");

            string LogSource = "";
           
            foreach (DataGridViewRow Row in LogSourceGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    LogSource = Row.Cells[1].Value.ToString();
                    break;
                }
            }
            if (LogSource.Length == 0)
            {
                ShowLogError("Log Source has not been selected.");
                return;
            }

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#We find out the number of logs in the selected log source and loop through it");
            Rb.AppendLine("#We find out the number of logs in the selected log source and loop through it");

            switch (LogSource)
            {
                case ("Proxy"):
                    Py.AppendLine("for i in range(1, Config.LastProxyLogId):");
                    Py.Append("  "); Py.AppendLine("sess = Session.FromProxyLog(i)");

                    Rb.AppendLine("for i in (1..Config.last_proxy_log_id)");
                    Rb.Append("  "); Rb.AppendLine("sess = Session.from_proxy_log(i)");
                    break;
                case ("Probe"):
                    Py.AppendLine("for i in range(1, Config.LastProbeLogId):");
                    Py.Append("  "); Py.AppendLine("sess = Session.FromProbeLog(i)");

                    Rb.AppendLine("for i in (1..Config.last_probe_log_id)");
                    Rb.Append("  "); Rb.AppendLine("sess = Session.from_probe_log(i)");
                    break;
                case ("Shell"):
                    Py.AppendLine("for i in range(1, Config.LastShellLogId):");
                    Py.Append("  "); Py.AppendLine("sess = Session.FromShellLog(i)");

                    Rb.AppendLine("for i in (1..Config.last_shell_log_id)");
                    Rb.Append("  "); Rb.AppendLine("sess = Session.from_shell_log(i)");
                    break;
                case ("Scan"):
                    Py.AppendLine("for i in range(1, Config.LastScanLogId):");
                    Py.Append("  "); Py.AppendLine("sess = Session.FromScanLog(i)");

                    Rb.AppendLine("for i in (1..Config.last_scan_log_id)");
                    Rb.Append("  "); Rb.AppendLine("sess = Session.from_scan_log(i)");
                    break;
                case ("Test"):
                    Py.AppendLine("for i in range(1, Config.LastTestLogId):");
                    Py.Append("  "); Py.AppendLine("sess = Session.FromTestLog(i)");

                    Rb.AppendLine("for i in (1..Config.last_test_log_id)");
                    Rb.Append("  "); Rb.AppendLine("sess = Session.from_test_log(i)");
                    break;
                default:
                    Py.AppendLine(string.Format(@"for i in range(1, Config.GetLastLogId(""{0}"")):", LogSource));
                    Py.Append("  "); Py.AppendLine(string.Format(@"sess = Session.FromLog(i, ""{0}"")", LogSource));

                    Rb.AppendLine(string.Format(@"for i in (1..Config.get_last_log_id(""{0}""))", LogSource));
                    Rb.Append("  "); Rb.AppendLine(string.Format(@"sess = Session.from_log(i, ""{0}"")", LogSource));
                    break;
            }

            Py.Append("  "); Py.AppendLine("#Session object represents a request and its corresponding response.");
            Py.Append("  "); Py.AppendLine("#Sometimes the log might not have a response for a request, for example when the server was not reachable.");
            Py.Append("  "); Py.AppendLine("#So we check and ensure the response object of the sess object is not null");
            Py.Append("  "); Py.AppendLine("if sess.Response:");
            Py.Append("  "); Py.AppendLine("#If response code is 500 we print a message");
            Py.Append("    "); Py.AppendLine("if sess.Response.Code == 500:");
            Py.Append("      "); Py.AppendLine("print 'Response code 500 was found in log id - ' + str(sess.Request.LogId)");
            Py.Append("    "); Py.AppendLine("else:");
            Py.Append("      "); Py.AppendLine("print 'No error in log id - ' + str(sess.Request.LogId)");

            Rb.Append("  "); Rb.AppendLine("#Session object represents a request and its corresponding response.");
            Rb.Append("  "); Rb.AppendLine("#Sometimes the log might not have a response for a request, for example when the server was not reachable.");
            Rb.Append("  "); Rb.AppendLine("#So we check and ensure the response object of the sess object is not null");
            Rb.Append("  "); Rb.AppendLine("if sess.response");
            Rb.Append("  "); Rb.AppendLine("#If response code is 500 we print a message");
            Rb.Append("    "); Rb.AppendLine("if sess.response.code == 500");
            Rb.Append("      "); Rb.AppendLine("puts 'Response code 500 was found in log id - ' + sess.request.log_id.to_s");
            Rb.Append("    "); Rb.AppendLine("else");
            Rb.Append("      "); Rb.AppendLine("puts 'No error in log id - ' + sess.request.log_id.to_s");
            Rb.Append("    "); Rb.AppendLine("end");
            Rb.Append("  "); Rb.AppendLine("end");
            Rb.AppendLine("end");

            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void CrawlCreateCodeBtn_Click(object sender, EventArgs e)
        {

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'req' is a variable that is assumed to contain a Request object and 'res' is a variable that contains the Response recieved after sending 'req' and contains SetCookie headers");
            Py.AppendLine("#'new_req' is a variable that is assumed to contain a Request object to which cookies must be set.");
            Rb.AppendLine("#'req' is a variable that is assumed to contain a Request object and 'res' is a variable that contains the Response recieved after sending 'req' and contains SetCookie headers");
            Rb.AppendLine("#'new_req' is a variable that is assumed to contain a Request object to which cookies must be set.");

            if (CrawlCookiesRB.Checked)
            {
                Py.AppendLine("#For simple cases you can update the cookie of a request directly from the response");
                Py.AppendLine("new_req.SetCookie(res)");
                Rb.AppendLine("#For simple cases you can update the cookie of a request directly from the response");
                Rb.AppendLine("new_req.set_cookie(res)");
                
                Py.AppendLine();
                Py.AppendLine();
                Rb.AppendLine();
                Rb.AppendLine();
                
                Py.AppendLine("#If you want to be precise you can just update the request from one SetCookie header of the response");
                Py.AppendLine("new_req.SetCookie(res.SetCookies[0])");
                Rb.AppendLine("#If you want to be precise you can just update the request from one SetCookie header of the response");
                Rb.AppendLine("new_req.set_cookie(res.set_cookies[0])");
                
                Py.AppendLine();
                Py.AppendLine();
                Rb.AppendLine();
                Rb.AppendLine();

                Py.AppendLine("#When dealing with multiple requests, its best to use CookieStore class");
                Py.AppendLine("#Create a CookieStore");
                Py.AppendLine("cs = CookieStore()");
                Py.AppendLine("#Store cookies in the CookieStore using the Add method. The request and the corresponding response are used as arguments.");
                Py.AppendLine("#The cookies from the SetCookie header of the response are stored in the CookieStore, the hostname and url path  from the request are stored along with the cookies.");
                Py.AppendLine("cs.Add(req, res)");
                Py.AppendLine("#When sending a new request, you can set or update its cookies from the cookie store. The cookie store will only update the request with cookies that match its hostname and url path, so it functions just like a browser's cookie store.");
                Py.AppendLine("new_req.SetCookie(cs)");

                Rb.AppendLine("#When dealing with multiple requests, its best to use CookieStore class");
                Rb.AppendLine("#Create a CookieStore");
                Rb.AppendLine("cs = CookieStore.new()");
                Rb.AppendLine("#Store cookies in the CookieStore using the Add method. The request and the corresponding response are used as arguments.");
                Rb.AppendLine("#The cookies from the SetCookie header of the response are stored in the CookieStore, the hostname and url path  from the request are stored along with the cookies.");
                Rb.AppendLine("cs.add(req, res)");
                Rb.AppendLine("#When sending a new request, you can set or update its cookies from the cookie store. The cookie store will only update the request with cookies that match its hostname and url path, so it functions just like a browser's cookie store.");
                Rb.AppendLine("new_req.set_cookie(cs)");
            }
            else if (CrawlLinksRB.Checked)
            {
                Py.AppendLine("#'cs' is the variable that is assumed to contain a CookieStore object that is handling cookies for these requests.");
                Py.AppendLine("link_requests = Crawler.GetLinkClicks(req, res, cs)");
                Py.AppendLine("#'link_requests' contains a list of Request objects. Each request object in the list is the equivalent of the request created by the browser when someone clicks on one of the links in the HTML");
                Py.AppendLine("#You can loop through this list and send each request. You can also update the parameters of the request before sending it.");
                Py.AppendLine("for link_req in link_requests:");
                Py.Append("  "); Py.AppendLine("print 'Sending request to - ' + link_req.FullUrl");
                Py.Append("  "); Py.AppendLine("link_res = link_req.Send()");

                Rb.AppendLine("#'cs' is the variable that is assumed to contain a CookieStore object that is handling cookies for these requests.");
                Rb.AppendLine("link_requests = Crawler.get_link_clicks(req, res, cs)");
                Rb.AppendLine("#'link_requests' contains a list of Request objects. Each request object in the list is the equivalent of the request created by the browser when someone clicks on one of the links in the HTML");
                Rb.AppendLine("#You can loop through this list and send each request. You can also update the parameters of the request before sending it.");
                Rb.AppendLine("for link_req in link_requests");
                Rb.Append("  "); Rb.AppendLine("puts 'Sending request to - ' + link_req.full_url");
                Rb.Append("  "); Rb.AppendLine("link_res = link_req.send_req");
                Rb.AppendLine("end");
            }
            else if (CrawlFormsRB.Checked)
            {
                Py.AppendLine("#'cs' is the variable that is assumed to contain a CookieStore object that is handling cookies for these requests.");
                Py.AppendLine("form_requests = Crawler.GetFormSubmissions(req, res, cs)");
                Py.AppendLine("#'form_requests' contains a list of Request objects. Each request object in the list is the equivalent of the request created by the browser when someone submits one of the forms in the HTML");
                Py.AppendLine("#You can loop through this list and send each request. You can also update the parameters of the request before sending it.");
                Py.AppendLine("for form_req in form_requests:");
                Py.Append("  "); Py.AppendLine("print 'Submitting form to - ' + form_req.FullUrl");
                Py.Append("  "); Py.AppendLine("form_res = form_req.Send()");

                Rb.AppendLine("#'cs' is the variable that is assumed to contain a CookieStore object that is handling cookies for these requests.");
                Rb.AppendLine("form_requests = Crawler.get_form_submissions(req, res, cs)");
                Rb.AppendLine("#'form_requests' contains a list of Request objects. Each request object in the list is the equivalent of the request created by the browser when someone submits one of the forms in the HTML");
                Rb.AppendLine("#You can loop through this list and send each request. You can also update the parameters of the request before sending it.");
                Rb.AppendLine("for form_req in form_requests");
                Rb.Append("  "); Rb.AppendLine("puts 'Submitting form to - ' + form_req.full_url");
                Rb.Append("  "); Rb.AppendLine("form_res = form_req.send_req");
                Rb.AppendLine("end");
            }
            else
            {
                ShowCode("", "");
                return;
            }

            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void ReqOCreateCodeBtn_Click(object sender, EventArgs e)
        {
            string Action = "";

            foreach (DataGridViewRow Row in ReqOActionsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Action = Row.Cells[2].Value.ToString();
                    ReqODescTB.Text = Row.Cells[3].Value.ToString();
                    break;
                }
            }
            

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Rb.AppendLine("#'req' is a variable that is assumed to contain a Request object");

            switch (Action)
            {
                case ("ToBinaryString"):
                    Py.AppendLine("#Convert the request to string. The string is specially formatted to preseve even binary content inside the request body.");
                    Py.AppendLine("#And it is a one-line string so you can easily embbed it in your scripts");
                    Py.AppendLine("req_str = req.ToBinaryString()");
                    Py.AppendLine("print 'The string representation of the request is :'");
                    Py.AppendLine("print req_str");
                    Py.AppendLine("#It is also possible to easily get the request object back from this string");
                    Py.AppendLine("same_req = Request.FromBinaryString(req_str)");

                    Rb.AppendLine("#Convert the request to string. The string is specially formatted to preseve even binary content inside the request body.");
                    Rb.AppendLine("#And it is a one-line string so you can easily embbed it in your scripts");
                    Rb.AppendLine("req_str = req.to_binary_string");
                    Rb.AppendLine("puts 'The string representation of the request is :'");
                    Rb.AppendLine("puts req_str");
                    Rb.AppendLine("#It is also possible to easily get the request object back from this string");
                    Rb.AppendLine("same_req = Request.from_binary_string(req_str)");
                    break;
                case ("ToTestUi"):
                    Py.AppendLine("#Create a new group in the ManualTesting section and display this request there.");
                    Py.AppendLine("name = req.ToTestUi()");
                    Py.AppendLine("print 'Request sent to ManualTesting section. Group name is :' + name");
                    Py.AppendLine("#You can also set a name to this group when sending the request. If this name is not valid or if it already exists then another name is automatically picked and used");
                    Py.AppendLine("name = req.ToTestUi('somename')");
                    Py.AppendLine("print 'Request sent to ManualTesting section. Group name is :' + name");

                    Rb.AppendLine("#Create a new group in the ManualTesting section and display this request there.");
                    Rb.AppendLine("name = req.to_test_ui");
                    Rb.AppendLine("puts 'Request sent to ManualTesting section. Group name is :' + name");
                    Rb.AppendLine("#You can also set a name to this group when sending the request. If this name is not valid or if it already exists then another name is automatically picked and used");
                    Rb.AppendLine("name = req.ToTestUi('somename')");
                    Rb.AppendLine("puts 'Request sent to ManualTesting section. Group name is :' + name");
                    break;
                case ("GetClone"):
                    Py.AppendLine("#Create a cloned copy of a request object");
                    Py.AppendLine("cloned_req = req.GetClone()");
                    Py.AppendLine("#'cloned_req' is exactly identical copy of 'req'");
                    

                    Rb.AppendLine("#Create a cloned copy of a request object");
                    Rb.AppendLine("cloned_req = req.get_clone");
                    Rb.AppendLine("#'cloned_req' is exactly identical copy of 'req'");
                    break;
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void ResOCreateCodeBtn_Click(object sender, EventArgs e)
        {
            string Action = "";

            foreach (DataGridViewRow Row in ResOActionsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Action = Row.Cells[2].Value.ToString();
                    ResODescTB.Text = Row.Cells[3].Value.ToString();
                    break;
                }
            }

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'res' is a variable that is assumed to contain a Response object");
            Rb.AppendLine("#'res' is a variable that is assumed to contain a Response object");

            switch (Action)
            {
                case ("Render"):
                    Py.AppendLine("#Will start an another application to display the page.");
                    Py.AppendLine("res.Render()");

                    Rb.AppendLine("#Will start an another application to display the page.");
                    Rb.AppendLine("res.render"); 
                    break;
                case ("Save"):
                    Py.AppendLine("#Save the body of the response to a file named image.jpg in c:\\ drive");
                    Py.AppendLine(@"res.SaveBody(""c:\\image.jpg"")");

                    Rb.AppendLine("#Save the body of the response to a file named image.jpg in c:\\ drive");
                    Rb.AppendLine(@"res.save_body(""c:\\image.jpg"")");
                    break;
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void ResOActionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ResOActionsGrid.SelectedRows == null) return;
            if (ResOActionsGrid.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow Row in ResOActionsGrid.Rows)
            {
                if (Row.Index == ResOActionsGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    ResOCreateCodeBtn.Enabled = true;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ReqOActionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ReqOActionsGrid.SelectedRows == null) return;
            if (ReqOActionsGrid.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow Row in ReqOActionsGrid.Rows)
            {
                if (Row.Index == ReqOActionsGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    ReqOCreateCodeBtn.Enabled = true;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ToolsItemGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ToolsItemGrid.SelectedRows == null) return;
            if (ToolsItemGrid.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow Row in ToolsItemGrid.Rows)
            {
                if (Row.Index == ToolsItemGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    ToolsCreateCodeBtn.Enabled = true;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void LogSourceGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LogSourceGrid.SelectedRows == null) return;
            if (LogSourceGrid.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow Row in LogSourceGrid.Rows)
            {
                if (Row.Index == LogSourceGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    LogCreateCodeBtn.Enabled = true;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ToolsCreateCodeBtn_Click(object sender, EventArgs e)
        {
            string Action = "";

            foreach (DataGridViewRow Row in ToolsItemGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    Action = Row.Cells[2].Value.ToString();
                    break;
                }
            }

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'res' is a variable that is assumed to contain a Response object");
            Rb.AppendLine("#'res' is a variable that is assumed to contain a Response object");

            switch (Action)
            {
                case ("path"):
                    Py.AppendLine("print 'the location of IronWASP executable is ' + Config.Path");

                    Rb.AppendLine("puts 'the location of IronWASP executable is ' + Config.path");
                    break;
                case ("base64"):
                    Py.AppendLine("#Base64 encode 'abcd'");
                    Py.AppendLine("enc_value = Tools.Base64Encode('abcd')");
                    Py.AppendLine("#now Base64 decode the encoded value");
                    Py.AppendLine("dec_value = Tools.Base64Decode(enc_value)");

                    Rb.AppendLine("#Base64 encode 'abcd'");
                    Rb.AppendLine("enc_value = Tools.base64_encode('abcd')");
                    Rb.AppendLine("#now Base64 decode the encoded value");
                    Rb.AppendLine("dec_value = Tools.base64_decode(enc_value)");
                    break;
                case ("hex"):
                    Py.AppendLine("#Hex encode 'abcd'");
                    Py.AppendLine("enc_value = Tools.HexEncode('abcd')");
                    Py.AppendLine("#now Hex decode the encoded value");
                    Py.AppendLine("dec_value = Tools.HexDecode(enc_value)");

                    Rb.AppendLine("#Hex encode 'abcd'");
                    Rb.AppendLine("enc_value = Tools.hex_encode('abcd')");
                    Rb.AppendLine("#now Hex decode the encoded value");
                    Rb.AppendLine("dec_value = Tools.hex_decode(enc_value)");
                    break;
                case ("html"):
                    Py.AppendLine("#Html encode '<abcd>'");
                    Py.AppendLine("enc_value = Tools.HtmlEncode('<abcd>')");
                    Py.AppendLine("#now Html decode the encoded value");
                    Py.AppendLine("dec_value = Tools.HtmlDecode(enc_value)");

                    Rb.AppendLine("#Html encode '<abcd>'");
                    Rb.AppendLine("enc_value = Tools.html_encode('<abcd>')");
                    Rb.AppendLine("#now Html decode the encoded value");
                    Rb.AppendLine("dec_value = Tools.html_decode(enc_value)");
                    break;
                case ("url"):
                    Py.AppendLine("#Url encode '<abcd>'");
                    Py.AppendLine("enc_value = Tools.UrlEncode('<abcd>')");
                    Py.AppendLine("#now Url decode the encoded value");
                    Py.AppendLine("dec_value = Tools.UrlDecode(enc_value)");

                    Rb.AppendLine("#Url encode '<abcd>'");
                    Rb.AppendLine("enc_value = Tools.url_encode('<abcd>')");
                    Rb.AppendLine("#now Url decode the encoded value");
                    Rb.AppendLine("dec_value = Tools.url_decode(enc_value)");
                    break;
                case ("hash"):
                    Py.AppendLine("#Create MD5 hash of 'abcd'");
                    Py.AppendLine(@"print ""MD5 of 'abcd' is "" + Tools.md5('abcd')");
                    Py.AppendLine("#Create SHA1 hash of 'abcd'");
                    Py.AppendLine(@"print ""SHA1 of 'abcd' is "" + Tools.sha1('abcd')");
                    Py.AppendLine("#Create SHA256 hash of 'abcd'");
                    Py.AppendLine(@"print ""SHA256 of 'abcd' is "" + Tools.sha256('abcd')");
                    Py.AppendLine("#Create SHA384 hash of 'abcd'");
                    Py.AppendLine(@"print ""SHA384 of 'abcd' is "" + Tools.sha384('abcd')");
                    Py.AppendLine("#Create SHA512 hash of 'abcd'");
                    Py.AppendLine(@"print ""SHA512 of 'abcd' is "" + Tools.sha512('abcd')");

                    Rb.AppendLine("#Create MD5 hash of 'abcd'");
                    Rb.AppendLine(@"puts ""MD5 of 'abcd' is "" + Tools.md5('abcd')");
                    Rb.AppendLine("#Create SHA1 hash of 'abcd'");
                    Rb.AppendLine(@"puts ""SHA1 of 'abcd' is "" + Tools.sha1('abcd')");
                    Rb.AppendLine("#Create SHA256 hash of 'abcd'");
                    Rb.AppendLine(@"puts ""SHA256 of 'abcd' is "" + Tools.sha256('abcd')");
                    Rb.AppendLine("#Create SHA384 hash of 'abcd'");
                    Rb.AppendLine(@"puts ""SHA384 of 'abcd' is "" + Tools.sha384('abcd')");
                    Rb.AppendLine("#Create SHA512 hash of 'abcd'");
                    Rb.AppendLine(@"puts ""SHA512 of 'abcd' is "" + Tools.sha512('abcd')");
                    break;
                case ("diff"):
                    Py.AppendLine("value_one = 'aa bb cc dd'");
                    Py.AppendLine("value_two = 'aa bx cc dd'");
                    Py.AppendLine("print 'The percentage of differnce between the two strings is - ' + str(Tools.DiffLevel(value_one, value_two)) + '%'");
                    
                    Rb.AppendLine("value_one = 'aa bb cc dd'");
                    Rb.AppendLine("value_two = 'aa bx cc dd'");
                    Rb.AppendLine("puts 'The percentage of differnce between the two strings is - ' + Tools.diff_level(value_one, value_two).to_s + '%'");
                    break;
                case ("exec"):
                    Py.AppendLine("#Run an external executable named test.exe located inside c:\\ drive");
                    Py.AppendLine(@"Tools.Run(""c:\  ext.exe"")");
                    Py.AppendLine("#Run an external executable named test.exe located inside c:\\ drive and pass it some arguments");
                    Py.AppendLine(@"Tools.RunWith(""c:\  ext.exe"", ""-h 127.0.0.1"")");

                    Rb.AppendLine("#Run an external executable named test.exe located inside c:\\ drive");
                    Rb.AppendLine(@"Tools.run(""c:\  ext.exe"")");
                    Rb.AppendLine("#Run an external executable named test.exe located inside c:\\ drive and pass it some arguments");
                    Rb.AppendLine(@"Tools.run_with(""c:\  ext.exe"", ""-h 127.0.0.1"")");
                    break;
                case ("debug"):
                    Py.AppendLine("#Debug plugins and scripts by printing trace messages. This similar to using the print command for debugging");
                    Py.AppendLine("#The trace messages are listed in the 'Trace' area of the 'Dev' section");
                    Py.AppendLine(@"Tools.Trace(""aaa"", ""bbb"")");
                    Py.AppendLine("#Here aaa and bbb are just demo strings, you can pass any string value you want here.");

                    Rb.AppendLine("#Debug plugins and scripts by printing trace messages. This similar to using the print command for debugging");
                    Rb.AppendLine("#The trace messages are listed in the 'Trace' area of the 'Dev' section");
                    Rb.AppendLine(@"Tools.trace(""aaa"", ""bbb"")");
                    Rb.AppendLine("#Here aaa and bbb are just demo strings, you can pass any string value you want here.");
                    break;
            }
            ShowCode(Py.ToString(), Rb.ToString());
        }




        int FuzzCurrentStep = 0;
        Dictionary<string, string[]> FuzzInjectionPoints = new Dictionary<string, string[]>();
        string[] FuzzPayloads = new string[]{};
        FileInfo FuzzPayloadsFile = null;
        string FuzzInjectedBodyType = "";
        string FuzzInjectedBodyFormatPlugin = "";
        string FuzzLogSourceValue = "";

        private void FuzzUseCustomLogSourceCB_CheckedChanged(object sender, EventArgs e)
        {
            FuzzLogSourceTB.Enabled = FuzzUseCustomLogSourceCB.Checked;
        }

        private void FuzzStepZeroNextBtn_Click(object sender, EventArgs e)
        {
            FuzzInjectionPoints.Clear();

            if (FuzzUseUiRB.Checked)
            {
                FuzzSessionPluginGrid.Visible = false;
                FuzzSessionPluginMsgTB.Visible = false;
                
                this.FuzzCurrentStep = 2;
                this.FuzzBaseTabs.SelectTab(2);
            }
            else
            {
                UpdateSelectedFuzzInjectionPointsList();
                FuzzSessionPluginGrid.Visible = true;
                FuzzSessionPluginMsgTB.Visible = true;

                this.FuzzCurrentStep = 1;
                this.FuzzBaseTabs.SelectTab(1);
            }
        }

        private void FuzzStepOnePreviousBtn_Click(object sender, EventArgs e)
        {
            this.FuzzCurrentStep = 0;
            this.FuzzBaseTabs.SelectTab(0);
        }

        private void FuzzStepOneNextBtn_Click(object sender, EventArgs e)
        {
            string Error = CheckFuzzStep1Input();
            if (Error.Length == 0)
            {
                this.FuzzCurrentStep = 2;
                this.FuzzBaseTabs.SelectTab(2);
            }
            else
            {
                ShowFuzzStep1Error(Error);
            }
        }

        private void FuzzStepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            if (FuzzUseUiRB.Checked)
            {
                this.FuzzCurrentStep = 0;
                this.FuzzBaseTabs.SelectTab(0);
            }
            else
            {
                this.FuzzCurrentStep = 1;
                this.FuzzBaseTabs.SelectTab(1);
            }
        }

        private void FuzzStepTwoNextBtn_Click(object sender, EventArgs e)
        {
            string Error = CheckFuzzStep2Input();
            if (Error.Length == 0)
            {
                this.FuzzCurrentStep = 3;
                this.FuzzBaseTabs.SelectTab(3);
            }
            else
            {
                ShowFuzzStep2Error(Error);
            }
        }

        private void FuzzStepThreePreviousBtn_Click(object sender, EventArgs e)
        {
            this.FuzzCurrentStep = 2;
            this.FuzzBaseTabs.SelectTab(2);
        }

        private void FuzzSessionPluginGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FuzzSessionPluginGrid.SelectedRows == null) return;
            if (FuzzSessionPluginGrid.SelectedRows.Count == 0) return;

            foreach (DataGridViewRow Row in FuzzSessionPluginGrid.Rows)
            {
                if (Row.Index == FuzzSessionPluginGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void FuzzBaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (FuzzBaseTabs.SelectedIndex != this.FuzzCurrentStep)
                FuzzBaseTabs.SelectTab(this.FuzzCurrentStep);
        }

        void ShowFuzzStep1Error(string Text)
        {
            this.FuzzStep1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.FuzzStep1StatusTB.Visible = false;
            }
            else
            {
                this.FuzzStep1StatusTB.ForeColor = Color.Red;
                this.FuzzStep1StatusTB.Visible = true;
            }
        }

        void ShowFuzzStep2Error(string Text)
        {
            this.FuzzStep2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.FuzzStep2StatusTB.Visible = false;
            }
            else
            {
                this.FuzzStep2StatusTB.ForeColor = Color.Red;
                this.FuzzStep2StatusTB.Visible = true;
            }
        }

        void ShowFuzzStep3Error(string Text)
        {
            this.FuzzStep3StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.FuzzStep3StatusTB.Visible = false;
            }
            else
            {
                this.FuzzStep3StatusTB.ForeColor = Color.Red;
                this.FuzzStep3StatusTB.Visible = true;
            }
        }

        private void FuzzParameterTypeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FuzzParameterTypeGrid.SelectedRows == null) return;
            if (FuzzParameterTypeGrid.SelectedRows.Count == 0) return;

            FuzzBodyTypeGB.Visible = false;
            FuzzAllParametersRB.Visible = false;
            FuzzAllParametersRB.Checked = true;
            FuzzListedParametersRB.Visible = false;
            FuzzParametersNameListLbl.Visible = false;
            FuzzParametersNameListTB.Visible = false;
            FuzzAddPointLL.Visible = false;
            
            foreach (DataGridViewRow Row in FuzzParameterTypeGrid.Rows)
            {
                if (Row.Index == FuzzParameterTypeGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    switch (Row.Cells[1].Value.ToString())
                    {
                        case ("UrlPathParts"):
                            FuzzAllParametersRB.Visible = true;
                            FuzzListedParametersRB.Visible = true;
                            FuzzParametersNameListLbl.Visible = true;
                            FuzzParametersNameListTB.Visible = true;
                            FuzzAddPointLL.Visible = true;
                            FuzzAllParametersRB.Text = "Fuzz all UrlPathPart positions";
                            FuzzListedParametersRB.Text = "Fuzz only UrlPathPart positions listed below";
                            FuzzParametersNameListLbl.Text = "Enter the zero-based index positions one per line:";
                            break;
                        case ("Query"):
                            FuzzAllParametersRB.Visible = true;
                            FuzzListedParametersRB.Visible = true;
                            FuzzParametersNameListLbl.Visible = true;
                            FuzzParametersNameListTB.Visible = true;
                            FuzzAddPointLL.Visible = true;
                            FuzzAllParametersRB.Text = "Fuzz all Query Parameters";
                            FuzzListedParametersRB.Text = "Fuzz only Query Parameters listed below";
                            FuzzParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                            break;
                        case ("Body"):
                            FuzzBodyTypeGrid.Rows.Clear();
                            FuzzBodyTypeGrid.Rows.Add(false, "Normal");
                            foreach(string Name in FormatPlugin.List())
                            {
                                FuzzBodyTypeGrid.Rows.Add(false, Name);
                            }
                            FuzzBodyTypeGrid.Rows.Add(false, "Other Unknown/Custom");
                            FuzzBodyTypeGrid.Visible = true;

                            FuzzBodyCustomMsgTB.Visible = false;
                            FuzzBodyCustomStartLbl.Visible = false;
                            FuzzBodyCustomStartTB.Visible = false;
                            FuzzBodyCustomEndLbl.Visible = false;
                            FuzzBodyCustomEndTB.Visible = false;

                            FuzzBodyTypeGB.Visible = true;

                            break;
                        case ("Cookie"):
                            FuzzAllParametersRB.Visible = true;
                            FuzzListedParametersRB.Visible = true;
                            FuzzParametersNameListLbl.Visible = true;
                            FuzzParametersNameListTB.Visible = true;
                            FuzzAddPointLL.Visible = true;
                            FuzzAllParametersRB.Text = "Fuzz all Cookie Parameters";
                            FuzzListedParametersRB.Text = "Fuzz only Cookie Parameters listed below";
                            FuzzParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                            break;
                        case ("Headers"):
                            FuzzAllParametersRB.Visible = true;
                            FuzzListedParametersRB.Visible = true;
                            FuzzParametersNameListLbl.Visible = true;
                            FuzzParametersNameListTB.Visible = true;
                            FuzzAddPointLL.Visible = true;
                            FuzzAllParametersRB.Text = "Fuzz all Header Parameters";
                            FuzzListedParametersRB.Text = "Fuzz only Header Parameters listed below";
                            FuzzParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                            break;
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void FuzzBodyTypeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FuzzBodyTypeGrid.SelectedRows == null) return;
            if (FuzzBodyTypeGrid.SelectedRows.Count == 0) return;

            FuzzAllParametersRB.Visible = false;
            FuzzAllParametersRB.Checked = true;
            FuzzListedParametersRB.Visible = false;
            FuzzParametersNameListLbl.Visible = false;
            FuzzParametersNameListTB.Visible = false;
            FuzzAddPointLL.Visible = false;
            
            FuzzBodyCustomMsgTB.Visible = false;
            FuzzBodyCustomStartLbl.Visible = false;
            FuzzBodyCustomStartTB.Visible = false;
            FuzzBodyCustomEndLbl.Visible = false;
            FuzzBodyCustomEndTB.Visible = false;

            foreach (DataGridViewRow Row in FuzzBodyTypeGrid.Rows)
            {
                if (Row.Index == FuzzBodyTypeGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    if (Row.Index == 0)
                    {
                        FuzzAllParametersRB.Visible = true;
                        FuzzListedParametersRB.Visible = true;
                        FuzzParametersNameListLbl.Visible = true;
                        FuzzParametersNameListTB.Visible = true;
                        FuzzAddPointLL.Visible = true;
                        FuzzAllParametersRB.Text = "Fuzz all Body Parameters";
                        FuzzListedParametersRB.Text = "Fuzz only Body Parameters listed below";
                        FuzzParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                    }
                    else if (Row.Index == FuzzBodyTypeGrid.Rows.Count - 1)
                    {
                        FuzzBodyCustomMsgTB.Visible = true;
                        FuzzBodyCustomStartLbl.Visible = true;
                        FuzzBodyCustomStartTB.Visible = true;
                        FuzzBodyCustomEndLbl.Visible = true;
                        FuzzBodyCustomEndTB.Visible = true;
                        FuzzAddPointLL.Visible = true;
                    }
                    else
                    {
                        FuzzAllParametersRB.Visible = true;
                        FuzzListedParametersRB.Visible = true;
                        FuzzParametersNameListLbl.Visible = true;
                        FuzzParametersNameListTB.Visible = true;
                        FuzzAddPointLL.Visible = true;
                        FuzzAllParametersRB.Text = string.Format("Fuzz all {0} Values", Row.Cells[1].Value.ToString());
                        FuzzListedParametersRB.Text = string.Format("Fuzz only {0} Values at listed indexes", Row.Cells[1].Value.ToString());
                        FuzzParametersNameListLbl.Text = "Enter the zero-based index positions of values one per line:";
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void FuzzListedParametersRB_CheckedChanged(object sender, EventArgs e)
        {
            FuzzParametersNameListTB.Enabled = FuzzListedParametersRB.Checked;
        }

        private void FuzzAddPointLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowFuzzStep1Error("");
            
            string ParameterType = "";
            string BodyType = "";
            string BodyFormatPlugin = "";
            string StartMarker = FuzzBodyCustomStartTB.Text;
            string EndMarker = FuzzBodyCustomEndTB.Text;
            string[] ParametersList = FuzzParametersNameListTB.Text.Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach(DataGridViewRow Row in FuzzParameterTypeGrid.Rows)
            {
                if((bool)Row.Cells[0].Value)
                {
                    ParameterType = Row.Cells[1].Value.ToString();
                    break;
                }
            }
            if (ParameterType.Equals("Body"))
            {
                foreach (DataGridViewRow Row in FuzzBodyTypeGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        if (Row.Index == 0)
                        {
                            BodyType = "Normal";
                        }
                        else if (Row.Index == FuzzBodyTypeGrid.Rows.Count - 1)
                        {
                            BodyType = "Other";
                        }
                        else
                        {
                            BodyType = "FormatPlugin";
                            BodyFormatPlugin = Row.Cells[1].Value.ToString();
                        }
                        break;
                    }
                }
            }

            switch (ParameterType)
            {
                case("UrlPathParts"):
                    if(FuzzAllParametersRB.Checked)
                    {
                        FuzzInjectionPoints["UrlPathParts"] = new string[]{};
                    }
                    else
                    {
                        foreach (string P in ParametersList)
                        {
                            try
                            {
                                Int32.Parse(P);
                            }
                            catch 
                            {
                                ShowFuzzStep1Error("UrlPathParts position must be a number");
                                return;
                            }
                        }
                        if (ParametersList.Length == 0)
                        {
                            ShowFuzzStep1Error("UrlPathParts positions list cannot be empty. Mention atleast one position.");
                            return;
                        }
                        FuzzInjectionPoints["UrlPathParts"] = ParametersList;
                    }
                    break;
                case ("Query"):
                    if (FuzzAllParametersRB.Checked)
                    {
                        FuzzInjectionPoints["Query"] = new string[] { };
                    }
                    else
                    {
                        if (ParametersList.Length == 0)
                        {
                            ShowFuzzStep1Error("Query parameter names list cannot be empty. Mention atleast one parameter.");
                            return;
                        }
                        FuzzInjectionPoints["Query"] = ParametersList;
                    }
                    break;
                case ("Body"):
                    FuzzInjectedBodyType = BodyType;
                    FuzzInjectedBodyFormatPlugin = "";
                    switch (BodyType)
                    {
                        case("Normal"):
                            if (FuzzAllParametersRB.Checked)
                            {
                                FuzzInjectionPoints["Body"] = new string[] { };
                            }
                            else
                            {
                                if (ParametersList.Length == 0)
                                {
                                    ShowFuzzStep1Error("Body parameter names list cannot be empty. Mention atleast one parameter.");
                                    return;
                                }
                                FuzzInjectionPoints["Body"] = ParametersList;
                            }
                            break;
                        case ("Other"):
                            if (StartMarker.Length == 0 || EndMarker.Length == 0)
                            {
                                ShowFuzzStep1Error("Start or End markers cannot be empty.");
                                return;
                            }
                            if (StartMarker.Equals(EndMarker))
                            {
                                ShowFuzzStep1Error("Start and End markers cannot be the same.");
                                return;
                            }
                            FuzzInjectionPoints["Body"] = new string[] { StartMarker, EndMarker };
                            break;
                        case ("FormatPlugin"):
                            FuzzInjectedBodyFormatPlugin = BodyFormatPlugin;
                            if (FuzzAllParametersRB.Checked)
                            {
                                FuzzInjectionPoints["Body"] = new string[] { };
                            }
                            else
                            {
                                foreach (string P in ParametersList)
                                {
                                    try
                                    {
                                        Int32.Parse(P);
                                    }
                                    catch
                                    {
                                        ShowFuzzStep1Error(string.Format("{0} values position must be a number", BodyFormatPlugin));
                                        return;
                                    }
                                }
                                if (ParametersList.Length == 0)
                                {
                                    ShowFuzzStep1Error(string.Format("{0} positions list cannot be empty. Mention atleast one position.", BodyFormatPlugin));
                                    return;
                                }
                                FuzzInjectionPoints["Body"] = ParametersList;
                            }
                            break;
                    }
                    break;
                case ("Cookie"):
                    if (FuzzAllParametersRB.Checked)
                    {
                        FuzzInjectionPoints["Cookie"] = new string[] { };
                    }
                    else
                    {
                        if (ParametersList.Length == 0)
                        {
                            ShowFuzzStep1Error("Cookie parameter names list cannot be empty. Mention atleast one parameter.");
                            return;
                        }
                        FuzzInjectionPoints["Cookie"] = ParametersList;
                    }
                    break;
                case ("Headers"):
                    if (FuzzAllParametersRB.Checked)
                    {
                        FuzzInjectionPoints["Headers"] = new string[] { };
                    }
                    else
                    {
                        if (ParametersList.Length == 0)
                        {
                            ShowFuzzStep1Error("Headers parameter names list cannot be empty. Mention atleast one parameter.");
                            return;
                        }
                        FuzzInjectionPoints["Headers"] = ParametersList;
                    }
                    break;
            }
            FuzzBodyTypeGB.Visible = false;
            FuzzAllParametersRB.Visible = false;
            FuzzListedParametersRB.Visible = false;
            FuzzParametersNameListLbl.Visible = false;
            FuzzParametersNameListTB.Visible = false;
            FuzzAddPointLL.Visible = false;

            foreach (DataGridViewRow Row in FuzzParameterTypeGrid.Rows)
            {
                Row.Cells[0].Value = false;
            }

            UpdateSelectedFuzzInjectionPointsList();
        }

        void UpdateSelectedFuzzInjectionPointsList()
        {
            StringBuilder SB = new StringBuilder("Selected Injection Points:\r\n");
            if (FuzzInjectionPoints.Count > 0)
            {
                foreach (string Section in FuzzInjectionPoints.Keys)
                {
                    SB.Append(Section); SB.Append(", ");
                }
            }
            else
            {
                SB.Append("-");
            }
            FuzzSelectedParametersListTB.Text = SB.ToString().TrimEnd().TrimEnd(',');

        }

        string CheckFuzzStep1Input()
        {
            if (FuzzInjectionPoints.Count == 0)
            {
                return "No injection points were selected. Select atleast one injection point before proceeding.";
            }
            return "";
        }

        string CheckFuzzStep2Input()
        {
            if (FuzzLoadPayloadsFromFileRB.Checked)
            {
                if (FuzzPayloadsFile == null || FuzzPayloadsFileLbl.Text.Trim().Length == 0)
                {
                    return "No file was selected to load payloads";
                }
            }
            else
            {
                FuzzPayloads = FuzzPayloadsListTB.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                if (FuzzPayloads.Length == 0)
                {
                    return "No payloads were entered by you.";
                }
            }
            return "";
        }

        private void LoadPayloadsFileLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            while (PayloadFileOpener.ShowDialog() == DialogResult.OK)
            {
                FuzzPayloadsFile = new FileInfo(PayloadFileOpener.FileName);
                try
                {
                    StreamReader SR = new StreamReader(FuzzPayloadsFile.FullName);
                    FuzzPayloadsListTB.Text = SR.ReadToEnd();
                    SR.Close();
                    FuzzPayloadsFileLbl.Text = FuzzPayloadsFile.FullName;
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(string.Format("Error reading selected file: {0}", Exp));
                }
                break;
            }
        }

        private void FuzzLoadPayloadsFromFileRB_CheckedChanged(object sender, EventArgs e)
        {
            FuzzLoadPayloadsFileLL.Enabled = FuzzLoadPayloadsFromFileRB.Checked;
        }

        private void FuzzUsePayloadsFromListRB_CheckedChanged(object sender, EventArgs e)
        {
            FuzzPayloadsListTB.Enabled = FuzzUsePayloadsFromListRB.Checked;
        }

        private void FuzzCreateCodeBtn_Click(object sender, EventArgs e)
        {
            ShowFuzzStep3Error("");
            string SessionPluginName = "";
            if (FuzzUseCustomLogSourceCB.Checked)
            {
                if (FuzzLogSourceTB.Text.Trim().Length > 0)
                {
                    try
                    {
                        Request Req = new Request("http://a.site");
                        Req.SetSource(FuzzLogSourceTB.Text.Trim());
                        FuzzLogSourceValue = FuzzLogSourceTB.Text.Trim();
                    }
                    catch(Exception Exp)
                    {
                        ShowFuzzStep3Error(string.Format("Invalid Log source - {0}", Exp.Message));
                        return;
                    }
                }
                else
                {
                    ShowFuzzStep3Error("Log source cannot be empty. Either uncheck this option or enter a valid log source");
                    return;
                }
            }

            foreach (DataGridViewRow Row in FuzzSessionPluginGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    if (Row.Index == 0)
                    {
                        SessionPluginName = "";
                    }
                    else
                    {
                        SessionPluginName = Row.Cells[1].Value.ToString();
                    }
                    break;
                }
            }

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Rb.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Py.AppendLine();
            Rb.AppendLine();

            if (FuzzUseUiRB.Checked)
            {
                Py.AppendLine("#We display a GUI based wizard to user and get the Fuzzer setting from user.");
                Py.AppendLine("f = Fuzzer.FromUi(req)");

                Rb.AppendLine("#We display a GUI based wizard to user and get the Fuzzer setting from user.");
                Rb.AppendLine("f = Fuzzer.FromUi(req)");
            }
            else
            {
                Py.AppendLine("#We create a new Fuzzer to fuzz the request 'req'");
                Py.AppendLine("f = Fuzzer(req)");

                Rb.AppendLine("#We create a new Fuzzer to fuzz the request 'req'");
                Rb.AppendLine("f = Fuzzer.new(req)");

                if (FuzzInjectionPoints.ContainsKey("UrlPathParts"))
                {
                    if (FuzzInjectionPoints["UrlPathParts"].Length == 0)
                    {
                        Py.AppendLine("#Select all UrlPathparts for injection");
                        Py.AppendLine("f.InjectUrl()");

                        Rb.AppendLine("#Select all UrlPathparts for injection");
                        Rb.AppendLine("f.inject_url");
                    }
                    else
                    {
                        Py.AppendLine("#Select the UrlPathpart at specified positions for injection");
                        Rb.AppendLine("#Select the UrlPathpart at specified positions for injection");
                        foreach (string Position in FuzzInjectionPoints["UrlPathParts"])
                        {
                            Py.AppendLine(string.Format("f.InjectUrl({0})", Position.Trim()));
                            
                            Rb.AppendLine(string.Format("f.inject_url({0})", Position.Trim()));
                        }
                    }
                }
                if (FuzzInjectionPoints.ContainsKey("Query"))
                {
                    if (FuzzInjectionPoints["Query"].Length == 0)
                    {
                        Py.AppendLine("#Select all Query parameters for injection");
                        Py.AppendLine("f.InjectQuery()");

                        Rb.AppendLine("#Select all Query parameters for injection");
                        Rb.AppendLine("f.inject_query()");
                    }
                    else
                    {
                        Py.AppendLine("#Select the specified Query parameters for injection");
                        Rb.AppendLine("#Select the specified Query parameters for injection");
                        foreach (string Parameter in FuzzInjectionPoints["Query"])
                        {
                            Py.AppendLine(string.Format(@"f.InjectQuery(""{0}"")", Parameter.Replace("\"", "\\\"")));
                            
                            Rb.AppendLine(string.Format(@"f.inject_query(""{0}"")", Parameter.Replace("\"", "\\\"")));
                        }
                    }
                }
                if (FuzzInjectionPoints.ContainsKey("Body"))
                {
                    switch(FuzzInjectedBodyType)
                    {
                        case ("Normal"):
                            if (FuzzInjectionPoints["Body"].Length == 0)
                            {
                                Py.AppendLine("#Select all Body parameters for injection");
                                Py.AppendLine("f.InjectBody()");

                                Rb.AppendLine("#Select all Body parameters for injection");
                                Rb.AppendLine("f.inject_body");
                            }
                            else
                            {
                                Py.AppendLine("#Select the specified Body parameters for injection");
                                Rb.AppendLine("#Select the specified Body parameters for injection");
                                foreach (string Parameter in FuzzInjectionPoints["Body"])
                                {
                                    Py.AppendLine(string.Format(@"f.InjectBody(""{0}"")", Parameter.Replace("\"", "\\\"")));
                                    
                                    Rb.AppendLine(string.Format(@"f.inject_body(""{0}"")", Parameter.Replace("\"", "\\\"")));
                                }
                            }
                            break;
                        case ("Other"):
                            Py.AppendLine("#Inject values between the specified start and end marker");
                            Py.AppendLine(string.Format(@"f.InjectBody(""{0}"", ""{1}"")", FuzzInjectionPoints["Body"][0].Replace("\"", "\\\""), FuzzInjectionPoints["Body"][1].Replace("\"", "\\\"")));

                            Rb.AppendLine("#Inject values between the specified start and end marker");
                            Rb.AppendLine(string.Format(@"f.inject_body(""{0}"", ""{1}"")", FuzzInjectionPoints["Body"][0].Replace("\"", "\\\""), FuzzInjectionPoints["Body"][1].Replace("\"", "\\\"")));
                            break;
                        case ("FormatPlugin"):
                            Py.AppendLine("#Specify the body format of the Request");
                            Py.AppendLine(string.Format(@"f.BodyFormat = FormatPlugin.Get(""{0}"")", FuzzInjectedBodyFormatPlugin));

                            Rb.AppendLine("#Specify the body format of the Request");
                            Rb.AppendLine(string.Format(@"f.body_format = FormatPlugin.get(""{0}"")", FuzzInjectedBodyFormatPlugin));

                            if (FuzzInjectionPoints["Body"].Length == 0)
                            {
                                Py.AppendLine("#Select all values for injection");
                                Py.AppendLine("f.InjectBody()");

                                Rb.AppendLine("#Select all values for injection");
                                Rb.AppendLine("f.inject_body");
                            }
                            else
                            {
                                Py.AppendLine("#Select value at the specified positions for injection");
                                Rb.AppendLine("#Select value at the specified positions for injection");
                                foreach (string Parameter in FuzzInjectionPoints["Body"])
                                {
                                    Py.AppendLine(string.Format("f.InjectBody({0})", Parameter.Trim()));
                                    
                                    Rb.AppendLine(string.Format("f.inject_body({0})", Parameter.Trim()));
                                }
                            }
                            break;
                    }
                    
                }
                if (FuzzInjectionPoints.ContainsKey("Cookie"))
                {
                    if (FuzzInjectionPoints["Cookie"].Length == 0)
                    {
                        Py.AppendLine("#Select all Cookie parameters for injection");
                        Py.AppendLine("f.InjectCookie()");

                        Rb.AppendLine("#Select all Cookie parameters for injection");
                        Rb.AppendLine("f.inject_cookie)");
                    }
                    else
                    {
                        Py.AppendLine("#Select the specified Cookie parameters for injection");
                        Rb.AppendLine("#Select the specified Cookie parameters for injection");
                        foreach (string Parameter in FuzzInjectionPoints["Cookie"])
                        {
                            Py.AppendLine(string.Format(@"f.InjectCookie(""{0}"")", Parameter.Replace("\"", "\\\"")));
                            
                            Rb.AppendLine(string.Format(@"f.inject_cookie(""{0}"")", Parameter.Replace("\"", "\\\"")));
                        }
                    }
                }
                if (FuzzInjectionPoints.ContainsKey("Headers"))
                {
                    if (FuzzInjectionPoints["Query"].Length == 0)
                    {
                        Py.AppendLine("#Select all Header parameters for injection");
                        Py.AppendLine("f.InjectHeaders()");

                        Rb.AppendLine("#Select all Header parameters for injection");
                        Rb.AppendLine("f.inject_headers");
                    }
                    else
                    {
                        Py.AppendLine("#Select the specified Header parameters for injection");
                        Rb.AppendLine("#Select the specified Header parameters for injection");
                        foreach (string Parameter in FuzzInjectionPoints["Headers"])
                        {
                            Py.AppendLine(string.Format(@"f.InjectHeaders(""{0}"")", Parameter.Replace("\"", "\\\"")));

                            Rb.AppendLine(string.Format(@"f.inject_headers(""{0}"")", Parameter.Replace("\"", "\\\"")));
                        }
                    }
                }

                if (SessionPluginName.Length > 0)
                {
                    Py.AppendLine("#Use a Session Plugin during Fuzzing");
                    Py.AppendLine(string.Format(@"f.SessionHandler = SessionPlugin.Get(""{0}"")", SessionPluginName));

                    Rb.AppendLine("#Use a Session Plugin during Fuzzing");
                    Rb.AppendLine(string.Format(@"f.session_handler = SessionPlugin.get(""{0}"")", SessionPluginName));
                }
            }

            if (FuzzUseCustomLogSourceCB.Checked)
            {
                Py.AppendLine("#Set a custom source name for the Fuzzer logs");
                Py.AppendLine(string.Format(@"f.SetLogSource(""{0}"")", FuzzLogSourceValue));

                Rb.AppendLine("#Set a custom source name for the Fuzzer logs");
                Rb.AppendLine("#Use a Session Plugin during Fuzzing");
                Rb.AppendLine(string.Format(@"f.set_log_source(""{0}"")", FuzzLogSourceValue));
            }

            if (FuzzUsePayloadsFromListRB.Checked)
            {
                Py.AppendLine();
                Rb.AppendLine();

                Py.AppendLine("#Store the payloads in a list");
                Py.Append("payloads = [");

                Rb.AppendLine("#Store the payloads in a list");
                Rb.Append("payloads = [");

                for (int i = 0; i < this.FuzzPayloads.Length; i++)
                {
                    string Payload = this.FuzzPayloads[i];
                    
                    Py.Append("\""); Py.Append(Tools.EscapeDoubleQuotes(Payload)); Py.Append("\"");
                    Rb.Append("\""); Rb.Append(Tools.EscapeDoubleQuotes(Payload)); Rb.Append("\"");

                    if (i < (this.FuzzPayloads.Length - 1))
                    {
                        Py.Append(",");
                        Rb.Append(",");
                    }
                }
                Py.Append("]");
                Py.AppendLine();

                Rb.Append("]");
                Rb.AppendLine();
            }
            else
            {
                Py.AppendLine();
                Py.AppendLine("#Open the payloads file and load payload from it");
                Py.AppendLine(string.Format(@"p_file = open(""{0}"")", FuzzPayloadsFile.FullName.Replace("\\", "\\\\")));
                Py.AppendLine("payloads = []");
                Py.AppendLine("payloads_with_newline = p_file.readlines()");
                Py.AppendLine("p_file.close()");
                Py.AppendLine("for pwnl in payloads_with_newline:");
                Py.Append("  "); Py.AppendLine("payloads.append(pwnl.rstrip())");
                Py.AppendLine();

                Rb.AppendLine();
                Rb.AppendLine("#Open the payloads file and load payload from it");
                Rb.AppendLine(string.Format(@"p_file = File.open(""{0}"")", FuzzPayloadsFile.FullName.Replace("\\", "\\\\")));
                Rb.AppendLine("payloads = []");
                Rb.AppendLine("payloads_with_newline = p_file.readlines");
                Rb.AppendLine("p_file.close");
                Rb.AppendLine("for pwnl in payloads_with_newline");
                Rb.Append("  "); Rb.AppendLine("payloads.push(pwnl.rstrip)");
                Rb.AppendLine("end");
                Rb.AppendLine();
            }

            Py.AppendLine("#Resets the fuzzer so that it is ready to start.");
            Py.AppendLine("f.Reset()");
            Py.AppendLine();
            Py.AppendLine("#We go through a while loop till there are Fuzz or Injection points");
            Py.AppendLine("while f.HasMore():");
            Py.AppendLine("#We make the fuzzer go to the next injection point. On first run this command makes it point to the first injection point.");
            Py.Append("  "); Py.AppendLine("f.Next()");

            Rb.AppendLine("#Resets the fuzzer so that it is ready to start.");
            Rb.AppendLine("f.reset");
            Rb.AppendLine();
            Rb.AppendLine("#We go through a while loop till there are Fuzz or Injection points");
            Rb.AppendLine("while f.has_more");
            Rb.AppendLine("#We make the fuzzer go to the next injection point. On first run this command makes it point to the first injection point.");
            Rb.Append("  "); Rb.AppendLine("f.next");


            Py.Append("  "); Py.AppendLine("for payload in payloads:");
            Rb.Append("  "); Rb.AppendLine("for payload in payloads");
            if (FuzzPayloadEncodedYesRB.Checked)
            {
                Py.AppendLine();
                Py.AppendLine("#The payload is in Url encoded form so we decode it before injecting");
                Py.Append("    "); Py.AppendLine("payload = Tools.UrlDecode(payload)");

                Rb.AppendLine();
                Rb.AppendLine("#The payload is in Url encoded form so we decode it before injecting");
                Rb.Append("    "); Rb.AppendLine("payload = Tools.url_decode(payload)");
            }
            if (FuzzOriginalParameterAfterPayloadRB.Checked)
            {
                Py.AppendLine();
                Py.AppendLine("#The injected parameter's original value is added before the payload");
                Py.Append("    "); Py.AppendLine("payload = payload + f.PreInjectionParameterValue");

                Rb.AppendLine();
                Rb.AppendLine("#The injected parameter's original value is added before the payload");
                Rb.Append("    "); Rb.AppendLine("payload = payload + f.pre_injection_parameter_value");
            }
            else if (FuzzOriginalParameterBeforePayloadRB.Checked)
            {
                Py.AppendLine();
                Py.AppendLine("#The injected parameter's original value is added before the payload");
                Py.Append("    "); Py.AppendLine("payload = f.PreInjectionParameterValue + payload");

                Rb.AppendLine();
                Rb.AppendLine("#The injected parameter's original value is added before the payload");
                Rb.Append("    "); Rb.AppendLine("payload = f.pre_injection_parameter_value + payload");
            }
            Py.AppendLine();
            Py.AppendLine("#Inject the payload in the Request at the current injection point, send it to the server and get the response");
            Py.Append("    "); Py.AppendLine("res = f.Inject(payload)");
            Py.Append("    "); Py.AppendLine("if res.Code == 500:");
            Py.Append("      "); Py.AppendLine("#If the response code is 500 then inform the user");
            Py.Append("      "); Py.AppendLine(@"print ""Injecting - "" + payload + "" made the server return a 500 response""");
            Py.Append("    "); Py.AppendLine("if res.BodyString.count('error') > 0:");
            Py.Append("      "); Py.AppendLine("#If the response body contains the string 'error' then inform the user");
            Py.Append("      "); Py.AppendLine(@"print ""Injecting - "" + payload + "" made the server return an error message in the response""");

            Rb.AppendLine();
            Rb.AppendLine("#Inject the payload in the Request at the current injection point, send it to the server and get the response");
            Rb.Append("    "); Rb.AppendLine("res = f.inject(payload)");
            Rb.Append("    "); Rb.AppendLine("if res.code == 500");
            Rb.Append("      "); Rb.AppendLine("#If the response code is 500 then inform the user");
            Rb.Append("      "); Rb.AppendLine(@"puts ""Injecting - "" + payload + "" made the server return a 500 response""");
            Rb.Append("    "); Rb.AppendLine("end");
            Rb.Append("    "); Rb.AppendLine("if res.body_string.index('error')");
            Rb.Append("      "); Rb.AppendLine("#If the response body contains the string 'error' then inform the user");
            Rb.Append("      "); Rb.AppendLine(@"puts ""Injecting - "" + payload + "" made the server return an error message in the response""");
            Rb.Append("    "); Rb.AppendLine("end");
            
            Rb.Append("  "); Rb.AppendLine("end");
            Rb.AppendLine("end");

            ShowCode(Py.ToString(), Rb.ToString());
        }

        int ScanCurrentStep = 0;
        Dictionary<string, string[]> ScanInjectionPoints = new Dictionary<string, string[]>();
        string[] ScanPayloads = new string[] { };
        string ScanInjectedBodyType = "";
        string ScanInjectedBodyFormatPlugin = "";

        void ShowScanStep0Error(string Text)
        {
            this.ScanStep0StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.ScanStep0StatusTB.Visible = false;
            }
            else
            {
                this.ScanStep0StatusTB.ForeColor = Color.Red;
                this.ScanStep0StatusTB.Visible = true;
            }
        }

        void ShowScanStep1Error(string Text)
        {
            this.ScanStep1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.ScanStep1StatusTB.Visible = false;
            }
            else
            {
                this.ScanStep1StatusTB.ForeColor = Color.Red;
                this.ScanStep1StatusTB.Visible = true;
            }
        }
        
        void ShowScanStep2Error(string Text)
        {
            this.ScanStep2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.ScanStep2StatusTB.Visible = false;
            }
            else
            {
                this.ScanStep2StatusTB.ForeColor = Color.Red;
                this.ScanStep2StatusTB.Visible = true;
            }
        }

        private void ScanParameterTypeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanParameterTypeGrid.SelectedRows == null) return;
            if (ScanParameterTypeGrid.SelectedRows.Count == 0) return;

            ScanBodyTypeGB.Visible = false;
            ScanAllParametersRB.Visible = false;
            ScanAllParametersRB.Checked = true;
            ScanListedParametersRB.Visible = false;
            ScanParametersNameListLbl.Visible = false;
            ScanParametersNameListTB.Visible = false;
            ScanAddPointLL.Visible = false;

            foreach (DataGridViewRow Row in ScanParameterTypeGrid.Rows)
            {
                if (Row.Index == ScanParameterTypeGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    switch (Row.Cells[1].Value.ToString())
                    {
                        case ("UrlPathParts"):
                            ScanAllParametersRB.Visible = true;
                            ScanListedParametersRB.Visible = true;
                            ScanParametersNameListLbl.Visible = true;
                            ScanParametersNameListTB.Visible = true;
                            ScanAddPointLL.Visible = true;
                            ScanAllParametersRB.Text = "Scan all UrlPathPart positions";
                            ScanListedParametersRB.Text = "Scan only UrlPathPart positions listed below";
                            ScanParametersNameListLbl.Text = "Enter the zero-based index positions one per line:";
                            break;
                        case ("Query"):
                            ScanAllParametersRB.Visible = true;
                            ScanListedParametersRB.Visible = true;
                            ScanParametersNameListLbl.Visible = true;
                            ScanParametersNameListTB.Visible = true;
                            ScanAddPointLL.Visible = true;
                            ScanAllParametersRB.Text = "Scan all Query Parameters";
                            ScanListedParametersRB.Text = "Scan only Query Parameters listed below";
                            ScanParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                            break;
                        case ("Body"):
                            ScanBodyTypeGrid.Rows.Clear();
                            ScanBodyTypeGrid.Rows.Add(false, "Normal");
                            foreach (string Name in FormatPlugin.List())
                            {
                                ScanBodyTypeGrid.Rows.Add(false, Name);
                            }
                            ScanBodyTypeGrid.Rows.Add(false, "Other Unknown/Custom");
                            ScanBodyTypeGrid.Visible = true;

                            ScanBodyCustomMsgTB.Visible = false;
                            ScanBodyCustomStartLbl.Visible = false;
                            ScanBodyCustomStartTB.Visible = false;
                            ScanBodyCustomEndLbl.Visible = false;
                            ScanBodyCustomEndTB.Visible = false;

                            ScanBodyTypeGB.Visible = true;

                            break;
                        case ("Cookie"):
                            ScanAllParametersRB.Visible = true;
                            ScanListedParametersRB.Visible = true;
                            ScanParametersNameListLbl.Visible = true;
                            ScanParametersNameListTB.Visible = true;
                            ScanAddPointLL.Visible = true;
                            ScanAllParametersRB.Text = "Scan all Cookie Parameters";
                            ScanListedParametersRB.Text = "Scan only Cookie Parameters listed below";
                            ScanParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                            break;
                        case ("Headers"):
                            ScanAllParametersRB.Visible = true;
                            ScanListedParametersRB.Visible = true;
                            ScanParametersNameListLbl.Visible = true;
                            ScanParametersNameListTB.Visible = true;
                            ScanAddPointLL.Visible = true;
                            ScanAllParametersRB.Text = "Scan all Header Parameters";
                            ScanListedParametersRB.Text = "Scan only Header Parameters listed below";
                            ScanParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                            break;
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ScanBodyTypeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanBodyTypeGrid.SelectedRows == null) return;
            if (ScanBodyTypeGrid.SelectedRows.Count == 0) return;

            ScanAllParametersRB.Visible = false;
            ScanAllParametersRB.Checked = true;
            ScanListedParametersRB.Visible = false;
            ScanParametersNameListLbl.Visible = false;
            ScanParametersNameListTB.Visible = false;
            ScanAddPointLL.Visible = false;

            ScanBodyCustomMsgTB.Visible = false;
            ScanBodyCustomStartLbl.Visible = false;
            ScanBodyCustomStartTB.Visible = false;
            ScanBodyCustomEndLbl.Visible = false;
            ScanBodyCustomEndTB.Visible = false;

            foreach (DataGridViewRow Row in ScanBodyTypeGrid.Rows)
            {
                if (Row.Index == ScanBodyTypeGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                    if (Row.Index == 0)
                    {
                        ScanAllParametersRB.Visible = true;
                        ScanListedParametersRB.Visible = true;
                        ScanParametersNameListLbl.Visible = true;
                        ScanParametersNameListTB.Visible = true;
                        ScanAddPointLL.Visible = true;
                        ScanAllParametersRB.Text = "Scan all Body Parameters";
                        ScanListedParametersRB.Text = "Scan only Body Parameters listed below";
                        ScanParametersNameListLbl.Text = "Enter one Parameter Name per line:";
                    }
                    else if (Row.Index == ScanBodyTypeGrid.Rows.Count - 1)
                    {
                        ScanBodyCustomMsgTB.Visible = true;
                        ScanBodyCustomStartLbl.Visible = true;
                        ScanBodyCustomStartTB.Visible = true;
                        ScanBodyCustomEndLbl.Visible = true;
                        ScanBodyCustomEndTB.Visible = true;
                        ScanAddPointLL.Visible = true;
                    }
                    else
                    {
                        ScanAllParametersRB.Visible = true;
                        ScanListedParametersRB.Visible = true;
                        ScanParametersNameListLbl.Visible = true;
                        ScanParametersNameListTB.Visible = true;
                        ScanAddPointLL.Visible = true;
                        ScanAllParametersRB.Text = string.Format("Scan all {0} Values", Row.Cells[1].Value.ToString());
                        ScanListedParametersRB.Text = string.Format("Scan only {0} Values at listed indexes", Row.Cells[1].Value.ToString());
                        ScanParametersNameListLbl.Text = "Enter the zero-based index positions of values one per line:";
                    }
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ScanListedParametersRB_CheckedChanged(object sender, EventArgs e)
        {
            ScanParametersNameListTB.Enabled = ScanListedParametersRB.Checked;
        }

        private void ScanAddPointLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowScanStep0Error("");

            string ParameterType = "";
            string BodyType = "";
            string BodyFormatPlugin = "";
            string StartMarker = ScanBodyCustomStartTB.Text;
            string EndMarker = ScanBodyCustomEndTB.Text;
            string[] ParametersList = ScanParametersNameListTB.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (DataGridViewRow Row in ScanParameterTypeGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    ParameterType = Row.Cells[1].Value.ToString();
                    break;
                }
            }
            if (ParameterType.Equals("Body"))
            {
                foreach (DataGridViewRow Row in ScanBodyTypeGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        if (Row.Index == 0)
                        {
                            BodyType = "Normal";
                        }
                        else if (Row.Index == ScanBodyTypeGrid.Rows.Count - 1)
                        {
                            BodyType = "Other";
                        }
                        else
                        {
                            BodyType = "FormatPlugin";
                            BodyFormatPlugin = Row.Cells[1].Value.ToString();
                        }
                        break;
                    }
                }
            }

            switch (ParameterType)
            {
                case ("UrlPathParts"):
                    if (ScanAllParametersRB.Checked)
                    {
                        ScanInjectionPoints["UrlPathParts"] = new string[] { };
                    }
                    else
                    {
                        foreach (string P in ParametersList)
                        {
                            try
                            {
                                Int32.Parse(P);
                            }
                            catch
                            {
                                ShowScanStep0Error("UrlPathParts position must be a number");
                                return;
                            }
                        }
                        if (ParametersList.Length == 0)
                        {
                            ShowScanStep0Error("UrlPathParts positions list cannot be empty. Mention atleast one position.");
                            return;
                        }
                        ScanInjectionPoints["UrlPathParts"] = ParametersList;
                    }
                    break;
                case ("Query"):
                    if (ScanAllParametersRB.Checked)
                    {
                        ScanInjectionPoints["Query"] = new string[] { };
                    }
                    else
                    {
                        if (ParametersList.Length == 0)
                        {
                            ShowScanStep0Error("Query parameter names list cannot be empty. Mention atleast one parameter.");
                            return;
                        }
                        ScanInjectionPoints["Query"] = ParametersList;
                    }
                    break;
                case ("Body"):
                    ScanInjectedBodyType = BodyType;
                    ScanInjectedBodyFormatPlugin = "";
                    switch (BodyType)
                    {
                        case ("Normal"):
                            if (ScanAllParametersRB.Checked)
                            {
                                ScanInjectionPoints["Body"] = new string[] { };
                            }
                            else
                            {
                                if (ParametersList.Length == 0)
                                {
                                    ShowScanStep0Error("Body parameter names list cannot be empty. Mention atleast one parameter.");
                                    return;
                                }
                                ScanInjectionPoints["Body"] = ParametersList;
                            }
                            break;
                        case ("Other"):
                            if (StartMarker.Length == 0 || EndMarker.Length == 0)
                            {
                                ShowScanStep0Error("Start or End markers cannot be empty.");
                                return;
                            }
                            if (StartMarker.Equals(EndMarker))
                            {
                                ShowScanStep0Error("Start and End markers cannot be the same.");
                                return;
                            }
                            ScanInjectionPoints["Body"] = new string[] { StartMarker, EndMarker };
                            break;
                        case ("FormatPlugin"):
                            ScanInjectedBodyFormatPlugin = BodyFormatPlugin;
                            if (ScanAllParametersRB.Checked)
                            {
                                ScanInjectionPoints["Body"] = new string[] { };
                            }
                            else
                            {
                                foreach (string P in ParametersList)
                                {
                                    try
                                    {
                                        Int32.Parse(P);
                                    }
                                    catch
                                    {
                                        ShowScanStep0Error(string.Format("{0} values position must be a number", BodyFormatPlugin));
                                        return;
                                    }
                                }
                                if (ParametersList.Length == 0)
                                {
                                    ShowScanStep0Error(string.Format("{0} positions list cannot be empty. Mention atleast one position.", BodyFormatPlugin));
                                    return;
                                }
                                ScanInjectionPoints["Body"] = ParametersList;
                            }
                            break;
                    }
                    break;
                case ("Cookie"):
                    if (ScanAllParametersRB.Checked)
                    {
                        ScanInjectionPoints["Cookie"] = new string[] { };
                    }
                    else
                    {
                        if (ParametersList.Length == 0)
                        {
                            ShowScanStep0Error("Cookie parameter names list cannot be empty. Mention atleast one parameter.");
                            return;
                        }
                        ScanInjectionPoints["Cookie"] = ParametersList;
                    }
                    break;
                case ("Headers"):
                    if (ScanAllParametersRB.Checked)
                    {
                        ScanInjectionPoints["Headers"] = new string[] { };
                    }
                    else
                    {
                        if (ParametersList.Length == 0)
                        {
                            ShowScanStep0Error("Headers parameter names list cannot be empty. Mention atleast one parameter.");
                            return;
                        }
                        ScanInjectionPoints["Headers"] = ParametersList;
                    }
                    break;
            }
            ScanBodyTypeGB.Visible = false;
            ScanAllParametersRB.Visible = false;
            ScanListedParametersRB.Visible = false;
            ScanParametersNameListLbl.Visible = false;
            ScanParametersNameListTB.Visible = false;
            ScanAddPointLL.Visible = false;

            foreach (DataGridViewRow Row in ScanParameterTypeGrid.Rows)
            {
                Row.Cells[0].Value = false;
            }

            UpdateSelectedScanInjectionPointsList();
        }

        void UpdateSelectedScanInjectionPointsList()
        {
            StringBuilder SB = new StringBuilder("Selected Injection Points:\r\n");
            if (ScanInjectionPoints.Count > 0)
            {
                foreach (string Section in ScanInjectionPoints.Keys)
                {
                    SB.Append(Section); SB.Append(", ");
                }
            }
            else
            {
                SB.Append("-");
            }
            ScanSelectedParametersListTB.Text = SB.ToString().TrimEnd().TrimEnd(',');
        }

        private void ScanStepZeroNextBtn_Click(object sender, EventArgs e)
        {
            string Error = CheckScanStep0Input();
            if (Error.Length == 0)
            {
                this.ScanCurrentStep = 1;
                this.ScanBaseTabs.SelectTab(1);
            }
            else
            {
                ShowScanStep0Error(Error);
            }
        }

        string CheckScanStep0Input()
        {
            if (ScanInjectionPoints.Count == 0)
            {
                return "No injection points were selected. Select atleast one injection point before proceeding.";
            }
            return "";
        }

        private void ScanStepOnePreviousBtn_Click(object sender, EventArgs e)
        {
            this.ScanCurrentStep = 0;
            this.ScanBaseTabs.SelectTab(0);
        }

        private void ScanStepOneNextBtn_Click(object sender, EventArgs e)
        {
            string Error = CheckScanStep1Input();
            if (Error.Length == 0)
            {
                this.ScanCurrentStep = 2;
                this.ScanBaseTabs.SelectTab(2);
            }
            else
            {
                ShowScanStep1Error(Error);
            }
        }

        string CheckScanStep1Input()
        {
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value) return "";
            }
            return "No vulnerability checks were selected. Select atleast one check before proceeding.";
        }

        private void ScanStepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            this.ScanCurrentStep = 1;
            this.ScanBaseTabs.SelectTab(1);
        }

        private void ScanBaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (ScanBaseTabs.SelectedIndex != this.ScanCurrentStep)
                ScanBaseTabs.SelectTab(this.ScanCurrentStep);
        }

        private void ScanSessionPluginGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanSessionPluginGrid.SelectedRows == null) return;
            if (ScanSessionPluginGrid.SelectedRows.Count == 0) return;

            foreach (DataGridViewRow Row in ScanSessionPluginGrid.Rows)
            {
                if (Row.Index == ScanSessionPluginGrid.SelectedRows[0].Index)
                {
                    Row.Cells[0].Value = true;
                }
                else
                {
                    Row.Cells[0].Value = false;
                }
            }
        }

        private void ScanPluginsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ScanPluginsGrid.SelectedRows == null) return;
            if (ScanPluginsGrid.SelectedRows.Count == 0) return;

            if ((bool)ScanPluginsGrid.SelectedRows[0].Cells[0].Value)
            {
                ScanPluginsGrid.SelectedRows[0].Cells[0].Value = false;
                ScanAllPluginsCB.Checked = false;
            }
            else
            {
                ScanPluginsGrid.SelectedRows[0].Cells[0].Value = true;
            }
        }

        private void ScanResetBtn_Click(object sender, EventArgs e)
        {
            ScanInjectionPoints.Clear();
            UpdateSelectedScanInjectionPointsList();
        }

        private void ScanAllPluginsCB_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
            {
                Row.Cells[0].Value = ScanAllPluginsCB.Checked;
            }
        }

        private void ScanCreateCodeBtn_Click(object sender, EventArgs e)
        {
            ShowScanStep2Error("");
            string SessionPluginName = "";

            foreach (DataGridViewRow Row in ScanSessionPluginGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    if (Row.Index == 0)
                    {
                        SessionPluginName = "";
                    }
                    else
                    {
                        SessionPluginName = Row.Cells[1].Value.ToString();
                    }
                    break;
                }
            }

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Py.AppendLine();
            Rb.AppendLine();
            Py.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Rb.AppendLine("#'req' is a variable that is assumed to contain a Request object");
            Py.AppendLine();
            Rb.AppendLine();

            
            Py.AppendLine("#We create a new Scanner to Scan the request 'req'");
            Py.AppendLine("s = Scanner(req)");

            Rb.AppendLine("#We create a new Scanner to Scan the request 'req'");
            Rb.AppendLine("s = Scanner.new(req)");

            if (ScanInjectionPoints.ContainsKey("UrlPathParts"))
            {
                if (ScanInjectionPoints["UrlPathParts"].Length == 0)
                {
                    Py.AppendLine("#Select all UrlPathparts for injection");
                    Py.AppendLine("s.InjectUrl()");

                    Rb.AppendLine("#Select all UrlPathparts for injection");
                    Rb.AppendLine("s.inject_url");
                }
                else
                {
                    Py.AppendLine("#Select the UrlPathpart at specified positions for injection");
                    Rb.AppendLine("#Select the UrlPathpart at specified positions for injection");
                    foreach (string Position in ScanInjectionPoints["UrlPathParts"])
                    {
                        Py.AppendLine(string.Format("s.InjectUrl({0})", Position.Trim()));
                        
                        Rb.AppendLine(string.Format("s.inject_url({0})", Position.Trim()));
                    }
                }
            }
            if (ScanInjectionPoints.ContainsKey("Query"))
            {
                if (ScanInjectionPoints["Query"].Length == 0)
                {
                    Py.AppendLine("#Select all Query parameters for injection");
                    Py.AppendLine("s.InjectQuery()");

                    Rb.AppendLine("#Select all Query parameters for injection");
                    Rb.AppendLine("s.inject_query()");
                }
                else
                {
                    Py.AppendLine("#Select the specified Query parameters for injection");
                    Rb.AppendLine("#Select the specified Query parameters for injection");
                    foreach (string Parameter in ScanInjectionPoints["Query"])
                    {   
                        Py.AppendLine(string.Format(@"s.InjectQuery(""{0}"")", Parameter.Replace("\"", "\\\"")));
  
                        Rb.AppendLine(string.Format(@"s.inject_query(""{0}"")", Parameter.Replace("\"", "\\\"")));
                    }
                }
            }
            if (ScanInjectionPoints.ContainsKey("Body"))
            {
                switch (ScanInjectedBodyType)
                {
                    case ("Normal"):
                        if (ScanInjectionPoints["Body"].Length == 0)
                        {
                            Py.AppendLine("#Select all Body parameters for injection");
                            Py.AppendLine("s.InjectBody()");

                            Rb.AppendLine("#Select all Body parameters for injection");
                            Rb.AppendLine("s.inject_body");
                        }
                        else
                        {
                            Py.AppendLine("#Select the specified Body parameters for injection");
                            Rb.AppendLine("#Select the specified Body parameters for injection");

                            foreach (string Parameter in ScanInjectionPoints["Body"])
                            {
                                Py.AppendLine(string.Format(@"s.InjectBody(""{0}"")", Parameter.Replace("\"", "\\\"")));
                                
                                Rb.AppendLine(string.Format(@"s.inject_body(""{0}"")", Parameter.Replace("\"", "\\\"")));
                            }
                        }
                        break;
                    case ("Other"):
                        Py.AppendLine("#Inject values between the specified start and end marker");
                        Py.AppendLine(string.Format(@"s.InjectBody(""{0}"", ""{1}"")", ScanInjectionPoints["Body"][0].Replace("\"", "\\\""), ScanInjectionPoints["Body"][1].Replace("\"", "\\\"")));

                        Rb.AppendLine("#Inject values between the specified start and end marker");
                        Rb.AppendLine(string.Format(@"s.inject_body(""{0}"", ""{1}"")", ScanInjectionPoints["Body"][0].Replace("\"", "\\\""), ScanInjectionPoints["Body"][1].Replace("\"", "\\\"")));
                        break;
                    case ("FormatPlugin"):
                        Py.AppendLine("#Specify the body format of the Request");
                        Py.AppendLine(string.Format(@"s.BodyFormat = FormatPlugin.Get(""{0}"")", ScanInjectedBodyFormatPlugin));

                        Rb.AppendLine("#Specify the body format of the Request");
                        Rb.AppendLine(string.Format(@"s.body_format = FormatPlugin.get(""{0}"")", ScanInjectedBodyFormatPlugin));

                        if (ScanInjectionPoints["Body"].Length == 0)
                        {
                            Py.AppendLine("#Select all values for injection");
                            Py.AppendLine("s.InjectBody()");

                            Rb.AppendLine("#Select all values for injection");
                            Rb.AppendLine("s.inject_body");
                        }
                        else
                        {
                            Py.AppendLine("#Select value at the specified positions for injection");
                            Rb.AppendLine("#Select value at the specified positions for injection");
                            foreach (string Parameter in ScanInjectionPoints["Body"])
                            {
                                Py.AppendLine(string.Format("s.InjectBody({0})", Parameter.Trim()));
                                
                                Rb.AppendLine(string.Format("s.inject_body({0})", Parameter.Trim()));
                            }
                        }
                        break;
                }

            }
            if (ScanInjectionPoints.ContainsKey("Cookie"))
            {
                if (ScanInjectionPoints["Cookie"].Length == 0)
                {
                    Py.AppendLine("#Select all Cookie parameters for injection");
                    Py.AppendLine("s.InjectCookie()");

                    Rb.AppendLine("#Select all Cookie parameters for injection");
                    Rb.AppendLine("s.inject_cookie)");
                }
                else
                {
                    Py.AppendLine("#Select the specified Cookie parameters for injection");
                    Rb.AppendLine("#Select the specified Cookie parameters for injection");
                    foreach (string Parameter in ScanInjectionPoints["Cookie"])
                    {
                        Py.AppendLine(string.Format(@"s.InjectCookie(""{0}"")", Parameter.Replace("\"", "\\\"")));

                        Rb.AppendLine(string.Format(@"s.inject_cookie(""{0}"")", Parameter.Replace("\"", "\\\"")));
                    }
                }
            }
            if (ScanInjectionPoints.ContainsKey("Headers"))
            {
                if (ScanInjectionPoints["Query"].Length == 0)
                {
                    Py.AppendLine("#Select all Header parameters for injection");
                    Py.AppendLine("s.InjectHeaders()");

                    Rb.AppendLine("#Select all Header parameters for injection");
                    Rb.AppendLine("s.inject_headers");
                }
                else
                {
                    Py.AppendLine("#Select the specified Header parameters for injection");
                    Rb.AppendLine("#Select the specified Header parameters for injection");

                    foreach (string Parameter in ScanInjectionPoints["Headers"])
                    {                        
                        Py.AppendLine(string.Format(@"s.InjectHeaders(""{0}"")", Parameter.Replace("\"", "\\\"")));
                        
                        Rb.AppendLine(string.Format(@"s.inject_headers(""{0}"")", Parameter.Replace("\"", "\\\"")));
                    }
                }
            }

            if (SessionPluginName.Length > 0)
            {
                Py.AppendLine("#Use a Session Plugin during Scaning");
                Py.AppendLine(string.Format(@"s.SessionHandler = SessionPlugin.Get(""{0}"")", SessionPluginName));

                Rb.AppendLine("#Use a Session Plugin during Scaning");
                Rb.AppendLine(string.Format(@"s.session_handler = SessionPlugin.get(""{0}"")", SessionPluginName));
            }

            if (ScanAllPluginsCB.Checked)
            {
                Py.AppendLine("#Enable all vulerability checks");
                Py.AppendLine("s.CheckAll()");

                Rb.AppendLine("#Enable all vulerability checks");
                Rb.AppendLine("s.check_all");
            }
            else
            {
                Py.AppendLine("#Add vulnerability checks by name. ActivePlugin.List() will give a list of all names");
                Rb.AppendLine("#Add vulnerability checks by name. ActivePlugin.list will give a list of all names");
                
                foreach (DataGridViewRow Row in ScanPluginsGrid.Rows)
                {
                    if ((bool)Row.Cells[0].Value)
                    {
                        Py.AppendLine(string.Format(@"s.AddCheck(""{0}"")", Row.Cells[1].Value.ToString()));

                        Rb.AppendLine(string.Format(@"s.add_check(""{0}"")", Row.Cells[1].Value.ToString()));
                    }
                }
            }

            
            Py.AppendLine("#Start the Scan");
            Py.AppendLine("scan_id = s.LaunchScan()");
            Py.AppendLine(@"print ""Scan started, Scan ID is "" + str(scan_id)");
            Py.AppendLine();

            Rb.AppendLine("#Start the Scan");
            Rb.AppendLine("scan_id = s.launch_scan");
            Rb.AppendLine(@"puts ""Scan started, Scan ID is "" + scan_id.to_s");
            Rb.AppendLine();

            ShowCode(Py.ToString(), Rb.ToString());
        }

        private void CopyScriptLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if(CodeTabs.SelectedIndex == 0)
                {
                    if (FullPyCode.Length > 0)
                    {
                        Clipboard.SetText(FullPyCode);
                    }
                }
                else
                {
                    if (FullRbCode.Length > 0)
                    {
                        Clipboard.SetText(FullRbCode);
                    }
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to copy code from Script Creation Assistant", Exp);
            }
        }

        private void CodeTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CodeTabs.SelectedIndex == 0)
            {
                CopyScriptLL.Text = "Copy Python Script to Clipboard";
            }
            else
            {
                CopyScriptLL.Text = "Copy Ruby Script to Clipboard";
            }
        }
    }
}

