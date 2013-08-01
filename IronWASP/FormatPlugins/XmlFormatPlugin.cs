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
    public class XmlFormatPlugin : FormatPlugin
    {
        public XmlFormatPlugin()
        {
            this.Name = "XML";
            this.Version = "0.5";
            this.Description = "Plugin to parse XML and provide the Injection points";
            this.FileName = "Internal";
            this.AutoDetect = true;
        }

        public override bool Is(Request Req)
        {
            try
            {
                return Tools.IsXml(Req.BodyString.Trim());
            }
            catch { return false; }
        }

        public override bool Is(Response Res)
        {
            try
            {
                return Tools.IsXml(Res.BodyString.Trim());
            }
            catch { return false; }
        }

        public override string ToXmlFromRequest(Request Req)
        {
            return this.OriginalXmlToIronWASPXml(Req.BodyString);
        }

        public override Request ToRequestFromXml(Request Req, string IXml)
        {
            string Dec = this.GetXmlDeclaration(Req.BodyString);
            string NewXml = this.IronWASPXmlToOriginalXml(IXml);
            if(Dec.Length > 0)
            {
                Req.BodyString = string.Format("<?xml {0}?>{1}", Dec, NewXml);
            }
            else
            {
                Req.BodyString = NewXml;
            }
            return Req;
        }

        public override string ToXmlFromResponse(Response Res)
        {
            return this.OriginalXmlToIronWASPXml(Res.BodyString);
        }

        public override Response ToResponseFromXml(Response Res, string IXml)
        {
            string Dec = this.GetXmlDeclaration(Res.BodyString);
            string NewXml = this.IronWASPXmlToOriginalXml(IXml);
            if (Dec.Length > 0)
            {
                Res.BodyString = string.Format("<?xml {0}?>{1}", Dec, NewXml);
            }
            else
            {
                Res.BodyString = NewXml;
            }
            return Res;
        }

        public string OriginalXmlToIronWASPXml(string OXml)
        {
            XmlDocument ODoc = new XmlDocument();
            ODoc.XmlResolver = null;
            ODoc.LoadXml(OXml.Trim());
            StringWriter SW = new StringWriter();
            XmlTextWriter XW = new XmlTextWriter(SW);
            XW.Formatting = Formatting.Indented;
            XW.WriteStartElement("xml");
            this.OtoiReadNode(ODoc.DocumentElement, XW);
            XW.WriteEndElement();

            XW.Close();
            SW.Close();
            return SW.ToString().Trim();
        }

        void OtoiReadNode(XmlNode Node, XmlWriter XW)
        {
            if (Node.NodeType == XmlNodeType.Element)
            {
                XW.WriteStartElement("element");
                XW.WriteStartAttribute("oname"); XW.WriteValue(Node.Name); XW.WriteEndAttribute();
                XW.WriteStartAttribute("local_name"); XW.WriteValue(Node.LocalName); XW.WriteEndAttribute();
                XW.WriteStartAttribute("prefix"); XW.WriteValue(Node.Prefix); XW.WriteEndAttribute();
                XW.WriteStartAttribute("namespace"); XW.WriteValue(Node.NamespaceURI); XW.WriteEndAttribute();

                if (Node.Attributes.Count > 0)
                {
                    bool IsNonNameSpaceAttributesAvailable = false;

                    foreach (XmlAttribute XA in Node.Attributes)
                    {
                        if (!(XA.Name == "xmlns" || XA.Name.StartsWith("xmlns:")))
                        {
                            IsNonNameSpaceAttributesAvailable = true;
                            break;
                        }
                    }
                    if (IsNonNameSpaceAttributesAvailable)
                    {
                        XW.WriteStartElement("attributes");
                        foreach (XmlAttribute XA in Node.Attributes)
                        {
                            if (!(XA.Name == "xmlns" || XA.Name.StartsWith("xmlns:")))
                            {
                                XW.WriteStartElement("attribute");
                                XW.WriteStartAttribute("oname"); XW.WriteValue(XA.Name); XW.WriteEndAttribute();
                                XW.WriteStartAttribute("local_name"); XW.WriteValue(XA.LocalName); XW.WriteEndAttribute();
                                XW.WriteStartAttribute("prefix"); XW.WriteValue(XA.Prefix); XW.WriteEndAttribute();
                                XW.WriteValue(XA.Value);
                                XW.WriteEndElement();
                            }
                        }
                        XW.WriteEndElement();
                    }
                }
                if (Node.HasChildNodes)
                {
                    if (Node.ChildNodes.Count == 1 && Node.ChildNodes[0].NodeType == XmlNodeType.Text)
                    {
                        XW.WriteStartElement("value"); XW.WriteValue(Node.ChildNodes[0].InnerText); XW.WriteEndElement();
                    }
                    else
                    {
                        XW.WriteStartElement("children");
                        foreach (XmlNode ChildNode in Node.ChildNodes)
                        {
                            OtoiReadNode(ChildNode, XW);
                        }
                        XW.WriteEndElement();
                    }
                }
                else
                {
                    XW.WriteStartElement("value");
                    if (!Node.OuterXml.Contains("</"))//check if this is an empty tag so that it can be reproduced the right way
                    {
                        XW.WriteStartAttribute("empty_tag"); XW.WriteValue("1"); XW.WriteEndAttribute();
                    }
                    XW.WriteValue(""); XW.WriteEndElement();
                }
                XW.WriteEndElement();
            }
            else if (Node.NodeType == XmlNodeType.Comment)
            {
                XW.WriteStartElement("comment");
                XW.WriteAttributeString("oname", "Comment");
                XW.WriteValue(Node.Value);
                XW.WriteEndElement();
            }
        }

        public string IronWASPXmlToOriginalXml(string IXml)
        {
            XmlDocument IDoc = new XmlDocument();
            IDoc.XmlResolver = null;
            IDoc.LoadXml(IXml.Trim());

            StringBuilder SB = new StringBuilder();
            XmlWriterSettings Settings = new XmlWriterSettings();
            Settings.Indent = true;
            Settings.NewLineHandling = NewLineHandling.None;
            XmlWriter XW = XmlWriter.Create(SB, Settings);

            this.ItooReadNode(IDoc.DocumentElement, XW);
            
            XW.Close();
            return SB.ToString().Split(new string[]{"?>"}, StringSplitOptions.None)[1].Trim();
        }

        void ItooReadNode(XmlNode Node, XmlWriter XW)
        {
            if (Node.NodeType == XmlNodeType.Element)
            {
                if (Node.Name == "xml")
                {
                    foreach (XmlNode ChildNode in Node.ChildNodes)
                    {
                        this.ItooReadNode(ChildNode, XW);
                    }
                }
                else if (Node.Name == "comment")
                {
                    XW.WriteComment(Node.InnerText);
                }
                else if (Node.Name == "element")
                {
                    string LocalName = Node.Attributes["local_name"].Value;
                    string Prefix = Node.Attributes["prefix"].Value;
                    string NamespaceURI = Node.Attributes["namespace"].Value;

                    if (Prefix.Length > 0)
                    {
                        XW.WriteStartElement(Prefix, LocalName, NamespaceURI);
                    }
                    else if (NamespaceURI.Length > 0)
                    {
                        XW.WriteStartElement(LocalName, NamespaceURI);
                    }
                    else
                    {
                        XW.WriteStartElement(LocalName);
                    }
                    bool IsEmptyTag = false;
                    try
                    {
                        if (Node.Attributes["empty_tag"].Value == "1")
                        {
                            IsEmptyTag = true;
                        }
                    }
                    catch { }
                    foreach (XmlNode ChildNode in Node.ChildNodes)
                    {
                        if (ChildNode.Name == "attributes")
                        {
                            foreach (XmlNode AttrNode in ChildNode.ChildNodes)
                            {
                                string AttrName = AttrNode.Attributes["local_name"].Value;
                                string AttrPrefix = AttrNode.Attributes["prefix"].Value;

                                if (Prefix.Length > 0)
                                {
                                    XW.WriteStartAttribute(Prefix, AttrName, NamespaceURI);
                                }
                                else
                                {
                                    XW.WriteStartAttribute(AttrName);
                                }
                                XW.WriteValue(AttrNode.InnerText);
                                XW.WriteEndAttribute();
                            }
                        }
                        else if (ChildNode.Name == "children")
                        {
                            foreach (XmlNode ChildElementNode in ChildNode.ChildNodes)
                            {
                                this.ItooReadNode(ChildElementNode, XW);
                            }
                        }
                        else if (ChildNode.Name == "value")
                        {
                            if (ChildNode.InnerText.Length == 0)
                            {
                                if (!IsEmptyTag)
                                {
                                    XW.WriteValue("");//this will force the creation of a closing tag with empty value.
                                }
                            }
                            else
                            {
                                XW.WriteValue(ChildNode.InnerText);
                            }
                        }
                    }
                    XW.WriteEndElement();
                }
            }
        }

        string GetXmlDeclaration(string XML)
        {
            string Declaration = "";
            try
            {
                StringReader SR = new StringReader(XML.Trim());
                XmlReader XR = XmlReader.Create(SR);
                XR.Read();
                if (XR.NodeType == XmlNodeType.XmlDeclaration)
                {
                    Declaration = XR.Value;
                }
                XR.Close();
                SR.Close();
            }
            catch { }
            return Declaration;
        }
    }
}
