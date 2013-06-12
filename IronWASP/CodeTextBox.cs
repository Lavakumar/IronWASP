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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IronWASP
{
    public partial class CodeTextBox : UserControl
    {
        int langCode = 1;

        public CodeTextBox()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return Editor.Text;
            }
            set
            {
                Editor.Text = value;
                Editor.Refresh();
            }
        }

        public bool ReadOnly
        {
            get
            {
                return Editor.Document.ReadOnly;
            }
            set
            {
                Editor.Document.ReadOnly = value;
            }
        }

        public bool ShowLineNumbers
        {
            get
            {
                return Editor.ShowLineNumbers;
            }
            set
            {
                Editor.ShowLineNumbers = value;
            }
        }

        public bool ShowSpacesAndTabs
        {
            get
            {
                return Editor.ShowTabs;
            }
            set
            {
                Editor.ShowSpaces = value;
                Editor.ShowTabs = value;
            }
        }

        public int LangCode
        {
            get
            {
                return this.langCode;
            }
            set
            {
                this.langCode = value;

                try
                {
                    if (this.LangCode == 1)
                        Editor.SetHighlighting("Python");
                    else
                        Editor.SetHighlighting("Ruby");
                }
                catch (Exception Exp)
                {
                    IronException.Report("Unable to set Syntax Highlighting", Exp);
                }
            }
        }

        public delegate void ValueChangedEvent();

        public event ValueChangedEvent ValueChanged;

        private void CodeTextBox_Load(object sender, EventArgs e)
        {
            Editor.ShowTabs = false;
            Editor.ShowEOLMarkers = false;
            Editor.ShowSpaces = false;
            Editor.ShowInvalidLines = false;
            Editor.TabIndent = 2;
            try
            {
                if (this.LangCode == 1)
                    Editor.SetHighlighting("Python");
                else
                    Editor.SetHighlighting("Ruby");
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to set Syntax Highlighting", Exp);
            }

            Editor.ActiveTextAreaControl.TextArea.KeyUp += new System.Windows.Forms.KeyEventHandler(Editor_KeyUp);
        }

        void Editor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Left || e.KeyData == Keys.Right || e.KeyData == Keys.PageUp || e.KeyData == Keys.PageDown || e.KeyData == Keys.Home || e.KeyData == Keys.End || e.KeyData == Keys.CapsLock || e.KeyData == Keys.LWin || e.KeyData == Keys.RWin)
            {
                return;
            }
            if (ValueChanged != null)
            {
                ValueChanged();
            }
        }
    }
}
