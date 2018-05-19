using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint;

namespace FigurePlug
{
    public class CreatorList : Dictionary<int, Figure>
    {
        public CreatorList()
        {
        }

        public Figure GetFigure(int figureID)
        {
            switch (figureID)
            {
                case 0:
                    figureID = 0;
                    AddCreator(figureID, new Circle());
                    break;
                case 1:
                    figureID = 1;
                    AddCreator(figureID, new Ellipse());
                    break;
                case 2:
                    figureID = 2;
                    AddCreator(figureID, new Line());
                    break;
                case 3:
                    figureID = 3;
                    AddCreator(figureID, new Square());
                    break;
            }
            Figure ret = this[figureID].CreateFigure();
            RemCreator(figureID);
            return ret;
            
        }

        public int AddCreator(int figureID, Figure figure)
        {
            this.Add(figureID, figure.CreateFigure());
            return figureID;
        }

        public int RemCreator(int figureID)
        {
            this.Remove(figureID);
            return figureID;
        }
    }
}
