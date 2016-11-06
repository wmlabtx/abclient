namespace ABClient.ABForms
{
    using System;
    using System.Net;
    using System.Globalization;
    using System.Web;
    using System.Text;
    using System.IO;
    using Helpers;
    using System.Threading;

    internal sealed partial class FormMain
    {
       // private static int _herbsCounter;

        internal static void HerbsList(string list)
        {
            if (list == null)
            {   
                return;
            }

            var updatedInTicks = DateTime.Now.Subtract(AppVars.Profile.ServDiff).Ticks;
            if (AppVars.Profile.HerbCells.ContainsKey(AppVars.Profile.MapLocation))
            {
                AppVars.Profile.HerbCells[AppVars.Profile.MapLocation].Herbs = list;
                AppVars.Profile.HerbCells[AppVars.Profile.MapLocation].UpdatedInTicks = updatedInTicks;
            }
            else
            {
                var herbcell = new HerbCell
                                   {
                                       RegNum = AppVars.Profile.MapLocation,
                                       Herbs = list,
                                       UpdatedInTicks = updatedInTicks
                                   };
                AppVars.Profile.HerbCells.Add(AppVars.Profile.MapLocation, herbcell);
            }
        }

        internal static bool IsHerbAutoCut(string herb)
        {
            /*
            if (herb == null)
            {
                return false;
            }

            if (!AppVars.DoHerbAutoCut)
            {
                return false;
            }

            for (var i = 0; i < AppVars.Profile.HerbsAutoCut.Count; i++)
            {
                if (AppVars.Profile.HerbsAutoCut[i].Equals(herb, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
             */ 

            return false;
        }

        internal static void HerbCut(string name)
        {
            /*
            if (AppVars.Profile.DoAutoCutWriteChat)
            {
                var message = string.Format(@"{0}: Автоспил травы ""{1}""...", AppVars.AppVersion.ProductShortVersion, name);
                Chat.AddAnswer(message);
            }

            var colormessage = string.Format("Автоспил травы &laquo;<b>{0}</b>&raquo;...", name);
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                        new object[] { colormessage });
                }
            }
            catch (InvalidOperationException)
            {
            }

            TraceCut(name);
             */ 
        }

        internal static bool DoHerbAutoCut()
        {
            /*
            if (Key.KeyFile.IsPay() && AppVars.DoHerbAutoCut)
            {
                CheckTied();
                return true;
            }
             */ 

            return false;
        }

        internal static void TraceCut(string herb)
        {
            var colormessage = string.Format("Трава &laquo;<b>{0}</b>&raquo; спилена. ", herb);
            var curTime = DateTime.Now.Subtract(AppVars.Profile.ServDiff);
            var curShift = GetShift(curTime);
            var h = 1;
            switch (herb)
            {
                case "Инжир":
                case "Кипарис":
                case "Брусника":
                case "Смертоцвет":
                case "Лимон":
                case "Дурман":
                case "Камелия":
                case "Ландыш":
                case "Рапонтикум":
                case "Береза":
                case "Дуб":
                case "Алоэ":
                case "Гравилат":
                case "Прагениана":
                case "Айва":
                case "Дягиль":
                case "Каперс":
                case "Секуринега":
                case "Кентарийская дикая роза":
                case "Кора дуба":
                    h = 2;
                    break;
            }

            var minutes = (h * 60) - 2;
            var nextTime = curTime.AddMinutes(minutes);
            var nextShift = GetShift(nextTime);
            if (curShift != nextShift)
            {
                colormessage += "Таймер не установлен, смена трав близка.";
            }
            else
            {
                minutes += 30;
                var appTimer = new AppTimer
                                   {
                                       Description =
                                           string.Format("Вырастет {0} на {1}", herb, AppVars.Profile.MapLocation),
                                       TriggerTime = DateTime.Now.AddMinutes(minutes),
                                       IsHerb = true
                                   };
                AppTimerManager.AddAppTimer(appTimer);
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTimersDelegate(AppVars.MainForm.UpdateTimers),
                            new object[] {});
                    }
                }
                catch (InvalidOperationException)
                {
                }

                AppVars.Profile.Save();
                colormessage += h == 1 ? "Таймер установлен на <b>1</b> час" : "Таймер установлен на <b>2</b> часа.";
            }

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                        new object[] { colormessage });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private static int GetShift(DateTime dateTime)
        {
            var d1 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 50, 0);
            var d2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 6, 50, 0);
            var d3 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 50, 0);
            var d4 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 18, 50, 0);
            if ((dateTime < d1) || (dateTime >= d4))
            {
                return 4;
            }

            if ((dateTime >= d1) && (dateTime < d2))
            {
                return 1;
            }

            if ((dateTime >= d2) && (dateTime < d3))
            {
                return 2;
            }

            if ((dateTime >= d3) && (dateTime < d4))
            {
                return 3;
            }

            return 0;
        }

        internal static void TraceCutID(string herbid)
        {
            string herb = herbid;
            var colormessage = string.Format("Трава &laquo;<b>{0}</b>&raquo; спилена. ", herb);
            var curTime = DateTime.Now.Subtract(AppVars.Profile.ServDiff);
            var curShift = GetShift(curTime);
            var h = 1;
            switch (herb)
            {
                case "Инжир":
                case "Кипарис":
                case "Брусника":
                case "Смертоцвет":
                case "Лимон":
                case "Дурман":
                case "Камелия":
                case "Ландыш":
                case "Рапонтикум":
                case "Береза":
                case "Дуб":
                case "Алоэ":
                case "Гравилат":
                case "Прагениана":
                case "Айва":
                case "Дягиль":
                case "Каперс":
                case "Секуринега":
                case "Кентарийская дикая роза":
                    h = 2;
                    break;
            }

            var minutes = (h * 60) - 2;
            var nextTime = curTime.AddMinutes(minutes);
            var nextShift = GetShift(nextTime);
            if (curShift != nextShift)
            {
                colormessage += "Таймер не установлен, смена трав близка.";
            }
            else
            {
                minutes += 30;
                var appTimer = new AppTimer
                                   {
                                       Description =
                                           string.Format("Вырастет {0} на {1}", herb, AppVars.Profile.MapLocation),
                                       TriggerTime = DateTime.Now.AddMinutes(minutes),
                                       IsHerb = true
                                   };
                AppTimerManager.AddAppTimer(appTimer);
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTimersDelegate(AppVars.MainForm.UpdateTimers),
                            new object[] {});
                    }
                }
                catch (InvalidOperationException)
                {
                }

                AppVars.Profile.Save();
                colormessage += h == 1 ? "Таймер установлен на <b>1</b> час." : "Таймер установлен на <b>2</b> часа.";
            }

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                        new object[] { colormessage });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}