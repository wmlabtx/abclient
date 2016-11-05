using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ABClient.MyForms;
using ABClient.MyProfile;
using View = System.Windows.Forms.View;

namespace ABClient.Lez
{
    public partial class FormSettingsAb : Form
    {
        private readonly List<LezBotsGroup> _lezGroups;
        private readonly BindingSource _bs = new BindingSource();

        public FormSettingsAb(List<LezBotsGroup> lezBotsGroupCollection)
        {
            InitializeComponent();
            
            _lezGroups = (List<LezBotsGroup>)Helpers.Misc.DeepClone(lezBotsGroupCollection);
            _bs.DataSource = _lezGroups;

            comboNewGroup.DataSource = LezBotsClassCollection.ListForComboBox();
            comboNewGroup.DisplayMember = "Plural";
        }

        private void FormSettingsAb_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void Apply()
        {
            var group = (LezBotsGroup)listGroups.SelectedItem;
            SaveGroup(group);
            SaveSettings();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            Apply();
            Close();
        }

        private void LoadSettings()
        {
            listGroups.DataSource = _bs;
            listGroups.SelectedIndex = _bs.Count - 1;            

            checkDoAutoboi.Checked = AppVars.Profile.LezDoAutoboi;
            checkDoWaitHp.Checked = linkWaitHp.Enabled = AppVars.Profile.LezDoWaitHp;
            checkDoWaitMa.Checked = linkWaitMa.Enabled = AppVars.Profile.LezDoWaitMa;
            SetWaitHp(AppVars.Profile.LezWaitHp);
            SetWaitMa(AppVars.Profile.LezWaitMa);
            checkDoDrinkHp.Checked = linkDrinkHp.Enabled = AppVars.Profile.LezDoDrinkHp;
            checkDoDrinkMa.Checked = linkDrinkMa.Enabled = AppVars.Profile.LezDoDrinkMa;
            SetDrinkHp(AppVars.Profile.LezDrinkHp);
            SetDrinkMa(AppVars.Profile.LezDrinkMa);
            checkDoWinTimeout.Checked = AppVars.Profile.LezDoWinTimeout;

            switch (AppVars.Profile.LezSay)
            {
                case LezSayType.No:
                    radioSayNo.Checked = true;
                    break;

                case LezSayType.Chat:
                    radioSayChat.Checked = true;
                    break;

                case LezSayType.Clan:
                    radioSayClan.Checked = true;
                    break;

                case LezSayType.Pair:
                    radioSayPair.Checked = true;
                    break;
            }

            SetNewGroupLevel(0);

            var group = (LezBotsGroup)listGroups.SelectedItem;
            LoadGroup(group);
        }

        private void SaveSettings()
        {
            AppVars.Profile.LezDoAutoboi = checkDoAutoboi.Checked;
            AppVars.Profile.LezDoWaitHp = checkDoWaitHp.Checked;
            AppVars.Profile.LezDoWaitMa = checkDoWaitMa.Checked;
            AppVars.Profile.LezWaitHp = (int)linkWaitHp.Tag;
            AppVars.Profile.LezWaitMa = (int)linkWaitMa.Tag;
            AppVars.Profile.LezDoDrinkHp = checkDoDrinkHp.Checked;
            AppVars.Profile.LezDoDrinkMa = checkDoDrinkMa.Checked;
            AppVars.Profile.LezDrinkHp = (int)linkDrinkHp.Tag;
            AppVars.Profile.LezDrinkMa = (int)linkDrinkMa.Tag;
            AppVars.Profile.LezDoWinTimeout = checkDoWinTimeout.Checked;

            if (radioSayNo.Checked)
                AppVars.Profile.LezSay = LezSayType.No;

            if (radioSayChat.Checked)
                AppVars.Profile.LezSay = LezSayType.Chat;

            if (radioSayClan.Checked)
                AppVars.Profile.LezSay = LezSayType.Clan;

            if (radioSayPair.Checked)
                AppVars.Profile.LezSay = LezSayType.Pair;

            AppVars.Profile.LezGroups = (List<LezBotsGroup>)Helpers.Misc.DeepClone(_lezGroups);
        }

