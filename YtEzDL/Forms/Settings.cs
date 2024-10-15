using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using YtEzDL.Config;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class Settings : MetroForm
    {
        private readonly Configuration _configuration = new Configuration();

        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Load current config
            Configuration.Default.Load(_configuration);

            // Set data bindings
            textBoxPath.AddTextBinding(_configuration.FileSettings,p => p.Path);
            checkBoxCreatePlaylistFolder.AddCheckedBinding(_configuration.FileSettings, p => p.CreatePlaylistFolder);
            checkBoxExtractAudio.AddCheckedBinding(_configuration.DownloadSettings, p => p.ExtractAudio);
            checkBoxAddMetadata.AddCheckedBinding(_configuration.DownloadSettings, p => p.AddMetadata);
            checkBoxEmbedThumbnail.AddCheckedBinding(_configuration.DownloadSettings, p => p.EmbedThumbnail);
            comboBoxAudioFormat.AddEnumBinding(_configuration.DownloadSettings, p => p.AudioFormat);
            comboBoxAudioQuality.AddEnumBinding(_configuration.DownloadSettings, p => p.AudioQuality);
            comboBoxVideoFormat.AddEnumBinding(_configuration.DownloadSettings, p => p.VideoFormat);
            checkBoxFetchThumbnail.AddCheckedBinding(_configuration.DownloadSettings, p => p.FetchThumbnail);
            checkBoxFetchBestThumbnail.AddCheckedBinding(_configuration.DownloadSettings, p => p.FetchBestThumbnail);
            checkBoxAutoSelect.AddCheckedBinding(_configuration.LayoutSettings, p => p.AutoSelect);
            comboBoxThreads.AddRangeBinding(_configuration.DownloadSettings, p => p.DownloadThreads, 1, Environment.ProcessorCount);
           
            // Path selector
            textBoxPath.CustomButton.Click += (sender, args) =>
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.SelectedPath = _configuration.FileSettings.Path;
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
            // Save to file
            Configuration.Default.Save(_configuration);

            // Reload default
            Configuration.Default.Load();
            Close();
        }

        private void checkBoxExtractAudio_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAudioFormat.Enabled = checkBoxExtractAudio.Checked;
            comboBoxAudioQuality.Enabled = checkBoxExtractAudio.Checked;
            comboBoxVideoFormat.Enabled = !checkBoxExtractAudio.Checked;
        }

        private void checkBoxFetchThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxFetchBestThumbnail.Enabled = checkBoxFetchThumbnail.Checked;
        }
    }
}
