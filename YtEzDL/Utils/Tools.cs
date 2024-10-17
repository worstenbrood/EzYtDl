using System;
using System.Collections.Generic;
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

        public static string GetToolVersion(string tool, string parameter = "-version")
        {
            var output = new StringBuilder();
            var file = System.IO.Path.Combine(Path, tool);
            var consoleProcess = new ConsoleProcess(file);

            try
            {
                consoleProcess.RunAsync(new List<string> { parameter}, s => output.AppendLine(s))
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
                return output.ToString();
            }
            catch (ConsoleProcessException e)
            {
                return e.Message;
            }
        }
    }
}
