using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ABClient.ABProxy;

namespace ABClient.MyForms
{
    public partial class FormShowCookies : Form
    {
        public FormShowCookies()
        {
            InitializeComponent();
        }

        private void FormShowCookiesLoad(object sender, EventArgs e)
        {
            textBoxCookies.Text = CookiesManager.Obtain("www.neverlands.ru");
            CopyToClipboard();
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyToClipboard()
        {
            try
            {
                Clipboard.SetText(textBoxCookies.Text);
            }
            catch (ExternalException)
            {
            }
        }

        private void ButtonCopyToClipboardClick(object sender, EventArgs e)
        {
            CopyToClipboard();
        }
    }
}
