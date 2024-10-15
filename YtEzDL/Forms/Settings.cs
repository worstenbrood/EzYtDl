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
            textBoxPath.DataBindings.Add("Text", Configuration.Default.FileSettings, "Path");
            checkBoxCreatePlaylistFolder.DataBindings.Add("Checked", Configuration.Default.FileSettings,
                "CreatePlaylistFolder");
            checkBoxExtractAudio.DataBindings.Add("Checked", Configuration.Default.DownloadSettings, "ExtractAudio");
            checkBoxAddMetadata.DataBindings.Add("Checked", Configuration.Default.DownloadSettings, "AddMetadata");
            checkBoxEmbedThumbnail.DataBindings.Add("Checked", Configuration.Default.DownloadSettings, "EmbedThumbnail");
            comboBoxAudioFormat.DataSource = Enum.GetValues(typeof(AudioFormat));
            comboBoxAudioFormat.DataBindings.Add("SelectedItem", Configuration.Default.DownloadSettings, "AudioFormat");
            comboBoxAudioQuality.DataSource = Enum.GetValues(typeof(AudioQuality));
            comboBoxAudioQuality.DataBindings.Add("SelectedItem", Configuration.Default.DownloadSettings, "AudioQuality");
            comboBoxVideoFormat.DataSource = Enum.GetValues(typeof(VideoFormat));
            comboBoxVideoFormat.DataBindings.Add("SelectedItem", Configuration.Default.DownloadSettings, "VideoFormat");
            checkBoxFetchThumbnail.DataBindings.Add("Checked", Configuration.Default.DownloadSettings, "FetchThumbnail");
            checkBoxFetchBestThumbnail.DataBindings.Add("Checked", Configuration.Default.DownloadSettings, "FetchBestThumbnail");
            checkBoxAutoSelect.DataBindings.Add("Checked", Configuration.Default.LayoutSettings, "AutoSelect");

            // Path selector
            textBoxPath.CustomButton.Click += (sender, args) =>
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.SelectedPath = Configuration.Default.FileSettings.Path;
                    var dialog = fbd.ShowDialog(this);
                    if (dialog == DialogResult.OK)
                    {
                        textBoxPath.Text = fbd.SelectedPath;
                    }
                }
            };
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Configuration.Default.Save();
            Close();
        }
    }
}
