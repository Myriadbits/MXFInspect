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
using System.ComponentModel;

namespace Myriadbits.MXF
{
    /// <summary>
    /// Meta data class (might) consists of multiple local tags
    /// </summary>
    public class MXFMetadataBaseclass : MXFKLV
    {
        private const string CATEGORYNAME = "Metadata";

        [Category(CATEGORYNAME)]
        [Description("Type of metadata node")]
        [Browsable(false)]
        public string MetaDataName { get; set; }

        public MXFMetadataBaseclass(MXFReader reader, MXFKLV headerKLV)
            : base(headerKLV, "MetaData", KeyType.MetaData)
        {
            this.m_eType = MXFObjectType.Meta;
            this.MetaDataName = "<unknown>";
            Initialize(reader);
        }

        public MXFMetadataBaseclass(MXFReader reader, MXFKLV headerKLV, string metaDataName)
            : base(headerKLV, "MetaData", KeyType.MetaData)
        {
            this.m_eType = MXFObjectType.Meta;
            this.MetaDataName = metaDataName;
            this.Key.Name = metaDataName; // TODO Correct??
            Initialize(reader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        private void Initialize(MXFReader reader)
        {
            // Make sure we read at the data position
            reader.Seek(this.DataOffset);

            // Read all local tags
            long klvEnd = this.DataOffset + this.Length;
            while (reader.Position + 4 < klvEnd)
            {
                MXFLocalTag tag = new MXFLocalTag(reader);
                long next = tag.DataOffset + tag.Size;
                AddRefKeyFromPrimerPack(tag);

                // Allow derived classes to handle the data
                if (!ParseLocalTag(reader, tag))
                {
                    // Not processed, use default
                    tag.Parse(reader);

                    // Add to the collection
                    AddChild(tag);
                }

                reader.Seek(next);
            }

            // Allow derived classes to do some final work
            PostInitialize();
        }

        /// <summary>
        ///	Tries to find the local tag in the primer pack and if so,
        ///	adds the referring key to the tag.
        /// </summary>
        /// <param name="tag"></param>
        private void AddRefKeyFromPrimerPack(MXFLocalTag tag)
        {
            if (this.Partition != null && this.Partition.PrimerKeys != null)
            {
                if (this.Partition.PrimerKeys.ContainsKey(tag.Tag))
                {
                    MXFEntryPrimer entry = this.Partition.PrimerKeys[tag.Tag];
                    tag.Key = entry.AliasUID.Key;
                }
            }
        }

        /// <summary>
        /// Allow derived classes to process the local tag
        /// </summary>
        /// <param name="localTag"></param>
        protected virtual bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            return false;
        }


		/// <summary>
		/// Called after all local tags have been processed
		/// </summary>
		/// <param name="localTag"></param>
		protected virtual void PostInitialize()
		{
		}

        /// <summary>
        /// Display some output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.MetaDataName))
                return string.Format("MetaData: {0} [len {1}]", this.MetaDataName, this.Length);
            else
                return string.Format("{0} [len {1}]", this.MetaDataName, this.Length);
        }
    }
}
