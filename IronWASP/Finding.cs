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
    public class Finding
    {
        internal static Finding CurrentPluginResult=null;
        public int Id=0;
        public string Plugin="";
        public string Title="";
        public string Summary="";
        public string AffectedHost = "";
        public Triggers Triggers = new Triggers();
        public FindingSeverity Severity = FindingSeverity.Low;
        public FindingConfidence Confidence = FindingConfidence.Low;
        public FindingType Type = FindingType.Vulnerability;
        public string Signature="";

        static Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> Signatures = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();

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

                if(IsUnique && AddIfUnique)
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
                    if(AddIfUnique)
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
    }
}
