using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Threading.Tasks;
using YtEzDL.Utils;
using MetroFramework;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Threading;
using MetroFramework.Controls;
using YtEzDL.Interfaces;

namespace YtEzDL.Forms
{
    public partial class Track : UserControl, IProgress
    {
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        private static readonly string DirectoryName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        private Mutex _mutex = new Mutex(false);

        public JToken Json { get; }
        public string Url { get; private set; }
        public string Filename { get; private set; }

        public Track()
        {
            InitializeComponent();
        }

        public Track(JToken json)
        {
            // Save json
            Json = json ?? throw new ArgumentNullException(nameof(json));

            // Save url
            Url = Json["webpage_url"]?.Value<string>();
            Filename = Json["_filename"]?.Value<string>();

            InitializeComponent();
        }

        private void SetProperty(Action<UserControl> action)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                action.Invoke(this);
            }));
        }

        private T GetProperty<T>(Func<T> func)
        {
            return (T)Invoke(new MethodInvoker(() => func.Invoke()));
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
                textBoxTitle.Font = MetroFonts.Subtitle;
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
                _youtubeDl
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
                _youtubeDl.Cancel(DirectoryName, Filename);
            }
            finally
            {
                _mutex.ReleaseMutex();
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
