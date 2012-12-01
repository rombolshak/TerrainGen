using System;

namespace TerrainGen.HeightMap
{
    /// <summary>
    /// Представляет карту высот, сгенерированную по diamond-square алгоритму
    /// </summary>
    class FactorialModel : IHeightMap
    {
        float[,] _map;
        readonly int _waterlevel = 50;

        /// <summary>
        /// Генерирует новую карту высот с заданными настройками
        /// </summary>
        /// <param name="s"><para>Настройки, использумые для генерации карты. Необходимо
        /// наличие следующих значений: </para>
        /// <list type="bullet">
        ///  <item>
        ///   <description>int size -- размер карты</description>
        ///  </item>
        ///  <item>
        ///   <description>int seed -- зерно генератора псевдослучайных чисел</description>
        ///  </item>
        ///  <item>
        ///   <description>double roughness -- т.н. &quot;шероховатость&quot;
        /// ландшафта</description>
        ///  </item>
        ///  <item>
        ///   <description>int waterlevel -- уровень воды</description>
        ///  </item>
        /// </list></param>
        public FactorialModel(Settings s) 
        {
            if (!CheckSettings(s)) throw new ArgumentException("Неверные настройки");

            int size = (int) s["size"], seed = (int) s["seed"];
            var roughness = (double) s["roughness"];
            _waterlevel = (int) s["waterlevel"];
            var r = new Random(seed);
            _map = new float[size, size];
            _map[0, 0] = r.Next(Math.Abs(r.Next((int) (roughness*size))));
            _map[0, size - 1] = r.Next(Math.Abs(r.Next((int) (roughness*size))));
            _map[size - 1, 0] = r.Next(Math.Abs(r.Next((int) (roughness*size))));
            _map[size - 1, size - 1] = r.Next(Math.Abs(r.Next((int) (roughness*size))));
            var currentSize = size;

            while (currentSize > 2)
            {
                int x = 0, y = 0;
                /** square **/
                while (y + currentSize <= size)
                {
                    while (x + currentSize <= size)
                    {
                        var centerX = x + currentSize / 2;
                        var centerY = y + currentSize / 2;
                        _map[centerX, centerY] = Math.Max((_map[x, y] + _map[x, y + currentSize - 1] + _map[x + currentSize - 1, y] + _map[x + currentSize - 1, y + currentSize - 1]) / 4 + r.Next(-(int)(currentSize * roughness), (int)(currentSize * roughness)), 0f);
                        x += currentSize - 1;
                    }
                    x = 0;
                    y += currentSize - 1;
                }
                x = y = 0;

                /** diamond **/
                int x1, y1, x2, y2, centerX1, centerX2, centerY1, centerY2;
                while (y + currentSize <= size)
                {
                    while (x + currentSize <= size)
                    {
                        centerX1 = x + currentSize / 2; centerY1 = y + currentSize / 2;

                        if (x == 0)
                        {
                            // left
                            x1 = x; y1 = y;
                            x2 = x1; y2 = y1 + currentSize - 1;
                            centerX2 = x - currentSize / 2; centerY2 = centerY1;
                            Diamond(size, roughness, r, currentSize, x1, y1, x2, y2, centerX1, centerY1, centerX2, centerY2);
                        }

                        if (y == 0)
                        {
                            // up
                            x1 = x; y1 = y;
                            x2 = x1 + currentSize - 1; y2 = y;
                            centerX2 = centerX1; centerY2 = y1 - currentSize / 2;
                            Diamond(size, roughness, r, currentSize, x1, y1, x2, y2, centerX1, centerY1, centerX2, centerY2);
                        }

                        // right
                        x1 = x + currentSize - 1; y1 = y;
                        x2 = x1; y2 = y1 + currentSize - 1;
                        centerX2 = x1 + currentSize / 2; centerY2 = centerY1;
                        Diamond(size, roughness, r, currentSize, x1, y1, x2, y2, centerX1, centerY1, centerX2, centerY2);

                        //bottom
                        x1 = x; y1 = y + currentSize - 1;
                        x2 = x1 + currentSize - 1; y2 = y1;
                        centerX2 = centerX1; centerY2 = y1 + currentSize / 2;
                        Diamond(size, roughness, r, currentSize, x1, y1, x2, y2, centerX1, centerY1, centerX2, centerY2);

                        x += currentSize - 1;
                    }
                    x = 0;
                    y += currentSize - 1;
                }
                currentSize = currentSize / 2 + 1;
            }
        }

