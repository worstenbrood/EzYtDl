using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YtEzDL.Config;

namespace YtEzDL.Utils
{
    public class AppStyle
    {
        private static MetroStyleManager _manager;

        public static MetroStyleManager Manager
        {
            get
            {
                if (_manager != null)
                {
                    return _manager;
                }

                _manager = new MetroStyleManager
                {
                    Style = Configuration.Default.LayoutSettings.ColorStyle
                };
                _manager.Update();
                return _manager;
            }
        }

        public static void SetManager(Control control)
        {
            if (control is IMetroControl metroControl)
            {
                metroControl.UseStyleColors = true;
                metroControl.StyleManager = Manager;
            }
            else if (control is IMetroForm metroForm)
            {
                metroForm.StyleManager = Manager;
            }

            foreach (Control c in control.Controls)
            {
                SetManager(c);
            }

            Manager.Update();
        }

        public static void SetStyle(MetroColorStyle style)
        {
            Manager.Style = style;
            Manager.Update();
        }
    }
}