        private void LoadGroup(LezBotsGroup group)
        {
            checkDoRestoreHp.Checked = linkRestoreHp.Enabled = group.DoRestoreHp;
            checkDoRestoreMa.Checked = linkRestoreMa.Enabled = group.DoRestoreMa;
            SetRestoreHp(group.RestoreHp);
            SetRestoreMa(group.RestoreMa);
            checkDoAbilBlocks.Checked = group.DoAbilBlocks;
            checkDoAbilHits.Checked = group.DoAbilHits;
            checkDoMagHits.Checked = linkMagHits.Enabled = group.DoMagHits;
            SetMagHits(group.MagHits);
            checkDoMagBlocks.Checked = group.DoMagBlocks;
            checkDoHits.Checked = group.DoHits;
            checkDoBlocks.Checked = group.DoBlocks;
            checkDoMiscAbils.Checked = group.DoMiscAbils;

            PopulateSpells(listSpellsHits, group.SpellsHits, LezSpellCollection.Hits);
            PopulateSpells(listSpellsBlocks, group.SpellsBlocks, LezSpellCollection.Blocks);
            PopulateSpells(listSpellsRestoreHp, group.SpellsRestoreHp, LezSpellCollection.RestoreHp);
            PopulateSpells(listSpellsRestoreMa, group.SpellsRestoreMa, LezSpellCollection.RestoreMa);
            PopulateSpells(listSpellsMisc, group.SpellsMisc, LezSpellCollection.Misc);

            checkDoStopNow.Checked = group.DoStopNow;
            checkDoStopLowHp.Checked = linkStopLowHp.Enabled = group.DoStopLowHp;
            checkDoStopLowMa.Checked = linkStopLowMa.Enabled = group.DoStopLowMa;
            SetStopLowHp(group.StopLowHp);
            SetStopLowMa(group.StopLowMa);
            checkDoExit.Checked = group.DoExit;
            checkDoExitRisky.Checked = group.DoExitRisky;
        }

        private void SaveGroup(LezBotsGroup group)
        {
            group.DoRestoreHp = checkDoRestoreHp.Checked;
            group.DoRestoreMa = checkDoRestoreMa.Checked;
            group.RestoreHp = (int) linkRestoreHp.Tag;
            group.RestoreMa = (int) linkRestoreMa.Tag;
            group.DoAbilBlocks = checkDoAbilBlocks.Checked;
            group.DoAbilHits = checkDoAbilHits.Checked;
            group.DoMagHits = checkDoMagHits.Checked;
            group.DoMagBlocks = checkDoMagBlocks.Checked;
            group.MagHits = (int)linkMagHits.Tag;
            group.DoHits = checkDoHits.Checked;
            group.DoBlocks = checkDoBlocks.Checked;
            group.DoMiscAbils = checkDoMiscAbils.Checked;

            AcquireSpells(listSpellsHits, out group.SpellsHits);
            AcquireSpells(listSpellsBlocks, out group.SpellsBlocks);
            AcquireSpells(listSpellsRestoreHp, out group.SpellsRestoreHp);
            AcquireSpells(listSpellsRestoreMa, out group.SpellsRestoreMa);
            AcquireSpells(listSpellsMisc, out group.SpellsMisc);

            group.DoStopNow = checkDoStopNow.Checked;
            group.DoStopLowHp = checkDoStopLowHp.Checked;
            group.DoStopLowMa = checkDoStopLowMa.Checked;
            group.StopLowHp = (int)linkStopLowHp.Tag;
            group.StopLowMa = (int)linkStopLowMa.Tag;
            group.DoExit = checkDoExit.Checked;
            group.DoExitRisky = checkDoExitRisky.Checked;
        }

        private void listGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cursorGroup = (LezBotsGroup)_bs[listGroups.SelectedIndex];
            buttonDeleteGroup.Enabled = (cursorGroup.Id != 001 || cursorGroup.MinimalLevel != 0);
            var group = (LezBotsGroup) listGroups.SelectedItem;
            LoadGroup(group);
        }

