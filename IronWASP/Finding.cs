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
using System.Xml;
using System.IO;

namespace IronWASP
{
    public class Finding
    {
        internal static Finding CurrentPluginResult = null;
        internal static List<int> TriggersSelectedForDiff = new List<int>();

        public int Id = 0;
        //public string Plugin="";
        public string Title = "";
        public string Summary = "";
        public string AffectedHost = "";
        public Triggers Triggers = new Triggers();
        public FindingSeverity Severity = FindingSeverity.Low;
        public FindingConfidence Confidence = FindingConfidence.Low;
        public FindingType Type = FindingType.Vulnerability;
        public string Signature = "";

        public string FinderName = "";
        public string FinderType = "";

        public int ScanId = 0;

        public string AffectedSection = "";
        public string AffectedParameter = "";
        public List<FindingReason> Reasons = new List<FindingReason>();
        public string ScanTrace = "";

        public Request BaseRequest;
        public Response BaseResponse;

        static Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> Signatures = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();

        public string Plugin
        {
            get
            {
                return this.FinderName;
            }
            set
            {
                this.FinderName = value;
            }
        }

        public string XmlSummary
        {
            set
            {
                XmlDocument XDoc = new XmlDocument();
                XDoc.XmlResolver = null;
                XDoc.LoadXml(value);
                foreach (XmlElement Node in XDoc.DocumentElement.ChildNodes)
                {
                    switch (Node.Name)
                    {
                        case ("desc"):
                            this.Summary = Node.InnerText;
                            break;
                        case ("trace"):
                            this.ScanTrace = Node.InnerText;
                            break;
                        case ("reasons"):
                            this.Reasons.Clear();
                            foreach (XmlElement ReasonNode in Node.ChildNodes)
                            {
                                string Reason = "";
                                string ReasonType = "";
                                string TriggerIds = "";
                                string FalsePositiveCheck = "";
                                foreach (XmlElement ReasonValue in ReasonNode.ChildNodes)
                                {
                                    switch (ReasonValue.Name)
                                    {
                                        case ("desc"):
                                            Reason = ReasonValue.InnerText;
                                            break;
                                        case ("type"):
                                            ReasonType = ReasonValue.InnerText;
                                            break;
                                        case ("triggers"):
                                            TriggerIds = ReasonValue.InnerText;
                                            break;
                                        case ("fpcheck"):
                                            FalsePositiveCheck = ReasonValue.InnerText;
                                            break;
                                    }
                                }
                                FindingReason FR = new FindingReason(Reason, ReasonType, FindingReason.StringToTriggerIds(TriggerIds), FalsePositiveCheck);
                                this.Reasons.Add(FR);
                            }
                            break;
                    }
                }
            }
            get
            {
                StringBuilder SB = new StringBuilder();
                XmlWriter XW = XmlWriter.Create(SB);
                XW.WriteStartDocument();
                XW.WriteStartElement("summary");

                XW.WriteStartElement("desc"); XW.WriteValue(this.Summary); XW.WriteEndElement();
                if (this.FromActiveScan)
                {
                    if (Reasons.Count > 0)
                    {
                        XW.WriteStartElement("reasons");
                        foreach (FindingReason Reason in Reasons)
                        {
                            XW.WriteStartElement("reason");

                            XW.WriteStartElement("desc"); XW.WriteValue(Reason.Reason); XW.WriteEndElement();
                            XW.WriteStartElement("type"); XW.WriteValue(Reason.ReasonType); XW.WriteEndElement();
                            XW.WriteStartElement("triggers"); XW.WriteValue(FindingReason.TriggerIdsToString(Reason.TriggerIds)); XW.WriteEndElement();
                            XW.WriteStartElement("fpcheck"); XW.WriteValue(Reason.FalsePositiveCheck); XW.WriteEndElement();

                            XW.WriteEndElement();
                        }
                        XW.WriteEndElement();
                    }
                    XW.WriteStartElement("trace"); XW.WriteValue(this.ScanTrace); XW.WriteEndElement();
                }

                XW.WriteEndElement();
                XW.WriteEndDocument();
                XW.Close();
                return SB.ToString();
            }
        }

