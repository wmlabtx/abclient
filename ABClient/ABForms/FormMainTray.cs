namespace ABClient.ABForms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Forms;

    internal sealed partial class FormMain
    {
        private int trayFlashFrame;
        private readonly Icon[] trayIcons = new Icon[] { null, null };
        private FormCode m_fc;
        private DateTime trayDigitsWait = DateTime.Now;

        internal bool TrayIsDigitsWaitTooLong()
        {
            if (trayIcon.Text.IndexOf("Ввод цифр", StringComparison.OrdinalIgnoreCase) == -1)
            {
                return false;
            }

            return DateTime.Now.Subtract(trayDigitsWait).TotalMinutes > 1;
        }

        private void TrayShow()
        {
            for (var i = 0; i < 2; i++)
            {
                if (trayIcons[i] != null) continue;
                var objBitmap = new Bitmap(trayImages.Images[i]);
                trayIcons[i] = Icon.FromHandle(objBitmap.GetHicon());
            }

            trayIcon.Text = AppVars.Profile.UserNick;
            TrayShowFrame(0);
            trayIcon.Visible = true;
        }

        private void TrayShowFrame(int frame)
        {
            trayIcon.Icon = trayIcons[frame];
        }

        private void TrayFlash(string message)
        {
            if (timerTray.Enabled || !trayIcon.Visible)
            {
                return;
            }

            if (message.Equals("Ввод цифр", StringComparison.OrdinalIgnoreCase))
            {
                trayDigitsWait = DateTime.Now;
            }

            trayIcon.Text = AppVars.Profile.UserNick + ": " + message;
            trayFlashFrame = 0;
            timerTray.Start();
        }

        private void TrayIconTick()
        {
            trayFlashFrame = 1 - trayFlashFrame;
            TrayShowFrame(trayFlashFrame);
        }

        private void TrayIconDoubleClick()
        {
            if (trayIcon.Text.IndexOf("Ввод цифр", StringComparison.OrdinalIgnoreCase) != -1)
            {
                if (m_fc != null)
                {
                    return;
                }

                m_fc = new FormCode();
                m_fc.Location =
                    new Point(
                        Screen.PrimaryScreen.WorkingArea.Width - m_fc.Width - 5,
                        Screen.PrimaryScreen.WorkingArea.Height - m_fc.Height - 5);
                m_fc.ShowDialog();
                var dr = m_fc.DialogResult;
                AppVars.GuamodCode = m_fc.Code;
                m_fc.Dispose();
                m_fc = null;
                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(AppVars.FightLink))
                    {
                        AppVars.FightLink = "????";
                    }
                    else
                    {
                        AppVars.Autoboi = AutoboiState.AutoboiOn;
                    }

                    AppVars.FightLink = AppVars.FightLink.Replace("????", AppVars.GuamodCode);

                    timerTray.Stop();
                    trayIcon.Text = AppVars.Profile.UserNick;
                    TrayShowFrame(0);
                }

                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }

            timerTray.Stop();
            
            Show();
            WindowState = _prevWindowState;
            trayIcon.Visible = false;
        }
    }
}