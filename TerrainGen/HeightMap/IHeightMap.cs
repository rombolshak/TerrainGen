namespace TerrainGen.HeightMap
{
    /// <summary>
    /// Представляет карту высот
    /// </summary>
    interface IHeightMap
    {
        /// <summary>
        /// Возвращает карту высот
        /// </summary>
        /// <returns>
        /// Карта высот в виде двумерного массива, значения лежат в промежутке [0; 255]
        /// </returns>
        float[,] GetMap();
    }
}
