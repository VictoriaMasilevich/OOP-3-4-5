using System.Drawing;
using System;
using AbstractClassLibrary;

namespace EllipseClassLibrary
{
    public class Ellipse : Figure
    {
        public override void GetParams()
        {
            base.GetParams();
        }

        public override void Draw(Graphics g, Color color)
        {
            GetParams();
            g.DrawEllipse(new Pen(color, widthParams), StartX, StartY, (EndX - StartX), (EndY - StartY));
        }
    }
}
