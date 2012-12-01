using System;
using System.Globalization;
using System.Windows.Forms;

namespace TerrainGen
{
    /// <summary>
    /// Форма расширенных настроек
    /// </summary>
    public partial class AdvancedOptionsForm : Form
    {
        private readonly Settings _settings;
        public AdvancedOptionsForm(Settings settings = null)
        {
            InitializeComponent();
            _settings = settings ?? new Settings();
        }

        private void Button2Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AdvancedOptionsFormLoad(object sender, EventArgs e)
        {
            densityNum.Enabled = fogCheckBox.Checked = (bool) ((Settings) _settings["special_vars"])["fog_enabled"];
            densityNum.Value = (decimal)(float) ((Settings) _settings["special_vars"])["fog_density"];
            var lightPos = (float[]) ((Settings) _settings["special_vars"])["light_position"];
            lightPosX.Text = lightPos[0].ToString(CultureInfo.InvariantCulture);
            lightPosY.Text = lightPos[1].ToString(CultureInfo.InvariantCulture);
            lightPosZ.Text = lightPos[2].ToString(CultureInfo.InvariantCulture);
            lightPosW.Text = lightPos[3].ToString(CultureInfo.InvariantCulture);

            var c = (Color) ((Settings) _settings["special_vars"])["clear_color"];
            dataGridView1.Rows.Add("", "Цвет фона", c.R.ToString(CultureInfo.InvariantCulture), c.G.ToString(CultureInfo.InvariantCulture), c.B.ToString(CultureInfo.InvariantCulture), c.A.ToString(CultureInfo.InvariantCulture), 0.ToString(CultureInfo.InvariantCulture));
            c = (Color)((Settings)_settings["special_vars"])["fog_color"];
            dataGridView1.Rows.Add("", "Цвет тумана", c.R.ToString(CultureInfo.InvariantCulture), c.G.ToString(CultureInfo.InvariantCulture), c.B.ToString(CultureInfo.InvariantCulture), c.A.ToString(CultureInfo.InvariantCulture), 0.ToString(CultureInfo.InvariantCulture));
            c = (Color) _settings["special_vars"]["watercolor"];
            dataGridView1.Rows.Add("", "Цвет воды", c.R.ToString(CultureInfo.InvariantCulture), c.G.ToString(CultureInfo.InvariantCulture), c.B.ToString(CultureInfo.InvariantCulture), c.A.ToString(CultureInfo.InvariantCulture), 0.ToString(CultureInfo.InvariantCulture));

            dataGridView1.Rows[0].Cells[1].ReadOnly = dataGridView1.Rows[1].Cells[1].ReadOnly = dataGridView1.Rows[3].Cells[1].ReadOnly = true;

            FillColorTable((ColorDescription[])_settings["colors"]);
            SetColorExamples();
        }

        private void FillColorTable(ColorDescription[] colorDescription)
        {
            foreach (var description in colorDescription)
                dataGridView1.Rows.Add("", description.Comment, description.Color.R.ToString(CultureInfo.InvariantCulture),
                                       description.Color.G.ToString(CultureInfo.InvariantCulture),
                                       description.Color.B.ToString(CultureInfo.InvariantCulture), description.Color.A.ToString(CultureInfo.InvariantCulture),
                                       description.Level.ToString(CultureInfo.InvariantCulture));
        }

        private void SetColorExamples()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                dataGridView1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(
                    Int32.Parse((string) dataGridView1.Rows[i].Cells[5].Value),
                    Int32.Parse((string) dataGridView1.Rows[i].Cells[2].Value),
                    Int32.Parse((string) dataGridView1.Rows[i].Cells[3].Value),
                    Int32.Parse((string) dataGridView1.Rows[i].Cells[4].Value)
                    );
        }

