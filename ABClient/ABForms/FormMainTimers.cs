namespace ABClient.ABForms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using MySounds;
    using Properties;

    internal sealed partial class FormMain
    {
        private void UpdateTimers()
        {
        again:
            var arrayAppTimers = AppTimerManager.GetTimers();
            for (var i = 0; i < arrayAppTimers.Length; i++)
            {
                if (DateTime.Now <= arrayAppTimers[i].TriggerTime)
                {
                    continue;
                }

                if (AppVars.FastNeed)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(arrayAppTimers[i].Potion))
                {
                    FastStartSafe(arrayAppTimers[i].Potion, AppVars.Profile.UserNick, arrayAppTimers[i].DrinkCount);
                    if (arrayAppTimers[i].IsRecur)
                    {
                        var nextTimer = new AppTimer
                                            {
                                                TriggerTime =
                                                    arrayAppTimers[i].TriggerTime.AddMinutes(
                                                    arrayAppTimers[i].EveryMinutes),
                                                Description = arrayAppTimers[i].Description,
                                                Potion = arrayAppTimers[i].Potion,
                                                DrinkCount = arrayAppTimers[i].DrinkCount,
                                                IsRecur = true,
                                                EveryMinutes = arrayAppTimers[i].EveryMinutes
                                            };
                        AppTimerManager.RemoveTimerAt(i);
                        AppTimerManager.AddAppTimer(nextTimer);
                    }
                    else
                    {
                        AppTimerManager.RemoveTimerAt(i);    
                    }
                    
                    EventSounds.PlayTimer();
                    ReloadMainFrame();
                    return;
                }

                var destination = arrayAppTimers[i].Destination;
                if (!string.IsNullOrEmpty(destination))
                {
                    AppTimerManager.RemoveTimerAt(i);
                    EventSounds.PlayTimer();
                    MoveTo(destination);
                    return;
                }

                var complect = arrayAppTimers[i].Complect;
                if (!string.IsNullOrEmpty(complect))
                {
                    AppVars.WearComplect = complect;
                    AppTimerManager.RemoveTimerAt(i);
                    EventSounds.PlayTimer();
                    ReloadMainFrame();
                    return;
                }

                AppTimerManager.RemoveTimerAt(i);
                EventSounds.PlayTimer();
                goto again;
            }

            if (dropdownTimers == null) return;
            var isNewList = true;
            if (arrayAppTimers.Length == (dropdownTimers.DropDownItems.Count - 1))
            {
                isNewList = false;
            }
            else
            {
                while (dropdownTimers.DropDownItems.Count > 1)
                {
                    dropdownTimers.DropDownItems.RemoveAt(1);
                }
            }

            if (arrayAppTimers.Length == 0)
            {
                dropdownTimers.Text = "Таймеры...";
            }
            else
            {
                dropdownTimers.Text = arrayAppTimers[0].ToString();
                for (var i = 0; i < arrayAppTimers.Length; i++)
                {
                    if (isNewList)
                    {
                        var item = new ToolStripMenuItem(arrayAppTimers[i].ToString()) { Tag = i };
                        if (arrayAppTimers[i].IsHerb)
                        {
                            item.Image = Resources._15x12_herb;
                            item.ImageAlign = ContentAlignment.MiddleCenter;
                            item.ImageScaling = ToolStripItemImageScaling.None;
                        }

                        item.Click += OnMenuitemWorkingTimerClick;
                        dropdownTimers.DropDownItems.Add(item);
                    }
                    else
                    {
                        dropdownTimers.DropDownItems[i + 1].Text = arrayAppTimers[i].ToString();
                        dropdownTimers.DropDownItems[i + 1].Tag = i;
                        if (arrayAppTimers[i].IsHerb)
                        {
                            dropdownTimers.DropDownItems[i + 1].Image = Resources._15x12_herb;
                            dropdownTimers.DropDownItems[i + 1].ImageAlign = ContentAlignment.MiddleCenter;
                            dropdownTimers.DropDownItems[i + 1].ImageScaling = ToolStripItemImageScaling.None;
                        }
                        else
                        {
                            dropdownTimers.DropDownItems[i + 1].Image = null;
                        }
                    }
                }
            }
        }

        private void RemoveTimer(int index)
        {
            var dr = MessageBox.Show(
                "Удалить таймер ?",
                "Удаление таймера",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            AppTimerManager.RemoveTimerAt(index);
            UpdateTimers();
        }
    }
}