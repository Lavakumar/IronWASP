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
using System.Text.RegularExpressions;

namespace IronWASP
{
    public class CookieStore
    {
        static CookieStore StaticCookieStore = new CookieStore();
        
        List<SetCookie> SetCookies = new List<SetCookie>();

        public static void AddToStore(Request Req, Response Res)
        {
            StaticCookieStore.Add(Req, Res);
        }
        public static void ReadFromStore(Request Req)
        {
            Req.SetCookie(StaticCookieStore);
        }

        public void Add(Request Req, Response Res)
        {
            List<string> CookieStrings = new List<string>();
            foreach (SetCookie SC in Res.SetCookies)
            {
                CookieStrings.Add(SC.FullString);
            }
            Add(Req.Host, CookieStrings);
        }

        public void Add(Request Req, SetCookie SC)
        {
            Add(Req.Host, SC.FullString);
        }

        public void Add(string Host, List<string> Cookies)
        {
            List<SetCookie> NewCookies = new List<SetCookie>();
            foreach (string CookieString in Cookies)
            {
                SetCookie SC = new SetCookie(CookieString);
                if (SC.Domain.Length == 0) SC.SetDomain(Host);
                NewCookies.Add(SC);
            }
            lock (SetCookies)
            {
                foreach (SetCookie SC in NewCookies)
                {
                    List<int> OverWritePositions = new List<int>();
                    for (int i = 0; i < SetCookies.Count; i++)
                    {
                        if (SetCookies[i].Name.Equals(SC.Name))
                        {
                            if ((SetCookies[i].Domain.Equals(SC.Domain) || SetCookies[i].Domain.EndsWith("." + SC.Domain))
                                && (SetCookies[i].Path.Equals(SC.Path) || Regex.IsMatch(SetCookies[i].Path, "^" + SC.Path + "\\W+.*") || SetCookies[i].Path.Length == 0 || SetCookies[i].Path.Equals("/")))
                            {
                                OverWritePositions.Add(i);
                            }
                        }
                    }
                    for (int i = 0; i < OverWritePositions.Count; i++)
                    {
                        SetCookies.RemoveAt(OverWritePositions[i] - i);
                    }
                    SetCookies.Add(SC);
                }
            }
        }

        public void Add(string Host, string Cookie)
        {
            List<string> CookieStrings = new List<string>() { Cookie };
            Add(Host, CookieStrings);
        }

        public List<SetCookie> GetCookies()
        {
            List<SetCookie> Cookies = new List<SetCookie>(SetCookies);
            return Cookies;
        }

        public List<SetCookie> GetCookies(Request Req)
        {
            List<SetCookie> AllCookies = new List<SetCookie>(SetCookies);
            List<SetCookie> Cookies = new List<SetCookie>();
            foreach (SetCookie SC in AllCookies)
            {
                string StoreDomain = SC.Domain;
                if (StoreDomain.StartsWith("*")) StoreDomain = StoreDomain.TrimStart(new char[]{'*'});
                if (StoreDomain.StartsWith(".")) StoreDomain = StoreDomain.TrimStart(new char[] { '.' });
                if ((Req.Host.Equals(SC.Domain) || Req.Host.EndsWith("." + StoreDomain))
                                && (Req.Url.Equals(SC.Path) || Regex.IsMatch(Req.Url, "^" + SC.Path + "\\W+.*") || Req.Url.Length == 0 || SC.Path.Length == 0 || SC.Path.Equals("/")))
                {
                    if (SC.Secure && !Req.SSL) continue;
                    Cookies.Add(SC);
                }
            }
            return Cookies;
        }
    }
}
