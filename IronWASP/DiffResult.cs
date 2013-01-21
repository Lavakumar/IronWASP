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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Mathertel;

namespace IronWASP
{
    public class DiffResult
    {
        public List<int> Deleted = new List<int>();
        public List<int> MissingAt = new List<int>();
        public List<int> Inserted = new List<int>();
        public List<int> UnChanged = new List<int>();
        public List<string> DeletedSections = new List<string>();
        public List<string> InsertedSections = new List<string>();
        public List<string> UnChangedSections = new List<string>();

        internal static DiffResult GetStringDiff(Mathertel.Diff.Item[] Results, string Source, string Destination)
        {
            return GetDiff(Results, Source, Destination, '\n');
        }
        
        internal static DiffResult GetLineDiff(Mathertel.Diff.Item[] Results, string Source, string Destination)
        {
            return GetDiff(Results, Source, Destination, ' ');
        }

        internal static DiffResult GetDiff(Mathertel.Diff.Item[] Results, string Source, string Destination, char Splitter)
        {
            DiffResult DResult = new DiffResult();

            int n = 0;
            string[] aLines = Source.Split(Splitter);
            string[] bLines = Destination.Split(Splitter);

            foreach (Diff.Item Result in Results)
            {
                Diff.Item aItem = Result;

                // write unchanged lines
                while ((n < aItem.StartB) && (n < bLines.Length))
                {
                    DResult.UnChanged.Add(n + 1);
                    DResult.UnChangedSections.Add(bLines[n]);
                    n++;
                } // while

                // write deleted lines
                for (int m = 0; m < aItem.deletedA; m++)
                {
                    DResult.Deleted.Add(aItem.StartA + m + 1);
                    DResult.MissingAt.Add(n + 1);
                    DResult.DeletedSections.Add(aLines[aItem.StartA + m]);
                } // for

                // write inserted lines
                while (n < aItem.StartB + aItem.insertedB)
                {
                    DResult.Inserted.Add(n + 1);
                    DResult.InsertedSections.Add(bLines[n]);
                    n++;
                } // while
            } // while

            // write rest of unchanged lines
            while (n < bLines.Length)
            {
                DResult.UnChanged.Add(n + 1);
                DResult.UnChangedSections.Add(bLines[n]);
                n++;
            } // while

            return DResult;
        }
    }
}
