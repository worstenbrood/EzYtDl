using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class Settings : MetroForm
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set data bindings
            metroTextBoxPath.DataBindings.Add("Text", Configuration.Default.FileSettings, "Path");

            // Path selector
            metroTextBoxPath.CustomButton.Click += (sender, args) =>
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    var dialog = fbd.ShowDialog(this);
                    if (dialog == DialogResult.OK)
                    {
                        metroTextBoxPath.Text = fbd.SelectedPath;
                    }
                }
            };
        }
    }
}
