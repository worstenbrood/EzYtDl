using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;
using MetroFramework.Interfaces;
using System.Linq;
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
            switch (control)
            {
                case IMetroControl metroControl:
                    metroControl.StyleManager = Manager;
                    break;
                case IMetroForm metroForm:
                    metroForm.StyleManager = Manager;
                    break;
                //case IMetroComponent metroComponent:
                //    metroComponent.StyleManager = Manager;
                //    break;
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

        public static void RefreshActiveForms()
        {
            // Refresh style
            foreach (var form in Application.OpenForms.OfType<MetroForm>())
            {
                form.BeginInvoke(new MethodInvoker(() => form.Refresh()));
            }
        }
    }
}