        private void tabControlGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = tabControlGeneral.SelectedIndex;
            string title;
            switch (index)
            {
                case 0:
                    title = "Общие настройки";
                    break;
                case 1:
                    title = "Настройки групп автобоя";
                    break;
                default:
                    var groupName = listGroups.SelectedItem.ToString();
                    title = string.Format($"Настройки группы \"{groupName}\"");
                    break;
            }

            Text = title;
        }

        // Общие

        private void checkDoWaitHp_CheckedChanged(object sender, EventArgs e)
        {
            linkWaitHp.Enabled = checkDoWaitHp.Checked;
        }

        private void checkDoWaitMa_CheckedChanged(object sender, EventArgs e)
        {
            linkWaitMa.Enabled = checkDoWaitMa.Checked;
        }

        private void checkDoDrinkHp_CheckedChanged(object sender, EventArgs e)
        {
            linkDrinkHp.Enabled = checkDoDrinkHp.Checked;
        }

        private void checkDoDrinkMa_CheckedChanged(object sender, EventArgs e)
        {
            linkDrinkMa.Enabled = checkDoDrinkMa.Checked;
        }

        private void SetWaitHp(int val)
        {
            linkWaitHp.Tag = val;
            linkWaitHp.Text = string.Format($"Пока здоровье не станет {val}%");
        }

        private void SetWaitMa(int val)
        {
            linkWaitMa.Tag = val;
            linkWaitMa.Text = string.Format($"Пока мана не станет {val}%");
        }

        private void SetDrinkHp(int val)
        {
            linkDrinkHp.Tag = val;
            linkDrinkHp.Text = string.Format($"Если здоровье упало ниже {val}%");
        }

        private void SetDrinkMa(int val)
        {
            linkDrinkMa.Tag = val;
            linkDrinkMa.Text = string.Format($"Если мана упала ниже {val}%");
        }

        private void linkWaitHp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Здоровье", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetWaitHp(ff.Val);
            }
        }

        private void linkWaitMa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Мана", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetWaitMa(ff.Val);
            }
        }

        private void linkDrinkHp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Здоровье", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetDrinkHp(ff.Val);
            }
        }

        private void linkDrinkMa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Мана", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetDrinkMa(ff.Val);
            }
        }

        // Группы

        private void SetNewGroupLevel(int level)
        {
            linkNewGroupLevel.Tag = level;
            linkNewGroupLevel.Text = string.Format($"с {level}-го уровня");
        }

        private void linkNewGroupLevel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Уровень", (int)((LinkLabel)sender).Tag, 0, 33))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetNewGroupLevel(ff.Val);
            }
        }

        private void buttonCreateGroup_Click(object sender, EventArgs e)
        {
            var group = new LezBotsGroup(001, 0);
            var id = ((LezBotsClass) comboNewGroup.SelectedItem).Id;
            var minimalLevel = (int) linkNewGroupLevel.Tag;
            group.Change(id, minimalLevel);
            for (var i = 0; i < _bs.Count; i++)
            {
                var cursorGroup = (LezBotsGroup) _bs[i];
                var result = group.CompareTo(cursorGroup);
                if (result == -1)
                {
                    _bs.Insert(i, group);
                    break;
                }

                if (result == 0)
                    return;
            }
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            var cursorGroup = (LezBotsGroup)_bs[listGroups.SelectedIndex];
            if (cursorGroup.Id != 001 || cursorGroup.MinimalLevel != 0)
            {
                _bs.RemoveAt(listGroups.SelectedIndex);
                listGroups.SelectedIndex = _bs.Count - 1;
                cursorGroup = (LezBotsGroup)_bs[listGroups.SelectedIndex];
                LoadGroup(cursorGroup);
            }
        }

        // Ротация

        private void SetRestoreHp(int val)
        {
            linkRestoreHp.Tag = val;
            linkRestoreHp.Text = string.Format($"Восстанавливать жизнь, если она упала до {val}% или ниже");
        }

        private void SetRestoreMa(int val)
        {
            linkRestoreMa.Tag = val;
            linkRestoreMa.Text = string.Format($"Восстанавливать ману, если она упала до {val}% или ниже");
        }

        private void SetMagHits(int val)
        {
            linkMagHits.Tag = val;
            linkMagHits.Text = string.Format($"Ставить магические удары по {val}");
        }

        private void checkDoRestoreHp_CheckedChanged(object sender, EventArgs e)
        {
            linkRestoreHp.Enabled = checkDoRestoreHp.Checked;
        }

        private void checkDoRestoreMa_CheckedChanged(object sender, EventArgs e)
        {
            linkRestoreMa.Enabled = checkDoRestoreMa.Checked;
        }

        private void checkDoMagHits_CheckedChanged(object sender, EventArgs e)
        {
            linkMagHits.Enabled = checkDoMagHits.Checked;
        }

        private void linkRestoreHp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Здоровье", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetRestoreHp(ff.Val);
            }
        }

        private void linkRestoreMa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Мана", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetRestoreMa(ff.Val);
            }
        }

        private void linkMagHits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Удар", (int)((LinkLabel)sender).Tag, 5, 1000))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetMagHits(ff.Val);
            }
        }

        // Абилки

        private static void PopulateSpells(ListView listView, int[] actualList, IEnumerable<int> fullList)
        {
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;

            listView.Columns.Clear();
            listView.Columns.Add("Код");
            listView.Columns.Add("Название");
            listView.Columns.Add("ОД");
            listView.Columns.Add("Мана");
            listView.Columns.Add("Тип");

            listView.Items.Clear();

            foreach (var spellId in fullList)
            {
                var item =
                    new ListViewItem(new[]
                    {
                        spellId.ToString(),
                        LezSpellCollection.Spells[spellId].Name,
                        LezSpellCollection.Od[spellId].ToString(),
                        LezSpellCollection.PosMana[spellId].ToString(),
                        LezSpellCollection.PosType[spellId].ToString()
                    }) {Checked = Array.IndexOf(actualList, spellId) > -1};

                listView.Items.Add(item);
            }

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            /*
            foreach (var spell in LezSpellCollection.Spells)
            {
                if (string.IsNullOrEmpty(spell.Value.Name))
                    continue;

                listSpellsHits.Items.Add(new ListViewItem(new[] { spell.Key.ToString(), spell.Value.Name }));
            }
            */
        }

        private static void AcquireSpells(ListView listView, out int[] actualList)
        {
            var list = new List<int>();
            foreach (var item in listView.Items)
            {
                if (((ListViewItem)item).Checked)
                {
                    list.Add(Convert.ToInt32(((ListViewItem)item).SubItems[0].Text));
                }
            }

            actualList = list.ToArray();
        }

        // Останов боя

        private void SetStopLowHp(int val)
        {
            linkStopLowHp.Tag = val;
            linkStopLowHp.Text = string.Format($"Остановить бой, если жизнь упала до {val}% или ниже");
        }

        private void SetStopLowMa(int val)
        {
            linkStopLowMa.Tag = val;
            linkStopLowMa.Text = string.Format($"Остановить бой, если мана упала до {val}% или ниже");
        }

        private void checkDoStopLowHp_CheckedChanged(object sender, EventArgs e)
        {
            linkStopLowHp.Enabled = checkDoStopLowHp.Checked;
        }

        private void checkDoStopLowMa_CheckedChanged(object sender, EventArgs e)
        {
            linkStopLowMa.Enabled = checkDoStopLowMa.Checked;
        }

        private void linkStopLowHp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Здоровье", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetStopLowHp(ff.Val);
            }
        }

        private void linkStopLowMa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Мана", (int)((LinkLabel)sender).Tag, 0, 100))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                    return;

                SetStopLowMa(ff.Val);
            }
        }

        private void buttonFullHp_Click(object sender, EventArgs e)
        {
            checkDoRestoreHp.Checked = true;
            SetRestoreHp(100);
        }

        private void buttonFullMa_Click(object sender, EventArgs e)
        {
            checkDoRestoreMa.Checked = true;
            SetRestoreMa(100);
        }
    }
}
