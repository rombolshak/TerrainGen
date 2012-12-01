using System;
using System.IO;

namespace TerrainGen.Serialization
{
    /// <summary>
    /// Предоставляет методы для сохранения файлов настроек формате JSON
    /// </summary>
    /// <seealso cref="T:TerrainGen.Serialization.JsonDeserializer"/>
    /// <seealso cref="T:TerrainGen.Serialization.BinarySerializer"/>
    class JsonSerializer : SerializeWriter
    {
        private string _indent = "";

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="T:TerrainGen.Serialization.JsonSerializer"/> class.
        /// </summary>
        /// <param name="filename">Путь к файлу, в который будет производиться сохранение
        /// данных</param>
        /// <exception cref="IOException">Файл используется другим процессом</exception>
        public JsonSerializer(string filename) : base(new StreamWriter(filename))
        {
        }

        /// <summary>
        /// Производит открытие файла
        /// </summary>
        /// <returns>
        /// <b>true</b>
        /// </returns>
        /// <remarks>Формальная реализация интерфейса</remarks>
        public override bool Open()
        {
            return true;
        }

        /// <summary>
        /// Обеспечивает корректное закрытие файла
        /// </summary>
        /// <returns>
        /// true
        /// </returns>
        public override bool Close()
        {
            Writer.Close();
            return true;
        }

        /// <summary>
        /// Обеспечивает запись строки
        /// </summary>
        /// <param name="str">Строка, значение которой будет записано</param>
        /// <returns>
        /// <b>true</b>, если запись прошла успешно, <b>false</b> в противном случае
        /// </returns>
        public override bool InOut(ref string str)
        {
            try
            {
                Writer.Write("\"{0}\"", str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Обеспечивает запись числа целочисленного типа
        /// </summary>
        /// <param name="i">Число, значение которого будет записано</param>
        /// <returns>
        /// <b>true</b>, если запись прошла успешно, <b>false</b> в противном случае
        /// </returns>
        public override bool InOut(ref int i)
        {
            try
            {
                Writer.Write(i);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Обеспечивает запись числа с плавающей точкой двойной точности
        /// </summary>
        /// <param name="d">Число, значение которого будет записано</param>
        /// <returns>
        /// <b>true</b>, если запись прошла успешно, <b>false</b> в противном случае
        /// </returns>
        public override bool InOut(ref double d)
        {
            try
            {
                Writer.Write(d);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Обеспечивает запись числа с плавающей точкой
        /// </summary>
        /// <param name="f">Число, значение которого будет записано</param>
        /// <returns>
        /// <b>true</b>, если запись прошла успешно, <b>false</b> в противном случае
        /// </returns>
        public override bool InOut(ref float f)
        {
            try
            {
                Writer.Write(f);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Обеспечивает запись булева значения
        /// </summary>
        /// <param name="b">Значение, которое необходимо записать</param>
        /// <returns>
        /// <b>true</b>, если запись прошла успешно, <b>false</b> в противном случае
        /// </returns>
        public override bool InOut(ref bool b)
        {
            try
            {
                Writer.Write(b);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Обеспечивает запись одного из специальных маркеров
        /// </summary>
        /// <param name="ch">Маркер, который нужно записать</param>
        /// <returns>
        /// <b>true</b>, если запись прошла успешно, <b>false</b> в противном случае
        /// </returns>
        public override bool InOut(SpecialChars ch)
        {
            switch (ch)
            {
                case SpecialChars.Delimiter:
                    Writer.Write(",{1}{0}", _indent, Environment.NewLine);
                    break;
                case SpecialChars.ArrayStart:
                    _indent += "   ";
                    Writer.Write("[{1}{0}", _indent, Environment.NewLine);
                    break;
                case SpecialChars.ArrayEnd:
                    _indent = _indent.Substring(3);
                    Writer.Write("{1}{0}]", _indent, Environment.NewLine);
                    break;
                case SpecialChars.ObjectStart:
                    _indent += "   ";
                    Writer.Write("{1}{2}{0}", _indent, "{", Environment.NewLine);
                    break;
                case SpecialChars.ObjectEnd:
                    _indent = _indent.Substring(3);
                    Writer.Write("{2}{0}{1}", _indent, "}", Environment.NewLine);
                    break;
                case SpecialChars.Null:
                    break;
            }
            return true;
        }

        /// <summary>
        /// Записывает указанную строку
        /// </summary>
        /// <param name="name">Строка, которая будет записана</param>
        /// <returns>
        /// <b>true</b>, если запись прошла успешно, <b>false</b> в противном случае
        /// </returns>
        public override bool WriteName(string name)
        {
            try
            {
                Writer.Write("\"{0}\": ", name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
