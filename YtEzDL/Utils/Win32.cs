using System;
using System.Runtime.InteropServices;

namespace YtEzDL.Utils
{
    public static class Win32
    {
        public const int MouseWheel = 0x020A;
        public const int SetFocus = 0x0007;
        public const int ClipboardUpdate = 0x031D;
       
        [DllImport("user32")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32", SetLastError = true)]
        public static extern bool AddClipboardFormatListener(IntPtr hWnd);

        [DllImport("user32", SetLastError = true)]
        public static extern bool RemoveClipboardFormatListener(IntPtr hWnd);
        
        [DllImport("user32", SetLastError = true)]
        public static extern uint GetClipboardSequenceNumber();

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32")]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, int dwThreadId);

        [DllImport("kernel32", SetLastError = true)]
        public static extern uint SuspendThread(IntPtr hThread);
        
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateToolhelp32Snapshot([In] SnapshotFlags dwFlags, int th32ProcessId);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool Process32First([In] IntPtr hSnapshot, ref ProcessEntry32 processEntry);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool Process32Next([In] IntPtr hSnapshot, ref ProcessEntry32 processEntry);

        [Flags]
        public enum ThreadAccess : int
        {
            Terminate = (0x0001),
            SuspendResume = (0x0002),
            GetContext = (0x0008),
            SetContext = (0x0010),
            SetInformation = (0x0020),
            QueryInformation = (0x0040),
            SetThreadToken = (0x0080),
            Impersonate = (0x0100),
            DirectImpersonation = (0x0200)
        }

        [Flags]
        public enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ProcessEntry32
        {
            public int Size;
            public int UsageCount;
            public int ProcessID;
            public IntPtr DefaultHeapID;
            public int ModuleID;
            public int ThreadCount;
            public int ParentProcessID;
            public int PriClassBase;
            public int Flags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string ExeFile;
        }
    }
}
