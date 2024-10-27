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

        private static readonly YoutubeDownload _youtubeDownload = new YoutubeDownload();

        private static void OutputReader(Stream stream, Action<byte[], int> output, CancellationToken cancellationToken)
        {
            int bytesRead;
            var binaryReader = new BinaryReader(stream, Encoding.ASCII);
            var buffer = new byte[DefaultBufferSize];

            while ((bytesRead = binaryReader.Read(buffer, 0, DefaultBufferSize)) > 0)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                output?.Invoke(buffer, bytesRead);
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
        public async Task ConvertToAudio(string url, Action<byte[], int> output, AudioFormat format = AudioFormat.Wav,
            CancellationToken cancellationToken = default)
        {
            var error = new StringBuilder();
            var parameters = new[]
            {
                "-i", // Input
                "pipe:0", // StdIn
                "-f", // Format
                format.ToString("G").ToLowerInvariant(),
                "pipe:1" // StdOut
            };

            using (var process = CreateProcess(parameters, null))
            {
                process.Start();

                // ffmpeg output reader
                _ = Task.Run(() => OutputReader(process.StandardOutput.BaseStream, output, cancellationToken));

                // Redirect yt-dlp's output to ffmpeg's input (hmm)
                await _youtubeDownload.StreamAsync(url, process.StandardInput.BaseStream, cancellationToken);
            }
        }

        public async Task ConvertToAudio(string url, Stream output, AudioFormat format = AudioFormat.Wav,
            CancellationToken cancellationToken = default)
        {
            await ConvertToAudio(url, (b, c) => output.Write(b, 0, c), format, cancellationToken);
        }
    }
}
