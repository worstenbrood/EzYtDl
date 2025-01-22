using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using YtEzDL.Utils;

namespace YtEzDL.Streams
{
    public class ConsoleStream : EventStream, IDisposable
    {
        protected Process Process { get; set; }

        private CancellationTokenSource _source = new CancellationTokenSource();

        protected CancellationTokenSource Source
        {
            get
            {
                lock (this)
                {
                    if (!_source.IsCancellationRequested)
                    {
                        return _source;
                    }

                    _source.Dispose();
                    return _source = new CancellationTokenSource();
                }
            }
        }

        public ConsoleStream()
        {
        }
        
        public ConsoleStream(Process process, Stream stream) : base(stream) 
        {
            BaseStream = stream;
            Process = process;
        }

        private void CheckForCancelled(Action action)
        {
            Source.Token.ThrowIfCancellationRequested();
            action.Invoke();
        }

        private T CheckForCancelled<T>(Func<T> action)
        {
            Source.Token.ThrowIfCancellationRequested();
            return action.Invoke();
        }

        public override void Flush()
        {
            CheckForCancelled(() => base.Flush());
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return CheckForCancelled(() => base.Seek(offset, origin));
        }

        public override void SetLength(long value)
        {
            CheckForCancelled(() => base.SetLength(value));
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return CheckForCancelled(() =>
            {
                int bytesRead;
                try
                {
                    bytesRead = base.Read(buffer, offset, count);
                }
                catch (IOException)
                {
                    bytesRead = 0;
                }

                return bytesRead;
            });
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            CheckForCancelled(() =>
            {
                base.Write(buffer, offset, count);
            });
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
