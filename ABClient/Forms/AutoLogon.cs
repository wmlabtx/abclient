namespace ABClient.Forms
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    internal partial class AutoLogon : Form
    {
        private int CountDown = AppConsts.AutoLogOnCountDown;

        internal AutoLogon(string userName)
        {
            InitializeComponent();

            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }
            
            labelUsername.Text = userName;
            timerCountDown.Start();
        }

        private void TimerCountDown_Tick(object sender, EventArgs e)
        {
            CountDown--;
            if (CountDown == 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                buttonOk.Text = string.Format(
                    CultureInfo.InvariantCulture,
                    AppConsts.AutoLogOnFormat,
                    CountDown);
            }
        }
    }
}