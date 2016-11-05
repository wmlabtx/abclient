namespace ABClient.Forms
{
    partial class FormProfiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProfiles));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelLastLogOn = new System.Windows.Forms.Label();
            this.linkEdit = new System.Windows.Forms.LinkLabel();
            this.comboConfigs = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.linkCreateNewProfile = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelLastLogOn);
            this.groupBox1.Controls.Add(this.linkEdit);
            this.groupBox1.Controls.Add(this.comboConfigs);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите, кем войти";
            // 
            // labelLastLogOn
            // 
            this.labelLastLogOn.AutoSize = true;
            this.labelLastLogOn.Location = new System.Drawing.Point(7, 49);
            this.labelLastLogOn.Name = "labelLastLogOn";
            this.labelLastLogOn.Size = new System.Drawing.Size(134, 13);
            this.labelLastLogOn.TabIndex = 2;
            this.labelLastLogOn.Text = "Последний заход в игру:";
            // 
            // linkEdit
            // 
            this.linkEdit.AutoSize = true;
            this.linkEdit.Location = new System.Drawing.Point(251, 24);
            this.linkEdit.Name = "linkEdit";
            this.linkEdit.Size = new System.Drawing.Size(64, 13);
            this.linkEdit.TabIndex = 1;
            this.linkEdit.TabStop = true;
            this.linkEdit.Text = "Параметры";
            this.linkEdit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkEdit_LinkClicked);
            // 
            // comboConfigs
            // 
            this.comboConfigs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConfigs.FormattingEnabled = true;
            this.comboConfigs.Location = new System.Drawing.Point(7, 21);
            this.comboConfigs.Name = "comboConfigs";
            this.comboConfigs.Size = new System.Drawing.Size(238, 21);
            this.comboConfigs.TabIndex = 0;
            this.comboConfigs.SelectedIndexChanged += new System.EventHandler(this.ComboConfigs_SelectedIndexChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(259, 129);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(12, 129);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(241, 23);
            this.buttonOk.TabIndex = 12;
            this.buttonOk.Text = "Вход в игру";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // linkCreateNewProfile
            // 
            this.linkCreateNewProfile.AutoSize = true;
            this.linkCreateNewProfile.Location = new System.Drawing.Point(197, 103);
            this.linkCreateNewProfile.Name = "linkCreateNewProfile";
            this.linkCreateNewProfile.Size = new System.Drawing.Size(137, 13);
            this.linkCreateNewProfile.TabIndex = 14;
            this.linkCreateNewProfile.TabStop = true;
            this.linkCreateNewProfile.Text = "Создать новый персонаж";
            this.linkCreateNewProfile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkCreateNewProfile_LinkClicked);
            // 
            // FormProfiles
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(346, 167);
            this.Controls.Add(this.linkCreateNewProfile);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormProfiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход в игру";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ComboBox comboConfigs;
        private System.Windows.Forms.LinkLabel linkEdit;
        private System.Windows.Forms.LinkLabel linkCreateNewProfile;
        private System.Windows.Forms.Label labelLastLogOn;
    }
}