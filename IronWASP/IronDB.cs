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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Text;
using System.IO;
using System.Threading;


namespace IronWASP
{
    public class IronDB
    {
        internal static string LogPath = "";
        internal static string IronProjectFile = "";
        internal static string ProxyLogFile = "";
        internal static string TestLogFile = "";
        internal static string ShellLogFile = "";
        internal static string ProbeLogFile = "";
        internal static string ScanLogFile = "";
        internal static string PluginResultsLogFile = "";
        internal static string ExceptionsLogFile = "";
        internal static string TraceLogFile = "";
        internal static string ConfigFile = Config.RootDir + "\\IronConfig.exe";

        internal static void LogProxyMessages(List<Session> IronSessions, List<Request> Requests, List<Response> Responses, List<Request> OriginalRequests, List<Response> OriginalResponses, List<Request> EditedRequests, List<Response> EditedResponses)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + ProxyLogFile);
            Log.Open();
            try
            {
                using (SQLiteTransaction InsertLogs = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        //Insert Request/Response in to DB
                        foreach (Session IrSe in IronSessions)
                        {
                            Cmd.CommandText = "INSERT INTO ProxyLog (ID , SSL, HostName, Method, URL, Edited, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, OriginalRequestHeaders, OriginalRequestBody, BinaryOriginalRequest, Code, Length, MIME, SetCookie, ResponseHeaders, ResponseBody, BinaryResponse, OriginalResponseHeaders, OriginalResponseBody, BinaryOriginalResponse, Notes) VALUES (@ID , @SSL, @HostName, @Method, @URL, @Edited, @File, @RequestHeaders, @RequestBody, @BinaryRequest, @OriginalRequestHeaders, @OriginalRequestBody, @BinaryOriginalRequest, @Code, @Length, @MIME, @SetCookie, @ResponseHeaders, @ResponseBody, @BinaryResponse, @OriginalResponseHeaders, @OriginalResponseBody, @BinaryOriginalResponse, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", IrSe.Request.ID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(IrSe.Request.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", IrSe.Request.Host);
                            Cmd.Parameters.AddWithValue("@Method", IrSe.Request.Method);
                            Cmd.Parameters.AddWithValue("@URL", IrSe.Request.URL);
                            Cmd.Parameters.AddWithValue("@Edited", AsInt((IrSe.OriginalRequest != null) || (IrSe.OriginalResponse != null)));
                            Cmd.Parameters.AddWithValue("@File", IrSe.Request.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", IrSe.Request.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.GetHeadersAsString());//IrSe.Request.GetHeadersAsStringWithoutFullURL());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.StoredHeadersString);
                            if(IrSe.Request.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(IrSe.Request.IsBinary));
                            if (IrSe.OriginalRequest != null)
                            {
                                //Cmd.Parameters.AddWithValue("@OriginalRequestHeaders", IrSe.OriginalRequest.GetHeadersAsString());
                                Cmd.Parameters.AddWithValue("@OriginalRequestHeaders", IrSe.OriginalRequest.StoredHeadersString);
                                if (IrSe.OriginalRequest.IsBinary)
                                    Cmd.Parameters.AddWithValue("@OriginalRequestBody", IrSe.OriginalRequest.StoredBinaryBodyString);
                                else
                                    Cmd.Parameters.AddWithValue("@OriginalRequestBody", IrSe.OriginalRequest.BodyString);
                                //Cmd.Parameters.AddWithValue("@OriginalRequestBody", IrSe.OriginalRequest.BodyString);
                                Cmd.Parameters.AddWithValue("@BinaryOriginalRequest", AsInt(IrSe.OriginalRequest.IsBinary));
                            }
                            else
                            {
                                Cmd.Parameters.AddWithValue("@OriginalRequestHeaders", "");
                                Cmd.Parameters.AddWithValue("@OriginalRequestBody", "");
                                Cmd.Parameters.AddWithValue("@BinaryOriginalRequest", 0);
                            }

                            Cmd.Parameters.AddWithValue("@Code", IrSe.Response.Code);
                            Cmd.Parameters.AddWithValue("@Length", IrSe.Response.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", IrSe.Response.ContentType);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((IrSe.Response.SetCookies.Count > 0)));
                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.StoredHeadersString);
                            if (IrSe.Response.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(IrSe.Response.IsBinary));
                            if (IrSe.OriginalResponse != null)
                            {
                                //Cmd.Parameters.AddWithValue("@OriginalResponseHeaders", IrSe.OriginalResponse.GetHeadersAsString());
                                Cmd.Parameters.AddWithValue("@OriginalResponseHeaders", IrSe.OriginalResponse.StoredHeadersString);
                                if (IrSe.OriginalResponse.IsBinary)
                                    Cmd.Parameters.AddWithValue("@OriginalResponseBody", IrSe.OriginalResponse.StoredBinaryBodyString);
                                else
                                    Cmd.Parameters.AddWithValue("@OriginalResponseBody", IrSe.OriginalResponse.BodyString);
                                //Cmd.Parameters.AddWithValue("@OriginalResponseBody", IrSe.OriginalResponse.BodyString);
                                Cmd.Parameters.AddWithValue("@BinaryOriginalResponse", AsInt(IrSe.OriginalResponse.IsBinary));
                            }
                            else
                            {
                                Cmd.Parameters.AddWithValue("@OriginalResponseHeaders", "");//IrSe.Response.GetHeadersAsString());
                                Cmd.Parameters.AddWithValue("@OriginalResponseBody", "");//IrSe.Response.BodyString);
                                Cmd.Parameters.AddWithValue("@BinaryOriginalResponse", 0);
                            }
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Request Req in Requests)
                        {
                            Cmd.CommandText = "INSERT INTO ProxyLog (ID , SSL, HostName, Method, URL, Edited, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Notes) VALUES (@ID , @SSL, @HostName, @Method, @URL, @Edited, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", Req.ID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(Req.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", Req.Host);
                            Cmd.Parameters.AddWithValue("@Method", Req.Method);
                            Cmd.Parameters.AddWithValue("@URL", Req.URL);
                            Cmd.Parameters.AddWithValue("@Edited", 0);
                            //Cmd.Parameters.AddWithValue("@File", Req.File);
                            Cmd.Parameters.AddWithValue("@File", Req.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", Req.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", Req.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", Req.StoredHeadersString);
                            if (Req.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Req.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Response Res in Responses)
                        {
                            Cmd.CommandText = "UPDATE ProxyLog SET Code=@Code, Length=@Length, MIME=@MIME, SetCookie=@SetCookie, Edited=@Edited, ResponseHeaders=@ResponseHeaders, ResponseBody=@ResponseBody, BinaryResponse=@BinaryResponse, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@Code", Res.Code);
                            Cmd.Parameters.AddWithValue("@Length", Res.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", Res.ContentType);
                            Cmd.Parameters.AddWithValue("@Edited", 0);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((Res.SetCookies.Count > 0)));
                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.StoredHeadersString);
                            if (Res.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(Res.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.Parameters.AddWithValue("@ID", Res.ID);
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Request Req in OriginalRequests)
                        {
                            Cmd.CommandText = "UPDATE ProxyLog SET Edited=@Edited, OriginalRequestHeaders=@OriginalRequestHeaders, OriginalRequestBody=@OriginalRequestBody, BinaryOriginalRequest=@BinaryOriginalRequest, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@ID", Req.ID);
                            //Cmd.Parameters.AddWithValue("@File", Req.File);
                            Cmd.Parameters.AddWithValue("@Edited", 1);
                            //Cmd.Parameters.AddWithValue("@OriginalRequestHeaders", Req.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@OriginalRequestHeaders", Req.StoredHeadersString);
                            if (Req.IsBinary)
                                Cmd.Parameters.AddWithValue("@OriginalRequestBody", Req.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@OriginalRequestBody", Req.BodyString);
                            //Cmd.Parameters.AddWithValue("@OriginalRequestBody", Req.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryOriginalRequest", AsInt(Req.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Response Res in OriginalResponses)
                        {
                            Cmd.CommandText = "UPDATE ProxyLog SET Edited=@Edited, OriginalResponseHeaders=@OriginalResponseHeaders, OriginalResponseBody=@OriginalResponseBody, BinaryOriginalResponse=@BinaryOriginalResponse, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@Edited", 1);
                            //Cmd.Parameters.AddWithValue("@OriginalResponseHeaders", Res.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@OriginalResponseHeaders", Res.StoredHeadersString);
                            if (Res.IsBinary)
                                Cmd.Parameters.AddWithValue("@OriginalResponseBody", Res.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@OriginalResponseBody", Res.BodyString);
                            //Cmd.Parameters.AddWithValue("@OriginalResponseBody", Res.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryOriginalResponse", AsInt(Res.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.Parameters.AddWithValue("@ID", Res.ID);
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Request Req in EditedRequests)
                        {
                            Cmd.CommandText = "UPDATE ProxyLog SET SSL=@SSL, HostName=@HostName, Method=@Method, URL=@URL, Edited=@Edited, File=@File, Parameters=@Parameters, RequestHeaders=@RequestHeaders, RequestBody=@RequestBody, BinaryRequest=@BinaryRequest, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@ID", Req.ID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(Req.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", Req.Host);
                            Cmd.Parameters.AddWithValue("@Method", Req.Method);
                            Cmd.Parameters.AddWithValue("@URL", Req.URL);
                            Cmd.Parameters.AddWithValue("@Edited", 1);
                            //Cmd.Parameters.AddWithValue("@File", Req.File);
                            Cmd.Parameters.AddWithValue("@File", Req.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", Req.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", Req.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", Req.StoredHeadersString);
                            if (Req.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Req.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Response Res in EditedResponses)
                        {
                            Cmd.CommandText = "UPDATE ProxyLog SET Code=@Code, Length=@Length, MIME=@MIME, SetCookie=@SetCookie, Edited=@Edited, ResponseHeaders=@ResponseHeaders, ResponseBody=@ResponseBody, BinaryResponse=@BinaryResponse, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@Code", Res.Code);
                            Cmd.Parameters.AddWithValue("@Length", Res.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", Res.ContentType);
                            Cmd.Parameters.AddWithValue("@Edited", 1);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((Res.SetCookies.Count > 0)));
                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.StoredHeadersString);
                            if (Res.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", Res.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", Res.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(Res.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.Parameters.AddWithValue("@ID", Res.ID);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    InsertLogs.Commit();
                }
            }
            catch(Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();
        }

        internal static void LogMTRequest(Request Request)
        {
            SQLiteConnection MT_DB = new SQLiteConnection("data source=" + TestLogFile);
            MT_DB.Open();
            try
            {
                SQLiteCommand Cmd = MT_DB.CreateCommand();
                Cmd.CommandText = "INSERT INTO TestLog (ID, SSL, HostName, Method, URL, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Notes) VALUES (@ID, @SSL, @HostName, @Method, @URL, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Notes)";
                Cmd.Parameters.AddWithValue("@ID", Request.ID);
                Cmd.Parameters.AddWithValue("@SSL", AsInt(Request.SSL));
                Cmd.Parameters.AddWithValue("@HostName", Request.Host);
                Cmd.Parameters.AddWithValue("@Method", Request.Method);
                Cmd.Parameters.AddWithValue("@URL", Request.URL);
                Cmd.Parameters.AddWithValue("@File", Request.File);
                Cmd.Parameters.AddWithValue("@Parameters", Request.GetParametersString());
                //Cmd.Parameters.AddWithValue("@RequestHeaders", Request.GetHeadersAsStringWithoutFullURL());
                Cmd.Parameters.AddWithValue("@RequestHeaders", Request.GetHeadersAsString());
                if (Request.IsBinary)
                    Cmd.Parameters.AddWithValue("@RequestBody", Request.BinaryBodyString);
                else
                    Cmd.Parameters.AddWithValue("@RequestBody", Request.BodyString);
                //Cmd.Parameters.AddWithValue("@RequestBody", Request.BodyString);
                Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Request.IsBinary));
                Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                Cmd.ExecuteNonQuery();
            }
            catch(Exception Exp)
            {
                throw Exp;
            }
            MT_DB.Close();
        }
        internal static void LogMTResponse(Response Response)
        {
            SQLiteConnection MT_DB = new SQLiteConnection("data source=" + TestLogFile);
            MT_DB.Open();
            try
            {
                SQLiteCommand Cmd = MT_DB.CreateCommand();
                Cmd.CommandText = "UPDATE TestLog SET Code=@Code, Length=@Length, MIME=@MIME, SetCookie=@SetCookie, ResponseHeaders=@ResponseHeaders, ResponseBody=@ResponseBody, BinaryResponse=@BinaryResponse, Notes=@Notes WHERE ID=@ID";
                Cmd.Parameters.AddWithValue("@Code", Response.Code);
                Cmd.Parameters.AddWithValue("@Length", Response.BodyLength);
                Cmd.Parameters.AddWithValue("@MIME", Response.ContentType);
                Cmd.Parameters.AddWithValue("@SetCookie", AsInt((Response.SetCookies.Count > 0)));
                Cmd.Parameters.AddWithValue("@ResponseHeaders", Response.GetHeadersAsString());
                if (Response.IsBinary)
                    Cmd.Parameters.AddWithValue("@ResponseBody", Response.BinaryBodyString);
                else
                    Cmd.Parameters.AddWithValue("@ResponseBody", Response.BodyString);
                //Cmd.Parameters.AddWithValue("@ResponseBody", Response.BodyString);
                Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(Response.IsBinary));
                Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                Cmd.Parameters.AddWithValue("@ID", Response.ID);
                Cmd.ExecuteNonQuery();
            }
            catch(Exception Exp)
            {
                MT_DB.Close();
                throw Exp;
            }
            MT_DB.Close();
        }

        internal static void AddToTestGroup(int ID, string Group)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            string CMD = "";
            switch(Group)
            {
                case("Red"):
                    CMD = "INSERT INTO TestGroups (Red, Green, Blue, Gray, Brown) VALUES (@ID,0,0,0,0)";
                    break;
                case ("Green"):
                    CMD = "INSERT INTO TestGroups (Red, Green, Blue, Gray, Brown) VALUES (0,@ID,0,0,0)";
                    break;
                case ("Blue"):
                    CMD = "INSERT INTO TestGroups (Red, Green, Blue, Gray, Brown) VALUES (0,0,@ID,0,0)";
                    break;
                case ("Gray"):
                    CMD = "INSERT INTO TestGroups (Red, Green, Blue, Gray, Brown) VALUES (0,0,0,@ID,0)";
                    break;
                case ("Brown"):
                    CMD = "INSERT INTO TestGroups (Red, Green, Blue, Gray, Brown) VALUES (0,0,0,0,@ID)";
                    break;
            }
            DB.Open();
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = CMD;
                Cmd.Parameters.AddWithValue("@ID", ID);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void ClearGroup(string Group)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            string CMD = "";
            switch (Group)
            {
                case ("Red"):
                    CMD = "UPDATE TestGroups SET Red=0";
                    break;
                case ("Blue"):
                    CMD = "UPDATE TestGroups SET Blue=0";
                    break;
                case ("Green"):
                    CMD = "UPDATE TestGroups SET Green=0";
                    break;
                case ("Gray"):
                    CMD = "UPDATE TestGroups SET Gray=0";
                    break;
                case ("Brown"):
                    CMD = "UPDATE TestGroups SET Brown=0";
                    break;
            }
            DB.Open();
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = CMD;
                Cmd.ExecuteNonQuery();
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void LoadTestGroups()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            
            List<int> RedGroup = new List<int>();
            List<int> BlueGroup = new List<int>();
            List<int> GreenGroup = new List<int>();
            List<int> GrayGroup = new List<int>();
            List<int> BrownGroup = new List<int>();

            DB.Open();
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = "SELECT Red, Green, Blue, Gray, Brown FROM TestGroups";
                SQLiteDataReader result = Cmd.ExecuteReader();
                while (result.Read())
                {
                    try
                    {
                        int Red = Int32.Parse(result["Red"].ToString());
                        if (Red != 0) RedGroup.Add(Red);
                    }catch { }
                    try
                    {
                        int Green = Int32.Parse(result["Green"].ToString());
                        if (Green != 0) GreenGroup.Add(Green);
                    }
                    catch { }
                    try
                    {
                        int Blue = Int32.Parse(result["Blue"].ToString());
                        if (Blue != 0) BlueGroup.Add(Blue);
                    }
                    catch { }
                    try
                    {
                        int Gray = Int32.Parse(result["Gray"].ToString());
                        if (Gray != 0) GrayGroup.Add(Gray);
                    }
                    catch { }
                    try
                    {
                        int Brown = Int32.Parse(result["Brown"].ToString());
                        if (Brown != 0) BrownGroup.Add(Brown);
                    }
                    catch { }
                }
                RedGroup.Sort(); BlueGroup.Sort(); GreenGroup.Sort(); GrayGroup.Sort(); BrownGroup.Sort();
                foreach (int ID in RedGroup)
                {
                    try
                    {
                        Session Irse = Session.FromTestLog(ID);
                        ManualTesting.RedGroupSessions.Add(ID, Irse);
                        ManualTesting.RedGroupID = ID;
                    }
                    catch { }
                }
                foreach (int ID in BlueGroup)
                {
                    try
                    {
                        Session Irse = Session.FromTestLog(ID);
                        ManualTesting.BlueGroupSessions.Add(ID, Irse);
                        ManualTesting.BlueGroupID = ID;
                    }
                    catch { }
                }
                foreach (int ID in GreenGroup)
                {
                    try
                    {
                        Session Irse = Session.FromTestLog(ID);
                        ManualTesting.GreenGroupSessions.Add(ID, Irse);
                        ManualTesting.GreenGroupID = ID;
                    }
                    catch { }
                }
                foreach (int ID in GrayGroup)
                {
                    try
                    {
                        Session Irse = Session.FromTestLog(ID);
                        ManualTesting.GrayGroupSessions.Add(ID, Irse);
                        ManualTesting.GrayGroupID = ID;
                    }
                    catch { }
                }
                foreach (int ID in BrownGroup)
                {
                    try
                    {
                        Session Irse = Session.FromTestLog(ID);
                        ManualTesting.BrownGroupSessions.Add(ID, Irse);
                        ManualTesting.BrownGroupID = ID;
                    }
                    catch { }
                }
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void LogShellMessages(List<Session> IronSessions, List<Request> Requests, List<Response> Responses)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + ShellLogFile);
            Log.Open();
            try
            {
                using (SQLiteTransaction Create = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        //Insert Request/Response in to DB
                        foreach (Session IrSe in IronSessions)
                        {
                            Cmd.CommandText = "INSERT INTO ShellLog (ID , SSL, HostName, Method, URL, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Code, Length, MIME, SetCookie, ResponseHeaders, ResponseBody, BinaryResponse, Notes) VALUES (@ID , @SSL, @HostName, @Method, @URL, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Code, @Length, @MIME, @SetCookie, @ResponseHeaders, @ResponseBody, @BinaryResponse, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", IrSe.Request.ID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(IrSe.Request.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", IrSe.Request.Host);
                            Cmd.Parameters.AddWithValue("@Method", IrSe.Request.Method);
                            Cmd.Parameters.AddWithValue("@URL", IrSe.Request.URL);
                            Cmd.Parameters.AddWithValue("@File", IrSe.Request.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", IrSe.Request.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.GetHeadersAsStringWithoutFullURL());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.StoredHeadersString);
                            if (IrSe.Request.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(IrSe.Request.IsBinary));
                            Cmd.Parameters.AddWithValue("@Code", IrSe.Response.Code);
                            Cmd.Parameters.AddWithValue("@Length", IrSe.Response.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", IrSe.Response.ContentType);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((IrSe.Response.SetCookies.Count > 0)));

                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.StoredHeadersString);
                            if (IrSe.Response.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(IrSe.Response.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Request Req in Requests)
                        {
                            Cmd.CommandText = "INSERT INTO ShellLog (ID , SSL, HostName, Method, URL, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Notes) VALUES (@ID , @SSL, @HostName, @Method, @URL, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", Req.ID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(Req.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", Req.Host);
                            Cmd.Parameters.AddWithValue("@Method", Req.Method);
                            Cmd.Parameters.AddWithValue("@URL", Req.URL);
                            Cmd.Parameters.AddWithValue("@File", Req.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", Req.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", Req.GetHeadersAsStringWithoutFullURL());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", Req.StoredHeadersString);
                            if (Req.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Req.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Response Res in Responses)
                        {
                            Cmd.CommandText = "UPDATE ShellLog SET Code=@Code, Length=@Length, MIME=@MIME, SetCookie=@SetCookie, ResponseHeaders=@ResponseHeaders, ResponseBody=@ResponseBody, BinaryResponse=@BinaryResponse, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@Code", Res.Code);
                            Cmd.Parameters.AddWithValue("@Length", Res.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", Res.ContentType);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((Res.SetCookies.Count > 0)));
                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.StoredHeadersString);
                            if (Res.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(Res.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.Parameters.AddWithValue("@ID", Res.ID);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();
        }

        internal static void LogProbeMessages(List<Session> IronSessions, List<Request> Requests, List<Response> Responses)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + ProbeLogFile);
            Log.Open();
            try
            {
                using (SQLiteTransaction Create = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        //Insert Request/Response in to DB
                        foreach (Session IrSe in IronSessions)
                        {
                            Cmd.CommandText = "INSERT INTO ProbeLog (ID , SSL, HostName, Method, URL, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Code, Length, MIME, SetCookie, ResponseHeaders, ResponseBody, BinaryResponse, Notes) VALUES (@ID , @SSL, @HostName, @Method, @URL, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Code, @Length, @MIME, @SetCookie, @ResponseHeaders, @ResponseBody, @BinaryResponse, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", IrSe.Request.ID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(IrSe.Request.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", IrSe.Request.Host);
                            Cmd.Parameters.AddWithValue("@Method", IrSe.Request.Method);
                            Cmd.Parameters.AddWithValue("@URL", IrSe.Request.URL);
                            Cmd.Parameters.AddWithValue("@File", IrSe.Request.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", IrSe.Request.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.GetHeadersAsStringWithoutFullURL());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.StoredHeadersString);
                            if (IrSe.Request.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(IrSe.Request.IsBinary));
                            Cmd.Parameters.AddWithValue("@Code", IrSe.Response.Code);
                            Cmd.Parameters.AddWithValue("@Length", IrSe.Response.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", IrSe.Response.ContentType);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((IrSe.Response.SetCookies.Count > 0)));

                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.StoredHeadersString);
                            if (IrSe.Response.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(IrSe.Response.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Request Req in Requests)
                        {
                            Cmd.CommandText = "INSERT INTO ProbeLog (ID , SSL, HostName, Method, URL, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Notes) VALUES (@ID , @SSL, @HostName, @Method, @URL, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", Req.ID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(Req.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", Req.Host);
                            Cmd.Parameters.AddWithValue("@Method", Req.Method);
                            Cmd.Parameters.AddWithValue("@URL", Req.URL);
                            Cmd.Parameters.AddWithValue("@File", Req.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", Req.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", Req.GetHeadersAsStringWithoutFullURL());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", Req.StoredHeadersString);
                            if (Req.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Req.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Response Res in Responses)
                        {
                            Cmd.CommandText = "UPDATE ProbeLog SET Code=@Code, Length=@Length, MIME=@MIME, SetCookie=@SetCookie, ResponseHeaders=@ResponseHeaders, ResponseBody=@ResponseBody, BinaryResponse=@BinaryResponse, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@Code", Res.Code);
                            Cmd.Parameters.AddWithValue("@Length", Res.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", Res.ContentType);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((Res.SetCookies.Count > 0)));
                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.StoredHeadersString);
                            if (Res.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(Res.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.Parameters.AddWithValue("@ID", Res.ID);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch (Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();
        }

        internal static void LogScanMessages(List<Session> IronSessions, List<Request> Requests, List<Response> Responses)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + ScanLogFile);
            Log.Open();
            try
            {
                using (SQLiteTransaction Create = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        //Insert Request/Response in to DB
                        foreach (Session IrSe in IronSessions)
                        {
                            Cmd.CommandText = "INSERT INTO ScanLog (ID , ScanID, SSL, HostName, Method, URL, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Code, Length, MIME, SetCookie, ResponseHeaders, ResponseBody, BinaryResponse, Notes) VALUES (@ID , @ScanID, @SSL, @HostName, @Method, @URL, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Code, @Length, @MIME, @SetCookie, @ResponseHeaders, @ResponseBody, @BinaryResponse, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", IrSe.Request.ID);
                            Cmd.Parameters.AddWithValue("@ScanID", IrSe.Request.ScanID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(IrSe.Request.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", IrSe.Request.Host);
                            Cmd.Parameters.AddWithValue("@Method", IrSe.Request.Method);
                            Cmd.Parameters.AddWithValue("@URL", IrSe.Request.URL);
                            Cmd.Parameters.AddWithValue("@File", IrSe.Request.StoredFile);
                            Cmd.Parameters.AddWithValue("@Parameters", IrSe.Request.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.GetHeadersAsStringWithoutFullURL());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", IrSe.Request.StoredHeadersString);
                            if (IrSe.Request.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", IrSe.Request.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(IrSe.Request.IsBinary));
                            Cmd.Parameters.AddWithValue("@Code", IrSe.Response.Code);
                            Cmd.Parameters.AddWithValue("@Length", IrSe.Response.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", IrSe.Response.ContentType);
                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", IrSe.Response.StoredHeadersString);
                            if (IrSe.Response.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", IrSe.Response.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(IrSe.Response.IsBinary));
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((IrSe.Response.SetCookies.Count > 0)));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Request Req in Requests)
                        {
                            Cmd.CommandText = "INSERT INTO ScanLog (ID , ScanID, SSL, HostName, Method, URL, File, Parameters, RequestHeaders, RequestBody, BinaryRequest, Notes) VALUES (@ID , @ScanID, @SSL, @HostName, @Method, @URL, @File, @Parameters, @RequestHeaders, @RequestBody, @BinaryRequest, @Notes)";
                            Cmd.Parameters.AddWithValue("@ID", Req.ID);
                            Cmd.Parameters.AddWithValue("@ScanID", Req.ScanID);
                            Cmd.Parameters.AddWithValue("@SSL", AsInt(Req.SSL));
                            Cmd.Parameters.AddWithValue("@HostName", Req.Host);
                            Cmd.Parameters.AddWithValue("@Method", Req.Method);
                            Cmd.Parameters.AddWithValue("@URL", Req.URL);
                            Cmd.Parameters.AddWithValue("@File", Req.File);
                            Cmd.Parameters.AddWithValue("@Parameters", Req.StoredParameters);
                            //Cmd.Parameters.AddWithValue("@RequestHeaders", Req.GetHeadersAsStringWithoutFullURL());
                            Cmd.Parameters.AddWithValue("@RequestHeaders", Req.StoredHeadersString);
                            if (Req.IsBinary)
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            //Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Req.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (Response Res in Responses)
                        {
                            Cmd.CommandText = "UPDATE ScanLog SET Code=@Code, Length=@Length, MIME=@MIME, SetCookie=@SetCookie, ResponseHeaders=@ResponseHeaders, ResponseBody=@ResponseBody, BinaryResponse=@BinaryResponse, Notes=@Notes WHERE ID=@ID";
                            Cmd.Parameters.AddWithValue("@Code", Res.Code);
                            Cmd.Parameters.AddWithValue("@Length", Res.BodyLength);
                            Cmd.Parameters.AddWithValue("@MIME", Res.ContentType);
                            Cmd.Parameters.AddWithValue("@SetCookie", AsInt((Res.SetCookies.Count > 0)));
                            //Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.GetHeadersAsString());
                            Cmd.Parameters.AddWithValue("@ResponseHeaders", Res.StoredHeadersString);
                            if (Res.IsBinary)
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.StoredBinaryBodyString);
                            else
                                Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            //Cmd.Parameters.AddWithValue("@ResponseBody", Res.BodyString);
                            Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(Res.IsBinary));
                            Cmd.Parameters.AddWithValue("@Notes", "Some Notes");
                            Cmd.Parameters.AddWithValue("@ID", Res.ID);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();
        }
        internal static void CreateScan(int ScanID, Request Req)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            DB.Open();
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = "INSERT INTO ScanQueue (ScanID, RequestHeaders, RequestBody, BinaryRequest, Status, Method, URL) VALUES (@ScanID, @RequestHeaders, @RequestBody, @BinaryRequest, @Status, @Method, @URL)";
                Cmd.Parameters.AddWithValue("@ScanID", ScanID);
                Cmd.Parameters.AddWithValue("@RequestHeaders", Req.GetHeadersAsString());
                if(Req.IsBinary)
                    Cmd.Parameters.AddWithValue("@RequestBody", Req.BinaryBodyString);
                else
                    Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Req.IsBinary));
                Cmd.Parameters.AddWithValue("@Status", "Not Started");
                Cmd.Parameters.AddWithValue("@Method", Req.Method);
                Cmd.Parameters.AddWithValue("@URL", Req.URL);
                Cmd.ExecuteNonQuery();
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();

            //CreateScan(ScanID, Req, "Not Started", "", "", "", "");
        }

        internal static void LogTraces(List<IronTrace> Traces)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + TraceLogFile);
            Log.Open();
            try
            {
                using (SQLiteTransaction Create = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        foreach (IronTrace Trace in Traces)
                        {
                            Cmd.CommandText = "INSERT INTO Trace (ID, Time, Date, ThreadID, Source, Message) VALUES (@ID, @Time, @Date, @ThreadID, @Source, @Message)";
                            Cmd.Parameters.AddWithValue("@ID", Trace.ID.ToString());
                            Cmd.Parameters.AddWithValue("@Time", Trace.Time);
                            Cmd.Parameters.AddWithValue("@Date", Trace.Date);
                            Cmd.Parameters.AddWithValue("@ThreadID", Trace.ThreadID.ToString());
                            Cmd.Parameters.AddWithValue("@Source", Trace.Source);
                            Cmd.Parameters.AddWithValue("@Message", Trace.Message);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();
        }

        internal static void LogScanTraces(List<IronTrace> Traces)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + TraceLogFile);
            Log.Open();
            try
            {
                using (SQLiteTransaction Create = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        foreach (IronTrace Trace in Traces)
                        {
                            Cmd.CommandText = "INSERT INTO ScanTrace (ID, ScanID, PluginName, Section, Parameter, Title, Message) VALUES (@ID, @ScanID, @PluginName, @Section, @Parameter, @Title, @Message)";
                            Cmd.Parameters.AddWithValue("@ID", Trace.ID.ToString());
                            Cmd.Parameters.AddWithValue("@ScanID", Trace.ScanID.ToString());
                            Cmd.Parameters.AddWithValue("@PluginName", Trace.PluginName);
                            Cmd.Parameters.AddWithValue("@Section", Trace.Section);
                            Cmd.Parameters.AddWithValue("@Parameter", Trace.Parameter);
                            Cmd.Parameters.AddWithValue("@Title", Trace.Title);
                            Cmd.Parameters.AddWithValue("@Message", Trace.Message);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch (Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();
        }

        internal static void UpdateScan(int ScanID, Request Req, string Status, string InjectionPoints, string FormatPlugin, string ScanPlugins, string SessionPlugin)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            DB.Open();
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = "UPDATE ScanQueue SET RequestHeaders=@RequestHeaders, RequestBody=@RequestBody, BinaryRequest=@BinaryRequest, Status=@Status, Method=@Method, URL=@URL, SessionPlugin=@SessionPlugin, InjectionPoints=@InjectionPoints, FormatPlugin=@FormatPlugin, ScanPlugins=@ScanPlugins WHERE ScanID=@ScanID";
                Cmd.Parameters.AddWithValue("@ScanID", ScanID);
                Cmd.Parameters.AddWithValue("@RequestHeaders", Req.GetHeadersAsString());
                if(Req.IsBinary)
                    Cmd.Parameters.AddWithValue("@RequestBody", Req.BinaryBodyString);
                else
                    Cmd.Parameters.AddWithValue("@RequestBody", Req.BodyString);
                Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(Req.IsBinary));
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@Method", Req.Method);
                Cmd.Parameters.AddWithValue("@URL", Req.URL);
                Cmd.Parameters.AddWithValue("@SessionPlugin", SessionPlugin);
                Cmd.Parameters.AddWithValue("@InjectionPoints", InjectionPoints);
                Cmd.Parameters.AddWithValue("@FormatPlugin", FormatPlugin);
                Cmd.Parameters.AddWithValue("@ScanPlugins", ScanPlugins);
                Cmd.ExecuteNonQuery();
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }
        
        internal static void UpdateScanStatus(int ScanID, string Status)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            DB.Open();
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = "UPDATE ScanQueue SET Status=@Status WHERE ScanID=@ScanID";
                Cmd.Parameters.AddWithValue("@ScanID", ScanID);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.ExecuteNonQuery();
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void UpdateScanStatus(List<int> ScanIDs, string Status)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + IronProjectFile);
            Log.Open();

            try
            {
                using (SQLiteTransaction Create = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        foreach (int ScanID in ScanIDs)
                        {
                            Cmd.CommandText = "UPDATE ScanQueue SET Status=@Status WHERE ScanID=@ScanID";
                            Cmd.Parameters.AddWithValue("@ScanID", ScanID);
                            Cmd.Parameters.AddWithValue("@Status", Status);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch (Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();

        }

        internal static void LogPluginResults(List<PluginResult> Results)
        {
            SQLiteConnection Log = new SQLiteConnection("data source=" + PluginResultsLogFile);
            Log.Open();
            try
            {
                using (SQLiteTransaction Create = Log.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(Log))
                    {
                        foreach (PluginResult PR in Results)
                        {
                            Cmd.CommandText = "INSERT INTO PluginResult (ID, HostName, Title, Plugin, Summary, Severity, Confidence, Type, UniquenessString) VALUES (@ID,@HostName,@Title,@Plugin,@Summary,@Severity,@Confidence,@Type,@UniquenessString)";
                            Cmd.Parameters.AddWithValue("@ID", PR.Id);
                            Cmd.Parameters.AddWithValue("@HostName", PR.AffectedHost);
                            Cmd.Parameters.AddWithValue("@Title", PR.Title);
                            Cmd.Parameters.AddWithValue("@Plugin", PR.Plugin);
                            Cmd.Parameters.AddWithValue("@Summary", PR.Summary);
                            Cmd.Parameters.AddWithValue("@Severity", GetSeverity(PR.Severity));
                            Cmd.Parameters.AddWithValue("@Confidence", GetConfidence(PR.Confidence));
                            Cmd.Parameters.AddWithValue("@Type", GetResultType(PR.ResultType));
                            Cmd.Parameters.AddWithValue("@UniquenessString", PR.Signature);
                            Cmd.ExecuteNonQuery();

                            Cmd.CommandText = "INSERT INTO Triggers (ID, RequestTrigger, RequestHeaders, RequestBody, BinaryRequest, ResponseTrigger, ResponseHeaders, ResponseBody, BinaryResponse) VALUES (@ID, @RequestTrigger, @RequestHeaders, @RequestBody, @BinaryRequest, @ResponseTrigger, @ResponseHeaders, @ResponseBody, @BinaryResponse)";
                            foreach (Trigger T in PR.Triggers.GetTriggers())
                            {
                                Cmd.Parameters.AddWithValue("@ID", PR.Id);
                                Cmd.Parameters.AddWithValue("@RequestTrigger", T.RequestTrigger);
                                Cmd.Parameters.AddWithValue("@RequestHeaders", T.Request.StoredHeadersString);// .GetHeadersAsString());
                                if (T.Request.IsBinary)
                                    Cmd.Parameters.AddWithValue("@RequestBody", T.Request.StoredBinaryBodyString);
                                else
                                    Cmd.Parameters.AddWithValue("@RequestBody", T.Request.BodyString);
                                Cmd.Parameters.AddWithValue("@BinaryRequest", AsInt(T.Request.IsBinary));
                                if (T.Response != null)
                                {
                                    Cmd.Parameters.AddWithValue("@ResponseTrigger", T.ResponseTrigger);
                                    Cmd.Parameters.AddWithValue("@ResponseHeaders", T.Response.StoredHeadersString);// .GetHeadersAsString());
                                    if (T.Response.IsBinary)
                                        Cmd.Parameters.AddWithValue("@ResponseBody", T.Response.StoredBinaryBodyString);
                                    else
                                        Cmd.Parameters.AddWithValue("@ResponseBody", T.Response.BodyString);
                                    Cmd.Parameters.AddWithValue("@BinaryResponse", AsInt(T.Response.IsBinary));
                                }
                                else
                                {
                                    Cmd.Parameters.AddWithValue("@ResponseTrigger", "");
                                    Cmd.Parameters.AddWithValue("@ResponseHeaders", "");
                                    Cmd.Parameters.AddWithValue("@ResponseBody", "");
                                    Cmd.Parameters.AddWithValue("@BinaryResponse", "");
                                }
                                Cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                Log.Close();
                throw Exp;
            }
            Log.Close();
        }
        internal static void LogException(IronException IrEx)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ExceptionsLogFile);
            DB.Open();
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = "INSERT INTO Exceptions (ID, Title, Message, StackTrace) VALUES (@ID, @Title, @Message, @StackTrace)";
                Cmd.Parameters.AddWithValue("@ID", IrEx.ID);
                Cmd.Parameters.AddWithValue("@Title", IrEx.Title);
                Cmd.Parameters.AddWithValue("@Message", IrEx.Message);
                Cmd.Parameters.AddWithValue("@StackTrace", IrEx.StackTrace);
                Cmd.ExecuteNonQuery();
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreProxyConfig()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM IronProxy";
                        Cmd.ExecuteNonQuery();

                        Cmd.CommandText = "INSERT INTO IronProxy (LoopBack, SystemProxy, Port) VALUES (@LoopBack, @SystemProxy, @Port)";
                        Cmd.Parameters.AddWithValue("@LoopBack", AsInt(IronProxy.LoopBackOnly));
                        Cmd.Parameters.AddWithValue("@SystemProxy", AsInt(IronProxy.SystemProxy));
                        Cmd.Parameters.AddWithValue("@Port", IronProxy.Port.ToString());
                        Cmd.ExecuteNonQuery();
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreUpstreamProxyConfig()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM UpstreamProxy";
                        Cmd.ExecuteNonQuery();

                        Cmd.CommandText = "INSERT INTO UpstreamProxy (Use, IP, Port) VALUES (@Use, @IP, @Port)";
                        Cmd.Parameters.AddWithValue("@Use", AsInt(IronProxy.UseUpstreamProxy));
                        Cmd.Parameters.AddWithValue("@IP", IronProxy.UpstreamProxyIP);
                        Cmd.Parameters.AddWithValue("@Port", IronProxy.UpstreamProxyPort.ToString());
                        Cmd.ExecuteNonQuery();
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreRequestTextContentTypesConfig()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM TextRequestTypes";
                        Cmd.ExecuteNonQuery();
                        foreach (string Type in Request.TextContentTypes)
                        {
                            Cmd.CommandText = "INSERT INTO TextRequestTypes (Type) VALUES (@Type)";
                            Cmd.Parameters.AddWithValue("@Type", Type);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreResponseTextContentTypesConfig()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM TextResponseTypes";
                        Cmd.ExecuteNonQuery();
                        foreach (string Type in Response.TextContentTypes)
                        {
                            Cmd.CommandText = "INSERT INTO TextResponseTypes (Type) VALUES (@Type)";
                            Cmd.Parameters.AddWithValue("@Type", Type);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreScriptPathsConfig()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM PyPath";
                        Cmd.ExecuteNonQuery();
                        Cmd.CommandText = "DELETE FROM RbPath";
                        Cmd.ExecuteNonQuery();
                        foreach (string Path in IronScripting.PyPaths)
                        {
                            Cmd.CommandText = "INSERT INTO PyPath (Path) VALUES (@Path)";
                            Cmd.Parameters.AddWithValue("@Path", Path);
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (string Path in IronScripting.RbPaths)
                        {
                            Cmd.CommandText = "INSERT INTO RbPath (Path) VALUES (@Path)";
                            Cmd.Parameters.AddWithValue("@Path", Path);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreScriptCommandsConfig()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM PyStartCommands";
                        Cmd.ExecuteNonQuery();
                        Cmd.CommandText = "DELETE FROM RbStartCommands";
                        Cmd.ExecuteNonQuery();
                        foreach (string Command in IronScripting.PyCommands)
                        {
                            Cmd.CommandText = "INSERT INTO PyStartCommands (Command) VALUES (@Command)";
                            Cmd.Parameters.AddWithValue("@Command", Command);
                            Cmd.ExecuteNonQuery();
                        }
                        foreach (string Command in IronScripting.RbCommands)
                        {
                            Cmd.CommandText = "INSERT INTO RbStartCommands (Command) VALUES (@Command)";
                            Cmd.Parameters.AddWithValue("@Command", Command);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreInterceptRules()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM InterceptRules";
                        Cmd.ExecuteNonQuery();

                        Cmd.CommandText = "INSERT INTO InterceptRules (Get, Post, OtherMethods, Html, JS, CSS, Xml, JSON, OtherText, Img, OtherBinary, Code200, Code2xx, Code301_2, Code3xx, Code403, Code4xx, Code500, Code5xx, FileExt, FileExtPlus, FileExtMinus, PlusFileExts, MinusFileExts, Host, HostPlus, HostMinus, PlusHosts, MinusHosts, RequestKeyword, RequestKeywordPlus, RequestKeywordMinus, PlusRequestKeyword, MinusRequestKeyword, ResponseKeyword, ResponseKeywordPlus, ResponseKeywordMinus, PlusResponseKeyword, MinusResponseKeyword, RequestRulesOnResponse) VALUES (@Get, @Post, @OtherMethods, @Html, @JS, @CSS, @Xml, @JSON, @OtherText, @Img, @OtherBinary, @Code200, @Code2xx, @Code301_2, @Code3xx, @Code403, @Code4xx, @Code500, @Code5xx, @FileExt, @FileExtPlus, @FileExtMinus, @PlusFileExts, @MinusFileExts, @Host, @HostPlus, @HostMinus, @PlusHosts, @MinusHosts, @RequestKeyword, @RequestKeywordPlus, @RequestKeywordMinus, @PlusRequestKeyword, @MinusRequestKeyword, @ResponseKeyword, @ResponseKeywordPlus, @ResponseKeywordMinus, @PlusResponseKeyword, @MinusResponseKeyword, @RequestRulesOnResponse)";
                        Cmd.Parameters.AddWithValue("@Get", AsInt(IronProxy.InterceptGET));
                        Cmd.Parameters.AddWithValue("@Post", AsInt(IronProxy.InterceptPOST));
                        Cmd.Parameters.AddWithValue("@OtherMethods", AsInt(IronProxy.InterceptOtherMethods));
                        Cmd.Parameters.AddWithValue("@Html", AsInt(IronProxy.InterceptHTML));
                        Cmd.Parameters.AddWithValue("@JS", AsInt(IronProxy.InterceptJS));
                        Cmd.Parameters.AddWithValue("@CSS", AsInt(IronProxy.InterceptCSS));
                        Cmd.Parameters.AddWithValue("@Xml", AsInt(IronProxy.InterceptXML));
                        Cmd.Parameters.AddWithValue("@JSON", AsInt(IronProxy.InterceptJSON));
                        Cmd.Parameters.AddWithValue("@OtherText", AsInt(IronProxy.InterceptOtherText));
                        Cmd.Parameters.AddWithValue("@Img", AsInt(IronProxy.InterceptImg));
                        Cmd.Parameters.AddWithValue("@OtherBinary", AsInt(IronProxy.InterceptOtherBinary));
                        Cmd.Parameters.AddWithValue("@Code200", AsInt(IronProxy.Intercept200));
                        Cmd.Parameters.AddWithValue("@Code2xx", AsInt(IronProxy.Intercept2xx));
                        Cmd.Parameters.AddWithValue("@Code301_2", AsInt(IronProxy.Intercept301_2));
                        Cmd.Parameters.AddWithValue("@Code3xx", AsInt(IronProxy.Intercept3xx));
                        Cmd.Parameters.AddWithValue("@Code403", AsInt(IronProxy.Intercept403));
                        Cmd.Parameters.AddWithValue("@Code4xx", AsInt(IronProxy.Intercept4xx));
                        Cmd.Parameters.AddWithValue("@Code500", AsInt(IronProxy.Intercept500));
                        Cmd.Parameters.AddWithValue("@Code5xx", AsInt(IronProxy.Intercept5xx));
                        Cmd.Parameters.AddWithValue("@FileExt", AsInt(IronProxy.InterceptCheckFileExtensions));
                        Cmd.Parameters.AddWithValue("@FileExtPlus", AsInt(IronProxy.InterceptCheckFileExtensionsPlus));
                        Cmd.Parameters.AddWithValue("@FileExtMinus", AsInt(IronProxy.InterceptCheckFileExtensionsMinus));
                        Cmd.Parameters.AddWithValue("@PlusFileExts", Tools.ListToCsv(IronProxy.InterceptFileExtensions));
                        Cmd.Parameters.AddWithValue("@MinusFileExts", Tools.ListToCsv(IronProxy.DontInterceptFileExtensions));
                        Cmd.Parameters.AddWithValue("@Host", AsInt(IronProxy.InterceptCheckHostNames));
                        Cmd.Parameters.AddWithValue("@HostPlus", AsInt(IronProxy.InterceptCheckHostNamesPlus));
                        Cmd.Parameters.AddWithValue("@HostMinus", AsInt(IronProxy.InterceptCheckHostNamesMinus));
                        Cmd.Parameters.AddWithValue("@PlusHosts", Tools.ListToCsv(IronProxy.InterceptHostNames));
                        Cmd.Parameters.AddWithValue("@MinusHosts", Tools.ListToCsv(IronProxy.DontInterceptHostNames));
                        Cmd.Parameters.AddWithValue("@RequestKeyword", AsInt(IronProxy.InterceptCheckRequestWithKeyword));
                        Cmd.Parameters.AddWithValue("@RequestKeywordPlus", AsInt(IronProxy.InterceptCheckRequestWithKeywordPlus));
                        Cmd.Parameters.AddWithValue("@RequestKeywordMinus", AsInt(IronProxy.InterceptCheckRequestWithKeywordMinus));
                        Cmd.Parameters.AddWithValue("@PlusRequestKeyword", IronProxy.InterceptRequestWithKeyword);
                        Cmd.Parameters.AddWithValue("@MinusRequestKeyword", IronProxy.DontInterceptRequestWithKeyword);
                        Cmd.Parameters.AddWithValue("@ResponseKeyword", AsInt(IronProxy.InterceptCheckResponseWithKeyword));
                        Cmd.Parameters.AddWithValue("@ResponseKeywordPlus", AsInt(IronProxy.InterceptCheckResponseWithKeywordPlus));
                        Cmd.Parameters.AddWithValue("@ResponseKeywordMinus", AsInt(IronProxy.InterceptCheckResponseWithKeywordMinus));
                        Cmd.Parameters.AddWithValue("@PlusResponseKeyword", IronProxy.InterceptResponseWithKeyword);
                        Cmd.Parameters.AddWithValue("@MinusResponseKeyword", IronProxy.DontInterceptResponseWithKeyword);
                        Cmd.Parameters.AddWithValue("@RequestRulesOnResponse", AsInt(IronProxy.RequestRulesOnResponse));
                        Cmd.ExecuteNonQuery();
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreDisplayRules()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM DisplayRules";
                        Cmd.ExecuteNonQuery();

                        Cmd.CommandText = "INSERT INTO DisplayRules (Get, Post, OtherMethods, Html, JS, CSS, Xml, JSON, OtherText, Img, OtherBinary, Code200, Code2xx, Code301_2, Code3xx, Code403, Code4xx, Code500, Code5xx, FileExt, FileExtPlus, FileExtMinus, PlusFileExts, MinusFileExts, Host, HostPlus, HostMinus, PlusHosts, MinusHosts) VALUES (@Get, @Post, @OtherMethods, @Html, @JS, @CSS, @Xml, @JSON, @OtherText, @Img, @OtherBinary, @Code200, @Code2xx, @Code301_2, @Code3xx, @Code403, @Code4xx, @Code500, @Code5xx, @FileExt, @FileExtPlus, @FileExtMinus, @PlusFileExts, @MinusFileExts, @Host, @HostPlus, @HostMinus, @PlusHosts, @MinusHosts)";
                        Cmd.Parameters.AddWithValue("@Get", AsInt(IronProxy.DisplayGET));
                        Cmd.Parameters.AddWithValue("@Post", AsInt(IronProxy.DisplayPOST));
                        Cmd.Parameters.AddWithValue("@OtherMethods", AsInt(IronProxy.DisplayOtherMethods));
                        Cmd.Parameters.AddWithValue("@Html", AsInt(IronProxy.DisplayHTML));
                        Cmd.Parameters.AddWithValue("@JS", AsInt(IronProxy.DisplayJS));
                        Cmd.Parameters.AddWithValue("@CSS", AsInt(IronProxy.DisplayCSS));
                        Cmd.Parameters.AddWithValue("@Xml", AsInt(IronProxy.DisplayXML));
                        Cmd.Parameters.AddWithValue("@JSON", AsInt(IronProxy.DisplayJSON));
                        Cmd.Parameters.AddWithValue("@OtherText", AsInt(IronProxy.DisplayOtherText));
                        Cmd.Parameters.AddWithValue("@Img", AsInt(IronProxy.DisplayImg));
                        Cmd.Parameters.AddWithValue("@OtherBinary", AsInt(IronProxy.DisplayOtherBinary));
                        Cmd.Parameters.AddWithValue("@Code200", AsInt(IronProxy.Display200));
                        Cmd.Parameters.AddWithValue("@Code2xx", AsInt(IronProxy.Display2xx));
                        Cmd.Parameters.AddWithValue("@Code301_2", AsInt(IronProxy.Display301_2));
                        Cmd.Parameters.AddWithValue("@Code3xx", AsInt(IronProxy.Display3xx));
                        Cmd.Parameters.AddWithValue("@Code403", AsInt(IronProxy.Display403));
                        Cmd.Parameters.AddWithValue("@Code4xx", AsInt(IronProxy.Display4xx));
                        Cmd.Parameters.AddWithValue("@Code500", AsInt(IronProxy.Display500));
                        Cmd.Parameters.AddWithValue("@Code5xx", AsInt(IronProxy.Display5xx));
                        Cmd.Parameters.AddWithValue("@FileExt", AsInt(IronProxy.DisplayCheckFileExtensions));
                        Cmd.Parameters.AddWithValue("@FileExtPlus", AsInt(IronProxy.DisplayCheckFileExtensionsPlus));
                        Cmd.Parameters.AddWithValue("@FileExtMinus", AsInt(IronProxy.DisplayCheckFileExtensionsMinus));
                        Cmd.Parameters.AddWithValue("@PlusFileExts", Tools.ListToCsv(IronProxy.DisplayFileExtensions));
                        Cmd.Parameters.AddWithValue("@MinusFileExts", Tools.ListToCsv(IronProxy.DontDisplayFileExtensions));
                        Cmd.Parameters.AddWithValue("@Host", AsInt(IronProxy.DisplayCheckHostNames));
                        Cmd.Parameters.AddWithValue("@HostPlus", AsInt(IronProxy.DisplayCheckHostNamesPlus));
                        Cmd.Parameters.AddWithValue("@HostMinus", AsInt(IronProxy.DisplayCheckHostNamesMinus));
                        Cmd.Parameters.AddWithValue("@PlusHosts", Tools.ListToCsv(IronProxy.DisplayHostNames));
                        Cmd.Parameters.AddWithValue("@MinusHosts", Tools.ListToCsv(IronProxy.DontDisplayHostNames));
                        Cmd.ExecuteNonQuery();
                    }
                    Create.Commit();
                }
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreJSTaintConfig()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM JSTaintConfig";
                        Cmd.ExecuteNonQuery();
                        
                        List<List<string>> Lists = new List<List<string>>() { new List<string>(IronJint.DefaultSourceObjects), new List<string>(IronJint.DefaultSinkObjects), new List<string>(IronJint.DefaultSourceReturningMethods), new List<string>(IronJint.DefaultSinkReturningMethods), new List<string>(IronJint.DefaultArgumentReturningMethods), new List<string>(IronJint.DefaultArgumentAssignedASourceMethods), new List<string>(IronJint.DefaultArgumentAssignedToSinkMethods) };
                        int MaxCount = 0;
                        foreach (List<string> List in Lists)
                        {
                            if (List.Count > MaxCount) MaxCount = List.Count;
                        }
                        foreach (List<string> List in Lists)
                        {
                            while (List.Count < MaxCount)
                            {
                                List.Add("");
                            }
                        }
                        Cmd.CommandText = "INSERT INTO JSTaintConfig (SourceObjects, SinkObjects, ArgumentAssignedASourceMethods, ArgumentAssignedToSinkMethods, SourceReturningMethods, SinkReturningMethods, ArgumentReturningMethods) VALUES (@SourceObjects, @SinkObjects, @ArgumentAssignedASourceMethods, @ArgumentAssignedToSinkMethods, @SourceReturningMethods, @SinkReturningMethods, @ArgumentReturningMethods)";
                        for (int i = 0; i < MaxCount; i++)
                        {
                            Cmd.Parameters.AddWithValue("@SourceObjects", Lists[0][i]);
                            Cmd.Parameters.AddWithValue("@SinkObjects", Lists[1][i]);
                            Cmd.Parameters.AddWithValue("@ArgumentAssignedASourceMethods", Lists[5][i]);
                            Cmd.Parameters.AddWithValue("@ArgumentAssignedToSinkMethods", Lists[6][i]);
                            Cmd.Parameters.AddWithValue("@SourceReturningMethods", Lists[2][i]);
                            Cmd.Parameters.AddWithValue("@SinkReturningMethods", Lists[3][i]);
                            Cmd.Parameters.AddWithValue("@ArgumentReturningMethods", Lists[4][i]);
                            Cmd.ExecuteNonQuery();
                        }
                    }
                    Create.Commit();
                }
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StoreScannerSettings()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM ScannerSettings";
                        Cmd.ExecuteNonQuery();

                        Cmd.CommandText = "INSERT INTO ScannerSettings (MaxScannerThreads, MaxCrawlerThreads, UserAgent) VALUES (@MaxScannerThreads, @MaxCrawlerThreads, @UserAgent)";
                        Cmd.Parameters.AddWithValue("@MaxScannerThreads", Scanner.MaxParallelScanCount);
                        Cmd.Parameters.AddWithValue("@MaxCrawlerThreads", Crawler.MaxCrawlThreads);
                        Cmd.Parameters.AddWithValue("@UserAgent", Crawler.UserAgent);
                        Cmd.ExecuteNonQuery();
                    }
                    Create.Commit();
                }
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static void StorePassiveAnalysisSettings()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            try
            {
                using (SQLiteTransaction Create = DB.BeginTransaction())
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand(DB))
                    {
                        Cmd.CommandText = "DELETE FROM PassiveAnalysisSettings";
                        Cmd.ExecuteNonQuery();

                        Cmd.CommandText = "INSERT INTO PassiveAnalysisSettings (Proxy, Shell, Test, Scan, Probe) VALUES (@Proxy, @Shell, @Test, @Scan, @Probe)";
                        Cmd.Parameters.AddWithValue("@Proxy", AsInt(PassiveChecker.RunOnProxyTraffic));
                        Cmd.Parameters.AddWithValue("@Shell", AsInt(PassiveChecker.RunOnShellTraffic));
                        Cmd.Parameters.AddWithValue("@Test", AsInt(PassiveChecker.RunOnTestTraffic));
                        Cmd.Parameters.AddWithValue("@Scan", AsInt(PassiveChecker.RunOnScanTraffic));
                        Cmd.Parameters.AddWithValue("@Probe", AsInt(PassiveChecker.RunOnProbeTraffic));
                        Cmd.ExecuteNonQuery();
                    }
                    Create.Commit();
                }
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
        }

        internal static IronLogRecord GetRecordFromProxyLog(int ID)
        {
            string DataSource = "data source=" + ProxyLogFile;
            string Cmd = "SELECT SSL, RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse, OriginalRequestHeaders, OriginalRequestBody, BinaryOriginalRequest, OriginalResponseHeaders, OriginalResponseBody, BinaryOriginalResponse FROM ProxyLog WHERE ID=@ID LIMIT 1";
            return GetRecordFromDB(DataSource, Cmd,ID, true);
        }
        internal static IronLogRecord GetRecordFromTestLog(int ID)
        {
            string DataSource = "data source=" + TestLogFile;
            string Cmd = "SELECT RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM TestLog WHERE ID=@ID LIMIT 1";
            return GetRecordFromDB(DataSource, Cmd, ID, false);
        }
        internal static IronLogRecord GetRecordFromShellLog(int ID)
        {
            string DataSource = "data source=" + ShellLogFile;
            string Cmd = "SELECT RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM ShellLog WHERE ID=@ID LIMIT 1";
            return GetRecordFromDB(DataSource, Cmd, ID, false);
        }
        internal static IronLogRecord GetRecordFromProbeLog(int ID)
        {
            string DataSource = "data source=" + ProbeLogFile;
            string Cmd = "SELECT RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM ProbeLog WHERE ID=@ID LIMIT 1";
            return GetRecordFromDB(DataSource, Cmd, ID, false);
        }
        internal static IronLogRecord GetRecordFromScanLog(int ID)
        {
            string DataSource = "data source=" + ScanLogFile;
            string Cmd = "SELECT RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM ScanLog WHERE ID=@ID LIMIT 1";
            return GetRecordFromDB(DataSource, Cmd, ID, false);
        }

        static IronLogRecord GetRecordFromDB(string DataSource, string CmdString, int ID, bool IsProxyLog)
        {
            SQLiteConnection DB = new SQLiteConnection(DataSource);
            IronLogRecord ILR = new IronLogRecord();
            DB.Open();
            try
            {
                SQLiteCommand cmd = DB.CreateCommand();
                cmd.CommandText = CmdString;
                cmd.Parameters.AddWithValue("@ID", ID);
                SQLiteDataReader result = cmd.ExecuteReader();
                if(!result.HasRows)
                {
                    throw new Exception("ID not found in DB");
                }
                ILR.RequestHeaders = result["RequestHeaders"].ToString();
                ILR.RequestBody = result["RequestBody"].ToString();
                ILR.ResponseHeaders = result["ResponseHeaders"].ToString();
                ILR.ResponseBody = result["ResponseBody"].ToString();
                //ILR.SSL = (result["SSL"].ToString().Equals("1"));
                ILR.IsRequestBinary = (result["BinaryRequest"].ToString().Equals("1"));
                ILR.IsResponseBinary = (result["BinaryResponse"].ToString().Equals("1"));
                if (IsProxyLog)
                {
                    ILR.OriginalRequestHeaders = result["OriginalRequestHeaders"].ToString();
                    ILR.OriginalRequestBody = result["OriginalRequestBody"].ToString();
                    ILR.IsOriginalRequestBinary = (result["BinaryOriginalRequest"].ToString().Equals("1"));
                    ILR.OriginalResponseHeaders = result["OriginalResponseHeaders"].ToString();
                    ILR.OriginalResponseBody = result["OriginalResponseBody"].ToString();
                    ILR.IsOriginalResponseBinary = (result["BinaryOriginalResponse"].ToString().Equals("1"));
                }
                result.Close();
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
            return ILR;
        }

        internal static List<IronLogRecord> GetRecordsFromProxyLog()
        {
            string DataSource = "data source=" + ProxyLogFile;
            string Cmd = "SELECT ID, SSL, RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse, OriginalRequestHeaders, OriginalRequestBody, BinaryOriginalRequest, OriginalResponseHeaders, OriginalResponseBody, BinaryOriginalResponse FROM ProxyLog";
            return GetRecordsFromDB(DataSource, Cmd, true);
        }
        internal static List<IronLogRecord> GetRecordsFromTestLog()
        {
            string DataSource = "data source=" + TestLogFile;
            string Cmd = "SELECT ID, RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM TestLog";
            return GetRecordsFromDB(DataSource, Cmd, false);
        }
        internal static List<IronLogRecord> GetRecordsFromShellLog()
        {
            string DataSource = "data source=" + ShellLogFile;
            string Cmd = "SELECT ID, RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM ShellLog";
            return GetRecordsFromDB(DataSource, Cmd, false);
        }
        internal static List<IronLogRecord> GetRecordsFromProbeLog()
        {
            string DataSource = "data source=" + ProbeLogFile;
            string Cmd = "SELECT ID, RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM ProbeLog";
            return GetRecordsFromDB(DataSource, Cmd, false);
        }
        internal static List<IronLogRecord> GetRecordsFromScanLog()
        {
            string DataSource = "data source=" + ScanLogFile;
            string Cmd = "SELECT ID, RequestHeaders, RequestBody, BinaryRequest, ResponseHeaders, ResponseBody, BinaryResponse FROM ScanLog";
            return GetRecordsFromDB(DataSource, Cmd, false);
        }

        static List<IronLogRecord> GetRecordsFromDB(string DataSource, string CmdString, bool IsProxyLog)
        {
            List<IronLogRecord> IronLogRecords = new List<IronLogRecord>();
            SQLiteConnection DB = new SQLiteConnection(DataSource);
            DB.Open();
            try
            {
                SQLiteCommand cmd = DB.CreateCommand();
                cmd.CommandText = CmdString;
                SQLiteDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    try
                    {
                        IronLogRecord ILR = new IronLogRecord();
                        try
                        {
                            ILR.ID = Int32.Parse(result["ID"].ToString());
                        }
                        catch
                        {
                            ILR.ID = 0;
                        }
                        ILR.RequestHeaders = result["RequestHeaders"].ToString();
                        ILR.RequestBody = result["RequestBody"].ToString();
                        ILR.ResponseHeaders = result["ResponseHeaders"].ToString();
                        ILR.ResponseBody = result["ResponseBody"].ToString();
                        //ILR.SSL = (result["SSL"].ToString().Equals("1"));
                        ILR.IsRequestBinary = (result["BinaryRequest"].ToString().Equals("1"));
                        ILR.IsResponseBinary = (result["BinaryResponse"].ToString().Equals("1"));
                        if (IsProxyLog)
                        {
                            ILR.OriginalRequestHeaders = result["OriginalRequestHeaders"].ToString();
                            ILR.OriginalRequestBody = result["OriginalRequestBody"].ToString();
                            ILR.IsOriginalRequestBinary = (result["BinaryOriginalRequest"].ToString().Equals("1"));
                            ILR.OriginalResponseHeaders = result["OriginalResponseHeaders"].ToString();
                            ILR.OriginalResponseBody = result["OriginalResponseBody"].ToString();
                            ILR.IsOriginalResponseBinary = (result["BinaryOriginalResponse"].ToString().Equals("1"));
                        }
                        IronLogRecords.Add(ILR);
                    }
                    catch { }
                }
                result.Close();
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
            return IronLogRecords;
        }

        internal static Scanner GetScannerFromDB(int ScanID)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            Scanner ScannerFromLog = null;
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT RequestHeaders, RequestBody, BinaryRequest, Status, InjectionPoints, FormatPlugin, SessionPlugin, ScanPlugins FROM ScanQueue WHERE ScanID=@ScanID LIMIT 1";
            cmd.Parameters.AddWithValue("@ScanID", ScanID);
            SQLiteDataReader result = cmd.ExecuteReader();

            IronLogRecord ILR = new IronLogRecord();
            ILR.RequestHeaders = result["RequestHeaders"].ToString();
            ILR.RequestBody = result["RequestBody"].ToString();
            ILR.IsRequestBinary = (result["BinaryRequest"].ToString().Equals("1"));
            Session Irse = Session.GetIronSessionFromIronLogRecord(ILR,0);
            Request Req = Irse.Request;
            string Status = result["Status"].ToString();
            string FormatPluginName =  result["FormatPlugin"].ToString();
            string SessionPluginName = result["SessionPlugin"].ToString();
            string InjectionString = result["InjectionPoints"].ToString();
            string[] ScanPluginsArray = result["ScanPlugins"].ToString().Split(new char[] { ',' });
            DB.Close();
            ScannerFromLog = new Scanner(Req);
            if (Status.Equals("Not Started")) return ScannerFromLog;

            if (SessionPluginName.Length > 0)
            {
                if (!SessionPluginName.Equals("None") && SessionPlugin.List().Contains(SessionPluginName))
                {
                    ScannerFromLog.SessionHandler = SessionPlugin.Get(SessionPluginName);
                }
            }
            if (FormatPluginName.Length > 0)
            {
                if (!FormatPluginName.Equals("None") && FormatPlugin.List().Contains(FormatPluginName))
                {
                    ScannerFromLog.BodyFormat = FormatPlugin.Get(FormatPluginName);
                }
            }
            if (ScanPluginsArray.Length > 0)
            {
                List<string> ValidScanPlugins = ActivePlugin.List();
                foreach (string Name in ScanPluginsArray)
                {
                    if (ValidScanPlugins.Contains(Name))
                    {
                        ScannerFromLog.AddCheck(Name);
                    }
                }
            }
            ScannerFromLog.AbsorbInjectionString(InjectionString);
            ScannerFromLog.Status = Status;
            return ScannerFromLog;
        }
        internal static PluginResult GetPluginResultFromDB(int ID)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + PluginResultsLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT HostName, Title, Plugin, Summary, Severity, Confidence, Type, UniquenessString FROM PluginResult WHERE ID=@ID LIMIT 1";
            cmd.Parameters.AddWithValue("@ID", ID);
            SQLiteDataReader result = cmd.ExecuteReader();
            PluginResult PR = new PluginResult(result["HostName"].ToString());
            PR.Id = ID;
            PR.Title = result["Title"].ToString();
            PR.Plugin = result["Plugin"].ToString();
            PR.Summary = result["Summary"].ToString();
            PR.Severity = GetSeverity(Int32.Parse(result["Severity"].ToString()));
            PR.Confidence = GetConfidence(Int32.Parse(result["Confidence"].ToString()));
            PR.ResultType = GetResultType(Int32.Parse(result["Type"].ToString()));
            PR.Signature = result["UniquenessString"].ToString();
            result.Close();
            cmd.CommandText = "SELECT RequestTrigger, RequestHeaders, RequestBody, BinaryRequest, ResponseTrigger, ResponseHeaders, ResponseBody, BinaryResponse  FROM Triggers WHERE ID=@ID";
            cmd.Parameters.AddWithValue("@ID", ID);
            result = cmd.ExecuteReader();
            while (result.Read())
            {
                string RequestTrigger = result["RequestTrigger"].ToString();
                string ResponseTrigger = result["ResponseTrigger"].ToString();

                IronLogRecord ILR = new IronLogRecord();
                ILR.RequestHeaders = result["RequestHeaders"].ToString();
                ILR.RequestBody = result["RequestBody"].ToString();
                ILR.IsRequestBinary = (result["BinaryRequest"].ToString().Equals("1"));
                
                ILR.ResponseHeaders = result["ResponseHeaders"].ToString();
                ILR.ResponseBody = result["ResponseBody"].ToString();
                ILR.IsResponseBinary = (result["BinaryResponse"].ToString().Equals("1"));

                Session IrSe = Session.GetIronSessionFromIronLogRecord(ILR,0);
                if (IrSe.Response != null)
                {
                    PR.Triggers.Add(RequestTrigger, IrSe.Request, ResponseTrigger, IrSe.Response);
                }
                else
                {
                    PR.Triggers.Add(RequestTrigger, IrSe.Request);
                }
            }
            result.Close();
            DB.Close();
            return PR;
        }
        
        internal static IronException GetException(int ID)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ExceptionsLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Title, Message, StackTrace FROM Exceptions WHERE ID=@ID";
            cmd.Parameters.AddWithValue("@ID", ID);
            SQLiteDataReader result = cmd.ExecuteReader();
            IronException IrEx = new IronException();
            IrEx.Title = result["Title"].ToString();
            IrEx.Message = result["Message"].ToString();
            IrEx.StackTrace = result["StackTrace"].ToString();
            result.Close();
            DB.Close();
            return IrEx;
        }

        internal static string GetTraceMessage(int ID)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + TraceLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Message FROM Trace WHERE ID=@ID";
            cmd.Parameters.AddWithValue("@ID", ID);
            SQLiteDataReader result = cmd.ExecuteReader();
            string Message = result["Message"].ToString();
            result.Close();
            DB.Close();
            return Message;
        }

        internal static string GetScanTraceMessage(int ID)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + TraceLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Message FROM ScanTrace WHERE ID=@ID";
            cmd.Parameters.AddWithValue("@ID", ID);
            SQLiteDataReader result = cmd.ExecuteReader();
            string Message = result["Message"].ToString();
            result.Close();
            DB.Close();
            return Message;
        }

        internal static string GetScanStatus(int ScanID)
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            DB.Open();
            string Message = "";
            try
            {
                SQLiteCommand Cmd = DB.CreateCommand();
                Cmd.CommandText = "SELECT Status FROM ScanQueue WHERE ScanID=@ScanID";
                Cmd.Parameters.AddWithValue("@ScanID", ScanID);
                SQLiteDataReader result = Cmd.ExecuteReader();
                Message = result["Status"].ToString();
                result.Close();
            }
            catch { }
            DB.Close();
            return Message;
        }

        internal static void UpdateProxyConfigFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT LoopBack, SystemProxy, Port FROM IronProxy";
            SQLiteDataReader result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                if (result.Read())
                {
                    string Port = result["Port"].ToString();
                    if (IronProxy.ValidProxyPort(Port))
                    {
                        IronProxy.Port = Int32.Parse(Port);
                    }
                    else
                    {
                        IronProxy.Port = 8080;
                    }
                    IronProxy.LoopBackOnly = result["LoopBack"].ToString().Equals("1");
                    IronProxy.SystemProxy = result["SystemProxy"].ToString().Equals("1");
                }
            }
            else
            {
                IronProxy.Port = 8080;
                IronProxy.LoopBackOnly = true;
                IronProxy.SystemProxy = false;
            }
            result.Close();
            DB.Close();
        }

        internal static void UpdateUpstreamProxyConfigFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Use, IP, Port FROM UpstreamProxy";
            SQLiteDataReader result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                if (result.Read())
                {
                    string Port = result["Port"].ToString();
                    if (IronProxy.ValidPort(Port))
                    {
                        IronProxy.UpstreamProxyPort = Int32.Parse(Port);
                    }
                    IronProxy.UseUpstreamProxy = result["Use"].ToString().Equals("1");
                    string IP = result["IP"].ToString();
                    if (IP.Length > 0)
                    {
                        IronProxy.UpstreamProxyIP = IP;
                    }
                }
            }
            result.Close();
            DB.Close();
        }

        internal static void UpdateRequestTextContentTypesConfigFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Type FROM TextRequestTypes";
            SQLiteDataReader result = cmd.ExecuteReader();
            List<string> RequestTextContentTypes = new List<string>();
            while (result.Read())
            {
                RequestTextContentTypes.Add(result["Type"].ToString());
            }
            result.Close();
            DB.Close();
            lock (Request.TextContentTypes)
            {
                Request.TextContentTypes.Clear();
                Request.TextContentTypes.AddRange(RequestTextContentTypes);
            }

        }

        internal static void UpdateResponseTextContentTypesConfigFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Type FROM TextResponseTypes";
            SQLiteDataReader result = cmd.ExecuteReader();
            List<string> ResponseTextContentTypes = new List<string>();
            while (result.Read())
            {
                ResponseTextContentTypes.Add(result["Type"].ToString());
            }
            result.Close();
            DB.Close();
            lock (Response.TextContentTypes)
            {
                Response.TextContentTypes.Clear();
                Response.TextContentTypes.AddRange(ResponseTextContentTypes);
            }
        }

        internal static void UpdateScriptPathsConfigFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Path FROM PyPath";
            SQLiteDataReader result = cmd.ExecuteReader();
            List<string> PyPaths = new List<string>(); 
            while (result.Read())
            {
                PyPaths.Add(result["Path"].ToString());
            }
            result.Close();

            cmd.CommandText = "SELECT Path FROM RbPath";
            result = cmd.ExecuteReader();
            List<string> RbPaths = new List<string>(); 
            while (result.Read())
            {
                RbPaths.Add(result["Path"].ToString());
            }
            result.Close();
            DB.Close();

            lock (IronScripting.PyPaths)
            {
                IronScripting.PyPaths.Clear();
                IronScripting.PyPaths.AddRange(PyPaths);
            }
            lock (IronScripting.RbPaths)
            {
                IronScripting.RbPaths.Clear();
                IronScripting.RbPaths.AddRange(RbPaths);
            }
        }

        internal static void UpdateScriptCommandsConfigFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Command FROM PyStartCommands";
            SQLiteDataReader result = cmd.ExecuteReader();
            List<string> PyCommands = new List<string>();
            while (result.Read())
            {
                PyCommands.Add(result["Command"].ToString());
            }
            result.Close();
            cmd.CommandText = "SELECT Command FROM RbStartCommands";
            result = cmd.ExecuteReader();
            List<string> RbCommands = new List<string>();
            while (result.Read())
            {
                RbCommands.Add(result["Command"].ToString());
            }
            result.Close();
            DB.Close();

            lock (IronScripting.PyCommands)
            {
                IronScripting.PyCommands.Clear();
                IronScripting.PyCommands.AddRange(PyCommands);
            }
            lock (IronScripting.RbCommands)
            {
                IronScripting.RbCommands.Clear();
                IronScripting.RbCommands.AddRange(RbCommands);
            }
        }

        internal static void UpdateInterceptRulesFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Get, Post, OtherMethods, Html, JS, CSS, Xml, JSON, OtherText, Img, OtherBinary, Code200, Code2xx, Code301_2, Code3xx, Code403, Code4xx, Code500, Code5xx, FileExt, FileExtPlus, FileExtMinus, PlusFileExts, MinusFileExts, Host, HostPlus, HostMinus, PlusHosts, MinusHosts, RequestKeyword, RequestKeywordPlus, RequestKeywordMinus, PlusRequestKeyword, MinusRequestKeyword, ResponseKeyword, ResponseKeywordPlus, ResponseKeywordMinus, PlusResponseKeyword, MinusResponseKeyword, RequestRulesOnResponse FROM InterceptRules";
            SQLiteDataReader result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                if (result.Read())
                {
                    IronProxy.InterceptGET = result["Get"].ToString().Equals("1");
                    IronProxy.InterceptPOST = result["Post"].ToString().Equals("1");
                    IronProxy.InterceptOtherMethods = result["OtherMethods"].ToString().Equals("1");
                    IronProxy.InterceptHTML = result["Html"].ToString().Equals("1");
                    IronProxy.InterceptJS = result["JS"].ToString().Equals("1");
                    IronProxy.InterceptCSS = result["CSS"].ToString().Equals("1");
                    IronProxy.InterceptXML = result["Xml"].ToString().Equals("1");
                    IronProxy.InterceptJSON = result["Json"].ToString().Equals("1");
                    IronProxy.InterceptOtherText = result["OtherText"].ToString().Equals("1");
                    IronProxy.InterceptImg = result["Img"].ToString().Equals("1");
                    IronProxy.InterceptOtherBinary = result["OtherBinary"].ToString().Equals("1");
                    IronProxy.Intercept200 = result["Code200"].ToString().Equals("1");
                    IronProxy.Intercept2xx = result["Code2xx"].ToString().Equals("1");
                    IronProxy.Intercept301_2 = result["Code301_2"].ToString().Equals("1");
                    IronProxy.Intercept3xx = result["Code3xx"].ToString().Equals("1");
                    IronProxy.Intercept403 = result["Code403"].ToString().Equals("1");
                    IronProxy.Intercept4xx = result["Code4xx"].ToString().Equals("1");
                    IronProxy.Intercept500 = result["Code500"].ToString().Equals("1");
                    IronProxy.Intercept5xx = result["Code5xx"].ToString().Equals("1");
                    IronProxy.InterceptCheckFileExtensions = result["FileExt"].ToString().Equals("1");
                    IronProxy.InterceptCheckFileExtensionsPlus = result["FileExtPlus"].ToString().Equals("1");
                    IronProxy.InterceptCheckFileExtensionsMinus = result["FileExtMinus"].ToString().Equals("1");
                    string[] PlusFileExtentions = result["PlusFileExts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.InterceptFileExtensions)
                    {
                        IronProxy.InterceptFileExtensions.Clear();
                        IronProxy.InterceptFileExtensions.AddRange(PlusFileExtentions);
                    }
                    string[] MinusFileExtentions = result["MinusFileExts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.DontInterceptFileExtensions)
                    {
                        IronProxy.DontInterceptFileExtensions.Clear();
                        IronProxy.DontInterceptFileExtensions.AddRange(MinusFileExtentions);
                    }

                    IronProxy.InterceptCheckHostNames = result["Host"].ToString().Equals("1");
                    IronProxy.InterceptCheckHostNamesPlus = result["HostPlus"].ToString().Equals("1");
                    IronProxy.InterceptCheckHostNamesMinus = result["HostMinus"].ToString().Equals("1");
                    string[] PlusHostNames = result["PlusHosts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.InterceptHostNames)
                    {
                        IronProxy.InterceptHostNames.Clear();
                        IronProxy.InterceptHostNames.AddRange(PlusHostNames);
                    }
                    string[] MinusHostNames = result["MinusHosts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.DontInterceptHostNames)
                    {
                        IronProxy.DontInterceptHostNames.Clear();
                        IronProxy.DontInterceptHostNames.AddRange(MinusHostNames);
                    }

                    IronProxy.InterceptCheckRequestWithKeyword = result["RequestKeyword"].ToString().Equals("1");
                    IronProxy.InterceptCheckRequestWithKeywordPlus = result["RequestKeywordPlus"].ToString().Equals("1");
                    IronProxy.InterceptCheckRequestWithKeywordMinus = result["RequestKeywordMinus"].ToString().Equals("1");
                    IronProxy.InterceptRequestWithKeyword = result["PlusRequestKeyword"].ToString();
                    IronProxy.DontInterceptRequestWithKeyword = result["MinusRequestKeyword"].ToString();

                    IronProxy.InterceptCheckResponseWithKeyword = result["ResponseKeyword"].ToString().Equals("1");
                    IronProxy.InterceptCheckResponseWithKeywordPlus = result["ResponseKeywordPlus"].ToString().Equals("1");
                    IronProxy.InterceptCheckResponseWithKeywordMinus = result["ResponseKeywordMinus"].ToString().Equals("1");
                    IronProxy.InterceptResponseWithKeyword = result["PlusResponseKeyword"].ToString();
                    IronProxy.DontInterceptResponseWithKeyword = result["MinusResponseKeyword"].ToString();

                    IronProxy.RequestRulesOnResponse = result["RequestRulesOnResponse"].ToString().Equals("1");

                }
            }
            result.Close();
            DB.Close();
        }

        internal static void UpdateDisplayRulesFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Get, Post, OtherMethods, Html, JS, CSS, Xml, JSON, OtherText, Img, OtherBinary, Code200, Code2xx, Code301_2, Code3xx, Code403, Code4xx, Code500, Code5xx, FileExt, FileExtPlus, FileExtMinus, PlusFileExts, MinusFileExts, Host, HostPlus, HostMinus, PlusHosts, MinusHosts FROM DisplayRules";
            SQLiteDataReader result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                if (result.Read())
                {
                    IronProxy.DisplayGET = result["Get"].ToString().Equals("1");
                    IronProxy.DisplayPOST = result["Post"].ToString().Equals("1");
                    IronProxy.DisplayOtherMethods = result["OtherMethods"].ToString().Equals("1");
                    IronProxy.DisplayHTML = result["Html"].ToString().Equals("1");
                    IronProxy.DisplayJS = result["JS"].ToString().Equals("1");
                    IronProxy.DisplayCSS = result["CSS"].ToString().Equals("1");
                    IronProxy.DisplayXML = result["Xml"].ToString().Equals("1");
                    IronProxy.DisplayJSON = result["Json"].ToString().Equals("1");
                    IronProxy.DisplayOtherText = result["OtherText"].ToString().Equals("1");
                    IronProxy.DisplayImg = result["Img"].ToString().Equals("1");
                    IronProxy.DisplayOtherBinary = result["OtherBinary"].ToString().Equals("1");
                    IronProxy.Display200 = result["Code200"].ToString().Equals("1");
                    IronProxy.Display2xx = result["Code2xx"].ToString().Equals("1");
                    IronProxy.Display301_2 = result["Code301_2"].ToString().Equals("1");
                    IronProxy.Display3xx = result["Code3xx"].ToString().Equals("1");
                    IronProxy.Display403 = result["Code403"].ToString().Equals("1");
                    IronProxy.Display4xx = result["Code4xx"].ToString().Equals("1");
                    IronProxy.Display500 = result["Code500"].ToString().Equals("1");
                    IronProxy.Display5xx = result["Code5xx"].ToString().Equals("1");

                    IronProxy.DisplayCheckFileExtensions = result["FileExt"].ToString().Equals("1");
                    IronProxy.DisplayCheckFileExtensionsPlus = result["FileExtPlus"].ToString().Equals("1");
                    IronProxy.DisplayCheckFileExtensionsMinus = result["FileExtMinus"].ToString().Equals("1");

                    string[] PlusFileExtentions = result["PlusFileExts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.DisplayFileExtensions)
                    {
                        IronProxy.DisplayFileExtensions.Clear();
                        IronProxy.DisplayFileExtensions.AddRange(PlusFileExtentions);
                    }
                    string[] MinusFileExtentions = result["MinusFileExts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.DontDisplayFileExtensions)
                    {
                        IronProxy.DontDisplayFileExtensions.Clear();
                        IronProxy.DontDisplayFileExtensions.AddRange(MinusFileExtentions);
                    }
                    
                    IronProxy.DisplayCheckHostNames = result["Host"].ToString().Equals("1");
                    IronProxy.DisplayCheckHostNamesPlus = result["HostPlus"].ToString().Equals("1");
                    IronProxy.DisplayCheckHostNamesMinus = result["HostMinus"].ToString().Equals("1");
                    string[] PlusHostNames = result["PlusHosts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.DisplayHostNames)
                    {
                        IronProxy.DisplayHostNames.Clear();
                        IronProxy.DisplayHostNames.AddRange(PlusHostNames);
                    }
                    string[] MinusHostNames = result["MinusHosts"].ToString().Split(new char[] { ',' });
                    lock (IronProxy.DontDisplayHostNames)
                    {
                        IronProxy.DontDisplayHostNames.Clear();
                        IronProxy.DontDisplayHostNames.AddRange(MinusHostNames);
                    }
                }
            }
            result.Close();
            DB.Close();
        }

        internal static void UpdateJSTaintConfigFromDB()
        {
            List<string> DefaultSourceObjects = new List<string>();
            List<string> DefaultSinkObjects = new List<string>();
            List<string> DefaultSourceReturningMethods = new List<string>() { };
            List<string> DefaultSinkReturningMethods = new List<string>() { };
            List<string> DefaultArgumentReturningMethods = new List<string>() { };
            List<string> DefaultArgumentAssignedToSinkMethods = new List<string>();
            List<string> DefaultArgumentAssignedASourceMethods = new List<string>() { };

            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT SourceObjects, SinkObjects, ArgumentAssignedASourceMethods, ArgumentAssignedToSinkMethods, SourceReturningMethods, SinkReturningMethods, ArgumentReturningMethods FROM JSTaintConfig";
            SQLiteDataReader result = cmd.ExecuteReader();
            
            if (result.HasRows)
            {
                while (result.Read())
                {
                    if (result["SourceObjects"].ToString().Length > 0) DefaultSourceObjects.Add(result["SourceObjects"].ToString());
                    if (result["SinkObjects"].ToString().Length > 0) DefaultSinkObjects.Add(result["SinkObjects"].ToString());
                    if (result["ArgumentAssignedASourceMethods"].ToString().Length > 0) DefaultArgumentAssignedASourceMethods.Add(result["ArgumentAssignedASourceMethods"].ToString());
                    if (result["ArgumentAssignedToSinkMethods"].ToString().Length > 0) DefaultArgumentAssignedToSinkMethods.Add(result["ArgumentAssignedToSinkMethods"].ToString());
                    if (result["SourceReturningMethods"].ToString().Length > 0) DefaultSourceReturningMethods.Add(result["SourceReturningMethods"].ToString());
                    if (result["SinkReturningMethods"].ToString().Length > 0) DefaultSinkReturningMethods.Add(result["SinkReturningMethods"].ToString());
                    if (result["ArgumentReturningMethods"].ToString().Length > 0) DefaultArgumentReturningMethods.Add(result["ArgumentReturningMethods"].ToString());
                }
            }
            lock (IronJint.DefaultSourceObjects)
            {
                IronJint.DefaultSourceObjects = new List<string>(DefaultSourceObjects);
            }
            lock (IronJint.DefaultSinkObjects)
            {
                IronJint.DefaultSinkObjects = new List<string>(DefaultSinkObjects);
            }
            lock (IronJint.DefaultArgumentAssignedASourceMethods)
            {
                IronJint.DefaultArgumentAssignedASourceMethods = new List<string>(DefaultArgumentAssignedASourceMethods);
            }
            lock (IronJint.DefaultArgumentAssignedToSinkMethods)
            {
                IronJint.DefaultArgumentAssignedToSinkMethods = new List<string>(DefaultArgumentAssignedToSinkMethods);
            }
            lock (IronJint.DefaultSourceReturningMethods)
            {
                IronJint.DefaultSourceReturningMethods = new List<string>(DefaultSourceReturningMethods);
            }
            lock (IronJint.DefaultSinkReturningMethods)
            {
                IronJint.DefaultSinkReturningMethods = new List<string>(DefaultSinkReturningMethods);
            }
            lock (IronJint.DefaultArgumentReturningMethods)
            {
                IronJint.DefaultArgumentReturningMethods = new List<string>(DefaultArgumentReturningMethods);
            }
            result.Close();
            DB.Close();
        }

        internal static void UpdateScannerSettingsFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT MaxScannerThreads, MaxCrawlerThreads, UserAgent FROM ScannerSettings";
            SQLiteDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                try
                {
                    Scanner.MaxParallelScanCount = Int32.Parse(result["MaxScannerThreads"].ToString());
                }
                catch { }
                try
                {
                    Crawler.MaxCrawlThreads = Int32.Parse(result["MaxCrawlerThreads"].ToString());
                }
                catch { }
                Crawler.UserAgent = result["UserAgent"].ToString();
            }
            result.Close();
            DB.Close();
        }

        internal static void UpdatePassiveAnalysisSettingsFromDB()
        {
            SQLiteConnection DB = new SQLiteConnection("data source=" + ConfigFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT Proxy, Shell, Test, Scan, Probe FROM PassiveAnalysisSettings";
            SQLiteDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                try
                {
                    PassiveChecker.RunOnProxyTraffic =  Int32.Parse(result["Proxy"].ToString()) == 1;
                }
                catch { }
                try
                {
                    PassiveChecker.RunOnShellTraffic = Int32.Parse(result["Shell"].ToString()) == 1;
                }
                catch { }
                try
                {
                    PassiveChecker.RunOnTestTraffic = Int32.Parse(result["Test"].ToString()) == 1;
                }
                catch { }
                try
                {
                    PassiveChecker.RunOnShellTraffic = Int32.Parse(result["Shell"].ToString()) == 1;
                }
                catch { }
                try
                {
                    PassiveChecker.RunOnProbeTraffic = Int32.Parse(result["Probe"].ToString()) == 1;
                }
                catch { }
            }
            result.Close();
            DB.Close();
        }

        internal static void UpdateConfigFromDB()
        {
            UpdateProxyConfigFromDB();
            UpdateUpstreamProxyConfigFromDB();
            UpdateRequestTextContentTypesConfigFromDB();
            UpdateResponseTextContentTypesConfigFromDB();
            UpdateScriptPathsConfigFromDB();
            UpdateScriptCommandsConfigFromDB();
            UpdateInterceptRulesFromDB();
            UpdateDisplayRulesFromDB();
            UpdateJSTaintConfigFromDB();
            UpdateScannerSettingsFromDB();
            UpdatePassiveAnalysisSettingsFromDB();
        }


        internal static List<LogRow> GetProxyLogRecords(int StartID)
        {
            List<LogRow> ProxyLogRecords = new List<LogRow>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + ProxyLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ID , SSL, HostName, Method, URL, Edited, File, Parameters, Code, Length, MIME, SetCookie, Notes, OriginalRequestHeaders FROM ProxyLog WHERE ID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                LogRow LR = new LogRow();
                try { LR.ID = Int32.Parse(result["ID"].ToString()); }
                catch { continue; }
                LR.Host = result["HostName"].ToString();
                LR.Method = result["Method"].ToString();
                LR.Url = result["URL"].ToString();
                LR.File = result["File"].ToString();
                LR.SSL = result["SSL"].ToString().Equals("1");
                LR.Parameters = result["Parameters"].ToString();
                try
                {LR.Code = Int32.Parse(result["Code"].ToString());}
                catch { LR.Code = -1; }
                try
                { LR.Length = Int32.Parse(result["Length"].ToString()); }
                catch { LR.Length = 0; }
                LR.Mime = result["MIME"].ToString();
                LR.SetCookie = result["SetCookie"].ToString().Equals("1");
                LR.Editied = result["Edited"].ToString().Equals("1");
                LR.Notes = result["Notes"].ToString();
                LR.OriginalRequestHeaders = result["OriginalRequestHeaders"].ToString();
                ProxyLogRecords.Add(LR);
            }
            result.Close();
            DB.Close();
            return ProxyLogRecords;
        }

        internal static List<LogRow> GetTestLogRecords(int StartID)
        {
            List<LogRow> MTLogRecords = new List<LogRow>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + TestLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ID, SSL, HostName, Method, URL, File, Parameters, Code, Length, MIME, SetCookie FROM TestLog WHERE ID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                LogRow LR = new LogRow();
                try { LR.ID = Int32.Parse(result["ID"].ToString()); }
                catch { continue; }
                LR.Host = result["HostName"].ToString();
                LR.Method = result["Method"].ToString();
                LR.Url = result["URL"].ToString();
                LR.File = result["File"].ToString();
                LR.SSL = result["SSL"].ToString().Equals("1");
                LR.Parameters = result["Parameters"].ToString();
                try
                { LR.Code = Int32.Parse(result["Code"].ToString()); }
                catch { LR.Code = -1; }
                try
                { LR.Length = Int32.Parse(result["Length"].ToString()); }
                catch { LR.Length = 0; }
                LR.Mime = result["MIME"].ToString();
                LR.SetCookie = result["SetCookie"].ToString().Equals("1");
                MTLogRecords.Add(LR);
            }
            result.Close();
            DB.Close();
            return MTLogRecords;
        }

        internal static List<LogRow> GetShellLogRecords(int StartID)
        {
            List<LogRow> ShellLogRecords = new List<LogRow>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + ShellLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ID , SSL, HostName, Method, URL, File, Parameters, Code, Length, MIME, SetCookie FROM ShellLog WHERE ID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                LogRow LR = new LogRow();
                try { LR.ID = Int32.Parse(result["ID"].ToString()); }
                catch { continue; }
                LR.Host = result["HostName"].ToString();
                LR.Method = result["Method"].ToString();
                LR.Url = result["URL"].ToString();
                LR.File = result["File"].ToString();
                LR.SSL = result["SSL"].ToString().Equals("1");
                LR.Parameters = result["Parameters"].ToString();
                try
                { LR.Code = Int32.Parse(result["Code"].ToString()); }
                catch { LR.Code = -1; }
                try
                { LR.Length = Int32.Parse(result["Length"].ToString()); }
                catch { LR.Length = 0; }
                LR.Mime = result["MIME"].ToString();
                 LR.SetCookie = result["SetCookie"].ToString().Equals("1");
                 ShellLogRecords.Add(LR);
            }
            result.Close();
            DB.Close();
            return ShellLogRecords;
        }

        internal static List<LogRow> GetProbeLogRecords(int StartID)
        {
            List<LogRow> ShellLogRecords = new List<LogRow>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + ProbeLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ID , SSL, HostName, Method, URL, File, Parameters, Code, Length, MIME, SetCookie FROM ProbeLog WHERE ID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                LogRow LR = new LogRow();
                try { LR.ID = Int32.Parse(result["ID"].ToString()); }
                catch { continue; }
                LR.Host = result["HostName"].ToString();
                LR.Method = result["Method"].ToString();
                LR.Url = result["URL"].ToString();
                LR.File = result["File"].ToString();
                LR.SSL = result["SSL"].ToString().Equals("1");
                LR.Parameters = result["Parameters"].ToString();
                try
                { LR.Code = Int32.Parse(result["Code"].ToString()); }
                catch { LR.Code = -1; }
                try
                { LR.Length = Int32.Parse(result["Length"].ToString()); }
                catch { LR.Length = 0; }
                LR.Mime = result["MIME"].ToString();
                LR.SetCookie = result["SetCookie"].ToString().Equals("1");
                ShellLogRecords.Add(LR);
            }
            result.Close();
            DB.Close();
            return ShellLogRecords;
        }

        internal static List<LogRow> GetScanLogRecords(int StartID)
        {
            List<LogRow> ScanLogRecords = new List<LogRow>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + ScanLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ID , ScanID, SSL, HostName, Method, URL, File, Parameters, Code, Length, MIME, SetCookie FROM ScanLog WHERE ID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                LogRow LR = new LogRow();
                try { LR.ID = Int32.Parse(result["ID"].ToString()); }
                catch { continue; }
                try { LR.ScanID = Int32.Parse(result["ScanID"].ToString()); }
                catch { continue; }
                LR.Host = result["HostName"].ToString();
                LR.Method = result["Method"].ToString();
                LR.Url = result["URL"].ToString();
                LR.File = result["File"].ToString();
                LR.SSL = result["SSL"].ToString().Equals("1");
                LR.Parameters = result["Parameters"].ToString();
                try
                { LR.Code = Int32.Parse(result["Code"].ToString()); }
                catch { LR.Code = -1; }
                try
                { LR.Length = Int32.Parse(result["Length"].ToString()); }
                catch { LR.Length = 0; }                
                LR.Mime = result["MIME"].ToString();
                LR.SetCookie = result["SetCookie"].ToString().Equals("1");
                ScanLogRecords.Add(LR);
            }
            result.Close();
            DB.Close();
            return ScanLogRecords;
        }

        internal static List<string[]> GetScanQueueRecords(int StartID)
        {
            List<string[]> ScanQueueRecords = new List<string[]>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + IronProjectFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ScanID, Status, Method, URL FROM ScanQueue WHERE ScanID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                string[] Fields = new string[4];
                Fields[0] = result["ScanID"].ToString();
                Fields[1] = result["Status"].ToString();
                Fields[2] = result["Method"].ToString();
                Fields[3] = result["URL"].ToString();
                ScanQueueRecords.Add(Fields);
            }
            result.Close();
            DB.Close();
            return ScanQueueRecords;
        }

        internal static List<PluginResult > GetPluginResultsLogRecords(int StartID)
        {
            List<PluginResult> PluginResultsLogRecords = new List<PluginResult>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + PluginResultsLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ID, HostName, Title, Plugin, UniquenessString, Severity, Confidence, Type FROM PluginResult WHERE ID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                PluginResult PR = new PluginResult(result["HostName"].ToString());
                PR.Id = Int32.Parse(result["ID"].ToString());
                PR.Title = result["Title"].ToString();
                PR.Plugin = result["Plugin"].ToString();
                PR.AffectedHost = result["HostName"].ToString();
                PR.Severity = GetSeverity(Int32.Parse(result["Severity"].ToString()));
                PR.Confidence = GetConfidence(Int32.Parse(result["Confidence"].ToString()));
                PR.ResultType = GetResultType(Int32.Parse(result["Type"].ToString()));
                PR.Signature = result["UniquenessString"].ToString();
                PluginResultsLogRecords.Add(PR);
            }
            result.Close();
            DB.Close();
            return PluginResultsLogRecords;
        }

        internal static List<IronException> GetExceptionLogRecords(int StartID)
        {
            List<IronException> ExceptionLogRecords = new List<IronException>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + ExceptionsLogFile);
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandText = "SELECT ID, Title, Message, StackTrace FROM Exceptions WHERE ID > @StartID LIMIT 1000";
            cmd.Parameters.AddWithValue("@StartID", StartID);
            SQLiteDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                IronException IrEx = new IronException();
                IrEx.ID = Int32.Parse(result["ID"].ToString());
                IrEx.Title = result["Title"].ToString();
                ExceptionLogRecords.Add(IrEx);
            }
            result.Close();
            DB.Close();
            return ExceptionLogRecords;
        }

        internal static List<IronTrace> GetTraceRecords(int StartID)
        {
            List<IronTrace> TraceRecords = new List<IronTrace>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + TraceLogFile);
            DB.Open();
            try
            {
                SQLiteCommand cmd = DB.CreateCommand();
                cmd.CommandText = "SELECT ID, Time, Date, ThreadID, Source, Message FROM Trace WHERE ID > @StartID LIMIT 1000";
                cmd.Parameters.AddWithValue("@StartID", StartID);
                SQLiteDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    IronTrace Trace = new IronTrace();
                    try
                    { Trace.ID = Int32.Parse(result["ID"].ToString()); }
                    catch { continue; }
                    try
                    { Trace.ThreadID = Int32.Parse(result["ThreadID"].ToString()); }
                    catch { }
                    Trace.Time = result["Time"].ToString();
                    Trace.Date = result["Date"].ToString();
                    Trace.Source = result["Source"].ToString();
                    Trace.Message = result["Message"].ToString();
                    TraceRecords.Add(Trace);
                }
                result.Close();
            }
            catch(Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
            return TraceRecords;
        }

        internal static List<IronTrace> GetScanTraceRecords(int StartID)
        {
            List<IronTrace> TraceRecords = new List<IronTrace>();
            SQLiteConnection DB = new SQLiteConnection("data source=" + TraceLogFile);
            DB.Open();
            try
            {
                SQLiteCommand cmd = DB.CreateCommand();
                cmd.CommandText = "SELECT ID, ScanID, PluginName, Section, Parameter, Title, Message FROM ScanTrace WHERE ID > @StartID LIMIT 1000";
                cmd.Parameters.AddWithValue("@StartID", StartID);
                SQLiteDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    IronTrace Trace = new IronTrace();
                    try
                    { Trace.ID = Int32.Parse(result["ID"].ToString()); }
                    catch { continue; }
                    try
                    { Trace.ScanID = Int32.Parse(result["ScanID"].ToString()); }
                    catch { }
                    Trace.PluginName = result["PluginName"].ToString();
                    Trace.Section = result["Section"].ToString();
                    Trace.Parameter = result["Parameter"].ToString();
                    Trace.Title = result["Title"].ToString();
                    Trace.Message = result["Message"].ToString();
                    TraceRecords.Add(Trace);
                }
                result.Close();
            }
            catch (Exception Exp)
            {
                DB.Close();
                throw Exp;
            }
            DB.Close();
            return TraceRecords;
        }

        internal static void InitialiseLogDB()
        {
            CreateLogFilesOnStartUp();
            SQLiteConnection log = new SQLiteConnection("data source=" + IronProjectFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS ScanQueue";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ScanQueue (ScanID INT PRIMARY KEY, RequestHeaders TEXT, RequestBody TEXT, BinaryRequest INT, Status TEXT, Method TEXT, URL TEXT, SessionPlugin TEXT, InjectionPoints TEXT, FormatPlugin TEXT, ScanPlugins TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS TestGroups (Red INT, Green INT, Blue INT, Gray INT, Brown INT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();

            log = new SQLiteConnection("data source=" + ProxyLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS ProxyLog";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ProxyLog (ID INT PRIMARY KEY, SSL INT, HostName TEXT, Method TEXT, URL TEXT, Edited INT, File TEXT, Parameters TEXT, RequestHeaders TEXT, RequestBody TEXT, BinaryRequest INT, OriginalRequestHeaders TEXT, OriginalRequestBody TEXT, BinaryOriginalRequest INT, Code INT, Length INT, MIME TEXT, SetCookie INT, ResponseHeaders TEXT, ResponseBody TEXT, BinaryResponse INT, OriginalResponseHeaders TEXT, OriginalResponseBody TEXT, BinaryOriginalResponse INT, Notes TEXT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();

            log = new SQLiteConnection("data source=" + TestLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS MTLog";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS TestLog (ID INT PRIMARY KEY, SSL INT, HostName TEXT, Method TEXT, URL TEXT, File TEXT, Parameters TEXT, RequestHeaders TEXT, RequestBody TEXT, BinaryRequest INT, Code INT, Length INT, MIME TEXT, SetCookie INT, ResponseHeaders TEXT, ResponseBody TEXT, BinaryResponse INT, Notes TEXT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();

            log = new SQLiteConnection("data source=" + ShellLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS ShellLog";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ShellLog (ID INT PRIMARY KEY, SSL INT, HostName TEXT, Method TEXT, URL TEXT, File TEXT, Parameters TEXT, RequestHeaders TEXT, RequestBody TEXT, BinaryRequest INT, Code INT, Length INT, MIME TEXT, SetCookie INT, ResponseHeaders TEXT, ResponseBody TEXT, BinaryResponse INT, Notes TEXT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();

            log = new SQLiteConnection("data source=" + ProbeLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS ProbeLog";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ProbeLog (ID INT PRIMARY KEY, SSL INT, HostName TEXT, Method TEXT, URL TEXT, File TEXT, Parameters TEXT, RequestHeaders TEXT, RequestBody TEXT, BinaryRequest INT, Code INT, Length INT, MIME TEXT, SetCookie INT, ResponseHeaders TEXT, ResponseBody TEXT, BinaryResponse INT, Notes TEXT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();

            log = new SQLiteConnection("data source=" + ScanLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS ScanLog";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ScanLog (ID INT PRIMARY KEY, ScanID INT, SSL INT, HostName TEXT, Method TEXT, URL TEXT, File TEXT, Parameters TEXT, RequestHeaders TEXT, RequestBody TEXT, BinaryRequest INT, Code INT, Length INT, MIME TEXT, SetCookie INT, ResponseHeaders TEXT, ResponseBody TEXT, BinaryResponse INT, Notes TEXT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();

            log = new SQLiteConnection("data source=" + PluginResultsLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS PluginResult";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS PluginResult (ID INT, HostName TEXT, Title TEXT, Plugin TEXT, Summary TEXT, Severity INT, Confidence INT, Type INT, UniquenessString TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DROP TABLE IF EXISTS Triggers";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Triggers (ID INT, RequestTrigger TEXT, RequestHeaders TEXT, RequestBody TEXT, BinaryRequest INT, ResponseTrigger TEXT, ResponseHeaders TEXT, ResponseBody TEXT, BinaryResponse INT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();
            
            log = new SQLiteConnection("data source=" + ExceptionsLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Exceptions";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Exceptions (ID INT PRIMARY KEY, Title TEXT, Message TEXT, StackTrace TEXT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();

            log = new SQLiteConnection("data source=" + TraceLogFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Trace";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Trace (ID INT PRIMARY KEY, Time TEXT, Date TEXT, ThreadID INT, Source TEXT, Message TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DROP TABLE IF EXISTS ScanTrace";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ScanTrace (ID INT PRIMARY KEY, ScanID INT, PluginName TEXT, Section TEXT, Parameter TEXT, Title TEXT, Message TEXT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();
            
            ConfigFile = Config.RootDir + "\\IronConfig.exe";
            log = new SQLiteConnection("data source=" + ConfigFile);
            log.Open();
            using (SQLiteTransaction create = log.BeginTransaction())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(log))
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS IronProxy (LoopBack INT, SystemProxy INT, Port INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS UpstreamProxy (Use INT, IP TEXT, Port INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS PyPath (Path TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS RbPath (Path TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS PyStartCommands (Command TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS RbStartCommands (Command TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS InterceptRules (Get INT, Post INT, OtherMethods INT, Html INT, JS INT, CSS INT, Xml INT, JSON INT, OtherText INT, Img INT, OtherBinary INT, Code200 INT, Code2xx INT, Code301_2 INT, Code3xx INT, Code403 INT, Code4xx INT, Code500 INT, Code5xx INT, FileExt INT, FileExtPlus INT, FileExtMinus INT, PlusFileExts TEXT, MinusFileExts TEXT, Host INT, HostPlus INT, HostMinus INT, PlusHosts TEXT, MinusHosts TEXT, RequestKeyword INT, RequestKeywordPlus INT, RequestKeywordMinus INT, PlusRequestKeyword TEXT, MinusRequestKeyword TEXT, ResponseKeyword INT, ResponseKeywordPlus INT, ResponseKeywordMinus INT, PlusResponseKeyword TEXT, MinusResponseKeyword TEXT, RequestRulesOnResponse INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS DisplayRules (Get INT, Post INT, OtherMethods INT, Html INT, JS INT, CSS INT, Xml INT, JSON INT, OtherText INT, Img INT, OtherBinary INT, Code200 INT, Code2xx INT, Code301_2 INT, Code3xx INT, Code403 INT, Code4xx INT, Code500 INT, Code5xx INT, FileExt INT, FileExtPlus INT, FileExtMinus INT, PlusFileExts TEXT, MinusFileExts TEXT, Host INT, HostPlus INT, HostMinus INT, PlusHosts TEXT, MinusHosts TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS TextRequestTypes (Type TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS TextResponseTypes (Type TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS JSTaintConfig (SourceObjects TEXT, SinkObjects TEXT, ArgumentAssignedASourceMethods TEXT, ArgumentAssignedToSinkMethods TEXT, SourceReturningMethods TEXT, SinkReturningMethods TEXT, ArgumentReturningMethods TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS ScannerSettings (MaxScannerThreads INT, MaxCrawlerThreads INT, UserAgent TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS PassiveAnalysisSettings (Proxy INT, Shell INT, Test INT, Scan INT, Probe INT)";
                    cmd.ExecuteNonQuery();
                }
                create.Commit();
            }
            log.Close();
        }

        static int GetConfidence(PluginResultConfidence Confidence)
        {
            if (Confidence == PluginResultConfidence.High) return 9;
            if (Confidence == PluginResultConfidence.Medium) return 6;
            if (Confidence == PluginResultConfidence.Low) return 3;
            return 3;
        }
        static int GetSeverity(PluginResultSeverity Severity)
        {
            if (Severity == PluginResultSeverity.High) return 9;
            if (Severity == PluginResultSeverity.Medium) return 6;
            if (Severity == PluginResultSeverity.Low) return 3;
            return 3;
        }
        static int GetResultType(PluginResultType ResultType)
        {
            if (ResultType == PluginResultType.Vulnerability) return 9;
            if (ResultType == PluginResultType.TestLead) return 6;
            if (ResultType == PluginResultType.Information) return 3;
            return 3;
        }
        static PluginResultConfidence GetConfidence( int Confidence)
        {
            if (Confidence == 9) return PluginResultConfidence.High;
            if (Confidence == 6) return PluginResultConfidence.Medium;
            if (Confidence == 3) return PluginResultConfidence.Low;
            return PluginResultConfidence.Low;
        }
        static PluginResultSeverity GetSeverity(int Severity)
        {
            if (Severity == 9) return PluginResultSeverity.High;
            if (Severity == 6) return PluginResultSeverity.Medium;
            if (Severity == 3) return PluginResultSeverity.Low;
            return PluginResultSeverity.Low;
        }
        static PluginResultType GetResultType(int ResultType)
        {
            if (ResultType == 9) return PluginResultType.Vulnerability;
            if (ResultType == 6) return PluginResultType.TestLead;
            if (ResultType == 3) return PluginResultType.Information;
            return PluginResultType.Information;
        }

        internal static void UpdateLogFilePaths(string LogPath)
        {
            IronDB.LogPath = LogPath;
            IronProjectFile = LogPath + "\\Project.iron";
            ProxyLogFile = LogPath + "\\Proxy.ironlog";
            TestLogFile = LogPath + "\\Test.ironlog";
            ShellLogFile = LogPath + "\\Shell.ironlog";
            ProbeLogFile = LogPath + "\\Probes.ironlog";
            ScanLogFile = LogPath + "\\Scan.ironlog";
            PluginResultsLogFile = LogPath + "\\Results.ironlog";
            ExceptionsLogFile = LogPath + "\\Exceptions.ironlog";
            TraceLogFile = LogPath + "\\Trace.ironlog";
        }

        internal static void CreateLogFilesOnStartUp()
        {
            string Path = DateTime.Now.ToString("yyyy_MMM_ddd_d__HH_mm_ss_tt");
            string LogDir = Config.RootDir + "\\log\\";
            while (Directory.Exists(LogDir + Path))
            {
                Path = DateTime.Now.ToString("yyyy_MMM_ddd_d__HH_mm_ss_tt") + "_ticks_" + DateTime.Now.Ticks.ToString();
            }
            Directory.CreateDirectory(LogDir + Path);
            UpdateLogFilePaths(LogDir + Path);
        }

        internal static int AsInt(bool Input)
        {
            if (Input)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
