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
            this.tpPhysical = new System.Windows.Forms.TabPage();
            this.tlvPhysical = new Myriadbits.MXFInspect.PhysicalTreeListView();
            this.tpLogical = new System.Windows.Forms.TabPage();
            this.tlvLogical = new Myriadbits.MXFInspect.LogicalTreeListView();
            this.splitRight = new System.Windows.Forms.SplitContainer();
            this.propGrid = new Myriadbits.MXFInspect.ReadOnlyPropertyGrid();
            this.rtfHexViewer = new Myriadbits.MXFInspect.HexViewer();
            this.txtOverall = new System.Windows.Forms.TextBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.prbProcessing = new System.Windows.Forms.ProgressBar();
            this.bgwProcess = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpPhysical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvPhysical)).BeginInit();
            this.tpLogical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvLogical)).BeginInit();
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
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tpPhysical);
            this.tabMain.Controls.Add(this.tpLogical);
            this.tabMain.Location = new System.Drawing.Point(4, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(523, 545);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tpPhysical
            // 
            this.tpPhysical.Controls.Add(this.tlvPhysical);
            this.tpPhysical.Location = new System.Drawing.Point(4, 24);
            this.tpPhysical.Name = "tpPhysical";
            this.tpPhysical.Padding = new System.Windows.Forms.Padding(3);
            this.tpPhysical.Size = new System.Drawing.Size(515, 517);
            this.tpPhysical.TabIndex = 0;
            this.tpPhysical.Text = "Physical";
            this.tpPhysical.UseVisualStyleBackColor = true;
            // 
            // tlvPhysical
            // 
            this.tlvPhysical.AlternateRowBackColor = System.Drawing.Color.WhiteSmoke;
            this.tlvPhysical.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlvPhysical.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvPhysical.EmptyListMsg = "No items present";
            this.tlvPhysical.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tlvPhysical.FullRowSelect = true;
            this.tlvPhysical.HideSelection = false;
            this.tlvPhysical.Location = new System.Drawing.Point(6, 6);
            this.tlvPhysical.MultiSelect = false;
            this.tlvPhysical.Name = "tlvPhysical";
            this.tlvPhysical.RowHeight = 19;
            this.tlvPhysical.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.tlvPhysical.ShowGroups = false;
            this.tlvPhysical.Size = new System.Drawing.Size(503, 505);
            this.tlvPhysical.TabIndex = 15;
            this.tlvPhysical.TintSortColumn = true;
            this.tlvPhysical.UnfocusedSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.tlvPhysical.UseAlternatingBackColors = true;
            this.tlvPhysical.UseCellFormatEvents = true;
            this.tlvPhysical.UseCompatibleStateImageBehavior = false;
            this.tlvPhysical.UseFiltering = true;
            this.tlvPhysical.UseHyperlinks = true;
            this.tlvPhysical.View = System.Windows.Forms.View.Details;
            this.tlvPhysical.VirtualMode = true;
            // 
            // tpLogical
            // 
            this.tpLogical.Controls.Add(this.tlvLogical);
            this.tpLogical.Location = new System.Drawing.Point(4, 24);
            this.tpLogical.Name = "tpLogical";
            this.tpLogical.Padding = new System.Windows.Forms.Padding(3);
            this.tpLogical.Size = new System.Drawing.Size(515, 517);
            this.tpLogical.TabIndex = 1;
            this.tpLogical.Text = "Logical";
            this.tpLogical.UseVisualStyleBackColor = true;
            // 
            // tlvLogical
            // 
            this.tlvLogical.AlternateRowBackColor = System.Drawing.Color.WhiteSmoke;
            this.tlvLogical.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlvLogical.EmptyListMsg = "No items present";
            this.tlvLogical.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tlvLogical.FullRowSelect = true;
            this.tlvLogical.HideSelection = false;
            this.tlvLogical.Location = new System.Drawing.Point(6, 6);
            this.tlvLogical.MultiSelect = false;
            this.tlvLogical.Name = "tlvLogical";
            this.tlvLogical.RowHeight = 19;
            this.tlvLogical.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.tlvLogical.ShowGroups = false;
            this.tlvLogical.Size = new System.Drawing.Size(503, 505);
            this.tlvLogical.TabIndex = 16;
            this.tlvLogical.TintSortColumn = true;
            this.tlvLogical.UnfocusedSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.tlvLogical.UseAlternatingBackColors = true;
            this.tlvLogical.UseCellFormatEvents = true;
            this.tlvLogical.UseCompatibleStateImageBehavior = false;
            this.tlvLogical.UseFiltering = true;
            this.tlvLogical.View = System.Windows.Forms.View.Details;
            this.tlvLogical.VirtualMode = true;
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
            this.splitRight.Panel1.Controls.Add(this.propGrid);
            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.rtfHexViewer);
            this.splitRight.Size = new System.Drawing.Size(574, 549);
            this.splitRight.SplitterDistance = 388;
            this.splitRight.SplitterWidth = 6;
            this.splitRight.TabIndex = 16;
            // 
            // propGrid
            // 
            this.propGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(1)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.propGrid.HelpVisible = false;
            this.propGrid.Location = new System.Drawing.Point(3, 26);
            this.propGrid.Name = "propGrid";
            this.propGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propGrid.ReadOnly = true;
            this.propGrid.Size = new System.Drawing.Size(565, 359);
            this.propGrid.TabIndex = 1;
            this.propGrid.ToolbarVisible = false;
            this.propGrid.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // rtfHexViewer
            // 
            this.rtfHexViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfHexViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtfHexViewer.BytesPerLine = 16;
            this.rtfHexViewer.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtfHexViewer.HideSelection = false;
            this.rtfHexViewer.Location = new System.Drawing.Point(3, 3);
            this.rtfHexViewer.DisplayableBytesThreshold = ((long)(1000000));
            this.rtfHexViewer.Name = "rtfHexViewer";
            this.rtfHexViewer.ReadOnly = true;
            this.rtfHexViewer.Size = new System.Drawing.Size(565, 142);
            this.rtfHexViewer.TabIndex = 16;
            this.rtfHexViewer.Text = "";
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
            this.tpPhysical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvPhysical)).EndInit();
            this.tpLogical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvLogical)).EndInit();
            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
            this.splitRight.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.TextBox txtOverall;
		private MXFInspect.ReadOnlyPropertyGrid propGrid;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tpPhysical;
		private System.Windows.Forms.Panel mainPanel;
		private System.ComponentModel.BackgroundWorker bgwProcess;
		private System.Windows.Forms.ProgressBar prbProcessing;
		private System.Windows.Forms.SplitContainer splitRight;
		private System.Windows.Forms.TabPage tpLogical;
		private HexViewer rtfHexViewer;
        private PhysicalTreeListView tlvPhysical;
        private LogicalTreeListView tlvLogical;
    }
}

