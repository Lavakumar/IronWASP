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
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    public class Scanner
   { 
        internal static Scanner CurrentScanner;
        internal static int CurrentScanID;
        internal static Queue<Scanner> ScanQueue = new Queue<Scanner>();

        internal static int MaxParallelScanCount = 3;

        static Queue<int> CompletedScanIDs = new Queue<int>();
        static Queue<int> AbortedScanIDs = new Queue<int>();

        internal static Dictionary<int, Thread> ScanThreads = new Dictionary<int, Thread>();

        static List<int> ActiveScanIDs = new List<int>();

        internal static bool NewScansAllowed = true;

        private int scanID=0;
        internal Request OriginalRequest;
        internal Response OriginalResponse;
        internal Request CurrentRequest;

        PluginResults PRs = new PluginResults();
        Dictionary<string, ActivePlugin> Plugins = new Dictionary<string, ActivePlugin>();
        
        internal List<int> URLInjections = new List<int>();
        internal InjectionParameters QueryInjections = new InjectionParameters();
        internal InjectionParameters BodyInjections = new InjectionParameters();
        internal List<int> BodyXmlInjections = new List<int>();
        internal Parameters BodyXmlInjectionParameters = new Parameters();
        internal InjectionParameters CookieInjections = new InjectionParameters();
        internal InjectionParameters HeadersInjections = new InjectionParameters();

        internal string ActivePluginName = "";

        public SessionPlugin SessionHandler = new SessionPlugin();
        public FormatPlugin BodyFormat = new FormatPlugin();

        //used only for updating the UI when pulling scanner from DB
        internal string Status = "Not Started";
        //internal List<string> InjectionPoints = new List<string>();
        //end

        //to check user editing in the scan configure window
        internal static bool RequestHeadersChanged = false;
        internal static bool RequestBodyChanged = false;

        Request TestRequest;
        Response TestResponse;

        string CurrentSection = "";
        int CurrentURLPartPosition = 0;
        int CurrentBodyXmlPosition = 0;
        string CurrentParameterName = "";
        string CurrentParameterValue = "";
        int CurrentSubParameterPosition = 0;

        string[,] XmlInjectionArray = new string[0,0];
        string XmlInjectionSignature = "";

        internal static Thread RequestFormatThread;

        StringBuilder TraceMsg = new StringBuilder();
        string RequestTraceMsg = "";
        string CurrentPlugin = "";

        bool StartedFromASTab = false;

        public string InjectedSection
        {
            get
            {
                return this.CurrentSection;
            }
        }
        public string InjectedParameter
        {
            get
            {
                return this.CurrentParameterName;
            }
        }

        public string PreInjectionParameterValue
        {
            get
            {
                //if (CurrentSection.Equals("URL"))
                //    return this.OriginalRequest.UrlPathParts[CurrentURLPartPosition];
                //else if (CurrentSection.Equals("Body") && BodyFormat.Name.Length > 0)
                //    return XmlInjectionArray[CurrentBodyXmlPosition, 1];
                //else
                    return CurrentParameterValue;
            }
        }

        public int InjectedUrlPathPosition
        {
            get
            {
                return CurrentURLPartPosition;
            }
        }

        public Request InjectedRequest
        {
            get
            {
                return this.TestRequest;
            }

        }
        public Response InjectionResponse
        {
            get
            {
                return this.TestResponse;
            }
        }

        public Parameters BodyXmlParameters
        {
            get
            {
                return this.BodyXmlInjectionParameters;
            }

        }

        public Request BaseRequest
        {
            get
            {
                return this.OriginalRequest.GetClone();
            }
        }

        public Response BaseResponse
        {
            get
            {
                return this.OriginalResponse.GetClone();
            }
        }

        internal int ScanID
        {
            get
            {
                return this.scanID;
            }
            set
            {
                this.scanID = value;
                this.OriginalRequest.ScanID = this.scanID;
            }
        }

        public int ID
        {
            get
            {
                return this.scanID;
            }
        }

        public int Id
        {
            get
            {
                return this.ID;
            }
        }

        public int InjectionPointsCount
        {
            get
            {
                return URLInjections.Count + QueryInjections.Count + BodyInjections.Count + CookieInjections.Count + HeadersInjections.Count + BodyXmlInjectionParameters.Count;
            }
        }

        public Scanner(Request Request)
        {
            this.OriginalRequest = Request;
        }

        public int LaunchScan()
        {
            if (!NewScansAllowed) return -1;
            this.ScanID = Interlocked.Increment(ref Config.ScanCount);
            this.StartedFromASTab = true;
            IronDB.CreateScan(ScanID, this.OriginalRequest);
            IronDB.UpdateScan(ScanID, OriginalRequest, "Queued", this.GetInjectionString(), this.BodyFormat.Name, this.GetScanPluginsString(), this.SessionHandler.Name);
            IronUI.CreateScan(this.ScanID, "Queued", this.OriginalRequest.Method, this.OriginalRequest.FullUrl);
            lock (Scanner.ScanQueue)
            {
                Scanner.ScanQueue.Enqueue(this);
            }
            this.DequeueAndStartScan();
            return this.ScanID;
        }

        internal void StartScan()
        {
            this.StartScan(Scanner.CurrentScanID);
        }
        
        internal void StartScan(int ScanID)
        {
            if (!NewScansAllowed) return;
            this.ScanID = ScanID;
            this.StartedFromASTab = true;
            IronDB.UpdateScan(this.ScanID, this.OriginalRequest, "Queued", this.GetInjectionString(), this.BodyFormat.Name, this.GetScanPluginsString(), this.SessionHandler.Name);
            IronUI.UpdateScanQueueStatus(this.ScanID, "Queued");

            lock (Scanner.ScanQueue)
            {
                Scanner.ScanQueue.Enqueue(this);
            }

            this.DequeueAndStartScan();
        }

        void DequeueAndStartScan()
        {
            if (this.StartedFromASTab) RemoveActiveScanID(this.ScanID);
            if (!NewScansAllowed) return;
            lock (Scanner.ScanQueue)
            {
                while (Scanner.ScanQueue.Count > 0 && Config.ActiveScansCount < MaxParallelScanCount)
                {
                    try
                    {
                        Scanner FromQueue = Scanner.ScanQueue.Dequeue();
                        IronUI.UpdateScanQueueStatus(FromQueue.ScanID, "Running");
                        IronDB.UpdateScanStatus(FromQueue.ScanID, "Running");
                        ThreadStart TS = new ThreadStart(FromQueue.Scan);
                        Thread ScannerThread = new Thread(TS);
                        lock (ScanThreads)
                        {
                            if (ScanThreads.ContainsKey(FromQueue.ScanID))
                            {
                                if (ScanThreads[FromQueue.ScanID] != null)
                                {
                                    try { ScanThreads[FromQueue.ScanID].Abort(); }
                                    catch { }
                                }
                                ScanThreads.Remove(FromQueue.ScanID);
                            }
                            ScanThreads.Add(FromQueue.ScanID, ScannerThread);
                        }
                        ScannerThread.Start();
                        Interlocked.Increment(ref Config.ActiveScansCount);
                    }
                    catch (Exception Exp)
                    {
                        IronException.Report("Error Starting a Queued Scan", Exp.Message, Exp.StackTrace);
                    }
                }
            }
        }
        public void Scan()
        {
            try
            {
                if (this.StartedFromASTab) AddActiveScanID(this.ScanID);
                
                this.OriginalRequest.SessionHandler = this.SessionHandler;
                this.OriginalResponse = this.OriginalRequest.Send();//this is just a temp value since calling inject from GetBaseLine would require a response object
                this.TestResponse = this.OriginalResponse;
                this.CurrentRequest = this.OriginalRequest;
                this.OriginalResponse = SessionHandler.GetBaseLine(this, this.OriginalRequest);
                this.CurrentRequest = this.OriginalRequest;
                this.TestResponse = this.OriginalResponse;

                foreach (ActivePlugin AP in this.Plugins.Values)
                {
                    this.CurrentPlugin = AP.Name;
                    this.CurrentSection = "URL";
                    foreach (int URLPartPosition in this.URLInjections)
                    {
                        this.CurrentURLPartPosition = URLPartPosition;
                        this.CurrentParameterName = "";
                        this.CurrentSubParameterPosition = 0;
                        this.CurrentParameterValue = this.CurrentRequest.UrlPathParts[URLPartPosition];
                        this.CheckWithActivePlugin(AP);
                    }
                    this.CurrentSection = "Query";
                    foreach (string ParameterName in this.QueryInjections.GetAll())
                    {
                        this.CurrentParameterName = ParameterName;
                        foreach (int SubParameterPosition in this.QueryInjections.GetAll(ParameterName))
                        {
                            this.CurrentSubParameterPosition = SubParameterPosition;
                            this.CurrentParameterValue = this.CurrentRequest.Query.GetAll(ParameterName)[SubParameterPosition];
                            this.CheckWithActivePlugin(AP);
                        }
                    }
                    this.CurrentSection = "Body";
                    if (BodyFormat.Name.Length == 0)
                    {
                        foreach (string ParameterName in this.BodyInjections.GetAll())
                        {
                            this.CurrentParameterName = ParameterName;
                            foreach (int SubParameterPosition in this.BodyInjections.GetAll(ParameterName))
                            {
                                this.CurrentSubParameterPosition = SubParameterPosition;
                                this.CurrentParameterValue = this.CurrentRequest.Body.GetAll(ParameterName)[SubParameterPosition];
                                this.CheckWithActivePlugin(AP);
                            }
                        }
                    }
                    else
                    {
                        if (this.BodyXmlInjections.Count != XmlInjectionArray.GetLength(0) || !XmlInjectionSignature.Equals(Tools.MD5("Name:" + BodyFormat.Name + "|Body" + this.OriginalRequest.BodyString)))
                        {
                            string Xml = BodyFormat.ToXmlFromRequest(this.OriginalRequest);
                            XmlInjectionArray = FormatPlugin.XmlToArray(Xml);
                            XmlInjectionSignature = Tools.MD5("Name:" + BodyFormat.Name + "|Body" + this.OriginalRequest.BodyString);
                        }
                        foreach (int BodyXmlPosition in this.BodyXmlInjections)
                        {
                            this.CurrentBodyXmlPosition = BodyXmlPosition;
                            if (XmlInjectionArray.GetLength(0) > BodyXmlPosition)
                            {
                                this.CurrentParameterName = XmlInjectionArray[BodyXmlPosition, 0];
                                this.CurrentParameterValue = XmlInjectionArray[BodyXmlPosition, 1];
                            }
                            else
                            {
                                this.CurrentParameterName = "";
                                this.CurrentParameterValue = "";
                            }
                            this.CurrentSubParameterPosition = 0;
                            this.CheckWithActivePlugin(AP);
                        }
                    }
                    this.CurrentSection = "Cookie";
                    foreach (string ParameterName in this.CookieInjections.GetAll())
                    {
                        this.CurrentParameterName = ParameterName;
                        foreach (int SubParameterPosition in this.CookieInjections.GetAll(ParameterName))
                        {
                            this.CurrentSubParameterPosition = SubParameterPosition;
                            this.CurrentParameterValue = this.CurrentRequest.Cookie.GetAll(ParameterName)[SubParameterPosition];
                            this.CheckWithActivePlugin(AP);
                        }
                    }
                    this.CurrentSection = "Headers";
                    foreach (string ParameterName in this.HeadersInjections.GetAll())
                    {
                        this.CurrentParameterName = ParameterName;
                        foreach (int SubParameterPosition in this.HeadersInjections.GetAll(ParameterName))
                        {
                            this.CurrentSubParameterPosition = SubParameterPosition;
                            this.CurrentParameterValue = this.CurrentRequest.Headers.GetAll(ParameterName)[SubParameterPosition];
                            this.CheckWithActivePlugin(AP);
                        }
                    }
                }
                if (this.StartedFromASTab)
                {
                    Interlocked.Decrement(ref Config.ActiveScansCount);
                    IronUI.UpdateScanQueueStatus(this.ScanID, "Completed");
                    IronDB.UpdateScanStatus(this.ScanID, "Completed");
                    try
                    {
                        lock (CompletedScanIDs)
                        {
                            CompletedScanIDs.Enqueue(this.ScanID);
                        }
                    }
                    catch { }
                    this.DequeueAndStartScan();
                }
            }
            catch (ThreadAbortException ThExp)
            {
                HandleScannerException(false, ThExp);
            }
            catch (Exception Exp)
            {
                HandleScannerException(true, Exp);
            }
        }

        void HandleScannerException(bool Aborted, Exception Exp)
        {
            if (this.StartedFromASTab)
            {
                Interlocked.Decrement(ref Config.ActiveScansCount);
                try
                {
                    lock (AbortedScanIDs)
                    {
                        AbortedScanIDs.Enqueue(this.ScanID);
                    }
                }
                catch { }
                try
                {
                    this.DequeueAndStartScan();
                }
                catch (Exception Expp)
                {
                    IronException.Report("Unable to Start the next Scan Job", Expp.Message, Expp.StackTrace);
                }
                if (Aborted)
                {
                    Scanner.UpdateScanStatus(this.scanID, "Aborted");
                    string Title = "Scan Aborted due to Error" + " - Scan ID: " + this.scanID.ToString();
                    string Message = Exp.Message;
                    IronException.Report(Title, Exp.Message, Exp.StackTrace);
                }
                else
                {
                    Scanner.UpdateScanStatus(this.scanID, "Stopped");
                }
            }
            else
            {
                throw Exp;
            }
        }

        internal static void UpdateScanStatus(int ScanID, string Status)
        {
            IronUI.UpdateScanQueueStatus(ScanID, Status);
            IronDB.UpdateScanStatus(ScanID, Status);
        }

        public void ScanAll()
        {
            this.ResetInjectionParameters();
            this.InjectAll();
            this.Scan();
        }
        public void InjectAll()
        {
            this.InjectURL();
            this.InjectQuery();
            this.InjectBody();
            this.InjectCookie();
            this.InjectHeaders();
        }
        public void ScanUrl() { this.ScanURL(); }
        public void ScanURL()
        {
            this.ResetInjectionParameters();
            this.InjectURL();
            this.Scan();
        }
        public void InjectUrl() { this.InjectURL(); }
        public void InjectURL()
        {
            List<string> URLParts = OriginalRequest.URLPathParts;
            for (int i = 0; i < URLParts.Count; i++)
            {
                this.InjectURL(i);
            }
        }
        public void ScanQuery()
        {
            this.ResetInjectionParameters();
            this.InjectQuery();
            this.Scan();
        }
        public void InjectQuery()
        {
            foreach (string Name in OriginalRequest.Query.GetNames())
            {
                this.InjectQuery(Name);
            }
        }
        public void ScanBody()
        {
            this.ResetInjectionParameters();
            this.InjectBody();
            this.Scan();
        }
        public void InjectBody()
        {
            if (BodyFormat.Name.Length == 0)
            {
                foreach (string Name in OriginalRequest.Body.GetNames())
                {
                    this.InjectBody(Name);
                }
            }
            else
            {
                if (this.BodyXmlInjectionParameters.Count == 0)
                {
                    string Xml = BodyFormat.ToXmlFromRequest(this.OriginalRequest);
                    XmlInjectionArray = FormatPlugin.XmlToArray(Xml);
                    XmlInjectionSignature = Tools.MD5("Name:" + BodyFormat.Name + "|Body" + this.OriginalRequest.BodyString);
                    for (int i = 0; i < XmlInjectionArray.GetLength(0); i++)
                    {
                        this.BodyXmlInjectionParameters.Add(XmlInjectionArray[i, 0], XmlInjectionArray[i, 1]);
                    }
                }
                for (int i = 0; i < this.BodyXmlInjectionParameters.Count; i++)
                {
                    this.InjectBody(i);
                }
            }
        }
        public void ScanCookie()
        {
            this.ResetInjectionParameters();
            this.InjectCookie();
            this.Scan();
        }
        public void InjectCookie()
        {
            foreach (string Name in OriginalRequest.Cookie.GetNames())
            {
                this.InjectCookie(Name);
            }
        }
        public void ScanHeaders()
        {
            this.ResetInjectionParameters();
            this.InjectHeaders();
            this.Scan();
        }
        public void InjectHeaders()
        {
            foreach (string Name in OriginalRequest.Headers.GetNames())
            {
                this.InjectHeaders(Name);
            }
        }
        public void ScanQuery(string ParameterName)
        {
            this.ResetInjectionParameters();
            this.InjectQuery(ParameterName);
            this.Scan();
        }
        public void InjectQuery(string ParameterName)
        {
            for (int i = 0; i < OriginalRequest.Query.GetAll(ParameterName).Count; i++)
            {
                this.InjectQuery(ParameterName,i);
            }
        }
        public void ScanBody(string ParameterName)
        {
            this.ResetInjectionParameters();
            this.InjectBody(ParameterName);
            this.Scan();
        }
        public void ScanBody(int XmlInjectionPoint)
        {
            this.ResetInjectionParameters();
            this.InjectBody(XmlInjectionPoint);
            this.Scan();
        }
        public void InjectBody(string ParameterName)
        {
            for (int i = 0; i < OriginalRequest.Body.GetAll(ParameterName).Count; i++)
            {
                this.InjectBody(ParameterName, i);
            }
        }
        public void InjectBody(int XmlInjectionPoint)
        {
            if (this.BodyFormat.Name.Length == 0)
            {
                throw new Exception("Format Plugin Not Selected");
            }
            if (this.BodyXmlInjectionParameters.Count == 0 || !XmlInjectionSignature.Equals(Tools.MD5("Name:" + BodyFormat.Name + "|Body" + this.OriginalRequest.BodyString)))
            {
                string Xml = BodyFormat.ToXmlFromRequest(this.OriginalRequest);
                XmlInjectionArray = FormatPlugin.XmlToArray(Xml);
                for (int i = 0; i < XmlInjectionArray.GetLength(0); i++)
                {
                    this.BodyXmlInjectionParameters.Add(XmlInjectionArray[i, 0], XmlInjectionArray[i, 1]);
                }
            }
            if (BodyXmlInjectionParameters.Count == 0) throw new Exception("No parameters to Inject");
            if (!this.BodyXmlInjections.Contains(XmlInjectionPoint)) this.BodyXmlInjections.Add(XmlInjectionPoint);
        }
        public void ScanCookie(string ParameterName)
        {
            this.ResetInjectionParameters();
            this.InjectCookie(ParameterName);
            this.Scan();
        }
        public void InjectCookie(string ParameterName)
        {
            for (int i = 0; i < OriginalRequest.Cookie.GetAll(ParameterName).Count; i++)
            {
                this.InjectCookie(ParameterName, i);
            }
        }
        public void ScanHeaders(string ParameterName)
        {
            this.ResetInjectionParameters();
            this.InjectHeaders(ParameterName);
            this.Scan();
        }
        public void InjectHeaders(string ParameterName)
        {
            for (int i = 0; i < OriginalRequest.Headers.GetAll(ParameterName).Count; i++)
            {
                this.InjectHeaders(ParameterName, i);
            }
        }

        public void ScanUrl(int i) { this.ScanURL(); }
        public void ScanURL(int i)
        {
            this.ResetInjectionParameters();
            this.InjectURL(i);
            this.Scan();
        }
        public void InjectUrl(int i) { this.InjectURL(i); }
        public void InjectURL(int i)
        {
            if(!URLInjections.Contains(i)) URLInjections.Add(i);
        }
        public void ScanQuery(string ParameterName, int SubParameterPosition)
        {
            this.ResetInjectionParameters();
            this.InjectQuery(ParameterName, SubParameterPosition);
            this.Scan();
        }
        public void InjectQuery(string ParameterName, int SubParameterPosition)
        {
            if (this.QueryInjections.Has(ParameterName))
            {
                if (!this.QueryInjections.GetAll(ParameterName).Contains(SubParameterPosition)) this.QueryInjections.Add(ParameterName, SubParameterPosition);
            }
            else
            {
                this.QueryInjections.Add(ParameterName, SubParameterPosition);
            }
        }
        public void ScanBody(string ParameterName, int SubParameterPosition)
        {
            this.ResetInjectionParameters();
            this.InjectBody(ParameterName, SubParameterPosition);
            this.Scan();
        }
        public void InjectBody(string ParameterName, int SubParameterPosition)
        {
            if (this.BodyInjections.Has(ParameterName))
            {
                if (!this.BodyInjections.GetAll(ParameterName).Contains(SubParameterPosition)) this.BodyInjections.Add(ParameterName, SubParameterPosition);
            }
            else
            {
                this.BodyInjections.Add(ParameterName, SubParameterPosition);
            }
        }
        public void ScanCookie(string ParameterName, int SubParameterPosition)
        {
            this.ResetInjectionParameters();
            this.InjectCookie(ParameterName, SubParameterPosition);
            this.Scan();
        }
        public void InjectCookie(string ParameterName, int SubParameterPosition)
        {
            if (this.CookieInjections.Has(ParameterName))
            {
                if (!this.CookieInjections.GetAll(ParameterName).Contains(SubParameterPosition)) this.CookieInjections.Add(ParameterName, SubParameterPosition);
            }
            else
            {
                this.CookieInjections.Add(ParameterName, SubParameterPosition);
            }
        }
        public void ScanHeaders(string ParameterName, int SubParameterPosition)
        {
            this.ResetInjectionParameters();
            this.InjectHeaders(ParameterName, SubParameterPosition);
            this.Scan();
        }
        public void InjectHeaders(string ParameterName, int SubParameterPosition)
        {
            if(!(ParameterName.Equals("Cookie", StringComparison.InvariantCultureIgnoreCase) || ParameterName.Equals("Host", StringComparison.InvariantCultureIgnoreCase)))
            {
                if (this.HeadersInjections.Has(ParameterName))
                {
                    if (!this.HeadersInjections.GetAll(ParameterName).Contains(SubParameterPosition)) this.HeadersInjections.Add(ParameterName, SubParameterPosition);
                }
                else
                {
                    this.HeadersInjections.Add(ParameterName, SubParameterPosition);
                }
            }
        }

        public Response Inject(string RawPayload)
        {
            return this.Inject(RawPayload, 0, false);
        }

        public Response Inject()
        {
            return this.Inject("", 0, true);
        }

        private Response Inject(string RawPayload, int ReDoCount, bool DummmyInjection)
        {
            this.CurrentRequest = SessionHandler.Update(this.CurrentRequest, this.TestResponse);
            this.TestRequest = SessionHandler.PrepareForInjection(this.CurrentRequest.GetClone());
            string Payload = "";
            if (!DummmyInjection) Payload = SessionHandler.ProcessInjection(this, this.TestRequest, RawPayload);
            List<string> SubValues = new List<string>();
            if (!DummmyInjection)
            {
                if (this.CurrentSection.Equals("URL"))
                {
                    List<string> URLParts = this.TestRequest.URLPathParts;
                    URLParts[this.CurrentURLPartPosition] = Payload;
                    this.TestRequest.URLPathParts = URLParts;
                }
                else if (this.CurrentSection.Equals("Query"))
                {
                    SubValues = this.TestRequest.Query.GetAll(this.CurrentParameterName);
                    SubValues[this.CurrentSubParameterPosition] = Payload;
                    this.TestRequest.Query.Set(this.CurrentParameterName, SubValues);
                }
                else if (this.CurrentSection.Equals("Body"))
                {
                    if (BodyFormat.Name.Length == 0)
                    {
                        SubValues = this.TestRequest.Body.GetAll(this.CurrentParameterName);
                        SubValues[this.CurrentSubParameterPosition] = Payload;
                        this.TestRequest.Body.Set(this.CurrentParameterName, SubValues);
                    }
                    else
                    {
                        string InjectedBodyXml = FormatPlugin.InjectInXml(BodyFormat.ToXmlFromRequest(this.TestRequest), this.CurrentBodyXmlPosition, Payload);
                        this.TestRequest = BodyFormat.ToRequestFromXml(this.TestRequest, InjectedBodyXml);
                    }
                }
                else if (this.CurrentSection.Equals("Cookie"))
                {
                    SubValues = this.TestRequest.Cookie.GetAll(this.CurrentParameterName);
                    SubValues[this.CurrentSubParameterPosition] = Payload;
                    this.TestRequest.Cookie.Set(this.CurrentParameterName, SubValues);
                }
                else if (this.CurrentSection.Equals("Headers"))
                {
                    SubValues = this.TestRequest.Headers.GetAll(this.CurrentParameterName);
                    SubValues[this.CurrentSubParameterPosition] = Payload;
                    this.TestRequest.Headers.Set(this.CurrentParameterName, SubValues);
                }
            }
            this.TestResponse = this.TestRequest.Send();
            if(!DummmyInjection) this.Trace("   " + this.TestResponse.ID.ToString() + " | " + this.RequestTraceMsg);
            Response InterestingResponse = SessionHandler.GetInterestingResponse(this.TestRequest, this.TestResponse);
            if (!DummmyInjection && SessionHandler.ShouldReDo(this, this.TestRequest, InterestingResponse))
            {
                if (SessionHandler.MaxReDoCount > ReDoCount)
                {
                    return Inject(Payload, ReDoCount + 1, false);
                }
                else
                {
                    throw new Exception("Unable to get valid Response for Injection, ReDoCount exceeded maximum value");
                }
            }
            else
            {
                return InterestingResponse;
            }
        }

        public Response Inject(Request RequestToInject)
        {
            RequestToInject.Source = RequestSource.Scan;
            RequestToInject.ScanID = this.ScanID;
            Response ResponseFromInjection = RequestToInject.Send();
            return SessionHandler.GetInterestingResponse(RequestToInject, ResponseFromInjection);
        }

        public void AddResult(PluginResult PR)
        {
            this.PRs.Add(PR);
            PR.Plugin = this.ActivePluginName;
            IronUpdater.AddPluginResult(PR);
        }
        public void CheckAll()
        {
            foreach (string Name in ActivePlugin.List())
            {
                this.AddCheck(Name);
            }
        }
        public void AddCheck(string PluginName)
        {
            if (ActivePlugin.List().Contains(PluginName))
            {
                if (!Plugins.ContainsKey(PluginName))
                {
                    Plugins.Add(PluginName, ActivePlugin.Get(PluginName));
                }
            }
        }

        public void RemoveCheck(string CheckName)
        {
            if (this.Plugins.ContainsKey(CheckName))
            {
                this.Plugins.Remove(CheckName);
            }
        }
        
        public void ClearChecks()
        {
            this.Plugins = new Dictionary<string, ActivePlugin>();
        }
        

        public List<string> ShowChecks()
        {
            List<string> List = new List<string>();
            foreach (string N in this.Plugins.Keys)
            {
                List.Add(N);
            }
            return List;
        }

        void ResetInjectionParameters()
        {
            this.URLInjections = new List<int>();
            this.QueryInjections = new InjectionParameters();
            this.BodyInjections = new InjectionParameters();
            this.BodyXmlInjections = new List<int>();
            this.CookieInjections = new InjectionParameters();
            this.HeadersInjections = new InjectionParameters();
        }
        void CheckWithActivePlugin(ActivePlugin AP)
        {
            this.ActivePluginName = AP.Name;
            if (!SessionHandler.CanInject(this, this.CurrentRequest))
            {
                return;
            }
            AP.Check(this.CurrentRequest.GetClone(), this);
        }
        internal void ReloadRequestFromString(string RequestString)
        {
            this.OriginalRequest = new Request(RequestString, false, true);
            this.OriginalRequest.ScanID = this.ScanID;
        }
        internal void ReloadRequestFromHeaderString(string RequestHeaderString)
        {
            Request NewRequest = new Request(RequestHeaderString, this.OriginalRequest.SSL, true);
            this.OriginalRequest.ScanID = this.ScanID;
            byte[] OldBody = new byte[this.OriginalRequest.BodyArray.Length];
            this.OriginalRequest.BodyArray.CopyTo(OldBody, 0);
            NewRequest.BodyArray = OldBody;
            this.OriginalRequest = NewRequest;
        }
        internal static void ResetChangedStatus()
        {
            RequestHeadersChanged = false;
            RequestBodyChanged = false;
        }

        internal static void StartDeSerializingRequestBody(Request Request, FormatPlugin Plugin, List<bool> CheckStatus, bool CheckAll)
        {
            BodyFormatParamters BFP = new BodyFormatParamters(Request, Plugin, CheckStatus, CheckAll);
            RequestFormatThread = new Thread(Scanner.DeSerializeRequestBody);
            RequestFormatThread.Start(BFP);
        }
        
        internal static void DeSerializeRequestBody(object BFPObject)
        {
            string PluginName = "";
            try
            {
                BodyFormatParamters BFP = (BodyFormatParamters)BFPObject;
                Request Request = BFP.Request;
                FormatPlugin Plugin = BFP.Plugin;
                PluginName = Plugin.Name;
                List<bool> CheckStatus = BFP.CheckStatus;
                bool CheckAll = BFP.CheckAll;

                string XML = Plugin.ToXmlFromRequest(Request);
                string[,] InjectionArray = FormatPlugin.XmlToArray(XML);
                IronUI.FillConfigureScanFormatDetails(XML, InjectionArray, CheckStatus, CheckAll, PluginName);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch(Exception Exp)
            {
                IronException.Report("Error Deserializing Request using Format Plugin - " + PluginName, Exp.Message, Exp.StackTrace);
                IronUI.ShowConfigureScanException("Error Deserializing");
            }
        }

        internal static void TerminateAllFormatThreads()
        {
            if(RequestFormatThread != null)
            {
                try { RequestFormatThread.Abort(); }
                catch { }
                finally { RequestFormatThread = null; }
            }
        }

        internal string GetScanPluginsString()
        {
            StringBuilder ScanPluginsBuilder = new StringBuilder();
            foreach (string ScanPlugin in this.Plugins.Keys)
            {
                ScanPluginsBuilder.Append(ScanPlugin);
                ScanPluginsBuilder.Append(",");
            }
            string SelectedScanPlugins = ScanPluginsBuilder.ToString().TrimEnd(new char[] { ',' });
            return SelectedScanPlugins;
        }

        public string GetInjectionString()
        {
            StringBuilder Url = new StringBuilder("Url:"); Url.Append(GetStringFromInjectionList(URLInjections));
            StringBuilder Query = new StringBuilder("Query:"); Query.Append(GetStringFromInjectionParameters(QueryInjections));
            StringBuilder Body = new StringBuilder("Body:");
            if (this.BodyFormat.Name.Length > 0)
            {
                Body.Append(GetStringFromInjectionParameters(BodyXmlInjectionParameters, BodyXmlInjections));
            }
            else
            {
                Body.Append(GetStringFromInjectionParameters(BodyInjections));   
            }
            StringBuilder Cookie = new StringBuilder("Cookie:"); Cookie.Append(GetStringFromInjectionParameters(CookieInjections));
            StringBuilder Headers = new StringBuilder("Headers:"); Headers.Append(GetStringFromInjectionParameters(HeadersInjections));
            StringBuilder FullInjectionString = new StringBuilder();
            FullInjectionString.AppendLine(Url.ToString());
            FullInjectionString.AppendLine(Query.ToString());
            FullInjectionString.AppendLine(Body.ToString());
            FullInjectionString.AppendLine(Cookie.ToString());
            FullInjectionString.AppendLine(Headers.ToString());
            return FullInjectionString.ToString();
        }

        internal void AbsorbInjectionString(string InjectionString)
        {
            string[] InjectionStringsArray = InjectionString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if(InjectionStringsArray.Length != 5) 
            {
                throw new Exception("Invalid Injection String");
            }
            this.URLInjections = GetListFromInjectionString(InjectionStringsArray[0].Replace("Url:",""));
            this.QueryInjections = GetParametersFromInjectionString(InjectionStringsArray[1].Replace("Query:", ""));
            if (this.BodyFormat.Name.Length == 0)
            {
                this.BodyInjections = GetParametersFromInjectionString(InjectionStringsArray[2].Replace("Body:", ""));
            }
            else
            {
                this.AbsorbFormatBodyParametersFromInjectionString(InjectionStringsArray[2].Replace("Body:", ""));
            }
            this.CookieInjections = GetParametersFromInjectionString(InjectionStringsArray[3].Replace("Cookie:", ""));
            this.HeadersInjections = GetParametersFromInjectionString(InjectionStringsArray[4].Replace("Headers:", ""));
        }

        static string GetStringFromInjectionList(List<int> InjectionList)
        {
            StringBuilder InjectionListString = new StringBuilder();
            foreach (int i in InjectionList)
            {
                InjectionListString.Append(i.ToString());
                InjectionListString.Append(",");
            }
            string ILS = InjectionListString.ToString();
            if (ILS.Length > 0) ILS = ILS.TrimEnd(new char[] { ',' });
            return ILS;
        }

        static string GetStringFromInjectionParameters(InjectionParameters InjectionParameters)
        {
            StringBuilder IS = new StringBuilder();
            foreach (string Name in InjectionParameters.GetAll())
            {
                IS.Append(Name); IS.Append("=");
                foreach (int i in InjectionParameters.GetAll(Name))
                {
                    IS.Append(i.ToString()); IS.Append(",");
                }
                IS.Append(";");
            }
            return IS.ToString();
        }

        static string GetStringFromInjectionParameters(Parameters InjectionParameters, List<int> InjectionList)
        {
            StringBuilder IS = new StringBuilder();
            int i = 0;
            foreach (string Name in InjectionParameters.GetNames())
            {
                IS.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(Name))); IS.Append(":"); IS.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(InjectionParameters.Get(Name))));
                IS.Append("-");
                if (InjectionList.Contains(i))
                {
                    IS.Append("1");
                }
                else
                {
                    IS.Append("0");
                }
                IS.Append(";");
                i++;
            }
            return IS.ToString();
        }

        static List<int> GetListFromInjectionString(string InjectionString)
        {
            List<int> InjectionList = new List<int>();
            string[] StrList = InjectionString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in StrList)
            {
                try
                {
                    InjectionList.Add(Int32.Parse(s));
                }
                catch
                {
                    throw new Exception("Invalid character in InjectionString");
                }
            }
            return InjectionList;
        }

        static InjectionParameters GetParametersFromInjectionString(string InjectionString)
        {
            InjectionParameters InjectionParameters = new InjectionParameters();
            string[] Parameters = InjectionString.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string Parameter in Parameters)
            {
                string[] ParameterParts = Parameter.Split(new string[]{"="}, StringSplitOptions.RemoveEmptyEntries);
                if (ParameterParts.Length != 2) throw new Exception("Invalid Injection String");
                string[] SubParameterPositions = ParameterParts[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string SubParameterPosition in SubParameterPositions)
                {
                    try
                    {
                        InjectionParameters.Add(ParameterParts[0], Int32.Parse(SubParameterPosition));
                    }
                    catch
                    {
                        throw new Exception("Invalid Injection String");
                    }
                }
            }
            return InjectionParameters;
        }

        void AbsorbFormatBodyParametersFromInjectionString(string InjectionString)
        {
            InjectionParameters InjectionParameters = new InjectionParameters();
            string[] Parameters = InjectionString.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            this.BodyXmlInjectionParameters = new Parameters();
            this.BodyXmlInjections.Clear();
            int i = 0;
            foreach (string Parameter in Parameters)
            {
                string[] ParameterParts = Parameter.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (ParameterParts.Length != 2) throw new Exception("Invalid Injection String");
                string[] SecondParameterParts = ParameterParts[1].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                if (SecondParameterParts.Length < 1 || SecondParameterParts.Length > 2) throw new Exception("Invalid Injection String");
                try
                {
                    string Value = "";
                    if (SecondParameterParts.Length == 2) Value = SecondParameterParts[0];
                    string InjectionState = "";
                    if (SecondParameterParts.Length == 2)
                        InjectionState = SecondParameterParts[1];
                    else
                        InjectionState = SecondParameterParts[0];
                    this.BodyXmlInjectionParameters.Add(Encoding.UTF8.GetString(Convert.FromBase64String(ParameterParts[0])), Encoding.UTF8.GetString(Convert.FromBase64String(Value)));
                    if (InjectionState.Equals("1")) this.BodyXmlInjections.Add(i);
                }
                catch
                {
                    throw new Exception("Invalid Injection String");
                }
                i++;
            }
        }

        public void StartTrace()
        {
            TraceMsg = new StringBuilder();
            RequestTraceMsg = "";
        }
        public void RequestTrace(string Message)
        {
            this.RequestTraceMsg = Message;
        }

        public void ResponseTrace(string Message)
        {
            TraceMsg.Append(Message);
        }

        public void Trace(string Message)
        {
            TraceMsg.Append("<i<br>>");
            TraceMsg.Append(Message);
        }
        public void LogTrace(string Title)
        {
            IronTrace IT = new IronTrace(this.ScanID, CurrentPlugin, this.CurrentSection, this.CurrentParameterName, Title, TraceMsg.ToString());
            IT.Report();
            this.TraceMsg = new StringBuilder();
            this.RequestTraceMsg = "";
        }

        public string GetTrace()
        {
            return TraceMsg.ToString();
        }

        public static string GetStatus(int ScanID)
        {
            return IronDB.GetScanStatus(ScanID);
        }

        public static List<int> GetCompletedScanIDs()
        {
            List<int> IDs = new List<int>();
            lock (CompletedScanIDs)
            {
                IDs = new List<int>(CompletedScanIDs.ToArray());
                CompletedScanIDs.Clear();
            }
            return IDs;
        }

        public static List<int> GetAbortedScanIDs()
        {
            List<int> IDs = new List<int>();
            lock (AbortedScanIDs)
            {
                IDs = new List<int>(AbortedScanIDs.ToArray());
                AbortedScanIDs.Clear();
            }
            return IDs;
        }

        internal static void StopAll()
        {
            NewScansAllowed = false;
            List<int> ToStop = new List<int>();
            lock (Scanner.ScanQueue)
            {
                while (Scanner.ScanQueue.Count > 0)
                {
                    Scanner Scan = Scanner.ScanQueue.Dequeue();
                    ToStop.Add(Scan.ScanID);
                }
            }
            foreach (int ScanID in ToStop)
            {
                try
                {
                    IronDB.UpdateScanStatus(ScanID, "Stopped");
                    IronUI.UpdateScanQueueStatus(ScanID, "Stopped");
                }
                catch
                { }
            }

            List<int> Running = new List<int>(ActiveScanIDs);

            foreach (int ScanID in Running)
            {
                try
                { Scanner.ScanThreads[ScanID].Abort(); }
                catch { }
            }
            NewScansAllowed = true;
        }

        void AddActiveScanID(int ID)
        {
            lock (ActiveScanIDs)
            {
                ActiveScanIDs.Add(ID);
            }
        }

        void RemoveActiveScanID(int ID)
        {
            lock (ActiveScanIDs)
            {
                if (ActiveScanIDs.Contains(ID))
                    ActiveScanIDs.Remove(ID);
            }
        }

        internal static void StartList(object ScanIDsToStartObject)
        {
            List<int> ScanIDsToStart = (List<int>)ScanIDsToStartObject;
            foreach (int ID in ScanIDsToStart)
            {
                try
                {
                    Scanner Scan = IronDB.GetScannerFromDB(ID);
                    Scan.StartScan(ID);
                }
                catch { }
            }
        }
    }
}
