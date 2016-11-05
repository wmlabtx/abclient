namespace ABClient.Forms
{
    internal partial class NewPassword
    {
        private System.Windows.Forms.TextBox textPassword1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkVisiblePassword;
        private System.Windows.Forms.TextBox textPassword2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPassword));
            textPassword1 = new System.Windows.Forms.TextBox();
            buttonOk = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            checkVisiblePassword = new System.Windows.Forms.CheckBox();
            textPassword2 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // textPassword1
            // 
            textPassword1.Location = new System.Drawing.Point(8, 28);
            textPassword1.Name = "textPassword1";
            textPassword1.Size = new System.Drawing.Size(120, 21);
            textPassword1.TabIndex = 0;
            textPassword1.UseSystemPasswordChar = true;
            textPassword1.TextChanged += new System.EventHandler(TextPassword_TextChanged);
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Enabled = false;
            buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            buttonOk.Location = new System.Drawing.Point(62, 87);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(111, 23);
            buttonOk.TabIndex = 3;
            buttonOk.Text = "Ввод";
            buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(179, 87);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(75, 23);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkVisiblePassword
            // 
            checkVisiblePassword.AutoSize = true;
            checkVisiblePassword.Location = new System.Drawing.Point(15, 55);
            checkVisiblePassword.Name = "checkVisiblePassword";
            checkVisiblePassword.Size = new System.Drawing.Size(110, 17);
            checkVisiblePassword.TabIndex = 2;
            checkVisiblePassword.Text = "Видимые пароли";
            checkVisiblePassword.UseVisualStyleBackColor = true;
            checkVisiblePassword.CheckedChanged += new System.EventHandler(CheckVisiblePassword_CheckedChanged);
            // 
            // textPassword2
            // 
            textPassword2.Location = new System.Drawing.Point(134, 28);
            textPassword2.Name = "textPassword2";
            textPassword2.Size = new System.Drawing.Size(120, 21);
            textPassword2.TabIndex = 1;
            textPassword2.UseSystemPasswordChar = true;
            textPassword2.TextChanged += new System.EventHandler(TextPassword_TextChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 13);
            label1.TabIndex = 5;
            label1.Text = "Пароль";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(140, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(83, 13);
            label2.TabIndex = 6;
            label2.Text = "Повтор пароля";
            // 
            // FormNewPassword
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(262, 119);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textPassword2);
            Controls.Add(checkVisiblePassword);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(textPassword1);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormNewPassword";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Новый пароль шифрования";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
    }
}