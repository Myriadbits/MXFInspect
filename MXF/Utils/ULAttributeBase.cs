﻿#region license
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

using Myriadbits.MXF.Identifiers;
using System;

namespace Myriadbits.MXF
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class ULBaseAttribute : Attribute
    {
        public string SMPTEULString { get; set; }
        public ByteArray UL { get; protected set; }


        public ULBaseAttribute(string smpteULString)
        {
            // TODO instead of throwing if the string is not a smpteUL just log 
            SMPTEULString = smpteULString;
            UL = SMPTERegisters.GetByteArrayFromSMPTEULString(smpteULString);
        }
    }
}