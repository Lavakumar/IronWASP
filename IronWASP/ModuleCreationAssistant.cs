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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace IronWASP
{
    public partial class ModuleCreationAssistant : Form
    {
        public ModuleCreationAssistant()
        {
            InitializeComponent();
        }

        string ModuleName = "";
        string ModuleDisplayName = "";
        string ModuleDescription = "";
        string ModuleAuthor = "";
        string ModuleProjectHome = "";
        string ModuleType = "Main";

        int CurrentStep = 0;
        string[] IndexNames = new string[] { "NameTab", "DetailsTab", "LanguageTab", "FinalTab" };

        private void StepOneNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep0Error("");
            string Name = ModuleNameTB.Text.Trim();
            if (Name.Length == 0)
            {
                ModuleNameTB.BackColor = Color.Red;
                ShowStep0Error("Module name cannot be empty");
                return;
            }
            if (!Regex.IsMatch(Name, "^[a-zA-Z0-9]+$"))
            {
                ModuleNameTB.BackColor = Color.Red;
                ShowStep0Error("Module Name should only contain alphabets and numbers (a-z 0-9)");
                return;
            }
            if (!Regex.IsMatch(Name[0].ToString(), "[A-Z]+"))
            {
                ShowStep0Error("Module Name should begin with an upper case letter");
                return;
            }
            if (Module.List().Contains(Name))
            {
                ModuleNameTB.BackColor = Color.Red;
                ShowStep0Error("A Module with this name already exists. Select a different name.");
                return;
            }
            string DisplayName = ModuleDisplayNameTB.Text;
            if (DisplayName.Length == 0)
            {
                ModuleDisplayNameTB.BackColor = Color.Red;
                ShowStep0Error("Module Display Name cannot be empty");
                return;
            }
            if (Module.DoesDisplayNameExist(DisplayName))
            {
                ModuleDisplayNameTB.BackColor = Color.Red;
                ShowStep0Error("A Module with this display name already exists. Select a different display name.");
                return;
            }
            string Desc = ModuleDescTB.Text;
            if (Desc.Trim().Length == 0)
            {
                ModuleDescTB.BackColor = Color.Red;
                ShowStep0Error("Module description cannot be empty");
                return;
            }
            this.ModuleName = Name;
            this.ModuleDisplayName = DisplayName;
            this.ModuleDescription = Desc;
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab("DetailsTab");
        }

        void ShowStep0Status(string Text)
        {
            this.Step0StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step0StatusTB.Visible = false;
            }
            else
            {
                this.Step0StatusTB.ForeColor = Color.Black;
                this.Step0StatusTB.Visible = true;
            }
        }
        void ShowStep0Error(string Text)
        {
            this.Step0StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step0StatusTB.Visible = false;
            }
            else
            {
                this.Step0StatusTB.ForeColor = Color.Red;
                this.Step0StatusTB.Visible = true;
            }
        }
        void ShowStep1Status(string Text)
        {
            this.Step1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step1StatusTB.Visible = false;
            }
            else
            {
                this.Step1StatusTB.ForeColor = Color.Black;
                this.Step1StatusTB.Visible = true;
            }
        }
        void ShowStep1Error(string Text)
        {
            this.Step1StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step1StatusTB.Visible = false;
            }
            else
            {
                this.Step1StatusTB.ForeColor = Color.Red;
                this.Step1StatusTB.Visible = true;
            }
        }
        void ShowStep2Status(string Text)
        {
            this.Step2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step2StatusTB.Visible = false;
            }
            else
            {
                this.Step2StatusTB.ForeColor = Color.Black;
                this.Step2StatusTB.Visible = true;
            }
        }
        void ShowStep2Error(string Text)
        {
            this.Step2StatusTB.Text = Text;
            if (Text.Length == 0)
            {
                this.Step2StatusTB.Visible = false;
            }
            else
            {
                this.Step2StatusTB.ForeColor = Color.Red;
                this.Step2StatusTB.Visible = true;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModuleNameTB_TextChanged(object sender, EventArgs e)
        {
            if (ModuleNameTB.BackColor == Color.Red) ModuleNameTB.BackColor = Color.White;
        }

        private void ModuleDisplayNameTB_TextChanged(object sender, EventArgs e)
        {
            if (ModuleDisplayNameTB.BackColor == Color.Red) ModuleDisplayNameTB.BackColor = Color.White;
        }

        private void ModuleDescTB_TextChanged(object sender, EventArgs e)
        {
            if (ModuleDescTB.BackColor == Color.Red) ModuleDescTB.BackColor = Color.White;
        }

        private void StepTwoNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep1Error("");

            ModuleAuthor = ModuleAuthorTB.Text;
            ModuleProjectHome = ModuleProjectHomeTB.Text;

            if (ModuleTypeMainRB.Checked)
            {
                ModuleType = "Main";
            }
            else
            {
                ModuleType = "Log";
            }
            this.CurrentStep = 2;
            this.BaseTabs.SelectTab("LanguageTab");            
        }

        private void StepTwoPreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 0;
            this.BaseTabs.SelectTab("NameTab");
        }

        private void StepThreePreviousBtn_Click(object sender, EventArgs e)
        {
            this.CurrentStep = 1;
            this.BaseTabs.SelectTab("DetailsTab");
        }

        private void StepThreeNextBtn_Click(object sender, EventArgs e)
        {
            ShowStep2Error("");
            try
            {
                CreateModule();
            }
            catch (Exception Exp)
            {
                ShowStep2Error(string.Format("Unable to create module - {0}", Exp.Message));
                return;
            }
            this.CurrentStep = 3;
            this.BaseTabs.SelectTab("FinalTab");
        }

        void CreateModule()
        {
            string[] ModuleCodes = CreateModuleCode();
            string PyCode = ModuleCodes[0];
            string RbCode = ModuleCodes[1];

            string ModuleCode = PyCode; ;

            string ModuleLang = "py";
            if (ModuleLangRubyRB.Checked)
            {
                ModuleCode = RbCode;
                ModuleLang = "rb";
            }
            
            string ModulesDir = string.Format("{0}\\modules\\", Config.Path);
            if (new List<string>(Directory.GetDirectories(ModulesDir)).Contains(ModuleName))
            {
                throw new Exception(string.Format("A directory with the name {0} already exists inside the modules directory. Delete it to proceed further or pick another module name.", ModuleName));
            }
            string CurrentModuleDir = string.Format("{0}{1}", ModulesDir, ModuleName);
            try
            {
                Directory.CreateDirectory(CurrentModuleDir);
            }
            catch(Exception Exp)
            {
                throw new Exception(string.Format("Unable to create a directory for the module. Reason: {0}", Exp.Message));
            }

            File.WriteAllText(string.Format("{0}\\{1}.{2}", CurrentModuleDir, ModuleName, ModuleLang), ModuleCode);
            File.WriteAllText(string.Format("{0}\\Module.xml", CurrentModuleDir), CreateModuleXml());

            ModuleFileTB.Text = CurrentModuleDir;
        }

        string CreateModuleXml()
        {
            StringBuilder SB = new StringBuilder();
            XmlWriterSettings XWS = new XmlWriterSettings();
            XWS.Indent = true;
            
            XmlWriter XW = XmlWriter.Create(SB, XWS);

            XW.WriteStartDocument();
            XW.WriteStartElement("module");
            
            XW.WriteStartElement("name"); XW.WriteValue(ModuleName); XW.WriteEndElement();
            XW.WriteStartElement("version"); XW.WriteValue("0.1"); XW.WriteEndElement();
            XW.WriteStartElement("display_name"); XW.WriteValue(ModuleDisplayName); XW.WriteEndElement();
            XW.WriteStartElement("author"); XW.WriteValue(ModuleAuthor); XW.WriteEndElement();
            XW.WriteStartElement("project_home"); XW.WriteValue(ModuleProjectHome); XW.WriteEndElement();


            XW.WriteStartElement("works_on_session"); XW.WriteValue(WorksOn("works_on_session")); XW.WriteEndElement();
            XW.WriteStartElement("works_on_url"); XW.WriteValue(WorksOn("works_on_url")); XW.WriteEndElement();
            XW.WriteStartElement("works_on_finding"); XW.WriteValue(WorksOn("works_on_finding")); XW.WriteEndElement();
            
            
            XW.WriteStartElement("description"); XW.WriteValue(ModuleProjectHome); XW.WriteEndElement();
            
            XW.WriteEndElement();
            XW.WriteEndDocument();

            XW.Close();

            return SB.ToString().Split(new string[] { "?>" }, StringSplitOptions.None)[1];
        }

        string WorksOn(string WorksOnArea)
        {
            if (ModuleType.Equals("Main"))
            {
                return "no";
            }
            if (ModuleType.Equals("Log"))
            {
                if (WorksOnArea.Equals("works_on_session"))
                {
                    return "yes";
                }
            }
            return "no";
        }

        string[] CreateModuleCode()
        {
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();

            Py.AppendLine("from IronWASP import *");
            Py.AppendLine("import re");
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("include IronWASP");
            Rb.AppendLine();
            Rb.AppendLine();


            Py.AppendLine("#Extend the Module base class");
            Py.AppendLine(string.Format("class {0}(Module):", ModuleName));
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("#Extend the RubyModule base class");
            Rb.AppendLine(string.Format("class {0} < RubyModule", ModuleName));
            Rb.AppendLine();
            Rb.Append("  "); Rb.AppendLine("@console");
            Rb.AppendLine();


            Py.Append("  "); Py.AppendLine("#Implement the GetInstance method of Module class. This method is used to create new instances of this module.");
            Py.Append("  "); Py.AppendLine("def GetInstance(self):");
            Py.Append("    "); Py.AppendLine(string.Format("m = {0}()", ModuleName));
            Py.Append("    "); Py.AppendLine(string.Format("m.Name = '{0}'", ModuleName));
            Py.Append("    "); Py.AppendLine("return m");
            Py.AppendLine();
            Py.AppendLine();

            Rb.Append("  "); Rb.AppendLine("#Implement the GetInstance method of RubyModule class. This method is used to create new instances of this module.");
            Rb.Append("  "); Rb.AppendLine("def GetInstance");
            Rb.Append("    "); Rb.AppendLine(string.Format("m = {0}.new", ModuleName));
            Rb.Append("    "); Rb.AppendLine(string.Format("m.name = '{0}'", ModuleName));
            Rb.Append("    "); Rb.AppendLine("return m");
            Rb.Append("  "); Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();

            if (ModuleType.Equals("Main"))
            {

                Py.Append("  "); Py.AppendLine("#Implement the StartModule method of Module class. This is the method called by IronWASP when user tries to launch the moduule from the UI.");
                Py.Append("  "); Py.AppendLine("def StartModule(self):");
                Py.Append("    "); Py.AppendLine("#IronConsole is a CLI window where output can be printed and user input accepted");
                Py.Append("    "); Py.AppendLine("self.console = IronConsole()");
                Py.Append("    "); Py.AppendLine(string.Format("self.console.SetTitle('{0}')", ModuleDisplayName));
                Py.Append("    "); Py.AppendLine("#Add an event handler to the close event of the console so that the module can be terminated when the user closes the console");
                Py.Append("    "); Py.AppendLine("self.console.ConsoleClosing += lambda e: self.close_console(e)");
                Py.Append("    "); Py.AppendLine("self.console.ShowConsole()");
                Py.Append("    "); Py.AppendLine("#'PrintLine' prints text at the CLI. 'Print' prints text without adding a newline at the end.");
                Py.Append("    "); Py.AppendLine(string.Format("self.console.PrintLine('[*] {0} has started')", ModuleDisplayName));
                Py.Append("    "); Py.AppendLine("self.console.Print('[*] Enter target URL: ')");
                Py.Append("    "); Py.AppendLine("#'ReadLine' accepts a single line input from the user through the CLI. 'Read' accepts multi-line input.");
                Py.Append("    "); Py.AppendLine("url = self.console.ReadLine()");
                Py.Append("    "); Py.AppendLine("self.console.PrintLine(url)");
                Py.Append("    "); Py.AppendLine("self.console.PrintLine('[*] Target scanned!')");


                Rb.Append("  "); Rb.AppendLine("#Implement the StartModule method of Module class. This is the method called by IronWASP when user tries to launch the moduule from the UI.");
                Rb.Append("  "); Rb.AppendLine("def StartModule");
                Rb.Append("    "); Rb.AppendLine("#IronConsole is a CLI window where output can be printed and user input accepted");
                Rb.Append("    "); Rb.AppendLine("@console = IronConsole.new");
                Rb.Append("    "); Rb.AppendLine(string.Format("@console.set_title('{0}')", ModuleDisplayName));
                Rb.Append("    "); Rb.AppendLine("#Add an event handler to the close event of the console so that the module can be terminated when the user closes the console");
                Rb.Append("    "); Rb.AppendLine("@console.ConsoleClosing  do |e|");
                Rb.Append("      "); Rb.AppendLine("close_console(e)");
                Rb.Append("    "); Rb.AppendLine("end");
                Rb.Append("    "); Rb.AppendLine("@console.show_console");
                Rb.Append("    "); Rb.AppendLine("#'print_line' prints text at the CLI. 'print' prints text without adding a newline at the end.");
                Rb.Append("    "); Rb.AppendLine(string.Format("@console.print_line('[*] {0} has started')", ModuleDisplayName));
                Rb.Append("    "); Rb.AppendLine("@console.print('[*] Enter target URL: ')");
                Rb.Append("    "); Rb.AppendLine("#'read_line' accepts a single line input from the user through the CLI. 'read' accepts multi-line input.");
                Rb.Append("    "); Rb.AppendLine("url = @console.read_line()");
                Rb.Append("    "); Rb.AppendLine("@console.print_line(url)");
                Rb.Append("    "); Rb.AppendLine("@console.print_line('[*] Target scanned!')");
                Rb.Append("  "); Rb.AppendLine("end");

            }
            else
            {

                Py.Append("  "); Py.AppendLine("#Implement the StartModule method of Module class. This is the method called by IronWASP when user tries to launch the moduule from the UI.");
                Py.Append("  "); Py.AppendLine("def StartModuleOnSession(self, sess):");
                Py.Append("    "); Py.AppendLine("#IronConsole is a CLI window where output can be printed and user input accepted");
                Py.Append("    "); Py.AppendLine("self.console = IronConsole()");
                Py.Append("    "); Py.AppendLine(string.Format("self.console.SetTitle('{0}')", ModuleDisplayName));
                Py.Append("    "); Py.AppendLine("#Add an event handler to the close event of the console so that the module can be terminated when the user closes the console");
                Py.Append("    "); Py.AppendLine("self.console.ConsoleClosing += lambda e: self.close_console(e)");
                Py.Append("    "); Py.AppendLine("self.console.ShowConsole()");
                Py.Append("    "); Py.AppendLine("#'Print' prints text at the CLI. 'PrintLine' prints text by adding a newline at the end.");
                Py.Append("    "); Py.AppendLine("self.console.Print('[*] Getting scan settings from user...')");
                Py.Append("    "); Py.AppendLine("f = Fuzzer.FromUi(sess.Request)");
                Py.Append("    "); Py.AppendLine("if not f.HasMore():");
                Py.Append("      "); Py.AppendLine("self.console.PrintLine('no scan settings provided. Scan cannot be proceed. Close this window.')");
                Py.Append("      "); Py.AppendLine("return");
                Py.Append("    "); Py.AppendLine(string.Format("f.SetLogSource('{0}')", ModuleName));
                
                Py.Append("    "); Py.AppendLine(@"self.console.PrintLine('done!')
    self.console.PrintLine('[*] Enter the payloads to be used for the scan below. (One payload per line)')
    #'Read' accepts multi-line input from the user through the CLI. 'ReadLine' accepts single line user input.
    payloads_input = self.console.Read()
    #We are getting the payloads list from user only to demonstarte the user input feature.
    payloads = payloads_input.split(""\r\n"")
    if len(payloads)== 0:
      payloads = ['']

    self.console.PrintLine('[*] Payloads recieved, starting the scan.')

    while f.HasMore():
      f.Next()
      for payload in payloads:
        self.console.Print('[*] Injecting payload - {0}'.format(payload))
        res = f.Inject(payload)
        self.console.PrintLine('  ==> Response code - {0}'.format(res.Code))

    self.console.PrintLine('[*] Scan completed')

");


                Rb.Append("  "); Rb.AppendLine("#Implement the StartModule method of Module class. This is the method called by IronWASP when user tries to launch the moduule from the UI.");
                Rb.Append("  "); Rb.AppendLine("def StartModuleOnSession(sess)");
                Rb.Append("    "); Rb.AppendLine("#IronConsole is a CLI window where output can be printed and user input accepted");
                Rb.Append("    "); Rb.AppendLine("@console = IronConsole.new");
                Rb.Append("    "); Rb.AppendLine(string.Format("@console.set_title('{0}')", ModuleDisplayName));
                Rb.Append("    "); Rb.AppendLine("#Add an event handler to the close event of the console so that the module can be terminated when the user closes the console");
                Rb.Append("    "); Rb.AppendLine("@console.ConsoleClosing  do |e|");
                Rb.Append("      "); Rb.AppendLine("close_console(e)");
                Rb.Append("    "); Rb.AppendLine("end");
                Rb.Append("    "); Rb.AppendLine("@console.show_console");
                Rb.Append("    "); Rb.AppendLine("#'print' prints text at the CLI. 'print_line' prints text by adding a newline at the end.");
                Rb.Append("    "); Rb.AppendLine("@console.print('[*] Getting scan settings from user...')");
                Rb.Append("    "); Rb.AppendLine("f = Fuzzer.FromUi(sess.request)");
                Rb.Append("    "); Rb.AppendLine("if not f.has_more");
                Rb.Append("      "); Rb.AppendLine("@console.print_line('no scan settings provided. Scan cannot be proceed. Close this window.')");
                Rb.Append("      "); Rb.AppendLine("return");
                Rb.Append("    "); Rb.AppendLine("end");
                Rb.Append("    "); Rb.AppendLine(string.Format("f.set_log_source('{0}')", ModuleName));
                Rb.Append("    "); Rb.AppendLine(@"@console.print_line('done!')
    @console.print_line('[*] Enter the payloads to be used for the scan below. (One payload per line)')
    #'read' accepts multi-line input from the user through the CLI. 'read_line' accepts single line user input.
    payloads_input = @console.read()
    #We are getting the payloads list from user only to demonstarte the user input feature.
    payloads = payloads_input.split(""\r\n"")
    if payloads.length == 0
      payloads = ['']
    end
    @console.print_line('[*] Payloads recieved, starting the scan.')

    while f.has_more
      f.next
      for payload in payloads
        @console.print(""[*] Injecting payload - #{payload}"")
        res = f.inject(payload)
        @console.print_line(""  ==> Response code - #{res.code}"")
      end
    end

    @console.print_line('[*] Scan completed')
");

                Rb.Append("  "); Rb.AppendLine("end");

            }

            Py.AppendLine();
            Py.AppendLine();
            Py.AppendLine();
            Py.Append("  "); Py.AppendLine("def close_console(self, e):");
            Py.Append("    "); Py.AppendLine("#This method terminates the main thread on which the module is running");
            Py.Append("    "); Py.AppendLine("self.StopModule()");
            Py.AppendLine();
            Py.AppendLine();
            Py.AppendLine();
            Py.AppendLine("#This code is executed only once when this new module is loaded in to the memory.");
            Py.AppendLine("#Create an instance of the this module");
            Py.AppendLine(string.Format("m = {0}()", this.ModuleName));

            Rb.AppendLine();
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.Append("  "); Rb.AppendLine("def close_console(e)");
            Rb.Append("    "); Rb.AppendLine("#This method terminates the main thread on which the module is running");
            Rb.Append("    "); Rb.AppendLine("stop_module");
            Rb.Append("  "); Rb.AppendLine("end");
            Rb.AppendLine("end");
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.AppendLine();
            Rb.AppendLine("#This code is executed only once when this new module is loaded in to the memory.");
            Rb.AppendLine("#Create an instance of the this module");
            Rb.AppendLine(string.Format("m = {0}.new", this.ModuleName));

            Py.AppendLine("#Call the GetInstance method on this instance which will return a new instance with all the approriate values filled in. Add this new instance to the list of Modules");
            Py.AppendLine("Module.Add(m.GetInstance())");
            Py.AppendLine();
            Py.AppendLine();

            Rb.AppendLine("#Call the get_instance method on this instance which will return a new instance with all the approriate values filled in. Add this new instance to the list of Modules");
            Rb.AppendLine("RubyModule.add(m.get_instance)");
            Rb.AppendLine();
            Rb.AppendLine();

            return new string[]{ Py.ToString(), Rb.ToString()};
        }

        private void FinalBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BaseTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!BaseTabs.SelectedTab.Name.Equals(IndexNames[this.CurrentStep]))
                BaseTabs.SelectTab(IndexNames[this.CurrentStep]);
        }

    }
}
