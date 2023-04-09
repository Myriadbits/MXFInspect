﻿#region license
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
using Serilog;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public partial class FormReport : Form
    {
        private readonly MXFFile mxfFile = null;

        public FormReport(MXFFile file)
        {
            InitializeComponent();
            if (file != null)
            {
                mxfFile = file;
            }
            else throw new ArgumentNullException(nameof(file));
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            InitValidationResultTreeListView();
            PopulateQuickInfoListView();
            await ValidateMXFFile();
        }

        private void InitValidationResultTreeListView()
        {
            this.tlvValidationResults.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);

            this.tlvValidationResults.CanExpandGetter = delegate (object x)
            {
                if (x is MXFValidationResult vr)
                {
                    return vr.Count > 1;
                }
                return false;
            };

            this.tlvValidationResults.ChildrenGetter = delegate (object x)
            {
                if (x is MXFValidationResult vr)
                {
                    return vr;
                }
                return null;
            };

            this.colOffset.AspectToStringConverter = delegate (object x)
            {
                if (x is long l)
                {
                    return l.ToString("N0");
                }
                return string.Empty;
            };

            this.colSeverity.AspectToStringConverter = delegate (object x)
            {
                return String.Empty;
            };


            this.colSeverity.ImageGetter = delegate (object x)
            {
                if (x is MXFValidationResult vr)
                {
                    return Enum.GetName(typeof(MXFValidationSeverity), vr.Severity);
                }
                return null;
            };

            OLVColumn col = (OLVColumn)this.tlvValidationResults.Columns[0];
            col.Renderer = null;

            OLVColumn col1 = (OLVColumn)this.tlvValidationResults.Columns[2];
            col1.Renderer = null;

            if (OperatingSystem.IsWindows())
            {
                Pen pen = new Pen(Color.Black, 1.001f);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                //this.tlvResults.TreeColumnRenderer.LinePen = pen;
                this.tlvValidationResults.TreeColumnRenderer.IsShowLines = false;
                this.tlvValidationResults.TreeColumnRenderer.UseTriangles = true;
            }
        }

        private void PopulateQuickInfoListView()
        {
            try
            {
                var qi = new MXFQuickInfo(mxfFile);
                olvQuickInfo.SetObjects(qi.ToKeyValue());
                olvQuickInfo.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                Log.ForContext<FormReport>().Error(ex, $"Exception occured while showing quick info:");
                MessageBox.Show(ex.Message, "Error occured while showing quick info");
            }
        }

        private void DisplayFileInfo()
        {
            StringBuilder sb = new StringBuilder();
            int errorCnt = 0;
            int warningCnt = 0;
            if (this.mxfFile != null)
            {
                errorCnt = this.mxfFile.ValidationResults.Count(a => a.Severity == MXFValidationSeverity.Error);
                warningCnt = this.mxfFile.ValidationResults.Count(a => a.Severity == MXFValidationSeverity.Warning);
                if (errorCnt == 0 && warningCnt == 0)
                    sb.AppendLine(string.Format("There are no errors found, file seems to be ok!"));
                else
                    sb.AppendLine(string.Format("Found {0} errors and {1} warnings!", errorCnt, warningCnt));
                sb.AppendLine(string.Format("(Double click on an item to see more details)"));
            }
            else
            {
                sb.AppendLine(string.Format("ERROR WHILE PARSING THE MXF FILE"));
            }
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
            await ValidateMXFFile();
        }

        private async Task ValidateMXFFile()
        {
            this.tlvValidationResults.ClearObjects();
            this.prbProcessing.Visible = true;
            this.Enabled = false;

            var cts = new CancellationTokenSource();
            var progressHandler = new Progress<TaskReport>(this.ReportProgress);

            var results = await mxfFile.ExecuteValidationTest(false, progressHandler, cts.Token);

            // display the one with biggest offset first, then autoresize columns executes
            // correctly and finally reverse order, i.e. lowest offset first
            this.tlvValidationResults.SetObjects(results.OrderByDescending(vr => vr.Offset));
            this.tlvValidationResults.AutoResizeColumns();
            this.tlvValidationResults.PrimarySortColumn = colSeverity;
            this.tlvValidationResults.PrimarySortOrder = SortOrder.Ascending;
            this.tlvValidationResults.SecondarySortColumn = colOffset;
            this.tlvValidationResults.SecondarySortOrder = SortOrder.Ascending;
            this.tlvValidationResults.Sort();

            this.prbProcessing.Visible = false;
            this.Enabled = true;
        }

        private void tlvResults_SelectionChanged(object sender, EventArgs e)
        {
            var frmMain = this.Owner as FormMain;
            if (tlvValidationResults.SelectedObject is MXFValidationResult selResult)
            {
                if (selResult.Object != null)
                {
                    frmMain.ActiveView.RevealAndSelect(selResult?.Object);
                }
                else
                {
                    var selObj = this.mxfFile.Descendants().Where(d => d.Offset == selResult.Offset).FirstOrDefault();
                    frmMain.ActiveView.RevealAndSelect(selObj);
                }
            }
        }

        private void ReportProgress(TaskReport report)
        {
            this.prbProcessing.SetValueFast(report.Percent);
        }

    }
}
