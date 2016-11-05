namespace ABClient.Forms
{
    using System;
    using System.Windows.Forms;

    public partial class FormGroup : Form
    {
        public FormGroup(string nick)
        {
            InitializeComponent();

            textBox.Text = nick;
        }

        public string GroupName
        {
            get { return textBox.Text.Trim(); }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = !string.IsNullOrEmpty(textBox.Text.Trim());
        }
    }
}