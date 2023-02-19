﻿#region license
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

using Myriadbits.MXF.Utils;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Myriadbits.MXF
{
    /// <summary>
    /// Provides a type converter based on the array converter, that shows a byte array
    /// as hex string (e.g. { 00.0A.C1...} )
    /// </summary>
    public class ByteArrayConverter : CustomArrayConverter<byte>
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        { 
            if (destinationType == typeof(string) && value != null && (value is byte[] arr))
            {
                return ArrayToString(arr, ".", FormatByte);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


        private string FormatByte(byte value)
        {
            return string.Format("{0:X2}", value);
        }
        

    }
}
