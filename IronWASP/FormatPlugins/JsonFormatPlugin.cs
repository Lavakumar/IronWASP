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
using Newtonsoft.Json;
using System.IO;

namespace IronWASP
{
    public class JsonFormatPlugin : FormatPlugin    
    {
        public JsonFormatPlugin()
        {
            this.Name = "JSON";
            this.Version = "0.2";
            this.Description = "Plugin to Convert JSON to XML and XML to JSON. Used in the Scanner section to set Injection points";
            this.FileName = "Internal";
            this.AutoDetect = true;
        }

        public override bool Is(Request Req)
        {
            try
            {
                return Tools.IsJson(Req.BodyString);
            }
            catch { return false; }
        }

        public override bool Is(Response Res)
        {
            try
            {
                return Tools.IsJson(Res.BodyString);
            }
            catch { return false; }
        }

        public override string ToXmlFromRequest(Request Req)
        {
            return this.JsonToXml(Req.BodyString);
        }

        public override Request ToRequestFromXml(Request Req, string IXml)
        {
            Req.BodyString = this.XmlToJson(IXml);
            return Req;
        }

        public override string ToXmlFromResponse(Response Res)
        {
            return this.JsonToXml(Res.BodyString);
        }

        public override Response ToResponseFromXml(Response Res, string IXml)
        {
            Res.BodyString = this.XmlToJson(IXml);
            return Res;
        }

        public string JsonToXml(string JsonIn)
        {
            bool JsonInArray = false;
            JsonIn = JsonIn.Trim();
            if (JsonIn.StartsWith("[") && JsonIn.EndsWith("]"))
            {
                JsonIn = JsonIn.TrimStart('[').TrimEnd(']');
                JsonInArray = true;
            }
            
            StringReader JR = new StringReader(JsonIn);
            JsonTextReader JTR = new JsonTextReader(JR);
            StringWriter SW = new StringWriter();
            XmlTextWriter XW = new XmlTextWriter(SW);
            XW.Formatting = System.Xml.Formatting.Indented;
            Dictionary<int, string> PropertyDict = new Dictionary<int, string>();

            XW.WriteStartElement("xml");
            if (JsonInArray)
            {
                XW.WriteAttributeString("in_array", "1");
            }
            else
            {
                XW.WriteAttributeString("in_array", "0");
            }
            bool Read = true;
            bool NextRead = false;

            while (Read)
            {
                if (!NextRead)
                {
                    Read = JTR.Read();
                }
                NextRead = false;
              
                switch(JTR.TokenType)
                {
                    case (JsonToken.StartConstructor):
                        XW.WriteStartElement("cons");
                        break;

                    case (JsonToken.EndConstructor):
                        XW.WriteEndElement();
                        break;

                    case (JsonToken.PropertyName):
                        if (PropertyDict.ContainsKey(JTR.Depth))
                        {
                            XW.WriteEndElement();
                        }
                        PropertyDict[JTR.Depth] = JTR.Value.ToString();
                        XW.WriteStartElement("prop");
                        XW.WriteAttributeString("oname", JTR.Value.ToString());
                        break;

                    case (JsonToken.Boolean):
                        XW.WriteStartElement("bool");
                        if (JTR.Value != null)
                        {
                            XW.WriteValue(1);
                        }
                        else
                        {
                            XW.WriteValue(0);
                        }
                        XW.WriteEndElement();
                        break;

                    case(JsonToken.Float):
                    case(JsonToken.Integer):
                    case(JsonToken.Date):
                        XW.WriteStartElement("num");
                        XW.WriteValue(JTR.Value.ToString());
                        XW.WriteEndElement();
                        break;

                    case (JsonToken.String):
                        XW.WriteStartElement("str");
                        XW.WriteValue(JTR.Value.ToString());
                        XW.WriteEndElement();
                        break;

                    case (JsonToken.Null):
                        XW.WriteStartElement("undef");
                        XW.WriteValue("null");
                        XW.WriteEndElement();
                        break;

                    case (JsonToken.StartArray):
                        XW.WriteStartElement("arr");
                        Read = JTR.Read();
                        NextRead = true;
                        if (JTR.TokenType == JsonToken.EndArray)
                        {
                            XW.WriteValue("");
                        }
                        break;

                    case (JsonToken.EndArray):
                        XW.WriteEndElement();
                        break;

                    case (JsonToken.StartObject):
                        XW.WriteStartElement("obj");
                        Read = JTR.Read();
                        NextRead = true;
                        if (JTR.TokenType == JsonToken.EndObject)
                        {
                            XW.WriteValue("");
                        }
                        break;

                    case (JsonToken.EndObject):
                        bool PropertyNameFound = false;
                        List<int> pd_keys = new List<int>(PropertyDict.Keys);
                        foreach( int k in pd_keys)
                        {
                            if (k > JTR.Depth)
                            {
                                PropertyNameFound = true;
                                PropertyDict.Remove(k);
                            }
                        }
                        if (PropertyNameFound)
                        {
                            XW.WriteEndElement();
                        }
                        XW.WriteEndElement();
                        if (JTR.Depth == 0)
                        {
                            Read = false;
                        }
                        break;
                }
            }

            XW.WriteEndElement();
            XW.Close();
            return SW.ToString().Trim();
        }


