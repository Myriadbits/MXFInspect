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


using static Myriadbits.MXF.KLVLength;

namespace Myriadbits.MXF
{
    public class KLVParser
    {
        private readonly MXFReader _reader;
        private long actualOffset = 0;

        public KLVParser(MXFReader reader)
        {
            _reader = reader;
        }

        public KLVTriplet GetNextKLV1()
        {
            UL ul;

            try
            {
                ul = KLVKeyParser.ParseUL(_reader);
            }
            catch (System.Exception)
            {
                SeekForNextPotentialKey();
                GetNextKLV();
                throw;
            }
            finally
            {
                var length = KLVLengthParser.ParseKLVLength(_reader, LengthEncodings.BER);
                //return new KLV(ul, length, null);
            }

            return null;
        }


        public KLVTriplet GetNextKLV()
        {
            var ul = KLVKeyParser.ParseUL(_reader);
            var length = KLVLengthParser.ParseKLVLength(_reader, LengthEncodings.BER);
            var klv = new KLVTriplet(ul, length, actualOffset);

            // advance file position
            actualOffset = actualOffset + klv.TotalLength;
            _reader.Seek(actualOffset);

            return klv;
        }

        private bool SeekForNextPotentialKey()
        {
            int foundBytes = 0;

            // TODO implement Boyer-Moore algorithm
            while (!_reader.EOF)
            {
                if (_reader.ReadByte() == UL.ValidULPrefix[foundBytes])
                {
                    foundBytes++;

                    if (foundBytes == 4)
                    {
                        _reader.Seek(_reader.Position - 4);
                        return true;
                    }
                }
                else
                {
                    foundBytes = 0;
                }
            }
            // TODO what does the caller have to do in this case?
            return false;
        }

    }
}
