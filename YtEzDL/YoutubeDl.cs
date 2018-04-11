using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

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

        private void ParseProgress(object sender, DataReceivedEventArgs args)
        {
            // [download]  10.0% of 40.17MiB at  3.86MiB/s ETA 00:09.net 
        }

        public void Download(string url)
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
            
            var process = CreateProcess(parameters, ParseProgress, (o, e) => error.Append(e.Data));

            // Wait for exit
            process.WaitForExit();

            // Error
            if (process.ExitCode != 0)
            {
                throw new Exception(error.ToString());
            }
        }

        public string GetInfo(string url)
        {
            var output = new StringBuilder();

            // Parameters
            var parameters = new List<string>
            {
                "-j",
                $"\"{url}\""
            };

            var process = CreateProcess(parameters, (o, e) => output.Append(e.Data));

            // Wait for exit
            process.WaitForExit();

            // Error
            return process.ExitCode != 0 ? null : output.ToString();
        }
    }
}
