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
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            // 
            // 
            // 
            this.textBoxPath.CustomButton.Image = null;
            this.textBoxPath.CustomButton.Location = new System.Drawing.Point(661, 1);
            this.textBoxPath.CustomButton.Name = "";
            this.textBoxPath.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxPath.CustomButton.TabIndex = 1;
            this.textBoxPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxPath.CustomButton.UseSelectable = true;
            this.textBoxPath.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.textBoxPath.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.textBoxPath.Lines = new string[0];
            this.textBoxPath.Location = new System.Drawing.Point(86, 66);
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
            this.textBoxPath.Size = new System.Drawing.Size(683, 23);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.UseSelectable = true;
            this.textBoxPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(43, 66);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(37, 19);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Path:";
            // 
            // checkBoxExtractAudio
            // 
            this.checkBoxExtractAudio.AutoSize = true;
            this.checkBoxExtractAudio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.checkBoxExtractAudio.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxExtractAudio.Location = new System.Drawing.Point(47, 120);
            this.checkBoxExtractAudio.Name = "checkBoxExtractAudio";
            this.checkBoxExtractAudio.Size = new System.Drawing.Size(101, 19);
            this.checkBoxExtractAudio.TabIndex = 2;
            this.checkBoxExtractAudio.Text = "Extract audio";
            this.checkBoxExtractAudio.UseSelectable = true;
            // 
            // checkBoxCreatePlaylistFolder
            // 
            this.checkBoxCreatePlaylistFolder.AutoSize = true;
            this.checkBoxCreatePlaylistFolder.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.checkBoxCreatePlaylistFolder.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxCreatePlaylistFolder.Location = new System.Drawing.Point(47, 95);
            this.checkBoxCreatePlaylistFolder.Name = "checkBoxCreatePlaylistFolder";
            this.checkBoxCreatePlaylistFolder.Size = new System.Drawing.Size(146, 19);
            this.checkBoxCreatePlaylistFolder.TabIndex = 3;
            this.checkBoxCreatePlaylistFolder.Text = "Create playlist folder";
            this.checkBoxCreatePlaylistFolder.UseSelectable = true;
            // 
            // checkBoxAddMetadata
            // 
            this.checkBoxAddMetadata.AutoSize = true;
            this.checkBoxAddMetadata.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.checkBoxAddMetadata.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxAddMetadata.Location = new System.Drawing.Point(47, 145);
            this.checkBoxAddMetadata.Name = "checkBoxAddMetadata";
            this.checkBoxAddMetadata.Size = new System.Drawing.Size(110, 19);
            this.checkBoxAddMetadata.TabIndex = 4;
            this.checkBoxAddMetadata.Text = "Add metadata";
            this.checkBoxAddMetadata.UseSelectable = true;
            // 
            // checkBoxEmbedThumbnail
            // 
            this.checkBoxEmbedThumbnail.AutoSize = true;
            this.checkBoxEmbedThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.checkBoxEmbedThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxEmbedThumbnail.Location = new System.Drawing.Point(47, 170);
            this.checkBoxEmbedThumbnail.Name = "checkBoxEmbedThumbnail";
            this.checkBoxEmbedThumbnail.Size = new System.Drawing.Size(129, 19);
            this.checkBoxEmbedThumbnail.TabIndex = 5;
            this.checkBoxEmbedThumbnail.Text = "Embed thumbnail";
            this.checkBoxEmbedThumbnail.UseSelectable = true;
            // 
            // comboBoxAudioFormat
            // 
            this.comboBoxAudioFormat.FormattingEnabled = true;
            this.comboBoxAudioFormat.ItemHeight = 23;
            this.comboBoxAudioFormat.Location = new System.Drawing.Point(373, 95);
            this.comboBoxAudioFormat.Name = "comboBoxAudioFormat";
            this.comboBoxAudioFormat.Size = new System.Drawing.Size(121, 29);
            this.comboBoxAudioFormat.TabIndex = 6;
            this.comboBoxAudioFormat.UseSelectable = true;
            // 
            // labelAudioFormat
            // 
            this.labelAudioFormat.AutoSize = true;
            this.labelAudioFormat.Location = new System.Drawing.Point(276, 100);
            this.labelAudioFormat.Name = "labelAudioFormat";
            this.labelAudioFormat.Size = new System.Drawing.Size(91, 19);
            this.labelAudioFormat.TabIndex = 7;
            this.labelAudioFormat.Text = "Audio format:";
            // 
            // labelAudioQuality
            // 
            this.labelAudioQuality.AutoSize = true;
            this.labelAudioQuality.Location = new System.Drawing.Point(276, 133);
            this.labelAudioQuality.Name = "labelAudioQuality";
            this.labelAudioQuality.Size = new System.Drawing.Size(89, 19);
            this.labelAudioQuality.TabIndex = 9;
            this.labelAudioQuality.Text = "Audio quality:";
            // 
            // comboBoxAudioQuality
            // 
            this.comboBoxAudioQuality.FormattingEnabled = true;
            this.comboBoxAudioQuality.ItemHeight = 23;
            this.comboBoxAudioQuality.Location = new System.Drawing.Point(373, 129);
            this.comboBoxAudioQuality.Name = "comboBoxAudioQuality";
            this.comboBoxAudioQuality.Size = new System.Drawing.Size(121, 29);
            this.comboBoxAudioQuality.TabIndex = 8;
            this.comboBoxAudioQuality.UseSelectable = true;
            // 
            // labelVideoFormat
            // 
            this.labelVideoFormat.AutoSize = true;
            this.labelVideoFormat.Location = new System.Drawing.Point(277, 168);
            this.labelVideoFormat.Name = "labelVideoFormat";
            this.labelVideoFormat.Size = new System.Drawing.Size(90, 19);
            this.labelVideoFormat.TabIndex = 11;
            this.labelVideoFormat.Text = "Video format:";
            // 
            // comboBoxVideoFormat
            // 
            this.comboBoxVideoFormat.FormattingEnabled = true;
            this.comboBoxVideoFormat.ItemHeight = 23;
            this.comboBoxVideoFormat.Location = new System.Drawing.Point(373, 164);
            this.comboBoxVideoFormat.Name = "comboBoxVideoFormat";
            this.comboBoxVideoFormat.Size = new System.Drawing.Size(121, 29);
            this.comboBoxVideoFormat.TabIndex = 10;
            this.comboBoxVideoFormat.UseSelectable = true;
            // 
            // checkBoxFetchThumbnail
            // 
            this.checkBoxFetchThumbnail.AutoSize = true;
            this.checkBoxFetchThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.checkBoxFetchThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxFetchThumbnail.Location = new System.Drawing.Point(47, 195);
            this.checkBoxFetchThumbnail.Name = "checkBoxFetchThumbnail";
            this.checkBoxFetchThumbnail.Size = new System.Drawing.Size(118, 19);
            this.checkBoxFetchThumbnail.TabIndex = 12;
            this.checkBoxFetchThumbnail.Text = "Fetch thumbnail";
            this.checkBoxFetchThumbnail.UseSelectable = true;
            // 
            // checkBoxFetchBestThumbnail
            // 
            this.checkBoxFetchBestThumbnail.AutoSize = true;
            this.checkBoxFetchBestThumbnail.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.checkBoxFetchBestThumbnail.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxFetchBestThumbnail.Location = new System.Drawing.Point(47, 220);
            this.checkBoxFetchBestThumbnail.Name = "checkBoxFetchBestThumbnail";
            this.checkBoxFetchBestThumbnail.Size = new System.Drawing.Size(146, 19);
            this.checkBoxFetchBestThumbnail.TabIndex = 13;
            this.checkBoxFetchBestThumbnail.Text = "Fetch best thumbnail";
            this.checkBoxFetchBestThumbnail.UseSelectable = true;
            // 
            // checkBoxAutoSelect
            // 
            this.checkBoxAutoSelect.AutoSize = true;
            this.checkBoxAutoSelect.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.checkBoxAutoSelect.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.checkBoxAutoSelect.Location = new System.Drawing.Point(47, 245);
            this.checkBoxAutoSelect.Name = "checkBoxAutoSelect";
            this.checkBoxAutoSelect.Size = new System.Drawing.Size(89, 19);
            this.checkBoxAutoSelect.TabIndex = 14;
            this.checkBoxAutoSelect.Text = "Auto select";
            this.checkBoxAutoSelect.UseSelectable = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(147, 280);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(118, 28);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseSelectable = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 280);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(118, 28);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseSelectable = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 320);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxAutoSelect);
            this.Controls.Add(this.checkBoxFetchBestThumbnail);
            this.Controls.Add(this.checkBoxFetchThumbnail);
            this.Controls.Add(this.labelVideoFormat);
            this.Controls.Add(this.comboBoxVideoFormat);
            this.Controls.Add(this.labelAudioQuality);
            this.Controls.Add(this.comboBoxAudioQuality);
            this.Controls.Add(this.labelAudioFormat);
            this.Controls.Add(this.comboBoxAudioFormat);
            this.Controls.Add(this.checkBoxEmbedThumbnail);
            this.Controls.Add(this.checkBoxAddMetadata);
            this.Controls.Add(this.checkBoxCreatePlaylistFolder);
            this.Controls.Add(this.checkBoxExtractAudio);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxPath);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}