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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.IO;
using System.Reflection;

namespace IronWASP
{
    //Uses code and ideas from http://msdn.microsoft.com/en-us/magazine/cc163634.aspx
    internal class ModToolBox : System.Windows.Forms.UserControl, IToolboxService
    {
		System.ComponentModel.Container components = null;
		int SelectedIndex = 0;
		IDesignerHost DesignerHost = null;

        ListBox ToolsList = null;

		public ModToolBox()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
            this.ToolsListBox = new ListBox();
            this.ToolsListBox.Size = new System.Drawing.Size(this.Width, this.Height);
            this.ToolsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Controls.Add(ToolsListBox);

            System.Drawing.Design.ToolboxItem TBI = new System.Drawing.Design.ToolboxItem();
            TBI.DisplayName = "Select from below:";
            TBI.Bitmap = new System.Drawing.Bitmap(16, 16);
            ToolsList.Items.Add(TBI);
            System.Drawing.ToolboxBitmapAttribute tba;

            //Label
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModLabel));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.Label))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.Label));
            ToolsList.Items.Add(TBI);
            //TextBox
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModTextBox));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.TextBox))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.TextBox));
            ToolsList.Items.Add(TBI);
            //RichTextBox
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModRichTextBox));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.RichTextBox))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.RichTextBox));
            ToolsList.Items.Add(TBI);
            //CheckBox
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModCheckBox));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.CheckBox))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.CheckBox));
            ToolsList.Items.Add(TBI);
            //RadioButton
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModRadioButton));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.RadioButton))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.RadioButton));
            ToolsList.Items.Add(TBI);
            //Button
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModButton));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.Button))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.Button));
            ToolsList.Items.Add(TBI);
            //Panel
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModPanel));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.Panel))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.Panel));
            ToolsList.Items.Add(TBI);
            //TabControl
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModTabControl));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.TabControl))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.TabControl));
            ToolsList.Items.Add(TBI);
            //ModDataGridView
            TBI = new System.Drawing.Design.ToolboxItem(typeof(ModDataGridView));
            tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.DataGridView))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.DataGridView));
            ToolsList.Items.Add(TBI);
            //Test
            //TBI = new System.Drawing.Design.ToolboxItem(typeof(ContextMenuStrip));
            //tba = TypeDescriptor.GetAttributes(typeof(System.Windows.Forms.ContextMenuStrip))[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;
            //TBI.Bitmap = (System.Drawing.Bitmap)tba.GetImage(typeof(System.Windows.Forms.ContextMenuStrip));
            //ToolsList.Items.Add(TBI);

            AddEventHandlers();
            this.DesignerHost = ModUiDesigner.IDH;
		}
		#endregion

		internal ListBox ToolsListBox
		{
			get
			{
				return ToolsList;
			}
			set
			{
				ToolsList = value;
			}
		}

		private void AddEventHandlers()
		{
            ToolsListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToolsListBox_KeyDown);
            ToolsListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolsListBox_MouseDown);
            ToolsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ToolsListBox_DrawItem);
		}

        private void ToolsListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            try
            {
                ListBox lbSender = sender as ListBox;
                if (lbSender == null)
                    return;

                // If this tool is the currently selected tool, draw it with a highlight.
                if (SelectedIndex == e.Index)
                {
                    e.Graphics.FillRectangle(Brushes.LightSlateGray, e.Bounds);
                }

                System.Drawing.Design.ToolboxItem tbi = lbSender.Items[e.Index] as System.Drawing.Design.ToolboxItem;
                Rectangle BitmapBounds = new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y + e.Bounds.Height / 2 - tbi.Bitmap.Height / 2, tbi.Bitmap.Width, tbi.Bitmap.Height);
                Rectangle StringBounds = new Rectangle(e.Bounds.Location.X + BitmapBounds.Width + 5, e.Bounds.Location.Y, e.Bounds.Width - BitmapBounds.Width, e.Bounds.Height);

                StringFormat format = new StringFormat();

                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Near;
                e.Graphics.DrawImage(tbi.Bitmap, BitmapBounds);
                e.Graphics.DrawString(tbi.DisplayName, new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.World), Brushes.Black, StringBounds, format);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void ToolsListBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				ListBox lbSender = sender as ListBox;
				Rectangle lastSelectedBounds = lbSender.GetItemRectangle(0);
				try
				{
					lastSelectedBounds = lbSender.GetItemRectangle(SelectedIndex);
				}
				catch(Exception ex)
				{
					ex.ToString();
				}

				SelectedIndex = lbSender.IndexFromPoint(e.X, e.Y); // change our selection
				lbSender.SelectedIndex = SelectedIndex;
				lbSender.Invalidate(lastSelectedBounds); // clear highlight from last selection
				lbSender.Invalidate(lbSender.GetItemRectangle(SelectedIndex)); // highlight new one

				if (SelectedIndex != 0)
				{
					if (e.Clicks == 2)
					{
						IDesignerHost idh = (IDesignerHost)this.DesignerHost.GetService (typeof(IDesignerHost));
						IToolboxUser tbu = idh.GetDesigner (idh.RootComponent as IComponent) as IToolboxUser;

						if (tbu != null)
						{
							tbu.ToolPicked ((System.Drawing.Design.ToolboxItem)(lbSender.Items[SelectedIndex]));
						}
					}
					else if (e.Clicks < 2)
					{
						System.Drawing.Design.ToolboxItem tbi = lbSender.Items[SelectedIndex] as System.Drawing.Design.ToolboxItem;
						IToolboxService tbs = this;  

						// The IToolboxService serializes ToolboxItems by packaging them in DataObjects.
						DataObject d = tbs.SerializeToolboxItem (tbi) as DataObject;

						try
						{
							lbSender.DoDragDrop (d, DragDropEffects.Copy);
						}
						catch (Exception ex)
						{
							MessageBox.Show (ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			catch(Exception ex)
			{
				ex.ToString();
			}
		}

        private void ToolsListBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				ListBox LbSender = sender as ListBox;
				Rectangle LastSelectedBounds = LbSender.GetItemRectangle(0);
				try
				{
					LastSelectedBounds = LbSender.GetItemRectangle(SelectedIndex);
				}
				catch(Exception ex)
				{
					ex.ToString();
				}
	
				switch (e.KeyCode)
				{
					case Keys.Up: if (SelectedIndex > 0)
								{
									SelectedIndex--; // change selection
									LbSender.SelectedIndex = SelectedIndex;
									LbSender.Invalidate(LastSelectedBounds); // clear old highlight
									LbSender.Invalidate(LbSender.GetItemRectangle(SelectedIndex)); // add new one
								}
						break;

					case Keys.Down: if (SelectedIndex + 1 < LbSender.Items.Count)
									{
										SelectedIndex++; // change selection
										LbSender.SelectedIndex = SelectedIndex;
										LbSender.Invalidate(LastSelectedBounds); // clear old highlight
										LbSender.Invalidate(LbSender.GetItemRectangle(SelectedIndex)); // add new one
									}
						break;

					case Keys.Enter:
						if (DesignerHost == null)
							MessageBox.Show ("idh Null");

						IToolboxUser TBU = DesignerHost.GetDesigner (DesignerHost.RootComponent as IComponent) as IToolboxUser;

                        if (TBU != null)
						{
							// Enter means place the tool with default location and default size.
                            TBU.ToolPicked((System.Drawing.Design.ToolboxItem)(LbSender.Items[SelectedIndex]));
							LbSender.Invalidate (LastSelectedBounds); // clear old highlight
							LbSender.Invalidate (LbSender.GetItemRectangle (SelectedIndex)); // add new one
						}

						break;

					default:
					{
						Console.WriteLine("Error: Not able to add");
						break;					
					}
				} // switch
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
	
		#region IToolboxService Members
        
        // We only implement what is really essential for ToolboxService

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem (IDesignerHost host)
		{
			ListBox List = this.ToolsListBox;
			System.Drawing.Design.ToolboxItem TBI = (System.Drawing.Design.ToolboxItem)List.Items[SelectedIndex];
            if (TBI.DisplayName != "Select from below:")
                return TBI;
			else
				return null;
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem ()
		{
			return this.GetSelectedToolboxItem(null);
		}

        public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem, string category)
        {
        }

        public void AddToolboxItem(System.Drawing.Design.ToolboxItem toolboxItem)
        {
        }

		public bool IsToolboxItem (object serializedObject, IDesignerHost host)
		{
			return false;
		}

		public bool IsToolboxItem (object serializedObject)
		{
			return false;
		}

		public void SetSelectedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
		}

		public void SelectedToolboxItemUsed ()
		{
			ListBox list = this.ToolsListBox;

			list.Invalidate(list.GetItemRectangle(SelectedIndex));
			SelectedIndex = 0;
			list.SelectedIndex = 0;
			list.Invalidate (list.GetItemRectangle (SelectedIndex));
		}

		public CategoryNameCollection CategoryNames
		{
			get
			{
				return null;
			}
		}

		void IToolboxService.Refresh ()
		{
		}

		public void AddLinkedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category, IDesignerHost host)
		{
		}

		public void AddLinkedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, IDesignerHost host)
		{
		}

		public bool IsSupported (object serializedObject, ICollection filterAttributes)
		{
			return false;
		}

		public bool IsSupported (object serializedObject, IDesignerHost host)
		{
			return false;
		}

		public string SelectedCategory
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem (object serializedObject, IDesignerHost host)
		{
			return (System.Drawing.Design.ToolboxItem)((DataObject)serializedObject).GetData(typeof(System.Drawing.Design.ToolboxItem));
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem (object serializedObject)
		{
			return this.DeserializeToolboxItem(serializedObject, this.DesignerHost);
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (string category, IDesignerHost host)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (string category)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (IDesignerHost host)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems ()
		{
			return null;
		}

		public void AddCreator (ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
		{
		}

		public void AddCreator (ToolboxItemCreatorCallback creator, string format)
		{
		}

		public bool SetCursor ()
		{
			return false;
		}

		public void RemoveToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category)
		{
		}

		public void RemoveToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
		}

		public object SerializeToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
			return new DataObject (toolboxItem);
		}

		public void RemoveCreator (string format, IDesignerHost host)
		{
		}

		public void RemoveCreator (string format)
		{
		}

	#endregion
    }
}
