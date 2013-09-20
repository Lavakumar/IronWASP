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
    public class CommandInjectionCheck : ActivePlugin
    {
        //#Check logic based on osCommanding.py of the W3AF project - http://w3af.sourceforge.net/
        List<string> seperators = new List<string>() { "", "&&", "|", ";" };
        List<string> prefixes = new List<string>();
        int TriggerCount = 0;
        //int time = 0;
        //int avg_delay = 0;
        //int buffer = 0;
        //int ping_count = 0;

        //#Override the GetInstance method of the base class to return a new instance with details
        public override ActivePlugin GetInstance()
        {
            CommandInjectionCheck p = new CommandInjectionCheck();
            p.Name = "Command Injection";
            p.Description = "Active Plugin to check for OS Command Injection vulnerabilities";
            p.Version = "0.5";
            p.FileName = "Internal";
            return p;
        }

        //#Override the Check method of the base class with custom functionlity
        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;
            this.BaseResponse = this.Scnr.BaseResponse;
            this.RequestTriggers = new List<string>();
            this.ResponseTriggers = new List<string>();
            this.RequestTriggerDescs = new List<string>();
            this.ResponseTriggerDescs = new List<string>();
            this.TriggerRequests = new List<Request>();
            this.TriggerResponses = new List<Response>();
            this.TriggerCount = 0;
            this.Reasons = new List<FindingReason>();
            this.CheckForCommandInjection();
            this.AnalyzeTestResults();
        }

        void CheckForCommandInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Command Injection:<i</h>>");
            //#start the checks
            this.prefixes = new List<string>() { "" };
            if (this.Scnr.PreInjectionParameterValue.Length > 0)
            {
                this.prefixes.Add(this.Scnr.PreInjectionParameterValue);
            }
            this.CheckForEchoBasedCommandInjection();
            this.CheckForTimeBasedCommandInjection();
        }

        void CheckForEchoBasedCommandInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Command Injection by Printing File Contents:<i</h>>");
            foreach (string prefix in this.prefixes)
            {
                string cmd = "";
                string payload = "";
                foreach (string seperator in this.seperators)
                {
                    cmd = "/bin/cat /etc/passwd";
                    payload = string.Format("{0}{1} {2}", prefix, seperator, cmd);
                    this.SendAndAnalyzeEchoPayload(payload, "etc/passwd", cmd);

                    cmd = "type %SYSTEMROOT%\\win.ini";
                    payload = string.Format("{0}{1} {2}", prefix, seperator, cmd);
                    this.SendAndAnalyzeEchoPayload(payload, "win.ini", cmd);
                }

                cmd = "/bin/cat /etc/passwd";
                payload = string.Format("{0} `{1}`", prefix, cmd);
                this.SendAndAnalyzeEchoPayload(payload, "etc/passwd", cmd);

                cmd = "run type %SYSTEMROOT%\\win.ini";
                payload = string.Format("{0} {1}", prefix, cmd);
                this.SendAndAnalyzeEchoPayload(payload, "win.ini", cmd);
            }
        }

        void CheckForTimeBasedCommandInjection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Command Injection by Inducing Time Delay:<i</h>>");

            foreach (string prefix in this.prefixes)
            {
                CommandInjectionPayloadParts PayloadParts = new CommandInjectionPayloadParts();
                foreach (string seperator in this.seperators)
                {
                    PayloadParts.Prefix = prefix;
                    PayloadParts.Seperator = seperator;

                    PayloadParts.Command = "ping -n {0} localhost";
                    this.SendAndAnalyzeTimePayload(PayloadParts);

                    PayloadParts.Command = "ping -c {0} localhost";
                    this.SendAndAnalyzeTimePayload(PayloadParts);

                    PayloadParts.Command = "/usr/sbin/ping -s localhost 1000 {0} ";
                    this.SendAndAnalyzeTimePayload(PayloadParts);
                }

                PayloadParts = new CommandInjectionPayloadParts();
                PayloadParts.Prefix = prefix;
                PayloadParts.Seperator = "";

                PayloadParts.Command = "`ping -c {0} localhost`";
                this.SendAndAnalyzeTimePayload(PayloadParts);

                PayloadParts.Command = "run ping -n {0} localhost";
                this.SendAndAnalyzeTimePayload(PayloadParts);
            }
        }

        void SendAndAnalyzeEchoPayload(string payload, string file_echoed, string cmd)
        {
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
            Response res = this.Scnr.Inject(payload);
            string echoed_file_info = this.GetDownloadedFileInfo(res, file_echoed);
            if (echoed_file_info.Length > 0)
            {
                this.Scnr.ResponseTrace(string.Format("    ==> <i<cr>>Response contains contens of {0}<i</cr>>", file_echoed));
                this.AddToTriggers(payload, string.Format("The payload in this request contains a system command which if executed will add the numbers prints the contens of the {0} file on the server. The system command is : {1}", file_echoed, cmd), echoed_file_info, string.Format("This response body contains the contents of the {0} file", file_echoed));
                FindingReason reason = this.GetErrorReason(payload, cmd, file_echoed, echoed_file_info);
                this.Reasons.Add(reason);
            }
            else
            {
                this.Scnr.ResponseTrace(string.Format("    ==> No trace of {0}", file_echoed));
            }
        }

        void SendAndAnalyzeTimePayload(CommandInjectionPayloadParts PayloadParts)
        {
            TimeBasedCheckResults TimeCheckResults = DoTimeDelayBasedCheck(TimePayloadGenerator, PayloadParts);
            
            if (TimeCheckResults.Success)
            {
                string Cmd = TimeCommandGenerator(TimeCheckResults.DelayInduced, PayloadParts);
                this.AddToTriggers(TimeCheckResults.DelayPayload, string.Format("The payload in this request contains a system command which if executed will cause the response to be delayed by {0} milliseconds. The system command is: {1}", TimeCheckResults.DelayInduced, Cmd), TimeCheckResults.DelayRequest, "", string.Format("It took {0}milliseconds to recieve the response from the server. It took so long because of the {1} millisecond delay caused by the payload.", TimeCheckResults.DelayObserved, TimeCheckResults.DelayInduced), TimeCheckResults.DelayResponse);
                FindingReason reason = this.GetBlindReason(TimeCheckResults.DelayPayload, Cmd, TimeCheckResults);
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
                CommandInjectionPayloadParts PayloadParts = (CommandInjectionPayloadParts) OtherInfo;
                
                string Cmd = TimeCommandGenerator(TimeDelayInMilliSeconds, PayloadParts);
                return string.Format("{0}{1} {2}", PayloadParts.Prefix, PayloadParts.Seperator, Cmd);
            }
        }

        string TimeCommandGenerator(int TimeDelayInMilliSeconds, CommandInjectionPayloadParts PayloadParts)
        {
            if (TimeDelayInMilliSeconds == 0)
            {
                return this.Scnr.PreInjectionParameterValue;
            }
            else
            {

                int PingCount = PingCountCalculator(TimeDelayInMilliSeconds);
                return string.Format(PayloadParts.Command, PingCount);
            }
        }

        int PingCountCalculator(int TimeDelayInMilliSeconds)
        {
            return (TimeDelayInMilliSeconds / 1000) + 1;
        }

        string GetDownloadedFileInfo(Response res, string file)
        {
            string bs = res.BodyString;
            string bbs = this.BaseResponse.BodyString;

            if (file == "etc/passwd")
            {
                int bs_c = Regex.Matches(bs, Regex.Escape("root:x:0:0:"), RegexOptions.IgnoreCase).Count;
                int bbs_c = Regex.Matches(bbs, Regex.Escape("root:x:0:0:"), RegexOptions.IgnoreCase).Count;
                if (bs_c > bbs_c)
                {
                    return "root:x:0:0:";
                }
                else if (bs_c == bbs_c && Regex.Matches(this.Scnr.PreInjectionParameterValue, Regex.Escape("etc/passwd"), RegexOptions.IgnoreCase).Count > 0)
                {
                    return "root:x:0:0:";
                }

                bs_c = Regex.Matches(bs, Regex.Escape("root:!:x:0:0:"), RegexOptions.IgnoreCase).Count;
                bbs_c = Regex.Matches(bbs, Regex.Escape("root:!:x:0:0:"), RegexOptions.IgnoreCase).Count;
                if (bs_c > bbs_c)
                {
                    return "root:!:x:0:0:";
                }
                else if (bs_c == bbs_c && Regex.Matches(this.Scnr.PreInjectionParameterValue, Regex.Escape("etc/passwd"), RegexOptions.IgnoreCase).Count > 0)
                {
                    return "root:!:x:0:0:";
                }
            }
            else if (file == "win.ini")
            {
                int bs_c = Regex.Matches(bs, Regex.Escape("[fonts]"), RegexOptions.IgnoreCase).Count;
                int bbs_c = Regex.Matches(bbs, Regex.Escape("[fonts]"), RegexOptions.IgnoreCase).Count;
                if (bs_c > bbs_c)
                {
                    return "[fonts]";
                }
                else if (bs_c == bbs_c && Regex.Matches(this.Scnr.PreInjectionParameterValue, Regex.Escape("win.ini"), RegexOptions.IgnoreCase).Count > 0)
                {
                    return "[fonts]";
                }
            }
            return "";
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

        void AnalyzeTestResults()
        {
            if (this.RequestTriggers.Count > 0)
            {
                this.ReportCommandInjection();
            }
        }

        void ReportCommandInjection()
        {
            this.Scnr.SetTraceTitle("Command Injection Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Command Injection Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Command Injection"), this.GetSummary());
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
            pr.Confidence = FindingConfidence.High;
            this.Scnr.AddFinding(pr);
        }

        string GetSummary()
        {
            string Summary = "Command Injection is an issue where it is possible to inject and execute operating system commands on the server-side. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/Command_Injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetErrorReason(string payload, string cmd, string echoed_file, string file_content_match)
        {
            //#payload - ';print 1234 + 7678;#
            //#code - print 1234 + 7678
            //#num_a - 1234
            //#num_b - 7678

            //#Reason = "IronWASP sent <i>'; /bin/cat /etc/passwd</i> as payload to the application. This payload has a small system command - <i>/bin/cat /etc/passwd</i>. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. This payload has a small system command - <i<hlg>>{1}<i</hlg>>. ", payload, cmd);
            //#Reason = Reason + "If this command is executed by the server then the contents of the <i<hlg>>/etc/passwd<i</hlg>> file will be present in the response. ".format(echoed_file)
            Reason = Reason + string.Format("If this command is executed by the server then the contents of the <i<hlg>>{0}<i</hlg>> file will be present in the response. ", echoed_file);
            //#Reason = Reason + "The response that came back from the application after the payload was injected had the text <i<hlg>>root:x:0:0:<i</hlg>>, which is usually found in <i<hlg>>/etc/passwd<i</hlg>> files. "
            Reason = Reason + string.Format("The response that came back from the application after the payload was injected had the text <i<hlg>>{0}<i</hlg>>, which is usually found in <i<hlg>>{1}<i</hlg>> files. ", file_content_match, echoed_file);
            Reason = Reason + string.Format("This indicates that the injected command was executed by the server and the contents of the <i<hlg>>{0}<i</hlg>> file was printed in the response.", echoed_file);

            string ReasonType = "Error";

            //#False Positive Check
            string FalsePositiveCheck = string.Format("To check if this was a valid case or a false positive you can first manually look at the response sent for this payload and determine if it actually contains the contents of the <i<hlg>>{0}<i</hlg>> file. ", echoed_file);
            FalsePositiveCheck = FalsePositiveCheck + "After that you can try changing the file name to something else and see if the server prints those file contents.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, this.TriggerCount, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindReason(string payload, string cmd, TimeBasedCheckResults TimeCheckResults)
        {
            //#Reason = "IronWASP sent <i>'; ping -n 8 localhost</i> as payload to the application. This payload has a small system command - <i>ping -n 8 localhost</i>. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. This payload has a small system command - <i<hlg>>{1}<i</hlg>>. ", Tools.EncodeForTrace(payload), cmd);
            //#Reason = Reason + "If this command is executed then the server will ping itself 8 times. This will cause the response to be returned around 5000 milliseconds later than usual. "
            Reason = Reason + string.Format("If this command is executed then the server will ping itself <i<hlg>>{0}<i</hlg>> times. This will cause the response to be returned around <i<hlg>>{1}<i</hlg>> milliseconds later than usual. ", PingCountCalculator(TimeCheckResults.DelayInduced), TimeCheckResults.DelayInduced);
            //#Reason = Reason + "After the payload was injected the response from the application took <i>6783</i> milliseconds. Normally this particular request is processed at around <i>463</i> milliseconds. "
            Reason = Reason + string.Format("After the payload was injected the response from the application took <i<hlg>>{0}<i</hlg>> milliseconds. Normally this particular request is processed at around <i<hlg>>{1}<i</hlg>> milliseconds. ", TimeCheckResults.DelayObserved, TimeCheckResults.AverageBaseTime);
            Reason = Reason + "This indicates that the injected command could have been executed on the server-side.";

            string ReasonType = "Blind";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can manually inject the same payload but by changing the number of ping requests sent to different values. Then you can observe if the time taken for the response to be returned is affected accordingly.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, this.TriggerCount, FalsePositiveCheck);
            return FR;
        }
    }

    public class CommandInjectionPayloadParts
    {
        public string Command;
        public string Prefix;
        public string Seperator;
    }
}
