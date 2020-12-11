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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
	// TODO should this class inherit from MXFObject?
	public class MXFValidator : MXFObject
	{
		public MXFFile File { get; set; }
		public BackgroundWorker Worker { get; set; }
		public List<MXFValidationResult> Results { get; set; }
		public string Task { get; set; }

		/// <summary>
		/// Initialize this test
		/// </summary>
		/// <param name="name"></param>
		/// <param name="file"></param>
		/// <param name="worker"></param>
		public void Initialize(MXFFile file, BackgroundWorker worker)
		{
			this.File = file;
			this.Worker = worker;
		}

		/// <summary>
		/// Change the progress
		/// </summary>
		/// <param name="percentage"></param>
		protected void ReportProgress(int percentage)
		{
			if (this.Worker != null)
				this.Worker.ReportProgress(percentage, this.Task);
		}

		/// <summary>
		/// Execute the test
		/// </summary>
		/// <param name="results"></param>
		public void ExecuteTest(ref List<MXFValidationResult> results)
		{
			Stopwatch sw = Stopwatch.StartNew();
			this.Worker.ReportProgress(0, string.Format("Test {0} started", this.Task));
			OnExecuteTest(ref results);
			this.Worker.ReportProgress(100, string.Format("Test {0} is completed", this.Task));
			Debug.WriteLine("Validation test run in {0} ms", sw.ElapsedMilliseconds);
		}


		/// <summary>
		/// Override in derived classes
		/// </summary>
		public virtual void OnExecuteTest(ref List<MXFValidationResult> results)
		{
		}
	}
}
