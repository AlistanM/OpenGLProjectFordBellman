using System;
using System.Drawing;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

//1 2 3
//2 3 4
//1 3 10
//4 5 16
//3 5 1

namespace openglgraphlastversion
{
    public class GraphPainter
    {
        private int _radius;
        private SimpleOpenGlControl _control;
        private const int OrbitalRadius = 10;
        public GraphPainter(SimpleOpenGlControl control)
        {
            _radius = 2;
            _control = control;
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGBA);
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(-25, 25, -25, 25);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        public void DrawVertex(Color col, int index, int n)
        {
            double angle = 2 * Math.PI / n;
            var x = Math.Cos(angle * index) * OrbitalRadius * _radius;
            var y = Math.Sin(angle * index) * OrbitalRadius * _radius;
            DrawCircle(x, y, col);
            Gl.glColor3d(1, 1, 1);
            Gl.glRasterPos2d(x - _radius / 2, y - _radius / 2);
            Glut.glutBitmapString(Glut.GLUT_BITMAP_TIMES_ROMAN_24, (index + 1).ToString());
            Update();
        }
        private void DrawCircle(double x, double y, Color col)
        {
            int num = 100;
            double segment = 2 * Math.PI / num;
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor3d(col.R / 255.0, col.G / 255.0, col.B / 255.0);
            for (int i = 0; i < num; i++)
            {
                Gl.glVertex2d(x + Math.Cos(i * segment) * _radius, y + Math.Sin(i * segment) * _radius);
            }
            Gl.glEnd();
        }
        public void DrawLine(Color col, int index1, int index2, int n, int w)
        {

            Gl.glColor3d(col.R / 255.0, col.G / 255.0, col.B / 255.0);
            var an = 2 * Math.PI / n; //угол
            var x1 = Math.Cos(an * index1) * OrbitalRadius * _radius;
            var y1 = Math.Sin(an * index1) * OrbitalRadius * _radius;
            var x2 = Math.Cos(an * index2) * OrbitalRadius * _radius;
            var y2 = Math.Sin(an * index2) * OrbitalRadius * _radius;
            var x = (x2 + x1) / 2;
            var y = (y2 + y1) / 2;
            Gl.glRasterPos2d(x, y);
            Glut.glutBitmapString(Glut.GLUT_BITMAP_TIMES_ROMAN_24, w.ToString());
            double length = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            double angle = Math.Atan2(y2 - y1, x2 - x1);
            double xc = x1 + Math.Cos(angle) * (length - _radius);
            double yc = y1 + Math.Sin(angle) * (length - _radius);
            double rotationAngle = Math.PI - Math.PI / 18;
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(x1, y1);
            Gl.glVertex2d(x2, y2);
            Gl.glVertex2d(xc, yc);
            Gl.glVertex2d(xc + Math.Cos(angle + rotationAngle) * _radius, yc + Math.Sin(angle + rotationAngle) * _radius);
            Gl.glVertex2d(xc, yc);
            Gl.glVertex2d(xc + Math.Cos(angle - rotationAngle) * _radius, yc + Math.Sin(angle - rotationAngle) * _radius);
            Gl.glEnd();
            Update();
        }
        public void Clear()
        {
            Gl.glClear(Gl.GL_CLEAR);
        }
        private void Update()
        {
            Gl.glPopMatrix();
            _control.Invalidate();
        }
    }
}
