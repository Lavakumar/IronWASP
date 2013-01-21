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
using Be.Windows.Forms;

namespace IronWASP
{
    public partial class TextBoxPlus : UserControl
    {
        DynamicByteProvider DynByteProvider = new DynamicByteProvider(new byte[] { });
        bool WasNormalTextChanged = false;
        bool WasRichTextChanged = false;
        bool WasHexChanged = false;

        bool IsInBinaryMode = false;

        string EncodingType = "UTF-8";

        StringComparison ComparisonType = StringComparison.OrdinalIgnoreCase;

        public TextBoxPlus()
        {
            InitializeComponent();
            DynByteProvider.Changed += new EventHandler(DynByteProvider_Changed);
        }

        public bool IsBinary
        {
            get
            {
                return IsInBinaryMode;
            }
        }

        bool WasTextChanged
        {
            get
            {
                if (MainText.Visible)
                    return WasNormalTextChanged;
                else
                    return WasRichTextChanged;
            }
        }

        public override string Text
        {
            get
            {
                return this.GetText();
            }
            set
            {
                this.SetText(value);
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
                MainRichText.ReadOnly = value;
                MainHex.ReadOnly = value;
                ReplaceAllLL.Enabled = !value;
                ReplaceBox.Enabled = !value;
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
                MainRichText.Font = value;
            }
        }
    
        string Keyword = "";
        string TextToSearch = "";
        List<int> MatchSpots = new List<int>();
        int CurrentSpot = 0;

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (SearchBox.Text.Length == 0)
            {
                ResetValues();
                return;
            }
            else if (MainText.Text.Length == 0 && MainRichText.TextLength == 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (SearchBox.Text.Equals(Keyword))
                {
                    FindNext();
                }
                else
                {
                    Find();
                }
                return;
            }
            if (e.KeyCode == Keys.PageUp)
            {
                FindPrev();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                FindNext();
            }
        }
   
        void Find()
        {
            ResetValues();
            Keyword = SearchBox.Text;
            if (IsInBinaryMode)
                TextToSearch = MainRichText.Text;
            else
                TextToSearch = MainText.Text;
            if (Keyword.Length == 0) return;
            bool loop = true;
            int StartIndex = 0;
            while (loop)
            {
                loop = false;
                int match_spot = TextToSearch.IndexOf(Keyword, StartIndex, ComparisonType);
                if (match_spot >= 0)
                {
                    MatchSpots.Add(match_spot);
                    if ((match_spot + Keyword.Length) < TextToSearch.Length)
                    {
                        StartIndex = match_spot + 1;
                        loop = true;
                    }
                }
            }
            MatchCountLabel.Text = MatchSpots.Count.ToString();
            if (MatchSpots.Count > 0)
            {
                if (IsInBinaryMode)
                {
                    MainRichText.Select(MatchSpots[0], Keyword.Length);
                    MainRichText.ScrollToCaret();
                }
                else
                {
                    MainText.Select(MatchSpots[0], Keyword.Length);
                    MainText.ScrollToCaret();
                }
            }
        }

        void FindNext()
        {
            if (MatchSpots.Count == 0)
            {
                return;
            }
            if (CurrentSpot == (MatchSpots.Count-1))
            {
                CurrentSpot = 0;
            }
            else
            {
                CurrentSpot++;
            }
            if (IsInBinaryMode)
            {
                MainRichText.Select(MatchSpots[CurrentSpot], Keyword.Length);
                MainRichText.ScrollToCaret();
            }
            else
            {
                MainText.Select(MatchSpots[CurrentSpot], Keyword.Length);
                MainText.ScrollToCaret();
            }
        }

        void FindPrev()
        {
            if (MatchSpots.Count == 0)
            {
                return;
            }
            if (CurrentSpot == 0)
            {
                CurrentSpot = MatchSpots.Count-1;
            }
            else
            {
                CurrentSpot--;
            }
            if (IsInBinaryMode)
            {
                MainRichText.Select(MatchSpots[CurrentSpot], Keyword.Length);
                MainRichText.ScrollToCaret();
            }
            else
            {
                MainText.Select(MatchSpots[CurrentSpot], Keyword.Length);
                MainText.ScrollToCaret();
            }
        }
        void ResetValues()
        {
            if (MatchCountLabel.Text != "0")
            {
                Keyword = "";
                MatchSpots.RemoveRange(0, MatchSpots.Count);
                CurrentSpot = 0;
                MatchCountLabel.Text = "0";
                MainText.DeselectAll();
                MainRichText.DeselectAll();
            }
        }

        private void MainText_TextChanged(object sender, EventArgs e)
        {
            ResetValues();
            WasNormalTextChanged = true;
            if (ValueChanged != null)
            {
                ValueChanged();
            }
        }

        private void MainRichText_TextChanged(object sender, EventArgs e)
        {
            ResetValues();
            WasRichTextChanged = true;
            if (ValueChanged != null)
            {
                ValueChanged();
            }
        }

        private void DynByteProvider_Changed(object sender, EventArgs e)
        {
            WasHexChanged = true;
            if (ValueChanged != null)
            {
                ValueChanged();
            }
        }

        public delegate void ValueChangedEvent();

        public event ValueChangedEvent ValueChanged;

