using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace TerrainGen.Serialization
{
    /// <summary>
    /// Предоставляет методы для расшифровки бинарных файлов настроек
    /// </summary>
    /// <seealso cref="T:TerrainGen.Serialization.BinarySerializer"/>
    /// <seealso cref="T:TerrainGen.Serialization.JsonDeserializer"/>
    public class BinaryDeserializer : SerializeReader
    {
        private Queue<string> _queue;

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="T:TerrainGen.Serialization.BinaryDeserializer"/> class.
        /// </summary>
        /// <param name="filename">Путь к файлу с настройками</param>
        public BinaryDeserializer(string filename)
            : base(new BinaryReader(new FileStream(filename, FileMode.Open), Encoding.UTF8))
        {
        }

        private static string DecodeFrom64(string encodedData)
        {
            var encodedDataAsBytes
                = Convert.FromBase64String(Decrypt(encodedData, "I want to get paid for the work I'm doing."));
            var returnValue =
               Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }
        private static string Decrypt(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));

            return result.ToString();
        }

        /// <summary>
        /// Открывает файл, указанный в конструкторе
        /// </summary>
        /// <returns>
        /// <para><b>true</b></para>
        /// </returns>
        /// <exception cref="IOException">Файл используется другим процессом</exception>
        public override bool Open()
        {
            _queue = new Queue<string>();
            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                string s = Reader.ReadString();
                _queue.Enqueue(DecodeFrom64(s));
            }
            return true;
        }

        /// <summary>
        /// Закрывает поток
        /// </summary>
        /// <returns><b>true</b></returns>
        public override bool Close()
        {
            Reader.Close();
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
            return _queue.Count != 0 && Single.TryParse(_queue.Dequeue(), NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out f);
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
            if (_queue.Count == 0) return false;
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