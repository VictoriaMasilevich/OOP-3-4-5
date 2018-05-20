﻿using AbstractClassLibrary;

namespace CircleClassLibrary
{
    public class CircleCreator: FigureCreator
    {
        public override Figure Create()
        {
            return new Circle();
        }
    }
}
