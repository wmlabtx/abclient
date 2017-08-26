[assembly: System.CLSCompliant(true)]
namespace ABClient
{
    using System;
    using System.Net;
    using System.Windows.Forms;
    using ABForms;
    using ABProxy;
    using Properties;


    internal static class Program
    {
        [STAThread]
        internal static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UnhandledExceptionManager.AddHandler();

            ServicePointManager.Expect100Continue = false;

            DataManager.Init();

            var selectedConfig = MyProfile.ConfigSelector.Process();
            if (selectedConfig == null)
            {
                return;
            }

            AppVars.Profile = selectedConfig;
            AppVars.Profile.DoHttpLog = true;

            AppTimerManager.SetAppTimers(AppVars.Profile.AppConfigTimers.ToArray());
            AppVars.AppVersion.AddNick(AppVars.Profile.UserNick);

            using (AppVars.ClearExplorerCacheFormMain = new ClearExplorerCacheForm())
            {
                ExplorerHelper.ClearCache();
                AppVars.ClearExplorerCacheFormMain.ShowDialog();
            }

            AppVars.ClearExplorerCacheFormMain = null;

            AppVars.DoPromptExit = AppVars.Profile.DoPromptExit;
            ChatUsersManager.Load();

            FeatureBrowserEmulation.ChangeMode();

            using (var proxy = new Proxy())
            {
                if (!proxy.Start())
                {
                    MessageBox.Show(
                        Resources.MessageProxyInitError,
                        AppVars.AppVersion.ProductShortVersion,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                AppVars.MainForm = new FormMain();
                Application.Run(AppVars.MainForm);
                AppVars.MainForm = null;
            }

            ChatUsersManager.Save();

            if (string.IsNullOrEmpty(AppVars.AccountError))
                return;

            MessageBox.Show(
                AppVars.AccountError,
                AppVars.AppVersion.NickProductShortVersion,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}