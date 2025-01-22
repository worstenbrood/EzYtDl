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
using YtEzDL.Properties;
using YtEzDL.Tools;
using YtEzDL.Utils;

namespace YtEzDL.UserControls
{
    public partial class Track : MetroUserControl, IProgress
    {
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        private volatile Image _thumbnail;

        private readonly Configuration _configuration = new Configuration
        {
            DownloadSettings =
            {
                ExtractAudio = Configuration.Default.DownloadSettings.ExtractAudio,
                AudioFormat = Configuration.Default.DownloadSettings.AudioFormat,
                AudioQuality = Configuration.Default.DownloadSettings.AudioQuality,
                VideoFormat = Configuration.Default.DownloadSettings.VideoFormat
            }
        };

        /// <summary>
        /// Returns the info of the track
        /// </summary>
        public string Content => textBoxTitle.Text;

        /// <summary>
        /// Json returned from yt-dlp
        /// </summary>
        public TrackData TrackData { get; }

        public delegate void ClickedEventHandler(object o, EventArgs args);

        public event ClickedEventHandler Play;

        private MetroTabPage _tabPageSettings;
        private MetroCheckBox _checkBoxExtractAudio;
        private MetroComboBox _comboBoxAudioFormat;
        private MetroLabel _labelAudioFormat;
        private MetroLabel _labelAudioQuality;
        private MetroComboBox _comboBoxAudioQuality;
        private MetroLabel _labelVideoFormat;
        private MetroComboBox _comboBoxVideoFormat;

