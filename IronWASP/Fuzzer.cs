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
    public class Fuzzer : Scanner
    {
        public Fuzzer(Request Req): base(Req)
        {
            base.LogSource = "Fuzzer";
        }

        public void SetLogSource(string LogSource)
        {
            if (LogSource.Equals("Scan"))
            {
                throw new Exception("Select another LogSource");
            }
            else
            {
                //If logsource is invalid this will throw an exception
                this.OriginalRequest.SetSource(LogSource);

                base.LogSource = LogSource;
            }
        }

        public static Fuzzer FromUi(Request Req)
        {
            Fuzzer F = new Fuzzer(Req);
            return F.FromUi();
        }

        public Fuzzer FromUi()
        {
            StartScanJobWizard SCJW = new StartScanJobWizard();
            SCJW.SetFuzzer(this);
            SCJW.ShowDialog();
            return SCJW.GetFuzzer();
        }
    }
}
