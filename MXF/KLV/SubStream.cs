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
using System.IO;

namespace Myriadbits.MXF.KLV
{
    public class SubStream : Stream
    {
        private readonly Stream baseStream;
        private readonly long offset;
        private readonly long _length;
        private long _position = 0;

        public SubStream(Stream baseStream, long offset, long length)
        {
            if (baseStream == null) throw new ArgumentNullException(nameof(baseStream));
            if (!baseStream.CanRead) throw new ArgumentException("Can't read base stream.");
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (length < 1) throw new ArgumentException("Length must be greater than zero.");
            //if(baseStream.Length - offset < length) throw new ArgumentOutOfRangeException(nameof(length));

            this.baseStream = baseStream;
            this.offset = offset;
            this._length = length;

            if (baseStream.CanSeek)
            {
                baseStream.Seek(offset, SeekOrigin.Begin);
            }
            else throw new ArgumentException("Base stream must be seekable.");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            CheckDisposed();
            long remaining = _length - _position;

            if (remaining <= 0 || remaining < count)
            {
                throw new EndOfStreamException();
            }

            int read = baseStream.Read(buffer, offset, count);
            _position += read;
            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long pos = _position;

            if (origin == SeekOrigin.Begin)
                pos = offset;
            else if (origin == SeekOrigin.End)
                pos = _length + offset;
            else if (origin == SeekOrigin.Current)
                pos += offset;

            if (pos > _length)
            {
                throw new EndOfStreamException();
            }

            _position = baseStream.Seek(this.offset + pos, SeekOrigin.Begin) - this.offset;
            return pos;
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => _length;

        public override long Position { get => _position; set { _position = this.Seek(value, SeekOrigin.Begin); } }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                baseStream.Dispose();
            }
        }

        private void CheckDisposed()
        {
            if (baseStream == null) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
