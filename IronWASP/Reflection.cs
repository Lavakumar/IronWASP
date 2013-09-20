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
    public class Reflection
    {
        public string Name = "";
        public string Value = "";
        public string Section = "";
        List<string> Contexts = new List<string>();
        List<string> Reflections = new List<string>();

        public int Length
        {
            get
            {
                return this.Value.Length;
            }
        }

        public int Count
        {
            get
            {
                return Reflections.Count;
            }
        }

        public Reflection(string Name, string Value, string Section)
        {
            this.Name = Name;
            this.Value = Value;
            this.Section = Section;
        }

        public List<string> GetReflections()
        {
            return new List<string>(Reflections);
        }

        public List<string> GetContexts()
        {
            return new List<string>(Contexts);
        }

        public void Add(string Context, string Reflection)
        {
            Contexts.Add(Context);
            Reflections.Add(Reflection);
        }

        public void Add(string Reflection)
        {
            Add("", Reflection);
        }
    }

    public class Reflections
    {
        public List<Reflection> Url = new List<Reflection>();
        public List<Reflection> UrlPathPart = new List<Reflection>();
        public List<Reflection> Query = new List<Reflection>();
        public List<Reflection> Body = new List<Reflection>();
        public List<Reflection> Cookie = new List<Reflection>();
        public List<Reflection> Header = new List<Reflection>();

        public int Count
        {
            get
            {
                return Url.Count + UrlPathPart.Count + Query.Count + Body.Count + Cookie.Count + Header.Count;
            }
        }

        public List<List<Reflection>> GetList()
        {
            return new List<List<Reflection>> { Url, UrlPathPart, Query, Body, Cookie, Header };
        }

        public bool HasUrlPathPartMatch(int Position)
        {
            string PositionName = string.Format("UrlPathPart : {0}", Position);
            foreach (Reflection Ref in UrlPathPart)
            {
                if (Ref.Name == PositionName)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasQueryMatch(string Name)
        {
            foreach (Reflection Ref in Query)
            {
                if (Ref.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasBodyMatch(string Name)
        {
            foreach (Reflection Ref in Body)
            {
                if (Ref.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasCookieMatch(string Name)
        {
            foreach (Reflection Ref in Cookie)
            {
                if (Ref.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasHeaderMatch(string Name)
        {
            foreach (Reflection Ref in Header)
            {
                if (Ref.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
