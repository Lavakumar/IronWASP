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
using System.IO;
using System.Windows.Forms;
using System.Threading;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Hosting;
using IronPython;
using IronPython.Hosting;
using IronPython.Modules;
using IronPython.Runtime;
using IronPython.Runtime.Exceptions;
using IronRuby;
using IronRuby.Hosting;
using IronRuby.Runtime;
using IronRuby.StandardLibrary;

namespace IronWASP
{
    public partial class PluginEditor : Form
    {
        static ScriptRuntime RunTime;
        static ScriptEngine Engine;
        static string CurrentLanguage = "py";
        internal static FileInfo OpenedFile;
        internal static Thread T;
        static bool Open = true;
        static ManualResetEvent MRE = new ManualResetEvent(false);

        static Stack<string> EditorTextStack = new Stack<string>();

        //Search Related Values
        string Keyword = "";
        List<int[]> MatchSpots = new List<int[]>();
        int CurrentSpot = 0;
        bool InResetState = true;

        public PluginEditor()
        {
            InitializeComponent();
        }

        private void PluginEditor_Load(object sender, EventArgs e)
        {
            PluginEditorTE.ShowTabs = false;
            PluginEditorTE.ShowEOLMarkers = false;
            PluginEditorTE.ShowSpaces = false;
            PluginEditorTE.ShowInvalidLines = false;
            PluginEditorTE.TabIndent = 2;
            
            Directory.SetCurrentDirectory(Config.RootDir);
            HighlightingManager.Manager.AddSyntaxModeFileProvider(new EditorSyntaxModesProvider());
            
            PluginEditorTE.SetHighlighting("Python");
            ScriptRuntimeSetup Setup = new ScriptRuntimeSetup();
            Setup.LanguageSetups.Add(IronRuby.Ruby.CreateRubySetup());
            Setup.LanguageSetups.Add(IronPython.Hosting.Python.CreateLanguageSetup(null));
            RunTime = new ScriptRuntime(Setup);
            Engine = RunTime.GetEngine("py");
            APIDoc.BuildPluginEditorTrees();
            PluginEditorTE.ActiveTextAreaControl.TextArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PluginEditorTE_KeyUp);
            PluginEditorTE.Document.DocumentChanged  += new DocumentEventHandler(this.PluginEditorTE_Change);
        }

