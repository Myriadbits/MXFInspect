using System;

namespace Myriadbits.MXF.KLV
{
    public class KLVLength : KLVLengthBase
    {
        public enum LengthEncodings
        {
            OneByte = 1,
            TwoBytes = 2,
            FourBytes = 4,
        }

        public LengthEncodings LengthEncoding { get; }

        public KLVLength(LengthEncodings lengthEncoding, long lengthValue, params byte[] bytes) : base(bytes)
        {
            LengthEncoding = lengthEncoding;
            Value = lengthValue;

            long calculatedLengthValue = bytes.ToLong();
            
            // TODO do we need to check if each byte does not exceed 0x7F?
            if (bytes.Length != (int)lengthEncoding)
            {
                throw new ArgumentException($"Declared length encoding ({lengthEncoding}) does not correspond to given array length ({bytes.Length})");
            }
            else if (calculatedLengthValue != Value)
            {
                throw new ArgumentException($"Byte array value ({calculatedLengthValue}) does not match with given length value ({Value})");
            }
        }

        public override string ToString()
        {
            return $"{LengthEncoding}, ({Value})";
        }
    }
}
