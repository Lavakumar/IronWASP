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
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace IronWASP
{
    public class Config
    {
        internal static int ScanCount = 0;
        internal static int ActiveScansCount = 0;
        internal static int ProxyRequestsCount = 0;
        internal static int TestRequestsCount = 0;
        internal static int ScanRequestsCount = 0;
        internal static int ShellRequestsCount = 0;
        internal static int ProbeRequestsCount = 0;
        internal static int StealthRequestsCount = 0;
        internal static int PluginResultCount = 0;
        internal static int ExceptionsCount = 0;
        internal static int TraceCount = 0;
        internal static int ScanTraceCount = 0;
        internal static int SessionPluginTraceCount = 0;

        internal static Dictionary<string, int> OtherSourceCounterDict = new Dictionary<string, int>();

        internal static Dictionary<string, Request> APIResponseDict = new Dictionary<string, Request>();
        internal static bool OpeningIronProjectFile = false;
        internal static string RootDir = "";
        internal static bool BlinkPrompt = true;

        public static Dictionary<string, Dictionary<string, string>> UserAgentsList = new Dictionary<string,Dictionary<string,string>>();

        static Dictionary<string, string> FiddlerFlags = new Dictionary<string, string>();
        internal static bool HasFiddlerFlags = false;

        public static string Path
        {
            get
            {
                return RootDir;
            }
        }

        public static int LastProxyLogId
        {
            get
            {
                return ProxyRequestsCount;
            }
        }

        public static int LastProbeLogId
        {
            get
            {
                return ProbeRequestsCount;
            }
        }
        public static int LastShellLogId
        {
            get
            {
                return ShellRequestsCount;
            }
        }
        public static int LastScanLogId
        {
            get
            {
                return ScanRequestsCount;
            }
        }
        public static int LastTestLogId
        {
            get
            {
                return TestRequestsCount;
            }
        }
        internal static int LastScanTraceId
        {
            get
            {
                return ScanTraceCount;
            }
        }

        internal static bool IsSourcePresent(string Source)
        {
            if (OtherSourceCounterDict.ContainsKey(Source))
                return true;
            else
                return false;
        }

        internal static List<string> GetOtherSourceList()
        {
            return new List<string>(OtherSourceCounterDict.Keys);
        }

        internal static int GetNewId(string Source)
        {
            int Result = 0;
            lock(OtherSourceCounterDict)
            {
                if (!OtherSourceCounterDict.ContainsKey(Source))
                    OtherSourceCounterDict[Source] = 0;
                OtherSourceCounterDict[Source]++;
                Result = OtherSourceCounterDict[Source];
            }
            return Result;
        }

        public static int GetLastLogId(string Source)
        {
            switch (Source)
            {
                case("Proxy"):
                    return LastProxyLogId;
                case ("Probe"):
                    return LastProbeLogId;
                case ("Test"):
                    return LastTestLogId;
                case ("Shell"):
                    return LastShellLogId;
                case ("Scan"):
                    return LastScanLogId;
                default:
                    if (OtherSourceCounterDict.ContainsKey(Source))
                        return OtherSourceCounterDict[Source];
                    else
                        throw new Exception(string.Format("No logs available for source - {0}", Source));
            }
            
        }

        public static void SetFiddlerFlag(string Name, string Value)
        {
            if (Name.Equals("x-overrideGateway"))
            {
                throw new Exception("'x-overrideGateway' can only be set by updating the Upstream Proxy setting in UI");
            }
            lock (FiddlerFlags)
            {
                FiddlerFlags[Name] = Value;
                HasFiddlerFlags = (FiddlerFlags.Count > 0);
            }
        }

        public static List<string> ListFiddlerFlags()
        {
            lock (FiddlerFlags)
            {
                return new List<string>(FiddlerFlags.Keys);
            }
        }

        public static string[,] GetFiddlerFlags()
        {
            lock (FiddlerFlags)
            {
                string[,] Flags = new string[FiddlerFlags.Count, 2];
                int Index = 0;
                foreach (string Name in FiddlerFlags.Keys)
                {
                    Flags[Index, 0] = Name;
                    Flags[Index, 1] = FiddlerFlags[Name];
                    Index++;
                }
                return Flags;
            }
        }

        public static string GetFiddlerFlag(string Name)
        {
            lock (FiddlerFlags)
            {
                if (FiddlerFlags.ContainsKey(Name))
                    return FiddlerFlags[Name];
                else
                    return "";
            }
        }

        public static void RemoveFiddlerFlag(string Name)
        {
            lock(FiddlerFlags)
            {
                if (FiddlerFlags.ContainsKey(Name)) FiddlerFlags.Remove(Name);
                HasFiddlerFlags = (FiddlerFlags.Count > 0);
            }
        }

        internal static void UpdateInterceptionRulesFromUI()
        {
            IronProxy.InterceptGET = IronUI.UI.ConfigRuleGETMethodCB.Checked;
            IronProxy.InterceptPOST = IronUI.UI.ConfigRulePOSTMethodCB.Checked;
            IronProxy.InterceptOtherMethods = IronUI.UI.ConfigRuleOtherMethodsCB.Checked;
            IronProxy.Intercept200 = IronUI.UI.ConfigRuleCode200CB.Checked;
            IronProxy.Intercept2xx = IronUI.UI.ConfigRuleCode2xxCB.Checked;
            IronProxy.Intercept301_2 = IronUI.UI.ConfigRuleCode301_2CB.Checked;
            IronProxy.Intercept3xx = IronUI.UI.ConfigRuleCode3xxCB.Checked;
            IronProxy.Intercept403 = IronUI.UI.ConfigRuleCode403CB.Checked;
            IronProxy.Intercept4xx = IronUI.UI.ConfigRuleCode4xxCB.Checked;
            IronProxy.Intercept500 = IronUI.UI.ConfigRuleCode500CB.Checked;
            IronProxy.Intercept5xx = IronUI.UI.ConfigRuleCode5xxCB.Checked;
            IronProxy.InterceptHTML = IronUI.UI.ConfigRuleContentHTMLCB.Checked;
            IronProxy.InterceptCSS = IronUI.UI.ConfigRuleContentCSSCB.Checked;
            IronProxy.InterceptJS = IronUI.UI.ConfigRuleContentJSCB.Checked;
            IronProxy.InterceptXML = IronUI.UI.ConfigRuleContentXMLCB.Checked;
            IronProxy.InterceptJSON = IronUI.UI.ConfigRuleContentJSONCB.Checked;
            IronProxy.InterceptOtherText = IronUI.UI.ConfigRuleContentOtherTextCB.Checked;
            IronProxy.InterceptImg = IronUI.UI.ConfigRuleContentImgCB.Checked;
            IronProxy.InterceptOtherBinary = IronUI.UI.ConfigRuleContentOtherBinaryCB.Checked;
            IronProxy.InterceptCheckHostNames = IronUI.UI.ConfigRuleHostNamesCB.Checked;
            IronProxy.InterceptCheckHostNamesPlus = IronUI.UI.ConfigRuleHostNamesPlusRB.Checked;
            IronProxy.InterceptCheckHostNamesMinus = IronUI.UI.ConfigRuleHostNamesMinusRB.Checked;
            if (IronProxy.InterceptCheckHostNames)
            {
                if (IronProxy.InterceptCheckHostNamesPlus)
                {
                    string[] HostNames = IronUI.UI.ConfigRuleHostNamesPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.InterceptHostNames)
                    {
                        IronProxy.InterceptHostNames.Clear();
                        IronProxy.InterceptHostNames.AddRange(HostNames);
                    }
                }
                if (IronProxy.InterceptCheckHostNamesMinus)
                {
                    string[] HostNames = IronUI.UI.ConfigRuleHostNamesMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.DontInterceptHostNames)
                    {
                        IronProxy.DontInterceptHostNames.Clear();
                        IronProxy.DontInterceptHostNames.AddRange(HostNames);
                    }
                }
            }
            IronProxy.InterceptCheckFileExtensions = IronUI.UI.ConfigRuleFileExtensionsCB.Checked;
            IronProxy.InterceptCheckFileExtensionsPlus = IronUI.UI.ConfigRuleFileExtensionsPlusRB.Checked;
            IronProxy.InterceptCheckFileExtensionsMinus = IronUI.UI.ConfigRuleFileExtensionsMinusRB.Checked;
            if (IronProxy.InterceptCheckFileExtensions)
            {
                if (IronProxy.InterceptCheckFileExtensionsPlus)
                {
                    string[] FileExtensions = IronUI.UI.ConfigRuleFileExtensionsPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.InterceptFileExtensions)
                    {
                        IronProxy.InterceptFileExtensions.Clear();
                        IronProxy.InterceptFileExtensions.AddRange(FileExtensions);
                    }
                }
                if (IronProxy.InterceptCheckFileExtensionsMinus)
                {
                    string[] FileExtensions = IronUI.UI.ConfigRuleFileExtensionsMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.DontInterceptFileExtensions)
                    {
                        IronProxy.DontInterceptFileExtensions.Clear();
                        IronProxy.DontInterceptFileExtensions.AddRange(FileExtensions);
                    }
                }
            }
            IronProxy.InterceptCheckRequestWithKeyword = IronUI.UI.ConfigRuleKeywordInRequestCB.Checked;
            IronProxy.InterceptCheckRequestWithKeywordPlus = IronUI.UI.ConfigRuleKeywordInRequestPlusRB.Checked;
            IronProxy.InterceptCheckRequestWithKeywordMinus = IronUI.UI.ConfigRuleKeywordInRequestMinusRB.Checked;
            if (IronProxy.InterceptCheckRequestWithKeyword)
            {
                if (IronProxy.InterceptCheckRequestWithKeywordPlus)
                {
                    lock (IronProxy.InterceptRequestWithKeyword)
                    {
                        IronProxy.InterceptRequestWithKeyword = IronUI.UI.ConfigRuleKeywordInRequestPlusTB.Text;
                    }
                }
                if (IronProxy.InterceptCheckRequestWithKeywordMinus)
                {
                    lock (IronProxy.DontInterceptRequestWithKeyword)
                    {
                        IronProxy.DontInterceptRequestWithKeyword = IronUI.UI.ConfigRuleKeywordInRequestMinusTB.Text;
                    }
                }
            }
            IronProxy.InterceptCheckResponseWithKeyword = IronUI.UI.ConfigRuleKeywordInResponseCB.Checked;
            IronProxy.InterceptCheckResponseWithKeywordPlus = IronUI.UI.ConfigRuleKeywordInResponsePlusRB.Checked;
            IronProxy.InterceptCheckResponseWithKeywordMinus = IronUI.UI.ConfigRuleKeywordInResponseMinusRB.Checked;
            if (IronProxy.InterceptCheckResponseWithKeyword)
            {
                if (IronProxy.InterceptCheckResponseWithKeywordPlus)
                {
                    lock (IronProxy.InterceptResponseWithKeyword)
                    {
                        IronProxy.InterceptResponseWithKeyword = IronUI.UI.ConfigRuleKeywordInResponsePlusTB.Text;
                    }
                }
                if (IronProxy.InterceptCheckResponseWithKeywordMinus)
                {
                    lock (IronProxy.DontInterceptResponseWithKeyword)
                    {
                        IronProxy.DontInterceptResponseWithKeyword = IronUI.UI.ConfigRuleKeywordInResponseMinusTB.Text;
                    }
                }
            }

            IronProxy.RequestRulesOnResponse = IronUI.UI.ConfigRuleRequestOnResponseRulesCB.Checked;
        }

        internal static void UpdateProxyLogDisplayFilterFromUI()
        {
            IronProxy.DisplayGET = IronUI.UI.ConfigDisplayRuleGETMethodCB.Checked;
            IronProxy.DisplayPOST = IronUI.UI.ConfigDisplayRulePOSTMethodCB.Checked;
            IronProxy.DisplayOtherMethods = IronUI.UI.ConfigDisplayRuleOtherMethodsCB.Checked;
            IronProxy.Display200 = IronUI.UI.ConfigDisplayRuleCode200CB.Checked;
            IronProxy.Display2xx = IronUI.UI.ConfigDisplayRuleCode2xxCB.Checked;
            IronProxy.Display301_2 = IronUI.UI.ConfigDisplayRuleCode301_2CB.Checked;
            IronProxy.Display3xx = IronUI.UI.ConfigDisplayRuleCode3xxCB.Checked;
            IronProxy.Display403 = IronUI.UI.ConfigDisplayRuleCode403CB.Checked;
            IronProxy.Display4xx = IronUI.UI.ConfigDisplayRuleCode4xxCB.Checked;
            IronProxy.Display500 = IronUI.UI.ConfigDisplayRuleCode500CB.Checked;
            IronProxy.Display5xx = IronUI.UI.ConfigDisplayRuleCode5xxCB.Checked;
            IronProxy.DisplayHTML = IronUI.UI.ConfigDisplayRuleContentHTMLCB.Checked;
            IronProxy.DisplayCSS = IronUI.UI.ConfigDisplayRuleContentCSSCB.Checked;
            IronProxy.DisplayJS = IronUI.UI.ConfigDisplayRuleContentJSCB.Checked;
            IronProxy.DisplayXML = IronUI.UI.ConfigDisplayRuleContentXMLCB.Checked;
            IronProxy.DisplayJSON = IronUI.UI.ConfigDisplayRuleContentJSONCB.Checked;
            IronProxy.DisplayOtherText = IronUI.UI.ConfigDisplayRuleContentOtherTextCB.Checked;
            IronProxy.DisplayImg = IronUI.UI.ConfigDisplayRuleContentImgCB.Checked;
            IronProxy.DisplayOtherBinary = IronUI.UI.ConfigDisplayRuleContentOtherBinaryCB.Checked;
            IronProxy.DisplayCheckHostNames = IronUI.UI.ConfigDisplayRuleHostNamesCB.Checked;
            IronProxy.DisplayCheckHostNamesPlus = IronUI.UI.ConfigDisplayRuleHostNamesPlusRB.Checked;
            IronProxy.DisplayCheckHostNamesMinus = IronUI.UI.ConfigDisplayRuleHostNamesMinusRB.Checked;
            if (IronProxy.DisplayCheckHostNames)
            {
                if (IronProxy.DisplayCheckHostNamesPlus)
                {
                    string[] HostNames = IronUI.UI.ConfigDisplayRuleHostNamesPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.DisplayHostNames)
                    {
                        IronProxy.DisplayHostNames.Clear();
                        IronProxy.DisplayHostNames.AddRange(HostNames);
                    }
                }
                if (IronProxy.DisplayCheckHostNamesMinus)
                {
                    string[] HostNames = IronUI.UI.ConfigDisplayRuleHostNamesMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.DontDisplayHostNames)
                    {
                        IronProxy.DontDisplayHostNames.Clear();
                        IronProxy.DontDisplayHostNames.AddRange(HostNames);
                    }
                }
            }
            IronProxy.DisplayCheckFileExtensions = IronUI.UI.ConfigDisplayRuleFileExtensionsCB.Checked;
            IronProxy.DisplayCheckFileExtensionsPlus = IronUI.UI.ConfigDisplayRuleFileExtensionsPlusRB.Checked;
            IronProxy.DisplayCheckFileExtensionsMinus = IronUI.UI.ConfigDisplayRuleFileExtensionsMinusRB.Checked;
            if (IronProxy.DisplayCheckFileExtensions)
            {
                if (IronProxy.DisplayCheckFileExtensionsPlus)
                {
                    string[] FileExtensions = IronUI.UI.ConfigDisplayRuleFileExtensionsPlusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.DisplayFileExtensions)
                    {
                        IronProxy.DisplayFileExtensions.Clear();
                        IronProxy.DisplayFileExtensions.AddRange(FileExtensions);
                    }
                }
                if (IronProxy.DisplayCheckFileExtensionsMinus)
                {
                    string[] FileExtensions = IronUI.UI.ConfigDisplayRuleFileExtensionsMinusTB.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    lock (IronProxy.DontDisplayFileExtensions)
                    {
                        IronProxy.DontDisplayFileExtensions.Clear();
                        IronProxy.DontDisplayFileExtensions.AddRange(FileExtensions);
                    }
                }
            }
            IronUI.UpdateProxyLogBasedOnDisplayFilter();
        }

        internal static void UpdateJSTaintConfigFromUI()
        {
            List<string> SourceObjects = new List<string>();
            List<string> SinkObjects = new List<string>();
            List<string> SourceReturningMethods = new List<string>() { };
            List<string> SinkReturningMethods = new List<string>() { };
            List<string> ArgumentReturningMethods = new List<string>() { };
            List<string> ArgumentAssignedToSinkMethods = new List<string>();
            List<string> ArgumentAssignedASourceMethods = new List<string>() { };

            foreach (DataGridViewRow Row in IronUI.UI.ConfigDefaultJSTaintConfigGrid.Rows)
            {
                if (Row == null) continue;
                if (Row.Cells == null) continue;
                if (Row.Cells.Count < 7) continue;
                if (Row.Cells["ConfigDefaultSourceObjectsColumn"].Value != null)
                {
                    string SourceObject = Row.Cells["ConfigDefaultSourceObjectsColumn"].Value.ToString().Trim();
                    if (SourceObject.Length > 0) SourceObjects.Add(SourceObject);
                }
                if (Row.Cells["ConfigDefaultSinkObjectsColumn"].Value != null)
                {
                    string SinkObject = Row.Cells["ConfigDefaultSinkObjectsColumn"].Value.ToString().Trim();
                    if (SinkObject.Length > 0) SinkObjects.Add(SinkObject);
                }
                if (Row.Cells["ConfigDefaultArgumentAssignedASourceMethodsColumn"].Value != null)
                {
                    string ArgumentAssignedASourceMethod = Row.Cells["ConfigDefaultArgumentAssignedASourceMethodsColumn"].Value.ToString().Trim();
                    if (ArgumentAssignedASourceMethod.Length > 0) ArgumentAssignedASourceMethods.Add(ArgumentAssignedASourceMethod);
                }
                if (Row.Cells["ConfigDefaultArgumentAssignedToSinkMethodsColumn"].Value != null)
                {
                    string ArgumentAssignedToSinkMethod = Row.Cells["ConfigDefaultArgumentAssignedToSinkMethodsColumn"].Value.ToString().Trim();
                    if (ArgumentAssignedToSinkMethod.Length > 0) ArgumentAssignedToSinkMethods.Add(ArgumentAssignedToSinkMethod);
                }
                if (Row.Cells["ConfigDefaultSourceReturningMethodsColumn"].Value != null)
                {
                    string SourceReturningMethod = Row.Cells["ConfigDefaultSourceReturningMethodsColumn"].Value.ToString().Trim();
                    if (SourceReturningMethod.Length > 0) SourceReturningMethods.Add(SourceReturningMethod);
                }
                if (Row.Cells["ConfigDefaultSinkReturningMethodsColumn"].Value != null)
                {
                    string SinkReturningMethod = Row.Cells["ConfigDefaultSinkReturningMethodsColumn"].Value.ToString().Trim();
                    if (SinkReturningMethod.Length > 0) SinkReturningMethods.Add(SinkReturningMethod);
                }
                if (Row.Cells["ConfigDefaultArgumentReturningMethodsColumn"].Value != null)
                {
                    string ArgumentReturningMethod = Row.Cells["ConfigDefaultArgumentReturningMethodsColumn"].Value.ToString().Trim();
                    if (ArgumentReturningMethod.Length > 0) ArgumentReturningMethods.Add(ArgumentReturningMethod);
                }
            }
            lock (IronJint.DefaultSourceObjects)
            {
                IronJint.DefaultSourceObjects = new List<string>(SourceObjects);
            }
            lock (IronJint.DefaultSinkObjects)
            {
                IronJint.DefaultSinkObjects = new List<string>(SinkObjects);
            }
            lock (IronJint.DefaultArgumentAssignedASourceMethods)
            {
                IronJint.DefaultArgumentAssignedASourceMethods = new List<string>(ArgumentAssignedASourceMethods);
            }
            lock (IronJint.DefaultArgumentAssignedToSinkMethods)
            {
                IronJint.DefaultArgumentAssignedToSinkMethods = new List<string>(ArgumentAssignedToSinkMethods);
            }
            lock (IronJint.DefaultSourceReturningMethods)
            {
                IronJint.DefaultSourceReturningMethods = new List<string>(SourceReturningMethods);
            }
            lock (IronJint.DefaultSinkReturningMethods)
            {
                IronJint.DefaultSinkReturningMethods = new List<string>(SinkReturningMethods);
            }
            lock (IronJint.DefaultArgumentReturningMethods)
            {
                IronJint.DefaultArgumentReturningMethods = new List<string>(ArgumentReturningMethods);
            }
        }

        internal static void UpdateScannerSettingsFromUI()
        {
            Scanner.MaxParallelScanCount = IronUI.UI.ConfigScannerThreadMaxCountTB.Value;
        }

        internal static void UpdatePassiveAnalysisSettingsFromUI()
        {
            PassiveChecker.RunOnProxyTraffic = IronUI.UI.ConfigPassiveAnalysisOnProxyTrafficCB.Checked;
            PassiveChecker.RunOnShellTraffic = IronUI.UI.ConfigPassiveAnalysisOnShellTrafficCB.Checked;
            PassiveChecker.RunOnTestTraffic = IronUI.UI.ConfigPassiveAnalysisOnTestTrafficCB.Checked;
            PassiveChecker.RunOnScanTraffic = IronUI.UI.ConfigPassiveAnalysisOnScanTrafficCB.Checked;
            PassiveChecker.RunOnProbeTraffic = IronUI.UI.ConfigPassiveAnalysisOnProbeTrafficCB.Checked;
        }

        internal static void SetRootDir()
        {
            Assembly MainAssembly = Assembly.GetExecutingAssembly();
            RootDir = Directory.GetParent(MainAssembly.Location).FullName;
        }

        internal static void ReadUserAgentsList()
        {
            try
            {
                XmlDocument UAL = new XmlDocument();
                UAL.Load(string.Format("{0}//useragentswitcher.xml", Config.Path));
                Config.UserAgentsList = new Dictionary<string, Dictionary<string, string>>();
                foreach (XmlNode CategoryNode in UAL.SelectNodes("useragentswitcher")[0].SelectNodes("folder"))
                {
                    string CategoryName = CategoryNode.Attributes["description"].Value;
                    Dictionary<string, string> CurrentCategoryList = new Dictionary<string, string>();
                    foreach (XmlNode UserAgentNode in CategoryNode.SelectNodes("useragent"))
                    {
                        try
                        {
                            string Name = UserAgentNode.Attributes["description"].Value;
                            string UserAgent = UserAgentNode.Attributes["useragent"].Value;
                            if (Name.Length > 0 && UserAgent.Length > 0)
                                CurrentCategoryList.Add(Name, UserAgent);
                        }
                        catch { }
                    }
                    foreach (XmlNode SubFolderNode in CategoryNode.SelectNodes("folder"))
                    {
                        foreach (XmlNode SubUserAgentNode in SubFolderNode.SelectNodes("useragent"))
                        {
                            try
                            {
                                string Name = SubUserAgentNode.Attributes["description"].Value;
                                string UserAgent = SubUserAgentNode.Attributes["useragent"].Value;
                                if (Name.Length > 0 && UserAgent.Length > 0)
                                    CurrentCategoryList.Add(Name, UserAgent);
                            }
                            catch { }
                        }
                    }
                    if (CurrentCategoryList.Count > 0)
                    {
                        Config.UserAgentsList[CategoryName] = CurrentCategoryList;
                    }
                }
            }
            catch { }
        }
    }
}