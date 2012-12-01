using System;
using System.Collections.Generic;
using Tao.OpenGl;

namespace TerrainGen
{
    /// <summary>
    /// Предоставляет методы для реализации алгоритма отсечения невидимых областей
    /// </summary>
    /// <remarks>
    /// Не используйте этот класс. Он несовершенен. Ну и не работает, как положено
    /// </remarks>
    class FrustrumCuller
    {
        private readonly float[,] _frustrum = new float[4,4];

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerrainGen.FrustrumCuller"/>
        /// class.
        /// </summary>
        public FrustrumCuller()
        {
            ExtractFrustrum();
        }

        /// <summary>
        /// Извлекает текущий объем видимости камеры. По форме является усеченной пирамидой
        /// </summary>
        /// <remarks>
        /// В этой реализации отброшены две стенки: верхняя и нижняя, чтобы не проверять
        /// вхождение объекта по высоте
        /// </remarks>
        private void ExtractFrustrum()
        {
            var proj = new float[16];
            var modl = new float[16];
            var clip = new float[16];

            Gl.glGetFloatv(Gl.GL_PROJECTION_MATRIX, proj);
            Gl.glGetFloatv(Gl.GL_MODELVIEW_MATRIX, modl);

            Multiply(proj, modl, clip);

            // right
            _frustrum[0,0] = clip[3] - clip[0];
            _frustrum[0,1] = clip[7] - clip[4];
            _frustrum[0,2] = clip[11] - clip[8];
            _frustrum[0,3] = clip[15] - clip[12];
            Normalize(0);

            // left
            _frustrum[1,0] = clip[3] + clip[0];
            _frustrum[1,1] = clip[7] + clip[4];
            _frustrum[1,2] = clip[11] + clip[8];
            _frustrum[1,3] = clip[15] + clip[12];
            Normalize(1);

            //// bottom
            //_frustrum[2,0] = clip[3] + clip[1];
            //_frustrum[2,1] = clip[7] + clip[5];
            //_frustrum[2,2] = clip[11] + clip[9];
            //_frustrum[2,3] = clip[15] + clip[13];
            //Normalize(2);

            //// up
            //_frustrum[3,0] = clip[3] - clip[1];
            //_frustrum[3,1] = clip[7] - clip[5];
            //_frustrum[3,2] = clip[11] - clip[9];
            //_frustrum[3,3] = clip[15] - clip[13];
            //Normalize(3);

            // back
            _frustrum[2,0] = clip[3] - clip[2];
            _frustrum[2,1] = clip[7] - clip[6];
            _frustrum[2,2] = clip[11] - clip[10];
            _frustrum[2,3] = clip[15] - clip[14];
            Normalize(2);

            // front
            _frustrum[3,0] = clip[3] + clip[2];
            _frustrum[3,1] = clip[7] + clip[6];
            _frustrum[3,2] = clip[11] + clip[10];
            _frustrum[3,3] = clip[15] + clip[14];
            Normalize(3);
        }

        private void Normalize(int i)
        {
            var t = (float) Math.Sqrt(_frustrum[i,0]*_frustrum[i,0] + _frustrum[i,1]*_frustrum[i,1] +
                                        _frustrum[i,2]*_frustrum[i,2]);
            _frustrum[i,0] /= t;
            _frustrum[i,1] /= t;
            _frustrum[i,2] /= t;
            _frustrum[i,3] /= t;
        }

        private static void Multiply(float[] proj, float[] modl, float[] clip)
        {
            clip[0] = modl[0]*proj[0] + modl[1]*proj[4] + modl[2]*proj[8] + modl[3]*proj[12];
            clip[1] = modl[0]*proj[1] + modl[1]*proj[5] + modl[2]*proj[9] + modl[3]*proj[13];
            clip[2] = modl[0]*proj[2] + modl[1]*proj[6] + modl[2]*proj[10] + modl[3]*proj[14];
            clip[3] = modl[0]*proj[3] + modl[1]*proj[7] + modl[2]*proj[11] + modl[3]*proj[15];

            clip[4] = modl[4]*proj[0] + modl[5]*proj[4] + modl[6]*proj[8] + modl[7]*proj[12];
            clip[5] = modl[4]*proj[1] + modl[5]*proj[5] + modl[6]*proj[9] + modl[7]*proj[13];
            clip[6] = modl[4]*proj[2] + modl[5]*proj[6] + modl[6]*proj[10] + modl[7]*proj[14];
            clip[7] = modl[4]*proj[3] + modl[5]*proj[7] + modl[6]*proj[11] + modl[7]*proj[15];

            clip[8] = modl[8]*proj[0] + modl[9]*proj[4] + modl[10]*proj[8] + modl[11]*proj[12];
            clip[9] = modl[8]*proj[1] + modl[9]*proj[5] + modl[10]*proj[9] + modl[11]*proj[13];
            clip[10] = modl[8]*proj[2] + modl[9]*proj[6] + modl[10]*proj[10] + modl[11]*proj[14];
            clip[11] = modl[8]*proj[3] + modl[9]*proj[7] + modl[10]*proj[11] + modl[11]*proj[15];

            clip[12] = modl[12]*proj[0] + modl[13]*proj[4] + modl[14]*proj[8] + modl[15]*proj[12];
            clip[13] = modl[12]*proj[1] + modl[13]*proj[5] + modl[14]*proj[9] + modl[15]*proj[13];
            clip[14] = modl[12]*proj[2] + modl[13]*proj[6] + modl[14]*proj[10] + modl[15]*proj[14];
            clip[15] = modl[12]*proj[3] + modl[13]*proj[7] + modl[14]*proj[11] + modl[15]*proj[15];
        }

