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
    public class CrossSiteScriptingCheck : ActivePlugin
    {
        string ProbeString = "";
        Request ProbeStringRequest;
        Response ProbeStringResponse;

        List<ReflectionContext> ProbeStringContexts = new List<ReflectionContext>();

        List<string> injectable_special_tags = new List<string>();
        List<string> injectable_special_attributes = new List<string>();

        public override ActivePlugin GetInstance()
        {
            CrossSiteScriptingCheck XssCheck = new CrossSiteScriptingCheck();
            XssCheck.Name = "Cross-site Scripting";
            XssCheck.Version = "0.6";
            XssCheck.Description = "Active Plugin to detect Cross-site Scripting vulnerabilities";
            XssCheck.FileName = "Internal";
            return XssCheck;
        }

        public override void Check(Scanner Scnr)
        {
            this.Scnr = Scnr;
            this.BaseRequest = Scnr.BaseRequest;
            this.BaseResponse = Scnr.BaseResponse;

            this.CheckReflectionWithProbeString();
            this.CheckCharsetSecurity();
            this.CheckForCrossSiteCookieSetting();
            //Show the reflection contexts to user and show the list of payloads that are going to be sent

            //Do Context specific checks
            foreach( ReflectionContext context in this.ProbeStringContexts)
            {
                if(context == ReflectionContext.JS)
                {
                    this.CheckForInjectionInFullJS();
                }
                else if(context == ReflectionContext.InLineJS || context == ReflectionContext.JSUrl || context == ReflectionContext.EventAttribute)
                {
                    this.CheckForInjectionInJSInsideHTML();
                }
                else if(context == ReflectionContext.InLineVB || context == ReflectionContext.VBUrl)
                {
                    this.CheckForInjectionInVBInsideHTML();
                }
                else if(context == ReflectionContext.UrlAttribute)
                {
                    this.CheckForInjectionInUrlAttribute();
                }
                else if(context == ReflectionContext.CSS || context == ReflectionContext.InLineCSS)
                {
                    this.CheckForInjectionInFullCSS();
                }
                else if(context == ReflectionContext.AttributeCSS)
                {
                    this.CheckForInjectionInCSSInsideStyleAttribute();
                }
                else if(context == ReflectionContext.AttributeName)
                {
                    this.CheckForInjectionInAttributeName();
                }
                else if(context == ReflectionContext.AttributeValueWithSingleQuote)
                {
                    this.CheckForInjectionInSingleQuoteAttributeValue();
                }
                else if(context == ReflectionContext.AttributeValueWithDoubleQuote)
                {
                    this.CheckForInjectionInDoubleQuoteAttributeValue();
                }
            }
    
            //Do a HTML Injection Check irrespective of the context
            this.CheckForInjectionInHtml();
    
            //Check for Injection in Special Attributes
            this.CheckForInjectionInSpecialAttributes();
    
            //scan is complete, analyse the results
            this.AnalyseResults();

        }

        void CheckReflectionWithProbeString()
        {
            this.ProbeString = Analyzer.GetProbeString();
            //Check if this string is already found in the response body
            while (this.BaseResponse.BodyString.Contains(this.ProbeString))
            {
                this.ProbeString = Analyzer.GetProbeString();
            }

            this.Scnr.Trace("<i<br>><i<h>>Checking Reflection Contexts with a Probe String:<i</h>>");
            this.Scnr.RequestTrace(string.Format("  Injected Probe String - {0}", this.ProbeString));

            this.ProbeStringResponse = this.Scnr.Inject(this.ProbeString);
            this.ProbeStringRequest = this.Scnr.InjectedRequest.GetClone();
            
            //Store the ProbeString in Analyzer for Stored XSS Reflection Checking
            Analyzer.AddProbeString(this.ProbeString, this.Scnr.InjectedRequest.LogId);

            if(this.ProbeStringResponse.BodyString.Contains(this.ProbeString))
            {
                this.ProbeStringContexts = this.GetContext(this.ProbeString, this.ProbeStringResponse);
                
                //make the list unique
                Dictionary<ReflectionContext, int> TempDict = new Dictionary<ReflectionContext, int>();
                foreach (ReflectionContext RC in this.ProbeStringContexts)
                {
                    TempDict[RC] = 0;
                }
                this.ProbeStringContexts = new List<ReflectionContext>(TempDict.Keys);
            }
    
            string   PsContextsString = "";
            if(this.ProbeStringContexts.Count == 0)
            {
                PsContextsString = "<i<cg>>No reflection<i</cg>>";
            }
            else
            {
                StringBuilder PSB = new StringBuilder();
                foreach (ReflectionContext RC in this.ProbeStringContexts)
                {
                    PSB.Append(RC.ToString()); PSB.Append(", ");
                }
                PsContextsString = string.Format("<i<cr>>{0}<i</cr>>", PSB.ToString().Trim().Trim(','));
            }

            string ResDetails = string.Format("    || Code - {0} | Length - {1}", this.ProbeStringResponse.Code, this.ProbeStringResponse.BodyLength);
            this.Scnr.ResponseTrace(string.Format(" ==> Reflection contexts - {0}{1}", PsContextsString, ResDetails));

            
        }

        void CheckCharsetSecurity()
        {
            
            if (! this.BaseResponse.IsCharsetSet)
            {
                this.ReportCharsetNotSet();
            }

            string ParsedCharset = this.ProbeStringResponse.ParsedBodyEncoding;
            if (ParsedCharset.Contains(this.ProbeString))
            {
                this.ReportCharsetManipulation();
            }
            else
            {
                this.Scnr.Trace("<i<br>><i<h>>Checking for Charset Manipulation:<i</h>>");

                string[] charsets = new string[] { "UTF-8", "UTF-7" };
                List<Request> inj_req = new List<Request>();
                List<Response> inj_res = new List<Response>();
                List<string> payloads = new List<string>();
                int match_count = 0;
                foreach (string charset in charsets)
                {
                    this.Scnr.RequestTrace(string.Format("  Injected {0} - ", charset));
                    Response res = this.Scnr.Inject(charset);
                    inj_req.Add(this.Scnr.InjectedRequest);
                    inj_res.Add(res);
                    payloads.Add(charset);
                    if (res.BodyEncoding == charset)
                    {
                        match_count = match_count + 1;
                        this.Scnr.ResponseTrace(string.Format("<i<b>>Response Charset matches injected value - {0}<i</b>>", charset));
                    }
                    else
                    {
                        this.Scnr.ResponseTrace(string.Format("Response Charset is {0} and does not match the injected value", res.BodyEncoding));
                    }
                }
                if (match_count == 2)
                {
                    this.Scnr.Trace("<i<cr>>It is possible to manipulate the response Charset!!<i</cr>>");
                    this.ReportCharsetManipulation(inj_req, inj_res, payloads);
                }
                else
                {
                    this.Scnr.Trace("Charset manipulation was not successful");
                }
            }
        }

        void CheckForCrossSiteCookieSetting()
        {
            List<string> MetaSetCookies = this.ProbeStringResponse.Html.GetMetaContent("http-equiv", "set-cookie");
            List<string> HeaderSetCookies = new List<string>();
            
            if(this.ProbeStringResponse.SetCookies.Count > 0)
            {
                HeaderSetCookies = this.ProbeStringResponse.Headers.GetAll("Set-Cookie");
            }
    
            bool MetaCsc = false;
            bool HeaderCsc = false;

            foreach (string SetCookieValue in MetaSetCookies)
            {
                if (SetCookieValue.ToLower().Contains(this.ProbeString))
                {
                    MetaCsc = true;
                    this.Scnr.Trace(string.Format("<i<br>><i<cr>>Injected ProbeString '{0}' is reflected inside Set-Cookie HTTP-EQUIV Meta Tag. Allows Cross-site Cookie Setting!<i</cr>>", this.ProbeString));
                    break;
                }
            }

            foreach (string SetCookieValue in HeaderSetCookies)
            {
                if (SetCookieValue.ToLower().Contains(this.ProbeString))
                {                    
                    HeaderCsc = true;
                    this.Scnr.Trace(string.Format("<i<br>><i<cr>>Injected ProbeString '{0}' is reflected inside Set-Cookie Header. Allows Cross-site Cookie Setting!<i</cr>>", this.ProbeString));
                    break;
                }
            }

            if (MetaCsc || HeaderCsc)
            {
                this.ReportCrossSiteCookieSetting(MetaCsc, HeaderCsc);
            }
        }

        void CheckForInjectionInHtml()
        {
            List<string> contexts = new List<string>(){"HTML"};

            if (this.ProbeStringContexts.Count == 0 
            || this.ProbeStringContexts.Contains(ReflectionContext.Unknown) 
            || this.ProbeStringContexts.Contains(ReflectionContext.AttributeName) 
            || this.ProbeStringContexts.Contains(ReflectionContext.AttributeValueWithSingleQuote) 
            || this.ProbeStringContexts.Contains(ReflectionContext.AttributeValueWithDoubleQuote))
            {
                contexts.Add("HTML Escape");
            }

            if (this.ProbeStringContexts.Contains(ReflectionContext.Textarea))
            {
                contexts.Add("TEXTAREA tag");
            }

            if (this.ProbeStringContexts.Contains(ReflectionContext.InLineJS))
            {
                contexts.Add("SCRIPT tag");
            }

            if (this.ProbeStringContexts.Contains(ReflectionContext.InLineCSS))
            {
                contexts.Add("STYLE tag");
            }

            if (this.ProbeStringContexts.Contains(ReflectionContext.HtmlComment))
            {
                contexts.Add("HTML Comment");
            }
    
            foreach (string context in contexts)
            {
                List<string> prefixes = new List<string>();
                List<string> suffixes = new List<string>();

                string attr_name = "";
                string attr_value = "";
                string trace_header = "";
                string trace_success = "";
                string trace_fail = "";
      
                switch (context)
                {
                    case ("HTML"):
                        prefixes = new List<string>(){""};
                        suffixes = new List<string>(){""};
                        attr_name = "xhx";
                        attr_value = "yhy";
                        trace_header = "Checking HTML Injection in HTML Context";
                        trace_success = "Got HTML injection in HTML Context";
                        trace_fail = "Unable to inject HTML in HTML Context";
                        break;

                    case ("HTML Escape"):
                        prefixes = new List<string>(){"a\">", "a'>", "a>", "a\">", "a'>", "a>"};
                        suffixes = new List<string>(){"<b\"", "<b'", "<b", "", "", ""};
                        attr_name = "xex";
                        attr_value = "yey";
                        trace_header = "Checking HTML Injection by escaping in to HTML Context";
                        trace_success = "Got HTML injection by escaping in to HTML Context";
                        trace_fail = "Unable to inject HTML by escaping in to HTML Context";
                        break;

                    case ("TEXTAREA tag"):
                        prefixes = new List<string>(){"</textarea>"};
                        suffixes = new List<string>(){""};
                        attr_name = "xtx";
                        attr_value = "yty";
                        trace_header = "Checking HTML Injection by escaping Textarea tag Context";
                        trace_success = "Got HTML injection by escaping Textarea tag Context";
                        trace_fail = "Unable to inject HTML by escaping Textarea tag Context";
                        break;

                    case ("SCRIPT tag"):
                        prefixes = new List<string>(){"</script>", "--></script>"};
                        suffixes = new List<string>(){"", ""};
                        attr_name = "xjx";
                        attr_value = "yjy";
                        trace_header = "Checking HTML Injection by escaping Script tag Context";
                        trace_success = "Got HTML injection by escaping Script tag Context";
                        trace_fail = "Unable to inject HTML by escaping Script tag Context";
                        break;

                    case ("STYLE tag"):
                        prefixes = new List<string>(){"</style>", "--></style>"};
                        suffixes = new List<string>(){"", ""};
                        attr_name = "xsx";
                        attr_value = "ysy";
                        trace_header = "Checking HTML Injection by escaping Style tag Context";
                        trace_success = "Got HTML injection by escaping Style tag Context";
                        trace_fail = "Unable to inject HTML by escaping Style tag Context";
                        break;

                    case ("HTML Comment"):
                        prefixes = new List<string>(){"-->"};
                        suffixes = new List<string>(){""};
                        attr_name = "xcx";
                        attr_value = "ycy";
                        trace_header = "Checking HTML Injection by escaping HTML Comment Context";
                        trace_success = "Got HTML injection by escaping HTML Comment Context";
                        trace_fail = "Unable to inject HTML by escaping HTML Comment Context";
                        break;
                }
      
                this.Scnr.Trace(string.Format("<i<br>><i<h>>{0}:<i</h>>", trace_header));
                for (int i=0; i < prefixes.Count; i++)
                {
                    string payload = string.Format("{0}<h {1}={2}>{3}", prefixes[i], attr_name, attr_value, suffixes[i]);
        
                    this.Scnr.RequestTrace(string.Format("  Injected {0} - ", payload));
                    Response res = this.Scnr.Inject(payload);
        
                    string res_details = string.Format("    || Code - {0} | Length - {1}", res.Code, res.BodyLength);
                    this.CheckResponseDetails(res);

                    if ( res.Html.Get("h", attr_name, attr_value).Count > 0)
                    {
                        this.AddToTriggers(payload, string.Format("The payload in this request tries to inject an HTML tag named 'h' with attribute name '{0}' and attribute value '{1}'. The payload is {2}", attr_name, attr_value, payload), payload, string.Format("This response contains an HTML tag named 'h' with attribute name '{0}' and attribute value '{1}'. This was inserted by the payload.", attr_name, attr_value));
                        this.SetConfidence(3);
                        this.Scnr.ResponseTrace(string.Format("<i<cr>>{0}<i</cr>>{1}", trace_success, res_details));
                    }
                    else
                    {
                        this.Scnr.ResponseTrace(string.Format("{0}{1}", trace_fail, res_details));
                    }
                }
            }
        }
        

        void CheckForInjectionInJSInsideHTML()
        {
            this.CheckForInjectionInJS(true);
        }
    
        void CheckForInjectionInFullJS()
        {
            this.CheckForInjectionInJS(false);
        }

        void CheckForInjectionInJS(bool InLine)
        {
  
            List<string> script_contexts = new List<string>();
            List<string> contaminated_scripts = new List<string>();
    
            if(InLine)
            {
                contaminated_scripts = this.ProbeStringResponse.Html.GetJavaScript(this.ProbeString);
            }
            else
            {
                contaminated_scripts.Add(this.ProbeStringResponse.BodyString);
            }

            List<string> payload_prefixes = new List<string>();

            foreach(string script in contaminated_scripts)
            {
                payload_prefixes.Add(this.GetJSPayload(script));
                this.CheckSinkAssignment(script);
                script_contexts.AddRange(this.GetJSContexts(script));
            }
   
            //make the list unique
            Dictionary<string, int> TempDict = new Dictionary<string, int>();
            foreach (string RC in script_contexts)
            {
                TempDict[RC] = 0;
            }

            script_contexts = new List<string>(TempDict.Keys);
    
            if (script_contexts.Contains("NormalString"))
            {
                this.AddToTriggersWithProbeStringInjection(this.ProbeString, "The payload in this request is random string just to check where this value it is reflected back in the response.", this.ProbeString, "The random string from the payload is found in the JavaScript code section of this response.");
                this.SetConfidence(1);
                this.Scnr.Trace("<i<cr>>Probe string is reflected inside JavaScript Script outside Quotes. Possibly vulnerable!<i</cr>>");
            }
    
            if(script_contexts.Count > 0)
            {
                this.Scnr.Trace(string.Format("<i<br>><i<b>>Got injection inside JavaScript as - {0}<i</b>>", string.Join(",", script_contexts.ToArray())));
            }

            this.Scnr.Trace("<i<br>><i<h>>Checking for Injection in JS Context:<i</h>>");
            string keyword = "dzkqivxy";
            bool js_inj_success = false;

            foreach(string payload_prefix in payload_prefixes)
            {
                List<string> binders = new List<string>(){";", "\n", "\r"};
                List<string> paddings = new List<string>(){";/*", ";//", "/*", "//"};
                bool payload_inj_success = false;
                foreach(string binder in binders)
                {
                    foreach(string padding in paddings)
                    {
                        if (payload_inj_success)
                        {
                            break;
                        }
                        string payload = string.Format("{0}{1}{2}{3}", payload_prefix, binder, keyword, padding);
                        this.Scnr.RequestTrace(string.Format("  Injected {0} - ", payload));
                        Response res = this.Scnr.Inject(payload);
                        if (this.IsExpressionStatement(res, keyword))
                        {
                            this.Scnr.ResponseTrace(string.Format("<i<cr>>Injected {0} as a JavaScript statement.<i</cr>>", keyword));
                            this.AddToTriggers(payload, string.Format("The payload in this request tries to insert the string '{0}' as a JavaScript statement. The payload is {1}", keyword, payload), keyword, string.Format("The string '{0}' is found as an statement in the JavaScript code section of this response. This was inserted by the payload.", keyword));
                            this.SetConfidence(3);
                            payload_inj_success = true;
                            js_inj_success = true;
                        }
                        else
                        {
                            this.Scnr.ResponseTrace(string.Format("Could not get {0} as JavaScript statement", keyword));
                        }
                    }
                }
            }
        
            if(! js_inj_success)
            {
                payload_prefixes = new List<string>();
                if (script_contexts.Contains("SingleQuotedString"))
                {
                    payload_prefixes.Add("'");
                }
                if (script_contexts.Contains("DoubleQuotedString"))
                {
                    payload_prefixes.Add("\"");
                }
                if (script_contexts.Contains("SingleLineComment"))
                {
                    payload_prefixes.Add("\r");
                    payload_prefixes.Add("\n");
                }
                if (script_contexts.Contains("MutliLineComment"))
                {
                    payload_prefixes.Add("*/");
                }
                keyword = "dzpyqmw";
                foreach(string pp in payload_prefixes)
                {
                    string payload = string.Format("{0}{1}", pp, keyword);
                    this.Scnr.RequestTrace(string.Format("  Injected {0} - ", payload));
                    Response res = this.Scnr.Inject(payload);
                    if (this.IsNormalString(res, keyword))
                    {
                        this.Scnr.ResponseTrace(string.Format("<i<cr>>Injected {0} as a JavaScript statement.<i</cr>>", keyword));
                        this.AddToTriggers(payload, string.Format("The payload in this request tries to insert the string '{0}' as a JavaScript statement. The payload is {1}", keyword, payload), keyword, string.Format("The string '{0}' is found as an statement in the JavaScript code section of this response. This was inserted by the payload.", keyword));
                        this.SetConfidence(2);
                        js_inj_success = true;
                        break;
                    }
                    else
                    {
                        this.Scnr.ResponseTrace(string.Format("Could not get {0} as JavaScript statement", keyword));
                    }
                }
            }

            if (!js_inj_success)
            {
                if (script_contexts.Contains("NormalString"))
                {
                    js_inj_success = true;
                }
            }

            if (!js_inj_success)
            {
                this.ReportJSTestLead();
            }
        }

        string GetJSPayload(string script)
        {
            List<string> context_finishers = new List<string>(){"", ")", "]", "}", "))", ")]", ")}", "])", "]]", "]}", "})", "}]", "}}",
            ")))", "))]", "))}", ")])", ")]]", ")]}", ")})", ")}]", ")}}", "]))", "])]", "])}", "]])", "]]]", "]]}", "]})", "]}]", "]}}",
            "}))", "})]", "})}", "}])", "}]]", "}]}", "}})", "}}]", "}}}"};

            List<string> quotes = new List<string>(){"", "'", "\""};
            string padding = ";/*";
            string keyword = "dzkqivxy";
            foreach(string cf in context_finishers)
            {
                foreach(string q in quotes)
                {
                    string payload_prefix = string.Format("{0}{1}", q, cf);
                    string payload = string.Format("{0};{1}{2}", payload_prefix, keyword, padding);
                    string script_updated = script.Replace(this.ProbeString, payload);
                    try
                    {
                        if (IronJint.IsExpressionStatement(script_updated, keyword))
                        {
                            return payload_prefix;
                        }
                    }
                    catch{}
                }
            }
            return "";
        }
  
        void CheckSinkAssignment(string script)
        {
            try
            {
                TraceResult ij = IronJint.Trace(script, this.ProbeString);
                if( ij.SourceToSinkLines.Count > 0)
                {
                    this.Scnr.Trace("<i<br>><i<cr>><i<b>>Injected ProbeString was assigned to a DOM XSS Sink<i</b>><i</cr>>");
                    List<string> js_triggers = new List<string>();
                    foreach(string line in ij.SourceToSinkLines)
                    {
                        js_triggers.Add(line);
                    }
                    this.AddToTriggersWithProbeStringInjection(this.ProbeString, "The payload in this request is random string just to check where this value it is reflected back in the response.", string.Join("\r\n", js_triggers.ToArray()), "The random string from the payload has been found in DOM XSS sinks inside the JavaScript of this response.");
                    this.SetConfidence(3);
                }
            }catch{}
        }
  
        List<string> GetJSContexts(string script)
        {
            List<string> script_contexts = new List<string>();
            try
            {
                script_contexts.AddRange(CodeContext.GetJavaScriptContext(script, this.ProbeString));
            }catch{}
            return script_contexts;
        }
  
        bool IsExpressionStatement(Response res, string keyword)
        {
            List<string> scripts = new List<string>();
            if( res.IsJavaScript)
            {
                if (res.BodyString.Contains(keyword))
                {
                    scripts.Add(res.BodyString);
                }
            }
            else if( res.IsHtml)
            {
                scripts = res.Html.GetJavaScript(keyword);
            }
    
            foreach(string script in scripts)
            {
                try
                {
                    if (IronJint.IsExpressionStatement(script, keyword))
                    {
                        return true;
                    }
                }catch{}
            }
            return false;
        }
  
        bool IsNormalString(Response res, string keyword)
        {
            List<string> scripts = new List<string>();
            if (res.IsJavaScript)
            {
                if (res.BodyString.Contains(keyword))
                {
                    scripts.Add(res.BodyString);
                }
                else if( res.IsHtml)
                {
                    scripts = res.Html.GetJavaScript(keyword);
                }    
            }
            foreach(string script in scripts)
            {
                try
                {
                    List<string> script_contexts = new List<string>();
                    script_contexts.AddRange(CodeContext.GetJavaScriptContext(script, keyword));
                    if (script_contexts.Contains("NormalString"))
                    {
                        return true;
                    }
                }catch{}
            }
            return false;
        }


        void CheckForInjectionInVBInsideHTML()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Script Injection inside VB Script Tag:<i</h>>");
            List<string> script_contexts = new List<string>();
            List<string> contaminated_scripts = this.ProbeStringResponse.Html.GetVisualBasic(this.ProbeString);
            foreach(string script in contaminated_scripts)
            {
                script_contexts.AddRange(this.GetVBContexts(script));
            }
    
            //make the list unique
            Dictionary<string, int> TempDict = new Dictionary<string, int>();
            foreach (string RC in script_contexts)
            {
                TempDict[RC] = 0;
            }
            script_contexts = new List<string>(TempDict.Keys);

            if (script_contexts.Contains("NormalString"))
            {
                this.AddToTriggersWithProbeStringInjection(this.ProbeString, "The payload in this request is random string just to check where this value it is reflected back in the response.", this.ProbeString, "The random string from the payload has been found in the VB Script code section of this response.");
                this.SetConfidence(1);
                this.Scnr.Trace("<i<cr>>Probe string is reflected inside VB Script outside Quotes. Possibly vulnerable!<i</cr>>");
            }
    
            if(script_contexts.Count > 0)
            {
                this.Scnr.Trace(string.Format("<i<br>><i<b>>Got injection inside VB Script as - {0}<i</b>>", string.Join(",", script_contexts.ToArray())));
            }
    
            List<string> payload_prefixes = new List<string>();
            if (script_contexts.Contains("DoubleQuotedString"))
            {
                payload_prefixes.Add("\"");
            }
            if (script_contexts.Contains("SingleLineComment"))
            {
                payload_prefixes.Add("\n");
            }
            string keyword = "dzpxqmw";
        
            bool vb_inj_success = false;
            foreach(string pp in payload_prefixes)
            {
                string payload = string.Format("{0}{1}", pp, keyword);
                this.Scnr.RequestTrace(string.Format("  Injected {0} - ", payload));
                Response res = this.Scnr.Inject(payload);
                if (this.IsNormalVBString(res, keyword))
                {
                    this.Scnr.ResponseTrace(string.Format("<i<cr>>Injected {0} as a VB statement.<i</cr>>", keyword));
                    this.AddToTriggers(payload, string.Format("The payload in this request tries to insert the string '{0}' as a VB Script statement. The payload is {1}", keyword, payload), keyword, string.Format("The string '{0}' is found as an statement in the VB Script code section of this response. This was inserted by the payload.", keyword));
                    this.SetConfidence(2);
                    vb_inj_success = true;
                    break;
                }
                else
                {
                    this.Scnr.ResponseTrace(string.Format("Could not get {0} as JavaScript statement", keyword));
                }
            }
        }
  
        List<string> GetVBContexts(string script)
        {
            List<string> script_contexts = new List<string>();
            try
            {
                script_contexts.AddRange(CodeContext.GetVisualBasicContext(script, this.ProbeString));
            }
            catch{}
            return script_contexts;
        }
  
        bool IsNormalVBString(Response res, string keyword)
        {
            List<string> scripts = new List<string>();
            if( res.IsHtml)
            {
                scripts = res.Html.GetVisualBasic(keyword);
            }
            foreach(string script in scripts)
            {
                try
                {
                    List<string> script_contexts = new List<string>();
                    script_contexts.AddRange(CodeContext.GetVisualBasicContext(script, keyword));
                    if( script_contexts.Contains("NormalString"))
                    {
                        return true;
                    }
                }catch{}
            }
            return false;
        }
  
        void CheckForInjectionInUrlAttribute()
        {
            //Start the test
            string keyword = "yhstdjbz";
            string payload = string.Format("javascript:{0}", keyword);

            this.Scnr.Trace("<i<br>><i<h>>Checking JS Injection in UrlAttribute Context:<i</h>>");
            this.Scnr.RequestTrace(string.Format("  Injected {0} - ", payload));
        
            Response ua_res = this.Scnr.Inject(payload);
    
            string res_details = string.Format("    || Code - {0} | Length - {1}", ua_res.Code, ua_res.BodyLength);
            this.CheckResponseDetails(ua_res);
    
            if (ua_res.BodyString.Contains(payload) || (ua_res.Headers.Has("Refresh") && ua_res.Headers.Get("Refresh").Contains(payload)))
            {
                List<ReflectionContext> ua_inj_contexts = this.GetContext(keyword, ua_res);
                if (ua_inj_contexts.Contains(ReflectionContext.JSUrl))
                {
                    this.Scnr.ResponseTrace(string.Format("<i<cr>>Got {0} in InLineJS context<i</cr>>{1}", keyword, res_details));
                    this.AddToTriggers(payload, string.Format("The payload in this request attempts to inject the string '{0}' as executable code using the javascript: url format. The payload is {1}", keyword, payload), payload, string.Format("The string '{0}' from the payload is found as a JavaScript url in this response.", keyword));
                    this.SetConfidence(3);
                }
                else
                {
                    this.Scnr.ResponseTrace(string.Format("Got {0} in non-UrlAttribute context", payload));
                }
            }
            else
            {
                //must check for the encoding here
                this.Scnr.ResponseTrace(string.Format("No reflection{0}", res_details));
            }
        }   
  
        void CheckForInjectionInAttributeName()
        {
            //Start the test
            this.Scnr.Trace("<i<br>><i<h>>Checking for Injection in HTML AttributeName Context:<i</h>>");
            this.InjectAttribute(" olpqir=\"vtkir(1)\"","olpqir","vtkir(1)");
            this.InjectAttribute(" olpqir='vtkir(1)'","olpqir","vtkir(1)");
  
        }
  
        void CheckForInjectionInSpecialAttributes()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Injection in Special HTML Attributes:<i</h>>");
    
            this.CheckForSameSiteScriptIncludeSetting();
    
            string host = this.BaseRequest.Host;
            //remove the port number from hostname
            try
            {
                if( host.IndexOf(":") > 0)
                {
                    host = host.Substring(host.IndexOf(":"));
                }
            }catch{}

            this.Scnr.Trace("<i<br>><i<b>>Checking for Reflection inside Special HTML Attributes:<i</b>>");
            List<string> initial_payloads = new List<string>(){ "fpwzyqmc", string.Format("http://{0}.fpwzyqmc", host), string.Format("https://{0}.fpwzyqmc", host), string.Format("//{0}.fpwzyqmc", host)};
            bool eligible = false;
    
            foreach(string i_p in initial_payloads)
            {
                this.Scnr.RequestTrace(string.Format("  Injected {0} ==> ", i_p));
                Response res = this.Scnr.Inject(i_p);
                if (this.IsInSpecialAttribute(i_p, res))
                {
                    eligible = true;
                    this.Scnr.ResponseTrace("  Found reflection inside Special HTML Attributes");
                    break;
                }
                else
                {
                    this.Scnr.ResponseTrace("  Not reflected inside Special HTML Attributes");
                }
            }
            if (! eligible)
            {
                this.Scnr.Trace("<i<br>>  No reflection found inside Special HTML Attributes");
                return;
            }
    
            this.Scnr.Trace("<i<br>><i<b>>Checking for Payload Injection inside Special HTML Attributes:<i</b>>");
            string sign_str = "olxizrk";
            this.injectable_special_tags = new List<string>();
            this.injectable_special_attributes = new List<string>();
            //prefixes taken from http://kotowicz.net/absolute/
            List<string> prefixes = new List<string>(){ "http://", "https://", "//", "http:\\\\", "https:\\\\", "\\\\", "/\\", "\\/", "\r//", "/ /", "http:", "https:", "http:/", "https:/", "http:////", "https:////", "://", ".:."};
            List<string> all_tags_and_attrs = new List<string>();
            foreach(string prefix in prefixes)
            {
                for(int ii=0; ii < 2; ii++)
                {
                    string payload = "";
                    if (ii == 0)
                    {
                        payload = string.Format("{0}{1}", prefix, sign_str);
                    }
                    else
                    {
                        payload = string.Format("{0}{1}.{2}", prefix, host, sign_str);
                    }
                    this.Scnr.RequestTrace(string.Format("  Injected {0} ==> ", payload));
                    Response res = this.Scnr.Inject(payload);
                    if (this.IsInSpecialAttribute(payload, res))
                    {
                        all_tags_and_attrs = new List<string>();
                        for(int i=0; i < this.injectable_special_tags.Count; i++)
                        {
                            all_tags_and_attrs.Add(string.Format("  {0}) <i<b>>{1}<i</b>> attribute of <i<b>>{2}<i</b>> tag", i + 1, this.injectable_special_tags[i], this.injectable_special_attributes[i]));
                        }
                    
                        this.Scnr.ResponseTrace(string.Format("<i<cr>>Got {0} inside the following Special HTML Attributes:<i</cr>><i<br>>{1}", payload, string.Format("<i<br>>", all_tags_and_attrs)));
                    
                    
                        if (this.injectable_special_tags.Contains("script"))
                        {
                            this.AddToTriggers(payload, string.Format("The payload in this request is an absolute url pointing to an external domain. The payload is {0}", payload), payload, "The absolute url from the payload is found in the src attribute of SCRIPT tag in this response.");
                            this.SetConfidence(3);
                            this.Scnr.Trace("<i<br>><i<cr>>Able to set the source attribute of the Script tag to remote URL and include rogue JavaScript<i</cr>>");
                        }
                        else if (this.injectable_special_tags.Contains("object"))
                        {
                            this.AddToTriggers(payload, string.Format("The payload in this request is an absolute url pointing to an external domain. The payload is {0}", payload), payload, "The absolute url from the payload is found in the data attribute of OBJECT tag in this response.");
                            this.SetConfidence(3);
                            this.Scnr.Trace("<i<br>><i<cr>>Able to set the data attribute of the Object tag to remote URL and include rogue active components like SWF files<i</cr>>");
                        }
                        else if (this.injectable_special_tags.Contains("embed"))
                        {
                            this.AddToTriggers(payload, string.Format("The payload in this request is an absolute url pointing to an external domain. The payload is {0}", payload), payload, "The absolute url from the payload is found in the href attribute of EMBED tag in this response.");
                            this.SetConfidence(3);
                            this.Scnr.Trace("<i<br>><i<cr>>Able to set the href attribute of the Embed tag to remote URL and include rogue active components like SWF files<i</cr>>");
                        }
                        else if (this.injectable_special_tags.Contains("link"))
                        {
                            this.AddToTriggers(payload, string.Format("The payload in this request is an absolute url pointing to an external domain. The payload is {0}", payload), payload, "The absolute url from the payload is found in the href attribute of LINK tag in this response.");
                            this.SetConfidence(3);
                            this.Scnr.Trace("<i<br>><i<cr>>Able to set the href attribute of the Link tag to remote URL and include rogue CSS that can contain JavaScript<i</cr>>");
                        }
                        else
                        {
                            this.ReportInjectionInSpecialAttributes(payload);
                        }
                        return;
                    }
                    else
                    {
                            string res_details = string.Format("    || Code - {0} | Length - {1}", res.Code, res.BodyLength);
                            this.Scnr.ResponseTrace(string.Format("Did not get payload inside the Special HTML Attributes{0}", res_details));
                    }
                }
            }
        }
        
        void CheckForSameSiteScriptIncludeSetting()
        {
            List<string> scripts = new List<string>();
            List<string> styles = new List<string>();
            List<string> scripts_vuln = new List<string>();
            List<string> styles_vuln = new List<string>();
    
            if (this.ProbeStringResponse.IsHtml)
            {
                scripts = this.ProbeStringResponse.Html.GetValues("script", "src");
                styles = this.ProbeStringResponse.Html.GetValues("link", "href");
            }
            foreach(string script in scripts)
            {
                if (this.IsInUrlPath(script, this.ProbeString))
                {
                    scripts_vuln.Add(script);
                }
            }
            foreach(string style in styles)
            {
                if (this.IsInUrlPath(style, this.ProbeString))
                {
                    styles_vuln.Add(style);
                }
            }
            if (scripts_vuln.Count + styles_vuln.Count > 0)
            {
                this.Scnr.Trace("<i<br>><i<cr>>Able to influence the location of the in-domain JS/CSS inlcuded in the page.<i</cr>>");
                this.ReportSameSiteScriptInclude(scripts_vuln, styles_vuln);
            }  
        }

        bool IsInUrlPath(string url, string keyword)
        {
            try
            {
                string full_url = "";
                if (url.StartsWith("http://") || url.StartsWith("https://"))
                {
                    full_url = url;
                }
                else
                {
                    full_url = string.Format("http://a/{0}", url);
                }

                Request r = new Request(full_url);
                if (r.UrlPath.Contains(keyword))
                {
                    return true;
                }
            }catch{}
            return false;
        }
  
        bool IsInSpecialAttribute(string keyword, Response res)
        {
            List<string> special_tags = new List<string>(){ "iframe", "frame", "script", "link", "object", "embed", "form", "button", "base", "a"};
            List<string> special_attributes = new List<string>(){ "src", "src", "src", "href", "data", "src", "action", "formaction", "href", "href"};
    
            this.injectable_special_tags = new List<string>();
            this.injectable_special_attributes = new List<string>();
    
            for(int i=0; i < special_tags.Count; i++)
            {
                string tag_name = special_tags[i];
                string tag_attr = special_attributes[i];
                List<string> values = res.Html.GetValues(tag_name, tag_attr);
                foreach(string value in values)
                {
                    if (value.StartsWith(keyword))
                    {
                        this.injectable_special_tags.Add(tag_name);
                        this.injectable_special_attributes.Add(tag_attr);
                        break;
                    }
                }
            }
            if (this.injectable_special_tags.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
  
        void CheckForInjectionInSingleQuoteAttributeValue()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Injection in HTML AttributeValue Context:<i</h>>");
            this.InjectAttribute(" \' olqpir=\"vtikr(1)\"","olqpir","vtikr(1)");
            this.InjectAttribute(" \' olqpir=\'vtikr(1)\'","olqpir","vtikr(1)");
        }
  
        void CheckForInjectionInDoubleQuoteAttributeValue()
        {
            this.Scnr.Trace("<i<br>><i<h>>Checking for Injection in HTML AttributeValue Context:<i</h>>");
            this.InjectAttribute(" \" olqpir=\"vtikr(1)\"","olqpir","vtikr(1)");
            this.InjectAttribute(" \" olqpir=\'vtikr(1)\'","olqpir","vtikr(1)");
            //HtmlAgilityPack considers quote-less as Double-Quote;
            this.InjectAttribute(" olqpir=\"vtikr(1)\"","olqpir","vtikr(1)");
            this.InjectAttribute(" olqpir=\'vtikr(1)\'","olqpir","vtikr(1)");
            this.InjectAttribute("aa olqpir=\"vtikr(1)\"","olqpir","vtikr(1)");
            this.InjectAttribute("aa olqpir=\'vtikr(1)\'","olqpir","vtikr(1)");
        }
  
        void InjectAttribute(string Payload, string AttrName, string AttrValue)
        {
            //Start the test
            this.Scnr.RequestTrace(string.Format("  Injected {0} - ", Payload));
    
            Response at_res = this.Scnr.Inject(Payload);
            string res_details = string.Format("    || Code - {0} | Length - {1}", at_res.Code, at_res.BodyLength);
            this.CheckResponseDetails(at_res);
    
            List<ReflectionContext> name_contexts = this.GetContext(AttrName, at_res);
            List<ReflectionContext> value_contexts = this.GetContext(AttrValue, at_res);
    
            if (name_contexts.Contains(ReflectionContext.AttributeName) && (value_contexts.Contains(ReflectionContext.AttributeValueWithSingleQuote) || value_contexts.Contains(ReflectionContext.AttributeValueWithDoubleQuote)))
            {
                this.Scnr.ResponseTrace(string.Format("<i<cr>>Got {0} as AttributeName and {1} as AttributeValue<i</cr>>{2}", AttrName, AttrValue, res_details));
                this.AddToTriggers(Payload, string.Format("The payload in this request tries to inject an attribute with name '{0}' and value '{1}' inside an HTML tag. The payload is {2}", AttrName, AttrValue, Payload), Payload, string.Format("This response contains an attribute with name '{0}' and value '{1}' inside an HTML tag. This was inserted by the payload.", AttrName, AttrValue));
                this.SetConfidence(3);
            }
            else if(at_res.BodyString.Contains(Payload))
            {
                this.Scnr.ResponseTrace(string.Format("Got {0} outside of AttributeName and AttributeValue context{1}", Payload, res_details));
            }
            else
            {
                this.Scnr.ResponseTrace(string.Format("No useful reflection{0}", res_details));
            }
  
        }
  
        void CheckForInjectionInCSSInsideStyleAttribute()
        {
            this.CheckForInjectionInCSS(true);
        }
    
        void CheckForInjectionInFullCSS()
        {
            this.CheckForInjectionInCSS(false);
        }
      
        void CheckForInjectionInCSS(bool InStyleAttribute)
        {
            List<string> css_contexts = this.GetCssContexts(this.ProbeString, this.ProbeStringResponse);
            foreach(string context in css_contexts)
            {
                this.CheckForInjectionInCSSContext(context, InStyleAttribute);
            }
        }
  
        List<string> GetCssContexts(string keyword, Response res)
        {
            List<string> css_contexts = new List<string>();
            List<string> contaminated_css = new List<string>();
            if (res.IsHtml)
            {
                contaminated_css = res.Html.GetCss(keyword, true);
            }
            else if (res.IsCss)
            {
                contaminated_css.Add(res.BodyString);
            }
            foreach(string css in contaminated_css)
            {
                try
                {
                    css_contexts.AddRange(IronCss.GetContext(css, keyword));
                }catch{}
            }

            //make the list unique
            Dictionary<string, int> TempDict = new Dictionary<string, int>();
            foreach (string RC in css_contexts)
            {
                TempDict[RC] = 0;
            }
            css_contexts = new List<string>(TempDict.Keys);

            return css_contexts;
        }
  
        void CheckForInjectionInCSSContext(string css_context, bool InStyleAttribute)
        {
            string payload = "";
            List<string> url_special_payloads = new List<string>();
            List<string> jsurl_special_payloads = new List<string>();
            List<string> js_special_payloads = new List<string>();
            string quote = "";
    
            string[] context_parts = css_context.Split('-');
    
            //#
            //#CSS Value contexts
            //#
            switch(context_parts[0])
            {
                case("Value"):
                    quote = context_parts[3];
                    switch(context_parts[1])
                    {
                        case("Normal"):
                        case("OnlyNormal"):
                            payload = "aa<quote>;} <vector> aa {aa:<quote>aa";
                            jsurl_special_payloads.Add("aa<quote>; background-image: url(<url>); aa:<quote>aa");
                            js_special_payloads.Add("aa<quote>; aa: expression('<js>'); aa:<quote>aa");
                            js_special_payloads.Add("aa<quote>; aa: expression(\"<js>\"); aa:<quote>aa");
                            if (context_parts[1] == "OnlyNormal")
                            {
                                if (context_parts[2] == "Full")
                                {
                                    js_special_payloads.Add("expression('<js>')");
                                }
                                else if (context_parts[2] == "Start")
                                {
                                    js_special_payloads.Add("expression('<js>'); aa:");
                                }
                            }
                            break;
                        case("JS"):
                            //report as xss
                            break;
                        case ("Url"):
                            payload = "aa<quote>);} <vector> aa {aa:<quote>url(aa";
                            jsurl_special_payloads.Add("aa<quote>); background-image: url(<url>); aa:url(<quote>aa");
                            js_special_payloads.Add("aa<quote>); aa: expression('<js>'); aa:url(<quote>aa");
                            js_special_payloads.Add("aa<quote>); aa: expression(\"<js>\"); aa:url(<quote>aa");
                            if (context_parts[2] == "Start" || context_parts[2] == "Full")
                            {
                                jsurl_special_payloads.Add("<url>");
                            }
                            break;
                    }
                    break;

                //#
                //#CSS Propery contexts
                //#
                case("Property"):
                    payload = "aa:aa} <vector> aa {aa";
                    if (context_parts[1] == "Start" || context_parts[1] == "Full")
                    {
                        jsurl_special_payloads.Add("background-image:url(<url>); aa");
                        js_special_payloads.Add("aa:expression('<js>'); aa");
                        js_special_payloads.Add("aa:expression(\"<js>\"); aa");
                    }
                    break;
                //#
                //#CSS Ident contexts
                //#
                case("Ident"):
                    switch(context_parts[1])
                    {
                        case("Ident"):
                            payload = "aa {x:x} <vector> @aa";
                            if (context_parts[2] == "Start" || context_parts[2] == "Full")
                            {
                            url_special_payloads.Add("import '<url>'; @a");
                            url_special_payloads.Add("import \"<url>\"; @a");
                            jsurl_special_payloads.Add("import '<url>'; @a");
                            jsurl_special_payloads.Add("import \"<url>\"; @a");
                            }
                            break;
                        case("MediaValue"):
                            payload = "aa {x {x:x}} <vector> @media aa";
                            break;
                    }
                break;
                //#
                //#CSS Import contexts
                //#
                case("Import"):
                    quote = context_parts[3];
                    switch(context_parts[1])
                    {
                        case("Raw"):
                            payload = "aa<quote>; <vector> @import <quote>aa";
                            if (context_parts[2] == "Start" || context_parts[2] == "Full")
                            {
                            url_special_payloads.Add("<url>");
                            jsurl_special_payloads.Add("<url>");
                            //#report as xss
                            }
                            break;
                        case("Url"):
                            payload = "aa<quote>); <vector> @import url(<quote>aa";
                            if (context_parts[2] == "Start" || context_parts[2] == "Full")
                            {
                            url_special_payloads.Add("<url>");
                            jsurl_special_payloads.Add("<url>");
                            //#report as xss
                            }
                            break;
                        case("RawJS"):
                            //#report as xss
                            break;
                        case("UrlJS"):
                            //#report as xss
                            break;
                    }
                    break;
                //#
                //#CSS Selector contexts
                //#
                case("Selector"):
                    if (context_parts[1] == "Normal")
                    {
                        if (context_parts[2] == "Start" || context_parts[2] == "Full")
                        {
                            payload = "<vector> aa";
                        }
                        else
                        {
                            payload = "aa {aa:aa} <vector> aa";
                        }
                    }
                    else if (context_parts[1] == "Round")
                    {
                        payload = "aa) {aa:aa} <vector> aa(aa";
                    }
                    else if (context_parts[1] == "SquareKey")
                    {
                        payload = "aa=aa] {aa:aa} <vector> aa[aa";
                    }
                    else if (context_parts[1] == "SquareValue")
                    {
                        payload = "aa<quote>] {aa:aa} <vector> aa[aa=<quote>aa";
                    }
                    break;
                //#
                //#CSS Comment contexts
                //#
                case("Comment"):
                    payload = "*/ <vector> /*";
                    break;
            }
    
            payload = this.InsertCssQuotes(quote, payload);
    
            List<string> url_vectors = new List<string>(){"@import '//iczpbtsq';", "@import \"//iczpbtsq\";", "@import url(//iczpbtsq);"};
            List<string> js_vectors = new List<string>(){"@import 'javascript:\"iczpbtsq\"';", "@import \"javascript:'iczpbtsq'\";"
            , "@import url(javascript:'iczpbtsq');", "@import url(javascript:\"iczpbtsq\");", "x {x:expression('iczpbtsq')}",
            "x {x:expression(\"iczpbtsq\")}", "x {background-image:url(javascript:'iczpbtsq')}", "x {background-image:url(javascript:\"iczpbtsq\")}"};
    
            //make the list unique
            Dictionary<string, int> TempDict = new Dictionary<string, int>();
            foreach (string RC in url_special_payloads)
            {
                TempDict[RC] = 0;
            }
            url_special_payloads = new List<string>(TempDict.Keys);


            TempDict.Clear();
            foreach (string RC in jsurl_special_payloads)
            {
                TempDict[RC] = 0;
            }
            jsurl_special_payloads = new List<string>(TempDict.Keys);

            TempDict.Clear();
            foreach (string RC in js_special_payloads)
            {
                TempDict[RC] = 0;
            }
            js_special_payloads = new List<string>(TempDict.Keys);
      
      
      
            foreach(string spl_payload in jsurl_special_payloads)
            {
                string current_payload = spl_payload.Replace("<url>", "javascript:'iczpbtsq'");
                current_payload = this.InsertCssQuotes(quote, current_payload);
      
                if (this.IsCssPayloadAllowed(InStyleAttribute, current_payload))
                {
                    this.InjectAndCheckCss(current_payload, "iczpbtsq", "js");
                }
                current_payload = spl_payload.Replace("<url>", "javascript:\"iczpbtsq\"");
                current_payload = this.InsertCssQuotes(quote, current_payload);

                if (this.IsCssPayloadAllowed(InStyleAttribute, current_payload))
                {
                    this.InjectAndCheckCss(current_payload, "iczpbtsq", "js");
                }
            }
    
            foreach(string spl_payload in js_special_payloads)
            {
                string current_payload = spl_payload.Replace("<js>", "iczpbtsq");
                current_payload = this.InsertCssQuotes(quote, current_payload);
                if (this.IsCssPayloadAllowed(InStyleAttribute, current_payload))
                {
                    this.InjectAndCheckCss(current_payload, "iczpbtsq", "js");
                }
            }
    
            foreach(string spl_payload in url_special_payloads)
            {
                string current_payload = spl_payload.Replace("<url>", "//iczpbtsq");
                current_payload = this.InsertCssQuotes(quote, current_payload);
                if (this.IsCssPayloadAllowed(InStyleAttribute, current_payload))
                {
                    this.InjectAndCheckCss(current_payload, "//iczpbtsq", "url");
                }
            }
    
            foreach(string vector in url_vectors)
            {
                string current_payload = payload.Replace("<vector>", vector);
                current_payload = this.InsertCssQuotes(quote, current_payload);
                if (this.IsCssPayloadAllowed(InStyleAttribute, current_payload))
                {
                    this.InjectAndCheckCss(current_payload, "//iczpbtsq", "url");
                }
            }

            foreach(string vector in js_vectors)
            {
                string current_payload = payload.Replace("<vector>", vector);
                current_payload = this.InsertCssQuotes(quote, current_payload);
                if (this.IsCssPayloadAllowed(InStyleAttribute, current_payload))
                {
                    this.InjectAndCheckCss(current_payload, "iczpbtsq", "js");
                }
            }
        }
    
        bool IsCssPayloadAllowed(bool InStyleAttribute, string payload)
        {
            if (payload.Contains("{") || payload.Contains("}"))
            {
                if (InStyleAttribute)
                {
                    return false;
                }
            }
            return true;
        }
  
        void InjectAndCheckCss(string payload, string keyword, string url_or_js)
        {
            this.Scnr.RequestTrace(string.Format("Injecting {0} - ", payload));
            Response res = this.Scnr.Inject(payload);
            
            if (this.IsReqCssContext(res, keyword, url_or_js))
            {
                this.Scnr.ResponseTrace("<i<cr>>XSS inside CSS successful!<i</cr>>");
                this.AddToTriggers(payload, string.Format("The payload in this request tries to insert the string '{0}' as executable JavaScript code inside CSS. The payload is {1}", keyword, payload), keyword, string.Format("The string '{0}' from the payload is found inside the CSS section of this response and it's exact position inside the CSS can lead to it being executed as JavaScript.", keyword));
                this.SetConfidence(3);
            }
            else
            {
                this.Scnr.ResponseTrace("Not in interesting CSS context");
            }
        }
  
        bool IsReqCssContext(Response res, string keyword, string url_or_js)
        {
            List<string> contexts = this.GetCssContexts(keyword, res);
            foreach(string context in contexts)
            {
                string[] context_parts = context.Split('-');
    
                if (context_parts[0] == "Value")
                {
                    if (context_parts[1] == "JS")
                    {
                        if (url_or_js == "js")
                        {
                           return true;
                        }
                    }
                    else if (context_parts[1] == "Url")
                    {
                        if (url_or_js == "url")
                        {
                        return true;
                        }
                    }
                }
                else if (context_parts[0] == "Import")
                {
                    if (context_parts[1] == "Raw" || context_parts[1] == "Url")
                    {
                        if (context_parts[2] == "Start" || context_parts[2] == "Full")
                        {
                            if (url_or_js == "url")
                            {
                                return true;
                            }
                        }
                    }
                    else if (context_parts[1] == "RawJS" || context_parts[1] == "UrlJS")
                    {
                        if (url_or_js == "js")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
  
        string InsertCssQuotes(string quote, string payload)
        {
            if (quote == "Double")
            {
                return payload.Replace("<quote>", "\"");
            }
            else if (quote == "Single")
            {
                return payload.Replace("<quote>","'");
            }
            else
            {
                return payload.Replace("<quote>","");
            }
        }

        //css,js,html,attributes,attribute,unknown
        List<ReflectionContext> GetContext(string InjectedValue, Response Res)
        {
            List<ReflectionContext> ContextsList = new List<ReflectionContext>();
            if(Res.Headers.Has("Refresh"))
            {
                try
                {
                    string RefreshUrl = Tools.GetRefreshHeaderUrl(Res.Headers.Get("Refresh"));
                    if(RefreshUrl.Contains(InjectedValue))
                    {
                        ContextsList.Add(ReflectionContext.UrlAttribute);
                    }
                }catch{}
            }
            if(Res.IsHtml)
            {
                ContextsList.AddRange(Res.Html.GetContext(InjectedValue));
            }
            else if(Res.IsCss)
            {
                ContextsList.Add(ReflectionContext.CSS);
            }
            else if(Res.IsJavaScript || Res.IsJson)
            {
                ContextsList.Add(ReflectionContext.JS);
            }
            else
            {
                ContextsList.Add(ReflectionContext.Unknown);
            }
            return ContextsList;
        }

        void ReportCSSTestLead()
        {
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "XSS Plugin found reflection in CSS";
            PR.Summary = string.Format("{0} Data injected in to this parameter is being reflected back as part of CSS. Manually check this for XSS.", this.GetFindingOpeningDesc("Reflection in to CSS"));
            PR.Triggers.Add(this.ProbeString, "The payload in this request is random string just to check where this value it is reflected back in the response.", this.ProbeStringRequest, this.ProbeString, "The injected payload is found inside the CSS section of this response.", this.ProbeStringResponse);
            PR.Type = FindingType.TestLead;
            this.Scnr.AddFinding(PR);  
        }

        void ReportJSTestLead()
        {
            Finding F = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            F.Title = "XSS Plugin found reflection in JavaScript";
            F.Summary = string.Format("{0} Data injected in to this parameter is being reflected back inside JavaScript. Manually check this for XSS.", this.GetFindingOpeningDesc("Reflection in to JavaScript"));
            F.Triggers.Add(this.ProbeString, "The payload in this request is random string just to check where this value it is reflected back in the response.", this.ProbeStringRequest, this.ProbeString, "The injected payload is found inside the JavaScript code section of this response.", this.ProbeStringResponse);
            F.Type = FindingType.TestLead;
            this.Scnr.AddFinding(F);
        }
    
        void AddToTriggers(string RequestTrigger, string RequestTriggerDesc, string ResponseTrigger, string ResponseTriggerDesc)
        {
            this.AddToTriggers(RequestTrigger, RequestTriggerDesc, this.Scnr.InjectedRequest.GetClone(), ResponseTrigger, ResponseTriggerDesc, this.Scnr.InjectionResponse.GetClone());
        }
    
        void AddToTriggersWithProbeStringInjection(string RequestTrigger, string RequestTriggerDesc, string ResponseTrigger, string ResponseTriggerDesc)
        {
            this.AddToTriggers(RequestTrigger, RequestTriggerDesc, this.ProbeStringRequest, ResponseTrigger, ResponseTriggerDesc, this.ProbeStringResponse);
        }

        void AddToTriggers(string RequestTrigger, string RequestTriggerDesc, Request TriggerRequest, string ResponseTrigger, string ResponseTriggerDesc, Response TriggerResponse)
        {
            this.RequestTriggers.Add(RequestTrigger);
            this.ResponseTriggers.Add(ResponseTrigger);
            this.RequestTriggerDescs.Add(RequestTriggerDesc);
            this.ResponseTriggerDescs.Add(ResponseTriggerDesc);
            this.TriggerRequests.Add(TriggerRequest);
            this.TriggerResponses.Add(TriggerResponse);
        }


        void SetConfidence(int NewConfidence)
        {
            if (NewConfidence > this.ConfidenceLevel) this.ConfidenceLevel = NewConfidence;
        }

        void CheckResponseDetails(Response res)
        {
            if (this.Scnr.InjectedSection == "URL" && this.ProbeStringResponse.Code == 404)
            return;

            if (this.ProbeStringResponse.Code != res.Code)
            {
                this.Scnr.SetTraceTitle("Injection Response Code varies from baseline", 2);
            }
            else if (this.ProbeStringResponse.BodyLength + res.BodyLength > 0)
            {
                double diff_percent = (res.BodyLength * 1.0)/((this.ProbeStringResponse.BodyLength + res.BodyLength)* 1.0);
                if(diff_percent > 0.6 ||  diff_percent < 0.4)
                {
                    this.Scnr.SetTraceTitle("Injection Response Length varies from baseline", 1);
                }
            }
        }
  
        void ReportInjectionInSpecialAttributes(string payload)
        {
            List<string> all_tags_and_attrs = new List<string>();
            for(int i=0; i < this.injectable_special_tags.Count; i++)
            {
                all_tags_and_attrs.Add(string.Format("    {0}) <i<b>>{1}<i</b>> attribute of <i<b>>{2}<i</b>> tag", i + 1, this.injectable_special_tags[i], this.injectable_special_attributes[i]));
            }
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "Scriptless HTML Injection";
            PR.Summary = string.Format("{0}<i<br>>It is possible to inject a remote URL in to the following sensitive HTML attributes:<i<br>>{1}", this.GetFindingOpeningDesc("Scriptless HTML Injection"), string.Join("<i<br>>", all_tags_and_attrs.ToArray()));
            PR.Triggers.Add(payload, string.Format("The payload in this request contains an absolute url. The payload is {0}", payload), this.Scnr.InjectedRequest, payload, "The absolute url from the injected payload has been found inside some sensitive attributes in the HTML in this response", this.Scnr.InjectionResponse);
            PR.Severity = FindingSeverity.High;
            PR.Confidence = FindingConfidence.High;
            this.Scnr.SetTraceTitle("Scriptless HTML Injection Found", 100);
            this.Scnr.AddFinding(PR);
        }
  
        void ReportCrossSiteCookieSetting(bool meta_csc, bool header_csc)
        {
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "Cross-site Cookie Setting";
    
            string context = "";
            if (meta_csc && header_csc)
            {
                context = "META HTTP-EQUIV Set-Cookie tag and Set-Cookie header";
            }
            else if (meta_csc)
            {
                context = "META HTTP-EQUIV Set-Cookie tag";
            }
            else
            {
                context = "Set-Cookie header";
            }
            PR.Summary = string.Format("{0}. The value of this parameter is returned in the {1}", this.GetFindingOpeningDesc("Cross-site Cookie Setting"), context);
            PR.Triggers.Add(this.ProbeString, "The payload in this request is random string just to check where this value it is reflected back in the response.", this.ProbeStringRequest, this.ProbeString, string.Format("The random string from the payload has been found in the {0} of this response", context), this.ProbeStringResponse);
            PR.Severity = FindingSeverity.Medium;
            PR.Confidence = FindingConfidence.Medium;
            this.Scnr.SetTraceTitle("Cross-site Cookie Setting", 50);
            this.Scnr.AddFinding(PR);
        }
  
        void ReportCharsetNotSet()
        {
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "Charset Not Set By Server";
            PR.Summary = "The Charset of the response content is not explicitly set by the server. Lack of charset can cause the browser to guess the encoding type and this could lead to Cross-site Scripting by encoding the payload in encoding types like UTF-7.";
            PR.Triggers.Add("", "", this.BaseRequest, "", "This response does not have an explicit declaration for what character encoding is used in it.", this.BaseResponse);
            PR.Severity = FindingSeverity.Medium;
            PR.Confidence = FindingConfidence.Medium;
            this.Scnr.SetTraceTitle("Charset Missing", 50);
            this.Scnr.AddFinding(PR);
        }

        void ReportCharsetManipulation()
        {
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "Charset Manipulation Possible";
            PR.Summary = string.Format("{0}<i<br>>It is possible to set the charset of the response body to any desired encoding type.", this.GetFindingOpeningDesc("Charset Manipulation Possibility"));
            PR.Triggers.Add(this.ProbeString, string.Format("The payload in this request is just a random text - {0}", this.ProbeString), this.ProbeStringRequest, this.ProbeString, string.Format("The random string from the payload {0} is set as the character encoding of this response. This shows that the character encoding of this page can be manipulated.", this.ProbeString), this.ProbeStringResponse);
            PR.Severity = FindingSeverity.Medium;
            PR.Confidence = FindingConfidence.High;
            this.Scnr.SetTraceTitle("Charset Manipulation", 50);
            this.Scnr.AddFinding(PR);
        }

        void ReportCharsetManipulation(List<Request> inj_req, List<Response> inj_res, List<string> payloads)
        {
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            PR.Title = "Charset Manipulation Possible";
            PR.Summary = string.Format("{0}<i<br>>It is possible to set the charset of the response body to any desired encoding type.", this.GetFindingOpeningDesc("Charset Manipulation Possibility"));
            for(int i=0; i < payloads.Count; i++)
            {
                PR.Triggers.Add(payloads[i], string.Format("The payload in this request is the name of character encoding type - {0}", payloads[i]), inj_req[i], payloads[i], string.Format("The character encoding of this response is set as {0}. This is caused by the payload.", payloads[i]), inj_res[i]);
            }
            PR.Severity = FindingSeverity.Medium;
            PR.Confidence = FindingConfidence.High;
            this.Scnr.SetTraceTitle("Charset Manipulation", 50);
            this.Scnr.AddFinding(PR);
        }
  
        void ReportSameSiteScriptInclude(List<string> scripts_vuln, List<string> styles_vuln)
        {
            Finding PR = new Finding(this.Scnr.InjectedRequest.BaseUrl);
            List<string> all_vuln = new List<string>();
            all_vuln.AddRange(scripts_vuln);
            all_vuln.AddRange(styles_vuln);
    
            string scope = "";
    
            if (scripts_vuln.Count > 0 && styles_vuln.Count > 0)
            {
                scope = "JS and CSS";
            }
            else if (scripts_vuln.Count > 0)
            {
                scope = "JS";
            }
            else
            {
                scope = "CSS";
            }
            PR.Title = string.Format("In-domain {0} Inclusion", scope);
            PR.Summary = string.Format("{0}<i<br>>It is possible to set the location of {1} source URL to a resource within the same domain. If user's are allowed to upload text files on to this domain then an attacker can upload script as a regular text file and execute it using this vulnerability.", this.GetFindingOpeningDesc(string.Format("In-domain {0} Inclusion", scope)), scope);
            PR.Triggers.Add(this.ProbeString, "The payload in this request is random string just to check where this value it is reflected back in the response.", this.ProbeStringRequest, string.Join("\r\n", all_vuln.ToArray()), string.Format("The random string from the payload has been found in the src attribute of tags loading {0} files.", scope), this.ProbeStringResponse);
            PR.Severity = FindingSeverity.Medium;
            PR.Confidence = FindingConfidence.High;
            this.Scnr.SetTraceTitle(string.Format("In-domain {0} Inclusion", scope), 50);
            this.Scnr.AddFinding(PR);
        }
  
        void AnalyseResults()
        {
            if(this.RequestTriggers.Count > 0)
            {
                Finding F = new Finding(this.Scnr.InjectedRequest.BaseUrl);
                F.Title = "Cross-site Scripting Detected";
                F.Summary = this.GetFindingOpeningDesc("Cross-site Scripting");
                for(int i=0; i < this.RequestTriggers.Count; i++)
                {
                    F.Triggers.Add(this.RequestTriggers[i], this.RequestTriggerDescs[i], this.TriggerRequests[i], this.ResponseTriggers[i], this.ResponseTriggerDescs[i], this.TriggerResponses[i]);
                }
                F.Type = FindingType.Vulnerability;
                F.Severity = FindingSeverity.High;
                if (this.ConfidenceLevel == 3)
                {
                    F.Confidence = FindingConfidence.High;
                }
                else if (this.ConfidenceLevel == 2)
                {
                    F.Confidence = FindingConfidence.Medium;
                }
                else
                {
                    F.Confidence = FindingConfidence.Low;
                }
                this.Scnr.AddFinding(F);
                this.Scnr.SetTraceTitle("XSS Found", 100);
            }
        }
    }
}
