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
    public class SoapFormatPlugin : FormatPlugin
    {
        public SoapFormatPlugin()
        {
            this.Name = "SOAP";
            this.Version = "0.1";
            this.Description = "Plugin to parse SOAP and provide the Injection points";
            this.FileName = "Internal";
            this.AutoDetect = true;
        }

        public override bool Is(Request Req)
        {
            try
            {
                return Tools.IsSoap(Req.BodyString.Trim());
            }
            catch { return false; }
        }

        public override bool Is(Response Res)
        {
            try
            {
                return Tools.IsSoap(Res.BodyString.Trim());
            }
            catch { return false; }
        }

        public override string ToXmlFromRequest(Request Req)
        {
            return this.SoapToIronWASPXml(Req.BodyString);
        }

        public override Request ToRequestFromXml(Request Req, string IXml)
        {
            string Dec = this.GetXmlDeclaration(Req.BodyString);
            string NewXml = this.IronWASPXmlToSoap(IXml, Req.BodyString);
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
            return this.SoapToIronWASPXml(Res.BodyString);
        }

        public override Response ToResponseFromXml(Response Res, string IXml)
        {
            string Dec = this.GetXmlDeclaration(Res.BodyString);
            string NewXml = this.IronWASPXmlToSoap(IXml, Res.BodyString);
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

        public string SoapToIronWASPXml(string OXml)
        {
            XmlDocument ODoc = new XmlDocument();
            ODoc.XmlResolver = null;
            ODoc.LoadXml(OXml.Trim());
            StringWriter SW = new StringWriter();
            XmlTextWriter XW = new XmlTextWriter(SW);
            XW.Formatting = Formatting.Indented;


            XW.WriteStartElement("Envelope");
            XmlNode HeaderNode = null;
            XmlNode BodyNode = null;
            
            if (ODoc.DocumentElement.ChildNodes.Count == 1)
            {
                BodyNode = ODoc.DocumentElement.ChildNodes[0];
            }
            else if (ODoc.DocumentElement.ChildNodes.Count == 2)
            {
                HeaderNode = ODoc.DocumentElement.ChildNodes[0];
                BodyNode =ODoc.DocumentElement.ChildNodes[1];
            }
            if (HeaderNode != null)
            {
                //XW.WriteStartElement("Header");
                this.StoiReadNode(HeaderNode, XW);
                //XW.WriteEndElement();
            }
            if (BodyNode != null)
            {
                //XW.WriteStartElement("Body");
                this.StoiReadNode(BodyNode, XW);
                //XW.WriteEndElement();
            }
            XW.WriteEndElement();

            XW.Close();
            SW.Close();
            return SW.ToString();
        }

        void StoiReadNode(XmlNode Node, XmlWriter XW)
        {
            if (Node.NodeType == XmlNodeType.Element)
            {
                XW.WriteStartElement(Node.LocalName);
                XW.WriteStartAttribute("oname"); XW.WriteValue(Node.LocalName); XW.WriteEndAttribute();
                if (Node.HasChildNodes)
                {
                    if (Node.ChildNodes.Count == 1 && Node.ChildNodes[0].NodeType == XmlNodeType.Text)
                    {
                        XW.WriteValue(Node.ChildNodes[0].InnerText);
                    }
                    else
                    {
                        foreach (XmlNode ChildNode in Node.ChildNodes)
                        {
                            StoiReadNode(ChildNode, XW);
                        }
                    }
                }
                else
                {
                    if (!Node.OuterXml.Contains("</"))//check if this is an empty tag so that it can be reproduced the right way
                    {
                        XW.WriteStartAttribute("empty_tag"); XW.WriteValue("1"); XW.WriteEndAttribute();
                    }
                    XW.WriteValue("");
                }
                XW.WriteEndElement();
            }
        }

        public string IronWASPXmlToSoap(string IXml, string OXml)
        {
            XmlDocument IDoc = new XmlDocument();
            IDoc.XmlResolver = null;
            IDoc.LoadXml(IXml.Trim());

            XmlDocument ODoc = new XmlDocument();
            ODoc.XmlResolver = null;
            ODoc.LoadXml(OXml.Trim());

            StringBuilder SB = new StringBuilder();
            XmlWriterSettings Settings = new XmlWriterSettings();
            Settings.Indent = true;
            Settings.NewLineHandling = NewLineHandling.None;
            XmlWriter XW = XmlWriter.Create(SB, Settings);
            this.ItosReadNode(IDoc.DocumentElement, ODoc.DocumentElement, XW);
            XW.Close();
            return SB.ToString().Split(new string[]{"?>"}, StringSplitOptions.None)[1];
        }

        void ItosReadNode(XmlNode Node, XmlNode ONode, XmlWriter XW)
        {
            if (Node.NodeType == XmlNodeType.Element)
            {
                if (ONode.Prefix.Length > 0)
                {
                    XW.WriteStartElement(ONode.Prefix, ONode.LocalName, ONode.NamespaceURI);
                }
                else if (ONode.NamespaceURI.Length > 0)
                {
                    XW.WriteStartElement(ONode.LocalName, ONode.NamespaceURI);
                }
                else
                {
                    XW.WriteStartElement(ONode.LocalName);
                }
                foreach (XmlAttribute Attr in ONode.Attributes)
                {
                    if (Attr.Prefix.Length > 0)
                    {
                        XW.WriteStartAttribute(Attr.Prefix, Attr.LocalName, Attr.NamespaceURI);
                    }
                    else if (Attr.NamespaceURI.Length > 0)
                    {
                        XW.WriteStartAttribute(Attr.LocalName, Attr.NamespaceURI);
                    }
                    else
                    {
                        XW.WriteStartAttribute(Attr.LocalName);
                    }
                    XW.WriteValue(Attr.Value);
                    XW.WriteEndAttribute();
                }
                if (Node.HasChildNodes)
                {
                    if (Node.ChildNodes.Count == 1 && Node.ChildNodes[0].NodeType == XmlNodeType.Text)
                    {
                        if (Node.InnerText.Length == 0)
                        {
                            if (Node.Attributes["empty_tag"] != null && Node.Attributes["empty_tag"].Value == "1")
                            {
                                //do nothing here so this tag is written as empty tag
                            }
                            else
                            {
                                XW.WriteValue("");
                            }
                        }
                        else
                        {
                            XW.WriteValue(Node.InnerText);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Node.ChildNodes.Count; i++)
                        {
                            this.ItosReadNode(Node.ChildNodes[i], ONode.ChildNodes[i], XW);
                        }
                    }
                }
                else
                {
                    if (Node.Attributes["empty_tag"] != null && Node.Attributes["empty_tag"].Value == "1")
                    {
                        //do nothing here so this tag is written as empty tag
                    }
                    else
                    {
                        XW.WriteValue("");
                    }
                }
                XW.WriteEndElement();
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
