using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace YtEzDL.UserControls
{
    public class MetroToolStripProgressBar : ToolStripControlHost, IMetroControl
    {
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground
        {
            add => ProgressBar.CustomPaintBackground += value;
            remove => ProgressBar.CustomPaintBackground -= value;
        }

        public event EventHandler<MetroPaintEventArgs> CustomPaint
        {
            add => ProgressBar.CustomPaint += value;
            remove => ProgressBar.CustomPaint -= value;
        }

        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground
        {
            add => ProgressBar.CustomPaintForeground += value;
            remove => ProgressBar.CustomPaintForeground -= value;
        }

        private static Control CreateControlInstance()
        {
            var progressBar = new MetroProgressBar
            {
                Size = new Size(100, 15),
                UseStyleColors = true,
                UseCustomBackColor = false,
                UseCustomForeColor = false,               
            };
            return progressBar;
        }

        public MetroProgressBar ProgressBar => Control as MetroProgressBar;
               
        public MetroToolStripProgressBar() : base(CreateControlInstance())
        {
        }
                
        public int Minimum { get => ProgressBar.Minimum; set => ProgressBar.Minimum = value; }
        public int Maximum { get => ProgressBar.Maximum; set => ProgressBar.Maximum = value; }
        public int Value { get => ProgressBar.Value; set => ProgressBar.Value = value; }
        public ProgressBarStyle ProgressBarStyle { get => ProgressBar.ProgressBarStyle; set => ProgressBar.ProgressBarStyle = value; }
        public MetroColorStyle Style { get => ProgressBar.Style; set => ProgressBar.Style = value; }
        public MetroThemeStyle Theme { get => ProgressBar.Theme; set => ProgressBar.Theme = value; }
        public MetroStyleManager StyleManager { get => ProgressBar.StyleManager; set => ProgressBar.StyleManager = value; }
        public bool UseCustomBackColor { get => ProgressBar.UseCustomBackColor; set => ProgressBar.UseCustomBackColor = value; }
        public bool UseCustomForeColor { get => ProgressBar.UseCustomForeColor; set => ProgressBar.UseCustomForeColor = value; }
        public bool UseStyleColors { get => ProgressBar.UseStyleColors; set => ProgressBar.UseStyleColors = value; }
        public bool UseSelectable { get => ProgressBar.UseSelectable; set => ProgressBar.UseSelectable = value; }
    }
}
