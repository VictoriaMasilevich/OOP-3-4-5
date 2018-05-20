using AbstractClassLibrary;

namespace EllipseClassLibrary
{
    public class CreatorEllipse: FigureCreator
    {
        public override Figure Create()
        {
            return new Ellipse();
        }
    }
}
