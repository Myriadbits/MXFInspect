namespace Myriadbits.MXFInspect
{
	partial class FormSettings
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
			this.bntOk = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.chkShowLines = new System.Windows.Forms.CheckBox();
			this.cmbTopNumber = new System.Windows.Forms.ComboBox();
			this.cmbThreshold = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.pbColorIndex = new System.Windows.Forms.PictureBox();
			this.pbColorEssence = new System.Windows.Forms.PictureBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pbColorPartition = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pbColorRIP = new System.Windows.Forms.PictureBox();
			this.label5 = new System.Windows.Forms.Label();
			this.pbColorSystemItem = new System.Windows.Forms.PictureBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.chkPartialLoadMsg = new System.Windows.Forms.CheckBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.pbColorMeta = new System.Windows.Forms.PictureBox();
			this.label10 = new System.Windows.Forms.Label();
			this.pbColorFiller = new System.Windows.Forms.PictureBox();
			this.label11 = new System.Windows.Forms.Label();
			this.pbColorSpecial = new System.Windows.Forms.PictureBox();
			this.label12 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbColorIndex)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorEssence)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorPartition)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorRIP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorSystemItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorMeta)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorFiller)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorSpecial)).BeginInit();
			this.SuspendLayout();
			// 
			// bntOk
			// 
			this.bntOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bntOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bntOk.Location = new System.Drawing.Point(356, 227);
			this.bntOk.Name = "bntOk";
			this.bntOk.Size = new System.Drawing.Size(75, 23);
			this.bntOk.TabIndex = 7;
			this.bntOk.Text = "Ok";
			this.bntOk.UseVisualStyleBackColor = true;
			this.bntOk.Click += new System.EventHandler(this.bntOk_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(13, 13);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(499, 208);
			this.tabControl1.TabIndex = 8;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.chkPartialLoadMsg);
			this.tabGeneral.Controls.Add(this.label9);
			this.tabGeneral.Controls.Add(this.chkShowLines);
			this.tabGeneral.Controls.Add(this.cmbTopNumber);
			this.tabGeneral.Controls.Add(this.cmbThreshold);
			this.tabGeneral.Controls.Add(this.label4);
			this.tabGeneral.Controls.Add(this.label1);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(491, 182);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// chkShowLines
			// 
			this.chkShowLines.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkShowLines.Location = new System.Drawing.Point(4, 151);
			this.chkShowLines.Name = "chkShowLines";
			this.chkShowLines.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkShowLines.Size = new System.Drawing.Size(214, 19);
			this.chkShowLines.TabIndex = 2;
			this.chkShowLines.Text = "Show grid lines in list";
			this.chkShowLines.UseVisualStyleBackColor = true;
			this.chkShowLines.Visible = false;
			// 
			// cmbTopNumber
			// 
			this.cmbTopNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTopNumber.Enabled = false;
			this.cmbTopNumber.FormattingEnabled = true;
			this.cmbTopNumber.Items.AddRange(new object[] {
            "10",
            "25",
            "50",
            "100",
            "150",
            "200",
            "250",
            "500",
            "750",
            "1000",
            "1500",
            "2000",
            "2500"});
			this.cmbTopNumber.Location = new System.Drawing.Point(204, 122);
			this.cmbTopNumber.Name = "cmbTopNumber";
			this.cmbTopNumber.Size = new System.Drawing.Size(133, 21);
			this.cmbTopNumber.TabIndex = 1;
			this.cmbTopNumber.Visible = false;
			// 
			// cmbThreshold
			// 
			this.cmbThreshold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbThreshold.FormattingEnabled = true;
			this.cmbThreshold.Items.AddRange(new object[] {
            "All files",
            "100 MB",
            "500 MB",
            "1 GB",
            "5 GB",
            "10 GB"});
			this.cmbThreshold.Location = new System.Drawing.Point(204, 9);
			this.cmbThreshold.Name = "cmbThreshold";
			this.cmbThreshold.Size = new System.Drawing.Size(133, 21);
			this.cmbThreshold.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Enabled = false;
			this.label4.Location = new System.Drawing.Point(6, 125);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(182, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Number of files to show in the top list:";
			this.label4.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Partial load threshold";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.pbColorSpecial);
			this.tabPage2.Controls.Add(this.label12);
			this.tabPage2.Controls.Add(this.pbColorFiller);
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.pbColorMeta);
			this.tabPage2.Controls.Add(this.label10);
			this.tabPage2.Controls.Add(this.pbColorSystemItem);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.pbColorRIP);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.pbColorIndex);
			this.tabPage2.Controls.Add(this.pbColorEssence);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.pbColorPartition);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(491, 182);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Colors";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// pbColorIndex
			// 
			this.pbColorIndex.BackColor = System.Drawing.Color.Red;
			this.pbColorIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorIndex.Location = new System.Drawing.Point(122, 44);
			this.pbColorIndex.Name = "pbColorIndex";
			this.pbColorIndex.Size = new System.Drawing.Size(112, 13);
			this.pbColorIndex.TabIndex = 3;
			this.pbColorIndex.TabStop = false;
			this.pbColorIndex.Click += new System.EventHandler(this.pbColorIndex_Click);
			// 
			// pbColorEssence
			// 
			this.pbColorEssence.BackColor = System.Drawing.Color.Red;
			this.pbColorEssence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorEssence.Location = new System.Drawing.Point(122, 25);
			this.pbColorEssence.Name = "pbColorEssence";
			this.pbColorEssence.Size = new System.Drawing.Size(112, 13);
			this.pbColorEssence.TabIndex = 3;
			this.pbColorEssence.TabStop = false;
			this.pbColorEssence.Click += new System.EventHandler(this.pbColorEssence_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label7.Location = new System.Drawing.Point(6, 166);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(194, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "Click on the colors to change the colors";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(85, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Index table color";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Essence color";
			// 
			// pbColorPartition
			// 
			this.pbColorPartition.BackColor = System.Drawing.Color.Red;
			this.pbColorPartition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorPartition.Location = new System.Drawing.Point(122, 6);
			this.pbColorPartition.Name = "pbColorPartition";
			this.pbColorPartition.Size = new System.Drawing.Size(112, 13);
			this.pbColorPartition.TabIndex = 1;
			this.pbColorPartition.TabStop = false;
			this.pbColorPartition.Click += new System.EventHandler(this.pbColorPartition_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Partition color";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(437, 227);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// pbColorRIP
			// 
			this.pbColorRIP.BackColor = System.Drawing.Color.Red;
			this.pbColorRIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorRIP.Location = new System.Drawing.Point(122, 63);
			this.pbColorRIP.Name = "pbColorRIP";
			this.pbColorRIP.Size = new System.Drawing.Size(112, 13);
			this.pbColorRIP.TabIndex = 5;
			this.pbColorRIP.TabStop = false;
			this.pbColorRIP.Click += new System.EventHandler(this.pbColorRIP_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 63);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "RIP color";
			// 
			// pbColorSystemItem
			// 
			this.pbColorSystemItem.BackColor = System.Drawing.Color.Red;
			this.pbColorSystemItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorSystemItem.Location = new System.Drawing.Point(122, 82);
			this.pbColorSystemItem.Name = "pbColorSystemItem";
			this.pbColorSystemItem.Size = new System.Drawing.Size(112, 13);
			this.pbColorSystemItem.TabIndex = 7;
			this.pbColorSystemItem.TabStop = false;
			this.pbColorSystemItem.Click += new System.EventHandler(this.pbColorSystemItem_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(3, 82);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(90, 13);
			this.label8.TabIndex = 6;
			this.label8.Text = "System Item color";
			// 
			// label9
			// 
			this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label9.Location = new System.Drawing.Point(6, 58);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(479, 35);
			this.label9.TabIndex = 3;
			this.label9.Text = "Files that are larger than the threshold will be partially loaded. A partition wi" +
    "ll be loaded when it is expanded in the tree.";
			// 
			// chkPartialLoadMsg
			// 
			this.chkPartialLoadMsg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkPartialLoadMsg.Location = new System.Drawing.Point(4, 36);
			this.chkPartialLoadMsg.Name = "chkPartialLoadMsg";
			this.chkPartialLoadMsg.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkPartialLoadMsg.Size = new System.Drawing.Size(214, 19);
			this.chkPartialLoadMsg.TabIndex = 4;
			this.chkPartialLoadMsg.Text = "Show partial loading message";
			this.chkPartialLoadMsg.UseVisualStyleBackColor = true;
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(13, 227);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(112, 23);
			this.btnReset.TabIndex = 8;
			this.btnReset.Text = "Reset to default";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// pbColorMeta
			// 
			this.pbColorMeta.BackColor = System.Drawing.Color.Red;
			this.pbColorMeta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorMeta.Location = new System.Drawing.Point(122, 101);
			this.pbColorMeta.Name = "pbColorMeta";
			this.pbColorMeta.Size = new System.Drawing.Size(112, 13);
			this.pbColorMeta.TabIndex = 9;
			this.pbColorMeta.TabStop = false;
			this.pbColorMeta.Click += new System.EventHandler(this.pbColorMeta_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 101);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(81, 13);
			this.label10.TabIndex = 8;
			this.label10.Text = "Meta data color";
			// 
			// pbColorFiller
			// 
			this.pbColorFiller.BackColor = System.Drawing.Color.Red;
			this.pbColorFiller.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorFiller.Location = new System.Drawing.Point(122, 120);
			this.pbColorFiller.Name = "pbColorFiller";
			this.pbColorFiller.Size = new System.Drawing.Size(112, 13);
			this.pbColorFiller.TabIndex = 11;
			this.pbColorFiller.TabStop = false;
			this.pbColorFiller.Click += new System.EventHandler(this.pbColorFiller_Click);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(3, 120);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(54, 13);
			this.label11.TabIndex = 10;
			this.label11.Text = "Filler color";
			// 
			// pbColorSpecial
			// 
			this.pbColorSpecial.BackColor = System.Drawing.Color.Red;
			this.pbColorSpecial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbColorSpecial.Location = new System.Drawing.Point(122, 141);
			this.pbColorSpecial.Name = "pbColorSpecial";
			this.pbColorSpecial.Size = new System.Drawing.Size(112, 13);
			this.pbColorSpecial.TabIndex = 13;
			this.pbColorSpecial.TabStop = false;
			this.pbColorSpecial.Click += new System.EventHandler(this.pbColorSpecial_Click);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(3, 141);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(68, 13);
			this.label12.TabIndex = 12;
			this.label12.Text = "Special color";
			// 
			// FormSettings
			// 
			this.AcceptButton = this.bntOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(524, 262);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.bntOk);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.FormSettings_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbColorIndex)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorEssence)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorPartition)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorRIP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorSystemItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorMeta)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorFiller)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbColorSpecial)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bntOk;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.ComboBox cmbThreshold;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.PictureBox pbColorPartition;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.PictureBox pbColorEssence;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkShowLines;
		private System.Windows.Forms.PictureBox pbColorIndex;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbTopNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pbColorRIP;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox pbColorSystemItem;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkPartialLoadMsg;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.PictureBox pbColorSpecial;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.PictureBox pbColorFiller;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.PictureBox pbColorMeta;
		private System.Windows.Forms.Label label10;
	}
}