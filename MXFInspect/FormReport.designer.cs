namespace Myriadbits.MXFInspect
{
	partial class FormReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReport));
            this.btnClose = new System.Windows.Forms.Button();
            this.tlvValidationResults = new BrightIdeasSoftware.TreeListView();
            this.colSeverity = new BrightIdeasSoftware.OLVColumn();
            this.colOffset = new BrightIdeasSoftware.OLVColumn();
            this.colCategory = new BrightIdeasSoftware.OLVColumn();
            this.colResult = new BrightIdeasSoftware.OLVColumn();
            this.imageListResult = new System.Windows.Forms.ImageList(this.components);
            this.btnExecuteAllTests = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabValidationReport = new System.Windows.Forms.TabPage();
            this.prbProcessing = new System.Windows.Forms.ProgressBar();
            this.tabQuickInfo = new System.Windows.Forms.TabPage();
            this.olvQuickInfo = new BrightIdeasSoftware.ObjectListView();
            this.colProperty = new BrightIdeasSoftware.OLVColumn();
            this.colValue = new BrightIdeasSoftware.OLVColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tlvValidationResults)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabValidationReport.SuspendLayout();
            this.tabQuickInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvQuickInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(744, 431);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 27);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tlvValidationResults
            // 
            this.tlvValidationResults.AllColumns.Add(this.colSeverity);
            this.tlvValidationResults.AllColumns.Add(this.colOffset);
            this.tlvValidationResults.AllColumns.Add(this.colCategory);
            this.tlvValidationResults.AllColumns.Add(this.colResult);
            this.tlvValidationResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlvValidationResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSeverity,
            this.colOffset,
            this.colCategory,
            this.colResult});
            this.tlvValidationResults.FullRowSelect = true;
            this.tlvValidationResults.GridLines = true;
            this.tlvValidationResults.Location = new System.Drawing.Point(4, 6);
            this.tlvValidationResults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tlvValidationResults.Name = "tlvValidationResults";
            this.tlvValidationResults.RowHeight = 19;
            this.tlvValidationResults.ShowGroups = false;
            this.tlvValidationResults.ShowItemToolTips = true;
            this.tlvValidationResults.Size = new System.Drawing.Size(806, 361);
            this.tlvValidationResults.SmallImageList = this.imageListResult;
            this.tlvValidationResults.TabIndex = 19;
            this.tlvValidationResults.UseCompatibleStateImageBehavior = false;
            this.tlvValidationResults.View = System.Windows.Forms.View.Details;
            this.tlvValidationResults.VirtualMode = true;
            this.tlvValidationResults.SelectionChanged += new System.EventHandler(this.tlvResults_SelectionChanged);
            // 
            // colSeverity
            // 
            this.colSeverity.AspectName = "Severity";
            this.colSeverity.Text = "Severity";
            this.colSeverity.Width = 40;
            // 
            // colOffset
            // 
            this.colOffset.AspectName = "Offset";
            this.colOffset.Text = "Offset";
            this.colOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOffset.Width = 80;
            // 
            // colCategory
            // 
            this.colCategory.AspectName = "Category";
            this.colCategory.Text = "Category";
            this.colCategory.Width = 114;
            // 
            // colResult
            // 
            this.colResult.AspectName = "Result";
            this.colResult.FillsFreeSpace = true;
            this.colResult.Text = "Result";
            this.colResult.WordWrap = true;
            // 
            // imageListResult
            // 
            this.imageListResult.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListResult.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListResult.ImageStream")));
            this.imageListResult.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListResult.Images.SetKeyName(0, "Error");
            this.imageListResult.Images.SetKeyName(1, "Success");
            this.imageListResult.Images.SetKeyName(2, "Warning");
            this.imageListResult.Images.SetKeyName(3, "Info");
            this.imageListResult.Images.SetKeyName(4, "Question");
            // 
            // btnExecuteAllTests
            // 
            this.btnExecuteAllTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteAllTests.Location = new System.Drawing.Point(611, 431);
            this.btnExecuteAllTests.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExecuteAllTests.Name = "btnExecuteAllTests";
            this.btnExecuteAllTests.Size = new System.Drawing.Size(125, 27);
            this.btnExecuteAllTests.TabIndex = 22;
            this.btnExecuteAllTests.Text = "Execute All Tests";
            this.btnExecuteAllTests.UseVisualStyleBackColor = true;
            this.btnExecuteAllTests.Click += new System.EventHandler(this.btnExecuteAllTests_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabValidationReport);
            this.tabControl.Controls.Add(this.tabQuickInfo);
            this.tabControl.Location = new System.Drawing.Point(7, 7);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(825, 418);
            this.tabControl.TabIndex = 24;
            // 
            // tabValidationReport
            // 
            this.tabValidationReport.Controls.Add(this.tlvValidationResults);
            this.tabValidationReport.Controls.Add(this.prbProcessing);
            this.tabValidationReport.Location = new System.Drawing.Point(4, 24);
            this.tabValidationReport.Name = "tabValidationReport";
            this.tabValidationReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabValidationReport.Size = new System.Drawing.Size(817, 390);
            this.tabValidationReport.TabIndex = 0;
            this.tabValidationReport.Text = "Validation Report";
            this.tabValidationReport.UseVisualStyleBackColor = true;
            // 
            // prbProcessing
            // 
            this.prbProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbProcessing.Location = new System.Drawing.Point(4, 374);
            this.prbProcessing.Margin = new System.Windows.Forms.Padding(4);
            this.prbProcessing.Name = "prbProcessing";
            this.prbProcessing.Size = new System.Drawing.Size(806, 12);
            this.prbProcessing.TabIndex = 31;
            this.prbProcessing.Visible = false;
            // 
            // tabQuickInfo
            // 
            this.tabQuickInfo.Controls.Add(this.olvQuickInfo);
            this.tabQuickInfo.Location = new System.Drawing.Point(4, 24);
            this.tabQuickInfo.Name = "tabQuickInfo";
            this.tabQuickInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuickInfo.Size = new System.Drawing.Size(817, 390);
            this.tabQuickInfo.TabIndex = 1;
            this.tabQuickInfo.Text = "Quick Info";
            this.tabQuickInfo.UseVisualStyleBackColor = true;
            // 
            // olvQuickInfo
            // 
            this.olvQuickInfo.AllColumns.Add(this.colSeverity);
            this.olvQuickInfo.AllColumns.Add(this.colOffset);
            this.olvQuickInfo.AllColumns.Add(this.colCategory);
            this.olvQuickInfo.AllColumns.Add(this.colResult);
            this.olvQuickInfo.AlternateRowBackColor = System.Drawing.SystemColors.Control;
            this.olvQuickInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvQuickInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProperty,
            this.colValue});
            this.olvQuickInfo.FullRowSelect = true;
            this.olvQuickInfo.GridLines = true;
            this.olvQuickInfo.Location = new System.Drawing.Point(7, 6);
            this.olvQuickInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.olvQuickInfo.Name = "olvQuickInfo";
            this.olvQuickInfo.RowHeight = 19;
            this.olvQuickInfo.ShowGroups = false;
            this.olvQuickInfo.ShowItemToolTips = true;
            this.olvQuickInfo.Size = new System.Drawing.Size(803, 378);
            this.olvQuickInfo.SmallImageList = this.imageListResult;
            this.olvQuickInfo.TabIndex = 31;
            this.olvQuickInfo.UseAlternatingBackColors = true;
            this.olvQuickInfo.UseCompatibleStateImageBehavior = false;
            this.olvQuickInfo.View = System.Windows.Forms.View.Details;
            // 
            // colProperty
            // 
            this.colProperty.AspectName = "Key";
            this.colProperty.IsEditable = false;
            this.colProperty.Text = "Property";
            // 
            // colValue
            // 
            this.colValue.AspectName = "Value";
            this.colValue.FillsFreeSpace = true;
            this.colValue.Text = "Value";
            // 
            // FormReport
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(840, 465);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnExecuteAllTests);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReport";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tlvValidationResults)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabValidationReport.ResumeLayout(false);
            this.tabQuickInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvQuickInfo)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
        private BrightIdeasSoftware.TreeListView tlvValidationResults;
        private BrightIdeasSoftware.OLVColumn colSeverity;
        private BrightIdeasSoftware.OLVColumn colOffset;
        private BrightIdeasSoftware.OLVColumn colCategory;
        private BrightIdeasSoftware.OLVColumn colResult;
        private System.Windows.Forms.ImageList imageListResult;
        private System.Windows.Forms.Button btnExecuteAllTests;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabValidationReport;
        private System.Windows.Forms.TabPage tabQuickInfo;
        private System.Windows.Forms.ProgressBar prbProcessing;
        private BrightIdeasSoftware.ObjectListView olvQuickInfo;
        private BrightIdeasSoftware.OLVColumn colProperty;
        private BrightIdeasSoftware.OLVColumn colValue;
    }
}