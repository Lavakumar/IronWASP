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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace IronWASP
{
    public partial class DiffWindow : Form
    {
        public DiffWindow()
        {
            InitializeComponent();
        }

        string SourceText = "";
        string DestinationText = "";

        internal static Thread T; 

        private void DiffWindowShowDiffBtn_Click(object sender, EventArgs e)
        {
            DiffWindowShowDiffBtn.Enabled = false;
            DiffStatusTB.Text = "Calculating Diff......";
            SourceText = IronUI.DW.DiffSourceTB.Text;
            DestinationText = IronUI.DW.DiffDestinationTB.Text;
            T = new Thread(DoDiff);
            T.Start();
        }

        void DoDiff()
        {
            string SinglePage = "";
            string[] SideBySideResults = new string[] { "", "", "" };
            string Status = "Done";
            try
            {
                SinglePage = DoSinglePageDiff(SourceText, DestinationText);
                SideBySideResults = DoSideBySideDiff(SourceText, DestinationText);
                Status = string.Format("Done. Diff Level - {0}% ", SideBySideResults[2]);
            }
            catch(Exception Exp)
            {
                Status = "Error: " + Exp.Message;
            }
            IronUI.ShowDiffResults(Status, SideBySideResults[0], SideBySideResults[1], SinglePage);
        }

        internal static string[] DoSideBySideDiff(string SourceText, string DestinationText)
        {
            string[] Result = new string[3];
            Differ DiffMaker = new Differ();
            SideBySideDiffBuilder SideBySideDiffer = new SideBySideDiffBuilder(DiffMaker);
            SideBySideDiffModel SideBySideDiffResult = SideBySideDiffer.BuildDiffModel(SourceText, DestinationText);
            int DiffLevel = IronDiffer.GetLevel(SideBySideDiffResult, SourceText, DestinationText);
            Result[0] = FullDiff(SideBySideDiffResult.OldText.Lines);
            Result[1] = FullDiff(SideBySideDiffResult.NewText.Lines);
            Result[2] = DiffLevel.ToString();
            return Result;
        }

        internal static string FullDiff(List<DiffPiece> Lines)
        {
            int MaxLineLength = 0;
            string CurrentFillerString = "";

            StringBuilder DR = new StringBuilder(@"{\rtf1{\colortbl ;\red50\green205\blue50;\red255\green165\blue0;\red190\green190\blue190;}");
            foreach (DiffPiece Line in Lines)
            {
                MaxLineLength = GetMaxLength(MaxLineLength, Line.Text);
                switch (Line.Type)
                {
                    case ChangeType.Deleted:
                        DR.Append(@"\highlight2 "); DR.Append(Tools.RtfSafe(Line.Text)); DR.Append(@" \highlight0 \par ");
                        break;
                    case ChangeType.Inserted:
                        DR.Append(@"\highlight1 "); DR.Append(Tools.RtfSafe(Line.Text)); DR.Append(@" \highlight0 \par ");
                        break;
                    case ChangeType.Modified:
                        DR.Append(LineDiff(Line)); DR.Append(@" \par ");
                        break;
                    case ChangeType.Unchanged:
                        DR.Append(Tools.RtfSafe(Line.Text)); DR.Append(@" \par ");
                        break;
                    case ChangeType.Imaginary:
                        DR.Append(@"\highlight3 "); DR.Append(GetFillerLine(MaxLineLength, CurrentFillerString)); DR.Append(@" \highlight0 \par ");
                        break;
                }
            }
            return DR.ToString();
        }

        internal static int GetMaxLength(int CurrentMaxLength, string Line)
        {
            if (Line == null) Line = "";
            int Length = 0;
            if (Line.Length > CurrentMaxLength)
            {
                Length = Line.Length;
            }
            else
            {
                Length = CurrentMaxLength;
            }
            return Length;
        }

        internal static string GetFillerLine(int MaxLineLength, string CurrentFillerString)
        {
            if (MaxLineLength == CurrentFillerString.Length) return CurrentFillerString;
            StringBuilder Line = new StringBuilder();
            for (int i = 0; i < MaxLineLength; i++)
            {
                Line.Append(" ");
            }
            return Line.ToString();
        }

        internal static string LineDiff(DiffPiece Line)
        {
            StringBuilder DR = new StringBuilder();
            foreach (DiffPiece Word in Line.SubPieces)
            {
                switch (Word.Type)
                {
                    case ChangeType.Deleted:
                        DR.Append(@"\highlight2 "); DR.Append(Tools.RtfSafe(Word.Text)); DR.Append(@" \highlight0 ");
                        break;
                    case ChangeType.Inserted:
                        DR.Append(@"\highlight1 "); DR.Append(Tools.RtfSafe(Word.Text)); DR.Append(@" \highlight0 ");
                        break;
                    case ChangeType.Imaginary:
                        break;
                    case ChangeType.Unchanged:
                        DR.Append(Tools.RtfSafe(Word.Text));
                        break;
                    case ChangeType.Modified:
                        DR.Append(Tools.RtfSafe(Word.Text));
                        break;
                }
            }
            return DR.ToString();
        }

        internal static string DoSinglePageDiff(string SourceText, string DestinationText)
        {
            DiffResult DResult = Tools.Diff(SourceText, DestinationText);
            int TotalDestLines = DResult.UnChanged.Count + DResult.Inserted.Count;
            StringBuilder Result = new StringBuilder(@"{\rtf1{\colortbl ;\red50\green205\blue50;\red255\green165\blue0;}");
            for (int i = 0; i < TotalDestLines; i++)
            {
                int LineNo = i + 1;
                string LineNoString = GetLineNoString(LineNo);
                if (DResult.UnChanged.Contains(LineNo))
                {
                    Result.Append(LineNoString); Result.Append(Tools.RtfSafe(DResult.UnChangedSections[DResult.UnChanged.IndexOf(LineNo)])); Result.Append(@" \par ");
                }
                if (DResult.MissingAt.Contains(LineNo))
                {
                    int Pointer = 0;
                    while ((Pointer = DResult.MissingAt.IndexOf(LineNo, Pointer)) > -1)
                    {
                        Result.Append("       "); Result.Append(@"\highlight2 "); Result.Append(Tools.RtfSafe(DResult.DeletedSections[Pointer])); Result.Append(@" \highlight0 \par ");
                        Pointer++;
                    }
                }
                if (DResult.Inserted.Contains(i + 1))
                {
                    Result.Append(LineNoString); Result.Append(@"\highlight1 "); Result.Append(Tools.RtfSafe(DResult.InsertedSections[DResult.Inserted.IndexOf(LineNo)])); Result.Append(@" \highlight0 \par ");
                }
            }
            return Result.ToString();
        }

        static string GetLineNoString(int LineNo)
        {
            string LineNoStr = LineNo.ToString();
            int LengthDiff = 6 - LineNoStr.Length;
            StringBuilder Line = new StringBuilder(LineNoStr);
            for (int i = 0; i < LengthDiff; i++)
            {
                Line.Append(" ");
            }
            return Line.ToString();
        }

        private void ClearSourceBtn_Click(object sender, EventArgs e)
        {
            DiffSourceTB.Text = "";
        }

        private void ClearDestinationBtn_Click(object sender, EventArgs e)
        {
            DiffDestinationTB.Text = "";
        }

        private void PasteSourceBtn_Click(object sender, EventArgs e)
        {
            DiffSourceTB.Text = "";
            try
            {
                DiffSourceTB.Text = Clipboard.GetText();
            }
            catch { }
        }

        private void PasteDestinationBtn_Click(object sender, EventArgs e)
        {
            DiffDestinationTB.Text = "";
            try
            {
                DiffDestinationTB.Text = Clipboard.GetText();
            }
            catch { }
        }
    }
}
