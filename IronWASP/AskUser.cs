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
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    public class AskUser
    {
        static Queue<AskUser> AskUserQueue = new Queue<AskUser>();

        internal ManualResetEvent MSR = new ManualResetEvent(false);

        internal string Title = "";
        internal string Message = "";
        internal string ImageFileLocation = "";
        internal List<string> List = new List<string>();
        internal string RBOne = "";
        internal string RBTwo = "";
        internal string Label = "";
        internal string ReturnType = "";
        internal bool BoolAnswer = false;
        internal string StringAnswer = "";
        internal List<int> ListAnswer = new List<int>();

        internal static AskUser CurrentlyAsked;
        internal static bool AskUserWindowFree = true;


        internal static int QueueLength
        {
            get
            {
                lock (AskUserQueue)
                {
                    return AskUserQueue.Count;
                }
            }
        }

        internal AskUser(string Title, string Message, string ReturnType)
        {
            this.Title = Title;
            this.Message = Message;
            this.ReturnType = ReturnType;
        }

        internal AskUser(string Title, string Message, string ImageFileLocation, string ReturnType)
        {
            this.Title = Title;
            this.Message = Message;
            this.ImageFileLocation = ImageFileLocation;
            this.ReturnType = ReturnType;
        }

        internal AskUser(string Title, string Message, List<string> List, string ReturnType)
        {
            this.Title = Title;
            this.Message = Message;
            this.List = new List<string>(List);
            this.ReturnType = ReturnType;
        }

        internal AskUser(string Title, string Message, string RBOne, string RBTwo, string Label, List<string> List, string ReturnType)
        {
            this.Title = Title;
            this.Message = Message;
            this.RBOne = RBOne;
            this.RBTwo = RBTwo;
            this.Label = Label;
            this.List = new List<string>(List);
            this.ReturnType = ReturnType;
        }

        public static bool ForBool(string Title, string Message)
        {
            AskUser AU = new AskUser(Title, Message, "Bool");
            AU.Ask();
            return AU.BoolAnswer;
        }

        public static string ForString(string Title, string Message)
        {
            AskUser AU = new AskUser(Title, Message, "String");
            AU.Ask();
            return AU.StringAnswer;
        }

        public static string ForString(string Title, string Message, string ImageFileLocation)
        {
            AskUser AU = new AskUser(Title, Message, ImageFileLocation, "String");
            AU.Ask();
            return AU.StringAnswer;
        }

        public static List<string> ForListValues(string Title, string Message, List<string> List)
        {
            List<string> ResultList = new List<string>();
            List<int> Positions = ForList(Title, Message, List);
            foreach (int Position in Positions)
            {
                ResultList.Add(List[Position]);
            }
            return ResultList;
        }

        public static List<int> ForList(string Title, string Message, List<string> List)
        {
            AskUser AU = new AskUser(Title, Message, List, "List");
            AU.Ask();
            return AU.ListAnswer;
        }

        public static List<int> ForList(string Title, string Message, string RBOne, string RBTwo, string Label, List<string> List)
        {
            AskUser AU = new AskUser(Title, Message, RBOne, RBTwo, Label, List, "List");
            AU.Ask();
            return AU.ListAnswer;
        }

        internal void Ask()
        {
            lock (AskUserQueue)
            {
                AskUserQueue.Enqueue(this);
            }
            IronUI.AskUser();
            MSR.WaitOne();
        }

        internal static AskUser GetNext()
        {
            try
            {
                AskUser AU;
                lock (AskUserQueue)
                {
                    AU = AskUserQueue.Dequeue();
                }
                return AU;
            }
            catch
            {
                return null;
            }
        }
    }
}
