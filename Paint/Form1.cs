using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using AbstractClassLibrary;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Globalization;
using System.ComponentModel;

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
        public readonly string russian = "ru";
        public readonly string english = "en";
        public string Lang { get; set; }
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
            LoadLanguageConfig();
            SetLanguage();
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
                try
                {
                    menuItem = new ToolStripMenuItem()
                    {
                        Tag = items.FigureCreator,
                        BackgroundImage = Bitmap.FromFile(Directory.GetCurrentDirectory() + "\\img\\" + items.figureName + ".bmp"),
                        BackgroundImageLayout = ImageLayout.Stretch
                    };
                    menuItem.Click += new EventHandler(MenuItemFigureClickHandler);
                    figuresToolStripMenuItem.DropDownItems.Add(menuItem);
                }
                catch
                {
                    MessageBox.Show("Проверьте наличие картинок");
                }
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

        public void LoadLanguageConfig()
        {
            var doc = new XmlDocument();
            try
            {
                doc.Load("config.xml");
                XmlElement elem = doc.DocumentElement["AppLang"];
                Lang = elem.InnerText;
                doc.Save("config.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Lang = english;
            }
        }

        private void SetLanguage()
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lang);
            }
            catch (CultureNotFoundException e)
            {
                MessageBox.Show(e.Message);
                Lang = english;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lang);
            }
        }

        public void ChangeConfig()
        {
            var doc = new XmlDocument();
            try
            {
                doc.Load("сonfig.xml");
            }
            catch
            {
                XmlTextWriter textWritter = new XmlTextWriter("config.xml", Encoding.UTF8);
                textWritter.WriteStartDocument();
                textWritter.WriteStartElement("head");
                textWritter.WriteEndElement();
                textWritter.Close();
                doc.Load("config.xml");
            }

            XmlNode AppLang = doc.CreateElement("AppLang");
            doc.DocumentElement.AppendChild(AppLang);
            AppLang.InnerText = Lang;
            doc.Save("config.xml");
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lang = russian;
            ChangeConfig();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            this.Close();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lang = english;
            ChangeConfig();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            this.Close();
        }
    }
}
