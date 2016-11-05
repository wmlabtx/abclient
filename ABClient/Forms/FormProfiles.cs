namespace ABClient.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Profile;

    internal partial class FormProfiles : Form
    {
        internal FormProfiles(ICollection<Config> arrayConfig)
        {
            InitializeComponent();

            if (arrayConfig == null || arrayConfig.Count == 0)
            {
                throw new ArgumentNullException("arrayConfig");
            }

            var array = new Config[arrayConfig.Count];
            arrayConfig.CopyTo(array, 0);
            comboConfigs.Items.AddRange(array);
            comboConfigs.SelectedIndex = 0;
        }

        internal Config SelectedUserConfig { get; private set; }

        private void ComboConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedUserConfig = (Config) comboConfigs.Items[comboConfigs.SelectedIndex];
            labelLastLogOn.Text = AppConsts.ConfigLastLogOn + SelectedUserConfig.HumanFormatConfigLastSaved();
        }

        private void LinkCreateNewProfile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateNewProfile();
        }

        private void LinkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var editedConfig = Manager.EditExistingConfig(SelectedUserConfig);
            if (editedConfig == null)
            {
                return;
            }

            comboConfigs.Items[comboConfigs.SelectedIndex] = editedConfig;
            comboConfigs.Refresh();
        }

        private void CreateNewProfile()
        {
            EditProfile();
        }

        private void EditProfile()
        {
            var newConfig = Manager.CreateNewConfig();
            if (newConfig == null)
            {
                return;
            }

            SelectedUserConfig = newConfig;
            DialogResult = DialogResult.OK;
            Close();            
        }
    }
}