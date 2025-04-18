﻿using MetroFramework;
using Resources = YtEzDL.Properties.Resources;

namespace YtEzDL.Forms
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxPath = new MetroFramework.Controls.MetroTextBox();
            this.labelPath = new MetroFramework.Controls.MetroLabel();
            this.checkBoxExtractAudio = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxCreatePlaylistFolder = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxAddMetadata = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxEmbedThumbnail = new MetroFramework.Controls.MetroCheckBox();
            this.comboBoxAudioFormat = new MetroFramework.Controls.MetroComboBox();
            this.labelAudioFormat = new MetroFramework.Controls.MetroLabel();
            this.labelAudioQuality = new MetroFramework.Controls.MetroLabel();
            this.comboBoxAudioQuality = new MetroFramework.Controls.MetroComboBox();
            this.labelVideoFormat = new MetroFramework.Controls.MetroLabel();
            this.comboBoxVideoFormat = new MetroFramework.Controls.MetroComboBox();
            this.checkBoxFetchThumbnail = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxFetchBestThumbnail = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxAutoSelect = new MetroFramework.Controls.MetroCheckBox();
            this.buttonCancel = new MetroFramework.Controls.MetroButton();
            this.buttonSave = new MetroFramework.Controls.MetroButton();
            this.labelThreads = new MetroFramework.Controls.MetroLabel();
            this.comboBoxThreads = new MetroFramework.Controls.MetroComboBox();
            this.checkBoxPerTrackSettings = new MetroFramework.Controls.MetroCheckBox();
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPageFileSettings = new MetroFramework.Controls.MetroTabPage();
            this.tabPageDownloadSettings = new MetroFramework.Controls.MetroTabPage();
            this.tabPageLayoutSettings = new MetroFramework.Controls.MetroTabPage();
            this.labelSelectionWidth = new MetroFramework.Controls.MetroLabel();
            this.comboBoxSelectionWidth = new MetroFramework.Controls.MetroComboBox();
            this.labelColorStyle = new MetroFramework.Controls.MetroLabel();
            this.comboBoxColorStyle = new MetroFramework.Controls.MetroComboBox();
            this.checkBoxYoutubeFastFetch = new MetroFramework.Controls.MetroCheckBox();
            this.tabPageApplication = new MetroFramework.Controls.MetroTabPage();
            this.checkBoxHistory = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxAdvanced = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxAutostart = new MetroFramework.Controls.MetroCheckBox();
            this.tabPageAdvanced = new MetroFramework.Controls.MetroTabPage();
            this.labelUpdateChannel = new MetroFramework.Controls.MetroLabel();
            this.comboBoxUpdateChannel = new MetroFramework.Controls.MetroComboBox();
            this.tabControl.SuspendLayout();
            this.tabPageFileSettings.SuspendLayout();
            this.tabPageDownloadSettings.SuspendLayout();
            this.tabPageLayoutSettings.SuspendLayout();
            this.tabPageApplication.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxPath.CustomButton.Image = null;
            this.textBoxPath.CustomButton.Location = new System.Drawing.Point(527, 2);
            this.textBoxPath.CustomButton.Name = "";
            this.textBoxPath.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.textBoxPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxPath.CustomButton.TabIndex = 1;
            this.textBoxPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxPath.CustomButton.UseSelectable = true;
            this.textBoxPath.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.textBoxPath.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.textBoxPath.ForeColor = System.Drawing.Color.Black;
            this.textBoxPath.Lines = new string[0];
            this.textBoxPath.Location = new System.Drawing.Point(68, 15);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxPath.MaxLength = 32767;
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.PasswordChar = '\0';
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxPath.SelectedText = "";
            this.textBoxPath.SelectionLength = 0;
            this.textBoxPath.SelectionStart = 0;
            this.textBoxPath.ShortcutsEnabled = true;
            this.textBoxPath.ShowButton = true;
            this.textBoxPath.Size = new System.Drawing.Size(555, 30);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.UseSelectable = true;
            this.textBoxPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.BackColor = System.Drawing.Color.White;
            this.labelPath.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelPath.ForeColor = System.Drawing.Color.Black;
            this.labelPath.Location = new System.Drawing.Point(4, 16);
            this.labelPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(49, 25);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Path:";
            // 
            // checkBoxExtractAudio
            // 
            this.checkBoxExtractAudio.AutoSize = true;
            this.checkBoxExtractAudio.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxExtractAudio.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxExtractAudio.ForeColor = System.Drawing.Color.Black;
            this.checkBoxExtractAudio.Location = new System.Drawing.Point(8, 13);
            this.checkBoxExtractAudio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxExtractAudio.Name = "checkBoxExtractAudio";
            this.checkBoxExtractAudio.Size = new System.Drawing.Size(126, 25);
            this.checkBoxExtractAudio.TabIndex = 2;
            this.checkBoxExtractAudio.Text = "Extract audio";
            this.checkBoxExtractAudio.UseSelectable = true;
            this.checkBoxExtractAudio.CheckedChanged += new System.EventHandler(this.CheckBoxExtractAudio_CheckedChanged);
            // 
            // checkBoxCreatePlaylistFolder
            // 
            this.checkBoxCreatePlaylistFolder.AutoSize = true;
            this.checkBoxCreatePlaylistFolder.BackColor = System.Drawing.Color.White;
            this.checkBoxCreatePlaylistFolder.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxCreatePlaylistFolder.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxCreatePlaylistFolder.ForeColor = System.Drawing.Color.Black;
            this.checkBoxCreatePlaylistFolder.Location = new System.Drawing.Point(9, 58);
            this.checkBoxCreatePlaylistFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxCreatePlaylistFolder.Name = "checkBoxCreatePlaylistFolder";
            this.checkBoxCreatePlaylistFolder.Size = new System.Drawing.Size(182, 25);
            this.checkBoxCreatePlaylistFolder.TabIndex = 3;
            this.checkBoxCreatePlaylistFolder.Text = "Create playlist folder";
            this.checkBoxCreatePlaylistFolder.UseSelectable = true;
            // 
            // checkBoxAddMetadata
            // 
            this.checkBoxAddMetadata.AutoSize = true;
            this.checkBoxAddMetadata.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxAddMetadata.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxAddMetadata.Location = new System.Drawing.Point(8, 44);
            this.checkBoxAddMetadata.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxAddMetadata.Name = "checkBoxAddMetadata";
            this.checkBoxAddMetadata.Size = new System.Drawing.Size(135, 25);
            this.checkBoxAddMetadata.TabIndex = 4;
            this.checkBoxAddMetadata.Text = "Add metadata";
            this.checkBoxAddMetadata.UseSelectable = true;
            // 
            // checkBoxEmbedThumbnail
            // 
            this.checkBoxEmbedThumbnail.AutoSize = true;
            this.checkBoxEmbedThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxEmbedThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxEmbedThumbnail.Location = new System.Drawing.Point(8, 75);
            this.checkBoxEmbedThumbnail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxEmbedThumbnail.Name = "checkBoxEmbedThumbnail";
            this.checkBoxEmbedThumbnail.Size = new System.Drawing.Size(163, 25);
            this.checkBoxEmbedThumbnail.TabIndex = 5;
            this.checkBoxEmbedThumbnail.Text = "Embed thumbnail";
            this.checkBoxEmbedThumbnail.UseSelectable = true;
            // 
            // comboBoxAudioFormat
            // 
            this.comboBoxAudioFormat.BackColor = System.Drawing.Color.White;
            this.comboBoxAudioFormat.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxAudioFormat.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.comboBoxAudioFormat.ForeColor = System.Drawing.Color.Black;
            this.comboBoxAudioFormat.FormattingEnabled = true;
            this.comboBoxAudioFormat.ItemHeight = 29;
            this.comboBoxAudioFormat.Location = new System.Drawing.Point(401, 13);
            this.comboBoxAudioFormat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxAudioFormat.Name = "comboBoxAudioFormat";
            this.comboBoxAudioFormat.Size = new System.Drawing.Size(140, 35);
            this.comboBoxAudioFormat.TabIndex = 6;
            this.comboBoxAudioFormat.UseSelectable = true;
            // 
            // labelAudioFormat
            // 
            this.labelAudioFormat.AutoSize = true;
            this.labelAudioFormat.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelAudioFormat.Location = new System.Drawing.Point(229, 16);
            this.labelAudioFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAudioFormat.Name = "labelAudioFormat";
            this.labelAudioFormat.Size = new System.Drawing.Size(116, 25);
            this.labelAudioFormat.TabIndex = 7;
            this.labelAudioFormat.Text = "Audio format:";
            // 
            // labelAudioQuality
            // 
            this.labelAudioQuality.AutoSize = true;
            this.labelAudioQuality.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelAudioQuality.Location = new System.Drawing.Point(229, 50);
            this.labelAudioQuality.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAudioQuality.Name = "labelAudioQuality";
            this.labelAudioQuality.Size = new System.Drawing.Size(116, 25);
            this.labelAudioQuality.TabIndex = 9;
            this.labelAudioQuality.Text = "Audio quality:";
            // 
            // comboBoxAudioQuality
            // 
            this.comboBoxAudioQuality.BackColor = System.Drawing.Color.White;
            this.comboBoxAudioQuality.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxAudioQuality.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.comboBoxAudioQuality.ForeColor = System.Drawing.Color.White;
            this.comboBoxAudioQuality.FormattingEnabled = true;
            this.comboBoxAudioQuality.ItemHeight = 29;
            this.comboBoxAudioQuality.Location = new System.Drawing.Point(401, 46);
            this.comboBoxAudioQuality.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxAudioQuality.Name = "comboBoxAudioQuality";
            this.comboBoxAudioQuality.Size = new System.Drawing.Size(140, 35);
            this.comboBoxAudioQuality.TabIndex = 8;
            this.comboBoxAudioQuality.UseSelectable = true;
            // 
            // labelVideoFormat
            // 
            this.labelVideoFormat.AutoSize = true;
            this.labelVideoFormat.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelVideoFormat.Location = new System.Drawing.Point(229, 84);
            this.labelVideoFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVideoFormat.Name = "labelVideoFormat";
            this.labelVideoFormat.Size = new System.Drawing.Size(115, 25);
            this.labelVideoFormat.TabIndex = 11;
            this.labelVideoFormat.Text = "Video format:";
            // 
            // comboBoxVideoFormat
            // 
            this.comboBoxVideoFormat.BackColor = System.Drawing.Color.White;
            this.comboBoxVideoFormat.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxVideoFormat.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.comboBoxVideoFormat.ForeColor = System.Drawing.Color.Black;
            this.comboBoxVideoFormat.FormattingEnabled = true;
            this.comboBoxVideoFormat.ItemHeight = 29;
            this.comboBoxVideoFormat.Location = new System.Drawing.Point(401, 79);
            this.comboBoxVideoFormat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxVideoFormat.Name = "comboBoxVideoFormat";
            this.comboBoxVideoFormat.Size = new System.Drawing.Size(140, 35);
            this.comboBoxVideoFormat.TabIndex = 10;
            this.comboBoxVideoFormat.UseSelectable = true;
            // 
            // checkBoxFetchThumbnail
            // 
            this.checkBoxFetchThumbnail.AutoSize = true;
            this.checkBoxFetchThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxFetchThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxFetchThumbnail.Location = new System.Drawing.Point(8, 13);
            this.checkBoxFetchThumbnail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxFetchThumbnail.Name = "checkBoxFetchThumbnail";
            this.checkBoxFetchThumbnail.Size = new System.Drawing.Size(155, 25);
            this.checkBoxFetchThumbnail.TabIndex = 12;
            this.checkBoxFetchThumbnail.Text = "Fetch thumbnail";
            this.checkBoxFetchThumbnail.UseSelectable = true;
            this.checkBoxFetchThumbnail.CheckedChanged += new System.EventHandler(this.CheckBoxFetchThumbnail_CheckedChanged);
            // 
            // checkBoxFetchBestThumbnail
            // 
            this.checkBoxFetchBestThumbnail.AutoSize = true;
            this.checkBoxFetchBestThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxFetchBestThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxFetchBestThumbnail.Location = new System.Drawing.Point(8, 44);
            this.checkBoxFetchBestThumbnail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxFetchBestThumbnail.Name = "checkBoxFetchBestThumbnail";
            this.checkBoxFetchBestThumbnail.Size = new System.Drawing.Size(194, 25);
            this.checkBoxFetchBestThumbnail.TabIndex = 13;
            this.checkBoxFetchBestThumbnail.Text = "Fetch best thumbnail";
            this.checkBoxFetchBestThumbnail.UseSelectable = true;
            // 
            // checkBoxAutoSelect
            // 
            this.checkBoxAutoSelect.AutoSize = true;
            this.checkBoxAutoSelect.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxAutoSelect.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxAutoSelect.Location = new System.Drawing.Point(8, 75);
            this.checkBoxAutoSelect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxAutoSelect.Name = "checkBoxAutoSelect";
            this.checkBoxAutoSelect.Size = new System.Drawing.Size(116, 25);
            this.checkBoxAutoSelect.TabIndex = 14;
            this.checkBoxAutoSelect.Text = "Auto select";
            this.checkBoxAutoSelect.UseSelectable = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.buttonCancel.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.buttonCancel.Location = new System.Drawing.Point(172, 337);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(138, 28);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseSelectable = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.buttonSave.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.buttonSave.Location = new System.Drawing.Point(27, 337);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(138, 28);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseSelectable = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelThreads.Location = new System.Drawing.Point(229, 118);
            this.labelThreads.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(76, 25);
            this.labelThreads.TabIndex = 18;
            this.labelThreads.Text = "Threads:";
            // 
            // comboBoxThreads
            // 
            this.comboBoxThreads.BackColor = System.Drawing.Color.White;
            this.comboBoxThreads.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxThreads.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.comboBoxThreads.ForeColor = System.Drawing.Color.Black;
            this.comboBoxThreads.FormattingEnabled = true;
            this.comboBoxThreads.ItemHeight = 29;
            this.comboBoxThreads.Location = new System.Drawing.Point(401, 112);
            this.comboBoxThreads.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxThreads.Name = "comboBoxThreads";
            this.comboBoxThreads.Size = new System.Drawing.Size(140, 35);
            this.comboBoxThreads.TabIndex = 17;
            this.comboBoxThreads.UseSelectable = true;
            // 
            // checkBoxPerTrackSettings
            // 
            this.checkBoxPerTrackSettings.AutoSize = true;
            this.checkBoxPerTrackSettings.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxPerTrackSettings.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxPerTrackSettings.Location = new System.Drawing.Point(8, 106);
            this.checkBoxPerTrackSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxPerTrackSettings.Name = "checkBoxPerTrackSettings";
            this.checkBoxPerTrackSettings.Size = new System.Drawing.Size(155, 25);
            this.checkBoxPerTrackSettings.TabIndex = 19;
            this.checkBoxPerTrackSettings.Text = "Per track settings";
            this.checkBoxPerTrackSettings.UseSelectable = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageFileSettings);
            this.tabControl.Controls.Add(this.tabPageDownloadSettings);
            this.tabControl.Controls.Add(this.tabPageLayoutSettings);
            this.tabControl.Controls.Add(this.tabPageApplication);
            this.tabControl.Controls.Add(this.tabPageAdvanced);
            this.tabControl.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.tabControl.Location = new System.Drawing.Point(27, 63);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(760, 260);
            this.tabControl.TabIndex = 20;
            this.tabControl.UseSelectable = true;
            // 
            // tabPageFileSettings
            // 
            this.tabPageFileSettings.BackColor = System.Drawing.Color.White;
            this.tabPageFileSettings.Controls.Add(this.labelPath);
            this.tabPageFileSettings.Controls.Add(this.textBoxPath);
            this.tabPageFileSettings.Controls.Add(this.checkBoxCreatePlaylistFolder);
            this.tabPageFileSettings.ForeColor = System.Drawing.Color.Black;
            this.tabPageFileSettings.HorizontalScrollbarBarColor = true;
            this.tabPageFileSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageFileSettings.HorizontalScrollbarSize = 10;
            this.tabPageFileSettings.Location = new System.Drawing.Point(4, 44);
            this.tabPageFileSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageFileSettings.Name = "tabPageFileSettings";
            this.tabPageFileSettings.Size = new System.Drawing.Size(752, 212);
            this.tabPageFileSettings.TabIndex = 0;
            this.tabPageFileSettings.Text = "File";
            this.tabPageFileSettings.VerticalScrollbarBarColor = true;
            this.tabPageFileSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageFileSettings.VerticalScrollbarSize = 8;
            // 
            // tabPageDownloadSettings
            // 
            this.tabPageDownloadSettings.BackColor = System.Drawing.Color.White;
            this.tabPageDownloadSettings.Controls.Add(this.labelThreads);
            this.tabPageDownloadSettings.Controls.Add(this.checkBoxExtractAudio);
            this.tabPageDownloadSettings.Controls.Add(this.comboBoxThreads);
            this.tabPageDownloadSettings.Controls.Add(this.checkBoxAddMetadata);
            this.tabPageDownloadSettings.Controls.Add(this.checkBoxEmbedThumbnail);
            this.tabPageDownloadSettings.Controls.Add(this.labelAudioFormat);
            this.tabPageDownloadSettings.Controls.Add(this.comboBoxAudioFormat);
            this.tabPageDownloadSettings.Controls.Add(this.comboBoxAudioQuality);
            this.tabPageDownloadSettings.Controls.Add(this.labelAudioQuality);
            this.tabPageDownloadSettings.Controls.Add(this.labelVideoFormat);
            this.tabPageDownloadSettings.Controls.Add(this.comboBoxVideoFormat);
            this.tabPageDownloadSettings.ForeColor = System.Drawing.Color.Black;
            this.tabPageDownloadSettings.HorizontalScrollbarBarColor = true;
            this.tabPageDownloadSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageDownloadSettings.HorizontalScrollbarSize = 10;
            this.tabPageDownloadSettings.Location = new System.Drawing.Point(4, 44);
            this.tabPageDownloadSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageDownloadSettings.Name = "tabPageDownloadSettings";
            this.tabPageDownloadSettings.Size = new System.Drawing.Size(752, 212);
            this.tabPageDownloadSettings.TabIndex = 1;
            this.tabPageDownloadSettings.Text = "Download";
            this.tabPageDownloadSettings.VerticalScrollbarBarColor = true;
            this.tabPageDownloadSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageDownloadSettings.VerticalScrollbarSize = 8;
            // 
            // tabPageLayoutSettings
            // 
            this.tabPageLayoutSettings.BackColor = System.Drawing.Color.White;
            this.tabPageLayoutSettings.Controls.Add(this.labelSelectionWidth);
            this.tabPageLayoutSettings.Controls.Add(this.comboBoxSelectionWidth);
            this.tabPageLayoutSettings.Controls.Add(this.labelColorStyle);
            this.tabPageLayoutSettings.Controls.Add(this.comboBoxColorStyle);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxYoutubeFastFetch);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxPerTrackSettings);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxFetchThumbnail);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxFetchBestThumbnail);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxAutoSelect);
            this.tabPageLayoutSettings.ForeColor = System.Drawing.Color.Black;
            this.tabPageLayoutSettings.HorizontalScrollbarBarColor = true;
            this.tabPageLayoutSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageLayoutSettings.HorizontalScrollbarSize = 10;
            this.tabPageLayoutSettings.Location = new System.Drawing.Point(4, 44);
            this.tabPageLayoutSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageLayoutSettings.Name = "tabPageLayoutSettings";
            this.tabPageLayoutSettings.Size = new System.Drawing.Size(752, 212);
            this.tabPageLayoutSettings.TabIndex = 2;
            this.tabPageLayoutSettings.Text = "Layout";
            this.tabPageLayoutSettings.VerticalScrollbarBarColor = true;
            this.tabPageLayoutSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageLayoutSettings.VerticalScrollbarSize = 8;
            // 
            // labelSelectionWidth
            // 
            this.labelSelectionWidth.AutoSize = true;
            this.labelSelectionWidth.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelSelectionWidth.Location = new System.Drawing.Point(259, 50);
            this.labelSelectionWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectionWidth.Name = "labelSelectionWidth";
            this.labelSelectionWidth.Size = new System.Drawing.Size(136, 25);
            this.labelSelectionWidth.TabIndex = 24;
            this.labelSelectionWidth.Text = "Selection width:";
            // 
            // comboBoxSelectionWidth
            // 
            this.comboBoxSelectionWidth.BackColor = System.Drawing.Color.White;
            this.comboBoxSelectionWidth.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxSelectionWidth.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.comboBoxSelectionWidth.ForeColor = System.Drawing.Color.Black;
            this.comboBoxSelectionWidth.FormattingEnabled = true;
            this.comboBoxSelectionWidth.ItemHeight = 29;
            this.comboBoxSelectionWidth.Location = new System.Drawing.Point(419, 47);
            this.comboBoxSelectionWidth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxSelectionWidth.Name = "comboBoxSelectionWidth";
            this.comboBoxSelectionWidth.Size = new System.Drawing.Size(140, 35);
            this.comboBoxSelectionWidth.TabIndex = 23;
            this.comboBoxSelectionWidth.UseSelectable = true;
            // 
            // labelColorStyle
            // 
            this.labelColorStyle.AutoSize = true;
            this.labelColorStyle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelColorStyle.Location = new System.Drawing.Point(259, 16);
            this.labelColorStyle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelColorStyle.Name = "labelColorStyle";
            this.labelColorStyle.Size = new System.Drawing.Size(100, 25);
            this.labelColorStyle.TabIndex = 22;
            this.labelColorStyle.Text = "Color style:";
            // 
            // comboBoxColorStyle
            // 
            this.comboBoxColorStyle.BackColor = System.Drawing.Color.White;
            this.comboBoxColorStyle.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxColorStyle.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.comboBoxColorStyle.ForeColor = System.Drawing.Color.Black;
            this.comboBoxColorStyle.FormattingEnabled = true;
            this.comboBoxColorStyle.ItemHeight = 29;
            this.comboBoxColorStyle.Location = new System.Drawing.Point(419, 13);
            this.comboBoxColorStyle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxColorStyle.Name = "comboBoxColorStyle";
            this.comboBoxColorStyle.Size = new System.Drawing.Size(140, 35);
            this.comboBoxColorStyle.TabIndex = 21;
            this.comboBoxColorStyle.UseSelectable = true;
            this.comboBoxColorStyle.SelectedIndexChanged += new System.EventHandler(this.ComboBoxColorStyle_SelectedIndexChanged);
            // 
            // checkBoxYoutubeFastFetch
            // 
            this.checkBoxYoutubeFastFetch.AutoSize = true;
            this.checkBoxYoutubeFastFetch.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxYoutubeFastFetch.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxYoutubeFastFetch.Location = new System.Drawing.Point(8, 137);
            this.checkBoxYoutubeFastFetch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxYoutubeFastFetch.Name = "checkBoxYoutubeFastFetch";
            this.checkBoxYoutubeFastFetch.Size = new System.Drawing.Size(171, 25);
            this.checkBoxYoutubeFastFetch.TabIndex = 20;
            this.checkBoxYoutubeFastFetch.Text = "Youtube fast fetch";
            this.checkBoxYoutubeFastFetch.UseSelectable = true;
            // 
            // tabPageApplication
            // 
            this.tabPageApplication.BackColor = System.Drawing.Color.White;
            this.tabPageApplication.Controls.Add(this.checkBoxHistory);
            this.tabPageApplication.Controls.Add(this.checkBoxAdvanced);
            this.tabPageApplication.Controls.Add(this.checkBoxAutostart);
            this.tabPageApplication.ForeColor = System.Drawing.Color.Black;
            this.tabPageApplication.HorizontalScrollbarBarColor = true;
            this.tabPageApplication.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageApplication.HorizontalScrollbarSize = 10;
            this.tabPageApplication.Location = new System.Drawing.Point(4, 44);
            this.tabPageApplication.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageApplication.Name = "tabPageApplication";
            this.tabPageApplication.Size = new System.Drawing.Size(752, 212);
            this.tabPageApplication.TabIndex = 3;
            this.tabPageApplication.Text = "Application";
            this.tabPageApplication.VerticalScrollbarBarColor = true;
            this.tabPageApplication.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageApplication.VerticalScrollbarSize = 8;
            // 
            // checkBoxHistory
            // 
            this.checkBoxHistory.AutoSize = true;
            this.checkBoxHistory.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxHistory.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxHistory.Location = new System.Drawing.Point(8, 75);
            this.checkBoxHistory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxHistory.Name = "checkBoxHistory";
            this.checkBoxHistory.Size = new System.Drawing.Size(139, 25);
            this.checkBoxHistory.TabIndex = 28;
            this.checkBoxHistory.Text = "Enable history";
            this.checkBoxHistory.UseSelectable = true;
            // 
            // checkBoxAdvanced
            // 
            this.checkBoxAdvanced.AutoSize = true;
            this.checkBoxAdvanced.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxAdvanced.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxAdvanced.Location = new System.Drawing.Point(8, 44);
            this.checkBoxAdvanced.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxAdvanced.Name = "checkBoxAdvanced";
            this.checkBoxAdvanced.Size = new System.Drawing.Size(174, 25);
            this.checkBoxAdvanced.TabIndex = 27;
            this.checkBoxAdvanced.Text = "Advanced settings";
            this.checkBoxAdvanced.UseSelectable = true;
            this.checkBoxAdvanced.CheckedChanged += new System.EventHandler(this.CheckBoxAdvanced_CheckedChanged);
            // 
            // checkBoxAutostart
            // 
            this.checkBoxAutostart.AutoSize = true;
            this.checkBoxAutostart.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxAutostart.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxAutostart.Location = new System.Drawing.Point(8, 13);
            this.checkBoxAutostart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxAutostart.Name = "checkBoxAutostart";
            this.checkBoxAutostart.Size = new System.Drawing.Size(102, 25);
            this.checkBoxAutostart.TabIndex = 13;
            this.checkBoxAutostart.Text = "Autostart";
            this.checkBoxAutostart.UseSelectable = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.labelUpdateChannel);
            this.tabPageAdvanced.Controls.Add(this.comboBoxUpdateChannel);
            this.tabPageAdvanced.HorizontalScrollbarBarColor = true;
            this.tabPageAdvanced.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageAdvanced.HorizontalScrollbarSize = 10;
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 44);
            this.tabPageAdvanced.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Size = new System.Drawing.Size(752, 212);
            this.tabPageAdvanced.TabIndex = 4;
            this.tabPageAdvanced.Text = "Advanced";
            this.tabPageAdvanced.VerticalScrollbarBarColor = true;
            this.tabPageAdvanced.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageAdvanced.VerticalScrollbarSize = 8;
            // 
            // labelUpdateChannel
            // 
            this.labelUpdateChannel.AutoSize = true;
            this.labelUpdateChannel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelUpdateChannel.Location = new System.Drawing.Point(4, 11);
            this.labelUpdateChannel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUpdateChannel.Name = "labelUpdateChannel";
            this.labelUpdateChannel.Size = new System.Drawing.Size(136, 25);
            this.labelUpdateChannel.TabIndex = 28;
            this.labelUpdateChannel.Text = "Update channel:";
            // 
            // comboBoxUpdateChannel
            // 
            this.comboBoxUpdateChannel.BackColor = System.Drawing.Color.White;
            this.comboBoxUpdateChannel.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxUpdateChannel.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.comboBoxUpdateChannel.ForeColor = System.Drawing.Color.Black;
            this.comboBoxUpdateChannel.FormattingEnabled = true;
            this.comboBoxUpdateChannel.ItemHeight = 29;
            this.comboBoxUpdateChannel.Location = new System.Drawing.Point(167, 7);
            this.comboBoxUpdateChannel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxUpdateChannel.Name = "comboBoxUpdateChannel";
            this.comboBoxUpdateChannel.Size = new System.Drawing.Size(140, 35);
            this.comboBoxUpdateChannel.TabIndex = 27;
            this.comboBoxUpdateChannel.UseSelectable = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 381);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(23, 60, 23, 20);
            this.Resizable = false;
            this.Text = "Settings";
            this.tabControl.ResumeLayout(false);
            this.tabPageFileSettings.ResumeLayout(false);
            this.tabPageFileSettings.PerformLayout();
            this.tabPageDownloadSettings.ResumeLayout(false);
            this.tabPageDownloadSettings.PerformLayout();
            this.tabPageLayoutSettings.ResumeLayout(false);
            this.tabPageLayoutSettings.PerformLayout();
            this.tabPageApplication.ResumeLayout(false);
            this.tabPageApplication.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox textBoxPath;
        private MetroFramework.Controls.MetroLabel labelPath;
        private MetroFramework.Controls.MetroCheckBox checkBoxExtractAudio;
        private MetroFramework.Controls.MetroCheckBox checkBoxCreatePlaylistFolder;
        private MetroFramework.Controls.MetroCheckBox checkBoxAddMetadata;
        private MetroFramework.Controls.MetroCheckBox checkBoxEmbedThumbnail;
        private MetroFramework.Controls.MetroComboBox comboBoxAudioFormat;
        private MetroFramework.Controls.MetroLabel labelAudioFormat;
        private MetroFramework.Controls.MetroLabel labelAudioQuality;
        private MetroFramework.Controls.MetroComboBox comboBoxAudioQuality;
        private MetroFramework.Controls.MetroLabel labelVideoFormat;
        private MetroFramework.Controls.MetroComboBox comboBoxVideoFormat;
        private MetroFramework.Controls.MetroCheckBox checkBoxFetchThumbnail;
        private MetroFramework.Controls.MetroCheckBox checkBoxFetchBestThumbnail;
        private MetroFramework.Controls.MetroCheckBox checkBoxAutoSelect;
        private MetroFramework.Controls.MetroButton buttonCancel;
        private MetroFramework.Controls.MetroButton buttonSave;
        private MetroFramework.Controls.MetroLabel labelThreads;
        private MetroFramework.Controls.MetroComboBox comboBoxThreads;
        private MetroFramework.Controls.MetroCheckBox checkBoxPerTrackSettings;
        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTabPage tabPageFileSettings;
        private MetroFramework.Controls.MetroTabPage tabPageDownloadSettings;
        private MetroFramework.Controls.MetroTabPage tabPageLayoutSettings;
        private MetroFramework.Controls.MetroTabPage tabPageApplication;
        private MetroFramework.Controls.MetroCheckBox checkBoxAutostart;
        private MetroFramework.Controls.MetroCheckBox checkBoxYoutubeFastFetch;
        private MetroFramework.Controls.MetroComboBox comboBoxColorStyle;
        private MetroFramework.Controls.MetroLabel labelColorStyle;
        private MetroFramework.Controls.MetroLabel labelSelectionWidth;
        private MetroFramework.Controls.MetroComboBox comboBoxSelectionWidth;
        private MetroFramework.Controls.MetroCheckBox checkBoxAdvanced;
        private MetroFramework.Controls.MetroTabPage tabPageAdvanced;
        private MetroFramework.Controls.MetroLabel labelUpdateChannel;
        private MetroFramework.Controls.MetroComboBox comboBoxUpdateChannel;
        private MetroFramework.Controls.MetroCheckBox checkBoxHistory;
    }
}