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

using System.ComponentModel;
using System.Linq;

namespace Myriadbits.MXF

{
    /// <summary>
    /// The Run-In sequence shall be less than 65536 bytes long and shall not contain the first 11 bytes of the
    /// Partition Pack label.
    /// </summary>
    public class MXFRunIn : MXFObject
    {
        [Browsable(false)]
        public string Name { get; set; }

        public MXFRunIn(long length) : base(0)
        {
            TotalLength = length;
        }

        public override string ToString()
        {
            if (!this.Children.Any())
            {
                return $"Run-In [len {this.TotalLength}]";
            }
            else
            {
                return $"Run-In [{this.Children.Count} items]";
            }

        }
    }
}
