using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YtEzDL.DownLoad;
using YtEzDL.Properties;
using YtEzDL.Tools;
using YtEzDL.UserControls;
using MetroFramework.Controls;

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

        private static void ShowSettingsForm()
        {
            var active = Application.OpenForms.OfType<Forms.Settings>()
                .FirstOrDefault();
            if (active != null)
            {
                active.FocusMe();
                return;
            }

            // Show form
            using (var settings = new Forms.Settings())
            {
                settings.ShowDialog();
            }
        }

        private static void ShowAboutForm()
        {
            var active = Application.OpenForms.OfType<Forms.About>()
                .FirstOrDefault();
            if (active != null)
            {
                active.FocusMe();
                return;
            }

            // Show form
            using (var about = new Forms.About())
            {
                about.ShowDialog();
            }
        }

        public ApplicationContext()
        {
            IContainer container = new Container();
            var contextMenu = new MetroContextMenu(container);
            contextMenu.Items.Add($"{CommonTools.ApplicationName} {CommonTools.ApplicationProductVersion}");
            contextMenu.Items[0].Enabled = false;
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("&About", null, (sender, args) => ShowAboutForm());
            contextMenu.Items.Add("&Settings", null, (sender, args) => ShowSettingsForm());
            contextMenu.Items.Add("&Clear cache", null, (sender, args) => ClearCache());
            contextMenu.Items.Add("&Update", null, (sender, args) => Update());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("&Exit", null, (sender, args) => ExitThread());
            
            // Setup notify icon
            _notifyIcon = new NotifyIcon(container)
            {
                ContextMenuStrip = contextMenu,
                Icon = Resources.YTIcon,
                Text = "youtube-dl",
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
