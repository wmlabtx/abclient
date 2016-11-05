namespace ABClient.Forms
{
    using System.Windows.Forms;

    internal partial class FormPromptExit : Form
    {
        private byte timeout = 10;

        public bool SkipPromptExit
        {
            get { return checkboxSkipPromptExit.Checked; }
        }

        public FormPromptExit()
        {
            InitializeComponent();
            timerExit.Start();
        }

        private void timerExit_Tick(object sender, System.EventArgs e)
        {
            buttonOk.Text = "Да (" + timeout + " сек до выхода)";
            timeout--;
            if (timeout != 0)
            {
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
