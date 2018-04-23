using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace PaintF
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        [Serializable]
        public class MyObject
        {
            public int n1 = 0;
            public int n2 = 0;
            public String str = null;
        }

        private void Serialize_Click(object sender, EventArgs e)
        {
            MyObject obj = new MyObject();
            obj.n1 = 1;
            obj.n2 = 24;
            obj.str = "Some String";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("C:\\Users\\Admin\\Desktop\\ооп 3\\mylab\\MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }
    }
}