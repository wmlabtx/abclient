namespace ABClient.MyForms
{
    using System.Diagnostics;
    using System.Windows.Forms;
    using System;

    internal partial class FormNewVersion : Form
    {
        internal FormNewVersion(string text)
        {
            InitializeComponent();

            label.Text = text;
        }

        private void FormNewVersion_Load(object sender, EventArgs e)
        {
            Text = AppVars.AppVersion.NickProductShortVersion;
            comboNextCheck.SelectedIndex = 0;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var startInfo = new ProcessStartInfo(linkLabel.Text);
            Process.Start(startInfo);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var textNextCheck = (string) comboNextCheck.SelectedItem;
            int days;
            switch (textNextCheck)
            {
                case "день":
                    days = 1;
                    break;
                case "три дня":
                    days = 3;
                    break;
                case "неделю":
                    days = 7;
                    break;
                case "месяц":
                    days = 31;
                    break;
                case "не проверять":
                    days = 365;
                    break;
                default:
                    days = 1;
                    break;
            }

            AppVars.Profile.NextCheckVersion = DateTime.Now.AddDays(days);
            AppVars.Profile.Save();
        }
    }
}