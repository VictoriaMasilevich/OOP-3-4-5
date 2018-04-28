using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Paint
{
    class CreatorList : Dictionary<int, Creator>
    {
        public Figure GetFigure(int figureID)
        {
            switch (figureID)
            {
                case 0:
                    figureID = 0;
                    AddCreator(figureID, new CreatorLine());
                    break;
                case 1:
                    figureID = 1;
                    AddCreator(figureID, new CreatorSquare());
                    break;
                case 2:
                    figureID = 2;
                    AddCreator(figureID, new CreatorCircle());
                    break;
                case 3:
                    figureID = 3;
                    AddCreator(figureID, new CreatorEllipse());
                    break;
            }
            Figure ret = this[figureID].CreateFigure();
            RemCreator(figureID);
            return ret;
            
        }

        public int AddCreator(int figureID, Creator creator)
        {
            this.Add(figureID, creator);
            return figureID;
        }

        public int RemCreator(int figureID)
        {
            this.Remove(figureID);
            return figureID;
        }
    }
}
