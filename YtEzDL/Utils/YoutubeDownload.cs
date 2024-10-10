using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using YtEzDL.Interfaces;

namespace YtEzDL.Utils
{
    public enum AudioFormat
    {
        Best,
        Aac,
        Flac,
        Mp3,
        M4A,
        Opus,
        Vorbis,
        Wav
    }

    public enum AudioQuality
    {
        Best = 0,
        Medium = 1,
        Worst = 9,
        Fixed192 = 192,
        Fixed256 = 256,
        Fixed320 = 320
    }

    public enum VideoFormat
    {
        Mp4,
        Flv,
        Ogg,
        Webm,
        Mkv,
        Avi
    }

    public enum DownloadAction
    {
        Download,
        ExtractAudio
    }

    public class YoutubeDownload
    {
        public const int DefaultProcessWaitTime = 500;

        private readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();

        public YoutubeDownload RemoveCache()
        {
            _parameters["--rm-cache-dir"] = string.Empty;
            return this;
        }

        public YoutubeDownload ExtractAudio()
        {
            _parameters["-x"] = string.Empty;
            return this;
        }

        public YoutubeDownload AddMetadata()
        {
            _parameters["--add-metadata"] = string.Empty;
            return this;
        }

        public YoutubeDownload EmbedThumbnail()
        {
            _parameters["--embed-thumbnail"] = string.Empty;
            return this;
        }

        public YoutubeDownload AudioFormat(AudioFormat format)
        {
            _parameters["--audio-format"] = format.ToString("G").ToLowerInvariant();
            return this;
        }

        public YoutubeDownload AudioQuality(AudioQuality quality)
        {
            _parameters["--audio-quality"] = quality.ToString("D");
            return this;
        }

        public YoutubeDownload MetadataFromTitle(string format)
        {
            _parameters["--metadata-from-title"] = format;
            return this;
        }

        public YoutubeDownload VideoFormat(VideoFormat format)
        {
            _parameters["--recode-video"] = format.ToString("G").ToLowerInvariant();
            return this;
        }

        public YoutubeDownload IgnoreErrors()
        {
            _parameters["--ignore-errors"] = string.Empty;
            return this;
        }
        
        public YoutubeDownload Reset()
        {
            _parameters.Clear();
            return this;
        }

        private List<string> GetParameters()
        {
            var parameters = new List<string>();
            foreach (var parameter in _parameters)
            {
                parameters.Add(parameter.Key);

                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    parameters.Add(parameter.Value);
                }
            }

            return parameters;
        }
        
        private const string YoutubeDlExe = "yt-dlp.exe";
        private string _youtubeDlPath;

        private readonly object _lock = new object();
        private Process _process;

        private string YoutubeDlPath
        {
            get
            {
                if (_youtubeDlPath != null)
                    return _youtubeDlPath;

                var assembly = Assembly.GetExecutingAssembly();
                var fileInfo = new FileInfo(assembly.Location);
                return _youtubeDlPath = Path.Combine(fileInfo.DirectoryName, "Tools", YoutubeDlExe);
            }
        }

        private Process CreateProcess(IEnumerable<string> parameters, DataReceivedEventHandler data = null, DataReceivedEventHandler error = null)
        {
            var arguments = string.Join(" ", parameters);

#if DEBUG
            Debug.WriteLine("Command: " + YoutubeDlPath + " Arguments: " + arguments);
#endif

            var processStartInfo = new ProcessStartInfo
            {
                FileName = YoutubeDlPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                LoadUserProfile = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = arguments
            };

            lock (_lock)
            {
                _process = new Process { StartInfo = processStartInfo, EnableRaisingEvents = true };
                if (data != null)
                {
                    _process.OutputDataReceived += data;
                }

                if (error != null)
                {
                    _process.ErrorDataReceived += error;
                }

                _process.Start();

                _process.BeginErrorReadLine();
                _process.BeginOutputReadLine();

                return _process;
            }
        }

        private static readonly Regex PercentRegex = new Regex(@"\[(?<action>\w+)\].[^\d]*(?<pct>\d+.\d+)%", RegexOptions.Compiled);
        private static readonly Regex ActionRegex = new Regex(@"^\[(?<action>\w+)\]", RegexOptions.Compiled);

