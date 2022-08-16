namespace Myriadbits.MXF.KLV
{
    public static class ByteArrayExtensions
    {
        public static long ToLong(this byte[] theBytes)
        {
            long lengthValue = 0;
            for (int i = 0; i < theBytes.Length; i++)
            {
                lengthValue = lengthValue << 8 | theBytes[i];
            }
            return lengthValue;
        }
    }
}
