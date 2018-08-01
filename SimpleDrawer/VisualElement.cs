using SimpleDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimpleDrawer
{
    public class VisualElement
    {
        public double X = 0;
        public double Y = 0;
        byte[] pic;
        private int stride = 256;
        public int Width;
        public int Height;
        MainWindow win;

        byte[] LoadImage(Uri uri, int bytesPerPixel)
        {
            var image = new BitmapImage(uri);
            Width = image.PixelWidth;
            Height = image.PixelHeight;
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

        public virtual void Draw(int time)
        {
            win.Draw(pic, stride, (int)X, (int)Y);
        }

        public virtual void Collide(VisualElement element)
        {
        }

    }
}
