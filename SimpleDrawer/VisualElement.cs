using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimpleDrawer
{
    class VisualElement
    {
        public int x = 0;
        public int y = 0;
        private int dx = 1;
        private int dy = 1;
        byte[] pic;
        private int stride = 256;
        MainWindow win;
        
        byte[] LoadImage(Uri uri, int bytesPerPixel)
        {
            var image = new BitmapImage(uri);
            // Array containing pixels data
            var buffer = new byte[image.PixelWidth * image.PixelHeight * bytesPerPixel];
            // Bytes per 1 row of image
            stride = image.PixelWidth * bytesPerPixel;
            // Write pixels to array
            image.CopyPixels(image.SourceRect, buffer, stride, 0);
            return buffer;
        }

        public byte[] LoadImage(string path, int bytesPerPixel = 4)
        {
            return LoadImage(new Uri(path, UriKind.Relative), bytesPerPixel);
        }

        public VisualElement(string path, MainWindow mw)
        {
            pic = LoadImage(path);
            win = mw;
        }
        public void Draw()
        {
            win.Draw(pic, stride, x, y);
        }
        public void Delta()
        {

            x += dx;
            y += dy;

            if (x >= win.Bitmap.PixelWidth || x <= 0)
            {
                dx = dx * -1;
            }

            if (y >= win.Bitmap.PixelHeight || y <= 0)
            {
                dy = dy * -1;
            }
        }
    }

}
