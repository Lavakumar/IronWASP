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
using System.IO;
using System.Text.RegularExpressions;

namespace IronWASP
{
    public class XpathInjectionCheck : ActivePlugin
    {
        static List<string> error_strings = new List<string>();
        FindingReason reason= null;

        static bool SetUpDone = false;

        public override ActivePlugin GetInstance()
        {
            if (!SetUpDone)
            {
                SetUp();
                SetUpDone = true;
            }
            XpathInjectionCheck p = new XpathInjectionCheck();
            p.Name = "XPATH Injection";
            p.Description = "Active plugin that checks for XPATH Injection";
            p.Version = "0.4";
            p.FileName = "Internal";
            return p;
        }
  
        //#Override the Check method of the base class with custom functionlity
        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;;
            this.reason = null;
            this.CheckForXPATHInjection();
        }
  
        void CheckForXPATHInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for XPATH Injection:<i</h>>");
            string payload = "<!--'\"a";
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
            Response res = this.Scnr.Inject(payload);
            List<string> errors_found = new List<string>();
            foreach(string error in error_strings)
            {
                if (res.BodyString.Contains(error))
                {
                    errors_found.Add(error);
                }
            }
            if (errors_found.Count > 0)
            {
                this.Scnr.ResponseTrace(string.Format("    ==> <i<cr>>XPATH Injection Found.<i<br>>Errors:<i<br>>{0}<i</cr>>", string.Join(", ", errors_found.ToArray())));
                this.reason = this.GetReason(payload, errors_found);
                this.ReportXPATHInjection(payload, string.Format("The payload in this request is meant to trigger XPATH errors. The payload is: {0}", payload), string.Join("\r\n", errors_found.ToArray()), "This response contains XPATH error messages due to the error triggered by the payload");
            }
            else
            {
                this.Scnr.ResponseTrace("    ==> No Errors Found");
            }
        }
  
        void ReportXPATHInjection(string req_trigger, string req_trigger_desc, string res_trigger, string res_trigger_desc)
        {
            this.Scnr.SetTraceTitle("XPATH Injection Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "XPATH Injection Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("XPATH Injection"), this.GetSummary());
            pr.AddReason(this.reason);
            pr.Triggers.Add(req_trigger, req_trigger_desc, this.Scnr.InjectedRequest, res_trigger, res_trigger_desc, this.Scnr.InjectionResponse);
            pr.Type = FindingType.Vulnerability;
            pr.Severity = FindingSeverity.High;
            pr.Confidence = FindingConfidence.High;
            this.Scnr.AddFinding(pr);
        }

        string GetSummary()
        {
            string Summary = "XPATH Injection is an issue where it is possible execute XPATH queries on the XML file being referenced on the server-side. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/XPATH_Injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetReason(string payload, List<string> errors)
        {
            payload = Tools.EncodeForTrace(payload);

            //#Reason = Reason + "IronWASP sent <i><!--'"a<i> as payload to the application, this payload would cause an exception to happen in insecure XPATH queries. ".format(payload)
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application, this payload would cause an exception to happen in insecure XPATH queries. ", payload);
            if (errors.Count > 1)
            {
                Reason = Reason + "The response from the application for this payload had the error messages:";
                foreach(string error in errors)
                {
                    Reason = Reason + string.Format("<i<br>><i<hlg>>{0}<i</hlg>>", error);
                }
                Reason = Reason + "<i<br>>These error messages are usually found in XPATH query related exceptions. Therefore this issue has been reported.";
            }
            else
            {
                Reason = Reason + string.Format("The response from the application for this payload had the error message: <i<hlg>>{0}<i</hlg>>. ", errors[0]);
                Reason = Reason + "This error message is usually found in XPATH query related exceptions. Therefore this issue has been reported.";
            }
    
            string ReasonType = "Error";
    
            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the response received for the payload and confirm if the error message is actually because of some exception on the server-side. Resend the same payload but without the single or double quote and check if the error message disappears.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";
    
            FindingReason FR = new FindingReason(Reason, ReasonType, 1, FalsePositiveCheck);
            return FR;
        }

        void SetUp()
        {
            List<string> error_strings_raw = new List<string>(File.ReadAllLines(Config.Path + "\\plugins\\active\\xpath_error_strings.txt"));

            //err_str_file = open(Config.Path + "\\plugins\\active\\xpath_error_strings.txt")
            //err_str_file.readline()#Ignore the first line containing comments
            //error_strings_raw = err_str_file.readlines()
            //err_str_file.close()
            error_strings_raw.RemoveAt(0);//Ignore the first line containing comments
            foreach(string raw_err_str in error_strings_raw)
            {
                string err_str = raw_err_str.Trim();
                if (err_str.Length > 0)
                {
                    error_strings.Add(err_str);
                }
            }
        }
    }
}
