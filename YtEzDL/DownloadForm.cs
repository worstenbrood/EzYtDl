using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;

namespace YtEzDL
{
    public partial class DownloadForm : MetroForm
    {
        private readonly List<JObject> _json;
        private readonly NotifyIcon _notifyIcon;
        private readonly YoutubeDl _youtubeDl = new YoutubeDl();
        private readonly AutoResetEvent _downloadEvent = new AutoResetEvent(false);
        
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetForegroundWindow(IntPtr hWnd);
        
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

        private Image GetThumbNail(JObject json, Size size)
        {
            var thumbnail = json["thumbnail"];
            if (thumbnail != null)
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

            return null;
        }

        private void LoadThumbNail(int index = 0)
        {
            var thumbnail = GetThumbNail(_json[index], pictureBox.Size);
            if (thumbnail != null)
            {
                pictureBox.Image = thumbnail;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // Set title
            Text = _json[0]["title"].ToString();
            
            // Set info
            textBoxTitle.Font = MetroFonts.Subtitle;
            textBoxTitle.Text = Text + Environment.NewLine + _json[0]["webpage_url"];

            // Add duration
            var duration = _json[0]["duration"];
            if (duration != null)
            {
                var timespan = TimeSpan.FromSeconds(Convert.ToDouble(duration));
                textBoxTitle.Text += Environment.NewLine + timespan.ToString(@"hh\:mm\:ss");
            }
            
            // Do this threaded
            var thread = new Thread(() =>
            {
                // Load thumbnail
                pictureBox.BeginInvoke(new MethodInvoker(() => LoadThumbNail()));

                // Show notification
                BeginInvoke(new MethodInvoker(() => _notifyIcon.ShowBalloonTip(10000, _json[0]["extractor"].ToString(), Text, ToolTipIcon.None)));
            });
            thread.Start();

            // Base
            base.OnLoad(e);

            // Set foregroundwindow
            SetForegroundWindow(Handle);
        }

        private void StartDownload()
        {
            var downloadThread = new Thread(() =>
            {
                try
                {
                    // Already running
                    if (_downloadEvent.WaitOne(1))
                        return;

                    _downloadEvent.Set();

                    // Set buttons
                    Invoke(new MethodInvoker(() =>
                    {
                        metroButtonCancel.Enabled = true;
                        metroButtonDownload.Enabled = false;
                    }));

                    // Start download
                    _youtubeDl.Download(_json[0]["webpage_url"].ToString(), new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName,
                        progress => metroProgressBar.Invoke(new MethodInvoker(() => metroProgressBar.Value = (int)progress)));
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
                    }));

                    _downloadEvent.Reset();
                }
            });
            downloadThread.Start();
        }

        private void StopDownLoad()
        {
            var stopThread = new Thread(() =>
            {
                // Stop youtube-dl
                _youtubeDl.Cancel(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, _json[0]["_filename"].ToString());

                // Wait for finish
                _downloadEvent.WaitOne();

                // Reset progressbar
                Invoke(new MethodInvoker(() => metroProgressBar.Value = 0));
            });

            stopThread.Start();
        }
        
        private void MetroButtonDownload_Click(object sender, EventArgs e)
        {
            StartDownload();
        }

        private void MetroButtonCancel_Click(object sender, EventArgs e)
        {
            StopDownLoad();
        }
    }
}
