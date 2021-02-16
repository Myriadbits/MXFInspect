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

using System;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
	public partial class AboutForm : Form
	{
		/// <summary>
		/// Constructor, duh
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Click on logo, is open myriadbits website
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbLogo_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.myriadbits.com");
		}

		/// <summary>
		/// Show GPL license when button is clicked (first hide all other info)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.txtLicense.Text = MXFInspect.Properties.Resources.LICENSE;

			bool vis = true;
			if (this.txtLicense.Visible)
				vis = false;

			this.lblTitle.Visible = !vis;
			this.lblVersion.Visible = !vis;
			this.lblCopyright.Visible = !vis;
			this.llMyriadbits.Visible = !vis;
			this.txtLicense.Visible = vis;
		}

		/// <summary>
		/// Link clicked, show myriadbits
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void llMyriadbits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.myriadbits.com");
		}

		/// <summary>
		/// Close is close
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bntClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Display correct assembly version number
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AboutForm_Load(object sender, EventArgs e)
		{
			this.lblVersion.Text = string.Format("Version: {0}", typeof(AboutForm).Assembly.GetName().Version.ToString());
		}

		/// <summary>
		/// Display objectlistview home page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://objectlistview.sourceforge.net/cs/index.html");
		}
	}
}