        public string XmlToJson(string Xml)
        {
            StringWriter JW = new StringWriter();
            JsonTextWriter JTW = new JsonTextWriter(JW);
            JTW.Formatting = Newtonsoft.Json.Formatting.Indented;
            StringReader XSR = new StringReader(Xml.Trim());
            XmlReader XR = XmlReader.Create(XSR);

            string ValType = "";
            bool InArray = false;

            XR.Read();
            if (!(XR.NodeType == XmlNodeType.Element && XR.Name == "xml"))
            {
                throw new  Exception("Invalid XML Input");
            }
            bool Read = true;
            bool NextRead = false;

            try
            {
                if (XR.GetAttribute("in_array") == "1") InArray = true;
            }
            catch{}
    
            while (Read)
            {
                if (! NextRead)
                {
                    Read = XR.Read();
                }
                NextRead = false;
                switch(XR.NodeType)
                {
                    case (XmlNodeType.Element):
                        switch(XR.Name)
                        {
                            case("obj"):
                                JTW.WriteStartObject();
                                break;
                            case ("arr"):
                                JTW.WriteStartArray();
                                break;
                            case("cons" ):
                                JTW.WriteStartConstructor("");
                                break;
                            case ("num"):
                            case ("str"):
                            case ("bool"):
                            case ("undef" ):
                                ValType = XR.Name;
                                Read = XR.Read();
                                NextRead = true;
                                if (XR.NodeType == XmlNodeType.EndElement)
                                {
                                    JTW.WriteValue("");
                                }
                                break;
                            case("prop"):
                                JTW.WritePropertyName(XR.GetAttribute("oname"));
                                break;
                        }
                        break;
                    case (XmlNodeType.EndElement ):
                        switch(XR.Name)
                        {
                            case("obj"):
                                JTW.WriteEndObject();
                                break;
                            case("arr"):
                                JTW.WriteEndArray();
                                break;
                            case("cons" ):
                                JTW.WriteEndConstructor();
                                break;
                        }
                        break;
                    case(XmlNodeType.Text):
                        switch(ValType)
                        {
                            case("num"):
                                try
                                {
                                    JTW.WriteValue(Int32.Parse(XR.Value.Trim()));
                                }
                                catch
                                {
                                    try
                                    {
                                        JTW.WriteValue(float.Parse(XR.Value.Trim()));
                                    }
                                    catch
                                    {
                                        JTW.WriteValue(XR.Value);
                                    }
                                }
                                break;
                            case("str"):
                                JTW.WriteValue(XR.Value.ToString());
                                break;
                            case("bool"):
                                if (XR.Value.ToString().Equals("1"))
                                {
                                    JTW.WriteValue(true);
                                }
                                else if(XR.Value.ToString().Equals("0"))
                                {
                                    JTW.WriteValue(false);
                                }
                                else
                                {
                                    JTW.WriteValue(XR.Value);
                                }
                                break;
                            case("undef"):
                                if (XR.Value.ToString() == "null")
                                {
                                    JTW.WriteNull();
                                }
                                else
                                {
                                    JTW.WriteValue(XR.Value.ToString());
                                }
                                break;
                        }
                        break;
                }
            }
            JTW.Close();
            if (InArray)
            {
                return string.Format("[{0}]",JW.ToString().Trim());
            }
            else
            {
                return JW.ToString().Trim();
            }
        }
    }
}
