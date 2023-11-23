namespace lab1SisProg
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sourceCodeTextBox = new System.Windows.Forms.TextBox();
            this.operationCodeDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tuneTableDT = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.firstPassErrorTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.symbolTableDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.binaryCodeTextBox = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.step = new System.Windows.Forms.Button();
            this.exampleComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.restart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operationCodeDataGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tuneTableDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableDataGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binaryCodeTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sourceCodeTextBox);
            this.groupBox1.Controls.Add(this.operationCodeDataGrid);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 603);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // sourceCodeTextBox
            // 
            this.sourceCodeTextBox.BackColor = System.Drawing.Color.White;
            this.sourceCodeTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sourceCodeTextBox.Location = new System.Drawing.Point(6, 32);
            this.sourceCodeTextBox.Multiline = true;
            this.sourceCodeTextBox.Name = "sourceCodeTextBox";
            this.sourceCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sourceCodeTextBox.Size = new System.Drawing.Size(317, 345);
            this.sourceCodeTextBox.TabIndex = 10;
            this.sourceCodeTextBox.TextChanged += new System.EventHandler(this.SourceCodeTextBox_TextChanged);
            // 
            // operationCodeDataGrid
            // 
            this.operationCodeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.operationCodeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.operationCodeDataGrid.Location = new System.Drawing.Point(60, 409);
            this.operationCodeDataGrid.Name = "operationCodeDataGrid";
            this.operationCodeDataGrid.RowHeadersVisible = false;
            this.operationCodeDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.operationCodeDataGrid.Size = new System.Drawing.Size(202, 188);
            this.operationCodeDataGrid.TabIndex = 3;
            this.operationCodeDataGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.OperationCodeDataGrid_CellBeginEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "МКО";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Дв.Код";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Длина";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(73, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Таблица кодов операций";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(107, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Исходный текст";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tuneTableDT);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.firstPassErrorTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.symbolTableDataGrid);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(347, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 603);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // tuneTableDT
            // 
            this.tuneTableDT.AllowUserToAddRows = false;
            this.tuneTableDT.AllowUserToDeleteRows = false;
            this.tuneTableDT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tuneTableDT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6});
            this.tuneTableDT.Location = new System.Drawing.Point(6, 291);
            this.tuneTableDT.Name = "tuneTableDT";
            this.tuneTableDT.ReadOnly = true;
            this.tuneTableDT.RowHeadersVisible = false;
            this.tuneTableDT.Size = new System.Drawing.Size(430, 224);
            this.tuneTableDT.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.Frozen = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "Симв. Имя";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 430;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(160, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Таблица настройки";
            // 
            // firstPassErrorTextBox
            // 
            this.firstPassErrorTextBox.BackColor = System.Drawing.Color.White;
            this.firstPassErrorTextBox.Location = new System.Drawing.Point(6, 538);
            this.firstPassErrorTextBox.Multiline = true;
            this.firstPassErrorTextBox.Name = "firstPassErrorTextBox";
            this.firstPassErrorTextBox.ReadOnly = true;
            this.firstPassErrorTextBox.Size = new System.Drawing.Size(430, 59);
            this.firstPassErrorTextBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(194, 518);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ошибки";
            // 
            // symbolTableDataGrid
            // 
            this.symbolTableDataGrid.AllowUserToAddRows = false;
            this.symbolTableDataGrid.AllowUserToDeleteRows = false;
            this.symbolTableDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symbolTableDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Column1});
            this.symbolTableDataGrid.Location = new System.Drawing.Point(6, 32);
            this.symbolTableDataGrid.Name = "symbolTableDataGrid";
            this.symbolTableDataGrid.ReadOnly = true;
            this.symbolTableDataGrid.RowHeadersVisible = false;
            this.symbolTableDataGrid.Size = new System.Drawing.Size(430, 236);
            this.symbolTableDataGrid.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.Frozen = true;
            this.dataGridViewTextBoxColumn4.HeaderText = "Симв. Имя";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 143;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.Frozen = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "Адрес Симв. Имени";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 143;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Значение счет. Адреса";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 143;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(140, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Таблица символьных имён";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.binaryCodeTextBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(789, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(342, 603);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // binaryCodeTextBox
            // 
            this.binaryCodeTextBox.AllowUserToAddRows = false;
            this.binaryCodeTextBox.AllowUserToDeleteRows = false;
            this.binaryCodeTextBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.binaryCodeTextBox.ColumnHeadersVisible = false;
            this.binaryCodeTextBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.binaryCodeTextBox.Location = new System.Drawing.Point(6, 32);
            this.binaryCodeTextBox.Name = "binaryCodeTextBox";
            this.binaryCodeTextBox.ReadOnly = true;
            this.binaryCodeTextBox.RowHeadersVisible = false;
            this.binaryCodeTextBox.Size = new System.Drawing.Size(330, 565);
            this.binaryCodeTextBox.TabIndex = 5;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 33;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 98;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 98;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 98;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(117, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Объектный модуль";
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.start.Location = new System.Drawing.Point(72, 627);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(202, 45);
            this.start.TabIndex = 10;
            this.start.Text = "Запуск/Продолжить";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // step
            // 
            this.step.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.step.Location = new System.Drawing.Point(353, 627);
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(202, 45);
            this.step.TabIndex = 11;
            this.step.Text = "Один шаг";
            this.step.UseVisualStyleBackColor = true;
            this.step.Click += new System.EventHandler(this.step_Click);
            // 
            // exampleComboBox
            // 
            this.exampleComboBox.FormattingEnabled = true;
            this.exampleComboBox.Items.AddRange(new object[] {
            "Прямая адресация",
            "Относительная адресация",
            "Смешанная адресация"});
            this.exampleComboBox.Location = new System.Drawing.Point(795, 651);
            this.exampleComboBox.Name = "exampleComboBox";
            this.exampleComboBox.Size = new System.Drawing.Size(330, 21);
            this.exampleComboBox.TabIndex = 12;
            this.exampleComboBox.SelectedIndexChanged += new System.EventHandler(this.ExampleComboBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.Location = new System.Drawing.Point(916, 632);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "Выбор примера";
            // 
            // restart
            // 
            this.restart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.restart.Location = new System.Drawing.Point(581, 627);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(202, 45);
            this.restart.TabIndex = 13;
            this.restart.Text = "Перезапуск";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 684);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.exampleComboBox);
            this.Controls.Add(this.step);
            this.Controls.Add(this.start);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Однопросмотровый ассемблер для программы в перемещаемом формате";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operationCodeDataGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tuneTableDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableDataGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binaryCodeTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView operationCodeDataGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView symbolTableDataGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstPassErrorTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button step;
        private System.Windows.Forms.TextBox sourceCodeTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ComboBox exampleComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView binaryCodeTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView tuneTableDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}

