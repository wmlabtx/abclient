using System;

namespace ABClient.Forms
{
    using System.Windows.Forms;

    internal partial class FormSostavMain : Form
    {
        public FormSostavMain(string members)
        {
            InitializeComponent();

            var par = members.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            listGroup.BeginUpdate();
            for (var t = 0; t < par.Length; t++)
            {
                var foe = new Foe(par[t]);
                if (!foe.IsValid) continue;
                listGroup.Items.Add(foe);
            }

            listGroup.ManualSort();
            listGroup.EndUpdate();
        }
    }
}
