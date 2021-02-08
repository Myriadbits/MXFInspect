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

using System.ComponentModel;

namespace Myriadbits.MXF
{
    public class MXFCryptographicContext : MXFDescriptiveObject
    {
        private const string CATEGORYNAME = "CryptographicContext";

        public readonly MXFKey cryptoContextId_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x01, 0x01, 0x15, 0x11, 0x00, 0x00, 0x00, 0x00);
        public readonly MXFKey cipherAlgorithm_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x02, 0x09, 0x03, 0x01, 0x01, 0x00, 0x00, 0x00);
        public readonly MXFKey cryptoKeyID_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x02, 0x09, 0x03, 0x01, 0x02, 0x00, 0x00, 0x00);
        public readonly MXFKey mICAlgorithm_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x02, 0x09, 0x03, 0x02, 0x01, 0x00, 0x00, 0x00);
        public readonly MXFKey sourceContainerFormat_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x02, 0x02, 0x00, 0x00, 0x00);
        public readonly MXFKey mICCarriage_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x01, 0x0d, 0x0e, 0x01, 0x01, 0x07, 0x04, 0x03, 0x00);

        [Category(CATEGORYNAME)]
        public MXFKey CryptographicContextID { get; set; }

        [Category(CATEGORYNAME)]
        public MXFKey CipherAlgorithm { get; set; }

        [Category(CATEGORYNAME)]
        public MXFKey CryptographicKeyID { get; set; }

        [Category(CATEGORYNAME)]
        public MXFKey MICAlgorithm { get; set; }

        [Category(CATEGORYNAME)]
        public MXFKey SourceContainerFormat { get; set; }

        [Category(CATEGORYNAME)]
        public MXFKey MICCarriage { get; set; }



        public MXFCryptographicContext(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "CryptographicContext";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            if (localTag.Key != null)
            {
                switch (localTag.Key)
                {
                    case var _ when localTag.Key == cryptoContextId_Key: this.CryptographicContextID = reader.ReadULKey(); return true;
                    case var _ when localTag.Key == cipherAlgorithm_Key: this.CipherAlgorithm = reader.ReadULKey(); return true;
                    case var _ when localTag.Key == cryptoKeyID_Key: this.CryptographicKeyID = reader.ReadULKey(); return true;
                    case var _ when localTag.Key == mICAlgorithm_Key: this.MICAlgorithm = reader.ReadULKey(); return true;
                    case var _ when localTag.Key == sourceContainerFormat_Key: this.SourceContainerFormat = reader.ReadULKey(); return true;
                    case var _ when localTag.Key == mICCarriage_Key: this.MICCarriage = reader.ReadULKey(); return true;

                }
            }

            return base.ParseLocalTag(reader, localTag);
        }

    }
}
