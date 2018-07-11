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

            mass = new byte[16,16*4];
            for (int j = 0; j < 16; j++)
            {
                for (int i = 0; i < 16 * 4; i++)
                {
                    if (i % 4 == 3 || i % 4 == 2)
                        mass[j, i] = 0xFF;
                }
            }
        }

        byte[,] mass;
        int x = 0;
        int y = 0;
        int dx = 1;
        int dy = 1;
        protected override void MainLoop(object state, EventArgs eventArgs)
        {
            Clear();
            Draw(mass, x, y);
                    
            x += dx;                        
            y += dy;

            if (x >= 640 || x <= 0)
            {
                dx = dx * -1;
            }

            if (y >= 480 || y <= 0)
            {
                dy = dy * -1;
            }


            // Метод вызывается по таймеру, все рисование происходит здесь.
            // Для рисования можно вызывать перегруженный метод Draw(...),
            // первая перегрузка которого использует одномерный массив, вторая - двумерный.
            // Для одномерного нужно указать stride - количество байт на строку
            // Метод Clear() очищает картинку

            // для кодирования цвета пикселя используется формат bgra32, т.е. 4 байта на пиксель,
            // голубой, зеленый, красный, альфа-канал (прозрачность, 0xFF - полностью непрозрачный)
        }

        protected override Image TargetImage => DrawerImage;
    }
}
