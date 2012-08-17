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

        internal static int ScanTraceMin = 0;
        internal static int ScanTraceMax = 0;

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

        internal static void MoveScanTraceRecordForward(int JumpLevel)
        {
            IronUI.ShowScanTraceStatus("Loading please wait....", false);
            Thread T = new Thread(MoveScanTraceRecordForward);
            T.Start(JumpLevel);
        }
        internal static void MoveScanTraceRecordForward(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<IronTrace> Records = GetNextScanTraceRecords(JumpLevel);
            if (Records.Count == 0)
            {
                IronUI.ShowScanTraceStatus("Reached end of Scan Traces", true);
                return;
            }
            IronUI.SetScanTraceGrid(Records);
        }
        internal static List<IronTrace> GetNextScanTraceRecords(int JumpLevel)
        {
            int JumpCount = IronLog.GetJumpCount(JumpLevel);
            int StartIndex = IronTrace.ScanTraceMax + JumpCount;
            List<IronTrace> Records = IronDB.GetScanTraceRecords(StartIndex, IronLog.MaxRowCount);
            if (Records.Count == 0)
            {
                int NewStartIndex = Config.LastScanTraceId - IronLog.MaxRowCount;
                if (NewStartIndex > 0)
                {
                    Records = IronDB.GetScanTraceRecords(NewStartIndex, IronLog.MaxRowCount);
                    if (Records.Count > 0)
                    {
                        if (Records[Records.Count - 1].ID == IronTrace.ScanTraceMax) Records.Clear();
                    }
                }
            }
            return Records;
        }


        internal static void MoveScanTraceRecordBack(int JumpLevel)
        {
            IronUI.ShowScanTraceStatus("Loading please wait....", false);
            Thread T = new Thread(MoveScanTraceRecordBack);
            T.Start(JumpLevel);
        }
        internal static void MoveScanTraceRecordBack(object JumpLevelObj)
        {
            int JumpLevel = (int)JumpLevelObj;
            List<IronTrace> Records = GetPreviousScanTraceRecords(JumpLevel);
            if (Records.Count == 0) return;
            IronUI.SetScanTraceGrid(Records);
        }

        internal static List<IronTrace> GetPreviousScanTraceRecords(int JumpLevel)
        {
            List<IronTrace> Records = new List<IronTrace>();
            int CurrentMin = IronTrace.ScanTraceMin;
            int JumpCount = IronLog.GetJumpCount(JumpLevel);
            if (CurrentMin <= 1)
            {
                IronUI.ShowScanTraceStatus("Reached beginning of the log. Cannot go back further.", true);
                return Records;
            }
            int StartIndex = CurrentMin - IronLog.MaxRowCount - JumpCount - 1;
            Records = IronDB.GetScanTraceRecords(StartIndex, IronLog.MaxRowCount);
            return Records;
        }

        static int[] GetScanTraceMinMaxIds(List<IronTrace> Records)
        {
            int[] MinMax = new int[] { 0, 0 };
            if (Records.Count > 0)
            {
                MinMax[0] = Records[0].ID;
                MinMax[1] = Records[Records.Count - 1].ID;
            }
            return MinMax;
        }
    }
}
