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
    public class PluginResult
    {
        internal static PluginResult CurrentPluginResult=null;
        public int Id=0;
        public string Plugin="";
        public string Title="";
        public string Summary="";
        public string AffectedHost = "";
        public Triggers Triggers = new Triggers();
        public PluginResultSeverity Severity = PluginResultSeverity.Low;
        public PluginResultConfidence Confidence = PluginResultConfidence.Low;
        public PluginResultType ResultType = PluginResultType.Vulnerability;
        public string Signature="";

        public PluginResult(string AffectedHost)
        {
            this.AffectedHost = AffectedHost;
        }

        public void Report()
        {
            IronUpdater.AddPluginResult(this);
        }
    }
}