        private void PluginEditorPythonAPITree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.PluginEditorAPIDetailsRTB.Rtf = APIDoc.GetPyDecription(e.Node);
        }

        private void PluginEditorRubyAPITree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.PluginEditorAPIDetailsRTB.Rtf = APIDoc.GetRbDecription(e.Node);
        }

        private void IronPythonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguageAsIronPython();
        }

        private void IronRubyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguageAsIronRuby();
        }

        internal void SetLanguageAsIronPython()
        {
            try
            {
                Engine = RunTime.GetEngine("py");
                Directory.SetCurrentDirectory(Config.RootDir);
                PluginEditorTE.SetHighlighting("Python");
                PluginEditorTE.Refresh();
                CheckSyntax();
                CurrentLanguage = "py";
            }
            catch (Exception Exp)
            {
                IronUI.ShowPluginCompilerError("Error Changing Language: " + Exp.Message);
            }
        }

        internal void SetLanguageAsIronRuby()
        {
            try
            {
                Engine = RunTime.GetEngine("rb");
                Directory.SetCurrentDirectory(Config.RootDir);
                PluginEditorTE.SetHighlighting("Ruby");
                PluginEditorTE.Refresh();
                CheckSyntax();
                CurrentLanguage = "rb";
            }
            catch (Exception Exp)
            {
                IronUI.ShowPluginCompilerError("Error Changing Language: " + Exp.Message);
            }
        }

        private void PluginEditorTE_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.F5)
            {
                CheckSyntax();
            }
        }

        private void PluginEditorTE_Change(object sender, DocumentEventArgs e)
        {
            ResetSearchValues();
        }

        private void PluginEditorTE_TextChanged(object sender, EventArgs e)
        {
            ResetSearchValues();
        }

        void CheckSyntax()
        {
            try
            {
                string EditorText = PluginEditorTE.Text;
                EditorTextStack.Push(EditorText);

                if (T != null && T.ThreadState == ThreadState.WaitSleepJoin)
                {
                    MRE.Set();
                }
                else
                {
                    T = new Thread(SyntaxChecker);
                    MRE.Set();
                    T.Start();
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Error checking Syntax", Exp);
            }
        }

        void SyntaxChecker()
        {
            try
            {
                while (Open)
                {
                    string EditorText = "";
                    lock (EditorTextStack)
                    {
                        if (EditorTextStack.Count > 0)
                        {

                            EditorText = EditorTextStack.Pop();
                            EditorTextStack.Clear();
                        }
                        else
                        {
                            continue;
                        }
                    }
                    string ErrorMessage = "";
                    try
                    {
                        ScriptSource Source = Engine.CreateScriptSourceFromString(EditorText);
                        ScriptErrorReporter CompileErrors = new ScriptErrorReporter();
                        Source.Compile(CompileErrors);
                        ErrorMessage = CompileErrors.GetErrors();
                        if (ErrorMessage.Length == 0) { ErrorMessage = "0"; }
                    }
                    catch (Exception Exp)
                    {
                        ErrorMessage = Exp.Message;
                    }
                    IronUI.ShowPluginCompilerError(ErrorMessage);
                    MRE.Reset();
                    MRE.WaitOne();
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception Exp) 
            {
                IronException.Report("Error performing Syntax checking", Exp);
            }
        }

        private void ActivePluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginEditorOpenFileDialog.Title = "Open an Active Plugin";
            PluginEditorOpenFileDialog.InitialDirectory = Config.RootDir + "\\plugins\\Active\\";
            while (PluginEditorOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                    OpenedFile = new FileInfo(PluginEditorOpenFileDialog.FileName);
                    StreamReader Reader = new StreamReader(OpenedFile.FullName);
                    PluginEditorTE.Text = Reader.ReadToEnd();
                    Reader.Close();
                    if (OpenedFile.Name.EndsWith(".py"))
                    {
                        SetLanguageAsIronPython();
                    }
                    else if (OpenedFile.Name.EndsWith(".rb"))
                    {
                        SetLanguageAsIronRuby();
                    }
                    Search();
                    break;
            }
        }

        private void passivePluginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PluginEditorOpenFileDialog.Title = "Open a Passive Plugin";
            PluginEditorOpenFileDialog.InitialDirectory = Config.RootDir + "\\plugins\\Passive\\";
            while (PluginEditorOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenedFile = new FileInfo(PluginEditorOpenFileDialog.FileName);
                StreamReader Reader = new StreamReader(OpenedFile.FullName);
                PluginEditorTE.Text = Reader.ReadToEnd();
                Reader.Close();
                if (OpenedFile.Name.EndsWith(".py"))
                {
                    SetLanguageAsIronPython();
                }
                else if (OpenedFile.Name.EndsWith(".rb"))
                {
                    SetLanguageAsIronRuby();
                }
                Search();
                break;
            }
        }

        private void formatPluginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PluginEditorOpenFileDialog.Title = "Open a Format Plugin";
            PluginEditorOpenFileDialog.InitialDirectory = Config.RootDir + "\\plugins\\Format\\";
            while (PluginEditorOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenedFile = new FileInfo(PluginEditorOpenFileDialog.FileName);
                StreamReader Reader = new StreamReader(OpenedFile.FullName);
                PluginEditorTE.Text = Reader.ReadToEnd();
                Reader.Close();
                if (OpenedFile.Name.EndsWith(".py"))
                {
                    SetLanguageAsIronPython();
                }
                else if (OpenedFile.Name.EndsWith(".rb"))
                {
                    SetLanguageAsIronRuby();
                }
                Search();
                break;
            }
        }

        private void sessionPluginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PluginEditorOpenFileDialog.Title = "Open a Session Plugin";
            PluginEditorOpenFileDialog.InitialDirectory = Config.RootDir + "\\plugins\\Session\\";
            while (PluginEditorOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenedFile = new FileInfo(PluginEditorOpenFileDialog.FileName);
                StreamReader Reader = new StreamReader(OpenedFile.FullName);
                PluginEditorTE.Text = Reader.ReadToEnd();
                Reader.Close();
                if (OpenedFile.Name.EndsWith(".py"))
                {
                    SetLanguageAsIronPython();
                }
                else if (OpenedFile.Name.EndsWith(".rb"))
                {
                    SetLanguageAsIronRuby();
                }
                Search();
                break;
            }
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginEditorOpenFileDialog.Title = "Open File";
            PluginEditorOpenFileDialog.InitialDirectory = Config.RootDir + "\\plugins\\";
            while (PluginEditorOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenedFile = new FileInfo(PluginEditorOpenFileDialog.FileName);
                StreamReader Reader = new StreamReader(OpenedFile.FullName);
                PluginEditorTE.Text = Reader.ReadToEnd();
                Reader.Close();
                if (OpenedFile.Name.EndsWith(".py"))
                {
                    SetLanguageAsIronPython();
                }
                else if (OpenedFile.Name.EndsWith(".rb"))
                {
                    SetLanguageAsIronRuby();
                }
                Search();
                break;
            }
        }

        private void SaveWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenedFile != null && OpenedFile.Name.Length > 0 && !OpenedFile.Name.Equals("Enter an unique name.py") && !OpenedFile.Name.Equals("Enter an unique name.rb"))
            {
                try
                {
                    string Content = PluginEditorTE.Text;
                    StreamWriter Writer = new StreamWriter(OpenedFile.FullName);
                    Writer.Write(Content);
                    Writer.Close();
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(string.Format("Unable to save file: {0}", new object[] { Exp.Message }));
                }
            }
            else
            {
                GetFileNameFromUserAndSave();
            }
        }

        private void PluginEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Open = false;
                MRE.Set();
            }catch{}
        }

        private void NewPyActivePluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewPythonActivePluginTemplate.txt");
            }
            catch(Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronPython();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Active\\Enter an unique name.py");
        }

        private void NewRbActivePluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewRubyActivePluginTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronRuby();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Active\\Enter an unique name.rb");
        }

        private void NewPyPassivePluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewPythonPassivePluginTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronPython();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Passive\\Enter an unique name.py");
        }

        private void NewRbPassivePluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewRubyPassivePluginTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronRuby();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Passive\\Enter an unique name.rb");
        }

        private void NewPySessionPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewPythonSessionPluginTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronPython();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Session\\Enter an unique name.py");
        }

        private void NewRbSessionPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewRubySessionPluginTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronRuby();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Session\\Enter an unique name.rb");
        }

        private void NewPyFormatPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewPythonFormatPluginTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronPython();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Format\\Enter an unique name.py");
        }

        private void NewRbFormatPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewRubyFormatPluginTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronRuby();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Format\\Enter an unique name.rb");
        }

        private void NewPyScriptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewPythonScriptTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronPython();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Enter an unique name.py");
        }

        private void NewRbScriptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PluginEditorTE.Text = File.ReadAllText(Config.RootDir + "\\NewRubyScriptTemplate.txt");
            }
            catch (Exception Exp)
            {
                PluginEditorTE.Text = "";
                IronException.Report("Script Editor Template Error", Exp);
            }
            SetLanguageAsIronRuby();
            OpenedFile = new FileInfo(Config.RootDir + "\\Plugins\\Enter an unique name.rb");
        }

        private void CheckSyntaxF5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSyntax();
        }

        private void SaveAsStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFileNameFromUserAndSave();
        }

        void GetFileNameFromUserAndSave()
        {
            if (OpenedFile != null)
            {
                PluginEditorSaveFileDialog.InitialDirectory = OpenedFile.DirectoryName;
                if (OpenedFile.Name.Length > 0)
                {
                    PluginEditorSaveFileDialog.FileName = OpenedFile.Name;
                }
            }

            while (PluginEditorSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo Info = new FileInfo(PluginEditorSaveFileDialog.FileName);
                string Content = PluginEditorTE.Text;
                if (Info.Name.StartsWith("Enter an unique name"))
                {
                    MessageBox.Show("Please select a different name");
                }
                else if (!(PluginEditorSaveFileDialog.FileName.EndsWith(".py") || PluginEditorSaveFileDialog.FileName.EndsWith(".rb")))
                {
                    MessageBox.Show("Mention .py or .rb extension");
                }
                else
                {
                    try
                    {
                        StreamWriter Writer = new StreamWriter(Info.FullName);
                        Writer.Write(Content);
                        Writer.Close();
                        OpenedFile = new FileInfo(Info.FullName);
                    }
                    catch (Exception Exp)
                    {
                        MessageBox.Show(string.Format("Unable to save file: {0}", new object[] { Exp.Message }));
                    }
                    break;
                }
            }
        }

        private void PluginEditorSearchTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (PluginEditorSearchTB.Text.Equals(Keyword))
                {
                    FindNext();
                }
                else
                {
                    Search();
                }
                return;
            }
            if (e.KeyCode == Keys.PageUp)
            {
                FindPrevious();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                FindNext();
            }
            else
            {
                ResetSearchValues();
            }
        }

        void Search()
        {
            ResetSearchValues();
            InResetState = false;
            Keyword = PluginEditorSearchTB.Text;
            if (Keyword.Length == 0) return;
            
            List<string> Lines = new List<string>(PluginEditorTE.Text.Split(new string[] {"\n"}, StringSplitOptions.None));
            for (int LineNo = 0; LineNo < Lines.Count; LineNo++)
            {
                bool Loop = true;
                int StartIndex = 0;
                while (Loop)
                {
                    Loop = false;
                    int MatchSpot = Lines[LineNo].IndexOf(Keyword, StartIndex, StringComparison.CurrentCultureIgnoreCase);
                    if (MatchSpot >= 0)
                    {
                        MatchSpots.Add(new int[]{LineNo, MatchSpot});
                        if ((MatchSpot + Keyword.Length) < Lines[LineNo].Length)
                        {
                            StartIndex = MatchSpot + 1;
                            Loop = true;
                        }
                    }
                }
            }
            
            MatchCountLbl.Text = MatchSpots.Count.ToString();
            if (MatchSpots.Count > 0)
            {
                TextAreaControl TAC = PluginEditorTE.ActiveTextAreaControl;
                TAC.SelectionManager.SetSelection(new Point(MatchSpots[0][1], MatchSpots[0][0]), new Point(MatchSpots[0][1] + Keyword.Length, MatchSpots[0][0]));
                TAC.ScrollTo(MatchSpots[0][0]);
            }
            else
            {
                ClearSelection();
            }
        }

        void FindNext()
        {
            if (MatchSpots.Count == 0)
            {
                ClearSelection();
                return;
            }
            if (CurrentSpot == (MatchSpots.Count - 1))
            {
                CurrentSpot = 0;
            }
            else
            {
                CurrentSpot++;
            }
            TextAreaControl TAC = PluginEditorTE.ActiveTextAreaControl;
            TAC.SelectionManager.SetSelection(new Point(MatchSpots[CurrentSpot][1], MatchSpots[CurrentSpot][0]), new Point(MatchSpots[CurrentSpot][1] + Keyword.Length, MatchSpots[CurrentSpot][0]));
            TAC.ScrollTo(MatchSpots[CurrentSpot][0]);
        }

        void FindPrevious()
        {
            if (MatchSpots.Count == 0)
            {
                ClearSelection();
                return;
            }
            if (CurrentSpot == 0)
            {
                CurrentSpot = MatchSpots.Count - 1;
            }
            else
            {
                CurrentSpot--;
            }
            TextAreaControl TAC = PluginEditorTE.ActiveTextAreaControl;
            TAC.SelectionManager.SetSelection(new Point(MatchSpots[CurrentSpot][1], MatchSpots[CurrentSpot][0]), new Point(MatchSpots[CurrentSpot][1] + Keyword.Length, MatchSpots[CurrentSpot][0]));
            TAC.ScrollTo(MatchSpots[CurrentSpot][0]);
        }

        void ResetSearchValues()
        {
            if (!InResetState)
            {
                Keyword = "";
                MatchSpots = new List<int[]>();
                CurrentSpot = 0;
                MatchCountLbl.Text = "";
                InResetState = true;
            }
        }

        void ClearSelection()
        {
            TextAreaControl TAC = PluginEditorTE.ActiveTextAreaControl;
            TAC.SelectionManager.ClearSelection();
        }

        private void SearchMoveNextBtn_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void SearchMovePreviousBtn_Click(object sender, EventArgs e)
        {
            FindPrevious();
        }
    }
}
