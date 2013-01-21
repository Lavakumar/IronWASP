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
    public class Findings
    {
        List<Finding> ResultList = new List<Finding>();
        public void Add(Finding PR)
        {
            this.ResultList.Add(PR);
        }
        public void Remove(Finding PR)
        {
            this.ResultList.Remove(PR);
        }
        public List<Finding> GetAll()
        {
            return ResultList;
        }
        public void Clear()
        {
            ResultList = new List<Finding>();
        }
    }
}
