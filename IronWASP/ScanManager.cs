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
using System.Threading;

namespace IronWASP
{
    internal class ScanManager
    {
        internal static ScanMode Mode = ScanMode.Default;
        internal static Thread ScanManagerThread;
        static Crawler Spider;

        static bool Stopped = false;

        internal static List<string> UrlsToAvoid = new List<string>();
        internal static List<string> HostsToInclude = new List<string>();
        internal static bool HTTP = false;
        internal static bool HTTPS = false;
        internal static SessionPlugin SessionHandler = new SessionPlugin();
        internal static string StartingUrl = "/";
        internal static string BaseUrl = "/";
        internal static string PrimaryHost = "";
        internal static bool PerformDirAndFileGuessing = true;
        internal static bool IncludeSubDomains = false;
        internal static bool CrawlAndScan = true;

        internal static bool InjectUrlPathParts = false;
        internal static bool InjectQuery = false;
        internal static bool InjectBody = false;
        internal static bool InjectCookie = false;
        internal static bool InjectHeaders = false;
        internal static bool InjectQueryName = false;
        internal static bool InjectBodyName = false;
        internal static bool InjectCookieName = false;
        internal static bool InjectHeaderName = false;

        internal static List<string> QueryBlackList = new List<string>();
        internal static List<string> QueryWhiteList = new List<string>();
        internal static List<string> BodyBlackList = new List<string>();
        internal static List<string> BodyWhiteList = new List<string>();
        internal static List<string> CookieBlackList = new List<string>();
        internal static List<string> CookieWhiteList = new List<string>();
        internal static List<string> HeaderBlackList = new List<string>();
        internal static List<string> HeaderWhiteList = new List<string>();

        internal static List<string> Checks = new List<string>();

        internal static bool CanPromptUser = false;

        internal static string[] SpecialHeader = new string[2];

        internal static bool CanStop = false;

        static List<string> ExtenionsToAvoid = new List<string>() { 
            "jpg", "png", "gif","bmp", "ico","exif","jpeg",//image files
            "7z", "zip", "rar","tar", "gz","tgz","bzip", "bzip2","dmg","cab",//compressed files
            "html", "htm", "js", "css","xhtml","svg","svgz","bak",//static web content
            "swf","exe", "jar", "msi","deb","bin","class","war",//executable content
            "rtf", "txt", "pdf", "doc", "docx", "ppt", "pptx","xls","xlsx", "iso","xml","json","xps","tex","csv","pps","tsv","db","log","rss",//document formats
            "mp3","wav","m4a","m4p","aac","dat",//audio content
            "mp4","aaf","3gp","wmv","avi","fla","sol","mov","mpeg","mpg","mpe","ogg","rm",//video content
               };

        internal static void StartScan()
        {
            try
            {
                CanStop = true;
                Stop(true);
                CanStop = false;
                Stopped = false;
                ScanManagerThread = new Thread(DoScanBlock);
                ScanManagerThread.Start();
            }
            catch(Exception Exp)
            {
                IronUI.ShowConsoleStatus("Error: " + Exp.Message, true); 
            }
        }

        static void DoScanBlock()
        {
            try
            {
                DoScan();
            }
            catch(Exception Exp)
            {
                IronException.Report("Error in ScanManager", Exp);
                try
                {
                    Stop();
                }
                catch { }
            }
        }

        static void DoScan()
        {
            Spider = new Crawler();
            try
            {
                Spider.PrimaryHost = PrimaryHost;
                Spider.BaseUrl = BaseUrl;
                Spider.StartingUrl = StartingUrl;
                Spider.PerformDirAndFileGuessing = PerformDirAndFileGuessing;
                Spider.IncludeSubDomains = IncludeSubDomains;
                Spider.HTTP = HTTP;
                Spider.HTTPS = HTTPS;
                Spider.UrlsToAvoid = UrlsToAvoid;
                Spider.HostsToInclude = HostsToInclude;
                Spider.SpecialHeader = SpecialHeader;

                Spider.Start();
            }
            catch(Exception Exp)
            {
                IronException.Report("Error starting Crawler", Exp);
                try
                {
                    Stop();
                }
                catch { }
                return;
            }

            ScanItemUniquenessChecker UniqueChecker = new ScanItemUniquenessChecker(CanPromptUser);
            
            List<int> ScanIDs = new List<int>();
            bool ScanActive = true;
            List<string> ActivePlugins = ActivePlugin.List();
            int TotalRequestsCrawled = 0;
            int TotalScanJobsCreated = 0;
            int TotalScanJobsCompleted = 0;
            List<Request> ScannedRequests = new List<Request>();
            int SleepCounter = 0;

            while (ScanActive)
            {
                ScanActive = false;
                List<Request> Requests = Spider.GetCrawledRequests();
                if (Stopped) return;
                if (Requests.Count > 0 || Spider.IsActive())
                {
                    ScanActive = true;
                    if (CrawlAndScan)
                    {
                        TotalRequestsCrawled = TotalRequestsCrawled + Requests.Count;
                        //update the ui with the number of requests crawled
                        foreach (Request Req in Requests)
                        {
                            if (Stopped) return;
                            if (!CanScan(Req)) continue;
                            if (!UniqueChecker.IsUniqueToScan(Req, ScannedRequests, false)) continue;
                            try
                            {
                                Scanner S = new Scanner(Req);
                                foreach (string Check in Checks)
                                {
                                    S.AddCheck(Check);
                                }
                                if (InjectQuery)
                                {
                                    if (QueryWhiteList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Query.GetNames())
                                        {
                                            if (QueryWhiteList.Contains(Name)) S.InjectQuery(Name);
                                        }
                                    }
                                    else if (QueryBlackList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Query.GetNames())
                                        {
                                            if (!QueryBlackList.Contains(Name)) S.InjectQuery(Name);
                                        }
                                    }
                                    else
                                    {
                                        S.InjectQuery();
                                    }
                                }

                                if (InjectBody)
                                {
                                    if (BodyWhiteList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Body.GetNames())
                                        {
                                            if (BodyWhiteList.Contains(Name)) S.InjectBody(Name);
                                        }
                                    }
                                    else if (BodyBlackList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Body.GetNames())
                                        {
                                            if (!BodyBlackList.Contains(Name)) S.InjectBody(Name);
                                        }
                                    }
                                    else
                                    {
                                        S.InjectBody();
                                    }
                                }

                                if (InjectCookie)
                                {
                                    if (CookieWhiteList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Cookie.GetNames())
                                        {
                                            if (CookieWhiteList.Contains(Name)) S.InjectCookie(Name);
                                        }
                                    }
                                    else if (CookieBlackList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Cookie.GetNames())
                                        {
                                            if (!CookieBlackList.Contains(Name)) S.InjectCookie(Name);
                                        }
                                    }
                                    else
                                    {
                                        S.InjectCookie();
                                    }
                                }

