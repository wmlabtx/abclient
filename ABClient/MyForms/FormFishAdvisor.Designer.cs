namespace ABClient.MyForms
{
    internal partial class FormFishAdvisor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFishAdvisor));
            this.label1 = new System.Windows.Forms.Label();
            this.textFishUm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboHandleBots = new System.Windows.Forms.ComboBox();
            this.buttonCalc = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnMoney = new System.Windows.Forms.ColumnHeader();
            this.columnLocation = new System.Windows.Forms.ColumnHeader();
            this.columnLevel = new System.Windows.Forms.ColumnHeader();
            this.columnTip = new System.Windows.Forms.ColumnHeader();
            this.buttonOk = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Умение рыбалки";
            // 
            // textFishUm
            // 
            this.textFishUm.Location = new System.Drawing.Point(12, 29);
            this.textFishUm.Name = "textFishUm";
            this.textFishUm.Size = new System.Drawing.Size(101, 21);
            this.textFishUm.TabIndex = 1;
            this.textFishUm.Validating += new System.ComponentModel.CancelEventHandler(this.FormFishAdvisor_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Убиваемые Вами боты";
            // 
            // comboHandleBots
            // 
            this.comboHandleBots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHandleBots.FormattingEnabled = true;
            this.comboHandleBots.Location = new System.Drawing.Point(142, 29);
            this.comboHandleBots.Name = "comboHandleBots";
            this.comboHandleBots.Size = new System.Drawing.Size(181, 21);
            this.comboHandleBots.TabIndex = 3;
            // 
            // buttonCalc
            // 
            this.buttonCalc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalc.Location = new System.Drawing.Point(136, 74);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(272, 23);
            this.buttonCalc.TabIndex = 4;
            this.buttonCalc.Text = "Посоветовать, где и что ловить";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.ButtonCalc_Click);
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnMoney,
            this.columnLocation,
            this.columnLevel,
            this.columnTip});
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.LabelWrap = false;
            this.listView.Location = new System.Drawing.Point(12, 103);
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.Size = new System.Drawing.Size(521, 168);
            this.listView.TabIndex = 5;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnMoney
            // 
            this.columnMoney.Text = "Прибыль";
            this.columnMoney.Width = 58;
            // 
            // columnLocation
            // 
            this.columnLocation.Text = "Клетка";
            this.columnLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnLocation.Width = 50;
            // 
            // columnLevel
            // 
            this.columnLevel.Text = "Боты";
            this.columnLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnLevel.Width = 75;
            // 
            // columnTip
            // 
            this.columnTip.Text = "Инструкция";
            this.columnTip.Width = 297;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(374, 277);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(159, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Закрыть";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FormFishAdvisor
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOk;
            this.ClientSize = new System.Drawing.Size(545, 312);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.buttonCalc);
            this.Controls.Add(this.comboHandleBots);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textFishUm);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFishAdvisor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Советник рыбака";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.FormFishAdvisor_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFishUm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboHandleBots;
        private System.Windows.Forms.Button buttonCalc;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ColumnHeader columnMoney;
        private System.Windows.Forms.ColumnHeader columnLocation;
        private System.Windows.Forms.ColumnHeader columnTip;
        private System.Windows.Forms.ColumnHeader columnLevel;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}