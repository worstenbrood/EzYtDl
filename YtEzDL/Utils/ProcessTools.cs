using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace YtEzDL.Utils
{
    public static class ProcessTools
    {
        public static void SuspendProcess(Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var handle = Win32.OpenThread(ThreadAccess.SuspendResume, false, thread.Id);
                if (handle == IntPtr.Zero)
                {
                    continue;
                }

                try
                {
                    if (Win32.SuspendThread(handle) != 0)
                    {
                        throw new Win32Exception();
                    }
                }
#if DEBUG
                catch(Exception ex)
                {
                    Debug.WriteLine("SuspendThread: " + ex.Message);
                }
#endif
                finally
                {
                    Win32.CloseHandle(handle);
                }
            }
        }

        public static void ProcessTree(int parentProcessId, Action<Process> action)
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

                ProcessTree(childProcessId, action);

                try
                {
                    var childProcess = Process.GetProcessById(childProcessId);
                    action.Invoke(childProcess);
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }

        public static void KillYtDlp(Process process, string directory, string filename)
        {
            // Do this when process Exited, otherwise files will be in use
            process.Exited += (sender, args) =>
            {
                // Cleanup files
                foreach (var file in Directory.EnumerateFiles(directory, $"{Path.GetFileNameWithoutExtension(filename)}.*"))
                {
                    File.Delete(file);
                }
            };

            // Suspend parent
            SuspendProcess(process);

            // Suspend children
            ProcessTree(process.Id, SuspendProcess);

            // Kill child process
            ProcessTree(process.Id, p =>
            {
                if (!p.HasExited)
                {
                    p.Kill();
                }
            });

            // Kill process
            if (!process.HasExited)
            {
                process.Kill();
            }
        }
    }
}
