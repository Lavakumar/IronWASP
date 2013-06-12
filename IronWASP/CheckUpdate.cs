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
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using Ionic.Zip;

namespace IronWASP
{
    class CheckUpdate
    {
        static string CurrentVersion = "0.9.6.0";

        //static string PluginManifestUrl = "https://ironwasp.org/update/plugin.manifest";
        //static string IronWASPManifestUrl = "https://ironwasp.org/update/ironwasp.manifest";

        static string PluginManifestUrl = "https://ironwasp.org/update/plugin_manifest.xml";
        static string ModuleManifestUrl = "https://ironwasp.org/update/module_manifest.xml";
        static string IronWASPManifestUrl = "https://ironwasp.org/update/ironwasp_manifest.xml";
        static string ModulesDbUrl = "https://ironwasp.org/update/modules_db.xml";

        static string ModuleDownloadBaseUrl = "https://ironwasp.org/update/modules/";
        static string PluginDownloadBaseUrl = "https://ironwasp.org/update/plugins/";
        static string IronWASPDownloadBaseUrl = "https://ironwasp.org/update/ironwasp/";

        static string ModuleManifestFile = "";
        static string PluginManifestFile = "";
        static string IronWASPManifestFile = "";
        static string ModulesDbFile = "";


        static List<string[]> PluginManifestInfo = new List<string[]>();
        static List<string[]> IronWASPManifestInfo = new List<string[]>();

        static Thread T;

        static bool NewUpdateAvailable = false;

        internal static void CheckForUpdates()
        {
            T = new Thread(StartCheck);
            T.Start();
        }

        static void StartCheck()
        {
            try
            {
                Request IronWASPManifestReq = new Request(IronWASPManifestUrl);
                IronWASPManifestReq.Source = RequestSource.Stealth;
                IronWASPManifestReq.Headers.Set("User-Agent", "IronWASP v" + CurrentVersion);
                Response IronWASPManifestRes = IronWASPManifestReq.Send();
                if (!IronWASPManifestRes.IsSslValid)
                {
                    throw new Exception("Invalid SSL Certificate provided by the server");
                }
                IronWASPManifestFile = IronWASPManifestRes.BodyString;

                Request ModulesDbReq = new Request(ModulesDbUrl);
                ModulesDbReq.Source = RequestSource.Stealth;
                ModulesDbReq.Headers.Set("User-Agent", "IronWASP v" + CurrentVersion);
                Response ModulesDbRes = ModulesDbReq.Send();
                if (!ModulesDbRes.IsSslValid)
                {
                    throw new Exception("Invalid SSL Certificate provided by the server");
                }
                //The ASCII conversion and substring is done to remove the unicode characters introduced at the beginning of the xml. Must find out why this happens.
                ModulesDbFile = Encoding.ASCII.GetString(ModulesDbRes.BodyArray);
                ModulesDbFile = ModulesDbFile.Substring(ModulesDbFile.IndexOf('<'));

                Request PluginManifestReq = new Request(PluginManifestUrl);
                PluginManifestReq.Source = RequestSource.Stealth;
                PluginManifestReq.Headers.Set("User-Agent","IronWASP v" + CurrentVersion);
                Response PluginManifestRes = PluginManifestReq.Send();
                if (!PluginManifestRes.IsSslValid)
                {
                    throw new Exception("Invalid SSL Certificate provided by the server");
                }
                PluginManifestFile = PluginManifestRes.BodyString;

                Request ModuleManifestReq = new Request(ModuleManifestUrl);
                ModuleManifestReq.Source = RequestSource.Stealth;
                ModuleManifestReq.Headers.Set("User-Agent", "IronWASP v" + CurrentVersion);
                Response ModuleManifestRes = ModuleManifestReq.Send();
                if (!ModuleManifestRes.IsSslValid)
                {
                    throw new Exception("Invalid SSL Certificate provided by the server");
                }
                ModuleManifestFile = ModuleManifestRes.BodyString;
                
                SetUpUpdateDirs();
                GetNewIronWASP();
                GetNewModulesDb();
                GetNewPlugins();
                GetNewModules();
                if (NewUpdateAvailable)
                {
                    try
                    {
                        Tools.Run(string.Format("{0}\\Updater.exe", Config.RootDir));
                    }
                    catch (Exception Exp) { IronException.Report("Unable to Open IronWASP Updater", Exp); }
                }
            }
            catch(ThreadAbortException) { }
            catch(Exception Exp)
            {
                IronException.Report("Software Update Failed", Exp);
            }
        }

