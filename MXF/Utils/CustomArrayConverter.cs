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

using Myriadbits.MXF.Utils;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Myriadbits.MXF
{
    /// <summary>
    /// Provides a base implementation for array converter classes and sets its expandable array 
    /// components to read-only, as the default value is NOT. 
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/36466732/how-to-make-a-readonly-expandable-properties-in-the-winforms-propertygrid"/>
    public abstract class CustomArrayConverter : ArrayConverter
    { 
        // set the array properties to readonly by attaching a readonly attribute to the property descriptor
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var props = base.GetProperties(context, value, attributes);
            foreach (PropertyDescriptor pd in props)
            {
                pd.AddReadOnlyAttribute(true);
            }
            return props;
        }

        protected string ArrayToString<T>(T[] array, string separator, Func<object, string> formatFunction)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            for (int n = 0; n < array.Length; n++)
            {
                if (n > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(formatFunction(array[n]));
            }
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
