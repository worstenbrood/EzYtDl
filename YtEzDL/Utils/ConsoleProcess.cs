using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace YtEzDL.Utils
{
    public class ConsoleProcessException : Exception
    {
        public int ExitCode;
        public ConsoleProcessException(int exitCode, string msg) : base(msg)
        {
            ExitCode = exitCode;
        }

        public ConsoleProcessException(int exitCode) : this(exitCode, $"ExitCode({exitCode})")
        {
        }
        
        public ConsoleProcessException(int exitCode, string format, params object[] arg) : this(exitCode, string.Format(format, arg))
        {
            
        }
    }

    public class ConsoleProcess : IDisposable
    {
        public const int DefaultProcessWaitTime = 250;
        public readonly string FileName;
        private volatile int _processCount;
        
        public bool IsRunning => _processCount > 0;

        public ConsoleProcess(string filename)
        {
            FileName = filename;
        }

        private Process CreateProcess(IEnumerable<string> parameters, Action<string> error)
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

            return process;
        }

        private Process CreateProcess(IEnumerable<string> parameters, Action<string> data, Action<string> error, CancellationToken cancellationToken = default)
        {
            var process = CreateProcess(parameters, error);
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
                using (var process = CreateProcess(parameters, outputAction, s => error.AppendLine(s), cancellationToken))
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
                            if (outputAction != null)
                            {
                                // Cancel output reading
                                process.CancelOutputRead();
                            }

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

        [ThreadStatic] private static Process _process;
        [ThreadStatic] private static StringBuilder _error;

        public Task<Process> StartAsync(params string[] parameters)
        {
            Interlocked.Increment(ref _processCount);

            try
            {
                _error = new StringBuilder();
                _process = CreateProcess(parameters, e => _error.AppendLine(e));
                _process.Exited += (o, a) => Interlocked.Decrement(ref _processCount);
                _process.Start();
                _process.BeginErrorReadLine();
                return Task.FromResult(_process);
            }
            catch (Exception ex)
            {
                _process = null;
                _error = null;
                Interlocked.Decrement(ref _processCount);
                return Task.FromException<Process>(ex);
            }
        }

        public Process Start(params string[] parameters)
        {
            return StartAsync(parameters)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public Task<int> WaitAsync(CancellationToken cancellationToken = default)
        {
            if (_process == null)
            {
                return Task.FromException<int>(new ArgumentNullException(nameof(_process)));
            }

            try
            {
                bool exited;
                do
                {
                    // Wait for exit
                    exited = _process.WaitForExit(DefaultProcessWaitTime);

                    // Canceled
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        continue;
                    }

                    // Kill process tree
                    _process.KillProcessTree();

                    // Canceled
                    return Task.FromCanceled<int>(cancellationToken);
                } while (!exited);

                if (_process.ExitCode == 0)
                {
                    return Task.FromResult(_process.ExitCode);
                }

                var message = _error.Length > 0 ? _error.ToString() : $"ExitCode({_process.ExitCode})";
                return Task.FromException<int>(new ConsoleProcessException(_process.ExitCode, message));
            }
            catch (Exception ex)
            {
                return Task.FromException<int>(ex);
            }
            finally
            {
                _process = null;
                _error = null;
            }
        }

        public int Wait(CancellationToken cancellationToken = default)
        {
            return WaitAsync(cancellationToken)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
