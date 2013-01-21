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
    public class ActivePlugin : Plugin
    {
        internal static List<ActivePlugin> Collection = new List<ActivePlugin>();

        public virtual void Check(Scanner Scan)
        {

        }

        public virtual ActivePlugin GetInstance()
        {
            return new ActivePlugin();
        }

        public static void Add(ActivePlugin AP)
        {
            if ((AP.Name.Length > 0) && !(AP.Name.Equals("All") || AP.Name.Equals("None")))
            {
                if (!List().Contains(AP.Name))
                {
                    AP.FileName = PluginStore.FileName;
                    Collection.Add(AP);
                }
            }
        }

        public static List<string> List()
        {
            List<string> Names = new List<string>();
            foreach (ActivePlugin AP in Collection)
            {
                Names.Add(AP.Name);
            }
            return Names;
        }

        public static ActivePlugin Get(string Name)
        {
            foreach (ActivePlugin AP in Collection)
            {
                if (AP.Name.Equals(Name))
                {
                    ActivePlugin NewInstance = AP.GetInstance();
                    NewInstance.FileName = AP.FileName;
                    return NewInstance;
                }
            }
            return null;
        }

        internal static void Remove(string Name)
        {
            int PluginIndex = 0;
            for (int i = 0; i < Collection.Count; i++)
            {
                if (Collection[i].Name.Equals(Name))
                {
                    PluginIndex = i;
                    break;
                }
            }
            Collection.RemoveAt(PluginIndex);
        }
    }
}
