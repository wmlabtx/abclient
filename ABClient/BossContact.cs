using ABClient.Helpers;
using System;
using System.Text;
using ABClient.ABForms;
using ABClient.MyHelpers;
using ABClient.MyProfile;

namespace ABClient
{
    public class BossContact
    {
        internal string Name { get; }
        internal DateTime NextCheck { get; set; }
        internal DateTime LastBossUpdated { get; set; }

        private string Flog { get; set; }

        internal BossContact(string name, bool delayedCheck, DateTime lastBossUpdated)
        {
            Name = name.Trim();
            LastBossUpdated = lastBossUpdated;
            Flog = string.Empty;
            NextCheck = delayedCheck ? DateTime.Now.AddSeconds(Dice.Make(30, 90)) : DateTime.Now;
        }

        public void Process(UserInfo userInfo)
        {
            if (userInfo == null)
                return;

            var location = userInfo.Location;
            var splocation = location.Split(new[] { " [" }, StringSplitOptions.RemoveEmptyEntries);
            if (splocation.Length == 2)
            {
                splocation[1] = splocation[1].Substring(0, splocation[1].Length - 1);
                location = splocation[1];
            }

            var isonline = !string.IsNullOrEmpty(location);
            var flog = userInfo.FightLog;
            if (flog.Equals("0", StringComparison.Ordinal))
                flog = string.Empty;

            NextCheck = DateTime.Now.AddSeconds(30);

            if (!isonline)
                return;

            if (flog.Equals(Flog))
                return;

            Flog = flog;

            if (string.IsNullOrEmpty(flog))
                return;

            var fight = NeverApi.GetFlog(flog);
            //fight = File.ReadAllText("boss3.txt");
            if (string.IsNullOrEmpty(fight))
                return;

            // var lives_g2 = [[3,"Королева Змей",49331,100000,2304578,1]];

            // [1,"Anothers_girl",21,4,"c174",1070,1070,1]
            // [1,"PAPA",16,3,"c249",795,795,1]
            // [1,"E Pluribus Unum",15,2,"c176",1095,1095,1]

            // 16:53:27 Босс Королева Змей [25] нападает...
            // 16:53:27 Босс Королева Змей [25]: 99950/100000
            // 13:30:22 Босс Хранитель Леса [25]: 0/100000
            // 13:30:22 Босс Королева Змей [25]: 100000/100000

            var livesg1 = HelperStrings.SubString(fight, "var lives_g1 = [[", "]]");
            var livesg2 = HelperStrings.SubString(fight, "var lives_g2 = [[", "]]");
            if (string.IsNullOrEmpty(livesg1) || string.IsNullOrEmpty(livesg2))
            {
                return;
            }

            //File.WriteAllText($"x{flog}.txt", fight);

            if (CheckLives(Name, flog, userInfo, location, livesg1))
            {
                ContactsManager.AddUsers(livesg2);
                //File.WriteAllText($"x{flog}.txt", fight);
                return;
            }

            if (CheckLives(Name, flog, userInfo, location, livesg2))
            {
                ContactsManager.AddUsers(livesg1);
                //File.WriteAllText($"x{flog}.txt", fight);
            }
        }

