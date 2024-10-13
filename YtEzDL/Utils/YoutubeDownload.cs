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
using Newtonsoft.Json;
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
        public const int DefaultProcessWaitTime = 250;
        
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

        public YoutubeDownload SetPath(string path)
        {
            _parameters["-P"] = path;
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
            //Debug.WriteLine("Data: " + data);
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
                       // Debug.WriteLine("Pct: {0}", pct);
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

            var process = CreateProcess(parameters, (o, e) =>
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    ParseProgress(e.Data, progress);
                }
            }, (o, e) => error.Append(e.Data));
            bool exited;

            do
            {
                // Canceled
                if (cancellationToken.IsCancellationRequested)
                {
                    if (!process.HasExited)
                    {
                        // Disable output reading
                        process.CancelOutputRead();
                    }

                    // Do not kill since we need Cleanup

                    // Exit loop
                    return Task.FromCanceled<YoutubeDownload>(cancellationToken);
                }

                // Wait for exit
                exited = process.WaitForExit(DefaultProcessWaitTime);
            } while (!exited);

            // Error
            if (process.ExitCode != 0)
            {
                if (error.Length != 0) // This probably means we're force killed
                {
                    return Task.FromException<YoutubeDownload>(new Exception(error.ToString()));
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
        
        public Task GetInfoAsync(string url, Action<TrackData> action, CancellationToken cancellationToken = default)
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
                if (cancellationToken.IsCancellationRequested || e.Data == null)
                {
                    return;
                }

                try
                {
                    var trackData = JsonConvert.DeserializeObject<TrackData>(e.Data);
                    action.Invoke(trackData);
                }
#if DEBUG
                catch (Exception ex)
#else
                catch (Exception)
#endif
                {
#if DEBUG
                    Debug.WriteLine($"DeserializeObject: {ex.Message}");
#else
                    // Ignore
#endif
                }
            });

            bool exited;
            do
            {
                // Canceled
                if (cancellationToken.IsCancellationRequested)
                {
                    if (!process.HasExited)
                    {
                        // Cancel output reading
                        process.CancelOutputRead();
                    }

                    // Kill process tree
                    process.KillProcessTree();

                    // Canceled
                    return Task.FromCanceled(cancellationToken);
                }

                // Wait for exit
                exited = process.WaitForExit(DefaultProcessWaitTime);
            } while (!exited);

            return Task.CompletedTask;
        }

        public void GetInfo(string url, Action<TrackData> action)
        {
            GetInfoAsync(url, action)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public List<TrackData> GetInfo(string url)
        {
            var result = new List<TrackData>();

            // Build list
            GetInfo(url, j => result.Add(j));

            // Error
            return result;
        }
        
        public void Cancel(string directory, string filename)
        {
            lock (_lock)
            {
                if (_process != null)
                {
                    _process.KillYtDlp(directory, filename);

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

        public int Run(Action<string> output = null)
        {
            var error = new StringBuilder();
            var parameters = GetParameters();
            var receiver = output == null ? null : new DataReceivedEventHandler((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(args.Data))
                {
                    output.Invoke(args.Data);
                }
            });
            var process = CreateProcess(parameters, receiver, (o, e) => error.Append(e.Data));

            // Wait for exit
            process.WaitForExit();

            // Error
            if (process.ExitCode != 0)
            {
                if (error.Length != 0) // This probably means we're force killed
                {
                    throw new Exception(error.ToString());
                }
            }

            return process.ExitCode;
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

    }

    public class Thumbnail
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "preference")]
        public int Preference { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }
    }

    public class TrackData
    {
        [JsonProperty(PropertyName = "webpage_url")]
        public string WebpageUrl { get; set; }

        [JsonProperty(PropertyName = "webpage_url_domain")]
        public string WebpageUrlDomain { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "playlist")]
        public string Playlist { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; set; }

        [JsonProperty(PropertyName = "upload_date")]
        public string UploadDate { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty(PropertyName = "thumbnails")]
        public Thumbnail[] Thumbnails { get; set; }
    }
}
