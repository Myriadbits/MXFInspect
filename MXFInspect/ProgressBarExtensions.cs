using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public static class ProgressBarExtensions
    {
        public static void SetValueFast(this ProgressBar progressBar, int value)
        {
            progressBar.Value = value;

            if (value > 0)    // prevent ArgumentException error on value = 0
            {
                progressBar.Value = value - 1;
                progressBar.Value = value;
            }

        }
    }
}
