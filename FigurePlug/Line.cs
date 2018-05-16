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
    public class Line : Figure
    {
        public override Figure CreateFigure()
        {
            return new Line();
        }

        public override void Draw(Graphics g, Color color)
        {
            GetParams();
            g.DrawLine(new Pen(color, widthParams), StartX, StartY, EndX, EndY);
        }

    }
}
