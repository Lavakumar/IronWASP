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
    public class SimilarityCheckerGroup
    {
        public List<SimilarityCheckerItem> Items
        {
            get
            {
                List<SimilarityCheckerItem> items = new List<SimilarityCheckerItem>();
                foreach (string Key in ItemDict.Keys)
                {
                    SimilarityCheckerItem Item = new SimilarityCheckerItem(Key, ItemDict[Key]);
                    items.Add(Item);
                }
                return items;
            }
        }

        public int Count
        {
            get
            {
                return ItemDict.Count;
            }
        }

        public string Signature
        {
            get
            {
                string[] k = new string[ItemDict.Keys.Count];
                ItemDict.Keys.CopyTo(k, 0);
                string AllKeysStr = string.Join(", ", k);
                return string.Format("[{0}]", AllKeysStr);
            }
        }

        Dictionary<string, Response> ItemDict = new Dictionary<string, Response>();
        
        public List<string> GetKeys()
        {
            List<string> Keys = new List<string>(ItemDict.Keys);
            return Keys;
        }

        public bool HasKey(string Key)
        {
            return ItemDict.ContainsKey(Key);
        }

        internal void AddItem(SimilarityCheckerItem Item)
        {
            if (ItemDict.ContainsKey(Item.Key))
            {
                throw new Exception("Key already exists");
            }
            ItemDict[Item.Key] = Item.Res;
        }

        internal void AddItem(string Key, Response Res)
        {
            SimilarityCheckerItem Item = new SimilarityCheckerItem(Key, Res);
            AddItem(Item);
        }
    }
}
