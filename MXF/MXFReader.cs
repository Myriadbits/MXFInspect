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
using System.IO;

namespace Myriadbits.MXF
{
    /// <summary>
    /// Read data from the MXF file, low-level read class 
    /// (Candidate for performance increase)
    /// </summary>
    public class MXFReader : IDisposable
    {
        protected FileStream m_FileStream = null;

        #region Properties

        /// <summary>
        /// Returns the file name of this MXF file
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// Returns the current file position
        /// </summary>
        public long Position
        {
            get
            {
                return this.m_FileStream.Position;
            }
        }


        /// <summary>
        /// Returns true when the end-of-file is reached
        /// </summary>
        public bool EOF
        {
            get
            {
                if (this.m_FileStream == null)
                    return true;
                return this.m_FileStream.Position >= this.m_FileStream.Length;
            }
        }


        /// <summary>
        /// Gets the size of the file
        /// </summary>
        public long Size
        {
            get
            {
                if (this.m_FileStream == null)
                    return 0;
                return this.m_FileStream.Length;
            }
        }

        #endregion

        /// <summary>
        /// Constructor, creates the file reader
        /// </summary>
        /// <param name="reader"></param>
        public MXFReader(string fileName)
        {
            Open(fileName);
        }

        /// <summary>
        /// Initializes, creates the file reader
        /// </summary>
        /// <param name="reader"></param>
        public void Open(string fileName)
        {
            this.FileName = fileName;
            this.m_FileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            this.m_FileStream.Seek(0, SeekOrigin.Begin);
        }


        /// <summary>
        /// Closes the file reader
        /// </summary>
        /// <param name="reader"></param>
        public void Close()
        {
            this.FileName = "";
            if (this.m_FileStream != null)
                this.m_FileStream.Close();
        }


        /// <summary>
        /// Seeks to a position in the file
        /// </summary>
        /// <param name="newPosition">The position to head for</param>
        public void Seek(long newPosition)
        {
            this.m_FileStream.Seek(newPosition, SeekOrigin.Begin);
        }

        /// <summary>
        /// Skips a given amount of bytes
        /// </summary>
        /// <param name="toSkip">The amount to skip</param>
        public void Skip(long toSkip)
        {
            Seek(this.Position + toSkip);
        }

