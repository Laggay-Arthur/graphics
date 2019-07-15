using System;
using System.Windows.Forms;


// для работы с библиотекой OpenGL 
using Tao.OpenGl; // для работы с библиотекой FreeGLUT 
using Tao.FreeGlut; // для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
using Tao.DevIl;


namespace tao_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }
        int p = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            timer3.Enabled = !timer3.Enabled;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit(); // инициализация Glut 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Gl.glClearColor(255, 255, 255, 1);// отчитка окна 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height); // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glMatrixMode(Gl.GL_PROJECTION);// настройка проекции
            Gl.glLoadIdentity();
            Glu.gluPerspective(60, (float)AnT.Width / AnT.Height, 0.01, 2000);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity(); // настройка параметров OpenGL для визуализации 
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glTranslated(0, 3, -5.0f);
            //Инициализация
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();
            Gl.glColor3b(0, 0, 0);

            Gl.glPopMatrix();

            Gl.glTranslated(-40, 0, -100);
            Gl.glRotated(45, 0, 0, 0);

            //Крыша
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex2d(-20, 40);
            Gl.glVertex2d(30, 80);
            Gl.glVertex2d(90, 40);
            Gl.glEnd();

            //Стены
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(0, 40);
            Gl.glVertex2d(60, 40);
            Gl.glVertex2d(60, -70);
            Gl.glVertex2d(0, -70);
            Gl.glVertex2d(0, 40);
            Gl.glVertex2d(0, -70);
            Gl.glEnd();

            Gl.glColor3b(40, 40, 40);

            //Окно
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex2d(20, 20);
            Gl.glVertex2d(40, 20);
            Gl.glVertex2d(40, 0);
            Gl.glVertex2d(20, 0);
            Gl.glEnd();

            Gl.glPushMatrix();

            Gl.glFlush();
            AnT.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        => timer1.Enabled = !timer1.Enabled;
        //треугольник
        private void timer1_Tick(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();
            Gl.glColor3b(0, 0, 0);

            Gl.glPopMatrix();
            Gl.glTranslated(0, 0,-func());

            Gl.glRotated(2, 1, 0, 1);

            //Треугольник
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex2d(0, 10);
            Gl.glVertex2d(20, 30);
            Gl.glVertex2d(40, 10);
            Gl.glEnd();

            Gl.glPushMatrix();

            Gl.glFlush();
            AnT.Invalidate();
        }
        int x = 0;
        int func()
        {
            if(x > 100) { x = 0; }
            return ++x;

        }
        int index = 0;
        string url = "";
        uint indexObject = 0;
        bool isLoaded = false;


        DialogResult ds;
        bool isImage = false;
        //Сфера с текстурой
        private void button4_Click(object sender, EventArgs e)
        {
            if (!isImage)
            {
                ds = openFileDialog1.ShowDialog();

                if (ds == DialogResult.OK)
                {
                    Il.ilBindImage(index);
                    url = openFileDialog1.FileName;
                    if (Il.ilLoadImage(url))
                    {
                        int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                        int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                        int bitsPP = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

                        switch (bitsPP)
                        {
                            case 24:
                                indexObject = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                                break;
                            case 32:
                                indexObject = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                                break;
                        }

                        isLoaded = true;
                        Il.ilDeleteImages(1, ref index);

                        timer2.Enabled = true;
                        timer1.Enabled = false;
                        isImage = true;
                    }
                }
            }
            else
            {
                timer2.Enabled = isImage = false;
            }
        }

        public uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            uint textObject;

            Gl.glGenTextures(1, out textObject);

            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textObject);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);

            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

            switch (Format)
            {
                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;
                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;
            }
            return textObject;
        }
        long t = 0;
        long t1 = 0;
        //Сфера с текстурой
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                //MessageBox.Show("timer2");
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                Gl.glClearColor(0, 0, 0, 1);

                Gl.glLoadIdentity();

                Gl.glEnable(Gl.GL_TEXTURE_2D);

                Gl.glBindTexture(Gl.GL_TEXTURE_2D, indexObject);

                Gl.glPushMatrix();

                Gl.glTranslated(0, 1, -9);

                Gl.glRotated(t, 4, 4, 0);
                //Gl.glRotated(t1, t, 500, 0);

                Glu.GLUquadric quadro = Glu.gluNewQuadric();

                Glu.gluQuadricTexture(quadro, Gl.GL_TRUE);

                Gl.glEnable(Gl.GL_TEXTURE_2D);

                Glu.gluSphere(quadro, 0.9, 50, 500);

                Gl.glDisable(Gl.GL_TEXTURE_2D);
                Gl.glPopMatrix();

                Gl.glDisable(Gl.GL_TEXTURE_2D);

                AnT.Invalidate();
                t += 10;
            }
        }
        //Сфера
        private void timer3_Tick(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3b(120, 255, 120);
            Gl.glPushMatrix();
            Gl.glTranslated(0, 0, -12);
            Gl.glRotated(60 + p, 10, 12, -5);
            Glut.glutWireSphere(2, 32, 32);// рисуем сферу с помощью библиотеки FreeGLUT
            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
            p += 10;
        }
    }
}