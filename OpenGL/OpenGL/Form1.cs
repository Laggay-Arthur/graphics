using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using Tao.DevIl;


namespace OpenGL
{
    public partial class Form1 : Form
    {
        int index =0;
        uint indexObject = 0;
        string url = "";
        bool isLoaded = false;

        public Form1()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
            
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (double)simpleOpenGlControl1.Width / (double)simpleOpenGlControl1.Height, 0.1, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);
        }

        long t = 0;
        long t1 = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            simpleOpenGlControl1.Left += 5;
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            
            Gl.glPushMatrix();

            Gl.glTranslated(0, 0, -double.Parse(textBox8.Text));
            //Gl.glRotated(double.Parse(textBox1.Text), 1, 0, 0);
            Gl.glRotated(t, 1, 0, 0);

            Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            Gl.glColor3b(255, 255, 0);
            Gl.glVertex3f(float.Parse(textBox2.Text), float.Parse(textBox3.Text), 0);
            Gl.glVertex3f(float.Parse(textBox4.Text), float.Parse(textBox5.Text), 0f);
            Gl.glVertex3f(float.Parse(textBox6.Text), float.Parse(textBox7.Text), 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUAD_STRIP);
            Gl.glVertex3f(-0.9f, 0, 0);
            Gl.glVertex3f(0.9f, 0, 0f);
            Gl.glVertex3f(-0.9f, -1, 0);
            Gl.glVertex3f(0.9f, -1, 0);
            Gl.glEnd();

            Gl.glPopMatrix();

            Gl.glFlush();
            simpleOpenGlControl1.Invalidate();

            timer1.Enabled = true;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //t+=3;
            
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            Gl.glColor3b(255, 0, 0);
            Gl.glPushMatrix();

            Gl.glTranslated(0, 0, -double.Parse(textBox8.Text));
            //Gl.glRotated(double.Parse(textBox1.Text), 1, 0, 0);
            Gl.glRotated(t, 1, 0, 0);
            Gl.glRotated(t1, 0, 1, 0);

            Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            Gl.glVertex3f(float.Parse(textBox2.Text), float.Parse(textBox3.Text), 0);
            Gl.glVertex3f(float.Parse(textBox4.Text), float.Parse(textBox5.Text), 0f);
            Gl.glVertex3f(float.Parse(textBox6.Text), float.Parse(textBox7.Text), 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUAD_STRIP);
            Gl.glVertex3f(-0.9f, 0, 0);
            Gl.glVertex3f(0.9f, 0, 0f);
            Gl.glVertex3f(-0.9f, -1, 0);
            Gl.glVertex3f(0.9f, -1, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            Gl.glVertex3f(float.Parse(textBox2.Text) + 3, float.Parse(textBox3.Text), 0);
            Gl.glVertex3f(float.Parse(textBox4.Text) + 3, float.Parse(textBox5.Text), 0f);
            Gl.glVertex3f(float.Parse(textBox6.Text) + 3, float.Parse(textBox7.Text), 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUAD_STRIP);
            Gl.glVertex3f(2.1f, 0, 0);
            Gl.glVertex3f(3.9f, 0, 0f);
            Gl.glVertex3f(2.1f, -1, 0);
            Gl.glVertex3f(3.9f, -1, 0);
            Gl.glEnd();

            Glut.glutSolidTeapot(2);


            Gl.glPopMatrix();

            Gl.glFlush();
            simpleOpenGlControl1.Invalidate();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                t += 3;
            if (e.KeyCode == Keys.S)
                t -= 3;
            if (e.KeyCode == Keys.A)
                t1 += 3;
            if (e.KeyCode == Keys.D)
                t1 -= 3;
        }

        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                t-=3;
            if (e.KeyCode == Keys.S)
                t+=3;
            if (e.KeyCode == Keys.A)
                t1 -= 3;
            if (e.KeyCode == Keys.D)
                t1 += 3;

        }

        private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
                t++;
            if (e.KeyChar == 's')
                t--;
            //if (e.KeyChar == 'w')
            //    t++;
            //if (e.KeyChar == 'w')
            //    t++;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
                t++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult ds = openFileDialog1.ShowDialog();
            if(ds == DialogResult.OK)
            {
                Il.ilGenImages(1, out index);
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
                }
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                Gl.glClearColor(0, 0, 0, 1);

                Gl.glLoadIdentity();

                Gl.glEnable(Gl.GL_TEXTURE_2D);

                Gl.glBindTexture(Gl.GL_TEXTURE_2D, indexObject);

                Gl.glPushMatrix();

                Gl.glTranslated(0,- 1, -15);

                Gl.glRotated(t, 1, 0, 0);
                Gl.glRotated(t1, 0, 1, 0);

                Glu.GLUquadric quadro = Glu.gluNewQuadric();

                Glu.gluQuadricTexture(quadro, Gl.GL_TRUE);

                Gl.glEnable(Gl.GL_TEXTURE_2D);

                //Glu.gluSphere(quadro, 0.3, 50, 50);
                Glut.glutSolidTeapot(2);

                Gl.glDisable(Gl.GL_TEXTURE_2D);
                Gl.glPopMatrix();

                Gl.glDisable(Gl.GL_TEXTURE_2D);

                simpleOpenGlControl1.Invalidate();
            }
        }
    }
}
