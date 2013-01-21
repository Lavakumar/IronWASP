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
    internal class BodyFormatParamters
    {
        internal Request Request;
        internal Response Response;
        internal FormatPlugin Plugin;
        internal string XML;
        internal List<bool> CheckStatus;
        internal bool CheckAll;

        internal BodyFormatParamters(Request Request, FormatPlugin Plugin)
        {
            this.Request = Request;
            this.Plugin = Plugin;
        }
        internal BodyFormatParamters(Request Request, FormatPlugin Plugin, string XML)
        {
            this.Request = Request;
            this.Plugin = Plugin;
            this.XML = XML;
        }
        internal BodyFormatParamters(Request Request, FormatPlugin Plugin, List<bool>CheckStatus, bool CheckAll)
        {
            this.Request = Request;
            this.Plugin = Plugin;
            this.CheckStatus = CheckStatus;
            this.CheckAll = CheckAll;
        }
        internal BodyFormatParamters(Response Response, FormatPlugin Plugin)
        {
            this.Response = Response;
            this.Plugin = Plugin;
        }
        internal BodyFormatParamters(Response Response, FormatPlugin Plugin, string XML)
        {
            this.Response = Response;
            this.Plugin = Plugin;
            this.XML = XML;
        }
    }
}
