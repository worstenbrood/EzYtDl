using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;

namespace YtEzDL
{
    public partial class DownloadForm : MetroForm, IProgress
    {
        private readonly List<JObject> _json;
        private readonly NotifyIcon _notifyIcon;
        private readonly YoutubeDownload _youtubeDl = new YoutubeDownload();
        private static readonly string DirectoryName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public DownloadForm(List<JObject> json, NotifyIcon notifyIcon)
        {
            _json = json;
            _notifyIcon = notifyIcon;

            InitializeComponent();

            metroButtonCancel.Enabled = false;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="boxSize"></param>
        /// <returns>The resized image.</returns>
        public Bitmap ResizeImage(Image image, Size boxSize)
        {
            // Figure out the ratio
            double ratioX = (double)boxSize.Width / (double)image.Width;
            double ratioY = (double)boxSize.Height / (double)image.Height;
            // Use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // Now we can get the new height and width
            int newHeight = Convert.ToInt32(image.Height * ratio);
            int newWidth = Convert.ToInt32(image.Width * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((boxSize.Width - (image.Width * ratio)) / 2);
            int posY = Convert.ToInt32((boxSize.Height - (image.Height * ratio)) / 2);

            var destRect = new Rectangle(posX, posY, newWidth, newHeight);
            var destImage = new Bitmap(boxSize.Width, boxSize.Height, PixelFormat.Format24bppRgb);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.Clear(tabPageInfo.BackColor);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private Image DownloadThumbNail(JObject json, Size size)
        {
            var thumbnail = json["thumbnail"];
            if (thumbnail != null)
            {
                try
                {
                    var request = WebRequest.Create(thumbnail.ToString());
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            return ResizeImage(Image.FromStream(stream), size);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }

        private Image GetThumbNail(int index = 0)
        {
            return DownloadThumbNail(_json[index], pictureBox.Size);
        }

        private void SetThumbNail()
        {
            var thumbnail = GetThumbNail();

            BeginInvoke(new MethodInvoker(() =>
            {
                // Set thumbnail
                if (thumbnail != null)
                {
                    pictureBox.Image = thumbnail;
                }

                // Show notification
                _notifyIcon.ShowBalloonTip(10000, _json[0]["extractor"].ToString(), Text, ToolTipIcon.None);
            }));
        }

        protected override void OnLoad(EventArgs e)
        {
            // Set title
            Text = _json[0]["title"].Value<string>().Replace("&", "&&");
            
            // Set info
            textBoxTitle.Font = MetroFonts.Subtitle;
            textBoxTitle.Text = _json[0]["title"] + Environment.NewLine + _json[0]["webpage_url"];

            // Add duration
            var duration = _json[0]["duration"];
            if (duration != null)
            {
                var timespan = TimeSpan.FromSeconds(Convert.ToDouble(duration));
                textBoxTitle.Text += Environment.NewLine + timespan.ToString(@"hh\:mm\:ss");
            }

            // Add upload date
            var uploadDate = _json[0]["upload_date"];
            if (uploadDate != null)
            {
                var date = DateTime.ParseExact(uploadDate.ToString(), "yyyyMMdd", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None);
                textBoxTitle.Text += Environment.NewLine + date.ToString("D");
            }

            Task.Run(() => SetThumbNail());

            // Base
            base.OnLoad(e);

            // Set foregroundwindow
            SetForegroundWindow(Handle);
            Activate();
        }

        private void StartDownload()
        {
            try
            {
                // Set buttons
                Invoke(new MethodInvoker(() =>
                {
                    metroButtonCancel.Enabled = true;
                    metroButtonDownload.Enabled = false;
                }));

                // Start download
                _youtubeDl
                    .Reset()
                    .ExtractAudio()
                    .AddMetadata()
                    .EmbedThumbnail()
                    .AudioFormat(AudioFormat.Mp3)
                    .AudioQuality(AudioQuality.Best)
                    .Download(_json[0]["webpage_url"].Value<string>(), DirectoryName, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Set buttons
                Invoke(new MethodInvoker(() =>
                {
                    metroButtonCancel.Enabled = false;
                    metroButtonDownload.Enabled = true;
                    metroProgressBar.Value = 0;
                    metroLabelAction.Text = "Finished";
                }));
            }
        }

        private void StopDownLoad()
        {
            // Stop youtube-dl
            _youtubeDl.Cancel(DirectoryName, _json[0]["_filename"].Value<string>());
        }
        
        private void MetroButtonDownload_Click(object sender, EventArgs e)
        {
            Task.Run(() => StartDownload());
        }

        private void MetroButtonCancel_Click(object sender, EventArgs e)
        {
            Task.Run(() => StopDownLoad());
        }

        private void TextBoxTitle_GotFocus(object sender, EventArgs e)
        {
            // Hide editbox caret
            HideCaret(textBoxTitle.Handle);
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.WindowsShutDown || _youtubeDl.IsRunning();
        }

        public void Download(double progress)
        {
            Invoke(new MethodInvoker(() =>
            {
                metroLabelAction.Text = "Downloading...";
                metroProgressBar.Value = (int)progress;
            }));
        }

        public void FfMpeg(double progress)
        {
            Invoke(new MethodInvoker(() =>
            {
                metroLabelAction.Text = "Converting...";
                metroProgressBar.Value = 100;
            }));
        }
    }
}