        public string XmlMeta
        {
            set
            {
                XmlDocument XDoc = new XmlDocument();
                XDoc.XmlResolver = null;
                XDoc.LoadXml(value);
                foreach (XmlElement Node in XDoc.DocumentElement.ChildNodes)
                {
                    switch (Node.Name)
                    {
                        case ("scanid"):
                            this.ScanId = Int32.Parse(Node.InnerText);
                            break;
                        case ("section"):
                            this.AffectedSection = Node.InnerText;
                            break;
                        case ("parameter"):
                            this.AffectedParameter = Node.InnerText;
                            break;
                    }
                }
            }
            get
            {
                StringBuilder SB = new StringBuilder();
                XmlWriter XW = XmlWriter.Create(SB);
                XW.WriteStartDocument();
                XW.WriteStartElement("meta");
                if (this.FromActiveScan)
                {
                    XW.WriteStartElement("scanid"); XW.WriteValue(this.ScanId); XW.WriteEndElement();
                    XW.WriteStartElement("section"); XW.WriteValue(this.AffectedSection); XW.WriteEndElement();
                    XW.WriteStartElement("parameter"); XW.WriteValue(this.AffectedParameter); XW.WriteEndElement();
                    //ReasonsTypes are stored here to help with searching for issues of similar nature. Values from here are not used to update the Reasons property of Finding.
                    if (Reasons.Count > 0)
                    {
                        XW.WriteStartElement("reasons");
                        foreach (FindingReason Reason in Reasons)
                        {
                            XW.WriteStartElement("reason"); XW.WriteValue(Reason.ReasonType); XW.WriteEndElement();
                        }
                        XW.WriteEndElement();
                    }
                }
                XW.WriteEndElement();
                XW.WriteEndDocument();
                XW.Close();
                return SB.ToString();
            }
        }

        public bool FromActiveScan
        {
            get
            {
                return this.FinderType.Equals("ActivePlugin");
            }
        }

        public Finding(string AffectedHost)
        {
            this.AffectedHost = AffectedHost;
        }

        public void Report()
        {
            if (IsSignatureUnique(this.Plugin, this.AffectedHost, this.Type, this.Signature, true))
            {
                IronUpdater.AddPluginResult(this);
            }
        }

        public void AddReason(FindingReason Reason)
        {
            this.Reasons.Add(Reason);
        }

        public static bool IsSignatureUnique(string PluginName, string Host, FindingType Type, string Signature)
        {
            return IsSignatureUnique(PluginName, Host, Type, Signature, false);
        }

        internal static bool IsSignatureUnique(string PluginName, string Host, FindingType Type, string Signature, bool AddIfUnique)
        {
            bool IsUnique = false;
            if (PluginName.Length == 0) return false;
            lock (Signatures)
            {
                if (!Signatures.ContainsKey(PluginName))
                {
                    IsUnique = true;
                    if (AddIfUnique)
                        Signatures.Add(PluginName, new Dictionary<string, Dictionary<string, List<string>>>());
                    else
                        return true;
                }

                if (Signature.Length == 0)
                {
                    IsUnique = true;
                    if (!AddIfUnique) return true;
                }


                if (!Signatures[PluginName].ContainsKey(Host))
                {
                    IsUnique = true;
                    if (AddIfUnique)
                        Signatures[PluginName].Add(Host, new Dictionary<string, List<string>>());
                    else
                        return true;
                }


                if (!Signatures[PluginName][Host].ContainsKey(Type.ToString()))
                {
                    IsUnique = true;
                    if (AddIfUnique)
                        Signatures[PluginName][Host].Add(Type.ToString(), new List<string>());
                    else
                        return true;
                }

                if (IsUnique && AddIfUnique)
                {
                    Signatures[PluginName][Host][Type.ToString()].Add(Signature);
                    return true;
                }
                else if (Signatures[PluginName][Host][Type.ToString()].Contains(Signature))
                {
                    return false;
                }
                else
                {
                    if (AddIfUnique)
                    {
                        Signatures[PluginName][Host][Type.ToString()].Add(Signature);
                    }
                    return true;
                }
            }
        }

