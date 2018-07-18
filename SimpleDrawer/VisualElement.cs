using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrawer
{
    class VisualElement
    {
        public int x = 0;
        public int y = 0;
        private int dx = 1;
        private int dy = 1;
        byte[] pic;
        MainWindow win;
        public VisualElement(string path, MainWindow mw)
        {
            pic = SpriteHelper.LoadImage(path);
            win = mw;
        } 
        public void Draw()
        {
            win.Draw(pic, 256, x, y);
        }
        public void Delta()
        {
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
        } 
    } 

}
