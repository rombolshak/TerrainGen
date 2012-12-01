using System;

namespace TerrainGen.HeightMap
{
    /// <summary>
    /// Представляет карту высот, сгенерированную по &quot;hill algorithm&quot;
    /// </summary>
    /// <remarks>
    /// Не рекомендуется использовать этот класс, карта получается слишком гористая
    /// </remarks>
    class HillModel : IHeightMap
    {
        float[,] _map;

        /// <summary>
        /// Генерирует новую карту высот с заданными настройками
        /// </summary>
        /// <param name="s"><para>Настройки, по которым строится карта высот. Необходимы
        /// значения:</para>
        /// <list type="bullet">
        ///  <item>
        ///   <description>int width -- ширина карты</description>
        ///  </item>
        ///  <item>
        ///   <description>int height -- глубина карты</description>
        ///  </item>
        ///  <item>
        ///   <description>int seed -- зерно генератора псевдослучайных
        /// последовательностей</description>
        ///  </item>
        ///  <item>
        ///   <description>int waterlevel -- уровень воды</description>
        ///  </item>
        /// </list></param>
        public HillModel(Settings s) 
        {
            if (!CheckSettings(s)) throw new ArgumentException("Неверно заданы настройки");

            int width = (int)s["width"], height = (int)s["height"], seed = (int)s["seed"];
            int waterlevel = (int) s["waterlevel"];
            _map = new float[width, height];
            Random r = new Random(seed);

            // предел q тоже хорошо бы вынести в настройки -- задает количество холмов
            for (int q = 0; q < 20; ++q)
            {
                int x = r.Next(0, width), y = r.Next(0, height), rad = r.Next(0, 10);
                for (int i = x - 2 * rad - 1; i < x + 2 * rad - 1; ++i)
                    for (int j = y - 2 * rad - 1; j < y + 2 * rad - 1; ++j)
                        if ((i >= 0) && (i < width) && (j >= 0) && (j < height))
                        _map[i, j] += rad * rad - ((i - x) * (i - x) + (j - y) * (j - y));
            }
            
            // нгормировка на [0; 255]
            float min = _map[0,0], max = _map[0,0];
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                {
                    if (_map[i, j] > max) max = _map[i, j];
                    if (_map[i, j] < min) min = _map[i, j];
                }
            float delim = (max - min);

            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    _map[i, j] = (float)Math.Sqrt((_map[i, j] - min) / delim) * 255;

            // сглаживание
            _map = Blur(_map);

            for (int i = 0; i < _map.GetLength(0); ++i)
                for (int j = 0; j < _map.GetLength(0); ++j)
                    if (_map[i, j] < waterlevel)
                        _map[i, j] = waterlevel;
        }

        private float[,] Blur(float[,] map)
        {
            var matrix = new double[5, 5];
            matrix[0, 0] = matrix[4, 0] = matrix[0, 4] = matrix[4, 4] = .00078633;
            matrix[0, 1] = matrix[1, 0] = matrix[0, 3] = matrix[3, 0] = .00655965;
            matrix[0, 2] = matrix[2, 0] = matrix[4, 2] = matrix[2, 4] = .01330373;
            matrix[1, 1] = matrix[1, 3] = matrix[3, 1] = matrix[3, 3] = .05472157;
            matrix[1, 2] = matrix[2, 1] = matrix[3, 2] = matrix[2, 3] = .11098164;
            matrix[2, 2] = .22508352;
            var res = new float[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); ++i)
                for (int j = 0; j < map.GetLength(1); ++j)
                    for (int k = 0; k < 5; ++k)
                        for (int t = 0; t < 5; ++t)
                            res[i, j] += (float)(matrix[k, t] * map[Math.Abs(i + k - 2) % map.GetLength(0), Math.Abs(j + t - 2) % map.GetLength(1)]);
            return res;
        }

        private bool CheckSettings(Settings s)
        {
            return (
                s.Exists("width") && s.GetTypeByName("width") == typeof(int) &&
                s.Exists("height") && s.GetTypeByName("height") == typeof(int) &&
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
            return _map;
        }
    }
}
