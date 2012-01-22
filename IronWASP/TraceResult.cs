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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    public class TraceResult
    {
        public List<string> Lines = new List<string>();
        public List<string> SourceLines = new List<string>();
        public List<string> SinkLines = new List<string>();
        public List<string> SourceToSinkLines = new List<string>();
        public List<int> SourceLineNos = new List<int>();
        public List<int> SinkLineNos = new List<int>();
        public List<int> SourceToSinkLineNos = new List<int>();
        public List<string> KeywordContexts = new List<string>();
    }
}
