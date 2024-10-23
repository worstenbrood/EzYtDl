namespace YtEzDL.UserControls
{
    partial class Track
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroLabel = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPageInfo = new MetroFramework.Controls.MetroTabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxTitle = new YtEzDL.UserControls.ScrollTextBox();
            this.tabPageSettings = new MetroFramework.Controls.MetroTabPage();
            this.labelVideoFormat = new MetroFramework.Controls.MetroLabel();
            this.comboBoxVideoFormat = new MetroFramework.Controls.MetroComboBox();
            this.labelAudioQuality = new MetroFramework.Controls.MetroLabel();
            this.comboBoxAudioQuality = new MetroFramework.Controls.MetroComboBox();
            this.labelAudioFormat = new MetroFramework.Controls.MetroLabel();
            this.comboBoxAudioFormat = new MetroFramework.Controls.MetroComboBox();
            this.checkBoxExtractAudio = new MetroFramework.Controls.MetroCheckBox();
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.metroTabControl.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tabPageSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel
            // 
            this.metroLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel.Location = new System.Drawing.Point(16, 224);
            this.metroLabel.Name = "metroLabel";
            this.metroLabel.Size = new System.Drawing.Size(624, 20);
            this.metroLabel.TabIndex = 16;
            this.metroLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            this.metroLabel.Resize += new System.EventHandler(this.ControlResize);
            // 
            // metroTabControl
            // 
            this.metroTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTabControl.Controls.Add(this.tabPageInfo);
            this.metroTabControl.Controls.Add(this.tabPageSettings);
            this.metroTabControl.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.metroTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.metroTabControl.Location = new System.Drawing.Point(16, 14);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.SelectedIndex = 0;
            this.metroTabControl.Size = new System.Drawing.Size(628, 207);
            this.metroTabControl.TabIndex = 14;
            this.metroTabControl.UseSelectable = true;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.BackColor = System.Drawing.Color.White;
            this.tabPageInfo.Controls.Add(this.pictureBox);
            this.tabPageInfo.Controls.Add(this.textBoxTitle);
            this.tabPageInfo.HorizontalScrollbarBarColor = false;
            this.tabPageInfo.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageInfo.HorizontalScrollbarSize = 10;
            this.tabPageInfo.Location = new System.Drawing.Point(4, 44);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Size = new System.Drawing.Size(620, 159);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "Info";
            this.tabPageInfo.VerticalScrollbarBarColor = false;
            this.tabPageInfo.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageInfo.VerticalScrollbarSize = 10;
            this.tabPageInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            this.tabPageInfo.Resize += new System.EventHandler(this.ControlResize);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox.Location = new System.Drawing.Point(16, 21);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(284, 138);
            this.pictureBox.TabIndex = 11;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxTitle.ForeColor = System.Drawing.Color.Black;
            this.textBoxTitle.Location = new System.Drawing.Point(305, 21);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(310, 126);
            this.textBoxTitle.TabIndex = 1;
            this.textBoxTitle.Text = "Title";
            this.textBoxTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.White;
            this.tabPageSettings.Controls.Add(this.labelVideoFormat);
            this.tabPageSettings.Controls.Add(this.comboBoxVideoFormat);
            this.tabPageSettings.Controls.Add(this.labelAudioQuality);
            this.tabPageSettings.Controls.Add(this.comboBoxAudioQuality);
            this.tabPageSettings.Controls.Add(this.labelAudioFormat);
            this.tabPageSettings.Controls.Add(this.comboBoxAudioFormat);
            this.tabPageSettings.Controls.Add(this.checkBoxExtractAudio);
            this.tabPageSettings.HorizontalScrollbarBarColor = false;
            this.tabPageSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageSettings.HorizontalScrollbarSize = 10;
            this.tabPageSettings.Location = new System.Drawing.Point(4, 44);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(620, 159);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.VerticalScrollbarBarColor = false;
            this.tabPageSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageSettings.VerticalScrollbarSize = 10;
            this.tabPageSettings.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            // 
            // labelVideoFormat
            // 
            this.labelVideoFormat.AutoSize = true;
            this.labelVideoFormat.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelVideoFormat.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.labelVideoFormat.ForeColor = System.Drawing.Color.Black;
            this.labelVideoFormat.Location = new System.Drawing.Point(4, 114);
            this.labelVideoFormat.Name = "labelVideoFormat";
            this.labelVideoFormat.Size = new System.Drawing.Size(121, 25);
            this.labelVideoFormat.TabIndex = 8;
            this.labelVideoFormat.Text = "Video format:";
            // 
            // comboBoxVideoFormat
            // 
            this.comboBoxVideoFormat.BackColor = System.Drawing.Color.White;
            this.comboBoxVideoFormat.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxVideoFormat.ForeColor = System.Drawing.Color.Black;
            this.comboBoxVideoFormat.FormattingEnabled = true;
            this.comboBoxVideoFormat.ItemHeight = 29;
            this.comboBoxVideoFormat.Location = new System.Drawing.Point(127, 110);
            this.comboBoxVideoFormat.Name = "comboBoxVideoFormat";
            this.comboBoxVideoFormat.Size = new System.Drawing.Size(121, 35);
            this.comboBoxVideoFormat.TabIndex = 7;
            this.comboBoxVideoFormat.UseSelectable = true;
            // 
            // labelAudioQuality
            // 
            this.labelAudioQuality.AutoSize = true;
            this.labelAudioQuality.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelAudioQuality.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.labelAudioQuality.ForeColor = System.Drawing.Color.Black;
            this.labelAudioQuality.Location = new System.Drawing.Point(4, 80);
            this.labelAudioQuality.Name = "labelAudioQuality";
            this.labelAudioQuality.Size = new System.Drawing.Size(122, 25);
            this.labelAudioQuality.TabIndex = 6;
            this.labelAudioQuality.Text = "Audio quality:";
            // 
            // comboBoxAudioQuality
            // 
            this.comboBoxAudioQuality.BackColor = System.Drawing.Color.White;
            this.comboBoxAudioQuality.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxAudioQuality.ForeColor = System.Drawing.Color.Black;
            this.comboBoxAudioQuality.FormattingEnabled = true;
            this.comboBoxAudioQuality.ItemHeight = 29;
            this.comboBoxAudioQuality.Location = new System.Drawing.Point(127, 76);
            this.comboBoxAudioQuality.Name = "comboBoxAudioQuality";
            this.comboBoxAudioQuality.Size = new System.Drawing.Size(121, 35);
            this.comboBoxAudioQuality.TabIndex = 5;
            this.comboBoxAudioQuality.UseSelectable = true;
            // 
            // labelAudioFormat
            // 
            this.labelAudioFormat.AutoSize = true;
            this.labelAudioFormat.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelAudioFormat.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.labelAudioFormat.ForeColor = System.Drawing.Color.Black;
            this.labelAudioFormat.Location = new System.Drawing.Point(4, 45);
            this.labelAudioFormat.Name = "labelAudioFormat";
            this.labelAudioFormat.Size = new System.Drawing.Size(123, 25);
            this.labelAudioFormat.TabIndex = 4;
            this.labelAudioFormat.Text = "Audio format:";
            // 
            // comboBoxAudioFormat
            // 
            this.comboBoxAudioFormat.BackColor = System.Drawing.Color.White;
            this.comboBoxAudioFormat.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxAudioFormat.ForeColor = System.Drawing.Color.Black;
            this.comboBoxAudioFormat.FormattingEnabled = true;
            this.comboBoxAudioFormat.ItemHeight = 29;
            this.comboBoxAudioFormat.Location = new System.Drawing.Point(127, 42);
            this.comboBoxAudioFormat.Name = "comboBoxAudioFormat";
            this.comboBoxAudioFormat.Size = new System.Drawing.Size(121, 35);
            this.comboBoxAudioFormat.TabIndex = 3;
            this.comboBoxAudioFormat.UseSelectable = true;
            // 
            // checkBoxExtractAudio
            // 
            this.checkBoxExtractAudio.AutoSize = true;
            this.checkBoxExtractAudio.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.checkBoxExtractAudio.ForeColor = System.Drawing.Color.Black;
            this.checkBoxExtractAudio.Location = new System.Drawing.Point(3, 14);
            this.checkBoxExtractAudio.Name = "checkBoxExtractAudio";
            this.checkBoxExtractAudio.Size = new System.Drawing.Size(130, 25);
            this.checkBoxExtractAudio.TabIndex = 2;
            this.checkBoxExtractAudio.Text = "Extract audio";
            this.checkBoxExtractAudio.UseSelectable = true;
            this.checkBoxExtractAudio.CheckedChanged += new System.EventHandler(this.CheckBoxExtractAudio_CheckedChanged);
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressBar.FontSize = MetroFramework.MetroProgressBarSize.Tall;
            this.metroProgressBar.Location = new System.Drawing.Point(16, 246);
            this.metroProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.Size = new System.Drawing.Size(625, 17);
            this.metroProgressBar.TabIndex = 15;
            this.metroProgressBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            this.metroProgressBar.Resize += new System.EventHandler(this.ControlResize);
            // 
            // Track
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.metroLabel);
            this.Controls.Add(this.metroTabControl);
            this.Controls.Add(this.metroProgressBar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Name = "Track";
            this.Size = new System.Drawing.Size(659, 283);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            this.Resize += new System.EventHandler(this.Track_Resize);
            this.metroTabControl.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private MetroFramework.Controls.MetroTabPage tabPageInfo;
        private System.Windows.Forms.PictureBox pictureBox;
        private ScrollTextBox textBoxTitle;
        private MetroFramework.Controls.MetroTabPage tabPageSettings;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar;
        private MetroFramework.Controls.MetroCheckBox checkBoxExtractAudio;
        private MetroFramework.Controls.MetroComboBox comboBoxAudioFormat;
        private MetroFramework.Controls.MetroLabel labelAudioFormat;
        private MetroFramework.Controls.MetroLabel labelAudioQuality;
        private MetroFramework.Controls.MetroComboBox comboBoxAudioQuality;
        private MetroFramework.Controls.MetroLabel labelVideoFormat;
        private MetroFramework.Controls.MetroComboBox comboBoxVideoFormat;
    }
}
