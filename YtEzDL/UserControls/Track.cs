using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using YtEzDL.Config;
using YtEzDL.DownLoad;
using YtEzDL.Interfaces;
using YtEzDL.Utils;

namespace YtEzDL.UserControls
{
    public partial class Track : MetroUserControl, IProgress
    {
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        
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
            if (TrackData.Thumbnails == null || !Configuration.Default.DownloadSettings.FetchBestThumbnail)
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

        private Image FetchThumbNail()
        {
            var thumbnail = FindThumbNail();
            if (thumbnail == null)
            {
                return null;
            }

            try
            {
                using (var image = ImageTools.Download(thumbnail))
                {
                    return ImageTools.Resize(image, pictureBox.Size, tabPageInfo.BackColor);
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
            var thumbnail = FetchThumbNail();
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
                textBoxTitle.Font = MetroFonts.Default(16);
                textBoxTitle.Text = TrackData.Title + Environment.NewLine + TrackData.WebpageUrl;

                // Add duration
                var timespan = TimeSpan.FromSeconds(TrackData.Duration);
                textBoxTitle.Text += Environment.NewLine + timespan.ToString(@"h\:mm\:ss");
                
                // Add upload date
                var uploadDate = TrackData.UploadDate;
                if (uploadDate != null)
                {
                    var date = DateTime.ParseExact(TrackData.UploadDate, "yyyyMMdd", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None);
                    textBoxTitle.Text += Environment.NewLine + date.ToString("D");
                }

                if (Configuration.Default.DownloadSettings.FetchThumbnail)
                {
                    Task.Run(SetThumbNail);
                }
            }

            checkBoxExtractAudio.AddCheckedBinding(Configuration.Default.DownloadSettings, p => p.ExtractAudio, DataSourceUpdateMode.Never);
            comboBoxAudioFormat.AddEnumBinding(Configuration.Default.DownloadSettings, p => p.AudioFormat, DataSourceUpdateMode.Never);
            comboBoxAudioQuality.AddEnumBinding(Configuration.Default.DownloadSettings, p => p.AudioQuality, DataSourceUpdateMode.Never);
            comboBoxVideoFormat.AddEnumBinding(Configuration.Default.DownloadSettings, p => p.VideoFormat, DataSourceUpdateMode.Never);
            base.OnLoad(e);
        }

        private volatile bool _isDownloading;

        /// <summary>
        /// Returns true if the track is downloading
        /// </summary>
        public bool DownLoading => _isDownloading;

        public void StartDownload(string path, CancellationToken token)
        {
            if (_isDownloading)
            {
                return;
            }

            _isDownloading = true;

            try
            {
                var parameters = DownLoadParameters.Create;
                if (checkBoxExtractAudio.Checked)
                {
                    parameters
                        .ExtractAudio()
                        .AudioFormat((AudioFormat)comboBoxAudioFormat.SelectedItem)
                        .AudioQuality((AudioQuality)comboBoxAudioQuality.SelectedItem);
                }
                else
                {
                    parameters.VideoFormat((VideoFormat)comboBoxVideoFormat.SelectedItem);
                }

                if (Configuration.Default.DownloadSettings.AddMetadata)
                {
                    parameters.AddMetadata();
                }

                if (Configuration.Default.DownloadSettings.EmbedThumbnail)
                {
                    parameters.EmbedThumbnail();
                }

                parameters
                    .SetPath(path)
                    .IgnoreErrors();

                // Download
                _youtubeDl.Download(parameters, TrackData.WebpageUrl, path, TrackData.Filename, this, token);
                    
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

        public class ToggleEventArgs : EventArgs
        {
            public bool Selected { get; }

            public ToggleEventArgs(bool selected)
            {
                Selected = selected;
            }
        }

        public delegate void ToggleEventHandler(object o, ToggleEventArgs e);

        public event ToggleEventHandler Toggle;
        
        public void SelectTrack(bool select, bool triggerEvent = true)
        {
            _selected = select;

            // Redraw border
            Invalidate(ClientRectangle, false);

            // Trigger event
            if (triggerEvent)
            {
                Toggle?.Invoke(this, new ToggleEventArgs(_selected));
            }
        }

        public void ToggleTrack(bool triggerEvent = true)
        {
            SelectTrack(!Selected, triggerEvent);
        }

        /// <summary>
        /// Set content of status label
        /// </summary>
        /// <param name="text"></param>
        public void SetStatus(string text)
        {
            SetProperty(c =>
            {
                metroLabel.Text = text;
                metroLabel.Refresh();
            });
        }

        private void Track_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                ToggleTrack();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw selection border
            if (_selected)
            {
                using (var pen = new Pen(Configuration.Default.LayoutSettings.SelectionColor, Configuration.Default.LayoutSettings.SelectionWidth))
                {
                    e.Graphics.DrawRectangle(pen, ClientRectangle);
                }
            }
        }

        private void Track_Resize(object sender, EventArgs e)
        {
            // Fix selection border
            Invalidate(ClientRectangle, false);
        }
        
        private void checkBoxExtractAudio_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAudioFormat.Enabled = checkBoxExtractAudio.Checked;
            comboBoxAudioQuality.Enabled = checkBoxExtractAudio.Checked;
            comboBoxVideoFormat.Enabled = !checkBoxExtractAudio.Checked;
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
