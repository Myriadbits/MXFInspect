using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF.Exceptions
{
    public class NotAnMXFFileException : KLVParsingException
    {
        // TODO does an offset here make really sense?
        public NotAnMXFFileException(string message, long offset, Exception innerException) : base(message, offset, innerException)
        {
        }
    }
}