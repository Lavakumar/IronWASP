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
using System.Reflection;

namespace IronWASP
{
    public class Doc
    {
        public static string Help(object InObj)
        {
            Type InType = InObj.GetType();
            if (InType.Name.Equals("PythonType") || InType.Name.Equals("RubyClass"))
            {
                Assembly Ass = Assembly.GetExecutingAssembly();
                Type ATT = typeof(Request);
                Type AT = Ass.GetType(InObj.ToString());
                return AT.Name;
                //return InObj.ToString();
            }
            else
            {

            }
            return InType.Name;
        }
    }
}
