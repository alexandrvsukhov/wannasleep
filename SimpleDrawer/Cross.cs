using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimpleDrawer
{
    public static class Cross
    {
        
        public static bool Crossing(VisualElement element1, VisualElement element2)
        {
            Rectangle rectangle1 = new Rectangle((int)element1.X, (int)element1.Y, element1.Width, element1.Height);
            Rectangle rectangle2 = new Rectangle((int)element2.X, (int)element2.Y, element2.Width, element2.Height);
            return rectangle1.IntersectsWith(rectangle2);
        }
    }
}
