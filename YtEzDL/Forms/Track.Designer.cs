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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
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
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(7, 171);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(0, 0);
            this.metroLabel1.TabIndex = 16;
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
            this.tabPageInfo.Location = new System.Drawing.Point(4, 38);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Size = new System.Drawing.Size(596, 122);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "Info";
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
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxTitle.Location = new System.Drawing.Point(139, 8);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(437, 103);
            this.textBoxTitle.TabIndex = 1;
            this.textBoxTitle.Text = "Title";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.White;
            this.tabPageSettings.Controls.Add(this.metroCheckBoxExtractAudio);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 35);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(596, 125);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
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
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroTabControl);
            this.Controls.Add(this.metroProgressBar);
            this.Controls.Add(this.metroLabelAction);
            this.Name = "Track";
            this.Size = new System.Drawing.Size(608, 234);
            this.metroTabControl.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabelAction;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TabPage tabPageSettings;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxExtractAudio;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar;
    }
}
