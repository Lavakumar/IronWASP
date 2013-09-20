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
using System.Text.RegularExpressions;
using System.IO;

namespace IronWASP
{
    public class LdapInjectionCheck : ActivePlugin
    {
        static List<string> error_strings = new List<string>();
        FindingReason reason = null;

        static bool SetUpDone = false;

        public override ActivePlugin GetInstance()
        {
            if (!SetUpDone)
            {
                SetUp();
                SetUpDone = true;
            }
            LdapInjectionCheck p = new LdapInjectionCheck();
            p.Name = "LDAP Injection";
            p.Description = "Active plugin that checks for LDAP Injection";
            p.Version = "0.4";
            p.FileName = "Internal";
            return p;
        }
  
        //#Override the Check method of the base class with custom functionlity
        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;
            this.reason = null;
            this.CheckForLDAPInjection();
        }
  
        void CheckForLDAPInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for LDAP Injection:<i</h>>");
            string payload = "#^($!@$)(()))******";
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
                this.Scnr.ResponseTrace(string.Format("  ==> <i<cr>>LDAP Injection Found.<i<br>>Errors:<i<br>>{0}<i</cr>>", string.Join(", ", errors_found.ToArray())));
                this.reason = this.GetReason(payload, errors_found);
                this.ReportLDAPInjection(payload, string.Format("The payload in this request is meant to trigger LDAP errors. The payload is: {0}", payload), string.Join("\r\n", errors_found.ToArray()), "This response contains LDAP error messages due to the error triggered by the payload");
            }
            else
            {
                this.Scnr.ResponseTrace("  ==> No Errors Found");
            }
        }
  
        void ReportLDAPInjection(string req_trigger, string req_trigger_desc, string res_trigger, string res_trigger_desc)
        {
            this.Scnr.SetTraceTitle("LDAP Injection Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "LDAP Injection Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("LDAP Injection"), this.GetSummary());
            pr.AddReason(this.reason);
            pr.Triggers.Add(req_trigger, req_trigger_desc, this.Scnr.InjectedRequest, res_trigger, res_trigger_desc, this.Scnr.InjectionResponse);
            pr.Type = FindingType.Vulnerability;
            pr.Severity = FindingSeverity.High;
            pr.Confidence = FindingConfidence.High;
            this.Scnr.AddFinding(pr);
        }
  
        string GetSummary()
        {
            string Summary = "LDAP Injection is an issue where it is possible execute LDAP queries on the LDAP directory being referenced on the server-side. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/LDAP_injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetReason(string payload, List<string> errors)
        {
            payload = Tools.EncodeForTrace(payload);

            //#Reason = Reason + "IronWASP sent <i>#^($!@$)(()))******<i> as payload to the application, this payload would cause an exception to happen in insecure LDAP queries. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application, this payload would cause an exception to happen in insecure LDAP queries. ", payload);
            if (errors.Count > 1)
            {
                Reason = Reason + "The response from the application for this payload had the error messages:";
                foreach (string error in errors)
                {
                    Reason = Reason + string.Format("<i<br>><i<hlg>>{0}<i</hlg>>", error);
                }
                Reason = Reason + "<i<br>>These error messages are usually found in LDAP query related exceptions. Therefore this issue has been reported.";
            }
            else
            {
                //#Reason = Reason + "The response from the application for this payload had the error message: <i>An inappropriate matching occurred</i>. ".format(error)
                Reason = Reason + string.Format("The response from the application for this payload had the error message: <i<hlg>>{0}<i</hlg>>. ", errors[0]);
                Reason = Reason + "This error message is usually found in LDAP query related exceptions. Therefore this issue has been reported.";
            }

            string ReasonType = "Error";

            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the response recived for the payload and confirm if the error message is actually because of some exception on the server-side.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, 1, FalsePositiveCheck);
            return FR;
        }
    
        static void SetUp()
        {
            List<string> error_strings_raw = new List<string>(File.ReadAllLines(Config.Path + "\\plugins\\active\\ldap_error_strings.txt"));
            //err_str_file = open(Config.Path + "\\plugins\\active\\ldap_error_strings.txt")
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
