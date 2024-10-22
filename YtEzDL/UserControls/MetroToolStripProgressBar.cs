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
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;

        private static Control CreateControlInstance()
        {
            MetroProgressBar progressBar = new MetroProgressBar
            {
                Size = new Size(100, 15),
                UseStyleColors = true,
                UseCustomBackColor = false,
                UseCustomForeColor = false
            };
            return progressBar;
        }

        public MetroProgressBar ProgressBar => Control as MetroProgressBar;

        public MetroToolStripProgressBar() : base(CreateControlInstance())
        {
           
        }
                
        public int Minimum
        {
            get
            {
                return ProgressBar.Minimum;
            }
            set
            {
                ProgressBar.Minimum = value;
            }
        }

        public int Maximum
        {
            get
            {
                return ProgressBar.Maximum;
            }
            set
            {
                ProgressBar.Maximum = value;
            }
        }

        public int Value
        {
            get
            {
                return ProgressBar.Value;
            }
            set
            {
                ProgressBar.Value = value;
            }
        }

        public ProgressBarStyle ProgressBarStyle
        {
            get
            {
                return ProgressBar.ProgressBarStyle;
            }
            set
            {
                ProgressBar.ProgressBarStyle = value;
            }
        }

        public MetroColorStyle Style { get { return ProgressBar.Style; } set { ProgressBar.Style = value; } }
        public MetroThemeStyle Theme { get { return ProgressBar.Theme; } set { ProgressBar.Theme = value; } }
        public MetroStyleManager StyleManager { get { return ProgressBar.StyleManager; } set { ProgressBar.StyleManager = value; } }
        public bool UseCustomBackColor { get { return ProgressBar.UseCustomBackColor; } set { ProgressBar.UseCustomBackColor = value; } }
        public bool UseCustomForeColor { get { return ProgressBar.UseCustomForeColor; } set { ProgressBar.UseCustomForeColor = value; } }
        public bool UseStyleColors { get { return ProgressBar.UseStyleColors; } set { ProgressBar.UseStyleColors = value; } }
        public bool UseSelectable { get { return ProgressBar.UseSelectable; } set { ProgressBar.UseSelectable = value; } }
    }
}
