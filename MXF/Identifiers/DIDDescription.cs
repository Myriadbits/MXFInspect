using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXF.Identifiers
{
    public class DIDDescription
    {
        public int? DataType { get; set; }
        public byte? DID { get; set; }
        public byte? SDID { get; set; }
        public string Status { get; set; }
        public string UsedWhere { get; set; }
        public string Application { get; set; }
        public string LastModifiedTime { get; set; }
    }
}
