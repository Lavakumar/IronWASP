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
    public class QueryParameters : Parameters
    {
        Request Request;

        internal QueryParameters(Request Request) : base()
        {
            this.Request = Request;
        }
        internal QueryParameters(Request Request, string URL) : base()
        {
            this.Request = Request;
            this.GetParametersFromString(URL);
        }

        public void RawSet(string Name, string Value)
        {
            base.Set(SafeRaw(Name), SafeRaw(Value));
            this.ProcessUpdate();
        }
        new public void Set(string Name, string Value)
        {
            this.RawSet(Encode(Name), Encode(Value));
        }

        public void RawSet(string Name, int Position, string Value)
        {
            base.Set(SafeRaw(Name), Position, SafeRaw(Value));
            this.ProcessUpdate();
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
            List<string> Vals = new List<string>();
            foreach (string Value in Values)
            {
                Vals.Add(SafeRaw(Value));
            }
            base.Set(SafeRaw(Name), Vals);
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

        public void RawAdd(string Name, string Value)
        {
            base.Add(SafeRaw(Name), SafeRaw(Value));
            this.ProcessUpdate();
        }
        new public void Add(string Name, string Value)
        {
            this.RawAdd(Encode(Name), Encode(Value));
        }

        public void RawRemove(string Name)
        {
            base.Remove(SafeRaw(Name));
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

        public bool RawHas(string Name)
        {
            return base.Has(SafeRaw(Name));
        }
        new public bool Has(string Name)
        {
            return this.RawHas(Encode(Name));
        }

        public string RawGet(string Name)
        {
            return base.Get(SafeRaw(Name));
        }
        new public string Get(string Name)
        {
            return Decode(this.RawGet(Encode(Name)));
        }

        public List<string> RawGetAll(string Name)
        {
            return base.GetAll(SafeRaw(Name));
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

        public List<string> RawGetMultis()
        {
            return base.GetMultis();
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

        internal string GetQueryStringFromParameters()
        {
            char Joiner = '&';
            return this.GetStringFromParameters(Joiner);
        }
        internal void GetParametersFromString(string URL)
        {
            char Splitter = '&';
            int StartOfQuery = URL.IndexOf("?");
            if (StartOfQuery >= 0)
            {
                string QueryString = URL.Substring(StartOfQuery + 1);
                this.AbsorbParametersFromString(QueryString, Splitter);
            }
        }

        new void AbsorbParametersFromString(string RawString, char Splitter)
        {
            if (RawString.Length > 0)
            {
                string[] RawParameters = RawString.Split(Splitter);
                foreach (string RequestParameter in RawParameters)
                {
                    string[] ParameterParts = RequestParameter.Split(new char[] { '=' }, 2);
                    if (ParameterParts.Length == 2)
                    {
                        this.Add(ParameterParts[0].Trim(), ParameterParts[1].Trim());
                    }
                    else if (ParameterParts.Length == 1)
                    {
                        this.Add(ParameterParts[0].Trim(), "");
                    }
                }
            }
            else
            {
                base.RemoveAll();
            }
        }

        void ProcessUpdate()
        {
            if (this.Request.FreezeURL) return;
            this.Request.UpdateURLWithQueryString(this.GetQueryStringFromParameters());
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
