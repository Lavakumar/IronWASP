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
    public partial class PassivePluginCreationAssistant : Form
    {
        string PluginName = "";
        string PluginDescription = "";
        PluginCallingState State;
        PluginWorksOn WorksOn;

        int CurrentStep = 0;
        string[] IndexNames = new string[] { "NameTab", "PluginTypeTab", "LanguageTab", "FinalTab" };

        public PassivePluginCreationAssistant()
        {
            InitializeComponent();
        }

        void CreatePlugin()
        {
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
                FFN = string.Format("{0}\\plugins\\passive\\{1}", Config.Path, FN);
                Counter++;
                if (!File.Exists(FFN))
                {
                    File.WriteAllText(FFN, PluginCode);
                    PluginCreated = true;
                    PluginStore.LoadNewPassivePlugins();
                    PluginFileTB.Text = FFN;
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

            Py.AppendLine("#Extend the PassivePlugin base class");
            Py.AppendLine(string.Format("class {0}(PassivePlugin):", PluginName));
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("#Extend the PassivePlugin base class");
            Rb.AppendLine(string.Format("class {0} < PassivePlugin", PluginName));
            Rb.AppendLine();
            Rb.AppendLine();

            Py.Append("\t"); Py.AppendLine("#Implement the GetInstance method of PassivePlugin class. This method is used to create new instances of this plugin.");
            Py.Append("\t"); Py.AppendLine("def GetInstance(self):");
            Py.Append("\t\t"); Py.AppendLine(string.Format("p = {0}()", PluginName));
            Py.Append("\t\t"); Py.AppendLine(string.Format("p.Name = '{0}'", PluginName));
            Py.Append("\t\t"); Py.AppendLine(string.Format("p.Description = '{0}'", PluginDescription.Replace("'", "\\'")));
            Py.Append("\t\t"); Py.AppendLine(string.Format("p.Version = '0.1'", PluginName));
            
            Rb.Append("\t"); Rb.AppendLine("#Implement the GetInstance method of PassivePlugin class. This method is used to create new instances of this plugin.");
            Rb.Append("\t"); Rb.AppendLine("def GetInstance()");
            Rb.Append("\t\t"); Rb.AppendLine(string.Format("p = {0}.new", PluginName));
            Rb.Append("\t\t"); Rb.AppendLine(string.Format("p.name = '{0}'", PluginName));
            Rb.Append("\t\t"); Rb.AppendLine(string.Format("p.description = '{0}'", PluginDescription.Replace("'", "\\'")));
            Rb.Append("\t\t"); Rb.AppendLine(string.Format("p.version = '0.1'", PluginName));

            if (State == PluginCallingState.Inline)
            {
                switch (WorksOn)
                {
                    case (PluginWorksOn.Request):
                        Py.Append("\t\t"); Py.AppendLine("#This plugin is called before the Request is intercepted in by the Proxy.");
                        Py.Append("\t\t"); Py.AppendLine("p.CallingState = PluginCallingState.Inline");

                        Rb.Append("\t\t"); Rb.AppendLine("#This plugin is called before the Request is intercepted in by the Proxy.");
                        Rb.Append("\t\t"); Rb.AppendLine("p.calling_state = PluginCallingState.inline");

                        Py.Append("\t\t"); Py.AppendLine("#This plugin is called on the Request object");
                        Py.Append("\t\t"); Py.AppendLine("p.WorksOn = PluginWorksOn.Request");

                        Rb.Append("\t\t"); Rb.AppendLine("#This plugin is called on the Request object");
                        Rb.Append("\t\t"); Rb.AppendLine("p.works_on = PluginWorksOn.request");
                        break;
                    case (PluginWorksOn.Response):
                        Py.Append("\t\t"); Py.AppendLine("#This plugin is called after the Response is intercepted in by the Proxy.");
                        Py.Append("\t\t"); Py.AppendLine("p.CallingState = PluginCallingState.Inline");

                        Rb.Append("\t\t"); Rb.AppendLine("#This plugin is called after the Response is intercepted in by the Proxy.");
                        Rb.Append("\t\t"); Rb.AppendLine("p.calling_state = PluginCallingState.inline");

                        Py.Append("\t\t"); Py.AppendLine("#This plugin is called on the Request object");
                        Py.Append("\t\t"); Py.AppendLine("p.WorksOn = PluginWorksOn.Response");

                        Rb.Append("\t\t"); Rb.AppendLine("#This plugin is called on the Request object");
                        Rb.Append("\t\t"); Rb.AppendLine("p.works_on = PluginWorksOn.response");
                        break;
                    case (PluginWorksOn.Both):
                        Py.Append("\t\t"); Py.AppendLine("#This plugin is called before the Request is intercepted in by the Proxy and after the Response is intercepted in by the Proxy.");
                        Py.Append("\t\t"); Py.AppendLine("p.CallingState = PluginCallingState.Inline");

                        Rb.Append("\t\t"); Rb.AppendLine("#This plugin is called before the Request is intercepted in by the Proxy and after the Response is intercepted in by the Proxy.");
                        Rb.Append("\t\t"); Rb.AppendLine("p.calling_state = PluginCallingState.inline");

                        Py.Append("\t\t"); Py.AppendLine("#This plugin is called on both the Request and Response objects");
                        Py.Append("\t\t"); Py.AppendLine("p.WorksOn = PluginWorksOn.Both");

                        Rb.Append("\t\t"); Rb.AppendLine("#This plugin is called on both the Request and Response objects");
                        Rb.Append("\t\t"); Rb.AppendLine("p.works_on = PluginWorksOn.both");
                        break;
                }
            }
            else
            {
                Py.Append("\t\t"); Py.AppendLine("#This plugin is called in the background in offline mode. It cannot make changes to the Request/Response, only view them.");
                Py.Append("\t\t"); Py.AppendLine("p.CallingState = PluginCallingState.Offline");

                Rb.Append("\t\t"); Rb.AppendLine("#This plugin is called in the background in offline mode. It cannot make changes to the Request/Response, only view them.");
                Rb.Append("\t\t"); Rb.AppendLine("p.calling_state = PluginCallingState.offline");
            }

            Py.Append("\t\t"); Py.AppendLine("return p");
            Py.AppendLine();
            Py.AppendLine();

            Rb.Append("\t\t"); Rb.AppendLine("return p");
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();

            Py.Append("\t"); Py.AppendLine("#Implement the Check method of PassivePlugin class. This is the method called by IronWASP on traffic created from within or the traffic proxied by the tool. This is the entry point in to the plugin.");
            Py.Append("\t"); Py.AppendLine("def Check(self, sess, results, report_all):");
            Py.Append("\t\t"); Py.AppendLine("self.sess = sess # 'sess' is the Session object on which this plugin is run. A Session object holds a pair of matching Request and Response objects.");
            Py.Append("\t\t"); Py.AppendLine("self.results = results # 'results' is the Findings object which will hold any findings reported by this plugin");
            Py.Append("\t\t"); Py.AppendLine("self.report_all = report_all # 'report_all' is a boolean value that informs the plugin if it should report duplicate findings.");
            Py.AppendLine();
            Py.Append("\t\t"); Py.AppendLine("#We do some analysis on the Request/Response");
            Py.Append("\t\t"); Py.AppendLine("if self.sess.Response and self.sess.Response.Code == 500:");
            Py.Append("\t\t"); Py.AppendLine("#If the Session object contains a Response and of the response code is 500 then we report a vulnerability");
            Py.Append("\t\t\t"); Py.AppendLine("self.report_vuln()");
            Py.AppendLine();
            Py.AppendLine();


            Rb.Append("\t"); Rb.AppendLine("#Implement the Check method of PassivePlugin class. This is the method called by IronWASP on traffic created from within or the traffic proxied by the tool. This is the entry point in to the plugin.");
            Rb.Append("\t"); Rb.AppendLine("def Check(sess, results, report_all)");
            Rb.Append("\t\t"); Rb.AppendLine("@sess = sess # 'sess' is the Session object on which this plugin is run. A Session object holds a pair of matching Request and Response objects.");
            Rb.Append("\t\t"); Rb.AppendLine("@results = results # 'results' is the Findings object which will hold any findings reported by this plugin");
            Rb.Append("\t\t"); Rb.AppendLine("@report_all = report_all # 'report_all' is a boolean value that informs the plugin if it should report duplicate findings.");
            Rb.AppendLine();
            Rb.Append("\t\t"); Rb.AppendLine("#We do some analysis on the Request/Response");
            Rb.Append("\t\t"); Rb.AppendLine("if @sess.response and @sess.response.code == 500");
            Rb.Append("\t\t"); Rb.AppendLine("#If the response code is 500 then we report a vulnerability");
            Rb.Append("\t\t\t"); Rb.AppendLine("report_vuln");
            Rb.Append("\t\t"); Rb.AppendLine("end");
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();

            Py.Append("\t"); Py.AppendLine("#This method implements the vulnerability reporting function");
            Py.Append("\t"); Py.AppendLine("def report_vuln(self):");
            Py.Append("\t\t"); Py.AppendLine("#Create a signature for this Finding. This helps reporting the same vulnerability multiple times. There are no rules to creating the signature, it is just a string. If the same plugin reports the same type of finding for the same host with the same signature then it is ignored as a duplicate.");
            Py.Append("\t\t"); Py.AppendLine("sign = '500 from server' + self.sess.Request.Url");
            Py.Append("\t\t"); Py.AppendLine("#We have created a signature that has some information about the issue and the Request Url. So if another page generates error then the signature will be unqiue and reported. But if the same page is requested again then there is not duplicate report.");
            Py.Append("\t\t"); Py.AppendLine("#Before reporting this issue we check if the plugin was called with report_all value set to true or if the signature is unique.");
            Py.Append("\t\t"); Py.AppendLine("if self.report_all or self.IsSignatureUnique(self.sess.Request.BaseUrl, FindingType.Vulnerability, sign):");
            Py.Append("\t\t\t"); Py.AppendLine("#Create a new instance of the Finding class, it takes the BaseUrl property of the Request object as constructor argument.");
            Py.Append("\t\t\t"); Py.AppendLine("f = Finding(self.sess.Request.BaseUrl)");
            Py.Append("\t\t\t"); Py.AppendLine("#The type of the finding is set as vulnerability. Other possible values are FindingType.Information and FindingType.TestLead");
            Py.Append("\t\t\t"); Py.AppendLine("f.Type = FindingType.Vulnerability");
            Py.Append("\t\t\t"); Py.AppendLine("#The confidence of the finding is set as Medium. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingConfidence.High and FindingConfidence.Low");
            Py.Append("\t\t\t"); Py.AppendLine("f.Confidence = FindingConfidence.Medium");
            Py.Append("\t\t\t"); Py.AppendLine("#The severity of the finding is set as High. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingSeverity.Medium and FindingSeverity.Low");
            Py.Append("\t\t\t"); Py.AppendLine("f.Severity = FindingSeverity.High");
            Py.Append("\t\t\t"); Py.AppendLine("#This vulnerability is given a title");
            Py.Append("\t\t\t"); Py.AppendLine("f.Title = 'Server returned an error'");
            Py.Append("\t\t\t"); Py.AppendLine("#This vulnerability summary is added.");
            Py.Append("\t\t\t"); Py.AppendLine(@"f.Summary = ""The server returned a 500 response to this request. This indicates lack of proper error handling on the server-side.""");
            Py.Append("\t\t\t"); Py.AppendLine("#Triggers are a collection of Trigger objects. A Trigger is a set of Request object, corrresponding Response object and some keywords that were found in the Request and Response that triggered the detection of this vulnerability.");
            Py.Append("\t\t\t"); Py.AppendLine("#In this case we don't know what triggered the error so we set the request trigger as empty string and the response trigger is the status code 500");
            Py.Append("\t\t\t"); Py.AppendLine(@"f.Triggers.Add('', self.sess.Request, '500', self.sess.Response)");
            Py.Append("\t\t\t"); Py.AppendLine("#The signature of this vulnerability is stored");
            Py.Append("\t\t\t"); Py.AppendLine("f.Signature = sign");
            Py.Append("\t\t\t"); Py.AppendLine("#After defining the vulnerability it is added to the Findings object");
            Py.Append("\t\t\t"); Py.AppendLine("self.results.Add(f)");



            Rb.Append("\t"); Rb.AppendLine("#This method implements the vulnerability reporting function");
            Rb.Append("\t"); Rb.AppendLine("def report_vuln()");
            Rb.Append("\t\t"); Rb.AppendLine("#Create a signature for this Finding. This helps reporting the same vulnerability multiple times. There are no rules to creating the signature, it is just a string. If the same plugin reports the same type of finding for the same host with the same signature then it is ignored as a duplicate.");
            Rb.Append("\t\t"); Rb.AppendLine("sign = '500 from server' + @sess.request.url");
            Rb.Append("\t\t"); Rb.AppendLine("#We have created a signature that has some information about the issue and the Request Url. So if another page generates error then the signature will be unqiue and reported. But if the same page is requested again then there is not duplicate report.");
            Rb.Append("\t\t"); Rb.AppendLine("#Before reporting this issue we check if the plugin was called with report_all value set to true or if the signature is unique.");
            Rb.Append("\t\t"); Rb.AppendLine("if @report_all or is_signature_unique(@sess.request.base_url, FindingType.vulnerability, sign)");
            Rb.Append("\t\t\t"); Rb.AppendLine("#Create a new instance of the Finding class, it takes the BaseUrl property of the Request object as constructor argument.");
            Rb.Append("\t\t\t"); Rb.AppendLine("f = Finding.new(@sess.request.base_url)");
            Rb.Append("\t\t\t"); Rb.AppendLine("#The type of the finding is set as vulnerability. Other possible values are FindingType.information and FindingType.test_lead");
            Rb.Append("\t\t\t"); Rb.AppendLine("f.type = FindingType.vulnerability");
            Rb.Append("\t\t\t"); Rb.AppendLine("#The confidence of the finding is set as Medium. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingConfidence.high and FindingConfidence.low");
            Rb.Append("\t\t\t"); Rb.AppendLine("f.confidence = FindingConfidence.medium");
            Rb.Append("\t\t\t"); Rb.AppendLine("#The severity of the finding is set as High. This property only applies to vulnerabilities. TestLeads and Information don't need to set this. Other possible values are FindingSeverity.Medium and FindingSeverity.Low");
            Rb.Append("\t\t\t"); Rb.AppendLine("f.severity = FindingSeverity.high");
            Rb.Append("\t\t\t"); Rb.AppendLine("#This vulnerability is given a title");
            Rb.Append("\t\t\t"); Rb.AppendLine("f.title = 'Server returned an error'");
            Rb.Append("\t\t\t"); Rb.AppendLine("#This vulnerability summary is added.");
            Rb.Append("\t\t\t"); Rb.AppendLine(@"f.summary = ""The server returned a 500 response to this request. This indicates lack of proper error handling on the server-side.""");
            Rb.Append("\t\t\t"); Rb.AppendLine("#Triggers are a collection of Trigger objects. A Trigger is a set of Request object, corrresponding Response object and some keywords that were found in the Request and Response that triggered the detection of this vulnerability.");
            Rb.Append("\t\t\t"); Rb.AppendLine("#In this case we don't know what triggered the error so we set the request trigger as empty string and the response trigger is the status code 500");
            Rb.Append("\t\t\t"); Rb.AppendLine(@"f.triggers.add('', @sess.request, '500', @sess.response)");
            Rb.Append("\t\t\t"); Rb.AppendLine("#The signature of this vulnerability is stored");
            Rb.Append("\t\t\t"); Rb.AppendLine("f.signature = sign");
            Rb.Append("\t\t\t"); Rb.AppendLine("#After defining the vulnerability it is added to the scanner objects list of findings");
            Rb.Append("\t\t\t"); Rb.AppendLine("@results.add(f)");
            Rb.Append("\t\t"); Rb.AppendLine("end");
            Rb.Append("\t"); Rb.AppendLine("end");

            
            Py.AppendLine();
            Py.AppendLine();
            Py.AppendLine();
            Py.AppendLine("#This code is executed only once when this new plugin is loaded in to the memory.");
            Py.AppendLine("#Create an instance of the this plugin");
            Py.AppendLine(string.Format("p = {0}()", this.PluginName));
            Py.AppendLine("#Call the GetInstance method on this instance which will return a new instance with all the approriate values filled in. Add this new instance to the list of PassivePlugins");
            Py.AppendLine("PassivePlugin.Add(p.GetInstance())");
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.AppendLine("#This code is executed only once when this new plugin is loaded in to the memory.");
            Rb.AppendLine("#Create an instance of the this plugin");
            Rb.AppendLine(string.Format("p = {0}.new", this.PluginName));
            Rb.AppendLine("#Call the get_instance method on this instance which will return a new instance with all the approriate values filled in. Add this new instance to the list of PassivePlugins");
            Rb.AppendLine("PassivePlugin.add(p.get_instance)");
            Rb.AppendLine();
            Rb.AppendLine();

            string BottomComments = @"
#The tags shown below can be used to format your vulerability summary section.
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
            return new string[] { Py.ToString(), Rb.ToString() };
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
            if (PassivePlugin.List().Contains(Name))
            {
                PluginNameTB.BackColor = Color.Red;
                ShowStep0Error("A Passive Plugin with this name already exists. Select a different name.");
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
            this.BaseTabs.SelectTab("PluginTypeTab");
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

        private void ModeInlineRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ModeInlineRB.Checked)
            {
                PluginWorksOnGB.Visible = true;
            }
            else
            {
                PluginWorksOnGB.Visible = false;
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
            if (ModeOfflineRB.Checked)
            {
                State = PluginCallingState.Offline;
            }
            else
            {
                State = PluginCallingState.Inline;

                if (WorksOnRequestRB.Checked)
                {
                    WorksOn = PluginWorksOn.Request;
                }
                else if (WorksOnResponseRB.Checked)
                {
                    WorksOn = PluginWorksOn.Response;
                }
                else
                {
                    WorksOn = PluginWorksOn.Both;
                }
            }
            this.CurrentStep = 2;
            this.BaseTabs.SelectTab("LanguageTab");
        }

        private void StepThreePreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab("PluginTypeTab");
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

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!BaseTabs.SelectedTab.Name.Equals(IndexNames[this.CurrentStep]))
                BaseTabs.SelectTab(IndexNames[this.CurrentStep]);
        }
    }
}
