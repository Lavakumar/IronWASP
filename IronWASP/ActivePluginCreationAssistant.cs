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
using System.Text.RegularExpressions;
using System.IO;

namespace IronWASP
{
    public partial class ActivePluginCreationAssistant : Form
    {
        string PluginName = "";
        string PluginDescription = "";
        List<string> Payloads = new List<string>();

        int CurrentStep = 0;
        string[] IndexNames = new string[] { "NameTab", "PayloadsTab", "LanguageTab", "FinalTab" };

        string PayloadsFileName = "";
        
        int MaxCountForPayloadsList = 5;

        public ActivePluginCreationAssistant()
        {
            InitializeComponent();
        }

        private void StepOneNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep0Error("");
            string Name = PluginNameTB.Text.Trim();
            if (Name.Length == 0)
            {
                PluginNameTB.BackColor = Color.Red;
                ShowStep0Error("Plugin name cannot be empty");
                return;
            }
            if (!Regex.IsMatch(Name, "^[a-zA-Z]+$"))
            {
                PluginNameTB.BackColor = Color.Red;
                ShowStep0Error("Plugin Name should only contain alphabets (a-z)");
                return;
            }
            if (!Name[0].ToString().ToUpper().Equals(Name[0].ToString()))
            {
                ShowStep0Error("Plugin Name should begin with an upper case letter");
                return;
            }
            if (ActivePlugin.List().Contains(Name))
            {
                PluginNameTB.BackColor = Color.Red;
                ShowStep0Error("An Active Plugin with this name already exists. Select a different name.");
                return;
            }
            string Desc = PluginDescTB.Text;
            if (Desc.Trim().Length == 0)
            {
                PluginDescTB.BackColor = Color.Red;
                ShowStep0Error("Plugin description cannot be empty");
                return;
            }
            this.PluginName = Name;
            this.PluginDescription = Desc;
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab("PayloadsTab");
        }

