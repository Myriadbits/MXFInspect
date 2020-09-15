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
	public class MXFKLV : MXFObject
	{
		private static MXFKey s_MxfKlvKey = new MXFKey(0x06, 0x0e, 0x2b, 0x34);

		[CategoryAttribute("KLV"), ReadOnly(true)]
		public MXFKey Key { get; set; }

		[CategoryAttribute("KLV"), ReadOnly(true)]
		public long DataOffset { get; set; } // Points just after the KLV
		
		[Browsable(false)]
		public MXFPartition Partition { get; set; }

		/// <summary>
		/// Create the KLV key
		/// </summary>
		/// <param name="reader"></param>
		public MXFKLV(MXFReader reader)
			: base(reader)
		{
			this.Key = CreateAndValidateKey(reader);
			this.Length = DecodeBerLength(reader);
			this.DataOffset = reader.Position;
		}

		/// <summary>
		/// Create the KLV key
		/// </summary>
		/// <param name="reader"></param>
		public MXFKLV(MXFReader reader, MXFKey key)
			: base(reader)
		{
			this.Key = CreateAndValidateKey(reader);
			this.Length = DecodeBerLength(reader);
			this.DataOffset = reader.Position;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFKLV(MXFKLV klv)
		{
			this.Offset = klv.Offset;
			this.Key = klv.Key;
			this.Length = klv.Length;
			this.DataOffset = klv.DataOffset;
			this.Partition = klv.Partition;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFKLV(MXFKLV klv, string name, KeyType type)
		{
			this.Offset = klv.Offset;
			this.Key = klv.Key;
			this.Key.Name = name;
			this.Key.Type = type;
			this.Length = klv.Length;
			this.DataOffset = klv.DataOffset;
			this.Partition = klv.Partition;
		}

		
		/// <summary>
		/// Validate if the current position is a valid SMPTE key
		/// </summary>
		private MXFKey CreateAndValidateKey(MXFReader reader)
		{
			byte iso = reader.ReadB();
			byte len = reader.ReadB();
			byte smp = 0, te = 0;
			bool valid = false;
			if (iso == 0x06 ) // Do not check length when not iso
			{
				smp = reader.ReadB();
				te = reader.ReadB();
				valid = (smp == 0x2B && te == 0x34); // SMPTE define
			}
			if (!valid)
			{
				//throw new ApplicationException(string.Format("Invalid SMPTE Key found at offset {0}! Incorrect MXF file!", reader.Position - 4));
				MXFKey key = new MXFKey(iso, len, smp, te, reader);
				LogError("Invalid SMPTE Key found at offset {0}! Key: {1}", reader.Position - 4, key.Name);
				return key;
			}
			else
			{
				return new MXFKey(iso, len, smp, te, reader);
			}
		}

		/// <summary>
		/// Decode the length
		/// </summary>
		/// <param name="reader"></param>
		private long DecodeBerLength(MXFReader reader)
		{
			long size = reader.ReadB();
			if ((size & 0x80) != 0)
			{ 
				// long form
				int bytes_num = (int)(size & 0x7F);
				// SMPTE 379M 5.3.4 guarantee that bytes_num must not exceed 8 bytes
				if (bytes_num > 8)
				{
					//throw new ArgumentException("KLV length more then 8 bytes!");
					LogWarning("KLV length more then 8 bytes (not valid according to SMPTE 379M 5.3.4) found at offset {0}!", reader.Position);
				}
				size = 0;
				while ( (bytes_num--) != 0)
					size = size << 8 | reader.ReadB();
			}
			return size;
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (this.Children != null && this.Children.Count > 0)
				return string.Format("{0} [len {1}]", this.Key.Name, this.Children.Count);
			return string.Format("{0} [len {1}]", this.Key.Name, this.Length);
		}
		
		/// <summary>
		/// Returns true if the parent is a specific type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool IsParentOfType(KeyType type)
		{
			MXFKLV klvParent = this.Parent as MXFKLV;
			if (klvParent == null) return false;
			return (klvParent.Key.Type == type);
		}

		/// <summary>
		/// Returns true if the grandparent is a specific type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool IsGrandParentOfType(KeyType type)
		{
			if (this.Parent == null) return false;
			MXFKLV klvGrandParent = this.Parent.Parent as MXFKLV;
			if (klvGrandParent == null) return false;
			return (klvGrandParent.Key.Type == type);
		}

		/// <summary>
		/// Returns true if the grandparent is a specific type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool IsParentOrGrandParentOfType(KeyType type)
		{
			MXFKLV klvParent = this.Parent as MXFKLV;
			if (klvParent == null) return false;
			if (klvParent.Key.Type == type) return true;
			MXFKLV klvGrandParent = this.Parent.Parent as MXFKLV;
			if (klvGrandParent == null) return false;
			return (klvGrandParent.Key.Type == type);
		}
	}
}
