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
		protected FileStream	m_FileStream = null;

		/// <summary>
		/// Reader constructor
		/// </summary>
		public MXFReader()
		{
		}

		/// <summary>
		/// Constructor, create the file reader
		/// </summary>
		/// <param name="reader"></param>
		public MXFReader(string fileName)
		{
			Open(fileName);
		}

		/// <summary>
		/// Initialize, create the file reader
		/// </summary>
		/// <param name="reader"></param>
		public void Open(string fileName)
		{
			this.FileName = fileName;
			this.m_FileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			this.m_FileStream.Seek(0, SeekOrigin.Begin);
		}


		/// <summary>
		/// Initialize, create the file reader
		/// </summary>
		/// <param name="reader"></param>
		public void Close()
		{
			this.FileName = "";
			if (this.m_FileStream != null)
				this.m_FileStream.Close();
		}

		/// <summary>
		/// Return the current file position
		/// </summary>
		public long Position 
		{ 
			get
			{
				return this.m_FileStream.Position;
			}
		}

		/// <summary>
		/// Seek to a position in the file
		/// </summary>
		/// <param name="newPosition"></param>
		public void Seek(long newPosition)
		{
			this.m_FileStream.Seek(newPosition, System.IO.SeekOrigin.Begin);
		}

		/// <summary>
		/// Skip some bytes
		/// </summary>
		public void Skip(long toSkip)
		{
			Seek(this.Position + toSkip);
		}


		/// <summary>
		/// Return the name of this MXF
		/// </summary>
		public string FileName { get; set; }


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
		/// Returns true when the end-of-file is reached
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

		/// <summary>
		/// Clean-up
		/// </summary>
		void IDisposable.Dispose()
		{
			Close();
		}
	
		/// <summary>
		/// Read a single byte
		/// </summary>
		public byte ReadB()
		{
			if (this.m_FileStream != null)
				return (byte)this.m_FileStream.ReadByte();
			return 0;
		}

		/// <summary>
		/// Read a single byte
		/// </summary>
		public bool ReadBool()
		{
			return (this.ReadB() != 0);
		}

		/// <summary>
		/// Read multiple bytes
		/// </summary>
		public void Read(byte[] array, long count)
		{
			if (this.m_FileStream != null)
				this.m_FileStream.Read(array, 0, (int) count);
		}

		/// <summary>
		/// Read a single byte
		/// </summary>
		public sbyte ReadsB()
		{
			if (this.m_FileStream != null)
				return (sbyte)this.m_FileStream.ReadByte();
			return 0;
		}

		/// <summary>
		/// Read a single word
		/// </summary>
		public UInt16 ReadW()
		{
			if (this.m_FileStream != null)
				return (UInt16)((this.m_FileStream.ReadByte() << 8) + this.m_FileStream.ReadByte());
			return 0;
		}
		
		/// <summary>
		/// Read a dword
		/// </summary>
		public UInt32 ReadD()
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
		/// Read a long
		/// </summary>
		public UInt64 ReadL()
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
		/// Read a string
		/// </summary>
		public string ReadS(int length)
		{
			byte[] data = new byte[length];
			for (int n = 0; n < length; n++)
				data[n] = this.ReadB();
			return System.Text.Encoding.BigEndianUnicode.GetString(data);
		}

		/// <summary>
		/// Read a reference key
		/// </summary>
		public MXFRefKey ReadRefKey()
		{
			return new MXFRefKey(this);
		}

		/// <summary>
		/// Read a normal (non-reference) key
		/// </summary>
		public MXFKey ReadKey()
		{
			return new MXFKey(this, 16); // Always read 16 bytes for keys (is not completely according to spec, length is part of the key...)
		}

		/// <summary>
		/// Read a UMID key (with reference)
		/// </summary>
		public MXFUMIDKey ReadUMIDKey()
		{
			return new MXFUMIDKey(this); // Always read 32 bytes for UMID's 
		}

		/// <summary>
		/// Read a version
		/// </summary>
		/// <returns></returns>
		public UInt16[] ReadVersion()
		{
			UInt16[] version = new UInt16[5];
			for (int n = 0; n < 5; n++)
				version[n] = ReadW();
			return version;
		}

		/// <summary>
		/// Read a timestamp
		/// </summary>
		/// <param name="rateNonDrop"></param>
		/// <returns></returns>
		public DateTime ReadTimestamp()
		{
			UInt16 year = this.ReadW();
			byte month = this.ReadB();
			byte day = this.ReadB();
			byte hour = this.ReadB();
			byte minute = this.ReadB();
			byte second = this.ReadB();
			byte millisecond = this.ReadB();
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
		/// Read a timestamp
		/// </summary>
		/// <param name="rateNonDrop"></param>
		/// <returns></returns>
		public MXFRational ReadRational()
		{
			MXFRational rat = new MXFRational();
			rat.Num = this.ReadD();
			rat.Den = this.ReadD();
			return rat;
		}

		/// <summary>
		/// Read a list of keys
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="categoryName"></param>
		/// <returns></returns>
		public MXFObject ReadKeyList(string categoryName, string singleItem)
		{
			UInt32 nofItems = this.ReadD();
			UInt32 objectSize = this.ReadD(); // useless size of objects, always 16 according to specs

			MXFObject keylist = new MXFNamedObject(categoryName, this.Position, objectSize);
			if (nofItems < UInt32.MaxValue)
			{
				for (int n = 0; n < nofItems; n++)
				{
					MXFRefKey key = new MXFRefKey(this, objectSize, singleItem);
					keylist.AddChild(key);
				}
			}
			return keylist;
		}


		/// <summary>
		/// Read a BCD timecode
		/// </summary>
		/// <param name="rateNonDrop"></param>
		/// <returns></returns>
		public MXFTimeStamp ReadBCDTimeCode(double frameRate)
		{
			// TODO If MJD is set, time is milliseconds since X
			byte type = this.ReadB();

			MXFTimeStamp timeStamp = new MXFTimeStamp();
			timeStamp.ParseBCDTimeCode(this, frameRate);

			this.ReadB(); // BG7 + BG8

			// Read 8 dummy bytes (always zero)
			this.ReadL();

			return timeStamp;
		}


		
	}
}
