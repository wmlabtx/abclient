namespace ABClient.Forms
{
    internal partial class FormProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProfile));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkDetectProxy = new System.Windows.Forms.LinkLabel();
            this.textProxyPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textProxyUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textProxyAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkUseProxy = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkPasswordProtected = new System.Windows.Forms.LinkLabel();
            this.checkVisiblePasswords = new System.Windows.Forms.CheckBox();
            this.checkAutoLogon = new System.Windows.Forms.CheckBox();
            this.labelFlashPassword = new System.Windows.Forms.Label();
            this.textFlashPassword = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkDetectProxy);
            this.groupBox2.Controls.Add(this.textProxyPassword);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textProxyUsername);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textProxyAddress);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkUseProxy);
            this.groupBox2.Location = new System.Drawing.Point(11, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 140);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки прокси";
            // 
            // linkDetectProxy
            // 
            this.linkDetectProxy.AutoSize = true;
            this.linkDetectProxy.Location = new System.Drawing.Point(161, 21);
            this.linkDetectProxy.Name = "linkDetectProxy";
            this.linkDetectProxy.Size = new System.Drawing.Size(108, 13);
            this.linkDetectProxy.TabIndex = 10;
            this.linkDetectProxy.TabStop = true;
            this.linkDetectProxy.Text = "Определить прокси";
            this.linkDetectProxy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkDetectProxy_LinkClicked);
            // 
            // textProxyPassword
            // 
            this.textProxyPassword.Enabled = false;
            this.textProxyPassword.Location = new System.Drawing.Point(155, 106);
            this.textProxyPassword.Name = "textProxyPassword";
            this.textProxyPassword.Size = new System.Drawing.Size(119, 21);
            this.textProxyPassword.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(158, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Пароль";
            // 
            // textProxyUsername
            // 
            this.textProxyUsername.Enabled = false;
            this.textProxyUsername.Location = new System.Drawing.Point(7, 106);
            this.textProxyUsername.Name = "textProxyUsername";
            this.textProxyUsername.Size = new System.Drawing.Size(141, 21);
            this.textProxyUsername.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Логин (если есть)";
            // 
            // textProxyAddress
            // 
            this.textProxyAddress.Enabled = false;
            this.textProxyAddress.Location = new System.Drawing.Point(7, 61);
            this.textProxyAddress.Name = "textProxyAddress";
            this.textProxyAddress.Size = new System.Drawing.Size(267, 21);
            this.textProxyAddress.TabIndex = 7;
            this.textProxyAddress.TextChanged += new System.EventHandler(this.TextProxyAddress_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Адрес прокси (например, localhost:3128)";
            // 
            // checkUseProxy
            // 
            this.checkUseProxy.AutoSize = true;
            this.checkUseProxy.Location = new System.Drawing.Point(7, 20);
            this.checkUseProxy.Name = "checkUseProxy";
            this.checkUseProxy.Size = new System.Drawing.Size(132, 17);
            this.checkUseProxy.TabIndex = 6;
            this.checkUseProxy.Text = "Работа через прокси";
            this.checkUseProxy.UseVisualStyleBackColor = true;
            this.checkUseProxy.CheckedChanged += new System.EventHandler(this.CheckUseProxy_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkPasswordProtected);
            this.groupBox1.Controls.Add(this.checkVisiblePasswords);
            this.groupBox1.Controls.Add(this.checkAutoLogon);
            this.groupBox1.Location = new System.Drawing.Point(11, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 91);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки паролей";
            // 
            // linkPasswordProtected
            // 
            this.linkPasswordProtected.AutoSize = true;
            this.linkPasswordProtected.Enabled = false;
            this.linkPasswordProtected.Location = new System.Drawing.Point(7, 21);
            this.linkPasswordProtected.Name = "linkPasswordProtected";
            this.linkPasswordProtected.Size = new System.Drawing.Size(205, 13);
            this.linkPasswordProtected.TabIndex = 6;
            this.linkPasswordProtected.TabStop = true;
            this.linkPasswordProtected.Text = "Зашифровать пароли (рекомендуется)";
            this.linkPasswordProtected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkPasswordProtected_LinkClicked);
            // 
            // checkVisiblePasswords
            // 
            this.checkVisiblePasswords.AutoSize = true;
            this.checkVisiblePasswords.Location = new System.Drawing.Point(7, 68);
            this.checkVisiblePasswords.Name = "checkVisiblePasswords";
            this.checkVisiblePasswords.Size = new System.Drawing.Size(110, 17);
            this.checkVisiblePasswords.TabIndex = 5;
            this.checkVisiblePasswords.Text = "Видимые пароли";
            this.checkVisiblePasswords.UseVisualStyleBackColor = true;
            this.checkVisiblePasswords.CheckedChanged += new System.EventHandler(this.CheckVisiblePasswords_CheckedChanged);
            // 
            // checkAutoLogon
            // 
            this.checkAutoLogon.AutoSize = true;
            this.checkAutoLogon.Enabled = false;
            this.checkAutoLogon.Location = new System.Drawing.Point(7, 45);
            this.checkAutoLogon.Name = "checkAutoLogon";
            this.checkAutoLogon.Size = new System.Drawing.Size(262, 17);
            this.checkAutoLogon.TabIndex = 4;
            this.checkAutoLogon.Text = "Автовход в игру (если пароли не шифрованы)";
            this.checkAutoLogon.UseVisualStyleBackColor = true;
            // 
            // labelFlashPassword
            // 
            this.labelFlashPassword.AutoSize = true;
            this.labelFlashPassword.Location = new System.Drawing.Point(11, 70);
            this.labelFlashPassword.Name = "labelFlashPassword";
            this.labelFlashPassword.Size = new System.Drawing.Size(79, 13);
            this.labelFlashPassword.TabIndex = 17;
            this.labelFlashPassword.Text = "Флеш-пароль:";
            // 
            // textFlashPassword
            // 
            this.textFlashPassword.Location = new System.Drawing.Point(149, 67);
            this.textFlashPassword.Name = "textFlashPassword";
            this.textFlashPassword.Size = new System.Drawing.Size(142, 21);
            this.textFlashPassword.TabIndex = 2;
            this.textFlashPassword.UseSystemPasswordChar = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(216, 352);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(11, 352);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(199, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Вход в игру";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(149, 39);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(142, 21);
            this.textPassword.TabIndex = 1;
            this.textPassword.UseSystemPasswordChar = true;
            this.textPassword.TextChanged += new System.EventHandler(this.TextPassword_TextChanged);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(11, 42);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(92, 13);
            this.labelPassword.TabIndex = 16;
            this.labelPassword.Text = "Игровой пароль:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Ник перса:";
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(149, 12);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(142, 21);
            this.textUsername.TabIndex = 0;
            this.textUsername.TextChanged += new System.EventHandler(this.TextUsername_TextChanged);
            // 
            // FormProfile
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(306, 388);
            this.Controls.Add(this.textUsername);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelFlashPassword);
            this.Controls.Add(this.textFlashPassword);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormProfile";
            this.Load += new System.EventHandler(this.FormProfile_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textProxyPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textProxyUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textProxyAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkUseProxy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkVisiblePasswords;
        private System.Windows.Forms.CheckBox checkAutoLogon;
        private System.Windows.Forms.Label labelFlashPassword;
        private System.Windows.Forms.TextBox textFlashPassword;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.LinkLabel linkDetectProxy;
        private System.Windows.Forms.LinkLabel linkPasswordProtected;
    }
}