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
using Myriadbits.MXF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
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
				this.CustomizeObjectAttributes(this.SelectedObject, _readOnly);
			}
		}

		protected override void OnSelectedObjectsChanged(EventArgs e)
		{
			this.CustomizeObjectAttributes(this.SelectedObject, this._readOnly);
			base.OnSelectedObjectsChanged(e);

		}

		private void CustomizeObjectAttributes(object selectedObject, bool isReadOnly)
		{
			if (selectedObject != null)
            {
                IEnumerable<PropertyDescriptor> entireList = GetProperties(selectedObject);

                foreach (PropertyDescriptor pd in entireList)
                {
                    // add readonly to all properties
                    pd.AddReadOnlyAttribute(isReadOnly);

                    // if marker attribute multiline found -> add a string editor attribute
                    if (pd.HasAttribute<MultiLineAttribute>())
                    {
                        pd.AddAttribute(new EditorAttribute(typeof(StringEditor), typeof(UITypeEditor)));
                    }
                }

                this.Refresh();
            }
        }

        // get props and child props
        private static IEnumerable<PropertyDescriptor> GetProperties(object selectedObject)
        {

            var propList = TypeDescriptor.GetProperties(selectedObject)
                                    .Cast<PropertyDescriptor>()
                                    .Where(prop => prop.IsBrowsable);

            var childpropList = propList.SelectMany(o => o.GetChildProperties().Cast<PropertyDescriptor>())
                                    .Where(prop => prop.IsBrowsable);

            var entireList = propList.Concat(childpropList).Distinct();
            return entireList;
        }
	}
}
