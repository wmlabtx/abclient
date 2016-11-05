namespace ABClient.MyForms
{
    using System.Windows.Forms;

    internal partial class FormAutoLogon : Form
    {
        private const int ConstCountDown = 3;
        private const string ConstAutoEnterOneFormat = "Автовход через ";
        private const string ConstAutoEnterTwoFormat = " сек";

        private int countDown = ConstCountDown;

        internal FormAutoLogon(string userName)
        {
            InitializeComponent();
            labelUsername.Text = userName;
            timerCountDown.Start();
        }

        private void TimerCountDown_Tick(object sender, System.EventArgs e)
        {
            countDown--;
            if (countDown == 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                buttonOk.Text = ConstAutoEnterOneFormat + countDown + ConstAutoEnterTwoFormat;    
            }
        }
    }
}
