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

using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using Myriadbits.MXF.Utils;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFPack : KLVTriplet<UL, KLVBERLength, ByteArray>
    {
        private const string CATEGORYNAME = "MXFPack";
        private const int CATEGORYPOS = 1;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Consecutive pack number")]
        public long Number { get; set; }

        // TODO remove this dependency
        [Browsable(false)]
        public MXFPartition Partition { get; set; }

        public MXFPack(UL key, KLVBERLength length, long offset, Stream stream) : base(key, length, offset, stream)
        {
        }

        public MXFPack(MXFPack pack) : base(pack.Key, pack.Length, pack.Offset, pack.Stream)
        {
        }

        public override string ToString()
        {
            return $"{Key.SMPTEInformation?.Name ?? Key.Name} [len {this.Length.Value}]";
        }
    }
}
