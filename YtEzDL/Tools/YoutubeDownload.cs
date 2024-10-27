using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using YtEzDL.Config;
using YtEzDL.DownLoad;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.Tools
{
    public class YoutubeDownload : ConsoleProcess, ITool
    {
        private const string YoutubeHost = "youtube.com";
        private const string Executable = "yt-dlp.exe";

        private static string _path;
        private static string Path
        {
            get
            {
                if (_path != null)
                    return _path;

                var profilePath = System.IO.Path.Combine(CommonTools.EzYtDlProfilePath, Executable);
                if (File.Exists(profilePath))
                {
                    return _path = profilePath;
                }

                // Copy yt-dlp to profile folder for updating
                var youtubeDlPath = System.IO.Path.Combine(CommonTools.ToolsPath, Executable);
                Directory.CreateDirectory(CommonTools.EzYtDlProfilePath);
                File.Copy(youtubeDlPath, profilePath, false);

                // Use profile copy
                return _path = profilePath;
            }
        }

        public YoutubeDownload() : base(Path)
        {
        }

        private static readonly Regex PercentRegex = new Regex(@"\[(?<action>\w+)\].[^\d]*(?<pct>\d+.\d+)%", RegexOptions.Compiled);
        private static readonly Regex ActionRegex = new Regex(@"^\[(?<action>\w+)\]", RegexOptions.Compiled);

        public enum DownloadAction
        {
            Download,
            ExtractAudio,
            VideoConvertor,
            Metadata,
            ThumbnailsConvertor,
            EmbedThumbnail
        }

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
                        progress.FfMpeg(action, 0);
                        break;
                    }
            }
        }

        public async Task<YoutubeDownload> DownloadAsync(DownLoadParameters downLoadParameters, string url, string directory, string filename, IProgress progress, CancellationToken cancellationToken = default)
        {
            var parameters = downLoadParameters
                .ReplaceMetadata("title", "(.+):(\\s+)", "")
                .FfMpegLocation(CommonTools.ToolsPath)
                .Url(url)
                .GetParameters();
            
            await RunAsync(parameters, 
                s => ParseProgress(s, progress), cancellationToken, 
                p =>
                {
                    // Do this when process Exited, otherwise files will be in use
                    p.Exited += (sender, args) =>
                    {
                        // Cleanup files
                        foreach (var file in Directory.EnumerateFiles(directory, 
                                     $"{System.IO.Path.GetFileNameWithoutExtension(filename)}.*"))
                        {
                            File.Delete(file);
                        }
                    };
                });
           
            return this;
        }

        public YoutubeDownload Download(DownLoadParameters downLoadParameters, string url, string directory, string filename, IProgress progress, CancellationToken cancellationToken = default)
        {
            return DownloadAsync(downLoadParameters, url, directory, filename, progress, cancellationToken)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
        
        public async Task GetJsonAsync(string url, Action<TrackData> action, CancellationToken cancellationToken = default)
        {
            // Parameters
            var parameters = DownLoadParameters.Create
                .IgnoreErrors()
                .ReplaceMetadata("title", "(.+):(\\s+)", "")
                .FfMpegLocation(CommonTools.ToolsPath)
                .GetJson();

            // Fast fetch "hack", shows less info, loads playlists faster
            if (Configuration.Default.LayoutSettings.YoutubeFastFetch)
            {
                var uri = new Uri(url);
                if (uri.Host.IndexOf(YoutubeHost, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    parameters.FlatPlaylist();
                }
            }

            // Set url
            parameters.Url(url);
            
            // Fetch
            await RunAsync(parameters.GetParameters(), s =>
                {
                    try
                    {
                        var trackData = JsonConvert.DeserializeObject<TrackData>(s);
                        action.Invoke(trackData);
                    }
#if DEBUG
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"DeserializeObject: {ex.Message}");
                    }
#else
                    catch (Exception)
                    {
                        // Ignore
                    }
#endif
                }, cancellationToken);
        }

        public void GetJson(string url, Action<TrackData> action, CancellationToken cancellationToken = default)
        {
            GetJsonAsync(url, action, cancellationToken)
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
            return await RunAsync(parameters, output, cancellationToken);
        }

        public int Run(DownLoadParameters downLoadParameters, Action<string> output = null, CancellationToken cancellationToken = default)
        {
            return RunAsync(downLoadParameters, output, cancellationToken)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
        
        public void Update(Action<string> action)
        {
            // Parameters
            var parameters = DownLoadParameters.Create
                .Update()
                .UpdateTo(Configuration.Default.AdvancedSettings.UpdateChannel)
                .GetParameters();
            
            RunAsync(parameters, action)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public async Task StreamAsync(string url, Stream output, CancellationToken cancellationToken)
        {
            var parameters = DownLoadParameters.Create
                .Output("-")
                .Url(url)
                .GetParameters();

            await StreamAsync(parameters, output, cancellationToken);
        }

        public void Stream(string url, Stream output)
        {
            var parameters = DownLoadParameters.Create
                .Output("-")
                .Url(url)
                .GetParameters();

            Stream(parameters, output);
        }
        
        public string GetVersion()
        {
            return CommonTools.GetFileVersionInfo(Path).ProductVersion;
        }

        public string GetPath()
        {
            return Path;
        }
    }
}
