using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AbstractFigureClassLibrary;

namespace AbstractFigureClassLibrary
{
    [Serializable]
    public class Ellipse : Figure
    {
        public int CenterX;
        public int CenterY;

        public Ellipse()
        {
        }

        public override void GetParams()
        {

            base.GetParams();
            CenterX = StartX + ((EndX - StartX) / 2);
            CenterY = StartY + ((EndY - StartY) / 2);
        }

        public override void Draw(Graphics g, Color color)
        {
            GetParams();
            g.DrawEllipse(new Pen(color, widthParams), StartX, StartY, (EndX-StartX), (EndY-StartY));
        }
    }
}
