using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YtEzDL.Utils
{
    public class ClipboardMonitor : Form
    {
        public static IntPtr HwndMessage = new IntPtr(-3);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        public enum Messages
        {
            WmClipboardupdate = 0x031D
        }

        // Event
        public delegate void ClipboardDataChanged(IDataObject dataObject);
        public event ClipboardDataChanged OnClipboardDataChanged;

        public void Monitor()
        {
            //Turn the child window into a message-only window (refer to Microsoft docs)
            SetParent(Handle, HwndMessage);

            //Place window in the system-maintained clipboard format listener list
            AddClipboardFormatListener(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            switch ((Messages)m.Msg)
            {
                case Messages.WmClipboardupdate:
                {
                    if (OnClipboardDataChanged != null)
                    {
                        // Get clipboard data
                        var data = Clipboard.GetDataObject();
                        // Pass to event
                        OnClipboardDataChanged.Invoke(data);
                    }
                    
                    break;
                }
            }

            base.WndProc(ref m);
        }
    }
}
