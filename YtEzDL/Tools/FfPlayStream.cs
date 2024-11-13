using YtEzDL.DownLoad;

namespace YtEzDL.Tools
{
    public class FfPlayStream : StreamBase
    {
        public FfPlayStream(AudioFormat format)
        {
            // Start ffplay using stdin as input
            Process = FfPlay.Instance.CreateStreamProcess(format);
            Process.Start();
            
            // This is where ffmpeg's output is coming through
            BaseStream = Process.StandardInput.BaseStream;
        }
    }
}
