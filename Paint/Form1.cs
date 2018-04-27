﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AbstractFigureClassLibrary;
using System.Reflection;

namespace AbstractFigureClassLibrary
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<Figure> Figures;
        private Figure figure;
        private Pen pen;
        public BinaryFormatter formatter;
        public Figure selectedFigure;
        private Point currentPoint;
        private List<Point> MovingPoints;
        private CreatorList creatorList = new CreatorList();
        private int buttonLocationX;
        private int buttonLocationY;

        public Form1()
        {
            InitializeComponent(); 
            Figures = new List<Figure>();
            MovingPoints = new List<Point>();
            pen = new Pen(Color.Black, 1);
            graphics = pictureBox1.CreateGraphics();
            buttonLocationX = LineButton.Location.X;
            buttonLocationY = LineButton.Location.Y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            pictureBoxColor.BackColor = Color.Black;
            trackBarPenWidth.Value = 1;
            pen.Width = trackBarPenWidth.Value;
            labelPenWidth.Text = "Толщина линий: " + trackBarPenWidth.Value.ToString();
            buttonSerialize.Enabled = false;
            buttonDeserialize.Enabled = true;
            selectedFigure = null;
        }
                              
        private void DrawAll()
        {
            for (int i = 0; i < Figures.Count; i++)
            {
                Figures[i].Draw(graphics, Figures[i].colorParams);
            }
        }

        private void btnFigure_MouseDown(object sender, MouseEventArgs e)
        {
            figure = creatorList.GetFigure(int.Parse((sender as Button).Tag.ToString()));
        }

        private void buttonChangeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pen.Color = colorDialog1.Color;
            pictureBoxColor.BackColor = colorDialog1.Color;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBarPenWidth.Value;
            labelPenWidth.Text = "Толщина линий: " + trackBarPenWidth.Value.ToString();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            currentPoint = new Point(e.X, e.Y);
            MovingPoints.Add(currentPoint);
            if (figure != null)
            {
                figure.AddPoint(currentPoint);
                figure.colorParams = pen.Color;
                figure.widthParams = pen.Width;
                Figures.Add(figure);
                buttonSerialize.Enabled = true;
                selectedFigure = null;
            }
            else
            {
            } 
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var movePoint = new Point(e.X, e.Y);
            MovingPoints.Add(movePoint);
            if ((figure != null) && (e.Button == MouseButtons.Left))
            {
                graphics.Clear(Color.White);
                figure.EndPoint(movePoint);
                figure.Draw(graphics, figure.colorParams);
                DrawAll();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            var UpPoint = new Point(e.X, e.Y);
            MovingPoints.Add(UpPoint);
            if (figure != null)
            {
                figure.EndPoint(new Point(e.X, e.Y));
                figure.Draw(graphics, figure.colorParams);
                DrawAll();
            }
            if ((selectedFigure != null) && (e.Button == MouseButtons.Left))
            {
                graphics.Clear(Color.White);
                selectedFigure.StartPoint(new Point((selectedFigure.StartX - (MovingPoints[MovingPoints.Count - 2].X - MovingPoints[MovingPoints.Count - 1].X)), selectedFigure.StartY - (MovingPoints[MovingPoints.Count - 2].Y - MovingPoints[MovingPoints.Count - 1].Y)));
                selectedFigure.EndPoint(new Point((selectedFigure.EndX - (MovingPoints[MovingPoints.Count - 2].X - MovingPoints[MovingPoints.Count - 1].X)), selectedFigure.EndY - (MovingPoints[MovingPoints.Count - 2].Y - MovingPoints[MovingPoints.Count - 1].Y)));
                DrawAll();
                selectedFigure.Draw(graphics, Color.Red);
            }
            figure = null;
            selectedFigure = null;
            MovingPoints.Clear(); 
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Figures.Clear();
            graphics.Clear(Color.White);
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {   
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height; 
        }

        private void buttonSerialize_Click(object sender, EventArgs e)
        {
            if (Figures.Count == 0)
            {
                MessageBox.Show("Нечего сериалиализовывать.", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                var formatter = new BinaryFormatter();
                formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    using (var fStream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        formatter.Serialize(fStream, Figures);
                    }
                }
            }
        }

        private void buttonDeserialize_Click(object sender, EventArgs e)
        {
            var formatter = new BinaryFormatter();
            formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            formatter.Binder = new VersionConfigToNamespaceAssemblyObjectBinder();
            openFileDialog1.ShowDialog();
            try
            {
                if (openFileDialog1.FileName != "")
                {
                    using (var fStream = File.OpenRead(openFileDialog1.FileName))
                    {
                        try
                        {
                            Figures = new List<Figure>();
                            Figures = (List<Figure>)formatter.Deserialize(fStream);
                            DrawAll();
                        }
                        catch (SerializationException)
                        {
                            MessageBox.Show("Ошибка десериализации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Figures.Clear();
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        class VersionConfigToNamespaceAssemblyObjectBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type typeToDeserialize = null;
                try
                {
                    string ToAssemblyName = assemblyName.Split(',')[0];
                    Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (Assembly ass in Assemblies)
                    {
                        if (ass.FullName.Split(',')[0] == ToAssemblyName)
                        {
                            typeToDeserialize = ass.GetType(typeName);
                            break;
                        }
                    }
                }
                catch (System.Exception exception)
                {
                    throw exception;
                }
                return typeToDeserialize;
            }
        }
    }
}
