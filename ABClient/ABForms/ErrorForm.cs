namespace ABClient.ABForms
{
    using System;
    using System.Windows.Forms;

    internal sealed partial class FormAutoTrap : Form
    {
        internal FormAutoTrap(string strException)
        {
            InitializeComponent();
            Icon = Properties.Resources.ABClientIcon;

            textBox.Text = strException;
            textBox.Select(0, 0);
        }

        private void ErrorLoad(object sender, EventArgs e)
        {
            Text = AppVars.AppVersion.ProductShortVersion;
        }
    }
}