        private void AddPerTrackSettings()
        {
            _tabPageSettings = new MetroTabPage();
            _checkBoxExtractAudio = new MetroCheckBox();
            _comboBoxAudioFormat = new MetroComboBox();
            _comboBoxVideoFormat = new MetroComboBox();
            _labelAudioFormat = new MetroLabel();
            _labelAudioQuality = new MetroLabel();
            _comboBoxAudioQuality = new MetroComboBox();
            _labelVideoFormat = new MetroLabel();
            
            // 
            // tabPageSettings
            // 
            _tabPageSettings.BackColor = Color.White;
            _tabPageSettings.Controls.Add(_labelVideoFormat);
            _tabPageSettings.Controls.Add(_comboBoxVideoFormat);
            _tabPageSettings.Controls.Add(_labelAudioQuality);
            _tabPageSettings.Controls.Add(_comboBoxAudioQuality);
            _tabPageSettings.Controls.Add(_labelAudioFormat);
            _tabPageSettings.Controls.Add(_comboBoxAudioFormat);
            _tabPageSettings.Controls.Add(_checkBoxExtractAudio);
            _tabPageSettings.HorizontalScrollbarBarColor = false;
            _tabPageSettings.HorizontalScrollbarHighlightOnWheel = false;
            _tabPageSettings.HorizontalScrollbarSize = 10;
            _tabPageSettings.Location = new Point(4, 44);
            _tabPageSettings.Name = "tabPageSettings";
            _tabPageSettings.Size = new Size(620, 159);
            _tabPageSettings.TabIndex = 1;
            _tabPageSettings.Text = "Settings";
            _tabPageSettings.VerticalScrollbarBarColor = false;
            _tabPageSettings.VerticalScrollbarHighlightOnWheel = false;
            _tabPageSettings.VerticalScrollbarSize = 10;
            _tabPageSettings.MouseClick += Track_MouseClick;
            _tabPageSettings.SuspendLayout();

            // 
            // labelVideoFormat
            // 
            _labelVideoFormat.AutoSize = true;
            _labelVideoFormat.FontSize = MetroLabelSize.Tall;
            _labelVideoFormat.FontWeight = MetroLabelWeight.Regular;
            _labelVideoFormat.ForeColor = Color.Black;
            _labelVideoFormat.Location = new Point(4, 114);
            _labelVideoFormat.Name = "labelVideoFormat";
            _labelVideoFormat.Size = new Size(121, 25);
            _labelVideoFormat.TabIndex = 8;
            _labelVideoFormat.Text = "Video format:";
            // 
            // comboBoxVideoFormat
            // 
            _comboBoxVideoFormat.BackColor = Color.White;
            _comboBoxVideoFormat.FontSize = MetroComboBoxSize.Tall;
            _comboBoxVideoFormat.ForeColor = Color.Black;
            _comboBoxVideoFormat.FormattingEnabled = true;
            _comboBoxVideoFormat.ItemHeight = 29;
            _comboBoxVideoFormat.Location = new Point(127, 110);
            _comboBoxVideoFormat.Name = "comboBoxVideoFormat";
            _comboBoxVideoFormat.Size = new Size(121, 35);
            _comboBoxVideoFormat.TabIndex = 7;
            _comboBoxVideoFormat.UseSelectable = true;
            // 
            // labelAudioQuality
            // 
            _labelAudioQuality.AutoSize = true;
            _labelAudioQuality.FontSize = MetroLabelSize.Tall;
            _labelAudioQuality.FontWeight = MetroLabelWeight.Regular;
            _labelAudioQuality.ForeColor = Color.Black;
            _labelAudioQuality.Location = new Point(4, 80);
            _labelAudioQuality.Name = "labelAudioQuality";
            _labelAudioQuality.Size = new Size(122, 25);
            _labelAudioQuality.TabIndex = 6;
            _labelAudioQuality.Text = "Audio quality:";
            // 
            // comboBoxAudioQuality
            // 
            _comboBoxAudioQuality.BackColor = Color.White;
            _comboBoxAudioQuality.FontSize = MetroComboBoxSize.Tall;
            _comboBoxAudioQuality.ForeColor = Color.Black;
            _comboBoxAudioQuality.FormattingEnabled = true;
            _comboBoxAudioQuality.ItemHeight = 29;
            _comboBoxAudioQuality.Location = new Point(127, 76);
            _comboBoxAudioQuality.Name = "comboBoxAudioQuality";
            _comboBoxAudioQuality.Size = new Size(121, 35);
            _comboBoxAudioQuality.TabIndex = 5;
            _comboBoxAudioQuality.UseSelectable = true;
            // 
            // labelAudioFormat
            // 
            _labelAudioFormat.AutoSize = true;
            _labelAudioFormat.FontSize = MetroLabelSize.Tall;
            _labelAudioFormat.FontWeight = MetroLabelWeight.Regular;
            _labelAudioFormat.ForeColor = Color.Black;
            _labelAudioFormat.Location = new Point(4, 45);
            _labelAudioFormat.Name = "labelAudioFormat";
            _labelAudioFormat.Size = new Size(123, 25);
            _labelAudioFormat.TabIndex = 4;
            _labelAudioFormat.Text = "Audio format:";
            // 
            // comboBoxAudioFormat
            // 
            _comboBoxAudioFormat.BackColor = Color.White;
            _comboBoxAudioFormat.FontSize = MetroComboBoxSize.Tall;
            _comboBoxAudioFormat.ForeColor = Color.Black;
            _comboBoxAudioFormat.FormattingEnabled = true;
            _comboBoxAudioFormat.ItemHeight = 29;
            _comboBoxAudioFormat.Location = new Point(127, 42);
            _comboBoxAudioFormat.Name = "comboBoxAudioFormat";
            _comboBoxAudioFormat.Size = new Size(121, 35);
            _comboBoxAudioFormat.TabIndex = 3;
            _comboBoxAudioFormat.UseSelectable = true;
            
            // 
            // checkBoxExtractAudio
            // 
            _checkBoxExtractAudio.AutoSize = true;
            _checkBoxExtractAudio.FontSize = MetroCheckBoxSize.Tall;
            _checkBoxExtractAudio.ForeColor = Color.Black;
            _checkBoxExtractAudio.Location = new Point(3, 14);
            _checkBoxExtractAudio.Name = "checkBoxExtractAudio";
            _checkBoxExtractAudio.Size = new Size(130, 25);
            _checkBoxExtractAudio.TabIndex = 2;
            _checkBoxExtractAudio.Text = "Extract audio";
            _checkBoxExtractAudio.UseSelectable = true;
            _checkBoxExtractAudio.CheckedChanged += CheckBoxExtractAudio_CheckedChanged;
            _tabPageSettings.ResumeLayout(false);
            _tabPageSettings.PerformLayout();
            
            metroTabControl.SuspendLayout();
            metroTabControl.Controls.Add(_tabPageSettings);
            metroTabControl.ResumeLayout(false);
            metroTabControl.PerformLayout();
        }

        public Track()
        {
            InitializeComponent();
            
            // Select first page
            metroTabControl.SelectedIndex = 0;

            if (!Configuration.Default.LayoutSettings.FetchThumbnail)
            {
                pictureBox.Visible = false;
                pictureBox.Enabled = false;
                textBoxTitle.Location = pictureBox.Location;
            }

            if (Configuration.Default.LayoutSettings.PerTrackSettings)
            {
                AddPerTrackSettings();
            }

            // Set style manager
            AppStyle.SetManager(this);
        }

