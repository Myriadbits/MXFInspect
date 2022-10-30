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
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFPack : KLVTriplet
    {
        private const string CATEGORYNAME = "MXFPack";
        private const int CATEGORYPOS = 1;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override UL Key { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override KLVLength Length { get; }

        [Browsable(false)]
        public MXFPartition Partition { get; set; }


        public MXFPack(UL key, KLVLength length, long offset) : base(key, length, offset)
        {
            // needed since it is overriden
            Key = key;
            Length = length;
        }

        public override string ToString()
        {
            return $"{Key.SMPTEInformation?.Name ?? Key.Name} [len {this.TotalLength}]";
        }
    }
}
