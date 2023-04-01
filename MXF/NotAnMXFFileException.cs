using Myriadbits.MXF.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
    public class NotAnMXFFileException : KLVParsingException
    {
        public NotAnMXFFileException(string message) : base(message)
        {

        }
    }
}