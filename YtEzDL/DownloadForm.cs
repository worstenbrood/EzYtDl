using System;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;

namespace YtEzDL
{
    public partial class DownloadForm : MetroForm
    {
        private readonly JObject _json;
        private readonly YoutubeDl _youtubeDl;
        private readonly NotifyIcon _notifyIcon;

        public DownloadForm(JObject json, YoutubeDl youtubeDl, NotifyIcon notifyIcon)
        {
            _json = json;
            _youtubeDl = youtubeDl;
            _notifyIcon = notifyIcon;

            InitializeComponent();
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="box"></param>
        /// <returns>The resized image.</returns>
        private Bitmap ResizeImage(Image image, Size box)
        {
            var size = new Size();

            if (image.Height > box.Height)
            {
                var ratio = (double)image.Height / box.Height;
                size.Height = box.Height;
                size.Width = (int)(image.Width / ratio);
            }

            if (image.Width > box.Width)
            {
                var ratio = (double)image.Width / box.Width;
                size.Width = box.Width;
                size.Height = (int)(image.Height / ratio);
            }
            
            return new Bitmap(image, size.Width, size.Height);
        }

        private void LoadThumbNail()
        {
            var thumbnail = _json["thumbnail"];
            if (thumbnail != null)
            {
                var request = WebRequest.Create(thumbnail.ToString());
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        pictureBox.Image = ResizeImage(Image.FromStream(stream), pictureBox.Size);
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // Set title
            Text = _json["title"].ToString();
            
            // Set info
            textBoxTitle.Font = MetroFonts.Subtitle;
            textBoxTitle.Text = Text + Environment.NewLine + _json["webpage_url"];

            // Add duration
            var duration = _json["duration"];
            if (duration != null)
            {
                var timespan = TimeSpan.FromSeconds(Convert.ToDouble(duration));
                textBoxTitle.Text += Environment.NewLine + timespan.ToString(@"hh\:mm\:ss");
            }
            
            // Do this threaded
            var thread = new Thread(() =>
            {
                // Load thumbnail
                pictureBox.BeginInvoke(new MethodInvoker(LoadThumbNail));

                // Show notification
                BeginInvoke(new MethodInvoker(() =>
                    _notifyIcon.ShowBalloonTip(10000, _json["extractor"].ToString(), Text, ToolTipIcon.None)));
            });
            thread.Start();

            base.OnLoad(e);
        }

        private void Download_Click(object sender, EventArgs e)
        {
            _youtubeDl.Download(_json["webpage_url"].ToString());
        }

        private void metroButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
