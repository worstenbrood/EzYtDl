using System;
using System.Threading.Tasks;
using YtEzDL.DownLoad;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfMpegStream : StreamBase
    {
        private readonly FfMpeg _mpeg = new FfMpeg();
        private Task _writer;
        private readonly string _url;
        
        private Task GetWriterTask(string url, TimeSpan position)
        {
            if (position == TimeSpan.Zero)
            {
                // Route yt-dlp into ffmpeg asynchronously
                return YoutubeDownload.Instance.StreamAsync(url, Process.StandardInput.BaseStream,
                    Source.Token);
            }

            // Route yt-dlp into ffmpeg asynchronously, with an offset
            return YoutubeDownload.Instance.StreamAsync(url, position, Process.StandardInput.BaseStream,
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
            _writer = GetWriterTask(_url, position);
        }

        public FfMpegStream(string url, AudioFormat format) : this(url, TimeSpan.Zero, format)
        {
        }

        public void SetPosition(TimeSpan position)
        {
            Source.Cancel();
            TryDisposeWriter();
            _writer = GetWriterTask(_url, position);
        }
        
        private void TryDisposeWriter()
        {
            if (_writer != null &&
                (_writer.Status == TaskStatus.RanToCompletion ||
                _writer.Status == TaskStatus.Faulted ||
                _writer.Status == TaskStatus.Canceled))
            {
                _writer.Dispose();
            }
        }
        
        public new void Dispose()
        {
            base.Dispose();

            if (Process != null && !Process.HasExited)
            {
                Process.KillProcessTree();
                Process = null;
            }

            Source?.Cancel();
            TryDisposeWriter();
             _writer = null;
        }
    }
}
