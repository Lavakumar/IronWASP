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
    public class BodyParameters : Parameters
    {
        Request Request;

        internal BodyParameters(Request Request) : base()
        {
            this.Request = Request;
        }
        internal BodyParameters(Request Request, string BodyString) : base()
        {
            this.Request = Request;
            this.GetParametersFromString(BodyString);
        }

        //Set method
        new public void RawSet(string Name, string Value)
        {
            //base.Set(SafeRaw(Name), SafeRaw(Value));
            base.RawSet(Name, Value, SafeRaw, Encode, Decode);
            this.ProcessUpdate();
        }
        new public void Set(string Name, string Value)
        {
            this.RawSet(Encode(Name), Encode(Value));
        }

        //Set at method
        new public void RawSet(string Name, int Position, string Value)
        {
            //base.Set(SafeRaw(Name), Position, SafeRaw(Value));
            base.RawSet(Name, Position, Value, SafeRaw, Encode, Decode);
            this.ProcessUpdate();
        }
        new public void Set(string Name, int Position, string Value)
        {
            this.RawSet(Encode(Name), Position, Encode(Value));
        }
        new public void RawSetAt(string Name, int Position, string Value)
        {
            this.RawSet(Name, Position, Value);
        }
        new public void SetAt(string Name, int Position, string Value)
        {
            this.Set(Name, Position, Value);
        }

        //Set multi method
        new public void RawSet(string Name, List<string> Values)
        {
            //List<string> Vals = new List<string>();
            //foreach (string Value in Values)
            //{
            //    Vals.Add(SafeRaw(Value));
            //}
            //base.Set(SafeRaw(Name), Vals);
            base.RawSet(Name, Values, SafeRaw, Encode, Decode);
            this.ProcessUpdate();
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

        //Add method
        new public void RawAdd(string Name, string Value)
        {
            //base.Add(SafeRaw(Name), SafeRaw(Value));
            base.RawAdd(Name, Value, SafeRaw, Encode, Decode);
            this.ProcessUpdate();
        }
        new public void Add(string Name, string Value)
        {
            this.RawAdd(Encode(Name), Encode(Value));
        }

        //Remove method
        new public void RawRemove(string Name)
        {
            //base.Remove(SafeRaw(Name));
            base.RawRemove(Name, SafeRaw, Encode, Decode);
            this.ProcessUpdate();
        }
        new public void Remove(string Name)
        {
            this.RawRemove(Encode(Name));
        }

        new public void RemoveAll()
        {
            base.RemoveAll();
            this.ProcessUpdate();
        }

        //Has method
        new public bool RawHas(string Name)
        {
            //return base.Has(SafeRaw(Name));
            return base.RawHas(Name, SafeRaw, Encode, Decode);
        }
        new public bool Has(string Name)
        {
            return this.RawHas(Encode(Name));
        }

        //Get method
        new public string RawGet(string Name)
        {
            //try
            //{
            //    return base.Get(SafeRaw(Name));
            //}
            //catch(Exception Exp)
            //{
            //    if (Exp.Message.Equals("Parameter not found"))
            //    {
            //        //lets check if the name is available in decoded form
            //        return base.Get(Decode(Name));
            //    }
            //    else
            //    {
            //        throw Exp;
            //    }
            //}
            return base.RawGet(Name, SafeRaw, Encode, Decode);
        }
        new public string Get(string Name)
        {
            return Decode(this.RawGet(Encode(Name)));
        }

        //GetAll method
        new public List<string> RawGetAll(string Name)
        {
            //try
            //{
            //    return base.GetAll(SafeRaw(Name));
            //}
            //catch(Exception Exp)
            //{
            //    if (Exp.Message.Equals("Parameter not found"))
            //    {
            //        //lets check if the name is available in decoded form
            //        return base.GetAll(Decode(Name));
            //    }
            //    else
            //    {
            //        throw Exp;
            //    }
            //}
            return base.RawGetAll(Name, SafeRaw, Encode, Decode);
        }
        new public List<string> GetAll(string Name)
        {
            List<string> Values = this.RawGetAll(Encode(Name));
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = Decode(Values[i]);
            }
            return Values;
        }

        public List<string> RawGetNames()
        {
            return base.GetNames();
        }
        new public List<string> GetNames()
        {
            List<string> Values = this.RawGetNames();
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = Decode(Values[i]);
            }
            return Values;
        }

        new public List<string> RawGetMultis()
        {
            return base.RawGetMultis();
        }
        new public List<string> GetMultis()
        {
            List<string> Values = this.RawGetMultis();
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = Decode(Values[i]);
            }
            return Values;
        }

        internal string GetBodyStringFromParameters()
        {
            char Joiner = '&';
            return this.GetStringFromParameters(Joiner);
        }
        internal void GetParametersFromString(string BodyString)
        {
            char Splitter = '&';
            this.AbsorbParametersFromString(BodyString, Splitter);
        }
        void ProcessUpdate()
        {
            if (this.Request.FreezeBodyString) return;
            this.Request.SetBodyWithoutUpdatingParameters(this.GetBodyStringFromParameters());
        }

        string Encode(string Value)
        {
            return Tools.UrlEncode(Value);
        }

        string Decode(string Value)
        {
            return Tools.UrlDecode(Value);
        }

        string SafeRaw(string Value)
        {
            return Tools.RelaxedUrlEncode(Value);
        }
    }
}
