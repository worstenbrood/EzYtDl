using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using YtEzDL.UserControls;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class DownloadForm : MetroForm
    {
        private readonly string _url;
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();

        private CancellationTokenSource _source;

        private CancellationTokenSource Source
        {
            get
            {
                if (_source == null)
                {
                    return _source = new CancellationTokenSource();
                }

                if (!_source.IsCancellationRequested)
                {
                    return _source;
                }
                _source.Dispose();
                return _source = new CancellationTokenSource();
            }
        }

        public DownloadForm(string url)
        {
            _url = url;
            
            InitializeComponent();
        }

        private void ExecuteAsync(Action<Form> action)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                action.Invoke(this);
            }));
        }

        private void Execute(Action<Form> action)
        {
            Invoke(new MethodInvoker(() =>
            {
                action.Invoke(this);
            }));
        }

        private void SetTrackWidth(Track track)
        {
            var offset = 10;
            
            if (flowLayoutPanel.VerticalScroll.Visible)
            {
                offset += SystemInformation.VerticalScrollBarWidth;
            }
            
            track.Width = flowLayoutPanel.Width - offset;
        }

        private Track[] Tracks =>
            flowLayoutPanel.Controls
                .Cast<Track>()
                .ToArray();

        private void ResizeTracks()
        {
            // Redraw flow panel
            flowLayoutPanel.Invalidate(flowLayoutPanel.ClientRectangle, true);
            flowLayoutPanel.SuspendLayout();

            // Resize tracks
            foreach (var track in Tracks)
            {
                SetTrackWidth(track);
            }
            
            flowLayoutPanel.ResumeLayout();
        }

        private void FilterTrack(Track track, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                track.Visible = true;
            }

            track.Visible = track.Content.IndexOf(toolStripTextBoxSearch.Text, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private void FilterTracks()
        {
            flowLayoutPanel.SuspendLayout();
            foreach (var track in Tracks)
            {
                FilterTrack(track, toolStripTextBoxSearch.Text);
            }
            flowLayoutPanel.ResumeLayout();
        }
        
        private void AddControl(TrackData trackData)
        {
            ExecuteAsync(f =>
            {
                var track = new Track(trackData);
                SetTrackWidth(track);
                FilterTrack(track, toolStripTextBoxSearch.Text);
                track.Select(true);
                flowLayoutPanel.Controls.Add(track);
            });
        }

        private static string SafeString(string str)
        {
            return str.Replace("&", "&&");
        }

        private void LoadData()
        {
            try
            {
                _youtubeDl.GetInfoAsync(_url, AddControl, Source.Token)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }
            catch (Exception exception)
            {
                Execute(f => MessageBox.Show(this, exception.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                ExecuteAsync(f =>
                {
                    if (Tracks.Length > 1)
                    {
                        Text = SafeString($"Playlist: {Tracks[0].TrackData.Playlist} ({Tracks[0].TrackData.WebpageUrlDomain})");
                    }
                    else if (Tracks.Length == 1)
                    {
                        Text = SafeString($"Track: {Tracks[0].TrackData.Title} ({Tracks[0].TrackData.WebpageUrlDomain})");
                        toolStripButtonAll.Enabled = false;
                        toolStripButtonNone.Enabled = false;
                        toolStripButtonToggle.Enabled = false;
                        toolStripTextBoxSearch.Enabled = false;
                        toolStripButtonReset.Enabled = false;
                    }

                    toolStripButtonDownload.Enabled = true;
                    toolStripButtonCancel.Enabled = false;
                    metroProgressSpinner.Spinning = false;
                    metroProgressSpinner.Visible = false;
                    Invalidate(ClientRectangle, false);
                });
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // Base
            base.OnLoad(e);

            Text = SafeString($"Fetching {_url}");
            toolStrip.Font = MetroFonts.Default(12);
            Font = MetroFonts.Default(11);
            
            // Load data
            Task.Run(LoadData);
            
            // Set foreground window
            Activate();
            Show();
            FocusMe();
        }

        private readonly LimitedConcurrencyLevelTaskScheduler _scheduler = new LimitedConcurrencyLevelTaskScheduler(2);

        private void StartDownloadTasks()
        {
            var downloadTasks = Tracks
                .Where(t => t.Selected)
                .Select(t =>
                {
                    var task = new Task(() => t.StartDownload(Source.Token), Source.Token);
                    task.Start(_scheduler);
                    return task;
                })
                .ToArray();

            try
            {
                Task.WaitAll(downloadTasks, Source.Token);
            }
            catch (OperationCanceledException)
            {
                CancelDownloadTasks();
            }
        }

        private void CancelDownloadTasks()
        {
            var stopTasks = Tracks
                .Where(t => t.Selected && t.DownLoading)
                .Select(t =>
                {
                    var task = new Task(t.CancelDownload);
                    task.Start();
                    return task;
                })
                .ToArray();

            Task.WaitAll(stopTasks);
        }

        private void SetButtons(bool download, bool cancel)
        {
            ExecuteAsync(f =>
            {
                toolStripButtonCancel.Enabled = cancel;
                toolStripButtonDownload.Enabled = download;
            });
        }

        private void StartDownload()
        {
            try
            {
                StartDownloadTasks();
            }
            finally
            {
                SetButtons(true, false);
            }
        }
        
        private void toolStripButtonDownload_Click(object sender, EventArgs e)
        {
            SetButtons(false, true);
            Task.Run(StartDownload, Source.Token);
        }

        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            Source.Cancel();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.WindowsShutDown || _youtubeDl.IsRunning() || Tracks.Any(t => t.DownLoading);
        }

        private void toolStripButtonNone_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => t.Visible).Where(t => t.Selected))
            {
                track.Select(false);
            }
        }

        private void toolStripButtonAll_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => t.Visible).Where(t => !t.Selected))
            {
                track.Select(true);
            }
        }

        private void toolStripButtonToggle_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => t.Visible))
            {
                track.Toggle();
            }
        }
        
        private void toolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterTracks();
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settings.ShowDialog(this);
        }

        private void toolStripButtonReset_Click(object sender, EventArgs e)
        {
            toolStripTextBoxSearch.Clear();
        }

        /// <summary>
        /// Resize tracks when manual resizing ends
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadForm_ResizeEnd(object sender, EventArgs e)
        {
            ResizeTracks();
        }

        private FormWindowState _previousWindowState = FormWindowState.Normal;

        /// <summary>
        /// Resize tracks if form gets maximized or restored
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                ResizeTracks();
            }

            if (WindowState == FormWindowState.Normal && _previousWindowState == FormWindowState.Maximized)
            {
                ResizeTracks();
            }

            _previousWindowState = WindowState;
        }

        private bool _scrollVisible;
        
        /// <summary>
        /// Resize tracks once, once the scrollbar is visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flowLayoutPanel_Layout(object sender, LayoutEventArgs e)
        {
            switch (_scrollVisible)
            {
                case false when flowLayoutPanel.VerticalScroll.Visible:
                    _scrollVisible = true;
                    ResizeTracks();
                    break;
                case true when !flowLayoutPanel.VerticalScroll.Visible:
                    _scrollVisible = false;
                    ResizeTracks();
                    break;
            }
        }

        private void ClearCache()
        {
            try
            {
                new YoutubeDownload()
                    .RemoveCache()
                    .Run();
                Execute(f => MessageBox.Show(this, "Cache cleared", "yt-dlp", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            catch (Exception exception)
            {
                Execute(f => MessageBox.Show(this, exception.ToString(), "Error removing cache", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                ExecuteAsync(f =>
                {
                    toolStripButtonClearCache.Enabled = true;
                });
            }
        }

        private void toolStripButtonClearCache_Click(object sender, EventArgs e)
        {
            toolStripButtonClearCache.Enabled = false;
            Task.Run(ClearCache);
        }
    }
}
