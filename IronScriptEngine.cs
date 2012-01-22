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
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Hosting;
using IronPython;
using IronPython.Hosting;
using IronPython.Modules;
using IronPython.Runtime;
using IronPython.Runtime.Exceptions;
using IronRuby;
using IronRuby.Hosting;
using IronRuby.Runtime;
using IronRuby.StandardLibrary;

namespace IronWASP
{
    class IronScriptEngine
    {
        internal ScriptEngine Engine;
        internal ScriptScope Scope;
        ShellResultStream OutputStream = new ShellResultStream();
        public IronScriptEngine(ScriptEngine Engine)
        {
            ScriptRuntime RunTime = Engine.Runtime;
            RunTime.IO.SetOutput(OutputStream, Encoding.UTF8);
            RunTime.IO.SetErrorOutput(OutputStream, Encoding.UTF8);
            ScriptScope Scope = Engine.CreateScope();
            Assembly MainAssembly = Assembly.GetExecutingAssembly();
            string RootDir = Directory.GetParent(MainAssembly.Location).FullName;
            RunTime.LoadAssembly(MainAssembly);
            RunTime.LoadAssembly(typeof(String).Assembly);
            RunTime.LoadAssembly(typeof(Uri).Assembly);
            this.Engine = Engine;
            this.Scope = Scope;
        }
    }
}
