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
using System.Collections.Generic;
using System.Linq;

namespace Myriadbits.MXF
{
    public static class MXFFileExtensions
    {
        public static IEnumerable<MXFPartition> GetPartitions(this MXFFile file)
        {
            var partitionRoot = file.Children.Last(obj => obj is not MXFRIP);
            return partitionRoot?.Children.OfType<MXFPartition>();
        }

        public static MXFPartition GetHeader(this MXFFile file)
        {
            //TODO should we return all found elements or use single explicitly?
            return file.GetPartitions().SingleOrDefault(p => p.PartitionType == PartitionType.Header);
        }

        public static MXFPartition GetFooter(this MXFFile file)
        {
            return file.GetPartitions().SingleOrDefault(p => p.PartitionType == PartitionType.Footer);
        }

        public static IEnumerable<MXFPartition> GetBodyPartitions(this MXFFile file)
        {
            return file.GetPartitions().Where(p => p.PartitionType == PartitionType.Body);
        }

        public static IEnumerable<MXFPartition> GetBodiesContainingEssences(this MXFFile file)
        {
            return file.GetBodyPartitions().Where(b => b.Children.OfType<MXFEssenceElement>().Any());
        }

        public static MXFCDCIDescriptor GetPictureDescriptorInHeader(this MXFFile file)
        {
            return file
                    .GetHeader()
                    .Children
                    .OfType<MXFCDCIDescriptor>()
                    .SingleOrDefault();
        }

        public static IEnumerable<MXFAES3PCMDescriptor> GetAudioEssenceDescriptorsInHeader(this MXFFile file)
        {
            return file
                .GetHeader()
                .Children
                .OfType<MXFAES3PCMDescriptor>();
        }

        public static bool IsKAGSizeOfAllPartitionsEqual(this MXFFile file, uint size)
        {
            return file.GetPartitions().All(p => p.KagSize == size);
        }

        public static bool AreAllPartitionsOP1a(this MXFFile file)
        {
            UL op1a = new UL(0x06, 0x0E, 0x2B, 0x34, 0x04, 0x01, 0x01, 0x01, 0x0D, 0x01, 0x02, 0x01, 0x01, 0x01, 0x09, 0x00);
            return file.GetPartitions().All(p => p.OperationalPattern == op1a);
        }

        public static bool IsFooterPartitionClosedAndComplete(this MXFFile file)
        {
            return file.GetFooter().IsPartitionClosedAndComplete();
        }

        public static bool IsHeaderPartitionClosedAndComplete(this MXFFile file)
        {
            return !(file.GetHeader().IsPartitionClosedAndComplete());
        }

        public static bool IsPartitionClosedAndComplete(this MXFPartition p)
        {
            return !(p.Closed && p.Complete);
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
            return file.LogicalTreeRoot
                        .Children
                        .OfWrappedType<MXFContentStorage>()
                        .FirstOrDefault()
                        .UnWrapAs<MXFContentStorage>();
        }

        public static MXFMaterialPackage GetFirstMaterialPackage(this MXFContentStorage cs)
        {
            // TODO assuming there is at least one material package linked to the content storage
            return cs.LogicalWrapper
                        .Children
                        .OfWrappedType<MXFMaterialPackage>()
                        .FirstOrDefault()?
                        .UnWrapAs<MXFMaterialPackage>();
        }

        public static IEnumerable<MXFTrack> GetGenericTracks(this MXFPackage package)
        {
            return package.LogicalWrapper
                        .Children
                        .OfWrappedType<MXFTrack>()
                        .UnWrapAs<MXFTrack>();
        }

        public static MXFSequence GetFirstMXFSequence(this MXFTrack track)
        {
            return track.LogicalWrapper
                    .Children.OfWrappedType<MXFSequence>()
                    .FirstOrDefault()?
                    .UnWrapAs<MXFSequence>();
        }

        public static IEnumerable<T> GetDescendantsOfType<T>(this MXFFile file) where T : MXFObject
        {
            file.LoadAllPartitions();
            return file.Descendants().OfType<T>();
        }

        public static void LoadAllPartitions(this MXFFile file)
        {
            foreach (MXFPartition p in file.GetPartitions())
            {
                p.Load();
            }
        }

    }
}
