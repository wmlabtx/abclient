namespace ABClient.Forms
{
    using System;
    using System.Windows.Forms;

    internal partial class FormSostav : Form
    {
        public FormSostav(string sostav)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(sostav))
            {
                
                listGroup.BeginUpdate();
                var par = sostav.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < par.Length; i++)
                {
                    var foe = new Foe(par[i]);
                    if (!foe.IsValid) continue;
                    if (!listGroup.Items.Contains(foe))
                    {
                        listGroup.Items.Add(foe);
                    }

                    listGroup.ManualSort();
                    listGroup.EndUpdate();
                }
            }

            comboTriba.SelectedIndex = 0;

            comboMinLevel.BeginUpdate();
            comboMaxLevel.BeginUpdate();
            for (var i = 0; i <= 33; i++)
            {
                comboMinLevel.Items.Add(i);
                comboMaxLevel.Items.Add(i);
            }

            comboMinLevel.EndUpdate();
            comboMaxLevel.EndUpdate();
            comboMinLevel.SelectedIndex = 0;
            comboMaxLevel.SelectedIndex = 33;
        }

        public ListBox.ObjectCollection Members
        {
            get { return listGroup.Items; }
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            var triba = (string)comboTriba.SelectedItem;
            var min = comboMinLevel.SelectedIndex;
            var max = comboMaxLevel.SelectedIndex;
            listGroup.BeginUpdate();
            for (var level=min; level<=max; level++)
            {
                var foe = new Foe(triba, level, "");
                if (!foe.IsValid) continue;
                if (!listGroup.Items.Contains(foe))
                {
                    listGroup.Items.Add(foe);
                }
            }

            listGroup.ManualSort();
            listGroup.EndUpdate();
        }

        private void buttonAddNevid_Click(object sender, EventArgs e)
        {
            listGroup.BeginUpdate();
            var foe = new Foe();
            if (!listGroup.Items.Contains(foe))
            {
                listGroup.Items.Add(foe);
                listGroup.ManualSort();
            }

            listGroup.EndUpdate();
        }

        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            listGroup.BeginUpdate();
            while (listGroup.SelectedItems.Count > 0)
            {
                listGroup.Items.Remove(listGroup.SelectedItem);
            }

            listGroup.EndUpdate();
        }

        private void comboMinLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMaxLevel.SelectedIndex = ((ComboBox) sender).SelectedIndex;
        }
    }
}
