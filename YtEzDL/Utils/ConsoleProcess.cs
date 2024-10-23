using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace YtEzDL.Utils
{
    public class ConsoleProcessException : Exception
    {
        public int ExitCode;

        public ConsoleProcessException(int exitCode, string msg) : base(msg)
        {
            ExitCode = exitCode;
        }

        public ConsoleProcessException(int exitCode, string format, params object[] arg) : base(string.Format(format, arg))
        {
            ExitCode = exitCode;
        }
    }

    public class ConsoleProcess
    {
        public const int DefaultProcessWaitTime = 250;
        public readonly string FileName;
        private volatile int _processCount;

        public bool IsRunning => _processCount > 0;

        public ConsoleProcess(string filename)
        {
            FileName = filename;
        }

        private Process CreateProcess(IEnumerable<string> parameters, Action<string> data = null,
            Action<string> error = null, CancellationToken cancellationToken = default)
        {
            var arguments = string.Join(" ", parameters);
#if DEBUG
            Debug.WriteLine("(ConsoleProcess) Command: " + FileName + " Arguments: " + arguments);
#endif

            var processStartInfo = new ProcessStartInfo
            {
                FileName = FileName,
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

            if (error != null)
            {
                process.BeginErrorReadLine();
            }

            if (data != null)
            {
                process.BeginOutputReadLine();
            }

            return process;
        }

        public Task<int> RunAsync(IEnumerable<string> parameters, Action<string> outputAction,
            CancellationToken cancellationToken = default, Action<Process> cancelAction = null, bool handleError = true)
        {
            Interlocked.Increment(ref _processCount);

            try
            {
                var error = new StringBuilder();
                using (var process =
                       CreateProcess(parameters, outputAction, s => error.AppendLine(s), cancellationToken))
                {
                    bool exited;
                    do
                    {
                        // Wait for exit
                        exited = process.WaitForExit(DefaultProcessWaitTime);

                        // Canceled
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            continue;
                        }

                        // Exit
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
                    } while (!exited);

                    if (!handleError || process.ExitCode == 0)
                    {
                        return Task.FromResult(process.ExitCode);
                    }

                    var message = error.Length > 0 ? error.ToString() : $"ExitCode({process.ExitCode})";
                    return Task.FromException<int>(new ConsoleProcessException(process.ExitCode, message));

                }
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

        public string GetOutput(params string[] parameters)
        {
#if DEBUG
            var start = DateTime.Now;
#endif
            var output = new StringBuilder();

            try
            {
                RunAsync(parameters, s => output.AppendLine(s))
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
#if DEBUG
                Debug.WriteLine("(GetOutput) {0}: {1}ms", FileName, (DateTime.Now - start).TotalMilliseconds);
#endif
                return output.ToString().TrimEnd('\r', '\n');
            }
            catch (ConsoleProcessException e)
            {
#if DEBUG
                Debug.WriteLine("(GetOutput) {0}: {1}ms", FileName, (DateTime.Now - start).TotalMilliseconds);
#endif
                return e.Message.TrimEnd('\r', '\n');
            }
        }
    }
}
