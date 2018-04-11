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
            this.metroButtonCancel = new MetroFramework.Controls.MetroButton();
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Download
            // 
            this.Download.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Download.Location = new System.Drawing.Point(19, 192);
            this.Download.Margin = new System.Windows.Forms.Padding(2);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(100, 24);
            this.Download.Style = MetroFramework.MetroColorStyle.Blue;
            this.Download.TabIndex = 0;
            this.Download.Text = "Download";
            this.Download.UseSelectable = true;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // metroButtonCancel
            // 
            this.metroButtonCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonCancel.Location = new System.Drawing.Point(140, 192);
            this.metroButtonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.metroButtonCancel.Name = "metroButtonCancel";
            this.metroButtonCancel.Size = new System.Drawing.Size(113, 24);
            this.metroButtonCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonCancel.TabIndex = 5;
            this.metroButtonCancel.Text = "Cancel";
            this.metroButtonCancel.UseSelectable = true;
            this.metroButtonCancel.Click += new System.EventHandler(this.metroButtonCancel_Click);
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.Location = new System.Drawing.Point(19, 164);
            this.metroProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.Size = new System.Drawing.Size(619, 15);
            this.metroProgressBar.TabIndex = 6;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(23, 59);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(108, 90);
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitle.Location = new System.Drawing.Point(137, 59);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(501, 90);
            this.textBoxTitle.TabIndex = 9;
            this.textBoxTitle.Text = "Title";
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(653, 231);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.metroProgressBar);
            this.Controls.Add(this.metroButtonCancel);
            this.Controls.Add(this.Download);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(12, 60, 12, 13);
            this.Resizable = false;
            this.Text = "DownloadForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroButton Download;
        private MetroButton metroButtonCancel;
        private MetroProgressBar metroProgressBar;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox textBoxTitle;
    }
}