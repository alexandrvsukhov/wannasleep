using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrawer
{
    class CollidbleVisualElement : SpeedVisualElement
    {
        public CollidbleVisualElement(string path, MainWindow mw) : base(path, mw)
        {
        }
        public override void Collide(VisualElement bounse)
        {
            SpeedX = SpeedX * -1;
        }
    }
}
