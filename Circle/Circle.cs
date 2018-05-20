using System.Drawing;
using System;
using AbstractClassLibrary;

namespace CircleClassLibrary
{
    public class Circle : Figure
    {
        private int diameter;

        public override void  GetParams()
        {
            base.GetParams();
            diameter = (Math.Abs(StartX - EndX) + Math.Abs(StartY - EndY)) / 2;
        }

        public override void Draw(Graphics g, Color color)
        {
            GetParams();
            if (((StartX - EndX) > 0) && ((StartY - EndX) > 0))    
            {
                g.DrawEllipse(new Pen(color, widthParams), StartX - (StartX - EndX), StartY - (StartY - EndY), diameter, diameter);
            }
            if (((StartX - EndX) > 0) && ((StartY - EndX) < 0))
            {
                g.DrawEllipse(new Pen(color, widthParams), StartX - (StartX - EndX), StartY, diameter, diameter);
            }
            if (((StartX - EndX) < 0) && ((StartY - EndX) > 0))
            {
                g.DrawEllipse(new Pen(color, widthParams), StartX, StartY - (StartY - EndY), diameter, diameter);
            }
            if (((StartX - EndX) < 0) && ((StartY - EndX) < 0))       
            {
                g.DrawEllipse(new Pen(color, widthParams), StartX, StartY, diameter, diameter);
            }
        }
    }
}
