using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace YtEzDL.Utils
{
    public static class ProcessTools
    {
        public static void SuspendProcess(Process process)
        {
            // Do nothing
            if (process.HasExited)
            {
                return;
            }

            foreach (ProcessThread thread in process.Threads)
            {
                var handle = Win32.OpenThread(Win32.ThreadAccess.SuspendResume, false, thread.Id);
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
                catch (Exception ex)
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
            var handle = Win32.CreateToolhelp32Snapshot(Win32.SnapshotFlags.Process | Win32.SnapshotFlags.NoHeaps, parentProcessId);
            if (handle == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            try
            {
                var entry = new Win32.ProcessEntry32();
                entry.dwSize = Marshal.SizeOf(entry);

                if (!Win32.Process32First(handle, ref entry))
                {
                    throw new Win32Exception();
                }

                do
                {
                    // Next
                    if (entry.th32ParentProcessID != parentProcessId)
                    {
                        continue;
                    }

                    // Process children of this process
                    ProcessTree(entry.th32ProcessID, action);

                    try
                    {
                        var childProcess = Process.GetProcessById(entry.th32ProcessID);
                        action.Invoke(childProcess);
                    }
                    catch (Exception)
                    {
                        // Ignore
                    }

                } while (Win32.Process32Next(handle, ref entry));
            }
            finally
            {
                Win32.CloseHandle(handle);
            }
        }

        /// <summary>
        /// Kill the whole process tree
        /// </summary>
        /// <param name="process"></param>
        public static void KillProcessTree(Process process)
        {
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
                    p.WaitForExit();
                }
            });
            
            // Kill process
            if (!process.HasExited)
            {
                process.Kill();
                process.WaitForExit();
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

            KillProcessTree(process);
        }
    }
}
