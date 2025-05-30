﻿using System;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using YtEzDL.Config;
using YtEzDL.Properties;
using YtEzDL.Utils;

namespace YtEzDL.Forms
{
    public partial class Settings : MetroForm
    {
        private readonly Configuration _configuration = new Configuration();
        private readonly MetroColorStyle _currentStyle = AppStyle.Manager.Style;

        public Settings()
        {
            InitializeComponent();
                                    
            Icon = Resources.YTIcon;

            // Select first page
            tabControl.SelectedIndex = 0;
            ToggleAdvancedTab(Configuration.Default.ApplicationSettings.AdvancedSettings);
                        
            // Set style manager
            AppStyle.SetManager(this);
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
            checkBoxFetchThumbnail.AddCheckedBinding(_configuration.LayoutSettings, p => p.FetchThumbnail);
            checkBoxFetchBestThumbnail.AddCheckedBinding(_configuration.LayoutSettings, p => p.FetchBestThumbnail);
            checkBoxAutoSelect.AddCheckedBinding(_configuration.LayoutSettings, p => p.AutoSelect);
            comboBoxThreads.AddRangeBinding(_configuration.DownloadSettings, p => p.DownloadThreads, 1, Environment.ProcessorCount);
            checkBoxPerTrackSettings.AddCheckedBinding(_configuration.LayoutSettings, p => p.PerTrackSettings);
            checkBoxAutostart.AddCheckedBinding(_configuration.ApplicationSettings, p => p.Autostart);
            checkBoxYoutubeFastFetch.AddCheckedBinding(_configuration.LayoutSettings, p => p.YoutubeFastFetch);
            comboBoxColorStyle.AddEnumBinding(_configuration.LayoutSettings, p => p.ColorStyle);
            AppStyle.SetStyle(_configuration.LayoutSettings.ColorStyle);
            comboBoxSelectionWidth.AddRangeBinding(_configuration.LayoutSettings, p => p.SelectionWidth, 1, 10);
            comboBoxUpdateChannel.AddEnumBinding(_configuration.AdvancedSettings, p => p.UpdateChannel);
            checkBoxAdvanced.AddCheckedBinding(_configuration.ApplicationSettings, p => p.AdvancedSettings);
            checkBoxHistory.AddCheckedBinding(_configuration.ApplicationSettings, p => p.EnableHistory);

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
        
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            // Revert
            Configuration.Default.LayoutSettings.ColorStyle = _currentStyle;
            if (AppStyle.Manager.Style != _currentStyle)
            {
                AppStyle.SetStyle(_currentStyle);
                AppStyle.RefreshActiveForms();
            }
            Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Save to file
            Configuration.Default.Save(_configuration);

            // Reload default
            Configuration.Default.Load();

            // Apply autostart
            CommonTools.SetAutoStart(Configuration.Default.ApplicationSettings.Autostart);

            // Close form
            Close();
        }

        private void CheckBoxExtractAudio_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAudioFormat.Enabled = checkBoxExtractAudio.Checked;
            comboBoxAudioQuality.Enabled = checkBoxExtractAudio.Checked;
            comboBoxVideoFormat.Enabled = !checkBoxExtractAudio.Checked;
        }

        private void CheckBoxFetchThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxFetchBestThumbnail.Enabled = checkBoxFetchThumbnail.Checked;
        }

        private void ComboBoxColorStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change style
            var style = (MetroColorStyle)comboBoxColorStyle.SelectedItem;

            // To make selection work
            Configuration.Default.LayoutSettings.ColorStyle = style;

            if (AppStyle.Manager.Style != style)
            {
                AppStyle.SetStyle(style);
                AppStyle.RefreshActiveForms();
            }
        }

        private void ToggleAdvancedTab(bool show)
        {
            var exists = tabControl.Controls.Contains(tabPageAdvanced);
            switch (show)
            {
                case true when !exists:
                    tabControl.Controls.Add(tabPageAdvanced);
                    break;
                case false when exists:
                    tabControl.Controls.Remove(tabPageAdvanced);
                    break;
            }
        }

        private void CheckBoxAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            ToggleAdvancedTab(checkBoxAdvanced.Checked);
        }
    }
}
