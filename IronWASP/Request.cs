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
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Hosting;
using IronPython;
using IronPython.Hosting;
using IronPython.Modules;
using IronPython.Runtime;
using IronPython.Runtime.Exceptions;
using IronRuby;
using IronRuby.Hosting;
using IronRuby.Runtime;
using IronRuby.StandardLibrary;

namespace IronWASP
{
    public class Request
    {
        public bool SSL = false;
        public string Method = "GET";
        public string HTTPVersion = "HTTP/1.1";
        public bool CanRunPassivePlugins = true;

        //internal properties
        internal int ID=0;
        internal ManualResetEvent MSR;
        internal bool FreezeURL = false;
        internal bool FreezeBodyString = false;
        internal bool FreezeCookieString = false;
        internal bool FreezeCookieHeader = false;

        //used for checking binary status of request body
        internal static List<string> TextContentTypes = new List<string>();

        //only to be used when updating the log in DB & UI to aviod computation in the DB and UI threads
        internal string StoredFile = "";
        internal string StoredParameters = "";
        internal string StoredHeadersString = "";
        internal string StoredBinaryBodyString = "";
        //internal string 

        internal DateTime TimeObject = DateTime.Now;

        //To be set explicitly when cloning
        public SessionPlugin SessionHandler = new SessionPlugin();
        

        //private properties
        string bodyString = "";
        byte[] bodyArray = new byte[0];
        Response response;
        string url = "";
        int scanID = 0;//to be used only by the Active Plugins
        QueryParameters query;//must instantiate in the constructor = new Parameters();
        BodyParameters body;//must instantiate in the constructor = new Parameters();
        CookieParameters cookie;//must instantiate in the constructor = new Parameters();
        RequestHeaderParameters headers;//must instantiate in the constructor = new Parameters();
        string source = RequestSource.Shell;

        string DefaultBodyCharset = "ISO-8859-1";


        //getter/setter properties
        public string FullURL
        {
            get
            {
                StringBuilder FL = new StringBuilder();
                if (this.SSL)
                {
                    FL.Append("https://");
                }
                else
                {
                    FL.Append("http://");
                }
                FL.Append(this.Host);
                FL.Append(this.URL);
                return FL.ToString();
            }
            set
            {
                this.AbsorbFullURL(value);
            }
        }
        //for ruby naming convention
        public string FullUrl
        {
            get
            {
                return this.FullURL;
            }
            set
            {
                this.FullURL = value;
            }
        }

        public string URL
        {
            get
            {
                return this.url;
            }
            set
            {
                this.FreezeURL = true;
                //this.url = Tools.UrlPathEncode(value);
                this.url = this.ProcessAndEncodeRawInputUrl(value);
                this.query = new QueryParameters(this, this.url);
                this.FreezeURL = false;
            }
        }

        public string Url
        {
            get
            {
                return this.URL;
            }
            set
            {
                this.URL = value;
            }
        }

