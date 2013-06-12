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

        internal static string GetSamplePythonScript()
        {
            return @"
#Scripted Send allows you to customize the Request sending action in the Manual Testing section
#This is a sample Python script to showcase the Scripted Send feature
#To enable this feature click on 'Activate this Script for Scripted Send'
#Once you do that the 'Scripted Send' button will be enabled in the Manual Testing section
#When you click on the 'Scripted Send' button the script written here will be executed.

#This sample script will add a new header to all requests you send via the 'Scripted Send' feature
#req is the Request that is currently in the active Manual Testing window
req.Headers.Set(""X-Sent-By"", ""Scripted-Send"")
res = req.Send()
#res is the Response variable that will be returned by this script and which will be ultimately displayed in the Manual Testin section
#The log of the request with this additional header will be available in the 'Shell Logs' section

#In addition to customizing the Send feature you can also automate Request updating using 'Stored Request' feature
#For example here if the response sends a Set-Cookie header then we update the Request with this new cookie value
#The updated Request is stored in memory and can be accessed by clicking on the 'Stored Request' button
#If there are no requests stored in memory then this button is disbled
if len(res.SetCookies) > 0:
  new_req = req.GetClone()
  new_req.SetCookie(res)
  self.StoreRequest(new_req)

#To debug your script you can make use of the command Tools.Trace(""Scripted Send"", ""Test Message"")
#This command will add an entry to the 'Debug Trace' section of the 'Dev' section. The two arguments must be strings, they can contain any value.
#This would provide a functionality similar to using the print command in your code. 

";
        }

        internal static string GetSampleRubyScript()
        {
            return @"#Scripted Send allows you to customize the Request sending action in the Manual Testing section
#This is a sample Ruby script to showcase the Scripted Send feature
#To enable this feature click on 'Activate this Script for Scripted Send'
#Once you do that the 'Scripted Send' button will be enabled in the Manual Testing section
#When you click on the 'Scripted Send' button the script written here will be executed.

#This sample script will add a new header to all requests you send via the 'Scripted Send' feature
#req is the Request that is currently in the active Manual Testing window
req.headers.set(""X-Sent-By"", ""Scripted-Sendy"")
res = req.send_req
#res is the Response variable that will be returned by this script and which will be ultimately displayed in the Manual Testin section
#The log of the request with this additional header will be available in the 'Shell Logs' section

#In addition to customizing the Send feature you can also automate Request updating using 'Stored Request' feature
#For example here if the response sends a Set-Cookie header then we update the Request with this new cookie value
#The updated Request is stored in memory and can be accessed by clicking on the 'Stored Request' button
#If there are no requests stored in memory then this button is disbled
if res.set_cookies.count > 0
  new_req = req.get_clone
  new_req.set_cookie(res)
  store_request(new_req)
end

#To debug your script you can make use of the command Tools.trace(""Scripted Send"", ""Test Message"")
#This command will add an entry to the 'Debug Trace' section of the 'Dev' section. The two arguments must be strings, they can contain any value.
#This would provide a functionality similar to using the print command in your code. 

";
        }
    }
}