        public static List<string> GetSignatureList(string PluginName, string Host, FindingType Type)
        {
            List<string> SignatureList = new List<string>();
            lock (Signatures)
            {
                if (Signatures.ContainsKey(PluginName))
                {
                    if (Signatures[PluginName].ContainsKey(Host))
                    {
                        if (Signatures[PluginName][Host].ContainsKey(Type.ToString()))
                        {
                            SignatureList.AddRange(Signatures[PluginName][Host][Type.ToString()]);
                        }
                    }
                }
            }
            return SignatureList;
        }

        public static string GetTriggerHighlighting(Trigger SelectedTrigger, string FinderType, bool IsNormal)
        {
            return GetTriggerHighlighting(SelectedTrigger, FinderType, IsNormal, true);
        }

        public static string GetTriggerHighlighting(Trigger SelectedTrigger, string FinderType, bool IsNormal, bool IncludeDesc)
        {
            if (IsNormal)
            {
                return @"
IronWASP's scanner identified this vulnerability by sending special payloads to the application and observing how its responded.

Before sending the payloads the scanner sends a normal request and sees how the server responds to it. You can see this normal request and response in the adjacent tabs.

Click on the items named <i<cb>>Trigger<i</cb>> (and an ID number) on the left-side to see the Requests containing the special payloads and corresponding Responses.

Head to the <i<cb>><i<b>>Trigger Analysis Tools<i</b>><i</cb>> tab to:
1) Do a diff of the normal Request/Response with the Trigger Request/Response or do a diff of two Trigger Request/Response.
2) View all the payloads, requests and responses associated with the scan that discovered this vulnerability.
3) Resend any Requests from this section or perform other similar actions

";
            }

            bool RequestTriggerPresent = false;
            bool ResponseTriggerPresent = false;
            bool RequestTriggerHighlighted = false;
            bool ResponseTriggerHighlighted = false;

            string HighlightedRequest = "";
            string HighlightedResponse = "";


            if (SelectedTrigger.RequestTrigger.Length > 0)
            {
                RequestTriggerPresent = true;
                if (SelectedTrigger.Request != null)
                {
                    HighlightedRequest = GetRequestTriggerHighlighting(SelectedTrigger.RequestTrigger, SelectedTrigger.Request);
                    if (HighlightedRequest.Contains("<i<hlg>>") && HighlightedRequest.Contains("<i</hlg>>"))
                    {
                        RequestTriggerHighlighted = true;
                    }
                }
            }

            if (SelectedTrigger.ResponseTrigger.Length > 0)
            {
                ResponseTriggerPresent = true;
                if (SelectedTrigger.Response != null)
                {
                    HighlightedResponse = GetResponseTriggerHighlighting(SelectedTrigger.ResponseTrigger, SelectedTrigger.Response);
                    if (HighlightedResponse.Contains("<i<hlg>>") && HighlightedResponse.Contains("<i</hlg>>"))
                    {
                        ResponseTriggerHighlighted = true;
                    }
                }
            }

            StringBuilder SB = new StringBuilder();
            switch (FinderType)
            {
                case ("ActivePlugin"):

                    if (IncludeDesc)
                    {
                        SB.Append("One pair of Request and Response that was helpful in identifing this vulnerability is available in the adjacent tabs.");
                        SB.Append("<i<br>>");
                        SB.Append(GetHighlightDescription(RequestTriggerPresent, ResponseTriggerPresent));
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                    }
                    if (RequestTriggerHighlighted)
                    {
                        SB.Append("<i<hh>> Request sent by Scanner: <i</hh>>");
                        if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>");
                        }
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        SB.Append(HighlightedRequest);
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                    }
                    else
                    {
                        if (RequestTriggerPresent)
                        {
                            SB.Append("<i<hh>> Interesting part of Request sent by Scanner: <i</hh>>");
                            if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                            {
                                SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>");
                            }
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append("IronWASP is not able to automatically highlight the interesting section of the Request, you would have to identify it manually.");
                            SB.Append("<i<br>>");
                            SB.Append("The scanner reported the following text as being of interest in this case:");
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append(GetInterestingTextWrap(SelectedTrigger.RequestTrigger));
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        }
                        else if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<hh>> Information about the Request sent by Scanner: <i</hh>>");
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>"); SB.Append("<i<br>><i<br>>");
                        }
                    }

                    if (ResponseTriggerHighlighted)
                    {
                        SB.Append("<i<hh>> Response from the Server: <i</hh>>");
                        if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>");
                        }
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        SB.Append(HighlightedResponse);
                        SB.Append("<i<br>>");
                    }
                    else
                    {
                        if (ResponseTriggerPresent)
                        {
                            SB.Append("<i<hh>> Interesting part of Response from the Server: <i</hh>>");
                            if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                            {
                                SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>");
                            }
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append("IronWASP is not able to automatically highlight the interesting section of the Response, you would have to identify it manually.");
                            SB.Append("<i<br>>");
                            SB.Append("The scanner reported the following text as being of interest in this case:");
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append(GetInterestingTextWrap(SelectedTrigger.ResponseTrigger));
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        }
                        else if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<hh>> Information about the Response from the Server: <i</hh>>");
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>"); SB.Append("<i<br>><i<br>>");
                        }
                        else
                        {
                            if (SelectedTrigger.Response != null)
                            {
                                SB.Append(string.Format("<i<hh>> The Response from the Server came back in {0} milli seconds <i</hh>>", SelectedTrigger.Response.RoundTrip));
                            }
                        }
                    }
                    break;

                case ("PassivePlugin"):
                    if (IncludeDesc)
                    {
                        SB.Append("One pair of Request and Response that was analyzed to identify this vulnerability is available in the adjacent tabs.");
                        SB.Append("<i<br>>");
                        SB.Append(GetHighlightDescription(RequestTriggerPresent, ResponseTriggerPresent));
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                    }
                    if (RequestTriggerHighlighted)
                    {
                        SB.Append("<i<hh>> Analyzed Request: <i</hh>>");
                        if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>");
                        }
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        SB.Append(HighlightedRequest);
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                    }
                    else
                    {
                        if (RequestTriggerPresent)
                        {
                            SB.Append("<i<hh>> Interesting part of Analyzed Request: <i</hh>>");
                            if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                            {
                                SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>");
                            }
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append("IronWASP is not able to automatically highlight the interesting section of the Request, you would have to identify it manually.");
                            SB.Append("<i<br>>");
                            SB.Append("IronWASP's Passive Analyzer reported the following text as being of interest in this case:");
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append(GetInterestingTextWrap(SelectedTrigger.RequestTrigger));
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        }
                        else if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<hh>> Information about the Analyzed Request: <i</hh>>");
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>"); SB.Append("<i<br>><i<br>>");
                        }

                    }

                    if (ResponseTriggerHighlighted)
                    {
                        SB.Append("<i<hh>> Analyzed Response: <i</hh>>");
                        if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>");
                        }
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        SB.Append(HighlightedResponse);
                        SB.Append("<i<br>>");
                    }
                    else
                    {
                        if (ResponseTriggerPresent)
                        {
                            SB.Append("<i<hh>> Interesting part of Analyzed Response: <i</hh>>");
                            if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                            {
                                SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>");
                            }
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append("IronWASP is not able to automatically highlight the interesting section of the Response, you would have to identify it manually.");
                            SB.Append("<i<br>>");
                            SB.Append("IronWASP's Passive Analyzer reported the following text as being of interest in this case:");
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append(GetInterestingTextWrap(SelectedTrigger.ResponseTrigger));
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        }
                        else if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<hh>> Information about the Analyzed Response: <i</hh>>");
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>"); SB.Append("<i<br>><i<br>>");
                        }
                    }
                    break;

                default:
                    if (IncludeDesc)
                    {
                        SB.Append("One pair of Request and Response that is associated with this vulnerability is available in the adjacent tabs.");
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        SB.Append(GetHighlightDescription(RequestTriggerPresent, ResponseTriggerPresent));
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                    }
                    if (RequestTriggerHighlighted)
                    {
                        SB.Append("<i<hh>> Associated Request: <i</hh>>");
                        if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>");
                        }
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        SB.Append(HighlightedRequest);
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                    }
                    else
                    {
                        if (RequestTriggerPresent)
                        {
                            SB.Append("<i<hh>> Interesting part of Associated Request: <i</hh>>");
                            if (SelectedTrigger.RequestTriggerDescription.Trim().Length > 0)
                            {
                                SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.RequestTriggerDescription); SB.Append("<i</cb>>");
                            }
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append("IronWASP is not able to automatically highlight the interesting section of the Request, you would have to identify it manually.");
                            SB.Append("<i<br>>");
                            SB.Append("The component that identified this vulnerability reported the following text as being of interest in this case:");
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append(GetInterestingTextWrap(SelectedTrigger.RequestTrigger));
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        }
                    }

                    if (ResponseTriggerHighlighted)
                    {
                        SB.Append("<i<hh>> Associated Response: <i</hh>>");
                        if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>");
                        }
                        SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        SB.Append(HighlightedResponse);
                        SB.Append("<i<br>>");
                    }
                    else
                    {
                        if (ResponseTriggerPresent)
                        {
                            SB.Append("<i<hh>> Interesting part of Associated Response: <i</hh>>");
                            if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                            {
                                SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>");
                            }
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append("IronWASP is not able to automatically highlight the interesting section of the Response, you would have to identify it manually.");
                            SB.Append("<i<br>>");
                            SB.Append("The component that identified this vulnerability reported the following text as being of interest in this case:");
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                            SB.Append(GetInterestingTextWrap(SelectedTrigger.ResponseTrigger));
                            SB.Append("<i<br>>"); SB.Append("<i<br>>");
                        }
                        else if (SelectedTrigger.ResponseTriggerDescription.Trim().Length > 0)
                        {
                            SB.Append("<i<hh>> Information about the Associated Response: <i</hh>>");
                            SB.Append("<i<br>><i<br>>"); SB.Append("<i<cb>>"); SB.Append(SelectedTrigger.ResponseTriggerDescription); SB.Append("<i</cb>>"); SB.Append("<i<br>><i<br>>");
                        }
                    }
                    break;
            }
            return SB.ToString();
        }

        static string GetHighlightDescription(bool RequestTriggerPresent, bool ResponseTriggerPresent)
        {
            if (RequestTriggerPresent && ResponseTriggerPresent)
            {
                return "Below you can see the sections of the Request and Response that are of interest (<i<hlg>> highlighted in green<i</hlg>>). Non-interesting sections have been stripped away for clarity.";
            }
            else if (RequestTriggerPresent)
            {
                return "Below you can see the sections of the Request that are of interest (<i<hlg>> highlighted in green<i</hlg>>). Non-interesting sections have been stripped away for clarity.";
            }
            else if (ResponseTriggerPresent)
            {
                return "Below you can see the sections of the Response that are of interest (<i<hlg>> highlighted in green<i</hlg>>). Non-interesting sections have been stripped away for clarity.";
            }
            else
            {
                return "";
            }
        }

        static string GetInterestingTextWrap(string TriggerValue)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(" <i<cb>>----- START OF INTERESTING TEXT -----<i</cb>> ");
            SB.Append("<i<br>>");
            SB.Append(Tools.EncodeForTrace(TriggerValue));
            SB.Append("<i<br>>");
            SB.Append(" <i<cb>>----- END OF INTERESTING TEXT -----<i</cb>> ");
            return SB.ToString();
        }

        public static string GetRequestTriggerHighlighting(string Trigg, Request Req)
        {
            StringBuilder SB = new StringBuilder();
            string ReqHeader = Req.GetHeadersAsString();
            string ReqBody = Req.BodyString;

            List<string> AllTriggerVariations = new List<string>();
            AllTriggerVariations.Add(Trigg);
            if (!AllTriggerVariations.Contains(Request.PathPartEncode(Trigg))) AllTriggerVariations.Add(Request.PathPartEncode(Trigg));
            if (!AllTriggerVariations.Contains(QueryParameters.Encode(Trigg))) AllTriggerVariations.Add(QueryParameters.Encode(Trigg));
            if (!AllTriggerVariations.Contains(CookieParameters.Encode(Trigg))) AllTriggerVariations.Add(CookieParameters.Encode(Trigg));
            if (!AllTriggerVariations.Contains(HeaderParameters.Encode(Trigg))) AllTriggerVariations.Add(HeaderParameters.Encode(Trigg));

            try
            {
                List<string> HeaderAdjustments = GetHeaderVariations(Trigg, Req.Headers, ReqHeader);
                foreach (string HA in HeaderAdjustments)
                {
                    if (!AllTriggerVariations.Contains(HA))
                    {
                        AllTriggerVariations.Add(HA);
                    }
                }
            }
            catch { }

            List<string> HeaderTriggerVariations = new List<string>();


            foreach (string CurrentVariation in AllTriggerVariations)
            {
                if (!HeaderTriggerVariations.Contains(CurrentVariation) && ReqHeader.Contains(CurrentVariation))
                {
                    HeaderTriggerVariations.Add(CurrentVariation);
                }
            }
            ReqHeader = Highlighter.InsertHighlights(ReqHeader, HeaderTriggerVariations);

            ReqBody = GetRequestBodyHighlighting(ReqBody, Trigg);
            if (!ReqHeader.Contains("<i<hlg>>") && !ReqBody.Contains("<i<hlg>>"))
            {
                foreach (string TriggLine in Tools.SplitLines(Trigg))
                {
                    ReqBody = GetRequestBodyHighlighting(ReqBody, TriggLine);
                }
            }

            SB.Append(Highlighter.SnipHeaderSection(ReqHeader).TrimEnd());
            SB.AppendLine(); SB.AppendLine();
            SB.Append(Highlighter.SnipBodySection(ReqBody));
            return SB.ToString().Replace("\n", "<i<br>>");
        }

        static string GetRequestBodyHighlighting(string ReqBody, string Trigg)
        {
            List<string> AllTriggerVariations = new List<string>();
            AllTriggerVariations.Add(Trigg);
            if (!AllTriggerVariations.Contains(BodyParameters.Encode(Trigg))) AllTriggerVariations.Add(BodyParameters.Encode(Trigg));

            List<string> BodyTriggerVariations = new List<string>();
            foreach (string CurrentVariation in AllTriggerVariations)
            {
                if (!BodyTriggerVariations.Contains(CurrentVariation) && ReqBody.Contains(CurrentVariation))
                {
                    BodyTriggerVariations.Add(CurrentVariation);
                }
            }
            ReqBody = Highlighter.InsertHighlights(ReqBody, BodyTriggerVariations);
            return ReqBody;
        }

        public static string GetResponseTriggerHighlighting(string Trigg, Response Res)
        {
            StringBuilder SB = new StringBuilder();
            string ResHeader = Res.GetHeadersAsString();
            string ResBody = Res.BodyString;

            List<string> AllTriggerVariations = new List<string>();
            AllTriggerVariations.Add(Trigg);
            if (!AllTriggerVariations.Contains(CookieParameters.Encode(Trigg))) AllTriggerVariations.Add(CookieParameters.Encode(Trigg));
            if (!AllTriggerVariations.Contains(HeaderParameters.Encode(Trigg))) AllTriggerVariations.Add(HeaderParameters.Encode(Trigg));

            try
            {
                List<string> HeaderAdjustments = GetHeaderVariations(Trigg, Res.Headers, ResHeader);
                foreach (string HA in HeaderAdjustments)
                {
                    if (!AllTriggerVariations.Contains(HA))
                    {
                        AllTriggerVariations.Add(HA);
                    }
                }
            }
            catch { }

            List<string> HeaderTriggerVariations = new List<string>();
            foreach (string CurrentVariation in AllTriggerVariations)
            {
                if (!HeaderTriggerVariations.Contains(CurrentVariation) && ResHeader.Contains(CurrentVariation))
                {
                    HeaderTriggerVariations.Add(CurrentVariation);
                }
            }
            ResHeader = Highlighter.InsertHighlights(ResHeader, HeaderTriggerVariations);

            ResBody = GetResponseBodyHighlighting(ResBody, Trigg);
            if (!ResHeader.Contains("<i<hlg>>") && !ResBody.Contains("<i<hlg>>"))
            {
                foreach (string TriggLine in Tools.SplitLines(Trigg))
                {
                    ResBody = GetResponseBodyHighlighting(ResBody, TriggLine);
                }
            }

            SB.Append(Highlighter.SnipHeaderSection(ResHeader).TrimEnd());
            SB.AppendLine(); SB.AppendLine();
            SB.Append(Highlighter.SnipBodySection(ResBody));
            return SB.ToString().Replace("\n", "<i<br>>");
        }

        static string GetResponseBodyHighlighting(string ResBody, string Trigg)
        {
            List<string> AllTriggerVariations = new List<string>();
            AllTriggerVariations.Add(Trigg);

            List<string> BodyTriggerVariations = new List<string>();
            foreach (string CurrentVariation in AllTriggerVariations)
            {
                if (!BodyTriggerVariations.Contains(CurrentVariation) && ResBody.Contains(CurrentVariation))
                {
                    BodyTriggerVariations.Add(Trigg);
                }
            }
            ResBody = Highlighter.InsertHighlights(ResBody, BodyTriggerVariations);
            return ResBody;
        }

        public static List<string> GetHeaderVariations(string Trigg, HeaderParameters Headers, string HeaderString)
        {
            List<string> FinalMatches = new List<string>();
            if (Trigg.Contains(":"))
            {
                string[] Parts = Trigg.Split(new char[] { ':' }, 2);
                string TrimmedName = Parts[0].Trim();
                string TrimmedValue = Parts[1].Trim();
                if (TrimmedName.Length > 0)
                {
                    List<string[]> Matches = new List<string[]>();
                    foreach (string Name in Headers.GetNames())
                    {
                        if (Name.Trim().Equals(TrimmedName, StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (string Value in Headers.GetAll(Name))
                            {
                                if (Value.Trim().Equals(TrimmedValue))
                                {
                                    Matches.Add(new string[] { Name, Value });
                                }
                            }
                        }
                    }

                    List<string> Lines = Tools.SplitLines(HeaderString);
                    foreach (string Line in Lines)
                    {
                        foreach (string[] Match in Matches)
                        {
                            string EncodedName = "";
                            string EncodedValue = "";

                            if (Line.StartsWith(Match[0]))
                            {
                                EncodedName = Match[0];
                            }
                            else if (Line.StartsWith(RequestHeaderParameters.Encode(Match[0])))
                            {
                                EncodedName = RequestHeaderParameters.Encode(Match[0]);
                            }
                            else if (Line.StartsWith(ResponseHeaderParameters.Encode(Match[0])))
                            {
                                EncodedName = ResponseHeaderParameters.Encode(Match[0]);
                            }

                            if (Line.EndsWith(Match[1]))
                            {
                                EncodedValue = Match[1];
                            }
                            else if (Line.EndsWith(RequestHeaderParameters.Encode(Match[1])))
                            {
                                EncodedValue = RequestHeaderParameters.Encode(Match[1]);
                            }
                            else if (Line.EndsWith(ResponseHeaderParameters.Encode(Match[1])))
                            {
                                EncodedValue = ResponseHeaderParameters.Encode(Match[1]);
                            }

                            if (EncodedValue.Length > 0)//If EncodedValue is empty then .Replace(EncodedValue, "") throws an exception, as empty value cannot be replaced
                            {
                                if (Line.Substring(EncodedName.Length).Replace(EncodedValue, "").Trim().Equals(":"))
                                {
                                    FinalMatches.Add(Line);
                                }
                            }
                            else
                            {
                                if (Line.Substring(EncodedName.Length).Trim().Equals(":"))
                                {
                                    FinalMatches.Add(Line);
                                }
                            }
                        }
                    }
                }
            }
            return FinalMatches;
        }
    }
}
