using System.Diagnostics;
using System.IO;
using YtEzDL.DownLoad;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfMpeg : ConsoleProcess<FfMpeg>, ITool
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

        /// <summary>
        /// Start ffmpeg using stdin as input and stdout as output
        /// </summary>
        /// <param name="format">AudioFormat</param>
        /// <returns></returns>
        internal Process CreateStreamProcess(AudioFormat format)
        {
            var parameters = new[]
            {
                "-i", // Input
                "pipe:0", // StdIn
                "-f", // Format
                format.ToString("G").ToLowerInvariant(),
                "pipe:1" // StdOut
            };

            return CreateProcess(parameters);
        }
    }
}
