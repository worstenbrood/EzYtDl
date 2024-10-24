using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using YtEzDL.Properties;
using YtEzDL.Tools;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class About : MetroForm
    {
        public About()
        {
            InitializeComponent();
            Icon = Resources.YTIcon;
            textBoxAbout.Font = MetroFonts.Default(16);

            // Set style manager
            AppStyle.SetManager(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            textBoxAbout.SuspendLayout();
            
            textBoxAbout.Text += string.Format(Resources.EzYtDlVersion, CommonTools.ApplicationProductVersion) + Environment.NewLine;
            textBoxAbout.Text += Resources.EzYtDlCredits + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;

            textBoxAbout.Text += string.Format(Resources.MetroFrameworkVersion, CommonTools.GetVersion<MetroForm>()) + Environment.NewLine;
            textBoxAbout.Text += Resources.MetroFrameworkUrl + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;

            textBoxAbout.Text += string.Format(Resources.WebPVersion, ImageTools.WebP.GetVersion()) + Environment.NewLine;
            textBoxAbout.Text += Resources.WebPUrl + Environment.NewLine;
            textBoxAbout.Text += Resources.WebPWrapperUrl + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;

            textBoxAbout.Text += string.Format(Resources.YtDlpVersion, new YoutubeDownload().GetVersion()) + Environment.NewLine;
            textBoxAbout.Text += Resources.YtDlpUrl + Environment.NewLine;
            textBoxAbout.Text += Environment.NewLine;
            textBoxAbout.ResumeLayout();

            Task.Run(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    textBoxAbout.Text += $"{new FfMpeg().GetVersion()}" + Environment.NewLine;
                    textBoxAbout.Text += Resources.FfMpegUrl;
                    textBoxAbout.Select(textBoxAbout.Text.Length, 0);
                }));
            });          
        }
        
        public Rectangle GetScreen()
        {
            return Screen.FromControl(this).Bounds;
        }

        private void TextBoxAbout_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAbout.ScrollBars != ScrollBars.Vertical)
            {
                var textHeight = textBoxAbout.GetTextHeight();
                var screen = GetScreen();
                var total = Location.Y + textBoxAbout.Location.Y * 2 + textHeight; 

                if (total > screen.Height)
                {
                    Height = screen.Height - Location.Y + textBoxAbout.Location.Y;
                    textBoxAbout.Height = Height - textBoxAbout.Location.Y - 25;
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

        private void TextBoxAbout_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            Win32.HideCaret(textBoxAbout.Handle);
        }
    }
}
