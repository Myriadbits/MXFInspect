using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXF.KLV
{
    public interface IKLVTriplet<K, L, V>
        where K : KLVKey
        where L : KLVLengthBase
        where V : ByteArray
    {
        public K Key { get; }

        public L Length { get; }

        public V Value { get; }
    }
}
