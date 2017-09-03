using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using ABClient.ABForms;
using ABClient.Helpers;
using ABClient.MyHelpers;
using ABClient.Things;

namespace ABClient
{
    internal sealed class Contact
    {
        internal string Name { get; private set; } // Имя 
        internal string Comments { get; set; } // Комментарии
        internal bool Tracing { get; set; } // Нужно ли следить за контактом
        internal int ClassId { get; set; } // Группа: 0 -нейтрал, 1 - враг, 2 - друг
        internal int ToolId { get; set; } // Применение нападалки: 0 - по умолчанию, 1 - боевая , 2 - закрытая боевая, 3 - кулачка, 4 - закрытая кулачка
        
        internal string Sign { get; private set; } // Значок клана
        internal string Align { get; private set; } // Склонность
        internal string Clan { get; private set; } // Название клана
        
        internal string Level { get; private set; } // Уровень
        internal string Location { get; private set; } // Локация

        internal string TreeNode { get; private set; } // Наш TreeNode
        internal string Parent { get; set; } // string.Empty - если мы в корне, иначе это TreeNodeId группы

        private int WoundCounts { get; set; }
        internal int[] Wounds { get; private set; }

        private string[] CodeEffects { get; set; }
        private string[] NameEffects { get; set; }

        internal bool IsMolch { get; private set; }
        internal bool IsOnline { get; private set; }
        private DateTime LastUpdated { get; set; }
        internal DateTime NextCheck { get; set; }

        private int Tied { get; set; }
        private string Flog { get; set; }
        private bool IsBotLog { get; set; }
        private string[] PSlots { get; set; }

        private bool _isFirst;
        private int _delay;

        internal Contact(string name, int classid, int toolid, string comments, bool tracing, bool delayedCheck)
        {
            Name = name.Trim();
            ClassId = classid;
            ToolId = toolid;
            Comments = comments;
            Tracing = tracing;
            Sign = string.Empty;
            Align = string.Empty;
            Clan = string.Empty;
            Level = string.Empty;
            Location = string.Empty;
            InternalInit(delayedCheck);
        }

        internal Contact(string name, int classid, int toolid, string sign, string clan, string align, string comments, bool tracing, string level, bool delayedCheck)
        {
            Name = name.Trim();
            ClassId = classid;
            ToolId = toolid;
            Sign = sign;
            Align = align;
            Clan = clan;
            Comments = comments;
            Tracing = tracing;
            Level = level;
            InternalInit(delayedCheck);
        }

        private void InternalInit(bool delayedCheck)
        {
            Parent = string.Empty;
            Location = string.Empty;
            WoundCounts = 0;
            Wounds = new int[4];
            Tied = 0;
            Flog = string.Empty;
            _isFirst = true;
            TreeNode = Guid.NewGuid().ToString();
            _delay = 0;
            LastUpdated = DateTime.MinValue;
            NextCheck = delayedCheck ? DateTime.Now.AddSeconds(Dice.Make(30, 90)) : DateTime.Now;
        }

        public override string ToString()
        {
            var result = string.IsNullOrEmpty(Level) ? Name : string.Format(CultureInfo.InvariantCulture, "{0} [{1}]", Name, Level);
            return result;
        }

