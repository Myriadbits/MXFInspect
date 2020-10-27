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

namespace Myriadbits.MXF
{
    // http://www.smpte-ra.org/reg/2003/2012 	
    // urn:smpte:ul:060e2b34.01040101.0201012f.00000000
    public enum MXFColorCorrectionFilterWheelSetting
    {
        CrossEffectFilter = 0x00,
        CCFilter3200K = 0x01,
        CCFilter4300K = 0x02,
        CCFilter6300K = 0x03,
        CCFilter5600K = 0x04,
    }
}