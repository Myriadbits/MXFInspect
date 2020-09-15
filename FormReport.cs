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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
	public partial class FormReport : Form
	{
		private MXFFile m_mainFile = null;
		private Stopwatch m_stopWatch = new Stopwatch();
		private int m_lastPercentage = -1;

		/// <summary>
		/// Constructor, duh
		/// </summary>
		public FormReport(MXFFile file)
		{
			InitializeComponent();
			SetFile(file);
		}

		/// <summary>
		/// Set the file
		/// </summary>
		/// <param name="file"></param>
		public void SetFile(MXFFile file)
		{
			m_mainFile = file;
		}

		private void ReportForm_Load(object sender, EventArgs e)
		{
			DisplayFileInfo();
		}


		private void DisplayFileInfo()
		{
			if (this.m_mainFile != null)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(string.Format("Filename: {0}", this.m_mainFile.Filename));
				sb.AppendLine(string.Format("File size: {0:0.00} Mb", (this.m_mainFile.Filesize) / (1024 * 1024)));
				sb.AppendLine(string.Format("Number of partitions: {0}", this.m_mainFile.PartitionCount));
				if (this.m_mainFile.RIP != null)
					sb.AppendLine(string.Format("RIP Found (containing {0} entries)", this.m_mainFile.RIPEntryCount));
				if (this.m_mainFile.FirstSystemItem != null)
					sb.AppendLine(string.Format("First system item time: {0}", this.m_mainFile.FirstSystemItem.UserDateFullFrameNb));
				if (this.m_mainFile.LastSystemItem != null)
					sb.AppendLine(string.Format("Last system item time: {0}", this.m_mainFile.LastSystemItem.UserDateFullFrameNb));

				this.txtGeneralInfo.Text = sb.ToString();

				this.tlvResults.SetObjects(this.m_mainFile.Results);
				this.tlvResults.CanExpandGetter = delegate(object x)
				{
					MXFValidationResult vr = x as MXFValidationResult;
					if (vr == null) return false;
					return vr.Count > 0;
				};
			}
			//this.tlvResults.ChildrenGetter = delegate(object x)
			//{
			//	MXFValidationResult vr = x as MXFValidationResult;
			//	if (vr == null) return null;
			//	return vr;
			//};


			this.chState.AspectToStringConverter = delegate(object x)
			{
				return String.Empty;
			};

			this.chState.ImageGetter = delegate(object x)
			{
				MXFValidationResult vr = x as MXFValidationResult;
				if (vr == null) return "";
				return Enum.GetName(typeof(MXFValidationState), vr.State);
			};

			OLVColumn col = (OLVColumn)this.tlvResults.Columns[0];
			col.Renderer = null;

			OLVColumn col1 = (OLVColumn)this.tlvResults.Columns[2];
			col1.Renderer = null;// this.tlvResults.TreeColumnRenderer;

			Pen pen = new Pen(Color.Black, 1.001f);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			this.tlvResults.TreeColumnRenderer.LinePen = pen;

			StringBuilder summary = new StringBuilder();
			int errorCnt = 0;
			int warningCnt = 0;
			if (this.m_mainFile != null)
			{
				errorCnt = this.m_mainFile.Results.Count(a => a.State == MXFValidationState.Error);
				warningCnt = this.m_mainFile.Results.Count(a => a.State == MXFValidationState.Warning);
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

		/// <summary>
		/// Close this dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bntClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Execute all tests
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExecuteAllTests_Click(object sender, EventArgs e)
		{
			if (this.m_mainFile != null)
			{
				this.prbProcessing.Visible = true;
				this.txtGeneralInfo.Visible = false;
				this.Enabled = false;
				this.m_lastPercentage = 0;

				backgroundWorker.RunWorkerAsync(this.m_mainFile);
			}
		}

		private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			MXFFile mainFile = e.Argument as MXFFile;
			if (mainFile != null)
			{			
				BackgroundWorker worker = sender as BackgroundWorker;
				mainFile.ExecuteValidationTest(worker, true);
			}
		}

		private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage > 0)
			{
				if (!m_stopWatch.IsRunning)
				{
					m_stopWatch.Start();
					m_lastPercentage = e.ProgressPercentage;
				}
				else
				{
					string currentTask = e.UserState as string;
					if (currentTask == null) currentTask = "";
					if (e.ProgressPercentage - m_lastPercentage > 0)
					{
						int estimate = 100 - e.ProgressPercentage;
						int msecPerPercentage = (int)(m_stopWatch.ElapsedMilliseconds / (e.ProgressPercentage - m_lastPercentage));
						txtSum.Text = string.Format("{0} - Estimated time: {1} s", currentTask, (estimate * msecPerPercentage) / 1000);
					}
					m_lastPercentage = e.ProgressPercentage;
				}
			}
			this.prbProcessing.Value = e.ProgressPercentage;
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			this.prbProcessing.Visible = false;
			this.txtGeneralInfo.Visible = true;
			this.Enabled = true;

			DisplayFileInfo();
		}
	}
}
