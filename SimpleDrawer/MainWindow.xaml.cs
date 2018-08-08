using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleDrawer.Drawing;


namespace SimpleDrawer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DrawerWindow
    {
        int _topPlayerScore = 0;
        int _bottomPlayerScore = 0;

        Random rnd = new Random(DateTime.Now.Millisecond);
        
        //int speedballY = rnd.Next();

        public MainWindow() : base(1280, 720)
        {
            InitializeComponent();
            InitGame();
        }

        private void Ball_Scored(object sender, EventArgs e)
        {
            _mainLoopTimer.Stop();
            _ready = false;
            var element = sender as SpeedVisualElement;
            if (element.Y < Bitmap.PixelHeight / 2 ) _topPlayerScore += 1;
            else _bottomPlayerScore += 1;
            element.Scored -= Ball_Scored;
            InitGame();            
        }

        private void InitGame()
        {
            var width = Bitmap.PixelWidth;
            var height = Bitmap.PixelHeight;
            time = 0;
            elements = new VisualElement[3];
            // Платформа 1
            elements[0] = new SpeedVisualElement(@"Drawing\Платформа.png", this) {
                X = Bitmap.PixelWidth / 2 - 128,
                Y = 16,
                SpeedY = 0,
                BorderBehaviour = BorderBehaviour.StopAtBorder,
                HeightLimit = height,
                WidthLimit = width,
            };
            // Шарик
            var ball = new CollidbleVisualElement(@"Drawing\green.png", this) {
                X = Bitmap.PixelWidth / 2 -32,
                Y = Bitmap.PixelHeight / 2 - 32,
                SpeedX = 3 * (rnd.Next(9) > 4 ? 1 : -1),
                SpeedY = 3 * (rnd.Next(9) > 4 ? 1 : -1),
                BorderBehaviour = BorderBehaviour.BounceFromBorder,
                HeightLimit = height,
                WidthLimit = width,
            };
            elements[1] = ball;

            // Платформа 2
            elements[2] = new SpeedVisualElement(@"Drawing\Платформа.png", this) {
                X = Bitmap.PixelWidth / 2 - 128,
                Y = Bitmap.PixelHeight - 32,
                SpeedY = 0, BorderBehaviour = BorderBehaviour.StopAtBorder,
                HeightLimit = height,
                WidthLimit = width,
            };

            ball.Scored += Ball_Scored;
            _mainLoopTimer.Start();
        }

        VisualElement[] elements;
        bool _ready = false;



        int time = 0;
        protected override void MainLoop(object state, EventArgs eventArgs)
        {
            
            Clear();
            foreach (VisualElement element in elements)
            {
                element.Draw(time);
            }
            if(_ready)
                time++;
            if (Cross.Crossing(elements[0], elements[1]))
            {
                elements[1].Collide(elements[0]);
            }
            if (Cross.Crossing(elements[2], elements[1]))
            {
                elements[1].Collide(elements[2]);
            }
            TopPlayerScore.Text = _topPlayerScore.ToString();
            BottomPlayerScore.Text = _bottomPlayerScore.ToString();
        }

        protected override Image TargetImage => DrawerImage;

        private void DrawerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            _ready = true;
            switch (e.Key)
            {
                case Key.Right:
                    (elements[0] as SpeedVisualElement).SpeedX = 3;
                    break;
                case Key.Left:
                    (elements[0] as SpeedVisualElement).SpeedX = -3;
                    break;
                case Key.D:
                    (elements[2] as SpeedVisualElement).SpeedX = 3;
                    break;
                case Key.A:
                    (elements[2] as SpeedVisualElement).SpeedX = -3;
                    break;
            }
        }

        private void DrawerWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    (elements[0] as SpeedVisualElement).SpeedX = 0;
                    break;
                case Key.Left:
                    (elements[0] as SpeedVisualElement).SpeedX = 0;
                    break;
                case Key.D:
                    (elements[2] as SpeedVisualElement).SpeedX = 0;
                    break;
                case Key.A:
                    (elements[2] as SpeedVisualElement).SpeedX = 0;
                    break;
            }
        }
    }


}
