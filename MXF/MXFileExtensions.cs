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

using System.Collections.Generic;
using System.Linq;

namespace Myriadbits.MXF
{
    public static class MXFileExtensions
    {
        public static MXFPartition GetHeader(this MXFFile file)
        {
            //TODO should we return all found elements or use single explicitly
            return file.Partitions.SingleOrDefault(p => p.PartitionType == PartitionType.Header);

        }

        public static MXFPartition GetFooter(this MXFFile file)
        {
            return file.Partitions.SingleOrDefault(p => p.PartitionType == PartitionType.Footer);
        }

        public static IEnumerable<MXFPartition> GetBodies(this MXFFile file)
        {
            return file.Partitions.Where(p => p.PartitionType == PartitionType.Body);
        }

        public static IEnumerable<MXFPartition> GetBodiesContainingEssences(this MXFFile file)
        {
            return file.GetBodies().Where(b => b.Children.OfType<MXFEssenceElement>().Any());
        }

        public static MXFCDCIPictureEssenceDescriptor GetPictureDescriptorInHeader(this MXFFile file)
        {
            return file
                    .GetHeader()
                    .Children
                    .OfType<MXFCDCIPictureEssenceDescriptor>()
                    .SingleOrDefault();
        }

        public static IEnumerable<MXFAES3AudioEssenceDescriptor> GetAudioEssenceDescriptorsInHeader(this MXFFile file)
        {
            return file
                .GetHeader()
                .Children
                .OfType<MXFAES3AudioEssenceDescriptor>();
        }

        public static bool IsKAGSizeOfAllPartitionsEqual(this MXFFile file, uint size)
        {
            return file.Partitions.Select(p => p.KagSize).All(s => s == size);
        }

        public static bool AreAllPartitionsOP1a(this MXFFile file)
        {
            MXFKey op1a = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x04, 0x01, 0x01, 0x01, 0x0D, 0x01, 0x02, 0x01, 0x01, 0x01, 0x09, 0x00);
            return file.Partitions.Select(p => p.OP).Any(s => s == op1a);
        }

        public static bool IsFooterClosedAndComplete(this MXFFile file)
        {
            return file.GetFooter().Closed && file.GetFooter().Complete;
        }

        public static bool IsHeaderStatusValid(this MXFFile file)
        {
            return !(file.GetHeader().Closed == false && file.GetHeader().Complete == true);
        }

        public static bool AreEssencesInHeader(this MXFFile file)
        {
            return !file.GetHeader().Children.OfType<MXFEssenceElement>().Any();
        }

        public static bool ISRIPPresent(this MXFFile file)
        {
            return file.RIP != null;
        }

        public static long CountPictureEssences(this MXFPartition partition)
        {
            return partition.Children.OfType<MXFEssenceElement>().Count(e => e.IsPicture);
        }

        public static bool IsPartitionDurationBetween(this MXFPartition partition, long min, long max)
        {
            return partition.CountPictureEssences() >= min &&
                partition.CountPictureEssences() <= max;
        }

        public static bool IsDurationOfBodiesMax240Units(this MXFFile file)
        {
            return file.GetBodiesContainingEssences().All(b => b.IsPartitionDurationBetween(1, 240));
        }

        public static MXFContentStorage GetContentStorage(this MXFFile file)
        {
            // TODO assuming there is only one content storage
            return file.LogicalBase.Children.OfType<MXFContentStorage>().FirstOrDefault();
        }

        //public static MXFMaterialPackage GetFirstMaterialPackage(this MXFContentStorage cs)
        //{
        //    // TODO assuming there is at least one material package linked to the content storage
        //    //var wrapper = 
        //    return cs.Children.Where<MXFMaterialPackage>().FirstOrDefault();
        //}

        //public static IEnumerable<MXFGenericTrack> GetGenericTracks(this MXFGenericPackage package)
        //{
        //    return package.LogicalChildren.OfType<MXFGenericTrack>().OrderBy(t => t.TrackID);
        //}

        //public static MXFSequence GetFirstMXFSequence(this MXFGenericTrack track)
        //{
        //    // TODO should 
        //    return track.LogicalChildren.OfType<MXFSequence>().FirstOrDefault();
        //}


        //public static MXFLogicalObject FindLogicalWrapper(this MXFObject obj)
        //{
        //    // TODO should 
        //    return obj.Root.LogicalChildren.OfType<MXFSequence>().FirstOrDefault();
        //}
    }
}
