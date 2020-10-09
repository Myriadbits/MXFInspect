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

namespace Myriadbits.MXF
{
    //namespace: http://www.smpte-ra.org/reg/2003/2012 	
    //urn:smpte:ul:060e2b34.01040101.02010126.00000000
    public enum MXFUserDataMode
    {
        NotDefined = 0x00,
        _192BitBlockStructure = 0x01,
        AES18 = 0x02,
        UserDefined = 0x03,
        IEC = 0x04,
        Metadata = 0x05,
        Reserved0 = 0x06,
        Reserved1 = 0x07,
        Reserved2 = 0x08,
        Reserved3 = 0x09,
        Reserved4 = 0x010,
        Reserved5 = 0x011,
        Reserved6 = 0x012,
        Reserved7 = 0x013,
        Reserved8 = 0x014,
        Reserved9 = 0x015,
    }
}
