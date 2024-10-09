using MetroFramework.Controls;

namespace YtEzDL.Forms
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // metroButtonDownload
            // 
            this.metroButtonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.metroButtonDownload.Enabled = false;
            this.metroButtonDownload.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonDownload.Location = new System.Drawing.Point(11, 367);
            this.metroButtonDownload.Margin = new System.Windows.Forms.Padding(2);
            this.metroButtonDownload.Name = "metroButtonDownload";
            this.metroButtonDownload.Size = new System.Drawing.Size(186, 26);
            this.metroButtonDownload.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonDownload.TabIndex = 3;
            this.metroButtonDownload.Text = "Download";
            this.metroButtonDownload.UseSelectable = true;
            this.metroButtonDownload.Click += new System.EventHandler(this.MetroButtonDownload_Click);
            // 
            // metroButtonCancel
            // 
            this.metroButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.metroButtonCancel.Enabled = false;
            this.metroButtonCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonCancel.Location = new System.Drawing.Point(219, 367);
            this.metroButtonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.metroButtonCancel.Name = "metroButtonCancel";
            this.metroButtonCancel.Size = new System.Drawing.Size(186, 26);
            this.metroButtonCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonCancel.TabIndex = 4;
            this.metroButtonCancel.Text = "Cancel";
            this.metroButtonCancel.UseSelectable = true;
            this.metroButtonCancel.Click += new System.EventHandler(this.MetroButtonCancel_Click);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.flowLayoutPanel.Location = new System.Drawing.Point(11, 57);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(728, 294);
            this.flowLayoutPanel.TabIndex = 12;
            this.flowLayoutPanel.WrapContents = false;
            this.flowLayoutPanel.Resize += new System.EventHandler(this.flowLayoutPanel_Resize);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(750, 405);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.metroButtonCancel);
            this.Controls.Add(this.metroButtonDownload);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(9, 46, 9, 10);
            this.Text = "DownloadForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroButton metroButtonDownload;
        private MetroButton metroButtonCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}