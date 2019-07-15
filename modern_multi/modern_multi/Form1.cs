using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace modern_multi
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }
        long t = 0;
        Bitmap bmp;
        BitmapData bitmapData;
        Random rand = new Random();
        private void draw(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format24bppRgb);//Создаём новый Bitmap с размерами pictubox1
            bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte[] arr = new byte[bitmapData.Stride * bitmapData.Height];//здесь храним все пиксели
            byte step = 3;//шаг по трём компонентам ( RGB )
            Marshal.Copy(bitmapData.Scan0, arr, 0, arr.Length);//копируем все пиксели в arr
            for (int x = 0; x < bmp.Width; x++) for (int y = 0; y < bmp.Height; y++)
                {//Задаём цвет каждого пикселя
                    arr[bitmapData.Stride * y + step * x] = (byte)(rand.Next(0, 255));              //R
                    arr[bitmapData.Stride * y + step * x + 1] = (byte)(x * x - y * y + t * t - 100);//G
                    arr[bitmapData.Stride * y + step * x + 1] = (byte)(x * x + y * y + t * t);      //B
                }
            Marshal.Copy(arr, 0, bitmapData.Scan0, bitmapData.Stride * bitmapData.Height);
            bmp.UnlockBits(bitmapData);
            pictureBox1.Image = bmp;
            t++;
        }
    }
}