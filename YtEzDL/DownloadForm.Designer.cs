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
            this.Download = new MetroFramework.Controls.MetroButton();
            this.metroLabelTitle = new MetroFramework.Controls.MetroLabel();
            this.metroLabelTitleContent = new MetroFramework.Controls.MetroLabel();
            this.metroLabelUrl = new MetroFramework.Controls.MetroLabel();
            this.metroLabelUrlContent = new MetroFramework.Controls.MetroLabel();
            this.metroButtonCancel = new MetroFramework.Controls.MetroButton();
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.SuspendLayout();
            // 
            // Download
            // 
            this.Download.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Download.Location = new System.Drawing.Point(38, 233);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(189, 37);
            this.Download.Style = MetroFramework.MetroColorStyle.Blue;
            this.Download.TabIndex = 0;
            this.Download.Text = "Download";
            this.Download.UseSelectable = true;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // metroLabelTitle
            // 
            this.metroLabelTitle.AutoSize = true;
            this.metroLabelTitle.Location = new System.Drawing.Point(38, 80);
            this.metroLabelTitle.Name = "metroLabelTitle";
            this.metroLabelTitle.Size = new System.Drawing.Size(36, 19);
            this.metroLabelTitle.TabIndex = 1;
            this.metroLabelTitle.Text = "Title:";
            // 
            // metroLabelTitleContent
            // 
            this.metroLabelTitleContent.AutoSize = true;
            this.metroLabelTitleContent.Location = new System.Drawing.Point(38, 105);
            this.metroLabelTitleContent.Name = "metroLabelTitleContent";
            this.metroLabelTitleContent.Size = new System.Drawing.Size(55, 19);
            this.metroLabelTitleContent.TabIndex = 2;
            this.metroLabelTitleContent.Text = "Content";
            // 
            // metroLabelUrl
            // 
            this.metroLabelUrl.AutoSize = true;
            this.metroLabelUrl.Location = new System.Drawing.Point(38, 131);
            this.metroLabelUrl.Name = "metroLabelUrl";
            this.metroLabelUrl.Size = new System.Drawing.Size(29, 19);
            this.metroLabelUrl.TabIndex = 3;
            this.metroLabelUrl.Text = "Url:";
            // 
            // metroLabelUrlContent
            // 
            this.metroLabelUrlContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabelUrlContent.AutoSize = true;
            this.metroLabelUrlContent.Location = new System.Drawing.Point(38, 156);
            this.metroLabelUrlContent.Name = "metroLabelUrlContent";
            this.metroLabelUrlContent.Size = new System.Drawing.Size(55, 19);
            this.metroLabelUrlContent.TabIndex = 4;
            this.metroLabelUrlContent.Text = "Content";
            // 
            // metroButtonCancel
            // 
            this.metroButtonCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonCancel.Location = new System.Drawing.Point(254, 233);
            this.metroButtonCancel.Name = "metroButtonCancel";
            this.metroButtonCancel.Size = new System.Drawing.Size(189, 37);
            this.metroButtonCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonCancel.TabIndex = 5;
            this.metroButtonCancel.Text = "Cancel";
            this.metroButtonCancel.UseSelectable = true;
            this.metroButtonCancel.Click += new System.EventHandler(this.metroButtonCancel_Click);
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressBar.Location = new System.Drawing.Point(38, 191);
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.Size = new System.Drawing.Size(823, 23);
            this.metroProgressBar.TabIndex = 6;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(884, 289);
            this.Controls.Add(this.metroProgressBar);
            this.Controls.Add(this.metroButtonCancel);
            this.Controls.Add(this.metroLabelUrlContent);
            this.Controls.Add(this.metroLabelUrl);
            this.Controls.Add(this.metroLabelTitleContent);
            this.Controls.Add(this.metroLabelTitle);
            this.Controls.Add(this.Download);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.MaximizeBox = false;
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(20, 61, 20, 20);
            this.Resizable = false;
            this.Text = "DownloadForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroButton Download;
        private MetroLabel metroLabelTitle;
        private MetroLabel metroLabelTitleContent;
        private MetroLabel metroLabelUrl;
        private MetroLabel metroLabelUrlContent;
        private MetroButton metroButtonCancel;
        private MetroProgressBar metroProgressBar;
    }
}