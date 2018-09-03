namespace MXFInspect
{
	partial class Main
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
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.treeMain = new System.Windows.Forms.TreeView();
			this.propGrid = new System.Windows.Forms.PropertyGrid();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.btnProcess = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.txtOverall = new System.Windows.Forms.TextBox();
			this.chkHideFillers = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitMain
			// 
			this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitMain.Location = new System.Drawing.Point(4, 67);
			this.splitMain.Name = "splitMain";
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(this.treeMain);
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.propGrid);
			this.splitMain.Size = new System.Drawing.Size(640, 323);
			this.splitMain.SplitterDistance = 309;
			this.splitMain.TabIndex = 0;
			// 
			// treeMain
			// 
			this.treeMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeMain.Location = new System.Drawing.Point(4, 4);
			this.treeMain.Name = "treeMain";
			this.treeMain.Size = new System.Drawing.Size(302, 316);
			this.treeMain.TabIndex = 0;
			this.treeMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMain_AfterSelect);
			// 
			// propGrid
			// 
			this.propGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propGrid.CommandsVisibleIfAvailable = false;
			this.propGrid.HelpVisible = false;
			this.propGrid.Location = new System.Drawing.Point(4, 4);
			this.propGrid.Name = "propGrid";
			this.propGrid.Size = new System.Drawing.Size(320, 316);
			this.propGrid.TabIndex = 1;
			this.propGrid.ToolbarVisible = false;
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(68, 12);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(544, 20);
			this.txtPath.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "File path:";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(618, 10);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(26, 23);
			this.btnBrowse.TabIndex = 7;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point(4, 38);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(103, 23);
			this.btnProcess.TabIndex = 8;
			this.btnProcess.Text = "Inspect";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 455);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(648, 22);
			this.statusStrip1.TabIndex = 9;
			this.statusStrip1.Text = "statusStrip1";
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
			this.txtOverall.Location = new System.Drawing.Point(4, 397);
			this.txtOverall.Multiline = true;
			this.txtOverall.Name = "txtOverall";
			this.txtOverall.ReadOnly = true;
			this.txtOverall.Size = new System.Drawing.Size(640, 55);
			this.txtOverall.TabIndex = 11;
			// 
			// chkHideFillers
			// 
			this.chkHideFillers.AutoSize = true;
			this.chkHideFillers.Checked = true;
			this.chkHideFillers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHideFillers.Location = new System.Drawing.Point(113, 42);
			this.chkHideFillers.Name = "chkHideFillers";
			this.chkHideFillers.Size = new System.Drawing.Size(77, 17);
			this.chkHideFillers.TabIndex = 12;
			this.chkHideFillers.Text = "Hide Fillers";
			this.chkHideFillers.UseVisualStyleBackColor = true;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 477);
			this.Controls.Add(this.chkHideFillers);
			this.Controls.Add(this.txtOverall);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.splitMain);
			this.Name = "Main";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "MXFInspect";
			this.Load += new System.EventHandler(this.Main_Load);
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
			this.splitMain.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.TreeView treeMain;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.TextBox txtOverall;
		private System.Windows.Forms.PropertyGrid propGrid;
		private System.Windows.Forms.CheckBox chkHideFillers;
	}
}

