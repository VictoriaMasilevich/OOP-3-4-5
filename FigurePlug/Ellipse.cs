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
    public class Ellipse : Figure
    {
        public override Figure CreateFigure()
        {
            return new Ellipse();
        }

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
