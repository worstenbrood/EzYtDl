using System.Windows.Forms;

namespace YtEzDL.UserControls
{
    internal class CustomLayoutPanel : FlowLayoutPanel
    {
        public CustomLayoutPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            // DoEvents eliminates flickering while scrolling the panel.
            Application.DoEvents();

            // PerformLayout ensures all child user controls added to this control are visible.
            PerformLayout();

            base.OnScroll(se);
        }
    }
}
