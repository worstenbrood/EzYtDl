using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using WebPWrapper;

namespace YtEzDL.Utils
{
    public class ImageTools
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="boxSize"></param>
        /// <param name="backColor"></param>
        /// <returns>The resized image.</returns>
        public static Bitmap Resize(Image image, Size boxSize, Color backColor)
        {
            // Figure out the ratio
            double ratioX = boxSize.Width / (double)image.Width;
            double ratioY = boxSize.Height / (double)image.Height;
            // Use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // Now we can get the new height and width
            int newHeight = Convert.ToInt32(image.Height * ratio);
            int newWidth = Convert.ToInt32(image.Width * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((boxSize.Width - (image.Width * ratio)) / 2);
            int posY = Convert.ToInt32((boxSize.Height - (image.Height * ratio)) / 2);

            var destRect = new Rectangle(posX, posY, newWidth, newHeight);
            var destImage = new Bitmap(boxSize.Width, boxSize.Height, PixelFormat.Format24bppRgb);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.Clear(backColor);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private static readonly WebP WebP = new WebP();

#if DEBUG
        static ImageTools()
        {

            Debug.WriteLine("libWebP version: " + WebP.GetVersion());
        }
#endif

        public static Image Download(string url)
        {
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    switch (response.ContentType.ToLower())
                    {
                        case "image/webp":
                            return WebP.Decode(stream.ReadFully());
                            
                        default:
                            return Image.FromStream(stream);
                    }
                }
            }
        }
    }
}
