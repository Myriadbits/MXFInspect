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
            this.tlvResults = new BrightIdeasSoftware.TreeListView();
            this.colSeverity = new BrightIdeasSoftware.OLVColumn();
            this.colCategory = new BrightIdeasSoftware.OLVColumn();
            this.colResult = new BrightIdeasSoftware.OLVColumn();
            this.imageListResult = new System.Windows.Forms.ImageList(this.components);
            this.txtGeneralInfo = new System.Windows.Forms.TextBox();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.btnExecuteAllTests = new System.Windows.Forms.Button();
            this.prbProcessing = new System.Windows.Forms.ProgressBar();
            this.colOffset = new BrightIdeasSoftware.OLVColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tlvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(768, 405);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 27);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tlvResults
            // 
            this.tlvResults.AllColumns.Add(this.colSeverity);
            this.tlvResults.AllColumns.Add(this.colOffset);
            this.tlvResults.AllColumns.Add(this.colCategory);
            this.tlvResults.AllColumns.Add(this.colResult);
            this.tlvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSeverity,
            this.colOffset,
            this.colCategory,
            this.colResult});
            this.tlvResults.FullRowSelect = true;
            this.tlvResults.GridLines = true;
            this.tlvResults.Location = new System.Drawing.Point(8, 84);
            this.tlvResults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tlvResults.Name = "tlvResults";
            this.tlvResults.RowHeight = 19;
            this.tlvResults.ShowGroups = false;
            this.tlvResults.ShowItemToolTips = true;
            this.tlvResults.Size = new System.Drawing.Size(850, 253);
            this.tlvResults.SmallImageList = this.imageListResult;
            this.tlvResults.TabIndex = 19;
            this.tlvResults.UseCompatibleStateImageBehavior = false;
            this.tlvResults.View = System.Windows.Forms.View.Details;
            this.tlvResults.VirtualMode = true;
            this.tlvResults.SelectionChanged += new System.EventHandler(this.tlvResults_SelectionChanged);
            // 
            // colSeverity
            // 
            this.colSeverity.AspectName = "Severity";
            this.colSeverity.Text = "Severity";
            this.colSeverity.Width = 40;
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
            // txtGeneralInfo
            // 
            this.txtGeneralInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGeneralInfo.Location = new System.Drawing.Point(9, 7);
            this.txtGeneralInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtGeneralInfo.Multiline = true;
            this.txtGeneralInfo.Name = "txtGeneralInfo";
            this.txtGeneralInfo.ReadOnly = true;
            this.txtGeneralInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGeneralInfo.Size = new System.Drawing.Size(849, 73);
            this.txtGeneralInfo.TabIndex = 20;
            // 
            // txtSum
            // 
            this.txtSum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSum.Location = new System.Drawing.Point(9, 357);
            this.txtSum.Margin = new System.Windows.Forms.Padding(4);
            this.txtSum.MinimumSize = new System.Drawing.Size(4, 17);
            this.txtSum.Multiline = true;
            this.txtSum.Name = "txtSum";
            this.txtSum.ReadOnly = true;
            this.txtSum.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSum.Size = new System.Drawing.Size(849, 43);
            this.txtSum.TabIndex = 21;
            // 
            // btnExecuteAllTests
            // 
            this.btnExecuteAllTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteAllTests.Location = new System.Drawing.Point(635, 405);
            this.btnExecuteAllTests.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExecuteAllTests.Name = "btnExecuteAllTests";
            this.btnExecuteAllTests.Size = new System.Drawing.Size(125, 27);
            this.btnExecuteAllTests.TabIndex = 22;
            this.btnExecuteAllTests.Text = "Execute All Tests";
            this.btnExecuteAllTests.UseVisualStyleBackColor = true;
            this.btnExecuteAllTests.Click += new System.EventHandler(this.btnExecuteAllTests_Click);
            // 
            // prbProcessing
            // 
            this.prbProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbProcessing.Location = new System.Drawing.Point(8, 343);
            this.prbProcessing.Margin = new System.Windows.Forms.Padding(4);
            this.prbProcessing.Name = "prbProcessing";
            this.prbProcessing.Size = new System.Drawing.Size(848, 9);
            this.prbProcessing.TabIndex = 23;
            this.prbProcessing.Visible = false;
            // 
            // colOffset
            // 
            this.colOffset.AspectName = "Offset";
            this.colOffset.Text = "Offset";
            this.colOffset.Width = 80;
            // 
            // FormReport
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(864, 439);
            this.Controls.Add(this.prbProcessing);
            this.Controls.Add(this.btnExecuteAllTests);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.txtGeneralInfo);
            this.Controls.Add(this.tlvResults);
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
            ((System.ComponentModel.ISupportInitialize)(this.tlvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
		private BrightIdeasSoftware.TreeListView tlvResults;
		private BrightIdeasSoftware.OLVColumn colSeverity;
        private BrightIdeasSoftware.OLVColumn colOffset;
        private BrightIdeasSoftware.OLVColumn colCategory;
		private BrightIdeasSoftware.OLVColumn colResult;
		private System.Windows.Forms.TextBox txtGeneralInfo;
		private System.Windows.Forms.ImageList imageListResult;
		private System.Windows.Forms.TextBox txtSum;
		private System.Windows.Forms.Button btnExecuteAllTests;
		private System.Windows.Forms.ProgressBar prbProcessing;
    }
}