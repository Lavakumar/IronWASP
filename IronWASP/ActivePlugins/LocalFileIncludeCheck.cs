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
    public class LocalFileIncludeCheck : ActivePlugin
    {
        string[] null_terminator = new string[] { "\0", "" };
        Dictionary<string, string> files = new Dictionary<string, string>() { { "etc/passwd", "nix" }, { "boot.ini", "win" }, { "Windows\\Win.ini", "win" } };
        string[] file_ext = new string[] { "txt", "html", "jpg", "" };
        string[] drives = new string[] { "c:\\", "d:\\" };

        int TriggerCount = 0;
        string slash_prefix = "";

        public override ActivePlugin GetInstance()
        {
            LocalFileIncludeCheck p = new LocalFileIncludeCheck();
            p.Name = "Local File Include";
            p.Description = "Active Plugin to check for Local File Include/Directory Traversal vulnerabilities";
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
            this.Reasons = new List<FindingReason>();
            this.TriggerCount = 0;
            this.slash_prefix = "";
            this.CheckForLocalFileInclude();
        }

        void CheckForLocalFileInclude()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Local File Include:<i</h>>");
            this.slash_prefix = this.GetPrefix();
            this.CheckForLocalFileIncludeWithKnownFiles();
            this.CheckForLocalFileIncludeWithDownwardTraversal();
            this.AnalyzeTestResult();
        }

        string GetPrefix()
        {
            //#Prefix detection logic is inspired by the WAVSEP LFI test cases
            this.Scnr.Trace("<i<br>><i<b>>Identifying the prefix to use in payloads:<i</b>>");
            this.Scnr.RequestTrace("  Injected paylaod - aaa");
            Response prefix_base_res = this.Scnr.Inject("aaa");
            this.Scnr.ResponseTrace("    ==> This response will be used as baseline for prefix detection");

            string prefix = "";

            List<string> payloads = new List<string>() { "/", "\\", "file:/aaa" };
            List<string> messages = new List<string>() { "is a directory", "is a directory", "no such file or directory" };

            for (int i = 0; i < payloads.Count; i++)
            {
                string payload = payloads[i];
                string message = messages[i];
                this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                Response res = this.Scnr.Inject(payload);
                int CurrentOccuranceCount = Regex.Matches(res.BodyString, Regex.Escape(message), RegexOptions.IgnoreCase).Count;
                int BaseOccuranceCount = Regex.Matches(prefix_base_res.BodyString, Regex.Escape(message), RegexOptions.IgnoreCase).Count;
                if (CurrentOccuranceCount > BaseOccuranceCount)
                {
                    prefix = payload;
                    this.Scnr.ResponseTrace(string.Format("    ==> <i<cb>>The message '{0}' occured {1} times in this response and {2} time in the baseline response. This indicates that the prefix should be {3}<i</cb>>", message, CurrentOccuranceCount, BaseOccuranceCount, payload));
                    return prefix;
                }
                else
                {
                    this.Scnr.ResponseTrace(string.Format("    ==> Response does not indicate that {0} could be the prefix", payload));
                }
            }

            if (this.Scnr.PreInjectionParameterValue.StartsWith("/"))
            {
                prefix = "/";
            }
            else if (this.Scnr.PreInjectionParameterValue.StartsWith("\\"))
            {
                prefix = "\\";
            }
            else if (this.Scnr.PreInjectionParameterValue.StartsWith("file:"))
            {
                prefix = "file:";
            }
            return prefix;
        }

        void CheckForLocalFileIncludeWithKnownFiles()
        {
            List<string> file_exts = new List<string>();
            this.BaseResponse = this.Scnr.BaseResponse;
            string[] parts = this.Scnr.PreInjectionParameterValue.Split('.');
            if (parts.Length > 1)
            {
                file_exts.Add(parts[parts.Length - 1]);
            }
            file_exts.AddRange(this.file_ext);

            this.Scnr.Trace("<i<br>><i<h>>Checking for Local File Include with Known Files:<i</h>>");

            foreach (string f in this.files.Keys)
            {
                foreach (string nt in this.null_terminator)
                {
                    foreach (string fe in file_exts)
                    {
                        if (nt.Length == 0 && fe.Length > 0)
                        {
                            continue;//#no point in adding a file extension without a null terminator
                        }
                        string payload = "";
                        if (this.slash_prefix == "file:")
                        {
                            if (this.files[f] == "nix")
                            {
                                payload = string.Format("/{0}{1}", f, nt);
                            }
                            else
                            {
                                payload = string.Format("c:\\{0}{1}", f, nt);
                            }
                        }
                        else
                        {
                            if (this.files[f] == "nix")
                            {
                                payload = string.Format("{0}{1}{2}{3}", this.slash_prefix, Tools.MultiplyString("../", 15), f, nt);
                            }
                            else
                            {
                                payload = string.Format("{0}{1}{2}{3}", this.slash_prefix, Tools.MultiplyString("..\\", 15), f, nt);
                            }
                        }

                        if (fe.Length > 0)
                        {
                            payload = string.Format("{0}.{1}", payload, fe);
                        }
                        this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                        Response res = this.Scnr.Inject(payload);
                        string downloaded_file_info = this.GetDownloadedFileInfo(res, f);
                        if (downloaded_file_info.Length > 0)
                        {
                            this.Scnr.ResponseTrace(string.Format("    ==> <i<cr>>Response contains contens of {0}<i</cr>>", f));
                            if (this.slash_prefix == "file:")
                            {
                                this.AddToTriggers(payload, string.Format("The payload in this request refers to the {0} file by using the file: protocol. The payload is {1}", f, payload), downloaded_file_info, string.Format("This response contains contents of the {0} file. This was caused by the payload", f));
                            }
                            else
                            {
                                this.AddToTriggers(payload, string.Format("The payload in this request refers to the {0} file by traversing upwards in the directory structure. The payload is {1}", f, payload), downloaded_file_info, string.Format("This response contains contents of the {0} file. This was caused by the payload", f));
                            }
                            this.SetConfidence(3);
                            string slash = "";
                            if (this.files[f] == "nix")
                            {
                                slash = "/";
                            }
                            else
                            {
                                slash = "\\";
                            }
                            FindingReason reason = this.GetEchoReason(payload, f, downloaded_file_info, slash, this.TriggerCount, this.slash_prefix);
                            this.Reasons.Add(reason);
                        }
                        else
                        {
                            this.Scnr.ResponseTrace(string.Format("    ==> No trace of {0}", f));
                        }
                    }
                }
            }
        }

        void CheckForLocalFileIncludeWithDownwardTraversal()
        {
            if (this.Scnr.InjectedSection == "URL")
            {
                this.Scnr.Trace("<i<b>>Skipping LFI check with downward traversal since the current injection section is URL and this check will create False Positives.<i</b>>");
                return;
            }

            string[] slashes = new string[] { "/", "\\" };
            foreach (string slash in slashes)
            {
                this.CheckForLocalFileIncludeWithDownwardTraversalWithSlash(slash);
            }
        }

        void CheckForLocalFileIncludeWithDownwardTraversalWithSlash(string slash)
        {
            //#check downward traversal
            //#indicates presence of file read function and also a insecure direct object reference in that function
            this.Scnr.Trace("<i<br>><i<b>>Checking for Downward Directory Traversal:<i</b>>");
            this.Scnr.Trace(string.Format("<i<br>>Normal Response Code - {0}. Length -{0}", this.BaseResponse.Code, this.BaseResponse.BodyLength));

            string payload_a = string.Format("aa<s>..<s>{0}", this.Scnr.PreInjectionParameterValue);
            payload_a = payload_a.Replace("<s>", slash);
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_a));
            Response res_a = this.Scnr.Inject(payload_a);
            Request req_a = this.Scnr.InjectedRequest;
            this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_a.Code, res_a.BodyLength));

            string payload_a1 = string.Format("aa..<s>{0}", this.Scnr.PreInjectionParameterValue);
            payload_a1 = payload_a1.Replace("<s>", slash);
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_a1));
            Response res_a1 = this.Scnr.Inject(payload_a1);
            Request req_a1 = this.Scnr.InjectedRequest;
            this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_a1.Code, res_a1.BodyLength));

            string payload_b = string.Format("bb<s>..<s>{0}", this.Scnr.PreInjectionParameterValue);
            payload_b = payload_b.Replace("<s>", slash);
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_b));
            Response res_b = this.Scnr.Inject(payload_b);
            Request req_b = this.Scnr.InjectedRequest;
            this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_b.Code, res_b.BodyLength));

            string payload_b1 = string.Format("bb..<s>{0}", this.Scnr.PreInjectionParameterValue);
            payload_b1 = payload_b1.Replace("<s>", slash);
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload_b1));
            Response res_b1 = this.Scnr.Inject(payload_b1);
            Request req_b1 = this.Scnr.InjectedRequest;
            this.Scnr.ResponseTrace(string.Format("    ==> Got Response. Code- {0}. Length- {1}", res_b1.Code, res_b1.BodyLength));

            this.Scnr.Trace("<i<br>>Analysing the responses for patterns...");

            //#Analyzing the responses for patterns
            SimilarityChecker sc = new SimilarityChecker();
            sc.Add("a", res_a);
            sc.Add("a1", res_a1);
            sc.Add("b", res_b);
            sc.Add("b1", res_b1);
            sc.Check();

            List<Request> requests = new List<Request>() { req_a, req_a1, req_b, req_b1 };
            List<Response> responses = new List<Response>() { res_a, res_a1, res_b, res_b1 };
            List<string> request_trigger_descs = new List<string>();
            request_trigger_descs.Add(string.Format("This payload refers to the {0} file by doing a proper upward directory traversal of a dummy directory 'aa'. The payload is {1}", this.Scnr.PreInjectionParameterValue, payload_a));
            request_trigger_descs.Add(string.Format("This payload does not do a proper upward directory traversal of the dummy directory 'aa' and so does not refer to the {0} file. The payload is {1}", this.Scnr.PreInjectionParameterValue, payload_a1));
            request_trigger_descs.Add(string.Format("This payload refers to the {0} file by doing a proper upward directory traversal of a dummy directory 'bb'. The payload is {1}", this.Scnr.PreInjectionParameterValue, payload_b));
            request_trigger_descs.Add(string.Format("This payload does not do a proper upward directory traversal of the dummy directory 'bb' and so does not refer to the {0} file. The payload is {1}", this.Scnr.PreInjectionParameterValue, payload_b1));
            List<string> response_trigger_descs = new List<string>();
            response_trigger_descs.Add("The contents of this response are different from the response of the next trigger but are similar to the response of the trigger after the next.");
            response_trigger_descs.Add("The contents of this response are different from the response of the previous trigger but are similar to the response of the trigger after the next.");
            response_trigger_descs.Add("The contents of this response are different from the response of the next trigger but are similar to the response of the trigger before the previous.");
            response_trigger_descs.Add("The contents of this response are different from the response of the previous trigger but are similar to the response of the trigger before the previous.");
            List<string> request_triggers = new List<string>() { payload_a, payload_a1, payload_b, payload_b1 };
            List<string> response_triggers = new List<string>() { "", "", "", "" };

            foreach (SimilarityCheckerGroup group in sc.StrictGroups)
            {
                if (group.Count == 2)
                {
                    if (group.HasKey("a") && group.HasKey("b"))
                    {
                        this.Scnr.Trace("<i<br>><i<cr>>Responses for traversal based payloads are similar to each other and are different from non-traversal based responses. Indicates presence of LFI.<i</cr>>");

                        FindingReason reason = this.GetDiffReason(new List<string>() { payload_a, payload_a1, payload_b, payload_b1 }, this.Scnr.PreInjectionParameterValue, slash, this.TriggerCount, request_triggers.Count);
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
                }
            }
            foreach (SimilarityCheckerGroup group in sc.RelaxedGroups)
            {
                if (group.Count == 2)
                {
                    if (group.HasKey("a") && group.HasKey("b"))
                    {
                        this.Scnr.Trace("<i<br>><i<cr>>Responses for traversal based payloads are similar to each other and are different from non-traversal based responses. Indicates presence of LFI.<i</cr>>");

                        FindingReason reason = this.GetDiffReason(new List<string>() { payload_a, payload_a1, payload_b, payload_b1 }, this.Scnr.PreInjectionParameterValue, slash, this.TriggerCount, request_triggers.Count);
                        this.Reasons.Add(reason);

                        this.RequestTriggers.AddRange(request_triggers);
                        this.ResponseTriggers.AddRange(response_triggers);
                        this.RequestTriggerDescs.AddRange(request_trigger_descs);
                        this.ResponseTriggerDescs.AddRange(response_trigger_descs);
                        this.TriggerRequests.AddRange(requests);
                        this.TriggerResponses.AddRange(responses);
                        this.TriggerCount = this.TriggerCount + request_triggers.Count;
                        this.SetConfidence(1);
                        return;
                    }
                }
            }
            this.Scnr.Trace("<i<br>>The responses did not fall in any patterns that indicate LFI");
        }

        string GetDownloadedFileInfo(Response res, string file)
        {
            string bs = res.BodyString.ToLower();
            string bbs = this.BaseResponse.BodyString.ToLower();

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
                else if (bs_c == bbs_c && Regex.Matches(this.Scnr.PreInjectionParameterValue, Regex.Escape("passwd"), RegexOptions.IgnoreCase).Count > 0)
                {
                    return "root:!:x:0:0:";
                }
            }
            else if (file == "boot.ini")
            {
                int bs_c_1 = Regex.Matches(bs, Regex.Escape("[boot loader]"), RegexOptions.IgnoreCase).Count;
                int bbs_c_1 = Regex.Matches(bbs, Regex.Escape("[boot loader]"), RegexOptions.IgnoreCase).Count;
                int bs_c_2 = Regex.Matches(bs, Regex.Escape("multi("), RegexOptions.IgnoreCase).Count;
                int bbs_c_2 = Regex.Matches(bbs, Regex.Escape("multi("), RegexOptions.IgnoreCase).Count;
                if (bs_c_1 > bbs_c_1 && bs_c_2 > bbs_c_2)
                {
                    return "[boot loader]";
                }
                else if (bs_c_1 == bbs_c_1 && bs_c_2 == bbs_c_2 && Regex.Matches(this.Scnr.PreInjectionParameterValue, Regex.Escape("boot.ini"), RegexOptions.IgnoreCase).Count > 0)
                {
                    return "[boot loader]";
                }
            }
            else if (file == "Windows\\Win.ini")
            {
                int bs_c = Regex.Matches(bs, Regex.Escape("for 16-bit app support"), RegexOptions.IgnoreCase).Count;
                int bbs_c = Regex.Matches(bbs, Regex.Escape("for 16-bit app support"), RegexOptions.IgnoreCase).Count;
                if (bs_c > bbs_c)
                {
                    return "for 16-bit app support";
                }
                else if (bs_c == bbs_c && Regex.Matches(this.Scnr.PreInjectionParameterValue, Regex.Escape("Win.ini"), RegexOptions.IgnoreCase).Count > 0)
                {
                    return "for 16-bit app support";
                }
            }
            return "";
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
                this.ReportLocalFileInclude();
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

        void ReportLocalFileInclude()
        {
            this.Scnr.SetTraceTitle("Local File Include Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Local File Include Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Local File Include/Path Traversal"), this.GetSummary());
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
            string Summary = "Local File Include is an issue where it is possible to load and view the raw contents of any files present on the web server. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/Path_Traversal<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetEchoReason(string payload, string file_name, string file_contents, string slash, int Trigger, string Prefix)
        {
            payload = Tools.EncodeForTrace(payload);
            //#Reason = "IronWASP sent <i>../../../../../../../../../../../../../etc/passwd\000.txt</i> as payload to the application. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. ", payload);
            //#Reason = Reason + "This payload tries to refer to the file <i>/etc/passwd</i> by traversing from the current directory with a series of <i>../</i>. "
            if (Prefix == "file:")
            {
                Reason = Reason + string.Format("This payload tries to refer to the file <i<hlg>>{0}<i</hlg>> by using the <i<hlg>>file:<i</hlg>> protocol.", file_name);
            }
            else
            {
                Reason = Reason + string.Format("This payload tries to refer to the file <i<hlg>>{0}<i</hlg>> by traversing from the current directory with a series of <i<hlg>>..{1}<i</hlg>>. ", file_name, slash);
            }
            //#Reason = Reason + "If the application is vulnerable it will load the <i>/etc/passwd</i> file and send its contents in the response. "
            Reason = Reason + string.Format("If the application is vulnerable it will load the <i<hlg>>{0}<i</hlg>> file and send its contents in the response. ", file_name);
            //#Reason = Reason + "The response that came back from the application after the payload was injected had the text <i>root:x:0:0:</i>, which is usually found in <i>/etc/passwd</i> files. This indicates that the <i>/etc/passwd</i> file was loaded and its content printed in the response.".format(payload, code)
            Reason = Reason + string.Format("The response that came back from the application after the payload was injected had the text <i<hlg>>{0}<i</hlg>>, which is usually found in <i<hlg>>{1}<i</hlg>> files. This indicates that the <i<hlg>>{1}<i</hlg>> file was loaded and its content printed in the response.", file_contents, file_name);

            string ReasonType = "Echo";

            //#False Positive Check
            //#Reason = Reason + "To check if this was a valid case or a false positive you can first manually look at the response sent for this payload and determine if it actually contains the contents of the <i<hlg>>/etc/passwd<i</hlg>> file. "
            string FalsePositiveCheck = string.Format("To check if this was a valid case or a false positive you can first manually look at the response sent for this payload and determine if it actually contains the contents of the <i<hlg>>{0}<i</hlg>> file. ", file_name);
            FalsePositiveCheck = FalsePositiveCheck + "After that you can try changing the file name to something else and see if the server prints those file contents.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, Trigger, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetDiffReason(List<string> payloads, string file_name, string slash, int trigger_start, int trigger_count)
        {
            string Reason = "IronWASP sent four payloads to the application.<i<br>>";
            string[] ids = new string[] { "A", "B", "C", "D" };
            //#Payload A - <i>aa/../abcd.jpg</i>
            //#Payload B - <i>aa../abcd.jpg</i>
            //#Payload C - <i>bb/../abcd.jpg</i>
            //#Payload D - <i>bb../abcd.jpg</i>

            for (int i = 0; i < ids.Length; i++)
            {
                payloads[i] = Tools.EncodeForTrace(payloads[i]);
                Reason = Reason + string.Format("Payload {0} - <i<hlg>>{1}<i</hlg>><i<br>>", ids[i], payloads[i]);
            }

            Reason = Reason + string.Format("<i<br>>Payloads A and C are similar in nature. They both refer to the file <i<hlg>>{0}<i</hlg>> ", file_name);
            Reason = Reason + string.Format("by including an imaginary directory in the path (aa & bb) but then also invalidating it by traversing upwards by one directory using <i<hlg>>..{0}<i</hlg>>. ", slash);
            //#Reason = Reason + "So these payloads must have the same effect as refering to the file <i<hlg>>abcd.jpg<i</hlg>> normally."
            Reason = Reason + string.Format("So these payloads must have the same effect as referring to the file <i<hlg>>{0}<i</hlg>> normally.", file_name);

            //#Reason = Reason + "<i<br>>Payloads B and D are similar to each other but different from A & C. They refer to the file <i>abcd.jpg</i> inside invalid directories (aa & bb)."
            Reason = Reason + string.Format("<i<br>>Payloads B and D are similar to each other but different from A & C. They refer to the file <i<hlg>>{0}<i</hlg>> inside invalid directories (aa & bb).", file_name);

            Reason = Reason + "<i<br>>If the application is vulnerable to Local File Include then the response for Payloads A & C must be similar to each other and different from responses for Payloads B&D. ";
            Reason = Reason + "The responses for the injected payloads were analyzed and it was found that Payloads A & C got a similar looking response and were also different from responses got from Payloads B & D, thereby indicating the presence of this vulnerability.";

            //#Trigger
            List<int> trigger_ids = new List<int>();
            for (int i = trigger_start + 1; i < trigger_start + trigger_count + 1; i++)
            {
                trigger_ids.Add(i);
            }

            string ReasonType = "Diff";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can first manually look at the responses received for Payloads A, B, C and D. Analyze these payloads and verify if indeed A & C got similar responses and were different from B & D. ";
            FalsePositiveCheck = FalsePositiveCheck + string.Format("You can also change the payloads for A & C by adding one more invalid directory and one more <i<hlg>>..{0}<i</hlg>> to invalidate that directory. ", slash);
            FalsePositiveCheck = FalsePositiveCheck + "This must get the same response as the responses for A & C.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, trigger_ids, FalsePositiveCheck);
            return FR;
        }

    }
}
