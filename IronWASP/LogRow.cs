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
    public class LogRow
    {
        public int ID = 0;
        public int ScanID = 0;
        public bool SSL = false;
        public bool Marked = false;
        public bool BinaryRequest = false;
        public string Host = "";
        public string Method = "";
        public string Url = "";
        public string File = "";
        public string Parameters = "";
        public int Code = 0;
        public int Length = 0;
        public bool BinaryResponse = false;
        public string Mime = "";
        public bool SetCookie = false;
        public bool Editied = false;
        public string Notes = "";
        internal string OriginalRequestHeaders = "";
        internal string Source = "";

        internal object[] ToProxyGridRowObjectArray()
        {
            if (this.Code > -1)
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, this.Code, this.Length, this.Mime, this.SetCookie, this.Editied, this.Notes };
            }
            else
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, null, null, "", false, this.Editied, "" };
            }
        }
        internal object[] ToTestGridRowObjectArray()
        {
            if (this.Code > -1)
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, this.Code, this.Length, this.Mime, this.SetCookie };
            }
            else
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, null, null, "", false };
            }
        }
        internal object[] ToShellGridRowObjectArray()
        {
            if (this.Code > -1)
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, this.Code, this.Length, this.Mime, this.SetCookie };
            }
            else
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, null, null, "", false };
            }
        }
        internal object[] ToScanGridRowObjectArray()
        {
            if (this.Code > -1)
            {
                return new object[] { this.ID, this.ScanID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, this.Code, this.Length, this.Mime, this.SetCookie };
            }
            else
            {
                return new object[] { this.ID, this.ScanID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, null, null, "", false };
            }
        }
        internal object[] ToProbeGridRowObjectArray()
        {
            if (this.Code > -1)
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, this.Code, this.Length, this.Mime, this.SetCookie };
            }
            else
            {
                return new object[] { this.ID, this.Host, this.Method, this.Url, this.File, this.SSL, this.Parameters, null, null, "", false };
            }
        }
    }
}
