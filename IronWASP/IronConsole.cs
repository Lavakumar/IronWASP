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
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace IronWASP
{
    public partial class IronConsole : Form
    {
        public IronConsole()
        {
            InitializeComponent();
        }

        public delegate void ConsoleClosingEvent(FormClosingEventArgs e);

        public event ConsoleClosingEvent ConsoleClosing;


        ManualResetEvent MSR = new ManualResetEvent(false);
        string UserInput = "";

        delegate void SetTitle_d(string Text);
        public void SetTitle(string Text)
        {
            if (this.IsDisposed) return;
            if (this.InvokeRequired)
            {
                SetTitle_d STD = new SetTitle_d(SetTitle);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        public void Print(object Message)
        {
            this.AddText(ConvertObjectIntoPrintableString(Message));
        }

        public void PrintLine(object Message)
        {
            this.Print(string.Format("{0}{1}", ConvertObjectIntoPrintableString(Message), Environment.NewLine));
        }

        static string ConvertObjectIntoPrintableString(object Obj)
        {
            string Type = Obj.GetType().FullName;
            if (Type.Equals("IronRuby.Builtins.RubyArray") || Type.Equals("IronPython.Runtime.List"))
            {
                try
                {
                    object[] Arr = Tools.ToDotNetArray(Obj);
                    StringBuilder SB = new StringBuilder("[");
                    for (int i=0; i < Arr.Length; i++)
                    {
                        SB.Append(ConvertObjectIntoPrintableString(Arr[i]));
                        if (i < Arr.Length - 1) SB.Append(", ");
                    }
                    SB.Append("]");
                    return SB.ToString();
                }
                catch { }
            }
            return Obj.ToString();
        }

        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.IsDisposed) return;
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.OutputTB.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.OutputTB.Text = Text;
                this.OutputTB.SelectionStart = this.OutputTB.Text.Length;
                this.OutputTB.ScrollToCaret();
            }
        }

        delegate void AddText_d(string Text);
        public void AddText(string Text)
        {
            if (this.IsDisposed) return;
            if (this.InvokeRequired)
            {
                AddText_d ATD = new AddText_d(AddText);
                this.OutputTB.Invoke(ATD, new object[] { Text });
            }
            else
            {
                this.OutputTB.Text = this.OutputTB.Text + Text;
                this.OutputTB.SelectionStart = this.OutputTB.Text.Length;
                this.OutputTB.ScrollToCaret();
            }
        }

        public string Read()
        {
            ShowInput(true);
            MSR.WaitOne();
            MSR.Reset();
            return UserInput;
        }

        public string ReadLine()
        {
            ShowInput(false);
            MSR.WaitOne();
            MSR.Reset();
            return UserInput;
        }

        public void ShowConsole()
        {
            IronThread.Run(ShowUiObject, this);
        }

        void ShowUiObject(object ConsoleUiObj)
        {
            IronConsole IUO = (IronConsole)ConsoleUiObj;
            Application.Run(IUO);
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            AcceptInput();
        }

        void AcceptInput()
        {
            UserInput = InputTB.Text;
            MSR.Set();
            BaseSplit.Panel2Collapsed = true;
        }

        delegate void ShowInput_d(bool Multiline);
        void ShowInput(bool Multiline)
        {
            if (this.IsDisposed) return;
            if (this.InvokeRequired)
            {
                ShowInput_d SI = new ShowInput_d(ShowInput);
                this.Invoke(SI, new object[] { Multiline });
            }
            else
            {
                InputTB.Text = "";
                
                BaseSplit.Panel2Collapsed = false;
                
                InputTB.Multiline = Multiline;
                if (Multiline)
                {
                    InputTB.Height = BaseSplit.Panel2.Height - 70;
                    InputTB.ScrollBars = ScrollBars.Both;
                    InputLbl.Text = "Enter your input below and press the 'Submit' button:";
                }
                else
                {
                    InputLbl.Text = "Enter your input below and press hit the enter key or press the 'Submit' button:";
                }
                OutputTB.SelectionStart = OutputTB.Text.Length;
                OutputTB.ScrollToCaret();
                InputTB.Focus();
            }
        }

        private void IronConsole_Load(object sender, EventArgs e)
        {
            BaseSplit.Panel2Collapsed = true;
        }

        private void InputTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (InputTB.Multiline) return;
            if (e.KeyChar == (Char)Keys.Return)
            {
                e.Handled = true;
                AcceptInput();
            }
        }

        private void IronConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ConsoleClosing != null)
            {
                try
                {
                    ConsoleClosing(e);
                    if (!e.Cancel)
                    {
                        try
                        {
                            MSR.Reset();//MSR.WaitOne() called on the accept input thread was keeping this thread alive and IronWASP process was running even after closing the UI. This fixes it.
                        }
                        catch { }
                    }
                }
                catch (Exception Exp)
                { 
                    IronException.Report("Error executing the IronConsole ConsoleClosing event", Exp);
                }
            }
        }
    }
}
