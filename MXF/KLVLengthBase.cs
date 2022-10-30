using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
    public class KLVLengthBase : ByteArray
    {
        public long Value { get; protected set; }

        public KLVLengthBase(params byte[] bytes) : base(bytes)
        {
        }
    }
}