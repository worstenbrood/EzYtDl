using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YtEzDL.DownLoad;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public delegate void DataOutput(byte[] data, int length);

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

        private static readonly YoutubeDownload YoutubeDownload = new YoutubeDownload();

        private static void OutputReader(Process process, DataOutput output, CancellationToken cancellationToken)
        {
            var binaryReader = new BinaryReader(process.StandardOutput.BaseStream, Encoding.ASCII);
            var buffer = new byte[DefaultBufferSize];

            try
            {
                int bytesRead;
                while ((bytesRead = binaryReader.Read(buffer, 0, DefaultBufferSize)) > 0)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    output?.Invoke(buffer, bytesRead);
                }
            }
            finally
            {
                process.Dispose();
            }
        }

        /// <summary>
        /// Converts any url to selected format for streaming purposes
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="output">Output stream</param>
        /// <param name="format">AudioFormat</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task ConvertToAudio(string url, DataOutput output, AudioFormat format = AudioFormat.S16Le,
            CancellationToken cancellationToken = default)
        {
            var parameters = new[]
            {
                "-i", // Input
                "pipe:0", // StdIn
                "-f", // Format
                format.ToString("G").ToLowerInvariant(),
                "pipe:1" // StdOut
            };

            var process = CreateProcess(parameters);
            process.Start();

            // ffmpeg output reader
            var reader = Task.Run(() => OutputReader(process, output, cancellationToken), cancellationToken);

            // Redirect yt-dlp's output to ffmpeg's input (hmm)
            var writer = YoutubeDownload.StreamAsync(url, process.StandardInput.BaseStream, cancellationToken);
            
            // Wait on both tasks
            await Task.WhenAll(reader, writer);
        }

        public async Task ConvertToAudio(string url, Stream output, AudioFormat format = AudioFormat.S16Le,
            CancellationToken cancellationToken = default)
        {
            await ConvertToAudio(url, (b, c) => output.Write(b, 0, c), format, cancellationToken);
        }
    }
}
