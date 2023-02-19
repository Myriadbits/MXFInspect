namespace Myriadbits.MXFInspect
{
    partial class FormProgress
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.prgOverall = new System.Windows.Forms.ProgressBar();
			this.prgSingle = new System.Windows.Forms.ProgressBar();
			this.lblOverall = new System.Windows.Forms.Label();
			this.lblSingle = new System.Windows.Forms.Label();
			this.lblOverallDesc = new System.Windows.Forms.Label();
			this.lblSingleDesc = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.tmrStopwatch = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(593, 299);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(210, 58);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// prgOverall
			// 
			this.prgOverall.Location = new System.Drawing.Point(26, 34);
			this.prgOverall.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.prgOverall.Name = "prgOverall";
			this.prgOverall.Size = new System.Drawing.Size(776, 58);
			this.prgOverall.TabIndex = 1;
			// 
			// prgSingle
			// 
			this.prgSingle.Location = new System.Drawing.Point(26, 136);
			this.prgSingle.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.prgSingle.Name = "prgSingle";
			this.prgSingle.Size = new System.Drawing.Size(776, 58);
			this.prgSingle.TabIndex = 2;
			// 
			// lblOverall
			// 
			this.lblOverall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblOverall.BackColor = System.Drawing.Color.Transparent;
			this.lblOverall.Location = new System.Drawing.Point(714, 98);
			this.lblOverall.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.lblOverall.Name = "lblOverall";
			this.lblOverall.Size = new System.Drawing.Size(95, 32);
			this.lblOverall.TabIndex = 6;
			this.lblOverall.Text = "20%";
			this.lblOverall.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSingle
			// 
			this.lblSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSingle.BackColor = System.Drawing.Color.Transparent;
			this.lblSingle.Location = new System.Drawing.Point(684, 200);
			this.lblSingle.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.lblSingle.Name = "lblSingle";
			this.lblSingle.Size = new System.Drawing.Size(119, 32);
			this.lblSingle.TabIndex = 7;
			this.lblSingle.Text = "30%";
			this.lblSingle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblOverallDesc
			// 
			this.lblOverallDesc.BackColor = System.Drawing.Color.Transparent;
			this.lblOverallDesc.Location = new System.Drawing.Point(26, 98);
			this.lblOverallDesc.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.lblOverallDesc.Name = "lblOverallDesc";
			this.lblOverallDesc.Size = new System.Drawing.Size(710, 32);
			this.lblOverallDesc.TabIndex = 8;
			// 
			// lblSingleDesc
			// 
			this.lblSingleDesc.BackColor = System.Drawing.Color.Transparent;
			this.lblSingleDesc.Location = new System.Drawing.Point(26, 200);
			this.lblSingleDesc.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.lblSingleDesc.Name = "lblSingleDesc";
			this.lblSingleDesc.Size = new System.Drawing.Size(710, 38);
			this.lblSingleDesc.TabIndex = 9;
			// 
			// lblTime
			// 
			this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTime.BackColor = System.Drawing.Color.Transparent;
			this.lblTime.Location = new System.Drawing.Point(26, 256);
			this.lblTime.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(777, 37);
			this.lblTime.TabIndex = 10;
			this.lblTime.Text = "Time Elapsed: 00:00:00";
			this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tmrStopwatch
			// 
			this.tmrStopwatch.Tick += new System.EventHandler(this.tmrStopwatch_Tick);
			// 
			// FormProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(829, 372);
			this.ControlBox = false;
			this.Controls.Add(this.lblTime);
			this.Controls.Add(this.lblSingleDesc);
			this.Controls.Add(this.lblOverallDesc);
			this.Controls.Add(this.lblSingle);
			this.Controls.Add(this.lblOverall);
			this.Controls.Add(this.prgSingle);
			this.Controls.Add(this.prgOverall);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.MinimumSize = new System.Drawing.Size(855, 443);
			this.Name = "FormProgress";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Progress";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar prgSingle;
        private System.Windows.Forms.ProgressBar prgOverall;
        private System.Windows.Forms.Label lblOverall;
        private System.Windows.Forms.Label lblSingle;
        private System.Windows.Forms.Label lblOverallDesc;
        private System.Windows.Forms.Label lblSingleDesc;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrStopwatch;
    }
}