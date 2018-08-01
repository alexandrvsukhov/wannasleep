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
        public MainWindow()
        {
            InitializeComponent();

            // Платформа 1
            elements[0] = new SpeedVisualElement(@"Drawing\red.png", this) { X = 10, Y = 10, SpeedX = 2.5, SpeedY = 2.7, BorderBehaviour = BorderBehaviour.StopAtBorder} ;
            // Шарик
            elements[1] = new SpeedVisualElement(@"Drawing\green.png", this) { X = 100, Y = 100, SpeedX = 1, SpeedY = 2, BorderBehaviour = BorderBehaviour.BounceFromBorder };
            // Платформа 2
            elements[2] = new SpeedVisualElement(@"Drawing\blue.png", this) { X = 300, Y = 300, SpeedX = 1.7, SpeedY = 2.1, BorderBehaviour = BorderBehaviour.BounceFromBorder };

        }

        VisualElement[] elements = new VisualElement[3];

        int time = 0;
        protected override void MainLoop(object state, EventArgs eventArgs)
        {
            Clear();
            foreach (VisualElement element in elements)
            {
                element.Draw(time);
            }
            time++;
            if(Cross.Crossing(elements[0], elements[1]))
            {
                elements[1].Collide(elements[0]);
            }
            if (Cross.Crossing(elements[2], elements[1]))
            {
                elements[1].Collide(elements[2]);
            }
        }

        protected override Image TargetImage => DrawerImage;
    }

    
}
