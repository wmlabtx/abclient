namespace ABClient.ABForms
{
    using System;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;

    /// <summary>
    /// Работа с ключом.
    /// </summary>
    internal sealed partial class FormMain
    {
        private static void KeyRestartMessage()
        {
            MessageBox.Show(
                AppConsts.MessageKeyUpdated + Environment.NewLine + AppConsts.MessageClientWillBeRestarted,
                AppVars.AppVersion.ProductShortVersion,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            AppVars.DoPromptExit = false;
            Application.Restart();
        }

        private static void ForceDownloadKey()
        {
            /*
            var wc = new WebClient { Proxy = AppVars.LocalProxy };
            try
            {
                var key = Path.Combine(Application.StartupPath, AppConsts.KeyFile);
                wc.DownloadFile(new Uri(AppConsts.KeyFileUrl), key);
                KeyRestartMessage();
                return;
            }
            catch (WebException exception)
            {
                MessageBox.Show(
                    exception.Message,
                    AppVars.AppVersion.NickProductShortVersion,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            Environment.Exit(0);
            */
        }

        private static void QuiteDownloadKey()
        {
            /*
            var wc = new WebClient { Proxy = AppVars.LocalProxy };
            try
            {
                var keydata = wc.DownloadData(new Uri(AppConsts.KeyFileUrl));
                var keytemp = Path.Combine(Application.StartupPath, AppConsts.KeyFileTemp);
                File.WriteAllBytes(keytemp, keydata);
                var key = Path.Combine(Application.StartupPath, AppConsts.KeyFile);
                if (File.Exists(key))
                {
                    File.Delete(key);
                }

                File.Move(keytemp, key);
            }
            catch (WebException)
            {
            }
             */ 
        }

        private static void DownloadKey()
        {
            /*
            var wc = new WebClient { Proxy = AppVars.LocalProxy };
            wc.DownloadDataCompleted += DownloadKeyCompleted;
            try
            {
                wc.DownloadDataAsync(new Uri(AppConsts.KeyFileUrl), 0);
            }
            catch (WebException exception)
            {
                MessageBox.Show(
                    exception.Message,
                    AppVars.AppVersion.NickProductShortVersion,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            */
        }

        private static void DownloadKeyCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                try
                {
                    var keytemp = Path.Combine(Application.StartupPath, AppConsts.KeyFileTemp);
                    File.WriteAllBytes(keytemp, e.Result);
                    var key = Path.Combine(Application.StartupPath, AppConsts.KeyFile);
                    if (File.Exists(key))
                    {
                        File.Delete(key);
                    }

                    File.Move(keytemp, key);
                    KeyRestartMessage();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        AppVars.AppVersion.NickProductShortVersion,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                if (e.Cancelled)
                {
                    MessageBox.Show(
                        e.Error.Message,
                        AppVars.AppVersion.NickProductShortVersion,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(
                            e.Error.Message,
                            AppVars.AppVersion.NickProductShortVersion,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}