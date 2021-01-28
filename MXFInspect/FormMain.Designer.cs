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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMXF = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiValidationReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFindNextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFindPreviousItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFilterCurrentType = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowFillers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbValidationReport = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFindNextItem = new System.Windows.Forms.ToolStripButton();
            this.tsbFindPreviousItem = new System.Windows.Forms.ToolStripButton();
            this.tsbFilterCurrentType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowFillers = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.tsbShowPropInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tsmiShowPropInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 635);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1337, 22);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiMXF,
            this.tsmiView,
            this.tsmiWindow,
            this.tsmiHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.MdiWindowListItem = this.tsmiWindow;
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1337, 24);
            this.menuMain.TabIndex = 14;
            this.menuMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenFile,
            this.tsmiClose,
            this.tsSeparator1,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "&File";
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.Image = global::Myriadbits.MXFInspect.Properties.Resources.Open_6529;
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpenFile.Size = new System.Drawing.Size(155, 22);
            this.tsmiOpenFile.Text = "&Open...";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsmiOpenFile_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmiClose.Size = new System.Drawing.Size(155, 22);
            this.tsmiClose.Text = "&Close";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.tsmiExit.Size = new System.Drawing.Size(155, 22);
            this.tsmiExit.Text = "E&xit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiMXF
            // 
            this.tsmiMXF.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiValidationReport,
            this.tsSeparator4,
            this.tsmiFindNextItem,
            this.tsmiFindPreviousItem,
            this.tsmiFilterCurrentType,
            this.tsSeparator5,
            this.tsmiShowFillers});
            this.tsmiMXF.Name = "tsmiMXF";
            this.tsmiMXF.Size = new System.Drawing.Size(43, 20);
            this.tsmiMXF.Text = "&MXF";
            // 
            // tsmiValidationReport
            // 
            this.tsmiValidationReport.Image = global::Myriadbits.MXFInspect.Properties.Resources.RSReport_16xLG;
            this.tsmiValidationReport.Name = "tsmiValidationReport";
            this.tsmiValidationReport.Size = new System.Drawing.Size(197, 22);
            this.tsmiValidationReport.Text = "&Validation report...";
            this.tsmiValidationReport.Click += new System.EventHandler(this.tsmiValidationReport_Click);
            // 
            // tsSeparator4
            // 
            this.tsSeparator4.Name = "tsSeparator4";
            this.tsSeparator4.Size = new System.Drawing.Size(194, 6);
            // 
            // tsmiFindNextItem
            // 
            this.tsmiFindNextItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindNext_13243;
            this.tsmiFindNextItem.Name = "tsmiFindNextItem";
            this.tsmiFindNextItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.tsmiFindNextItem.Size = new System.Drawing.Size(197, 22);
            this.tsmiFindNextItem.Text = "&Next item";
            this.tsmiFindNextItem.ToolTipText = "Select the next object in the MXF file";
            this.tsmiFindNextItem.Click += new System.EventHandler(this.tsmiFindNextItem_Click);
            // 
            // tsmiFindPreviousItem
            // 
            this.tsmiFindPreviousItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindPrevious_13244;
            this.tsmiFindPreviousItem.Name = "tsmiFindPreviousItem";
            this.tsmiFindPreviousItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.tsmiFindPreviousItem.Size = new System.Drawing.Size(197, 22);
            this.tsmiFindPreviousItem.Text = "&Previous item";
            this.tsmiFindPreviousItem.ToolTipText = "Select the previous object in the MXF file";
            this.tsmiFindPreviousItem.Click += new System.EventHandler(this.tsmiFindPreviousItem_Click);
            // 
            // tsmiFilterCurrentType
            // 
            this.tsmiFilterCurrentType.Image = global::Myriadbits.MXFInspect.Properties.Resources.FilteredObject_13400_14x;
            this.tsmiFilterCurrentType.Name = "tsmiFilterCurrentType";
            this.tsmiFilterCurrentType.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiFilterCurrentType.Size = new System.Drawing.Size(197, 22);
            this.tsmiFilterCurrentType.Text = "&Filter current type";
            this.tsmiFilterCurrentType.Click += new System.EventHandler(this.tsmiFilterCurrentType_Click);
            // 
            // tsSeparator5
            // 
            this.tsSeparator5.Name = "tsSeparator5";
            this.tsSeparator5.Size = new System.Drawing.Size(194, 6);
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
            // tsmiView
            // 
            this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCollapseAll,
            this.tsmiShowPropInfo,
            this.tsSeparator8,
            this.tsmiSettings});
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(44, 20);
            this.tsmiView.Text = "View";
            // 
            // tsmiCollapseAll
            // 
            this.tsmiCollapseAll.Image = global::Myriadbits.MXFInspect.Properties.Resources.CollapseAll;
            this.tsmiCollapseAll.Name = "tsmiCollapseAll";
            this.tsmiCollapseAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmiCollapseAll.Size = new System.Drawing.Size(180, 22);
            this.tsmiCollapseAll.Text = "Collapse all";
            this.tsmiCollapseAll.Click += new System.EventHandler(this.tsmiCollapseAll_Click);
            // 
            // tsSeparator8
            // 
            this.tsSeparator8.Name = "tsSeparator8";
            this.tsSeparator8.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Image = global::Myriadbits.MXFInspect.Properties.Resources.settings;
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(180, 22);
            this.tsmiSettings.Text = "Settings...";
            this.tsmiSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
            // 
            // tsmiWindow
            // 
            this.tsmiWindow.Name = "tsmiWindow";
            this.tsmiWindow.Size = new System.Drawing.Size(63, 20);
            this.tsmiWindow.Text = "&Window";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(24, 20);
            this.tsmiHelp.Text = "&?";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(116, 22);
            this.tsmiAbout.Text = "&About...";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.toolStripSeparator2,
            this.tsbValidationReport,
            this.tsSeparator3,
            this.tsbFindNextItem,
            this.tsbFindPreviousItem,
            this.tsbFilterCurrentType,
            this.toolStripSeparator6,
            this.tsbShowFillers,
            this.tsbCollapseAll,
            this.tsbShowPropInfo,
            this.toolStripSeparator7,
            this.tsbSettings});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(1337, 25);
            this.toolStrip.TabIndex = 16;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::Myriadbits.MXFInspect.Properties.Resources.Open_6529;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open file";
            this.tsbOpen.Click += new System.EventHandler(this.tsmiOpenFile_Click);
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
            this.tsbValidationReport.Click += new System.EventHandler(this.tsmiValidationReport_Click);
            // 
            // tsSeparator3
            // 
            this.tsSeparator3.Name = "tsSeparator3";
            this.tsSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFindNextItem
            // 
            this.tsbFindNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindNextItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindNext_13243;
            this.tsbFindNextItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindNextItem.Name = "tsbFindNextItem";
            this.tsbFindNextItem.Size = new System.Drawing.Size(23, 22);
            this.tsbFindNextItem.Text = "Select Next";
            this.tsbFindNextItem.Click += new System.EventHandler(this.tsmiFindNextItem_Click);
            // 
            // tsbFindPreviousItem
            // 
            this.tsbFindPreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindPreviousItem.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindPrevious_13244;
            this.tsbFindPreviousItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindPreviousItem.Name = "tsbFindPreviousItem";
            this.tsbFindPreviousItem.Size = new System.Drawing.Size(23, 22);
            this.tsbFindPreviousItem.Text = "Find Previous";
            this.tsbFindPreviousItem.Click += new System.EventHandler(this.tsmiFindPreviousItem_Click);
            // 
            // tsbFilterCurrentType
            // 
            this.tsbFilterCurrentType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFilterCurrentType.Image = global::Myriadbits.MXFInspect.Properties.Resources.FilteredObject_13400_14x;
            this.tsbFilterCurrentType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterCurrentType.Name = "tsbFilterCurrentType";
            this.tsbFilterCurrentType.Size = new System.Drawing.Size(23, 22);
            this.tsbFilterCurrentType.Text = "Filter current type";
            this.tsbFilterCurrentType.Click += new System.EventHandler(this.tsmiFilterCurrentType_Click);
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
            // tsbShowPropInfo
            // 
            this.tsbShowPropInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShowPropInfo.Image = global::Myriadbits.MXFInspect.Properties.Resources.showInfo;
            this.tsbShowPropInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowPropInfo.Name = "tsbShowPropInfo";
            this.tsbShowPropInfo.Size = new System.Drawing.Size(23, 22);
            this.tsbShowPropInfo.Text = "Show property info";
            this.tsbShowPropInfo.Click += new System.EventHandler(this.tsmiShowPropInfo_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = global::Myriadbits.MXFInspect.Properties.Resources.settings;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(23, 22);
            this.tsbSettings.Text = "Settings...";
            this.tsbSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
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
            // tsmiShowPropInfo
            // 
            this.tsmiShowPropInfo.Image = global::Myriadbits.MXFInspect.Properties.Resources.showInfo;
            this.tsmiShowPropInfo.Name = "tsmiShowPropInfo";
            this.tsmiShowPropInfo.Size = new System.Drawing.Size(180, 22);
            this.tsmiShowPropInfo.Text = "Show property info";
            this.tsmiShowPropInfo.Click += new System.EventHandler(this.tsmiShowPropInfo_Click);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 657);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
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
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton tsbOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiMXF;
		private System.Windows.Forms.ToolStripMenuItem tsmiValidationReport;
		private System.Windows.Forms.ToolStripSeparator tsSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbValidationReport;
		private System.Windows.Forms.ToolStripSeparator tsSeparator3;
		private System.Windows.Forms.ToolStripSeparator tsSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiFindNextItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiFindPreviousItem;
		private System.Windows.Forms.ToolStripSeparator tsSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilterCurrentType;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowFillers;
		private System.Windows.Forms.ToolStripButton tsbFindNextItem;
		private System.Windows.Forms.ToolStripButton tsbFindPreviousItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton tsbFilterCurrentType;
		private System.Windows.Forms.ToolStripButton tsbShowFillers;
		private System.Windows.Forms.ToolStripMenuItem tsmiView;
		private System.Windows.Forms.ToolStripMenuItem tsmiCollapseAll;
		private System.Windows.Forms.ToolStripSeparator tsSeparator8;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripButton tsbCollapseAll;
        private System.Windows.Forms.ToolStripButton tsbShowPropInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPropInfo;
    }
}

