using System;

namespace TerrainGen.Serialization
{
    public abstract class SerializeWriter : ISerializer
    {
        protected readonly dynamic Writer;// StreamWriter или BinaryWriter. Обязан реализовывать метод Write()

        /// <summary>
        /// Инициализирует новый объект сериализатора <see
        /// cref="T:TerrainGen.Serialization.SerializeWriter"/>
        /// </summary>
        /// <param name="writer">Объект любого типа, в котором реализованы методы Write() с
        /// перегрузками для основных типов данных и Close()</param>
        protected SerializeWriter(dynamic writer)
        {
            Writer = writer;
        }

        public abstract bool Open();
        public abstract bool Close();
        public abstract bool InOut(ref string str);

        /// <summary>
        /// Обеспечивает ввод объекта типа Int32 в  объект сериализатора
        /// </summary>
        /// <param name="i">Число целочисленного типа, значение которого будет считано при сериализации</param>
        /// <returns>
        /// true
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref int i);

        /// <summary>
        /// Обеспечивает ввод объекта типа Double в объект сериализатора
        /// </summary>
        /// <param name="d">Число с плавающей точкой двойной точности, значение которого будет считано при сериализации</param>
        /// <returns>
        /// true
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref double d);

        /// <summary>
        /// Обеспечивает ввод объекта типа Single в  объект сериализатора
        /// </summary>
        /// <param name="f">Число с плавающей точкой, значение которого будет считано при сериализации
        /// </param>
        /// <returns>
        /// true
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref float f);

        /// <summary>
        /// Обеспечивает ввод булева значения в  объект сериализатора
        /// </summary>
        /// <param name="b">Переменная типа bool, значение которой будет считано при сериализации
        /// </param>
        /// <returns>
        /// true
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref bool b);

        /// <summary>
        /// Обеспечивает ввод специальных маркеров в  объект сериализатора
        /// </summary>
        /// <param name="ch">Маркер, являющийся элементом перечесления <see cref="SpecialChars"/>, значение которого будет считано при сериализации</param>
        /// <returns>
        /// true
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        public abstract bool InOut(SpecialChars ch);
        /// <summary>
        /// Записывает указанную строку в сериализатор
        /// </summary>
        /// <param name="name">Строка, которая будет записана</param>
        public abstract bool WriteName(string name);
    }
}