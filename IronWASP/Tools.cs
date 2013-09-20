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
using System.Web;
using System.Web.Util;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using Mathertel;
using Newtonsoft.Json;
using Jint;
using Jint.Expressions;
using Antlr.Runtime;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


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

        internal static string StripRtfTags(string Text)
        {
            Text = Text.Replace("<i<br>>", "");
            Text = Text.Replace("<i<hh>>", "");
            Text = Text.Replace("<i</hh>>", "");
            Text = Text.Replace("<i<h>>", "");
            Text = Text.Replace("<i</h>>", "");
            Text = Text.Replace("<i<b>>", "");
            Text = Text.Replace("<i</b>>", "");
            Text = Text.Replace("<i<i>>", "");
            Text = Text.Replace("<i</i>>", "");
            Text = Text.Replace("<i<cr>>", "");
            Text = Text.Replace("<i</cr>>", "");
            Text = Text.Replace("<i<cg>>", "");
            Text = Text.Replace("<i</cg>>", "");
            Text = Text.Replace("<i<cb>>", "");
            Text = Text.Replace("<i</cb>>", "");
            Text = Text.Replace("<i<co>>", "");
            Text = Text.Replace("<i</co>>", "");
            Text = Text.Replace("<i<cw>>", "");
            Text = Text.Replace("<i</cw>>", "");
            Text = Text.Replace("<i<cy>>", "");
            Text = Text.Replace("<i</cy>>", "");
            Text = Text.Replace("<i<ac>>", "");
            Text = Text.Replace("<i</ac>>", "");
            Text = Text.Replace("<i<ar>>", "");
            Text = Text.Replace("<i</ar>>", "");
            Text = Text.Replace("<i<al>>", "");
            Text = Text.Replace("<i</al>>", "");
            Text = Text.Replace("<i<hlr>>", "");
            Text = Text.Replace("<i</hlr>>", "");
            Text = Text.Replace("<i<hlg>>", "");
            Text = Text.Replace("<i</hlg>>", "");
            Text = Text.Replace("<i<hlb>>", "");
            Text = Text.Replace("<i</hlb>>", "");
            Text = Text.Replace("<i<hlo>>", "");
            Text = Text.Replace("<i</hlo>>", "");
            Text = Text.Replace("<i<hlw>>", "");
            Text = Text.Replace("<i</hlw>>", "");
            Text = Text.Replace("<i<h1>>", "");
            Text = Text.Replace("<i</h1>>", "");
            return Text;
        }

        internal static string RtfSafe(string Text)
        {
            Text = Text.Replace("\r\n", "<i<br>>");
            Text = Text.Replace("\n", "<i<br>>");
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
            Text = Text.Replace("<i<co>>", " \\cf2 ");
            Text = Text.Replace("<i</co>>", " \\cf0 ");
            Text = Text.Replace("<i<cy>>", " \\cf6 ");
            Text = Text.Replace("<i</cy>>", " \\cf0 ");
            Text = Text.Replace("<i<cw>>", " \\cf5 ");
            Text = Text.Replace("<i</cw>>", " \\cf0 ");
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
            Text = Text.Replace("<i<hly>>", " \\highlight6 ");
            Text = Text.Replace("<i</hly>>", " \\highlight0 ");
            Text = Text.Replace("<i<h1>>", " \\fs30 ");
            Text = Text.Replace("<i</h1>>", " \\fs20 ");
            return Text;
        }

        internal static string ConvertForHtmlReport(string Text)
        {
            Text = Tools.HtmlEncode(Text);
            Text = Text.Replace("\r\n", Tools.HtmlEncode("<i<br>>"));
            Text = Text.Replace("\n", Tools.HtmlEncode("<i<br>>"));
            //Text = Text.Replace("\\", "\\\\");
            //Text = Text.Replace("{", "\\{");
            //Text = Text.Replace("}", "\\}");
            Text = Text.Replace(Tools.HtmlEncode("<i<br>>"), "<br>");
            
            Text = Text.Replace(Tools.HtmlEncode("<i<hh>>"), "<span class='ihh'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</hh>>"), "</span>");
            
            Text = Text.Replace(Tools.HtmlEncode("<i<h>>"), " <span class='ih'> ");
            Text = Text.Replace(Tools.HtmlEncode("<i</h>>"), "</span>");
            
            Text = Text.Replace(Tools.HtmlEncode("<i<b>>"), "<b>");
            Text = Text.Replace(Tools.HtmlEncode("<i</b>>"), "</b>");

            Text = Text.Replace(Tools.HtmlEncode("<i<i>>"), "<i>");
            Text = Text.Replace(Tools.HtmlEncode("<i</i>>"), "</b>");

            Text = Text.Replace(Tools.HtmlEncode("<i<cr>>"), "<span class='icr'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</cr>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<cg>>"), " <span class='icg'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</cg>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<cb>>"), "<span class='icb'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</cb>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<co>>"), "<span class='ico'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</co>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<cy>>"), "<span class='icy'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</cy>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<cw>>"), "<span class='icw'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</cw>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<ac>>"), "<span class='iac'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</ac>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<ar>>"), "<span class='iar'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</ar>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<al>>"), "<span class='ial'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</al>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<hlr>>"), "<span class='hlr'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</hlr>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<hlg>>"), "<span class='hlg'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</hlg>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<hlb>>"), "<span class='hlb'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</hlb>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<hlo>>"), "<span class='hlo'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</hlo>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<hly>>"), "<span class='hly'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</hly>>"), "</span>");

            Text = Text.Replace(Tools.HtmlEncode("<i<h1>>"), "<span class='ih1'>");
            Text = Text.Replace(Tools.HtmlEncode("<i</h1>>"), "</span>");

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
                    case '\r':
                        SB.Append("%0d");
                        break;
                    case '\n':
                        SB.Append("%0a");
                        break;
                    case '\t':
                        SB.Append("%09");
                        break;
                    case '\0':
                        SB.Append("%00");
                        break;
                    default:
                        SB.Append(Input[i]);
                        break;
                }
            }
            return SB.ToString();
        }

        public static string RelaxedCookieEncode(string Input)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < Input.Length; i++)
            {
                switch (Input[i])
                {
                    case ' ':
                        SB.Append("%20");
                        break;
                    case ';':
                        SB.Append("%3b");
                        break;
                    case ',':
                        SB.Append("%2c");
                        break;
                    case '\r':
                        SB.Append("%0d");
                        break;
                    case '\n':
                        SB.Append("%0a");
                        break;
                    case '\t':
                        SB.Append("%09");
                        break;
                    case '\0':
                        SB.Append("%00");
                        break;
                    default:
                        SB.Append(Input[i]);
                        break;
                }
            }
            return SB.ToString();
        }

        public static string HeaderEncode(string Input)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < Input.Length; i++)
            {
                switch (Input[i])
                {
                    case '\r':
                        SB.Append("%0d");
                        break;
                    case '\n':
                        SB.Append("%0a");
                        break;
                    case '\0':
                        SB.Append("%00");
                        break;
                    default:
                        SB.Append(Input[i]);
                        break;
                }
            }
            return SB.ToString();
        }

        public static string UrlPathEncode(string Input)
        {
            return HttpUtility.UrlPathEncode(Input);
        }

        public static string UrlEncode(string Input)
        {
            return HttpUtility.UrlEncode(Input);
        }
        
        public static string UrlDecode(string Input)
        {
            return HttpUtility.UrlDecode(Input);
        }
        
        public static string UrlPathPartEncode(string Input)
        {
            Input = Input.Replace("~", "%7e").Replace("/", "%2f");
            string EncodedInput = HttpUtility.UrlEncode(Input);
            EncodedInput = EncodedInput.Replace("+", "%20");//Server treats + in Url Path Part as + instead of space. %20 is the right representation. + is ok for Query.
            return EncodedInput;
        }
        public static string UrlPathPartDecode(string Input)
        {
            Input = Input.Replace("%257e", "%7e").Replace("%252f", "%2f");
            return Tools.UrlDecode(Input);
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
            return HexEncodeBytes(Tools.ToByteArray(Input));
        }

        public static string HexEncodeBytes(byte[] Input)
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
            try
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
            catch(Exception Exp)
            {
                if (Input.Length == 1)
                {
                    throw Exp;//to avoid infinite recursive loop when checking a single character
                }
                else
                {
                    StringBuilder SB = new StringBuilder();
                    foreach (Char C in Input)
                    {
                        try
                        {
                            SB.Append(Tools.XmlEncode(C.ToString()));
                        }
                        catch { }
                    }
                    return SB.ToString();
                }
            }
        }

        public static string JsonEncode(string Input)
        {
            try
            {
                StringWriter JW = new StringWriter();
                JsonTextWriter JTW = new JsonTextWriter(JW);
                JTW.WriteStartObject();
                JTW.WritePropertyName("a");
                JTW.WriteValue(Input);
                JTW.WriteEndObject();
                JTW.Close();
                JW.Close();

                string Val = JW.ToString().Substring(5);
                Val = Val.TrimEnd('}');
                Val = Val.Trim();

                if (Val.StartsWith("\""))
                {
                    Val = Val.Trim('"');
                }
                else if (Val.StartsWith("\'"))
                {
                    Val = Val.Trim('\'');
                }
                return Val;
            }
            catch (Exception Exp)
            {
                if (Input.Length == 1)
                {
                    throw Exp;//to avoid infinite recursive loop when checking a single character
                }
                else
                {
                    StringBuilder SB = new StringBuilder();
                    foreach (Char C in Input)
                    {
                        try
                        {
                            SB.Append(Tools.JsonEncode(C.ToString()));
                        }
                        catch { }
                    }
                    return SB.ToString();
                }
            }
        }

        public static string EncodeForTrace(string Input)
        {
            return Input.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\000", "\\0").Replace("\00", "\\0").Replace("\0", "\\0");
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
            IronTrace IT = new IronTrace(ScanID, PluginName, Section, Parameter, Title, Message, new List<string[]>());
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
            if (!(TrimmedText.Contains("=") || TrimmedText.Contains(";") || TrimmedText.Contains("(") || TrimmedText.Contains(")")))
                return false;

            try
            {
                Jint.Expressions.Program Prog = JintEngine.Compile(Text, false);
                if (Prog.Statements.Count == 0) return false;
            }
            catch { return false; }
            return true;
        }

        public static bool IsCss(string Text)
        {
            string TrimmedText = Text.Trim();
            if (TrimmedText.StartsWith("<") || TrimmedText.StartsWith("{") || TrimmedText.StartsWith("[") || TrimmedText.EndsWith(">"))
                return false;
            if (!(TrimmedText.Contains("{") || TrimmedText.Contains("}") || TrimmedText.Contains(":") || TrimmedText.Contains(";") || TrimmedText.Contains("@")))
                return false;

            CssFx.CssStyleSheet ParsedCss = IronCss.Parse(Text);
            if (ParsedCss == null)
            {
                return false;
            }
            else
            {
                if (ParsedCss.Statements.Count > 0 || ParsedCss.Comments.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsJson(string Text)
        {
            string TrimmedText = Text.Trim();
            if(TrimmedText.StartsWith("[") && TrimmedText.EndsWith("]"))
            {
                TrimmedText = TrimmedText.TrimStart('[').TrimEnd(']').Trim();
            }
            if (TrimmedText.StartsWith("{") && TrimmedText.EndsWith("}"))
            {
                try
                {
                    JsonConvert.DeserializeObject<object>(Text);
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
                    Doc.XmlResolver = null;
                    Doc.LoadXml(TrimmedText);
                }
                catch { return false; }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsSoap(string Text)
        {
            string TrimmedText = Text.Trim();
            if (TrimmedText.StartsWith("<") && TrimmedText.EndsWith(">"))
            {
                try
                {
                    XmlDocument Doc = new XmlDocument();
                    Doc.XmlResolver = null;
                    Doc.LoadXml(TrimmedText);
                    if (Doc.DocumentElement.LocalName == "Envelope")
                    {
                        if (Doc.DocumentElement.ChildNodes.Count == 1 && Doc.DocumentElement.ChildNodes[0].LocalName == "Body")
                        {
                            return true;
                        }
                        else if (Doc.DocumentElement.ChildNodes.Count == 2 && Doc.DocumentElement.ChildNodes[0].LocalName == "Header" && Doc.DocumentElement.ChildNodes[1].LocalName == "Body")
                        {
                            return true;
                        }
                    }
                }
                catch {}
            }
            return false;
        }


        public static bool IsBinary(byte[] Bytes)
        {
            if (Bytes.Length > 10)
            {
                if (Bytes[0] == 255 && Bytes[1] == 216 && Bytes[2] == 255) return true;//jpeg
                if (Bytes[0] == 71 && Bytes[1] == 73 && Bytes[2] == 70 && Bytes[Bytes.Length - 2] == 0 && Bytes[Bytes.Length - 1] == 59) return true;//gif
                if (Bytes[0] == 137 && Bytes[1] == 80 && Bytes[2] == 78 && Bytes[3] == 71 && Bytes[4] == 13 && Bytes[5] == 10 && Bytes[6] == 26 && Bytes[7] == 10) return true;//png
                if (Bytes[0] == 80 && Bytes[1] == 75 && Bytes[2] == 3 && Bytes[3] == 4) return true;//zip
                if (Bytes[0] == 37 && Bytes[1] == 80 && Bytes[2] == 68 && Bytes[3] == 70) return true;//pdf
                if (Bytes[0] == 172 && Bytes[1] == 237 && Bytes[2] == 0) return true;//Java Serialized Object
                if (Bytes[0] == 3 && Bytes[1] ==  0) return true;//Action Message Format
            }
            if ((new List<byte>(Bytes)).Contains(0))
                return true;
            return false;
        }

        public static bool IsBinary(string Text)
        {
            try
            {
                return IsBinary(Encoding.UTF8.GetBytes(Text));
            }
            catch
            {
                if (Text.Contains("\0"))
                    return true;
                else
                    return false;
            }
        }

        public static bool IsEven(int Num)
        {
            return Num % 2 == 0;
        }

        public static string GetCharsetFromContentType(string ContentType)
        {
            string Charset = "";
            int Loc = ContentType.IndexOf("charset=");
            if (Loc >= 0)
            {
                Charset = ContentType.Substring(Loc + 8).Trim();
            }
            return Charset;
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
            try
            {
                Process.Start(Executable);
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to start external application", Exp);
            }
        }

        public static void RunWith(string Executable, string Arguments)
        {
            try
            {
                Process.Start(Executable, Arguments);
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to start external application", Exp);
            }
        }

        public static void RunInShellWith(string Executable, string Arguments)
        {
            try
            {
                ProcessStartInfo Info = new ProcessStartInfo();
                Info.UseShellExecute = true;
                Info.CreateNoWindow = false;
                Info.Arguments = "/k " + Executable + " " + Arguments;
                Info.FileName = "cmd";
                Info.WindowStyle = ProcessWindowStyle.Normal;
                Process P = Process.Start(Info);
            }
            catch (Exception Exp)
            {
                IronException.Report("Unable to start external application", Exp);
            }
        }

        public static int GetPercent(int One, int Two)
        {
            Double O;
            Double T;
            if (Two > One)
            {
                O = (Double)One;
                T = (Double)Two;
            }
            else
            {
                O = (Double)Two;
                T = (Double)One;
            }
            int Per = (int)((O / T) * 100.0);
            return 100 - Per;
        }

        public static void ExecuteMethod(ThreadStart PTS)
        {
            PTS();
        }

        public static void ExecuteMethodP(ParameterizedThreadStart PTS, object Obj)
        {
            PTS(Obj);
        }

        public static object[] ToDotNetArray(object O)
        {
            object[] Values = new object[0];
            string Type = O.GetType().FullName;
            switch(Type)
            {
                case("IronRuby.Builtins.RubyArray"):
                    Values = new object[(O as IronRuby.Builtins.RubyArray).Count];
                    (O as IronRuby.Builtins.RubyArray).CopyTo(Values, 0);
                    break;
                case("IronPython.Runtime.List"):
                    Values = new object[(O as IronPython.Runtime.List).Count];
                    (O as IronPython.Runtime.List).CopyTo(Values, 0);
                    break;
            }
            return Values;
        }
        public static List<object> ToDotNetList(object O)
        {
            return new List<object>(ToDotNetArray(O));
        }

        public static string CamelCaseToUnderScore(string CamelCase)
        {
            StringBuilder SB = new StringBuilder();
            for(int i=0; i < CamelCase.Length; i++)
            {
                string LowerChar = CamelCase[i].ToString().ToLower();
                if (i > 0)
                {
                    if (!Char.IsUpper(CamelCase[i - 1]) && Char.IsUpper(CamelCase[i]))
                    {
                        SB.Append("_");
                    }
                }
                SB.Append(LowerChar);
            }
            return SB.ToString();
        }

        public static string EscapeDoubleQuotes(string Input)
        {
            StringBuilder Output = new StringBuilder();
            bool EscapeNext = false;
            for (int i = 0; i < Input.Length; i++)
            {
                char C = Input[i];
                if (C == '"')
                {
                    if (!EscapeNext)
                    {
                        Output.Append("\\");
                    }
                }
                Output.Append(C.ToString());
                
                if(C == '\\')
                {
                    if (EscapeNext)
                    {
                        EscapeNext = false;//there is a \ before this escaping this \
                    }
                    else
                    {
                        EscapeNext = true;
                    }
                }
                else
                {
                    EscapeNext = false;
                }
            }
            return Output.ToString();
        }

        public static List<string> SplitLines(string Content)
        {
            List<string> Result = new List<string>();
            string[] Split = null;
            if (Content.Contains("\n"))
            {
                Split = Content.Split(new char[] { '\n' });
            }
            else
            {
                Split = Content.Split(new char[] { '\r' });
            }
            for (int i = 0; i < Split.Length; i++)
            {
                string Trimmed = Split[i].Trim('\r').Trim('\n');
                if(Trimmed.Length > 0) Result.Add(Trimmed);
            }
            return Result;
        }

        public static string CreateString(char C, int Repeat)
        {
            return new String(C, Repeat);
        }

        public static bool IsXmlContentSame(string One, string Two)
        {
            XmlDocument XmlOne = new XmlDocument();
            XmlOne.XmlResolver = null;
            XmlOne.LoadXml(One);
            XmlDocument XmlTwo = new XmlDocument();
            XmlTwo.XmlResolver = null;
            XmlTwo.LoadXml(Two);

            if (XmlOne.ChildNodes.Count == XmlTwo.ChildNodes.Count)
            {
                if (XmlOne.ChildNodes.Count > 0)
                {
                    for (int i = 0; i < XmlOne.ChildNodes.Count; i++)
                    {
                        if (!IsXmlNodeSame(XmlOne.ChildNodes[i], XmlTwo.ChildNodes[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if (XmlOne.InnerText == XmlTwo.InnerText)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public static string MultiplyString(string StrToMulti, int Factor)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < Factor; i++)
            {
                SB.Append(StrToMulti);
            }
            return SB.ToString();
        }

        public static string GetRefreshHeaderUrl(string Header)
        {
            Match M = Regex.Match(Header, @"\s*\d+;\s*url\s*=\s*(.*)", RegexOptions.IgnoreCase);
            if (M.Success)
            {
                if (M.Groups.Count > 1)
                {
                    string TrimmedUrl = M.Groups[1].Value.Trim();
                    if (TrimmedUrl.StartsWith("'"))
                    {
                        return TrimmedUrl.Trim('\'');
                    }
                    else if (TrimmedUrl.StartsWith("\""))
                    {
                        return TrimmedUrl.Trim('"');
                    }
                    else
                    {
                        return TrimmedUrl;
                    }
                }
            }
            throw new Exception("Invalid Refresh Header");
        }

        public static Bitmap CaptureScreen()
        {
            Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics G = Graphics.FromImage(BM as Image);
            G.CopyFromScreen(0, 0, 0, 0, BM.Size);
            //BM.Save(Config.Path + "\\sh.bmp", ImageFormat.Bmp);
            return BM;
        }

        public static bool IsValidIpv4(string Input)
        {
            try
            {
                IPAddress IpAdd = IPAddress.Parse(Input);
                if (IpAdd.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static bool IsValidIpv6(string Input)
        {
            try
            {
                IPAddress IpAdd = IPAddress.Parse(Input);
                if (IpAdd.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static List<string> NwToIp(string NetworkAddress)
        {
            List<string> IPs = new List<string>();
            if (NetworkAddress.Contains("/"))
            {
                string[] Parts = NetworkAddress.Split('/');
                if (Parts.Length != 2)
                {
                    throw new Exception("Invalid address format");
                }
                string[] NwParts = GetCleanNwAddr(Parts[0]);
                if (NwParts.Length == 1)
                {
                    throw new Exception(NwParts[0]);
                }
                int SM = 0;
                try
                {
                    SM = Int32.Parse(Parts[1].Trim());
                    if (SM < 1 || SM > 31) throw new Exception();
                }
                catch
                {
                    throw new Exception("Invalid CIDR number");
                }
                int Octect = SM / 8;
                int Position = SM % 8;
                string[] NwAddr = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    if (i == Octect)
                    {
                        int OctNum = Int32.Parse(NwParts[i]);
                        string OctBin = Convert.ToString(OctNum, 2).PadLeft(8, '0');
                        string OctBinPrefix = OctBin.Substring(0, Position);
                        int Lowest = Convert.ToInt32(OctBinPrefix.PadRight(8, '0'), 2);
                        int Highest = Convert.ToInt32(OctBinPrefix.PadRight(8, '1'), 2);
                        NwAddr[i] = string.Format("{0}-{1}", Lowest, Highest);
                    }
                    else if(i > Octect)
                    {
                        NwAddr[i] = "0-255";
                    }
                    else
                    {
                        NwAddr[i] = NwParts[i];
                    }
                }
                IPs = GetOctectRange(NwAddr, 0);
                if (IPs.Count > 1)
                {
                    IPs.RemoveAt(0);//Remove network address
                    IPs.RemoveAt(IPs.Count - 1);//Remove broadcast address
                }
            }
            else if (NetworkAddress.Contains("-"))
            {
                string[] NwParts = GetCleanNwAddr(NetworkAddress);
                if (NwParts.Length != 4) throw new Exception(NwParts[0]);
                IPs.AddRange(GetOctectRange(NwParts, 0));
            }
            return IPs;
        }

        static List<string> GetOctectRange(string[] NwAddr, int OctectIndex)
        {
            List<string> IPs = new List<string>();

            if (NwAddr[OctectIndex].Contains("-"))
            {
                List<int> Range = GetNumberRange(NwAddr[OctectIndex]);
                foreach (int Number in Range)
                {
                    if (OctectIndex == 3)
                    {
                        IPs.Add(string.Format("{0}.{1}.{2}.{3}", NwAddr[0], NwAddr[1], NwAddr[2], Number));
                    }
                    else
                    {
                        string[] NewNwAddr = new string[4];
                        NwAddr.CopyTo(NewNwAddr, 0);
                        NewNwAddr[OctectIndex] = Number.ToString();
                        IPs.AddRange(GetOctectRange(NewNwAddr, OctectIndex + 1));
                    }
                }
            }
            else
            {
                if (OctectIndex == 3)
                {
                    IPs.Add(string.Format("{0}.{1}.{2}.{3}", NwAddr[0], NwAddr[1], NwAddr[2], NwAddr[3]));
                }
                else
                {
                    IPs.AddRange(GetOctectRange(NwAddr, OctectIndex + 1));
                }
            }
            return IPs;
        }

        public static string[] GetCleanNwAddr(string NwAddr)
        {
            string[] Parts = NwAddr.Trim().Split('.');
            string[] Result = new string[4];
            if (Parts.Length != 4)
            {
                return new string[] {"Invalid Network address"};
            }
            for (int i = 0; i < 4; i++)
            {
                List<int> Nums = new List<int>();
                if (Parts[i].Contains("-"))
                {
                    string[] SubParts = Parts[i].Split('-');
                    if (SubParts.Length != 2)
                    {
                        return new string[] { "Invalid Network address" };
                    }
                    try
                    {
                        int FirstNum = Int32.Parse(SubParts[0].Trim());
                        int LastNum = Int32.Parse(SubParts[1].Trim());
                        if (FirstNum >= LastNum)
                        {
                            return new string[] { "Invalid address range" };
                        }
                        Nums.Add(FirstNum);
                        Nums.Add(LastNum);
                    }
                    catch
                    {
                        return new string[] { "Invalid Network address" };
                    }
                }
                else
                {
                    try
                    {
                        Nums.Add(Int32.Parse(Parts[i].Trim()));
                    }
                    catch
                    {
                        return new string[] { "Invalid Network address" };
                    }
                }
                foreach (int Num in Nums)
                {
                    if ((Num < 0 || Num > 255) || ((i == 0) && (Num < 1 || Num > 223)))
                    {
                        return new string[] { "Invalid Network address" };
                    }
                }
                if (Nums.Count == 1)
                {
                    Result[i] = Nums[0].ToString();
                }
                else if (Nums.Count == 2)
                {
                    Result[i] = string.Format("{0}-{1}", Nums[0], Nums[1]);
                }
            }
            return Result;
        }

        static List<int> GetNumberRange(string Range)
        {
            List<int> Numbers = new List<int>();
            string[] RangeParts = Range.Split('-');
            if(RangeParts.Length != 2)
            {
                throw new Exception("Invalid range");
            }
            int Start = Int32.Parse(RangeParts[0].Trim());
            int End = Int32.Parse(RangeParts[1].Trim());
            for (int i = Start; i <= End; i++)
            {
                Numbers.Add(i);
            }
            return Numbers;
        }

        static bool IsXmlNodeSame(XmlNode One, XmlNode Two)
        {
            if (One.ChildNodes.Count == Two.ChildNodes.Count)
            {
                if (One.ChildNodes.Count > 0)
                {
                    for (int i = 0; i < One.ChildNodes.Count; i++)
                    {
                        if (!IsXmlNodeSame(One.ChildNodes[i], Two.ChildNodes[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if (One.InnerText == Two.InnerText)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
