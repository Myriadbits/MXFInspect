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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public partial class MXFView : MyFormPage
    {
        private MXFObject m_selectedObject = null;
        private MXFObject m_currentReference = null;
        private Stopwatch m_stopWatch = new Stopwatch();
        private int m_lastPercentage = 0;
        private MXFFile m_MXFFile = null;
        private List<MXFObject> m_filterList = null;
        private bool m_fDoNotSelectOther = false;
        private FileParseOptions m_eFileParseOptions = FileParseOptions.Normal;

        public bool HideFillers { get; set; }
        public string Filename { get; set; }
        public MXFFile File
        {
            get
            {
                return m_MXFFile;
            }
        }

        public MXFView(string fileName)
        {
            this.Filename = fileName;
            this.HideFillers = true;
            InitializeComponent();
            this.Text = fileName;
            this.MainPanel = this.mainPanel; // Do this AFTER the InitializeComponent call!!!
        }

        /// <summary>
        /// Initialize the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MXFView_Load(object sender, EventArgs e)
        {
            ObjectListView.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.Text = this.Filename;
            this.btnSelectReference.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnPrevious.Enabled = false;

            //bool hideFillers = true;
            //FormProgress fp = new FormProgress(this.Filename, this.treeListViewMain, this.txtOverall, hideFillers);
            //fp.StartPosition = FormStartPosition.CenterParent;
            //fp.ShowDialog(this);
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.splitMain.Visible = false;
            this.prbProcessing.Visible = true;

            //bug that means you have to set the desired icon again otherwise it reverts to default when child form is maximised
            this.Icon = Myriadbits.MXFInspect.Properties.Resources.ChildIcon;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.chkInfo.Checked = true;
            this.propGrid.HelpVisible = this.chkInfo.Checked;

            //
            // Set the tree styles
            //OLVColumn col = (OLVColumn)this.treeListViewPhysical.Columns[0];
            //col.Renderer = null;
            //col = (OLVColumn)this.treeListViewPhysical.Columns[1];
            //col.Renderer = this.treeListViewPhysical.TreeColumnRenderer;
            OLVColumn col = (OLVColumn)this.treeListViewLogical.Columns[0];
            col.Renderer = null;
            col = (OLVColumn)this.treeListViewLogical.Columns[1];
            col.Renderer = this.treeListViewLogical.TreeColumnRenderer;

            Pen pen = new Pen(Color.Black, 1.001f);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //this.treeListViewPhysical.TreeColumnRenderer.LinePen = pen;
            this.treeListViewLogical.TreeColumnRenderer.LinePen = pen;

            // Set tree delegates
            //this.treeListViewPhysical.CanExpandGetter = Tree_HasChildren;
            //this.treeListViewPhysical.ChildrenGetter = Tree_ChildGetter;
            this.treeListViewLogical.CanExpandGetter = Tree_HasLogicalChildren;
            this.treeListViewLogical.ChildrenGetter = Tree_LogicalChildGetter;
            //this.treeListViewPhysical.ParentGetter = PhysicalTree_ParentGetter;
            //this.treeListViewLogical.ParentGetter = LogicalTree_ParentGetter;



            // wiring physical treelistview with selectionchanged event
            this.tlvPhysical.SelectionChanged += PhysicalTreeSelectionChanged;



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

            FormMain frmMain = this.MdiParent as FormMain;
            if (frmMain != null)
                frmMain.EnableUI(false);
        }

        private void PhysicalTreeSelectionChanged(object sender, EventArgs e)
        {
            MXFObject obj = this.tlvPhysical.SelectedObject as MXFObject;
            if (obj != null)
            {
                if (!m_fDoNotSelectOther)
                {
                    this.propGrid.SelectedObject = obj;

                    // Try to select this object in the logical list as well
                    m_fDoNotSelectOther = true;
                    SelectObjectInLogicalList(obj);
                    m_fDoNotSelectOther = false;

                    // Display the hex data
                    ReadData(obj);
                }
            }
            this.btnNext.Enabled = this.btnPrevious.Enabled = (obj != null);
        }


        /// <summary>
        /// User clicked another item in the tree, show the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void treeListViewPhysical_SelectionChanged(object sender, EventArgs e)
        //{
        //    MXFObject obj = this.treeListViewPhysical.SelectedObject as MXFObject;
        //    if (obj != null)
        //    {
        //         if (!m_fDoNotSelectOther)
        //        {
        //            this.propGrid.SelectedObject = obj;

        //            // Try to select this object in the logical list as well
        //            m_fDoNotSelectOther = true;
        //            SelectObjectInLogicalList(obj);
        //            m_fDoNotSelectOther = false;

        //            // Display the hex data
        //            ReadData(obj);
        //        }
        //    }
        //    this.btnNext.Enabled = this.btnPrevious.Enabled = (obj != null);
        //}



        /// <summary>
        /// User clicked another item in the LOGICAL tree, show the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListViewLogical_SelectionChanged(object sender, EventArgs e)
        {
            MXFLogicalObject lobj = this.treeListViewLogical.SelectedObject as MXFLogicalObject;
            if (lobj != null)
            {
                MXFObject obj = lobj.Object;
                if (obj != null)
                {
                    if (!m_fDoNotSelectOther)
                    {
                        this.propGrid.SelectedObject = obj;


                        // Try to select this item in the main list as well
                        m_fDoNotSelectOther = true;
                        SelectObjectInPhysicalTree(obj);
                        m_fDoNotSelectOther = false;

                        // Display the hex data
                        ReadData(obj);
                    }
                }
                this.btnNext.Enabled = this.btnPrevious.Enabled = (obj != null);
            }
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
                SelectObjectInPhysicalTree(m_currentReference);
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
                MXFObject nextObject = selectedObject;
                if (this.m_filterList != null)
                {
                    // Filtering is currently active!
                    int index = this.m_filterList.IndexOf(selectedObject);
                    if (index >= 0 && index < this.m_filterList.Count - 1)
                        nextObject = this.m_filterList[index + 1];
                }
                else
                    // TODO is the hidefillers boolean really needed?
                    nextObject = selectedObject.FindNextObjectOfType(selectedObject.GetType(), this.HideFillers);
                SelectObjectInPhysicalTree(nextObject);
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
                MXFObject previousObject = selectedObject;
                if (this.m_filterList != null)
                {
                    // Filtering is currently active!
                    int index = this.m_filterList.IndexOf(selectedObject);
                    if (index >= 1)
                        previousObject = this.m_filterList[index - 1];
                }
                else
                    // TODO is the hidefillers boolean really needed?
                    previousObject = selectedObject.FindPreviousObjectOfType(selectedObject.GetType(), this.HideFillers);
                SelectObjectInPhysicalTree(previousObject);
            }
        }

        public void SetTypeFilter(bool filtered)
        {
            tlvPhysical.SetTypeFilter(filtered);
        }

        public void ExcludeFiller(bool exclude)
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
        /// Select an object in the physical tree
        /// </summary>
        /// <param name="selObject"></param>
        private void SelectObjectInPhysicalTree(MXFObject selObject)
        {
            if (selObject != null)
            {
                if (this.m_filterList != null)
                {
                    // Filtering active, just select
                }
                else
                {
                    // Open entire parent tree and select object
                    tlvPhysical.Reveal(selObject, true);
                }

                this.tlvPhysical.EnsureModelVisible(selObject);
                this.tlvPhysical.RefreshObject(selObject);
            }
        }


        /// <summary>
        /// Select an object in the logical tree
        /// </summary>
        /// <param name="selObject"></param>
        public void SelectObjectInLogicalList(MXFObject selObject)
        {
            if (selObject != null)
            {
                // Open entire parent tree
                // Open entire parent tree and select object
                treeListViewLogical.Reveal(selObject.LogicalWrapper, true);

                // Select the next object
                this.treeListViewLogical.EnsureModelVisible(selObject);
                this.treeListViewLogical.SelectObject(selObject);
                this.treeListViewLogical.RefreshObject(selObject);
            }
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
                this.m_MXFFile = new MXFFile(this.Filename, worker, m_eFileParseOptions);
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
                // File physical tree
                this.tlvPhysical.FillTree(this.m_MXFFile.Children, this.HideFillers);
                
                this.treeListViewLogical.Items.Clear();

                // Add the data
                AddItemsToTree(false);

                this.txtOverall.Text = string.Format("Total objects: {0}", this.m_MXFFile.Descendants().Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while populating the trees");
            }
        }


        /// <summary>
        /// Does this object have children?
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool Tree_HasLogicalChildren(object x)
        {
            MXFLogicalObject mxf = x as MXFLogicalObject;
            if (mxf == null) return false;
            return mxf.Children.Any();
        }

        /// <summary>
        /// Get the childs!
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private IEnumerable Tree_LogicalChildGetter(object x)
        {
            MXFLogicalObject mxf = x as MXFLogicalObject;
            if (mxf == null) return null;
            return mxf.Children.ToArray();
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
            FormMain frmMain = this.MdiParent as FormMain;
            if (frmMain != null)
                frmMain.EnableUI(true);
            this.prbProcessing.Visible = false;
            this.splitMain.Visible = true;

            FillTree();

            this.tabMain.SelectedIndex = 0;

            FormReport fr = new FormReport(this.m_MXFFile);
            fr.ShowDialog(frmMain);
        }

        /// <summary>
        /// Color the stuff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// // TODO fix for logical tree
        private void treeListViewPhysical_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                // Address
                e.SubItem.ForeColor = Color.Gray;
            }
            else if (e.ColumnIndex == 1)
            {
                MXFObject obj = e.Model as MXFObject;
                if (obj != null)
                {
                    if (obj.Type == MXFObjectType.Partition)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_Partition;
                    else if (obj.Type == MXFObjectType.Essence)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_Essence;
                    else if (obj.Type == MXFObjectType.Index)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_IndexTable;
                    else if (obj.Type == MXFObjectType.SystemItem)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_SystemItem;
                    else if (obj.Type == MXFObjectType.RIP)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_RIP;
                    else if (obj.Type == MXFObjectType.Meta)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_MetaData;
                    else if (obj.Type == MXFObjectType.Filler)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_Filler;
                    else if (obj.Type == MXFObjectType.Special)
                        e.SubItem.ForeColor = MXFInspect.Properties.Settings.Default.Color_Special;
                }
            }
        }


        /// <summary>
        /// (re)Fill the tree
        /// </summary>
        private void AddItemsToTree(bool filterCurrentType)
        {
            //MXFObject selObject = this.treeListViewPhysical.SelectedObject as MXFObject;
            //if (filterCurrentType && selObject != null)
            //{
            //    Type selectedType = selObject.GetType();

            //    // Create a new list with the selected items only)
            //    if (this.HideFillers)
            //        this.m_filterList = this.m_MXFFile.Descendants().Where(a => a.GetType() == selectedType && a.Type != MXFObjectType.Filler).ToList();
            //    else
            //        this.m_filterList = this.m_MXFFile.Descendants().Where(a => a.GetType() == selectedType).ToList();
            //    this.treeListViewPhysical.SetObjects(this.m_filterList);
            //    this.txtOverall.Text = string.Format("Number of filtered objects: {0}", this.m_filterList.Count);
            //}
            //else
            //{
            //    this.m_filterList = null;
            //    if (this.m_MXFFile != null)
            //    {
            //        this.treeListViewPhysical.SetObjects(this.m_MXFFile.Children);
            //        this.txtOverall.Text = string.Format("Total objects: {0}", this.m_MXFFile.Descendants().Count());
            //    }
            //    else
            //        this.txtOverall.Text = "";
            }

            //    // Set logical tree
            //    List<MXFLogicalObject> los = new List<MXFLogicalObject>();
            //    if (this.m_MXFFile != null)
            //        los.Add(this.m_MXFFile.LogicalBase);
            //    this.treeListViewLogical.SetObjects(los);


            //    // (Re)-select the selected item
            //    if (selObject != null)
            //        SelectObjectInPhysicalTree(selObject);
            //    else
            //    {
                    // No item selected, just select the first partition
                    //if (this.m_MXFFile != null && this.m_MXFFile.Partitions != null && this.m_MXFFile.Partitions.Count > 0)
                    //{
                    //    SelectObjectInPhysicalTree(this.m_MXFFile.Partitions[0]);
                    //}
            //    }
            //}

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
            this.tlvPhysical.CollapseAll();

            // No item selected, just select the first partition
            if (this.m_MXFFile.Partitions != null && this.m_MXFFile.Partitions.Count > 0)
            {
                this.tlvPhysical.Expand(this.m_MXFFile.Children[0]);
                //foreach (MXFObject obj in this.m_MXFFile.Partitions)
                //	if (!this.treeListViewMain.IsExpanded(obj))
                //		this.treeListViewMain.Expand(obj);
            }
        }


        /// <summary>
        /// Apply all user settings
        /// </summary>
        public void ApplyUserSettings()
        {
            this.tlvPhysical.Refresh();
            this.treeListViewLogical.Refresh();
        }

        //private void treeListViewPhysical_IsHyperlink(object sender, IsHyperlinkEventArgs e)
        //{
        //    if (e.Model is IResolvable resolvable && resolvable.GetReference() != null)
        //    {
        //        e.IsHyperlink = true;
        //        //e.Url = null;
        //    }
        //    else e.IsHyperlink = false;
        //}

        //private void treeListViewPhysical_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
        //{
        //    var resolvable = e.Model as IResolvable;
        //    treeListViewPhysical.SelectObject(resolvable.GetReference());
        //    this.treeListViewPhysical.EnsureModelVisible(resolvable.GetReference());
        //}
    }
}
