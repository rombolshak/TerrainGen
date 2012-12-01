namespace TerrainGen.Serialization
{
    /// <summary>
    /// Представляет специальные символы, используемые для сериализации
    /// </summary>
    public enum SpecialChars
    {
        /// <summary>
        /// Пустой символ
        /// </summary>
        Null,
        /// <summary>
        /// Разделитель записей
        /// </summary>
        Delimiter,
        /// <summary>
        /// Маркер начала массива
        /// </summary>
        ArrayStart,
        /// <summary>
        /// Маркер конца массива
        /// </summary>
        ArrayEnd,
        /// <summary>
        /// Маркер начала объекта
        /// </summary>
        ObjectStart,
        /// <summary>
        /// Маркер конца объекта
        /// </summary>
        ObjectEnd
    }

    /// <summary>
    /// Предоставляет основные методы для сериализации
    /// </summary>
    /// <remarks>
    /// <para><b>Запрещается</b> наследование этого интерфейса напрямую. Следует
    /// унаследовать <see cref="T:TerrainGen.Serialization.SerializeWriter"/> для
    /// сериализаторов и <see cref="T:TerrainGen.Serialization.SerializeReader"/> для
    /// десериализаторов.</para>
    /// <para>В случае явной реализации этого интерфейса следует исправить метод <see
    /// cref="M:TerrainGen.Settings.Serialize(TerrainGen.Serialization.ISerializer)"/> так,
    /// чтобы он корректно принимал экземпляры классов, реализующих интерфейс</para>
    /// </remarks>
    /// <seealso cref="N:TerrainGen.Serialization"/>
    /// <seealso cref="T:TerrainGen.Serialization.SerializeWriter"/>
    /// <seealso cref="T:TerrainGen.Serialization.SerializeReader"/>
    /// <seealso
    /// cref="M:TerrainGen.Settings.Serialize(TerrainGen.Serialization.ISerializer)"/>
    public interface ISerializer
    {
        /// <summary>
        /// Открывает файл, в соответствии с предназначением класса: для десериализации на
        /// чтение, для сериализации -- на запись
        /// </summary>
        /// <returns>
        /// <para><b>true</b>, если открытие прошло успешно;</para>
        /// <para><b>false</b> в противном случае</para>
        /// </returns>
        bool Open();
        /// <summary>
        /// Обеспечивает корректное закрытие файла
        /// </summary>
        /// <remarks>
        /// Метод всегда завершается успешно, но для единообразия сигнатур возвращает bool
        /// </remarks>
        /// <returns>
        /// true
        /// </returns>
        bool Close();
        /// <summary>
        /// Обеспечивает ввод (вывод) строки в (из) объект (де)сериализатора
        /// </summary>
        /// <remarks>
        /// Для этой перегрузки всегда возвращается <b>true</b>.
        /// Возможно возвращение <b>false</b> в случае возникновения исключений.
        /// </remarks>
        /// <param name="str">Строка, значение которой будет считано при сериализации, либо
        /// записано при десериализации</param>
        /// <returns>
        /// <para>При сериализации всегда <b>true</b></para>
        /// <para>При десериализации <b>true</b>, если удалось получить строку, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        bool InOut(ref string str);
        /// <summary>
        /// Обеспечивает ввод (вывод) объекта типа Int32 в (из) объект(а) (де)сериализатора
        /// </summary>
        /// <param name="i">Число целочисленного типа, значение которого будет считано при сериализации, либо
        /// записано при десериализации</param>
        /// <returns>
        /// <para>При сериализации всегда <b>true</b></para>
        /// <para>При десериализации <b>true</b>, если текущим элементом в десереализаторе является число или объект, который можно привести к числу целочисленного типа, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        bool InOut(ref int i);
        /// <summary>
        /// Обеспечивает ввод (вывод) объекта типа Double в (из) объект(а) (де)сериализатора
        /// </summary>
        /// <param name="d">Число с плавающей точкой двойной точности, значение которого будет считано при сериализации, либо
        /// записано при десериализации</param>
        /// <returns>
        /// <para>При сериализации всегда <b>true</b></para>
        /// <para>При десериализации <b>true</b>, если текущим элементом в десереализаторе является число или объект, который можно привести к числу типа Double, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        bool InOut(ref double d);
        /// <summary>
        /// Обеспечивает ввод (вывод) объекта типа Single в (из) объект(а) (де)сериализатора
        /// </summary>
        /// <param name="f">Число с плавающей точкой, значение которого будет считано при сериализации, либо
        /// записано при десериализации</param>
        /// <returns>
        /// <para>При сериализации всегда <b>true</b></para>
        /// <para>При десериализации <b>true</b>, если текущим элементом в десереализаторе является число или объект, который можно привести к числу типа Single, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        bool InOut(ref float f);
        /// <summary>
        /// Обеспечивает ввод (вывод) булева значения в (из) объект(а) (де)сериализатора
        /// </summary>
        /// <param name="b">Переменная типа bool, значение которой будет считано при сериализации, либо
        /// записано при десериализации</param>
        /// <returns>
        /// <para>При сериализации всегда <b>true</b></para>
        /// <para>При десериализации <b>true</b>, если текущим элементом в десереализаторе является булево значение, либо строка, значение которой можно интепретировать как булево значение, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        bool InOut(ref bool b);
        /// <summary>
        /// Обеспечивает ввод (вывод) специальных маркеров в (из) объект(а) (де)сериализатора
        /// </summary>
        /// <param name="ch">Маркер, являющийся элементом перечесления <see cref="SpecialChars"/>, значение которого будет считано при сериализации, либо
        /// записано при десериализации</param>
        /// <returns>
        /// <para>При сериализации всегда <b>true</b></para>
        /// <para>При десериализации <b>true</b>, если текущим элементом в десереализаторе является указанный (а не любой) маркер, и
        /// <b>false</b> в противном случае</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        bool InOut(SpecialChars ch);
    }
}
