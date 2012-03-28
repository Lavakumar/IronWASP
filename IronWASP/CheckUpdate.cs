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
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IronWASP
{
    class CheckUpdate
    {
        static string CurrentVersion = "0.9.0.3";

        static string PluginManifestUrl = "https://ironwasp.org/update/plugin.manifest";
        static string IronWASPManifestUrl = "https://ironwasp.org/update/ironwasp.manifest";

        static string PluginDownloadBaseUrl = "https://ironwasp.org/update/plugins/";
        static string IronWASPDownloadBaseUrl = "https://ironwasp.org/update/ironwasp/";

        static string PluginManifestFile = "";
        static string IronWASPManifestFile = "";

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
                Request PluginManifestReq = new Request(PluginManifestUrl);
                PluginManifestReq.Source = RequestSource.Stealth;
                PluginManifestReq.Headers.Set("User-Agent","IronWASP v" + CurrentVersion);
                Response PluginManifestRes = PluginManifestReq.Send();
                if (!PluginManifestRes.IsSslValid)
                {
                    throw new Exception("Invalid SSL Certificate provided by the server");
                }
                PluginManifestFile = PluginManifestRes.BodyString;

                Request IronWASPManifestReq = new Request(IronWASPManifestUrl);
                IronWASPManifestReq.Source = RequestSource.Stealth;
                IronWASPManifestReq.Headers.Set("User-Agent", "IronWASP v" + CurrentVersion);
                Response IronWASPManifestRes = IronWASPManifestReq.Send();
                if (!IronWASPManifestRes.IsSslValid)
                {
                    throw new Exception("Invalid SSL Certificate provided by the server");
                }
                IronWASPManifestFile = IronWASPManifestRes.BodyString;
                
                SetUpUpdateDirs();
                GetNewPlugins();
                GetNewIronWASP();
                if (NewUpdateAvailable)
                {
                    try
                    {
                        Tools.Run(Config.RootDir + "/" + "Updater.exe");
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
                Directory.CreateDirectory(Config.Path + "\\updates\\ironwasp");
            }
            catch
            {
                throw new Exception("Unable to create Update directories, update failed");
            }
        }

        static void GetNewPlugins()
        {
            string[] PluginManifestLines = PluginManifestFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string Line in PluginManifestLines)
            {
                string[] LineParts = Line.Split(new char[] { '|' }, 6);
                if (LineParts.Length != 6)
                {
                    throw new Exception("Invalid 'Plugin Manifest File' recieved from server");
                }
                PluginManifestInfo.Add(LineParts);
            }
            List<string[]> CurrentPluginInfo = new List<string[]>();
            foreach (string Name in ActivePlugin.List())
            {
                ActivePlugin AP = ActivePlugin.Get(Name);
                string[] CurrentInfo = new string[] { "active", AP.Version, AP.FileName.Substring(AP.FileName.LastIndexOf('\\') + 1) };
                CurrentPluginInfo.Add(CurrentInfo);
            }
            foreach (string Name in PassivePlugin.List())
            {
                PassivePlugin PP = PassivePlugin.Get(Name);
                string[] CurrentInfo = new string[] { "passive", PP.Version, PP.FileName.Substring(PP.FileName.LastIndexOf('\\') + 1) };
                CurrentPluginInfo.Add(CurrentInfo);
            }
            foreach (string Name in FormatPlugin.List())
            {
                FormatPlugin FP = FormatPlugin.Get(Name);
                string[] CurrentInfo = new string[] { "format", FP.Version, FP.FileName.Substring(FP.FileName.LastIndexOf('\\') + 1) };
                CurrentPluginInfo.Add(CurrentInfo);
            }
            foreach (string Name in SessionPlugin.List())
            {
                SessionPlugin SP = SessionPlugin.Get(Name);
                string[] CurrentInfo = new string[] { "session", SP.Version, SP.FileName.Substring(SP.FileName.LastIndexOf('\\') + 1) };
                CurrentPluginInfo.Add(CurrentInfo);
            }
            foreach (string[] PluginManifestInfoLine in PluginManifestInfo)
            {
                if (PluginManifestInfoLine[0].Equals("+") || PluginManifestInfoLine[0].Equals("*"))
                {
                    bool MatchFound = false;
                    foreach (string[] CurrentPluginLineInfo in CurrentPluginInfo)
                    {
                        if (PluginManifestInfoLine[1].Equals(CurrentPluginLineInfo[0]) && PluginManifestInfoLine[3].Equals(CurrentPluginLineInfo[2]))
                        {
                            MatchFound = true;
                            if (!PluginManifestInfoLine[2].Equals(CurrentPluginLineInfo[1]))
                            {
                                DownloadPlugin(PluginManifestInfoLine[1], PluginManifestInfoLine[3], PluginManifestInfoLine[4]);
                            }
                            break;
                        }
                    }
                    if (!MatchFound)
                    {
                        DownloadPlugin(PluginManifestInfoLine[1], PluginManifestInfoLine[3], PluginManifestInfoLine[4]);
                    }
                }
            }
        }

        static void GetNewIronWASP()
        {
            string[] IronWASPManifestLines = IronWASPManifestFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string Line in IronWASPManifestLines)
            {
                string[] LineParts = Line.Split(new char[] { '|' }, 5);
                if (LineParts.Length != 5)
                {
                    throw new Exception("Invalid 'IronWASP Manifest File' recieved from server");
                }
                IronWASPManifestInfo.Add(LineParts);
            }
            foreach (string[] IronWASPManifestInfoLine in IronWASPManifestInfo)
            {
                if (IronWASPManifestInfoLine[0].Equals("+") || IronWASPManifestInfoLine[0].Equals("*"))
                {
                    if (IsGreaterVersion(IronWASPManifestInfoLine[1]))
                    {
                        DownloadIronWASPFile(IronWASPManifestInfoLine[2], IronWASPManifestInfoLine[3]);
                    }
                }
            }
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
            PluginFetchRes.SaveBody(Config.Path + "\\updates\\plugins\\" + PluginType + "\\" + FileName);
            NewUpdateAvailable = true;
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
