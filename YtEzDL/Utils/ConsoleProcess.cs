using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YtEzDL.Utils
{
    public class ConsoleProcess
    {
        public const int DefaultProcessWaitTime = 250;
        private readonly string _fileName;
        private volatile int _processCount;

        public bool IsRunning => _processCount > 0;

        public ConsoleProcess(string filename)
        {
            _fileName = filename;
        }

        private Process CreateProcess(IEnumerable<string> parameters, Action<string> data = null, Action<string> error = null, CancellationToken cancellationToken = default)
        {
            var arguments = string.Join(" ", parameters);
#if DEBUG
            Debug.WriteLine("Command: " + _fileName + " Arguments: " + arguments);
#endif

            var processStartInfo = new ProcessStartInfo
            {
                FileName = _fileName,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                LoadUserProfile = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = arguments
            };

            var process = new Process { StartInfo = processStartInfo, EnableRaisingEvents = true };
            if (data != null)
            {
                process.OutputDataReceived += (sender, args) =>
                {
                    if (!cancellationToken.IsCancellationRequested && !string.IsNullOrWhiteSpace(args.Data))
                    {
                        data.Invoke(args.Data);
                    }
                };
            }

            if (error != null)
            {
                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrWhiteSpace(args.Data))
                    {
                        error.Invoke(args.Data);
                    }
                };
            }

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            return process;
        }

        public Task<int> RunAsync(List<string> parameters, Action<string> outputAction, CancellationToken cancellationToken = default, Action<Process> cancelAction = null, bool handleError = true)
        {
            Interlocked.Increment(ref _processCount);

            try
            {
                var error = new StringBuilder();
                var process = CreateProcess(parameters, outputAction, s => error.AppendLine(s), cancellationToken);
                bool exited;
                do
                {
                    // Wait for exit
                    exited = process.WaitForExit(DefaultProcessWaitTime);

                    // Canceled
                    if (cancellationToken.IsCancellationRequested)
                    {
                        if (!process.HasExited)
                        {
                            // Cancel output reading
                            process.CancelOutputRead();

                            // Invoke cancel action
                            cancelAction?.Invoke(process);
                        }

                        // Kill process tree
                        process.KillProcessTree();

                        // Canceled
                        return Task.FromCanceled<int>(cancellationToken);
                    }
                } while (!exited);

                if (handleError && process.ExitCode != 0 && error.Length > 0)
                {
                    return Task.FromException<int>(new Exception(error.ToString()));
                }

                return Task.FromResult(process.ExitCode);
            }
            catch (Exception ex)
            {
                return Task.FromException<int>(ex);
            }
            finally
            {
                Interlocked.Decrement(ref _processCount);
            }
        }
    }
}