        static void SetUpUpdateDirs()
        {
            try
            {
                Directory.Delete(Config.Path + "\\updates", true);
            }
            catch { }
            try
            {
                Directory.CreateDirectory(Config.Path + "\\updates");
                Directory.CreateDirectory(Config.Path + "\\updates\\plugins");
                Directory.CreateDirectory(Config.Path + "\\updates\\plugins\\active");
                Directory.CreateDirectory(Config.Path + "\\updates\\plugins\\passive");
                Directory.CreateDirectory(Config.Path + "\\updates\\plugins\\format");
                Directory.CreateDirectory(Config.Path + "\\updates\\plugins\\session");
                Directory.CreateDirectory(Config.Path + "\\updates\\modules");
                Directory.CreateDirectory(Config.Path + "\\updates\\ironwasp");
            }
            catch
            {
                throw new Exception("Unable to create Update directories, update failed");
            }
        }

        static void GetNewModules()
        {
            List<string[]> ModulesInfo = new List<string[]>();
            foreach (string Name in Module.ListAll())
            {
                 string MVersion = Module.GetVersion(Name);
                 ModulesInfo.Add(new string[]{Name, MVersion});
            }
            StringBuilder SB = new StringBuilder();
            XmlWriter XW = XmlWriter.Create(SB);

            XW.WriteStartDocument();
            XW.WriteStartElement("manifest");

            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                MemoryStream MS = new MemoryStream(Encoding.UTF8.GetBytes(ModuleManifestFile));
                XmlDoc.Load(MS);
                MS.Close();
            }
            catch { throw new Exception("Invalid IronWASP update manifest file recieved."); }

            XmlNodeList FileNodes = null;

            if (XmlDoc.ChildNodes.Count == 1)
            {
                FileNodes = XmlDoc.FirstChild.ChildNodes;
            }
            else if (XmlDoc.ChildNodes.Count == 2)
            {
                FileNodes = XmlDoc.ChildNodes[1].ChildNodes;
            }

            foreach (XmlNode FileNode in FileNodes)
            {
                string Version = "";
                string Action = "";
                string ModuleName = "";
                string DownloadFileName = "";
                string Comment = "";

                foreach (XmlNode PropertyNode in FileNode.ChildNodes)
                {
                    switch (PropertyNode.Name)
                    {
                        case ("version"):
                            Version = PropertyNode.InnerText;
                            break;
                        case ("action"):
                            Action = PropertyNode.InnerText;
                            break;
                        case ("modulename"):
                            ModuleName = PropertyNode.InnerText;
                            break;
                        case ("downloadname"):
                            DownloadFileName = PropertyNode.InnerText;
                            break;
                        case ("comment"):
                            Comment = PropertyNode.InnerText;
                            break;
                    }
                }

                if (Action.Equals("add") || Action.Equals("update"))
                {
                    bool MatchFound = false;
                    string[] MatchedModuleInfo = new string[2];
                    foreach (string[] ModuleInfo in ModulesInfo)
                    {
                        if (ModuleInfo[0].Equals(ModuleName.Replace(".zip", "")))
                        {
                            MatchFound = true;
                            MatchedModuleInfo = ModuleInfo;
                            break;
                        }
                    }

                    if ((MatchFound && !MatchedModuleInfo[1].Equals(Version)) || !MatchFound)
                    {
                        DownloadModule(ModuleName, DownloadFileName);
                        XW.WriteStartElement("file");
                        XW.WriteStartElement("action"); XW.WriteValue(Action); XW.WriteEndElement();
                        XW.WriteStartElement("modulename"); XW.WriteValue(ModuleName); XW.WriteEndElement();
                        XW.WriteStartElement("comment"); XW.WriteValue(Comment); XW.WriteEndElement();
                        XW.WriteEndElement();
                    }
                }
            }

