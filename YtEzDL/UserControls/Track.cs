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
using MetroFramework;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.UserControls
{
    public partial class Track : MetroUserControl, IProgress
    {
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        private static readonly string DirectoryName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        private volatile bool _isDownloading;
        
        public bool DownLoading => _isDownloading;

        public bool Selected => BorderStyle == BorderStyle.FixedSingle;

        public JToken Json { get; }
        public string Url { get; }
        public string Filename { get; }

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

            // Init
            InitializeComponent();

            textBoxTitle.Font = MetroFonts.Default(12);
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
            var thumbnail = Json["thumbnail"]?.Value<string>();
            if (thumbnail == null)
            {
                return null;
            }

            try
            {
                return ImageUtils.Resize(ImageUtils.Download(thumbnail), pictureBox.Size, tabPageInfo.BackColor);
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

        public void StartDownload(CancellationToken token)
        {
            if (_isDownloading)
            {
                return;
            }

            _isDownloading = true;

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
                    .DownloadAsync(Url, DirectoryName, this, token)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
                SetStatus("Done");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}");
            }
            finally
            {
                _isDownloading = false;
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
                SetStatus($"Error: {ex.Message}");
            }
            finally
            {
                _isDownloading = false;
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

        public void SetStatus(string text)
        {
            SetProperty(c => metroLabel.Text = text);
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
