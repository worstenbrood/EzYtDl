using System;
using System.Threading.Tasks;
using YtEzDL.DownLoad;
using YtEzDL.Tools;
using YtEzDL.Utils;

namespace YtEzDL.Streams
{
    public class FfMpegStream : ConsoleStream
    {
        private readonly FfMpeg _mpeg = new FfMpeg();
        private Task _writer;

        public void CreateWriter(string url, TimeSpan position)
        {
            if (position == TimeSpan.Zero)
            {
                // Route yt-dlp into ffmpeg asynchronously
                _writer = YoutubeDownload.Instance.StreamAsync(url, Process.StandardInput.BaseStream,
                    Source.Token);
            }
            else
            {
                // Route yt-dlp into ffmpeg asynchronously, with an offset
                _writer = YoutubeDownload.Instance.StreamAsync(url, position, Process.StandardInput.BaseStream,
                    Source.Token);
            }
        }

        public FfMpegStream(string url, TimeSpan position, AudioFormat format, params string[] args)
        {
            // Start ffmpeg using stdin as input and stdout as output
            Process = _mpeg.CreateStreamProcess(format, args);
            Process.Start();

            // This is where ffmpeg's output is coming through
            BaseStream = Process.StandardOutput.BaseStream;

            // Create task
            CreateWriter(url, position);
        }

        public FfMpegStream(string url, AudioFormat format) : this(url, TimeSpan.Zero, format)
        {
        }

        public void DisposeWriter()
        {
            Source.Cancel();
           
            if (_writer != null &&
                (_writer.Status == TaskStatus.RanToCompletion ||
                 _writer.Status == TaskStatus.Faulted ||
                 _writer.Status == TaskStatus.Canceled))
            {
                _writer.Dispose();
            }

            _writer = null;
        }
        
        public new void Dispose()
        {
            DisposeWriter();
            base.Dispose();
        }
    }
}
