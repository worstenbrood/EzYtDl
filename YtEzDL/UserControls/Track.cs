using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using Newtonsoft.Json.Linq;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.UserControls
{
    public partial class Track : MetroUserControl, IProgress
    {
        public readonly YoutubeDownload YoutubeDl = new YoutubeDownload();
        private static readonly string DirectoryName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        private readonly Mutex _mutex = new Mutex(false);
        private readonly NotifyIcon _notifyIcon;

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool HideCaret(IntPtr hWnd);

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
                MessageBox.Show(ex.ToString());
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
                textBoxTitle.Font = MetroFonts.Title;
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
        
        public async void StartDownload()
        {
            if (!_mutex.WaitOne(1))
            {
                throw new Exception("Already downloading");
            }
           
            try
            {
                YoutubeDl
                    .Reset()
                    .ExtractAudio()
                    .AddMetadata()
                    .EmbedThumbnail()
                    .AudioFormat(AudioFormat.Mp3)
                    .AudioQuality(AudioQuality.Fixed320)
                    .Download(Url, DirectoryName, this);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public async void CancelDownload()
        {
            if (_mutex.WaitOne(1))
            {
                throw new Exception("Not downloading");
            }

            try
            {
                YoutubeDl.Cancel(DirectoryName, Filename);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        private void TextBoxTitle_GotFocus(object sender, EventArgs e)
        {
            // Hide editbox caret
            HideCaret(textBoxTitle.Handle);
        }
        
        [DllImport("User32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            switch (m.Msg)
            {
                case WM_MOUSEWHEEL:
                    if (m.HWnd == Handle)
                    {
                         PostMessage(Parent.Handle, m.Msg, m.WParam, m.LParam);
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        // IProgress

        public void Download(double progress)
        {
            SetProperty(c =>
            {
                metroLabelAction.Text = "Downloading...";
                metroProgressBar.Value = (int)progress;
            });
        }

        public void FfMpeg(double progress)
        {
            SetProperty(c =>
            {
                metroLabelAction.Text = "Converting...";
                metroProgressBar.Value = 100;
            });
        }
    }
}
