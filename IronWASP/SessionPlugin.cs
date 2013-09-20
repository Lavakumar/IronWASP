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
    public class SessionPlugin : Plugin
    {
        internal static List<SessionPlugin> Collection = new List<SessionPlugin>();

        //public bool IsLoggedIn = false;
        //public string Cookie = "";
        //public string Token = "";
        //public int MaxReDoCount = 5;

        //Dictionary<string, Queue<int>> SyncWaitQueue = new Dictionary<string, Queue<int>>();
        //Dictionary<string, int[]> SyncManageQueue = new Dictionary<string, int[]>();
        //int SyncID = 0;

        //virtual public Request Update(Request Request, Response Response)
        //{
        //    Request.SetCookie(Response);
        //    return Request;
        //}
                
        //virtual public Request Login(Request Request, Response Response)
        //{
        //    return Request;
        //}

        virtual public Response GetBaseLine(Scanner Scanner, Request Request)
        {
            return Scanner.Inject();
        }

        //virtual public Request PrepareForInjection(Request Request)
        //{
        //    return Request;
        //}

        //virtual public Response GetInterestingResponse(Request Request, Response Response)
        //{
        //    return Response;
        //}

        virtual public bool CanInject(Scanner Scanner, Request Request)
        {
            return true;
        }

        virtual public string EncodePayload(string RequestSection, Request Request, string Payload)
        {
            return Payload;
        }

        //virtual public bool ShouldReDo(Scanner Scnr, Request Req, Response Res)
        //{
        //    return false;
        //}

        //protected int StartSync(string Name, int MaxTime)
        //{
        //    int ID = Interlocked.Increment(ref SyncID);
        //    int StartTime = (int)DateTime.Now.TimeOfDay.TotalSeconds;
        //    lock (SyncWaitQueue)
        //    {
        //        if (!SyncWaitQueue.ContainsKey(Name))
        //        {
        //            SyncWaitQueue.Add(Name, new Queue<int>());
        //        }
        //        SyncWaitQueue[Name].Enqueue(ID);
        //    }
        //    while (!CanProceed(Name, ID, StartTime, MaxTime))
        //    {
        //        Thread.Sleep(1000);
        //    }
        //    return ID;
        //}

        //protected void StopSync(string Name, int JobID)
        //{
        //    lock (SyncManageQueue)
        //    {
        //        if (SyncManageQueue.ContainsKey(Name))
        //        {
        //            if (SyncManageQueue[Name][0] == JobID) SyncManageQueue[Name][0] = 0;
        //        }
        //    }
        //}

        //bool CanProceed(string Name, int ID, int StartTime, int MaxTime)
        //{
        //    bool Result = false;
        //    int CurrentTime = (int)DateTime.Now.TimeOfDay.TotalSeconds;
        //    lock (SyncManageQueue)
        //    {
        //        if (!SyncManageQueue.ContainsKey(Name))
        //        {
        //            SyncManageQueue.Add(Name, new int[] { ID, CurrentTime, MaxTime });
        //            Result = true;
        //        }
        //        else
        //        {
        //            int[] CurrentValues = SyncManageQueue[Name];
        //            if (CurrentValues[0] == 0 || CurrentValues[0] == ID || ((CurrentTime - CurrentValues[1]) > CurrentValues[2]))
        //            {
        //                lock (SyncWaitQueue)
        //                {
        //                    if (SyncWaitQueue.ContainsKey(Name))
        //                    {
        //                        if (SyncWaitQueue[Name].Count > 0)
        //                        {
        //                            int FetchedID = SyncWaitQueue[Name].Dequeue();
        //                            SyncManageQueue[Name] = new int[] { FetchedID, CurrentTime, MaxTime };
        //                            if (FetchedID == ID) Result = true;
        //                        }
        //                        else
        //                        {
        //                            Result = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Result = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return Result;
        //}

        //internal Request DoLogin(Request Req, Response Res)
        //{
        //    int LoginSyncID = StartSync(this.Name, 120);
        //    Request LoggedInReq = Login(Req, Res);
        //    StopSync(this.Name, LoginSyncID);
        //    return LoggedInReq;
        //}

        virtual public Request DoBeforeSending(Request Req, Response Res)
        {
            return Req;
        }
        virtual public Response DoAfterSending(Response Res, Request Req)
        {
            return Res;
        }

        public virtual SessionPlugin GetInstance()
        {
            return new SessionPlugin();
        }

        public static void Add(SessionPlugin SP)
        {
            if ((SP.Name.Length > 0) && !(SP.Name.Equals("All") || SP.Name.Equals("None")))
            {
                if (!List().Contains(SP.Name))
                {
                    if (SP.FileName != "Internal")
                    {
                        SP.FileName = PluginEngine.FileName;
                    }
                    Collection.Add(SP);
                }
            }
        }

        public static List<string> List()
        {
            List<string> Names = new List<string>();
            foreach (SessionPlugin SP in Collection)
            {
                Names.Add(SP.Name);
            }
            return Names;
        }

        public static SessionPlugin Get(string Name)
        {
            foreach (SessionPlugin SP in Collection)
            {
                if (SP.Name.Equals(Name))
                {
                    SessionPlugin NewInstance = SP.GetInstance();
                    NewInstance.FileName = SP.FileName;
                    return NewInstance;
                }
            }
            return null;
        }

        internal static void Remove(string Name)
        {
            int PluginIndex = 0;
            for (int i = 0; i < Collection.Count; i++)
            {
                if (Collection[i].Name.Equals(Name))
                {
                    PluginIndex = i;
                    break;
                }
            }
            Collection.RemoveAt(PluginIndex);
        }

        public void Trace(Request Req, string Action, string Message)
        {
            IronTrace IT = new IronTrace(Req, this.Name, Action, Message);
            IT.Report();
        }
    }
}
