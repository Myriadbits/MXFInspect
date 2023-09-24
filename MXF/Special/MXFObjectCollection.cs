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

namespace Myriadbits.MXF
{
    /// <summary>
    /// Serves as parent node for an object collection
    /// </summary>
    public class MXFObjectCollection : MXFObject
    {
        [Browsable(false)]
        public string Name { get; set; }

        /// <summary>
        /// MXF Object constructor
        /// </summary>
        public MXFObjectCollection(string name, long offset) : base(offset)
        {
            Name = name;
            TotalLength = 0;
        }

        /// <summary>
        /// Some output
        /// </summary>
        public override string ToString()
        {
            return $"{this.Name} [{this.Children.Count} items]";
        }
    }
}
