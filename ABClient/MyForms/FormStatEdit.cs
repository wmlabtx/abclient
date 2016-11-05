namespace ABClient.Forms
{
    using System.Windows.Forms;

    internal partial class FormStatEdit : Form
    {
        internal FormStatEdit(string statstring)
        {
            InitializeComponent();
            textBox.Text = statstring;
        }
    }
}