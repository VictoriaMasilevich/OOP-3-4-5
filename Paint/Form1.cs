using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using AbstractClassLibrary;
using System.Reflection;

namespace Paint
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<Figure> Figures;
        private Figure figure;
        private Pen pen;
        private Point currentPoint;
        private List<Point> MovingPoints;
        private readonly string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "FigurePlugins");
        FigureCreatorList FigureCreatorList = new FigureCreatorList();
        FigureCreator FigureCreator;
        public bool isClicked = false;
        FigureList FigureList = new FigureList();

        public struct MenuItemInfo
        {
            public string figureName;
            public string creatorType;
            public FigureCreator FigureCreator;
        }

        public Form1()
        {
            try
            {
                LoadPlugin();
            }
            catch
            {
                MessageBox.Show("Ошибка загрузки плагина.");
            }
            InitializeComponent();
            string[] FigureNames = new string[] { "Line", "Square", "Rectangle", "Circle", "Ellipse" };
            List<MenuItemInfo> itemsList = new List<MenuItemInfo>();
            foreach (var creator in FigureCreatorList.Creators)
            {
                foreach (var figurename in FigureNames)
                {
                    if ((creator).ToString().Contains(figurename))
                    {
                        itemsList.Add(new MenuItemInfo
                        {
                            figureName = figurename,
                            creatorType = (creator).ToString(),
                            FigureCreator = creator
                        });
                        break;
                    }
                }
            }
            ToolStripMenuItem menuItem;
            foreach (MenuItemInfo items in itemsList)
            {
                menuItem = new ToolStripMenuItem(items.figureName)
                {
                    Tag = items.FigureCreator
                };
                menuItem.Click += new EventHandler(MenuItemFigureClickHandler);
                figuresToolStripMenuItem.DropDownItems.Add(menuItem);
            }
            Figures = new List<Figure>();
            MovingPoints = new List<Point>();
            pen = new Pen(Color.Black, 1);
            graphics = pictureBox1.CreateGraphics();
        }

        public void LoadPlugin()
        {
            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists) pluginDirectory.Create();
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    Type[] types = assembly.GetExportedTypes();

                    foreach (Type type in types)
                    {
                        if (type.IsClass && type.GetTypeInfo().BaseType == typeof(FigureCreator) && !type.IsAbstract)
                        {
                            var plugin = Activator.CreateInstance(type);
                            FigureCreatorList.Creators.Add((FigureCreator)plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    string caption = "Error";
                    MessageBox.Show(ex.Message, caption, button);
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

        private void MenuItemFigureClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            FigureCreator = (FigureCreator)clickedItem.Tag;
            figure = FigureCreator.Create();
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
            //figure = null;
            MovingPoints.Clear();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Figures.Clear();
            graphics.Clear(Color.White);
        }
    }
}
