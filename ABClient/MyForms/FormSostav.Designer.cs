namespace ABClient.Forms
{
    using AppControls;

    internal partial class FormSostav
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
#pragma warning disable 649
        private System.ComponentModel.IContainer components;
#pragma warning restore 649

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSostav));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.comboMaxLevel = new System.Windows.Forms.ComboBox();
            this.до = new System.Windows.Forms.Label();
            this.comboMinLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboTriba = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAddNevid = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRemoveGroup = new System.Windows.Forms.Button();
            this.listGroup = new ABClient.AppControls.ListBoxEx();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(305, 365);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(107, 23);
            this.buttonOk.TabIndex = 1004;
            this.buttonOk.Text = "Ввод";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(418, 365);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(81, 23);
            this.buttonCancel.TabIndex = 1005;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAddGroup);
            this.groupBox1.Controls.Add(this.comboMaxLevel);
            this.groupBox1.Controls.Add(this.до);
            this.groupBox1.Controls.Add(this.comboMinLevel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboTriba);
            this.groupBox1.Location = new System.Drawing.Point(161, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 89);
            this.groupBox1.TabIndex = 1008;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавление группы";
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.Location = new System.Drawing.Point(91, 57);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(152, 23);
            this.buttonAddGroup.TabIndex = 5;
            this.buttonAddGroup.Text = "<< Добавить группу";
            this.buttonAddGroup.UseVisualStyleBackColor = true;
            this.buttonAddGroup.Click += new System.EventHandler(this.buttonAddGroup_Click);
            // 
            // comboMaxLevel
            // 
            this.comboMaxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaxLevel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboMaxLevel.FormattingEnabled = true;
            this.comboMaxLevel.Location = new System.Drawing.Point(274, 20);
            this.comboMaxLevel.Name = "comboMaxLevel";
            this.comboMaxLevel.Size = new System.Drawing.Size(54, 21);
            this.comboMaxLevel.TabIndex = 4;
            // 
            // до
            // 
            this.до.AutoSize = true;
            this.до.Location = new System.Drawing.Point(248, 23);
            this.до.Name = "до";
            this.до.Size = new System.Drawing.Size(20, 13);
            this.до.TabIndex = 3;
            this.до.Text = "до";
            // 
            // comboMinLevel
            // 
            this.comboMinLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMinLevel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboMinLevel.FormattingEnabled = true;
            this.comboMinLevel.Location = new System.Drawing.Point(188, 20);
            this.comboMinLevel.Name = "comboMinLevel";
            this.comboMinLevel.Size = new System.Drawing.Size(54, 21);
            this.comboMinLevel.TabIndex = 2;
            this.comboMinLevel.SelectedIndexChanged += new System.EventHandler(this.comboMinLevel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "от";
            // 
            // comboTriba
            // 
            this.comboTriba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTriba.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboTriba.FormattingEnabled = true;
            this.comboTriba.Items.AddRange(new object[] {
            "Человек",
            "Орк",
            "Гоблин",
            "Огр",
            "Кабан",
            "Крыса",
            "Паук",
            "Ядовитый паук",
            "Зомби",
            "Скелет",
            "Скелет-Воин",
            "Разбойник", 
            "Грабитель",
            "Королева Змей",
            "Хранитель Леса",
            "Громлех Синезубый",
            "Выползень"});
            this.comboTriba.Location = new System.Drawing.Point(19, 20);
            this.comboTriba.Name = "comboTriba";
            this.comboTriba.Size = new System.Drawing.Size(138, 21);
            this.comboTriba.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAddNevid);
            this.groupBox2.Location = new System.Drawing.Point(161, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 54);
            this.groupBox2.TabIndex = 1009;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Добавление невидимки";
            // 
            // buttonAddNevid
            // 
            this.buttonAddNevid.Location = new System.Drawing.Point(91, 20);
            this.buttonAddNevid.Name = "buttonAddNevid";
            this.buttonAddNevid.Size = new System.Drawing.Size(152, 23);
            this.buttonAddNevid.TabIndex = 6;
            this.buttonAddNevid.Text = "<< Добавить невидимку";
            this.buttonAddNevid.UseVisualStyleBackColor = true;
            this.buttonAddNevid.Click += new System.EventHandler(this.buttonAddNevid_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.buttonRemoveGroup);
            this.groupBox3.Location = new System.Drawing.Point(161, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(334, 87);
            this.groupBox3.TabIndex = 1010;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Удаление группы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Группу в списке можно выделить, зажимая Shift";
            // 
            // buttonRemoveGroup
            // 
            this.buttonRemoveGroup.Location = new System.Drawing.Point(91, 54);
            this.buttonRemoveGroup.Name = "buttonRemoveGroup";
            this.buttonRemoveGroup.Size = new System.Drawing.Size(152, 23);
            this.buttonRemoveGroup.TabIndex = 7;
            this.buttonRemoveGroup.Text = "Удалить группу >>";
            this.buttonRemoveGroup.UseVisualStyleBackColor = true;
            this.buttonRemoveGroup.Click += new System.EventHandler(this.buttonRemoveGroup_Click);
            // 
            // listGroup
            // 
            this.listGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listGroup.FormattingEnabled = true;
            this.listGroup.Location = new System.Drawing.Point(13, 13);
            this.listGroup.Name = "listGroup";
            this.listGroup.ScrollAlwaysVisible = true;
            this.listGroup.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listGroup.Size = new System.Drawing.Size(138, 329);
            this.listGroup.TabIndex = 1011;
            // 
            // FormSostav
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(507, 394);
            this.Controls.Add(this.listGroup);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSostav";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Состав группы противников";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboTriba;
        private System.Windows.Forms.ComboBox comboMinLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboMaxLevel;
        private System.Windows.Forms.Label до;
        private System.Windows.Forms.Button buttonAddGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAddNevid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonRemoveGroup;
        private System.Windows.Forms.Label label3;
        private ListBoxEx listGroup;
    }
}