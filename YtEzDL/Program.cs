using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace YtEzDL
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var mutexName = Assembly.GetExecutingAssembly().FullName;

            // Check if mutex already exists
            if (Mutex.TryOpenExisting(mutexName, out var mutex))
            {
                var currentProcess = Process.GetCurrentProcess();
                var message = $"{currentProcess.ProcessName} (PID:{currentProcess.Id}) is already running.";
                MessageBox.Show(message, currentProcess.ProcessName);
                return;
            }

            mutex = new Mutex(true, mutexName);
            mutex.WaitOne();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ApplicationContext());
        }
    }
}
