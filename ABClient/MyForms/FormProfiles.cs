namespace ABClient.MyForms
{
    using System;
    using System.Windows.Forms;
    using MyProfile;

    internal partial class FormProfiles : Form
    {
        private const string ConstLastLogOn = "Последний заход в игру: ";

        internal FormProfiles(UserConfig[] arrayConfig)
        {
            if (arrayConfig == null || arrayConfig.Length == 0)
            {
                throw new ArgumentNullException("arrayConfig");
            }

            InitializeComponent();
            comboConfigs.Items.AddRange(arrayConfig);
            comboConfigs.SelectedIndex = 0;
        }

        /// <summary>
        /// Выбранная конфигурация
        /// </summary>
        internal UserConfig SelectedUserConfig { get; private set; }

        private void ComboConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedUserConfig = (UserConfig) comboConfigs.Items[comboConfigs.SelectedIndex];
            labelLastLogOn.Text = ConstLastLogOn + SelectedUserConfig.HumanFormatConfigLastSaved();
        }

        private void LinkCreateNewProfile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newConfig = ConfigSelector.CreateNewConfig();
            if (newConfig == null)
            {
                return;
            }

            SelectedUserConfig = newConfig;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LinkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var editedConfig = ConfigSelector.EditExistingConfig(SelectedUserConfig);
            if (editedConfig == null)
            {
                return;
            }

            comboConfigs.Items[comboConfigs.SelectedIndex] = editedConfig;
            comboConfigs.Refresh();
        }
    }
}
