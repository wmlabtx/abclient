using System;
using System.Windows.Forms;
using ABClient.Properties;

namespace ABClient.MyForms
{
    internal partial class FormNewTab : Form
    {
        public FormNewTab()
        {
            InitializeComponent();
        }

        public string GetAddress()
        {
            var address = textAddress.Text;
            Uri uri;
            if (Uri.TryCreate(address, UriKind.Absolute, out uri))
            {
                return address;
            }

            if (radioPInfo.Checked)
            {
                return AddAddress(Resources.AddressPInfo, address);
            }

            if (radioFightLog.Checked)
            {
                return AddAddress(Resources.AddressFightLog, address);
            }

            if (radioForum.Checked)
            {
                return AddAddress(Resources.AddressForum, address);
            }

            if (radioUrl.Checked)
            {
                return AddAddress("http://", address);
            }

            return address;
        }

        private static string AddAddress(string prefix, string address)
        {
            Uri uri;
            address = prefix + address;
            return Uri.TryCreate(address, UriKind.Absolute, out uri) ? address : null;
        }

        private void FormNewTab_Load(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText(TextDataFormat.Text))
            {
                return;
            }

            textAddress.Text = Clipboard.GetText(TextDataFormat.Text);
        }

        private void textAddress_TextChanged(object sender, EventArgs e)
        {
            var address = textAddress.Text;

            if (address.StartsWith(Resources.AddressPInfo) && address.Length > Resources.AddressPInfo.Length)
            {
                radioPInfo.Checked = true;
                buttonOk.Enabled = true;
                return;
            }

            if (address.StartsWith(Resources.AddressPName) && address.Length > Resources.AddressPName.Length)
            {
                radioPInfo.Checked = true;
                buttonOk.Enabled = true;
                return;
            }

            if (address.StartsWith(Resources.AddressPBots) && address.Length > Resources.AddressPBots.Length)
            {
                radioPInfo.Checked = true;
                buttonOk.Enabled = true;
                return;
            }

            if (address.StartsWith(Resources.AddressFightLog) && address.Length > Resources.AddressFightLog.Length)
            {
                radioFightLog.Checked = true;
                buttonOk.Enabled = true;
                return;
            }

            if (address.StartsWith(Resources.AddressForum) && address.Length >= Resources.AddressForum.Length)
            {
                radioForum.Checked = true;
                buttonOk.Enabled = true;
                return;
            }

            Uri uri;
            if (Uri.TryCreate(address, UriKind.Absolute, out uri))
            {
                radioUrl.Checked = true;
                buttonOk.Enabled = true;
                return;
            }

            long log;
            if (long.TryParse(address, out log))
            {
                radioFightLog.Checked = true;
                buttonOk.Enabled = true;
                return;
            }

            if (!string.IsNullOrEmpty(address) && address.IndexOf(' ') == -1 && address.IndexOf('.') != -1)
            {
                radioUrl.Checked = true;
                buttonOk.Enabled = true;
                return;
            }
            

            if (!string.IsNullOrEmpty(address) && address.IndexOf("  ", StringComparison.Ordinal) == -1 && address.Length < 40)
            {
                radioPInfo.Checked = true;
                buttonOk.Enabled = true;
                return;                
            }

            buttonOk.Enabled = false;
        }
    }
}
