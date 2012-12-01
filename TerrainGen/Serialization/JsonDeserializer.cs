using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TerrainGen.Serialization
{
    /// <summary>
    /// Предоставляет методы для чтения файлов настроек в формате JSON
    /// </summary>
    /// <seealso cref="T:TerrainGen.Serialization.BinaryDeserializer"/>
    /// <seealso cref="T:TerrainGen.Serialization.JsonSerializer"/>
    public class JsonDeserializer : SerializeReader
    {
        private Queue<string> _queue;

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="T:TerrainGen.Serialization.JsonDeserializer"/> class.
        /// </summary>
        /// <param name="filename">Путь к файлу с настройками</param>
        public JsonDeserializer(string filename) : base(new StreamReader(filename))
        {
        }

        /// <summary>
        /// Открывает файл, указанный в конструкторе
        /// </summary>
        /// <returns>
        /// <para><b>true</b></para>
        /// </returns>
        public override bool Open()
        {
            var text = Reader.ReadToEnd();
            string[] lines = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            _queue = new Queue<string>();
            foreach (var pair in lines.Select(line => line.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)))
            {
                if (pair.Length == 2)
                {
                    var key = ClearString(pair[0]);
                    var val = pair[1];

                    _queue.Enqueue(key);
                    if (val[val.Length - 1] == ',')
                    {
                        _queue.Enqueue(ClearString(val.Substring(0, val.Length - 1)));
                        _queue.Enqueue(",");
                    }
                    else _queue.Enqueue(ClearString(val));
                }
                else
                {
                    var s = ClearString(pair[0]);
                    if (s[s.Length - 1] == ',')
                    {
                        _queue.Enqueue(s.Substring(0, s.Length - 1));
                        _queue.Enqueue(",");
                    }
                    else _queue.Enqueue(s);
                }
            }
            return true;
        }

        private static string ClearString(string p)
        {
            p = p.Trim();
            var ind0 = p.IndexOf("\"", StringComparison.Ordinal);
            var ind1 = p.LastIndexOf("\"", StringComparison.Ordinal);
            if (ind0 != -1 && ind1 != -1)
                p = p.Substring(ind0 + 1, ind1 - ind0 - 1);
            return p;
        }

        /// <summary>
        /// Закрывает поток
        /// </summary>
        /// <returns><b>true</b></returns>
        public override bool Close()
        {
            return true;
        }

        /// <summary>
        /// Обеспечивает вывод строки из объекта десериализатора
        /// </summary>
        /// <param name="str">Строка, в которую будет записано значение</param>
        /// <returns>
        /// <para><b>true</b>, если удалось получить строку, и
        /// <b>false</b>, если достигнут конец файла</para>
        /// </returns>
        public override bool InOut(ref string str)
        {
            if (_queue.Count == 0) return false;
            str = _queue.Dequeue();
            return true;
        }

        /// <summary>
        /// Обеспечивает вывод типа Int32 из объекта десериализатора
        /// </summary>
        /// <param name="i">Число, в которую будет записано значение</param>
        /// <returns>
        /// <para><b>true</b>, если удалось получить число, и
        /// <b>false</b>, если достигнут конец файла либо строка не является представлением числа</para>
        /// </returns>
        public override bool InOut(ref int i)
        {
            return _queue.Count != 0 && Int32.TryParse(_queue.Dequeue(), out i);
        }

        /// <summary>
        /// Обеспечивает вывод типа Double из объекта десериализатора
        /// </summary>
        /// <param name="d">Число, в которую будет записано значение</param>
        /// <returns>
        /// <para><b>true</b>, если удалось получить число, и
        /// <b>false</b>, если достигнут конец файла либо строка не является представлением числа</para>
        /// </returns>
        public override bool InOut(ref double d)
        {
            return _queue.Count != 0 && Double.TryParse(_queue.Dequeue(), out d);
        }

        /// <summary>
        /// Обеспечивает вывод типа Single из объекта десериализатора
        /// </summary>

        /// <param name="f">Число, в которую будет записано значение</param>
        /// <returns>
        /// <para><b>true</b>, если удалось получить число, и
        /// <b>false</b>, если достигнут конец файла либо строка не является представлением числа</para>
        /// </returns>
        public override bool InOut(ref float f)
        {
            return _queue.Count != 0 && Single.TryParse(_queue.Dequeue(), out f);
        }

        /// <summary>
        /// Обеспечивает вывод типа Boolean из объекта десериализатора
        /// </summary>
        /// <param name="b">Число, в которую будет записано значение</param>
        /// <returns>
        /// <para><b>true</b>, если удалось получить значение, и
        /// <b>false</b>, если достигнут конец файла либо строка не является представлением булева значения</para>
        /// </returns>
        public override bool InOut(ref bool b)
        {
            return _queue.Count != 0 && Boolean.TryParse(_queue.Dequeue(), out b);
        }

        /// <summary>
        /// Проверяет, является ли текущая строка представлением специального маркера
        /// </summary>
        /// <param name="ch">Специальный маркер, на соответствие которому необходимо
        /// проверить текущую строку</param>
        /// <param name="delete">Если <b>true</b>, удаляет маркер, иначе оставляет его для
        /// последующего чтения</param>
        /// <returns>
        /// <para><b>true</b>, если маркеры совпадают, и <b>false</b>, если достигнут конец
        /// файла либо маркеры не совпадают</para>
        /// </returns>
        public override bool InOut(SpecialChars ch, bool delete = true)
        {
            var s = delete ? _queue.Dequeue() : _queue.Peek();
            return ch == ParseSpecialChar(s);
        }

        private static SpecialChars ParseSpecialChar(string s)
        {
            switch (s)
            {
                case "{": return SpecialChars.ObjectStart;
                case "}": return SpecialChars.ObjectEnd;
                case "[": return SpecialChars.ArrayStart;
                case "]": return SpecialChars.ArrayEnd;
                case ",": return SpecialChars.Delimiter;
                default: return SpecialChars.Null;
            }
        }

        /// <summary>
        /// Возвращает текущий специальный маркер
        /// </summary>
        /// <returns></returns>
        public override SpecialChars TestCurrentSpecialChar()
        {
            return _queue.Count == 0 ? SpecialChars.Null : ParseSpecialChar(_queue.Peek());
        }
    }
}