namespace TerrainGen
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.glControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.advancedOptionsButton = new System.Windows.Forms.Button();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.zoom = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.generateMapButton = new System.Windows.Forms.Button();
            this.waterlevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.roughness = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.mapSizeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.randomSeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waterlevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roughness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomSeed)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.AccumBits = ((byte)(0));
            this.glControl.AutoCheckErrors = false;
            this.glControl.AutoFinish = false;
            this.glControl.AutoMakeCurrent = true;
            this.glControl.AutoSwapBuffers = true;
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.ColorBits = ((byte)(32));
            this.glControl.DepthBits = ((byte)(16));
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(792, 573);
            this.glControl.StencilBits = ((byte)(0));
            this.glControl.TabIndex = 1;
            this.glControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GLControlKeyDown);
            this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLControlMouseDown);
            this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLControlMouseMove);
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLControlMouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.TimerTick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadButton);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.importButton);
            this.groupBox1.Controls.Add(this.exportButton);
            this.groupBox1.Controls.Add(this.advancedOptionsButton);
            this.groupBox1.Controls.Add(this.fpsLabel);
            this.groupBox1.Controls.Add(this.zoom);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.generateMapButton);
            this.groupBox1.Controls.Add(this.waterlevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.roughness);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.mapSizeBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.randomSeed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(804, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 549);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(10, 343);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(184, 23);
            this.importButton.TabIndex = 14;
            this.importButton.Text = "Импорт настроек";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.ImportSettings);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(10, 314);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(184, 23);
            this.exportButton.TabIndex = 13;
            this.exportButton.Text = "Экспорт настроек";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportSettings);
            // 
            // advancedOptionsButton
            // 
            this.advancedOptionsButton.Location = new System.Drawing.Point(10, 223);
            this.advancedOptionsButton.Name = "advancedOptionsButton";
            this.advancedOptionsButton.Size = new System.Drawing.Size(184, 23);
            this.advancedOptionsButton.TabIndex = 12;
            this.advancedOptionsButton.Text = "Расширенные настройки";
            this.advancedOptionsButton.UseVisualStyleBackColor = true;
            this.advancedOptionsButton.Click += new System.EventHandler(this.AdvancedOptionsButtonClick);
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Location = new System.Drawing.Point(6, 533);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(39, 13);
            this.fpsLabel.TabIndex = 11;
            this.fpsLabel.Text = "FPS: 0";
            // 
            // zoom
            // 
            this.zoom.Location = new System.Drawing.Point(10, 197);
            this.zoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(184, 20);
            this.zoom.TabIndex = 10;
            this.zoom.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Коэффициент масштабирования:";
            // 
            // generateMapButton
            // 
            this.generateMapButton.Location = new System.Drawing.Point(10, 252);
            this.generateMapButton.Name = "generateMapButton";
            this.generateMapButton.Size = new System.Drawing.Size(184, 23);
            this.generateMapButton.TabIndex = 8;
            this.generateMapButton.Text = "Генерировать";
            this.generateMapButton.UseVisualStyleBackColor = true;
            this.generateMapButton.Click += new System.EventHandler(this.GenerateMapButtonClick);
            // 
            // waterlevel
            // 
            this.waterlevel.Location = new System.Drawing.Point(10, 158);
            this.waterlevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.waterlevel.Name = "waterlevel";
            this.waterlevel.Size = new System.Drawing.Size(184, 20);
            this.waterlevel.TabIndex = 7;
            this.waterlevel.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Уровень воды:";
            // 
            // roughness
            // 
            this.roughness.DecimalPlaces = 2;
            this.roughness.Location = new System.Drawing.Point(10, 119);
            this.roughness.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.roughness.Name = "roughness";
            this.roughness.Size = new System.Drawing.Size(184, 20);
            this.roughness.TabIndex = 5;
            this.roughness.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Шероховатость (roughness):";
            // 
            // mapSizeBox
            // 
            this.mapSizeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.mapSizeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.mapSizeBox.DisplayMember = "129x129";
            this.mapSizeBox.FormattingEnabled = true;
            this.mapSizeBox.Items.AddRange(new object[] {
            "33x33",
            "65x65",
            "129x129",
            "257x257",
            "513x513",
            "1025x1025"});
            this.mapSizeBox.Location = new System.Drawing.Point(10, 79);
            this.mapSizeBox.Name = "mapSizeBox";
            this.mapSizeBox.Size = new System.Drawing.Size(184, 21);
            this.mapSizeBox.TabIndex = 3;
            this.mapSizeBox.Text = "129x129";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Размер карты:";
            // 
            // randomSeed
            // 
            this.randomSeed.Location = new System.Drawing.Point(10, 36);
            this.randomSeed.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.randomSeed.Name = "randomSeed";
            this.randomSeed.Size = new System.Drawing.Size(184, 20);
            this.randomSeed.TabIndex = 1;
            this.randomSeed.Value = new decimal(new int[] {
            1234,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Инициализатор рандома:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(10, 392);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(184, 23);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Сохранить настройки";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveSettings);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(10, 421);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(184, 23);
            this.loadButton.TabIndex = 16;
            this.loadButton.Text = "Загрузить настройки";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadSettings);
            // 
            // Form1
            // 
            this.AcceptButton = this.generateMapButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 573);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.glControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GLControlKeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waterlevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roughness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomSeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl glControl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown randomSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox mapSizeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown waterlevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown roughness;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button generateMapButton;
        private System.Windows.Forms.NumericUpDown zoom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.Button advancedOptionsButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
    }
}

