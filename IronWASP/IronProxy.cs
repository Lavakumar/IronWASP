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
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace IronWASP
{
    internal class IronProxy
    {
        internal static Dictionary<string, Session> InterceptedSessions = new Dictionary<string, Session>();
        internal static bool ManualTamperingFree = true;
        internal static Queue<string> SessionsQ = new Queue<string>();
        internal static Session CurrentSession;
        //internal static Session StoredSession;
        
        internal static int Port = 8080;
        internal static bool LoopBackOnly = true;
        internal static bool SystemProxy = false;

        internal static bool UseUpstreamProxy = false;
        internal static string UpstreamProxyIP = "";
        internal static int UpstreamProxyPort = 0;

        static bool EventHandlersAssigned = false;

        //Traffic Interception Rules
        internal static bool InterceptRequest = false;
        internal static bool InterceptResponse = false;

        internal static bool InterceptGET = true;
        internal static bool InterceptPOST = true;
        internal static bool InterceptOtherMethods = true;

        internal static bool InterceptHTML = true;
        internal static bool InterceptJS = true;
        internal static bool InterceptCSS = true;
        internal static bool InterceptXML = true;
        internal static bool InterceptJSON = true;
        internal static bool InterceptOtherText = true;
        internal static bool InterceptImg = true;
        internal static bool InterceptOtherBinary = true;

        internal static bool Intercept200 = true;
        internal static bool Intercept2xx = true;
        internal static bool Intercept301_2 = true;
        internal static bool Intercept3xx = true;
        internal static bool Intercept403 = true;
        internal static bool Intercept4xx = true;
        internal static bool Intercept500 = true;
        internal static bool Intercept5xx = true;

        internal static bool InterceptCheckFileExtensions = false;
        internal static bool InterceptCheckFileExtensionsPlus = false;
        internal static List<string> InterceptFileExtensions = new List<string>();
        internal static bool InterceptCheckFileExtensionsMinus = false;
        internal static List<string> DontInterceptFileExtensions = new List<string>();

        internal static bool InterceptCheckHostNames = false;
        internal static bool InterceptCheckHostNamesPlus = false;
        internal static List<string> InterceptHostNames = new List<string>();
        internal static bool InterceptCheckHostNamesMinus = false;
        internal static List<string> DontInterceptHostNames = new List<string>();

        internal static bool InterceptCheckRequestWithKeyword = false;
        internal static bool InterceptCheckRequestWithKeywordPlus = false;
        internal static string InterceptRequestWithKeyword = "";
        internal static bool InterceptCheckRequestWithKeywordMinus = false;
        internal static string DontInterceptRequestWithKeyword = "";

        internal static bool InterceptCheckResponseWithKeyword = false;
        internal static bool InterceptCheckResponseWithKeywordPlus = false;
        internal static string InterceptResponseWithKeyword = "";
        internal static bool InterceptCheckResponseWithKeywordMinus = false;
        internal static string DontInterceptResponseWithKeyword = "";

        internal static bool RequestRulesOnResponse = true;

        //Log Display Filter
        internal static bool DisplayGET = true;
        internal static bool DisplayPOST = true;
        internal static bool DisplayOtherMethods = true;

        internal static bool DisplayHTML = true;
        internal static bool DisplayJS = true;
        internal static bool DisplayCSS = true;
        internal static bool DisplayXML = true;
        internal static bool DisplayJSON = true;
        internal static bool DisplayOtherText = true;
        internal static bool DisplayImg = false;
        internal static bool DisplayOtherBinary = false;

        internal static bool Display200 = true;
        internal static bool Display2xx = true;
        internal static bool Display301_2 = true;
        internal static bool Display3xx = true;
        internal static bool Display403 = true;
        internal static bool Display4xx = true;
        internal static bool Display500 = true;
        internal static bool Display5xx = true;

        internal static bool DisplayCheckFileExtensions = false;
        internal static bool DisplayCheckFileExtensionsPlus = false;
        internal static List<string> DisplayFileExtensions = new List<string>();
        internal static bool DisplayCheckFileExtensionsMinus = false;
        internal static List<string> DontDisplayFileExtensions = new List<string>();

        internal static bool DisplayCheckHostNames = false;
        internal static bool DisplayCheckHostNamesPlus = false;
        internal static List<string> DisplayHostNames = new List<string>();
        internal static bool DisplayCheckHostNamesMinus = false;
        internal static List<string> DontDisplayHostNames = new List<string>();

        //To check intercepted message editing
        internal static bool RequestChanged = false;
        internal static bool RequestHeaderChanged = false;
        internal static bool RequestBodyChanged = false;
        internal static bool RequestQueryParametersChanged = false;
        internal static bool RequestBodyParametersChanged = false;
        internal static bool RequestCookieParametersChanged = false;
        internal static bool RequestHeaderParametersChanged = false;
        internal static bool ResponseChanged = false;
        internal static bool ResponseHeaderChanged = false;
        internal static bool ResponseBodyChanged = false;

        internal static Thread RequestFormatThread;
        internal static Thread ResponseFormatThread;

        internal static void Start()
        {
            if (!EventHandlersAssigned)
            {
                Fiddler.FiddlerApplication.AfterSessionComplete += delegate(Fiddler.Session Sess)
                {
                    IronProxy.AfterSessionComplete(Sess);
                };

                Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session Sess)
                {
                    IronProxy.BeforeRequest(Sess);
                };

                Fiddler.FiddlerApplication.BeforeResponse += delegate(Fiddler.Session Sess)
                {
                    IronProxy.BeforeResponse(Sess);
                };

                Fiddler.FiddlerApplication.OverrideServerCertificateValidation += delegate(Fiddler.Session Sess, string sExpectedCN, X509Certificate ServerCertificate, X509Chain ServerCertificateChain, SslPolicyErrors sslPolicyErrors, out bool bTreatCertificateAsValid)
                {
                    string SSLError = sslPolicyErrors.ToString();
                    if (!SSLError.Equals("None"))
                    {
                        string PluginName = "Internal SSL Checker";
                        string Signature = string.Format("SSLCertificateChecker|{0}|{1}|{2}", new object[] { Sess.host, Sess.port.ToString(), sslPolicyErrors.ToString() });
                        if (PluginResult.IsSignatureUnique(PluginName, Sess.host, PluginResultType.Vulnerability, Signature))
                        {
                            PluginResult PR = new PluginResult(Sess.host);
                            PR.Plugin = PluginName;
                            PR.Severity = PluginResultSeverity.Medium;
                            PR.Confidence = PluginResultConfidence.High;
                            PR.Title = string.Format("SSL Certificate Error for {0}:{1} ", new object[] { Sess.host, Sess.port.ToString() });
                            PR.Summary = string.Format("The remote server running Host: {0} and Port: {1} returned an invalid SSL certificate.<i<br>> <i<h>>Error:<i</h>> {2}. <i<br>> <i<h>>Certificate Details:<i</h>> {3}", new object[] { Sess.host, Sess.port.ToString(), sslPolicyErrors.ToString(), ServerCertificate.Subject });
                            PR.Signature = Signature;
                            PR.Report();
                        }
                        Sess.oFlags.Add("IronFlag-SslError", "Yes");
                        bTreatCertificateAsValid = false;
                        return false;
                    }
                    else
                    {
                        bTreatCertificateAsValid = true;
                        return true;
                    }                    
                };

                Fiddler.FiddlerApplication.OnNotification += delegate(object Sender, Fiddler.NotificationEventArgs Args)
                {
                    if (Args.NotifyString.Contains("Unable to bind to port"))
                    {
                        IronProxy.Stop();
                        IronException.Report("Proxy could not be stared", "Listening Proxy could not be started. Likely reason could be the use of the port by another process","");
                        IronUI.ShowProxyStoppedError("Proxy Not Started! All features depend on the proxy, start proxy to activate them.");
                    }
                };

                EventHandlersAssigned = true;
            }

            Fiddler.CONFIG.IgnoreServerCertErrors = true;
            IronUI.UpdateProxyStatusInConfigPanel(true);
            if (IronProxy.LoopBackOnly)
            {
                Fiddler.FiddlerApplication.Startup(IronProxy.Port, Fiddler.FiddlerCoreStartupFlags.DecryptSSL);
            }
            else
            {
                Fiddler.FiddlerApplication.Startup(IronProxy.Port, Fiddler.FiddlerCoreStartupFlags.DecryptSSL | Fiddler.FiddlerCoreStartupFlags.AllowRemoteClients);
            }

        }

        internal static void Stop()
        {
            try
            {
                Fiddler.FiddlerApplication.Shutdown();
            }
            catch { }
            IronUI.UpdateProxyStatusInConfigPanel(false);
        }

        internal static void AfterSessionComplete(Fiddler.Session Sess)
        {
            if (Sess.HTTPMethodIs("Connect")) return;
            if (Sess.isFlagSet(Fiddler.SessionFlags.RequestGeneratedByFiddler))
            {
                Session IrSe;
                try
                {
                    IrSe = new Session(Sess);
                }
                catch(Exception Exp)
                {
                    IronException.Report("Error handling Response", Exp.Message, Exp.StackTrace);
                    return;
                }
                if (IrSe == null)
                {
                    IronException.Report("Error handling Response", "", "");
                    return;
                }
                if (IrSe.FiddlerSession == null)
                {
                    IronException.Report("Error handling Response", "", "");
                    return;
                }
                if (IrSe.Response == null)
                {
                    IronException.Report("Error handling Response", "", "");
                    return;
                }
                
                if (Sess.oFlags["IronFlag-BuiltBy"] == "ManualTestingSection")
                {
                    try
                    {
                        ManualTesting.HandleResponse(IrSe);
                    }
                    catch(Exception Exp)
                    {
                        IronException.Report("Error handling 'Manual Testing' Response", Exp.Message, Exp.StackTrace);
                    }
                }
                else if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Shell") || Sess.oFlags["IronFlag-BuiltBy"].Equals("Scan") || Sess.oFlags["IronFlag-BuiltBy"].Equals("Probe") || Sess.oFlags["IronFlag-BuiltBy"].Equals("Stealth"))
                {
                    try
                    {
                        string DictID = "";
                        if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Shell"))
                        {
                            try
                            {
                                IronUpdater.AddShellResponse(IrSe.Response);
                                DictID = Sess.oFlags["IronFlag-ID"] + "-Shell";
                            }
                            catch (Exception Exp)
                            {
                                IronException.Report("Error handling 'Scripting Shell' Response", Exp.Message, Exp.StackTrace);
                            }
                        }
                        else if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Probe"))
                        {
                            try
                            {
                                IronUpdater.AddProbeResponse(IrSe.Response);
                                DictID = Sess.oFlags["IronFlag-ID"] + "-Probe";
                            }
                            catch (Exception Exp)
                            {
                                IronException.Report("Error handling 'Probe' Response", Exp.Message, Exp.StackTrace);
                            }
                        }
                        else if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Stealth"))
                        {
                            try
                            {
                                DictID = Sess.oFlags["IronFlag-ID"] + "-Stealth";
                            }
                            catch (Exception Exp)
                            {
                                IronException.Report("Error handling 'Stealth' Response", Exp.Message, Exp.StackTrace);
                            }
                        }
                        else
                        {
                            try
                            {
                                IronUpdater.AddScanResponse(IrSe.Response);
                                DictID = Sess.oFlags["IronFlag-ID"] + "-Scan";
                            }
                            catch (Exception Exp)
                            {
                                IronException.Report("Error handling 'Automated Scanning' Response", Exp.Message, Exp.StackTrace);
                            }
                        }
                        Config.APIResponseDict[DictID].SetResponse(IrSe.Response);
                        Config.APIResponseDict[DictID].MSR.Set();       
                    }
                    catch (Exception MainExp)
                    {
                        IronException.Report("Error handling 'Scripting Shell/Automated Scanning/Probe' Response", MainExp.Message, MainExp.StackTrace);
                    }
                }
            }
        }

        internal static void BeforeRequest(Fiddler.Session Sess)
        {
            if (Sess.HTTPMethodIs("Connect"))
            {
                if (IronProxy.UseUpstreamProxy)
                {
                    string UpstreamProxyString = string.Format("{0}:{1}", IronProxy.UpstreamProxyIP, IronProxy.UpstreamProxyPort.ToString());
                    Sess.oFlags.Add("x-overrideGateway", UpstreamProxyString);
                }
                if (Config.HasFiddlerFlags)
                {
                    string[,] Flags = Config.GetFiddlerFlags();
                    for (int i = 0; i < Flags.GetLength(0); i++)
                    {
                        Sess.oFlags.Add(Flags[i, 0], Flags[i, 1]);
                    }
                }
                return;
            }
            if(Sess.oFlags.ContainsKey("IronFlag-BuiltBy"))
            {
                if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Stealth"))
                {
                    if (IronProxy.UseUpstreamProxy)
                    {
                        string UpstreamProxyString = string.Format("{0}:{1}", IronProxy.UpstreamProxyIP, IronProxy.UpstreamProxyPort.ToString());
                        Sess.oFlags.Add("x-overrideGateway", UpstreamProxyString);
                    }
                    if (Config.HasFiddlerFlags)
                    {
                        string[,] Flags = Config.GetFiddlerFlags();
                        for (int i = 0; i < Flags.GetLength(0); i++)
                        {
                            Sess.oFlags.Add(Flags[i, 0], Flags[i, 1]);
                        }
                    }
                    return;
                }
            }
            Session IrSe;
            try
            {
                IrSe = new Session(Sess);
            }
            catch(Exception Exp)
            {
                IronException.Report("Error reading Request", Exp.Message, Exp.StackTrace);
                return;
            }
            if (IrSe == null)
            {
                IronException.Report("Error reading Request", "", "");
                return;
            }
            if (IrSe.Request == null)
            {
                IronException.Report("Error reading Request", "", "");
                return;
            }
            if (IrSe.FiddlerSession == null)
            {
                IronException.Report("Error reading Request", "", "");
                return;
            }

            //Needs to be turned on to read the response body
            IrSe.FiddlerSession.bBufferResponse = true;
            
            IrSe.Request.TimeObject = DateTime.Now;
            if (Sess.oFlags.ContainsKey("IronFlag-Ticks"))
            {
                IrSe.FiddlerSession.oFlags["IronFlag-Ticks"] = IrSe.Request.TimeObject.Ticks.ToString();
            }
            else
            {
                IrSe.FiddlerSession.oFlags.Add("IronFlag-Ticks", IrSe.Request.TimeObject.Ticks.ToString());
            }
            try
            {
                Session ClonedIronSessionWithRequest = IrSe.GetClone();
                if (ClonedIronSessionWithRequest != null && ClonedIronSessionWithRequest.Request != null)
                    PassiveChecker.AddToCheckRequest(ClonedIronSessionWithRequest);
                else
                    IronException.Report("IronSession Request Couldn't be cloned at ID - " + IrSe.ID.ToString(),"","");
            }
            catch(Exception Exp)
            {
                IronException.Report("Error Cloning IronSession in BeforeRequest", Exp.Message, Exp.StackTrace);
            }

            try
            {
                PluginStore.RunAllPassivePluginsBeforeRequestInterception(IrSe);
            }
            catch (Exception Exp)
            {
                IronException.Report("Error running 'BeforeInterception' Passive plugins on Request", Exp.Message, Exp.StackTrace);
            }


            if (!IrSe.FiddlerSession.isFlagSet(Fiddler.SessionFlags.RequestGeneratedByFiddler))
            {
                IrSe.ID = Interlocked.Increment(ref Config.ProxyRequestsCount);
                IronUpdater.AddProxyRequest(IrSe.Request);
                if(CanIntercept(IrSe.Request))
                {
                    IrSe.MSR = new ManualResetEvent(false);
                    InterceptedSessions.Add(IrSe.ID + "-Request", IrSe);
                    IrSe.FiddlerSession.state = Fiddler.SessionStates.HandTamperRequest;
                    IronUI.SendSessionToProxy(IrSe);
                    InterceptedSessions[IrSe.ID + "-Request"].MSR.WaitOne();
                    InterceptedSessions.Remove(IrSe.ID + "-Request");
                }
                else
                {
                    IrSe.FiddlerSession.state = Fiddler.SessionStates.AutoTamperRequestBefore;
                }
            }
            else
            {
                if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Shell"))
                {
                    IronUpdater.AddShellRequest(IrSe.Request);
                }
                else if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Scan"))
                {
                    IronUpdater.AddScanRequest(IrSe.Request);
                }
                else if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Probe"))
                {
                    IronUpdater.AddProbeRequest(IrSe.Request);
                }
            }


            try
            {
                PluginStore.RunAllPassivePluginsAfterRequestInterception(IrSe);
            }
            catch (Exception Exp)
            {
                IronException.Report("Error running 'AfterInterception' Passive plugins on Request", Exp.Message, Exp.StackTrace);
            }


            if (IronProxy.UseUpstreamProxy)
            {
                string UpstreamProxyString = string.Format("{0}:{1}", IronProxy.UpstreamProxyIP, IronProxy.UpstreamProxyPort.ToString());
                IrSe.FiddlerSession.oFlags.Add("x-overrideGateway", UpstreamProxyString);
            }
            if (Config.HasFiddlerFlags)
            {
                string[,] Flags = Config.GetFiddlerFlags();
                for (int i = 0; i < Flags.GetLength(0); i++)
                {
                    IrSe.FiddlerSession.oFlags.Add(Flags[i, 0], Flags[i, 1]);
                }
            }
        }

        internal static void BeforeResponse(Fiddler.Session Sess)
        {
            if (Sess.HTTPMethodIs("Connect")) return;
            if (Sess.oFlags.ContainsKey("IronFlag-BuiltBy"))
            {
                if (Sess.oFlags["IronFlag-BuiltBy"].Equals("Stealth")) return;
            }
            Session IrSe;
            try
            {
                Sess.utilDecodeResponse();
                IrSe = new Session(Sess);
            }
            catch(Exception Exp)
            {
                IronException.Report("Error reading Response", Exp.Message, Exp.StackTrace);
                return;
            }
            if (IrSe == null)
            {
                IronException.Report("Error reading Response", "", "");
                return;
            }
            if (IrSe.Response == null)
            {
                IronException.Report("Error reading Response", "", "");
                return;
            }
            if (IrSe.FiddlerSession == null)
            {
                IronException.Report("Error reading Response", "", "");
                return;
            }
            long TTL = DateTime.Now.Ticks - IrSe.Request.TimeObject.Ticks;
            IrSe.Response.TTL = (int)(TTL / 10000);
            if (Sess.oFlags.ContainsKey("IronFlag-TTL"))
            {
                IrSe.FiddlerSession.oFlags["IronFlag-TTL"] = IrSe.Response.TTL.ToString();
            }
            else
            {
                IrSe.FiddlerSession.oFlags.Add("IronFlag-TTL", IrSe.Response.TTL.ToString());
            }
            try
            {
                Session ClonedIronSessionWithResponse = IrSe.GetClone();
                if (ClonedIronSessionWithResponse != null && ClonedIronSessionWithResponse.Response != null)
                {
                    PassiveChecker.AddToCheckResponse(ClonedIronSessionWithResponse);
                }
                else
                    IronException.Report("IronSession with Response Couldn't be cloned at ID - " + IrSe.ID.ToString(), "", "");
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Cloning IronSession in BeforeRequest", Exp.Message, Exp.StackTrace);
            }


            if (!IrSe.FiddlerSession.isFlagSet(Fiddler.SessionFlags.RequestGeneratedByFiddler))
            {
                IronUpdater.AddProxyResponse(IrSe.Response);
            }


            try
            {
                PluginStore.RunAllPassivePluginsBeforeResponseInterception(IrSe);
            }
            catch(Exception Exp)
            {
                IronException.Report("Error running 'BeforeInterception' Passive plugins on Response", Exp.Message, Exp.StackTrace);
            }


            if (!IrSe.FiddlerSession.isFlagSet(Fiddler.SessionFlags.RequestGeneratedByFiddler))
            {
                IrSe.Response.Host = IrSe.Request.Host;
                if(CanIntercept(IrSe.Response, IrSe.Request))
                {
                    IrSe.MSR = new ManualResetEvent(false);
                    IrSe.FiddlerSession.state = Fiddler.SessionStates.HandTamperResponse;
                    InterceptedSessions.Add(IrSe.ID + "-Response", IrSe);
                    IronUI.SendSessionToProxy(IrSe);
                    InterceptedSessions[IrSe.ID + "-Response"].MSR.WaitOne();
                    InterceptedSessions.Remove(IrSe.ID + "-Response");
                }
                else
                {
                    IrSe.FiddlerSession.state = Fiddler.SessionStates.AutoTamperResponseBefore;
                }
            }


            try
            {
                PluginStore.RunAllPassivePluginsAfterResponseInterception(IrSe);
            }
            catch(Exception Exp)
            {
                IronException.Report("Error running 'AfterInterception' Passive plugins on Response", Exp.Message, Exp.StackTrace);
            }
        }

        internal static void UpdateCurrentSessionWithNewRequestHeader(string HeaderString)
        {
            string NewRequestHeaders = HeaderString.TrimEnd(new char[]{'\r','\n'});
            NewRequestHeaders += "\r\n\r\n";
            IronProxy.CurrentSession.Request = new Request(NewRequestHeaders, IronProxy.CurrentSession.Request.SSL, false);
            IronProxy.CurrentSession.Request.ID = IronProxy.CurrentSession.OriginalRequest.ID;
            byte[] OldBody = new byte[IronProxy.CurrentSession.OriginalRequest.BodyArray.Length];
            IronProxy.CurrentSession.OriginalRequest.BodyArray.CopyTo(OldBody, 0);
            IronProxy.CurrentSession.Request.BodyArray = OldBody;
            IronProxy.CurrentSession.FiddlerSession.oRequest.headers.AssignFromString(IronProxy.CurrentSession.Request.GetHeadersAsString());
        }

        internal static void UpdateFiddlerSessionWithNewRequestHeader()
        {
            IronProxy.CurrentSession.FiddlerSession.oRequest.headers.AssignFromString(IronProxy.CurrentSession.Request.GetHeadersAsString());
        }

        internal static void UpdateCurrentSessionWithNewRequestBodyText(string BodyString)
        {
            if (IronProxy.CurrentSession.Request.IsBinary)
            {
                IronProxy.CurrentSession.Request.BodyArray = Encoding.UTF8.GetBytes(BodyString);
            }
            else
            {
                IronProxy.CurrentSession.Request.BodyString = BodyString;
            }
            IronProxy.CurrentSession.FiddlerSession.utilSetRequestBody(IronProxy.CurrentSession.Request.BodyString);
        }

        internal static void UpdateFiddlerSessionWithNewRequestBody()
        {
            IronProxy.CurrentSession.FiddlerSession.utilSetRequestBody(IronProxy.CurrentSession.Request.BodyString);
        }

        internal static void UpdateFiddlerSessionWithNewRequest()
        {
            IronProxy.CurrentSession.FiddlerSession.oRequest.headers.AssignFromString(IronProxy.CurrentSession.Request.GetHeadersAsString());
            IronProxy.CurrentSession.FiddlerSession.requestBodyBytes = IronProxy.CurrentSession.Request.BodyArray;
        }

        internal static void UpdateCurrentSessionWithNewResponseHeader(string HeaderString)
        {
            string NewResponseHeaders = HeaderString.TrimEnd(new char[]{'\r','\n'});
            NewResponseHeaders += "\r\n\r\n";
            IronProxy.CurrentSession.Response = new Response(NewResponseHeaders);
            IronProxy.CurrentSession.Response.ID = IronProxy.CurrentSession.OriginalResponse.ID;
            IronProxy.CurrentSession.Response.BodyArray = new byte[IronProxy.CurrentSession.OriginalResponse.BodyArray.Length];
            IronProxy.CurrentSession.OriginalResponse.BodyArray.CopyTo(IronProxy.CurrentSession.Response.BodyArray, 0);
            IronProxy.CurrentSession.FiddlerSession.oResponse.headers.AssignFromString(IronProxy.CurrentSession.Response.GetHeadersAsString());
        }

        internal static void UpdateCurrentSessionWithNewResponseBodyText(string BodyString)
        {
            IronProxy.CurrentSession.Response.BodyString = BodyString;
            IronProxy.CurrentSession.FiddlerSession.utilSetResponseBody(IronProxy.CurrentSession.Response.BodyString);
        }

        internal static void UpdateFiddlerSessionWithNewResponse()
        {
            IronProxy.CurrentSession.FiddlerSession.oResponse.headers.AssignFromString(IronProxy.CurrentSession.Response.GetHeadersAsString());
            IronProxy.CurrentSession.FiddlerSession.responseBodyBytes = IronProxy.CurrentSession.Response.BodyArray;
        }

        internal static void ForwardInterceptedMessage()
        {
            if (IronProxy.ManualTamperingFree == true)
            {
                return;
            }
            string ID = IronProxy.CurrentSession.ID.ToString();
            if (IronProxy.CurrentSession.FiddlerSession.state == Fiddler.SessionStates.HandTamperRequest)
            {
                ID = ID + "-Request";
                if (IronProxy.RequestChanged)
                {
                    Request ClonedRequest = IronProxy.CurrentSession.Request.GetClone(true);
                    IronUpdater.AddProxyRequestsAfterEdit(IronProxy.CurrentSession.OriginalRequest.GetClone(true), ClonedRequest);
                    IronUI.UpdateEditedProxyLogEntry(ClonedRequest);
                }
            }
            else
            {
                ID = ID + "-Response";
                if (IronProxy.ResponseChanged)
                {
                    Response ClonedResponse = IronProxy.CurrentSession.Response.GetClone(true);
                    IronUpdater.AddProxyResponsesAfterEdit(IronProxy.CurrentSession.OriginalResponse.GetClone(true), ClonedResponse);
                    IronUI.UpdateEditedProxyLogEntry(ClonedResponse);
                }
            }
            IronProxy.InterceptedSessions[ID].MSR.Set();
            IronUI.ResetProxyInterceptionFields();
            IronProxy.ManualTamperingFree = true;
            IronProxy.CurrentSession = null;
        }

        internal static void DropInterceptedMessage()
        {
            if (IronProxy.ManualTamperingFree == true)
            {
                return;
            }
            string ID = IronProxy.CurrentSession.ID.ToString();
            if (IronProxy.CurrentSession.FiddlerSession.state == Fiddler.SessionStates.HandTamperRequest)
            {
                ID = ID + "-Request";
                IronProxy.InterceptedSessions[ID].MSR.Set();
                IronProxy.CurrentSession.FiddlerSession.oRequest.FailSession(200, "OK", "Request Dropped By the User");
            }
            else
            {
                ID = ID + "-Response";
                IronProxy.CurrentSession.FiddlerSession.utilSetResponseBody("Response Dropped By the User");
                IronProxy.CurrentSession.FiddlerSession.responseCode = 200;
                IronProxy.InterceptedSessions[ID].MSR.Set();
            }
            IronUI.ResetProxyInterceptionFields();
            IronProxy.ManualTamperingFree = true;
            IronProxy.CurrentSession = null;
        }

        internal static void ClearRequestQueue()
        {
            List<string> IronProxySessionsQ = new List<string>();
            List<string> IronProxyRequestQ = new List<string>();
            List<string> IronProxyResponseQ = new List<string>();
            lock (IronProxy.SessionsQ)
            {
                IronProxySessionsQ = new List<string>(IronProxy.SessionsQ.ToArray());
                IronProxy.SessionsQ.Clear();
            }
            foreach (string SessionID in IronProxySessionsQ)
            {
                if (SessionID.EndsWith("-Request"))
                {
                    IronProxyRequestQ.Add(SessionID);
                }
                else
                {
                    IronProxyResponseQ.Add(SessionID);
                }
            }
            lock (IronProxy.InterceptedSessions)
            {
                foreach(string SessionID in IronProxyRequestQ)
                {
                    IronProxy.InterceptedSessions[SessionID].MSR.Set();
                }
            }
            lock (IronProxy.SessionsQ)
            {
                foreach (string SessionID in IronProxyResponseQ)
                {
                    IronProxy.SessionsQ.Enqueue(SessionID);
                }
            }
        }

        internal static void ClearResponseQueue()
        {
            List<string> IronProxySessionsQ = new List<string>();
            List<string> IronProxyRequestQ = new List<string>();
            List<string> IronProxyResponseQ = new List<string>();
            lock (IronProxy.SessionsQ)
            {
                IronProxySessionsQ = new List<string>(IronProxy.SessionsQ.ToArray());
                IronProxy.SessionsQ.Clear();
            }
            foreach (string SessionID in IronProxySessionsQ)
            {
                if (SessionID.EndsWith("-Response"))
                {
                    IronProxyResponseQ.Add(SessionID);
                }
                else
                {
                    IronProxyRequestQ.Add(SessionID);
                }
            }
            lock (IronProxy.InterceptedSessions)
            {
                foreach (string SessionID in IronProxyResponseQ)
                {
                    IronProxy.InterceptedSessions[SessionID].MSR.Set();
                }
            }
            lock (IronProxy.SessionsQ)
            {
                foreach (string SessionID in IronProxyRequestQ)
                {
                    IronProxy.SessionsQ.Enqueue(SessionID);
                }
            }
        }

        static bool CanIntercept(Request Req)
        {
            if (InterceptRequest)
            {
               return CanInterceptBasedOnFilter(Req);
            }
            else
            {
                return false;
            }
        }

        static bool CanInterceptBasedOnFilter(Request Req)
        {
            //Check Hostnames
            if (InterceptCheckHostNames)
            {
                if (InterceptCheckHostNamesPlus && InterceptHostNames.Count > 0)
                {
                    bool Match = false;
                    foreach (string HostName in InterceptHostNames)
                    {
                        if (Req.Host.Equals(HostName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Match = true;
                            break;
                        }
                    }
                    if (!Match)
                    {
                        return false;
                    }
                }
                if (InterceptCheckHostNamesMinus && DontInterceptHostNames.Count > 0)
                {
                    foreach (string HostName in DontInterceptHostNames)
                    {
                        if (Req.Host.Equals(HostName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return false;
                        }
                    }
                }
            }
                
            //Check Methods Rule
            if (!InterceptGET)
            {
                if (Req.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            if (!InterceptPOST)
            {
                if (Req.Method.Equals("POST", StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            if (!InterceptOtherMethods)
            {
                if (!(Req.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase) || Req.Method.Equals("POST", StringComparison.CurrentCultureIgnoreCase)))
                {
                    return false;
                }
            }

            //Check File Extensions
            Req.StoredFile = Req.File;
            if (InterceptCheckFileExtensions && Req.StoredFile.Length > 0)
            {
                if (InterceptCheckFileExtensionsPlus && InterceptFileExtensions.Count > 0)
                {
                    bool Match = false;
                    foreach (string File in InterceptFileExtensions)
                    {
                        if (Req.StoredFile.Equals(File, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Match = true;
                            break;
                        }
                    }
                    if (!Match)
                    {
                        return false;
                    }
                }
                if (InterceptCheckFileExtensionsMinus && DontInterceptFileExtensions.Count > 0)
                {
                    foreach (string File in DontInterceptFileExtensions)
                    {
                        if (Req.StoredFile.Equals(File, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return false;
                        }
                    }
                }
            }

            //Check Keyword
            if (InterceptCheckRequestWithKeyword)
            {
                if (InterceptCheckRequestWithKeywordPlus && InterceptRequestWithKeyword.Length > 0)
                {
                    if (!Req.ToString().Contains(InterceptRequestWithKeyword))
                    {
                        return false;
                    }
                }
                if (InterceptCheckRequestWithKeywordMinus && DontInterceptRequestWithKeyword.Length > 0)
                {
                    if (Req.ToString().Contains(DontInterceptRequestWithKeyword))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool CanIntercept(Response Res, Request Req)
        {
            if (InterceptResponse)
            {
                if (RequestRulesOnResponse)
                {
                    if (!CanInterceptBasedOnFilter(Req)) return false;
                }
                //Check Hostnames
                if (InterceptCheckHostNames)
                {
                    if (InterceptCheckHostNamesPlus && InterceptHostNames.Count > 0)
                    {
                        bool Match = false;
                        foreach (string HostName in InterceptHostNames)
                        {
                            if (Res.Host.Equals(HostName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Match = true;
                                break;
                            }
                        }
                        if (!Match)
                        {
                            return false;
                        }
                    }
                    if (InterceptCheckHostNamesMinus && DontInterceptHostNames.Count > 0)
                    {
                        foreach (string HostName in DontInterceptHostNames)
                        {
                            if (Res.Host.Equals(HostName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return false;
                            }
                        }
                    }
                }

                //Check Methods Rule
                int Code = Res.Code;
                switch (Code)
                {
                    case 200:
                        if(!Intercept200)
                            return false;
                        break;
                    case 301:
                    case 302:
                        if(!Intercept301_2)
                            return false;
                        break;
                    case 403:
                        if(!Intercept403)
                            return false;
                        break;
                    case 500:
                        if(!Intercept500)
                            return false;
                        break;
                    default:
                        if(Code > 199 && Code < 300)
                        {
                            if(!Intercept2xx)
                                return false;
                        }
                        else if(Code > 299  && Code < 400)
                        {
                            if(!Intercept3xx)
                                return false;
                        }
                        else if(Code > 399 && Code < 500)
                        {
                            if(!Intercept500)
                                return false;
                        }
                        else if(Code > 499 && Code < 600)
                        {
                            if(!Intercept5xx)
                                return false;
                        }
                        break;
                }

                if(Res.ContentType.ToLower().Contains("html"))
                {
                    if(!InterceptHTML) return false;
                }
                else if(Res.ContentType.ToLower().Contains("css"))
                {
                    if(!InterceptCSS) return false;
                }
                else if(Res.ContentType.ToLower().Contains("javascript"))
                {
                    if(!InterceptJS) return false;
                }
                else if(Res.ContentType.ToLower().Contains("xml"))
                {
                    if(!InterceptXML) return false;
                }
                else if (Res.ContentType.ToLower().Contains("json"))
                {
                    if (!InterceptJSON) return false;
                }
                else if(Res.ContentType.ToLower().Contains("text"))
                {
                    if(!InterceptOtherText) return false;
                }
                else if(Res.ContentType.ToLower().Contains("jpg") || Res.ContentType.ToLower().Contains("png") || Res.ContentType.ToLower().Contains("jpeg") || Res.ContentType.ToLower().Contains("gif") || Res.ContentType.ToLower().Contains("ico"))
                {
                    if(!InterceptImg) return false;
                }
                else
                {
                    if(!InterceptOtherBinary) return false;
                }
                
                //Check Keyword
                if (InterceptCheckResponseWithKeyword)
                {
                    if (InterceptCheckResponseWithKeywordPlus && InterceptResponseWithKeyword.Length > 0)
                    {
                        if (!Res.ToString().Contains(InterceptResponseWithKeyword))
                        {
                            return false;
                        }
                    }
                    if (InterceptCheckResponseWithKeywordMinus && DontInterceptResponseWithKeyword.Length > 0)
                    {
                        if (Res.ToString().Contains(DontInterceptResponseWithKeyword))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        internal static bool CanDisplayRowInLogDisplay(string Method, string Host, string FileExtension, int Code, string ContentType)
        {
            if (Method != null)
            {
                if (!DisplayGET)
                {
                    if (Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return false;
                    }
                }
                if (!DisplayPOST)
                {
                    if (Method.Equals("POST", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return false;
                    }
                }
                if (!DisplayOtherMethods)
                {
                    if (!(Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase) || Method.Equals("POST", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        return false;
                    }
                }
            }
            if (Host != null)
            {
                if (DisplayCheckHostNames)
                {
                    if (DisplayCheckHostNamesPlus && DisplayHostNames.Count > 0)
                    {
                        bool Match = false;
                        foreach (string HostName in DisplayHostNames)
                        {
                            if (Host.Equals(HostName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Match = true;
                                break;
                            }
                        }
                        if (!Match)
                        {
                            return false;
                        }
                    }
                    if (DisplayCheckHostNamesMinus && DontDisplayHostNames.Count > 0)
                    {
                        foreach (string HostName in DontDisplayHostNames)
                        {
                            if (Host.Equals(HostName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            if (FileExtension != null)
            {
                if (DisplayCheckFileExtensions && FileExtension.Length > 0)
                {
                    if (DisplayCheckFileExtensionsPlus && DisplayFileExtensions.Count > 0)
                    {
                        bool Match = false;
                        foreach (string File in DisplayFileExtensions)
                        {
                            if (FileExtension.Equals(File, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Match = true;
                                break;
                            }
                        }
                        if (!Match)
                        {
                            return false;
                        }
                    }
                    if (DisplayCheckFileExtensionsMinus && DontDisplayFileExtensions.Count > 0)
                    {
                        foreach (string File in DontDisplayFileExtensions)
                        {
                            if (FileExtension.Equals(File, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            if (Code > 0)
            {
                switch (Code)
                {
                    case 200:
                        if (!Display200)
                            return false;
                        break;
                    case 301:
                    case 302:
                        if (!Display301_2)
                            return false;
                        break;
                    case 403:
                        if (!Display403)
                            return false;
                        break;
                    case 500:
                        if (!Display500)
                            return false;
                        break;
                    default:
                        if (Code > 199 && Code < 300)
                        {
                            if (!Display2xx)
                                return false;
                        }
                        else if (Code > 299 && Code < 400)
                        {
                            if (!Display3xx)
                                return false;
                        }
                        else if (Code > 399 && Code < 500)
                        {
                            if (!Display500)
                                return false;
                        }
                        else if (Code > 499 && Code < 600)
                        {
                            if (!Display5xx)
                                return false;
                        }
                        break;
                }
            }
            if (ContentType != null)
            {
                if (ContentType.ToLower().Contains("html"))
                {
                    if (!DisplayHTML) return false;
                }
                else if (ContentType.ToLower().Contains("css"))
                {
                    if (!DisplayCSS) return false;
                }
                else if (ContentType.ToLower().Contains("javascript"))
                {
                    if (!DisplayJS) return false;
                }
                else if (ContentType.ToLower().Contains("xml"))
                {
                    if (!DisplayXML) return false;
                }
                else if (ContentType.ToLower().Contains("json"))
                {
                    if (!DisplayJSON) return false;
                }
                else if (ContentType.ToLower().Contains("text"))
                {
                    if (!DisplayOtherText) return false;
                }
                else if (ContentType.ToLower().Contains("jpg") || ContentType.ToLower().Contains("png") || ContentType.ToLower().Contains("jpeg") || ContentType.ToLower().Contains("gif") || ContentType.ToLower().Contains("ico"))
                {
                    if (!DisplayImg) return false;
                }
                else
                {
                    if (!DisplayOtherBinary) return false;
                }
            }
            return true;
        }

        internal static void ResetChangedStatus()
        {
            ResetNonParameterChangedStatus();
            ResetParametersChangedStatus();
            RequestChanged = false;
            ResponseChanged = false;
        }
        internal static void ResetNonParameterChangedStatus()
        {
            RequestHeaderChanged = false;
            RequestBodyChanged = false;
            ResponseHeaderChanged = false;
            ResponseBodyChanged = false;
        }
        internal static void ResetParametersChangedStatus()
        {
            RequestQueryParametersChanged = false;
            RequestBodyParametersChanged = false;
            RequestCookieParametersChanged = false;
            RequestHeaderParametersChanged = false;
        }

        internal static void StartDeSerializingRequestBody(Request Request, FormatPlugin Plugin)
        {
            BodyFormatParamters BFP = new BodyFormatParamters(Request, Plugin);
            RequestFormatThread = new Thread(IronProxy.DeSerializeRequestBody);
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

                string XML = Plugin.ToXmlFromRequest(Request);
                IronUI.FillProxyRequestFormatXML(XML);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Deserializing 'Proxy' Request using Format Plugin - " + PluginName, Exp.Message, Exp.StackTrace);
                IronUI.ShowProxyException("Error Deserializing");
            }
        }

        internal static void StartSerializingRequestBody(Request Request, FormatPlugin Plugin, string XML)
        {
            BodyFormatParamters BFP = new BodyFormatParamters(Request, Plugin, XML);
            RequestFormatThread = new Thread(IronProxy.SerializeRequestBody);
            RequestFormatThread.Start(BFP);
        }

        internal static void SerializeRequestBody(object BFPObject)
        {
            string PluginName = "";
            try
            {
                BodyFormatParamters BFP = (BodyFormatParamters)BFPObject;
                Request Request = BFP.Request;
                FormatPlugin Plugin = BFP.Plugin;
                PluginName = Plugin.Name;
                string XML = BFP.XML;

                Request = Plugin.ToRequestFromXml(Request, XML);
                IronProxy.CurrentSession.Request = Request;
                IronProxy.UpdateFiddlerSessionWithNewRequest();
                IronUI.FillProxyRequestWithNewRequestFromFormatXML(Request, PluginName);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Serializing 'Proxy' Request using Format Plugin - " + PluginName, Exp.Message, Exp.StackTrace);
                IronUI.ShowProxyException("Error Serializing");
            }
        }

        internal static void StartDeSerializingResponseBody(Response Response, FormatPlugin Plugin)
        {
            BodyFormatParamters BFP = new BodyFormatParamters(Response, Plugin);
            ResponseFormatThread = new Thread(IronProxy.DeSerializeResponseBody);
            ResponseFormatThread.Start(BFP);
        }

        internal static void DeSerializeResponseBody(object BFPObject)
        {
            string PluginName = "";
            try
            {
                BodyFormatParamters BFP = (BodyFormatParamters)BFPObject;
                Response Response = BFP.Response;
                FormatPlugin Plugin = BFP.Plugin;
                PluginName = Plugin.Name;

                string XML = Plugin.ToXmlFromResponse(Response);
                IronUI.FillProxyResponseFormatXML(XML);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Deserializing 'Proxy' Response using Format Plugin - " + PluginName, Exp.Message, Exp.StackTrace);
                IronUI.ShowProxyException("Error Deserializing");
            }
        }

        internal static void StartSerializingResponseBody(Response Response, FormatPlugin Plugin, string XML)
        {
            BodyFormatParamters BFP = new BodyFormatParamters(Response, Plugin, XML);
            ResponseFormatThread = new Thread(IronProxy.SerializeResponseBody);
            ResponseFormatThread.Start(BFP);
        }

        internal static void SerializeResponseBody(object BFPObject)
        {
            string PluginName = "";
            try
            {
                BodyFormatParamters BFP = (BodyFormatParamters)BFPObject;
                Response Response = BFP.Response;
                FormatPlugin Plugin = BFP.Plugin;
                PluginName = Plugin.Name;
                string XML = BFP.XML;
                IronProxy.CurrentSession.Response = Response;
                Response = Plugin.ToResponseFromXml(Response, XML);
                IronProxy.UpdateFiddlerSessionWithNewResponse();
                IronUI.FillProxyResponseWithNewResponseFromFormatXML(Response, PluginName);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch (Exception Exp)
            {
                IronException.Report("Error Serializing Proxy Response using Format Plugin - " + PluginName, Exp.Message, Exp.StackTrace);
                IronUI.ShowProxyException("Error Serializing");
            }
        }
        internal static void TerminateAllFormatThreads()
        {
            TerminateRequestFormatThreads();
            TerminateResponseFormatThreads();
        }
        internal static void TerminateRequestFormatThreads()
        {
            if (RequestFormatThread != null)
            {
                try { RequestFormatThread.Abort(); }
                catch { }
                finally { RequestFormatThread = null; }
            }
        }
        internal static void TerminateResponseFormatThreads()
        {
            if (ResponseFormatThread != null)
            {
                try { ResponseFormatThread.Abort(); }
                catch { }
                finally { ResponseFormatThread = null; }
            }
        }

        internal static bool ValidProxyPort(string Port)
        {
            try
            {
                int IntPort = Int32.Parse(Port);
                return ValidProxyPort(IntPort);
            }
            catch
            {
                return false;
            }
        }

        internal static bool ValidProxyPort(int Port)
        {

            if (Port > 1024 && Port < 65536)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool ValidPort(string Port)
        {
            try
            {
                int IntPort = Int32.Parse(Port);
                return ValidPort(IntPort);
            }
            catch
            {
                return false;
            }
        }

        internal static bool ValidPort(int Port)
        {

            if (Port > 0 && Port < 65536)
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
