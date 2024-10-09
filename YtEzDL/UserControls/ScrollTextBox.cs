using System.Windows.Forms;
using YtEzDL.Utils;

namespace YtEzDL.UserControls
{
    /// <summary>
    /// TextBox which redirects scroll events to parent
    /// </summary>

    public class ScrollTextBox : TextBox
    {
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // Redirect scroll to parent
                case Win32.MouseWheel:
                    if (m.HWnd == Handle)
                    {
                        Win32.PostMessage(Parent.Handle, m.Msg, m.WParam, m.LParam);
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
