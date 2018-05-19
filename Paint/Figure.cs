﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    [Serializable]
    public abstract class Figure
    {
        public string name;
        public Color color = new Color();
        public List<Point> Points;
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public Color colorParams { get; set; }
        public float widthParams { get; set; }

        public abstract Figure CreateFigure();

        public Figure()
        {
            Points = new List<Point>(0);
        }

        public void StartPoint(Point p)
        {
            Points.Clear();
            AddPoint(p);
        }

        public void AddPoint(Point p)
        {
            Points.Add(p);
        }

        public void setColor()
        {
            color = Color.Black;
        }

        public void EndPoint(Point p)
        {
            AddPoint(p);
            Points[Points.Count - 1] = p;
        }

        public virtual void GetParams()
        {
            StartX = Points[0].X;
            StartY = Points[0].Y;
            EndX = Points[Points.Count - 1].X;
            EndY = Points[Points.Count - 1].Y;
        }

        public abstract void Draw(Graphics graphics, Color color);

        public CheckedListBox GenerateButtons(Type type, CheckedListBox CheckedListBox1)
        {
            CheckedListBox1.Items.Add(type);
            return CheckedListBox1;
        }

    }
}
