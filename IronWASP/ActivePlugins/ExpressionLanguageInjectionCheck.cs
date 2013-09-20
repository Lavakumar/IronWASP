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
    public class ExpressionLanguageInjectionCheck : ActivePlugin
    {
        int TriggerCount = 0;

        public override ActivePlugin GetInstance()
        {
            ExpressionLanguageInjectionCheck p = new ExpressionLanguageInjectionCheck();
            p.Name = "Expression Language Injection";
            p.Description = "Active plugin to check for Expression Language injection";
            p.Version = "0.1";
            p.FileName = "Internal";
            return p;
        }


        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;
            this.RequestTriggers = new List<string>();
            this.ResponseTriggers = new List<string>();
            this.RequestTriggerDescs = new List<string>();
            this.ResponseTriggerDescs = new List<string>();
            this.TriggerRequests = new List<Request>();
            this.TriggerResponses = new List<Response>();
            this.TriggerCount = 0;
            this.Reasons = new List<FindingReason>();
            this.CheckForELI();
            this.AnalyzeTestResult();
        }


        void CheckForELI()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Expression Langugage Injection:<i</h>>");
            for (int i = 0; i < 2; i++)
            {
                int add_num_1 = 0;
                int add_num_2 = 0;
                Response base_res = this.Scnr.BaseResponse;
                bool found_rand_nums = false;
                while (!found_rand_nums)
                {
                    add_num_1 = Tools.GetRandomNumber(1000000, 10000000);
                    add_num_2 = Tools.GetRandomNumber(1000000, 10000000);
                    if (!base_res.BodyString.Contains((add_num_1 + add_num_2).ToString()))
                        found_rand_nums = true;
                }

                string add_str = string.Format("{0}+{1}", add_num_1, add_num_2);
                string added_str = (add_num_1 + add_num_2).ToString();

                string payload = string.Format("${{{0}}}", add_str);
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                Response res = this.Scnr.Inject(payload);
                if (res.BodyString.Contains(added_str))
                {
                    if (i == 0)
                    {
                        this.Scnr.ResponseTrace(string.Format("    ==> <i<b>>Got {0} in the response, this is the result of executing '{1}'. Rechecking to confirm.<i</b>>", added_str, add_str));
                        continue;
                    }
                    else
                    {
                        this.Scnr.ResponseTrace(string.Format("    ==> <i<cr>>Got {0} in the response, this is the result of executing '{1}'. Indicates Expression Language Injection!<i</cr>>", added_str, add_str));
                        this.Scnr.SetTraceTitle("Expression Language Injection", 5);
                        this.AddToTriggers(payload, string.Format("The payload in this request contains a Expression Language snippet which if executed will add the numbers {0} & {1} and print the result. The Expression Language snippet is: {2}", add_num_1, add_num_2, payload), added_str, string.Format("This response contains the value {0} which is the sum of the numbers {1} & {2} which were sent in the request.", add_num_1 + add_num_2, add_num_1, add_num_2));
                        FindingReason reason = this.GetEchoReason(payload, payload, add_num_1, add_num_2);
                        this.Reasons.Add(reason);
                        return;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        this.Scnr.ResponseTrace(string.Format("    ==> Did not get {0} in the response", added_str));
                        this.Scnr.Trace("<i<br>>No indication for presence of Expression Language Injection");
                        break;
                    }
                    else
                    {
                        this.Scnr.ResponseTrace(string.Format("    ==> Did not get {0} in the response. The last instance might have been a false trigger.", added_str));
                        this.Scnr.Trace("<i<br>>No indication for presence of Expression Language Injection");
                    }
                }
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

        void AnalyzeTestResult()
        {
            if (this.RequestTriggers.Count > 0)
            {
                this.ReportELInjection(FindingConfidence.Medium);
            }
        }

        void ReportELInjection(FindingConfidence confidence)
        {
            this.Scnr.SetTraceTitle("Expression Language Injection Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Expression Language Injection Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Expression Language Injection"), this.GetSummary());
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
            pr.Confidence = confidence;
            this.Scnr.AddFinding(pr);
        }

        string GetSummary()
        {
            string Summary = "Expression Language Injection is an issue where it is possible to inject and execute code on the server-side. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/Expression_Language_Injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetEchoReason(string payload, string code, int num_a, int num_b)
        {
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. This payload has a small Expression Language snippet - <i<hlg>>{1}<i</hlg>>. ", payload, code);
            Reason = Reason + string.Format("If this code is executed then <i<hlg>>{0}<i</hlg>> and <i<hlg>>{1}<i</hlg>> will be added together and the sum of the addition will be printed back in the response. ", num_a, num_b);
            Reason = Reason + string.Format("The response that came back from the application after the payload was injected had the value <i<hlg>>{0}<i</hlg>>, which is the sum of <i<hlg>>{1}<i</hlg>> & <i<hlg>>{2}<i</hlg>>. ", num_a + num_b, num_a, num_b);
            Reason = Reason + "This indicates that the injected code snippet could have been executed on the server-side.";

            string ReasonType = "Error";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can manually inject the same payload but by changing the two numbers to some other value. Then you can observe if the response contains the sum of two numbers.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, this.TriggerCount, FalsePositiveCheck);
            return FR;
        }
    }
}
