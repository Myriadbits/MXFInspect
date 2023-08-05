using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF.Exceptions
{
    public class NotAnMXFFileException : KLVParsingException
    {
        public NotAnMXFFileException(string message, long offset, Exception innerException) : base(message, offset, innerException)
        {
        }
    }
}