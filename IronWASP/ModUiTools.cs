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
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    public class ModUiTools
    {
        List<string> ControlNamesRead = new List<string>();
        List<string> ControlNamesUsed = new List<string>();
        int ControlNameCounter = 1;
        #region XmlCreation
        internal static string XmlFromFormControl(Form F)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Ui));
            //SB.Append(XmlFromControlNameProperty(F.Name));
            //properties
            SB.Append(StartTag(ModUiTags.Properties));
            SB.Append(XmlFromControlSizeProperty(F.Size));
            SB.Append(XmlFromControlTextProperty(F.Text));
            SB.Append(StartTag(ModUiTags.Icon));
            /**/
            MemoryStream MS = new MemoryStream();
            /**/
            F.Icon.Save(MS);
            /**/
            SB.Append(Convert.ToBase64String(MS.ToArray()));
            SB.Append(EndTag(ModUiTags.Icon));

            SB.Append(EndTag(ModUiTags.Properties));
            //event handlers
            SB.Append(XmlFromEventHandlers(ModUi.EventHandlers));
            //children
            SB.Append(XmlFromControlChildrenProperty(F.Controls));

            SB.Append(EndTag(ModUiTags.Ui));
            return SB.ToString();
        }

        static string XmlFromControl(Control C)
        {
            StringBuilder SB = new StringBuilder();
            switch (C.GetType().Name)
            {
                case (ModUiTags.ModTextBox):
                    SB.Append(XmlFromModTextBoxControl(C as ModTextBox));
                    break;
                case (ModUiTags.ModRichTextBox):
                    SB.Append(XmlFromModRichTextBoxControl(C as ModRichTextBox));
                    break;
                case (ModUiTags.ModLabel):
                    SB.Append(XmlFromModLabelControl(C as ModLabel));
                    break;
                case (ModUiTags.ModButton):
                    SB.Append(XmlFromModButtonControl(C as ModButton));
                    break;
                case (ModUiTags.ModCheckBox):
                    SB.Append(XmlFromModCheckBoxControl(C as ModCheckBox));
                    break;
                case (ModUiTags.ModRadioButton):
                    SB.Append(XmlFromModRadioButtonControl(C as ModRadioButton));
                    break;
                case (ModUiTags.ModDataGridView):
                    SB.Append(XmlFromModDataGridViewControl(C as ModDataGridView));
                    break;
                case (ModUiTags.ModPanel):
                    SB.Append(XmlFromModPanelControl(C as ModPanel));
                    break;
                case (ModUiTags.ModTabControl):
                    SB.Append(XmlFromModTabControl(C as ModTabControl));
                    break;
            }
            return SB.ToString();
        }
        static string XmlFromModTextBoxControl(ModTextBox C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModTextBox));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //textbox specific properties
            SB.Append(XmlFromControlBorderStyleProperty(C.BorderStyle));
            SB.Append(XmlFromControlReadOnlyProperty(C.ReadOnly));
            SB.Append(XmlFromControlScrollBarsProperty(C.ScrollBars));
            SB.Append(XmlFromControlFontProperty(C.Font));
            SB.Append(XmlFromControlMultiLineProperty(C.Multiline));
            SB.Append(XmlFromControlPasswordCharProperty(C.PasswordChar));
            SB.Append(XmlFromControlWordWrapProperty(C.WordWrap));
            SB.Append(XmlFromControlTextAlignProperty(C.TextAlign));
            SB.Append(XmlFromControlTextProperty(C.Text));
            SB.Append(EndTag(ModUiTags.Properties));

            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModTextBox));

            return SB.ToString();
        }
        static string XmlFromModRichTextBoxControl(ModRichTextBox C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModRichTextBox));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //textbox specific properties
            SB.Append(XmlFromControlBorderStyleProperty(C.BorderStyle));
            SB.Append(XmlFromControlReadOnlyProperty(C.ReadOnly));
            SB.Append(XmlFromControlScrollBarsProperty(C.ScrollBars));
            SB.Append(XmlFromControlFontProperty(C.Font));
            SB.Append(XmlFromControlMultiLineProperty(C.Multiline));
            SB.Append(XmlFromControlWordWrapProperty(C.WordWrap));
            SB.Append(XmlFromControlTextProperty(C.Text));
            //richtextbox specific properties
            SB.Append(XmlFromControlDetectUrlsProperty(C.DetectUrls));
            SB.Append(XmlFromControlRichTextProperty(C.Rtf));
            SB.Append(EndTag(ModUiTags.Properties));

            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModRichTextBox));

            return SB.ToString();
        }
        static string XmlFromModLabelControl(ModLabel C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModLabel));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //label specific properties
            SB.Append(XmlFromControlBorderStyleProperty(C.BorderStyle));
            SB.Append(XmlFromControlFontProperty(C.Font));
            SB.Append(XmlFromControlTextProperty(C.Text));
            SB.Append(EndTag(ModUiTags.Properties));

            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModLabel));

            return SB.ToString();
        }
        static string XmlFromModButtonControl(ModButton C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModButton));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //button specific properties
            SB.Append(XmlFromControlFontProperty(C.Font));
            SB.Append(XmlFromControlTextProperty(C.Text));
            SB.Append(EndTag(ModUiTags.Properties));

            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModButton));

            return SB.ToString();
        }
        static string XmlFromModCheckBoxControl(ModCheckBox C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModCheckBox));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //checkbox specific properties
            SB.Append(XmlFromControlFontProperty(C.Font));
            SB.Append(XmlFromControlTextProperty(C.Text));
            SB.Append(XmlFromControlCheckedProperty(C.Checked));
            SB.Append(EndTag(ModUiTags.Properties));

            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModCheckBox));

            return SB.ToString();
        }
        static string XmlFromModRadioButtonControl(ModRadioButton C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModRadioButton));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //textbox specific properties
            SB.Append(XmlFromControlFontProperty(C.Font));
            SB.Append(XmlFromControlTextProperty(C.Text));
            SB.Append(XmlFromControlCheckedProperty(C.Checked));
            SB.Append(EndTag(ModUiTags.Properties));

            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModRadioButton));

            return SB.ToString();
        }
        static string XmlFromModDataGridViewControl(ModDataGridView C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModDataGridView));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //textbox specific properties
            SB.Append(XmlFromAllowUserToAddRowsProperty(C.AllowUserToAddRows));
            SB.Append(XmlFromAllowUserToDeleteRowsProperty(C.AllowUserToDeleteRows));
            SB.Append(XmlFromAllowUserToOrderColumnsProperty(C.AllowUserToOrderColumns));
            SB.Append(XmlFromAllowUserToResizeColumnsProperty(C.AllowUserToResizeColumns));
            SB.Append(XmlFromAllowUserToResizeRowsProperty(C.AllowUserToResizeRows));
            SB.Append(XmlFromColumnHeadersVisibleProperty(C.ColumnHeadersVisible));
            SB.Append(XmlFromRowHeadersVisibleProperty(C.RowHeadersVisible));
            //SB.Append(XmlFromColumnHeadersDefaultCellStyleProperty(C.ColumnHeadersDefaultCellStyle));
            SB.Append(XmlFromColumnHeadersHeightSizeModeProperty(C.ColumnHeadersHeightSizeMode));
            SB.Append(XmlFromControlSelectionModeProperty(C.SelectionMode));
            SB.Append(XmlFromControlGridColorProperty(C.GridColor));
            SB.Append(XmlFromColumnsProperty(C.Columns));
            SB.Append(EndTag(ModUiTags.Properties));
            
            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModDataGridView));

            return SB.ToString();
        }
        static string XmlFromModPanelControl(ModPanel C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModPanel));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            SB.Append(EndTag(ModUiTags.Properties));
            //child controls
            SB.Append(XmlFromControlChildrenProperty(C.Controls));
            SB.Append(XmlFromEventHandlers(C.EventHandlers));

            SB.Append(EndTag(ModUiTags.ModPanel));

            return SB.ToString();
        }
        static string XmlFromModTabControl(ModTabControl C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.ModTabControl));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            SB.Append(XmlFromBasicProperties(C));
            //tab pages
            SB.Append(StartTag(ModUiTags.TabPages));
            foreach (TabPage T in C.TabPages)
            {
                SB.Append(XmlFromTabPageControl(T));
            }
            SB.Append(EndTag(ModUiTags.TabPages));
            SB.Append(EndTag(ModUiTags.Properties));
            //child controls
            SB.Append(XmlFromControlChildrenProperty(C.Controls));
            SB.Append(XmlFromEventHandlers(C.EventHandlers));
            
            SB.Append(EndTag(ModUiTags.ModTabControl));

            return SB.ToString();
        }
        static string XmlFromTabPageControl(TabPage C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.TabPage));
            SB.Append(XmlFromControlNameProperty(C));
            SB.Append(StartTag(ModUiTags.Properties));
            //basic properties
            //SB.Append(XmlFromBasicProperties(C));
            //tabpage properties
            SB.Append(XmlFromTagValue(ModUiTags.Text, C.Text));
            SB.Append(EndTag(ModUiTags.Properties));
            //child controls
            SB.Append(XmlFromControlChildrenProperty(C.Controls));
            //SB.Append(XmlFromEventHandlers(new Dictionary<string, string>{}));
            SB.Append(EndTag(ModUiTags.TabPage));

            return SB.ToString();
        }

        static string XmlFromBasicProperties(Control C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(XmlFromControlSizeProperty(C.Size));
            SB.Append(XmlFromControlLocationProperty(C));
            SB.Append(XmlFromControlAnchorProperty(C));
            SB.Append(XmlFromControlDockProperty(C));
            SB.Append(XmlFromControlEnabledProperty(C));
            //SB.Append(XmlFromControlVisibleProperty(C));
            if (C.GetType() == typeof(ModDataGridView))
                SB.Append(XmlFromControlBackgroundColorProperty((C as DataGridView).BackgroundColor));
            else
                SB.Append(XmlFromControlBackColorProperty(C.BackColor));
            SB.Append(XmlFromControlForeColorProperty(C.ForeColor));
            return SB.ToString();
        }
        static string XmlFromControlNameProperty(Control C)
        {
            string Name = "";
            if(C.Name.Equals(C.Site.Name))
            {
                Name = C.Name;
            }
            else
            {
                if(C.Site.Name.Length > 0)
                    Name = C.Site.Name;
                else
                    Name = C.Name;
            }
            return XmlFromControlNameProperty(Name);
        }
        static string XmlFromControlNameProperty(String Name)
        {
            return XmlFromTagValue(ModUiTags.Name, Name);
        }
        static string XmlFromControlSizeProperty(Size S)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Size));
            SB.Append(XmlFromTagValue(ModUiTags.SizeWidth, S.Width));
            SB.Append(XmlFromTagValue(ModUiTags.SizeHeight, S.Height));
            SB.Append(EndTag(ModUiTags.Size));
            return SB.ToString();
        }
        static string XmlFromControlLocationProperty(Control C)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Location));
            SB.Append(XmlFromTagValue(ModUiTags.LocationX, C.Location.X));
            SB.Append(XmlFromTagValue(ModUiTags.LocationY, C.Location.Y));
            SB.Append(EndTag(ModUiTags.Location));
            return SB.ToString();
        }
        static string XmlFromControlEnabledProperty(Control C)
        {
            return XmlFromTagValue(ModUiTags.Enabled, C.Enabled);
        }
        static string XmlFromControlVisibleProperty(Control C)
        {
            return XmlFromTagValue(ModUiTags.Visible, C.Visible);
        }
        static string XmlFromControlReadOnlyProperty(bool ReadOnly)
        {
            return XmlFromTagValue(ModUiTags.ReadOnly, ReadOnly);
        }
        static string XmlFromControlAnchorProperty(Control C)
        {
            List<string> RawAnchors = new List<string>(C.Anchor.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            List<string> Anchors = new List<string>();
            foreach (string Anchor in RawAnchors)
            {
                Anchors.Add(Anchor.Trim());
            }
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Anchor));
            SB.Append(XmlFromTagValue(ModUiTags.AnchorTop, Anchors.Contains("Top")));
            SB.Append(XmlFromTagValue(ModUiTags.AnchorBottom, Anchors.Contains("Bottom")));
            SB.Append(XmlFromTagValue(ModUiTags.AnchorLeft, Anchors.Contains("Left")));
            SB.Append(XmlFromTagValue(ModUiTags.AnchorRight, Anchors.Contains("Right")));
            SB.Append(EndTag(ModUiTags.Anchor));
            return SB.ToString();
        }
        static string XmlFromControlDockProperty(Control C)
        {
            string DockStyleName = C.Dock.ToString();
            return XmlFromTagValue(ModUiTags.Dock, DockStyleName);
        }
        static string XmlFromControlFontProperty(Font F)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Font));
            SB.Append(XmlFromTagValue(ModUiTags.FontBold, F.Bold));
            SB.Append(XmlFromTagValue(ModUiTags.FontFamily, F.FontFamily.Name));
            SB.Append(XmlFromTagValue(ModUiTags.FontItalic, F.Italic));
            SB.Append(XmlFromTagValue(ModUiTags.FontSize, F.Size));
            SB.Append(XmlFromTagValue(ModUiTags.FontStrikeout, F.Strikeout));
            SB.Append(XmlFromTagValue(ModUiTags.FontUnderline, F.Underline));
            SB.Append(EndTag(ModUiTags.Font));
            return SB.ToString();
        }
        static string XmlFromControlBorderStyleProperty(BorderStyle BS)
        {
            return XmlFromTagValue(ModUiTags.BorderStyle, BS);
        }
        static string XmlFromControlTextProperty(string Text)
        {
            return XmlFromTagValue(ModUiTags.Text, Tools.Base64Encode(Text));
        }
        static string XmlFromControlRichTextProperty(string RichText)
        {
            return XmlFromTagValue(ModUiTags.Rtf, Tools.Base64Encode(RichText));
        }
        static string XmlFromControlBackColorProperty(Color C)
        {
            return XmlFromTagValue(ModUiTags.BackColor, C.ToArgb());
        }
        static string XmlFromControlForeColorProperty(Color C)
        {
            return XmlFromTagValue(ModUiTags.ForeColor, C.ToArgb());
        }
        static string XmlFromControlPasswordCharProperty(Char PasswordChar)
        {
            if (PasswordChar == '\0')
                return XmlFromTagValue(ModUiTags.PasswordChar, "");
            else
                return XmlFromTagValue(ModUiTags.PasswordChar, PasswordChar);
        }
        static string XmlFromControlMultiLineProperty(bool MultiLine)
        {
            return XmlFromTagValue(ModUiTags.MultiLine, MultiLine);
        }
        static string XmlFromControlWordWrapProperty(bool WordWrap)
        {
            return XmlFromTagValue(ModUiTags.Wordwrap, WordWrap);
        }
        static string XmlFromControlTextAlignProperty(HorizontalAlignment TextAlign)
        {
            return XmlFromTagValue(ModUiTags.TextAlign, TextAlign);
        }
        static string XmlFromControlScrollBarsProperty(ScrollBars ScBars)
        {
            return XmlFromTagValue(ModUiTags.ScrollBars, ScBars);
        }
        static string XmlFromControlScrollBarsProperty(RichTextBoxScrollBars ScBars)
        {
            return XmlFromTagValue(ModUiTags.RichTextScrollBars, ScBars);
        }
        static string XmlFromControlDetectUrlsProperty(bool DetectUrls)
        {
            return XmlFromTagValue(ModUiTags.DetectUrls, DetectUrls);
        }
        static string XmlFromControlCheckedProperty(bool Checked)
        {
            return XmlFromTagValue(ModUiTags.Checked, Checked);
        }
        static string XmlFromAllowUserToAddRowsProperty(bool Allow)
        {
            return XmlFromTagValue(ModUiTags.AllowUserToAddRows, Allow);
        }
        static string XmlFromAllowUserToDeleteRowsProperty(bool Allow)
        {
            return XmlFromTagValue(ModUiTags.AllowUserToDeleteRows, Allow);
        }
        static string XmlFromAllowUserToOrderColumnsProperty(bool Allow)
        {
            return XmlFromTagValue(ModUiTags.AllowUserToOrderColumns, Allow);
        }
        static string XmlFromAllowUserToResizeColumnsProperty(bool Allow)
        {
            return XmlFromTagValue(ModUiTags.AllowUserToResizeColumns, Allow);
        }
        static string XmlFromAllowUserToResizeRowsProperty(bool Allow)
        {
            return XmlFromTagValue(ModUiTags.AllowUserToResizeRows, Allow);
        }
        static string XmlFromColumnHeadersVisibleProperty(bool Visible)
        {
            return XmlFromTagValue(ModUiTags.ColumnHeadersVisible, Visible);
        }
        static string XmlFromRowHeadersVisibleProperty(bool Visible)
        {
            return XmlFromTagValue(ModUiTags.RowHeadersVisible, Visible);
        }
        static string XmlFromColumnHeadersDefaultCellStyleProperty(DataGridViewCellStyle CellStyle)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.DefaultCellStyle));
            SB.Append(XmlFromDataGridViewCellStyle(CellStyle));
            SB.Append(EndTag(ModUiTags.DefaultCellStyle));
            return SB.ToString();
        }
        static string XmlFromDataGridViewCellStyle(DataGridViewCellStyle CellStyle)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.DefaultCellStyle));
            SB.Append(XmlFromTagValue(ModUiTags.CellStyleAlignment, CellStyle.Alignment.ToString()));
            SB.Append(StartTag(ModUiTags.Font));
            SB.Append(XmlFromControlFontProperty(CellStyle.Font));
            SB.Append(EndTag(ModUiTags.Font));
            SB.Append(XmlFromTagValue(ModUiTags.BackColor, CellStyle.BackColor));
            SB.Append(XmlFromTagValue(ModUiTags.ForeColor, CellStyle.ForeColor));
            SB.Append(XmlFromTagValue(ModUiTags.CellStyleWrapMode, CellStyle.WrapMode));
            SB.Append(StartTag(ModUiTags.DefaultCellStyle));
            return SB.ToString();
        }
        static string XmlFromColumnHeadersHeightSizeModeProperty(DataGridViewColumnHeadersHeightSizeMode SizeMode)
        {
            return XmlFromTagValue(ModUiTags.ColumnHeadersHeightSizeMode, SizeMode);
        }
        
        static string XmlFromControlGridColorProperty(Color C)
        {
            return XmlFromTagValue(ModUiTags.GridColor, C.ToArgb());
        }
        static string XmlFromControlBackgroundColorProperty(Color C)
        {
            return XmlFromTagValue(ModUiTags.BackgroundColor, C.ToArgb());
        }
        static string XmlFromControlSelectionModeProperty(DataGridViewSelectionMode SelectionMode)
        {
            return XmlFromTagValue(ModUiTags.SelectionMode, SelectionMode);
        }
        static string XmlFromColumnsProperty(DataGridViewColumnCollection Columns)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Columns));
            foreach (DataGridViewColumn Column in Columns)
            {
                SB.Append(XmlFromColumnProperty(Column));
            }
            SB.Append(EndTag(ModUiTags.Columns));
            return SB.ToString();
        }
        static string XmlFromColumnProperty(DataGridViewColumn Column)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Column));
            SB.Append(XmlFromControlNameProperty(Column.Name));
            SB.Append(XmlFromTagValue(ModUiTags.CellType, Column.CellType.Name));
            SB.Append(XmlFromTagValue(ModUiTags.AutoSizeColumnsMode, Column.AutoSizeMode));
            SB.Append(XmlFromTagValue(ModUiTags.FillWeight, Column.FillWeight));
            SB.Append(XmlFromTagValue(ModUiTags.MinimumWidth, Column.MinimumWidth));
            SB.Append(XmlFromTagValue(ModUiTags.Width, Column.Width));
            SB.Append(XmlFromControlReadOnlyProperty(Column.ReadOnly));
            //SB.Append(XmlFromDataGridViewCellStyle(Column.DefaultCellStyle));
            //SB.Append(XmlFromControlFillWeightProperty(Column.FillWeight));
            SB.Append(XmlFromControlHeaderTextProperty(Column.HeaderText));
            SB.Append(EndTag(ModUiTags.Column));
            return SB.ToString();
        }
        static string XmlFromControlWrapModeProperty(DataGridViewTriState WrapMode)
        {
            return XmlFromTagValue(ModUiTags.CellStyleWrapMode, WrapMode);
        }
        static string XmlFromControlHeaderTextProperty(string HeaderText)
        {
            return XmlFromTagValue(ModUiTags.HeaderText, Tools.Base64Encode(HeaderText));
        }
        static string XmlFromControlChildrenProperty(Control.ControlCollection Children)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.Children));
            if (Children != null)
            {
                foreach (Control ChildControl in Children)
                {
                    SB.Append(XmlFromControl(ChildControl));
                }
            }
            SB.Append(EndTag(ModUiTags.Children));
            return SB.ToString();
        }
        static string XmlFromEventHandlers(Dictionary<string, string> EventHandlers)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(ModUiTags.EventHandlers));
            foreach (string Event in EventHandlers.Keys)
            {
                SB.Append(StartTag(Event));
                SB.Append(EventHandlers[Event]);
                SB.Append(EndTag(Event));
            }
            SB.Append(EndTag(ModUiTags.EventHandlers));
            return SB.ToString();
        }
        static string XmlFromTagValue(string TagName, object Value)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append(StartTag(TagName)); SB.Append(Tools.XmlEncode(Value.ToString())); SB.Append(EndTag(TagName));
            return SB.ToString();
        }
        static string StartTag(string TagName)
        {
            return String.Format("<{0}>", TagName);
        }
        static string EndTag(string TagName)
        {
            return String.Format("</{0}>", TagName);
        }
        #endregion

        #region CodeCreation
        public static ModCodeAndControlHolder XmlToCode(string UiXml)
        {
            return XmlToCode(UiXml, new Form());
        }
        public static ModCodeAndControlHolder XmlToCode(string UiXml, Form F)
        {
            ModUiTools MUT = new ModUiTools();
            return MUT.RawXmlToCode(UiXml, F);
        }
        ModCodeAndControlHolder RawXmlToCode(string UiXml, Form F)
        {
            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            XmlDocument XDoc = new XmlDocument();
            XDoc.LoadXml(UiXml);
            MemoryStream MS = new MemoryStream();
            XmlTextWriter XWriter = new XmlTextWriter(MS, Encoding.ASCII);
            XWriter.Formatting = Formatting.Indented;
            XDoc.WriteContentTo(XWriter);
            XWriter.Flush();
            MS.Flush();
            MS.Position = 0;
            StreamReader SR = new StreamReader(MS);
            Result.XmlCode = SR.ReadToEnd();
            ModCodeAndControlHolder PyRbControlResult = XmlToCode(XDoc, F);
            Result.PyCode = PyRbControlResult.PyCode;
            Result.RbCode = PyRbControlResult.RbCode;
            Result.Control = PyRbControlResult.Control;
            return Result;
        }
        ModCodeAndControlHolder XmlToCode(XmlDocument XDoc, Form F)
        {
            XmlNode UiTagNode = XDoc.ChildNodes[0];
            ReadAllControlNames(UiTagNode);
            StringBuilder PyCode = new StringBuilder();
            StringBuilder RbCode = new StringBuilder();
            PyCode.AppendLine("ui = ModUi()");
            RbCode.AppendLine("ui = ModUi.new()");

            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            if (XDoc.DocumentElement.Name.Equals("UI", StringComparison.OrdinalIgnoreCase))
            {
                ModCodeAndControlHolder CPECC = ConvertControlPropertiesEventHandlersChildrenToCode(UiTagNode, "ui", F);
                PyCode.Append(CPECC.PyCode);
                PyCode.AppendLine("ui.ShowUi()");
                RbCode.Append(CPECC.RbCode);
                RbCode.AppendLine("ui.ShowUi()");
            }
            else
            {
                throw new Exception("Invalid UI description. Outermost tag must be the <ui> tag.");
            }
            Result.PyCode = PyCode.ToString();
            Result.RbCode = RbCode.ToString();
            Result.Control = F;
            return Result;
        }
        void ReadAllControlNames(XmlNode Node)
        {
            foreach (XmlNode ChildNode in Node.ChildNodes)
            {
                if (ChildNode.Name.Equals(ModUiTags.Name))
                {
                    if (!ControlNamesRead.Contains(ChildNode.InnerText))
                        ControlNamesRead.Add(ChildNode.InnerText);
                }
                else if (ChildNode.Equals(ModUiTags.Children))
                    ReadAllControlNames(ChildNode);
            }
        }
        string GetControlName(string ControlType, string CurrentName)
        {
            if (CurrentName.Length > 0 && !ControlNamesUsed.Contains(CurrentName))
            {
                ControlNamesUsed.Add(CurrentName);
                return CurrentName;
            }
            else
            {
                while (true)
                {
                    string Name = String.Format("{0}_{1}", Tools.CamelCaseToUnderScore(ControlType), ControlNameCounter);
                    if (ControlNamesRead.Contains(Name) || ControlNamesUsed.Contains(Name))
                    {
                        ControlNameCounter++;
                    }
                    else
                    {
                        ControlNamesUsed.Add(Name);
                        return Name;
                    }
                }
            }
        }
        ModCodeAndControlHolder ConvertControlPropertiesEventHandlersChildrenToCode(XmlNode Node, string ControlName, Control C)
        {
            XmlNode PropertiesNode = null;
            XmlNode EventHandlersNode = null;
            XmlNode ChildrenNode = null;
            foreach (XmlNode SubNode in Node.ChildNodes)
            {
                switch (SubNode.Name)
                {
                    case (ModUiTags.Properties):
                        PropertiesNode = SubNode;
                        break;
                    case (ModUiTags.EventHandlers):
                        EventHandlersNode = SubNode;
                        break;
                    case (ModUiTags.Children):
                        ChildrenNode = SubNode;
                        break;
                    default:
                        continue;
                }
            }
            StringBuilder PyCode = new StringBuilder();
            StringBuilder RbCode = new StringBuilder();
            ModCodeAndControlHolder NodeResults = new ModCodeAndControlHolder();
            if (PropertiesNode != null)
            {
                NodeResults = ConvertControlPropertiesNodeToCode(PropertiesNode, ControlName, C);
                PyCode.Append(NodeResults.PyCode);
                RbCode.Append(NodeResults.RbCode);
            }
            if (EventHandlersNode != null)
            {
                NodeResults = ConvertControlEventHandlersNodeToCode(EventHandlersNode, ControlName, C);
                PyCode.Append(NodeResults.PyCode);
                RbCode.Append(NodeResults.RbCode);
            }
            if (ChildrenNode != null)
            {
                NodeResults = ConvertControlChildXmlNodesToCode(ChildrenNode, ControlName, C);
                PyCode.Append(NodeResults.PyCode);
                RbCode.Append(NodeResults.RbCode);
            }
            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            Result.PyCode = PyCode.ToString();
            Result.RbCode = RbCode.ToString();
            Result.Control = C;
            return Result;
        }
        ModCodeAndControlHolder ConvertControlPropertiesNodeToCode(XmlNode PropertiesNode, string ControlName, Control C)
        {
            StringBuilder PyCode = new StringBuilder();
            StringBuilder RbCode = new StringBuilder();
            foreach (XmlNode ChildNode in PropertiesNode.ChildNodes)
            {
                ModCodeAndControlHolder PropertyCode = ConvertControlPropertyNodeToCode(ChildNode, ControlName, C);
                PyCode.Append(PropertyCode.PyCode);
                RbCode.Append(PropertyCode.RbCode);
            }
            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            Result.PyCode = PyCode.ToString();
            Result.RbCode = RbCode.ToString();
            Result.Control = C;
            return Result;
        }
        ModCodeAndControlHolder ConvertControlPropertyNodeToCode(XmlNode PropertyNode, string ControlName, Control C)
        {
            StringBuilder PyCode = new StringBuilder();
            StringBuilder RbCode = new StringBuilder();
            switch (PropertyNode.Name)
            {
                case (ModUiTags.Size):
                    SetSizeInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Location):
                    SetLocationInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Anchor):
                    SetAnchorInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Dock):
                    SetDockStyleInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Enabled):
                    SetEnabledInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Visible):
                    SetVisibleInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.BackColor):
                    SetBackColorInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.ForeColor):
                    SetForeColorInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Font):
                    SetFontInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.BorderStyle):
                    SetBorderStyleInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Icon):
                    SetIconInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.ReadOnly):
                    SetReadOnlyInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Text):
                    SetTextInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.PasswordChar):
                    SetPasswordCharInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.MultiLine):
                    SetMultiLineInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Wordwrap):
                    SetWordwrapInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.TextAlign):
                    SetTextAlignInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.ScrollBars):
                    SetScrollBarsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                //case (ModUiTags.Rtf):
                //    SetRtfInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                //    break;
                case (ModUiTags.RichTextScrollBars):
                    SetRichTextScrollBarsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.DetectUrls):
                    SetDetectUrlsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Checked):
                    SetCheckedInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.Columns):
                    SetColumnsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.AllowUserToAddRows):
                    SetAllowUserToAddRowsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.AllowUserToDeleteRows):
                    SetAllowUserToDeleteRowsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.AllowUserToResizeColumns):
                    SetAllowUserToResizeColumnsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.AllowUserToResizeRows):
                    SetAllowUserToResizeRowsInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.ColumnHeadersVisible):
                    SetColumnHeadersVisibleInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.RowHeadersVisible):
                    SetRowHeadersVisibleInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.GridColor):
                    SetGridColorInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.BackgroundColor):
                    SetBackgroundColorInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
                case (ModUiTags.TabPages):
                    SetTabPagesInCode(PyCode, RbCode, ControlName, C, PropertyNode);
                    break;
            }
            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            Result.PyCode = PyCode.ToString();
            Result.RbCode = RbCode.ToString();
            Result.Control = C;
            return Result;
        }

        #region SetPropertiesInCode
        static void SetNameInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C)
        {
            PyCode.AppendLine(string.Format("{0}.Name = '{1}'", ControlName, ControlName));
            RbCode.AppendLine(string.Format("{0}.Name = '{1}'", ControlName, ControlName));
            C.Name = ControlName;
        }
        static void SetSizeInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            if (Node.HasChildNodes && Node.ChildNodes.Count == 2)
            {
                int Width = 0;
                int Height = 0;
                foreach (XmlNode SubNode in Node.ChildNodes)
                {
                    if (SubNode.Name.Equals(ModUiTags.SizeWidth))
                        Width = Int32.Parse(SubNode.InnerText);
                    else if (SubNode.Name.Equals(ModUiTags.SizeHeight))
                        Height = Int32.Parse(SubNode.InnerText);
                    else
                        throw new Exception(string.Format("Invalid Size value {0}", SubNode.Name));
                }
                PyCode.AppendLine(string.Format("{0}.Size = ModUiTools.GetSizeDefinition({1},{2})", ControlName, Width, Height));
                RbCode.AppendLine(string.Format("{0}.Size = ModUiTools.get_size_definition({1},{2})", ControlName, Width, Height));
                C.Size = ModUiTools.GetSizeDefinition(Width, Height);
            }
        }
        static void SetLocationInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            if (Node.HasChildNodes && Node.ChildNodes.Count == 2)
            {
                int X = 0;
                int Y = 0;
                foreach (XmlNode SubNode in Node.ChildNodes)
                {
                    if (SubNode.Name.Equals(ModUiTags.LocationX))
                        X = Int32.Parse(SubNode.InnerText);
                    else if (SubNode.Name.Equals(ModUiTags.LocationY))
                        Y = Int32.Parse(SubNode.InnerText);
                    else
                        throw new Exception(string.Format("Invalid Location value {0}", SubNode.Name));
                }
                PyCode.AppendLine(string.Format("{0}.Location = ModUiTools.GetLocationDefinition({1},{2})", ControlName, X, Y));
                RbCode.AppendLine(string.Format("{0}.Location = ModUiTools.get_location_definition({1},{2})", ControlName, X, Y));
                C.Location = ModUiTools.GetLocationDefinition(X, Y);
            }
        }
        static void SetAnchorInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            if (Node.HasChildNodes && Node.ChildNodes.Count == 4)
            {
                bool Top = true;
                bool Bottom = false;
                bool Left = true;
                bool Right = false;
                foreach (XmlNode SubNode in Node.ChildNodes)
                {
                    if (SubNode.Name.Equals(ModUiTags.AnchorLeft))
                        Left = SubNode.InnerText.Equals("True");
                    else if (SubNode.Name.Equals(ModUiTags.AnchorRight))
                        Right = SubNode.InnerText.Equals("True");
                    else if (SubNode.Name.Equals(ModUiTags.AnchorTop))
                        Top = SubNode.InnerText.Equals("True");
                    else if (SubNode.Name.Equals(ModUiTags.AnchorBottom))
                        Bottom = SubNode.InnerText.Equals("True");
                    else
                        throw new Exception(string.Format("Invalid Anchor value {0}", SubNode.Name));
                }
                PyCode.AppendLine(string.Format("{0}.Anchor = ModUiTools.GetAnchorStyleDefinition({1},{2},{3},{4})", ControlName, PyBool(Top), PyBool(Bottom), PyBool(Left), PyBool(Right)));
                RbCode.AppendLine(string.Format("{0}.Anchor = ModUiTools.get_anchor_style_definition({1},{2},{3},{4})", ControlName, RbBool(Top), RbBool(Bottom), RbBool(Left), RbBool(Right)));
                C.Anchor = ModUiTools.GetAnchorStyleDefinition(Top, Bottom, Left, Right);
            }
        }
        static void SetDockStyleInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string DockStyleName = Node.InnerText;
            PyCode.AppendLine(string.Format("{0}.Dock = ModUiTools.GetDockStyleDefinition('{1}')", ControlName, DockStyleName));
            RbCode.AppendLine(string.Format("{0}.Dock = ModUiTools.get_dock_style_definition('{1}')", ControlName, DockStyleName));
            C.Dock = ModUiTools.GetDockStyleDefinition(DockStyleName);
        }
        static void SetEnabledInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Enabled = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.Enabled = {1}", ControlName, PyBool(Enabled)));
            RbCode.AppendLine(string.Format("{0}.Enabled = {1}", ControlName, RbBool(Enabled)));
            C.Enabled = Enabled;
        }
        static void SetVisibleInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Visible = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.Visible = {1}", ControlName, PyBool(Visible)));
            RbCode.AppendLine(string.Format("{0}.Visible = {1}", ControlName, RbBool(Visible)));
            C.Visible = Visible;
        }
        static void SetBackColorInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            int ColorCode = Int32.Parse(Node.InnerText);
            PyCode.AppendLine(string.Format("{0}.BackColor = ModUiTools.GetColorDefinition({1})", ControlName, ColorCode));
            RbCode.AppendLine(string.Format("{0}.BackColor = ModUiTools.get_color_definition({1})", ControlName, ColorCode));
            C.BackColor = ModUiTools.GetColorDefinition(ColorCode);
        }
        static void SetForeColorInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            int ColorCode = Int32.Parse(Node.InnerText);
            PyCode.AppendLine(string.Format("{0}.ForeColor = ModUiTools.GetColorDefinition({1})", ControlName, ColorCode));
            RbCode.AppendLine(string.Format("{0}.ForeColor = ModUiTools.get_color_definition({1})", ControlName, ColorCode));
            C.ForeColor = ModUiTools.GetColorDefinition(ColorCode);
        }
        static void SetFontInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            if (Node.HasChildNodes && Node.ChildNodes.Count == 6)
            {
                string Family = "Arial";
                bool Bold = false;
                bool Italic = false;
                float Size = 0;
                bool Strikeout = false;
                bool Underline = false;
                foreach (XmlNode SubNode in Node.ChildNodes)
                {
                    if (SubNode.Name.Equals(ModUiTags.FontBold))
                        Bold = SubNode.InnerText.Equals("True");
                    else if (SubNode.Name.Equals(ModUiTags.FontItalic))
                        Italic = SubNode.InnerText.Equals("True");
                    else if (SubNode.Name.Equals(ModUiTags.FontStrikeout))
                        Strikeout = SubNode.InnerText.Equals("True");
                    else if (SubNode.Name.Equals(ModUiTags.FontUnderline))
                        Underline = SubNode.InnerText.Equals("True");
                    else if (SubNode.Name.Equals(ModUiTags.FontFamily))
                        Family = SubNode.InnerText;
                    else if (SubNode.Name.Equals(ModUiTags.FontSize))
                        Size = float.Parse(SubNode.InnerText);
                    else
                        throw new Exception(string.Format("Invalid Font value {0}", SubNode.Name));
                }
                PyCode.AppendLine(string.Format("{0}.Font = ModUiTools.GetFontDefinition('{1}',{2},{3},{4},{5},{6})", ControlName, Family, Size, PyBool(Bold), PyBool(Italic), PyBool(Strikeout), PyBool(Underline)));
                RbCode.AppendLine(string.Format("{0}.Font = ModUiTools.get_font_definition('{1}',{2},{3},{4},{5},{6})", ControlName, Family, Size, RbBool(Bold), RbBool(Italic), RbBool(Strikeout), RbBool(Underline)));
                C.Font = ModUiTools.GetFontDefinition(Family, Size, Bold, Italic, Strikeout, Underline);
            }
        }
        static void SetBorderStyleInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string BorderStyle = Node.InnerText;
            switch (C.GetType().Name)
            {
                case ("ModTextBox"):
                    (C as ModTextBox).BorderStyle = ModUiTools.GetBorderStyleDefinition(BorderStyle);
                    break;
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).BorderStyle = ModUiTools.GetBorderStyleDefinition(BorderStyle);
                    break;
                case ("ModLabel"):
                    (C as ModLabel).BorderStyle = ModUiTools.GetBorderStyleDefinition(BorderStyle);
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.BorderStyle = ModUiTools.GetBorderStyleDefinition('{1}')", ControlName, BorderStyle));
            RbCode.AppendLine(string.Format("{0}.BorderStyle = ModUiTools.get_border_style_definition('{1}')", ControlName, BorderStyle));
        }
        static void SetIconInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string IconString = Node.InnerText;
            PyCode.AppendLine(string.Format("{0}.Icon = ModUiTools.GetIconDefinition('{1}')", ControlName, IconString));
            RbCode.AppendLine(string.Format("{0}.Icon = ModUiTools.get_icon_definition('{1}')", ControlName, IconString));
            (C as Form).Icon = ModUiTools.GetIconDefinition(IconString);
        }
        static void SetReadOnlyInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool ReadOnly = (Node.InnerText == "True");
            switch (C.GetType().Name)
            {
                case ("ModTextBox"):
                    (C as ModTextBox).ReadOnly = ReadOnly;
                    break;
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).ReadOnly = ReadOnly;
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.ReadOnly = {1}", ControlName, PyBool(ReadOnly)));
            RbCode.AppendLine(string.Format("{0}.ReadOnly = {1}", ControlName, RbBool(ReadOnly)));
        }
        static void SetTextInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string EncodedText = Node.InnerText;
            string DecodedText = "";
            try
            {
                DecodedText = Tools.Base64Decode(EncodedText);
            }
            catch { }
            if (DecodedText.Length == 0) return;
            switch (C.GetType().Name)
            {
                case ("Form"):
                    (C as Form).Text = DecodedText;
                    break;
                case ("ModTextBox"):
                    (C as ModTextBox).Text = DecodedText;
                    break;
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).Text = DecodedText;
                    break;
                case ("ModLabel"):
                    (C as ModLabel).Text = DecodedText;
                    break;
                case ("ModCheckBox"):
                    (C as ModCheckBox).Text = DecodedText;
                    break;
                case ("ModRadioButton"):
                    (C as ModRadioButton).Text = DecodedText;
                    break;
                case ("ModButton"):
                    (C as ModButton).Text = DecodedText;
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.Text =  Tools.Base64Decode('{1}')", ControlName, EncodedText));
            RbCode.AppendLine(string.Format("{0}.Text = Tools.base64_decode('{1}')", ControlName, EncodedText));
        }
        static void SetPasswordCharInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string PasswordChar = Node.InnerText;
            if (PasswordChar.Length == 1)
            {
                switch (C.GetType().Name)
                {
                    case ("ModTextBox"):
                        (C as ModTextBox).PasswordChar = PasswordChar.ToCharArray()[0];
                        break;
                }
                PyCode.AppendLine(string.Format("{0}.PasswordChar = '{1}'", ControlName, PasswordChar));
                RbCode.AppendLine(string.Format("{0}.PasswordChar = '{1}'", ControlName, PasswordChar));
            }
        }
        static void SetTextAlignInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string TextAlign = Node.InnerText;
            switch (C.GetType().Name)
            {
                case ("ModTextBox"):
                    (C as ModTextBox).TextAlign = ModUiTools.GetTextAlignDefinition(TextAlign);
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.TextAlign = ModUiTools.GetTextAlignDefinition('{1}')", ControlName, TextAlign));
            RbCode.AppendLine(string.Format("{0}.TextAlign = ModUiTools.get_text_align_definition('{1}')", ControlName, TextAlign));
        }
        static void SetScrollBarsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string ScrollBars = Node.InnerText;
            switch (C.GetType().Name)
            {
                case ("ModTextBox"):
                    (C as ModTextBox).ScrollBars = ModUiTools.GetScrollBarsDefinition(ScrollBars);
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.ScrollBars = ModUiTools.GetScrollBarsDefinition('{1}')", ControlName, ScrollBars));
            RbCode.AppendLine(string.Format("{0}.ScrollBars = ModUiTools.get_scroll_bars_definition('{1}')", ControlName, ScrollBars));
        }
        static void SetMultiLineInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool MultiLine = (Node.InnerText == "True");
            switch (C.GetType().Name)
            {
                case ("ModTextBox"):
                    (C as ModTextBox).Multiline = MultiLine;
                    break;
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).Multiline = MultiLine;
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.Multiline = {1}", ControlName, PyBool(MultiLine)));
            RbCode.AppendLine(string.Format("{0}.Multiline = {1}", ControlName, RbBool(MultiLine)));
        }
        static void SetWordwrapInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Wordwrap = (Node.InnerText == "True");
            switch (C.GetType().Name)
            {
                case ("ModTextBox"):
                    (C as ModTextBox).WordWrap = Wordwrap;
                    break;
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).WordWrap = Wordwrap;
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.WordWrap = {1}", ControlName, PyBool(Wordwrap)));
            RbCode.AppendLine(string.Format("{0}.WordWrap = {1}", ControlName, RbBool(Wordwrap)));
        }
        static void SetDetectUrlsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool DetectUrls = (Node.InnerText == "True");
            switch (C.GetType().Name)
            {
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).DetectUrls = DetectUrls;
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.DetectUrls = {1}", ControlName, PyBool(DetectUrls)));
            RbCode.AppendLine(string.Format("{0}.DetectUrls = {1}", ControlName, RbBool(DetectUrls)));
        }
        static void SetRichTextScrollBarsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string ScrollBars = Node.InnerText;
            switch (C.GetType().Name)
            {
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).ScrollBars = ModUiTools.GetRichTextBoxScrollBarsDefinition(ScrollBars);
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.ScrollBars = ModUiTools.GetRichTextBoxScrollBarsDefinition('{1}')", ControlName, ScrollBars));
            RbCode.AppendLine(string.Format("{0}.ScrollBars = ModUiTools.get_rich_text_box_scroll_bars_definition('{1}')", ControlName, ScrollBars));
        }
        static void SetRtfInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            string EncodedRtf = Node.InnerText;
            string DecodedRtf = "";
            try
            {
                DecodedRtf = Tools.Base64Decode(EncodedRtf);
            }
            catch { }
            if (DecodedRtf.Length == 0) return;
            switch (C.GetType().Name)
            {
                case ("ModRichTextBox"):
                    (C as ModRichTextBox).Rtf = DecodedRtf;
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.Rtf =  Tools.Base64Decode('{1}')", ControlName, EncodedRtf));
            RbCode.AppendLine(string.Format("{0}.Rtf = Tools.base64_decode('{1}')", ControlName, EncodedRtf));
        }
        static void SetCheckedInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Checked = (Node.InnerText == "True");
            switch (C.GetType().Name)
            {
                case ("ModRadioButton"):
                    (C as ModRadioButton).Checked = Checked;
                    break;
                case ("ModCheckBox"):
                    (C as ModCheckBox).Checked = Checked;
                    break;
                default:
                    return;
            }
            PyCode.AppendLine(string.Format("{0}.Checked = {1}", ControlName, PyBool(Checked)));
            RbCode.AppendLine(string.Format("{0}.Checked = {1}", ControlName, RbBool(Checked)));
        }
        static void SetColumnsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            if (!Node.HasChildNodes) return;
            foreach (XmlNode ColumnNode in Node.ChildNodes)
            {
                if (!ColumnNode.HasChildNodes) continue;
                string Name = "";
                string Type = "";
                bool ReadOnly = false;
                float FillWeight = 0;
                int Width = 0;
                int MinWidth = 0;
                string AutoSizeModeStr = "";
                string EncodedHeaderText = "";
                foreach (XmlNode SubNode in ColumnNode.ChildNodes)
                {
                    switch (SubNode.Name)
                    {
                        case(ModUiTags.Name):
                            Name = SubNode.InnerText;
                            break;
                        case (ModUiTags.CellType):
                            Type = SubNode.InnerText;
                            break;
                        case (ModUiTags.ReadOnly):
                            ReadOnly = SubNode.InnerText.Equals("True");
                            break;
                        case (ModUiTags.FillWeight):
                            FillWeight = float.Parse(SubNode.InnerText);
                            break;
                        case (ModUiTags.MinimumWidth):
                            MinWidth = Int32.Parse(SubNode.InnerText);
                            break;
                        case (ModUiTags.Width):
                            Width = Int32.Parse(SubNode.InnerText);
                            break;
                        case (ModUiTags.AutoSizeColumnsMode):
                            AutoSizeModeStr = SubNode.InnerText;
                            break;
                        case (ModUiTags.HeaderText):
                            EncodedHeaderText = SubNode.InnerText;
                            break;
                    }
                }
                PyCode.AppendLine(string.Format("{0}.Columns.Add(ModUiTools.GetDataGridViewColumnDefinition('{1}', '{2}', {3}, {4}, {5}, {6}, '{7}', '{8}'))", ControlName, Name, Type, PyBool(ReadOnly), FillWeight, Width, MinWidth, AutoSizeModeStr, EncodedHeaderText));
                RbCode.AppendLine(string.Format("{0}.Columns.Add(ModUiTools.get_data_grid_view_column_definition('{1}', '{2}', {3}, {4}, {5}, {6}, '{7}, '{8}''))", ControlName, Name, Type, RbBool(ReadOnly), FillWeight, Width, MinWidth, AutoSizeModeStr, EncodedHeaderText));
                (C as ModDataGridView).Columns.Add(ModUiTools.GetDataGridViewColumnDefinition(Name, Type, ReadOnly, FillWeight, Width, MinWidth, AutoSizeModeStr, EncodedHeaderText));
            }
        }
        static void SetAllowUserToAddRowsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Allow = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.AllowUserToAddRows = {1}", ControlName, PyBool(Allow)));
            RbCode.AppendLine(string.Format("{0}.AllowUserToAddRows = {1}", ControlName, RbBool(Allow)));
            (C as ModDataGridView).AllowUserToAddRows = Allow;
        }
        static void SetAllowUserToDeleteRowsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Allow = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.AllowUserToDeleteRows = {1}", ControlName, PyBool(Allow)));
            RbCode.AppendLine(string.Format("{0}.AllowUserToDeleteRows = {1}", ControlName, RbBool(Allow)));
            (C as ModDataGridView).AllowUserToDeleteRows = Allow;
        }
        static void SetAllowUserToResizeColumnsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Allow = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.AllowUserToResizeColumns = {1}", ControlName, PyBool(Allow)));
            RbCode.AppendLine(string.Format("{0}.AllowUserToResizeColumns = {1}", ControlName, RbBool(Allow)));
            (C as ModDataGridView).AllowUserToResizeColumns = Allow;
        }
        static void SetAllowUserToResizeRowsInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Allow = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.AllowUserToResizeRows = {1}", ControlName, PyBool(Allow)));
            RbCode.AppendLine(string.Format("{0}.AllowUserToResizeRows = {1}", ControlName, RbBool(Allow)));
            (C as ModDataGridView).AllowUserToResizeRows = Allow;
        }
        static void SetColumnHeadersVisibleInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Visible = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.ColumnHeadersVisible = {1}", ControlName, PyBool(Visible)));
            RbCode.AppendLine(string.Format("{0}.ColumnHeadersVisible = {1}", ControlName, RbBool(Visible)));
            (C as ModDataGridView).ColumnHeadersVisible = Visible;
        }
        static void SetRowHeadersVisibleInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            bool Visible = (Node.InnerText == "True");
            PyCode.AppendLine(string.Format("{0}.RowHeadersVisible = {1}", ControlName, PyBool(Visible)));
            RbCode.AppendLine(string.Format("{0}.RowHeadersVisible = {1}", ControlName, RbBool(Visible)));
            (C as ModDataGridView).RowHeadersVisible = Visible;
        }
        static void SetGridColorInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            int ColorCode = Int32.Parse(Node.InnerText);
            PyCode.AppendLine(string.Format("{0}.GridColor = ModUiTools.GetColorDefinition({1})", ControlName, ColorCode));
            RbCode.AppendLine(string.Format("{0}.GridColor = ModUiTools.get_color_definition({1})", ControlName, ColorCode));
            (C as ModDataGridView).GridColor = ModUiTools.GetColorDefinition(ColorCode);
        }
        static void SetBackgroundColorInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            int ColorCode = Int32.Parse(Node.InnerText);
            PyCode.AppendLine(string.Format("{0}.BackgroundColor = ModUiTools.GetColorDefinition({1})", ControlName, ColorCode));
            RbCode.AppendLine(string.Format("{0}.BackgroundColor = ModUiTools.get_color_definition({1})", ControlName, ColorCode));
            (C as ModDataGridView).BackgroundColor = ModUiTools.GetColorDefinition(ColorCode);
        }
        void SetTabPagesInCode(StringBuilder PyCode, StringBuilder RbCode, string ControlName, Control C, XmlNode Node)
        {
            if (!Node.HasChildNodes) return;
            foreach (XmlNode TabPageNode in Node.ChildNodes)
            {
                if (!TabPageNode.HasChildNodes) continue;
                string Name = "";
                string Text = "";
                XmlNode ChildrenNode = null;
                foreach (XmlNode SubNode in TabPageNode.ChildNodes)
                {
                    switch (SubNode.Name)
                    {
                        case (ModUiTags.Name):
                            Name = SubNode.InnerText;
                            break;
                        case (ModUiTags.Properties):
                            if (!SubNode.HasChildNodes) continue;
                            foreach (XmlNode PropertyNode in SubNode.ChildNodes)
                            {
                                if(PropertyNode.Name.Equals(ModUiTags.Text))
                                    Text = SubNode.InnerText;
                            }
                            break;
                        case(ModUiTags.Children):
                            ChildrenNode = SubNode;
                            break;
                    }
                }
                Name = GetControlName("TabPage", Name);
                PyCode.AppendLine(string.Format("{0}.TabPages.Add('{1}', '{2}')", ControlName, Name, Text));
                RbCode.AppendLine(string.Format("{0}.TabPages.Add('{1}', '{2}')", ControlName, Name, Text));
                (C as ModTabControl).TabPages.Add(Name, Text);
                if (ChildrenNode != null)
                {
                    ModCodeAndControlHolder ChildResult = ConvertControlChildXmlNodesToCode(ChildrenNode, string.Format("{0}.TabPages['{1}']", ControlName, Name), (C as ModTabControl).TabPages[Name]);
                    PyCode.Append(ChildResult.PyCode);
                    RbCode.Append(ChildResult.RbCode);
                }
            }
        }
        #endregion

        ModCodeAndControlHolder ConvertControlEventHandlersNodeToCode(XmlNode EventHandlersNode, string ControlName, Control C)
        {
            StringBuilder PyCode = new StringBuilder();
            StringBuilder RbCode = new StringBuilder();
            foreach (XmlNode ChildNode in EventHandlersNode.ChildNodes)
            {
                ModCodeAndControlHolder ChildCode = ConvertControlEventHandlerNodeToCode(ChildNode, ControlName, C);
                PyCode.Append(ChildCode.PyCode);
                RbCode.Append(ChildCode.RbCode);
            }
            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            Result.PyCode = PyCode.ToString();
            Result.RbCode = RbCode.ToString();
            Result.Control = C;
            return Result;
        }
        ModCodeAndControlHolder ConvertControlEventHandlerNodeToCode(XmlNode EventHandlerNode, string ControlName, Control C)
        {
            StringBuilder PyCode = new StringBuilder();
            StringBuilder RbCode = new StringBuilder();
            string EventName = EventHandlerNode.Name;
            string EventHandlerMethod = EventHandlerNode.InnerText;
            if (EventHandlerMethod.Length > 0)
            {
                PyCode.AppendLine(string.Format("{0}.{1} += lambda s,e: {2}", ControlName, EventName, EventHandlerMethod));
                RbCode.AppendLine(string.Format("{0}.{1}  do |s, e|", ControlName, EventName));
                RbCode.AppendLine(string.Format("   {0}", EventHandlerMethod));
                RbCode.AppendLine("end");
                switch (C.GetType().Name)
                {
                    case ("Form"):
                        ModUi.EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModTextBox"):
                        (C as ModTextBox).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModRichTextBox"):
                        (C as ModRichTextBox).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModButton"):
                        (C as ModButton).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModCheckBox"):
                        (C as ModCheckBox).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModRadioButton"):
                        (C as ModRadioButton).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModLabel"):
                        (C as ModLabel).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModDataGridView"):
                        (C as ModDataGridView).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModPanel"):
                        (C as ModPanel).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                    case ("ModTabControl"):
                        (C as ModTabControl).EventHandlers[EventName] = EventHandlerMethod;
                        break;
                }
            }
            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            Result.PyCode = PyCode.ToString();
            Result.RbCode = RbCode.ToString();
            Result.Control = C;
            return Result;
        }

        ModCodeAndControlHolder ConvertControlChildXmlNodesToCode(XmlNode ParentNode, string ParentName, Control ParentControl)
        {
            StringBuilder PyCode = new StringBuilder();
            StringBuilder RbCode = new StringBuilder();
            foreach (XmlNode ChildNode in ParentNode.ChildNodes)
            {
                string ReadControlName = GetControlNameFromControlXmlNode(ChildNode);
                string ControlName = GetControlName(ChildNode.Name, ReadControlName);
                Control C;
                switch (ChildNode.Name)
                {
                    case ("ModLabel"):
                        PyCode.AppendLine(string.Format("{0} = ModLabel()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModLabel.new()", ControlName));
                        try
                        {
                            C = (ModLabel)ModUiDesigner.IDH.CreateComponent(typeof(ModLabel), ControlName);
                        }
                        catch { C = new ModLabel(); }
                        break;
                    case ("ModTextBox"):
                        PyCode.AppendLine(string.Format("{0} = ModTextBox()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModTextBox.new()", ControlName));
                        try
                        {
                            C = (ModTextBox)ModUiDesigner.IDH.CreateComponent(typeof(ModTextBox), ControlName);
                        }
                        catch { C = new ModTextBox(); }
                        break;
                    case ("ModRichTextBox"):
                        PyCode.AppendLine(string.Format("{0} = ModRichTextBox()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModRichTextBox.new()", ControlName));
                        try
                        {
                            C = (ModRichTextBox)ModUiDesigner.IDH.CreateComponent(typeof(ModRichTextBox), ControlName);
                        }
                        catch { C = new ModRichTextBox(); }
                        break;
                    case ("ModButton"):
                        PyCode.AppendLine(string.Format("{0} = ModButton()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModButton.new()", ControlName));
                        try
                        {
                            C = (ModButton)ModUiDesigner.IDH.CreateComponent(typeof(ModButton), ControlName);
                        }
                        catch { C = new ModButton(); }
                        break;
                    case ("ModCheckBox"):
                        PyCode.AppendLine(string.Format("{0} = ModCheckBox()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModCheckBox.new()", ControlName));
                        try
                        {
                            C = (ModCheckBox)ModUiDesigner.IDH.CreateComponent(typeof(ModCheckBox), ControlName);
                        }
                        catch { C = new ModCheckBox(); }
                        break;
                    case ("ModRadioButton"):
                        PyCode.AppendLine(string.Format("{0} = ModRadioButton()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModRadioButton.new()", ControlName));
                        try
                        {
                            C = (ModRadioButton)ModUiDesigner.IDH.CreateComponent(typeof(ModRadioButton), ControlName);
                        }
                        catch { C = new ModRadioButton(); }
                        break;
                    case ("ModDataGridView"):
                        PyCode.AppendLine(string.Format("{0} = ModDataGridView()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModDataGridView.new()", ControlName));
                        try
                        {
                            C = (ModDataGridView)ModUiDesigner.IDH.CreateComponent(typeof(ModDataGridView), ControlName);
                        }
                        catch { C = new ModDataGridView(); }
                        break;
                    case ("ModPanel"):
                        PyCode.AppendLine(string.Format("{0} = ModPanel()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModPanel.new()", ControlName));
                        try
                        {
                            C = (ModPanel)ModUiDesigner.IDH.CreateComponent(typeof(ModPanel), ControlName);
                        }
                        catch { C = new ModPanel(); }
                        break;
                    case ("ModTabControl"):
                        PyCode.AppendLine(string.Format("{0} = ModTabControl()", ControlName));
                        RbCode.AppendLine(string.Format("{0} = ModTabControl.new()", ControlName));
                        ModTabControl M = new ModTabControl();
                        try
                        {
                            C = (ModTabControl)ModUiDesigner.IDH.CreateComponent(typeof(ModTabControl), ControlName);
                        }
                        catch { C = new ModTabControl(); }
                        break;
                    default:
                        continue;
                }
                SetNameInCode(PyCode, RbCode, ControlName, C);
                ModCodeAndControlHolder CPECC = ConvertControlPropertiesEventHandlersChildrenToCode(ChildNode, ControlName, C);
                PyCode.Append(CPECC.PyCode);
                RbCode.Append(CPECC.RbCode);
                PyCode.AppendLine(string.Format("{0}.Controls.Add({1})", ParentName, ControlName));
                RbCode.AppendLine(string.Format("{0}.Controls.Add({1})", ParentName, ControlName));
                PyCode.AppendLine(string.Format("ui.ModControls['{0}'] = {0}", ControlName));
                RbCode.AppendLine(string.Format("ui.mod_controls['{0}'] = {0}", ControlName));
                CPECC.Control.Parent = ParentControl;
            }
            ModCodeAndControlHolder Result = new ModCodeAndControlHolder();
            Result.PyCode = PyCode.ToString();
            Result.RbCode = RbCode.ToString();
            Result.Control = ParentControl;
            return Result;
        }
        string GetControlNameFromControlXmlNode(XmlNode Node)
        {
            if (Node.HasChildNodes)
            {
                foreach (XmlNode ChildNode in Node.ChildNodes)
                {
                    if (ChildNode.Name.Equals(ModUiTags.Name)) return ChildNode.InnerText;
                }
            }
            return "";
        }
        static string PyBool(bool BoolValue)
        {
            if (BoolValue)
                return "True";
            else
                return "False";
        }
        static string RbBool(bool BoolValue)
        {
            if (BoolValue)
                return "true";
            else
                return "false";
        }
        //static string ConvertControlPropertiesXmlNodeToCode(XmlNode Node, bool IsPython)
        //{
        //    StringBuilder Code = new StringBuilder();
        //    if (ValidControlTags.ContainsKey(Node.Name.ToLower()))
        //    {

        //    }
        //    return Code.ToString();
        //}
        #endregion

        #region DefinitionGetters
        public static AnchorStyles GetAnchorStyleDefinition(bool Top, bool Bottom, bool Left, bool Right)
        {
            AnchorStyles AS = AnchorStyles.None;
            if (Top || Bottom || Left || Right)
            {
                AS = AS ^ AnchorStyles.None;
                if (Top) AS = AS | AnchorStyles.Top;
                if (Bottom) AS = AS | AnchorStyles.Bottom;
                if (Left) AS = AS | AnchorStyles.Left;
                if (Right) AS = AS | AnchorStyles.Right;
            }
            return AS;
        }
        public static DockStyle GetDockStyleDefinition(string DockStyleName)
        {
            switch (DockStyleName)
            {
                case("Fill"):
                    return DockStyle.Fill;
                case ("Top"):
                    return DockStyle.Top;
                case ("Left"):
                    return DockStyle.Left;
                case ("Right"):
                    return DockStyle.Right;
                case ("Bottom"):
                    return DockStyle.Bottom;
                default:
                    return DockStyle.None;
            }
        }
        public static BorderStyle GetBorderStyleDefinition(string BorderStyleName)
        {
            switch (BorderStyleName)
            {
                case ("Fixed3D"):
                    return BorderStyle.Fixed3D;
                case ("FixedSingle"):
                    return BorderStyle.FixedSingle;
                case ("None"):
                    return BorderStyle.None;
                default:
                    throw new Exception(string.Format("Invalid BorderStyle - {0}", BorderStyleName));
            }
        }
        public static ScrollBars GetScrollBarsDefinition(string ScrollBarsName)
        {
            switch (ScrollBarsName)
            {
                case ("Both"):
                    return ScrollBars.Both;
                case ("Horizontal"):
                    return ScrollBars.Horizontal;
                case ("None"):
                    return ScrollBars.None;
                case ("Vertical"):
                    return ScrollBars.Vertical;
                default:
                    throw new Exception(string.Format("Invalid ScrollBars - {0}", ScrollBarsName));
            }
        }
        public static RichTextBoxScrollBars GetRichTextBoxScrollBarsDefinition(string ScrollBarsName)
        {
            switch (ScrollBarsName)
            {
                case ("Both"):
                    return RichTextBoxScrollBars.Both;
                case ("ForcedBoth"):
                    return RichTextBoxScrollBars.ForcedBoth;
                case ("ForcedHorizontal"):
                    return RichTextBoxScrollBars.ForcedHorizontal;
                case ("ForcedVertical"):
                    return RichTextBoxScrollBars.ForcedVertical;
                case ("Horizontal"):
                    return RichTextBoxScrollBars.Horizontal;
                case ("Vertical"):
                    return RichTextBoxScrollBars.Vertical;
                case ("None"):
                    return RichTextBoxScrollBars.None;
                default:
                    throw new Exception(string.Format("Invalid ScrollBars - {0}", ScrollBarsName));
            }
        }
        public static HorizontalAlignment GetTextAlignDefinition(string TextAlignName)
        {
            switch (TextAlignName)
            {
                case ("Left"):
                    return HorizontalAlignment.Left;
                case ("Right"):
                    return HorizontalAlignment.Right;
                case ("Center"):
                    return HorizontalAlignment.Center;
                default:
                    throw new Exception(string.Format("Invalid TextAlignment - {0}", TextAlignName));
            }
        }
        public static Font GetFontDefinition(string FontFamilyName, float Size, bool Bold, bool Italic, bool Strikeout, bool Underline)
        {
            FontStyle FS = FontStyle.Regular;
            if (Bold || Italic || Strikeout || Underline)
            {
                FS = FS ^ FontStyle.Regular;
                if (Bold) FS = FS | FontStyle.Bold;
                if (Italic) FS = FS | FontStyle.Italic;
                if (Strikeout) FS = FS | FontStyle.Strikeout;
                if (Underline) FS = FS | FontStyle.Underline;

            }
            return new Font(FontFamilyName, Size, FS);
        }
        public static Size GetSizeDefinition(int Width, int Height)
        {
            return new Size(Width, Height);
        }
        public static Point GetLocationDefinition(int X, int Y)
        {
            return new Point(X, Y);
        }
        public static Color GetColorDefinition(int Colorcode)
        {
            return Color.FromArgb(Colorcode);
        }
        public static Icon GetIconDefinition(string IconString)
        {
            MemoryStream TMS = new MemoryStream(Convert.FromBase64String(IconString));
            return new Icon(TMS);
        }
        public static DataGridViewColumn GetDataGridViewColumnDefinition(string Name, string Type, bool ReadOnly, float FillWeight, int Width, int MinWidth, string AutoSizeModeStr, string EncodedHeaderText)
        {
            DataGridViewColumn C = new DataGridViewColumn();
            switch (Type)
            {
                case("DataGridTextBoxColumn"):
                case ("DataGridViewTextBoxCell"):    
                    C = new DataGridViewTextBoxColumn();
                    break;
                case ("DataGridViewCheckBoxColumn"):
                case ("DataGridViewCheckBoxCell"):
                    C = new DataGridViewCheckBoxColumn();
                    break;
            }
            C.Name = Name;
            C.FillWeight = FillWeight;
            C.Width = Width;
            C.MinimumWidth = MinWidth;
            C.ReadOnly = ReadOnly;
            C.HeaderText = Tools.Base64Decode(EncodedHeaderText);
            C.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            switch (AutoSizeModeStr)
            {
                case ("AllCells"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;
                case ("AllCellsExceptHeader"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    break;
                case ("ColumnHeader"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    break;
                case ("DisplayedCells"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    break;
                case ("DisplayedCellsExceptHeader"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                    break;
                case ("Fill"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    break;
                case ("None"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    break;
                case ("NotSet"):
                    C.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                    break;
            }
            return C;
        }
        #endregion
    }

    public static class ModUiTags
    {
        #region Controls
        public const string Ui = "UI";
        public const string ModLabel = "ModLabel";
        public const string ModButton = "ModButton";
        public const string ModTextBox = "ModTextBox";
        public const string ModRichTextBox = "ModRichTextBox";
        public const string ModCheckBox = "ModCheckBox";
        public const string ModRadioButton = "ModRadioButton";
        public const string ModDataGridView = "ModDataGridView";
        public const string ModPanel = "ModPanel";
        public const string ModTabControl = "ModTabControl";
        #endregion
        #region Basic
        public const string Properties = "properties";
        public const string EventHandlers = "event_handlers";
        public const string Children = "children";
        #endregion
        #region Properties
        //basic
        public const string Name = "name";
        public const string Size = "size";
        public const string SizeWidth = "width";
        public const string SizeHeight = "height";
        public const string Location = "location";
        public const string LocationX = "x";
        public const string LocationY = "y";
        public const string Anchor = "anchor";
        public const string AnchorTop = "top";
        public const string AnchorBottom = "bottom";
        public const string AnchorLeft = "left";
        public const string AnchorRight = "right";
        public const string Dock = "dock";
        public const string Enabled = "enabled";
        public const string Visible = "visible";
        public const string BackColor = "back_color";
        public const string ForeColor = "fore_color";
        //form
        public const string Icon = "icon";
        //text
        public const string Font = "font";
        public const string FontBold = "bold";
        public const string FontFamily = "font_family";
        public const string FontItalic = "italic";
        public const string FontSize = "size";
        public const string FontStrikeout = "strikeout";
        public const string FontUnderline = "underline";
        public const string ReadOnly = "read_only";
        public const string BorderStyle = "border_style";
        public const string Text = "text";
        public const string PasswordChar = "pwd_char";
        public const string MultiLine = "multi_line";
        public const string Wordwrap = "word_wrap";
        public const string TextAlign = "text_align";
        public const string ScrollBars = "scroll_bars";
        //rich text
        public const string Rtf = "rtf";
        public const string RichTextScrollBars = "rich_text_scroll_bars";
        public const string DetectUrls = "detect_urls";
        //radio button/checkbox
        public const string Checked = "checked";
        //datagridview
        public const string AllowUserToAddRows = "allow_add_rows";
        public const string AllowUserToDeleteRows = "allow_delete_rows";
        public const string AllowUserToOrderColumns = "allow_order_columns";
        public const string AllowUserToResizeColumns = "allow_resize_columns";
        public const string AllowUserToResizeRows = "allow_resize_rows";
        public const string ColumnHeadersVisible = "columns_headers_visible";
        public const string ColumnHeadersDefaultCellStyle = "columns_headers_default_cell_style";
        public const string ColumnHeadersBorderStyle = "columns_headers_border_style";
        public const string ColumnHeadersHeightSizeMode = "columns_headers_height_size_mode";
        public const string Columns = "columns";
        public const string Column = "column";
        public const string AutoSizeColumnsMode = "auto_size_columns_mode";
        public const string AlternatingRowsDefaultCellStyle = "alternating_rows_style";
        public const string AutoSizeRowsMode = "auto_size_rows_mode";
        public const string RowsDefaultCellStyle = "rows_default_cell_style";
        public const string RowHeadersVisible = "row_headers_visible";
        public const string DefaultCellStyle = "default_cell_style";
        public const string CellBorderStyle = "cell_border_style";
        public const string CellStyleAlignment = "alignment";
        public const string GridColor = "grid_color";
        public const string MultiSelect = "multi_select";
        public const string BackgroundColor = "background_color";
        public const string CellStyleWrapMode = "wrap_mode";
        public const string CellStyle = "wrap_mode";
        public const string SelectionMode = "selection_mode";
        //data grid view column listbox
        public const string AutoSizeMode = "auto_size_mode";
        public const string SortMode = "sort_mode";
        public const string MinimumWidth = "minimum_width";
        public const string Width = "width";
        public const string Resizable = "resizable";
        public const string HeaderText = "header_text";
        public const string CellType = "cell_type";
        public const string FillWeight = "fill_weight";
        //tab control
        public const string TabPages = "tab_pages";
        public const string TabPage = "tab_page";
        //panel & tab control
        public const string Controls = "controls";
        
        #endregion
        #region EventHandlers
        #endregion

    }

    public class ModCodeAndControlHolder
    {
        internal string XmlCode = "";
        internal string PyCode = "";
        internal string RbCode = "";
        internal Control Control = null;
    }
}
