using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using YtEzDL.DownLoad;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class About : MetroForm
    {
        public About()
        {
            InitializeComponent();
        }

        private volatile bool _loading;

        protected override void OnLoad(EventArgs e)
        {
            textBoxAbout.Text += "written by worstenbrood (worstenbrood@gmail.com)" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;
            textBoxAbout.Text += $"WebP version: {ImageTools.WebP.GetVersion()}" + Environment.NewLine;
            textBoxAbout.Text += "https://chromium.googlesource.com/webm/libwebp" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/JosePineiro/WebP-wrapper" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;

            Task.Run(() =>
            {
                _loading = true;

                var version = new YoutubeDownload().GetVersion();
                
                Invoke(new MethodInvoker(() =>
                {
                    textBoxAbout.Text += $"yt-dlp version: {version}" + Environment.NewLine;
                    textBoxAbout.Text += "https://github.com/yt-dlp/yt-dlp" + Environment.NewLine;
                    textBoxAbout.Text += "https://github.com/FFmpeg/FFmpeg" + Environment.NewLine;
                    Height = textBoxAbout.Height + 75;
                }));

                _loading = false;
            });

            base.OnLoad(e);
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_loading)
            {
                e.Cancel = true;
            }
        }
    }
}
