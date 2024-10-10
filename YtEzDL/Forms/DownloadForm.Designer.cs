using MetroFramework;
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNone = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonToggle = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroButtonDownload
            // 
            this.metroButtonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.metroButtonDownload.Enabled = false;
            this.metroButtonDownload.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonDownload.Location = new System.Drawing.Point(11, 349);
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
            this.metroButtonCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButtonCancel.Location = new System.Drawing.Point(219, 349);
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
            this.flowLayoutPanel.Location = new System.Drawing.Point(9, 87);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(885, 256);
            this.flowLayoutPanel.TabIndex = 12;
            this.flowLayoutPanel.WrapContents = false;
            this.flowLayoutPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.flowLayoutPanel_Layout);
            this.flowLayoutPanel.Resize += new System.EventHandler(this.flowLayoutPanel_Resize);
            // 
            // toolStrip
            // 
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNone,
            this.toolStripButtonAll,
            this.toolStripButtonToggle});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(9, 60);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(885, 22);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 13;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonNone
            // 
            this.toolStripButtonNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNone.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNone.Image")));
            this.toolStripButtonNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNone.Name = "toolStripButtonNone";
            this.toolStripButtonNone.Size = new System.Drawing.Size(40, 19);
            this.toolStripButtonNone.Text = "None";
            this.toolStripButtonNone.ToolTipText = "Select none";
            this.toolStripButtonNone.Click += new System.EventHandler(this.toolStripButtonNone_Click);
            // 
            // toolStripButtonAll
            // 
            this.toolStripButtonAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAll.Image")));
            this.toolStripButtonAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAll.Name = "toolStripButtonAll";
            this.toolStripButtonAll.Size = new System.Drawing.Size(25, 19);
            this.toolStripButtonAll.Text = "All";
            this.toolStripButtonAll.ToolTipText = "Select all";
            this.toolStripButtonAll.Click += new System.EventHandler(this.toolStripButtonAll_Click);
            // 
            // toolStripButtonToggle
            // 
            this.toolStripButtonToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonToggle.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonToggle.Image")));
            this.toolStripButtonToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonToggle.Name = "toolStripButtonToggle";
            this.toolStripButtonToggle.Size = new System.Drawing.Size(46, 19);
            this.toolStripButtonToggle.Text = "Toggle";
            this.toolStripButtonToggle.ToolTipText = "Toggle selection";
            this.toolStripButtonToggle.Click += new System.EventHandler(this.toolStripButtonToggle_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(903, 385);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.metroButtonCancel);
            this.Controls.Add(this.metroButtonDownload);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(9, 60, 9, 10);
            this.Text = "DownloadForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroButton metroButtonDownload;
        private MetroButton metroButtonCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.ToolStripButton toolStripButtonNone;
        private System.Windows.Forms.ToolStripButton toolStripButtonAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonToggle;
        private System.Windows.Forms.ToolStrip toolStrip;
    }
}