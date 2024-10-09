using System.Windows.Forms;

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
            this.metroLabelAction = new MetroFramework.Controls.MetroLabel();
            this.metroLabel = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPageInfo = new MetroFramework.Controls.MetroTabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
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
            this.metroLabelAction.Location = new System.Drawing.Point(5, 208);
            this.metroLabelAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabelAction.Name = "metroLabelAction";
            this.metroLabelAction.Size = new System.Drawing.Size(0, 0);
            this.metroLabelAction.TabIndex = 13;
            // 
            // metroLabel
            // 
            this.metroLabel.AutoSize = true;
            this.metroLabel.Location = new System.Drawing.Point(9, 210);
            this.metroLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel.Name = "metroLabel";
            this.metroLabel.Size = new System.Drawing.Size(0, 0);
            this.metroLabel.TabIndex = 16;
            // 
            // metroTabControl
            // 
            this.metroTabControl.Controls.Add(this.tabPageInfo);
            this.metroTabControl.Controls.Add(this.tabPageSettings);
            this.metroTabControl.Location = new System.Drawing.Point(4, 2);
            this.metroTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.SelectedIndex = 0;
            this.metroTabControl.Size = new System.Drawing.Size(805, 202);
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
            this.tabPageInfo.HorizontalScrollbarSize = 12;
            this.tabPageInfo.Location = new System.Drawing.Point(4, 38);
            this.tabPageInfo.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Size = new System.Drawing.Size(797, 160);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "Info";
            this.tabPageInfo.VerticalScrollbar = true;
            this.tabPageInfo.VerticalScrollbarBarColor = true;
            this.tabPageInfo.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageInfo.VerticalScrollbarSize = 13;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(21, 10);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(156, 127);
            this.pictureBox.TabIndex = 11;
            this.pictureBox.TabStop = false;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxTitle.Location = new System.Drawing.Point(185, 10);
            this.textBoxTitle.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(583, 127);
            this.textBoxTitle.TabIndex = 1;
            this.textBoxTitle.Text = "Title";
            this.textBoxTitle.Enter += new System.EventHandler(this.TextBoxTitle_GotFocus);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.White;
            this.tabPageSettings.Controls.Add(this.metroCheckBoxExtractAudio);
            this.tabPageSettings.HorizontalScrollbarBarColor = true;
            this.tabPageSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageSettings.HorizontalScrollbarSize = 12;
            this.tabPageSettings.Location = new System.Drawing.Point(4, 38);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(797, 160);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.VerticalScrollbarBarColor = true;
            this.tabPageSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageSettings.VerticalScrollbarSize = 13;
            // 
            // metroCheckBoxExtractAudio
            // 
            this.metroCheckBoxExtractAudio.AutoSize = true;
            this.metroCheckBoxExtractAudio.Location = new System.Drawing.Point(11, 10);
            this.metroCheckBoxExtractAudio.Margin = new System.Windows.Forms.Padding(4);
            this.metroCheckBoxExtractAudio.Name = "metroCheckBoxExtractAudio";
            this.metroCheckBoxExtractAudio.Size = new System.Drawing.Size(101, 17);
            this.metroCheckBoxExtractAudio.TabIndex = 13;
            this.metroCheckBoxExtractAudio.Text = "Extract Audio";
            this.metroCheckBoxExtractAudio.UseSelectable = true;
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.FontSize = MetroFramework.MetroProgressBarSize.Tall;
            this.metroProgressBar.Location = new System.Drawing.Point(9, 245);
            this.metroProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.Size = new System.Drawing.Size(795, 21);
            this.metroProgressBar.TabIndex = 15;
            // 
            // Track
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.metroLabel);
            this.Controls.Add(this.metroTabControl);
            this.Controls.Add(this.metroProgressBar);
            this.Controls.Add(this.metroLabelAction);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Track";
            this.Size = new System.Drawing.Size(809, 286);
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
        private MetroFramework.Controls.MetroLabel metroLabel;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private MetroFramework.Controls.MetroTabPage tabPageInfo;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox textBoxTitle;
        private MetroFramework.Controls.MetroTabPage tabPageSettings;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxExtractAudio;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar;
    }
}
