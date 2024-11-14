using YtEzDL.DownLoad;
using YtEzDL.Tools;
using YtEzDL.Utils;

namespace YtEzDL.Streams
{
    public class FfPlayStream : ConsoleStream
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
