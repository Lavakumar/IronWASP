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
using Jint.Debugger;
using System.Text;
using Jint;
using Jint.Expressions;

namespace IronWASP
{
    internal class JintItem
    {
        internal int LineNo = 0;
        internal int CharNo = 0;
        internal JintState State = JintState.Identifier;

        internal string Value = "";
        internal bool Tainted = false;
        internal bool Source = false;
        internal bool Sink = false;

        internal List<JintItem> SubItems = new List<JintItem>();
        internal List<Statement> SubStatements = new List<Statement>();//to hold the body of function declarations

        internal List<string> SourceReasons = new List<string>();
        internal List<string> SinkReasons = new List<string>();

        internal List<JintItem> ItemBody = new List<JintItem>();

        internal List<List<JintItem>> LocalVariables = new List<List<JintItem>>();

        internal JintItem(int LineNo, int CharNo, JintState State)
        {
            this.LineNo = LineNo;
            this.CharNo = CharNo;
            this.State = State;
        }

        internal JintItem(int LineNo, int CharNo, JintState State, string Value)
        {
            this.LineNo = LineNo;
            this.CharNo = CharNo;
            this.State = State;
            this.Value = Value;
        }

        internal JintItem(SourceCodeDescriptor Source, JintState State, IronJint IJ)
        {
            if (Source != null)
            {
                this.LineNo = Source.Start.Line;
                this.CharNo = Source.Start.Char;
            }
            else
            {
                this.LineNo = IJ.CurrentLineNo;
                this.CharNo = IJ.CurrentCharNo;
            }
            this.State = State;
        }

        internal JintItem(SourceCodeDescriptor Source, JintState State, string Value, IronJint IJ)
        {
            if (Source != null)
            {
                this.LineNo = Source.Start.Line;
                this.CharNo = Source.Start.Char;
            }
            else
            {
                this.LineNo = IJ.CurrentLineNo;
                this.CharNo = IJ.CurrentCharNo;
            }
            this.State = State;
            this.Value = Value;
        }
    }
}
