using System;
using System.Windows.Forms;
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

        protected override void OnLoad(EventArgs e)
        {
            // Set title
            Text = _json["title"].ToString();

            // Show notification
            _notifyIcon.ShowBalloonTip(10000, _json["extractor"].ToString(), _json["title"].ToString(), ToolTipIcon.None);

            base.OnLoad(e);
        }

        private void Download_Click(object sender, EventArgs e)
        {
            _youtubeDl.Download(_json["webpage_url"].ToString());
        }
    }
}
