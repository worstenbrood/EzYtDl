using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using YtEzDL.UserControls;

namespace YtEzDL.Utils
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly object _lock = new object();

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

            // Start youtube-dl
            var youtubeDl = new YoutubeDownload();

            try
            {
                // Update
                youtubeDl.Update(t => _notifyIcon.ShowBalloonTip(2000, "yt-dlp update", t, ToolTipIcon.Info));
            }
            catch (ConsoleProcessException ex)
            {
                // Update
                _notifyIcon.ShowBalloonTip(2000, "yt-dlp update error", ex.Message, ToolTipIcon.Error);
            }
            

            // Start clipboard monitor
            var clipboardMonitor = new ClipboardMonitor();
            clipboardMonitor.OnClipboardDataChanged += d => Task.Run(() => HandleClipboard(d));
            clipboardMonitor.Monitor();
        }

        private static void ShowDownLoadForm(string url)
        {
            // Show form
            using (var downloadForm = new Forms.DownloadForm(url))
            {
                Application.EnableVisualStyles();
                Application.Run(downloadForm);
            }
        }

        private readonly TimedVariable<string> _prevData = new TimedVariable<string>(string.Empty, 5000, string.Empty);

        private void HandleClipboard(IDataObject dataObject)
        {
            if (!dataObject.GetDataPresent(DataFormats.StringFormat))
                return;

            var text = (string) dataObject.GetData(DataFormats.StringFormat);

            // Synchronize
            lock (_lock)
            {
                if (_prevData.Value.Equals(text, StringComparison.OrdinalIgnoreCase))
                    return;

                _prevData.Value = text;
            }

            try
            {
                var url = new Uri(text);
                Task.Run(() => ShowDownLoadForm(url.ToString()));
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false; // should remove lingering tray icon
            base.ExitThreadCore();
        }
    }
}
