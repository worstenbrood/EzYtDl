using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YtEzDL.Utils;

namespace YtEzDL.Console
{
    public delegate void StringOutput(string s);
    public delegate void CancelProcess(Process process);

    public abstract class ConsoleProcess<T> where T: ConsoleProcess<T>, new()
    {
        public const int DefaultProcessWaitTime = 250;
        public const int DefaultBufferSize = 8192;
        public string FileName { get; }
        private volatile int _processCount;
        
        public bool IsRunning => _processCount > 0;

        // Singleton
        private static readonly Lazy<T> Lazy = new Lazy<T>(() => new T());

        public static T Instance => Lazy.Value;
        
        protected ConsoleProcess(string filename)
        {
            FileName = filename;
        }

        protected Process CreateProcess(IEnumerable<string> parameters, StringOutput error = null)
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
                RedirectStandardError = error != null,
                CreateNoWindow = true,
                LoadUserProfile = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = arguments,
                WorkingDirectory = CommonTools.EzYtDlProfilePath
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

        protected Process CreateProcess(IEnumerable<string> parameters, StringOutput output, StringOutput error, CancellationToken cancellationToken)
        {
            var process = CreateProcess(parameters, error);
            if (output != null)
            {
                process.OutputDataReceived += (sender, args) =>
                {
                    if (!cancellationToken.IsCancellationRequested && !string.IsNullOrWhiteSpace(args.Data))
                    {
                        output.Invoke(args.Data);
                    }
                };
            }
            
            process.Start();
            
            if (error != null)
            {
                process.BeginErrorReadLine();
            }

            if (output != null)
            {
                process.BeginOutputReadLine();
            }

            return process;
        }

        private static Task<int> WaitAsync(Process process, StringBuilder error, StringOutput output, 
            CancellationToken cancellationToken, CancelProcess cancel, bool handleError)
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
                    if (output != null)
                    {
                        // Cancel output reading
                        process.CancelOutputRead();
                    }

                    // Invoke cancel action
                    cancel?.Invoke(process);
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

        /// <summary>
        /// Run the console app async
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="output"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="cancel"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        public async Task<int> RunAsync(IEnumerable<string> parameters, StringOutput output,
            CancellationToken cancellationToken, CancelProcess cancel = null, bool handleError = true)
        {
            var error = new StringBuilder();
            using (var process = CreateProcess(parameters, output, s => error.AppendLine(s), cancellationToken))
            {
                Interlocked.Increment(ref _processCount);
                try
                {
                    return await WaitAsync(process, error, output, cancellationToken, cancel, handleError);
                }
                finally
                {
                    Interlocked.Decrement(ref _processCount);
                }
            }
        }

        public async Task<int> RunAsync(IEnumerable<string> parameters, StringOutput output,
            CancelProcess cancel = null, bool handleError = true)
        {
            return await RunAsync(parameters, output, CancellationToken.None, cancel, handleError);
        }

        /// <summary>
        /// Write std out to a stream async
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="outputStream">Output stream</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="handleError">If true, throw exception in case of bad exit code or output on std error</param>
        /// <param name="bufferSize">Buffer size to read</param>
        /// <returns>Process exit code</returns>
        public async Task<int> StreamAsync(IEnumerable<string> parameters, Stream outputStream, CancellationToken cancellationToken, bool handleError = true, int bufferSize = DefaultBufferSize)
        {
            var error = new StringBuilder();
            using (var process = CreateProcess(parameters, e => error.AppendLine(e)))
            {
                Interlocked.Increment(ref _processCount);

                try
                {
                    // Start process
                    process.Start();

                    // Read errors
                    process.BeginErrorReadLine();

                    try
                    {
                        // Copy data to output stream
                        await process.StandardOutput.BaseStream.CopyToAsync(outputStream, bufferSize,
                            cancellationToken);
                    }
                    catch (TaskCanceledException)
                    { 
                        // Cancelled
                    }
                    catch (IOException)
                    {
                        // Pipe ends
                    }
                    
                    // Close process nicely 
                    return await WaitAsync(process, error, null, cancellationToken, null, handleError);
                }
                finally
                {
                    Interlocked.Decrement(ref _processCount);
                }
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
