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
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IronWASP
{
    public partial class EncodeDecodeWindow : Form
    {
        internal static bool UrlEncode = true;
        internal static bool HtmlEncode = false;
        internal static bool HexEncode = false;
        internal static bool Base64Encode = false;
        internal static bool ToHex = false;

        internal static bool UrlDecode = false;
        internal static bool HtmlDecode = false;
        internal static bool HexDecode = false;
        internal static bool Base64Decode = false;

        internal static bool MD5 = false;
        internal static bool SHA1 = false;
        internal static bool SHA256 = false;
        internal static bool SHA384 = false;
        internal static bool SHA512 = false;

        internal static EncodeDecode_d Command;
        internal static string Input = "";
        internal static Thread T;

        public EncodeDecodeWindow()
        {
            InitializeComponent();
        }

        internal delegate string EncodeDecode_d(string Input);

        void StartExecution()
        {
            DisableAllButtons();
            StatusTB.Text = "Executing...";
            OutputTB.Text = "";
            Input = InputTB.GetText();
            T = new Thread(ExecuteCommand);
            T.Start();
        }

        void ExecuteCommand()
        {
            string Status = "";
            string Result = "";
            try
            {
                Result = Command(Input);
            }
            catch (Exception Exp)
            {
                Status = "Error: " + Exp.Message;
            }
            ShowEncodeDecodeResult(Result, Status);
        }

        delegate void ShowEncodeDecodeResult_d(string Result, string Message);
        internal void ShowEncodeDecodeResult(string Result, string Message)
        {
            if (this.InvokeRequired)
            {
                ShowEncodeDecodeResult_d SEDR_d = new ShowEncodeDecodeResult_d(ShowEncodeDecodeResult);
                this.Invoke(SEDR_d, new object[] { Result, Message });
            }
            else
            {
                OutputTB.SetText(Result);
                if (Message.Length > 0)
                {
                    StatusTB.BackColor = Color.Red;
                }
                else
                {
                    StatusTB.BackColor = SystemColors.Control;
                }
                StatusTB.Text = Message;
                EnableAllEncodeDecodeCommandButtons();
            }
        }

        void EnableAllEncodeDecodeCommandButtons()
        {
            UrlEncodeBtn.Enabled = true;
            HtmlEncodeBtn.Enabled = true;
            HexEncodeBtn.Enabled = true;
            Base64EncodeBtn.Enabled = true;
            ToHexBtn.Enabled = true;
            UrlDecodeBtn.Enabled = true;
            HtmlDecodeBtn.Enabled = true;
            HexDecodeBtn.Enabled = true;
            Base64DecodeBtn.Enabled = true;
            MD5Btn.Enabled = true;
            SHA1Btn.Enabled = true;
            SHA256Btn.Enabled = true;
            SHA384Btn.Enabled = true;
            SHA512Btn.Enabled = true;
        }

        void DisableAllButtons()
        {
            UrlEncodeBtn.Enabled = false;
            HtmlEncodeBtn.Enabled = false;
            HexEncodeBtn.Enabled = false;
            Base64EncodeBtn.Enabled = false;
            ToHexBtn.Enabled = false;
            UrlDecodeBtn.Enabled = false;
            HtmlDecodeBtn.Enabled = false;
            HexDecodeBtn.Enabled = false;
            Base64DecodeBtn.Enabled = false;
            MD5Btn.Enabled = false;
            SHA1Btn.Enabled = false;
            SHA256Btn.Enabled = false;
            SHA384Btn.Enabled = false;
            SHA512Btn.Enabled = false;
        }

        private void UrlEncodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.UrlEncode);
            StartExecution();
        }

        private void HtmlEncodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.HtmlEncode);
            StartExecution();
        }

        private void HexEncodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.HexEncode);
            StartExecution();
        }

        private void Base64EncodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.Base64Encode);
            StartExecution();
        }

        private void ToHexBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.ToHex);
            StartExecution();
        }

        private void UrlDecodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.UrlDecode);
            StartExecution();
        }

        private void HtmlDecodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.HtmlDecode);
            StartExecution();
        }

        private void HexDecodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.HexDecode);
            StartExecution();
        }

        private void Base64DecodeBtn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.Base64Decode);
            StartExecution();
        }

        private void MD5Btn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.MD5);
            StartExecution();
        }

        private void SHA1Btn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.SHA1);
            StartExecution();
        }

        private void SHA256Btn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.SHA256);
            StartExecution();
        }

        private void SHA384Btn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.SHA384);
            StartExecution();
        }

        private void SHA512Btn_Click(object sender, EventArgs e)
        {
            Command = new EncodeDecode_d(Tools.SHA512);
            StartExecution();
        }

        private void EncodeOutToEncodeInBtn_Click(object sender, EventArgs e)
        {
            if (OutputTB.IsBinary)
            {
                InputTB.SetBytes(OutputTB.GetBytes());
            }
            else
            {
                InputTB.SetText(OutputTB.GetText());
            }
        }
    }
}
