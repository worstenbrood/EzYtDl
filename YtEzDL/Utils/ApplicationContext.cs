using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using YtEzDL.DownLoad;
using YtEzDL.Properties;
using YtEzDL.Tools;
using YtEzDL.UserControls;
using MetroFramework.Controls;
using YtEzDL.Config;

namespace YtEzDL.Utils
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly object _lock = new object();
        // Start youtube-dl
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();

        private void RunYtDlp(Action a)
        {
            try
            {
                a.Invoke();
            }
            catch (ConsoleProcessException ex)
            {
                // Error
                _notifyIcon.ShowBalloonTip(2000, "yt-dlp error", ex.Message, ToolTipIcon.Error);
            }
            catch (Exception ex)
            {
                // Error
                _notifyIcon.ShowBalloonTip(2000, "System error", ex.Message, ToolTipIcon.Error);
            }
        }

        private void Update()
        {
           // Update
           RunYtDlp(() => _youtubeDl.Update(t => _notifyIcon.ShowBalloonTip(2000, "yt-dlp update", t, ToolTipIcon.Info)));
        }

        private void ClearCache()
        {
           RunYtDlp(() => _youtubeDl.Run(DownLoadParameters.Create.RemoveCache(), t => _notifyIcon.ShowBalloonTip(2000, "yt-dlp", t, ToolTipIcon.Info)));
        }

        private MetroContextMenu SetupContextMenu(IContainer container)
        {
            var contextMenu = new MetroContextMenu(container);
            var captureClipboard = new ToolStripMenuItem
            {
                Text = "Capture clipboard",
                Checked = Configuration.Default.ApplicationSettings.CaptureClipboard,
            };
            captureClipboard.Click += (o, e) =>
            {
                // Change checked status
                captureClipboard.Checked = !captureClipboard.Checked;
            };
            captureClipboard.CheckedChanged += (o, e) =>
            {
                // Change and save config
                Configuration.Default.ApplicationSettings.CaptureClipboard = captureClipboard.Checked;
                Configuration.Default.Save();
            };

            contextMenu.Items.Add($"{CommonTools.ApplicationName} {CommonTools.ApplicationProductVersion}");
            contextMenu.Items[0].Enabled = false;
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(captureClipboard);
            contextMenu.Items.Add("&About", null, (sender, args) => FormTools.ShowFormDialog<Forms.About>());
            contextMenu.Items.Add("&Settings", null, (sender, args) => FormTools.ShowFormDialog<Forms.Settings>());
            contextMenu.Items.Add("&Clear cache", null, (sender, args) => ClearCache());
            contextMenu.Items.Add("&Update", null, (sender, args) => Update());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("&Exit", null, (sender, args) => ExitThread());

            return contextMenu;
        }

        public ApplicationContext()
        {
            IContainer container = new Container();
            var contextMenu = SetupContextMenu(container);
            
            // Setup notify icon
            _notifyIcon = new NotifyIcon(container)
            {
                ContextMenuStrip = contextMenu,
                Icon = Resources.YTIcon,
                Text = contextMenu.Items[0].Text,
                Visible = true,
            };

#if !DEBUG
            Update();
#endif

            // Start clipboard monitor
            var clipboardMonitor = new ClipboardMonitor();
            clipboardMonitor.OnClipboardDataChanged += d => Task.Run(() => HandleClipboard(d));
            clipboardMonitor.Monitor();
        }

        private static void ShowDownLoadForm(Uri url)
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
                Task.Run(() => ShowDownLoadForm(url));
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
