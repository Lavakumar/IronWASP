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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IronWASP
{
    //Uses code and ideas from http://msdn.microsoft.com/en-us/magazine/cc163634.aspx
    internal class ModDesignSurface : DesignSurface
    {
        ISelectionService SelectionService;

        internal static object[] SelectedObjects;

        internal void SetUpSelectionService()
        {
            // Set SelectionService - SelectionChanged event handler
            SelectionService = (ISelectionService)(this.ServiceContainer.GetService(typeof(ISelectionService)));
            SelectionService.SelectionChanged += new EventHandler(SelectionService_SelectionChanged);
        }

        private void SelectionService_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionService != null)
            {
                ICollection SelectedComponents = SelectionService.GetSelectedComponents();
                PropertyGrid ModPropertyGrid = (PropertyGrid)this.GetService(typeof(PropertyGrid));
                if (ModPropertyGrid == null) return;

                object[] Comps = new object[SelectedComponents.Count];
                int i = 0;
                SelectedObjects = Comps;
                foreach (Object O in SelectedComponents)
                {
                    Comps[i] = O;
                    i++;
                }
                IronUI.UD.EventHandlersGrid.Rows.Clear();
                if (Comps.Length == 1)
                {
                    Dictionary<string, string> EventHandlers = GetEventHandlers(Comps[0]);
                    foreach (string Event in EventHandlers.Keys)
                    {
                        IronUI.UD.EventHandlersGrid.Rows.Add(new object[]{Event, EventHandlers[Event]});
                    }
                    if (Comps[0].GetType() == typeof(ModDataGridView))
                    {
                        ModPropertyGrid.Dock = DockStyle.None;
                        ModPropertyGrid.Anchor = ModUiTools.GetAnchorStyleDefinition(true, true, true, true);
                        ModPropertyGrid.Location = ModUiTools.GetLocationDefinition(0, 0);
                        ModPropertyGrid.Size = ModUiTools.GetSizeDefinition(IronUI.UD.PropertiesPropertySubTab.Size.Width, IronUI.UD.PropertiesPropertySubTab.Size.Height - (IronUI.UD.DataGridColumnAddPanel.Size.Height + 5));
                        IronUI.UD.DataGridColumnAddPanel.Visible = true;
                    }
                    else if (ModPropertyGrid.Dock == DockStyle.None)
                    {
                        ModPropertyGrid.Dock = DockStyle.Fill;
                        IronUI.UD.DataGridColumnAddPanel.Visible = false;
                    }
                }
                ModPropertyGrid.SelectedObjects = Comps;
                if (Comps.Length > 0)
                {
                    IronUI.UD.LeftTabs.SelectTab("PropertiesTab");
                }
            }
        }

        Dictionary<string, string> GetEventHandlers(object Obj)
        {
            switch(Obj.GetType().Name)
            {
                case("Form"):
                    return ModUi.EventHandlers;
                case("ModTextBox"):
                    return (Obj as ModTextBox).EventHandlers;
                case ("ModRichTextBox"):
                    return (Obj as ModRichTextBox).EventHandlers;
                case ("ModButton"):
                    return (Obj as ModButton).EventHandlers;
                case ("ModCheckBox"):
                    return (Obj as ModCheckBox).EventHandlers;
                case ("ModRadioButton"):
                    return (Obj as ModRadioButton).EventHandlers;
                case ("ModLabel"):
                    return (Obj as ModLabel).EventHandlers;
                case ("ModDataGridView"):
                    return (Obj as ModDataGridView).EventHandlers;
                default:
                    return new Dictionary<string, string>();
            }
        }
    }

    public class CustomFilterService : ITypeDescriptorFilterService
    {
        public ITypeDescriptorFilterService OldService = null;

        static Dictionary<string, List<string>> AllowedProperties = new Dictionary<string, List<string>>()
        {
            { "All", new List<string>(){"Name", "Size", "Location", "Anchor", "Dock", "Enabled", /*"Visible",*/ "BackColor", "ForeColor"}},
            { "System.Windows.Forms.Form", new List<string>(){"Icon", "Text"}},
            { "IronWASP.ModTextBox", new List<string>(){"BorderStyle", "ReadOnly", "ScrollBars", "Font", "Multiline", "PasswordChar", "WordWrap", "TextAlign", "Text"}},
            { "IronWASP.ModRichTextBox", new List<string>(){"BorderStyle", "ReadOnly", "ScrollBars", "Font", "Multiline", "WordWrap", "Text", "DetectUrls"}},
            { "IronWASP.ModLabel", new List<string>(){"BorderStyle", "Font", "Text"}},
            { "IronWASP.ModButton", new List<string>(){"Font", "Text"}},
            { "IronWASP.ModCheckBox", new List<string>(){"Font", "Text", "Checked"}},
            { "IronWASP.ModRadioButton", new List<string>(){"Font", "Text", "Checked"}},
            { "IronWASP.ModPanel", new List<string>(){"BorderStyle", "HorizontalScroll", "VerticalScroll", "BackgroundImage", "BackgroundImageLayout", "Padding", "Margin"}},
            { "IronWASP.ModTabControl", new List<string>(){"Alignment", "Appearance", "Multiline", "Padding", "TabPages"}},
            { "System.Windows.Forms.TabPage", new List<string>(){"Text"}},
            { "IronWASP.ModDataGridView", new List<string>(){
                //Allow User Actions
                "AllowUserToAddRows", "AllowUserToDeleteRows", "AllowUserToOrderColumns", "AllowUserToResizeColumns", "AllowUserToResizeRows",
                //Column Header Styling
                "ColumnHeadersVisible", "ColumnHeadersDefaultCellStyle", "ColumnHeadersBorderStyle", "ColumnHeadersHeightSizeMode",
                //Column
                "Columns", "AutoSizeColumnsMode", 
                //Row Styling
                "AlternatingRowsDefaultCellStyle", "AutoSizeRowsMode",
                //Cell
                "RowsDefaultCellStyle", "ReadOnly", "DefaultCellStyle", "CellBorderStyle",
                //Other
                "ScrollBars", "ClipboardCopyMode", "GridColor", "MultiSelect", "BackgroundColor", "BorderStyle"}},
            { "System.Windows.Forms.Design.DataGridViewColumnCollectionDialog+ListBoxItem", new List<string>(){"AutoSizeMode", "SortMode", "MinimumWidth", "Width", "ReadOnly", "Visible", "Resizable", "HeaderText", "DefaultCellStyle"}}
            
        };

        public CustomFilterService()
        {
            //DataGridView G = new DataGridView();
            //G.AdvancedCellBorderStyle
            //G.AdvancedColumnHeadersBorderStyle
            //G.AdvancedRowHeadersBorderStyle
            //G.CellBorderStyle
            //G.ColumnHeadersDefaultCellStyle
            //G.ColumnHeadersHeight
            //G.ColumnHeadersVisible
            //G.EditingControl
            //G.EditMode
            //G.EnableHeadersVisualStyles
            //G.RowHeadersWidthSizeMode
            //G.RowsDefaultCellStyle
            //G.RowTemplate
            //G.
        }

        public CustomFilterService(ITypeDescriptorFilterService OldService)
        {
            this.OldService = OldService;
        }

        public bool FilterAttributes(System.ComponentModel.IComponent component, System.Collections.IDictionary attributes)
        {
            if (OldService != null)
                OldService.FilterAttributes(component, attributes);
            return true;
        }

        public bool FilterEvents(System.ComponentModel.IComponent component, System.Collections.IDictionary events)
        {
            if (OldService != null)
                OldService.FilterEvents(component, events);
            return true;
        }

        public bool FilterProperties(System.ComponentModel.IComponent component, System.Collections.IDictionary properties)
        {
            if (OldService != null)
                OldService.FilterProperties(component, properties);

            string[] PropertyNames = new string[properties.Keys.Count];
            properties.Keys.CopyTo(PropertyNames, 0);
            string PN = string.Join(",", PropertyNames);
            List<string> AllowedPropertyNames = new List<string>(AllowedProperties["All"]);
            string ComponentType = component.GetType().Name;
            string FullComponentType = component.GetType().FullName;
            if (FullComponentType.Equals("System.Windows.Forms.Form"))
                AllowedPropertyNames.Remove("Name");
            if (AllowedProperties.ContainsKey(FullComponentType))
                AllowedPropertyNames.AddRange(AllowedProperties[FullComponentType]);
            foreach (string PropertyName in PropertyNames)
            {
                if (PropertyName.StartsWith("Name_") && !FullComponentType.Equals("System.Windows.Forms.Form")) continue;
                if (!AllowedPropertyNames.Contains(PropertyName)) properties.Remove(PropertyName);
            }
            return true;
        }
    }
}