            XW.WriteEndElement();
            XW.WriteEndDocument();
            XW.Close();

            StreamWriter SW = File.CreateText(Config.Path + "\\updates\\module_manifest.xml");
            SW.Write(SB.ToString());
            SW.Close();

            //string[] IronWASPManifestLines = IronWASPManifestFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            //foreach (string Line in IronWASPManifestLines)
            //{
            //    string[] LineParts = Line.Split(new char[] { '|' }, 5);
            //    if (LineParts.Length != 5)
            //    {
            //        throw new Exception("Invalid 'IronWASP Manifest File' recieved from server");
            //    }
            //    IronWASPManifestInfo.Add(LineParts);
            //}
            //foreach (string[] IronWASPManifestInfoLine in IronWASPManifestInfo)
            //{
            //    if (IronWASPManifestInfoLine[0].Equals("+") || IronWASPManifestInfoLine[0].Equals("*"))
            //    {
            //        if (IsGreaterVersion(IronWASPManifestInfoLine[1]))
            //        {
            //            DownloadIronWASPFile(IronWASPManifestInfoLine[2], IronWASPManifestInfoLine[3]);
            //        }
            //    }
            //}
        }

        static void GetNewPlugins()
        {
            StringBuilder SB = new StringBuilder();
            XmlWriter XW = XmlWriter.Create(SB);

            XW.WriteStartDocument();
            XW.WriteStartElement("manifest");

            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                MemoryStream MS = new MemoryStream(Encoding.UTF8.GetBytes(PluginManifestFile));
                XmlDoc.Load(MS);
                MS.Close();
            }
            catch { throw new Exception("Invalid IronWASP update manifest file recieved."); }
            
            XmlNodeList PluginNodes = null;

            if (XmlDoc.ChildNodes.Count == 1)
            {
                PluginNodes = XmlDoc.FirstChild.ChildNodes;
            }
            else if (XmlDoc.ChildNodes.Count == 2)
            {
                PluginNodes = XmlDoc.ChildNodes[1].ChildNodes;
            }

