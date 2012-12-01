using System;

namespace TerrainGen
{
    /// <summary>
    /// Предоставляет методы-помощники общего назначения
    /// </summary>
    static class CommonHelper
    {
        private class Vector
        {
            public float X, Y, Z;
        }


        /// <summary>
        /// Генерирует карту нормалей на основе заданной карты высот
        /// </summary>
        /// <param name="hMap">Карта высот, полученная от <see
        /// cref="M:TerrainGen.HeightMap.IHeightMap.GetMap"/></param>
        /// <returns>
        /// Двумерный массив векторов. Каждой вершине в соответствие ставится трехмерный
        /// вектор нормали
        /// </returns>
        internal static float[,][] GenerateNormalMap(float[,] hMap)
        {
            // сначала нужно посчитать нормали к плоскостям, которые образовывают вершины
            var nPlaneMap = new float[hMap.GetLength(0) - 1, hMap.GetLength(1) - 1, 2][];
            for (var i = 0; i < nPlaneMap.GetLength(0); ++i)
                for (var j = 0; j < nPlaneMap.GetLength(1); ++j)
                    for (var k = 0; k < 2; ++k)
                        nPlaneMap[i, j, k] = CalculateNormal(
                            i, j + k, hMap[i, j + k],
                            i + k, j + 1 - k, hMap[i + k, j + 1 - k],
                            i + 1, j + k, hMap[i + 1, j + k]);
            // а затем усреднить нормали для каждый вершины
            var nMap = new float[hMap.GetLength(0), hMap.GetLength(1)][];
            var nullVector = new float[] { 0, 0, 0 };
            for (var i = 0; i < hMap.GetLength(0); ++i)
                for (var j = 0; j < hMap.GetLength(1); ++j)
                {
                    nMap[i, j] = Normalize(SumVectors(
                        ((i != hMap.GetLength(0) - 1) && (j != hMap.GetLength(1) - 1)) ? nPlaneMap[i, j, 0] : nullVector,
                        ((j != 0) && (i != hMap.GetLength(0) - 1)) ?                     Inverse(nPlaneMap[i, j - 1, 1]) : nullVector,
                        ((j != 0) && (i != hMap.GetLength(0) - 1)) ?                     nPlaneMap[i, j - 1, 0] : nullVector,
                        ((i != 0) && (j != 0)) ?                                         Inverse(nPlaneMap[i - 1, j - 1, 1]) : nullVector,
                        ((i != 0) && (j != hMap.GetLength(1) - 1))?                      nPlaneMap[i - 1, j, 0] : nullVector,
                        ((i != 0) && (j != hMap.GetLength(1) - 1))?                      Inverse(nPlaneMap[i - 1, j, 1]) : nullVector
                        ));
                }

            return nMap;
        }

        private static float[] Inverse(float[] p)
        {
            return new[] { -p[0], -p[1], -p[2] };
        }

        private static float[] SumVectors(float[] v1, float[] v2, float[] v3, float[] v4, float[] v5, float[] v6)
        {
            var r = new float[3];
            for (var i = 0; i < 3; ++i)
                r[i] = v1[i] + v2[i] + v3[i] + v4[i] + v5[i] + v6[i];
            return r;
        }

        private static float[] CalculateNormal(int x1, int y1, float z1, int x2, int y2, float z2, int x3, int y3, float z3)
        {
            Vector v1 = new Vector(), v2 = new Vector();
            v1.X = x2 - x1; v2.X = x3 - x1;
            v1.Y = y2 - y1; v2.Y = y3 - y1;
            v1.Z = z2 - z1; v2.Z = z3 - z1;
            return new[] {
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            };
        }

        private static float[] Normalize(float[] p)
        {
            var magnitude = Magnitude(p);
            return new[] { p[0] / magnitude, p[1] / magnitude, p[2] / magnitude };
        }

        private static float Magnitude(float[] p)
        {
            return (float)Math.Sqrt(
                p[0] * p[0] + 
                p[1] * p[1] + 
                p[2] * p[2]
                );
        }
    }
}
