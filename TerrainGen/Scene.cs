using TerrainGen.HeightMap;

namespace TerrainGen
{
    /// <summary>
    /// Представляет основной объект отрисовки
    /// </summary>
    class Scene
    {
        /// <summary>
        /// Получает настройки сцены
        /// </summary>
        public Settings Settings { get; private set; }
        private readonly Camera _cam = new Camera();

        /// <summary>
        /// Загружает новую сцену с настройками по умолчанию
        /// </summary>
        public Scene()
        {
            LoadDefaultSettings();
        }

        /// <summary>
        /// Загружает новую сцену с указанными настройкам
        /// </summary>
        /// <remarks>
        /// <para>Если есть несколько объектов настройки, то не обязательно сначала сливать
        /// их в один объект можно вызывать </para>
        /// <code lang="C#"><![CDATA[new Settings(set1, set2, set3, ...);]]></code>
        /// <para>причем, если у нескольких объектов будут совпадающие имена настроек, то в
        /// итоге будет записано то, которое шло последним</para>
        /// </remarks>
        /// <param name="settings">Настройки сцены</param>
        public Scene(params Settings[] settings) : this()
        {
            Settings = Settings.MergeWith(settings);
        }

        /// <summary>
        /// Обновляет некоторые настройки
        /// </summary>
        /// <param name="settings">Настройки, которые нуждаются в обновлении</param>
        public void AddSettings(params Settings[] settings)
        {
            Settings = Settings.MergeWith(settings);
        }

        /// <summary>
        /// Получает объект камеры
        /// </summary>
        /// <remarks>
        /// Задать вторую и более камеру невозможно
        /// </remarks>
        public Camera Camera
        {
            get { return _cam; }
        }

        private void LoadDefaultSettings()
        {
            Settings = new Settings();
            Settings["seed"] = 1234;
            Settings["size"] = 513;
            Settings["roughness"] = (double)40;
            Settings["waterlevel"] = 50;
            Settings["zoom"] = 2;
            Settings["width"] = 792;
            Settings["height"] = 593;

            var colorTable = new ColorDescription[9];
            colorTable[0] = new ColorDescription
                                {Color = new[] {39f/255, 196f/255, 89f/255, 1f}, Comment = "Трава 1", Level = 50};
            colorTable[1] = new ColorDescription
                                {Color = new[] {14f/255, 153f/255, 58f/255, 1f}, Comment = "Трава 2", Level = 75};
            colorTable[2] = new ColorDescription
                                {Color = new[] {7f/255, 102f/255, 37f/255, 1f}, Comment = "Трава 3", Level = 100};
            colorTable[3] = new ColorDescription
                                {Color = new[] {66f/255, 9f/255, 9f/255, 1f}, Comment = "Земля 1", Level = 120};
            colorTable[4] = new ColorDescription
                                {Color = new[] {112f/255, 61f/255, 61f/255, 1f}, Comment = "Земля 2", Level = 140};
            colorTable[5] = new ColorDescription
                                {Color = new[] {120f/255, 92f/255, 92f/255, 1f}, Comment = "Земля 3", Level = 160};
            colorTable[6] = new ColorDescription
                                {Color = new[] {138f/255, 132f/255, 132f/255, 1f}, Comment = "Гора 1", Level = 180};
            colorTable[7] = new ColorDescription
                                {Color = new[] {99f/255, 96f/255, 96f/255, 1f}, Comment = "Гора 2", Level = 200};
            colorTable[8] = new ColorDescription {Color = new[] {1f, 1f, 1f, 1f}, Comment = "Снег", Level = 255};
            Settings["colors"] = colorTable;

            var specialVars = new Settings();
            specialVars["clear_color"] = new Color(.1f, .3f, .6f, .2f);
            specialVars["light_position"] = new float[] { 0, 1, 0, 0 };
            specialVars["watercolor"] = new Color(23f / 255, 135f / 255, 209f / 255);
            specialVars["fog_enabled"] = true;
            specialVars["fog_color"] = new Color(0.5f, 0.5f, 0.5f);
            specialVars["fog_density"] = 0.0009f;
            Settings["special_vars"] = specialVars;
        }

        /// <summary>
        /// Инициализирует сцену, подготавливая ее к отрисовке.
        /// </summary>
        /// <remarks>
        /// Этот метод необходимо один раз после создания объекта, но до вызова <see
        /// cref="M:TerrainGen.Scene.Render"/>
        /// </remarks>
        public void Init()
        {
            GlHelper.InitScene(Settings);
            var hMap = (new FactorialModel(Settings)).GetMap();
            var nMap = CommonHelper.GenerateNormalMap(hMap);
            SetInitialCameraPosition((int)Settings["zoom"], hMap);
            GlHelper.Prepare(hMap, nMap);
        }

        /// <summary>
        /// Отрисовывает сцену
        /// </summary>
        /// <remarks>
        /// Необходимо вызывать этот метод каждый раз, как требуется перерисовка сцены
        /// </remarks>
        public void Render()
        {
            Camera.Update();
            GlHelper.Draw(Camera);
        }

        private void SetInitialCameraPosition(int zoom, float[,] hMap)
        {
            float max = hMap[0, 0];
            for (int i = 0; i < hMap.GetLength(0); ++i)
                for (int j = 0; j < hMap.GetLength(1); ++j)
                    if (hMap[i, j] > max) max = hMap[i, j];

            Camera.PositionCamera(0, (max - 50) * zoom, 0, hMap.GetLength(0) / 2 * zoom, (max - 100) * zoom,
                                hMap.GetLength(0) / 2 * zoom, 0, 1, 0);
        }
    }
}
