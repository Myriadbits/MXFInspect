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

namespace Myriadbits.MXF
{
	public enum MXFValidationState
	{
		Success = 1,
		Warning = 2,
		Error = 3,
		Info = 4,
		Question = 5
	};

	public class MXFValidationDetails
	{
		public MXFValidationState State { get; set; }
		public string Result { get; set; }
		public string Category { get; set; }

		public MXFValidationDetails(MXFValidationState state, string result)
		{
			this.State = state;
			this.Result = result;
		}
	}


	
	public class MXFValidationResult : List<MXFValidationDetails>
	{
		public string Category { get; set; }
		public MXFValidationState State { get; set; }
		public string Result { get; set; }

		public MXFValidationResult(string category)
		{
			this.Category = category;
		}

		public void SetSuccess(string result)
		{
			this.State = MXFValidationState.Success;
			this.Result = result;
		}

		public void SetWarning(string result)
		{
			this.State = MXFValidationState.Warning;
			this.Result = result;
		}

		public void SetError(string result)
		{
			this.State = MXFValidationState.Error;
			this.Result = result;
		}

		public void SetInfo(string result)
		{
			this.State = MXFValidationState.Info;
			this.Result = result;
		}

		public void SetQuestion(string result)
		{
			this.State = MXFValidationState.Question;
			this.Result = result;
		}

		public void AddSuccess(string result)
		{
			this.Add(new MXFValidationDetails(MXFValidationState.Success, result));
		}

		public void AddWarning(string result)
		{
			this.Add(new MXFValidationDetails(MXFValidationState.Warning, result));
		}
	
		public void AddError(string result)
		{
			this.Add(new MXFValidationDetails(MXFValidationState.Error, result));
		}

	}
}
