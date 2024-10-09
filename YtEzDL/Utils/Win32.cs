using System;
using System.Runtime.InteropServices;

namespace YtEzDL.Utils
{
    public static class Win32
    {
        public const int MouseWheel = 0x020A;

        [DllImport("User32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}
