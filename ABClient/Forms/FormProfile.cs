namespace ABClient.Forms
{
    using System;
    using System.Net;
    using System.Windows.Forms;
    using Profile;

    internal partial class FormProfile : Form
    {
        private readonly string StringTitle;

        private bool PasswordProtected;

        internal FormProfile()
        {
            InitializeComponent();

            StringTitle = AppConsts.ConfigNewTitle;
            SelectedUserConfig = new Config();
        }

        internal FormProfile(Config userConfig)
        {
            InitializeComponent();

            StringTitle = userConfig.UserNick;
            SelectedUserConfig = userConfig;
            textUsername.Text = userConfig.UserNick;
            textPassword.Text = userConfig.UserPassword;
            textFlashPassword.Text = userConfig.UserPasswordFlash;
            checkAutoLogon.Checked = userConfig.UserAutoLogon;
            checkUseProxy.Checked = userConfig.UseProxy;
            textProxyAddress.Text = userConfig.ProxyAddress;
            textProxyUsername.Text = userConfig.ProxyUserName;
            textProxyPassword.Text = userConfig.ProxyPassword;
            if (!string.IsNullOrEmpty(userConfig.ConfigHash))
            {
                PasswordProtected = true;
                linkPasswordProtected.Text = AppConsts.ConfigPasswordNotProtected;
            }

            buttonOk.Text = AppConsts.ConfigSave;
            CheckAvailability();
        }

        internal Config SelectedUserConfig { get; private set; }

        private void FormProfile_Load(object sender, EventArgs e)
        {
            Text = StringTitle;
        }

        private void TextUsername_TextChanged(object sender, EventArgs e)
        {
            CheckAvailability();
        }

        private void TextPassword_TextChanged(object sender, EventArgs e)
        {
            CheckAvailability();
        }

        private void LinkDetectProxy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DetectProxy();
        }

        private void CheckVisiblePasswords_CheckedChanged(object sender, EventArgs e)
        {
            var visiblePasswords = !checkVisiblePasswords.Checked;
            textPassword.UseSystemPasswordChar = visiblePasswords;
            textFlashPassword.UseSystemPasswordChar = visiblePasswords;
            textProxyPassword.UseSystemPasswordChar = visiblePasswords;
        }

        private void CheckUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            var enabledProxy = checkUseProxy.Checked;
            textProxyAddress.Enabled = enabledProxy;
            textProxyUsername.Enabled = enabledProxy;
            textProxyPassword.Enabled = enabledProxy;
            CheckAvailability();
        }

        private void TextProxyAddress_TextChanged(object sender, EventArgs e)
        {
            CheckAvailability();
        }

        private void LinkPasswordProtected_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Protect();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            SelectedUserConfig.UserNick = textUsername.Text.Trim();
            SelectedUserConfig.UserPassword = textPassword.Text.Trim();
            SelectedUserConfig.UserPasswordFlash = textFlashPassword.Text.Trim();
            SelectedUserConfig.UserAutoLogon = checkAutoLogon.Checked;
            SelectedUserConfig.UseProxy = checkUseProxy.Checked;
            SelectedUserConfig.ProxyAddress = textProxyAddress.Text.Trim();
            SelectedUserConfig.ProxyUserName = textProxyUsername.Text.Trim();
            SelectedUserConfig.ProxyPassword = textProxyPassword.Text.Trim();
        }

        private void CheckAvailability()
        {
            var nickAndPasswordPresented = !string.IsNullOrEmpty(textUsername.Text.Trim()) &&
                                           !string.IsNullOrEmpty(textPassword.Text.Trim());

            linkPasswordProtected.Enabled = nickAndPasswordPresented;
            checkAutoLogon.Enabled = nickAndPasswordPresented && !PasswordProtected;
            var proxyValid = !checkUseProxy.Checked ||
                             (checkUseProxy.Checked && !string.IsNullOrEmpty(textProxyAddress.Text.Trim()));
            buttonOk.Enabled = nickAndPasswordPresented && proxyValid;
        }

        private void DetectProxy()
        {
            var defaultWebProxy = WebRequest.DefaultWebProxy;
            var gameProxyUrl = defaultWebProxy.GetProxy(new Uri(AppConsts.GameUrl));
            if (AppConsts.GameUrl.Equals(gameProxyUrl.OriginalString))
            {
                MessageBox.Show(
                    AppConsts.ConfigNoProxyMessage,
                    AppConsts.ConfigNoProxyTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                checkUseProxy.Checked = false;
                textProxyAddress.Enabled = false;
                textProxyAddress.Text = string.Empty;
                textProxyUsername.Enabled = false;
                textProxyPassword.Enabled = false;
            }
            else
            {
                checkUseProxy.Checked = true;
                textProxyAddress.Enabled = true;
                textProxyAddress.Text = gameProxyUrl.Authority;
                textProxyUsername.Enabled = true;
                textProxyPassword.Enabled = true;
            }

            textProxyUsername.Text = string.Empty;
            textProxyPassword.Text = string.Empty;            
        }

        private void Protect()
        {
            if (!PasswordProtected)
            {
                using (var formNewPassword = new NewPassword())
                {
                    if (formNewPassword.ShowDialog() == DialogResult.OK)
                    {
                        SelectedUserConfig.UserPassword = textPassword.Text.Trim();
                        SelectedUserConfig.UserPasswordFlash = textFlashPassword.Text.Trim();
                        SelectedUserConfig.Encrypt(formNewPassword.Password);
                        PasswordProtected = true;
                        linkPasswordProtected.Text = AppConsts.ConfigPasswordNotProtected;
                        checkAutoLogon.Enabled = false;
                        checkAutoLogon.Checked = false;
                    }
                }
            }
            else
            {
                SelectedUserConfig.Decrypt(SelectedUserConfig.ConfigPassword);
                SelectedUserConfig.ConfigHash = string.Empty;
                PasswordProtected = false;
                linkPasswordProtected.Text = AppConsts.ConfigPasswordProtected;
                checkAutoLogon.Enabled = true;
                checkAutoLogon.Checked = false;
            }
        }
    }
}