namespace TerrainGen
{
    partial class AdvancedOptionsForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalOptions = new System.Windows.Forms.TabPage();
            this.colorOptions = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.fogCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.densityNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lightPosX = new System.Windows.Forms.TextBox();
            this.lightPosY = new System.Windows.Forms.TextBox();
            this.lightPosZ = new System.Windows.Forms.TextBox();
            this.lightPosW = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ColorExample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Red = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Green = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Blue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alpha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.generalOptions.SuspendLayout();
            this.colorOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.densityNum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColorExample,
            this.Comment,
            this.Red,
            this.Green,
            this.Blue,
            this.Alpha,
            this.Level});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(404, 193);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1CellDoubleClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1CellLeave);
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataGridView1UserDeletingRow);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.generalOptions);
            this.tabControl1.Controls.Add(this.colorOptions);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(418, 225);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1SelectedIndexChanged);
            // 
            // generalOptions
            // 
            this.generalOptions.Controls.Add(this.groupBox2);
            this.generalOptions.Controls.Add(this.groupBox1);
            this.generalOptions.Location = new System.Drawing.Point(4, 22);
            this.generalOptions.Name = "generalOptions";
            this.generalOptions.Padding = new System.Windows.Forms.Padding(3);
            this.generalOptions.Size = new System.Drawing.Size(434, 199);
            this.generalOptions.TabIndex = 0;
            this.generalOptions.Text = "Общие";
            this.generalOptions.UseVisualStyleBackColor = true;
            // 
            // colorOptions
            // 
            this.colorOptions.Controls.Add(this.dataGridView1);
            this.colorOptions.Location = new System.Drawing.Point(4, 22);
            this.colorOptions.Name = "colorOptions";
            this.colorOptions.Padding = new System.Windows.Forms.Padding(3);
            this.colorOptions.Size = new System.Drawing.Size(410, 199);
            this.colorOptions.TabIndex = 1;
            this.colorOptions.Text = "Настройки цветов";
            this.colorOptions.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "ОК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(355, 245);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // fogCheckBox
            // 
            this.fogCheckBox.AutoSize = true;
            this.fogCheckBox.Checked = true;
            this.fogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fogCheckBox.Location = new System.Drawing.Point(6, 19);
            this.fogCheckBox.Name = "fogCheckBox";
            this.fogCheckBox.Size = new System.Drawing.Size(108, 17);
            this.fogCheckBox.TabIndex = 0;
            this.fogCheckBox.Text = "Включить туман";
            this.fogCheckBox.UseVisualStyleBackColor = true;
            this.fogCheckBox.CheckedChanged += new System.EventHandler(this.FogCheckBoxCheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Густота тумана:";
            // 
            // densityNum
            // 
            this.densityNum.DecimalPlaces = 4;
            this.densityNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.densityNum.Location = new System.Drawing.Point(100, 41);
            this.densityNum.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.densityNum.Name = "densityNum";
            this.densityNum.Size = new System.Drawing.Size(59, 20);
            this.densityNum.TabIndex = 2;
            this.densityNum.Value = new decimal(new int[] {
            9,
            0,
            0,
            262144});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Позиция источника света:";
            // 
            // lightPosX
            // 
            this.lightPosX.Location = new System.Drawing.Point(9, 33);
            this.lightPosX.MaxLength = 3;
            this.lightPosX.Name = "lightPosX";
            this.lightPosX.Size = new System.Drawing.Size(25, 20);
            this.lightPosX.TabIndex = 4;
            // 
            // lightPosY
            // 
            this.lightPosY.Location = new System.Drawing.Point(40, 33);
            this.lightPosY.MaxLength = 3;
            this.lightPosY.Name = "lightPosY";
            this.lightPosY.Size = new System.Drawing.Size(25, 20);
            this.lightPosY.TabIndex = 5;
            // 
            // lightPosZ
            // 
            this.lightPosZ.Location = new System.Drawing.Point(72, 33);
            this.lightPosZ.MaxLength = 3;
            this.lightPosZ.Name = "lightPosZ";
            this.lightPosZ.Size = new System.Drawing.Size(25, 20);
            this.lightPosZ.TabIndex = 6;
            // 
            // lightPosW
            // 
            this.lightPosW.Location = new System.Drawing.Point(122, 33);
            this.lightPosW.MaxLength = 3;
            this.lightPosW.Name = "lightPosW";
            this.lightPosW.Size = new System.Drawing.Size(25, 20);
            this.lightPosW.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fogCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.densityNum);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 70);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lightPosX);
            this.groupBox2.Controls.Add(this.lightPosW);
            this.groupBox2.Controls.Add(this.lightPosY);
            this.groupBox2.Controls.Add(this.lightPosZ);
            this.groupBox2.Location = new System.Drawing.Point(266, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 70);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // ColorExample
            // 
            this.ColorExample.Frozen = true;
            this.ColorExample.HeaderText = "";
            this.ColorExample.Name = "ColorExample";
            this.ColorExample.ReadOnly = true;
            this.ColorExample.Width = 21;
            // 
            // Comment
            // 
            this.Comment.Frozen = true;
            this.Comment.HeaderText = "Комментарий";
            this.Comment.Name = "Comment";
            this.Comment.Width = 102;
            // 
            // Red
            // 
            this.Red.HeaderText = "Red";
            this.Red.Name = "Red";
            this.Red.Width = 52;
            // 
            // Green
            // 
            this.Green.HeaderText = "Green";
            this.Green.Name = "Green";
            this.Green.Width = 61;
            // 
            // Blue
            // 
            this.Blue.HeaderText = "Blue";
            this.Blue.Name = "Blue";
            this.Blue.Width = 53;
            // 
            // Alpha
            // 
            this.Alpha.HeaderText = "Alpha";
            this.Alpha.Name = "Alpha";
            this.Alpha.Width = 59;
            // 
            // Level
            // 
            this.Level.HeaderText = "Level";
            this.Level.Name = "Level";
            this.Level.Width = 58;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(171, 245);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Добавить цвет";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.Button3Click);
            // 
            // AdvancedOptionsForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(442, 280);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.AdvancedOptionsFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.generalOptions.ResumeLayout(false);
            this.colorOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.densityNum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalOptions;
        private System.Windows.Forms.TextBox lightPosW;
        private System.Windows.Forms.TextBox lightPosZ;
        private System.Windows.Forms.TextBox lightPosY;
        private System.Windows.Forms.TextBox lightPosX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown densityNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox fogCheckBox;
        private System.Windows.Forms.TabPage colorOptions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorExample;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Red;
        private System.Windows.Forms.DataGridViewTextBoxColumn Green;
        private System.Windows.Forms.DataGridViewTextBoxColumn Blue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alpha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.Button button3;
    }
}