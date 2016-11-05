namespace ABClient.MyForms
{
    using System;
    using System.Windows.Forms;
    using MyHelpers;

    internal partial class FormAskPassword : Form
    {
        private readonly string hash;

        internal FormAskPassword(string hashPassword)
        {
            if (string.IsNullOrEmpty(hashPassword))
            {
                throw new ArgumentNullException("hashPassword");
            }

            InitializeComponent();
            hash = hashPassword;
        }

        /// <summary>
        /// Пароль, введенный в поле
        /// </summary>
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
            var validPassword = Helpers.Crypts.Password2Hash(textPassword.Text).Equals(hash);
            buttonOk.Enabled = validPassword;
        }
    }
}
