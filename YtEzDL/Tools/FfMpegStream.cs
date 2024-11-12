using System;
using System.Threading;
using System.Threading.Tasks;
using YtEzDL.DownLoad;

namespace YtEzDL.Tools
{
    public class FfMpegStream : StreamBase
    {
        private readonly FfMpeg _mpeg = new FfMpeg();
        private Task _writer;
        
        public FfMpegStream(string url, TimeSpan start, AudioFormat format, CancellationToken cancellationToken)
        {
            // Start ffmpeg using stdin as input and stdout as output
            Process = _mpeg.CreateStreamProcess(format);
            Process.Start();

            // This is where ffmpeg's output is coming through
            BaseStream = Process.StandardOutput.BaseStream;

            if (start == TimeSpan.Zero)
            {
                // Route yt-dlp into ffmpeg asynchronously
                _writer = YoutubeDownload.Instance.StreamAsync(url, Process.StandardInput.BaseStream,
                    cancellationToken);
            }
            else
            {
                // Route yt-dlp into ffmpeg asynchronously, with an offset
                _writer = YoutubeDownload.Instance.StreamAsync(url, start, Process.StandardInput.BaseStream,
                    cancellationToken);
            }
        }

        public FfMpegStream(string url, AudioFormat format, CancellationToken cancellationToken) : this(url, TimeSpan.Zero, format, cancellationToken)
        {
        }

        public FfMpegStream(string url, AudioFormat format) : this(url, format, CancellationToken.None)
        {

        }

        public FfMpegStream(string url, TimeSpan start, AudioFormat format) : this(url, start, format, CancellationToken.None)
        {
        }

        public new void Dispose()
        {
            base.Dispose();

            _writer.Dispose();
            _writer = null;
        }
    }
}
