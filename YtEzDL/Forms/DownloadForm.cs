using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using YtEzDL.UserControls;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class DownloadForm : MetroForm
    {
        private readonly string _url;
        private readonly NotifyIcon _notifyIcon;
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
      
        public DownloadForm(string url, NotifyIcon notifyIcon)
        {
            _url = url;
            _notifyIcon = notifyIcon;
            
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

        private void AddControl(JObject o)
        {
            flowLayoutPanel.BeginInvoke(new MethodInvoker(() =>
            {
                var track = new Track(o, _notifyIcon);
                track.Enabled = true;
                flowLayoutPanel.Controls.Add(track);
                SetTrackWidth(track);
                
            }));
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
            
            // Load data
            Task.Run(LoadData);
            
            // Set foreground window
            FocusMe();
            Activate();
        }

        private Track[] Tracks =>
            flowLayoutPanel.Controls
                .Cast<Track>()
                .ToArray();

        private void StartDownload()
        {
            try
            {
                foreach (var track in Tracks.Where(t => t.Selected))
                {
                    track.StartDownload();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Set buttons
                ExecuteAsync(f =>
                {
                    metroButtonCancel.Enabled = false;
                    metroButtonDownload.Enabled = true;
                });
            }
        }

        private void StopDownLoad()
        {
            // Stop youtube-dl
            foreach (var track in Tracks.Where(t => t.YoutubeDl.IsRunning()))
            {
                Task.Run(track.CancelDownload);
            }
        }
        
        private void MetroButtonDownload_Click(object sender, EventArgs e)
        {
            Task.Run(StartDownload);
        }

        private void MetroButtonCancel_Click(object sender, EventArgs e)
        {
            Task.Run(StopDownLoad);
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.WindowsShutDown || _youtubeDl.IsRunning();
        }
        
        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            // Resize tracks
            foreach (var track in Tracks)
            {
                SetTrackWidth(track);
            }
        }
    }
}
