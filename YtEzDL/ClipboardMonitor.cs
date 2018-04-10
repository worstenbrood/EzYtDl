using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YtEzDL
{
    public class ClipboardMonitor : NativeWindow, IDisposable
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove,IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public enum Messages
        {
            WmDrawclipboard = 0x0308,
            WmChangecbchain = 0x030D
        }

        private IntPtr _clipboardViewerNext;

        // Event
        public delegate void ClipboardDataChanged(IDataObject dataObject);
        public event ClipboardDataChanged OnClipboardDataChanged;

        public void Monitor()
        {
            // Create handle
            CreateHandle(new CreateParams());

            // Set clipboard viewer
            _clipboardViewerNext = SetClipboardViewer(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            switch ((Messages)m.Msg)
            {
                case Messages.WmDrawclipboard:
                {
                    if (OnClipboardDataChanged != null)
                    {
                        // Get clipboard data
                        var data = Clipboard.GetDataObject();
                        // Pass to event
                        OnClipboardDataChanged.Invoke(data);
                    }

                    if (_clipboardViewerNext != IntPtr.Zero)
                    {
                        // Pass message to chain
                        SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    
                    break;
                }

                case Messages.WmChangecbchain:
                {
                    // Monitor chain changed
                    if (m.WParam == _clipboardViewerNext)
                    {
                        _clipboardViewerNext = m.LParam;
                    }
                    else
                    {
                        if (_clipboardViewerNext != IntPtr.Zero)
                        {
                            // Pass message to chain
                            SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                        }
                    }
                    break;
                }

                default:
                {
                    base.WndProc(ref m);
                    break;
                }
            }
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                // Remove from chain
                ChangeClipboardChain(Handle, _clipboardViewerNext);

                // Destroy window/handle
                DestroyHandle();
            }
        }
    }
}
