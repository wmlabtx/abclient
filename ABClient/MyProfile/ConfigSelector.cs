namespace ABClient.MyProfile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;
    using System.Windows.Forms;
    using MyForms;

    internal static class ConfigSelector
    {
        private const string ConstConfigsLoadErrorTitle = "Ошибка работы с профайлами";

        /// <summary>
        /// Возвращает выбранный файл конфигурации либо null
        /// </summary>
        /// <returns>выбранный файл конфигурации либо null</returns>
        internal static UserConfig Process()
        {
            try
            {
                // Получаем список профайлов в текущей папке клиента
                
                var directoryInfo = new DirectoryInfo(Application.StartupPath);
                var fileList = directoryInfo.GetFiles("*" + AppConsts.ProfileExtension, SearchOption.TopDirectoryOnly);
                if (fileList.Length == 0)
                {
                    return CreateNewConfig();
                }

                var listProfiles = new List<UserConfig>(fileList.Length);
                foreach (var fileInfo in fileList)
                {
                    var currentConfig = new UserConfig();
                    if (!currentConfig.Load(fileInfo.FullName))
                    {
                        continue;
                    }

                    listProfiles.Add(currentConfig);
                }

                if (listProfiles.Count == 0)
                {
                    return CreateNewConfig();
                }

                listProfiles.Sort();

                // Проверка на автовход
                 
                var firstProfile = listProfiles[0];
                if ((firstProfile.UserAutoLogon &&
                    !string.IsNullOrEmpty(firstProfile.UserNick)) &&
                    !string.IsNullOrEmpty(firstProfile.UserPassword))
                {
                    using (var ff = new FormAutoLogon(firstProfile.UserNick))
                    {
                        if (ff.ShowDialog() == DialogResult.OK)
                        {
                            return firstProfile;
                        }
                    }
                }

                var selectedUserConfig = SelectExistingConfig(listProfiles.ToArray());
                return selectedUserConfig == null ? null : TryDecrypt(selectedUserConfig);
            }
            catch (ArgumentNullException ex)
            {
                ConfigsLoadError(ex.Message);
            }
            catch (SecurityException ex)
            {
                ConfigsLoadError(ex.Message);
            }
            catch (ArgumentException ex)
            {
                ConfigsLoadError(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                ConfigsLoadError(ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                ConfigsLoadError(ex.Message);
            }
            catch (IOException ex)
            {
                ConfigsLoadError(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Возвращает созданный профайл или null
        /// </summary>
        /// <returns>созданный профайл или null</returns>
        internal static UserConfig CreateNewConfig()
        {
            using (var formProfile = new FormProfile(null))
            {
                if (formProfile.ShowDialog() == DialogResult.OK)
                {
                    formProfile.SelectedUserConfig.Save();
                    return formProfile.SelectedUserConfig;
                }

                return null;
            }
        }

        /// <summary>
        /// Возвращает расшифрованный файл или null, если это невозможно
        /// </summary>
        /// <param name="userConfig"></param>
        /// <returns></returns>
        internal static UserConfig TryDecrypt(UserConfig userConfig)
        {
            if (userConfig == null)
            {
                throw new ArgumentNullException("userConfig");
            }

            if (string.IsNullOrEmpty(userConfig.UserPassword))
            {
                using (var formAskPassword = new FormAskPassword(userConfig.ConfigHash))
                {
                    if (formAskPassword.ShowDialog() != DialogResult.OK)
                    {
                        return null;
                    }

                    userConfig.Decrypt(formAskPassword.Password);
                }
            }

            return userConfig;
        }

        /// <summary>
        /// Возвращает исправленный профайл или null. Может потребовать ввести пароль.
        /// </summary>
        /// <param name="userConfig">Профайл для редактирования</param>
        /// <returns>исправленный профайл или null</returns>
        internal static UserConfig EditExistingConfig(UserConfig userConfig)
        {
            if (userConfig == null)
            {
                throw new ArgumentNullException("userConfig");
            }

            userConfig = TryDecrypt(userConfig);
            if (userConfig == null)
            {
                return null;
            }

            using (var formProfile = new FormProfile(userConfig))
            {
                if (formProfile.ShowDialog() == DialogResult.OK)
                {
                    formProfile.SelectedUserConfig.Save();
                    return formProfile.SelectedUserConfig;
                }

                return null;
            }
        }

        private static void ConfigsLoadError(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }

            MessageBox.Show(
                message,
                ConstConfigsLoadErrorTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static UserConfig SelectExistingConfig(UserConfig[] arrayConfig)
        {
            using (var formProfiles = new FormProfiles(arrayConfig))
            {
                if (formProfiles.ShowDialog() == DialogResult.OK)
                {
                    return formProfiles.SelectedUserConfig;
                }
            }

            return null;
        }
    }
}