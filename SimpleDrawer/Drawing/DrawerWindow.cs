using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SimpleDrawer.Drawing
{
    public abstract class DrawerWindow : Window
    {
        #region Consts

        private const int DefaultBitmapDpi = 96;

        #endregion

        #region Private and protected members

        private readonly DispatcherTimer _mainLoopTimer;

        protected readonly WriteableBitmap Bitmap;
        protected readonly Int32Rect Rect;
        protected readonly int BytesPerPixel;
        protected readonly int Stride;
        protected readonly byte[] ImageArray;

        #endregion

        public DrawerWindow(int bitmapWidth = 640, int bitmapHeight = 480,
            DispatcherPriority priority = DispatcherPriority.Normal)
        {
            _mainLoopTimer = new DispatcherTimer(priority);
            Bitmap = new WriteableBitmap(bitmapWidth, bitmapHeight, DefaultBitmapDpi, DefaultBitmapDpi,
                PixelFormats.Bgra32, null);
            Rect  = new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight);
            // Calculate the number of bytes per pixel. 
            BytesPerPixel = (Bitmap.Format.BitsPerPixel + 7) / 8;
            // Stride is bytes per pixel times the number of pixels.
            // Stride is the byte width of a single rectangle row.
            Stride = Bitmap.PixelWidth * BytesPerPixel;
            // Create a byte array for a the entire size of bitmap.
            var arraySize = Stride * Bitmap.PixelHeight;
            // Array for drawing
            ImageArray  = new byte[arraySize];
            
            this.Loaded += OnLoaded;
        }

        /// <summary>
        /// Applies image source and starts dispatcher timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TargetImage.Source = Bitmap;
            _mainLoopTimer.Interval = TimeSpan.FromMilliseconds(1);
            _mainLoopTimer.Tick += MainLoop;
            _mainLoopTimer.Start();
        }

        /// <summary>
        /// Draws sprite fromm 1-dimensional array in window's image
        /// </summary>
        /// <param name="sprite">sprite to draw</param>
        /// <param name="stride">sprite's stride (bytes per row)</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public virtual void Draw(byte[] sprite, int stride, int x, int y)
        {
            for (int i = 0; i < sprite.Length; i += stride, y++)
            {
                if (y >= Bitmap.PixelHeight)
                    break;
                var destinationIndex = x * BytesPerPixel + y * Stride;
                var count = x * BytesPerPixel + stride > Stride ? Stride - x * BytesPerPixel : stride;
                Buffer.BlockCopy(sprite, i, ImageArray, destinationIndex, count < 0 ? 0 : count);
            }
            Bitmap.WritePixels(Rect, ImageArray, Stride, 0);
        }

        /// <summary>
        /// Draws sprite fromm 2-dimensional array in window's image
        /// </summary>
        /// <param name="sprite">sprite to draw</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public virtual void Draw(byte[,] sprite, int x, int y)
        {
            var spriteDim0 = sprite.GetLength(0);
            var spriteDim1 = sprite.GetLength(1);
            for (int j = 0; j < spriteDim0; j++)
            {
                if (j + y >= Bitmap.PixelHeight)
                    break;
                for (int i = 0; i < spriteDim1; i++)
                {
                    var destionationIndex = (i + x * BytesPerPixel);
                    if (destionationIndex >= Stride)
                        break;
                    ImageArray[destionationIndex + (j + y) * Stride] = sprite[j, i];
                }
            }
            Bitmap.WritePixels(Rect, ImageArray, Stride, 0);
        }

        /// <summary>
        /// Clears window image
        /// </summary>
        protected virtual void Clear(bool applyImmidiately = false)
        {
            Array.Clear(ImageArray, 0, ImageArray.Length);
            if(applyImmidiately)
                Bitmap.WritePixels(Rect, ImageArray, Stride, 0);
        }

        /// <summary>
        /// Called periodically by Dispatcher timer, main rendering loop
        /// </summary>
        /// <param name="state">always null</param>
        /// <param name="eventArgs"></param>
        protected abstract void MainLoop(object state, EventArgs eventArgs);

        /// <summary>
        /// Target image to draw in
        /// </summary>
        protected abstract Image TargetImage { get; }

    }
}
