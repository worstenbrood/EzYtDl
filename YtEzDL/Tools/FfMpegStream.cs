using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YtEzDL.DownLoad;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfMpegStream : Stream, IDisposable
    {
        private readonly FfMpeg _mpeg = new FfMpeg();
        private Process _process;
        private readonly Stream _outputStream;
        private Task _writer;

        public FfMpegStream(string url, AudioFormat format, CancellationToken cancellationToken)
        {
            // Start ffmpeg using stdin as input and stdout as output
            _process = _mpeg.CreateStreamProcess(format);
            _process.Start();

            // This is where ffmpeg's output is coming through
            _outputStream = _process.StandardOutput.BaseStream;

            // Route yt-dlp into ffmpeg asynchronously
            _writer = YoutubeDownload.Instance.StreamAsync(url, _process.StandardInput.BaseStream, cancellationToken);
        }

        public FfMpegStream(string url, AudioFormat format) : this(url, format, CancellationToken.None)
        {
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

            _writer.Dispose();
            _writer = null;
        }

        public override void Flush()
        {
            _outputStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _outputStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _outputStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _outputStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _outputStream.Write(buffer, offset, count);
        }

        public override bool CanRead => _outputStream.CanRead;
        public override bool CanSeek => _outputStream.CanSeek;
        public override bool CanWrite => _outputStream.CanWrite;
        public override long Length => _outputStream.Length;

        public override long Position
        {
            get => _outputStream.Position;
            set => _outputStream.Position = value;
        }
    }
}
