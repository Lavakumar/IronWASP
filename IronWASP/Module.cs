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
using System.IO;
using System.Xml;
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
using System.Reflection;
using System.Windows.Forms;

namespace IronWASP
{
    public class Module
    {
        public string Name = "";
        //Read from Module XML
        internal string Version = "0.0";
        internal string DisplayName = "";
        internal string Category = "";
        internal string Author = "";
        internal string Description = "";
        internal string ProjectHome = "";
        internal string FileName = "";
        internal bool WorksOnSession = false;
        internal bool WorksOnFinding = false;
        internal bool WorksOnUrl = false;
        internal int ThreadId = 0;

        public const string FINDING = "Finding";
        public const string URL = "Url";
        public const string SESSION = "Session";
        public const string ALL = "All";

        public int MainThreadId
        {
            get
            {
                return this.ThreadId;
            }
        }

        internal static List<Module> Collection = new List<Module>();

        internal static List<Module> ModuleListFromXml = new List<Module>();

        static List<ModuleStartPromptForm> PromptWindows = new List<ModuleStartPromptForm>();
        static ScriptEngine Engine = null;

        internal static Queue<string> RecentOnSessionModules = new Queue<string>();

        public virtual void StartModule()
        {
            List<string> Messages = new List<string>();
            StringBuilder Msg = new StringBuilder("To run this module - ");

            foreach (Module M in ModuleListFromXml)
            {
                if (M.Name.Equals(this.Name))
                {
                    if (M.WorksOnSession)
                    {
                        Messages.Add("You must right-click on a log from the 'Logs' section and run this module on the selected log.");
                    }
                    if (M.WorksOnUrl)
                    {
                        Messages.Add("You must right-click on any section of the sitemap and run this module on selected url");
                    }
                    if (M.WorksOnFinding)
                    {
                        Messages.Add("You must right-click on any finding and run this module on selected finding");
                    }

                    break;
                }
            }

            Msg.AppendLine();
            for (int i = 0; i < Messages.Count; i++)
            {
                Msg.AppendLine(Messages[i]);
                if (i < Messages.Count - 1)
                {
                    Msg.AppendLine("or");
                }
            }

            MessageBox.Show(Msg.ToString());
        }

        public virtual void StartModuleOnSession(Session Sess)
        {

        }

        public virtual void StartModuleOnUrl(string Url)
        {

        }

        public virtual void StartModuleOnFinding(Finding Result)
        {

        }

        public void StopModule()
        {
            try
            {
                IronThread.Stop(this.ThreadId);
            }
            catch { }
        }

        public virtual Module GetInstance()
        {
            return new Module();
        }

        public static void Add(Module Mod)
        {
            if ((Mod.Name.Length > 0) && !(Mod.Name.Equals("All") || Mod.Name.Equals("None")))
            {
                if (!List().Contains(Mod.Name))
                {
                    foreach (Module M in ModuleListFromXml)
                    {
                        if (M.Name.Equals(Mod.Name))
                        {
                            CopyModuleProperties(M, Mod);
                            break;
                        }
                    }
                    lock (Collection)
                    {
                        Collection.Add(Mod);
                    }
                }
            }
        }

        public static List<string> List()
        {
            List<string> Names = new List<string>();
            foreach (Module Mod in Collection)
            {
                Names.Add(Mod.Name);
            }
            return Names;
        }

        public static List<string> ListAll()
        {
            List<string> Names = new List<string>();
            foreach (Module Mod in ModuleListFromXml)
            {
                Names.Add(Mod.Name);
            }
            return Names;
        }

        public static Module Get(string Name)
        {
            foreach (Module Mod in Collection)
            {
                if (Mod.Name.Equals(Name))
                {
                    Module NewInstance = Mod.GetInstance();
                    CopyModuleProperties(Mod, NewInstance);
                    return NewInstance;
                }
            }
            return null;
        }

        public static Module LoadAndGet(string Name)
        {
            Module M = Get(Name);
            if (M != null)
            {
                return M;
            }
            else
            {
                M = GetModuleReadFromXml(Name);
                if (M != null)
                {
                    return AskUserAndLoadModule(M);
                }
            }
            return null;
        }

        public static string GetVersion(string Name)
        {
            foreach (Module Mod in ModuleListFromXml)
            {
                if (Mod.Name.Equals(Name))
                {
                    return Mod.Version;
                }
            }
            return null;
        }

        internal static Module GetModuleReadFromXml(string Name)
        {
            foreach (Module Mod in ModuleListFromXml)
            {
                if (Mod.Name.Equals(Name))
                {
                    return Mod;
                }
            }
            return null;
        }

        internal static void ReloadModule(object NameObj)
        {
            ReloadModule(NameObj.ToString());
        }
        
