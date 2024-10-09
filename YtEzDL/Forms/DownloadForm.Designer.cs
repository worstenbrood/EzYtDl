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
            this.metroLabelAction = new MetroFramework.Controls.MetroLabel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // metroButtonDownload
            // 
            this.metroButtonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.metroButtonDownload.Enabled = false;
            this.metroButtonDownload.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonDownload.Location = new System.Drawing.Point(15, 451);
            this.metroButtonDownload.Margin = new System.Windows.Forms.Padding(2);
            this.metroButtonDownload.Name = "metroButtonDownload";
            this.metroButtonDownload.Size = new System.Drawing.Size(248, 34);
            this.metroButtonDownload.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonDownload.TabIndex = 3;
            this.metroButtonDownload.Text = "Download";
            this.metroButtonDownload.UseSelectable = true;
            this.metroButtonDownload.Click += new System.EventHandler(this.MetroButtonDownload_Click);
            // 
            // metroButtonCancel
            // 
            this.metroButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.metroButtonCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonCancel.Location = new System.Drawing.Point(288, 451);
            this.metroButtonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.metroButtonCancel.Name = "metroButtonCancel";
            this.metroButtonCancel.Size = new System.Drawing.Size(248, 34);
            this.metroButtonCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButtonCancel.TabIndex = 4;
            this.metroButtonCancel.Text = "Cancel";
            this.metroButtonCancel.UseSelectable = true;
            this.metroButtonCancel.Click += new System.EventHandler(this.MetroButtonCancel_Click);
            // 
            // metroLabelAction
            // 
            this.metroLabelAction.AutoSize = true;
            this.metroLabelAction.Location = new System.Drawing.Point(27, 232);
            this.metroLabelAction.Name = "metroLabelAction";
            this.metroLabelAction.Size = new System.Drawing.Size(0, 0);
            this.metroLabelAction.TabIndex = 11;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.flowLayoutPanel.Location = new System.Drawing.Point(15, 74);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(958, 351);
            this.flowLayoutPanel.TabIndex = 12;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.metroLabelAction);
            this.Controls.Add(this.metroButtonCancel);
            this.Controls.Add(this.metroButtonDownload);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(12, 60, 12, 13);
            this.Text = "DownloadForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.Resize += new System.EventHandler(this.DownloadForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroButton metroButtonDownload;
        private MetroButton metroButtonCancel;
        private MetroLabel metroLabelAction;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}