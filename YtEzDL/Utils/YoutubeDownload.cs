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
        Cbr192 = 192,
        Cbr256 = 256,
        Cbr320 = 320
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
    
    public class DownLoadParameters : Dictionary<string, string>
    {
        public static DownLoadParameters Create => new DownLoadParameters();

        public DownLoadParameters RemoveCache()
        {
            this["--rm-cache-dir"] = string.Empty;
            return this;
        }

        public DownLoadParameters ExtractAudio()
        {
            this["-x"] = string.Empty;
            return this;
        }

        public DownLoadParameters AddMetadata()
        {
            this["--add-metadata"] = string.Empty;
            return this;
        }

        public DownLoadParameters EmbedThumbnail()
        {
            this["--embed-thumbnail"] = string.Empty;
            return this;
        }

        public DownLoadParameters AudioFormat(AudioFormat format)
        {
            this["--audio-format"] = format.ToString("G").ToLowerInvariant();
            return this;
        }

        public DownLoadParameters AudioQuality(AudioQuality quality)
        {
            this["--audio-quality"] = quality.ToString("D");
            return this;
        }

        public DownLoadParameters MetadataFromTitle(string format)
        {
            this["--metadata-from-title"] = format;
            return this;
        }

        public DownLoadParameters VideoFormat(VideoFormat format)
        {
            this["--recode-video"] = format.ToString("G").ToLowerInvariant();
            return this;
        }

        public DownLoadParameters IgnoreErrors()
        {
            this["--ignore-errors"] = string.Empty;
            return this;
        }

        public DownLoadParameters SetPath(string path)
        {
            this["-P"] = $"\"{path}\"";
            return this;
        }

        public DownLoadParameters GetJson()
        {
            this["-j"] = string.Empty;
            return this;
        }

        public DownLoadParameters Update()
        {
            this["--update"] = string.Empty;
            return this;
        }

        public DownLoadParameters Version()
        {
            this["--version"] = string.Empty;
            return this;
        }

        public DownLoadParameters Reset()
        {
            Clear();
            return this;
        }

        public List<string> GetParameters()
        {
            var parameters = new List<string>();
            foreach (var parameter in this)
            {
                parameters.Add(parameter.Key);

                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    parameters.Add(parameter.Value);
                }
            }

            return parameters;
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
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

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

    public class YoutubeDownload
    {
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

        public async Task<YoutubeDownload> DownloadAsync(DownLoadParameters downLoadParameters, string url, string directory, string filename, IProgress progress, CancellationToken cancellationToken = default)
        {
            var parameters = downLoadParameters.GetParameters();
            parameters.Add($"\"{url}\"");

            await _consoleProcess.RunAsync(parameters, 
                s => ParseProgress(s, progress), cancellationToken, 
                p =>
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

        public YoutubeDownload Download(DownLoadParameters downLoadParameters, string url, string directory, string filename, IProgress progress)
        {
            return DownloadAsync(downLoadParameters, url, directory, filename, progress)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
        
        public async Task GetJsonAsync(string url, Action<TrackData> action, CancellationToken cancellationToken = default)
        {
            // Parameters
            var parameters = DownLoadParameters.Create
                .IgnoreErrors()
                .GetJson()
                .GetParameters();
            parameters.Add($"\"{url}\"");
            
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
                }, cancellationToken);
        }

        public void GetJson(string url, Action<TrackData> action)
        {
            GetJsonAsync(url, action)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public List<TrackData> GetJson(string url)
        {
            var result = new List<TrackData>();

            // Build list
            GetJson(url, j => result.Add(j));

            // Error
            return result;
        }
      
        public async Task<int> RunAsync(DownLoadParameters downLoadParameters, Action<string> output = null, CancellationToken cancellationToken = default)
        {
            var parameters = downLoadParameters.GetParameters();
            return await _consoleProcess.RunAsync(parameters, output, cancellationToken);
        }

        public int Run(DownLoadParameters downLoadParameters, Action<string> output = null)
        {
            return RunAsync(downLoadParameters, output)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
        
        public void Update(Action<string> action)
        {
            // Parameters
            var parameters = DownLoadParameters.Create
                .Update()
                .GetParameters();
            
            _consoleProcess.RunAsync(parameters, action, default)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public string GetVersion()
        {
            var output = new StringBuilder();
            
            // Parameters
            var parameters = DownLoadParameters.Create
                .Version()
                .GetParameters();
            
            _consoleProcess.RunAsync(parameters, s => output.AppendLine(s))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return output.ToString();
        }
    }
}
