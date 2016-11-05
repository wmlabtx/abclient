namespace ABClient.MyForms
{
    using System;
    using System.Net;
    using System.Windows.Forms;
    using MyProfile;

    internal partial class FormProfile : Form
    {
        private const string ConstNewTitle = "Новый персонаж";
        private const string ConstNoProxyTitle = "Попытка определения прокси";
        private const string ConstNoProxyMessage = "Настройки прокси можно определить только, если они явно прописаны в Internet Explorer";
        private const string ConstPasswordProtected = "Зашифровать пароли (рекомендуется)";
        private const string ConstPasswordNotProtected = "Держать пароли открытыми (не рекомендуется)";
        private const string ConstSave = "Сохранить";

        private readonly string _stringTitle;
        private bool passwordProtected;

        internal FormProfile(UserConfig userConfig)
        {
            InitializeComponent();
            if (userConfig == null)
            {
                _stringTitle = ConstNewTitle;
                SelectedUserConfig = new UserConfig();    
            }
            else
            {
                _stringTitle = userConfig.UserNick;
                SelectedUserConfig = userConfig;
                textUsername.Text = userConfig.UserNick;
                textUserKey.Text = userConfig.UserKey;
                textPassword.Text = userConfig.UserPassword;
                textFlashPassword.Text = userConfig.UserPasswordFlash;
                checkAutoLogon.Checked = userConfig.UserAutoLogon;
                checkUseProxy.Checked = userConfig.DoProxy;
                textProxyAddress.Text = userConfig.ProxyAddress;
                textProxyUsername.Text = userConfig.ProxyUserName;
                textProxyPassword.Text = userConfig.ProxyPassword;
                if (!string.IsNullOrEmpty(userConfig.ConfigHash))
                {
                    passwordProtected = true;
                    linkPasswordProtected.Text = ConstPasswordNotProtected;
                }

                buttonOk.Text = ConstSave;
                CheckAvailability();
            }
        }

        /// <summary>
        /// Выбранная конфигурация
        /// </summary>
        internal UserConfig SelectedUserConfig { get; private set; }

        private void FormProfile_Load(object sender, EventArgs e)
        {
            Text = _stringTitle;
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
            var defaultWebProxy = WebRequest.DefaultWebProxy;
            var gameProxyUrl = defaultWebProxy.GetProxy(new Uri(AppConsts.GameUrl));
            if (AppConsts.GameUrl.Equals(gameProxyUrl.OriginalString))
            {
                MessageBox.Show(
                    ConstNoProxyMessage,
                    ConstNoProxyTitle,
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
            if (!passwordProtected)
            {
                using (var formNewPassword = new FormNewPassword())
                {
                    if (formNewPassword.ShowDialog() == DialogResult.OK)
                    {
                        SelectedUserConfig.UserPassword = textPassword.Text.Trim();
                        SelectedUserConfig.UserPasswordFlash = textFlashPassword.Text.Trim();
                        SelectedUserConfig.Encrypt(formNewPassword.Password);
                        passwordProtected = true;
                        linkPasswordProtected.Text = ConstPasswordNotProtected;
                        checkAutoLogon.Enabled = false;
                        checkAutoLogon.Checked = false;
                    }
                }
            }
            else
            {
                SelectedUserConfig.Decrypt(SelectedUserConfig.ConfigPassword);
                SelectedUserConfig.ConfigHash = string.Empty;
                passwordProtected = false;
                linkPasswordProtected.Text = ConstPasswordProtected;
                checkAutoLogon.Enabled = true;
                checkAutoLogon.Checked = false;
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            SelectedUserConfig.UserNick = textUsername.Text.Trim();
            SelectedUserConfig.UserKey = textUserKey.Text.Trim();
            SelectedUserConfig.UserPassword = textPassword.Text.Trim();
            SelectedUserConfig.UserPasswordFlash = textFlashPassword.Text.Trim();
            SelectedUserConfig.UserAutoLogon = checkAutoLogon.Checked;
            SelectedUserConfig.DoProxy = checkUseProxy.Checked;
            SelectedUserConfig.ProxyAddress = textProxyAddress.Text.Trim();
            SelectedUserConfig.ProxyUserName = textProxyUsername.Text.Trim();
            SelectedUserConfig.ProxyPassword = textProxyPassword.Text.Trim();
        }

        private void CheckAvailability()
        {
            var nickAndPasswordPresented = !string.IsNullOrEmpty(textUsername.Text.Trim()) &&
                                           !string.IsNullOrEmpty(textPassword.Text.Trim());

            linkPasswordProtected.Enabled = nickAndPasswordPresented;
            checkAutoLogon.Enabled = nickAndPasswordPresented && !passwordProtected;
            var proxyValid = !checkUseProxy.Checked ||
                             (checkUseProxy.Checked && !string.IsNullOrEmpty(textProxyAddress.Text.Trim()));
            buttonOk.Enabled = nickAndPasswordPresented && proxyValid;
        }
    }
}