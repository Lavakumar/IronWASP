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
    public class ServerSideRequestForgeryCheck : ActivePlugin
    {
        int TriggerCount = 0;

        //#Implement the GetInstance method of ActivePlugin class. This method is used to create new instances of this plugin.
        public override ActivePlugin GetInstance()
        {
            ServerSideRequestForgeryCheck p = new ServerSideRequestForgeryCheck();
            p.Name = "Server Side Request Forgery";
            p.Description = "A plugin to discover SSRF vulnerabilities";
            p.Version = "0.1";
            p.FileName = "Internal";
            return p;
        }

        //#Override the Check method of the base class with custom functionlity
        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;
            this.BaseResponse = this.Scnr.BaseResponse;
            this.ConfidenceLevel = 0;
            this.RequestTriggers = new List<string>();
            this.ResponseTriggers = new List<string>();
            this.RequestTriggerDescs = new List<string>();
            this.ResponseTriggerDescs = new List<string>();
            this.TriggerRequests = new List<Request>();
            this.TriggerResponses = new List<Response>();
            this.TriggerCount = 0;
            this.Reasons = new List<FindingReason>();
            this.CheckForSSRF();
            this.AnalyzeTestResult();
        }

        void CheckForSSRF()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Server Side Request Forgery:<i</h>>");
            this.Scnr.Trace(string.Format("<i<br>>Normal Response Code - {0}. Length -{0}", this.BaseResponse.Code, this.BaseResponse.BodyLength));
            string p = "";
            string first_time_pattern = "";
            string second_time_pattern = "";
            bool strict_group_matched = false;
            bool relaxed_group_matched = false;

            if (this.Scnr.PreInjectionParameterValue.StartsWith("http://"))
            {
                p = "http://";
            }
            else if (this.Scnr.PreInjectionParameterValue.StartsWith("https://"))
            {
                p = "https://";
            }
            else
            {
                p = "";
            }

            for (int i = 0; i < 2; i++)
            {
                string payload_a = string.Format("{0}localhost:65555", p);
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_a));
                Response res_a = this.Scnr.Inject(payload_a);
                Request req_a = this.Scnr.InjectedRequest;
                this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_a.Code, res_a.BodyLength));

                string payload_a1 = string.Format("{0}localhost:1", p);
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_a1));
                Response res_a1 = this.Scnr.Inject(payload_a1);
                Request req_a1 = this.Scnr.InjectedRequest;
                this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_a1.Code, res_a1.BodyLength));

                string payload_b = string.Format("{0}localhost:66666", p);
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_b));
                Response res_b = this.Scnr.Inject(payload_b);
                Request req_b = this.Scnr.InjectedRequest;
                this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_b.Code, res_b.BodyLength));

                string payload_b1 = string.Format("{0}localhost:2", p);
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_b1));
                Response res_b1 = this.Scnr.Inject(payload_b1);
                Request req_b1 = this.Scnr.InjectedRequest;
                this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_b1.Code, res_b1.BodyLength));

                this.Scnr.Trace("<i<br>>Analysing the responses for patterns...");

                //#Analyzing the responses for patterns
                SimilarityChecker sc = new SimilarityChecker();
                sc.Add("a", res_a, payload_a);
                sc.Add("a1", res_a1, payload_a1);
                sc.Add("b", res_b, payload_b);
                sc.Add("b1", res_b1, payload_b1);
                sc.Check();

                List<Request> requests = new List<Request>() { req_a, req_a1, req_b, req_b1 };
                List<Response> responses = new List<Response>() { res_a, res_a1, res_b, res_b1 };
                List<string> request_trigger_descs = new List<string>();
                request_trigger_descs.Add(string.Format("This payload points to the invalid port 65555 on localhost. The payload is {0}", payload_a));
                request_trigger_descs.Add(string.Format("This payload points to the valid port 1 on localhost. The payload is {0}", payload_a1));
                request_trigger_descs.Add(string.Format("This payload points to the invalid port 66666 on localhost. The payload is {0}", payload_b));
                request_trigger_descs.Add(string.Format("This payload points to the valid port 2 on localhost. The payload is {0}", payload_b1));
                List<string> response_trigger_descs = new List<string>();
                response_trigger_descs.Add("The contents of this response are different from the response of the next trigger but are similar to the response of the trigger after the next.");
                response_trigger_descs.Add("The contents of this response are different from the response of the previous trigger but are similar to the response of the trigger after the next.");
                response_trigger_descs.Add("The contents of this response are different from the response of the next trigger but are similar to the response of the trigger before the previous.");
                response_trigger_descs.Add("The contents of this response are different from the response of the previous trigger but are similar to the response of the trigger before the previous.");
                List<string> request_triggers = new List<string>() { payload_a, payload_a1, payload_b, payload_b1 };
                List<string> response_triggers = new List<string>() { "", "", "", "" };

                if (i == 0)
                {
                    foreach (SimilarityCheckerGroup group in sc.StrictGroups)
                    {
                        if (group.Count == 2)
                            if (group.HasKey("a") && group.HasKey("b"))
                            {
                                this.Scnr.Trace("<i<br>><i<cr>>Responses for invalid port based payloads are similar to each other and are different from responses for valid port based payloads. Indicates presence of SSRF.<i</cr>>");

                                FindingReason reason = this.GetDiffReason(new List<string>() { payload_a, payload_a1, payload_b, payload_b1 }, false, new List<int>(), this.TriggerCount, request_triggers.Count);
                                this.Reasons.Add(reason);

                                this.RequestTriggers.AddRange(request_triggers);
                                this.ResponseTriggers.AddRange(response_triggers);
                                this.RequestTriggerDescs.AddRange(request_trigger_descs);
                                this.ResponseTriggerDescs.AddRange(response_trigger_descs);
                                this.TriggerRequests.AddRange(requests);
                                this.TriggerResponses.AddRange(responses);
                                this.TriggerCount = this.TriggerCount + request_triggers.Count;
                                this.SetConfidence(2);
                                strict_group_matched = true;
                            }
                    }
                    if (!strict_group_matched)
                    {
                        foreach (SimilarityCheckerGroup group in sc.RelaxedGroups)
                        {
                            if (group.Count == 2)
                            {
                                if (group.HasKey("a") && group.HasKey("b"))
                                {
                                    this.Scnr.Trace("<i<br>><i<cr>>Responses for invalid port based payloads are similar to each other and are different responses for valid port based payload. Indicates presence of SSRF.<i</cr>>");

                                    FindingReason reason = this.GetDiffReason(new List<string>() { payload_a, payload_a1, payload_b, payload_b1 }, false, new List<int>(), this.TriggerCount, request_triggers.Count);
                                    this.Reasons.Add(reason);

                                    this.RequestTriggers.AddRange(request_triggers);
                                    this.ResponseTriggers.AddRange(response_triggers);
                                    this.RequestTriggerDescs.AddRange(request_trigger_descs);
                                    this.ResponseTriggerDescs.AddRange(response_trigger_descs);
                                    this.TriggerRequests.AddRange(requests);
                                    this.TriggerResponses.AddRange(responses);
                                    this.TriggerCount = this.TriggerCount + request_triggers.Count;
                                    this.SetConfidence(2);
                                    relaxed_group_matched = true;
                                }
                            }
                        }
                    }
                }
                List<int> res_times = new List<int>() { res_a.RoundTrip, res_a1.RoundTrip, res_b.RoundTrip, res_b1.RoundTrip };
                res_times.Sort();
                if ((res_times[2] - res_times[0] > 200) && (res_times[3] - res_times[0] > 200) && (res_times[2] - res_times[1] > 200) && (res_times[3] - res_times[1] > 200) && ((res_times[1] - res_times[0]) < 200) && ((res_times[3] - res_times[2]) < 200))
                {
                    if ((res_a.RoundTrip == res_times[0] && res_b.RoundTrip == res_times[1]) || (res_a.RoundTrip == res_times[1] && res_b.RoundTrip == res_times[0]))
                    {
                        if (i == 0)
                        {
                            first_time_pattern = "Valid>Invalid";
                        }
                        else
                        {
                            second_time_pattern = "Valid>Invalid";
                        }
                    }
                    else if ((res_a1.RoundTrip == res_times[0] && res_b1.RoundTrip == res_times[1]) || (res_a1.RoundTrip == res_times[1] && res_b1.RoundTrip == res_times[0]))
                    {
                        if (i == 0)
                        {
                            first_time_pattern = "Invalid>Valid";
                        }
                        else
                        {
                            second_time_pattern = "Invalid>Valid";
                        }
                    }
                }
                if (first_time_pattern.Length > 0)
                {
                    if (i == 0)
                    {
                        this.Scnr.Trace("<i<br>>There is a pattern in the roundtrip time of the four responses. Rechecking to confirm.<i<br>>");
                        continue;
                    }
                    else if (i == 1)
                    {
                        if (first_time_pattern == second_time_pattern)
                        {
                            this.Scnr.Trace("<i<br>><i<cr>>Response times for invalid port based payloads are similar to each other and are different from response times for valid port based payload. Indicates presence of SSRF.<i</cr>>");
                            response_trigger_descs = new List<string>();
                            response_trigger_descs.Add("This response time is different from the response time of the next trigger but is similar to the response time of the trigger after the next.");
                            response_trigger_descs.Add("This response time is different from the response time of the previous trigger but is similar to the response time of the trigger after the next.");
                            response_trigger_descs.Add("This response time is different from the response time of the next trigger but is similar to the response time of the trigger before the previous.");
                            response_trigger_descs.Add("This response time is different from the response time of the previous trigger but is similar to the response time of the trigger before the previous.");

                            FindingReason reason = this.GetDiffReason(new List<string>() { payload_a, payload_a1, payload_b, payload_b1 }, true, new List<int>() { res_a.RoundTrip, res_a1.RoundTrip, res_b.RoundTrip, res_b1.RoundTrip }, this.TriggerCount, request_triggers.Count);
                            this.Reasons.Add(reason);

                            this.RequestTriggers.AddRange(request_triggers);
                            this.ResponseTriggers.AddRange(response_triggers);
                            this.RequestTriggerDescs.AddRange(request_trigger_descs);
                            this.ResponseTriggerDescs.AddRange(response_trigger_descs);
                            this.TriggerRequests.AddRange(requests);
                            this.TriggerResponses.AddRange(responses);
                            this.TriggerCount = this.TriggerCount + request_triggers.Count;
                            this.SetConfidence(2);
                            return;
                        }
                        else
                        {
                            this.Scnr.Trace("<i<br>>The pattern in the response times is inconsistent and therefore does not indicate SSRF");
                            return;
                        }
                    }
                }
                else if (!(relaxed_group_matched || strict_group_matched))
                {
                    this.Scnr.Trace("<i<br>>The responses did not fall in any patterns that indicate SSRF");
                    break;
                }
            }
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
                this.ReportSSRF();
        }

        void ReportSSRF()
        {
            this.Scnr.SetTraceTitle("Server Side Request Forgery Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Server Side Request Forgery Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Server Side Request Forgery"), this.GetSummary());
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
        }

        string GetSummary()
        {
            string Summary = "Server Side Request Forgery is an issue where it is possible to forge an HTTP request on the server-side by sending the url in a request. For more details on this issue refer <i<cb>>http://cwe.mitre.org/data/definitions/918.html<i</cb>><i<br>><i<br>>";
            return Summary;
        }


        FindingReason GetDiffReason(List<string> payloads, bool time, List<int> time_delays, int trigger_start, int trigger_count)
        {
            string Reason = "IronWASP sent four payloads to the application.<i<br>>";
            List<string> ids = new List<string>() { "A", "B", "C", "D" };

            for (int i = 0; i < ids.Count; i++)
            {
                payloads[i] = Tools.EncodeForTrace(payloads[i]);
                Reason = Reason + string.Format("Payload {0} - <i<hlg>>{1}<i</hlg>><i<br>>", ids[i], payloads[i]);
            }

            Reason = Reason + "<i<br>>Payloads A and C are similar in nature. They both refer to ports 65555 and 66666 on the server which are invalid ports.";
            Reason = Reason + "<i<br>>Payloads B and D are similar to each other but different from A & C. They both refer to ports 1 and 2 on the server which are valid ports.";
            Reason = Reason + "<i<br>>If the application is vulnerable to SSRF then it will try to connect to these ports and connections to invalid potrs with throw an exception of different type than the exception or error caused by connecting to the valid ports 1 and 2 which are most likely to be closed.";

            Reason = Reason + "<i<br>>This would mean that the response for Payloads A & C must be similar to each other and different from responses for Payloads B&D. ";
            if (time)
            {
                Reason = Reason + "<i<br>><i<br>>The responses for the injected payloads were analyzed and it was found that the response times for Payloads A & C were similar to each other and were also different from response times for Payloads B & D, thereby indicating the presence of this vulnerability.";
                Reason = Reason + "<i<br>>The responses times for the four payloads were:";
                Reason = Reason + string.Format("<i<br>>Payload A - {0}ms", time_delays[0]);
                Reason = Reason + string.Format("<i<br>>Payload B - {0}ms", time_delays[1]);
                Reason = Reason + string.Format("<i<br>>Payload C - {0}ms", time_delays[2]);
                Reason = Reason + string.Format("<i<br>>Payload D - {0}ms", time_delays[3]);
            }
            else
            {
                Reason = Reason + "<i<br>><i<br>>The responses for the injected payloads were analyzed and it was found that Payloads A & C got a similar looking response and were also different from responses got from Payloads B & D, thereby indicating the presence of this vulnerability.";
            }
            //#Trigger
            List<int> trigger_ids = new List<int>();
            for (int i = trigger_start + 1; i < trigger_start + trigger_count + 1; i++)
            {
                trigger_ids.Add(i);
            }

            string ReasonType = "";
            if (time)
            {
                ReasonType = "Delay";
            }
            else
            {
                ReasonType = "Diff";
            }

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can first manually look at the responses received for Payloads A, B, C and D. Analyze these payloads and verify if indeed A & C got similar responses and were different from B & D. ";
            FalsePositiveCheck = FalsePositiveCheck + "You can also change the payloads for A & C by chaning the port number to some other invalid port and change payloads B & D to some other valid port numbers and check of the four response show the same pattern as before.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, trigger_ids, FalsePositiveCheck);
            return FR;
        }
    }
}
