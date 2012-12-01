using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace TerrainGen.Serialization
{
    /// <summary>
    /// Предоставляет методы для сохранения файлов настроек в бинарном виде
    /// </summary>
    /// <seealso cref="T:TerrainGen.Serialization.BinaryDeserializer"/>
    /// <seealso cref="T:TerrainGen.Serialization.JsonSerializer"/>
    class BinarySerializer : SerializeWriter
    {

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="T:TerrainGen.Serialization.BinarySerializer"/> class.
        /// </summary>
        /// <param name="filename">Путь к файлу, в который будет производиться сохранение
        /// данных</param>
        /// <exception cref="IOException">Файл используется другим процессом</exception>
        public BinarySerializer(string filename)
            : base(new BinaryWriter(new FileStream(filename, FileMode.Create), Encoding.UTF8))
        {
        }

        private static string EncodeTo64(string toEncode)
        {
            var toEncodeAsBytes
                  = Encoding.UTF8.GetBytes(toEncode);
            var returnValue
                  = Convert.ToBase64String(toEncodeAsBytes);
            return Encrypt(returnValue, "I want to get paid for the work I'm doing.");
        }
        private static string Encrypt(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
                result.Append((char)(text[c] ^ (uint)key[c % key.Length]));

            return result.ToString();
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
                Writer.Write(EncodeTo64(str));
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
                Writer.Write(EncodeTo64(i.ToString(CultureInfo.InvariantCulture)));
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
                Writer.Write(EncodeTo64(d.ToString(CultureInfo.InvariantCulture)));
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
                Writer.Write(EncodeTo64(f.ToString(CultureInfo.InvariantCulture)));
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
                Writer.Write(EncodeTo64(b.ToString(CultureInfo.InvariantCulture)));
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
                    Writer.Write(EncodeTo64(","));
                    break;
                case SpecialChars.ArrayStart:
                    Writer.Write(EncodeTo64("["));
                    break;
                case SpecialChars.ArrayEnd:
                    Writer.Write(EncodeTo64("]"));
                    break;
                case SpecialChars.ObjectStart:
                    Writer.Write(EncodeTo64("{"));
                    break;
                case SpecialChars.ObjectEnd:
                    Writer.Write(EncodeTo64("}"));
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
                Writer.Write(EncodeTo64(name));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
