using System.Windows.Forms;
using ABClient.ABForms;

namespace ABClient.MyForms
{
    internal partial class FormQuick : Form
    {
        internal FormQuick(string nick)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(nick))
            {
                textBoxNick.Text = nick;
            }
        }

        private void TextBoxNickTextChanged(object sender, System.EventArgs e)
        {
            Text = textBoxNick.Text;
        }

        private void FormQuickLoad(object sender, System.EventArgs e)
        {
            toolTip1.SetToolTip(buttonHitSimple, "Обычная нападалка");
            toolTip1.SetToolTip(buttonHitBlood, "Кровавая нападалка");
            toolTip1.SetToolTip(buttonHitUltimate, "Боевая нападалка");
            toolTip1.SetToolTip(buttonHitClosedUltimate, "Закрытая боевая нападалка");
            toolTip1.SetToolTip(buttonClosed, "Закрытая нападалка");
            toolTip1.SetToolTip(buttonFistSimple, "Обычная кулачка");
            toolTip1.SetToolTip(buttonFistClosed, "Закрытая кулачка");
            toolTip1.SetToolTip(buttonFog, "Туман");
            toolTip1.SetToolTip(buttonPoison, "Яд");
            toolTip1.SetToolTip(buttonStrong, "Сильная спина");
            toolTip1.SetToolTip(buttonInvisible, "Невид");
        }

        private void ButtonHitSimpleClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttack(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonHitBloodClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackBlood(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonHitUltimateClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackUltimate(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonHitClosedUltimateClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackClosedUltimate(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonFistSimpleClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackFist(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonFistClosedClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackClosedFist(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonClosedClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackClosed(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonFogClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackFog(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonPoisonClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackPoison(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonStrongClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackStrong(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void ButtonInvisibleClick(object sender, System.EventArgs e)
        {
            FormMain.FastAttackNevidPot(textBoxNick.Text.Trim());
            CheckClose();
        }

        private void CheckClose()
        {
            if (checkBoxClose.Checked)
                Close();
        }

    }
}
