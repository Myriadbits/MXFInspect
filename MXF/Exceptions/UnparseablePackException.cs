#region license
//
// MXF - Myriadbits .NET MXF library. 
// Read MXF Files.
// Copyright (C) 2015 Myriadbits, Jochem Bakker
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// For more information, contact me at: info@myriadbits.com
//
#endregion
using System;

namespace Myriadbits.MXF.Exceptions
{
    public class UnparseablePackException : KLVParsingException
    {
        public MXFPack Pack { get; }

        // TODO save streamPosition in order to localize the exception better
        // needs a new initialization concept as the streamPos cannot be saved/retrieved,
        // since the ctor never completes because of the raised exception
        public long StreamPosition { get; }

        public UnparseablePackException(MXFPack pack, Exception innerException) : base(innerException)
        {
            Pack = pack;
            Offset = pack.Offset;
        }
    }
}
