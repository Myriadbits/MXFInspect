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
using System.Collections.Generic;
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
        public MXFLocalSet(MXFPack pack)
            : base(pack)
        {
            if (Key.SMPTEInformation != null)
            {
                this.Key.Name ??= "LocalSet";
            }

            AttachTags(this.GetReader());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void AttachTags(IKLVStreamReader reader)
        {
            reader.Seek(this.RelativeValueOffset);
            SubStream ss = new SubStream(this.Stream, this.RelativeValueOffset, this.Length.Value);

            var localTagParser = InitializeLocalTagParser(ss, this.ValueOffset);

            if (localTagParser != null)
            {
                while (localTagParser.HasNext())
                {
                    var tag = localTagParser.GetNext();
                    this.AddChild(tag);
                }

                // Allow derived classes to do some final work
                PostInitialize();
            }
            else
            {
                // TODO maybe raise an exception
                Log.ForContext<MXFLocalSet>().Warning($"Local tags of {this} cannot be parsed, since Local Set @{this.Offset} is of type {this.Key.RegistryDesignator}");
            }

        }

        public void ReadLocalTagValues()
        {
            var localTags = this.Children.OfType<MXFLocalTag>();

            // ToList() materialization absolutely needed as the inner method call
            // could potentially modify the iterating list by calling "AddChild"
            foreach (var lt in localTags.ToList())
            {
                try
                {
                    IKLVStreamReader reader = lt.GetReader();
                    reader.Seek(lt.RelativeValueOffset);
                    ReadLocalTagValue(reader, lt);

                }
                catch (Exception e)
                {
                    var ex = new LocalTagParsingException(lt, e);
                    Log.ForContext<MXFLocalSet>().Error(ex, $"Exception occured during reading of local tag {lt} @{lt.Offset} of MXFPack {lt.Parent}:");
                    if (this.Root() is MXFFile file)
                    {
                        file.ParsingExceptions.Add(ex);
                    }
                }
            }
        }

        public void LookUpLocalTagKeys()
        {
            var primerEntries = GetPrimerEntries();

            if (primerEntries != null)
            {
                var localTags = this.Children.OfType<MXFLocalTag>();
                foreach (var tag in localTags)
                {
                    if (primerEntries.TryGetValue(tag.TagValue, out MXFPrimerEntry primerEntry))
                    {
                        tag.AliasUID = primerEntry.AliasUID;
                    }
                }
            }
        }

        private IReadOnlyDictionary<UInt16, MXFPrimerEntry> GetPrimerEntries()
        {
            var parentPartition = this.Ancestors().OfType<MXFPartition>().FirstOrDefault();
            var primerPack = parentPartition?.Children.OfType<MXFPrimerPack>().FirstOrDefault();
            return primerPack?.PrimerEntries;
        }

        /// <summary>
        /// Allow derived classes to process the local tag
        /// </summary>
        /// <param name="localTag"></param>
        protected virtual bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
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
            return string.Format($"{this.Key} [len {this.Length.Value}]");
        }

        private MXFLocalTagParser InitializeLocalTagParser(SubStream ss, long valueOffset)
        {
            switch (this.Key.RegistryDesignator)
            {
                case ULRegistries.LocalSet_2Bytes_2Bytes:
                    return new MXFLocalTagParser(ss, valueOffset, KLVKey.KeyLengths.TwoBytes, KLVLength.LengthEncodings.TwoBytes);

                case ULRegistries.LocalSet_2Bytes_1Byte:
                    return new MXFLocalTagParser(ss, valueOffset, KLVKey.KeyLengths.OneByte, KLVLength.LengthEncodings.TwoBytes);

                case ULRegistries.LocalSet_4Bytes_1Byte:
                    return new MXFLocalTagParser(ss, valueOffset, KLVKey.KeyLengths.OneByte, KLVLength.LengthEncodings.FourBytes);

                default:
                    return null;
            }
        }
    }
}
