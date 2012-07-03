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
    public class ReportPlugin : Plugin
    {
        internal static List<ReportPlugin> Collection = new List<ReportPlugin>();

        public static void Add(ReportPlugin RP)
        {
            if ((RP.Name.Length > 0) && !(RP.Name.Equals("All") || RP.Name.Equals("None")))
            {
                if (!List().Contains(RP.Name))
                {
                    Collection.Add(RP);
                }
            }
        }

        public static List<string> List()
        {
            List<string> Names = new List<string>();
            foreach (ReportPlugin RP in Collection)
            {
                Names.Add(RP.Name);
            }
            return Names;
        }

        public static ReportPlugin Get(string Name)
        {
            foreach (ReportPlugin RP in Collection)
            {
                if (RP.Name.Equals(Name))
                {
                    return RP;
                }
            }
            return null;
        }
    }
}