        internal void Process(UserInfo userInfo)
        {
            var currentTimeStamp = DateTime.Now;
            if (userInfo == null)
                return;

            var tied = userInfo.Tied;
            var isKa = false;
            var sslots = new StringBuilder();
            var pslots = new string[userInfo.SlotsCodes.Length];
            for (var indexSlot = 0; indexSlot < userInfo.SlotsCodes.Length; indexSlot++)
            {
                pslots[indexSlot] = $"{userInfo.SlotsCodes[indexSlot]}:{userInfo.SlotsNames[indexSlot]}";
                if ((indexSlot != 0) && (indexSlot != 1) && (indexSlot != 2) && (indexSlot != 3) &&
                    (indexSlot != 7) && (indexSlot != 10) &&
                    (indexSlot != 11) && (indexSlot != 12) && (indexSlot != 13) && (indexSlot != 14) &&
                    (indexSlot != 15))
                    continue;

                var thingImage = userInfo.SlotsCodes[indexSlot];
                if (!isKa)
                {
                    var tl = ThingsDb.Find(thingImage);
                    if (tl.Count == 0)
                        isKa = true;
                }

                sslots.Append(thingImage);
                sslots.Append('@');
            }

            if (sslots.Length == 0)
                return;

            sslots.Length--;

            var nick = userInfo.Nick;
            var align = userInfo.Align;
            var sign = userInfo.ClanSign;
            var level = userInfo.Level;
            var location = userInfo.Location;
            var isonline = !string.IsNullOrEmpty(location);
            var flog = userInfo.FightLog;
            if (flog.Equals("0", StringComparison.Ordinal))
            {
                flog = string.Empty;
            }

            var clan = userInfo.ClanName;

            var woundCounts = 0;
            var wounds = new int[4];
            var isMolch = false;
            var codeEffects = new List<string>();
            var nameEffects = new List<string>();

            // var effects = [[1,'Боевая травма (x9) (еще 23:06:17)'],[2,'Тяжелая травма (x2) (еще 07:01:22)'],[17,'Молчанка (еще 00:00:05)']];
            var sbeff = new StringBuilder();
            if (userInfo.EffectsCodes.Length > 0)
            {
                for (var k = 0; k < userInfo.EffectsCodes.Length; k++)
                {
                    var effcode = userInfo.EffectsCodes[k];
                    var effname = userInfo.EffectsNames[k];
                    sbeff.AppendFormat(
                        @"&nbsp;<img src=http://image.neverlands.ru/pinfo/eff_{0}.gif width=15 height=15 align=absmiddle title=""{1}"">",
                        effcode,
                        effname);
                    int effcount;
                    int.TryParse(userInfo.EffectsSizes[k], out effcount);

                    switch (effcode)
                    {
                        case "1":
                            woundCounts += effcount;
                            wounds[3] = effcount;
                            break;

                        case "2":
                            woundCounts += effcount;
                            wounds[2] = effcount;
                            break;

                        case "3":
                            woundCounts += effcount;
                            wounds[1] = effcount;
                            break;

                        case "4":
                            woundCounts += effcount;
                            wounds[0] = effcount;
                            break;

                        case "17":
                            isMolch = true;
                            break;

                        default:
                            codeEffects.Add(effcode);
                            var pos = effname.IndexOf(" (", StringComparison.Ordinal);
                            var ename = (pos >= 0 ? effname.Substring(0, pos) : effname).Trim('\'');
                            nameEffects.Add(ename);
                            break;
                    }
                }
            }

            var splocation = location.Split(new[] { " [" }, StringSplitOptions.RemoveEmptyEntries);
            if (splocation.Length == 2)
            {
                splocation[1] = splocation[1].Substring(0, splocation[1].Length - 1);
                if ((splocation[1].IndexOf(splocation[0], StringComparison.OrdinalIgnoreCase) != -1) || splocation[1].Contains(","))
                {
                    location = splocation[1];
                }
            }

            Name = nick;
            Level = level;
            Align = align;
            Sign = sign;
            Clan = clan;

            var sb = new StringBuilder();
            var messagePrefix = HtmlContactEntry(this)/* + sbeff*/;

            // Вход/выход

            if (!_isFirst && IsOnline != isonline)
            {
                if (sb.Length > 0)
                    sb.Append(',');

                if (isonline)
                    sb.AppendFormat(@" появляется в <font color=""#3F7F62"">{0}</font>", location);
                else
                    sb.AppendFormat(
                        isKa
                            ? @" исчезает в <font color=""#3F7F62"">{0}</font>"
                            : @" выходит из игры в <font color=""#3F7F62"">{0}</font>", Location);
            }

            // Переодевания

            if (ClassId != 2)
            {
                if (!_isFirst)
                {
                    var sbp = new StringBuilder();
                    var changes = 0;
                    if (pslots != null && PSlots != null && pslots.Length == PSlots.Length)
                    {
                        for (var i = 0; i < pslots.Length; i++)
                        {
                            var opars = PSlots[i].Split(':');
                            if (opars.Length < 2)
                                continue;

                            var npars = pslots[i].Split(':');
                            if (npars.Length < 2)
                                continue;

                            var oimage = opars[0];
                            var nimage = npars[0];

                            var oname = opars[1];
                            var nname = npars[1];

                            if (oimage.Equals(nimage, StringComparison.CurrentCultureIgnoreCase) &&
                                oname.Equals(nname, StringComparison.CurrentCultureIgnoreCase))
                                continue;

                            sbp.Append(sbp.Length > 0 ? ", " : " ");
                            if (nimage.StartsWith("sl_"))
                            {
                                sbp.Append("снимает");
                                sbp.Append(" &laquo;");
                                sbp.Append(oname);
                            }
                            else
                            {
                                sbp.Append("одевает");
                                sbp.Append(" &laquo;");
                                sbp.Append(nname);
                            }

                            sbp.Append("&raquo;");
                            changes++;
                        }
                    }

                    if (sbp.Length > 0)
                    {
                        if (sb.Length > 0)
                            sb.Append(',');

                        sb.Append(changes > 3 ? " переодевается" : sbp.ToString());
                    }
                }
            }

            // Изменение усталости

            if (ClassId != 2)
            {
                if (!_isFirst && isonline && IsOnline && Location != location && !string.IsNullOrEmpty(location))
                {
                    if (sb.Length > 0)
                        sb.Append(',');

                    if (Tied != tied)
                        sb.AppendFormat(@" усталость <b>{0}</b> в <font color=""#3F7F62"">{1}</font>", tied, location);
                    else
                        sb.AppendFormat(@" переходит в <font color=""#3F7F62"">{0}</font>", location);
                }
                else
                {
                    if (!_isFirst && isonline && IsOnline && Location == location && Tied < tied &&
                        !string.IsNullOrEmpty(location))
                    {
                        if (sb.Length > 0)
                            sb.Append(',');

                        sb.AppendFormat(@" усталость {0}&rarr;<b>{1}</b> в <font color=""#3F7F62"">{2}</font>", Tied,
                            tied, location);
                    }
                }
            }

            // Вступление в бой

            if (!_isFirst && isonline && (string.IsNullOrEmpty(Flog) && !string.IsNullOrEmpty(flog) || (!string.IsNullOrEmpty(Flog) && !string.IsNullOrEmpty(flog) && !Flog.Equals(flog, StringComparison.Ordinal))))
            {
                IsBotLog = false;
                var wc = new WebClient {Proxy = AppVars.LocalProxy};
                byte[] bufferLog = null;
                try
                {
                    IdleManager.AddActivity();
                    bufferLog = wc.DownloadData("http://www.neverlands.ru/logs.fcg?fid=" + flog);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                finally
                {
                    wc.Dispose();
                    IdleManager.RemoveActivity();
                }

                // var logs = [[1387615150,6],[[0,"12:39"],"Бой между",[4,1]," и",[1,2,"*Трогвар*",22,1,"c138"]," начался (закрытое боевое нападение) (21.12.2013 12:39:10)."]];
                // var logs = [[1387626458,6],[[0,"15:47"],"Бой между",[4,1]," и",[1,2,"~Shtaket~",18,3,"c167"]," начался (боевое нападение) (21.12.2013 15:47:38)."],[[0,"15:47"],[1,1,"Ruber",22,1,"necr"]," <B>вмешался в бой.</B>"],[[0,"15:49"],[1,1,"_NickName_",22,1,"glow"]," использовал заклинание <img src=http://image.neverlands.ru/signs/darks.gif width=15 height=12 border=0 align=absmiddle> «<B>Темное нападение</B>» и вмешался в бой.</B>"],[[0,"15:49"],[1,2,"~Shtaket~",18,3,"c167"]," получил осложненную травму <font color=#E34242><b>«Множественные повреждения основания свода черепа»</b></font>."],[[0,"15:49"],"<B>Бой закончен по таймауту</B> (",[4,1]," )."],[[0,"15:49"],"<B>Победа за</B>",[4,1],",",[1,1,"_NickName_",22,1,"glow"],",",[1,1,"Ruber",22,1,"necr"],"."]];

                if (bufferLog != null)
                {
                    var fight = Russian.Codepage.GetString(bufferLog);

                    if ((fight.IndexOf("(нападение бота)", StringComparison.OrdinalIgnoreCase) != -1) ||
                        (fight.IndexOf("var logs = [];", StringComparison.OrdinalIgnoreCase) != -1))
                    {
                        IsBotLog = true;
                    }

                    if (!IsBotLog)
                    {
                        if (sb.Length > 0)
                            sb.Append(',');

                        var fighttype = HelperStrings.SubString(fight, " начался (", ")") ?? "обычное нападение";
                        if (fight.IndexOf("Бой (жертвенный)", StringComparison.OrdinalIgnoreCase) != -1)
                            fighttype = "жертвенный бой";


                        var livesg1 = HelperStrings.SubString(fight, "var lives_g1 = [", "];");
                        if (!string.IsNullOrEmpty(livesg1))
                        {
                            var pars = livesg1.Split(',');
                            var nick1 = (pars.Length > 2) && !livesg1.StartsWith("[4")
                                ? pars[1].Trim('"')
                                : "невидимка";

                            var livesg2 = HelperStrings.SubString(fight, "var lives_g2 = [", "];");
                            if (!string.IsNullOrEmpty(livesg2))
                            {
                                pars = livesg2.Split(',');
                                var nick2 = (pars.Length > 2) && !livesg2.StartsWith("[4")
                                    ? pars[1].Trim('"')
                                    : "невидимка";

                                if (livesg2.IndexOf(Name, StringComparison.Ordinal) == -1)
                                {
                                    // нападает на перса nick2
                                    sb.AppendFormat(
                                        @" <a href=http://www.neverlands.ru/logs.fcg?fid={0} onclick=""window.open(this.href);"">нападает</a> на {1} ({2})",
                                        flog, HtmlPercEntry(nick2), fighttype);
                                }
                                else
                                {
                                    if (nick1.Equals("невидимка", StringComparison.OrdinalIgnoreCase))
                                    {
                                        // на меня напал невидимка
                                        sb.AppendFormat(
                                            @" <a href=http://www.neverlands.ru/logs.fcg?fid={0} onclick=""window.open(this.href);"">атакован</a> невидимкой ({1})",
                                            flog, fighttype);
                                    }
                                    else
                                    {
                                        // на меня напал перс nick1
                                        sb.AppendFormat(
                                            @" <a href=http://www.neverlands.ru/logs.fcg?fid={0} onclick=""window.open(this.href);"">атакован</a> {1} ({2})",
                                            flog, HtmlPercEntry(nick1), fighttype);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Получение молчанки

            if (!_isFirst && IsMolch != isMolch)
            {
                if (sb.Length > 0)
                    sb.Append(',');

                sb.AppendFormat(isMolch ? " получает молчанку" : " выходит из молчания");
            }

            // Травмы

            if (!_isFirst && (WoundCounts > 0) && (woundCounts == 0))
            {
                if (sb.Length > 0)
                    sb.Append(',');

                sb.AppendFormat(" излечивается от всех травм");
            }

            // Травмы

            if (!_isFirst && (WoundCounts > 0) && (woundCounts > 0) && (woundCounts < WoundCounts))
            {
                if (sb.Length > 0)
                    sb.Append(',');

                sb.AppendFormat(" излечивается (травм стало: {0})", woundCounts);
            }

            // Травмы

            if (!_isFirst && (woundCounts > WoundCounts))
            {
                if (sb.Length > 0)
                    sb.Append(',');

                if ((woundCounts - WoundCounts) > 1)
                    sb.AppendFormat(" получает несколько травм");
                else
                {
                    string wound = "никакую";
                    if (wounds[3] > Wounds[3])
                        wound = "боевую";
                    else
                    {
                        if (wounds[2] > Wounds[2])
                            wound = "тяжелую";
                        else
                        {
                            if (wounds[1] > Wounds[1])
                                wound = "среднюю";
                            else
                            {
                                if (wounds[0] > Wounds[0])
                                    wound = "легкую";
                            }
                        }
                    }

                    sb.AppendFormat(" получает {0} травму (травм стало: {1})", wound, woundCounts);
                }
            }

            // Эффекты

            if (ClassId != 2)
            {
                if (!_isFirst)
                {
                    var sbadd = new StringBuilder();
                    for (var i = 0; i < codeEffects.Count; i++)
                    {
                        if (Array.IndexOf(CodeEffects, codeEffects[i]) < 0)
                        {
                            if (sbadd.Length == 0)
                                sbadd.Append(" получает");

                            sbadd.Append(
                                $"&nbsp;<img src=http://image.neverlands.ru/pinfo/eff_{codeEffects[i]}.gif width=15 height=15 align=absmiddle title=\"{nameEffects[i]}\">&nbsp;{nameEffects[i]}");
                        }
                    }

                    if (sbadd.Length > 0)
                    {
                        if (sb.Length > 0)
                            sb.Append(',');

                        sb.Append(sbadd);
                    }

                    var sbrem = new StringBuilder();
                    for (var i = 0; i < CodeEffects.Length; i++)
                    {
                        if (!codeEffects.Contains(CodeEffects[i]))
                        {
                            if (sbrem.Length == 0)
                                sbrem.Append(" теряет");

                            sbrem.Append(
                                $"&nbsp;<img src=http://image.neverlands.ru/pinfo/eff_{CodeEffects[i]}.gif width=15 height=15 align=absmiddle title=\"{NameEffects[i]}\">&nbsp;{NameEffects[i]}");
                        }
                    }

                    if (sbrem.Length > 0)
                    {
                        if (sb.Length > 0)
                            sb.Append(',');

                        sb.Append(sbrem);
                    }
                }
            }

            NameEffects = nameEffects.ToArray();
            CodeEffects = codeEffects.ToArray();

            IsOnline = isonline;
            Location = location;
            Tied = tied;
            Flog = flog;
            PSlots = pslots;
            IsMolch = isMolch;
            WoundCounts = woundCounts;
            Wounds = wounds;

            _isFirst = false;
            if (sb.Length > 0)
            {
                _delay = 0;
                var message = messagePrefix + sb;
                if (currentTimeStamp > LastUpdated)
                {
                    LastUpdated = currentTimeStamp;
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
                }
            }
            else
            {
                _delay += Dice.Make(1, 1000);
                if (_delay > 10000)
                {
                    _delay = 10000;
                }
            }

            if (!IsOnline)
            {
                if (ClassId != 2)
                    _delay = 60000;
            }

            NextCheck = DateTime.Now.AddMilliseconds(_delay);

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateContactDelegate(AppVars.MainForm.UpdateContact), this);
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private static string HtmlPercEntry(string nick)
        {
            var userInfo = NeverApi.GetAll(nick);
            if (userInfo == null)
                return "Аноним";

            nick = userInfo.Nick;
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

            var align = userInfo.Align;
            var sign = userInfo.ClanSign;
            var level = userInfo.Level;
            var clan = userInfo.ClanName;

            var sbeff = new StringBuilder();
            if (userInfo.EffectsCodes.Length > 0)
            {
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

            var sleeps = string.Empty/*sbeff.ToString()*/;
            var ali1 = string.Empty;
            var ali2 = string.Empty;
            switch (align)
            {
                case "0":
                    ali1 = string.Empty;
                    ali2 = string.Empty;
                    break;
                case "1":
                    ali1 = "darks.gif";
                    ali2 = "Дети Тьмы";
                    break;
                case "2":
                    ali1 = "lights.gif";
                    ali2 = "Дети Света";
                    break;
                case "3":
                    ali1 = "sumers.gif";
                    ali2 = "Дети Сумерек";
                    break;
                case "4":
                    ali1 = "chaoss.gif";
                    ali2 = "Дети Хаоса";
                    break;
                case "5":
                    ali1 = "light.gif";
                    ali2 = "Истинный Свет";
                    break;
                case "6":
                    ali1 = "dark.gif";
                    ali2 = "Истинная Тьма";
                    break;
                case "7":
                    ali1 = "sumer.gif";
                    ali2 = "Нейтральные Сумерки";
                    break;
                case "8":
                    ali1 = "chaos.gif";
                    ali2 = "Абсолютный Хаос";
                    break;
                case "9":
                    ali1 = "angel.gif";
                    ali2 = "Ангел";
                    break;
            }

            align = string.IsNullOrEmpty(ali1)?
                string.Empty :
                "<img src=http://image.neverlands.ru/signs/" +
                ali1 +
                @" width=15 height=12 align=absmiddle border=0 title=""" +
                ali2 +
                @""">&nbsp";

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
                align +
                ss +
                @"<a class=""activenick"" href=""#"" onclick=""top.say_to('" + nick + @"');""><font class=nickname><b>" +
                colorNick +
                "</b></a>[" +
                level +
                @"]</font><a href=""http://www.neverlands.ru/pinfo.cgi?" +
                nnhtmlSec +
                @""" onclick=""window.open(this.href);""><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" +
                sleeps;
            return result;
        }

        private static string HtmlContactEntry(Contact contact)
        {
            var nnhtmlSec = contact.Name;
            {
                nnhtmlSec = nnhtmlSec.Replace("+", "%2B");
            }

            var colorNick = contact.Name;
            switch (contact.ClassId)
            {
                case 0:
                    colorNick = @"<font color=""#000000"">" + colorNick + "</font>";
                    break;
                case 1:
                    colorNick = @"<font color=""#8A0808"">" + colorNick + "</font>";
                    break;
                case 2:
                    colorNick = @"<font color=""#0B610B"">" + colorNick + "</font>";
                    break;
            }

            var sleeps = string.Empty;
            var ali1 = string.Empty;
            var ali2 = string.Empty;
            
            switch (contact.Align)
            {
                case "0":
                    ali1 = string.Empty;
                    ali2 = string.Empty;
                    break;
                case "1":
                    ali1 = "darks.gif";
                    ali2 = "Дети Тьмы";
                    break;
                case "2":
                    ali1 = "lights.gif";
                    ali2 = "Дети Света";
                    break;
                case "3":
                    ali1 = "sumers.gif";
                    ali2 = "Дети Сумерек";
                    break;
                case "4":
                    ali1 = "chaoss.gif";
                    ali2 = "Дети Хаоса";
                    break;
                case "5":
                    ali1 = "light.gif";
                    ali2 = "Истинный Свет";
                    break;
                case "6":
                    ali1 = "dark.gif";
                    ali2 = "Истинная Тьма";
                    break;
                case "7":
                    ali1 = "sumer.gif";
                    ali2 = "Нейтральные Сумерки";
                    break;
                case "8":
                    ali1 = "chaos.gif";
                    ali2 = "Абсолютный Хаос";
                    break;
                case "9":
                    ali1 = "angel.gif";
                    ali2 = "Ангел";
                    break;
            }

            var align = string.IsNullOrEmpty(ali1)
                ? string.Empty
                : "<img src=http://image.neverlands.ru/signs/" +
                  ali1 +
                  @" width=15 height=12 align=absmiddle border=0 title=""" +
                  ali2 +
                  @""">&nbsp";

            var ss = string.Empty;
            if (!string.IsNullOrEmpty(contact.Clan))
            {
                ss =
                    "<img src=http://image.neverlands.ru/signs/" +
                    contact.Sign +
                    @" width=15 height=12 align=absmiddle title=""" +
                    contact.Clan +
                    @""">&nbsp;";
            }

            /* top.say_private( top.say_to( не работает */
            var result =
                @"<a href=""#"" onclick=""top.say_private('" +
                contact.Name +
                @"');""><img src=http://image.neverlands.ru/chat/private.gif width=11 height=12 border=0 align=absmiddle></a>&nbsp;" +
                align +
                ss +
                @"<a class=""activenick"" href=""#"" onclick=""top.say_to('" + contact.Name + @"');""><font class=nickname><b>" +
                colorNick +
                "</b></a>[" +
                contact.Level +
                @"]</font><a href=""http://www.neverlands.ru/pinfo.cgi?" +
                nnhtmlSec +
                @""" onclick=""window.open(this.href);""><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" +
                sleeps;
            return result;
        }
    }
}