        private static void ParseProgress(string data, IProgress progress)
        {
            if (data == null)
                return;

#if DEBUG
            Debug.WriteLine("Data: " + data);
#endif

            // [download]  10.0% of 40.17MiB at  3.86MiB/s ETA 00:09.net 
            var match = ActionRegex.Match(data);
            if (!match.Success)
                return;

            // Parse
            if (!Enum.TryParse(match.Groups["action"].Value, true, out DownloadAction action))
                return;

            switch (action)
            {
                case DownloadAction.Download:
                    {
                        var m = PercentRegex.Match(data);
                        if (!m.Success)
                            break;

                        var pct = double.Parse(m.Groups["pct"].Value, CultureInfo.InvariantCulture);

#if DEBUG
                        Debug.WriteLine("Pct: {0}", pct);
#endif

                        progress.Download(pct);
                        break;
                    }

                default:
                    {
                        progress.FfMpeg(0);
                        break;
                    }
            }
        }

        public Task<YoutubeDownload> DownloadAsync(string url, string directory, IProgress progress, CancellationToken cancellationToken = default)
        {
            var error = new StringBuilder();
            var parameters = GetParameters();
            parameters.Add($"\"{url}\"");

            var process = CreateProcess(parameters, (o, e) => ParseProgress(e.Data, progress), (o, e) => error.Append(e.Data));
            var exited = false;

            do
            {
                // Wait for exit
                exited = process.WaitForExit(DefaultProcessWaitTime);

                // Cancel
                if (!cancellationToken.IsCancellationRequested)
                {
                    continue;
                }

                // Disable output reading
                process.CancelOutputRead();
                
                // Exit loop
                return Task.FromCanceled<YoutubeDownload>(cancellationToken);
            } while (!exited);

            // Error
            if (process.ExitCode != 0)
            {
                if (error.Length != 0) // This probably means we're force killed
                {
                    throw new Exception(error.ToString());
                }
            }

            return Task.FromResult(this);
        }

        public YoutubeDownload Download(string url, string directory, IProgress progress)
        {
            return DownloadAsync(url, directory, progress)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public Task GetInfoAsync(string url, Action<JObject> action, CancellationToken cancellationToken = default)
        {
            // Parameters
            var parameters = new List<string>
            {
                "-i",
                "-j",
                $"\"{url}\""
            };

            var process = CreateProcess(parameters, (o, e) =>
            {
                if (e.Data == null)
                {
                    return;
                }

                var json = JObject.Parse(e.Data);
                action.Invoke(json);
            });

            var exited = false;
            do
            {
                // Wait for exit
                exited = process.WaitForExit(DefaultProcessWaitTime);

                // Cancel
                if (!cancellationToken.IsCancellationRequested)
                {
                    continue;
                }

                // Cancel output reading
                process.CancelOutputRead();
                
                // Kill process and exit loop
                if (!process.HasExited)
                {
                    process.Kill();
                }

                return Task.FromCanceled(cancellationToken);
            } while (!exited);

            return Task.CompletedTask;
        }

        public void GetInfo(string url, Action<JObject> action)
        {
            GetInfoAsync(url, action)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public List<JObject> GetInfo(string url)
        {
            var result = new List<JObject>();

            // Build list
            GetInfo(url, j => result.Add(j));

            // Error
            return result;
        }
        
        public string GetVersion()
        {
            // Parameters
            var parameters = new List<string>
            {
                "--version"
            };

            var output = new StringBuilder();

            // Version
            var process = CreateProcess(parameters, (o, e) => output.Append(e.Data));

            // Wait for exit
            process.WaitForExit();

            // Error
            return process.ExitCode != 0 ? null : output.ToString();
        }

        public void Cancel(string directory, string filename)
        {
            lock (_lock)
            {
                if (_process != null)
                {
                    // Do this when process Exited, otherwise files will be in use
                    _process.Exited += (sender, args) =>
                    {
                        // Cleanup files
                        foreach (var file in Directory.EnumerateFiles(directory, $"{Path.GetFileNameWithoutExtension(filename)}.*"))
                        {
                            File.Delete(file);
                        }
                    };

                    // Suspend parent
                    ProcessTools.SuspendProcess(_process);

                    // Suspend children
                    ProcessTools.ProcessTree(_process.Id, ProcessTools.SuspendProcess);

                    // Kill child process
                    ProcessTools.ProcessTree(_process.Id, p =>
                    {
                        if (!p.HasExited)
                        {
                            p.Kill();
                        }
                    });

                    // Kill process
                    if (!_process.HasExited)
                    {
                        _process.Kill();
                    }

                    // Reset
                    _process = null;
                }
            }
        }

        public bool IsRunning()
        {
            lock (_lock)
            {
                return _process != null && !_process.HasExited;
            }
        }

        public void Update(Action<string> action)
        {
            // Parameters
            var parameters = new List<string>
            {
                "--update"
            };

            // Version
            var process = CreateProcess(parameters, (o, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data))
                {
                    action.Invoke(e.Data);
                };
            });

            process.StandardInput.Write(Environment.NewLine);

            // Wait for exit
            process.WaitForExit();
        }
    }
}
