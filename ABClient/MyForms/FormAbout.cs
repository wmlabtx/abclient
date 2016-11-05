namespace ABClient.MyForms
{
    using System.Windows.Forms;

    internal partial class FormAbout : Form
    {
        internal FormAbout()
        {
            InitializeComponent();
        }

        private void FormAboutLoad(object sender, System.EventArgs e)
        {
            labelExpired.Text = string.Format("Лицензия до: {0}", AppVars.LicenceExpired.ToString("d", AppVars.Culture));
        }
    }
}