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
    public class Parameters
    {
        Dictionary<string, List<string>> ParameterStore =  new Dictionary<string, List<string>>();

        public int Count
        {
            get
            {
                return ParameterStore.Count;
            }
        }

        internal Parameters()
        {

        }

        //Get Method
        public string RawGet(string Name)
        {
            return this.RawGet(Name, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public string RawGet(string Name, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            string SafeName = SafeRawMethod(Name);
            string Key = MatchAndGetKey(SafeName, EncodeMethod, DecodeMethod);
            if (Key == null)
            {
                throw new Exception("Parameter not found");
            }
            else
            {
                List<string> Values = ParameterStore[Key];
                return Values[0];   
            }
        }
        public string Get(string Name)
        {
            return BaseDecode(this.RawGet(BaseEncode(Name)));
        }

        //Set method
        public void RawSet(string Name, string Value)
        {
            this.RawSet(Name, Value, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public void RawSet(string Name, string Value, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            if (Name.Trim().Length == 0) return;
            string SafeName = SafeRawMethod(Name);
            string SafeValue = SafeRawMethod(Value);
            string Key = MatchAndGetKey(SafeName, EncodeMethod, DecodeMethod);
            if(Key == null)
                ParameterStore[SafeName] = new List<string>() { SafeValue };
            else
                ParameterStore[Key] = new List<string>() { SafeValue };
        }
        public void Set(string Name, string Value)
        {
            this.RawSet(BaseEncode(Name), BaseEncode(Value));            
        }

        //Set at point
        public void RawSet(string Name, int Position, string Value)
        {
            RawSet(Name, Position, Value, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public void RawSetAt(string Name, int Position, string Value)
        {
            RawSet(Name, Position, Value);
        }
        public void RawSet(string Name, int Position, string Value, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            if (Name.Trim().Length == 0) return;
            string SafeName = SafeRawMethod(Name);
            string SafeValue = SafeRawMethod(Value);
            if (Position < 0) return;
            string Key = MatchAndGetKey(SafeName, EncodeMethod, DecodeMethod);
            if (Key == null)
            {
                ParameterStore[SafeName] = new List<string>() { SafeValue };
            }
            else
            {
                if (Position >= ParameterStore[Key].Count)
                {
                    this.Add(Key, SafeValue);
                }
                else
                {
                    ParameterStore[Key][Position] = SafeValue;
                }   
            }
        }
        public void Set(string Name, int Position, string Value)
        {
            this.RawSet(BaseEncode(Name), Position, BaseEncode(Value));
        }
        public void SetAt(string Name, int Position, string Value)
        {
            this.Set(Name, Position, Value);
        }

        //Set mutliple
        public void RawSet(string Name, List<string> Values)
        {
            this.RawSet(Name, Values, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public void RawSet(string Name, List<string> Values, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            if (Name.Trim().Length == 0) return;
            string SafeName = SafeRawMethod(Name);
            List<string> SafeValues = new List<string>();
            foreach (string Value in Values)
            {
                SafeValues.Add(SafeRawMethod(Value));
            }
            string Key = MatchAndGetKey(SafeName, EncodeMethod, DecodeMethod);
            if (Key == null)
            {
                ParameterStore.Add(SafeName, SafeValues);
            }
            else
            {
                ParameterStore[Key] = SafeValues;
            }
        }
        public void Set(string Name, List<string> Values)
        {
            List<string> EncodedValues = new List<string>();
            foreach (string Value in Values)
            {
                EncodedValues.Add(BaseEncode(Value));
            }
            this.RawSet(BaseEncode(Name), EncodedValues);
        }

        //Add method
        public void RawAdd(string Name, string Value)
        {
            RawAdd(Name, Value, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public void RawAdd(string Name, string Value, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            if (Name.Trim().Length == 0) return;
            string SafeName = SafeRawMethod(Name);
            string SafeValue = SafeRawMethod(Value);
            string Key = MatchAndGetKey(SafeName, EncodeMethod, DecodeMethod);
            if (Key == null)
            {
                List<string> Values = new List<string>();
                Values.Add(SafeValue);
                ParameterStore.Add(SafeName, Values);
            }
            else
            {
                ParameterStore[Key].Add(SafeValue);
            }
        }
        public void Add(string Name, string Value)
        {
            this.RawAdd(BaseEncode(Name), BaseEncode(Value));
        }

        public List<string> GetNames()
        {
            List<string> Keys = new List<string>();
            foreach (string Key in ParameterStore.Keys)
            {
                Keys.Add(Key);
            }
            return Keys;
        }
        
        //GetAll method
        public List<string> RawGetAll(string Name)
        {
            return this.RawGetAll(Name, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public List<string> RawGetAll(string Name, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            string SafeName = SafeRawMethod(Name);
            string Key = MatchAndGetKey(Name, EncodeMethod, DecodeMethod);
            if (Key == null)
            {
                throw new Exception("Parameter not found"); 
            }
            else
            {
                return ParameterStore[Key];
            }
        }
        public List<string> GetAll(string Name)
        {
            List<string> RawValues = this.RawGetAll(BaseEncode(Name));
            List<string> DecodedValues = new List<string>();
            foreach (string Value in RawValues)
            {
                DecodedValues.Add(BaseDecode(Value));
            }
            return DecodedValues;
        }

        //GetMultis method
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
            List<string> RawMultis = this.RawGetMultis();
            List<string> DecodedMultis = new List<string>();
            foreach (string Multi in RawMultis)
            {
                DecodedMultis.Add(BaseDecode(Multi));
            }
            return DecodedMultis;
        }

        //Remove method
        public void RawRemove(string Name)
        {
            RawRemove(Name, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public void RawRemove(string Name, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            string SafeName = SafeRawMethod(Name);
            string Key = MatchAndGetKey(SafeName, EncodeMethod, DecodeMethod);
            if (Key != null)
            {
                ParameterStore.Remove(Key);
            }
        }
        public void Remove(string Name)
        {
            this.RawRemove(BaseEncode(Name));
        }

        public void RemoveAll()
        {
            this.ParameterStore = new Dictionary<string, List<string>>();
        }

        //Has method
        public bool RawHas(string Name)
        {
            return RawHas(Name, BaseSafeRaw, BaseEncode, BaseDecode);
        }
        public bool RawHas(string Name, SafeRawDelegate SafeRawMethod, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            string SafeName = SafeRawMethod(Name);
            string Key = MatchAndGetKey(SafeName, EncodeMethod, DecodeMethod);
            if (Key == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Has(string Name)
        {
            return this.RawHas(BaseEncode(Name));
        }
        
        protected string GetStringFromParameters(char Joiner)
        {
            StringBuilder SB = new StringBuilder();
            foreach (string Key in ParameterStore.Keys)
            {
                foreach (string Value in ParameterStore[Key])
                {
                    SB.Append(Key);
                    SB.Append("=");
                    SB.Append(Value);
                    SB.Append(Joiner);
                }
            }
            string QS = SB.ToString().TrimEnd(new char[] { Joiner });
            return QS;
        }

        //protected string GetStringFromHeaders()
        //{
        //    StringBuilder SB = new StringBuilder();
        //    StringBuilder Host = new StringBuilder("Host: ");
        //    bool HasHost = false;
        //    StringBuilder Cookie = new StringBuilder("Cookie: ");
        //    bool HasCookie = false;
        //    foreach (string Key in ParameterStore.Keys)
        //    {
        //        if (Key.Equals("Host", StringComparison.OrdinalIgnoreCase) && !HasHost)// && !Key.Equals("Cookie", StringComparison.OrdinalIgnoreCase))
        //        {
        //            Host.Append(ParameterStore[Key][0]);
        //            Host.Append("\r\n");
        //            HasHost = true;
        //        }
        //        else if (Key.Equals("Cookie", StringComparison.OrdinalIgnoreCase) && !HasCookie)
        //        {
        //            Cookie.Append(ParameterStore[Key][0]);
        //            Cookie.Append("\r\n");
        //            HasCookie = true;
        //        }
        //        else
        //        {
        //            foreach (string Value in ParameterStore[Key])
        //            {
        //                SB.Append(Key);
        //                SB.Append(": ");
        //                SB.Append(Value);
        //                SB.Append("\r\n");
        //            }
        //        }
        //    }
        //    if (HasCookie)
        //    {
        //        SB.Append(Cookie.ToString());
        //    }
        //    SB.Append("\r\n");
        //    if (HasHost)
        //    {
        //        Host.Append(SB.ToString());
        //        string QSWH = Host.ToString();
        //        return QSWH;
        //    }
        //    string QS = SB.ToString();
        //    return QS;
        //}
        
        //internal void BuildFromHeaderArray(string[] HeaderArray)
        //{
        //    foreach (string Element in HeaderArray)
        //    {
        //        if (Element.Length >= 3)
        //        {
        //            string[] HeaderParts = Element.Split(new char[] { ':' }, 2);
        //            if (HeaderParts.Length == 2)
        //            {
        //                this.Add(HeaderParts[0].Trim(), HeaderParts[1].Trim());
        //            }
        //        }
        //    }
        //}

        protected void AbsorbParametersFromString(string RawString, char Splitter)
        {
            if (RawString.Length > 0)
            {
                string[] RawParameters = RawString.Split(Splitter);
                foreach (string RequestParameter in RawParameters)
                {
                    string[] ParameterParts = RequestParameter.Split(new char[] { '=' }, 2 );
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
                this.ParameterStore = new Dictionary<string, List<string>>();
            }
        }

        public delegate string DecodeDelegate(string Input);
        public delegate string EncodeDelegate(string Input);
        public delegate string SafeRawDelegate(string Input);

        public string BaseDecode(string Input)
        {
            return Input;
        }
        public string BaseEncode(string Input)
        {
            return Input;
        }
        public string BaseSafeRaw(string Input)
        {
            return Input;
        }

        string MatchAndGetKey(string Name, EncodeDelegate EncodeMethod, DecodeDelegate DecodeMethod)
        {
            foreach(string Key in ParameterStore.Keys)
            {
                if(Key.Equals(Name)) return Key;
            }
            foreach(string Key in ParameterStore.Keys)
            {
                if(DecodeMethod(Key).Equals(DecodeMethod(Name))) return Key;
            }
            foreach(string Key in ParameterStore.Keys)
            {
                if(Key.Equals(DecodeMethod(Name))) return Key;
            }
            foreach(string Key in ParameterStore.Keys)
            {
                if(DecodeMethod(Key).Equals(Name)) return Key;
            }
            return null;
        }
    }
}
