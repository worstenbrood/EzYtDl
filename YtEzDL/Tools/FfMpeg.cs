using System.IO;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfMpeg : ConsoleProcess, ITool
    {
        private const string Executable = "ffmpeg.exe";
        
        public FfMpeg() : base(Path.Combine(CommonTools.ToolsPath, Executable))
        {
        }

        public string GetPath()
        {
            return FileName;
        }

        public string GetVersion()
        {
            return GetOutput("-version");
        }
    }
}
