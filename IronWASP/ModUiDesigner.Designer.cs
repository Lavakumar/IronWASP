namespace IronWASP
{
    partial class ModUiDesigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModUiDesigner));
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.ProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadDesignFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.DesignTab = new System.Windows.Forms.TabPage();
            this.BaseSplit = new System.Windows.Forms.SplitContainer();
            this.LeftTabs = new System.Windows.Forms.TabControl();
            this.ToolboxTab = new System.Windows.Forms.TabPage();
            this.PropertiesTab = new System.Windows.Forms.TabPage();
            this.PropertiesSubTabs = new System.Windows.Forms.TabControl();
            this.PropertiesPropertySubTab = new System.Windows.Forms.TabPage();
            this.DataGridColumnAddPanel = new System.Windows.Forms.Panel();
            this.DataGridColumnAddMsgTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DataGridColumnAddTBRB = new System.Windows.Forms.RadioButton();
            this.DataGridColumnAddCBRB = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.DataGridColumnNameAddTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DataGridColumnAddBtn = new System.Windows.Forms.Button();
            this.PropertiesEventHandlerSubTab = new System.Windows.Forms.TabPage();
            this.EventHandlersGrid = new System.Windows.Forms.DataGridView();
            this.EventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventHandler = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodeTab = new System.Windows.Forms.TabPage();
            this.CodeTabs = new System.Windows.Forms.TabControl();
            this.XmlCodeTab = new System.Windows.Forms.TabPage();
            this.XmlOutRTB = new System.Windows.Forms.RichTextBox();
            this.PythonCodeTab = new System.Windows.Forms.TabPage();
            this.PyOutRTB = new System.Windows.Forms.RichTextBox();
            this.RubyCodeTab = new System.Windows.Forms.TabPage();
            this.RbOutRTB = new System.Windows.Forms.RichTextBox();
            this.TopMenu.SuspendLayout();
            this.MainTabs.SuspendLayout();
            this.DesignTab.SuspendLayout();
            this.BaseSplit.Panel1.SuspendLayout();
            this.BaseSplit.SuspendLayout();
            this.LeftTabs.SuspendLayout();
            this.PropertiesTab.SuspendLayout();
            this.PropertiesSubTabs.SuspendLayout();
            this.PropertiesPropertySubTab.SuspendLayout();
            this.DataGridColumnAddPanel.SuspendLayout();
            this.PropertiesEventHandlerSubTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EventHandlersGrid)).BeginInit();
            this.CodeTab.SuspendLayout();
            this.CodeTabs.SuspendLayout();
            this.XmlCodeTab.SuspendLayout();
            this.PythonCodeTab.SuspendLayout();
            this.RubyCodeTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenu
            // 
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.GenerateCodeToolStripMenuItem});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(884, 24);
            this.TopMenu.TabIndex = 0;
            this.TopMenu.Text = "menuStrip1";
            // 
            // ProjectToolStripMenuItem
            // 
            this.ProjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewDesignToolStripMenuItem,
            this.LoadDesignFromXMLToolStripMenuItem});
            this.ProjectToolStripMenuItem.Name = "ProjectToolStripMenuItem";
            this.ProjectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.ProjectToolStripMenuItem.Text = "Project";
            // 
            // NewDesignToolStripMenuItem
            // 
            this.NewDesignToolStripMenuItem.Name = "NewDesignToolStripMenuItem";
            this.NewDesignToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.NewDesignToolStripMenuItem.Text = "New Design";
            this.NewDesignToolStripMenuItem.Click += new System.EventHandler(this.NewDesignToolStripMenuItem_Click);
            // 
            // LoadDesignFromXMLToolStripMenuItem
            // 
            this.LoadDesignFromXMLToolStripMenuItem.Name = "LoadDesignFromXMLToolStripMenuItem";
            this.LoadDesignFromXMLToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.LoadDesignFromXMLToolStripMenuItem.Text = "Load Design from XML";
            this.LoadDesignFromXMLToolStripMenuItem.Click += new System.EventHandler(this.LoadDesignFromXMLToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.DeleteToolStripMenuItem.Text = "Delete Selected Control(s)";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // GenerateCodeToolStripMenuItem
            // 
            this.GenerateCodeToolStripMenuItem.Name = "GenerateCodeToolStripMenuItem";
            this.GenerateCodeToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.GenerateCodeToolStripMenuItem.Text = "Generate Code";
            this.GenerateCodeToolStripMenuItem.Click += new System.EventHandler(this.GenerateCodeToolStripMenuItem_Click);
            // 
            // MainTabs
            // 
            this.MainTabs.Controls.Add(this.DesignTab);
            this.MainTabs.Controls.Add(this.CodeTab);
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 24);
            this.MainTabs.Margin = new System.Windows.Forms.Padding(0);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.Padding = new System.Drawing.Point(0, 0);
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(884, 538);
            this.MainTabs.TabIndex = 1;
            // 
            // DesignTab
            // 
            this.DesignTab.Controls.Add(this.BaseSplit);
            this.DesignTab.Location = new System.Drawing.Point(4, 22);
            this.DesignTab.Margin = new System.Windows.Forms.Padding(0);
            this.DesignTab.Name = "DesignTab";
            this.DesignTab.Size = new System.Drawing.Size(876, 512);
            this.DesignTab.TabIndex = 0;
            this.DesignTab.Text = "Design";
            this.DesignTab.UseVisualStyleBackColor = true;
            // 
            // BaseSplit
            // 
            this.BaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseSplit.Location = new System.Drawing.Point(0, 0);
            this.BaseSplit.Margin = new System.Windows.Forms.Padding(0);
            this.BaseSplit.Name = "BaseSplit";
            // 
            // BaseSplit.Panel1
            // 
            this.BaseSplit.Panel1.Controls.Add(this.LeftTabs);
            // 
            // BaseSplit.Panel2
            // 
            this.BaseSplit.Panel2.BackColor = System.Drawing.Color.White;
            this.BaseSplit.Size = new System.Drawing.Size(876, 512);
            this.BaseSplit.SplitterDistance = 179;
            this.BaseSplit.TabIndex = 0;
            // 
            // LeftTabs
            // 
            this.LeftTabs.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.LeftTabs.Controls.Add(this.ToolboxTab);
            this.LeftTabs.Controls.Add(this.PropertiesTab);
            this.LeftTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftTabs.Location = new System.Drawing.Point(0, 0);
            this.LeftTabs.Margin = new System.Windows.Forms.Padding(0);
            this.LeftTabs.Multiline = true;
            this.LeftTabs.Name = "LeftTabs";
            this.LeftTabs.Padding = new System.Drawing.Point(0, 0);
            this.LeftTabs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LeftTabs.SelectedIndex = 0;
            this.LeftTabs.Size = new System.Drawing.Size(179, 512);
            this.LeftTabs.TabIndex = 0;
            // 
            // ToolboxTab
            // 
            this.ToolboxTab.Location = new System.Drawing.Point(23, 4);
            this.ToolboxTab.Margin = new System.Windows.Forms.Padding(0);
            this.ToolboxTab.Name = "ToolboxTab";
            this.ToolboxTab.Size = new System.Drawing.Size(152, 504);
            this.ToolboxTab.TabIndex = 0;
            this.ToolboxTab.Text = "Toolbox";
            this.ToolboxTab.UseVisualStyleBackColor = true;
            // 
            // PropertiesTab
            // 
            this.PropertiesTab.Controls.Add(this.PropertiesSubTabs);
            this.PropertiesTab.Location = new System.Drawing.Point(23, 4);
            this.PropertiesTab.Margin = new System.Windows.Forms.Padding(0);
            this.PropertiesTab.Name = "PropertiesTab";
            this.PropertiesTab.Size = new System.Drawing.Size(152, 504);
            this.PropertiesTab.TabIndex = 1;
            this.PropertiesTab.Text = "Properties";
            this.PropertiesTab.UseVisualStyleBackColor = true;
            // 
            // PropertiesSubTabs
            // 
            this.PropertiesSubTabs.Controls.Add(this.PropertiesPropertySubTab);
            this.PropertiesSubTabs.Controls.Add(this.PropertiesEventHandlerSubTab);
            this.PropertiesSubTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertiesSubTabs.Location = new System.Drawing.Point(0, 0);
            this.PropertiesSubTabs.Margin = new System.Windows.Forms.Padding(0);
            this.PropertiesSubTabs.Name = "PropertiesSubTabs";
            this.PropertiesSubTabs.Padding = new System.Drawing.Point(0, 0);
            this.PropertiesSubTabs.SelectedIndex = 0;
            this.PropertiesSubTabs.Size = new System.Drawing.Size(152, 504);
            this.PropertiesSubTabs.TabIndex = 0;
            // 
            // PropertiesPropertySubTab
            // 
            this.PropertiesPropertySubTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PropertiesPropertySubTab.Controls.Add(this.DataGridColumnAddPanel);
            this.PropertiesPropertySubTab.Location = new System.Drawing.Point(4, 22);
            this.PropertiesPropertySubTab.Margin = new System.Windows.Forms.Padding(0);
            this.PropertiesPropertySubTab.Name = "PropertiesPropertySubTab";
            this.PropertiesPropertySubTab.Size = new System.Drawing.Size(144, 478);
            this.PropertiesPropertySubTab.TabIndex = 0;
            this.PropertiesPropertySubTab.Text = "Properties";
            this.PropertiesPropertySubTab.UseVisualStyleBackColor = true;
            // 
            // DataGridColumnAddPanel
            // 
            this.DataGridColumnAddPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridColumnAddPanel.Controls.Add(this.DataGridColumnAddMsgTB);
            this.DataGridColumnAddPanel.Controls.Add(this.label3);
            this.DataGridColumnAddPanel.Controls.Add(this.DataGridColumnAddTBRB);
            this.DataGridColumnAddPanel.Controls.Add(this.DataGridColumnAddCBRB);
            this.DataGridColumnAddPanel.Controls.Add(this.label2);
            this.DataGridColumnAddPanel.Controls.Add(this.DataGridColumnNameAddTB);
            this.DataGridColumnAddPanel.Controls.Add(this.label1);
            this.DataGridColumnAddPanel.Controls.Add(this.DataGridColumnAddBtn);
            this.DataGridColumnAddPanel.Location = new System.Drawing.Point(3, 315);
            this.DataGridColumnAddPanel.Name = "DataGridColumnAddPanel";
            this.DataGridColumnAddPanel.Size = new System.Drawing.Size(136, 158);
            this.DataGridColumnAddPanel.TabIndex = 0;
            this.DataGridColumnAddPanel.Visible = false;
            // 
            // DataGridColumnAddMsgTB
            // 
            this.DataGridColumnAddMsgTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridColumnAddMsgTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridColumnAddMsgTB.Location = new System.Drawing.Point(3, 99);
            this.DataGridColumnAddMsgTB.Multiline = true;
            this.DataGridColumnAddMsgTB.Name = "DataGridColumnAddMsgTB";
            this.DataGridColumnAddMsgTB.Size = new System.Drawing.Size(130, 27);
            this.DataGridColumnAddMsgTB.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Enter Grid Column Details:";
            // 
            // DataGridColumnAddTBRB
            // 
            this.DataGridColumnAddTBRB.AutoSize = true;
            this.DataGridColumnAddTBRB.Checked = true;
            this.DataGridColumnAddTBRB.Location = new System.Drawing.Point(46, 53);
            this.DataGridColumnAddTBRB.Name = "DataGridColumnAddTBRB";
            this.DataGridColumnAddTBRB.Size = new System.Drawing.Size(64, 17);
            this.DataGridColumnAddTBRB.TabIndex = 0;
            this.DataGridColumnAddTBRB.TabStop = true;
            this.DataGridColumnAddTBRB.Text = "TextBox";
            this.DataGridColumnAddTBRB.UseVisualStyleBackColor = true;
            // 
            // DataGridColumnAddCBRB
            // 
            this.DataGridColumnAddCBRB.AutoSize = true;
            this.DataGridColumnAddCBRB.Location = new System.Drawing.Point(46, 76);
            this.DataGridColumnAddCBRB.Name = "DataGridColumnAddCBRB";
            this.DataGridColumnAddCBRB.Size = new System.Drawing.Size(74, 17);
            this.DataGridColumnAddCBRB.TabIndex = 1;
            this.DataGridColumnAddCBRB.Text = "CheckBox";
            this.DataGridColumnAddCBRB.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Type:";
            // 
            // DataGridColumnNameAddTB
            // 
            this.DataGridColumnNameAddTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridColumnNameAddTB.Location = new System.Drawing.Point(46, 27);
            this.DataGridColumnNameAddTB.Name = "DataGridColumnNameAddTB";
            this.DataGridColumnNameAddTB.Size = new System.Drawing.Size(87, 20);
            this.DataGridColumnNameAddTB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // DataGridColumnAddBtn
            // 
            this.DataGridColumnAddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridColumnAddBtn.Location = new System.Drawing.Point(3, 132);
            this.DataGridColumnAddBtn.Name = "DataGridColumnAddBtn";
            this.DataGridColumnAddBtn.Size = new System.Drawing.Size(130, 23);
            this.DataGridColumnAddBtn.TabIndex = 0;
            this.DataGridColumnAddBtn.Text = "Add Column to Grid";
            this.DataGridColumnAddBtn.UseVisualStyleBackColor = true;
            this.DataGridColumnAddBtn.Click += new System.EventHandler(this.DataGridColumnAddBtn_Click);
            // 
            // PropertiesEventHandlerSubTab
            // 
            this.PropertiesEventHandlerSubTab.Controls.Add(this.EventHandlersGrid);
            this.PropertiesEventHandlerSubTab.Location = new System.Drawing.Point(4, 22);
            this.PropertiesEventHandlerSubTab.Margin = new System.Windows.Forms.Padding(0);
            this.PropertiesEventHandlerSubTab.Name = "PropertiesEventHandlerSubTab";
            this.PropertiesEventHandlerSubTab.Size = new System.Drawing.Size(144, 478);
            this.PropertiesEventHandlerSubTab.TabIndex = 1;
            this.PropertiesEventHandlerSubTab.Text = "Event Handlers";
            this.PropertiesEventHandlerSubTab.UseVisualStyleBackColor = true;
            // 
            // EventHandlersGrid
            // 
            this.EventHandlersGrid.AllowUserToAddRows = false;
            this.EventHandlersGrid.AllowUserToDeleteRows = false;
            this.EventHandlersGrid.AllowUserToResizeRows = false;
            this.EventHandlersGrid.BackgroundColor = System.Drawing.Color.White;
            this.EventHandlersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EventHandlersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EventHandlersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventName,
            this.EventHandler});
            this.EventHandlersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventHandlersGrid.Location = new System.Drawing.Point(0, 0);
            this.EventHandlersGrid.Margin = new System.Windows.Forms.Padding(0);
            this.EventHandlersGrid.Name = "EventHandlersGrid";
            this.EventHandlersGrid.RowHeadersVisible = false;
            this.EventHandlersGrid.Size = new System.Drawing.Size(144, 478);
            this.EventHandlersGrid.TabIndex = 0;
            this.EventHandlersGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.EventHandlersGrid_CellValueChanged);
            // 
            // EventName
            // 
            this.EventName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventName.FillWeight = 75F;
            this.EventName.HeaderText = "Event";
            this.EventName.Name = "EventName";
            this.EventName.ReadOnly = true;
            // 
            // EventHandler
            // 
            this.EventHandler.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventHandler.HeaderText = "EventHandler Method";
            this.EventHandler.Name = "EventHandler";
            // 
            // CodeTab
            // 
            this.CodeTab.Controls.Add(this.CodeTabs);
            this.CodeTab.Location = new System.Drawing.Point(4, 22);
            this.CodeTab.Margin = new System.Windows.Forms.Padding(0);
            this.CodeTab.Name = "CodeTab";
            this.CodeTab.Size = new System.Drawing.Size(876, 512);
            this.CodeTab.TabIndex = 1;
            this.CodeTab.Text = "Code";
            this.CodeTab.UseVisualStyleBackColor = true;
            // 
            // CodeTabs
            // 
            this.CodeTabs.Controls.Add(this.XmlCodeTab);
            this.CodeTabs.Controls.Add(this.PythonCodeTab);
            this.CodeTabs.Controls.Add(this.RubyCodeTab);
            this.CodeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeTabs.Location = new System.Drawing.Point(0, 0);
            this.CodeTabs.Margin = new System.Windows.Forms.Padding(0);
            this.CodeTabs.Name = "CodeTabs";
            this.CodeTabs.Padding = new System.Drawing.Point(0, 0);
            this.CodeTabs.SelectedIndex = 0;
            this.CodeTabs.Size = new System.Drawing.Size(876, 512);
            this.CodeTabs.TabIndex = 0;
            // 
            // XmlCodeTab
            // 
            this.XmlCodeTab.Controls.Add(this.XmlOutRTB);
            this.XmlCodeTab.Location = new System.Drawing.Point(4, 22);
            this.XmlCodeTab.Margin = new System.Windows.Forms.Padding(0);
            this.XmlCodeTab.Name = "XmlCodeTab";
            this.XmlCodeTab.Size = new System.Drawing.Size(868, 486);
            this.XmlCodeTab.TabIndex = 0;
            this.XmlCodeTab.Text = "XML";
            this.XmlCodeTab.UseVisualStyleBackColor = true;
            // 
            // XmlOutRTB
            // 
            this.XmlOutRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.XmlOutRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XmlOutRTB.Location = new System.Drawing.Point(0, 0);
            this.XmlOutRTB.Margin = new System.Windows.Forms.Padding(0);
            this.XmlOutRTB.Name = "XmlOutRTB";
            this.XmlOutRTB.ReadOnly = true;
            this.XmlOutRTB.Size = new System.Drawing.Size(868, 486);
            this.XmlOutRTB.TabIndex = 0;
            this.XmlOutRTB.Text = "";
            // 
            // PythonCodeTab
            // 
            this.PythonCodeTab.Controls.Add(this.PyOutRTB);
            this.PythonCodeTab.Location = new System.Drawing.Point(4, 22);
            this.PythonCodeTab.Margin = new System.Windows.Forms.Padding(0);
            this.PythonCodeTab.Name = "PythonCodeTab";
            this.PythonCodeTab.Size = new System.Drawing.Size(868, 486);
            this.PythonCodeTab.TabIndex = 1;
            this.PythonCodeTab.Text = "Python";
            this.PythonCodeTab.UseVisualStyleBackColor = true;
            // 
            // PyOutRTB
            // 
            this.PyOutRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PyOutRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PyOutRTB.Location = new System.Drawing.Point(0, 0);
            this.PyOutRTB.Margin = new System.Windows.Forms.Padding(0);
            this.PyOutRTB.Name = "PyOutRTB";
            this.PyOutRTB.ReadOnly = true;
            this.PyOutRTB.Size = new System.Drawing.Size(868, 486);
            this.PyOutRTB.TabIndex = 1;
            this.PyOutRTB.Text = "";
            // 
            // RubyCodeTab
            // 
            this.RubyCodeTab.Controls.Add(this.RbOutRTB);
            this.RubyCodeTab.Location = new System.Drawing.Point(4, 22);
            this.RubyCodeTab.Margin = new System.Windows.Forms.Padding(0);
            this.RubyCodeTab.Name = "RubyCodeTab";
            this.RubyCodeTab.Size = new System.Drawing.Size(868, 486);
            this.RubyCodeTab.TabIndex = 2;
            this.RubyCodeTab.Text = "Ruby";
            this.RubyCodeTab.UseVisualStyleBackColor = true;
            // 
            // RbOutRTB
            // 
            this.RbOutRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RbOutRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RbOutRTB.Location = new System.Drawing.Point(0, 0);
            this.RbOutRTB.Margin = new System.Windows.Forms.Padding(0);
            this.RbOutRTB.Name = "RbOutRTB";
            this.RbOutRTB.ReadOnly = true;
            this.RbOutRTB.Size = new System.Drawing.Size(868, 486);
            this.RbOutRTB.TabIndex = 1;
            this.RbOutRTB.Text = "";
            // 
            // ModUiDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.MainTabs);
            this.Controls.Add(this.TopMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.TopMenu;
            this.Name = "ModUiDesigner";
            this.Text = "UI Designer";
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.MainTabs.ResumeLayout(false);
            this.DesignTab.ResumeLayout(false);
            this.BaseSplit.Panel1.ResumeLayout(false);
            this.BaseSplit.ResumeLayout(false);
            this.LeftTabs.ResumeLayout(false);
            this.PropertiesTab.ResumeLayout(false);
            this.PropertiesSubTabs.ResumeLayout(false);
            this.PropertiesPropertySubTab.ResumeLayout(false);
            this.DataGridColumnAddPanel.ResumeLayout(false);
            this.DataGridColumnAddPanel.PerformLayout();
            this.PropertiesEventHandlerSubTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EventHandlersGrid)).EndInit();
            this.CodeTab.ResumeLayout(false);
            this.CodeTabs.ResumeLayout(false);
            this.XmlCodeTab.ResumeLayout(false);
            this.PythonCodeTab.ResumeLayout(false);
            this.RubyCodeTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem ProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewDesignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadDesignFromXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GenerateCodeToolStripMenuItem;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage DesignTab;
        private System.Windows.Forms.TabPage CodeTab;
        private System.Windows.Forms.TabControl CodeTabs;
        private System.Windows.Forms.TabPage XmlCodeTab;
        private System.Windows.Forms.TabPage PythonCodeTab;
        private System.Windows.Forms.TabPage RubyCodeTab;
        private System.Windows.Forms.SplitContainer BaseSplit;
        private System.Windows.Forms.TabPage ToolboxTab;
        private System.Windows.Forms.TabPage PropertiesTab;
        private System.Windows.Forms.TabControl PropertiesSubTabs;
        private System.Windows.Forms.TabPage PropertiesEventHandlerSubTab;
        internal System.Windows.Forms.TabControl LeftTabs;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.RichTextBox XmlOutRTB;
        private System.Windows.Forms.RichTextBox PyOutRTB;
        private System.Windows.Forms.RichTextBox RbOutRTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventHandler;
        internal System.Windows.Forms.DataGridView EventHandlersGrid;
        internal System.Windows.Forms.TabPage PropertiesPropertySubTab;
        internal System.Windows.Forms.Panel DataGridColumnAddPanel;
        private System.Windows.Forms.Button DataGridColumnAddBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DataGridColumnNameAddTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton DataGridColumnAddTBRB;
        private System.Windows.Forms.RadioButton DataGridColumnAddCBRB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DataGridColumnAddMsgTB;
    }
}