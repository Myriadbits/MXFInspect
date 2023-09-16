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

using Myriadbits.MXF.Exceptions;
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using static Myriadbits.MXF.KLVKey;

namespace Myriadbits.MXF
{
    public class MXFPackParser : KLVTripletParser<MXFPack>
    {
        private long currentPackNumber = 0;

        public MXFPackParser(Stream stream)
            : base(stream)
        {
        }

        public MXFPackParser(Stream stream, long baseOffset) : base(stream, baseOffset)
        {

        }

        public override MXFPack GetNext()
        {
            MXFPack pack = base.GetNext();
            try
            {
                pack = MXFPackFactory.CreateStronglyTypedPack(pack);
            }
            catch (TargetInvocationException ex)
            {
                // Exception raised during ctor via Activator.CreateInstance, therefore unparseable pack
                Log.ForContext<MXFPackParser>().Error($"Exception occured while parsing {pack}: {ex.InnerException}", pack);
                MXFUnparseablePack unparseablePack = new MXFUnparseablePack(pack, ex.InnerException);
                unparseablePack.Number = currentPackNumber;
                // seek to last good position, i.e. after current/last klv/pack
                SeekToEndOfCurrentKLV();
                throw new UnparseablePackException(unparseablePack, $"Exception occured while parsing {pack}", pack.Offset, ex.InnerException);
            }
            finally
            {
                pack.Number = currentPackNumber++;
                Current = pack;
            }
            return pack;
        }

        protected override UL ParseKLVKey()
        {
            // before parsing check if we have enough bytes to read
            if (RemainingBytesCount >= (int)KeyLengths.SixteenBytes)
            {
                return reader.ReadUL();
            }
            else
            {
                // return to last KLV
                SeekToEndOfCurrentKLV();
                var truncatedObject = new MXFNamedObject("Truncated Object/NON-KLV area", Current?.Offset ?? 0, RemainingBytesCount);
                throw new EndOfKLVStreamException("Premature end of file: Not enough bytes to read KLV Key(K).", Current?.Offset ?? 0 + RemainingBytesCount, truncatedObject, null);
            }
        }

        protected override KLVBERLength ParseKLVLength()
        {
            // we need at least one byte to read
            if (RemainingBytesCount >= 1)
            {
                byte[] bytes = new byte[] { reader.ReadByte() };

                switch (bytes[0])
                {
                    case <= 0x7F:
                        // short form, size = length
                        return new KLVBERLength(bytes[0], bytes);

                    case 0x80:
                        // Indefinite form 
                        return new KLVBERLength(bytes[0], bytes);

                    case > 0x80:

                        // long form: size is number of octets following, 1 + x octets
                        int additionalOctetsCount = bytes[0] - 0x80;

                        // check again if the remaining bytes from stream are at least as many as additional octets
                        if (RemainingBytesCount >= additionalOctetsCount)
                        {
                            // SMPTE 379M 5.3.4 guarantee that additional octets must not exceed 8 bytes
                            if (additionalOctetsCount > 8)
                            {
                                throw new NotSupportedException($"BER Length exceeds 8 octets (not valid according to SMPTE 379M 5.3.4). Found at offset {reader.Position}");
                            }

                            byte[] additionalOctets = reader.ReadBytes(additionalOctetsCount);
                            long lengthValue = additionalOctets.ToLong();
                            bytes = bytes.Concat(additionalOctets).ToArray();
                            return new KLVBERLength(lengthValue, bytes);
                        }
                        else
                        {
                            SeekToEndOfCurrentKLV();
                            var truncatedObject = new MXFNamedObject("Truncated Object/NON-KLV area", Current?.Offset ?? 0, RemainingBytesCount);
                            throw new EndOfKLVStreamException("Premature end of file: Not enough bytes to read KLV Length(L).", Current?.Offset ?? 0 + RemainingBytesCount, truncatedObject, null);
                        }
                }
            }
            else
            {
                SeekToEndOfCurrentKLV();
                var truncatedObject = new MXFNamedObject("Truncated Object/NON-KLV area", Current?.Offset ?? 0, RemainingBytesCount);
                throw new EndOfKLVStreamException("Premature end of file: Not enough bytes to read KLV Length.", Current?.Offset ?? 0 + RemainingBytesCount, truncatedObject, null);
            }

        }

        protected override MXFPack InstantiateKLV(KLVKey key, ILength length, long offset, Stream stream)
        {
            return new MXFPack((UL)key, (KLVBERLength)length, offset, stream);
        }
    }
}