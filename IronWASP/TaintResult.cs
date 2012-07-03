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
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    internal class TaintResult
    {
        internal List<string> SourceReasons = new List<string>();
        internal List<string> SinkReasons = new List<string>();

        internal List<string> NeutralReasons = new List<string>();

        internal List<string> MethodNameReasons = new List<string>();
        internal List<string> MethodArgumentSinkReasons = new List<string>();
        internal List<string> MethodArgumentSourceReasons = new List<string>();
        internal List<string> MethodArgumentNeutralReasons = new List<string>();

        internal List<string> SourceToSinkReasons = new List<string>();

        internal bool Tainted
        {
            get
            {
                if (SourceTaint || SinkTaint)
                    return true;
                else
                    return false;

            }
        }

        internal bool SourceTaint
        {
            get
            {
                return (SourceReasons.Count > 0);
            }
        }

        internal bool SinkTaint
        {
            get
            {
                return (SinkReasons.Count > 0);
            }
        }

        internal void Add(TaintResult Result)
        {
            if (Result.SourceTaint) this.SourceReasons.AddRange(Result.SourceReasons);
            if (Result.SinkTaint) this.SinkReasons.AddRange(Result.SinkReasons);
            if(Result.NeutralReasons.Count > 0) this.NeutralReasons.AddRange(Result.NeutralReasons);
            if (Result.MethodNameReasons.Count > 0) this.MethodNameReasons.AddRange(Result.MethodNameReasons);
            if (Result.MethodArgumentSourceReasons.Count > 0) this.MethodArgumentSourceReasons.AddRange(Result.MethodArgumentSourceReasons);
            if (Result.MethodArgumentSinkReasons.Count > 0) this.MethodArgumentSinkReasons.AddRange(Result.MethodArgumentSinkReasons);
            if (Result.MethodArgumentNeutralReasons.Count > 0) this.MethodArgumentNeutralReasons.AddRange(Result.MethodArgumentNeutralReasons);
        }

        internal void AddAsSource(TaintResult Result)
        {
            if (Result.SourceTaint) this.SourceReasons.AddRange(Result.SourceReasons);
            if (Result.SinkTaint) this.SinkReasons.AddRange(Result.SinkReasons);
            if (Result.NeutralReasons.Count > 0) this.SourceReasons.AddRange(Result.NeutralReasons);
            if(Result.MethodNameReasons.Count > 0)
            {
                this.SourceReasons.AddRange(Result.MethodNameReasons);
            }
        }

        internal void AddAsSource(List<JintItem> Item, string Reason)
        {
            this.SourceReasons.Add(Reason);
            if (Item.Count > 0) this.SourceReasons.AddRange(Item[0].SourceReasons);
        }

        internal void AddAsSink(TaintResult Result)
        {
            if (Result.SourceTaint) this.SourceReasons.AddRange(Result.SourceReasons);
            if (Result.SinkTaint) this.SinkReasons.AddRange(Result.SinkReasons);
            if (Result.NeutralReasons.Count > 0) this.SinkReasons.AddRange(Result.NeutralReasons);
            if (Result.MethodArgumentSourceReasons.Count > 0)
            {
                this.SourceToSinkReasons.AddRange(Result.MethodNameReasons);
                this.SourceToSinkReasons.AddRange(Result.MethodArgumentSourceReasons);
            }
            else if (Result.MethodNameReasons.Count > 0)
            {
                this.SinkReasons.AddRange(Result.MethodNameReasons);
            }
        }

        internal void AddAsSink(List<JintItem> Item, string Reason)
        {
            this.SinkReasons.Add(Reason);
            if (Item.Count > 0) this.SinkReasons.AddRange(Item[0].SinkReasons);
        }

        internal void AddToMethodArgumentReasons(TaintResult Result)
        {
            if (Result.SinkReasons.Count > 0) this.MethodArgumentSinkReasons.AddRange(Result.SinkReasons);
            if (Result.SourceReasons.Count > 0) this.MethodArgumentSourceReasons.AddRange(Result.SourceReasons);
            if (Result.NeutralReasons.Count > 0) this.MethodArgumentNeutralReasons.AddRange(Result.NeutralReasons);
        }

        internal void Clear()
        {
            this.SinkReasons.Clear();
            this.SourceReasons.Clear();
            this.NeutralReasons.Clear();
        }

    }
}
