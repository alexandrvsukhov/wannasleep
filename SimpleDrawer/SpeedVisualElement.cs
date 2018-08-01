﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrawer
{
    class SpeedVisualElement : VisualElement
    {
            public SpeedVisualElement(string path, MainWindow mw) : base(path, mw)
            {
            }
            public BorderBehaviour BorderBehaviour { get; set; }
            public double SpeedX { get; set; }
            public double SpeedY { get; set; }
            private int _previousTime;

        public override void Draw(int time)
            {
                CheckBorders();
                var dtime = time - _previousTime;
                _previousTime = time;
                double dx = dtime * SpeedX;
                double dy = dtime * SpeedY;
                X += dx;
                if (X < 0)
                    X = 0;
                Y += dy;
                if (Y < 0)
                    Y = 0;
                base.Draw(time);
            }

            private void CheckBorders()
            {
                if (X >= 640 - Width || X <= 0)
                {
                    if (BorderBehaviour == BorderBehaviour.BounceFromBorder)
                        SpeedX = SpeedX * -1;
                    else
                    {
                        SpeedX = 0;
                        SpeedY = 0;
                    }
                }


                if (Y >= 480 - Height || Y <= 0)
                {
                    if (BorderBehaviour == BorderBehaviour.BounceFromBorder)
                        SpeedY = SpeedY * -1;
                    else
                    {
                        SpeedX = 0;
                        SpeedY = 0;
                    }
                }
            }
        }
}