        private void MoreOptionsLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MoreOptionsLL.Text.Equals("Show Options"))
            {
                ShowOptions();
            }
            else
            {
                HideOptions();
            }
        }

        void ShowOptions()
        {
            FunctionsPanel.Height = 80;
            TextPanel.Height = TextPanel.Height - 29;
            MoreOptionsLL.Text = "Hide Options";
        }

        void HideOptions()
        {
            FunctionsPanel.Height = 30;
            TextPanel.Height = TextPanel.Height + 29;
            MoreOptionsLL.Text = "Show Options";
        }

        private void ModTextBoxPlus_Load(object sender, EventArgs e)
        {
            this.MainHex.ByteProvider = DynByteProvider;
            
            HideOptions();
            MainText.Dock = DockStyle.Fill;
            MainRichText.Visible = false;
            BinaryWarningLbl.Visible = false;
            
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            MoreOptionsLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            LessOptionsLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            BinaryWarningLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            ReplaceAllLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            MatchCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            CaseSensitiveSearchCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            ToolTip Tips = new ToolTip();
            Tips.SetToolTip(BinaryWarningLbl, "Binary content detected. It is advisable to edit this value from the Hex section.");
        }

        public void SetText(string Text)
        {
            this.SetText(Text, "UTF-8");
        }
        delegate void SetText_d(string Text, string EncodingType);
        public void SetText(string Text, string EncodingType)
        {
            if (MainText.InvokeRequired)
            {
                SetText_d UiDelegate = new SetText_d(SetText);
                MainText.Invoke(UiDelegate, new object[] { Text, EncodingType });
            }
            else
            {
                SetTextValue(Text, Tools.IsBinary(Text));
                Encoding EncToUse = Encoding.GetEncoding(EncodingType);
                SetHex(EncToUse.GetBytes(Text));
                this.EncodingType = EncodingType;
                this.ResetChangedStatus();
                Find();
            }
        }

        public void SetBytes(byte[] Bytes)
        {
            this.SetBytes(Bytes, "UTF-8");
        }
        delegate void SetBytes_d(byte[] Bytes, string EncodingType);
        public void SetBytes(byte[] Bytes, string EncodingType)
        {
            if (MainHex.InvokeRequired)
            {
                SetBytes_d UiDelegate = new SetBytes_d(SetBytes);
                MainHex.Invoke(UiDelegate, new object[] { Bytes, EncodingType });
            }
            else
            {
                SetHex(Bytes);
                Encoding EncToUse = Encoding.GetEncoding(EncodingType);
                this.EncodingType = EncodingType;
                SetTextValue(EncToUse.GetString(Bytes), Tools.IsBinary(Bytes));
                this.ResetChangedStatus();
                Find();
            }
        }

        private void SetHex(byte[] Bytes)
        {
            this.DynByteProvider.DeleteBytes(0, this.DynByteProvider.Length);
            this.DynByteProvider.InsertBytes(0, Bytes);
            if (MainTabs.SelectedTab.Name.Equals("HexTab"))
            {
                MainHex.Refresh();
            }
        }
        private void SetTextValue(string Text, bool IsBinary)
        {
            if (IsBinary)
            {
                MainText.Text = "";
                MainText.Visible = false;
                MainRichText.Text = Text;
                MainRichText.Visible = true;
                BinaryWarningLbl.Visible = true;
                MainRichText.Dock = DockStyle.Fill;
            }
            else
            {
                MainRichText.Text = "";
                MainRichText.Visible = false;
                BinaryWarningLbl.Visible = false;
                MainText.Text = Text;
                MainText.Visible = true;
                MainText.Dock = DockStyle.Fill;
            }
            IsInBinaryMode = IsBinary;
        }

        void CheckAndHandleContentChanges()
        {
            if (WasHexChanged)
            {
                byte[] NewBytes = DynByteProvider.Bytes.ToArray();
                SetTextValue(Encoding.GetEncoding(this.EncodingType).GetString(NewBytes), Tools.IsBinary(NewBytes));
            }
            else if (WasRichTextChanged || WasTextChanged)
            {
                string NewText = "";
                if(IsInBinaryMode)
                    NewText = MainRichText.Text;
                else
                    NewText = MainText.Text;
                this.SetHex(Encoding.GetEncoding(this.EncodingType).GetBytes(NewText));
            }
            ResetChangedStatus();
        }

        void ResetChangedStatus()
        {
            WasHexChanged = false;
            WasNormalTextChanged = false;
            WasRichTextChanged = false;
        }

        public void ClearData()
        {
            this.SetText("");
        }

        private void MainTabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            this.CheckAndHandleContentChanges();
        }

        public string GetText()
        {
            this.CheckAndHandleContentChanges();
            if (IsInBinaryMode)
                return MainRichText.Text;
            else
                return MainText.Text;
        }

        public byte[] GetBytes()
        {
            this.CheckAndHandleContentChanges();
            return DynByteProvider.Bytes.ToArray();
        }

        private void CaseSensitiveSearchCB_Click(object sender, EventArgs e)
        {
            if (CaseSensitiveSearchCB.Checked)
                ComparisonType = StringComparison.Ordinal;
            else
                ComparisonType = StringComparison.OrdinalIgnoreCase;
            Find();
        }

        private void ReplaceAllLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string CurrentValue = "";
            if (IsInBinaryMode)
                CurrentValue = MainRichText.Text;
            else
                CurrentValue = MainText.Text;
            string ValueToReplace = SearchBox.Text;
            string ReplaceWith = ReplaceBox.Text;
            if (ValueToReplace.Length == 0) return;
            string NewString = CurrentValue.Replace(ValueToReplace, ReplaceWith);
            if (IsInBinaryMode)
                MainRichText.Text = NewString;
            else
                MainText.Text = NewString;
        }

        private void LessOptionsLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HideOptions();
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void ReplaceBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void MainText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "A")
            {
                MainText.SelectAll();
            }
        }


    }
}
