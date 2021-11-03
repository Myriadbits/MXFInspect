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

using System;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
	public partial class FormMain : Form
	{
		protected StringCollection m_mru = new StringCollection(); // Most Recently Used Files

		static int m_maxMRU = 9;

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

			// Process command line argument
			string[] cmdline = Environment.GetCommandLineArgs();
			if (cmdline.Length > 1)
				OpenFile(cmdline[1]);
		}

		/// <summary>
		/// Enable (or disable) the UI
		/// </summary>
		/// <param name="enable"></param>
		public void EnableUI(bool enable)
		{
			this.MainMenuStrip.Enabled = enable;
			this.toolStrip1.Enabled = enable;
		}

		/// <summary>
		/// Initialize/fill the MRU
		/// </summary>
		protected void FillMRU()
		{
			// Start by clearing all menu items we added 
			for (int n = this.menuFile.DropDownItems.Count - 1; n >= 0; n-- )
			{
				ToolStripItem tsdi = this.menuFile.DropDownItems[n];
				if (tsdi.Tag != null)
					this.menuFile.DropDownItems.Remove(tsdi);
			}

			if (this.m_mru != null)
			{
				int startIndex = this.menuFile.DropDownItems.Count - 1; // Start just before the exit
				if (m_mru.Count > 0)
				{
					ToolStripSeparator tsms = new ToolStripSeparator();
					tsms.Tag = -1; // Set the tag so it will be removed
					this.menuFile.DropDownItems.Insert(startIndex, tsms);
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
						this.menuFile.DropDownItems.Insert(startIndex, tsmi);
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
			ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
			if (tsmi != null)
			{
				// Open the file when valid
				OpenFile(tsmi.Tag as string);
			}
		}

		/// <summary>
		/// Open a new MXF file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuOpenFile_Click(object sender, EventArgs e)
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

			MXFView newView = new MXFView(fileName);
			newView.MdiParent = this;
			newView.Show();
		}


		/// <summary>
		/// Close the current mdi child
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuClose_Click(object sender, EventArgs e)
		{
			this.ActiveMdiChild.Close();
		}


		/// <summary>
		/// Show the about screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuAbout_Click(object sender, EventArgs e)
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

						this.tabMain.Show();//.Visible = true;
					}
				}
				else
				{
					MyTabPage mtp = this.ActiveMdiChild.Tag as MyTabPage;
					this.tabMain.SelectedTab = mtp;

					this.ActiveView.HideFillers = !showFillersToolStripMenuItem.Checked;
					this.ActiveView.FilterCurrentType(this.filterCurrentTypeToolStripMenuItem.Checked);
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

		private void tsmValidationReport_Click(object sender, EventArgs e)
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
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO Ask??
			this.Close();
		}


		/// <summary>
		/// Update the menu staye
		/// </summary>
		private void UpdateMenu()
		{
			bool fEnable = (this.ActiveView != null);
			this.nextItemToolStripMenuItem.Enabled = fEnable;
			this.previousItemToolStripMenuItem.Enabled = fEnable;
			this.filterCurrentTypeToolStripMenuItem.Enabled = fEnable;
			this.showFillersToolStripMenuItem.Enabled = fEnable;
			this.tsmValidationReport.Enabled = fEnable;
			this.tsmiCollapseAll.Enabled = fEnable;
			this.tsbReport.Enabled = fEnable;
			this.tsbFilterCurrent.Enabled = fEnable;
			this.tsbFindNext.Enabled = fEnable;
			this.tsbFindPrevious.Enabled = fEnable;
			this.tsbShowFillers.Enabled = fEnable;
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

		private void nextItemToolStripMenuItem_Click(object sender, EventArgs e) { if (this.ActiveView != null) this.ActiveView.SelectNextObject();	}
		private void previousItemToolStripMenuItem_Click(object sender, EventArgs e) { if (this.ActiveView != null) this.ActiveView.SelectPreviousObject();	}

		private void filterCurrentTypeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.ActiveView != null)
			{
				this.filterCurrentTypeToolStripMenuItem.Checked = !this.filterCurrentTypeToolStripMenuItem.Checked;
				this.tsbFilterCurrent.Checked = this.filterCurrentTypeToolStripMenuItem.Checked;
				this.ActiveView.FilterCurrentType(this.filterCurrentTypeToolStripMenuItem.Checked);
			}
		}

		private void showFillersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.ActiveView != null)
			{
				this.showFillersToolStripMenuItem.Checked = !showFillersToolStripMenuItem.Checked;
				this.tsbShowFillers.Checked = this.showFillersToolStripMenuItem.Checked;
				this.ActiveView.HideFillers = !showFillersToolStripMenuItem.Checked;
				this.ActiveView.FilterCurrentType(this.filterCurrentTypeToolStripMenuItem.Checked);
			}
		}

		/// <summary>
		/// Collapse all except the partitions
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
		/// Show the settings dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormSettings formSettings = new FormSettings();
			if (formSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// Refresh settings
				LoadUserSettings();				
			}
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
	}
}
