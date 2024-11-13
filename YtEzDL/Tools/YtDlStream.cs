using System;
using YtEzDL.Utils;

namespace YtEzDL.Tools
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
