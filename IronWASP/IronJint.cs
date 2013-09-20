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
using System.Text;
using System.Text.RegularExpressions;
using Jint;
using Jint.Expressions;
using Antlr.Runtime;
using System.Threading;
using System.Windows.Forms;
using jsbeautifylib;

namespace IronWASP
{
    public class IronJint
    {
        static Thread UITraceThread;

        internal static IronJint UIIJ = new IronJint();

        internal static List<string> DefaultSourceObjects = new List<string>();// { "document.URL", "document.documentURI", "document.URLUnencoded", "document.baseURI", "location", "location.href", "location.search", "location.hash", "location.pathname", "document.cookie", "document.referrer", "window.name" };
        internal static List<string> DefaultSinkObjects = new List<string>();// { "location", "location.href", "location.pathname", "location.search", "location.protocol", "location.hostname" };
        internal static List<string> DefaultSourceReturningMethods = new List<string>();// { };
        internal static List<string> DefaultSinkReturningMethods = new List<string>();// { };
        internal static List<string> DefaultArgumentReturningMethods = new List<string>();// { };
        internal static List<string> DefaultArgumentAssignedToSinkMethods = new List<string>();// { "eval(!)", "Function()", "setTimeout(!,0)", "setInterval(!,0)" };
        internal static List<string> DefaultArgumentAssignedASourceMethods = new List<string>();// { };

        static string InputCodeString = "";
        static List<string> ConfiguredSourceObjects = new List<string>();
        static List<string> ConfiguredSinkObjects = new List<string>();
        static List<string> ConfiguredSourceReturningMethods = new List<string>();
        static List<string> ConfiguredSinkReturningMethods = new List<string>();
        static List<string> ConfiguredArgumentReturningMethods = new List<string>();
        static List<string> ConfiguredArgumentAssignedToSinkMethods = new List<string>();
        static List<string> ConfiguredArgumentAssignedASourceMethods = new List<string>();

        internal static bool PauseAtTaint = false;

        internal static List<int> SourceLinesToIgnore = new List<int>();
        internal static List<int> SinkLinesToIgnore = new List<int>();

        internal static List<int> SourceLinesToInclude = new List<int>();
        internal static List<int> SinkLinesToInclude = new List<int>();

        internal List<List<JintItem>> SourceItems = new List<List<JintItem>>();
        internal List<List<JintItem>> SinkItems = new List<List<JintItem>>();

        public List<string> Lines = new List<string>();
        public List<int> SourceLines = new List<int>();
        public List<int> SinkLines = new List<int>();
        public List<int> SourceToSinkLines = new List<int>();
        

        internal Dictionary<int, List<string>> SourceReasons = new Dictionary<int, List<string>>();
        internal Dictionary<int, List<string>> SinkReasons = new Dictionary<int, List<string>>();

        internal List<List<JintItem>> SourceTaintedItems = new List<List<JintItem>>();
        internal List<List<JintItem>> SinkTaintedItems = new List<List<JintItem>>();

        internal List<List<JintItem>> GlobalVariables = new List<List<JintItem>>();
        internal List<List<JintItem>> GlobalMethods = new List<List<JintItem>>();

        //to be initialized from the UI
        internal List<List<JintItem>> SourceObjects = new List<List<JintItem>>();
        internal List<List<JintItem>> SinkObjects = new List<List<JintItem>>();
        internal List<List<JintItem>> SourceReturningMethods = new List<List<JintItem>>();
        internal List<List<JintItem>> SinkReturningMethods = new List<List<JintItem>>();
        internal List<List<JintItem>> ArgumentReturningMethods = new List<List<JintItem>>();
        internal List<List<JintItem>> ArgumentAssignedToSinkMethods = new List<List<JintItem>>();
        internal List<List<JintItem>> ArgumentAssignedASourceMethods = new List<List<JintItem>>();

        internal Dictionary<string, List<List<JintItem>>> InterestingStringHolders = new Dictionary<string, List<List<JintItem>>>();

        long AnalyzeCallStartTime = 0;

        //Used by the UI
        internal static Dictionary<int, int> LineNoToGridRowNoMapping = new Dictionary<int, int>();

        //this is to avoid recursive looping to go on for ever
        //Test Case that causes stack-overflow:
        //o[o.x] = "0";
        // p[o.y] = 1;
        //
        List<List<JintItem>> SentForStringConversion = new List<List<JintItem>>();

        internal List<JintItem> JintStack = new List<JintItem>();

        internal int CurrentLineNo = 0;
        internal int CurrentCharNo = 0;

        public List<string> RawLines = new List<string>();

        internal bool TraceKeyword = false;
        internal string KeywordToTrace = "";
        public List<string> KeywordContexts = new List<string>();

        bool StartedFromUI = false;
        internal ManualResetEvent MSR = new ManualResetEvent(false);

        public static string Beautify(string Code)
        {
            JSBeautify JB = new JSBeautify(Code, new JSBeautifyOptions());
            string BeautifiedCode = JB.GetResult();
            return BeautifiedCode;
        }
        
        //internal static void StartTraceFromUI(string Code, List<string> SourceObjs, List<string> SinkObjs, List<string> SourceRetMets, List<string> SinkRetMets, List<string> ArgRetMets, List<string> ArgAssASourceMets, List<string> ArgAssToSinkMets)
        //{
        //    StopUITrace();
        //    SourceLinesToIgnore.Clear();
        //    SinkLinesToIgnore.Clear();
        //    SourceLinesToInclude.Clear();
        //    SinkLinesToInclude.Clear();

        //    InputCodeString = Code;
        //    ConfiguredSourceObjects = SourceObjs;
        //    ConfiguredSinkObjects = SinkObjs;
        //    ConfiguredSourceReturningMethods = SourceRetMets;
        //    ConfiguredSinkReturningMethods = SinkRetMets;
        //    ConfiguredArgumentReturningMethods = ArgRetMets;
        //    ConfiguredArgumentAssignedASourceMethods = ArgAssASourceMets;
        //    ConfiguredArgumentAssignedToSinkMethods = ArgAssToSinkMets;

        //    UITraceThread = new Thread(TraceFromUI);
        //    UITraceThread.Start();
        //}

        //internal static void ReDoTraceFromUI()
        //{
        //    UITraceThread = new Thread(TraceFromUI);
        //    UITraceThread.Start();
        //}

        //internal static void StopUITrace()
        //{
        //    try
        //    {
        //        UITraceThread.Abort();
        //    }
        //    catch { }
        //}

        //internal static void TraceFromUI()
        //{
        //    try
        //    {
        //        IronUI.ShowTraceStatus("Trace in progress...", false);
        //        IronJint IJ = new IronJint();
        //        UIIJ = IJ;
        //        IJ.SetSourcesAndSinks(ConfiguredSourceObjects, ConfiguredSinkObjects, ConfiguredSourceReturningMethods, ConfiguredSinkReturningMethods, ConfiguredArgumentReturningMethods, ConfiguredArgumentAssignedASourceMethods, ConfiguredArgumentAssignedToSinkMethods);
        //        IJ.ClearAllTaint();
        //        IJ.JintStack.Clear();
        //        string DirtyJS = "";
        //        if (Tools.IsJavaScript(InputCodeString))
        //        {
        //            DirtyJS = InputCodeString;
        //        }
        //        else
        //        {
        //            try
        //            {
        //                HTML H = new HTML(InputCodeString);
        //                List<string> Scripts = H.GetJavaScript();
        //                StringBuilder ScriptString = new StringBuilder();
        //                foreach (string Script in Scripts)
        //                {
        //                    ScriptString.AppendLine(Script);
        //                }
        //                DirtyJS = ScriptString.ToString();
        //            }
        //            catch
        //            {
        //                throw new Exception("Entered text does not contain valid JavaScript");
        //            }
        //        }
        //        if (DirtyJS.Length == 0)
        //        {
        //            throw new Exception("No valid JavaScript input available to trace");
        //        }
        //        string CleanCode = Beautify(DirtyJS);
        //        IronUI.SetJSTaintTraceCode(CleanCode, false);
        //        IJ.Lines = SplitCodeLines(CleanCode);
        //        if (PauseAtTaint) IronUI.SetJSTaintTraceResult();
        //        IJ.StartedFromUI = true;
        //        IJ.Analyze(CleanCode);
        //        if (!PauseAtTaint) IronUI.SetJSTaintTraceResult();
        //        IronUI.ShowTraceStatus("Trace Completed", false);
        //        IronUI.ResetTraceStatus();
        //    }
        //    catch(ThreadAbortException)
        //    {}
        //    catch(Exception Exp)
        //    {
        //        StopUITrace();
        //        IronUI.ResetTraceStatus();
        //        IronUI.ShowTraceStatus("Trace Stopped due to error: " + Exp.Message, true);
        //        IronException.Report("Error performing JS Taint Trace", Exp.Message, Exp.StackTrace);
        //    }
        //}

