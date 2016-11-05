namespace ABClient.MyForms
{
    internal partial class FormAutoLogon
    {
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Timer timerCountDown;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoLogon));
            labelUsername = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            buttonCancel = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            timerCountDown = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // labelUsername
            // 
            labelUsername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            labelUsername.Location = new System.Drawing.Point(12, 9);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new System.Drawing.Size(283, 20);
            labelUsername.TabIndex = 0;
            labelUsername.Text = "label1";
            labelUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(12, 29);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(283, 23);
            label2.TabIndex = 1;
            label2.Text = "входит в игру";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(218, 68);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(77, 23);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            buttonOk.Location = new System.Drawing.Point(12, 68);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(200, 23);
            buttonOk.TabIndex = 5;
            buttonOk.Text = "Автовход через 3 сек";
            buttonOk.UseVisualStyleBackColor = true;
            // 
            // timerCountDown
            // 
            timerCountDown.Interval = 1000;
            timerCountDown.Tick += new System.EventHandler(TimerCountDown_Tick);
            // 
            // FormAutoLogon
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(307, 107);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(label2);
            Controls.Add(labelUsername);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAutoLogon";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Автовход в игру";
            ResumeLayout(false);

        }

        #endregion
    }
}