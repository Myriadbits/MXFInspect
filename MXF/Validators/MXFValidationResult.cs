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
    public enum MXFValidationSeverity
    {
        Success = 1,
        Warning = 2,
        Error = 3,
        Info = 4,
        Question = 5
    };

    public class MXFValidationDetails
    {
        public MXFValidationSeverity Severity { get; set; }
        public long Offset { get; set; } = 0;
        public string Category { get; set; }
        public string Result { get; set; }

        public MXFValidationDetails(MXFValidationSeverity severity, string result)
        {
            this.Severity = severity;
            //this.Offset = offset;
            //this.Category = category;
            this.Result = result;
        }

        public MXFValidationDetails(MXFValidationSeverity severity, long offset, string result)
        {
            this.Severity = severity;
            this.Offset = offset;
            //this.Category = category;
            this.Result = result;
        }
    }


    public class MXFValidationResult : List<MXFValidationDetails>
    {
        public string Category { get; set; }
        public long Offset { get { return Object?.Offset ?? 0; } set { } }
        public MXFValidationSeverity Severity { get; set; }
        public string Result { get; set; }
        public MXFObject Object { get; set; }

        public MXFValidationResult(string category)
        {
            this.Category = category;
        }
        public MXFValidationResult()
        {

        }

        public void SetSuccess(string result)
        {
            this.Severity = MXFValidationSeverity.Success;
            this.Result = result;
        }

        public void SetSuccess(string result, long offset)
        {
            this.Severity = MXFValidationSeverity.Success;
            this.Result = result;
            this.Offset = offset;
        }

        public void SetWarning(string result)
        {
            this.Severity = MXFValidationSeverity.Warning;
            this.Result = result;
        }

        public void SetError(string result)
        {
            this.Severity = MXFValidationSeverity.Error;
            this.Result = result;
        }

        public void SetError(string result, long offset)
        {
            this.Severity = MXFValidationSeverity.Error;
            this.Result = result;
            this.Offset = offset;
        }

        public void SetInfo(string result)
        {
            this.Severity = MXFValidationSeverity.Info;
            this.Result = result;
        }

        public void SetQuestion(string result)
        {
            this.Severity = MXFValidationSeverity.Question;
            this.Result = result;
        }

        public void AddSuccess(string result)
        {
            this.Add(new MXFValidationDetails(MXFValidationSeverity.Success, result));
        }

        public void AddWarning(string result)
        {
            this.Add(new MXFValidationDetails(MXFValidationSeverity.Warning, result));
        }

        public void AddError(string result)
        {
            this.Add(new MXFValidationDetails(MXFValidationSeverity.Error, result));
        }

        public void AddError(string result, long offset)
        {
            this.Add(new MXFValidationDetails(MXFValidationSeverity.Error, offset, result));
        }
    }
}
