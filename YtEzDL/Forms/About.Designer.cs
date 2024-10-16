using YtEzDL.UserControls;

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
            this.textBoxAbout = new YtEzDL.UserControls.MetroScrollTextBox();
            this.SuspendLayout();
            // 
            // textBoxAbout
            // 
            this.textBoxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.textBoxAbout.CustomButton.Image = null;
            this.textBoxAbout.CustomButton.Location = new System.Drawing.Point(248, 1);
            this.textBoxAbout.CustomButton.Name = "";
            this.textBoxAbout.CustomButton.Size = new System.Drawing.Size(163, 163);
            this.textBoxAbout.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxAbout.CustomButton.TabIndex = 1;
            this.textBoxAbout.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxAbout.CustomButton.UseSelectable = true;
            this.textBoxAbout.CustomButton.Visible = false;
            this.textBoxAbout.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.textBoxAbout.Lines = new string[0];
            this.textBoxAbout.Location = new System.Drawing.Point(10, 63);
            this.textBoxAbout.MaxLength = 32767;
            this.textBoxAbout.Multiline = true;
            this.textBoxAbout.Name = "textBoxAbout";
            this.textBoxAbout.PasswordChar = '\0';
            this.textBoxAbout.ReadOnly = true;
            this.textBoxAbout.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxAbout.SelectedText = "";
            this.textBoxAbout.SelectionLength = 0;
            this.textBoxAbout.SelectionStart = 0;
            this.textBoxAbout.ShortcutsEnabled = true;
            this.textBoxAbout.Size = new System.Drawing.Size(412, 165);
            this.textBoxAbout.TabIndex = 0;
            this.textBoxAbout.TabStop = false;
            this.textBoxAbout.UseSelectable = true;
            this.textBoxAbout.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxAbout.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(432, 237);
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

        private MetroScrollTextBox textBoxAbout;
    }
}