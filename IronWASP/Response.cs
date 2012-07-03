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
// along with IronWASP.  If not, see http://www.gnu.org/licenses/.
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
//using Fiddler;

namespace IronWASP
{
    public class Response
    {
        //internal property
        internal int ID=0;
        internal string Host = "";//to be used inside IronProxy alone

        //only to be used when updating the log in DB & UI to aviod computation in the DB and UI threads
        internal string StoredHeadersString = "";
        internal string StoredBinaryBodyString = "";

        //used for checking binary status of response body
        internal static List<string> TextContentTypes = new List<string>();

        //Just a place holder to associate information with the Response object
        internal Dictionary<string, object> Flags = new Dictionary<string, object>();

        //private property
        string bodyString = "";
        byte[] bodyArray = new byte[0];
        int code;
        string status;
        string httpVersion;
        ResponseHeaderParameters headers;//must initialised in all constructors
        HTML html = new HTML();
        string bodyEncoding = "ISO-8859-1";
        List<SetCookie> setCookies = new List<SetCookie>();
        string DefaultEncoding = "ISO-8859-1";
        bool isCharsetSet = false;
        bool isSSlValid = true;
        bool isHtml = false;
        bool isJavaScript = false;
        bool isJson = false;
        bool isXml = false;
        bool isCss = false;

        internal int TTL = 0;

        //public getter/setter
        public string BodyString
        {
            get
            {
                return bodyString;
            }
            set
            {
                this.SetBody(value);
            }
        }

        public string BinaryBodyString
        {
            get
            {
                return Convert.ToBase64String(this.BodyArray);
            }
            set
            {
                this.BodyArray = Convert.FromBase64String(value);
            }
        }

        public byte[] BodyArray
        {
            get
            {
                return bodyArray;
            }
            set
            {
                this.SetBody(value);
            }
        }

