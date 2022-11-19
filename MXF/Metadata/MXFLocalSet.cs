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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using static Myriadbits.MXF.KLV.KLVLength;
using static Myriadbits.MXF.KLVKey;

namespace Myriadbits.MXF
{
    /// <summary>
    /// The most popular grouping in MXF is the local set where each data item has, as an identifier, a
    /// short local tag defined within the context of the local set. The local tag is typically only 1 
    /// or 2 bytes long and is used as an alias for the full 16-byte universal label value.
    /// </summary>
    public class MXFLocalSet : MXFPack
    {
        //public override List<MXFLokalTag> Value { get;  }

        public MXFLocalSet(IMXFReader reader, MXFPack pack)
            : base(pack)
        {
            if (Key.SMPTEInformation != null)
            {
                this.Key.Name ??= "LocalSet";
            }

            ParseTags(reader);
        }

        //public List<MXFLokalTag> GetSubKLV()
        //{
        //    var subKLVList = new List<MXFLokalTag>();
        //    var offset = this.ValueOffset;
        //    long summedLength = 0;
        //    while (summedLength < this.Length.Value)
        //    {
        //        var subKLV = ParseKLV(subKeyLength, subLengthEncoding, offset);

        //        if (subKLV.Offset + subKLV.TotalLength > this.Offset + this.TotalLength)
        //        {
        //            // TODO should be of type KLVParser exception
        //            throw new System.Exception("SubKLV out range");
        //        }
        //        else
        //        {
        //            subKLVList.Add(subKLV);
        //            offset += subKLV.TotalLength;
        //            summedLength += subKLV.TotalLength;
        //        }
        //    }
        //    return subKLVList;
        //}

        //private KLVTriplet ParseKLV(KeyLengths keyLength, LengthEncodings encoding, long offset)
        //{
        //    // move to file pos
        //    reader.Seek(offset);

        //    var key = ParseKLVKey(keyLength);
        //    var length = ParseKLVLength(encoding);
        //    var value = reader.ReadArray(reader.ReadByte, length.Value);
        //    return new KLVTriplet(key, length, offset, value);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        private void ParseTags(IMXFReader reader)
        {
            // Make sure we read at the data position
            reader.Seek(this.ValueOffset);

            // Read all local tags
            long klvEnd = this.ValueOffset + this.Length.Value;
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

                }

                // Add to the collection
                AddChild(tag);

                reader.Seek(next);
            }

            // Allow derived classes to do some final work
            PostInitialize();
        }


        public void ParseTagsAgain(IMXFReader reader)
        {
            var tags = this.Children.OfType<MXFLocalTag>();
            foreach (var tag in tags)
            {
                AddRefKeyFromPrimerPack(tag);
                tag.Parse(reader);
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
            var parentPartition = this.Ancestors().OfType<MXFPartition>().FirstOrDefault();

            if (parentPartition != null && parentPartition.PrimerKeys != null)
            {
                if (parentPartition.PrimerKeys.TryGetValue(tag.Tag, out MXFEntryPrimer primerEntry))
                {
                    //MXFEntryPrimer entry = this.Partition.PrimerKeys[tag.Tag];
                    tag.Key = primerEntry.AliasUID;
                }
            }
            //if (this.Partition?.PrimerKeys != null)
            //{
            //    if (this.Partition.PrimerKeys.TryGetValue(tag.Tag, out MXFEntryPrimer primerEntry))
            //    {
            //        MXFEntryPrimer entry = this.Partition.PrimerKeys[tag.Tag];
            //        tag.Key = primerEntry.AliasUID;
            //    }
            //}
        }

        /// <summary>
        /// Allow derived classes to process the local tag
        /// </summary>
        /// <param name="localTag"></param>
        protected virtual bool ParseLocalTag(IMXFReader reader, MXFLocalTag localTag)
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

        public override string ToString()
        {
            return string.Format("{0} [len {1}]", this.Key, this.Length);
        }
    }
}
