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
    public class ResponseHeaderParameters : HeaderParameters
    {
        Response Response;
        internal ResponseHeaderParameters(Response Response) : base()
        {
            this.Response = Response;
        }
        internal ResponseHeaderParameters(Response Response, HeaderParameters ResponseHeaders) : base(ResponseHeaders)
        {
            this.Response = Response;
            this.Response.CreateSetCookieListFromParameters(ResponseHeaders);
        }
        new public void Set(string Name, string Value)
        {
            Name = ProcessName(Name);
            base.Set(Name, Value);
            if (Name.Equals("Set-Cookie"))
            {
                this.ProcessUpdate();
            }
        }
        new public void Set(string Name, int Position, string Value)
        {
            Name = ProcessName(Name);
            base.Set(Name, Position, Value);
            if (Name.Equals("Set-Cookie"))
            {
                this.ProcessUpdate();
            }
        }
        new public void Set(string Name, List<string> Values)
        {
            Name = ProcessName(Name);
            base.Set(Name, Values);
            if (Name.Equals("Set-Cookie"))
            {
                this.ProcessUpdate();
            }
        }
        new public void Add(string Name, string Value)
        {
            Name = ProcessName(Name);
            base.Add(Name, Value);
            if (Name.Equals("Set-Cookie"))
            {
                this.ProcessUpdate();
            }
        }
        new public void Remove(string Name)
        {
            Name = ProcessName(Name);
            base.Remove(Name);
            if (Name.Equals("Set-Cookie"))
            {
                this.ProcessUpdate();
            }
        }
        new public void RemoveAll()
        {
            base.RemoveAll();
            this.ProcessUpdate();
        }
        internal string GetAsString()
        {
            return base.GetStringFromHeaders();
        }

        void ProcessUpdate()
        {
            this.Response.CreateSetCookieListFromHeaders();
        }

        string ProcessName(string Name)
        {
            if (Name.Equals("Set-Cookie", StringComparison.OrdinalIgnoreCase)) Name = "Set-Cookie";
            return Name;
        }
    }
}