        /// <summary>
        /// Проверяет, входит ли указанный объем в текущую область видимости
        /// </summary>
        /// <param name="boundary">Объем ограничивающий что-либо в пространстве</param>
        /// <returns>
        /// <para><b>0</b>, если объем не содержится в области видимости,</para>
        /// <para><b>1</b>, если содержится частично,</para>
        /// <para><b>2</b>, если содержится полностью</para>
        /// </returns>
        public int BoundaryInFrustrum(Boundary boundary)
        {
            int c2 = 0;
            for (int p = 0; p < 4; ++p)
            {
                int c = 0;
                if (_frustrum[p, 0] * boundary.Point.X + _frustrum[p, 1] * boundary.Point.Z + _frustrum[p, 3] > 0) ++c;
                if (_frustrum[p, 0] * boundary.Point.X + _frustrum[p, 1] * (boundary.Point.Z + boundary.Rad) + _frustrum[p, 3] > 0) ++c;
                if (_frustrum[p, 0] * (boundary.Point.X + boundary.Rad) + _frustrum[p, 1] * boundary.Point.Z + _frustrum[p, 3] > 0) ++c;
                if (_frustrum[p, 0] * (boundary.Point.X + boundary.Rad) + _frustrum[p, 1] * (boundary.Point.Z + boundary.Rad) + _frustrum[p, 3] > 0) ++c;
                if (c == 0) return 0;
                if (c == 4) ++c2;
            }
            return (c2 == 4) ? 2 : 1;
        }

        /// <summary>
        /// Описывает указанное дерево, подготавливая индексы вершин для отрисовки
        /// </summary>
        /// <remarks>
        /// Этот метод работает крайне разочаровывающе. Не используйте его, рисуйте все
        /// вершины
        /// </remarks>
        /// <param name="tree">Дерево, описывающее необходимую часть ландшафта</param>
        /// <param name="mapSize">Размер карты</param>
        /// <returns>
        /// Список индексов вершин, в том порядке, в котором необходимо их отрисовать
        /// </returns>
        public uint[] RenderTree(QuadTree tree, int mapSize)
        {
            if (tree.Boundary.Rad == 1)
                return new uint[] { };
            if (tree.Boundary.Rad == 2)
                return DescribeIndicies(tree.Boundary.Point.X, tree.Boundary.Point.Z, mapSize);

            var res = new List<uint>();
            foreach (var quadTree in tree.Children)
                res.AddRange(RenderTree(quadTree, mapSize));

            return res.ToArray();
        }

        private uint[] DescribeIndicies(int i, int j, int length)
        {
            var iMap = new uint[6];
            iMap[0] = (uint)(i * (length + 1) + j);
            iMap[1] = (uint)((i + 1) * (length + 1) + j);
            iMap[2] = (uint)(i * (length + 1) + j + 1);
            iMap[3] = (uint)(i * (length + 1) + j + 1);
            iMap[4] = (uint)((i + 1) * (length + 1) + j);
            iMap[5] = (uint)((i + 1) * (length + 1) + j + 1);
            return iMap;
        }

        //public float SphereInFrustrum(float x, float y, float z, float r)
        //{
        //    int p;
        //    int c = 0;
        //    float d;

        //    for (p = 0; p < 6; ++p)
        //    {
        //        d = _frustrum[p, 0]*x + _frustrum[p, 1]*y + _frustrum[p, 2]*z + _frustrum[p, 3];
        //        if (d <= -r)
        //            return 0;
        //        if (d > r)
        //            ++c;
        //    }
        //    return (c == 6) ? 2 : 1;
        //}
    }
}
