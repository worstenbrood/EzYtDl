using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using YtEzDL.UserControls;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class DownloadForm : MetroForm
    {
        private readonly string _url;
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        
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
            // Resize tracks
            foreach (var track in Tracks)
            {
                SetTrackWidth(track);
            }
        }

        private void AddControl(JObject o)
        {
            ExecuteAsync(f =>
            {
                var track = new Track(o);
                track.Enabled = true;
                SetTrackWidth(track);
                flowLayoutPanel.Controls.Add(track);
            });
        }
        private void LoadData()
        {
            try
            {
                _youtubeDl.GetInfo(_url, AddControl);
                    
                ExecuteAsync(f =>
                {
                    if (Tracks.Length > 1)
                    {
                        Text = $"Playlist: {Tracks[0].Json["playlist"]}";
                    }
                    else if (Tracks.Length == 1)
                    {
                        Text = $"Track: {Tracks[0].Json["title"]}";
                    }

                    Invalidate();
                });
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ExecuteAsync(f =>
                {
                    Cursor = Cursors.Arrow;
                    metroButtonDownload.Enabled = true;
                });
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // Base
            base.OnLoad(e);

            Text = $"Fetching {_url}";
            Cursor = Cursors.WaitCursor;
            toolStrip.Font = MetroFonts.Default(12);
            Font = MetroFonts.Default(11);
            
            // Load data
            Task.Run(LoadData);
            
            // Set foreground window
            Activate();
            FocusMe();
        }
        
        private void StartDownloadTasks()
        {
            var downloadTasks = Tracks
                .Where(t => t.Selected)
                .Select(t =>
                {
                    var task = new Task(() => t.StartDownload(Source.Token), Source.Token);
                    task.Start();
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
                metroButtonCancel.Enabled = cancel;
                metroButtonDownload.Enabled = download;
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
        
        private void MetroButtonDownload_Click(object sender, EventArgs e)
        {
            SetButtons(false, true);
            Task.Run(StartDownload, Source.Token);
        }

        private void MetroButtonCancel_Click(object sender, EventArgs e)
        {
            _source.Cancel();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.WindowsShutDown || _youtubeDl.IsRunning() || Tracks.Any(t => t.DownLoading);
        }

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            ResizeTracks();
        }

        private void toolStripButtonNone_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => t.Selected))
            {
                track.Select(false);
            }
        }

        private void toolStripButtonAll_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks.Where(t => !t.Selected))
            {
                track.Select(true);
            }
        }

        private void toolStripButtonToggle_Click(object sender, EventArgs e)
        {
            foreach (var track in Tracks)
            {
                track.Toggle();
            }
        }
    }
}
