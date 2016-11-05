namespace ABClient.MyForms
{
    internal partial class FormNewTimer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewTimer));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboPotion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textCell = new System.Windows.Forms.TextBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkRecur = new System.Windows.Forms.CheckBox();
            this.textDrinkCount = new System.Windows.Forms.TextBox();
            this.radioNone = new System.Windows.Forms.RadioButton();
            this.radioPotion = new System.Windows.Forms.RadioButton();
            this.radioDestination = new System.Windows.Forms.RadioButton();
            this.numTriggerHour = new System.Windows.Forms.NumericUpDown();
            this.numTriggerMin = new System.Windows.Forms.NumericUpDown();
            this.radioComplect = new System.Windows.Forms.RadioButton();
            this.textComplect = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTriggerHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTriggerMin)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Имя таймера";
            // 
            // buttonOk
            // 
            this.buttonOk.AutoSize = true;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(139, 275);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(122, 23);
            this.buttonOk.TabIndex = 1014;
            this.buttonOk.Text = "Ввод";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(267, 275);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 23);
            this.buttonCancel.TabIndex = 1015;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // comboPotion
            // 
            this.comboPotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPotion.Enabled = false;
            this.comboPotion.FormattingEnabled = true;
            this.comboPotion.Items.AddRange(new object[] {
            "Не пить, просто таймер",
            "Зелье Метаболизма",
            "Зелье Блаженства",
            "Зелье Сильной Спины",
            "Зелье Просветления",
            "Зелье Сокрушительных Ударов",
            "Зелье Стойкости",
            "Зелье Недосягаемости",
            "Зелье Точного Попадания",
            "Зелье Ловких Ударов",
            "Зелье Мужества",
            "Зелье Жизни",
            "Зелье Лечения",
            "Зелье Восстановления Маны",
            "Зелье Энергии",
            "Зелье Удачи",
            "Зелье Силы",
            "Зелье Ловкости",
            "Зелье Гения",
            "Зелье Боевой Славы",
            "Зелье Невидимости",
            "Зелье Секрет Волшебника",
            "Зелье Медитации",
            "Зелье Иммунитета",
            "Яд",
            "Зелье Лечения Отравлений",
            "Зелье Огненного Ореола",
            "Зелье Колкости",
            "Зелье Загрубелой Кожи",
            "Зелье Панциря",
            "Зелье Человек-гора",
            "Зелье Скорости",
            "Жажда Жизни",
            "Ментальная Жажда",
            "Зелье подвижности",
            "Ярость Берсерка",
            "Зелье Хрупкости",
            "Зелье Мифриловый Стержень",
            "Зелье Соколиный взор",
            "Секретное Зелье"});
            this.comboPotion.Location = new System.Drawing.Point(11, 35);
            this.comboPotion.Name = "comboPotion";
            this.comboPotion.Size = new System.Drawing.Size(314, 21);
            this.comboPotion.TabIndex = 1017;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(16, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 13);
            this.label4.TabIndex = 1018;
            this.label4.Text = "Введите название зелья (в инвентаре)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(16, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 1020;
            this.label5.Text = "Делать глотков";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 1021;
            this.label7.Text = "Сработает через";
            // 
            // textCell
            // 
            this.textCell.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textCell.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textCell.Enabled = false;
            this.textCell.Location = new System.Drawing.Point(178, 97);
            this.textCell.Name = "textCell";
            this.textCell.Size = new System.Drawing.Size(69, 21);
            this.textCell.TabIndex = 1024;
            this.textCell.TextChanged += new System.EventHandler(this.OnTextCellTextChanged);
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(12, 25);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(341, 21);
            this.textName.TabIndex = 1025;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkRecur);
            this.groupBox1.Controls.Add(this.textDrinkCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboPotion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 100);
            this.groupBox1.TabIndex = 1029;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пьем зелье по таймеру";
            // 
            // checkRecur
            // 
            this.checkRecur.AutoSize = true;
            this.checkRecur.Enabled = false;
            this.checkRecur.Location = new System.Drawing.Point(190, 69);
            this.checkRecur.Name = "checkRecur";
            this.checkRecur.Size = new System.Drawing.Size(126, 17);
            this.checkRecur.TabIndex = 1036;
            this.checkRecur.Text = "Циклическое питье";
            this.checkRecur.UseVisualStyleBackColor = true;
            // 
            // textDrinkCount
            // 
            this.textDrinkCount.Enabled = false;
            this.textDrinkCount.Location = new System.Drawing.Point(111, 67);
            this.textDrinkCount.Name = "textDrinkCount";
            this.textDrinkCount.Size = new System.Drawing.Size(33, 21);
            this.textDrinkCount.TabIndex = 1021;
            this.textDrinkCount.Text = "1";
            // 
            // radioNone
            // 
            this.radioNone.AutoSize = true;
            this.radioNone.Checked = true;
            this.radioNone.Location = new System.Drawing.Point(12, 52);
            this.radioNone.Name = "radioNone";
            this.radioNone.Size = new System.Drawing.Size(100, 17);
            this.radioNone.TabIndex = 1030;
            this.radioNone.TabStop = true;
            this.radioNone.Text = "Просто таймер";
            this.radioNone.UseVisualStyleBackColor = true;
            this.radioNone.CheckedChanged += new System.EventHandler(this.radioNone_CheckedChanged);
            // 
            // radioPotion
            // 
            this.radioPotion.AutoSize = true;
            this.radioPotion.Location = new System.Drawing.Point(12, 75);
            this.radioPotion.Name = "radioPotion";
            this.radioPotion.Size = new System.Drawing.Size(142, 17);
            this.radioPotion.TabIndex = 1031;
            this.radioPotion.Text = "Пьем зелье по таймеру";
            this.radioPotion.UseVisualStyleBackColor = true;
            this.radioPotion.CheckedChanged += new System.EventHandler(this.radioNone_CheckedChanged);
            // 
            // radioDestination
            // 
            this.radioDestination.AutoSize = true;
            this.radioDestination.Location = new System.Drawing.Point(12, 98);
            this.radioDestination.Name = "radioDestination";
            this.radioDestination.Size = new System.Drawing.Size(160, 17);
            this.radioDestination.TabIndex = 1032;
            this.radioDestination.Text = "Перемещаемся по таймеру";
            this.radioDestination.UseVisualStyleBackColor = true;
            this.radioDestination.CheckedChanged += new System.EventHandler(this.radioNone_CheckedChanged);
            // 
            // numTriggerHour
            // 
            this.numTriggerHour.Location = new System.Drawing.Point(253, 52);
            this.numTriggerHour.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numTriggerHour.Name = "numTriggerHour";
            this.numTriggerHour.Size = new System.Drawing.Size(45, 21);
            this.numTriggerHour.TabIndex = 1034;
            this.numTriggerHour.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numTriggerMin
            // 
            this.numTriggerMin.Location = new System.Drawing.Point(301, 52);
            this.numTriggerMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numTriggerMin.Name = "numTriggerMin";
            this.numTriggerMin.Size = new System.Drawing.Size(45, 21);
            this.numTriggerMin.TabIndex = 1035;
            // 
            // radioComplect
            // 
            this.radioComplect.AutoSize = true;
            this.radioComplect.Location = new System.Drawing.Point(12, 121);
            this.radioComplect.Name = "radioComplect";
            this.radioComplect.Size = new System.Drawing.Size(121, 17);
            this.radioComplect.TabIndex = 1036;
            this.radioComplect.Text = "Одеваем комплект";
            this.radioComplect.UseVisualStyleBackColor = true;
            // 
            // textComplect
            // 
            this.textComplect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textComplect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textComplect.Enabled = false;
            this.textComplect.Location = new System.Drawing.Point(139, 120);
            this.textComplect.Name = "textComplect";
            this.textComplect.Size = new System.Drawing.Size(69, 21);
            this.textComplect.TabIndex = 1037;
            // 
            // FormNewTimer
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(369, 325);
            this.Controls.Add(this.textComplect);
            this.Controls.Add(this.radioComplect);
            this.Controls.Add(this.numTriggerMin);
            this.Controls.Add(this.textCell);
            this.Controls.Add(this.numTriggerHour);
            this.Controls.Add(this.radioDestination);
            this.Controls.Add(this.radioPotion);
            this.Controls.Add(this.radioNone);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewTimer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новый таймер";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTriggerHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTriggerMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboPotion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textCell;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioNone;
        private System.Windows.Forms.RadioButton radioPotion;
        private System.Windows.Forms.RadioButton radioDestination;
        private System.Windows.Forms.TextBox textDrinkCount;
        private System.Windows.Forms.CheckBox checkRecur;
        private System.Windows.Forms.NumericUpDown numTriggerHour;
        private System.Windows.Forms.NumericUpDown numTriggerMin;
        private System.Windows.Forms.RadioButton radioComplect;
        private System.Windows.Forms.TextBox textComplect;
    }
}