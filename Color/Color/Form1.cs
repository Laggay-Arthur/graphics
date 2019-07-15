using System;
using System.Drawing;
using System.Windows.Forms;


namespace Color
{
    public partial class Form1 : Form
    {
        Bitmap bmp; 
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        long t = 0;
        System.Drawing.Color c;
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int x   = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    c = System.Drawing.Color.FromArgb((byte)(x  * t), (byte)( y * t), (byte)(x * y * t));
                    bmp.SetPixel((byte)(x * t), (byte)( y * t), c);
                }
            }
            pictureBox1.Image = bmp;
            t++;
        }
    }
}