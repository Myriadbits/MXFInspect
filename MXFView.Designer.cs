namespace Myriadbits.MXFInspect
{
	partial class MXFView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MXFView));
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tpLocation = new System.Windows.Forms.TabPage();
			this.treeListViewMain = new BrightIdeasSoftware.TreeListView();
			this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.tpLogical = new System.Windows.Forms.TabPage();
			this.treeListViewLogical = new BrightIdeasSoftware.TreeListView();
			this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.splitRight = new System.Windows.Forms.SplitContainer();
			this.chkInfo = new System.Windows.Forms.CheckBox();
			this.btnSelectReference = new System.Windows.Forms.Button();
			this.propGrid = new System.Windows.Forms.PropertyGrid();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.txtHex = new System.Windows.Forms.RichTextBox();
			this.imageListResult = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.txtOverall = new System.Windows.Forms.TextBox();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.prbProcessing = new System.Windows.Forms.ProgressBar();
			this.bgwProcess = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tpLocation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListViewMain)).BeginInit();
			this.tpLogical.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListViewLogical)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
			this.splitRight.Panel1.SuspendLayout();
			this.splitRight.Panel2.SuspendLayout();
			this.splitRight.SuspendLayout();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitMain
			// 
			this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitMain.Location = new System.Drawing.Point(0, 3);
			this.splitMain.Name = "splitMain";
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(this.tabMain);
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.splitRight);
			this.splitMain.Size = new System.Drawing.Size(1108, 548);
			this.splitMain.SplitterDistance = 525;
			this.splitMain.TabIndex = 0;
			// 
			// tabMain
			// 
			this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabMain.Controls.Add(this.tpLocation);
			this.tabMain.Controls.Add(this.tpLogical);
			this.tabMain.Location = new System.Drawing.Point(4, 4);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(523, 545);
			this.tabMain.TabIndex = 0;
			// 
			// tpLocation
			// 
			this.tpLocation.Controls.Add(this.treeListViewMain);
			this.tpLocation.Location = new System.Drawing.Point(4, 22);
			this.tpLocation.Name = "tpLocation";
			this.tpLocation.Padding = new System.Windows.Forms.Padding(3);
			this.tpLocation.Size = new System.Drawing.Size(515, 519);
			this.tpLocation.TabIndex = 0;
			this.tpLocation.Text = "Location";
			this.tpLocation.UseVisualStyleBackColor = true;
			// 
			// treeListViewMain
			// 
			this.treeListViewMain.AllColumns.Add(this.olvColumn1);
			this.treeListViewMain.AllColumns.Add(this.olvColumn2);
			this.treeListViewMain.AlternateRowBackColor = System.Drawing.Color.WhiteSmoke;
			this.treeListViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeListViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
			this.treeListViewMain.EmptyListMsg = "No items present";
			this.treeListViewMain.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeListViewMain.FullRowSelect = true;
			this.treeListViewMain.HideSelection = false;
			this.treeListViewMain.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
			this.treeListViewMain.Location = new System.Drawing.Point(6, 6);
			this.treeListViewMain.MultiSelect = false;
			this.treeListViewMain.Name = "treeListViewMain";
			this.treeListViewMain.OwnerDraw = true;
			this.treeListViewMain.RowHeight = 19;
			this.treeListViewMain.ShowGroups = false;
			this.treeListViewMain.Size = new System.Drawing.Size(503, 507);
			this.treeListViewMain.TabIndex = 15;
			this.treeListViewMain.TintSortColumn = true;
			this.treeListViewMain.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
			this.treeListViewMain.UseAlternatingBackColors = true;
			this.treeListViewMain.UseCellFormatEvents = true;
			this.treeListViewMain.UseCompatibleStateImageBehavior = false;
			this.treeListViewMain.UseFiltering = true;
			this.treeListViewMain.View = System.Windows.Forms.View.Details;
			this.treeListViewMain.VirtualMode = true;
			this.treeListViewMain.Expanding += new System.EventHandler<BrightIdeasSoftware.TreeBranchExpandingEventArgs>(this.treeListViewMain_Expanding);
			this.treeListViewMain.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.treeListViewMain_FormatCell);
			this.treeListViewMain.SelectionChanged += new System.EventHandler(this.treeListViewMain_SelectionChanged);
			// 
			// olvColumn1
			// 
			this.olvColumn1.AspectName = "Offset";
			this.olvColumn1.Text = "Offset";
			this.olvColumn1.Width = 84;
			// 
			// olvColumn2
			// 
			this.olvColumn2.AspectName = "ToString";
			this.olvColumn2.FillsFreeSpace = true;
			this.olvColumn2.Hyperlink = true;
			this.olvColumn2.Text = "Name";
			this.olvColumn2.Width = 276;
			// 
			// tpLogical
			// 
			this.tpLogical.Controls.Add(this.treeListViewLogical);
			this.tpLogical.Location = new System.Drawing.Point(4, 22);
			this.tpLogical.Name = "tpLogical";
			this.tpLogical.Padding = new System.Windows.Forms.Padding(3);
			this.tpLogical.Size = new System.Drawing.Size(515, 519);
			this.tpLogical.TabIndex = 1;
			this.tpLogical.Text = "Logical";
			this.tpLogical.UseVisualStyleBackColor = true;
			// 
			// treeListViewLogical
			// 
			this.treeListViewLogical.AllColumns.Add(this.olvColumn3);
			this.treeListViewLogical.AllColumns.Add(this.olvColumn4);
			this.treeListViewLogical.AlternateRowBackColor = System.Drawing.Color.WhiteSmoke;
			this.treeListViewLogical.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeListViewLogical.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3,
            this.olvColumn4});
			this.treeListViewLogical.EmptyListMsg = "No items present";
			this.treeListViewLogical.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeListViewLogical.FullRowSelect = true;
			this.treeListViewLogical.HideSelection = false;
			this.treeListViewLogical.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
			this.treeListViewLogical.Location = new System.Drawing.Point(6, 6);
			this.treeListViewLogical.MultiSelect = false;
			this.treeListViewLogical.Name = "treeListViewLogical";
			this.treeListViewLogical.OwnerDraw = true;
			this.treeListViewLogical.RowHeight = 19;
			this.treeListViewLogical.ShowGroups = false;
			this.treeListViewLogical.Size = new System.Drawing.Size(503, 507);
			this.treeListViewLogical.TabIndex = 16;
			this.treeListViewLogical.TintSortColumn = true;
			this.treeListViewLogical.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
			this.treeListViewLogical.UseAlternatingBackColors = true;
			this.treeListViewLogical.UseCellFormatEvents = true;
			this.treeListViewLogical.UseCompatibleStateImageBehavior = false;
			this.treeListViewLogical.UseFiltering = true;
			this.treeListViewLogical.View = System.Windows.Forms.View.Details;
			this.treeListViewLogical.VirtualMode = true;
			this.treeListViewLogical.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.treeListViewMain_FormatCell);
			this.treeListViewLogical.SelectionChanged += new System.EventHandler(this.treeListViewLogical_SelectionChanged);
			// 
			// olvColumn3
			// 
			this.olvColumn3.AspectName = "Object.Offset";
			this.olvColumn3.Text = "Offset";
			this.olvColumn3.Width = 66;
			// 
			// olvColumn4
			// 
			this.olvColumn4.AspectName = "ToString";
			this.olvColumn4.FillsFreeSpace = true;
			this.olvColumn4.Hyperlink = true;
			this.olvColumn4.Text = "Name";
			this.olvColumn4.Width = 276;
			// 
			// splitRight
			// 
			this.splitRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitRight.Location = new System.Drawing.Point(0, 0);
			this.splitRight.Name = "splitRight";
			this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitRight.Panel1
			// 
			this.splitRight.Panel1.Controls.Add(this.chkInfo);
			this.splitRight.Panel1.Controls.Add(this.btnSelectReference);
			this.splitRight.Panel1.Controls.Add(this.propGrid);
			this.splitRight.Panel1.Controls.Add(this.btnPrevious);
			this.splitRight.Panel1.Controls.Add(this.btnNext);
			// 
			// splitRight.Panel2
			// 
			this.splitRight.Panel2.Controls.Add(this.txtHex);
			this.splitRight.Size = new System.Drawing.Size(582, 549);
			this.splitRight.SplitterDistance = 388;
			this.splitRight.TabIndex = 16;
			// 
			// chkInfo
			// 
			this.chkInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkInfo.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkInfo.AutoSize = true;
			this.chkInfo.Location = new System.Drawing.Point(493, 362);
			this.chkInfo.Name = "chkInfo";
			this.chkInfo.Size = new System.Drawing.Size(19, 23);
			this.chkInfo.TabIndex = 16;
			this.chkInfo.Text = "i";
			this.chkInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkInfo.UseVisualStyleBackColor = true;
			this.chkInfo.CheckedChanged += new System.EventHandler(this.chkInfo_CheckedChanged);
			// 
			// btnSelectReference
			// 
			this.btnSelectReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSelectReference.Location = new System.Drawing.Point(3, 362);
			this.btnSelectReference.Name = "btnSelectReference";
			this.btnSelectReference.Size = new System.Drawing.Size(103, 23);
			this.btnSelectReference.TabIndex = 13;
			this.btnSelectReference.Text = "Select Reference";
			this.btnSelectReference.UseVisualStyleBackColor = true;
			this.btnSelectReference.Click += new System.EventHandler(this.btnSelectReference_Click);
			// 
			// propGrid
			// 
			this.propGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propGrid.HelpVisible = false;
			this.propGrid.Location = new System.Drawing.Point(3, 32);
			this.propGrid.Name = "propGrid";
			this.propGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.propGrid.Size = new System.Drawing.Size(573, 324);
			this.propGrid.TabIndex = 1;
			this.propGrid.ToolbarVisible = false;
			this.propGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propGrid_SelectedGridItemChanged);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPrevious.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindPrevious_13244;
			this.btnPrevious.Location = new System.Drawing.Point(550, 362);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(26, 23);
			this.btnPrevious.TabIndex = 14;
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			// 
			// btnNext
			// 
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.Image = global::Myriadbits.MXFInspect.Properties.Resources.FindNext_13243;
			this.btnNext.Location = new System.Drawing.Point(518, 362);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(26, 23);
			this.btnNext.TabIndex = 14;
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// txtHex
			// 
			this.txtHex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtHex.Font = new System.Drawing.Font("Courier New", 9.75F);
			this.txtHex.HideSelection = false;
			this.txtHex.Location = new System.Drawing.Point(3, 3);
			this.txtHex.Name = "txtHex";
			this.txtHex.ReadOnly = true;
			this.txtHex.Size = new System.Drawing.Size(573, 150);
			this.txtHex.TabIndex = 16;
			this.txtHex.Text = "";
			// 
			// imageListResult
			// 
			this.imageListResult.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListResult.ImageStream")));
			this.imageListResult.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListResult.Images.SetKeyName(0, "Error");
			this.imageListResult.Images.SetKeyName(1, "Success");
			this.imageListResult.Images.SetKeyName(2, "Warning");
			this.imageListResult.Images.SetKeyName(3, "Info");
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// txtOverall
			// 
			this.txtOverall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOverall.Location = new System.Drawing.Point(3, 557);
			this.txtOverall.Multiline = true;
			this.txtOverall.Name = "txtOverall";
			this.txtOverall.ReadOnly = true;
			this.txtOverall.Size = new System.Drawing.Size(1105, 21);
			this.txtOverall.TabIndex = 11;
			// 
			// mainPanel
			// 
			this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.mainPanel.Controls.Add(this.prbProcessing);
			this.mainPanel.Controls.Add(this.txtOverall);
			this.mainPanel.Controls.Add(this.splitMain);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(1111, 581);
			this.mainPanel.TabIndex = 12;
			// 
			// prbProcessing
			// 
			this.prbProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.prbProcessing.Location = new System.Drawing.Point(3, 535);
			this.prbProcessing.Name = "prbProcessing";
			this.prbProcessing.Size = new System.Drawing.Size(1105, 18);
			this.prbProcessing.TabIndex = 16;
			// 
			// bgwProcess
			// 
			this.bgwProcess.WorkerReportsProgress = true;
			this.bgwProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcess_DoWork);
			this.bgwProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwProcess_ProgressChanged);
			this.bgwProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwProcess_RunWorkerCompleted);
			// 
			// MXFView
			// 
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(1111, 581);
			this.Controls.Add(this.mainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MXFView";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.TransparencyKey = System.Drawing.SystemColors.Window;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MXFView_Load);
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
			this.splitMain.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tpLocation.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListViewMain)).EndInit();
			this.tpLogical.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListViewLogical)).EndInit();
			this.splitRight.Panel1.ResumeLayout(false);
			this.splitRight.Panel1.PerformLayout();
			this.splitRight.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
			this.splitRight.ResumeLayout(false);
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.TextBox txtOverall;
		private System.Windows.Forms.PropertyGrid propGrid;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.Button btnSelectReference;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tpLocation;
		private BrightIdeasSoftware.TreeListView treeListViewMain;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private BrightIdeasSoftware.OLVColumn olvColumn2;
		private System.Windows.Forms.Panel mainPanel;
		private System.ComponentModel.BackgroundWorker bgwProcess;
		private System.Windows.Forms.ProgressBar prbProcessing;
		private System.Windows.Forms.ImageList imageListResult;
		private System.Windows.Forms.SplitContainer splitRight;
		private System.Windows.Forms.CheckBox chkInfo;
		private System.Windows.Forms.TabPage tpLogical;
		private BrightIdeasSoftware.TreeListView treeListViewLogical;
		private BrightIdeasSoftware.OLVColumn olvColumn3;
		private BrightIdeasSoftware.OLVColumn olvColumn4;
		private System.Windows.Forms.RichTextBox txtHex;
	}
}

