namespace ABClient.ABForms
{
    using System.Windows.Forms;

    /// <summary>
    /// Главная форма.
    /// </summary>
    internal sealed partial class FormMain
    {
        private void InitSize()
        {
            if (AppVars.Profile.Window.Width <= 0 ||
                AppVars.Profile.Window.Height <= 0)
            {
                Left = 15;
                Top = 15;
                Width = Screen.PrimaryScreen.WorkingArea.Width - 30;
                Height = Screen.PrimaryScreen.WorkingArea.Height - 30;
            }
            else
            {
                Left = AppVars.Profile.Window.Left;
                Top = AppVars.Profile.Window.Top;
                Width = AppVars.Profile.Window.Width;
                Height = AppVars.Profile.Window.Height;
            }

            WindowState = AppVars.Profile.Window.State;
            if (WindowState != FormWindowState.Minimized)
            {
                _prevWindowState = WindowState;
            }

            Resize += OnFormMainResize;
        }

        private void SaveSize()
        {
            if (WindowState == FormWindowState.Normal)
            {
                AppVars.Profile.Window.Left = Left;
                AppVars.Profile.Window.Top = Top;
                AppVars.Profile.Window.Width = Width;
                AppVars.Profile.Window.Height = Height;
            }

            AppVars.Profile.Window.State = WindowState;
        }

        private void MainFormResize()
        {
            if (WindowState != _prevWindowState && WindowState != FormWindowState.Minimized)
            {
                _prevWindowState = WindowState;
            }

            if (WindowState == FormWindowState.Minimized && AppVars.Profile.DoTray)
            {
                Hide();
                TrayShow();
            }
            else
            {
                SaveSize();
            }
        }
    }
}