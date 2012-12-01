using System;

namespace TerrainGen
{
    /// <summary>
    /// Представляет любой цвет в формате RGBA
    /// </summary>
    class Color
    {
        private int _r, _g, _b, _a;
        /// <summary>
        /// Получает или задает красную компоненту цвета
        /// </summary>
        /// <value>
        /// Целое число от 0 до 255 включительно
        /// </value>
        public int R { get { return _r; }
            private set { _r = value; } }
        /// <summary>
        /// Получает или задает зеленую компоненту цвета
        /// </summary>
        /// <value>
        /// Целое число от 0 до 255 включительно
        /// </value>
        public int G { get { return _g; }
            private set { _g = value; } }
        /// <summary>
        /// Получает или задает синюю компоненту цвета
        /// </summary>
        /// <value>
        /// Целое число от 0 до 255 включительно
        /// </value>
        public int B { get { return _b; }
            private set { _b = value; } }
        /// <summary>
        /// Получает или задает прозрачность цвета
        /// </summary>
        /// <value>
        /// Целое число от 0 до 255 включительно
        /// </value>
        public int A { get { return _a; }
            private set { _a = value; } }

        /// <summary>
        /// Получает или задает красную компоненту цвета
        /// </summary>
        /// <value>
        /// Число от 0 до 1 включительно
        /// </value>
        public float Rf { get { return _r / 255f; }
            private set { _r = (int) (value * 255); } }
        /// <summary>
        /// Получает или задает зеленую компоненту цвета
        /// </summary>
        /// <value>
        /// Число от 0 до 1 включительно
        /// </value>
        public float Gf { get { return _g / 255f; }
            private set { _g = (int) (value * 255); } }
        /// <summary>
        /// Получает или задает синюю компоненту цвета
        /// </summary>
        /// <value>
        /// Число от 0 до 1 включительно
        /// </value>
        public float Bf { get { return _b / 255f; }
            private set { _b = (int) (value * 255); } }
        public float Af { get { return _a / 255f; }
            private set { _a = (int) (value * 255); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerrainGen.Color"/> class.
        /// </summary>
        /// <param name="r">Красная компонента цвета. Число от 0 до 255</param>
        /// <param name="g">Зеленая компонента цвета. Число от 0 до 255</param>
        /// <param name="b">Синяя компонента цвета. Число от 0 до 255</param>
        /// <param name="a">Прозрачность. Значение по-умолчанию 255.</param>
        public Color(int r, int g, int b, int a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerrainGen.Color"/> class.
        /// </summary>
        /// <param name="r">Красная компонента цвета. Число от 0 до 1</param>
        /// <param name="g">Зеленая компонента цвета. Число от 0 до 1</param>
        /// <param name="b">Синяя компонента цвета. Число от 0 до 1</param>
        /// <param name="a">Прозрачность. Значение по-умолчанию 1f.</param>
        public Color(float r, float g, float b, float a = 1f)
        {
            Rf = r;
            Gf = g;
            Bf = b;
            Af = a;
        }

        /// <summary>
        /// Возвращает цвет как массив чисел с плавающей точкой
        /// </summary>
        /// <returns>
        /// Массив из 4 элементов (r, g, b, a)
        /// </returns>
        public float[] ToArray()
        {
            return new[] {Rf, Gf, Bf, Af};
        }

        /// <summary>
        /// Задает неявное преобразование цвета в массив чисел с плавающей точкой
        /// </summary>
        /// <param name="c">Цвет, который необходимо преобразовать</param>
        public static implicit operator float[](Color c)
        {
            return c.ToArray();
        }
        /// <summary>
        /// Задает неявное преобразование массива чисел с плавающей точкой в экземпляр
        /// класса <see cref="T:TerrainGen.Color"/>
        /// </summary>
        /// <param name="arr">Массив из 3 или 4 элементов, где первый элемент суть красная
        /// компонента, второй -- зеленая, третий -- синяя, четвертая -- прозрачность. Если
        /// прозрачность не задана, то принимается равной единице</param>
        public static implicit operator Color(float[] arr)
        {
            if (arr.Length < 3 || arr.Length > 4)
                throw new ArgumentException("Неверное число элементов в массиве (должно быть 3 либо 4)", "arr");
            return new Color(arr[0], arr[1], arr[2], arr.Length == 4 ? arr[3] : 1f);
        }
    }
}
