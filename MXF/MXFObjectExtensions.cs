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
using System.Linq;
using System.Runtime.CompilerServices;

namespace Myriadbits.MXF
{
    public static class MXFObjectExtensions
    {
        public static bool IsRIPOrRIPEntry(this MXFObject obj)
        {
            return obj is MXFRIP or MXFEntryRIP;
        }

        public static bool IsPartition(this MXFObject obj)
        {
            return obj is MXFPartition;
        }

        public static bool IsFiller(this MXFObject obj)
        {
            return obj is MXFFillerData;
        }

        public static bool IsEssenceElement(this MXFObject obj)
        {
            return obj is MXFEssenceElement;
        }

        public static bool IsFooterPartition(this MXFPartition p)
        {
            return p.PartitionType == PartitionType.Footer;
        }

        public static bool IsHeaderPartition(this MXFPartition p)
        {
            return p.PartitionType == PartitionType.Header;
        }

        public static bool IsIndexLike(this MXFObject obj)
        {
            return obj is MXFIndexTableSegment or MXFEntryDelta or MXFEntryIndex;
        }

        public static bool IsSystemMetaData(this MXFObject obj)
        {
            return obj is MXFSystemMetaDataPack;
        }

        public static bool ContainsIndexTableSegments(this MXFObject obj)
        {
            return obj.Children.Any(c => c is MXFIndexTableSegment);
        }

        public static bool IsMetadataLike(this MXFObject obj)
        {
            return obj is MXFMetadataBaseclass or MXFPackageMetaData or MXFPrimerPack;
        }

        public static bool IsHeaderMetadataLike(this MXFObject obj)
        {
            return obj is MXFMetadataBaseclass or MXFPrimerPack;
        }

        public static bool IsIndexCollection(this MXFObject obj)
        {
            return obj.Descendants().Any() &&
                obj.Descendants().All(d => d is MXFEntryDelta || d is MXFEntryIndex);
        }

        public static long GetTreeMaxOffset(this MXFObject obj)
        {
            return obj.Root().Descendants().Max(o => o.Offset);
        }
    }
}
