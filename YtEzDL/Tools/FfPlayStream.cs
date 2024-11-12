using System;
using System.Diagnostics;
using System.IO;
using YtEzDL.DownLoad;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfPlayStream : Stream, IDisposable
    {
        private readonly FfPlay _ffPlay = new FfPlay();
        private Process _process;
        private readonly Stream _inputStream;
         
        public FfPlayStream(AudioFormat format)
        {
            // Start ffplay using stdin as input
            _process = _ffPlay.CreateStreamProcess(format);
            _process.Start();
            
            // This is where ffmpeg's output is coming through
            _inputStream = _process.StandardInput.BaseStream;
        }

        public new void Dispose()
        {
            base.Dispose(true);

            if (!_process.HasExited)
            {
                _process.KillProcessTree();
            }

            _process.Dispose();
            _process = null;
        }

        public override void Flush()
        {
            _inputStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _inputStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _inputStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _inputStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _inputStream.Write(buffer, offset, count);
        }

        public override bool CanRead => _inputStream.CanRead;
        public override bool CanSeek => _inputStream.CanSeek;
        public override bool CanWrite => _inputStream.CanWrite;
        public override long Length => _inputStream.Length;

        public override long Position
        {
            get => _inputStream.Position; 
            set => _inputStream.Position = value;
        }
    }
}
