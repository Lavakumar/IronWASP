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
    public class PassivePlugin : Plugin
    {
        public PluginCallingState CallingState = PluginCallingState.Offline;
        public PluginWorksOn WorksOn = PluginWorksOn.Both;
        internal static List<PassivePlugin> Collection = new List<PassivePlugin>();
        internal static List<string> DeactivatedPlugins = new List<string>();

        public virtual void Check(Session IrSe, Findings Results, bool ReportDuplicates)
        {

        }

        public virtual PassivePlugin GetInstance()
        {
            return new PassivePlugin();
        }

        public static void Add(PassivePlugin PP)
        {
            if ((PP.Name.Length > 0) && !(PP.Name.Equals("All") || PP.Name.Equals("None")))
            {
                if (!List().Contains(PP.Name))
                {
                    PP.FileName = PluginEngine.FileName;
                    Collection.Add(PP);
                }
            }
        }

        public static List<string> List()
        {
            List<string> Names = new List<string>();
            foreach (PassivePlugin PP in Collection)
            {
                if(!DeactivatedPlugins.Contains(PP.Name)) Names.Add(PP.Name);
            }
            return Names;
        }

        public static List<string> GetInLinePluginsForRequest()
        {
            List<string> Names = new List<string>();
            foreach (PassivePlugin PP in Collection)
            {
                if (!DeactivatedPlugins.Contains(PP.Name))
                {
                    if (PP.WorksOn == PluginWorksOn.Request || PP.WorksOn == PluginWorksOn.Both)
                        Names.Add(PP.Name);
                }
            }
            return Names;
        }

        public static List<string> GetInLinePluginsForResponse()
        {
            List<string> Names = new List<string>();
            foreach (PassivePlugin PP in Collection)
            {
                if (!DeactivatedPlugins.Contains(PP.Name))
                {
                    if (PP.WorksOn == PluginWorksOn.Response || PP.WorksOn == PluginWorksOn.Both)
                        Names.Add(PP.Name);
                }
            }
            return Names;
        }

        public static PassivePlugin Get(string Name)
        {
            foreach (PassivePlugin PP in Collection)
            {
                if (PP.Name.Equals(Name))
                {
                    PassivePlugin NewInstance = PP.GetInstance();
                    if (PP.FileName != "Internal")
                    {
                        NewInstance.FileName = PP.FileName;
                    }
                    return NewInstance;
                }
            }
            return null;
        }

        internal static void Remove(string Name)
        {
            int PluginIndex = 0;
            for (int i=0;i<Collection.Count;i++)
            {
                if (Collection[i].Name.Equals(Name))
                {
                    PluginIndex = i;
                    break;
                }
            }
            Collection.RemoveAt(PluginIndex);
            if (DeactivatedPlugins.Contains(Name)) DeactivatedPlugins.Remove(Name);
        }

        internal static void Deactivate(string Name)
        {
            lock (DeactivatedPlugins)
            {
                if (!DeactivatedPlugins.Contains(Name)) DeactivatedPlugins.Add(Name);
            }
        }
        internal static void Activate(string Name)
        {
            lock (DeactivatedPlugins)
            {
                if (DeactivatedPlugins.Contains(Name)) DeactivatedPlugins.Remove(Name);
            }
        }
        internal static List<string> GetDeactivated()
        {
            List<string> Deactivated = new List<string>();
            Deactivated.AddRange(DeactivatedPlugins);
            return Deactivated;
        }
    }
}
