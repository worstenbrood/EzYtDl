using System.Drawing;
using System.Windows.Forms;

namespace YtEzDL.UserControls
{
    public class CustomToolStrip : ToolStrip
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Disable shade
            using (var pen = new Pen(Color.White, 2))
            {
                e.Graphics.DrawRectangle(pen, ClientRectangle);
            }
        }
    }
}
