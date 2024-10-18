using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Win32;

namespace YtEzDL.Utils
{
    public static class Tools
    {
        public static string GetMemberName<TDelegate>(Expression<TDelegate> p)
            where TDelegate : class, Delegate
        {
            switch (p.Body)
            {
                case MemberExpression expression:
                    return ((PropertyInfo)expression.Member).Name;

                case UnaryExpression expression:
                    return ((PropertyInfo)((MemberExpression)expression.Operand).Member).Name;

                default:
                    throw new Exception("Unhandled expression cast.");
            }
        }

        private static string _path;

        public static string Path
        {
            get
            {
                if (_path != null)
                {
                    return _path;
                }

                var fileInfo = new FileInfo(ApplicationPath);
                var directory = fileInfo.DirectoryName ?? Environment.CurrentDirectory; 
                return _path = System.IO.Path.Combine(directory, "Tools");
            }
        }

        public static string GetToolOutput(string tool, string parameter = "--version")
        {
#if DEBUG
            var start = DateTime.Now;
#endif
            var output = new StringBuilder();
            var file = System.IO.Path.Combine(Path, tool);
            var consoleProcess = new ConsoleProcess(file);

            try
            {
                consoleProcess.RunAsync(new List<string> { parameter}, s => output.AppendLine(s))
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
#if DEBUG
                Debug.WriteLine("GetToolVersion({0}): {1}ms", tool, (DateTime.Now - start).TotalMilliseconds);
#endif
                return output.ToString().TrimEnd('\r', '\n'); 
            }
            catch (ConsoleProcessException e)
            {
#if DEBUG
                Debug.WriteLine("GetToolVersion({0}): {1}ms", tool, (DateTime.Now - start).TotalMilliseconds);
#endif
                return e.Message.TrimEnd('\r', '\n'); 
            }
        }

        public static FileVersionInfo GetFileVersionInfo(string filename)
        {
            return FileVersionInfo.GetVersionInfo(filename);
        }

        public static FileVersionInfo GetToolFileVersionInfo(string tool)
        {
            return GetFileVersionInfo(System.IO.Path.Combine(Path, tool));
        }

        public static Version GetVersion<T>()
        {
            return Assembly.GetAssembly(typeof(T)).GetName().Version;
        }

        public static string ApplicationName = nameof(YtEzDL);
        public static string ApplicationPath = Assembly.GetExecutingAssembly().Location;
        
        public static string GetProductVersion()
        {
            return FileVersionInfo.GetVersionInfo(ApplicationPath).ProductVersion;
        }

        public const string YtDlp = "yt-dlp.exe";
        public const string FfMpeg = "ffmpeg.exe";

        public static string GetYtDlpVersion()
        {
            return GetToolFileVersionInfo(YtDlp).ProductVersion;
        }

        public static string GetFfMpegVersion()
        {
            return GetToolOutput(FfMpeg, "-version");
        }

        public static string ProfileFolderCombine(params string[] arg)
        {
            var paths = new[] { Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) }
                .Concat(arg)
                .ToArray();

            return System.IO.Path.Combine(paths);
        }

        public static string EzYtDlProfilePath = ProfileFolderCombine(ApplicationName);

        public static void SetAutoStart(bool autostart)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (autostart)
            {
                // Specify the name and path of your application executable, add the application to the startup
                regKey?.SetValue(ApplicationName, $"\"{ApplicationPath}\"");
            }
            else
            {
                regKey?.DeleteValue(ApplicationName, false);
            }
        }
    }
}
