using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFigureClassLibrary;

namespace AbstractFigureClassLibrary
{
    class CreatorList: Dictionary<int, Creator>
    {
        private int currentId = 0;

        public Figure GetFigure(int figureID)
        {
            switch (figureID)
            {
                case 0:
                    AddCreator(new CreatorLine());
                    break;
                case 1:
                    AddCreator(new CreatorSquare());
                    break;
                case 2:
                    AddCreator(new CreatorRectangle());
                    break;
                case 3:
                    AddCreator(new CreatorEllipse());
                    break;
                default:
                    AddCreator(new CreatorLine());
                    break;
            }
            return this[figureID].CreateFigure();
        }

        public int AddCreator(Creator creator)
        {
            this.Add(currentId, creator);
            return currentId++;
        }
    }
}
