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


            elements[0] = new VisualElement(@"C:\Users\суховав\Source\Repos\wannasleep\SimpleDrawer\Drawing\red.png", this) { x = 0, y = 0 };
            elements[1] = new VisualElement(@"C:\Users\суховав\Source\Repos\wannasleep\SimpleDrawer\Drawing\green.png", this) { x = 100, y = 100 };
            elements[2] = new VisualElement(@"C:\Users\суховав\Source\Repos\wannasleep\SimpleDrawer\Drawing\blue.png", this) { x = 300, y = 300 };

        }

        VisualElement[] elements = new VisualElement[3];
    

        protected override void MainLoop(object state, EventArgs eventArgs)
        {
            Clear();
            foreach (VisualElement element in elements)
            {
                element.Draw();
                element.Delta();
            }
            
        }

        protected override Image TargetImage => DrawerImage;
    }
}
