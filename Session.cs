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
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace IronWASP
{
    public class Session
    {
        internal Fiddler.Session FiddlerSession;
        public Request Request;
        public Response Response;
        internal ManualResetEvent MSR;

        //to be used only by IronProxy
        internal Request OriginalRequest;
        internal Response OriginalResponse;

        internal Dictionary<string, object> Flags = new Dictionary<string, object>();

        public Session(Fiddler.Session _FiddlerSession)
        {
            this.FiddlerSession = _FiddlerSession;
            this.Request = new Request(this.FiddlerSession);
            if (this.FiddlerSession.bHasResponse)
            {
                this.Response = new Response(this.FiddlerSession);
            }
        }

        public Session(string RequestString, string ResponseString)
        {
            this.Request = new Request(RequestString,false,true);
            this.Response = new Response(ResponseString);
            this.FiddlerSession = new Fiddler.Session(this.Request.GetFullRequestAsByteArray(), this.Response.GetFullResponseAsByteArray());
        }

        public Session(Request Request, Response Response)
        {
            this.Request = Request;
            this.Response = Response;
            this.FiddlerSession = new Fiddler.Session(this.Request.GetFullRequestAsByteArray(), this.Response.GetFullResponseAsByteArray());
        }

        public Session(string RequestString)
        {
            this.FiddlerSession = Session.CreateFiddlerSessionFromRequestString(RequestString);
            this.Request = new Request(this.FiddlerSession);
        }

        public Session(Request Req)
        {
            this.FiddlerSession = Req.ReturnAsFiddlerSession();
            this.Request = Req;
        }

        public Session()
        {

        }

        internal int ID
        {
            set
            {
                FiddlerSession.oFlags["IronFlag-ID"] = value.ToString();
                this.Request.ID = value;
            }
            get
            {
                return Int32.Parse(FiddlerSession.oFlags["IronFlag-ID"]);
            }
        }

        public static Session CreateSessionFromRequestString(string RequestString)
        {
            Fiddler.Session Sess = Session.CreateFiddlerSessionFromRequestString(RequestString);
            Session IrSe = new Session(Sess);
            return IrSe;
        }

        static Fiddler.Session CreateFiddlerSessionFromRequestString(string RequestString)
        {
            string[] RequestParts = RequestString.Split(new string[] { "\r\n\r\n" }, 2, StringSplitOptions.RemoveEmptyEntries);
            Fiddler.HTTPRequestHeaders RequestHeaders = new Fiddler.HTTPRequestHeaders();
            RequestHeaders.AssignFromString(RequestParts[0] + "\r\n\r\n");
            byte[] RequestBody = new byte[] { };
            if (RequestParts.Length > 1)
            {
                RequestBody = Encoding.GetEncoding("ISO-8859-1").GetBytes(RequestParts[1]);
            }
            Fiddler.Session Sess = new Fiddler.Session(RequestHeaders, RequestBody);
            return Sess;
        }

        public string GetResponseAsString()
        {
            return (((Fiddler.HTTPHeaders)this.FiddlerSession.oResponse.headers).ToString() + "\r\n" + this.FiddlerSession.GetResponseBodyAsString());
        }

        public int GetId()
        {
            return this.Request.GetId();
        }

        public Session GetClone()
        {
            Session ClonedIrSe = null;
            if (this.Response != null)
            {
                ClonedIrSe = new Session(this.Request.GetClone(), this.Response.GetClone());
            }
            else
            {
                ClonedIrSe = new Session(this.Request.GetClone());
            }
            return ClonedIrSe;
        }
        

        public static Session FromProxyLog(int ID)
        {
            IronLogRecord ILR = IronDB.GetRecordFromProxyLog(ID);
            return Session.GetIronSessionFromIronLogRecord(ILR, true, ID);
        }

        public static Session FromTestLog(int ID)
        {
            IronLogRecord ILR = IronDB.GetRecordFromTestLog(ID);
            return Session.GetIronSessionFromIronLogRecord(ILR, ID);
        }

        public static Session FromShellLog(int ID)
        {
            IronLogRecord ILR = IronDB.GetRecordFromShellLog(ID);
            return Session.GetIronSessionFromIronLogRecord(ILR, ID);
        }

        public static Session FromProbeLog(int ID)
        {
            IronLogRecord ILR = IronDB.GetRecordFromProbeLog(ID);
            return Session.GetIronSessionFromIronLogRecord(ILR, ID);
        }

        public static Session FromScanLog(int ID)
        {
            IronLogRecord ILR = IronDB.GetRecordFromScanLog(ID);
            return Session.GetIronSessionFromIronLogRecord(ILR, ID);
        }

        public static List<Session> FromProxyLog()
        {
            List<Session> Sessions = new List<Session>();
            foreach (IronLogRecord ILR in IronDB.GetRecordsFromProxyLog())
            {
                try
                {
                    Sessions.Add(Session.GetIronSessionFromIronLogRecord(ILR, ILR.ID));
                }
                catch { }
            }
            return Sessions;
        }

        public static List<Session> FromTestLog()
        {
            List<Session> Sessions = new List<Session>();
            foreach (IronLogRecord ILR in IronDB.GetRecordsFromTestLog())
            {
                try
                {
                    Sessions.Add(Session.GetIronSessionFromIronLogRecord(ILR, ILR.ID));
                }
                catch { }
            }
            return Sessions;
        }

        public static List<Session> FromShellLog()
        {
            List<Session> Sessions = new List<Session>();
            foreach (IronLogRecord ILR in IronDB.GetRecordsFromShellLog())
            {
                try
                {
                    Sessions.Add(Session.GetIronSessionFromIronLogRecord(ILR, ILR.ID));
                }
                catch { }
            }
            return Sessions;
        }

        public static List<Session> FromProbeLog()
        {
            List<Session> Sessions = new List<Session>();
            foreach (IronLogRecord ILR in IronDB.GetRecordsFromProbeLog())
            {
                try
                {
                    Sessions.Add(Session.GetIronSessionFromIronLogRecord(ILR, ILR.ID));
                }
                catch { }
            }
            return Sessions;
        }

        public static List<Session> FromScanLog()
        {
            List<Session> Sessions = new List<Session>();
            foreach (IronLogRecord ILR in IronDB.GetRecordsFromScanLog())
            {
                try
                {
                    Sessions.Add(Session.GetIronSessionFromIronLogRecord(ILR, ILR.ID));
                }
                catch { }
            }
            return Sessions;
        }

        internal static Session GetIronSessionFromIronLogRecord(IronLogRecord ILR, int ID)
        {
            return GetIronSessionFromIronLogRecord(ILR, false, ID);
        }

        internal static Session GetIronSessionFromIronLogRecord(IronLogRecord ILR, bool IsFromProxy, int ID)
        {
            Session IrSe;
            Request Req = GetRequest(ILR.RequestHeaders, ILR.RequestBody, ILR.IsRequestBinary);
            Req.ID = ID;

            if (ILR.ResponseHeaders.Length > 0)
            {
                Response Res = GetResponse(ILR.ResponseHeaders, ILR.ResponseBody, ILR.IsResponseBinary);
                IrSe = new Session(Req, Res);
                IrSe.Request.ID = ID;
            }
            else
            {
                IrSe = new Session(Req);
                IrSe.Request.ID = ID;
            }
            if (IsFromProxy)
            {
                if (ILR.OriginalRequestHeaders.Length > 0)
                {
                    IrSe.OriginalRequest = GetRequest(ILR.OriginalRequestHeaders, ILR.OriginalRequestBody, ILR.IsOriginalRequestBinary);
                    IrSe.OriginalRequest.ID = ID;
                }
                else
                {
                    IrSe.OriginalRequest = null;
                }
                if (ILR.OriginalResponseHeaders.Length > 0)
                {
                    IrSe.OriginalResponse = GetResponse(ILR.OriginalResponseHeaders, ILR.OriginalResponseBody, ILR.IsOriginalResponseBinary);
                    IrSe.OriginalResponse.ID = ID;
                }
                else
                {
                    IrSe.OriginalResponse = null;
                }
            }
            return IrSe;
        }

        static Request GetRequest(string RequestHeaders, string RequestBody, bool IsRequestBinary)
        {
            Request Req;
            if (IsRequestBinary)
            {
                if (RequestBody.Length > 0)
                {
                    byte[] BodyArray;
                    try
                    {
                        BodyArray = Convert.FromBase64String(RequestBody);
                    }
                    catch (Exception Exp)
                    {
                        throw new Exception("Error retriving body array from base64 string", Exp);
                    }
                    Req = new Request(RequestHeaders, BodyArray);
                }
                else
                {
                    Req = new Request(RequestHeaders, false, true);
                }
            }
            else
            {
                Req = new Request(RequestHeaders + RequestBody, false, true);
            }
            return Req;
        }

        static Response GetResponse(string ResponseHeaders, string ResponseBody, bool IsResponseBinary)
        {
            Response Res;
            if (IsResponseBinary)
            {
                if (ResponseBody.Length > 0)
                {
                    byte[] BodyArray;
                    try
                    {
                        BodyArray = Convert.FromBase64String(ResponseBody);
                    }
                    catch (Exception Exp)
                    {
                        throw new Exception("Error retriving body array from base64 string", Exp);
                    }
                    Res = new Response(ResponseHeaders, BodyArray);
                }
                else
                {
                    Res = new Response(ResponseHeaders);
                }
            }
            else
            {
                Res = new Response(ResponseHeaders + ResponseBody);
            }
            return Res;
        }
    }
}
