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
    public class RequestHeaderParameters : HeaderParameters
    {
        Request Request;
        internal RequestHeaderParameters(Request Request) : base()
        {
            this.Request = Request;
        }
        internal RequestHeaderParameters(Request Request, HeaderParameters RequestHeaders) : base(RequestHeaders)
        {
            this.Request = Request;
        }
        new public void Set(string Name, string Value)
        {
            Name = ProcessName(Name);

            base.Set(Name, Value);
            if (Name.Equals("Cookie"))
            {
                this.ProcessUpdate(Value);
            }
        }
        new public void Set(string Name, int Position, string Value)
        {
            Name = ProcessName(Name);

            base.Set(Name, Position, Value);
            if (Name.Equals("Cookie"))
            {
                this.ProcessUpdate(Value);
            }
        }
        new public void Set(string Name, List<string> Values)
        {
            Name = ProcessName(Name);

            if ((Name.Equals("Host") || Name.Equals("Content-Length") || Name.Equals("Content-Type") || Name.Equals("User-Agent") || Name.Equals("Cookie")) && (Values.Count > 1))
            {                
                throw new Exception("Hostname, Content-Length, Content-Type, User-Agent cannot take multiple entries, try Add(string Name, string Value)");
            }
            else
            {
                base.Set(Name, Values);
            }
        }
        new public void Add(string Name, string Value)
        {
            Name = ProcessName(Name);

            if (Name.Equals("Host") || Name.Equals("Content-Length") || Name.Equals("Content-Type") || Name.Equals("User-Agent") || Name.Equals("Cookie"))
            {
                base.Set(Name, Value);
            }
            else
            {
                base.Add(Name, Value);
            }
            if (Name.Equals("Cookie"))
            {
                this.ProcessUpdate(Value);
            }
        }
        new public void Remove(string Name)
        {
            Name = ProcessName(Name);

            base.Remove(Name);
            if (Name.Equals("Cookie"))
            {
                this.ProcessUpdate("");
            }
        }
        new public void RemoveAll()
        {
            base.RemoveAll();
            this.ProcessUpdate("");
        }
        internal string GetAsString()
        {
            return base.GetStringFromHeaders();
        }

        void ProcessUpdate(string Value)
        {
            this.Request.UpdateCookieParametersWithoutUpdatingHeaders(Value);
        }

        string ProcessName(string Name)
        {
            if (Name.Equals("Host", StringComparison.OrdinalIgnoreCase)) Name = "Host";
            if (Name.Equals("Content-Length", StringComparison.OrdinalIgnoreCase)) Name = "Content-Length";
            if (Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)) Name = "Content-Type";
            if (Name.Equals("User-Agent", StringComparison.OrdinalIgnoreCase)) Name = "User-Agent";
            if (Name.Equals("Cookie", StringComparison.OrdinalIgnoreCase)) Name = "Cookie";
            return Name;
        }
    }
}
