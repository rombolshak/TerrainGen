using System;
using System.Linq;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace TerrainGen
{
    /// <summary>
    /// Класс, предоставляющий методы, отвечающие за собственно вывод изображения на
    /// экран
    /// </summary>
    static class GlHelper
    {
        private static float[] _hMap, _nMap, _cMap;
        //private static QuadTree _vertexTree;
        private static uint[] _iMap; // карта индексов вершин
        private static int _waterlevel = 50;
        private static Color _watercolor;
        private static int _zoom = 3;
        private static int _frames;
        private static System.Timers.Timer _timer;
        private static bool _locked;
        private static ColorDescription[] _colorTable;

        /// <summary>
        /// Определяет производительность. Число кадров в секунду
        /// </summary>
        public static int Fps;

        /// <summary>
        /// Подготавливает окружение к использованию указанной карты высот
        /// </summary>
        /// <remarks>
        /// Этот метод следует вызывать каждый раз, как меняется карта высот (а вместе с ней
        /// и карта нормалей), но не каждый раз, как нужно отрисовать сцену
        /// </remarks>
        /// <param name="heightMap">Карта высот</param>
        /// <param name="normalMap">Карта нормалей, полученная с помощью <see
        /// cref="M:TerrainGen.CommonHelper.GenerateNormalMap(System.Single[0:,0:])"/></param>
        public static void Prepare(float[,] heightMap, float[,][] normalMap)
        {
            int length = heightMap.GetLength(0);
            _hMap = new float[length * length * 3];
            _nMap = new float[length * length * 3];
            _cMap = new float[length * length * 4];
            _iMap = new uint[(length - 1) * (length - 1) * 2 * 3];
            for (int i = 0; i < length; ++i)
                for (int j = 0; j < length; ++j)
                {
                    DescribeHeight(heightMap, length, i, j);
                    DescribeNormal(normalMap, length, i, j);
                    DescribeColor(heightMap, length, i, j);
                    if ((i != length - 1) && (j != length - 1))
                        DescribeIndicies(length - 1, i, j);
                }
            //vertexTree = new QuadTree(new Boundary(0,0,0, length), heightMap, 0);
        }

        private static void DescribeIndicies(int length, int i, int j)
        {
            _iMap[(i * length + j) * 6 + 0] = (uint) (i * (length + 1) + j);
            _iMap[(i * length + j) * 6 + 1] = (uint) ((i + 1) * (length + 1) + j);
            _iMap[(i * length + j) * 6 + 2] = (uint) (i * (length + 1) + j + 1);
            _iMap[(i * length + j) * 6 + 3] = (uint) (i * (length + 1) + j + 1);
            _iMap[(i * length + j) * 6 + 4] = (uint) ((i + 1) * (length + 1) + j);
            _iMap[(i * length + j) * 6 + 5] = (uint) ((i + 1) * (length + 1) + j + 1);
        }

        private static void DescribeColor(float[,] heightMap, int length, int i, int j)
        {
            var color = GetColor(heightMap[i, j]);
            _cMap[(i * length + j) * 4 + 0] = color[0];
            _cMap[(i * length + j) * 4 + 1] = color[1];
            _cMap[(i * length + j) * 4 + 2] = color[2];
            _cMap[(i * length + j) * 4 + 3] = color[3];
        }

        private static void DescribeNormal(float[,][] normalMap, int length, int i, int j)
        {
            _nMap[(i * length + j) * 3 + 0] = normalMap[i, j][0];
            _nMap[(i * length + j) * 3 + 1] = normalMap[i, j][1];
            _nMap[(i * length + j) * 3 + 2] = normalMap[i, j][2];
        }

        private static void DescribeHeight(float[,] heightMap, int length, int i, int j)
        {
            _hMap[(i * length + j) * 3 + 0] = (i) * _zoom * 3;
            _hMap[(i * length + j) * 3 + 1] = heightMap[i, j] * _zoom;
            _hMap[(i * length + j) * 3 + 2] = (j) * _zoom * 3;
        }

        /// <summary>
        /// Отрисовывает сцену с указанной камерой
        /// </summary>
        /// <remarks>
        /// Этот метод необходимо вызывать каждый раз, как требуется перерисовка сцены.
        /// Внутри происходит обновление камеры, поэтому нет нужды делать это где-либо еще
        /// </remarks>
        /// <param name="cam">Камера, настроенная необходимым образом</param>
        public static void Draw(Camera cam)
        {
            if (_locked) return;
            _locked = true;
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);
            Gl.glLoadIdentity();
            cam.Look(); //Обновляем взгляд камеры
            Gl.glPushMatrix();
            //iMap = GenIMap(vertexTree, (int) Math.Sqrt(hMap.Length / 3), new FrustrumCuller());
            DrawMap();
            Gl.glPopMatrix();
            Gl.glFlush();
            ++_frames;
            _locked = false;
        }

/*
        private static uint[] GenIMap(QuadTree tree, int length, FrustrumCuller fc)
        {
            if (tree == null) return new uint[] {};

            int result = fc.BoundaryInFrustrum(tree.Boundary);
            if (result == 0)
                return new uint[] {};
            if (result == 2)
                return fc.RenderTree(tree, length);

            var indicies = new List<uint>();
            foreach (var quadTree in tree.Children)
                indicies.AddRange(GenIMap(tree, length, fc));
            return indicies.ToArray();
        }
*/

        private static void DrawMap()
        {
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, _hMap);

            Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glNormalPointer(Gl.GL_FLOAT, 0, _nMap);

            Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
            Gl.glColorPointer(4, Gl.GL_FLOAT, 0, _cMap);

            //Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, hMap.Length / 3);
            Gl.glDrawElements(Gl.GL_TRIANGLES, _iMap.Length, Gl.GL_UNSIGNED_INT, _iMap);

            Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
        }

        private static float[] GetColor(float p)
        {
            if (Math.Abs(p - _waterlevel) < .001) return _watercolor;
            foreach (var c in _colorTable.Where(c => p - c.Level <= 0))
                return c.Color;
            throw new Exception("Не найден цвет для высоты " + p + ". Убедитесь, что в настройках цветов задан цвет с уровнем 255");
        }

        /// <summary>
        /// Метод инициализирует сцену с заданными настройками
        /// </summary>
        /// <remarks>Необходимо снова вызвать этот метод при изменении настроек</remarks>
        /// <param name="settings"><para>Настройки сцены. Необходимы следующие
        /// значения:</para>
        /// <list type="bullet">
        ///  <item>
        ///   <description>int waterlevel -- уровень воды</description>
        ///  </item>
        ///  <item>
        ///   <description>int zoom -- размер ячейки</description>
        ///  </item>
        ///  <item>
        ///   <description>ColorDescription[] colors -- цвета ландшафта</description>
        ///  </item>
        ///  <item>
        ///   <description>Settings special_vars -- настройки, в свою очередь содержащие: 
        /// <list type="bullet">
        ///  <item>
        ///   <description>Color clear_color </description>
        ///  </item>
        ///  <item>
        ///   <description>float[] light_position </description>
        ///  </item>
        ///  <item>
        ///   <description>bool fog_enabled </description>
        ///  </item>
        ///  <item>
        ///   <description>Color fog_color </description>
        ///  </item>
        ///  <item>
        ///   <description>float fog_density</description>
        ///  </item>
        ///  <item>
        ///   <description>Color watercolor</description>
        ///  </item>
        /// </list>
        /// </description>
        ///  </item>
        /// </list></param>
        public static void InitScene(Settings settings)
        {
            var specialVars = (Settings) settings["special_vars"];
            var clearColor = (Color) specialVars["clear_color"];
            Gl.glClearColor(clearColor.Rf, clearColor.Gf, clearColor.Bf, clearColor.Af);
            Gl.glClearDepth(1f);
            Gl.glClearStencil(0);

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, (float[]) specialVars["light_position"]);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);

            var fogColor = (Color) specialVars["fog_color"];
            if ((bool) specialVars["fog_enabled"])
                Gl.glEnable(Gl.GL_FOG);
            Gl.glFogi(Gl.GL_FOG_MODE, Gl.GL_EXP);
            Gl.glFogfv(Gl.GL_FOG_COLOR, fogColor);
            Gl.glFogf(Gl.GL_FOG_DENSITY, (float) specialVars["fog_density"]);  
            Gl.glHint(Gl.GL_FOG_HINT, Gl.GL_DONT_CARE); 
            //Gl.glFogf(Gl.GL_FOG_START, .0001f);
            //Gl.glFogf(Gl.GL_FOG_END, 10f); 

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glLineWidth(1.0f);

            _waterlevel = (int) settings["waterlevel"];
            _watercolor = (Color) settings["special_vars"]["watercolor"];
            _zoom = (int) settings["zoom"];
            _colorTable = (ColorDescription[]) settings["colors"];
            Array.Sort(_colorTable, (color1, color2) => color1.Level.CompareTo(color2.Level));
        }

        /// <summary>
        /// Инициализирует окружение OpenGL. Необходим вызов один раз за программу, до
        /// начала любого рисования
        /// </summary>
        /// <param name="width">Ширина окна, в которое выводится изображение</param>
        /// <param name="height">Высота окна, в которое выводится изображение</param>
        public static void InitGL(int width, int height)
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Gl.glViewport(0, 0, width, height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(50, width/(float) height, .1, 1400);
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        static void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Fps = _frames;
            _frames = 0;
        }
    }
}
