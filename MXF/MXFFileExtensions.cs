using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Myriadbits.MXF
{
    public static class MXFFileExtensions
    {
        public static MXFPartition GetHeaderPartition(this MXFFile file)
        {
            return file
                    .FlatList.OfType<MXFPartition>()
                    .SingleOrDefault(p => p.PartitionType == PartitionType.Header);

        }

        public static MXFCDCIPictureEssenceDescriptor GetMXFPictureDescriptorInHeader(this MXFFile file)
        {
            return file
                    .GetHeaderPartition()
                    .Children
                    .OfType<MXFCDCIPictureEssenceDescriptor>()
                    .SingleOrDefault();
        }

        public static IEnumerable<MXFAES3AudioEssenceDescriptor> GetMXFAES3AudioEssenceDescriptor(this MXFFile file)
        {
            return file
                .GetHeaderPartition()
                .Children
                .OfType<MXFAES3AudioEssenceDescriptor>();
        }

        //public static MXFKey FindKey()
        //{

        //}
    }
}
