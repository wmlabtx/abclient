namespace ABClient.Forms
{
    using System;
    using System.Windows.Forms;

    internal partial class NewPassword : Form
    {
        internal NewPassword()
        {
            InitializeComponent();
        }

        internal string Password
        {
            get { return textPassword1.Text; }
        }

        private void CheckVisiblePassword_CheckedChanged(object sender, EventArgs e)
        {
            textPassword1.UseSystemPasswordChar = !checkVisiblePassword.Checked;
            textPassword2.UseSystemPasswordChar = textPassword1.UseSystemPasswordChar;
        }

        private void TextPassword_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = textPassword1.Text.Equals(textPassword2.Text, StringComparison.Ordinal);
        }
    }
}