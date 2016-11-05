namespace ABClient.Forms
{
    using System;
    using System.Windows.Forms;

    internal partial class AskPassword : Form
    {
        private readonly string Hash;

        internal AskPassword(string hashPassword)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(hashPassword))
            {
                throw new ArgumentNullException("hashPassword");
            }
            
            Hash = hashPassword;
        }

        internal string Password
        {
            get { return textPassword.Text; }
        }

        private void CheckVisiblePassword_CheckedChanged(object sender, EventArgs e)
        {
            textPassword.UseSystemPasswordChar = !checkVisiblePassword.Checked;
        }

        private void TextPassword_TextChanged(object sender, EventArgs e)
        {
            var validPassword = Helpers.Crypts.Password2Hash(textPassword.Text).Equals(Hash);
            buttonOk.Enabled = validPassword;
        }
    }
}