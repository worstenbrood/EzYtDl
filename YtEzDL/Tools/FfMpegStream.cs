using System;
using System.Threading.Tasks;
using YtEzDL.DownLoad;

namespace YtEzDL.Tools
{
    public class FfMpegStream : StreamBase
    {
        private readonly FfMpeg _mpeg = new FfMpeg();
        private Task _writer;
        private readonly string _url;
        
        private void CreateWriterTask(TimeSpan position)
        {
            if (position == TimeSpan.Zero)
            {
                // Route yt-dlp into ffmpeg asynchronously
                _writer = YoutubeDownload.Instance.StreamAsync(_url, Process.StandardInput.BaseStream,
                    Source.Token);
            }

            // Route yt-dlp into ffmpeg asynchronously, with an offset
            _writer = YoutubeDownload.Instance.StreamAsync(_url, position, Process.StandardInput.BaseStream,
                Source.Token);
        }
        
        public FfMpegStream(string url, TimeSpan position, AudioFormat format)
        {
            // Start ffmpeg using stdin as input and stdout as output
            Process = _mpeg.CreateStreamProcess(format);
            Process.Start();

            // This is where ffmpeg's output is coming through
            BaseStream = Process.StandardOutput.BaseStream;

            _url = url;
            CreateWriterTask(position);
        }

        public FfMpegStream(string url, AudioFormat format) : this(url, TimeSpan.Zero, format)
        {
        }

        private void DisposeWriter()
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

        public void SetPosition(TimeSpan position)
        {
            DisposeWriter();
            CreateWriterTask(position);
        }
        
        public new void Dispose()
        {
            base.Dispose();
            DisposeWriter();
        }
    }
}
