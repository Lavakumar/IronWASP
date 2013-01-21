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
    public class SimilarityChecker
    {
        Dictionary<string, SimilarityCheckerItem> ItemsDictionary = new Dictionary<string, SimilarityCheckerItem>();

        public List<SimilarityCheckerGroup> StrictGroups = new List<SimilarityCheckerGroup>();
        public List<SimilarityCheckerGroup> RelaxedGroups = new List<SimilarityCheckerGroup>();

        public string StrictGroupsSignature
        {
            get
            {
                StringBuilder SB = new StringBuilder();
                foreach (SimilarityCheckerGroup Group in StrictGroups)
                {
                    SB.Append(Group.Signature);
                    SB.Append(" ");
                }
                return SB.ToString();
            }
        }

        public string RelaxedGroupsSignature
        {
            get
            {
                StringBuilder SB = new StringBuilder();
                foreach (SimilarityCheckerGroup Group in RelaxedGroups)
                {
                    SB.Append(Group.Signature);
                    SB.Append(" ");
                }
                return SB.ToString();
            }
        }

        public void Add(string Key, Response Item)
        {
            if (ItemsDictionary.ContainsKey(Key))
            {
                throw new Exception(string.Format("Key-{0} already exists!", Key));
            }
            ItemsDictionary[Key] = new SimilarityCheckerItem(Key, Item);
        }

        public void Check()
        {
            List<List<SimilarityCheckerItem>> CodeGroups = GroupByCode(new List<SimilarityCheckerItem>(ItemsDictionary.Values));
            
            List<List<SimilarityCheckerItem>> StrictLengthGroups = new List<List<SimilarityCheckerItem>>();
            List<List<SimilarityCheckerItem>> RelaxedLengthGroups = new List<List<SimilarityCheckerItem>>();
            
            foreach (List<SimilarityCheckerItem> Group in CodeGroups)
            {
                StrictLengthGroups.AddRange(StrictGroupByLength(Group));
                RelaxedLengthGroups.AddRange(RelaxedGroupByLength(Group));
            }
            
            List<List<SimilarityCheckerItem>> StrictContentGroups = new List<List<SimilarityCheckerItem>>();
            List<List<SimilarityCheckerItem>> RelaxedContentGroups = new List<List<SimilarityCheckerItem>>();
            foreach (List<SimilarityCheckerItem> Group in StrictLengthGroups)
            {
                StrictContentGroups.AddRange(StrictGroupByContent(Group));
            }

            foreach (List<SimilarityCheckerItem> Group in RelaxedLengthGroups)
            {
                RelaxedContentGroups.AddRange(RelaxedGroupByContent(Group));
            }

            StrictGroups.Clear();
            foreach (List<SimilarityCheckerItem> sig in StrictContentGroups)
            {
                SimilarityCheckerGroup SCG = new SimilarityCheckerGroup();
                foreach (SimilarityCheckerItem sci in sig)
                {
                    SCG.AddItem(sci);
                }
                StrictGroups.Add(SCG);
            }

            RelaxedGroups.Clear();
            foreach (List<SimilarityCheckerItem> rig in RelaxedContentGroups)
            {
                SimilarityCheckerGroup RCG = new SimilarityCheckerGroup();
                foreach (SimilarityCheckerItem rci in rig)
                {
                    RCG.AddItem(rci);
                }
                RelaxedGroups.Add(RCG);
            }
        }

        List<List<SimilarityCheckerItem>> GroupByCode(List<SimilarityCheckerItem> Items)
        {
            Dictionary<int, List<SimilarityCheckerItem>> CodeGroupDict = new Dictionary<int, List<SimilarityCheckerItem>>();
            List<List<SimilarityCheckerItem>> CodeGroups = new List<List<SimilarityCheckerItem>>();
            foreach (SimilarityCheckerItem Item in Items)
            {
                if (!CodeGroupDict.ContainsKey(Item.Res.Code))
                {
                    CodeGroupDict[Item.Res.Code] = new List<SimilarityCheckerItem>();
                }
                CodeGroupDict[Item.Res.Code].Add(Item);
            }
            foreach (int Key in CodeGroupDict.Keys)
            {
                CodeGroups.Add(CodeGroupDict[Key]);
            }
            return CodeGroups;
        }

        List<List<SimilarityCheckerItem>> StrictGroupByLength(List<SimilarityCheckerItem> Group)
        {
            Dictionary<int, List<SimilarityCheckerItem>> LengthGroupDict = new Dictionary<int, List<SimilarityCheckerItem>>();
            List<List<SimilarityCheckerItem>> LengthGroups = new List<List<SimilarityCheckerItem>>();
            if (Group.Count == 0)
            {
                return LengthGroups;
            }
            else if (Group.Count == 1)
            {
                LengthGroups.Add(Group);
                return LengthGroups;
            }

            foreach (SimilarityCheckerItem Item in Group)
            {
                if (!LengthGroupDict.ContainsKey(Item.Res.BodyLength))
                {
                    LengthGroupDict[Item.Res.BodyLength] = new List<SimilarityCheckerItem>();
                }
                LengthGroupDict[Item.Res.BodyLength].Add(Item);
            }

            foreach (int Key in LengthGroupDict.Keys)
            {
                LengthGroups.Add(LengthGroupDict[Key]);
            }
            return LengthGroups;
        }

        List<List<SimilarityCheckerItem>> RelaxedGroupByLength(List<SimilarityCheckerItem> Group)
        {
            Dictionary<int, List<int>> LengthGroupDict = new Dictionary<int, List<int>>();
            List<List<SimilarityCheckerItem>> LengthGroups = new List<List<SimilarityCheckerItem>>();
            if (Group.Count == 0)
            {
                return LengthGroups;
            }
            else if (Group.Count == 1)
            {
                LengthGroups.Add(Group);
                return LengthGroups;
            }

            int[] Lengths = new int[Group.Count];
            int i = 0;
            foreach (SimilarityCheckerItem Item in Group)
            {
                Lengths[i] = Item.Res.BodyLength;
                i++;
            }

            Array.Sort(Lengths);

            int CurrentKey = Lengths[0];

            LengthGroupDict[CurrentKey] = new List<int>();
            LengthGroupDict[CurrentKey].Add(Lengths[0]);

            for (int j = 0; j < Lengths.Length - 1; j++)
            {
                if (Tools.GetPercent(CurrentKey, Lengths[j + 1]) > 5)
                {
                    CurrentKey = Lengths[j + 1];
                    LengthGroupDict[CurrentKey] = new List<int>();
                }
                LengthGroupDict[CurrentKey].Add(Lengths[j + 1]);
            }

            List<string> GroupedKeys = new List<string>();

            foreach (int Key in LengthGroupDict.Keys)
            {
                List<SimilarityCheckerItem> SubGroup = new List<SimilarityCheckerItem>();
                foreach (int Length in LengthGroupDict[Key])
                {
                    foreach (SimilarityCheckerItem Item in Group)
                    {
                        if (Item.Res.BodyLength == Length && !GroupedKeys.Contains(Item.Key))
                        {
                            SubGroup.Add(Item);
                            GroupedKeys.Add(Item.Key);
                            break;
                        }
                    }
                }
                LengthGroups.Add(SubGroup);
            }
            return LengthGroups;
        }

        List<List<SimilarityCheckerItem>> StrictGroupByContent(List<SimilarityCheckerItem> Group)
        {
            List<List<SimilarityCheckerItem>> ContentGroups = new List<List<SimilarityCheckerItem>>();

            if (Group.Count == 0)
            {
                return ContentGroups;
            }
            else if (Group.Count == 1)
            {
                ContentGroups.Add(Group);
                return ContentGroups;
            }

            List<SimilarityCheckerItem> PendingMatch = new List<SimilarityCheckerItem>(Group);
            
            
            while(PendingMatch.Count > 0)
            {
                List<SimilarityCheckerItem> Matched = new List<SimilarityCheckerItem>();
                List<SimilarityCheckerItem> UnMatched = new List<SimilarityCheckerItem>();
                Matched.Add(PendingMatch[0]);
                for (int i = 1; i < PendingMatch.Count; i++)
                {
                    if (PendingMatch[0].Res.BodyString == PendingMatch[i].Res.BodyString)
                    {
                        Matched.Add(PendingMatch[i]);
                    }
                    else
                    {
                        UnMatched.Add(PendingMatch[i]);
                    }
                }
                PendingMatch = new List<SimilarityCheckerItem>(UnMatched);
                ContentGroups.Add(Matched);
            }

            return ContentGroups;
        }

        List<List<SimilarityCheckerItem>> RelaxedGroupByContent(List<SimilarityCheckerItem> Group)
        {
            List<List<SimilarityCheckerItem>> ContentGroups = new List<List<SimilarityCheckerItem>>();

            if (Group.Count == 0)
            {
                return ContentGroups;
            }
            else if (Group.Count == 1)
            {
                ContentGroups.Add(Group);
                return ContentGroups;
            }

            List<SimilarityCheckerItem> PendingMatch = new List<SimilarityCheckerItem>(Group);


            while (PendingMatch.Count > 0)
            {
                List<SimilarityCheckerItem> Matched = new List<SimilarityCheckerItem>();
                List<SimilarityCheckerItem> UnMatched = new List<SimilarityCheckerItem>();
                Matched.Add(PendingMatch[0]);
                for (int i = 1; i < PendingMatch.Count; i++)
                {
                    if (Tools.DiffLevel(PendingMatch[0].Res.BodyString, PendingMatch[i].Res.BodyString) < 4)
                    {
                        Matched.Add(PendingMatch[i]);
                    }
                    else
                    {
                        UnMatched.Add(PendingMatch[i]);
                    }
                }
                PendingMatch = new List<SimilarityCheckerItem>(UnMatched);
                ContentGroups.Add(Matched);
            }

            return ContentGroups;
        }

        
    }
}
