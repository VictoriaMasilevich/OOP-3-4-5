using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Paint;

namespace FigurePlug
{
    [Serializable]
    public class Circle : Ellipse
    {
        private int diameter;
        public override Figure CreateFigure()
        {
            return new Circle();
        }

        public override void GetParams()
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