                                if (InjectHeaders)
                                {
                                    if (HeaderWhiteList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Headers.GetNames())
                                        {
                                            if (HeaderWhiteList.Contains(Name)) S.InjectHeaders(Name);
                                        }
                                    }
                                    else if (HeaderBlackList.Count > 0)
                                    {
                                        foreach (string Name in S.OriginalRequest.Headers.GetNames())
                                        {
                                            if (!HeaderBlackList.Contains(Name)) S.InjectHeaders(Name);
                                        }
                                    }
                                    else
                                    {
                                        S.InjectHeaders();
                                    }
                                }

                                if (InjectUrlPathParts)
                                {
                                    if (S.OriginalRequest.Query.Count == 0 && S.OriginalRequest.File.Length == 0)
                                        S.InjectUrl();
                                }

                                if (S.InjectionPointsCount == 0) continue;
                                TotalScanJobsCreated++;
                                if (Stopped) return;
                                int ScanID = S.LaunchScan();
                                if (Stopped)
                                {
                                    Stop(true);
                                    return;
                                }
                                if (ScanID > 0)
                                {
                                    ScannedRequests.Add(Req);
                                    ScanIDs.Add(ScanID);
                                }
                            }
                            catch(Exception Exp)
                            {
                                IronException.Report(string.Format("Error creating Scan Job with Request - {0}", Req.Url), Exp);
                            }
                        }
                    }
                }
                if (CrawlAndScan)
                {
                    List<int> ScanIDsToRemove = new List<int>();
                    List<int> AbortedScanIDs = Scanner.GetAbortedScanIDs();
                    List<int> CompletedScanIDs = Scanner.GetCompletedScanIDs();
                    for (int i = 0; i < ScanIDs.Count; i++)
                    {
                        if (Stopped) return;
                        if (CompletedScanIDs.Contains(ScanIDs[i]))
                        {
                            ScanIDsToRemove.Add(i);
                            TotalScanJobsCompleted++;
                        }
                        else if (AbortedScanIDs.Contains(ScanIDs[i]))
                        {
                            ScanIDsToRemove.Add(i);
                        }
                    }
                    for (int i = 0; i < ScanIDsToRemove.Count; i++)
                    {
                        if (Stopped) return;
                        ScanIDs.RemoveAt(ScanIDsToRemove[i] - i);
                    }
                }
                if (ScanActive)
                {
                    Thread.Sleep(2000);
                }
                else
                {
                    if (ScanIDs.Count > 0)
                    {
                        ScanActive = true;
                        Thread.Sleep(5000);
                    }
                    else if (SleepCounter < 10)
                    {
                        ScanActive = true;
                        Thread.Sleep(2000);
                        SleepCounter = SleepCounter + 2;
                    }
                }
                if (Stopped) return;
                IronUI.UpdateConsoleCrawledRequestsCount(TotalRequestsCrawled);
                IronUI.UpdateConsoleScanJobsCreatedCount(TotalScanJobsCreated);
                IronUI.UpdateConsoleScanJobsCompletedCount(TotalScanJobsCompleted);
            }
            if (Stopped) return;
            Stop();
        }


        internal static void Pause()
        {

        }

        internal static void Resume()
        {

        }

        internal static void Stop()
        {
            Stop(false);
        }

        internal static void Stop(bool SameThread)
        {
            Stopped = true;
            if (SameThread)
            {
                DoStop();
            }
            else
            {
                Thread T = new Thread(DoStop);
                T.Start();
            }
        }

        internal static void DoStop()
        {
            try
            {
                Stopped = true;
                if (ScanManagerThread != null)
                {
                    if (ScanManagerThread.ThreadState == ThreadState.Running && !CanStop)
                    {
                        //ask if user is sure about stopping scan and then call...
                        EndAll();
                    }
                    else
                    {
                        EndAll();
                    }
                }
                else
                {
                    IronUI.UpdateConsoleControlsStatus(false);
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Error stopping the Scan", Exp);
            }
        }

        static void EndAll()
        {
            try
            {
                //if (Thread.CurrentThread.ManagedThreadId != ScanManagerThread.ManagedThreadId)
                //ScanManagerThread.Abort();
            }
            catch { }
            try
            {
                Spider.Stop();
            }
            catch { }
            Scanner.StopAll();
            IronUI.UpdateConsoleControlsStatus(false);
        }

        static bool IsScanActive()
        {
            return true;
        }

        static bool CanScan(Request Req)
        {
            string File = Req.File.Trim().ToLower();
            if (ExtenionsToAvoid.Contains(File))
                return false;
            else
                return true;
        }
    }
}
