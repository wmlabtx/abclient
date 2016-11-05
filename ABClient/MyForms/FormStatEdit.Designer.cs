using ABClient.Properties;

namespace ABClient.Forms
{
    internal partial class FormStatEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatEdit));
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.AutoSize = true;
            this.buttonReset.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonReset.Location = new System.Drawing.Point(108, 206);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(125, 23);
            this.buttonReset.TabIndex = 1007;
            this.buttonReset.Text = "Сброс статистики";
            this.buttonReset.UseVisualStyleBackColor = true;
            // 
            // buttonContinue
            // 
            this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonContinue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonContinue.Location = new System.Drawing.Point(239, 206);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(107, 23);
            this.buttonContinue.TabIndex = 1008;
            this.buttonContinue.Text = "Продолжить";
            this.buttonContinue.UseVisualStyleBackColor = true;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(13, 12);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(333, 179);
            this.textBox.TabIndex = 1011;
            // 
            // FormStatEdit
            // 
            this.AcceptButton = this.buttonReset;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonContinue;
            this.ClientSize = new System.Drawing.Size(358, 237);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonContinue);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStatEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистика";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.TextBox textBox;
    }
}