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
        readonly Stream Vector;
        private readonly long Offset;
        private long _Length;
        private long _Position = 0;

        public SubStream(Stream vector, long offset, long length)
        {
            if (length < 1) throw new ArgumentException("Length must be greater than zero.");

            this.Vector = vector;
            this.Offset = offset;
            this._Length = length;

            vector.Seek(offset, SeekOrigin.Begin);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            CheckDisposed();
            long remaining = _Length - _Position;
            if (remaining <= 0) return 0;
            if (remaining < count) count = (int)remaining;
            int read = Vector.Read(buffer, offset, count);
            _Position += read;
            return read;
        }

        private void CheckDisposed()
        {
            if (Vector == null) throw new ObjectDisposedException(GetType().Name);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long pos = _Position;

            if (origin == SeekOrigin.Begin)
                pos = offset;
            else if (origin == SeekOrigin.End)
                pos = _Length + offset;
            else if (origin == SeekOrigin.Current)
                pos += offset;

            if (pos < 0) pos = 0;
            else if (pos >= _Length) pos = _Length - 1;

            _Position = Vector.Seek(this.Offset + pos, SeekOrigin.Begin) - this.Offset;

            return pos;
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => _Length;

        public override long Position { get => _Position; set { _Position = this.Seek(value, SeekOrigin.Begin); } }

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
                Vector.Dispose();
            }
        }
    }
}
