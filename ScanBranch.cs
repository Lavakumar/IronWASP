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
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    internal class ScanBranch
    {
        internal static List<int> ProxyLogIDs = new List<int>();
        internal static List<int> ProbeLogIDs = new List<int>();
        internal static Thread ScanThread;

        internal static string HostName = "";
        internal static string UrlPattern = "";
        internal static bool HTTP = true;
        internal static bool HTTPS = true;

        internal static bool ScanAll = true;
        internal static bool ScanUrl = true;
        internal static bool ScanQuery = true;
        internal static bool ScanBody = true;
        internal static bool ScanCookie = true;
        internal static bool ScanHeaders = true;

        internal static string SessionPlugin = "";
        internal static string FormatPlugin = "";

        internal static List<string> ActivePlugins = new List<string>();

        internal static bool SelectGET = true;
        internal static bool SelectPOST = true;
        internal static bool SelectOtherMethods = true;

        internal static bool SelectHTML = true;
        internal static bool SelectJS = true;
        internal static bool SelectCSS = true;
        internal static bool SelectXML = true;
        internal static bool SelectJSON = true;
        internal static bool SelectOtherText = true;
        internal static bool SelectImg = true;
        internal static bool SelectOtherBinary = true;

        internal static bool Select200 = true;
        internal static bool Select2xx = true;
        internal static bool Select301_2 = true;
        internal static bool Select3xx = true;
        internal static bool Select304 = true;
        internal static bool Select403 = true;
        internal static bool Select4xx = true;
        internal static bool Select500 = true;
        internal static bool Select5xx = true;

        internal static bool SelectCheckFileExtensions = false;
        internal static bool SelectCheckFileExtensionsPlus = false;
        internal static List<string> SelectFileExtensions = new List<string>();
        internal static bool SelectCheckFileExtensionsMinus = false;
        internal static List<string> DontSelectFileExtensions = new List<string>();

        internal static bool SelectCheckQueryParameters = false;
        internal static bool SelectCheckQueryParametersPlus = false;
        internal static List<string> SelectQueryParameters = new List<string>();
        internal static bool SelectCheckQueryParametersMinus = false;
        internal static List<string> DontSelectQueryParameters = new List<string>();

        internal static bool SelectCheckBodyParameters = false;
        internal static bool SelectCheckBodyParametersPlus = false;
        internal static List<string> SelectBodyParameters = new List<string>();
        internal static bool SelectCheckBodyParametersMinus = false;
        internal static List<string> DontSelectBodyParameters = new List<string>();

        internal static bool SelectCheckCookieParameters = false;
        internal static bool SelectCheckCookieParametersPlus = false;
        internal static List<string> SelectCookieParameters = new List<string>();
        internal static bool SelectCheckCookieParametersMinus = false;
        internal static List<string> DontSelectCookieParameters = new List<string>();

        internal static bool SelectCheckHeadersParameters = false;
        internal static bool SelectCheckHeadersParametersPlus = false;
        internal static List<string> SelectHeadersParameters = new List<string>();
        internal static bool SelectCheckHeadersParametersMinus = false;
        internal static List<string> DontSelectHeadersParameters = new List<string>();

        internal static bool SkipScanned = false;

        internal static bool PickFromProxyLog = true;
        internal static bool PickFromProbeLog = false;

        static List<string> RequestSignatures = new List<string>();

        static List<string> NewRequestSignatures = new List<string>();

        static List<Request> ScannedRequests = new List<Request>();

        static List<string> UniqueQueryParameters = new List<string>();
        static List<string> NonUniqueQueryParameters = new List<string>();
        static List<string> UniqueBodyParameters = new List<string>();
        static List<string> NonUniqueBodyParameters = new List<string>();
        static List<string> UniqueUrlParameters = new List<string>();

        static int ScanDone = 0;
        static int TotalScans = 0;

        internal static void Start()
        {
            ScanThread = new Thread(StartBranchScan);
            ScanThread.Start();
        }

        internal static void StartBranchScan()
        {
            try
            {
                ScanDone = 0;
                TotalScans = ProxyLogIDs.Count;
                NewRequestSignatures.Clear();
                ScanItemUniquenessChecker UniqueChecker = new ScanItemUniquenessChecker(true);
                foreach (int i in ScanBranch.ProxyLogIDs)
                {
                    ScanItem(UniqueChecker, "Proxy", i);
                }
                foreach (int i in ScanBranch.ProbeLogIDs)
                {
                    ScanItem(UniqueChecker, "Probe", i);
                }
                foreach (string Signature in NewRequestSignatures)
                {
                    if (!RequestSignatures.Contains(Signature)) RequestSignatures.Add(Signature);
                }
                NewRequestSignatures.Clear();
                IronUI.UpdateScanBranchStats(0, 0, "All Scan Jobs Created && Queued. Close this Window.", false, true);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch(Exception Exp)
            {
                IronException.Report("Scan Branch Exception", Exp.Message, Exp.StackTrace);
            }
        }

        static void ScanItem(ScanItemUniquenessChecker UniqueChecker, string LogSource, int LogID)
        {
            try
            {
                Request Req;
                if(LogSource.Equals("Proxy"))
                   Req = Request.FromProxyLog(LogID);
                else
                    Req = Request.FromProbeLog(LogID);
                if (!CanScan(Req))
                {
                    TotalScans--;
                    IronUI.UpdateScanBranchStats(ScanDone, TotalScans, "Skipping previously scanned Request...." + ScanDone.ToString() + "/" + TotalScans.ToString() + " done", true, false);
                    return;
                }
                if (!UniqueChecker.IsUniqueToScan(Req, ScannedRequests, !ScanUrl))
                {
                    TotalScans--;
                    IronUI.UpdateScanBranchStats(ScanDone, TotalScans, "Skipping duplicate Request...." + ScanDone.ToString() + "/" + TotalScans.ToString() + " done", true, false);
                    return;
                }
                ScannedRequests.Add(Req.GetClone());
                Scanner Scan = new Scanner(Req);
                Scan = SetSessionPlugin(Scan);
                Scan = SetFormatPlugin(Scan);
                Scan = AddActivePlugins(Scan);
                Scan = SetInjectionPoints(Scan);
                if (Scan.InjectionPointsCount == 0)
                {
                    TotalScans--;
                    IronUI.UpdateScanBranchStats(ScanDone, TotalScans, "Skipping Request as no Injection Points were Identified...." + ScanDone.ToString() + "/" + TotalScans.ToString() + " done", true, false);
                    return;
                }
                Scan.LaunchScan();
                ScanDone++;
                IronUI.UpdateScanBranchStats(ScanDone, TotalScans, "Creating and Queueing Scans...." + ScanDone.ToString() + "/" + TotalScans.ToString() + " done", true, false);
            }
            catch (Exception Exp)
            {
                IronException.Report("ScanBranch Error Creating Scan Job with " + LogSource  + " Log ID - " + LogID.ToString(), Exp.Message, Exp.StackTrace);
            }
        }

        static Scanner SetInjectionPoints(Scanner Scan)
        {
            if (ScanUrl) Scan.InjectUrl();
            if (ScanQuery && SelectCheckQueryParameters && (SelectCheckQueryParametersPlus || SelectCheckQueryParametersMinus))
            {
                if (SelectCheckQueryParametersPlus)
                {
                    foreach (string Name in Scan.OriginalRequest.Query.GetNames())
                    {
                        if (SelectQueryParameters.Contains(Name))
                        {
                            for (int i = 0; i < Scan.OriginalRequest.Query.GetAll(Name).Count; i++)
                            {
                                Scan.InjectQuery(Name, i);
                            }
                        }
                    }
                }
                else
                {
                    foreach (string Name in Scan.OriginalRequest.Query.GetNames())
                    {
                        if (!DontSelectQueryParameters.Contains(Name))
                        {
                            for (int i = 0; i < Scan.OriginalRequest.Query.GetAll(Name).Count; i++)
                            {
                                Scan.InjectQuery(Name, i);
                            }
                        }
                    }
                }
            }
            else
            {
                if (ScanQuery) Scan.InjectQuery();
            }

            if (Scan.BodyFormat.Name.Length > 0)
            {
                string Xml = Scan.BodyFormat.ToXmlFromRequest(Scan.OriginalRequest);
                string[,] XmlArray = IronWASP.FormatPlugin.XmlToArray(Xml);
                Scan.BodyXmlInjectionParameters = new Parameters();
                for (int i = 0; i < XmlArray.GetLength(0); i++)
                {
                    Scan.BodyXmlInjectionParameters.Add(XmlArray[i,0], XmlArray[i,1]);
                }
            }

            if (ScanBody && SelectCheckBodyParameters && (SelectCheckBodyParametersPlus || SelectCheckBodyParametersMinus))
            {
                if (SelectCheckBodyParametersPlus)
                {
                    if (Scan.BodyFormat.Name.Length == 0)
                    {
                        foreach (string Name in Scan.OriginalRequest.Body.GetNames())
                        {
                            if (SelectBodyParameters.Contains(Name))
                            {
                                for (int i = 0; i < Scan.OriginalRequest.Body.GetAll(Name).Count; i++)
                                {
                                    Scan.InjectBody(Name, i);
                                }
                            }
                        }
                    }
                    else
                    {
                        int i = 0;
                        foreach (string Name in Scan.BodyXmlInjectionParameters.GetNames())
                        {
                            if (SelectBodyParameters.Contains(Name))
                            {
                                Scan.InjectBody(i);
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    if (Scan.BodyFormat.Name.Length == 0)
                    {
                        foreach (string Name in Scan.OriginalRequest.Body.GetNames())
                        {
                            if (!DontSelectBodyParameters.Contains(Name))
                            {
                                for (int i = 0; i < Scan.OriginalRequest.Body.GetAll(Name).Count; i++)
                                {
                                    Scan.InjectBody(Name, i);
                                }
                            }
                        }
                    }
                    else
                    {
                        int i = 0;
                        foreach (string Name in Scan.BodyXmlInjectionParameters.GetNames())
                        {
                            if (!DontSelectBodyParameters.Contains(Name))
                            {
                                Scan.InjectBody(i);
                            }
                            i++;
                        }
                    }
                }
            }
            else
            {
                if (ScanBody) Scan.InjectBody();
            }

            if (ScanCookie && SelectCheckCookieParameters && (SelectCheckCookieParametersPlus || SelectCheckCookieParametersMinus))
            {
                if (SelectCheckCookieParametersPlus)
                {
                    foreach (string Name in Scan.OriginalRequest.Cookie.GetNames())
                    {
                        if (SelectCookieParameters.Contains(Name))
                        {
                            for (int i = 0; i < Scan.OriginalRequest.Cookie.GetAll(Name).Count; i++)
                            {
                                Scan.InjectCookie(Name, i);
                            }
                        }
                    }
                }
                else
                {
                    foreach (string Name in Scan.OriginalRequest.Cookie.GetNames())
                    {
                        if (!DontSelectCookieParameters.Contains(Name))
                        {
                            for (int i = 0; i < Scan.OriginalRequest.Cookie.GetAll(Name).Count; i++)
                            {
                                Scan.InjectCookie(Name, i);
                            }
                        }
                    }
                }
            }
            else
            {
                if (ScanCookie) Scan.InjectCookie();
            }

            if (ScanHeaders && SelectCheckHeadersParameters && (SelectCheckHeadersParametersPlus || SelectCheckHeadersParametersMinus))
            {
                if (SelectCheckHeadersParametersPlus)
                {
                    foreach (string Name in Scan.OriginalRequest.Headers.GetNames())
                    {
                        if (SelectHeadersParameters.Contains(Name))
                        {
                            for (int i = 0; i < Scan.OriginalRequest.Headers.GetAll(Name).Count; i++)
                            {
                                Scan.InjectHeaders(Name, i);
                            }
                        }
                    }
                }
                else
                {
                    foreach (string Name in Scan.OriginalRequest.Headers.GetNames())
                    {
                        if (!DontSelectHeadersParameters.Contains(Name))
                        {
                            for (int i = 0; i < Scan.OriginalRequest.Headers.GetAll(Name).Count; i++)
                            {
                                Scan.InjectHeaders(Name, i);
                            }
                        }
                    }
                }
            }
            else
            {
                if (ScanHeaders) Scan.InjectHeaders();
            }

            return Scan;
        }

        static Scanner AddActivePlugins(Scanner Scan)
        {
            foreach (string Name in ActivePlugins)
            {
                Scan.AddCheck(Name);
            }
            return Scan;
        }

        static Scanner SetSessionPlugin(Scanner Scan)
        {
            if (SessionPlugin.Length > 0)
            {
                Scan.SessionHandler = IronWASP.SessionPlugin.Get(SessionPlugin);
            }
            else
            {
                Scan.SessionHandler = new SessionPlugin();
            }
            return Scan;
        }

        static Scanner SetFormatPlugin(Scanner Scan)
        {
            if (FormatPlugin.Length > 0)
            {
                Scan.BodyFormat = IronWASP.FormatPlugin.Get(FormatPlugin);
            }
            else
            {
                if (!IronWASP.FormatPlugin.IsNormal(Scan.OriginalRequest))
                {
                    List<IronWASP.FormatPlugin> RightPlugins = IronWASP.FormatPlugin.Get(Scan.OriginalRequest);
                    if (RightPlugins.Count > 0)
                    {
                        Scan.BodyFormat = RightPlugins[0];
                    }
                }
            }
            return Scan;
        }

        internal static bool CanScan(DataGridViewRow Row, string Source)
        {
            if (Row.Cells[Source + "LogGridColumnForHostName"].Value == null) return false;
            if (Row.Cells[Source + "LogGridColumnForURL"].Value == null) return false;
            if (Row.Cells[Source + "LogGridColumnForSSL"].Value == null) return false;
            if (Row.Cells[Source + "LogGridColumnForMethod"].Value == null) return false;

            if (!Row.Cells[Source + "LogGridColumnForHostName"].Value.ToString().Equals(HostName, StringComparison.OrdinalIgnoreCase)) return false;
            if (UrlPattern.EndsWith("*"))
            {
                string Url = UrlPattern.TrimEnd(new char[] { '*' });
                if (!Row.Cells[Source + "LogGridColumnForURL"].Value.ToString().StartsWith(Url, StringComparison.OrdinalIgnoreCase)) return false;
            }
            else
            {
                if (!Row.Cells[Source + "LogGridColumnForURL"].Value.ToString().Equals(UrlPattern, StringComparison.OrdinalIgnoreCase)) return false;
            }
            if (!HTTPS)
            {
                if ((bool)Row.Cells[Source + "LogGridColumnForSSL"].Value) return false;
            }
            if (!HTTP)
            {
                if (!(bool)Row.Cells[Source + "LogGridColumnForSSL"].Value) return false;
            }
            switch (Row.Cells[Source + "LogGridColumnForMethod"].Value.ToString())
            {
                case "POST":
                    if (!SelectPOST) return false;
                    break;
                case "GET":
                    if (!SelectGET) return false;
                    break;
                default:
                    if (!SelectOtherMethods) return false;
                    break;
            }
            
            if (SelectCheckFileExtensions)
            {
                string FileExtension = "";
                if (Row.Cells[Source + "LogGridColumnForFile"].Value != null) FileExtension = Row.Cells[Source + "LogGridColumnForFile"].Value.ToString();
                if (SelectCheckFileExtensions && FileExtension.Length > 0)
                {
                    if (SelectCheckFileExtensionsPlus && SelectFileExtensions.Count > 0)
                    {
                        bool Match = false;
                        foreach (string File in SelectFileExtensions)
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
                    if (SelectCheckFileExtensionsMinus && DontSelectFileExtensions.Count > 0)
                    {
                        foreach (string File in DontSelectFileExtensions)
                        {
                            if (FileExtension.Equals(File, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            //Response based checks start. Return true if any values are null as the response field would be empty by the request would be valid
            if (Row.Cells[Source + "LogGridColumnForCode"].Value == null) return true;

            int Code = (int)Row.Cells[Source + "LogGridColumnForCode"].Value;
            switch (Code)
            {
                case 200:
                    if (!Select200) return false;
                    break;
                case 301:
                case 302:
                    if (!Select301_2) return false;
                    break;
                case 304:
                    if (!Select304) return false;
                    break;
                case 403:
                    if (!Select403) return false;
                    break;
                case 500:
                    if (!Select500) return false;
                    break;
                default:
                    if (Code > 199 && Code < 300)
                    {
                        if (!Select2xx) return false;
                    }
                    else if (Code > 299 && Code < 400)
                    {
                        if (!Select3xx) return false;
                    }
                    else if (Code > 399 && Code < 500)
                    {
                        if (!Select500) return false;
                    }
                    else if (Code > 499 && Code < 600)
                    {
                        if (!Select5xx) return false;   
                    }
                    break;
            }

            if (Row.Cells[Source + "LogGridColumnForMIME"].Value == null) return true;

            string ContentType = Row.Cells[Source + "LogGridColumnForMIME"].Value.ToString();
            if (ContentType != null)
            {
                if (ContentType.ToLower().Contains("html"))
                {
                    if (!SelectHTML) return false;
                }
                else if (ContentType.ToLower().Contains("css"))
                {
                    if (!SelectCSS) return false;
                }
                else if (ContentType.ToLower().Contains("javascript"))
                {
                    if (!SelectJS) return false;
                }
                else if (ContentType.ToLower().Contains("xml"))
                {
                    if (!SelectXML) return false;
                }
                else if (ContentType.ToLower().Contains("json"))
                {
                    if (!SelectJSON) return false;
                }
                else if (ContentType.ToLower().Contains("text"))
                {
                    if (!SelectOtherText) return false;
                }
                else if (ContentType.ToLower().Contains("jpg") || ContentType.ToLower().Contains("png") || ContentType.ToLower().Contains("jpeg") || ContentType.ToLower().Contains("gif") || ContentType.ToLower().Contains("ico"))
                {
                    if (!SelectImg) return false;
                }
                else
                {
                    if (!SelectOtherBinary) return false;
                }
            }
            return true;
        }

        internal static bool CanScan(Request Req)
        {
            string Signature = "";
            try
            {
                Signature = MakeSignature(Req);
            }
            catch(Exception Exp)
            {
                IronException.Report("ScanBranch error creating signatures", Exp.Message, Exp.StackTrace);
                return true;//empty signature should not be added and checked
            }
            if (NewRequestSignatures.Contains(Signature))
            {
                return false;
            }
            else
            {
                NewRequestSignatures.Add(Signature);
                if (SkipScanned)
                {
                    if(RequestSignatures.Contains(Signature))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        static string MakeSignature(Request Req)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(Req.FullURL.ToLower());
            SB.Append("|");
            SB.Append(Req.BodyString);
            return Tools.MD5(SB.ToString());
        }
    }
}
