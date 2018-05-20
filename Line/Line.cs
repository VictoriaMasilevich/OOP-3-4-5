using System.Drawing;
using System;
using AbstractClassLibrary;

namespace LineClassLibrary
{
    public class Line : Figure
    {
        public override void Draw(Graphics g, Color color)
        {
            GetParams();
            g.DrawLine(new Pen(color, widthParams), StartX, StartY, EndX, EndY);
        }
    }
}
