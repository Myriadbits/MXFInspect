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
            statusStrip = new System.Windows.Forms.StatusStrip();
            tslActivity = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            tslOffsetStyle = new System.Windows.Forms.ToolStripStatusLabel();
            tslPartialLoading = new System.Windows.Forms.ToolStripStatusLabel();
            tslVersion = new System.Windows.Forms.ToolStripStatusLabel();
            menuMain = new System.Windows.Forms.MenuStrip();
            tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            tsmiMXF = new System.Windows.Forms.ToolStripMenuItem();
            tsmiValidationReport = new System.Windows.Forms.ToolStripMenuItem();
            tsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            tsmiFindNextItem = new System.Windows.Forms.ToolStripMenuItem();
            tsmiFindPreviousItem = new System.Windows.Forms.ToolStripMenuItem();
            tsmiFilterCurrentType = new System.Windows.Forms.ToolStripMenuItem();
            tsSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            tsmiShowFillers = new System.Windows.Forms.ToolStripMenuItem();
            tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            tsmiShowPropInfo = new System.Windows.Forms.ToolStripMenuItem();
            tsSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
            tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            toolStrip = new System.Windows.Forms.ToolStrip();
            tsbOpen = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            tsbValidationReport = new System.Windows.Forms.ToolStripButton();
            tsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            tsbFindNextItem = new System.Windows.Forms.ToolStripButton();
            tsbFindPreviousItem = new System.Windows.Forms.ToolStripButton();
            tsbFilterCurrentType = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            tsbShowFillers = new System.Windows.Forms.ToolStripButton();
            tsbCollapseAll = new System.Windows.Forms.ToolStripButton();
            tsbShowPropInfo = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            tsbSettings = new System.Windows.Forms.ToolStripButton();
            tabMain = new System.Windows.Forms.TabControl();
            tslNetRuntimeVersion = new System.Windows.Forms.ToolStripStatusLabel();
            statusStrip.SuspendLayout();
            menuMain.SuspendLayout();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tslActivity, toolStripStatusLabel1, tslOffsetStyle, tslPartialLoading, tslNetRuntimeVersion, tslVersion });
            statusStrip.Location = new System.Drawing.Point(0, 725);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip.Size = new System.Drawing.Size(1370, 24);
            statusStrip.TabIndex = 9;
            // 
            // tslActivity
            // 
            tslActivity.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            tslActivity.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            tslActivity.Name = "tslActivity";
            tslActivity.Size = new System.Drawing.Size(4, 19);
            tslActivity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(995, 19);
            toolStripStatusLabel1.Spring = true;
            // 
            // tslOffsetStyle
            // 
            tslOffsetStyle.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            tslOffsetStyle.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            tslOffsetStyle.Name = "tslOffsetStyle";
            tslOffsetStyle.Size = new System.Drawing.Size(73, 19);
            tslOffsetStyle.Text = "Offset style:";
            tslOffsetStyle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslPartialLoading
            // 
            tslPartialLoading.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            tslPartialLoading.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            tslPartialLoading.Name = "tslPartialLoading";
            tslPartialLoading.Size = new System.Drawing.Size(87, 19);
            tslPartialLoading.Text = "PartialLoading";
            tslPartialLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslVersion
            // 
            tslVersion.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            tslVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            tslVersion.Name = "tslVersion";
            tslVersion.Size = new System.Drawing.Size(45, 19);
            tslVersion.Text = "Version";
            tslVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuMain
            // 
            menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmiFile, tsmiMXF, tsmiView, tsmiWindow, tsmiHelp });
            menuMain.Location = new System.Drawing.Point(0, 0);
            menuMain.MdiWindowListItem = tsmiWindow;
            menuMain.Name = "menuMain";
            menuMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuMain.Size = new System.Drawing.Size(1370, 24);
            menuMain.TabIndex = 14;
            menuMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmiOpenFile, tsmiClose, tsSeparator1, tsmiExit });
            tsmiFile.Name = "tsmiFile";
            tsmiFile.Size = new System.Drawing.Size(37, 20);
            tsmiFile.Text = "&File";
            // 
            // tsmiOpenFile
            // 
            tsmiOpenFile.Image = (System.Drawing.Image)resources.GetObject("tsmiOpenFile.Image");
            tsmiOpenFile.Name = "tsmiOpenFile";
            tsmiOpenFile.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            tsmiOpenFile.Size = new System.Drawing.Size(155, 22);
            tsmiOpenFile.Text = "&Open...";
            tsmiOpenFile.Click += tsmiOpenFile_Click;
            // 
            // tsmiClose
            // 
            tsmiClose.Name = "tsmiClose";
            tsmiClose.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W;
            tsmiClose.Size = new System.Drawing.Size(155, 22);
            tsmiClose.Text = "&Close";
            tsmiClose.Click += tsmiClose_Click;
            // 
            // tsSeparator1
            // 
            tsSeparator1.Name = "tsSeparator1";
            tsSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // tsmiExit
            // 
            tsmiExit.Name = "tsmiExit";
            tsmiExit.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4;
            tsmiExit.Size = new System.Drawing.Size(155, 22);
            tsmiExit.Text = "E&xit";
            tsmiExit.Click += tsmiExit_Click;
            // 
            // tsmiMXF
            // 
            tsmiMXF.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmiValidationReport, tsSeparator4, tsmiFindNextItem, tsmiFindPreviousItem, tsmiFilterCurrentType, tsSeparator5, tsmiShowFillers });
            tsmiMXF.Name = "tsmiMXF";
            tsmiMXF.Size = new System.Drawing.Size(43, 20);
            tsmiMXF.Text = "&MXF";
            // 
            // tsmiValidationReport
            // 
            tsmiValidationReport.Image = (System.Drawing.Image)resources.GetObject("tsmiValidationReport.Image");
            tsmiValidationReport.Name = "tsmiValidationReport";
            tsmiValidationReport.Size = new System.Drawing.Size(197, 22);
            tsmiValidationReport.Text = "&Validation report...";
            tsmiValidationReport.Click += tsmiValidationReport_Click;
            // 
            // tsSeparator4
            // 
            tsSeparator4.Name = "tsSeparator4";
            tsSeparator4.Size = new System.Drawing.Size(194, 6);
            // 
            // tsmiFindNextItem
            // 
            tsmiFindNextItem.Image = (System.Drawing.Image)resources.GetObject("tsmiFindNextItem.Image");
            tsmiFindNextItem.Name = "tsmiFindNextItem";
            tsmiFindNextItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            tsmiFindNextItem.Size = new System.Drawing.Size(197, 22);
            tsmiFindNextItem.Text = "&Next item";
            tsmiFindNextItem.ToolTipText = "Select the next object in the MXF file";
            tsmiFindNextItem.Click += tsmiFindNextItem_Click;
            // 
            // tsmiFindPreviousItem
            // 
            tsmiFindPreviousItem.Image = (System.Drawing.Image)resources.GetObject("tsmiFindPreviousItem.Image");
            tsmiFindPreviousItem.Name = "tsmiFindPreviousItem";
            tsmiFindPreviousItem.ShortcutKeys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3;
            tsmiFindPreviousItem.Size = new System.Drawing.Size(197, 22);
            tsmiFindPreviousItem.Text = "&Previous item";
            tsmiFindPreviousItem.ToolTipText = "Select the previous object in the MXF file";
            tsmiFindPreviousItem.Click += tsmiFindPreviousItem_Click;
            // 
            // tsmiFilterCurrentType
            // 
            tsmiFilterCurrentType.Image = (System.Drawing.Image)resources.GetObject("tsmiFilterCurrentType.Image");
            tsmiFilterCurrentType.Name = "tsmiFilterCurrentType";
            tsmiFilterCurrentType.ShortcutKeys = System.Windows.Forms.Keys.F5;
            tsmiFilterCurrentType.Size = new System.Drawing.Size(197, 22);
            tsmiFilterCurrentType.Text = "&Filter current type";
            tsmiFilterCurrentType.Click += tsmiFilterCurrentType_Click;
            // 
            // tsSeparator5
            // 
            tsSeparator5.Name = "tsSeparator5";
            tsSeparator5.Size = new System.Drawing.Size(194, 6);
            // 
            // tsmiShowFillers
            // 
            tsmiShowFillers.Image = (System.Drawing.Image)resources.GetObject("tsmiShowFillers.Image");
            tsmiShowFillers.Name = "tsmiShowFillers";
            tsmiShowFillers.ShortcutKeys = System.Windows.Forms.Keys.F7;
            tsmiShowFillers.Size = new System.Drawing.Size(197, 22);
            tsmiShowFillers.Text = "&Show fillers";
            tsmiShowFillers.Click += showFillersToolStripMenuItem_Click;
            // 
            // tsmiView
            // 
            tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmiCollapseAll, tsmiShowPropInfo, tsSeparator8, tsmiSettings });
            tsmiView.Name = "tsmiView";
            tsmiView.Size = new System.Drawing.Size(44, 20);
            tsmiView.Text = "View";
            // 
            // tsmiCollapseAll
            // 
            tsmiCollapseAll.Image = (System.Drawing.Image)resources.GetObject("tsmiCollapseAll.Image");
            tsmiCollapseAll.Name = "tsmiCollapseAll";
            tsmiCollapseAll.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A;
            tsmiCollapseAll.Size = new System.Drawing.Size(176, 22);
            tsmiCollapseAll.Text = "Collapse all";
            tsmiCollapseAll.Click += tsmiCollapseAll_Click;
            // 
            // tsmiShowPropInfo
            // 
            tsmiShowPropInfo.Name = "tsmiShowPropInfo";
            tsmiShowPropInfo.Size = new System.Drawing.Size(176, 22);
            tsmiShowPropInfo.Text = "Show property info";
            tsmiShowPropInfo.Click += tsmiShowPropInfo_Click;
            // 
            // tsSeparator8
            // 
            tsSeparator8.Name = "tsSeparator8";
            tsSeparator8.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiSettings
            // 
            tsmiSettings.Name = "tsmiSettings";
            tsmiSettings.Size = new System.Drawing.Size(176, 22);
            tsmiSettings.Text = "Settings...";
            tsmiSettings.Click += tsmiSettings_Click;
            // 
            // tsmiWindow
            // 
            tsmiWindow.Name = "tsmiWindow";
            tsmiWindow.Size = new System.Drawing.Size(63, 20);
            tsmiWindow.Text = "&Window";
            // 
            // tsmiHelp
            // 
            tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmiAbout });
            tsmiHelp.Name = "tsmiHelp";
            tsmiHelp.Size = new System.Drawing.Size(24, 20);
            tsmiHelp.Text = "&?";
            // 
            // tsmiAbout
            // 
            tsmiAbout.Name = "tsmiAbout";
            tsmiAbout.Size = new System.Drawing.Size(116, 22);
            tsmiAbout.Text = "&About...";
            tsmiAbout.Click += tsmiAbout_Click;
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsbOpen, toolStripSeparator2, tsbValidationReport, tsSeparator3, tsbFindNextItem, tsbFindPreviousItem, tsbFilterCurrentType, toolStripSeparator6, tsbShowFillers, tsbCollapseAll, tsbShowPropInfo, toolStripSeparator7, tsbSettings });
            toolStrip.Location = new System.Drawing.Point(0, 24);
            toolStrip.Name = "toolStrip";
            toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            toolStrip.Size = new System.Drawing.Size(1370, 25);
            toolStrip.TabIndex = 16;
            toolStrip.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbOpen.Image = (System.Drawing.Image)resources.GetObject("tsbOpen.Image");
            tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbOpen.Name = "tsbOpen";
            tsbOpen.Size = new System.Drawing.Size(23, 22);
            tsbOpen.Text = "Open file";
            tsbOpen.Click += tsmiOpenFile_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbValidationReport
            // 
            tsbValidationReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbValidationReport.Image = (System.Drawing.Image)resources.GetObject("tsbValidationReport.Image");
            tsbValidationReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbValidationReport.Name = "tsbValidationReport";
            tsbValidationReport.Size = new System.Drawing.Size(23, 22);
            tsbValidationReport.Text = "Show report";
            tsbValidationReport.Click += tsmiValidationReport_Click;
            // 
            // tsSeparator3
            // 
            tsSeparator3.Name = "tsSeparator3";
            tsSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFindNextItem
            // 
            tsbFindNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbFindNextItem.Image = (System.Drawing.Image)resources.GetObject("tsbFindNextItem.Image");
            tsbFindNextItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbFindNextItem.Name = "tsbFindNextItem";
            tsbFindNextItem.Size = new System.Drawing.Size(23, 22);
            tsbFindNextItem.Text = "Select Next";
            tsbFindNextItem.Click += tsmiFindNextItem_Click;
            // 
            // tsbFindPreviousItem
            // 
            tsbFindPreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbFindPreviousItem.Image = (System.Drawing.Image)resources.GetObject("tsbFindPreviousItem.Image");
            tsbFindPreviousItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbFindPreviousItem.Name = "tsbFindPreviousItem";
            tsbFindPreviousItem.Size = new System.Drawing.Size(23, 22);
            tsbFindPreviousItem.Text = "Find Previous";
            tsbFindPreviousItem.Click += tsmiFindPreviousItem_Click;
            // 
            // tsbFilterCurrentType
            // 
            tsbFilterCurrentType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbFilterCurrentType.Image = (System.Drawing.Image)resources.GetObject("tsbFilterCurrentType.Image");
            tsbFilterCurrentType.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbFilterCurrentType.Name = "tsbFilterCurrentType";
            tsbFilterCurrentType.Size = new System.Drawing.Size(23, 22);
            tsbFilterCurrentType.Text = "Filter current type";
            tsbFilterCurrentType.Click += tsmiFilterCurrentType_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbShowFillers
            // 
            tsbShowFillers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbShowFillers.Image = (System.Drawing.Image)resources.GetObject("tsbShowFillers.Image");
            tsbShowFillers.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbShowFillers.Name = "tsbShowFillers";
            tsbShowFillers.Size = new System.Drawing.Size(23, 22);
            tsbShowFillers.Text = "Show Fillers";
            tsbShowFillers.Click += showFillersToolStripMenuItem_Click;
            // 
            // tsbCollapseAll
            // 
            tsbCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbCollapseAll.Image = (System.Drawing.Image)resources.GetObject("tsbCollapseAll.Image");
            tsbCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbCollapseAll.Name = "tsbCollapseAll";
            tsbCollapseAll.Size = new System.Drawing.Size(23, 22);
            tsbCollapseAll.Text = "Collapse all";
            tsbCollapseAll.Click += tsmiCollapseAll_Click;
            // 
            // tsbShowPropInfo
            // 
            tsbShowPropInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbShowPropInfo.Image = (System.Drawing.Image)resources.GetObject("tsbShowPropInfo.Image");
            tsbShowPropInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbShowPropInfo.Name = "tsbShowPropInfo";
            tsbShowPropInfo.Size = new System.Drawing.Size(23, 22);
            tsbShowPropInfo.Text = "Show property info";
            tsbShowPropInfo.Click += tsmiShowPropInfo_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSettings
            // 
            tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            tsbSettings.Image = (System.Drawing.Image)resources.GetObject("tsbSettings.Image");
            tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsbSettings.Name = "tsbSettings";
            tsbSettings.Size = new System.Drawing.Size(23, 22);
            tsbSettings.Text = "Settings...";
            tsbSettings.Click += tsmiSettings_Click;
            // 
            // tabMain
            // 
            tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tabMain.Location = new System.Drawing.Point(0, 49);
            tabMain.Margin = new System.Windows.Forms.Padding(0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new System.Drawing.Size(1370, 676);
            tabMain.TabIndex = 18;
            tabMain.Visible = false;
            tabMain.SelectedIndexChanged += tabMain_SelectedIndexChanged;
            // 
            // tslNetRuntimeVersion
            // 
            tslNetRuntimeVersion.Name = "tslNetRuntimeVersion";
            tslNetRuntimeVersion.Size = new System.Drawing.Size(118, 19);
            tslNetRuntimeVersion.Text = "toolStripStatusLabel2";
            // 
            // FormMain
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1370, 749);
            Controls.Add(tabMain);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip);
            Controls.Add(menuMain);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuMain;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormMain";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            Text = "MXFInspect";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += Main_Load;
            MdiChildActivate += FormMain_MdiChildActivate;
            DragDrop += FormMain_DragDrop;
            DragEnter += FormMain_DragEnter;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuMain.ResumeLayout(false);
            menuMain.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
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
        private System.Windows.Forms.ToolStripStatusLabel tslVersion;
        private System.Windows.Forms.ToolStripStatusLabel tslActivity;
        private System.Windows.Forms.ToolStripStatusLabel tslOffsetStyle;
        private System.Windows.Forms.ToolStripStatusLabel tslPartialLoading;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tslNetRuntimeVersion;
    }
}

