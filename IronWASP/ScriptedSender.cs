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

namespace IronWASP
{
    public class ScriptedSender
    {
        internal Request Request;
        internal int ID;
        internal ScriptedSender ActualSender;

        public ScriptedSender()
        {

        }
        public virtual Response ScriptedSend(Request Req)
        {
            Response Res = new Response();
            return Res;
        }
        public void StoreRequest(Request Request)
        {
            ManualTesting.StoreRequest(Request.GetClone());
        }
        internal ScriptedSender(Request Request, int ID, ScriptedSender ActualSender)
        {
            this.Request = Request;
            this.ID = ID;
            this.ActualSender = ActualSender;
        }
        internal void DoScriptedSend()
        {
            try
            {
                Response FinalResponse = ActualSender.ScriptedSend(this.Request).GetClone();
                FinalResponse.ID = this.ID;
                IronDB.LogMTResponse(FinalResponse);
                IronUI.UpdateMTLogGridWithResponse(FinalResponse);
                ManualTesting.StoreInGroupList(FinalResponse);
            }
            catch(Exception Exp)
            {
                IronException.Report("ScriptedSend Failed", Exp.Message, Exp.StackTrace);
                IronUI.ShowScriptedSendScriptException(Exp.Message);
                IronUI.ShowMTException("ScriptedSend Failed");
            }
        }
    }
}
