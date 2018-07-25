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
    }
    
    public class SpeedVisualElement : VisualElement
    {       

        public BorderBehaviour BorderBehaviour { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }
        
        private int _previousTime;
        public override void Draw(int time)
        {
            CheckBorders();
            var dtime = time - _previousTime;
            _previousTime = time;            
            double dx = dtime * SpeedX;
            double dy = dtime * SpeedY;
            X += dx;
            if (X < 0)
                X = 0;
            Y += dy;
            if (Y < 0)
                Y = 0;
            base.Draw(time);            
        }

        private void CheckBorders()
        {
            if (X >= 640 - Width || X <= 0)
            {
                if(BorderBehaviour == BorderBehaviour.BounceFromBorder)
                    SpeedX = SpeedX * -1;
                else
                {
                    SpeedX = 0;
                    SpeedY = 0;
                }
            }


            if (Y >= 480 - Height || Y <= 0)
            {
                if (BorderBehaviour == BorderBehaviour.BounceFromBorder)
                    SpeedY = SpeedY * -1;
                else
                {
                    SpeedX = 0;
                    SpeedY = 0;
                }
            }
        }

        public SpeedVisualElement(string path, MainWindow mw) : base(path, mw)
        {
        }
    }

    //public class RigidBodyVisualElement : SpeedVisualElement
    //{
    //    public RigidBodyVisualElement(string path, MainWindow mw) : base(path, mw)
    //    {
    //    }

    //    public void Collide(RigidBodyVisualElement element)
    //    {
    //        this
    //        element
    //    }
    //}


}
