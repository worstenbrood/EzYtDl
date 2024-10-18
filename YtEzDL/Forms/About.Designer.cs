using System.Windows.Forms;
using MetroFramework.Controls;
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
            this.textBoxAbout = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // textBoxAbout
            // 
            this.textBoxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAbout.BackColor = System.Drawing.Color.White;
            this.textBoxAbout.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // 
            // 
            this.textBoxAbout.CustomButton.Image = null;
            this.textBoxAbout.CustomButton.Location = new System.Drawing.Point(476, 1);
            this.textBoxAbout.CustomButton.Name = "";
            this.textBoxAbout.CustomButton.Size = new System.Drawing.Size(239, 239);
            this.textBoxAbout.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxAbout.CustomButton.TabIndex = 1;
            this.textBoxAbout.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxAbout.CustomButton.UseSelectable = true;
            this.textBoxAbout.CustomButton.Visible = false;
            this.textBoxAbout.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.textBoxAbout.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.textBoxAbout.ForeColor = System.Drawing.Color.Black;
            this.textBoxAbout.Lines = new string[0];
            this.textBoxAbout.Location = new System.Drawing.Point(23, 63);
            this.textBoxAbout.MaxLength = 32767;
            this.textBoxAbout.Multiline = true;
            this.textBoxAbout.Name = "textBoxAbout";
            this.textBoxAbout.PasswordChar = '\0';
            this.textBoxAbout.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxAbout.SelectedText = "";
            this.textBoxAbout.SelectionLength = 0;
            this.textBoxAbout.SelectionStart = 0;
            this.textBoxAbout.ShortcutsEnabled = true;
            this.textBoxAbout.Size = new System.Drawing.Size(716, 241);
            this.textBoxAbout.TabIndex = 0;
            this.textBoxAbout.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxAbout.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxAbout.TextChanged += new System.EventHandler(this.textBoxAbout_TextChanged);
            this.textBoxAbout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAbout_KeyPress);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(762, 326);
            this.Controls.Add(this.textBoxAbout);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.Resizable = false;
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox textBoxAbout;
    }
}