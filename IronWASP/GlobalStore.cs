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
    public class GlobalStore
    {
        static Dictionary<string, object> InternalStore = new Dictionary<string,object>();

        public static void Put(string Name, object Value)
        {
            lock (InternalStore)
            {
                if (InternalStore.ContainsKey(Name))
                {
                    InternalStore[Name] = Value;
                }
                else
                {
                    InternalStore.Add(Name, Value);
                }
            }
        }

        public static object Get(string Name)
        {
            try
            {
                if (InternalStore.ContainsKey(Name))
                {
                    return InternalStore[Name];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool Has(string Name)
        {
            try
            {
                if (InternalStore.ContainsKey(Name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false; 
            }
        }

        public static void Remove(string Name)
        {
            try
            {
                lock (InternalStore)
                {
                    InternalStore.Remove(Name);
                }
            }
            catch { }
        }
    }
}
