using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            var process = new Process { StartInfo = processStartInfo };
            if (data != null)
            {
                process.OutputDataReceived += data;
            }
            if (error != null)
            {
                process.ErrorDataReceived += error;
            }

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            return process;
        }

        private static readonly Regex Regex = new Regex(@"\[(?<type>\w+)\].[^\d]*(?<pct>\d+.\d+)%", RegexOptions.Compiled);

        private void ParseProgress(string data, Action<double> progress)
        {
            // [download]  10.0% of 40.17MiB at  3.86MiB/s ETA 00:09.net 
            var match = Regex.Match(data);
            if (!match.Success)
                return;

            // download
            if (!match.Groups["type"].Value.Equals("download", StringComparison.OrdinalIgnoreCase))
                return;

            var pct = double.Parse(match.Groups["pct"].Value);

#if DEBUG
            Debug.WriteLine("Pct: {0}", pct);
#endif

            progress.Invoke(pct);
        }

        public void Download(string url, Action<double> progress)
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
                throw new Exception(error.ToString());
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
    }
}
