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

namespace IronWASP
{
    internal class PassiveChecker
    {
        internal static Thread CheckerThreadOne;
        internal static Thread CheckerThreadTwo;
        internal static Thread CheckerThreadThree;
        internal static Thread CheckerThreadFour;
        internal static bool On = true;
        static Queue<Session> CheckRequest = new Queue<Session>();
        static Queue<Session> CheckResponse = new Queue<Session>();

        internal static bool RunOnProxyTraffic = true;
        internal static bool RunOnShellTraffic = true;
        internal static bool RunOnTestTraffic = true;
        internal static bool RunOnScanTraffic = false;
        internal static bool RunOnProbeTraffic = true;

        internal static int RequestQueueLength
        {
            get
            {
                lock (CheckRequest)
                {
                    return CheckRequest.Count;
                }
            }

        }

        internal static int ResponseQueueLength
        {
            get
            {
                lock (CheckResponse)
                {
                    return CheckResponse.Count;
                }
            }
        }

        internal static void AddToCheckRequest(Session Sess)
        {
            switch (Sess.Request.Source)
            {
                case RequestSource.Proxy:
                    if (!RunOnProxyTraffic) return;
                    break;
                case RequestSource.Shell:
                    if (!RunOnShellTraffic) return;
                    break;
                case RequestSource.Test:
                    if (!RunOnTestTraffic) return;
                    break;
                case RequestSource.Scan:
                    if (!RunOnScanTraffic) return;
                    break;
                case RequestSource.Probe:
                    if (!RunOnProbeTraffic) return;
                    break;
                default:
                    if (!Sess.Request.CanRunPassivePlugins) return;
                    break;
            }
            lock (CheckRequest)
            {
                CheckRequest.Enqueue(Sess);
            }
        }

        internal static void AddToCheckResponse(Session Sess)
        {
            if (Sess.Response != null)
            {
                switch(Sess.Request.Source)
                {
                    case RequestSource.Proxy:
                        if (!RunOnProxyTraffic) return;
                        break;
                    case RequestSource.Shell:
                        if (!RunOnShellTraffic) return;
                        break;
                    case RequestSource.Test:
                        if (!RunOnTestTraffic) return;
                        break;
                    case RequestSource.Scan:
                        if (!RunOnScanTraffic) return;
                        break;
                    case RequestSource.Probe:
                        if (!RunOnProbeTraffic) return;
                        break;
                    default:
                        if (!Sess.Request.CanRunPassivePlugins) return;
                        break;
                }
                lock (CheckResponse)
                {
                    CheckResponse.Enqueue(Sess);
                }
            }
        }

        internal static void Start()
        {
            CheckerThreadOne = new Thread(Check);
            CheckerThreadTwo = new Thread(Check);
            CheckerThreadThree = new Thread(Check);
            CheckerThreadFour = new Thread(Check);
            CheckerThreadOne.Start();
            CheckerThreadTwo.Start();
            CheckerThreadThree.Start();
            CheckerThreadFour.Start();
        }

        internal static void Check()
        {
            while (On)
            {
                try
                {
                    Session IrSe;
                    lock (CheckRequest)
                    {
                        IrSe = CheckRequest.Dequeue();
                    }
                    if(IrSe != null) PluginStore.RunAllRequestBasedOfflinePassivePlugins(IrSe);
                }
                catch (InvalidOperationException)
                { }
                catch(Exception Exp)
                {
                    IronException.Report("Error Running Offline Plugins on Request", Exp.Message, Exp.StackTrace);
                }

                try
                {
                    Session IrSe;
                    lock (CheckResponse)
                    {
                        IrSe = CheckResponse.Dequeue();
                    }
                    if (IrSe != null) PluginStore.RunAllResponseBasedOfflinePassivePlugins(IrSe);
                }
                catch (InvalidOperationException)
                {
                    Thread.Sleep(500);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Error Running Offline Plugins on Response", Exp.Message, Exp.StackTrace);
                }
            }
        }

        internal static void Stop()
        {
            On = false;
            
            try
            {
                CheckerThreadOne.Abort();
            }
            catch { }
            
            try
            {
                CheckerThreadTwo.Abort();
            }
            catch { }

            try
            {
                CheckerThreadThree.Abort();
            }
            catch { }

            try
            {
                CheckerThreadFour.Abort();
            }
            catch { }
        }
    }
}
