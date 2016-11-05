namespace ABClient.ABForms
{
    using System;
    using System.Windows.Forms;

    internal partial class FormSettingsAutoCut : Form
    {
        internal FormSettingsAutoCut()
        {
            InitializeComponent();

            for (var i = 0; i < AppVars.Profile.HerbsAutoCut.Count; i++)
            {
                var herbName = AppVars.Profile.HerbsAutoCut[i];
                for (var j = 0; j < listViewHerbs.Items.Count; j++)
                {
                    if (!listViewHerbs.Items[j].Text.Equals(herbName, StringComparison.OrdinalIgnoreCase)) continue;
                    listViewHerbs.Items[j].Checked = true;
                    break;
                }
            }

            checkDoAutoCutWriteChat.Checked = AppVars.Profile.DoAutoCutWriteChat;
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < listViewHerbs.Items.Count; i++)
            {
                listViewHerbs.Items[i].Checked = true;
            }
        }

        private void buttonUnselectAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < listViewHerbs.Items.Count; i++)
            {
                listViewHerbs.Items[i].Checked = false;
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            AppVars.Profile.HerbsAutoCut.Clear();
            for (var i = 0; i < listViewHerbs.Items.Count; i++)
            {
                if (listViewHerbs.Items[i].Checked)
                {
                    AppVars.Profile.HerbsAutoCut.Add(listViewHerbs.Items[i].Text);
                }
            }

            AppVars.Profile.DoAutoCutWriteChat = checkDoAutoCutWriteChat.Checked;
            AppVars.Profile.Save();
            Close();
        }
    }
}