        public static List<string> SplitCodeLines(string Code)
        {
            string[] UnTrimmedLines = Code.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<string> TrimmedLines = new List<string>();
            foreach (string Line in UnTrimmedLines)
            {
                string TrimmedLine = Line.Trim();
                if (TrimmedLine.Length > 0)
                {
                    TrimmedLines.Add(TrimmedLine);
                }
            }
            return TrimmedLines;
        }

        public static TraceResult Trace(string Code)
        {
            TraceResult TR = new TraceResult();
            try
            {
                IronJint IJ = new IronJint();
                IJ.AnalyzeCallStartTime = 0;
                IJ.SetSourcesAndSinks(DefaultSourceObjects, DefaultSinkObjects, DefaultSourceReturningMethods, DefaultSinkReturningMethods, DefaultArgumentReturningMethods, DefaultArgumentAssignedASourceMethods, DefaultArgumentAssignedToSinkMethods);
                IJ.ClearAllTaint();
                IJ.JintStack.Clear();
                string CleanCode = Beautify(Code);
            
                IJ.Analyze(CleanCode);
            
                TR.Lines.AddRange(IJ.RawLines);
                TR.SourceLineNos.AddRange(IJ.SourceLines);
                TR.SinkLineNos.AddRange(IJ.SinkLines);
                TR.SourceToSinkLineNos.AddRange(IJ.SourceToSinkLines);
                foreach (int LineNo in TR.SourceLineNos)
                {
                    TR.SourceLines.Add(IJ.RawLines[LineNo - 1]);
                }
                foreach (int LineNo in TR.SinkLineNos)
                {
                    TR.SinkLines.Add(IJ.RawLines[LineNo - 1]);
                }
                foreach (int LineNo in TR.SourceToSinkLineNos)
                {
                    TR.SourceToSinkLines.Add(IJ.RawLines[LineNo - 1]);
                }
            }
            catch (TimeoutException){}
            return TR;
        }

        public static TraceResult Trace(string Code, string Keyword)
        {
            TraceResult TR = new TraceResult();
            try
            {
                IronJint IJ = new IronJint();
                IJ.AnalyzeCallStartTime = 0;
                IJ.SetSourcesAndSinks(new List<string>() { Keyword }, DefaultSinkObjects, new List<string>(), DefaultSinkReturningMethods, DefaultArgumentReturningMethods, DefaultArgumentAssignedASourceMethods, DefaultArgumentAssignedToSinkMethods);
                IJ.ClearAllTaint();
                IJ.JintStack.Clear();
                IJ.KeywordToTrace = Keyword;
                IJ.TraceKeyword = true;
                string CleanCode = Beautify(Code);
                //List<string> Lines = new List<string>(CleanCode.Split(new string[] { "\r\n" }, StringSplitOptions.None));

                IJ.Analyze(CleanCode);
                //return IJ;

                TR.Lines.AddRange(IJ.RawLines);
                TR.SourceLineNos.AddRange(IJ.SourceLines);
                TR.SinkLineNos.AddRange(IJ.SinkLines);
                TR.SourceToSinkLineNos.AddRange(IJ.SourceToSinkLines);
                foreach (int LineNo in TR.SourceLineNos)
                {
                    TR.SourceLines.Add(IJ.RawLines[LineNo - 1]);
                }
                foreach (int LineNo in TR.SinkLineNos)
                {
                    TR.SinkLines.Add(IJ.RawLines[LineNo - 1]);
                }
                foreach (int LineNo in TR.SourceToSinkLineNos)
                {
                    TR.SourceToSinkLines.Add(IJ.RawLines[LineNo - 1]);
                }
                TR.KeywordContexts.AddRange(IJ.KeywordContexts);
            }
            catch(TimeoutException) { }
            return TR;
        }

