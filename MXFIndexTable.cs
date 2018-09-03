using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXFInspect
{
	public class MXFIndexTable : MXFKLV
	{
		public MXFIndexTable(MXFReader reader, MXFKLV headerKLV)
			: base(headerKLV)
		{
			// Make sure we read at the data position
			reader.Seek(this.DataOffset);

		}

//		 int index_sid;
//00195     int body_sid;
//00196     int nb_ptses;               /* number of PTSes or total duration of index */
//00197     int64_t first_dts;          /* DTS = EditUnit + first_dts */
//00198     int64_t *ptses;             /* maps EditUnit -> PTS */
//00199     int nb_segments;
//00200     MXFIndexTableSegment **segments;    /* sorted by IndexStartPosition */
//00201     AVIndexEntry *fake_index;   /* used for calling ff_index_search_timestamp() */

	}
}
