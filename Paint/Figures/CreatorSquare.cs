using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Paint
{
    public class CreatorSquare: Creator
    {
        public CreatorSquare()
        {
            Name = "Квадрат";
        }
        public override Figure CreateFigure()
        {
            return new Square();
        }
    }
}
