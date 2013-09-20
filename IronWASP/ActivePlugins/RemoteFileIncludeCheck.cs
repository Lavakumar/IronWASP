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
    public class RemoteFileIncludeCheck : ActivePlugin
    {
        List<string> prefixes = new List<string>() { "", "http://", "https://" };
        List<string> suffixes = new List<string>() { "", "/", "/a" };
        int TriggerCount = 0;
        bool IsResponseTimeConsistent = false;

        public override ActivePlugin GetInstance()
        {
            RemoteFileIncludeCheck p = new RemoteFileIncludeCheck();
            p.Name = "Remote File Include";
            p.Description = "Active Plugin to check for Remote File Include vulnerabilities";
            p.Version = "0.5";
            p.FileName = "Internal";
            return p;
        }

        //#Override the Check method of the base class with custom functionlity
        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;
            this.ConfidenceLevel = 0;
            this.RequestTriggers = new List<string>();
            this.ResponseTriggers = new List<string>();
            this.RequestTriggerDescs = new List<string>();
            this.ResponseTriggerDescs = new List<string>();
            this.TriggerRequests = new List<Request>();
            this.TriggerResponses = new List<Response>();
            this.TriggerCount = 0;
            this.Reasons = new List<FindingReason>();
            this.CheckForRemoteFileInclude();
        }

        void CheckForRemoteFileInclude()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Remote File Include:<i</h>>");
            this.CheckForEchoBasedRemoteFileInclude();
            this.CheckForTimeBasedRemoteFileInclude();
            this.AnalyzeTestResult();
        }

        void CheckForEchoBasedRemoteFileInclude()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Remote File Include with Echo:<i</h>>");
            foreach (string p in this.prefixes)
            {
                foreach (string s in this.suffixes)
                {
                    string payload = string.Format("{0}www.iana.org{1}", p, s);
                    this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                    Response res = this.Scnr.Inject(payload);
                    if (res.BodyString.Contains("IANA is responsible for coordinating the Internet"))
                    {
                        this.AddToTriggers(payload, string.Format("The payload in this request refers to the home page of iana.org. The payload is {0}", payload), "IANA is responsible for coordinating the Internet", "This response contains contents from the home page of iana.org. This was caused by the payload.");
                        this.Scnr.ResponseTrace("    ==> <i<cr>>Response includes content from http://www.iana.org/. Indicates RFI<i</cr>>");
                        this.SetConfidence(3);
                        FindingReason reason = this.GetEchoReason(payload, "IANA is responsible for coordinating the Internet", this.TriggerCount);
                        this.Reasons.Add(reason);
                    }
                    else
                    {
                        this.Scnr.ResponseTrace("    ==> Response does not seem to contain content from http://www.iana.org/.");
                    }
                }
            }
        }

        void CheckForTimeBasedRemoteFileInclude()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Remote File Include with Time Delay:<i</h>>");
            this.IsResponseTimeConsistent = true;
            foreach (string p in this.prefixes)
            {
                foreach (string s in this.suffixes)
                {
                    string sd = this.GetUniqueSubdomain();
                    string payload = string.Format("{0}<sub_domain>.example.org{1}", p, s);
                    if (this.IsResponseTimeConsistent)
                    {
                        this.CheckForRemoteFileIncludeWithSubDomainDelay(payload);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        void CheckForRemoteFileIncludeWithSubDomainDelay(string payload_raw)
        {
            int worked = 0;
            for (int ii = 0; ii < 3; ii++)
            {
                string sub_domain = this.GetUniqueSubdomain().ToString();
                string payload = payload_raw.Replace("<sub_domain>", sub_domain);
                
                int worked_result = CheckForRemoteFileIncludeWithSubDomainDelayOnce(payload, sub_domain, worked);

                if (worked_result == 0) return;
                if (worked == worked_result) return;

                worked = worked_result;

                if (worked == 3)
                {
                    this.SetConfidence(2);
                    return;
                }
            }
        }

        int CheckForRemoteFileIncludeWithSubDomainDelayOnce(string payload, string sub_domain, int worked)
        {
            int first_time = 0;
            int last_res_time = 0;
            List<int> res_times = new List<int>();
            Request req_current = null;
            Response res_current = null;

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    this.Scnr.Trace(string.Format("<i<br>><i<b>>Sending First Request with Payload - {0}:<i</b>>", payload));
                }
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                Response res = this.Scnr.Inject(payload);
                res_times.Add(res.RoundTrip);
                if (i == 0)
                {
                    req_current = this.Scnr.InjectedRequest;
                    res_current = res;
                    first_time = res.RoundTrip;
                    this.Scnr.ResponseTrace(string.Format("    ==> Response time is {0}ms. This will be treated as the base time.", res.RoundTrip));
                }
                else
                {
                    if (i == 1)
                    {
                        last_res_time = res.RoundTrip;
                    }
                    else
                    {
                        if (res.RoundTrip > (last_res_time + 150) || res.RoundTrip < (last_res_time - 150))
                        {
                            this.IsResponseTimeConsistent = false;
                            this.Scnr.ResponseTrace("<i<br>><i<b>>Response times are inconsistent, terminating time based RFI check.<i</b>>");
                            return 0;
                        }
                    }
                    if (res.RoundTrip >= first_time - 300)
                    {
                        this.Scnr.ResponseTrace(string.Format("    ==> Response time is {0}ms which is not 300ms lower than base time. Not an indication of RFI", res.RoundTrip));
                        break;
                    }
                    else
                    {
                        this.Scnr.ResponseTrace(string.Format("    ==> Response time is {0}ms which is 300ms lower than base time. If this is repeated then it could mean RFI", res.RoundTrip));
                    }
                }
                if (i == 3)
                {
                    worked = worked + 1;
                    this.Scnr.SetTraceTitle("RFI Time Delay Observed Once", 5);
                    if (worked == 3)
                    {
                        this.RequestTriggers.Add(payload);
                        this.RequestTriggerDescs.Add(string.Format("The payload in this request refers to an non-existent domain {0}.example.org. The payload is {1}.", sub_domain, payload));
                        this.ResponseTriggers.Add("");
                        this.ResponseTriggerDescs.Add("The first time this payload was sent the response took longer to come back. In subsequent attempts to send the same payload, including this one, the response came back much faster.");
                        this.TriggerRequests.Add(req_current);
                        this.TriggerResponses.Add(res_current);
                        this.Scnr.Trace(string.Format("<i<br>><i<cr>>Got a delay in first request with payload - {0}. The three requests after that with the same payload took 300ms less. Infering that this is due to DNS caching on the server-side this is a RFI!<i</cr>>", payload));
                        FindingReason reason = this.GetDelayReason(payload, res_times, string.Format("{0}.example.org", sub_domain), this.TriggerCount + 1);
                        this.Reasons.Add(reason);
                    }
                }
            }
            return worked;
        }

        string GetUniqueSubdomain()
        {
            string sd = string.Format("r{0}{1}", this.Scnr.ID.ToString(), DateTime.Now.Ticks);
            return sd;
        }

        void SetConfidence(int conf)
        {
            if (conf > this.ConfidenceLevel)
            {
                this.ConfidenceLevel = conf;
            }
        }

        void AnalyzeTestResult()
        {
            if (this.RequestTriggers.Count > 0)
            {
                this.ReportRemoteFileInclude();
            }
        }

        void AddToTriggers(string RequestTrigger, string RequestTriggerDesc, string ResponseTrigger, string ResponseTriggerDesc)
        {
            this.RequestTriggers.Add(RequestTrigger);
            this.ResponseTriggers.Add(ResponseTrigger);
            this.RequestTriggerDescs.Add(RequestTriggerDesc);
            this.ResponseTriggerDescs.Add(ResponseTriggerDesc);
            this.TriggerRequests.Add(this.Scnr.InjectedRequest.GetClone());
            this.TriggerResponses.Add(this.Scnr.InjectionResponse.GetClone());
            this.TriggerCount = this.TriggerCount + 1;
        }

        void ReportRemoteFileInclude()
        {
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Remote File Include Found";
            //#pr.Summary = "Remote File Include been detected in the '{0}' parameter of the {1} section of the request.<i<br>>This was tested by injecting a payload with a unique domain name, then time taken to fetch the response is noted. If subsequent requests with the same payload return quicker then it is inferred that DNS cachcing of the domain name in the payload by the server has sped up the response times.<i<br>><i<br>><i<hh>>Test Trace:<i</hh>>{2}".format(this.Scnr.InjectedParameter, this.Scnr.InjectedSection, this.Scnr.GetTrace());
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Remote File Include"), this.GetSummary());
            foreach (FindingReason reason in this.Reasons)
            {
                pr.AddReason(reason);
            }

            for (int i = 0; i < this.RequestTriggers.Count; i++)
            {
                pr.Triggers.Add(this.RequestTriggers[i], this.RequestTriggerDescs[i], this.TriggerRequests[i], this.ResponseTriggers[i], this.ResponseTriggerDescs[i], this.TriggerResponses[i]);
            }
            pr.Type = FindingType.Vulnerability;
            pr.Severity = FindingSeverity.High;
            if (this.ConfidenceLevel == 3)
            {
                pr.Confidence = FindingConfidence.High;
            }
            else if (this.ConfidenceLevel == 2)
            {
                pr.Confidence = FindingConfidence.Medium;
            }
            else
            {
                pr.Confidence = FindingConfidence.Low;
            }
            this.Scnr.AddFinding(pr);
            this.Scnr.SetTraceTitle("Remote File Include", 10);
        }

        string GetSummary()
        {
            string Summary = "Remote File Include is an issue where it is possible execute or load contents from a file located on some remote web server through the target application. For more details on this issue refer <i<cb>>http://en.wikipedia.org/wiki/File_inclusion_vulnerability<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetEchoReason(string payload, string echo_content, int Trigger)
        {
            payload = Tools.EncodeForTrace(payload);
            //#Reason = "IronWASP sent <i>http://www.iana.org/a</i> as payload to the application. This payload refers to the home page of IANA. ".format(payload)
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. This payload refers to the home page of IANA. ", payload);
            //#Reason = Reason + "The response that came back for this payload had the string <i>IANA is responsible for coordinating the Internet</i>. ".format(payload);
            Reason = Reason + string.Format("The response that came back for this payload had the string <i<hlg>>{0}<i</hlg>>. ", echo_content);
            Reason = Reason + "This string is found in the home page of IANA. This indicates that the application fetched the home page of IANA and returned it in the response, which is RFI.";

            string ReasonType = "Echo";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can first manually look at the response sent for this payload and determine if it actually contains the contents of the IANA website. After that you can try loading contents of other URLs and check if they get added in the response.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, Trigger, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetDelayReason(string payload, List<int> res_times, string domain, int trigger)
        {
            payload = Tools.EncodeForTrace(payload);
            //#Reason = "IronWASP sent <i>http://abcd1234.example.org/a</i> four times to the application. The first time the payload was sent the response came back in 789ms. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> four times to the application. The first time the payload was sent the response came back in <i<hlg>>{1}ms<i</hlg>>. ", payload, res_times[0]);
            //#Reason = Reason + "The second, third and fourth time the responses came back in <i<hlg>>204ms<i</hlg>>, <i<hlg>>140ms<i</hlg>> and <i<hlg>>134ms<i</hlg>> respectively. ".format(res_times[1], res_times[2], res_times[3])
            Reason = Reason + string.Format("The second, third and fourth time the responses came back in <i<hlg>>{0}ms<i</hlg>>, <i<hlg>>{1}ms<i</hlg>> and <i<hlg>>{2}ms<i</hlg>> respectively. ", res_times[1], res_times[2], res_times[3]);
            Reason = Reason + "The second, third and fourth responses came back atleast 300ms quicker than the first one. ";
            //#Reason = Reason + "<i>abcd1234.example.org</i> is an invalid subdomain. "
            Reason = Reason + string.Format("<i<hlg>>{0}<i</hlg>> is a non-existent subdomain. If the server had RFI vulnerability then it would try to connect to this non-existent domain. ", domain);
            Reason = Reason + "The first time the DNS resolution would have taken extra time. Subsequent attempts to connect to the same domain would be quicker due to DNS caching. Since similar behaviour was observed  for the payload this indicates a RFI vulnerability.";

            string ReasonType = "TimeDelay";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can first manually send the same payload but by changing the domain name to some other non-existent domain. ";
            FalsePositiveCheck = FalsePositiveCheck + "Send this modified payload multiple times and check if the first time takes about 300ms longer than the subsequent attempts. ";
            FalsePositiveCheck = FalsePositiveCheck + "If this behaviour is observed repeatedly then this is mostly likely a genuine RFI.<i<br>>";
            FalsePositiveCheck = FalsePositiveCheck + "Ofcourse the most concrete way to check this is to refer to a page on one of your public web servers in the payload and check if the target sever fetched that page.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, trigger, FalsePositiveCheck);
            return FR;
        }
    }
}
