using System;
using System.Threading;
using ABClient.ABForms;
using ABClient.MyHelpers;
using ABClient.MyProfile;

namespace ABClient
{
    internal static class UnderAttack
    {
        internal static bool IsHuman;
        private static string _fightty = string.Empty;
        internal static bool IsMe;

        /*
        private static readonly List<string> Bots = new List<string>
                                                        {
                                                "Орк", "Гоблин", "Огр", "Огр-берсеркер", "Разбойник", "Грабитель", "Зомби", "Скелет", "Скелет-мечник",
                                                "Паук", "Ядовитый паук", "Крыса", "Кабан", "Призрак", "Элементаль Воды", "Элементаль Земли", "Элементаль Огня",
                                                "Элементаль Воздуха", "Некромант", "Дух", "Болотный тролль", "Сильф", "Нетопырь"
                                            };
         */ 

        internal static void Parse(string html)
        {
            ThreadPool.UnsafeQueueUserWorkItem(ParseAsync, html);
        }

        private static void WriteChatMessage(string message)
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateWriteRealChatMsgDelegate(AppVars.MainForm.WriteMessageToChat), message);
                }
            }
            catch (InvalidOperationException)
            {
            }            
        }

        private static void ParseAsync(object state)
        {
            if (AppVars.Profile.LezSay == LezSayType.No)
                return;

            var html = (string) state;
            var fightty = HelperStrings.SubString(html, "var fight_ty = [", "];");
            if (string.IsNullOrEmpty(fightty))
                return;

            var pars = fightty.Split(',');
            if (pars.Length < 9)
                return;

            fightty = pars[8].Trim('"');
            if (fightty.Equals(_fightty))
                return;

            _fightty = fightty;

            var livesg1 = HelperStrings.SubString(html, "var lives_g1 = [", "];");
            if (string.IsNullOrEmpty(livesg1))
                return;

            pars = livesg1.Split(',');
            var nick1 = (pars.Length > 2) && !livesg1.StartsWith("[4") ? pars[1].Trim(new[] { '"' }) : "невидимка";

            var livesg2 = HelperStrings.SubString(html, "var lives_g2 = [", "];");
            if (string.IsNullOrEmpty(livesg2))
                return;

            if (livesg2.IndexOf(AppVars.Profile.UserNick, StringComparison.Ordinal) == -1)
                IsMe = true;

            pars = livesg2.Split(',');
            var nick2 = (pars.Length > 2) && !livesg2.StartsWith("[4") ? pars[1].Trim(new[] { '"' }) : "невидимка";

            var fighttype = HelperStrings.SubString(html, " начался (", ")") ?? "обычное нападение";
            IsHuman = !fighttype.Equals("нападение бота", StringComparison.OrdinalIgnoreCase);
            
            if (!IsHuman)
                return;

            var suffix = string.Empty;
            string message;

            switch (AppVars.Profile.LezSay)
            {
                case LezSayType.Chat:
                    break;

                case LezSayType.Clan:
                    suffix = "%clan%";
                    break;

                case LezSayType.Pair:
                    suffix = "%pair%";
                    break;

                case LezSayType.No:
                    break;
            }

            if (IsMe)
            {
                message = string.Format($"{suffix} я нападаю на перса «{nick2}», клетка {AppVars.Profile.MapLocation}, [[[{_fightty}]]] ({fighttype})!");
                WriteChatMessage(message);
            }
            else
            {
                message = string.Format(nick1.Equals("невидимка", StringComparison.OrdinalIgnoreCase) ? 
                    $"{suffix} на меня напал невидимка, клетка {AppVars.Profile.MapLocation}, [[[{_fightty}]]] ({fighttype})!" : 
                    $"{suffix} на меня напал перс «{nick1}», клетка {AppVars.Profile.MapLocation}, [[[{_fightty}]]] ({fighttype})!");

                WriteChatMessage(message);
            }
        }
    }
}
