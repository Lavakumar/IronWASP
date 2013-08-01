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
    public class FindingReason
    {
        public string Reason = "";
        public string ReasonType = "";
        public List<int> TriggerIds = new List<int>();
        public string FalsePositiveCheck = "";        
        
        public FindingReason(string Reason, string ReasonType, object TriggerIds, string FalsePositiveCheck)
        {
            this.Reason = Reason;
            this.ReasonType = ReasonType;
            try
            {
                this.TriggerIds = (List<int>)TriggerIds;
            }
            catch
            {
                try
                {
                    this.TriggerIds = new List<int>() { Int32.Parse(TriggerIds.ToString().Trim()) };
                }
                catch
                {
                    this.TriggerIds.Clear();
                    try
                    {
                        foreach (object TriggerId in Tools.ToDotNetList(TriggerIds))
                        {
                            this.TriggerIds.Add(Int32.Parse(TriggerId.ToString().Trim()));
                        }
                    }
                    catch
                    {
                        throw new Exception("Trigger Id must be an integer or a list of integers");
                    }
                }
            }
            this.FalsePositiveCheck = FalsePositiveCheck;
        }

        public static string TriggerIdsToString(List<int> TriggerIds)
        {
            StringBuilder SB = new StringBuilder();
            foreach (int TriggerId in TriggerIds)
            {
                SB.Append(TriggerId);
                SB.Append(",");
            }
            return SB.ToString().Trim(',');
        }
        public static List<int> StringToTriggerIds(string TriggerIdString)
        {
            List<int> TriggerIds = new List<int>();
            foreach (string Part in TriggerIdString.Split(','))
            {
                try
                {
                    TriggerIds.Add(Int32.Parse(Part.Trim()));
                }
                catch { }
            }
            return TriggerIds;
        }
    }
}
