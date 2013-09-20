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
    public class Highlighter
    {
        public static string Highlight(Request Req, string ToHighlight)
        {
            return Highlight(Req, new List<string>() { ToHighlight });
        }

        public static string Highlight(Response Res, string ToHighlight)
        {
            return Highlight(Res, new List<string>() { ToHighlight });
        }

        public static string Highlight(Request Req, List<string> ToHighlight)
        {
            string ReqHeader = Req.GetHeadersAsString();
            string Body = Req.BodyString;

            ReqHeader = InsertHighlights(ReqHeader, ToHighlight);
            Body = InsertHighlights(Body, ToHighlight);

            StringBuilder SB = new StringBuilder();
            SB.Append(SnipHeaderSection(ReqHeader));
            SB.AppendLine(); SB.AppendLine();
            SB.Append(SnipBodySection(Body));

            return SB.ToString();
        }

        public static string Highlight(Response Res, List<string> ToHighlight)
        {
            string ResHeader = Res.GetHeadersAsString();
            string Body = Res.BodyString;

            ResHeader = InsertHighlights(ResHeader, ToHighlight);
            Body = InsertHighlights(Body, ToHighlight);

            StringBuilder SB = new StringBuilder();
            SB.Append(SnipHeaderSection(ResHeader));
            SB.AppendLine(); SB.AppendLine();
            SB.Append(SnipBodySection(Body));

            return SB.ToString();
        }

        public static string InsertHighlights(string Value, List<string> Triggs)
        {
            foreach (string Trigg in Triggs)
            {
                Value = InsertHighlight(Value, Trigg);
            }
            return Value;
        }

        public static string InsertHighlight(string Value, string Trigg)
        {
            return Value.Replace(Trigg, "<i<hlg>>" + Trigg + "<i</hlg>>");
        }

        public static string SnipHeaderSection(string Header)
        {
            List<int> InterestingHeaders = new List<int>();
            StringBuilder SB = new StringBuilder();
            List<string> HeaderParts = Tools.SplitLines(Header);
            if (HeaderParts.Count > 0)
            {
                SB.AppendLine(HeaderParts[0]);
                bool BrevityAdded = false;
                for (int i = 1; i < HeaderParts.Count; i++)
                {
                    if (HeaderParts[i].Contains("<i<hlg>>") && HeaderParts[i].Contains("<i</hlg>>"))
                    {
                        if (i > 1)
                        {
                            if (InterestingHeaders.Count > 0)
                            {
                                if (i == InterestingHeaders[InterestingHeaders.Count - 1] + 1)//Check if the previous header was the only header between this and the last interesting header. In the case add the header instead of the snipped message as both take up same space
                                {
                                    if (HeaderParts[i - 1].Length > 150)
                                    {
                                        SB.Append(HeaderParts[i - 1].Substring(0, 100)); SB.AppendLine("<i<cb>>[---- Snipped rest of this header for brevity ----]<i</cb>>");
                                    }
                                    else
                                    {
                                        SB.AppendLine(HeaderParts[i - 1]);
                                    }
                                }
                                else
                                {
                                    SB.AppendLine("<i<cb>>[---- Snipped parts of HTTP headers section for brevity ----]<i</cb>>");
                                }
                            }
                        }
                        SB.AppendLine(HeaderParts[i]);
                        BrevityAdded = false;
                        InterestingHeaders.Add(i);
                    }
                    else
                    {
                        if (i == 1 || i == HeaderParts.Count - 1)
                        {
                            if (HeaderParts[i].Length > 150)
                            {
                                SB.Append(HeaderParts[i].Substring(0, 100)); SB.AppendLine("<i<cb>>[---- Snipped rest of this header for brevity ----]<i</cb>>");
                            }
                            else
                            {
                                SB.AppendLine(HeaderParts[i]);
                            }
                        }
                        else
                        {
                            if (!BrevityAdded)
                            {
                                SB.AppendLine("<i<cb>>[---- Snipped parts of HTTP headers section for brevity ----]<i</cb>>");
                                BrevityAdded = true;
                            }
                        }
                    }
                }
            }
            else
            {
                return Header;
            }

            return SB.ToString();
        }

        public static string SnipBodySection(string Body)
        {
            bool InTag = false;
            int TagCount = 0;
            int Pointer = 0;
            int PartPointer = 0;
            List<string> Parts = new List<string>();
            while (Pointer < Body.Length)
            {
                if (IsOpeningTag(Body, Pointer))
                {
                    //Add the part before the highlight section
                    if (!InTag && Pointer > 0)
                    {
                        string Part = Body.Substring(PartPointer, Pointer - PartPointer);
                        if (Part.Length > 0)
                        {
                            Parts.Add(Part);
                            PartPointer = PartPointer + Part.Length;
                        }
                    }
                    InTag = true;
                    TagCount++;
                    Pointer = Pointer + 8;
                }
                else if (InTag && IsClosingTag(Body, Pointer))
                {
                    TagCount--;
                    if (TagCount == 0)
                    {
                        InTag = false;
                        string Part = Body.Substring(PartPointer, (Pointer + 9) - PartPointer);
                        if (Part.Length > 0)
                        {
                            Parts.Add(Part);
                            PartPointer = PartPointer + Part.Length;
                        }
                    }
                    Pointer = Pointer + 9;
                }
                else
                {
                    Pointer++;
                }
            }
            if (PartPointer < Body.Length)
            {
                string Part = Body.Substring(PartPointer);
                if (Part.Length > 0)
                {
                    Parts.Add(Part);
                }
            }
            StringBuilder SB = new StringBuilder();

            List<int> AddedFullParts = new List<int>();
            List<int> AddedFirstParts = new List<int>();
            List<int> AddedLastParts = new List<int>();

            for (int i = 0; i < Parts.Count; i++)
            {
                if (Parts[i].StartsWith("<i<hlg>>") && Parts[i].EndsWith("<i</hlg>>"))
                {
                    if (i > 0)
                    {
                        if (!(Parts[i - 1].StartsWith("<i<hlg>>") && Parts[i - 1].EndsWith("<i</hlg>>")))
                        {
                            if (Parts[i - 1].Length <= 150)
                            {
                                if (!(AddedFullParts.Contains(i - 1) || AddedFirstParts.Contains(i - 1) || AddedLastParts.Contains(i - 1)))
                                {
                                    SB.Append(Parts[i - 1]);
                                    AddedFullParts.Add(i - 1);
                                }
                            }
                            else
                            {
                                if (!(AddedFullParts.Contains(i - 1) || AddedLastParts.Contains(i - 1)))
                                {
                                    SB.Append("<i<cb>>[---- Snipped parts of HTTP body section for brevity ----]<i</cb>>");
                                    SB.Append(Parts[i - 1].Substring(Parts[i - 1].Length - 150));
                                    AddedLastParts.Add(i - 1);
                                }
                            }
                        }
                    }
                    if (!(AddedFullParts.Contains(i) || AddedFirstParts.Contains(i) || AddedLastParts.Contains(i)))
                    {
                        SB.Append(Parts[i]);
                        AddedFullParts.Add(i);
                    }
                }
                else
                {
                    if (i > 0)
                    {
                        if ((Parts[i - 1].StartsWith("<i<hlg>>") && Parts[i - 1].EndsWith("<i</hlg>>")))
                        {
                            if (Parts[i].Length <= 150)
                            {
                                if (!(AddedFullParts.Contains(i) || AddedFirstParts.Contains(i) || AddedLastParts.Contains(i)))
                                {
                                    SB.Append(Parts[i]);
                                    AddedFullParts.Add(i);
                                }
                            }
                            else
                            {
                                if (!(AddedFullParts.Contains(i) || AddedFirstParts.Contains(i)))
                                {
                                    SB.Append(Parts[i].Substring(0, 150));
                                    SB.Append("<i<cb>>[---- Snipped parts of HTTP body section for brevity ----]<i</cb>>");
                                    AddedFirstParts.Add(i);
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //SB.Append("<i<cb>>[---- Snipped parts of HTTP body section for brevity ----]<i</cb>>");
                    //}
                }
            }
            return SB.ToString();
        }

        static bool IsOpeningTag(string Body, int Position)
        {
            if (Position + 7 < Body.Length)
            {
                string Part = Body.Substring(Position, 8);
                if (Body.Substring(Position, 8).Equals("<i<hlg>>"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static bool IsClosingTag(string Body, int Position)
        {
            if (Position + 8 < Body.Length)
            {
                string Part = Body.Substring(Position, 9);
                if (Body.Substring(Position, 9).Equals("<i</hlg>>"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
