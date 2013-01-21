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
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    public class IronException
    {
        internal int ID=0;
        internal string Title = "";
        internal string Message = "";
        internal string StackTrace = "";

        public IronException()
        {

        }

        public IronException(string Title, string Message)
        {
            this.Title = Title;
            this.Message = Message;
        }

        public IronException(string Title, string Message, string StackTrace)
        {
            this.Title = Title;
            this.Message = Message;
            this.StackTrace = StackTrace;
        }

        public static void Report(string Title, string Message)
        {
            Report(Title, Message, "");
        }
        
        internal static void Report(string Title, Exception InnerException)
        {
            Report(Title, InnerException.Message, InnerException.StackTrace);
        }

        internal static void Report(string Title, string Message, string StackTrace)
        {
            try
            {
                int ExceptionID = Interlocked.Increment(ref Config.ExceptionsCount);
                IronException IrEx = new IronException();
                IrEx.ID = ExceptionID;
                IrEx.Title = Title;
                IrEx.Message = Message;
                IrEx.StackTrace = StackTrace;
                IronDB.LogException(IrEx);
                IronUI.UpdateException(IrEx);
            }
            catch
            {
                //End of the road
            }
        }        
    }
}
