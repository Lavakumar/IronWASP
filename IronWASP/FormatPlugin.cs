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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace IronWASP
{
    public class FormatPlugin : Plugin
    {

        internal static List<FormatPlugin> Collection = new List<FormatPlugin>();
        string RequestXml = "";
        string RequestHash = "";

        public virtual bool Is(Request Request)
        {
            try
            {
                Request Req = Request.GetClone();
                string XmlString = ToXmlFromRequest(Req);
                if (!Tools.IsXml(XmlString)) return false;
                Request FinalReq = ToRequestFromXml(Req, XmlString);
                if (Req.ToString().Equals(FinalReq.ToString())) return true;
            }
            catch { return false; }
            return false;
        }

        public virtual bool Is(Response Response)
        {
            try
            {
                Response Res = Response.GetClone();
                string XmlString = ToXmlFromResponse(Res);
                if (!Tools.IsXml(XmlString)) return false;
                Response FinalRes = ToResponseFromXml(Res, XmlString);
                if (Res.ToString().Equals(FinalRes.ToString())) return true;
            }
            catch { return false; }
            return false;
        }

        public virtual string ToXmlFromRequest(Request Request)
        {
            return ToXml(Request.BodyArray);
        }

        public virtual Request ToRequestFromXml(Request Request, string XmlString)
        {
            Request.BodyArray = ToObject(XmlString);
            return Request;
        }

        public virtual string ToXmlFromResponse(Response Response)
        {
            return ToXml(Response.BodyArray);
        }

        public virtual Response ToResponseFromXml(Response Response, string XmlString)
        {
            Response.BodyArray = ToObject(XmlString);
            return Response;
        }

        public virtual string ToXml(byte[] ObjectArray)
        {
            return "";
        }

        public virtual byte[] ToObject(string XmlString)
        {
            return new byte[] { };
        }

        public int GetXmlInjectionPointsCount(Request Req)
        {
            this.RequestXml  = this.ToXmlFromRequest(Req);
            this.RequestHash = Tools.MD5(Req.ToString());
            string[,] XmlInjectionArray = XmlToArray(this.RequestXml);
            return XmlInjectionArray.GetLength(0);
        }

        public Request InjectInRequest(Request Req, int InjectionPoint, string Payload)
        {
            string CurrentRequestHash = Tools.MD5(Req.ToString());
            string XML = "";
            if(this.RequestHash.Equals(CurrentRequestHash))
            {
                XML = this.RequestXml;
            }
            else
            {
                XML = this.ToXmlFromRequest(Req);
                this.RequestXml = XML;
                this.RequestHash = CurrentRequestHash;
            }
            string InjectedXml = InjectInXml(this.RequestXml, InjectionPoint, Payload);
            return this.ToRequestFromXml(Req.GetClone(true), InjectedXml);
        }

        public static string[,] XmlToArray(string XML)
        {
            bool HasOname = DoesXmlHaveOname(XML);
            
            List<string> ParameterNames = new List<string>();
            List<string> ParameterValues = new List<string>();
            
            StringBuilder OutXml = new StringBuilder();
            List<string> Paths = new List<string>();
            List<string> PropertyPaths = new List<string>();
            StringReader XMLStringReader = new StringReader(XML.Trim());
            XmlReader Reader = XmlReader.Create(XMLStringReader);
            int ParameterCount = 0;
            bool Read = true;
            bool NextRead = false;
            while (Read)
            {
                if (!NextRead) Read = Reader.Read();
                NextRead = false;
                if (!Read) continue;
                if (Reader.IsStartElement())
                {
                    Paths.Add(Reader.Name);
                    if (HasOname)
                    {
                        if (Reader.HasAttributes)
                        {
                            try
                            {
                                string OriginalName = Reader.GetAttribute("oname");
                                if (OriginalName.Length == 0)
                                {
                                    PropertyPaths.Add(Reader.Name);
                                }
                                else
                                {
                                    PropertyPaths.Add(OriginalName);
                                }
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        PropertyPaths.Add(Reader.Name);
                    }
                    if (Paths.Count > PropertyPaths.Count)
                    {
                        PropertyPaths.Add("");
                    }
                    Read = Reader.Read();
                    if (Reader.NodeType == XmlNodeType.EndElement)
                    {
                        ParameterCount++;
                        string Value = "";
                        ParameterNames.Add(JoinPaths(Paths, PropertyPaths));
                        ParameterValues.Add(Value);
                        int C = Paths.Count;
                        if (C > 0)
                        {
                            Paths.RemoveAt(C - 1);
                            PropertyPaths.RemoveAt(C - 1);
                        }
                    }
                    else
                    {
                        NextRead = true;
                    }
                }
                else
                {
                    if (Reader.NodeType == XmlNodeType.EndElement)
                    {
                        int C = Paths.Count;
                        if (C > 0)
                        {
                            Paths.RemoveAt(C - 1);
                            PropertyPaths.RemoveAt(C - 1);
                        }
                    }
                    else
                    {
                        ParameterCount++;
                        string Value = Reader.Value.Trim();
                        ParameterNames.Add(JoinPaths(Paths, PropertyPaths));
                        ParameterValues.Add(Value);
                    }
                }
            }
            Reader.Close();
            string[,] InjectionPoints = new string[ParameterNames.Count, 2];
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                InjectionPoints[i, 0] = ParameterNames[i];
                InjectionPoints[i, 1] = ParameterValues[i];
            }
            return InjectionPoints;
        }

        static bool DoesXmlHaveOname(string Xml)
        {
            bool HasOname = (Xml.IndexOf("oname", StringComparison.OrdinalIgnoreCase) > -1);

            if (!HasOname) return false;

            
            StringReader XMLStringReader = new StringReader(Xml.Trim());
            XmlReader Reader = XmlReader.Create(XMLStringReader);
            
            bool Read = true;
            bool NextRead = false;
            while (Read)
            {
                if (!NextRead) Read = Reader.Read();
                NextRead = false;
                if (!Read) continue;
                if (Reader.IsStartElement())
                {                    
                    if (Reader.HasAttributes)
                    {
                        try
                        {
                            Reader.GetAttribute("oname");
                            return true;
                        }
                        catch { }
                    }
                    
                    Read = Reader.Read();
                    if (Reader.NodeType != XmlNodeType.EndElement)
                    {
                        NextRead = true;
                    }
                }
            }
            Reader.Close();
            
            return false;
        }

        public static string InjectInXml(string XML, int InjectionPosition, string Payload)
        {
            //StringBuilder OutXml = new StringBuilder();
            StringReader XMLStringReader = new StringReader(XML.Trim());
            XmlReader Reader = XmlReader.Create(XMLStringReader);
            //XmlWriter Writer = XmlWriter.Create(OutXml);
            StringWriter OutXml = new StringWriter();
            XmlTextWriter Writer = new XmlTextWriter(OutXml);
            int ParameterCount = 0;
            bool Read = true;
            bool NextRead = false;
            while (Read)
            {
                if (!NextRead) Read = Reader.Read();
                NextRead = false;
                if (!Read) continue;
                if (Reader.IsStartElement())
                {
                    Writer.WriteStartElement(Reader.Name);
                    if(Reader.HasAttributes) Writer.WriteAttributes(Reader, false);
                    Read = Reader.Read();
                    if (Reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (ParameterCount == InjectionPosition)
                            Writer.WriteString(Payload);
                        else
                            Writer.WriteString("");
                        ParameterCount++;
                        Writer.WriteEndElement();
                    }
                    else
                    {
                        NextRead = true;
                    }
                }
                else
                {
                    if (Reader.NodeType == XmlNodeType.EndElement)
                    {
                        Writer.WriteEndElement();
                    }
                    else
                    {
                        if (ParameterCount == InjectionPosition)
                            Writer.WriteString(Payload);
                        else
                            Writer.WriteString(Reader.Value);
                        ParameterCount++;
                    }
                }
            }
            Reader.Close();
            Writer.Close();
            OutXml.Close();
            //string OutXmlString = OutXml.ToString().Split(new string[] { "?>" }, 2, StringSplitOptions.None)[1];
            return OutXml.ToString();
        }

        private static string JoinPaths(List<string> Paths, List<string> PropertyPaths)
        {
            StringBuilder FullPath = new StringBuilder();
            for (int i=0; i < Paths.Count; i++)
            {
                if (PropertyPaths[i].Length > 0)
                {                   
                    FullPath.Append(PropertyPaths[i]); FullPath.Append(" > ");
                }
            }
            return FullPath.ToString().TrimEnd().TrimEnd('>').TrimEnd();
        }

        public static void Add(FormatPlugin FP)
        {
            if ((FP.Name.Length > 0) && !(FP.Name.Equals("All") || FP.Name.Equals("None")))
            {
                if (!List().Contains(FP.Name))
                {
                    FP.FileName = PluginEngine.FileName;
                    Collection.Add(FP);
                }
            }
        }

        public static List<string> List()
        {
            List<string> Names = new List<string>();
            foreach (FormatPlugin FP in Collection)
            {
                Names.Add(FP.Name);
            }
            return Names;
        }

        public static FormatPlugin Get(string Name)
        {
            foreach (FormatPlugin FP in Collection)
            {
                if (FP.Name.Equals(Name))
                {
                    return FP;
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

        public static List<FormatPlugin> Get(Request Request)
        {
            return Get(Request, new List<string>() { "JSON", "MultiPart", "XML" });
        }

        public static List<FormatPlugin> Get(Request Request, List<string> FormatsToCheckFor)
        {
            List<FormatPlugin> RightPlugins = new List<FormatPlugin>();
            foreach (string Name in List())
            {
                if (!FormatsToCheckFor.Contains(Name)) continue;
                if (Get(Name).Is(Request)) RightPlugins.Add(Get(Name));
            }
            return RightPlugins;
        }

        public static List<FormatPlugin> Get(Response Response)
        {
            List<FormatPlugin> RightPlugins = new List<FormatPlugin>();
            foreach (string Name in List())
            {
                if (Get(Name).Is(Response)) RightPlugins.Add(Get(Name));
            }
            return RightPlugins;
        }

        public static bool IsNormal(Request Request)
        {
            try
            {
                if (Request.BodyLength == 0) return true;
                if (Request.Body.Count == 0) return false;
                string BodyString = Request.BodyString;
                string[] KVs = Request.BodyString.Split('&');
                if (KVs.Length == 0) return false;
                bool EqualFound = false;
                foreach (string KV in KVs)
                {
                    if (KV.Length == 0) return false;
                    if (KV.Contains("=")) EqualFound = true;
                    string[] kv = KV.Split('=');
                    if (kv.Length == 0) return false;
                    if (!Regex.IsMatch(kv[0], @"^[A-Za-z0-9_\-\.]+$")) return false;
                }
                if (!EqualFound) return false;
            }
            catch
            {
                return true;
            }
            return true;
        }
    }
}
