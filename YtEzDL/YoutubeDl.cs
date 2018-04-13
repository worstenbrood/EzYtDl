using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace YtEzDL
{
    public class YoutubeDl
    {
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
                _process = new Process {StartInfo = processStartInfo, EnableRaisingEvents = true};
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

        private static readonly Regex Regex = new Regex(@"\[(?<type>\w+)\].[^\d]*(?<pct>\d+.\d+)%", RegexOptions.Compiled);

        private void ParseProgress(string data, Action<double> progress)
        {
            if (data == null)
                return;

            // [download]  10.0% of 40.17MiB at  3.86MiB/s ETA 00:09.net 
            var match = Regex.Match(data);
            if (!match.Success)
                return;

            // download
            if (!match.Groups["type"].Value.Equals("download", StringComparison.OrdinalIgnoreCase))
                return;

            var pct = double.Parse(match.Groups["pct"].Value, CultureInfo.InvariantCulture);

#if DEBUG
            Debug.WriteLine("Pct: {0}", pct);
#endif
            progress.Invoke(pct);
        }

        public void Download(string url, string directory, Action<double> progress)
        {
            var error = new StringBuilder();
            
            // Parameters
            var parameters = new List<string>
            {
                "-x",
                "--add-metadata",
                "--embed-thumbnail",
                "--audio-format",
                "mp3",
                "--audio-quality",
                "0",
                $"\"{url}\""
            };
            
            var process = CreateProcess(parameters, (o, e) => ParseProgress(e.Data, progress), (o, e) => error.Append(e.Data));

            // Wait for exit
            process.WaitForExit();

            // Error
            if (process.ExitCode != 0)
            {
                if (error.Length != 0)
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

                var childProcess = Process.GetProcessById(childProcessId);
                childProcess.Kill();
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
                        foreach (var file in Directory.EnumerateFiles(directory, Path.GetFileNameWithoutExtension(filename) + ".*"))
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
    }
}
