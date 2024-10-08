using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
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
            
            metroButtonCancel.Enabled = false;
        }

        private void ExecuteAsync(Action<Form> action)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                action.Invoke(this);
            }));
        }

        private void LoadData()
        {
            var info = _youtubeDl.GetInfo(_url);
            if (info != null && info.Count > 0)
            {
                flowLayoutPanel.BeginInvoke(new MethodInvoker(() =>
                {
                    var controls = info
                        .Select(o =>
                        {
                            var control = new Track(o, _notifyIcon);
                            control.Enabled = true;
                            return control;
                        })
                        .Cast<Control>()
                        .ToArray();

                    flowLayoutPanel.Controls.AddRange(controls);
                }));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // Base
            base.OnLoad(e);

            // Load data
            Task.Run(LoadData);

            Text = _url;

            // Set foregroundwindow
            SetForegroundWindow(Handle);
            Activate();
        }

        private void StartDownload()
        {
            try
            {
                ExecuteAsync(form => ((Track)flowLayoutPanel.Controls[0]).StartDownload());
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
    }
}
