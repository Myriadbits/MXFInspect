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
using System;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public partial class FormMain : Form
    {
        protected StringCollection m_mru = new StringCollection(); // Most Recently Used Files

        static readonly int m_maxMRU = 9;

        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            // Initialize the MRU
            this.m_mru = Properties.Settings.Default.MRU;
            FillMRU();
            this.UpdateMenu();
            this.tslVersion.Text = string.Format("Version: {0}", typeof(FormMain).Assembly.GetName().Version.ToString());

            RefreshStatusStrip();
        }

        /// <summary>
        /// Enable (or disable) the UI
        /// </summary>
        /// <param name="enable"></param>
        public void EnableUI(bool enable)
        {
            this.MainMenuStrip.Enabled = enable;
            this.toolStrip.Enabled = enable;
        }

        /// <summary>
        /// Initialize/fill the MRU
        /// </summary>
        protected void FillMRU()
        {
            // Start by clearing all menu items we added 
            for (int n = this.tsmiFile.DropDownItems.Count - 1; n >= 0; n--)
            {
                ToolStripItem tsdi = this.tsmiFile.DropDownItems[n];
                if (tsdi.Tag != null)
                    this.tsmiFile.DropDownItems.Remove(tsdi);
            }

            if (this.m_mru != null)
            {
                int startIndex = this.tsmiFile.DropDownItems.Count - 1; // Start just before the exit
                if (m_mru.Count > 0)
                {
                    ToolStripSeparator tsms = new ToolStripSeparator();
                    tsms.Tag = -1; // Set the tag so it will be removed
                    this.tsmiFile.DropDownItems.Insert(startIndex, tsms);
                    for (int n = 0; n < m_mru.Count; n++)
                    {
                        ToolStripMenuItem tsmi = new ToolStripMenuItem(string.Format("&{0} {1}", n + 1, m_mru[n]));
                        if (n < 9)
                        {
                            // Only show the key, when there is a key (0-9)
                            Keys key = (Keys)((49 + n) + Keys.Alt);
                            tsmi.ShortcutKeys = key;
                            tsmi.ShowShortcutKeys = true;
                        }
                        tsmi.Click += menuOpenRecentFile_Click;
                        tsmi.Tag = m_mru[n];
                        this.tsmiFile.DropDownItems.Insert(startIndex, tsmi);
                        startIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// Add a new name to the MRU
        /// </summary>
        protected void AddFileToMRU(string fileName)
        {
            if (this.m_mru == null)
                this.m_mru = new StringCollection();
            // Remove all same filenames
            this.m_mru.Remove(fileName);

            // Insert the new one
            this.m_mru.Insert(0, fileName);

            // If too many, remove the oldest
            if (this.m_mru.Count > m_maxMRU)
                this.m_mru.RemoveAt(m_maxMRU);
            MXFInspect.Properties.Settings.Default.MRU = this.m_mru;
            MXFInspect.Properties.Settings.Default.Save();
            FillMRU();
        }


        /// <summary>
        /// Open a new MXF file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuOpenRecentFile_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem tsmi)
            {
                if (tsmi.Tag is string path)
                {
                    if (File.Exists(path))
                    {
                        // Open the file when valid
                        OpenFile(path);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("The path \"{0}\" does not seem to exist anymore on disk.", path), "Open recent file ...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.m_mru.Remove(path);
                        FillMRU();
                    }
                }
            }
        }

        /// <summary>
        /// Open a new MXF file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MXF files (.mxf)|*.mxf|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // And open the file
                OpenFile(openFileDialog.FileName);
            }
        }


        /// <summary>
        /// Really open a MXF file
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenFile(string fileName)
        {
            // Update the MRU
            AddFileToMRU(fileName);

            var fileParseMode = DetermineFileParseMode(fileName);

            if (fileParseMode == FileParseMode.Partial && MXFInspect.Properties.Settings.Default.PartialLoadWarning)
            {
                MessageBox.Show(string.Format("The file {0} is larger then the threshold and will be loaded partially." +
                    "\nA partition will be loaded when expanding the partition in the tree.", fileName),
                    "Partial loading active",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            MXFView newView = new MXFView(fileName, fileParseMode);
            newView.MdiParent = this;
            newView.Show();
        }

        private FileParseMode DetermineFileParseMode(string fileName)
        {
            // Determine the filesize
            long fileThreshold = ((long)MXFInspect.Properties.Settings.Default.PartialLoadThresholdMB) * 1024 * 1024;
            FileInfo f = new FileInfo(fileName);

            // if setting is no partial load at all then threshold is negative
            if (f.Length > fileThreshold && fileThreshold >= 0)
            {
                return FileParseMode.Partial;
            }
            else return FileParseMode.Full;
        }


        /// <summary>
        /// Close the current mdi child
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiClose_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
        }


        /// <summary>
        /// Show the about screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        /// <summary>
        /// When a different MDI child is activated, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                this.tabMain.Visible = false; // If no any child form, hide tabControl
            else
            {
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized; // Child form always maximized

                // If child form is new and no has tabPage, create new tabPage
                if (this.ActiveMdiChild.Tag == null)
                {
                    MyFormPage mfp = this.ActiveMdiChild as MyFormPage;
                    if (mfp != null)
                    {
                        MyTabPage newPage = new MyTabPage(mfp);
                        this.tabMain.TabPages.Add(newPage);

                        this.tabMain.SelectedTab = newPage;

                        this.ActiveMdiChild.Tag = newPage;
                        this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(ActiveMdiChild_FormClosed);

                        this.tabMain.Show();
                    }
                }
                else
                {
                    MyTabPage mtp = this.ActiveMdiChild.Tag as MyTabPage;
                    this.tabMain.SelectedTab = mtp;
                }
            }
            this.UpdateMenu();
        }

        // If child form closed, remove tabPage
        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((sender as Form).Tag as TabPage).Dispose();
        }

        /// <summary>
        /// Different tab is selected, change the active MDI child
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedTab != null)
            {
                MyTabPage mtp = this.tabMain.SelectedTab as MyTabPage;
                if (mtp != null)
                    mtp.m_frm.Activate();
            }
        }

        private void tsmiValidationReport_Click(object sender, EventArgs e)
        {
            if (this.ActiveView != null)
            {
                FormReport fr = new FormReport(this.ActiveView.File);
                fr.ShowDialog();
            }
        }


        /// <summary>
        /// Exit button pressed, quit the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Update the menu based on the current active view
        /// </summary>
        public void UpdateMenu()
        {
            if (this.ActiveView == null)
            {
                // meaning no file open

                this.tsmiClose.Enabled = false;

                this.tsmiCollapseAll.Enabled = false;
                this.tsbCollapseAll.Enabled = false;

                this.tsmiFindNextItem.Enabled = false;
                this.tsbFindNextItem.Enabled = false;

                this.tsbFindPreviousItem.Enabled = false;
                this.tsmiFindPreviousItem.Enabled = false;

                this.tsmiValidationReport.Enabled = false;
                this.tsbValidationReport.Enabled = false;

                this.tsmiFilterCurrentType.Enabled = false;
                this.tsbFilterCurrentType.Enabled = false;
                this.tsbFilterCurrentType.Checked = false;
                this.tsmiFilterCurrentType.Checked = false;

                this.tsmiShowFillers.Enabled = false;
                this.tsbShowFillers.Enabled = false;
                this.tsbShowFillers.Checked = false;
                this.tsmiShowFillers.Checked = false;

                this.tsmiShowPropInfo.Enabled = false;
                this.tsbShowPropInfo.Enabled = false;
            }
            else
            {
                this.tsmiClose.Enabled = true;

                this.tsmiCollapseAll.Enabled = true;
                this.tsbCollapseAll.Enabled = true;

                this.tsmiFindNextItem.Enabled = this.ActiveView.PhysicalViewShown && this.ActiveView.PhysicalTreeSelectedObject != null;
                this.tsbFindNextItem.Enabled = this.ActiveView.PhysicalViewShown && this.ActiveView.PhysicalTreeSelectedObject != null;

                this.tsbFindPreviousItem.Enabled = this.ActiveView.PhysicalViewShown && this.ActiveView.PhysicalTreeSelectedObject != null;
                this.tsmiFindPreviousItem.Enabled = this.ActiveView.PhysicalViewShown && this.ActiveView.PhysicalTreeSelectedObject != null;

                this.tsmiValidationReport.Enabled = true;
                this.tsbValidationReport.Enabled = true;


                this.tsmiFilterCurrentType.Enabled = this.ActiveView.PhysicalViewShown && this.ActiveView.PhysicalTreeSelectedObject != null;
                this.tsbFilterCurrentType.Enabled = this.ActiveView.PhysicalViewShown && this.ActiveView.PhysicalTreeSelectedObject != null;
                this.tsbFilterCurrentType.Checked = this.ActiveView.FilterCurrentType;
                this.tsmiFilterCurrentType.Checked = this.ActiveView.FilterCurrentType;

                this.tsmiShowFillers.Enabled = true;
                this.tsbShowFillers.Enabled = true;
                this.tsbShowFillers.Checked = !this.ActiveView.FillerHidden;
                this.tsmiShowFillers.Checked = !this.ActiveView.FillerHidden;

                this.tsmiShowPropInfo.Enabled = true;
                this.tsbShowPropInfo.Enabled = true;

                this.tsmiShowPropInfo.Checked = this.ActiveView.ShowPropertyInfo;
                this.tsbShowPropInfo.Checked = this.ActiveView.ShowPropertyInfo;
            }

        }

        /// <summary>
        /// Returns the current active view
        /// </summary>
        /// <returns></returns>
        public MXFView ActiveView
        {
            get
            {
                if (this.tabMain.Visible)
                {
                    if (this.tabMain.SelectedTab != null)
                    {
                        MyTabPage mtp = this.tabMain.SelectedTab as MyTabPage;
                        MXFView view = mtp.m_frm as MXFView;
                        return view;
                    }
                }
                return null;
            }
        }


        /// <summary>
        /// Buttons/command that are forwarded to the current active view
        /// </summary>

        private void tsmiFindNextItem_Click(object sender, EventArgs e) { if (this.ActiveView != null) this.ActiveView.SelectNextObject(); }
        private void tsmiFindPreviousItem_Click(object sender, EventArgs e) { if (this.ActiveView != null) this.ActiveView.SelectPreviousObject(); }

        private void tsmiFilterCurrentType_Click(object sender, EventArgs e)
        {
            if (this.ActiveView != null)
            {
                this.tsmiFilterCurrentType.Checked = !this.tsmiFilterCurrentType.Checked;
                this.tsbFilterCurrentType.Checked = this.tsmiFilterCurrentType.Checked;
                this.ActiveView.FilterCurrentType = this.tsmiFilterCurrentType.Checked;
            }
        }

        private void showFillersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveView != null)
            {
                this.tsmiShowFillers.Checked = !tsmiShowFillers.Checked;
                this.tsbShowFillers.Checked = this.tsmiShowFillers.Checked;
                this.ActiveView.FillerHidden = !tsmiShowFillers.Checked;
            }
        }

        /// <summary>
        /// Collapse all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCollapseAll_Click(object sender, EventArgs e)
        {
            if (this.ActiveView != null)
            {
                this.ActiveView.CollapseAll();
            }
        }

        /// <summary>
        /// Show/Hide property info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiShowPropInfo_Click(object sender, EventArgs e)
        {
            if (this.ActiveView != null)
            {
                this.tsmiShowPropInfo.Checked = !tsmiShowPropInfo.Checked;
                this.tsbShowPropInfo.Checked = this.tsmiShowPropInfo.Checked;
                this.ActiveView.ShowPropertyInfo = tsmiShowPropInfo.Checked;
            }
        }

        /// <summary>
        /// Show the settings dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            if (formSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RefreshStatusStrip();

                // Refresh settings
                LoadUserSettings();
            }
        }

        private void RefreshStatusStrip()
        {
            tslPartialLoading.Text =
                Properties.Settings.Default.PartialLoadThresholdMB >= 0 ?
                $"Partial loading enabled (>={Properties.Settings.Default.PartialLoadThresholdMB:N0}MB)" :
                "Partial loading disabled";

            tslOffsetStyle.Text =
                Properties.Settings.Default.ShowOffsetAsHex ?
                "Offset style(hex)" : "Offset style(dec)";
        }


        /// <summary>
        /// Load all (relevant) user settings
        /// </summary>
        private void LoadUserSettings()
        {
            foreach (Form frm in this.MdiChildren)
            {
                MXFView mxfview = frm as MXFView;
                if (mxfview != null)
                {
                    mxfview.ApplyUserSettings();
                }
            }
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                // allow only one mxf file to be dropped

                if (fileNames.Length == 1)
                {
                    e.Effect = DragDropEffects.All;
                }
            }

            else
                e.Effect = DragDropEffects.None;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            OpenFile(fileNames[0]);
        }
    }
}
