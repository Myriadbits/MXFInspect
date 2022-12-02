using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXF
{
    public static class Helper
    {

        public static int GetDigitCount(long number)
        {
            number = Math.Abs(number);
            int digits = 1;
            while ((number /= 10) >= 1)
            {
                digits++;
            }
            return digits;
        }
    }
}
