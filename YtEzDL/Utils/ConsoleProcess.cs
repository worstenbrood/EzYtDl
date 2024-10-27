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

    public class ConsoleProcess
    {
        public const int DefaultProcessWaitTime = 250;
        public const int DefaultBufferSize = 8192;
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
            process.Exited += (o, a) => Interlocked.Decrement(ref _processCount);
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

        /// <summary>
        /// Run the console app async
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="outputAction"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="cancelAction"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        public Task<int> RunAsync(IEnumerable<string> parameters, Action<string> outputAction,
            CancellationToken cancellationToken = default, Action<Process> cancelAction = null, bool handleError = true)
        {
            try
            {
                var error = new StringBuilder();
                using (var process = CreateProcess(parameters, outputAction, s => error.AppendLine(s), cancellationToken))
                {
                    Interlocked.Increment(ref _processCount);
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
        }

        private static Task<int> WaitAsync(Process process, StringBuilder error, CancellationToken cancellationToken, bool handleError)
        {
            try
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
            catch (Exception ex)
            {
                return Task.FromException<int>(ex);
            }
        }

        /// <summary>
        /// Write std out to a stream async
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="outputStream">Stream to write to</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="handleError">If true, throw exception in case of bad exit code or output on std error</param>
        /// <param name="bufferSize">Buffer size to read</param>
        /// <returns>Process exit code</returns>
        public Task<int> StreamAsync(IEnumerable<string> parameters, Stream outputStream, CancellationToken cancellationToken = default, bool handleError = true, int bufferSize = DefaultBufferSize)
        {
            try
            {
                var error = new StringBuilder();
                using (var process = CreateProcess(parameters, e => error.AppendLine(e)))
                {
                    process.Start();
                    Interlocked.Increment(ref _processCount);
                    process.BeginErrorReadLine();
                    
                    int bytesRead;
                    var binaryReader = new BinaryReader(process.StandardOutput.BaseStream, Encoding.UTF8);
                    var buffer = new byte[bufferSize];

                    while ((bytesRead = binaryReader.Read(buffer, 0, bufferSize)) > 0)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            // Canceled
                            break;
                        }

                        // Write to stream
                        outputStream.Write(buffer, 0, bytesRead);
                    }

                    return WaitAsync(process, error, cancellationToken, handleError);
                }
            }
            catch (Exception ex)
            {
                return Task.FromException<int>(ex);
            }
        }

        /// <summary>
        /// Write std out to a stream
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="outputStream">Stream to write to</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="handleError">If true, throw exception in case of bad exit code or output on std error</param>
        /// <param name="bufferSize">Buffer size to read</param>
        /// <returns>Process exit code</returns>
        public int Stream(IEnumerable<string> parameters, Stream outputStream,
            CancellationToken cancellationToken = default, bool handleError = true, int bufferSize = DefaultBufferSize)
        {
            return StreamAsync(parameters, outputStream, cancellationToken, handleError, bufferSize)
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
    }
}
