using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimpleDrawer
{
    static class SpriteHelper
    {
        static byte[] LoadImage(Uri uri, int bytesPerPixel)
        {
            var image = new BitmapImage(uri);
            // Array containing pixels data
            var buffer = new byte[image.PixelHeight * image.PixelHeight * bytesPerPixel];
            // Bytes per 1 row of image
            var stride = image.PixelWidth * bytesPerPixel;
            // Write pixels to array
            image.CopyPixels(image.SourceRect, buffer, stride, 0);
            return buffer;
        }

        public static byte[] LoadImage(string path, int bytesPerPixel = 4)
        {
            return LoadImage(new Uri(path, UriKind.Relative), bytesPerPixel);
        }
    }
}
