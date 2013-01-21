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
    public class IronLogRecord
    {
        internal int ID = 0;
        internal string RequestHeaders="";
        internal string RequestBody = "";
        internal bool SSL = false;
        internal bool IsRequestBinary = false;
        internal string ResponseHeaders = "";
        internal string ResponseBody = "";
        internal bool IsResponseBinary = false;

        internal string OriginalRequestHeaders = "";
        internal string OriginalRequestBody = "";
        internal bool OriginalSSL = false;
        internal bool IsOriginalRequestBinary = false;
        internal string OriginalResponseHeaders = "";
        internal string OriginalResponseBody = "";
        internal bool IsOriginalResponseBinary = false;
    }
}
