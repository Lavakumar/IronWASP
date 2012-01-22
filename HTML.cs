//
// Copyright 2011-2012 Lavakumar Kuppan
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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
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
        
        public HTML(string HtmlString)
        {
            HtmlNode.ElementsFlags.Remove("form");
            this.Html.LoadHtml(HtmlString);
        }

        public HTML()
        {
            HtmlNode.ElementsFlags.Remove("form");
        }

        public List<string> Links
        {
            get
            {
                List<string> RawLinks = this.GetValues("a", "href");
                List<string> ProcessedLinks = new List<string>();
                foreach (string RawLink in RawLinks)
                {
                    if(RawLink.StartsWith("%2f") || RawLink.StartsWith("%2F"))
                        ProcessedLinks.Add(Tools.UrlDecode(RawLink));
                    else
                        ProcessedLinks.Add(RawLink);
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
            return Query(string.Format("//{0}", ElementName));
        }

        public List<string> Get(string ElementName, string AttributeName)
        {
            return Query(string.Format("//{0}[@{1}]", ElementName, AttributeName));
        }

        public List<string> GetValues(string ElementName, string AttributeName)
        {
            return QueryValues(string.Format("//{0}[@{1}]", ElementName, AttributeName), AttributeName);
        }
        public List<string> Get(string ElementName, string AttributeName, string AttributeValue)
        {
            return Query(string.Format("//{0}[@{1}={2}]", ElementName, AttributeName, XpathSafe(AttributeValue)));
        }

        public HtmlNodeCollection GetNodes(string ElementName)
        {
            return QueryNodes(string.Format("//{0}", ElementName));
        }

        public HtmlNodeCollection GetNodes(string ElementName, string AttributeName)
        {
            return QueryNodes(string.Format("//{0}[@{1}]", ElementName, AttributeName));
        }

        public HtmlNodeCollection GetNodes(string ElementName, string AttributeName, string AttributeValue)
        {
            return QueryNodes(string.Format("//{0}[@{1}={2}]", ElementName, AttributeName, XpathSafe(AttributeValue)));
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
                        Contexts.Add("InLineJS");
                    }
                    else if (Node.ParentNode.Name.Equals("style", StringComparison.OrdinalIgnoreCase))
                    {
                        Contexts.Add("InLineCSS");
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
                        if ((Node.Name.Equals("iframe") && Attribute.Name.Equals("src")) || (Node.Name.Equals("a") && Attribute.Name.Equals("href")) || (Node.Name.Equals("form") && Attribute.Name.Equals("action")))
                        {
                            if (Attribute.Value.StartsWith(Parameter, StringComparison.OrdinalIgnoreCase))
                                Contexts.Add("UrlAttribute");
                            else if (Attribute.Value.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                                Contexts.Add("InLineJS");
                        }
                        else if (EventAttributes.Contains(Attribute.Name.ToLower()))
                        {
                            Contexts.Add("EventAttribute");
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
            List<string> JavaScripts = new List<string>();
            HtmlNodeCollection JSNodes = this.GetNodes("script");
            if (JSNodes == null) return JavaScripts;
            foreach (HtmlNode Node in JSNodes)
            {
                if (!Node.Attributes.Contains("src"))
                {
                    if (Keyword.Length == 0 || Node.InnerText.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        JavaScripts.Add(Node.InnerText);
                    }
                }
            }
            JavaScripts.AddRange(GetJavaScript(this.Html.DocumentNode, Keyword));
            return JavaScripts;
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
                        if (EventAttributes.Contains(Attr.Name) && (Keyword.Length == 0 || Node.InnerText.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            if(Attr.Value.EndsWith(";"))
                                JavaScripts.Add(Attr.Value);
                            else
                                JavaScripts.Add(Attr.Value + ";");
                        }
                        else if (Attr.Value.StartsWith("javascript:") && (Keyword.Length == 0 || Node.InnerText.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            JavaScripts.AddRange(GetJavaScriptUrlScripts(new List<string>() { Attr.Value}));
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
                if (Url.StartsWith("javascript:"))
                {
                    JavaScriptUrlScripts.Add(Url.Substring(11));
                }
            }
            return JavaScriptUrlScripts;
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
    }
}
