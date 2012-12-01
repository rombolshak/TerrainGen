namespace TerrainGen
{
    /// <summary>
    /// Представляет точку в трехмерном пространстве
    /// </summary>
    class Point3D
    {
        /// <summary>
        /// Получает или задает позицию по ширине
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Получает или задает позицию по глубине (вдаль)
        /// </summary>
        public int Z { get; private set; }
        /// <summary>
        /// Получает или задает позицию по высоте
        /// </summary>
        public float H { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerrainGen.Point3D"/> class.
        /// </summary>
        /// <param name="x">Положение по ширине</param>
        /// <param name="z">Положение по глубине</param>
        /// <param name="h">Положение по высоте</param>
        public Point3D(int x, int z, float h)
        {
            X = x;
            Z = z;
            H = h;
        }
    }

    /// <summary>
    /// Представляет объем, описывающий некоторую область в пространстве
    /// </summary>
    /// <remarks>
    /// В этой реализации является квадратным столбом без нижней и верхней границ (можно
    /// считать их бесконечно далекими)
    /// </remarks>
    class Boundary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerrainGen.Boundary"/> class.
        /// </summary>
        /// <param name="point">Точка, задающая левый нижний угол квадрата</param>
        /// <param name="rad">Сторона квадрата</param>
        public Boundary(Point3D point, int rad)
        {
            Point = point;
            Rad = rad;
        }
        public Boundary(int x, int z, float h, int rad) : this(new Point3D(x,z,h), rad){}

        /// <summary>
        /// Левый нижний угол
        /// </summary>
        public Point3D Point { get; private set; }

        /// <summary>
        /// Длина стороны квадрата
        /// </summary>
        public int Rad { get; private set; }

        /// <summary>
        /// Проверяет, содержится ли точка в текущем объеме
        /// </summary>
        /// <param name="p"></param>
        public bool ContainsPoint(Point3D p)
        {
            return p.X >= Point.X && p.X - Point.X <= Rad && p.Z >= Point.Z && p.Z - Point.Z <= Rad;
        }
    }

    /// <summary>
    /// Представляет квадродерево
    /// </summary>
    /// <remarks>
    /// Основная черта квадродерева -- наличие у каждого элемента, не являющегося
    /// листом, 4 дочерних
    /// </remarks>
    class QuadTree
    {
        /// <summary>
        /// Получает объем, ограничивающий данное дерево
        /// </summary>
        public Boundary Boundary { get; private set; }
        private readonly QuadTree[] _children = new QuadTree[4];
        /// <summary>
        /// Получает уровень дерева. Чем больше уровень, тем меньшим объемом оно описывается
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Возвращает 4 потомков данного дерева
        /// </summary>
        public QuadTree[] Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerrainGen.QuadTree"/> class.
        /// </summary>
        /// <param name="boundary">Объем, в котором находится создаваемое дерево</param>
        /// <param name="hMap">Карта высот, чьими значениями будут заполняться вершины
        /// дерева</param>
        /// <param name="level">Уровень дерева</param>
        public QuadTree(Boundary boundary, float[,] hMap, int level)
        {
            Boundary = boundary;
            Level = level;
            if (boundary.Rad == 1) return;
            if (boundary.Rad == 2)
            {
                int x = Boundary.Point.X, z = Boundary.Point.Z;
                Children[0] = new QuadTree(new Boundary(x, z,          hMap[x, z], 1), hMap, Level + 1);
                Children[1] = new QuadTree(new Boundary(x + 1, z,      hMap[x + 1, z], 1), hMap, Level + 1);
                Children[2] = new QuadTree(new Boundary(x, z + 1,      hMap[x, z + 1], 1), hMap, Level + 1);
                Children[3] = new QuadTree(new Boundary(x + 1, z + 1,  hMap[x + 1, z + 1], 1), hMap, Level + 1);
                return;
            }
            Subdivide(hMap);
        }

        private void Subdivide(float[,] hMap)
        {
            int x = Boundary.Point.X, z = Boundary.Point.Z, newRad = Boundary.Rad / 2;
            Children[0] = new QuadTree(new Boundary(x, z,                   hMap[x, z], newRad + 1),                   hMap, Level + 1);
            Children[1] = new QuadTree(new Boundary(x + newRad, z,          hMap[x + newRad, z], newRad + 1),          hMap, Level + 1);
            Children[2] = new QuadTree(new Boundary(x, z + newRad,          hMap[x, z + newRad], newRad + 1),          hMap, Level + 1);
            Children[3] = new QuadTree(new Boundary(x + newRad, z + newRad, hMap[x + newRad, z + newRad], newRad + 1), hMap, Level + 1);
        }
    }
}
