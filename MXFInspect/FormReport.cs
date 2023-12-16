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

using Myriadbits.MXF;
using Myriadbits.MXFInspect.CustomControls;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public partial class FormReport : Form
    {
        private readonly MXFFile mxfFile = null;
        private CancellationTokenSource cts;

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

            this.colOffset.UseFiltering = false;
            this.colResult.UseFiltering = false;
            this.tlvValidationResults.UseFilterIndicator = true;
            this.tlvValidationResults.UseFiltering = true;
            this.colSeverity.ClusteringStrategy = new MXFValidationSeverityCluster();

            // TODO what are these lines for?
            this.colCategory.Renderer = null;
            this.colSeverity.Renderer = null;

            this.tlvValidationResults.CanExpandGetter = delegate (object x)
            {
                if (x is List<MXFValidationResult> vr)
                {
                    return vr.Count > 1;
                }
                return false;
            };

            this.tlvValidationResults.ChildrenGetter = delegate (object x)
            {
                if (x is List<MXFValidationResult> vr)
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

        /// <summary>
        /// Close this dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnClose.Text = "Cancelling...";
            btnClose.Enabled = false;
            cts.Cancel();
        }

        private async Task ValidateMXFFile()
        {
            try
            {
                this.tlvValidationResults.ClearObjects();
                this.tlvValidationResults.Enabled = false;
                this.prbProcessing.Visible = true;
                //this.Enabled = false;
                btnClose.Enabled = true;
                this.btnClose.Text = "Cancel";
                btnClose.Click -= btnClose_Click;
                btnClose.Click += btnCancel_Click;

                cts = new CancellationTokenSource();
                cts.Token.ThrowIfCancellationRequested();

                var progressHandler = new Progress<TaskReport>(this.ReportProgress);

                // create test list (TODO implement test suite class)
                List<MXFValidator> validatorList = new List<MXFValidator>
                {
                    new MXFValidatorKLVStream(mxfFile),
                    new MXFValidatorPartitions(mxfFile),
                    new MXFValidatorRIP(mxfFile),
                    new MXFValidatorUL(mxfFile),
                    new MXFValidatorInfo(mxfFile),
                };

                var results = await MXFFileValidationEngine.Validate(mxfFile, validatorList, progressHandler, cts.Token);

                // display the one with biggest offset first, then autoresize columns executes
                // correctly and finally reverse order, i.e. lowest offset first
                this.tlvValidationResults.SetObjects(results.OrderByDescending(vr => vr.Offset));
                this.tlvValidationResults.AutoResizeColumns();
                this.tlvValidationResults.PrimarySortColumn = colOffset;
                this.tlvValidationResults.PrimarySortOrder = SortOrder.Ascending;
                this.tlvValidationResults.Sort();

                this.prbProcessing.Visible = false;
                this.tlvValidationResults.Enabled = true;

                btnClose.Click += btnClose_Click;
                btnClose.Click -= btnCancel_Click;
            }
            catch (OperationCanceledException ex)
            {
                Log.ForContext<FormReport>().Error(ex, $"OperationCanceled exception occured while validating file:");
                this.Close();
            }
            catch (Exception ex)
            {
                Log.ForContext<FormReport>().Error(ex, $"Exception occured while validating file:");
                MessageBox.Show(ex.Message, "Exception occured while validating file");

            }
            finally
            {
                btnClose.Text = "Close";
            }

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

        private void FormReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            // set cancellation on cancellation token
            cts.Cancel();
        }
    }
}
