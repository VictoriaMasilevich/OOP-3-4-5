using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Paint
{
    [Serializable]
    public class Line : Figure
    {
        public override void Draw(Graphics g, Color color)
        {   
            GetParams();
            g.DrawLine(new Pen(color, widthParams), StartX, StartY, EndX, EndY);
        }

    }
}
