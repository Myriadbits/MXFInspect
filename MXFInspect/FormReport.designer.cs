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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReport));
            btnClose = new System.Windows.Forms.Button();
            tlvValidationResults = new BrightIdeasSoftware.TreeListView();
            colSeverity = new BrightIdeasSoftware.OLVColumn();
            colOffset = new BrightIdeasSoftware.OLVColumn();
            colCategory = new BrightIdeasSoftware.OLVColumn();
            colResult = new BrightIdeasSoftware.OLVColumn();
            imageListResult = new System.Windows.Forms.ImageList(components);
            tabControl = new System.Windows.Forms.TabControl();
            tabValidationReport = new System.Windows.Forms.TabPage();
            prbProcessing = new System.Windows.Forms.ProgressBar();
            tabQuickInfo = new System.Windows.Forms.TabPage();
            olvQuickInfo = new BrightIdeasSoftware.ObjectListView();
            colProperty = new BrightIdeasSoftware.OLVColumn();
            colValue = new BrightIdeasSoftware.OLVColumn();
            ((System.ComponentModel.ISupportInitialize)tlvValidationResults).BeginInit();
            tabControl.SuspendLayout();
            tabValidationReport.SuspendLayout();
            tabQuickInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)olvQuickInfo).BeginInit();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnClose.Location = new System.Drawing.Point(744, 431);
            btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(88, 27);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // tlvValidationResults
            // 
            tlvValidationResults.AllColumns.Add(colSeverity);
            tlvValidationResults.AllColumns.Add(colOffset);
            tlvValidationResults.AllColumns.Add(colCategory);
            tlvValidationResults.AllColumns.Add(colResult);
            tlvValidationResults.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tlvValidationResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colSeverity, colOffset, colCategory, colResult });
            tlvValidationResults.FullRowSelect = true;
            tlvValidationResults.GridLines = true;
            tlvValidationResults.Location = new System.Drawing.Point(4, 6);
            tlvValidationResults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tlvValidationResults.Name = "tlvValidationResults";
            tlvValidationResults.RowHeight = 19;
            tlvValidationResults.ShowGroups = false;
            tlvValidationResults.ShowItemToolTips = true;
            tlvValidationResults.Size = new System.Drawing.Size(806, 361);
            tlvValidationResults.SmallImageList = imageListResult;
            tlvValidationResults.TabIndex = 19;
            tlvValidationResults.UseCompatibleStateImageBehavior = false;
            tlvValidationResults.View = System.Windows.Forms.View.Details;
            tlvValidationResults.VirtualMode = true;
            tlvValidationResults.SelectionChanged += tlvResults_SelectionChanged;
            // 
            // colSeverity
            // 
            colSeverity.AspectName = "Severity";
            colSeverity.Text = "Severity";
            colSeverity.Width = 40;
            // 
            // colOffset
            // 
            colOffset.AspectName = "Offset";
            colOffset.Text = "Offset";
            colOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            colOffset.Width = 80;
            // 
            // colCategory
            // 
            colCategory.AspectName = "Category";
            colCategory.Text = "Category";
            colCategory.Width = 114;
            // 
            // colResult
            // 
            colResult.AspectName = "Message";
            colResult.FillsFreeSpace = true;
            colResult.Text = "Result";
            colResult.WordWrap = true;
            // 
            // imageListResult
            // 
            imageListResult.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageListResult.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageListResult.ImageStream");
            imageListResult.TransparentColor = System.Drawing.Color.Transparent;
            imageListResult.Images.SetKeyName(0, "Error");
            imageListResult.Images.SetKeyName(1, "Success");
            imageListResult.Images.SetKeyName(2, "Warning");
            imageListResult.Images.SetKeyName(3, "Info");
            imageListResult.Images.SetKeyName(4, "Question");
            // 
            // tabControl
            // 
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl.Controls.Add(tabValidationReport);
            tabControl.Controls.Add(tabQuickInfo);
            tabControl.Location = new System.Drawing.Point(7, 7);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(825, 418);
            tabControl.TabIndex = 24;
            // 
            // tabValidationReport
            // 
            tabValidationReport.Controls.Add(tlvValidationResults);
            tabValidationReport.Controls.Add(prbProcessing);
            tabValidationReport.Location = new System.Drawing.Point(4, 24);
            tabValidationReport.Name = "tabValidationReport";
            tabValidationReport.Padding = new System.Windows.Forms.Padding(3);
            tabValidationReport.Size = new System.Drawing.Size(817, 390);
            tabValidationReport.TabIndex = 0;
            tabValidationReport.Text = "Validation Report";
            tabValidationReport.UseVisualStyleBackColor = true;
            // 
            // prbProcessing
            // 
            prbProcessing.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            prbProcessing.Location = new System.Drawing.Point(4, 374);
            prbProcessing.Margin = new System.Windows.Forms.Padding(4);
            prbProcessing.Name = "prbProcessing";
            prbProcessing.Size = new System.Drawing.Size(806, 12);
            prbProcessing.TabIndex = 31;
            prbProcessing.Visible = false;
            // 
            // tabQuickInfo
            // 
            tabQuickInfo.Controls.Add(olvQuickInfo);
            tabQuickInfo.Location = new System.Drawing.Point(4, 24);
            tabQuickInfo.Name = "tabQuickInfo";
            tabQuickInfo.Padding = new System.Windows.Forms.Padding(3);
            tabQuickInfo.Size = new System.Drawing.Size(817, 390);
            tabQuickInfo.TabIndex = 1;
            tabQuickInfo.Text = "Quick Info";
            tabQuickInfo.UseVisualStyleBackColor = true;
            // 
            // olvQuickInfo
            // 
            olvQuickInfo.AllColumns.Add(colSeverity);
            olvQuickInfo.AllColumns.Add(colOffset);
            olvQuickInfo.AllColumns.Add(colCategory);
            olvQuickInfo.AllColumns.Add(colResult);
            olvQuickInfo.AlternateRowBackColor = System.Drawing.SystemColors.Control;
            olvQuickInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            olvQuickInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colProperty, colValue });
            olvQuickInfo.FullRowSelect = true;
            olvQuickInfo.GridLines = true;
            olvQuickInfo.Location = new System.Drawing.Point(7, 6);
            olvQuickInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            olvQuickInfo.Name = "olvQuickInfo";
            olvQuickInfo.RowHeight = 19;
            olvQuickInfo.ShowGroups = false;
            olvQuickInfo.ShowItemToolTips = true;
            olvQuickInfo.Size = new System.Drawing.Size(803, 378);
            olvQuickInfo.SmallImageList = imageListResult;
            olvQuickInfo.TabIndex = 31;
            olvQuickInfo.UseAlternatingBackColors = true;
            olvQuickInfo.UseCompatibleStateImageBehavior = false;
            olvQuickInfo.View = System.Windows.Forms.View.Details;
            // 
            // colProperty
            // 
            colProperty.AspectName = "Key";
            colProperty.IsEditable = false;
            colProperty.Text = "Property";
            // 
            // colValue
            // 
            colValue.AspectName = "Value";
            colValue.FillsFreeSpace = true;
            colValue.Text = "Value";
            // 
            // FormReport
            // 
            AcceptButton = btnClose;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new System.Drawing.Size(840, 465);
            Controls.Add(tabControl);
            Controls.Add(btnClose);
            Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormReport";
            Padding = new System.Windows.Forms.Padding(4);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Report";
            FormClosing += FormReport_FormClosing;
            Load += ReportForm_Load;
            ((System.ComponentModel.ISupportInitialize)tlvValidationResults).EndInit();
            tabControl.ResumeLayout(false);
            tabValidationReport.ResumeLayout(false);
            tabQuickInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)olvQuickInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private BrightIdeasSoftware.TreeListView tlvValidationResults;
        private BrightIdeasSoftware.OLVColumn colSeverity;
        private BrightIdeasSoftware.OLVColumn colOffset;
        private BrightIdeasSoftware.OLVColumn colCategory;
        private BrightIdeasSoftware.OLVColumn colResult;
        private System.Windows.Forms.ImageList imageListResult;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabValidationReport;
        private System.Windows.Forms.TabPage tabQuickInfo;
        private System.Windows.Forms.ProgressBar prbProcessing;
        private BrightIdeasSoftware.ObjectListView olvQuickInfo;
        private BrightIdeasSoftware.OLVColumn colProperty;
        private BrightIdeasSoftware.OLVColumn colValue;
    }
}