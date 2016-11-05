namespace ABClient.MyForms
{
    internal partial class FormNewTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewTab));
            this.textAddress = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioUrl = new System.Windows.Forms.RadioButton();
            this.radioForum = new System.Windows.Forms.RadioButton();
            this.radioFightLog = new System.Windows.Forms.RadioButton();
            this.radioPInfo = new System.Windows.Forms.RadioButton();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textAddress
            // 
            this.textAddress.Location = new System.Drawing.Point(12, 24);
            this.textAddress.Name = "textAddress";
            this.textAddress.Size = new System.Drawing.Size(524, 21);
            this.textAddress.TabIndex = 0;
            this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioUrl);
            this.groupBox1.Controls.Add(this.radioForum);
            this.groupBox1.Controls.Add(this.radioFightLog);
            this.groupBox1.Controls.Add(this.radioPInfo);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 106);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Считать ссылку...";
            // 
            // radioUrl
            // 
            this.radioUrl.AutoSize = true;
            this.radioUrl.Location = new System.Drawing.Point(6, 80);
            this.radioUrl.Name = "radioUrl";
            this.radioUrl.Size = new System.Drawing.Size(60, 17);
            this.radioUrl.TabIndex = 4;
            this.radioUrl.TabStop = true;
            this.radioUrl.Text = "сайтом";
            this.radioUrl.UseVisualStyleBackColor = true;
            // 
            // radioForum
            // 
            this.radioForum.AutoSize = true;
            this.radioForum.Location = new System.Drawing.Point(6, 60);
            this.radioForum.Name = "radioForum";
            this.radioForum.Size = new System.Drawing.Size(69, 17);
            this.radioForum.TabIndex = 3;
            this.radioForum.TabStop = true;
            this.radioForum.Text = "форумом";
            this.radioForum.UseVisualStyleBackColor = true;
            // 
            // radioFightLog
            // 
            this.radioFightLog.AutoSize = true;
            this.radioFightLog.Location = new System.Drawing.Point(6, 40);
            this.radioFightLog.Name = "radioFightLog";
            this.radioFightLog.Size = new System.Drawing.Size(75, 17);
            this.radioFightLog.TabIndex = 2;
            this.radioFightLog.TabStop = true;
            this.radioFightLog.Text = "логом боя";
            this.radioFightLog.UseVisualStyleBackColor = true;
            // 
            // radioPInfo
            // 
            this.radioPInfo.AutoSize = true;
            this.radioPInfo.Location = new System.Drawing.Point(6, 20);
            this.radioPInfo.Name = "radioPInfo";
            this.radioPInfo.Size = new System.Drawing.Size(89, 17);
            this.radioPInfo.TabIndex = 1;
            this.radioPInfo.TabStop = true;
            this.radioPInfo.Text = "инфой перса";
            this.radioPInfo.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.AutoSize = true;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(300, 181);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(137, 23);
            this.buttonOk.TabIndex = 1007;
            this.buttonOk.Text = "Ввод";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(443, 181);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 23);
            this.buttonCancel.TabIndex = 1008;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(15, 7);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(89, 13);
            this.label.TabIndex = 1010;
            this.label.Text = "Введите ссылку";
            // 
            // FormNewTab
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(548, 209);
            this.Controls.Add(this.label);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textAddress);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNewTab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Открыть новую закладку";
            this.Load += new System.EventHandler(this.FormNewTab_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textAddress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioPInfo;
        private System.Windows.Forms.RadioButton radioForum;
        private System.Windows.Forms.RadioButton radioFightLog;
        private System.Windows.Forms.RadioButton radioUrl;
        private System.Windows.Forms.Label label;
    }
}