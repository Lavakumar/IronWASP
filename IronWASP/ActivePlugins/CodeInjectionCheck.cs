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
    public class CodeInjectionCheck : ActivePlugin
    {
        int TriggerCount = 0;
        //int time = 0;

        public override ActivePlugin GetInstance()
        {
            CodeInjectionCheck p = new CodeInjectionCheck();
            p.Name = "Code Injection";
            p.Description = "Active Plugin to check for CodeInjection vulnerability";
            p.Version = "0.4";
            p.FileName = "Internal";
            return p;
        }

        //#Check logic based on https://github.com/Zapotek/arachni/blob/master/modules/audit/code_injection.rb of the Arachni project
        //#Override the Check method of the base class with custom functionlity
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
            this.CheckForCodeInjection();
        }

        void CheckForCodeInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Code Injection:<i</h>>");
            this.CheckForEchoBasedCodeInjection();
            this.CheckForTimeBasedCodeInjection();
            this.AnalyzeTestResult();
        }

        void CheckForEchoBasedCodeInjection()
        {
            //#lang_order [php, perl, pyton, asp, ruby]
            List<string> functions = new List<string>() { "echo <add_str>;", "print <add_str>;", "print <add_str>", "Response.Write(<add_str>)", "puts <add_str>" };
            List<string> comments = new List<string>() { "#", "#", "#", "'", "#" };
            List<string> prefixes = new List<string>() { "", ";", "';", "\";" };

            int add_num_1 = 0;
            int add_num_2 = 0;
            Response base_res = this.Scnr.BaseResponse;
            bool found_rand_nums = false;
            while (!found_rand_nums)
            {
                add_num_1 = Tools.GetRandomNumber(1000000, 10000000);
                add_num_2 = Tools.GetRandomNumber(1000000, 10000000);
                if (!base_res.BodyString.Contains((add_num_1 + add_num_2).ToString()))
                {
                    found_rand_nums = true;
                }
            }

            string add_str = string.Format("{0}+{1}", add_num_1, add_num_2);
            string added_str = (add_num_1 + add_num_2).ToString();

            this.Scnr.Trace("<i<br>><i<h>>Checking for Echo based Code Injection:<i</h>>");
            for (int i = 0; i < functions.Count; i++)
            {
                foreach (string p in prefixes)
                {
                    List<string> inj_comments = new List<string>() { "", comments[i] };
                    foreach (string c in inj_comments)
                    {
                        string func_to_execute = functions[i].Replace("<add_str>", add_str);
                        string payload = string.Format("{0}{1}{2}", p, func_to_execute, c);
                        this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                        Response res = this.Scnr.Inject(payload);
                        if (res.BodyString.Contains(added_str))
                        {
                            this.Scnr.ResponseTrace(string.Format("    ==> <i<cr>>Got {0} in the response, this is the result of executing '{1}'. Indicates Code Injection!<i</cr>>", added_str, add_str));
                            this.Scnr.SetTraceTitle("Echo based Code Injection", 5);
                            this.AddToTriggers(payload, string.Format("The payload in this request contains a code snippet which if executed will add the numbers {0} & {1} and print the result. The code snippet is: {2}", add_num_1, add_num_2, func_to_execute), added_str, string.Format("This response contains the value {0} which is the sum of the numbers {1} & {2} which were sent in the request.", add_num_1 + add_num_2, add_num_1, add_num_2));
                            FindingReason reason = this.GetErrorReason(payload, func_to_execute, add_num_1, add_num_2);
                            this.Reasons.Add(reason);
                            return;
                        }
                        else
                        {
                            this.Scnr.ResponseTrace(string.Format("    ==> Did not get {0} in the response", added_str));
                        }
                    }
                }
            }
        }

        void CheckForTimeBasedCodeInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Time based Code Injection:<i</h>>");
            //#set the time related values for time-based code injection check
            //this.time = 0;
            //int max_delay = 0;
            //int min_delay = -1;
            //this.Scnr.Trace("<i<br>>Sending three requests to get a baseline of the response time for time based check:");
            //List<string> base_line_delays = new List<string>();
            //int avg_delay = 0;
            //for (int i = 0; i < 3; i++)
            //{
            //    Response res = this.Scnr.Inject();
            //    avg_delay = avg_delay + res.RoundTrip;
            //    base_line_delays.Add(string.Format("  {0}) Response time is - {1} ms", i + 1, res.RoundTrip));
            //    if (res.RoundTrip > max_delay)
            //    {
            //        max_delay = res.RoundTrip;
            //    }
            //    if (res.RoundTrip < min_delay || min_delay == -1)
            //    {
            //        min_delay = res.RoundTrip;
            //    }
            //}
            //avg_delay = avg_delay / 3;

            //this.Scnr.Trace(string.Join("<i<br>>", base_line_delays.ToArray()));
            //if (min_delay > 5000)
            //{
            //    this.time = ((max_delay + min_delay) / 1000) + 1;
            //}
            //else
            //{
            //    this.time = ((max_delay + 5000) / 1000) + 1;
            //}
            //this.Scnr.Trace(string.Format("<i<br>>Maximum Response Time: {0}ms. Minimum Response Time: {1}ms<i<br>>Induced Time Delay will be for {2}ms<i<br>>", max_delay, min_delay, this.time * 1000));

            List<string> functions = new List<string>() { "sleep(<seconds>);", "import time;time.sleep(<seconds>);" };
            List<string> prefixes = new List<string>() { "", "';", "\";" };
            List<string> comments = new List<string>() { "", "#" };
            foreach (string f in functions)
            {
                foreach (string p in prefixes)
                {
                    foreach (string c in comments)
                    {
                        CodeInjectionPayloadParts PayloadParts = new CodeInjectionPayloadParts();
                        PayloadParts.Prefix = p;
                        PayloadParts.Function = f.Replace("<seconds>","{0}");
                        PayloadParts.Comment = c;
                        //string func_to_execute = f.Replace("<seconds>", this.time.ToString());
                        //string payload = string.Format("{0}{1}{2}", p, func_to_execute, c);
                        //this.SendAndAnalyzeTimePayload(payload, func_to_execute, avg_delay);
                        this.SendAndAnalyzeTimePayload(PayloadParts);
                    }
                }
            }
        }

        //void SendAndAnalyzeTimePayload(string payload, string func_to_execute, int avg_time)
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
        //        Response res = this.Scnr.Inject(payload);
        //        //#we reduce the delay by 3 seconds to make up for the the fact that the ping could complete earlier
        //        if (res.RoundTrip >= this.time * 1000)
        //        {
        //            if (i == 0)
        //            {
        //                this.Scnr.ResponseTrace(string.Format("    ==> <i<b>>Observed a delay of {0}ms, induced delay was for {1}ms. Rechecking the delay by sending the same payload again<i</b>>", res.RoundTrip, this.time * 1000));
        //            }
        //            else
        //            {
        //                this.Scnr.ResponseTrace(string.Format("    ==> <i<cr>>Observed a delay of {0}ms, induced delay was for {1}ms. Delay observed twice, indicates Code Injection!!<i</cr>>", res.RoundTrip, this.time * 1000));
        //                this.AddToTriggers(payload, string.Format("The payload in this request contains a code snippet which if executed will cause the response to be delayed by {0} milliseconds. The code snippet is: {1}", this.time * 1000, func_to_execute), "", string.Format("It took {0}milliseconds to recieve the response from the server. It took so long because of the {1} millisecond delay caused by the payload.", res.RoundTrip, this.time * 1000));
        //                FindingReason reason = this.GetBlindReason(payload, func_to_execute, res.RoundTrip, avg_time);
        //                this.Reasons.Add(reason);
        //            }
        //        }
        //        else
        //        {
        //            if (i == 0)
        //            {
        //                this.Scnr.ResponseTrace(string.Format("    ==> Response time was {0}ms. No delay observed.", res.RoundTrip));
        //                return;
        //            }
        //            else
        //            {
        //                this.Scnr.ResponseTrace(string.Format("    ==> Response time was {0}ms. Delay did not reoccur, initial delay could have been due to network issues.", res.RoundTrip));
        //            }
        //        }
        //    }
        //}

        void SendAndAnalyzeTimePayload(CodeInjectionPayloadParts PayloadParts)
        {
            TimeBasedCheckResults TimeCheckResults = DoTimeDelayBasedCheck(TimePayloadGenerator, PayloadParts);

            if (TimeCheckResults.Success)
            {
                string Function = TimeCommandGenerator(TimeCheckResults.DelayInduced, PayloadParts);
                this.AddToTriggers(TimeCheckResults.DelayPayload, string.Format("The payload in this request contains a code snippet which if executed will cause the response to be delayed by {0} milliseconds. The code snippet is: {1}", TimeCheckResults.DelayInduced, Function), TimeCheckResults.DelayRequest, "", string.Format("It took {0} milliseconds to recieve the response from the server. It took so long because of the {1} millisecond delay caused by the payload.", TimeCheckResults.DelayObserved, TimeCheckResults.DelayInduced), TimeCheckResults.DelayResponse);
                FindingReason reason = this.GetBlindReason(TimeCheckResults.DelayPayload, Function, TimeCheckResults);
                this.Reasons.Add(reason);
            }
        }

        string TimePayloadGenerator(int TimeDelayInMilliSeconds, object OtherInfo)
        {
            if (TimeDelayInMilliSeconds == 0)
            {
                return this.Scnr.PreInjectionParameterValue;
            }
            else
            {
                CodeInjectionPayloadParts PayloadParts = (CodeInjectionPayloadParts) OtherInfo;

                string Function = TimeCommandGenerator(TimeDelayInMilliSeconds, PayloadParts);
                return string.Format("{0}{1}{2}", PayloadParts.Prefix, Function, PayloadParts.Comment);
                //"{0}{1}{2}", p, func_to_execute, c);
            }
        }

        string TimeCommandGenerator(int TimeDelayInMilliSeconds, CodeInjectionPayloadParts PayloadParts)
        {
            if (TimeDelayInMilliSeconds == 0)
            {
                return this.Scnr.PreInjectionParameterValue;
            }
            else
            {

                int DelayCount = TimeDelayNumberCalculator(TimeDelayInMilliSeconds);
                return string.Format(PayloadParts.Function, DelayCount);
            }
        }

        int TimeDelayNumberCalculator(int TimeDelayInMilliSeconds)
        {
            return TimeDelayInMilliSeconds / 1000;
        }

        void AddToTriggers(string RequestTrigger, string RequestTriggerDesc, string ResponseTrigger, string ResponseTriggerDesc)
        {
            this.AddToTriggers(RequestTrigger, RequestTriggerDesc, this.Scnr.InjectedRequest.GetClone(), ResponseTrigger, ResponseTriggerDesc, this.Scnr.InjectionResponse.GetClone());
        }
        void AddToTriggers(string RequestTrigger, string RequestTriggerDesc, Request TriggerRequest, string ResponseTrigger, string ResponseTriggerDesc, Response TriggerResponse)
        {
            this.RequestTriggers.Add(RequestTrigger);
            this.ResponseTriggers.Add(ResponseTrigger);
            this.RequestTriggerDescs.Add(RequestTriggerDesc);
            this.ResponseTriggerDescs.Add(ResponseTriggerDesc);
            this.TriggerRequests.Add(TriggerRequest);
            this.TriggerResponses.Add(TriggerResponse);
            this.TriggerCount = this.TriggerCount + 1;
        }

        void AnalyzeTestResult()
        {
            if (this.RequestTriggers.Count == 1)
            {
                this.ReportCodeInjection(FindingConfidence.Medium);
            }
            else if (this.RequestTriggers.Count > 1)
            {
                this.ReportCodeInjection(FindingConfidence.High);
            }
        }

        void ReportCodeInjection(FindingConfidence confidence)
        {
            this.Scnr.SetTraceTitle("Code Injection Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Code Injection Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Code Injection"), this.GetSummary());
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
            string Summary = "Code Injection is an issue where it is possible to inject and execute code on the server-side. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/Code_Injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetErrorReason(string payload, string code, int num_a, int num_b)
        {
            //#payload - ';print 1234 + 7678;#
            //#code - print 1234 + 7678
            //#num_a - 1234
            //#num_b - 7678
            //#Reason = "IronWASP sent <i<hlg>>';print 1234 + 7678;#<i</hlg>> as payload to the application. This payload has a small snippet of code - <i<hlg>>print 1234 + 7678<i</hlg>>. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. This payload has a small snippet of code - <i<hlg>>{1}<i</hlg>>. ", payload, code);
            Reason = Reason + string.Format("If this code is executed then <i<hlg>>{0}<i</hlg>> and <i<hlg>>{1}<i</hlg>> will be added together and the sum of the addition will be printed back in the response. ", num_a, num_b);
            //#Reason = Reason + "The response that came back from the application after the payload was injected had the value <i>34345</i>, which is the sum of 1234 & 7678. This indicates that the injected code snippet could have been executed on the server-side."
            Reason = Reason + string.Format("The response that came back from the application after the payload was injected had the value <i<hlg>>{0}<i</hlg>>, which is the sum of <i<hlg>>{1}<i</hlg>> & <i<hlg>>{2}<i</hlg>>. ", num_a + num_b, num_a, num_b);
            Reason = Reason + "This indicates that the injected code snippet could have been executed on the server-side.";

            string ReasonType = "Error";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can manually inject the same payload but by changing the two numbers to some other value. Then you can observe if the response contains the sum of two numbers.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, this.TriggerCount, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindReason(string payload, string code, TimeBasedCheckResults TimeCheckResults)
        {
            //#Reason = "IronWASP sent <i>';sleep(5);#</i> as payload to the application. This payload has a small snippet of code - <i>sleep(5)</i>. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. This payload has a small snippet of code - <i<hlg>>{1}<i</hlg>>. ", Tools.EncodeForTrace(payload), code);
            Reason = Reason + string.Format("If this code is executed then the application will return the response <i<hlg>>{0}<i</hlg>> milliseconds later than usual. ", TimeCheckResults.DelayInduced);
            //#Reason = Reason + "After the payload was injected the response from the application took <i>6783</i> milliseconds. "
            Reason = Reason + string.Format("After the payload was injected the response from the application took <i<hlg>>{0}<i</hlg>> milliseconds. ", TimeCheckResults.DelayObserved);
            //#Reason = Reason + "Normally this particular request is processed at around <i>463</i> milliseconds. "
            Reason = Reason + string.Format("Normally this particular request is processed at around <i<hlg>>{0}<i</hlg>> milliseconds. ", TimeCheckResults.AverageBaseTime);
            Reason = Reason + "This indicates that the injected code snippet could have been executed on the server-side.";

            string ReasonType = "Blind";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can manually inject the same payload but by changing the number of seconds of delay to different values. Then you can observe if the time taken for the response to be returned is affected accordingly.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, this.TriggerCount, FalsePositiveCheck);
            return FR;
        }
    }

    public class CodeInjectionPayloadParts
    {
        public string Function;
        public string Prefix;
        public string Comment;
    }
}
