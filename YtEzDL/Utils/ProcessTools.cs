using System;
using System.Diagnostics;
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
                    Win32.SuspendThread(handle);
                }
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
    }
}