        public string ContentType
        {
            get
            {
                if (this.Headers.Has("Content-Type"))
                {
                    return this.Headers.Get("Content-Type");
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.Headers.Set("Content-Type", value);
            }
        }
        public string HttpVersion
        {
            get
            {
                return this.HTTPVersion;
            }
        }

        //public getter
        public int BodyLength
        {
            get
            {
                if (this.bodyArray == null)
                {
                    return 0;
                }
                else
                {
                    return this.bodyArray.Length;
                }
            }
        }

        public bool IsSSLValid
        {
            get
            {
                return this.isSSlValid;
            }
        }

        public bool IsSslValid
        {
            get
            {
                return this.isSSlValid;
            }
        }

        public bool IsCharsetSet
        {
            get
            {
                return this.isCharsetSet;
            }
        }

        public bool HasBody
        {
            get
            {
                if (this.bodyArray == null) return false;
                if (this.bodyArray.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsHtml
        {
            get
            {
                return isHtml;
            }
        }

        public bool IsJavaScript
        {
            get
            {
                return isJavaScript;
            }
        }

        public bool IsJson
        {
            get
            {
                return isJson;
            }
        }

        public bool IsXml
        {
            get
            {
                return isXml;
            }
        }

        public bool IsCss
        {
            get
            {
                return isCss;
            }
        }

        public bool IsBinary
        {
            get
            {
                if (TextContentTypes.Contains("$NONE") && !Headers.Has("Content-Type")) return false;
                foreach (string Type in TextContentTypes)
                {
                    if (ContentType.IndexOf(Type, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public int Code
        {
            get
            {
                return this.code;
            }
        }
        public string Status
        {
            get
            {
                return this.status;
            }
        }
        public string HTTPVersion
        {
            get
            {
                return this.httpVersion;
            }
        }

        public HTML Html
        {
            get
            {
                return this.html;
            }
        }

        public string BodyEncoding
        {
            get
            {
                return this.bodyEncoding;
            }
        }

        public List<SetCookie> SetCookies
        {
            get
            {
                return this.setCookies;
            }
        }
        public ResponseHeaderParameters Headers
        {
            get
            {
                return this.headers;
            }
        }
        public int RoundTrip
        {
            get
            {
                return this.TTL;
            }
        }

        public bool IsRedirect
        {
            get
            {
                if (this.Code == 301 || this.Code == 302 || this.Code == 303 || this.Code == 307)
                {
                    if (this.Headers.Has("Location"))
                    {
                        string NewUrl = this.Headers.Get("Location");
                        if (NewUrl.StartsWith("/"))
                        {
                            return true;
                        }
                        else if (NewUrl.StartsWith("http://") || NewUrl.StartsWith("https://"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
        }

        internal Response(string ResponseString)
        {
            this.AbsorbResponseString(ResponseString);
        }

        public Response(string ResponseHeaders, byte[] BodyArray)
        {
            this.AbsorbResponseString(ResponseHeaders + "bw==");
            this.SetBody(BodyArray);
        }

        internal Response(Fiddler.Session Sess)
        {
            this.headers = new ResponseHeaderParameters(this);
            try
            {
                this.ID = Int32.Parse(Sess.oFlags["IronFlag-ID"]);
            }
            catch
            {
                this.ID = 0;
            }
            if (Sess.oFlags.ContainsKey("IronFlag-TTL"))
            {
                this.TTL = Int32.Parse(Sess.oFlags["IronFlag-TTL"]);
            }
            if (Sess.oFlags.ContainsKey("IronFlag-SslError"))
            {
                this.isSSlValid = false;
            }
            this.httpVersion = Sess.oResponse.headers.HTTPVersion;
            this.code = Sess.oResponse.headers.HTTPResponseCode;
            try
            {
                if (Sess.oResponse.headers.HTTPResponseStatus.IndexOf(' ') > -1)
                    this.status = Sess.oResponse.headers.HTTPResponseStatus.Substring(Sess.oResponse.headers.HTTPResponseStatus.IndexOf(' ')).Trim();
                else
                    this.status = "";
            }
            catch(Exception Exp)
            {
                Tools.Trace("Response.cs", "Invalid Fiddler Session Response Status - " + Sess.oResponse.headers.HTTPResponseStatus);
                throw (Exp);
            }
            Fiddler.HTTPResponseHeaders ResHeaders = new Fiddler.HTTPResponseHeaders();
            foreach (Fiddler.HTTPHeaderItem HHI in Sess.oResponse.headers)
            {
                this.Headers.Add(HHI.Name, HHI.Value);
            }
            if (Sess.responseBodyBytes.Length > 0)
            {
                this.SetBody(Sess.responseBodyBytes);
            }
        }

        void AbsorbResponseString(string ResponseString)
        {
            this.headers = new ResponseHeaderParameters(this);
            HTTPMessage Msg = new HTTPMessage(ResponseString);
            string[] FirstHeaderParts = Msg.FirstHeader.Split(new char[] { ' ' }, 3);
            if (FirstHeaderParts.Length < 3)
            {
                throw new Exception("Invalid Response Header");
            }
            this.httpVersion = FirstHeaderParts[0];
            if (!(this.HTTPVersion.Equals("HTTP/1.1") || this.HTTPVersion.Equals("HTTP/1.0")))
            {
                throw new Exception("Invalid Response Header");
            }
            try
            {
                this.code = Convert.ToInt32(FirstHeaderParts[1]);
            }
            catch
            {
                throw new Exception("Invalid Response Header");
            }
            this.status = FirstHeaderParts[2];
            this.headers = new ResponseHeaderParameters(this, Msg.Headers);
            this.SetBody(Msg.BodyString);
        }

        internal void SetBody(string BodyString)
        {
            this.html = new HTML();
            if (BodyString == null)
            {
                this.SetEmptyBody();
                return;
            }
            else if(BodyString.Length == 0)
            {
                this.SetEmptyBody();
                return;
            }
            this.bodyString = BodyString;
            this.bodyArray = Encoding.GetEncoding(this.GetEncoding()).GetBytes(this.bodyString);
            this.CheckBodyFormatAndHandleIt();
        }

        internal void SetBody(byte[] BodyArray)
        {
            this.html = new HTML();
            if (BodyArray == null)
            {
                this.SetEmptyBody();
                return;
            }
            else if (BodyArray.Length == 0)
            {
                this.SetEmptyBody();
                return;
            }
            this.bodyArray = BodyArray;
            this.bodyString = Encoding.GetEncoding(this.GetEncoding(BodyArray)).GetString(this.bodyArray);
            this.CheckBodyFormatAndHandleIt();
        }

        void SetEmptyBody()
        {
            this.bodyString = "";
            this.bodyArray = new byte[0];
            this.Headers.Set("Content-Type","0");
        }

        internal Response()
        {
            this.headers = new ResponseHeaderParameters(this);
        }


        string GetEncoding()
        {
            return this.GetEncoding(this.bodyString);
        }

        string GetEncoding(byte[] BodyArray)
        {
            return this.GetEncoding(Encoding.GetEncoding("ISO-8859-1").GetString(BodyArray));
        }

        string GetEncoding(string BodyString)
        {
            
            string ParsedEnc = ParseOutEncoding(BodyString);
            if (ParsedEnc == "")
            {
                isCharsetSet = false;
                this.bodyEncoding = DefaultEncoding;
                ParsedEnc = DefaultEncoding;
            }
            else
            {
                isCharsetSet = true;
                this.bodyEncoding = ParsedEnc;
            }
            return ParsedEnc;
        }

        public string ParseOutEncoding()
        {
            return this.ParseOutEncoding(this.BodyString);
        }

        string ParseOutEncoding(string BodyString)
        {
            try
            {
                string Charset = Tools.GetCharsetFromContentType(this.ContentType);
                if (Charset.Length > 0)
                {
                    try
                    {
                        Encoding.GetEncoding(Charset);
                        return Charset;
                    }
                    catch{}
                }
                //http://www.w3.org/International/questions/qa-html-encoding-declarations
                //HTML4
                //<meta http-equiv="Content-type" content="text/html;charset=UTF-8">
                //Match M = Regex.Match(BodyString, @"<meta.+http-equiv.+Content-Type.+content=.+charset=(\S+)(\""|\s+|>)", RegexOptions.IgnoreCase);
                Match M = Regex.Match(BodyString, @"<meta.+http-equiv.+Content-Type.+content=.+charset=([A-Za-z0-9_\:\.\-]+).*>", RegexOptions.IgnoreCase);
                if (M.Groups.Count > 0)
                {
                    if (M.Groups[1].Value.Length > 0)
                    {
                        try
                        {
                            Encoding.GetEncoding(M.Groups[1].Value);
                            return M.Groups[1].Value;
                        }
                        catch { }
                    }
                }
                //HTML5
                //<meta charset="UTF-8">
                M = Regex.Match(BodyString, @"<meta\s+charset=\""*([A-Za-z0-9_\:\.\-]+).*>", RegexOptions.IgnoreCase);
                if (M.Groups.Count > 0)
                {
                    if (M.Groups[1].Value.Length > 0)
                    {
                        try
                        {
                            Encoding.GetEncoding(M.Groups[1].Value);
                            return M.Groups[1].Value;
                        }
                        catch { }
                    }
                }
                //XHTML 1 as XML
                //<?xml version="1.0" encoding="UTF-8"?>
                M = Regex.Match(BodyString, @"<\?xml\s+version.+encoding=\""*([A-Za-z0-9_\:\.\-]+).*\?>", RegexOptions.IgnoreCase);
                if (M.Groups.Count > 0)
                {
                    if (M.Groups[1].Value.Length > 0)
                    {
                        try
                        {
                            Encoding.GetEncoding(M.Groups[1].Value);
                            return M.Groups[1].Value;
                        }
                        catch { }
                    }
                }
            }
            catch
            {
                return "";
            }
            return "";
        }
        
        public string GetHeadersAsString()
        {
            StringBuilder RB = new StringBuilder();
            RB.Append(this.HTTPVersion);
            RB.Append(" ");
            RB.Append(this.Code.ToString());
            RB.Append(" ");
            RB.Append(this.Status);
            RB.Append("\r\n");
            foreach (string Name in this.Headers.GetNames())
            {
                foreach (string Value in this.Headers.GetAll(Name))
                {
                    RB.Append(Name);
                    RB.Append(": ");
                    RB.Append(Value);
                    RB.Append("\r\n");
                }
            }
            RB.Append("\r\n");
            return RB.ToString();
        }
        public byte[] GetFullResponseAsByteArray()
        {
            byte[] HeaderArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(this.GetHeadersAsString());
            return HTTPMessage.GetFullMessageAsByteArray(HeaderArray, this.BodyArray);
        }

        void CheckBodyFormatAndHandleIt()
        {
            this.isJson = false;
            this.isHtml = false;
            this.isJavaScript = false;
            this.isXml = false;
            this.isCss = false;

            string LowerCaseBodyString = this.BodyString.Trim().ToLower();

            if (LowerCaseBodyString.StartsWith("<") || LowerCaseBodyString.EndsWith(">"))
            {
                if (LowerCaseBodyString.Contains("<html ") || LowerCaseBodyString.Contains("<html>") ||
                    LowerCaseBodyString.Contains("</html>") || LowerCaseBodyString.Contains("<a ") ||
                    LowerCaseBodyString.Contains("<div ") || LowerCaseBodyString.Contains("<div>") ||
                    LowerCaseBodyString.Contains("<form ") || LowerCaseBodyString.Contains("<form>") ||
                    LowerCaseBodyString.Contains("<span ") || LowerCaseBodyString.Contains("<span>") ||
                    LowerCaseBodyString.Contains("<textarea ") || LowerCaseBodyString.Contains("<textarea>") ||
                    LowerCaseBodyString.Contains("<style ") || LowerCaseBodyString.Contains("<style>") ||
                    LowerCaseBodyString.Contains("<script ") || LowerCaseBodyString.Contains("<script>") ||
                    LowerCaseBodyString.Contains("<input ") || LowerCaseBodyString.Contains("<input>"))
                {
                    if (this.ProcessHtml())
                    {
                        this.isHtml = true;
                        return;
                    }
                }
                else
                {
                    if (Tools.IsXml(this.BodyString))
                    {
                        this.isXml = true;
                        return;
                    }
                }
            }
            else
            {
                if (!Tools.IsBinary(this.BodyArray))
                {
                    if (Tools.IsJson(this.BodyString))
                    {
                        this.isJson = true;
                        return;
                    }
                    if (Tools.IsJavaScript(this.BodyString))
                    {
                        this.isJavaScript = true;
                        return;
                    }
                    if (Tools.IsCss(this.BodyString))
                    {
                        this.isCss = true;
                        return;
                    }
                    if (this.ProcessHtml())
                    {
                        this.isHtml = true;
                        return;
                    }
                }
            }
        }

        public bool ProcessHtml()
        {
            try
            {
                this.Html.Load(this.BodyString);
            }
            catch { }

            if (this.Html == null) return false;
            if (this.Html.Html == null) return false;
            if (this.html.Html.DocumentNode == null) return false;
            if (this.html.Html.DocumentNode.InnerHtml == null) return false;
            if (this.html.Html.DocumentNode.InnerText == null) return false;
            if (this.html.Html.DocumentNode.InnerText == this.html.Html.DocumentNode.InnerHtml) return false;
        
            return true;
        }

        public override string ToString()
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(this.GetHeadersAsString());
            SB.Append(this.BodyString);
            return SB.ToString();
        }

        internal void CreateSetCookieListFromHeaders()
        {
            CreateSetCookieListFromParameters(this.Headers);
        }
        internal void CreateSetCookieListFromParameters(HeaderParameters Headers)
        {
            this.setCookies = new List<SetCookie>();
            if (Headers.Has("Set-Cookie"))
            {
                foreach (string SCString in Headers.GetAll("Set-Cookie"))
                {
                    SetCookie SC = new SetCookie(SCString);
                    this.SetCookies.Add(SC);
                }
            }
        }

        public Response GetClone()
        {
            return GetClone(false);
        }
        
        internal Response GetClone(bool CopyID)
        {
            Response ClonedResponse = new Response(this.ToString());
            if (CopyID) ClonedResponse.ID = this.ID;
            ClonedResponse.TTL = this.TTL;
            return ClonedResponse;
        }

        public void SaveBody(string FileName)
        {
            File.WriteAllBytes(FileName, this.bodyArray);
        }
    }
}
