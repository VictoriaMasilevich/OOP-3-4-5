﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractFigureClassLibrary;

namespace AbstractFigureClassLibrary
{
    public class CreatorEllipse: Creator
    {
        public CreatorEllipse()
        {
            Name = "Эллипс";
        }
        public override Figure CreateFigure()
        {
            return new Ellipse();
        }
    }
}
