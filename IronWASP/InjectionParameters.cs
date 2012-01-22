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
    class InjectionParameters
    {
        Dictionary<string, List<int>> ParameterStore =  new Dictionary<string, List<int>>();

        public int Count
        {
            get
            {
                return ParameterStore.Count;
            }
        }

        internal InjectionParameters()
        {

        }
        
        public int Get(string Name)
        {
            if (ParameterStore.ContainsKey(Name))
            {
                List<int> Values = ParameterStore[Name];
                return Values[0];
            }
            else
            {
                //Must return custom exception
                int Value = 0;
                return Value;
            }
        }
        
        public void Add(string Name, int Value)
        {
            if (ParameterStore.ContainsKey(Name))
            {
                List<int> StoredValues = ParameterStore[Name];
                foreach (int StoredValue in StoredValues)
                {
                    if (StoredValue == Value)
                    {
                        return;
                    }
                }
                ParameterStore[Name].Add(Value);
            }
            else
            {
                List<int> Values = new List<int>();
                Values.Add(Value);
                ParameterStore.Add(Name,Values);
            }
        }
        public List<string> GetAll()
        {
            List<string> Keys = new List<string>();
            foreach (string Key in ParameterStore.Keys)
            {
                Keys.Add(Key);
            }
            return Keys;
        }
        public List<int> GetAll(string Name)
        {
            if (ParameterStore.ContainsKey(Name))
            {
                return ParameterStore[Name];
            }
            else
            {
                //Must return custom exception
                List<int> EmptyValues = new List<int>();
                return EmptyValues;
            }
        }
        public List<string> GetMultis()
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
        public void Remove(string Name)
        {
            if (ParameterStore.ContainsKey(Name))
            {
                ParameterStore.Remove(Name);
            }
        }

        public bool Has(string Name)
        {
            if (ParameterStore.ContainsKey(Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool AllSelected()
        {
            foreach (string Key in ParameterStore.Keys)
            {

            }
            return false;
        }
    }
}
