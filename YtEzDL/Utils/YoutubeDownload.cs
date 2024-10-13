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

        private readonly ConsoleProcess _consoleProcess;

        public bool IsRunning => _consoleProcess.IsRunning;

        public YoutubeDownload()
        {
            _consoleProcess = new ConsoleProcess(YoutubeDlPath);
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

        public async Task<YoutubeDownload> DownloadAsync(string url, string directory, string filename, IProgress progress, CancellationToken cancellationToken = default)
        {
            var parameters = GetParameters();
            parameters.Add($"\"{url}\"");

            await _consoleProcess.RunAsync(parameters, s => ParseProgress(s, progress), cancellationToken, p =>
                {
                    // Do this when process Exited, otherwise files will be in use
                    p.Exited += (sender, args) =>
                    {
                        // Cleanup files
                        foreach (var file in Directory.EnumerateFiles(directory, $"{Path.GetFileNameWithoutExtension(filename)}.*"))
                        {
                            File.Delete(file);
                        }
                    };
                });
           
            return this;
        }

        public YoutubeDownload Download(string url, string directory, string filename, IProgress progress)
        {
            return DownloadAsync(url, directory, filename, progress)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
        
        public async Task GetInfoAsync(string url, Action<TrackData> action, CancellationToken cancellationToken = default)
        {
            // Parameters
            var parameters = new List<string>
            {
                "-i",
                "-j",
                $"\"{url}\""
            };
            
            await _consoleProcess.RunAsync(parameters, s =>
                {
                    try
                    {
                        var trackData = JsonConvert.DeserializeObject<TrackData>(s);
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
                }, cancellationToken, null, false);
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
      
        public async Task<int> RunAsync(Action<string> output = null, CancellationToken cancellationToken = default)
        {
            var parameters = GetParameters();
            return await _consoleProcess.RunAsync(parameters, output, cancellationToken);
        }

        public int Run(Action<string> output = null)
        {
            return RunAsync(output)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
        
        public void Update(Action<string> action)
        {
            // Parameters
            var parameters = new List<string>
            {
                "--update"
            };
            
            _consoleProcess.RunAsync(parameters, action, default, null, false)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public string GetVersion()
        {
            // Parameters
            var parameters = new List<string>
            {
                "--version"
            };

            var output = new StringBuilder();

            _consoleProcess.RunAsync(parameters, s => output.AppendLine(s), default, null, false)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return output.ToString();
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
        public double Duration { get; set; }

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
