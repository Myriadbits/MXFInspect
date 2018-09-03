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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
	public enum KeyType
	{
		None,
		// Real MXF types
		Partition,
		PackageMetaDataSet,
		Essence,
		IndexSegment,
		MetaData,
		SystemItem,
		PrimerPack,
		Preface,
		Filler,
		RIP
	}


	public enum KeyField
	{
		Unknown = 0x00,
		Dictionary = 0x01,
		Group = 0x02,
		Container = 0x03,
		Label = 0x04
	}


	public struct MXFShortKey
	{
		UInt64 Key1;
		UInt64 Key2;

		public MXFShortKey(UInt64 key1, UInt64 key2)
		{
			this.Key1 = key1;
			this.Key2 = key2;
		}

		public MXFShortKey(byte[] data)
		{
			// Change endianess
			this.Key1 = 0;
			this.Key2 = 0;
			if (data.Length == 16)
			{
				byte[] datar = new byte[16];
				Array.Copy(data, datar, 16);
				Array.Reverse(datar);
				this.Key2 = BitConverter.ToUInt64(datar, 0);
				this.Key1 = BitConverter.ToUInt64(datar, 8);
			}
		}

		public override string ToString()
		{
			return string.Format(string.Format("{0:X16}.{1:X16}", this.Key1, this.Key2));
		}

	};

	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class MXFKey : IEquatable<MXFKey>
	{
		private static Dictionary<MXFShortKey, string[]> m_ULDescriptions;
		private byte[] m_mxfKey = null;
		private bool m_fIsUID = false;

	
		static MXFKey()
		{
			m_ULDescriptions = new Dictionary<MXFShortKey,  string[]>();
			
			// Read SMPTE RP224 data
			string allText = MXF.Properties.Resources.UL_Identifiers;
			string[] allLines = allText.Split('\n');
			if (allLines.Count() > 3)
			{
				string temp;
				for (int n = 3; n < allLines.Count(); n++) // Start the first 3 header lines
				{
					string line = allLines[n];
					string[] parts = line.Split(';');
					if (parts.Count() > 6)
					{
						try
						{
							UInt64 value1 = 0;
							UInt64 value2 = 0;
							if (!string.IsNullOrEmpty(parts[1]))
							{
								temp = parts[1].Trim(' ');
								temp = temp.Replace(".", "");
								value1 = Convert.ToUInt64(temp, 16);
							}
							if (!string.IsNullOrEmpty(parts[2]))
							{
								temp = parts[2].Trim(' ');
								temp = temp.Replace(".", "");
								value2 = Convert.ToUInt64(temp, 16);
							}
							MXFShortKey shortKey = new MXFShortKey(value1, value2);
							m_ULDescriptions.Add(shortKey, new string[] { parts[4], parts[5], parts[6] });
							//Debug.WriteLine("combinedDID = {0}, Name = {1} ({2}) [{3}]", shortKey, parts[4], parts[5], parts[6]);
						}
						catch (Exception)
						{
						}
					}
				}
			}


			// Read SMPTE RP210 data
			allText = MXF.Properties.Resources.MD_Identifiers;
			allLines = allText.Split('\n');
			if (allLines.Count() > 7)
			{
				for (int n = 7; n < allLines.Count(); n++) // Start the first 7 header lines
				{
					string line = allLines[n];
					string[] parts = line.Split(';');
					if (parts.Count() > 8)
					{
						try
						{
							string fullID = parts[5].Replace(".", "");
							if (!string.IsNullOrEmpty(fullID))
							{
								UInt64 value1 = 0;
								UInt64 value2 = 0;
								string svalue1 = fullID.Substring(0, 16);
								string svalue2 = fullID.Substring(16, 16);
								if (!string.IsNullOrEmpty(svalue1))
									value1 = Convert.ToUInt64(svalue1, 16);
								if (!string.IsNullOrEmpty(svalue2))
									value2 = Convert.ToUInt64(svalue2, 16);
								MXFShortKey shortKey = new MXFShortKey(value1, value2);
								if (parts.Count() > 12)
									m_ULDescriptions.Add(shortKey, new string[] { string.Format("{0} - {1}", parts[6], parts[7]), parts[8], parts[13] });
								else
									m_ULDescriptions.Add(shortKey, new string[] { string.Format("{0} - {1}", parts[6], parts[7]), parts[8], string.Empty });
							}
						}
						catch (Exception)
						{
						}
					}
				}
			}
		}

		[Browsable(false)]
		public int Length 
		{ 
			get
			{
				if (this.m_mxfKey == null)
					return 0;
				return this.m_mxfKey.Length;
			}
		}

		[Browsable(false)]
		public KeyType Type { get; set; }
		[Browsable(false)]
		public Type ObjectType { get; set; }

		/// <summary>
		/// The name of this key (if found in SMPTE RP210 or RP224)
		/// </summary>
		[CategoryAttribute("Key"), ReadOnly(true)]
		public string Name { get; set; }

		/// <summary>
		/// Keyfield, describes the type of data
		/// </summary>
		[Browsable(false)]
		public KeyField KeyField // TODO
		{
			get
			{
				if (this.m_mxfKey != null && this.m_mxfKey.Length > 4)
					return (KeyField)this.m_mxfKey[5];
				return KeyField.Unknown;
			}
		}

		/// <summary>
		/// Create a new key
		/// </summary>
		/// <param name="list"></param>
		public MXFKey(params int[] list)
		{
			this.Type = KeyType.None;
			Initialize(list);
		}

		/// <summary>
		/// Create a new key
		/// </summary>
		/// <param name="list"></param>
		public MXFKey(string name, params int[] list)
		{
			this.Type = KeyType.None;
			this.Name = name;
			Initialize(list);
		}

		/// <summary>
		/// Create a new key
		/// </summary>
		/// <param name="list"></param>
		public MXFKey(Type objectType, params int[] list)
		{
			this.ObjectType = objectType;
			Initialize(list);
		}

		/// <summary>
		/// Create a new key
		/// </summary>
		/// <param name="list"></param>
		public MXFKey(string name, KeyType type, params int[] list)
		{
			this.Name = name;
			this.Type = type;
			Initialize(list);
		}

		/// <summary>
		/// Initialize the key value
		/// </summary>
		/// <param name="list"></param>
		private void Initialize(params int[] list)
		{
			this.m_mxfKey = new byte[list.Length];
			for (int n = 0; n < list.Length; n++)
				m_mxfKey[n] = (byte)list[n];

			FindKeyName();
		}

		/// <summary>
		/// Create a new key combining 2 parts, first should be 4 bytes
		/// </summary>
		/// <param name="firstPart"></param>
		/// <param name="reader"></param>
		public MXFKey(byte b0, byte b1, byte b2, byte b3, MXFReader reader)
		{
			this.m_mxfKey = null;
			int len = b1 + 2;
			if (len >= 4)
			{
				this.m_mxfKey = new byte[len];
				this.m_mxfKey[0] = b0;
				this.m_mxfKey[1] = b1;
				this.m_mxfKey[2] = b2;
				this.m_mxfKey[3] = b3;

				for (int n = 4; n < len; n++)
					m_mxfKey[n] = reader.ReadB();
			}

			FindKeyName();
		}

		/// <summary>
		/// Create a new key by reading from the current file location
		/// </summary>
		/// <param name="firstPart"></param>
		/// <param name="reader"></param>
		public MXFKey(MXFReader reader)
		{
			byte isoMark = reader.ReadB();
			byte length = reader.ReadB();
			this.m_mxfKey = new byte[length];
			for (int n = 2; n < length; n++)
				m_mxfKey[n] = reader.ReadB();

			FindKeyName();
		}


		/// <summary>
		/// Create a new key by reading from the current file location with a fixed size
		/// </summary>
		/// <param name="firstPart"></param>
		/// <param name="reader"></param>
		public MXFKey(MXFReader reader, UInt32 length)
		{
			this.m_mxfKey = new byte[length];
			for (int n = 0; n < length; n++)
				m_mxfKey[n] = reader.ReadB();

			FindKeyName();
		}

		/// <summary>
		/// Locate the key name (if found)
		/// </summary>
		private void FindKeyName()
		{
			MXFShortKey skey = this.ShortKey;
			if (m_ULDescriptions.ContainsKey(skey))
			{
				this.Name = m_ULDescriptions[skey][0];
				this.m_fIsUID = false;
			}
			else
			{
				this.Name = "UID"; // Not in the global UL list, probably an unique ID
				this.m_fIsUID = true;
			}
		}


		/// <summary>
		/// Return a byte of the key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[Browsable(false)]
		public byte this[int key]
		{
			get
			{
				if (this.m_mxfKey == null)
					return 0;
				if (key >= 0 && key < this.m_mxfKey.Length)
					return m_mxfKey[key];
				return 0;
			}
			set
			{
				if (this.m_mxfKey != null && key >= 0 && key < this.m_mxfKey.Length)
					m_mxfKey[key] = (byte) value;
			}
		}


		[CategoryAttribute("Key"), ReadOnly(true)]
		public MXFShortKey ShortKey 
		{ 
			get
			{
				return new MXFShortKey(this.m_mxfKey);
			}
		}


		/// <summary>
		/// Return a description if available
		/// </summary>
		[CategoryAttribute("Key"), ReadOnly(true)]
		public string Description
		{
			get
			{
				MXFShortKey skey = this.ShortKey;
				if (m_ULDescriptions.ContainsKey(skey))
					return m_ULDescriptions[skey][1];
				return string.Empty;
			}
		}


		/// <summary>
		/// Return a description if available
		/// </summary>
		[CategoryAttribute("Key"), ReadOnly(true)]
		public string Information
		{
			get
			{
				MXFShortKey skey = this.ShortKey;
				if (m_ULDescriptions.ContainsKey(skey))
					return m_ULDescriptions[skey][2];
				return string.Empty;
			}
		}


		//
		//
		// Equal stuff
		//
		//

		
		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (m_fIsUID)
			{
				StringBuilder sb = new StringBuilder();
				if (!string.IsNullOrEmpty(this.Name))
					sb.Append(this.Name + " - ");
				sb.Append("{");
				for (int n = 0; n < this.Length; n++)
				{
					if (n > 0)
						sb.Append(", ");
					sb.Append(string.Format("{0:X2}", this.m_mxfKey[n]));
				}
				sb.Append("}");
				return sb.ToString();
			}
			else
				return this.Name;
		}

		/// <summary>
		/// Equal keys?
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(MXFKey other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			int len = Math.Min(this.Length, other.Length);
			if (len == 0) return false;
			for (int n = 0; n < len; n++)
				if (this[n] != other[n])
					return false;
			return true;
		}

		/// <summary>
		/// Equal to object?
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(MXFKey)) return false;
			return Equals((MXFKey)obj);
		}

		public override int GetHashCode() { return this.m_mxfKey.GetHashCode(); }
		public static bool operator ==(MXFKey x, MXFKey y) { return Equals(x, y); }
		public static bool operator !=(MXFKey x, MXFKey y) { return !Equals(x, y); }
	}
}
