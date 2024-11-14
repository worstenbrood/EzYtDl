using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using YtEzDL.Utils;

namespace YtEzDL.Streams
{
    public class ReadEventArgs : EventArgs
    {
        public readonly int BytesRead;

        internal ReadEventArgs(int bytesRead)
        {
            BytesRead = bytesRead;
        }
    }

    public delegate void ReadEventHandler(object o, ReadEventArgs args);

    public class WriteEventArgs : EventArgs
    {
        public readonly int BytesWritten;

        internal WriteEventArgs(int bytesWritten)
        {
            BytesWritten = bytesWritten;
        }
    }

    public delegate void WriteEventHandler(object o, WriteEventArgs args);
    
    public class ConsoleStream : Stream, IDisposable
    {
        protected Stream BaseStream { get; set; }
        protected Process Process { get; set; }

        private CancellationTokenSource _source;

        protected CancellationTokenSource Source
        {
            get
            {
                if (_source == null)
                {
                    return _source = new CancellationTokenSource();
                }

                if (!_source.IsCancellationRequested)
                {
                    return _source;
                }
                _source.Dispose();
                return _source = new CancellationTokenSource();
            }
        }

        public event ReadEventHandler ReadEvent;
        public event WriteEventHandler WriteEvent;
        
        public ConsoleStream()
        {
        }

        public ConsoleStream(Process process, Stream stream)
        {
            BaseStream = stream;
            Process = process;
        }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            BaseStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead;
            try
            {
                bytesRead = BaseStream.Read(buffer, offset, count);
            }
            catch (IOException)
            {
                bytesRead = 0;
            }
            
            // Trigger event if any
            ReadEvent?.Invoke(this, new ReadEventArgs(bytesRead));
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            BaseStream.Write(buffer, offset, count);
            // Trigger event if any
            WriteEvent?.Invoke(this, new WriteEventArgs(count));
        }

        public override bool CanRead => BaseStream.CanRead;
        public override bool CanSeek => BaseStream.CanSeek;
        public override bool CanWrite => BaseStream.CanWrite;
        public override long Length => BaseStream.Length;

        public override long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public new void Dispose()
        {
            base.Dispose();

            if (!Process.HasExited)
            {
                Process.KillProcessTree();
            }

            Process?.Dispose();
            Process = null;
        }
    }
}
