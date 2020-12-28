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

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Myriadbits.MXF
{
	public class MXFValidatorIndex : MXFValidator
	{
		private List<MXFIndexTableSegment> m_indexTables = new List<MXFIndexTableSegment>();
		private List<MXFSystemItem> m_systemItems = new List<MXFSystemItem>();
		private List<MXFEssenceElement> m_pictureItems = new List<MXFEssenceElement>();

		/// <summary>
		/// Check all index tables
		/// </summary>
		/// <param name="file"></param>
		/// <param name="results"></param>
		public override void OnExecuteTest(ref List<MXFValidationResult> results)
		{
			MXFValidationResult valResult = new MXFValidationResult("Index Tables");
			results.Add(valResult); // And directly add the results
			
			Stopwatch sw = Stopwatch.StartNew();

			// Clear list
			m_indexTables = new List<MXFIndexTableSegment>();
			m_systemItems = new List<MXFSystemItem>();

			// Load all partitions
			this.Task = "Loading partitions";
			List<MXFPartition> partitionsToLoad = this.File.Partitions.Where(a => !a.IsLoaded).ToList();			
			for(int n = 0; n < partitionsToLoad.Count(); n++)
			{
				int progress = (n * 50) / partitionsToLoad.Count();
				ReportProgress(progress);
				partitionsToLoad[n].Load();
			}

			// Find all index tables (and the first system index)
			this.Task = "Locating index tables";
			FindIndexTableAndSystemItems(this.File);
			ReportProgress(55);
			LogInfo("Found {0} index table segments in {1} ms", this.m_indexTables.Count, sw.ElapsedMilliseconds);
			sw.Restart();

			// Check if first index table is CBE
			if (this.m_indexTables.Count == 0)
			{
				valResult.SetError(string.Format("No index tables found!", m_indexTables.Count));
				return;
			}


			MXFIndexTableSegment firstTable = this.m_indexTables.FirstOrDefault();
			if (firstTable != null)
			{
				if (firstTable.EditUnitByteCount > 0)
				{
					// Check other tables
					for (int n = 1; n < this.m_indexTables.Count; n++ )
					{
						if (this.m_indexTables[n].EditUnitByteCount != firstTable.EditUnitByteCount &&
							this.m_indexTables[n].BodySID == firstTable.BodySID)
						{
							valResult.SetError(string.Format("Constant Bytes per Element ({0} bytes) but index table {1} has other CBE: {2} (both have BodySID {3}).", firstTable.EditUnitByteCount, n, this.m_indexTables[n].EditUnitByteCount, firstTable.BodySID));
							return;
						}
					}

					// CBE for this BodySID
					if (firstTable.IndexDuration == 0 && firstTable.IndexStartPosition == 0)
					{
						valResult.SetSuccess(string.Format("Constant Bytes per Element ({0} bytes) for the whole duration of BodySID {1}", firstTable.EditUnitByteCount, firstTable.BodySID));
						// TODO Check if the size of all compounds is the same??
						return;
					}
					else
					{
						// Complicated, mixed file
						valResult.SetWarning(string.Format("Constant Bytes per Element ({0} bytes) but not for the whole file for BodySID {1}. Duration: {2}, StartOffset: {3}", firstTable.EditUnitByteCount, firstTable.BodySID, firstTable.IndexDuration, firstTable.IndexStartPosition));
						return;
					}
				}
			}

			// Check if there are multiple index table segments pointing to the same data
			int invalidCt = 0;
			int validCt = 0;
			int totalSystemItems = 0;
			int counter = 0;
			this.Task = "Checking for duplicates";
			foreach (MXFIndexTableSegment ids in this.m_indexTables)
			{
				ReportProgress(55 + (counter * 20) / this.m_indexTables.Count());

				// And all index entries
				if (ids.IndexEntries != null)
				{					
					List<MXFIndexTableSegment> sameStuff = this.m_indexTables.Where(a => a != ids && a.IndexSID == ids.IndexSID && a.IndexStartPosition == ids.IndexStartPosition).ToList();
					foreach (MXFIndexTableSegment sameIds in sameStuff)
					{
						if (sameIds.IndexEntries.Count != ids.IndexEntries.Count)
						{
							valResult.AddError(string.Format("Index {0} in partition {1} is not the same length as in partition {2}!", sameIds.IndexSID, sameIds.Partition.ToString(), ids.Partition.ToString()));
							invalidCt++;
						}
						else
						{
							for (int n = 0; n < sameIds.IndexEntries.Count; n++)
							{
								if (ids.IndexEntries[n].KeyFrameOffset != sameIds.IndexEntries[n].KeyFrameOffset ||
									ids.IndexEntries[n].StreamOffset != sameIds.IndexEntries[n].StreamOffset ||
									ids.IndexEntries[n].TemporalOffset != sameIds.IndexEntries[n].TemporalOffset)
								{
									valResult.AddError(string.Format("The indexentry {0} of Index {1} in partition {2} is not the same data as in partition {3}!", n, sameIds.IndexSID, sameIds.Partition.ToString(), ids.Partition.ToString()));
									invalidCt++;
									break;
								}
							}
						}
					}
				}
				counter++;
			}


			// First try to match system items
			if (m_systemItems.Count != 0)
			{
				// For each index table segment				
				totalSystemItems = m_systemItems.Count();
				counter = 0;
				this.Task = "Checking essence offsets";
				foreach(MXFIndexTableSegment ids in this.m_indexTables)
				{
					ReportProgress(75 + (counter * 20) / this.m_indexTables.Count());

					// And all index entries
					if (ids.IndexEntries != null)
					{
						foreach (MXFEntryIndex index in ids.IndexEntries)
						{
							// Check if there is a system item at this offset
							long searchIndex = (long)(index.StreamOffset);
							MXFSystemItem si = this.m_systemItems.Where(a => a.EssenceOffset == searchIndex).FirstOrDefault();
							if (si != null)
							{
								// Yes, found
								validCt++;
								si.Indexed = true;
							}
							else
							{
								// Not found
								valResult.AddError(string.Format("Index {0} not pointing to valid essence!", index.Index));
								invalidCt++;
							}
						}
					}
					counter++;
				}
			}
			else if (m_pictureItems.Count != 0)
			{
				// Now try to match the picture essences

				// For each index table segment				
				counter = 0;
				totalSystemItems = m_pictureItems.Count();
				this.Task = "Checking picture offsets";
				foreach (MXFIndexTableSegment ids in this.m_indexTables)
				{
					ReportProgress(75 + (counter * 20) / this.m_indexTables.Count());

					// And all index entries
					if (ids.IndexEntries != null)
					{
						foreach (MXFEntryIndex index in ids.IndexEntries)
						{
							// Check if there is a system item at this offset
							long searchIndex = (long)(index.StreamOffset);
							MXFEssenceElement ee = this.m_pictureItems.Where(a => a.EssenceOffset == searchIndex).FirstOrDefault();
							if (ee != null)
							{
								// Yes, found
								validCt++;
								ee.Indexed = true;
							}
							else
							{
								// Not found
								valResult.AddError(string.Format("Index {0} not pointing to a valid picture essence!", index.Index));
								invalidCt++;
							}
						}
					}
				}
			}
			else
			{
				valResult.SetError(string.Format("No system items and/or picture essences found (found {0} index table segments)", m_indexTables.Count));
				return;
			}


			bool fError = false;
			if (this.m_systemItems.Count() > 0)
			{
				if (this.m_systemItems.Count(a => !a.Indexed) != 0)
				{
					// Hmm still some items left in the array
					valResult.SetError(string.Format("There are {0} essence elements (of the total {1}) that are not referenced in an index table!", this.m_systemItems.Count, totalSystemItems));
					fError = true;
				}
			}
			else if (this.m_pictureItems.Count() > 0)
			{
				if (this.m_pictureItems.Count(a => !a.Indexed) != 0)
				{
					// Hmm still some items left in the array
					valResult.SetError(string.Format("There are {0} essence elements (of the total {1}) that are not referenced in an index table!", this.m_pictureItems.Count, totalSystemItems));
					fError = true;
				}
			}
			

			if (!fError)
			{
				if (invalidCt > 0)
					valResult.SetError(string.Format("Found {0} index errors! There are {0} indices that are NOT pointing to valid essence data (valid {1})!", invalidCt, validCt));
				else if (validCt > 0)
					valResult.SetSuccess(string.Format("Index table is valid! All {0} index entries point to valid essences!", validCt));
				if (validCt == 0 && invalidCt == 0)
					valResult.SetError(string.Format("No valid indexes found in this file!"));
			}
			LogInfo("Validation completed in {0} msec", sw.ElapsedMilliseconds);


			// Check system item range
			if (this.m_systemItems.Count() > 0)
			{
				CheckUserDates(this.File, results);
				CheckContinuityCounter(this.File, results);
			}
			ReportProgress(100); 

		}


		/// <summary>
		/// Check the essence range
		/// </summary>
		/// <param name="file"></param>
		/// <param name="results"></param>
		protected void CheckUserDates(MXFFile file, List<MXFValidationResult> results)
		{			
			List<MXFSystemItem> items = this.m_systemItems.OrderBy(a => a.ContinuityCount).ToList();
			if (items.Count > 1)
			{
				MXFTimeStamp ts = new MXFTimeStamp(items.First().UserDate);
				if (ts != null)
				{
					MXFValidationResult valResult = new MXFValidationResult("System Items");
					results.Add(valResult); // And directly add the results

					MXFTimeStamp tsLast = null; 
					for (int n = 1; n < items.Count() - 1; n++) // Skip last one (always invalid??)
					{
						ts.Increase();
						if (!items[n].UserDate.IsEmpty())
						{
							if (!items[n].UserDate.IsSame(ts))
							{
								valResult.SetError(string.Format("Invalid user date at offset {0} (was {1}, expected {2})!", items[n].Offset, items[n].UserDate, ts));
								return;
							}
							tsLast = items[n].UserDate;
						}
					}
					if (tsLast != null)
						valResult.SetSuccess(string.Format("UserDates are continious from {0} to {1}, at {2} fps!", items.First().UserDate, tsLast, ts.FrameRate));
					else
						valResult.SetSuccess(string.Format("UserDates are continious!"));
				}
			}
		}


			/// <summary>
		/// Check the essence range
		/// </summary>
		/// <param name="file"></param>
		/// <param name="results"></param>
		protected void CheckContinuityCounter(MXFFile file, List<MXFValidationResult> results)
		{
			MXFValidationResult valResult = new MXFValidationResult("System Items");
			results.Add(valResult); // And directly add the results
			
			// Check for continous range
			int cc = -1;
			int errorCount = 0;
			foreach (MXFSystemItem si in this.m_systemItems)
			{
				if (si.ContinuityCount - cc != 1)
				{
					errorCount++;
					//valResult.SetError(string.Format("Invalid continuity count for system item at offset {0}. CC should be {1} but is {2}!", si.Offset, cc + 1, si.ContinuityCount));
					//return;
				}
				cc = si.ContinuityCount;
			}

			if (errorCount > 0)
			{
				if (errorCount >= this.m_systemItems.Count() - 1)
					valResult.SetWarning(string.Format("All continuity counter values are not set!"));
				else
					valResult.SetError(string.Format("Found {0} invalid continuity counter values (total system items {1})!", errorCount, this.m_systemItems.Count()));
			}
			else
			{
				valResult.SetSuccess(string.Format("Continuity counter values are correct!"));
			}			
		}


		/// <summary>
		/// Locate all index table segments RECURSIVELY
		/// </summary>
		/// <param name="current"></param>
		public void FindIndexTableAndSystemItems(MXFObject current)
		{
			// LOAD the object (when not yet loaded)
			// This may take some time!!!
			if(current is ILazyLoadable loadable)
            {
				loadable.Load();
			}
			
			MXFKLV klv = current as MXFKLV;
			if (klv != null)
			{
				if (klv.Key.Type == KeyType.IndexSegment)
				{
					MXFIndexTableSegment its = klv as MXFIndexTableSegment;
					if (its != null)
						this.m_indexTables.Add(its);
				}
				else if (klv.Key.Type == KeyType.SystemItem)
				{
					MXFSystemItem si = klv as MXFSystemItem;
					if (si != null)
					{
						si.Indexed = false;
						this.m_systemItems.Add(si);
					}
				}
				else if (klv.Key.Type == KeyType.Essence)
				{
					MXFEssenceElement ee = klv as MXFEssenceElement;
					if (ee != null)
					{
						if (ee.IsPicture)
						{
							ee.Indexed = false;
							this.m_pictureItems.Add(ee);
						}
					}
				}
			}

			if (!current.Children.Any())
				return;

			foreach(MXFObject child in current.Children)
			{
				FindIndexTableAndSystemItems(child);
			}
		}

	}
}
