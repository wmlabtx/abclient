using System;
using System.Windows.Forms;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        private void MiAutoAttackClick(object sender, EventArgs e)
        {
            int tag;
            if (!int.TryParse((string)((ToolStripMenuItem) sender).Tag, out tag))
            {
                tag = 0;
            }

            AppVars.AutoAttackToolId = tag;
            buttonAutoAttack.Text = ((ToolStripMenuItem) sender).Text;
            buttonAutoAttack.ToolTipText = ((ToolStripMenuItem)sender).ToolTipText;
            buttonAutoAttack.Image = ((ToolStripMenuItem)sender).Image;
            if (tag != 0)
            {
                buttonWalkers.Checked = true;
                ButtonWalkers(true);                
            }
        }
    }
}
