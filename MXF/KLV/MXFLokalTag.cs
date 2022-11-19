using Myriadbits.MXF.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Myriadbits.MXF.KLV.KLVLength;
using static Myriadbits.MXF.KLVKey;

namespace Myriadbits.MXF.KLV
{
    public class MXFLokalTag : KLVTriplet<KLVKey, KLVLength, ByteArray>
    {
        // TODO add Alias Universal Label?
        //public AUID AliasUID { get; private set; }
        
        public MXFLokalTag(KLVKey key, KLVLength length, long offset) : base(key, length, offset)
        {
            // check passed parameters 
            if (Key.KeyLength != KeyLengths.TwoBytes)
            {
                throw new ArgumentException($"The key for a local tag must be two bytes long, instead is: {Key.KeyLength}.");
            }

            if (Length.LengthEncoding != LengthEncodings.TwoBytes)
            {
                throw new ArgumentException($"The length encoding for a local tag must be two bytes long, instead is: {Length.LengthEncoding}.");
            }
        }
    }
}
