using System;
using YtEzDL.DownLoad;

namespace YtEzDL.Tools
{
    public class FfPlayStream : StreamBase, IDisposable
    {
        private readonly FfPlay _ffPlay = new FfPlay();

        public FfPlayStream(AudioFormat format)
        {
            // Start ffplay using stdin as input
            Process = _ffPlay.CreateStreamProcess(format);
            Process.Start();
            
            // This is where ffmpeg's output is coming through
            BaseStream = Process.StandardInput.BaseStream;
        }
    }
}
