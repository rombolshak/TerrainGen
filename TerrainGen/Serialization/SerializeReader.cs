using System;

namespace TerrainGen.Serialization
{
    public abstract class SerializeReader : ISerializer
    {
        protected readonly dynamic Reader;

        /// <summary>
        /// Инициализирует новый объекта класса <see
        /// cref="T:TerrainGen.Serialization.SerializeReader"/>
        /// </summary>
        /// <remarks>
        /// На роль <b>Reader</b> подходят, к примеру, <see
        /// cref="T:System.IO.BinaryReader"/> или <see cref="T:System.IO.StreamReader"/>.
        /// Вообще же говоря, реализация собственно чтения зависит только от реализации
        /// класса десериализатора, поэтому строгих ограничений нет
        /// </remarks>
        /// <param name="reader">Любой объекта, реализующий методы чтения</param>
        protected SerializeReader(dynamic reader)
        {
            Reader = reader;
        }

        public abstract bool Open();
        public abstract bool Close();

        /// <summary>
        /// Обеспечивает вывод строки из объекта десериализатора
        /// </summary>
        /// <param name="str">Строка, значение в которую будет записано при десериализации</param>
        /// <returns>
        /// <para><b>true</b>, если удалось получить строку, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref string str);

        /// <summary>
        /// Обеспечивает вывод объекта типа Int32 из объекта десериализатора
        /// </summary>
        /// <param name="i">Число целочисленного типа, значение в которое будет
        /// записано при десериализации</param>
        /// <returns>
        /// <para><b>true</b>, если текущим элементом в десереализаторе является число или объекта, который можно привести к числу целочисленного типа, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref int i);

        /// <summary>
        /// Обеспечивает вывод объекта типа Double из объекта десериализатора
        /// </summary>
        /// <param name="d">Число с плавающей точкой двойной точности, значение в которое будет 
        /// записано при десериализации</param>
        /// <returns>
        /// <para><b>true</b>, если текущим элементом в десереализаторе является число или объекта, который можно привести к числу типа Double, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref double d);

        /// <summary>
        /// Обеспечивает вывод объекта типа Single из объекта десериализатора
        /// </summary>
        /// <param name="f">Число с плавающей точкой, значение в которое будет
        /// записано при десериализации</param>
        /// <returns>
        /// <para><b>true</b>, если текущим элементом в десереализаторе является число или объекта, который можно привести к числу типа Single, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref float f);

        /// <summary>
        /// Обеспечивает вывод булева значения из объекта десериализатора
        /// </summary>
        /// <param name="b">Переменная типа bool, значение которой будет
        /// записано при десериализации</param>
        /// <returns>
        /// <para><b>true</b>, если текущим элементом в десереализаторе является булево значение, либо строка, значение которой можно интепретировать как булево значение, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref bool b);

        /// <summary>
        /// Обеспечивает вывод специальных маркеров из объекта десериализатора
        /// </summary>
        /// <param name="ch">Маркер, являющийся элементом перечесления <see cref="SpecialChars"/>, значение в которое будет 
        /// записано при десериализации</param>
        /// <returns>
        /// <para><b>true</b>, если текущим элементом в десереализаторе является указанный (а не любой) маркер, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        public bool InOut(SpecialChars ch)
        {
            return InOut(ch, true);
        }

        /// <summary>
        /// Обеспечивает вывод специальных маркеров из объекта
        /// десериализатора
        /// </summary>
        /// <param name="ch">Маркер, являющийся элементом перечесления <see
        /// cref="SpecialChars"/>, значение в которое будет 
        /// записано при десериализации</param>
        /// <param name="delete">Если <b>true </b>(по умолчанию), то объекта будет удален
        /// после чтения. Иначе же при следующем обращении на чтение к экземпляру данного
        /// класса будет выдан тот же самый объекта</param>
        /// <returns>
        /// <para><b>true</b>, если текущим элементом в десереализаторе
        /// является указанный (а не любой) маркер, и <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)">Boolean@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)">Double@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)">Single@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)">String@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)">Int32@)</seealso>
        public abstract bool InOut(SpecialChars ch, bool delete = true);
        /// <summary>
        /// Возвращает текущий специальный символ
        /// </summary>
        public abstract SpecialChars TestCurrentSpecialChar();
    }
}