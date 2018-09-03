using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{	
	/// <summary>
	/// Meta data class (might) consists of multiple local tags
	/// </summary>
	public class MXFMetaData : MXFKLV
	{
		[CategoryAttribute("Metadata"), ReadOnly(true)]
		public string MetaDataName { get; set; }
		[CategoryAttribute("Metadata"), ReadOnly(true)]
		public MXFKey UID { get; set; }
	
		public MXFMetaData(MXFReader reader, MXFKLV headerKLV)
			: base(headerKLV, "MetaData", KeyType.MetaData)
		{
			this.MetaDataName = "<unknown>";
			Initialize(reader);
		}

		public MXFMetaData(MXFReader reader, MXFKLV headerKLV, string metaDataName)
			: base(headerKLV, "MetaData", KeyType.MetaData)
		{
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

				// Parse UID tag (if present)
				if (tag.Tag > 0x7FFF)
				{ 
					// dynamic tag
					// TODO
					Log(LogType.Warning, "TODO Implement dynamic tags!");
					//int i;
					//for (i = 0; i < mxf->local_tags_count; i++)
					//{
					//	int local_tag = AV_RB16(mxf->local_tags + i * 18);
					//	if (local_tag == tag)
					//	{
					//		memcpy(uid, mxf->local_tags + i * 18 + 2, 16);
					//		av_dlog(mxf->fc, "local tag %#04x\n", local_tag);
					//		PRINT_KEY(mxf->fc, "uid", uid);
					//	}
					//}
				}
				if (tag.Tag == 0x3C0A)
				{
					this.UID = new MXFKey(reader, 16);
				}
				else
				{
					// Allow derived classes to handle the data
					if (!ParseLocalTag(reader, tag))
					{						
						// Not processed, use default
						tag.Parse(reader);

						// Add to the collection
						AddChild(tag);
					}
				}
				
				reader.Seek(next);
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
		/// Read a list of keys
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="categoryName"></param>
		/// <returns></returns>
		protected UInt32 ReadKeyList(MXFReader reader, string categoryName, string singleItem)
		{
			UInt32 nofItems = reader.ReadD();
			UInt32 objectSize = reader.ReadD(); // useless size of objects, always 16 according to specs

			MXFObject keylist = new MXFObject(categoryName, reader.Position);
			if (nofItems < UInt32.MaxValue)
			{
				for (int n = 0; n < nofItems; n++)
				{
					MXFRefKey key = new MXFRefKey(reader, objectSize, singleItem);
					keylist.AddChild(key);
				}
			}
			this.AddChild(keylist);
			return nofItems;
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
