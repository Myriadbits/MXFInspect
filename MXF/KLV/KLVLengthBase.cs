using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF.KLV
{
    public abstract class KLVLengthBase : ByteArray
    {
        /// <summary>
        /// Gets the length value of a Length Encoding
        /// </summary>
        public long Value { get; protected set; }

        public KLVLengthBase(params byte[] bytes) : base(bytes)
        {
        }
    }
}