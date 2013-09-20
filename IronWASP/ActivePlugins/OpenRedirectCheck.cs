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
    public class OpenRedirectCheck : ActivePlugin
    {

        List<string> basic_redirect_urls = new List<string>() { "http://<host>", "https://<host>", "//<host>", "<host>", "URL='http://<host>'", "5;URL='http://<host>'" };
        //#taken from http://kotowicz.net/absolute/
        List<string> full_redirect_urls = new List<string>() { "http://<host>", "https://<host>", "//<host>", "http:\\\\<host>", "https:\\\\<host>", "\\\\<host>", "/\\<host>", "\\/<host>", "\r//<host>", "/ /<host>", "http:<host>", "https:<host>", "http:/<host>", "https:/<host>", "http:////<host>", "https:////<host>", "://<host>", ".:.<host>", "<host>", "URL='http://<host>'", "5;URL='http://<host>'" };

        FindingReason reason;

        public override ActivePlugin GetInstance()
        {
            OpenRedirectCheck p = new OpenRedirectCheck();
            p.Name = "Open Redirect";
            p.Description = "Active Plugin to check for Open Redirect vulnerability";
            p.Version = "0.4";
            p.FileName = "Internal";
            return p;
        }

        //#Override the Check method of the base class with custom functionlity
        public override void Check(Scanner scnr)
        {
            this.Scnr = scnr;
            this.BaseRequest = this.Scnr.BaseRequest;
            this.reason = null;
            this.CheckForOpenRedirection();
        }

        void CheckForOpenRedirection()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Open Redirect:<i</h>>");
            List<string> urls = new List<string>();
            string uniq_str = "eziepwlivt";
            this.Scnr.Trace("<i<br>><i<h>>Checking if In-Domain Redirect Happens:<i</h>>");
            this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", uniq_str));
            Response res = this.Scnr.Inject(uniq_str);
            if (this.IsRedirectedTo(uniq_str, res, false).Length > 0)
            {
                this.Scnr.ResponseTrace("    ==> <i<b>>In-domain redirect happens. Using full payload set!<i</b>>");
                this.Scnr.SetTraceTitle("In-domain redirect happens", 5);
                urls.AddRange(this.full_redirect_urls);
            }
            else
            {
                this.Scnr.ResponseTrace("    ==> In-domain redirect does not happen. Using only basic payload set");
                urls.AddRange(this.basic_redirect_urls);
            }

            string host = this.BaseRequest.Host;
            //#remove the port number from hostname
            try
            {
                if (host.Contains(":"))
                {
                    host = host.Substring(host.IndexOf(":"));
                }
            }
            catch { }
            this.Scnr.Trace("<i<br>><i<h>>Checking if Out-of-Domain Redirect Happens:<i</h>>");
            foreach (string url in urls)
            {
                for (int i = 0; i < 2; i++)
                {
                    string h = "";
                    if (i == 0)
                    {
                        h = "example.org";
                    }
                    else
                    {
                        h = string.Format("{0}.example.org", host);
                    }
                    string payload = url.Replace("<host>", h);
                    this.Scnr.RequestTrace(string.Format("  Injected payload - {0}", payload));
                    res = this.Scnr.Inject(payload);
                    string redirected = "";
                    if (payload.StartsWith("5;") || payload.StartsWith("URL="))
                    {
                        string redirect_url = string.Format("http://{0}", h);
                        redirected = this.IsRedirectedTo(redirect_url, res, false);
                    }
                    else if (payload.StartsWith(h))
                    {
                        redirected = this.IsRedirectedTo(payload, res, true);
                    }
                    else
                    {
                        redirected = this.IsRedirectedTo(payload, res, false);
                    }
                    if (redirected.Length > 0)
                    {
                        this.reason = this.GetReason(payload, redirected);
                        this.Scnr.ResponseTrace("    ==> <i<cr>>Redirects to Injected payload!<i</cr>>");
                        this.ReportOpenRedirect(payload, string.Format("The payload in this request contains an url to the domain {0}. The payload is {1}", h, payload), payload, this.GetResponseTriggerDesc(redirected, h));
                        return;
                    }
                    else
                    {
                        this.Scnr.ResponseTrace("    ==> No redirect to payload");
                    }
                }
            }
        }

        string IsRedirectedTo(string ru, Response res, bool host_only)
        {
            if (!host_only)
            {
                //#check if redirection is happening through Location
                if (res.Headers.Has("Location"))
                {
                    string location_url = res.Headers.Get("Location");
                    if (this.IsLocationRedirected(location_url, ru))
                    {
                        return "Location-Header";
                    }
                }

                List<string> lus = res.Html.GetMetaContent("http-equiv", "Location");
                if (lus.Count > 0)
                {
                    if (this.IsLocationRedirected(lus[0], ru))
                    {
                        return "Location-Meta";
                    }
                }

                //#check if redirection is happening through Refresh
                if (res.Headers.Has("Refresh"))
                {
                    if (this.IsRefreshRedirected(res.Headers.Get("Refresh"), ru))
                    {
                        return "Refresh-Header";
                    }
                }

                List<string> rus = res.Html.GetMetaContent("http-equiv", "Refresh");
                if (rus.Count > 0)
                {
                    if (this.IsRefreshRedirected(rus[0], ru))
                    {
                        return "Refresh-Meta";
                    }
                }
            }
            //#check if redirection is happening through JavaScript
            //#location.href="url"
            //#navigate("url")
            //#location="url"
            //#location.Replace("url")
            if (res.BodyString.IndexOf(ru, StringComparison.OrdinalIgnoreCase) > -1)
            {//String .lower().count(ru) > 0:
                List<string> JS = res.Html.GetJavaScript();
                foreach (string script in JS)
                {
                    if (script.IndexOf(ru, StringComparison.OrdinalIgnoreCase) > -1) // . count(ru) > 0:
                    {
                        if (host_only)
                        {
                            if (Regex.Match(script, string.Format(@"location\.host\s*=\s*('|"")\s*{0}", ru), RegexOptions.IgnoreCase).Success)
                            {
                                return "JS-location.host";
                            }
                        }
                        else
                        {
                            if (Regex.Match(script, string.Format(@"location(\.href)*\s*=\s*('|"")\s*{0}", ru), RegexOptions.IgnoreCase).Success) //.format(re.escape(ru)), script):
                            {
                                return "JS-location.href";
                            }
                            else if (Regex.Match(script, string.Format(@"(navigate|location\.replace)\(\s*('|"")\s*{0}", ru), RegexOptions.IgnoreCase).Success) //.format(re.escape(ru)), script):
                            {
                                return "JS-*";
                            }
                        }
                    }
                }
            }
            return "";
        }

        bool IsLocationRedirected(string location, string redirect_url)
        {
            location = location.Trim();
            redirect_url = redirect_url.Trim();
            if (location.StartsWith(redirect_url, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IsRefreshRedirected(string refresh, string redirect_url)
        {
            refresh = refresh.Trim();
            try
            {
                string r_url = Tools.GetRefreshHeaderUrl(refresh);
                if (r_url.StartsWith(redirect_url, StringComparison.OrdinalIgnoreCase)) //.lower()):
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        void ReportOpenRedirect(string req_trigger, string req_trigger_desc, string res_trigger, string res_trigger_desc)
        {
            this.Scnr.SetTraceTitle("Open Redirect Found", 10);
            Finding pr = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            pr.Title = "Open Redirect Found";
            pr.Summary = string.Format("{0}<i<br>><i<br>>{1}", this.GetFindingOpeningDesc("Open redirect"), this.GetSummary());
            pr.AddReason(this.reason);
            pr.Triggers.Add(req_trigger, req_trigger_desc, this.Scnr.InjectedRequest, res_trigger, res_trigger_desc, this.Scnr.InjectionResponse);
            pr.Type = FindingType.Vulnerability;
            pr.Severity = FindingSeverity.High;
            pr.Confidence = FindingConfidence.High;
            this.Scnr.AddFinding(pr);
        }

        string GetSummary()
        {
            string Summary = "Open Redirect is an issue where it is possible to redirect the user to any arbitrary website from the vulnerable site. For more details on this issue refer <i<cb>>http://cwe.mitre.org/data/definitions/601.html<i</cb>><i<br>><i<br>>";
            return Summary;
        }

        FindingReason GetReason(string payload, string redir_type)
        {
            payload = Tools.EncodeForTrace(payload);

            //#Reason = Reason + "IronWASP sent <i>http://vulnsite.example.com</i> as payload to the application. The response that came back from the application to this payload had"
            string Reason = string.Format("IronWASP sent <i<hlg>>{0}<i</hlg>> as payload to the application. The response that came back from the application to this payload had ", payload);

            if (redir_type == "Location-Header")
            {
                Reason = Reason + string.Format("the value <i<hlg>>{0}<i</hlg>> in its 'Location' header.", payload);
            }
            else if (redir_type == "Location-Meta")
            {
                Reason = Reason + string.Format("the value <i<hlg>>{0}<i</hlg>> in its meta http-equiv tag for 'Location'. This is equivalent to having this value in the 'Location' header.", payload);
            }
            else if (redir_type == "Refresh-Header")
            {
                Reason = Reason + string.Format("the value <i<hlg>>{0}<i</hlg>> in its 'Refresh' header.", payload);
            }
            else if (redir_type == "Refresh-Meta")
            {
                Reason = Reason + string.Format("the value <i<hlg>>{0}<i</hlg>> in its meta http-equiv tag for 'Refresh'. This is equivalent to having this value in the 'Refresh' header.", payload);
            }
            else if (redir_type.StartsWith("JS"))
            {
                Reason = Reason + string.Format("the value <i<hlg>>{0}<i</hlg>> inside JavaScript of the page in such a way that it would cause a redirection to this value.", payload);
            }

            string ReasonType = redir_type;

            //#False Positive Check
            string FalsePositiveCheck = "To check if this was a valid case or a false positive you can manually send this payload from the browser and observe is the page is actually being redirect outside. If the browser does not perform a redirect then observe the HTML source of the page and try to identify why the page does not redirect inspite of the payload URL occurring in a section of the page that would trigger a redirect.";
            FalsePositiveCheck = FalsePositiveCheck + "<i<br>>If you discover that this issue was a false positive then please consider reporting this to <i<cb>>lava@ironwasp.org<i</cb>>. Your feedback will help improve the accuracy of the scanner.";

            FindingReason FR = new FindingReason(Reason, ReasonType, 1, FalsePositiveCheck);
            return FR;
        }

        string GetResponseTriggerDesc(string redir_type, string domain)
        {
            if (redir_type == "Location-Header")
            {
                return string.Format("This response contains a redirect to the domain {0} in its Location header. This redirect has been caused by the payload.", domain);
            }
            else if (redir_type == "Location-Meta")
            {
                return string.Format("This response contains a redirect to the domain {0} in its meta http-equiv tag for 'Location'. This redirect has been caused by the payload.", domain);
            }
            else if (redir_type == "Refresh-Header")
            {
                return string.Format("This response contains a redirect to the domain {0} in its Refresh header. This redirect has been caused by the payload.", domain);
            }
            else if (redir_type == "Refresh-Meta")
            {
                return string.Format("This response contains a redirect to the domain {0} in its meta http-equiv tag for 'Refresh'. This redirect has been caused by the payload.", domain);
            }
            else if (redir_type.StartsWith("JS"))
            {
                return string.Format("This response contains a redirect to the domain {0} in its JavaScript code. This redirect has been caused by the payload.", domain);
            }

            return string.Format("This response contains a redirect to the domain {0}. This redirect has been caused by the payload.", domain);
        }

    }
}
