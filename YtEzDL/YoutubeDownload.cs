using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace YtEzDL
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
        Worst = 9
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
        Ffmpeg
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
        
        private const string DownloadUrl = "https://yt-dl.org/downloads/latest/youtube-dl.exe";
        private const string YoutubeDlExe = "youtube-dl.exe";
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
            var processStartInfo = new ProcessStartInfo
            {
                FileName = YoutubeDlPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                LoadUserProfile = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = string.Join(" ", parameters),
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

        private void ParseProgress(string data, IProgress progress)
        {
            if (data == null)
                return;

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

        public void Download(string url, string directory, IProgress progress)
        {
            var error = new StringBuilder();
            var parameters = GetParameters();
            parameters.Add($"\"{url}\"");

            var process = CreateProcess(parameters, (o, e) => ParseProgress(e.Data, progress), (o, e) => error.Append(e.Data));

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
        }

        public List<JObject> GetInfo(string url)
        {
            var result = new List<JObject>();

            // Parameters
            var parameters = new List<string>
            {
                "-j",
                $"\"{url}\""
            };

            var process = CreateProcess(parameters, (o, e) =>
            {
                if (e.Data != null)
                {
                    result.Add(JObject.Parse(e.Data));
                }
            });

            // Wait for exit
            process.WaitForExit();

            // Error
            return process.ExitCode != 0 ? null : result;
        }

        public int Run()
        {
            var error = new StringBuilder();
            var parameters = GetParameters();
            
            var process = CreateProcess(parameters, null, (o, e) => error.Append(e.Data));

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

        private static void KillProcessTree(int parentProcessId)
        {
            var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ParentProcessId={parentProcessId}");
            var collection = searcher.Get();

            if (collection.Count <= 0)
            {
                return;
            }

            foreach (var item in collection)
            {
                var childProcessId = Convert.ToInt32(item["ProcessId"]);
                if (childProcessId == Process.GetCurrentProcess().Id)
                {
                    continue;
                }

                KillProcessTree(childProcessId);

                try
                {
                    var childProcess = Process.GetProcessById(childProcessId);
                    childProcess.Kill();
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
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

                    // Kill child process
                    KillProcessTree(_process.Id);

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

    }
}
