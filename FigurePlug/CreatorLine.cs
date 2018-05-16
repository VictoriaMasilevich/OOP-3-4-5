using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint;

namespace FigurePlug
{
    class CreatorLine: Creator
    {
        public override Figure CreateFigure()
        {
            return new Line();
        }
    }
}
