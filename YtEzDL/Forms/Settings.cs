using System;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class Settings : MetroForm
    {
        private readonly Configuration _configuration = new Configuration(Configuration.Default.Filename);

        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set data bindings
            textBoxPath.DataBindings.Add("Text", _configuration.FileSettings, "Path");
            checkBoxCreatePlaylistFolder.DataBindings.Add("Checked", _configuration.FileSettings,
                "CreatePlaylistFolder");
            checkBoxExtractAudio.DataBindings.Add("Checked", _configuration.DownloadSettings, "ExtractAudio");
            checkBoxAddMetadata.DataBindings.Add("Checked", _configuration.DownloadSettings, "AddMetadata");
            checkBoxEmbedThumbnail.DataBindings.Add("Checked", _configuration.DownloadSettings, "EmbedThumbnail");
            comboBoxAudioFormat.DataSource = Enum.GetValues(typeof(AudioFormat));
            comboBoxAudioFormat.DataBindings.Add("SelectedItem", _configuration.DownloadSettings, "AudioFormat");
            comboBoxAudioQuality.DataSource = Enum.GetValues(typeof(AudioQuality));
            comboBoxAudioQuality.DataBindings.Add("SelectedItem", _configuration.DownloadSettings, "AudioQuality");
            comboBoxVideoFormat.DataSource = Enum.GetValues(typeof(VideoFormat));
            comboBoxVideoFormat.DataBindings.Add("SelectedItem", _configuration.DownloadSettings, "VideoFormat");
            checkBoxFetchThumbnail.DataBindings.Add("Checked", _configuration.DownloadSettings, "FetchThumbnail");
            checkBoxFetchBestThumbnail.DataBindings.Add("Checked", _configuration.DownloadSettings, "FetchBestThumbnail");
            checkBoxAutoSelect.DataBindings.Add("Checked", _configuration.LayoutSettings, "AutoSelect");
            comboBoxThreads.DataSource = Enumerable.Range(1, Environment.ProcessorCount).ToArray();
            comboBoxThreads.DataBindings.Add("SelectedItem", _configuration.DownloadSettings, "DownloadThreads");

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
            _configuration.Save();
            Close();
        }
    }
}
