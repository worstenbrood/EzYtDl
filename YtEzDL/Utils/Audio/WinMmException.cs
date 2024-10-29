using System;
using System.Text;

namespace YtEzDL.Utils.Audio
{
    public class WinMmException : Exception
    {
        public MmResult Result { get; }

        private const int BufferSize = 256;

        public static string GetErrorText(MmResult result)
        {
            var buffer = new StringBuilder(BufferSize);
            var textResult = WinMm.waveOutGetErrorText(result, buffer, BufferSize);
            if (textResult == MmResult.NoError)
            {
                return buffer.ToString();
            }
            return $"{result} ({textResult})";
        }

        public WinMmException(MmResult result) : base(GetErrorText(result))
        {
            Result = result;
        }

        public static void Try(MmResult result)
        {
            if (result != MmResult.NoError)
            {
                throw new WinMmException(result);
            }
        }
    }
}