        void ShowStep0Status(string Text)
        {
            this.Step0StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step0StatusTB.Visible = false;
            }
            else
            {
                this.Step0StatusTB.ForeColor = Color.Black;
                this.Step0StatusTB.Visible = true;
            }
        }
        void ShowStep0Error(string Text)
        {
            this.Step0StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step0StatusTB.Visible = false;
            }
            else
            {
                this.Step0StatusTB.ForeColor = Color.Red;
                this.Step0StatusTB.Visible = true;
            }
        }
        void ShowStep1Status(string Text)
        {
            this.Step1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step1StatusTB.Visible = false;
            }
            else
            {
                this.Step1StatusTB.ForeColor = Color.Black;
                this.Step1StatusTB.Visible = true;
            }
        }
        void ShowStep1Error(string Text)
        {
            this.Step1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step1StatusTB.Visible = false;
            }
            else
            {
                this.Step1StatusTB.ForeColor = Color.Red;
                this.Step1StatusTB.Visible = true;
            }
        }
        void ShowStep2Status(string Text)
        {
            this.Step2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step2StatusTB.Visible = false;
            }
            else
            {
                this.Step2StatusTB.ForeColor = Color.Black;
                this.Step2StatusTB.Visible = true;
            }
        }
        void ShowStep2Error(string Text)
        {
            this.Step2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step2StatusTB.Visible = false;
            }
            else
            {
                this.Step2StatusTB.ForeColor = Color.Red;
                this.Step2StatusTB.Visible = true;
            }
        }

        private void PluginNameTB_TextChanged(object sender, EventArgs e)
        {
            if (PluginNameTB.BackColor == Color.Red) PluginNameTB.BackColor = Color.White;
        }

        private void PluginDescTB_TextChanged(object sender, EventArgs e)
        {
            if (PluginDescTB.BackColor == Color.Red) PluginDescTB.BackColor = Color.White;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 0;
            this.BaseTabs.SelectTab("NameTab");
        }

        private void StepTwoNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep1Error("");
            List<string> PayloadsTempList = new List<string>(PayloadsListTB.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            if (PayloadsTempList.Count == 0)
            {
                ShowStep1Error("No valid payloads were entered. Enter atleast one payload before you proceed to next step.");
            }
            else
            {
                this.Payloads.Clear();
                this.Payloads = new List<string>(PayloadsTempList);
                this.CurrentStep = 2;
                this.BaseTabs.SelectTab("LanguageTab");
            }
        }

        private void StepThreePreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab("PayloadsTab");
        }

        private void StepThreeNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep2Error("");
            try
            {
                CreatePlugin();
            }
            catch (Exception Exp)
            {
                ShowStep2Error(string.Format("Unable to create plugin file - {0}", Exp.Message));
                return;
            }
            this.CurrentStep = 3;
            this.BaseTabs.SelectTab("FinalTab");
        }

        private void FinalBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadPayloadsFileLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            while (PayloadFileOpener.ShowDialog() == DialogResult.OK)
            {
                FileInfo Info = new FileInfo(PayloadFileOpener.FileName);
                try
                {
                    StreamReader SR = new StreamReader(Info.FullName);
                    PayloadsListTB.Text = SR.ReadToEnd();
                    SR.Close();
                    PayloadsFileLbl.Text = Info.FullName;
                }
                catch(Exception Exp)
                {
                    MessageBox.Show(string.Format("Error reading selected file: {0}", Exp));
                }
                break;
            }
        }

        void CreatePlugin()
        {
            if (this.Payloads.Count > this.MaxCountForPayloadsList) SavePayloadsToFile();
            string[] PluginCodes = CreatePluginCode();
            string PyCode = PluginCodes[0];
            string RbCode = PluginCodes[1];
            
            string PluginCode = PyCode;

            string PluginLang = "py";
            if (PluginLangRubyRB.Checked)
            {
                PluginCode = RbCode;
                PluginLang = "rb";
            }

            bool PluginCreated = false;
            int Counter = 0;
            string FFN = "";
            
            while (!PluginCreated)
            {
                string FN = "";
                if (Counter == 0)
                    FN = string.Format("{0}.{1}", PluginName, PluginLang);
                else
                    FN = string.Format("{0}_{1}.{2}", PluginName, Counter, PluginLang);
                FFN = string.Format("{0}\\plugins\\active\\{1}", Config.Path, FN);
                Counter++;
                if (!File.Exists(FFN))
                {
                    File.WriteAllText(FFN, PluginCode);
                    PluginCreated = true;
                    PluginEngine.LoadNewActivePlugins();
                    PluginFileTB.Text = FFN;
                }
            }
        }

        void SavePayloadsToFile()
        {
            bool FileCreated = false;
            int Counter = 0;
            string FFN = "";
            StringBuilder SB = new StringBuilder();
            foreach (string Payload in this.Payloads)
            {
                SB.AppendLine(Payload);
            }
            while (!FileCreated)
            {
                string FN = "";
                if (Counter == 0)
                    FN = string.Format("{0}_payloads.txt", PluginName);
                else
                    FN = string.Format("{0}_payloads_{1}.txt", PluginName, Counter);
                FFN = string.Format("{0}\\plugins\\active\\{1}", Config.Path, FN);
                Counter++;
                if (!File.Exists(FFN))
                {
                    File.WriteAllText(FFN, SB.ToString());
                    FileCreated = true;
                    this.PayloadsFileName = FN;
                }
            }
        }

        string[] CreatePluginCode()
        {
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();

            Py.AppendLine("from IronWASP import *");
            Py.AppendLine("import re");
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("include IronWASP");
            Rb.AppendLine();
            Rb.AppendLine();
            
            Py.AppendLine("#Extend the ActivePlugin base class");
            Py.AppendLine(string.Format("class {0}(ActivePlugin):", PluginName));
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("#Extend the ActivePlugin base class");
            Rb.AppendLine(string.Format("class {0} < ActivePlugin", PluginName));
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.Append("  "); Rb.AppendLine("attr_accessor :payloads");
            Rb.AppendLine();


            Py.Append("  "); Py.AppendLine("#Implement the GetInstance method of ActivePlugin class. This method is used to create new instances of this plugin.");
            Py.Append("  "); Py.AppendLine("def GetInstance(self):");
            Py.Append("    "); Py.AppendLine(string.Format("p = {0}()", PluginName));
            Py.Append("    "); Py.AppendLine(string.Format("p.Name = '{0}'", PluginName));
            Py.Append("    "); Py.AppendLine(string.Format("p.Description = '{0}'", PluginDescription.Replace("'", "\\'")));
            Py.Append("    "); Py.AppendLine(string.Format("p.Version = '0.1'", PluginName));
            Py.Append("    "); Py.AppendLine("p.payloads = []");

            Rb.Append("  "); Rb.AppendLine("#Implement the GetInstance method of ActivePlugin class. This method is used to create new instances of this plugin.");
            Rb.Append("  "); Rb.AppendLine("def GetInstance()");
            Rb.Append("    "); Rb.AppendLine(string.Format("p = {0}.new", PluginName));
            Rb.Append("    "); Rb.AppendLine(string.Format("p.name = '{0}'", PluginName));
            Rb.Append("    "); Rb.AppendLine(string.Format("p.description = '{0}'", PluginDescription.Replace("'", "\\'")));
            Rb.Append("    "); Rb.AppendLine(string.Format("p.version = '0.1'", PluginName));
            Rb.Append("    "); Rb.AppendLine("p.payloads = []");

            if(this.Payloads.Count > this.MaxCountForPayloadsList)
            {
                Py.Append("    "); Py.AppendLine("#Update this instance with the payloads read from the payloads file");
                Py.Append("    "); Py.AppendLine("p.payloads.extend(self.payloads)");

                Rb.Append("    "); Rb.AppendLine("#Update this instance with the payloads read from the payloads file");
                Rb.Append("    "); Rb.AppendLine("for payload in @payloads");
                Rb.Append("      "); Rb.AppendLine("p.payloads.push(payload)");
                Rb.Append("    "); Rb.AppendLine("end");
            }
            Py.Append("    "); Py.AppendLine("return p");
            Py.AppendLine();
            Py.AppendLine();

            Rb.Append("    "); Rb.AppendLine("return p");
            Rb.Append("  "); Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();

            Py.Append("  "); Py.AppendLine("#Implement the Check method of ActivePlugin class. This is the method called by the Scanner and the entry point in to the plugin.");
            Py.Append("  "); Py.AppendLine("def Check(self, scnr):");
            Py.Append("    "); Py.AppendLine("self.scnr = scnr # 'scnr' is the Scanner object calling this plugin");
            Py.Append("    "); Py.AppendLine("#Print out a message to the Scan trace indicating the start of the check. Check the comments at the bottom to know more about the Trace feature and the formattng inside Scan trace messages.");
            Py.Append("    "); Py.AppendLine(string.Format(@"self.scnr.Trace(""<i<br>><i<h>>Checking for {0}:<i</h>><i<br>><i<br>>"")", PluginName));

            Rb.Append("  "); Rb.AppendLine("#Implement the Check method of ActivePlugin class. This is the method called by the Scanner and the entry point in to the plugin.");
            Rb.Append("  "); Rb.AppendLine("def Check(scnr)");
            Rb.Append("    "); Rb.AppendLine("@scnr = scnr # 'scnr' is the Scanner object calling this plugin");
            Rb.Append("    "); Rb.AppendLine("#Print out a message to the Scan trace indicating the start of the check. Check the comments at the bottom to know more about the Trace feature and the formattng inside Scan trace messages.");
            Rb.Append("    "); Rb.AppendLine(string.Format(@"@scnr.trace(""<i<br>><i<h>>Checking for {0}:<i</h>><i<br>><i<br>>"")", PluginName));

            if(this.Payloads.Count <= this.MaxCountForPayloadsList)
            {
                Py.Append("    "); Py.AppendLine("#Store the payloads in a list");
                Py.Append("    ");Py.Append("self.payloads = [");

                Rb.Append("    "); Rb.AppendLine("#Store the payloads in a list");
                Rb.Append("    "); Rb.Append("@payloads = [");

                for(int i = 0; i < this.Payloads.Count; i++)
                {
                    string Payload = this.Payloads[i];

                    Py.Append("\""); Py.Append(Tools.EscapeDoubleQuotes(Payload)); Py.Append("\"");
                    Rb.Append("\""); Rb.Append(Tools.EscapeDoubleQuotes(Payload)); Rb.Append("\"");

                    if (i < (this.Payloads.Count - 1))
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
            Py.Append("    "); Py.AppendLine("#Request trace adds information about the request to the Trace. The log id of the Request is automatically added.");
            Py.Append("    "); Py.AppendLine(@"self.scnr.RequestTrace(""Sending request without payloads to get normal response - "")");
            Py.Append("    "); Py.AppendLine("#This methods sends the original request without any payloads. If any Session Plugin is selected with this scan job then that Session Plugin is used to update the original request before sending.");
            Py.Append("    "); Py.AppendLine(@"res = self.scnr.Inject()");
            Py.Append("    "); Py.AppendLine("#Response trace adds information about the response to the Trace in the same line as the previous RequestTrace message. This and RequestTrace must be called together.");
            Py.Append("    "); Py.AppendLine(@"self.scnr.ResponseTrace("" ==> Normal Code - "" + str(res.Code) + "" | Normal Length - "" + str(res.BodyLength))");
            Py.Append("    "); Py.AppendLine(@"self.scnr.Trace(""<i<br>><i<b>>Starting injection of payloads<i</b>><i<br>>"")");
            Py.Append("    "); Py.AppendLine("for payload in self.payloads:");
            Py.Append("      "); Py.AppendLine("#Since the payloads are stored in url encoded form they are decoded before being used.");
            Py.Append("      "); Py.AppendLine("payload = Tools.UrlDecode(payload)");

            Rb.Append("    "); Rb.AppendLine("#Request trace adds information about the request to the Trace. The log id of the Request is automatically added.");
            Rb.Append("    "); Rb.AppendLine(@"@scnr.request_trace(""Sending request without payloads to get normal response - "")");
            Rb.Append("    "); Rb.AppendLine("#This methods sends the original request without any payloads. If any Session Plugin is selected with this scan job then that Session Plugin is used to update the original request before sending.");
            Rb.Append("    "); Rb.AppendLine(@"res = @scnr.inject");
            Rb.Append("    "); Rb.AppendLine("#Response trace adds information about the response to the Trace in the same line as the previous RequestTrace message. This and RequestTrace must be called together.");
            Rb.Append("    "); Rb.AppendLine(@"@scnr.response_trace("" ==> Normal Code - "" + res.code.to_s + "" | Normal Length - "" + res.body_length.to_s)");
            Rb.Append("    "); Rb.AppendLine(@"@scnr.trace(""<i<br>><i<b>>Starting injection of payloads<i</b>><i<br>>"")");
            Rb.Append("    "); Rb.AppendLine("for payload in @payloads");
            Rb.Append("      "); Rb.AppendLine("#Since the payloads are stored in url encoded form they are decoded before being used.");
            Rb.Append("      "); Rb.AppendLine("payload = Tools.url_decode(payload)");

            if (OriginalParameterBeforePayloadRB.Checked)
            {
                Py.Append("      "); Py.AppendLine("#The original value of the currently tested parameter is added before the payload. self.scnr.PreInjectionParameterValue gives this value.");
                Py.Append("      "); Py.AppendLine("payload = self.scnr.PreInjectionParameterValue + payload");

                Rb.Append("      "); Rb.AppendLine("#The original value of the currently tested parameter is added before the payload. @scnr.pre_injection_parameter_value gives this value.");
                Rb.Append("      "); Rb.AppendLine("payload = @scnr.pre_injection_parameter_value + payload");
            }
            else if (OriginalParameterAfterPayloadRB.Checked)
            {
                Py.Append("      "); Py.AppendLine("#The original value of the currently tested parameter is added at the end of the payload. self.scnr.PreInjectionParameterValue gives this value.");
                Py.Append("      "); Py.AppendLine("payload = payload + self.scnr.PreInjectionParameterValue");

                Rb.Append("      "); Rb.AppendLine("#The original value of the currently tested parameter is added at the end of the payload. @scnr.pre_injection_parameter_value gives this value.");
                Rb.Append("      "); Rb.AppendLine("payload = payload + @scnr.pre_injection_parameter_value");
            }
            Py.Append("      "); Py.AppendLine("#Tools.EncodeForTrace converts the input in to a form that is friendly towards being added to the Scan Trace");
            Py.Append("      "); Py.AppendLine(@"self.scnr.RequestTrace(""Injected - "" + Tools.EncodeForTrace(payload))");
            Py.Append("      "); Py.AppendLine("#The payload is injected in the parameter currently being tested and the response is returned. If a Session Plugin was used along with this Scan Job it would have been called now internally.");
            Py.Append("      "); Py.AppendLine(@"res = self.scnr.Inject(payload)");
            Py.Append("      "); Py.AppendLine("if res.Code == 500:");
            Py.Append("        "); Py.AppendLine(@"self.scnr.ResponseTrace("" ==> <i<cr>> Got 500 response code. Indicates error on the server.<i</cr>>"")");
            Py.Append("        "); Py.AppendLine("#If the response code is 500 then we report a vulnerability");
            Py.Append("        "); Py.AppendLine("self.report_vuln(Tools.EncodeForTrace(payload))");
            Py.Append("      "); Py.AppendLine("else:");
            Py.Append("        "); Py.AppendLine(@"self.scnr.ResponseTrace("" ==> Code - "" + str(res.Code) + "" | Length - "" + str(res.BodyLength))");
            Py.AppendLine();
            Py.AppendLine();

            Rb.Append("      "); Rb.AppendLine("#Tools.encode_for_trace converts the input in to a form that is friendly towards being added to the Scan Trace");
            Rb.Append("      "); Rb.AppendLine(@"@scnr.request_trace(""Injected - "" + Tools.encode_for_trace(payload))");
            Rb.Append("      "); Rb.AppendLine("#The payload is injected in the parameter currently being tested and the response is returned. If a Session Plugin was used along with this Scan Job it would have been called now internally.");
            Rb.Append("      "); Rb.AppendLine(@"res = @scnr.inject(payload)");
            Rb.Append("      "); Rb.AppendLine("if res.code == 500");
            Rb.Append("        "); Rb.AppendLine(@"@scnr.response_trace("" ==> <i<cr>> Got 500 response code. Indicates error on the server.<i</cr>>"")");
            Rb.Append("        "); Rb.AppendLine("#If the response code is 500 then we report a vulnerability");
            Rb.Append("        "); Rb.AppendLine("report_vuln(Tools.encode_for_trace(payload))");
            Rb.Append("      "); Rb.AppendLine("else");
            Rb.Append("        "); Rb.AppendLine(@"@scnr.response_trace("" ==> Code - "" + res.code.to_s + "" | Length - "" + res.body_length.to_s)");
            Rb.Append("      "); Rb.AppendLine("end");
            Rb.Append("    "); Rb.AppendLine("end");
            Rb.Append("  "); Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();

            Py.Append("  "); Py.AppendLine("#This method implements the vulnerability reporting function");
            Py.Append("  "); Py.AppendLine("def report_vuln(self, payload):");
            Py.Append("    "); Py.AppendLine("#Create a new instance of the Finding class, it takes the BaseUrl property of the Request object as constructor argument. The self.scnr.BaseRequest property returns the original request that is being scanned.");
            Py.Append("    "); Py.AppendLine("f = Finding(self.scnr.BaseRequest.BaseUrl)");
            Py.Append("    "); Py.AppendLine("#The type of the finding is set as vulnerability. Other possible values are FindingType.Information and FindingType.TestLead");
            Py.Append("    "); Py.AppendLine("f.Type = FindingType.Vulnerability");
            Py.Append("    "); Py.AppendLine("#The confidence of the finding is set as Medium. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingConfidence.High and FindingConfidence.Low");
            Py.Append("    "); Py.AppendLine("f.Confidence = FindingConfidence.Medium");
            Py.Append("    "); Py.AppendLine("#The severity of the finding is set as High. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingSeverity.Medium and FindingSeverity.Low");
            Py.Append("    "); Py.AppendLine("f.Severity = FindingSeverity.High");
            Py.Append("    "); Py.AppendLine("#This vulnerability is given a title");
            Py.Append("    "); Py.AppendLine(string.Format("f.Title = '{0} vulnerability found'", this.PluginName));
            Py.Append("    "); Py.AppendLine("#This vulnerability summary and trace are added. self.scnr.InjectedParameter gives the name of the parameter that was tested. self.scnr.InjectedSection gives the section where the parameter is located in the request. self.scnr.GetTrace() returns the scan trace messages collected up to this ponit as a string.");
            Py.Append("    "); Py.AppendLine(string.Format(@"f.Summary = ""{0} vulnerability has been detected in the '"" + self.scnr.InjectedParameter + ""' parameter of the "" + self.scnr.InjectedSection + "" section of the request. <i<br>><i<br>><i<hh>>Test Trace:<i</hh>> "" + self.scnr.GetTrace()", this.PluginName));
            Py.Append("    "); Py.AppendLine("#Triggers are a collection of Trigger objects. A Trigger is a set of Request object, corrresponding Response object and some keywords that were found in the Request and Response that triggered the detection of this vulnerability.");
            Py.Append("    "); Py.AppendLine("#self.scnr.InjectedRequest property returns the request that was sent using the Inject method and self.scnr.InjectionResponse property gives the response to that request. In this case the request trigger is added as the injected payload and the response trigger is the status code 500");
            Py.Append("    "); Py.AppendLine(@"f.Triggers.Add(payload, self.scnr.InjectedRequest, '500', self.scnr.InjectionResponse)");
            Py.Append("    "); Py.AppendLine("#After defining the vulnerability it is added to the scanner objects list of findings");
            Py.Append("    "); Py.AppendLine("self.scnr.AddFinding(f)");
            Py.Append("    "); Py.AppendLine("#self.scnr.SetTraceTitle sets a title to this scan trace message. A title makes it easy to identify that this particular scan had some interesting finding.");
            Py.Append("    "); Py.AppendLine("#The second argument to this function is the title importantance value. The SetTraceTitle method can be called multiple times in this plugin. But only the title that was given the highest importance value will be displayed in the scan trace. If there are more than one title with the highest priority value then their all these high importance titles will be shown.");
            Py.Append("    "); Py.AppendLine(string.Format(@"self.scnr.SetTraceTitle(""{0} Found"",100)", this.PluginName));

            Rb.Append("  "); Rb.AppendLine("#This method implements the vulnerability reporting function");
            Rb.Append("  "); Rb.AppendLine("def report_vuln(payload)");
            Rb.Append("    "); Rb.AppendLine("#Create a new instance of the Finding class, it takes the BaseUrl property of the Request object as constructor argument. The @scnr.base_request.base_url property returns the original request that is being scanned.");
            Rb.Append("    "); Rb.AppendLine("f = Finding.new(@scnr.base_request.base_url)");
            Rb.Append("    "); Rb.AppendLine("#The type of the finding is set as vulnerability. Other possible values are FindingType.information and FindingType.test_lead");
            Rb.Append("    "); Rb.AppendLine("f.type = FindingType.vulnerability");
            Rb.Append("    "); Rb.AppendLine("#The confidence of the finding is set as Medium. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingConfidence.high and FindingConfidence.low");
            Rb.Append("    "); Rb.AppendLine("f.confidence = FindingConfidence.medium");
            Rb.Append("    "); Rb.AppendLine("#The severity of the finding is set as High. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingSeverity.Medium and FindingSeverity.Low");
            Rb.Append("    "); Rb.AppendLine("f.severity = FindingSeverity.high");
            Rb.Append("    "); Rb.AppendLine("#This vulnerability is given a title");
            Rb.Append("    "); Rb.AppendLine(string.Format("f.title = '{0} vulnerability found'", this.PluginName));
            Rb.Append("    "); Rb.AppendLine("#This vulnerability summary and trace are added. @scnr.injected_parameter gives the name of the parameter that was tested. @scnr.injected_section gives the section where the parameter is located in the request. @scnr.get_trace returns the scan trace messages collected up to this ponit as a string.");
            Rb.Append("    "); Rb.AppendLine(string.Format(@"f.summary = ""{0} vulnerability has been detected in the '"" + @scnr.injected_parameter + ""' parameter of the "" + @scnr.injected_section + "" section of the request. <i<br>><i<br>><i<hh>>Test Trace:<i</hh>> "" + @scnr.get_trace", this.PluginName));
            Rb.Append("    "); Rb.AppendLine("#Triggers are a collection of Trigger objects. A Trigger is a set of Request object, corrresponding Response object and some keywords that were found in the Request and Response that triggered the detection of this vulnerability.");
            Rb.Append("    "); Rb.AppendLine("#@scnr.injected_request property returns the request that was sent using the Inject method and @scnr.injection_response property gives the response to that request. In this case the request trigger is added as the injected payload and the response trigger is the status code 500");
            Rb.Append("    "); Rb.AppendLine(@"f.triggers.add(payload, @scnr.injected_request, '500', @scnr.injection_response)");
            Rb.Append("    "); Rb.AppendLine("#After defining the vulnerability it is added to the scanner objects list of findings");
            Rb.Append("    "); Rb.AppendLine("@scnr.add_finding(f)");
            Rb.Append("    "); Rb.AppendLine("#@scnr.set_trace_title sets a title to this scan trace message. A title makes it easy to identify that this particular scan had some interesting finding.");
            Rb.Append("    "); Rb.AppendLine("#The second argument to this function is the title importantance value. The set_trace_title method can be called multiple times in this plugin. But only the title that was given the highest importance value will be displayed in the scan trace. If there are more than one title with the highest priority value then their all these high importance titles will be shown.");
            Rb.Append("    "); Rb.AppendLine(string.Format(@"@scnr.set_trace_title(""{0} Found"",100)", this.PluginName));
            Rb.Append("  "); Rb.AppendLine("end");

            if (this.Payloads.Count > this.MaxCountForPayloadsList)
            {
                Py.AppendLine();
                Py.AppendLine();
                Py.Append("  "); Py.AppendLine("#This method reads the payloads from the payloads files and stores it in a variable");
                Py.Append("  "); Py.AppendLine("def load_payloads_from_file(self):");
                Py.Append("    "); Py.AppendLine("#Config.Path gives the full path directory containing IronWASP.exe");
                Py.Append("    "); Py.AppendLine(string.Format(@"p_file = open(Config.Path + ""\\plugins\\active\\{0}"")", this.PayloadsFileName));
                Py.Append("    "); Py.AppendLine("self.payloads = []");
                Py.Append("    "); Py.AppendLine("payloads_with_newline = p_file.readlines()");
                Py.Append("    "); Py.AppendLine("p_file.close()");
                Py.Append("    "); Py.AppendLine("for pwnl in payloads_with_newline:");
                Py.Append("      "); Py.AppendLine("self.payloads.append(pwnl.rstrip())");

                Rb.AppendLine();
                Rb.AppendLine();
                Rb.Append("  "); Rb.AppendLine("#This method reads the payloads from the payloads files and stores it in a variable");
                Rb.Append("  "); Rb.AppendLine("def load_payloads_from_file()");
                Rb.Append("    "); Rb.AppendLine("#Config.path gives the full path directory containing IronWASP.exe");
                Rb.Append("    "); Rb.AppendLine(string.Format(@"p_file = File.open(Config.path + ""\\plugins\\active\\{0}"")", this.PayloadsFileName));
                Rb.Append("    "); Rb.AppendLine("@payloads = []");
                Rb.Append("    "); Rb.AppendLine("payloads_with_newline = p_file.readlines");
                Rb.Append("    "); Rb.AppendLine("p_file.close");
                Rb.Append("    "); Rb.AppendLine("for pwnl in payloads_with_newline");
                Rb.Append("      "); Rb.AppendLine("@payloads.push(pwnl.rstrip)");
                Rb.Append("    "); Rb.AppendLine("end");
                Rb.Append("  "); Rb.AppendLine("end");
            }
            Py.AppendLine();
            Py.AppendLine();
            Py.AppendLine();
            Py.AppendLine("#This code is executed only once when this new plugin is loaded in to the memory.");
            Py.AppendLine("#Create an instance of the this plugin");
            Py.AppendLine(string.Format("p = {0}()", this.PluginName));
            
            Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.AppendLine("#This code is executed only once when this new plugin is loaded in to the memory.");
            Rb.AppendLine("#Create an instance of the this plugin");
            Rb.AppendLine(string.Format("p = {0}.new", this.PluginName));

            if (this.Payloads.Count > this.MaxCountForPayloadsList)
            {
                Py.AppendLine("#load payloads from the file");
                Py.AppendLine("p.load_payloads_from_file()");

                Rb.AppendLine("#load payloads from the file");
                Rb.AppendLine("p.load_payloads_from_file");
            }
            Py.AppendLine("#Call the GetInstance method on this instance which will return a new instance with all the approriate values filled in. Add this new instance to the list of ActivePlugins");
            Py.AppendLine("ActivePlugin.Add(p.GetInstance())");
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("#Call the get_instance method on this instance which will return a new instance with all the approriate values filled in. Add this new instance to the list of ActivePlugins");
            Rb.AppendLine("ActivePlugin.add(p.get_instance)");
            Rb.AppendLine();
            Rb.AppendLine();

            string BottomComments = @"
#Information about Trace and Trace message formatting
#IronWASP has a special Scan Trace feature using which the plugin can tell the user exactly what it tried to do during the scan.
#The scan messages are available in the Automated Scanning -> Scan Trace section.
#The messasge formation system used is similar to HTML. There are special tags that you can use to format the message in differnet forms.
#The same system can be used to format your Finding summary as well.
#<i<br>>introduces a line break
#<i<b>>Makes the enclosed text bold<i</b>>
#<i<h>>Makes the enclosed text prominent like a heading<i</h>>
#<i<cr>>Makes the enclosed text appear in red<i</cr>>
#<i<cg>>Makes the enclosed text appear in green<i</cg>>
#<i<cb>>Makes the enclosed text appear in blue<i</cb>>
#<i<hlr>>Highlights the enclosed text appear in red<i</hlr>>
#<i<hlg>>Highlights the enclosed text appear in green<i</hlg>>
#<i<hlb>>Highlights the enclosed text appear in blue<i</hlb>>
";
            Py.AppendLine(BottomComments);
            Rb.AppendLine(BottomComments);
            return new string[]{Py.ToString(), Rb.ToString()};
        }

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!BaseTabs.SelectedTab.Name.Equals(IndexNames[this.CurrentStep]))
                BaseTabs.SelectTab(IndexNames[this.CurrentStep]);
        }
    }
}
