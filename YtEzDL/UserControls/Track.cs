using MetroFramework.Controls;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
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
        
        /// <summary>
        /// Returns the info of the track
        /// </summary>
        public string Content => textBoxTitle.Text;

        /// <summary>
        /// Json returned from yt-dlp
        /// </summary>
        public TrackData TrackData { get; }
        
        public Track()
        {
            InitializeComponent();
        }

        public Track(TrackData trackData)
        {
            // Save json
            TrackData = trackData ?? throw new ArgumentNullException(nameof(trackData));

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

        private string FindThumbNail()
        {
            if (TrackData.Thumbnails == null)
            {
                return TrackData.Thumbnail;
            }

            var best = 
                TrackData.Thumbnails
                     .OrderBy(t => t.Height)
                     .LastOrDefault(t => t.Height <= pictureBox.Height) ??
                TrackData.Thumbnails
                    .OrderBy(t => t.Height)
                    .FirstOrDefault(t => t.Height > pictureBox.Height);
            
            return best?.Url ?? TrackData.Thumbnail;
        }

        private Image DownloadThumbNail()
        {
            var thumbnail = FindThumbNail();
            if (thumbnail == null)
            {
                return null;
            }

            try
            {
                using (var image = ImageUtils.Download(thumbnail))
                {
                    return ImageUtils.Resize(image, pictureBox.Size, tabPageInfo.BackColor);
                }
            }
            catch (Exception ex)
            {
                Invoke(new MethodInvoker(() => MessageBox.Show(this, ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)));
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
            if (TrackData != null)
            {
                // Set title
                Text = TrackData.Title?.Replace("&", "&&") ?? "Untitled";

                // Set info
                textBoxTitle.Font = new Font(textBoxTitle.Font.FontFamily, 12);
                textBoxTitle.Text = TrackData.Title + Environment.NewLine + TrackData.WebpageUrl;

                // Add duration
                var duration = TrackData.Duration;
                if (duration != null)
                {
                    var timespan = TimeSpan.FromSeconds(Convert.ToDouble(duration));
                    textBoxTitle.Text += Environment.NewLine + timespan.ToString(@"hh\:mm\:ss");
                }

                // Add upload date
                var uploadDate = TrackData.UploadDate;
                if (uploadDate != null)
                {
                    var date = DateTime.ParseExact(TrackData.UploadDate, "yyyyMMdd", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None);
                    textBoxTitle.Text += Environment.NewLine + date.ToString("D");
                }

                Task.Run(SetThumbNail);
            }

            base.OnLoad(e);
        }

        private volatile bool _isDownloading;

        /// <summary>
        /// Returns true if the track is downloading
        /// </summary>
        public bool DownLoading => _isDownloading;

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
                    .DownloadAsync(TrackData.WebpageUrl, DirectoryName, this, token)
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
                _youtubeDl.Cancel(DirectoryName, TrackData.Filename);
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

        private volatile bool _selected;
        
        /// <summary>
        /// Returns the selected state of the track
        /// </summary>
        public bool Selected => _selected;

        public void Select(bool select)
        {
            _selected = select;

            // Redraw border
            Invalidate(ClientRectangle, false);
        }

        public void Toggle()
        {
            Select(!Selected);
        }

        /// <summary>
        /// Set content of status label
        /// </summary>
        /// <param name="text"></param>
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

        private static readonly Pen Pen = new Pen(MetroColors.Blue, 4);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw selection border
            if (_selected)
            {
                e.Graphics.DrawRectangle(Pen, ClientRectangle);
            }
        }

        private void Track_Resize(object sender, EventArgs e)
        {
            // Fix selection border
            Invalidate(ClientRectangle, false);
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
