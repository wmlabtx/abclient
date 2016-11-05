namespace ABClient.MyForms
{
    using AppControls;

    public partial class FormNavigator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNavigator));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCalc = new System.Windows.Forms.Button();
            this.textTooltipSuggest = new System.Windows.Forms.TextBox();
            this.treeDest = new ABClient.AppControls.TreeViewEx();
            this.textDest = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonClearFavorites = new System.Windows.Forms.Button();
            this.buttonSaveInFavorites = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelBotLevel = new System.Windows.Forms.Label();
            this.labelJumps = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browserMap = new ABClient.AppControls.ExtendedWebBrowser();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTied = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(391, 479);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(192, 32);
            this.buttonOk.TabIndex = 1003;
            this.buttonOk.Text = "В путь!";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(589, 479);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(106, 32);
            this.buttonCancel.TabIndex = 1004;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.buttonCalc);
            this.groupBox1.Controls.Add(this.textTooltipSuggest);
            this.groupBox1.Controls.Add(this.treeDest);
            this.groupBox1.Controls.Add(this.textDest);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 499);
            this.groupBox1.TabIndex = 1005;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Куда";
            // 
            // buttonCalc
            // 
            this.buttonCalc.Location = new System.Drawing.Point(81, 15);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(75, 23);
            this.buttonCalc.TabIndex = 1026;
            this.buttonCalc.Text = "проложить";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
            // 
            // textTooltipSuggest
            // 
            this.textTooltipSuggest.Location = new System.Drawing.Point(162, 17);
            this.textTooltipSuggest.Name = "textTooltipSuggest";
            this.textTooltipSuggest.Size = new System.Drawing.Size(191, 21);
            this.textTooltipSuggest.TabIndex = 1025;
            this.textTooltipSuggest.TextChanged += new System.EventHandler(this.textTitleSuggest_TextChanged);
            // 
            // treeDest
            // 
            this.treeDest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeDest.FullRowSelect = true;
            this.treeDest.Location = new System.Drawing.Point(9, 44);
            this.treeDest.Name = "treeDest";
            this.treeDest.Size = new System.Drawing.Size(344, 446);
            this.treeDest.TabIndex = 1027;
            this.treeDest.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDest_AfterSelect);
            // 
            // textDest
            // 
            this.textDest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textDest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textDest.Location = new System.Drawing.Point(9, 17);
            this.textDest.Name = "textDest";
            this.textDest.Size = new System.Drawing.Size(66, 21);
            this.textDest.TabIndex = 5;
            this.textDest.TextChanged += new System.EventHandler(this.textCell_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 20);
            this.label2.TabIndex = 1019;
            this.label2.Text = "Можно перемещаться, кликая по клеткам карты";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonClearFavorites);
            this.groupBox2.Controls.Add(this.buttonSaveInFavorites);
            this.groupBox2.Location = new System.Drawing.Point(380, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 48);
            this.groupBox2.TabIndex = 1020;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Запомненные локации";
            // 
            // buttonClearFavorites
            // 
            this.buttonClearFavorites.Location = new System.Drawing.Point(209, 15);
            this.buttonClearFavorites.Name = "buttonClearFavorites";
            this.buttonClearFavorites.Size = new System.Drawing.Size(108, 23);
            this.buttonClearFavorites.TabIndex = 1;
            this.buttonClearFavorites.Text = "Очистить список";
            this.buttonClearFavorites.UseVisualStyleBackColor = true;
            this.buttonClearFavorites.Click += new System.EventHandler(this.buttonClearFavorites_Click);
            // 
            // buttonSaveInFavorites
            // 
            this.buttonSaveInFavorites.Location = new System.Drawing.Point(11, 15);
            this.buttonSaveInFavorites.Name = "buttonSaveInFavorites";
            this.buttonSaveInFavorites.Size = new System.Drawing.Size(192, 23);
            this.buttonSaveInFavorites.TabIndex = 0;
            this.buttonSaveInFavorites.Text = "Сохранить в списке";
            this.buttonSaveInFavorites.UseVisualStyleBackColor = true;
            this.buttonSaveInFavorites.Click += new System.EventHandler(this.buttonSaveInFavorites_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelTied);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.labelBotLevel);
            this.groupBox3.Controls.Add(this.labelJumps);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.browserMap);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(380, 67);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(323, 392);
            this.groupBox3.TabIndex = 1021;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Маршрут";
            // 
            // labelBotLevel
            // 
            this.labelBotLevel.AutoSize = true;
            this.labelBotLevel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBotLevel.Location = new System.Drawing.Point(102, 38);
            this.labelBotLevel.Name = "labelBotLevel";
            this.labelBotLevel.Size = new System.Drawing.Size(14, 13);
            this.labelBotLevel.TabIndex = 1023;
            this.labelBotLevel.Text = "0";
            // 
            // labelJumps
            // 
            this.labelJumps.AutoSize = true;
            this.labelJumps.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelJumps.Location = new System.Drawing.Point(121, 21);
            this.labelJumps.Name = "labelJumps";
            this.labelJumps.Size = new System.Drawing.Size(14, 13);
            this.labelJumps.TabIndex = 1022;
            this.labelJumps.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 1021;
            this.label3.Text = "Уровень ботов:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1020;
            this.label1.Text = "Кол-во переходов:";
            // 
            // browserMap
            // 
            this.browserMap.Location = new System.Drawing.Point(11, 82);
            this.browserMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserMap.Name = "browserMap";
            this.browserMap.ScrollBarsEnabled = false;
            this.browserMap.Size = new System.Drawing.Size(300, 300);
            this.browserMap.TabIndex = 1017;
            this.browserMap.WebBrowserShortcutsEnabled = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 1024;
            this.label4.Text = "Усталость:";
            // 
            // labelTied
            // 
            this.labelTied.AutoSize = true;
            this.labelTied.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTied.Location = new System.Drawing.Point(246, 21);
            this.labelTied.Name = "labelTied";
            this.labelTied.Size = new System.Drawing.Size(14, 13);
            this.labelTied.TabIndex = 1025;
            this.labelTied.Text = "0";
            // 
            // FormNavigator
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(714, 524);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNavigator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Навигатор";
            this.Load += new System.EventHandler(this.FormNavigator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private ExtendedWebBrowser browserMap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textDest;
        private System.Windows.Forms.TextBox textTooltipSuggest;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonClearFavorites;
        private System.Windows.Forms.Button buttonSaveInFavorites;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelJumps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelBotLevel;
        private System.Windows.Forms.Button buttonCalc;
        private TreeViewEx treeDest;
        private System.Windows.Forms.Label labelTied;
        private System.Windows.Forms.Label label4;
    }
}