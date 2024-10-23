using System;
using System.Windows.Forms;
using YtEzDL.Utils;

namespace YtEzDL.UserControls
{
    public class ClipboardMonitor : Form
    {
        public static IntPtr HwndMessage = new IntPtr(-3);
        
        // Event
        public delegate void ClipboardDataChanged(IDataObject dataObject);
        public event ClipboardDataChanged OnClipboardDataChanged;

        // Globals
        private volatile uint _prevSequence;

        public void Monitor()
        {
            // Initial sequence
            _prevSequence = Win32.GetClipboardSequenceNumber();

            //Turn the child window into a message-only window (refer to Microsoft docs)
            Win32.SetParent(Handle, HwndMessage);

            //Place window in the system-maintained clipboard format listener list
            Win32.AddClipboardFormatListener(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.ClipboardUpdate:
                {
                    if (OnClipboardDataChanged != null)
                    {
                        lock (this)
                        {
                            var sequence = Win32.GetClipboardSequenceNumber();
                            if (_prevSequence == sequence)
                                break;

                            _prevSequence = sequence;
                        }

                        // Get clipboard data
                        var data = Clipboard.GetDataObject();
#if DEBUG
                        Debug.WriteLine("(ClipboardMonitor) Sequence: {0} Data: {1}", _prevSequence, data?.GetData(DataFormats.StringFormat));
#endif
                        // Pass to event
                        OnClipboardDataChanged.Invoke(data);
                    }
                    break;
                }

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