        public static bool IsExpressionStatement(string Code, string Keyword)
        {
            //string CleanCode = Beautify(Code);
            LinkedList<Statement> Statements = GetStatementsFromCode(Code, false);
            foreach (Statement Stmt in Statements)
            {
                if (Stmt.GetType().Name.Equals("ExpressionStatement"))
                {
                    ExpressionStatement ExS = (ExpressionStatement)Stmt;
                    if (ExS.Expression.GetType().Name.Equals("Identifier"))
                    {
                        Identifier Id = (Identifier)ExS.Expression;
                        if (Id.Text.Equals(Keyword))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        internal void Analyze(string Code)
        {
            if (IsTimedOut()) return;
            RawLines.Clear();
            Analyze(new List<string>() { Code });
        }

        internal void Analyze(List<string> Codes)
        {
            if (IsTimedOut()) return;
            RawLines.Clear();
            foreach (string C in Codes)
            {
                RawLines.AddRange(SplitCodeLines(C));
            }
            JintStack.Clear();
            LinkedList<Statement> Statements = GetStatementsFromCode(Codes);
            Analyze(Statements);
        }

        internal static LinkedList<Statement> GetStatementsFromCode(string Code)
        {
            return GetStatementsFromCode(Code, true);
        }

        internal static LinkedList<Statement> GetStatementsFromCode(string Code, bool IgnoreErrors)
        {
            return GetStatementsFromCode(new List<string>() { Code }, IgnoreErrors);
        }

        internal static LinkedList<Statement> GetStatementsFromCode(List<string> Codes)
        {
            return GetStatementsFromCode(Codes, true);
        }

        internal static LinkedList<Statement> GetStatementsFromCode(List<string> Codes, bool IgnoreErrors)
        {
            StringBuilder CodeBuilder = new StringBuilder();
            foreach (string C in Codes)
            {
                CodeBuilder.Append(C);
                if (!C.EndsWith("\n"))
                {
                    CodeBuilder.Append("\r\n");
                }
            }
            string Code = CodeBuilder.ToString();
            if (Code.Length == 0) return new LinkedList<Statement>();
            JintEngine Eng = new JintEngine(Options.Ecmascript3);

            ES3Lexer Lexer = new ES3Lexer(new ANTLRStringStream(Code));
            ES3Parser Parser = new ES3Parser(new CommonTokenStream(Lexer)) { DebugMode = true };
            Jint.Expressions.Program Prog = Parser.program().value;
            if (Parser.Errors != null && Parser.Errors.Count > 0 && !IgnoreErrors)
            {
                throw new Exception("Invalid JavaScript Syntax");
            }
            return Prog.Statements;
        }

        

        void Analyze(LinkedList<Statement> Statements)
        {
            if (IsTimedOut()) return;

            foreach (Statement Stmt in Statements)
            {
                if (Stmt != null) Analyze(Stmt);
            }
            //Statement[] Stmts = new Statement[Statements.Count];
            //Statements.CopyTo(Stmts, 0);
            //for (int i = 0; i < Stmts.Length; i++)
            //{
                
            //    if (Stmts[i] != null) Analyze(Stmts[i]);
            //}
        }

        void Analyze(List<Statement> Statements)
        {
            if (IsTimedOut()) return;
            for (int i = 0; i < Statements.Count; i++)
            {
                if (Statements[i] != null) Analyze(Statements[i]);
            }
        }

        void Analyze(Jint.Expressions.Statement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            Type T = Stmt.GetType();
            switch (T.Name)
            {
                case("ArrayDeclaration"):
                    ArrayDeclaration AD = (ArrayDeclaration)Stmt;
                    Analyze(AD);
                    break;
                case ("AssignmentExpression"):
                    AssignmentExpression AE = (AssignmentExpression)Stmt;
                    Analyze(AE);
                    break;
                case ("BinaryExpression"):
                    BinaryExpression BE = (BinaryExpression)Stmt;
                    Analyze(BE);
                    break;
                case ("BlockStatement"):
                    BlockStatement BS = (BlockStatement)Stmt;
                    Analyze(BS);
                    break;
                case ("BreakStatement"):
                    BreakStatement BrS = (BreakStatement)Stmt;
                    Analyze(BrS);
                    break;
                case ("CaseClause"):
                    CaseClause CC = new CaseClause();
                    try
                    {
                        CC.Expression = (Expression)Stmt;
                        Analyze(CC);
                    }
                    catch { }
                    break;
                case ("CatchClause"):
                    try
                    {
                        CatchClause CaC = new CatchClause("a", Stmt);
                        Analyze(CaC);
                    }
                    catch { }
                    break;
                case ("ClrIdentifier"):
                    ClrIdentifier CI = (ClrIdentifier)Stmt;
                    Analyze(CI);
                    break;
                case ("CommaOperatorStatement"):
                    CommaOperatorStatement COS = (CommaOperatorStatement)Stmt;
                    Analyze(COS);
                    break;
                case ("ContinueStatement"):
                    ContinueStatement CS = (ContinueStatement)Stmt;
                    Analyze(CS);
                    break;
                case ("DoWhileStatement"):
                    DoWhileStatement DWS = (DoWhileStatement)Stmt;
                    Analyze(DWS);
                    break;
                case ("EmptyStatement"):
                    EmptyStatement ES = (EmptyStatement)Stmt;
                    Analyze(ES);
                    break;
                case ("ExpressionStatement"):
                    ExpressionStatement ExS = (ExpressionStatement)Stmt;
                    Analyze(ExS);
                    break;
                case ("FinallyClause"):
                    try
                    {
                        FinallyClause FC = new FinallyClause(Stmt);
                        Analyze(FC);
                    }
                    catch { }
                    break;
                case ("ForEachInStatement"):
                    ForEachInStatement FEIS = (ForEachInStatement)Stmt;
                    Analyze(FEIS);
                    break;
                case ("ForStatement"):
                    ForStatement FoS = (ForStatement)Stmt;
                    Analyze(FoS);
                    break;
                case ("FunctionDeclarationStatement"):
                    FunctionDeclarationStatement FDS = (FunctionDeclarationStatement)Stmt;
                    Analyze(FDS);
                    break;
                case ("FunctionExpression"):
                    FunctionExpression FE = (FunctionExpression)Stmt;
                    Analyze(FE);
                    break;
                case ("Identifier"):
                    Identifier Id = (Identifier)Stmt;
                    Analyze(Id);
                    break;
                case ("IfStatement"):
                    IfStatement IS = (IfStatement)Stmt;
                    Analyze(IS);
                    break;
                case ("Indexer"):
                    Indexer Ind = (Indexer)Stmt;
                    Analyze(Ind);
                    break;
                case ("JsonExpression"):
                    JsonExpression JE = (JsonExpression)Stmt;
                    Analyze(JE);
                    break;
                case ("MemberExpression"):
                    MemberExpression ME = (MemberExpression)Stmt;
                    Analyze(ME);
                    break;
                case ("MethodCall"):
                    MethodCall MC = (MethodCall)Stmt;
                    Analyze(MC);
                    break;
                case ("NewExpression"):
                    NewExpression NE = (NewExpression)Stmt;
                    Analyze(NE);
                    break;
                case ("Program"):
                    Jint.Expressions.Program Pr = (Jint.Expressions.Program)Stmt;
                    Analyze(Pr);
                    break;
                case ("PropertyDeclarationExpression"):
                    PropertyDeclarationExpression PDP = (PropertyDeclarationExpression)Stmt;
                    Analyze(PDP);
                    break;
                case ("PropertyExpression"):
                    PropertyExpression PE = (PropertyExpression)Stmt;
                    Analyze(PE);
                    break;
                case ("RegexpExpression"):
                    RegexpExpression RE = (RegexpExpression)Stmt;
                    Analyze(RE);
                    break;
                case ("ReturnStatement"):
                    ReturnStatement RS = (ReturnStatement)Stmt;
                    Analyze(RS);
                    break;
                case ("SwitchStatement"):
                    SwitchStatement SS = (SwitchStatement)Stmt;
                    Analyze(SS);
                    break;
                case ("TernaryExpression"):
                    TernaryExpression TE = (TernaryExpression)Stmt;
                    Analyze(TE);
                    break;
                case ("ThrowStatement"):
                    ThrowStatement TS = (ThrowStatement)Stmt;
                    Analyze(TS);
                    break;
                case ("TryStatement"):
                    TryStatement TrS = (TryStatement)Stmt;
                    Analyze(TrS);
                    break;
                case ("UnaryExpression"):
                    UnaryExpression UE = (UnaryExpression)Stmt;
                    Analyze(UE);
                    break;
                case ("ValueExpression"):
                    ValueExpression VE = (ValueExpression)Stmt;
                    Analyze(VE);
                    break;
                case ("VariableDeclarationStatement"):
                    VariableDeclarationStatement VDS = (VariableDeclarationStatement)Stmt;
                    Analyze(VDS);
                    break;
                case ("WhileStatement"):
                    WhileStatement WS = (WhileStatement)Stmt;
                    Analyze(WS);
                    break;
                case ("WithStatement"):
                    WithStatement WiS = (WithStatement)Stmt;
                    Analyze(WiS);
                    break;
            }
        }

        void Analyze(List<Expression> Statements)
        {
            if (IsTimedOut()) return;
            for (int i = 0; i < Statements.Count; i++)
            {
                bool IsMethodArgument = false;
                int StatusIndex = 0;
                if (JintStack.Count > 0)
                {
                    JintItem LastItem = JintStack[JintStack.Count - 1];
                    if (LastItem.State == JintState.MethodCallName || LastItem.State == JintState.MethodCallArgument)
                    {
                        IsMethodArgument = true;
                        StatusIndex = AddToJintStack(Statements[i].Source, JintState.MethodCallArgument);
                    }
                }

                if (Statements[0] != null) Analyze(Statements[i]);
                if (IsMethodArgument)
                {
                    if (JintStack.Count > 0 && (JintStack.Count > StatusIndex + 1))
                    {
                        List<JintItem> ArgumentItems = RemoveJintStackFrom(StatusIndex + 1);
                        List<JintItem> ArgumentItem = RemoveJintStackFrom(StatusIndex);
                        ArgumentItem[0].SubItems = new List<JintItem>(ArgumentItems);
                        JintStack.Add(ArgumentItem[0]);
                    }
                }
            }
        }

        void Analyze(ArrayDeclaration Stmt)
        {
            if (IsTimedOut()) return;
            if (Stmt.Parameters != null)
            {
                int StatusIndex = AddToJintStack(Stmt.Source, JintState.ArrayDeclaration);
                Analyze(Stmt.Parameters);
                RemoveJintStackFrom(StatusIndex);
            }
        }

        void Analyze(AssignmentExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            List<JintItem> LeftItems = new List<JintItem>();
            List<List<JintItem>> LeftItemParts = new List<List<JintItem>>();
            List<List<JintItem>> SinkTaintedLeftItems = new List<List<JintItem>>();

            ItemChecker TC = new ItemChecker(this);

            if (Stmt.Left != null)
            {
                int StatusIndex = AddToJintStack(Stmt.Left.Source, JintState.AssignmentExpressionLeft);
                Analyze(Stmt.Left);
                LeftItems = RemoveJintStackFrom(StatusIndex + 1);

                LeftItemParts = ItemChecker.GetItemParts(LeftItems);               

                foreach (List<JintItem> LeftItemPart in LeftItemParts)
                {
                    TaintResult LeftResult = TC.Check(LeftItemPart);
                    if (LeftResult.SinkTaint)
                    {
                        SinkTaintedLeftItems.Add(LeftItemPart);
                        if (LeftItemPart.Count > 0)
                            AddSinkLine(LeftItemPart[0].LineNo, LeftResult.SinkReasons);
                        else
                            AddSinkLine(CurrentLineNo, LeftResult.SinkReasons);
                    }
                }
                RemoveJintStackFrom(StatusIndex);
            }
            List<JintItem> RightItems = new List<JintItem>();
            if (Stmt.Right != null)
            {
                int StatusIndex = AddToJintStack(Stmt.Left.Source, JintState.AssignmentExpressionRight);
                Analyze(Stmt.Right);
                RightItems = RemoveJintStackFrom(StatusIndex + 1);

                if (Stmt.Right.GetType().Name.Equals("AssignmentExpression"))
                {
                    Analyze(((AssignmentExpression)Stmt.Right).Left);
                    RightItems = RemoveJintStackFrom(StatusIndex + 1);
                }

                List<List<JintItem>> RightItemParts = ItemChecker.GetItemParts(RightItems);
                UpdateMappings(LeftItems, RightItemParts);

                foreach (List<JintItem> RightItemPart in RightItemParts)
                {
                    TaintResult RightResult = TC.Check(RightItemPart);
                    if (RightResult.SourceTaint)
                    {
                        foreach (JintItem Item in LeftItems)
                        {
                            Item.SourceReasons.AddRange(RightResult.SourceReasons);
                        }
                        AddToSourceObjects(LeftItems);
                        if (RightItems.Count > 0)
                            AddSourceLine(RightItemPart[0].LineNo, RightResult.SourceReasons);
                        else
                            AddSourceLine(CurrentLineNo, RightResult.SourceReasons);
                        if (SinkTaintedLeftItems.Count > 0)
                            if (LeftItems.Count > 0)
                                AddSourceToSinkLine(LeftItems[0].LineNo);
                            else
                                AddSourceToSinkLine(CurrentLineNo);
                    }
                    else
                    {
                        foreach (List<JintItem> LeftItemPart in LeftItemParts)
                        {
                            RemoveFromSourceTaintedItems(LeftItemPart);
                        }
                    }
                    if (RightResult.SinkTaint)
                    {
                        foreach (JintItem Item in LeftItems)
                        {
                            Item.SinkReasons.AddRange(RightResult.SinkReasons);
                        }
                        foreach (List<JintItem> LeftItemPart in LeftItemParts)
                        {
                            AddToSinkObjects(LeftItemPart);
                        }
                        if (RightItems.Count > 0)
                            AddSinkLine(RightItemPart[0].LineNo, RightResult.SinkReasons);
                        else
                            AddSinkLine(CurrentLineNo, RightResult.SinkReasons);
                    }
                    else
                    {
                        foreach (List<JintItem> LeftItemPart in LeftItemParts)
                        {
                            RemoveFromSinkTaintedItems(LeftItemPart);
                        }
                    }
                }
                RemoveJintStackFrom(StatusIndex);
            }
        }

        void Analyze(BinaryExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.LeftExpression != null) Analyze(Stmt.LeftExpression);
            AddToJintStack(Stmt.Source, JintState.BinaryOperator, "");
            if (Stmt.RightExpression != null) Analyze(Stmt.RightExpression);
        }

        void Analyze(BlockStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if(Stmt.Statements != null) Analyze(Stmt.Statements);
        }

        void Analyze(BreakStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
        }

        void Analyze(CaseClause Stmt)
        {
            if (IsTimedOut()) return;
            if (Stmt.Statements != null) Analyze(Stmt.Statements);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
        }

        void Analyze(CatchClause Stmt)
        {
            if (IsTimedOut()) return;
            //if (Stmt.Identifier != null) Analyze(Stmt.Identifier);
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(ClrIdentifier Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            //if (Stmt.Text != null) Analyzer.CheckIdentifier(Stmt.Text);
        }

        void Analyze(CommaOperatorStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Statements != null) Analyze(Stmt.Statements);
        }

        void Analyze(ContinueStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
        }

        void Analyze(DoWhileStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if(Stmt.Condition != null) Analyze(Stmt.Condition);
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(EmptyStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
        }

        void Analyze(ExpressionStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
            if (TraceKeyword)
            {
                if(Stmt.Expression.GetType().Name.Equals("Identifier"))
                {
                    Identifier Id = (Identifier)Stmt.Expression;
                    if(Id.Text.Equals(KeywordToTrace, StringComparison.OrdinalIgnoreCase))
                    {
                        KeywordContexts.Add("Expression");
                    }
                }
            }
        }

        void Analyze(FinallyClause Stmt)
        {
            if (IsTimedOut()) return;
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(ForEachInStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
            if (Stmt.InitialisationStatement != null) Analyze(Stmt.InitialisationStatement);
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(ForStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.ConditionExpression != null) Analyze(Stmt.ConditionExpression);
            if (Stmt.IncrementExpression != null) Analyze(Stmt.IncrementExpression);
            if (Stmt.InitialisationStatement != null) Analyze(Stmt.InitialisationStatement);
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(FunctionDeclarationStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Name != null)
            {
                int StatusIndex = AddToJintStack(Stmt.Source, JintState.MethodName);
                JintStack[StatusIndex].Value = Stmt.Name;
                //Analyzer.CheckIdentifier(Stmt.Name);
            }
            if (Stmt.Parameters != null)
            {
                for (int i=0; i< Stmt.Parameters.Count; i++)
                {
                    if (Stmt.Parameters[i] != null)
                    {
                        AddToJintStack(Stmt.Source, JintState.MethodArgumentIdentifier);                        
                    }
                }
            }
            if (Stmt.Statement != null)
            {
                //function body is declared here. Variable scoping etc must be handled.
                Analyze(Stmt.Statement);
            }
        }

        void Analyze(FunctionExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(Identifier Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Text != null)
            {
                if (JintStack.Count == 0)
                {
                    AddToJintStack(Stmt.Source, JintState.Identifier, Stmt.Text);
                    return;
                }
                int LastItem = JintStack.Count - 1;
                switch (JintStack[LastItem].State)
                {
                    case (JintState.AssignmentExpressionLeft):
                    case (JintState.AssignmentExpressionRight):
                    case (JintState.MethodCallArgument):
                        AddToJintStack(Stmt.Source, JintState.Identifier, Stmt.Text);
                        break;
                    case (JintState.Identifier):
                        AddToJintStack(Stmt.Source, JintState.Property, Stmt.Text);
                        break;
                    case (JintState.Indexer):
                        AddToJintStack(Stmt.Source, JintState.Identifier, Stmt.Text);
                        break;
                    case(JintState.MethodArgument):
                        RemoveJintStackFrom(JintStack.Count - 1);
                        AddToJintStack(Stmt.Source, JintState.MethodArgumentIdentifier, Stmt.Text);
                        break;
                    default:
                        AddToJintStack(Stmt.Source, JintState.Identifier, Stmt.Text);
                        break;
                }
            }
        }

        void Analyze(IfStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
            if (Stmt.Then != null) Analyze(Stmt.Then);
            if (Stmt.Else != null) Analyze(Stmt.Else);
        }

        void Analyze(Indexer Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Index != null)
            {
                int StatusIndex = AddToJintStack(Stmt.Source, JintState.Indexer);
                Analyze(Stmt.Index);
                if (JintStack[StatusIndex].State == JintState.Indexer)
                {
                    List<JintItem> SubItems = RemoveJintStackFrom(StatusIndex + 1);
                    if (SubItems.Count > 1)
                    {
                        JintStack[StatusIndex].SubItems = new List<JintItem>(SubItems);
                    }
                    else if (SubItems.Count == 1)
                    {
                        if (SubItems[0].State == JintState.Identifier)
                        {
                            JintStack[StatusIndex].State = JintState.IdentifierIndex;
                            JintStack[StatusIndex].Value = SubItems[0].Value;
                        }
                        else if (SubItems[0].State == JintState.StringValue)
                        {
                            JintStack[StatusIndex].State = JintState.StringIndex;
                            JintStack[StatusIndex].Value = SubItems[0].Value;
                        }
                        else if (SubItems[0].State == JintState.IntValue)
                        {
                            JintStack[StatusIndex].State = JintState.IntIndex;
                            JintStack[StatusIndex].Value = SubItems[0].Value;
                        }
                    }
                }
            }
        }

        void Analyze(JsonExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Values != null)
            {
                foreach(string Name in Stmt.Values.Keys)
                {
                    //if (Name != null) Analyzer.CheckIdentifier(Name);
                    Analyze(Stmt.Values[Name]);
                }
            }
        }

        void Analyze(MemberExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Previous != null) Analyze(Stmt.Previous);
            if (Stmt.Member != null) Analyze(Stmt.Member);
        }

        void Analyze(MethodCall Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            int LineNo = CurrentLineNo;
            JintItem LastItem;
            if (JintStack.Count > 0)
            {
                LastItem = JintStack[JintStack.Count - 1];
                if (LastItem.State == JintState.MethodCallArgument || LastItem.State == JintState.MethodName)
                {
                    LastItem = new JintItem(CurrentLineNo, 0, JintState.AnonymousMethod);
                    JintStack.Add(LastItem);
                }
            }
            else
            {
                LastItem = new JintItem(CurrentLineNo, 0, JintState.AnonymousMethod);
                JintStack.Add(LastItem);
            }
            int MethodCallIndex = 0;
            if (LastItem.State == JintState.Identifier || LastItem.State == JintState.Property || LastItem.State == JintState.Indexer || LastItem.State == JintState.StringIndex || LastItem.State == JintState.IdentifierIndex || LastItem.State == JintState.IntIndex || LastItem.State == JintState.AnonymousMethod || LastItem.LineNo != LineNo)
            {
                RemoveJintStackFrom(JintStack.Count - 1);
                if (LastItem.State == JintState.Identifier || LastItem.State == JintState.Property)
                    JintStack.Add(new JintItem(LastItem.LineNo, LastItem.CharNo, JintState.MethodCallName, LastItem.Value));
                else
                    JintStack.Add(new JintItem(LastItem.LineNo, LastItem.CharNo, JintState.MethodCallName, ""));

                LineNo = LastItem.LineNo;
                MethodCallIndex = JintStack.Count - 1;

                if (Stmt.Arguments != null)
                {
                    if (Stmt.Arguments.Count > 0)
                        Analyze(Stmt.Arguments);
                    else
                        AddToJintStack(Stmt.Source, JintState.MethodCallArgument);
                }

                List<JintItem> MethodRelatedItems = GetMethodItems(MethodCallIndex);
                ItemChecker IC = new ItemChecker(this);
                List<JintItem> MethodArguments = IC.GetLastMethodArguments(MethodRelatedItems);
                List<int> SourcePositions = new List<int>();
                List<int> SinkPositions = new List<int>();

                for (int i = 0; i < MethodArguments.Count; i++)
                {
                    TaintResult Result = IC.Check(MethodArguments[i].SubItems);
                    if (Result.SourceTaint)
                    {
                        SourcePositions.Add(i);
                        Result.SourceReasons.Add("Method Argument is a Source");
                        AddSourceLine(CurrentLineNo, Result.SourceReasons);
                    }
                    if (Result.SinkTaint)
                    {
                        SinkPositions.Add(i);
                        Result.SinkReasons.Add("Method Argument is a Sink");
                        AddSinkLine(CurrentLineNo, Result.SinkReasons);
                    }
                }

                foreach (List<JintItem> Template in ArgumentAssignedASourceMethods)
                {
                    if(Template.Count == 0) continue;
                    TaintResult MethodResult = IC.IsMatch(MethodRelatedItems, Template);
                    if (MethodResult.NeutralReasons.Count > 0)
                    {
                        List<JintItem> TemplateArguments = IC.GetLastMethodArguments(Template);
                        if(TemplateArguments.Count == 0) continue;
                        AddSourceLine(LineNo, Template[0].SourceReasons);
                        if (TemplateArguments.Count == 1 && TemplateArguments[0].SubItems.Count == 0 && SinkPositions.Count > 0)
                            AddSourceToSinkLine(LineNo);
                        else if (MethodArguments.Count == TemplateArguments.Count)
                        {
                            foreach (int i in SinkPositions)
                            {
                                if(TemplateArguments.Count > i)
                                    if(TemplateArguments[i].SubItems.Count > 0)
                                        if(TemplateArguments[i].SubItems[0].State == JintState.MethodCallArgumentTaintPointer)
                                            AddSourceToSinkLine(LineNo);
                            }
                        }
                    }
                }

                foreach (List<JintItem> Template in ArgumentAssignedToSinkMethods)
                {
                    if (Template.Count == 0) continue;
                    TaintResult MethodResult = IC.IsMatch(MethodRelatedItems, Template);
                    if (MethodResult.NeutralReasons.Count > 0)
                    {
                        List<JintItem> TemplateArguments = IC.GetLastMethodArguments(Template);
                        if (TemplateArguments.Count == 0) continue;
                        AddSinkLine(LineNo, Template[0].SinkReasons);
                        if (TemplateArguments.Count == 1 && TemplateArguments[0].SubItems.Count == 0 && SourcePositions.Count > 0)
                            AddSourceToSinkLine(LineNo);
                        else if (MethodArguments.Count == TemplateArguments.Count)
                        {
                            foreach (int i in SourcePositions)
                            {
                                if (TemplateArguments.Count > i)
                                    if (TemplateArguments[i].SubItems.Count > 0)
                                        if (TemplateArguments[i].SubItems[0].State == JintState.MethodCallArgumentTaintPointer)
                                            AddSourceToSinkLine(LineNo);
                            }
                        }
                    }
                }
            }
            else
            {
                IronException.Report("MethodName missing in IronJint", "LastItem State -" + LastItem.State.ToString());
            }

            if (Stmt.Generics != null) Analyze(Stmt.Generics);
        }

        void Analyze(NewExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Arguments != null) Analyze(Stmt.Arguments);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
            if (Stmt.Generics != null) Analyze(Stmt.Generics);
        }

        void Analyze(Jint.Expressions.Program Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Statements != null) Analyze(Stmt.Statements);
        }

