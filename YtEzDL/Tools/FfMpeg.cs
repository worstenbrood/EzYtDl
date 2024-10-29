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
    public delegate bool DataReader(Stream reader);

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

        private static void OutputReader(Process process, DataReader reader, CancellationToken cancellationToken)
        {
            try
            {
                do
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                } while (reader(process.StandardOutput.BaseStream));
            }
            finally
            {
                // Cleanup
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
            var reader = Task.Run(() => OutputReader(process, output, cancellationToken), cancellationToken);
                
            // Redirect yt-dlp's output to ffmpeg's input (hmm)
            var writer = YoutubeDownload.Instance.StreamAsync(url, process.StandardInput.BaseStream, cancellationToken);
            
            // Wait on both tasks
            await Task.WhenAll(reader, writer);
        }

        private static bool Redirect(Stream reader, byte[] buffer, int length, Stream output)
        {
            try
            {
                var bytesRead = reader.Read(buffer, 0, length);
                if (bytesRead > 0)
                {
                    output.Write(buffer, 0, bytesRead);
                }
                return bytesRead > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task ConvertToAudio(string url, Stream output, AudioFormat format = AudioFormat.S16Le,
            CancellationToken cancellationToken = default)
        {
            var buffer = new byte[DefaultBufferSize];
            await ConvertToAudio(url, s => Redirect(s, buffer, DefaultBufferSize, output), format, cancellationToken);
        }
    }
}
