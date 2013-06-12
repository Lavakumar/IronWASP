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
    public class ScriptedInterceptor
    {
        public bool CallAfterInterception = false;
        public bool AfterInterception = false;
        
        public ScriptedInterceptor()
        {

        }

        public virtual bool ShouldIntercept(Session Sess)
        {
            return false;
        }

        public bool DoesMatchRules(Session Sess)
        {
            if (Sess.Response == null)
            {
                return this.DoesMatchRequestInterceptionRules(Sess);
            }
            else
            {
                return this.DoesMatchResponseInterceptionRules(Sess);
            }
        }

        public bool DoesMatchRequestInterceptionRules(Session Sess)
        {
            return IronProxy.CanInterceptBasedOnFilter(Sess.Request);
        }

        public bool DoesMatchResponseInterceptionRules(Session Sess)
        {
            if (Sess.Response == null)
            {
                return false;
            }
            else
            {
                return IronProxy.CanInterceptBasedOnFilter(Sess.Request, Sess.Response);
            }
        }

        internal static string GetSamplePythonScript()
        {
            return @"
#Scripted Interception allows you to determine when a Request or Response must be intercepted
#You can also manipulate Request or Response going through the proxy without intercepting them
#This is a sample Python script to showcase the 'Scripted Interception' feature
#To enable this feature click on 'Activate this Script for Scripted Interception'
#Once you do that the script here will be executed everytime a Request or Response goes through the proxy

#sess is the Session object containing the Request or Response currently going through the proxy
#You can analyze and modify it. To intercept it you must return the boolean value - True

#If this session does not match the Proxy Traffic Interception Rules defined in the config we don't intercept it
#The rules in the config can be accessed by clicking on 'Show Config' on the top right corner of IronWASP
#Doing this helps filter out things you had already blocked in the config
if not self.DoesMatchRules(sess):
  return False

#We check if the session object has a valid Response, if it does then currently a Response is passing through the proxy
if sess.Response:
  #We check if the Response has SetCookie headers and if the corresponding request Url is equal to '/login'
  #If that is the case we return True and this Response will be intercepted and displayed to the user
  if len(sess.Response.SetCookies) > 0 and sess.Request.Url == ""/login"":
    return True
#If the session object does not have valid Response then currently a Request is passing through the proxy
else:
  #We check if the Request has a body parameter named 'c_token' if it does then we change it to some other value
  if sess.Request.Body.Has(""c_token""):
    sess.Request.Body.Set(""c_token"", ""xxxxxxxxx"")

#To debug your script you can make use of the command Tools.Trace(""Scripted Interception"", ""Test Message"")
#This command will add an entry to the 'Debug Trace' section of the 'Dev' section. The two arguments must be strings, they can contain any value.
#This would provide a functionality similar to using the print command in your code. 

";
        }

        internal static string GetSampleRubyScript()
        {
            return @"

#Scripted Interception allows you to determine when a Request or Response must be intercepted
#You can also manipulate Request or Response going through the proxy without intercepting them
#This is a sample Python script to showcase the 'Scripted Interception' feature
#To enable this feature click on 'Activate this Script for Scripted Interception'
#Once you do that the script here will be executed everytime a Request or Response goes through the proxy

#sess is the Session object containing the Request or Response currently going through the proxy
#You can analyze and modify it. To intercept it you must return the boolean value - true

#If this session does not match the Proxy Traffic Interception Rules defined in the config we don't intercept it
#The rules in the config can be accessed by clicking on 'Show Config' on the top right corner of IronWASP
#Doing this helps filter out things you had already blocked in the config
if not does_match_rules(sess)
  return false
end

#We check if the session object has a valid Response, if it does then currently a Response is passing through the proxy
if sess.response
  #We check if the Response has SetCookie headers and if the corresponding request Url is equal to '/login'
  #If that is the case we return true and this Response will be intercepted and displayed to the user
  if sess.response.set_cookies.count > 0 and sess.request.url == ""/login""
    return true
  end
#If the session object does not have valid Response then currently a Request is passing through the proxy
else
  #We check if the Request has a body parameter named 'c_token' if it does then we change it to some other value
  if sess.request.body.has(""c_token""):
    sess.request.body.set(""c_token"", ""xxxxxxx"")
  end
end

#To debug your script you can make use of the command Tools.trace(""Scripted Interception"", ""Test Message"")
#This command will add an entry to the 'Debug Trace' section of the 'Dev' section. The two arguments must be strings, they can contain any value.
#This would provide a functionality similar to using the print command in your code. 

";
        }
    }
}
