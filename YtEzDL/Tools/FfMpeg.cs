﻿using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YtEzDL.Console;
using YtEzDL.DownLoad;
using YtEzDL.Interfaces;
using YtEzDL.Streams;
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
        /// <param name="args"></param>
        /// <returns></returns>
        internal Process CreateStreamProcess(AudioFormat format, params string[] args)
        {
            var parameters =
                new[]
                {
                    "-err_detect", 
                    "ignore_err",
                    "-hide_banner", 
                    "-loglevel", 
                    "error",
                    "-i", // Input
                    "pipe:0", // StdIn
                    "-f", // Format
                    format.ToString("G").ToLowerInvariant(),
                }
                .Concat(args)
                .Append("pipe:1"); // StdOut
       
            return CreateProcess(parameters);
        }

        public static async Task UrlToAudioStreamAsync(string url, Stream output, AudioFormat audioFormat,
            CancellationToken cancellationToken)
        {
            using (var ffmpeg = new FfMpegStream(url, audioFormat))
            {
                await ffmpeg.CopyToAsync(output, DefaultBufferSize, cancellationToken);
            }
        }

        public static async Task UrlToAudioStreamAsync(string url, Stream output, AudioFormat audioFormat)
        {
            await UrlToAudioStreamAsync(url, output, audioFormat, CancellationToken.None);
        }
    }
}
