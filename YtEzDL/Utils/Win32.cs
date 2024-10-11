using System;
using System.Runtime.InteropServices;

namespace YtEzDL.Utils
{
    public static class Win32
    {
        public const int MouseWheel = 0x020A;
        public const int SetFocus = 0x0007;
        public const int ClipboardUpdate = 0x031D;

        [DllImport("User32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool AddClipboardFormatListener(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetClipboardSequenceNumber();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, int dwThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint ResumeThread(IntPtr hThread);
        
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateToolhelp32Snapshot([In] SnapshotFlags dwFlags, int th32ProcessId);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool Process32First([In] IntPtr hSnapshot, ref ProcessEntry32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool Process32Next([In] IntPtr hSnapshot, ref ProcessEntry32 lppe);

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

        //inner struct used only internally
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ProcessEntry32
        {
            public int dwSize;
            public int cntUsage;
            public int th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public int th32ModuleID;
            public int cntThreads;
            public int th32ParentProcessID;
            public int pcPriClassBase;
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }
    }
}
