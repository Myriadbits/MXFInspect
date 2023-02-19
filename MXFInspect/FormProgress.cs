using Myriadbits.MXF;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{

    public partial class FormProgress : Form
    {
        private CancellationTokenSource _cts;
        private readonly Stopwatch sw = new Stopwatch();
        private int stopWatchInterval = 1000;

        public TimeSpan ElapsedTime => sw.Elapsed;

        public string ElapsedTimeFormatted => ElapsedTime.ToString(@"hh\:mm\:ss");

        public FormProgress(string formTitle, CancellationTokenSource cts)
        {
            InitializeComponent();
            ResetUI();
            SetFormTitle(formTitle);
            _cts = cts;
            tmrStopwatch.Interval = stopWatchInterval;
            tmrStopwatch.Start();
            sw.Start();
        }

        public async Task<DialogResult> ShowDialogAsync()
        {
            await Task.Yield();
            if (this.IsDisposed)
            { return DialogResult.OK; }
            return this.ShowDialog();
        }

        private void SetFormTitle(string text)
        {
            Text = text;
        }

        public void ReportSingleProgress(TaskReport report)
        {
            prgSingle.SetValueFast(report.Percent);
            lblSingle.Text = $"{report.Percent}%";
            lblSingleDesc.Text = report.Description;
        }

        public void ReportOverallProgress(TaskReport report)
        {
            prgOverall.SetValueFast(report.Percent);
            lblOverall.Text = $"{report.Percent}%";
            lblOverallDesc.Text = report.Description;
        }

        public void ResetUI()
        {
            prgOverall.Value = 0;
            prgSingle.Value = 0;
            lblSingle.Text = "0%";
            lblOverall.Text = "0%";
            lblOverallDesc.Text = string.Empty;
            lblSingleDesc.Text = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Text = "Cancelling...";
            btnCancel.Enabled = false;
            _cts.Cancel();
        }

        private void tmrStopwatch_Tick(object sender, EventArgs e)
        {
            lblTime.Text = $"{"Time Elapsed:"} {ElapsedTimeFormatted}";
        }
    }
}
