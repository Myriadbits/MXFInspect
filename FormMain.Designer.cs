namespace Myriadbits.MXFInspect
{
	partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mXFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmValidationReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.nextItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterCurrentTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.showFillersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsdbConformance = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiArdZdfHDF01a = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArdZdfHDF01b = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArdZdfHDF02a = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArdZdfHDF02b = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArdZdfHDF03a = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArdZdfHDF03b = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.raiSuedtirolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFindNext = new System.Windows.Forms.ToolStripButton();
            this.tsbFindPrevious = new System.Windows.Forms.ToolStripButton();
            this.tsbFilterCurrent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowFillers = new System.Windows.Forms.ToolStripButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.menuMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 635);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1337, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.mXFToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.menuWindow,
            this.toolStripMenuItem1});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.MdiWindowListItem = this.menuWindow;
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1337, 24);
            this.menuMain.TabIndex = 14;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenFile,
            this.menuClose,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuOpenFile
            // 
            this.menuOpenFile.Image = global::Myriadbits.MXFInspect.Properties.Resources.Open_6529;
            this.menuOpenFile.Name = "menuOpenFile";
            this.menuOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpenFile.Size = new System.Drawing.Size(155, 22);
            this.menuOpenFile.Text = "&Open...";
            this.menuOpenFile.Click += new System.EventHandler(this.menuOpenFile_Click);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.menuClose.Size = new System.Drawing.Size(155, 22);
            this.menuClose.Text = "&Close";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mXFToolStripMenuItem
            // 
            this.mXFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmValidationReport,
            this.toolStripSeparator4,
            this.nextItemToolStripMenuItem,
            this.previousItemToolStripMenuItem,
            this.filterCurrentTypeToolStripMenuItem,
            this.toolStripSeparator5,
            this.showFillersToolStripMenuItem});
            this.mXFToolStripMenuItem.Name = "mXFToolStripMenuItem";
            this.mXFToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mXFToolStripMenuItem.Text = "&MXF";
            // 
            // tsmValidationReport
            // 
            this.tsmValidationReport.Image = global::Myriadbits.MXFInspect.Properties.Resources.RSReport_16xLG;
            this.tsmValidationReport.Name = "tsmValidationReport";
            this.tsmValidationReport.Size = new System.Drawing.Size(197, 22);
            this.tsmValidationReport.Text = "&Validation report...";
            this.tsmValidationReport.Click += new System.EventHandler(this.tsmValidationReport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(194, 6);
            // 
            // nextItemToolStripMenuItem
            // 
            this.nextItemToolStripMenuItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindNext_13243;
            this.nextItemToolStripMenuItem.Name = "nextItemToolStripMenuItem";
            this.nextItemToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.nextItemToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.nextItemToolStripMenuItem.Text = "&Next item";
            this.nextItemToolStripMenuItem.ToolTipText = "Select the next object in the MXF file";
            this.nextItemToolStripMenuItem.Click += new System.EventHandler(this.nextItemToolStripMenuItem_Click);
            // 
            // previousItemToolStripMenuItem
            // 
            this.previousItemToolStripMenuItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindPrevious_13244;
            this.previousItemToolStripMenuItem.Name = "previousItemToolStripMenuItem";
            this.previousItemToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.previousItemToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.previousItemToolStripMenuItem.Text = "&Previous item";
            this.previousItemToolStripMenuItem.ToolTipText = "Select the previous object in the MXF file";
            this.previousItemToolStripMenuItem.Click += new System.EventHandler(this.previousItemToolStripMenuItem_Click);
            // 
            // filterCurrentTypeToolStripMenuItem
            // 
            this.filterCurrentTypeToolStripMenuItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FilteredObject_13400_14x;
            this.filterCurrentTypeToolStripMenuItem.Name = "filterCurrentTypeToolStripMenuItem";
            this.filterCurrentTypeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.filterCurrentTypeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.filterCurrentTypeToolStripMenuItem.Text = "&Filter current type";
            this.filterCurrentTypeToolStripMenuItem.Click += new System.EventHandler(this.filterCurrentTypeToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(194, 6);
            // 
            // showFillersToolStripMenuItem
            // 
            this.showFillersToolStripMenuItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.HideMember_6755;
            this.showFillersToolStripMenuItem.Name = "showFillersToolStripMenuItem";
            this.showFillersToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.showFillersToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.showFillersToolStripMenuItem.Text = "&Show fillers";
            this.showFillersToolStripMenuItem.Click += new System.EventHandler(this.showFillersToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCollapseAll,
            this.toolStripSeparator8,
            this.settingsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // tsmiCollapseAll
            // 
            this.tsmiCollapseAll.Name = "tsmiCollapseAll";
            this.tsmiCollapseAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmiCollapseAll.Size = new System.Drawing.Size(176, 22);
            this.tsmiCollapseAll.Text = "Collapse all";
            this.tsmiCollapseAll.Click += new System.EventHandler(this.tsmiCollapseAll_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(173, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.Name = "menuWindow";
            this.menuWindow.Size = new System.Drawing.Size(63, 20);
            this.menuWindow.Text = "&Window";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "&?";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(116, 22);
            this.menuAbout.Text = "&About...";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.toolStripSeparator2,
            this.tsbReport,
            this.toolStripSeparator3,
            this.tsdbConformance,
            this.toolStripSeparator7,
            this.tsbFindNext,
            this.tsbFindPrevious,
            this.tsbFilterCurrent,
            this.toolStripSeparator6,
            this.tsbShowFillers});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1337, 25);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::Myriadbits.MXFInspect.Properties.Resources.Open_6529;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open file";
            this.tsbOpen.Click += new System.EventHandler(this.menuOpenFile_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbReport
            // 
            this.tsbReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReport.Image = ((System.Drawing.Image)(resources.GetObject("tsbReport.Image")));
            this.tsbReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReport.Name = "tsbReport";
            this.tsbReport.Size = new System.Drawing.Size(23, 22);
            this.tsbReport.Text = "Show report";
            this.tsbReport.Click += new System.EventHandler(this.tsmValidationReport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsdbConformance
            // 
            this.tsdbConformance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsdbConformance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiArdZdfHDF01a,
            this.tsmiArdZdfHDF01b,
            this.tsmiArdZdfHDF02a,
            this.tsmiArdZdfHDF02b,
            this.tsmiArdZdfHDF03a,
            this.tsmiArdZdfHDF03b,
            this.toolStripSeparator9,
            this.raiSuedtirolToolStripMenuItem});
            this.tsdbConformance.Image = global::Myriadbits.MXFInspect.Properties.Resources._64702;
            this.tsdbConformance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsdbConformance.Name = "tsdbConformance";
            this.tsdbConformance.Size = new System.Drawing.Size(29, 22);
            this.tsdbConformance.Text = "Check conformance...";
            this.tsdbConformance.ToolTipText = "Check conformance...";
            // 
            // tsmiArdZdfHDF01a
            // 
            this.tsmiArdZdfHDF01a.Name = "tsmiArdZdfHDF01a";
            this.tsmiArdZdfHDF01a.Size = new System.Drawing.Size(227, 22);
            this.tsmiArdZdfHDF01a.Text = "ARD-ZDF HDF01a-1.2-Profile";
            this.tsmiArdZdfHDF01a.Click += new System.EventHandler(this.tsmiArdZdfHDF01a_Click);
            // 
            // tsmiArdZdfHDF01b
            // 
            this.tsmiArdZdfHDF01b.Enabled = false;
            this.tsmiArdZdfHDF01b.Name = "tsmiArdZdfHDF01b";
            this.tsmiArdZdfHDF01b.Size = new System.Drawing.Size(227, 22);
            this.tsmiArdZdfHDF01b.Text = "ARD-ZDF HDF01b-1.2-Profile";
            // 
            // tsmiArdZdfHDF02a
            // 
            this.tsmiArdZdfHDF02a.Enabled = false;
            this.tsmiArdZdfHDF02a.Name = "tsmiArdZdfHDF02a";
            this.tsmiArdZdfHDF02a.Size = new System.Drawing.Size(227, 22);
            this.tsmiArdZdfHDF02a.Text = "ARD-ZDF HDF02a-1.2-Profile";
            // 
            // tsmiArdZdfHDF02b
            // 
            this.tsmiArdZdfHDF02b.Enabled = false;
            this.tsmiArdZdfHDF02b.Name = "tsmiArdZdfHDF02b";
            this.tsmiArdZdfHDF02b.Size = new System.Drawing.Size(227, 22);
            this.tsmiArdZdfHDF02b.Text = "ARD-ZDF HDF02b-1.2-Profile";
            // 
            // tsmiArdZdfHDF03a
            // 
            this.tsmiArdZdfHDF03a.Enabled = false;
            this.tsmiArdZdfHDF03a.Name = "tsmiArdZdfHDF03a";
            this.tsmiArdZdfHDF03a.Size = new System.Drawing.Size(227, 22);
            this.tsmiArdZdfHDF03a.Text = "ARD-ZDF HDF03a-1.2-Profile";
            // 
            // tsmiArdZdfHDF03b
            // 
            this.tsmiArdZdfHDF03b.Enabled = false;
            this.tsmiArdZdfHDF03b.Name = "tsmiArdZdfHDF03b";
            this.tsmiArdZdfHDF03b.Size = new System.Drawing.Size(227, 22);
            this.tsmiArdZdfHDF03b.Text = "ARD-ZDF HDF03b-1.2-Profile";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(224, 6);
            // 
            // raiSuedtirolToolStripMenuItem
            // 
            this.raiSuedtirolToolStripMenuItem.Enabled = false;
            this.raiSuedtirolToolStripMenuItem.Name = "raiSuedtirolToolStripMenuItem";
            this.raiSuedtirolToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.raiSuedtirolToolStripMenuItem.Text = "Rai Suedtirol";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFindNext
            // 
            this.tsbFindNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindNext.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindNext_13243;
            this.tsbFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindNext.Name = "tsbFindNext";
            this.tsbFindNext.Size = new System.Drawing.Size(23, 22);
            this.tsbFindNext.Text = "Select Next";
            this.tsbFindNext.Click += new System.EventHandler(this.nextItemToolStripMenuItem_Click);
            // 
            // tsbFindPrevious
            // 
            this.tsbFindPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindPrevious.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindPrevious_13244;
            this.tsbFindPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindPrevious.Name = "tsbFindPrevious";
            this.tsbFindPrevious.Size = new System.Drawing.Size(23, 22);
            this.tsbFindPrevious.Text = "Find Previous";
            this.tsbFindPrevious.Click += new System.EventHandler(this.previousItemToolStripMenuItem_Click);
            // 
            // tsbFilterCurrent
            // 
            this.tsbFilterCurrent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFilterCurrent.Image = global::Myriadbits.MXFInspect.Properties.Resources.FilteredObject_13400_14x;
            this.tsbFilterCurrent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterCurrent.Name = "tsbFilterCurrent";
            this.tsbFilterCurrent.Size = new System.Drawing.Size(23, 22);
            this.tsbFilterCurrent.Text = "Filter current type";
            this.tsbFilterCurrent.Click += new System.EventHandler(this.filterCurrentTypeToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbShowFillers
            // 
            this.tsbShowFillers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowFillers.Image = global::Myriadbits.MXFInspect.Properties.Resources.HideMember_6755;
            this.tsbShowFillers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowFillers.Name = "tsbShowFillers";
            this.tsbShowFillers.Size = new System.Drawing.Size(23, 22);
            this.tsbShowFillers.Text = "Show Fillers";
            this.tsbShowFillers.Click += new System.EventHandler(this.showFillersToolStripMenuItem_Click);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Location = new System.Drawing.Point(0, 49);
            this.tabMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1337, 586);
            this.tabMain.TabIndex = 18;
            this.tabMain.Visible = false;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 657);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "MXFInspect";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MdiChildActivate += new System.EventHandler(this.FormMain_MdiChildActivate);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuOpenFile;
		private System.Windows.Forms.ToolStripMenuItem menuClose;
		private System.Windows.Forms.ToolStripMenuItem menuWindow;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuAbout;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.ToolStripMenuItem mXFToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmValidationReport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbReport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem nextItemToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem previousItemToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem filterCurrentTypeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showFillersToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton tsbFindNext;
		private System.Windows.Forms.ToolStripButton tsbFindPrevious;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton tsbFilterCurrent;
		private System.Windows.Forms.ToolStripButton tsbShowFillers;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiCollapseAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripDropDownButton tsdbConformance;
        private System.Windows.Forms.ToolStripMenuItem tsmiArdZdfHDF01a;
        private System.Windows.Forms.ToolStripMenuItem tsmiArdZdfHDF01b;
        private System.Windows.Forms.ToolStripMenuItem tsmiArdZdfHDF02a;
        private System.Windows.Forms.ToolStripMenuItem tsmiArdZdfHDF02b;
        private System.Windows.Forms.ToolStripMenuItem tsmiArdZdfHDF03a;
        private System.Windows.Forms.ToolStripMenuItem tsmiArdZdfHDF03b;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem raiSuedtirolToolStripMenuItem;
    }
}

