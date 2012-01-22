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
            Input = InputTB.Text;
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
            IronUI.ShowEncodeDecodeResult(Result, Status);
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
            InputTB.Text = OutputTB.Text;
        }        
    }
}
