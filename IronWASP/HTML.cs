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
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace IronWASP
{
    public class HTML
    {
        public HtmlDocument Html = new HtmlDocument();

        static List<string> EventAttributes = new List<string>() { "onload", "onunload", "onabort", "onbeforeunload", "onhashchange", "onmessage", "onoffline", "ononline", "onpagehide", "onpageshow", "onpopstate", "onredo", "onresize", "onstorage", "onundo", "onunload", "onkeypress", "onkeydown", "onkeyup", "onmouseover", "onmousemove", "onmouseout", "onclick", "ondblclick", "onmousedown", "onmouseup", "onmousewheel", "ondrag", "ondragover", "ondragenter", "ondragleave", "ondrop", "ondragend", "onfocus", "onblur", "onchange", "oncontextmenu", "onformchange", "onforminput", "oninput", "oninvalid", "onselect", "onsubmit", "onreset", "onbeforeprint", "onafterprint", "onerror" };

        public string Title
        {
            get
            {
                try
                {
                    string TitleWithTag = this.Get("title")[0];
                    return TitleWithTag.Substring(7, TitleWithTag.Length - 15);
                }
                catch { return ""; }
            }
        }

        public HTML(string HtmlString)
        {
            //http://stackoverflow.com/questions/2385840/how-to-get-all-input-elements-in-a-form-with-htmlagilitypack
            HtmlNode.ElementsFlags.Remove("form");
            //http://htmlagilitypack.codeplex.com/discussions/76550
            if (!HtmlNode.ElementsFlags.ContainsKey("textarea"))
                HtmlNode.ElementsFlags.Add("textarea", HtmlElementFlag.CData);
            this.Html.LoadHtml(HtmlString);
        }

        public HTML()
        {
            HtmlNode.ElementsFlags.Remove("form");
            if (!HtmlNode.ElementsFlags.ContainsKey("textarea"))
                HtmlNode.ElementsFlags.Add("textarea", HtmlElementFlag.CData);
        }

        public List<string> Links
        {
            get
            {
                List<string> RawLinks = this.GetValues("a", "href");
                List<string> ProcessedLinks = new List<string>();
                foreach (string RawLink in RawLinks)
                {
                    string DecodedRawLink = Tools.HtmlDecode(RawLink);
                    if (DecodedRawLink.StartsWith("%2f") || DecodedRawLink.StartsWith("%2F"))
                        ProcessedLinks.Add(Tools.UrlDecode(DecodedRawLink));
                    else
                        ProcessedLinks.Add(DecodedRawLink);
                }
                return ProcessedLinks;
            }
        }

        public List<string> Comments
        {
            get
            {
                return this.Get("comment()");
            }
        }

        public void Load(string HtmlString)
        {
            this.Html.LoadHtml(HtmlString);
        }

        public List<string> Get(string ElementName)
        {
            return Query(string.Format("//{0}", ElementName.ToLower()));
        }

        public List<string> Get(string ElementName, string AttributeName)
        {
            return Query(string.Format("//{0}[@{1}]", ElementName.ToLower(), AttributeName.ToLower()));
        }

        public List<string> GetValues(string ElementName, string AttributeName)
        {
            return QueryValues(string.Format("//{0}[@{1}]", ElementName.ToLower(), AttributeName.ToLower()), AttributeName.ToLower());
        }

        public List<string> GetValues(string ElementName, string AttributeName, string AttributeValue, string InterestedAttributeName)
        {
            return QueryValues(string.Format("//{0}[@{1}={2}]", ElementName.ToLower(), AttributeName.ToLower(), XpathSafe(AttributeValue)), InterestedAttributeName.ToLower());
        }

        public List<string> GetValuesIgnoreValueCase(string ElementName, string AttributeName, string AttributeValue, string InterestedAttributeName)
        {
            List<string> Values = new List<string>();
            AttributeName = AttributeName.ToLower();
            InterestedAttributeName = InterestedAttributeName.ToLower();
            HtmlNodeCollection Nodes = GetNodes(ElementName, AttributeName);
            if (Nodes == null) return Values;
            foreach (HtmlNode Node in Nodes)
            {
                string Value = "";
                bool CorrectTag = false;
                foreach (HtmlAttribute Attr in Node.Attributes)
                {
                    if (Attr.Name.Equals(InterestedAttributeName))
                    {
                        Value = Attr.Value;
                    }
                    if (Attr.Name.Equals(AttributeName) && Attr.Value.Equals(AttributeValue, StringComparison.OrdinalIgnoreCase))
                    {
                        CorrectTag = true;
                    }
                }
                if (CorrectTag)
                {
                    Values.Add(Value);
                }
            }
            return Values;
        }

        public List<string> Get(string ElementName, string AttributeName, string AttributeValue)
        {
            return Query(string.Format("//{0}[@{1}={2}]", ElementName.ToLower(), AttributeName.ToLower(), XpathSafe(AttributeValue)));
        }

        public List<string> GetMetaContent(string Attribute, string AttributeValue)
        {
            return GetValuesIgnoreValueCase("meta", Attribute, AttributeValue, "content");
        }

        public HtmlNodeCollection GetNodes(string ElementName)
        {
            return QueryNodes(string.Format("//{0}", ElementName.ToLower()));
        }

        public HtmlNodeCollection GetNodes(string ElementName, string AttributeName)
        {
            return QueryNodes(string.Format("//{0}[@{1}]", ElementName.ToLower(), AttributeName.ToLower()));
        }

        public HtmlNodeCollection GetNodes(string ElementName, string AttributeName, string AttributeValue)
        {
            return QueryNodes(string.Format("//{0}[@{1}={2}]", ElementName.ToLower(), AttributeName.ToLower(), XpathSafe(AttributeValue)));
        }

        public HtmlNodeCollection QueryNodes(string Xpath)
        {
            return Html.DocumentNode.SelectNodes(Xpath);
        }

        public List<string> Query(string Xpath)
        {
            List<string> Result = new List<string>();
            try
            {
                HtmlNodeCollection Nodes = QueryNodes(Xpath);
                foreach (HtmlNode Node in Nodes)
                {
                    Result.Add(Node.OuterHtml);
                }
            }
            catch
            {}
            return Result;
        }

        public List<string> QueryValues(string Xpath, string AttributeName)
        {
            AttributeName = AttributeName.ToLower();
            List<string> Result = new List<string>();
            try
            {
                HtmlNodeCollection Nodes = QueryNodes(Xpath);
                foreach (HtmlNode Node in Nodes)
                {
                    if (Node.Attributes.Contains(AttributeName))
                    {
                        Result.Add(Node.Attributes[AttributeName].Value);
                    }
                }
            }
            catch { }
            return Result;
        }

        public List<HtmlNode> GetForms()
        {
            List<HtmlNode> ProcessedForms = new List<HtmlNode>();
            HtmlNodeCollection Forms = this.GetNodes("form");
            
            if (Forms == null) return ProcessedForms;

            foreach (HtmlNode Form in Forms)
            {
                string FormString = Form.OuterHtml;
                HTML FormHtml = new HTML(FormString);
                List<string> InputElements = GetInputNodeStrings(FormHtml.Html.DocumentNode.FirstChild);
                ProcessedForms.Add(GetStrippedForm(FormHtml.Html.DocumentNode.FirstChild, InputElements));
            }
            return ProcessedForms;
        }

        List<string> GetInputNodeStrings(HtmlNode InputNode)
        {
            List<string> InputNodeStrings = new List<string>();
            foreach(HtmlNode Node in InputNode.ChildNodes)
            {
                if (Node.Name.Equals("input"))
                    InputNodeStrings.Add(Node.OuterHtml);
                else if (Node.ChildNodes.Count > 0)
                    InputNodeStrings.AddRange(GetInputNodeStrings(Node));
            }
            return InputNodeStrings;
        }

        HtmlNode GetStrippedForm(HtmlNode OriginalForm, List<string> InputElementStrings)
        {
            OriginalForm.RemoveAllChildren();
            foreach (string InputElementString in InputElementStrings)
            {
                HTML InputHtml = new HTML(InputElementString);
                OriginalForm.AppendChild(InputHtml.Html.DocumentNode.FirstChild);
            }
            return OriginalForm;
        }

        public List<string> GetContext(string Parameter)
        {
            List<string> Contexts = new List<string>();
            if (Html == null) return Contexts;

            foreach (HtmlNode Node in Html.DocumentNode.ChildNodes)
            {
                if (Node.OuterHtml.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Contexts.AddRange(GetContextInNode(Parameter, Node));
                }
            }
            return Contexts;
        }

        List<string> GetContextInNode(string Parameter, HtmlNode Node)
        {
            List<string> Contexts = new List<string>();
            if (Node.NodeType == HtmlNodeType.Comment)
            {
                if (Node.InnerText.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Contexts.Add("Comment");
                }
            }
            else if (Node.NodeType == HtmlNodeType.Text)
            {
                if (Node.InnerText.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (Node.ParentNode.Name.Equals("script", StringComparison.OrdinalIgnoreCase))
                    {
                        bool IsJS = true;
                        if(Node.ParentNode.Attributes.Contains("type"))
                        {
                            if(Node.ParentNode.Attributes["type"].Value.IndexOf("vbscript", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                IsJS = false;
                            }
                        }
                        else if(Node.ParentNode.Attributes.Contains("language"))
                        {
                            if(Node.ParentNode.Attributes["language"].Value.IndexOf("vbscript", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                IsJS = false;
                            }
                        }
                        if (IsJS)
                        {
                            Contexts.Add("InLineJS"); 
                        }
                        else
                        {
                            Contexts.Add("InLineVB");
                        }
                    }
                    else if (Node.ParentNode.Name.Equals("style", StringComparison.OrdinalIgnoreCase))
                    {
                        Contexts.Add("InLineCSS");
                    }
                    else if (Node.ParentNode.Name.Equals("textarea"))
                    {
                        Contexts.Add("Textarea");
                    }
                    else
                    {
                        Contexts.Add("Html");
                    }
                }
            }
            else if (Node.NodeType == HtmlNodeType.Element)
            {
                if(Node.OuterHtml.Equals(Parameter, StringComparison.OrdinalIgnoreCase))
                {
                    Contexts.Add("Html");
                }
                else if (Node.InnerHtml.Equals(Parameter, StringComparison.OrdinalIgnoreCase))
                {
                    Contexts.Add("Html");
                }
                else if (Node.Name.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Contexts.Add("ElementName");
                }
                foreach (HtmlAttribute Attribute in Node.Attributes)
                {
                    if (Attribute.Name.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Contexts.Add("AttributeName");
                    }
                    if (Attribute.Value.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (Attribute.QuoteType == AttributeValueQuote.SingleQuote)
                        {
                            Contexts.Add("AttributeValueWithSingleQuote");
                        }
                        else
                        {
                            Contexts.Add("AttributeValueWithDoubleQuote");
                        }
                        if (IsUrlAttribute(Node.Name, Attribute.Name))
                        {
                            if (Attribute.Value.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                            {
                                string JSAttributeValue = Attribute.Value.Substring(11);
                                if (JSAttributeValue.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    Contexts.Add("JSUrl");
                                }
                                else
                                {
                                    Contexts.Add("UrlAttribute");
                                }
                            }
                            else
                            {
                                Contexts.Add("UrlAttribute");
                            }
                        }
                        else if (Attribute.Name.Equals("style", StringComparison.OrdinalIgnoreCase))
                        {
                            Contexts.Add("AttributeCSS");
                        }
                        else if (EventAttributes.Contains(Attribute.Name.ToLower()))
                        {
                            Contexts.Add("EventAttribute");
                        }

                        if(Node.Name.Equals("meta", StringComparison.OrdinalIgnoreCase))
                        {
                            Contexts.Add("MetaAttribute");
                            if(Attribute.Name.Equals("content", StringComparison.OrdinalIgnoreCase))
                            {
                                if (Node.Attributes.Contains("http-equiv"))
                                {
                                    if (Node.Attributes["http-equiv"].Value.Equals("refresh", StringComparison.OrdinalIgnoreCase))
                                    {
                                        string[] RedirectParts = Attribute.Value.Split(new char[] { ';' }, 2);
                                        if (RedirectParts.Length == 2)
                                        {
                                            string RedirectUrl = RedirectParts[1].Trim();
                                            RedirectUrl = RedirectUrl.Substring(4);//strip off 'url='
                                            RedirectUrl = RedirectUrl.Trim('"').Trim('\'');
                                            if (RedirectUrl.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                                            {
                                                string JSAttributeValue = RedirectUrl.Substring(11);
                                                if (JSAttributeValue.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                                                {
                                                    Contexts.Add("JSUrl");
                                                }
                                                else if (RedirectUrl.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                                                {
                                                    Contexts.Add("UrlAttribute");
                                                }
                                            }
                                            else if (RedirectUrl.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                                            {
                                                Contexts.Add("UrlAttribute");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Node.OuterHtml.IndexOf(Parameter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    foreach (HtmlNode ChildNode in Node.ChildNodes)
                    {
                        Contexts.AddRange(GetContextInNode(Parameter, ChildNode));
                    }
                }
            }
            return Contexts;
        }

        public List<string> GetJavaScript()
        {
            return GetJavaScript("");
        }

        public List<string> GetJavaScript(string Keyword)
        {
            return GetScript(Keyword, true);
        }

        public List<string> GetVisualBasic()
        {
            return GetVisualBasic("");
        }

        public List<string> GetVisualBasic(string Keyword)
        {
            return GetScript(Keyword, false);
        }

        public List<string> GetScript(string Keyword, bool ShouldGetJavaScript)
        {
            List<string> Scripts = new List<string>();
            HtmlNodeCollection JSNodes = this.GetNodes("script");
            if(JSNodes != null)
            {
                foreach (HtmlNode Node in JSNodes)
                {
                    if (!Node.Attributes.Contains("src"))
                    {
                        if (Keyword.Length == 0 || Node.InnerText.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            bool IsJS = true;
                            if (Node.Attributes.Contains("type"))
                            {
                                if (Node.Attributes["type"].Value.IndexOf("vbscript", StringComparison.OrdinalIgnoreCase) > -1)
                                {
                                    IsJS = false;
                                }
                            }
                            else if (Node.Attributes.Contains("language"))
                            {
                                if (Node.Attributes["language"].Value.IndexOf("vbscript", StringComparison.OrdinalIgnoreCase) > -1)
                                {
                                    IsJS = false;
                                }
                            }
                            if (ShouldGetJavaScript)
                            {
                                if (IsJS)
                                {
                                    Scripts.Add(StripHtmlComment(Node.InnerText));
                                }
                            }
                            else
                            {
                                if (!IsJS)
                                {
                                    Scripts.Add(StripHtmlComment(Node.InnerText));
                                }
                            }
                        }
                    }
                }
            }
            if (ShouldGetJavaScript)
                Scripts.AddRange(GetJavaScript(this.Html.DocumentNode, Keyword));
            return Scripts;
        }

        public List<string> GetJavaScript(HtmlNode Node, string Keyword)
        {
            List<string> JavaScripts = new List<string>();
            if (Node == null) return JavaScripts;
            if (Node.Attributes != null)
            {
                foreach (HtmlAttribute Attr in Node.Attributes)
                {
                    if (Attr.Value != null)
                    {
                        if (EventAttributes.Contains(Attr.Name) && (Keyword.Length == 0 || Attr.Value.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            if(Attr.Value.EndsWith(";"))
                                JavaScripts.Add(Attr.Value);
                            else
                                JavaScripts.Add(Attr.Value + ";");
                        }
                        else if (IsUrlAttribute(Node.Name, Attr.Name) && Attr.Value.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) && (Keyword.Length == 0 || Attr.Value.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            JavaScripts.AddRange(GetJavaScriptUrlScripts(new List<string>() { Attr.Value }));
                        }
                    }
                }
            }
            if(Node.ChildNodes != null)
            {
                foreach(HtmlNode ChildNode in Node.ChildNodes)
                {
                    JavaScripts.AddRange(GetJavaScript(ChildNode, Keyword));
                }
            }
            return JavaScripts;
        }

        List<string> GetJavaScriptUrlScripts(List<string> JavaScriptUrls)
        {
            List<string> JavaScriptUrlScripts = new List<string>();
            foreach (string Url in JavaScriptUrls)
            {
                if (Url.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                {
                    string JSWithinUrl = Url.Substring(11);
                    JSWithinUrl = JSWithinUrl.Trim();
                    if (JSWithinUrl.StartsWith("\"") && JSWithinUrl.EndsWith("\""))
                        JSWithinUrl = JSWithinUrl.Trim('"');
                    else if (JSWithinUrl.StartsWith("'") && JSWithinUrl.EndsWith("'"))
                        JSWithinUrl = JSWithinUrl.Trim('\'');
                    JavaScriptUrlScripts.Add(JSWithinUrl);
                }
            }
            return JavaScriptUrlScripts;
        }

        public List<string> GetCss()
        {
            return GetCssOnWrappingCondition(false);
        }

        public List<string> GetCssOnWrappingCondition(bool WrapInLineCss)
        {
            return GetCss("", WrapInLineCss);
        }

        public List<string> GetCss(string Keyword)
        {
            return GetCss(Keyword, false);
        }

        public List<string> GetCss(string Keyword, bool WrapInLineCss)
        {
            List<string> CssStrings = new List<string>();
            HtmlNodeCollection CssNodes = this.GetNodes("style");
            if (CssNodes != null)
            {
                foreach (HtmlNode Node in CssNodes)
                {
                    if (Keyword.Length == 0 || Node.InnerText.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        CssStrings.Add(StripHtmlComment(Node.InnerText));
                    }
                }
            }
            List<string> InLineCssStrings = this.GetValues("*", "style");
            foreach (string InLineCssString in InLineCssStrings)
            {
                if (Keyword.Length == 0 || InLineCssString.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (WrapInLineCss)
                    {
                        CssStrings.Add(string.Format("x{{{0}}}", InLineCssString));
                    }
                    else
                    {
                        CssStrings.Add(InLineCssString);
                    }
                }
            }
            return CssStrings;
        }

        public string XpathSafe(string Input)
        {
            if (Input.Contains("\"") && Input.Contains("'"))
            {
                string[] DoubleQuoteSplitArray = Input.Split(new char['"']);
                StringBuilder Result = new StringBuilder("concat(");
                foreach (string Part in DoubleQuoteSplitArray)
                {
                    Result.Append("\""); Result.Append(Part); Result.Append("\""); Result.Append(",");
                    Result.Append("\""); Result.Append(",");
                }
                return Result.ToString().TrimEnd(',');
            }
            else if (Input.Contains("\""))
            {
                return "'" + Input + "'";
            }
            else if (Input.Contains("'"))
            {
                return "\"" + Input + "\"";
            }
            else
            {
                return "'" + Input + "'";
            }
        }

        public static string StripHtmlComment(string Input)
        {
            string ActualInput = Input;
            bool Processed = false;
            string ProcessedInput = Input.Trim();
            if (ProcessedInput.StartsWith("<!"))
            {
                ProcessedInput = ProcessedInput.TrimStart('<');
                ProcessedInput = ProcessedInput.TrimStart('!');
                string ProcessedInputCopy = ProcessedInput;
                foreach (Char C in ProcessedInputCopy)
                {
                    if (C == '-')
                    {
                        ProcessedInput = ProcessedInput.TrimStart('-');
                    }
                    else
                    {
                        break;
                    }
                }
                ProcessedInput = ProcessedInput.TrimEnd('>');
                ProcessedInputCopy = ProcessedInput;
                for (int i = ProcessedInputCopy.Length - 1; i >= 0; i--)
                {
                    if (ProcessedInputCopy[i] == '-')
                    {
                        Processed = true;
                        ProcessedInput = ProcessedInput.TrimEnd('-');
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (Processed)
            {
                return ProcessedInput;
            }
            else
            {
                return ActualInput;
            }
        }

        public static bool IsUrlAttribute(string TagName, string AttributeName)
        {
            if ((TagName.Equals("iframe", StringComparison.OrdinalIgnoreCase) && AttributeName.Equals("src", StringComparison.OrdinalIgnoreCase))
               || (TagName.Equals("frame", StringComparison.OrdinalIgnoreCase) && AttributeName.Equals("src", StringComparison.OrdinalIgnoreCase))
               || (TagName.Equals("a", StringComparison.OrdinalIgnoreCase) && AttributeName.Equals("href", StringComparison.OrdinalIgnoreCase))
               || (TagName.Equals("form", StringComparison.OrdinalIgnoreCase) && AttributeName.Equals("action", StringComparison.OrdinalIgnoreCase))
               || (TagName.Equals("base", StringComparison.OrdinalIgnoreCase) && AttributeName.Equals("href", StringComparison.OrdinalIgnoreCase))
               || (TagName.Equals("embed", StringComparison.OrdinalIgnoreCase) && AttributeName.Equals("src", StringComparison.OrdinalIgnoreCase))
               || (TagName.Equals("button", StringComparison.OrdinalIgnoreCase) && AttributeName.Equals("formaction", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
