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
            this.btnCancel.Location = new System.Drawing.Point(164, 181);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 27);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // prgOverall
            // 
            this.prgOverall.Location = new System.Drawing.Point(14, 29);
            this.prgOverall.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.prgOverall.Name = "prgOverall";
            this.prgOverall.Size = new System.Drawing.Size(418, 27);
            this.prgOverall.TabIndex = 1;
            // 
            // prgSingle
            // 
            this.prgSingle.Location = new System.Drawing.Point(14, 89);
            this.prgSingle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.prgSingle.Name = "prgSingle";
            this.prgSingle.Size = new System.Drawing.Size(418, 27);
            this.prgSingle.TabIndex = 2;
            // 
            // lblOverall
            // 
            this.lblOverall.BackColor = System.Drawing.Color.Transparent;
            this.lblOverall.Location = new System.Drawing.Point(384, 59);
            this.lblOverall.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOverall.Name = "lblOverall";
            this.lblOverall.Size = new System.Drawing.Size(51, 15);
            this.lblOverall.TabIndex = 6;
            this.lblOverall.Text = "20%";
            this.lblOverall.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSingle
            // 
            this.lblSingle.BackColor = System.Drawing.Color.Transparent;
            this.lblSingle.Location = new System.Drawing.Point(371, 119);
            this.lblSingle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSingle.Name = "lblSingle";
            this.lblSingle.Size = new System.Drawing.Size(64, 15);
            this.lblSingle.TabIndex = 7;
            this.lblSingle.Text = "30%";
            this.lblSingle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblOverallDesc
            // 
            this.lblOverallDesc.AutoSize = true;
            this.lblOverallDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblOverallDesc.Location = new System.Drawing.Point(14, 59);
            this.lblOverallDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOverallDesc.Name = "lblOverallDesc";
            this.lblOverallDesc.Size = new System.Drawing.Size(107, 15);
            this.lblOverallDesc.TabIndex = 8;
            this.lblOverallDesc.Text = "Overall Description";
            // 
            // lblSingleDesc
            // 
            this.lblSingleDesc.AutoSize = true;
            this.lblSingleDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblSingleDesc.Location = new System.Drawing.Point(14, 119);
            this.lblSingleDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSingleDesc.Name = "lblSingleDesc";
            this.lblSingleDesc.Size = new System.Drawing.Size(99, 15);
            this.lblSingleDesc.TabIndex = 9;
            this.lblSingleDesc.Text = "SingleDescription";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Location = new System.Drawing.Point(158, 155);
            this.lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(124, 15);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 224);
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
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

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