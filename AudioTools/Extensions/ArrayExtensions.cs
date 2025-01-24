using System;

namespace AudioTools.Extensions
{
    public static class ArrayExtensions
    {
        public static void BlockCopy(this Array src, int srcOffset, Array dst, int dstOffset, int bytes)
        {
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, bytes);
        }

        public static T[] EnsureBufferSize<T>(this T[] buffer, int sizeNeeded)
        {
            if (buffer.Length < sizeNeeded)
            {
                Array.Resize(ref buffer, sizeNeeded);
            }

            return buffer;
        }
    }
}
