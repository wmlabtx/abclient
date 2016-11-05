namespace ABClient.ABForms
{
    using System;
    using MyForms;

    internal sealed partial class FormMain
    {
        private void SettingsGeneral()
        {
            using (var formSettingsGeneral = new FormSettingsGeneral())
            {
                if (formSettingsGeneral.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                AppVars.AdvArray = null;
                buttonAutoAnswer.Checked = AppVars.Profile.DoAutoAnswer;
                buttonSilence.Checked = !AppVars.Profile.Sound.Enabled;
                Text = AppVars.AppVersion.NickProductShortVersion;

                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                            new object[] { });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        private void SettingsAb()
        {
            using (var formSettingsAb = new Lez.FormSettingsAb(AppVars.Profile.LezGroups))
            {
                if (formSettingsAb.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                try
                {
                    ChangeAutoboiState(AppVars.Profile.LezDoAutoboi ? AutoboiState.AutoboiOn : AutoboiState.AutoboiOff);
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                            new object[] { });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }
    }
}