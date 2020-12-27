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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
				//TypeDescriptor.AddAttributes(selectedObject.GetType(), new ReadOnlyAttribute(true));

				//TypeDescriptor.AddAttributes(selectedObject, new ShouldSerializeAttribute)

				//if(selectedObject is MXFKLV klv)
				//            {
				//	TypeDescriptor.AddAttributes(klv.Key, new ReadOnlyAttribute(true));
				//}
				//TypeDescriptor.AddAttributes(selectedObject, new ReadOnlyAttribute(true));
				//TypeDescriptor.GetProperties(this)["StringProperty"].SetReadOnlyAttribute(!editable);

				TypeDescriptor.AddAttributes(selectedObject, new ReadOnlyAttribute(true));
				var list = TypeDescriptor.GetProperties(selectedObject);

				propExpandAndReadOnly(list);

				//foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(selectedObject))
    //            {
    //                prop.SetReadOnlyAttribute(isReadOnly);
    //                //var childProp = prop.PropertyType.GetProperties();
    //                //               foreach (PropertyDescriptor cprop in childProp)
    //                //               {
    //                //	cprop.SetReadOnlyAttribute(isReadOnly);
    //                //}
    //                //ReadOnlyAttribute attr = prop.Attributes[typeof(ReadOnlyAttribute)] as ReadOnlyAttribute;
    //                //if (attr != null)
    //                //{
    //                //    FieldInfo fi = attr.GetType().GetField("isReadOnly", BindingFlags.NonPublic | BindingFlags.Instance);
    //                //    if (fi != null)
    //                //        fi.SetValue(attr, isReadOnly);
    //                //}
    //            }
                this.Refresh();
			}
		}

		private void propExpandAndReadOnly(PropertyDescriptorCollection propertyDescriptorList)
		{
			var propList = propertyDescriptorList.Cast<PropertyDescriptor>().ToList();
			var childpropList = propList.SelectMany(o => o.GetChildProperties().Cast<PropertyDescriptor>().ToList());

			var entireList = propList.Concat(childpropList).Distinct();

			//propList.First().ser

			foreach (PropertyDescriptor pd in entireList)
			{
				pd.SetReadOnlyAttribute(true);
                //propertyDescriptor.SetExpandableAttribute(true);

    //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(propertyDescriptor.PropertyType);
				//var a = propertyDescriptor.GetChildProperties();
    //            if (propertyDescriptor.IsBrowsable  && properties != null && properties.Count > 0)
    //            {
    //                propExpandAndReadOnly(properties);
    //            }

				//PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(propertyDescriptor.PropertyType);
				//var a = pd.GetChildProperties();
				//if (pd.IsBrowsable && a.Count>0)
				//{
				//	propExpandAndReadOnly(a);
				//}
				//DynamicTypeDescriptor typeDescriptor = new DefaultTypeDesc ((propertyDescriptor.PropertyType.Get);
				//var chilPropertyDescriptorList = properties.Properties.Cast<PropertyDescriptor>().ToList();

			}
		}
	}

	public static class PropertyDescriptorExtensions
	{
		public static void SetReadOnlyAttribute(this PropertyDescriptor p, bool value)
		{
			var attributes = p.Attributes.Cast<Attribute>().Where(x => !(x is ReadOnlyAttribute)).ToList();
			attributes.Add(new ReadOnlyAttribute(value));
			var pi = typeof(MemberDescriptor).GetProperty("AttributeArray", BindingFlags.Instance | BindingFlags.NonPublic);
			pi.SetValue(p, attributes.ToArray());
		}
	}

}
