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

using Myriadbits.MXF.KLV;
using Myriadbits.MXF.Utils;
using System.ComponentModel;
using System.Dynamic;
using System.IO;

namespace Myriadbits.MXF
{
    public class KLVTriplet : MXFObject
    {
        private const string CATEGORYNAME = "KLV";
        private const int CATEGORYPOS = 1;

        // Substream from klv offset (thus including key and length enc) to klv end (=its total length)
        protected Stream Stream { get; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Key part of KLV triplet")]
        public virtual KLVKey Key { get; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Length part of KLV triplet")]
        public virtual ILength Length { get; }

        [Browsable(false)]
        [Description("Value part of KLV triplet")]
        public virtual byte[] Value { get; }

        /// <summary>
        /// Offset of the value (=data), i.e. where the payload begins.
        /// </summary>
        [Browsable(false)]
        public long ValueOffset { get; }

        /// <summary>
        /// Offset of the value (=data), i.e. where the payload begins from the beginning of the triplet,
        /// i. e. length of the key +  length of lengthencoding 
        /// </summary>
        [Browsable(false)]
        public long RelativeValueOffset { get; }


        public KLVTriplet(KLVKey key, ILength length, long offset, Stream stream) : base(offset)
        {
            Key = key;
            Length = length;
            RelativeValueOffset = key.ArrayLength + length.ArrayLength;
            ValueOffset = offset + RelativeValueOffset;
            if (length.Value == -1)
            {
                // indefinite BER form
                TotalLength = (int)key.KeyLength + length.ArrayLength;
            }
            else
            {
                TotalLength = (int)key.KeyLength + length.ArrayLength + length.Value;
            }

            Stream = stream;
        }

        public IKLVStreamReader GetReader()
        {
            return new KLVStreamReader(this.Stream);
        }

        //// TODO this should not be the responsibility of the class to read its content
        //public V GetValue(SubStream ss)
        //{
        //    byte[] buffer = new byte[byte]
        //    ss.Seek(Offset, SeekOrigin.Begin);
        //    ss.Read()

        //        //return new KLVValue(reader.ReadArray(reader.ReadByte, Length.Value));
        //}

        private MXFFile GetFile()
        {
            return this.Root() as MXFFile;
        }

        public SubStream GetValueStream()
        {
            MXFFile mxfFile = GetFile();
            FileStream fs = new FileStream(mxfFile.File.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 10240);
            return new SubStream(fs, ValueOffset, Length.Value); 
        }
    }

}
