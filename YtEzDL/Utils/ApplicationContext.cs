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
using YtEzDL.Config;
using YtEzDL.Console;

namespace YtEzDL.Utils
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly object _lock = new object();
        // Start youtube-dl
        private readonly ClipboardMonitor _clipboardMonitor = new ClipboardMonitor();

        private void RunYtDlp(Action a)
        {
            try
            {
                a.Invoke();
            }
            catch (ConsoleProcessException ex)
            {
                // Error
                _notifyIcon.ShowBalloonTip(2000, Resources.YtDlpError, ex.Message, ToolTipIcon.Error);
            }
            catch (Exception ex)
            {
                // Error
                _notifyIcon.ShowBalloonTip(2000, Resources.SystemException, ex.Message, ToolTipIcon.Error);
            }
        }

        private void Update()
        {
           // Update
           RunYtDlp(() => YoutubeDownload.Instance.Update(t => _notifyIcon.ShowBalloonTip(2000, Resources.YtDlpUpdate, t, ToolTipIcon.Info)));
        }

        private void ClearCache()
        {
           RunYtDlp(() => YoutubeDownload.Instance.Run(DownLoadParameters.New.RemoveCache(), t => _notifyIcon.ShowBalloonTip(2000, "yt-dlp", t, ToolTipIcon.Info)));
        }

        private static ToolStripMenuItem SetupClearHistory()
        {
            return new ToolStripMenuItem("Clear history", null, (o, args) =>
            {
                History.Default.Clear();
                History.Default.Save();
            })
            {
                Enabled = History.Default.Count > 0
            };
        }

        private static ToolStripMenuItem SetupHistoryMenu()
        {
            var historyMenu = new ToolStripMenuItem
            {
                Text = Resources.History,
            };

            // Clear history
            historyMenu.DropDownItems.Add(SetupClearHistory());

            // Rebuild menu on dropdown opening
            historyMenu.DropDownOpening += (o, e) =>
            {
                historyMenu.DropDownItems.Clear();
                
                // Clear history
                historyMenu.DropDownItems.Add(SetupClearHistory());

                if (History.Default.Count > 0)
                {
                    // Spacer
                    historyMenu.DropDownItems.Add("-");
                }

                // Items
                historyMenu.DropDownItems.AddRange(History.Default.Items
                    .Select(historyItem =>
                    {
                        var toolStripMenuItem = new ToolStripMenuItem(historyItem.Title);
                        toolStripMenuItem.ToolTipText = historyItem.Url;
                        toolStripMenuItem.Click += (sender, args) =>
                        {
                            var url = new Uri(historyItem.Url);
                            Task.Run(() => ShowDownLoadForm(url));
                        };
                        return toolStripMenuItem;
                    })
                    .Cast<ToolStripItem>()
                    .ToArray());
            };

            return historyMenu;
        }

        private MetroContextMenu SetupContextMenu(IContainer container)
        {
            var contextMenu = new MetroContextMenu(container);
            var captureClipboard = new ToolStripMenuItem
            {
                Text = Resources.ContextCaptureClipboard,
                Checked = Configuration.Default.ApplicationSettings.CaptureClipboard,
                CheckOnClick = true
            };
           
            captureClipboard.CheckedChanged += (o, e) =>
            {
                // Change and save config
                Configuration.Default.ApplicationSettings.CaptureClipboard = captureClipboard.Checked;
                Configuration.Default.Save();

                _clipboardMonitor.EnableListener(captureClipboard.Checked);
            };

            contextMenu.Items.Add($"{CommonTools.ApplicationName} {CommonTools.ApplicationProductVersion}");
            contextMenu.Items[0].Enabled = false;
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(captureClipboard);
            contextMenu.Items.Add(SetupHistoryMenu());
            contextMenu.Items.Add(Resources.ContextAbout, null, (sender, args) => FormTools.ShowFormDialog<Forms.About>());
            contextMenu.Items.Add(Resources.ContextSettings, null, (sender, args) => FormTools.ShowFormDialog<Forms.Settings>());
            contextMenu.Items.Add(Resources.ContextClearCache, null, (sender, args) => ClearCache());
            contextMenu.Items.Add(Resources.ContextUpdate, null, (sender, args) => Update());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(Resources.ContextExit, null, (sender, args) => ExitThread());

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
            _clipboardMonitor.OnClipboardDataChanged += d => Task.Run(() => HandleClipboard(d));
            _clipboardMonitor.EnableListener(Configuration.Default.ApplicationSettings.CaptureClipboard);
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
