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
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mXFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmValidationReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFindNextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFindPreviousItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFilterCurrentType = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowFillers = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsbValidationReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFindNextItem = new System.Windows.Forms.ToolStripButton();
            this.tsbFindPreviousItem = new System.Windows.Forms.ToolStripButton();
            this.tsbFilterCurrentType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowFillers = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapseAll = new System.Windows.Forms.ToolStripButton();
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
            this.tsmiClose,
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
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmiClose.Size = new System.Drawing.Size(155, 22);
            this.tsmiClose.Text = "&Close";
            this.tsmiClose.Click += new System.EventHandler(this.menuClose_Click);
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
            this.tsmiFindNextItem,
            this.tsmiFindPreviousItem,
            this.tsmiFilterCurrentType,
            this.toolStripSeparator5,
            this.tsmiShowFillers});
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
            // tsmiFindNextItem
            // 
            this.tsmiFindNextItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindNext_13243;
            this.tsmiFindNextItem.Name = "tsmiFindNextItem";
            this.tsmiFindNextItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.tsmiFindNextItem.Size = new System.Drawing.Size(197, 22);
            this.tsmiFindNextItem.Text = "&Next item";
            this.tsmiFindNextItem.ToolTipText = "Select the next object in the MXF file";
            this.tsmiFindNextItem.Click += new System.EventHandler(this.nextItemToolStripMenuItem_Click);
            // 
            // tsmiFindPreviousItem
            // 
            this.tsmiFindPreviousItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindPrevious_13244;
            this.tsmiFindPreviousItem.Name = "tsmiFindPreviousItem";
            this.tsmiFindPreviousItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.tsmiFindPreviousItem.Size = new System.Drawing.Size(197, 22);
            this.tsmiFindPreviousItem.Text = "&Previous item";
            this.tsmiFindPreviousItem.ToolTipText = "Select the previous object in the MXF file";
            this.tsmiFindPreviousItem.Click += new System.EventHandler(this.previousItemToolStripMenuItem_Click);
            // 
            // tsmiFilterCurrentType
            // 
            this.tsmiFilterCurrentType.Image = global::Myriadbits.MXFInspect.Properties.Resources.FilteredObject_13400_14x;
            this.tsmiFilterCurrentType.Name = "tsmiFilterCurrentType";
            this.tsmiFilterCurrentType.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiFilterCurrentType.Size = new System.Drawing.Size(197, 22);
            this.tsmiFilterCurrentType.Text = "&Filter current type";
            this.tsmiFilterCurrentType.Click += new System.EventHandler(this.filterCurrentTypeToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(194, 6);
            // 
            // tsmiShowFillers
            // 
            this.tsmiShowFillers.Image = global::Myriadbits.MXFInspect.Properties.Resources.HideMember_6755;
            this.tsmiShowFillers.Name = "tsmiShowFillers";
            this.tsmiShowFillers.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmiShowFillers.Size = new System.Drawing.Size(197, 22);
            this.tsmiShowFillers.Text = "&Show fillers";
            this.tsmiShowFillers.Click += new System.EventHandler(this.showFillersToolStripMenuItem_Click);
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
            this.tsmiCollapseAll.Image = global::Myriadbits.MXFInspect.Properties.Resources.CollapseAll;
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
            this.tsbValidationReport,
            this.toolStripSeparator3,
            this.tsbFindNextItem,
            this.tsbFindPreviousItem,
            this.tsbFilterCurrentType,
            this.toolStripSeparator6,
            this.tsbShowFillers,
            this.tsbCollapseAll});
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
            // tsbValidationReport
            // 
            this.tsbValidationReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbValidationReport.Image = ((System.Drawing.Image)(resources.GetObject("tsbValidationReport.Image")));
            this.tsbValidationReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbValidationReport.Name = "tsbValidationReport";
            this.tsbValidationReport.Size = new System.Drawing.Size(23, 22);
            this.tsbValidationReport.Text = "Show report";
            this.tsbValidationReport.Click += new System.EventHandler(this.tsmValidationReport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFindNextItem
            // 
            this.tsbFindNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindNextItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindNext_13243;
            this.tsbFindNextItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindNextItem.Name = "tsbFindNextItem";
            this.tsbFindNextItem.Size = new System.Drawing.Size(23, 22);
            this.tsbFindNextItem.Text = "Select Next";
            this.tsbFindNextItem.Click += new System.EventHandler(this.nextItemToolStripMenuItem_Click);
            // 
            // tsbFindPreviousItem
            // 
            this.tsbFindPreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindPreviousItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindPrevious_13244;
            this.tsbFindPreviousItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindPreviousItem.Name = "tsbFindPreviousItem";
            this.tsbFindPreviousItem.Size = new System.Drawing.Size(23, 22);
            this.tsbFindPreviousItem.Text = "Find Previous";
            this.tsbFindPreviousItem.Click += new System.EventHandler(this.previousItemToolStripMenuItem_Click);
            // 
            // tsbFilterCurrentType
            // 
            this.tsbFilterCurrentType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFilterCurrentType.Image = global::Myriadbits.MXFInspect.Properties.Resources.FilteredObject_13400_14x;
            this.tsbFilterCurrentType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterCurrentType.Name = "tsbFilterCurrentType";
            this.tsbFilterCurrentType.Size = new System.Drawing.Size(23, 22);
            this.tsbFilterCurrentType.Text = "Filter current type";
            this.tsbFilterCurrentType.Click += new System.EventHandler(this.filterCurrentTypeToolStripMenuItem_Click);
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
            // tsbCollapseAll
            // 
            this.tsbCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCollapseAll.Image = global::Myriadbits.MXFInspect.Properties.Resources.CollapseAll;
            this.tsbCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollapseAll.Name = "tsbCollapseAll";
            this.tsbCollapseAll.Size = new System.Drawing.Size(23, 22);
            this.tsbCollapseAll.Text = "Collapse all";
            this.tsbCollapseAll.Click += new System.EventHandler(this.tsmiCollapseAll_Click);
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
            this.AllowDrop = true;
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
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
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
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
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
		private System.Windows.Forms.ToolStripButton tsbValidationReport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiFindNextItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiFindPreviousItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilterCurrentType;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowFillers;
		private System.Windows.Forms.ToolStripButton tsbFindNextItem;
		private System.Windows.Forms.ToolStripButton tsbFindPreviousItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton tsbFilterCurrentType;
		private System.Windows.Forms.ToolStripButton tsbShowFillers;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiCollapseAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbCollapseAll;
    }
}

