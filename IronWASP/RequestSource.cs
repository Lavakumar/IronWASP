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
    public static class RequestSource
    {
        static List<string> BannedSources = new List<string> { "Trigger", "TestGroup", "SelectedLogEntry", "CurrentProxyInterception" };
        static List<string> InternalSources = new List<string> { "Proxy" };
        
        public const string Test = "Test";
        public const string Shell = "Shell";
        public const string Scan = "Scan";
        public const string Probe = "Probe";
        public const string Proxy = "Proxy";
        public const string Stealth = "Stealth";
        
        internal const string Trigger = "Trigger";
        internal const string TestGroup = "TestGroup";
        internal const string SelectedLogEntry = "SelectedLogEntry";
        internal const string CurrentProxyInterception = "CurrentProxyInterception";

        public static bool IsBanned(string EnteredSource)
        {
            if (BannedSources.Contains(EnteredSource))
                return true;
            else
                return false;
        }
        public static bool IsInternal(string EnteredSource)
        {
            if (InternalSources.Contains(EnteredSource))
                return true;
            else
                return false;
        }
        public static bool IsValid(string EnteredSource)
        {
            Regex R = new Regex("^[a-zA-Z]*$");
            if (EnteredSource.Length > 0 && R.IsMatch(EnteredSource))
                return true;
            else
                return false;
        }

    }
}
