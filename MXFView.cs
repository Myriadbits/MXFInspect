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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public partial class MXFView : MyFormPage
    {
        public MXFObject PhysicalTreeSelectedObject { get; private set; }
        public MXFLogicalObject LogicalTreeSelectedObject { get; private set; }
        public MXFObject SelectedObject { get; private set; }

        private FormMain ParentMainForm { get; set; }

        private MXFObject m_selectedObject = null;

        private MXFObject m_currentReference = null;

        private Stopwatch m_stopWatch = new Stopwatch();
        private int m_lastPercentage = 0;


        private bool m_fDoNotSelectOther = false;
        private FileParseOptions m_eFileParseOptions = FileParseOptions.Normal;

        private bool _fillerHidden = true;
        public bool FillerHidden { get => _fillerHidden; set { this._fillerHidden = value; this.HideFiller(value); } }

        public bool PhysicalViewShown { get; set; } = true;
        public string Filename { get; set; }

        private bool _currentTypeFiltered = false;
        public bool FilterCurrentType
        {
            get => _currentTypeFiltered;
            set
            {
                this._currentTypeFiltered = value;
                this.SetTypeFilter(value);
            }
        }

        public MXFFile File { get; private set; }

        public MXFView(string fileName)
        {
            InitializeComponent();
            this.Filename = fileName;
            this.Text = fileName;
            this.FillerHidden = true;
            this.MainPanel = this.mainPanel; // Do this AFTER the InitializeComponent call!!!

        }

        /// <summary>
        /// Initialize the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MXFView_Load(object sender, EventArgs e)
        {
            this.ParentMainForm = this.MdiParent as FormMain;

            ObjectListView.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.Text = this.Filename;
            this.btnSelectReference.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnPrevious.Enabled = false;

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.splitMain.Visible = false;
            this.prbProcessing.Visible = true;

            //bug that means you have to set the desired icon again otherwise it reverts to default when child form is maximised
            this.Icon = Myriadbits.MXFInspect.Properties.Resources.ChildIcon;

            this.chkInfo.Checked = true;
            this.propGrid.HelpVisible = this.chkInfo.Checked;


            // wiring treelistviews with selectionchanged event
            this.tlvPhysical.SelectionChanged += PhysicalTree_SelectionChanged;
            this.tlvLogical.SelectionChanged += LogicalTree_SelectionChanged;

            // Determine the filesize
            m_eFileParseOptions = FileParseOptions.Normal;
            long fileThreshold = ((long)MXFInspect.Properties.Settings.Default.PartialLoadThresholdMB) * 1024 * 1024;
            FileInfo f = new FileInfo(this.Filename);
            if (f.Length > fileThreshold)
            {
                m_eFileParseOptions = FileParseOptions.Fast;
                if (MXFInspect.Properties.Settings.Default.PartialLoadWarning)
                {
                    MessageBox.Show(string.Format("The file {0} is larger then the threshold and will be loaded partially.\nA partition will be loaded when expanding the partition in the tree.", this.Filename), "Partial loading active", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


            this.bgwProcess.RunWorkerAsync(this);

            ParentMainForm.EnableUI(false);
        }

        private void PhysicalTree_SelectionChanged(object sender, EventArgs e)
        {
            PhysicalTreeSelectedObject = this.tlvPhysical.SelectedObject as MXFObject;
            var logicalObj = PhysicalTreeSelectedObject?.LogicalWrapper;

            if (PhysicalTreeSelectedObject != null)
            {
                if (!m_fDoNotSelectOther)
                {
                    this.propGrid.SelectedObject = PhysicalTreeSelectedObject;

                    // Try to select this object in the logical list as well
                    m_fDoNotSelectOther = true;
                    this.tlvLogical.RevealAndSelectObject(logicalObj);
                    m_fDoNotSelectOther = false;

                    // Display the hex data
                    ReadData(PhysicalTreeSelectedObject);
                }
            }
            this.btnNext.Enabled = this.btnPrevious.Enabled = (PhysicalTreeSelectedObject != null);
            // TODO this could case the glitch
            ParentMainForm.UpdateMenu();
        }

        /// <summary>
        /// User clicked another item in the logical tree, show the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogicalTree_SelectionChanged(object sender, EventArgs e)
        {
            LogicalTreeSelectedObject = this.tlvLogical.SelectedObject as MXFLogicalObject;
            if (LogicalTreeSelectedObject != null)
            {
                MXFObject obj = LogicalTreeSelectedObject.Object;
                if (obj != null)
                {
                    if (!m_fDoNotSelectOther)
                    {
                        this.propGrid.SelectedObject = obj;


                        // Try to select this item in the main list as well
                        m_fDoNotSelectOther = true;
                        this.tlvPhysical.RevealAndSelectObject(obj);
                        m_fDoNotSelectOther = false;

                        // Display the hex data
                        ReadData(obj);
                    }
                }
                this.btnNext.Enabled = this.btnPrevious.Enabled = (obj != null);
            }
            // TODO this could case the glitch
            ParentMainForm.UpdateMenu();
        }

        /// <summary>
        /// Read the data and display it in the hex window
        /// </summary>
        /// <param name="obj"></param>
        public void ReadData(MXFObject obj)
        {
            StringBuilder sb = new StringBuilder();

            // Cast to KLV
            long readerOffset = obj.Offset;
            long len = (int)obj.Length;
            MXFKLV klv = obj as MXFKLV;
            if (klv != null)
            {
                // Determine real length including BER + Key
                len = (klv.DataOffset - readerOffset) + klv.Length;
            }
            MXFLocalTag lt = obj as MXFLocalTag;
            if (lt != null)
            {
                len = (lt.DataOffset - readerOffset) + lt.Size;
            }

            const int dataLength = 16;
            if (len > 0)
            {
                byte[] data = new byte[len];
                using (MXFReader reader = new MXFReader(this.Filename))
                {
                    reader.Seek(readerOffset);
                    data = reader.ReadArray(reader.ReadByte, data.Length);
                }

                long lines = (len + (dataLength - 1)) / dataLength;
                long offset = 0;
                byte[] dataString = new byte[dataLength + 1];
                for (int n = 0; n < lines; n++)
                {
                    long cnt = dataLength;
                    if (len - offset < dataLength)
                        cnt = len - offset;
                    string hex = BitConverter.ToString(data, (int)offset, (int)cnt).Replace('-', ' ');
                    hex = hex.PadRight(dataLength * 3);

                    for (int m = 0; m < cnt; m++)
                        dataString[m] = data[offset + m];
                    string ascii = System.Text.Encoding.ASCII.GetString(dataString);
                    string asciisafe = "";
                    for (int m = 0; m < cnt && m < ascii.Length; m++)
                    {
                        if (ascii[m] < 0x20)
                            asciisafe += ' ';
                        else
                            asciisafe += ascii[m];
                    }

                    sb.AppendLine(string.Format("{0:0000000000}  {1}  {2}", readerOffset + (n * dataLength), hex, asciisafe));
                    offset += dataLength;
                }
            }
            this.txtHex.Text = sb.ToString();
        }

        /// <summary>
        /// If the newly selected item is of type referenceKey, allow jump on double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            m_selectedObject = e.NewSelection.Value as MXFObject;

            m_currentReference = null;
            if (m_selectedObject != null)
            {
                // Select the reference itself by default
                m_currentReference = m_selectedObject;

                if (m_selectedObject is IResolvable resolvable)
                {
                    if (resolvable.GetReference() != null)
                        m_currentReference = resolvable.GetReference();
                    else
                        m_currentReference = null; // Reset ?? dumb logic?
                }
            }
            this.btnSelectReference.Enabled = (m_currentReference != null);
        }

        /// <summary>
        /// Select the reference
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectReference_Click(object sender, EventArgs e)
        {
            if (m_currentReference != null)
            {
                this.tlvPhysical.RevealAndSelectObject(m_currentReference);
            }
        }

        /// <summary>
        /// Find the next item with the same key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelectNextObject()
        {
            MXFObject selectedObject = this.tlvPhysical.SelectedObject as MXFObject;
            if (selectedObject != null)
            {
                // TODO is the hidefillers boolean really needed?
                MXFObject nextObject = selectedObject.FindNextObjectOfType(selectedObject.GetType(), this.FillerHidden);

                if (nextObject != null)
                {
                    this.tlvPhysical.RevealAndSelectObject(nextObject);
                }

            }
        }

        /// <summary>
        /// Find the previous item in this parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelectPreviousObject()
        {
            MXFObject selectedObject = this.tlvPhysical.SelectedObject as MXFObject;
            if (selectedObject != null)
            {
                // TODO is the hidefillers boolean really needed?
                MXFObject prevObject = selectedObject.FindPreviousObjectOfType(selectedObject.GetType(), this.FillerHidden);

                if (prevObject != null)
                {
                    this.tlvPhysical.RevealAndSelectObject(prevObject);
                }

            }
        }

        private void SetTypeFilter(bool filtered)
        {
            tlvPhysical.SetTypeFilter(filtered);
            ParentMainForm.UpdateMenu();
        }

        private void HideFiller(bool exclude)
        {
            tlvPhysical.HideFillers(exclude);
        }

        /// <summary>
        /// Find the next item with the same key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            SelectNextObject();
        }

        /// <summary>
        /// Find the previous item in this parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            SelectPreviousObject();
        }

        /// <summary>
        /// Worker thread!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            // Open the selected file to read.
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                // Process the file
                this.File = new MXFFile(this.Filename, worker, m_eFileParseOptions);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while opening the file");
            }
        }

        /// <summary>
        /// Fill the tree
        /// </summary>
        private void FillTree()
        {
            try
            {
                this.tlvPhysical.FillTree(this.File.Children.OrderBy(c => c.Offset));
                this.tlvPhysical.HideFillers(this.FillerHidden);

                var logicalList = new List<MXFLogicalObject>() { this.File.LogicalBase };
                this.tlvLogical.FillTree(logicalList);

                this.txtOverall.Text = string.Format("Total objects: {0}", this.File.Descendants().Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while populating the trees");
                this.Close();
            }
        }

        /// <summary>
        /// The progress has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
                        txtOverall.Text = string.Format("{0} - Estimated time: {1} s", currentTask, (estimate * msecPerPercentage) / 1000);
                    }
                    m_lastPercentage = e.ProgressPercentage;
                }
            }
            this.prbProcessing.Value = e.ProgressPercentage;
        }


        /// <summary>
        /// Finished processing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ParentMainForm.EnableUI(true);
            this.prbProcessing.Visible = false;
            this.splitMain.Visible = true;

            FillTree();

            this.tabMain.SelectedIndex = 0;

            FormReport fr = new FormReport(this.File);
            fr.ShowDialog(ParentMainForm);
        }


        /// <summary>
        /// Show/hide help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInfo_CheckedChanged(object sender, EventArgs e)
        {
            this.propGrid.HelpVisible = this.chkInfo.Checked;
        }

        /// <summary>
        /// Collapse all except the partitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CollapseAll()
        {
            if (this.PhysicalViewShown)
            {
                this.tlvPhysical.CollapseAll();
            }
            else
            {
                this.tlvLogical.CollapseAll();
            }
            
        }


        /// <summary>
        /// Apply all user settings
        /// </summary>
        public void ApplyUserSettings()
        {
            this.tlvPhysical.Refresh();
            this.tlvLogical.Refresh();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PhysicalViewShown = tabMain.SelectedTab == tpPhysical;
            this.ParentMainForm.UpdateMenu();
        }
    }
}
