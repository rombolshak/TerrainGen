/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace CG3
{
    class MainController
    {
        Camera cam = new Camera();
        Settings s = new Settings();
        MainForm form = new MainForm();
        int width, height;
        private float[,] hMap;
        public MainController() {
            s["width"] = 256;
            s["height"] = 256;
            s["seed"] = 12347537;
            s["size"] = 65;
            s["roughness"] = 0.2;
            //hMap = (new HillModel(s)).GetMap();
            hMap = (new FactorialModel(s)).GetMap();
            //InitGL();
        }

        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(.1f, .3f, .6f, .2f);
            Gl.glLoadIdentity();
            // Gl.glColor3i(255, 0, 0);

            cam.Look(); //Обновляем взгляд камеры

            Gl.glPushMatrix();

            DrawGrid();//Нарисуем сетку

            Gl.glPopMatrix();

            Gl.glFlush();

            form.glControl.Invalidate();
        }

        private void DrawGrid()
        {
            int length = hMap.GetLength(1);
            //float[] MatrixColorOX = { 1, 0, 0, 1 };
            //float[] MatrixColorOY = { 0, 1, 0, 1 };
            //float[] MatrixColorOZ = { 0, 0, 1, 1 };
            ////x - количество или длина сетки, quad_size - размер клетки
            //Gl.glPushMatrix(); //Рисуем оси координат, цвет объявлен в самом начале
            //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOX);
            //Gl.glTranslated((-x * 2) / 2, 0, 0);
            //Gl.glRotated(90, 0, 1, 0);
            //Glut.glutSolidCylinder(0.02, x * 2, 12, 12);
            //Gl.glPopMatrix();

            //Gl.glPushMatrix();
            //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOZ);
            //Gl.glTranslated(0, 0, (-x * 2) / 2);
            //Glut.glutSolidCylinder(0.02, x * 2, 12, 12);
            //Gl.glPopMatrix();

            //Gl.glPushMatrix();
            //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOY);
            //Gl.glTranslated(0, x / 2, 0);
            //Gl.glRotated(90, 1, 0, 0);
            //Glut.glutSolidCylinder(0.02, x, 12, 12);
            //Gl.glPopMatrix();

            //Gl.glBegin(Gl.GL_LINES);

            //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixOXOYColor);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            int zoom = 4;
            for (int i = -length / 2; i < length / 2 - 1; ++i)
                for (int j = -length / 2; j < length / 2 - 1; ++j)
                {
                    int x = i * zoom, y = j * zoom;
                    Gl.glBegin(Gl.GL_TRIANGLE_STRIP);

                   // Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, GetColor(hMap[i + length / 2, j + length / 2]));

                    Gl.glVertex3f(x, Math.Max(30, hMap[i + length / 2, j + length / 2]), y);
                    Gl.glVertex3f(x, Math.Max(30, hMap[i + length / 2, j + 1 + length / 2]), y + zoom);
                    Gl.glVertex3f(x + zoom, Math.Max(30, hMap[i + 1 + length / 2, j + length / 2]), y);
                    Gl.glVertex3f(x + zoom, Math.Max(30, hMap[i + 1 + length / 2, j + 1 + length / 2]), y + zoom);

                    Gl.glEnd();
                }
            // Рисуем сетку 1х1 вдоль осей
            //for (float i = -x; i <= x; i += 1)
            //{
            //    Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
            //    // Ось Х
            //    Gl.glVertex3f(-x * quad_size, 0, i * quad_size);
            //    Gl.glVertex3f(x * quad_size, 0, i * quad_size);

            //    // Ось Z
            //    Gl.glVertex3f(i * quad_size, 0, -x * quad_size);
            //    Gl.glVertex3f(i * quad_size, 0, x * quad_size);
            //    Gl.glEnd();
            //}
        }

        private float[] GetColor(float p)
        {
            if (p - 30 <= 0) return new float[] { 23f / 255, 135f / 255, 209f / 255, 1f };
            if (p - 50 <= 0) return new float[] { 39f / 255, 196f / 255, 89f / 255, 1f };
            if (p - 75 <= 0) return new float[] { 14f / 255, 153f / 255, 58f / 255, 1f };
            if (p - 100 <= 0) return new float[] { 7f / 255, 102f / 255, 37f / 255, 1f };
            if (p - 125 <= 0) return new float[] { 66f / 255, 9f / 255, 9f / 255, 1f };
            if (p - 150 <= 0) return new float[] { 112f / 255, 61f / 255, 61f / 255, 1f };
            if (p - 170 <= 0) return new float[] { 120f / 255, 92f / 255, 92f / 255, 1f };
            if (p - 200 <= 0) return new float[] { 138f / 255, 132f / 255, 132f / 255, 1f };
            if (p - 240 <= 0) return new float[] { 227f / 255, 227f / 255, 227f / 255, 1f };
            return new float[] { 1f, 1f, 1f, 1f };
        }

        private void InitGL()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, width, height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPerspective(45, (float)width / (float)height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            //Gl.glEnable(Gl.GL_LIGHTING);
            //Gl.glEnable(Gl.GL_LIGHT0);

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glLineWidth(1.0f);


            float max = hMap[0, 0];
            for (int i = 0; i < hMap.GetLength(0); ++i)
                for (int j = 0; j < hMap.GetLength(1); ++j)
                    if (hMap[i, j] > max) max = hMap[i, j];

            cam.Position_Camera(-hMap.GetLength(0), max + 40, -hMap.GetLength(1), 0, max - 50, 0, 0, 1, 0); //Вот тут в инициализации
            //укажем начальную позицию камеры,взгляда и вертикального вектора.
        }


        internal void Run()
        {
            
            height = form.glControl.Height;
            width = form.glControl.Width;
            form.timer1.Tick += new EventHandler(timer1_Tick);
            form.Rotate += new EventHandler<RotateEventArgs>(form_Rotate);
            form.Move += new EventHandler<MoveEventArgs>(form_Move);
            InitGL();
            form.ShowDialog();            
        }

        void form_Move(object sender, MoveEventArgs e)
        {
            switch (e.MoveType)
            {
                case MoveType.Forward:
                    cam.Move_Camera((float)(0.5));
                    cam.Strafe(-((float)(0)));
                    break;
                case MoveType.Back:
                    cam.Move_Camera(-(float)(0.5));
                    cam.Strafe(((float)(0)));
                    break;
                case MoveType.Left:
                    cam.Move_Camera((float)(0));
                    cam.Strafe(-((float)(0.5)));
                    break;
                case MoveType.Right:
                    cam.Move_Camera((float)(0));
                    cam.Strafe(((float)(0.5)));
                    break;
                case MoveType.Up:
                    cam.moveUpDown(0.5f);
                    break;
                case MoveType.Down:
                    cam.moveUpDown(-0.5f);
                    break;
            }
        }

        void form_Rotate(object sender, RotateEventArgs e)
        {
            switch (e.MoveType)
            {
                case MoveType.Right:
                    cam.Rotate_View(e.Speed);//.Rotate_Position((float)(myMouseYcoordVar - myMouseYcoord), 0, 1, 0); //крутим камеру, в моем случае это от 3го лица
                    break;
                case MoveType.Up:
                    cam.upDown(e.Speed);
                    break;
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            cam.update();
            Draw();
            //form.glControl.Invalidate();
            form.label1.Text = cam.getPosX().ToString();
            form.label2.Text = cam.getPosY().ToString();
            form.label3.Text = cam.getPosZ().ToString();
            form.label4.Text = cam.getViewX().ToString();
            form.label5.Text = cam.getViewY().ToString();
            form.label6.Text = cam.getViewZ().ToString();
        }
    }
}
*/