        private void FogCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            densityNum.Enabled = fogCheckBox.Checked;
        }

        private void TabControl1SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Visible = tabControl1.SelectedIndex == 1;
        }

        private void Button3Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog {AllowFullOpen = true, AnyColor = true};
            cd.ShowDialog();
            var index = dataGridView1.Rows.Add("", "Comment here", cd.Color.R.ToString(CultureInfo.InvariantCulture), cd.Color.G.ToString(CultureInfo.InvariantCulture),
                                       cd.Color.B.ToString(CultureInfo.InvariantCulture), cd.Color.A.ToString(CultureInfo.InvariantCulture), 0.ToString(CultureInfo.InvariantCulture));
            dataGridView1.Rows[index].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(cd.Color.A, cd.Color.R,
                                                                                               cd.Color.G, cd.Color.B);
        }

        private void DataGridView1CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var cd = new ColorDialog { AllowFullOpen = true, AnyColor = true };
            cd.ShowDialog();
            dataGridView1.Rows[e.RowIndex].Cells[2].Value = cd.Color.R.ToString(CultureInfo.InvariantCulture);
            dataGridView1.Rows[e.RowIndex].Cells[3].Value = cd.Color.G.ToString(CultureInfo.InvariantCulture);
            dataGridView1.Rows[e.RowIndex].Cells[4].Value = cd.Color.B.ToString(CultureInfo.InvariantCulture);
            dataGridView1.Rows[e.RowIndex].Cells[5].Value = cd.Color.A.ToString(CultureInfo.InvariantCulture);
            dataGridView1.Rows[e.RowIndex].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(cd.Color.A, cd.Color.R,
                                                                                               cd.Color.G, cd.Color.B);
        }

        private void DataGridView1UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Index < 3)
                e.Cancel = true;
        }

        private void DataGridView1CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= 1 || e.ColumnIndex >= 6) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;
            try
            {
                var val = Int32.Parse((string) dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (val < 0 || val > 255)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText =
                        "Значение должно быть целым числом от 0 до 255 включительно";
                }

                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                    int a = Int32.Parse((string) dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    int r = Int32.Parse((string)dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    int g = Int32.Parse((string)dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    int b = Int32.Parse((string) dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    dataGridView1.Rows[e.RowIndex].Cells[0].Style.BackColor =
                        System.Drawing.Color.FromArgb(a, r, g, b);
                }
            }
            catch (Exception)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText =
                    "Значение должно быть целым числом от 0 до 255 включительно";
            }
            dataGridView1.UpdateCellErrorText(e.ColumnIndex, e.RowIndex);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            for (var i = 0; i < dataGridView1.Rows.Count - 1; ++i)
                for (var j = 2; j < 6; ++j)
                    if (dataGridView1.Rows[i].Cells[j].ErrorText != "")
                        return;
            var specialVars = new Settings();
            specialVars["clear_color"] = (Color) new[]
                                                {
                                                    float.Parse((string) dataGridView1.Rows[0].Cells[2].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[0].Cells[3].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[0].Cells[4].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[0].Cells[5].Value) / 255
                                                };
            specialVars["fog_color"] = (Color)new[]
                                                {
                                                    float.Parse((string) dataGridView1.Rows[1].Cells[2].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[1].Cells[3].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[1].Cells[4].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[1].Cells[5].Value) / 255
                                                };
            specialVars["watercolor"] = (Color)new[]
                                                {
                                                    float.Parse((string) dataGridView1.Rows[2].Cells[2].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[2].Cells[3].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[2].Cells[4].Value) / 255,
                                                    float.Parse((string) dataGridView1.Rows[2].Cells[5].Value) / 255
                                                };
            specialVars["fog_enabled"] = fogCheckBox.Checked;
            specialVars["fog_density"] = (float)densityNum.Value;
            specialVars["light_position"] = new[]
                                           {
                                               float.Parse(lightPosX.Text),
                                               float.Parse(lightPosY.Text),
                                               float.Parse(lightPosZ.Text),
                                               float.Parse(lightPosW.Text)
                                           };
            var colors = new ColorDescription[dataGridView1.Rows.Count - 4];
            for (var i = 3; i < dataGridView1.Rows.Count - 1; ++i)
                colors[i - 3] = new ColorDescription
                                    {
                                        Color = new Color(
                                            Int32.Parse((string) dataGridView1.Rows[i].Cells[2].Value),
                                            Int32.Parse((string) dataGridView1.Rows[i].Cells[3].Value),
                                            Int32.Parse((string) dataGridView1.Rows[i].Cells[4].Value),
                                            Int32.Parse((string) dataGridView1.Rows[i].Cells[5].Value)
                                            ),
                                        Comment = (string) dataGridView1.Rows[i].Cells[1].Value,
                                        Level = Int32.Parse((string) dataGridView1.Rows[i].Cells[6].Value)
                                    };
            var colorTable = new Settings();
            colorTable["colors"] = colors;

            ((MainForm)Owner).RegisterAdvancedSettings(specialVars, colorTable);
            Close();
        }
    }
}
