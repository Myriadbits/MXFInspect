﻿//
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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Myriadbits.MXF
{
	public enum FileParseOptions
	{
		Normal,
		Fast,
	}


	public enum PartialSeekMode
	{
		Unknown,
		UsingRIP,
		Backwards,
		Full,
	}


	/// <summary>
	/// Name object, represents a complete MXF file
	/// </summary>
	public class MXFFile : MXFObject
	{
		private MXFReader m_reader;
		private List<MXFValidationResult> m_results;

		public string Filename { get; set; }
		public long Filesize { get; set; }

		public List<MXFPartition> Partitions { get; set; }
		public MXFRIP RIP { get; set; }

		public List<MXFValidationResult> Results { get { return m_results;} }
		public MXFSystemItem FirstSystemItem { get; set; }
		public MXFSystemItem LastSystemItem { get; set; }

		public List<MXFObject> FlatList { get; set; }

		public MXFLogicalObject LogicalBase { get; set; }


		/// <summary>
		/// Create/open an MXF file
		/// </summary>
		/// <param name="fileName"></param>
		public MXFFile(string fileName, BackgroundWorker worker, FileParseOptions options = FileParseOptions.Normal)
		{
			this.Filename = fileName;
			this.Partitions = new List<MXFPartition>();
			this.m_results = new List<MXFValidationResult>();

			
			switch (options)
			{
				case FileParseOptions.Normal:
					ParseFull(worker);
					break;

				case FileParseOptions.Fast:
					ParsePartial(worker);
					break;
			}

		}


		/// <summary>
		/// Fully Parse an MXF file 
		/// </summary>
		protected void ParseFull(BackgroundWorker worker)
		{
			Stopwatch sw = Stopwatch.StartNew();

			MXFKLVFactory klvFactory = new MXFKLVFactory();

			MXFPartition currentPartition = null;
			int previousPercentage = 0;
			Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys = null;
			int[] counters = new int[Enum.GetNames(typeof(KeyType)).Length];
			using (m_reader = new MXFReader(this.Filename))
			{
				this.Filesize = m_reader.Size;
				MXFObject partitions = new MXFNamedObject("Partitions", 0);
				this.AddChild(partitions);

				int partitionNumber = 0; // For easy partition identification
				while (!m_reader.EOF)
				{
					MXFKLV klv = klvFactory.CreateObject(m_reader, currentPartition);

					// Update overall counters
					if (klv.Key.Type == KeyType.None)
						counters[(int)klv.Key.Type]++;

					// Process the new KLV
					ProcessKLVObject(klv, partitions, ref currentPartition, ref partitionNumber, ref allPrimerKeys);

					// Next KLV please
					m_reader.Seek(klv.DataOffset + klv.Length);

					// Only report progress when the percentage has changed
					int currentPercentage = (int)((m_reader.Position * 90) / m_reader.Size);
					if (currentPercentage != previousPercentage)
					{
						worker.ReportProgress(currentPercentage, "Parsing MXF file");
						previousPercentage = currentPercentage;
					}
				}
			}
			// Progress should now be 90%

			// Update all type descriptions
			klvFactory.UpdateAllTypeDescriptions(allPrimerKeys);
			worker.ReportProgress(91, "Creating key list");

			Debug.WriteLine("Finished parsing file '{0}' in {1} ms", this.Filename, sw.ElapsedMilliseconds);
			sw.Restart();

			// Create a list with all UID keys
			Dictionary<string, MXFObject> allKeys = new Dictionary<string, MXFObject>();
			CreateKeyList(allKeys, this);
			worker.ReportProgress(92, "Creating resolving references");

			// Resolve the references
			ResolveReferences(allKeys, this);
			Debug.WriteLine("Finished resolving references in {0} ms", sw.ElapsedMilliseconds);
			sw.Restart();
			worker.ReportProgress(93, "Resolving flatlist");

			this.FlatList = new List<MXFObject>();
			this.AddToList(this.FlatList);
			Debug.WriteLine("Flatlist created in {0} ms", sw.ElapsedMilliseconds);
			sw.Restart();
			worker.ReportProgress(94, "Creating Logical tree");


			// Create the logical tree
			CreateLogicalTree();
			Debug.WriteLine("Logical tree created in {0} ms", sw.ElapsedMilliseconds);
			sw.Restart();
			
			// And Execute ALL test
			this.ExecuteValidationTest(worker, true);

			// Finished
			worker.ReportProgress(100, "Finished");
		}

		
		/// <summary>
		/// Partially Parse an MXF file, skip all data
		/// </summary>
		protected void ParsePartial(BackgroundWorker worker)
		{
			Stopwatch sw = Stopwatch.StartNew();

			MXFKLVFactory klvFactory = new MXFKLVFactory();

			MXFPartition currentPartition = null;
			int previousPercentage = 0;
			Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys = null;
			int[] counters = new int[Enum.GetNames(typeof(KeyType)).Length];
			PartialSeekMode seekMode = PartialSeekMode.Unknown;
			using (m_reader = new MXFReader(this.Filename))
			{
				this.Filesize = m_reader.Size;
				MXFObject partitions = new MXFNamedObject("Partitions", 0);
				this.AddChild(partitions);

				// Start with trying to find the RIP
				bool ripFound = ReadRIP(klvFactory);
				if (ripFound)
					seekMode = PartialSeekMode.UsingRIP;
				m_reader.Seek(0); // Start at the beginning

				// Start by reading the first partition
				int partitionNumber = 0; // For easy partition identification
				while (!m_reader.EOF && seekMode != PartialSeekMode.Backwards) // Eof and NOT searching backwards
				{
					MXFKLV klv = klvFactory.CreateObject(m_reader, currentPartition);

					// Update overall counters
					if (klv.Key.Type == KeyType.None)
						counters[(int)klv.Key.Type]++;

					if (klv.Key.Type == KeyType.Partition && seekMode == PartialSeekMode.Backwards)
					{
						if (this.Partitions.Exists(a => a.Offset == klv.Offset))
						{
							// A new partition has been found that we already found, quit the main loop
							break;
						}
					}


					// Process the new KLV
					ProcessKLVObject(klv, partitions, ref currentPartition, ref partitionNumber, ref allPrimerKeys);


					// If we found the second partition 
					long nextSeekPosition = klv.DataOffset + klv.Length;
					if (partitionNumber >= 2) // Header fully read, now busy with the second partition
					{
						switch (seekMode)
						{
							case PartialSeekMode.UsingRIP: // And we already found the RIP
								if (currentPartition.FirstSystemItem != null) // And we found the first system item
								{
									MXFEntryRIP ripEntry = this.RIP.GetPartition(partitionNumber);
									if (ripEntry != null)
									{
										// Mark the current partition as not-completely read
										currentPartition.IsLoaded = false;

										// Start at the next partition
										nextSeekPosition = (long)ripEntry.PartitionOffset;
									}
								}
								break;
						
							case PartialSeekMode.Backwards: // NO RIP, searching backwards
								// Backwards, jump to the PREVIOUS partition
								if (currentPartition.FirstSystemItem != null) // And we found the first system item
								{
									// Jump to the previous partition
									if (currentPartition.PreviousPartition != 0)
									{
										// And we haven't found this partition yet
										if (!this.Partitions.Exists(a => a.ThisPartition == currentPartition.PreviousPartition))
											nextSeekPosition = (long)currentPartition.PreviousPartition; // Jump to previous
									}
								}
								break;

							case PartialSeekMode.Unknown: // No RIP....
								// Hmmm, RIP is not found, check if we have a footer partition somewhere
								MXFPartition part = this.Partitions.Where(a => a.FooterPartition != 0).FirstOrDefault();
								if (part != null)
								{
									// If we are already at the footer, don't bother to seek
									if (currentPartition.Offset != (long)part.FooterPartition)
									{
										nextSeekPosition = (long)part.FooterPartition; // Start at the footer
										seekMode = PartialSeekMode.Backwards;
									}
								}
								break;
						}
					}

					// Next KLV please
					m_reader.Seek(nextSeekPosition);

					// Only report progress when the percentage has changed
					int currentPercentage = (int)((m_reader.Position * 90) / m_reader.Size);
					if (currentPercentage != previousPercentage)
					{
						worker.ReportProgress(currentPercentage, "Partial Parsing MXF file");
						previousPercentage = currentPercentage;
					}
				}
			}
			// Progress should now be 90%

			// Update all type descriptions
			klvFactory.UpdateAllTypeDescriptions(allPrimerKeys);
			Debug.WriteLine("Finished parsing file '{0}' in {1} ms", this.Filename, sw.ElapsedMilliseconds);
			sw.Restart();

			// Create a list with all UID keys
			worker.ReportProgress(91, "Creating key list");
			Dictionary<string, MXFObject> allKeys = new Dictionary<string, MXFObject>();
			CreateKeyList(allKeys, this);

			// Resolve the references
			worker.ReportProgress(92, "Resolving references");
			ResolveReferences(allKeys, this);
			Debug.WriteLine("Finished resolving references in {0} ms", sw.ElapsedMilliseconds);
			sw.Restart();

			this.FlatList = new List<MXFObject>();
			worker.ReportProgress(93, "Creating flat list");
			this.AddToList(this.FlatList);
			Debug.WriteLine("Flatlist created in {0} ms", sw.ElapsedMilliseconds);
			sw.Restart();


			// Create the logical tree
			worker.ReportProgress(94, "Creating logical tree");
			CreateLogicalTree();
			Debug.WriteLine("Logical tree created in {0} ms", sw.ElapsedMilliseconds);
			sw.Restart();

			// And Execute FAST tests
			this.ExecuteValidationTest(worker, false);

			// Finished
			worker.ReportProgress(100, "Finished");
		}

		/// <summary>
		/// Process a new KLV object
		/// </summary>
		/// <param name="klv"></param>
		/// <param name="partitions"></param>
		/// <param name="currentPartition"></param>
		/// <param name="partitionNumber"></param>
		/// <param name="allPrimerKeys"></param>
		private void ProcessKLVObject(MXFKLV klv, MXFObject partitions, ref MXFPartition currentPartition, ref int partitionNumber, ref Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys)
		{
			// Is this a header, add to the partitions
			switch (klv.Key.Type)
			{
				case KeyType.Partition:
					currentPartition = klv as MXFPartition;
					currentPartition.File = this;
					currentPartition.PartitionNumber = partitionNumber;
					this.Partitions.Add(currentPartition);
					partitions.AddChild(currentPartition);
					partitionNumber++;
					break;

				case KeyType.PrimerPack:
					if (currentPartition != null)
					{
						MXFPrimerPack primer = klv as MXFPrimerPack;
						if (primer != null) // Just to be sure
						{
							// Let the partition know all primer keys
							allPrimerKeys = primer.AllKeys;
							currentPartition.PrimerKeys = primer.AllKeys;
						}
						currentPartition.AddChild(klv); // Add the primer 
					}
					break;

				case KeyType.RIP:
					// Only add the RIP when not yet present
					if (this.RIP == null)
					{
						this.AddChild(klv);
						this.RIP = klv as MXFRIP;
					}
					break;

				case KeyType.SystemItem:
					if (currentPartition != null)
					{
						// Store the first system item for every partition
						// (required to calculate essence positions)
						if (currentPartition.FirstSystemItem == null)
							currentPartition.FirstSystemItem = klv as MXFSystemItem;
						currentPartition.AddChild(klv);
					}
					else
						this.AddChild(klv);

					// Store the first and the last system item
					if (this.FirstSystemItem == null)
						this.FirstSystemItem = klv as MXFSystemItem;
					this.LastSystemItem = klv as MXFSystemItem;
					break;


				case KeyType.Essence:
					if (currentPartition != null)
					{
						// Store the first system item for every partition
						// (required to calculate essence positions)
						MXFEssenceElement ee = klv as MXFEssenceElement;
						if (ee.IsPicture && currentPartition.FirstPictureEssenceElement == null)
							currentPartition.FirstPictureEssenceElement = ee;
						currentPartition.AddChild(klv);
					}
					else
						this.AddChild(klv);
					break;

				case KeyType.Preface:
					this.LogicalBase = new MXFLogicalObject(klv, klv.ToString());
					// Normal
					if (currentPartition != null)
						currentPartition.AddChild(klv);
					else
						this.AddChild(klv);
					break;

				default:
					// Normal
					if (currentPartition != null)
						currentPartition.AddChild(klv);
					else
						this.AddChild(klv);
					break;
			}
		}



		/// <summary>
		/// Try to locate the RIP
		/// </summary>
		private bool ReadRIP(MXFKLVFactory klvFactory)
		{
			if (this.RIP == null)
			{
				// Read the last 4 bytes of the file
				m_reader.Seek(this.Filesize - 4);
				uint ripSize = m_reader.ReadD();
				if (ripSize < this.Filesize && ripSize >= 4) // At least 4 bytes
				{
					m_reader.Seek(this.Filesize - ripSize);					
					MXFKLV klv = klvFactory.CreateObject(m_reader, null);
					if (klv.Key.Type == KeyType.RIP)
					{
						// Yes, RIP found
						this.AddChild(klv);
						this.RIP = klv as MXFRIP;
						return true;
					}
				}
			}
			return false;
		}


		/// <summary>
		///  Execute all validation tests
		/// </summary>
		public void ExecuteValidationTest(BackgroundWorker worker, bool extendedTest)
		{
			// Reset results
			this.m_results.Clear();

			// Execute validation tests
			List<MXFValidator> allTest = new List<MXFValidator>();
			allTest.Add(new MXFValidatorInfo());
			allTest.Add(new MXFValidatorPartitions());
			allTest.Add(new MXFValidatorRIP());
			if (extendedTest)
			{
				allTest.Add(new MXFValidatorIndex());
			}
			foreach(MXFValidator mxfTest in allTest)
			{
				mxfTest.Initialize(this, worker);
				mxfTest.ExecuteTest(ref m_results);
			}			

			if (!extendedTest)
			{
				MXFValidationResult valResult = new MXFValidationResult("Index Table");
				this.m_results.Add(valResult);
				valResult.SetWarning("Index table test not executed in partial loading mode (to execute test press the execute all test button).");
				//MXFValidationResult valResult = new MXFValidationResult("Index Tables");				
				//this.Results.Add(valResult); // And directly add the results
			}
		}
			
		/// <summary>
		/// Create the logical view (starting with the preface)
		/// </summary>
		protected void CreateLogicalTree()
		{
			if (this.LogicalBase == null)
				return;

			LogicalAddChilds(this.LogicalBase);			
		}

		/// <summary>
		/// Add all children (recusive)
		/// </summary>
		/// <param name="parent"></param>
		protected MXFLogicalObject LogicalAddChilds(MXFLogicalObject parent)
		{
			// Check properties for reference
			MXFObject reference = parent.Object;
			if (reference != null)
			{
				foreach (PropertyInfo propertyInfo in reference.GetType().GetProperties())
				{
					if (propertyInfo.CanRead)
					{
						if (propertyInfo.PropertyType == typeof(MXFRefKey))
						{
							// Found one!
							MXFRefKey refKey = (MXFRefKey)propertyInfo.GetValue(reference, null);
							if (refKey != null && refKey.Reference != null)
							{
								// Add the child
								MXFLogicalObject lo = new MXFLogicalObject(refKey.Reference, refKey.Reference.ToString());
								parent.AddChild(lo);

								// Add all sub stuff
								LogicalAddChilds(lo);
							}
						}
					}
				}

				if (reference.HasChildren)
				{
					foreach (MXFObject child in reference.Children)
					{
						if (child.HasChildren)
						{
							foreach (MXFObject grandchild in child.Children)
							{
								MXFRefKey refKey = grandchild as MXFRefKey;
								if (refKey != null && refKey.Reference != null)
								{
									MXFLogicalObject lo = new MXFLogicalObject(refKey.Reference, refKey.Reference.ToString());

									// Add the child
									parent.AddChild(lo);

									// Add all sub stuff
									LogicalAddChilds(lo);
								}
							}
						}
					}
				}
			}
			return parent;
		}

		/// <summary>
		/// Loop through all items, when an item with a reference is found, 
		/// </summary>
		/// <param name="parent"></param> 
		protected void ResolveReferences(Dictionary<string, MXFObject> allKeys, MXFObject parent)
		{
			MXFRefKey refParent = parent as MXFRefKey;
			if ((object)refParent != null)
			{
				if (allKeys.ContainsKey(refParent.Key.ShortKey.ToString()))
				{
					MXFObject referencedObject = allKeys[refParent.Key.ShortKey.ToString()];
					Debug.WriteLine(string.Format("Found reference: {0} -> {1}", refParent.ToString(), referencedObject.ToString()));
					refParent.Reference = referencedObject;
				}
			}


			// Loop through all properties of this object
			// This will only use public properties. Is that enough?
			foreach (PropertyInfo propertyInfo in parent.GetType().GetProperties())
			{
				if (propertyInfo.CanRead)
				{
					if (propertyInfo.PropertyType == typeof(MXFRefKey))
					{
						// Found one!
						MXFRefKey refKey = (MXFRefKey)propertyInfo.GetValue(parent, null);
						if (refKey != null)
						{
							if (allKeys.ContainsKey(refKey.Key.ShortKey.ToString()))
							{
								MXFObject referencedObject = allKeys[refKey.Key.ShortKey.ToString()];
								Debug.WriteLine(string.Format("Found reference: {0} -> {1}", propertyInfo.Name, referencedObject.ToString()));
								refKey.Reference = referencedObject;
							}
						}
					}
				}
			}

			if (parent.Children != null)
			{
				foreach (MXFObject child in parent.Children)
				{
					ResolveReferences(allKeys, child);
				}
			}
		}


		/// <summary>
		/// Create a list of all keys in the file and their objects (recursive)
		/// </summary>
		/// <param name="refKey"></param>
		protected void CreateKeyList(Dictionary<string, MXFObject> allKeys, MXFObject parent)
		{
			// This will only use public properties. Is that enough?
			MXFMetadataBaseclass meta = parent as MXFMetadataBaseclass;
			if (meta != null)
			{
				if (meta.InstanceUID != null)
				{
					if (!allKeys.ContainsKey(meta.InstanceUID.ShortKey.ToString()))
					{
						allKeys.Add(meta.InstanceUID.ShortKey.ToString(), parent);
					}
				}
			}

			// Loop through all properties of this object
			// This will only use public properties. Is that enough?
			//int xtraCnt = 0;
			//foreach (PropertyInfo propertyInfo in parent.GetType().GetProperties())
			//{
			//	if (propertyInfo.CanRead)
			//	{
			//		if (propertyInfo.PropertyType == typeof(MXFKey))
			//		{
			//			// Found one!
			//			MXFKey key = (MXFKey)propertyInfo.GetValue(parent);
			//			if (key != null)
			//			{
			//				if (!allKeys.ContainsKey(key.ShortKey.ToString()))
			//				{
			//					allKeys.Add(key.ShortKey.ToString(), parent);
			//					xtraCnt++;
			//				}
			//			}
			//		}
			//	}
			//}


			if (parent.Children != null)
			{
				foreach (MXFObject child in parent.Children)
					CreateKeyList(allKeys, child);
			}
		}


		public int PartitionCount
		{
			get
			{
				if (this.Partitions == null)
					return 0;
				return this.Partitions.Count;
			}
		}

		public int RIPEntryCount
		{
			get
			{
				if (this.RIP == null) return 0;
				if (this.RIP.Children == null) return 0;
				return this.RIP.Children.Count;
			}
		}


		/// <summary>
		/// Return info from this MXFFile
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public string GetTrackInfo(int trackID)
		{
			try
			{
				List<MXFLogicalObject> lot = MXFLogicalObject.GetLogicChilds<MXFGenericTrack>(this.LogicalBase[typeof(MXFMaterialPackage)]);
				MXFLogicalObject track = lot.Where(a => ((MXFGenericTrack) a.Object).TrackID == trackID).FirstOrDefault();
				if (track != null)
				{
					MXFGenericTrack gtrack = track.Object as MXFGenericTrack;
					if (gtrack != null)
					{
						StringBuilder sb = new StringBuilder();
						sb.Append(string.Format("'{0}' ", gtrack.TrackName));
						MXFTimelineTrack ttrack = gtrack as MXFTimelineTrack;
						if (ttrack != null)
							sb.Append(string.Format("Rate: {0} ", ttrack.EditRate));
						MXFSequence seq = MXFLogicalObject.GetFirstChild<MXFSequence>(track);
						if (seq != null && seq.DataDefinition != null)
							sb.Append(string.Format("Type: {0} ", seq.DataDefinition.Description));
						return sb.ToString();
					}
				}
				return "";
			}
			catch(Exception)
			{
			}
			return "";
		}


		/// <summary>
		/// Count the number of tracks
		/// </summary>
		public int NumberOfTracks
		{
			get
			{
				if (this.LogicalBase == null)
					return 0;
				MXFLogicalObject lo = this.LogicalBase[typeof(MXFMaterialPackage)];
				if (lo != null && lo.Children != null)
					return lo.Children.Count();
				return 0;
			}
		}

	}
}
