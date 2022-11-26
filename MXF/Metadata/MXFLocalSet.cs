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
using System.IO;
using System.Linq;

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

        public MXFLocalSet(IKLVStreamReader reader, MXFPack pack)
            : base(pack)
        {
            if (Key.SMPTEInformation != null)
            {
                this.Key.Name ??= "LocalSet";
            }

            ParseTags(reader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        private void ParseTags(IKLVStreamReader reader)
        {
            // Make sure we read at the data position
            reader.Seek(this.RelativeValueOffset);

            // Read all local tags
            while(!reader.EOF)
            //while (reader.Position + 4 < klvEnd)
            {
                MXFLocalTag tag = new MXFLocalTag(reader);

                // TODO hm???
                if (tag.Size == 0)
                    break;
                
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

            reader.Seek(this.RelativeValueOffset);
            var ms = new MemoryStream(reader.ReadBytes((int)this.TotalLength));
            var localTagParser = new MXFLocalTagParser(ms);

            while (localTagParser.HasNext())
            {
                var tag = localTagParser.GetNext();
                this.AddChild(tag);
            }

            PostInitialize();
        }

        public void ParseTagsAgain(IKLVStreamReader reader)
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
            var primerEntries = GetPrimerEntries();

            if (primerEntries != null)
            {
                if (primerEntries.TryGetValue(tag.Tag, out MXFEntryPrimer primerEntry))
                {
                    tag.Key = primerEntry.AliasUID;
                }
            }
        }

        public void LookUpLocalTagKeys()
        {
            var primerEntries = GetPrimerEntries();

            if(primerEntries != null)
            {
                var localTags = this.Children.OfType<MXFLokalTag>();
                foreach (var tag in localTags)
                {
                    if (primerEntries.TryGetValue(tag.TagValue, out MXFEntryPrimer primerEntry))
                    {
                        //MXFEntryPrimer entry = this.Partition.PrimerKeys[tag.Tag];
                        tag.AliasUID = primerEntry.AliasUID;
                    }
                }
            }
        }

        private IReadOnlyDictionary<UInt16, MXFEntryPrimer> GetPrimerEntries()
        {
            var parentPartition = this.Ancestors().OfType<MXFPartition>().FirstOrDefault();
            var primerPack = parentPartition?.Children.OfType<MXFPrimerPack>().FirstOrDefault();
            //parentPartition = null;
            return primerPack?.PrimerEntries;
        }

        /// <summary>
        /// Allow derived classes to process the local tag
        /// </summary>
        /// <param name="localTag"></param>
        protected virtual bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
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
