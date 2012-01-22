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
    public class HTTPMessage
    {
        public string FirstHeader;
        public HeaderParameters Headers;
        public bool HasBody;
        public string BodyString;
        public HTTPMessage(string HTTPInput)
        {
            string[] MessageParts = HTTPInput.Split(new string[] { "\r\n\r\n" }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (MessageParts.Length == 0) throw new Exception("Invalid HTTP Message Header");            
            MessageParts[0] += "\r\n";
            string[] HeaderArray = MessageParts[0].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (HeaderArray.Length == 0) throw new Exception("Invalid HTTP Message Header");
            FirstHeader = HeaderArray[0];
            if (FirstHeader.Length == 0) throw new Exception("Invalid HTTP Message Header");
            HeaderArray[0] = "";
            Headers = new HeaderParameters(HeaderArray);
            if (MessageParts.Length == 2)
            {
                HasBody = true;
                BodyString = MessageParts[1];
            }
        }
        public static byte[] GetFullMessageAsByteArray(byte[] Header, byte[] Body)
        {
            if (Header == null)
            {
                throw new Exception("Null header array passed to 'GetFullMessageAsByteArray' function");
            }
            if (Body == null)
            {
                Body = new byte[0];
            }
            byte[] FullArray = new byte[Header.Length + Body.Length];
            Header.CopyTo(FullArray, 0);
            Body.CopyTo(FullArray,Header.Length);
            return FullArray;
        }
    }
}
