using ABClient.AppControls;

namespace ABClient.Forms
{
    internal partial class HerbNavigator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HerbNavigator));
            this.listResult = new System.Windows.Forms.ListBox();
            this.browserReport = new ExtendedWebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkedListSearch = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // listResult
            // 
            this.listResult.FormattingEnabled = true;
            this.listResult.Location = new System.Drawing.Point(324, 25);
            this.listResult.Name = "listResult";
            this.listResult.Size = new System.Drawing.Size(207, 303);
            this.listResult.TabIndex = 1023;
            // 
            // browserReport
            // 
            this.browserReport.Location = new System.Drawing.Point(547, 25);
            this.browserReport.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserReport.Name = "browserReport";
            this.browserReport.Size = new System.Drawing.Size(307, 265);
            this.browserReport.TabIndex = 1026;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1027;
            this.label1.Text = "Поиск трав:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(174, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 23);
            this.button1.TabIndex = 1028;
            this.button1.Text = "Поиск трав >>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(175, 107);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(119, 17);
            this.checkBox1.TabIndex = 1029;
            this.checkBox1.Text = "Голос Мойры (VIP)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(327, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 1030;
            this.label2.Text = "Найденные травы:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(549, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 13);
            this.label3.TabIndex = 1031;
            this.label3.Text = "Путь к клетке (настройки навигатора):";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(593, 296);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(192, 32);
            this.buttonOk.TabIndex = 1032;
            this.buttonOk.Text = "В путь!";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // checkedListSearch
            // 
            this.checkedListSearch.CheckOnClick = true;
            this.checkedListSearch.FormattingEnabled = true;
            this.checkedListSearch.Items.AddRange(new object[] {
            "Арника",
            "Дурман"});
            this.checkedListSearch.Location = new System.Drawing.Point(13, 26);
            this.checkedListSearch.Name = "checkedListSearch";
            this.checkedListSearch.ScrollAlwaysVisible = true;
            this.checkedListSearch.Size = new System.Drawing.Size(155, 308);
            this.checkedListSearch.TabIndex = 1033;
            // 
            // HerbNavigator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(867, 345);
            this.Controls.Add(this.checkedListSearch);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browserReport);
            this.Controls.Add(this.listResult);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HerbNavigator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Навигатор трав";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listResult;
        private ExtendedWebBrowser browserReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.CheckedListBox checkedListSearch;
    }
}