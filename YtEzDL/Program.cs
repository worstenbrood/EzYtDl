using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using YtEzDL.DownLoad;
using YtEzDL.Tools;

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
            const string url = "https://soundcloud.com/phobiqrecordings/1joyhauser-entropy-spartaque-remix-master";
            var start = DateTime.Now;
            using (var ffmpeg = new FfMpegStream(url, AudioFormat.S16Le))
            {
                using (var file = File.Create("output.raw"))
                {
                    ffmpeg.CopyTo(file);
                }
            }
            Debug.WriteLine($"{(DateTime.Now - start).TotalMilliseconds} ms");
            
            var mutexName = Assembly.GetExecutingAssembly().FullName;

            // Check if mutex already exists
            if (Mutex.TryOpenExisting(mutexName, out _))
            {
                var currentProcess = Process.GetCurrentProcess();
                var message = $"{currentProcess.ProcessName} is already running.";
                MessageBox.Show(message, currentProcess.ProcessName);
                return;
            }

            using (var mutex = new Mutex(true, mutexName))
            {
                mutex.WaitOne();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Utils.ApplicationContext());
            }
        }
    }
}
