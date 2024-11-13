using System;

namespace YtEzDL.Tools
{
    public class YtDlStream : StreamBase
    {
        public YtDlStream(string url, TimeSpan position)
        {
            Process = YoutubeDownload.Instance.CreateStreamProcess(url, position);
            Process.Start();
            BaseStream = Process.StandardOutput.BaseStream;
        }
    }
}
