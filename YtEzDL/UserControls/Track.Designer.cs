namespace YtEzDL.Forms
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
            this.metroLabelAction = new MetroFramework.Controls.MetroLabel();
            this.metroLabel = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPageInfo = new MetroFramework.Controls.MetroTabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxTitle = new MetroFramework.Controls.MetroTextBox();
            this.tabPageSettings = new MetroFramework.Controls.MetroTabPage();
            this.metroCheckBoxExtractAudio = new MetroFramework.Controls.MetroCheckBox();
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.metroTabControl.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tabPageSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabelAction
            // 
            this.metroLabelAction.AutoSize = true;
            this.metroLabelAction.Location = new System.Drawing.Point(4, 169);
            this.metroLabelAction.Name = "metroLabelAction";
            this.metroLabelAction.Size = new System.Drawing.Size(0, 0);
            this.metroLabelAction.TabIndex = 13;
            // 
            // metroLabel
            // 
            this.metroLabel.AutoSize = true;
            this.metroLabel.Location = new System.Drawing.Point(7, 171);
            this.metroLabel.Name = "metroLabel";
            this.metroLabel.Size = new System.Drawing.Size(0, 0);
            this.metroLabel.TabIndex = 16;
            // 
            // metroTabControl
            // 
            this.metroTabControl.Controls.Add(this.tabPageInfo);
            this.metroTabControl.Controls.Add(this.tabPageSettings);
            this.metroTabControl.Location = new System.Drawing.Point(3, 2);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.SelectedIndex = 0;
            this.metroTabControl.Size = new System.Drawing.Size(604, 164);
            this.metroTabControl.TabIndex = 14;
            this.metroTabControl.UseSelectable = true;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.AutoScroll = true;
            this.tabPageInfo.BackColor = System.Drawing.Color.White;
            this.tabPageInfo.Controls.Add(this.pictureBox);
            this.tabPageInfo.Controls.Add(this.textBoxTitle);
            this.tabPageInfo.HorizontalScrollbar = true;
            this.tabPageInfo.HorizontalScrollbarBarColor = true;
            this.tabPageInfo.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageInfo.HorizontalScrollbarSize = 10;
            this.tabPageInfo.Location = new System.Drawing.Point(4, 38);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Size = new System.Drawing.Size(596, 122);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "Info";
            this.tabPageInfo.VerticalScrollbar = true;
            this.tabPageInfo.VerticalScrollbarBarColor = true;
            this.tabPageInfo.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageInfo.VerticalScrollbarSize = 10;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(16, 8);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(117, 103);
            this.pictureBox.TabIndex = 11;
            this.pictureBox.TabStop = false;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // 
            // 
            this.textBoxTitle.CustomButton.Image = null;
            this.textBoxTitle.CustomButton.Location = new System.Drawing.Point(335, 1);
            this.textBoxTitle.CustomButton.Name = "";
            this.textBoxTitle.CustomButton.Size = new System.Drawing.Size(101, 101);
            this.textBoxTitle.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxTitle.CustomButton.TabIndex = 1;
            this.textBoxTitle.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxTitle.CustomButton.UseSelectable = true;
            this.textBoxTitle.CustomButton.Visible = false;
            this.textBoxTitle.Lines = new string[] {
        "Title"};
            this.textBoxTitle.Location = new System.Drawing.Point(139, 8);
            this.textBoxTitle.MaxLength = 32767;
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.PasswordChar = '\0';
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxTitle.SelectedText = "";
            this.textBoxTitle.SelectionLength = 0;
            this.textBoxTitle.SelectionStart = 0;
            this.textBoxTitle.ShortcutsEnabled = true;
            this.textBoxTitle.Size = new System.Drawing.Size(437, 103);
            this.textBoxTitle.TabIndex = 1;
            this.textBoxTitle.Text = "Title";
            this.textBoxTitle.UseSelectable = true;
            this.textBoxTitle.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxTitle.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxTitle.Enter += new System.EventHandler(this.TextBoxTitle_GotFocus);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.White;
            this.tabPageSettings.Controls.Add(this.metroCheckBoxExtractAudio);
            this.tabPageSettings.HorizontalScrollbarBarColor = true;
            this.tabPageSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageSettings.HorizontalScrollbarSize = 10;
            this.tabPageSettings.Location = new System.Drawing.Point(4, 38);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(596, 122);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.VerticalScrollbarBarColor = true;
            this.tabPageSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageSettings.VerticalScrollbarSize = 10;
            // 
            // metroCheckBoxExtractAudio
            // 
            this.metroCheckBoxExtractAudio.AutoSize = true;
            this.metroCheckBoxExtractAudio.Location = new System.Drawing.Point(8, 8);
            this.metroCheckBoxExtractAudio.Name = "metroCheckBoxExtractAudio";
            this.metroCheckBoxExtractAudio.Size = new System.Drawing.Size(94, 15);
            this.metroCheckBoxExtractAudio.TabIndex = 13;
            this.metroCheckBoxExtractAudio.Text = "Extract Audio";
            this.metroCheckBoxExtractAudio.UseSelectable = true;
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.Location = new System.Drawing.Point(7, 199);
            this.metroProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.Size = new System.Drawing.Size(596, 17);
            this.metroProgressBar.TabIndex = 15;
            // 
            // Track
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroLabel);
            this.Controls.Add(this.metroTabControl);
            this.Controls.Add(this.metroProgressBar);
            this.Controls.Add(this.metroLabelAction);
            this.Name = "Track";
            this.Size = new System.Drawing.Size(608, 234);
            this.metroTabControl.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabelAction;
        private MetroFramework.Controls.MetroLabel metroLabel;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private MetroFramework.Controls.MetroTabPage tabPageInfo;
        private System.Windows.Forms.PictureBox pictureBox;
        private MetroFramework.Controls.MetroTextBox textBoxTitle;
        private MetroFramework.Controls.MetroTabPage tabPageSettings;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxExtractAudio;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar;
    }
}
