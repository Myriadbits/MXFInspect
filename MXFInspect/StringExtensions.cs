using System;

namespace Myriadbits.MXFInspect
{
    public static class StringExtensions
    {
        public static string Ellipsis(this string src, int maxLength)
        {
            if (maxLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, "maxLength must be greater zero");
            }

            if (src.Length < maxLength)
            {
                return src;
            }

            // Find last '\' character
            int i = src.LastIndexOf(@"\", StringComparison.Ordinal);

            string tokenRight = src.Substring(i, src.Length - i);
            const string tokenMiddle = @"\...";

            if (maxLength < tokenMiddle.Length + tokenRight.Length)
            {
                return tokenMiddle + tokenRight;
            }

            string tokenLeft = src.Substring(0, Math.Min(maxLength - (tokenMiddle.Length + tokenRight.Length), src.Length));
            return tokenLeft + tokenMiddle + tokenRight;
        }
    }
}
