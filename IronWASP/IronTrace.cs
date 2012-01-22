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
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    internal class IronTrace
    {
        internal int ID=0;
        internal int ThreadID = 0;
        internal string Time = "";
        internal string Date = "";
        internal string Source = "";
        internal string Message = "";

        internal int ScanID = 0;
        internal string PluginName = "";
        internal string Section = "";
        internal string Parameter = "";
        internal string Title = "";

        string Type = "Normal";

        internal IronTrace()
        {

        }
        
        internal IronTrace(string Source, string Message)
        {
            this.ID = Interlocked.Increment(ref Config.TraceCount);
            this.ThreadID = Thread.CurrentThread.ManagedThreadId;
            this.Time = DateTime.Now.ToShortTimeString();
            this.Date = DateTime.Now.ToShortDateString();
            this.Source = Source;
            this.Message = Message;
            this.Type = "Normal";
        }

        internal IronTrace(int ScanID, string PluginName, string Section, string Parameter, string Title, string Message)
        {
            this.ID = Interlocked.Increment(ref Config.ScanTraceCount);
            this.ScanID = ScanID;
            this.PluginName = PluginName;
            this.Section = Section;
            this.Parameter = Parameter;
            this.Title = Title;
            this.Message = Message;
            this.Type = "Scan";
        }

        internal void Report()
        {
            if (this.Type.Equals("Normal"))
            {
                IronUpdater.AddTrace(this);
            }
            else
            {
                IronUpdater.AddScanTrace(this);
            }
        }
    }
}
