using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IronWASP
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
        }

        delegate void ShowLoadMessage_d(string Message);
        internal void ShowLoadMessage(string Message)
        {
            if (this.StatusTB.InvokeRequired)
            {
                ShowLoadMessage_d SLM_d = new ShowLoadMessage_d(ShowLoadMessage);
                this.StatusTB.Invoke(SLM_d, new object[] { Message });
            }
            else
            {
                if (Message.Equals("0"))
                {
                    this.Close();
                }
                else
                {
                    this.StatusTB.Text = Message;
                }
            }
        }
    }
}
