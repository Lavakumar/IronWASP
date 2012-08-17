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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
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
    public class IronUpdater
    {
        static bool IsOn = true;
        static Queue<PluginResult> PluginResultQ = new Queue<PluginResult>();

        static Queue<Request> ScanRequestQ = new Queue<Request>();
        static Queue<Response> ScanResponseQ = new Queue<Response>();
        internal static Dictionary<int, int> ScanGridMap = new Dictionary<int, int>();

        static Queue<Request> ShellRequestQ = new Queue<Request>();
        static Queue<Response> ShellResponseQ = new Queue<Response>();
        internal static Dictionary<int, int> ShellGridMap = new Dictionary<int, int>();

        static Queue<Request> ProbeRequestQ = new Queue<Request>();
        static Queue<Response> ProbeResponseQ = new Queue<Response>();
        internal static Dictionary<int, int> ProbeGridMap = new Dictionary<int, int>();

        static Queue<Request> ProxyRequestQ = new Queue<Request>();
        static Queue<Response> ProxyResponseQ = new Queue<Response>();
        static Queue<Request> ProxyOriginalRequestQ = new Queue<Request>();
        static Queue<Response> ProxyOriginalResponseQ = new Queue<Response>();
        static Queue<Request> ProxyEditedRequestQ = new Queue<Request>();
        static Queue<Response> ProxyEditedResponseQ = new Queue<Response>();
        
        internal static Queue<IronTrace> Traces = new Queue<IronTrace>();
        internal static Queue<IronTrace> ScanTraces = new Queue<IronTrace>();
        
        internal static Dictionary<int, int> ProxyGridMap = new Dictionary<int, int>();

        internal static Dictionary<int, int> MTGridMap = new Dictionary<int, int>();

        internal static List<List<string>> Urls = new List<List<string>>();

        static int SleepTime = 2000;
        static Thread T;

        internal static void Start()
        {
            ThreadStart TS = new ThreadStart(IronUpdater.Run);
            IronUpdater.T = new Thread(TS);
            IronUpdater.T.Start();
        }
        static void Run()
        {
            int Counter = 0;
            while(IronUpdater.IsOn)
            {
                if (Counter == 5) Counter = 0;
                Thread.Sleep(IronUpdater.SleepTime);
                Counter++;

                try { UpdateProxyLogAndGrid(); }
                catch (Exception Exp) { IronException.Report("Error Updating Proxy Log & Grid", Exp.Message, Exp.StackTrace); }
                try 
                { 
                    if(Counter == 2 || Counter == 4) UpdatePluginResult(); 
                }
                catch (Exception Exp) { IronException.Report("Error Updating PluginResult", Exp.Message, Exp.StackTrace); }

                try { UpdateShellLogAndGrid(); }
                catch (Exception Exp) { IronException.Report("Error Updating Shell Log & Grid", Exp.Message, Exp.StackTrace); }
                try 
                {
                    if (Counter == 5) UpdateProbeLogAndGrid(); 
                }
                catch (Exception Exp) { IronException.Report("Error Updating Probe Log & Grid", Exp.Message, Exp.StackTrace); }
                try
                {
                    if (Counter == 5) UpdateScanLogAndGrid(); 
                }
                catch (Exception Exp) { IronException.Report("Error Updating Scan Log & Grid", Exp.Message, Exp.StackTrace); }
                try 
                {
                    if (Counter == 5) UpdateSiteMapTree(); 
                }
                catch (Exception Exp) { IronException.Report("Error Updating SiteMapTree", Exp.Message, Exp.StackTrace); }
                try { UpdateTraceLogAndGrid(); }
                catch (Exception Exp) { IronException.Report("Error Updating Trace Log & Grid", Exp.Message, Exp.StackTrace); }
                try
                {
                    if (Counter == 2 || Counter == 4) UpdateScanTraceLogAndGrid();
                }
                catch (Exception Exp) { IronException.Report("Error Updating ScanTrace Log & Grid", Exp.Message, Exp.StackTrace); }
                try
                {
                    if (Counter == 5) ThreadStore.CleanUp(); 
                }
                catch (Exception Exp) { IronException.Report("Error cleaning up ThreadStore", Exp.Message, Exp.StackTrace); }
            }
        }
        internal static void Stop()
        {
            IsOn = false;
            T.Abort();
        }
        internal static void AddPluginResult(PluginResult PR)
        {
            if (PR != null)
            {
                lock (PluginResultQ)
                {
                    PluginResultQ.Enqueue(PR);
                }
            }
        }
        static void UpdatePluginResult()
        {
            PluginResult[] DequedPluginResult;
            lock (PluginResultQ)
            {
                DequedPluginResult = PluginResultQ.ToArray();
                PluginResultQ.Clear();
            }
            if (DequedPluginResult == null) return;

            List<PluginResult> PRs = new List<PluginResult>();
            foreach (PluginResult PR in DequedPluginResult)
            {
                try
                {
                    foreach (Trigger T in PR.Triggers.GetTriggers())
                    {
                        if (T.Request != null)
                        {
                            T.Request.StoredHeadersString = T.Request.GetHeadersAsString();
                            if (T.Request.IsBinary) T.Request.StoredBinaryBodyString = T.Request.BinaryBodyString;
                        }
                        if (T.Response != null)
                        {
                            T.Response.StoredHeadersString = T.Response.GetHeadersAsString();
                            if (T.Response.IsBinary) T.Response.StoredBinaryBodyString = T.Response.BinaryBodyString;
                        }
                    }
                    PR.Id = Interlocked.Increment(ref Config.PluginResultCount);
                    PRs.Add(PR);
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }
            if (PRs.Count > 0)
            {
                IronDB.LogPluginResults(PRs);
                IronUI.UpdatePluginResultTree(PRs);
            }
        }

        internal static void AddProxyRequest(Request Request)
        {
            if (Request != null)
            {
                try
                {
                    Request ClonedRequest = Request.GetClone(true);
                    if (ClonedRequest != null)
                    {
                        lock (ProxyRequestQ)
                        {
                            ProxyRequestQ.Enqueue(ClonedRequest);
                        }
                    }
                    else
                    {
                        Tools.Trace("IronUpdater", "Null Proxy Request");
                    }
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Proxy Request for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        internal static void AddProxyResponse(Response Response)
        {
            if (Response != null)
            {
                try
                {
                    Response ClonedResponse = Response.GetClone(true);
                    if (ClonedResponse != null)
                    {
                        lock (ProxyResponseQ)
                        {
                            ProxyResponseQ.Enqueue(ClonedResponse);
                        }
                    }
                    else
                        Tools.Trace("IronUpdater", "Null Proxy Response");
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Proxy Response for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        internal static void AddProxyRequestsAfterEdit(Request OriginalRequest, Request EditedRequest)
        {
            if (OriginalRequest != null)
            {
                lock (ProxyOriginalRequestQ)
                {
                    ProxyOriginalRequestQ.Enqueue(OriginalRequest);
                }
            }
            if (ProxyEditedRequestQ != null)
            {
                lock (ProxyEditedRequestQ)
                {
                    ProxyEditedRequestQ.Enqueue(EditedRequest);
                }
            }
        }

        internal static void AddProxyResponsesAfterEdit(Response OriginalResponse, Response EditedResponse)
        {
            if (OriginalResponse != null)
            {
                lock (ProxyOriginalResponseQ)
                {
                    ProxyOriginalResponseQ.Enqueue(OriginalResponse);
                }
            }
            if (EditedResponse != null)
            {
                lock (ProxyEditedResponseQ)
                {
                    ProxyEditedResponseQ.Enqueue(EditedResponse);
                }
            }
        }
        
        static void UpdateProxyLogAndGrid()
        {
            Response[] DequedResponses;
            lock (ProxyResponseQ)
            {
                DequedResponses = ProxyResponseQ.ToArray();
                ProxyResponseQ.Clear();
            }
            
            List<Response> Responses = new List<Response>();            
            foreach(Response Res in DequedResponses)
            {
                try
                {
                    if (Res == null)
                    {
                        IronException.Report("Null Response DeQed from Proxy Response Q", "Null Response DeQed from Proxy Response Q");
                        continue;
                    }
                    Res.StoredHeadersString = Res.GetHeadersAsString();
                    if(Res.IsBinary) Res.StoredBinaryBodyString = Res.BinaryBodyString;
                    Responses.Add(Res);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Response for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            Response[] DequedOriginalResponses;
            List<Response> OriginalResponses = new List<Response>();
            lock (ProxyOriginalResponseQ)
            {
                DequedOriginalResponses = ProxyOriginalResponseQ.ToArray();
                ProxyOriginalResponseQ.Clear();
            }
            foreach(Response Res in DequedOriginalResponses)
            {
                try
                {
                    if (Res == null)
                    {
                        IronException.Report("Null Response DeQed from Original Proxy Response Q", "Null Response DeQed from Original Proxy Response Q");
                        continue;
                    }
                    Res.StoredHeadersString = Res.GetHeadersAsString();
                    if (Res.IsBinary) Res.StoredBinaryBodyString = Res.BinaryBodyString;
                    OriginalResponses.Add(Res);
                }
                catch (Exception Exp)
                { 
                    IronException.Report("Error preparing Original Response for UI & DB Update", Exp.Message, Exp.StackTrace); 
                }
            }
            Response[] DequedEditedResponses;
            List<Response> EditedResponses = new List<Response>();
            lock (ProxyEditedResponseQ)
            {
                DequedEditedResponses = ProxyEditedResponseQ.ToArray();
                ProxyEditedResponseQ.Clear();
            }
            foreach(Response Res in DequedEditedResponses)
            {
                try
                {
                    if (Res == null)
                    {
                        IronException.Report("Null Response DeQed from Edited Proxy Response Q", "Null Response DeQed from Edited Proxy Response Q");
                        continue;
                    }
                    Res.StoredHeadersString = Res.GetHeadersAsString();
                    if (Res.IsBinary) Res.StoredBinaryBodyString = Res.BinaryBodyString;
                    EditedResponses.Add(Res);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Edited Response for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }
            
            Request[] DequedRequests;
            List<Request> Requests = new List<Request>();
            lock (ProxyRequestQ)
            {
                DequedRequests = ProxyRequestQ.ToArray();
                ProxyRequestQ.Clear();
            }
            foreach(Request Req in DequedRequests)
            {
                try
                {
                    if (Req == null)
                    {
                        IronException.Report("Null Request DeQed from Proxy Request Q", "Null Request DeQed from Proxy Request Q");
                        continue;
                    }
                    Req.StoredFile = Req.File;
                    Req.StoredParameters = Req.GetParametersString();
                    Req.StoredHeadersString = Req.GetHeadersAsString();
                    if (Req.IsBinary) Req.StoredBinaryBodyString = Req.BinaryBodyString;
                    Urls.Add(GetUrlForList(Req));
                    Requests.Add(Req);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Proxy Request for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            Request[] DequedOriginalRequests;
            List<Request> OriginalRequests = new List<Request>();
            lock (ProxyOriginalRequestQ)
            {
                DequedOriginalRequests = ProxyOriginalRequestQ.ToArray();
                ProxyOriginalRequestQ.Clear();
            }
            foreach(Request Req in DequedOriginalRequests)
            {
                try
                {
                    if (Req == null)
                    {
                        IronException.Report("Null Request DeQed from Proxy Original Request Q", "Null Request DeQed from Proxy Original Request Q");
                        continue;
                    }
                    Req.StoredFile = Req.File;
                    Req.StoredParameters = Req.GetParametersString();
                    Req.StoredHeadersString = Req.GetHeadersAsString();
                    if (Req.IsBinary) Req.StoredBinaryBodyString = Req.BinaryBodyString;
                    Urls.Add(GetUrlForList(Req));
                    OriginalRequests.Add(Req);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Original Request for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            Request[] DequedEditedRequests;
            List<Request> EditedRequests = new List<Request>();
            lock (ProxyEditedRequestQ)
            {
                DequedEditedRequests = ProxyEditedRequestQ.ToArray();
                ProxyEditedRequestQ.Clear();
            }
            foreach(Request Req in DequedEditedRequests)
            {
                try
                {
                    if (Req == null)
                    {
                        IronException.Report("Null Request DeQed from Proxy Edited Request Q", "Null Request DeQed from Proxy Edited Request Q");
                        continue;
                    }
                    Req.StoredFile = Req.File;
                    Req.StoredParameters = Req.GetParametersString();
                    Req.StoredHeadersString = Req.GetHeadersAsString();
                    if (Req.IsBinary) Req.StoredBinaryBodyString = Req.BinaryBodyString;
                    Urls.Add(GetUrlForList(Req));
                    EditedRequests.Add(Req);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Edited Request for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            List<Session> IronSessions = new List<Session>();
            if (Requests.Count > 0 || Responses.Count > 0 || OriginalRequests.Count > 0 || OriginalResponses.Count > 0 || EditedRequests.Count > 0 || EditedResponses.Count > 0)
            {
                IronDB.LogProxyMessages(IronSessions, Requests, Responses, OriginalRequests, OriginalResponses, EditedRequests, EditedResponses);
            }
            if (Requests.Count > 0 | Responses.Count > 0)
            {
                IronUI.UpdateProxyLogGrid(Requests, Responses);
            }
        }

        public static void AddMTRequest(Session Sess)
        {

        }
        public static void AddMTResponse(Session Sess)
        {

        }

        internal static void AddShellRequest(Request Request)
        {
            if (Request != null)
            {
                try
                {
                    Request ClonedRequest = Request.GetClone(true);
                    if (ClonedRequest != null)
                    {
                        lock (ShellRequestQ)
                        {
                            ShellRequestQ.Enqueue(ClonedRequest);
                        }
                    }
                    else
                    {
                        Tools.Trace("IronUpdater", "Null Shell Request");
                    }
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Shell Request for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        internal static void AddShellResponse(Response Response)
        {
            if (Response != null)
            {
                try
                {
                    Response ClonedResponse = Response.GetClone(true);
                    if (ClonedResponse != null)
                    {
                        lock (ShellResponseQ)
                        {
                            ShellResponseQ.Enqueue(ClonedResponse);
                        }
                    }
                    else
                        Tools.Trace("IronUpdater", "Null Shell Response");
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Shell Response for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        static void UpdateShellLogAndGrid()
        {
            Response[] DequedResponses;
            lock (ShellResponseQ)
            {
                DequedResponses = ShellResponseQ.ToArray();
                ShellResponseQ.Clear();
            }
            List<Response> Responses = new List<Response>();
            foreach(Response Res in DequedResponses)
            {
                try
                {
                    if (Res == null)
                    {
                        IronException.Report("Null Response DeQed from Shell Response Q", "Null Response DeQed from Shell Response Q");
                        continue;
                    }
                    Res.StoredHeadersString = Res.GetHeadersAsString();
                    if (Res.IsBinary) Res.StoredBinaryBodyString = Res.BinaryBodyString;
                    Responses.Add(Res);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Shell Response for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            Request[] DequedRequests;
            lock (ShellRequestQ)
            {
                DequedRequests = ShellRequestQ.ToArray();
                ShellRequestQ.Clear();
            }
            List<Request> Requests = new List<Request>();
            foreach(Request Req in DequedRequests)
            {
                try
                {
                    if (Req == null)
                    {
                        IronException.Report("Null Request DeQed from Shell Request Q", "Null Request DeQed from Shell Request Q");
                        continue;
                    }
                    Req.StoredFile = Req.File;
                    Req.StoredParameters = Req.GetParametersString();
                    Req.StoredHeadersString = Req.GetHeadersAsString();
                    if (Req.IsBinary) Req.StoredBinaryBodyString = Req.BinaryBodyString;
                    Requests.Add(Req);

                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Shell Request for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            List<Session> IronSessions = new List<Session>();
            if (Requests.Count > 0 || Responses.Count > 0)
            {
                IronDB.LogShellMessages(IronSessions, Requests, Responses);
                IronUI.UpdateShellLogGrid(Requests, Responses);
            }
        }

        internal static void AddProbeRequest(Request Request)
        {
            if (Request != null)
            {
                try
                {
                    Request ClonedRequest = Request.GetClone(true);
                    if (ClonedRequest != null)
                    {
                        lock (ProbeRequestQ)
                        {
                            ProbeRequestQ.Enqueue(ClonedRequest);
                        }
                    }
                    else
                    {
                        Tools.Trace("IronUpdater", "Null Probe Request");
                    }
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Probe Request for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        internal static void AddProbeResponse(Response Response)
        {
            if (Response != null)
            {
                try
                {
                    Response ClonedResponse = Response.GetClone(true);
                    if (ClonedResponse != null)
                    {
                        lock (ProbeResponseQ)
                        {
                            ProbeResponseQ.Enqueue(ClonedResponse);
                        }
                    }
                    else
                        Tools.Trace("IronUpdater", "Null Probe Response");
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Probe Response for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        static void UpdateProbeLogAndGrid()
        {
            Response[] DequedResponses;
            lock (ProbeResponseQ)
            {
                DequedResponses = ProbeResponseQ.ToArray();
                ProbeResponseQ.Clear();
            }
            List<Response> Responses = new List<Response>();
            foreach(Response Res in DequedResponses)
            {
                try
                {
                    if (Res == null)
                    {
                        IronException.Report("Null Response DeQed from Probe Response Q", "Null Response DeQed from Probe Response Q");
                        continue;
                    }
                    Res.StoredHeadersString = Res.GetHeadersAsString();
                    if (Res.IsBinary) Res.StoredBinaryBodyString = Res.BinaryBodyString;
                    Responses.Add(Res);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Probe Response for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }
            
            Request[] DequedRequests;
            lock (ProbeRequestQ)
            {
                DequedRequests = ProbeRequestQ.ToArray();
                ProbeRequestQ.Clear();
            }
            List<Request> Requests = new List<Request>();
            foreach(Request Req in DequedRequests)
            {
                try
                {
                    if (Req == null)
                    {
                        IronException.Report("Null Request DeQed from Probe Request Q", "Null Request DeQed from Probe Request Q");
                        continue;
                    }
                    Req.StoredFile = Req.File;
                    Req.StoredParameters = Req.GetParametersString();
                    Req.StoredHeadersString = Req.GetHeadersAsString();
                    if (Req.IsBinary) Req.StoredBinaryBodyString = Req.BinaryBodyString;
                    Requests.Add(Req);

                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Probe Request for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            List<Session> IronSessions = new List<Session>();
            if (Requests.Count > 0 || Responses.Count > 0)
            {
                IronDB.LogProbeMessages(IronSessions, Requests, Responses);
                IronUI.UpdateProbeLogGrid(Requests, Responses);
            }
        }

        internal static void AddScanRequest(Request Request)
        {
            if (Request != null)
            {
                try
                {
                    Request ClonedRequest = Request.GetClone(true);
                    if (ClonedRequest != null)
                    {
                        lock (ScanRequestQ)
                        {
                            ScanRequestQ.Enqueue(ClonedRequest);
                        }
                    }
                    else
                    {
                        Tools.Trace("IronUpdater", "Null Scan Request");
                    }
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Scan Request for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        internal static void AddScanResponse(Response Response)
        {
            if (Response != null)
            {
                try
                {
                    Response ClonedResponse = Response.GetClone(true);
                    if (ClonedResponse != null)
                    {
                        lock (ScanResponseQ)
                        {
                            ScanResponseQ.Enqueue(ClonedResponse);
                        }
                    }
                    else
                        Tools.Trace("IronUpdater", "Null Scan Response");
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error adding Scan Response for updating", Exp.Message, Exp.StackTrace);
                }
            }
        }

        static void UpdateScanLogAndGrid()
        {
            Response[] DequedResponse;

            lock (ScanResponseQ)
            {
                DequedResponse = ScanResponseQ.ToArray();
                ScanResponseQ.Clear();
            }
            List<Response> Responses = new List<Response>();
            foreach(Response Res in DequedResponse)
            {
                try
                {
                    if (Res == null)
                    {
                        IronException.Report("Null Response DeQed from Scan Response Q", "Null Response DeQed from Scan Response Q");
                        continue;
                    }
                    Res.StoredHeadersString = Res.GetHeadersAsString();
                    if (Res.IsBinary) Res.StoredBinaryBodyString = Res.BinaryBodyString;
                    Responses.Add(Res);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Scan Response for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }
            
            Request[] DequedRequests;
            lock (ScanRequestQ)
            {
                DequedRequests = ScanRequestQ.ToArray();
                ScanRequestQ.Clear();
            }
            List<Request> Requests = new List<Request>();
            foreach(Request Req in DequedRequests)
            {
                try
                {
                    if (Req == null)
                    {
                        IronException.Report("Null Request DeQed from Scan Request Q", "Null Request DeQed from Scan Request Q");
                        continue;
                    }
                    Req.StoredFile = Req.File;
                    Req.StoredParameters = Req.GetParametersString();
                    Req.StoredHeadersString = Req.GetHeadersAsString();
                    if (Req.IsBinary) Req.StoredBinaryBodyString = Req.BinaryBodyString;
                    Requests.Add(Req);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error preparing Scan Request for UI & DB Update", Exp.Message, Exp.StackTrace);
                }
            }

            List<Session> IronSessions = new List<Session>();
            if (Requests.Count > 0 || Responses.Count > 0)
            {
                IronDB.LogScanMessages(IronSessions, Requests, Responses);
                IronUI.UpdateScanLogGrid(Requests, Responses);
            }
        }

        internal static void AddTrace(IronTrace Trace)
        {
            if (Trace != null)
            {
                lock (Traces)
                {
                    Traces.Enqueue(Trace);
                }
            }
        }

        static void UpdateTraceLogAndGrid()
        {
            IronTrace[] DequedTraces;
            lock (Traces)
            {
                DequedTraces = Traces.ToArray();
                Traces.Clear();
            }
            List<IronTrace> TraceList = new List<IronTrace>(DequedTraces);
            if (TraceList.Count > 0)
            {
                IronDB.LogTraces(TraceList);
                IronUI.UpdateTraceGrid(TraceList);
            }
        }

        internal static void AddScanTrace(IronTrace Trace)
        {
            if (Trace != null)
            {
                lock (ScanTraces)
                {
                    ScanTraces.Enqueue(Trace);
                }
            }
        }

        static void UpdateScanTraceLogAndGrid()
        {
            IronTrace[] DequedTraces;
            lock (ScanTraces)
            {
                DequedTraces = ScanTraces.ToArray();
                ScanTraces.Clear();
            }
            List<IronTrace> TraceList = new List<IronTrace>(DequedTraces);
            if (TraceList.Count > 0)
            {
                IronDB.LogScanTraces(TraceList);
                IronUI.UpdateScanTraceGrid(TraceList);
            }
        }

        internal static List<string> GetUrlForList(Request Req)
        {
            List<string> UrlParts = new List<string>();
            UrlParts.Add(Req.Host);
            UrlParts.AddRange(Req.UrlPathParts);
            UrlParts.Add("");
            if (Req.Query.Count > 0)
            {               
                UrlParts.Add("?" + Req.Query.GetQueryStringFromParameters());
            }
            return UrlParts;
        }

        internal static void AddToSiteMap(Request Req)
        {
            List<string> Url = GetUrlForList(Req);
            lock (Urls)
            {
                Urls.Add(Url);
            }
        }

        static void UpdateSiteMapTree()
        {
            if (Urls.Count > 0)
            {
                List<List<string>> UrlsToBeUpdated = new List<List<string>>();
                lock (Urls)
                {
                    UrlsToBeUpdated.AddRange(Urls);
                    Urls.Clear();
                }
                IronUI.UpdateSitemapTree(UrlsToBeUpdated);
            }
        }
    }
}
