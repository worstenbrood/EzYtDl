using MetroFramework.Controls;

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
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.metroTabControl.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
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
            this.pictureBox.BackgroundImage = global::YtEzDL.Properties.Resources.Play;
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox.Location = new System.Drawing.Point(16, 21);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(284, 138);
            this.pictureBox.TabIndex = 11;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            this.pictureBox.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
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
            // metroProgressBar
            // 
            this.metroProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressBar.FontSize = MetroFramework.MetroProgressBarSize.Tall;
            this.metroProgressBar.Location = new System.Drawing.Point(16, 246);
            this.metroProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.metroProgressBar.Name = "_metroProgressBar";
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Track";
            this.Size = new System.Drawing.Size(659, 283);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Track_MouseClick);
            this.Resize += new System.EventHandler(this.Track_Resize);
            this.metroTabControl.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private MetroFramework.Controls.MetroTabPage tabPageInfo;
        private System.Windows.Forms.PictureBox pictureBox;
        private ScrollTextBox textBoxTitle;
        private MetroProgressBar metroProgressBar;
    }
}
