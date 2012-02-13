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
using System.Web;
using System.Web.Util;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Security.Cryptography;
using Mathertel;
using Newtonsoft.Json;
using Jint;
using Jint.Expressions;
using Antlr.Runtime;

namespace IronWASP
{
    public class Tools
    {
        public static string ListToCsv(List<string> CSVList)
        {
            if (CSVList.Count == 0) return "";
            StringBuilder CSV = new StringBuilder();
            foreach (string Ele in CSVList)
            {
                CSV.Append(Ele); CSV.Append(",");
            }
            return CSV.ToString().TrimEnd(',');
        }

        public static string ArrayToCsv(string[] CSVArray)
        {
            List<string> CSVList = new List<string>();
            CSVList.AddRange(CSVArray);
            return ListToCsv(CSVList);
        }

        public static string[] ListToArray(List<string> Input)
        {
            string[] Output = new string[Input.Count];
            Input.CopyTo(Output);
            return Output;
        }

        public static List<string> ArrayToList(string[] Input)
        {
            List<string> Output = new List<string>();
            Output.AddRange(Input);
            return Output;
        }

        internal static string RtfSafe(string Text)
        {
            Text = Text.Replace("\\","\\\\");
            Text = Text.Replace("{", "\\{");
            Text = Text.Replace("}", "\\}");
            Text = Text.Replace("<i<br>>", " \\par ");
            Text = Text.Replace("<i<hh>>", "\\cf1\\b ");
            Text = Text.Replace("<i</hh>>", "\\cf0\\b0 ");
            Text = Text.Replace("<i<h>>", " \\cf2 \\b ");
            Text = Text.Replace("<i</h>>", " \\cf0 \\b0 ");
            Text = Text.Replace("<i<b>>", " \\b ");
            Text = Text.Replace("<i</b>>", " \\b0 ");
            Text = Text.Replace("<i<i>>", " \\i ");
            Text = Text.Replace("<i</i>>", " \\i0 ");
            Text = Text.Replace("<i<cr>>", " \\cf3 ");
            Text = Text.Replace("<i</cr>>", " \\cf0 ");
            Text = Text.Replace("<i<cg>>", " \\cf4 ");
            Text = Text.Replace("<i</cg>>", " \\cf0 ");
            Text = Text.Replace("<i<cb>>", " \\cf1 ");
            Text = Text.Replace("<i</cb>>", " \\cf0 ");
            Text = Text.Replace("<i<ac>>", " \\qc ");
            Text = Text.Replace("<i</ac>>", " \\pard ");
            Text = Text.Replace("<i<ar>>", " \\qr ");
            Text = Text.Replace("<i</ar>>", " \\pard ");
            Text = Text.Replace("<i<al>>", " \\ql ");
            Text = Text.Replace("<i</al>>", " \\pard ");
            Text = Text.Replace("<i<hlr>>", " \\highlight3 ");
            Text = Text.Replace("<i</hlr>>", " \\highlight0 ");
            Text = Text.Replace("<i<hlg>>", " \\highlight4 ");
            Text = Text.Replace("<i</hlg>>", " \\highlight0 ");
            Text = Text.Replace("<i<hlb>>", " \\highlight1 ");
            Text = Text.Replace("<i</hlb>>", " \\highlight0 ");
            Text = Text.Replace("<i<hlo>>", " \\highlight2 ");
            Text = Text.Replace("<i</hlo>>", " \\highlight0 ");
            return Text;
        }
        public static string RelaxedUrlEncode(string Input)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < Input.Length; i++)
            {
                switch (Input[i])
                {
                    case ' ':
                        SB.Append("+");
                        break;
                    case '&':
                        SB.Append("%26");
                        break;
                    case '?':
                        SB.Append("%3f");
                        break;
                    case '#':
                        SB.Append("%23");
                        break;
                    case ';':
                        SB.Append("%3b");
                        break;
                    case '=':
                        SB.Append("%3d");
                        break;
                    default:
                        SB.Append(Input[i]);
                        break;
                }
            }
            return SB.ToString();
        }

        public static string UrlEncode(string Input)
        {
            return HttpUtility.UrlEncode(Input);
        }
        public static string UrlDecode(string Input)
        {
            return HttpUtility.UrlDecode(Input);
        }
        public static string UrlPathEncode(string Input)
        {
            return HttpUtility.UrlPathEncode(Input);
        }
        public static string HtmlEncode(string Input)
        {
            return HttpUtility.HtmlEncode(Input);
        }
        public static string HtmlDecode(string Input)
        {
            return HttpUtility.HtmlDecode(Input);
        }
        public static string HtmlAttributeEncode(string Input)
        {
            return HttpUtility.HtmlAttributeEncode(Input);
        }
        public static string UrlUnicodeEncode(string Input)
        {
            return HttpUtility.UrlEncodeUnicode(Input);
        }
        public static string ToHex(string Input)
        {
            byte[] BA = Encoding.UTF8.GetBytes(Input);
            return ToHex(BA);
        }
 
        public static string ToHex(byte[] Input)
        {
            string Hex = BitConverter.ToString(Input);
            return Hex.Replace("-","");
        }

        public static byte[] ToByteArray(string Input)
        {
            return ToByteArray(Input, "UTF-8");
        }

        public static byte[] ToByteArray(string Input, string EncodingType)
        {
            Encoding Enc;
            try
            {
                Enc = Encoding.GetEncoding(EncodingType);
            }
            catch
            {
                throw new Exception("Invalid Encoding Type Specified");
            }
            return Enc.GetBytes(Input);
        }

        public static string HexEncode(string Input)
        {
            return HexEncode(Tools.ToByteArray(Input));
        }

        public static string HexEncode(byte[] Input)
        {
            string Hex = BitConverter.ToString(Input);
            if (Hex.Length == 0) return "";
            return string.Format("%{0}", new object[] { Hex.Replace("-", "%") });
        }

        public static string HexDecode(string Input)
        {
            string[] Bytes = Input.Split(new char[]{'%'},StringSplitOptions.RemoveEmptyEntries);
            StringBuilder Result = new StringBuilder();
            foreach (string Byte in Bytes)
            {
                Result.Append(Convert.ToChar(Convert.ToUInt32(Byte, 16)));
            }
            return Result.ToString();
        }

        public static string Base64Encode(string Input)
        {
            byte[] BA = Encoding.UTF8.GetBytes(Input);
            return Base64EncodeByteArray(BA);
        }

        public static string Base64EncodeByteArray(byte[] Input)
        {
            return Convert.ToBase64String(Input);
        }

        public static string XmlEncode(string Input)
        {
            StringBuilder XB = new StringBuilder();
		    XmlWriterSettings Settings = new XmlWriterSettings();
		    XmlWriter XW = XmlWriter.Create(XB, Settings);
            XW.WriteStartElement("xml");
            XW.WriteValue(Input);
            XW.WriteEndElement();
            XW.Close();
            string XML = XB.ToString();
            int StartPoint = XML.IndexOf("<xml>") + 5;
            return XML.Substring(StartPoint, XML.Length - (StartPoint + 6));
        }

        public static byte[] Base64DecodeToByteArray(string Input)
        {
            byte[] BA = Convert.FromBase64String(Input);
            return BA;
        }

        public static string Base64Decode(string Input)
        {
            byte[] BA = Convert.FromBase64String(Input);
            return Encoding.UTF8.GetString(BA);
        }

        public static string Base64DecodeToHex(string Input)
        {
            byte[] BA = Convert.FromBase64String(Input);
            return ToHex(BA);
        }

        public static string sha256(string Input)
        {
            return SHA256(Input);
        }
        
        public static string SHA256(string Input)
        {
            byte[] InBa = ToByteArray(Input);
            SHA256Managed Sha = new SHA256Managed();
            byte[] Result = Sha.ComputeHash(InBa);
            return ToHex(Result);
        }

        public static string sha512(string Input)
        {
            return SHA512(Input);
        }

        public static string SHA512(string Input)
        {
            byte[] InBa = ToByteArray(Input);
            SHA512Managed Sha = new SHA512Managed();
            byte[] Result = Sha.ComputeHash(InBa);
            return ToHex(Result);
        }

        public static string sha1(string Input)
        {
            return SHA1(Input);
        }

        public static string SHA1(string Input)
        {
            byte[] InBa = ToByteArray(Input);
            SHA1Managed Sha = new SHA1Managed();
            byte[] Result = Sha.ComputeHash(InBa);
            return ToHex(Result);
        }

        public static string sha384(string Input)
        {
            return SHA384(Input);
        }

        public static string SHA384(string Input)
        {
            byte[] InBa = ToByteArray(Input);
            SHA384Managed Sha = new SHA384Managed();
            byte[] Result = Sha.ComputeHash(InBa);
            return ToHex(Result);
        }

        public static string md5(string Input)
        {
            return MD5(Input);
        }

        public static string MD5(string Input)
        {
            byte[] InBa = ToByteArray(Input);
            MD5CryptoServiceProvider MD5er = new MD5CryptoServiceProvider();
            byte[] Result = MD5er.ComputeHash(InBa);
            return ToHex(Result);
        }

        public static DiffResult Diff(string Source, string Destination)
        {
            Mathertel.Diff.Item[] Results = Mathertel.Diff.DiffText(Source, Destination, false, false, false);
            return DiffResult.GetStringDiff(Results, Source, Destination);
        }

        public static DiffResult DiffLine(string Source, string Destination)
        {
            Mathertel.Diff.Item[] Results = Mathertel.Diff.DiffText(Source.Replace(' ','\n'), Destination.Replace(' ','\n'), false, false, false);
            return DiffResult.GetLineDiff(Results, Source, Destination);
        }

        public static int DiffLevel(string Source, string Destination)
        {
            return IronDiffer.GetLevel(Source, Destination);
        }

        public static List<String> ScrapeCcNos(string Input)
        {
            return ScrapeCCNos(Input);
        }

        public static List<String> ScrapeCCNos(string Input)
        {
            return Scrape(Input.Replace("-",""), @"\b(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})\b");
        }

        public static List<String> ScrapeEmailIds(string Input)
        {
            return Scrape(Input, @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b");
        }

        public static List<String> Scrape(string Input, string RegexString)
        {
            List<String> Results = new List<string>();
            Regex Regex = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match M = Regex.Match(Input);
            while (M.Success)
            {
                Results.Add(M.Value);
                M = M.NextMatch();
            }
            return Results;
        }

        public static int Count(string Text, string Keyword)
        {
            if (Keyword.Length == 0 || Text.Length == 0) return 0;
            bool Loop = true;
            int StartIndex = 0;
            int Count = 0;
            while (Loop)
            {
                Loop = false;
                int MatchSpot = Text.IndexOf(Keyword, StartIndex, StringComparison.CurrentCultureIgnoreCase);
                if (MatchSpot >= 0)
                {
                    Count++;
                    if ((MatchSpot + Keyword.Length) < Text.Length)
                    {
                        StartIndex = MatchSpot + 1;
                        Loop = true;
                    }
                }
            }
            return Count;
        }

        public static void Trace(string Source, string Message)
        {
            IronTrace IT = new IronTrace(Source, Message);
            IT.Report();
        }

        public static void ScanTrace(int ScanID, string PluginName, string Section, string Parameter, string Title, string Message)
        {
            IronTrace IT = new IronTrace(ScanID, PluginName, Section, Parameter, Title, Message);
            IT.Report();
        }

        public static string GetRandomString()
        {
            int Min = GetRandomNumber();
            return GetRandomString(Min, GetRandomNumber(Min + GetRandomNumber()));
        }

        public static string GetRandomString(int MinLength, int MaxLength)
        {
            string[] Alphabets = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            int Length = GetRandomNumber(MinLength, MaxLength);
            StringBuilder PS = new StringBuilder();
            for (int i = 0; i < Length; i++)
            {
                PS.Append(Alphabets[GetRandomNumber(25)]);
            }
            return PS.ToString();
        }

        public static int GetRandomNumber()
        {
            Random R = new Random();
            return R.Next();
        }

        public static int GetRandomNumber(int Max)
        {
            Random R = new Random();
            return R.Next(Max + 1);
        }

        public static int GetRandomNumber(int Min, int Max)
        {
            Random R = new Random();
            return R.Next(Min, Max + 1);
        }

        public static bool IsJavaScript(string Text)
        {
            string TrimmedText = Text.Trim();
            if (TrimmedText.StartsWith("<") || TrimmedText.StartsWith("{") || TrimmedText.StartsWith("[") || TrimmedText.EndsWith(">")) 
                return false;

            try
            {
                Jint.Expressions.Program Prog = JintEngine.Compile(Text, false);
                if (Prog.Statements.Count == 0) return false;
            }
            catch { return false; }
            return true;
        }

        public static bool IsJson(string Text)
        {
            string TrimmedText = Text.Trim();
            if ((TrimmedText.StartsWith("{") || TrimmedText.StartsWith("[{")) && (TrimmedText.EndsWith("}") || TrimmedText.EndsWith("}]")))
            {
                try
                {
                    JsonConvert.DeserializeXmlNode(Text);
                }
                catch { return false; }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsXml(string Text)
        {
            string TrimmedText = Text.Trim();
            if (TrimmedText.StartsWith("<") && TrimmedText.EndsWith(">"))
            {
                try
                {
                    XmlDocument Doc = new XmlDocument();
                    Doc.InnerXml = Text;
                }
                catch { return false; }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Shell(string Command)
        {
            try
            {
                string[] Result = new string[]{"",""};
                Process P = new Process();
                try
                {
                    P.StartInfo.FileName = "cmd.exe";
                    P.StartInfo.UseShellExecute = false;
                    P.StartInfo.RedirectStandardInput = true;
                    P.StartInfo.RedirectStandardError = true;
                    P.StartInfo.RedirectStandardOutput = true;
                    P.StartInfo.CreateNoWindow = true;
                    P.Start();
                    P.StandardInput.WriteLine(Command);
                    P.StandardInput.WriteLine("exit");
                    Result[0] = P.StandardOutput.ReadToEnd();
                    Result[1] = P.StandardError.ReadToEnd();
                    P.WaitForExit();
                }
                catch { Result[1] = "Command Execution failed"; }
                finally
                {
                    P.Close();
                }
                return Result[1];
            }
            catch { return "Command Execution failed"; }
        }

        public static void Run(string Executable)
        {
            Process.Start(Executable);
        }

        public static string SyncJsonObjectTypes(string OldJson, string NewJson)
        {
            StringReader NSR = new StringReader(NewJson);
            JsonTextReader New = new JsonTextReader(NSR);
            StringReader OSR = new StringReader(OldJson);
            JsonTextReader Old = new JsonTextReader(OSR);
            StringWriter SW = new StringWriter();
            JsonTextWriter Writer = new JsonTextWriter(SW);
            while (New.Read() && Old.Read())
            {
                if ((Old.TokenType == JsonToken.Integer) && (New.TokenType != JsonToken.Integer))
                {
                    int NewIntValue = 0;
                    if (Int32.TryParse(New.Value.ToString(), out NewIntValue))
                    {
                        Writer.WriteValue(NewIntValue);
                    }
                    else
                    {
                        Writer.WriteValue(New.Value);
                    }
                }
                else if ((Old.TokenType == JsonToken.Float) && (New.TokenType != JsonToken.Float))
                {
                    float NewFloatValue = 0;
                    if (float.TryParse(New.Value.ToString(), out NewFloatValue))
                    {
                        Writer.WriteValue(NewFloatValue);
                    }
                    else
                    {
                        Writer.WriteValue(New.Value);
                    }
                }
                else if ((Old.TokenType == JsonToken.Boolean) && (New.TokenType != JsonToken.Boolean))
                {
                    string Value = New.Value.ToString();
                    if (Value.Equals("true"))
                    {
                        Writer.WriteValue(true);
                    }
                    else if (Value.Equals("false"))
                    {
                        Writer.WriteValue(false);
                    }
                    else
                    {
                        Writer.WriteValue(New.Value);
                    }
                }
                else if ((Old.TokenType == JsonToken.String) && (New.TokenType == JsonToken.Null))
                {
                    Writer.WriteValue("");
                }
                else if ((Old.TokenType == JsonToken.StartArray) && (New.TokenType == JsonToken.String))
                {
                    Old.Read();
                    if (Old.TokenType == JsonToken.EndArray)
                    {
                        Writer.WriteValue(JsonToken.StartArray);
                        if (New.Value.ToString() != "") Writer.WriteValue(New.Value);
                        Writer.WriteValue(JsonToken.EndArray);
                    }
                    else
                    {
                        Writer.WriteValue(New.Value);
                    }
                }
                else
                {
                    if (Old.TokenType != New.TokenType)
                    {
                        Writer.Close();
                        New.Close();
                        Old.Close();
                        return NewJson;
                    }
                    else if (New.TokenType == JsonToken.PropertyName)
                    {
                        if (Old.Value == null || New.Value == null)
                        {
                            Writer.Close();
                            New.Close();
                            Old.Close();
                            return NewJson;
                        }
                        else if (!Old.Value.ToString().Equals(New.Value.ToString()))
                        {
                            Writer.Close();
                            New.Close();
                            Old.Close();
                            return NewJson;
                        }
                    }
                    switch (New.TokenType)
                    {
                        case JsonToken.StartConstructor:
                            Writer.WriteStartConstructor("");
                            break;
                        case JsonToken.EndConstructor:
                            Writer.WriteEndConstructor();
                            break;
                        case JsonToken.StartObject:
                            Writer.WriteStartObject();
                            break;
                        case JsonToken.EndObject:
                            Writer.WriteEndObject();
                            break;
                        case JsonToken.StartArray:
                            Writer.WriteStartArray();
                            break;
                        case JsonToken.EndArray:
                            Writer.WriteEndArray();
                            break;
                        case JsonToken.PropertyName:
                            Writer.WritePropertyName(New.Value.ToString());
                            break;
                        case JsonToken.String:
                        case JsonToken.Integer:
                        case JsonToken.Boolean:
                        case JsonToken.Date:
                        case JsonToken.Null:
                        case JsonToken.Float:
                            Writer.WriteValue(New.Value);
                            break;
                    }
                }
            }
            return SW.ToString();
        }
    }
}
