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
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IronWASP
{
    public partial class ScreenshotForm : Form
    {
        static string InitialDirectory = "";
        static float FontSize = -1;

        Session Sess;

        public ScreenshotForm()
        {
            InitializeComponent();
        }

        private void TakeScreenshotBtn_Click(object sender, EventArgs e)
        {
            RootSplit.Panel1Collapsed = true;
            this.Refresh();
            Bitmap BM = new Bitmap(this.Bounds.Width, this.Bounds.Height);
            Graphics G = Graphics.FromImage(BM as Image);
            G.CopyFromScreen(new Point(this.Bounds.Left, this.Bounds.Top), Point.Empty, this.Bounds.Size);
            RootSplit.Panel1Collapsed = false;
            GetFileNameFromUserAndSave(BM);
        }

        void GetFileNameFromUserAndSave(Bitmap BM)
        {
            SaveScreenshotDialog.FileName = "";

            if (InitialDirectory.Length > 0)
            {
                SaveScreenshotDialog.InitialDirectory = InitialDirectory;
            }
            else
            {
                SaveScreenshotDialog.InitialDirectory = Config.Path;
            }
            
            while (SaveScreenshotDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo Info = new FileInfo(SaveScreenshotDialog.FileName);
                InitialDirectory = Info.Directory.FullName;
                if (!Info.Name.EndsWith(".jpg"))
                {
                    MessageBox.Show("Select extension as jpg");
                }
                else
                {
                    try
                    {
                        BM.Save(Info.FullName, ImageFormat.Jpeg);
                    }
                    catch (Exception Exp)
                    {
                        MessageBox.Show(string.Format("Unable to save file: {0}", new object[] { Exp.Message }));
                    }
                    break;
                }
            }
        }

        private void LogScreenshotForm_Load(object sender, EventArgs e)
        {
            SaveScreenshotDialog.DefaultExt = ".jpg";
            SaveScreenshotDialog.Filter = "Jpeg Files (*.jpg) | *.jpg";
            if (FontSize > -1)
            {
                RequestRTB.Font = new Font(RequestRTB.Font.Name, FontSize);
                ResponseRTB.Font = new Font(ResponseRTB.Font.Name, FontSize);
            }
            if (Sess != null)
            {
                if (Sess.Request != null)
                {
                    this.RequestRTB.Text = Sess.Request.ToString();
                }
                if (Sess.Response != null)
                {
                    this.ResponseRTB.Text = Sess.Response.ToString();
                }
            }
        }

        public void SetSession(Session Sess_P)
        {
            this.Sess = Sess_P;
        }
        public void SetRequestResponse(Request Req, Response Res)
        {
            this.Sess = new Session(Req, Res);
        }

        private void AlignLeftRightRB_CheckedChanged(object sender, EventArgs e)
        {
            if (AlignLeftRightRB.Checked)
            {
                ReqResSplit.Orientation = Orientation.Vertical;
            }
            else
            {
                ReqResSplit.Orientation = Orientation.Horizontal;
            }
        }

        private void TextSizeUpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                RequestRTB.Font = new Font(RequestRTB.Font.Name, RequestRTB.Font.Size + 1);
                ResponseRTB.Font = new Font(ResponseRTB.Font.Name, ResponseRTB.Font.Size + 1);
                FontSize = RequestRTB.Font.Size;
            }catch{}
        }

        private void TextSizeDownBtn_Click(object sender, EventArgs e)
        {
            try
            {
                RequestRTB.Font = new Font(RequestRTB.Font.Name, RequestRTB.Font.Size - 1);
                ResponseRTB.Font = new Font(ResponseRTB.Font.Name, ResponseRTB.Font.Size - 1);
                FontSize = RequestRTB.Font.Size;
            }
            catch { }
        }

        private void FontColorBtn_Click(object sender, EventArgs e)
        {
            DialogResult Answer = FontColorDialog.ShowDialog();
            if (Answer == DialogResult.OK)
            {
                RequestRTB.SelectionColor = FontColorDialog.Color;
                ResponseRTB.SelectionColor = FontColorDialog.Color;
            }
        }

        private void FontHighlightBtn_Click(object sender, EventArgs e)
        {
            DialogResult Answer = FontColorDialog.ShowDialog();
            if (Answer == DialogResult.OK)
            {
                RequestRTB.SelectionBackColor = FontColorDialog.Color;
                ResponseRTB.SelectionBackColor = FontColorDialog.Color;
            }
        }

        private void RequestRTB_SelectionChanged(object sender, EventArgs e)
        {
            ResponseRTB.SelectionLength = 0;
        }

        private void ResponseRTB_SelectionChanged(object sender, EventArgs e)
        {
            RequestRTB.SelectionLength = 0;
        }

        private void SelectedTextSizeDownBtn_Click(object sender, EventArgs e)
        {
            try
            {
                RequestRTB.SelectionFont = new Font(RequestRTB.SelectionFont.Name, RequestRTB.SelectionFont.Size - 1);
                ResponseRTB.SelectionFont = new Font(ResponseRTB.SelectionFont.Name, ResponseRTB.SelectionFont.Size - 1);
            }
            catch { }
        }

        private void SelectedTextSizeUpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                RequestRTB.SelectionFont = new Font(RequestRTB.SelectionFont.Name, RequestRTB.SelectionFont.Size + 1);
                ResponseRTB.SelectionFont = new Font(ResponseRTB.SelectionFont.Name, ResponseRTB.SelectionFont.Size + 1);
            }
            catch { }
        }

        private void SnipSelectedTextBtn_Click(object sender, EventArgs e)
        {
            if (RequestRTB.SelectionLength > 0)
            {
                RequestRTB.SelectionColor = Color.Blue;
                RequestRTB.SelectedText = "  [---- Snipped for brevity ----]  ";
            }
            else if (ResponseRTB.SelectionLength > 0)
            {
                ResponseRTB.SelectionColor = Color.Blue;
                ResponseRTB.SelectedText = "  [---- Snipped for brevity ----]  ";
            }
        }
    }
}
