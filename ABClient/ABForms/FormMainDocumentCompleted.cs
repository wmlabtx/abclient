using System;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        private void DocumentCompleted()
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                        new object[] { "DocumentCompleted()" });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}