        public bool SeekForNextPotentialKey()
        {
            byte[] validULPrefix = { 0x06, 0x0e, 0x2b, 0x34 };
            int foundBytes = 0;

            while (!this.EOF)
            {
                if (this.ReadByte() == validULPrefix[foundBytes])
                {
                    foundBytes++;

                    if (foundBytes == 4)
                    {
                        this.Seek(this.Position - 4);
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

        /// <summary>
        /// Clean-up
        /// </summary>
        void IDisposable.Dispose()
        {
            Close();
        }

        #region Basic types

        /// <summary>
        /// Reads a single byte
        /// </summary>
        public byte ReadByte()
        {
            if (this.m_FileStream != null)
                return (byte)this.m_FileStream.ReadByte();
            return 0;
        }

        /// <summary>
        /// Reads a signed byte
        /// </summary>
        public sbyte ReadSignedByte()
        {
            if (this.m_FileStream != null)
                return (sbyte)this.m_FileStream.ReadByte();
            return 0;
        }

        /// <summary>
        /// Reads a single byte holding a boolean value
        /// </summary>
        public bool ReadBool()
        {
            return (this.ReadByte() != 0);
        }

        /// <summary>
        /// Reads a single word
        /// </summary>
        public UInt16 ReadUInt16()
        {
            if (this.m_FileStream != null)
                return (UInt16)((this.m_FileStream.ReadByte() << 8) + this.m_FileStream.ReadByte());
            return 0;
        }

        /// <summary>
        /// Reads a dword
        /// </summary>
        public UInt32 ReadUInt32()
        {
            if (this.m_FileStream != null)
                return (UInt32)(
                        ((UInt32)this.m_FileStream.ReadByte() << 24) +
                        ((UInt32)this.m_FileStream.ReadByte() << 16) +
                        ((UInt32)this.m_FileStream.ReadByte() << 8) +
                        ((UInt32)this.m_FileStream.ReadByte())
                        );
            return 0;
        }


        /// <summary>
        /// Reads a signed dword
        /// </summary>
        public Int32 ReadInt32()
        {
            // TODO pay attention, this method works only for positive numbers!!!
            return (Int32)this.ReadUInt32();
        }


        /// <summary>
        /// Reads a long
        /// </summary>
        public UInt64 ReadUInt64()
        {
            if (this.m_FileStream != null)
                return (UInt64)(
                        ((UInt64)this.m_FileStream.ReadByte() << 56) +
                        ((UInt64)this.m_FileStream.ReadByte() << 48) +
                        ((UInt64)this.m_FileStream.ReadByte() << 40) +
                        ((UInt64)this.m_FileStream.ReadByte() << 32) +
                        ((UInt64)this.m_FileStream.ReadByte() << 24) +
                        ((UInt64)this.m_FileStream.ReadByte() << 16) +
                        ((UInt64)this.m_FileStream.ReadByte() << 8) +
                        ((UInt64)this.m_FileStream.ReadByte())
                        );
            return 0;
        }

        /// <summary>
        /// Reads a string in UTF8 coding
        /// </summary>
        /// <param name="length">The length of the string to read</param>
        public string ReadUTF8String(int length)
        {
            byte[] data = new byte[length];
            for (int n = 0; n < length; n++)
                data[n] = this.ReadByte();
            return System.Text.Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// Reads a string
        /// </summary>
        /// <param name="length">The length of the string to read</param>
        public string ReadUTF16String(int length)
        {
            byte[] data = new byte[length];
            for (int n = 0; n < length; n++)
                data[n] = this.ReadByte();
            return System.Text.Encoding.BigEndianUnicode.GetString(data);
        }


        #endregion

        #region Identifiers

        public MXFKey ReadULKey()
        {
            // Always read 16 bytes for UL keys (is not completely according to spec, length is part of the key...)
            byte[] byteArr = this.ReadArray(this.ReadByte, 16);
            return new MXFKey(byteArr);
        }

        /// <summary>
        /// Reads a UMID key
        /// </summary>
        public MXFUMID ReadUMIDKey()
        {
            // Always read 32 bytes for UMID's 
            byte[] byteArr = this.ReadArray(this.ReadByte, 32);
            return new MXFUMID(byteArr);
        }

        /// <summary>
        /// Reads a UUID key
        /// </summary>
        public MXFUUID ReadUUIDKey()
        {
            // Always read 16 bytes for UUIDs
            byte[] byteArr = this.ReadArray(this.ReadByte, 16);
            return new MXFUUID(byteArr);
        }

        /// <summary>
        /// Reads a list of AUIDs and returns a MXFObject containing the AUIDs as children
        /// </summary>
        /// <param name="groupName">The name of the MXFObject acting as group container</param>
        /// <param name="singleItem">The name of the single items</param>
        public MXFObject ReadAUIDSet(string groupName, string singleItem)
        {
            UInt32 nofItems = this.ReadUInt32();
            UInt32 objectSize = this.ReadUInt32(); // TODO useless size of objects, always 16 according to specs

            MXFObject auidGroup = new MXFNamedObject(groupName, this.Position, objectSize);
            if (nofItems < UInt32.MaxValue)
            {
                for (int n = 0; n < nofItems; n++)
                {
                    MXFAUID auid = new MXFAUID(this, singleItem);
                    auidGroup.AddChild(auid);
                }
            }
            return auidGroup;
        }

        #endregion

        #region Reference types

        /// <summary>
        /// Reads a MXF long version in a partition
        /// </summary>
        public MXFProductVersion ReadProductVersion()
        {
            UInt16[] version = this.ReadArray(this.ReadUInt16, 4);
            MXFProductReleaseType build = (MXFProductReleaseType)this.ReadUInt16();
            return new MXFProductVersion
            {
                Major = version[0],
                Minor = version[1],
                Tertiary = version[2],
                Patch = version[3],
                Build = build,
            };
        }

        /// <summary>
        /// Reads a MXF version
        /// </summary>
        public MXFVersion ReadVersion()
        {
            var major = this.ReadByte();
            var minor = this.ReadByte();
            return new MXFVersion(major, minor);
        }

        /// <summary>
        /// Reads a timestamp
        /// </summary>
        public DateTime ReadTimestamp()
        {
            UInt16 year = this.ReadUInt16();
            byte month = this.ReadByte();
            byte day = this.ReadByte();
            byte hour = this.ReadByte();
            byte minute = this.ReadByte();
            byte second = this.ReadByte();
            byte millisecond = this.ReadByte();
            try
            {
                return new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// Reads a rational
        /// </summary>
        public MXFRational ReadRational()
        {
            MXFRational rat = new MXFRational();
            rat.Num = this.ReadUInt32();
            rat.Den = this.ReadUInt32();
            return rat;
        }

        /// <summary>
        /// Reads a BCD timecode
        /// </summary>
        /// <param name="frameRate"></param>
        /// <returns></returns>
        public MXFTimeStamp ReadBCDTimeCode(double frameRate)
        {
            // TODO If MJD is set, time is milliseconds since X
            byte type = this.ReadByte();

            MXFTimeStamp timeStamp = new MXFTimeStamp();
            timeStamp.ParseBCDTimeCode(this, frameRate);

            this.ReadByte(); // BG7 + BG8

            // Read 8 dummy bytes (always zero)
            this.ReadUInt64();

            return timeStamp;
        }


        /// <summary>
        /// Reads a color primary
        /// </summary>
        public MXFColorPrimary ReadColorPrimary()
        {
            MXFColorPrimary colorPrimary = new MXFColorPrimary();
            colorPrimary.XColorCoordinate = this.ReadUInt16();
            colorPrimary.YColorCoordinate = this.ReadUInt16();
            return colorPrimary;
        }

        /// <summary>
        /// Reads a RGBA component
        /// </summary>
        public MXFRGBAComponent ReadRGBAComponent()
        {
            var code = (MXFRGBAComponentKind)this.ReadByte();
            var componentSize = this.ReadByte();
            return new MXFRGBAComponent
            {
                Code = code,
                ComponentSize = componentSize
            };
        }

        /// <summary>
        /// Reads a RGBA layout
        /// </summary>
        public MXFRGBAComponent[] ReadRGBALayout()
        {
            return this.ReadArray(this.ReadRGBAComponent, 8);
        }

        /// <summary>
        /// Reads a channel status mode
        /// </summary>
        public MXFChannelStatusMode ReadChannelstatusMode()
        {
            return (MXFChannelStatusMode)this.ReadByte();
        }

        public MXFUserDataMode ReadUserDataMode()
        {
            return (MXFUserDataMode)this.ReadByte();
        }

        public T[] ReadArray<T>(Func<T> readFunction, int count)
        {
            T[] retval = new T[count];
            for (int i = 0; i < count; i++)
            {
                retval[i] = readFunction();
            }
            return retval;
        }

        public MXFObject ReadReferenceSet<T>(string referringSetName, string singleItemName) where T : MXFObject
        {
            UInt32 nofItems = this.ReadUInt32();
            UInt32 objectSize = this.ReadUInt32(); // useless size of objects, always 16 according to specs

            MXFObject referenceSet = new MXFNamedObject(referringSetName, this.Position, objectSize);

            // TODO what if this condition is not met? should we throw an exception?
            if (nofItems < UInt32.MaxValue)
            {
                for (int n = 0; n < nofItems; n++)
                {
                    var reference = new MXFReference<T>(this, singleItemName);
                    referenceSet.AddChild(reference);
                }
            }

            return referenceSet;
        }

        /// <summary>
        /// Reads a strong reference
        /// </summary>
        /// <param name="reader"></param>
        public MXFReference<T> ReadReference<T>(string referringItemName) where T : MXFObject
        {
            return new MXFReference<T>(this, referringItemName);
        }
        #endregion
    }
}
