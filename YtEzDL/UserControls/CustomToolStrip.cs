using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YtEzDL.UserControls
{
    public class CustomToolStrip : ToolStrip
    {
        [Description("Show border"), Category("Appearance")]
        public bool ShowBorder { get; set; }

        [Description("Border size"), Category("Appearance")]
        public int BorderSize { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!ShowBorder)
            {
                return;
            }

            // Disable shade
            using (var pen = new Pen(Color.Black, BorderSize))
            {
                e.Graphics.DrawRectangle(pen, ClientRectangle);
                //e.Graphics.DrawLine(pen, ClientRectangle.X + Width - 1, ClientRectangle.Y, ClientRectangle.X + Width, ClientRectangle.Y + Height);
            }
        }
    }
}
