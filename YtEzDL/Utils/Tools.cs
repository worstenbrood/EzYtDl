using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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

                var assembly = Assembly.GetExecutingAssembly();
                var fileInfo = new FileInfo(assembly.Location);
                return _path = System.IO.Path.Combine(fileInfo.DirectoryName, "Tools");
            }
        }

        public static string GetToolVersion(string tool, string parameter = "--version")
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

        public static FileVersionInfo GetToolFileVersionInfo(string tool)
        {
            return FileVersionInfo.GetVersionInfo(System.IO.Path.Combine(Path, tool));
        }

        public const string YtDlp = "yt-dlp.exe";
        public const string FfMpeg = "ffmpeg.exe";

        public static string GetYtDlpVersion()
        {
            return GetToolFileVersionInfo(YtDlp).ProductVersion;
        }

        public static string GetFfMpegVersion()
        {
            return GetToolVersion(FfMpeg, "-version");
        }
    }
}
