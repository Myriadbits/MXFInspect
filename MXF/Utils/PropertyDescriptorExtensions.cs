﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
#region license
//
// MXF - Myriadbits .NET MXF library. 
// Read MXF Files.
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

namespace Myriadbits.MXF.Utils
{
    public static class PropertyDescriptorExtensions
    {
        //public static IEnumerable<Attribute> GetAllAttributes(this PropertyDescriptor prop)
        //{
        //    return prop.GetAttributes<Attribute>();
        //}

        //public static IEnumerable<T> GetAttributes<T>(this PropertyDescriptor prop)
        //{
        //    return prop.Attributes.OfType<T>();
        //}

        //public static void AddReadOnlyAttribute(this PropertyDescriptor p, bool value)
        //{
        //    var attributes = p.Attributes.OfType<Attribute>().Where(x => !(x is ReadOnlyAttribute)).ToList();
        //    attributes.Add(new ReadOnlyAttribute(value));
        //    var pi = typeof(MemberDescriptor).GetProperty("AttributeArray", BindingFlags.Instance | BindingFlags.NonPublic);
        //    pi.SetValue(p, attributes.ToArray());
        //}

        //public static void AddDescriptionAttribute(this PropertyDescriptor p, DescriptionAttribute descAttr)
        //{
        //    var attributes = p.Attributes.OfType<Attribute>().Where(x => !(x is DescriptionAttribute)).ToList();
        //    attributes.Add(descAttr);
        //    var pi = typeof(MemberDescriptor).GetProperty("AttributeArray", BindingFlags.Instance | BindingFlags.NonPublic);
        //    pi.SetValue(p, attributes.ToArray());
        //}

        public static void AddAttribute<T>(this PropertyDescriptor p, T newAttribute) where T: Attribute
        {
            var attributes = p.Attributes.OfType<Attribute>().Where(x => !(x is T)).ToList();
            attributes.Add(newAttribute);
            var pi = typeof(MemberDescriptor).GetProperty("AttributeArray", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(p, attributes.ToArray());
        }
    }
}
