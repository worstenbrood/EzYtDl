using MetroFramework.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.UserControls
{
    public partial class Track : MetroUserControl, IProgress
    {
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        private static readonly string DirectoryName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        private readonly Mutex _mutex = new Mutex(false);
        private readonly NotifyIcon _notifyIcon;
        
        public bool IsDownLoading
        {
            get
            {
                var enter = _mutex.WaitOne(1);
                if (!enter)
                {
                    return true;
                }

                // Release mutex
                _mutex.ReleaseMutex();
                
                return false;
            }
        }

        public bool Selected => BorderStyle == BorderStyle.FixedSingle;

        public JToken Json { get; }
        public string Url { get; }
        public string Filename { get; }

        public Track()
        {
            InitializeComponent();
        }

        public Track(JToken json, NotifyIcon notifyIcon)
        {
            // Save json
            Json = json ?? throw new ArgumentNullException(nameof(json));

            // Save url
            Url = Json["webpage_url"]?.Value<string>();
            Filename = Json["_filename"]?.Value<string>();

            _notifyIcon = notifyIcon;

            InitializeComponent();
        }

        private void SetProperty(Action<UserControl> action)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                action.Invoke(this);
            }));
        }

        private Image DownloadThumbNail()
        {
            var thumbnail = Json["thumbnails"]?.Last["url"];
            if (thumbnail == null)
            {
                return null;
            }

            try
            {
                return ImageUtils.Resize(ImageUtils.Download(thumbnail.Value<string>()), pictureBox.Size, tabPageInfo.BackColor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void SetThumbNail()
        {
            var thumbnail = DownloadThumbNail();
            if (thumbnail != null)
            {
                SetProperty(c => pictureBox.Image = thumbnail);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Json != null)
            {
                // Set title
                Text = Json["title"]?.Value<string>()?.Replace("&", "&&") ?? "Untitled";

                // Set info
                textBoxTitle.Font = new Font(textBoxTitle.Font.FontFamily, 12);
                textBoxTitle.Text = Json["title"] + Environment.NewLine + Json["webpage_url"];

                // Add duration
                var duration = Json["duration"];
                if (duration != null)
                {
                    var timespan = TimeSpan.FromSeconds(Convert.ToDouble(duration));
                    textBoxTitle.Text += Environment.NewLine + timespan.ToString(@"hh\:mm\:ss");
                }

                // Add upload date
                var uploadDate = Json["upload_date"];
                if (uploadDate != null)
                {
                    var date = DateTime.ParseExact(uploadDate.Value<string>(), "yyyyMMdd", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None);
                    textBoxTitle.Text += Environment.NewLine + date.ToString("D");
                }

                Task.Run(SetThumbNail);
            }

            base.OnLoad(e);
        }

        public void StartDownload()
        {
            if (IsDownLoading)
            {
                throw new Exception("Already downloading");
            }

            try
            {
                _youtubeDl
                    .Reset()
                    .ExtractAudio()
                    .AddMetadata()
                    .EmbedThumbnail()
                    .AudioFormat(AudioFormat.Mp3)
                    .AudioQuality(AudioQuality.Fixed320)
                    .IgnoreErrors()
                    .Download(Url, DirectoryName, this);
                SetProperty(c => metroLabel.Text = "Done");
            }
            catch (Exception ex)
            {
                SetProperty(c => metroLabel.Text = $"Error: {ex.Message}");
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public void CancelDownload()
        {
            try
            {
                _youtubeDl.Cancel(DirectoryName, Filename);
            }
            catch (Exception ex)
            {
                SetProperty(c => metroLabel.Text = $"Error: {ex.Message}");
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // Redirect scroll to parent
                case Win32.MouseWheel:
                    Win32.PostMessage(Parent.Handle, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        public void Select(bool select)
        {
            SetProperty(c => BorderStyle = select ? BorderStyle.FixedSingle : BorderStyle.None);
        }

        public void Toggle()
        {
            Select(!Selected);
        }

        private void Track_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                Toggle();
            }
        }

        // IProgress

        public void Download(double progress)
        {
            SetProperty(c =>
            {
                metroLabel.Text = "Downloading...";
                metroProgressBar.Value = (int)progress;
            });
        }

        public void FfMpeg(double progress)
        {
            SetProperty(c =>
            {
                metroLabel.Text = "Converting...";
                metroProgressBar.Value = 100;
            });
        }
    }
}
