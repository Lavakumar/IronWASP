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
using System.Text;

namespace IronWASP
{
    public class ServerSideIncludesInjectionCheck : ActivePlugin
    {
        FindingReason reason = null;
        
        public override ActivePlugin GetInstance()
        {
            ServerSideIncludesInjectionCheck p = new ServerSideIncludesInjectionCheck();
            p.Name = "Server Side Includes Injection";
            p.Description = "An Active Plugin to detect Sever Side Include Injection vulnerabilities";
            p.Version = "0.1";
            p.FileName = "Internal";
            return p;
        }


        public override void Check(Scanner scnr)
        {
            //#Check logic based on https://github.com/fnordbg/SSI-Scan
            this.Scnr = scnr;
            this.Scnr.Trace("<i<br>><i<h>>Checking for Server Side Includes Injection:<i</h>><i<br>><i<br>>");
            List<string> payloads = new List<string>() {string.Format("{0}\"'><!--#printenv -->", this.Scnr.PreInjectionParameterValue), "\"'><!--#printenv -->", "<!--#printenv -->"};
            foreach(string payload in payloads)
            {
                this.Scnr.RequestTrace("Injected - " + payload);
                Response res = this.Scnr.Inject(payload);
                if (res.BodyString.Contains("REMOTE_ADDR") && res.BodyString.Contains("DATE_LOCAL") && res.BodyString.Contains("DATE_GMT") && res.BodyString.Contains("DOCUMENT_URI") && res.BodyString.Contains("LAST_MODIFIED"))
                {
                    this.Scnr.ResponseTrace(" ==> <i<cr>> Got contents of Environment variables in the response body. Indicates SSI Injection.<i</cr>>");
                    this.reason = this.GetReason(payload, new List<string>() {"REMOTE_ADDR", "DATE_LOCAL", "DATE_GMT", "DOCUMENT_URI", "LAST_MODIFIED"});
                    this.ReportSSI(payload, string.Format("The payload in this request contains a SSI snippet <!--#printenv--> which if executed will print the contents of the environment variables. The payload is: {0}", payload),  string.Join("\r\n", new string[] {"REMOTE_ADDR", "DATE_LOCAL", "DATE_GMT", "DOCUMENT_URI", "LAST_MODIFIED"}), "This response contains some keywords that are similar to some standard environment variable names.");
                    return;
                }
                else
                {
                    this.Scnr.ResponseTrace(" ==> The response does not contain any Environment variable information.");
                }
            }
            this.Scnr.Trace("<i<br>>No indication for presence of SSI Injection");
        }


        void ReportSSI(string req_trigger, string req_trigger_desc, string res_trigger, string res_trigger_desc)
        {
            this.Scnr.SetTraceTitle("Server Side Includes Injection Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Server Side Includes Injection Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Server Side Includes Injection"), this.GetSummary());
            pr.AddReason(this.reason);
            pr.Triggers.Add(req_trigger, req_trigger_desc, this.Scnr.InjectedRequest, res_trigger, res_trigger_desc, this.Scnr.InjectionResponse);
            pr.Type = FindingType.Vulnerability;
            pr.Severity = FindingSeverity.High;
            pr.Confidence = FindingConfidence.High;
            this.Scnr.AddFinding(pr);
        }

        string GetSummary()
        {
            string Summary = "Server Side Includes Injection is an issue where it is possible to code on the server-side. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/Server-Side_Includes_(SSI)_Injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetReason(string payload, List<string> keywords)
        {
            payload = Tools.EncodeForTrace(payload);
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application, this payload would display the environment variables to be printed in the response if the application is vulnerable to Server Side Includes Injection.", payload);
            Reason = Reason + "The response from the application for this payload had some keywords that are similar to the names of environment variables. These keywords were:";
            
            foreach(string keyword in keywords)
            {
                Reason = Reason + string.Format("<i<br>><i<hlg>>{0}<i</hlg>>", keyword);
            }
            Reason = Reason + "<i<br>>These words are similar to that of standard environment variable names, therefore this issue has been reported.";
    
            string ReasonType = "Echo";
    
            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the response received for the payload and confirm if it actually contains the environment variable details. Change the printenv command to some other SSI command and see if the response contains that command's output.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";
    
            FindingReason FR = new FindingReason(Reason, ReasonType, 1, FalsePositiveCheck);
            return FR;
        }

    }
}
