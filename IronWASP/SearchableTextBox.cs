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
    public partial class SearchableTextBox : UserControl
    {
        public SearchableTextBox()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return MainText.Text;
            }
            set
            {
                MainText.Text = value;
                find();
            }
        }

        public bool ReadOnly
        {
            get
            {
                return MainText.ReadOnly;
            }
            set
            {
                MainText.ReadOnly = value;
                MainText.BackColor = Color.White;
            }
        }

        public override Font Font
        {
            get
            {
                return MainText.Font;
            }
            set
            {
                MainText.Font = value;
            }
        }

        string keyword = "";
        List<int> match_spots = new List<int>();
        int current_spot = 0;

        private void searchbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (searchbox.Text.Length == 0)
            {
                reset_values();
                return;
            }
            else if (MainText.Text.Length == 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (searchbox.Text.Equals(keyword))
                {
                    find_next();
                }
                else
                {
                    find();
                }
                return;
            }
            if (e.KeyCode == Keys.PageUp)
            {
                find_prev();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                find_next();
            }
        }

        void find()
        {
            reset_values();
            keyword = searchbox.Text;
            if (keyword.Length == 0) return;
            bool loop = true;
            int start_index = 0;
            while (loop)
            {
                loop = false;
                int match_spot = MainText.Text.IndexOf(keyword, start_index, StringComparison.CurrentCultureIgnoreCase);
                if (match_spot >= 0)
                {
                    match_spots.Add(match_spot);
                    if ((match_spot + keyword.Length) < MainText.Text.Length)
                    {
                        start_index = match_spot + 1;
                        loop = true;
                    }
                }
            }
            matches.Text = match_spots.Count.ToString();
            if (match_spots.Count > 0)
            {
                MainText.Select(match_spots[0], keyword.Length);
                MainText.ScrollToCaret();
            }
        }

        void find_next()
        {
            if (match_spots.Count == 0)
            {
                return;
            }
            if (current_spot == (match_spots.Count - 1))
            {
                current_spot = 0;
            }
            else
            {
                current_spot++;
            }
            MainText.Select(match_spots[current_spot], keyword.Length);
            MainText.ScrollToCaret();
        }

        void find_prev()
        {
            if (match_spots.Count == 0)
            {
                return;
            }
            if (current_spot == 0)
            {
                current_spot = match_spots.Count - 1;
            }
            else
            {
                current_spot--;
            }
            MainText.Select(match_spots[current_spot], keyword.Length);
            MainText.ScrollToCaret();
        }
        void reset_values()
        {
            if (matches.Text != "0")
            {
                keyword = "";
                match_spots.RemoveRange(0, match_spots.Count);
                current_spot = 0;
                matches.Text = "0";
                MainText.DeselectAll();
            }
        }

        internal string PlaceMarkersAroundSelectedText(string StartMarker, string EndMarker)
        {
            try
            {
                string OldText = MainText.Text;
                int SelectionStart = MainText.SelectionStart;
                int SelectionLength = MainText.SelectionLength;
                if (SelectionLength < 1) return "No section selected";
                string FirstPart = OldText.Substring(0, SelectionStart);
                string MiddlePart = OldText.Substring(SelectionStart, SelectionLength);
                string LastPart = OldText.Substring(SelectionStart + SelectionLength);
                StringBuilder SB = new StringBuilder();
                SB.Append(FirstPart); SB.Append(StartMarker); SB.Append(MiddlePart); SB.Append(EndMarker); SB.Append(LastPart);
                MainText.Text = SB.ToString();
                return "";
            }
            catch { return "Could not place markers"; }
        }

        private void maintext_TextChanged(object sender, EventArgs e)
        {
            reset_values();
            if (TextValueChanged != null)
            {
                TextValueChanged();
            }
        }

        public delegate void TextChangedEvent();

        public event TextChangedEvent TextValueChanged;
    }
}