        public string Host
        {
            get
            {
                if (this.headers.Has("Host"))
                {
                    return this.headers.Get("Host");
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.headers.Set("Host", value);
            }
        }
        public int BodyLength
        {
            get
            {
                return this.bodyArray.Length;
            }            
        }
        public string ContentType
        {
            get
            {
                if (this.headers.Has("Content-Type"))
                {
                    return this.headers.Get("Content-Type");
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.SetContentType(Tools.RelaxedUrlEncode(value));
            }
        }
        public RequestHeaderParameters Headers
        {
            get
            {
                return this.headers;
            }
        }
        public string BodyString
        {
            get
            {
                return this.bodyString;
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
                return this.bodyArray;
            }
            set
            {
                this.SetBody(value);
            }
        }
        public string CookieString
        {
            get
            {
                if (this.headers.Has("Cookie"))
                {
                    return this.headers.Get("Cookie"); ;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.SetCookie(value);
            }
        }
        public bool Ssl
        {
            get
            {
                return this.SSL;
            }
            set
            {
                this.SSL = value;
            }
        }
        public string HttpMethod
        {
            get
            {
                return this.Method;
            }
            set
            {
                this.Method = value;
            }
        }
        public string HttpVersion
        {
            get
            {
                return this.HTTPVersion;
            }
            set
            {
                this.HTTPVersion = value;
            }
        }
        public int ScanID
        {
            get
            {
                return this.scanID;
            }
            set
            {
                this.Source = RequestSource.Scan;
                this.scanID = value;
            }
        }

        internal string Source
        {
            get
            {
                return this.source;
            }
            set
            {
                 if(!RequestSource.IsValid(value))
                    throw new Exception(string.Format("{0} - is an invalid value. Only alphabets are allowed in source names", value));
                else if (RequestSource.IsBanned(value))
                     throw new Exception(string.Format("{0} - is a banned word for Request Source", value));
                else
                    this.source = value;
            }
        }

        public string Hash = "";

        //getter properties
        public QueryParameters Query
        {
            get
            {
                return this.query;
            }
        }
        public BodyParameters Body
        {
            get
            {
                return this.body;
            }
        }
        public CookieParameters Cookie
        {
            get
            {
                return this.cookie;
            }
        }
        public bool HasBody
        {
            get
            {
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
        public bool IsBinary
        {
            get
            {
                try
                {
                    return Tools.IsBinary(this.BodyArray);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error checking request body for binary content", Exp);
                    return false;
                }
                //if (TextContentTypes.Contains("$NONE") && !Headers.Has("Content-Type")) return false;
                
                //foreach (string Type in TextContentTypes)
                //{
                //    if (ContentType.IndexOf(Type, StringComparison.OrdinalIgnoreCase) > -1)
                //    {
                //        return false;
                //    }
                //}
                //return true;
            }
        }
        public Response Response
        {
            get
            {
                return this.response;
            }
        }
        public string Time
        {
            get
            {
                return this.TimeObject.ToShortTimeString();
            }
        }
        public string URLPath
        {
            get
            {
                string[] URLParts = this.URL.Split(new char[] { '?' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (URLParts.Length > 0)
                {
                    return URLParts[0];
                }
                else
                {
                    return this.URL;
                }
            }
            set
            {
                string Path = value;
                if(Path.Length == 0) Path = "/";
                string[] URLParts = this.URL.Split(new char[] { '?' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (URLParts.Length > 1)
                {
                    this.URL = Path + "?" + URLParts[1];
                }
                else
                {
                    this.URL = Path;
                }
            }
        }

        public string UrlPath
        {
            get
            {
                return URLPath;
            }
            set
            {
                URLPath = value;
            }
        }

        public List<string> RawURLPathParts
        {
            get
            {
                string[] URLParts = this.URLPath.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> URLPartsList = new List<string>(URLParts);
                return URLPartsList;
            }
            set
            {
                StringBuilder URLBuilder = new StringBuilder();
                if (value.Count == 0)
                {
                    this.URLPath = "/";
                    return;
                }
                foreach (string Part in value)
                {
                    URLBuilder.Append("/");
                    URLBuilder.Append(SafeRaw(Part));
                }
                if (this.URLPath.EndsWith("/"))
                {
                    URLBuilder.Append("/");
                }
                this.URLPath = URLBuilder.ToString();
            }
        }

        public List<string> RawUrlPathParts
        {
            get
            {
                return RawURLPathParts;
            }

            set
            {
                RawURLPathParts = value;
            }
        }

        public List<string> UrlPathParts
        {
            get
            {
                List<string> RawPaths = RawUrlPathParts;
                for (int i = 0; i < RawPaths.Count; i++)
                {
                    RawPaths[i] = PathPartDecode(RawPaths[i]);
                }
                return RawPaths;
            }
            set
            {
                List<string> RawPaths = new List<string>(value);
                for (int i = 0; i < RawPaths.Count; i++)
                {
                    RawPaths[i] = PathPartEncode(RawPaths[i]);
                }
                RawUrlPathParts = RawPaths;
            }
        }

        public string UrlDir
        {
            get
            {
                if (UrlPath.EndsWith("/"))
                    return UrlPath;
                else
                {
                    return UrlPath.Substring(0, UrlPath.LastIndexOf('/') + 1);
                }
            }
        }

        public string BaseUrl
        {
            get
            {
                StringBuilder SB = new StringBuilder();
                if (this.SSL)
                    SB.Append("https://");
                else
                    SB.Append("http://");
                SB.Append(this.Host);
                SB.Append("/");
                return SB.ToString();
            }
        }

       //if the file extension is longer than 8 characters or has any non-alpha characters then return ""
        public string File
        {
            get
            {
                string FullURLPath = this.URLPath;
                int DotPosition = FullURLPath.LastIndexOf('.');
                if (DotPosition >= 0)
                {
                    string File = FullURLPath.Substring(DotPosition + 1).ToLower();
                    if ((File.Length == 0) || (File.Length > 8)) return "";
                    char[] Chars = File.ToCharArray();
                    StringBuilder ExtensionBuilder = new StringBuilder();
                    foreach (char C in Chars)
                    {
                        int CharCode = (int)C;
                        if (CharCode > 96 && CharCode < 123)
                        {
                            ExtensionBuilder.Append(C.ToString());
                        }
                        else
                        {
                            return "";
                        }
                    }
                    return ExtensionBuilder.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public int LogId
        {
            get
            {
                return this.GetId();
            }
        }

        //constructors
        public Request(string FullURL)
        {
            this.InitiateParameters();
            this.AbsorbFullURL(FullURL);
        }
        public Request(string Method, string FullURL)
        {
            this.InitiateParameters();
            this.AbsorbFullURL(FullURL);
            this.Method = Method;
        }
        public Request(string Method, string FullURL, string BodyString)
        {
            this.InitiateParameters();
            this.AbsorbFullURL(FullURL);
            this.Method = Method;
            this.SetBody(BodyString);
        }
        internal Request(string RequestHeaders, byte[] BodyArray)
        {
            this.InitiateParameters();
            this.AbsorbRequestString(RequestHeaders + "bw==", false, true);
            this.SetBody(BodyArray);
        }
        internal Request (string RequestString, bool IsSSL)
        {
            this.InitiateParameters();
            this.AbsorbRequestString(RequestString,IsSSL,false);
        }
        internal Request(string RequestString, bool IsSSL, bool OverrideSSLParameter)
        {
            this.InitiateParameters();
            this.AbsorbRequestString(RequestString, IsSSL, OverrideSSLParameter);
        }

        internal Request(Fiddler.Session Sess)
        {
            this.InitiateParameters();
            if (Sess.oFlags.ContainsKey("IronFlag-ID"))
            {
                this.ID = Int32.Parse(Sess.oFlags["IronFlag-ID"]);
            }
            if (Sess.oFlags.ContainsKey("IronFlag-ScanID"))
            {
                this.ScanID = Int32.Parse(Sess.oFlags["IronFlag-ScanID"]);
            }
            if (Sess.oFlags.ContainsKey("IronFlag-Ticks"))
            {
                this.TimeObject = new DateTime(long.Parse(Sess.oFlags["IronFlag-Ticks"]));
            }
            if (Sess.oFlags.ContainsKey("IronFlag-BuiltBy"))
            {
                switch (Sess.oFlags["IronFlag-BuiltBy"])
                {
                    case("ManualTestingSection"):
                        this.Source = RequestSource.Test;
                        break;
                    case (RequestSource.Shell):
                        this.Source = RequestSource.Shell;
                        break;
                    case (RequestSource.Probe):
                        this.Source = RequestSource.Probe;
                        break;
                    case (RequestSource.Scan):
                        this.Source = RequestSource.Scan;
                        break;
                    case (RequestSource.Stealth):
                        this.Source = RequestSource.Stealth;
                        break;
                    default:
                        if (Config.IsSourcePresent(Sess.oFlags["IronFlag-BuiltBy"]))
                            this.Source = Sess.oFlags["IronFlag-BuiltBy"];
                        break;
                }
            }
            if (Sess.oFlags.ContainsKey("IronFlag-RunPassivePlugins"))
            {
                this.CanRunPassivePlugins = Sess.oFlags["IronFlag-RunPassivePlugins"].Equals("1");
            }
            this.AbsorbFullURL(Sess.fullUrl);
            this.Method = Sess.oRequest.headers.HTTPMethod;
            foreach (Fiddler.HTTPHeaderItem HHI in Sess.oRequest.headers)
            {
                this.Headers.Add(HHI.Name, HHI.Value);
            }
            this.SetBody(Sess.requestBodyBytes);
        }

        //public non-static methods
        public void SetBody(string BodyString)
        {
            this.FreezeBodyString = true;
            this.SetBodyWithoutUpdatingParameters(BodyString);
            this.body = new BodyParameters(this, this.bodyString);
            this.FreezeBodyString = false;
        }

        public void SetBody(byte[] BodyArray)
        {
            this.FreezeBodyString = true;
            if (BodyArray == null)
            {
                this.SetEmptyBody();
                return;
            }
            else if(BodyArray.Length == 0)
            {
                this.SetEmptyBody();
                return;
            }
            this.bodyArray = BodyArray;
            this.bodyString = this.GetBodyArrayAsString(this.bodyArray);
            this.body = new BodyParameters(this, this.bodyString);
            this.headers.Set("Content-Length", this.bodyArray.Length.ToString());
            this.FreezeBodyString = false;
        }

        public string GetBodyEncoding()
        {
            if (this.Headers.Has("Content-Type"))
            {
                string ContentType = this.Headers.Get("Content-Type");
                int Loc = ContentType.IndexOf("charset=");
                string Charset = Tools.GetCharsetFromContentType(ContentType);                
                if (Charset.Length > 0)
                {
                    try
                    {
                        Encoding.GetEncoding(Charset);
                        return Charset;
                    }
                    catch
                    {
                        return this.DefaultBodyCharset;
                    }
                }
            }
            return this.DefaultBodyCharset;
        }

        public string GetHeadersAsString()
        {
            StringBuilder RB = new StringBuilder();
            RB.Append(this.Method);
            RB.Append(" ");
            if (this.SSL)
            {
                RB.Append("https://");
            }
            else
            {
                RB.Append("http://");
            }
            RB.Append(this.Host);
            RB.Append(this.URL);
            RB.Append(" ");
            RB.Append(this.HTTPVersion);
            RB.Append("\r\n");
            RB.Append(this.headers.GetAsString());
            string Result = RB.ToString();
            return Result;
        }

        public byte[] GetFullRequestAsByteArray()
        {
            byte[] HeaderArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(this.GetHeadersAsString());
            if (this.bodyArray == null)
            {
                this.bodyArray = new byte[0];
            }
            return HTTPMessage.GetFullMessageAsByteArray(HeaderArray, this.bodyArray);
        }
        public override string ToString()
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(this.GetHeadersAsString());
            SB.Append(this.BodyString);
            return SB.ToString();
        }
        public string ToShortString()
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(this.GetHeadersAsStringWithoutFullURL());
            SB.Append(this.BodyString);
            return SB.ToString();
        }
        public string ToBinaryString()
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(Tools.Base64Encode(this.GetHeadersAsString()));
            SB.Append(":");
            SB.Append(Tools.Base64EncodeByteArray(this.BodyArray));
            return SB.ToString();
        }
        public Response SendReq()
        {
            return this.Send();
        }
        public Response Send()
        {
            StringDictionary Flags = new StringDictionary();
            string BuiltBy;
            if(this.Source == RequestSource.Scan)
            {
                BuiltBy = "Scan";
                this.ID = Interlocked.Increment(ref Config.ScanRequestsCount);
                Flags.Add("IronFlag-ScanID", this.ScanID.ToString());
            }
            else if(this.Source == RequestSource.Probe)
            {
                BuiltBy = "Probe";
                this.ID = Interlocked.Increment(ref Config.ProbeRequestsCount);
            }
            else if (this.Source == RequestSource.Stealth)
            {
                BuiltBy = "Stealth";
                this.ID = Interlocked.Increment(ref Config.StealthRequestsCount);
            }
            else if (this.Source == RequestSource.Shell)
            {
                BuiltBy = "Shell";
                this.ID = Interlocked.Increment(ref Config.ShellRequestsCount);
            }
            else
            {
                BuiltBy = this.Source;
                this.ID = Config.GetNewId(this.Source);
            }
            Flags.Add("IronFlag-BuiltBy", BuiltBy);
            Flags.Add("IronFlag-ID", this.ID.ToString());
            if(this.CanRunPassivePlugins)
                Flags.Add("IronFlag-RunPassivePlugins", "1");
            else
                Flags.Add("IronFlag-RunPassivePlugins", "0");
            Fiddler.HTTPRequestHeaders ReqHeaders = new Fiddler.HTTPRequestHeaders();
            ReqHeaders.HTTPMethod = this.Method;
            ReqHeaders.HTTPVersion = this.HTTPVersion;
            ReqHeaders.RequestPath = this.URL;
            if (this.SSL)
            {
                ReqHeaders.UriScheme = "https";
            }
            else
            {
                ReqHeaders.UriScheme = "http";
            }
            foreach (string Name in this.Headers.GetNames())
            {
                foreach (string Value in this.headers.GetAll(Name))
                {
                    ReqHeaders.Add(Name, Value);
                }
            }
            this.MSR = new ManualResetEvent(false);
            string DictID = string.Format("{0}-{1}", this.ID, BuiltBy);
            this.TimeObject = DateTime.Now;
            lock (Config.APIResponseDict)
            {
                Config.APIResponseDict.Add(DictID, this);
            }
            if (this.HasBody)
            {
                Fiddler.FiddlerApplication.oProxy.InjectCustomRequest(ReqHeaders, this.bodyArray, Flags);
            }
            else
            {
                string RequestStringForFiddler = this.GetHeadersAsString();
                Fiddler.FiddlerApplication.oProxy.InjectCustomRequest(RequestStringForFiddler, Flags);
            }
            this.MSR.WaitOne();
            lock (Config.APIResponseDict)
            {
                Config.APIResponseDict.Remove(DictID);
            }
            if (this.response.Code == 502 && this.response.Status.StartsWith("Fiddler - "))
            {
                throw new Exception(this.response.Status.Replace("Fiddler - ",""));
            }
            return this.response;
        }

        public Request GetClone()
        {
            return GetClone(false);
        }

        public Request GetClone(bool CopyID)
        {
            Request ClonedRequest = new Request(this.ToString(), this.SSL);
            ClonedRequest.SessionHandler = this.SessionHandler;
            ClonedRequest.Source = this.Source;
            ClonedRequest.CanRunPassivePlugins = this.CanRunPassivePlugins;
            if (CopyID) ClonedRequest.ID = this.ID;
            if (this.ScanID != 0)
            {
                ClonedRequest.ScanID = this.ScanID;
            }
            ClonedRequest.TimeObject = this.TimeObject;
            return ClonedRequest;
        }

        public void SetCookie(Response Res)
        {
            this.SetCookie(Res.SetCookies);
        }

        public void SetCookie(List<SetCookie> SetCookies)
        {
            foreach (SetCookie SC in SetCookies)
            {
                this.SetCookie(SC);
            }
        }

        public void SetCookie(SetCookie SetCookie)
        {
            this.Cookie.Set(SetCookie.Name, SetCookie.Value);
        }

        public void SetCookie(CookieStore Store)
        {
            foreach (SetCookie SC in Store.GetCookies(this))
            {
                this.Cookie.Set(SC.Name, SC.Value);
            }
        }

        public void SetSource(string EnteredSource)
        {
            if (RequestSource.IsInternal(EnteredSource))
            {
                throw new Exception(string.Format("{0} - is not allowed as a source name. Try another value.", EnteredSource));
            }
            else
            {
                if (EnteredSource.Trim().Length == 0)
                {
                    throw new Exception("Log Source cannot be empty");
                }
                if (!Regex.IsMatch(EnteredSource, "^[a-zA-Z\\s]+$"))
                {
                    throw new Exception("Log Source can only contain alphabets and space.");
                }
                this.Source = EnteredSource;
            }
        }

        public Request GetRedirect(Response Res)
        {
            if (Res.IsRedirect)
            {
                if (Res.Headers.Has("Location"))
                {
                    string NewUrl = Res.Headers.Get("Location");
                    Request NewReq = new Request(this.FullURL);
                    if (NewUrl.StartsWith("/"))
                    {
                        NewReq.URL = NewUrl;
                    }
                    else if (NewUrl.StartsWith("http://") || NewUrl.StartsWith("https://"))
                    {
                        NewReq.FullURL = NewUrl;                        
                    }
                    else if (NewUrl.StartsWith(".."))
                    {
                        int DirBackNumber = 0;
                        List<string> UpdatedNewUrlPathParts = NewReq.UrlPathParts;
                        List<string> NewUrlPathParts = new List<string>(NewUrl.Split('/'));
                        if (!NewReq.Url.EndsWith("/") && UpdatedNewUrlPathParts.Count > 0) UpdatedNewUrlPathParts.RemoveAt(UpdatedNewUrlPathParts.Count - 1);
                        foreach (string NewUrlPathPart in NewUrlPathParts)
                        {
                            if (NewUrlPathPart.Equals(".."))
                                DirBackNumber++;
                            else
                                break;
                        }
                        
                        while (UpdatedNewUrlPathParts.Count > 0 && DirBackNumber > 0)
                        {
                            UpdatedNewUrlPathParts.RemoveAt(UpdatedNewUrlPathParts.Count - 1);
                            if (NewUrlPathParts.Count > 0) NewUrlPathParts.RemoveAt(0);
                            DirBackNumber--;
                        }
                        UpdatedNewUrlPathParts.AddRange(NewUrlPathParts);
                        NewReq.UrlPathParts = UpdatedNewUrlPathParts;
                        if (NewUrl.EndsWith("/"))
                        {
                            if (!NewReq.Url.EndsWith("/")) NewReq.Url = NewReq.Url + "/";
                        }
                        else
                        {
                            if (NewReq.Url.EndsWith("/") && NewReq.Url.Length > 1) NewReq.Url = NewReq.Url.TrimEnd(new char[] { '/' });
                        }
                    }
                    else
                    {
                        List<string> NewUrlPathParts = NewReq.UrlPathParts;
                        if (!NewReq.Url.EndsWith("/") && NewUrlPathParts.Count > 0) NewUrlPathParts.RemoveAt(NewUrlPathParts.Count - 1);
                        NewReq.UrlPathParts = NewUrlPathParts;
                        if (!NewReq.Url.EndsWith("/")) NewReq.Url = NewReq.Url + "/";
                        NewReq.Url = NewReq.Url + NewUrl;
                    }
                    //this check is needed since sometimes the redirect can go to a different domain
                    if (this.Host == NewReq.Host)
                    {
                        NewReq.CookieString = this.CookieString;
                        NewReq.SetCookie(Res.SetCookies);
                    }
                    //Update the Source of the redirect request with the value of the current request
                    NewReq.Source = this.Source;
                    if (this.Source.Equals("Scan"))
                        NewReq.ScanID = this.ScanID;
                    //Update general headers associated with the current request in to the header of the redirect request
                    foreach (string HeaderName in this.Headers.GetNames())
                    {
                        if (!(HeaderName.Equals("Host", StringComparison.OrdinalIgnoreCase) || HeaderName.Equals("Content-Length", StringComparison.OrdinalIgnoreCase) || HeaderName.Equals("Content-Type", StringComparison.OrdinalIgnoreCase) || HeaderName.Equals("Cookie", StringComparison.OrdinalIgnoreCase)))
                        {
                            foreach (string HeaderValue in this.Headers.GetAll(HeaderName))
                            {
                                NewReq.Headers.Add(HeaderName, HeaderValue);
                            }
                        }
                    }
                    return NewReq;
                }
            }
            return null;
        }

        public Response Follow(Response Res)
        {
            Request RedirectReq = GetRedirect(Res);
            if (RedirectReq == null)
                return Res;
            else
                return RedirectReq.Send();
        }

        public int GetId()
        {
            return this.ID;
        }

        public string ToTestUi()
        {
            return ToTestUi();
        }

        public string ToTestUi(string Name)
        {
            if (Name.Trim().Length == 0 || ManualTesting.GroupSessions.ContainsKey(Name))
            {
                return ManualTesting.CreateNewGroupWithRequest(this, false);
            }
            else
            {
                ManualTesting.CreateNewGroupWithRequest(this, Name, false);
                return Name;
            }
        }

        //internal non-static methods
        internal void UpdateURLWithQueryString(string QueryString)
        {
            string[] URLParts = new string[2];
            URLParts = this.url.Split(new char[] {'?'}, 2);
            if (URLParts[0].Length == 0) URLParts[0] = "/";
            if (QueryString.Length > 0)
            {
                this.url = URLParts[0] + "?" + QueryString;
            }
            else
            {
                this.url = URLParts[0];
            }
        }
        internal void SetBodyWithoutUpdatingParameters(string BodyString)
        {
            if (BodyString == null)
            {
                this.SetEmptyBody();
                return;
            }
            else if (BodyString.Length == 0)
            {
                this.SetEmptyBody();
                return;
            }
            this.bodyString = BodyString;
            this.bodyArray = this.GetBodyStringAsArray(this.bodyString);
            this.headers.Set("Content-Length", this.bodyArray.Length.ToString());
        }

        internal void SetCookieWithoutUpdatingParameters(string CookieString)
        {
            if (CookieString.Length > 0)
            {
                this.Headers.Set("Cookie", CookieString);
            }
            else
            {
                if (this.Headers.Has("Cookie"))
                {
                    this.Headers.Remove("Cookie");
                }
            }
        }

        internal void UpdateCookieParametersWithoutUpdatingHeaders(string CookieString)
        {
            this.FreezeCookieHeader = true;
            this.cookie = new CookieParameters(this, CookieString);
            this.FreezeCookieHeader = false;
        }
        internal void SetResponse(Response Res)
        {
            this.response = Res;
        }
        internal Fiddler.Session ReturnAsFiddlerSession()
        {
            Fiddler.HTTPRequestHeaders HRH = this.GetFiddlerHTTPRequestHeaders();
            Fiddler.Session Sess = new Fiddler.Session(HRH, this.bodyArray);
            return Sess;
        }
        internal Fiddler.HTTPRequestHeaders GetFiddlerHTTPRequestHeaders()
        {
            Fiddler.HTTPRequestHeaders HRH = new Fiddler.HTTPRequestHeaders();
            HRH.HTTPMethod = this.Method;
            HRH.HTTPVersion = this.HTTPVersion;
            HRH.RequestPath = this.URL;
            if (this.SSL)
            {
                HRH.UriScheme = "https";
            }
            else
            {
                HRH.UriScheme = "http";
            }
            foreach (string Key in Headers.GetNames())
            {
                foreach (string Value in Headers.GetAll(Key))
                {
                    HRH.Add(Key, Value);
                }
            }
            return HRH;
        }
        internal string GetHeadersAsStringWithoutFullURL()
        {
            StringBuilder RB = new StringBuilder();
            RB.Append(this.Method);
            RB.Append(" ");
            RB.Append(this.URL);
            RB.Append(" ");
            RB.Append(this.HTTPVersion);
            RB.Append("\r\n");
            RB.Append(this.headers.GetAsString());
            string Result = RB.ToString();
            return Result;
        }


        //private non-static methods
        void AbsorbRequestString(string RequestString, bool SSL, bool OverrideSSLParameter)
        {
            HTTPMessage Msg = new HTTPMessage(RequestString);
            this.headers = new RequestHeaderParameters(this,Msg.Headers);
            this.SetParametersFromHeaders(this.headers);
            this.SSL = SSL;
            string[] FirstHeaderParts = new string[3];
            string[] FirstSplit = Msg.FirstHeader.Split(new string[]{" "}, 2, StringSplitOptions.RemoveEmptyEntries);
            if (FirstSplit.Length != 2)
            {
                throw new Exception("Invalid Request URL");
            }
            FirstHeaderParts[0] = FirstSplit[0];
            int LastSpace = FirstSplit[1].LastIndexOf(" ");
            if (LastSpace < 1 || LastSpace > (FirstSplit[1].Length - 8))
            {
                throw new Exception("Invalid Request URL");
            }
            FirstHeaderParts[1] = FirstSplit[1].Substring(0, LastSpace).Replace(" ", "%20");
            FirstHeaderParts[2] = FirstSplit[1].Substring(LastSpace+1);
            this.HTTPVersion = FirstHeaderParts[2];
            if (this.HTTPVersion.Equals("HTTP/1.1"))
            {
                if (!this.Headers.Has("Host"))
                {
                    throw new Exception("Hostname information missing");
                }
            }
            else if (!this.HttpVersion.Equals("HTTP/1.0"))
            {
                throw new Exception("Invalid HTTP version");
            }
            this.Method = FirstHeaderParts[0];
            string URLPart = FirstHeaderParts[1];
            if (URLPart.StartsWith("/"))
            {
                this.URL = URLPart;
                this.AbsorbFullURL(this.FullURL);
            }
            else if (URLPart.StartsWith("https://") || URLPart.StartsWith("http://"))
            {
                this.AbsorbFullURL(URLPart);
                if (!OverrideSSLParameter)
                {
                    this.SSL = SSL;
                }
            }
            else
            {
                throw new Exception("Invalid Request URL");
            }

            //process body            
            this.SetBody(Msg.BodyString);
        }

        internal void SetCookie(string CookieString)
        {
            this.FreezeCookieString = true;
            this.SetCookieWithoutUpdatingParameters(CookieString);
            this.cookie = new CookieParameters(this, CookieString);
            this.FreezeCookieString = false;
        }

        string GetBodyArrayAsString(byte[] BodyArray)
        {
            return Encoding.GetEncoding(this.GetBodyEncoding()).GetString(BodyArray);
        }

        byte[] GetBodyStringAsArray(string BodyString)
        {
            return Encoding.GetEncoding(this.GetBodyEncoding()).GetBytes(BodyString);
        }

        string ProcessAndEncodeRawInputUrl(string Input)
        {
            string Path = "";
            string Query = "";
            string HashPart = "";

            if (Input.Contains("#"))
            {
                string[] InputPartsAtHash = Input.Split(new char[] { '#' }, 2);
                {
                    if (InputPartsAtHash.Length == 2)
                    {
                        Input = InputPartsAtHash[0];
                        HashPart = InputPartsAtHash[1];
                    }
                }
            }

            if (Input.Contains("?"))
            {
                string[] InputParts = Input.Split(new char[] { '?' }, 2);
                if (InputParts.Length == 2)
                {
                    Path = InputParts[0];
                    Query = InputParts[1];
                }
                else
                {
                    Path = Input;
                }
            }
            else
            {
                Path = Input;
            }
            Path = Path.Replace("%2f", "/").Replace("%2F", "/");
            StringBuilder SB = new StringBuilder();
            SB.Append(Path);
            if (Query.Length > 0 || Input.EndsWith("?"))
            {
                SB.Append("?");
            }
            string[] QueryParts = Query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string QueryPart in QueryParts)
            {
                string[] KV = QueryPart.Split(new char[] { '=' }, 2);
                string Key = "";
                string Value = "";
                if (KV.Length > 0)
                {
                    Key = Tools.RelaxedUrlEncode(KV[0]);
                }
                if (KV.Length == 2)
                {
                    Value = Tools.RelaxedUrlEncode(KV[1]);
                }
                SB.Append(Key);
                if (Value.Length > 0 || QueryPart.EndsWith("="))
                {
                    SB.Append("=");
                }
                SB.Append(Value);
                SB.Append("&");
            }
            string ProcessedInput = SB.ToString();
            if (!Query.EndsWith("&"))
            {
                ProcessedInput = ProcessedInput.TrimEnd('&');
            }
            return Tools.UrlPathEncode(ProcessedInput);
        }

        void AbsorbFullURL(string FullURL)
        {
            FullURL = this.ProcessAndEncodeRawInputUrl(FullURL);
            if (!(FullURL.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || FullURL.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Request URL does not start with http:// or https://");
            }
            string[] URIParts = FullURL.Split(new string[] { "://" }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (URIParts.Length != 2) throw new Exception("Invalid Request URL");
            string Scheme = URIParts[0];
            string HostAndPath = URIParts[1];
            if (!HostAndPath.Contains("/"))
            {
                HostAndPath += "/";
            }
            int StartOfPath = HostAndPath.IndexOf("/");
            this.Host = HostAndPath.Substring(0, StartOfPath);
            if (this.Host.Length == 0) throw new Exception("Hostname is missing in URL");
            this.URL = HostAndPath.Substring(StartOfPath);
            if (Scheme.Equals("https"))
            {
                this.SSL = true;
            }
            else if (Scheme.Equals("http"))
            {
                this.SSL = false;
            }
            else
            {
                throw new Exception("Invalid Request URL");
            }
        }

        void SetContentType(string ContentType)
        {
            if (ContentType.Length > 0)
            {
                this.Headers.Set("Content-Type", ContentType);
            }
            else
            {
                if (this.Method.Equals("GET") || this.Method.Equals("HEAD"))
                {
                    if (this.Headers.Has("Content-Type"))
                    {
                        this.Headers.Remove("Content-Type");
                    }
                }
                else
                {
                    this.Headers.Set("Content-Type", ContentType);
                }
            }
        }
        void SetParametersFromHeaders(RequestHeaderParameters Headers)
        {
            if (this.headers.Has("Cookie"))
            {
                this.SetCookie(this.headers.Get("Cookie"));
            }
        }
        void SetEmptyBody()
        {
            this.bodyString = "";
            this.bodyArray = new byte[0];
            if (this.Method.Equals("GET") || this.Method.Equals("HEAD") || this.Method.Equals("CONNECT"))
            {
                if (this.headers.Has("Content-Length"))
                {
                    this.headers.Remove("Content-Length");
                }
            }
            else
            {
                this.headers.Set("Content-Length", "0");
            }
            return;
        }
        void InitiateParameters()
        {
            this.query = new QueryParameters(this);
            this.body = new BodyParameters(this);
            this.cookie = new CookieParameters(this);
            this.headers = new RequestHeaderParameters(this);
        }

        //to be used inside UI and DB updating areas
        internal string GetParametersString()
        {
            StringBuilder PB = new StringBuilder();
            if (Query.Count > 0) PB.Append("Q ");
            if (Body.Count > 0) PB.Append("B ");
            if (Cookie.Count > 0) PB.Append("C ");
            if (Headers.Count > 0) PB.Append("H ");
            return PB.ToString();
        }

        //public static methods
        public static Request FromProxyLog(int ID)
        {
            Session IrSe = Session.FromProxyLog(ID);
            return IrSe.Request;
        }
        public static Request FromTestLog(int ID)
        {
            Session IrSe = Session.FromTestLog(ID);
            return IrSe.Request;
        }
        public static Request FromShellLog(int ID)
        {
            Session IrSe = Session.FromShellLog(ID);
            return IrSe.Request;
        }
        public static Request FromProbeLog(int ID)
        {
            Session IrSe = Session.FromProbeLog(ID);
            return IrSe.Request;
        }
        public static Request FromScanLog(int ID)
        {
            Session IrSe = Session.FromScanLog(ID);
            return IrSe.Request;
        }
        public static Request FromLog(int ID, string Source)
        {
            switch (Source)
            {
                case ("Proxy"):
                    return FromProxyLog(ID);
                case ("Probe"):
                    return FromProbeLog(ID);
                case ("Test"):
                    return FromTestLog(ID);
                case ("Shell"):
                    return FromShellLog(ID);
                case ("Scan"):
                    return FromScanLog(ID);
                default:
                    Session IrSe = Session.FromLog(ID, Source);
                    return IrSe.Request;
            }
        }
        public static Request FromString(string RequestString)
        {
            return new Request(RequestString, false, true);
        }
        public static Request FromBinaryString(string BinaryRequestString)
        {
            string[] RequestParts = BinaryRequestString.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (RequestParts.Length == 1)
            {
                return Request.FromString(Tools.Base64Decode(RequestParts[0]));
            }
            else if (RequestParts.Length == 2)
            {
                return new Request(Tools.Base64Decode(RequestParts[0]), Tools.Base64DecodeToByteArray(RequestParts[1]));
            }
            else
            {
                throw new Exception("Invalid input string");
            }
        }

        public static List<Request> FromProxyLog()
        {
            List<Request> Requests = new List<Request>();
            List<Session> Sessions = Session.FromProxyLog();
            foreach (Session Sess in Sessions)
            {
                if (Sess.Request != null) Requests.Add(Sess.Request);
            }
            return Requests;
        }
        public static List<Request> FromTestLog()
        {
            List<Request> Requests = new List<Request>();
            List<Session> Sessions = Session.FromTestLog();
            foreach (Session Sess in Sessions)
            {
                if (Sess.Request != null) Requests.Add(Sess.Request);
            }
            return Requests;
        }
        public static List<Request> FromShellLog()
        {
            List<Request> Requests = new List<Request>();
            List<Session> Sessions = Session.FromShellLog();
            foreach (Session Sess in Sessions)
            {
                if (Sess.Request != null) Requests.Add(Sess.Request);
            }
            return Requests;
        }
        public static List<Request> FromProbeLog()
        {
            List<Request> Requests = new List<Request>();
            List<Session> Sessions = Session.FromProbeLog();
            foreach (Session Sess in Sessions)
            {
                if (Sess.Request != null) Requests.Add(Sess.Request);
            }
            return Requests;
        }
        public static List<Request> FromScanLog()
        {
            List<Request> Requests = new List<Request>();
            List<Session> Sessions = Session.FromScanLog();
            foreach (Session Sess in Sessions)
            {
                if (Sess.Request != null) Requests.Add(Sess.Request);
            }
            return Requests;
        }
        public static List<Request> FromLog(string Source)
        {
            switch (Source)
            {
                case ("Proxy"):
                    return FromProxyLog();
                case ("Probe"):
                    return FromProbeLog();
                case ("Test"):
                    return FromTestLog();
                case ("Shell"):
                    return FromShellLog();
                case ("Scan"):
                    return FromScanLog();
                default:
                    List<Request> Requests = new List<Request>();
                    List<Session> Sessions = Session.FromLog(Source);
                    foreach (Session Sess in Sessions)
                    {
                        if (Sess.Request != null) Requests.Add(Sess.Request);
                    }
                    return Requests;
            }
        }

        public static bool IsSame(Request A, Request B)
        {
            try
            {
                if (!A.GetHeadersAsString().Equals(B.GetHeadersAsString())) return false;
                if (A.BodyLength != B.BodyLength) return false;
                for (int i = 0; i < A.BodyLength; i++)
                {
                    if (A.BodyArray[i] != B.BodyArray[i]) return false;
                }
            }
            catch { return false; }
            return true;
        }

        string PathPartEncode(string Value)
        {
            return Tools.UrlPathPartEncode(Value);
        }
        string PathPartDecode(string Value)
        {
            return Tools.UrlPathPartDecode(Value);
        }

        string Decode(string Value)
        {
            return Tools.UrlDecode(Value);
        }

        string SafeRaw(string Value)
        {
            return Tools.RelaxedUrlEncode(Value);
        }
    }
}