        /// <summary>
        /// Расчитывает значение в центре ромба на шаге "diamond"
        /// </summary>
        /// <param name="size">Размер карты</param>
        /// <param name="roughness">Шероховатость</param>
        /// <param name="r">Экземпляр генератора псевдослучайных последовательностей</param>
        /// <param name="currentSize">Текущий размер квадрата</param>
        /// <param name="x1">Горизонтальная координата первого угла (точка на ребре квадрата)</param>
        /// <param name="y1">Вертикальная координата первого угла</param>
        /// <param name="x2">Горизонтальная координата второго угла</param>
        /// <param name="y2">Вертикальная координата второго угла</param>
        /// <param name="centerX1">Горизонтальная коорината центра первого квадрата (посчитанного шагом раньше)</param>
        /// <param name="centerY1">Вертикальная координата центра первого квадрата</param>
        /// <param name="centerX2">Горизонтальная координата второго квадрата</param>
        /// <param name="centerY2">Вертикальная координата второго квадрата</param>
        private void Diamond(int size, double roughness, Random r, int currentSize, int x1, int y1, int x2, int y2, int centerX1, int centerY1, int centerX2, int centerY2)
        {
            float a = _map[x1, y1];
            float b = ((x2 < 0) || (y2 < 0) || (x2 >= size) || (y2 >= size)) ? 0 : _map[x2, y2];
            float c = ((centerX1 < 0) || (centerY1 < 0) || (centerX1 >= size) || (centerY1 >= size)) ? 0 : _map[centerX1, centerY1];
            float d = ((centerX2 < 0) || (centerY2 < 0) || (centerX2 >= size) || (centerY2 >= size)) ? 0 : _map[centerX2, centerY2];
            _map[(x1 + x2) / 2, (y1 + y2) / 2] = Math.Max((a + b + c + d) / 4 + r.Next((int)(-currentSize * currentSize / 1.4 * roughness), (int)(currentSize * currentSize / 1.4 * roughness)), 0f);
        }

        private static bool CheckSettings(Settings s)
        {
            return (
                 s.Exists("size") && s.GetTypeByName("size") == typeof(int) &&
                 s.Exists("roughness") && s.GetTypeByName("roughness") == typeof(double) &&
                 s.Exists("seed") && s.GetTypeByName("seed") == typeof(int) &&
                 s.Exists("waterlevel") && s.GetTypeByName("waterlevel") == typeof(int)
                 );
        }

        /// <summary>
        /// Возвращает карту высот
        /// </summary>
        /// <returns>
        /// Карта высот в виде двумерного массива, значения лежат в промежутке [0; 255]
        /// </returns>
        public float[,] GetMap()
        {
            // нормировка карты на [0; 255]
            float min = _map[0, 0], max = _map[0, 0];
            for (var i = 0; i < _map.GetLength(0); ++i)
                for (var j = 0; j < _map.GetLength(1); ++j)
                {
                    if (_map[i, j] > max) max = _map[i, j];
                    if (_map[i, j] < min) min = _map[i, j];
                }
            float delim = (max - min);

            for (var i = 0; i < _map.GetLength(0); ++i)
                for (var j = 0; j < _map.GetLength(0); ++j)
                    _map[i, j] = (float)Math.Sqrt((_map[i, j] - min) / delim) * 255;

            // сглаживание пиков
            _map = Blur(_map);

            // установка уровня воды
            for (var i = 0; i < _map.GetLength(0); ++i)
                for (var j = 0; j < _map.GetLength(0); ++j)
                    if (_map[i, j] < _waterlevel)
                        _map[i, j] = _waterlevel;

            return _map;
        }

        /// <summary>
        /// Размывает карту по Гауссу
        /// </summary>
        /// <param name="map">Отнормированная карта высот</param>
        /// <returns></returns>
        private static float[,] Blur(float[,] map)
        {
            var matrix = new double[5, 5];
            matrix[0, 0] = matrix[4, 0] = matrix[0, 4] = matrix[4, 4] = .00078633;
            matrix[0, 1] = matrix[1, 0] = matrix[0, 3] = matrix[3, 0] = .00655965;
            matrix[0, 2] = matrix[2, 0] = matrix[4, 2] = matrix[2, 4] = .01330373;
            matrix[1, 1] = matrix[1, 3] = matrix[3, 1] = matrix[3, 3] = .05472157;
            matrix[1, 2] = matrix[2, 1] = matrix[3, 2] = matrix[2, 3] = .11098164;
            matrix[2, 2] = .22508352;
            var res = new float[map.GetLength(0), map.GetLength(1)];
            for (var i = 0; i < map.GetLength(0); ++i)
                for (var j = 0; j < map.GetLength(1); ++j)
                    for (var k = 0; k < 5; ++k)
                        for (var t = 0; t < 5; ++t)
                            res[i, j] += (float)(matrix[k, t] * map[Math.Abs(i + k - 2) % map.GetLength(0), Math.Abs(j + t - 2) % map.GetLength(1)]);
            return res;
        }
    }
}
