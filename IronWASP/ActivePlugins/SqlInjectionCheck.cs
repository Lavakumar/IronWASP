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
    public class SqlInjectionCheck : ActivePlugin
    {
        static List<string> error_regex_raw = new List<string>();
        static List<Regex> error_regex = new List<Regex>();
        static List<string> time_check = new List<string>();
        int TriggerCount = 0;
        List<FindingReason> reasons = new List<FindingReason>();
        Response base_response;

        int[] ErrorCount = new int[] { 0, 0, 0 };
        List<string> Errors = new List<string>();
        int ErrorTriggerCount = 0;

        List<string> first_group = new List<string>();
        List<string> second_group = new List<string>();

        static bool SetUpDone = false;

        public override ActivePlugin GetInstance()
        {
            if (!SetUpDone)
            {
                SetUp();
                SetUpDone = true;
            }
            SqlInjectionCheck p = new SqlInjectionCheck();
            p.Name = "SQL Injection";
            p.Description = "Plugin to discover SQL Injection vulnerabilities";
            p.Version = "0.6";
            p.FileName = "Internal";
            return p;
        }

        //Override the Check method of the base class with custom functionlity
        public override void Check(Scanner Scnr)
        {

            this.Scnr = Scnr;
            this.RequestTriggers = new List<string>();
            this.ResponseTriggers = new List<string>();
            this.RequestTriggerDescs = new List<string>();
            this.ResponseTriggerDescs = new List<string>();
            this.TriggerRequests = new List<Request>();
            this.TriggerResponses = new List<Response>();
            this.TriggerCount = 0;
            this.reasons = new List<FindingReason>();
            this.ConfidenceLevel = 0;
            this.base_response = this.Scnr.BaseResponse;

            this.ErrorCount = new int[] { 0, 0, 0 };
            this.Errors = new List<string>();
            this.ErrorTriggerCount = 0;

            this.Scnr.Trace("<i<br>><i<h>>Checking for SQL Injection:<i</h>>");
            int overall_error_score = this.CheckForErrorBasedSQLi();
            int overall_blind_score = this.CheckForBlindSQLi();

            int overall_score = overall_error_score + overall_blind_score;

            if (this.RequestTriggers.Count == this.ErrorTriggerCount && (this.ErrorCount[0] + this.ErrorCount[1] + this.ErrorCount[2]) > 0 && (this.ErrorCount[0] == this.ErrorCount[1] && this.ErrorCount[1] == this.ErrorCount[2]))
            {
                this.ReportSQLError(this.Errors);
            }
            else if (overall_score > 7)
            {
                this.ReportSQLInjection(FindingConfidence.High);
            }
            else if (overall_score > 4)
            {
                this.ReportSQLInjection(FindingConfidence.Medium);
            }
            else if (overall_score > 3)
            {
                this.ReportSQLInjection(FindingConfidence.Low);
            }
            //overall_blind_score = this.CheckForBlindSQLi(Request, Scanner)
            //overall_score = overall_error_score + overall_blind_score
            //if(overall_score == 0):
            //	return
        }

        int CheckForErrorBasedSQLi()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Error based Injection:<i</h>>");
            this.Scnr.Trace("<i<br>>Sending a request with a normal value to get a Error baseline");
            this.Scnr.RequestTrace("  Injected 123 - ");
            Response err_base_res = this.Scnr.Inject("123");
            this.Scnr.ResponseTrace(string.Format("  ==> Code {0} | Length {0}", err_base_res.Code, err_base_res.BodyLength));

            List<string> payloads = new List<string>() { "'", "\"", "\xBF'\"(", "(", ")" };
            int final_error_score = 0;

            foreach (string payload in payloads)
            {
                this.Scnr.RequestTrace(string.Format("  Injected {0} - ", payload));
                Response inj_res;

                if (payload == "\xBF'\"(")
                {
                    inj_res = this.Scnr.RawInject(payload);
                }
                else
                {
                    inj_res = this.Scnr.Inject(payload);
                }

                int score = this.AnalyseInjectionResultForError(payload, inj_res, err_base_res);
                if (score > final_error_score)
                {
                    final_error_score = score;
                }
            }
            this.ErrorTriggerCount = this.RequestTriggers.Count;
            return final_error_score;
        }

        int AnalyseInjectionResultForError(string payload, Response payload_response, Response err_base_res)
        {
            Response res = payload_response;

            List<string> triggers = new List<string>();
            Dictionary<string, int[]> all_error_matches = new Dictionary<string, int[]>();

            int error_score = 0;
            for (int i = 0; i < error_regex.Count; i++)
            {
                Regex error_re = error_regex[i];
                string error_re_raw = error_regex_raw[i];
                Match matches = error_re.Match(res.BodyString);
                if (matches.Success)
                {
                    Match original_error_matches = error_re.Match(this.base_response.BodyString);
                    Match base_error_matches = error_re.Match(err_base_res.BodyString);
                    all_error_matches[error_re_raw] = new int[] { matches.Groups.Count, original_error_matches.Groups.Count, base_error_matches.Groups.Count };
                    for (int gi = 0; gi < matches.Groups.Count; gi++)
                    {
                        triggers.Add(matches.Groups[gi].Value);
                    }


                    this.ErrorCount[0] = this.ErrorCount[0] + matches.Groups.Count;
                    this.ErrorCount[1] = this.ErrorCount[1] + original_error_matches.Groups.Count;
                    this.ErrorCount[2] = this.ErrorCount[2] + base_error_matches.Groups.Count;
                }
            }

            if (all_error_matches.Count > 0)
            {
                this.Errors.AddRange(triggers);
                foreach (string error_key in all_error_matches.Keys)// for error_key,(inj_matches,base_matches,base_err_matches) in all_error_matches.items():
                {
                    int inj_matches = all_error_matches[error_key][0];
                    int base_matches = all_error_matches[error_key][1];
                    int base_err_matches = all_error_matches[error_key][2];

                    this.Scnr.ResponseTrace(string.Format("      <i<cr>>Got {0} occurance[s] of error signature. Normal Response had {1} occurance[s]<i</cr>>. Error Baseline Response had {2} occurance[s]<i</cr>>.<i<b>>Error Signature:<i</b>> {3}", inj_matches, base_matches, base_err_matches, error_key));
                    if (this.ErrorCount[0] == this.ErrorCount[1] && this.ErrorCount[1] == this.ErrorCount[2])
                    {
                        error_score = 4;
                    }
                    else
                    {
                        error_score = 7;
                    }
                }
            }
            else
            {
                this.Scnr.ResponseTrace("      No errors");
            }

            if (error_score > 0)
            {
                this.RequestTriggers.Add(payload);
                this.RequestTriggerDescs.Add(string.Format("The payload in this request is meant to trigger database error messages. The payload is {0}.", payload));
                this.ResponseTriggers.Add(string.Join("\r\n", triggers.ToArray()));
                this.ResponseTriggerDescs.Add("This response contains database error messages.");
                this.TriggerRequests.Add(this.Scnr.InjectedRequest.GetClone());
                this.TriggerResponses.Add(res);
                this.TriggerCount = this.TriggerCount + 1;

                FindingReason reason = this.GetErrorReason(payload, triggers, this.TriggerCount);
                this.reasons.Add(reason);
            }

            return error_score;
        }

        int CheckForBlindSQLi()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Blind Injection:<i</h>>");
            bool is_int = false;
            int int_value = 0;
            string str_value = "";
            str_value = this.Scnr.PreInjectionParameterValue.Replace("'", "").Replace("\"", "");

            try
            {
                int_value = Int32.Parse(this.Scnr.PreInjectionParameterValue);
                is_int = true;
            }
            catch { }

            int blind_int_math_score = 0;
            int blind_str_conc_score = 0;
            int blind_bool_score = 0;
            int blind_time_score = 0;

            if (is_int)
            {
                blind_int_math_score = this.InjectBlindIntMath(int_value);
            }
            else
            {
                blind_int_math_score = this.InjectBlindIntMath(0);
            }

            if (str_value.Length > 1)
            {
                blind_str_conc_score = this.InjectBlindStrConc(str_value);
            }

            blind_bool_score = this.InjectBlindBool();

            blind_time_score = this.CheckBlindTime();

            if (blind_int_math_score + blind_str_conc_score + blind_bool_score + blind_time_score > 0)
            {
                return 6;
            }
            else
            {
                return 0;
            }
        }

        int InjectBlindIntMath(int int_value)
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Blind Injection with Integer Math:<i</h>>");

            int val = int_value;

            /*
            #Addition Algo
            #if val < 2 then val = 3
            #(val - 1) + 1
            #(val - 2) + 2
            #
            #(val) + 5
            #(val - 1) + 6
            #
            #(val) + "a"
            #(val) + "b"
            #
            */

            if (val < 2)
            {
                val = 3;//#adjust the value to be suitable for addition based check
            }
            int[] plus_left = new int[] { val - 1, val - 2, val, val - 1, val, val };
            string[] plus_right = new string[] { "1", "2", "5", "6", "a", "b" };

            /*
            #Subtraction Algo
            #(val + 1) - 1
            #(val + 2) - 2
            #
            #if val < 6 then val = 11
            #
            #(val) - 5
            #(val + 1) - 6
            #
            #(val) - "a"
            #(val) - "b"
            #
            */

            val = int_value;
            int sub_val = 0;
            if (val < 6)
            {
                sub_val = 11;//#adjust the value to be suitable for subtraction based check
            }
            else
            {
                sub_val = val;
            }

            int[] minus_left = new int[] { val + 1, val + 2, sub_val, sub_val + 1, val, val };
            string[] minus_right = new string[] { "1", "2", "5", "6", "a", "b" };

            string[] symbols = new string[] { "+", "-" };
            string[] keys = new string[] { "a", "aa", "b", "bb", "c", "cc" };

            for (int ii = 0; ii < 2; ii++)
            {
                string sym = symbols[ii];
                List<int> left = new List<int>();
                List<string> right = new List<string>();
                if (sym == "+")
                {
                    this.Scnr.Trace("<i<br>>  <i<b>>Checking Addition:<i</b>>");
                    left.AddRange(plus_left);
                    right.AddRange(plus_right);
                }
                else
                {
                    this.Scnr.Trace("<i<br>>  <i<b>>Checking Subtraction:<i</b>>");
                    left.AddRange(minus_left);
                    right.AddRange(minus_right);
                }

                //#variables to keep track for rechecking
                string first_strict_signature = "";
                string first_relaxed_signature = "";
                string second_strict_signature = "";
                string second_relaxed_signature = "";
                int confidence = 0;
                bool vuln = false;
                bool first_strict_vuln = false;
                bool first_relaxed_vuln = false;

                for (int j = 0; j < 2; j++)
                {
                    if (j == 1 && !(first_strict_vuln || first_relaxed_vuln))
                    {
                        break;
                    }
                    List<string> payloads = new List<string>();
                    List<Request> requests = new List<Request>();
                    List<Response> responses = new List<Response>();
                    SimilarityChecker sc = new SimilarityChecker();
                    this.Scnr.Trace("<i<br>>");
                    for (int i = 0; i < 6; i++)
                    {
                        string payload = string.Format("{0}{1}{2}", left[i], sym, right[i]);
                        this.Scnr.RequestTrace(string.Format("  Request Key: '{0}' - Injecting {1} ", keys[i], payload));
                        Response res = this.Scnr.Inject(payload);
                        //#store the request and responses to be added to the vulnerability data if SQLi is found
                        payloads.Add(payload);
                        requests.Add(this.Scnr.InjectedRequest.GetClone());
                        responses.Add(res);
                        sc.Add(keys[i], res);
                        this.Scnr.ResponseTrace(string.Format(" ==> Code-{0} Length-{1}", res.Code, res.BodyLength));
                    }
                    sc.Check();

                    this.Scnr.Trace("<i<br>>  The responses are analyzed for similarity based grouping to determine if injection succeeded.");
                    this.Scnr.Trace("  Analysis Results:");
                    this.Scnr.Trace(string.Format("  Strict Groups Signature: {0}", sc.StrictGroupsSignature));
                    this.Scnr.Trace(string.Format("  Relaxed Groups Signature: {0}", sc.RelaxedGroupsSignature));

                    if (j == 0)
                    {
                        first_strict_signature = sc.StrictGroupsSignature;
                        first_relaxed_signature = sc.RelaxedGroupsSignature;

                        if (this.IsBlindMathInjectableGroupingCheck(sc.StrictGroups))
                        {
                            this.Scnr.Trace("  <i<b>>Strict Grouping indicates that injection succeeded. Rechecking to confirm.<i</b>>");
                            if (j == 0)
                            {
                                first_strict_vuln = true;
                            }
                        }
                        else
                        {
                            this.Scnr.Trace("  Strict Grouping does not indicates that injection succeeded.");
                        }

                        if (this.IsBlindMathInjectableGroupingCheck(sc.RelaxedGroups))
                        {
                            this.Scnr.Trace("  <i<b>>Relaxed Grouping indicates that injection succeeded. Rechecking to confirm.<i</b>>");
                            if (j == 0)
                            {
                                first_relaxed_vuln = true;
                            }
                            else
                            {
                                this.Scnr.Trace("  Relaxed Grouping does not indicates that injection succeeded.");
                            }
                        }
                    }
                    else
                    {
                        second_strict_signature = sc.StrictGroupsSignature;
                        second_relaxed_signature = sc.RelaxedGroupsSignature;
                        vuln = false;

                        if (first_strict_vuln && first_strict_signature == second_strict_signature)
                        {
                            vuln = true;
                            confidence = confidence + 1;
                            this.Scnr.Trace("  <i<cr>>Even the second time Strict Grouping indicates that injection succeeded.<i</cr>>");
                        }
                        else
                        {
                            this.Scnr.Trace("  Strict Grouping does not indicate that injection succeeded.");
                        }

                        if (first_relaxed_vuln && first_relaxed_signature == second_relaxed_signature)
                        {
                            vuln = true;
                            confidence = confidence + 1;
                            this.Scnr.Trace("  <i<cr>>Even the second time Relaxed Grouping indicates that injection succeeded.<i</cr>>");
                        }
                        else
                        {
                            this.Scnr.Trace("  Relaxed Grouping does not indicate that injection succeeded.");
                        }

                        if (vuln)
                        {
                            this.RequestTriggers.AddRange(payloads);
                            this.TriggerRequests.AddRange(requests);
                            this.TriggerResponses.AddRange(responses);

                            for (int i = 0; i < payloads.Count; i++)
                            {
                                this.ResponseTriggers.Add("");
                                this.ResponseTriggerDescs.Add("Refer to the 'Reasons' section of this vulnerabilty's description to understand how to interpret this response.");
                                if (i < 4)
                                {
                                    if (sym == "+")
                                    {
                                        this.RequestTriggerDescs.Add(string.Format("The payload in this request tries to add the numbers {0} and {1}.", plus_left[i], plus_right[i]));
                                    }
                                    else
                                    {
                                        this.RequestTriggerDescs.Add(string.Format("The payload in this request tries to subtract the number {0} from {1}.", minus_left[i], minus_right[i]));
                                    }
                                }
                                else
                                {
                                    if (sym == "+")
                                    {
                                        this.RequestTriggerDescs.Add(string.Format("The payload in this request is an invalid attempt to add the number {0} with string {1}.", plus_left[i], plus_right[i]));
                                    }
                                    else
                                    {
                                        this.RequestTriggerDescs.Add(string.Format("The payload in this request is an invalid attempt to subtract the number {0} from the string {1}.", minus_left[i], minus_right[i]));
                                    }
                                }

                            }
                            this.TriggerCount = this.TriggerCount + 6;

                            this.second_group = new List<string>();
                            foreach (string item in new string[] { "A", "B", "C", "D", "E", "F" })
                            {
                                if (this.first_group.Contains(item))
                                {
                                    this.second_group.Add(item);
                                }
                            }
                            FindingReason reason;
                            if (sym == "+")
                            {
                                reason = this.GetBlindMathAddReason(payloads, plus_left[0] + Int32.Parse(plus_right[0]), plus_left[2] + Int32.Parse(plus_right[2]), this.first_group, this.second_group, this.TriggerCount);
                            }
                            else
                            {
                                reason = this.GetBlindMathSubtractReason(payloads, minus_left[0] - Int32.Parse(minus_right[0]), minus_left[2] - Int32.Parse(minus_right[2]), this.first_group, this.second_group, this.TriggerCount);
                            }
                            this.reasons.Add(reason);

                            return confidence;
                        }
                    }
                }
            }
            return 0;
        }

        bool IsBlindMathInjectableGroupingCheck(List<SimilarityCheckerGroup> groups)
        {
            this.first_group = new List<string>();
            this.second_group = new List<string>();

            bool vuln = false;
            foreach (SimilarityCheckerGroup group in groups)
            {
                if (group.Count == 2 || group.Count == 4)
                {
                    int m = 0;
                    if (group.HasKey("a") && group.HasKey("aa"))
                    {
                        m = m + 1;
                        if (this.first_group.Count == 0)
                        {
                            this.first_group.Add("A");
                            this.first_group.Add("B");
                        }
                        else
                        {
                            this.second_group.Add("A");
                            this.second_group.Add("B");
                        }
                    }

                    if (group.HasKey("b") && group.HasKey("bb"))
                    {
                        m = m + 1;
                        if (this.first_group.Count == 0)
                        {
                            this.first_group.Add("C");
                            this.first_group.Add("D");
                        }
                        else
                        {
                            this.second_group.Add("C");
                            this.second_group.Add("D");
                        }
                    }

                    if (group.HasKey("c") && group.HasKey("cc"))
                    {
                        m = m + 1;
                        if (this.first_group.Count == 0)
                        {
                            this.first_group.Add("E");
                            this.first_group.Add("F");
                        }
                        else
                        {
                            this.second_group.Add("E");
                            this.second_group.Add("F");
                        }
                    }
                    if ((group.Count == 2 && m == 1) || (group.Count == 4 && m == 2))
                    {
                        //#indicates SQL Injection report it
                        vuln = true;
                    }

                }
                else
                {
                    vuln = false;
                    break;
                }
            }
            return vuln;
        }

        int InjectBlindStrConc(string str_value)
        {
            int BlindConcInjectionScore = 0;
            this.Scnr.Trace("<i<br>><i<h>>Checking for Blind Injection with String Concatenation:<i</h>>");
            List<Response> blind_str_conc_res = new List<Response>();
            if (str_value.Length < 2)
            {
                str_value = "aaa";
            }
            string str_value_first_part = str_value[0].ToString();
            string str_value_second_part = str_value.Substring(1);

            string[] quotes = new string[] { "'", "\"" };
            string[] joiners = new string[] { "||", "+", " " };
            string[] keys = new string[] { "Oracle", "MS SQL", "MySQL" };
            List<Request> requests = new List<Request>();
            List<Response> responses = new List<Response>();

            foreach (string quote in quotes)
            {
                if (quote == "'")
                {
                    this.Scnr.Trace("<i<br>>  <i<b>>Checking with Single Quotes:<i</b>>");
                }
                else
                {
                    this.Scnr.Trace("<i<br>>  <i<b>>Checking with Double Quotes:<i</b>>");
                }

                //#variables to keep track of rechecking
                string first_strict_signature = "";
                string first_relaxed_signature = "";
                string second_strict_signature = "";
                string second_relaxed_signature = "";
                int confidence = 0;
                bool vuln = false;
                bool first_strict_vuln = false;
                bool first_relaxed_vuln = false;

                for (int j = 0; j < 2; j++)
                {
                    if (j == 1 && !(first_strict_vuln || first_relaxed_vuln))
                    {
                        break;
                    }
                    List<string> payloads = new List<string>();
                    requests = new List<Request>();
                    responses = new List<Response>();
                    SimilarityChecker sc = new SimilarityChecker();
                    this.Scnr.Trace("<i<br>>");
                    for (int i = 0; i < 3; i++)
                    {
                        string payload = string.Format("{0}{1}{2}{3}{4}", str_value_first_part, quote, joiners[i], quote, str_value_second_part);
                        this.Scnr.RequestTrace(string.Format("  Request Key: '{0}' - Injecting {1}", keys[i], payload));
                        Response res = this.Scnr.Inject(payload);
                        payloads.Add(payload);
                        requests.Add(this.Scnr.InjectedRequest.GetClone());
                        responses.Add(res);
                        sc.Add(keys[i], res);
                        this.Scnr.ResponseTrace(string.Format(" ==> Code-{0} Length-{1}", res.Code, res.BodyLength));
                    }
                    sc.Check();

                    this.Scnr.Trace("<i<br>>  The responses are analyzed for similarity based grouping to determine if injection succeeded.");
                    this.Scnr.Trace("  Analysis Results:");
                    this.Scnr.Trace(string.Format("  Strict Groups Signature: {0}", sc.StrictGroupsSignature));
                    this.Scnr.Trace(string.Format("  Relaxed Groups Signature: {0}", sc.RelaxedGroupsSignature));
                    if (j == 0)
                    {
                        first_strict_signature = sc.StrictGroupsSignature;
                        first_relaxed_signature = sc.RelaxedGroupsSignature;

                        if (this.IsBlindStrConcInjectableGroupingCheck(sc.StrictGroups))
                        {
                            this.Scnr.Trace("  <i<b>>Strict Grouping indicates that injection succeeded. Rechecking to confirm.<i</b>>");
                            if (j == 0)
                            {
                                first_strict_vuln = true;
                            }
                        }
                        else
                        {
                            this.Scnr.Trace("  Strict Grouping does not indicates that injection succeeded.");
                        }

                        if (this.IsBlindStrConcInjectableGroupingCheck(sc.RelaxedGroups))
                        {
                            this.Scnr.Trace("  <i<b>>Relaxed Grouping indicates that injection succeeded. Rechecking to confirm.<i</b>>");
                            if (j == 0)
                            {
                                first_relaxed_vuln = true;
                            }
                        }
                        else
                        {
                            this.Scnr.Trace("  Relaxed Grouping does not indicates that injection succeeded.");
                        }
                    }
                    else
                    {
                        second_strict_signature = sc.StrictGroupsSignature;
                        second_relaxed_signature = sc.RelaxedGroupsSignature;
                        vuln = false;
                        string db = "";

                        if (first_strict_vuln && first_strict_signature == second_strict_signature)
                        {
                            vuln = true;
                            confidence = confidence + 1;
                            this.Scnr.Trace("  <i<cr>>Even the second time Strict Grouping indicates that injection succeeded.<i</cr>>");
                            foreach (SimilarityCheckerGroup g in sc.StrictGroups)
                            {
                                if (g.Count == 1)
                                {
                                    db = g.GetKeys()[0];
                                }
                            }
                        }
                        else
                        {
                            this.Scnr.Trace("  Strict Grouping does not indicate that injection succeeded.");
                        }

                        if (first_relaxed_vuln && first_relaxed_signature == second_relaxed_signature)
                        {
                            vuln = true;
                            confidence = confidence + 1;
                            this.Scnr.Trace("  <i<cr>>Even the second time Relaxed Grouping indicates that injection succeeded.<i</cr>>");
                            foreach (SimilarityCheckerGroup g in sc.RelaxedGroups)
                            {
                                if (g.Count == 1)
                                {
                                    db = g.GetKeys()[0];
                                }
                            }
                        }
                        else
                        {
                            this.Scnr.Trace("  Relaxed Grouping does not indicate that injection succeeded.");
                        }

                        if (vuln)
                        {
                            this.RequestTriggers.AddRange(payloads);
                            this.TriggerRequests.AddRange(requests);
                            this.TriggerResponses.AddRange(responses);
                            List<string> non_db = new List<string>();
                            non_db.AddRange(keys);
                            non_db.Remove(db);
                            for (int i = 0; i < payloads.Count; i++)
                            {
                                this.ResponseTriggers.Add("");
                                this.RequestTriggerDescs.Add(string.Format("The payload in this request tries to concatenate two strings as per {0} database's syntax. The payload is {1}", keys[i], payloads[i]));
                                if (keys[i] == db)
                                {
                                    this.ResponseTriggerDescs.Add(string.Format("This response is different from the responses recieved for the payloads that used {0} and {1} databases' concatenation syntax.", non_db[0], non_db[1]));
                                }
                                else
                                {
                                    non_db.Remove(keys[i]);
                                    this.ResponseTriggerDescs.Add(string.Format("This response is different from the response recieved for the payloads that used {0} database's concatenation syntax but similar to the response for the payload that used {1} database's concatenation syntax", db, non_db[0]));
                                    non_db.Add(keys[i]);
                                }
                            }
                            this.TriggerCount = this.TriggerCount + 3;

                            FindingReason reason = this.GetBlindConcatReason(payloads, db, this.TriggerCount);
                            this.reasons.Add(reason);

                            return confidence;
                        }
                    }
                }
            }
            return 0;
        }

        bool IsBlindStrConcInjectableGroupingCheck(List<SimilarityCheckerGroup> groups)
        {
            bool vuln = false;
            if (groups.Count == 2)
            {
                vuln = true;
            }
            return vuln;
        }

        int InjectBlindBool()
        {
            int score = 0;

            this.Scnr.Trace("<i<br>><i<h>>Checking for Blind Injection with Boolean check:<i</h>>");

            string prefix = this.Scnr.PreInjectionParameterValue;

            string[] int_trailers = new string[] { "8=8--", "7=5--", "7=7--", "5=8--" };
            string[] char_trailers = new string[] { "<q>s<q>=<q>s", "<q>s<q>=<q>r", "<q>t<q>=<q>t", "<q>t<q>=<q>r" };
            string[] keys = new string[] { "true-a", "false-a", "true-b", "false-b" };
            string[] quotes = new string[] { "'", "\"" };

            this.Scnr.Trace("<i<br>>  <i<b>>Checking with OR Operator:<i</b>>");

            string clean_prefix = prefix.Replace("'", "").Replace("\"", "");
            string or_prefix = clean_prefix + "xxx";///#this is to change the prefix to an invalid value to help with OR
            foreach (string quote in quotes)
            {
                score = score + this.CheckForBlindBoolWith(or_prefix, quote, "or", int_trailers);
                score = score + this.CheckForBlindBoolWith(or_prefix, quote, "or", char_trailers);
            }

            //#do one check with a number as prefix without any quotes
            if (clean_prefix == "21")
            {
                or_prefix = "22";
            }
            else
            {
                or_prefix = "21";
            }
            score = score + this.CheckForBlindBoolWith(or_prefix, "", "or", int_trailers);

            this.Scnr.Trace("<i<br>>  <i<b>>Checking with AND Operator:<i</b>>");
            foreach (string quote in quotes)
            {
                string and_prefix = prefix.Replace(quote, "");
                score = score + this.CheckForBlindBoolWith(and_prefix, quote, "and", int_trailers);
                score = score + this.CheckForBlindBoolWith(and_prefix, quote, "and", char_trailers);
            }
            return score;
        }

        int CheckForBlindBoolWith(string prefix, string quote, string operato, string[] trailers)
        {
            string[] keys = new string[] { "true-a", "false-a", "true-b", "false-b" };

            //#variables to keep track of rechecking
            string first_strict_signature = "";
            string first_relaxed_signature = "";
            string second_strict_signature = "";
            string second_relaxed_signature = "";
            int confidence = 0;
            bool vuln = false;
            bool first_strict_vuln = false;
            bool first_relaxed_vuln = false;

            for (int j = 0; j < 2; j++)
            {
                if (j == 1 && !(first_strict_vuln || first_relaxed_vuln))
                {
                    break;
                }
                List<string> payloads = new List<string>();
                List<Request> requests = new List<Request>();
                List<Response> responses = new List<Response>();
                List<string> conditions = new List<string>();
                SimilarityChecker sc = new SimilarityChecker();
                this.Scnr.Trace("<i<br>>");
                for (int i = 0; i < trailers.Length; i++)
                {
                    string payload = string.Format("{0}{1} {2} {3}", prefix, quote, operato, trailers[i].Replace("<q>", quote));
                    this.Scnr.RequestTrace(string.Format("  Request Key: '{0}' - Injecting {1}", keys[i], payload));
                    Response res = this.Scnr.Inject(payload);
                    payloads.Add(payload);
                    conditions.Add(trailers[i].Replace("<q>", quote));
                    requests.Add(this.Scnr.InjectedRequest.GetClone());
                    responses.Add(res);
                    sc.Add(keys[i], res);
                    this.Scnr.ResponseTrace(string.Format(" ==> Code-{0} Length-{1}", res.Code, res.BodyLength));
                }
                sc.Check();

                this.Scnr.Trace("<i<br>>  The responses are analyzed for similarity based grouping to determine if injection succeeded.");
                this.Scnr.Trace("  Analysis Results:");
                this.Scnr.Trace(string.Format("  Strict Groups Signature: {0}", sc.StrictGroupsSignature));
                this.Scnr.Trace(string.Format("  Relaxed Groups Signature: {0}", sc.RelaxedGroupsSignature));

                if (j == 0)
                {
                    first_strict_signature = sc.StrictGroupsSignature;
                    first_relaxed_signature = sc.RelaxedGroupsSignature;

                    if (this.IsBlindBoolInjectableGroupingCheck(sc.StrictGroups))
                    {
                        this.Scnr.Trace("  <i<b>>Strict Grouping indicates that injection succeeded. Rechecking to confirm.<i</b>>");
                        if (j == 0)
                        {
                            first_strict_vuln = true;
                        }
                    }
                    else
                    {
                        this.Scnr.Trace("  Strict Grouping does not indicates that injection succeeded.");
                    }

                    if (this.IsBlindBoolInjectableGroupingCheck(sc.RelaxedGroups))
                    {
                        this.Scnr.Trace("  <i<b>>Relaxed Grouping indicates that injection succeeded. Rechecking to confirm.<i</b>>");
                        if (j == 0)
                        {
                            first_relaxed_vuln = true;
                        }
                    }
                    else
                    {
                        this.Scnr.Trace("  Relaxed Grouping does not indicates that injection succeeded.");
                    }
                }
                else
                {
                    second_strict_signature = sc.StrictGroupsSignature;
                    second_relaxed_signature = sc.RelaxedGroupsSignature;
                    vuln = false;

                    if (first_strict_vuln && first_strict_signature == second_strict_signature)
                    {
                        vuln = true;
                        confidence = confidence + 1;
                        this.Scnr.Trace("  <i<cr>>Even the second time Strict Grouping indicates that injection succeeded.<i</cr>>");
                    }
                    else
                    {
                        this.Scnr.Trace("  Strict Grouping does not indicate that injection succeeded.");
                    }

                    if (first_relaxed_vuln && first_relaxed_signature == second_relaxed_signature)
                    {
                        vuln = true;
                        confidence = confidence + 1;
                        this.Scnr.Trace("  <i<cr>>Even the second time Relaxed Grouping indicates that injection succeeded.<i</cr>>");
                    }
                    else
                    {
                        this.Scnr.Trace("  Relaxed Grouping does not indicate that injection succeeded.");
                    }

                    if (vuln)
                    {
                        this.RequestTriggers.AddRange(payloads);
                        this.TriggerRequests.AddRange(requests);
                        this.TriggerResponses.AddRange(responses);
                        for (int i = 0; i < payloads.Count; i++)
                        {
                            this.ResponseTriggers.Add("");
                            if (i == 0 || i == 2)
                            {
                                this.RequestTriggerDescs.Add(string.Format("The payload in this request contains the conditional operator '{0}' followed by the SQL condition {1} which evaluates to true. The payload is {2}", operato, conditions[i], payloads[i]));
                            }
                            else
                            {
                                this.RequestTriggerDescs.Add(string.Format("The payload in this request contains the conditional operator '{0}' followed by the SQL condition {1} which evaluates to false. The payload is {2}", operato, conditions[i], payloads[i]));
                            }
                        }
                        this.ResponseTriggerDescs.Add("This response is the result of the first boolean true condition based payload. This response is equal to the response of the second boolean true condition payload and different from the responses of the boolean false condition payloads.");
                        this.ResponseTriggerDescs.Add("This response is the result of the first boolean false condition based payload. This response is equal to the response of the second boolean false condition payload and different from the responses of the boolean true condition payloads.");
                        this.ResponseTriggerDescs.Add("This response is the result of the second boolean true condition based payload. This response is equal to the response of the first boolean true condition payload and different from the responses of the boolean false condition payloads.");
                        this.ResponseTriggerDescs.Add("This response is the result of the second boolean false condition based payload. This response is equal to the response of the first boolean false condition payload and different from the responses of the boolean true condition payloads.");
                        this.TriggerCount = this.TriggerCount + 4;
                        FindingReason reason = this.GetBlindBoolReason(payloads, operato, this.TriggerCount);
                        this.reasons.Add(reason);
                        return confidence;
                    }
                }
            }
            return 0;
        }

        bool IsBlindBoolInjectableGroupingCheck(List<SimilarityCheckerGroup> groups)
        {
            int match = 0;
            if (groups.Count == 2)
            {
                foreach (SimilarityCheckerGroup group in groups)
                {
                    if (group.Count == 2)
                    {
                        match = 0;
                        if (group.HasKey("true-a") && group.HasKey("true-b"))
                        {
                            match = 1;
                        }
                        else if (group.HasKey("false-a") && group.HasKey("false-b"))
                        {
                            match = 1;
                        }
                    }
                }
            }

            if (match > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int CheckBlindTime()
        {
            int score = 0;
            this.Scnr.Trace("<i<br>><i<h>>Checking for Time based Injection:<i</h>>");
            //this.Scnr.Trace("<i<br>> Sending three requests to get a baseline of the response time for time based check:");
            //int min_delay = -1;
            //int max_delay = 0;
            //int time = 10000;
            //List<string> base_line_delays = new List<string>();
            //int avg_time = 0;
            //for (int i = 0; i < 3; i++)
            //{
            //    Response res = this.Scnr.Inject();
            //    avg_time = avg_time + res.RoundTrip;
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
            //this.Scnr.Trace(string.Join("<i<br>>", base_line_delays.ToArray()));
            //avg_time = avg_time / 3;

            //if (min_delay > 5000)
            //{
            //    time = ((max_delay + min_delay) / 1000) + 1;
            //}
            //else
            //{
            //    time = ((max_delay + 5000) / 1000) + 1;
            //}

            //this.Scnr.Trace(string.Format("<i<br>> Response Times: Minimum - {0}ms. Maximum - {1}ms.", min_delay, max_delay));
            //this.Scnr.Trace(string.Format("<i<br>> <i<b>>Testing with delay time of {0}ms.<i</b>>", time * 1000));
            SqlInjectionPayloadParts PayloadParts = new SqlInjectionPayloadParts();
            foreach (string inj_str in time_check)
            {
                PayloadParts.SqlCommand = inj_str;
                //string payload = inj_str.Replace("__TIME__", time.ToString());
                //score = score + this.InjectAndCheckBlindDelay(payload, time, avg_time);
                score = score + this.InjectAndCheckBlindDelay(PayloadParts);
            }
            return score;
        }

        //int InjectAndCheckBlindDelay(string payload, int time, int avg_time)
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        this.Scnr.RequestTrace(string.Format("  Injecting {0}", payload));
        //        Response res = this.Scnr.Inject(payload);
        //        string res_trace = string.Format("	==> Code-{0} Length-{1} Time-{2}ms.", res.Code, res.BodyLength, res.RoundTrip);
        //        if (i == 0)
        //        {
        //            if (res.RoundTrip >= (time * 1000))
        //            {
        //                this.Scnr.ResponseTrace(string.Format("{0} <i<b>>Delay Observed! Rechecking the result with the same Injection string<i</b>>", res_trace));
        //            }
        //            else
        //            {
        //                this.Scnr.ResponseTrace(string.Format("{0} No Time Delay.", res_trace));
        //                break;
        //            }
        //        }
        //        else if (i == 1)
        //        {
        //            if (res.RoundTrip >= (time * 1000))
        //            {
        //                this.Scnr.ResponseTrace(string.Format("{0} <i<br>><i<cr>>Delay Observed Again! Indicates Presence of SQL Injection<i</cr>>", res_trace));

        //                this.RequestTriggers.Add(payload);
        //                this.RequestTriggerDescs.Add(string.Format("The payload in this request contains a SQL query snippet which if executed will cause a delay of {0} milliseconds. The payload is {1}", time * 1000, payload));
        //                this.TriggerRequests.Add(this.Scnr.InjectedRequest.GetClone());

        //                this.ResponseTriggers.Add("");
        //                this.ResponseTriggerDescs.Add(string.Format("It took {0} milliseconds to get this response. It took so long because of the {1} milliseconds delay caused by the payload.", res.RoundTrip, time * 1000));
        //                this.TriggerResponses.Add(res);

        //                this.TriggerCount = this.TriggerCount + 1;
        //                FindingReason reason = this.GetBlindTimeReason(payload, time * 1000, res.RoundTrip, avg_time, this.TriggerCount);
        //                this.reasons.Add(reason);
        //                //#this.ReportSQLInjection()
        //                return 1;
        //            }
        //            else
        //            {
        //                this.Scnr.ResponseTrace(string.Format("{0} <i<b>>Time Delay did not occur again!<i</b>>", res_trace));
        //            }
        //        }
        //    }
        //    return 0;
        //}

        int InjectAndCheckBlindDelay(SqlInjectionPayloadParts PayloadParts)
        {
            TimeBasedCheckResults TimeCheckResults = DoTimeDelayBasedCheck(TimePayloadGenerator, PayloadParts);

            if (TimeCheckResults.Success)
            {

                this.RequestTriggers.Add(TimeCheckResults.DelayPayload);
                this.RequestTriggerDescs.Add(string.Format("The payload in this request contains a SQL query snippet which if executed will cause a delay of {0} milliseconds. The payload is {1}", TimeCheckResults.DelayInduced, TimeCheckResults.DelayPayload));
                this.TriggerRequests.Add(TimeCheckResults.DelayRequest);

                this.ResponseTriggers.Add("");
                this.ResponseTriggerDescs.Add(string.Format("It took {0} milliseconds to get this response. It took so long because of the {1} milliseconds delay caused by the payload.", TimeCheckResults.DelayObserved, TimeCheckResults.DelayInduced));
                this.TriggerResponses.Add(TimeCheckResults.DelayResponse);

                this.TriggerCount = this.TriggerCount + 1;
                FindingReason reason = this.GetBlindTimeReason(TimeCheckResults, this.TriggerCount);
                this.reasons.Add(reason);
                //#this.ReportSQLInjection()
                return 1;
            }
            return 0;
        }

        string TimePayloadGenerator(int TimeDelayInMilliSeconds, object OtherInfo)
        {
            if (TimeDelayInMilliSeconds == 0)
            {
                return this.Scnr.PreInjectionParameterValue;
            }
            else
            {
                SqlInjectionPayloadParts PayloadParts = (SqlInjectionPayloadParts)OtherInfo;

                return PayloadParts.SqlCommand.Replace("__TIME__", (TimeDelayInMilliSeconds/1000).ToString());
                //string Cmd = TimeCommandGenerator(TimeDelayInMilliSeconds, PayloadParts);
                //return string.Format("{0}{1} {2}", PayloadParts.Prefix, PayloadParts.Seperator, Cmd);
            }
        }

        void ReportSQLInjection(FindingConfidence Confidence)
        {
            this.Scnr.SetTraceTitle("SQLi Found", 100);
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "SQL Injection Detected";
            PR.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("SQL Injection"), this.GetSummary());
            foreach (FindingReason reason in this.reasons)
            {
                PR.AddReason(reason);
            }

            for (int i = 0; i < this.RequestTriggers.Count; i++)
            {
                PR.Triggers.Add(this.RequestTriggers[i], this.RequestTriggerDescs[i], this.TriggerRequests[i], this.ResponseTriggers[i], this.ResponseTriggerDescs[i], this.TriggerResponses[i]);
            }
            PR.Type = FindingType.Vulnerability;
            PR.Severity = FindingSeverity.High;
            PR.Confidence = Confidence;
            this.Scnr.AddFinding(PR);
        }

        void ReportSQLError(List<string> Errors)
        {
            this.Scnr.SetTraceTitle("SQL Error Messages Found", 100);
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "SQL Error Messages Found";
            string Summary = string.Format("{0} SQL Error Messages have been found in the response when testing this parameter. All checks performed to identify SQL Injection returned negative results so the reason for why these error messages appear cannot be determined.<i<br>>", this.GetFindingOpeningDesc("SQL Error Triggering"));
            Summary = Summary + "The error messages are:<i<br>>";
            foreach (string Error in Errors)
            {
                Summary = Summary + string.Format("<i<cr>>{0}<i</cr>><i<br>>", Error);
            }
            PR.Summary = Summary;
            if (this.RequestTriggers.Count > 0)
            {
                PR.Triggers.Add("", "", this.TriggerRequests[0], string.Join("\r\n", Errors.ToArray()), string.Format("The response contained {0} SQL error messages", Errors.Count), this.TriggerResponses[0]);
            }
            PR.Type = FindingType.Vulnerability;
            PR.Severity = FindingSeverity.Medium;
            PR.Confidence = FindingConfidence.High;
            this.Scnr.AddFinding(PR);
        }

        static void SetUp()
        {
            List<string> error_strings = new List<string>(File.ReadAllLines(Config.Path + "\\plugins\\active\\sql_error_regex.txt"));
            error_strings.RemoveAt(0);//#Ignore the first line containing comments
            foreach (string raw_err_str in error_strings)
            {
                string err_str = raw_err_str.Trim();
                if (err_str.Length > 0)
                {
                    error_regex_raw.Add(err_str);
                    error_regex.Add(new Regex(Regex.Escape(err_str), RegexOptions.Compiled | RegexOptions.IgnoreCase));
                }
            }
            List<string> time_check_temp = new List<string>(File.ReadAllLines(Config.Path + "\\plugins\\active\\sql_time_check.txt"));
            time_check_temp.RemoveAt(0);//#Ignore the first line containing comments
            foreach (string raw_tct in time_check_temp)
            {
                string tct = raw_tct.Trim();
                if (tct.Length > 0)
                {
                    time_check.Add(tct);
                }
            }
        }

        string GetSummary()
        {
            string Summary = "SQL Injection is an issue where it is possible execute SQL queries on the database being used on the server-side. For more details on this issue refer <i<cb>>https://www.owasp.org/index.php/SQL_Injection<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetErrorReason(string payload, List<string> errors, int Trigger)
        {
            payload = Tools.EncodeForTrace(payload);

            //#Reason = "IronWASP sent <i>'abcd<i> as payload to the application and the response that came back had the error message <i>Incorrect SQL syntax</i>. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application and the response that came back ", payload);

            if (errors.Count == 1)
            {
                Reason = Reason + string.Format("had the error message <i<hlg>>{0}<i</hlg>>. ", errors[0]);
            }
            else
            {
                Reason = Reason + "had the error messages ";
                for (int i = 0; i < errors.Count; i++)
                {
                    if (i == (errors.Count - 1))
                    {
                        Reason = Reason + " and ";
                    }
                    else if (i > 0)
                    {
                        Reason = Reason + " , ";
                    }
                    Reason = Reason + string.Format("<i<hlg>>{0}<i</hlg>>", errors[i]);
                }
                Reason = Reason + ".";
            }

            Reason = Reason + "This error message is usually associated with SQL query related errors and it appears that the payload was able to break out of the data context and cause this error. ";
            Reason = Reason + "This is an indication of SQL Injection.";

            string ReasonType = "Error";

            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the response received for the payload and confirm if the error message actually is because of some SQL related exception on the server-side. Try sending the same request without the payload and check if the error goes away.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, Trigger, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindMathAddReason(List<string> payloads, int first_sum, int second_sum, List<string> first_group, List<string> second_group, int Trigger)
        {
            string Reason = "IronWASP sent six payload to the application with SQL code snippets in them.<i<br>>";

            string[] ids = new string[] { "A", "B", "C", "D", "E", "F" };

            //#Payload A - <i>4+1<i>
            //#Payload B - <i>3+2<i>
            //#Payload C - <i>4+5<i>
            //#Payload D - <i>3+6<i>
            //#Payload E - <i>4+a<i>
            //#Payload F - <i>4+b<i>

            for (int i = 0; i < ids.Length; i++)
            {
                payloads[i] = Tools.EncodeForTrace(payloads[i]);
                Reason = Reason + string.Format("Payload {0} - <i<hlg>>{1}<i</hlg>><i<br>>", ids[i], payloads[i]);
            }

            //#Reason = Reason + "Payload A and B is the addition of two numbers whose sum 5. "
            Reason = Reason + string.Format("Payload A and B is the addition of two numbers whose sum would be <i<hlg>>{0}<i</hlg>>. ", first_sum);
            //#Reason = Reason + "Payload C and D is also the addition of two numbers whose sum would be 9. "
            Reason = Reason + string.Format("Payload C and D is also the addition of two numbers whose sum would be <i<hlg>>{0}<i</hlg>>. ", second_sum);
            Reason = Reason + "Payload E and F are invalid addition attempts as a number is being added to a string.<i<br>>";

            if (first_group.Count == 2)
            {
                Reason = Reason + "The response for Payload A and B is similar to each other and is different from Payloads C, D, E and F. ";
                Reason = Reason + "This indicates that the application actually performed the addition of the two numbers in the Payload A and B. ";
                Reason = Reason + "Since they add up to the same value their responses are similar. Payloads C and D add up to different values. ";
                Reason = Reason + "Payload E and F are invalid addition attempts. If the application was not actually performing addition then all six payload should have returned very similar responses. ";
            }
            else
            {
                Reason = Reason + "The response for Payload A, B, C and D are similar to each other and is different from Payloads E and F. ";
                Reason = Reason + "This indicates that the application actually performed the addition of the two numbers in the Payload A, B, C and D. ";
                Reason = Reason + "Since in all four cases the addition is a valid SQL syntax their responses are similar. ";
                Reason = Reason + "Payload E and F are invalid addition attempts so their responses are different. If the application was not actually performing addition then all six payloads should have returned very similar responses. ";
            }
            Reason = Reason + "Therefore this indicates that SQL syntax from the payload is executed as part of the SQL query on the server.";

            string ReasonType = "MathAdd";

            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the responses received for the six payloads and confirm if the type of similarity explained above actually exists in them. Try resending the same payloads again but with different numbers and check if this behaviour is repeated.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, new List<int>() { Trigger - 5, Trigger - 4, Trigger - 3, Trigger - 2, Trigger - 1, Trigger }, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindMathSubtractReason(List<string> payloads, int first_diff, int second_diff, List<string> first_group, List<string> second_group, int Trigger)
        {
            string Reason = "IronWASP sent six payload to the application with SQL code snippets in them.<i<br>>";

            string[] ids = new string[] { "A", "B", "C", "D", "E", "F" };

            //#Payload A - <i>4-1<i>
            //#Payload B - <i>5-2<i>
            //#Payload C - <i>7-5<i>
            //#Payload D - <i>8-6<i>
            //#Payload E - <i>4-a<i>
            //#Payload F - <i>4-b<i>


            for (int i = 0; i < ids.Length; i++)
            {
                payloads[i] = Tools.EncodeForTrace(payloads[i]);
                Reason = Reason + string.Format("Payload {0} - <i<hlg>>{1}<i</hlg>><i<br>>", ids[i], payloads[i]);
            }
            //#Reason = Reason + "Payload A and B is the subtraction of two numbers whose difference is 3. "
            Reason = Reason + string.Format("Payload A and B is the subtraction of two numbers whose difference is <i<hlg>>{0}<i</hlg>>. ", first_diff);
            //#Reason = Reason + "Payload C and D is also the subtraction of two numbers whose difference would be 2. "
            Reason = Reason + string.Format("Payload C and D is also the subtraction of two numbers whose difference would be <i<hlg>>{0}<i</hlg>>. ", second_diff);
            Reason = Reason + "Payload E and F are invalid subtraction attempts as a string is being deducted from a number.<i<br>>";

            if (first_group.Count == 2)
            {
                Reason = Reason + "The response for Payload A and B is similar to each other and is different from Payloads C, D, E and F. ";
                Reason = Reason + "This indicates that the application actually performed the subtraction of the two numbers in the Payload A and B. ";
                Reason = Reason + "Since their differnce is the same their responses are similar. Payloads C and D have a different difference values. ";
                Reason = Reason + "Payload E and F are invalid subtraction attempts. If the application was not actually performing subtraction then all six payload should have returned very similar responses. ";
            }
            else
            {
                Reason = Reason + "The response for Payload A, B, C and D are similar to each other and is different from Payloads E and F. ";
                Reason = Reason + "This indicates that the application actually performed the subtraction of the two numbers in the Payload A, B, C and D. ";
                Reason = Reason + "Since in all four cases the substration is a valid SQL syntax their responses are similar. ";
                Reason = Reason + "Payload E and F are invalid subtraction attempts so their responses are different. If the application was not actually performing subtraction then all six payloads should have returned very similar responses. ";
            }
            Reason = Reason + "Therefore this indicates that SQL syntax from the payload is executed as part of the SQL query on the server.";

            string ReasonType = "MathSubtract";

            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the responses received for the six payloads and confirm if the type of similarity explained above actually exists in them. Try resending the same payloads again but with different numbers and check if this behaviour is repeated.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, new List<int> { Trigger - 5, Trigger - 4, Trigger - 3, Trigger - 2, Trigger - 1, Trigger }, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindConcatReason(List<string> payloads, string db, int Trigger)
        {
            string Reason = "IronWASP sent three payloads to the application with SQL code snippets in them.<i<br>>";

            string[] ids = new string[] { "A", "B", "C" };

            //#Payload A - <i>a'||'b<i>
            //#Payload B - <i>a'+'b<i>
            //#Payload C - <i>a' 'b<i>

            for (int i = 0; i < ids.Length; i++)
            {
                payloads[i] = Tools.EncodeForTrace(payloads[i]);
                Reason = Reason + string.Format("Payload {0} - <i<hlg>>{1}<i</hlg>><i<br>>", ids[i], payloads[i]);
            }
            Reason = Reason + "Payload A is trying to concatenate two strings as per the SQL syntax of Oracle database servers. ";
            Reason = Reason + "Payload B is trying to concatenate the same two strings as per SQL syntax of MS SQL database servers. ";
            Reason = Reason + "Payload C is trying to concatenate the same two strings as per the SQL syntax of MySQL database servers.<i<br>>";

            List<string> same = new List<string>();
            string diff = "";

            //#keys = [ "Oracle", "MS SQL", "MySQL"]

            if (db == "Oracle")
            {
                diff = "A";
                same = new List<string>() { "B", "C" };
            }
            else if (db == "MS SQL")
            {
                diff = "B";
                same = new List<string>() { "A", "C" };
            }
            else if (db == "MySQL")
            {
                diff = "C";
                same = new List<string>() { "A", "B" };
            }
            else
            {
                return null;
            }

            //#Reason = Reason + "The response for Payload A and B were similar to each other and is different from the response recieved for Payloads C. "
            Reason = Reason + string.Format("The response for Payload {0} and {1} were similar to each other and is different from the response received for Payloads {2}. ", same[0], same[1], diff);
            //#Reason = Reason + "This indicates that the application was actually trying to perform the string concatenation on the server-side and that the backend database in use is MySQL. "
            Reason = Reason + string.Format("This indicates that the application was actually trying to perform the string concatenation on the server-side and that the backend database in use is <i<hlg>>{0}<i</hlg>>. ", db);
            //#Reason = Reason + "Since incase of MySQL Payloads A & B would have simply thrown an invalid SQL syntax exception their responses are similar. "
            Reason = Reason + string.Format("Since incase of <i<hlg>>{0}<i</hlg>> database server Payloads {0} & {1} would have simply thrown an invalid SQL syntax exception their responses are similar. ", db, same[0], same[1]);
            //#Reason = Reason + "And Payload C would have executed without this error and so its response was different than the other two.<i<br>>"
            Reason = Reason + string.Format("And Payload {0} would have executed without this error and so its response was different than the other two.<i<br>>", diff);

            Reason = Reason + "If the application was not actually performing the concatenation then all three payload should have received very similar responses. ";
            Reason = Reason + "Therefore this indicates that SQL syntax from the payload is executed as part of the SQL query on the server.";

            string ReasonType = "Concat";

            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the responses received for the three payloads and confirm if the type of similarity explained above actually exists in them. Try resending the same payloads again but with different strings and check if this behaviour is repeated.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, new List<int> { Trigger - 2, Trigger - 1, Trigger }, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindBoolReason(List<string> payloads, string bool_cond, int Trigger)
        {
            bool_cond = bool_cond.ToUpper();

            string Reason = "IronWASP sent four payloads to the application with SQL code snippets in them.<i<br>>";

            string[] ids = new string[] { "A", "B", "C", "D" };
            //#Payload A - <i>a' or 8=8--<i>
            //#Payload B - <i>a' or 7=5--<i>
            //#Payload C - <i>a' or 6=6--<i>
            //#Payload D - <i>a' or 4=6--<i>

            for (int i = 0; i < ids.Length; i++)
            {
                payloads[i] = Tools.EncodeForTrace(payloads[i]);
                Reason = Reason + string.Format("Payload {0} - <i<hlg>>{1}<i</hlg>><i<br>>", ids[i], payloads[i]);
            }

            //#Reason = Reason + "Payload A and C have a boolean condition after the OR keyword that will evaluate to true. The boolean condition in Payload B and D would evaluate to false.".format(payload)
            Reason = Reason + string.Format("Payload A and C have a boolean condition after the <i<hlg>>{0}<i</hlg>> keyword that will evaluate to true. ", bool_cond);
            Reason = Reason + "The boolean condition in Payload B and D would evaluate to false.<i<br>>";

            Reason = Reason + "The response for Payload A and C were similar to each other and were different from the response received for Payload B and D. ";
            Reason = Reason + "This indicates that the application was actually evaluating the boolean condition in the payloads. ";
            Reason = Reason + "So since Payload A and C both has a true boolean condition their responses are similar, C and D had a false boolean condition.<i<br>>";

            Reason = Reason + "If the application was not actually evaluating the boolean condition then all four payload should have returned very similar responses. ";
            Reason = Reason + "Therefore this indicates that SQL syntax from the payload is executed as part of the SQL query on the server.";

            string ReasonType = "Bool";

            //#False Positive Check
            string FalsePositiveCheck = "Manually analyze the responses received for the four payloads and confirm if the type of similarity explained above actually exists in them. Try resending the same payloads again but with values in the boolean expression and check if this behaviour is repeated.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, new List<int> { Trigger - 3, Trigger - 2, Trigger - 1, Trigger }, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindTimeReason(string payload, int delay_time, int res_time, int normal_time, int Trigger)
        {
            payload = Tools.EncodeForTrace(payload);

            //#Reason = "IronWASP sent <i>' and pg_sleep(5)--</i> as payload to the application. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. ", payload);
            //#Reason = Reason + "This payload has a small snippet of SQL code that will cause the database server to sleep for 5000 milliseconds. "
            Reason = Reason + string.Format("This payload has a small snippet of SQL code that will cause the database server to sleep for <i<hlg>>{0}<i</hlg>> milliseconds. ", delay_time);
            //#Reason = Reason + "If this code is executed then the application will return the response 5000 milliseconds later than usual. "
            Reason = Reason + string.Format("If this code is executed then the application will return the response <i<hlg>>{0}<i</hlg>> milliseconds later than usual. ", delay_time);
            //#Reason = Reason + "After the payload was injected the response from the application took <i>6783</i> milliseconds. "
            Reason = Reason + string.Format("After the payload was injected the response from the application took <i<hlg>>{0}<i</hlg>> milliseconds. ", res_time);
            //#Reason = Reason + "Normally this particular request is processed at around <i>463</i> milliseconds. "
            Reason = Reason + string.Format("Normally this particular request is processed at around <i</hlg>>{0}<i</hlg>> milliseconds. ", normal_time);
            Reason = Reason + "This indicates that the injected SQL code snippet could have been executed on the server-side.";

            string ReasonType = "TimeDelay";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can manually inject the same payload but by changing the number of seconds of delay to different values. Then you can observe if the time taken for the response to be returned is affected accordingly.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, Trigger, FalsePositiveCheck);
            return FR;
        }

        FindingReason GetBlindTimeReason(TimeBasedCheckResults TimeCheckResults, int Trigger)
        {
            //#Reason = "IronWASP sent <i>' and pg_sleep(5)--</i> as payload to the application. "
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. ", Tools.EncodeForTrace(TimeCheckResults.DelayPayload));
            //#Reason = Reason + "This payload has a small snippet of SQL code that will cause the database server to sleep for 5000 milliseconds. "
            Reason = Reason + string.Format("This payload has a small snippet of SQL code that will cause the database server to sleep for <i<hlg>>{0}<i</hlg>> milliseconds. ", TimeCheckResults.DelayInduced);
            //#Reason = Reason + "If this code is executed then the application will return the response 5000 milliseconds later than usual. "
            Reason = Reason + string.Format("If this code is executed then the application will return the response <i<hlg>>{0}<i</hlg>> milliseconds later than usual. ", TimeCheckResults.DelayInduced);
            //#Reason = Reason + "After the payload was injected the response from the application took <i>6783</i> milliseconds. "
            Reason = Reason + string.Format("After the payload was injected the response from the application took <i<hlg>>{0}<i</hlg>> milliseconds. ", TimeCheckResults.DelayObserved);
            //#Reason = Reason + "Normally this particular request is processed at around <i>463</i> milliseconds. "
            Reason = Reason + string.Format("Normally this particular request is processed at around <i</hlg>>{0}<i</hlg>> milliseconds. ", TimeCheckResults.AverageBaseTime);
            Reason = Reason + "This indicates that the injected SQL code snippet could have been executed on the server-side.";

            string ReasonType = "TimeDelay";

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can manually inject the same payload but by changing the number of seconds of delay to different values. Then you can observe if the time taken for the response to be returned is affected accordingly.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, Trigger, FalsePositiveCheck);
            return FR;
        }
    }

    public class SqlInjectionPayloadParts
    {
        public string SqlCommand = "";
    }
}
