using System;
using System.Linq;
using System.Runtime.InteropServices;
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
      
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

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

        private void AddControl(JObject o)
        {
            flowLayoutPanel.BeginInvoke(new MethodInvoker(() =>
            {
                var control = new Track(o, _notifyIcon);
                control.Enabled = true;
                control.Width = flowLayoutPanel.Width - 25;
                flowLayoutPanel.Controls.Add(control);
            }));
        }

        protected override void OnLoad(EventArgs e)
        {
            // Base
            base.OnLoad(e);

            // Load data
            Task.Run(() => _youtubeDl.GetInfo(_url, AddControl));

            Text = "Fetching data ...";

            // Set foreground window
            SetForegroundWindow(Handle);
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
                Tracks[0].StartDownload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                // Set buttons
                ExecuteAsync(f =>
                {
                    metroButtonCancel.Enabled = false;
                    metroButtonDownload.Enabled = true;
                    metroLabelAction.Text = "Finished";
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
        
        private void DownloadForm_Resize(object sender, EventArgs e)
        {
            // Resize tracks
            foreach (var track in Tracks)
            {
                track.Width = flowLayoutPanel.Width - 25;
            }
        }
    }
}
