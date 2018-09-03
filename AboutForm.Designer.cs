namespace Myriadbits.MXFInspect
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.lblTitle = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.llMyriadbits = new System.Windows.Forms.LinkLabel();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.txtLicense = new System.Windows.Forms.TextBox();
			this.bntClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(34, 124);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(262, 36);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "MXFInspect";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbLogo
			// 
			this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
			this.pbLogo.Location = new System.Drawing.Point(0, 0);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(1262, 121);
			this.pbLogo.TabIndex = 3;
			this.pbLogo.TabStop = false;
			this.pbLogo.Click += new System.EventHandler(this.pbLogo_Click);
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(35, 160);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(69, 13);
			this.lblVersion.TabIndex = 4;
			this.lblVersion.Text = "Version 1.0.0";
			// 
			// llMyriadbits
			// 
			this.llMyriadbits.AutoSize = true;
			this.llMyriadbits.Location = new System.Drawing.Point(35, 186);
			this.llMyriadbits.Name = "llMyriadbits";
			this.llMyriadbits.Size = new System.Drawing.Size(103, 13);
			this.llMyriadbits.TabIndex = 5;
			this.llMyriadbits.TabStop = true;
			this.llMyriadbits.Text = "www.myriadbits.com";
			this.llMyriadbits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMyriadbits_LinkClicked);
			// 
			// lblCopyright
			// 
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new System.Drawing.Point(35, 173);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(184, 13);
			this.lblCopyright.TabIndex = 4;
			this.lblCopyright.Text = "Copyright (c) 2015 by Jochem Bakker";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(35, 232);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(223, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "This program is licensed under the GPL, click ";
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(253, 232);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(28, 13);
			this.linkLabel2.TabIndex = 5;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "here";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(280, 232);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "to view this license.";
			// 
			// txtLicense
			// 
			this.txtLicense.Location = new System.Drawing.Point(12, 124);
			this.txtLicense.Multiline = true;
			this.txtLicense.Name = "txtLicense";
			this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLicense.Size = new System.Drawing.Size(500, 97);
			this.txtLicense.TabIndex = 6;
			this.txtLicense.Visible = false;
			// 
			// bntClose
			// 
			this.bntClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bntClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bntClose.Location = new System.Drawing.Point(437, 247);
			this.bntClose.Name = "bntClose";
			this.bntClose.Size = new System.Drawing.Size(75, 23);
			this.bntClose.TabIndex = 7;
			this.bntClose.Text = "Close";
			this.bntClose.UseVisualStyleBackColor = true;
			this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 252);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "This application uses the (great!)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(301, 252);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "from Phillip Piper.";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(194, 252);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(107, 13);
			this.linkLabel1.TabIndex = 10;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "ObjectListView library";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// AboutForm
			// 
			this.AcceptButton = this.bntClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bntClose;
			this.ClientSize = new System.Drawing.Size(524, 282);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bntClose);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.llMyriadbits);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.pbLogo);
			this.Controls.Add(this.txtLicense);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1270, 320);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(540, 320);
			this.Name = "AboutForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.LinkLabel llMyriadbits;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtLicense;
		private System.Windows.Forms.Button bntClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}