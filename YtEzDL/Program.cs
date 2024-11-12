using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YtEzDL.DownLoad;
using YtEzDL.Tools;
using YtEzDL.Utils;

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
            /*
            const string url = "https://soundcloud.com/hate_music/premiere-znzl-the-substance-of-my-tears-ft-linn-elisabetdem008";
            using (var ffmpeg = new FfMpegStream(url, AudioFormat.Mp3))
            using (var ffPlay = new FfPlayStream(AudioFormat.Mp3)) 
                ffmpeg.CopyTo(ffPlay);
            */
            
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