        internal static void ReloadModule(string Name)
        {
            Remove(Name);
            LoadModule(Module.GetModuleReadFromXml(Name));
        }
        
        static void Remove(string Name)
        {
            int RemoveAt = -1;
            lock (Collection)
            {
                for (int i = 0; i < Collection.Count; i++)
                {
                    if (Collection[i].Name.Equals(Name))
                    {
                        RemoveAt = i;
                        break;
                    }
                }
                if (RemoveAt > -1)
                    Collection.RemoveAt(RemoveAt);
            }
        }

        static void CopyModuleProperties(Module From, Module To)
        {
            To.Category = From.Category;
            To.FileName = From.FileName;
            To.Version = From.Version;
            To.DisplayName = From.DisplayName;
            To.Author = From.Author;
            To.ProjectHome = From.ProjectHome;
            To.WorksOnSession = From.WorksOnSession;
            To.WorksOnUrl = From.WorksOnUrl;
            To.WorksOnFinding = From.WorksOnFinding;
            To.Description = From.Description;
        }

        internal static void ReadModulesXml()
        {
            List<Module> ReadModules = new List<Module>();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(string.Format("{0}\\ModulesDB.exe", Config.RootDir));
            XmlNodeList ModuleCategoryNodes = null;
            if (XmlDoc.ChildNodes.Count > 1)
            {
                ModuleCategoryNodes = XmlDoc.ChildNodes[1].ChildNodes;
            }
            else if (XmlDoc.ChildNodes.Count == 1)
            {
                ModuleCategoryNodes = XmlDoc.FirstChild.ChildNodes;
            }
            else
            {
                return;
            }
            foreach (XmlNode CategoryNode in ModuleCategoryNodes)
            {
                if (CategoryNode.FirstChild.Name.Equals("name"))
                {
                    string CategoryName = CategoryNode.FirstChild.InnerText;
                    foreach (XmlNode ModuleNode in CategoryNode.ChildNodes)
                    {
                        if (ModuleNode.Name.Equals("name")) continue;

                        Module M = ReadModuleXml(ModuleNode, CategoryName);
                        if (M != null)
                        {
                            try
                            {
                                bool IsDuplicate = false;
                                foreach (Module MM in ReadModules)
                                {
                                    if (MM.Name.Equals(M.Name)) IsDuplicate = true;
                                }
                                if (IsDuplicate) continue;
                                ReadModules.Add(M);
                            }
                            catch { }
                        }
                    }
                }
            }
            string ModulesDir = string.Format("{0}\\modules\\", Config.RootDir);

            try
            {
                string[] ModulesDirs = Directory.GetDirectories(ModulesDir);
                foreach (string ModuleDir in ModulesDirs)
                {
                    string ModulesXmlFile = string.Format("{0}\\Module.xml", ModuleDir);
                    if (File.Exists(ModulesXmlFile))
                    {
                        try
                        {
                            XmlDocument ModXmlDoc = new XmlDocument();
                            ModXmlDoc.Load(ModulesXmlFile);
                            Module M = ReadModuleXml(ModXmlDoc.FirstChild, "My Downloads");
                            if (M != null)
                            {
                                try
                                {
                                    bool IsDuplicate = false;
                                    foreach (Module MM in ReadModules)
                                    {
                                        if (MM.Name.Equals(M.Name)) IsDuplicate = true;
                                    }
                                    if (IsDuplicate) continue;
                                    ReadModules.Add(M);
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }

            lock (ModuleListFromXml)
            {
                ModuleListFromXml = new List<Module>(ReadModules);
            }
        }

        static Module ReadModuleXml(XmlNode ModuleNode, string CategoryName)
        {
            Module M = new Module();
            M.Category = CategoryName;
            foreach (XmlNode ModulePropertyNode in ModuleNode.ChildNodes)
            {
                switch (ModulePropertyNode.Name)
                {
                    case ("name"):
                        M.Name = ModulePropertyNode.InnerText;
                        break;
                    case ("version"):
                        M.Version = ModulePropertyNode.InnerText;
                        break;
                    case ("display_name"):
                        M.DisplayName = ModulePropertyNode.InnerText;
                        break;
                    case ("author"):
                        M.Author = ModulePropertyNode.InnerText;
                        break;
                    case ("project_home"):
                        M.ProjectHome = ModulePropertyNode.InnerText;
                        break;
                    case ("works_on_session"):
                        M.WorksOnSession = ModulePropertyNode.InnerText.Equals("yes");
                        break;
                    case ("works_on_url"):
                        M.WorksOnUrl = ModulePropertyNode.InnerText.Equals("yes");
                        break;
                    case ("works_on_finding"):
                        M.WorksOnFinding = ModulePropertyNode.InnerText.Equals("yes");
                        break;
                    case ("description"):
                        M.Description = ModulePropertyNode.InnerText;
                        break;
                }
            }
            try
            {
                string ModuleDir = string.Format("{0}\\modules\\{1}\\", Config.RootDir, M.Name);
                string[] Files = Directory.GetFiles(ModuleDir);
                for (int i = 0; i < Files.Length; i++)
                {
                    Files[i] = Path.GetFileName(Files[i]);
                }
                string PyName = string.Format("{0}.py", M.Name);
                string RbName = string.Format("{0}.rb", M.Name);
                string DllName = string.Format("{0}.dll", M.Name);
                string ModuleFileName = "";
                foreach (string FileName in Files)
                {
                    if (PyName.Equals(FileName) || RbName.Equals(FileName) || DllName.Equals(FileName))
                    {
                        ModuleFileName = FileName;
                        break;
                    }
                }
                if (ModuleFileName.Length > 0)
                {
                    M.FileName = ModuleFileName;
                    return M;
                }
            }
            catch { }
            return null;
        }

        internal static void StartModule(string ModuleDisplayName)
        {
            IronThread.RunSTAThread(StartModule, new List<object> { ModuleDisplayName});
        }
        static void StartModule(object ModuleDisplayNameObj)
        {
            List<object> Args = (List<object>)ModuleDisplayNameObj;
            string ModuleDisplayName = Args[0].ToString();
            Module M = GetModuleFromDisplayName(ModuleDisplayName, ALL);
            if (M != null)
            {
                M.ThreadId = IronThread.ThreadId;
                M.StartModule();
            }
        }
        internal static void StartModuleOnSession(string ModuleDisplayName, string Source, int LogId)
        {
            IronThread.RunSTAThread(StartModuleOnSession, new List<object> { ModuleDisplayName, Source, LogId });
        }
        static void StartModuleOnSession(object ModuleDisplayNameSourceLogId)
        {
            List<object> Args = (List<object>)ModuleDisplayNameSourceLogId;
            string ModuleDisplayName = Args[0].ToString();
            string Source = Args[1].ToString();
            int LogId = (int)Args[2];
            Session Sess = null;
            switch (Source)
            {
                case(RequestSource.Proxy):
                    Sess = Session.FromProxyLog(LogId);
                    break;
                case (RequestSource.Probe):
                    Sess = Session.FromProbeLog(LogId);
                    break;
                case (RequestSource.Scan):
                    Sess = Session.FromScanLog(LogId);
                    break;
                case (RequestSource.Shell):
                    Sess = Session.FromShellLog(LogId);
                    break;
                case (RequestSource.Test):
                case (RequestSource.TestGroup):
                    Sess = Session.FromTestLog(LogId);
                    break;
                default:
                    Sess = Session.FromLog(LogId, Source);
                    break;
            }
            if (Sess != null)
            {
                Module M = GetModuleFromDisplayName(ModuleDisplayName, SESSION);
                if (M != null)
                {
                    M.ThreadId = IronThread.ThreadId;
                    M.StartModuleOnSession(Sess);
                }
            }
        }
        internal static void StartModuleOnFinding(string ModuleDisplayName, int FindingId)
        {
            IronThread.RunSTAThread(StartModuleOnFinding, new List<object> { ModuleDisplayName, FindingId });
        }
        static void StartModuleOnFinding(object ModuleDisplayNameFindingId)
        {
            List<object> Args = (List<object>)ModuleDisplayNameFindingId;
            string ModuleDisplayName = Args[0].ToString();
            int FindingId = (int)Args[1];
            if (FindingId == -1) return;
            Finding PR = IronDB.GetPluginResultFromDB(FindingId);
            Module M = GetModuleFromDisplayName(ModuleDisplayName, FINDING);
            if (M != null)
            {
                M.ThreadId = IronThread.ThreadId;
                M.StartModuleOnFinding(PR);
            }
        }
        internal static void StartModuleOnUrl(string ModuleDisplayName, string Url)
        {
            IronThread.RunSTAThread(StartModuleOnUrl, new List<object> { ModuleDisplayName, Url });
        }
        static void StartModuleOnUrl(object ModuleDisplayNameUrl)
        {
            List<object> Args = (List<object>)ModuleDisplayNameUrl;
            string ModuleDisplayName = Args[0].ToString();
            string Url = Args[1].ToString();
            try
            {
                new Request(Url);
            }
            catch { return; }
            Module M = GetModuleFromDisplayName(ModuleDisplayName, URL);
            if (M != null)
            {
                M.ThreadId = IronThread.ThreadId;
                M.StartModuleOnUrl(Url);
            }
        }

        internal static bool DoesDisplayNameExist(string DisplayName)
        {
            foreach (Module Mod in ModuleListFromXml)
            {
                if (Mod.DisplayName.Equals(DisplayName))
                {
                    return true;
                }
            }
            return false;
        }

        static Module GetModuleFromDisplayName(string ModuleDisplayName, string WorksOn)
        {
            Module M = null;
            foreach (Module Mod in ModuleListFromXml)
            {
                if (Mod.DisplayName.Equals(ModuleDisplayName))
                {
                    switch(WorksOn)
                    {
                        case(ALL):
                            M = Mod;
                            break;
                        case (URL):
                            if (Mod.WorksOnUrl) M = Mod;
                            break;
                        case (SESSION):
                            if (Mod.WorksOnSession) M = Mod;
                            break;
                        case (FINDING):
                            if (Mod.WorksOnFinding) M = Mod;
                            break;
                    }
                }
            }
            if (M == null) return null;
            foreach (Module Mod in Collection)
            {
                if (Mod.Name.Equals(M.Name))
                {
                    return Mod;
                }
            }
            Module NewlyLoaded = AskUserAndLoadModule(M);
            return NewlyLoaded;
        }

        static Module AskUserAndLoadModule(Module M)
        {
            ModuleStartPromptForm F = null;
            foreach (ModuleStartPromptForm PF in PromptWindows)
            {
                if (M.Name.Equals(PF.DisplayedModule.Name))
                    F = PF;
            }
            if (F == null)
            {
                F = new ModuleStartPromptForm();
                lock (PromptWindows)
                {
                    PromptWindows.Add(F);
                }
                F.SetModule(M);
                F.ShowDialog();
            }
            else
            {
                F.Activate();
                return null;
            }
            if (F.ModuleAuthroized)
            {
                return Module.Get(M.Name);
            }
            else
            {
                return null;
            }
        }
        internal static void LoadModule(object ModObj)
        {
            LoadModule((ModObj as Module));
        }

        internal static Module LoadModule(Module M)
        {            
            try
            {
                string FullName = string.Format("{0}\\modules\\{1}\\{2}", Config.RootDir, M.Name, M.FileName);
                if (M.FileName.EndsWith(".dll"))
                {
                    Assembly MA = System.Reflection.Assembly.LoadFile(FullName);
                    Module NewModule = (Module) Activator.CreateInstance(MA.GetTypes()[0]);
                    Module.Add(NewModule.GetInstance());
                }
                else
                {
                    Engine = PluginEngine.GetScriptEngine();

                    if (M.FileName.EndsWith(".py"))
                        Engine.Runtime.TryGetEngine("py", out Engine);
                    else
                        Engine.Runtime.TryGetEngine("rb", out Engine);
                    List<string> ModulePaths = new List<string>();
                    foreach (Module ModuleFromXml in ModuleListFromXml)
                    {
                        ModulePaths.Add(string.Format("{0}\\modules\\{1}\\", Config.RootDir, ModuleFromXml.Name));
                    }
                    Engine.SetSearchPaths(ModulePaths);
                    if (M.FileName.Length == 0)
                        throw new Exception("Module is missing script files");
                    ScriptSource ModuleSource = Engine.CreateScriptSourceFromFile(FullName);
                    CompiledCode CompiledModule = ModuleSource.Compile();
                    ModuleSource.ExecuteProgram();
                }
            }
            catch(Exception Exp)
            {
                 ModuleStartPromptForm PF = GetPromptWindow(M);
                 if (PF != null) PF.ShowError("Error Loading Module.");
                 IronException.Report(string.Format("Error Loading Module - {0}", M.Name), Exp);
                 return null;
            }
            IronUI.BuildPluginTree();
            ModuleStartPromptForm UsedPF = RemovePromptWindowFromList(M);
            if (UsedPF != null)
            {
                try
                {
                    if (!UsedPF.IsClosed)
                        UsedPF.CloseForm();
                }
                catch { }
            }
            return Get(M.Name);
        }
        static ModuleStartPromptForm GetPromptWindow(Module M)
        {
            for (int i = 0; i < PromptWindows.Count; i++)
            {
                if (M.Name.Equals(PromptWindows[i].DisplayedModule.Name))
                {
                    return PromptWindows[i];
                }
            }
            return null;
        }
        internal static ModuleStartPromptForm RemovePromptWindowFromList(Module M)
        {
            ModuleStartPromptForm PF = null;
            lock (PromptWindows)
            {
                int RemoveAt = -1;
                for (int i = 0; i < PromptWindows.Count; i++)
                {
                    if (M.Name.Equals(PromptWindows[i].DisplayedModule.Name))
                    {
                        RemoveAt = i;
                        PF = PromptWindows[i];
                        break;
                    }
                }
                if (RemoveAt > -1)
                {
                    PromptWindows.RemoveAt(RemoveAt);
                }
            }
            return PF;
        }
    }
}
