using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YtEzDL.DownLoad;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public delegate bool DataReader(BinaryReader reader);

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

        private static void OutputReader(Process process, DataReader reader, CancellationToken cancellationToken)
        {
            var binaryReader = new BinaryReader(process.StandardOutput.BaseStream);
            
            try
            {
                while (reader(binaryReader))
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
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
        public async Task ConvertToAudio(string url, DataReader output, AudioFormat format = AudioFormat.S16Le,
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
            var reader = new Task(() => OutputReader(process, output, cancellationToken));
            reader.Start();

            // Redirect yt-dlp's output to ffmpeg's input (hmm)
            var writer = YoutubeDownload.StreamAsync(url, process.StandardInput.BaseStream, cancellationToken);
            
            // Wait on both tasks
            await Task.WhenAll(reader, writer);
        }

        private static bool Redirect(BinaryReader reader, byte[] buffer, int length, Stream output)
        {
            var bytesRead = reader.Read(buffer, 0, length);
            if (bytesRead > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
            return bytesRead != 0;
        }

        public async Task ConvertToAudio(string url, Stream output, AudioFormat format = AudioFormat.S16Le,
            CancellationToken cancellationToken = default)
        {
            var buffer = new byte[DefaultBufferSize];
            await ConvertToAudio(url, s => Redirect(s, buffer, DefaultBufferSize, output), format, cancellationToken);
        }
    }
}
