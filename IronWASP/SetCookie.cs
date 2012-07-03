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
    public class SetCookie
    {
        string name = "";
        string value = "";
        string fullString = "";
        List<string> attributes = new List<string>();
        string path = "";
        string domain = "";
        string expires = "";
        string maxAge = "";
        string comment = "";
        string version = "";
        bool httpOnly = false;
        bool secure = false;

        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public string Value
        {
            get
            {
                return this.value;
            }
        }
        public string FullString
        {
            get
            {
                return this.fullString;
            }
        }
        public List<string> Attributes
        {
            get
            {
                return this.attributes;
            }
        }
        public string Path
        {
            get
            {
                return this.path;
            }
        }
        public string Domain
        {
            get
            {
                return this.domain;
            }
        }
        public string Expires
        {
            get
            {
                return this.expires;
            }
        }
        public string MaxAge
        {
            get
            {
                return this.maxAge;
            }
        }
        public string Comment
        {
            get
            {
                return this.comment;
            }
        }
        public string Version
        {
            get
            {
                return this.version;
            }
        }
        public bool HttpOnly
        {
            get
            {
                return this.httpOnly;
            }
        }
        public bool Secure
        {
            get
            {
                return this.secure;
            }
        }

        public SetCookie(string SetCookieString)
        {
            this.fullString = SetCookieString;
            string[] Parts = this.fullString.Split(new char[]{';'}, StringSplitOptions.RemoveEmptyEntries);
            if (Parts.Length == 0) return;
            int NVE = Parts[0].IndexOf("=");
            if(NVE > 0)
            {
                this.name = Parts[0].Substring(0, NVE).Trim();
                if(Parts[0].Length > (NVE + 1))
                {
                    this.value = Parts[0].Substring(NVE + 1).Trim();
                }
            }
            for(int i=1; i<Parts.Length; i++)
            {
                NVE = Parts[i].IndexOf("=");
                if(NVE > 0)
                {
                    string K = Parts[i].Substring(0, NVE).ToLower().Trim();
                    if(Parts[i].Length > (NVE + 1))
                    {
                        string V = Parts[i].Substring(NVE + 1).Trim();   
                        if(K.Equals("domain"))
                        {
                            this.domain = V;
                        }
                        else if(K.Equals("path"))
                        {
                            this.path = V;
                        }
                        else if(K.Equals("max-age"))
                        {
                            this.maxAge = V;
                        }
                        else if(K.Equals("expires"))
                        {
                            this.expires = V;
                        }
                        else if(K.Equals("comment"))
                        {
                            this.comment = V;
                        }
                        else if(K.Equals("version"))
                        {
                            this.version = V;
                        }
                    }
                }
                else
                {
                    string Part = Parts[i].ToLower().Trim();
                    if(Part.Equals("httponly"))
                    {
                        this.httpOnly = true;
                    }
                    else if(Part.Equals("secure"))
                    {
                        this.secure = true;
                    }
                }
                this.attributes.Add(Parts[i]);
            } 
        }

        internal void SetDomain(string Domain)
        {
            this.domain = Domain;
        }
    }
}
