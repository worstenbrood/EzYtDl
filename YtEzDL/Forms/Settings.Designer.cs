namespace YtEzDL.Forms
{
    partial class Settings
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
            this.metroTextBoxPath = new MetroFramework.Controls.MetroTextBox();
            this.metroLabelPath = new MetroFramework.Controls.MetroLabel();
            this.metroCheckBoxExtractAudio = new MetroFramework.Controls.MetroCheckBox();
            this.SuspendLayout();
            // 
            // metroTextBoxPath
            // 
            // 
            // 
            // 
            this.metroTextBoxPath.CustomButton.Image = null;
            this.metroTextBoxPath.CustomButton.Location = new System.Drawing.Point(661, 1);
            this.metroTextBoxPath.CustomButton.Name = "";
            this.metroTextBoxPath.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBoxPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBoxPath.CustomButton.TabIndex = 1;
            this.metroTextBoxPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBoxPath.CustomButton.UseSelectable = true;
            this.metroTextBoxPath.Lines = new string[0];
            this.metroTextBoxPath.Location = new System.Drawing.Point(86, 66);
            this.metroTextBoxPath.MaxLength = 32767;
            this.metroTextBoxPath.Name = "metroTextBoxPath";
            this.metroTextBoxPath.PasswordChar = '\0';
            this.metroTextBoxPath.ReadOnly = true;
            this.metroTextBoxPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBoxPath.SelectedText = "";
            this.metroTextBoxPath.SelectionLength = 0;
            this.metroTextBoxPath.SelectionStart = 0;
            this.metroTextBoxPath.ShortcutsEnabled = true;
            this.metroTextBoxPath.ShowButton = true;
            this.metroTextBoxPath.Size = new System.Drawing.Size(683, 23);
            this.metroTextBoxPath.TabIndex = 0;
            this.metroTextBoxPath.UseSelectable = true;
            this.metroTextBoxPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBoxPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabelPath
            // 
            this.metroLabelPath.AutoSize = true;
            this.metroLabelPath.Location = new System.Drawing.Point(43, 66);
            this.metroLabelPath.Name = "metroLabelPath";
            this.metroLabelPath.Size = new System.Drawing.Size(37, 19);
            this.metroLabelPath.TabIndex = 1;
            this.metroLabelPath.Text = "Path:";
            // 
            // metroCheckBoxExtractAudio
            // 
            this.metroCheckBoxExtractAudio.AutoSize = true;
            this.metroCheckBoxExtractAudio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.metroCheckBoxExtractAudio.FontWeight = MetroFramework.MetroCheckBoxWeight.Light;
            this.metroCheckBoxExtractAudio.Location = new System.Drawing.Point(47, 95);
            this.metroCheckBoxExtractAudio.Name = "metroCheckBoxExtractAudio";
            this.metroCheckBoxExtractAudio.Size = new System.Drawing.Size(101, 19);
            this.metroCheckBoxExtractAudio.TabIndex = 2;
            this.metroCheckBoxExtractAudio.Text = "Extract audio";
            this.metroCheckBoxExtractAudio.UseSelectable = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.metroCheckBoxExtractAudio);
            this.Controls.Add(this.metroLabelPath);
            this.Controls.Add(this.metroTextBoxPath);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox metroTextBoxPath;
        private MetroFramework.Controls.MetroLabel metroLabelPath;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxExtractAudio;
    }
}