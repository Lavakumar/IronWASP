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
    public class HeaderParameters
    {
        Dictionary<string, List<string>> ParameterStore =  new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public int Count
        {
            get
            {
                return ParameterStore.Count;
            }
        }

        internal HeaderParameters()
        {

        }

        internal HeaderParameters(HeaderParameters Headers)
        {
            this.ParameterStore = Headers.ParameterStore;
        }

        internal HeaderParameters(string[] HeaderArray)
        {
            this.BuildFromHeaderArray(HeaderArray);
        }

        public string RawGet(string Name)
        {
            Name = SafeRaw(Name);
            if (ParameterStore.ContainsKey(Name))
            {
                List<string> Values = ParameterStore[Name];
                return Values[0];
            }
            else
            {
                throw new Exception("Parameter not found");
            }
        }
        public string Get(string Name)
        {
            return Decode(this.RawGet(Encode(Name)));
        }

        public void Set(string Name, string Value)
        {
            Name = SafeRaw(Name);
            Value = SafeRaw(Value);
            if (Name.Trim().Length == 0) return;
            ParameterStore[Name] = new List<string>() { Value };
        }
        public void Set(string Name, int Position, string Value)
        {
            Name = SafeRaw(Name);
            Value = SafeRaw(Value);
            if (Name.Trim().Length == 0) return;
            if (Position < 0) return;
            if (ParameterStore.ContainsKey(Name))
            {
                if (Position >= ParameterStore[Name].Count)
                {
                    this.Add(Name, Value);
                }
                else
                {
                    ParameterStore[Name][Position] = Value;
                }
            }
            else
            {
                ParameterStore[Name] = new List<string>() { Value };
            }
        }
        public void SetAt(string Name, int Position, string Value)
        {
            this.Set(Name, Position, Value);
        }
        public void Set(string Name, List<string> Values)
        {
            Name = SafeRaw(Name);
            List<string> Vals = new List<string>();
            foreach(string Value in Values)
            {
                Vals.Add(SafeRaw(Value));
            }
            if (Name.Trim().Length == 0) return;
            if (ParameterStore.ContainsKey(Name))
            {
                ParameterStore[Name] = Vals;
            }
            else
            {
                ParameterStore.Add(Name, Vals);
            }
        }

        public void Add(string Name, string Value)
        {
            Name = SafeRaw(Name);
            Value = SafeRaw(Value);
            if (Name.Trim().Length == 0) return;
            if (ParameterStore.ContainsKey(Name))
            {
                ParameterStore[Name].Add(Value);
            }
            else
            {
                List<string> Values = new List<string>();
                Values.Add(Value);
                ParameterStore.Add(Name,Values);
            }
        }

        public List<string> RawGetNames()
        {
            List<string> Keys = new List<string>();
            foreach (string Key in ParameterStore.Keys)
            {
                Keys.Add(Key);
            }
            return Keys;
        }
        public List<string> GetNames()
        {
            List<string> Values = this.RawGetNames();
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = Decode(Values[i]);
            }
            return Values;
        }

        public List<string> RawGetAll(string Name)
        {
            Name = SafeRaw(Name);
            if (ParameterStore.ContainsKey(Name))
            {
                return new List<string>(ParameterStore[Name]);
            }
            else
            {
                throw new Exception("Parameter not found");
            }
        }
        public List<string> GetAll(string Name)
        {
            List<string> Values = this.RawGetAll(Encode(Name));
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = Decode(Values[i]);
            }
            return Values;
        }

        public List<string> RawGetMultis()
        {
            List<string> Multis = new List<string>();
            foreach (string Key in ParameterStore.Keys)
            {
                if (ParameterStore[Key].Count > 1)
                {
                    Multis.Add(Key);
                }
            }
            return Multis;
        }
        public List<string> GetMultis()
        {
            List<string> Values = this.RawGetMultis();
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i] = Decode(Values[i]);
            }
            return Values;
        }

        public void Remove(string Name)
        {
            Name = SafeRaw(Name);
            if (ParameterStore.ContainsKey(Name))
            {
                ParameterStore.Remove(Name);
            }
        }

        public void RemoveAll()
        {
            this.ParameterStore = new Dictionary<string, List<string>>();
        }

        public bool RawHas(string Name)
        {
            Name = SafeRaw(Name);
            if (ParameterStore.ContainsKey(Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Has(string Name)
        {
            return this.RawHas(Encode(Name));
        }
        
        protected string GetStringFromHeaders()
        {
            StringBuilder SB = new StringBuilder();
            StringBuilder Host = new StringBuilder("Host: ");
            bool HasHost = false;
            StringBuilder Cookie = new StringBuilder("Cookie: ");
            bool HasCookie = false;
            foreach (string Key in ParameterStore.Keys)
            {
                if (Key.Equals("Host", StringComparison.OrdinalIgnoreCase) && !HasHost)// && !Key.Equals("Cookie", StringComparison.OrdinalIgnoreCase))
                {
                    Host.Append(ParameterStore[Key][0]);
                    Host.Append("\r\n");
                    HasHost = true;
                }
                else if (Key.Equals("Cookie", StringComparison.OrdinalIgnoreCase) && !HasCookie)
                {
                    Cookie.Append(ParameterStore[Key][0]);
                    Cookie.Append("\r\n");
                    HasCookie = true;
                }
                else
                {
                    foreach (string Value in ParameterStore[Key])
                    {
                        SB.Append(Key);
                        SB.Append(": ");
                        SB.Append(Value);
                        SB.Append("\r\n");
                    }
                }
            }
            if (HasCookie)
            {
                SB.Append(Cookie.ToString());
            }
            SB.Append("\r\n");
            if (HasHost)
            {
                Host.Append(SB.ToString());
                string QSWH = Host.ToString();
                return QSWH;
            }
            string QS = SB.ToString();
            return QS;
        }
        internal void BuildFromHeaderArray(string[] HeaderArray)
        {
            foreach (string Element in HeaderArray)
            {
                if (Element.Length >= 3)
                {
                    string[] HeaderParts = Element.Split(new char[] { ':' }, 2);
                    if (HeaderParts.Length == 2)
                    {
                        this.Add(HeaderParts[0].Trim(), HeaderParts[1].Trim());
                    }
                }
            }
        }

        internal static string Encode(string Value)
        {
            return Tools.HeaderEncode(Value);
        }

        internal static string Decode(string Value)
        {
            return Value;
        }

        internal static string SafeRaw(string Value)
        {
            return Tools.HeaderEncode(Value);
        }
    }
}
