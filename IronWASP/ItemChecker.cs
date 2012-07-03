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
// along with IronWASP.  If not, see http://www.gnu.org/licenses/.
//

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    internal class ItemChecker
    {
        IronJint IJ = new IronJint();

        internal ItemChecker(IronJint IJ)
        {
            this.IJ = IJ;
        }
        
        internal TaintResult Check(List<JintItem> Item)
        {
            TaintResult Result = new TaintResult();

            List<List<JintItem>> ItemParts = GetItemParts(Item);
           
            foreach (List<JintItem> Part in ItemParts)
            {
                Result.Add(IsTainted(Part));
                //When tracing keyword if keyword is inside string value even then it should be treated as a source
                if (IJ.TraceKeyword)
                {
                    foreach (JintItem PartItem in Part)
                    {
                        if ((PartItem.State == JintState.StringValue || PartItem.State == JintState.IntValue) && PartItem.Value.IndexOf(IJ.KeywordToTrace, StringComparison.OrdinalIgnoreCase) > -1)
                            Result.SourceReasons.Add("Matches with Keyword being traced");
                    }
                }
            }
            return Result;
        }

        TaintResult IsTainted(List<JintItem> Item)
        {
            TaintResult Result = new TaintResult();

            //Check if Item matches any of the SourceObjects
            foreach (List<JintItem> SourceItem in IJ.SourceObjects)
            {
                TaintResult SourceTaintResult = IsMatch(Item, SourceItem);
                if(SourceTaintResult.NeutralReasons.Count > 0) Result.AddAsSource(SourceItem, "Matches SourceObject - " + GetItemAsString(SourceItem));
            }


            //Check if Item matches any of the SinkObjects
            foreach (List<JintItem> SinkItem in IJ.SinkObjects)
            {
                TaintResult SinkTaintResult = IsMatch(Item, SinkItem);
                if (SinkTaintResult.NeutralReasons.Count > 0) Result.AddAsSink(SinkItem, "Matches SinkObject - " + GetItemAsString(SinkItem));
            }
            
            //Check if Item is a method call, then method call related checks can be made
            List<JintItem> Arguments = GetLastMethodArguments(Item);
            if (Arguments.Count > 0)
            {
                //Item is a method.
                //Check if Item matches any SourceReturningMethods
                foreach (List<JintItem> SourceItem in IJ.SourceReturningMethods)
                {
                    TaintResult SourceTaintResult = IsMatch(Item, SourceItem);
                    if (SourceTaintResult.NeutralReasons.Count > 0) Result.AddAsSource(SourceItem, "Matches Source Returning Method - " + GetItemAsString(SourceItem));
                }
                //Check if Item matches any SinkReturningMethods
                foreach (List<JintItem> SinkItem in IJ.SinkReturningMethods)
                {
                    TaintResult SinkTaintResult = IsMatch(Item, SinkItem);
                    if (SinkTaintResult.NeutralReasons.Count > 0) Result.AddAsSink(SinkItem, "Matches Sink Returning Method - " + GetItemAsString(SinkItem));
                }

                //Now to check with ArgumentReturningMethods
                //First check if any of the arguments of Item are Sources or Sinks
                List<int> SourceTaintedArgumentPositions = new List<int>();
                List<int> SinkTaintedArgumentPositions = new List<int>();
                for(int i=0; i<Arguments.Count;i++)
                {
                    TaintResult ArgResult = IsTainted(Arguments[i].SubItems);
                    if (ArgResult.SourceTaint) SourceTaintedArgumentPositions.Add(i);
                    if (ArgResult.SinkTaint) SinkTaintedArgumentPositions.Add(i);
                }
                //If there are any Sources or Sinks then check if Item matches the ArgumentReturningMethods
                if (SourceTaintedArgumentPositions.Count > 0 || SinkTaintedArgumentPositions.Count > 0)
                {
                    foreach (List<JintItem> MethodItem in IJ.ArgumentReturningMethods)
                    {
                        TaintResult MethodReturnResult = DoItemsMatch(Item, MethodItem);
                        //If Item matches any ArgumentReturningMethod item check if the TaintPointer position matches the source or sink argument posistions in Item
                        if (MethodReturnResult.NeutralReasons.Count > 0)
                        {
                            List<JintItem> TemplateArguments = GetLastMethodArguments(MethodItem);
                            if (TemplateArguments.Count == Arguments.Count)
                            {
                                for (int j = 0; j < TemplateArguments.Count; j++)
                                {
                                    if (TemplateArguments[j].SubItems.Count == 1)
                                    {
                                        if (TemplateArguments[j].SubItems[0].State == JintState.MethodCallArgumentTaintPointer)
                                        {
                                            if (SourceTaintedArgumentPositions.Contains(j))
                                                Result.AddAsSource(MethodItem, "Method - " + GetItemAsString(MethodItem) + " - returns a Source passed in at argument position " + (j + 1).ToString());
                                            if (SinkTaintedArgumentPositions.Contains(j))
                                                Result.AddAsSink(MethodItem, "Method - " + GetItemAsString(MethodItem) + " - returns a Sink passed in at argument position " + (j + 1).ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int LineNo = 0;
            if (Item.Count > 0) LineNo = Item[0].LineNo;
            if (LineNo == 0) LineNo = IJ.CurrentLineNo;
            if (IronJint.SourceLinesToIgnore.Contains(LineNo)) Result.SourceReasons.Clear();
            if (IronJint.SourceLinesToInclude.Contains(LineNo)) Result.SourceReasons.Add("Source set by User");
            if (IronJint.SinkLinesToIgnore.Contains(LineNo)) Result.SinkReasons.Clear();
            if (IronJint.SinkLinesToInclude.Contains(LineNo)) Result.SinkReasons.Add("Sink set by User");

            return Result;
        }

        internal TaintResult IsMatch(List<JintItem> Item, List<JintItem> Template)
        {
            TaintResult Result = new TaintResult();

            if (Item.Count == 0) return Result;
            if (Template.Count == 0) return Result;

            if ((Item[0].State != JintState.Identifier) && (Item[0].State != JintState.MethodCallName))
            {
                //MessageBox.Show("Item starts with - " + Item[0].State.ToString());
                return Result;
            }
            if ((Template[0].State != JintState.Identifier) && (Template[0].State != JintState.MethodCallName))
            {
                //MessageBox.Show("Template starts with - " + Template[0].State.ToString());
                return Result;
            }

            int ItemMatchIndex = 0;

            while (ItemMatchIndex < Item.Count)
            {
                switch (Template[0].State)
                {
                    case (JintState.Identifier):
                        switch (Item[ItemMatchIndex].State)
                        {
                            case (JintState.Identifier):
                            case (JintState.Property):
                            case (JintState.StringIndex):
                                if (Item[ItemMatchIndex].Value.Equals(Template[0].Value))
                                    Result.Add(DoItemsMatch(Item.GetRange(ItemMatchIndex, Item.Count - ItemMatchIndex), Template));
                                break;
                            case (JintState.IdentifierIndex):
                            case (JintState.Indexer):
                                string IndexValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexValue.Length > 0 && IndexValue.Equals(Template[0].Value))
                                    Result.Add(DoItemsMatch(Item.GetRange(ItemMatchIndex, Item.Count - ItemMatchIndex), Template));
                                break;
                        }
                        break;
                    case (JintState.MethodCallName):
                        switch (Item[ItemMatchIndex].State)
                        {
                            case (JintState.MethodCallName):
                            case (JintState.StringIndex):
                                if (Item[ItemMatchIndex].Value.Equals(Template[0].Value))
                                    Result.Add(DoItemsMatch(Item.GetRange(ItemMatchIndex, Item.Count - ItemMatchIndex), Template));
                                break;
                            case (JintState.IdentifierIndex):
                            case (JintState.Indexer):
                                string IndexValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexValue.Length > 0 && IndexValue.Equals(Template[0].Value))
                                    Result.Add(DoItemsMatch(Item.GetRange(ItemMatchIndex, Item.Count - ItemMatchIndex), Template));
                                break;
                        }
                        break;
                }
                ItemMatchIndex++;
            }
            return Result;
        }

        internal TaintResult DoItemsMatch(List<JintItem> Item, List<JintItem> Template)
        {
            return DoItemsMatch(Item, Template, true);
        }

        internal TaintResult DoItemsMatch(List<JintItem> Item, List<JintItem> Template, bool MethodCall)
        {
            TaintResult Result = new TaintResult();
            int ItemMatchIndex = 0;
            int TemplateMatchIndex = 0;

            bool MatchFailed = false;
            while ((ItemMatchIndex < Item.Count) && (TemplateMatchIndex < Template.Count) && !MatchFailed)
            {
                switch (Template[TemplateMatchIndex].State)
                {
                    case (JintState.Identifier):
                        switch (Item[ItemMatchIndex].State)
                        {
                            case (JintState.Identifier):
                            case (JintState.Property):
                            case (JintState.StringIndex):
                                if (Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.IdentifierIndex):
                            case (JintState.Indexer):
                                string IndexValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexValue.Length > 0 && IndexValue.Equals(Template[TemplateMatchIndex].Value))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.MethodCallName):
                                if (MethodCall)
                                    MatchFailed = true;
                                else
                                {
                                    if (Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                        Result.NeutralReasons.Add(Item[ItemMatchIndex].Value); 
                                    else
                                        MatchFailed = true;
                                }
                                break;
                            default:
                                MatchFailed = true;
                                break;
                        }
                        break;
                    case (JintState.Property):
                    case (JintState.StringIndex):
                        switch (Item[ItemMatchIndex].State)
                        {
                            case (JintState.Property):
                            case (JintState.StringIndex):
                                if (Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.IdentifierIndex):
                            case (JintState.Indexer):
                                string IndexValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexValue.Length > 0 && IndexValue.Equals(Template[TemplateMatchIndex].Value))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.MethodCallName):
                                if (MethodCall)
                                {
                                    if (Template.Count > TemplateMatchIndex + 1 && Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                    {
                                        if (Template[TemplateMatchIndex + 1].State == JintState.MethodCallName && Template[TemplateMatchIndex + 1].Value.Length == 0)
                                        {
                                            TemplateMatchIndex++;
                                            Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                            List<JintItem> ItemArguments = GetMethodArguments(Item.GetRange(ItemMatchIndex + 1, Item.Count - (ItemMatchIndex + 1)));
                                            List<JintItem> TemplateArguments = GetMethodArguments(Template.GetRange(TemplateMatchIndex + 1, Template.Count - (TemplateMatchIndex + 1)));
                                            ItemMatchIndex = ItemMatchIndex + ItemArguments.Count;
                                            TemplateMatchIndex = TemplateMatchIndex + TemplateArguments.Count;
                                        }
                                        else
                                            MatchFailed = true;
                                    }
                                    else
                                        MatchFailed = true;
                                }
                                else
                                {
                                    if (Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                        Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                    else
                                        MatchFailed = true;
                                }
                                break;
                            default:
                                MatchFailed = true;
                                break;
                        }
                        break;
                    case(JintState.IdentifierIndex):
                        string TemplateItemValue = GetIndexStringValue(Template[TemplateMatchIndex]);    
                        switch (Item[ItemMatchIndex].State)
                        {
                            case (JintState.Property):
                            case (JintState.StringIndex):
                                if (Item[ItemMatchIndex].Value.Equals(TemplateItemValue))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.IdentifierIndex):
                                string IndexValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else if (IndexValue.Length > 0 && IndexValue.Equals(TemplateItemValue))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.Indexer):
                                string IndexerValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexerValue.Length > 0 && IndexerValue.Equals(TemplateItemValue))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.MethodCallName):
                                if (Template.Count > TemplateMatchIndex + 1 && Item[ItemMatchIndex].Value.Equals(TemplateItemValue))
                                {
                                    if (Template[TemplateMatchIndex + 1].State == JintState.MethodCallName && Template[TemplateMatchIndex + 1].Value.Length == 0)
                                    {
                                        TemplateMatchIndex++;
                                        Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                        List<JintItem> ItemArguments = GetMethodArguments(Item.GetRange(ItemMatchIndex + 1, Item.Count - (ItemMatchIndex + 1)));
                                        List<JintItem> TemplateArguments = GetMethodArguments(Template.GetRange(TemplateMatchIndex + 1, Template.Count - (TemplateMatchIndex + 1)));
                                        ItemMatchIndex = ItemMatchIndex + ItemArguments.Count;
                                        TemplateMatchIndex = TemplateMatchIndex + TemplateArguments.Count;
                                    }
                                    else
                                        MatchFailed = true;
                                }
                                else
                                    MatchFailed = true;
                                break;
                            default:
                                MatchFailed = true;
                                break;
                        }
                        break;
                    case (JintState.Indexer):
                        string TemplateIndexerValue = GetIndexStringValue(Template[TemplateMatchIndex]);
                        switch (Item[ItemMatchIndex].State)
                        {
                            case (JintState.StringIndex):
                                if (Item[ItemMatchIndex].Value.Equals(TemplateIndexerValue))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.IdentifierIndex):
                                string IndexValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexValue.Length > 0 && IndexValue.Equals(TemplateIndexerValue))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.Indexer):
                                string IndexerValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IsMatch(Item[ItemMatchIndex].SubItems, Template[TemplateMatchIndex].SubItems).NeutralReasons.Count > 0)
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else if (IndexerValue.Length > 0 && IndexerValue.Equals(TemplateIndexerValue))
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.MethodCallName):
                                if (Template.Count > TemplateMatchIndex + 1 && Item[ItemMatchIndex].Value.Equals(TemplateIndexerValue))
                                {
                                    if (Template[TemplateMatchIndex + 1].State == JintState.MethodCallName && Template[TemplateMatchIndex + 1].Value.Length == 0)
                                    {
                                        TemplateMatchIndex++;
                                        Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                        List<JintItem> ItemArguments = GetMethodArguments(Item.GetRange(ItemMatchIndex + 1, Item.Count - (ItemMatchIndex + 1)));
                                        List<JintItem> TemplateArguments = GetMethodArguments(Template.GetRange(TemplateMatchIndex + 1, Template.Count - (TemplateMatchIndex + 1)));
                                        ItemMatchIndex = ItemMatchIndex + ItemArguments.Count;
                                        TemplateMatchIndex = TemplateMatchIndex + TemplateArguments.Count;
                                    }
                                    else
                                        MatchFailed = true;
                                }
                                else
                                    MatchFailed = true;
                                break;
                            default:
                                MatchFailed = true;
                                break;
                        }
                        break;
                    case (JintState.MethodCallName):
                        switch (Item[ItemMatchIndex].State)
                        {
                            case (JintState.MethodCallName):
                                if (MethodCall)
                                {
                                    if (Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                    {
                                        Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                        List<JintItem> ItemArguments = GetMethodArguments(Item.GetRange(ItemMatchIndex + 1, Item.Count - (ItemMatchIndex + 1)));
                                        List<JintItem> TemplateArguments = GetMethodArguments(Template.GetRange(TemplateMatchIndex + 1, Template.Count - (TemplateMatchIndex + 1)));
                                        ItemMatchIndex = ItemMatchIndex + ItemArguments.Count;
                                        TemplateMatchIndex = TemplateMatchIndex + TemplateArguments.Count;
                                    }
                                    else
                                        MatchFailed = true;
                                }
                                else
                                    MatchFailed = true;

                                break;
                            case (JintState.StringIndex):
                                if (Item[ItemMatchIndex].Value.Equals(Template[TemplateMatchIndex].Value))
                                {
                                    if (Item.Count > ItemMatchIndex + 1)
                                    {
                                        if (Item[ItemMatchIndex + 1].State == JintState.MethodCallName && Item[ItemMatchIndex + 1].Value.Length == 0 && MethodCall)
                                        {
                                            ItemMatchIndex++;
                                            Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                            List<JintItem> ItemArguments = GetMethodArguments(Item.GetRange(ItemMatchIndex + 1, Item.Count - (ItemMatchIndex + 1)));
                                            List<JintItem> TemplateArguments = GetMethodArguments(Template.GetRange(TemplateMatchIndex + 1, Template.Count - (TemplateMatchIndex + 1)));
                                            ItemMatchIndex = ItemMatchIndex + ItemArguments.Count;
                                            TemplateMatchIndex = TemplateMatchIndex + TemplateArguments.Count;
                                        }
                                        else
                                            MatchFailed = true;
                                    }
                                    else
                                    {
                                        if(MethodCall) MatchFailed = true;
                                    }
                                }
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.IdentifierIndex):
                                string IndexValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexValue.Length > 0 && IndexValue.Equals(Template[TemplateMatchIndex].Value))
                                {
                                    if (Item.Count > ItemMatchIndex + 1)
                                    {
                                        if (Item[ItemMatchIndex + 1].State == JintState.MethodCallName && Item[ItemMatchIndex + 1].Value.Length == 0 && MethodCall)
                                        {
                                            ItemMatchIndex++;
                                            Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                            List<JintItem> ItemArguments = GetMethodArguments(Item.GetRange(ItemMatchIndex + 1, Item.Count - (ItemMatchIndex + 1)));
                                            List<JintItem> TemplateArguments = GetMethodArguments(Template.GetRange(TemplateMatchIndex + 1, Template.Count - (TemplateMatchIndex + 1)));
                                            ItemMatchIndex = ItemMatchIndex + ItemArguments.Count;
                                            TemplateMatchIndex = TemplateMatchIndex + TemplateArguments.Count;
                                        }
                                        else
                                            MatchFailed = true;
                                    }
                                    else
                                    {
                                        if(MethodCall) MatchFailed = true;
                                    }
                                }
                                else
                                    MatchFailed = true;
                                break;
                            case (JintState.Indexer):
                                string IndexerValue = GetIndexStringValue(Item[ItemMatchIndex]);
                                if (IndexerValue.Length > 0 && IndexerValue.Equals(Template[TemplateMatchIndex].Value))
                                {
                                    if (Item.Count > ItemMatchIndex + 1)
                                    {
                                        if (Item[ItemMatchIndex + 1].State == JintState.MethodCallName && Item[ItemMatchIndex + 1].Value.Length == 0 && MethodCall)
                                        {
                                            ItemMatchIndex++;
                                            Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                            List<JintItem> ItemArguments = GetMethodArguments(Item.GetRange(ItemMatchIndex + 1, Item.Count - (ItemMatchIndex + 1)));
                                            List<JintItem> TemplateArguments = GetMethodArguments(Template.GetRange(TemplateMatchIndex + 1, Template.Count - (TemplateMatchIndex + 1)));
                                            ItemMatchIndex = ItemMatchIndex + ItemArguments.Count;
                                            TemplateMatchIndex = TemplateMatchIndex + TemplateArguments.Count;
                                        }
                                        else
                                            MatchFailed = true;
                                    }
                                    else
                                    {
                                        if(MethodCall) MatchFailed = true;
                                    }
                                }
                                else
                                    MatchFailed = true;
                                break;
                            case(JintState.Property):
                                if (!MethodCall && Item.Count == ItemMatchIndex + 1)
                                    Result.NeutralReasons.Add(Item[ItemMatchIndex].Value);
                                else
                                    MatchFailed = true;
                                break;
                            default:
                                MatchFailed = true;
                                break;
                        }
                        break;
                    default:
                        MatchFailed = true;
                        break;
                }
                ItemMatchIndex++;
                TemplateMatchIndex++;
            }
            if (TemplateMatchIndex != Template.Count) MatchFailed = true;
            if (MatchFailed)
            {
                Result.NeutralReasons.Clear();
                Result.SourceReasons.Clear();
                Result.SinkReasons.Clear();
                Result.MethodNameReasons.Clear();
                Result.MethodArgumentSinkReasons.Clear();
                Result.MethodArgumentSourceReasons.Clear();
                Result.MethodArgumentNeutralReasons.Clear();
                Result.SourceToSinkReasons.Clear();
            }
            return Result;
        }

        string GetIndexStringValue(JintItem IndexItem)
        {
            string IndexValue = "";
            switch(IndexItem.State)
            {
                case (JintState.IdentifierIndex):
                    IndexValue = IJ.GetStringObject(new List<JintItem>() { new JintItem(IndexItem.LineNo, IndexItem.CharNo, JintState.Identifier, IndexItem.Value) });
                    break;
                case (JintState.Indexer):
                    IndexValue = IJ.GetStringObject(IndexItem.SubItems);
                    break;
            }
            return IndexValue;
        }

        List<JintItem> GetMethodArguments(List<JintItem> Item)
        {
            List<JintItem> Result = new List<JintItem>();
            int i=0;
            while(i < Item.Count)
            {
                if (Item[i].State == JintState.MethodCallArgument)
                    Result.Add(Item[i]);
                else
                    break;
                i++;
            }
            return Result;
        }

        internal List<JintItem> GetLastMethodArguments(List<JintItem> Item)
        {
            List<JintItem> Result = new List<JintItem>();
            int i = Item.Count - 1;
            while (i >= 0)
            {
                if (Item[i].State == JintState.MethodCallArgument)
                    Result.Add(Item[i]);
                else
                    break;
                i--;
            }
            Result.Reverse();
            return Result;
        }

        TaintResult IsMethodArgumentsMatch(List<JintItem> ItemArguments, List<JintItem> TemplateArguments)
        {
            TaintResult Result = new TaintResult();
            if (ItemArguments.Count != TemplateArguments.Count) return Result;

            for (int i = 0; i < ItemArguments.Count; i++)
            {
                if((ItemArguments[i].SubItems.Count > 0) && (TemplateArguments[i].SubItems.Count > 0))
                {
                    if (TemplateArguments[i].SubItems[0].State == JintState.MethodCallArgumentTaintPointer)
                    {
                        Result.Add(Check(ItemArguments[i].SubItems));
                    }
                }
            }

            return Result;
        }

        internal static List<List<JintItem>> GetItemParts(List<JintItem> Item)
        {
            List<List<JintItem>> Parts = new List<List<JintItem>>();
            List<JintItem> Part = new List<JintItem>();
            foreach (JintItem SubItem in Item)
            {
                if (SubItem.State == JintState.BinaryOperator)
                {
                    if (Part.Count > 0) Parts.Add(new List<JintItem>(Part));
                    Part.Clear();
                }
                else
                {
                    Part.Add(SubItem);
                }
            }
            Parts.Add(new List<JintItem>(Part));
            return Parts;
        }

        internal static bool IsSimilar(List<JintItem> First, List<JintItem> Second)
        {
            if (First.Count != Second.Count) return false;
            for (int i = 0; i < First.Count; i++)
            {
                if (First[i].State != Second[i].State) return false;
                if (First[i].Value != Second[i].Value) return false;
                if (First[i].SubItems.Count != Second[i].SubItems.Count)
                {
                    return false;
                }
                else
                {
                    if (!IsSimilar(First[i].SubItems, Second[i].SubItems)) return false;
                }
            }
            return true;
        }

        internal static string GetItemAsString(List<JintItem> Item)
        {
            StringBuilder StringItem = new StringBuilder();
            foreach (JintItem ItemPart in Item)
            {
                switch(ItemPart.State)
                {
                    case(JintState.Identifier):
                    case (JintState.StringValue):
                        StringItem.Append(ItemPart.Value);
                        break;
                    case (JintState.Property):
                        StringItem.Append(".");StringItem.Append(ItemPart.Value);
                        break;
                    case (JintState.StringIndex):
                    case (JintState.IntIndex):
                    case (JintState.IdentifierIndex):
                        StringItem.Append("[");StringItem.Append(ItemPart.Value);StringItem.Append("]");
                        break;
                    case (JintState.Indexer):
                        StringItem.Append("["); GetItemAsString(ItemPart.SubItems); StringItem.Append("]");
                        break;
                    case (JintState.MethodCallName):
                         StringItem.Append(ItemPart.Value);StringItem.Append("()");
                        break;
                }
            }
            return StringItem.ToString();
        }
    }
}
