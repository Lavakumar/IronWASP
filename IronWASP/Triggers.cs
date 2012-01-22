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
using System.Text;

namespace IronWASP
{
    public class Triggers
    {
        List<Trigger> TriggerList = new List<Trigger>();
        List<string> RequestTriggers = new List<string>();
        List<string> ResponseTriggers = new List<string>();
        
        public void Add(string RequestTrigger, Request Req, string ResponseTrigger, Response Res)
        {
            if(Req != null || Res != null)
            {
                Trigger T = new Trigger(RequestTrigger, Req, ResponseTrigger, Res);
                this.TriggerList.Add(T);
            }
        }
        public void Add(string RequestTrigger, Request Req)
        {
            if (Req != null)
            {
                Trigger T = new Trigger(RequestTrigger, Req);
                this.TriggerList.Add(T);
            }
        }
        internal List<Trigger> GetTriggers()
        {
            return this.TriggerList;
        }

        internal Trigger GetTrigger(int TriggerNumber)
        {
            return TriggerList[TriggerNumber];
        }
    }
}
