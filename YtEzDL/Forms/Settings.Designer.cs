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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
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
            this.tabControl.SuspendLayout();
            this.tabPageFileSettings.SuspendLayout();
            this.tabPageDownloadSettings.SuspendLayout();
            this.tabPageLayoutSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxPath.CustomButton.Image = null;
            this.textBoxPath.CustomButton.Location = new System.Drawing.Point(448, 2);
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
            this.textBoxPath.Location = new System.Drawing.Point(58, 15);
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
            this.textBoxPath.Size = new System.Drawing.Size(476, 30);
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
            this.labelPath.Location = new System.Drawing.Point(3, 16);
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
            this.checkBoxExtractAudio.Location = new System.Drawing.Point(7, 13);
            this.checkBoxExtractAudio.Name = "checkBoxExtractAudio";
            this.checkBoxExtractAudio.Size = new System.Drawing.Size(126, 25);
            this.checkBoxExtractAudio.TabIndex = 2;
            this.checkBoxExtractAudio.Text = "Extract audio";
            this.checkBoxExtractAudio.UseSelectable = true;
            this.checkBoxExtractAudio.CheckedChanged += new System.EventHandler(this.checkBoxExtractAudio_CheckedChanged);
            // 
            // checkBoxCreatePlaylistFolder
            // 
            this.checkBoxCreatePlaylistFolder.AutoSize = true;
            this.checkBoxCreatePlaylistFolder.BackColor = System.Drawing.Color.White;
            this.checkBoxCreatePlaylistFolder.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxCreatePlaylistFolder.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxCreatePlaylistFolder.ForeColor = System.Drawing.Color.Black;
            this.checkBoxCreatePlaylistFolder.Location = new System.Drawing.Point(8, 58);
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
            this.checkBoxAddMetadata.Location = new System.Drawing.Point(7, 44);
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
            this.checkBoxEmbedThumbnail.Location = new System.Drawing.Point(7, 75);
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
            this.comboBoxAudioFormat.ForeColor = System.Drawing.Color.Black;
            this.comboBoxAudioFormat.FormattingEnabled = true;
            this.comboBoxAudioFormat.ItemHeight = 29;
            this.comboBoxAudioFormat.Location = new System.Drawing.Point(344, 13);
            this.comboBoxAudioFormat.Name = "comboBoxAudioFormat";
            this.comboBoxAudioFormat.Size = new System.Drawing.Size(121, 35);
            this.comboBoxAudioFormat.TabIndex = 6;
            this.comboBoxAudioFormat.UseSelectable = true;
            // 
            // labelAudioFormat
            // 
            this.labelAudioFormat.AutoSize = true;
            this.labelAudioFormat.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelAudioFormat.Location = new System.Drawing.Point(196, 17);
            this.labelAudioFormat.Name = "labelAudioFormat";
            this.labelAudioFormat.Size = new System.Drawing.Size(116, 25);
            this.labelAudioFormat.TabIndex = 7;
            this.labelAudioFormat.Text = "Audio format:";
            // 
            // labelAudioQuality
            // 
            this.labelAudioQuality.AutoSize = true;
            this.labelAudioQuality.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelAudioQuality.Location = new System.Drawing.Point(196, 51);
            this.labelAudioQuality.Name = "labelAudioQuality";
            this.labelAudioQuality.Size = new System.Drawing.Size(116, 25);
            this.labelAudioQuality.TabIndex = 9;
            this.labelAudioQuality.Text = "Audio quality:";
            // 
            // comboBoxAudioQuality
            // 
            this.comboBoxAudioQuality.BackColor = System.Drawing.Color.White;
            this.comboBoxAudioQuality.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxAudioQuality.ForeColor = System.Drawing.Color.White;
            this.comboBoxAudioQuality.FormattingEnabled = true;
            this.comboBoxAudioQuality.ItemHeight = 29;
            this.comboBoxAudioQuality.Location = new System.Drawing.Point(344, 46);
            this.comboBoxAudioQuality.Name = "comboBoxAudioQuality";
            this.comboBoxAudioQuality.Size = new System.Drawing.Size(121, 35);
            this.comboBoxAudioQuality.TabIndex = 8;
            this.comboBoxAudioQuality.UseSelectable = true;
            // 
            // labelVideoFormat
            // 
            this.labelVideoFormat.AutoSize = true;
            this.labelVideoFormat.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelVideoFormat.Location = new System.Drawing.Point(196, 85);
            this.labelVideoFormat.Name = "labelVideoFormat";
            this.labelVideoFormat.Size = new System.Drawing.Size(115, 25);
            this.labelVideoFormat.TabIndex = 11;
            this.labelVideoFormat.Text = "Video format:";
            // 
            // comboBoxVideoFormat
            // 
            this.comboBoxVideoFormat.BackColor = System.Drawing.Color.White;
            this.comboBoxVideoFormat.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxVideoFormat.ForeColor = System.Drawing.Color.Black;
            this.comboBoxVideoFormat.FormattingEnabled = true;
            this.comboBoxVideoFormat.ItemHeight = 29;
            this.comboBoxVideoFormat.Location = new System.Drawing.Point(344, 79);
            this.comboBoxVideoFormat.Name = "comboBoxVideoFormat";
            this.comboBoxVideoFormat.Size = new System.Drawing.Size(121, 35);
            this.comboBoxVideoFormat.TabIndex = 10;
            this.comboBoxVideoFormat.UseSelectable = true;
            // 
            // checkBoxFetchThumbnail
            // 
            this.checkBoxFetchThumbnail.AutoSize = true;
            this.checkBoxFetchThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxFetchThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxFetchThumbnail.Location = new System.Drawing.Point(7, 13);
            this.checkBoxFetchThumbnail.Name = "checkBoxFetchThumbnail";
            this.checkBoxFetchThumbnail.Size = new System.Drawing.Size(150, 25);
            this.checkBoxFetchThumbnail.TabIndex = 12;
            this.checkBoxFetchThumbnail.Text = "Fetch thumbnail";
            this.checkBoxFetchThumbnail.UseSelectable = true;
            this.checkBoxFetchThumbnail.CheckedChanged += new System.EventHandler(this.checkBoxFetchThumbnail_CheckedChanged);
            // 
            // checkBoxFetchBestThumbnail
            // 
            this.checkBoxFetchBestThumbnail.AutoSize = true;
            this.checkBoxFetchBestThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxFetchBestThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxFetchBestThumbnail.Location = new System.Drawing.Point(7, 44);
            this.checkBoxFetchBestThumbnail.Name = "checkBoxFetchBestThumbnail";
            this.checkBoxFetchBestThumbnail.Size = new System.Drawing.Size(186, 25);
            this.checkBoxFetchBestThumbnail.TabIndex = 13;
            this.checkBoxFetchBestThumbnail.Text = "Fetch best thumbnail";
            this.checkBoxFetchBestThumbnail.UseSelectable = true;
            // 
            // checkBoxAutoSelect
            // 
            this.checkBoxAutoSelect.AutoSize = true;
            this.checkBoxAutoSelect.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxAutoSelect.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxAutoSelect.Location = new System.Drawing.Point(7, 75);
            this.checkBoxAutoSelect.Name = "checkBoxAutoSelect";
            this.checkBoxAutoSelect.Size = new System.Drawing.Size(111, 25);
            this.checkBoxAutoSelect.TabIndex = 14;
            this.checkBoxAutoSelect.Text = "Auto select";
            this.checkBoxAutoSelect.UseSelectable = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.buttonCancel.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.buttonCancel.Location = new System.Drawing.Point(147, 336);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(118, 28);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseSelectable = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.buttonSave.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.buttonSave.Location = new System.Drawing.Point(23, 336);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(118, 28);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseSelectable = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelThreads.Location = new System.Drawing.Point(196, 119);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(76, 25);
            this.labelThreads.TabIndex = 18;
            this.labelThreads.Text = "Threads:";
            // 
            // comboBoxThreads
            // 
            this.comboBoxThreads.BackColor = System.Drawing.Color.White;
            this.comboBoxThreads.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxThreads.ForeColor = System.Drawing.Color.Black;
            this.comboBoxThreads.FormattingEnabled = true;
            this.comboBoxThreads.ItemHeight = 29;
            this.comboBoxThreads.Location = new System.Drawing.Point(344, 112);
            this.comboBoxThreads.Name = "comboBoxThreads";
            this.comboBoxThreads.Size = new System.Drawing.Size(121, 35);
            this.comboBoxThreads.TabIndex = 17;
            this.comboBoxThreads.UseSelectable = true;
            // 
            // checkBoxPerTrackSettings
            // 
            this.checkBoxPerTrackSettings.AutoSize = true;
            this.checkBoxPerTrackSettings.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxPerTrackSettings.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxPerTrackSettings.Location = new System.Drawing.Point(7, 106);
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
            this.tabControl.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.tabControl.Location = new System.Drawing.Point(23, 63);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 2;
            this.tabControl.Size = new System.Drawing.Size(651, 260);
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
            this.tabPageFileSettings.Name = "tabPageFileSettings";
            this.tabPageFileSettings.Size = new System.Drawing.Size(643, 212);
            this.tabPageFileSettings.TabIndex = 0;
            this.tabPageFileSettings.Text = "File";
            this.tabPageFileSettings.VerticalScrollbarBarColor = true;
            this.tabPageFileSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageFileSettings.VerticalScrollbarSize = 10;
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
            this.tabPageDownloadSettings.Name = "tabPageDownloadSettings";
            this.tabPageDownloadSettings.Size = new System.Drawing.Size(643, 212);
            this.tabPageDownloadSettings.TabIndex = 1;
            this.tabPageDownloadSettings.Text = "Download";
            this.tabPageDownloadSettings.VerticalScrollbarBarColor = true;
            this.tabPageDownloadSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageDownloadSettings.VerticalScrollbarSize = 10;
            // 
            // tabPageLayoutSettings
            // 
            this.tabPageLayoutSettings.BackColor = System.Drawing.Color.White;
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxPerTrackSettings);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxFetchThumbnail);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxFetchBestThumbnail);
            this.tabPageLayoutSettings.Controls.Add(this.checkBoxAutoSelect);
            this.tabPageLayoutSettings.ForeColor = System.Drawing.Color.Black;
            this.tabPageLayoutSettings.HorizontalScrollbarBarColor = true;
            this.tabPageLayoutSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageLayoutSettings.HorizontalScrollbarSize = 10;
            this.tabPageLayoutSettings.Location = new System.Drawing.Point(4, 44);
            this.tabPageLayoutSettings.Name = "tabPageLayoutSettings";
            this.tabPageLayoutSettings.Size = new System.Drawing.Size(643, 212);
            this.tabPageLayoutSettings.TabIndex = 2;
            this.tabPageLayoutSettings.Text = "Layout";
            this.tabPageLayoutSettings.VerticalScrollbarBarColor = true;
            this.tabPageLayoutSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageLayoutSettings.VerticalScrollbarSize = 10;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 381);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Resizable = false;
            this.Text = "Settings";
            this.tabControl.ResumeLayout(false);
            this.tabPageFileSettings.ResumeLayout(false);
            this.tabPageFileSettings.PerformLayout();
            this.tabPageDownloadSettings.ResumeLayout(false);
            this.tabPageDownloadSettings.PerformLayout();
            this.tabPageLayoutSettings.ResumeLayout(false);
            this.tabPageLayoutSettings.PerformLayout();
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
    }
}