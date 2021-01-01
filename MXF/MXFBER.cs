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
    public enum BERForm
    {
        ShortForm,
        LongForm,
        Indefinite
    }


    public class MXFBER
    {
        public BERForm Form { get; private set; }
        public int AdditionalOctets { get; private set; }
        public long Size { get; private set; }
        
        public MXFBER(int octets, long size)
        {
            AdditionalOctets = octets;
            Size = size;
            if(octets == -1 && size == -1)
            {
                Form = BERForm.Indefinite;
            }
            else
            {
                Form = (AdditionalOctets > 0) ? BERForm.LongForm : BERForm.ShortForm;
            }     
        }

        public override string ToString()
        {
            return (AdditionalOctets > 0) ? $"{Form}, 1 + {AdditionalOctets} Octets ({Size})" : $"{Form} ({Size})";
        }
    }
}
