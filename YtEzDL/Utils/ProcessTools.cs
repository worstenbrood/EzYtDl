using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace YtEzDL.Utils
{
    public static class ProcessTools
    {
        public static void SuspendProcess(this Process process)
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
                entry.Size = Marshal.SizeOf(entry);

                if (!Win32.Process32First(handle, ref entry))
                {
                    throw new Win32Exception();
                }

                do
                {
                    // Next
                    if (entry.ParentProcessID != parentProcessId)
                    {
                        continue;
                    }
                    
                    try
                    {
                        var childProcess = Process.GetProcessById(entry.ProcessID);
                        action.Invoke(childProcess);
                    }
                    catch (Exception)
                    {
                        // Ignore
                    }

                    // Process children of this process
                    ProcessTree(entry.ProcessID, action);

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
        public static void KillProcessTree(this Process process)
        {
            // Suspend parent
            process.SuspendProcess();

            // Suspend children
            ProcessTree(process.Id, SuspendProcess);

            // Kill child process
            ProcessTree(process.Id, p =>
            {
                if (p.HasExited)
                {
                    return;
                }

                p.Kill();
                p.WaitForExit();
            });
            
            // Kill process
            if (process.HasExited)
            {
                return;
            }

            process.Kill();
            process.WaitForExit();
        }

        public static List<IntPtr> GetProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();
            var result = Win32.EnumWindows((hWnd, param) =>
            {
                var windowProcessId = 0;
                Win32.GetWindowThreadProcessId(hWnd, ref windowProcessId);
                if (windowProcessId != processId)
                {
                    return true;
                }
#if DEBUG
                Debug.WriteLine("Handle: " + hWnd);
#endif
                handles.Add(hWnd);

                // Children
                Win32.EnumChildWindows(hWnd, (chWnd, cParam) =>
                {
#if DEBUG
                    Debug.WriteLine("Handle: " + chWnd);
#endif
                    handles.Add(chWnd);
                    return true;
                }, param);

                return true;
            }, IntPtr.Zero);

            if (!result)
            {
                throw new Win32Exception();
            }

            return handles;
        }
    }
}
