using System.Collections.Generic;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ABClient.ABForms;
using ABClient.ExtMap;
using ABClient.Helpers;
using ABClient.MyChat;
using ABClient.MyHelpers;
using ABClient.MySounds;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static void FilterGetLocation(string url)
        {
            var regX = new Regex(@"&gx=([\d]+)");
            var matchX = regX.Match(url);
            if (matchX.Groups.Count < 2)
            {
                return;
            }

            var gx = Convert.ToInt32(matchX.Groups[1].Value, CultureInfo.InvariantCulture);
            var regY = new Regex(@"&gy=([\d]+)");
            var matchY = regY.Match(url);
            if (matchY.Groups.Count < 2)
            {
                return;
            }

            var gy = Convert.ToInt32(matchY.Groups[1].Value, CultureInfo.InvariantCulture);
            AppVars.LocationReal = Map.ConvertToRegNum(gx, gy);

            /*
            if (Map.AbcCells.ContainsKey(AppVars.LocationReal))
            {
                Map.AbcCells[AppVars.LocationReal].Visited = DateTime.Now;
            }
            */
        }

        private static string[] GetComplects(string html)
        {
            /*
               compl_view("11","2000646344f0c5a13d362b","0ba0906db5c991b14c162922e7be1ceb");
               compl_view("test 2013","2066004990523ac2c7cf6a5","62eaf967bcd608cd89f37dceec194474");
             */

            if (string.IsNullOrEmpty(html))
                return null;

            var list = new List<string>();
            int pos = 0;
            while ((pos >= 0) && (pos < html.Length))
            {
                pos = html.IndexOf(@"compl_view(""", pos, StringComparison.Ordinal);
                if (pos == -1)
                    break;

                pos += @"compl_view(""".Length;
                var pos1 = pos;
                if (pos1 >= html.Length)
                    break;

                pos1 = html.IndexOf(@"""", pos1, StringComparison.Ordinal);
                if (pos1 == -1)
                    break;

                var compl = html.Substring(pos, pos1 - pos);
                list.Add(compl);

                pos = pos1 + 1;
            }

            return list.Count == 0 ? null : list.ToArray();
        }

        private static string MainPhpRoulette(string html)
        {
            var pos1 = html.IndexOf("<object", StringComparison.Ordinal);
            if (pos1 == -1)
                return html;

            var pos2 = html.IndexOf("</object>", pos1 + "<object".Length, StringComparison.Ordinal);
            if (pos2 == -1)
                return html;

            pos2 += "</object>".Length;

            var jackpot = HelperStrings.SubString(html, @"""jackpot"" value=""", @""""); // $1579.70
            var isfree = HelperStrings.SubString(html, @"""is_free"" value=""", @""""); // 1
            var enabled = HelperStrings.SubString(html, @"""enabled"" value=""", @""""); // 1
            var paidtext = HelperStrings.SubString(html, @"""paid_text"" value=""", @""""); // 0.2 DNV

            var sb = new StringBuilder();
            sb.AppendFormat("Текущий джекпот: <b>{0}</b><br><br>", jackpot);

            //isfree = "0";
            //enabled = "0";

            if (isfree.Equals("1"))
            {
                sb.AppendFormat(@"<input type=""button"" onclick=""javascript:start_roulette('free');"" value=""Крутить рулетку бесплатно"" class=""lbut""");
            }
            else
            {
                sb.AppendFormat(@"<input type=""button"" onclick=""javascript:start_roulette('1dnv');"" value=""Заплатить {0} и крутить рулетку"" class=""lbut""", paidtext);
            }

            if (!enabled.Equals("1"))
                sb.AppendFormat(@"disabled");

            sb.AppendFormat(@" /><br><br>");

            html = html.Substring(0, pos1) + sb + html.Substring(pos2);

            return html;
        }

        private static byte[] MainPhp(string address, byte[] array)
        {
            FilterGetLocation(address);

            AppVars.IdleTimer = DateTime.Now;
            AppVars.LastMainPhp = DateTime.Now;
            AppVars.ContentMainPhp = null;

            var html = Russian.Codepage.GetString(array);
            html = RemoveDoctype(html);

            /*
            if (html.IndexOf("view_map();", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                //html = html.Replace("<HEAD>", "<HEAD><meta http-equiv=\"x-ua-compatible\" content=\"IE=9\">");
                html = "<!DOCTYPE html>" + html;
            }
            */

            // view_map();
            // <meta http-equiv="x-ua-compatible" content="IE=7" >


            //var html = File.ReadAllText("telep.txt", AppVars.Codepage);
            //var html = File.ReadAllText("arena2.txt", AppVars.Codepage);
            // html = File.ReadAllText("noinv.txt", AppVars.Codepage);

            AppVars.ContentMainPhp = html;
            EventSounds.PlayRefresh();

            // Линки под картой
            /*
            if (html.Contains("<map name=\"links\""))
            {
                var sb = new StringBuilder();
                var pos = -1;
                do
                {
                    // AREA SHAPE="POLYGON" 
                    // HREF="main.php?get_id=56&act=10&go=build&pl=tarena1&vcode=fbcf6e1e1f13168c974a674ad355a26e"  
                    // COORDS="335,254,357,235,355,184,347,190,342,185,347,167,342,157,342,135,315,106,259,91,199,92,132,106,84,135,72,157,79,242,97,254" 
                    // onmouseover="tooltip(this,'Малая Арена')" onmouseout="hide_info(this)" >
                    //

                    const string strL1 = "<area shape=\"poly\" HREF=\"";
                    const string strL1Old = "<AREA SHAPE=\"POLYGON\" HREF=\"";
                    var strlen = strL1.Length;
                    var px = html.IndexOf(strL1, pos + 1, StringComparison.InvariantCultureIgnoreCase);
                    if (px == -1)
                    {
                        strlen = strL1Old.Length;
                        px = html.IndexOf(strL1Old, pos + 1, StringComparison.InvariantCultureIgnoreCase);
                        if (px == -1)
                            break;
                    }

                    pos = px;
                    var p1 = html.IndexOf("\"", pos + strlen, StringComparison.Ordinal);
                    var link = html.Substring(pos + strlen, p1 - pos - strlen);
                    const string strL2 = @"tooltip(this,'";
                    var pos2 = html.IndexOf(strL2, p1, StringComparison.Ordinal);
                    var p2 = html.IndexOf(@"'", pos2 + strL2.Length, StringComparison.Ordinal);
                    var text = html.Substring(pos2 + strL2.Length, p2 - pos2 - strL2.Length);
                    if (sb.Length > 0)
                    {
                        sb.Append(" | ");
                    }

                    sb.AppendFormat(@"<a href=""{0}"" style=""font-family: Verdana; font-size: 10px; color: #3564A5; white-space: nowrap;""><b>{1}</b></a>", link, text);
                }
                while (pos != -1);

                const string strEnd = @"USEMAP=""#links""></td></tr>";
                pos = html.IndexOf(strEnd, StringComparison.InvariantCultureIgnoreCase);
                if (pos != -1)
                {
                    html = html.Insert(pos + strEnd.Length,
                        @"<tr><td style=""padding: 10 10 10 10; text-align:center; font-size: 10px;"">" +
                        sb +
                        @"</td></tr>");
                }
            }
            */

            // Системное сообщение

            var sysMessage = HelperStrings.SubString(html, "<font class=nickname><font color=#cc0000><b>", "<br><br></b></font></font>");
            if (!string.IsNullOrEmpty(sysMessage))
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg), $"<font color=#cc0000><b>{sysMessage}</b></font>");
                    }
                }
                catch (InvalidOperationException)
                {
                }                
            }           

            UnderAttack.Parse(html);

            /*             
            if (AppVars.ServerDateTime >= new DateTime(2017, 11, 5))
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateWriteChatMsgDelegate(AppVars.MainForm.FormMainClose),
                            "Обновите версию ABClient!");
                    }
                }
                catch (InvalidOperationException)
                {
                }

                return null;
            }
            */

            var poisonAndCure = GetPoisonAndWounds(html);
            if (poisonAndCure != null)
                AppVars.PoisonAndWounds = poisonAndCure;

            if (!string.IsNullOrEmpty(AppVars.Profile.Complects))
            {               
                var par = AppVars.Profile.Complects.Split('|');                
                if (par.Length > 0)
                {
                    if (string.IsNullOrEmpty(AppVars.Profile.AutoWearComplect))
                    {
                        AppVars.Profile.AutoWearComplect = par[Dice.Make(par.Length)];
                        if (AppVars.MainForm != null)
                            AppVars.MainForm.WriteChatMsgSafe(
                                $"Комплект для одевания не был указан; теперь - ({AppVars.Profile.AutoWearComplect})");
                    }
                    else
                    {
                        var i = 0;
                        while (i < par.Length)
                        {
                            if (par[i].Equals(AppVars.Profile.AutoWearComplect))
                                break;
                            i++;
                        }

                        if (i == par.Length)
                        {
                            AppVars.Profile.AutoWearComplect = par[Dice.Make(par.Length)];
                            if (AppVars.MainForm != null)
                                AppVars.MainForm.WriteChatMsgSafe($"Комплект для одевания был указан неверно; теперь - ({AppVars.Profile.AutoWearComplect})");
                        }
                    }
                }
            }

            //var xhtml = "var logs = [9,[[0,\"16:35\"],[11,0,\"Попытка кражи не удалась\",0]],[[0,\"16:35\"],\"<B>Победа за</B>\",[1,1,\"Черный\",16,0,\"\"],\".\"],[";
            var robMessage = HelperStrings.SubString(html, "],[11,", "]");
            if (!string.IsNullOrEmpty(robMessage))
            {
                var args = robMessage.Split(',');
                if (args.Length == 3)
                {
                    var message = args[1];
                    if (!string.IsNullOrEmpty(message))
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg), $"Результат воровства: <font color=#cc0000><b>{message}</b></font>");
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                }
            }

            if (address.EndsWith("?mselect=15"))
            {
                html = MainPhpRoulette(html);
                goto end;
            }

            // Нужно ли обновлять ожидание боя?
            if (html.IndexOf("var arpar = [", StringComparison.CurrentCultureIgnoreCase) != -1 &&
                html.IndexOf(",\"Ожидаем начала боя!\"];", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                var data = HelperStrings.SubString(html, "var data = [", "];");
                if (!string.IsNullOrEmpty(data))
                {
                    if (
                        data.IndexOf("\"Королева Змей\"", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                        data.IndexOf("\"Хранитель Леса\"", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                        data.IndexOf("\"Громлех Синезубый\"", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                        data.IndexOf("\"Выползень\"", StringComparison.CurrentCultureIgnoreCase) != -1
                        )
                    {
                        var sb = new StringBuilder(HelperErrors.Head());
                        sb.AppendLine("Ожидаем начала боя!");
                        sb.AppendLine(@"<script language=""JavaScript"">");
                        sb.AppendLine(@"setTimeout(function(){location='./main.php'}, 1000);");
                        sb.Append(@"</script></body></html>");
                        html = sb.ToString();
                        goto end;
                    }
                }
            }

            // Нужно ли воровать ?
            if (AppVars.Profile.DoRob)
            {
                var robhtml = MainPhpRob(html);
                if (!string.IsNullOrEmpty(robhtml))
                {
                    html = robhtml;
                    goto end;
                }
            }

            // Нужно ли разделывать ?
            if (AppVars.Profile.SkinAuto)
            {
                var razhtml = MainPhpRaz(html);
                if (!string.IsNullOrEmpty(razhtml))
                {
                    html = razhtml;
                    goto end;
                }
            }

            // Инвентарь?
            if (html.IndexOf("/invent/0.gif", StringComparison.OrdinalIgnoreCase) != -1)
            {
                html = MainPhpInv(html);
            }

            html = html.Replace(AppConsts.HtmlCounters, string.Empty);

            if (html.IndexOf(
                    @"<font color=#dd0000>Внимание! Сеанс работы прерван.</b>",
                    StringComparison.OrdinalIgnoreCase) != -1)
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateGameDelegate(AppVars.MainForm.UpdateGame),
                            new object[] { "Сеанс работы прерван. Перезаход в игру" });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            if (html.IndexOf(
                    @"<font color=#cc0000><b>Ошибка при использовании. Истек срок годности зелья.",
                    StringComparison.OrdinalIgnoreCase) != -1)
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                            new object[] { "Истек срок годности зелья" });
                    }
                }
                catch (InvalidOperationException)
                {
                }

                AppTimerManager.RemoveTimerLastAdded();
            }

            if (html.IndexOf(
                    @"<font color=#cc0000><b>Ошибка при использовании. Достигнут предел одновременного использования зелий.",
                    StringComparison.OrdinalIgnoreCase) != -1)
            {
                if (AppVars.MainForm != null)
                    AppVars.MainForm.WriteChatMsgSafe("Достигнут предел одновременного использования зелий");

                AppTimerManager.RemoveTimerLastAdded();

                if (AppVars.DoSelfNevid && (AppVars.SelfNevidStage == 0))
                {
                    AppVars.SelfNevidNeed = true;
                    AppVars.SelfNevidStage = 1;
                }
            }

            if (
                (html.IndexOf(@"<font color=#cc0000><b>Ошибка при использовании. Уровень персонажа слишком мал.", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf(@"<font color=#cc0000><b>Ошибка при использовании. Союзник не находится в бою.", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf(@"<font color=#cc0000><b>Ошибка при использовании. Нет такого игрока в данный момент.", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf(@"<font color=#cc0000><b>Персонаж не имеет склонности или его нет в данном месте.", StringComparison.OrdinalIgnoreCase) != -1) ||
                (html.IndexOf(@"<font color=#cc0000><b>У Персонажа уровень выше Вашего.", StringComparison.OrdinalIgnoreCase) != -1))                
            {
                if (AppVars.MainForm != null)
                    AppVars.MainForm.FastCancelSafe();
            }

            if (
                (html.IndexOf(@"<font color=#cc0000><b>Ошибка при использовании. Нельзя вмешаться в закрытый бой.", StringComparison.OrdinalIgnoreCase) != -1) &&
                (AppVars.AutoAttackToolId != 0)
                )
            {
                if (AppVars.MainForm != null)
                {
                    RoomManager.CharAddToBlackList(AppVars.FastNick);
                    AppVars.MainForm.WriteChatMsgSafe(string.Format("<b>{0}</b> в бою, отменяем действие!", AppVars.FastNick));
                    AppVars.MainForm.FastCancelSafe();
                }
            }

            if (
                (html.IndexOf(
                    @"<font color=#cc0000><b>Ошибка при использовании. Невозможно использовать зелье в данный момент.",
                    StringComparison.OrdinalIgnoreCase) != -1))
            {
                if (AppVars.DoSelfNevid && (AppVars.SelfNevidStage == 1))
                    AppVars.SelfNevidNeed = true;
            }

            if (html.IndexOf(
                    @"<font color=#cc0000><b>Предмет успешно использован.",
                    StringComparison.OrdinalIgnoreCase) != -1)
            {
                /*
                if (AppVars.MainForm != null) 
                    AppVars.MainForm.WriteChatMsgSafe("Предмет успешно использован");
                 */ 
            }

            if (!string.IsNullOrEmpty(AppVars.CureNick))
            {
                if (html.IndexOf("<font color=#cc0000><b>Поздравляем, всё успешно.<br>", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    AppVars.CureNickDone = AppVars.CureNick;
                    AppVars.CureNick = string.Empty;
                }

                if (html.IndexOf("<font color=#cc0000><b>Ошибка. Персонаж находится в бою.<br>", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    AppVars.CureNickBoi = AppVars.CureNick;
                    AppVars.CureNick = string.Empty;
                }
            }

            var inshp = html.IndexOf("ins_HP(", StringComparison.OrdinalIgnoreCase);
            if (inshp != -1)
            {
                MainPhpInsHp(html, inshp + "ins_HP(".Length);
            }

            if (!string.IsNullOrEmpty(AppVars.UsersOnline))
            {
                const string hpfont = "<td rowspan=3> <div id=hbar><font class=hpfont>: </div></td>";
                var hpfontpos = html.IndexOf(hpfont, StringComparison.OrdinalIgnoreCase);
                if (hpfontpos != -1)
                {
                    hpfontpos += hpfont.Length;
                    html = html.Insert(
                        hpfontpos,
                        string.Format(CultureInfo.InvariantCulture,
                            "<td rowspan=3><div><img src=http://image.neverlands.ru/1x1.gif width=8 height=1><font class=hpfont>[<font color=#ACAAA3>&nbsp;<b>{0}</b>&nbsp;</font>]</font></div></td>", 
                            AppVars.UsersOnline));
                }
            }

            if (AppVars.Profile.TorgActive && TorgList.Trigger && address.StartsWith("http://www.neverlands.ru/main.php?get_id=0&", StringComparison.OrdinalIgnoreCase))
            {
                if (html.IndexOf("<font color=#cc0000>Сделка удачно завершена.", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    Chat.AddAnswer(TorgList.MessageThanks);
                    TorgList.Trigger = false;    
                }
                else
                {
                    if (html.IndexOf("<font color=#cc0000>У Вас не хватает средств для завершения сделки.", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        Chat.AddAnswer(TorgList.MessageNoMoney);
                        TorgList.Trigger = false;
                        TorgList.TriggerBuy = false;
                    }                    
                }
            }

            if (html.IndexOf("magic_slots();", StringComparison.OrdinalIgnoreCase) != -1)
            {
                // Мы находимся в бою
                html = MainPhpFight(html);
                goto end;
            }

            //if (AppVars.Profile.DoStopOnDig && (Dice.Make(10) == 0))
            if (AppVars.Profile.DoStopOnDig && (html.IndexOf("[\"dig\",\"Копать\",", StringComparison.Ordinal) != -1))
            {
                AppVars.AutoMoving = false;
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateFishOffDelegate(AppVars.MainForm.UpdateNavigatorOff),
                            new object[] {});
                    }
                }
                catch (InvalidOperationException)
                {
                }

                EventSounds.PlayAlarm();
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg),
                            new object[] { "На текущей клетке обнаружен клад!" });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            if (AppVars.Profile.FishAuto)
            {
                if (
                    (AppVars.Profile.FishStopOverWeight && html.IndexOf("<font color=#CC0000>Внимание! Возможен перегруз.", StringComparison.OrdinalIgnoreCase) != -1) ||
                    html.IndexOf("<font color=#CC0000><b>У Вас нет рыболовных снастей.", StringComparison.OrdinalIgnoreCase) != -1 ||
                    html.IndexOf("<font color=#CC0000><b>У Вас нет приманки, чтобы ловить рыбу.", StringComparison.OrdinalIgnoreCase) != -1 ||
                    html.IndexOf("<font color=#CC0000><b>Приманок нет в наличии.", StringComparison.OrdinalIgnoreCase) != -1 ||
                    html.IndexOf("<font color=#CC0000><b>У Вас не хватает умения, чтобы ловить тут рыбу.", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateFishOffDelegate(AppVars.MainForm.UpdateFishOff),
                                new object[] { });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    goto end;
                }
            }

            var postied = html.IndexOf("Усталость:</td><td bgcolor=#336699 nowrap><font class=proce><font color=#ffffff><b><div align=center>&nbsp;<b>", StringComparison.OrdinalIgnoreCase);
            if (postied != -1)
            {
                MainPhpTied(html, postied + "Усталость:</td><td bgcolor=#336699 nowrap><font class=proce><font color=#ffffff><b><div align=center>&nbsp;<b>".Length);
            }

            // Надо ли лечить?
            if (AppVars.CureNeed && (DateTime.Now > AppVars.NeverTimer))
            {
                var invHtml = MainPhpFindInv(html, "&im=0&wca=85");
                if (!string.IsNullOrEmpty(invHtml))
                {
                    html = invHtml;
                    goto end;
                }

                if (MainPhpIsInv(html))
                {
                    var cureHtml = MainPhpCure(html);
                    if (string.IsNullOrEmpty(cureHtml))
                    {
                        if (!address.EndsWith("im=0&wca=85"))
                        {
                            html = BuildRedirect("Переключение на аптечки", "main.php?im=0&wca=85");
                            goto end;
                        }

                        AppVars.MainForm.WriteChatMsgSafe("Подходящая аптечка не найдена!");
                        AppVars.CureNeed = false;
                    }
                    else
                    {
                        AppVars.CureNeed = false;
                        html = cureHtml;
                        goto end;
                    }
                }
            }

            if (AppVars.Profile.ChatKeepMoving)
            {
                html = html.Replace("top.clr_chat();", string.Empty);
                html = html.Replace("parent.clr_chat();", string.Empty);
            }

            // Читаем умелку со страницы

            var sust = HelperStrings.SubString(
                html,
                "Рыбалка</td><td bgcolor=#FCFAF3><font class=proce><font color=#555555><div align=center>[",
                "]");

            if (!string.IsNullOrEmpty(sust))
            {
                int fishUm;
                if (int.TryParse(sust, out fishUm))
                {
                    AppVars.Profile.FishUm = fishUm;
                    AppVars.AutoFishCheckUm = false;
                }
            }

            sust = HelperStrings.SubString(
                html,
                "Охота</td><td bgcolor=#FCFAF3><font class=proce><font color=#555555><div align=center>[",
                "]");

            if (!string.IsNullOrEmpty(sust))
            {
                int skinUm;
                if (int.TryParse(sust, out skinUm))
                {
                    AppVars.AutoSkinCheckUm = false;
                    if (AppVars.SkinUm != skinUm)
                    {
                        var sb = new StringBuilder($"Умение разделки: <span style=\"color:#009933;font-weight:bold;\">{skinUm}</span>");
                        if (AppVars.SkinUm > 0 && AppVars.SkinUm < skinUm)
                        {
                            var diff = skinUm - AppVars.SkinUm;
                            sb.Append($" (+{diff})");
                        }

                        AppVars.SkinUm = skinUm;
                        if (AppVars.Profile.SkinAuto)
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg),
                                        sb.ToString());
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                    }
                }
            }

            // Читаем список комплектов

            var complects = GetComplects(html);
            if (complects != null)
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateComplectsDelegate(AppVars.MainForm.UpdateComplects),
                            new object[] { complects });
                    }
                }catch (InvalidOperationException)
                {
                }                
            }

            // Нужно ли одеть комплект ?

            if (!string.IsNullOrEmpty(AppVars.WearComplect) && (DateTime.Now > AppVars.NeverTimer))
            {
                var invHtml = MainPhpFindInv(html, "&im=0&wca=4");
                if (!string.IsNullOrEmpty(invHtml))
                {
                    html = invHtml;
                    goto end;
                }

                if (MainPhpIsInv(html))
                {
                    var wearHtml = MainPhpWearComplect(html, AppVars.WearComplect);
                    if (string.IsNullOrEmpty(wearHtml))
                    {
                        if (!address.EndsWith("&im=0&wca=4"))
                        {
                            html = BuildRedirect("Переключение на вещи", "main.php?im=0&wca=4");
                            goto end;
                        }

                        AppVars.MainForm.WriteChatMsgSafe(string.Format("Невозможно одеть комплект ({0})", AppVars.WearComplect));
                        AppVars.WearComplect = string.Empty;
                    }
                    else
                    {
                        AppVars.MainForm.WriteChatMsgSafe(string.Format("Одеваем комплект ({0})...", AppVars.WearComplect));
                        AppVars.WearComplect = string.Empty;
                        html = wearHtml;
                        goto end;
                    }
                }
            }

            // Нужно ли выпить блаж (авто зелик/элик) ?

            if ((AppVars.Profile.DoAutoDrinkBlaz) && (AppVars.Tied >= AppVars.Profile.AutoDrinkBlazTied) && (DateTime.Now > AppVars.NeverTimer))
            {
                if (!MainPhpIsInv(html))
                { 
                    var invHtml = MainPhpFindInv(html, AppVars.Profile.AutoDrinkBlazOrder == 0 ? "&im=0&wca=27" : "&im=6");
                    if (!string.IsNullOrEmpty(invHtml))
                    {
                        html = invHtml;
                        goto end;
                    }
                }

                if (MainPhpIsInv(html))
                {
                    var cureHtml = MainPhpDrinkBlazPotOrElixir(html);
                    if (string.IsNullOrEmpty(cureHtml))
                    {
                        if (AppVars.Profile.AutoDrinkBlazOrder == 0)
                        {
                            if (!AppVars.DrinkBlazPotOrElixirFirst && !address.EndsWith("im=0&wca=27"))
                            {
                                AppVars.DrinkBlazPotOrElixirFirst = true;
                                html = BuildRedirect("Переключение на зелья", "main.php?im=0&wca=27");
                                goto end;
                            }

                            if (address.EndsWith("im=0&wca=27"))
                            {
                                html = BuildRedirect("Переключение на элексиры", "main.php?im=6");
                                goto end;
                            }
                        }
                        else
                        {
                            if (!AppVars.DrinkBlazPotOrElixirFirst && !address.EndsWith("im=6"))
                            {
                                AppVars.DrinkBlazPotOrElixirFirst = true;
                                html = BuildRedirect("Переключение на элексиры", "main.php?im=6");
                                goto end;
                            }

                            if (address.EndsWith("im=6"))
                            {
                                html = BuildRedirect("Переключение на зелья", "main.php?im=0&wca=27");
                                goto end;
                            }
                        }

                        AppVars.DrinkBlazPotOrElixirFirst = false;
                        AppVars.MainForm.WriteChatMsgSafe("Ни зелье ни эликсир блаженства не найдены. Автопитье блажа отключено. Не забудьте включить его обратно.");
                        AppVars.Profile.DoAutoDrinkBlaz = false;
                    }
                    else
                    {
                        AppVars.Tied = 0;
                        html = cureHtml;
                        goto end;
                    }
                }
            }

            // Нужно ли войти в невидимость?

            if (AppVars.SelfNevidNeed && (DateTime.Now > AppVars.NeverTimer))
            {
                while (AppVars.SelfNevidStage < 4)
                {
                    // Зелик невидимости
                    if (AppVars.SelfNevidStage == 0)
                    {
                        var invHtml = MainPhpFindInv(html, "&im=0&wca=27");
                        if (!string.IsNullOrEmpty(invHtml))
                        {
                            html = invHtml;
                            goto end;
                        }

                        if (MainPhpIsInv(html))
                        {
                            var fastHtml = MainPhpNevidPotion(html);                            
                            if (string.IsNullOrEmpty(fastHtml))
                            {
                                if (!address.EndsWith("im=0&wca=27"))
                                {
                                    html = BuildRedirect("Переключение на зелья", "main.php?im=0&wca=27");
                                    goto end;
                                }

                                // Зелье невидимости не обнаружено
                                AppVars.SelfNevidStage++;
                            }
                            else
                            {
                                if (AppVars.MainForm != null)
                                    AppVars.MainForm.WriteChatMsgSafe("Мы не в невиде, используем <b><font color=#610B5E>зелье невидимости</font></b> на себя");

                                AppVars.SelfNevidNeed = false;
                                html = fastHtml;
                                goto end;
                            }
                        }
                    }

                    // Свиток тумана
                    if (AppVars.SelfNevidStage == 1)
                    {
                        if (AppVars.SelfNevidSkl.StartsWith("Дети", StringComparison.CurrentCultureIgnoreCase))
                        {                            
                            var invHtml = MainPhpFindInv(html, "&im=0&wca=28");
                            if (!string.IsNullOrEmpty(invHtml))
                            {
                                html = invHtml;
                                goto end;
                            }

                            if (MainPhpIsInv(html))
                            {
                                var fastHtml = MainPhpSelfSviFog(html);
                                if (string.IsNullOrEmpty(fastHtml))
                                {
                                    if (!address.EndsWith("im=0&wca=28"))
                                    {
                                        html = BuildRedirect("Переключение на свитки", "main.php?im=0&wca=28");
                                        goto end;
                                    }

                                    // Свиток тумана не обнаружен
                                    AppVars.SelfNevidStage++;
                                    continue;
                                }

                                if (AppVars.MainForm != null)
                                    AppVars.MainForm.WriteChatMsgSafe("Мы не в невиде, используем <b><font color=#610B5E>свиток тумана</font></b> на себя");

                                AppVars.SelfNevidNeed = false;
                                html = fastHtml;
                                goto end;
                            }
                        }
                        else
                        {
                            AppVars.SelfNevidStage++;
                        }
                    }

                    // Свиток невидимости
                    if (AppVars.SelfNevidStage == 2)
                    {
                        var invHtml = MainPhpFindInv(html, "&im=0&wca=28");
                        if (!string.IsNullOrEmpty(invHtml))
                        {
                            html = invHtml;
                            goto end;
                        }

                        if (MainPhpIsInv(html))
                        {
                            var fastHtml = MainPhpSviNevidFourHour(html);
                            if (string.IsNullOrEmpty(fastHtml))
                            {
                                if (!address.EndsWith("im=0&wca=28"))
                                {
                                    html = BuildRedirect("Переключение на свитки", "main.php?im=0&wca=28");
                                    goto end;
                                }

                                // Свиток невидимости не обнаружен
                                AppVars.SelfNevidStage++;
                            }
                            else
                            {
                                if (AppVars.MainForm != null)
                                    AppVars.MainForm.WriteChatMsgSafe("Мы не в невиде, используем <b><font color=#610B5E>свиток невидимости 4 часа</font></b> на себя");

                                AppVars.SelfNevidNeed = false;
                                html = fastHtml;
                                goto end;
                            }
                        }

                        // Проверяем абилку тумана
                        if (AppVars.SelfNevidStage == 3)
                        {
                            // !!! AppVars.SelfNevidSkl = "Дети Сумерек";
                            if (AppVars.SelfNevidSkl.Equals("Дети Сумерек", StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (address.EndsWith("main.php?useaction=addon-action&addid=1",
                                    StringComparison.OrdinalIgnoreCase))
                                {
                                    var darkfoghtml = MainPhpDarkFog(html);
                                    if (!string.IsNullOrEmpty(darkfoghtml))
                                    {
                                        if (AppVars.MainForm != null)
                                            AppVars.MainForm.WriteChatMsgSafe(
                                                "Мы не в невиде, применяем <b><font color=#610B5E>сумеречный туман</b></font> на себя!");

                                        AppVars.SelfNevidNeed = false;
                                        html = darkfoghtml;
                                        goto end;
                                    }

                                    // Сумеречный туман сейчас недоступен
                                    AppVars.SelfNevidStage++;
                                    continue;
                                }

                                if (address.EndsWith("main.php?useaction=addon-action", StringComparison.OrdinalIgnoreCase))
                                {
                                    html = BuildRedirect("Переключение на абилки", "main.php?useaction=addon-action&addid=1");
                                    goto end;
                                }

                                html = BuildRedirect("Переключение на возможности", "main.php?useaction=addon-action");
                                goto end;
                            }

                            // Мы не дети сумерек, у нас нет сумеречного тумана
                            AppVars.SelfNevidStage++;
                        }
                    }
                }

                if (AppVars.SelfNevidStage == 4)
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.WriteChatMsgSafe(
                            "Ни один способ ухода в невид (абилка и свиток тумана, зелье и свиток невида) не обнаружен. Автоуход в невид отключен. Не забудьте включить его обратно.");
                        AppVars.MainForm.SelfNevidOffSafe();
                    }
                }
            }

            // Нужно ли продать вещь?
            /*
            if (TorgList.TriggerBuy)
            {
                var invhtml = MainPhpFindInv(html, "im=0");
                if (!string.IsNullOrEmpty(invhtml))
                {
                    html = invhtml;
                    goto end;
                }

                if (MainPhpIsInv(html))
                {
                    if (html.Contains("?im=0") && !html.Contains("?wfo=1"))
                    {
                        html = BuildRedirect("Переключение на вещи", "main.php?im=0");
                        goto end;
                    }

                    TorgList.TriggerBuy = false;
                    var sellLinkPrefix = "main.php?get_id=8&uid=" + TorgList.UidThing;
                    var sellLink = HelperStrings.SubString(html, sellLinkPrefix, "'");
                    if (sellLink != null)
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                    new object[] { "Продажа купленной вещи в лавку" });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        html = BuildRedirect("Продажа купленной вещи в лавку", sellLinkPrefix + sellLink);
                        goto end;                
                    }
                }
            }
             */ 

            if (AppVars.SwitchToPerc && (DateTime.Now > AppVars.NeverTimer))
            {
                var newhtml = MainPhpFindPerc(html);
                if (!string.IsNullOrEmpty(newhtml))
                {
                    AppVars.SwitchToPerc = false;
                    html = newhtml;
                    goto end;
                }
            }
            else
            {
                if (AppVars.SwitchToFlora)
                {
                    var newhtml = MainPhpFindFlora(html);
                    if (!string.IsNullOrEmpty(newhtml))
                    {
                        AppVars.SwitchToFlora = false;
                        html = newhtml;
                        goto end;
                    }
                }
            }

            // Переключения перед разделкой

            if (AppVars.Profile.SkinAuto && (DateTime.Now > AppVars.NeverTimer))
            {
                // Надо прочтитать умелку?
                if (AppVars.AutoSkinCheckUm && (DateTime.Now > AppVars.NeverTimer))
                {
                    var phtml = MainPhpFindPerc(html);
                    if (!string.IsNullOrEmpty(phtml))
                    {
                        html = phtml;
                        goto end;
                    }

                    if (
                        html.IndexOf(@"<input type=button class=lbut value=""Умения"" onclick", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        html = BuildRedirect("Переключение на умения персонажа", "main.php?mselect=1");
                        goto end;
                    }
                }

                // Считываем охотничьи ресурсы
                if (AppVars.AutoSkinCheckRes)
                {
                    var invHtml = MainPhpFindInv(html, "&im=5");
                    if (!string.IsNullOrEmpty(invHtml))
                    {
                        html = invHtml;
                        goto end;
                    }
                    
                    if (MainPhpIsInv(html))
                    {
                        AppVars.AutoSkinCheckRes = false;
                        MainPhpGetSkinRes(html);
                    }
                }

                // Надо одеть нож?

                if (AppVars.AutoSkinCheckKnife && (DateTime.Now > AppVars.NeverTimer))
                {
                    var perchtml = MainPhpFindPerc(html);
                    if (!string.IsNullOrEmpty(perchtml))
                    {
                        html = perchtml;
                        goto end;
                    }

                    AppVars.AutoSkinArmedKnife = false;
                    if (MainPhpIsPerc(html))
                    {
                        AppVars.AutoSkinArmedKnife = MainPhpArmedKinfe(html);
                        AppVars.AutoSkinCheckKnife = false;
                    }
                }

                // Одеваем нож
                
                if (!AppVars.AutoSkinArmedKnife && (DateTime.Now > AppVars.NeverTimer))
                {
                    var invHtml = MainPhpFindInv(html, "&im=0&wca=4");
                    if (!string.IsNullOrEmpty(invHtml))
                    {
                        html = invHtml;
                        goto end;
                    }

                    if (MainPhpIsInv(html))
                    {
                        invHtml = MainPhpWearKnife(html);
                        if (string.IsNullOrEmpty(invHtml))
                        {
                            if (!address.EndsWith("im=0&wca=4"))
                            {
                                html = BuildRedirect("Переключение на вещи", "main.php?im=0&wca=4");
                                goto end;
                            }
                        }
                        else
                        {
                            AppVars.AutoSkinCheckKnife = true;
                            html = invHtml;
                            goto end;
                        }
                    }
                }
            }

            // Переключения перед забросом удочки

            if (AppVars.Profile.FishAuto && (DateTime.Now > AppVars.NeverTimer))
            {
                // Нормальная для заброса усталость?
                if (!AppVars.AutoFishDrink)
                {
                    AppVars.AutoFishDrink = (AppVars.Tied > AppVars.Profile.FishTiedHigh) && AppVars.Profile.FishTiedZero;
                }

                if ((AppVars.Tied > AppVars.Profile.FishTiedHigh) ||
                    AppVars.AutoDrink ||
                    AppVars.AutoFishDrink)
                {
                    var newhtml = MainPhpFindDrink(html);
                    if (!string.IsNullOrEmpty(newhtml))
                    {
                        html = newhtml;
                        AppVars.AutoFishDrinkOnce = true;
                        AppVars.SwitchToPerc = true;

                        if (AppVars.Profile.ShowTrayBaloons)
                        {
                            var sbu = new StringBuilder();
                            sbu.Append("Усталость: ");
                            sbu.Append(AppVars.Tied);
                            sbu.AppendLine();
                            if (AppVars.AutoDrink || AppVars.AutoFishDrink)
                            {
                                sbu.Append("Пьем до нуля");
                            }
                            else
                            {
                                sbu.Append("Делаем глоток");
                            }

                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon),
                                        new object[] { sbu.ToString() });
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }

                        goto end;
                    }

                    newhtml = MainPhpFindFlora(html);
                    if (!string.IsNullOrEmpty(newhtml))
                    {
                        html = newhtml;
                        goto end;
                    }

                    if (html.IndexOf(" id=wtime>", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        html = MainPhpWtime(address, html);
                        goto end;
                    }
                }

                // Надо прочтитать умелку?
                if (AppVars.AutoFishCheckUm && (DateTime.Now > AppVars.NeverTimer))
                {
                    var phtml = MainPhpFindPerc(html);
                    if (!string.IsNullOrEmpty(phtml))
                    {
                        html = phtml;
                        goto end;
                    }

                    if (
                        html.IndexOf(@"<input type=button class=lbut value=""Умения"" onclick", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        html = BuildRedirect("Переключение на умения персонажа", "main.php?mselect=1");
                        goto end;
                    }
                }

                // Надо переодеться?

                if (AppVars.AutoFishCheckUd && (DateTime.Now > AppVars.NeverTimer))
                {
                    var perchtml = MainPhpFindPerc(html);
                    if (!string.IsNullOrEmpty(perchtml))
                    {
                        html = perchtml;
                        goto end;
                    }

                    AppVars.AutoFishWearUd = false;
                    if (MainPhpIsPerc(html))
                    {
                        AppVars.AutoFishWearUd = MainPhpIsMustWearUd(html);
                        AppVars.AutoFishCheckUd = false;
                    }
                }

                if (AppVars.AutoFishWearUd && (DateTime.Now > AppVars.NeverTimer))
                {
                    var invHtml = MainPhpFindInv(html, "&im=0&wca=4");
                    if (!string.IsNullOrEmpty(invHtml))
                    {
                        html = invHtml;
                        goto end;
                    }

                    if (MainPhpIsInv(html))
                    {
                        invHtml = MainPhpWearUd(html);
                        if (string.IsNullOrEmpty(invHtml))
                        {
                            if (!address.EndsWith("im=0&wca=4"))
                            {
                                html = BuildRedirect("Переключение на вещи", "main.php?im=0&wca=4");
                                goto end;
                            }
                        }
                        else
                        {
                            html = invHtml;
                            goto end;
                        }
                    }
                }
            }

            // Быстрые абилки, их нужно проверять после боя

            if (AppVars.FastNeedAbilDarkTeleport || AppVars.FastNeedAbilDarkFog)
            {
                if (address.EndsWith("main.php?useaction=addon-action&addid=1", StringComparison.OrdinalIgnoreCase))
                {
                    if (AppVars.FastNeedAbilDarkTeleport)
                    {
                        AppVars.FastNeedAbilDarkTeleport = false;
                        var darkteleporthtml = MainPhpDarkTeleport(html);
                        if (!string.IsNullOrEmpty(darkteleporthtml))
                        {
                            html = darkteleporthtml;
                            goto end;
                        }

                        AppVars.MainForm.WriteChatMsgSafe("Нет возможности применить сумеречный телепорт!");                        
                    }

                    if (AppVars.FastNeedAbilDarkFog)
                    {
                        AppVars.FastNeedAbilDarkFog = false;
                        var darkfoghtml = MainPhpDarkFog(html);
                        if (!string.IsNullOrEmpty(darkfoghtml))
                        {
                            html = darkfoghtml;
                            goto end;
                        }

                        AppVars.MainForm.WriteChatMsgSafe("Нет возможности применить сумеречный туман!");                        
                    }
                }
                else
                {
                    if (address.EndsWith("main.php?useaction=addon-action", StringComparison.OrdinalIgnoreCase))
                    {
                        html = BuildRedirect("Переключение на абилки", "main.php?useaction=addon-action&addid=1");
                        goto end;
                    }

                    html = BuildRedirect("Переключение на возможности", "main.php?useaction=addon-action");
                    goto end;
                }
            }

            // Автолечение ядов и небоевых травм

            if (AppVars.Profile.DoAutoCure)
            {
                // Есть ли отравление ?
                if (AppVars.PoisonAndWounds[0] > 0)
                {
                    var invhtml = MainPhpFindInv(html, "&im=0&wca=27");
                    if (!string.IsNullOrEmpty(invhtml))
                    {
                        html = invhtml;
                        goto end;
                    }

                    if (MainPhpIsInv(html))
                    {
                        var cureHtml = MainPhpRemovePoison(html);
                        if (string.IsNullOrEmpty(cureHtml))
                        {
                            if (!address.EndsWith("im=0&wca=27"))
                            {
                                html = BuildRedirect("Переключение на зелья", "main.php?im=0&wca=27");
                                goto end;
                            }

                            AppVars.MainForm.WriteChatMsgSafe("У вас отравление и нет зелья лечения отравлений! Автолечение отключено. Не забудьте включить его обратно.");
                            AppVars.PoisonAndWounds[0] = 0;
                            AppVars.Profile.DoAutoCure = false;
                            if ((AppVars.PoisonAndWounds[1] == 0) && (AppVars.PoisonAndWounds[2] == 0) && (AppVars.PoisonAndWounds[3] == 0))
                                AppVars.WearComplect = AppVars.Profile.AutoWearComplect;
                        }
                        else
                        {
                            AppVars.MainForm.WriteChatMsgSafe("Лечим свое отравление...");
                            AppVars.PoisonAndWounds[0]--;

                            if ((AppVars.PoisonAndWounds[1] == 0) && (AppVars.PoisonAndWounds[2] == 0) && (AppVars.PoisonAndWounds[3] == 0))
                                AppVars.WearComplect = AppVars.Profile.AutoWearComplect;

                            html = cureHtml;
                            goto end;                            
                        }
                    }
                }
                else
                {
                    // Есть ли травма ? 
                    if ((AppVars.PoisonAndWounds[1] > 0) || (AppVars.PoisonAndWounds[2] > 0) || (AppVars.PoisonAndWounds[3] > 0))
                    {
                        var invhtml = MainPhpFindInv(html, "&im=0&wca=85");
                        if (!string.IsNullOrEmpty(invhtml))
                        {
                            html = invhtml;
                            goto end;
                        }

                        if (MainPhpIsInv(html))
                        {
                            AppVars.CureNeed = true;
                            AppVars.CureNick = AppVars.Profile.UserNick;
                            if (AppVars.PoisonAndWounds[1] > 0)
                            {
                                AppVars.CureTravm = "1";                                
                            }
                            else if (AppVars.PoisonAndWounds[2] > 0)
                            {
                                AppVars.CureTravm = "2";                                
                            }
                            else if (AppVars.PoisonAndWounds[3] > 0)
                            {
                                AppVars.CureTravm = "3";                                
                            }

                            var cureHtml = MainPhpCure(html);
                            if (string.IsNullOrEmpty(cureHtml))
                            {
                                if (!address.EndsWith("im=0&wca=85"))
                                {
                                    html = BuildRedirect("Переключение на аптечки", "main.php?im=0&wca=85");
                                    goto end;
                                }

                                AppVars.MainForm.WriteChatMsgSafe("У вас травма, но нет возможности ее вылечить! Автолечение отключено. Не забудьте включить его обратно.");
                                AppVars.Profile.DoAutoCure = false;
                                AppVars.CureNeed = false;
                                AppVars.PoisonAndWounds[1] = AppVars.PoisonAndWounds[2] = AppVars.PoisonAndWounds[3] = 0;
                            }
                            else
                            {
                                switch (AppVars.CureTravm)
                                {
                                    case "1":
                                        AppVars.PoisonAndWounds[1]--;
                                        AppVars.MainForm.WriteChatMsgSafe("Лечим свою легкую травму...");
                                        break;
                                    case "2":
                                        AppVars.PoisonAndWounds[2]--;
                                        AppVars.MainForm.WriteChatMsgSafe("Лечим свою среднюю травму...");
                                        break;
                                    case "3":
                                        AppVars.PoisonAndWounds[3]--;
                                        AppVars.MainForm.WriteChatMsgSafe("Лечим свою тяжелую травму...");
                                        break;
                                }

                                if ((AppVars.PoisonAndWounds[1] == 0) && (AppVars.PoisonAndWounds[2] == 0) && (AppVars.PoisonAndWounds[3] == 0))
                                    AppVars.WearComplect = AppVars.Profile.AutoWearComplect;

                                html = cureHtml;
                                goto end;
                            }
                        }
                    }
                }
            }

            // Заказано ли быстрое действие?

            if (AppVars.FastNeed)
            {
                if (DateTime.Now > AppVars.NeverTimer)
                {
                    string invHtml, fastHtml;

                    // Определяем, на что мы должны переключиться
                    switch (AppVars.FastId)
                    {
                        case "i_w28_22.gif": // Свиток телепорта
                        case "i_w28_23.gif": // Свиток саморассеивания
                        case "i_w28_28.gif": // Свиток обнаружения
                        case "i_svi_213.gif": // Свиток искажающего тумана
                        case "i_svi_001.gif": // Обычная нападалка
                        case "i_svi_002.gif": // Кровавая нападалка
                        case "i_w28_26.gif": // Боевая нападалка
                        case "i_w28_26X.gif": // Закрытая боевая нападалка
                        case "i_w28_24.gif": // Кулачка
                        case "i_w28_25.gif": // Закрытая кулачка
                        case "i_svi_205.gif": // Закрытая нападалка
                        case "i_w28_27.gif": // Свиток защиты
                        case "Телепорт (Остров Туротор)":
                        case "i_w28_86.gif": // Портал
                            // Работаем со свитками
                            invHtml = MainPhpFindInv(html, "&im=0&wca=28");
                            if (!string.IsNullOrEmpty(invHtml))
                            {
                                html = invHtml;
                                goto end;
                            }

                            if (MainPhpIsInv(html))
                            {
                                fastHtml = MainPhpFast(html);
                                if (string.IsNullOrEmpty(fastHtml))
                                {
                                    if (!address.EndsWith("im=0&wca=28"))
                                    {
                                        html = BuildRedirect("Переключение на свитки", "main.php?im=0&wca=28");
                                        goto end;
                                    }

                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.WriteChatMsgSafe("Свиток не обнаружен, действие отменено.");
                                        AppVars.MainForm.FastCancelSafe();
                                    }
                                }
                                else
                                {
                                    if (AppVars.MainForm != null && AppVars.FastNick != null)
                                        AppVars.MainForm.WriteChatMsgSafe($"Используем свиток на <b>{AppVars.FastNick}</b>");

                                    AppVars.FastCount--;
                                    if (AppVars.FastCount == 0)
                                    {
                                        if (AppVars.MainForm != null)
                                            AppVars.MainForm.FastCancelSafe();
                                    }

                                    html = fastHtml;
                                    goto end;                                    
                                }
                            }

                            break;
                        case "Яд":
                        case "Зелье Сильной Спины":
                        case "Зелье Невидимости":
                        case "Зелье Блаженства":
                        case "Зелье Метаболизма":
                        case "Зелье Просветления":
                        case "Зелье Сокрушительных Ударов":
                        case "Зелье Стойкости":
                        case "Зелье Недосягаемости":
                        case "Зелье Точного Попадания":
                        case "Зелье Ловких Ударов":
                        case "Зелье Мужества":
                        case "Зелье Жизни":
                        case "Зелье Лечения":
                        case "Зелье Восстановления Маны":
                        case "Зелье Энергии":
                        case "Зелье Удачи":
                        case "Зелье Силы":
                        case "Зелье Ловкости":
                        case "Зелье Гения":
                        case "Зелье Боевой Славы":
                        case "Зелье Секрет Волшебника":
                        case "Зелье Медитации":
                        case "Зелье Иммунитета":
                        case "Зелье Лечения Отравлений":
                        case "Зелье Огненного Ореола":
                        case "Зелье Колкости":
                        case "Зелье Загрубелой Кожи":
                        case "Зелье Панциря":
                        case "Зелье Человек-гора":
                        case "Зелье Скорости":
                        case "Жажда Жизни":
                        case "Ментальная Жажда":
                        case "Зелье подвижности":
                        case "Ярость Берсерка":
                        case "Зелье Хрупкости":
                        case "Зелье Мифриловый Стержень":
                        case "Зелье Соколиный взор":
                        case "Секретное Зелье":
                            // Работаем со зельями
                            invHtml = MainPhpFindInv(html, "&im=0&wca=27");
                            if (!string.IsNullOrEmpty(invHtml))
                            {
                                html = invHtml;
                                goto end;
                            }

                            if (MainPhpIsInv(html))
                            {
                                fastHtml = MainPhpFast(html);
                                if (string.IsNullOrEmpty(fastHtml))
                                {
                                    if (!address.EndsWith("im=0&wca=27"))
                                    {
                                        html = BuildRedirect("Переключение на зелья", "main.php?im=0&wca=27");
                                        goto end;
                                    }

                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.FastCancelSafe();
                                        AppVars.MainForm.WriteChatMsgSafe("Зелье не обнаружено, действие отменено.");
                                    }
                                }
                                else
                                {
                                    if (AppVars.MainForm != null && AppVars.FastId != null && AppVars.FastNick != null)
                                        AppVars.MainForm.WriteChatMsgSafe(
                                            $"Используем <b><font color=#610B5E>{AppVars.FastId}</font></b> на <b>{AppVars.FastNick}</b>");

                                    AppVars.FastCount--;
                                    if (AppVars.FastCount == 0)
                                    {
                                        if (AppVars.MainForm != null)
                                            AppVars.MainForm.FastCancelSafe();
                                    }
                                   
                                    html = fastHtml;
                                    goto end;
                                }
                            }

                            break;
                        case "Эликсир Блаженства":
                        case "Эликсир Мгновенного Исцеления":
                        case "Эликсир Восстановления":
                        case AppConsts.Bait:
                            // Работаем со эликсирами
                            invHtml = MainPhpFindInv(html, "&im=6");
                            if (!string.IsNullOrEmpty(invHtml))
                            {
                                html = invHtml;
                                goto end;
                            }

                            if (MainPhpIsInv(html))
                            {
                                fastHtml = MainPhpFast(html);
                                if (string.IsNullOrEmpty(fastHtml))
                                {
                                    if (!address.EndsWith("im=6"))
                                    {
                                        html = BuildRedirect("Переключение на эликсиры", "main.php?im=6");
                                        goto end;
                                    }

                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.FastCancelSafe();
                                        AppVars.MainForm.WriteChatMsgSafe(AppVars.FastId == AppConsts.Bait
                                            ? "Приманка не обнаружена, действие отменено."
                                            : "Эликсир не обнаружен, действие отменено.");
                                    }
                                }
                                else
                                {
                                    if (AppVars.MainForm != null && AppVars.FastId != null && AppVars.FastNick != null)
                                        AppVars.MainForm.WriteChatMsgSafe(
                                            $"Используем <b><font color=#610B5E>{AppVars.FastId}</font></b> на <b>{AppVars.FastNick}</b>");

                                    AppVars.FastCount--;
                                    if (AppVars.FastCount == 0)
                                    {
                                        if (AppVars.MainForm != null)
                                            AppVars.MainForm.FastCancelSafe();
                                    }

                                    html = fastHtml;
                                    goto end;
                                }
                            }

                            break;
                        case "Тотем":
                            // Работаем с тотемным нападением
                            if (DateTime.Now > AppVars.NeverTimer)
                            {
                                var newhtml = MainPhpFindFlora(html);
                                if (!string.IsNullOrEmpty(newhtml))
                                {
                                    html = newhtml;
                                    goto end;
                                }

                                fastHtml = MainPhpFast(html);
                                if (string.IsNullOrEmpty(fastHtml))
                                {
                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.FastCancelSafe();
                                        AppVars.MainForm.WriteChatMsgSafe(
                                            "Нападение по тотему сейчас невозможно, действие отменено.");
                                    }
                                }
                                else
                                {
                                    if (DateTime.Now.Subtract(AppVars.FastTotemMessageTime).TotalSeconds >
                                        AppConsts.FastTotemMessageTimeBlockSeconds)
                                    {
                                        AppVars.FastTotemMessageTime = DateTime.Now;
                                        if (AppVars.MainForm != null && AppVars.FastNick != null)
                                            AppVars.MainForm.WriteChatMsgSafe(
                                                $"Используем <b><font color=#610B5E>тотемное нападение</font></b> на <b>{AppVars.FastNick}</b>");
                                    }

                                    AppVars.FastCount--;
                                    if (AppVars.FastCount == 0)
                                    {
                                        if (AppVars.MainForm != null)
                                            AppVars.MainForm.FastCancelSafe();
                                    }

                                    html = fastHtml;
                                    goto end;
                                }
                            }

                            break;

                        default:
                            throw new NotImplementedException($"AppVars.FastId = {AppVars.FastId}");
                    }
                }
            }
                
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateAutoboiResetDelegate(AppVars.MainForm.UpdateAutoboiReset),
                        new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }

            /*
             * Новое автопитье
             */

            if (AppVars.AutoDrink && (DateTime.Now > AppVars.NeverTimer))
            {
                var newhtml = MainPhpFindDrink(html);
                if (!string.IsNullOrEmpty(newhtml))
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.UpdateCheckTiedSafe();
                        }
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch (Exception)
                    {
                    }

                    html = newhtml;
                    goto end;
                }
            }

            /*
             * Новая рыбалка
             */

            if (AppVars.Profile.FishAuto && (DateTime.Now > AppVars.NeverTimer))
            {
                var newhtml = MainPhpFindFlora(html);
                if (!string.IsNullOrEmpty(newhtml))
                {
                    html = newhtml;
                    goto end;
                }

                newhtml = MainPhpFindFish(html);
                if (!string.IsNullOrEmpty(newhtml))
                {
                    html = newhtml;
                    goto end;
                }
            }

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadChPhpInvokeDelegate(AppVars.MainForm.ReloadChPhpInvoke),
                        new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }

            // Оббегаем карту

            if (AppVars.DoSearchBox && !AppVars.AutoMoving && (DateTime.Now > AppVars.NeverTimer))
            {
                var dest = FormMain.FindNextDestForBox();
                if (!string.IsNullOrEmpty(dest) && (AppVars.MainForm != null))
                {
                    AppVars.MainForm.MoveToSafe(dest);
                    goto end;
                }
            }

            /*
             * Новый телепорт и навигация
             */

            if (AppVars.AutoMoving && (DateTime.Now > AppVars.NeverTimer))
            {
                var cityhtml = MainPhpStartFromCityNavigation(html);
                if (!string.IsNullOrEmpty(cityhtml))
                {
                    html = cityhtml;
                    goto end;
                }

                var newhtml = MainPhpCityNavigation(html);
                if (!string.IsNullOrEmpty(newhtml))
                {
                    html = newhtml;
                    goto end;
                }

                if (html.IndexOf("var telep = ", StringComparison.Ordinal) != -1)
                {
                    newhtml = TeleportAjax(html);
                    if (newhtml != null)
                    {
                        html = newhtml;
                        goto end;
                    }
                }
            }

            /*
             * Новая карта
             */

            if (html.IndexOf("var map = ", StringComparison.Ordinal) != -1)
            {
                if ((AppVars.Profile.DoAutoDrinkBlaz) && (AppVars.Tied >= AppVars.Profile.AutoDrinkBlazTied) &&
                    (DateTime.Now > AppVars.NeverTimer))
                {
                    html = BuildRedirect("Требуется обнулить усталость", "main.php");
                    goto end;                    
                }

                html = MapAjax(html);
                if (AppVars.AutoMoving)
                    goto end;
            }

            if ((AppVars.AutoDrink || AppVars.AutoMoving) && (DateTime.Now > AppVars.NeverTimer))
            {
                var newhtml = MainPhpFindFlora(html);
                if (!string.IsNullOrEmpty(newhtml))
                {
                    html = newhtml;
                    goto end;
                }
            }

            // Нужно ли пить Эликсир восстановления?
            if (!AppVars.DoFury)
            {
                var drinkHpMa = MainPhpDrinkHpMa(address, html);
                if (!string.IsNullOrEmpty(drinkHpMa))
                {
                    html = drinkHpMa;
                    goto end;
                }
            }

            // Переключаем на полный инвентарь
            
            /*
            if (MainPhpIsInv(html))
            {
                if (
                    html.IndexOf("<tr><td bgcolor=#D8CDAF width=50% colspan=3><div align=center><font class=invtitle><font color=#000000>свойства</font>", StringComparison.CurrentCultureIgnoreCase) == -1 &&
                    html.IndexOf("<br><img src=http://image.neverlands.ru/solidst.gif", StringComparison.CurrentCultureIgnoreCase) != -1 &&
                    html.IndexOf("<a href=\"?wsi=1\">", StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    html = BuildRedirect("Переключение на полный инвентарь", "main.php?wsi=1");
                    goto end;
                }
            }
            */

            /*
            if (DateTime.Now.Subtract(AppVars.LastMainPhp).TotalMinutes > 9.5)
            {
                if (MainPhpIsInv(html))
                {
                    var idlehtml = MainPhpFindPerc(html);
                    if (!string.IsNullOrEmpty(idlehtml))
                    {
                        AppVars.IdleTimer = DateTime.Now;
                        html = idlehtml;
                    }                    
                }
                else
                {
                    var idlehtml = MainPhpFindInv(html, string.Empty);
                    if (!string.IsNullOrEmpty(idlehtml))
                    {
                        AppVars.IdleTimer = DateTime.Now;
                        html = idlehtml;
                    }                       
                }
            }
            */

            if (!string.IsNullOrEmpty(html))
            {
                html = html.Replace("document.write(view_t())", string.Empty);
            }

            end:
            if (!string.IsNullOrEmpty(html))
            {
                AppVars.ContentMainPhp = html;
                return Russian.Codepage.GetBytes(html);
            }

            return array;
        }
    }
}