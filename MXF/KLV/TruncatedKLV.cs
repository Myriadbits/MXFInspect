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
using System.Security.Cryptography;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TruncatedKLV : KLVTriplet
    {

        private const string CATEGORYNAME = "Truncated KLV triplet";
        private const int CATEGORYPOS = 2;

        // override as the declared length is longer than stream, as this KLV is truncated
        public override long TotalLength { get => Stream.Length; }

        //[SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        //public long RealLength { get => Stream.Length - Key.ArrayLength - Length.ArrayLength; }

        public TruncatedKLV(KLVKey ul, ILength length, long offset, Stream stream) : base(ul, length, offset, stream)
        {
        }

        public override string ToString()
        {
            string keyString = (Key is UL ul) ? ul.ToString(true): Key.ToString();
            return $"Truncated KLV triplet - {keyString} [len {this.Length.Value}]";
        }
    }
}
