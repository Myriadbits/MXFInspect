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
using System.ComponentModel;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
	public class ReadOnlyPropertyGrid : PropertyGrid
	{
		private bool _readOnly;
		public bool ReadOnly
		{
			get { return _readOnly; }
			set
			{
				_readOnly = value;
				this.SetObjectAsReadOnly(this.SelectedObject, _readOnly);
			}
		}

		protected override void OnSelectedObjectsChanged(EventArgs e)
		{
			this.SetObjectAsReadOnly(this.SelectedObject, this._readOnly);
			base.OnSelectedObjectsChanged(e);
		}

		private void SetObjectAsReadOnly(object selectedObject, bool isReadOnly)
		{
			if (this.SelectedObject != null)
			{
				TypeDescriptor.AddAttributes(this.SelectedObject, new Attribute[] { new ReadOnlyAttribute(_readOnly) });
				this.Refresh();
			}
		}
	}

}