            foreach (XmlNode PluginNode in PluginNodes)
            {
                GetNewPlugins(PluginNode);
            }
            //string[] PluginManifestLines = PluginManifestFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            //foreach (string Line in PluginManifestLines)
            //{
            //    string[] LineParts = Line.Split(new char[] { '|' }, 6);
            //    if (LineParts.Length != 6)
            //    {
            //        throw new Exception("Invalid 'Plugin Manifest File' recieved from server");
            //    }
            //    PluginManifestInfo.Add(LineParts);
            //}
            //List<string[]> CurrentPluginInfo = new List<string[]>();
            //foreach (string Name in ActivePlugin.List())
            //{
            //    ActivePlugin AP = ActivePlugin.Get(Name);
            //    string[] CurrentInfo = new string[] { "active", AP.Version, AP.FileName.Substring(AP.FileName.LastIndexOf('\\') + 1) };
            //    CurrentPluginInfo.Add(CurrentInfo);
            //}
            //foreach (string Name in PassivePlugin.List())
            //{
            //    PassivePlugin PP = PassivePlugin.Get(Name);
            //    string[] CurrentInfo = new string[] { "passive", PP.Version, PP.FileName.Substring(PP.FileName.LastIndexOf('\\') + 1) };
            //    CurrentPluginInfo.Add(CurrentInfo);
            //}
            //foreach (string Name in FormatPlugin.List())
            //{
            //    FormatPlugin FP = FormatPlugin.Get(Name);
            //    string[] CurrentInfo = new string[] { "format", FP.Version, FP.FileName.Substring(FP.FileName.LastIndexOf('\\') + 1) };
            //    CurrentPluginInfo.Add(CurrentInfo);
            //}
            //foreach (string Name in SessionPlugin.List())
            //{
            //    SessionPlugin SP = SessionPlugin.Get(Name);
            //    string[] CurrentInfo = new string[] { "session", SP.Version, SP.FileName.Substring(SP.FileName.LastIndexOf('\\') + 1) };
            //    CurrentPluginInfo.Add(CurrentInfo);
            //}
            //foreach (string[] PluginManifestInfoLine in PluginManifestInfo)
            //{
            //    if (PluginManifestInfoLine[0].StartsWith("+") || PluginManifestInfoLine[0].StartsWith("*"))
            //    {
            //        bool MatchFound = false;
            //        foreach (string[] CurrentPluginLineInfo in CurrentPluginInfo)
            //        {
            //            if (PluginManifestInfoLine[1].Equals(CurrentPluginLineInfo[0]) && PluginManifestInfoLine[3].Equals(CurrentPluginLineInfo[2]))
            //            {
            //                MatchFound = true;
            //                if (!PluginManifestInfoLine[2].Equals(CurrentPluginLineInfo[1]))
            //                {
            //                    DownloadPlugin(PluginManifestInfoLine[1], PluginManifestInfoLine[3], PluginManifestInfoLine[4]);
            //                }
            //                break;
            //            }
            //            else if (PluginManifestInfoLine[0].Contains("_"))
            //            {
            //                string[] SupportDetailParts = PluginManifestInfoLine[0].Split(new char[] { '_' }, 2);
            //                if (PluginManifestInfoLine[1].Equals(CurrentPluginLineInfo[0]) && SupportDetailParts[1].Equals(CurrentPluginLineInfo[2]))
            //                {
            //                    MatchFound = true;
            //                    if (!PluginManifestInfoLine[2].Equals(CurrentPluginLineInfo[1]))
            //                    {
            //                        DownloadPlugin(PluginManifestInfoLine[1], PluginManifestInfoLine[3], PluginManifestInfoLine[4]);
            //                    }
            //                    break;
            //                }
            //            }
            //        }
            //        if (!MatchFound)
            //        {
            //            DownloadPlugin(PluginManifestInfoLine[1], PluginManifestInfoLine[3], PluginManifestInfoLine[4]);
            //        }
            //    }
            //}
        }

        static void GetNewPlugins(XmlNode ManifestNode)
        {
            string PluginType = ManifestNode.Name;
            
            List<string[]> AllPluginInfo = new List<string[]>();

            switch (PluginType)
            {
                case("active"):
                    foreach (string Name in ActivePlugin.List())
                    {
                        ActivePlugin P = ActivePlugin.Get(Name);
                        AllPluginInfo.Add(new string[]{P.FileName, P.Version});
                    }
                    break;
                case ("passive"):
                    foreach (string Name in PassivePlugin.List())
                    {
                        PassivePlugin P = PassivePlugin.Get(Name);
                        AllPluginInfo.Add(new string[] { P.FileName, P.Version });
                    }
                    break;
                case ("format"):
                    foreach (string Name in FormatPlugin.List())
                    {
                        FormatPlugin P = FormatPlugin.Get(Name);
                        AllPluginInfo.Add(new string[] { P.FileName, P.Version });
                    }
                    break;
                case ("session"):
                    foreach (string Name in SessionPlugin.List())
                    {
                        SessionPlugin P = SessionPlugin.Get(Name);
                        AllPluginInfo.Add(new string[] { P.FileName, P.Version });
                    }
                    break;
            }
            
            StringBuilder SB = new StringBuilder();
            XmlWriter XW = XmlWriter.Create(SB);

            XW.WriteStartDocument();
            XW.WriteStartElement("manifest");

            foreach (XmlNode FileNode in ManifestNode.ChildNodes)
            {
                string Version = "";
                string Action = "";
                string FileName = "";
                string DownloadFileName = "";
                string Comment = "";
                List<string[]> SupportFiles = new List<string[]>();

                foreach (XmlNode PropertyNode in FileNode.ChildNodes)
                {
                    switch (PropertyNode.Name)
                    {
                        case ("version"):
                            Version = PropertyNode.InnerText;
                            break;
                        case ("action"):
                            Action = PropertyNode.InnerText;
                            break;
                        case ("filename"):
                            FileName = PropertyNode.InnerText;
                            break;
                        case ("downloadname"):
                            DownloadFileName = PropertyNode.InnerText;
                            break;
                        case ("comment"):
                            Comment = PropertyNode.InnerText;
                            break;
                        case ("support_file"):
                            string SupportFileName = "";
                            string SupportFileDownloadName = "";
                            foreach (XmlNode SupportFileNode in PropertyNode.ChildNodes)
                            {
                                switch (SupportFileNode.Name)
                                {
                                    case ("filename"):
                                        SupportFileName = SupportFileNode.InnerText;
                                        break;
                                    case ("downloadname"):
                                        SupportFileDownloadName = SupportFileNode.InnerText;
                                        break;
                                }
                            }
                            SupportFiles.Add(new string[]{SupportFileName, SupportFileDownloadName});
                            break;
                    }
                }

                if (Action.Equals("add") || Action.Equals("update"))
                {
                    bool MatchFound = false;
                    string[] MatchedPluginInfo = new string[2];
                    foreach (string[] PluginInfo in AllPluginInfo)
                    {
                        if (PluginInfo[0].Equals(FileName))
                        {
                            MatchFound = true;
                            MatchedPluginInfo = PluginInfo;
                            break;
                        }
                    }

                    if ((MatchFound && !MatchedPluginInfo[1].Equals(Version)) || !MatchFound)
                    {
                        DownloadPlugin(PluginType, FileName, DownloadFileName);
                        XW.WriteStartElement("file");
                        XW.WriteStartElement("action"); XW.WriteValue(Action); XW.WriteEndElement();
                        XW.WriteStartElement("filename"); XW.WriteValue(FileName); XW.WriteEndElement();
                        XW.WriteStartElement("comment"); XW.WriteValue(Comment); XW.WriteEndElement();
                        XW.WriteEndElement();
                        foreach (string[] SupportFile in SupportFiles)
                        {
                            DownloadPlugin(PluginType, SupportFile[0], SupportFile[1]);
                            XW.WriteStartElement("file");
                            XW.WriteStartElement("action"); XW.WriteValue(Action); XW.WriteEndElement();
                            XW.WriteStartElement("filename"); XW.WriteValue(SupportFile[0]); XW.WriteEndElement();
                            XW.WriteStartElement("comment"); XW.WriteValue(Comment); XW.WriteEndElement();
                            XW.WriteEndElement();
                        }
                    }
                }
            }

            XW.WriteEndElement();
            XW.WriteEndDocument();
            XW.Close();

            StreamWriter SW = File.CreateText(Config.Path + "\\updates\\" + PluginType + "_plugin_manifest.xml");
            SW.Write(SB.ToString());
            SW.Close();
        }

        static void GetNewIronWASP()
        {
            StringBuilder SB = new StringBuilder();
            XmlWriter XW = XmlWriter.Create(SB);

            XW.WriteStartDocument();
            XW.WriteStartElement("manifest");

            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                MemoryStream MS = new MemoryStream(Encoding.UTF8.GetBytes(IronWASPManifestFile));
                XmlDoc.Load(MS);
                MS.Close();
            }
            catch { throw new Exception("Invalid IronWASP update manifest file recieved."); }
            
            XmlNodeList FileNodes = null;

            if (XmlDoc.ChildNodes.Count == 1)
            {
                FileNodes = XmlDoc.FirstChild.ChildNodes;
            }
            else if (XmlDoc.ChildNodes.Count == 2)
            {
                FileNodes = XmlDoc.ChildNodes[1].ChildNodes;
            }

            foreach (XmlNode FileNode in FileNodes)
            {
                string Version = "";
                string Action = "";
                string FileName = "";
                string DownloadFileName = "";
                string Comment = "";

                foreach (XmlNode PropertyNode in FileNode.ChildNodes)
                {
                    switch (PropertyNode.Name)
                    {
                        case ("version"):
                            Version = PropertyNode.InnerText;
                            break;
                        case ("action"):
                            Action = PropertyNode.InnerText;
                            break;
                        case ("filename"):
                            FileName = PropertyNode.InnerText;
                            break;
                        case ("downloadname"):
                            DownloadFileName = PropertyNode.InnerText;
                            break;
                        case ("comment"):
                            Comment = PropertyNode.InnerText;
                            break;
                    }
                }

                if (Action.Equals("add") || Action.Equals("update"))
                {
                    if (IsGreaterVersion(Version))
                    {
                        DownloadIronWASPFile(FileName, DownloadFileName);
                        XW.WriteStartElement("file");
                        XW.WriteStartElement("action"); XW.WriteValue(Action); XW.WriteEndElement();
                        XW.WriteStartElement("filename"); XW.WriteValue(FileName); XW.WriteEndElement();
                        XW.WriteStartElement("comment"); XW.WriteValue(Comment); XW.WriteEndElement();
                        XW.WriteEndElement();
                    }
                }
            }

            XW.WriteEndElement();
            XW.WriteEndDocument();
            XW.Close();

            StreamWriter SW = File.CreateText(Config.Path + "\\updates\\ironwasp_manifest.xml");
            SW.Write(SB.ToString());
            SW.Close();

            //string[] IronWASPManifestLines = IronWASPManifestFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            //foreach (string Line in IronWASPManifestLines)
            //{
            //    string[] LineParts = Line.Split(new char[] { '|' }, 5);
            //    if (LineParts.Length != 5)
            //    {
            //        throw new Exception("Invalid 'IronWASP Manifest File' recieved from server");
            //    }
            //    IronWASPManifestInfo.Add(LineParts);
            //}
            //foreach (string[] IronWASPManifestInfoLine in IronWASPManifestInfo)
            //{
            //    if (IronWASPManifestInfoLine[0].Equals("+") || IronWASPManifestInfoLine[0].Equals("*"))
            //    {
            //        if (IsGreaterVersion(IronWASPManifestInfoLine[1]))
            //        {
            //            DownloadIronWASPFile(IronWASPManifestInfoLine[2], IronWASPManifestInfoLine[3]);
            //        }
            //    }
            //}
        }

        static void GetNewModulesDb()
        {
            string CurrentModulesDbFile = "";
            try
            {
                CurrentModulesDbFile = File.ReadAllText(string.Format("{0}\\ModulesDB.exe", Config.RootDir));
            }
            catch (Exception Exp)
            {
                throw new Exception("Unable to read the contents of ModulesDB.exe file", Exp);
            }
            try
            {
                if (!Tools.IsXmlContentSame(CurrentModulesDbFile, ModulesDbFile))
                {
                    try
                    {
                        File.WriteAllText(Config.Path + "\\updates\\ironwasp\\ModulesDB.exe", ModulesDbFile);
                        NewUpdateAvailable = true;
                    }
                    catch (Exception Exp)
                    {
                        throw new Exception("Unable to update the contents of ModulesDB.exe file", Exp);
                    }
                }
            }
            catch(Exception Exp)
            {
                throw new Exception("Invalid ModulesDB.exe file, not a valid XML", Exp);
            }
        }

        static void DownloadModule(string ModuleName, string PseudoName)
        {
            Request ModuleFetchReq = new Request(ModuleDownloadBaseUrl + "/" + PseudoName);
            ModuleFetchReq.Source = RequestSource.Stealth;
            Response ModuleFetchRes = ModuleFetchReq.Send();
            if (!ModuleFetchRes.IsSslValid)
            {
                throw new Exception("Invalid SSL Certificate provided by the server");
            }
            if (ModuleFetchRes.Code != 200)
            {
                throw new Exception("Downloading updated modules failed");
            }
            try
            {
                ModuleFetchRes.SaveBody(Config.Path + "\\updates\\modules\\" + ModuleName + ".zip");
                using (ZipFile ZF = ZipFile.Read(Config.Path + "\\updates\\modules\\" + ModuleName + ".zip"))
                {
                    ZF.ExtractAll(Config.Path + "\\updates\\modules\\");
                }
                NewUpdateAvailable = true;
            }
            catch (Exception Exp) { IronException.Report(string.Format("Error Downloading Module: {0} - {1} ", ModuleName, PseudoName), Exp); }
        }

        static void DownloadPlugin(string PluginType, string FileName, string PseudoName)
        {
            Request PluginFetchReq = new Request(PluginDownloadBaseUrl + PluginType + "/" + PseudoName);
            PluginFetchReq.Source = RequestSource.Stealth;
            Response PluginFetchRes = PluginFetchReq.Send();
            if (!PluginFetchRes.IsSslValid)
            {
                throw new Exception("Invalid SSL Certificate provided by the server");
            }
            if (PluginFetchRes.Code != 200)
            {
                throw new Exception("Downloading updated plugins failed");
            }
            try
            {
                PluginFetchRes.SaveBody(Config.Path + "\\updates\\plugins\\" + PluginType + "\\" + FileName);
                NewUpdateAvailable = true;
            }
            catch (Exception Exp) { IronException.Report(string.Format("Error Downloading Plugin: {0} - {1} - {2}", PluginType, FileName, PseudoName), Exp); }
        }

        static void DownloadIronWASPFile(string FileName, string PseudoName)
        {
            Request IronWASPFileFetchReq = new Request(IronWASPDownloadBaseUrl + PseudoName);
            IronWASPFileFetchReq.Source = RequestSource.Stealth;
            Response IronWASPFileFetchRes = IronWASPFileFetchReq.Send();
            if (!IronWASPFileFetchRes.IsSslValid)
            {
                throw new Exception("Invalid SSL Certificate provided by the server");
            }
            if (IronWASPFileFetchRes.Code != 200)
            {
                throw new Exception("Downloading updated IronWASP version failed");
            }
            if (FileName.Equals("Updater.exe"))
            {
                try
                {
                    File.Delete(Config.Path + "\\Updater.exe");
                }
                catch { }
                IronWASPFileFetchRes.SaveBody(Config.Path + "\\Updater.exe");
            }
            else
            {
                IronWASPFileFetchRes.SaveBody(Config.Path + "\\updates\\ironwasp\\" + FileName);
            }
            NewUpdateAvailable = true;
        }

        static bool IsGreaterVersion(string ManifestVersion)
        {
            try
            {
                string[] CurrentVersionParts = CurrentVersion.Split('.');
                string[] ManifestVersionParts = ManifestVersion.Split('.');
                List<int> CurrentVersionIntParts = new List<int>();
                List<int> ManifestVersionIntParts = new List<int>();
                foreach (string VP in CurrentVersionParts)
                {
                    CurrentVersionIntParts.Add(Int32.Parse(VP));
                }
                foreach (string VP in ManifestVersionParts)
                {
                    ManifestVersionIntParts.Add(Int32.Parse(VP));
                }
                if (CurrentVersionIntParts.Count != ManifestVersionIntParts.Count) return true;
                for (int i = 0; i < CurrentVersionIntParts.Count; i++)
                {
                    if (CurrentVersionIntParts[i] < ManifestVersionIntParts[i])
                    {
                        return true;
                    }
                    else if (CurrentVersionIntParts[i] > ManifestVersionIntParts[i])
                    {
                        return false;
                    }
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        internal static void StopUpdateCheck()
        {
            try
            {
                T.Abort();
            }
            catch{}
        }
    }
}
