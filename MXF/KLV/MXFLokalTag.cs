using Myriadbits.MXF.Identifiers;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
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
        public AUID AliasUID { get; set; }

        public UInt16 TagValue { get { return (UInt16)((UInt16)(Key[0] << 8) + Key[1]); }}
        
        public MXFLokalTag(KLVKey key, KLVLength length, long offset, Stream stream) : base(key, length, offset, stream)
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


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (AliasUID is UL ul)
            {
                sb.Append($"LocalTag {this.Key:X4} [len {this.Length.Value}] -> {ul.Name} ");
            }
            else
            {
                sb.Append($"LocalTag {this.Key:X4} [len {this.Length.Value}] -> <Unknown tag> ");
            }
            return sb.ToString();
        }
    }
}
