using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using YtEzDL.Utils;

namespace YtEzDL
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly YoutubeDownload _youtubeDl;
       
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
            _youtubeDl = new YoutubeDownload();

            // Update
            _notifyIcon.ShowBalloonTip(2000, "Updating...", _youtubeDl.Update(), ToolTipIcon.Info);

            // Start clipboard monitor
            var clipboardMonitor = new ClipboardMonitor();
            clipboardMonitor.OnClipboardDataChanged += data => Task.Run(() => HandleClipboard(data));
            clipboardMonitor.Monitor();
        }

        private void ShowDownLoadForm(List<JObject> info)
        {
            // Show form
            var downloadForm = new DownloadForm(info, _notifyIcon);
            Application.EnableVisualStyles();
            Application.Run(downloadForm);
        }

        private readonly TimedVariable<string> _prevData = new TimedVariable<string>(string.Empty, 5000, string.Empty);

        private void HandleClipboard(IDataObject dataObject)
        {
            if (!dataObject.GetDataPresent(DataFormats.StringFormat))
                return;

            var text = (string) dataObject.GetData(DataFormats.StringFormat);
            if (_prevData.Value.Equals(text))
                return;

            _prevData.Value = text;
            
            try
            {
                var url = new Uri(text);

                // Get info
                var info = _youtubeDl.GetInfo(url.OriginalString);

                // Check if url is supported
                if (info != null && info.Count > 0)
                {
                    Task.Run(() => ShowDownLoadForm(info));
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
            base.ExitThreadCore();
        }
    }
}
