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
    public class Square : Figure
    {
        private int length;

        public override Figure CreateFigure()
        {
            return new Square();
        }

        public override void GetParams()
        {
            base.GetParams();
            length = (Math.Abs(StartX - EndX) + Math.Abs(StartY - EndY)) / 2;
        }

        public override void Draw(Graphics g, Color color)
        {
            GetParams();
            if (((StartX - EndX) > 0) && ((StartY - EndY) > 0))
            {
                g.DrawRectangle(new Pen(color, widthParams), StartX - (StartX - EndX), StartY - (StartY - EndY), length, length);
            }
            if (((StartX - EndX) > 0) && ((StartY - EndY) < 0))
            {
                g.DrawRectangle(new Pen(color, widthParams), StartX - (StartX - EndX), StartY, length, length);
            }
            if (((StartX - EndX) < 0) && ((StartY - EndY) > 0))
            {
                g.DrawRectangle(new Pen(color, widthParams), StartX, StartY - (StartY - EndY), length, length);
            }
            if (((StartX - EndX) < 0) && ((StartY - EndY) < 0))
            {
                g.DrawRectangle(new Pen(color, widthParams), StartX, StartY, length, length);
            }
        }

    }
}
