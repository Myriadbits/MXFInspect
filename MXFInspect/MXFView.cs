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
using System.Threading;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public partial class MXFView : MyFormPage
    {
        #region Public props

        public MXFFile File { get; private set; }

        public MXFObject PhysicalTreeSelectedObject { get; private set; }
        public MXFLogicalObject LogicalTreeSelectedObject { get; private set; }
        public bool PhysicalViewShown { get; set; } = true;
        public FileInfo FileInfo { get; private set; }

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

        private bool _fillerHidden = true;
        public bool FillerHidden
        {
            get => _fillerHidden;
            set
            {
                this._fillerHidden = value;
                this.HideFiller(value);
            }
        }

        private bool _showPropInfo = false;
        public bool ShowPropertyInfo
        {
            get => _showPropInfo;
            set
            {
                this._showPropInfo = value;
                this.ShowPropInfo(value);
            }
        }

        #endregion

        private FormMain ParentMainForm { get; set; }

        private FileParseMode FileParseMode { get; set; }

        private Stopwatch sw = new Stopwatch();
        private bool m_fDoNotSelectOther = false;



        public MXFView(FileInfo fi, FileParseMode fileParseMode)
        {
            InitializeComponent();
            this.FileInfo = fi;
            this.FileParseMode = fileParseMode;
            this.FillerHidden = true;
            this.MainPanel = this.mainPanel; // Do this AFTER the InitializeComponent call!!!

        }

        /// <summary>
        /// Initialize the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MXFView_Load(object sender, EventArgs e)
        {
            this.ParentMainForm = this.MdiParent as FormMain;

            ObjectListView.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.Text = this.FileInfo.FullName;

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.splitMain.Visible = false;

            // bug that means you have to set the desired icon again otherwise it reverts to default when child form is maximised
            this.Icon = Myriadbits.MXFInspect.Properties.Resources.ChildIcon;

            // wiring treelist selectionchanged events
            this.tlvPhysical.SelectionChanged += PhysicalTree_SelectionChanged;
            this.tlvLogical.SelectionChanged += LogicalTree_SelectionChanged;

            ParentMainForm.EnableUI(false);
            this.ApplyUserSettings();

            var cts = new CancellationTokenSource();
            var prgForm = new FormProgress("Opening file...", cts);
            var overallProgressHandler = new Progress<TaskReport>(prgForm.ReportOverallProgress);
            var singleProgressHandler = new Progress<TaskReport>(prgForm.ReportSingleProgress);
            var progressFormTask = prgForm.ShowDialogAsync();
            sw.Start();

            // Open the selected file to read.
            try
            {
                this.ParentMainForm.SetActivityText($"Opening file file '{this.FileInfo.FullName}'..."); 
                this.File = await MXFFile.CreateAsync(this.FileInfo, overallProgressHandler, singleProgressHandler, cts.Token);

                FillTrees();
                this.splitMain.Visible = true;
                this.tabMain.SelectedIndex = 0;
                this.ParentMainForm.SetActivityText($"Finished reading file '{this.FileInfo.FullName}' in {sw.ElapsedMilliseconds:N0} ms");
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine("Operation aborted by user {0}", ex);
                this.ParentMainForm.SetActivityText(string.Format("File opening aborted by user"));
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while opening the file");
                this.ParentMainForm.SetActivityText(string.Format($"Error while opening the file {this.FileInfo.FullName}"));
                this.Close();
            }
            finally
            {
                // TODO: really needed?
                prgForm.Close();
                await progressFormTask;
                ParentMainForm.EnableUI(true);
            }
        }


        /// <summary>
        /// Fill the tree
        /// </summary>
        private void FillTrees()
        {
            try
            {
                this.tlvPhysical.FillTree(this.File.Children.OrderBy(c => c.Offset));
                this.tlvPhysical.RevealAndSelectObject(this.tlvPhysical.GetFirstPartition());
                this.tlvPhysical.HideFillers(this.FillerHidden);
                this.tlvPhysical.ColumnOffset.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                var logicalList = new List<MXFLogicalObject>() { this.File.LogicalTreeRoot };
                this.tlvLogical.FillTree(logicalList);
                this.tlvLogical.ColumnOffset.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                this.tlvPhysical.ColumnMXFObject.Text = $"Object [{this.File.Descendants().Count():N0} items]";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while populating the trees");
                this.Close();
            }
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

                    // Display the mxfobject as hex dump
                    rtfHexViewer.SetObject(PhysicalTreeSelectedObject);

                    m_fDoNotSelectOther = false;
                }
            }
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
                        // Display the mxfobject as hex dump
                        rtfHexViewer.SetObject(obj);
                        m_fDoNotSelectOther = false;
                    }
                }
            }
            ParentMainForm.UpdateMenu();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PhysicalViewShown = tabMain.SelectedTab == tpPhysical;
            this.ParentMainForm.UpdateMenu();
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
        /// Show/hide property info help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPropInfo(bool showInfo)
        {
            this.propGrid.HelpVisible = showInfo;
        }

        public void SelectNextObject()
        {
            MXFObject selectedObject = this.tlvPhysical.SelectedObject as MXFObject;
            if (selectedObject != null)
            {
                MXFObject nextObject = selectedObject.FindNextObjectOfType(selectedObject.GetType());

                if (nextObject != null)
                {
                    this.tlvPhysical.RevealAndSelectObject(nextObject);
                }

            }
        }

        public void SelectPreviousObject()
        {
            MXFObject selectedObject = this.tlvPhysical.SelectedObject as MXFObject;
            if (selectedObject != null)
            {
                MXFObject prevObject = selectedObject.FindPreviousObjectOfType(selectedObject.GetType());

                if (prevObject != null)
                {
                    this.tlvPhysical.RevealAndSelectObject(prevObject);
                }

            }
        }

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

        public void RevealAndSelect(MXFObject obj)
        {

            if (PhysicalViewShown)
            {
                tlvPhysical.RevealAndSelectObject(obj);
            }
            else
            {
                tlvLogical.RevealAndSelectObject(obj.LogicalWrapper);
            }
        }

        /// <summary>
        /// Apply all user settings
        /// </summary>
        public void ApplyUserSettings()
        {
            this.tlvLogical.SetOffsetStyle(Properties.Settings.Default.ShowOffsetAsHex);
            this.tlvPhysical.SetOffsetStyle(Properties.Settings.Default.ShowOffsetAsHex);
            this.rtfHexViewer.SetOffsetStyle(Properties.Settings.Default.ShowOffsetAsHex);
        }
    }
}
