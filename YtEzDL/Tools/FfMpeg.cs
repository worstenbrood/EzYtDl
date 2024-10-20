using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfMpeg : ConsoleProcess, ITool
    {
        private const string Executable = "ffmpeg.exe";
        private static readonly string Path = System.IO.Path.Combine(CommonTools.ToolsPath, Executable);
       
        public FfMpeg() : base(Path)
        {
        }

        public string GetPath()
        {
            return Path;
        }

        public string GetVersion()
        {
            return GetOutput("-version");
        }
    }
}
