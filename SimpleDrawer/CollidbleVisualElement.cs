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

        DateTime _lastCollide;
        public override void Collide(VisualElement bounse)
        {
            if ((DateTime.Now - _lastCollide).TotalSeconds < 1)
                return;
            SpeedY = SpeedY * -1;
            _lastCollide = DateTime.Now;
        }
    }
}
