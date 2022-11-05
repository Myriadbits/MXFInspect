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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
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
                IEnumerable<PropertyDescriptor> entireList = GetPropertyDescriptors(selectedObject);

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

        /// <summary>
        /// Get props and child props descriptors
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <returns></returns>
        private static IEnumerable<PropertyDescriptor> GetPropertyDescriptors(object selectedObject)
        {
            //return TypeDescriptor.GetProperties(selectedObject)
            //                        .Cast<PropertyDescriptor>()
            //                        .Where(propInfo => propInfo.IsBrowsable)
            //                        .GetChildPropertyDescriptors();


            var propList = TypeDescriptor.GetProperties(selectedObject)
                                    .Cast<PropertyDescriptor>()
                                    .Where(prop => prop.IsBrowsable);

            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            list.AddRange(propList);
            
            foreach (var item in propList)
            {
                Debug.WriteLine(item.Name);
                list.AddRange(ReadOnlyPropertyGridHelpers.GetAllChildsProperties(item, list));
            }

            return list;
            //propList = propList.Concat(propList.SelectMany(o => o.GetChildProperties().Cast<PropertyDescriptor>())
            //                        .Where(prop => prop.IsBrowsable));

            //var childchildpropList = childpropList.SelectMany(o => o.GetChildProperties().Cast<PropertyDescriptor>())
            //            .Where(prop => prop.IsBrowsable);

            //var childchildpropList = childpropList.SelectMany(o => o.GetChildProperties().Cast<PropertyDescriptor>())
            //            .Where(prop => prop.IsBrowsable);

            //var entireList = propList.Concat(childchildpropList).Distinct();
            //return entireList;
            //List<Type> visitedTypes = new List<Type>();
            //var list = TypeDescriptor.GetProperties(selectedObject)
            //                        .Cast<PropertyDescriptor>()
            //                        .Where(propInfo => propInfo.IsBrowsable)
            //                        .GetChildPropertyDescriptors(visitedTypes);


            ////PrintAllTypes(selectedObject.GetType(), "", visitedTypes);
            //var s = new List<PropertyDescriptor>();
            ////var l = GetAllProperties(selectedObject.GetType(), "", visitedTypes, s);
            //return TypeDescriptor.GetProperties(selectedObject)
            //                        .Cast<PropertyDescriptor>()
            //                        //.Where(propInfo => propInfo.IsBrowsable)
            //                        .GetChildPropertyDescriptors(visitedTypes);

        }



        ////private List<Type> alreadyVisitedTypes = new List<Type>(); // to avoid infinite recursion
        //public static void PrintAllTypes(Type currentType, string prefix, List<Type> alreadyVisitedTypes)
        //{
        //    if (alreadyVisitedTypes.Contains(currentType)) return;
        //    alreadyVisitedTypes.Add(currentType);
        //    foreach (PropertyInfo pi in currentType.GetProperties())
        //    {
        //        Debug.WriteLine($"{prefix} {pi.PropertyType.Name} {pi.Name}");
        //        if (!pi.PropertyType.IsPrimitive) PrintAllTypes(pi.PropertyType, prefix + "  ", alreadyVisitedTypes);
        //    }
        //}

        //public static IEnumerable<PropertyInfo> GetAll(Type currentType, string prefix, List<Type> alreadyVisitedTypes)
        //{
        //    if (alreadyVisitedTypes.Contains(currentType)) return;
        //    alreadyVisitedTypes.Add(currentType);
        //    foreach (PropertyInfo pi in currentType.GetProperties())
        //    {
        //        Debug.WriteLine($"{prefix} {pi.PropertyType.Name} {pi.Name}");
        //        if (!pi.PropertyType.IsPrimitive) yield return (pi.PropertyType, prefix + "  ", alreadyVisitedTypes);
        //    }
        //}

        //public static IEnumerable<PropertyDescriptor> GetAllProperties(Type currentType, string prefix, List<Type> alreadyVisitedTypes, List<PropertyDescriptor> list)
        //{
        //    if (alreadyVisitedTypes.Contains(currentType)) return list;
        //    alreadyVisitedTypes.Add(currentType);

        //    foreach (var pi in currentType.GetProperties())
        //    {
        //        list.Concat(GetPropertyDescriptors(pi));
        //    }
        //        return list.Concat(GetAllProperties(pi.GetType(), "", alreadyVisitedTypes, list));
        //        //Debug.WriteLine($"{prefix} {pi.PropertyType.Name} {pi.Name}");
        //        //if (!pi.PropertyType.IsPrimitive)
        //        //return GetAllProperties(pi.PropertyType, prefix + "  ", alreadyVisitedTypes, list);
        //    //}
        //    //return list;
        //}

        //public static IEnumerable<PropertyDescriptor> GetPropertyDescriptors(PropertyInfo PropertyInfo)
        //{
        //    return TypeDescriptor.GetProperties(PropertyInfo.DeclaringType).Cast<PropertyDescriptor>();
        //}
    }

    //internal static class ReadOnlyPropertyGridHelpers
    //{
    //    public static IEnumerable<PropertyDescriptor> GetChildPropertyDescriptors(this IEnumerable<PropertyDescriptor> l, IEnumerable<Type> alreadyVisitedTypes)
    //    {
    //        alreadyVisitedTypes = alreadyVisitedTypes.Concat(l.Select(s => s.GetType())).Distinct();

    //        if (l.Any())
    //        {
    //            return l.Distinct().Concat(l.SelectMany(pi => pi.GetChildProperties()
    //                                        .Cast<PropertyDescriptor>()
    //                                        .Where(propInfo => propInfo.IsBrowsable && !alreadyVisitedTypes.Contains(pi.GetType()))
    //                                ).GetChildPropertyDescriptors(alreadyVisitedTypes)
    //                                .Distinct());
    //        }
    //        else return l;

    //    }
    //}
}
