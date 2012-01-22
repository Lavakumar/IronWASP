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
    public class Trigger
    {
        public string RequestTrigger = "";
        public Request Request;
        public string ResponseTrigger = "";
        public Response Response;
        public Trigger(string RequestTrigger, Request Req, string ResponseTrigger, Response Res)
        {
            this.RequestTrigger = RequestTrigger;
            this.Request = Req.GetClone();
            this.ResponseTrigger = ResponseTrigger;
            this.Response = Res.GetClone();
        }
        public Trigger(string RequestTrigger, Request Req)
        {
            this.RequestTrigger = RequestTrigger;
            this.Request = Req.GetClone();
        }
    }
}
