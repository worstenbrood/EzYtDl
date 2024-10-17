using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using YtEzDL.Properties;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class About : MetroForm
    {
        public About()
        {
            InitializeComponent();
            textBoxAbout.Font = MetroFonts.Default(16);
            Icon = Resources.YTIcon;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            textBoxAbout.SuspendLayout();
            textBoxAbout.Text += $"ezytdl version: {Assembly.GetExecutingAssembly().GetName().Version}" + Environment.NewLine;
            textBoxAbout.Text += "written by worstenbrood (worstenbrood@gmail.com)" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;

            textBoxAbout.Text += $"MetroFramework version: {Assembly.GetAssembly(typeof(MetroForm)).GetName().Version}" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/thielj/MetroFramework" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;

            textBoxAbout.Text += $"WebP version: {ImageTools.WebP.GetVersion()}" + Environment.NewLine;
            textBoxAbout.Text += "https://chromium.googlesource.com/webm/libwebp" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/JosePineiro/WebP-wrapper" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;
            textBoxAbout.Text += $"{Tools.GetFfMpegVersion()}" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/FFmpeg/FFmpeg" + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;

            textBoxAbout.Text += $"yt-dlp version: {Tools.GetYtDlpVersion()}" + Environment.NewLine;
            textBoxAbout.Text += "https://github.com/yt-dlp/yt-dlp";

            textBoxAbout.Select(textBoxAbout.Text.Length, 0);
            textBoxAbout.ResumeLayout();
        }
        
        public Rectangle GetScreen()
        {
            return Screen.FromControl(this).Bounds;
        }

        private void textBoxAbout_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAbout.ScrollBars != ScrollBars.Vertical)
            {
                var textHeight = textBoxAbout.GetTextHeight();
                var screen = GetScreen();
                var total = Location.Y + textBoxAbout.Location.Y * 2 + textHeight; 

                if (total > screen.Height)
                {
                    Height = screen.Height - Location.Y + textBoxAbout.Location.Y;
                    textBoxAbout.Height = Height - textBoxAbout.Location.Y * 2;
                    textBoxAbout.ScrollBars = ScrollBars.Vertical;
                }
                else
                {
                    Height = textHeight + textBoxAbout.Location.Y * 2;
                    textBoxAbout.Height = textHeight;
                }

                var centerX = (screen.Width - Width) / 2;
                var centerY = (screen.Height - Height) / 2;
                Location = new Point(centerX, centerY);
            }

            textBoxAbout.Select(textBoxAbout.Text.Length, 0);
            Win32.HideCaret(textBoxAbout.Handle);
        }

        private void textBoxAbout_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            Win32.HideCaret(textBoxAbout.Handle);
        }
    }
}
