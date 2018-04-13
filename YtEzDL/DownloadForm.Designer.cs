using MetroFramework;
using MetroFramework.Controls;

namespace YtEzDL
{
    partial class DownloadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
            this.metroButtonDownload = new MetroFramework.Controls.MetroButton();
            this.metroButtonCancel = new MetroFramework.Controls.MetroButton();
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.metroTabControl.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // metroButtonDownload
            // 
            this.metroButtonDownload.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonDownload.Location = new System.Drawing.Point(27, 260);
            this.metroButtonDownload.Margin = new System.Windows.Forms.Padding(2);
            this.metroButtonDownload.Name = "metroButtonDownload";
            this.metroButtonDownload.Size = new System.Drawing.Size(100, 24);
            this.metroButtonDownload.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonDownload.TabIndex = 0;
            this.metroButtonDownload.Text = "Download";
            this.metroButtonDownload.UseSelectable = true;
            this.metroButtonDownload.Click += new System.EventHandler(this.MetroButtonDownload_Click);
            // 
            // metroButtonCancel
            // 
            this.metroButtonCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonCancel.Location = new System.Drawing.Point(143, 260);
            this.metroButtonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.metroButtonCancel.Name = "metroButtonCancel";
            this.metroButtonCancel.Size = new System.Drawing.Size(113, 24);
            this.metroButtonCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonCancel.TabIndex = 5;
            this.metroButtonCancel.Text = "Cancel";
            this.metroButtonCancel.UseSelectable = true;
            this.metroButtonCancel.Click += new System.EventHandler(this.MetroButtonCancel_Click);
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.Location = new System.Drawing.Point(27, 232);
            this.metroProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.Size = new System.Drawing.Size(596, 15);
            this.metroProgressBar.TabIndex = 6;
            // 
            // metroTabControl
            // 
            this.metroTabControl.Controls.Add(this.tabPageInfo);
            this.metroTabControl.Controls.Add(this.tabPageSettings);
            this.metroTabControl.Location = new System.Drawing.Point(23, 63);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.SelectedIndex = 0;
            this.metroTabControl.Size = new System.Drawing.Size(604, 164);
            this.metroTabControl.TabIndex = 10;
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
            this.textBoxTitle.Location = new System.Drawing.Point(139, 8);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(437, 103);
            this.textBoxTitle.TabIndex = 10;
            this.textBoxTitle.Text = "Title";
            this.textBoxTitle.GotFocus += new System.EventHandler(this.TextBoxTitle_GotFocus);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.White;
            this.tabPageSettings.Location = new System.Drawing.Point(4, 38);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(596, 122);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(653, 297);
            this.Controls.Add(this.metroTabControl);
            this.Controls.Add(this.metroProgressBar);
            this.Controls.Add(this.metroButtonCancel);
            this.Controls.Add(this.metroButtonDownload);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(12, 60, 12, 13);
            this.Resizable = false;
            this.Text = "DownloadForm";
            this.metroTabControl.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroButton metroButtonDownload;
        private MetroButton metroButtonCancel;
        private MetroProgressBar metroProgressBar;
        private MetroTabControl metroTabControl;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TabPage tabPageSettings;
    }
}