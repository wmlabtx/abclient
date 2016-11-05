namespace ABClient.ABForms
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using Forms;
    using MyHelpers;

    internal sealed partial class FormMain
    {
        internal void UpdateTrafficSafe(long addTraffic)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateTrafficSafe(addTraffic)));
                return;
            }

            try
            {
                LockStat.AcquireWriterLock(5000);
                try
                {
                    AppVars.Profile.Stat.Traffic += addTraffic;
                }
                finally
                {
                    LockStat.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            menuitemStatItem0.Text = string.Concat("Использовано трафика: ", TraficToString(AppVars.Profile.Stat.Traffic));
            UpdateStatString();
        }

        internal void UpdateSavedTrafficSafe(int addSavedTraffic)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateSavedTrafficSafe(addSavedTraffic)));
                return;
            }

            try
            {
                LockStat.AcquireWriterLock(5000);
                try
                {
                    AppVars.Profile.Stat.SavedTraffic += addSavedTraffic;
                }
                finally
                {
                    LockStat.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            menuitemStatItem1.Text = string.Concat("Сэкономлено трафика: ", TraficToString(AppVars.Profile.Stat.SavedTraffic));
            UpdateStatString();
        }

        private static string TraficToString(long size)
        {
            var str = string.Concat(size.ToString(AppVars.Culture), " байт");
            if (size >= 1024)
            {
                var ksize = (double)size / 1024;
                str = string.Concat(ksize.ToString("0.0", AppVars.Culture), " Кбайт");
                if (ksize >= 1024)
                {
                    ksize = ksize / 1024;
                    str = string.Concat(ksize.ToString("0.00", AppVars.Culture), " Мбайт");
                }
            }

            return str;
        }

        private void ChangeShowStat(string indexItem)
        {
            byte item;
            if (!byte.TryParse(indexItem, out item))
            {
                return;
            }

            AppVars.Profile.Stat.Show = item;
            AppVars.Profile.Save();
            UpdateStatString();
        }

        private static void StatReset()
        {
            try
            {
                LockStat.AcquireWriterLock(5000);
                try
                {
                    AppVars.Profile.Stat.LastReset = DateTime.Now.Ticks;
                    AppVars.Profile.Stat.LastUpdateDay = DateTime.Now.DayOfYear;
                    AppVars.Profile.Stat.Traffic = 0;
                    AppVars.Profile.Stat.SavedTraffic = 0;
                    AppVars.Profile.Stat.Drop = string.Empty;
                    AppVars.Profile.Stat.ItemDrop.Clear();
                    AppVars.Profile.Stat.XP = 0;
                    AppVars.Profile.Stat.NV = 0;
                    AppVars.Profile.Stat.FishNV = 0;
                }
                finally
                {
                    LockStat.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        private void UpdateStatString()
        {
            switch (AppVars.Profile.Stat.Show)
            {
                case 0:
                    dropdownCurrentStat.Text = menuitemStatItem0.Text;
                    break;
                case 1:
                    dropdownCurrentStat.Text = menuitemStatItem1.Text;
                    break;
                case 2:
                    dropdownCurrentStat.Text = menuitemStatItem2.Text;
                    break;
                case 3:
                    dropdownCurrentStat.Text = menuitemStatItem3.Text;
                    break;
                case 4:
                    dropdownCurrentStat.Text = menuitemStatItem4.Text;
                    break;
            }

            if (!AppVars.Profile.Stat.Reset || (DateTime.Now.DayOfYear == AppVars.Profile.Stat.LastUpdateDay))
            {
                return;
            }

            StatReset();
            UpdateStat();
        }

        private void UpdateStat()
        {
            UpdateTrafficSafe(0);
            UpdateSavedTrafficSafe(0);
            UpdateXP();
            UpdateNV();
            UpdateFishNV();
        }

        private void UpdateXP()
        {
            menuitemStatItem2.Text = string.Format("Получено опыта: {0}", AppVars.Profile.Stat.XP);
            UpdateStatString();
        }

        private void UpdateNV()
        {
            menuitemStatItem3.Text = string.Format("Выбито с ботов: {0} NV", AppVars.Profile.Stat.NV);
            UpdateStatString();
        }

        private void UpdateFishNV()
        {
            menuitemStatItem4.Text = string.Format("Заработано на рыбалке: {0} NV", AppVars.Profile.Stat.FishNV);
            UpdateStatString();
        }

        private void ShowAndClearStat()
        {
            var sb = new StringBuilder("Статистика за ");
            sb.Append(HelperConverters.TimeIntervalToNow(AppVars.Profile.Stat.LastReset));
            sb.AppendLine(":");
            sb.AppendLine(menuitemStatItem0.Text);
            sb.AppendLine(menuitemStatItem1.Text);
            sb.AppendLine(menuitemStatItem2.Text);
            sb.AppendLine(menuitemStatItem3.Text);
            sb.AppendLine(menuitemStatItem4.Text);
            if (AppVars.Profile.Stat.ItemDrop.Count == 0)
            {
                sb.Append("Вещей не найдено");
            }
            else
            {
                sb.AppendLine("Найдены вещи:");
                for (var index = 0; index < AppVars.Profile.Stat.ItemDrop.Count; index++)
                {
                    sb.Append(AppVars.Profile.Stat.ItemDrop[index].Name);
                    if (AppVars.Profile.Stat.ItemDrop[index].Count > 1)
                    {
                        sb.AppendFormat(" ({0} шт.)", AppVars.Profile.Stat.ItemDrop[index].Count);
                    }

                    sb.AppendLine();
                }
            }

            using (var ff = new FormStatEdit(sb.ToString()))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                StatReset();
                UpdateStat();
            }
        }
    }
}