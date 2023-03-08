#region license
//
// MXFInspect - Myriadbits MXF Viewer. 
// Inspect MXF Files.
// Copyright (C) 2015 Myriadbits, Jochem Bakker
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// For more information, contact me at: info@myriadbits.com
//
#endregion

using BrightIdeasSoftware;
using Myriadbits.MXF;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
	public partial class FormReport : Form
	{
		private readonly MXFFile mxfFile = null;

		/// <summary>
		/// Constructor, duh
		/// </summary>
		public FormReport(MXFFile file)
		{
			InitializeComponent();
            mxfFile = file;

        }

		private void ReportForm_Load(object sender, EventArgs e)
		{
            InitListView();
            this.txtGeneralInfo.Text = GetFileInfo();
            //DisplayFileInfo();
        }


		private void DisplayFileInfo()
		{
			StringBuilder summary = new StringBuilder();
			int errorCnt = 0;
			int warningCnt = 0;
			if (this.mxfFile != null)
			{
				errorCnt = this.mxfFile.ValidationResults.Count(a => a.Severity == MXFValidationSeverity.Error);
				warningCnt = this.mxfFile.ValidationResults.Count(a => a.Severity == MXFValidationSeverity.Warning);
				if (errorCnt == 0 && warningCnt == 0)
					summary.AppendLine(string.Format("There are no errors found, file seems to be ok!"));
				else
					summary.AppendLine(string.Format("Found {0} errors and {1} warnings!", errorCnt, warningCnt));
				summary.AppendLine(string.Format("(Double click on an item to see more details)"));
			}
			else
			{
				summary.AppendLine(string.Format("ERROR WHILE PARSING THE MXF FILE"));
			}

			txtSum.Text = summary.ToString();
		}



		private void InitListView()
		{
			this.tlvResults.CanExpandGetter = delegate (object x)
			{
				if (x is MXFValidationResult vr)
				{
					return vr.Count > 0;
				}
				return false;
			};

			this.tlvResults.ChildrenGetter = delegate (object x)
			{
				if (x is MXFValidationResult vr)
				{
					return null;
				}
				return null;
			};

			this.colSeverity.AspectToStringConverter = delegate (object x)
			{
				return String.Empty;
			};


			this.colSeverity.ImageGetter = delegate (object x)
			{
				if(x is MXFValidationResult vr)
				{
                    return Enum.GetName(typeof(MXFValidationSeverity), vr.Severity);
                }
				return null;
			};

			OLVColumn col = (OLVColumn)this.tlvResults.Columns[0];
			col.Renderer = null;

			OLVColumn col1 = (OLVColumn)this.tlvResults.Columns[2];
			col1.Renderer = null;

			if (OperatingSystem.IsWindows())
			{
				Pen pen = new Pen(Color.Black, 1.001f);
				pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
				this.tlvResults.TreeColumnRenderer.LinePen = pen;
			}

            this.tlvResults.AutoResizeColumns();
        }

		private string GetFileInfo()
		{
			if (this.mxfFile != null)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(string.Format("Filename: {0}", this.mxfFile.File.FullName));
				sb.AppendLine(string.Format("File size: {0:0.00} Mb", (this.mxfFile.File.Length) / (1024 * 1024)));
				sb.AppendLine(string.Format("Number of partitions: {0}", this.mxfFile.PartitionCount));
				if (this.mxfFile.RIP != null)
					sb.AppendLine(string.Format("RIP Found (containing {0} entries)", this.mxfFile.RIPEntryCount));
				if (this.mxfFile.FirstSystemItem != null)
					sb.AppendLine(string.Format("First system item time: {0}", this.mxfFile.FirstSystemItem.UserDateFullFrameNb));
				if (this.mxfFile.LastSystemItem != null)
					sb.AppendLine(string.Format("Last system item time: {0}", this.mxfFile.LastSystemItem.UserDateFullFrameNb));
				return sb.ToString();
			}
			return "";
		}

		/// <summary>
		/// Close this dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Execute all tests
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btnExecuteAllTests_Click(object sender, EventArgs e)
		{
			if (this.mxfFile != null)
			{
				this.tlvResults.ClearObjects();
				this.prbProcessing.Visible = true;
				this.txtGeneralInfo.Visible = false;
				this.Enabled = false;
                
				var cts = new CancellationTokenSource();
                var progressHandler = new Progress<TaskReport>(this.ReportProgress);
                
				var results = await mxfFile.ExecuteValidationTest(true, progressHandler, cts.Token);
                this.tlvResults.SetObjects(results);

				this.tlvResults.AutoResizeColumns();
                this.prbProcessing.Visible = false;
                this.txtGeneralInfo.Visible = true;
                this.Enabled = true;
                
                DisplayFileInfo();
            }
		}

        private void tlvResults_SelectionChanged(object sender, EventArgs e)
        {
			var selResult = tlvResults.SelectedObject as MXFValidationResult;

			if(selResult?.Object != null)
            {
				var frmMain = this.Owner as FormMain;
				frmMain.ActiveView.RevealAndSelect(selResult?.Object);

			}
        }

        public void ReportProgress(TaskReport report)
        {
            this.prbProcessing.SetValueFast(report.Percent);
            //lblSingle.Text = $"{report.Percent}%";
            //lblSingleDesc.Text = report.Description;
        }
    }
}
