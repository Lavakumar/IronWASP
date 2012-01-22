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
using System.IO;
using System.Xml;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace IronWASP
{
    internal class EditorSyntaxModesProvider : ISyntaxModeFileProvider
    {
        List<SyntaxMode> syntaxModes = null;

        public ICollection<SyntaxMode> SyntaxModes
        {
            get
            {
                return syntaxModes;
            }
        }

        public EditorSyntaxModesProvider()
        {
            StreamReader ModesXml = new StreamReader(Config.RootDir + "\\SyntaxModes.xml");

            if (ModesXml.BaseStream != null)
            {
                syntaxModes = SyntaxMode.GetSyntaxModes(ModesXml.BaseStream);
            }
            else
            {
                syntaxModes = new List<SyntaxMode>();
            }
        }

        public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
        {
            XmlTextReader Xml = new XmlTextReader(syntaxMode.FileName);
            return Xml;
        }

        public void UpdateSyntaxModeList()
        {
            // resources don't change during runtime
        }
    }
}