        void Analyze(PropertyDeclarationExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
            if (Stmt.GetExpression != null) Analyze(Stmt.GetExpression);
            if (Stmt.SetExpression != null) Analyze(Stmt.SetExpression);
            //if (Stmt.Name != null) Analyzer.CheckIdentifier(Stmt.Name);
        }

        void Analyze(PropertyExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Text != null)
            {
                AddToJintStack(Stmt.Source, JintState.Property, Stmt.Text);
            }
        }

        void Analyze(RegexpExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            //if (Stmt.Options != null) Analyzer.CheckIdentifier(Stmt.Options);
            //if (Stmt.Regexp != null) Analyzer.CheckIdentifier(Stmt.Regexp);
        }

        void Analyze(ReturnStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Expression != null)
            {
                int StatusIndex = JintStack.Count;
                Analyze(Stmt.Expression);
                if (JintStack.Count > StatusIndex)
                {
                    List<JintItem> ReturnItems = RemoveJintStackFrom(StatusIndex);
                    if (ReturnItems.Count > 0)
                    {
                        ItemChecker IC = new ItemChecker(this);
                        TaintResult ReturnResult = IC.Check(ReturnItems);
                        if (ReturnResult.SourceTaint)
                            AddSourceLine(ReturnItems[0].LineNo, ReturnResult.SourceReasons);
                        if (ReturnResult.SinkTaint)
                            AddSinkLine(ReturnItems[0].LineNo, ReturnResult.SinkReasons);
                    }
                }
            }
        }

        void Analyze(SwitchStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.CaseClauses != null)
            {
                for (int i = 0; i < Stmt.CaseClauses.Count; i++)
                {
                    if (Stmt.CaseClauses[i] != null) Analyze(Stmt.CaseClauses[i]);
                }
            }
            if (Stmt.DefaultStatements != null) Analyze(Stmt.DefaultStatements);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
        }

        void Analyze(TernaryExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.LeftExpression != null) Analyze(Stmt.LeftExpression);
            if (Stmt.MiddleExpression != null) Analyze(Stmt.MiddleExpression);
            if (Stmt.RightExpression != null) Analyze(Stmt.RightExpression);
        }

        void Analyze(ThrowStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Expression != null) Analyze(Stmt.Expression);
        }

        void Analyze(TryStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Catch != null) Analyze(Stmt.Catch);
            if (Stmt.Finally != null) Analyze(Stmt.Finally);
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(UnaryExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            
            if (Stmt.Type == UnaryExpressionType.Not && Stmt.Expression == null)
            {
                if (JintStack.Count > 0)
                {
                    if (JintStack[JintStack.Count - 1].State == JintState.MethodCallArgument)
                    {
                        JintStack.Add(new JintItem(Stmt.Source, JintState.MethodCallArgumentTaintPointer, this));
                    }
                }
            }

            if (Stmt.Expression != null)
            {
                Analyze(Stmt.Expression);
            }
        }

        void Analyze(ValueExpression Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (JintStack.Count == 0)
            {
                if (Stmt.TypeCode == TypeCode.String)
                    AddToJintStack(Stmt.Source, JintState.StringValue, Stmt.Value.ToString());
                else
                    AddToJintStack(Stmt.Source, JintState.NonStringValue, Stmt.Value.ToString());
                return;
            }
            JintItem LastItem = JintStack[JintStack.Count - 1];
            switch (LastItem.State)
            {
                case (JintState.Indexer):
                    if(Stmt.TypeCode == TypeCode.String)
                        AddToJintStack(Stmt.Source, JintState.StringValue, Stmt.Value.ToString());
                    else
                        AddToJintStack(Stmt.Source, JintState.IntValue, Stmt.Value.ToString());
                    break;
                default:
                    AddToJintStack(Stmt.Source, JintState.StringValue, Stmt.Value.ToString());
                    break;
            }
            if (TraceKeyword)
            {
                List<string> Contexts = GetContextInLine(RawLines[CurrentLineNo - 1], Stmt.Value.ToString());
                if (Contexts.Count == 0 && Stmt.Value.ToString().IndexOf(KeywordToTrace) >= 0)
                {
                    if(Stmt.TypeCode == TypeCode.String)
                        KeywordContexts.Add("StringValue");
                    else
                        KeywordContexts.Add("NonStringValue");
                }
                KeywordContexts.AddRange(Contexts);
            }
            //Analyzer.CheckIdentifier(Stmt.Value.ToString());
        }

        void Analyze(VariableDeclarationStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            ItemChecker TC = new ItemChecker(this);
            if (Stmt.Identifier != null)
            {
                List<JintItem> LeftItems = new List<JintItem>() { new JintItem(Stmt.Source, JintState.Identifier, Stmt.Identifier, this)};
                int StatusIndex = AddToJintStack(Stmt.Source, JintState.Identifier, Stmt.Identifier);

                if (Stmt.Expression != null)
                {
                    int RightExpIndex = AddToJintStack(Stmt.Source, JintState.AssignmentExpressionRight);
                    Analyze(Stmt.Expression);

                    List<JintItem> RightItems = RemoveJintStackFrom(RightExpIndex + 1);

                    if (Stmt.Expression.GetType().Name.Equals("AssignmentExpression"))
                    {
                        Analyze(((AssignmentExpression)Stmt.Expression).Left);
                        RightItems = RemoveJintStackFrom(RightExpIndex + 1);
                    }

                    List<List<JintItem>> RightItemParts = ItemChecker.GetItemParts(RightItems);
                    UpdateMappings(LeftItems, RightItemParts);

                    foreach (List<JintItem> RightItemPart in RightItemParts)
                    {
                        TaintResult RightResult = TC.Check(RightItemPart);
                        if (RightResult.SourceTaint)
                        {
                            foreach (JintItem Item in LeftItems)
                            {
                                Item.SourceReasons.AddRange(RightResult.SourceReasons);
                            }
                            AddToSourceObjects(LeftItems);
                            if (RightItems.Count > 0)
                                AddSourceLine(RightItemPart[0].LineNo, RightResult.SourceReasons);
                            else
                                AddSourceLine(CurrentLineNo, RightResult.SourceReasons);
                        }
                        if (RightResult.SinkTaint)
                        {
                            foreach (JintItem Item in LeftItems)
                            {
                                Item.SinkReasons.AddRange(RightResult.SinkReasons);
                            }
                            AddToSinkObjects(LeftItems);
                            if (RightItems.Count > 0)
                                AddSinkLine(RightItemPart[0].LineNo, RightResult.SinkReasons);
                            else
                                AddSinkLine(CurrentLineNo, RightResult.SinkReasons);
                        }
                    }
                    RemoveJintStackFrom(StatusIndex);
                }
            }
        }

        void Analyze(WhileStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Condition != null) Analyze(Stmt.Condition);
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        void Analyze(WithStatement Stmt)
        {
            if (IsTimedOut()) return;
            SetCurrentLineAndCharNos(Stmt);
            if (Stmt.Expression != null)
            {
                if (Stmt.Expression.GetType().Name.Equals("ValueExpression"))
                {
                    ValueExpression ValStmt = (ValueExpression)Stmt.Expression;
                    if (ValStmt.Value != null) AddToJintStack(Stmt.Source, JintState.WithStringValue, ValStmt.Value.ToString());
                }
                else
                {
                    AddToJintStack(Stmt.Source, JintState.WithExpression);
                }
                Analyze(Stmt.Expression);
            }
            if (Stmt.Statement != null) Analyze(Stmt.Statement);
        }

        //internal static void ShowDefaultTaintConfig()
        //{
        //    List<List<string>> AllLists = new List<List<string>>() { new List<string>(DefaultSourceObjects), new List<string>(DefaultSinkObjects), new List<string>(DefaultSourceReturningMethods), new List<string>(DefaultSinkReturningMethods), new List<string>(DefaultArgumentReturningMethods), new List<string>(DefaultArgumentAssignedASourceMethods), new List<string>(DefaultArgumentAssignedToSinkMethods)};
        //    int MaxCount = 0;
        //    foreach (List<string> List in AllLists)
        //    {
        //        if (List.Count > MaxCount) MaxCount = List.Count;
        //    }
        //    foreach (List<string> List in AllLists)
        //    {
        //        while (List.Count < MaxCount) 
        //        {
        //            List.Add(""); 
        //        }
        //    }
        //    //IronUI.SetTaintConfig(AllLists, MaxCount);
        //}

        internal void SetSourcesAndSinks(List<string> SourceObjs, List<string> SinkObjs, List<string> SourceRetMets, List<string> SinkRetMets, List<string> ArgRetMets, List<string> ArgAssASourceMets, List<string> ArgAssToSinkMets)
        {
            SetSourceObjects(SourceObjs);
            SetSinkObjects(SinkObjs);
            SetSourceReturningMethods(SourceRetMets);
            SetSinkReturningMethods(SinkRetMets);
            SetArgumentReturningMethods(ArgRetMets);
            SetArgumentAssignedToSinkMethods(ArgAssToSinkMets);
            SetArgumentAssignedASourceMethods(ArgAssASourceMets);
            this.RawLines.Clear();
        }

        internal void SetSourceObjects(List<string> Taints)
        {
            SourceObjects.Clear();

            foreach (string RawLine in Taints)
            {
                JintStack.Clear();
                Analyze(RawLine);
                foreach (JintItem Item in JintStack)
                {
                    Item.SourceReasons.Add("Preconfigured SourceObject");
                }
                SourceObjects.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetSinkObjects(List<string> Taints)
        {
            SinkObjects.Clear();

            foreach (string RawLine in Taints)
            {
                JintStack.Clear();
                Analyze(RawLine);
                foreach (JintItem Item in JintStack)
                {
                    Item.SinkReasons.Add("Preconfigured SinkObject");
                }
                SinkObjects.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetSourceReturningMethods(List<string> Taints)
        {
            SourceReturningMethods.Clear();

            foreach (string RawLine in Taints)
            {
                JintStack.Clear();
                Analyze(RawLine);
                foreach (JintItem Item in JintStack)
                {
                    Item.SourceReasons.Add("Preconfigured SourceReturningMethod");
                }
                SourceReturningMethods.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetSinkReturningMethods(List<string> Taints)
        {
            SinkReturningMethods.Clear();

            foreach (string RawLine in Taints)
            {
                JintStack.Clear();
                Analyze(RawLine);
                foreach (JintItem Item in JintStack)
                {
                    Item.SourceReasons.Add("Preconfigured SinkReturningMethod");
                }
                SinkReturningMethods.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetArgumentReturningMethods(List<string> Taints)
        {
            
            ArgumentReturningMethods.Clear();

            foreach (string RawLine in Taints)
            {
                JintStack.Clear();
                Analyze(RawLine);
                ArgumentReturningMethods.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetArgumentAssignedToSinkMethods(List<string> Taints)
        {

            ArgumentAssignedToSinkMethods.Clear();

            foreach (string RawLine in Taints)
            {
                JintStack.Clear();
                Analyze(RawLine);
                ArgumentAssignedToSinkMethods.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetArgumentAssignedASourceMethods(List<string> Taints)
        {

            ArgumentAssignedASourceMethods.Clear();

            foreach (string RawLine in Taints)
            {
                JintStack.Clear();
                Analyze(RawLine);
                ArgumentAssignedASourceMethods.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetSources(string Sources)
        {
            List<string> SourceLines = new List<string>(Sources.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries));
            
            JintStack.Clear();
            SourceItems.Clear();

            foreach (string RawLine in SourceLines)
            {
                JintStack.Clear();
                Analyze(RawLine);
                SourceItems.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        internal void SetSinks(string Sinks)
        {
            List<string> SinkLines = new List<string>(Sinks.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            
            JintStack.Clear();
            SinkItems.Clear();

            foreach (string RawLine in SinkLines)
            {
                JintStack.Clear();
                Analyze(RawLine);
                SinkItems.Add(new List<JintItem>(JintStack));
                JintStack.Clear();
            }
        }

        List<JintItem> RemoveJintStackFrom(int Index)
        {
            List<JintItem> Items = new List<JintItem>();
            if (JintStack.Count > Index)
            {
                for (int i = Index; i < JintStack.Count; i++)
                {
                    Items.Add(JintStack[i]);
                }
                JintStack.RemoveRange(Index, JintStack.Count - Index);
            }
            return Items;
        }

        int AddToJintStack(Jint.Debugger.SourceCodeDescriptor Source, JintState State)
        {
            return AddToJintStack(Source, State, "");
        }

        int AddToJintStack(Jint.Debugger.SourceCodeDescriptor Source, JintState State, string Value)
        {
            JintStack.Add(new JintItem(Source, State, Value, this));
            return (JintStack.Count - 1);
        }

        List<JintItem> GetMethodItems(int MethodCallIndex)
        {
            List<JintItem> MethodItems = new List<JintItem>();
            int LineNo = 0;
            if (JintStack.Count <= MethodCallIndex) return MethodItems;
            LineNo = JintStack[MethodCallIndex].LineNo;
            if (LineNo == 0) LineNo = CurrentLineNo;

            bool TopReached = false;
            int StartPointer = MethodCallIndex;
            while (!TopReached)
            {
                TopReached = true;
                StartPointer--;
                if (StartPointer >= 0 && JintStack[StartPointer].LineNo == LineNo)
                {
                    switch (JintStack[StartPointer].State)
                    {
                        case(JintState.AssignmentExpressionLeft):
                        case (JintState.AssignmentExpressionRight):
                        case (JintState.WithStringValue):
                        case (JintState.WithExpression):
                            TopReached = true;
                            StartPointer++;
                            break;
                        case(JintState.Identifier):
                            TopReached = true;
                            break;
                        default:
                            TopReached = false;
                            break;
                    }
                }
                else
                {
                    StartPointer++;
                }
            }
            bool BottomReached = false;
            int EndPointer = MethodCallIndex;
            while (!BottomReached)
            {
                BottomReached = true;
                EndPointer++;
                if (EndPointer < JintStack.Count)
                {
                    if (JintStack[EndPointer].State == JintState.MethodCallArgument)
                    {
                        BottomReached = false;
                    }
                    else
                    {
                        BottomReached = true;
                        EndPointer--;
                    }
                }
                else
                {
                    EndPointer--;
                }
            }
            for (int i = StartPointer; i <= EndPointer; i++)
            {
                MethodItems.Add(JintStack[i]);
            }
            return MethodItems;
        }

        void SetCurrentLineAndCharNos(Statement Stmt)
        {
            if (Stmt != null)
            {
                if (Stmt.Source != null)
                {
                    CurrentLineNo = Stmt.Source.Start.Line;
                    CurrentCharNo = Stmt.Source.Start.Char;
                }
            }
        }

        void AddToSourceObjects(List<JintItem> Item)
        {
            if (Item.Count > 0)
            {
                if (SourceLinesToIgnore.Contains(Item[0].LineNo)) return;
            }
            foreach(List<JintItem> Tainted in SourceObjects)
            {
                if (ItemChecker.IsSimilar(Item, Tainted)) return;
            }
            SourceObjects.Add(Item);
        }

        void AddToSinkObjects(List<JintItem> Item)
        {
            if (Item.Count > 0)
            {
                if (SinkLinesToIgnore.Contains(Item[0].LineNo)) return;
            }
            foreach (List<JintItem> Tainted in SinkObjects)
            {
                if (ItemChecker.IsSimilar(Item, Tainted)) return;
            }
            SinkObjects.Add(Item);
        }

        void RemoveFromSourceTaintedItems(List<JintItem> Item)
        {
            if (Item.Count > 0)
            {
                if (SourceLinesToInclude.Contains(Item[0].LineNo)) return;
            }
            int RemoveIndex = -1;
            for (int i=0; i< SourceTaintedItems.Count; i++)
            {
                List<JintItem> Tainted = SourceTaintedItems[i];
                if (ItemChecker.IsSimilar(Item, Tainted)) RemoveIndex = i;
            }
            if (RemoveIndex >= 0)
                SourceTaintedItems.RemoveAt(RemoveIndex);
        }

        void RemoveFromSinkTaintedItems(List<JintItem> Item)
        {
            if (Item.Count > 0)
            {
                if (SinkLinesToInclude.Contains(Item[0].LineNo)) return;
            }
            int RemoveIndex = -1;
            for (int i = 0; i < SinkTaintedItems.Count; i++)
            {
                List<JintItem> Tainted = SinkTaintedItems[i];
                if (ItemChecker.IsSimilar(Item, Tainted)) RemoveIndex = i;
            }
            if (RemoveIndex >= 0)
                SinkTaintedItems.RemoveAt(RemoveIndex);
        }

        void AddSourceLine(int LineNo, List<string> SourceReaons)
        {
            if (LineNo == 0) LineNo = CurrentLineNo;
            if (SourceLinesToIgnore.Contains(LineNo)) return;
            if (!SourceLines.Contains(LineNo)) SourceLines.Add(LineNo);

            if (SourceReasons.ContainsKey(LineNo))
                SourceReasons[LineNo].AddRange(SourceReaons);
            else
                SourceReasons.Add(LineNo, SourceReaons);

            //if (StartedFromUI && PauseAtTaint)
            //{
            //    if (SourceToSinkLines.Contains(LineNo))
            //        IronUI.SetJSTaintTraceLine("SourceToSink", LineNo);
            //    else if(SinkLines.Contains(LineNo))
            //        IronUI.SetJSTaintTraceLine("SourcePlusSink", LineNo);
            //    else
            //        IronUI.SetJSTaintTraceLine("Source", LineNo);
            //    IronUI.ShowTaintReasons(LineNo, IronJint.UIIJ.GetSourceReasons(LineNo), IronJint.UIIJ.GetSinkReasons(LineNo));
            //    PauseAtTaintLine(LineNo);
            //    IronUI.ShowTaintReasons(LineNo, new List<string>(), new List<string>());
            //}
        }
        void AddSinkLine(int LineNo, List<string>SinkReaons)
        {
            if (LineNo == 0) LineNo = CurrentLineNo;
            if (SinkLinesToIgnore.Contains(LineNo)) return;
            if (!SinkLines.Contains(LineNo)) SinkLines.Add(LineNo);

            if (SinkReasons.ContainsKey(LineNo))
                SinkReasons[LineNo].AddRange(SinkReaons);
            else
                SinkReasons.Add(LineNo,SinkReaons);

            //if (StartedFromUI && PauseAtTaint)
            //{
            //    if (SourceToSinkLines.Contains(LineNo))
            //        IronUI.SetJSTaintTraceLine("SourceToSink", LineNo);
            //    else if (SourceLines.Contains(LineNo))
            //        IronUI.SetJSTaintTraceLine("SourcePlusSink", LineNo);
            //    else
            //        IronUI.SetJSTaintTraceLine("Sink", LineNo);
            //    IronUI.ShowTaintReasons(LineNo, IronJint.UIIJ.GetSourceReasons(LineNo), IronJint.UIIJ.GetSinkReasons(LineNo));
            //    PauseAtTaintLine(LineNo);
            //    IronUI.ShowTaintReasons(LineNo, new List<string>(), new List<string>());
            //}
        }
        void AddSourceToSinkLine(int LineNo)
        {
            if (LineNo == 0) LineNo = CurrentLineNo;
            if (SourceLinesToIgnore.Contains(LineNo) || SinkLinesToIgnore.Contains(LineNo)) return;
            if (!SourceToSinkLines.Contains(LineNo)) SourceToSinkLines.Add(LineNo);
            //if (StartedFromUI && PauseAtTaint)
            //{
            //    IronUI.SetJSTaintTraceLine("SourceToSink", LineNo);
            //    IronUI.ShowTaintReasons(LineNo, IronJint.UIIJ.GetSourceReasons(LineNo), IronJint.UIIJ.GetSinkReasons(LineNo));
            //    PauseAtTaintLine(LineNo);
            //    IronUI.ShowTaintReasons(LineNo, new List<string>(), new List<string>());
            //}
        }

        //void PauseAtTaintLine(int LineNo)
        //{
        //    MSR.Reset();
        //    IronUI.ShowTraceContinuteButton();
        //    IronUI.ShowTraceStatus("Paused at Taint. Line No: " + LineNo.ToString(), false);
        //    MSR.WaitOne();
        //    IronUI.RemoveTaintPauseMarker(LineNo);
        //    IronUI.ShowTraceStatus("Trace in progress...", false);
        //}

        string GetPropertyValue(List<JintItem> Item)
        {
            return "";
        }

        void SetPropertyValue(List<JintItem> Item, string Value)
        {

        }

        void ClearAllTaint()
        {
            SourceLines.Clear();
            SinkLines.Clear();
            SourceToSinkLines.Clear();
            SourceTaintedItems.Clear();
            SinkTaintedItems.Clear();
        }

        internal void StoreStringObject(List<JintItem> Object, string Value)
        {
            if (Value.Length == 0) return;
            string ExistingValue = GetStringObject(Object);
            if (ExistingValue.Equals(Value)) return;
            
            if (ExistingValue.Length > 0)
            {
                int RemovalIndex = -1;
                for (int i = 0; i < InterestingStringHolders[ExistingValue].Count; i++)
                {
                    ItemChecker IC = new ItemChecker(this);
                    if (IC.DoItemsMatch(InterestingStringHolders[ExistingValue][i], Object).NeutralReasons.Count > 0) RemovalIndex = i;
                }
                if (RemovalIndex >= 0) InterestingStringHolders[ExistingValue].RemoveAt(RemovalIndex);
            }

            if (InterestingStringHolders.ContainsKey(Value))
                InterestingStringHolders[Value].Add(Object);
            else
                InterestingStringHolders.Add(Value, new List<List<JintItem>>() { Object });
        }

        internal string GetStringObject(List<JintItem> Object)
        {
            bool FirstCheck = true;
            ItemChecker IC = new ItemChecker(this);
            int AddedAt = 0;

            foreach (List<JintItem> StrItem in SentForStringConversion)
            {
                if (ItemChecker.IsSimilar(Object, StrItem))
                {
                    FirstCheck = false;
                    break;
                }
            }

            if (FirstCheck)
            {
                SentForStringConversion.Add(Object);
                AddedAt = SentForStringConversion.Count - 1;
            }

            foreach (string Key in InterestingStringHolders.Keys)
            {
                foreach (List<JintItem> Item in InterestingStringHolders[Key])
                {
                    if (FirstCheck)
                    {
                        if (IC.DoItemsMatch(Item, Object).NeutralReasons.Count > 0)
                        {
                            SentForStringConversion.RemoveAt(AddedAt);
                            return Key;
                        }
                    }
                    else
                    {
                        if (ItemChecker.IsSimilar(Item, Object)) return Key;                        
                    }
                }
            }
            if (FirstCheck) SentForStringConversion.RemoveAt(AddedAt);
            return "";
        }

        internal void RemoveStringObject(List<JintItem> Object)
        {
            foreach (string Key in InterestingStringHolders.Keys)
            {
                List<int> RemoveIndexes = new List<int>();
                int Index = -1;
                foreach (List<JintItem> Item in InterestingStringHolders[Key])
                {
                    Index++;
                    ItemChecker IC = new ItemChecker(this);
                    if (IC.DoItemsMatch(Item, Object).NeutralReasons.Count > 0) RemoveIndexes.Add(Index);
                }
                for (int i = 0; i < RemoveIndexes.Count; i++ )
                {
                    InterestingStringHolders[Key].RemoveAt(RemoveIndexes[i] - i);
                }
            }
        }

        internal void UpdateMappings(List<JintItem> LeftItem, List<List<JintItem>>RightItems)
        {
            if (RightItems.Count != 1) return;
            List<JintItem> RightItem = RightItems[0];
            
            string ExistingValue = GetStringObject(RightItem);
            if (ExistingValue.Length > 0)
            {
                StoreStringObject(LeftItem, ExistingValue);
            }
            else
            {
                if (RightItem.Count == 1)
                {
                    if (RightItem[0].State == JintState.StringValue)
                    {
                        StoreStringObject(LeftItem, RightItem[0].Value);
                        return;
                    }
                    else
                    {
                        RemoveStringObject(LeftItem);
                    }
                }
                else
                {
                    RemoveStringObject(LeftItem);
                }
            }

            foreach (string StoredString in InterestingStringHolders.Keys)
            {
                List<List<JintItem>> ItemsToAdd = new List<List<JintItem>>();
                foreach (List<JintItem> ListItem in InterestingStringHolders[StoredString])
                {
                    List<JintItem> ResultItem = GetMappedItem(LeftItem, RightItem, ListItem);
                    if (ResultItem.Count > 0) ItemsToAdd.Add(ResultItem);
                }
                if (ItemsToAdd.Count > 0) InterestingStringHolders[StoredString].AddRange(ItemsToAdd);
            }

            List<List<List<JintItem>>> AllLists = new List<List<List<JintItem>>>() { SourceObjects, SinkObjects, SourceReturningMethods, SinkReturningMethods, ArgumentReturningMethods, ArgumentAssignedToSinkMethods, ArgumentAssignedASourceMethods};
            foreach (List<List<JintItem>> List in AllLists)
            {
                List<List<JintItem>> ItemsToAdd = new List<List<JintItem>>();
                foreach (List<JintItem> ListItem in List)
                {
                    List<JintItem> ResultItem = GetMappedItem(LeftItem, RightItem, ListItem);
                    if (ResultItem.Count > 0) ItemsToAdd.Add(ResultItem);
                }
                if (ItemsToAdd.Count > 0) List.AddRange(ItemsToAdd);
            }
        }

        List<JintItem> GetMappedItem(List<JintItem> LeftItem, List<JintItem> RightItem, List<JintItem> List)
        {
            List<JintItem> EmptyResult = new List<JintItem>();
            if (RightItem.Count == 0 || LeftItem.Count == 0 || List.Count == 0) return EmptyResult;

            ItemChecker IC = new ItemChecker(this);
            if (IC.DoItemsMatch(List, RightItem, false).NeutralReasons.Count == 0) return EmptyResult;
            int RightMatchStopIndex = -1;
            if (RightItem[0].State == JintState.Identifier && LeftItem[0].State == JintState.Identifier && (List[0].State == JintState.Identifier || List[0].State == JintState.MethodCallName))
            {
                if (RightItem[0].Value.Equals(List[0].Value))
                    RightMatchStopIndex++;
                else
                    return EmptyResult;
                for (int i = 1; i < RightItem.Count; i++)
                {
                    if ((RightItem[i].State == JintState.Property || RightItem[i].State == JintState.StringIndex) && (List.Count > i) &&
                        (List[i].State == JintState.Property || List[i].State == JintState.StringIndex || List[i].State == JintState.MethodCallName))
                    {
                        if (RightItem[i].Value.Equals(List[i].Value))
                            RightMatchStopIndex++;
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            else
            {
                return EmptyResult;
            }
            if (RightMatchStopIndex < RightItem.Count -1) return EmptyResult;
            List<JintItem> ResultItem = new List<JintItem>(LeftItem);
            for (int i = RightMatchStopIndex + 1; i < List.Count; i++)
            {
                if (List[i].State == JintState.MethodCallArgument)
                {
                    if (i > 0 && ResultItem.Count > 0)
                    {
                        if (ResultItem[ResultItem.Count - 1].State == JintState.Identifier || ResultItem[ResultItem.Count - 1].State == JintState.Property)
                            ResultItem[ResultItem.Count - 1].State = JintState.MethodCallName;
                    }
                }
                ResultItem.Add(List[i]);
            }
            return ResultItem;
        }

        internal bool IsInterestingString(string Value)
        {
            bool Result = false;
            Result = IsInterestingString(Value, SourceObjects);
            if (Result) return Result;
            Result = IsInterestingString(Value, SinkObjects);
            if (Result) return Result;
            Result = IsInterestingString(Value, SourceReturningMethods);
            if (Result) return Result;
            Result = IsInterestingString(Value, SinkReturningMethods);
            if (Result) return Result;
            Result = IsInterestingString(Value, ArgumentReturningMethods);
            if (Result) return Result;
            Result = IsInterestingString(Value, ArgumentAssignedToSinkMethods);
            if (Result) return Result;
            Result = IsInterestingString(Value, ArgumentAssignedASourceMethods);
            return Result;
        }

        bool IsInterestingString(string Value, List<List<JintItem>> LookUpList)
        {
            foreach (List<JintItem> Items in LookUpList)
            {
                foreach (JintItem Item in Items)
                {
                    switch(Item.State)
                    {
                        case(JintState.Identifier):
                        case (JintState.Property):
                        case (JintState.MethodCallName):
                        case (JintState.MethodName):
                            if (Item.Value.Equals(Value)) return true;
                            break;
                    }
                }
            }
            return false;
        }

        internal bool IsTimedOut()
        {
            if (AnalyzeCallStartTime == 0)
            {
                AnalyzeCallStartTime = DateTime.Now.Ticks;
            }
            else
            {
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - AnalyzeCallStartTime).TotalMilliseconds > 2000)
                {
                    throw new TimeoutException();
                }
                else if (TimeSpan.FromTicks(DateTime.Now.Ticks - AnalyzeCallStartTime).TotalMilliseconds > 1000)
                {
                    return true;
                }
            }
            return false;
        }

        internal List<string> GetSourceReasons(int LineNo)
        {
            List<string> Reasons = new List<string>();
            lock(SourceReasons)
            {
                if (SourceReasons.ContainsKey(LineNo))
                    Reasons.AddRange(SourceReasons[LineNo]);
            }
            return Reasons;
        }

        internal List<string> GetSinkReasons(int LineNo)
        {
            List<string> Reasons = new List<string>();
            lock (SinkReasons)
            {
                if (SinkReasons.ContainsKey(LineNo))
                    Reasons.AddRange(SinkReasons[LineNo]);
            }
            return Reasons;
        }

        static List<string> GetContextInLine(string Code, string Keyword)
        {
            int MatchPosition = 0;
            List<string> Contexts = new List<string>();
            if (Keyword.Length == 0) return Contexts;
            char[] CodeArray = Code.ToCharArray();
            while (MatchPosition < Code.Length && (MatchPosition = Code.IndexOf(Keyword, MatchPosition + 1)) >= 0 )
            {
                if (MatchPosition > 0)
                {
                    if (CodeArray[MatchPosition - 1] == '\'')
                        Contexts.Add("SingleQuoteStringValue");
                    else if (CodeArray[MatchPosition - 1] == '"')
                        Contexts.Add("DoubleQuoteStringValue");
                    else if (MatchPosition < Code.Length - 1)
                    {
                        if (CodeArray[MatchPosition + 1] == '\'')
                            Contexts.Add("SingleQuoteStringValue");
                        else if (CodeArray[MatchPosition + 1] == '"')
                            Contexts.Add("DoubleQuoteStringValue");
                    }
                }
            }
            return Contexts;
        }
    }
}
