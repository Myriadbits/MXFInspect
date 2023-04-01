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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public enum TimeStampType
    {
        TimeCode,
        DateTimeCode,
        Unknown
    }


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFTimeStamp
    {
        public TimeStampType Type { get; private set; }
        public int Year { get; private set; }
        public byte Month { get; private set; }
        public byte Day { get; private set; }
        public byte Hour { get; private set; }
        public byte Minute { get; private set; }
        public byte Second { get; private set; }
        public byte Frame { get; private set; }
        public byte Field { get; private set; }

        public byte FullFrame { get; private set; }

        // Flags
        public bool DropFrame { get; private set; }
        public bool HasFields { get; private set; }
        public bool ColorFlag { get; private set; }

        public double FrameRate { get; private set; }

        public MXFTimeStamp()
        {
        }

        public MXFTimeStamp(byte[] array, double frameRate)
        {
            if (array.Length != 17)
            {
                // TODO write message
                throw new ArgumentException();
            }

            this.FrameRate = frameRate;

            switch (array[0])
            {
                case 0x81:
                    Type = TimeStampType.TimeCode;
                    break;
                case 0x82:
                    Type = TimeStampType.DateTimeCode;
                    break;
                default:
                    Type = TimeStampType.Unknown;
                    break;
            }

            byte frameb = array[1];
            byte secondb = array[2];
            byte minuteb = array[3];
            byte hourb = array[4];

            ColorFlag = (frameb & 0x80) == 0x80;
            DropFrame = (frameb & 0x40) == 0x40;

            Frame = ParseBCD(frameb, 0x30);

            // When the rate is greater than 30 fps, use the toggle
            if (frameRate >= 59)
            {
                Field = (byte)(((secondb & 0x80) == 0x80) ? 1 : 0);
                FullFrame = (byte)((this.Frame * 2) + this.Field);
                HasFields = true;
            }
            else if (frameRate >= 49)
            {
                Field = (byte)(((hourb & 0x80) == 0x80) ? 1 : 0);
                FullFrame = (byte)((this.Frame * 2) + this.Field);
                HasFields = true;
            }
            else
            {
                FullFrame = Frame;
                HasFields = false;
            }

            Second = ParseBCD(secondb, 0x70);
            Minute = ParseBCD(minuteb, 0x70);
            Hour = ParseBCD(hourb, 0x30);

            // Read the other bytes
            Day = ParseBCD(array[5], 0x30); // Binary group data BG1 + BG2
            Month = ParseBCD(array[6], 0x10); // Binary group data BG3 + BG4
            Year = ParseBCD(array[7], 0xF0); // Binary group data BG5 + BG6
        }


        // TODO use clone() method instead
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other"></param>
        public MXFTimeStamp(MXFTimeStamp other)
        {
            this.DropFrame = other.DropFrame;
            this.HasFields = other.HasFields;
            this.FrameRate = other.FrameRate;
            this.Frame = other.Frame;
            this.Second = other.Second;
            this.Minute = other.Minute;
            this.Hour = other.Hour;
            this.Day = other.Day;
            this.Month = other.Month;
            this.Year = other.Year;
            this.FullFrame = other.FullFrame;
            this.Frame = other.Frame;
        }

        // TODO better return new timestamp
        /// <summary>
        /// Increase time step by a single timestep
        /// </summary>
        public void Increase()
        {
            this.FullFrame++;
            if (this.FullFrame >= this.FrameRate)
            {
                this.FullFrame = 0;
                this.Second++;
                if (this.Second > 59)
                {
                    this.Second = 0;
                    this.Minute++;
                    if (this.Minute > 59)
                    {
                        this.Minute = 0;
                        this.Hour++;
                        if (this.Hour > 23)
                        {
                            this.Hour = 0;
                            this.Day++;

                            if (this.Day > DateTime.DaysInMonth(Year, Month))
                            {
                                this.Day = 1;
                                this.Month++;
                                if (this.Month > 12)
                                {
                                    this.Month = 1;
                                    this.Year++;
                                }
                            }
                        }
                    }
                }
            }

            if (this.HasFields)
                this.Frame = (byte)(this.FullFrame / 2);
            else
                this.Frame = this.FullFrame;
        }


        // TODO Use IEquality<T>
        /// <summary>
        /// Compares two timestamps to check if they are the same
        /// </summary>
        /// <param name="other">The timestamp to be compared</param>
        /// <returns>True if the timestamps are the same</returns>
        public bool IsSame(MXFTimeStamp other)
        {
            if (this.Year == other.Year &&
                this.Month == other.Month &&
                this.Day == other.Day &&
                this.Hour == other.Hour &&
                this.Minute == other.Minute &&
                this.Second == other.Second &&
                this.FullFrame == other.FullFrame &&
                this.FrameRate == other.FrameRate)
            {
                return true;
            }
            return false;
        }

        // TODO find better
        /// <summary>
        /// Checks whether the timestamp is empty
        /// </summary>
        /// <returns>True if the timestamp is empty</returns>
        public bool IsEmpty()
        {
            if (this.Year == 0 &&
                this.Month == 0 &&
                this.Day == 0 &&
                this.Hour == 0 &&
                this.Minute == 0 &&
                this.Second == 0 &&
                this.FullFrame == 0 &&
                this.FrameRate == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Parse a BCD time code
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tenMask"></param>
        /// <returns>The numeric value of the timestamp component</returns>
        private byte ParseBCD(byte value, byte tenMask)
        {
            return (byte)((value & 0x0F) + ((value & tenMask) >> 4) * 10);
        }


        ///// <summary>
        ///// Parse a BCD timecode from the reader
        ///// </summary>
        ///// <param name="reader"></param>
        ///// <param name="frameRateNonDrop"></param>
        //public void ParseBCDTimeCode(IKLVStreamReader reader, double frameRate)
        //{
        //    byte frameb = reader.ReadByte();
        //    byte secondb = reader.ReadByte();
        //    byte minuteb = reader.ReadByte();
        //    byte hourb = reader.ReadByte();

        //    this.ColorFlag = (frameb & 0x80) == 0x80;
        //    this.DropFrame = (frameb & 0x40) == 0x40;

        //    this.Frame = ParseBCD(frameb, 0x30);

        //    this.FrameRate = frameRate;

        //    // When the rate is greater than 30 fps, use the toggle
        //    if (frameRate >= 59)
        //    {
        //        this.Field = (byte)(((secondb & 0x80) == 0x80) ? 1 : 0);
        //        this.FullFrame = (byte)((this.Frame * 2) + this.Field);
        //        this.HasFields = true;
        //    }
        //    else if (frameRate >= 49)
        //    {
        //        this.Field = (byte)(((hourb & 0x80) == 0x80) ? 1 : 0);
        //        this.FullFrame = (byte)((this.Frame * 2) + this.Field);
        //        this.HasFields = true;
        //    }
        //    else
        //    {
        //        this.FullFrame = this.Frame;
        //        this.HasFields = false;
        //    }

        //    this.Second = ParseBCD(secondb, 0x70);
        //    this.Minute = ParseBCD(minuteb, 0x70);
        //    this.Hour = ParseBCD(hourb, 0x30);

        //    // Read the other bytes
        //    this.Day = ParseBCD(reader.ReadByte(), 0x30); // Binary group data BG1 + BG2
        //    this.Month = ParseBCD(reader.ReadByte(), 0x10); // Binary group data BG3 + BG4
        //    this.Year = ParseBCD(reader.ReadByte(), 0xF0); // Binary group data BG5 + BG6
        //}


        /// <summary>
        /// Parse a SMPTE12M timecode
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="frameRate"></param>
        public void ParseSMPTE12M(IKLVStreamReader reader, double frameRate)
        {
            byte hoursb = reader.ReadByte();
            byte minutesb = reader.ReadByte();
            byte secondsb = reader.ReadByte();
            byte framesb = reader.ReadByte();

            this.Hour = ParseBCD(hoursb, 0x30);
            this.Minute = ParseBCD(minutesb, 0x70);
            this.Second = ParseBCD(secondsb, 0x70);
            this.Frame = ParseBCD(framesb, 0x30);

            this.FrameRate = frameRate;

            this.Field = (byte)(((secondsb & 0x80) != 0) ? 1 : 0);
            this.DropFrame = ((framesb & 0x80) != 0);
            this.HasFields = false;

            if (frameRate >= 49.0)
            {
                this.FullFrame = (byte)(this.Frame * 2 + this.Field);
                this.HasFields = true;
            }
        }

        /// <summary>
        /// Return the string representation of this timestamp
        /// </summary>
        /// <param name="fullFrame"></param>
        /// <returns></returns>
        public string GetString(bool fullFrame)
        {
            string time = $"{this.Hour:00}:{this.Minute:00}:{this.Second:00}.{this.Frame:00}";
            if (this.HasFields)
            {
                if (fullFrame)
                    time = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", this.Hour, this.Minute, this.Second, this.FullFrame);
                else
                    time = string.Format("{0:00}:{1:00}:{2:00}.{3:00}.{4}", this.Hour, this.Minute, this.Second, this.Frame, this.Field);
            }

            if (this.Day != 0)
            {
                time = $"{this.Year}-{this.Month:00}-{this.Day:00} {time}";
            }               
            return time;
        }

        public override string ToString()
        {
            return GetString(false);
        }
    }
}
