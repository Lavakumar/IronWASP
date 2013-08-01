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

        public void RawSet(string Name, string Value)
        {
            Name = ProcessName(Name);

            base.Set(Name, Value);
            if (Name.Equals("Cookie"))
            {
                this.ProcessUpdate(Value);
            }
        }
        new public void Set(string Name, string Value)
        {
            this.RawSet(Encode(Name), Encode(Value));
        }

        public void RawSet(string Name, int Position, string Value)
        {
            Name = ProcessName(Name);

            base.Set(Name, Position, Value);
            if (Name.Equals("Cookie"))
            {
                this.ProcessUpdate(Value);
            }
        }
        new public void Set(string Name, int Position, string Value)
        {
            this.RawSet(Encode(Name), Position, Encode(Value));
        }

        public void RawSetAt(string Name, int Position, string Value)
        {
            this.RawSet(Name, Position, Value);
        }
        new public void SetAt(string Name, int Position, string Value)
        {
            this.Set(Name, Position, Value);
        }

        public void RawSet(string Name, List<string> Values)
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
        new public void Set(string Name, List<string> Values)
        {
            List<string> Vals = new List<string>();
            foreach (string Value in Values)
            {
                Vals.Add(Encode(Value));
            }
            this.RawSet(Encode(Name), Vals);
        }

        public void RawAdd(string Name, string Value)
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
        new public void Add(string Name, string Value)
        {
            this.RawAdd(Encode(Name), Encode(Value));
        }

        public void RawRemove(string Name)
        {
            Name = ProcessName(Name);
            if (Name == "Host")
            {
                return;
            }
            base.Remove(Name);
            if (Name.Equals("Cookie"))
            {
                this.ProcessUpdate("");
            }
        }
        new public void Remove(string Name)
        {
            this.RawRemove(Encode(Name));
        }

        new public void RemoveAll()
        {
            string Host = "";
            if (this.Has("Host"))
            {
                Host = this.Get("Host");
            }
            base.RemoveAll();
            this.Set("Host", Host);
            this.ProcessUpdate("");
        }
        internal string GetAsString()
        {
            return base.GetStringFromHeaders();
        }

        void ProcessUpdate(string Value)
        {
            this.Request.UpdateCookieParametersWithoutUpdatingHeaders(SafeRaw(Value));
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

        new string SafeRaw(string Value)
        {
            return Tools.HeaderEncode(Value);
        }

        new string Encode(string Value)
        {
            return Tools.HeaderEncode(Value);
        }
    }
}
