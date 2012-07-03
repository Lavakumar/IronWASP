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
using System.Xml;
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
    internal class PluginStore
    {
        internal static bool StartUp = false;
        internal static string FileName
        {
            get
            {
                return fileName;
            }
        }

        static string fileName = "";

        internal static void InitialiseAllPlugins()
        {
            LoadAllPlugins();
        }

        static ScriptEngine GetScriptEngine()
        {
            ScriptRuntimeSetup Setup = new ScriptRuntimeSetup();
            Setup.LanguageSetups.Add(IronRuby.Ruby.CreateRubySetup());
            Setup.LanguageSetups.Add(IronPython.Hosting.Python.CreateLanguageSetup(null));
            ScriptRuntime RunTime = new ScriptRuntime(Setup);
            ScriptEngine Engine = RunTime.GetEngine("py");
            ScriptScope Scope = RunTime.CreateScope();

            Assembly MainAssembly = Assembly.GetExecutingAssembly();
            string RootDir = Directory.GetParent(MainAssembly.Location).FullName;

            RunTime.LoadAssembly(MainAssembly);
            RunTime.LoadAssembly(typeof(String).Assembly);
            RunTime.LoadAssembly(typeof(Uri).Assembly);
            RunTime.LoadAssembly(typeof(XmlDocument).Assembly);

            Engine.Runtime.TryGetEngine("py", out Engine);            
            return Engine;
        }

        internal static void LoadAllPlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadAllPassivePlugins(Engine);
            LoadAllActivePlugins(Engine);
            LoadAllFormatPlugins(Engine);
            LoadAllSessionPlugins(Engine);
            IronUI.UpdateAllFormatPluginRows();
            IronUI.UpdateAllActivePluginRows();
            IronUI.UpdateSessionPluginsInASTab();
            IronUI.BuildPluginTree();
        }

        internal static void LoadAllNewPlugins()
        {
            LoadNewPassivePlugins();
            LoadNewActivePlugins();
            LoadNewFormatPlugins();
            LoadNewSessionPlugins();
            IronUI.UpdateAllFormatPluginRows();
            IronUI.UpdateAllActivePluginRows();
            IronUI.UpdateSessionPluginsInASTab();
            IronUI.BuildPluginTree();
        }

        internal static void LoadAllPassivePlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadAllPassivePlugins(Engine);
            IronUI.BuildPluginTree();
        }

        internal static void LoadAllPassivePlugins(ScriptEngine Engine)
        {
            string PassivePluginPath = Path.Combine(Config.RootDir, "plugins\\passive");

            lock (PassivePlugin.Collection)
            {
                PassivePlugin.Collection.Clear();
                PassivePlugin.DeactivatedPlugins.Clear();
                string[] PassivePluginFiles = Directory.GetFiles(PassivePluginPath);
                foreach (string PassivePluginFile in PassivePluginFiles)
                {
                    LoadPlugin(PassivePluginFile, Engine);
                }
            }
        }

        internal static void LoadNewPassivePlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadNewPassivePlugins(Engine);
            IronUI.BuildPluginTree();
        }

        internal static void LoadNewPassivePlugins(ScriptEngine Engine)
        {
            string PassivePluginPath = Path.Combine(Config.RootDir, "plugins\\passive");
            string[] PassivePluginFiles = Directory.GetFiles(PassivePluginPath);
            List<string> OldPluginFiles = new List<string>();
            List<string> NewPluginFiles = new List<string>();
            foreach (string Name in PassivePlugin.List())
            {
                OldPluginFiles.Add((Config.RootDir + "\\plugins\\passive\\" + PassivePlugin.Get(Name).FileName).Replace("/","\\"));
            }
            foreach (string PassivePluginFile in PassivePluginFiles)
            {
                if (!OldPluginFiles.Contains(PassivePluginFile))
                {
                    NewPluginFiles.Add(PassivePluginFile);
                }
            }
            LoadPassivePlugins(Engine, NewPluginFiles);
        }

        internal static void LoadPassivePlugins(ScriptEngine Engine, List<string> FileNames)
        {
            lock (PassivePlugin.Collection)
            {
                foreach (string PassivePluginFile in FileNames)
                {
                    LoadPlugin(PassivePluginFile, Engine);
                }
            }
        }

        internal static void LoadAllActivePlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadAllActivePlugins(Engine);
            IronUI.UpdateAllActivePluginRows();
            IronUI.BuildPluginTree();
        }

        internal static void LoadAllActivePlugins(ScriptEngine Engine)
        {
            string ActivePluginPath = Path.Combine(Config.RootDir, "plugins\\active");
            
            lock (ActivePlugin.Collection)
            {
                ActivePlugin.Collection.Clear();
                string[] ActivePluginFiles = Directory.GetFiles(ActivePluginPath);
                foreach (string ActivePluginFile in ActivePluginFiles)
                {
                    LoadPlugin(ActivePluginFile, Engine);
                }
            }
        }

        internal static void LoadNewActivePlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadNewActivePlugins(Engine);
            IronUI.UpdateAllActivePluginRows();
            IronUI.BuildPluginTree();
        }

        internal static void LoadNewActivePlugins(ScriptEngine Engine)
        {
            string ActivePluginPath = Path.Combine(Config.RootDir, "plugins\\active");
            string[] ActivePluginFiles = Directory.GetFiles(ActivePluginPath);
            List<string> OldPluginFiles = new List<string>();
            List<string> NewPluginFiles = new List<string>();
            foreach (string Name in ActivePlugin.List())
            {
                OldPluginFiles.Add((Config.RootDir + "\\plugins\\active\\" + ActivePlugin.Get(Name).FileName).Replace("/", "\\"));
            }
            foreach (string PluginFile in ActivePluginFiles)
            {
                if (!OldPluginFiles.Contains(PluginFile))
                {
                    NewPluginFiles.Add(PluginFile);
                }
            }
            LoadActivePlugins(Engine, NewPluginFiles);
        }

        internal static void LoadActivePlugins(ScriptEngine Engine, List<string> FileNames)
        {
            lock (ActivePlugin.Collection)
            {
                foreach (string ActivePluginFile in FileNames)
                {
                    LoadPlugin(ActivePluginFile, Engine);
                }
            }
        }

        internal static void LoadAllFormatPlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadAllFormatPlugins(Engine);
            IronUI.UpdateAllFormatPluginRows();
            IronUI.BuildPluginTree();
        }

        internal static void LoadAllFormatPlugins(ScriptEngine Engine)
        {
            string FormatPluginPath = Path.Combine(Config.RootDir, "plugins\\format");

            lock (FormatPlugin.Collection)
            {
                FormatPlugin.Collection.Clear();
                string[] FormatPluginFiles = Directory.GetFiles(FormatPluginPath);
                foreach (string FormatPluginFile in FormatPluginFiles)
                {
                    LoadPlugin(FormatPluginFile, Engine);
                }
            }
        }

        internal static void LoadNewFormatPlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadNewFormatPlugins(Engine);
            IronUI.UpdateAllFormatPluginRows();
            IronUI.BuildPluginTree();
        }

        internal static void LoadNewFormatPlugins(ScriptEngine Engine)
        {
            string FormatPluginPath = Path.Combine(Config.RootDir, "plugins\\format");
            string[] FormatPluginFiles = Directory.GetFiles(FormatPluginPath);
            List<string> OldPluginFiles = new List<string>();
            List<string> NewPluginFiles = new List<string>();
            foreach (string Name in FormatPlugin.List())
            {
                OldPluginFiles.Add((Config.RootDir + "\\plugins\\format\\" + FormatPlugin.Get(Name).FileName).Replace("/", "\\"));
            }
            foreach (string PluginFile in FormatPluginFiles)
            {
                if (!OldPluginFiles.Contains(PluginFile))
                {
                    NewPluginFiles.Add(PluginFile);
                }
            }
            LoadFormatPlugins(Engine, NewPluginFiles);
        }

        internal static void LoadFormatPlugins(ScriptEngine Engine, List<string> FileNames)
        {
            lock (FormatPlugin.Collection)
            {
                foreach (string FormatPluginFile in FileNames)
                {
                    LoadPlugin(FormatPluginFile, Engine);
                }
            }
        }

        internal static void LoadAllSessionPlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadAllSessionPlugins(Engine);
            IronUI.UpdateSessionPluginsInASTab();
            IronUI.BuildPluginTree();
        }

        internal static void LoadAllSessionPlugins(ScriptEngine Engine)
        {
            string SessionPluginPath = Path.Combine(Config.RootDir, "plugins\\session");

            lock (SessionPlugin.Collection)
            {
                SessionPlugin.Collection.Clear();
                string[] SessionPluginFiles = Directory.GetFiles(SessionPluginPath);
                foreach (string SessionPluginFile in SessionPluginFiles)
                {
                    LoadPlugin(SessionPluginFile, Engine);
                }
            }
        }

        internal static void LoadNewSessionPlugins()
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadNewSessionPlugins(Engine);
            IronUI.UpdateSessionPluginsInASTab();
            IronUI.BuildPluginTree();
        }

        internal static void LoadNewSessionPlugins(ScriptEngine Engine)
        {
            string SessionPluginPath = Path.Combine(Config.RootDir, "plugins\\session");
            string[] SessionPluginFiles = Directory.GetFiles(SessionPluginPath);
            List<string> OldPluginFiles = new List<string>();
            List<string> NewPluginFiles = new List<string>();
            foreach (string Name in SessionPlugin.List())
            {
                OldPluginFiles.Add((Config.RootDir + "\\plugins\\session\\" + SessionPlugin.Get(Name).FileName).Replace("/", "\\"));
            }
            foreach (string PluginFile in SessionPluginFiles)
            {
                if (!OldPluginFiles.Contains(PluginFile))
                {
                    NewPluginFiles.Add(PluginFile);
                }
            }
            LoadSessionPlugins(Engine, NewPluginFiles);
        }

        internal static void LoadSessionPlugins(ScriptEngine Engine, List<string> FileNames)
        {
            lock (SessionPlugin.Collection)
            {
                foreach (string SessionPluginFile in FileNames)
                {
                    LoadPlugin(SessionPluginFile, Engine);
                }
            }
        }

        static void LoadPlugin(string PluginFile)
        {
            ScriptEngine Engine = GetScriptEngine();
            LoadPlugin(PluginFile, Engine);
        }

        static void LoadPlugin(string PluginFile, ScriptEngine Engine)
        {
            try
            {
                ScriptSource PluginSource;
                CompiledCode CompiledPlugin;

                fileName = PluginFile.Substring(PluginFile.LastIndexOf('\\') + 1);
                if(StartUp) IronUI.ShowLoadMessage("Loading Plugin - " + fileName);
                if (PluginFile.EndsWith(".py", StringComparison.CurrentCultureIgnoreCase))
                {
                    Engine.Runtime.TryGetEngine("py", out Engine);
                    PluginSource = Engine.CreateScriptSourceFromFile(PluginFile);
                    CompiledPlugin = PluginSource.Compile();
                    PluginSource.ExecuteProgram();
                }
                else if (PluginFile.EndsWith(".rb", StringComparison.CurrentCultureIgnoreCase))
                {
                    Engine.Runtime.TryGetEngine("rb", out Engine);
                    PluginSource = Engine.CreateScriptSourceFromFile(PluginFile);
                    CompiledPlugin = PluginSource.Compile();
                    PluginSource.ExecuteProgram();
                }
            }
            catch (Exception Exp)
            {
                IronException.Report("Error loading plugin - " + PluginFile, Exp.Message, Exp.StackTrace);
            }
            finally
            {
                fileName = "";
            }
        }

        internal static void ReloadPlugin(PluginType Type, string Name, string PluginFileName)
        {
            string FileName = "";
            switch (Type)
            {
                case PluginType.Passive:
                    FileName = Config.RootDir + "\\plugins\\passive\\" + PluginFileName;
                    lock (PassivePlugin.Collection)
                    {
                        PassivePlugin.Remove(Name);
                        LoadPlugin(FileName);
                    }
                    break;
                case PluginType.Active:
                    FileName = Config.RootDir + "\\plugins\\active\\" + PluginFileName;
                    lock (ActivePlugin.Collection)
                    {
                        ActivePlugin.Remove(Name);
                        LoadPlugin(FileName);
                    }
                    break;
                case PluginType.Format:
                    FileName = Config.RootDir + "\\plugins\\format\\" + PluginFileName;
                    lock (FormatPlugin.Collection)
                    {
                        FormatPlugin.Remove(Name);
                        LoadPlugin(FileName);
                        IronUI.UpdateAllFormatPluginRows();
                    }
                    break;
                case PluginType.Session:
                    FileName = Config.RootDir + "\\plugins\\session\\" + PluginFileName;
                    lock (SessionPlugin.Collection)
                    {
                        SessionPlugin.Remove(Name);
                        LoadPlugin(FileName);
                    }
                    break;
            }
            IronUI.UpdateAllFormatPluginRows();
            IronUI.UpdateAllActivePluginRows();
            IronUI.BuildPluginTree();
        }

        public static PluginResults RunPassivePlugin(PassivePlugin P, Session Irse)
        {
            PluginResults Results = new PluginResults();
            P.Check(Irse, Results);
            foreach (PluginResult PR in Results.GetAll())
            {
                PR.Plugin = P.Name;
                PR.Report();
            }
            return Results;
        }

        public static void RunAllPassivePluginsBeforeRequestInterception(Session IrSe)
        {
            foreach (string Name in PassivePlugin.List())
            {
                PassivePlugin P = PassivePlugin.Get(Name);
                if ((P.WorksOn == PluginWorksOn.Request) || (P.WorksOn == PluginWorksOn.Both))
                {
                    if ((P.CallingState == PluginCallingState.BeforeInterception) || (P.CallingState == PluginCallingState.Both))
                    {
                        try
                        {
                            PluginStore.RunPassivePlugin(P, IrSe);
                        }
                        catch (Exception Exp)
                        {
                            IronException.Report("Error executing 'BeforeRequestInterception' Passive Plugin - " + Name, Exp.Message, Exp.StackTrace);
                        }
                    }
                }
            }
        }

        public static void RunAllPassivePluginsAfterRequestInterception(Session IrSe)
        {
            foreach (string Name in PassivePlugin.List())
            {
                PassivePlugin P = PassivePlugin.Get(Name);
                if ((P.WorksOn == PluginWorksOn.Request) || (P.WorksOn == PluginWorksOn.Both))
                {
                    if ((P.CallingState == PluginCallingState.AfterInterception) || (P.CallingState == PluginCallingState.Both))
                    {
                        try
                        {
                            PluginStore.RunPassivePlugin(P, IrSe);
                        }
                        catch(Exception Exp)
                        {
                            IronException.Report("Error executing 'AfterRequestInterception' Passive Plugin - " + Name, Exp.Message, Exp.StackTrace);
                        }
                    }
                }
            }
        }

        public static void RunAllRequestBasedOfflinePassivePlugins(Session IrSe)
        {
            foreach (string Name in PassivePlugin.List())
            {
                PassivePlugin P = PassivePlugin.Get(Name);
                if ((P.WorksOn == PluginWorksOn.Request) || (P.WorksOn == PluginWorksOn.Both))
                {
                    if ((P.CallingState == PluginCallingState.Offline))
                    {
                        try
                        {
                            PluginStore.RunPassivePlugin(P, IrSe);
                        }
                        catch (Exception Exp)
                        {
                            IronException.Report("Error executing 'Offline' Passive Plugin - " + Name, Exp.Message, Exp.StackTrace);
                        }
                    }
                }
            }
        }

        public static void RunAllPassivePluginsBeforeResponseInterception(Session IrSe)
        {
            foreach (string Name in PassivePlugin.List())
            {
                PassivePlugin P = PassivePlugin.Get(Name); 
                if ((P.WorksOn == PluginWorksOn.Response) || (P.WorksOn == PluginWorksOn.Both))
                {
                    if ((P.CallingState == PluginCallingState.BeforeInterception) || (P.CallingState == PluginCallingState.AfterInterception))
                    {
                        try
                        {
                            PluginStore.RunPassivePlugin(P, IrSe);
                        }
                        catch(Exception Exp)
                        {
                            IronException.Report("Error executing 'BeforeResponseInterception' Passive Plugin : " + Name, Exp.Message, Exp.StackTrace);
                        }
                    }
                }
            }
        }
        public static void RunAllPassivePluginsAfterResponseInterception(Session IrSe)
        {
            foreach (string Name in PassivePlugin.List())
            {
                PassivePlugin P = PassivePlugin.Get(Name);
                if ((P.WorksOn == PluginWorksOn.Response) || (P.WorksOn == PluginWorksOn.Both))
                {
                    if ((P.CallingState == PluginCallingState.AfterInterception) || (P.CallingState == PluginCallingState.Both))
                    {
                        try
                        {
                            PluginStore.RunPassivePlugin(P, IrSe);
                        }
                        catch(Exception Exp)
                        {
                            IronException.Report("Error executing 'AfterResponseInterception' Passive Plugin : " + Name, Exp.Message, Exp.StackTrace);
                        }
                    }
                }
            }
        }

        public static void RunAllResponseBasedOfflinePassivePlugins(Session IrSe)
        {
            foreach (string Name in PassivePlugin.List())
            {
                PassivePlugin P = PassivePlugin.Get(Name);
                if ((P.WorksOn == PluginWorksOn.Response) || (P.WorksOn == PluginWorksOn.Both))
                {
                    if (P.CallingState == PluginCallingState.Offline)
                    {
                        try
                        {
                            PluginStore.RunPassivePlugin(P, IrSe);
                        }
                        catch (Exception Exp)
                        {
                            IronException.Report("Error executing 'Offline' Passive Plugin : " + Name, Exp.Message, Exp.StackTrace);
                        }
                    }
                }
            }
        }
    }
}
