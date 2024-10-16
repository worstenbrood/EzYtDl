using System;
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

        protected override void OnLoad(EventArgs e)
        {
            textBoxAbout.Text += "written by worstenbrood (worstenbrood@gmail.com)" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;
            textBoxAbout.Text += $"WebP version: {ImageTools.WebP.GetVersion()}" + Environment.NewLine;
            textBoxAbout.Text += "https://chromium.googlesource.com/webm/libwebp" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/JosePineiro/WebP-wrapper" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;
            textBoxAbout.Text += $"yt-dlp version: {new YoutubeDownload().GetVersion()}" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/yt-dlp/yt-dlp" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/FFmpeg/FFmpeg" + Environment.NewLine;
            Height = textBoxAbout.Height + 100;
            base.OnLoad(e);
        }
    }
}
