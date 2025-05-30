﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Win32;

namespace YtEzDL.Utils
{
    public static class CommonTools
    {
        public static string GetMemberName<TDelegate>(this Expression<TDelegate> p)
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

        public static string ApplicationName = nameof(YtEzDL);
        public static string ApplicationPath = Assembly.GetExecutingAssembly().Location;
        public static string ApplicationProductVersion = GetFileVersionInfo(ApplicationPath).ProductVersion;
        public static string EzYtDlProfilePath = ProfileFolderCombine(ApplicationName);

        private static string _toolsPath;

        public static string ToolsPath
        {
            get
            {
                if (_toolsPath != null)
                {
                    return _toolsPath;
                }

                var fileInfo = new FileInfo(ApplicationPath);
                var directory = fileInfo.DirectoryName ?? Environment.CurrentDirectory; 
                return _toolsPath = Path.Combine(directory, "Tools");
            }
        }
        
        public static FileVersionInfo GetFileVersionInfo(string filename)
        {
            return FileVersionInfo.GetVersionInfo(filename);
        }

        public static Version GetVersion<T>()
        {
            return Assembly.GetAssembly(typeof(T)).GetName().Version;
        }
        
        public static string ProfileFolderCombine(params string[] arg)
        {
            var paths = new[] { Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) }
                .Concat(arg)
                .ToArray();

            return Path.Combine(paths);
        }
        
        private const string RunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public static void SetAutoStart(bool autostart)
        {
            using (var regKey = Registry.CurrentUser.OpenSubKey(RunKey, true))
            {
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
        
        public static string RemoveInvalidChars(this string name, IEnumerable<char> invalidChars, char replaceChar = '-')
        {
            return invalidChars
                .Join(name, c => c, c => c, (c, d) => c)
                .Aggregate(name, (current, c) => current.Replace(c, replaceChar));
        }

        public static string RemoveInvalidPathChars(this string name, char replaceChar = '-')
        {
            return name.RemoveInvalidChars(Path.GetInvalidPathChars().Append(':'), replaceChar);
        }

        public static string GetValidPath(this string s)
        {
            return s.Normalize(NormalizationForm.FormC).RemoveInvalidPathChars();
        }
    }
}
