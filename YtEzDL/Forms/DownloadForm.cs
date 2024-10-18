using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using YtEzDL.Config;
using YtEzDL.DownLoad;
using YtEzDL.Properties;
using YtEzDL.UserControls;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class DownloadForm : MetroForm
    {
        private readonly Uri _url;
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        private readonly HashSet<string> _ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

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

        public DownloadForm(Uri url)
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

        private void SetStatusLabel()
        {
            toolStripStatusLabel.Text = $"Total: {Tracks.Count()} / Selected: {Tracks.Count(t => t.Selected)}";
        }

        private void track_OnToggle(object o, Track.ToggleEventArgs e)
        {
            Execute(f => SetStatusLabel());
        }

        private void SetTrackWidth(Track track)
        {
            track.Width = flowLayoutPanel.Width - flowLayoutPanel.Left - flowLayoutPanel.Margin.Right - SystemInformation.VerticalScrollBarWidth;
        }

        private IEnumerable<Track> Tracks =>
            flowLayoutPanel.Controls
                .OfType<Track>();

        private bool IsPlaylist => Tracks.Count() > 1;

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
            Execute(f =>
            {
                // Check if already added
                if (_ids.Contains(trackData.Id))
                {
                    return;
                }
               
                var track = new Track(trackData);
                SetTrackWidth(track);
                track.Toggle += track_OnToggle; 
                FilterTrack(track, toolStripTextBoxSearch.Text);
                flowLayoutPanel.Controls.Add(track);
                track.SelectTrack(Configuration.Default.LayoutSettings.AutoSelect);
                _ids.Add(trackData.Id);
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
                _youtubeDl.GetJson(_url.ToString(), AddControl, Source.Token);
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }
            catch (ConsoleProcessException exception)
            {
                if (Tracks.Count() > 1)
                {
                    Execute(f => MetroMessageBox.Show(this, exception.Message, "yt-dlp error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error));
                }
            }
            catch (Exception exception)
            {
                Execute(f => MetroMessageBox.Show(this, exception.Message, "System exception", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                ExecuteAsync(f =>
                {
                    var count = Tracks.Count();

                    if (count > 1)
                    {
                        Text = SafeString($"Playlist: {Tracks.First().TrackData.Playlist} ({Tracks.First().TrackData.WebpageUrlDomain})");
                    }
                    else switch (count)
                    {
                        case 1:
                            Text = SafeString($"Track: {Tracks.First().TrackData.Title} ({Tracks.First().TrackData.WebpageUrlDomain})");
                            toolStripButtonAll.Enabled = false;
                            toolStripButtonNone.Enabled = false;
                            toolStripButtonToggle.Enabled = false;
                            toolStripTextBoxSearch.Enabled = false;
                            toolStripButtonReset.Enabled = false;
                            break;
                        case 0:
                            Close();
                            return;
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

            // Layout
            Icon = Resources.YTIcon;
            Text = SafeString($"Fetching {_url}");
            metroProgressSpinner.Value = 25;
            flowLayoutPanel.AutoScroll = false;
            flowLayoutPanel.HorizontalScroll.Minimum = int.MaxValue;
            flowLayoutPanel.HorizontalScroll.Maximum = int.MaxValue;
            flowLayoutPanel.HorizontalScroll.Visible = false;
            flowLayoutPanel.HorizontalScroll.Enabled = false;
            flowLayoutPanel.AutoScroll = true;
            
            // Load data
            Task.Run(LoadData);
        }

        private string GetPath()
        {
            var path = Configuration.Default.FileSettings.Path;
            if (!IsPlaylist || !Configuration.Default.FileSettings.CreatePlaylistFolder)
            {
                return path;
            }
            path = Path.Combine(path, Tracks.First().TrackData.Playlist);
            Directory.CreateDirectory(path);
            return path;
        }

        private void CleanupPath()
        {
            if (IsPlaylist && Configuration.Default.FileSettings.CreatePlaylistFolder)
            {
                var path = Path.Combine(Configuration.Default.FileSettings.Path, Tracks.First().TrackData.Playlist);
                if (!Directory.EnumerateFileSystemEntries(path).Any())
                {
                    try
                    {
                        Directory.Delete(path);
                    }
#if DEBUG
                    catch (Exception ex)
                    {

                        Debug.WriteLine("DeleteDirectory: " + ex.Message);
                    }
#else
                    catch (Exception)
                    {

                        // Ignore
                    }
#endif
                }
            }
        }

        private void StartDownloadTasks()
        {
            var path = GetPath();
            var scheduler = new LimitedConcurrencyLevelTaskScheduler(Configuration.Default.DownloadSettings.DownloadThreads);
            var downloadTasks = Tracks
                .Where(t => t.Selected)
                .Select(t =>
                {
                    var task = new Task(() => t.StartDownload(path, Source.Token), Source.Token);
                    task.Start(scheduler);
                    return task;
                })
                .ToArray();

            try
            {
                Task.WaitAll(downloadTasks, Source.Token);
            }
            catch (OperationCanceledException)
            {
                CleanupPath();
            }
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
            e.Cancel = e.CloseReason == CloseReason.WindowsShutDown || _youtubeDl.IsRunning || Tracks.Any(t => t.DownLoading);
        }

        private void toolStripButtonNone_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => t.Visible).Where(t => t.Selected))
            {
                track.SelectTrack(false, false);
            }
            SetStatusLabel();
        }

        private void toolStripButtonAll_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => t.Visible).Where(t => !t.Selected))
            {
                track.SelectTrack(true, false);
            }
            SetStatusLabel();
        }

        private void toolStripButtonToggle_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => t.Visible))
            {
                track.ToggleTrack(false);
            }
            SetStatusLabel();
        }
        
        private void toolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterTracks();
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
        
        private void ClearCache()
        {
            try
            {
                var output = new StringBuilder();
                _youtubeDl.Run(DownLoadParameters.Create.RemoveCache(), t => output.AppendLine(t));
                
                Execute(f => MetroMessageBox.Show(this, output.ToString(), "yt-dlp", MessageBoxButtons.OK, MessageBoxIcon.Information));
            }
            catch (ConsoleProcessException exception)
            {
                Execute(f => MetroMessageBox.Show(this, exception.Message, "yt-dlp error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            catch (Exception exception)
            {
                Execute(f => MetroMessageBox.Show(this, exception.Message, "Error removing cache", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                ExecuteAsync(f => toolStripButtonClearCache.Enabled = true);
            }
        }

        private void toolStripButtonClearCache_Click(object sender, EventArgs e)
        {
            toolStripButtonClearCache.Enabled = false;
            Task.Run(ClearCache);
        }

        private void flowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (Tracks.Count() == 1)
            {
                // Set foreground window
                Activate();
                Show();
                FocusMe();
            }
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            using (var about = new About())
            {
                about.ShowInTaskbar = false;
                about.ShowDialog(this); 
            }
        }
    }
}
