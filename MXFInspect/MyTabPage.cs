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

using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
	public class MyTabPage : TabPage
	{
		public Form m_frm;

		public MyTabPage(MyFormPage subFrm)
		{
			this.m_frm = subFrm;
			this.Controls.Add(subFrm.MainPanel);
			this.Text = subFrm.Text;
			this.BackColor = System.Drawing.SystemColors.Window;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				this.m_frm.Dispose();
			base.Dispose(disposing);
		}
	}
}
