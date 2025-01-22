using System;
using System.IO;

namespace YtEzDL.Streams
{
    public class ReadEventArgs : EventArgs
    {
        public readonly int BytesRead;
        public readonly int Offset;
        public readonly byte[] Data;

        internal ReadEventArgs(int bytesRead, byte[] data, int offset)
        {
            BytesRead = bytesRead;
            Data = data;
            Offset = offset;
        }
    }

    public delegate void ReadEventHandler(object o, ReadEventArgs args);

    public class WriteEventArgs : EventArgs
    {
        public readonly int BytesWritten;
        public readonly int Offset;
        public readonly byte[] Data;

        internal WriteEventArgs(int bytesWritten, byte[] data, int offset)
        {
            BytesWritten = bytesWritten;
            Data = data;
            Offset = offset;
        }
    }

    public delegate void WriteEventHandler(object o, WriteEventArgs args);

    public class EventStream : Stream
    {
        public override bool CanRead => BaseStream.CanRead;

        public override bool CanSeek => BaseStream.CanSeek;

        public override bool CanWrite => BaseStream.CanWrite;

        public override long Length => BaseStream.Length;

        public override long Position { get => BaseStream.Position; set => BaseStream.Position = value; }

        public event ReadEventHandler ReadEvent;
        public event WriteEventHandler WriteEvent;

        protected Stream BaseStream { get; set; }

        internal EventStream()
        {
        }

        public EventStream(Stream stream)
        {
            BaseStream = stream;
        }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var bytesRead = BaseStream.Read(buffer, offset, count);

            // Trigger event if any
            ReadEvent?.Invoke(this, new ReadEventArgs(bytesRead, buffer, offset));

            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            BaseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            BaseStream.Write(buffer, offset, count);

            // Trigger event if any
            WriteEvent?.Invoke(this, new WriteEventArgs(count, buffer, offset));
        }
    }
}
