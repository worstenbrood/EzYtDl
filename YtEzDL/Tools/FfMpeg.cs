using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfMpeg : ConsoleProcess, ITool
    {
        private const string Executable = "ffmpeg.exe";

        private static string _path;

        public static string Path
        {
            get
            {
                if (_path != null)
                    return _path;

                return _path = System.IO.Path.Combine(CommonTools.ToolsPath, Executable);
            }
        }

        public FfMpeg() : base(Path)
        {
        }

        public string GetVersion()
        {
            return GetOutput("-version");
        }
    }
}
