using System.Diagnostics;
using System.IO;
using YtEzDL.Console;
using YtEzDL.DownLoad;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class FfPlay : ConsoleProcess<FfPlay>, ITool
    {
        private const string Executable = "ffplay.exe";

        public FfPlay() : base(Path.Combine(CommonTools.ToolsPath, Executable))
        {

        }

        /// <summary>
        /// Start ffplay using stdin as input and stdout as output
        /// </summary>
        /// <param name="format">AudioFormat</param>
        /// <returns></returns>
        internal Process CreateStreamProcess(AudioFormat format)
        {
            var parameters = new[]
            {
                //"-nodisp",
                "-showmode rdft",
                "-autoexit",
                "-i", // Input
                "pipe:0", // StdIn
                "-f", // Format
                format.ToString("G").ToLowerInvariant(),
            };

            return CreateProcess(parameters);
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
