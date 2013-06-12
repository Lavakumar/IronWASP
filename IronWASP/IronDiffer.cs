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
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace IronWASP
{
    public class IronDiffer
    {
        public static SideBySideDiffModel GetDiff(string Source, string Destination)
        {
            Differ DiffMaker = new Differ();
            SideBySideDiffBuilder SideBySideDiffer = new SideBySideDiffBuilder(DiffMaker);
            return SideBySideDiffer.BuildDiffModel(Source, Destination);
        }
        
        public static int GetLevel(string Source, string Destination)
        {
            Differ DiffMaker = new Differ();
            SideBySideDiffBuilder SideBySideDiffer = new SideBySideDiffBuilder(DiffMaker);
            SideBySideDiffModel SideBySideDiffResult = SideBySideDiffer.BuildDiffModel(Source, Destination);
            return GetLevel(SideBySideDiffResult, Source, Destination);
        }

        public static int GetLevel(SideBySideDiffModel SideBySideDiffResult, string Source, string Destination)
        {
            Double[] Result = new Double[2];
            Result[0] = FullDiff(SideBySideDiffResult.OldText.Lines, Source.Length);
            if (Source.Length == 0 && Destination.Length != 0) Result[0] = 100.0;
            Result[1] = FullDiff(SideBySideDiffResult.NewText.Lines, Destination.Length);
            if (Destination.Length == 0 && Source.Length != 0) Result[1] = 100.0;
            return (int)((Result[0] + Result[1]) / 2);
        }

        public static double FullDiff(List<DiffPiece> Lines, int FullLength)
        {
            double OverAll = 0;
            foreach (DiffPiece Line in Lines)
            {
                switch (Line.Type)
                {
                    case ChangeType.Deleted:
                        OverAll = OverAll + GetWeight(Line.Text.Length, FullLength);
                        break;
                    case ChangeType.Inserted:
                        OverAll = OverAll + GetWeight(Line.Text.Length, FullLength);
                        break;
                    case ChangeType.Modified:
                        OverAll = OverAll + ((LineDiff(Line, Line.Text.Length) /100) * GetWeight(Line.Text.Length, FullLength));
                        break;
                    case ChangeType.Unchanged:
                    case ChangeType.Imaginary:
                        break;
                }
            }
            return OverAll;
        }

        public static double LineDiff(DiffPiece Line, int FullLength)
        {
            double OverAll = 0;
            foreach (DiffPiece Word in Line.SubPieces)
            {
                switch (Word.Type)
                {
                    case ChangeType.Deleted:
                        OverAll = OverAll + GetWeight(Word.Text.Length, FullLength);
                        break;
                    case ChangeType.Inserted:
                        OverAll = OverAll + GetWeight(Word.Text.Length, FullLength);
                        break;
                    case ChangeType.Imaginary:
                    case ChangeType.Unchanged:
                    case ChangeType.Modified:
                        break;
                }
            }
            return OverAll;
        }

        public static List<string> GetInsertedStrings(SideBySideDiffModel SideBySideDiffResult)
        {
            List<string> InsertedStrings = new List<string>();
            foreach (DiffPiece Line in SideBySideDiffResult.NewText.Lines)
            {
                switch (Line.Type)
                {
                    case ChangeType.Inserted:
                        InsertedStrings.Add(Line.Text);
                        break;
                    case ChangeType.Modified:
                        foreach (DiffPiece Word in Line.SubPieces)
                        {
                            switch (Word.Type)
                            {
                                case ChangeType.Inserted:
                                    InsertedStrings.Add(Word.Text);
                                    break;
                                case ChangeType.Deleted:    
                                case ChangeType.Imaginary:
                                case ChangeType.Unchanged:
                                case ChangeType.Modified:
                                    break;
                            }
                        }
                        break;
                    case ChangeType.Deleted:    
                    case ChangeType.Unchanged:
                    case ChangeType.Imaginary:
                        break;
                }
            }
            return InsertedStrings;
        }

        public static List<string> GetDeletedStrings(SideBySideDiffModel SideBySideDiffResult)
        {
            List<string> DeletedStrings = new List<string>();
            foreach (DiffPiece Line in SideBySideDiffResult.OldText.Lines)
            {
                switch (Line.Type)
                {
                    case ChangeType.Deleted:
                        DeletedStrings.Add(Line.Text);
                        break;
                    case ChangeType.Modified:
                        foreach (DiffPiece Word in Line.SubPieces)
                        {
                            switch (Word.Type)
                            {
                                case ChangeType.Deleted:
                                    DeletedStrings.Add(Word.Text);
                                    break;
                                case ChangeType.Inserted:
                                case ChangeType.Imaginary:
                                case ChangeType.Unchanged:
                                case ChangeType.Modified:
                                    break;
                            }
                        }
                        break;
                    case ChangeType.Inserted:
                    case ChangeType.Unchanged:
                    case ChangeType.Imaginary:
                        break;
                }
            }
            return DeletedStrings;
        }

        public static double GetWeight(int SectionLength, int FullLength)
        {
            return ((double)SectionLength / (double)FullLength) * 100;
        }
    }

}
