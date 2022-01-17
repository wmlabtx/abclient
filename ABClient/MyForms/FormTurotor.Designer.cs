
namespace ABClient.MyForms
{
    partial class FormTurotor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxLocationsSecondary = new System.Windows.Forms.ComboBox();
            this.IntervalEnd = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.IntervalStart = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.comboBoxLocations = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalStart)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxLocationsSecondary);
            this.groupBox1.Controls.Add(this.IntervalEnd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.IntervalStart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(9, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 80);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дополнительные условия";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Куда идти:";
            // 
            // comboBoxLocationsSecondary
            // 
            this.comboBoxLocationsSecondary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocationsSecondary.FormattingEnabled = true;
            this.comboBoxLocationsSecondary.Items.AddRange(new object[] {
            "11-458 Огры [20]",
            "11-457 Огры-берсерки [21]",
            "11-456 Огры-берсерки [22]",
            "11-488 Огры-берсерки [23]",
            "11-487 Огры-берсерки [24]",
            "28-462 Огры-защитники [25]",
            "28-463 Огры-защитники [26]",
            "28-464 Огры-защитники [27]",
            "28-465 Огры-защитники [28]"});
            this.comboBoxLocationsSecondary.Location = new System.Drawing.Point(72, 46);
            this.comboBoxLocationsSecondary.Name = "comboBoxLocationsSecondary";
            this.comboBoxLocationsSecondary.Size = new System.Drawing.Size(239, 21);
            this.comboBoxLocationsSecondary.TabIndex = 7;
            // 
            // IntervalEnd
            // 
            this.IntervalEnd.Location = new System.Drawing.Point(216, 20);
            this.IntervalEnd.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.IntervalEnd.Name = "IntervalEnd";
            this.IntervalEnd.Size = new System.Drawing.Size(50, 20);
            this.IntervalEnd.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "до";
            // 
            // IntervalStart
            // 
            this.IntervalStart.Location = new System.Drawing.Point(135, 20);
            this.IntervalStart.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.IntervalStart.Name = "IntervalStart";
            this.IntervalStart.Size = new System.Drawing.Size(50, 20);
            this.IntervalStart.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Временной интервал с";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(194, 125);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(126, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(14, 125);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(174, 23);
            this.buttonGo.TabIndex = 9;
            this.buttonGo.Text = "Запустить";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click_1);
            // 
            // comboBoxLocations
            // 
            this.comboBoxLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocations.FormattingEnabled = true;
            this.comboBoxLocations.Items.AddRange(new object[] {
            "11-458 Огры [20]",
            "11-457 Огры-берсерки [21]",
            "11-456 Огры-берсерки [22]",
            "11-488 Огры-берсерки [23]",
            "11-487 Огры-берсерки [24]",
            "28-462 Огры-защитники [25]",
            "28-463 Огры-защитники [26]",
            "28-464 Огры-защитники [27]",
            "28-465 Огры-защитники [28]"});
            this.comboBoxLocations.Location = new System.Drawing.Point(81, 12);
            this.comboBoxLocations.Name = "comboBoxLocations";
            this.comboBoxLocations.Size = new System.Drawing.Size(239, 21);
            this.comboBoxLocations.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Куда идти:";
            // 
            // FormTurotor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.comboBoxLocations);
            this.Controls.Add(this.label1);
            this.Name = "FormTurotor";
            this.Text = "Остров Туротор / Гиблая Топь";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxLocationsSecondary;
        private System.Windows.Forms.NumericUpDown IntervalEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown IntervalStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.ComboBox comboBoxLocations;
        private System.Windows.Forms.Label label1;
    }
}