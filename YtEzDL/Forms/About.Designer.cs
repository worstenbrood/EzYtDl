using MetroFramework.Controls;

namespace YtEzDL.Forms
{
    partial class About
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
            this.textBoxAbout = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // textBoxAbout
            // 
            this.textBoxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAbout.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.textBoxAbout.Location = new System.Drawing.Point(10, 63);
            this.textBoxAbout.Name = "textBoxAbout";
            this.textBoxAbout.Size = new System.Drawing.Size(457, 237);
            this.textBoxAbout.TabIndex = 0;
            this.textBoxAbout.UseSelectable = true;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(477, 308);
            this.Controls.Add(this.textBoxAbout);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.Resizable = false;
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroLabel textBoxAbout;
    }
}