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
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.chkOffsetAsHex = new System.Windows.Forms.CheckBox();
            this.chkPartialLoadMsg = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkShowLines = new System.Windows.Forms.CheckBox();
            this.cmbTopNumber = new System.Windows.Forms.ComboBox();
            this.cmbThreshold = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pbColorReference = new System.Windows.Forms.PictureBox();
            this.lblColorSpecial = new System.Windows.Forms.Label();
            this.pbColorSpecial = new System.Windows.Forms.PictureBox();
            this.lblColorReference = new System.Windows.Forms.Label();
            this.pbColorFiller = new System.Windows.Forms.PictureBox();
            this.lblColorFiller = new System.Windows.Forms.Label();
            this.pbColorMeta = new System.Windows.Forms.PictureBox();
            this.lblColorMetaData = new System.Windows.Forms.Label();
            this.pbColorSystemItem = new System.Windows.Forms.PictureBox();
            this.lblColorSystemItem = new System.Windows.Forms.Label();
            this.pbColorRIP = new System.Windows.Forms.PictureBox();
            this.lblColorRIP = new System.Windows.Forms.Label();
            this.pbColorIndex = new System.Windows.Forms.PictureBox();
            this.pbColorEssence = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblColorIndexTable = new System.Windows.Forms.Label();
            this.lblColorEssence = new System.Windows.Forms.Label();
            this.pbColorPartition = new System.Windows.Forms.PictureBox();
            this.lblColorPartition = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorReference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorSpecial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorFiller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorMeta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorSystemItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorRIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorEssence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorPartition)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(415, 308);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 27);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.bntOk_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(582, 286);
            this.tabControl1.TabIndex = 8;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.chkOffsetAsHex);
            this.tabGeneral.Controls.Add(this.chkPartialLoadMsg);
            this.tabGeneral.Controls.Add(this.label9);
            this.tabGeneral.Controls.Add(this.chkShowLines);
            this.tabGeneral.Controls.Add(this.cmbTopNumber);
            this.tabGeneral.Controls.Add(this.cmbThreshold);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabGeneral.Size = new System.Drawing.Size(574, 258);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // chkOffsetAsHex
            // 
            this.chkOffsetAsHex.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOffsetAsHex.Location = new System.Drawing.Point(5, 110);
            this.chkOffsetAsHex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkOffsetAsHex.Name = "chkOffsetAsHex";
            this.chkOffsetAsHex.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkOffsetAsHex.Size = new System.Drawing.Size(250, 22);
            this.chkOffsetAsHex.TabIndex = 5;
            this.chkOffsetAsHex.Text = "Show byte offsets as hex values";
            this.chkOffsetAsHex.UseVisualStyleBackColor = true;
            // 
            // chkPartialLoadMsg
            // 
            this.chkPartialLoadMsg.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPartialLoadMsg.Enabled = false;
            this.chkPartialLoadMsg.Location = new System.Drawing.Point(5, 42);
            this.chkPartialLoadMsg.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkPartialLoadMsg.Name = "chkPartialLoadMsg";
            this.chkPartialLoadMsg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkPartialLoadMsg.Size = new System.Drawing.Size(250, 22);
            this.chkPartialLoadMsg.TabIndex = 4;
            this.chkPartialLoadMsg.Text = "Show partial loading message";
            this.chkPartialLoadMsg.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(7, 67);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(559, 40);
            this.label9.TabIndex = 3;
            this.label9.Text = "Files that are larger than the threshold will be partially loaded. A partition wi" +
    "ll be loaded when it is expanded in the tree.";
            // 
            // chkShowLines
            // 
            this.chkShowLines.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowLines.Location = new System.Drawing.Point(5, 174);
            this.chkShowLines.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkShowLines.Name = "chkShowLines";
            this.chkShowLines.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkShowLines.Size = new System.Drawing.Size(250, 22);
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
            this.cmbTopNumber.Location = new System.Drawing.Point(238, 141);
            this.cmbTopNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbTopNumber.Name = "cmbTopNumber";
            this.cmbTopNumber.Size = new System.Drawing.Size(154, 23);
            this.cmbTopNumber.TabIndex = 1;
            this.cmbTopNumber.Visible = false;
            // 
            // cmbThreshold
            // 
            this.cmbThreshold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThreshold.Enabled = false;
            this.cmbThreshold.FormattingEnabled = true;
            this.cmbThreshold.Items.AddRange(new object[] {
            "All files",
            "100 MB",
            "500 MB",
            "1 GB",
            "5 GB",
            "10 GB",
            "No files"});
            this.cmbThreshold.Location = new System.Drawing.Point(238, 10);
            this.cmbThreshold.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbThreshold.Name = "cmbThreshold";
            this.cmbThreshold.Size = new System.Drawing.Size(154, 23);
            this.cmbThreshold.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(7, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Number of files to show in the top list:";
            this.label4.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Partial load threshold";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pbColorReference);
            this.tabPage2.Controls.Add(this.lblColorSpecial);
            this.tabPage2.Controls.Add(this.pbColorSpecial);
            this.tabPage2.Controls.Add(this.lblColorReference);
            this.tabPage2.Controls.Add(this.pbColorFiller);
            this.tabPage2.Controls.Add(this.lblColorFiller);
            this.tabPage2.Controls.Add(this.pbColorMeta);
            this.tabPage2.Controls.Add(this.lblColorMetaData);
            this.tabPage2.Controls.Add(this.pbColorSystemItem);
            this.tabPage2.Controls.Add(this.lblColorSystemItem);
            this.tabPage2.Controls.Add(this.pbColorRIP);
            this.tabPage2.Controls.Add(this.lblColorRIP);
            this.tabPage2.Controls.Add(this.pbColorIndex);
            this.tabPage2.Controls.Add(this.pbColorEssence);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.lblColorIndexTable);
            this.tabPage2.Controls.Add(this.lblColorEssence);
            this.tabPage2.Controls.Add(this.pbColorPartition);
            this.tabPage2.Controls.Add(this.lblColorPartition);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Size = new System.Drawing.Size(574, 258);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Colors";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pbColorReference
            // 
            this.pbColorReference.BackColor = System.Drawing.Color.Red;
            this.pbColorReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorReference.Location = new System.Drawing.Point(149, 165);
            this.pbColorReference.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorReference.Name = "pbColorReference";
            this.pbColorReference.Size = new System.Drawing.Size(130, 15);
            this.pbColorReference.TabIndex = 15;
            this.pbColorReference.TabStop = false;
            this.pbColorReference.Click += new System.EventHandler(this.pbColorReference_Click);
            // 
            // lblColorSpecial
            // 
            this.lblColorSpecial.AutoSize = true;
            this.lblColorSpecial.Location = new System.Drawing.Point(11, 186);
            this.lblColorSpecial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorSpecial.Name = "lblColorSpecial";
            this.lblColorSpecial.Size = new System.Drawing.Size(74, 15);
            this.lblColorSpecial.TabIndex = 14;
            this.lblColorSpecial.Text = "Special color";
            // 
            // pbColorSpecial
            // 
            this.pbColorSpecial.BackColor = System.Drawing.Color.Red;
            this.pbColorSpecial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorSpecial.Location = new System.Drawing.Point(149, 186);
            this.pbColorSpecial.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorSpecial.Name = "pbColorSpecial";
            this.pbColorSpecial.Size = new System.Drawing.Size(130, 15);
            this.pbColorSpecial.TabIndex = 13;
            this.pbColorSpecial.TabStop = false;
            this.pbColorSpecial.Click += new System.EventHandler(this.pbColorSpecial_Click);
            // 
            // lblColorReference
            // 
            this.lblColorReference.AutoSize = true;
            this.lblColorReference.Location = new System.Drawing.Point(11, 165);
            this.lblColorReference.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorReference.Name = "lblColorReference";
            this.lblColorReference.Size = new System.Drawing.Size(89, 15);
            this.lblColorReference.TabIndex = 12;
            this.lblColorReference.Text = "Reference color";
            // 
            // pbColorFiller
            // 
            this.pbColorFiller.BackColor = System.Drawing.Color.Red;
            this.pbColorFiller.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorFiller.Location = new System.Drawing.Point(149, 143);
            this.pbColorFiller.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorFiller.Name = "pbColorFiller";
            this.pbColorFiller.Size = new System.Drawing.Size(130, 15);
            this.pbColorFiller.TabIndex = 11;
            this.pbColorFiller.TabStop = false;
            this.pbColorFiller.Click += new System.EventHandler(this.pbColorFiller_Click);
            // 
            // lblColorFiller
            // 
            this.lblColorFiller.AutoSize = true;
            this.lblColorFiller.Location = new System.Drawing.Point(11, 143);
            this.lblColorFiller.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorFiller.Name = "lblColorFiller";
            this.lblColorFiller.Size = new System.Drawing.Size(62, 15);
            this.lblColorFiller.TabIndex = 10;
            this.lblColorFiller.Text = "Filler color";
            // 
            // pbColorMeta
            // 
            this.pbColorMeta.BackColor = System.Drawing.Color.Red;
            this.pbColorMeta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorMeta.Location = new System.Drawing.Point(149, 121);
            this.pbColorMeta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorMeta.Name = "pbColorMeta";
            this.pbColorMeta.Size = new System.Drawing.Size(130, 15);
            this.pbColorMeta.TabIndex = 9;
            this.pbColorMeta.TabStop = false;
            this.pbColorMeta.Click += new System.EventHandler(this.pbColorMeta_Click);
            // 
            // lblColorMetaData
            // 
            this.lblColorMetaData.AutoSize = true;
            this.lblColorMetaData.Location = new System.Drawing.Point(11, 121);
            this.lblColorMetaData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorMetaData.Name = "lblColorMetaData";
            this.lblColorMetaData.Size = new System.Drawing.Size(90, 15);
            this.lblColorMetaData.TabIndex = 8;
            this.lblColorMetaData.Text = "Meta data color";
            // 
            // pbColorSystemItem
            // 
            this.pbColorSystemItem.BackColor = System.Drawing.Color.Red;
            this.pbColorSystemItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorSystemItem.Location = new System.Drawing.Point(149, 99);
            this.pbColorSystemItem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorSystemItem.Name = "pbColorSystemItem";
            this.pbColorSystemItem.Size = new System.Drawing.Size(130, 15);
            this.pbColorSystemItem.TabIndex = 7;
            this.pbColorSystemItem.TabStop = false;
            this.pbColorSystemItem.Click += new System.EventHandler(this.pbColorSystemItem_Click);
            // 
            // lblColorSystemItem
            // 
            this.lblColorSystemItem.AutoSize = true;
            this.lblColorSystemItem.Location = new System.Drawing.Point(11, 99);
            this.lblColorSystemItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorSystemItem.Name = "lblColorSystemItem";
            this.lblColorSystemItem.Size = new System.Drawing.Size(102, 15);
            this.lblColorSystemItem.TabIndex = 6;
            this.lblColorSystemItem.Text = "System Item color";
            // 
            // pbColorRIP
            // 
            this.pbColorRIP.BackColor = System.Drawing.Color.Red;
            this.pbColorRIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorRIP.Location = new System.Drawing.Point(149, 77);
            this.pbColorRIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorRIP.Name = "pbColorRIP";
            this.pbColorRIP.Size = new System.Drawing.Size(130, 15);
            this.pbColorRIP.TabIndex = 5;
            this.pbColorRIP.TabStop = false;
            this.pbColorRIP.Click += new System.EventHandler(this.pbColorRIP_Click);
            // 
            // lblColorRIP
            // 
            this.lblColorRIP.AutoSize = true;
            this.lblColorRIP.Location = new System.Drawing.Point(11, 77);
            this.lblColorRIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorRIP.Name = "lblColorRIP";
            this.lblColorRIP.Size = new System.Drawing.Size(54, 15);
            this.lblColorRIP.TabIndex = 4;
            this.lblColorRIP.Text = "RIP color";
            // 
            // pbColorIndex
            // 
            this.pbColorIndex.BackColor = System.Drawing.Color.Red;
            this.pbColorIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorIndex.Location = new System.Drawing.Point(149, 55);
            this.pbColorIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorIndex.Name = "pbColorIndex";
            this.pbColorIndex.Size = new System.Drawing.Size(130, 15);
            this.pbColorIndex.TabIndex = 3;
            this.pbColorIndex.TabStop = false;
            this.pbColorIndex.Click += new System.EventHandler(this.pbColorIndex_Click);
            // 
            // pbColorEssence
            // 
            this.pbColorEssence.BackColor = System.Drawing.Color.Red;
            this.pbColorEssence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorEssence.Location = new System.Drawing.Point(149, 33);
            this.pbColorEssence.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorEssence.Name = "pbColorEssence";
            this.pbColorEssence.Size = new System.Drawing.Size(130, 15);
            this.pbColorEssence.TabIndex = 3;
            this.pbColorEssence.TabStop = false;
            this.pbColorEssence.Click += new System.EventHandler(this.pbColorEssence_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label7.Location = new System.Drawing.Point(11, 216);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(216, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Click on the colors to change the colors";
            // 
            // lblColorIndexTable
            // 
            this.lblColorIndexTable.AutoSize = true;
            this.lblColorIndexTable.Location = new System.Drawing.Point(11, 55);
            this.lblColorIndexTable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorIndexTable.Name = "lblColorIndexTable";
            this.lblColorIndexTable.Size = new System.Drawing.Size(95, 15);
            this.lblColorIndexTable.TabIndex = 2;
            this.lblColorIndexTable.Text = "Index table color";
            // 
            // lblColorEssence
            // 
            this.lblColorEssence.AutoSize = true;
            this.lblColorEssence.Location = new System.Drawing.Point(11, 33);
            this.lblColorEssence.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorEssence.Name = "lblColorEssence";
            this.lblColorEssence.Size = new System.Drawing.Size(78, 15);
            this.lblColorEssence.TabIndex = 2;
            this.lblColorEssence.Text = "Essence color";
            // 
            // pbColorPartition
            // 
            this.pbColorPartition.BackColor = System.Drawing.Color.Red;
            this.pbColorPartition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColorPartition.Location = new System.Drawing.Point(149, 11);
            this.pbColorPartition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbColorPartition.Name = "pbColorPartition";
            this.pbColorPartition.Size = new System.Drawing.Size(130, 15);
            this.pbColorPartition.TabIndex = 1;
            this.pbColorPartition.TabStop = false;
            this.pbColorPartition.Click += new System.EventHandler(this.pbColorPartition_Click);
            // 
            // lblColorPartition
            // 
            this.lblColorPartition.AutoSize = true;
            this.lblColorPartition.Location = new System.Drawing.Point(11, 11);
            this.lblColorPartition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorPartition.Name = "lblColorPartition";
            this.lblColorPartition.Size = new System.Drawing.Size(82, 15);
            this.lblColorPartition.TabIndex = 0;
            this.lblColorPartition.Text = "Partition color";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(510, 308);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 27);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(15, 308);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(131, 27);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset to default";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FormSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(611, 348);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbColorReference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorSpecial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorFiller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorMeta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorSystemItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorRIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorEssence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorPartition)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.ComboBox cmbThreshold;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.PictureBox pbColorPartition;
		private System.Windows.Forms.Label lblColorPartition;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.PictureBox pbColorEssence;
		private System.Windows.Forms.Label lblColorEssence;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkShowLines;
		private System.Windows.Forms.PictureBox pbColorIndex;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblColorIndexTable;
		private System.Windows.Forms.ComboBox cmbTopNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pbColorRIP;
		private System.Windows.Forms.Label lblColorRIP;
		private System.Windows.Forms.PictureBox pbColorSystemItem;
		private System.Windows.Forms.Label lblColorSystemItem;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkPartialLoadMsg;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.PictureBox pbColorSpecial;
		private System.Windows.Forms.Label lblColorReference;
		private System.Windows.Forms.PictureBox pbColorFiller;
		private System.Windows.Forms.Label lblColorFiller;
		private System.Windows.Forms.PictureBox pbColorMeta;
		private System.Windows.Forms.Label lblColorMetaData;
        private System.Windows.Forms.CheckBox chkOffsetAsHex;
        private System.Windows.Forms.PictureBox pbColorReference;
        private System.Windows.Forms.Label lblColorSpecial;
    }
}