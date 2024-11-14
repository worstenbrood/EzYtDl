using System;
using YtEzDL.Tools;

namespace YtEzDL.Streams
{
    public class YtDlStream : ConsoleStream
    {
        public YtDlStream(string url, TimeSpan position)
        {
            Process = YoutubeDownload.Instance.CreateStreamProcess(url, position);
            Process.Start();
            BaseStream = Process.StandardOutput.BaseStream;
        }
    }
}
