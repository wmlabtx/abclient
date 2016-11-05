using ABClient.Properties;

namespace ABClient.Forms
{
    internal partial class FormCode
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.textCode = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Image = Resources._16x16_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(7, 83);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(22, 22);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.AutoSize = true;
            this.btnMaximize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMaximize.Image = Resources._16x16_maximize;
            this.btnMaximize.Location = new System.Drawing.Point(119, 83);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(22, 22);
            this.btnMaximize.TabIndex = 2;
            this.btnMaximize.UseVisualStyleBackColor = true;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.AutoSize = true;
            this.btnEnter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnEnter.Enabled = false;
            this.btnEnter.Image = Resources._16x16_wright;
            this.btnEnter.Location = new System.Drawing.Point(88, 83);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(22, 22);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // textCode
            // 
            this.textCode.Location = new System.Drawing.Point(36, 84);
            this.textCode.Name = "textCode";
            this.textCode.Size = new System.Drawing.Size(46, 22);
            this.textCode.TabIndex = 0;
            this.textCode.TextChanged += new System.EventHandler(this.textCode_TextChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(7, 13);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(134, 60);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // FormCode
            // 
            this.AcceptButton = this.btnEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(149, 111);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnMaximize);
            this.Controls.Add(this.btnRefresh);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Ручной ввод кода";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox textCode;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}