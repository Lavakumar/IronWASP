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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace IronWASP
{
    internal class Import
    {
        internal static Thread BurpImportThread;

        internal static void ImportBurpLog(string BurpLogFile)
        {
            IronUI.OpenImportForm();
            IronUI.SetUIVisibility(false);
            BurpImportThread = new Thread(StartImportBurpLog);
            BurpImportThread.Start(BurpLogFile);
        }

        internal static void StartImportBurpLog(Object BurpLogFile)
        {
            try
            {
                StreamReader SR = new StreamReader(BurpLogFile.ToString());
                ReadBurpMessages(SR);
                SR.Close();
            }
            catch(Exception Exp)
            {
                IronUI.CloseImportForm();
                MessageBox.Show("Unable to import log - " + Exp.Message);
                return;
            }
            IronUI.SetUIVisibility(true);
            IronUI.CloseImportForm();
        }

        static void ReadBurpMessages(StreamReader Reader)
        {
            Queue<string> ResponseLines = new Queue<string>();
            string MetaLine = "";

            List<string> Lines = new List<string>();
            Lines.Add(Reader.ReadLine());
            Lines.Add(Reader.ReadLine());
            Lines.Add(Reader.ReadLine());
            
            if (Reader.EndOfStream) return;
            if (Lines[0].Equals("======================================================") && Lines[1].IndexOf("  http") > 5 && Lines[1].IndexOf("  http") < 20 & Lines[2].Equals("======================================================"))
            {
                MetaLine = Lines[1];
                string[] Result = ReadBurpMessage(Reader);
                ProcessBurpMessage(Result[0], MetaLine);
                MetaLine = Result[1];
                if (MetaLine.Length == 0) return;
            }
            else
            {
                return;
            }

            while(!Reader.EndOfStream)
            {
                MetaLine = Lines[1];
                string[] Result = ReadBurpMessage(Reader);
                ProcessBurpMessage(Result[0], MetaLine);
                MetaLine = Result[1];
                if (MetaLine.Length == 0) return;
            }
        }

        static string[] ReadBurpMessage(StreamReader Reader)
        {
            string[] Result = new string[2];
            Queue<string> MessageLines = new Queue<string>();
            StringBuilder MessageBuilder = new StringBuilder();
            while(MessageLines.Count < 7 && !Reader.EndOfStream)
            {
                MessageLines.Enqueue(Reader.ReadLine());
            }
            while (!Reader.EndOfStream)
            {
                string[] ResponseBuffer = MessageLines.ToArray();
                if (ResponseBuffer[0].Equals("======================================================") && ResponseBuffer[1].Equals("") && ResponseBuffer[2].Equals("") && ResponseBuffer[3].Equals("") && ResponseBuffer[4].Equals("======================================================") && ResponseBuffer[5].IndexOf("  http") > 5 && ResponseBuffer[5].IndexOf("  http") < 20 && ResponseBuffer[6].Equals("======================================================"))
                {
                    Result[0] = MessageBuilder.ToString();
                    Result[1] = ResponseBuffer[5];
                    break;
                }
                else
                {
                    MessageBuilder.AppendLine(MessageLines.Dequeue());
                    MessageLines.Enqueue(Reader.ReadLine());
                }
                if (Reader.EndOfStream)
                {
                    Result[0] = MessageBuilder.ToString();
                    Result[1] = "";
                    break;
                }
            }
            return Result;
        }

        static void ProcessBurpMessage(string BurpMessage, string MetaLine)
        {
            string[] BurpMessageParts = BurpMessage.Split(new string[] { "\r\n======================================================\r\n" }, 2, StringSplitOptions.RemoveEmptyEntries);
            Session IrSe = null;
            if (BurpMessageParts.Length > 0)
            {
                Request Req = ReadBurpRequest(BurpMessageParts[0], MetaLine);
                if (Req != null)
                {
                    IrSe = new Session(Req);
                    IrSe.ID = Interlocked.Increment(ref Config.ProxyRequestsCount);
                    IronUpdater.AddProxyRequest(IrSe.Request.GetClone(true));
                    PassiveChecker.AddToCheckRequest(IrSe);
                }
            }
            if (BurpMessageParts.Length == 2)
            {
                if (IrSe != null)
                {
                    try
                    {
                        Response Res = new Response(BurpMessageParts[1]);
                        IrSe.Response = Res;
                        IrSe.Response.ID = IrSe.Request.ID;
                        IronUpdater.AddProxyResponse(IrSe.Response.GetClone(true));
                        PassiveChecker.AddToCheckResponse(IrSe);
                    }
                    catch
                    {

                    }
                }
            }
        }

        static Request ReadBurpRequest(string RequestString, string MetaLine)
        {
            string[] MetaParts = MetaLine.Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
            if (MetaParts.Length != 2) return null;
            bool SSL = false;
            if (MetaParts[0].EndsWith("https")) SSL = true;
            try
            {
                Request Req = new Request(RequestString, SSL);
                return Req;
            }
            catch
            {
                return null;
            }
        }
    }
}
