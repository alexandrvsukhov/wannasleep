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
        }

        protected override void MainLoop(object state, EventArgs eventArgs)
        {
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
