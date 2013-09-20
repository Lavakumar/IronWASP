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

namespace IronWASP
{
    public class HeaderInjectionCheck : ActivePlugin
    {
        List<string> crlf_inj_str = new List<string>() {"\r\nNeww: Headerr", "aa\r\nNeww: Headerr", "\r\nNeww: Headerr\r\n", "aa\r\nNeww: Headerr\r\n"};
        FindingReason reason = null;
  
        public override ActivePlugin GetInstance()
        {
            HeaderInjectionCheck p = new HeaderInjectionCheck();
            p.Name = "Header Injection";
            p.Description = "Active plugin that checks for HTTP Header Injection by inserting CR LF characters";
            p.Version = "0.4";
            p.FileName = "Internal";
            return p;
        }
  
        //#Override the Check method of the base class with custom functionlity
        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;
            this.reason = null;
            this.CheckForCRLFInjection();
        }
  
        void CheckForCRLFInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Header Injection:<i</h>>");
            this.Scnr.Trace("<i<br>><i<b>>  Trying to inject a header named 'Neww'<i</b>>");
            bool crlf_inj_found = false;
            List<string> prefix = new List<string>() {"", this.Scnr.PreInjectionParameterValue};
            foreach(string cis in this.crlf_inj_str)
            {
                if (crlf_inj_found)
                {
                    break;
                }
                foreach(string p in prefix)
                {
                    string payload = p + cis;
                    this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                    Response res = this.Scnr.Inject(payload);
                    if(res.Headers.Has("Neww"))
                    {
                        this.Scnr.ResponseTrace("  ==> <i<cr>>Header 'Neww' injected<i</cr>>");
                        this.reason = this.GetReason(payload);
                        this.ReportCRLFInjection(payload, string.Format("The payload in this request attempts to insert a header with name 'Neww' in the response. The payload is {0}", payload), "Neww: Headerr", "This response has a header named 'Neww' which was added because of the payload");
                        crlf_inj_found = true;
                        break;
                    }
                    else
                    {
                        this.Scnr.ResponseTrace("  ==> Header not injected");
                    }
                }
            }
        }
  
        void ReportCRLFInjection(string req_trigger, string req_trigger_desc, string res_trigger, string res_trigger_desc)
        {
            this.Scnr.SetTraceTitle("Header Injection Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Header Injection Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Header Injection"), this.GetSummary());
            pr.AddReason(this.reason);
            pr.Triggers.Add(req_trigger, req_trigger_desc, this.Scnr.InjectedRequest, res_trigger, res_trigger_desc, this.Scnr.InjectionResponse);
            pr.Type = FindingType.Vulnerability;
            pr.Severity = FindingSeverity.High;
            pr.Confidence = FindingConfidence.High;
            this.Scnr.AddFinding(pr);
        }

        string GetSummary()
        {
            string Summary = "Header Injection is an issue where it is possible to inject a new HTTP Header in the response from the application. For more details on this issue refer <i<cb>>http://en.wikipedia.org/wiki/HTTP_header_injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetReason(string payload)
        {
            payload = Tools.EncodeForTrace(payload);
    
            //#Reason = "IronWASP sent <i>'\r\nNeww: Headerr</i> as payload to the application. This payload has CRLF characters followed by the string <i>Neww: Headerr</i> which is in the format of a HTTP Header with name <i>Neww</i> and value <i>Headerr</i>. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. This payload has CRLF characters followed by the string <i<hlg>>Neww: Headerr<i</hlg>> which is in the format of a HTTP Header with name <i<hlg>>Neww<i</hlg>> and value <i<hlg>>Headerr<i</hlg>>. ", payload);
            Reason = Reason + "The response that came back from the application after injecting this payload has an HTTP header named <i<hlg>>Neww<i</hlg>>. ";
            Reason = Reason + "This indicates that our payload caused an HTTP header to be injected in the response.";
        
            string ReasonType = "HeaderAdded";
    
            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can send the same payload but with different values for the header name part of the payload. If the response contains any HTTP headers with the specified names then there actually is Header Injection.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";
    
            FindingReason FR = new FindingReason(Reason, ReasonType, 1, FalsePositiveCheck);
            return FR;
        }
    }
}
