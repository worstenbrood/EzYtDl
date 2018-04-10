using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace YtEzDL
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly ClipboardMonitor _clipboardMonitor;
        private readonly YoutubeDl _youtubeDl;
       
        public ApplicationContext()
        {
            IContainer container = new Container();

            // Setup notifyicon
            _notifyIcon = new NotifyIcon(container)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(typeof(Program), "YTIcon.ico"),
                Text = "youtube-dl",
                Visible = true,
            };

            // Click
            _notifyIcon.BalloonTipClicked += (sender, args) => MessageBox.Show(Thread.CurrentThread.GetHashCode().ToString());
            
            // Start youtube-dl
            _youtubeDl = new YoutubeDl();

            // Start clipboard monitor
            _clipboardMonitor = new ClipboardMonitor();
            _clipboardMonitor.OnClipboardDataChanged += HandleClipboard;
            _clipboardMonitor.Monitor();
        }

        private void HandleClipboard(IDataObject dataObject)
        {
            if (!dataObject.GetDataPresent(DataFormats.StringFormat))
                return;

            var text = (string) dataObject.GetData(DataFormats.StringFormat);

            try
            {
                var url = new Uri(text);

                // Get info
                var info = _youtubeDl.GetInfo(url.OriginalString);

                // Check if url is supported
                if (info != null)
                {
                    var json = JObject.Parse(info);

                    // Show form
                    var downloadForm = new DownloadForm(json);
                    downloadForm.Show();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false; // should remove lingering tray icon
            _clipboardMonitor.Dispose();
            base.ExitThreadCore();
        }
    }
}
