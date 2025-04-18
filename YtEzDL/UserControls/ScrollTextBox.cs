﻿using System;
using System.Windows.Forms;
using MetroFramework.Controls;
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
                    Win32.PostMessage(Parent.Handle, m.Msg, m.WParam, m.LParam);
                    break;

                // Hide caret
                case Win32.SetFocus:
                    Win32.HideCaret(m.HWnd);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }

    public class MetroScrollTextBox : MetroTextBox
    {
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Cursor = Cursors.Default;
            Win32.HideCaret(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // Redirect scroll to parent
                case Win32.MouseWheel:
                    Win32.PostMessage(Parent.Handle, m.Msg, m.WParam, m.LParam);
                    break;

                // Hide caret
                case Win32.SetFocus:
                    Win32.HideCaret(m.HWnd);
                    goto default;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
