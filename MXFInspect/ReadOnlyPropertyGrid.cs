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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    /// <summary>
    /// <see cref="https://www.csharp-examples.net/readonly-propertygrid/"/>
    /// </summary>
    public class ReadOnlyPropertyGrid : PropertyGrid
	{
        public ReadOnlyPropertyGrid() : base()
        {
			// this hack changes the color from disabled-grey to
			// almost-black, see: https://stackoverflow.com/a/11183799 
			this.ViewForeColor = Color.FromArgb(1, 0, 0);
		}

		private bool _readOnly = true;

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
			if (selectedObject != null)
			{
				// add a global read only attribute, which works for most of the properties (propgrid showing in non-bold)
				TypeDescriptor.AddAttributes(selectedObject, new ReadOnlyAttribute(isReadOnly));

				// for some properties and child properties add readonly attribute additionally in this way
				var propList = TypeDescriptor.GetProperties(selectedObject)
										.Cast<PropertyDescriptor>()
										.Where(prop => prop.IsBrowsable);

				var childpropList = propList.SelectMany(o => o.GetChildProperties().Cast<PropertyDescriptor>())
										.Where(prop => prop.IsBrowsable);

				var entireList = propList.Concat(childpropList).Distinct();

				foreach (PropertyDescriptor pd in entireList)
				{
					pd.SetReadOnlyAttribute(true);
				}

				this.Refresh();
			}
		}
	}
}
