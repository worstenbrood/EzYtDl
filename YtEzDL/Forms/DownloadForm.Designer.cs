using MetroFramework.Controls;
using YtEzDL.UserControls;

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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNone = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonToggle = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonReset = new System.Windows.Forms.ToolStripButton();
            this.dropDownButtonSort = new System.Windows.Forms.ToolStripDropDownButton();
            this.sortByTitleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByTitleDescendingItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByLengthItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByLengthDescendingItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonDownload = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClearCache = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.flowLayoutPanel = new YtEzDL.UserControls.CustomLayoutPanel();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.White;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNone,
            this.toolStripButtonAll,
            this.toolStripButtonToggle,
            this.toolStripTextBoxSearch,
            this.toolStripButtonReset,
            this.dropDownButtonSort,
            this.toolStripButtonDownload,
            this.toolStripButtonCancel,
            this.toolStripButtonClearCache,
            this.toolStripButtonAbout});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(9, 60);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(894, 27);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 13;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonNone
            // 
            this.toolStripButtonNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNone.ForeColor = System.Drawing.Color.Black;
            this.toolStripButtonNone.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNone.Image")));
            this.toolStripButtonNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNone.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonNone.Name = "toolStripButtonNone";
            this.toolStripButtonNone.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.toolStripButtonNone.Size = new System.Drawing.Size(40, 24);
            this.toolStripButtonNone.Text = "None";
            this.toolStripButtonNone.ToolTipText = "Select none";
            this.toolStripButtonNone.Click += new System.EventHandler(this.toolStripButtonNone_Click);
            // 
            // toolStripButtonAll
            // 
            this.toolStripButtonAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAll.ForeColor = System.Drawing.Color.Black;
            this.toolStripButtonAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAll.Image")));
            this.toolStripButtonAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAll.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonAll.Name = "toolStripButtonAll";
            this.toolStripButtonAll.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.toolStripButtonAll.Size = new System.Drawing.Size(25, 24);
            this.toolStripButtonAll.Text = "All";
            this.toolStripButtonAll.ToolTipText = "Select all";
            this.toolStripButtonAll.Click += new System.EventHandler(this.toolStripButtonAll_Click);
            // 
            // toolStripButtonToggle
            // 
            this.toolStripButtonToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonToggle.ForeColor = System.Drawing.Color.Black;
            this.toolStripButtonToggle.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonToggle.Image")));
            this.toolStripButtonToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonToggle.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonToggle.Name = "toolStripButtonToggle";
            this.toolStripButtonToggle.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.toolStripButtonToggle.Size = new System.Drawing.Size(46, 24);
            this.toolStripButtonToggle.Text = "Toggle";
            this.toolStripButtonToggle.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripButtonToggle.ToolTipText = "Toggle selection";
            this.toolStripButtonToggle.Click += new System.EventHandler(this.toolStripButtonToggle_Click);
            // 
            // toolStripTextBoxSearch
            // 
            this.toolStripTextBoxSearch.BackColor = System.Drawing.Color.White;
            this.toolStripTextBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBoxSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxSearch.ForeColor = System.Drawing.Color.Black;
            this.toolStripTextBoxSearch.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.toolStripTextBoxSearch.Name = "toolStripTextBoxSearch";
            this.toolStripTextBoxSearch.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.toolStripTextBoxSearch.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxSearch.ToolTipText = "Filter";
            this.toolStripTextBoxSearch.TextChanged += new System.EventHandler(this.toolStripTextBoxSearch_TextChanged);
            // 
            // toolStripButtonReset
            // 
            this.toolStripButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReset.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReset.Image")));
            this.toolStripButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReset.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonReset.Name = "toolStripButtonReset";
            this.toolStripButtonReset.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonReset.Text = "Reset filter";
            this.toolStripButtonReset.Click += new System.EventHandler(this.toolStripButtonReset_Click);
            // 
            // dropDownButtonSort
            // 
            this.dropDownButtonSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dropDownButtonSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortByTitleItem,
            this.sortByTitleDescendingItem,
            this.sortByLengthItem,
            this.sortByLengthDescendingItem,
            this.resetItem});
            this.dropDownButtonSort.Enabled = false;
            this.dropDownButtonSort.ForeColor = System.Drawing.Color.Black;
            this.dropDownButtonSort.Image = ((System.Drawing.Image)(resources.GetObject("dropDownButtonSort.Image")));
            this.dropDownButtonSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropDownButtonSort.Name = "dropDownButtonSort";
            this.dropDownButtonSort.Size = new System.Drawing.Size(33, 24);
            this.dropDownButtonSort.Text = "Sort";
            this.dropDownButtonSort.ToolTipText = "Sort";
            // 
            // sortByTitleItem
            // 
            this.sortByTitleItem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sortByTitleItem.Name = "sortByTitleItem";
            this.sortByTitleItem.Size = new System.Drawing.Size(246, 24);
            this.sortByTitleItem.Text = "Sort by title";
            this.sortByTitleItem.ToolTipText = "Sort by title";
            this.sortByTitleItem.Click += new System.EventHandler(this.sortByTitleItem_Click);
            // 
            // sortByTitleDescendingItem
            // 
            this.sortByTitleDescendingItem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sortByTitleDescendingItem.Name = "sortByTitleDescendingItem";
            this.sortByTitleDescendingItem.Size = new System.Drawing.Size(246, 24);
            this.sortByTitleDescendingItem.Text = "Sort by title (Descending)";
            this.sortByTitleDescendingItem.ToolTipText = "Sort by title, descending";
            this.sortByTitleDescendingItem.Click += new System.EventHandler(this.sortByTitleDescendingItem_Click);
            // 
            // sortByLengthItem
            // 
            this.sortByLengthItem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sortByLengthItem.Name = "sortByLengthItem";
            this.sortByLengthItem.Size = new System.Drawing.Size(246, 24);
            this.sortByLengthItem.Text = "Sort by length";
            this.sortByLengthItem.ToolTipText = "Sort by length";
            this.sortByLengthItem.Click += new System.EventHandler(this.sortByLengthItem_Click);
            // 
            // sortByLengthDescendingItem
            // 
            this.sortByLengthDescendingItem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sortByLengthDescendingItem.Name = "sortByLengthDescendingItem";
            this.sortByLengthDescendingItem.Size = new System.Drawing.Size(246, 24);
            this.sortByLengthDescendingItem.Text = "Sort by length (Descending)";
            this.sortByLengthDescendingItem.ToolTipText = "Sort by length, descending";
            this.sortByLengthDescendingItem.Click += new System.EventHandler(this.sortByLengthDescendingItem_Click);
            // 
            // resetItem
            // 
            this.resetItem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.resetItem.Name = "resetItem";
            this.resetItem.Size = new System.Drawing.Size(246, 24);
            this.resetItem.Text = "Reset";
            this.resetItem.ToolTipText = "Reset sorting to original";
            this.resetItem.Click += new System.EventHandler(this.resetItem_Click);
            // 
            // toolStripButtonDownload
            // 
            this.toolStripButtonDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDownload.Enabled = false;
            this.toolStripButtonDownload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDownload.Image")));
            this.toolStripButtonDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDownload.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonDownload.Name = "toolStripButtonDownload";
            this.toolStripButtonDownload.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonDownload.ToolTipText = "Download";
            this.toolStripButtonDownload.Click += new System.EventHandler(this.toolStripButtonDownload_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCancel.Image")));
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonCancel.ToolTipText = "Cancel";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStripButtonClearCache
            // 
            this.toolStripButtonClearCache.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClearCache.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClearCache.Image")));
            this.toolStripButtonClearCache.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearCache.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonClearCache.Name = "toolStripButtonClearCache";
            this.toolStripButtonClearCache.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonClearCache.ToolTipText = "Clear yt-dlp cache";
            this.toolStripButtonClearCache.Click += new System.EventHandler(this.toolStripButtonClearCache_Click);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonAbout.ToolTipText = "About";
            this.toolStripButtonAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.BackColor = System.Drawing.Color.White;
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(9, 392);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(894, 25);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 15;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(131, 22);
            this.toolStripStatusLabel.Text = "Total: 0 / Selected: 0";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 21);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar.ToolTipText = "Total progress";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.flowLayoutPanel.Location = new System.Drawing.Point(9, 99);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(894, 288);
            this.flowLayoutPanel.TabIndex = 12;
            this.flowLayoutPanel.WrapContents = false;
            this.flowLayoutPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel_ControlAdded);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(912, 427);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.flowLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MinimumSize = new System.Drawing.Size(909, 400);
            this.Name = "DownloadForm";
            this.Padding = new System.Windows.Forms.Padding(9, 60, 9, 10);
            this.Text = "DownloadForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.DownloadForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.DownloadForm_Resize);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.ToolStripButton toolStripButtonNone;
        private System.Windows.Forms.ToolStripButton toolStripButtonAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonToggle;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSearch;
        private System.Windows.Forms.ToolStripButton toolStripButtonReset;
        private System.Windows.Forms.ToolStripButton toolStripButtonDownload;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearCache;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolStripDropDownButton dropDownButtonSort;
        private System.Windows.Forms.ToolStripMenuItem sortByTitleItem;
        private System.Windows.Forms.ToolStripMenuItem sortByLengthItem;
        private System.Windows.Forms.ToolStripMenuItem sortByTitleDescendingItem;
        private System.Windows.Forms.ToolStripMenuItem sortByLengthDescendingItem;
        private System.Windows.Forms.ToolStripMenuItem resetItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}