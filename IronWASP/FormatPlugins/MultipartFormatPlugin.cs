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
    public class MultipartFormatPlugin : FormatPlugin
    {
        public MultipartFormatPlugin()
        {
            this.Name = "MultiPart";
            this.Version = "0.5";
            this.Description = "Plugin to parse XML and provide the Injection points";
            this.FileName = "Internal";
            this.AutoDetect = true;
        }

        public override bool Is(Request Req)
        {
            try
            {
                if (Req.Headers.Has("Content-Type"))
                {
                    if (Req.Headers.Get("Content-Type").Trim().StartsWith("multipart", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }

        public override bool Is(Response Res)
        {
            try
            {
                if (Res.Headers.Has("Content-Type"))
                {
                    if (Res.Headers.Get("Content-Type").Trim().StartsWith("multipart", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }

        public override string ToXmlFromRequest(Request Req)
        {
            return this.MultiPartToXml(Req.Headers.Get("Content-Type"), Req.BodyArray);
        }

        public override Request ToRequestFromXml(Request Req, string IXml)
        {
            Req.BodyArray = this.XmlToMultiPart(Req.Headers.Get("Content-Type"), IXml);
            return Req;
        }

        public override string ToXmlFromResponse(Response Res)
        {
            return this.MultiPartToXml(Res.Headers.Get("Content-Type"), Res.BodyArray);
        }

        public override Response ToResponseFromXml(Response Res, string IXml)
        {
            Res.BodyArray = this.XmlToMultiPart(Res.Headers.Get("Content-Type"), IXml);
            return Res;
        }

        string CheckBoundary(byte[] BoundaryArray, byte[] Body, int Index)
        {
            for(int i=0; i < BoundaryArray.Length; i++)
		    {
			    if(BoundaryArray[i] != Body[Index + i])
			    {
				    return "No";
			    }
		    }
		    byte[] EndBa = new byte[2];
            EndBa[0] = Body[Index + BoundaryArray.Length];
            EndBa[1] = Body[Index + BoundaryArray.Length + 1];

            if (EndBa[0] == 13 && EndBa[1] == 10)//\r\n
            {
                return "Bound";
            }
            else if (EndBa[0] == 45 && EndBa[1] == 45)//--
            {
                return "End";
            }
            else
            {
                return "No";
            }
        }

        byte[] GetBoundaryArray(string Header)
        {
            string BSignStr = "--" + Header.Split(new string[]{"boundary="}, StringSplitOptions.None)[1];
		    byte[] BSignBytes = Encoding.UTF8.GetBytes(BSignStr);
            return BSignBytes;
        }

        public string MultiPartToXml(string Header, byte[] Body)
        {
            int BoundaryLength = Header.Split(new string[]{"boundary="}, StringSplitOptions.None)[1].Length + 4;//the +4 is to handle -- at the beginning and \r\n at the end
		    byte[] BoundaryArray = this.GetBoundaryArray(Header);

    		int i = 0;
		    List<int> BoundaryPoints = new List<int>();
		    while (i < Body.Length)
		    {
			    string CheckResult = this.CheckBoundary(BoundaryArray, Body, i);
			    if (CheckResult == "Bound")
			    {
				    BoundaryPoints.Add(i);
				    i = i + BoundaryLength;
			    }
			    else if(CheckResult == "End")
			    {
				    BoundaryPoints.Add(i);
				    i = Body.Length;
			    }
			    else
			    {
				    i = i + 1;
			    }
		    }
		    List<byte[]> BAParts = new List<byte[]>();
		    int StartPoint = 0;
            int EndPoint = 0;
		    for(i=0; i < BoundaryPoints.Count; i ++)
		    {
                if (i == BoundaryPoints.Count - 1)
			    {
				    //this is the just the trailer part so nothing to add here
			    }
			    else
			    {
                    StartPoint = BoundaryPoints[i] + BoundaryLength;
				    EndPoint = BoundaryPoints[i + 1] - 2;//-2 to account for the \r\n at the end
				    byte[] Part = new byte[EndPoint - StartPoint];
                    
                    Array.Copy(Body, StartPoint, Part, 0, Part.Length);
				    BAParts.Add(Part);
			    }
		    }

		    StringBuilder XB = new StringBuilder();
		    XmlWriterSettings Settings = new XmlWriterSettings();
		    Settings.Indent = true;
		    XmlWriter XW = XmlWriter.Create(XB, Settings);

		    XW.WriteStartElement("xml");
		    foreach(byte[] BA in BAParts)
		    {
			    this.GetXml(BA, XW);
		    }
		    XW.WriteEndElement();
		    XW.Close();
            return XB.ToString().Split(new string[]{"?>"}, StringSplitOptions.None)[1];
        }

        XmlWriter GetXml(byte[] SectionBytes, XmlWriter XW)
        {
            string SectionName = "";
            XW.WriteStartElement("section");

		    string SectionString = Encoding.UTF8.GetString(SectionBytes);
		    string[] SectionParts = SectionString.Split(new string[]{"\r\n\r\n"}, 2, StringSplitOptions.None);
		    string[] SectionHeaders = SectionParts[0].Split(new string[]{"\r\n"}, StringSplitOptions.None);

            int HeaderCount = 0;
            if (SectionHeaders.Length == 0)
            {
                XW.WriteStartElement("section_value");
                XW.WriteAttributeString("oname", "value");
                byte[] BinaryData = new byte[SectionBytes.Length - (SectionParts[0].Length + 4)];//#don't use len(parts[1]) as binary values would have incorrect length in the string form
                Array.Copy(SectionBytes, SectionParts[0].Length + 4, BinaryData, 0, BinaryData.Length);

                if (Tools.IsBinary(BinaryData))
                {
                    XW.WriteAttributeString("encoded", "1");
                    XW.WriteValue(Tools.Base64EncodeByteArray(BinaryData));
                }
                else
                {
                    if (SectionParts[1].Contains("\r\n"))
                    {
                        XW.WriteAttributeString("crlf", "1");
                    }
                    XW.WriteValue(SectionParts[1]);
                }
                XW.WriteEndElement();//Closing the <section_value> tag
            }
            else
            {
                foreach (string FullHeader in SectionHeaders)
                {
                    HeaderCount++;

                    string[] HeaderParts = FullHeader.Split(new string[] { ";" }, StringSplitOptions.None);
                    string[] HeaderFirstPart = HeaderParts[0].Split(new string[] { ":" }, StringSplitOptions.None);

                    List<string[]> HeaderPartNameValuePairs = new List<string[]>();

                    for (int i = 1; i < HeaderParts.Length; i++)
                    {
                        string[] KeyValuePair = HeaderParts[i].Split(new string[] { "=" }, StringSplitOptions.None);
                        KeyValuePair[0] = KeyValuePair[0].Trim();
                        KeyValuePair[1] = KeyValuePair[1].Trim();
                        if (KeyValuePair[1].StartsWith("\"") && KeyValuePair[1].EndsWith("\""))
                        {
                            KeyValuePair[1] = KeyValuePair[1].Trim('"');
                        }
                        else if (KeyValuePair[1].StartsWith("'") && KeyValuePair[1].EndsWith("'"))
                        {
                            KeyValuePair[1] = KeyValuePair[1].Trim('\'');
                        }
                        HeaderPartNameValuePairs.Add(KeyValuePair);
                    }
                    if (HeaderCount == 1)
                    {
                        XW.WriteStartElement("first_header");
                        XW.WriteAttributeString("header_name", HeaderFirstPart[0].Trim());
                        XW.WriteAttributeString("oname", Tools.UrlDecode(HeaderFirstPart[1].Trim()));

                        foreach (string[] KeyValuePair in HeaderPartNameValuePairs)
                        {
                            if (KeyValuePair[0] == "name")
                            {
                                SectionName = Tools.UrlDecode(KeyValuePair[1]);
                            }
                        }
                        if (SectionName.Length > 0)
                        {
                            XW.WriteAttributeString("name", SectionName);
                        }
                        foreach (string[] KeyValuePair in HeaderPartNameValuePairs)
                        {
                            if (KeyValuePair[0] != "name")
                            {
                                XW.WriteStartElement("first_header_section");
                                XW.WriteAttributeString("oname", Tools.UrlDecode(KeyValuePair[0]));
                                XW.WriteValue(Tools.UrlDecode(KeyValuePair[1]));
                                XW.WriteEndElement();
                            }
                        }

                        XW.WriteStartElement("section_value");
                        if (SectionName.Length > 0)
                        {
                            XW.WriteAttributeString("oname", SectionName);
                        }
                        byte[] BinaryData = new byte[SectionBytes.Length - (SectionParts[0].Length + 4)];//#don't use len(parts[1]) as binary values would have incorrect length in the string form
                        Array.Copy(SectionBytes, SectionParts[0].Length + 4, BinaryData, 0, BinaryData.Length);

                        if (Tools.IsBinary(BinaryData))
                        {
                            XW.WriteAttributeString("encoded", "1");
                            XW.WriteValue(Tools.Base64EncodeByteArray(BinaryData));
                        }
                        else
                        {
                            if (SectionParts[1].Contains("\r\n"))
                            {
                                XW.WriteAttributeString("crlf", "1");
                            }
                            XW.WriteValue(SectionParts[1]);
                        }
                        XW.WriteEndElement();//Closing the <section_value> tag
                    }
                    else
                    {
                        if (HeaderCount == 2)
                        {
                            XW.WriteStartElement("other_headers");
                            if (SectionName.Length > 0)
                            {
                                XW.WriteAttributeString("oname", SectionName);
                            }
                        }
                        XW.WriteStartElement("header");
                        XW.WriteAttributeString("oname", HeaderFirstPart[0].Trim());

                        XW.WriteStartElement("value");
                        XW.WriteValue(HeaderFirstPart[1]);
                        XW.WriteEndElement();//Closing the <value> tag
                        foreach (string[] KeyValuePair in HeaderPartNameValuePairs)
                        {
                            XW.WriteStartElement("header_section");
                            XW.WriteAttributeString("oname", Tools.UrlDecode(KeyValuePair[0]));
                            XW.WriteValue(Tools.UrlDecode(KeyValuePair[1]));
                            XW.WriteEndElement();//Closing the <header_section> tag
                        }
                        XW.WriteEndElement();//Closing the <header> tag
                    }
                }
                if (HeaderCount > 1)
                {
                    XW.WriteEndElement();//Closing the <other_headers> tag
                }
                if (HeaderCount > 0)
                {
                    XW.WriteEndElement();//Closing the <first_header> tag
                }
            }
            
            XW.WriteEndElement();//Closing the <section> tag
            return XW;
        }

        public byte[] XmlToMultiPart(string Header, string Xml)
        {
            string Boundary = "--" + Header.Split(new string[]{"boundary="}, StringSplitOptions.None)[1];
		    List<byte> BodyList = new List<byte>();
		
		    XmlDocument XDoc = new XmlDocument();
            XDoc.XmlResolver = null;
		    XDoc.LoadXml(Xml);
		
		    XmlNodeList Sections = XDoc.SelectNodes("/xml/section");
			
		    foreach(XmlNode SectionNode in Sections)
		    {
			    BodyList.AddRange(Encoding.UTF8.GetBytes(Boundary + "\r\n"));
                
                XmlNodeList FirstHeaderNodes = SectionNode.SelectNodes("./first_header");
                string FirstHeaderName = "";
                string FirstHeaderFirstValue = "";
                string SectionName = "";
                StringBuilder HB = new StringBuilder();
                if (FirstHeaderNodes != null && FirstHeaderNodes.Count > 0)
                {
                    FirstHeaderName = FirstHeaderNodes[0].Attributes["header_name"].Value;
                    FirstHeaderFirstValue = FirstHeaderNodes[0].Attributes["oname"].Value;
                    if (FirstHeaderNodes[0].Attributes["name"] != null)
                    {
                        SectionName = FirstHeaderNodes[0].Attributes["name"].Value;
                    }
                    HB.Append(FirstHeaderName); HB.Append(": "); HB.Append(FirstHeaderFirstValue);
                    
                    if (SectionName.Length > 0)
                    {
                        HB.Append("; "); HB.Append("name=\""); HB.Append(Tools.UrlEncode(SectionName)); HB.Append("\"");
                    }
                    XmlNodeList FirstHeaderSectionNodes = FirstHeaderNodes[0].SelectNodes("./first_header_section");
                    if (FirstHeaderSectionNodes != null)
                    {
                        foreach (XmlNode FirstHeaderSectionNode in FirstHeaderSectionNodes)
                        {
                            HB.Append("; "); HB.Append(Tools.UrlEncode(FirstHeaderSectionNode.Attributes["oname"].Value)); HB.Append("=\""); HB.Append(Tools.UrlEncode(FirstHeaderSectionNode.InnerText)); HB.Append("\"");
                        }
                    }
                    HB.AppendLine();
                    XmlNodeList OtherHeaderNodes = FirstHeaderNodes[0].SelectNodes("./other_headers/header");
                    if (OtherHeaderNodes != null)
                    {
                        foreach (XmlNode OtherHeadderNode in OtherHeaderNodes)
                        {
                            HB.Append(OtherHeadderNode.Attributes["oname"].Value); HB.Append(": ");
                            XmlNodeList ValueNodes = OtherHeadderNode.SelectNodes("./value");
                            if (ValueNodes != null && ValueNodes.Count > 0)
                            {
                                HB.Append(ValueNodes[0].InnerText);
                            }
                            XmlNodeList HeaderSections = OtherHeadderNode.SelectNodes("./header_section");
                            if (HeaderSections != null)
                            {
                                foreach (XmlNode HeaderSection in HeaderSections)
                                {
                                    HB.Append("; "); HB.Append(Tools.UrlEncode(HeaderSection.Attributes["oname"].Value)); HB.Append("=\""); HB.Append(Tools.UrlEncode(HeaderSection.InnerText)); HB.Append("\"");
                                }
                            }
                            HB.AppendLine();
                        }
                    }
                }
                HB.AppendLine();
                BodyList.AddRange(Encoding.UTF8.GetBytes(HB.ToString()));

                XmlNodeList SectionValueNodes = SectionNode.SelectNodes("./first_header/section_value");
                if (SectionValueNodes != null && SectionValueNodes.Count > 0)
                {
                    bool CrlfAttributeSet = false;
                    if (SectionValueNodes[0].Attributes["crlf"] != null && SectionValueNodes[0].Attributes["crlf"].Value == "1")
                    {
                        CrlfAttributeSet = true;
                    }
                    string InnerText = SectionValueNodes[0].InnerText;
                    if (CrlfAttributeSet)
                    {
                        InnerText = InnerText.Replace("\n", "\r\n");
                    }
                    if (SectionValueNodes[0].Attributes["encoded"] != null && SectionValueNodes[0].Attributes["encoded"].Value == "1")
                    {
                        BodyList.AddRange(Tools.Base64DecodeToByteArray(InnerText));
                    }
                    else
                    {
                        BodyList.AddRange(Encoding.UTF8.GetBytes(InnerText));
                    }
                }			    
                BodyList.AddRange(Encoding.UTF8.GetBytes("\r\n"));
		    }
		    BodyList.AddRange(Encoding.UTF8.GetBytes(Boundary + "--\r\n"));
            return BodyList.ToArray();
        }
    }
}
