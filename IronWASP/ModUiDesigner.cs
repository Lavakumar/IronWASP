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
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace IronWASP
{
    public partial class ModUiDesigner : Form
    {
        internal static IDesignerHost IDH;

        public ModUiDesigner()
        {
            InitializeComponent();
        }

        private void NewDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateDesigner(true, "");
            }
            catch(Exception Exp)
            {
                IronException.Report("Error Creating New Design", Exp);
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ModDesignSurface.SelectedObjects == null) return;
            foreach (Object O in ModDesignSurface.SelectedObjects)
            {
                try
                {
                    if (O.GetType() == typeof(Form)) continue;
                }
                catch { }
                try
                {
                    IDH.DestroyComponent(O as IComponent);
                }
                catch { }
            }
        }

        private void GenerateCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormControl = RecursivelyGetFormControl(this.BaseSplit.Panel2);
            if (FormControl == null) return;
            try
            {
                string Xml = ModUiTools.XmlFromFormControl(FormControl);
                ModCodeAndControlHolder Code = ModUiTools.XmlToCode(Xml);
                XmlOutRTB.Text = Code.XmlCode;
                PyOutRTB.Text = Code.PyCode;
                RbOutRTB.Text = Code.RbCode;
                MainTabs.SelectTab("CodeTab");
            }
            catch(Exception Exp)
            {
                IronException.Report("UI Designer Error", Exp);
            }
        }

        Form RecursivelyGetFormControl(Control ParentControl)
        {
            try
            {
                foreach (Control ChildControl in ParentControl.Controls)
                {
                    if (ChildControl.GetType() == typeof(Form)) return ChildControl as Form;
                    Control MinedControl = RecursivelyGetFormControl(ChildControl);
                    if (MinedControl != null) return MinedControl as Form;
                }
            }
            catch { }
            return null;
        }

        private void LoadDesignFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string XmlCode = "";
                OpenFileDialog XmlFileOpenDialog = new OpenFileDialog();
                XmlFileOpenDialog.Title = "Open an Design XML file";
                XmlFileOpenDialog.InitialDirectory = Config.RootDir;
                if (XmlFileOpenDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo OpenedFile = new FileInfo(XmlFileOpenDialog.FileName);
                    StreamReader Reader = new StreamReader(OpenedFile.FullName);
                    XmlCode = Reader.ReadToEnd();
                    Reader.Close();
                    CreateDesigner(false, XmlCode);
                }
            }
            catch(Exception Exp)
            {
                IronException.Report("Error Loading Design XML", Exp);
            }
        }

        void CreateDesigner(bool New, string XmlCode)
        {
            ModUi.ResetEventHandlers();
            //Create a new DesignSurface
            ModDesignSurface MDS = new ModDesignSurface();
            MDS.SetUpSelectionService();

            MDS.BeginLoad(typeof(Form));

            IDH = (IDesignerHost)MDS.GetService(typeof(IDesignerHost));
            try
            {
                IDH.RemoveService(typeof(ITypeDescriptorFilterService));
            }catch{}
            try
            {
                IDH.RemoveService(typeof(IToolboxService));
            }catch{}
            try
            {
                IDH.RemoveService(typeof(PropertyGrid));
            }catch { }
            
            //Read XML and update the Form control
            if (!New)
            {
                ModCodeAndControlHolder Code = ModUiTools.XmlToCode(XmlCode, (Form)IDH.RootComponent);
            }

            //Panel P = (Panel)IDH.CreateComponent(typeof(Panel));
            //P.Location = ModUiTools.GetLocationDefinition(20, 20);
            //P.Size = ModUiTools.GetSizeDefinition(100, 100);
            //Button B = (Button)IDH.CreateComponent(typeof(Button), "TestButton");
            //B.Text = "123";
            //P.Controls.Add(B);
            //P.Parent = (Form)IDH.RootComponent;

            try
            {
                this.BaseSplit.Panel2.Controls.RemoveAt(0);
            }
            catch { }
            Control C = MDS.View as Control;
            C.Parent = this.BaseSplit.Panel2;
            C.Dock = DockStyle.Fill;


            
            IDH.AddService(typeof(ITypeDescriptorFilterService), new CustomFilterService());

            ModToolBox TB = new ModToolBox();
            TB.Parent = this.ToolboxTab;
            TB.Dock = DockStyle.Fill;
            IDH.AddService(typeof(IToolboxService), TB);

            PropertyGrid PG = new PropertyGrid();
            PG.Parent = this.PropertiesPropertySubTab;
            PG.Dock = DockStyle.Fill;
            
            IDH.AddService(typeof(PropertyGrid), PG);

            // Use ComponentChangeService to announce changing of the 
            // Form's Controls collection */
            IComponentChangeService ICC = (IComponentChangeService)IDH.GetService(typeof(IComponentChangeService));
            ICC.OnComponentChanging(IDH.RootComponent, TypeDescriptor.GetProperties(IDH.RootComponent)["Controls"]);
        }

        private void EventHandlersGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Dictionary<string, string> EventHandlers = new Dictionary<string, string>();
            foreach (DataGridViewRow Row in EventHandlersGrid.Rows)
            {
                EventHandlers.Add(Row.Cells[0].Value.ToString(), Row.Cells[1].Value.ToString());
            }
            SetEventHandlers(EventHandlers);
        }

        void SetEventHandlers(Dictionary<string, string> EventHandlers)
        {
            if (ModDesignSurface.SelectedObjects == null || ModDesignSurface.SelectedObjects.Length != 1) return;
            object Obj = ModDesignSurface.SelectedObjects[0];
            switch (Obj.GetType().Name)
            {
                case ("Form"):
                    ModUi.EventHandlers = EventHandlers;
                    break;
                case ("ModTextBox"):
                    (Obj as ModTextBox).EventHandlers = EventHandlers;
                    break;
                case ("ModRichTextBox"):
                    (Obj as ModRichTextBox).EventHandlers = EventHandlers;
                    break;
                case ("ModButton"):
                    (Obj as ModButton).EventHandlers = EventHandlers;
                    break;
                case ("ModCheckBox"):
                    (Obj as ModCheckBox).EventHandlers = EventHandlers;
                    break;
                case ("ModRadioButton"):
                    (Obj as ModRadioButton).EventHandlers = EventHandlers;
                    break;
                case ("ModLabel"):
                    (Obj as ModLabel).EventHandlers = EventHandlers;
                    break;
                case ("ModDataGridView"):
                    (Obj as ModDataGridView).EventHandlers = EventHandlers;
                    break;
                case ("ModTabControl"):
                    (Obj as ModTabControl).EventHandlers = EventHandlers;
                    break;
            }
        }

        private void DataGridColumnAddBtn_Click(object sender, EventArgs e)
        {
            DataGridColumnAddMsgTB.Text = "";
            DataGridColumnAddMsgTB.Visible = false;
            string ColumnName = DataGridColumnNameAddTB.Text;
            if (ColumnName.Length == 0)
            {
                DataGridColumnAddMsgTB.Text = "Column name cannot be empty";
                DataGridColumnAddMsgTB.Visible = true;
                return;
            }
            try
            {
                DataGridView DV = (ModDesignSurface.SelectedObjects[0] as DataGridView);
                foreach (DataGridViewColumn Col in DV.Columns)
                {
                    if (Col.Name.Equals(ColumnName))
                    {
                        DataGridColumnAddMsgTB.Text = "Column with this name already exists. Assign a unique name.";
                        DataGridColumnAddMsgTB.Visible = true;
                        return;
                    }
                }
                if (DataGridColumnAddTBRB.Checked)
                {
                    DataGridViewTextBoxColumn C = new DataGridViewTextBoxColumn();
                    C.Name = ColumnName;
                    DV.Columns.Add(C);
                }
                else if (DataGridColumnAddCBRB.Checked)
                {
                    DataGridViewCheckBoxColumn C = new DataGridViewCheckBoxColumn();
                    C.Name = ColumnName;
                    DV.Columns.Add(C);
                }
            }
            catch
            {
                DataGridColumnAddMsgTB.Text = "Error adding column. Check input values.";
                DataGridColumnAddMsgTB.Visible = true;
                return;
            }
        }
    }
}
