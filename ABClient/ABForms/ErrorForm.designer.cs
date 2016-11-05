namespace ABClient.ABForms
{
    internal sealed partial class FormAutoTrap
    {
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Label labelText;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoTrap));
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(620, 334);
            this.textBox.TabIndex = 0;
            // 
            // buttonContinue
            // 
            this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonContinue.Location = new System.Drawing.Point(235, 368);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(150, 23);
            this.buttonContinue.TabIndex = 1;
            this.buttonContinue.Text = "Продолжить";
            this.buttonContinue.UseVisualStyleBackColor = true;
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(12, 342);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(596, 17);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "Скопируйте текст ошибки и отошлите автору на wmlab@hotmail.com";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ErrorForm
            // 
            this.AcceptButton = this.buttonContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 403);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.textBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ErrorLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}