using System.IO;

namespace YtEzDL.Utils
{
    public static class Tools
    {
        public static byte[] ReadFully(this Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
