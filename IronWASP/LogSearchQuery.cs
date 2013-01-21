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
    class LogSearchQuery
    {
        internal string LogSource = "";

        internal int MinLogId = -1;
        internal int MaxLogId = -1;

        internal List<int> LogIds = new List<int>();

        internal int ScanLogId = -1;

        internal string Keyword = "";
        internal bool SearchRequestHeaders = false;
        internal bool SearchResponseHeaders = false;
        internal bool SearchRequestBody = false;
        internal bool SearchResponseBody = false;

        internal string UrlMatchString = "";
        internal int UrlMatchMode = 0;

        internal List<string> MethodsToIgnore = new List<string>();
        internal List<string> MethodsToCheck = new List<string>();

        internal List<int> CodesToIgnore = new List<int>();
        internal List<int> CodesToCheck = new List<int>();

        internal List<string> HostNamesToIgnore = new List<string>();
        internal List<string> HostNamesToCheck = new List<string>();

        internal List<string> FileExtensionsToIgnore = new List<string>();
        internal List<string> FileExtensionsToCheck = new List<string>();

    }
}
