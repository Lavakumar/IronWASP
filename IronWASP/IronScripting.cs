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
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
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
    class IronScripting
    {
        static ScriptRuntime RunTime;
        static ScriptEngine Engine;
        static ScriptScope Scope;

        static ShellResultStream ShellOutStream = new ShellResultStream();
        static string CommandBuffer = "";
        static string CurrentLanguage = "Python";
        internal static string ShellPrompt = ">>> ";
        internal static bool MoreExpected = false;
        internal static bool BlockShell = false;
        static List<string> CommandHistory = new List<string>();
        static int CommandHistoryPointer = -1;
        static int MaxHistoryLength = 50;
        static bool On = false;
        static ManualResetEvent MSR = new ManualResetEvent(false);
        static string QueuedCommand = "";
        static string QueuedCommands = "";
        static Thread ExecutorThread;

        internal static List<string> PyPaths = new List<string>();
        internal static List<string> RbPaths = new List<string>();
        internal static List<string> PyCommands = new List<string>();
        internal static List<string> RbCommands = new List<string>();

        internal static StringBuilder ShellOutText = new StringBuilder();
        static int MaxOutTextLength = 100000;

        internal static void InitialiseScriptingEnvironment()
        {

            ScriptRuntimeSetup Setup = new ScriptRuntimeSetup();
            Setup.LanguageSetups.Add(IronRuby.Ruby.CreateRubySetup());
            Setup.LanguageSetups.Add(IronPython.Hosting.Python.CreateLanguageSetup(null));
            RunTime = new ScriptRuntime(Setup);
            Engine = RunTime.GetEngine("py");
            Scope = RunTime.CreateScope();

            RunTime.IO.SetOutput(ShellOutStream, Encoding.UTF8);
            RunTime.IO.SetErrorOutput(ShellOutStream, Encoding.UTF8);

            Assembly MainAssembly = Assembly.GetExecutingAssembly();
            string RootDir = Directory.GetParent(MainAssembly.Location).FullName;
            string HAGPath = Path.Combine(RootDir, "HtmlAgilityPack.dll");
            Assembly HAGAssembly = Assembly.LoadFile(HAGPath);

            RunTime.LoadAssembly(MainAssembly);
            RunTime.LoadAssembly(HAGAssembly);
            RunTime.LoadAssembly(typeof(String).Assembly);
            RunTime.LoadAssembly(typeof(Uri).Assembly);
            RunTime.LoadAssembly(typeof(XmlDocument).Assembly);
            
            Engine.Runtime.TryGetEngine("py", out Engine);
            List<string> PySearchPaths = new List<string>();
            foreach (string PyPath in PyPaths)
            {
                PySearchPaths.Add(PyPath.Replace("$ROOTDIR", RootDir));
            }
            try
            {
                Engine.SetSearchPaths(PySearchPaths);
            }
            catch(Exception Exp)
            {
                IronException.Report("Unable to set PyPaths", Exp.Message, Exp.StackTrace);
            }

            foreach (string PyCommand in PyCommands)
            {
                try
                {
                    ExecuteStartUpCommand(PyCommand);
                }
                catch(Exception Exp)
                {
                    IronException.Report("Unable to execute Python startup command - " + PyCommand, Exp.Message, Exp.StackTrace);
                }
            }

            Engine.Runtime.TryGetEngine("rb", out Engine);

            List<string> RbSearchPaths = new List<string>();

            foreach (string RbPath in RbPaths)
            {
                RbSearchPaths.Add(RbPath.Replace("$ROOTDIR", RootDir));
            }
            Engine.SetSearchPaths(RbSearchPaths);

            foreach (string RbCommand in RbCommands)
            {
                try
                {
                    ExecuteStartUpCommand(RbCommand);
                }
                catch (Exception Exp)
                {
                    IronException.Report("Unable to execute Ruby startup command" + RbCommand, Exp.Message, Exp.StackTrace);
                }
            }

            Engine.Runtime.TryGetEngine("py", out Engine);
            ExecuteStartUpCommand("print 123");
            ShellOutText = new StringBuilder();
            IronUI.ResetInteractiveShellResult();
        }

        internal static void ChangeLanguageToPython()
        {
            CurrentLanguage = "Python";
            Engine.Runtime.TryGetEngine("py", out Engine);
            Reset();
        }
        internal static void ChangeLanguageToRuby()
        {
            CurrentLanguage = "Ruby";
            Engine.Runtime.TryGetEngine("rb", out Engine);
            Reset();
        }

        internal static void QueueMultiLineShellInputForExecution(string Commands)
        {
            QueuedCommand = "";
            QueuedCommands = Commands;
            ExecuteQueuedCommands();           
        }

        internal static void QueueInteractiveShellInputForExecution(string Command)
        {
            QueuedCommands = "";
            QueuedCommand = Command;
            ExecuteQueuedCommands();
        }

        static void ExecuteQueuedCommands()
        {
            On = true;
            if (!IsExecutorAlive())
            {
                ThreadStart TS = new ThreadStart(Executor);
                ExecutorThread = new Thread(TS);
                ExecutorThread.Start();
            }
            MSR.Set();
        }

        static bool IsExecutorAlive()
        {
            if (ExecutorThread == null) return false;
            try
            {
                if (ExecutorThread.ThreadState == ThreadState.WaitSleepJoin)
                {
                    return true;
                }
                else
                {
                    ExecutorThread.Abort();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        static void ExecuteStartUpCommand(string Command)
        {
            ScriptSource Source = IronScripting.Engine.CreateScriptSourceFromString(Command, SourceCodeKind.InteractiveCode);
            Source.Execute(IronScripting.Scope);
        }

        internal static InteractiveShellResult ExecuteInteractiveShellInput(string Command)
        {
            InteractiveShellResult ISR = new InteractiveShellResult();
            if (MoreExpected)
            {
                CommandBuffer += Command;
            }
            else
            {
                CommandBuffer = Command;
            }
            try
            {
                ScriptSource Source = IronScripting.Engine.CreateScriptSourceFromString(CommandBuffer, SourceCodeKind.InteractiveCode);
                ScriptCodeParseResult Result = Source.GetCodeProperties();
                if (Result == ScriptCodeParseResult.Complete || Result == ScriptCodeParseResult.Invalid || CanExecute(Command))
                {
                    Source.Execute(IronScripting.Scope);
                    Reset();
                    return ISR;
                }
                else
                {
                    ISR.MoreExpected = true;
                    Set(Command);
                    return ISR;
                }
            }
            catch (Exception exp)
            {
                ISR.ResultString = "Exception: " + exp.Message + Environment.NewLine;
                Reset();
                return ISR;
            }
        }

        internal static InteractiveShellResult ExecuteMultiLineShellInput(string Commands)
        {
            List<string> CommandsList = new List<string>();
            InteractiveShellResult ISR = new InteractiveShellResult();
            try
            {
                if (IronScripting.Engine.Setup.DisplayName.Contains("IronPython"))
                {
                    string[] Results = PluginEditor.CheckPythonIndentation(Commands);
                    if (Results[1].Length > 0)
                    {
                        throw new Exception(Results[1]);
                    }
                }
                ScriptSource Source = IronScripting.Engine.CreateScriptSourceFromString(Commands, SourceCodeKind.AutoDetect);
                Source.Execute(IronScripting.Scope);
                Reset();
                return ISR;
            }
            catch (Exception exp)
            {
                ISR.ResultString = "Exception : "  + exp.Message + Environment.NewLine;
                Reset();
                return ISR;
            }
        }

        static void Reset()
        {
            if (CurrentLanguage.Equals("Python"))
            {
                ShellPrompt = ">>>> ";
            }
            else
            {
                ShellPrompt = "irb> ";
            }
            MoreExpected = false;
            CommandBuffer = "";
        }
        static void Set(string Command)
        {
            if (CurrentLanguage.Equals("Python"))
            {
                ShellPrompt = ".... ";
            }
            else
            {
                ShellPrompt = "irb: ";
            }
            MoreExpected = true;
            CommandBuffer += "\n";
        }
        static bool CanExecute(string Command)
        {
            if (MoreExpected && Command.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static void AddCommandToHistory(string Command)
        {
            if (CommandHistory.Contains(Command))
            {
                CommandHistory.Remove(Command);
            }
            if (Command.Length > 0)
            {
                CommandHistory.Add(Command);
            }
            CommandHistoryPointer = -1;
            if (CommandHistory.Count > MaxHistoryLength)
            {
                CommandHistory.RemoveAt(0);
            }
        }
        internal static string GetNextCommandFromHistory()
        {
            if (CommandHistory.Count == 0)
            {
                return "";
            }
            if (CommandHistoryPointer == -1 || CommandHistoryPointer == CommandHistory.Count -1)
            {
                return "";
            }
            CommandHistoryPointer++;
            return CommandHistory[CommandHistoryPointer];
        }
        internal static string GetPreviousCommandFromHistory()
        {
            if (CommandHistory.Count == 0)
            {
                return "";
            }
            if (CommandHistoryPointer == 0)
            {
                return "";
            }
            if (CommandHistoryPointer == -1)
            {
                CommandHistoryPointer = CommandHistory.Count;
            }   
            CommandHistoryPointer--;
            return CommandHistory[CommandHistoryPointer];
        }

        static void Executor()
        {
            while (On)
            {
                try
                {
                    On = false;
                    if (QueuedCommands.Length > 0)
                    {
                        InteractiveShellResult Result = IronScripting.ExecuteMultiLineShellInput(QueuedCommands);
                        IronUI.UpdateInteractiveShellResult(Result);
                    }
                    else
                    {
                        InteractiveShellResult Result = IronScripting.ExecuteInteractiveShellInput(QueuedCommand);
                        IronUI.UpdateInteractiveShellResult(Result);
                    }
                }
                catch (ThreadAbortException)
                {
                    
                }
                catch (Exception exp)
                {
                    IronException.Report("Error executing Scripting Shell commands", exp.Message, exp.StackTrace);
                }
                MSR.Reset();
                MSR.WaitOne();
            }
        }  

        internal static void StopExecutor()
        {
            try
            {
                ExecutorThread.Abort();
            }
            catch
            {
                //
            }
        }

        internal static bool CheckOnOutText()
        {
            if (ShellOutText.Length > MaxOutTextLength)
            {
                ShellOutText.Remove(0, (ShellOutText.Length - MaxOutTextLength) + 10000);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
