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
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
//using Fiddler;
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
    public class ManualTesting
    {
        internal static int CurrentRequestID = 0;
        internal static Request CurrentRequest;
        internal static bool CurrentRequestIsSSL = false;
        internal static bool CurrentRequestMarked = false;
        internal static ScriptedSender ScSe = new ScriptedSender();
        internal static bool ScriptedSendEnabled = false;
        static Stack<Request> StoredRequestStack = new Stack<Request>();

        internal static int UntitledCount = 0;

        //To check message editing
        internal static bool RequestChanged = false;
        internal static bool RequestHeaderChanged = false;
        internal static bool RequestBodyChanged = false;
        internal static bool RequestQueryParametersChanged = false;
        internal static bool RequestBodyParametersChanged = false;
        internal static bool RequestCookieParametersChanged = false;
        internal static bool RequestHeaderParametersChanged = false;

        internal static Thread RequestFormatThread;
        internal static Thread ResponseFormatThread;

        //internal static Dictionary<int, Session> RedGroupSessions = new Dictionary<int,Session>();
        //internal static Dictionary<int, Session> BlueGroupSessions = new Dictionary<int,Session>();
        //internal static Dictionary<int, Session> GreenGroupSessions = new Dictionary<int,Session>();
        //internal static Dictionary<int, Session> GrayGroupSessions = new Dictionary<int,Session>();
        //internal static Dictionary<int, Session> BrownGroupSessions = new Dictionary<int,Session>();

        //internal static List<string> GroupNames = new List<string>();
        internal static Dictionary<string, Dictionary<int, Session>> GroupSessions = new Dictionary<string, Dictionary<int, Session>>();
        internal static Dictionary<string, int> CurrentGroupLogId = new Dictionary<string, int>();

        //internal static int RedGroupID = 0;
        //internal static int BlueGroupID = 0;
        //internal static int GreenGroupID = 0;
        //internal static int GrayGroupID = 0;
        //internal static int BrownGroupID = 0;

        internal static string CurrentGroup = "";

        internal static void SendRequest()
        {
            if (ManualTesting.CurrentRequest == null) return;
            IronUI.ResetMTResponseDisplayFields();
            Request Request = ManualTesting.CurrentRequest.GetClone();
            StringDictionary Flags = new StringDictionary();
            Flags.Add("IronFlag-BuiltBy", "ManualTestingSection");
            Request.ID = Interlocked.Increment(ref Config.TestRequestsCount);
            StoreInGroupList(Request);
            SetCurrentID(Request.ID);
            IronDB.LogMTRequest(Request);
            IronUI.UpdateMTLogGridWithRequest(Request);
            Flags.Add("IronFlag-ID", Request.ID.ToString());
            Fiddler.FiddlerApplication.oProxy.InjectCustomRequest(Request.GetFiddlerHTTPRequestHeaders(), Request.BodyArray, Flags);
        }

        internal static void StoreInGroupList(Request Req)
        {
            Session IrSe = new Session(Req);
            IrSe.Flags.Add("Group", CurrentGroup);
            lock (GroupSessions)
            {
                if (!GroupSessions.ContainsKey(CurrentGroup))
                    GroupSessions[CurrentGroup] = new Dictionary<int, Session>();

                if (GroupSessions[CurrentGroup].ContainsKey(Req.ID))
                    GroupSessions[CurrentGroup][Req.ID] = IrSe;
                else
                    GroupSessions[CurrentGroup].Add(Req.ID, IrSe);
            }
            //switch (CurrentGroup)
            //{
            //    case("Red"):
            //        IrSe.Flags.Add("Group", "Red");
            //        lock (RedGroupSessions)
            //        {
            //            if (RedGroupSessions.ContainsKey(Req.ID))
            //                RedGroupSessions[Req.ID] = IrSe;
            //            else
            //                RedGroupSessions.Add(Req.ID, IrSe);
            //        }
            //        IronDB.AddToTestGroup(Req.ID, "Red");
            //        break;
            //    case ("Blue"):
            //        IrSe.Flags.Add("Group", "Blue");
            //        lock (BlueGroupSessions)
            //        {
            //            if (BlueGroupSessions.ContainsKey(Req.ID))
            //                BlueGroupSessions[Req.ID] = IrSe;
            //            else
            //                BlueGroupSessions.Add(Req.ID, IrSe);
            //        }
            //        IronDB.AddToTestGroup(Req.ID, "Blue");
            //        break;
            //    case ("Green"):
            //        IrSe.Flags.Add("Group", "Green");
            //        lock (GreenGroupSessions)
            //        {
            //            if (GreenGroupSessions.ContainsKey(Req.ID))
            //                GreenGroupSessions[Req.ID] = IrSe;
            //            else
            //                GreenGroupSessions.Add(Req.ID, IrSe);
            //        }
            //        IronDB.AddToTestGroup(Req.ID, "Green");
            //        break;
            //    case ("Gray"):
            //        IrSe.Flags.Add("Group", "Gray");
            //        lock (GrayGroupSessions)
            //        {
            //            if (GrayGroupSessions.ContainsKey(Req.ID))
            //                GrayGroupSessions[Req.ID] = IrSe;
            //            else
            //                GrayGroupSessions.Add(Req.ID, IrSe);
            //        }
            //        IronDB.AddToTestGroup(Req.ID, "Gray");
            //        break;
            //    case ("Brown"):
            //        IrSe.Flags.Add("Group", "Brown");
            //        lock (BrownGroupSessions)
            //        {
            //            if (BrownGroupSessions.ContainsKey(Req.ID))
            //                BrownGroupSessions[Req.ID] = IrSe;
            //            else
            //                BrownGroupSessions.Add(Req.ID, IrSe);
            //        }
            //        IronDB.AddToTestGroup(Req.ID, "Brown");
            //        break;
            //}
            IronDB.AddToTestGroup(Req.ID, CurrentGroup);
            IronUI.UpdateTestGroupLogGridWithRequest(IrSe);
        }

        internal static void StoreInGroupList(Response Res)
        {
            Session IrSe = null;
            
            foreach (string Group in GroupSessions.Keys)
            {                
                if (GroupSessions[Group].ContainsKey(Res.ID))
                {
                    lock (GroupSessions)
                    {
                        GroupSessions[Group][Res.ID].Response = Res;
                        IrSe = GroupSessions[Group][Res.ID];
                    }
                    break;
                }
            }
            //if (RedGroupSessions.ContainsKey(Res.ID))
            //{
            //    lock (RedGroupSessions)
            //    { 
            //        RedGroupSessions[Res.ID].Response = Res;
            //        IrSe = RedGroupSessions[Res.ID];
            //    }
            //}
            //else if (BlueGroupSessions.ContainsKey(Res.ID))
            //{
            //    lock (BlueGroupSessions)
            //    {
            //        BlueGroupSessions[Res.ID].Response = Res;
            //        IrSe = BlueGroupSessions[Res.ID];
            //    }
            //}
            //else if (GreenGroupSessions.ContainsKey(Res.ID))
            //{
            //    lock (GreenGroupSessions) 
            //    {
            //        GreenGroupSessions[Res.ID].Response = Res;
            //        IrSe = GreenGroupSessions[Res.ID];
            //    }
            //}
            //else if (GrayGroupSessions.ContainsKey(Res.ID))
            //{
            //    lock (GrayGroupSessions)
            //    {
            //        GrayGroupSessions[Res.ID].Response = Res;
            //        IrSe = GrayGroupSessions[Res.ID];
            //    }
            //}
            //else if (BrownGroupSessions.ContainsKey(Res.ID))
            //{
            //    lock (BrownGroupSessions)
            //    {
            //        BrownGroupSessions[Res.ID].Response = Res;
            //        IrSe = BrownGroupSessions[Res.ID];
            //    }
            //}
            if (IrSe != null)
            {
                if (IrSe.Flags.ContainsKey("Reflecton"))
                    IrSe.Flags["Reflecton"] = Analyzer.CheckReflections(IrSe);
                else
                    IrSe.Flags.Add("Reflecton", Analyzer.CheckReflections(IrSe));
                IronUI.UpdateTestGroupLogGridWithResponse(IrSe);
            }
        }

        internal static void SetCurrentID(int ID)
        {
            CurrentRequestID = ID;
            CurrentGroupLogId[CurrentGroup] = ID;
            //switch(CurrentGroup)
            //{
            //    case("Red"):
            //        RedGroupID = ID;
            //        break;
            //    case ("Blue"):
            //        BlueGroupID = ID;
            //        break;
            //    case ("Green"):
            //        GreenGroupID = ID;
            //        break;
            //    case ("Gray"):
            //        GrayGroupID = ID;
            //        break;
            //    case ("Brown"):
            //        BrownGroupID = ID;
            //        break;
            //}
        }

        internal static void ClearGroup()
        {
            ClearGroup(CurrentGroup, 0);
        }

        internal static void ClearGroup(string Group, int NewID)
        {
            SetCurrentID(NewID);
            lock(GroupSessions)
            {
                if (GroupSessions.ContainsKey(Group))
                {
                    GroupSessions.Remove(Group);
                }
            }
            //switch (CurrentGroup)
            //{
            //    case("Red"):
            //        lock (RedGroupSessions) { RedGroupSessions.Clear(); }
            //        break;
            //    case ("Blue"):
            //        lock (BlueGroupSessions) { BlueGroupSessions.Clear(); }
            //        break;
            //    case ("Green"):
            //        lock (GreenGroupSessions) { GreenGroupSessions.Clear(); }
            //        break;
            //    case ("Gray"):
            //        lock (GrayGroupSessions) { GrayGroupSessions.Clear(); }
            //        break;
            //    case ("Brown"):
            //        lock (BrownGroupSessions) { BrownGroupSessions.Clear(); }
            //        break;
            //}
            IronDB.ClearGroup(CurrentGroup);
        }

        internal static void ShowGroup(string Group)
        {
            //TestIDLbl.BackColor = Color.Red;
            //TestIDLbl.Text = "ID: 0";
            //ManualTesting.CurrentGroup = "Red";
            ManualTesting.CurrentGroup = Group;
            IronUI.ResetMTDisplayFields();
            IronUI.UpdateTestGroupLogGrid(ManualTesting.GroupSessions[Group]);
            ManualTesting.ShowSession(ManualTesting.CurrentGroupLogId[Group]);
            //if (ManualTesting.RedGroupID == 0) MTReqResTabs.SelectTab("MTRequestTab");
        }

        internal static string CreateNewGroupWithRequest(Request Req, bool SwitchToMTSection)
        {
            string Name = "";
            bool Named = false;
            while (!Named)
            {
                Name = string.Format("untitled-{0}", Interlocked.Increment(ref ManualTesting.UntitledCount));
                if (!ManualTesting.GroupSessions.ContainsKey(Name))
                {
                    Named = true;
                }
            }
            CreateNewGroupWithRequest(Req, Name, SwitchToMTSection);
            return Name;
        }

        internal static void CreateNewGroupWithRequest(Request Req, string Group, bool SwitchToMTSection)
        {
            int TestID = Interlocked.Increment(ref Config.TestRequestsCount);
            Req.ID = TestID;
            IronDB.LogMTRequest(Req);
            //IronDB.ClearGroup(Group);
            ManualTesting.CurrentRequestID = TestID;
            ManualTesting.CurrentGroup = Group;
            ManualTesting.ClearGroup(Group, TestID);
            ManualTesting.StoreInGroupList(Req);
            IronUI.SetNewTestRequest(Req, Group, SwitchToMTSection);
        }

        internal static void ShowSession(int ID)
        {
            Session IrSe = null;
            foreach (string Group in GroupSessions.Keys)
            {
                if(GroupSessions[Group].ContainsKey(ID))
                {
                    IrSe = GroupSessions[Group][ID];
                    IrSe.Flags["Group"] = Group;
                }
            }
            //if (RedGroupSessions.ContainsKey(ID))
            //{
            //    lock (RedGroupSessions) { IrSe = RedGroupSessions[ID]; IrSe.Flags["Group"] = "Red"; }
            //}
            //else if (BlueGroupSessions.ContainsKey(ID))
            //{
            //    lock (BlueGroupSessions) { IrSe = BlueGroupSessions[ID]; IrSe.Flags["Group"] = "Blue"; }
            //}
            //else if (GreenGroupSessions.ContainsKey(ID))
            //{
            //    lock (GreenGroupSessions) { IrSe = GreenGroupSessions[ID]; IrSe.Flags["Group"] = "Green"; }
            //}
            //else if (GrayGroupSessions.ContainsKey(ID))
            //{
            //    lock (GrayGroupSessions) { IrSe = GrayGroupSessions[ID]; IrSe.Flags["Group"] = "Gray"; }
            //}
            //else if (BrownGroupSessions.ContainsKey(ID))
            //{
            //    lock (BrownGroupSessions) { IrSe = BrownGroupSessions[ID]; IrSe.Flags["Group"] = "Brown"; }
            //}
            if (IrSe != null)
            {
                IronUI.ResetMTDisplayFields();
                IronUI.FillMTFields(IrSe);
                //if (IrSe.Flags.ContainsKey("Reflecton"))
                //    IronUI.FillTestReflection(IrSe.Flags["Reflecton"].ToString());
                //else
                //    IronUI.FillTestReflection("");
            }
        }

        internal static void ShowNextSession()
        {
            int[] IDs = GetGroupIDs();
            if (IDs == null) return;
            Array.Sort(IDs);
            foreach (int i in IDs)
            {
                if (i > CurrentRequestID)
                {
                    ShowSession(i);
                    return;
                }
            }
        }

        internal static void ShowPreviousSession()
        {
            int[] IDs = GetGroupIDs();
            if (IDs == null) return;
            Array.Sort(IDs);
            for (int i = IDs.Length - 1; i >= 0; i-- )
            {
                if (IDs[i] < CurrentRequestID)
                {
                    ShowSession(IDs[i]);
                    return;
                }
            }
        }

        static int[] GetGroupIDs()
        {
            if (GroupSessions.ContainsKey(CurrentGroup))
            {
                int[] IDs = new int[GroupSessions[CurrentGroup].Keys.Count];
                GroupSessions[CurrentGroup].Keys.CopyTo(IDs, 0);
                return IDs;
            }
            //switch (CurrentGroup)
            //{
            //    case("Red"):
            //        int[] RedIDs = new int[RedGroupSessions.Keys.Count];
            //        RedGroupSessions.Keys.CopyTo(RedIDs, 0);
            //        return RedIDs;
            //    case ("Blue"):
            //        int[] BlueIDs = new int[BlueGroupSessions.Keys.Count];
            //        BlueGroupSessions.Keys.CopyTo(BlueIDs, 0);
            //        return BlueIDs;
            //    case ("Green"):
            //        int[] GreenIDs = new int[GreenGroupSessions.Keys.Count];
            //        GreenGroupSessions.Keys.CopyTo(GreenIDs, 0);
            //        return GreenIDs;
            //    case ("Gray"):
            //        int[] GrayIDs = new int[GrayGroupSessions.Keys.Count];
            //        GrayGroupSessions.Keys.CopyTo(GrayIDs, 0);
            //        return GrayIDs;
            //    case ("Brown"):
            //        int[] BrownIDs = new int[BrownGroupSessions.Keys.Count];
            //        BrownGroupSessions.Keys.CopyTo(BrownIDs, 0);
            //        return BrownIDs;
            //}
            return null;
        }

        internal static void ScriptedSend()
        {
            if (ManualTesting.CurrentRequest == null) return;
            IronUI.ResetMTResponseDisplayFields();
            Request Request = ManualTesting.CurrentRequest.GetClone();
            Request.ID = Interlocked.Increment(ref Config.TestRequestsCount);
            StoreInGroupList(Request);
            SetCurrentID(Request.ID);
            IronDB.LogMTRequest(Request);
            IronUI.UpdateMTLogGridWithRequest(Request);
            ScriptedSender Sender = new ScriptedSender(Request.GetClone(), Request.ID, ScSe);   
            ThreadStart TS = new  ThreadStart(Sender.DoScriptedSend);
            Thread SSThread = new Thread(TS);
            SSThread.Start();
        }

        internal static string SetPyScriptedSend(string FunctionCode)
        {
            ScriptEngine Engine = Python.CreateEngine();
            StringBuilder FullCode = new StringBuilder();
            FullCode.AppendLine("from IronWASP import *");
            FullCode.AppendLine("import re");
            FullCode.AppendLine("class ss(ScriptedSender):");
            FullCode.AppendLine("  def ScriptedSend(self, req):");
            string[] CodeLines = FunctionCode.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string Line in CodeLines)
            {
                FullCode.Append("    ");
                FullCode.AppendLine(Line);
            }
            FullCode.AppendLine("    return res");
            FullCode.AppendLine("");
            FullCode.AppendLine("");
            FullCode.AppendLine("s = ss();");
            FullCode.AppendLine("ManualTesting.SetScriptedSender(s)");
            return SetScriptedSend(Engine, FullCode.ToString());
        }

        internal static string SetRbScriptedSend(string FunctionCode)
        {
            ScriptEngine Engine = Ruby.CreateEngine();
            StringBuilder FullCode = new StringBuilder();
            FullCode.AppendLine("include IronWASP");
            FullCode.AppendLine("class SS < ScriptedSender");
            FullCode.AppendLine("  def scripted_send(req)");
            string[] CodeLines = FunctionCode.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string Line in CodeLines)
            {
                FullCode.Append("    ");
                FullCode.AppendLine(Line);
            }
            FullCode.AppendLine("    return res");
            FullCode.AppendLine("  end");
            FullCode.AppendLine("end");
            FullCode.AppendLine("");
            FullCode.AppendLine("s = SS.new");
            FullCode.AppendLine("ManualTesting.set_scripted_sender(s)");
            return SetScriptedSend(Engine, FullCode.ToString());
        }

        internal static string SetScriptedSend(ScriptEngine Engine, string Code)
        {
            try
            {
                ScriptRuntime Runtime = Engine.Runtime;
                Assembly MainAssembly = Assembly.GetExecutingAssembly();
                string RootDir = Directory.GetParent(MainAssembly.Location).FullName;
                Runtime.LoadAssembly(MainAssembly);
                Runtime.LoadAssembly(typeof(String).Assembly);
                Runtime.LoadAssembly(typeof(Uri).Assembly);

                if (Engine.Setup.DisplayName.Contains("IronPython"))
                {
                    string[] Results = PluginEditor.CheckPythonIndentation(Code);
                    if (Results[1].Length > 0)
                    {
                        throw new Exception(Results[1]);
                    }
                }

                ScriptSource Source = Engine.CreateScriptSourceFromString(Code);
                Source.ExecuteProgram();
                return "";
            }
            catch(Exception Exp)
            {
                return Exp.Message;
            }
        }
        public static void SetScriptedSender(ScriptedSender ScSe)
        {
            ManualTesting.ScSe = ScSe;
        }
        internal static void HandleResponse(Session IrSe)
        {
            IrSe.FiddlerSession.state = Fiddler.SessionStates.Done;
            
            IronDB.LogMTResponse(IrSe.Response);

            IronUI.UpdateMTLogGridWithResponse(IrSe.Response);

            StoreInGroupList(IrSe.Response);
        }
        internal static void StartSend()
        {

        }
        internal static void EndSend()
        {

        }
        public static void StoreRequest(Request RequestToStore)
        {
            StoredRequestStack.Push(RequestToStore);
            if (StoredRequestStack.Count == 1)
            {
                IronUI.EnableStoredRequestBtn();
            }
        }
        internal static bool HasStoredRequest()
        {
            if (StoredRequestStack.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static Request GetStoredRequest()
        {
            Request StoredRequest = StoredRequestStack.Pop();
            lock (StoredRequestStack)
            {
                StoredRequestStack.Clear();                
            }
            return StoredRequest;
        }
        internal static void UpdateCurrentRequestWithNewHeader(string HeaderString)
        {
            string NewRequestHeaders = HeaderString.TrimEnd(new char[] { '\r', '\n' });
            NewRequestHeaders += "\r\n\r\n";
            Request ChangedRequest = new Request(NewRequestHeaders, ManualTesting.CurrentRequestIsSSL, false);
            ChangedRequest.ID = ManualTesting.CurrentRequestID;
            if (ManualTesting.CurrentRequest != null)
            {
                byte[] OldBody = new byte[ManualTesting.CurrentRequest.BodyArray.Length];
                ManualTesting.CurrentRequest.BodyArray.CopyTo(OldBody, 0);
                ChangedRequest.BodyArray = OldBody;
            }
            ManualTesting.CurrentRequest = ChangedRequest;
        }

        internal static void UpdateCurrentRequestWithNewBodyText(string BodyString)
        {
            if (ManualTesting.CurrentRequest != null)
            {
                if (ManualTesting.CurrentRequest.IsBinary)
                {
                    ManualTesting.CurrentRequest.BodyArray = Encoding.UTF8.GetBytes(BodyString);
                }
                else
                {
                    ManualTesting.CurrentRequest.BodyString = BodyString;
                }
            }
        }

        internal static void ResetChangedStatus()
        {
            ResetNonParameterChangedStatus();
            ResetParametersChangedStatus();
            RequestChanged = false;
        }
        internal static void ResetNonParameterChangedStatus()
        {
            RequestHeaderChanged = false;
            RequestBodyChanged = false;
        }
        internal static void ResetParametersChangedStatus()
        {
            RequestQueryParametersChanged = false;
            RequestBodyParametersChanged = false;
            RequestCookieParametersChanged = false;
            RequestHeaderParametersChanged = false;
        }

        //internal static void StartDeSerializingRequestBody(Request Request, FormatPlugin Plugin)
        //{
        //    BodyFormatParamters BFP = new BodyFormatParamters(Request, Plugin);
        //    RequestFormatThread = new Thread(ManualTesting.DeSerializeRequestBody);
        //    RequestFormatThread.Start(BFP);
        //}

        //internal static void DeSerializeRequestBody(object BFPObject)
        //{
        //    string PluginName = "";
        //    try
        //    {
        //        BodyFormatParamters BFP = (BodyFormatParamters)BFPObject;
        //        Request Request = BFP.Request;
        //        FormatPlugin Plugin = BFP.Plugin;
        //        PluginName = Plugin.Name;

        //        string XML = Plugin.ToXmlFromRequest(Request);
        //        IronUI.FillMTRequestFormatXML(XML);
        //    }
        //    catch (ThreadAbortException)
        //    {
        //        //
        //    }
        //    catch (Exception Exp)
        //    {
        //        IronException.Report("Error Deserializing 'Manual Testing' Request using Format Plugin - " + PluginName, Exp.Message, Exp.StackTrace);
        //        IronUI.ShowMTException("Error Deserializing");
        //    }
        //}

        //internal static void StartSerializingRequestBody(Request Request, FormatPlugin Plugin, string XML)
        //{
        //    BodyFormatParamters BFP = new BodyFormatParamters(Request, Plugin, XML);
        //    RequestFormatThread = new Thread(ManualTesting.SerializeRequestBody);
        //    RequestFormatThread.Start(BFP);
        //}

        //internal static void SerializeRequestBody(object BFPObject)
        //{
        //    string PluginName = "";
        //    try
        //    {
        //        BodyFormatParamters BFP = (BodyFormatParamters)BFPObject;
        //        Request Request = BFP.Request;
        //        FormatPlugin Plugin = BFP.Plugin;
        //        PluginName = Plugin.Name;
        //        string XML = BFP.XML;
                
        //        Request NewRequest = Plugin.ToRequestFromXml(Request, XML);
        //        IronUI.FillMTRequestWithNewRequestFromFormatXML(NewRequest, PluginName);
        //    }
        //    catch (ThreadAbortException)
        //    {
        //        //
        //    }
        //    catch (Exception Exp)
        //    {
        //        IronException.Report("Error Serializing 'Manual Testing' Request using Format Plugin - " + PluginName, Exp.Message, Exp.StackTrace);
        //        IronUI.ShowMTException("Error Serializing");
        //    }
        //}
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

        internal static Request GetRedirectRequestOnly()
        {
            Session CurrentSession = GroupSessions[CurrentGroup][CurrentGroupLogId[CurrentGroup]];
            if (CurrentSession.Response != null)
            {
                Request RedirectRequest = CurrentSession.Request.GetRedirect(CurrentSession.Response);
                return RedirectRequest;
            }
            return null;
        }
        
        internal static Request GetRedirect()
        {
            Request RedirectRequest = GetRedirectRequestOnly();
            if (RedirectRequest != null)
            {
                IronUI.ResetMTDisplayFields();
                Session RedirectSession = new Session(RedirectRequest);
                IronUI.FillMTFields(RedirectRequest);
            }
            else
            {
                IronUI.ShowMTException("Response does not contain a redirection");
            }
            return RedirectRequest;
        }

        internal static void FollowRedirect()
        {
            Request RedirectRequest = GetRedirect();
            if (RedirectRequest != null)
            {
                ManualTesting.SendRequest();
                IronUI.StartMTSend(ManualTesting.CurrentRequestID);
            }
        }

        internal static void UpdateCookieStoreFromResponse()
        {
            try
            {
                Session CurrentSession = GroupSessions[CurrentGroup][CurrentGroupLogId[CurrentGroup]];
                if (CurrentSession.Response == null)
                {
                    IronUI.ShowMTException("No Set-Cookie headers in the Response"); 
                }
                else
                {
                    CookieStore.AddToStore(CurrentSession.Request, CurrentSession.Response);
                }
            }
            catch(Exception Exp)
            {
                IronUI.ShowMTException("Error reading cookies from the Response");
                IronException.Report("Error reading cookies from the Manual Testing Response", Exp);
            }
        }

        internal static void UpdateRequestFromCookieStore()
        {
            try
            {
                if (ManualTesting.CurrentRequest == null)
                {
                    IronUI.ShowMTException("No valid Request found");
                }
                else
                {
                    Request NewRequest = ManualTesting.CurrentRequest.GetClone();
                    CookieStore.ReadFromStore(NewRequest);
                    IronUI.ResetMTDisplayFields();
                    IronUI.FillMTFields(NewRequest);   
                }
            }
            catch
            {
                IronUI.ShowMTException("No valid Request found");
            }
        }
    }
}
