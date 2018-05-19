using System;
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
using System.Reflection;

namespace Paint
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<Figure> Figures;
        private Figure figure;
        private Pen pen;
        public BinaryFormatter formatter;
        private Point currentPoint;
        private List<Point> MovingPoints;
        private int buttonLocationX;
        private int buttonLocationY;
        private readonly string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        private object creatorList;
        private Type t;

        public Form1()
        {
            InitializeComponent();
            Figures = new List<Figure>();
            MovingPoints = new List<Point>();
            pen = new Pen(Color.Black, 1);
            graphics = pictureBox1.CreateGraphics();
        }

        private void Selectt(Figure figure1)
        {
            figure = figure1;
        }

        public void LoadPlugin()
        {
            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists) pluginDirectory.Create();

            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                Assembly loaded = Assembly.LoadFile(file);
                Type[] types = loaded.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(Figure)))
                    {
                        var plugin = loaded.CreateInstance(type.FullName) as Figure;
                        plugin.GenerateButtons(type, CheckedListBox1);
                        CheckedListBox1.ItemCheck += (sender, e) => Selectt(plugin.CreateFigure());
                    }
                }
                try
                {
                    t = loaded.GetType("FigurePlug.CreatorList", true, true);
                    creatorList = Activator.CreateInstance(t);
                }
                catch
                {
                    MessageBox.Show("Ошибкааааа");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            DoubleBuffered = true;
            pictureBoxColor.BackColor = Color.Black;
            trackBarPenWidth.Value = 1;
            pen.Width = trackBarPenWidth.Value;
            labelPenWidth.Text = "Толщина линий: " + trackBarPenWidth.Value.ToString();
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
            MethodInfo method = t.GetMethod("GetFigure");
            figure = (Figure)method.Invoke(creatorList, new object[] { int.Parse((sender as Button).Tag.ToString()) });
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
            figure = null;
            MovingPoints.Clear(); 
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Figures.Clear();
            graphics.Clear(Color.White);
        }

        private void PluginButton_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPlugin();
            }
            catch
            {
                MessageBox.Show("Ошибка загрузки плагина.");
            }
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Selectt(plugin.CreateFigure());
        }
    }
}
