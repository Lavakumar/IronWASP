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
    public class CookieParameters : Parameters
    {
        Request Request;

        internal CookieParameters(Request Request) : base()
        {
            this.Request = Request;
        }
        internal CookieParameters(Request Request, string CookieString) : base()
        {
            this.Request = Request;
            this.GetParametersFromString(CookieString);
        }
        new public void Set(string Name, string Value)
        {
            base.Set(Encode(Name), Encode(Value));
            this.ProcessUpdate();
        }
        new public void Set(string Name, int Position, string Value)
        {
            base.Set(Encode(Name), Position, Encode(Value));
            this.ProcessUpdate();
        }
        new public void SetAt(string Name, int Position, string Value)
        {
            this.Set(Name, Position, Value);
        }
        new public void Set(string Name, List<string> Values)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = Encode(Values[i]);
            }
            base.Set(Encode(Name), Values);
            this.ProcessUpdate();
        }
        new public void Add(string Name, string Value)
        {
            base.Add(Encode(Name), Encode(Value));
            this.ProcessUpdate();
        }
        new public void Remove(string Name)
        {
            base.Remove(Encode(Name));
            this.ProcessUpdate();
        }
        new public void RemoveAll()
        {
            base.RemoveAll();
            this.ProcessUpdate();
        }
        internal string GetCookieStringFromParameters()
        {
            char Joiner = ';';
            return this.GetStringFromParameters(Joiner);
        }
        internal void GetParametersFromString(string CookieString)
        {
            char Splitter = ';';
            this.AbsorbParametersFromString(CookieString, Splitter);
        }
        void ProcessUpdate()
        {
            if (this.Request.FreezeCookieString) return;
            if (this.Request.FreezeCookieHeader) return;
            this.Request.SetCookieWithoutUpdatingParameters(this.GetCookieStringFromParameters());
        }
        string Encode(string Value)
        {
            //return Tools.RelaxedUrlEncode(Value);
            return Value;//Cookie values don't need encoding
        }
    }
}