        public Track(TrackData trackData) : this()
        {
            // Save json
            TrackData = trackData ?? throw new ArgumentNullException(nameof(trackData));
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
            if (TrackData.Thumbnails == null || !Configuration.Default.LayoutSettings.FetchBestThumbnail)
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
                    return ImageTools.Resize(image, pictureBox.ClientSize, tabPageInfo.BackColor);
                }
            }
            catch (Exception ex)
            {
                Invoke(new MethodInvoker(() => MetroMessageBox.Show(this, ex.ToString(), nameof(Exception), MessageBoxButtons.OK, MessageBoxIcon.Error)));
                return null;
            }
        }

        private void SetThumbNail()
        {
            var thumbnail = FetchThumbNail();
            if (thumbnail != null)
            {
                SetProperty(c => _thumbnail = pictureBox.Image = thumbnail);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (TrackData != null)
            {
                textBoxTitle.Font = MetroFonts.Default(16);

                // Set title
                Text = TrackData.Title?.Replace("&", "&&") ?? Resources.Untitled;

                // Set info
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

                if (Configuration.Default.LayoutSettings.FetchThumbnail)
                {
                    Task.Run(SetThumbNail);
                }
            }
            
            if (Configuration.Default.LayoutSettings.PerTrackSettings)
            {
                _checkBoxExtractAudio.AddCheckedBinding(_configuration.DownloadSettings, p => p.ExtractAudio);
                _comboBoxAudioFormat.AddEnumBinding(_configuration.DownloadSettings, p => p.AudioFormat);
                _comboBoxAudioQuality.AddEnumBinding(_configuration.DownloadSettings, p => p.AudioQuality);
                _comboBoxVideoFormat.AddEnumBinding(_configuration.DownloadSettings, p => p.VideoFormat);
            }

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
                var parameters = DownLoadParameters.New;
                if (_configuration.DownloadSettings.ExtractAudio)
                {
                    parameters
                        .ExtractAudio()
                        .AudioFormat(_configuration.DownloadSettings.AudioFormat)
                        .AudioQuality(_configuration.DownloadSettings.AudioQuality);
                }
                else
                {
                    parameters.VideoFormat(_configuration.DownloadSettings.VideoFormat);
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
                    
                SetStatus(Resources.StatusDone);
            }
            catch (Exception ex)
            {
                SetStatus(string.Format(Resources.StatusError, ex.Message));
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
            if (e.Button.HasFlag(MouseButtons.Left) && pictureBox.Image != null)
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
                var color = FormTools.ColorMapping[Configuration.Default.LayoutSettings.ColorStyle];
                using (var pen = new Pen(color, Configuration.Default.LayoutSettings.SelectionWidth))
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
        
        private void CheckBoxExtractAudio_CheckedChanged(object sender, EventArgs e)
        {
            _comboBoxAudioFormat.Enabled = _checkBoxExtractAudio.Checked;
            _comboBoxAudioQuality.Enabled = _checkBoxExtractAudio.Checked;
            _comboBoxVideoFormat.Enabled = !_checkBoxExtractAudio.Checked;
        }

        // IProgress

        public void Download(double progress)
        {
            SetProperty(c =>
            {
                metroLabel.Text = Resources.Downloading;
                metroProgressBar.Value = (int)progress;
            });
        }

        public void FfMpeg(YoutubeDownload.DownloadAction action, double progress)
        {
            SetProperty(c =>
            {
                metroLabel.Text = Resources.StatusConverting;

                switch (action)
                {
                    case YoutubeDownload.DownloadAction.VideoConvertor:
                        metroLabel.Text = Resources.StatusConvertingVideo;
                        break;

                    case YoutubeDownload.DownloadAction.ExtractAudio:
                        metroLabel.Text = Resources.StatusExtractingAudio;
                        break;

                    case YoutubeDownload.DownloadAction.ThumbnailsConvertor:
                        metroLabel.Text = Resources.StatusConvertingThumbnail;
                        break;

                    case YoutubeDownload.DownloadAction.EmbedThumbnail:
                        metroLabel.Text = Resources.StatusEmbeddingThumbnail;
                        break;

                    case YoutubeDownload.DownloadAction.Metadata:
                        metroLabel.Text = Resources.StatusAddingMetadata;
                        break;
                }

                metroProgressBar.Value = 100;
            });
        }

        private void ControlResize(object sender, EventArgs e)
        {
            //metroTabControl.Width = Width - 25;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (!Configuration.Default.DownloadSettings.ExtractAudio)
            {
                return;
            }

            if (pictureBox.Image == null)
            {
                Play?.Invoke(this, e);
            }
        }
        
        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (Configuration.Default.DownloadSettings.ExtractAudio)
            {
                pictureBox.Image = null;
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (!Configuration.Default.DownloadSettings.ExtractAudio)
            {
                return;
            }

            if (_thumbnail != null)
            {
                pictureBox.Image = _thumbnail;
            }
        }
    }
}
