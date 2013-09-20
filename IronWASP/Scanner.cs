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
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace IronWASP
{
   public class Scanner
   { 
        internal static Scanner CurrentScanner;
        internal static int CurrentScanID;
        internal static Queue<Scanner> ScanQueue = new Queue<Scanner>();

        internal static int CurrentScannerBodyFormatTabIndex = 0;

        internal static int MaxParallelScanCount = 3;

        static Queue<int> CompletedScanIDs = new Queue<int>();
        static Queue<int> AbortedScanIDs = new Queue<int>();

        internal static Dictionary<int, Thread> ScanThreads = new Dictionary<int, Thread>();

        static List<int> ActiveScanIDs = new List<int>();

        internal static bool NewScansAllowed = true;
        internal const string DefaultStartMarker = "<<+++>>";
        internal const string DefaultEndMarker = "<<--->>";

        internal static List<string[]> DefaultEncodingRuleList = new List<string[]>() { new string[] { ">", "%3e" }, new string[] { "<", "%3c" }, new string[] { "\"", "%22" }, new string[] { "%", "%25" },
                                                                                        new string[] { ">", "&gt;" }, new string[] { "<", "&lt;" } , new string[] { "&", "&amp;" }};

        internal static List<string[]> UserSpecifiedEncodingRuleList = new List<string[]>();

        private int scanID=0;
        internal Request OriginalRequest;
        internal Response OriginalResponse;
        internal Request CurrentRequest;

        Findings PRs = new Findings();
        Dictionary<string, string> Plugins = new Dictionary<string, string>();
        
        internal List<int> URLInjections = new List<int>();
        internal InjectionParameters QueryInjections = new InjectionParameters();
        internal InjectionParameters BodyInjections = new InjectionParameters();
        internal List<int> BodyXmlInjections = new List<int>();
        internal Parameters BodyXmlInjectionParameters = new Parameters();
        internal List<string> BodyCustomInjectionParts = new List<string>();
        internal InjectionParameters CookieInjections = new InjectionParameters();
        internal InjectionParameters HeadersInjections = new InjectionParameters();
        internal InjectionParameters ParameterNameInjections = new InjectionParameters();

        int CurrentInjectionPointIndex = 0;

        internal string ActivePluginName = "";

        public SessionPlugin SessionHandler = new SessionPlugin();
        FormatPlugin bodyFormat = new FormatPlugin();

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
        int CurrentCustomInjectionPosition = 0;
        string CurrentParameterName = "";
        string CurrentParameterValue = "";
        int CurrentSubParameterPosition = 0;

        internal string[,] XmlInjectionArray = new string[0,0];
        internal string InjectionArrayXML = "";
        string XmlInjectionSignature = "";

        internal string CustomInjectionPointStartMarker = "";
        internal string CustomInjectionPointEndMarker = "";
        internal List<string[]> CharacterEscapingRules = new List<string[]>();

        internal static Thread RequestFormatThread;

        StringBuilder TraceMsg = new StringBuilder();
        XmlWriter TraceMsgXml;
        string RequestTraceMsg = "";
        int CurrentTraceRequestLogId = 0;
        string TraceTitle = "";
        int TraceTitleWeight = 0;
        string CurrentPlugin = "";
        List<string[]> TraceOverviewEntries = new List<string[]>();

        bool StartedFromASTab = false;

        protected string LogSource = RequestSource.Scan;

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

        public FormatPlugin BodyFormat
        {
            get
            {
                return this.bodyFormat;
            }
            set
            {
                this.bodyFormat = value;
                this.BodyXmlInjections = new List<int>();

                this.BodyXmlInjectionParameters = new Parameters();
                XmlInjectionArray = new string[,]{};
                if (value.Name.Length > 0)
                {
                    XmlInjectionSignature = Tools.MD5("Name:" + value.Name + "|Body" + this.OriginalRequest.BodyString);
                }
                else
                {
                    XmlInjectionSignature = "";
                }
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
                if(this.CustomInjectionPointStartMarker.Length > 0 && this.CustomInjectionPointEndMarker.Length > 0)
                    return ParameterNameInjections.Count + URLInjections.Count + QueryInjections.Count + CookieInjections.Count + HeadersInjections.Count + GetCustomInjectionPointsCount();
                else
                    return ParameterNameInjections.Count + URLInjections.Count + QueryInjections.Count + BodyInjections.Count + CookieInjections.Count + HeadersInjections.Count + BodyXmlInjectionParameters.Count;
            }
        }

        public Scanner(Request Request)
        {
            this.OriginalRequest = Request.GetClone();
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

        void PrepareScanner()
        {
            this.OriginalRequest.SessionHandler = this.SessionHandler;
            //this.OriginalResponse = this.OriginalRequest.Send();//this is just a temp value since calling inject from GetBaseLine would require a response object
            this.OriginalResponse = null;
            //this.TestResponse = this.OriginalResponse;
            this.TestResponse = null;
            this.CurrentRequest = this.OriginalRequest;
            this.OriginalResponse = SessionHandler.GetBaseLine(this, null);
            this.CurrentRequest = this.OriginalRequest;
            this.TestResponse = this.OriginalResponse;
        }

        public void Scan()
        {
            try
            {
                if (this.StartedFromASTab) AddActiveScanID(this.ScanID);

                this.PrepareScanner();
                
                foreach (string AP in this.Plugins.Keys)
                {
                    this.CurrentPlugin = AP;
                    
                    this.Reset();
                    while (this.HasMore())
                    {
                        this.Next();
                        this.CheckWithActivePlugin(AP);
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

        public bool HasMore()
        {
            if (CurrentInjectionPointIndex < this.InjectionPointsCount)
                return true;
            else
                return false;
        }

        public void Reset()
        {
            this.CurrentInjectionPointIndex = 0;
        }

        public void Next()
        {
            if (this.CurrentRequest == null) this.PrepareScanner();
            
            CurrentInjectionPointIndex++;

            int LocalPointCounter = 0;
            
            this.CurrentSection = "URL";
            foreach (int URLPartPosition in this.URLInjections)
            {
                LocalPointCounter++;

                if (LocalPointCounter == CurrentInjectionPointIndex)
                {
                    this.CurrentURLPartPosition = URLPartPosition;
                    this.CurrentParameterName = "";
                    this.CurrentSubParameterPosition = 0;
                    this.CurrentParameterValue = this.CurrentRequest.UrlPathParts[URLPartPosition];
                    return;
                }
            }

            this.CurrentSection = "Query";
            foreach (string ParameterName in this.QueryInjections.GetAll())
            {
                this.CurrentParameterName = ParameterName;
                foreach (int SubParameterPosition in this.QueryInjections.GetAll(ParameterName))
                {
                    LocalPointCounter++;

                    if (LocalPointCounter == CurrentInjectionPointIndex)
                    {
                        this.CurrentSubParameterPosition = SubParameterPosition;
                        this.CurrentParameterValue = this.CurrentRequest.Query.GetAll(ParameterName)[SubParameterPosition];
                        return;
                    }
                }
            }
            this.CurrentSection = "Body";
            if (BodyFormat.Name.Length > 0)
            {
                if (this.BodyXmlInjections.Count != XmlInjectionArray.GetLength(0) || !XmlInjectionSignature.Equals(Tools.MD5("Name:" + BodyFormat.Name + "|Body:" + this.OriginalRequest.BodyString)))
                {
                    string Xml = BodyFormat.ToXmlFromRequest(this.OriginalRequest);
                    XmlInjectionArray = FormatPlugin.XmlToArray(Xml);
                    XmlInjectionSignature = Tools.MD5("Name:" + BodyFormat.Name + "|Body:" + this.OriginalRequest.BodyString);
                }
                foreach (int BodyXmlPosition in this.BodyXmlInjections)
                {
                    LocalPointCounter++;

                    if (LocalPointCounter == CurrentInjectionPointIndex)
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
                        return;
                    }
                }
            }
            else if (CustomInjectionPointStartMarker.Length > 0 && CustomInjectionPointEndMarker.Length > 0)
            {
                this.CurrentParameterName = "";
                this.CurrentParameterValue = "";
                this.CurrentSubParameterPosition = 0;

                for (int i = 0; i < this.GetCustomInjectionPointsCount(); i++)
                {
                    LocalPointCounter++;

                    if (LocalPointCounter == CurrentInjectionPointIndex)
                    {
                        this.CurrentCustomInjectionPosition = i;
                        this.CurrentParameterName = String.Format("Custom Injection Point no. {0}", i + 1);
                        this.CurrentParameterValue = GetValueAtCustomInjectionPoint(i);
                        return;
                    }
                }
            }
            else
            {
                foreach (string ParameterName in this.BodyInjections.GetAll())
                {
                    this.CurrentParameterName = ParameterName;
                    foreach (int SubParameterPosition in this.BodyInjections.GetAll(ParameterName))
                    {
                        LocalPointCounter++;

                        if (LocalPointCounter == CurrentInjectionPointIndex)
                        {
                            this.CurrentSubParameterPosition = SubParameterPosition;
                            this.CurrentParameterValue = this.CurrentRequest.Body.GetAll(ParameterName)[SubParameterPosition];
                            return;
                        }
                    }
                }
            }

            this.CurrentSection = "Cookie";
            foreach (string ParameterName in this.CookieInjections.GetAll())
            {
                this.CurrentParameterName = ParameterName;
                foreach (int SubParameterPosition in this.CookieInjections.GetAll(ParameterName))
                {
                    LocalPointCounter++;

                    if (LocalPointCounter == CurrentInjectionPointIndex)
                    {
                        this.CurrentSubParameterPosition = SubParameterPosition;
                        this.CurrentParameterValue = this.CurrentRequest.Cookie.GetAll(ParameterName)[SubParameterPosition];
                        return;
                    }
                }
            }

            this.CurrentSection = "Headers";
            foreach (string ParameterName in this.HeadersInjections.GetAll())
            {
                this.CurrentParameterName = ParameterName;
                foreach (int SubParameterPosition in this.HeadersInjections.GetAll(ParameterName))
                {
                    LocalPointCounter++;

                    if (LocalPointCounter == CurrentInjectionPointIndex)
                    {
                        this.CurrentSubParameterPosition = SubParameterPosition;
                        this.CurrentParameterValue = this.CurrentRequest.Headers.GetAll(ParameterName)[SubParameterPosition];
                        return;
                    }
                }
            }

            this.CurrentSection = "ParameterNames";
            foreach (string ParameterName in this.ParameterNameInjections.GetAll())
            {
                LocalPointCounter++;

                if (LocalPointCounter == CurrentInjectionPointIndex)
                {
                    this.CurrentParameterName = ParameterName;
                    this.CurrentSubParameterPosition = 0;
                    this.CurrentParameterValue = "";
                    return;
                }
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

        #region InjectAndScanInShell
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
            List<string> URLParts = OriginalRequest.UrlPathParts;
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
            if (this.BodyXmlInjectionParameters.Count == 0)
            {
                DeserializeRequestBodyWithFormatPlugin();
                //string Xml = BodyFormat.ToXmlFromRequest(this.OriginalRequest);
                //InjectionArrayXML = Xml;
                //XmlInjectionArray = FormatPlugin.XmlToArray(Xml);
                //XmlInjectionSignature = Tools.MD5("Name:" + BodyFormat.Name + "|Body" + this.OriginalRequest.BodyString);
                //for (int i = 0; i < XmlInjectionArray.GetLength(0); i++)
                //{
                //    this.BodyXmlInjectionParameters.Add(XmlInjectionArray[i, 0], XmlInjectionArray[i, 1]);
                //}
            }
            //if (this.BodyXmlInjectionParameters.Count == 0 || !XmlInjectionSignature.Equals(Tools.MD5("Name:" + BodyFormat.Name + "|Body" + this.OriginalRequest.BodyString)))
            if (BodyXmlInjectionParameters.Count == 0) throw new Exception("No parameters to Inject");
            //if (!XmlInjectionSignature.Equals(Tools.MD5("Name:" + BodyFormat.Name + "|Body:" + this.OriginalRequest.BodyString)))
            //{
            //    string Xml = BodyFormat.ToXmlFromRequest(this.OriginalRequest);
            //    XmlInjectionArray = FormatPlugin.XmlToArray(Xml);
            //    for (int i = 0; i < XmlInjectionArray.GetLength(0); i++)
            //    {
            //        this.BodyXmlInjectionParameters.Add(XmlInjectionArray[i, 0], XmlInjectionArray[i, 1]);
            //    }
            //}
            if (XmlInjectionPoint >= BodyXmlInjectionParameters.Count) throw new Exception("Injection point is outside the list of available values");
            if (!this.BodyXmlInjections.Contains(XmlInjectionPoint)) this.BodyXmlInjections.Add(XmlInjectionPoint);
            this.CustomInjectionPointStartMarker = "";
            this.CustomInjectionPointEndMarker = "";
            this.BodyInjections = new InjectionParameters();
        }
        
        public void InjectBody(string StartMarker, string EndMarker)
        {
            if (StartMarker.Length == 0)
            {
                throw new Exception("Start Marker cannot be empty");
            }
            if (EndMarker.Length == 0)
            {
                throw new Exception("End Marker cannot be empty");
            }
            if (StartMarker.Equals(EndMarker))
            {
                throw new Exception("Start Marker and End Marker cannot be the same");
            }
            this.CustomInjectionPointStartMarker = StartMarker;
            this.CustomInjectionPointEndMarker = EndMarker;
            this.BodyFormat = new FormatPlugin();
            this.BodyXmlInjectionParameters = new Parameters();
            this.BodyXmlInjections = new List<int>();
            this.BodyInjections = new InjectionParameters();
        }
        public void AddEscapeRule(string RawCharacter, string EncodedCharacter)
        {
            for (int i=0; i < this.CharacterEscapingRules.Count; i++)
            {
                if (this.CharacterEscapingRules[i][0].Equals(RawCharacter))
                {
                    this.CharacterEscapingRules[i][1] = EncodedCharacter;
                    return;
                }
            }
            this.CharacterEscapingRules.Add(new string[]{RawCharacter, EncodedCharacter});
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
            this.CustomInjectionPointStartMarker = "";
            this.CustomInjectionPointEndMarker = "";
            this.BodyFormat = new FormatPlugin();
            this.BodyXmlInjectionParameters = new Parameters();
            this.BodyXmlInjections = new List<int>();
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
            if (!(ParameterName.Equals("Cookie", StringComparison.InvariantCultureIgnoreCase) || ParameterName.Equals("Host", StringComparison.InvariantCultureIgnoreCase) || ParameterName.Equals("Content-Length", StringComparison.InvariantCultureIgnoreCase)))
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
        public void InjectParameterNames()
        {
            ParameterNameInjections.Add("Query", 1);
            ParameterNameInjections.Add("Body", 1);
            ParameterNameInjections.Add("Cookie", 1);
            ParameterNameInjections.Add("Headers", 1);
        }
        public void InjectParameterName(string ParameterSectionName)
        {
            if ((new List<string>() { "Query", "Body", "Cookie", "Headers" }).Contains(ParameterSectionName))
            {
                ParameterNameInjections.Add(ParameterSectionName, 1);
            }
            else
            {
                throw new Exception("Only 'Query', 'Body', 'Cookie', 'Headers' are the accepted parameter values");
            }
        }
        #endregion

        internal void DeserializeRequestBodyWithFormatPlugin()
        {
            string Xml = BodyFormat.ToXmlFromRequest(this.OriginalRequest);
            InjectionArrayXML = Xml;
            XmlInjectionArray = FormatPlugin.XmlToArray(Xml);
            XmlInjectionSignature = Tools.MD5("Name:" + BodyFormat.Name + "|Body" + this.OriginalRequest.BodyString);
            this.BodyXmlInjectionParameters = new Parameters();
            for (int i = 0; i < XmlInjectionArray.GetLength(0); i++)
            {
                this.BodyXmlInjectionParameters.Add(XmlInjectionArray[i, 0], XmlInjectionArray[i, 1]);
            }
        }

        public Response RawInject(string Payload)
        {
            return this.Inject(Payload, 0, false, true, -1);
        }
        
        public Response Inject(string Payload)
        {
            return this.Inject(Payload, 0, false, false, -1);
        }

        public Response Inject(string Payload, int TimeOut)
        {
            return this.Inject(Payload, 0, false, false, TimeOut);
        }

        public Response Inject()
        {
            return this.Inject("", 0, true, false, -1);
        }
        
        private Response Inject(string RawPayload, int ReDoCount, bool PayloadLessInjection, bool Raw, int TimeOut)
        {
            //this.CurrentRequest = SessionHandler.Update(this.CurrentRequest, this.TestResponse);
            this.CurrentRequest = SessionHandler.DoBeforeSending(this.CurrentRequest, this.TestResponse);
            //this.TestRequest = SessionHandler.PrepareForInjection(this.CurrentRequest.GetClone());
            this.TestRequest = this.CurrentRequest.GetClone();
            string Payload = "";
            if (!PayloadLessInjection) Payload = SessionHandler.EncodePayload(this.CurrentSection, this.TestRequest, RawPayload);
            List<string> SubValues = new List<string>();
            if (!PayloadLessInjection)
            {
                if (this.CurrentSection.Equals("URL"))
                {
                    List<string> URLParts = new List<string>();
                    if(Raw)
                        URLParts = this.TestRequest.RawUrlPathParts;
                    else
                        URLParts = this.TestRequest.UrlPathParts;
                    URLParts[this.CurrentURLPartPosition] = Payload;
                    if(Raw)
                        this.TestRequest.RawUrlPathParts = URLParts;
                    else
                        this.TestRequest.UrlPathParts = URLParts;
                }
                else if (this.CurrentSection.Equals("Query"))
                {
                    if(Raw)
                        SubValues = this.TestRequest.Query.RawGetAll(this.CurrentParameterName);
                    else
                        SubValues = this.TestRequest.Query.GetAll(this.CurrentParameterName);
                    SubValues[this.CurrentSubParameterPosition] = Payload;
                    if(Raw)
                        this.TestRequest.Query.RawSet(this.CurrentParameterName, SubValues);
                    else
                        this.TestRequest.Query.Set(this.CurrentParameterName, SubValues);
                }
                else if (this.CurrentSection.Equals("Body"))
                {
                    if (BodyFormat.Name.Length > 0)
                    {
                        string InjectedBodyXml = FormatPlugin.InjectInXml(BodyFormat.ToXmlFromRequest(this.TestRequest), this.CurrentBodyXmlPosition, Payload);
                        this.TestRequest = BodyFormat.ToRequestFromXml(this.TestRequest, InjectedBodyXml);
                    }
                    else if (this.CustomInjectionPointStartMarker.Length > 0 && this.CustomInjectionPointEndMarker.Length > 0)
                    {
                        this.TestRequest.BodyString = InjectAtCustomInjectionPoint(this.CurrentCustomInjectionPosition, Payload);
                    }
                    else
                    {
                        if (Raw)
                            SubValues = this.TestRequest.Body.RawGetAll(this.CurrentParameterName);
                        else
                            SubValues = this.TestRequest.Body.GetAll(this.CurrentParameterName);
                        SubValues[this.CurrentSubParameterPosition] = Payload;
                        if (Raw)
                            this.TestRequest.Body.RawSet(this.CurrentParameterName, SubValues);
                        else
                            this.TestRequest.Body.Set(this.CurrentParameterName, SubValues);
                    }
                }
                else if (this.CurrentSection.Equals("Cookie"))
                {
                    if(Raw)
                        SubValues = this.TestRequest.Cookie.RawGetAll(this.CurrentParameterName);
                    else
                        SubValues = this.TestRequest.Cookie.GetAll(this.CurrentParameterName);
                    SubValues[this.CurrentSubParameterPosition] = Payload;
                    if(Raw)
                        this.TestRequest.Cookie.RawSet(this.CurrentParameterName, SubValues);
                    else
                        this.TestRequest.Cookie.Set(this.CurrentParameterName, SubValues);
                }
                else if (this.CurrentSection.Equals("Headers"))
                {
                    if(Raw)
                        SubValues = this.TestRequest.Headers.RawGetAll(this.CurrentParameterName);
                    else
                        SubValues = this.TestRequest.Headers.GetAll(this.CurrentParameterName);
                    SubValues[this.CurrentSubParameterPosition] = Payload;
                    if(Raw)
                        this.TestRequest.Headers.RawSet(this.CurrentParameterName, SubValues);
                    else
                        this.TestRequest.Headers.Set(this.CurrentParameterName, SubValues);
                }
                else if (this.CurrentSection.Equals("ParameterNames"))
                {
                    switch (this.CurrentParameterName)
                    {
                        case("Query"):
                            if (Raw)
                                this.TestRequest.Query.RawSet(Payload, "0");
                            else
                                this.TestRequest.Query.Set(Payload, "0");
                            break;
                        case ("Body"):
                            if (Raw)
                                this.TestRequest.Body.RawSet(Payload, "0");
                            else
                                this.TestRequest.Body.Set(Payload, "0");
                            break;
                        case ("Cookie"):
                            if (Raw)
                                this.TestRequest.Cookie.RawSet(Payload, "0");
                            else
                                this.TestRequest.Cookie.Set(Payload, "0");
                            break;
                        case ("Headers"):
                            if (Raw)
                                this.TestRequest.Headers.RawSet(Payload, "0");
                            else
                                this.TestRequest.Headers.Set(Payload, "0");
                            break;
                    }
                }
            }
            if (this.CustomInjectionPointStartMarker.Length > 0 && this.CustomInjectionPointEndMarker.Length > 0)
            {
                this.TestRequest.BodyString = this.TestRequest.BodyString.Replace(this.CustomInjectionPointStartMarker, "").Replace(this.CustomInjectionPointEndMarker, "");
            }
            if (this.LogSource != RequestSource.Scan) this.TestRequest.SetSource(this.LogSource);//For Scanner, the logsource is set when ScanId is assigned to the request
            if (TimeOut > 0)
            {
                this.TestResponse = this.TestRequest.Send(TimeOut);
            }
            else
            {
                this.TestResponse = this.TestRequest.Send();
            }
            if (this.LogSource.Equals(RequestSource.Scan))
            {
                this.CurrentTraceRequestLogId = this.TestResponse.ID;
            }
            //Response InterestingResponse = SessionHandler.GetInterestingResponse(this.TestRequest, this.TestResponse);
            Response ResponseFromInjection = SessionHandler.DoAfterSending(this.TestResponse, this.TestRequest);
            //if (!DummmyInjection && SessionHandler.ShouldReDo(this, this.TestRequest, ResponseFromInjection))
            //{
            //    if (SessionHandler.MaxReDoCount > ReDoCount)
            //    {
            //        return Inject(Payload, ReDoCount + 1, false, Raw);
            //    }
            //    else
            //    {
            //        throw new Exception("Unable to get valid Response for Injection, ReDoCount exceeded maximum value");
            //    }
            //}
            //else
            //{
            //    return ResponseFromInjection;
            //}
            if (this.LogSource.Equals(RequestSource.Scan))
            {
                if (this.TestResponse.ID == ResponseFromInjection.ID)
                {
                    if (PayloadLessInjection)
                    {
                        this.TraceOverviewEntries.Add(new string[] { PreInjectionParameterValue, ResponseFromInjection.ID.ToString(), ResponseFromInjection.Code.ToString(), ResponseFromInjection.BodyLength.ToString(), ResponseFromInjection.ContentType, ResponseFromInjection.RoundTrip.ToString(), Tools.MD5(ResponseFromInjection.ToString()) });
                    }
                    else
                    {
                        this.TraceOverviewEntries.Add(new string[] { Payload, ResponseFromInjection.ID.ToString(), ResponseFromInjection.Code.ToString(), ResponseFromInjection.BodyLength.ToString(), ResponseFromInjection.ContentType, ResponseFromInjection.RoundTrip.ToString(), Tools.MD5(ResponseFromInjection.ToString()) });
                    }
                }
                else
                {
                    if (PayloadLessInjection)
                    {
                        this.TraceOverviewEntries.Add(new string[] { PreInjectionParameterValue, this.TestResponse.ID.ToString(), this.TestResponse.Code.ToString(), this.TestResponse.BodyLength.ToString(), this.TestResponse.ContentType, this.TestResponse.RoundTrip.ToString(), Tools.MD5(this.TestResponse.ToString()), ResponseFromInjection.ID.ToString(), ResponseFromInjection.Code.ToString(), ResponseFromInjection.BodyLength.ToString(), ResponseFromInjection.ContentType, ResponseFromInjection.RoundTrip.ToString(), Tools.MD5(ResponseFromInjection.ToString()) });
                    }
                    else
                    {
                        this.TraceOverviewEntries.Add(new string[] { Payload, this.TestResponse.ID.ToString(), this.TestResponse.Code.ToString(), this.TestResponse.BodyLength.ToString(), this.TestResponse.ContentType, this.TestResponse.RoundTrip.ToString(), Tools.MD5(this.TestResponse.ToString()), ResponseFromInjection.ID.ToString(), ResponseFromInjection.Code.ToString(), ResponseFromInjection.BodyLength.ToString(), ResponseFromInjection.ContentType, ResponseFromInjection.RoundTrip.ToString(), Tools.MD5(ResponseFromInjection.ToString()) });
                    }
                }
            }

            return ResponseFromInjection;
        }

        public Response Inject(Request RequestToInject)
        {
            RequestToInject.Source = RequestSource.Scan;
            RequestToInject.ScanID = this.ScanID;
            Response ResponseFromInjection = RequestToInject.Send();
            //return SessionHandler.GetInterestingResponse(RequestToInject, ResponseFromInjection);
            return SessionHandler.DoAfterSending(ResponseFromInjection, RequestToInject);
        }

        public void AddFinding(Finding F)
        {
            F.ScanId = this.ID;
            F.AffectedSection = this.InjectedSection;
            F.AffectedParameter = this.InjectedParameter;

            F.FinderName = this.ActivePluginName;
            F.FinderType = "ActivePlugin";

            F.BaseRequest = this.BaseRequest;
            F.BaseResponse = this.BaseResponse;

            this.PRs.Add(F);
            F.Report();
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
                    Plugins.Add(PluginName, "");
                }
            }
            else
            {
                throw new Exception("Plugin with name ' "+ PluginName + "' not found");
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
            this.Plugins = new Dictionary<string, string>();
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
        void CheckWithActivePlugin(string PluginName)
        {
            Exception E = null;
            ActivePlugin AP = ActivePlugin.Get(PluginName);
            this.ActivePluginName = AP.Name;
            if (!SessionHandler.CanInject(this, this.CurrentRequest))
            {
                return;
            }
            try
            {
                this.StartTrace();
                this.SetTraceTitle("-", 0);
                AP.Check(this);
            }
            catch (ThreadAbortException)
            { }
            catch (Exception Exp)
            {
                E = Exp;
                IronException.Report(string.Format("'{0}' check for Scan ID-{1} crashed with an exception", PluginName, this.ScanID.ToString()), Exp);
            }
            try
            {
                this.LogTrace();
            }
            catch(Exception Exp)
            {
                IronException.Report("Error Logging Scan Trace", Exp);
            }
            if (E != null) throw E;
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
                Scanner.CurrentScanner.BodyFormat = Plugin;
                Scanner.CurrentScanner.DeserializeRequestBodyWithFormatPlugin();
                string XML = Scanner.CurrentScanner.InjectionArrayXML;
                string[,] InjectionArray = Scanner.CurrentScanner.XmlInjectionArray;

                PluginName = Plugin.Name;

                List<bool> CheckStatus = BFP.CheckStatus;
                bool CheckAll = BFP.CheckAll;

                //string XML = Plugin.ToXmlFromRequest(Request);
                //string[,] InjectionArray = FormatPlugin.XmlToArray(XML);
                //Scanner.CurrentScanner.BodyFormat = Plugin;
                IronUI.FillConfigureScanFormatDetails(XML, InjectionArray, CheckStatus, CheckAll, PluginName);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch(Exception Exp)
            {
                IronException.Report(string.Format("Error Parsing Body in Selected Format. Format Plugin - {0}", PluginName), Exp.Message, Exp.StackTrace);
                IronUI.ShowConfigureScanException("Error Parsing Body in Selected Format");
                Scanner.CurrentScanner.BodyFormat = new FormatPlugin();
                IronUI.UpdateScanBodyTabWithXmlArray();
            }
        }

        internal static void LoadScannerFromDDAndFillAutomatedScanningTab(object ScanJobIdObj)
        {
            int ScanJobId = (int)ScanJobIdObj;
            Scanner ScannerFromDb = null;
            try
            {
                ScannerFromDb = IronDB.GetScannerFromDB(ScanJobId);
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to load Request from Scan Queue DB", Exp.Message, Exp.StackTrace);
                IronUI.ShowConfigureScanException("Unable to load Request");
                return;
            }
            SetScannerFromDBToUiAfterProcessing(ScannerFromDb);
        }
        internal static void SetScannerFromDBToUiAfterProcessing(Scanner ScannerFromDb)
        {
            ScannerFromDb.OriginalRequest.Source = RequestSource.Scan;
            string[,] XmlInjectionPoints = new string[,] { };
            string XML = "";
            //try
            //{
            //    if (ScannerFromDb.Status.Equals("Not Started"))
            //    {
            //        if (ScannerFromDb.BodyFormat.Name.Length == 0 && ScannerFromDb.CustomInjectionPointStartMarker.Length == 0 && ScannerFromDb.CustomInjectionPointEndMarker.Length == 0 && !FormatPlugin.IsNormal(ScannerFromDb.OriginalRequest))
            //        {
            //            List<FormatPlugin> RightList = FormatPlugin.Get(ScannerFromDb.OriginalRequest);
            //            if (RightList.Count > 0)
            //            {
            //                ScannerFromDb.BodyFormat = RightList[0];
            //                XML = ScannerFromDb.BodyFormat.ToXmlFromRequest(ScannerFromDb.OriginalRequest);
            //                XmlInjectionPoints = FormatPlugin.XmlToArray(XML);
            //            }
            //        }
            //    }
            //}
            //catch (Exception Exp) { IronException.Report("Error guessing Request body type", Exp); }
            Scanner.CurrentScanner = ScannerFromDb;
            Scanner.CurrentScanID = ScannerFromDb.ID;

            IronUI.SetAutomatedScanningScanner(ScannerFromDb, XML, XmlInjectionPoints);
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

        #region CreateInjectionString
        //Old format (till v0.9.1.5)
        //public string GetInjectionString()
        //{
        //    StringBuilder Url = new StringBuilder("Url:"); Url.Append(GetStringFromInjectionList(URLInjections));
        //    StringBuilder Query = new StringBuilder("Query:"); Query.Append(GetStringFromInjectionParameters(QueryInjections));
        //    StringBuilder Body = new StringBuilder("Body:");
        //    if (this.BodyFormat.Name.Length > 0)
        //    {
        //        Body.Append(GetStringFromInjectionParameters(BodyXmlInjectionParameters, BodyXmlInjections));
        //    }
        //    else
        //    {
        //        Body.Append(GetStringFromInjectionParameters(BodyInjections));
        //    }
        //    StringBuilder Cookie = new StringBuilder("Cookie:"); Cookie.Append(GetStringFromInjectionParameters(CookieInjections));
        //    StringBuilder Headers = new StringBuilder("Headers:"); Headers.Append(GetStringFromInjectionParameters(HeadersInjections));
        //    StringBuilder FullInjectionString = new StringBuilder();
        //    FullInjectionString.AppendLine(Url.ToString());
        //    FullInjectionString.AppendLine(Query.ToString());
        //    FullInjectionString.AppendLine(Body.ToString());
        //    FullInjectionString.AppendLine(Cookie.ToString());
        //    FullInjectionString.AppendLine(Headers.ToString());
        //    return FullInjectionString.ToString();
        //}
        
        public string GetInjectionString()
        {
            StringBuilder Url = new StringBuilder("Url|"); Url.Append(GetStringFromInjectionList(URLInjections));
            StringBuilder Query = new StringBuilder("Query|"); Query.Append(GetStringFromInjectionParameters(QueryInjections));
            StringBuilder Body = new StringBuilder("Body|");
            if (CustomInjectionPointStartMarker.Length > 0 && CustomInjectionPointEndMarker.Length > 0)
            {
                Body.Append("CustomMarker|");
                Body.Append(GetStringFromInjectionMarker(CustomInjectionPointStartMarker, CustomInjectionPointEndMarker));
            }
            else if (this.BodyFormat.Name.Length > 0)
            {
                Body.Append("FormatPlugin|"); 
                Body.Append(GetStringFromInjectionParameters(BodyXmlInjectionParameters, BodyXmlInjections));
            }
            else
            {
                Body.Append("Normal|");
                Body.Append(GetStringFromInjectionParameters(BodyInjections)); 
            }
            StringBuilder Cookie = new StringBuilder("Cookie|"); Cookie.Append(GetStringFromInjectionParameters(CookieInjections));
            StringBuilder Headers = new StringBuilder("Headers|"); Headers.Append(GetStringFromInjectionParameters(HeadersInjections));
            StringBuilder Names = new StringBuilder("Names|"); Names.Append(GetStringFromInjectionParameters(ParameterNameInjections));
            StringBuilder FullInjectionString = new StringBuilder();
            FullInjectionString.AppendLine(Url.ToString());
            FullInjectionString.AppendLine(Query.ToString());
            FullInjectionString.AppendLine(Body.ToString());
            FullInjectionString.AppendLine(Cookie.ToString());
            FullInjectionString.AppendLine(Headers.ToString());
            FullInjectionString.AppendLine(Names.ToString());
            return FullInjectionString.ToString();
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
                //IS.Append(Name); IS.Append("="); - old format
                IS.Append(Tools.Base64Encode(Name)); IS.Append("-");
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

        //New format
        static string GetStringFromInjectionMarker(string StartMarker, string EndMarker)
        {
            StringBuilder IS = new StringBuilder();
            IS.Append(Tools.Base64Encode(StartMarker));
            IS.Append(":");
            IS.Append(Tools.Base64Encode(EndMarker));
            return IS.ToString();
        }
        #endregion

        #region ReadInjectionString
        internal void AbsorbInjectionString(string InjectionString)
        {
            if (InjectionString.StartsWith("Url:"))
                AbsorbOldFormatInjectionString(InjectionString);
            else
                AbsorbNewFormatInjectionString(InjectionString);
        }

        internal void AbsorbNewFormatInjectionString(string InjectionString)
        {
            string[] InjectionStringsArray = InjectionString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (InjectionStringsArray.Length != 6)
            {
                throw new Exception("Invalid Injection String");
            }
            this.URLInjections = GetListFromInjectionString(InjectionStringsArray[0].Substring(4));//Remove 'Url|'
            this.QueryInjections = GetParametersFromInjectionString(InjectionStringsArray[1].Substring(6));//Remove 'Query|'
            string BodyInjectionStringPart = InjectionStringsArray[2].Substring(5);
            if (BodyInjectionStringPart.StartsWith("CustomMarker|"))//Remove 'Body|'
            {
                string[] CustomMarkers = GetCustomMarkersFromInjectionString(BodyInjectionStringPart.Substring(13));//Remove 'CustomMarker|'
                this.CustomInjectionPointStartMarker = CustomMarkers[0];
                this.CustomInjectionPointEndMarker = CustomMarkers[1];
            }
            else if (BodyInjectionStringPart.StartsWith("FormatPlugin|"))
            {
                this.AbsorbFormatBodyParametersFromInjectionString(BodyInjectionStringPart.Substring(13));//Remove 'FormatPlugin|'
            }
            else
            {
                this.BodyInjections = GetParametersFromInjectionString(BodyInjectionStringPart.Substring(7));//Remove 'Normal|'
            }
            
            this.CookieInjections = GetParametersFromInjectionString(InjectionStringsArray[3].Substring(7));//Remove 'Cookie|'
            this.HeadersInjections = GetParametersFromInjectionString(InjectionStringsArray[4].Substring(8));//Remove 'Headers|'
            this.ParameterNameInjections = GetParametersFromInjectionString(InjectionStringsArray[5].Substring(6));//Remove 'Names|'
        }

        internal void AbsorbOldFormatInjectionString(string InjectionString)
        {
            string[] InjectionStringsArray = InjectionString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if(InjectionStringsArray.Length != 5)
            {
                throw new Exception("Invalid Injection String");
            }
            this.URLInjections = GetListFromInjectionString(InjectionStringsArray[0].Substring(4));//Remove 'Url:'
            this.QueryInjections = GetParametersFromOldFormatInjectionString(InjectionStringsArray[1].Substring(6));//Remove 'Query:'
            if (this.BodyFormat.Name.Length == 0)
            {
                this.BodyInjections = GetParametersFromOldFormatInjectionString(InjectionStringsArray[2].Substring(5));//Remove 'Body:'
            }
            else
            {
                this.AbsorbFormatBodyParametersFromInjectionString(InjectionStringsArray[2].Substring(5));//Remove 'Body:'
            }
            this.CookieInjections = GetParametersFromOldFormatInjectionString(InjectionStringsArray[3].Substring(7));//Remove 'Cookie:'
            this.HeadersInjections = GetParametersFromOldFormatInjectionString(InjectionStringsArray[4].Substring(8));//Remove 'Headers:'
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
                string[] ParameterParts = Parameter.Split(new string[]{"-"}, StringSplitOptions.RemoveEmptyEntries);
                if (ParameterParts.Length != 2) throw new Exception("Invalid Injection String");
                string[] SubParameterPositions = ParameterParts[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string SubParameterPosition in SubParameterPositions)
                {
                    try
                    {
                        InjectionParameters.Add(Tools.Base64Decode(ParameterParts[0]), Int32.Parse(SubParameterPosition));
                    }
                    catch
                    {
                        throw new Exception("Invalid Injection String");
                    }
                }
            }
            return InjectionParameters;
        }

        static InjectionParameters GetParametersFromOldFormatInjectionString(string InjectionString)
        {
            InjectionParameters InjectionParameters = new InjectionParameters();
            string[] Parameters = InjectionString.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string Parameter in Parameters)
            {
                string[] ParameterParts = Parameter.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
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

        static string[] GetCustomMarkersFromInjectionString(string InjectionString)
        {
            string[] EncodedMarkers = InjectionString.Split(':');
            return new string[] { Tools.Base64Decode(EncodedMarkers[0]), Tools.Base64Decode(EncodedMarkers[1])};
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
        #endregion

        #region ScanTrace
        public void StartTrace()
        {
            TraceMsg = new StringBuilder();
            TraceMsgXml = XmlWriter.Create(TraceMsg);
            TraceMsgXml.WriteStartDocument();
            TraceMsgXml.WriteStartElement("trace");
            TraceOverviewEntries = new List<string[]>();
            CurrentTraceRequestLogId = 0;
            RequestTraceMsg = "";
            TraceTitle = "";
            TraceTitleWeight = 0;
            this.TraceOverviewEntries.Add(new string[] { PreInjectionParameterValue, OriginalResponse.ID.ToString(), OriginalResponse.Code.ToString(), OriginalResponse.BodyLength.ToString(), OriginalResponse.ContentType, OriginalResponse.RoundTrip.ToString(), Tools.MD5(OriginalResponse.ToString()) });
        }
        public void RequestTrace(string Message)
        {
            if (this.RequestTraceMsg.Length > 0)
            {
                IronException.Report(string.Format("Invalid Trace Commands in {0} Plugin", this.CurrentPlugin), string.Format("RequestTrace() command called after RequestTrace() command. RequestTrace() command must be followed by a ResponseTrace() command before calling any other commands.\r\nThe message being logged was:\r\n{0}", Tools.EncodeForTrace(Message)));
                //TraceMsg.Append("<i<br>>");//Probably the ResponseTrace matching the last RequestTrace was not called
            }
            this.RequestTraceMsg = Tools.EncodeForTrace(Message);
        }

        public void ResponseTrace(string Message)
        {
            TraceMsgXml.WriteStartElement("type_b");
            
            TraceMsgXml.WriteStartElement("log_id"); TraceMsgXml.WriteValue(this.CurrentTraceRequestLogId); TraceMsgXml.WriteEndElement();
            TraceMsgXml.WriteStartElement("req"); TraceMsgXml.WriteValue(RequestTraceMsg); TraceMsgXml.WriteEndElement();
            TraceMsgXml.WriteStartElement("res"); TraceMsgXml.WriteValue(Tools.EncodeForTrace(Message)); TraceMsgXml.WriteEndElement();

            TraceMsgXml.WriteEndElement();

            //if (RequestTraceMsg.Length == 0 && Message.Length > 0)
            //{
            //    TraceMsg.Append("<i<cr>>This request was not traced. You forgot to call the RequestTrace method in your code.<i</cr>> ");
            //}
            //TraceMsg.Append(Tools.EncodeForTrace(Message));
            
            RequestTraceMsg = "";
        }

        public void Trace(string Message)
        {
            if (RequestTraceMsg.Length > 0)
            {
                IronException.Report(string.Format("Invalid Trace Commands in {0} Plugin", this.CurrentPlugin), string.Format("Trace() command called after RequestTrace() command. RequestTrace() command must be followed by a ResponseTrace() command before calling any other commands.\r\nThe message being logged was:\r\n{0}", Tools.EncodeForTrace(Message)));
            }
            
            TraceMsgXml.WriteStartElement("type_a"); TraceMsgXml.WriteValue(Tools.EncodeForTrace(Message)); TraceMsgXml.WriteEndElement();
            //TraceMsg.Append("<i<br>>");
            //TraceMsg.Append(Tools.EncodeForTrace(Message));
        }

        public void SetTraceTitle(string Title, int Weight)
        {
            if (Weight > TraceTitleWeight)
            {
                TraceTitle = Title;
                TraceTitleWeight = Weight;
            }
            else if (Weight == TraceTitleWeight)
            {
                if (TraceTitle.Length == 0 || TraceTitle == "-")
                {
                    TraceTitle = Title;
                }
                else
                {
                    TraceTitle = string.Format("{0} | {1}", TraceTitle, Title);
                }
            }
        }

        public void LogTrace()
        {
            this.LogTrace(TraceTitle);
        }

        public void LogTrace(string Title)
        {
            try
            {
                this.TraceMsgXml.WriteEndElement();
                this.TraceMsgXml.WriteEndDocument();
                this.TraceMsgXml.Close();
            }
            catch { }
            IronTrace IT = new IronTrace(this.ScanID, CurrentPlugin, this.CurrentSection, this.CurrentParameterName, Title, TraceMsg.ToString(), this.TraceOverviewEntries);
            IT.Report();
            this.TraceMsg = new StringBuilder();
            
            this.RequestTraceMsg = "";
            this.TraceTitle = "";
            this.TraceTitleWeight = 0;
        }

        public string GetTrace()
        {
            return TraceMsg.ToString();
        }
        #endregion

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
            List<int> Running = new List<int>(ActiveScanIDs);

            foreach (int ScanID in Running)
            {
                try
                { Scanner.ScanThreads[ScanID].Abort(); }
                catch { }
            }
            try
            {
                IronDB.UpdateScanStatus(ToStop, "Stopped");
            }catch{}
            if (IronUI.UI.CanShutdown) return;//If user has started shutdown of IronWASP then dont't update UI
            try
            {
                IronUI.UpdateScanQueueStatuses(ToStop, "Stopped");
            }
            catch
            {}
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


        internal int GetCustomInjectionPointsCount()
        {
            int FullCount = SplitAtCustomInjectionPoints().Count;
            if (Tools.IsEven(FullCount))
                return FullCount / 2;
            else
                return (FullCount - 1) / 2;
        }

        List<string> SplitAtCustomInjectionPoints()
        {
            if(this.BodyCustomInjectionParts.Count == 0)
            this.BodyCustomInjectionParts = SplitAtCustomInjectionPoints(this.OriginalRequest.BodyString,this.CustomInjectionPointStartMarker, this.CustomInjectionPointEndMarker);
            return this.BodyCustomInjectionParts;
        }

        string GetValueAtCustomInjectionPoint(int Position)
        {
            List<string> Parts = SplitAtCustomInjectionPoints();
            if (Position > GetCustomInjectionPointsCount()) return "";
            int Index = ((Position + 1) * 2) - 1;
            return Parts[Index];
        }

        string InjectAtCustomInjectionPoint(int Position, string Payload)
        {
            foreach (string[] Rule in this.CharacterEscapingRules)
            {
                try
                {
                    Payload = Payload.Replace(Rule[0], Rule[1]);
                }
                catch { }
            }
            List<string> Parts = new List<string>(SplitAtCustomInjectionPoints());
            if (Position > GetCustomInjectionPointsCount()) return "";
            int Index = ((Position + 1) * 2) - 1;
            Parts[Index] = Payload;
            StringBuilder InjectedContent = new StringBuilder();
            foreach(string Part in Parts)
            {
                InjectedContent.Append(Part);
            }
            return InjectedContent.ToString();
        }

        public static List<string> SplitAtCustomInjectionPoints(string Content, string StartMarker, string EndMarker)
        {
            if (StartMarker.Length == 0 || EndMarker.Length == 0) return new List<string>();
            List<string> Result = new List<string>();
            bool CheckFurther = true;
            int Pointer = 0;
            while (CheckFurther && Content.Length > Pointer)
            {
                int Start = Content.IndexOf(StartMarker, Pointer);
                int Stop = Content.IndexOf(EndMarker, Start + StartMarker.Length);
                if (Start == -1 || Stop == -1) CheckFurther = false;
                if (CheckFurther)
                {
                    Result.Add(Content.Substring(Pointer, Start - Pointer));
                    Result.Add(Content.Substring(Start + StartMarker.Length, Stop - (Start + StartMarker.Length)));
                }
                else
                {
                    Result.Add(Content.Substring(Pointer));
                }
                Pointer = Stop + EndMarker.Length;
            }
            return Result;
        }
    }
}
