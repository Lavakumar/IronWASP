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
        new public void Set(string Name, List<string> Values)
        {
            for (int i = 0; i < Values.Count; i++ )
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
        new public bool Has(string Name)
        {
            return base.Has(Encode(Name));
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
            return Tools.RelaxedUrlEncode(Value);
        }
    }
}
