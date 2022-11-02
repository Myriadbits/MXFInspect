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

using Myriadbits.MXF.Identifiers;
using System.Text;

namespace Myriadbits.MXF
{
    public class UUID : AUID
    {
        /// <summary>
        /// Universally unique identifier according to ISO 11578.
        /// </summary>
        public enum UUIDVersionType
        {
            Version1 = 1,
            Version2 = 2,
            Version3 = 3,
            Version4 = 4,
            Version5 = 5,
            VersionUnknown
        }
        public UUIDVersionType Version { get; private set; }

        public UUID(params byte[] list) : base(list)
        {
            switch (this[12])
            {
                case >= 0x06:
                case 0x00:
                    Version = UUIDVersionType.VersionUnknown;
                    break;

                default:
                    Version = (UUIDVersionType)(this[13]);
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UUID - { ");
            for (int n = 0; n < this.ArrayLength; n++)
            {
                if (n == 4 || n == 6 || n == 8 || n == 10)
                {
                    sb.Append('-');
                }

                sb.Append(string.Format("{0:X2}", this[n]));
            }
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
