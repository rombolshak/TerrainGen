using System;
using System.Globalization;
using System.Windows.Forms;
using TerrainGen.Serialization;

namespace TerrainGen
{
    /// <summary>
    /// Главная форма приложения
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly Scene _scene;
        private readonly Settings _settings;

        public MainForm()
        {
            InitializeComponent();
            _scene = new Scene();
            _settings = new Settings();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            glControl.InitializeContexts();
            GlHelper.InitGL(glControl.Width, glControl.Height);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            MouseEvents();
            _scene.Render();
            glControl.Invalidate();
            fpsLabel.Text = "FPS: " + GlHelper.Fps.ToString(CultureInfo.InvariantCulture);
        }

        private void GenerateMapButtonClick(object sender, EventArgs e)
        {
            if (mapSizeBox.Text == "")
            {
                MessageBox.Show("Не задан размер карты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int size;
            try
            {
                size = Int32.Parse(mapSizeBox.Text.Split('x')[0]);
                if (size != 33 && size != 65 && size != 129 && size != 257 && size != 513 && size != 1025)
                {
                    MessageBox.Show("Некорректный размер карты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный формат размера карты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            timer1.Stop();

            _settings["seed"] = (int) randomSeed.Value;
            _settings["size"] = size;
            _settings["width"] = size;
            _settings["height"] = size;
            _settings["roughness"] = (double) roughness.Value;
            _settings["waterlevel"] = (int) waterlevel.Value;
            _settings["zoom"] = (int) zoom.Value;
            _settings["width"] = glControl.Width;
            _settings["height"] = glControl.Height;
            
            _scene.AddSettings(_settings);
            try
            {
                _scene.Init();
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Управление движением

        #region Управление мышью

        private bool _mouseRotate;
        private int _mouseXcoord;
        private int _mouseXcoordVar;
        private int _mouseYcoord;
        private int _mouseYcoordVar;

        private void GLControlMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _mouseRotate = true; //Если нажата левая кнопка мыши

            _mouseXcoord = e.X;
            _mouseYcoord = e.Y;
        }

        private void GLControlMouseUp(object sender, MouseEventArgs e)
        {
            _mouseRotate = false;
        }

        private void GLControlMouseMove(object sender, MouseEventArgs e)
        {
            _mouseYcoordVar = e.Y;
            _mouseXcoordVar = e.X;
        }

        private void MouseEvents()
        {
            if (_mouseRotate) //Если нажата левая кнопка мыши
            {
                glControl.Cursor = Cursors.SizeAll;

                _scene.Camera.RotateView((float) (_mouseXcoordVar - _mouseXcoord)/100);
                _scene.Camera.UpDown(_mouseYcoordVar - _mouseYcoord);

                _mouseXcoord = _mouseXcoordVar;
                _mouseYcoord = _mouseYcoordVar;
            }
            else
                glControl.Cursor = Cursors.Default;
        }

        #endregion

        private void GLControlKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _scene.Camera.MoveCamera(10f);
                    break;
                case Keys.S:
                    _scene.Camera.MoveCamera(-10f);
                    break;
                case Keys.A:
                    _scene.Camera.Strafe(-10f);
                    break;
                case Keys.D:
                    _scene.Camera.Strafe(10f);
                    break;
                case Keys.PageUp:
                    _scene.Camera.MoveUpDown(10f);
                    break;
                case Keys.PageDown:
                    _scene.Camera.MoveUpDown(-10f);
                    break;
            }
        }

        #endregion

        private void AdvancedOptionsButtonClick(object sender, EventArgs e)
        {
            var aof = new AdvancedOptionsForm(_scene.Settings);
            aof.ShowDialog(this);
        }

        internal void RegisterAdvancedSettings(Settings specialVars, Settings colors)
        {
            var specialVarsWrapper = new Settings();
            specialVarsWrapper["special_vars"] = specialVars;
            _scene.AddSettings(specialVarsWrapper, colors);
        }

        private void ExportSettings(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
                                     {DefaultExt = ".json", Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"};
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var s = new JsonSerializer(sfd.FileName);
            if (!s.Open()) { MessageBox.Show("Не удалось открыть файл"); return;}
            if (!_scene.Settings.Serialize(s))
            {
                MessageBox.Show("Не удалось сохранить настройки");
            }
            s.Close();
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { DefaultExt = ".tgc", Filter = "Файлы настроек (*.tgc)|*.tgc|Все файлы (*.*)|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var s = new BinarySerializer(sfd.FileName);
            if (!s.Open()) { MessageBox.Show("Не удалось открыть файл"); return; }
            if (!_scene.Settings.Serialize(s))
            {
                MessageBox.Show("Не удалось сохранить настройки");
            }
            s.Close();
        }

        private void ImportSettings(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { DefaultExt = ".json", Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var s = new JsonDeserializer(ofd.FileName);
            if (!s.Open()) { MessageBox.Show("Не удалось открыть файл"); return; }
            if (!_scene.Settings.Serialize(s))
            {
                MessageBox.Show("Не удалось загрузить настройки");
                s.Close();
                return;
            }
            s.Close();
            RefreshSettings();
        }

        private void LoadSettings(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { DefaultExt = ".tgc", Filter = "Файлы настроек (*.tgc)|*.tgc|Все файлы (*.*)|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var s = new BinaryDeserializer(ofd.FileName);
            if (!s.Open()) { MessageBox.Show("Не удалось открыть файл"); return; }
            if (!_scene.Settings.Serialize(s))
            {
                MessageBox.Show("Не удалось загрузить настройки");
                s.Close();
                return;
            }
            s.Close();
            RefreshSettings();
        }

        private void RefreshSettings()
        {
            randomSeed.Value = _scene.Settings["seed"];
            mapSizeBox.Text = String.Format("{0}x{0}", _scene.Settings["size"]);
            roughness.Value = (decimal) _scene.Settings["roughness"];
            waterlevel.Value = _scene.Settings["waterlevel"];
            zoom.Value = _scene.Settings["zoom"];
        }
    }
}