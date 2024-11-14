using System;

namespace YtEzDL.Console
{
    public class ConsoleProcessException : Exception
    {
        public int ExitCode;
        public ConsoleProcessException(int exitCode, string msg) : base(msg)
        {
            ExitCode = exitCode;
        }

        public ConsoleProcessException(int exitCode) : this(exitCode, $"ExitCode({exitCode})")
        {
        }

        public ConsoleProcessException(int exitCode, string format, params object[] arg) : this(exitCode, string.Format(format, arg))
        {

        }
    }
}
