using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IronWASP
{
    public class ModUi : Form
    {
        internal static Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { { "FormClosing", "" } };

        public Dictionary<string, Control> ModControls = new Dictionary<string, Control>();

        public ModUi()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ModUi_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ModUi_ThreadException);
        }

        private static void ModUi_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                IronException.Report("Unhandled Exception in ModUi Thread", e.Exception);
                MessageBox.Show("Unhanled Exception was encountered in this Module/Script. Exception details are available under the Exception node.");
            }
            catch { }
        }

        private static void ModUi_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception Exp = (Exception)e.ExceptionObject;
                IronException.Report("Unhandled Exception in ModUi", Exp);
                MessageBox.Show("Unhanled Exception was encountered in this Module/Script. Exception details are available under the Exception node.");
            }
            catch { }
        }

        public void ShowUi()
        {
            IronThread.Run(ShowUiObject, this);
        }

        void ShowUiObject(object ModUiObj)
        {
            ModUi MU = (ModUi)ModUiObj;
            Application.Run(MU);
        }

        internal static void ResetEventHandlers()
        {
            EventHandlers = new Dictionary<string, string>() { { "FormClosing", "" } };
        }

        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }
    }

    public class ModTextBox : TextBox
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { {"TextChanged", ""}};

        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        delegate void AddText_d(string Text);
        public void AddText(string Text)
        {
            if (this.InvokeRequired)
            {
                AddText_d ATD = new AddText_d(AddText);
                this.Invoke(ATD, new object[] { Text });
            }
            else
            {
                this.Text = this.Text + Text;
            }
        }

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }

        delegate void SetReadOnly_d(bool ReadOnly);
        public void SetReadOnly(bool ReadOnly)
        {
            if (this.InvokeRequired)
            {
                SetReadOnly_d SROD = new SetReadOnly_d(SetReadOnly);
                this.Invoke(SROD, new object[] { ReadOnly });
            }
            else
            {
                this.ReadOnly = ReadOnly;
            }
        }
    }

    public class ModRichTextBox : RichTextBox
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { { "TextChanged", "" } };
        StringBuilder RichTextHolder = new StringBuilder();

        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        delegate void AddText_d(string Text);
        public void AddText(string Text)
        {
            if (this.InvokeRequired)
            {
                AddText_d ATD = new AddText_d(AddText);
                this.Invoke(ATD, new object[] { Text });
            }
            else
            {
                this.Text = this.Text + Text;
            }
        }

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }

        delegate void SetReadOnly_d(bool ReadOnly);
        public void SetReadOnly(bool ReadOnly)
        {
            if (this.InvokeRequired)
            {
                SetReadOnly_d SROD = new SetReadOnly_d(SetReadOnly);
                this.Invoke(SROD, new object[] { ReadOnly });
            }
            else
            {
                this.ReadOnly = ReadOnly;
            }
        }

        delegate void SetRichText_d(string Text);
        public void SetRichText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetRichText_d SRTD = new SetRichText_d(SetRichText);
                this.Invoke(SRTD, new object[] { Text });
            }
            else
            {
                this.Rtf = Text;
            }
        }

        delegate void HighLight_d(string StartMarker, string EndMarker);
        public void HighLight(string StartMarker, string EndMarker)
        {
            if (this.InvokeRequired)
            {
                HighLight_d HD = new HighLight_d(HighLight);
                this.Invoke(HD, new object[] { StartMarker, EndMarker });
            }
            else
            {
                int SSI = this.SelectionStart;
                int SSL = this.SelectionLength;
                string TempText = this.Text;
                this.Text = TempText;//this is done to clear all previous selection highlighting
                bool CheckFurther = true;
                int Pointer = 0;
                string Content = this.Text;
                while (CheckFurther && Content.Length > Pointer)
                {
                    int Start = Content.IndexOf(StartMarker, Pointer);
                    int Stop = Content.IndexOf(EndMarker, Start + StartMarker.Length);
                    if (Start == -1 || Stop == -1) CheckFurther = false;
                    if (CheckFurther)
                    {
                        this.SelectionStart = Start;
                        this.SelectionLength = Stop - Start;
                        this.SelectionBackColor = Color.Orange;
                    }
                    Pointer = Stop + EndMarker.Length;
                }
                this.SelectionStart = SSI;
                this.SelectionLength = SSL;
            }
        }
    }

    public class ModLabel : Label
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { };
        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        delegate void AddText_d(string Text);
        public void AddText(string Text)
        {
            if (this.InvokeRequired)
            {
                AddText_d ATD = new AddText_d(AddText);
                this.Invoke(ATD, new object[] { Text });
            }
            else
            {
                this.Text = this.Text + Text;
            }
        }

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }
    }

    public class ModButton : Button
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { { "Click", "" } };
        
        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        delegate void AddText_d(string Text);
        public void AddText(string Text)
        {
            if (this.InvokeRequired)
            {
                AddText_d ATD = new AddText_d(AddText);
                this.Invoke(ATD, new object[] { Text });
            }
            else
            {
                this.Text = this.Text + Text;
            }
        }

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }
    }

    public class ModCheckBox : CheckBox
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { { "CheckedChanged", "" } };
        
        delegate void SetChecked_d(bool Checked);
        public void SetChecked(bool Checked)
        {
            if (this.InvokeRequired)
            {
                SetChecked_d SCD = new SetChecked_d(SetChecked);
                this.Invoke(SCD, new object[] { Checked });
            }
            else
            {
                this.Checked = Checked;
            }
        }

        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }
    }

    public class ModRadioButton : RadioButton
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { { "CheckedChanged", "" } };

        delegate void SetChecked_d(bool Checked);
        public void SetChecked(bool Checked)
        {
            if (this.InvokeRequired)
            {
                SetChecked_d SCD = new SetChecked_d(SetChecked);
                this.Invoke(SCD, new object[] { Checked });
            }
            else
            {
                this.Checked = Checked;
            }
        }

        delegate void SetText_d(string Text);
        public void SetText(string Text)
        {
            if (this.InvokeRequired)
            {
                SetText_d STD = new SetText_d(SetText);
                this.Invoke(STD, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }
    }

    public class ModDataGridView : DataGridView
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { { "CellClick", "" } };
        Dictionary<int, int> GridMap = new Dictionary<int, int>();
        public ModDataGridView()
        {
            this.RowHeadersVisible = false;
            this.GridColor = Color.White;
            this.BackgroundColor = Color.White;
            this.AllowUserToResizeRows = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        delegate void AddRow_d(object CellsObj);
        public void AddRow(object CellsObj)
        {
            if (this.InvokeRequired)
            {
                AddRow_d ARD = new AddRow_d(AddRow);
                this.Invoke(ARD, new object[] { CellsObj });
            }
            else
            {
                object[] CellsArray = Tools.ToDotNetArray(CellsObj);
                if (CellsArray.Length > 0)
                {
                    int GridId = this.Rows.Add(CellsArray);
                    if (CellsArray.Length > 0)
                    {
                        try
                        {
                            int ID = Int32.Parse(CellsArray[0].ToString());
                            GridMap[ID] = GridId;
                        }
                        catch { }
                    }
                }
            }
        }

        delegate void UpdateRow_d(object CellsObj);
        public void UpdateRow(object CellsObj)
        {
            if (this.InvokeRequired)
            {
                UpdateRow_d URD = new UpdateRow_d(UpdateRow);
                this.Invoke(URD, new object[] { CellsObj });
            }
            else
            {
                object[] CellsArray = Tools.ToDotNetArray(CellsObj);
                if(CellsArray.Length == 0 || CellsArray[0] == null)
                {
                    throw new Exception("ID not provided");
                }
                
                int GridId = -1;
                try
                {
                    int ID = Int32.Parse(CellsArray[0].ToString());
                    if(GridMap.ContainsKey(ID)) GridId = GridMap[ID];
                }catch{}
                if(GridId == -1 || !this.Rows[GridId].Cells[0].Value.ToString().Equals(CellsArray[0].ToString()))
                {
                    foreach(DataGridViewRow Row in this.Rows)
                    {
                        if(Row.Cells[0].Value.ToString().Equals(CellsArray[0].ToString()))
                        {
                            GridId = Row.Index;
                            break;
                        }
                    }
                }
                if(GridId == -1)
                {
                    throw new Exception("ID is missing in the Grid");
                }
                for(int i=1; i < CellsArray.Length; i++)
                {
                    if (CellsArray[i] != null)
                        this.Rows[GridId].Cells[i].Value = CellsArray[i];
                }
            }
        }

    }

    public class ModPanel : Panel
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { };

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }
    }

    public class ModTabControl : TabControl
    {
        internal Dictionary<string, string> EventHandlers = new Dictionary<string, string>() { { "SelectedIndexChanged", "" } };

        delegate void SetSelectedTab_d(string TabName);
        public void SetSelectedTab(string TabName)
        {
            if (this.InvokeRequired)
            {
                SetSelectedTab_d SSTD = new SetSelectedTab_d(SetSelectedTab);
                this.Invoke(SSTD, new object[] { TabName });
            }
            else
            {
                this.SelectTab(TabName);
            }
        }

        delegate void SetEnabled_d(bool Enable);
        public void SetEnabled(bool Enable)
        {
            if (this.InvokeRequired)
            {
                SetEnabled_d SED = new SetEnabled_d(SetEnabled);
                this.Invoke(SED, new object[] { Enable });
            }
            else
            {
                this.Enabled = Enable;
            }
        }

        delegate void SetVisible_d(bool Visible);
        public void SetVisible(bool Visible)
        {
            if (this.InvokeRequired)
            {
                SetVisible_d SVD = new SetVisible_d(SetVisible);
                this.Invoke(SVD, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
            }
        }
    }

    public class ModTabPage : TabPage
    {

    }
}
