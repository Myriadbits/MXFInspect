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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
    public class MXFQuickInfo
    {
        public readonly MXFFile mxfFile;

        public string FilePathName { get; }

        public long FileSize { get; }

        public UL OperationalPattern { get; }

        public bool FooterPresent { get; }

        public bool RIPPresent { get; }

        public string ApplicationSupplierName { get; }

        public string ApplicationName { get; }

        public DateTime? LastModified { get; }

        public MXFProductVersion ApplicationVersion { get; }

        public int? TrackCount { get; }

        public MXFLength? EditUnitsCount { get; }

        public int? PartitionsCount { get; }

        public MXFPosition? SourceStartTimeCode { get; }

        public int? TimeCodeBase { get; }

        public bool? DropFrame { get; }

        MXFTimeStamp FirstSystemItemTimeStamp { get; }

        MXFTimeStamp LastSystemItemTimeStamp { get; }

        public MXFQuickInfo(MXFFile file)
        {
            mxfFile = file;
            FilePathName = file.File.FullName;
            FileSize = file.File.Length;
            OperationalPattern = file.Descendants().OfType<MXFPreface>().FirstOrDefault()?.OperationalPattern;
            FooterPresent = file.GetFooterPartition() != null;
            RIPPresent = file.Descendants().Last() is MXFRIP;
            var id = file.Descendants().OfType<MXFIdentification>().FirstOrDefault();
            if (id != null)
            {
                ApplicationSupplierName = id.ApplicationSupplierName;
                ApplicationName = id.ApplicationSupplierName;
                ApplicationVersion = id.ApplicationVersion;
                LastModified = id.FileModificationDate;
            }
            
            PartitionsCount = file.Descendants().OfType<MXFPartition>().Count();

            var mp = file.GetFirstMaterialPackage();
            if (mp != null)
            {
                TrackCount = mp.Children.OfType<MXFTrack>().Count();
                var timecodePack = mp.LogicalDescendants().OfType<MXFTimecodeComponent>().FirstOrDefault();
                if(timecodePack != null)
                {
                    EditUnitsCount = timecodePack.Duration;
                    SourceStartTimeCode = timecodePack.StartTimecode;
                    TimeCodeBase = timecodePack.FramesPerSecond;
                    DropFrame = timecodePack.DropFrame;
                    FirstSystemItemTimeStamp = mxfFile.FirstSystemItem?.UserDate;
                    LastSystemItemTimeStamp = mxfFile.LastSystemItem?.UserDate;
                }
            }
        }

        public Dictionary<string, string> ToKeyValue()
        {
            var retval = new Dictionary<string, string>
            {
                { "File Path", FilePathName },
                { "Size", $"{FileSize:N0} ({Helper.GetBytesReadable(FileSize)})" },
                { "Operational Pattern", OperationalPattern?.SMPTEInformation?.Name ?? string.Empty },
                { "Footer Present", FooterPresent.ToString() },
                { "RIP Present", RIPPresent.ToString() },
                { "App. Supplier", ApplicationSupplierName ?? string.Empty },
                { "App. Name", ApplicationName ?? string.Empty },
                { "App. Version", ApplicationVersion?.ToString() ?? string.Empty },
                { "Last modified", LastModified?.ToString("yyyy'-'MM'-'dd'  'HH':'mm':'ss'.'fff") },
                { "Track Count", TrackCount?.ToString() ?? string.Empty },
                { "Edit Units Count", EditUnitsCount?.ToString() ?? string.Empty },
                { "Partition Count", PartitionsCount?.ToString() ?? string.Empty },
                { "Source Start Timecode", SourceStartTimeCode?.ToString() ?? string.Empty },
                { "Timecode base", TimeCodeBase?.ToString() ?? string.Empty },
                { "Drop Frame", DropFrame.ToString() ?? string.Empty },
                { "First System Item TC", FirstSystemItemTimeStamp?.ToString() ?? string.Empty },
                { "Last System Item TC", LastSystemItemTimeStamp?.ToString() ?? string.Empty }
            };
            return retval;
        }


    }
}