        private static bool CheckLives(string nick, string flog, UserInfo userInfo, string location, string args)
        {
            /* 
                var lives_g1 = [[1,"_(*Na*(_",23,1,"dshi",2810,5075,1],[1,"WAMPIRIK",23,3,"c125",1425,1425,1]];
                var lives_g2 = [[3,"Хранитель Леса",67935,100000,2303103,1]];
             */

            var users = args.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var user in users)
            {
                var spar = user.Split(',');
                if (spar.Length < 5)
                    continue;

                //var names = new[] { "\"Королева Змей\"", "\"Хранитель Леса\"" };
                var ids = new[] { "2304578", "2347953", "2358275" };
                for (var i = 0; i < ids.Length; i++)
                {
                    if (spar[0].Equals("3") && spar[4].Equals(ids[i]))
                    {
                        var id = spar[4];
                        var name = spar[1].Trim('\"');
                        var message =
                            $"{HtmlBossEntry(id, name)} напал на {HtmlPercEntry(userInfo)} в локации <b>{location}</b>. Возможные клетки: <i>{BossMap.GetRegNum(location)}</i>.  <a href='http://www.neverlands.ru/logs.fcg?fid={flog}' onclick='window.open(this.href);' target=_blank>Ссылка на бой</a>";

                        MySounds.EventSounds.PlayBear();
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateChatDelegate(AppVars.MainForm.UpdateChat), message);
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        if (AppVars.Profile.BossSay != LezSayType.No)
                        {
                            if (string.IsNullOrEmpty(AppVars.BossSayLastLog) || !flog.Equals(AppVars.BossSayLastLog))
                            {
                                AppVars.BossSayLastLog = flog;
                                var suffix = string.Empty;
                                switch (AppVars.Profile.BossSay)
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

                                message =
                                    $"{suffix}{name} напал на «{nick}» в локации [{location}]. Возможные клетки: {BossMap.GetRegNum(location)}. [[[{flog}]]]";
                                try
                                {
                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.BeginInvoke(
                                            new UpdateWriteRealChatMsgDelegate(AppVars.MainForm.WriteMessageToChat),
                                            message);
                                    }
                                }
                                catch (InvalidOperationException)
                                {
                                }
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        private static string HtmlBossEntry(string id, string name)
        {
            //2304578 - Королева Змей
            //2347953 - Хранитель Леса

            var sb = new StringBuilder();
            sb.Append(@"<font class=nickname><b><font color=""#8800BB"">");
            sb.Append(name);
            sb.Append(@"</font></b></font><a href=""http://www.neverlands.ru/pbots.cgi?");
            sb.Append(id);
            sb.Append(@""" onclick=""window.open(this.href);""><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>");
            return sb.ToString();
        }

        private static string HtmlPercEntry(UserInfo userInfo)
        {
            if (userInfo == null)
                return "Аноним";

            var nick = userInfo.Nick;
            var nnhtmlSec = nick.Replace("+", "%2B");
            var colorNick = nick;
            switch (ContactsManager.GetClassIdOfContact(nick))
            {
                case 1:
                    colorNick = @"<font color=""#8A0808"">" + colorNick + "</font>";
                    break;
                case 2:
                    colorNick = @"<font color=""#0B610B"">" + colorNick + "</font>";
                    break;
                default:
                    colorNick = @"<font color=""#000000"">" + colorNick + "</font>";
                    break;
            }

            var sign = userInfo.ClanSign;
            var level = userInfo.Level;
            var clan = userInfo.ClanName;

            // var effects = [[1,'Боевая травма (x9) (еще 23:06:17)'],[2,'Тяжелая травма (x2) (еще 07:01:22)'],[17,'Молчанка (еще 00:00:05)']];
            if (userInfo.EffectsCodes.Length > 0)
            {
                var sbeff = new StringBuilder();
                for (var k = 0; k < userInfo.EffectsCodes.Length; k++)
                {
                    var effcode = userInfo.EffectsCodes[k];
                    var effname = userInfo.EffectsNames[k];
                    sbeff.AppendFormat(
                        @"&nbsp;<img src=http://image.neverlands.ru/pinfo/eff_{0}.gif width=15 height=15 align=absmiddle title=""{1}"">",
                        effcode,
                        effname);
                }
            }

            var ss = string.Empty;
            if (!string.IsNullOrEmpty(clan))
            {
                ss =
                    "<img src=http://image.neverlands.ru/signs/" +
                    sign +
                    @" width=15 height=12 align=absmiddle title=""" +
                    clan +
                    @""">&nbsp;";
            }

            var result =
                @"<a href=""#"" onclick=""top.say_private('" +
                nick +
                @"');""><img src=http://image.neverlands.ru/chat/private.gif width=11 height=12 border=0 align=absmiddle></a>&nbsp;" +
                ss +
                @"<a class=""activenick"" href=""#"" onclick=""top.say_to('" + nick + @"');""><font class=nickname><b>" +
                colorNick +
                "</b></a>[" +
                level +
                @"]</font><a href=""http://www.neverlands.ru/pinfo.cgi?" +
                nnhtmlSec +
                @""" onclick=""window.open(this.href);""><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>";
            return result;
        }
    }
}
